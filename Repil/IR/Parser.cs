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

  protected const int yyFinal = 9;
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
//t    "module_part : global_variable",
//t    "module_part : ATTRIBUTES ATTRIBUTE_GROUP_REF '=' '{' attributes '}'",
//t    "module_part : META_SYMBOL_DEF '=' '!' '{' '}'",
//t    "module_part : META_SYMBOL_DEF '=' '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL_DEF '=' META_SYMBOL '(' metadata_args ')'",
//t    "module_part : META_SYMBOL_DEF '=' DISTINCT '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL_DEF '=' DISTINCT META_SYMBOL '(' metadata_args ')'",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type constant ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type constant ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
//t    "global_kind : GLOBAL",
//t    "global_kind : CONSTANT",
//t    "linkage : EXTERNAL",
//t    "linkage : INTERNAL",
//t    "visibility : PRIVATE",
//t    "metadata_args : metadata_arg",
//t    "metadata_args : metadata_args ',' metadata_arg",
//t    "metadata_arg : SYMBOL ':' SYMBOL",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL",
//t    "metadata_arg : SYMBOL ':' STRING",
//t    "metadata_arg : SYMBOL ':' constant",
//t    "metadata_arg : TYPE ':' META_SYMBOL",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' ')'",
//t    "metadata_kvs : META_SYMBOL META_SYMBOL",
//t    "metadata_kvs : metadata_kvs META_SYMBOL META_SYMBOL",
//t    "metadata : metadatum",
//t    "metadata : metadata META_SYMBOL",
//t    "metadata : metadata ',' typed_value",
//t    "metadata : metadata ',' META_SYMBOL",
//t    "metadata : metadata ',' NULL",
//t    "metadatum : typed_value",
//t    "metadatum : META_SYMBOL",
//t    "metadatum : NULL",
//t    "attributes : attribute",
//t    "attributes : attributes attribute",
//t    "attribute : NORECURSE",
//t    "attribute : NOUNWIND",
//t    "attribute : READNONE",
//t    "attribute : SPECULATABLE",
//t    "attribute : SSP",
//t    "attribute : UWTABLE",
//t    "attribute : ARGMEMONLY",
//t    "attribute : STRING '=' STRING",
//t    "attribute : STRING",
//t    "attribute : SYMBOL",
//t    "attribute : READONLY",
//t    "attribute : SYMBOL '(' metadata_value_args ')'",
//t    "literal_structure : '{' type_list '}'",
//t    "type_list : type",
//t    "type_list : type_list ',' type",
//t    "return_type : type",
//t    "return_type : VOID",
//t    "type : literal_structure",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I1",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
//t    "type : return_type '(' function_type_args ')'",
//t    "type : type '*'",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_type_args : function_type_arg",
//t    "function_type_args : function_type_args ',' function_type_arg",
//t    "function_type_arg : type",
//t    "function_type_arg : ELLIPSIS",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' ')' attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "function_declaration : DECLARE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list ',' parameter",
//t    "parameter : type",
//t    "parameter : type parameter_attributes",
//t    "parameter : METADATA",
//t    "parameter : ELLIPSIS",
//t    "parameter_attributes : parameter_attribute",
//t    "parameter_attributes : parameter_attributes parameter_attribute",
//t    "parameter_attribute : NONNULL",
//t    "parameter_attribute : NOCAPTURE",
//t    "parameter_attribute : READONLY",
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
//t    "value : INTTOPTR '(' typed_value TO type ')'",
//t    "value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "pointer_value : value",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : UNDEF",
//t    "constant : ZEROINITIALIZER",
//t    "constant : CONSTANT_BYTES",
//t    "constant : '<' typed_constants '>'",
//t    "constant : '{' typed_values '}'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_value : VOID",
//t    "typed_pointer_value : type pointer_value",
//t    "typed_values : typed_value",
//t    "typed_values : typed_values ',' typed_value",
//t    "typed_constant : type constant",
//t    "typed_constants : typed_constant",
//t    "typed_constants : typed_constants ',' typed_constant",
//t    "element_index : typed_value",
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "blocks : block",
//t    "blocks : blocks block",
//t    "block : assignments terminator_instruction",
//t    "block : assignments terminator_instruction metadata_kvs",
//t    "block : terminator_instruction",
//t    "block : terminator_instruction metadata_kvs",
//t    "assignments : assignment",
//t    "assignments : assignments assignment",
//t    "assignment : instruction",
//t    "assignment : instruction metadata_kvs",
//t    "assignment : LOCAL_SYMBOL '=' instruction",
//t    "assignment : LOCAL_SYMBOL '=' instruction metadata_kvs",
//t    "function_pointer : value",
//t    "function_args : function_arg",
//t    "function_args : function_args ',' function_arg",
//t    "function_arg : type value",
//t    "function_arg : type parameter_attributes value",
//t    "function_arg : METADATA type LOCAL_SYMBOL",
//t    "function_arg : METADATA type constant",
//t    "function_arg : METADATA META_SYMBOL",
//t    "function_arg : METADATA META_SYMBOL '(' ')'",
//t    "function_arg : METADATA META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_value_args : metadata_value_arg",
//t    "metadata_value_args : metadata_value_args ',' metadata_value_arg",
//t    "metadata_value_arg : constant",
//t    "metadata_value_arg : SYMBOL",
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
//t    "calling_convention : FASTCC",
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "instruction : ADD type value ',' value",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL return_type function_pointer '(' function_args ')'",
//t    "instruction : CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')'",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer '(' function_args ')'",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FCMP fcmp_condition type value ',' value",
//t    "instruction : FDIV type value ',' value",
//t    "instruction : FMUL type value ',' value",
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
//t    "instruction : FSUB type value ',' value",
//t    "instruction : GETELEMENTPTR type ',' typed_value ',' element_indices",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : INSERTELEMENT typed_value ',' typed_value ',' typed_value",
//t    "instruction : LOAD type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LSHR type value ',' value",
//t    "instruction : LSHR EXACT type value ',' value",
//t    "instruction : OR type value ',' value",
//t    "instruction : MUL type value ',' value",
//t    "instruction : MUL wrappings type value ',' value",
//t    "instruction : PHI type phi_vals",
//t    "instruction : PTRTOINT typed_value TO type",
//t    "instruction : SDIV type value ',' value",
//t    "instruction : SELECT type value ',' typed_value ',' typed_value",
//t    "instruction : SEXT typed_value TO type",
//t    "instruction : SHL type value ',' value",
//t    "instruction : SHL wrappings type value ',' value",
//t    "instruction : SHUFFLEVECTOR typed_value ',' typed_value ',' typed_value",
//t    "instruction : SITOFP typed_value TO type",
//t    "instruction : STORE typed_value ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : SUB type value ',' value",
//t    "instruction : SUB wrappings type value ',' value",
//t    "instruction : TRUNC typed_value TO type",
//t    "instruction : UDIV type value ',' value",
//t    "instruction : UITOFP typed_value TO type",
//t    "instruction : UREM type value ',' value",
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
    null,null,null,null,null,null,"':'",null,"'<'","'='","'>'",null,null,
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
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","META_SYMBOL_DEF",
    "SYMBOL","DISTINCT","METADATA","CONSTANT_BYTES","TYPE","HALF","FLOAT",
    "DOUBLE","I1","I8","I16","I32","I64","ZEROINITIALIZER","DEFINE",
    "DECLARE","UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS",
    "GLOBAL","CONSTANT","PRIVATE","INTERNAL","EXTERNAL","FASTCC",
    "NONNULL","NOCAPTURE","WRITEONLY","READONLY","ATTRIBUTE_GROUP_REF",
    "ATTRIBUTES","NORECURSE","NOUNWIND","READNONE","SPECULATABLE","SSP",
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
#line 59 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 63 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 67 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 71 "Repil/IR/IR.jay"
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
case 12:
#line 91 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 13:
#line 95 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 14:
  case_14();
  break;
case 15:
#line 104 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 16:
  case_16();
  break;
case 17:
#line 116 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (bool)yyVals[-6+yyTop], (LType)yyVals[-5+yyTop], (Constant)yyVals[-4+yyTop], isPrivate: false);
    }
  break;
case 18:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (bool)yyVals[-5+yyTop], (LType)yyVals[-4+yyTop], (Constant)yyVals[-3+yyTop], isPrivate: true);
    }
  break;
case 19:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (bool)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: true);
    }
  break;
case 20:
#line 128 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 21:
#line 129 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 25:
  case_25();
  break;
case 26:
  case_26();
  break;
case 27:
#line 155 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 28:
#line 156 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 29:
#line 157 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 30:
#line 158 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 31:
#line 159 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 32:
#line 163 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 33:
#line 167 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 34:
#line 174 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 35:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 36:
#line 185 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 37:
#line 189 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 38:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 39:
#line 197 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 40:
#line 201 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 58:
  case_58();
  break;
