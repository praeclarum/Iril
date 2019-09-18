using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Iril.Types;
using CecilInstruction = Mono.Cecil.Cil.Instruction;
using System.Runtime.InteropServices;
using System.Numerics;
using Iril.IR;
using System.Collections;

namespace Iril
{
    public class DefinedFunction
    {
        public Symbol Symbol;
        public Iril.Module IRModule;
        public IR.FunctionDeclaration IRDeclaration;
        public IR.FunctionDefinition IRDefinition;
        public MethodDefinition ILDefinition;
        public SymbolTable<ParameterDefinition> ParamSyms;
        public int ReferenceCount;
    }

    class FunctionCompiler : Emitter
    {
        // Input
        protected readonly DefinedFunction function;

        // Working Variables
        bool triedToCompile;
        readonly LivelinessTable liveliness;
        readonly SymbolTable<VariableDefinition> phiLocals = new SymbolTable<VariableDefinition>();
        readonly SymbolTable<bool> shouldInline = new SymbolTable<bool>();
        readonly BlocksContext mainContext = new BlocksContext ();

        readonly SymbolTable<LandingPad> landingPads = new SymbolTable<LandingPad> ();
        readonly SymbolTable<bool> isLandingPad = new SymbolTable<bool> ();

        public List<Message> Messages { get; } = new List<Message> ();

        public FunctionCompiler(Compilation compilation, DefinedFunction function)
            : base(compilation, function.IRModule, function.ILDefinition)
        {
            this.function = function;
            liveliness = new LivelinessTable(function);

            UMulOverflowResultTypeI64 = new Lazy<TypeDefinition> (() => GetUMulOverflowResultType (Types.IntegerType.I64));
        }

        readonly SymbolTable<int> localCounts = new SymbolTable<int> ();

        readonly SymbolTable<VariableDefinition> allocas = new SymbolTable<VariableDefinition> ();

        public void CompileFunction()
        {
            if (triedToCompile)
                return;
            triedToCompile = true;

            var f = function.IRDefinition;
            var paramSyms = function.ParamSyms;
            var md = function.ILDefinition;

            //
            // Get local usage count
            //            
            foreach (var p in paramSyms)
            {
                localCounts.Add(p.Key, 0);
            }
            foreach (var b in f.Blocks)
            {
                foreach (var i in b.AllAssignments)
                {
                    if (i.HasResult)
                        localCounts.Add(i.Result, 0);
                }
            }
            foreach (var b in f.Blocks)
            {
                var insts = b.Assignments.Select(x => x.Instruction).Concat(new IR.Instruction[] { b.Terminator });
                foreach (var i in insts)
                {
                    foreach (var l in i.ReferencedLocals)
                    {
                        localCounts[l]++;
                    }
                }
            }

            //
            // Mark which instructions are used for phi
            //
            var phiValues = new HashSet<Symbol>();
            foreach (var b in f.Blocks)
            {
                foreach (var a in b.Assignments)
                {
                    if (a.HasResult && a.Instruction is IR.PhiInstruction phi)
                    {
                        foreach (var pv in phi.Values)
                        {
                            if (pv.Value is LocalValue lv)
                            {
                                phiValues.Add(lv.Symbol);
                            }
                        }
                    }
                }
            }

            //
            // Determine whether assignments can be inlined
            //
            foreach (var p in paramSyms.Keys)
            {
                shouldInline[p] = true;
            }
            foreach (var b in f.Blocks)
            {
                var all = b.AllAssignments.ToList();

                for (var i = 0; i < all.Count; i++)
                {
                    var a = all[i];
                    if (!a.HasResult)
                        continue;
                    var symbol = a.Result;

                    // Make sure it's used only once
                    var referencedOnce = localCounts.ContainsKey(symbol) && localCounts[symbol] == 1;

                    var should = referencedOnce && !phiValues.Contains(symbol);

                    if (should)
                    {
                        // Make sure it's used in this block
                        should = false;
                        for (var j = i + 1; j < all.Count; j++)
                        {
                            if (all[j].Instruction.ReferencedLocals.Contains(symbol))
                            {
                                should = true;
                                break;
                            }
                        }

                        // Decide based on what the instruction does
                        if (a.Instruction.IsIdempotent(function.IRDefinition))
                        {
                            // OK
                        }
                        else if (a.Instruction is IR.LoadInstruction)
                        {
                            // Make sure it's used is before a state-changing instruction
                            for (var j = i + 1; should && j < all.Count; j++)
                            {
                                if (all[j].Instruction.ReferencedLocals.FirstOrDefault() == symbol)
                                {
                                    //Console.WriteLine ($"Inline {b.Assignments[i]} in {ins[j]}");
                                    should = true;
                                    break;
                                }
                                if (!all[j].Instruction.IsIdempotent(function.IRDefinition))
                                {
                                    should = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            should = false;
                        }
                    }
                    shouldInline.Add(symbol, should);
                }
            }

            //
            // Mark exception values to be inlined
            // if they are only used in a simple resume
            //
            foreach (var b in f.Blocks) {
                if (b.Assignments.Length <= 0)
                    continue;

                var a = b.Assignments[0];

                if (!a.HasResult)
                    continue;
                var symbol = a.Result;

                if (a.Instruction is LandingPadInstruction landingPad
                    && b.Terminator is ResumeInstruction resume
                    && resume.Value.Value is LocalValue rlocal
                    && rlocal.Symbol == symbol
                    && localCounts[symbol] == 1) {

                    shouldInline[symbol] = true;
                }
            }

            var vdbgs = new List<VariableDebugInformation>();

            //
            // Create variables for non-inlined assignments
            //
            foreach (var b in f.Blocks)
            {
                foreach (var a in b.AllAssignments)
                {
                    var symbol = a.Result;
                    if (a.HasResult
                        && !ShouldInline(symbol)
                        && !(a.Instruction is IR.PhiInstruction)
                        && localCounts[a.Result] > 0)
                    {

                        var irtype = a.Instruction.ResultType(function.IRModule);
                        var ctype = compilation.GetClrType(irtype, module: this.module);
                        var local = GetFreeVariable(symbol, ctype);
                        locals[a.Result] = local;
                        //var name = "local" + symbol.Text.Substring (1);
                        //var dbg = new VariableDebugInformation (local, name);
                        //vdbgs.Add (dbg);

                    }
                }
            }

            //
            // Create phi locals
            //
            foreach (var b in f.Blocks)
            {
                foreach (var a in b.Assignments)
                {
                    var symbol = a.Result;
                    if (a.HasResult && a.Instruction is IR.PhiInstruction phi)
                    {
                        var irtype = a.Instruction.ResultType(function.IRModule);
                        var ctype = compilation.GetClrType(irtype, module: this.module);
                        var local = GetFreeVariable(symbol, ctype);
                        //var local = new VariableDefinition (ctype);
                        //body.Variables.Add (local);
                        phiLocals[a.Result] = local;
                        //var name = "phi" + a.Result.Text.Substring (1);
                        //var dbg = new VariableDebugInformation (local, name);
                        //vdbgs.Add (dbg);

                        foreach (var pv in phi.Values)
                        {
                            if (pv.Value is LocalValue lv)
                            {
                                phiValues.Add(lv.Symbol);
                            }
                        }
                    }
                }
            }

            //
            // Find landing pads for exception handlers
            //
            FindLandingPads ();

            //
            // Find setjmps
            //
            FindSetjmps ();

            prev = null;

            //
            // Trace
            //
            if (ShouldTrace >= 1) {
                Emit (il.Create (OpCodes.Ldc_I4, 32));
                Emit (il.Create (OpCodes.Newobj, compilation.sysStackTraceCtor));
                Emit (il.Create (OpCodes.Callvirt, compilation.sysStackTraceGetFrameCount));
                Emit (il.Create (OpCodes.Ldc_I4, 4));
                Emit (il.Create (OpCodes.Mul));
                Emit (il.Create (OpCodes.Newobj, compilation.sysStringCharCountCtor));
                Emit (il.Create (OpCodes.Ldstr, $"{function.IRDefinition.Symbol}("));
                Emit (il.Create (OpCodes.Call, compilation.sysStringConcat));
                Emit (il.Create (OpCodes.Call, compilation.sysConsoleWrite));

                var head = "";
                for (var i = 0; i < function.IRDefinition.Parameters.Length; i++) {
                    var p = function.IRDefinition.Parameters[i];
                    Emit (il.Create (OpCodes.Ldstr, head));
                    Emit (il.Create (OpCodes.Call, compilation.sysConsoleWrite));
                    Emit (il.Create (OpCodes.Ldarg, i));
                    EmitBox (p.ParameterType);
                    Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteObj));
                    head = ", ";
                }

                Emit (il.Create (OpCodes.Ldstr, $")"));
                Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
            }

            //
            // Create target instructions for each block
            //
            var emitBlocks = (from b in f.Blocks
                              where !(isLandingPad.ContainsKey (b.Symbol) && isLandingPad[b.Symbol])
                              where !setjmpHandledBlocks.ContainsKey (b.Symbol)
                              select b).ToList ();
            foreach (var b in emitBlocks) {
                EmitBlockFirstInstruction (b, mainContext);
            }

            //
            // Emit block assignments
            //
            var sqpts = new List<(CecilInstruction, MetaSymbol)>();
            for (var i = 0; i < emitBlocks.Count; i++) {
                var b = emitBlocks[i];
                var nextBlock = i + 1 < emitBlocks.Count ? emitBlocks[i + 1] : null;
                if (setjmpBlocks.TryGetValue (b.Symbol, out var setjmp)) {
                    EmitSetjmpBlockAssignments (b, nextBlock, mainContext, setjmp);
                }
                else {
                    EmitBlockAssignments (b, nextBlock, mainContext);
                }
            }

            body.InitLocals = true;
            body.Optimize ();

            //
            // Emit try handlers
            //
            foreach (var eh in ehs) {
                var handler = new ExceptionHandler (ExceptionHandlerType.Catch) {
                    TryStart = eh.TryStart,
                    TryEnd = eh.TryLast.Next,
                    HandlerStart = eh.TryLast.Next,
                    HandlerEnd = eh.CatchLast.Next,
                    CatchType = compilation.sysException,
                };
                body.ExceptionHandlers.Add (handler);
            }

            //
            // Emit setjmp handlers
            //
            foreach (var eh in setjmpBlocks.Values) {
                var handler = new ExceptionHandler (ExceptionHandlerType.Catch) {
                    TryStart = eh.TryStart,
                    TryEnd = eh.TryLast.Next,
                    HandlerStart = eh.TryLast.Next,
                    HandlerEnd = eh.CatchLast.Next,
                    CatchType = compilation.LongjmpException,
                };
                body.ExceptionHandlers.Add (handler);
            }

            //
            // Add sequence points
            //
            foreach (var (cinst, dbgSym) in sqpts) {
                var sp = TryGetSequencePoint (dbgSym);
                if (sp != null)
                    method.DebugInformation.SequencePoints.Add (sp);
            }

            //
            // Add local variable names
            //
            var bodyDbg = new ScopeDebugInformation(body.Instructions.First(), null);

            foreach (var b in emitBlocks)
            {
                if (!mainContext.BlockFirstInstr.ContainsKey (b.Symbol) || !mainContext.BlockLastInstr.ContainsKey (b.Symbol))
                    continue;
                var scope = new ScopeDebugInformation(mainContext.BlockFirstInstr[b.Symbol], mainContext.BlockLastInstr[b.Symbol]);
                foreach (var a in b.Assignments)
                {
                    if (!a.HasResult)
                        continue;
                    var name = "";
                    if (blockLocalNames.TryGetValue (b.Symbol, out var localNames)
                        && localNames.TryGetValue (a.Result, out var dbgName)) {
                        name = dbgName + "_";
                    }

                    if (locals.TryGetValue(a.Result, out var vd))
                    {
                        name += "local" + a.Result.Text.Substring(1) + "_";
                        var vdbg = new VariableDebugInformation(vd, name);
                        vdbg.IsDebuggerHidden = false;
                        scope.Variables.Add(vdbg);
                    }
                    else if (phiLocals.TryGetValue(a.Result, out vd))
                    {
                        name += "phi" + a.Result.Text.Substring(1) + "_";
                        var vdbg = new VariableDebugInformation(vd, name);
                        vdbg.IsDebuggerHidden = false;
                        scope.Variables.Add(vdbg);
                    }
                }
                bodyDbg.Scopes.Add(scope);
            }

            md.DebugInformation.Scope = bodyDbg;
            md.Body = body;
        }

