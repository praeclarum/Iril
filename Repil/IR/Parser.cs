// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Repil/IR/IR.jay"
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Linq;

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
//t    "function_definition : DEFINE NOALIAS type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
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
//t    "block : assignments terminator_instruction",
//t    "block : terminator_instruction",
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
//t    "instruction : MUL type value ',' value",
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
    "UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NOALIAS","NONNULL","NOCAPTURE",
    "WRITEONLY","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND",
    "SSP","UWTABLE","ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR",
    "INVOKE","RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE",
    "FNEG","ADD","NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV",
    "SDIV","FDIV","UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND",
    "OR","XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR",
    "EXTRACTVALUE","INSERTVALUE","ALLOCA","LOAD","STORE","FENCE",
    "CMPXCHG","ATOMICRMW","GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE",
    "TRUNC","ZEXT","SEXT","FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI",
    "UITOFP","SITOFP","PTRTOINT","INTTOPTR","BITCAST","ADDRSPACECAST",
    "ICMP","EQ","NE","UGT","UGE","ULT","ULE","SGT","SGE","SLT","SLE",
    "FCMP","OEQ","OGT","OGE","OLT","OLE","ONE","ORD","UEQ","UNE","UNO",
    "PHI","SELECT","CALL","TAIL","VA_ARG","LANDINGPAD","CATCHPAD",
    "CLEANUPPAD",
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
#line 57 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 61 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 65 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 69 "Repil/IR/IR.jay"
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
#line 122 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 28:
#line 126 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 30:
#line 131 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 31:
#line 132 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 32:
#line 133 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 33:
#line 134 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 34:
#line 135 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 35:
#line 136 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 36:
#line 137 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 37:
#line 138 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 38:
#line 139 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 39:
#line 143 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 40:
#line 147 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 41:
#line 151 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 42:
#line 155 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 43:
#line 159 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 44:
#line 166 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 45:
#line 170 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 46:
#line 177 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 47:
#line 181 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 48:
#line 188 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 49:
#line 192 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 50:
#line 199 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 51:
#line 203 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 53:
#line 211 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 54:
#line 215 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 55:
#line 216 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 56:
#line 217 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 62:
#line 235 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 63:
#line 236 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 64:
#line 237 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 65:
#line 238 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 66:
#line 239 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 67:
#line 240 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 68:
#line 241 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 69:
#line 242 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 70:
#line 243 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 71:
#line 244 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 72:
#line 248 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 73:
#line 249 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 74:
#line 250 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 75:
#line 251 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 76:
#line 252 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 77:
#line 253 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 78:
#line 254 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 79:
#line 255 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 80:
#line 256 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 81:
#line 257 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 82:
#line 258 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 83:
#line 259 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 84:
#line 260 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 85:
#line 261 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 86:
#line 262 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 87:
#line 263 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 89:
#line 268 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 90:
#line 269 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 91:
#line 273 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 92:
#line 274 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 93:
#line 275 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 94:
#line 276 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 95:
#line 277 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 96:
#line 278 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 97:
#line 282 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 289 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 99:
#line 296 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 100:
#line 303 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 334 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 338 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 345 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 349 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 360 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 367 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 114:
#line 371 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 115:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 116:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 393 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 119:
#line 397 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 120:
#line 404 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 121:
#line 408 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 122:
#line 415 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 124:
#line 425 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 125:
#line 432 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 126:
#line 436 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 127:
#line 443 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 132:
#line 460 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 133:
#line 464 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 134:
#line 468 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-7+yyTop], (LabelValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop]);
    }
  break;
case 135:
#line 472 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 136:
#line 476 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 137:
#line 483 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 138:
#line 487 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 491 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 140:
#line 495 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 142:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 143:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 144:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 145:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 146:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new FloatAddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 527 "Repil/IR/IR.jay"
  {
        yyVal = new FloatMultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 531 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 547 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 551 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 555 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 156:
#line 559 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 157:
#line 563 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 158:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 587 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 591 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-7+yyTop], (TypedValue)yyVals[-5+yyTop]);
    }
  break;