case 59:
#line 242 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 60:
#line 246 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 62:
#line 251 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 64:
#line 256 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 65:
#line 257 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 66:
#line 258 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 67:
#line 259 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 68:
#line 260 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 69:
#line 261 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 70:
#line 262 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 71:
#line 263 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 72:
#line 267 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 73:
#line 271 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 74:
#line 275 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 75:
#line 279 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 76:
#line 283 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 77:
#line 290 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 78:
#line 294 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 80:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 81:
#line 309 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 313 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 83:
#line 317 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 84:
#line 321 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 85:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 86:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 87:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 88:
#line 337 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 89:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 90:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 91:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 359 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 93:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 370 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 95:
#line 374 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 97:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 99:
#line 390 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 100:
#line 394 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 101:
#line 395 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 102:
#line 396 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 103:
#line 397 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 109:
#line 415 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 110:
#line 416 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 111:
#line 417 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 112:
#line 418 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 113:
#line 419 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 114:
#line 420 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 115:
#line 421 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 116:
#line 422 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 117:
#line 423 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 118:
#line 424 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 119:
#line 428 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 120:
#line 429 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 121:
#line 430 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 122:
#line 431 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 123:
#line 432 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 124:
#line 433 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 125:
#line 434 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 126:
#line 435 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 127:
#line 436 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 128:
#line 437 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 129:
#line 438 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 130:
#line 439 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 131:
#line 440 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 132:
#line 441 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 133:
#line 442 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 134:
#line 443 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 136:
#line 448 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 137:
#line 449 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 138:
#line 453 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 139:
#line 457 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 465 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 142:
#line 466 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 143:
#line 467 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 144:
#line 468 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 145:
#line 469 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 146:
#line 470 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 147:
#line 471 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 148:
#line 472 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 149:
#line 476 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 150:
#line 480 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 487 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 494 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 498 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 154:
#line 505 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 512 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 516 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 157:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 530 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 534 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 545 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 549 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 556 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 167:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 169:
#line 586 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 590 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 597 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 601 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 173:
#line 605 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 609 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 176:
#line 620 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 624 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 179:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 180:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 181:
#line 643 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 182:
#line 647 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 183:
#line 651 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 184:
#line 655 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 189:
#line 672 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 676 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 682 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 192:
#line 689 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 693 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 721 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 725 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 729 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 204:
#line 740 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 744 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 748 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 752 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 756 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 760 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 210:
#line 764 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 211:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 212:
#line 772 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 213:
#line 776 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 214:
#line 780 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 784 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 227:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 228:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 229:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 242:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 920 "Repil/IR/IR.jay"
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
#line 73 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 78 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_10()
#line 83 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_14()
#line 97 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_16()
#line 106 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_25()
#line 143 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_26()
#line 148 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_58()
#line 232 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    6,    6,    6,   11,
   11,   16,   16,   15,    9,    9,   17,   17,   17,   17,
   17,   17,   17,   14,   14,    8,    8,    8,    8,    8,
   19,   19,   19,    7,    7,   21,   21,   21,   21,   21,
   21,   21,   21,   21,   21,   21,   21,    3,   22,   22,
   23,   23,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   24,   24,   25,   25,
    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,
    5,   26,   26,   30,   30,   30,   30,   31,   31,   32,
   32,   32,   32,   10,   10,   27,   27,   33,   34,   34,
   34,   34,   34,   34,   34,   34,   34,   34,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   36,   36,   36,   36,   36,   38,
   13,   13,   13,   13,   13,   13,   13,   13,   13,   13,
   41,   20,   20,   42,   40,   40,   43,   39,   39,   44,
   37,   37,   28,   28,   45,   45,   45,   45,   46,   46,
   48,   48,   48,   48,   50,   51,   51,   52,   52,   52,
   52,   52,   52,   52,   18,   18,   53,   53,   54,   54,
   55,   56,   56,   57,   58,   58,   59,   59,   29,   47,
   47,   47,   47,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    1,
    6,    5,    6,    6,    7,    7,   10,   10,    9,    1,
    1,    1,    1,    1,    1,    3,    3,    3,    3,    3,
    3,    6,    5,    2,    3,    1,    2,    3,    3,    3,
    1,    1,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    3,    1,    1,    1,    4,    3,    1,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    4,    2,    1,    5,    5,    1,    3,    1,    1,
   11,   11,   11,   10,   12,   12,   13,   13,    7,    8,
    8,    1,    3,    1,    2,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    6,    9,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    3,
    2,    2,    1,    2,    1,    3,    2,    1,    3,    1,
    1,    3,    1,    2,    2,    3,    1,    2,    1,    2,
    1,    2,    3,    4,    1,    1,    3,    2,    3,    3,
    3,    2,    4,    5,    1,    3,    1,    1,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    1,    2,
    7,    2,    7,    5,    6,    5,    5,    4,    6,    7,
    8,    7,    8,    4,    5,    6,    5,    5,    4,    4,
    5,    6,    7,    6,    6,    7,    5,    6,    5,    5,
    6,    3,    4,    5,    7,    4,    5,    6,    6,    4,
    7,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   62,   74,   64,   65,   66,   67,   68,   69,   70,   71,
    0,   23,   22,    0,    0,    0,   63,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  104,  105,   24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   73,  199,    0,    0,    0,    0,    0,
    0,    5,    6,   20,   21,    0,    0,    0,    7,    0,
    0,    0,    0,    0,   58,    0,    0,    0,    0,    0,
   80,    0,    0,   77,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   25,    0,    0,    0,   43,   42,   12,
    0,    0,   36,   41,    0,    0,    0,    0,    0,   96,
   97,    0,    0,    0,   92,   72,    0,    0,    0,    0,
    0,   56,   46,   47,   48,   49,   50,   51,   52,    0,
   44,  143,  142,  144,  145,  146,  141,  148,  147,    0,
    0,    0,    0,    0,    0,    0,   14,    0,    0,    0,
   37,   13,    0,  137,  136,    0,    0,  135,  152,    0,
   75,   76,    0,  108,    0,    0,  106,  100,  101,  103,
  102,    0,   98,    0,    0,   78,    0,    0,    0,    0,
   11,   45,  155,    0,    0,    0,  158,    0,    0,    0,
   29,    0,   27,   30,   31,   26,   16,   15,   40,   39,
   38,    0,    0,    0,    0,    0,    0,    0,  107,   99,
    0,    0,   93,    0,    0,    0,   53,  188,  187,    0,
  185,  150,    0,  157,    0,  149,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   34,    0,    0,    0,    0,
    0,    0,   57,    0,  156,  159,    0,    0,   19,   33,
    0,    0,    0,    0,    0,    0,   35,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  163,
    0,    0,  169,    0,    0,    0,    0,  186,    0,   18,
   32,    0,    0,    0,    0,    0,    0,    0,  202,    0,
    0,  200,    0,  197,  198,    0,    0,  195,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  109,  110,  111,  112,  113,  114,  115,
  116,  117,  118,    0,  119,  120,  131,  132,  133,  134,
  122,  124,  125,  126,  127,  123,  121,  129,  130,  128,
    0,    0,    0,    0,    0,   84,  164,    0,  170,    0,
    0,    0,    0,    0,    0,  138,    0,    0,    0,   83,
    0,  151,    0,    0,    0,    0,  196,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  189,    0,  175,    0,
    0,    0,    0,   81,    0,   82,    0,   86,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  214,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   85,
  160,    0,  161,   87,   88,    0,    0,  204,    0,  215,
  242,    0,  221,  230,    0,  218,  245,  234,  217,  247,
  237,    0,    0,  227,  207,  229,  248,    0,    0,  206,
  140,  154,    0,    0,    0,    0,    0,    0,    0,  190,
    0,    0,    0,    0,  176,    0,    0,  139,    0,    0,
    0,    0,  192,  205,  243,  231,  238,  228,  225,  239,
    0,    0,    0,    0,  224,  216,    0,    0,    0,    0,
    0,  178,    0,    0,    0,    0,  162,  201,    0,  203,
  193,  226,  241,    0,  191,  235,    0,  180,  181,  179,
    0,  177,    0,    0,  194,  183,    0,    0,  213,  184,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  140,  111,  103,   51,
   76,  112,  168,  218,   52,   39,  104,  230,  113,  531,
  141,   60,   61,   93,   94,  124,  176,  309,   66,  125,
  182,  183,  177,  384,  401,  469,  532,  562,  196,  194,
  332,  509,  581,  533,  310,  311,  312,  313,  314,  470,
  574,  575,  231,  466,  467,  582,  583,  337,  338,
  };
  protected static readonly short [] yySindex = {         1222,
  -14,  -58,   28,   38,   83, 2509, 2589, -255,    0, 1222,
    0,    0,    0,    0, -192,   87,  105,  -67, -162,  -21,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2793,    0,    0, 2793, -100,  -83,    0,  140, -115,  -33,
 2793,  -32,  137,    0,    0,  -57,  -22,    0,    0,    0,
    2,   41,   41,  159,  188,  -17,  180,  -31,  140,  -23,
  203,   -2,   45,    0,    0, 2793,  267, 2562,  -30,  288,
  202,    0,    0,    0,    0, 2793,    2,    2,    0, -155,
  290,  211, 2623,  301,    0, 2793, 2793, 2793,  -29, 2407,
    0,  140,   56,    0,  302, 2537, 1361, 1493, 2793, 2793,
  286,  289,   64,    0, -155, 2651,    0,    0,    0,    0,
  -15,  158,    0,    0, 2537,  140,    6,  -19,  314,    0,
    0, -228,   -8,  108,    0,    0, 2562, 2537,  109,  297,
  316,    0,    0,    0,    0,    0,    0,    0,    0, -103,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2820,
 2793,  315, 1493,   96, 2479,   89,    0, -155,  114,  -12,
    0,    0, 2689,    0,    0,    9,  330,    0,    0,  118,
    0,    0, 2537,    0,   66, -230,    0,    0,    0,    0,
    0, -167,    0, -228, 2537,    0,  120, -228,  113, 1881,
    0,    0,    0,  -18, 1493,    7,    0,   15,  332,   19,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  344, 2820,   41,  134, -230,  112, -105,    0,    0,
   66, -230,    0,   66,   66,   66,    0,    0,    0,  139,
    0,    0, 2820,    0, 2793,    0,  129,   29,  131,   91,
 2793,   24,   66,   41,  -87,    0,  119, 3352, -108,  -86,
   66,   66,    0, 1881,    0,    0,  128,  145,    0,    0,
  149,  179, 2793,  -92,   66, 3352,    0,  345, 2820, -207,
 2820, 2329, 2793, 2329, 2793, 2329, 2793, 2793, 2793, 2793,
 2793, 2329,  -36, 2793, 2793, 2793, 2820, 2820, 2820, 2793,
 2793, 2820,  588, 2820, 2820, 2820, 2820, 2820, 2820, 2820,
 2820, 2820,  793, 2730, 2793, 2793, 2793,   10, 1366,    0,
 3352,  128,    0,  128, 3352,  -85, 3352,    0,  130,    0,
    0, 2820,  295, 3352,  -84,  -90, 1444, 3736,    0,  133,
 1335,    0,  366,    0,    0,  158, 2329,    0,  158,  158,
 2329,  158,  158, 2329,  158,  158,  158,  158,  158,  158,
 2329, 2793,  158,  158,  158,  158,  367,  368,  369,  260,
  270,  373, 2793,  282,   57,   59,   60,   67,   68,   69,
   71,   73,   75,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 2793,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2793,    4,  158,  103, 2447,    0,    0,  128,    0,  130,
  130, 1521, 3352, 1599,  388,    0, 1677, 3352, 3352,    0,
  128,    0,  398,  192,  399,  158,    0,  414,  415,  158,
  417,  418,  158,  419,  420,  421,  426,  429,  432,  158,
  158,  434,  438,  441,  455, 2820, 2820, 2820,  142, 2793,
 2793,  285, 2820, 2793, 2793, 2793, 2793, 2793, 2793, 2793,
 2793, 2793,  158,  158, 1335,  457,    0,  458,    0,  463,
  103, 2793,  130,    0, 1754,    0, 2820,    0, 1832, 1910,
  130,  192,  422, 1335,  461, 1335, 1335,  465, 1335, 1335,
  466, 1335, 1335, 1335, 1335, 1335, 1335,  470,  472, 1335,
 1335, 1335, 1335,    0,  473,  475,  263,  158,  478,  480,
 2820,  482,  140,  140,  140,  140,  140,  140,  140,  140,
  140,  484,  485,  490,  444, 2820, 2715,  497,  103,    0,
    0,  150,    0,    0,    0,  494, 2793,    0, 1335,    0,
    0, 1335,    0,    0, 1335,    0,    0,    0,    0,    0,
    0, 1335, 1335,    0,    0,    0,    0, 2820, 2820,    0,
    0,    0,  181,  185,  501, 2820, 1335, 1335, 1335,    0,
  502, 2741, 1258,  151,    0, 2715,  508,    0, 2820,  192,
  505, 2766,    0,    0,    0,    0,    0,    0,    0,    0,
  296,  299, 2820,  514,    0,    0,  459, 2820,  519, 2379,
 1280,    0,   66, 2715,  176, 2715,    0,    0,  192,    0,
    0,    0,    0,  514,    0,    0, 1400,    0,    0,    0,
   66,    0,   66,  178,    0,    0,  219,   66,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  560,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  136,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -27,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   12,    0,    0,    0,    0,    0,  521,    0,    0,
    0,    0,    0,    0,    0,    0,  387,    0,    0,    0,
    0,  521,    0,    0,    0,    5,  521,  521,    0,    0,
    0,    0,   74,    0,    0,    0,    0,    0,    0,  354,
 1197,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  521,  521,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  221,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  521,    0,    0,    0,    0,    0,
    0,  245,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  283,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  350,  383,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  521,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 1987,    0, 3429,    0,    0,    0,    0, 1350,    0,
    0,    0,  521,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  521,    0,    0,  521,  521,
    0,  521,  521,    0,  521,  521,  521,  521,  521,  521,
    0,    0,  521,  521,  521,  521,    0,    0,    0,  521,
  521,    0,    0,  521,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  521,  521,    0,    0,    0,    0, 2065,    0, 2143,
 3506,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3583,    0,    0,    0,    0,  521,    0,    0,    0,  521,
    0,    0,  521,    0,    0,    0,    0,    0,    0,  521,
  521,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  521,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  521,  521,    0, 2813,    0,    0,    0,    0,
    0,    0, 2220,    0,    0,    0,    0,    0,    0,    0,
 3660,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  521,    0,    0,
    0,    0,  464,  551,  628,  705,  782,  869,  946, 1023,
 1100,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  521,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 2890,    0,    0,    0,    0,  247,  521,
    0,    0, 2967,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3044,    0,    0,    0,    0,    0,    0,
 3121,    0, 3198,    0,    0,    0,    0, 3275,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  552,  509,    0,    0,    0,    0,  462,  460,  -11,
  262,   -6,  -94,  276,    0,  546,  411, -237,    0,   21,
  435,    0,   -1,    0,  450,  -42,  -96,  123,  173,  400,
   11, -180, -110,    0,    0,  141, -522,    0,    0,    0,
 -410,  132, -132,    8,  774,    0,  275,  281,  265, -451,
 -549,  -10,  341,    0,   72,    0,   14, -191, -218,
  };
  protected static readonly short [] yyTable = {            38,
   38,  220,  261,  152,   40,   42,   68,   68,   68,   68,
   68,   57,   61,  483,  315,   82,   59,  248,  197,  528,
   86,  191,   64,   35,   38,  233,  605,   59,  163,   58,
  324,  163,  419,   64,   38,  266,  317,  413,  418,   69,
   77,   78,  217,  594,   61,   64,   15,   64,   60,   43,
  235,   61,   79,  129,   36,   79,  624,  330,  199,   38,
  204,   92,   48,   49,   89,  219,   45,  171,  236,   98,
  614,  536,  170,  172,  174,  331,  174,  577,  216,  116,
  117,  118,  341,  123,  344,  187,   34,  222,   18,  123,
  351,  226,  153,  154,  465,  229,  126,   59,   19,  127,
  234,   85,  256,  114,  157,  219,  232,  158,  123,  162,
  175,  219,  208,   61,   94,  219,   54,   94,  427,  101,
   92,  123,  427,  102,  249,  427,  114,  251,  252,   60,
  215,  260,  427,  178,  179,  180,  181,   64,  219,  200,
  219,  219,   68,   20,  195,  229,  264,   46,  184,  188,
  151,  185,  185,  219,  207,  130,   62,  158,  214,  229,
  224,  185,  151,  185,  217,   47,  123,  247,  326,  608,
  193,  131,  221,   63,  244,   61,  225,  185,  123,  253,
  217,   64,  254,  211,   65,  247,  247,  247,  247,  321,
  578,  603,  254,  579,  604,   61,  174,   71,  625,   64,
  132,   72,  243,  133,  134,  135,  136,  137,  138,  139,
   16,   17,  174,  150,  174,  219,  623,  151,  629,  604,
   64,  604,  322,   48,   49,  150,   21,   80,  195,   50,
   32,   33,  265,  242,  262,   22,   73,   67,   70,   84,
   95,  119,   68,   23,   24,   25,   26,   27,   28,   29,
   30,   55,  169,  255,   56,   81,  323,  161,   61,  630,
  161,   95,  254,   87,   95,  336,  339,  340,  342,  343,
  345,  346,  347,  348,  349,  350,  353,  354,  355,  356,
  150,   34,   89,  360,  361,   28,  364,  182,   28,  329,
  182,  333,  178,  179,  180,  181,   74,   75,  402,  403,
   38,   64,   83,  449,  352,  404,   90,  357,  358,  359,
   88,   64,  362,  450,  365,  366,  367,  368,  369,  370,
  371,  372,  373,   64,   97,  453,   64,   96,  511,  105,
  426,   48,   49,  106,  430,  416,   64,  433,   99,  100,
  115,  128,  415,  155,  440,  441,  156,  142,  143,   90,
  144,  145,  146,  173,  147,  190,  452,  189,  198,  142,
  143,  205,  144,  145,  146,  228,  147,  212,  148,  213,
  174,  227,  237,  164,  165,  238,  239,  463,  149,  627,
  148,  240,   91,  241,  246,  257,  258,  259,  327,  263,
  149,  267,   61,   61,  464,   61,   61,   61,   38,   61,
  217,  320,  247,  471,  422,  328,   61,   61,  405,  424,
  446,  447,  448,   61,  142,  143,  451,  144,  145,  146,
  220,  147,  454,   61,  455,  456,   62,  153,  164,  165,
  153,  477,  457,  458,  459,  148,  460,  412,  461,  414,
  462,  482,  484,  508,  508,  149,  417,  513,  514,  515,
  516,  517,  518,  519,  520,  521,  330,  486,  487,  166,
  489,  490,  492,  493,  494,   38,  504,  505,  506,  495,
  529,  423,  496,  512,  167,  497,  425,  500,   54,  428,
  429,  501,  431,  432,  502,  434,  435,  436,  437,  438,
  439,  245,   61,  442,  443,  444,  445,  250,  503,  507,
  525,  526,  527,   61,  539,  619,  621,   61,  542,  545,
  219,  153,  537,  552,  166,  553,  558,  219,  559,  560,
  573,  563,  229,  564,  316,  566,  628,  567,  568,  167,
  195,  565,  319,  569,  465,  475,  576,  580,  591,  325,
  479,  480,  592,  468,  593,  598,  571,  606,  609,   89,
   89,  615,  612,   89,   89,  613,   89,  579,  617,    1,
   61,   44,   79,   53,  159,  600,  485,  160,  206,  573,
  488,   89,   89,  491,  192,  195,  186,  472,  589,  590,
  498,  499,  510,  601,  223,  408,  607,  410,   89,  411,
   61,  409,  421,  622,  318,  611,  570,  573,    0,  573,
    0,    0,    0,  522,  523,  524,    0,    0,    0,    0,
    0,    0,   54,    0,    0,    0,   90,   90,  616,    0,
   90,   90,    0,   90,  538,    0,  540,  541,   54,  543,
  544,    0,  546,  547,  548,  549,  550,  551,   90,   90,
  554,  555,  556,  557,    0,    0,    0,   35,  561,   91,
   91,    0,    0,   91,   91,   90,   91,   54,  153,  153,
   54,   54,   54,   54,   54,   54,   54,   61,    0,    0,
    0,   91,   91,    0,    0,    0,    0,    0,   36,  584,
    0,    0,  585,  473,    0,  586,    0,    0,   91,    0,
    0,    0,  587,  588,    0,    0,  481,    0,    0,    0,
  153,  153,  153,    0,    0,    0,    0,  595,  596,  597,
   34,  153,    0,  602,  153,  153,  153,  153,  153,  153,
  153,  153,  153,    0,    0,  153,  153,    0,    0,  153,
  153,  153,  153,  153,  153,  244,  244,  153,  153,  153,
    0,  620,    0,  153,   61,    0,    0,  153,  153,  153,
    0,    0,  153,  153,  153,  153,  153,  153,    0,  153,
    0,  153,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  153,    0,    0,    0,    0,  244,  244,  244,
    0,    0,    0,  153,  153,  153,  153,    0,  244,    0,
    0,  244,  244,  244,  244,  244,  244,  244,  244,  244,
    0,    0,  244,  244,    0,    0,  244,  244,  244,  244,
  244,  244,    0,    0,  244,  244,  244,    0,    0,    0,
  244,   61,  249,  249,  244,  244,  244,    0,    0,    0,
  244,  244,  244,  244,  244,    0,  244,    0,  244,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  244,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
  244,  244,  244,  244,  249,  249,  249,   23,   24,   25,
   26,   27,   28,   29,   30,  249,    0,    0,  249,  249,
  249,  249,  249,  249,  249,  249,  249,    0,    0,  249,
  249,    0,    0,  249,  249,  249,  249,  249,  249,  236,
  236,  249,  249,  249,    0,    0,    0,  249,   61,    0,
    0,  249,  249,  249,    0,    0,    0,  249,  249,  249,
  249,  249,    0,  249,    0,  249,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  249,    0,    0,    0,
    0,  236,  236,  236,    0,    0,  363,  249,  249,  249,
  249,    0,  236,    0,    0,  236,  236,  236,  236,  236,
  236,  236,  236,  236,    0,    0,  236,  236,    0,    0,
  236,  236,  236,  236,  236,  236,  219,  219,  236,  236,
  236,    0,    0,    0,  236,   61,    0,    0,  236,  236,
  236,    0,    0,    0,  236,  236,  236,  236,  236,    0,
  236,    0,  236,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  236,    0,    0,    0,    0,  219,  219,
  219,    0,    0,    0,  236,  236,  236,  236,    0,  219,
    0,    0,  219,  219,  219,  219,  219,  219,  219,  219,
  219,    0,    0,  219,  219,    0,    0,  219,  219,  219,
  219,  219,  219,  220,  220,  219,  219,  219,    0,    0,
    0,  219,   61,    0,    0,  219,  219,  219,    0,    0,
    0,  219,  219,  219,  219,  219,    0,  219,    0,  219,
    0,    0,  407,    0,    0,    0,    0,    0,    0,    0,
  219,    0,    0,    0,    0,  220,  220,  220,    0,    0,
  407,  219,  219,  219,  219,    0,  220,    0,    0,  220,
  220,  220,  220,  220,  220,  220,  220,  220,    0,    0,
  220,  220,    0,    0,  220,  220,  220,  220,  220,  220,
    0,    0,  220,  220,  220,    0,    0,    0,  220,   61,
  246,  246,  220,  220,  220,    0,    0,    0,  220,  220,
  220,  220,  220,    0,  220,    0,  220,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  220,  374,  375,
  376,  377,  378,  379,  380,  381,  382,  383,  220,  220,
  220,  220,  246,  246,  246,  407,    0,  407,    0,    0,
  407,    0,    0,  246,    0,    0,  246,  246,  246,  246,
  246,  246,  246,  246,  246,    0,    0,  246,  246,    0,
    0,  246,  246,  246,  246,  246,  246,  240,  240,  246,
  246,  246,    0,    0,    0,  246,    0,    0,    0,  246,
  246,  246,    0,    0,    0,  246,  246,  246,  246,  246,
    0,  246,    0,  246,    0,    0,    0,    0,  407,    0,
    0,    0,  407,  407,  246,    0,    0,    0,    0,  240,
  240,  240,    0,    0,    0,  246,  246,  246,  246,    0,
  240,    0,    0,  240,  240,  240,  240,  240,  240,  240,
  240,  240,    0,    0,  240,  240,    0,    0,  240,  240,
  240,  240,  240,  240,  233,  233,  240,  240,  240,   64,
    0,    0,  240,    0,    0,    0,  240,  240,  240,    0,
    0,    0,  240,  240,  240,  240,  240,  151,  240,    0,
  240,   55,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  240,    0,    0,    0,    0,  233,  233,  233,  151,
    0,    0,  240,  240,  240,  240,    0,  233,    0,   17,
  233,  233,  233,  233,  233,  233,  233,  233,  233,    0,
    0,  233,  233,    0,    0,  233,  233,  233,  233,  233,
  233,  208,  208,  233,  233,  233,    0,    0,    0,  233,
  150,    0,    0,  233,  233,  233,    0,    0,    0,  233,
  233,  233,  233,  233,  151,  233,    0,  233,    0,    0,
    0,    0,  150,    0,    0,    0,    0,    0,  233,    0,
    0,    0,    0,  208,  208,  208,    0,    0,    0,  233,
  233,  233,  233,    0,  208,    0,    0,  208,  208,  208,
  208,  208,  208,  208,  208,  208,    0,    0,  208,  208,
  626,    0,  208,  208,  208,  208,  208,  208,    0,    0,
  208,  208,  208,    0,    0,   55,  208,  150,    0,  151,
  208,  208,  208,    0,    0,    0,  208,  208,  208,  208,
  208,   55,  208,    0,  208,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  208,    0,    0,    1,    2,
  406,    0,    3,    4,    0,    5,  208,  208,  208,  208,
   55,    0,    0,   55,   55,   55,   55,   55,   55,   55,
    6,    7,    0,    0,  142,  143,    0,  144,  145,  146,
    0,  147,  150,    0,    0,    0,    0,    8,  164,  165,
    0,    0,    0,    0,   64,  148,  142,  143,    0,  144,
  145,  146,    0,  147,    0,  149,    0,    0,    0,    0,
  164,  165,  151,    0,    0,    0,    0,  148,  178,  179,
  180,  181,    0,    0,    0,    0,    0,  149,  420,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  178,  179,  180,  181,    0,    0,    0,    0,    0,    0,
    0,  142,  143,    0,  144,  145,  146,    0,  147,    0,
    0,    0,    0,    0,    0,  164,  165,    0,    0,    0,
    0,    0,  148,    0,  166,  150,   17,   17,    0,  130,
   17,   17,  149,   17,    0,    0,    0,    0,    0,  167,
    0,    0,    0,    0,    0,  131,  166,  268,   17,   17,
    0,    0,    0,    0,    0,  474,    0,    0,    0,    0,
    0,  167,    0,    0,    0,   17,  142,  143,    0,  144,
  145,  146,    0,  147,  132,    0,    0,  133,  134,  135,
  136,  137,  138,  139,  228,    0,    0,  148,    0,  269,
  270,  271,    0,    0,    0,    0,    0,  149,    0,    0,
  272,  166,    0,  273,  274,  275,  276,  277,  278,  279,
  280,  281,    0,    0,  282,  283,  167,    0,  284,  285,
  286,  287,  288,  289,    0,  268,  290,  291,  292,    0,
    0,    0,  293,  476,    0,    0,  294,  295,  296,    0,
    0,    0,  297,  298,  299,  300,  301,    0,  302,    0,
  303,    0,    0,    0,    0,    0,    0,    0,    0,  142,
  143,  304,  144,  145,  146,    0,  147,  269,  270,  271,
    0,    0,  305,  306,  307,  308,    0,    0,  272,    0,
  148,  273,  274,  275,  276,  277,  278,  279,  280,  281,
  149,    0,  282,  283,    0,    0,  284,  285,  286,  287,
  288,  289,  268,    0,  290,  291,  292,    0,    0,    0,
  293,  478,    0,    0,  294,  295,  296,    0,    0,    0,
  297,  298,  299,  300,  301,    0,  302,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  304,
    0,    0,    0,    0,  269,  270,  271,    0,    0,    0,
  305,  306,  307,  308,    0,  272,    0,    0,  273,  274,
  275,  276,  277,  278,  279,  280,  281,    0,    0,  282,
  283,    0,    0,  284,  285,  286,  287,  288,  289,    0,
  268,  290,  291,  292,    0,    0,    0,  293,  530,    0,
    0,  294,  295,  296,    0,    0,    0,  297,  298,  299,
  300,  301,    0,  302,    0,  303,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  304,    0,    0,    0,
    0,    0,  269,  270,  271,    0,    0,  305,  306,  307,
  308,    0,    0,  272,    0,    0,  273,  274,  275,  276,
  277,  278,  279,  280,  281,    0,    0,  282,  283,    0,
  151,  284,  285,  286,  287,  288,  289,    0,  268,  290,
  291,  292,    0,    0,    0,  293,  534,    0,    0,  294,
  295,  296,    0,    0,    0,  297,  298,  299,  300,  301,
    0,  302,    0,  303,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  304,    0,    0,    0,    0,    0,
  269,  270,  271,    0,    0,  305,  306,  307,  308,    0,
    0,  272,    0,  150,  273,  274,  275,  276,  277,  278,
  279,  280,  281,    0,    0,  282,  283,    0,    0,  284,
  285,  286,  287,  288,  289,  268,    0,  290,  291,  292,
    0,    0,    0,  293,  535,    0,    0,  294,  295,  296,
    0,    0,    0,  297,  298,  299,  300,  301,    0,  302,
    0,  303,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  304,    0,    0,    0,    0,  269,  270,  271,
    0,    0,    0,  305,  306,  307,  308,    0,  272,    0,
    0,  273,  274,  275,  276,  277,  278,  279,  280,  281,
    0,    0,  282,  283,    0,    0,  284,  285,  286,  287,
  288,  289,    0,  268,  290,  291,  292,    0,    0,    0,
  293,  167,    0,    0,  294,  295,  296,    0,    0,    0,
  297,  298,  299,  300,  301,    0,  302,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,  142,  143,  304,
  144,  145,  146,    0,  147,  269,  270,  271,    0,    0,
  305,  306,  307,  308,    0,  228,  272,    0,  148,  273,
  274,  275,  276,  277,  278,  279,  280,  281,  149,    0,
  282,  283,    0,    0,  284,  285,  286,  287,  288,  289,
    0,  268,  290,  291,  292,    0,    0,    0,  293,  165,
    0,    0,  294,  295,  296,    0,    0,    0,  297,  298,
  299,  300,  301,    0,  302,    0,  303,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  304,    0,    0,
    0,    0,    0,  269,  270,  271,    0,    0,  305,  306,
  307,  308,    0,    0,  272,    0,    0,  273,  274,  275,
  276,  277,  278,  279,  280,  281,    0,    0,  282,  283,
    0,    0,  284,  285,  286,  287,  288,  289,  167,    0,
  290,  291,  292,    0,    0,    0,  293,  168,    0,    0,
  294,  295,  296,    0,    0,    0,  297,  298,  299,  300,
  301,    0,  302,    0,  303,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,    0,    0,    0,    0,
  167,  167,  167,    0,    0,    0,  305,  306,  307,  308,
    0,  167,    0,    0,  167,  167,  167,  167,  167,  167,
  167,  167,  167,    0,    0,  167,  167,    0,    0,  167,
  167,  167,  167,  167,  167,    0,  165,  167,  167,  167,
    0,    0,    0,  167,  166,    0,    0,  167,  167,  167,
    0,    0,    0,  167,  167,  167,  167,  167,    0,  167,
    0,  167,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  167,    0,    0,    0,    0,    0,  165,  165,
  165,    0,    0,  167,  167,  167,  167,    0,   35,  165,
    0,    0,  165,  165,  165,  165,  165,  165,  165,  165,
  165,    0,    0,  165,  165,    0,    0,  165,  165,  165,
  165,  165,  165,    0,  168,  165,  165,  165,    0,   36,
   64,  165,    0,    0,    0,  165,  165,  165,    0,    0,
    0,  165,  165,  165,  165,  165,    0,  165,  151,  165,
    0,    0,    0,    0,    0,    0,    0,  122,    0,    0,
  165,   34,    0,    0,    0,    0,  168,  168,  168,    0,
    0,  165,  165,  165,  165,    0,   35,  168,    0,    0,
  168,  168,  168,  168,  168,  168,  168,  168,  168,    0,
    0,  168,  168,    0,    0,  168,  168,  168,  168,  168,
  168,  166,    0,  168,  168,  168,    0,   36,    0,  168,
    0,  150,    0,  168,  168,  168,   35,    0,    0,  168,
  168,  168,  168,  168,    0,  168,    0,  168,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  168,   34,
    0,    0,    0,  166,  166,  166,    0,   36,  151,  168,
  168,  168,  168,    0,  166,    0,    0,  166,  166,  166,
  166,  166,  166,  166,  166,  166,    0,    0,  166,  166,
    0,    0,  166,  166,  166,  166,  166,  166,   35,   34,
  166,  166,  166,    0,    0,    0,  166,    0,    0,    0,
  166,  166,  166,    0,    0,    0,  166,  166,  166,  166,
  166,   21,  166,    0,  166,    0,   35,    0,    0,   36,
   22,  150,    0,    0,    0,  166,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,  166,  166,  166,  166,
    0,   35,    0,    0,    0,    0,    0,   36,    0,    0,
    0,   34,    0,    0,    0,  142,  143,    0,  144,  145,
  146,    0,  147,    0,    0,    0,    0,    0,   35,    0,
  618,    0,   36,    0,  334,  335,  148,    0,    0,   34,
    0,    0,    0,    0,    0,    0,  149,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,   36,
    0,    0,   35,  120,   34,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,    0,    0,
  121,    0,    0,    0,    0,    0,    0,    0,    0,   21,
   35,   34,    0,   36,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,  142,  143,  201,  144,  145,
  146,   36,  147,    0,    0,   34,   65,  110,   35,    0,
    0,  202,    0,  203,    0,    0,  148,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  149,    0,    0,    0,
    0,   21,    0,   34,   35,    0,    0,    0,    0,   36,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,   21,
   35,   31,    0,    0,    0,   36,   32,   33,   22,    0,
    0,   34,    0,  120,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,   21,   35,    0,    0,    0,    0,
  121,   36,    0,   22,    0,    0,    0,   34,    0,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
    0,   21,   35,    0,    0,   91,   36,    0,  610,    0,
   22,    0,    0,   34,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,   35,
    0,   41,    0,   36,    0,  107,  108,    0,   34,    0,
    0,    0,    0,    0,   22,  109,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   36,    0,    0,  107,  108,   34,    0,    0,    0,    0,
    0,    0,   22,  109,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,   34,    0,    0,    0,    0,    0,    0,    0,
    0,  107,  209,    0,    0,    0,    0,    0,    0,    0,
   22,  210,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,  385,
  386,  572,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,  599,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   21,    0,
    0,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,   21,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,  107,    0,  232,  232,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,  387,  388,  389,
  390,    0,    0,    0,    0,    0,  391,  392,  393,  394,
  395,  396,  397,  398,  399,  400,  232,  232,  232,    0,
    0,    0,    0,    0,    0,    0,    0,  232,    0,    0,
  232,  232,  232,  232,  232,  232,  232,  232,  232,    0,
    0,  232,  232,    0,    0,  232,  232,  232,  232,  232,
  232,  222,  222,  232,  232,  232,    0,    0,    0,  232,
    0,    0,    0,  232,  232,  232,    0,    0,    0,  232,
  232,  232,  232,  232,    0,  232,    0,  232,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  232,    0,
    0,    0,    0,  222,  222,  222,    0,    0,    0,  232,
  232,  232,  232,    0,  222,    0,    0,  222,  222,  222,
  222,  222,  222,  222,  222,  222,    0,    0,  222,  222,
    0,    0,  222,  222,  222,  222,  222,  222,  209,  209,
  222,  222,  222,    0,    0,    0,  222,    0,    0,    0,
  222,  222,  222,    0,    0,    0,  222,  222,  222,  222,
  222,    0,  222,    0,  222,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  222,    0,    0,    0,    0,
  209,  209,  209,    0,    0,    0,  222,  222,  222,  222,
    0,  209,    0,    0,  209,  209,  209,  209,  209,  209,
  209,  209,  209,    0,    0,  209,  209,    0,    0,  209,
  209,  209,  209,  209,  209,  223,  223,  209,  209,  209,
    0,    0,    0,  209,    0,    0,    0,  209,  209,  209,
    0,    0,    0,  209,  209,  209,  209,  209,    0,  209,
    0,  209,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  209,    0,    0,    0,    0,  223,  223,  223,
    0,    0,    0,  209,  209,  209,  209,    0,  223,    0,
    0,  223,  223,  223,  223,  223,  223,  223,  223,  223,
    0,    0,  223,  223,    0,    0,  223,  223,  223,  223,
  223,  223,  210,  210,  223,  223,  223,    0,    0,    0,
  223,    0,    0,    0,  223,  223,  223,    0,    0,    0,
  223,  223,  223,  223,  223,    0,  223,    0,  223,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  223,
    0,    0,    0,    0,  210,  210,  210,    0,    0,    0,
  223,  223,  223,  223,    0,  210,    0,    0,  210,  210,
  210,  210,  210,  210,  210,  210,  210,    0,    0,  210,
  210,    0,    0,  210,  210,  210,  210,  210,  210,  212,
  212,  210,  210,  210,    0,    0,    0,  210,    0,    0,
    0,  210,  210,  210,    0,    0,    0,  210,  210,  210,
  210,  210,    0,  210,    0,  210,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  210,    0,    0,    0,
    0,  212,  212,  212,    0,    0,    0,  210,  210,  210,
  210,    0,  212,    0,    0,  212,  212,  212,  212,  212,
  212,  212,  212,  212,    0,    0,  212,  212,    0,    0,
  212,  212,  212,  212,  212,  212,  211,  211,  212,  212,
  212,    0,    0,    0,  212,    0,    0,    0,  212,  212,
  212,    0,    0,    0,  212,  212,  212,  212,  212,    0,
  212,    0,  212,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  212,    0,    0,    0,    0,  211,  211,
  211,    0,    0,    0,  212,  212,  212,  212,    0,  211,
    0,    0,  211,  211,  211,  211,  211,  211,  211,  211,
  211,    0,    0,  211,  211,    0,    0,  211,  211,  211,
  211,  211,  211,  268,    0,  211,  211,  211,    0,    0,
    0,  211,    0,    0,    0,  211,  211,  211,    0,    0,
    0,  211,  211,  211,  211,  211,    0,  211,    0,  211,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  211,    0,    0,    0,    0,  269,  270,  271,    0,    0,
    0,  211,  211,  211,  211,    0,  272,    0,    0,  273,
  274,  275,  276,  277,  278,  279,  280,  281,    0,    0,
  282,  283,    0,    0,  284,  285,  286,  287,  288,  289,
  171,    0,  290,  291,  292,    0,    0,    0,  293,    0,
    0,    0,  294,  295,  296,    0,    0,    0,  297,  298,
  299,  300,  301,    0,  302,    0,  303,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  304,    0,    0,
    0,    0,  171,  171,  171,    0,    0,    0,  305,  306,
  307,  308,    0,  171,    0,    0,  171,  171,  171,  171,
  171,  171,  171,  171,  171,    0,    0,  171,  171,    0,
    0,  171,  171,  171,  171,  171,  171,  172,    0,  171,
  171,  171,    0,    0,    0,  171,    0,    0,    0,  171,
  171,  171,    0,    0,    0,  171,  171,  171,  171,  171,
    0,  171,    0,  171,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  171,    0,    0,    0,    0,  172,
  172,  172,    0,    0,    0,  171,  171,  171,  171,    0,
  172,    0,    0,  172,  172,  172,  172,  172,  172,  172,
  172,  172,    0,    0,  172,  172,    0,    0,  172,  172,
  172,  172,  172,  172,  173,    0,  172,  172,  172,    0,
    0,    0,  172,    0,    0,    0,  172,  172,  172,    0,
    0,    0,  172,  172,  172,  172,  172,    0,  172,    0,
  172,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  172,    0,    0,    0,    0,  173,  173,  173,    0,
    0,    0,  172,  172,  172,  172,    0,  173,    0,    0,
  173,  173,  173,  173,  173,  173,  173,  173,  173,    0,
    0,  173,  173,    0,    0,  173,  173,  173,  173,  173,
  173,  174,    0,  173,  173,  173,    0,    0,    0,  173,
    0,    0,    0,  173,  173,  173,    0,    0,    0,  173,
  173,  173,  173,  173,    0,  173,    0,  173,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  173,    0,
    0,    0,    0,  174,  174,  174,    0,    0,    0,  173,
  173,  173,  173,    0,  174,    0,    0,  174,  174,  174,
  174,  174,  174,  174,  174,  174,    0,    0,  174,  174,
    0,    0,  174,  174,  174,  174,  174,  174,    0,    0,
  174,  174,  174,    0,    0,    0,  174,    0,    0,    0,
  174,  174,  174,    0,    0,    0,  174,  174,  174,  174,
  174,    0,  174,    0,  174,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  174,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  174,  174,  174,  174,
  272,    0,    0,  273,  274,  275,  276,  277,  278,  279,
  280,  281,    0,    0,  282,  283,    0,    0,  284,  285,
  286,  287,  288,  289,    0,    0,  290,  291,  292,    0,
    0,    0,  293,    0,    0,    0,  294,  295,  296,    0,
    0,    0,  297,  298,  299,  300,  301,    0,  302,    0,
  303,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  304,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  305,  306,  307,  308,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  182,  240,   98,    6,    7,   40,   40,   40,   40,
   40,   33,   40,  424,  123,   33,   44,  123,  151,  471,
   44,  125,   42,   60,   31,   44,  576,   34,   44,   31,
  123,   44,  123,   42,   41,  123,  123,  123,  123,   41,
   52,   53,  273,  566,   40,   42,   61,   42,   44,  305,
   44,   40,   41,   96,   91,   44,  606,  265,  153,   66,
  155,   68,  291,  292,   66,  176,  259,   62,   62,   76,
  593,  482,  115,   93,  305,  283,  305,  529,  175,   86,
   87,   88,  274,   90,  276,  128,  123,  184,   61,   96,
  282,  188,   99,  100,   91,  190,   41,  125,   61,   44,
  195,  125,  235,   83,   41,  216,  125,   44,  115,  125,
  122,  222,  125,   40,   41,  226,  279,   44,  337,  275,
  127,  128,  341,  279,  221,  344,  106,  224,  225,  125,
  173,   41,  351,  301,  302,  303,  304,   42,  249,   44,
  251,  252,   40,   61,  151,  240,  243,   61,   41,   41,
   60,   44,   44,  264,   41,  259,  257,   44,   41,  254,
   41,   44,   60,   44,  273,   61,  173,  273,  265,  580,
  150,  275,  184,  257,   41,   40,  188,   44,  185,   41,
  273,   42,   44,  163,  300,  273,  273,  273,  273,   41,
   41,   41,   44,   44,   44,   60,  305,   61,  609,   42,
  304,  259,  214,  307,  308,  309,  310,  311,  312,  313,
  269,  270,  305,  123,  305,  326,   41,   60,   41,   44,
   42,   44,   44,  291,  292,  123,  263,   40,  235,  297,
  298,  299,  244,  213,  241,  272,  259,  271,  271,  271,
  271,  271,   40,  280,  281,  282,  283,  284,  285,  286,
  287,  273,  112,  233,  276,  273,  263,  273,  123,   41,
  273,   41,   44,  266,   44,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  123,  123,    0,  290,  291,   41,  293,   41,   44,  269,
   44,  271,  301,  302,  303,  304,  295,  296,  305,  306,
  307,   42,  123,   44,  341,  307,   40,  287,  288,  289,
  266,   42,  292,   44,  294,  295,  296,  297,  298,  299,
  300,  301,  302,   42,  123,   44,   42,   40,   44,   40,
  337,  291,  292,  123,  341,   41,   42,  344,   77,   78,
   40,   40,  322,   58,  351,  352,   58,  257,  258,    0,
  260,  261,  262,   40,  264,   40,  363,   61,   44,  257,
  258,  273,  260,  261,  262,  275,  264,  359,  278,   40,
  305,  259,  358,  271,  272,   44,  358,  384,  288,  617,
  278,   40,    0,   40,  273,  257,  358,  257,  266,  366,
  288,  273,  257,  258,  401,  260,  261,  262,  405,  264,
  273,  257,  273,  405,  272,   61,  271,  272,  399,   44,
   44,   44,   44,  278,  257,  258,   44,  260,  261,  262,
  601,  264,  366,  288,  366,  366,   40,   41,  271,  272,
   44,   44,  366,  366,  366,  278,  366,  315,  366,  317,
  366,   44,   44,  450,  451,  288,  324,  454,  455,  456,
  457,  458,  459,  460,  461,  462,  265,   44,   44,  357,
   44,   44,   44,   44,   44,  472,  446,  447,  448,   44,
  472,  331,   44,  453,  372,   44,  336,   44,  125,  339,
  340,   44,  342,  343,   44,  345,  346,  347,  348,  349,
  350,  216,  357,  353,  354,  355,  356,  222,   44,  358,
   44,   44,   40,   40,   44,  600,  603,  372,   44,   44,
  621,  125,   91,   44,  357,   44,   44,  628,   44,  257,
  527,   44,  617,   44,  249,   44,  623,   44,   44,  372,
  537,  511,  257,   44,   91,  413,   40,   44,  358,  264,
  418,  419,  358,  403,   44,   44,  526,   40,   44,  267,
  268,   93,  257,  271,  272,  257,  274,   44,   40,    0,
   40,   10,   54,   18,  105,  572,  426,  106,  158,  576,
  430,  289,  290,  433,  140,  582,  127,  405,  558,  559,
  440,  441,  451,  573,  185,  311,  579,  312,  306,  314,
   40,  311,  328,  604,  254,  582,  525,  604,   -1,  606,
   -1,   -1,   -1,  463,  464,  465,   -1,   -1,   -1,   -1,
   -1,   -1,  259,   -1,   -1,   -1,  267,  268,  598,   -1,
  271,  272,   -1,  274,  484,   -1,  486,  487,  275,  489,
  490,   -1,  492,  493,  494,  495,  496,  497,  289,  290,
  500,  501,  502,  503,   -1,   -1,   -1,   60,  508,  267,
  268,   -1,   -1,  271,  272,  306,  274,  304,  272,  273,
  307,  308,  309,  310,  311,  312,  313,   40,   -1,   -1,
   -1,  289,  290,   -1,   -1,   -1,   -1,   -1,   91,  539,
   -1,   -1,  542,  408,   -1,  545,   -1,   -1,  306,   -1,
   -1,   -1,  552,  553,   -1,   -1,  421,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,   -1,   -1,  567,  568,  569,
  123,  325,   -1,  573,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  272,  273,  351,  352,  353,
   -1,  601,   -1,  357,   40,   -1,   -1,  361,  362,  363,
   -1,   -1,  366,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,   -1,   -1,  351,  352,  353,   -1,   -1,   -1,
  357,   40,  272,  273,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  397,  398,  399,  400,  314,  315,  316,  280,  281,  282,
  283,  284,  285,  286,  287,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
  273,  351,  352,  353,   -1,   -1,   -1,  357,   40,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,  359,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  272,  273,  351,  352,
  353,   -1,   -1,   -1,  357,   40,   -1,   -1,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,
  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  272,  273,  351,  352,  353,   -1,   -1,
   -1,  357,   40,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,  375,
   -1,   -1,  309,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,
  327,  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
   -1,   -1,  351,  352,  353,   -1,   -1,   -1,  357,   40,
  272,  273,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,  376,  377,
  378,  379,  380,  381,  382,  383,  384,  385,  397,  398,
  399,  400,  314,  315,  316,  412,   -1,  414,   -1,   -1,
  417,   -1,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,  273,  351,
  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,  475,   -1,
   -1,   -1,  479,  480,  386,   -1,   -1,   -1,   -1,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  272,  273,  351,  352,  353,   42,
   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   60,  373,   -1,
  375,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,   60,
   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,    0,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,  273,  351,  352,  353,   -1,   -1,   -1,  357,
  123,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   60,  373,   -1,  375,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   41,   -1,  343,  344,  345,  346,  347,  348,   -1,   -1,
  351,  352,  353,   -1,   -1,  259,  357,  123,   -1,   60,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,  275,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,  267,  268,
  125,   -1,  271,  272,   -1,  274,  397,  398,  399,  400,
  304,   -1,   -1,  307,  308,  309,  310,  311,  312,  313,
  289,  290,   -1,   -1,  257,  258,   -1,  260,  261,  262,
   -1,  264,  123,   -1,   -1,   -1,   -1,  306,  271,  272,
   -1,   -1,   -1,   -1,   42,  278,  257,  258,   -1,  260,
  261,  262,   -1,  264,   -1,  288,   -1,   -1,   -1,   -1,
  271,  272,   60,   -1,   -1,   -1,   -1,  278,  301,  302,
  303,  304,   -1,   -1,   -1,   -1,   -1,  288,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  301,  302,  303,  304,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  257,  258,   -1,  260,  261,  262,   -1,  264,   -1,
   -1,   -1,   -1,   -1,   -1,  271,  272,   -1,   -1,   -1,
   -1,   -1,  278,   -1,  357,  123,  267,  268,   -1,  259,
  271,  272,  288,  274,   -1,   -1,   -1,   -1,   -1,  372,
   -1,   -1,   -1,   -1,   -1,  275,  357,  272,  289,  290,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,  372,   -1,   -1,   -1,  306,  257,  258,   -1,  260,
  261,  262,   -1,  264,  304,   -1,   -1,  307,  308,  309,
  310,  311,  312,  313,  275,   -1,   -1,  278,   -1,  314,
  315,  316,   -1,   -1,   -1,   -1,   -1,  288,   -1,   -1,
  325,  357,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,  372,   -1,  343,  344,
  345,  346,  347,  348,   -1,  272,  351,  352,  353,   -1,
   -1,   -1,  357,  125,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  386,  260,  261,  262,   -1,  264,  314,  315,  316,
   -1,   -1,  397,  398,  399,  400,   -1,   -1,  325,   -1,
  278,  328,  329,  330,  331,  332,  333,  334,  335,  336,
  288,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  272,   -1,  351,  352,  353,   -1,   -1,   -1,
  357,  125,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,
  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,   -1,
  272,  351,  352,  353,   -1,   -1,   -1,  357,  125,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,   -1,  314,  315,  316,   -1,   -1,  397,  398,  399,
  400,   -1,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   60,  343,  344,  345,  346,  347,  348,   -1,  272,  351,
  352,  353,   -1,   -1,   -1,  357,  125,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,  397,  398,  399,  400,   -1,
   -1,  325,   -1,  123,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  272,   -1,  351,  352,  353,
   -1,   -1,   -1,  357,  125,   -1,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,   -1,  272,  351,  352,  353,   -1,   -1,   -1,
  357,  125,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  386,
  260,  261,  262,   -1,  264,  314,  315,  316,   -1,   -1,
  397,  398,  399,  400,   -1,  275,  325,   -1,  278,  328,
  329,  330,  331,  332,  333,  334,  335,  336,  288,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
   -1,  272,  351,  352,  353,   -1,   -1,   -1,  357,  125,
   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,  397,  398,
  399,  400,   -1,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  272,   -1,
  351,  352,  353,   -1,   -1,   -1,  357,  125,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,
   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,   -1,  272,  351,  352,  353,
   -1,   -1,   -1,  357,  125,   -1,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,   -1,  314,  315,
  316,   -1,   -1,  397,  398,  399,  400,   -1,   60,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,   -1,  272,  351,  352,  353,   -1,   91,
   42,  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   60,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,   -1,
  386,  123,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,
   -1,  397,  398,  399,  400,   -1,   60,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,   -1,  351,  352,  353,   -1,   91,   -1,  357,
   -1,  123,   -1,  361,  362,  363,   60,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,  123,
   -1,   -1,   -1,  314,  315,  316,   -1,   91,   60,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,   60,  123,
  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,  263,  373,   -1,  375,   -1,   60,   -1,   -1,   91,
  272,  123,   -1,   -1,   -1,  386,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,  397,  398,  399,  400,
   -1,   60,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,  123,   -1,   -1,   -1,  257,  258,   -1,  260,  261,
  262,   -1,  264,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  272,   -1,   91,   -1,  326,  327,  278,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  288,   -1,   -1,  263,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,   91,
   -1,   -1,   60,  277,  123,   -1,  280,  281,  282,  283,
  284,  285,  286,  287,   -1,   -1,   -1,   -1,   -1,   -1,
  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  263,
   60,  123,   -1,   91,   -1,   -1,   -1,   -1,  272,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,  282,  283,
  284,  285,  286,  287,   -1,  257,  258,  259,  260,  261,
  262,   91,  264,   -1,   -1,  123,  300,  125,   60,   -1,
   -1,  273,   -1,  275,   -1,   -1,  278,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  288,   -1,   -1,   -1,
   -1,  263,   -1,  123,   60,   -1,   -1,   -1,   -1,   91,
  272,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,   -1,   -1,   -1,  263,
   60,  293,   -1,   -1,   -1,   91,  298,  299,  272,   -1,
   -1,  123,   -1,  277,   -1,   -1,  280,  281,  282,  283,
  284,  285,  286,  287,  263,   60,   -1,   -1,   -1,   -1,
  294,   91,   -1,  272,   -1,   -1,   -1,  123,   -1,   -1,
   -1,  280,  281,  282,  283,  284,  285,  286,  287,   -1,
   -1,  263,   60,   -1,   -1,  294,   91,   -1,   93,   -1,
  272,   -1,   -1,  123,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,   -1,   -1,   -1,   60,
   -1,  293,   -1,   91,   -1,  263,  264,   -1,  123,   -1,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,   -1,  280,  281,  282,  283,  284,  285,  286,  287,
   91,   -1,   -1,  263,  264,  123,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,
  280,  281,  282,  283,  284,  285,  286,  287,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  263,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,   -1,  263,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  272,   -1,   -1,  260,
  261,  277,   -1,   -1,  280,  281,  282,  283,  284,  285,
  286,  287,   -1,  263,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,
  280,  281,  282,  283,  284,  285,  286,  287,  263,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  280,  281,  282,  283,  284,
  285,  286,  287,   -1,   -1,  263,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  272,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  280,  281,  282,  283,  284,  285,  286,  287,
   -1,   -1,  263,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  272,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  280,
  281,  282,  283,  284,  285,  286,  287,  378,  379,  380,
  381,   -1,   -1,   -1,   -1,   -1,  387,  388,  389,  390,
  391,  392,  393,  394,  395,  396,  314,  315,  316,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,  273,  351,  352,  353,   -1,   -1,   -1,  357,
   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  272,  273,
  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,
   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  272,  273,  351,  352,  353,
   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  272,  273,  351,  352,  353,   -1,   -1,   -1,
  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,
  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
  273,  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  272,  273,  351,  352,
  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,
  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  272,   -1,  351,  352,  353,   -1,   -1,
   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,
   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  272,   -1,  351,  352,  353,   -1,   -1,   -1,  357,   -1,
   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,
   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,
  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,   -1,  351,
  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  272,   -1,  351,  352,  353,   -1,
   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,
   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,   -1,  351,  352,  353,   -1,   -1,   -1,  357,
   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,   -1,   -1,
  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  397,  398,  399,  400,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,   -1,   -1,  351,  352,  353,   -1,
   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  397,  398,  399,  400,
  };

