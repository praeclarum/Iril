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
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value",
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
//t    "type : INTEGER_TYPE",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : X86_FP80",
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
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage parameter_attribute return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention parameter_attribute return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters attribute_group_refs",
//t    "function_declaration : DECLARE NOALIAS return_type GLOBAL_SYMBOL parameters attribute_group_refs",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs",
//t    "parameters : '(' parameter_list ')'",
//t    "parameters : '(' ')'",
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
//t    "parameter_attribute : READNONE",
//t    "parameter_attribute : SIGNEXT",
//t    "parameter_attribute : ZEROEXT",
//t    "parameter_attribute : RETURNED",
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
//t    "value : PTRTOINT '(' typed_value TO type ')'",
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
//t    "function_args : '(' function_arg_list ')'",
//t    "function_args : '(' ')'",
//t    "function_arg_list : function_arg",
//t    "function_arg_list : function_arg_list ',' function_arg",
//t    "function_arg : type value",
//t    "function_arg : type parameter_attributes value",
//t    "function_arg : METADATA type LOCAL_SYMBOL",
//t    "function_arg : METADATA type metadata_value",
//t    "function_arg : METADATA META_SYMBOL",
//t    "function_arg : METADATA META_SYMBOL '(' ')'",
//t    "function_arg : METADATA META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_value : constant",
//t    "metadata_value : GLOBAL_SYMBOL",
//t    "metadata_value : INTTOPTR '(' typed_value TO type ')'",
//t    "metadata_value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "metadata_value : BITCAST '(' typed_value TO type ')'",
//t    "metadata_value : PTRTOINT '(' typed_value TO type ')'",
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
//t    "atomic_constraint : SEQ_CST",
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR INTEGER_TYPE value ',' label_value ',' label_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "instruction : ADD type value ',' value",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
//t    "instruction : ASHR type value ',' value",
//t    "instruction : ASHR EXACT type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL return_type function_pointer function_args",
//t    "instruction : CALL calling_convention return_type function_pointer function_args",
//t    "instruction : CALL calling_convention parameter_attribute return_type function_pointer function_args",
//t    "instruction : CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention parameter_attribute return_type function_pointer function_args",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FCMP fcmp_condition type value ',' value",
//t    "instruction : FDIV type value ',' value",
//t    "instruction : FENCE atomic_constraint",
//t    "instruction : FMUL type value ',' value",
//t    "instruction : FPEXT typed_value TO type",
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
//t    "instruction : FPTRUNC typed_value TO type",
//t    "instruction : FSUB type value ',' value",
//t    "instruction : GETELEMENTPTR type ',' typed_value ',' element_indices",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : INSERTELEMENT typed_value ',' typed_value ',' typed_value",
//t    "instruction : INTTOPTR typed_value TO type",
//t    "instruction : LOAD type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LOAD VOLATILE type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LSHR type value ',' value",
//t    "instruction : LSHR EXACT type value ',' value",
//t    "instruction : OR type value ',' value",
//t    "instruction : MUL type value ',' value",
//t    "instruction : MUL wrappings type value ',' value",
//t    "instruction : PHI type phi_vals",
//t    "instruction : PTRTOINT typed_value TO type",
//t    "instruction : SDIV type value ',' value",
//t    "instruction : SDIV EXACT type value ',' value",
//t    "instruction : SELECT type value ',' typed_value ',' typed_value",
//t    "instruction : SEXT typed_value TO type",
//t    "instruction : SHL type value ',' value",
//t    "instruction : SHL wrappings type value ',' value",
//t    "instruction : SHUFFLEVECTOR typed_value ',' typed_value ',' typed_value",
//t    "instruction : SITOFP typed_value TO type",
//t    "instruction : SREM type value ',' value",
//t    "instruction : STORE typed_value ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : STORE VOLATILE typed_value ',' typed_pointer_value ',' ALIGN INTEGER",
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
    "TYPE","HALF","FLOAT","DOUBLE","X86_FP80","INTEGER_TYPE",
    "ZEROINITIALIZER","OPAQUE","DEFINE","DECLARE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS","GLOBAL","CONSTANT",
    "PRIVATE","INTERNAL","EXTERNAL","FASTCC","SIGNEXT","ZEROEXT",
    "VOLATILE","RETURNED","NONNULL","NOCAPTURE","WRITEONLY","READONLY",
    "READNONE","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND",
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST","RET","BR",
    "SWITCH","INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET",
    "CLEANUPRET","UNREACHABLE","FNEG","ADD","NUW","NSW","FADD","SUB",
    "FSUB","MUL","FMUL","UDIV","SDIV","FDIV","UREM","SREM","FREM","SHL",
    "LSHR","EXACT","ASHR","AND","OR","XOR","EXTRACTELEMENT",
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
#line 60 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 64 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 68 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 72 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 76 "Repil/IR/IR.jay"
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
#line 96 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 100 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 109 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 121 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 125 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 129 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 133 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 137 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 141 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 145 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 149 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 153 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 27:
#line 154 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 28:
#line 158 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 159 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 30:
#line 163 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 31:
  case_31();
  break;
case 32:
  case_32();
  break;
