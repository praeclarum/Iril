// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Repil/IR/IR.jay"
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

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
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' instructions '}'",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list parameter",
//t    "parameter : type",
//t    "function_addr : UNNAMED_ADDR",
//t    "function_addr : LOCAL_UNNAMED_ADDR",
//t    "attribute_group_refs : attribute_group_ref",
//t    "attribute_group_refs : attribute_group_refs attribute_group_ref",
//t    "attribute_group_ref : ATTRIBUTE_GROUP_REF",
//t    "instructions : instruction",
//t    "instructions : instructions instruction",
//t    "instruction : terminal_instruction",
//t    "instruction : assign_instruction",
//t    "terminal_instruction : RET",
//t    "terminal_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminal_instruction : BR label_value",
//t    "assign_instruction : LOCAL_SYMBOL '=' ICMP icmp_condition type value ',' value",
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
//t    "label_value : LABEL LOCAL_SYMBOL",
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
    null,null,null,null,null,null,null,null,"'='",null,null,null,null,
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
    null,null,null,null,null,null,null,null,null,null,"INTEGER","STRING",
    "TRUE","FALSE","NULL","LABEL","SOURCE_FILENAME","TARGET","DATALAYOUT",
    "TRIPLE","GLOBAL_SYMBOL","LOCAL_SYMBOL","TYPE","HALF","FLOAT",
    "DOUBLE","I1","I8","I16","I32","I64","DEFINE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","ATTRIBUTE_GROUP_REF","RET","BR","SWITCH",
    "INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET",
    "UNREACHABLE","FNEG","ADD","FADD","SUB","FSUB","MUL","FMUL","UDIV",
    "SDIV","FDIV","UREM","SREM","FREM","SHL","LSHR","ASHR","AND","OR",
    "XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","TRUNC","ZEXT","SEXT","FPTRUNC","FPEXT","FPTOUI",
    "FPTOSI","UITOFP","SITOFP","PTRTOINT","INTTOPTR","BITCAST",
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
#line 54 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 58 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 62 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 66 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 9:
  case_9();
  break;
case 10:
#line 82 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 11:
#line 86 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 13:
#line 91 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 14:
#line 92 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 15:
#line 93 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 16:
#line 94 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 17:
#line 95 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 18:
#line 96 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 19:
#line 97 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 20:
#line 101 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ();
    }
  break;
case 21:
#line 105 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 22:
#line 109 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 23:
#line 116 "Repil/IR/IR.jay"
  {
        var f = new FunctionDefinition ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-9+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Instruction>)yyVals[-1+yyTop]);
    }
  break;
case 39:
#line 162 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((LocalSymbol)yyVals[-7+yyTop], (IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 40:
#line 166 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 41:
#line 167 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 42:
#line 168 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 43:
#line 169 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 44:
#line 170 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 45:
#line 171 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 46:
#line 172 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 47:
#line 173 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 48:
#line 174 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 49:
#line 175 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 51:
#line 180 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 52:
#line 184 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 53:
#line 191 "Repil/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
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
#line 72 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ();
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    3,    5,
    5,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    4,    7,    7,   11,    8,    8,    9,    9,
   12,   10,   10,   13,   13,   14,   14,   14,   15,   18,
   18,   18,   18,   18,   18,   18,   18,   18,   18,   16,
   16,   19,   17,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    3,    1,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    4,
    2,    1,   11,    1,    2,    1,    1,    1,    1,    2,
    1,    1,    2,    1,    1,    1,    7,    2,    8,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    2,    8,    0,    0,
    0,    0,   22,   13,   14,   15,   16,   17,   18,   19,
    0,   12,    0,    3,    4,    0,    0,    0,    0,    0,
    0,    0,   21,    5,    6,    7,    9,    0,    0,    0,
    0,    0,    0,   24,   20,    0,   25,   27,   28,    0,
   31,    0,   29,    0,   30,    0,   36,    0,    0,   32,
   34,   35,    0,    0,    0,   38,   23,   33,    0,   53,
   52,   51,    0,   50,   40,   41,   42,   43,   44,   45,
   46,   47,   48,   49,    0,    0,    0,    0,    0,    0,
    0,   37,   39,
  };
  protected static readonly short [] yyDgoto  = {             5,
    6,    7,   22,    8,   29,   30,   43,   50,   52,   59,
   44,   53,   60,   61,   62,   73,   66,   85,   74,
  };
  protected static readonly short [] yySindex = {         -245,
  -53, -228,  -45, -106,    0, -245,    0,    0, -234,  -18,
  -17, -224,    0,    0,    0,    0,    0,    0,    0,    0,
 -106,    0,  -39,    0,    0, -201, -200,  -64,  -29,   -6,
   20, -106,    0,    0,    0,    0,    0, -106, -106,   -9,
   -6,   -6,  -41,    0,    0, -240,    0,    0,    0, -220,
    0, -118,    0, -255,    0,    1,    0, -242, -123,    0,
    0,    0, -273, -204, -239,    0,    0,    0, -290,    0,
    0,    0,   21,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, -106, -196,  -36,   23,   24, -196,
 -239,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,   69,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -32,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  -30,  -31,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,   64,   43,    0,   40,    3,    0,    0,    0,    0,
   30,   22,   16,    0,    0,  -66,  -60,    0,    0,
  };
  protected static readonly short [] yyTable = {            46,
   32,   67,   33,   32,   54,   33,   23,    9,   10,   26,
   11,   10,   56,   11,   38,   12,   21,    1,    2,   64,
   89,   71,    3,   25,   93,   88,   57,   58,   72,   92,
   65,   45,    4,   32,   38,   33,   10,   11,   48,   49,
   41,   42,   26,   27,   28,   42,   75,   76,   77,   78,
   79,   80,   81,   82,   83,   84,   34,   35,   21,   39,
   51,   63,   69,   70,   86,   64,   90,   91,    1,   24,
   36,   40,   47,   55,   68,    0,    0,    0,    0,    0,
    0,   21,    0,    0,    0,    0,    0,   87,    0,    0,
    0,   26,   10,    0,   11,   37,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   56,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   57,   58,
    0,   13,   51,   14,   15,   16,    0,   17,   18,   19,
   20,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   71,    0,   13,   31,   14,   15,
   16,   72,   17,   18,   19,   20,   26,    0,   26,   26,
   26,    0,   26,   26,   26,   26,
  };
  protected static readonly short [] yyCheck = {            41,
   40,  125,   42,   40,  123,   42,    4,   61,   41,   41,
   41,   44,  268,   44,   44,   61,  123,  263,  264,  262,
   87,  261,  268,  258,   91,   86,  282,  283,  268,   90,
  273,   41,  278,   40,   44,   42,  265,  266,  279,  280,
   38,   39,   61,   61,  269,   43,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,  258,  258,  123,   40,
  281,   61,  336,  268,   44,  262,   44,   44,    0,    6,
   28,   32,   43,   52,   59,   -1,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   85,   -1,   -1,
   -1,  123,  125,   -1,  125,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  268,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
   -1,  268,  281,  270,  271,  272,   -1,  274,  275,  276,
  277,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  261,   -1,  268,  267,  270,  271,
  272,  268,  274,  275,  276,  277,  268,   -1,  270,  271,
  272,   -1,  274,  275,  276,  277,
  };

