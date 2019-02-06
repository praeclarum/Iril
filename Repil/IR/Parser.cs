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

  protected const int yyFinal = 6;
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
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' assignments '}'",
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
//t    "assignments : assignment",
//t    "assignments : assignments assignment",
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
//t    "instruction : RET typed_value",
//t    "instruction : BR I1 value ',' label_value ',' label_value",
//t    "instruction : BR label_value",
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER ',' tbaa",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : CALL type function_pointer '(' function_args ')'",
//t    "instruction : PHI type phi_vals",
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
    null,null,null,null,null,null,null,null,null,null,null,"'['",null,
    "']'",null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,"'{'",null,"'}'",null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,"INTEGER",
    "FLOAT_LITERAL","STRING","TRUE","FALSE","UNDEF","VOID","NULL","LABEL",
    "X","SOURCE_FILENAME","TARGET","DATALAYOUT","TRIPLE","GLOBAL_SYMBOL",
    "LOCAL_SYMBOL","META_SYMBOL","TYPE","HALF","FLOAT","DOUBLE","I1","I8",
    "I16","I32","I64","DEFINE","DECLARE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","NONNULL","NOCAPTURE","WRITEONLY",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","RET","BR","SWITCH","INDIRECTBR",
    "INVOKE","RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE",
    "FNEG","ADD","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV",
    "UREM","SREM","FREM","SHL","LSHR","ASHR","AND","OR","XOR",
    "EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT",
    "FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT",
    "INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE",
    "ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ","OGT","OGE","OLT",
    "OLE","ONE","ORD","UEQ","UNE","UNO","PHI","SELECT","CALL","VA_ARG",
    "LANDINGPAD","CATCHPAD","CLEANUPPAD",
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
              Console.WriteLine(String.Format ("syntax error, got token `{0}' expecting {1}",
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
case 10:
  case_10();
  break;
case 11:
#line 93 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 12:
#line 97 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 14:
#line 102 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 15:
#line 103 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 16:
#line 104 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 17:
#line 105 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 18:
#line 106 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 19:
#line 107 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 20:
#line 108 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 21:
#line 109 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 22:
#line 110 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 23:
#line 114 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ();
    }
  break;
case 24:
#line 118 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 25:
#line 122 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 26:
#line 126 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 27:
#line 133 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Assignment>)yyVals[-1+yyTop]);
    }
  break;
case 28:
#line 140 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 29:
#line 147 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 30:
#line 151 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 31:
#line 158 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter ((LType)yyVals[0+yyTop]);
    }
  break;
case 32:
#line 162 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter ((LType)yyVals[-1+yyTop]);
    }
  break;
case 34:
#line 170 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 35:
#line 174 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 36:
#line 175 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 37:
#line 176 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 43:
#line 194 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 44:
#line 195 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 45:
#line 196 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 46:
#line 197 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 47:
#line 198 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 48:
#line 199 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 49:
#line 200 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 50:
#line 201 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 51:
#line 202 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 52:
#line 203 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 54:
#line 208 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 55:
#line 209 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 56:
#line 213 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 57:
#line 214 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 58:
#line 215 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 59:
#line 216 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 60:
#line 217 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 61:
#line 221 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 62:
#line 228 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 63:
#line 235 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 64:
#line 242 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 65:
#line 249 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 253 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 264 "Repil/IR/IR.jay"
  {
        yyVal = (int)(BigInteger)yyVals[0+yyTop];
    }
  break;
case 69:
#line 271 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((int)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 275 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (int)yyVals[0+yyTop]);
    }
  break;
case 71:
#line 282 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 72:
#line 286 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 293 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 74:
#line 297 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 76:
#line 308 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 77:
#line 312 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 78:
#line 319 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 79:
#line 323 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 80:
#line 330 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 334 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 82:
#line 340 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 347 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 351 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 355 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 359 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 87:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 367 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 89:
#line 371 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<int>)yyVals[0+yyTop]);
    }
  break;
case 90:
#line 375 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 379 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
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