case 33:
#line 180 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 34:
#line 181 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 182 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 183 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 184 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 188 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 39:
#line 192 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 40:
#line 199 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 203 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 42:
#line 210 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 43:
#line 214 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 44:
#line 218 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 45:
#line 222 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 46:
#line 226 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 64:
#line 259 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 65:
#line 263 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 66:
#line 267 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 67:
#line 274 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 278 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 283 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 73:
#line 289 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 74:
#line 290 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 75:
#line 291 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 76:
#line 292 "Repil/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 77:
#line 296 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 78:
#line 300 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 79:
#line 304 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 80:
#line 308 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 312 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 316 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 323 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 327 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 335 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 87:
#line 342 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 88:
#line 346 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 89:
#line 350 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 90:
#line 354 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 358 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 362 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 366 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 370 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 95:
#line 374 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 96:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 98:
#line 386 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 99:
#line 393 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 397 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 401 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 102:
#line 405 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 103:
#line 406 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 104:
#line 413 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 417 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 424 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 428 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 108:
#line 432 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 109:
#line 436 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 111:
#line 444 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 448 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 113:
#line 449 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 114:
#line 450 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 115:
#line 451 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 116:
#line 452 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 117:
#line 453 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 118:
#line 454 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 119:
#line 455 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 125:
#line 473 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 126:
#line 474 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 127:
#line 475 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 128:
#line 476 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 129:
#line 477 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 130:
#line 478 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 131:
#line 479 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 132:
#line 480 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 133:
#line 481 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 134:
#line 482 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 135:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 136:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 137:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 138:
#line 489 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 139:
#line 490 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 140:
#line 491 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 141:
#line 492 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 142:
#line 493 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 143:
#line 494 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 144:
#line 495 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 145:
#line 496 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 146:
#line 497 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 147:
#line 498 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 148:
#line 499 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 149:
#line 500 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 150:
#line 501 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 152:
#line 506 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 153:
#line 507 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 154:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 527 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 531 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 160:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 162:
#line 543 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 163:
#line 544 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 164:
#line 545 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 165:
#line 546 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 166:
#line 547 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 167:
#line 548 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 168:
#line 549 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 169:
#line 550 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 170:
#line 551 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 171:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 565 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 174:
#line 576 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 587 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 594 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 605 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 609 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 616 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 620 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 185:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 646 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 650 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 657 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 661 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 191:
#line 665 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 669 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 194:
#line 677 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 195:
#line 678 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 196:
#line 685 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 689 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 696 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 199:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 200:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 201:
#line 708 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 202:
#line 712 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 203:
#line 716 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 204:
#line 720 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 206:
#line 725 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 207:
#line 729 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 737 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 741 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 215:
#line 758 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 762 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 775 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 779 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 786 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 804 "Repil/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 227:
#line 811 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 815 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 819 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 823 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 830 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 834 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 838 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 842 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 846 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 236:
#line 850 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 237:
#line 854 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 858 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 239:
#line 862 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 240:
#line 866 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 241:
#line 870 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 242:
#line 874 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 243:
#line 878 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 244:
#line 882 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 245:
#line 886 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 246:
#line 890 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 247:
#line 894 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 248:
#line 898 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 249:
#line 902 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 906 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 910 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 914 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 918 "Repil/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 922 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 926 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 930 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 934 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 938 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 942 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 946 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 950 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 954 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 958 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 962 "Repil/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 966 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 266:
#line 970 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 267:
#line 974 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 268:
#line 978 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 269:
#line 982 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 986 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 990 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 994 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 998 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 1002 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1006 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1010 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 1014 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 1018 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1022 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1026 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1030 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1034 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1038 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 284:
#line 1042 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 285:
#line 1046 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1050 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1054 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1058 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1062 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1066 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1070 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1074 "Repil/IR/IR.jay"
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
#line 78 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 83 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 88 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 102 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 111 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_31()
#line 168 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_32()
#line 173 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,   10,   10,   16,   16,   15,
    9,    9,   17,   17,   17,   17,   17,   17,   17,   13,
   13,    8,    8,    8,    8,    8,   20,   20,   20,    7,
    7,   22,   22,   22,   22,   22,   22,   22,   22,   22,
   22,   22,   22,    3,    3,    3,   23,   23,   24,   24,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   25,   25,   26,   26,    4,    4,    4,    4,
    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,
    5,   27,   27,   32,   32,   33,   33,   33,   33,   34,
   34,   30,   30,   30,   30,   30,   30,   30,   30,   14,
   14,   28,   28,   35,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   36,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   40,   18,   18,   18,   18,   18,   18,   18,   18,   18,
   41,   21,   21,   42,   39,   39,   43,   44,   38,   38,
   29,   29,   45,   45,   45,   45,   46,   46,   48,   48,
   48,   48,   50,   51,   51,   52,   52,   53,   53,   53,
   53,   53,   53,   53,   54,   54,   54,   54,   54,   54,
   19,   19,   55,   55,   56,   56,   57,   58,   58,   59,
   60,   60,   61,   61,   31,   62,   47,   47,   47,   47,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    3,    3,    3,    6,    5,    2,
    3,    1,    2,    3,    3,    3,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    3,    1,
    1,    1,    4,    2,    3,    5,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    4,    2,    1,
    5,    5,    1,    3,    1,    1,    9,    9,   10,   10,
   11,    9,   10,   11,   11,   11,   13,   12,    5,    6,
    6,    3,    2,    1,    3,    1,    2,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    6,    9,    6,    6,    3,    3,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    2,    1,    2,    1,    3,    2,    1,    1,    3,
    1,    2,    2,    3,    1,    2,    1,    2,    1,    2,
    3,    4,    1,    3,    2,    1,    3,    2,    3,    3,
    3,    2,    4,    5,    1,    1,    6,    9,    6,    6,
    1,    3,    1,    1,    1,    3,    5,    1,    2,    3,
    1,    2,    1,    1,    1,    1,    2,    7,    2,    7,
    5,    6,    5,    5,    5,    6,    4,    4,    5,    6,
    5,    6,    6,    6,    7,    5,    6,    7,    4,    5,
    6,    5,    2,    5,    4,    4,    4,    4,    5,    6,
    7,    6,    6,    4,    7,    8,    5,    6,    5,    5,
    6,    3,    4,    5,    6,    7,    4,    5,    6,    6,
    4,    5,    7,    8,    5,    6,    4,    5,    4,    5,
    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   80,   73,   74,   75,   76,   72,    0,   29,   28,
    0,    0,    0,   71,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  120,  121,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   64,    0,
    0,    0,    0,    0,    0,   79,  225,  117,  118,  119,
  112,  113,  115,  114,  116,    0,    0,    0,    0,    0,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   65,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   86,
   77,    0,    0,   83,    0,    0,    0,  164,  165,  163,
  166,  167,  168,  162,  153,  152,  170,  169,    0,    0,
    0,    0,    0,    0,    0,    0,  151,    0,    0,    0,
    0,    0,    0,    0,   31,    0,    0,    0,   49,   48,
   13,    0,    0,   42,   47,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  108,  109,  103,    0,    0,  104,
  124,    0,    0,  122,   78,    0,    0,    0,    0,    0,
    0,   62,   54,   52,   53,   55,   56,   57,   58,    0,
   50,    0,    0,    0,    0,  175,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   43,   14,    0,  172,    0,   81,   66,   82,    0,    0,
    0,    0,    0,  110,    0,  102,    0,    0,    0,    0,
  123,   84,    0,    0,    0,    0,   12,   51,    0,    0,
    0,    0,  160,    0,  158,  159,    0,    0,    0,    0,
    0,    0,   35,    0,   33,   36,   37,   32,   17,   16,
   46,   45,   44,    0,    0,    0,    0,    0,    0,    0,
  111,  105,    0,    0,   40,    0,    0,   59,  214,  213,
    0,  211,    0,    0,    0,    0,  176,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  181,    0,    0,  187,    0,
    0,    0,    0,    0,    0,    0,   41,    0,   63,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   39,    0,    0,    0,    0,    0,  229,    0,    0,  227,
    0,  223,  224,    0,    0,  221,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  226,  253,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  125,  126,  127,  128,  129,  130,  131,  132,
  133,  134,    0,  135,  136,  147,  148,  149,  150,  138,
  140,  141,  142,  143,  139,  137,  145,  146,  144,    0,
    0,    0,    0,    0,    0,    0,   92,  182,    0,  188,
    0,    0,    0,    0,    0,    0,    0,   87,    0,   88,
  212,    0,  157,  154,  156,    0,    0,    0,    0,   38,
   90,    0,    0,    0,  171,    0,    0,    0,    0,  222,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  215,    0,
  193,    0,    0,    0,    0,    0,    0,    0,    0,   93,
    0,    0,    0,    0,   89,    0,    0,    0,   91,   95,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  249,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   94,   96,    0,    0,  178,    0,
  179,    0,    0,  231,    0,  250,  285,    0,  259,  270,
    0,  254,  288,    0,  274,  252,  290,  282,  278,    0,
    0,  267,    0,  235,  234,  269,  291,    0,    0,  233,
    0,  161,  174,    0,    0,    0,    0,    0,    0,    0,
    0,  216,    0,    0,  195,    0,    0,  196,    0,    0,
  239,    0,    0,    0,    0,    0,   98,    0,  155,    0,
    0,    0,    0,    0,  218,  232,  286,  271,  275,  279,
  268,  236,  263,  280,    0,    0,    0,    0,    0,    0,
  262,  251,    0,    0,    0,    0,  198,    0,  194,    0,
    0,  240,    0,    0,  247,    0,   97,  180,  228,  177,
    0,  230,  219,    0,  265,    0,  283,    0,  217,  276,
    0,  206,  200,    0,    0,    0,    0,  205,  201,  199,
  197,    0,  248,  220,  266,  284,  203,    0,    0,    0,
    0,    0,  204,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  210,  207,  209,    0,
    0,  208,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  190,  152,  144,   50,
  153,  551,  230,   51,   52,   36,  145,  137,  281,  154,
  639,  191,   61,   62,  113,  114,  109,  173,  345,  224,
   78,  169,  170,  225,  174,  443,  460,  640,  197,  673,
  380,  604,  703,  641,  346,  347,  348,  349,  350,  552,
  627,  687,  688,  759,  282,  548,  549,  704,  705,  385,
  386,  418,
  };
  protected static readonly short [] yySindex = {         3803,
  -41, -185,  -11,   15,   47, 3670, 3740, -229,    0, 3803,
    0,    0,    0,    0, -157,   61,   66,  221, -137,  -24,
    0,    0,    0,    0,    0,    0,    0, 3856,    0,    0,
 3815, -111, -108,    0,  115, 3551,  -35, 3856,  -33,  100,
    0,    0,  -65,  -51,    0,    0,    0,    0,    0, 3856,
 -120,  -75,  -60,  -46,  125,  -25,  153,  -21,    0,  115,
    4,  215,   -5, 3856,   28,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -16, 3856, 3589,  241, 3525,
  -13,  241,  163,    0,    0, 1897, 3856, -120, 3856, -120,
    0,  198,    0, -227,  287,  208, 3753,  241,    0, 3856,
 3856,   19, 3856,  241,    2,   11, 3856, 3473, -202,    0,
    0,  115,   73,    0,  241, -202,  447,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   10,  299,
  337,  341, 3905, 3905, 3905,  340,    0, 1897, 3856, 1897,
 3856,  335,  339,   75,    0, -227, 3778,    0,    0,    0,
    0,  -38, 1897,    0,    0,  -75,  115,   42,  353,   13,
 -202,  241,  241,   12,    0,    0,    0,  -40,  132,    0,
    0,   96, -245,    0,    0, 3705,   96,   96,   96,  357,
  381,    0,    0,    0,    0,    0,    0,    0,    0, 3868,
    0,  382, 3905, 3905, 3905,    0,   23,   17,    3,   63,
  385, 1897,  388, 1878, 5265,  159,    0, -227,  176,  -34,
    0,    0, 3800,    0,   96,    0,    0,    0,   96, -112,
   96,  -75,  241,    0,  600,    0, 3699, -110,  171, -102,
    0,    0,   96,   96,  166, 3515,    0,    0, 3856,   79,
   80,   81,    0, 3905,    0,    0,  195,   93,  418,  104,
  105,  424,    0,  430,    0,    0,    0,    0,    0,    0,
    0,    0,    0, -105, -245, 4735,  -95, -245,   96,  -75,
    0,    0, 4735,  -92,    0,  197, 4735,    0,    0,    0,
  188,    0,   31, 3856, 3856, 3856,    0,  200,  223,  124,
  230,  232,  133,  177, 4735,  -90,  -89,  433, 3905, -220,
 3905, 3433, 3856, 3433, 3856, 3433, 3856, 3856,   65, 3856,
 3856, 3856, 3433,  291, 1967, 3856, 3856, 3856, 3905, 3905,
 3905, 3856,  506, 2024,  182,    9, 3905, 3905, 3905, 3905,
 3905, 3905, 3905, 3905, 3905, 3905, 3905, 3905, 2082, 3828,
 3856, 3856, 3551,   97, 1984,    0, 4735,  200,    0,  200,
 4735,  -88,  -97,   96, 2071, 4735,    0, 2158,    0, 3515,
 3905,  161,  272,  303,  227,  200,  245,  200,    0,  246,
    0,  213, 2245, 4735, 4735, 5131,    0,  231, 2003,    0,
  463,    0,    0, 1897, 3433,    0, 1897, 1897, 3433, 1897,
 1897, 3433, 1897, 1897, 3856, 1897, 1897, 1897, 1897, 1897,
 3433, 3856, 1897, 3856, 1897, 1897, 1897, 1897,  465,  466,
  477,   48, 3856,  139, 3905,  479,    0,    0, 3856,  332,
  135,  156,  170,  180,  183,  191,  192,  201,  209,  210,
  212,  216,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3856,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3856,
   14, 1897, 1639, 3856, 3589, 3551,    0,    0,  200,    0,
  227,  227, 2332, 4735, 4735,  -87, -245,    0, 2419,    0,
    0,  481,    0,    0,    0,  227,  200,  227,  200,    0,
    0, 2506, 2593,  200,    0,  497,  280,  523, 1897,    0,
  524,  534, 1897,  539,  541, 1897,  548,  549, 1897,  551,
  558,  560,  561,  563, 1897, 1897,  566, 1897,  569,  571,
  572,  574, 3905, 3905, 3905,  236,  336, 3856,  575, 3856,
  343, 3905, 3856, 3856, 3856, 3856, 3856, 3856, 3856, 3856,
 3856, 3856, 3856, 3856, 1897, 1897, 2003,  577,    0,  579,
    0,  584, 1639, 1639, 3856, 1639, 3856, 3589,  227,    0,
 2680, 2767, 4735,  -83,    0, 3905,  227,  227,    0,    0,
  227,  280,  535, 2003,  581, 2003, 2003,  583, 2003, 2003,
  589, 2003, 2003,  590, 2003, 2003, 2003, 2003, 2003,  591,
  594, 2003,  597, 2003, 2003, 2003, 2003,    0,  598,  601,
  386, 3856, 1897,  602, 3856,  603, 3905,  604,  115,  115,
  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,
  605,  607,  608,  562, 3905, 3648,   96,  584,  584, 1639,
  584, 1639, 1639, 3856,    0,    0, 2854, 4735,    0,  244,
    0,  610, 3856,    0, 2003,    0,    0, 2003,    0,    0,
 2003,    0,    0, 2003,    0,    0,    0,    0,    0, 2003,
 2003,    0, 2003,    0,    0,    0,    0, 3905, 3905,    0,
  612,    0,    0,  295,  616,  301,  631, 3905, 2003, 2003,
 2003,    0,  632, 3850,    0, 1824,  274,    0,   96,   96,
    0,  584,   96,  584,  584, 1639,    0, 2941,    0, 3905,
  280, 3464,  640, 3822,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  325,  434,  331,  437, 3905,  651,
    0,    0,  606, 3905,  656, 1669,    0, 1916,    0, 3879,
   96,    0,   96,   96,    0,  584,    0,    0,    0,    0,
  280,    0,    0,  440,    0,  443,    0,  651,    0,    0,
 1949,    0,    0,  345,  663,  664,  670,    0,    0,    0,
    0,   96,    0,    0,    0,    0,    0,  278,  672, 3905,
 3905, 3905,    0, 3856,  344,  347,  348,  346, 3856, 3856,
 3856, 3905,  350,  360,  367,  671,    0,    0,    0, 3905,
  282,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  716,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 1701,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    1,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  680,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  110,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  680,    0,  680,
    0,    0,    0,    0,    0,    0,    0,  519,    0,    0,
    0,    0,  680,    0,    0,    0,   22,  680,    0,  680,
    0,    0,    0,    0,    0,    0,    0,  152,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  131, 3889,
 3931,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  680,    0,  680,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  284,    0,    0,    0,    0,    0,
    0,    0,  155,  417,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  508,    0,
    0,    0,    0,  314,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  680,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3028,    0, 4815,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  680,  680,  680,  554,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  680,    0,    0,  680,  680,    0,  680,
  680,    0,  680,  680,    0,  680,  680,  680,  680,  680,
    0,    0,  680,    0,  680,  680,  680,  680,    0,    0,
    0,  680,    0,  680,    0,    0,    0,    0,    0,  680,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  680,  680,    0,    0,    0,    0,    0,    0, 3115,    0,
 3202, 4895,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 1599, 1724, 1763,    0,    0,
    0,    0,    0, 4975,    0,    0,    0,    0,  680,    0,
    0,    0,  680,    0,    0,  680,    0,    0,  680,    0,
    0,    0,    0,    0,  680,  680,    0,  680,    0,    0,
    0,    0,    0,    0,    0,    0,  680,    0,    0,    0,
  680,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  680,  680,    0, 3935,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3289,    0,
    0,    0,    0,    0,    0,    0, 1781, 1832,    0,    0,
 5055,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  680,    0,    0,    0,    0,    0,  599,  679,
  759,  839,  919,  999, 1079, 1159, 1239, 1319, 1399, 1479,
    0,    0,    0,    0,    0,    0, 4015,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  680,    0,    0, 4095,    0,
    0,    0, 4175,    0,    0,    0,    0,    0,    0,    0,
    0,  680,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4255,
    0,    0,    0,    0,  315,  680,    0,    0,    0,    0,
 4335,    0, 4415, 4495,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4575,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4655,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  680,    0,    0,
    0,    0,  680,  680,  680,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  711,  668,    0,    0,    0,    0,  578,  580,   70,
   -6,   85, 3431,  461,    0,  710,  521, -182, -290,    0,
   33,  542,  673,   32,    0,  559,  107,  -79, -132,  -19,
 -328,    0,  507,   52,  -20,    0,    0, -662,  277,    0,
 -494, -487,    0,   44, -221,    0,  400,  402,  374,  187,
  -86,    0,   21,    0,  392,    0,  129,    0,   68, -193,
 -191,    0,
  };
  protected static readonly short [] yyTable = {            35,
   35,   66,  573,  372,   80,  213,   80,   96,   57,  213,
  266,   64,  273,   92,  465,  720,   77,  295,   80,   15,
  277,   35,  256,   80,   60,  475,   80,  351,  229,   35,
  356,   35,  374,  375,  474,  563,  179,   37,   39,  638,
   69,   80,  606,   86,   67,  378,  244,  100,  142,   18,
   80,   80,  143,  280,   66,   66,  748,   60,  107,   58,
  244,   69,  100,  171,  379,   68,  244,   76,   32,   81,
   35,   35,   66,  112,  361,   19,   31,  642,  245,   40,
  138,  220,  140,   66,   16,   17,  212,   45,   46,   66,
  260,  526,  228,  157,  158,  246,  160,  233,  234,   33,
   35,  168,   42,  216,  547,  218,  171,   20,  105,  106,
  389,  280,  392,  175,  671,  207,  176,  675,  208,  401,
   87,   43,   89,  468,   32,   67,   44,  791,   99,  155,
   99,   31,  202,  468,  204,  264,  468,  558,  164,  265,
  355,  268,   54,  159,  358,   63,   68,  243,   65,   69,
   85,  468,  231,   85,  100,   33,   66,  139,  231,  141,
   83,  229,  373,  229,   94,  196,  196,  196,  229,  112,
  136,  276,  226,   47,   48,  227,  229,  280,  276,  155,
   66,  276,  528,  276,  276,  276,  276,   31,  116,  353,
  276,   69,  106,  500,   84,  106,  171,  500,  171,  231,
  500,  483,   66,  171,  156,  271,  739,  231,   85,  500,
  161,  171,  231,  231,   45,   46,  259,  371,  473,  208,
  168,  177,  201,  479,  203,  240,  241,  242,  359,   45,
   46,  360,  283,   47,   48,  211,   79,  214,   82,  211,
   91,  492,  493,  231,  231,  263,  764,  231,   95,   55,
   98,  468,   56,  490,   80,  104,  360,  468,  115,   68,
   69,  101,   70,   71,   72,   73,   74,   75,  221,  222,
  468,  468,   21,  162,  477,   97,  287,  362,  363,  364,
  108,   22,  163,  223,  699,  117,  249,  700,  252,   23,
   24,   25,   26,   27,  103,  384,  387,  388,  390,  391,
  393,  394,  396,  397,  398,  399,  400,  403,  405,  406,
  407,  408,  484,   66,  729,  412,  414,  730,  773,  420,
   64,  360,  792,  464,  107,  700,  146,  107,   21,  270,
  147,  377,  231,  381,  461,  462,   35,   22,  193,  468,
  468,  561,  562,  485,   66,   23,   24,   25,   26,   27,
   32,  409,  410,  411,   34,  202,  416,   34,  202,  421,
  422,  423,  424,  425,  426,  427,  428,  429,  430,  431,
  432,  419,  192,   66,  463,  532,  194,   66,  499,  602,
  195,   33,  503,  200,   66,  506,  607,   66,  509,  782,
  787,   66,  205,  482,  515,  516,  206,  518,   99,   99,
  788,   66,   99,   99,  171,   99,  527,  789,   66,  395,
  198,  199,  531,   31,  217,  468,  101,  235,   99,   99,
  236,  239,  100,  100,  247,  278,  100,  100,  248,  100,
  637,  250,  257,  118,  119,  120,  545,  121,  122,  123,
   99,  124,  100,  100,  275,  555,  557,  529,  284,  285,
  286,  288,  279,  546,  289,  127,  231,   35,   35,   35,
  768,  290,  128,  496,  100,  291,  292,  293,  498,  294,
  357,  501,  502,  229,  504,  505,  468,  507,  508,  366,
  510,  511,  512,  513,  514,  367,  368,  517,  369,  519,
  520,  521,  522,  376,  370,  553,  554,  556,  417,  466,
  276,  487,  489,  495,  533,  698,  497,   21,  523,  524,
   45,   46,   88,   90,   47,   48,   49,   29,   30,  740,
  525,  603,  530,  603,  566,  534,  609,  610,  611,  612,
  613,  614,  615,  616,  617,  618,  619,  620,  634,  535,
  572,  690,  691,  758,  693,  378,  550,  689,   35,  536,
   35,   35,  537,   18,   21,  598,  599,  600,   70,  173,
  538,  539,  173,   22,  608,   32,  574,  576,  280,  172,
  540,   23,   24,   25,   26,   27,  178,  577,  541,  542,
  173,  543,  579,  575,  580,  544,  630,  578,  632,  633,
  581,  582,  583,  584,  585,  603,   33,  601,  603,  590,
  591,  586,  593,  587,  588,  732,  589,  734,  735,  592,
  731,  173,  594,  733,  595,  596,  215,  597,  605,  686,
  624,  219,  625,  626,  645,  643,  648,   35,   31,  621,
  622,  623,  651,  654,  660,  402,  702,  661,   69,  677,
  663,  668,  670,  173,  669,  674,  676,  678,  679,  763,
  680,  681,  547,  701,  762,  715,  716,  683,  644,  717,
  646,  647,  718,  649,  650,  696,  652,  653,  231,  655,
  656,  657,  658,  659,  719,  724,  662,  726,  664,  665,
  666,  667,  269,  741,  101,  101,  744,  672,  101,  101,
  745,  101,  746,  747,  700,  751,  765,  702,  749,  766,
  713,  714,  770,  771,  101,  101,  180,  769,  271,  772,
  231,  774,  231,  779,  790,    1,  780,  781,   69,   69,
   41,   93,  181,  686,  210,  209,  101,   53,  258,  706,
  354,  238,  707,  272,  232,  708,  102,  728,  709,  628,
  629,  231,  631,  738,  710,  711,  469,  712,  470,  494,
  761,  481,  682,  182,  183,    0,  750,  184,  185,  186,
  187,  188,  189,  721,  722,  723,    0,  778,    0,   21,
  727,  743,  783,  784,  785,   21,   21,    0,   22,   21,
   21,    0,   21,    0,    0,    0,   23,   24,   25,   26,
   27,  173,  173,    0,    0,   21,   21,    0,   69,    0,
    0,    0,  775,  776,  777,    0,    0,  413,    0,    0,
    0,    0,  760,    0,  786,    0,  692,   21,  694,  695,
    0,   18,   18,    0,    0,   18,   18,    0,   18,    0,
    0,    0,    0,    0,    0,    0,  173,  173,  173,    0,
    0,   18,   18,    0,    0,    0,    0,  173,    0,    0,
  173,  173,  173,  173,  173,  173,  173,  173,  173,  173,
    0,  173,  173,   18,  173,  173,  173,  173,  173,  173,
  173,  287,  287,  173,  173,  173,  173,    0,   69,  173,
    0,    0,  736,  173,  173,  173,  173,  173,  173,  173,
  173,  173,  173,  173,  173,  173,    0,  173,    0,   68,
   69,    0,   70,   71,   72,   73,   74,   75,  173,    0,
    0,    0,    0,    0,    0,    0,  287,  287,  287,  173,
  173,  173,  173,    0,    0,    0,    0,  287,    0,    0,
  287,  287,  287,  287,  287,  287,  287,  287,  287,  287,
    0,  287,  287,    0,  287,  287,  287,  287,  287,  287,
  287,  292,  292,  287,  287,  287,  287,    0,   69,  287,
    0,    0,    0,  287,  287,  287,  287,  287,    0,  287,
  287,  287,  287,  287,  287,  287,    0,  287,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  287,    0,
    0,    0,    0,    0,    0,    0,  292,  292,  292,  287,
  287,  287,  287,    0,    0,    0,    0,  292,    0,    0,
  292,  292,  292,  292,  292,  292,  292,  292,  292,  292,
    0,  292,  292,    0,  292,  292,  292,  292,  292,  292,
  292,  277,  277,  292,  292,  292,  292,    0,   69,  292,
    0,    0,    0,  292,  292,  292,  292,  292,    0,  292,
  292,  292,  292,  292,  292,  292,    0,  292,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  292,    0,
    0,    0,    0,    0,    0,    0,  277,  277,  277,  292,
  292,  292,  292,    0,    0,    0,    0,  277,    0,    0,
  277,  277,  277,  277,  277,  277,  277,  277,  277,  277,
    0,  277,  277,    0,  277,  277,  277,  277,  277,  277,
  277,  258,  258,  277,  277,  277,  277,    0,   69,  277,
    0,    0,    0,  277,  277,  277,  277,  277,    0,  277,
  277,  277,  277,  277,  277,  277,    0,  277,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  277,    0,
    0,    0,    0,    0,    0,    0,  258,  258,  258,  277,
  277,  277,  277,    0,    0,    0,    0,  258,    0,    0,
  258,  258,  258,  258,  258,  258,  258,  258,  258,  258,
    0,  258,  258,    0,  258,  258,  258,  258,  258,  258,
  258,  255,  255,  258,  258,  258,  258,    0,   69,  258,
    0,    0,    0,  258,  258,  258,  258,  258,    0,  258,
  258,  258,  258,  258,  258,  258,    0,  258,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  258,    0,
    0,    0,    0,    0,    0,    0,  255,  255,  255,  258,
  258,  258,  258,    0,    0,    0,    0,  255,    0,    0,
  255,  255,  255,  255,  255,  255,  255,  255,  255,  255,
    0,  255,  255,    0,  255,  255,  255,  255,  255,  255,
  255,  256,  256,  255,  255,  255,  255,    0,   69,  255,
    0,    0,    0,  255,  255,  255,  255,  255,    0,  255,
  255,  255,  255,  255,  255,  255,    0,  255,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  255,    0,
    0,    0,    0,    0,    0,    0,  256,  256,  256,  255,
  255,  255,  255,    0,    0,    0,    0,  256,    0,    0,
  256,  256,  256,  256,  256,  256,  256,  256,  256,  256,
    0,  256,  256,    0,  256,  256,  256,  256,  256,  256,
  256,  257,  257,  256,  256,  256,  256,    0,   69,  256,
    0,    0,    0,  256,  256,  256,  256,  256,    0,  256,
  256,  256,  256,  256,  256,  256,    0,  256,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  256,    0,
    0,    0,    0,    0,    0,    0,  257,  257,  257,  256,
  256,  256,  256,    0,    0,    0,    0,  257,    0,    0,
  257,  257,  257,  257,  257,  257,  257,  257,  257,  257,
    0,  257,  257,    0,  257,  257,  257,  257,  257,  257,
  257,  289,  289,  257,  257,  257,  257,    0,   69,  257,
    0,    0,    0,  257,  257,  257,  257,  257,    0,  257,
  257,  257,  257,  257,  257,  257,    0,  257,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  257,    0,
    0,    0,    0,    0,    0,    0,  289,  289,  289,  257,
  257,  257,  257,    0,    0,    0,    0,  289,    0,    0,
  289,  289,  289,  289,  289,  289,  289,  289,  289,  289,
    0,  289,  289,    0,  289,  289,  289,  289,  289,  289,
  289,  281,  281,  289,  289,  289,  289,    0,   69,  289,
    0,    0,    0,  289,  289,  289,  289,  289,    0,  289,
  289,  289,  289,  289,  289,  289,    0,  289,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  289,    0,
    0,    0,    0,    0,    0,    0,  281,  281,  281,  289,
  289,  289,  289,    0,    0,    0,    0,  281,    0,    0,
  281,  281,  281,  281,  281,  281,  281,  281,  281,  281,
    0,  281,  281,    0,  281,  281,  281,  281,  281,  281,
  281,  273,  273,  281,  281,  281,  281,    0,   19,  281,
    0,    0,    0,  281,  281,  281,  281,  281,    0,  281,
  281,  281,  281,  281,  281,  281,    0,  281,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  281,    0,
    0,    0,    0,    0,    0,    0,  273,  273,  273,  281,
  281,  281,  281,    0,    0,    0,    0,  273,    0,    0,
  273,  273,  273,  273,  273,  273,  273,  273,  273,  273,
    0,  273,  273,    0,  273,  273,  273,  273,  273,  273,
  273,  264,  264,  273,  273,  273,  273,    0,   80,  273,
    0,    0,    0,  273,  273,  273,  273,  273,    0,  273,
  273,  273,  273,  273,  273,  273,    0,  273,  134,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  273,    0,
   66,    0,    0,    0,    0,    0,  264,  264,  264,  273,
  273,  273,  273,   20,    0,    0,    0,  264,    0,  135,
  264,  264,  264,  264,  264,  264,  264,  264,  264,  264,
   69,  264,  264,    0,  264,  264,  264,  264,  264,  264,
  264,  237,  237,  264,  264,  264,  264,    0,    0,  264,
   69,  133,   25,  264,  264,  264,  264,  264,    0,  264,
  264,  264,  264,  264,  264,  264,    0,  264,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,  264,    0,
    0,   69,    0,    0,    0,    0,  237,  237,  237,  264,
  264,  264,  264,    0,    0,    0,    0,  237,    0,    0,
  237,  237,  237,  237,  237,  237,  237,  237,  237,  237,
    0,  237,  237,   69,  237,  237,  237,  237,  237,  237,
  237,   24,    0,  237,  237,  237,  237,    0,    0,  237,
    0,    0,    0,  237,  237,  237,  237,  237,    0,  237,
  237,  237,  237,  237,  237,  237,    0,  237,    0,    0,
    0,    0,    0,    0,    0,   66,   19,   19,  237,    0,
   19,   19,    0,   19,    0,    0,    0,    0,    0,  237,
  237,  237,  237,  134,    0,    0,   19,   19,    0,    0,
    0,    0,    0,    0,    0,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    0,    0,    0,    0,   19,    0,
  125,  126,    0,    0,  135,    0,    0,  127,    0,   66,
    0,  251,    0,    0,  128,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    0,    0,    0,  134,   66,    0,
  752,  753,    0,    0,    0,    0,  133,  127,    0,    0,
    0,    0,    0,    0,  128,    0,  134,   69,   69,   69,
    0,   69,   69,   69,    0,   69,    0,    0,  135,    0,
    0,    0,   69,   69,    0,  134,    0,    0,    0,   69,
    0,    0,    0,    0,    0,    0,   69,  135,    0,  767,
    0,   20,   20,    0,    0,   20,   20,    0,   20,  129,
  133,    0,    0,    0,    0,    0,  135,    0,    0,    0,
    0,   20,   20,  130,  131,  132,    0,    0,    0,  133,
    0,    0,    0,    0,    0,    0,   32,    0,    0,  754,
   25,   25,    0,   20,   25,   25,    0,   25,  133,    0,
    0,    0,    0,  755,  756,  757,    0,    0,   22,   22,
   25,   25,   22,   22,    0,   22,    0,   33,    0,    0,
    0,   69,  134,    0,    0,    0,    0,    0,   22,   22,
    0,    0,   25,    0,    0,   69,   69,   69,    0,    0,
  118,  119,  120,   32,  121,  122,  123,    0,  124,   31,
   22,    0,    0,  135,    0,  125,  126,    0,    0,   24,
   24,    0,  127,   24,   24,    0,   24,    0,  467,  128,
    0,    0,    0,    0,   33,    0,    0,    0,    0,   24,
   24,    0,    0,   68,   69,  133,   70,   71,   72,   73,
   74,   75,    0,    0,  118,  119,  120,    0,  121,  122,
  123,   24,  124,    0,    0,    0,   31,    0,    0,  125,
  126,    0,    0,  118,  119,  120,  127,  121,  122,  123,
    0,  124,    0,  128,    0,    0,    0,    0,  125,  126,
    0,    0,  118,  119,  120,  127,  121,  122,  123,    0,
  124,    0,  128,    0,  129,    0,    0,  125,  126,    0,
    0,    0,    0,    0,  127,  478,    0,    0,  130,  131,
  132,  128,    0,    0,    0,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    0,   68,   69,    0,   70,   71,
   72,   73,   74,   75,  279,    0,    0,  127,    0,    0,
   21,    0,    0,    0,  128,    0,    0,    0,  129,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,  130,  131,  132,    0,  298,  129,    0,  118,
  119,  120,    0,  121,  122,  123,    0,  124,    0,    0,
    0,  130,  131,  132,  125,  126,  129,    0,    0,    0,
    0,  127,  480,    0,    0,    0,    0,  148,  128,    0,
  130,  131,  132,    0,    0,    0,   22,    0,    0,    0,
    0,  299,  300,  301,   23,   24,   25,   26,   27,    0,
    0,  404,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  309,  310,  311,  312,  415,  313,  314,    0,  315,
  316,  317,  318,  319,  320,  321,    0,    0,  322,  323,
  324,  325,    0,  298,  326,    0,    0,    0,  327,  328,
  329,  330,  331,    0,  332,  333,  334,  335,  336,  337,
  338,    0,  339,  129,    0,    0,    0,    0,    0,  491,
    0,    0,    0,  340,    0,    0,    0,  130,  131,  132,
    0,    0,    0,    0,  341,  342,  343,  344,  299,  300,
  301,    0,    0,    0,    0,    0,    0,    0,    0,  302,
    0,    0,  303,  304,  305,  306,  307,  308,  309,  310,
  311,  312,    0,  313,  314,    0,  315,  316,  317,  318,
  319,  320,  321,    0,    0,  322,  323,  324,  325,    0,
  298,  326,    0,    0,    0,  327,  328,  329,  330,  331,
    0,  332,  333,  334,  335,  336,  337,  338,    0,  339,
    0,    0,    0,    0,    0,    0,  560,    0,    0,    0,
  340,  433,  434,  435,  436,  437,  438,  439,  440,  441,
  442,  341,  342,  343,  344,  299,  300,  301,    0,    0,
    0,    0,    0,    0,    0,    0,  302,    0,    0,  303,
  304,  305,  306,  307,  308,  309,  310,  311,  312,    0,
  313,  314,    0,  315,  316,  317,  318,  319,  320,  321,
    0,    0,  322,  323,  324,  325,    0,  298,  326,    0,
    0,    0,  327,  328,  329,  330,  331,    0,  332,  333,
  334,  335,  336,  337,  338,    0,  339,    0,    0,    0,
    0,    0,    0,  565,    0,    0,    0,  340,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  341,  342,
  343,  344,  299,  300,  301,    0,    0,    0,    0,    0,
    0,    0,    0,  302,    0,    0,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,    0,  313,  314,    0,
  315,  316,  317,  318,  319,  320,  321,    0,    0,  322,
  323,  324,  325,    0,  298,  326,    0,    0,    0,  327,
  328,  329,  330,  331,    0,  332,  333,  334,  335,  336,
  337,  338,    0,  339,    0,    0,    0,    0,    0,    0,
  569,    0,    0,    0,  340,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  341,  342,  343,  344,  299,
  300,  301,    0,    0,    0,    0,    0,    0,    0,    0,
  302,    0,    0,  303,  304,  305,  306,  307,  308,  309,
  310,  311,  312,    0,  313,  314,    0,  315,  316,  317,
  318,  319,  320,  321,    0,    0,  322,  323,  324,  325,
    0,  298,  326,    0,    0,    0,  327,  328,  329,  330,
  331,    0,  332,  333,  334,  335,  336,  337,  338,    0,
  339,    0,    0,    0,    0,    0,    0,  570,    0,    0,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  341,  342,  343,  344,  299,  300,  301,    0,
    0,    0,    0,    0,    0,    0,    0,  302,    0,    0,
  303,  304,  305,  306,  307,  308,  309,  310,  311,  312,
    0,  313,  314,    0,  315,  316,  317,  318,  319,  320,
  321,    0,    0,  322,  323,  324,  325,    0,  298,  326,
    0,    0,    0,  327,  328,  329,  330,  331,    0,  332,
  333,  334,  335,  336,  337,  338,    0,  339,    0,    0,
    0,    0,    0,    0,  635,    0,    0,    0,  340,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  341,
  342,  343,  344,  299,  300,  301,    0,    0,    0,    0,
    0,    0,    0,    0,  302,    0,    0,  303,  304,  305,
  306,  307,  308,  309,  310,  311,  312,    0,  313,  314,
    0,  315,  316,  317,  318,  319,  320,  321,    0,    0,
  322,  323,  324,  325,    0,  298,  326,    0,    0,    0,
  327,  328,  329,  330,  331,    0,  332,  333,  334,  335,
  336,  337,  338,    0,  339,    0,    0,    0,    0,    0,
    0,  636,    0,    0,    0,  340,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  341,  342,  343,  344,
  299,  300,  301,    0,    0,    0,    0,    0,    0,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,    0,    0,  322,  323,  324,
  325,    0,  298,  326,    0,    0,    0,  327,  328,  329,
  330,  331,    0,  332,  333,  334,  335,  336,  337,  338,
    0,  339,    0,    0,    0,    0,    0,    0,  697,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  341,  342,  343,  344,  299,  300,  301,
    0,    0,    0,    0,    0,    0,    0,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
  312,    0,  313,  314,    0,  315,  316,  317,  318,  319,
  320,  321,    0,    0,  322,  323,  324,  325,    0,  298,
  326,    0,    0,    0,  327,  328,  329,  330,  331,    0,
  332,  333,  334,  335,  336,  337,  338,    0,  339,    0,
    0,    0,    0,    0,    0,  737,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  341,  342,  343,  344,  299,  300,  301,    0,    0,    0,
    0,    0,    0,    0,    0,  302,    0,    0,  303,  304,
  305,  306,  307,  308,  309,  310,  311,  312,    0,  313,
  314,    0,  315,  316,  317,  318,  319,  320,  321,    0,
    0,  322,  323,  324,  325,    0,  298,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,  333,  334,
  335,  336,  337,  338,    0,  339,    0,    0,    0,    0,
    0,    0,  185,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  341,  342,  343,
  344,  299,  300,  301,    0,    0,    0,    0,    0,    0,
    0,    0,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  309,  310,  311,  312,    0,  313,  314,    0,  315,
  316,  317,  318,  319,  320,  321,    0,    0,  322,  323,
  324,  325,    0,  298,  326,    0,    0,    0,  327,  328,
  329,  330,  331,    0,  332,  333,  334,  335,  336,  337,
  338,    0,  339,    0,    0,    0,    0,    0,    0,  183,
    0,    0,    0,  340,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,  342,  343,  344,  299,  300,
  301,    0,    0,    0,    0,    0,    0,    0,    0,  302,
    0,    0,  303,  304,  305,  306,  307,  308,  309,  310,
  311,  312,    0,  313,  314,    0,  315,  316,  317,  318,
  319,  320,  321,    0,    0,  322,  323,  324,  325,    0,
  185,  326,    0,    0,    0,  327,  328,  329,  330,  331,
    0,  332,  333,  334,  335,  336,  337,  338,    0,  339,
    0,    0,    0,    0,    0,    0,  186,    0,    0,    0,
  340,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  341,  342,  343,  344,  185,  185,  185,    0,    0,
    0,    0,    0,    0,    0,    0,  185,    0,    0,  185,
  185,  185,  185,  185,  185,  185,  185,  185,  185,    0,
  185,  185,    0,  185,  185,  185,  185,  185,  185,  185,
    0,    0,  185,  185,  185,  185,    0,  183,  185,    0,
    0,    0,  185,  185,  185,  185,  185,    0,  185,  185,
  185,  185,  185,  185,  185,    0,  185,    0,    0,    0,
    0,    0,    0,  184,    0,    0,    0,  185,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  185,  185,
  185,  185,  183,  183,  183,    0,    0,    0,    0,    0,
    0,    0,    0,  183,    0,    0,  183,  183,  183,  183,
  183,  183,  183,  183,  183,  183,    0,  183,  183,    0,
  183,  183,  183,  183,  183,  183,  183,    0,    0,  183,
  183,  183,  183,    0,  186,  183,    0,    0,    0,  183,
  183,  183,  183,  183,    0,  183,  183,  183,  183,  183,
  183,  183,   32,  183,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  183,   66,    0,    0,    0,    0,
    0,    0,    0,  167,    0,  183,  183,  183,  183,  186,
  186,  186,    0,   33,    0,    0,    0,    0,    0,    0,
  186,    0,   32,  186,  186,  186,  186,  186,  186,  186,
  186,  186,  186,    0,  186,  186,    0,  186,  186,  186,
  186,  186,  186,  186,    0,   31,  186,  186,  186,  186,
    0,  184,  186,   33,    0,  111,  186,  186,  186,  186,
  186,    0,  186,  186,  186,  186,  186,  186,  186,    0,
  186,    0,    0,    0,   32,    0,    0,    0,    0,    0,
    0,  186,    0,    0,    0,   31,    0,    0,    0,    0,
    0,    0,  186,  186,  186,  186,  184,  184,  184,    0,
   32,    0,    0,    0,    0,   33,    0,  184,    0,    0,
  184,  184,  184,  184,  184,  184,  184,  184,  184,  184,
    0,  184,  184,    0,  184,  184,  184,  184,  184,  184,
  184,   33,    0,  184,  184,  184,  184,   31,   32,  184,
  267,    0,    0,  184,  184,  184,  184,  184,  274,  184,
  184,  184,  184,  184,  184,  184,    0,  184,    0,    0,
    0,    0,    0,   31,    0,    0,    0,    0,  184,   33,
    0,    0,    0,    0,    0,    0,    0,    0,  685,  184,
  184,  184,  184,    0,  296,  297,   21,    0,  352,    0,
    0,    0,    0,    0,    0,   22,    0,   32,    0,    0,
    0,   31,    0,   23,   24,   25,   26,   27,  365,    0,
  118,  119,  120,    0,  121,  122,  123,    0,  124,   32,
    0,    0,    0,    0,    0,    0,   21,    0,   33,    0,
    0,    0,  127,    0,    0,   22,    0,    0,    0,  128,
  165,    0,    0,   23,   24,   25,   26,   27,   32,    0,
   33,    0,  382,  383,   32,  166,    0,    0,    0,    0,
   31,  118,  119,  120,    0,  121,  122,  123,  471,  124,
  472,    0,    0,  476,    0,    0,    0,    0,   21,   33,
  279,    0,   31,  127,    0,   33,  486,   22,  488,   32,
  128,    0,    0,    0,    0,   23,   24,   25,   26,   27,
    0,    0,   32,    0,   21,    0,    0,  110,    0,    0,
    0,   31,    0,   22,    0,    0,    0,   31,    0,    0,
   33,   23,   24,   25,   26,   27,    0,   32,    0,    0,
    0,    0,    0,   33,    0,    0,    0,    0,    0,   67,
   68,   69,   21,   70,   71,   72,   73,   74,   75,   32,
    0,   22,   31,    0,    0,    0,    0,    0,   33,   23,
   24,   25,   26,   27,   32,   31,    0,  151,    0,    0,
    0,   32,    0,    0,    0,    0,    0,    0,   68,   69,
   33,   70,   71,   72,   73,   74,   75,    0,    0,  559,
   31,    0,    0,    0,    0,   33,    0,  564,    0,   32,
    0,   21,   33,    0,  742,   32,    0,  567,    0,  568,
   22,    0,   31,    0,  571,  684,    0,    0,   23,   24,
   25,   26,   27,   21,    0,    0,    0,   31,   32,   59,
   33,    0,   22,    0,   31,    0,   33,    0,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,    0,    0,
    0,   28,   21,    0,   32,    0,   29,   30,   21,   33,
    0,   22,   31,    0,    0,    0,  165,   22,   31,   23,
   24,   25,   26,   27,    0,   23,   24,   25,   26,   27,
    0,  166,  237,    0,    0,   33,    0,  110,    0,    0,
    0,   31,    0,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,   60,    0,    0,  148,  149,    0,    0,
   23,   24,   25,   26,   27,   22,  150,   31,    0,    0,
    0,   38,    0,   23,   24,   25,   26,   27,    0,    0,
    0,  148,  149,    0,    0,    0,    0,    0,    0,    0,
   22,  150,    0,    0,    0,   61,    0,    0,   23,   24,
   25,   26,   27,  148,  261,    0,    0,    0,    0,    0,
    1,    2,   22,  262,    3,    4,    0,    5,   21,    0,
   23,   24,   25,   26,   27,   21,    0,   22,  444,  445,
    6,    7,    0,    0,   22,   23,   24,   25,   26,   27,
    0,    0,   23,   24,   25,   26,   27,    0,    0,    0,
    0,    0,    8,   21,    0,    0,    0,    0,    0,   21,
    0,    0,   22,  725,    0,    0,    0,  180,   22,    0,
   23,   24,   25,   26,   27,    0,   23,   24,   25,   26,
   27,    0,   21,  181,    0,    0,    0,    0,   60,    0,
    0,   22,    0,    0,    0,    0,  684,    0,    0,   23,
   24,   25,   26,   27,   60,    0,    0,    0,  148,    0,
    0,    0,    0,    0,  182,  183,    0,   22,  184,  185,
  186,  187,  188,  189,    0,   23,   24,   25,   26,   27,
   61,    0,    0,    0,    0,   60,   60,    0,    0,   60,
   60,   60,   60,   60,   60,    0,   61,  272,  272,  446,
  447,  448,  449,    0,    0,    0,    0,    0,  450,  451,
  452,  453,  454,  455,  456,  457,  458,  459,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   61,   61,    0,
    0,   61,   61,   61,   61,   61,   61,    0,    0,    0,
    0,    0,  272,  272,  272,    0,    0,    0,    0,    0,
    0,    0,    0,  272,    0,    0,  272,  272,  272,  272,
  272,  272,  272,  272,  272,  272,    0,  272,  272,    0,
  272,  272,  272,  272,  272,  272,  272,  238,  238,  272,
  272,  272,  272,    0,    0,  272,    0,    0,    0,  272,
  272,  272,  272,  272,    0,  272,  272,  272,  272,  272,
  272,  272,    0,  272,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  272,    0,    0,    0,    0,    0,
    0,    0,  238,  238,  238,  272,  272,  272,  272,    0,
    0,    0,    0,  238,    0,    0,  238,  238,  238,  238,
  238,  238,  238,  238,  238,  238,    0,  238,  238,    0,
  238,  238,  238,  238,  238,  238,  238,  241,  241,  238,
  238,  238,  238,    0,    0,  238,    0,    0,    0,  238,
  238,  238,  238,  238,    0,  238,  238,  238,  238,  238,
  238,  238,    0,  238,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  238,    0,    0,    0,    0,    0,
    0,    0,  241,  241,  241,  238,  238,  238,  238,    0,
    0,    0,    0,  241,    0,    0,  241,  241,  241,  241,
  241,  241,  241,  241,  241,  241,    0,  241,  241,    0,
  241,  241,  241,  241,  241,  241,  241,  246,  246,  241,
  241,  241,  241,    0,    0,  241,    0,    0,    0,  241,
  241,  241,  241,  241,    0,  241,  241,  241,  241,  241,
  241,  241,    0,  241,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  241,    0,    0,    0,    0,    0,
    0,    0,  246,  246,  246,  241,  241,  241,  241,    0,
    0,    0,    0,  246,    0,    0,  246,  246,  246,  246,
  246,  246,  246,  246,  246,  246,    0,  246,  246,    0,
  246,  246,  246,  246,  246,  246,  246,  260,  260,  246,
  246,  246,  246,    0,    0,  246,    0,    0,    0,  246,
  246,  246,  246,  246,    0,  246,  246,  246,  246,  246,
  246,  246,    0,  246,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  246,    0,    0,    0,    0,    0,
    0,    0,  260,  260,  260,  246,  246,  246,  246,    0,
    0,    0,    0,  260,    0,    0,  260,  260,  260,  260,
  260,  260,  260,  260,  260,  260,    0,  260,  260,    0,
  260,  260,  260,  260,  260,  260,  260,  242,  242,  260,
  260,  260,  260,    0,    0,  260,    0,    0,    0,  260,
  260,  260,  260,  260,    0,  260,  260,  260,  260,  260,
  260,  260,    0,  260,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  260,    0,    0,    0,    0,    0,
    0,    0,  242,  242,  242,  260,  260,  260,  260,    0,
    0,    0,    0,  242,    0,    0,  242,  242,  242,  242,
  242,  242,  242,  242,  242,  242,    0,  242,  242,    0,
  242,  242,  242,  242,  242,  242,  242,  243,  243,  242,
  242,  242,  242,    0,    0,  242,    0,    0,    0,  242,
  242,  242,  242,  242,    0,  242,  242,  242,  242,  242,
  242,  242,    0,  242,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  242,    0,    0,    0,    0,    0,
    0,    0,  243,  243,  243,  242,  242,  242,  242,    0,
    0,    0,    0,  243,    0,    0,  243,  243,  243,  243,
  243,  243,  243,  243,  243,  243,    0,  243,  243,    0,
  243,  243,  243,  243,  243,  243,  243,  244,  244,  243,
  243,  243,  243,    0,    0,  243,    0,    0,    0,  243,
  243,  243,  243,  243,    0,  243,  243,  243,  243,  243,
  243,  243,    0,  243,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  243,    0,    0,    0,    0,    0,
    0,    0,  244,  244,  244,  243,  243,  243,  243,    0,
    0,    0,    0,  244,    0,    0,  244,  244,  244,  244,
  244,  244,  244,  244,  244,  244,    0,  244,  244,    0,
  244,  244,  244,  244,  244,  244,  244,  261,  261,  244,
  244,  244,  244,    0,    0,  244,    0,    0,    0,  244,
  244,  244,  244,  244,    0,  244,  244,  244,  244,  244,
  244,  244,    0,  244,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  244,    0,    0,    0,    0,    0,
    0,    0,  261,  261,  261,  244,  244,  244,  244,    0,
    0,    0,    0,  261,    0,    0,  261,  261,  261,  261,
  261,  261,  261,  261,  261,  261,    0,  261,  261,    0,
  261,  261,  261,  261,  261,  261,  261,  245,  245,  261,
  261,  261,  261,    0,    0,  261,    0,    0,    0,  261,
  261,  261,  261,  261,    0,  261,  261,  261,  261,  261,
  261,  261,    0,  261,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  261,    0,    0,    0,    0,    0,
    0,    0,  245,  245,  245,  261,  261,  261,  261,    0,
    0,    0,    0,  245,    0,    0,  245,  245,  245,  245,
  245,  245,  245,  245,  245,  245,    0,  245,  245,    0,
  245,  245,  245,  245,  245,  245,  245,  298,    0,  245,
  245,  245,  245,    0,    0,  245,    0,    0,    0,  245,
  245,  245,  245,  245,    0,  245,  245,  245,  245,  245,
  245,  245,    0,  245,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  245,    0,    0,    0,    0,    0,
    0,    0,  299,  300,  301,  245,  245,  245,  245,    0,
    0,    0,    0,  302,    0,    0,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,    0,  313,  314,    0,
  315,  316,  317,  318,  319,  320,  321,  189,    0,  322,
  323,  324,  325,    0,    0,  326,    0,    0,    0,  327,
  328,  329,  330,  331,    0,  332,  333,  334,  335,  336,
  337,  338,    0,  339,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  340,    0,    0,    0,    0,    0,
    0,    0,  189,  189,  189,  341,  342,  343,  344,    0,
    0,    0,    0,  189,    0,    0,  189,  189,  189,  189,
  189,  189,  189,  189,  189,  189,    0,  189,  189,    0,
  189,  189,  189,  189,  189,  189,  189,  190,    0,  189,
  189,  189,  189,    0,    0,  189,    0,    0,    0,  189,
  189,  189,  189,  189,    0,  189,  189,  189,  189,  189,
  189,  189,    0,  189,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  189,    0,    0,    0,    0,    0,
    0,    0,  190,  190,  190,  189,  189,  189,  189,    0,
    0,    0,    0,  190,    0,    0,  190,  190,  190,  190,
  190,  190,  190,  190,  190,  190,    0,  190,  190,    0,
  190,  190,  190,  190,  190,  190,  190,  191,    0,  190,
  190,  190,  190,    0,    0,  190,    0,    0,    0,  190,
  190,  190,  190,  190,    0,  190,  190,  190,  190,  190,
  190,  190,    0,  190,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  190,    0,    0,    0,    0,    0,
    0,    0,  191,  191,  191,  190,  190,  190,  190,    0,
    0,    0,    0,  191,    0,    0,  191,  191,  191,  191,
  191,  191,  191,  191,  191,  191,    0,  191,  191,    0,
  191,  191,  191,  191,  191,  191,  191,  192,    0,  191,
  191,  191,  191,    0,    0,  191,    0,    0,    0,  191,
  191,  191,  191,  191,    0,  191,  191,  191,  191,  191,
  191,  191,    0,  191,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  191,    0,    0,    0,    0,    0,
    0,    0,  192,  192,  192,  191,  191,  191,  191,    0,
    0,    0,    0,  192,    0,    0,  192,  192,  192,  192,
  192,  192,  192,  192,  192,  192,    0,  192,  192,    0,
  192,  192,  192,  192,  192,  192,  192,    0,    0,  192,
  192,  192,  192,    0,    0,  192,    0,    0,    0,  192,
  192,  192,  192,  192,    0,  192,  192,  192,  192,  192,
  192,  192,    0,  192,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  192,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  192,  192,  192,  192,  302,
    0,    0,  303,  304,  305,  306,  307,  308,  309,  310,
  311,  312,    0,  313,  314,    0,  315,  316,  317,  318,
  319,  320,  321,    0,    0,  322,  323,  324,  325,    0,
    0,  326,    0,    0,    0,  327,  328,  329,  330,  331,
    0,  332,  333,  334,  335,  336,  337,  338,    0,  339,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  340,  118,  119,  120,  253,  121,  122,  123,    0,  124,
    0,  341,  342,  343,  344,    0,    0,    0,  254,    0,
  255,    0,    0,  127,    0,    0,    0,    0,    0,    0,
  128,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   42,  497,  294,   40,   44,   40,   33,   33,   44,
  123,  123,  123,   60,  343,  678,   36,  123,   40,   61,
  123,   28,  205,   40,   31,  123,   40,  123,  274,   36,
  123,   38,  123,  123,  123,  123,  116,    6,    7,  123,
   40,   40,  530,   50,   44,  266,   44,   44,  276,   61,
   40,   40,  280,  236,   42,   42,  719,   64,   78,   28,
   44,   40,   44,  309,  285,   44,   44,   36,   60,   38,
   77,   78,   42,   80,   44,   61,  123,  572,   62,  309,
   87,  161,   89,   42,  270,  271,  125,  290,  291,   42,
  125,   44,  172,  100,  101,   93,  103,  177,  178,   91,
  107,  108,  260,   62,   91,   93,  309,   61,   77,   78,
  304,  294,  306,   41,  602,   41,   44,  605,   44,  313,
   51,   61,   53,  345,   60,  125,   61,  790,  125,   97,
    0,  123,  139,  355,  141,  215,  358,  466,  107,  219,
  273,  221,  280,  125,  277,  257,  125,  125,  257,   40,
   41,  373,  173,   44,    0,   91,   42,   88,  179,   90,
   61,  274,  295,  274,   40,  133,  134,  135,  274,  176,
   86,  274,   41,  294,  295,   44,  274,  360,  274,  147,
   42,  274,   44,  274,  274,  274,  274,  123,   82,  269,
  274,   40,   41,  385,  260,   44,  309,  389,  309,  220,
  392,   41,   42,  309,   98,  225,  701,  228,  260,  401,
  104,  309,  233,  234,  290,  291,   41,   41,  351,   44,
  227,  115,  138,  356,  140,  193,  194,  195,   41,  290,
  291,   44,  239,  294,  295,  274,  272,  153,  272,  274,
  287,  374,  375,  264,  265,  213,  741,  268,  274,  274,
  272,  473,  277,   41,   40,  272,   44,  479,  272,  300,
  301,  267,  303,  304,  305,  306,  307,  308,  162,  163,
  492,  493,  264,  272,  354,  123,  244,  284,  285,  286,
   40,  273,  272,  272,   41,  123,  202,   44,  204,  281,
  282,  283,  284,  285,  267,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  315,  316,
  317,  318,   41,   42,   41,  322,  323,   44,   41,  326,
  123,   44,   41,  343,   41,   44,   40,   44,  264,  223,
  123,  299,  353,  301,  341,  342,  343,  273,   40,  561,
  562,  474,  475,   41,   42,  281,  282,  283,  284,  285,
   60,  319,  320,  321,   41,   41,  324,   44,   44,  327,
  328,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  338,  363,  363,   42,  343,   44,   40,   42,  385,   44,
   40,   91,  389,   44,   42,  392,   44,   42,  395,   44,
   41,   42,   58,  361,  401,  402,   58,  404,  268,  269,
   41,   42,  272,  273,  309,  275,  413,   41,   42,  345,
  134,  135,  419,  123,   62,  637,    0,   61,  288,  289,
   40,   40,  268,  269,  362,  260,  272,  273,   44,  275,
  563,   44,  274,  257,  258,  259,  443,  261,  262,  263,
  310,  265,  288,  289,  274,  465,  466,  415,  370,  370,
  370,  257,  276,  460,  362,  279,  477,  464,  465,  466,
  751,   44,  286,  379,  310,  362,  362,   44,  384,   40,
  274,  387,  388,  274,  390,  391,  698,  393,  394,  257,
  396,  397,  398,  399,  400,  362,  257,  403,  257,  405,
  406,  407,  408,   61,  362,  464,  465,  466,  317,  403,
  274,  257,  257,  273,  370,  638,   44,    0,   44,   44,
  290,  291,   52,   53,  294,  295,  296,  297,  298,  702,
   44,  528,   44,  530,   44,  370,  533,  534,  535,  536,
  537,  538,  539,  540,  541,  542,  543,  544,  558,  370,
   44,  628,  629,  726,  631,  266,  462,  627,  555,  370,
  557,  558,  370,    0,  264,  523,  524,  525,   40,   41,
  370,  370,   44,  273,  532,   60,   44,   44,  751,  109,
  370,  281,  282,  283,  284,  285,  116,   44,  370,  370,
   62,  370,   44,  499,   44,  370,  555,  503,  557,  558,
  506,   44,   44,  509,   44,  602,   91,  362,  605,  515,
  516,   44,  518,   44,   44,  692,   44,  694,  695,   44,
  690,   93,   44,  693,   44,   44,  156,   44,   44,  626,
   44,  161,   44,   40,   44,   91,   44,  634,  123,  545,
  546,  547,   44,   44,   44,  345,  643,   44,   40,  607,
   44,   44,  257,  125,   44,   44,   44,   44,   44,  736,
   44,   44,   91,   44,  734,   44,  362,  625,  574,   44,
  576,  577,  362,  579,  580,  634,  582,  583,  689,  585,
  586,  587,  588,  589,   44,   44,  592,  684,  594,  595,
  596,  597,  222,   44,  268,  269,  362,  603,  272,  273,
  257,  275,  362,  257,   44,   40,  257,  704,   93,  257,
  668,  669,   40,   40,  288,  289,  260,  363,  728,   40,
  731,   40,  733,  370,   44,    0,  370,  370,   40,   40,
   10,   54,  276,  730,  147,  146,  310,   18,  208,  645,
  270,  190,  648,  227,  176,  651,   64,  686,  654,  553,
  554,  762,  556,  700,  660,  661,  347,  663,  347,  376,
  730,  360,  624,  307,  308,   -1,  724,  311,  312,  313,
  314,  315,  316,  679,  680,  681,   -1,  774,   -1,  264,
  686,  704,  779,  780,  781,  268,  269,   -1,  273,  272,
  273,   -1,  275,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  273,  274,   -1,   -1,  288,  289,   -1,   40,   -1,
   -1,   -1,  770,  771,  772,   -1,   -1,  302,   -1,   -1,
   -1,   -1,  728,   -1,  782,   -1,  630,  310,  632,  633,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,   -1,
   -1,  288,  289,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,  310,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,  696,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,  300,
  301,   -1,  303,  304,  305,  306,  307,  308,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,    0,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   40,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   42,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,    0,   -1,   -1,   -1,  329,   -1,   91,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   40,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   -1,  361,
   60,  123,    0,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   91,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,  123,  346,  347,  348,  349,  350,  351,
  352,    0,   -1,  355,  356,  357,  358,   -1,   -1,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   42,  268,  269,  390,   -1,
  272,  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,  401,
  402,  403,  404,   60,   -1,   -1,  288,  289,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,  310,   -1,
  272,  273,   -1,   -1,   91,   -1,   -1,  279,   -1,   42,
   -1,   44,   -1,   -1,  286,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   60,   42,   -1,
  272,  273,   -1,   -1,   -1,   -1,  123,  279,   -1,   -1,
   -1,   -1,   -1,   -1,  286,   -1,   60,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   91,   -1,
   -1,   -1,  272,  273,   -1,   60,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,  286,   91,   -1,   41,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,  361,
  123,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,
   -1,  288,  289,  375,  376,  377,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,  361,
  268,  269,   -1,  310,  272,  273,   -1,  275,  123,   -1,
   -1,   -1,   -1,  375,  376,  377,   -1,   -1,  268,  269,
  288,  289,  272,  273,   -1,  275,   -1,   91,   -1,   -1,
   -1,  361,   60,   -1,   -1,   -1,   -1,   -1,  288,  289,
   -1,   -1,  310,   -1,   -1,  375,  376,  377,   -1,   -1,
  257,  258,  259,   60,  261,  262,  263,   -1,  265,  123,
  310,   -1,   -1,   91,   -1,  272,  273,   -1,   -1,  268,
  269,   -1,  279,  272,  273,   -1,  275,   -1,  125,  286,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  288,
  289,   -1,   -1,  300,  301,  123,  303,  304,  305,  306,
  307,  308,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  310,  265,   -1,   -1,   -1,  123,   -1,   -1,  272,
  273,   -1,   -1,  257,  258,  259,  279,  261,  262,  263,
   -1,  265,   -1,  286,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,  257,  258,  259,  279,  261,  262,  263,   -1,
  265,   -1,  286,   -1,  361,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,  125,   -1,   -1,  375,  376,
  377,  286,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,  300,  301,   -1,  303,  304,
  305,  306,  307,  308,  276,   -1,   -1,  279,   -1,   -1,
  264,   -1,   -1,   -1,  286,   -1,   -1,   -1,  361,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  375,  376,  377,   -1,  273,  361,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,  375,  376,  377,  272,  273,  361,   -1,   -1,   -1,
   -1,  279,  125,   -1,   -1,   -1,   -1,  264,  286,   -1,
  375,  376,  377,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  318,  319,  320,  281,  282,  283,  284,  285,   -1,
   -1,  345,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,  302,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,  361,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  390,   -1,   -1,   -1,  375,  376,  377,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
  273,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,
  390,  380,  381,  382,  383,  384,  385,  386,  387,  388,
  389,  401,  402,  403,  404,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,  273,  361,   -1,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
  125,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,  273,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,  402,  403,  404,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,  273,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
  402,  403,  404,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   -1,   -1,
  355,  356,  357,  358,   -1,  273,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,  125,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,
  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,  273,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,  273,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,  273,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,  125,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
  273,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,  273,  361,   -1,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   60,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   42,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   41,   -1,  401,  402,  403,  404,  318,
  319,  320,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
  329,   -1,   60,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,  123,  355,  356,  357,  358,
   -1,  273,  361,   91,   -1,   41,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,  401,  402,  403,  404,  318,  319,  320,   -1,
   60,   -1,   -1,   -1,   -1,   91,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   91,   -1,  355,  356,  357,  358,  123,   60,  361,
  220,   -1,   -1,  365,  366,  367,  368,  369,  228,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  390,   91,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,  401,
  402,  403,  404,   -1,  264,  265,  264,   -1,  268,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   60,   -1,   -1,
   -1,  123,   -1,  281,  282,  283,  284,  285,  288,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   60,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   91,   -1,
   -1,   -1,  279,   -1,   -1,  273,   -1,   -1,   -1,  286,
  278,   -1,   -1,  281,  282,  283,  284,  285,   60,   -1,
   91,   -1,  330,  331,   60,  293,   -1,   -1,   -1,   -1,
  123,  257,  258,  259,   -1,  261,  262,  263,  348,  265,
  350,   -1,   -1,  353,   -1,   -1,   -1,   -1,  264,   91,
  276,   -1,  123,  279,   -1,   91,  366,  273,  368,   60,
  286,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   60,   -1,  264,   -1,   -1,  293,   -1,   -1,
   -1,  123,   -1,  273,   -1,   -1,   -1,  123,   -1,   -1,
   91,  281,  282,  283,  284,  285,   -1,   60,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  299,
  300,  301,  264,  303,  304,  305,  306,  307,  308,   60,
   -1,  273,  123,   -1,   -1,   -1,   -1,   -1,   91,  281,
  282,  283,  284,  285,   60,  123,   -1,  125,   -1,   -1,
   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,  300,  301,
   91,  303,  304,  305,  306,  307,  308,   -1,   -1,  469,
  123,   -1,   -1,   -1,   -1,   91,   -1,  477,   -1,   60,
   -1,  264,   91,   -1,   93,   60,   -1,  487,   -1,  489,
  273,   -1,  123,   -1,  494,  278,   -1,   -1,  281,  282,
  283,  284,  285,  264,   -1,   -1,   -1,  123,   60,  125,
   91,   -1,  273,   -1,  123,   -1,   91,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,  292,  264,   -1,   60,   -1,  297,  298,  264,   91,
   -1,  273,  123,   -1,   -1,   -1,  278,  273,  123,  281,
  282,  283,  284,  285,   -1,  281,  282,  283,  284,  285,
   -1,  293,  125,   -1,   -1,   91,   -1,  293,   -1,   -1,
   -1,  123,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  125,   -1,   -1,  264,  265,   -1,   -1,
  281,  282,  283,  284,  285,  273,  274,  123,   -1,   -1,
   -1,  292,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  274,   -1,   -1,   -1,  125,   -1,   -1,  281,  282,
  283,  284,  285,  264,  265,   -1,   -1,   -1,   -1,   -1,
  268,  269,  273,  274,  272,  273,   -1,  275,  264,   -1,
  281,  282,  283,  284,  285,  264,   -1,  273,  261,  262,
  288,  289,   -1,   -1,  273,  281,  282,  283,  284,  285,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   -1,  310,  264,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,  273,  274,   -1,   -1,   -1,  260,  273,   -1,
  281,  282,  283,  284,  285,   -1,  281,  282,  283,  284,
  285,   -1,  264,  276,   -1,   -1,   -1,   -1,  260,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  276,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,   -1,  307,  308,   -1,  273,  311,  312,
  313,  314,  315,  316,   -1,  281,  282,  283,  284,  285,
  260,   -1,   -1,   -1,   -1,  307,  308,   -1,   -1,  311,
  312,  313,  314,  315,  316,   -1,  276,  273,  274,  382,
  383,  384,  385,   -1,   -1,   -1,   -1,   -1,  391,  392,
  393,  394,  395,  396,  397,  398,  399,  400,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,
   -1,  311,  312,  313,  314,  315,  316,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,  257,  258,  259,  260,  261,  262,  263,   -1,  265,
   -1,  401,  402,  403,  404,   -1,   -1,   -1,  274,   -1,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,
  };