        private SequencePoint TryGetSequencePoint (MetaSymbol dbgSym)
        {
            if (module.Metadata.TryGetValue (dbgSym, out var dbg) && dbg is SymbolTable<object> dbgVals) {
                var cinstr = prev;
                if (dbgVals.TryGetValue (Symbol.Line, out var lineO) && lineO is Constant line
                    && dbgVals.TryGetValue (Symbol.Column, out var columnO) && columnO is Constant column
                    && dbgVals.TryGetValue (Symbol.Scope, out var scopeO) && scopeO is MetaSymbol scopeRef) {

                    var doc = compilation.GetScopeDocument (module, scopeRef);
                    if (doc != null) {
                        var sp = new SequencePoint (cinstr, doc);
                        sp.StartLine = line.Int32Value;
                        sp.EndLine = line.Int32Value;
                        sp.StartColumn = column.Int32Value;
                        sp.EndColumn = column.Int32Value + 1;
                        return sp;
                    }
                }
            }
            return null;
        }

        void EmitBlocks (List<Block> emitBlocks, BlocksContext context)
        {
            foreach (var b in emitBlocks) {
                EmitBlockFirstInstruction (b, context);
            }

            //
            // Emit block assignments
            //
            for (var i = 0; i < emitBlocks.Count; i++) {
                var b = emitBlocks[i];
                var nextBlock = i + 1 < emitBlocks.Count ? emitBlocks[i + 1] : null;

                if (setjmpBlocks.TryGetValue (b.Symbol, out var innerSetjmp)) {
                    EmitSetjmpBlockAssignments (b, nextBlock: null, context, innerSetjmp);
                }
                else if (!(isLandingPad.ContainsKey (b.Symbol) && isLandingPad[b.Symbol])) {
                    EmitBlockAssignments (b, nextBlock, context);
                }                
            }
        }

        void EmitSetjmpBlockAssignments (Block setjmpBlock, Block nextBlock, BlocksContext origContext, SetjmpInfo setjmp)
        {
            var b = setjmpBlock;
            prev = origContext.BlockHeadLastInstr[b.Symbol];
            foreach (var a in b.Assignments) {
                if (!ReferenceEquals (setjmp.SetjmpCallAssignment, a))
                    EmitBlockAssignment (b, nextBlock: null, origContext, a);
            }
            origContext.BlockLastInstr[b.Symbol] = prev;

            Emit (OpCodes.Nop);
            setjmp.TryStart = prev;

            if (setjmp.InitialBlock != setjmp.ExitBlockSymbol && setjmp.InitialBlocks.Count > 0) {
                var context = new BlocksContext (origContext, setjmp.InitialBlocks);
                var emitBlocks = setjmp.InitialBlocks.Select (x => setjmp.BlockIndex[x]).ToList ();
                //EmitBlockAssignment (emitBlocks[0], null, context, new Assignment (setjmp.SetjmpCallAssignment.Result, IR.Instruction.ZeroI32));
                EmitBlocks (emitBlocks, context);
            }
            else {
                if (setjmp.ExitBlockSymbol != null) {
                    Emit (il.Create (OpCodes.Leave, GetLabel (new LabelValue ((LocalSymbol)setjmp.ExitBlockSymbol), setjmpBlock, origContext)));
                }
                else {
                    throw new NotSupportedException ($"Empty try block without exit");
                }
            }

            setjmp.TryLast = prev;

            if (setjmp.LaterBlock != setjmp.ExitBlockSymbol && setjmp.LaterBlocks.Count > 0) {
                var context = new BlocksContext (origContext, setjmp.LaterBlocks);
                var emitBlocks = setjmp.LaterBlocks.Select (x => setjmp.BlockIndex[x]).ToList ();
                //EmitBlockAssignment (emitBlocks[0], null, context, new Assignment (setjmp.SetjmpCallAssignment.Result, IR.Instruction.OneI32));
                EmitBlocks (emitBlocks, context);
            }
            else {
                if (setjmp.ExitBlockSymbol != null) {
                    Emit (il.Create (OpCodes.Leave, GetLabel (new LabelValue ((LocalSymbol)setjmp.ExitBlockSymbol), setjmpBlock, origContext)));
                }
                else {
                    throw new NotSupportedException ($"Empty catch block without exit");
                }
            }

            setjmp.CatchLast = prev;
        }

        void EmitBlockAssignments (Block b, Block nextBlock, BlocksContext context)
        {
            prev = context.BlockHeadLastInstr[b.Symbol];

            foreach (var a in b.Assignments) {
                EmitBlockAssignment (b, nextBlock, context, a);
            }
            EmitInstruction (b.TerminatorAssignment.Result, b.Terminator, b, nextBlock, context, unsigned: null);

            context.BlockLastInstr[b.Symbol] = prev;
        }

        private void EmitBlockAssignment (Block b, Block nextBlock, BlocksContext context, Assignment a)
        {
            if (!ShouldInline (a.Result)
                                && !(a.Instruction is IR.PhiInstruction)) {

                EmitInstruction (a.Result, a.Instruction, b, nextBlock, context, unsigned: null);

                if (a.HasDebugSymbol) {
                    //sqpts.Add ((prev, a.DebugSymbol));
                }

                // If we need to assign it, do so
                if (locals.TryGetValue (a.Result, out var vd)) {
                    Emit (il.Create (OpCodes.Stloc, vd));
                }
                else {
                    // If it produced a value but it's discarded, pop it
                    if (a.Result != LocalSymbol.None && localCounts[a.Result] == 0) {
                        Emit (il.Create (OpCodes.Pop));
                    }
                }
            }
        }

        class BlocksContext
        {
            public readonly bool IsExceptionHandler;
            public readonly SymbolTable<CecilInstruction> BlockFirstInstr;
            public readonly SymbolTable<SymbolTable<CecilInstruction>> BlockPredInstr;
            public readonly SymbolTable<CecilInstruction> BlockHeadLastInstr;
            public readonly SymbolTable<CecilInstruction> BlockLastInstr;
            public readonly SymbolSet ProtectedBlocks;
            public BlocksContext ()
            {
                IsExceptionHandler = false;
                BlockFirstInstr = new SymbolTable<CecilInstruction> ();
                BlockPredInstr = new SymbolTable<SymbolTable<CecilInstruction>> ();
                BlockHeadLastInstr = new SymbolTable<CecilInstruction> ();
                BlockLastInstr = new SymbolTable<CecilInstruction> ();
            }
            public BlocksContext (BlocksContext other, IEnumerable<Symbol> protectedBlocks)
            {
                IsExceptionHandler = true;
                ProtectedBlocks = new SymbolSet (protectedBlocks);
                BlockFirstInstr = new SymbolTable<CecilInstruction> (other.BlockFirstInstr);
                BlockHeadLastInstr = new SymbolTable<CecilInstruction> (other.BlockHeadLastInstr);
                BlockLastInstr = new SymbolTable<CecilInstruction> (other.BlockLastInstr);
                BlockPredInstr = new SymbolTable<SymbolTable<CecilInstruction>> ();
                foreach (var kv in other.BlockPredInstr) {
                    var r = new SymbolTable<CecilInstruction> (kv.Value);
                    BlockPredInstr[kv.Key] = r;
                }
            }

            public bool IsProtecting (LabelValue destination)
            {
                return ProtectedBlocks.Contains (destination.Symbol);
            }
        }

        private void EmitBlockFirstInstruction (Block b, BlocksContext context)
        {
            var firstI = il.Create (OpCodes.Nop);

            var phipreds = b.PhiPredecessors.ToList ();
            if (phipreds.Count > 0) {
                var phis = b.Assignments.Where (x => x.Instruction is PhiInstruction).ToList ();
                var pis = new SymbolTable<CecilInstruction> ();
                context.BlockPredInstr[b.Symbol] = pis;
                for (var j = 0; j < phipreds.Count; j++) {
                    var pred = phipreds[j];
                    var i = il.Create (OpCodes.Nop);
                    pis[pred] = i;
                    Emit (i);
                    var phiVs = new List<(Assignment Assignment, PhiInstruction Phi, Value Value)> ();
                    foreach (var oa in phis) {
                        var phi = (PhiInstruction)oa.Instruction;
                        foreach (var v in phi.Values.Where (x => ((LocalValue)x.Label).Symbol == pred)) {
                            phiVs.Add ((oa, phi, v.Value));
                        }
                    }
                    EmitPhis (phiVs);
                    if (j + 1 < phipreds.Count) {
                        Emit (il.Create (OpCodes.Br, firstI));
                    }
                }
            }

            Emit (firstI);
            context.BlockFirstInstr[b.Symbol] = firstI;

            //
            // Block Trace
            //
            if (ShouldTrace >= 2) {
                Emit (il.Create (OpCodes.Ldc_I4, 32));
                Emit (il.Create (OpCodes.Newobj, compilation.sysStackTraceCtor));
                Emit (il.Create (OpCodes.Callvirt, compilation.sysStackTraceGetFrameCount));
                Emit (il.Create (OpCodes.Ldc_I4, 4));
                Emit (il.Create (OpCodes.Mul));
                Emit (il.Create (OpCodes.Newobj, compilation.sysStringCharCountCtor));

                var message = $"- {b.Symbol}";

                var fass = b.Assignments.FirstOrDefault (x => x.HasDebugSymbol);
                if (fass != null) {
                    var seq = TryGetSequencePoint (fass.DebugSymbol);
                    if (seq != null) {
                        message += $" {seq.Document.Url}:{seq.StartLine}";
                    }
                }

                Emit (il.Create (OpCodes.Ldstr, message));
                Emit (il.Create (OpCodes.Call, compilation.sysStringConcat));
                Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
            }

            //
            // Block Debugger Break
            //
            //i = il.Create (OpCodes.Call, compilation.sysDebuggerBreak);
            //il.Append (i);

            context.BlockHeadLastInstr[b.Symbol] = prev;
        }

        void FindLandingPads ()
        {
            var blocks = function.IRDefinition.Blocks;
            var blockIndex = new SymbolTable<Block> ();
            foreach (var b in blocks)
                blockIndex[b.Symbol] = b;

            foreach (var ab in blocks) {
                var s = ab.Symbol;
                var first = ab.FirstNonPhiAssignment;
                if (first.Instruction is LandingPadInstruction landing) {
                    var l = new LandingPad {
                        Block = ab,
                        Assignment = first,
                        Blocks = new List<Block> (),
                    };
                    landingPads[s] = l;
                    var needsVisit = new List<Symbol> { ab.Symbol };
                    var visited = new SymbolTable<bool> ();
                    while (needsVisit.Count > 0) {
                        var b = blockIndex[needsVisit[0]];
                        needsVisit.RemoveAt (0);
                        visited[b.Symbol] = true;
                        isLandingPad[b.Symbol] = true;
                        l.Blocks.Add (b);

                        var t = b.Terminator;
                        if (t is IR.ResumeInstruction resume) {
                            b = null;
                        }
                        else if (t is IR.InvokeInstruction invoke) {
                            var ns = invoke.NormalLabel.Symbol;
                            if (!visited.ContainsKey (ns) && !needsVisit.Contains (ns)) {
                                needsVisit.Add (ns);
                            }
                        }
                        else {
                            foreach (var ns in t.NextLabelSymbols) {
                                if (!visited.ContainsKey (ns) && !needsVisit.Contains (ns)) {
                                    needsVisit.Add (ns);
                                }
                            }
                        }
                    }
                }
            }

            //
            // Unmark isLandingPad for blocks accessible from non-landing pads
            //
            var changed = true;
            while (changed) {
                changed = false;
                foreach (var b in blocks) {
                    if (changed)
                        break;
                    if (isLandingPad.TryGetValue (b.Symbol, out var isLanding) && isLanding)
                        continue;
                    if (b.Terminator is InvokeInstruction) {
                    }
                    else {
                        foreach (var l in b.Terminator.NextLabelSymbols) {
                            if (isLandingPad.TryGetValue (l, out isLanding) && isLanding) {
                                changed = true;
                                isLandingPad.Remove (l);
                                break;
                            }
                        }
                    }
                }
            }
        }

