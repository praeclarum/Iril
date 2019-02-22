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
//t    "module_part : LOCAL_SYMBOL '=' TYPE OPAQUE",
//t    "module_part : function_definition",
//t    "module_part : function_declaration",
//t    "module_part : global_variable",
//t    "module_part : ATTRIBUTES ATTRIBUTE_GROUP_REF '=' '{' attributes '}'",
//t    "module_part : META_SYMBOL_DEF '=' '!' '{' '}'",
//t    "module_part : META_SYMBOL_DEF '=' '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL_DEF '=' META_SYMBOL '(' metadata_args ')'",
//t    "module_part : META_SYMBOL_DEF '=' DISTINCT '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL_DEF '=' DISTINCT META_SYMBOL '(' metadata_args ')'",
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value ',' ALIGN INTEGER metadata_kvs",
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
//t    "literal_structure : '{' '}'",
//t    "literal_structure : '{' type_list '}'",
//t    "literal_structure : '<' '{' type_list '}' '>'",
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
//t    "type : return_type '(' ')'",
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
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
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
//t    "value : BITCAST '(' typed_value TO type ')'",
//t    "value : '<' typed_values '>'",
//t    "value : '[' typed_values ']'",
//t    "value : '{' typed_values '}'",
//t    "pointer_value : value",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : HEX_INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : UNDEF",
//t    "constant : ZEROINITIALIZER",
//t    "constant : CONSTANT_BYTES",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_value : VOID",
//t    "typed_pointer_value : type pointer_value",
//t    "typed_values : typed_value",
//t    "typed_values : typed_values ',' typed_value",
//t    "typed_constant : type constant",
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
//t    "instruction : CALL calling_convention return_type function_pointer '(' function_args ')'",
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
    "INTEGER","HEX_INTEGER","FLOAT_LITERAL","STRING","TRUE","FALSE",
    "UNDEF","VOID","NULL","LABEL","X","SOURCE_FILENAME","TARGET",
    "DATALAYOUT","TRIPLE","GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL",
    "META_SYMBOL_DEF","SYMBOL","DISTINCT","METADATA","CONSTANT_BYTES",
    "TYPE","HALF","FLOAT","DOUBLE","I1","I8","I16","I32","I64",
    "ZEROINITIALIZER","OPAQUE","DEFINE","DECLARE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS","GLOBAL","CONSTANT",
    "PRIVATE","INTERNAL","EXTERNAL","FASTCC","NONNULL","NOCAPTURE",
    "WRITEONLY","READONLY","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE",
    "NOUNWIND","READNONE","SPECULATABLE","SSP","UWTABLE","ARGMEMONLY",
    "RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME","CATCHSWITCH",
    "CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD","NUW","NSW","FADD",
    "SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV","UREM","SREM","FREM",
    "SHL","LSHR","EXACT","ASHR","AND","OR","XOR","EXTRACTELEMENT",
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
#line 75 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = OpaqueStructureType.Opaque;
    }
  break;
case 9:
  case_9();
  break;
case 10:
  case_10();
  break;
case 11:
  case_11();
  break;
case 13:
#line 95 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 99 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 108 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 128 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 132 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 136 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 23:
#line 140 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 24:
#line 144 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 148 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 26:
#line 149 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 27:
#line 153 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 28:
#line 154 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 29:
#line 158 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 30:
  case_30();
  break;
case 31:
  case_31();
  break;
