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
//t    "module_part : META_SYMBOL '=' DISTINCT '!' '{' metadata '}'",
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
//t    "function_declaration : DECLARE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs",
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
//t    "fcmp_condition : TRUE",
//t    "fcmp_condition : FALSE",
//t    "fcmp_condition : ORD",
//t    "fcmp_condition : OEQ",
//t    "fcmp_condition : ONE",
//t    "fcmp_condition : OGT",
//t    "fcmp_condition : OGE",
//t    "fcmp_condition : OLT",
//t    "fcmp_condition : OLE",
//t    "fcmp_condition : UNO",
//t    "fcmp_condition : UEQ",
//t    "fcmp_condition : UNE",
//t    "fcmp_condition : UGT",
//t    "fcmp_condition : UGE",
//t    "fcmp_condition : ULT",
//t    "fcmp_condition : ULE",
//t    "value : constant",
//t    "value : LOCAL_SYMBOL",
//t    "value : GLOBAL_SYMBOL",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : UNDEF",
//t    "constant : '<' typed_constants '>'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_constant : type constant",
//t    "typed_constants : typed_constant",
//t    "typed_constants : typed_constants ',' typed_constant",
//t    "metadata_kv : META_SYMBOL META_SYMBOL",
//t    "metadata_kvs : metadata_kv",
//t    "metadata_kvs : metadata_kvs ',' metadata_kv",
//t    "element_index : typed_value",
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "blocks : block",
//t    "blocks : blocks block",
//t    "block : assignments terminator_assignment",
//t    "block : terminator_assignment",
//t    "assignments : assignment",
//t    "assignments : assignments assignment",
//t    "terminator_assignment : terminator_instruction",
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
//t    "switch_cases : switch_case",
//t    "switch_cases : switch_cases switch_case",
//t    "switch_case : typed_constant ',' label_value",
//t    "wrappings : wrapping",
//t    "wrappings : wrappings wrapping",
//t    "wrapping : NUW",
//t    "wrapping : NSW",
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminator_instruction : BR I1 value ',' label_value ',' label_value ',' META_SYMBOL META_SYMBOL",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "instruction : ADD type value ',' value",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL type function_pointer '(' function_args ')'",
//t    "instruction : CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FCMP fcmp_condition type value ',' value",
//t    "instruction : FMUL type value ',' value",
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
//t    "instruction : GETELEMENTPTR type ',' typed_value ',' element_indices",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : INSERTELEMENT typed_value ',' typed_value ',' typed_value",
//t    "instruction : LOAD type ',' typed_value ',' ALIGN INTEGER ',' metadata_kvs",
//t    "instruction : LSHR type value ',' value",
//t    "instruction : LSHR EXACT type value ',' value",
//t    "instruction : OR type value ',' value",
//t    "instruction : MUL wrappings type value ',' value",
//t    "instruction : PHI type phi_vals",
//t    "instruction : SDIV type value ',' value",
//t    "instruction : SELECT type value ',' typed_value ',' typed_value",
//t    "instruction : SEXT typed_value TO type",
//t    "instruction : SHL type value ',' value",
//t    "instruction : SHL wrappings type value ',' value",
//t    "instruction : SHUFFLEVECTOR typed_value ',' typed_value ',' typed_value",
//t    "instruction : SITOFP typed_value TO type",
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER ',' metadata_kvs",
//t    "instruction : SUB type value ',' value",
//t    "instruction : SUB wrappings type value ',' value",
//t    "instruction : TRUNC typed_value TO type",
//t    "instruction : UITOFP typed_value TO type",
//t    "instruction : XOR type value ',' value",
//t    "instruction : ZEXT typed_value TO type",
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
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","DISTINCT","TYPE","HALF",
    "FLOAT","DOUBLE","I1","I8","I16","I32","I64","DEFINE","DECLARE",
    "UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NONNULL","NOCAPTURE","WRITEONLY",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","SSP",
    "UWTABLE","ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR","INVOKE",
    "RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG",
    "ADD","NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV",
    "FDIV","UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR",
    "XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT",
    "FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT",
    "INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE",
    "ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ","OGT","OGE","OLT",
    "OLE","ONE","ORD","UEQ","UNE","UNO","PHI","SELECT","CALL","TAIL",
    "VA_ARG","LANDINGPAD","CATCHPAD","CLEANUPPAD",
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
case 26:
  case_26();
  break;
case 27:
#line 121 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 28:
#line 125 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 30:
#line 130 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 31:
#line 131 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 32:
#line 132 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 33:
#line 133 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 34:
#line 134 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 35:
#line 135 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 36:
#line 136 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 37:
#line 137 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 38:
#line 138 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 39:
#line 142 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 40:
#line 146 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 41:
#line 150 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 42:
#line 154 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 43:
#line 158 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 44:
#line 165 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 45:
#line 172 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 46:
#line 176 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 47:
#line 183 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 48:
#line 187 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 49:
#line 194 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 50:
#line 198 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 52:
#line 206 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 53:
#line 210 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 54:
#line 211 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 55:
#line 212 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 61:
#line 230 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 62:
#line 231 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 63:
#line 232 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 64:
#line 233 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 65:
#line 234 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 66:
#line 235 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 67:
#line 236 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 68:
#line 237 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 69:
#line 238 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 70:
#line 239 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 71:
#line 243 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 72:
#line 244 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 73:
#line 245 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 74:
#line 246 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 75:
#line 247 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 76:
#line 248 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 77:
#line 249 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 78:
#line 250 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 79:
#line 251 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 80:
#line 252 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 81:
#line 253 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 82:
#line 254 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 83:
#line 255 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 84:
#line 256 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 85:
#line 257 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 86:
#line 258 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 88:
#line 263 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 89:
#line 264 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 90:
#line 268 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 91:
#line 269 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 92:
#line 270 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 93:
#line 271 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 94:
#line 272 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 95:
#line 273 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 96:
#line 277 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 284 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 291 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 99:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 100:
#line 305 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 309 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 340 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 110:
  case_110();
  break;