        void FindSetjmps ()
        {
            var blocks = function.IRDefinition.Blocks;
            var blockIndex = new SymbolTable<Block> ();
            foreach (var b in blocks)
                blockIndex[b.Symbol] = b;
            FindSetjmps (blocks.First ().Symbol, blockIndex);
        }

        void FindSetjmps (List<Symbol> blocks, SymbolTable<Block> blockIndex)
        {
            if (blocks.Count == 0)
                return;
            var newBlockIndex = new SymbolTable<Block> ();
            foreach (var b in blocks)
                newBlockIndex[b] = blockIndex[b];
            FindSetjmps (blocks.First (), newBlockIndex);
        }

        readonly SymbolTable<SetjmpInfo> setjmpBlocks = new SymbolTable<SetjmpInfo> ();
        readonly SymbolTable<SetjmpInfo> setjmpHandledBlocks = new SymbolTable<SetjmpInfo> ();

        void FindSetjmps (Symbol firstBlockSymbol, SymbolTable<Block> blockIndex)
        {
            var analyzed = new SymbolSet ();
            var needsAnalysis = new SymbolQueue ();
            needsAnalysis.Enqueue (firstBlockSymbol);
            while (needsAnalysis.Count > 0) {
                var blockSymbol = needsAnalysis.Dequeue ();
                analyzed.Add (blockSymbol);
                var block = blockIndex[blockSymbol];

                //
                // Find the setjmp
                //
                Assignment con = null, call = null;
                SetjmpInfo setjmpInfo = null;
                if (block.Terminator is ConditionalBrInstruction br
                    && br.Condition is LocalValue conv) {
                    con = block.FindAssignment (conv.Symbol);
                    if (con != null
                        && con.Instruction is IcmpInstruction icmp
                        && icmp.Op1 is LocalValue icmpop1v) {
                        call = block.FindAssignment (icmpop1v.Symbol);
                        if (call != null
                            && call.Instruction is CallInstruction calli
                            && calli.Arguments.Length == 1
                            && calli.Pointer is GlobalValue setjmp
                            && setjmp.Symbol.Text == "@setjmp") {
                            // FOUND SETJMP
                            setjmpInfo = new SetjmpInfo {
                                SetjmpBlock = blockSymbol,
                                SetjmpCallAssignment = call,
                                InitialBlock = br.IfTrue.Symbol,
                                LaterBlock = br.IfFalse.Symbol,
                                Argument = calli.Arguments[0],
                                BlockIndex = new SymbolTable<Block> (blockIndex),
                            };
                        }
                    }
                }

                //
                // If found,
                // 1. trace blocks to find: (a) initial blocks, (b) later blocks, (c) exit block
                // 2. remember it
                // 3. recursively look for setjmps in the initial and later blocks
                // 4. remove the used blocks from block index
                // 5. enqueue the exit block to keep looking for other setjmps
                //
                if (setjmpInfo != null) {
                    //Console.WriteLine ("FOUND SETJMP IN " + function.Symbol);

                    //
                    // 1. trace blocks to find: (a) initial blocks, (b) later blocks, (c) exit block
                    //
                    var initialTrace = TraceBlocks (setjmpInfo.InitialBlock, blockSymbol, blockIndex);
                    var laterTrace = TraceBlocks (setjmpInfo.LaterBlock, blockSymbol, blockIndex);
                    var common = new List<Symbol> (initialTrace);
                    for (var i = 0; i < common.Count;) {
                        var s = common[i];
                        if (laterTrace.Contains (s)) {
                            i++;
                        }
                        else {
                            common.RemoveAt (i);
                        }
                    }
                    setjmpInfo.ExitBlockSymbol = common.FirstOrDefault ();
                    setjmpInfo.InitialBlocks = new List<Symbol> (initialTrace);
                    for (var i = 0; i < setjmpInfo.InitialBlocks.Count;) {
                        if (common.Contains (setjmpInfo.InitialBlocks[i])) {
                            setjmpInfo.InitialBlocks.RemoveAt (i);
                        }
                        else {
                            i++;
                        }
                    }
                    setjmpInfo.LaterBlocks = new List<Symbol> (laterTrace);
                    for (var i = 0; i < setjmpInfo.LaterBlocks.Count;) {
                        if (common.Contains (setjmpInfo.LaterBlocks[i])) {
                            setjmpInfo.LaterBlocks.RemoveAt (i);
                        }
                        else {
                            i++;
                        }
                    }

                    //
                    // 2. remember it
                    //
                    setjmpBlocks[setjmpInfo.SetjmpBlock] = setjmpInfo;
                    foreach (var b in setjmpInfo.InitialBlocks)
                        setjmpHandledBlocks[b] = setjmpInfo;
                    foreach (var b in setjmpInfo.LaterBlocks)
                        setjmpHandledBlocks[b] = setjmpInfo;

                    //
                    // 3. recursively look for setjmps in the initial and later blocks
                    //
                    FindSetjmps (setjmpInfo.InitialBlocks, blockIndex);
                    FindSetjmps (setjmpInfo.LaterBlocks, blockIndex);

                    //
                    // 4. remove the used blocks from block index
                    //
                    foreach (var b in setjmpInfo.InitialBlocks)
                        blockIndex.Remove (b);
                    foreach (var b in setjmpInfo.LaterBlocks)
                        blockIndex.Remove (b);

                    //
                    // 5. enqueue the exit block to keep looking for other setjmps
                    //
                    var exit = setjmpInfo.ExitBlockSymbol;
                    if (exit != null && blockIndex.ContainsKey (exit) && !analyzed.Contains(exit) && !needsAnalysis.Contains(exit))
                        needsAnalysis.Enqueue (exit);
                }
                else {
                    //
                    // If not found, keep tracing
                    //
                    foreach (var n in block.Terminator.NextLabelSymbols) {
                        if (blockIndex.ContainsKey (n) && !analyzed.Contains (n) && !needsAnalysis.Contains (n))
                            needsAnalysis.Enqueue (n);
                    }
                }
            }
        }

        class SetjmpInfo
        {
            public Symbol SetjmpBlock;
            public Assignment SetjmpCallAssignment;
            public Symbol InitialBlock;
            public Symbol LaterBlock;
            public Argument Argument;
            public Symbol ExitBlockSymbol;
            public List<Symbol> InitialBlocks;
            public List<Symbol> LaterBlocks;
            public SymbolTable<Block> BlockIndex;

            public CecilInstruction TryStart, TryLast, CatchLast;
        }

        List<Symbol> TraceBlocks (Symbol firstBlockSymbol, Symbol terminator, SymbolTable<Block> blocks)
        {
            var r = new List<Symbol> ();
            var visited = new SymbolSet ();
            var tovisit = new SymbolQueue ();
            tovisit.Enqueue (firstBlockSymbol);
            while (tovisit.Count > 0) {
                var blockSymbol = tovisit.Dequeue ();
                if (visited.Contains (blockSymbol))
                    continue;
                visited.Add (blockSymbol);
                if (terminator != null && blockSymbol == terminator)
                    continue;
                if (!blocks.TryGetValue (blockSymbol, out var block))
                    continue;

                r.Add (blockSymbol);

                foreach (var n in block.Terminator.NextLabelSymbols) {
                    if (!visited.Contains (n) && !tovisit.Contains (n))
                        tovisit.Enqueue (n);
                }
            }
            return r;
        }