case 32:
#line 175 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 33:
#line 176 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 34:
#line 177 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 178 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 179 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 183 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 38:
#line 187 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 39:
#line 194 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 40:
#line 198 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 205 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 42:
#line 209 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 43:
#line 213 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 44:
#line 217 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 45:
#line 221 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 63:
#line 254 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 64:
#line 258 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 65:
#line 262 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 66:
#line 269 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 273 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 69:
#line 278 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 71:
#line 283 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 72:
#line 284 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 73:
#line 285 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 74:
#line 286 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 75:
#line 287 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 76:
#line 288 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 77:
#line 289 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 78:
#line 290 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 79:
#line 294 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 80:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 81:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 82:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 83:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 84:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 85:
#line 321 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 89:
#line 340 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 90:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 91:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 93:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 360 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 95:
#line 364 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 96:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 372 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 379 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 99:
#line 383 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 100:
#line 387 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 101:
#line 394 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 398 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 103:
#line 405 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 409 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 413 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 106:
#line 417 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 108:
#line 425 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 429 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 110:
#line 430 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 111:
#line 431 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 112:
#line 432 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 118:
#line 450 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 119:
#line 451 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 120:
#line 452 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 121:
#line 453 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 122:
#line 454 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 123:
#line 455 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 124:
#line 456 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 125:
#line 457 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 126:
#line 458 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 127:
#line 459 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 128:
#line 463 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 129:
#line 464 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 130:
#line 465 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 131:
#line 466 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 132:
#line 467 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 133:
#line 468 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 134:
#line 469 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 135:
#line 470 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 136:
#line 471 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 137:
#line 472 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 138:
#line 473 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 139:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 140:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 141:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 142:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 143:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 145:
#line 483 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 146:
#line 484 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 147:
#line 488 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 148:
#line 492 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 149:
#line 496 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 150:
#line 500 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 504 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 508 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 516 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 155:
#line 517 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 156:
#line 518 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 157:
#line 519 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 158:
#line 520 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 159:
#line 521 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 160:
#line 522 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 161:
#line 523 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 162:
#line 524 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 163:
#line 531 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 538 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 542 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 166:
#line 549 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 556 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 578 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 582 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 589 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 593 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 600 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 604 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 608 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 612 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 179:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 630 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 634 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 183:
#line 638 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 642 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 186:
#line 653 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 657 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 664 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 189:
#line 668 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 190:
#line 672 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 191:
#line 676 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 192:
#line 680 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 193:
#line 684 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 194:
#line 688 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 199:
#line 705 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 709 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 715 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 202:
#line 722 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 726 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 754 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 758 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 762 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 766 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 214:
#line 773 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 785 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 789 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 793 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 220:
#line 797 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 221:
#line 801 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 222:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 223:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 224:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 225:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 829 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 857 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 861 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 865 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 238:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 239:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 240:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 885 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 889 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 893 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 905 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 909 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 913 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 253:
#line 929 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 933 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 937 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 941 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 945 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 949 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 953 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 957 "Repil/IR/IR.jay"
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
void case_9()
#line 77 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 82 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 87 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 101 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 110 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_30()
#line 163 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_31()
#line 168 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,   10,   10,   16,   16,   15,    9,
    9,   17,   17,   17,   17,   17,   17,   17,   13,   13,
    8,    8,    8,    8,    8,   20,   20,   20,    7,    7,
   22,   22,   22,   22,   22,   22,   22,   22,   22,   22,
   22,   22,    3,    3,    3,   23,   23,   24,   24,   11,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   25,   25,   26,   26,    4,    4,
    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,
   27,   27,   31,   31,   31,   31,   32,   32,   33,   33,
   33,   33,   14,   14,   28,   28,   34,   35,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   36,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   39,   18,   18,   18,   18,   18,   18,   18,
   18,   18,   40,   21,   21,   41,   38,   38,   42,   43,
   37,   37,   29,   29,   44,   44,   44,   44,   45,   45,
   47,   47,   47,   47,   49,   50,   50,   51,   51,   51,
   51,   51,   51,   51,   19,   19,   52,   52,   53,   53,
   54,   55,   55,   56,   57,   57,   58,   58,   30,   46,
   46,   46,   46,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   48,   48,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
   11,    9,   11,   10,    1,    1,    1,    1,    1,    1,
    3,    3,    3,    3,    3,    3,    6,    5,    2,    3,
    1,    2,    3,    3,    3,    1,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    1,    1,    3,    1,    1,
    1,    4,    2,    3,    5,    1,    3,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    4,
    2,    1,    5,    5,    1,    3,    1,    1,   11,   11,
   11,   10,   12,   12,   13,   13,   14,    7,    8,    8,
    1,    3,    1,    2,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    6,    9,    6,    3,
    3,    3,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,    2,    1,    2,    1,    3,    2,    1,
    1,    3,    1,    2,    2,    3,    1,    2,    1,    2,
    1,    2,    3,    4,    1,    1,    3,    2,    3,    3,
    3,    2,    4,    5,    1,    3,    1,    1,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    1,    2,
    7,    2,    7,    5,    6,    5,    5,    4,    6,    7,
    7,    8,    7,    8,    4,    5,    6,    5,    5,    4,
    4,    5,    6,    7,    6,    6,    7,    5,    6,    5,
    5,    6,    3,    4,    5,    7,    4,    5,    6,    6,
    4,    7,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   69,   82,   71,   72,   73,   74,   75,   76,   77,   78,
    0,   28,   27,    0,    0,    0,   70,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  113,  114,   25,
   26,   29,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   63,    0,    0,    0,    0,    0,    0,   81,  209,
    0,    0,    0,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    8,    0,    7,    0,    0,    0,    0,
    0,   64,    0,    0,    0,    0,    0,    0,   88,   79,
    0,    0,   85,    0,    0,    0,  156,  157,  155,  158,
  159,  160,  154,  146,  145,  162,  161,    0,    0,    0,
    0,    0,    0,    0,  144,    0,    0,    0,    0,    0,
    0,    0,   30,    0,    0,    0,   48,   47,   13,    0,
    0,   41,   46,    0,    0,    0,    0,    0,    0,  105,
  106,    0,    0,    0,  101,   80,    0,    0,    0,    0,
    0,   61,   51,   52,   53,   54,   55,   56,   57,    0,
   49,    0,    0,    0,  167,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   42,
   14,    0,  164,    0,   83,   65,   84,    0,  117,    0,
    0,  115,  109,  110,  112,  111,    0,  107,    0,    0,
   86,    0,    0,    0,    0,   12,   50,    0,    0,    0,
  152,    0,  150,  151,    0,    0,    0,    0,    0,    0,
   34,    0,   32,   35,   36,   31,   17,   16,   45,   44,
   43,    0,    0,    0,    0,    0,  116,  108,    0,    0,
  102,    0,    0,    0,   58,  198,  197,    0,  195,    0,
    0,    0,  168,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   39,    0,    0,    0,    0,    0,    0,
   62,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,   38,    0,    0,    0,    0,   40,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  173,
    0,    0,  179,    0,    0,    0,    0,  196,    0,  147,
  149,    0,    0,    0,    0,   37,    0,    0,    0,    0,
    0,  212,    0,    0,  210,    0,  207,  208,    0,    0,
  205,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  118,  119,  120,  121,
  122,  123,  124,  125,  126,  127,    0,  128,  129,  140,
  141,  142,  143,  131,  133,  134,  135,  136,  132,  130,
  138,  139,  137,    0,    0,    0,    0,    0,    0,   92,
  174,    0,  180,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   91,    0,  163,    0,    0,
    0,    0,  206,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  199,    0,  185,    0,    0,    0,    0,    0,
   89,    0,   90,  170,    0,  171,   94,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  225,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   93,  148,    0,   95,   96,    0,    0,    0,  214,    0,
  226,  253,    0,  232,  241,    0,  229,  256,  245,  228,
  258,  248,    0,    0,  238,  217,  240,  259,    0,    0,
  216,  153,  166,    0,    0,    0,    0,    0,    0,    0,
  200,    0,    0,    0,    0,  186,    0,    0,    0,  172,
   97,    0,    0,    0,    0,  202,  215,  254,  242,  249,
  239,  236,  250,    0,    0,    0,    0,  235,  227,    0,
    0,    0,    0,  188,    0,    0,    0,    0,    0,    0,
  211,  169,    0,  213,  203,  237,  252,    0,  201,  246,
    0,  190,  191,  189,    0,  187,  220,    0,    0,  204,
  193,    0,    0,  224,  194,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  170,  140,  132,   53,
  141,  505,  246,   54,   55,   39,  133,  125,  258,  142,
  514,  171,   64,   65,  102,  103,  154,  201,  339,   71,
  155,  207,  208,  202,  417,  434,  515,  176,  603,  365,
  549,  624,  516,  340,  341,  342,  343,  344,  506,  615,
  616,  259,  502,  503,  625,  626,  370,  371,
  };
  protected static readonly short [] yySindex = {         -164,
   19,  -71,   49,   59,   65, 2718, 2794, -159,    0, -164,
    0,    0,    0,    0,  -77,   98,  127,  383,  -69,  -24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3048,    0,    0, 2902,  -83,  -41,    0,  185,  -82,  -26,
 3048,  -23,  201,    0,    0,  -21,   17,    0,    0,    0,
    0,    0, 3048,  -46,  -12,  -68,  -48,  226,  -20,  147,
  -14,    0,  185,   18,  246,   26, 3048,   45,    0,    0,
 3048,  334, 1031,  -13,  343,  210,    0,    0, 1496, 3048,
  -46, 3048,  -46,    0,  261,    0, -231,  347,  265, 2819,
  349,    0, 3048, 3048,   24, 3048,  -11, 2640,    0,    0,
  185,   73,    0,  351, 2751, 1504,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   31,  354,  358,
 3087, 3087, 3087,  360,    0, 1496, 3048, 1496, 3048,  348,
  352,   81,    0, -231, 2847,    0,    0,    0,    0,    9,
 1496,    0,    0, 2751,  185,   53,  345,   -4,  365,    0,
    0, -131,  -31,  113,    0,    0, 2767, 2751,  154,  362,
  369,    0,    0,    0,    0,    0,    0,    0,    0,  431,
    0,  372, 3087, 3087,    0,   25,   75,   13,   61,  380,
 1496,  381,    6, 1424,  139,    0, -231,  166,   10,    0,
    0, 2872,    0,  199,    0,    0,    0, 2751,    0,  119,
 -167,    0,    0,    0,    0,    0,  -15,    0, -131, 2751,
    0,  200, -131,  171,  971,    0,    0, 3048,   67,   69,
    0, 3087,    0,    0,  188,   83,  407,   87,   90,  409,
    0,  416,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -12,  272, -167,  189, -105,    0,    0,  119, -167,
    0,  119,  119,  119,    0,    0,    0,  277,    0,  122,
 3048, 3048,    0,  191,  205,   97,  211,  212,  100, 1570,
  119,  -12, -103,    0,  196, 3595, -102,  -93,  119,  119,
    0,  971, 3087,  285,  315,  202,  191,  238,  191,    0,
  243,    0,  278, -101,  119, 3595,    0,  422, 3087, -119,
 3087, 1519, 3048, 1519, 3048, 1519, 3048, 3048, 3048, 3048,
 3048, 1519,  515, 3048, 3048, 3048, 3087, 3087, 3087, 3048,
 3048, 3087,  -50, 3087, 3087, 3087, 3087, 3087, 3087, 3087,
 3087, 3087, 2915, 2861, 3048, 3048, 2673,  110, 1550,    0,
 3595,  191,    0,  191, 3595,  -92, 3595,    0,  475,    0,
    0,  202,  191,  202,  191,    0, 3595,  -90,  -99, 1637,
 3983,    0,  249, 1462,    0,  479,    0,    0, 1496, 1519,
    0, 1496, 1496, 1519, 1496, 1496, 1519, 1496, 1496, 1496,
 1496, 1496, 1496, 1519, 3048, 1496, 1496, 1496, 1496,  483,
  489,  490,  126,  135,  491, 3048,  203,  168,  173,  174,
  175,  179,  182,  192,  194,  195,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3048,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3048,   51, 1496,  157, 3048, 2673,    0,
    0,  191,    0,  202,  202, 1715, 3595, 1793, 3087,  202,
  202, 1872, 3595, 3595,  -89,    0,  191,    0,  502,  287,
  510, 1496,    0,  520,  525, 1496,  527,  529, 1496,  530,
  534,  535,  539,  541,  543, 1496, 1496,  545,  546,  547,
  550, 3087, 3087, 3087,  240, 3048, 3048,  250, 3087, 3048,
 3048, 3048, 3048, 3048, 3048, 3048, 3048, 3048, 1496, 1496,
 1462,  554,    0,  557,    0,  519,  157,  157, 3048,  202,
    0, 1950,    0,    0,  279,    0,    0, 2028, 2107, 3595,
  202,  287,  512, 1462,  561, 1462, 1462,  565, 1462, 1462,
  566, 1462, 1462, 1462, 1462, 1462, 1462,  569,  570, 1462,
 1462, 1462, 1462,    0,  574,  577,  366, 1496,  578,  580,
 3087,  582,  185,  185,  185,  185,  185,  185,  185,  185,
  185,  583,  584,  585,  517, 3087, 2918,  591,  593,  157,
    0,    0, 3087,    0,    0, 2185,  590, 3048,    0, 1462,
    0,    0, 1462,    0,    0, 1462,    0,    0,    0,    0,
    0,    0, 1462, 1462,    0,    0,    0,    0, 3087, 3087,
    0,    0,    0,  275,  280,  595, 3087, 1462, 1462, 1462,
    0,  598, 2951, 1391,  284,    0, 2918, 2918,  603,    0,
    0,  287,  138,  601, 3004,    0,    0,    0,    0,    0,
    0,    0,    0,  389,  390, 3087,  604,    0,    0,  556,
 3087,  612,  801,    0,  406,  119, 2918,  291,  293, 2918,
    0,    0,  287,    0,    0,    0,    0,  604,    0,    0,
 2000,    0,    0,    0,  119,    0,    0,  119,  298,    0,
    0,  299,  119,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  653,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  497,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   -1,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  616,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  149,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  616,    0,  616,    0,    0,
    0,    0,    0,    0,    0,  477,    0,    0,    0,    0,
  616,    0,    0,    0,   20,  616,    0,  616,    0,    0,
    0,    0,  178,    0,    0,    0,    0,    0,    0, 1251,
 2674,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  616,    0,  616,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  305,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  329,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   94,    0,    0,    0,    0,    0,  616,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  186,  276,
    0,    0,    0,  616,  616,  324,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 2263,    0, 3673,    0,    0,    0,    0,    0,    0,
    0,  382,  708, 1033,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  616,    0,
    0,  616,  616,    0,  616,  616,    0,  616,  616,  616,
  616,  616,  616,    0,    0,  616,  616,  616,  616,    0,
    0,    0,  616,  616,    0,    0,  616,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  616,  616,    0,    0,    0,    0,
    0, 2342,    0, 2420, 3751,    0,    0,    0,    0, 1399,
 1437,    0,    0,    0,    0,    0, 3829,    0,    0,    0,
    0,  616,    0,    0,    0,  616,    0,    0,  616,    0,
    0,    0,    0,    0,    0,  616,  616,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  616,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  616,  616,
    0, 3049,    0,    0,    0,    0,    0,    0,    0, 2498,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3907,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  616,    0,    0,
    0,    0,  564,  677,  765,  843,  921, 1015, 1093, 1171,
 1259,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  616,    0,    0,    0,    0,    0,    0,
    0,    0,  616,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3127,    0,    0,    0,
    0,  331,  616,    0,    0, 3205,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3283,    0,    0,
    0,    0,    0,    0, 3361,    0,    0, 3439,    0,    0,
    0,    0, 3517,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  648,  602,    0,    0,    0,    0,  526,  528,   77,
   -6,  -28, 1499,   15,    0,  642,  485, -179, -268,    0,
  403,  496,  608,    1,    0,  513,  -42, -194,  183, -300,
  478,   78, -203, -198,    0,    0, -495,  254,    0, -444,
  206,    0,  114, -316,    0,  353,  355,  328, -424, -532,
   50,  417,    0,  133,    0,   76,  -91, -183,
  };
  protected static readonly short [] yyTable = {            38,
   38,  293,  247,  248,  234,  244,   40,   42,   60,   35,
   69,   85,   89,   73,  250,  523,   73,  276,  254,  296,
  345,  357,  441,  454,   38,   73,   73,   63,   73,  347,
  447,   61,  453,  520,   38,  257,  438,   69,   68,   67,
   36,   74,   66,  441,  130,  247,   79,   69,  131,  229,
  124,  247,  192,  192,  277,  247,  222,  279,  280,   68,
   63,   93,  159,   67,   38,  122,  101,   93,  222,   81,
   83,   97,   34,  126,   34,  128,  294,  577,  247,   15,
  247,  247,  568,  569,  648,  649,  145,  146,  197,  148,
  257,  153,   69,   98,   69,  247,  123,  180,  153,  182,
  359,  194,  257,    1,    2,  224,  245,    3,    4,   18,
    5,  637,  193,  156,  195,  212,  157,  669,  222,   19,
  181,  186,  183,   66,  187,   20,    6,    7,  121,  441,
   80,  441,   82,  191,  238,  441,  223,  153,  509,  199,
  658,  501,   92,    8,   67,  619,  363,   43,  147,  221,
  101,  153,  227,  209,  230,  243,  210,  127,   46,  129,
  247,   48,   49,   69,  364,  283,  200,   69,  275,  485,
  275,  245,  245,   66,  245,  199,   69,  651,  486,   69,
  275,  275,   45,  275,  275,   99,  463,   47,   68,   87,
  463,  153,   87,  463,  213,  441,   73,  210,   16,   17,
  463,  441,  441,  153,  199,  199,  237,  199,  670,  187,
   57,  260,  374,   21,  377,   68,  122,   68,  103,   70,
  384,  103,   22,  249,   48,   49,   69,  253,   50,   51,
   23,   24,   25,   26,   27,   28,   29,   30,   77,  242,
  252,   84,  210,  210,   69,   72,  489,  123,   75,   58,
   50,   51,   59,   88,  284,  285,  271,   91,  104,  441,
  149,   76,  107,  108,  109,   87,  110,  111,  112,   90,
  113,  203,  204,  205,  206,  100,   78,  114,  115,  121,
   48,   49,  190,  190,  116,   73,  295,  203,  204,  205,
  206,   69,   94,  551,  117,  369,  372,  373,  375,  376,
  378,  379,  380,  381,  382,  383,  386,  387,  388,  389,
  396,   96,  272,  393,  394,  210,  397,  281,  356,  572,
  282,  282,  573,   18,  646,  350,   69,  647,  435,  436,
   38,  667,  106,  668,  647,  459,  647,  437,  674,  675,
  461,  647,  282,  464,  465,  104,  467,  468,  104,  470,
  471,  472,  473,  474,  475,  351,   69,  478,  479,  480,
  481,   98,   98,  462,  118,   98,   98,  466,   98,   33,
  469,  192,   33,   98,  192,  177,  178,  476,  477,  119,
  120,   19,  105,   67,   98,   98,  134,  135,  144,  488,
  158,  172,  672,  173,  107,  108,  109,  174,  110,  111,
  112,   98,  113,  179,  198,  184,  196,  504,  215,  185,
  499,  218,  235,  107,  108,  109,  116,  110,  111,  112,
  225,  113,  214,  226,  228,  199,  117,  500,  114,  115,
  255,   38,   38,  525,  261,  116,  262,  528,  507,  508,
  531,  248,  265,  652,  264,  117,  267,  538,  539,  268,
  266,  665,  269,   99,   99,  270,  288,   99,   99,  291,
   99,  287,  274,  663,  245,  122,  247,  289,  290,  297,
  562,  563,  564,  673,  247,  275,   99,   99,  360,  548,
  548,  257,  361,  553,  554,  555,  556,  557,  558,  559,
  560,  561,  143,   99,  353,  579,  123,  581,  582,  355,
  584,  585,   38,  587,  588,  589,  590,  591,  592,  570,
  439,  595,  596,  597,  598,  118,   69,  165,  449,  602,
  165,  458,  460,  175,  175,  175,  482,  446,  121,  448,
  119,  120,  483,  484,  487,  490,   68,  143,  165,  452,
  491,  492,  493,  100,  100,  522,  494,  100,  100,  495,
  100,  627,  363,  524,  628,  216,   68,  629,  567,  496,
  614,  497,  498,  526,  630,  631,  100,  100,  527,  165,
  529,  623,  530,  532,   35,  219,  220,  533,  534,  638,
  639,  640,  535,  100,  536,  644,  537,   68,  540,  541,
  542,   18,   18,  543,  241,   18,   18,  565,   18,  547,
  566,  165,  578,   68,  580,   36,  643,  501,  583,  586,
  614,  614,  593,  594,   18,   18,  664,  599,  623,   68,
  600,  604,  601,  605,  263,  607,  608,  609,  610,  512,
  617,   18,  618,  622,  634,  518,  519,   34,  636,  635,
  614,  641,  650,  614,  653,  656,  657,  573,  659,   19,
   19,  661,    1,   19,   19,   68,   19,   44,   86,   56,
  189,  188,  107,  108,  109,  217,  110,  111,  112,  211,
  113,  236,   19,   19,   95,   48,   49,  114,  115,   50,
   51,   52,   32,   33,  116,  349,  620,  251,  457,   19,
  160,  645,  550,  442,  117,  443,  666,  611,  348,    0,
  655,  362,  576,  366,    0,    0,  161,   20,  203,  204,
  205,  206,    0,    0,    0,    0,   68,    0,    0,  390,
  391,  392,    0,    0,  395,    0,  398,  399,  400,  401,
  402,  403,  404,  405,  406,    0,  162,    0,    0,  163,
  164,  165,  166,  167,  168,  169,    0,    0,    0,  165,
  165,    0,    0,   68,   68,   68,    0,   68,   68,   68,
    0,   68,    0,    0,  118,    0,    0,    0,   68,   68,
    0,    0,    0,    0,    0,   68,    0,    0,   21,  119,
  120,    0,    0,    0,    0,   68,    0,   22,    0,    0,
    0,    0,  165,  165,  165,   23,   24,   25,   26,   27,
   28,   29,   30,  165,   68,    0,  165,  165,  165,  165,
  165,  165,  165,  165,  165,    0,    0,  165,  165,    0,
    0,  165,  165,  165,  165,  165,  165,    0,    0,  165,
  165,  165,    0,    0,    0,  165,  255,  255,    0,  165,
  165,  165,   69,    0,  165,  165,  165,  165,  165,  165,
    0,  165,    0,  165,    0,   68,    0,  385,    0,    0,
    0,    0,    0,    0,  165,    0,    0,    0,    0,    0,
   68,   68,    0,    0,    0,  165,  165,  165,  165,  255,
  255,  255,   68,    0,  544,  545,  546,    0,    0,    0,
  255,  552,    0,  255,  255,  255,  255,  255,  255,  255,
  255,  255,    0,    0,  255,  255,    0,    0,  255,  255,
  255,  255,  255,  255,    0,    0,  255,  255,  255,    0,
    0,    0,  255,    0,    0,    0,  255,  255,  255,    0,
    0,    0,  255,  255,  255,  255,  255,    0,  255,    0,
  255,    0,    0,    0,    0,    0,    0,    0,    0,  260,
  260,  255,    0,  606,    0,    0,    0,    0,    0,    0,
   68,    0,  255,  255,  255,  255,    0,    0,  612,    0,
    0,    0,    0,    0,    0,   20,   20,    0,    0,   20,
   20,    0,   20,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  260,  260,  260,    0,    0,    0,   20,   20,
    0,  632,  633,  260,    0,    0,  260,  260,  260,  260,
  260,  260,  260,  260,  260,   20,    0,  260,  260,    0,
    0,  260,  260,  260,  260,  260,  260,    0,    0,  260,
  260,  260,   24,    0,    0,  260,    0,  247,  247,  260,
  260,  260,    0,  660,    0,  260,  260,  260,  260,  260,
    0,  260,    0,  260,   68,    0,    0,  107,  108,  109,
    0,  110,  111,  112,  260,  113,    0,    0,    0,    0,
    0,  100,    0,  662,    0,  260,  260,  260,  260,  116,
  247,  247,  247,    0,    0,    0,    0,    0,    0,  117,
   35,  247,    0,    0,  247,  247,  247,  247,  247,  247,
  247,  247,  247,    0,    0,  247,  247,    0,    0,  247,
  247,  247,  247,  247,  247,  230,  230,  247,  247,  247,
    0,   36,    0,  247,    0,    0,    0,  247,  247,  247,
    0,    0,   68,  247,  247,  247,  247,  247,    0,  247,
    0,  247,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  247,   34,    0,    0,    0,    0,  230,  230,
  230,    0,    0,  247,  247,  247,  247,    0,    0,  230,
    0,    0,  230,  230,  230,  230,  230,  230,  230,  230,
  230,    0,    0,  230,  230,    0,    0,  230,  230,  230,
  230,  230,  230,  231,  231,  230,  230,  230,    0,    0,
    0,  230,    0,    0,    0,  230,  230,  230,    0,    0,
   68,  230,  230,  230,  230,  230,    0,  230,    0,  230,
    0,    0,    0,    0,    0,    0,    0,  107,  108,  109,
  230,  110,  111,  112,    0,  113,  231,  231,  231,    0,
    0,  230,  230,  230,  230,    0,  256,  231,    0,  116,
  231,  231,  231,  231,  231,  231,  231,  231,  231,  117,
    0,  231,  231,    0,    0,  231,  231,  231,  231,  231,
  231,    0,    0,  231,  231,  231,    0,    0,    0,  231,
    0,    0,    0,  231,  231,  231,    0,  257,  257,  231,
  231,  231,  231,  231,   21,  231,    0,  231,   68,    0,
   24,   24,    0,   22,   24,   24,    0,   24,  231,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,  231,
  231,  231,  231,   24,   24,    0,   99,    0,    0,    0,
  257,  257,  257,    0,    0,    0,    0,    0,    0,    0,
   24,  257,    0,    0,  257,  257,  257,  257,  257,  257,
  257,  257,  257,    0,    0,  257,  257,    0,    0,  257,
  257,  257,  257,  257,  257,  251,  251,  257,  257,  257,
    0,    0,    0,  257,    0,   59,    0,  257,  257,  257,
    0,    0,    0,  257,  257,  257,  257,  257,    0,  257,
    0,  257,    0,    0,    0,    0,    0,    0,   21,    0,
    0,    0,  257,    0,    0,    0,    0,    0,  251,  251,
  251,    0,    0,  257,  257,  257,  257,    0,    0,  251,
    0,    0,  251,  251,  251,  251,  251,  251,  251,  251,
  251,    0,   69,  251,  251,    0,   23,  251,  251,  251,
  251,  251,  251,  244,  244,  251,  251,  251,    0,    0,
  122,  251,    0,    0,    0,  251,  251,  251,    0,    0,
    0,  251,  251,  251,  251,  251,    0,  251,    0,  251,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  251,  123,    0,    0,    0,    0,  244,  244,  244,    0,
    0,  251,  251,  251,  251,    0,    0,  244,    0,    0,
  244,  244,  244,  244,  244,  244,  244,  244,  244,    0,
   59,  244,  244,  121,    0,  244,  244,  244,  244,  244,
  244,  122,    0,  244,  244,  244,   59,    0,    0,  244,
    0,  218,  218,  244,  244,  244,    0,   69,    0,  244,
  244,  244,  244,  244,    0,  244,    0,  244,    0,    0,
    0,    0,  123,    0,    0,  122,   59,    0,  244,   59,
   59,   59,   59,   59,   59,   59,    0,    0,    0,  244,
  244,  244,  244,    0,  218,  218,  218,    0,   35,    0,
    0,    0,    0,    0,  121,  218,  123,    0,  218,  218,
  218,  218,  218,  218,  218,  218,  218,    0,    0,  218,
  218,    0,    0,  218,  218,  218,  218,  218,  218,   36,
  292,  218,  218,  218,    0,    0,    0,  218,  121,    0,
    0,  218,  218,  218,    0,    0,    0,  218,  218,  218,
  218,  218,    0,  218,    0,  218,    0,    0,    0,    0,
    0,   34,    0,    0,    0,    0,  218,  107,  108,  109,
    0,  110,  111,  112,    0,  113,    0,  218,  218,  218,
  218,    0,  114,  115,    0,    0,   21,   21,    0,  116,
   21,   21,    0,   21,  440,    0,    0,    0,    0,  117,
  107,  108,  109,  231,  110,  111,  112,    0,  113,   21,
   21,    0,    0,  203,  204,  205,  206,  232,    0,  233,
    0,    0,  116,    0,   23,   23,   21,    0,   23,   23,
    0,   23,  117,    0,    0,    0,    0,    0,  107,  108,
  109,    0,  110,  111,  112,    0,  113,   23,   23,    0,
    0,    0,    0,  114,  115,    0,    0,    0,    0,    0,
  116,    0,  273,    0,   23,    0,    0,    0,  278,  118,
  117,    0,  107,  108,  109,    0,  110,  111,  112,    0,
  113,  456,  286,  160,  119,  120,    0,  114,  115,    0,
    0,    0,    0,    0,  116,  346,    0,    0,    0,  161,
    0,    0,   21,    0,  117,  352,    0,  354,    0,    0,
    0,   22,  358,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,    0,  162,
    0,    0,  163,  164,  165,  166,  167,  168,  169,    0,
  118,    0,  298,    0,    0,    0,  107,  108,  109,    0,
  110,  111,  112,    0,  113,  119,  120,    0,    0,  511,
  444,    0,  445,    0,    0,  256,  367,  368,  116,    0,
    0,  450,    0,  451,  118,    0,    0,  455,  117,    0,
    0,    0,    0,    0,    0,  299,  300,  301,    0,  119,
  120,    0,    0,    0,    0,    0,  302,    0,    0,  303,
  304,  305,  306,  307,  308,  309,  310,  311,    0,    0,
  312,  313,    0,    0,  314,  315,  316,  317,  318,  319,
    0,    0,  320,  321,  322,    0,    0,    0,  323,  298,
    0,    0,  324,  325,  326,    0,    0,  513,  327,  328,
  329,  330,  331,    0,  332,    0,  333,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  334,    0,    0,
  510,    0,    0,    0,    0,    0,    0,    0,  335,  336,
  337,  338,  299,  300,  301,  521,    0,    0,    0,    0,
    0,    0,    0,  302,    0,    0,  303,  304,  305,  306,
  307,  308,  309,  310,  311,    0,    0,  312,  313,    0,
    0,  314,  315,  316,  317,  318,  319,  298,    0,  320,
  321,  322,    0,    0,    0,  323,  517,    0,    0,  324,
  325,  326,    0,    0,    0,  327,  328,  329,  330,  331,
    0,  332,    0,  333,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  334,    0,    0,    0,    0,    0,
  299,  300,  301,    0,    0,  335,  336,  337,  338,    0,
  671,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,    0,    0,  312,  313,    0,    0,  314,
  315,  316,  317,  318,  319,  298,    0,  320,  321,  322,
    0,    0,    0,  323,  571,    0,    0,  324,  325,  326,
    0,    0,    0,  327,  328,  329,  330,  331,    0,  332,
    0,  333,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  334,    0,    0,    0,    0,    0,  299,  300,
  301,    0,    0,  335,  336,  337,  338,    0,    0,  302,
    0,    0,  303,  304,  305,  306,  307,  308,  309,  310,
  311,    0,    0,  312,  313,    0,    0,  314,  315,  316,
  317,  318,  319,    0,  298,  320,  321,  322,    0,    0,
    0,  323,  574,    0,    0,  324,  325,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,    0,  333,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  334,    0,    0,    0,    0,    0,    0,  299,  300,  301,
    0,  335,  336,  337,  338,    0,    0,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
    0,    0,  312,  313,    0,    0,  314,  315,  316,  317,
  318,  319,  298,    0,  320,  321,  322,    0,    0,    0,
  323,  575,    0,    0,  324,  325,  326,    0,    0,    0,
  327,  328,  329,  330,  331,    0,  332,    0,  333,    0,
    0,    0,    0,    0,    0,    0,  107,  108,  109,  334,
  110,  111,  112,    0,  113,  299,  300,  301,    0,    0,
  335,  336,  337,  338,    0,  256,  302,    0,  116,  303,
  304,  305,  306,  307,  308,  309,  310,  311,  117,    0,
  312,  313,    0,    0,  314,  315,  316,  317,  318,  319,
  298,    0,  320,  321,  322,    0,    0,    0,  323,  621,
    0,    0,  324,  325,  326,    0,    0,    0,  327,  328,
  329,  330,  331,    0,  332,    0,  333,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  334,    0,    0,
    0,    0,    0,  299,  300,  301,    0,    0,  335,  336,
  337,  338,    0,    0,  302,    0,    0,  303,  304,  305,
  306,  307,  308,  309,  310,  311,    0,    0,  312,  313,
    0,    0,  314,  315,  316,  317,  318,  319,    0,  298,
  320,  321,  322,    0,    0,    0,  323,  177,    0,    0,
  324,  325,  326,    0,    0,    0,  327,  328,  329,  330,
  331,    0,  332,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  334,    0,    0,    0,    0,
    0,    0,  299,  300,  301,    0,  335,  336,  337,  338,
    0,    0,    0,  302,    0,    0,  303,  304,  305,  306,
  307,  308,  309,  310,  311,    0,    0,  312,  313,    0,
    0,  314,  315,  316,  317,  318,  319,  298,    0,  320,
  321,  322,    0,    0,    0,  323,  175,    0,    0,  324,
  325,  326,    0,    0,    0,  327,  328,  329,  330,  331,
    0,  332,    0,  333,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  334,    0,    0,    0,    0,    0,
  299,  300,  301,    0,    0,  335,  336,  337,  338,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,    0,    0,  312,  313,    0,    0,  314,
  315,  316,  317,  318,  319,  177,    0,  320,  321,  322,
    0,    0,    0,  323,  178,    0,    0,  324,  325,  326,
    0,    0,    0,  327,  328,  329,  330,  331,    0,  332,
    0,  333,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  334,    0,    0,    0,    0,    0,  177,  177,
  177,    0,    0,  335,  336,  337,  338,    0,    0,  177,
    0,    0,  177,  177,  177,  177,  177,  177,  177,  177,
  177,    0,    0,  177,  177,    0,    0,  177,  177,  177,
  177,  177,  177,    0,  175,  177,  177,  177,    0,    0,
    0,  177,  176,    0,    0,  177,  177,  177,    0,    0,
    0,  177,  177,  177,  177,  177,    0,  177,    0,  177,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  177,    0,    0,    0,    0,    0,    0,  175,  175,  175,
    0,  177,  177,  177,  177,    0,    0,    0,  175,    0,
    0,  175,  175,  175,  175,  175,  175,  175,  175,  175,
  152,    0,  175,  175,    0,    0,  175,  175,  175,  175,
  175,  175,  178,    0,  175,  175,  175,    0,    0,   35,
  175,    0,    0,    0,  175,  175,  175,    0,    0,    0,
  175,  175,  175,  175,  175,    0,  175,    0,  175,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  175,
   36,    0,   35,    0,    0,  178,  178,  178,    0,    0,
  175,  175,  175,  175,    0,    0,  178,    0,    0,  178,
  178,  178,  178,  178,  178,  178,  178,  178,    0,    0,
  178,  178,   34,   36,  178,  178,  178,  178,  178,  178,
  176,    0,  178,  178,  178,    0,    0,   35,  178,    0,
    0,    0,  178,  178,  178,    0,    0,    0,  178,  178,
  178,  178,  178,    0,  178,   34,  178,    0,   60,    0,
    0,    0,    0,    0,    0,    0,    0,  178,   36,    0,
   35,    0,    0,  176,  176,  176,    0,    0,  178,  178,
  178,  178,    0,    0,  176,    0,   35,  176,  176,  176,
  176,  176,  176,  176,  176,  176,    0,    0,  176,  176,
   34,   36,  176,  176,  176,  176,  176,  176,    0,    0,
  176,  176,  176,   35,    0,    0,  176,   36,    0,    0,
  176,  176,  176,    0,    0,    0,  176,  176,  176,  176,
  176,    0,  176,   34,  176,    0,    0,    0,   35,    0,
    0,    0,    0,    0,   36,  176,    0,    0,    0,   34,
    0,    0,    0,    0,    0,    0,  176,  176,  176,  176,
    0,    0,    0,   21,    0,    0,   35,    0,    0,   36,
    0,    0,   22,    0,    0,    0,   34,  150,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,   35,    0,   60,    0,  151,   21,   36,    0,    0,
    0,   34,    0,  139,    0,   22,    0,    0,    0,   60,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,   35,   36,    0,    0,    0,    0,    0,    0,   34,
    0,    0,    0,    0,   70,    0,    0,   35,    0,   60,
    0,   21,   60,   60,   60,   60,   60,   60,   60,    0,
   22,    0,   36,    0,   34,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,   36,    0,
   35,    0,   31,    0,   21,    0,    0,   32,   33,    0,
    0,    0,    0,   22,   34,    0,   62,    0,  150,    0,
   21,   23,   24,   25,   26,   27,   28,   29,   30,   22,
   34,   36,    0,    0,    0,    0,  151,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,   21,    0,    0,
    0,    0,   99,   35,    0,    0,   22,    0,    0,    0,
    0,    0,    0,   34,   23,   24,   25,   26,   27,   28,
   29,   30,  136,  137,    0,    0,    0,    0,   41,    0,
    0,   22,  138,    0,   36,    0,  654,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,   35,    0,    0,
  136,  137,    0,    0,    0,    0,    0,    0,    0,   22,
  138,  418,  419,    0,    0,    0,   34,   23,   24,   25,
   26,   27,   28,   29,   30,  136,  239,    0,   36,    0,
    0,    0,    0,    0,   22,  240,   35,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,    0,    0,    0,   21,    0,    0,    0,    0,
   34,    0,    0,    0,   22,    0,    0,   36,    0,    0,
    0,   21,   23,   24,   25,   26,   27,   28,   29,   30,
   22,    0,    0,    0,    0,  613,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,   34,
    0,    0,    0,    0,   21,    0,    0,    0,    0,    0,
    0,    0,    0,   22,  642,    0,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
  420,  421,  422,  423,    0,    0,    0,    0,    0,  424,
  425,  426,  427,  428,  429,  430,  431,  432,  433,    0,
    0,    0,    0,    0,    0,    0,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,  407,  408,  409,  410,  411,  412,  413,  414,
  415,  416,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,  243,  243,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  136,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,  243,  243,  243,   23,   24,   25,
   26,   27,   28,   29,   30,  243,    0,    0,  243,  243,
  243,  243,  243,  243,  243,  243,  243,    0,    0,  243,
  243,    0,    0,  243,  243,  243,  243,  243,  243,  233,
  233,  243,  243,  243,    0,    0,    0,  243,    0,    0,
    0,  243,  243,  243,    0,    0,    0,  243,  243,  243,
  243,  243,    0,  243,    0,  243,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  243,    0,    0,    0,
    0,    0,  233,  233,  233,    0,    0,  243,  243,  243,
  243,    0,    0,  233,    0,    0,  233,  233,  233,  233,
  233,  233,  233,  233,  233,    0,    0,  233,  233,    0,
    0,  233,  233,  233,  233,  233,  233,  219,  219,  233,
  233,  233,    0,    0,    0,  233,    0,    0,    0,  233,
  233,  233,    0,    0,    0,  233,  233,  233,  233,  233,
    0,  233,    0,  233,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  233,    0,    0,    0,    0,    0,
  219,  219,  219,    0,    0,  233,  233,  233,  233,    0,
    0,  219,    0,    0,  219,  219,  219,  219,  219,  219,
  219,  219,  219,    0,    0,  219,  219,    0,    0,  219,
  219,  219,  219,  219,  219,  234,  234,  219,  219,  219,
    0,    0,    0,  219,    0,    0,    0,  219,  219,  219,
    0,    0,    0,  219,  219,  219,  219,  219,    0,  219,
    0,  219,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  219,    0,    0,    0,    0,    0,  234,  234,
  234,    0,    0,  219,  219,  219,  219,    0,    0,  234,
    0,    0,  234,  234,  234,  234,  234,  234,  234,  234,
  234,    0,    0,  234,  234,    0,    0,  234,  234,  234,
  234,  234,  234,  221,  221,  234,  234,  234,    0,    0,
    0,  234,    0,    0,    0,  234,  234,  234,    0,    0,
    0,  234,  234,  234,  234,  234,    0,  234,    0,  234,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  234,    0,    0,    0,    0,    0,  221,  221,  221,    0,
    0,  234,  234,  234,  234,    0,    0,  221,    0,    0,
  221,  221,  221,  221,  221,  221,  221,  221,  221,    0,
    0,  221,  221,    0,    0,  221,  221,  221,  221,  221,
  221,  223,  223,  221,  221,  221,    0,    0,    0,  221,
    0,    0,    0,  221,  221,  221,    0,    0,    0,  221,
  221,  221,  221,  221,    0,  221,    0,  221,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  221,    0,
    0,    0,    0,    0,  223,  223,  223,    0,    0,  221,
  221,  221,  221,    0,    0,  223,    0,    0,  223,  223,
  223,  223,  223,  223,  223,  223,  223,    0,    0,  223,
  223,    0,    0,  223,  223,  223,  223,  223,  223,  222,
  222,  223,  223,  223,    0,    0,    0,  223,    0,    0,
    0,  223,  223,  223,    0,    0,    0,  223,  223,  223,
  223,  223,    0,  223,    0,  223,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  223,    0,    0,    0,
    0,    0,  222,  222,  222,    0,    0,  223,  223,  223,
  223,    0,    0,  222,    0,    0,  222,  222,  222,  222,
  222,  222,  222,  222,  222,    0,    0,  222,  222,    0,
    0,  222,  222,  222,  222,  222,  222,  298,    0,  222,
  222,  222,    0,    0,    0,  222,    0,    0,    0,  222,
  222,  222,    0,    0,    0,  222,  222,  222,  222,  222,
    0,  222,    0,  222,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  222,    0,    0,    0,    0,    0,
  299,  300,  301,    0,    0,  222,  222,  222,  222,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,    0,    0,  312,  313,    0,    0,  314,
  315,  316,  317,  318,  319,  181,    0,  320,  321,  322,
    0,    0,    0,  323,    0,    0,    0,  324,  325,  326,
    0,    0,    0,  327,  328,  329,  330,  331,    0,  332,
    0,  333,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  334,    0,    0,    0,    0,    0,  181,  181,
  181,    0,    0,  335,  336,  337,  338,    0,    0,  181,
    0,    0,  181,  181,  181,  181,  181,  181,  181,  181,
  181,    0,    0,  181,  181,    0,    0,  181,  181,  181,
  181,  181,  181,  182,    0,  181,  181,  181,    0,    0,
    0,  181,    0,    0,    0,  181,  181,  181,    0,    0,
    0,  181,  181,  181,  181,  181,    0,  181,    0,  181,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  181,    0,    0,    0,    0,    0,  182,  182,  182,    0,
    0,  181,  181,  181,  181,    0,    0,  182,    0,    0,
  182,  182,  182,  182,  182,  182,  182,  182,  182,    0,
    0,  182,  182,    0,    0,  182,  182,  182,  182,  182,
  182,  183,    0,  182,  182,  182,    0,    0,    0,  182,
    0,    0,    0,  182,  182,  182,    0,    0,    0,  182,
  182,  182,  182,  182,    0,  182,    0,  182,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  182,    0,
    0,    0,    0,    0,  183,  183,  183,    0,    0,  182,
  182,  182,  182,    0,    0,  183,    0,    0,  183,  183,
  183,  183,  183,  183,  183,  183,  183,    0,    0,  183,
  183,    0,    0,  183,  183,  183,  183,  183,  183,  184,
    0,  183,  183,  183,    0,    0,    0,  183,    0,    0,
    0,  183,  183,  183,    0,    0,    0,  183,  183,  183,
  183,  183,    0,  183,    0,  183,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  183,    0,    0,    0,
    0,    0,  184,  184,  184,    0,    0,  183,  183,  183,
  183,    0,    0,  184,    0,    0,  184,  184,  184,  184,
  184,  184,  184,  184,  184,    0,    0,  184,  184,    0,
    0,  184,  184,  184,  184,  184,  184,    0,    0,  184,
  184,  184,    0,    0,    0,  184,    0,    0,    0,  184,
  184,  184,    0,    0,    0,  184,  184,  184,  184,  184,
    0,  184,    0,  184,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  184,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  184,  184,  184,  184,  302,
    0,    0,  303,  304,  305,  306,  307,  308,  309,  310,
  311,    0,    0,  312,  313,    0,    0,  314,  315,  316,
  317,  318,  319,    0,    0,  320,  321,  322,    0,    0,
    0,  323,    0,    0,    0,  324,  325,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,    0,  333,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  334,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  335,  336,  337,  338,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  270,  201,  207,  184,  200,    6,    7,   33,   60,
   42,   60,   33,   40,  209,  460,   40,  123,  213,  123,
  123,  123,  339,  123,   31,   40,   40,   34,   40,  123,
  123,   31,  123,  123,   41,  215,  337,   42,   40,  123,
   91,   41,   44,  360,  276,  244,   53,   42,  280,   44,
   79,  250,   44,   44,  249,  254,   44,  252,  253,   40,
   67,   44,  105,   44,   71,   60,   73,   44,   44,   55,
   56,   71,  123,   80,  123,   82,  271,  522,  277,   61,
  279,  280,  507,  508,  617,  618,   93,   94,   93,   96,
  270,   98,   42,    0,   42,  294,   91,  126,  105,  128,
  295,  144,  282,  268,  269,   93,  274,  272,  273,   61,
  275,  607,  141,   41,   62,  158,   44,  650,   44,   61,
  127,   41,  129,  125,   44,   61,  291,  292,  123,  446,
   54,  448,   56,  125,  125,  452,   62,  144,  439,  307,
  636,   91,  125,  308,  125,  570,  266,  307,  125,  125,
  157,  158,  181,   41,  183,  198,   44,   81,   61,   83,
  359,  293,  294,   42,  284,   44,  152,   42,  274,   44,
  274,  274,  274,  257,  274,  307,   42,  622,   44,   42,
  274,  274,  260,  274,  274,    0,  370,   61,   40,   41,
  374,  198,   44,  377,   41,  512,   40,   44,  270,  271,
  384,  518,  519,  210,  307,  307,   41,  307,  653,   44,
  280,  218,  304,  264,  306,  257,   60,   40,   41,  302,
  312,   44,  273,  209,  293,  294,   42,  213,  297,  298,
  281,  282,  283,  284,  285,  286,  287,  288,  260,   41,
   41,  290,   44,   44,   42,  272,   44,   91,  272,  274,
  297,  298,  277,  274,  261,  262,  242,  272,  272,  576,
  272,   61,  257,  258,  259,   40,  261,  262,  263,  123,
  265,  303,  304,  305,  306,    0,  260,  272,  273,  123,
  293,  294,  274,  274,  279,   40,  272,  303,  304,  305,
  306,   42,  267,   44,  289,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  315,  316,
  361,  267,   41,  320,  321,   44,  323,   41,   41,   41,
   44,   44,   44,    0,   41,   41,   42,   44,  335,  336,
  337,   41,  123,   41,   44,  364,   44,  337,   41,   41,
  369,   44,   44,  372,  373,   41,  375,  376,   44,  378,
  379,  380,  381,  382,  383,   41,   42,  386,  387,  388,
  389,  268,  269,  370,  359,  272,  273,  374,  275,   41,
  377,   41,   44,   40,   44,  122,  123,  384,  385,  374,
  375,    0,   40,  123,  291,  292,   40,  123,   40,  396,
   40,  361,  661,   40,  257,  258,  259,   40,  261,  262,
  263,  308,  265,   44,   40,   58,   62,  436,   40,   58,
  417,   40,  274,  257,  258,  259,  279,  261,  262,  263,
  360,  265,   61,   44,   44,  307,  289,  434,  272,  273,
  260,  438,  439,  462,  368,  279,  368,  466,  438,  439,
  469,  645,  360,  623,  257,  289,  360,  476,  477,  360,
   44,  646,   44,  268,  269,   40,  360,  272,  273,  360,
  275,  257,  274,  643,  274,   60,  665,  257,  257,  274,
  499,  500,  501,  668,  673,  274,  291,  292,  296,  486,
  487,  661,   61,  490,  491,  492,  493,  494,  495,  496,
  497,  498,   90,  308,  257,  524,   91,  526,  527,  257,
  529,  530,  509,  532,  533,  534,  535,  536,  537,  509,
  401,  540,  541,  542,  543,  359,   40,   41,   44,  548,
   44,  273,   44,  121,  122,  123,   44,  345,  123,  347,
  374,  375,   44,   44,   44,  368,   40,  135,   62,  357,
  368,  368,  368,  268,  269,   44,  368,  272,  273,  368,
  275,  580,  266,   44,  583,  125,   60,  586,   40,  368,
  567,  368,  368,   44,  593,  594,  291,  292,   44,   93,
   44,  578,   44,   44,   60,  173,  174,   44,   44,  608,
  609,  610,   44,  308,   44,  614,   44,   91,   44,   44,
   44,  268,  269,   44,  192,  272,  273,   44,  275,  360,
   44,  125,   91,   40,   44,   91,  613,   91,   44,   44,
  617,  618,   44,   44,  291,  292,  645,   44,  625,  123,
   44,   44,  257,   44,  222,   44,   44,   44,   44,  447,
   40,  308,   40,   44,  360,  453,  454,  123,   44,  360,
  647,   44,   40,  650,   44,  257,  257,   44,   93,  268,
  269,   40,    0,  272,  273,   40,  275,   10,   57,   18,
  135,  134,  257,  258,  259,  170,  261,  262,  263,  157,
  265,  187,  291,  292,   67,  293,  294,  272,  273,  297,
  298,  299,  300,  301,  279,  283,  573,  210,  361,  308,
  260,  614,  487,  341,  289,  341,  647,  565,  282,   -1,
  625,  299,  520,  301,   -1,   -1,  276,    0,  303,  304,
  305,  306,   -1,   -1,   -1,   -1,   40,   -1,   -1,  317,
  318,  319,   -1,   -1,  322,   -1,  324,  325,  326,  327,
  328,  329,  330,  331,  332,   -1,  306,   -1,   -1,  309,
  310,  311,  312,  313,  314,  315,   -1,   -1,   -1,  273,
  274,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,  359,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,  264,  374,
  375,   -1,   -1,   -1,   -1,  289,   -1,  273,   -1,   -1,
   -1,   -1,  316,  317,  318,  281,  282,  283,  284,  285,
  286,  287,  288,  327,   40,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,  273,  274,   -1,  363,
  364,  365,   42,   -1,  368,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,  359,   -1,  343,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  374,  375,   -1,   -1,   -1,  399,  400,  401,  402,  316,
  317,  318,   40,   -1,  482,  483,  484,   -1,   -1,   -1,
  327,  489,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,   -1,   -1,  353,  354,  355,   -1,
   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  274,  388,   -1,  551,   -1,   -1,   -1,   -1,   -1,   -1,
   40,   -1,  399,  400,  401,  402,   -1,   -1,  566,   -1,
   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,   -1,  291,  292,
   -1,  599,  600,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,  308,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,  353,
  354,  355,    0,   -1,   -1,  359,   -1,  273,  274,  363,
  364,  365,   -1,  641,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   40,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  388,  265,   -1,   -1,   -1,   -1,
   -1,   41,   -1,  273,   -1,  399,  400,  401,  402,  279,
  316,  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,  289,
   60,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   91,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   -1,   40,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,  123,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,  274,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   40,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  388,  261,  262,  263,   -1,  265,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,  276,  327,   -1,  279,
  330,  331,  332,  333,  334,  335,  336,  337,  338,  289,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,   -1,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,  273,  274,  369,
  370,  371,  372,  373,  264,  375,   -1,  377,   40,   -1,
  268,  269,   -1,  273,  272,  273,   -1,  275,  388,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,  399,
  400,  401,  402,  291,  292,   -1,  296,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  308,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,  125,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,    0,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   42,  341,  342,   -1,    0,  345,  346,  347,
  348,  349,  350,  273,  274,  353,  354,  355,   -1,   -1,
   60,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   91,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
  260,  341,  342,  123,   -1,  345,  346,  347,  348,  349,
  350,   60,   -1,  353,  354,  355,  276,   -1,   -1,  359,
   -1,  273,  274,  363,  364,  365,   -1,   42,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   60,  306,   -1,  388,  309,
  310,  311,  312,  313,  314,  315,   -1,   -1,   -1,  399,
  400,  401,  402,   -1,  316,  317,  318,   -1,   60,   -1,
   -1,   -1,   -1,   -1,  123,  327,   91,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,   91,
   41,  353,  354,  355,   -1,   -1,   -1,  359,  123,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,  388,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,  399,  400,  401,
  402,   -1,  272,  273,   -1,   -1,  268,  269,   -1,  279,
  272,  273,   -1,  275,  125,   -1,   -1,   -1,   -1,  289,
  257,  258,  259,  260,  261,  262,  263,   -1,  265,  291,
  292,   -1,   -1,  303,  304,  305,  306,  274,   -1,  276,
   -1,   -1,  279,   -1,  268,  269,  308,   -1,  272,  273,
   -1,  275,  289,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  291,  292,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,  244,   -1,  308,   -1,   -1,   -1,  250,  359,
  289,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,  125,  264,  260,  374,  375,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,  277,   -1,   -1,   -1,  276,
   -1,   -1,  264,   -1,  289,  287,   -1,  289,   -1,   -1,
   -1,  273,  294,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,   -1,   -1,  306,
   -1,   -1,  309,  310,  311,  312,  313,  314,  315,   -1,
  359,   -1,  273,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,  374,  375,   -1,   -1,  125,
  342,   -1,  344,   -1,   -1,  276,  328,  329,  279,   -1,
   -1,  353,   -1,  355,  359,   -1,   -1,  359,  289,   -1,
   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,  374,
  375,   -1,   -1,   -1,   -1,   -1,  327,   -1,   -1,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   -1,
  341,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
   -1,   -1,  353,  354,  355,   -1,   -1,   -1,  359,  273,
   -1,   -1,  363,  364,  365,   -1,   -1,  125,  369,  370,
  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,
  442,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,
  401,  402,  316,  317,  318,  457,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   41,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,   -1,  353,  354,  355,
   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,   -1,  273,  353,  354,  355,   -1,   -1,
   -1,  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,  399,  400,  401,  402,   -1,   -1,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,
  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  388,
  261,  262,  263,   -1,  265,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,   -1,  276,  327,   -1,  279,  330,
  331,  332,  333,  334,  335,  336,  337,  338,  289,   -1,
  341,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
  273,   -1,  353,  354,  355,   -1,   -1,   -1,  359,  125,
   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,
   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,
  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,   -1,  341,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,   -1,  273,
  353,  354,  355,   -1,   -1,   -1,  359,  125,   -1,   -1,
  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,
  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,  399,  400,  401,  402,
   -1,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,   -1,  353,  354,  355,
   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,   -1,  273,  353,  354,  355,   -1,   -1,
   -1,  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,  399,  400,  401,  402,   -1,   -1,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   41,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,   60,
  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   91,   -1,   60,   -1,   -1,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   -1,
  341,  342,  123,   91,  345,  346,  347,  348,  349,  350,
  273,   -1,  353,  354,  355,   -1,   -1,   60,  359,   -1,
   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,  123,  377,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   91,   -1,
   60,   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,
  401,  402,   -1,   -1,  327,   -1,   60,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,   -1,  341,  342,
  123,   91,  345,  346,  347,  348,  349,  350,   -1,   -1,
  353,  354,  355,   60,   -1,   -1,  359,   91,   -1,   -1,
  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,
  373,   -1,  375,  123,  377,   -1,   -1,   -1,   60,   -1,
   -1,   -1,   -1,   -1,   91,  388,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,
   -1,   -1,   -1,  264,   -1,   -1,   60,   -1,   -1,   91,
   -1,   -1,  273,   -1,   -1,   -1,  123,  278,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   60,   -1,  260,   -1,  296,  264,   91,   -1,   -1,
   -1,  123,   -1,  125,   -1,  273,   -1,   -1,   -1,  276,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,   60,   91,   -1,   -1,   -1,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,  302,   -1,   -1,   60,   -1,  306,
   -1,  264,  309,  310,  311,  312,  313,  314,  315,   -1,
  273,   -1,   91,   -1,  123,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   91,   -1,
   60,   -1,  295,   -1,  264,   -1,   -1,  300,  301,   -1,
   -1,   -1,   -1,  273,  123,   -1,  125,   -1,  278,   -1,
  264,  281,  282,  283,  284,  285,  286,  287,  288,  273,
  123,   91,   -1,   -1,   -1,   -1,  296,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,  264,   -1,   -1,
   -1,   -1,  296,   60,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,  123,  281,  282,  283,  284,  285,  286,
  287,  288,  264,  265,   -1,   -1,   -1,   -1,  295,   -1,
   -1,  273,  274,   -1,   91,   -1,   93,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,   60,   -1,   -1,
  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  274,  261,  262,   -1,   -1,   -1,  123,  281,  282,  283,
  284,  285,  286,  287,  288,  264,  265,   -1,   91,   -1,
   -1,   -1,   -1,   -1,  273,  274,   60,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  273,   -1,   -1,   91,   -1,   -1,
   -1,  264,  281,  282,  283,  284,  285,  286,  287,  288,
  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,   -1,
  380,  381,  382,  383,   -1,   -1,   -1,   -1,   -1,  389,
  390,  391,  392,  393,  394,  395,  396,  397,  398,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,  378,  379,  380,  381,  382,  383,  384,  385,
  386,  387,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  273,  274,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  316,  317,  318,  281,  282,  283,
  284,  285,  286,  287,  288,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
  274,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,  274,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,  274,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,  274,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
  274,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,   -1,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
   -1,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,   -1,   -1,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  399,  400,  401,  402,
  };