#line 195 "Repil/IR/IR.jay"

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
  public const int STRING = 258;
  public const int TRUE = 259;
  public const int FALSE = 260;
  public const int NULL = 261;
  public const int LABEL = 262;
  public const int SOURCE_FILENAME = 263;
  public const int TARGET = 264;
  public const int DATALAYOUT = 265;
  public const int TRIPLE = 266;
  public const int GLOBAL_SYMBOL = 267;
  public const int LOCAL_SYMBOL = 268;
  public const int TYPE = 269;
  public const int HALF = 270;
  public const int FLOAT = 271;
  public const int DOUBLE = 272;
  public const int I1 = 273;
  public const int I8 = 274;
  public const int I16 = 275;
  public const int I32 = 276;
  public const int I64 = 277;
  public const int DEFINE = 278;
  public const int UNNAMED_ADDR = 279;
  public const int LOCAL_UNNAMED_ADDR = 280;
  public const int ATTRIBUTE_GROUP_REF = 281;
  public const int RET = 282;
  public const int BR = 283;
  public const int SWITCH = 284;
  public const int INDIRECTBR = 285;
  public const int INVOKE = 286;
  public const int RESUME = 287;
  public const int CATCHSWITCH = 288;
  public const int CATCHRET = 289;
  public const int CLEANUPRET = 290;
  public const int UNREACHABLE = 291;
  public const int FNEG = 292;
  public const int ADD = 293;
  public const int FADD = 294;
  public const int SUB = 295;
  public const int FSUB = 296;
  public const int MUL = 297;
  public const int FMUL = 298;
  public const int UDIV = 299;
  public const int SDIV = 300;
  public const int FDIV = 301;
  public const int UREM = 302;
  public const int SREM = 303;
  public const int FREM = 304;
  public const int SHL = 305;
  public const int LSHR = 306;
  public const int ASHR = 307;
  public const int AND = 308;
  public const int OR = 309;
  public const int XOR = 310;
  public const int EXTRACTELEMENT = 311;
  public const int INSERTELEMENT = 312;
  public const int SHUFFLEVECTOR = 313;
  public const int EXTRACTVALUE = 314;
  public const int INSERTVALUE = 315;
  public const int ALLOCA = 316;
  public const int LOAD = 317;
  public const int STORE = 318;
  public const int FENCE = 319;
  public const int CMPXCHG = 320;
  public const int ATOMICRMW = 321;
  public const int GETELEMENTPTR = 322;
  public const int TRUNC = 323;
  public const int ZEXT = 324;
  public const int SEXT = 325;
  public const int FPTRUNC = 326;
  public const int FPEXT = 327;
  public const int FPTOUI = 328;
  public const int FPTOSI = 329;
  public const int UITOFP = 330;
  public const int SITOFP = 331;
  public const int PTRTOINT = 332;
  public const int INTTOPTR = 333;
  public const int BITCAST = 334;
  public const int ADDRSPACECAST = 335;
  public const int ICMP = 336;
  public const int EQ = 337;
  public const int NE = 338;
  public const int UGT = 339;
  public const int UGE = 340;
  public const int ULT = 341;
  public const int ULE = 342;
  public const int SGT = 343;
  public const int SGE = 344;
  public const int SLT = 345;
  public const int SLE = 346;
  public const int FCMP = 347;
  public const int OEQ = 348;
  public const int OGT = 349;
  public const int OGE = 350;
  public const int OLT = 351;
  public const int OLE = 352;
  public const int ONE = 353;
  public const int ORD = 354;
  public const int UEQ = 355;
  public const int UNE = 356;
  public const int UNO = 357;
  public const int PHI = 358;
  public const int SELECT = 359;
  public const int CALL = 360;
  public const int VA_ARG = 361;
  public const int LANDINGPAD = 362;
  public const int CATCHPAD = 363;
  public const int CLEANUPPAD = 364;
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