        void EmitPhis (List<(Assignment Assignment, PhiInstruction Phi, Value Value)> phiVs)
        {
            // Recursive phis need to be handled specially
            // Make sure to emit all reads before overwriting the phi
            // Start by making a list of all the reads
            var phiSyms = new HashSet<LocalSymbol>(phiVs.Select(x => x.Assignment.Result));
            var phiRs = new SymbolTable<List<Symbol>>();
            var phiIndex = new SymbolTable<int>();
            foreach (var s in phiSyms)
            {
                phiRs[s] = new List<Symbol>();
            }
            for (var i = 0; i < phiVs.Count; i++)
            {
                var p = phiVs[i];
                phiIndex[p.Assignment.Result] = i;
                foreach (var v in p.Phi.Values)
                {
                    if (v.Value is LocalValue lv && phiSyms.Contains(lv.Symbol))
                    {
                        phiRs[lv.Symbol].Add(p.Assignment.Result);
                    }
                }
            }

            var ok = true;
            while (ok && phiRs.Count > 0)
            {
                var reads = phiRs.Where(x => x.Value.Count == 0).ToList();
                if (reads.Count > 0)
                {
                    foreach (var r in reads)
                    {
                        var index = phiIndex[r.Key];
                        phiRs.Remove(r.Key);
                        EmitPhiRead(index);
                        var sym = phiVs[index].Assignment.Result;
                        foreach (var rr in phiRs)
                        {
                            rr.Value.Remove(sym);
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException("Mutually recursive phi values");
                }
            }

            void EmitPhiRead(int index)
            {
                var (oa, phi, phiV) = phiVs[index];
                var phiLocal = GetPhiLocal(oa.Result);
                EmitValue(phiV, phi.Type);
                Emit(il.Create(OpCodes.Stloc, phiLocal));
            }
        }

        bool HasLocal(LocalSymbol symbol)
        {
            return locals.ContainsKey(symbol);
        }

        bool ShouldInline(LocalSymbol symbol)
        {
            return shouldInline.TryGetValue(symbol, out var s) && s;
        }

        VariableDefinition GetPhiLocal(Symbol assignment)
        {
            return phiLocals[assignment];
        }

        void EmitInstruction(LocalSymbol assignedSymbol, IR.Instruction instruction, IR.Block block, IR.Block nextBlock, BlocksContext context, bool? unsigned)
        {
            switch (instruction)
            {
                case IR.AddInstruction add:
                    if (add.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Add, add.Op1, add.Op2, (Types.VectorType)add.Type);
                    }
                    else
                    {
                        EmitValue(add.Op1, add.Type);
                        EmitValue(add.Op2, add.Type);
                        Emit(il.Create(OpCodes.Add));
                        if (add.Type is IntegerType intt) {
                            var bits = intt.Bits;
                            var upBits = Compilation.RoundUpIntBits (bits);
                            switch (upBits) {
                                case 8:
                                    Emit (il.Create (OpCodes.Conv_U1));
                                    break;
                                case 16:
                                    Emit (il.Create (OpCodes.Conv_I2));
                                    break;
                            }
                        }
                    }
                    break;
                case IR.AllocaInstruction alloca:
                    EmitAllocaSize (alloca);
                    Emit (il.Create (OpCodes.Localloc));

                    if (compilation.Options.SafeMemory) {
                        var v = new VariableDefinition (compilation.sysBytePtr);
                        body.Variables.Add (v);
                        allocas.Add (assignedSymbol, v);
                        Emit (il.Create (OpCodes.Dup));
                        Emit (il.Create (OpCodes.Stloc, v));
                        Emit (il.Create (OpCodes.Ldloc, v));
                        EmitAllocaSize (alloca);
                        Emit (il.Create (OpCodes.Conv_I8));
                        Emit (il.Create (OpCodes.Ldstr, $"{function.Symbol}.alloca"));
                        Emit (il.Create (OpCodes.Call, compilation.GetSystemMethod ("@_register_memory")));
                    }
                    break;
                case IR.AndInstruction and:
                    if (and.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.And, and.Op1, and.Op2, (Types.VectorType)and.Type);
                    }
                    else
                    {
                        EmitValue(and.Op1, and.Type);
                        EmitValue(and.Op2, and.Type);
                        Emit(il.Create(OpCodes.And));
                    }
                    break;
                case IR.AshrInstruction ashr: {
                        var shiftType = ashr.Type;
                        if (ashr.Type is IntegerType intt) {
                            switch (Compilation.RoundUpIntBits (intt.Bits)) {
                                case 8:
                                case 16:
                                case 32:
                                    shiftType = IntegerType.I32;
                                    break;
                                default:
                                    shiftType = IntegerType.I64;
                                    break;
                            }
                        }
                        EmitSext (shiftType, new TypedValue (ashr.Type, ashr.Op1));
                        EmitValue (ashr.Op2, Types.IntegerType.I32);
                        Emit (il.Create (OpCodes.Shr));
                    }
                    break;
                case IR.BitcastInstruction bitcast:
                    // CLR doesn't need bitcast
                    EmitTypedValue(bitcast.Input);
                    break;
                case IR.CallInstruction call:
                    EmitCall(call, block);
                    break;
                case IR.ConditionalBrInstruction cbr: {
                        var trueDest = GetLabel (cbr.IfTrue, block, context);
                        CecilInstruction leaveDest = null;
                        if (context.IsExceptionHandler && !context.IsProtecting (cbr.IfTrue)) {
                            leaveDest = il.Create (OpCodes.Leave, trueDest);
                            EmitBrtrue (cbr.Condition, Types.IntegerType.I1, leaveDest);
                        }
                        else {
                            EmitBrtrue (cbr.Condition, Types.IntegerType.I1, trueDest);
                        }
                        //if (cbr.IfFalse.Symbol != nextBlock?.Symbol)
                        var falseDest = GetLabel (cbr.IfFalse, block, context);
                        if (context.IsExceptionHandler && !context.IsProtecting (cbr.IfFalse)) {
                            Console.WriteLine ("EMIT FARLSE");
                            Emit (il.Create (OpCodes.Leave, falseDest));
                        }
                        else {
                            Emit (il.Create (OpCodes.Br, falseDest));
                        }
                        if (leaveDest != null)
                            Emit (leaveDest);
                    }
                    break;
                case IR.DivInstruction div:
                    if (div.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Div, div.Op1, div.Op2, (Types.VectorType)div.Type);
                    }
                    else
                    {
                        EmitValue(div.Op1, div.Type);
                        EmitValue(div.Op2, div.Type);
                        Emit(il.Create(OpCodes.Div));
                    }
                    break;
                case IR.ExtractElementInstruction ee:
                    {
                        EmitTypedValue(ee.Value);
                        var index = ((IR.Constant)ee.Index.Value).Int32Value;
                        var v = GetVectorType((VectorType)ee.Value.Type);
                        var field = v.ElementFields[index];
                        Emit(il.Create(OpCodes.Ldfld, field));
                    }
                    break;
                case IR.ExtractValueInstruction ee:
                    EmitExtractValue (ee.Value, ee.Indices);
                    break;
                case IR.FaddInstruction fadd:
                    if (fadd.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Add, fadd.Op1, fadd.Op2, (Types.VectorType)fadd.Type);
                    }
                    else
                    {
                        EmitValue(fadd.Op1, fadd.Type);
                        EmitValue(fadd.Op2, fadd.Type);
                        Emit(il.Create(OpCodes.Add));
                    }
                    break;
                case IR.FcmpInstruction fcmp:
                    EmitFcmp(fcmp);
                    break;
                case IR.FdivInstruction add:
                    if (add.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Div, add.Op1, add.Op2, (Types.VectorType)add.Type);
                    }
                    else
                    {
                        EmitValue(add.Op1, add.Type);
                        EmitValue(add.Op2, add.Type);
                        Emit(il.Create(OpCodes.Div));
                    }
                    break;
                case IR.FmulInstruction fmul:
                    if (fmul.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Mul, fmul.Op1, fmul.Op2, (Types.VectorType)fmul.Type);
                    }
                    else
                    {
                        EmitValue(fmul.Op1, fmul.Type);
                        EmitValue(fmul.Op2, fmul.Type);
                        Emit(il.Create(OpCodes.Mul));
                    }
                    break;
                case IR.FpextInstruction fpext:
                    EmitTypedValue(fpext.Value);
                    switch (fpext.Type)
                    {
                        case Types.FloatType fltt:
                            switch (fltt.Bits)
                            {
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_R4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_R8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot fpext {fpext.Type}");
                    }
                    break;
                case IR.FptosiInstruction fptosi:
                    EmitTypedValue(fptosi.Value);
                    switch (fptosi.Type)
                    {
                        case Types.IntegerType intt:
                            switch (Compilation.RoundUpIntBits (intt.Bits))
                            {
                                case 8:
                                    Emit(il.Create(OpCodes.Conv_I1));
                                    break;
                                case 16:
                                    Emit(il.Create(OpCodes.Conv_I2));
                                    break;
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_I4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_I8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot fptoui {fptosi.Type}");
                    }
                    break;
                case IR.FptouiInstruction fptoui:
                    EmitTypedValue(fptoui.Value);
                    switch (fptoui.Type)
                    {
                        case Types.IntegerType intt:
                            switch (Compilation.RoundUpIntBits (intt.Bits))
                            {
                                case 8:
                                    Emit(il.Create(OpCodes.Conv_U1));
                                    break;
                                case 16:
                                    Emit(il.Create(OpCodes.Conv_U2));
                                    break;
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_U4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_U8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot fptoui {fptoui.Type}");
                    }
                    break;
                case IR.FsubInstruction fsub:
                    if (fsub.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Sub, fsub.Op1, fsub.Op2, (Types.VectorType)fsub.Type);
                    }
                    else
                    {
                        EmitValue(fsub.Op1, fsub.Type);
                        EmitValue(fsub.Op2, fsub.Type);
                        Emit(il.Create(OpCodes.Sub));
                    }
                    break;
                case IR.GetElementPointerInstruction gep:
                    EmitGetElementPointer(gep.Pointer, gep.Indices);
                    break;
                case IR.IcmpInstruction icmp:
                    EmitIcmp(icmp);
                    break;
                case IR.InsertElementInstruction ie:
                    EmitInsertElement (ie.Value, ie.Element, ie.Index);
                    break;
                case IR.InsertValueInstruction iv:
                    EmitInsertValue (iv.Value, iv.Element, iv.Indices);
                    break;
                case IR.InttoptrInstruction inttoptr:
                    EmitTypedValue(inttoptr.Value);
                    switch (inttoptr.Type)
                    {
                        case Types.PointerType ptrt:
                            Emit(il.Create(OpCodes.Conv_U));
                            break;
                        default:
                            throw new NotSupportedException($"Cannot inttoptr {inttoptr.Type}");
                    }
                    break;
                case IR.InvokeInstruction invoke:
                    EmitInvoke (assignedSymbol, invoke, block, context);
                    break;
                case IR.LandingPadInstruction landing:
                    if (locals.TryGetValue (assignedSymbol, out var _)) {
                        var locData = GetStructTempLocal (landing.Type);
                        Emit (il.Create (OpCodes.Ldloca, locData));
                        Emit (il.Create (OpCodes.Initobj, locData.VariableType));
                        Emit (il.Create (OpCodes.Isinst, compilation.nativeException.Value));
                        Emit (OpCodes.Dup);
                        Emit (il.Create (OpCodes.Stloc, nativeExceptionLocal.Value));
                        var next = il.Create (OpCodes.Ldloc, locData);
                        Emit (il.Create (OpCodes.Brfalse, next));
                        Emit (il.Create (OpCodes.Ldloc, nativeExceptionLocal.Value));
                        Emit (il.Create (OpCodes.Ldfld, compilation.nativeExceptionData));
                        Emit (il.Create (OpCodes.Unbox_Any, locData.VariableType));
                        Emit (il.Create (OpCodes.Stloc, locData));
                        Emit (next);
                    }
                    else {
                        Emit (OpCodes.Pop);
                    }
                    break;
                case IR.LoadInstruction load:
                    EmitLoad(load, unsigned: unsigned);
                    break;
                case IR.LshrInstruction lshr:
                    EmitValue(lshr.Op1, lshr.Type, unsigned: true);
                    EmitValue(lshr.Op2, Types.IntegerType.I32);
                    Emit(il.Create(OpCodes.Shr_Un));
                    break;
                case IR.MultiplyInstruction mul:
                    EmitValue(mul.Op1, mul.Type);
                    EmitValue(mul.Op2, mul.Type);
                    Emit(il.Create(OpCodes.Mul));
                    break;
                case IR.OrInstruction or:
                    EmitValue(or.Op1, or.Type);
                    EmitValue(or.Op2, or.Type);
                    Emit(il.Create(OpCodes.Or));
                    break;
                case IR.PhiInstruction phi:
                    Emit(il.Create(OpCodes.Ldloc, GetPhiLocal(assignedSymbol)));
                    break;
                case IR.PtrtointInstruction zext:
                    EmitTypedValue(zext.Value);
                    switch (zext.Type)
                    {
                        case Types.IntegerType intt:
                            switch (Compilation.RoundUpIntBits (intt.Bits)) {
                                case 8:
                                    Emit(il.Create(OpCodes.Conv_I1));
                                    break;
                                case 16:
                                    Emit(il.Create(OpCodes.Conv_I2));
                                    break;
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_I4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_I8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot ptrtoint {zext.Type}");
                    }
                    break;
                case IR.ResumeInstruction resume:
                    Emit (il.Create (OpCodes.Rethrow));
                    break;
                case IR.RetInstruction ret:
                    EmitTypedValue(ret.Value);
                    if (ShouldTrace >= 1 && !(ret.Value.Type is VoidType)) {
                        Emit (il.Create (OpCodes.Ldc_I4, 32));
                        Emit (il.Create (OpCodes.Newobj, compilation.sysStackTraceCtor));
                        Emit (il.Create (OpCodes.Callvirt, compilation.sysStackTraceGetFrameCount));
                        Emit (il.Create (OpCodes.Ldc_I4, 4));
                        Emit (il.Create (OpCodes.Mul));
                        Emit (il.Create (OpCodes.Newobj, compilation.sysStringCharCountCtor));
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleWrite));
                        Emit (il.Create (OpCodes.Ldstr, "= "));
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleWrite));
                        Emit (il.Create (OpCodes.Dup));
                        EmitBox (ret.Value.Type);
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteObj));
                        Emit (il.Create (OpCodes.Ldstr, ""));
                        Emit (il.Create (OpCodes.Call, compilation.sysConsoleWriteLine));
                    }
                    if (compilation.Options.SafeMemory) {
                        EmitUnregisterAllocas ();
                    }
                    Emit (il.Create(OpCodes.Ret));
                    break;
                case IR.SdivInstruction sdiv:
                    if (sdiv.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Div, sdiv.Op1, sdiv.Op2, (Types.VectorType)sdiv.Type);
                    }
                    else
                    {
                        EmitValue(sdiv.Op1, sdiv.Type);
                        EmitValue(sdiv.Op2, sdiv.Type);
                        Emit(il.Create(OpCodes.Div));
                    }
                    break;
                case IR.SextInstruction sext:
                    EmitSext (sext.Type, sext.Value);
                    break;
                case IR.SelectInstruction sel:
                    if (sel.Type is VectorType selV)
                    {
                        EmitVSelect(sel, selV);
                    }
                    else
                    {
                        var end = il.Create(OpCodes.Nop);
                        var trueI = il.Create(OpCodes.Nop);

                        EmitBrtrue(sel.Condition, sel.Type, trueI);

                        EmitTypedValue(sel.Value2);
                        Emit(il.Create(OpCodes.Br, end));

                        Emit(trueI);
                        EmitTypedValue(sel.Value1);

                        Emit(end);
                    }
                    break;
                case IR.ShlInstruction shl:
                    EmitValue(shl.Op1, shl.Type);
                    EmitValue(shl.Op2, IntegerType.I32);
                    Emit(il.Create(OpCodes.Shl));
                    break;
                case IR.ShuffleVectorInstruction sh:
                    {
                        var type1 = (VectorType)sh.Value1.Type;
                        var type2 = (VectorType)sh.Value2.Type;
                        var len1 = type1.Length;
                        var ctype1 = GetVectorType(type1);
                        var ctype2 = GetVectorType(type2);
                        var crt = GetVectorType(sh.Type);
                        var local1 = GetVectorTempVariable(ctype1, sh.Value1.Value, 0);
                        var local2 = GetVectorTempVariable(ctype2, sh.Value2.Value, 0);
                        if (sh.Mask.Value is ZeroConstant)
                        {
                            for (var c = 0; c < len1; c++)
                            {
                                EmitZeroValue(type1.ElementType);
                            }
                        }
                        else if (sh.Mask.Value is VectorConstant vc)
                        {
                            foreach (var c in vc.Constants)
                            {
                                var index = c.Value.GetInt32Value(function.IRModule);
                                var loc = index >= len1 ? local2 : local1;
                                var loci = index >= len1 ? index - len1 : index;
                                var typ = index >= len1 ? ctype2 : ctype1;
                                Emit(il.Create(OpCodes.Ldloc, loc));
                                Emit(il.Create(OpCodes.Ldfld, typ.ElementFields[loci]));
                            }
                        }
                        else
                        {
                            throw new NotSupportedException("Cannot shuffle with mask: " + sh.Mask);
                        }
                        Emit(il.Create(OpCodes.Newobj, crt.Ctor));
                    }
                    break;
                case IR.SitofpInstruction sitofp:
                    EmitTypedValue(sitofp.Value);
                    switch (sitofp.Type)
                    {
                        case Types.FloatType fltt:
                            switch (fltt.Bits)
                            {
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_R4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_R8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot sitofp {sitofp.Type}");
                    }
                    break;
                case IR.SremInstruction srem:
                    if (srem.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Rem, srem.Op1, srem.Op2, (Types.VectorType)srem.Type);
                    }
                    else
                    {
                        EmitValue(srem.Op1, srem.Type);
                        EmitValue(srem.Op2, srem.Type);
                        Emit(il.Create(OpCodes.Rem));
                    }
                    break;
                case IR.StoreInstruction store:
                    EmitStore(store);
                    break;
                case IR.SubInstruction sub:
                    if (sub.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Sub, sub.Op1, sub.Op2, (Types.VectorType)sub.Type);
                    }
                    else
                    {
                        EmitValue(sub.Op1, sub.Type);
                        EmitValue(sub.Op2, sub.Type);
                        Emit(il.Create(OpCodes.Sub));
                    }
                    break;
                case IR.SwitchInstruction sw:
                    EmitSwitch(sw, block, nextBlock, context);
                    break;
                case IR.TruncInstruction trunc:
                    EmitTypedValue(trunc.Value);
                    switch (trunc.Type)
                    {
                        case Types.IntegerType intt: {
                                int nbits = 0;
                                switch (Compilation.RoundUpIntBits (intt.Bits)) {
                                    case 8:
                                        nbits = 8;
                                        Emit (il.Create (OpCodes.Conv_U1));
                                        break;
                                    case 16:
                                        nbits = 16;
                                        Emit (il.Create (OpCodes.Conv_U2));
                                        break;
                                    case 32:
                                        nbits = 32;
                                        Emit (il.Create (OpCodes.Conv_U4));
                                        break;
                                    default:
                                        nbits = 64;
                                        Emit (il.Create (OpCodes.Conv_U8));
                                        break;
                                }
                                if (intt.Bits < nbits) {
                                    EmitValue (IntegerConstant.MaskBits (intt.Bits), IntegerType.WithBits (nbits));
                                    Emit (il.Create (OpCodes.And));
                                }
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot trunc {trunc.Type}");
                    }
                    break;
                case IR.UdivInstruction udiv:
                    if (udiv.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Div_Un, udiv.Op1, udiv.Op2, (Types.VectorType)udiv.Type);
                    }
                    else
                    {
                        EmitValue(udiv.Op1, udiv.Type);
                        EmitValue(udiv.Op2, udiv.Type);
                        Emit(il.Create(OpCodes.Div_Un));
                    }
                    break;
                case IR.UitofpInstruction uitofp:
                    EmitTypedValue(uitofp.Value);
                    switch (uitofp.Type)
                    {
                        case Types.FloatType fltt:
                            switch (fltt.Bits)
                            {
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_R4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_R8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot uitofp {uitofp.Type}");
                    }
                    break;
                case IR.UnconditionalBrInstruction br: {
                        var destination = GetLabel (br.Destination, block, context);
                        if (context.IsExceptionHandler && !context.IsProtecting (br.Destination)) {
                            Emit (il.Create (OpCodes.Leave, destination));
                        }
                        else {
                            Emit (il.Create (OpCodes.Br, destination));
                        }
                    }
                    break;
                case IR.UnreachableInstruction unreach:
                    if (compilation.Options.SafeMemory) {
                        EmitUnregisterAllocas ();
                    }
                    if (!function.IRDefinition.ReturnType.StructurallyEquals (VoidType.Void)) {
                        EmitZeroValue (function.IRDefinition.ReturnType);
                    }
                    Emit (OpCodes.Ret);
                    break;
                case IR.UremInstruction urem:
                    if (urem.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Rem_Un, urem.Op1, urem.Op2, (Types.VectorType)urem.Type);
                    }
                    else
                    {
                        EmitValue(urem.Op1, urem.Type);
                        EmitValue(urem.Op2, urem.Type);
                        Emit(il.Create(OpCodes.Rem_Un));
                    }
                    break;
                case IR.XorInstruction xor:
                    if (xor.Type is Types.VectorType)
                    {
                        EmitVectorOp(OpCodes.Xor, xor.Op1, xor.Op2, (Types.VectorType)xor.Type);
                    }
                    else
                    {
                        EmitValue(xor.Op1, xor.Type);
                        EmitValue(xor.Op2, xor.Type);
                        Emit(il.Create(OpCodes.Xor));
                    }
                    break;
                case IR.ZextInstruction zext:
                    EmitTypedValue(zext.Value);
                    switch (zext.Type)
                    {
                        case Types.IntegerType intt:
                            switch (Compilation.RoundUpIntBits (intt.Bits)) {
                                case 8:
                                    Emit(il.Create(OpCodes.Conv_U1));
                                    break;
                                case 16:
                                    Emit(il.Create(OpCodes.Conv_U2));
                                    break;
                                case 32:
                                    Emit(il.Create(OpCodes.Conv_U4));
                                    break;
                                default:
                                    Emit(il.Create(OpCodes.Conv_U8));
                                    break;
                            }
                            break;
                        default:
                            throw new NotSupportedException($"Cannot zext {zext.Type}");
                    }
                    break;
                default:
                    throw new NotImplementedException($"{instruction.GetType().Name}: {instruction}");
            }
        }

        void EmitSext (LType resultType, TypedValue inputValue)
        {
            switch (resultType) {
                case Types.IntegerType intt: {
                        var toBits = intt.Bits;
                        var toUpBits = Compilation.RoundUpIntBits (toBits);
                        EmitTypedValue (inputValue);
                        if (inputValue.Type is Types.IntegerType sintt) {
                            var fromBits = sintt.Bits;
                            var fromUpBits = Compilation.RoundUpIntBits (fromBits);
                            if (fromBits == fromUpBits && toBits == toUpBits && fromBits == toBits)
                                return;
                            if (fromBits == 1) {
                                Emit (il.Create (OpCodes.Ldc_I4_M1));
                                Emit (il.Create (OpCodes.Mul));
                            }
                            else if (fromBits == 8) {
                                Emit (il.Create (OpCodes.Conv_I1));
                            }
                            else if (fromBits != fromUpBits) {
                                compilation.ErrorMessage (module.SourceFilename, $"Cannot sign extend from {fromBits}-bit to {toBits}-bit integers");
                            }

                            switch (toUpBits) {
                                case 8:
                                    Emit (il.Create (OpCodes.Conv_I1));
                                    break;
                                case 16:
                                    Emit (il.Create (OpCodes.Conv_I2));
                                    break;
                                case 32:
                                    Emit (il.Create (OpCodes.Conv_I4));
                                    break;
                                default:
                                    Emit (il.Create (OpCodes.Conv_I8));
                                    break;
                            }
                        }
                        else {
                            compilation.ErrorMessage (module.SourceFilename, $"Cannot sign extend from type {inputValue.Type} to {toBits}-bit integers");
                        }
                    }
                    break;
                case VectorType vt when vt.ElementType is Types.IntegerType vintt:
                    switch (vintt.Bits) {
                        case 1:
                        case 8:
                            EmitVectorUnop (OpCodes.Conv_I1, inputValue, vt);
                            break;
                        case 16:
                            EmitVectorUnop (OpCodes.Conv_I2, inputValue, vt);
                            break;
                        case 32:
                            EmitVectorUnop (OpCodes.Conv_I4, inputValue, vt);
                            break;
                        default:
                            EmitVectorUnop (OpCodes.Conv_I8, inputValue, vt);
                            break;
                    }
                    break;
                default:
                    throw new NotSupportedException ($"Cannot sext {resultType}");
            }
        }

        private void EmitAllocaSize (AllocaInstruction alloca)
        {
            var byteSize = alloca.Type.GetByteSize (function.IRModule);
            if (byteSize > 1) {
                Emit (il.Create (OpCodes.Ldc_I4, (int)byteSize));
            }
            if (alloca.NumElements != null) {
                EmitTypedValue (alloca.NumElements);
                if (byteSize > 1) {
                    Emit (il.Create (OpCodes.Mul));
                }
            }
            else {
                if (byteSize == 1) {
                    Emit (il.Create (OpCodes.Ldc_I4_1));
                }
            }
            Emit (il.Create (OpCodes.Conv_U));
        }

        void EmitUnregisterAllocas ()
        {
            foreach (var kv in allocas) {
                Emit (il.Create (OpCodes.Ldloc, kv.Value));
                Emit (il.Create (OpCodes.Call, compilation.GetSystemMethod ("@_unregister_memory")));
            }
        }

        void EmitVSelect(SelectInstruction sel, VectorType type)
        {
            EmitValue(sel.Condition, sel.Type);
            EmitTypedValue(sel.Value1);
            EmitTypedValue(sel.Value2);
            var v = GetVectorType((VectorType)sel.Value1.Type);
            Emit(il.Create(OpCodes.Call, v.Select));
        }

        private void EmitIcmp(IcmpInstruction icmp)
        {
            bool unsigned = true;
            switch (icmp.Condition) {
                case IR.IcmpCondition.SignedGreaterThan:
                case IR.IcmpCondition.SignedGreaterThanOrEqual:
                case IR.IcmpCondition.SignedLessThan:
                case IR.IcmpCondition.SignedLessThanOrEqual:
                    unsigned = false;
                    break;
            }

            EmitValue (icmp.Op1, icmp.Type, unsigned: unsigned);
            EmitValue (icmp.Op2, icmp.Type, unsigned: unsigned);
            if (icmp.Type is VectorType v)
            {
                EmitVIcmp(icmp, v);
                return;
            }
            switch (icmp.Condition)
            {
                case IR.IcmpCondition.Equal:
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.IcmpCondition.NotEqual:
                    Emit(il.Create(OpCodes.Ceq));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.IcmpCondition.UnsignedGreaterThan:
                    Emit(il.Create(OpCodes.Cgt_Un));
                    break;
                case IR.IcmpCondition.UnsignedGreaterThanOrEqual:
                    Emit(il.Create(OpCodes.Clt_Un));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.IcmpCondition.UnsignedLessThan:
                    Emit(il.Create(OpCodes.Clt_Un));
                    break;
                case IR.IcmpCondition.UnsignedLessThanOrEqual:
                    Emit(il.Create(OpCodes.Cgt_Un));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.IcmpCondition.SignedGreaterThan:
                    Emit(il.Create(OpCodes.Cgt));
                    break;
                case IR.IcmpCondition.SignedGreaterThanOrEqual:
                    Emit(il.Create(OpCodes.Clt));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.IcmpCondition.SignedLessThan:
                    Emit(il.Create(OpCodes.Clt));
                    break;
                case IR.IcmpCondition.SignedLessThanOrEqual:
                    Emit(il.Create(OpCodes.Cgt));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
            }
        }

        private void EmitFcmp(FcmpInstruction fcmp)
        {
            switch (fcmp.Condition)
            {
                case IR.FcmpCondition.Ordered:
                    if (((FloatType)fcmp.Type).Bits == 32)
                    {
                        EmitValue(fcmp.Op1, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysSingleIsNaN));
                        EmitValue(fcmp.Op2, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysSingleIsNaN));
                    }
                    else
                    {
                        EmitValue(fcmp.Op1, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysDoubleIsNaN));
                        EmitValue(fcmp.Op2, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysDoubleIsNaN));
                    }
                    Emit(il.Create(OpCodes.Or));
                    Emit(il.Create(OpCodes.Ldc_I4_0));
                    Emit(il.Create(OpCodes.Ceq));
                    break;
                case IR.FcmpCondition.Unordered:
                    if (((FloatType)fcmp.Type).Bits == 32)
                    {
                        EmitValue(fcmp.Op1, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysSingleIsNaN));
                        EmitValue(fcmp.Op2, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysSingleIsNaN));
                    }
                    else
                    {
                        EmitValue(fcmp.Op1, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysDoubleIsNaN));
                        EmitValue(fcmp.Op2, fcmp.Type);
                        Emit(il.Create(OpCodes.Call, compilation.sysDoubleIsNaN));
                    }
                    Emit(il.Create(OpCodes.Or));
                    break;
                default:
                    EmitValue(fcmp.Op1, fcmp.Type);
                    EmitValue(fcmp.Op2, fcmp.Type);
                    if (fcmp.Type is VectorType v)
                    {
                        EmitVFcmp(fcmp, v);
                        return;
                    }
                    switch (fcmp.Condition)
                    {
                        case IR.FcmpCondition.True:
                            Emit(il.Create(OpCodes.Pop));
                            Emit(il.Create(OpCodes.Pop));
                            Emit(il.Create(OpCodes.Ldc_I4_1));
                            break;
                        case IR.FcmpCondition.False:
                            Emit(il.Create(OpCodes.Pop));
                            Emit(il.Create(OpCodes.Pop));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            break;
                        case IR.FcmpCondition.OrderedEqual:
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.OrderedGreaterThan:
                            Emit(il.Create(OpCodes.Cgt));
                            break;
                        case IR.FcmpCondition.OrderedGreaterThanOrEqual:
                            Emit(il.Create(OpCodes.Clt));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.OrderedLessThan:
                            Emit(il.Create(OpCodes.Clt));
                            break;
                        case IR.FcmpCondition.OrderedLessThanOrEqual:
                            Emit(il.Create(OpCodes.Cgt));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.UnorderedEqual:
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.UnorderedNotEqual:
                            Emit(il.Create(OpCodes.Ceq));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.UnorderedGreaterThan:
                            Emit(il.Create(OpCodes.Cgt_Un));
                            break;
                        case IR.FcmpCondition.UnorderedGreaterThanOrEqual:
                            Emit(il.Create(OpCodes.Clt_Un));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        case IR.FcmpCondition.UnorderedLessThan:
                            Emit(il.Create(OpCodes.Clt_Un));
                            break;
                        case IR.FcmpCondition.UnorderedLessThanOrEqual:
                            Emit(il.Create(OpCodes.Cgt_Un));
                            Emit(il.Create(OpCodes.Ldc_I4_0));
                            Emit(il.Create(OpCodes.Ceq));
                            break;
                        default:
                            throw new NotSupportedException("fcmp condition " + fcmp.Condition);
                    }
                    break;
            }
        }

        void EmitVIcmp(IcmpInstruction icmp, VectorType type)
        {
            var v = GetVectorType(type);

            switch (icmp.Condition)
            {
                case IcmpCondition.NotEqual:
                    Emit(il.Create(OpCodes.Call, v.IcmpNotEqual));
                    break;
                case IcmpCondition.SignedGreaterThan:
                    Emit(il.Create(OpCodes.Call, v.IcmpSignedGreaterThan));
                    break;
                case IcmpCondition.SignedLessThan:
                    Emit(il.Create(OpCodes.Call, v.IcmpSignedLessThan));
                    break;
                default:
                    throw new NotSupportedException($"Vector icmp {icmp.Condition}");
            }
        }

        void EmitVFcmp(FcmpInstruction fcmp, VectorType type)
        {
            var v = GetVectorType(type);

            switch (fcmp.Condition)
            {
                case FcmpCondition.OrderedLessThan:
                    Emit(il.Create(OpCodes.Call, v.FcmpOrderedLessThan));
                    break;
                default:
                    throw new NotSupportedException($"Vector fcmp {fcmp.Condition}");
            }
        }

        protected override void EmitLocalValue(IR.LocalValue local, LType resultType, bool? unsigned)
        {
            TypeReference vt = null;
            if (locals.TryGetValue(local.Symbol, out var vd))
            {
                vt = vd.VariableType;
                Emit(il.Create(OpCodes.Ldloc, vd));
            }
            else
            {
                if (function.ParamSyms.TryGetValue(local.Symbol, out var pd))
                {
                    vt = pd.ParameterType;
                    Emit (il.Create(OpCodes.Ldarg, pd));
                }
                else
                {
                    var a = function.IRDefinition.GetAssignment(local);
                    EmitInstruction(a.Result, a.Instruction, null, null, mainContext, unsigned);
                }
            }

            var crt = compilation.GetClrType (resultType, module, unsigned: unsigned);
            if (vt != null && crt.FullName != vt.FullName) {
                if (resultType is Types.IntegerType intt) {
                    if (unsigned.HasValue) {
                        switch (Compilation.RoundUpIntBits (intt.Bits)) {
                            case 8:
                                Emit (unsigned.Value ? OpCodes.Conv_U1 : OpCodes.Conv_I1);
                                break;
                            case 16:
                                Emit (unsigned.Value ? OpCodes.Conv_U2 : OpCodes.Conv_I2);
                                break;
                            case 32:
                                Emit (unsigned.Value ? OpCodes.Conv_U4 : OpCodes.Conv_I4);
                                break;
                            case 64:
                                Emit (unsigned.Value ? OpCodes.Conv_U8 : OpCodes.Conv_I8);
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot emit integer type `{crt}` for local type `{vd.VariableType}`");
                        }
                    }
                    else {
                        switch (Compilation.RoundUpIntBits (intt.Bits)) {
                            case 8:
                                Emit (OpCodes.Conv_U1);
                                break;
                            case 16:
                                Emit (OpCodes.Conv_I2);
                                break;
                            case 32:
                                Emit (OpCodes.Conv_I4);
                                break;
                            case 64:
                                Emit (OpCodes.Conv_I8);
                                break;
                            default:
                                throw new NotSupportedException ($"Cannot emit integer type `{crt}` for local type `{vd.VariableType}`");
                        }
                    }
                }
                else if (resultType is Types.PointerType) {
                    // OK
                }
                else {
                    throw new NotSupportedException ($"Cannot emit type `{crt}` for local type `{vd.VariableType}`");
                }
            }
        }

        void EmitStore(IR.StoreInstruction store)
        {
            // Shortcut Store Field
            if (store.Pointer.Value is IR.LocalValue pointerLocal
                && ShouldInline(pointerLocal.Symbol))
            {

                var pointerInst = function.IRDefinition.FindAssignment(pointerLocal)?.Instruction;
                if (pointerInst is IR.GetElementPointerInstruction gep
                    && gep.Indices.Length == 2
                    && gep.Indices[0].Value is IR.Constant ptrConst && ptrConst.Int32Value == 0
                    && gep.Indices[1].Value is IR.Constant indexConst
                    && gep.Pointer.Type is Types.PointerType gepPointerType
                    && gepPointerType.ElementType.Resolve(function.IRModule) is LiteralStructureType structType)
                {

                    var td = compilation.GetClrType(gepPointerType.ElementType, module: module).Resolve();
                    var fieldIndex = indexConst.Int32Value;
                    if (fieldIndex < 0 || fieldIndex >= td.Fields.Count)
                        throw new IndexOutOfRangeException ($"Field #{fieldIndex} does not exist in {td.FullName} ({store})");
                    var field = td.Fields[fieldIndex];

                    EmitTypedValue(gep.Pointer);
                    EmitVerifyWritePointer ();
                    EmitTypedValue(store.Value);
                    Emit(il.Create(OpCodes.Stfld, field));
                    return;
                }
            }

            var isfptr = store.Pointer.Type is Types.PointerType && ((Types.PointerType)store.Pointer.Type).ElementType is FunctionType;

            EmitTypedValue (store.Pointer);
            if (!isfptr) {
                EmitVerifyWritePointer ();
            }
            EmitTypedValue (store.Value);
            EmitStind (store.Value.Type);
        }

        void EmitLoad(IR.LoadInstruction load, bool? unsigned)
        {
            // Shortcut Load Field
            if (load.Pointer.Value is IR.LocalValue pointerLocal
                && (ShouldInline(pointerLocal.Symbol) || HasLocal(pointerLocal.Symbol)))
            {

                var pointerInst = function.IRDefinition.FindAssignment(pointerLocal)?.Instruction;
                if (pointerInst is IR.GetElementPointerInstruction gep
                    && gep.Indices.Length == 2
                    && gep.Indices[0].Value is IR.Constant ptrConst && ptrConst.Int32Value == 0
                    && gep.Indices[1].Value is IR.Constant indexConst
                    && gep.Pointer.Type is Types.PointerType gepPointerType
                    && gepPointerType.ElementType.Resolve(function.IRModule) is LiteralStructureType structType)
                {
                    //Console.WriteLine (function.Symbol + " SHORTCUT " + load);
                    var td = compilation.GetClrType(gepPointerType.ElementType, module: module).Resolve();
                    var fieldIndex = indexConst.Int32Value;
                    if (fieldIndex < 0 || fieldIndex >= td.Fields.Count)
                        throw new IndexOutOfRangeException ($"Field #{fieldIndex} does not exist in {td.FullName} ({load})");
                    var field = td.Fields[fieldIndex];

                    EmitTypedValue(gep.Pointer);
                    EmitVerifyReadPointer ();
                    Emit(il.Create(OpCodes.Ldfld, field));
                    return;
                }
            }

            var isfptr = load.Type is Types.PointerType && ((Types.PointerType)load.Type).ElementType is FunctionType;

            EmitTypedValue(load.Pointer);

            if (!isfptr) {
                EmitVerifyReadPointer ();
            }

            var et = compilation.GetClrType(load.Type, module: module);
            if (load.Type is IntegerType intt)
            {
                switch (Compilation.RoundUpIntBits (intt.Bits))
                {
                    case 8:
                        if (unsigned.HasValue && !unsigned.Value) {
                            Emit (il.Create (OpCodes.Ldind_I1));
                        }
                        else {
                            Emit (il.Create (OpCodes.Ldind_U1));
                        }
                        break;
                    case 16:
                        if (unsigned.HasValue && unsigned.Value) {
                            Emit (il.Create (OpCodes.Ldind_U2));
                        }
                        else {
                            Emit (il.Create (OpCodes.Ldind_I2));
                        }
                        break;
                    case 32:
                        if (unsigned.HasValue && unsigned.Value) {
                            Emit (il.Create (OpCodes.Ldind_U4));
                        }
                        else {
                            Emit (il.Create (OpCodes.Ldind_I4));
                        }
                        break;
                    case 64:
                        if (unsigned.HasValue && unsigned.Value) {
                            Emit (il.Create (OpCodes.Ldind_I8));
                        }
                        else {
                            Emit (il.Create (OpCodes.Ldind_I8));
                        }
                        break;
                    default:
                        Emit(il.Create(OpCodes.Ldobj, et));
                        break;
                }
            }
            else if (load.Type is FloatType fltt)
            {
                switch (fltt.Bits)
                {
                    case 32:
                        Emit(il.Create(OpCodes.Ldind_R4));
                        break;
                    default:
                        Emit(il.Create(OpCodes.Ldind_R8));
                        break;
                }
            }
            else if (load.Type is Types.PointerType pt)
            {
                Emit(il.Create(OpCodes.Ldind_I));
            }
            else
            {
                Emit(il.Create(OpCodes.Ldobj, et));
            }
        }

        void EmitSwitch(IR.SwitchInstruction sw, IR.Block block, IR.Block nextBlock, BlocksContext context)
        {
            var rem = new List<IR.SwitchCase>(sw.Cases.OrderBy(x => x.Value.Constant.Int32Value));

            while (rem.Count > 0)
            {

                var offset = rem[0].Value.Constant.Int32Value;

                int nextValue = offset + 1;
                int endIndex = 1;
                while (endIndex < rem.Count && rem[endIndex].Value.Constant.Int32Value == nextValue)
                {
                    endIndex++;
                    nextValue++;
                }

                EmitTypedValue(sw.Value);
                if (offset != 0)
                {
                    EmitValue(rem[0].Value.Constant, rem[0].Value.Type);
                }

                if (endIndex > 1)
                {
                    var labels =
                        rem.Take(endIndex)
                        .Select(x => GetLabel(x.Label, block, context))
                        .ToArray();
                    if (offset != 0)
                    {
                        Emit(il.Create(OpCodes.Sub));
                    }
                    if (!(sw.Value.Type is IntegerType intt && intt.Bits == 32)) {
                        Emit (il.Create (OpCodes.Conv_U4));
                    }
                    Emit(il.Create(OpCodes.Switch, labels));
                    rem.RemoveRange(0, endIndex);
                }
                else
                {
                    var c = rem[0];
                    if (offset == 0)
                    {
                        EmitValue(new IR.IntegerConstant(0), c.Value.Type);
                    }
                    Emit(il.Create(OpCodes.Beq, GetLabel(c.Label, block, context)));
                    rem.RemoveAt(0);
                }
            }

            //if (nextBlock.Symbol != sw.DefaultLabel.Symbol)
            Emit(il.Create(OpCodes.Br, GetLabel(sw.DefaultLabel, block, context)));
        }

        void EmitVectorUnop(OpCode op, IR.TypedValue op1, Types.VectorType type)
        {
            EmitTypedValue(op1);
            var v = GetVectorType(type);
            switch (op.Code)
            {
                case Code.Conv_I1:
                    Emit(il.Create(OpCodes.Call, v.ToInt8));
                    break;
                case Code.Conv_I2:
                    Emit(il.Create(OpCodes.Call, v.ToInt16));
                    break;
                case Code.Conv_I4:
                    Emit(il.Create(OpCodes.Call, v.ToInt32));
                    break;
                case Code.Conv_I8:
                    Emit(il.Create(OpCodes.Call, v.ToInt64));
                    break;
                default:
                    throw new NotSupportedException($"Cannot perform vector unop {op.Code} {type}");
            }
        }

        void EmitVectorOp(OpCode op, IR.Value op1, IR.Value op2, Types.VectorType type)
        {
            EmitValue(op1, type);
            EmitValue(op2, type);
            var v = GetVectorType(type);
            switch (op.Code)
            {
                case Code.Add:
                    Emit(il.Create(OpCodes.Call, v.Add));
                    break;
                case Code.Sub:
                    Emit(il.Create(OpCodes.Call, v.Subtract));
                    break;
                case Code.Mul:
                    Emit(il.Create(OpCodes.Call, v.Multiply));
                    break;
                case Code.Div:
                    Emit(il.Create(OpCodes.Call, v.Divide));
                    break;
                default:
                    throw new NotSupportedException($"Cannot perform vector op {op.Code} {type}");
            }
        }

        void EmitVectorFunc(IR.Value value, Types.VectorType type, MethodReference func)
        {
            var temp = GetVectorTempVariable(GetVectorType(type), value, 0);
            EmitValue(value, type);
            Emit(il.Create(OpCodes.Stloc, temp));
            Emit(il.Create(OpCodes.Ldloc, temp));
        }

        void EmitBrtrue(IR.Value condition, LType conditionType, CecilInstruction trueTarget)
        {
            if (condition is IR.LocalValue local && ShouldInline(local.Symbol))
            {
                var a = function.IRDefinition.FindAssignment(local);
                if (a?.Instruction is IR.IcmpInstruction icmp && !(icmp.Type is VectorType))
                {
                    var op = OpCodes.Brtrue;
                    var unsigned = true;
                    switch (icmp.Condition)
                    {
                        case IR.IcmpCondition.Equal:
                            op = OpCodes.Beq;
                            break;
                        case IR.IcmpCondition.NotEqual:
                            op = OpCodes.Bne_Un;
                            break;
                        case IR.IcmpCondition.UnsignedGreaterThan:
                            op = OpCodes.Bgt_Un;
                            break;
                        case IR.IcmpCondition.UnsignedGreaterThanOrEqual:
                            op = OpCodes.Bge_Un;
                            break;
                        case IR.IcmpCondition.UnsignedLessThan:
                            op = OpCodes.Blt_Un;
                            break;
                        case IR.IcmpCondition.UnsignedLessThanOrEqual:
                            op = OpCodes.Ble_Un;
                            break;
                        case IR.IcmpCondition.SignedGreaterThan:
                            unsigned = false;
                            op = OpCodes.Bgt;
                            break;
                        case IR.IcmpCondition.SignedGreaterThanOrEqual:
                            unsigned = false;
                            op = OpCodes.Bge;
                            break;
                        case IR.IcmpCondition.SignedLessThan:
                            unsigned = false;
                            op = OpCodes.Blt;
                            break;
                        case IR.IcmpCondition.SignedLessThanOrEqual:
                            unsigned = false;
                            op = OpCodes.Ble;
                            break;
                    }
                    EmitValue(icmp.Op1, icmp.Type, unsigned: unsigned);
                    EmitValue(icmp.Op2, icmp.Type, unsigned: unsigned);
                    Emit(il.Create(op, trueTarget));
                    return;
                }
            }

            EmitValue(condition, conditionType);
            Emit(il.Create(OpCodes.Brtrue, trueTarget));
        }

        void EmitInvoke (LocalSymbol assignedSymbol, IR.InvokeInstruction invoke, IR.Block block, BlocksContext context)
        {
            var tryPrev = prev;
            TypeReference returnType;

            if ((invoke.Pointer is IR.GlobalValue gv)
                && (compilation.TryGetFunction (module, gv.Symbol, out var m))) {

                var ps = m.ILDefinition.Parameters;
                var nps = ps.Count;
                var hasVarArgs =
                    nps > 0
                    && ps[nps - 1].ParameterType.IsArray
                    && ps[nps - 1].ParameterType.GetElementType ().FullName == "System.Object";
                if (hasVarArgs)
                    nps--;
                if (invoke.Arguments.Length < nps) {
                    throw new InvalidOperationException ($"Too few arguments to {function.IRDefinition.Symbol}");
                }

                for (var i = 0; i < nps; i++) {
                    var a = invoke.Arguments[i];
                    EmitValue (a.Value, a.Type);
                }
                if (hasVarArgs) {
                    EmitVarArgs (invoke.Arguments, nps);
                }

                Emit (il.Create (OpCodes.Call, m.ILDefinition));

                returnType = m.ILDefinition.ReturnType;

            }
            else if (invoke.Pointer is IR.LocalValue lv) {
                LType ltype;
                if (function.ParamSyms.TryGetValue (lv.Symbol, out var p)) {
                    ltype = function.IRDefinition.Parameters.First (x => x.Symbol == lv.Symbol).ParameterType;
                }
                else {
                    var lva = function.IRDefinition.GetAssignment (lv);
                    ltype = lva.Instruction.ResultType (function.IRModule);
                }
                var ft = (FunctionType)((Types.PointerType)ltype).ElementType;
                var ps = ft.ParameterTypes;
                var nps = ps.Length;
                var hasVarArgs = nps > 0 && (ps[nps - 1] is VarArgsType);
                if (hasVarArgs)
                    nps--;
                if (invoke.Arguments.Length < nps) {
                    throw new InvalidOperationException ($"Too few arguments to {function.IRDefinition.Symbol}");
                }
                for (var i = 0; i < nps; i++) {
                    var a = invoke.Arguments[i];
                    EmitValue (a.Value, a.Type);
                }
                if (hasVarArgs) {
                    EmitVarArgs (invoke.Arguments, nps);
                }
                EmitValue (lv, ltype);

                var site = CreateCallSite (ft);
                EmitCalli (site);

                returnType = site.ReturnType;
            }
            else {
                throw new Exception ($"Cannot invoke {invoke.Pointer}");
            }

            // LLVM allows for return type mismatches with void
            if (returnType.FullName == "System.Void" && !(invoke.ReturnType is VoidType)) {
                EmitZeroValue (invoke.ReturnType);
            }
            else if (returnType.FullName != "System.Void" && (invoke.ReturnType is VoidType)) {
                Emit (OpCodes.Pop);
            }

            // Assign it now
            if (locals.TryGetValue (assignedSymbol, out var vd)) {
                Emit (il.Create (OpCodes.Stloc, vd));
            }

            Emit (il.Create (OpCodes.Leave, context.BlockFirstInstr[invoke.NormalLabel.Symbol]));

            var tryLast = prev;

            var catchContext = new BlocksContext (context, Enumerable.Empty<Symbol> ());

            var pad = landingPads[invoke.ExceptionLabel.Symbol];

            EmitBlocks (pad.Blocks, catchContext);            
            
            var catchLast = prev;
            ehs.Add ((tryPrev.Next, tryLast, catchLast));
        }

        class LandingPad
        {
            public Block Block;
            public Assignment Assignment;
            public List<IR.Block> Blocks;
        }

        readonly List<(CecilInstruction TryStart, CecilInstruction TryLast, CecilInstruction CatchLast)> ehs =
            new List<(CecilInstruction TryStart, CecilInstruction TryLast, CecilInstruction CatchLast)> ();

        readonly Lazy<TypeDefinition> UMulOverflowResultTypeI64;

        TypeDefinition GetUMulOverflowResultType (LType valueType)
        {
            var irType = new LiteralStructureType (false, new LType[] { valueType, Types.IntegerType.I1 });
            return compilation.GetClrType (irType, module: module).Resolve ();
        }

        void EmitCall(IR.CallInstruction call, Block fromBlock)
        {
            if (call.Pointer is IR.GlobalValue gv) {
                switch (gv.Symbol.Text) {
                    case "@llvm.lifetime.start.p0i8":
                    case "@llvm.lifetime.end.p0i8":
                    case "@llvm.dbg.declare":
                        return;
                    case "@llvm.dbg.value":
                        // call void @llvm.dbg.value(metadata %struct._parser_t* %3, metadata !1020, metadata !DIExpression(DW_OP_deref)), !dbg !1140
                        // !DILocalVariable (name: "parser", scope: !1013, file: !3, line: 871, type: !790)
                        if (call.Arguments.Length >= 2
                            && call.Arguments[0].Value.ReferencedLocals.Count () > 0
                            && call.Arguments[1].Value is MetaValue meta
                            && module.Metadata.TryGetValue(meta.Symbol, out var o)
                            && o is SymbolTable<object> metadata) {
                            var local = call.Arguments[0].Value.ReferencedLocals.First ();
                            AddLocalDebugInfo (fromBlock, local, metadata);
                            if (metadata.TryGetValue (Symbol.Type, out o)
                                && o is MetaSymbol ts
                                && module.Metadata.TryGetValue (ts, out o)
                                && o is SymbolTable<object> tdata
                                ) {
                                compilation.AddDebugInfoToStruct (call.Arguments[0].Type, module, tdata);
                            }
                        }
                        return;
                    case "@llvm.ceil.f64":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        Emit (il.Create (OpCodes.Call, compilation.sysMathCeilD));
                        return;
                    case "@llvm.ceil.v2f64" when call.Arguments[0].Type is VectorType ceilVt:
                        EmitVectorFunc (call.Arguments[0].Value, ceilVt, compilation.sysMathCeilD);
                        return;
                    case "@llvm.fabs.f64":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        Emit (il.Create (OpCodes.Call, compilation.sysMathAbsD));
                        return;
                    case "@llvm.sqrt.f64":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        Emit (il.Create (OpCodes.Call, compilation.sysMathSqrtD));
                        return;
                    case "@llvm.pow.f64":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                        Emit (il.Create (OpCodes.Call, compilation.sysMathPowD));
                        return;
                    case "@llvm.objectsize.i32.p0i8" when call.Arguments.Length >= 3: {
                            var min = 0;
                            if (call.Arguments[1].Value is IR.Constant osizeConst) {
                                min = osizeConst.Int32Value;
                            }
                            if (min == 0) {
                                Emit (il.Create (OpCodes.Ldc_I4, -1));
                            }
                            else {
                                Emit (il.Create (OpCodes.Ldc_I4, 0));
                            }
                        }
                        return;
                    case "@llvm.objectsize.i64.p0i8" when call.Arguments.Length >= 3: {
                            var min = 0;
                            if (call.Arguments[1].Value is IR.Constant osizeConst) {
                                min = osizeConst.Int32Value;
                            }
                            if (min == 0) {
                                Emit (il.Create (OpCodes.Ldc_I8, -1L));
                            }
                            else {
                                Emit (il.Create (OpCodes.Ldc_I8, 0L));
                            }
                        }
                        return;
                    // declare void @llvm.memset.p0i8.i32(i8* <dest>, i8 <val>,
                    //                                    i32<len>, i1<isvolatile>)
                    case "@llvm.memset.p0i8.i32" when call.Arguments.Length >= 3:
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                        EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                        Emit (il.Create (OpCodes.Initblk));
                        return;
                    // declare void @llvm.memset.p0i8.i64 (i8 * < dest >, i8<val>,
                    //                                     i64<len>, i1<isvolatile>)
                    case "@llvm.memset.p0i8.i64" when call.Arguments.Length >= 3:
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                        EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                        Emit (il.Create (OpCodes.Conv_U4));
                        Emit (il.Create (OpCodes.Initblk));
                        return;
                    // declare void @llvm.memcpy.p0i8.p0i8.i32(i8* <dest>, i8* <src>,
                    //                                         i32 <len>, i1 <isvolatile>)
                    case "@llvm.memcpy.p0i8.p0i8.i32" when call.Arguments.Length >= 3:
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                        EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                        Emit (il.Create (OpCodes.Cpblk));
                        return;
                    // declare void @llvm.memcpy.p0i8.p0i8.i64(i8* <dest>, i8* <src>,
                    //                                         i64 <len>, i1 <isvolatile>)
                    case "@llvm.memcpy.p0i8.p0i8.i64" when call.Arguments.Length >= 3:
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                        EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                        Emit (il.Create (OpCodes.Conv_U4));
                        Emit (il.Create (OpCodes.Cpblk));
                        return;
                    // declare void @llvm.memmove.p0i8.p0i8.i64(i8* <dest>, i8* <src>,
                    //                                         i64 <len>, i1 <isvolatile>)
                    //case "@llvm.memmove.p0i8.p0i8.i64" when call.Arguments.Length >= 3:
                    //    EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                    //    EmitValue (call.Arguments[1].Value, call.Arguments[1].Type);
                    //    EmitValue (call.Arguments[2].Value, call.Arguments[2].Type);
                    //    Emit (il.Create (OpCodes.Conv_U4));
                    //    Emit (il.Create (OpCodes.Cpblk));
                    //    return;
                    case "@llvm.stacksave":
                        compilation.WarningMessage (module.SourceFilename, $"Stack save is not supported in `{MangledName.Demangle (function.Symbol)}`");
                        Emit (il.Create (OpCodes.Ldc_I4_0));
                        Emit (il.Create (OpCodes.Conv_U));
                        return;
                    case "@llvm.stackrestore":
                        compilation.WarningMessage (module.SourceFilename, $"Stack restore is not supported in `{MangledName.Demangle (function.Symbol)}`");
                        return;
                    case "@llvm.va_start":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        Emit (il.Create (OpCodes.Ldarg, function.IRDefinition.Parameters.Length - 1));
                        Emit (il.Create (OpCodes.Call, compilation.GetSystemMethod(gv.Symbol)));
                        return;
                    case "@llvm.va_end":
                        EmitValue (call.Arguments[0].Value, call.Arguments[0].Type);
                        Emit (il.Create (OpCodes.Call, compilation.GetSystemMethod (gv.Symbol)));
                        return;
                    case "@llvm.trap":
                        Emit (il.Create (OpCodes.Ldstr, "Trap"));
                        Emit (il.Create (OpCodes.Newobj, compilation.sysExceptionCtor));
                        Emit (il.Create (OpCodes.Throw));
                        return;
                    default:
                        if (compilation.TryGetFunction (module, gv.Symbol, out var m)) {

                            var ps = m.ILDefinition.Parameters;
                            var nps = ps.Count;
                            var hasVarArgs =
                                nps > 0
                                && ps[nps - 1].ParameterType.IsArray
                                && ps[nps - 1].ParameterType.GetElementType ().FullName == "System.Object";
                            if (hasVarArgs)
                                nps--;
                            if (call.Arguments.Length < nps) {
                                throw new InvalidOperationException ($"Too few arguments to {function.IRDefinition.Symbol}");
                            }

                            if (compilation.Options.Reentrant) {
                                EmitContext (function.ILDefinition.DeclaringType, m.ILDefinition.DeclaringType);
                            }

                            for (var i = 0; i < nps; i++) {
                                var a = call.Arguments[i];
                                EmitValue (a.Value, a.Type);
                            }
                            if (hasVarArgs) {
                                EmitVarArgs (call.Arguments, nps);
                            }

                            Emit (il.Create (OpCodes.Call, m.ILDefinition));

                            return;
                        }
                        break;
                }
            }
            else if (call.Pointer is IR.LocalValue lv) {
                LType ltype;
                if (function.ParamSyms.TryGetValue (lv.Symbol, out var p)) {
                    ltype = function.IRDefinition.Parameters.First (x => x.Symbol == lv.Symbol).ParameterType;
                }
                else {
                    var lva = function.IRDefinition.GetAssignment (lv);
                    ltype = lva.Instruction.ResultType (function.IRModule);
                }
                var ft = (FunctionType)((Types.PointerType)ltype).ElementType;
                var ps = ft.ParameterTypes;
                var nps = ps.Length;
                var hasVarArgs = nps > 0 && (ps[nps - 1] is VarArgsType);
                if (hasVarArgs)
                    nps--;
                if (call.Arguments.Length < nps) {
                    throw new InvalidOperationException ($"Too few arguments to {function.IRDefinition.Symbol}");
                }
                for (var i = 0; i < nps; i++) {
                    var a = call.Arguments[i];
                    EmitValue (a.Value, a.Type);
                }
                if (hasVarArgs) {
                    EmitVarArgs (call.Arguments, nps);
                }
                EmitValue (lv, ltype);
                EmitCalli (CreateCallSite (ft));
                return;
            }
            else if (call.Pointer is IR.InlineAssemblyValue asm) {
                if (!string.IsNullOrWhiteSpace (asm.Assembly)) {
                    var msg = new Message (MessageType.Warning, $"Native assembly not supported in `{function.Symbol}`");
                    Messages.Add (msg);
                    msg.FilePath = module.SourceFilename;
                }
                return;
            }
            throw new NotSupportedException($"Cannot call `{call.Pointer}`");
        }

        void EmitCalli (CallSite site)
        {
            // Convert the token to an actual function pointer
            Emit (il.Create (OpCodes.Call, compilation.LoadFunction));
            Emit (il.Create (OpCodes.Calli, site));
        }

        void EmitContext (TypeDefinition fromType, TypeDefinition toType)
        {
            throw new NotImplementedException ();
        }

        void AddLocalDebugInfo (Block block, LocalSymbol local, SymbolTable<object> metadata)
        {
            if (!blockLocalNames.TryGetValue (block.Symbol, out var names)) {
                names = new SymbolTable<string> ();
                blockLocalNames.Add (block.Symbol, names);
            }
            names[local] = metadata[Symbol.Name].ToString();
        }

        private void EmitVarArgs(IR.Argument[] arguments, int numFixedArgs)
        {
            var numVarArgs = arguments.Length - numFixedArgs;
            Emit(il.Create(OpCodes.Ldc_I4, numVarArgs));
            Emit(il.Create(OpCodes.Newarr, compilation.sysObj));
            for (var i = 0; i < numVarArgs; i++)
            {
                Emit(il.Create(OpCodes.Dup));
                var a = arguments[numFixedArgs + i];
                Emit(il.Create(OpCodes.Ldc_I4, i));
                EmitValue(a.Value, a.Type);
                
                EmitBox (a.Type);
                Emit (il.Create(OpCodes.Stelem_Any, compilation.sysObj));
            }
        }

        CecilInstruction GetLabel(IR.LabelValue label, IR.Block fromBlock, BlocksContext context)
        {
            if (context.BlockPredInstr.TryGetValue(label.Symbol, out var preds))
            {
                if (preds.TryGetValue(fromBlock.Symbol, out var i))
                    return i;
            }
            return context.BlockFirstInstr[label.Symbol];
        }

        class SharedVariable
        {
            public readonly HashSet<LocalSymbol> Users =
                new HashSet<LocalSymbol>();
            public VariableDefinition Variable;
            public TypeReference ClrType => Variable.VariableType;
        }
        readonly Dictionary<string, List<SharedVariable>> sharedVariablesByType =
            new Dictionary<string, List<SharedVariable>>();

        VariableDefinition GetFreeVariable(LocalSymbol symbol, TypeReference clrType)
        {
            //
            // Get the right list
            //
            var types = sharedVariablesByType;
            var living = liveliness;

            if (!types.TryGetValue(clrType.FullName, out var variables))
            {
                variables = new List<SharedVariable>();
                types[clrType.FullName] = variables;
            }

            //
            // Has it already been assigned?
            //
            foreach (var v in variables)
            {
                if (v.Users.Contains(symbol))
                    return v.Variable;
            }

            //
            // Find an existing variable with no interference
            //
            SharedVariable variable = null;
            foreach (var v in variables)
            {
                var interferes = living.VariablesInterfere(symbol, v.Users);
                if (!interferes)
                {
                    variable = v;
                    break;
                }
            }

            //
            // If we didn't find one, create one
            //
            if (variable == null)
            {
                var vd = new VariableDefinition(clrType);
                variable = new SharedVariable
                {
                    Variable = vd,
                };
                body.Variables.Add(vd);
                variables.Add(variable);
            }
            variable.Users.Add(symbol);
            return variable.Variable;
        }

        CallSite CreateCallSite(FunctionType ft)
        {
            var c = new CallSite(compilation.GetClrType(ft.ReturnType, module: module));
            foreach (var p in ft.ParameterTypes)
            {
                var pd = new ParameterDefinition(compilation.GetClrType(p, module: module));
                c.Parameters.Add(pd);
            }
            return c;
        }

        SimdVector GetVectorType(VectorType vt) => compilation.GetVectorType(vt, module: module);
    }
}