#line 1078 "Repil/IR/IR.jay"

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
  public const int X86_FP80 = 284;
  public const int INTEGER_TYPE = 285;
  public const int ZEROINITIALIZER = 286;
  public const int OPAQUE = 287;
  public const int DEFINE = 288;
  public const int DECLARE = 289;
  public const int UNNAMED_ADDR = 290;
  public const int LOCAL_UNNAMED_ADDR = 291;
  public const int NOALIAS = 292;
  public const int ELLIPSIS = 293;
  public const int GLOBAL = 294;
  public const int CONSTANT = 295;
  public const int PRIVATE = 296;
  public const int INTERNAL = 297;
  public const int EXTERNAL = 298;
  public const int FASTCC = 299;
  public const int SIGNEXT = 300;
  public const int ZEROEXT = 301;
  public const int VOLATILE = 302;
  public const int RETURNED = 303;
  public const int NONNULL = 304;
  public const int NOCAPTURE = 305;
  public const int WRITEONLY = 306;
  public const int READONLY = 307;
  public const int READNONE = 308;
  public const int ATTRIBUTE_GROUP_REF = 309;
  public const int ATTRIBUTES = 310;
  public const int NORECURSE = 311;
  public const int NOUNWIND = 312;
  public const int SPECULATABLE = 313;
  public const int SSP = 314;
  public const int UWTABLE = 315;
  public const int ARGMEMONLY = 316;
  public const int SEQ_CST = 317;
  public const int RET = 318;
  public const int BR = 319;
  public const int SWITCH = 320;
  public const int INDIRECTBR = 321;
  public const int INVOKE = 322;
  public const int RESUME = 323;
  public const int CATCHSWITCH = 324;
  public const int CATCHRET = 325;
  public const int CLEANUPRET = 326;
  public const int UNREACHABLE = 327;
  public const int FNEG = 328;
  public const int ADD = 329;
  public const int NUW = 330;
  public const int NSW = 331;
  public const int FADD = 332;
  public const int SUB = 333;
  public const int FSUB = 334;
  public const int MUL = 335;
  public const int FMUL = 336;
  public const int UDIV = 337;
  public const int SDIV = 338;
  public const int FDIV = 339;
  public const int UREM = 340;
  public const int SREM = 341;
  public const int FREM = 342;
  public const int SHL = 343;
  public const int LSHR = 344;
  public const int EXACT = 345;
  public const int ASHR = 346;
  public const int AND = 347;
  public const int OR = 348;
  public const int XOR = 349;
  public const int EXTRACTELEMENT = 350;
  public const int INSERTELEMENT = 351;
  public const int SHUFFLEVECTOR = 352;
  public const int EXTRACTVALUE = 353;
  public const int INSERTVALUE = 354;
  public const int ALLOCA = 355;
  public const int LOAD = 356;
  public const int STORE = 357;
  public const int FENCE = 358;
  public const int CMPXCHG = 359;
  public const int ATOMICRMW = 360;
  public const int GETELEMENTPTR = 361;
  public const int ALIGN = 362;
  public const int INBOUNDS = 363;
  public const int INRANGE = 364;
  public const int TRUNC = 365;
  public const int ZEXT = 366;
  public const int SEXT = 367;
  public const int FPTRUNC = 368;
  public const int FPEXT = 369;
  public const int TO = 370;
  public const int FPTOUI = 371;
  public const int FPTOSI = 372;
  public const int UITOFP = 373;
  public const int SITOFP = 374;
  public const int PTRTOINT = 375;
  public const int INTTOPTR = 376;
  public const int BITCAST = 377;
  public const int ADDRSPACECAST = 378;
  public const int ICMP = 379;
  public const int EQ = 380;
  public const int NE = 381;
  public const int UGT = 382;
  public const int UGE = 383;
  public const int ULT = 384;
  public const int ULE = 385;
  public const int SGT = 386;
  public const int SGE = 387;
  public const int SLT = 388;
  public const int SLE = 389;
  public const int FCMP = 390;
  public const int OEQ = 391;
  public const int OGT = 392;
  public const int OGE = 393;
  public const int OLT = 394;
  public const int OLE = 395;
  public const int ONE = 396;
  public const int ORD = 397;
  public const int UEQ = 398;
  public const int UNE = 399;
  public const int UNO = 400;
  public const int PHI = 401;
  public const int SELECT = 402;
  public const int CALL = 403;
  public const int TAIL = 404;
  public const int VA_ARG = 405;
  public const int LANDINGPAD = 406;
  public const int CATCHPAD = 407;
  public const int CLEANUPPAD = 408;
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
