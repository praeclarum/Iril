// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Repil/IR/IR.jay"
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

using Repil.Types;

#pragma warning disable 219,414

namespace Repil.IR
{
	public partial class Parser
	{
#line default

  /** error output stream.
      It should be changeable.
    */
  public System.IO.TextWriter ErrorOutput = new StringWriter ();

  /** simplified error message.
      @see <a href="#yyerror(java.lang.String, java.lang.String[])">yyerror</a>
    */
  public void yyerror (string message) {
    yyerror(message, null);
  }

  /* An EOF token */
  public int eof_token;
  
  public int yacc_verbose_flag;

  /** (syntax) error message.
      Can be overwritten to control message format.
      @param message text to be displayed.
      @param expected vector of acceptable tokens, if available.
    */
  public void yyerror (string message, string[] expected) {
    if ((yacc_verbose_flag > 0) && (expected != null) && (expected.Length  > 0)) {
      ErrorOutput.Write (message+", expecting");
      for (int n = 0; n < expected.Length; ++ n)
        ErrorOutput.Write (" "+expected[n]);
        ErrorOutput.WriteLine ();
    } else
      ErrorOutput.WriteLine (message);
  }

  /** debugging support, requires the package jay.yydebug.
      Set to null to suppress debugging messages.
    */
//t  internal yydebug.yyDebug debug;

  protected const int yyFinal = 5;
//t // Put this array into a separate class so it is only initialized if debugging is actually used
//t // Use MarshalByRefObject to disable inlining
//t class YYRules : MarshalByRefObject {
//t  public static readonly string [] yyRule = {
//t    "$accept : module",
//t    "module : module_parts",
//t    "module_parts : module_part",
//t    "module_parts : module_parts module_part",
//t    "module_part : SOURCE_FILENAME '=' STRING",
//t    "module_part : TARGET DATALAYOUT '=' STRING",
//t    "module_part : TARGET TRIPLE '=' STRING",
//t    "module_part : LOCAL_SYMBOL '=' TYPE literal_structure",
//t    "module_part : function_definition",
//t    "literal_structure : '{' type_list '}'",
//t    "type_list : type",
//t    "type_list : type_list ',' type",
//t    "type : literal_structure",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
//t    "type : type '(' type_list ')'",
//t    "type : type '*'",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' instructions '}'",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list parameter",
//t    "parameter : type",
//t    "function_addr : UNNAMED_ADDR",
//t    "function_addr : LOCAL_UNNAMED_ADDR",
//t    "attribute_group_refs : attribute_group_ref",
//t    "attribute_group_refs : attribute_group_refs attribute_group_ref",
//t    "attribute_group_ref : ATTRIBUTE_GROUP_REF",
//t    "icmp_condition : EQ",
//t    "icmp_condition : NE",
//t    "icmp_condition : UGT",
//t    "icmp_condition : UGE",
//t    "icmp_condition : ULT",
//t    "icmp_condition : ULE",
//t    "icmp_condition : SGT",
//t    "icmp_condition : SGE",
//t    "icmp_condition : SLT",
//t    "icmp_condition : SLE",
//t    "value : constant",
//t    "value : LOCAL_SYMBOL",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : '<' typed_constants '>'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_constant : type constant",
//t    "typed_constants : typed_constant",
//t    "typed_constants : typed_constants ',' typed_constant",
//t    "tbaa : META_SYMBOL META_SYMBOL",
//t    "element_index : type INTEGER",
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "instructions : instruction",
//t    "instructions : instructions instruction",
//t    "instruction : terminal_instruction",
//t    "instruction : assign_instruction",
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t    "terminal_instruction : RET",
//t    "terminal_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminal_instruction : BR label_value",
//t    "assign_instruction : LOCAL_SYMBOL '=' ICMP icmp_condition type value ',' value",
//t    "assign_instruction : LOCAL_SYMBOL '=' BITCAST typed_value TO type",
//t    "assign_instruction : LOCAL_SYMBOL '=' GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t  };
//t public static string getRule (int index) {
//t    return yyRule [index];
//t }
//t}
  protected static readonly string [] yyNames = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    "'('","')'","'*'",null,"','",null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"'<'","'='","'>'",null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,"'{'",null,"'}'",null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,"INTEGER",
    "FLOAT_LITERAL","STRING","TRUE","FALSE","NULL","LABEL","X",
    "SOURCE_FILENAME","TARGET","DATALAYOUT","TRIPLE","GLOBAL_SYMBOL",
    "LOCAL_SYMBOL","META_SYMBOL","TYPE","HALF","FLOAT","DOUBLE","I1","I8",
    "I16","I32","I64","DEFINE","UNNAMED_ADDR","LOCAL_UNNAMED_ADDR",
    "ATTRIBUTE_GROUP_REF","RET","BR","SWITCH","INDIRECTBR","INVOKE",
    "RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG",
    "ADD","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV","UREM",
    "SREM","FREM","SHL","LSHR","ASHR","AND","OR","XOR","EXTRACTELEMENT",
    "INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE","INSERTVALUE","ALLOCA",
    "LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW","GETELEMENTPTR","ALIGN",
    "INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT","FPTRUNC","FPEXT","TO",
    "FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT","INTTOPTR","BITCAST",
    "ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE","ULT","ULE","SGT","SGE",
    "SLT","SLE","FCMP","OEQ","OGT","OGE","OLT","OLE","ONE","ORD","UEQ",
    "UNE","UNO","PHI","SELECT","CALL","VA_ARG","LANDINGPAD","CATCHPAD",
    "CLEANUPPAD",
  };

