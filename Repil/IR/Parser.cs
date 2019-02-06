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
//t    "function_arg : type NONNULL value",
//t    "phi_vals : phi_val",
//t    "phi_vals : phi_vals ',' phi_val",
//t    "phi_val : '[' value ',' value ']'",
//t    "instruction : RET",
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
    "FLOAT_LITERAL","STRING","TRUE","FALSE","UNDEF","VOID","NULL",
    "NONNULL","LABEL","X","SOURCE_FILENAME","TARGET","DATALAYOUT",
    "TRIPLE","GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","TYPE","HALF",
    "FLOAT","DOUBLE","I1","I8","I16","I32","I64","DEFINE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","ATTRIBUTE_GROUP_REF","RET","BR","SWITCH",
    "INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET",
    "UNREACHABLE","FNEG","ADD","FADD","SUB","FSUB","MUL","FMUL","UDIV",
    "SDIV","FDIV","UREM","SREM","FREM","SHL","LSHR","ASHR","AND","OR",
    "XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
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
  { yyVal = VoidType.Void; }
  break;
case 14:
#line 93 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 15:
#line 94 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 16:
#line 95 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 17:
#line 96 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
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
#line 99 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 21:
#line 100 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 22:
#line 104 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ();
    }
  break;
case 23:
#line 108 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 24:
#line 112 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 25:
#line 116 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 26:
#line 123 "Repil/IR/IR.jay"
  {
        var f = new FunctionDefinition ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-9+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Assignment>)yyVals[-1+yyTop]);
    }
  break;
case 35:
#line 150 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 36:
#line 151 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 37:
#line 152 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 38:
#line 153 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 39:
#line 154 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 40:
#line 155 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 41:
#line 156 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 42:
#line 157 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 43:
#line 158 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 44:
#line 159 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 46:
#line 164 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 47:
#line 165 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 48:
#line 169 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 49:
#line 170 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 50:
#line 171 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 51:
#line 172 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 52:
#line 173 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 53:
#line 177 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 54:
#line 184 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 55:
#line 191 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 56:
#line 198 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 57:
#line 205 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 58:
#line 209 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 60:
#line 220 "Repil/IR/IR.jay"
  {
        yyVal = (int)(BigInteger)yyVals[0+yyTop];
    }
  break;
case 61:
#line 227 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((int)yyVals[0+yyTop]);
    }
  break;
case 62:
#line 231 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (int)yyVals[0+yyTop]);
    }
  break;
case 63:
#line 238 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 64:
#line 242 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 65:
#line 249 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 253 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 264 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 69:
#line 268 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 275 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 71:
#line 279 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 72:
#line 286 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 290 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 74:
#line 296 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 76:
#line 304 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 77:
#line 308 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 78:
#line 312 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 79:
#line 316 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 80:
#line 320 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 324 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<int>)yyVals[0+yyTop]);
    }
  break;