void case_10()
#line 83 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ();
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    3,
    6,    6,    7,    7,    7,    7,    7,    7,    7,    7,
    7,    7,    7,    7,    7,    7,    4,    5,    8,    8,
   12,   12,   13,   13,   14,   14,   14,    9,    9,   10,
   10,   15,   16,   16,   16,   16,   16,   16,   16,   16,
   16,   16,   17,   17,   17,   18,   18,   18,   18,   18,
   18,   20,   21,   22,   19,   19,   23,   24,   25,   25,
   11,   11,   26,   26,   28,   29,   29,   30,   30,   31,
   31,   32,   27,   27,   27,   27,   27,   27,   27,   27,
   27,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    3,
    1,    3,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    4,    2,    1,    5,   11,    7,    1,    3,
    1,    2,    1,    2,    1,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    3,    2,    2,    2,    1,    3,    2,    2,    1,    3,
    1,    2,    1,    3,    1,    1,    3,    2,    3,    1,
    3,    5,    2,    7,    2,    9,    6,    4,    7,    6,
    3,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    2,    8,    9,
    0,    0,    0,    0,   14,   25,   15,   16,   17,   18,
   19,   20,   21,   22,    0,    0,   13,    0,    0,    3,
    4,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    5,    6,    7,   10,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   29,   23,    0,   26,   35,   36,
   37,    0,   33,    0,    0,    0,   34,   30,   38,   39,
    0,   42,    0,   40,    0,   41,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   71,   73,    0,
    0,   83,    0,    0,   85,    0,    0,    0,   43,   44,
   45,   46,   47,   48,   49,   50,   51,   52,    0,    0,
    0,   27,   72,   74,   58,   57,   59,   60,   56,   55,
   54,    0,   63,   53,   62,    0,    0,    0,    0,    0,
    0,    0,   80,   75,    0,    0,    0,   65,    0,    0,
    0,    0,    0,    0,    0,    0,   64,    0,   61,    0,
    0,    0,    0,    0,   81,    0,    0,   76,   66,    0,
    0,    0,   87,    0,    0,   78,    0,   90,   84,    0,
    0,   69,    0,   82,   79,   77,    0,   68,    0,    0,
   86,   70,   67,
  };
  protected static readonly short [] yyDgoto  = {             6,
    7,    8,   27,    9,   10,   35,   91,   54,   71,   73,
   87,   55,   62,   63,   74,  109,  123,  124,  137,   95,
   92,  138,  181,  172,  173,   88,   89,  135,  157,  158,
  132,  133,
  };
  protected static readonly short [] yySindex = {         -230,
  -57, -222,  -26,   65,   65,    0, -230,    0,    0,    0,
 -218,   -6,   27, -166,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   65, -146,    0,  -35,  -32,    0,
    0, -140, -137,    5,  -30,   37, -136,   93,   65,    0,
   94,    0,    0,    0,    0,   65,   65,   65,    2,   65,
   37,  -13,  -25,   15,    0,    0,   34,    0,    0,    0,
    0, -171,    0,   65, -181, -159,    0,    0,    0,    0,
 -159,    0, -159,    0, -117,    0, -248,   74,   65, -252,
   65, -198,   65, -290,   65,   65, -122,    0,    0, -235,
   31,    0, -134,   91,    0,   96,   65, -200,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   65,  -19,
   31,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   65,    0,    0,    0,  101,   65,   -8,   65,   31,
   91,  102,    0,    0,  107,   52,  -11,    0, -116,  104,
   65,   37,  108,  109,   63,   65,    0,   65,    0,  111,
 -177,  114,   91,   91,    0,  -40,   45,    0,    0, -116,
  -97,   65,    0,   68,   47,    0,   65,    0,    0,  118,
  -24,    0,  119,    0,    0,    0, -106,    0,   65, -104,
    0,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,  172,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -22,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  -16,    0,   46,    0,    0,    0,    0,    0,    0,    0,
    0,   57,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    1,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, -113,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -94,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -49,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  167,  141,    0,    0,  137,   35,  132,    0,  113,
    0,  121,   30,  -51,   54,    0,   12,   51,    0, -130,
  -31,   41,    0,   11,    0,  105,  103,    0,    0,   24,
    0,   49,
  };
  protected static readonly short [] yyTable = {            39,
   28,   40,  112,   11,   39,   77,   40,   39,  150,   40,
   67,   91,   93,   46,   39,   39,   40,   40,   11,  122,
   39,   11,   40,   78,   12,   94,   39,   12,   40,  169,
   88,   39,  148,   40,   14,  141,    1,    2,   28,   29,
   31,    3,   56,   79,   80,   46,   12,   13,   58,   96,
  149,   98,    4,    5,   32,   65,   79,   80,   64,   36,
   99,  100,  101,  102,  103,  104,  105,  106,  107,  108,
   39,  131,   40,   36,   66,   89,   39,   64,   40,   81,
   51,   52,   53,   82,   53,  168,   31,   33,  167,   31,
  122,   39,   81,   40,   45,  140,   82,   32,   53,   83,
   32,   84,   11,   69,   70,  126,  122,   34,   12,  152,
   37,  122,   83,   67,   84,   59,   60,   61,   42,  110,
  111,   43,  134,   85,   26,   86,   76,   25,   76,   47,
   72,  128,   48,   50,   90,   97,   85,  125,   86,  127,
  129,  143,  144,  130,  139,  145,  146,  151,   93,   78,
  122,  153,  154,  131,  160,  161,  136,  162,   91,  170,
  174,  177,  179,  142,  163,  164,  180,  166,  183,   79,
   80,    1,   72,   30,   44,   49,  175,   88,   91,   91,
  156,   57,  136,   75,   68,  165,  147,   25,  159,  182,
  176,  113,  114,  155,    0,    0,  171,   88,   88,    0,
    0,  156,    0,    0,    0,   81,    0,    0,    0,   82,
    0,    0,    0,  171,   91,    0,  115,  116,   91,  117,
  118,    0,   89,  119,    0,   83,    0,   84,    0,    0,
  120,  121,  178,   88,   91,   38,   91,   88,   41,    0,
    0,    0,   89,   89,    0,    0,   59,   60,   61,   85,
    0,   86,    0,   88,    0,   88,    0,    0,   91,    0,
   91,   59,   60,   61,    0,    0,    0,   28,   28,    0,
    0,    0,   28,    0,    0,    0,    0,   88,   89,   88,
    0,    0,   89,   28,   28,    0,    0,  115,  116,    0,
  117,  118,    0,    0,  119,    0,    0,    0,   89,    0,
   89,  120,  121,  115,  116,    0,  117,  118,  115,  116,
  119,  117,  118,    0,    0,  119,    0,  120,  121,    0,
    0,    0,   89,    0,   89,    0,    0,   15,    0,    0,
    0,    0,    0,   59,   60,   61,   16,    0,    0,   17,
   18,   19,   20,   21,   22,   23,   24,  115,  116,    0,
  117,  118,    0,    0,  119,    0,    0,    0,    0,    0,
    0,  120,  121,
  };
  protected static readonly short [] yyCheck = {            40,
    0,   42,  125,   61,   40,  123,   42,   40,  139,   42,
   62,  125,  265,   44,   40,   40,   42,   42,   41,   60,
   40,   44,   42,  272,   41,  278,   40,   44,   42,  160,
  125,   40,   44,   42,   61,   44,  267,  268,    4,    5,
  259,  272,   41,  292,  293,   44,  269,  270,   62,   81,
   62,   83,  283,  284,   61,   41,  292,  293,   44,   25,
  351,  352,  353,  354,  355,  356,  357,  358,  359,  360,
   40,   91,   42,   39,   41,  125,   40,   44,   42,  328,
   46,   47,   48,  332,   50,   41,   41,   61,   44,   44,
   60,   40,  328,   42,  125,  127,  332,   41,   64,  348,
   44,  350,  125,  285,  286,   94,   60,  274,  125,  141,
  257,   60,  348,  165,  350,  287,  288,  289,  259,   85,
   86,  259,  111,  372,   60,  374,   73,  123,   75,  266,
  290,   97,   40,   40,   61,  334,  372,  272,  374,   44,
  341,  130,  131,  109,   44,   44,   40,   44,  265,  272,
   60,   44,   44,   91,   44,  333,  122,   44,  272,  257,
   93,   44,   44,  129,  153,  154,  273,  156,  273,  292,
  293,    0,  290,    7,   34,   39,  165,  272,  292,  293,
  146,   50,  148,   71,   64,  156,  136,  123,  148,  179,
  167,   87,   90,  145,   -1,   -1,  162,  292,  293,   -1,
   -1,  167,   -1,   -1,   -1,  328,   -1,   -1,   -1,  332,
   -1,   -1,   -1,  179,  328,   -1,  257,  258,  332,  260,
  261,   -1,  272,  264,   -1,  348,   -1,  350,   -1,   -1,
  271,  272,  257,  328,  348,  271,  350,  332,  271,   -1,
   -1,   -1,  292,  293,   -1,   -1,  287,  288,  289,  372,
   -1,  374,   -1,  348,   -1,  350,   -1,   -1,  372,   -1,
  374,  287,  288,  289,   -1,   -1,   -1,  267,  268,   -1,
   -1,   -1,  272,   -1,   -1,   -1,   -1,  372,  328,  374,
   -1,   -1,  332,  283,  284,   -1,   -1,  257,  258,   -1,
  260,  261,   -1,   -1,  264,   -1,   -1,   -1,  348,   -1,
  350,  271,  272,  257,  258,   -1,  260,  261,  257,  258,
  264,  260,  261,   -1,   -1,  264,   -1,  271,  272,   -1,
   -1,   -1,  372,   -1,  374,   -1,   -1,  263,   -1,   -1,
   -1,   -1,   -1,  287,  288,  289,  272,   -1,   -1,  275,
  276,  277,  278,  279,  280,  281,  282,  257,  258,   -1,
  260,  261,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,  271,  272,
  };