  /** index-checked interface to yyNames[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
//t  public static string yyname (int token) {
//t    if ((token < 0) || (token > yyNames.Length)) return "[illegal]";
//t    string name;
//t    if ((name = yyNames[token]) != null) return name;
//t    return "[unknown]";
//t  }

  //int yyExpectingState;
  /** computes list of expected tokens on error by tracing the tables.
      @param state for which to compute the list.
      @return list of token names.
    */
  protected int [] yyExpectingTokens (int state){
    int token, n, len = 0;
    bool[] ok = new bool[yyNames.Length];
    if ((n = yySindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    if ((n = yyRindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    int [] result = new int [len];
    for (n = token = 0; n < len;  ++ token)
      if (ok[token]) result[n++] = token;
    return result;
  }
  protected string[] yyExpecting (int state) {
    int [] tokens = yyExpectingTokens (state);
    string [] result = new string[tokens.Length];
    for (int n = 0; n < tokens.Length;  n++)
      result[n++] = yyNames[tokens [n]];
    return result;
  }

  /** the generated parser, with debugging messages.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @param yydebug debug message writer implementing yyDebug, or null.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex, Object yyd)
				 {
//t    this.debug = (yydebug.yyDebug)yyd;
    return yyparse(yyLex);
  }

  /** initial size and increment of the state/value stack [default 256].
      This is not final so that it can be overwritten outside of invocations
      of yyparse().
    */
  protected int yyMax;

  /** executed at the beginning of a reduce action.
      Used as $$ = yyDefault($1), prior to the user-specified action, if any.
      Can be overwritten to provide deep copy, etc.
      @param first value for $1, or null.
      @return first.
    */
  protected Object yyDefault (Object first) {
    return first;
  }

	static int[] global_yyStates;
	static object[] global_yyVals;
	protected bool use_global_stacks;
	object[] yyVals;					// value stack
	object yyVal;						// value stack ptr
	int yyToken;						// current input
	int yyTop;

  /** the generated parser.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex)
  {
    if (yyMax <= 0) yyMax = 256;		// initial size
    int yyState = 0;                   // state stack ptr
    int [] yyStates;               	// state stack 
    yyVal = null;
    yyToken = -1;
    int yyErrorFlag = 0;				// #tks to shift
	if (use_global_stacks && global_yyStates != null) {
		yyVals = global_yyVals;
		yyStates = global_yyStates;
   } else {
		yyVals = new object [yyMax];
		yyStates = new int [yyMax];
		if (use_global_stacks) {
			global_yyVals = yyVals;
			global_yyStates = yyStates;
		}
	}

    /*yyLoop:*/ for (yyTop = 0;; ++ yyTop) {
      if (yyTop >= yyStates.Length) {			// dynamically increase
        global::System.Array.Resize (ref yyStates, yyStates.Length+yyMax);
        global::System.Array.Resize (ref yyVals, yyVals.Length+yyMax);
      }
      yyStates[yyTop] = yyState;
      yyVals[yyTop] = yyVal;
//t      if (debug != null) debug.push(yyState, yyVal);

      /*yyDiscarded:*/ while (true) {	// discarding a token does not change stack
        int yyN;
        if ((yyN = yyDefRed[yyState]) == 0) {	// else [default] reduce (yyN)
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t              debug.lex(yyState, yyToken, yyname(yyToken), yyLex.value());
          }
          if ((yyN = yySindex[yyState]) != 0 && ((yyN += yyToken) >= 0)
              && (yyN < yyTable.Length) && (yyCheck[yyN] == yyToken)) {
//t            if (debug != null)
//t              debug.shift(yyState, yyTable[yyN], yyErrorFlag-1);
            yyState = yyTable[yyN];		// shift to yyN
            yyVal = yyLex.value();
            yyToken = -1;
            if (yyErrorFlag > 0) -- yyErrorFlag;
            goto continue_yyLoop;
          }
          if ((yyN = yyRindex[yyState]) != 0 && (yyN += yyToken) >= 0
              && yyN < yyTable.Length && yyCheck[yyN] == yyToken)
            yyN = yyTable[yyN];			// reduce (yyN)
          else
            switch (yyErrorFlag) {
  
            case 0:
              //yyExpectingState = yyState;
              // yyerror(String.Format ("syntax error, got token `{0}'", yyname (yyToken)), yyExpecting(yyState));
//t              if (debug != null) debug.error("syntax error");
              if (yyToken == 0 /*eof*/ || yyToken == eof_token) throw new yyParser.yyUnexpectedEof ();
              goto case 1;
            case 1: case 2:
              yyErrorFlag = 3;
              do {
                if ((yyN = yySindex[yyStates[yyTop]]) != 0
                    && (yyN += Token.yyErrorCode) >= 0 && yyN < yyTable.Length
                    && yyCheck[yyN] == Token.yyErrorCode) {
//t                  if (debug != null)
//t                    debug.shift(yyStates[yyTop], yyTable[yyN], 3);
                  yyState = yyTable[yyN];
                  yyVal = yyLex.value();
                  goto continue_yyLoop;
                }
//t                if (debug != null) debug.pop(yyStates[yyTop]);
              } while (-- yyTop >= 0);
//t              if (debug != null) debug.reject();
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
              }
//t              if (debug != null)
//t                debug.discard(yyState, yyToken, yyname(yyToken),
//t  							yyLex.value());
              yyToken = -1;
              goto continue_yyDiscarded;		// leave stack alone
            }
        }
        int yyV = yyTop + 1-yyLen[yyN];
//t        if (debug != null)
//t          debug.reduce(yyState, yyStates[yyV-1], yyN, YYRules.getRule (yyN), yyLen[yyN]);
        yyVal = yyV > yyTop ? null : yyVals[yyV]; // yyVal = yyDefault(yyV > yyTop ? null : yyVals[yyV]);
        switch (yyN) {
case 4:
#line 55 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 59 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 63 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 67 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 9:
  case_9();
  break;
case 10:
#line 83 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 11:
#line 87 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 13:
#line 92 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 14:
#line 93 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 15:
#line 94 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 16:
#line 95 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 17:
#line 96 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 18:
#line 97 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 19:
#line 98 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 20:
#line 102 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ();
    }
  break;
case 21:
#line 106 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 22:
#line 110 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 23:
#line 114 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 24:
#line 121 "Repil/IR/IR.jay"
  {
        var f = new FunctionDefinition ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-9+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Instruction>)yyVals[-1+yyTop]);
    }
  break;
case 33:
#line 148 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 34:
#line 149 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 35:
#line 150 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 36:
#line 151 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 37:
#line 152 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 38:
#line 153 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 39:
#line 154 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 40:
#line 155 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 41:
#line 156 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 42:
#line 157 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 44:
#line 162 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 45:
#line 166 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 46:
#line 167 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 47:
#line 168 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 48:
#line 172 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 49:
#line 179 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 50:
#line 186 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 51:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 52:
#line 200 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 53:
#line 204 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 55:
#line 215 "Repil/IR/IR.jay"
  {
        yyVal = (int)(BigInteger)yyVals[0+yyTop];
    }
  break;
case 56:
#line 222 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((int)yyVals[0+yyTop]);
    }
  break;
case 57:
#line 226 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (int)yyVals[0+yyTop]);
    }
  break;
case 64:
#line 245 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 65:
#line 249 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 256 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((LocalSymbol)yyVals[-7+yyTop], (IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 260 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((LocalSymbol)yyVals[-5+yyTop], (TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 264 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LocalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<int>)yyVals[0+yyTop]);
    }
  break;