#line 961 "Repil/IR/IR.jay"

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
  public const int HEX_INTEGER = 258;
  public const int FLOAT_LITERAL = 259;
  public const int STRING = 260;
  public const int TRUE = 261;
  public const int FALSE = 262;
  public const int UNDEF = 263;
  public const int VOID = 264;
  public const int NULL = 265;
  public const int LABEL = 266;
  public const int X = 267;
  public const int SOURCE_FILENAME = 268;
  public const int TARGET = 269;
  public const int DATALAYOUT = 270;
  public const int TRIPLE = 271;
  public const int GLOBAL_SYMBOL = 272;
  public const int LOCAL_SYMBOL = 273;
  public const int META_SYMBOL = 274;
  public const int META_SYMBOL_DEF = 275;
  public const int SYMBOL = 276;
  public const int DISTINCT = 277;
  public const int METADATA = 278;
  public const int CONSTANT_BYTES = 279;
  public const int TYPE = 280;
  public const int HALF = 281;
  public const int FLOAT = 282;
  public const int DOUBLE = 283;
  public const int I1 = 284;
  public const int I8 = 285;
  public const int I16 = 286;
  public const int I32 = 287;
  public const int I64 = 288;
  public const int ZEROINITIALIZER = 289;
  public const int OPAQUE = 290;
  public const int DEFINE = 291;
  public const int DECLARE = 292;
  public const int UNNAMED_ADDR = 293;
  public const int LOCAL_UNNAMED_ADDR = 294;
  public const int NOALIAS = 295;
  public const int ELLIPSIS = 296;
  public const int GLOBAL = 297;
  public const int CONSTANT = 298;
  public const int PRIVATE = 299;
  public const int INTERNAL = 300;
  public const int EXTERNAL = 301;
  public const int FASTCC = 302;
  public const int NONNULL = 303;
  public const int NOCAPTURE = 304;
  public const int WRITEONLY = 305;
  public const int READONLY = 306;
  public const int ATTRIBUTE_GROUP_REF = 307;
  public const int ATTRIBUTES = 308;
  public const int NORECURSE = 309;
  public const int NOUNWIND = 310;
  public const int READNONE = 311;
  public const int SPECULATABLE = 312;
  public const int SSP = 313;
  public const int UWTABLE = 314;
  public const int ARGMEMONLY = 315;
  public const int RET = 316;
  public const int BR = 317;
  public const int SWITCH = 318;
  public const int INDIRECTBR = 319;
  public const int INVOKE = 320;
  public const int RESUME = 321;
  public const int CATCHSWITCH = 322;
  public const int CATCHRET = 323;
  public const int CLEANUPRET = 324;
  public const int UNREACHABLE = 325;
  public const int FNEG = 326;
  public const int ADD = 327;
  public const int NUW = 328;
  public const int NSW = 329;
  public const int FADD = 330;
  public const int SUB = 331;
  public const int FSUB = 332;
  public const int MUL = 333;
  public const int FMUL = 334;
  public const int UDIV = 335;
  public const int SDIV = 336;
  public const int FDIV = 337;
  public const int UREM = 338;
  public const int SREM = 339;
  public const int FREM = 340;
  public const int SHL = 341;
  public const int LSHR = 342;
  public const int EXACT = 343;
  public const int ASHR = 344;
  public const int AND = 345;
  public const int OR = 346;
  public const int XOR = 347;
  public const int EXTRACTELEMENT = 348;
  public const int INSERTELEMENT = 349;
  public const int SHUFFLEVECTOR = 350;
  public const int EXTRACTVALUE = 351;
  public const int INSERTVALUE = 352;
  public const int ALLOCA = 353;
  public const int LOAD = 354;
  public const int STORE = 355;
  public const int FENCE = 356;
  public const int CMPXCHG = 357;
  public const int ATOMICRMW = 358;
  public const int GETELEMENTPTR = 359;
  public const int ALIGN = 360;
  public const int INBOUNDS = 361;
  public const int INRANGE = 362;
  public const int TRUNC = 363;
  public const int ZEXT = 364;
  public const int SEXT = 365;
  public const int FPTRUNC = 366;
  public const int FPEXT = 367;
  public const int TO = 368;
  public const int FPTOUI = 369;
  public const int FPTOSI = 370;
  public const int UITOFP = 371;
  public const int SITOFP = 372;
  public const int PTRTOINT = 373;
  public const int INTTOPTR = 374;
  public const int BITCAST = 375;
  public const int ADDRSPACECAST = 376;
  public const int ICMP = 377;
  public const int EQ = 378;
  public const int NE = 379;
  public const int UGT = 380;
  public const int UGE = 381;
  public const int ULT = 382;
  public const int ULE = 383;
  public const int SGT = 384;
  public const int SGE = 385;
  public const int SLT = 386;
  public const int SLE = 387;
  public const int FCMP = 388;
  public const int OEQ = 389;
  public const int OGT = 390;
  public const int OGE = 391;
  public const int OLT = 392;
  public const int OLE = 393;
  public const int ONE = 394;
  public const int ORD = 395;
  public const int UEQ = 396;
  public const int UNE = 397;
  public const int UNO = 398;
  public const int PHI = 399;
  public const int SELECT = 400;
  public const int CALL = 401;
  public const int TAIL = 402;
  public const int VA_ARG = 403;
  public const int LANDINGPAD = 404;
  public const int CATCHPAD = 405;
  public const int CLEANUPPAD = 406;
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
