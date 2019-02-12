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

  protected const int yyFinal = 8;
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
//t    "module_part : function_declaration",
//t    "module_part : ATTRIBUTES ATTRIBUTE_GROUP_REF '=' '{' attributes '}'",
//t    "module_part : META_SYMBOL '=' '!' '{' metadata '}'",
//t    "attributes : attribute",
//t    "attributes : attributes attribute",
//t    "attribute : NORECURSE",
//t    "attribute : NOUNWIND",
//t    "attribute : SSP",
//t    "attribute : UWTABLE",
//t    "attribute : ARGMEMONLY",
//t    "attribute : STRING '=' STRING",
//t    "attribute : STRING",
//t    "metadata : metadatum",
//t    "metadata : metadata ',' metadatum",
//t    "metadatum : typed_value",
//t    "metadatum : META_SYMBOL",
//t    "literal_structure : '{' type_list '}'",
//t    "type_list : type",
//t    "type_list : type_list ',' type",
//t    "type : literal_structure",
//t    "type : VOID",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I1",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
//t    "type : type '(' type_list ')'",
//t    "type : type '*'",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_declaration : DECLARE type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list ',' parameter",
//t    "parameter : type",
//t    "parameter : type parameter_attributes",
//t    "parameter_attributes : parameter_attribute",
//t    "parameter_attributes : parameter_attributes parameter_attribute",
//t    "parameter_attribute : NONNULL",
//t    "parameter_attribute : NOCAPTURE",
//t    "parameter_attribute : WRITEONLY",
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
//t    "value : GLOBAL_SYMBOL",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
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
//t    "blocks : block",
//t    "blocks : blocks block",
//t    "block : assignments terminal_assignment",
//t    "assignments : assignment",
//t    "assignments : assignments assignment",
//t    "terminal_assignment : terminal_instruction",
//t    "assignment : instruction",
//t    "assignment : LOCAL_SYMBOL '=' instruction",
//t    "function_pointer : value",
//t    "function_args : function_arg",
//t    "function_args : function_args ',' function_arg",
//t    "function_arg : type value",
//t    "function_arg : type parameter_attributes value",
//t    "phi_vals : phi_val",
//t    "phi_vals : phi_vals ',' phi_val",
//t    "phi_val : '[' value ',' value ']'",
//t    "terminal_instruction : RET typed_value",
//t    "terminal_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminal_instruction : BR label_value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL type function_pointer '(' function_args ')'",
//t    "instruction : CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : LOAD type ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t    "instruction : PHI type phi_vals",
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t  };
//t public static string getRule (int index) {
//t    return yyRule [index];
//t }
//t}
  protected static readonly string [] yyNames = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"'!'",null,null,null,null,null,
    null,"'('","')'","'*'",null,"','",null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,"'<'","'='","'>'",null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,"'['",
    null,"']'",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,"'{'",null,"'}'",null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,
    "INTEGER","FLOAT_LITERAL","STRING","TRUE","FALSE","UNDEF","VOID",
    "NULL","LABEL","X","SOURCE_FILENAME","TARGET","DATALAYOUT","TRIPLE",
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","TYPE","HALF","FLOAT",
    "DOUBLE","I1","I8","I16","I32","I64","DEFINE","DECLARE",
    "UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NONNULL","NOCAPTURE","WRITEONLY",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","SSP",
    "UWTABLE","ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR","INVOKE",
    "RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG",
    "ADD","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV","UREM",
    "SREM","FREM","SHL","LSHR","ASHR","AND","OR","XOR","EXTRACTELEMENT",
    "INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE","INSERTVALUE","ALLOCA",
    "LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW","GETELEMENTPTR","ALIGN",
    "INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT","FPTRUNC","FPEXT","TO",
    "FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT","INTTOPTR","BITCAST",
    "ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE","ULT","ULE","SGT","SGE",
    "SLT","SLE","FCMP","OEQ","OGT","OGE","OLT","OLE","ONE","ORD","UEQ",
    "UNE","UNO","PHI","SELECT","CALL","TAIL","VA_ARG","LANDINGPAD",
    "CATCHPAD","CLEANUPPAD",
  };

  /** index-checked interface to yyNames[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
  public static string yyname (int token) {
    if ((token < 0) || (token > yyNames.Length)) return "[illegal]";
    string name;
    if ((name = yyNames[token]) != null) return name;
    return "[unknown]";
  }

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
      result[n] = yyNames[tokens [n]];
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
              Console.WriteLine(String.Format ("syntax error, got token `{0}' expecting: {1}",
                                yyname (yyToken),
                                String.Join(", ", yyExpecting(yyState))));
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
#line 56 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 60 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 64 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 68 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
  case_8();
  break;
case 9:
  case_9();
  break;
case 25:
  case_25();
  break;
case 26:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 27:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 29:
#line 129 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 30:
#line 130 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 31:
#line 131 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 32:
#line 132 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 33:
#line 133 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 34:
#line 134 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 35:
#line 135 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 36:
#line 136 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 37:
#line 137 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 38:
#line 141 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 39:
#line 145 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 40:
#line 149 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 153 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 42:
#line 157 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 43:
#line 164 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 44:
#line 171 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 45:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 46:
#line 182 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 47:
#line 189 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 48:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 50:
#line 201 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 51:
#line 205 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 52:
#line 206 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 53:
#line 207 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 59:
#line 225 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 60:
#line 226 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 61:
#line 227 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 62:
#line 228 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 63:
#line 229 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 64:
#line 230 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 65:
#line 231 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 66:
#line 232 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 67:
#line 233 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 68:
#line 234 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 70:
#line 239 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 71:
#line 240 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 72:
#line 244 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 73:
#line 245 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 74:
#line 246 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 75:
#line 247 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 76:
#line 248 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 77:
#line 252 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 78:
#line 259 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 79:
#line 266 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 80:
#line 273 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 280 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 82:
#line 284 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 295 "Repil/IR/IR.jay"
  {
        yyVal = (int)(BigInteger)yyVals[0+yyTop];
    }
  break;
case 85:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((int)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (int)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 313 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 317 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 89:
  case_89();
  break;
case 90:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 91:
#line 337 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 92:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 93:
#line 351 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 355 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 366 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 97:
#line 370 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 377 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 99:
#line 381 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 100:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 392 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 398 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 103:
#line 405 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 409 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 413 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 420 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 424 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 428 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 109:
#line 432 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 110:
#line 436 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 111:
#line 440 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<int>)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 444 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 448 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 114:
#line 452 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 115:
#line 456 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
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
void case_8()
#line 70 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 75 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_25()
#line 110 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

void case_89()
#line 322 "Repil/IR/IR.jay"
{
        var l = (List<Assignment>)yyVals[-1+yyTop];
        l.Add ((Assignment)yyVals[0+yyTop]);
        yyVal = new Block (LocalSymbol.None, l);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    6,    6,    8,    8,    8,    8,    8,    8,    8,
    7,    7,    9,    9,    3,   11,   11,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,    4,    5,   13,   13,   17,   17,   18,   18,
   19,   19,   19,   14,   14,   15,   15,   20,   21,   21,
   21,   21,   21,   21,   21,   21,   21,   21,   22,   22,
   22,   23,   23,   23,   23,   23,   23,   25,   10,   26,
   24,   24,   27,   28,   29,   29,   16,   16,   30,   31,
   31,   32,   33,   33,   36,   37,   37,   38,   38,   39,
   39,   40,   34,   34,   34,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    6,
    6,    1,    2,    1,    1,    1,    1,    1,    3,    1,
    1,    3,    1,    1,    3,    1,    3,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    4,    2,    1,
    5,    5,   11,    7,    1,    3,    1,    2,    1,    2,
    1,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    2,    2,    2,
    1,    3,    2,    2,    1,    3,    1,    2,    2,    1,
    2,    1,    1,    3,    1,    1,    3,    2,    3,    1,
    3,    5,    2,    7,    2,    5,    4,    6,    7,    8,
    7,    6,    9,    3,    9,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    2,
    8,    9,    0,    0,    0,    0,    0,   29,   40,   30,
   31,   32,   33,   34,   35,   36,   37,    0,    0,    0,
   28,    0,    0,    0,    3,    4,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   39,    0,    0,    5,
    6,    7,    0,   25,    0,    0,    0,    0,    0,    0,
    0,   24,    0,   21,   23,    0,    0,    0,    0,    0,
    0,   45,   38,    0,    0,   14,   15,   16,   17,   18,
    0,   12,   11,    0,   74,   73,   75,   76,   72,   71,
   70,    0,   79,   69,   41,   42,   51,   52,   53,    0,
   49,    0,    0,    0,    0,   10,   13,   22,    0,    0,
   81,   50,   46,   54,   55,    0,   58,    0,   56,   19,
   80,    0,   77,    0,   57,   82,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   87,    0,
   90,   93,    0,    0,    0,    0,    0,    0,   59,   60,
   61,   62,   63,   64,   65,   66,   67,   68,    0,    0,
    0,    0,   43,   88,    0,    0,   89,   91,   92,   94,
    0,    0,    0,    0,    0,    0,    0,    0,  100,   95,
    0,    0,  103,    0,    0,  105,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   78,    0,  106,    0,
    0,    0,    0,    0,  101,    0,    0,   96,    0,    0,
    0,    0,    0,  112,    0,    0,   98,    0,    0,    0,
    0,    0,    0,    0,   85,    0,  102,   99,   97,    0,
    0,    0,    0,    0,   84,    0,    0,  104,    0,  113,
  115,   86,   83,
  };
  protected static readonly short [] yyDgoto  = {             8,
    9,   10,   31,   11,   12,   81,   63,   82,   64,   65,
   41,   66,   71,  116,  118,  138,   72,  100,  101,  119,
  159,  180,   94,  110,  186,  111,  240,  225,  226,  139,
  140,  167,  141,  169,  142,  181,  207,  208,  178,  179,
  };
  protected static readonly short [] yySindex = {           82,
  -45, -157,  -35,  -21,   51,   51, -236,    0,   82,    0,
    0,    0, -203,   16,   24, -214,   29,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   51, -160, -158,
    0,  -34,  -28,   56,    0,    0, -138, -136,   14,   20,
  -29,    5, -127, -120,  111,   51,    0,  114,   33,    0,
    0,    0,   40,    0,   51,   51,   51,   51,   -9,   51,
 -220,    0,  -19,    0,    0,    6,    5,    2,   -4,  -31,
   26,    0,    0,   27,   96,    0,    0,    0,    0,    0,
 -115,    0,    0,   40,    0,    0,    0,    0,    0,    0,
    0,   51,    0,    0,    0,    0,    0,    0,    0, -139,
    0,   51, -152, -130,  -98,    0,    0,    0,   44,  -13,
    0,    0,    0,    0,    0, -130,    0, -130,    0,    0,
    0,   51,    0, -118,    0,    0,  161,  101,   51,   51,
   51, -175,   51,  199,   51,   51, -213, -125,    0, -239,
    0,    0,  174,   13,   65,  121,   51, -179,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   51,   10,
    6,   51,    0,    0,   51, -235,    0,    0,    0,    0,
 -170,   51,   51,   84,   51,    6,  -47,  125,    0,    0,
  131,    6,    0,  -99,  -47,    0,  -74,  142,  143,   51,
    5,  149,  150,  104,   51,  156,    0,  153,    0, -137,
 -134,  155,  -47,  -47,    0,  -38,   46,    0,   51,  -65,
  -48,  -41,   51,    0,  128,   22,    0,   51, -130,   47,
  183,  185,  187,  -22,    0,  194,    0,    0,    0, -130,
 -130,  -65,  -33,  -33,    0,   51, -130,    0,  -27,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,  239,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -17,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   -7,    0,    0,   86,
    0,    0,    0,    0, -104,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   91,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    1,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    9,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   62,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   71,    0,
    0,    0,    0,    0,    0,   79,    0,    0,    0,  133,
    0,    0,    0,    0,    0,    0,  146,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  233,  205,    0,    0,    0,    0,  164,  163,  -70,
  202,   23,  193,    0, -109,    0,  158,   55,  -97, -101,
    0,   -1,  162,    0, -191,  140,   31,   36,    0,  137,
    0,    0,  136,    0,  144,  106,   80,   72,    0,   97,
  };
  protected static readonly short [] yyTable = {           163,
   44,   46,  112,   47,  127,   46,  124,   47,   46,  106,
   47,   46,   92,   47,   55,   13,  125,   46,  221,   47,
   20,   92,  125,   26,   84,   16,   26,   32,   33,  184,
  122,   73,  128,   27,   55,   46,   27,   47,   75,   17,
  238,   46,  185,   47,   46,   46,   47,   47,  123,   46,
   42,   47,   46,   34,   47,   36,  171,  165,  166,   39,
  146,   40,  148,   95,   93,   92,  103,  104,   42,  102,
  102,   76,   77,   78,   79,   80,   37,   67,   68,   69,
   70,   92,   70,   46,   38,   47,  219,  231,   96,  218,
  218,  129,  130,  131,  183,   54,   43,  132,   44,   29,
  177,  188,  189,   92,   46,   83,   47,   26,  172,  230,
   29,   14,   15,  133,  109,  134,   49,   27,  112,  202,
   50,  237,   51,   46,   70,   47,   47,  190,  125,   47,
   30,   48,  114,  115,   48,  125,   28,  135,   56,  136,
  137,   30,   53,   75,  109,   57,  128,   97,   98,   99,
   58,  144,  145,   60,   20,   61,  105,  160,  161,  117,
  120,  143,   28,  147,  173,  162,  175,  187,  194,  174,
  195,  117,  197,   28,  192,  193,   76,   77,   78,   79,
   80,  176,  199,  198,  182,  200,  201,   20,   20,   20,
   20,   20,  203,  204,  177,  209,  210,  191,  213,  184,
  211,  214,  215,  212,  217,  129,  130,  131,  222,   85,
   86,  132,   87,   88,  228,  223,   89,  206,   85,   86,
  227,   87,   88,   90,   91,   89,  232,  133,  233,  134,
  234,  206,   90,   91,  235,  224,   45,  236,    1,  239,
  206,   35,   48,   52,  107,  243,  108,   59,   97,   98,
   99,  135,   74,  136,  137,   97,   98,   99,  224,  113,
  216,  126,   85,   86,  241,   87,   88,   44,   44,   89,
  121,  242,   44,   44,  164,  168,   90,   91,   85,   86,
  114,   87,   88,   44,   44,   89,  170,  196,  220,  229,
  205,   44,   90,   91,    0,    0,    0,    0,    0,    0,
   85,   86,   18,   87,   88,  114,  114,   89,   97,   98,
   99,   19,   62,   18,   20,   21,   22,   23,   24,   25,
   26,   27,   19,    0,    0,   20,   21,   22,   23,   24,
   25,   26,   27,  107,    0,    0,    0,    0,    0,  114,
  114,  114,  108,    0,    0,  114,    0,    0,    1,    2,
  111,    0,    0,    3,    4,    0,    0,    0,  107,  107,
    0,  114,    0,  114,    5,    6,    0,  108,  108,    0,
    0,    0,    7,    0,    0,  111,  111,    0,    0,    0,
    0,    0,    0,    0,    0,  114,    0,  114,  114,    0,
    0,    0,  107,  107,  107,    0,    0,    0,  107,    0,
    0,  108,  108,  108,  109,    0,    0,  108,    0,  111,
  111,  111,    0,    0,  107,  111,  107,  110,    0,    0,
    0,    0,    0,  108,    0,  108,    0,    0,    0,  109,
  109,  111,  128,  111,    0,    0,    0,    0,  107,    0,
  107,  107,  110,  110,    0,    0,    0,  108,    0,  108,
  108,    0,    0,    0,    0,  111,    0,  111,  111,    0,
    0,    0,    0,  109,  109,  109,    0,    0,    0,  109,
    0,    0,    0,    0,    0,    0,  110,  110,  110,    0,
    0,    0,  110,    0,    0,  109,    0,  109,    0,    0,
    0,  129,  130,  131,    0,    0,    0,  132,  110,    0,
  110,    0,    0,    0,  129,  130,  131,    0,    0,  109,
  132,  109,  109,  133,    0,  134,    0,    0,    0,    0,
    0,    0,  110,    0,  110,  110,  133,    0,  134,    0,
    0,    0,    0,    0,    0,    0,    0,  135,    0,  136,
  137,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  135,    0,  136,  137,  149,  150,  151,  152,  153,  154,
  155,  156,  157,  158,
  };
  protected static readonly short [] yyCheck = {           125,
    0,   40,  100,   42,  123,   40,  116,   42,   40,  125,
   42,   40,   60,   42,   44,   61,  118,   40,  210,   42,
  125,   60,  124,   41,   44,   61,   44,    5,    6,  265,
   44,   41,  272,   41,   44,   40,   44,   42,  259,   61,
  232,   40,  278,   42,   40,   40,   42,   42,   62,   40,
   28,   42,   40,  290,   42,  259,   44,  297,  298,  274,
  131,   33,  133,   62,   66,   60,   41,   41,   46,   44,
   44,  292,  293,  294,  295,  296,   61,   55,   56,   57,
   58,   60,   60,   40,   61,   42,   41,   41,   93,   44,
   44,  331,  332,  333,  165,  125,  257,  337,  257,   60,
   91,  172,  173,   60,   40,  125,   42,  125,   44,  219,
   60,  269,  270,  353,   92,  355,   61,  125,  216,  190,
  259,  231,  259,   40,  102,   42,   41,   44,  230,   44,
   91,   41,  285,  286,   44,  237,  123,  377,  266,  379,
  380,   91,  123,  259,  122,  266,  272,  287,  288,  289,
   40,  129,  130,   40,  259,  123,   61,  135,  136,  290,
  259,   61,  123,  339,   44,  379,  346,  338,   44,  147,
   40,  290,  272,  123,  176,  177,  292,  293,  294,  295,
  296,  159,  257,  185,  162,   44,   44,  292,  293,  294,
  295,  296,   44,   44,   91,   40,   44,  175,   44,  265,
  338,  203,  204,  338,  206,  331,  332,  333,  257,  257,
  258,  337,  260,  261,  216,  257,  264,  195,  257,  258,
   93,  260,  261,  271,  272,  264,   44,  353,   44,  355,
   44,  209,  271,  272,  257,  213,  271,   44,    0,  273,
  218,    9,  271,   39,   81,  273,   84,   46,  287,  288,
  289,  377,   60,  379,  380,  287,  288,  289,  236,  102,
  206,  122,  257,  258,  234,  260,  261,  267,  268,  264,
  109,  236,  272,  273,  138,  140,  271,  272,  257,  258,
  272,  260,  261,  283,  284,  264,  143,  182,  209,  218,
  194,  291,  271,  272,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  263,  260,  261,  297,  298,  264,  287,  288,
  289,  272,  273,  263,  275,  276,  277,  278,  279,  280,
  281,  282,  272,   -1,   -1,  275,  276,  277,  278,  279,
  280,  281,  282,  272,   -1,   -1,   -1,   -1,   -1,  331,
  332,  333,  272,   -1,   -1,  337,   -1,   -1,  267,  268,
  272,   -1,   -1,  272,  273,   -1,   -1,   -1,  297,  298,
   -1,  353,   -1,  355,  283,  284,   -1,  297,  298,   -1,
   -1,   -1,  291,   -1,   -1,  297,  298,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  377,   -1,  379,  380,   -1,
   -1,   -1,  331,  332,  333,   -1,   -1,   -1,  337,   -1,
   -1,  331,  332,  333,  272,   -1,   -1,  337,   -1,  331,
  332,  333,   -1,   -1,  353,  337,  355,  272,   -1,   -1,
   -1,   -1,   -1,  353,   -1,  355,   -1,   -1,   -1,  297,
  298,  353,  272,  355,   -1,   -1,   -1,   -1,  377,   -1,
  379,  380,  297,  298,   -1,   -1,   -1,  377,   -1,  379,
  380,   -1,   -1,   -1,   -1,  377,   -1,  379,  380,   -1,
   -1,   -1,   -1,  331,  332,  333,   -1,   -1,   -1,  337,
   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,
   -1,   -1,  337,   -1,   -1,  353,   -1,  355,   -1,   -1,
   -1,  331,  332,  333,   -1,   -1,   -1,  337,  353,   -1,
  355,   -1,   -1,   -1,  331,  332,  333,   -1,   -1,  377,
  337,  379,  380,  353,   -1,  355,   -1,   -1,   -1,   -1,
   -1,   -1,  377,   -1,  379,  380,  353,   -1,  355,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  377,   -1,  379,
  380,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  377,   -1,  379,  380,  356,  357,  358,  359,  360,  361,
  362,  363,  364,  365,
  };

#line 460 "Repil/IR/IR.jay"

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
  public const int UNDEF = 262;
  public const int VOID = 263;
  public const int NULL = 264;
  public const int LABEL = 265;
  public const int X = 266;
  public const int SOURCE_FILENAME = 267;
  public const int TARGET = 268;
  public const int DATALAYOUT = 269;
  public const int TRIPLE = 270;
  public const int GLOBAL_SYMBOL = 271;
  public const int LOCAL_SYMBOL = 272;
  public const int META_SYMBOL = 273;
  public const int TYPE = 274;
  public const int HALF = 275;
  public const int FLOAT = 276;
  public const int DOUBLE = 277;
  public const int I1 = 278;
  public const int I8 = 279;
  public const int I16 = 280;
  public const int I32 = 281;
  public const int I64 = 282;
  public const int DEFINE = 283;
  public const int DECLARE = 284;
  public const int UNNAMED_ADDR = 285;
  public const int LOCAL_UNNAMED_ADDR = 286;
  public const int NONNULL = 287;
  public const int NOCAPTURE = 288;
  public const int WRITEONLY = 289;
  public const int ATTRIBUTE_GROUP_REF = 290;
  public const int ATTRIBUTES = 291;
  public const int NORECURSE = 292;
  public const int NOUNWIND = 293;
  public const int SSP = 294;
  public const int UWTABLE = 295;
  public const int ARGMEMONLY = 296;
  public const int RET = 297;
  public const int BR = 298;
  public const int SWITCH = 299;
  public const int INDIRECTBR = 300;
  public const int INVOKE = 301;
  public const int RESUME = 302;
  public const int CATCHSWITCH = 303;
  public const int CATCHRET = 304;
  public const int CLEANUPRET = 305;
  public const int UNREACHABLE = 306;
  public const int FNEG = 307;
  public const int ADD = 308;
  public const int FADD = 309;
  public const int SUB = 310;
  public const int FSUB = 311;
  public const int MUL = 312;
  public const int FMUL = 313;
  public const int UDIV = 314;
  public const int SDIV = 315;
  public const int FDIV = 316;
  public const int UREM = 317;
  public const int SREM = 318;
  public const int FREM = 319;
  public const int SHL = 320;
  public const int LSHR = 321;
  public const int ASHR = 322;
  public const int AND = 323;
  public const int OR = 324;
  public const int XOR = 325;
  public const int EXTRACTELEMENT = 326;
  public const int INSERTELEMENT = 327;
  public const int SHUFFLEVECTOR = 328;
  public const int EXTRACTVALUE = 329;
  public const int INSERTVALUE = 330;
  public const int ALLOCA = 331;
  public const int LOAD = 332;
  public const int STORE = 333;
  public const int FENCE = 334;
  public const int CMPXCHG = 335;
  public const int ATOMICRMW = 336;
  public const int GETELEMENTPTR = 337;
  public const int ALIGN = 338;
  public const int INBOUNDS = 339;
  public const int INRANGE = 340;
  public const int TRUNC = 341;
  public const int ZEXT = 342;
  public const int SEXT = 343;
  public const int FPTRUNC = 344;
  public const int FPEXT = 345;
  public const int TO = 346;
  public const int FPTOUI = 347;
  public const int FPTOSI = 348;
  public const int UITOFP = 349;
  public const int SITOFP = 350;
  public const int PTRTOINT = 351;
  public const int INTTOPTR = 352;
  public const int BITCAST = 353;
  public const int ADDRSPACECAST = 354;
  public const int ICMP = 355;
  public const int EQ = 356;
  public const int NE = 357;
  public const int UGT = 358;
  public const int UGE = 359;
  public const int ULT = 360;
  public const int ULE = 361;
  public const int SGT = 362;
  public const int SGE = 363;
  public const int SLT = 364;
  public const int SLE = 365;
  public const int FCMP = 366;
  public const int OEQ = 367;
  public const int OGT = 368;
  public const int OGE = 369;
  public const int OLT = 370;
  public const int OLE = 371;
  public const int ONE = 372;
  public const int ORD = 373;
  public const int UEQ = 374;
  public const int UNE = 375;
  public const int UNO = 376;
  public const int PHI = 377;
  public const int SELECT = 378;
  public const int CALL = 379;
  public const int TAIL = 380;
  public const int VA_ARG = 381;
  public const int LANDINGPAD = 382;
  public const int CATCHPAD = 383;
  public const int CLEANUPPAD = 384;
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