#line default
        }
        yyTop -= yyLen[yyN];
        yyState = yyStates[yyTop];
        int yyM = yyLhs[yyN];
        if (yyState == 0 && yyM == 0) {
//t          if (debug != null) debug.shift(0, yyFinal);
          yyState = yyFinal;
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t               debug.lex(yyState, yyToken,yyname(yyToken), yyLex.value());
          }
          if (yyToken == 0) {
//t            if (debug != null) debug.accept(yyVal);
            return yyVal;
          }
          goto continue_yyLoop;
        }
        if (((yyN = yyGindex[yyM]) != 0) && ((yyN += yyState) >= 0)
            && (yyN < yyTable.Length) && (yyCheck[yyN] == yyState))
          yyState = yyTable[yyN];
        else
          yyState = yyDgoto[yyM];
//t        if (debug != null) debug.shift(yyStates[yyTop], yyState);
	 goto continue_yyLoop;
      continue_yyDiscarded: ;	// implements the named-loop continue: 'continue yyDiscarded'
      }
    continue_yyLoop: ;		// implements the named-loop continue: 'continue yyLoop'
    }
  }

/*
 All more than 3 lines long rules are wrapped into a method
*/
void case_9()
#line 73 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ();
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    3,    5,
    5,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    6,    4,    7,    7,   11,    8,    8,    9,
    9,   12,   13,   13,   13,   13,   13,   13,   13,   13,
   13,   13,   14,   14,   15,   15,   15,   15,   17,   18,
   19,   16,   16,   20,   21,   22,   22,   10,   10,   23,
   23,   23,   24,   24,   24,   25,   25,   25,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    3,    1,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    4,
    2,    1,    5,   11,    1,    2,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    3,    2,    2,
    2,    1,    3,    2,    2,    1,    3,    1,    2,    1,
    1,    9,    1,    7,    2,    8,    6,    9,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    2,    8,    0,    0,
    0,    0,   22,   13,   14,   15,   16,   17,   18,   19,
    0,    0,   12,    0,    3,    4,    0,    0,    0,    0,
    0,    0,    0,    0,   21,    5,    6,    7,    9,    0,
    0,    0,    0,    0,    0,    0,    0,   25,   20,   23,
    0,   26,   28,   29,    0,   32,    0,   30,    0,   31,
    0,   63,    0,    0,    0,   58,   60,   61,    0,    0,
    0,   65,    0,    0,   24,   59,    0,    0,    0,   49,
   47,   46,   45,   44,    0,    0,   43,   50,    0,    0,
    0,   33,   34,   35,   36,   37,   38,   39,   40,   41,
   42,    0,    0,    0,   52,    0,    0,    0,    0,    0,
   51,    0,   48,    0,    0,    0,    0,    0,   53,    0,
    0,    0,    0,   64,    0,    0,   66,    0,    0,   56,
    0,    0,   62,   55,    0,   54,   57,
  };
  protected static readonly short [] yyDgoto  = {             5,
    6,    7,   23,    8,   30,   73,   47,   55,   57,   65,
   48,   58,  102,   86,   87,  104,   72,   74,  105,  133,
  130,  131,   66,   67,   68,
  };
  protected static readonly short [] yySindex = {         -208,
  -26, -203,  -23,  -53,    0, -208,    0,    0, -199,    8,
   13, -197,    0,    0,    0,    0,    0,    0,    0,    0,
  -53, -181,    0,  -34,    0,    0, -182, -180,  -45,  -40,
   12, -184,   41,  -53,    0,    0,    0,    0,    0,  -53,
  -53,  -53,    1,   12,   -6,   12,  -41,    0,    0,    0,
 -211,    0,    0,    0, -201,    0, -109,    0, -254,    0,
   23,    0, -226,  -53, -120,    0,    0,    0, -292, -183,
  -58,    0,  -39,   42,    0,    0, -239,  -53, -225,    0,
    0,    0,    0,    0,  -53,   45,    0,    0,  -53,  -53,
 -244,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -53,  -16,  -21,    0, -171,   50,   19,  -53,  -39,
    0,  -53,    0,   51, -229,  -53,   12,   54,    0, -171,
 -158,   56,  -58,    0,   58,  -53,    0, -168,  -27,    0,
   60, -166,    0,    0,  -53,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,  107,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  -24,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -19,    0,  -30,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, -116,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -113,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  103,   81,    0,   77,    6,    0,    0,    0,    0,
   65,   57,    0,  -55,   10,    0,  -77,  -50,    4,    0,
  -18,    0,   64,    0,    0,
  };
  protected static readonly short [] yyTable = {            51,
   34,   85,   35,   40,   75,   34,   22,   35,   67,   24,
   27,   68,   34,   59,   35,   61,   10,   88,   22,   10,
   85,   11,  112,   34,   11,   35,   31,   91,  114,   27,
   62,   63,   77,   34,    9,   35,   70,   12,  107,   31,
  113,   49,  124,   85,   40,   44,   45,   46,   78,   71,
   79,   34,   46,   35,  118,   50,    1,    2,   34,   26,
   35,    3,  116,   10,   11,  122,   64,  127,   27,   21,
   53,   54,    4,   28,   29,   32,   36,   21,   37,   41,
   42,   21,   56,   69,   39,   89,   80,   90,  106,  109,
  103,   70,   27,  115,  120,  108,  121,  123,  125,  126,
   10,  128,  132,  135,  136,   11,    1,  110,   25,   38,
   43,   52,  111,   60,  117,  119,  137,  103,   92,   93,
   94,   95,   96,   97,   98,   99,  100,  101,   76,    0,
    0,  129,    0,    0,    0,    0,    0,    0,    0,    0,
  129,    0,    0,    0,    0,    0,    0,    0,    0,   61,
    0,    0,    0,   67,    0,    0,   68,    0,    0,    0,
    0,    0,    0,    0,   62,   63,    0,    0,   67,   67,
    0,   68,   68,    0,   56,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   81,   82,
   64,    0,    0,   83,   67,    0,    0,   68,    0,    0,
    0,   84,    0,    0,    0,    0,   13,   81,   82,   14,
   15,   16,   83,   17,   18,   19,   20,    0,   13,  134,
   84,   14,   15,   16,   33,   17,   18,   19,   20,   27,
   81,   82,   27,   27,   27,   83,   27,   27,   27,   27,
  };
  protected static readonly short [] yyCheck = {            41,
   40,   60,   42,   44,  125,   40,   60,   42,  125,    4,
   41,  125,   40,  123,   42,  270,   41,   73,   60,   44,
   60,   41,   44,   40,   44,   42,   21,   78,  106,   60,
  285,  286,  325,   40,   61,   42,  263,   61,   89,   34,
   62,   41,  120,   60,   44,   40,   41,   42,  341,  276,
  343,   40,   47,   42,  110,   62,  265,  266,   40,  259,
   42,  270,   44,  267,  268,  116,  321,  123,   61,  123,
  282,  283,  281,   61,  272,  257,  259,  123,  259,  264,
   40,  123,  284,   61,  125,   44,  270,  327,   44,  334,
   85,  263,  123,   44,   44,   90,  326,   44,  257,   44,
  125,   44,  271,   44,  271,  125,    0,  102,    6,   29,
   34,   47,  103,   57,  109,  112,  135,  112,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   65,   -1,
   -1,  126,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  135,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  270,
   -1,   -1,   -1,  270,   -1,   -1,  270,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  285,  286,   -1,   -1,  285,  286,
   -1,  285,  286,   -1,  284,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,
  321,   -1,   -1,  262,  321,   -1,   -1,  321,   -1,   -1,
   -1,  270,   -1,   -1,   -1,   -1,  270,  257,  258,  273,
  274,  275,  262,  277,  278,  279,  280,   -1,  270,  257,
  270,  273,  274,  275,  269,  277,  278,  279,  280,  270,
  257,  258,  273,  274,  275,  262,  277,  278,  279,  280,
  };