case 82:
#line 328 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 332 "Repil/IR/IR.jay"
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
    6,    6,    6,    6,    6,    4,    7,    7,   11,    8,
    8,    9,    9,   12,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   13,   14,   14,   14,   15,   15,   15,
   15,   15,   15,   17,   18,   19,   16,   16,   20,   21,
   22,   22,   10,   10,   23,   23,   25,   26,   26,   27,
   27,   28,   28,   29,   24,   24,   24,   24,   24,   24,
   24,   24,   24,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    3,    1,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    4,    2,    1,    5,   11,    1,    2,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    3,    2,    2,    2,    1,    3,    2,    2,
    1,    3,    1,    2,    1,    3,    1,    1,    3,    2,
    3,    1,    3,    5,    1,    7,    2,    9,    6,    4,
    7,    6,    3,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    2,    8,    0,    0,
    0,    0,   13,   24,   14,   15,   16,   17,   18,   19,
   20,   21,    0,    0,   12,    0,    3,    4,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    5,    6,    7,
    9,    0,    0,    0,    0,    0,    0,    0,    0,   27,
   22,   25,    0,   28,   30,   31,    0,   34,    0,   32,
    0,   33,    0,   75,    0,    0,    0,    0,    0,    0,
    0,    0,   63,   65,    0,    0,    0,   77,    0,    0,
    0,    0,   35,   36,   37,   38,   39,   40,   41,   42,
   43,   44,    0,    0,    0,   26,   64,   66,   54,   50,
   49,   51,   52,   48,   47,   46,    0,    0,   45,   55,
    0,    0,    0,    0,    0,    0,   72,   67,    0,    0,
    0,   57,    0,    0,    0,    0,    0,    0,    0,    0,
   56,    0,   53,    0,    0,    0,    0,    0,   73,    0,
    0,   68,   58,    0,    0,    0,   79,    0,    0,   70,
    0,   82,   76,    0,    0,   61,    0,   74,   71,   69,
    0,   60,    0,    0,   78,   62,   59,
  };
  protected static readonly short [] yyDgoto  = {             5,
    6,    7,   25,    8,   32,   79,   49,   57,   59,   72,
   50,   60,   93,  108,  109,  121,   78,   80,  122,  165,
  156,  157,   73,   74,  119,  141,  142,  116,  117,
  };
  protected static readonly short [] yySindex = {         -235,
  -60, -240,  -15,   35,    0, -235,    0,    0, -207,   -3,
    5, -202,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   35, -182,    0,  -38,    0,    0, -180, -179,
  -46,  -31,   11, -186,   43,   35,    0,    0,    0,    0,
    0,   35,   35,   35,   -4,   11,  -14,   11,  -16,    0,
    0,    0, -215,    0,    0,    0, -203,    0, -113,    0,
 -268,    0,   25,    0, -234,   35, -242,   35, -172,   35,
   35, -125,    0,    0, -281, -183,   62,    0,   32,   47,
   35, -244,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   35,  -24,   32,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   35,   52,    0,    0,
   35,   17,   35,   32,   62,   53,    0,    0,   58,  -25,
  -21,    0, -167,   57,   35,   11,   65,   66,   15,   35,
    0,   35,    0,   67, -217,   69,   62,   62,    0,   22,
   -2,    0,    0, -167, -143,   35,    0,   23,   62,    0,
   35,    0,    0,   71,  -28,    0,   73,    0,    0,    0,
 -155,    0,   35, -154,    0,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,  121,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -22,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -17,    0,   -5,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, -122,    0,    0,    0,    0,
    0,    0,    0,    0,    0, -119,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, -116,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  117,   93,    0,   89,  302,    0,    0,    0,    0,
   77,   70,    0,  -10,   12,    0,  -94,  -57,   -1,    0,
  -30,    0,   63,   59,    0,    0,  -13,    0,    7,
  };
  protected static readonly short [] yyTable = {            96,
    9,   36,   83,   37,   63,   80,   64,   65,   81,   61,
   82,   36,   42,   37,   36,   36,   37,   37,   10,   64,
   65,   10,  132,   11,   53,   36,   11,   37,  134,   10,
   11,   76,    1,    2,  107,   29,   51,    3,  152,   42,
  133,  151,   66,   24,   77,   12,   67,   52,    4,  153,
   36,   28,   37,  124,   29,   66,   36,   29,   37,   67,
  125,   36,   68,   37,   69,   30,  115,  136,  110,   55,
   56,   36,   31,   37,   34,   68,   23,   69,   38,   39,
   43,  107,   44,   58,  118,   75,   70,   81,   71,   99,
  111,  107,  113,   41,   24,  123,  129,  130,   76,   70,
  135,   71,   10,  127,  128,  115,   23,   11,  137,  138,
  144,  145,  146,  154,  161,  158,  163,   29,  164,  167,
    1,  107,   27,   40,   45,   54,  147,  148,   62,  150,
  143,  131,  166,   98,   97,  139,    0,  160,  159,    0,
    0,    0,    0,    0,    0,    0,    0,   63,    0,    0,
   83,    0,    0,   80,    0,    0,   81,   23,    0,    0,
    0,    0,   64,   65,    0,   83,   83,    0,   80,   80,
    0,   81,   81,   58,   83,   84,   85,   86,   87,   88,
   89,   90,   91,   92,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   66,    0,
    0,   83,   67,    0,   80,   83,    0,   81,   80,    0,
    0,   81,    0,    0,    0,    0,    0,    0,   68,    0,
   69,   83,    0,   83,   80,    0,   80,   81,  162,   81,
    0,  100,  101,   35,  102,  103,    0,    0,  104,    0,
    0,    0,   70,    0,   71,   83,   13,   83,   80,    0,
   80,   81,    0,   81,    0,    0,   14,   29,    0,   15,
   16,   17,   18,   19,   20,   21,   22,   29,    0,    0,
   29,   29,   29,   29,   29,   29,   29,   29,  100,  101,
    0,  102,  103,    0,    0,  104,  149,    0,  100,  101,
    0,  102,  103,  105,  106,  104,    0,   13,    0,    0,
    0,    0,    0,  105,  106,   26,    0,   14,    0,    0,
   15,   16,   17,   18,   19,   20,   21,   22,  100,  101,
    0,  102,  103,    0,   33,  104,    0,    0,    0,    0,
    0,    0,    0,  105,  106,    0,    0,   33,    0,    0,
    0,    0,    0,   46,   47,   48,    0,    0,    0,    0,
   48,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   94,   95,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  112,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  114,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  120,    0,
    0,    0,    0,    0,  126,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  140,    0,  120,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  155,    0,    0,
    0,    0,  140,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  155,
  };
  protected static readonly short [] yyCheck = {           125,
   61,   40,  125,   42,  273,  125,  288,  289,  125,  123,
   68,   40,   44,   42,   40,   40,   42,   42,   41,  288,
  289,   44,   44,   41,   41,   40,   44,   42,  123,  270,
  271,  266,  268,  269,   60,   41,   41,  273,   41,   44,
   62,   44,  324,   60,  279,   61,  328,   62,  284,  144,
   40,  259,   42,  111,   60,  324,   40,   61,   42,  328,
   44,   40,  344,   42,  346,   61,   91,  125,   79,  285,
  286,   40,  275,   42,  257,  344,  123,  346,  259,  259,
  267,   60,   40,  287,   95,   61,  368,  330,  370,  273,
   44,   60,  337,  125,   60,   44,   44,   40,  266,  368,
   44,  370,  125,  114,  115,   91,  123,  125,   44,   44,
   44,  329,   44,  257,   44,   93,   44,  123,  274,  274,
    0,   60,    6,   31,   36,   49,  137,  138,   59,  140,
  132,  120,  163,   75,   72,  129,   -1,  151,  149,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
  273,   -1,   -1,  273,   -1,   -1,  273,  123,   -1,   -1,
   -1,   -1,  288,  289,   -1,  288,  289,   -1,  288,  289,
   -1,  288,  289,  287,  347,  348,  349,  350,  351,  352,
  353,  354,  355,  356,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  324,   -1,
   -1,  324,  328,   -1,  324,  328,   -1,  324,  328,   -1,
   -1,  328,   -1,   -1,   -1,   -1,   -1,   -1,  344,   -1,
  346,  344,   -1,  346,  344,   -1,  346,  344,  257,  346,
   -1,  257,  258,  272,  260,  261,   -1,   -1,  264,   -1,
   -1,   -1,  368,   -1,  370,  368,  263,  370,  368,   -1,
  370,  368,   -1,  370,   -1,   -1,  273,  263,   -1,  276,
  277,  278,  279,  280,  281,  282,  283,  273,   -1,   -1,
  276,  277,  278,  279,  280,  281,  282,  283,  257,  258,
   -1,  260,  261,   -1,   -1,  264,  265,   -1,  257,  258,
   -1,  260,  261,  272,  273,  264,   -1,  263,   -1,   -1,
   -1,   -1,   -1,  272,  273,    4,   -1,  273,   -1,   -1,
  276,  277,  278,  279,  280,  281,  282,  283,  257,  258,
   -1,  260,  261,   -1,   23,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   36,   -1,   -1,
   -1,   -1,   -1,   42,   43,   44,   -1,   -1,   -1,   -1,
   49,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   70,   71,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   81,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   93,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  107,   -1,
   -1,   -1,   -1,   -1,  113,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  130,   -1,  132,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  146,   -1,   -1,
   -1,   -1,  151,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  163,
  };