case 170:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 175:
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
#line 71 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 76 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_26()
#line 112 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    6,    6,    8,    8,    8,    8,    8,    8,
    8,    7,    7,    9,    9,    3,   11,   11,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,    4,    4,    5,    5,   13,   13,   17,
   17,   18,   18,   19,   19,   19,   14,   14,   15,   15,
   20,   21,   21,   21,   21,   21,   21,   21,   21,   21,
   21,   22,   22,   22,   22,   22,   22,   22,   22,   22,
   22,   22,   22,   22,   22,   22,   22,   23,   23,   23,
   24,   24,   24,   24,   24,   24,   24,   26,   10,   27,
   25,   25,   28,   29,   29,   30,   31,   31,   16,   16,
   32,   32,   33,   33,   35,   35,   37,   38,   38,   39,
   39,   40,   40,   41,   42,   42,   43,   44,   44,   45,
   45,   34,   34,   34,   34,   34,   36,   36,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   36,   36,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    6,
    6,    7,    1,    2,    1,    1,    1,    1,    1,    3,
    1,    1,    3,    1,    1,    3,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    4,    2,
    1,    5,    5,   11,   12,    7,    8,    1,    3,    1,
    2,    1,    2,    1,    1,    1,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    2,    2,    2,
    1,    3,    2,    1,    3,    1,    1,    3,    1,    2,
    2,    1,    1,    2,    1,    3,    1,    1,    3,    2,
    3,    1,    3,    5,    1,    2,    3,    1,    2,    1,
    1,    2,    7,   10,    2,    7,    5,    6,    5,    5,
    4,    6,    7,    8,    4,    5,    6,    5,    4,    4,
    6,    7,    6,    6,    9,    5,    6,    5,    5,    6,
    3,    5,    7,    4,    5,    6,    6,    4,    9,    5,
    6,    4,    4,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    2,
    8,    9,    0,    0,    0,    0,    0,   30,   41,   31,
   32,   33,   34,   35,   36,   37,   38,    0,    0,    0,
    0,   29,    0,    0,    0,    3,    4,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   40,
    0,    0,    5,    6,    7,    0,    0,    0,   26,    0,
    0,    0,    0,    0,    0,    0,    0,   25,    0,   22,
   24,    0,    0,    0,    0,    0,    0,    0,   48,   39,
    0,    0,   15,   16,   17,   18,   19,    0,   13,    0,
   11,    0,   93,   92,   94,   95,   96,   91,   90,   89,
    0,   99,   88,    0,   42,   43,   54,   55,   56,    0,
   52,    0,    0,    0,    0,   10,   14,   12,   23,    0,
    0,  101,    0,   53,   49,   57,   58,    0,   61,    0,
    0,   59,   20,  100,    0,   97,    0,    0,    0,   60,
  102,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  109,    0,  112,  113,  115,    0,    0,  135,    0,
    0,  132,    0,  130,  131,    0,    0,  128,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   62,   63,
   64,   65,   66,   67,   68,   69,   70,   71,    0,   72,
   73,   84,   85,   86,   87,   75,   77,   78,   79,   80,
   76,   74,   82,   83,   81,    0,    0,    0,    0,    0,
   44,  110,  111,  114,   45,  116,   98,    0,    0,    0,
    0,  129,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  122,    0,  117,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  145,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  137,    0,  146,  170,    0,  159,
    0,  148,  162,  165,    0,    0,  156,  140,  158,  174,
    0,    0,  139,    0,    0,    0,    0,    0,    0,    0,
  123,    0,    0,    0,  118,    0,    0,    0,    0,  125,
  138,  171,  160,  166,  157,  154,  167,    0,    0,    0,
  106,  107,    0,  153,  147,    0,    0,    0,  120,    0,
    0,    0,    0,    0,  136,  126,    0,    0,    0,    0,
  124,  163,  121,  119,    0,    0,    0,  127,    0,    0,
  108,    0,    0,    0,  104,    0,    0,  134,  103,    0,
  105,
  };
  protected static readonly short [] yyDgoto  = {             8,
    9,   10,   32,   11,   12,   88,   69,   89,   70,   71,
   44,   72,   78,  128,  131,  181,   79,  110,  111,  132,
  239,  256,  309,  103,  121,  192,  388,  435,  436,  402,
  403,  182,  183,  184,  185,  186,  310,  384,  385,  306,
  307,  389,  390,  197,  198,
  };
  protected static readonly short [] yySindex = {         -187,
  -39, -100,   22,   39,  522,  568, -242,    0, -187,    0,
    0,    0, -196,   41,   65, -147,  -27,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  568,  568, -115,
 -101,    0,  -32,  -23,  116,    0,    0,  -99,  -73,   64,
  155,   66,  -15,  -41,   49,  -76,  -75,  153,  568,    0,
  154,   72,    0,    0,    0,   74,  543,  159,    0,  568,
  568,  568,  568,    8,  568, -221,  543,    0,  -14,    0,
    0,  512,  568,   49,    3,   -6,  -22,   38,    0,    0,
   97,  139,    0,    0,    0,    0,    0,  -79,    0,  -11,
    0,  543,    0,    0,    0,    0,    0,    0,    0,    0,
  568,    0,    0,   99,    0,    0,    0,    0,    0, -181,
    0,  568, -103, -233,  -58,    0,    0,    0,    0,  115,
    4,    0, -103,    0,    0,    0,    0,  -88,    0,  -88,
  -88,    0,    0,    0,  568,    0,  -88, -110,  -88,    0,
    0, -107,  580,  580,  144,  568, -239,  568,  239,  568,
  239,  239,  568,  568,  239,    2,  568,  568,  568,  568,
  568,  568,  568,  568,  568,  -19,  568,  568,  568,  568,
  568,  568,  568,  568, -246,  493,  568,  568,  568, -176,
  141,    0,  580,    0,    0,    0,  264, 1634,    0,  -60,
  -51,    0,  179,    0,    0,  512,  239,    0,  512,  512,
  239,  512,  239,  512,  512,  512,  239,  568,  512,  512,
  512,  512,  180,  181,  182,   50,   87,  184,  568,   92,
 -122, -119, -113, -106, -105,  -97,  -81,  -80,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  568,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  568,   -3,  512,  512,  568,
    0,    0,    0,    0,    0,    0,    0,  205,    7,  206,
  512,    0,  208,  229,  512,  231,  512,  232,  233,  242,
  512,  512,  243,  257,  260,  263,  568,  568,  568,  -34,
  568,  568,   93,  568,  568,  568,  568,  568,  568,  568,
  568,  568,  512,  512,  -51,  266,    0,  267,    0,  276,
  512,    7,  226,  -51,  274,  -51,  -51,  277,  -51,  278,
  -51,  -51,  -51,  279,  280,  -51,  -51,  -51,  -51,    0,
  282,  283,   76,  285,  293,  568,  297,   49,   49,   49,
   49,   49,   49,   49,   49,  298,  307,  317,  272,  568,
  568,  325,  322,  568,    0,  -51,    0,    0,  -51,    0,
  -51,    0,    0,    0,  -51,  -51,    0,    0,    0,    0,
  568,  568,    0,   24,   25,  326,  568,  -51,  -51,  -51,
    0,  327,  488,  121,    0,  568,    7,  330,  444,    0,
    0,    0,    0,    0,    0,    0,    0,  112,  123,  568,
    0,    0,  334,    0,    0,  289,  568,  472,    0,  568,
  -88,  127,  339,    7,    0,    0,  341,  342,  334,  568,
    0,    0,    0,    0,  -88,  -88,  118,    0,  120,  120,
    0,  -88,  122,  125,    0,  344,  344,    0,    0,  120,
    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,  394,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  -13,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -12,    0,    0,  135,    0,    0,    0,
    0,  356,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  137,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   47,    0,    0,    0,    0,    0,    0,    0,   71,    0,
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
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  642,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  704,  766,  828,
  890,  952, 1014, 1076, 1138,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 1200,    0,    0,    0,    0,    0,    0,    0,
 1262,    0,  357,    0,    0,    0,    0,    0, 1324,    0,
    0,    0,    0,    0, 1386,    0,    0,    0,    0,    0,
    0, 1448,    0,    0,    0, 1510, 1572,    0,    0,    0,
    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  390,  360,    0,    0,    0,  335,  316,  314,  255,
  358,   -5,    5,  -72, -123,  268,  296,   35, -108, -127,
    0,    0,   31,  299,    0, -248,  -66,  -10,   -9,   11,
   32, -120,    0,  250,  251,  247,  126,   52,   26,    0,
   94,    0,   55,   12, -102,
  };
  protected static readonly short [] yyTable = {            33,
   34,  124,   60,  140,  138,   42,  139,   49,  101,   50,
  140,  140,  143,  142,  140,  144,   49,   49,   50,   50,
  313,   13,   43,   45,   49,  190,   50,   27,   28,   92,
   27,   28,   92,   49,  122,   50,   49,   82,   50,  191,
   30,  130,   49,   45,   50,  116,   46,  135,   80,   35,
  137,   60,  126,  127,   74,   75,   76,   77,  129,   77,
  262,   30,   37,  353,  105,  136,  262,   77,  141,   81,
   47,   31,   83,   84,   85,   86,   87,  104,  113,    1,
    2,  112,   16,   59,    3,    4,  106,  305,   49,   49,
   50,   50,   31,  290,  272,  120,    5,    6,  272,   17,
  272,   38,  102,   29,  272,    7,   77,  107,  108,  109,
   91,   27,   28,  118,  229,  230,  231,  232,  233,  234,
  235,  236,  237,  238,   29,   39,   49,   40,   50,  120,
  291,   49,   49,   50,   50,  294,  336,  114,  413,  123,
  112,   46,  112,  196,  199,  200,  202,  204,  205,  206,
  209,  210,  211,  212,   49,   47,   50,  216,  217,   53,
  220,  411,  201,  203,  410,  428,  207,  426,   14,   15,
  410,  257,  258,  259,  101,   50,   52,   51,   50,   82,
   51,  129,  126,  127,  129,   54,   29,   56,   57,   61,
   62,  271,   63,   65,   66,  275,   67,  277,   73,  115,
  133,  281,  282,  129,  188,   93,   94,  260,   95,   96,
   97,  267,   98,  293,   83,   84,   85,   86,   87,   99,
  100,  268,  269,  287,  288,  289,  270,  292,  295,  273,
  274,  296,  276,  303,  278,  279,  280,  297,   48,  283,
  284,  285,  286,   18,  298,  299,   41,   51,  312,  314,
  304,  316,   19,  300,  311,   58,   20,   21,   22,   23,
   24,   25,   26,   27,   18,  261,  107,  108,  109,  301,
  302,  190,  317,   19,  319,  321,  322,   20,   21,   22,
   23,   24,   25,   26,   27,  323,  326,  425,  308,  338,
  339,  340,  341,  342,  343,  344,  345,  140,   30,  124,
  327,  315,  432,  328,  140,  318,  329,  320,  333,  349,
  350,  324,  325,   46,   46,  351,  354,  356,   46,   46,
  359,  361,  365,  366,  219,  371,  372,  208,  374,   31,
   46,   46,  373,  346,  347,  348,  375,   47,   47,   46,
  377,  378,   47,   47,  355,  383,  357,  358,  120,  360,
  379,  362,  363,  364,   47,   47,  367,  368,  369,  370,
  380,   29,  305,   47,  386,  387,  398,  399,  417,  400,
  407,   93,   94,  414,   95,   96,   97,  420,   98,  418,
  383,  421,  427,  120,  429,  430,  391,  440,  265,  392,
  433,  393,  434,    1,  438,  394,  395,  439,   36,   55,
  189,   90,  193,  117,  383,  119,   64,  125,  404,  405,
  406,  187,  145,  409,  213,  214,  215,  408,  134,  218,
  437,  221,  222,  223,  224,  225,  226,  227,  228,  441,
  431,  419,  263,  264,  266,  424,  352,  412,  423,  146,
  147,  148,  381,  416,    0,    0,    0,    0,    0,    0,
  149,    0,    0,  150,  151,    0,  152,  153,    0,  154,
    0,    0,    0,    0,  155,  156,    0,    0,  157,  158,
  159,  160,  161,  162,    0,    0,  163,  164,  165,    0,
   21,  133,  166,    0,    0,    0,  167,  168,  169,    0,
    0,    0,  170,  171,  172,  173,    0,    0,  174,    0,
  175,   18,    0,   30,    0,    0,    0,    0,    0,    0,
   19,  176,    0,    0,   20,   21,   22,   23,   24,   25,
   26,   27,  177,  178,  179,  180,    0,   49,    0,   50,
    0,  101,    0,    0,   31,  145,  415,    0,    0,    0,
    0,  330,  331,  332,    0,  334,  335,  101,  337,  194,
  195,   49,    0,   50,    0,    0,    0,    0,    0,    0,
    0,    0,  146,  147,  148,    0,   29,    0,    0,    0,
    0,  101,    0,  149,    0,    0,  150,  151,    0,  152,
  153,   30,  154,    0,    0,    0,    0,  155,  156,    0,
  376,  157,  158,  159,  160,  161,  162,    0,    0,  163,
  164,  165,   30,    0,  382,  166,    0,    0,    0,  167,
  168,  169,   31,    0,   21,  170,  171,  172,  173,    0,
    0,  174,    0,  175,    0,  396,  397,   30,  133,    0,
    0,  401,    0,   31,  176,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   29,  177,  178,  179,  180,   21,
   21,   21,   21,   21,  401,  133,  133,  133,   31,    0,
    0,  422,    0,    0,    0,   29,  133,    0,    0,  133,
  133,    0,  133,  133,  401,  133,    0,    0,    0,    0,
  133,  133,    0,    0,  133,  133,  133,  133,  133,  133,
   29,    0,  133,  133,  133,    0,    0,    0,  133,    0,
    0,    0,  133,  133,  133,    0,   18,    0,  133,  133,
  133,  133,    0,    0,  133,   19,  133,    0,    0,   20,
   21,   22,   23,   24,   25,   26,   27,  133,   93,   94,
    0,   95,   96,   97,    0,   98,    0,    0,  133,  133,
  133,  133,   99,  100,   93,   94,    0,   95,   96,   97,
    0,   98,  240,  241,    0,    0,    0,    0,   99,  100,
  107,  108,  109,    0,    0,    0,    0,    0,   93,   94,
    0,   95,   96,   97,    0,   98,  107,  108,  109,    0,
    0,    0,   99,  100,   18,    0,    0,    0,    0,    0,
    0,    0,    0,   19,    0,    0,    0,   20,   21,   22,
   23,   24,   25,   26,   27,   18,    0,    0,    0,   28,
    0,    0,    0,    0,   19,   68,    0,    0,   20,   21,
   22,   23,   24,   25,   26,   27,    0,    0,    0,    0,
   18,    0,    0,    0,    0,    0,    0,    0,    0,   19,
    0,    0,    0,   20,   21,   22,   23,   24,   25,   26,
   27,  145,    0,    0,    0,  242,  243,  244,  245,    0,
    0,    0,    0,    0,  246,  247,  248,  249,  250,  251,
  252,  253,  254,  255,    0,    0,    0,    0,  146,  147,
  148,    0,    0,    0,    0,    0,    0,    0,    0,  149,
    0,    0,  150,  151,    0,  152,  153,    0,  154,    0,
    0,    0,    0,  155,  156,    0,    0,  157,  158,  159,
  160,  161,  162,  161,    0,  163,  164,  165,    0,    0,
    0,  166,    0,    0,    0,  167,  168,  169,    0,    0,
    0,  170,  171,  172,  173,    0,    0,  174,    0,  175,
  161,  161,  161,    0,    0,    0,    0,    0,    0,    0,
  176,  161,    0,    0,  161,  161,    0,  161,  161,    0,
  161,  177,  178,  179,  180,  161,  161,    0,    0,  161,
  161,  161,  161,  161,  161,  172,    0,  161,  161,  161,
    0,    0,    0,  161,    0,    0,    0,  161,  161,  161,
    0,    0,    0,  161,  161,  161,  161,    0,    0,  161,
    0,  161,  172,  172,  172,    0,    0,    0,    0,    0,
    0,    0,  161,  172,    0,    0,  172,  172,    0,  172,
  172,    0,  172,  161,  161,  161,  161,  172,  172,    0,
    0,  172,  172,  172,  172,  172,  172,  175,    0,  172,
  172,  172,    0,    0,    0,  172,    0,    0,    0,  172,
  172,  172,    0,    0,    0,  172,  172,  172,  172,    0,
    0,  172,    0,  172,  175,  175,  175,    0,    0,    0,
    0,    0,    0,    0,  172,  175,    0,    0,  175,  175,
    0,  175,  175,    0,  175,  172,  172,  172,  172,  175,
  175,    0,    0,  175,  175,  175,  175,  175,  175,  164,
    0,  175,  175,  175,    0,    0,    0,  175,    0,    0,
    0,  175,  175,  175,    0,    0,    0,  175,  175,  175,
  175,    0,    0,  175,    0,  175,  164,  164,  164,    0,
    0,    0,    0,    0,    0,    0,  175,  164,    0,    0,
  164,  164,    0,  164,  164,    0,  164,  175,  175,  175,
  175,  164,  164,    0,    0,  164,  164,  164,  164,  164,
  164,  149,    0,  164,  164,  164,    0,    0,    0,  164,
    0,    0,    0,  164,  164,  164,    0,    0,    0,  164,
  164,  164,  164,    0,    0,  164,    0,  164,  149,  149,
  149,    0,    0,    0,    0,    0,    0,    0,  164,  149,
    0,    0,  149,  149,    0,  149,  149,    0,  149,  164,
  164,  164,  164,  149,  149,    0,    0,  149,  149,  149,
  149,  149,  149,  150,    0,  149,  149,  149,    0,    0,
    0,  149,    0,    0,    0,  149,  149,  149,    0,    0,
    0,  149,  149,  149,  149,    0,    0,  149,    0,  149,
  150,  150,  150,    0,    0,    0,    0,    0,    0,    0,
  149,  150,    0,    0,  150,  150,    0,  150,  150,    0,
  150,  149,  149,  149,  149,  150,  150,    0,    0,  150,
  150,  150,  150,  150,  150,  173,    0,  150,  150,  150,
    0,    0,    0,  150,    0,    0,    0,  150,  150,  150,
    0,    0,    0,  150,  150,  150,  150,    0,    0,  150,
    0,  150,  173,  173,  173,    0,    0,    0,    0,    0,
    0,    0,  150,  173,    0,    0,  173,  173,    0,  173,
  173,    0,  173,  150,  150,  150,  150,  173,  173,    0,
    0,  173,  173,  173,  173,  173,  173,  168,    0,  173,
  173,  173,    0,    0,    0,  173,    0,    0,    0,  173,
  173,  173,    0,    0,    0,  173,  173,  173,  173,    0,
    0,  173,    0,  173,  168,  168,  168,    0,    0,    0,
    0,    0,    0,    0,  173,  168,    0,    0,  168,  168,
    0,  168,  168,    0,  168,  173,  173,  173,  173,  168,
  168,    0,    0,  168,  168,  168,  168,  168,  168,  141,
    0,  168,  168,  168,    0,    0,    0,  168,    0,    0,
    0,  168,  168,  168,    0,    0,    0,  168,  168,  168,
  168,    0,    0,  168,    0,  168,  141,  141,  141,    0,
    0,    0,    0,    0,    0,    0,  168,  141,    0,    0,
  141,  141,    0,  141,  141,    0,  141,  168,  168,  168,
  168,  141,  141,    0,    0,  141,  141,  141,  141,  141,
  141,  151,    0,  141,  141,  141,    0,    0,    0,  141,
    0,    0,    0,  141,  141,  141,    0,    0,    0,  141,
  141,  141,  141,    0,    0,  141,    0,  141,  151,  151,
  151,    0,    0,    0,    0,    0,    0,    0,  141,  151,
    0,    0,  151,  151,    0,  151,  151,    0,  151,  141,
  141,  141,  141,  151,  151,    0,    0,  151,  151,  151,
  151,  151,  151,  142,    0,  151,  151,  151,    0,    0,
    0,  151,    0,    0,    0,  151,  151,  151,    0,    0,
    0,  151,  151,  151,  151,    0,    0,  151,    0,  151,
  142,  142,  142,    0,    0,    0,    0,    0,    0,    0,
  151,  142,    0,    0,  142,  142,    0,  142,  142,    0,
  142,  151,  151,  151,  151,  142,  142,    0,    0,  142,
  142,  142,  142,  142,  142,  152,    0,  142,  142,  142,
    0,    0,    0,  142,    0,    0,    0,  142,  142,  142,
    0,    0,    0,  142,  142,  142,  142,    0,    0,  142,
    0,  142,  152,  152,  152,    0,    0,    0,    0,    0,
    0,    0,  142,  152,    0,    0,  152,  152,    0,  152,
  152,    0,  152,  142,  142,  142,  142,  152,  152,    0,
    0,  152,  152,  152,  152,  152,  152,  143,    0,  152,
  152,  152,    0,    0,    0,  152,    0,    0,    0,  152,
  152,  152,    0,    0,    0,  152,  152,  152,  152,    0,
    0,  152,    0,  152,  143,  143,  143,    0,    0,    0,
    0,    0,    0,    0,  152,  143,    0,    0,  143,  143,
    0,  143,  143,    0,  143,  152,  152,  152,  152,  143,
  143,    0,    0,  143,  143,  143,  143,  143,  143,  144,
    0,  143,  143,  143,    0,    0,    0,  143,    0,    0,
    0,  143,  143,  143,    0,    0,    0,  143,  143,  143,
  143,    0,    0,  143,    0,  143,  144,  144,  144,    0,
    0,    0,    0,    0,    0,    0,  143,  144,    0,    0,
  144,  144,    0,  144,  144,    0,  144,  143,  143,  143,
  143,  144,  144,    0,    0,  144,  144,  144,  144,  144,
  144,  155,    0,  144,  144,  144,    0,    0,    0,  144,
    0,    0,    0,  144,  144,  144,    0,    0,    0,  144,
  144,  144,  144,    0,    0,  144,    0,  144,  155,  155,
  155,    0,    0,    0,    0,    0,    0,    0,  144,  155,
    0,    0,  155,  155,    0,  155,  155,    0,  155,  144,
  144,  144,  144,  155,  155,    0,    0,  155,  155,  155,
  155,  155,  155,  169,    0,  155,  155,  155,    0,    0,
    0,  155,    0,    0,    0,  155,  155,  155,    0,    0,
    0,  155,  155,  155,  155,    0,    0,  155,    0,  155,
  169,  169,  169,    0,    0,    0,    0,    0,    0,    0,
  155,  169,    0,    0,  169,  169,    0,  169,  169,    0,
  169,  155,  155,  155,  155,  169,  169,    0,    0,  169,
  169,  169,  169,  169,  169,    0,    0,  169,  169,  169,
    0,    0,    0,  169,    0,    0,    0,  169,  169,  169,
    0,    0,    0,  169,  169,  169,  169,    0,    0,  169,
    0,  169,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  169,  149,    0,    0,  150,  151,    0,  152,
  153,    0,  154,  169,  169,  169,  169,  155,  156,    0,
    0,  157,  158,  159,  160,  161,  162,    0,    0,  163,
  164,  165,    0,    0,    0,  166,    0,    0,    0,  167,
  168,  169,    0,    0,    0,  170,  171,  172,  173,    0,
    0,  174,    0,  175,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  176,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  177,  178,  179,  180,
  };
  protected static readonly short [] yyCheck = {             5,
    6,  110,   44,  131,  128,   33,  130,   40,   60,   42,
  138,  139,  123,  137,  142,  123,   40,   40,   42,   42,
  269,   61,   28,   29,   40,  265,   42,   41,   41,   44,
   44,   44,   44,   40,  101,   42,   40,  259,   42,  279,
   60,  114,   40,   49,   42,  125,    0,   44,   41,  292,
  123,   44,  286,  287,   60,   61,   62,   63,  292,   65,
  181,   60,  259,  312,   62,   62,  187,   73,  135,   65,
    0,   91,  294,  295,  296,  297,  298,   73,   41,  267,
  268,   44,   61,  125,  272,  273,   93,   91,   40,   40,
   42,   42,   91,   44,  197,  101,  284,  285,  201,   61,
  203,   61,   72,  123,  207,  293,  112,  289,  290,  291,
  125,  125,  125,  125,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  123,   61,   40,  275,   42,  135,
   44,   40,   40,   42,   42,   44,   44,   41,  387,   41,
   44,  257,   44,  149,  150,  151,  152,  153,  154,  155,
  156,  157,  158,  159,   40,  257,   42,  163,  164,  259,
  166,   41,  151,  152,   44,  414,  155,   41,  269,  270,
   44,  177,  178,  179,   60,   41,   61,   41,   44,  259,
   44,  292,  286,  287,  292,  259,  123,   33,  123,  266,
  266,  197,   40,   40,  123,  201,  123,  203,   40,   61,
  259,  207,  208,  292,   61,  257,  258,  384,  260,  261,
  262,  272,  264,  219,  294,  295,  296,  297,  298,  271,
  272,  191,   44,   44,   44,   44,  196,   44,  351,  199,
  200,  351,  202,  239,  204,  205,  206,  351,  271,  209,
  210,  211,  212,  263,  351,  351,  274,  271,   44,   44,
  256,   44,  272,  351,  260,  271,  276,  277,  278,  279,
  280,  281,  282,  283,  263,  125,  289,  290,  291,  351,
  351,  265,   44,  272,   44,   44,   44,  276,  277,  278,
  279,  280,  281,  282,  283,   44,   44,  411,  258,  295,
  296,  297,  298,  299,  300,  301,  302,  425,   60,  408,
   44,  271,  426,   44,  432,  275,   44,  277,  343,   44,
   44,  281,  282,  267,  268,   40,   91,   44,  272,  273,
   44,   44,   44,   44,  344,   44,   44,  326,   44,   91,
  284,  285,  257,  303,  304,  305,   44,  267,  268,  293,
   44,   44,  272,  273,  314,  351,  316,  317,  354,  319,
   44,  321,  322,  323,  284,  285,  326,  327,  328,  329,
   44,  123,   91,  293,   40,   44,  343,  343,  257,   44,
   44,  257,  258,   44,  260,  261,  262,   44,  264,  257,
  386,   93,   44,  389,   44,   44,  356,   44,  125,  359,
  273,  361,  273,    0,  273,  365,  366,  273,    9,   40,
  146,   67,  148,   88,  410,   92,   49,  112,  378,  379,
  380,  144,  272,  383,  160,  161,  162,  383,  120,  165,
  430,  167,  168,  169,  170,  171,  172,  173,  174,  440,
  420,  400,  183,  183,  188,  410,  311,  386,  408,  299,
  300,  301,  349,  389,   -1,   -1,   -1,   -1,   -1,   -1,
  310,   -1,   -1,  313,  314,   -1,  316,  317,   -1,  319,
   -1,   -1,   -1,   -1,  324,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,   -1,   -1,  336,  337,  338,   -1,
  125,  125,  342,   -1,   -1,   -1,  346,  347,  348,   -1,
   -1,   -1,  352,  353,  354,  355,   -1,   -1,  358,   -1,
  360,  263,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  371,   -1,   -1,  276,  277,  278,  279,  280,  281,
  282,  283,  382,  383,  384,  385,   -1,   40,   -1,   42,
   -1,   60,   -1,   -1,   91,  272,   93,   -1,   -1,   -1,
   -1,  287,  288,  289,   -1,  291,  292,   60,  294,  311,
  312,   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  299,  300,  301,   -1,  123,   -1,   -1,   -1,
   -1,   60,   -1,  310,   -1,   -1,  313,  314,   -1,  316,
  317,   60,  319,   -1,   -1,   -1,   -1,  324,  325,   -1,
  336,  328,  329,  330,  331,  332,  333,   -1,   -1,  336,
  337,  338,   60,   -1,  350,  342,   -1,   -1,   -1,  346,
  347,  348,   91,   -1,  259,  352,  353,  354,  355,   -1,
   -1,  358,   -1,  360,   -1,  371,  372,   60,  272,   -1,
   -1,  377,   -1,   91,  371,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  382,  383,  384,  385,  294,
  295,  296,  297,  298,  400,  299,  300,  301,   91,   -1,
   -1,  407,   -1,   -1,   -1,  123,  310,   -1,   -1,  313,
  314,   -1,  316,  317,  420,  319,   -1,   -1,   -1,   -1,
  324,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  123,   -1,  336,  337,  338,   -1,   -1,   -1,  342,   -1,
   -1,   -1,  346,  347,  348,   -1,  263,   -1,  352,  353,
  354,  355,   -1,   -1,  358,  272,  360,   -1,   -1,  276,
  277,  278,  279,  280,  281,  282,  283,  371,  257,  258,
   -1,  260,  261,  262,   -1,  264,   -1,   -1,  382,  383,
  384,  385,  271,  272,  257,  258,   -1,  260,  261,  262,
   -1,  264,  260,  261,   -1,   -1,   -1,   -1,  271,  272,
  289,  290,  291,   -1,   -1,   -1,   -1,   -1,  257,  258,
   -1,  260,  261,  262,   -1,  264,  289,  290,  291,   -1,
   -1,   -1,  271,  272,  263,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  272,   -1,   -1,   -1,  276,  277,  278,
  279,  280,  281,  282,  283,  263,   -1,   -1,   -1,  288,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,  276,  277,
  278,  279,  280,  281,  282,  283,   -1,   -1,   -1,   -1,
  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   -1,   -1,   -1,  276,  277,  278,  279,  280,  281,  282,
  283,  272,   -1,   -1,   -1,  363,  364,  365,  366,   -1,
   -1,   -1,   -1,   -1,  372,  373,  374,  375,  376,  377,
  378,  379,  380,  381,   -1,   -1,   -1,   -1,  299,  300,
  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  310,
   -1,   -1,  313,  314,   -1,  316,  317,   -1,  319,   -1,
   -1,   -1,   -1,  324,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  272,   -1,  336,  337,  338,   -1,   -1,
   -1,  342,   -1,   -1,   -1,  346,  347,  348,   -1,   -1,
   -1,  352,  353,  354,  355,   -1,   -1,  358,   -1,  360,
  299,  300,  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  371,  310,   -1,   -1,  313,  314,   -1,  316,  317,   -1,
  319,  382,  383,  384,  385,  324,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  272,   -1,  336,  337,  338,
   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,  353,  354,  355,   -1,   -1,  358,
   -1,  360,  299,  300,  301,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  371,  310,   -1,   -1,  313,  314,   -1,  316,
  317,   -1,  319,  382,  383,  384,  385,  324,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  272,   -1,  336,
  337,  338,   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,  353,  354,  355,   -1,
   -1,  358,   -1,  360,  299,  300,  301,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,  313,  314,
   -1,  316,  317,   -1,  319,  382,  383,  384,  385,  324,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  272,
   -1,  336,  337,  338,   -1,   -1,   -1,  342,   -1,   -1,
   -1,  346,  347,  348,   -1,   -1,   -1,  352,  353,  354,
  355,   -1,   -1,  358,   -1,  360,  299,  300,  301,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,
  313,  314,   -1,  316,  317,   -1,  319,  382,  383,  384,
  385,  324,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  272,   -1,  336,  337,  338,   -1,   -1,   -1,  342,
   -1,   -1,   -1,  346,  347,  348,   -1,   -1,   -1,  352,
  353,  354,  355,   -1,   -1,  358,   -1,  360,  299,  300,
  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,
   -1,   -1,  313,  314,   -1,  316,  317,   -1,  319,  382,
  383,  384,  385,  324,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  272,   -1,  336,  337,  338,   -1,   -1,
   -1,  342,   -1,   -1,   -1,  346,  347,  348,   -1,   -1,
   -1,  352,  353,  354,  355,   -1,   -1,  358,   -1,  360,
  299,  300,  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  371,  310,   -1,   -1,  313,  314,   -1,  316,  317,   -1,
  319,  382,  383,  384,  385,  324,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  272,   -1,  336,  337,  338,
   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,  353,  354,  355,   -1,   -1,  358,
   -1,  360,  299,  300,  301,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  371,  310,   -1,   -1,  313,  314,   -1,  316,
  317,   -1,  319,  382,  383,  384,  385,  324,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  272,   -1,  336,
  337,  338,   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,  353,  354,  355,   -1,
   -1,  358,   -1,  360,  299,  300,  301,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,  313,  314,
   -1,  316,  317,   -1,  319,  382,  383,  384,  385,  324,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  272,
   -1,  336,  337,  338,   -1,   -1,   -1,  342,   -1,   -1,
   -1,  346,  347,  348,   -1,   -1,   -1,  352,  353,  354,
  355,   -1,   -1,  358,   -1,  360,  299,  300,  301,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,
  313,  314,   -1,  316,  317,   -1,  319,  382,  383,  384,
  385,  324,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  272,   -1,  336,  337,  338,   -1,   -1,   -1,  342,
   -1,   -1,   -1,  346,  347,  348,   -1,   -1,   -1,  352,
  353,  354,  355,   -1,   -1,  358,   -1,  360,  299,  300,
  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,
   -1,   -1,  313,  314,   -1,  316,  317,   -1,  319,  382,
  383,  384,  385,  324,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  272,   -1,  336,  337,  338,   -1,   -1,
   -1,  342,   -1,   -1,   -1,  346,  347,  348,   -1,   -1,
   -1,  352,  353,  354,  355,   -1,   -1,  358,   -1,  360,
  299,  300,  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  371,  310,   -1,   -1,  313,  314,   -1,  316,  317,   -1,
  319,  382,  383,  384,  385,  324,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  272,   -1,  336,  337,  338,
   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,  353,  354,  355,   -1,   -1,  358,
   -1,  360,  299,  300,  301,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  371,  310,   -1,   -1,  313,  314,   -1,  316,
  317,   -1,  319,  382,  383,  384,  385,  324,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  272,   -1,  336,
  337,  338,   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,  353,  354,  355,   -1,
   -1,  358,   -1,  360,  299,  300,  301,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,  313,  314,
   -1,  316,  317,   -1,  319,  382,  383,  384,  385,  324,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  272,
   -1,  336,  337,  338,   -1,   -1,   -1,  342,   -1,   -1,
   -1,  346,  347,  348,   -1,   -1,   -1,  352,  353,  354,
  355,   -1,   -1,  358,   -1,  360,  299,  300,  301,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,   -1,   -1,
  313,  314,   -1,  316,  317,   -1,  319,  382,  383,  384,
  385,  324,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  272,   -1,  336,  337,  338,   -1,   -1,   -1,  342,
   -1,   -1,   -1,  346,  347,  348,   -1,   -1,   -1,  352,
  353,  354,  355,   -1,   -1,  358,   -1,  360,  299,  300,
  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  371,  310,
   -1,   -1,  313,  314,   -1,  316,  317,   -1,  319,  382,
  383,  384,  385,  324,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  272,   -1,  336,  337,  338,   -1,   -1,
   -1,  342,   -1,   -1,   -1,  346,  347,  348,   -1,   -1,
   -1,  352,  353,  354,  355,   -1,   -1,  358,   -1,  360,
  299,  300,  301,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  371,  310,   -1,   -1,  313,  314,   -1,  316,  317,   -1,
  319,  382,  383,  384,  385,  324,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,   -1,   -1,  336,  337,  338,
   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,  353,  354,  355,   -1,   -1,  358,
   -1,  360,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  371,  310,   -1,   -1,  313,  314,   -1,  316,
  317,   -1,  319,  382,  383,  384,  385,  324,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,   -1,   -1,  336,
  337,  338,   -1,   -1,   -1,  342,   -1,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,  353,  354,  355,   -1,
   -1,  358,   -1,  360,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  371,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  382,  383,  384,  385,
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
  public const int NOALIAS = 288;
  public const int NONNULL = 289;
  public const int NOCAPTURE = 290;
  public const int WRITEONLY = 291;
  public const int ATTRIBUTE_GROUP_REF = 292;
  public const int ATTRIBUTES = 293;
  public const int NORECURSE = 294;
  public const int NOUNWIND = 295;
  public const int SSP = 296;
  public const int UWTABLE = 297;
  public const int ARGMEMONLY = 298;
  public const int RET = 299;
  public const int BR = 300;
  public const int SWITCH = 301;
  public const int INDIRECTBR = 302;
  public const int INVOKE = 303;
  public const int RESUME = 304;
  public const int CATCHSWITCH = 305;
  public const int CATCHRET = 306;
  public const int CLEANUPRET = 307;
  public const int UNREACHABLE = 308;
  public const int FNEG = 309;
  public const int ADD = 310;
  public const int NUW = 311;
  public const int NSW = 312;
  public const int FADD = 313;
  public const int SUB = 314;
  public const int FSUB = 315;
  public const int MUL = 316;
  public const int FMUL = 317;
  public const int UDIV = 318;
  public const int SDIV = 319;
  public const int FDIV = 320;
  public const int UREM = 321;
  public const int SREM = 322;
  public const int FREM = 323;
  public const int SHL = 324;
  public const int LSHR = 325;
  public const int EXACT = 326;
  public const int ASHR = 327;
  public const int AND = 328;
  public const int OR = 329;
  public const int XOR = 330;
  public const int EXTRACTELEMENT = 331;
  public const int INSERTELEMENT = 332;
  public const int SHUFFLEVECTOR = 333;
  public const int EXTRACTVALUE = 334;
  public const int INSERTVALUE = 335;
  public const int ALLOCA = 336;
  public const int LOAD = 337;
  public const int STORE = 338;
  public const int FENCE = 339;
  public const int CMPXCHG = 340;
  public const int ATOMICRMW = 341;
  public const int GETELEMENTPTR = 342;
  public const int ALIGN = 343;
  public const int INBOUNDS = 344;
  public const int INRANGE = 345;
  public const int TRUNC = 346;
  public const int ZEXT = 347;
  public const int SEXT = 348;
  public const int FPTRUNC = 349;
  public const int FPEXT = 350;
  public const int TO = 351;
  public const int FPTOUI = 352;
  public const int FPTOSI = 353;
  public const int UITOFP = 354;
  public const int SITOFP = 355;
  public const int PTRTOINT = 356;
  public const int INTTOPTR = 357;
  public const int BITCAST = 358;
  public const int ADDRSPACECAST = 359;
  public const int ICMP = 360;
  public const int EQ = 361;
  public const int NE = 362;
  public const int UGT = 363;
  public const int UGE = 364;
  public const int ULT = 365;
  public const int ULE = 366;
  public const int SGT = 367;
  public const int SGE = 368;
  public const int SLT = 369;
  public const int SLE = 370;
  public const int FCMP = 371;
  public const int OEQ = 372;
  public const int OGT = 373;
  public const int OGE = 374;
  public const int OLT = 375;
  public const int OLE = 376;
  public const int ONE = 377;
  public const int ORD = 378;
  public const int UEQ = 379;
  public const int UNE = 380;
  public const int UNO = 381;
  public const int PHI = 382;
  public const int SELECT = 383;
  public const int CALL = 384;
  public const int TAIL = 385;
  public const int VA_ARG = 386;
  public const int LANDINGPAD = 387;
  public const int CATCHPAD = 388;
  public const int CLEANUPPAD = 389;
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