#line 268 "Repil/IR/IR.jay"

}

#line default
namespace yydebug {
        using System;
	 internal interface yyDebug {
		 void push (int state, Object value);
		 void lex (int state, int token, string name, Object value);
		 void shift (int from, int to, int errorFlag);
		 void pop (int state);
		 void discard (int state, int token, string name, Object value);
		 void reduce (int from, int to, int rule, string text, int len);
		 void shift (int from, int to);
		 void accept (Object value);
		 void error (string message);
		 void reject ();
	 }
	 
	 class yyDebugSimple : yyDebug {
		 void println (string s){
			 System.Diagnostics.Debug.WriteLine (s);
		 }
		 
		 public void push (int state, Object value) {
			 println ("push\tstate "+state+"\tvalue "+value);
		 }
		 
		 public void lex (int state, int token, string name, Object value) {
			 println("lex\tstate "+state+"\treading "+name+"\tvalue "+value);
		 }
		 
		 public void shift (int from, int to, int errorFlag) {
			 switch (errorFlag) {
			 default:				// normally
				 println("shift\tfrom state "+from+" to "+to);
				 break;
			 case 0: case 1: case 2:		// in error recovery
				 println("shift\tfrom state "+from+" to "+to
					     +"\t"+errorFlag+" left to recover");
				 break;
			 case 3:				// normally
				 println("shift\tfrom state "+from+" to "+to+"\ton error");
				 break;
			 }
		 }
		 