#line 336 "Repil/IR/IR.jay"

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
  public const int NONNULL = 265;
  public const int LABEL = 266;
  public const int X = 267;
  public const int SOURCE_FILENAME = 268;
  public const int TARGET = 269;
  public const int DATALAYOUT = 270;
  public const int TRIPLE = 271;
  public const int GLOBAL_SYMBOL = 272;
  public const int LOCAL_SYMBOL = 273;
  public const int META_SYMBOL = 274;
  public const int TYPE = 275;
  public const int HALF = 276;
  public const int FLOAT = 277;
  public const int DOUBLE = 278;
  public const int I1 = 279;
  public const int I8 = 280;
  public const int I16 = 281;
  public const int I32 = 282;
  public const int I64 = 283;
  public const int DEFINE = 284;
  public const int UNNAMED_ADDR = 285;
  public const int LOCAL_UNNAMED_ADDR = 286;
  public const int ATTRIBUTE_GROUP_REF = 287;
  public const int RET = 288;
  public const int BR = 289;
  public const int SWITCH = 290;
  public const int INDIRECTBR = 291;
  public const int INVOKE = 292;
  public const int RESUME = 293;
  public const int CATCHSWITCH = 294;
  public const int CATCHRET = 295;
  public const int CLEANUPRET = 296;
  public const int UNREACHABLE = 297;
  public const int FNEG = 298;
  public const int ADD = 299;
  public const int FADD = 300;
  public const int SUB = 301;
  public const int FSUB = 302;
  public const int MUL = 303;
  public const int FMUL = 304;
  public const int UDIV = 305;
  public const int SDIV = 306;
  public const int FDIV = 307;
  public const int UREM = 308;
  public const int SREM = 309;
  public const int FREM = 310;
  public const int SHL = 311;
  public const int LSHR = 312;
  public const int ASHR = 313;
  public const int AND = 314;
  public const int OR = 315;
  public const int XOR = 316;
  public const int EXTRACTELEMENT = 317;
  public const int INSERTELEMENT = 318;
  public const int SHUFFLEVECTOR = 319;
  public const int EXTRACTVALUE = 320;
  public const int INSERTVALUE = 321;
  public const int ALLOCA = 322;
  public const int LOAD = 323;
  public const int STORE = 324;
  public const int FENCE = 325;
  public const int CMPXCHG = 326;
  public const int ATOMICRMW = 327;
  public const int GETELEMENTPTR = 328;
  public const int ALIGN = 329;
  public const int INBOUNDS = 330;
  public const int INRANGE = 331;
  public const int TRUNC = 332;
  public const int ZEXT = 333;
  public const int SEXT = 334;
  public const int FPTRUNC = 335;
  public const int FPEXT = 336;
  public const int TO = 337;
  public const int FPTOUI = 338;
  public const int FPTOSI = 339;
  public const int UITOFP = 340;
  public const int SITOFP = 341;
  public const int PTRTOINT = 342;
  public const int INTTOPTR = 343;
  public const int BITCAST = 344;
  public const int ADDRSPACECAST = 345;
  public const int ICMP = 346;
  public const int EQ = 347;
  public const int NE = 348;
  public const int UGT = 349;
  public const int UGE = 350;
  public const int ULT = 351;
  public const int ULE = 352;
  public const int SGT = 353;
  public const int SGE = 354;
  public const int SLT = 355;
  public const int SLE = 356;
  public const int FCMP = 357;
  public const int OEQ = 358;
  public const int OGT = 359;
  public const int OGE = 360;
  public const int OLT = 361;
  public const int OLE = 362;
  public const int ONE = 363;
  public const int ORD = 364;
  public const int UEQ = 365;
  public const int UNE = 366;
  public const int UNO = 367;
  public const int PHI = 368;
  public const int SELECT = 369;
  public const int CALL = 370;
  public const int VA_ARG = 371;
  public const int LANDINGPAD = 372;
  public const int CATCHPAD = 373;
  public const int CLEANUPPAD = 374;
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