case 111:
#line 357 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, NewList ((Assignment)yyVals[0+yyTop]));
    }
  break;
case 112:
#line 364 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 114:
#line 375 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 115:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 116:
#line 386 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 397 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 119:
#line 401 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 120:
#line 408 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 121:
#line 412 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 122:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 423 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 124:
#line 429 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 125:
#line 436 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 126:
#line 440 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 127:
#line 447 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 132:
#line 464 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 133:
#line 468 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 134:
#line 472 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-7+yyTop], (LabelValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop]);
    }
  break;
case 135:
#line 476 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 136:
#line 480 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 137:
#line 487 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 138:
#line 491 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 495 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 140:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 142:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 143:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 144:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 145:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 146:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new FloatAddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 527 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 531 "Repil/IR/IR.jay"
  {
        yyVal = new FloatMultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 547 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 551 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 555 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 559 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 156:
#line 563 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 157:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 158:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 587 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 591 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 169:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new ZextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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

void case_26()
#line 111 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

void case_110()
#line 349 "Repil/IR/IR.jay"
{
        var l = (List<Assignment>)yyVals[-1+yyTop];
        l.Add ((Assignment)yyVals[0+yyTop]);
        yyVal = new Block (LocalSymbol.None, l);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    6,    6,    8,    8,    8,    8,    8,    8,
    8,    7,    7,    9,    9,    3,   11,   11,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,    4,    5,    5,   13,   13,   17,   17,
   18,   18,   19,   19,   19,   14,   14,   15,   15,   20,
   21,   21,   21,   21,   21,   21,   21,   21,   21,   21,
   22,   22,   22,   22,   22,   22,   22,   22,   22,   22,
   22,   22,   22,   22,   22,   22,   23,   23,   23,   24,
   24,   24,   24,   24,   24,   24,   26,   10,   27,   25,
   25,   28,   29,   29,   30,   31,   31,   16,   16,   32,
   32,   33,   33,   34,   35,   35,   38,   39,   39,   40,
   40,   41,   41,   42,   43,   43,   44,   45,   45,   46,
   46,   36,   36,   36,   36,   36,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    6,
    6,    7,    1,    2,    1,    1,    1,    1,    1,    3,
    1,    1,    3,    1,    1,    3,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    4,    2,
    1,    5,    5,   11,    7,    8,    1,    3,    1,    2,
    1,    2,    1,    1,    1,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    3,    2,    2,    2,    1,
    3,    2,    1,    3,    1,    1,    3,    1,    2,    2,
    1,    1,    2,    1,    1,    3,    1,    1,    3,    2,
    3,    1,    3,    5,    1,    2,    3,    1,    2,    1,
    1,    2,    7,   10,    2,    7,    5,    6,    5,    5,
    4,    6,    7,    8,    4,    5,    6,    5,    4,    4,
    6,    7,    6,    6,    9,    5,    6,    5,    6,    3,
    5,    7,    4,    5,    6,    6,    4,    9,    5,    6,
    4,    4,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    2,
    8,    9,    0,    0,    0,    0,    0,   30,   41,   31,
   32,   33,   34,   35,   36,   37,   38,    0,    0,    0,
   29,    0,    0,    0,    3,    4,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   40,    0,    0,
    5,    6,    7,    0,    0,   26,    0,    0,    0,    0,
    0,    0,    0,    0,   25,    0,   22,   24,    0,    0,
    0,    0,    0,    0,   47,   39,    0,    0,   15,   16,
   17,   18,   19,    0,   13,    0,   11,    0,   92,   91,
   93,   94,   95,   90,   89,   88,    0,   98,   87,   42,
   43,   53,   54,   55,    0,   51,    0,    0,    0,    0,
   10,   14,   12,   23,    0,    0,  100,   52,   48,   56,
   57,    0,   60,    0,    0,   58,   20,   99,    0,   96,
    0,    0,   59,  101,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  108,    0,  111,  112,  114,  115,    0,  135,
    0,    0,  132,    0,  130,  131,    0,    0,  128,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   61,   62,
   63,   64,   65,   66,   67,   68,   69,   70,    0,   71,
   72,   83,   84,   85,   86,   74,   76,   77,   78,   79,
   75,   73,   81,   82,   80,    0,    0,    0,    0,    0,
   44,  109,  110,  113,  116,   97,    0,    0,    0,    0,
  129,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  122,    0,  117,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  145,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  137,    0,  146,  169,    0,    0,  148,  161,  164,
    0,    0,  156,  140,  158,  173,    0,    0,  139,    0,
    0,    0,    0,    0,    0,    0,  123,    0,    0,    0,
  118,    0,    0,    0,    0,  125,  138,  170,  159,  165,
  157,  154,  166,    0,    0,    0,  105,  106,    0,  153,
  147,    0,    0,    0,  120,    0,    0,    0,    0,    0,
  136,  126,    0,    0,    0,    0,  124,  162,  121,  119,
    0,    0,    0,  127,    0,    0,  107,    0,    0,    0,
  103,    0,    0,  134,  102,    0,  104,
  };
  protected static readonly short [] yyDgoto  = {             8,
    9,   10,   31,   11,   12,   84,   66,   85,   67,   68,
   42,   69,   74,  122,  125,  172,   75,  105,  106,  126,
  229,  246,  297,   99,  116,  183,  374,  421,  422,  388,
  389,  173,  174,  175,  176,  177,  178,  298,  370,  371,
  294,  295,  375,  376,  188,  189,
  };
  protected static readonly short [] yySindex = {         -177,
   -1, -220,   16,   28,  489,  489, -254,    0, -177,    0,
    0,    0, -203,   37,   53, -207,  -28,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  489, -126, -124,
    0,  -36,  -15,   73,    0,    0, -121, -104,   33,  118,
   35,  -32,   64, -109, -106,  121,  489,    0,  122,   45,
    0,    0,    0,   46,  423,    0,  489,  489,  489,  489,
    4,  489, -230,  423,    0,  -26,    0,    0,   70,   64,
   -4,  -14,  -27,   31,    0,    0,   56,  105,    0,    0,
    0,    0,    0,  -79,    0,  -24,    0,  423,    0,    0,
    0,    0,    0,    0,    0,    0,  489,    0,    0,    0,
    0,    0,    0,    0, -142,    0,  489, -159, -217,  -92,
    0,    0,    0,    0,  430,  -11,    0,    0,    0,    0,
    0, -120,    0, -120, -120,    0,    0,    0,  489,    0,
 -116, -120,    0,    0,  505,  109,  489, -218,  489,  363,
  489,  363, -157,  489,  489,  363,  -29,  489,  489,  489,
  489,  489,  489,  489,  489,  489,  -50,  489,  489,  489,
  489,  489,  489,  489,  489,  140,  411,  489,  489,  489,
 -211,  112,    0,  505,    0,    0,    0,    0, 1559,    0,
  -99,  356,    0,  130,    0,    0,   70,  363,    0,   70,
   70,  363,  363,   70,   70,   70,  363,  489,   70,   70,
   70,   70,  132,  133,  134,   38,   43,  135,  489,   44,
 -169, -168, -166, -165, -164, -160, -155, -154,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  489,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  489,  -10,   70,   70,  489,
    0,    0,    0,    0,    0,    0,  147,  -66,  156,   70,
    0,  161,  166,   70,   70,  167,  168,  175,   70,   70,
  176,  177,  179,  181,  489,  489,  489, -103,  489,  489,
   69,  489,  489,  489,  489,  489,  489,  489,  489,  489,
   70,   70,  356,  192,    0,  194,    0,  200,   70,  -66,
  151,  356,  213,  356,  356,  214,  215,  356,  356,  356,
  216,  220,  356,  356,  356,  356,    0,  221,  222,  -13,
  224,  225,  489,  226,   64,   64,   64,   64,   64,   64,
   64,   64,  229,  230,  231,  195,  489,  489,  250,  247,
  489,    0,  356,    0,    0,  356,  356,    0,    0,    0,
  356,  356,    0,    0,    0,    0,  489,  489,    0,  -48,
  -45,  259,  489,  356,  356,  356,    0,  261,  391,   75,
    0,  489,  -66,  264,  451,    0,    0,    0,    0,    0,
    0,    0,    0,   57,   67,  489,    0,    0,  266,    0,
    0,  232,  489,  227,    0,  489, -120,   79,  269,  -66,
    0,    0,  285,  291,  266,  489,    0,    0,    0,    0,
 -120, -120,   72,    0,   74,   74,    0, -120,   76,   78,
    0,  293,  293,    0,    0,   74,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,  340,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -22,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -20,
    0,    0,   81,    0,    0,    0,    0,  372,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   85,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   34,    0,    0,    0,    0,    0,
    0,   71,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  567,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  629,  691,  753,  815,  877,  939,
 1001, 1063,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 1125,    0,
    0,    0,    0,    0,    0,    0, 1187,    0,  241,    0,
    0,    0,    0,    0, 1249,    0,    0,    0,    0,    0,
 1311,    0,    0,    0,    0,    0,    0, 1373,    0,    0,
    0, 1435, 1497,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  337,  309,    0,    0,    0,  288,  273,  272,  244,
  314,   -5,  300,  255, -108,    0,  258,    5, -102, -123,
    0,    0,    7,  253,    0, -241,  -86,  -57,  -41,  -21,
   -9,  206,    0,  205,  208,    0,  201,   87,   15,   -8,
    0,   54,    0,   14,  -25, -153,
  };
  protected static readonly short [] yyTable = {            32,
   33,  133,  118,   47,   41,   48,  135,  133,  133,   29,
  117,   57,   47,  131,   48,  132,  301,   88,   27,   88,
   28,   27,   43,   28,   47,   47,   48,   48,   78,   47,
   29,   48,  129,   45,  261,   47,   34,   48,  261,  261,
   30,   43,  134,  261,   76,  111,  181,   57,   14,   15,
  130,   70,   71,   72,   73,   36,   73,  100,  340,   13,
  182,   30,   79,   80,   81,   82,   83,   39,  120,  121,
   46,  108,   28,  123,  107,   98,   16,   47,  101,   48,
  293,  278,   47,   47,   48,   48,  279,  282,   17,    1,
    2,  115,   56,   28,    3,    4,  109,   37,   87,  107,
  113,   73,   27,   47,   28,   48,    5,    6,   47,   47,
   48,   48,  323,   38,    7,  397,  192,  193,  396,  412,
  197,   49,  396,  115,   49,   50,  120,  121,   50,   97,
   44,  399,   45,   50,  187,  190,  191,   51,  194,  195,
  196,  199,  200,  201,  202,  102,  103,  104,  206,  207,
   54,  210,  185,  186,   52,   28,   58,   55,  414,   59,
   60,   62,  247,  248,  249,  110,  127,   63,   64,  179,
  123,  250,  256,  258,  123,  275,  276,  277,  280,   78,
  283,  284,  260,  285,  286,  287,  264,  265,  257,  288,
  300,  269,  270,  259,  289,  290,  262,  263,  181,  302,
  266,  267,  268,  281,  304,  271,  272,  273,  274,  305,
  308,  309,   18,   79,   80,   81,   82,   83,  310,  313,
  314,   19,  315,  291,  316,   20,   21,   22,   23,   24,
   25,   26,   27,   18,   46,  336,  251,  337,  320,  338,
  292,  341,   19,  359,  299,   40,   20,   21,   22,   23,
   24,   25,   26,   27,  296,   49,  343,  346,  347,  351,
  102,  103,  104,  352,  357,  358,  303,  360,  361,  363,
  306,  307,  364,  365,  366,  311,  312,  325,  326,  327,
  328,  329,  330,  331,  332,  293,   97,  133,  411,  372,
  373,  118,  209,  384,  133,  198,  385,  333,  334,  335,
   45,   45,  386,  418,  393,   45,   45,  400,  342,  406,
  344,  345,  413,  403,  348,  349,  350,   45,   45,  353,
  354,  355,  356,  404,  407,   45,   89,   90,  415,   91,
   92,   93,  369,   94,  416,  115,  426,   46,   46,    1,
   95,   96,   46,   46,  419,   35,  420,   53,  424,  377,
  425,   86,  378,  379,   46,   46,  112,  380,  381,  114,
   61,   77,   46,  124,  119,  133,  369,  128,  427,  115,
  390,  391,  392,  394,  423,  395,  405,  252,  253,  255,
  180,  254,  184,  136,  417,  339,  398,  410,  402,  367,
  369,    0,    0,    0,  203,  204,  205,    0,    0,  208,
  409,  211,  212,  213,  214,  215,  216,  217,  218,  137,
  138,  139,    0,    0,    0,   97,    0,    0,    0,    0,
  140,    0,   29,  141,  142,    0,  143,  144,    0,  145,
   47,    0,   48,    0,  146,  147,    0,    0,  148,  149,
  150,  151,  152,  153,    0,    0,  154,  155,  156,    0,
   97,    0,  157,   30,    0,    0,  158,  159,  160,    0,
    0,    0,  161,  162,  163,  164,    0,    0,  165,   47,
  166,   48,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  167,   29,   89,   90,   28,   91,   92,   93,   97,
   94,    0,  168,  169,  170,  171,   21,   95,   96,  219,
  220,  221,  222,  223,  224,  225,  226,  227,  228,    0,
   29,    0,  133,   30,  102,  103,  104,    0,  317,  318,
  319,    0,  321,  322,    0,  324,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  133,  133,
  133,   30,    0,  401,    0,   28,    0,    0,   29,  133,
    0,    0,  133,  133,    0,  133,  133,    0,  133,    0,
    0,    0,    0,  133,  133,    0,  362,  133,  133,  133,
  133,  133,  133,   28,    0,  133,  133,  133,    0,   30,
  368,  133,    0,    0,    0,  133,  133,  133,    0,    0,
    0,  133,  133,  133,  133,    0,    0,  133,    0,  133,
  382,  383,    0,    0,    0,    0,  387,    0,    0,    0,
  133,   28,   89,   90,    0,   91,   92,   93,    0,   94,
    0,  133,  133,  133,  133,   18,   95,   96,    0,  387,
   21,    0,    0,    0,   19,    0,  408,    0,   20,   21,
   22,   23,   24,   25,   26,   27,    0,   89,   90,  387,
   91,   92,   93,    0,   94,    0,    0,    0,    0,    0,
    0,   95,   96,    0,   21,   21,   21,   21,   21,    0,
  230,  231,  185,  186,    0,    0,    0,    0,  102,  103,
  104,    0,    0,    0,    0,   18,   89,   90,    0,   91,
   92,   93,    0,   94,   19,   65,    0,    0,   20,   21,
   22,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,    0,    0,   18,    0,    0,    0,    0,    0,    0,
    0,    0,   19,    0,    0,    0,   20,   21,   22,   23,
   24,   25,   26,   27,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   18,    0,    0,    0,    0,    0,    0,    0,    0,
   19,    0,    0,    0,   20,   21,   22,   23,   24,   25,
   26,   27,  232,  233,  234,  235,  136,    0,    0,    0,
    0,  236,  237,  238,  239,  240,  241,  242,  243,  244,
  245,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  137,  138,  139,    0,    0,    0,    0,    0,
    0,    0,    0,  140,    0,    0,  141,  142,    0,  143,
  144,    0,  145,    0,    0,    0,    0,  146,  147,    0,
    0,  148,  149,  150,  151,  152,  153,    0,  160,  154,
  155,  156,    0,    0,    0,  157,    0,    0,    0,  158,
  159,  160,    0,    0,    0,  161,  162,  163,  164,    0,
    0,  165,    0,  166,  160,  160,  160,    0,    0,    0,
    0,    0,    0,    0,  167,  160,    0,    0,  160,  160,
    0,  160,  160,    0,  160,  168,  169,  170,  171,  160,
  160,    0,    0,  160,  160,  160,  160,  160,  160,    0,
  171,  160,  160,  160,    0,    0,    0,  160,    0,    0,
    0,  160,  160,  160,    0,    0,    0,  160,  160,  160,
  160,    0,    0,  160,    0,  160,  171,  171,  171,    0,
    0,    0,    0,    0,    0,    0,  160,  171,    0,    0,
  171,  171,    0,  171,  171,    0,  171,  160,  160,  160,
  160,  171,  171,    0,    0,  171,  171,  171,  171,  171,
  171,    0,  174,  171,  171,  171,    0,    0,    0,  171,
    0,    0,    0,  171,  171,  171,    0,    0,    0,  171,
  171,  171,  171,    0,    0,  171,    0,  171,  174,  174,
  174,    0,    0,    0,    0,    0,    0,    0,  171,  174,
    0,    0,  174,  174,    0,  174,  174,    0,  174,  171,
  171,  171,  171,  174,  174,    0,    0,  174,  174,  174,
  174,  174,  174,    0,  163,  174,  174,  174,    0,    0,
    0,  174,    0,    0,    0,  174,  174,  174,    0,    0,
    0,  174,  174,  174,  174,    0,    0,  174,    0,  174,
  163,  163,  163,    0,    0,    0,    0,    0,    0,    0,
  174,  163,    0,    0,  163,  163,    0,  163,  163,    0,
  163,  174,  174,  174,  174,  163,  163,    0,    0,  163,
  163,  163,  163,  163,  163,    0,  149,  163,  163,  163,
    0,    0,    0,  163,    0,    0,    0,  163,  163,  163,
    0,    0,    0,  163,  163,  163,  163,    0,    0,  163,
    0,  163,  149,  149,  149,    0,    0,    0,    0,    0,
    0,    0,  163,  149,    0,    0,  149,  149,    0,  149,
  149,    0,  149,  163,  163,  163,  163,  149,  149,    0,
    0,  149,  149,  149,  149,  149,  149,    0,  150,  149,
  149,  149,    0,    0,    0,  149,    0,    0,    0,  149,
  149,  149,    0,    0,    0,  149,  149,  149,  149,    0,
    0,  149,    0,  149,  150,  150,  150,    0,    0,    0,
    0,    0,    0,    0,  149,  150,    0,    0,  150,  150,
    0,  150,  150,    0,  150,  149,  149,  149,  149,  150,
  150,    0,    0,  150,  150,  150,  150,  150,  150,    0,
  172,  150,  150,  150,    0,    0,    0,  150,    0,    0,
    0,  150,  150,  150,    0,    0,    0,  150,  150,  150,
  150,    0,    0,  150,    0,  150,  172,  172,  172,    0,
    0,    0,    0,    0,    0,    0,  150,  172,    0,    0,
  172,  172,    0,  172,  172,    0,  172,  150,  150,  150,
  150,  172,  172,    0,    0,  172,  172,  172,  172,  172,
  172,    0,  167,  172,  172,  172,    0,    0,    0,  172,
    0,    0,    0,  172,  172,  172,    0,    0,    0,  172,
  172,  172,  172,    0,    0,  172,    0,  172,  167,  167,
  167,    0,    0,    0,    0,    0,    0,    0,  172,  167,
    0,    0,  167,  167,    0,  167,  167,    0,  167,  172,
  172,  172,  172,  167,  167,    0,    0,  167,  167,  167,
  167,  167,  167,    0,  141,  167,  167,  167,    0,    0,
    0,  167,    0,    0,    0,  167,  167,  167,    0,    0,
    0,  167,  167,  167,  167,    0,    0,  167,    0,  167,
  141,  141,  141,    0,    0,    0,    0,    0,    0,    0,
  167,  141,    0,    0,  141,  141,    0,  141,  141,    0,
  141,  167,  167,  167,  167,  141,  141,    0,    0,  141,
  141,  141,  141,  141,  141,    0,  151,  141,  141,  141,
    0,    0,    0,  141,    0,    0,    0,  141,  141,  141,
    0,    0,    0,  141,  141,  141,  141,    0,    0,  141,
    0,  141,  151,  151,  151,    0,    0,    0,    0,    0,
    0,    0,  141,  151,    0,    0,  151,  151,    0,  151,
  151,    0,  151,  141,  141,  141,  141,  151,  151,    0,
    0,  151,  151,  151,  151,  151,  151,    0,  142,  151,
  151,  151,    0,    0,    0,  151,    0,    0,    0,  151,
  151,  151,    0,    0,    0,  151,  151,  151,  151,    0,
    0,  151,    0,  151,  142,  142,  142,    0,    0,    0,
    0,    0,    0,    0,  151,  142,    0,    0,  142,  142,
    0,  142,  142,    0,  142,  151,  151,  151,  151,  142,
  142,    0,    0,  142,  142,  142,  142,  142,  142,    0,
  152,  142,  142,  142,    0,    0,    0,  142,    0,    0,
    0,  142,  142,  142,    0,    0,    0,  142,  142,  142,
  142,    0,    0,  142,    0,  142,  152,  152,  152,    0,
    0,    0,    0,    0,    0,    0,  142,  152,    0,    0,
  152,  152,    0,  152,  152,    0,  152,  142,  142,  142,
  142,  152,  152,    0,    0,  152,  152,  152,  152,  152,
  152,    0,  143,  152,  152,  152,    0,    0,    0,  152,
    0,    0,    0,  152,  152,  152,    0,    0,    0,  152,
  152,  152,  152,    0,    0,  152,    0,  152,  143,  143,
  143,    0,    0,    0,    0,    0,    0,    0,  152,  143,
    0,    0,  143,  143,    0,  143,  143,    0,  143,  152,
  152,  152,  152,  143,  143,    0,    0,  143,  143,  143,
  143,  143,  143,    0,  144,  143,  143,  143,    0,    0,
    0,  143,    0,    0,    0,  143,  143,  143,    0,    0,
    0,  143,  143,  143,  143,    0,    0,  143,    0,  143,
  144,  144,  144,    0,    0,    0,    0,    0,    0,    0,
  143,  144,    0,    0,  144,  144,    0,  144,  144,    0,
  144,  143,  143,  143,  143,  144,  144,    0,    0,  144,
  144,  144,  144,  144,  144,    0,  155,  144,  144,  144,
    0,    0,    0,  144,    0,    0,    0,  144,  144,  144,
    0,    0,    0,  144,  144,  144,  144,    0,    0,  144,
    0,  144,  155,  155,  155,    0,    0,    0,    0,    0,
    0,    0,  144,  155,    0,    0,  155,  155,    0,  155,
  155,    0,  155,  144,  144,  144,  144,  155,  155,    0,
    0,  155,  155,  155,  155,  155,  155,    0,  168,  155,
  155,  155,    0,    0,    0,  155,    0,    0,    0,  155,
  155,  155,    0,    0,    0,  155,  155,  155,  155,    0,
    0,  155,    0,  155,  168,  168,  168,    0,    0,    0,
    0,    0,    0,    0,  155,  168,    0,    0,  168,  168,
    0,  168,  168,    0,  168,  155,  155,  155,  155,  168,
  168,    0,    0,  168,  168,  168,  168,  168,  168,    0,
    0,  168,  168,  168,    0,    0,    0,  168,    0,    0,
    0,  168,  168,  168,    0,    0,    0,  168,  168,  168,
  168,    0,    0,  168,    0,  168,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  168,  140,    0,    0,
  141,  142,    0,  143,  144,    0,  145,  168,  168,  168,
  168,  146,  147,    0,    0,  148,  149,  150,  151,  152,
  153,    0,    0,  154,  155,  156,    0,    0,    0,  157,
    0,    0,    0,  158,  159,  160,    0,    0,    0,  161,
  162,  163,  164,    0,    0,  165,    0,  166,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  167,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  168,
  169,  170,  171,
  };
  protected static readonly short [] yyCheck = {             5,
    6,  125,  105,   40,   33,   42,  123,  131,  132,   60,
   97,   44,   40,  122,   42,  124,  258,   44,   41,   44,
   41,   44,   28,   44,   40,   40,   42,   42,  259,   40,
   60,   42,   44,    0,  188,   40,  291,   42,  192,  193,
   91,   47,  129,  197,   41,  125,  265,   44,  269,  270,
   62,   57,   58,   59,   60,  259,   62,   62,  300,   61,
  279,   91,  293,  294,  295,  296,  297,  275,  286,  287,
    0,   41,  123,  291,   44,   69,   61,   40,   93,   42,
   91,   44,   40,   40,   42,   42,   44,   44,   61,  267,
  268,   97,  125,  123,  272,  273,   41,   61,  125,   44,
  125,  107,  125,   40,  125,   42,  284,  285,   40,   40,
   42,   42,   44,   61,  292,   41,  142,  143,   44,   41,
  146,   41,   44,  129,   44,   41,  286,  287,   44,   60,
  257,  373,  257,   61,  140,  141,  142,  259,  144,  145,
  146,  147,  148,  149,  150,  288,  289,  290,  154,  155,
   33,  157,  310,  311,  259,  123,  266,  123,  400,  266,
   40,   40,  168,  169,  170,   61,  259,  123,  123,   61,
  291,  383,  272,   44,  291,   44,   44,   44,   44,  259,
  350,  350,  188,  350,  350,  350,  192,  193,  182,  350,
   44,  197,  198,  187,  350,  350,  190,  191,  265,   44,
  194,  195,  196,  209,   44,  199,  200,  201,  202,   44,
   44,   44,  263,  293,  294,  295,  296,  297,   44,   44,
   44,  272,   44,  229,   44,  276,  277,  278,  279,  280,
  281,  282,  283,  263,  271,   44,  125,   44,  342,   40,
  246,   91,  272,  257,  250,  274,  276,  277,  278,  279,
  280,  281,  282,  283,  248,  271,   44,   44,   44,   44,
  288,  289,  290,   44,   44,   44,  260,   44,   44,   44,
  264,  265,   44,   44,   44,  269,  270,  283,  284,  285,
  286,  287,  288,  289,  290,   91,   60,  411,  397,   40,
   44,  394,  343,  342,  418,  325,  342,  291,  292,  293,
  267,  268,   44,  412,   44,  272,  273,   44,  302,   44,
  304,  305,   44,  257,  308,  309,  310,  284,  285,  313,
  314,  315,  316,  257,   93,  292,  257,  258,   44,  260,
  261,  262,  338,  264,   44,  341,   44,  267,  268,    0,
  271,  272,  272,  273,  273,    9,  273,   39,  273,  343,
  273,   64,  346,  347,  284,  285,   84,  351,  352,   88,
   47,   62,  292,  109,  107,  125,  372,  115,  426,  375,
  364,  365,  366,  369,  416,  369,  386,  172,  174,  179,
  137,  174,  139,  272,  406,  299,  372,  396,  375,  336,
  396,   -1,   -1,   -1,  151,  152,  153,   -1,   -1,  156,
  394,  158,  159,  160,  161,  162,  163,  164,  165,  298,
  299,  300,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,
  309,   -1,   60,  312,  313,   -1,  315,  316,   -1,  318,
   40,   -1,   42,   -1,  323,  324,   -1,   -1,  327,  328,
  329,  330,  331,  332,   -1,   -1,  335,  336,  337,   -1,
   60,   -1,  341,   91,   -1,   -1,  345,  346,  347,   -1,
   -1,   -1,  351,  352,  353,  354,   -1,   -1,  357,   40,
  359,   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  370,   60,  257,  258,  123,  260,  261,  262,   60,
  264,   -1,  381,  382,  383,  384,  125,  271,  272,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,   -1,
   60,   -1,  272,   91,  288,  289,  290,   -1,  275,  276,
  277,   -1,  279,  280,   -1,  282,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  298,  299,
  300,   91,   -1,   93,   -1,  123,   -1,   -1,   60,  309,
   -1,   -1,  312,  313,   -1,  315,  316,   -1,  318,   -1,
   -1,   -1,   -1,  323,  324,   -1,  323,  327,  328,  329,
  330,  331,  332,  123,   -1,  335,  336,  337,   -1,   91,
  337,  341,   -1,   -1,   -1,  345,  346,  347,   -1,   -1,
   -1,  351,  352,  353,  354,   -1,   -1,  357,   -1,  359,
  357,  358,   -1,   -1,   -1,   -1,  363,   -1,   -1,   -1,
  370,  123,  257,  258,   -1,  260,  261,  262,   -1,  264,
   -1,  381,  382,  383,  384,  263,  271,  272,   -1,  386,
  259,   -1,   -1,   -1,  272,   -1,  393,   -1,  276,  277,
  278,  279,  280,  281,  282,  283,   -1,  257,  258,  406,
  260,  261,  262,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,  271,  272,   -1,  293,  294,  295,  296,  297,   -1,
  260,  261,  310,  311,   -1,   -1,   -1,   -1,  288,  289,
  290,   -1,   -1,   -1,   -1,  263,  257,  258,   -1,  260,
  261,  262,   -1,  264,  272,  273,   -1,   -1,  276,  277,
  278,  279,  280,  281,  282,  283,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  263,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  272,   -1,   -1,   -1,  276,  277,  278,  279,
  280,  281,  282,  283,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,   -1,   -1,   -1,  276,  277,  278,  279,  280,  281,
  282,  283,  362,  363,  364,  365,  272,   -1,   -1,   -1,
   -1,  371,  372,  373,  374,  375,  376,  377,  378,  379,
  380,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  298,  299,  300,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  309,   -1,   -1,  312,  313,   -1,  315,
  316,   -1,  318,   -1,   -1,   -1,   -1,  323,  324,   -1,
   -1,  327,  328,  329,  330,  331,  332,   -1,  272,  335,
  336,  337,   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,
  346,  347,   -1,   -1,   -1,  351,  352,  353,  354,   -1,
   -1,  357,   -1,  359,  298,  299,  300,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,  312,  313,
   -1,  315,  316,   -1,  318,  381,  382,  383,  384,  323,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,   -1,
  272,  335,  336,  337,   -1,   -1,   -1,  341,   -1,   -1,
   -1,  345,  346,  347,   -1,   -1,   -1,  351,  352,  353,
  354,   -1,   -1,  357,   -1,  359,  298,  299,  300,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,
  312,  313,   -1,  315,  316,   -1,  318,  381,  382,  383,
  384,  323,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,   -1,  272,  335,  336,  337,   -1,   -1,   -1,  341,
   -1,   -1,   -1,  345,  346,  347,   -1,   -1,   -1,  351,
  352,  353,  354,   -1,   -1,  357,   -1,  359,  298,  299,
  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,
   -1,   -1,  312,  313,   -1,  315,  316,   -1,  318,  381,
  382,  383,  384,  323,  324,   -1,   -1,  327,  328,  329,
  330,  331,  332,   -1,  272,  335,  336,  337,   -1,   -1,
   -1,  341,   -1,   -1,   -1,  345,  346,  347,   -1,   -1,
   -1,  351,  352,  353,  354,   -1,   -1,  357,   -1,  359,
  298,  299,  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  370,  309,   -1,   -1,  312,  313,   -1,  315,  316,   -1,
  318,  381,  382,  383,  384,  323,  324,   -1,   -1,  327,
  328,  329,  330,  331,  332,   -1,  272,  335,  336,  337,
   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,  346,  347,
   -1,   -1,   -1,  351,  352,  353,  354,   -1,   -1,  357,
   -1,  359,  298,  299,  300,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  370,  309,   -1,   -1,  312,  313,   -1,  315,
  316,   -1,  318,  381,  382,  383,  384,  323,  324,   -1,
   -1,  327,  328,  329,  330,  331,  332,   -1,  272,  335,
  336,  337,   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,
  346,  347,   -1,   -1,   -1,  351,  352,  353,  354,   -1,
   -1,  357,   -1,  359,  298,  299,  300,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,  312,  313,
   -1,  315,  316,   -1,  318,  381,  382,  383,  384,  323,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,   -1,
  272,  335,  336,  337,   -1,   -1,   -1,  341,   -1,   -1,
   -1,  345,  346,  347,   -1,   -1,   -1,  351,  352,  353,
  354,   -1,   -1,  357,   -1,  359,  298,  299,  300,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,
  312,  313,   -1,  315,  316,   -1,  318,  381,  382,  383,
  384,  323,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,   -1,  272,  335,  336,  337,   -1,   -1,   -1,  341,
   -1,   -1,   -1,  345,  346,  347,   -1,   -1,   -1,  351,
  352,  353,  354,   -1,   -1,  357,   -1,  359,  298,  299,
  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,
   -1,   -1,  312,  313,   -1,  315,  316,   -1,  318,  381,
  382,  383,  384,  323,  324,   -1,   -1,  327,  328,  329,
  330,  331,  332,   -1,  272,  335,  336,  337,   -1,   -1,
   -1,  341,   -1,   -1,   -1,  345,  346,  347,   -1,   -1,
   -1,  351,  352,  353,  354,   -1,   -1,  357,   -1,  359,
  298,  299,  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  370,  309,   -1,   -1,  312,  313,   -1,  315,  316,   -1,
  318,  381,  382,  383,  384,  323,  324,   -1,   -1,  327,
  328,  329,  330,  331,  332,   -1,  272,  335,  336,  337,
   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,  346,  347,
   -1,   -1,   -1,  351,  352,  353,  354,   -1,   -1,  357,
   -1,  359,  298,  299,  300,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  370,  309,   -1,   -1,  312,  313,   -1,  315,
  316,   -1,  318,  381,  382,  383,  384,  323,  324,   -1,
   -1,  327,  328,  329,  330,  331,  332,   -1,  272,  335,
  336,  337,   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,
  346,  347,   -1,   -1,   -1,  351,  352,  353,  354,   -1,
   -1,  357,   -1,  359,  298,  299,  300,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,  312,  313,
   -1,  315,  316,   -1,  318,  381,  382,  383,  384,  323,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,   -1,
  272,  335,  336,  337,   -1,   -1,   -1,  341,   -1,   -1,
   -1,  345,  346,  347,   -1,   -1,   -1,  351,  352,  353,
  354,   -1,   -1,  357,   -1,  359,  298,  299,  300,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,
  312,  313,   -1,  315,  316,   -1,  318,  381,  382,  383,
  384,  323,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,   -1,  272,  335,  336,  337,   -1,   -1,   -1,  341,
   -1,   -1,   -1,  345,  346,  347,   -1,   -1,   -1,  351,
  352,  353,  354,   -1,   -1,  357,   -1,  359,  298,  299,
  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,
   -1,   -1,  312,  313,   -1,  315,  316,   -1,  318,  381,
  382,  383,  384,  323,  324,   -1,   -1,  327,  328,  329,
  330,  331,  332,   -1,  272,  335,  336,  337,   -1,   -1,
   -1,  341,   -1,   -1,   -1,  345,  346,  347,   -1,   -1,
   -1,  351,  352,  353,  354,   -1,   -1,  357,   -1,  359,
  298,  299,  300,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  370,  309,   -1,   -1,  312,  313,   -1,  315,  316,   -1,
  318,  381,  382,  383,  384,  323,  324,   -1,   -1,  327,
  328,  329,  330,  331,  332,   -1,  272,  335,  336,  337,
   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,  346,  347,
   -1,   -1,   -1,  351,  352,  353,  354,   -1,   -1,  357,
   -1,  359,  298,  299,  300,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  370,  309,   -1,   -1,  312,  313,   -1,  315,
  316,   -1,  318,  381,  382,  383,  384,  323,  324,   -1,
   -1,  327,  328,  329,  330,  331,  332,   -1,  272,  335,
  336,  337,   -1,   -1,   -1,  341,   -1,   -1,   -1,  345,
  346,  347,   -1,   -1,   -1,  351,  352,  353,  354,   -1,
   -1,  357,   -1,  359,  298,  299,  300,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,  312,  313,
   -1,  315,  316,   -1,  318,  381,  382,  383,  384,  323,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,   -1,
   -1,  335,  336,  337,   -1,   -1,   -1,  341,   -1,   -1,
   -1,  345,  346,  347,   -1,   -1,   -1,  351,  352,  353,
  354,   -1,   -1,  357,   -1,  359,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  370,  309,   -1,   -1,
  312,  313,   -1,  315,  316,   -1,  318,  381,  382,  383,
  384,  323,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,   -1,   -1,  335,  336,  337,   -1,   -1,   -1,  341,
   -1,   -1,   -1,  345,  346,  347,   -1,   -1,   -1,  351,
  352,  353,  354,   -1,   -1,  357,   -1,  359,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  370,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,
  382,  383,  384,
  };

#line 639 "Repil/IR/IR.jay"

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
  public const int DISTINCT = 274;
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
  public const int DECLARE = 285;
  public const int UNNAMED_ADDR = 286;
  public const int LOCAL_UNNAMED_ADDR = 287;
  public const int NONNULL = 288;
  public const int NOCAPTURE = 289;
  public const int WRITEONLY = 290;
  public const int ATTRIBUTE_GROUP_REF = 291;
  public const int ATTRIBUTES = 292;
  public const int NORECURSE = 293;
  public const int NOUNWIND = 294;
  public const int SSP = 295;
  public const int UWTABLE = 296;
  public const int ARGMEMONLY = 297;
  public const int RET = 298;
  public const int BR = 299;
  public const int SWITCH = 300;
  public const int INDIRECTBR = 301;
  public const int INVOKE = 302;
  public const int RESUME = 303;
  public const int CATCHSWITCH = 304;
  public const int CATCHRET = 305;
  public const int CLEANUPRET = 306;
  public const int UNREACHABLE = 307;
  public const int FNEG = 308;
  public const int ADD = 309;
  public const int NUW = 310;
  public const int NSW = 311;
  public const int FADD = 312;
  public const int SUB = 313;
  public const int FSUB = 314;
  public const int MUL = 315;
  public const int FMUL = 316;
  public const int UDIV = 317;
  public const int SDIV = 318;
  public const int FDIV = 319;
  public const int UREM = 320;
  public const int SREM = 321;
  public const int FREM = 322;
  public const int SHL = 323;
  public const int LSHR = 324;
  public const int EXACT = 325;
  public const int ASHR = 326;
  public const int AND = 327;
  public const int OR = 328;
  public const int XOR = 329;
  public const int EXTRACTELEMENT = 330;
  public const int INSERTELEMENT = 331;
  public const int SHUFFLEVECTOR = 332;
  public const int EXTRACTVALUE = 333;
  public const int INSERTVALUE = 334;
  public const int ALLOCA = 335;
  public const int LOAD = 336;
  public const int STORE = 337;
  public const int FENCE = 338;
  public const int CMPXCHG = 339;
  public const int ATOMICRMW = 340;
  public const int GETELEMENTPTR = 341;
  public const int ALIGN = 342;
  public const int INBOUNDS = 343;
  public const int INRANGE = 344;
  public const int TRUNC = 345;
  public const int ZEXT = 346;
  public const int SEXT = 347;
  public const int FPTRUNC = 348;
  public const int FPEXT = 349;
  public const int TO = 350;
  public const int FPTOUI = 351;
  public const int FPTOSI = 352;
  public const int UITOFP = 353;
  public const int SITOFP = 354;
  public const int PTRTOINT = 355;
  public const int INTTOPTR = 356;
  public const int BITCAST = 357;
  public const int ADDRSPACECAST = 358;
  public const int ICMP = 359;
  public const int EQ = 360;
  public const int NE = 361;
  public const int UGT = 362;
  public const int UGE = 363;
  public const int ULT = 364;
  public const int ULE = 365;
  public const int SGT = 366;
  public const int SGE = 367;
  public const int SLT = 368;
  public const int SLE = 369;
  public const int FCMP = 370;
  public const int OEQ = 371;
  public const int OGT = 372;
  public const int OGE = 373;
  public const int OLT = 374;
  public const int OLE = 375;
  public const int ONE = 376;
  public const int ORD = 377;
  public const int UEQ = 378;
  public const int UNE = 379;
  public const int UNO = 380;
  public const int PHI = 381;
  public const int SELECT = 382;
  public const int CALL = 383;
  public const int TAIL = 384;
  public const int VA_ARG = 385;
  public const int LANDINGPAD = 386;
  public const int CATCHPAD = 387;
  public const int CLEANUPPAD = 388;
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