		 public void pop (int state) {
			 println("pop\tstate "+state+"\ton error");
		 }
		 
		 public void discard (int state, int token, string name, Object value) {
			 println("discard\tstate "+state+"\ttoken "+name+"\tvalue "+value);
		 }
		 
		 public void reduce (int from, int to, int rule, string text, int len) {
			 println("reduce\tstate "+from+"\tuncover "+to
				     +"\trule ("+rule+") "+text);
		 }
		 
		 public void shift (int from, int to) {
			 println("goto\tfrom state "+from+" to "+to);
		 }
		 
		 public void accept (Object value) {
			 println("accept\tvalue "+value);
		 }
		 
		 public void error (string message) {
			 println("error\t"+message);
		 }
		 
		 public void reject () {
			 println("reject");
		 }
		 
	 }
}
// %token constants
 class Token {
  public const int INTEGER = 257;
  public const int FLOAT_LITERAL = 258;
  public const int STRING = 259;
  public const int TRUE = 260;
  public const int FALSE = 261;
  public const int NULL = 262;
  public const int LABEL = 263;
  public const int X = 264;
  public const int SOURCE_FILENAME = 265;
  public const int TARGET = 266;
  public const int DATALAYOUT = 267;
  public const int TRIPLE = 268;
  public const int GLOBAL_SYMBOL = 269;
  public const int LOCAL_SYMBOL = 270;
  public const int META_SYMBOL = 271;
  public const int TYPE = 272;
  public const int HALF = 273;
  public const int FLOAT = 274;
  public const int DOUBLE = 275;
  public const int I1 = 276;
  public const int I8 = 277;
  public const int I16 = 278;
  public const int I32 = 279;
  public const int I64 = 280;
  public const int DEFINE = 281;
  public const int UNNAMED_ADDR = 282;
  public const int LOCAL_UNNAMED_ADDR = 283;
  public const int ATTRIBUTE_GROUP_REF = 284;
  public const int RET = 285;
  public const int BR = 286;
  public const int SWITCH = 287;
  public const int INDIRECTBR = 288;
  public const int INVOKE = 289;
  public const int RESUME = 290;
  public const int CATCHSWITCH = 291;
  public const int CATCHRET = 292;
  public const int CLEANUPRET = 293;
  public const int UNREACHABLE = 294;
  public const int FNEG = 295;
  public const int ADD = 296;
  public const int FADD = 297;
  public const int SUB = 298;
  public const int FSUB = 299;
  public const int MUL = 300;
  public const int FMUL = 301;
  public const int UDIV = 302;
  public const int SDIV = 303;
  public const int FDIV = 304;
  public const int UREM = 305;
  public const int SREM = 306;
  public const int FREM = 307;
  public const int SHL = 308;
  public const int LSHR = 309;
  public const int ASHR = 310;
  public const int AND = 311;
  public const int OR = 312;
  public const int XOR = 313;
  public const int EXTRACTELEMENT = 314;
  public const int INSERTELEMENT = 315;
  public const int SHUFFLEVECTOR = 316;
  public const int EXTRACTVALUE = 317;
  public const int INSERTVALUE = 318;
  public const int ALLOCA = 319;
  public const int LOAD = 320;
  public const int STORE = 321;
  public const int FENCE = 322;
  public const int CMPXCHG = 323;
  public const int ATOMICRMW = 324;
  public const int GETELEMENTPTR = 325;
  public const int ALIGN = 326;
  public const int INBOUNDS = 327;
  public const int INRANGE = 328;
  public const int TRUNC = 329;
  public const int ZEXT = 330;
  public const int SEXT = 331;
  public const int FPTRUNC = 332;
  public const int FPEXT = 333;
  public const int TO = 334;
  public const int FPTOUI = 335;
  public const int FPTOSI = 336;
  public const int UITOFP = 337;
  public const int SITOFP = 338;
  public const int PTRTOINT = 339;
  public const int INTTOPTR = 340;
  public const int BITCAST = 341;
  public const int ADDRSPACECAST = 342;
  public const int ICMP = 343;
  public const int EQ = 344;
  public const int NE = 345;
  public const int UGT = 346;
  public const int UGE = 347;
  public const int ULT = 348;
  public const int ULE = 349;
  public const int SGT = 350;
  public const int SGE = 351;
  public const int SLT = 352;
  public const int SLE = 353;
  public const int FCMP = 354;
  public const int OEQ = 355;
  public const int OGT = 356;
  public const int OGE = 357;
  public const int OLT = 358;
  public const int OLE = 359;
  public const int ONE = 360;
  public const int ORD = 361;
  public const int UEQ = 362;
  public const int UNE = 363;
  public const int UNO = 364;
  public const int PHI = 365;
  public const int SELECT = 366;
  public const int CALL = 367;
  public const int VA_ARG = 368;
  public const int LANDINGPAD = 369;
  public const int CATCHPAD = 370;
  public const int CLEANUPPAD = 371;
  public const int yyErrorCode = 256;
 }
 namespace yyParser {
  using System;
  /** thrown for irrecoverable syntax errors and stack overflow.
    */
  internal class yyException : System.Exception {
    public yyException (string message) : base (message) {
    }
  }
  internal class yyUnexpectedEof : yyException {
    public yyUnexpectedEof (string message) : base (message) {
    }
    public yyUnexpectedEof () : base ("") {
    }
  }

  /** must be implemented by a scanner object to supply input to the parser.
    */
  internal interface yyInput {
    /** move on to next token.
        @return false if positioned beyond tokens.
        @throws IOException on input error.
      */
    bool advance (); // throws java.io.IOException;
    /** classifies current token.
        Should not be called if advance() returned false.
        @return current %token or single character.
      */
    int token ();
    /** associated with current token.
        Should not be called if advance() returned false.
        @return value for token().
      */
    Object value ();
  }
 }
} // close outermost namespace, that MUST HAVE BEEN opened in the prolog