#line 383 "Repil/IR/IR.jay"

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
  public const int RET = 292;
  public const int BR = 293;
  public const int SWITCH = 294;
  public const int INDIRECTBR = 295;
  public const int INVOKE = 296;
  public const int RESUME = 297;
  public const int CATCHSWITCH = 298;
  public const int CATCHRET = 299;
  public const int CLEANUPRET = 300;
  public const int UNREACHABLE = 301;
  public const int FNEG = 302;
  public const int ADD = 303;
  public const int FADD = 304;
  public const int SUB = 305;
  public const int FSUB = 306;
  public const int MUL = 307;
  public const int FMUL = 308;
  public const int UDIV = 309;
  public const int SDIV = 310;
  public const int FDIV = 311;
  public const int UREM = 312;
  public const int SREM = 313;
  public const int FREM = 314;
  public const int SHL = 315;
  public const int LSHR = 316;
  public const int ASHR = 317;
  public const int AND = 318;
  public const int OR = 319;
  public const int XOR = 320;
  public const int EXTRACTELEMENT = 321;
  public const int INSERTELEMENT = 322;
  public const int SHUFFLEVECTOR = 323;
  public const int EXTRACTVALUE = 324;
  public const int INSERTVALUE = 325;
  public const int ALLOCA = 326;
  public const int LOAD = 327;
  public const int STORE = 328;
  public const int FENCE = 329;
  public const int CMPXCHG = 330;
  public const int ATOMICRMW = 331;
  public const int GETELEMENTPTR = 332;
  public const int ALIGN = 333;
  public const int INBOUNDS = 334;
  public const int INRANGE = 335;
  public const int TRUNC = 336;
  public const int ZEXT = 337;
  public const int SEXT = 338;
  public const int FPTRUNC = 339;
  public const int FPEXT = 340;
  public const int TO = 341;
  public const int FPTOUI = 342;
  public const int FPTOSI = 343;
  public const int UITOFP = 344;
  public const int SITOFP = 345;
  public const int PTRTOINT = 346;
  public const int INTTOPTR = 347;
  public const int BITCAST = 348;
  public const int ADDRSPACECAST = 349;
  public const int ICMP = 350;
  public const int EQ = 351;
  public const int NE = 352;
  public const int UGT = 353;
  public const int UGE = 354;
  public const int ULT = 355;
  public const int ULE = 356;
  public const int SGT = 357;
  public const int SGE = 358;
  public const int SLT = 359;
  public const int SLE = 360;
  public const int FCMP = 361;
  public const int OEQ = 362;
  public const int OGT = 363;
  public const int OGE = 364;
  public const int OLT = 365;
  public const int OLE = 366;
  public const int ONE = 367;
  public const int ORD = 368;
  public const int UEQ = 369;
  public const int UNE = 370;
  public const int UNO = 371;
  public const int PHI = 372;
  public const int SELECT = 373;
  public const int CALL = 374;
  public const int VA_ARG = 375;
  public const int LANDINGPAD = 376;
  public const int CATCHPAD = 377;
  public const int CLEANUPPAD = 378;
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