#line 924 "Repil/IR/IR.jay"

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
  public const int META_SYMBOL_DEF = 274;
  public const int SYMBOL = 275;
  public const int DISTINCT = 276;
  public const int METADATA = 277;
  public const int CONSTANT_BYTES = 278;
  public const int TYPE = 279;
  public const int HALF = 280;
  public const int FLOAT = 281;
  public const int DOUBLE = 282;
  public const int I1 = 283;
  public const int I8 = 284;
  public const int I16 = 285;
  public const int I32 = 286;
  public const int I64 = 287;
  public const int ZEROINITIALIZER = 288;
  public const int DEFINE = 289;
  public const int DECLARE = 290;
  public const int UNNAMED_ADDR = 291;
  public const int LOCAL_UNNAMED_ADDR = 292;
  public const int NOALIAS = 293;
  public const int ELLIPSIS = 294;
  public const int GLOBAL = 295;
  public const int CONSTANT = 296;
  public const int PRIVATE = 297;
  public const int INTERNAL = 298;
  public const int EXTERNAL = 299;
  public const int FASTCC = 300;
  public const int NONNULL = 301;
  public const int NOCAPTURE = 302;
  public const int WRITEONLY = 303;
  public const int READONLY = 304;
  public const int ATTRIBUTE_GROUP_REF = 305;
  public const int ATTRIBUTES = 306;
  public const int NORECURSE = 307;
  public const int NOUNWIND = 308;
  public const int READNONE = 309;
  public const int SPECULATABLE = 310;
  public const int SSP = 311;
  public const int UWTABLE = 312;
  public const int ARGMEMONLY = 313;
  public const int RET = 314;
  public const int BR = 315;
  public const int SWITCH = 316;
  public const int INDIRECTBR = 317;
  public const int INVOKE = 318;
  public const int RESUME = 319;
  public const int CATCHSWITCH = 320;
  public const int CATCHRET = 321;
  public const int CLEANUPRET = 322;
  public const int UNREACHABLE = 323;
  public const int FNEG = 324;
  public const int ADD = 325;
  public const int NUW = 326;
  public const int NSW = 327;
  public const int FADD = 328;
  public const int SUB = 329;
  public const int FSUB = 330;
  public const int MUL = 331;
  public const int FMUL = 332;
  public const int UDIV = 333;
  public const int SDIV = 334;
  public const int FDIV = 335;
  public const int UREM = 336;
  public const int SREM = 337;
  public const int FREM = 338;
  public const int SHL = 339;
  public const int LSHR = 340;
  public const int EXACT = 341;
  public const int ASHR = 342;
  public const int AND = 343;
  public const int OR = 344;
  public const int XOR = 345;
  public const int EXTRACTELEMENT = 346;
  public const int INSERTELEMENT = 347;
  public const int SHUFFLEVECTOR = 348;
  public const int EXTRACTVALUE = 349;
  public const int INSERTVALUE = 350;
  public const int ALLOCA = 351;
  public const int LOAD = 352;
  public const int STORE = 353;
  public const int FENCE = 354;
  public const int CMPXCHG = 355;
  public const int ATOMICRMW = 356;
  public const int GETELEMENTPTR = 357;
  public const int ALIGN = 358;
  public const int INBOUNDS = 359;
  public const int INRANGE = 360;
  public const int TRUNC = 361;
  public const int ZEXT = 362;
  public const int SEXT = 363;
  public const int FPTRUNC = 364;
  public const int FPEXT = 365;
  public const int TO = 366;
  public const int FPTOUI = 367;
  public const int FPTOSI = 368;
  public const int UITOFP = 369;
  public const int SITOFP = 370;
  public const int PTRTOINT = 371;
  public const int INTTOPTR = 372;
  public const int BITCAST = 373;
  public const int ADDRSPACECAST = 374;
  public const int ICMP = 375;
  public const int EQ = 376;
  public const int NE = 377;
  public const int UGT = 378;
  public const int UGE = 379;
  public const int ULT = 380;
  public const int ULE = 381;
  public const int SGT = 382;
  public const int SGE = 383;
  public const int SLT = 384;
  public const int SLE = 385;
  public const int FCMP = 386;
  public const int OEQ = 387;
  public const int OGT = 388;
  public const int OGE = 389;
  public const int OLT = 390;
  public const int OLE = 391;
  public const int ONE = 392;
  public const int ORD = 393;
  public const int UEQ = 394;
  public const int UNE = 395;
  public const int UNO = 396;
  public const int PHI = 397;
  public const int SELECT = 398;
  public const int CALL = 399;
  public const int TAIL = 400;
  public const int VA_ARG = 401;
  public const int LANDINGPAD = 402;
  public const int CATCHPAD = 403;
  public const int CLEANUPPAD = 404;
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
