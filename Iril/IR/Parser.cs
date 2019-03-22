// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Iril/IR/IR.jay"
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Linq;

using Iril.Types;

#pragma warning disable 219,414

namespace Iril.IR
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
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type ',' ALIGN INTEGER",
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
//t    "metadata_arg : SYMBOL ':' typed_constant",
//t    "metadata_arg : TYPE ':' META_SYMBOL",
//t    "metadata_arg : ALIGN ':' constant",
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
//t    "function_definition : define_header GLOBAL_SYMBOL parameters attribute_group_refs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "define_header : DEFINE return_type",
//t    "define_header : DEFINE parameter_attribute return_type",
//t    "define_header : DEFINE define_header_attributes return_type",
//t    "define_header : DEFINE define_header_attributes parameter_attribute return_type",
//t    "define_header_attributes : NOALIAS",
//t    "define_header_attributes : runtime_preemption_specifier",
//t    "define_header_attributes : calling_convention",
//t    "define_header_attributes : linkage",
//t    "define_header_attributes : linkage runtime_preemption_specifier",
//t    "define_header_attributes : linkage runtime_preemption_specifier calling_convention",
//t    "define_header_attributes : linkage calling_convention",
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
//t    "runtime_preemption_specifier : DSO_LOCAL",
//t    "runtime_preemption_specifier : DSO_PREEMPTABLE",
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
//t    "terminator_instruction : UNREACHABLE",
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
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST","DSO_LOCAL",
    "DSO_PREEMPTABLE","RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME",
    "CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD",
    "NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV",
    "UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR","XOR",
    "EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
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
#line 60 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 64 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 68 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 72 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 76 "Iril/IR/IR.jay"
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
#line 96 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 100 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 109 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 121 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 125 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 129 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 133 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 137 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 141 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 145 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 149 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 153 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 27:
#line 157 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 28:
#line 158 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 162 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 30:
#line 163 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 31:
#line 167 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 32:
  case_32();
  break;
case 33:
  case_33();
  break;
case 34:
#line 184 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 185 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 186 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 187 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 188 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 39:
#line 189 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 40:
#line 190 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 41:
#line 194 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 42:
#line 198 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 43:
#line 205 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 44:
#line 209 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 45:
#line 216 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 46:
#line 220 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 47:
#line 224 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 48:
#line 228 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 49:
#line 232 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 67:
#line 265 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 68:
#line 269 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 69:
#line 273 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 70:
#line 280 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 71:
#line 284 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 289 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 76:
#line 295 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 77:
#line 296 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 78:
#line 297 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 79:
#line 298 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 80:
#line 302 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 81:
#line 306 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 310 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 83:
#line 314 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 318 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 85:
#line 322 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 86:
#line 329 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 333 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 89:
#line 341 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 90:
  case_90();
  break;
case 91:
  case_91();
  break;
case 92:
  case_92();
  break;
case 93:
  case_93();
  break;
case 94:
#line 371 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 95:
#line 375 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 96:
#line 379 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 97:
#line 383 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 98:
#line 390 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 99:
#line 394 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 100:
#line 398 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 105:
#line 409 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 106:
#line 413 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 107:
#line 417 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 108:
#line 421 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 109:
#line 422 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 110:
#line 429 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 433 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 440 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 444 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 114:
#line 448 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 115:
#line 452 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 117:
#line 460 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 464 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 119:
#line 465 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 120:
#line 466 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 121:
#line 467 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 122:
#line 468 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 123:
#line 469 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 124:
#line 470 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 125:
#line 471 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 133:
#line 494 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 134:
#line 495 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 135:
#line 496 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 136:
#line 497 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 137:
#line 498 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 138:
#line 499 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 139:
#line 500 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 140:
#line 501 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 141:
#line 502 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 142:
#line 503 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 143:
#line 507 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 144:
#line 508 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 145:
#line 509 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 146:
#line 510 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 147:
#line 511 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 148:
#line 512 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 149:
#line 513 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 150:
#line 514 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 151:
#line 515 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 152:
#line 516 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 153:
#line 517 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 154:
#line 518 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 155:
#line 519 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 156:
#line 520 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 157:
#line 521 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 158:
#line 522 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 160:
#line 527 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 161:
#line 528 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 162:
#line 532 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 163:
#line 536 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 164:
#line 540 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 165:
#line 544 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 166:
#line 548 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 167:
#line 552 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 168:
#line 556 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 170:
#line 564 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 171:
#line 565 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 172:
#line 566 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 173:
#line 567 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 174:
#line 568 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 175:
#line 569 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 176:
#line 570 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 177:
#line 571 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 178:
#line 572 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 179:
#line 579 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 586 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 590 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 182:
#line 597 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 604 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 608 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 615 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 626 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 630 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 637 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 641 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 648 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 652 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 193:
#line 656 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 660 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 195:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 671 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 678 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 682 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 199:
#line 686 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 690 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 698 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 203:
#line 699 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 204:
#line 706 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 710 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 717 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 207:
#line 721 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 208:
#line 725 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 209:
#line 729 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 210:
#line 733 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 211:
#line 737 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 212:
#line 741 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 214:
#line 746 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 215:
#line 750 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 216:
#line 754 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 758 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 762 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 223:
#line 779 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 783 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 789 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 226:
#line 796 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 800 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 807 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 825 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 235:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 844 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 239:
#line 848 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 240:
#line 855 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 859 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 863 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 867 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 871 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 245:
#line 875 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 246:
#line 879 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 883 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 248:
#line 887 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 249:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 250:
#line 895 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 251:
#line 899 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 252:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 253:
#line 907 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 254:
#line 911 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 255:
#line 915 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 256:
#line 919 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 257:
#line 923 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 258:
#line 927 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 931 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 935 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 943 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 947 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 951 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 955 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 959 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 963 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 967 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 971 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 975 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 979 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 983 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 987 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 991 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 275:
#line 995 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 276:
#line 999 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 277:
#line 1003 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 278:
#line 1007 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1011 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1015 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1019 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1023 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1027 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1031 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1035 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1039 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1043 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1047 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1051 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1055 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1059 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1063 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 293:
#line 1067 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 294:
#line 1071 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1075 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1079 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1083 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1087 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1091 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1095 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1099 "Iril/IR/IR.jay"
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
#line 78 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 83 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 88 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 102 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 111 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_32()
#line 172 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_33()
#line 177 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_90()
#line 346 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_91()
#line 351 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_92()
#line 356 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_93()
#line 361 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-8+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,   10,   10,   16,   16,
   15,    9,    9,   17,   17,   17,   17,   17,   17,   17,
   17,   17,   13,   13,    8,    8,    8,    8,    8,   21,
   21,   21,    7,    7,   23,   23,   23,   23,   23,   23,
   23,   23,   23,   23,   23,   23,    3,    3,    3,   24,
   24,   25,   25,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   26,   26,   27,   27,    4,
    4,    4,    4,   28,   28,   28,   28,   33,   33,   33,
   33,   33,   33,   33,    5,    5,    5,   29,   29,   36,
   36,   37,   37,   37,   37,   38,   38,   32,   32,   32,
   32,   32,   32,   32,   32,   14,   14,   34,   34,   30,
   30,   39,   40,   40,   40,   40,   40,   40,   40,   40,
   40,   40,   41,   41,   41,   41,   41,   41,   41,   41,
   41,   41,   41,   41,   41,   41,   41,   41,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   44,   18,
   18,   18,   18,   18,   18,   18,   18,   18,   45,   22,
   22,   46,   43,   43,   19,   47,   42,   42,   31,   31,
   48,   48,   48,   48,   49,   49,   51,   51,   51,   51,
   53,   54,   54,   55,   55,   56,   56,   56,   56,   56,
   56,   56,   57,   57,   57,   57,   57,   57,   20,   20,
   58,   58,   59,   59,   60,   61,   61,   62,   63,   63,
   64,   64,   35,   65,   50,   50,   50,   50,   50,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    8,    1,    1,    1,    1,
    1,    1,    3,    3,    3,    3,    3,    3,    3,    3,
    6,    5,    2,    3,    1,    2,    3,    3,    3,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    3,    1,    1,    1,    4,    2,    3,    5,    1,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    4,    2,    1,    5,    5,    1,    3,    1,    1,    7,
    8,    8,    9,    2,    3,    3,    4,    1,    1,    1,
    1,    2,    3,    2,    5,    6,    6,    3,    2,    1,
    3,    1,    2,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    6,    9,    6,    6,    3,    3,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    2,
    1,    2,    1,    3,    2,    1,    1,    3,    1,    2,
    2,    3,    1,    2,    1,    2,    1,    2,    3,    4,
    1,    3,    2,    1,    3,    2,    3,    3,    3,    2,
    4,    5,    1,    1,    6,    9,    6,    6,    1,    3,
    1,    1,    1,    3,    5,    1,    2,    3,    1,    2,
    1,    1,    1,    1,    2,    7,    2,    7,    1,    5,
    6,    5,    5,    5,    6,    4,    4,    5,    6,    5,
    6,    6,    6,    7,    5,    6,    7,    4,    5,    6,
    5,    2,    5,    4,    4,    4,    4,    5,    6,    7,
    6,    6,    4,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   73,   83,   76,   77,   78,   79,   75,   98,   30,
   29,  233,  123,  124,  125,  118,  119,  121,  120,  122,
  128,  129,    0,    0,    0,   74,    0,    0,    0,    0,
    0,   99,  100,    0,    0,    0,    3,    0,    4,    0,
    0,  126,  127,   27,   28,   31,    0,    0,    0,    0,
    0,    0,    0,    0,   67,    0,    0,    0,    0,    0,
    0,   82,    0,  104,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,   68,    0,    0,
    0,    0,  103,   89,   80,    0,    0,   86,    0,    0,
    0,    0,  114,  115,  109,    0,    0,  110,  132,    0,
    0,  130,  172,  173,  171,  174,  175,  176,  170,  161,
  160,  178,  177,    0,    0,    0,    0,    0,    0,    0,
    0,  159,    0,    0,    0,    0,    0,    0,    0,    0,
   32,    0,    0,    0,   52,   51,   13,    0,    0,   45,
   50,    0,    0,    0,    0,   81,    0,    0,    0,    0,
    0,    0,   65,   57,   55,   56,   58,   59,   60,   61,
    0,   53,  116,    0,  108,    0,    0,    0,    0,    0,
  131,    0,    0,    0,    0,  183,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   15,    0,
    0,    0,   46,   14,    0,  180,   84,   69,   85,   87,
    0,    0,    0,    0,   12,   54,  117,  111,    0,    0,
   43,    0,    0,    0,    0,  239,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  189,    0,    0,  195,    0,    0,    0,    0,    0,    0,
    0,  168,    0,  166,  167,    0,    0,    0,    0,    0,
    0,    0,   36,    0,   34,    0,   37,   38,   39,   40,
   33,   17,   16,   49,   48,   47,   62,  222,  221,    0,
  219,    0,    0,    0,  237,    0,    0,  235,    0,  231,
  232,    0,    0,  229,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  234,  262,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  133,  134,  135,  136,  137,  138,  139,  140,  141,  142,
    0,  143,  144,  155,  156,  157,  158,  146,  148,  149,
  150,  151,  147,  145,  153,  154,  152,    0,    0,    0,
    0,    0,    0,    0,   90,  190,    0,  196,    0,    0,
   44,    0,    0,    0,    0,    0,  184,    0,    0,    0,
   26,    0,    0,    0,    0,  185,   66,    0,   92,    0,
    0,  179,    0,    0,    0,    0,  230,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  223,    0,  201,    0,    0,
    0,    0,    0,    0,    0,    0,   91,    0,    0,    0,
    0,    0,    0,    0,    0,   23,    0,   42,    0,  220,
   93,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  258,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  165,  162,  164,    0,
    0,    0,    0,   41,    0,    0,  240,    0,  259,  294,
    0,  268,  279,    0,  263,  297,    0,  283,  261,  299,
  291,  287,    0,    0,  276,    0,  244,  243,  278,  300,
    0,    0,  242,    0,  169,  182,    0,    0,    0,    0,
    0,    0,    0,    0,  224,    0,    0,  203,    0,    0,
  204,    0,    0,  248,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  226,  241,  295,  280,  284,
  288,  277,  245,  272,  289,    0,    0,    0,    0,    0,
  186,    0,  187,  271,  260,    0,    0,    0,    0,  206,
    0,  202,    0,    0,  249,    0,    0,  256,    0,    0,
  236,    0,  238,  227,    0,  274,    0,  292,    0,    0,
  225,  285,    0,  214,  208,    0,    0,    0,    0,  213,
  209,  207,  205,    0,  257,  163,  228,  275,  293,  188,
  211,    0,    0,    0,    0,    0,  212,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  218,  215,  217,    0,    0,  216,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   46,   12,   13,   14,  191,  168,  160,   67,
  169,  508,  200,   68,   69,   48,  161,  152,  664,  330,
  170,  681,  192,   77,   78,  117,  118,   15,   93,  131,
  290,  193,   51,   52,   53,  127,  128,  194,  132,  401,
  418,  682,  207,  636,  338,  565,  683,  291,  292,  293,
  294,  295,  509,  588,  650,  651,  721,  331,  505,  506,
  665,  666,  343,  344,  376,
  };
  protected static readonly short [] yySindex = {          668,
   -8, -142,   20,   24,   44, 2925, 3111, -196,    0,  668,
    0,    0,    0,    0, -149, -123,   78,   85,  474, -119,
   17,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 1548,  -99,  -78,    0,  145, -187,  151, 3384,
 3051,    0,    0, 3384,  -37,  160,    0,  194,    0,  -34,
   -1,    0,    0,    0,    0,    0, 3384, -141,   25,   23,
  -55,  227,  -16,  155,    0,  145,  -23,  151,   22, 3384,
   26,    0,  -15,    0, 2166,  151,  151, 3384,  -12,  194,
  180, 2140, -126,    0,    0, 2020, 3384, -141, 3384, -141,
    0,  184,    0, -238,  271,  208, 3239,    0, 3384, 3384,
  -14, 3384,    0,    0,    0,  145,  -31,    0,  151,  194,
 -126, 1936,    0,    0,    0,  162,   16,    0,    0,   33,
 -112,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -19,  294,  315,  324, 3410, 3410, 3410,
  304,    0, 2020, 3384,  517, 3384,  312,  314,  333,   57,
    0, -238, 3307,    0,    0,    0,    0,   -5, 2020,    0,
    0,  145,   35,  330,  -10,    0, 3156,   33,   33,   33,
  305,  334,    0,    0,    0,    0,    0,    0,    0,    0,
 -113,    0,    0,  440,    0, 3215,  -92,  120, 4277, -117,
    0,  368, 3410, 3410, 3410,    0,  -11,   46,   -4,   45,
  371, 2020,   55,  376, 1960, 3279,  149, 1736,    0, -238,
   74,   -3,    0,    0, 3322,    0,    0,    0,    0,    0,
   33,   33,  171, 2008,    0,    0,    0,    0, 4277, -115,
    0,  377, 3410, -197, 3410,    0,  590, 3384,  590, 3384,
  590, 3384, 3384,  -45, 3384, 3384, 3384,  590,   95,  413,
 3384, 3384, 3384, 3410, 3410, 3410, 3384, 1193, 1281,  115,
  173, 3410, 3410, 3410, 3410, 3410, 3410, 3410, 3410, 3410,
 3410, 3410, 3410, 1707, 3312, 3384, 3384, 3013,   38, 2165,
    0, 4277,  174,    0,  174,  175, 4277, 3384,   67,   80,
   92,    0, 3410,    0,    0,  193,  107,  428,  218,  112,
  113,  434,    0,  443,    0,  850,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  137,
    0, 2253, 4277, 4705,    0,  207, 2114,    0,  452,    0,
    0, 2020,  590,    0, 2020, 2020,  590, 2020, 2020,  590,
 2020, 2020, 3384, 2020, 2020, 2020, 2020, 2020,  590, 3384,
 2020, 3384, 2020, 2020, 2020, 2020,  454,  455,  456,   91,
 3384,  125, 3410,  457,    0,    0, 3384,  241,  130,  131,
  133,  138,  139,  140,  142,  143,  147,  148,  150,  153,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3384,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3384,   34, 2020,
   47, 3384, 3051, 3013,    0,    0,  174,    0,  243,  243,
    0, 2341,  255, 3384, 3384, 3384,    0,  174,  264,  159,
    0,  276,  277,  164,  551,    0,    0, 2008,    0, 2429,
  174,    0,  491,  274,  493, 2020,    0,  497,  499, 2020,
  500,  501, 2020,  502,  506, 2020,  510,  512,  514,  520,
  521, 2020, 2020,  522, 2020,  523,  526,  527,  535, 3410,
 3410, 3410,  209,  281, 3384,  536, 3384,  296, 3410, 3384,
 3384, 3384, 3384, 3384, 3384, 3384, 3384, 3384, 3384, 3384,
 3384, 2020, 2020, 2114,  538,    0,  541,    0,  546,   47,
   47, 3384,   47, 3384, 3051,  243,    0, 3410,  310,  316,
  319,  243,  174,  331,  174,    0,  336,    0,  152,    0,
    0,  243,  274,  496, 2114,  552, 2114, 2114,  554, 2114,
 2114,  555, 2114, 2114,  557, 2114, 2114, 2114, 2114, 2114,
  559,  565, 2114,  568, 2114, 2114, 2114, 2114,    0,  570,
  573,  361, 3384, 2020,  582, 3384,  584, 3410,  592,  145,
  145,  145,  145,  145,  145,  145,  145,  145,  145,  145,
  145,  594,  595,  602,  556, 3410, 3199,   33,  546,  546,
   47,  546,   47,   47, 3384,  604,    0,    0,    0,  243,
  174,  243,  174,    0,  605, 3384,    0, 2114,    0,    0,
 2114,    0,    0, 2114,    0,    0, 2114,    0,    0,    0,
    0,    0, 2114, 2114,    0, 2114,    0,    0,    0,    0,
 3410, 3410,    0,  608,    0,    0,  263,  610,  293,  611,
 3410, 2114, 2114, 2114,    0,  616, 1636,    0,  583,  179,
    0,   33,   33,    0,  546,   33,  546,  546,   47, 3410,
  243,  243,  274,  617, 3371,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  298,  406,  300,  408, 3410,
    0,  622,    0,    0,    0,  577, 3410,  631,  372,    0,
 2057,    0, 3337,   33,    0,   33,   33,    0,  546,  186,
    0,  274,    0,    0,  415,    0,  416,    0,  622, 3410,
    0,    0, 2195,    0,    0,  311,  635,  638,  639,    0,
    0,    0,    0,   33,    0,    0,    0,    0,    0,    0,
    0,  235,  642, 3410, 3410, 3410,    0, 3384,  313,  318,
  327,  301, 3384, 3384, 3384, 3410,  321,  341,  343,  640,
    0,    0,    0, 3410,  254,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  683,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  290, 3104,  419,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,    0,
    0,    0, 3151,    0,    0,  421,  429,    0,    0,    0,
    0,    0,    0,    0,    0,  648,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  144,    0,    0,  430,    0,
    0,    0,    0,    0,    0,  165,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  648,    0,  648,    0,    0,    0,    0,    0,
    0,    0,    0,  722,    0,    0,    0,    0,  648,    0,
    0,   27,  648,    0,  648,    0,    0,    0,    0,  172,
 -100,  308,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  280,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  648,    0,    0,  648,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  322,  529,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 2517,    0, 4365,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  656,    0,    0,
    0,    0,    0,  291,    0,  648,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  648,    0,    0,  648,  648,    0,  648,  648,    0,
  648,  648,    0,  648,  648,  648,  648,  648,    0,    0,
  648,    0,  648,  648,  648,  648,    0,    0,    0,  648,
    0,  648,    0,    0,    0,    0,    0,  648,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  648,  648,
    0,    0,    0,    0,    0,    0, 2605,    0, 2693, 4453,
    0,    0,  648,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4541,    0,    0,    0,    0,  648,    0,    0,    0,  648,
    0,    0,  648,    0,    0,  648,    0,    0,    0,    0,
    0,  648,  648,    0,  648,    0,    0,    0,    0,    0,
    0,    0,    0,  648,    0,    0,    0,  648,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  648,  648,    0, 3397,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 2781,    0,    0,  648,  648,
  648,  680,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4629,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  648,    0,    0,    0,    0,    0,  810,
  898,  986, 1077, 1165, 1253, 1341, 1432, 1520, 1608, 1696,
 1787,    0,    0,    0,    0,    0,    0, 3485,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  725,
  759,  935,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  648,    0,
    0, 3573,    0,    0,    0, 3661,    0,    0,    0,    0,
 1023, 1914,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3749,    0,    0,    0,    0,    0,  295,  648,    0,
    0,    0,    0, 3837,    0, 3925, 4013,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4101,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 4189,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  648,    0,    0,    0,    0,  648,  648,  648,    0,
    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  693,  633,    0,    0,    0,    0,  544,  560,   51,
   -6,  363, -150,    3,    0,  697,  503, -182,  515, -443,
    0,  124,  539,  653,   12,    0,  575,    0,  -36, -114,
 -217,   -2,    0,  686,  -13,    0,  540,   93, -122,    0,
    0, -637,  237,    0, -434, -436,   43, -276,    0,  462,
  465,  424, -369, -563,    0,   68,    0,  325,    0,  182,
    0,  116,  -83, -170,    0,
  };
  protected static readonly short [] yyTable = {            47,
   47,  529,   85,   50,  102,  297,  180,  333,  201,  176,
  199,  235,  177,  426,   44,  197,  106,   49,   55,  534,
  109,  332,  700,   80,   63,  653,  654,   85,  656,  109,
  239,   82,  303,  317,   84,  320,   76,  157,  225,  303,
  225,  158,  709,   47,   47,   45,  240,   47,   88,   74,
  567,  329,   16,  121,   72,  426,  195,  201,   70,  196,
   96,   86,   87,  231,  232,   89,   72,   43,  336,  113,
   71,   98,  100,   76,  201,   82,   82,   43,  116,  432,
   19,   47,  229,  178,   20,  126,   85,  337,  305,  303,
  153,  695,  155,  697,  698,  130,  227,  219,  605,  119,
  220,  108,  172,  173,   21,  175,  149,  304,  201,  201,
  174,   32,   56,  302,  322,  450,  755,  220,   97,  224,
   99,  323,   58,  179,  504,  159,  634,   17,   18,  638,
   41,   42,   82,  446,  483,  725,   59,  150,   60,   70,
  589,  590,  429,  592,  430,   61,  181,  212,  154,  215,
  156,   71,   64,   65,   44,  426,  296,   79,  296,   63,
   71,  198,  182,   62,   63,  347,   82,  350,  485,  148,
  116,  105,  457,  426,  359,   63,  457,  447,   81,  457,
  448,  198,  129,   72,   88,   45,   82,   88,  457,  126,
   85,  237,  604,  183,  184,  448,  129,  185,  186,  187,
  188,  189,  190,   82,   72,  112,   63,   63,  112,  316,
   63,   63,   63,   63,   63,   63,  129,   43,   22,  692,
   91,  655,  693,  657,  658,   94,  726,   23,  701,  710,
  171,  101,   44,   92,   90,   24,   25,   26,   27,   28,
  342,  345,  346,  348,  349,  351,  352,  354,  355,  356,
  357,  358,  361,  363,  364,  365,  366,  105,   95,  120,
  370,  372,  329,   45,  378,  329,  104,  727,  223,  732,
  223,  206,  206,  206,  423,  737,  516,  107,  448,  419,
  420,   47,   82,   32,  489,  422,  171,  522,  110,  699,
   72,  433,  112,   73,  756,   43,   82,  710,  518,  421,
  532,  353,  122,  133,  134,  135,   80,  136,  137,  138,
  162,  139,   62,   63,   62,   63,   64,   65,  140,  141,
  113,  106,   82,  113,  563,  142,  299,  300,  301,   72,
  163,   35,  143,  203,   35,  210,  456,   82,  210,  568,
  460,  129,   82,  463,  746,  202,  466,  210,  326,   72,
  597,   82,  472,  473,  204,  475,  598,   82,   22,  599,
   82,  751,   82,  205,  484,  233,  335,   23,  339,  216,
  488,  217,  600,  234,  602,   24,   25,   26,   27,   28,
   72,  752,   82,  753,   82,  208,  209,  367,  368,  369,
  218,  228,  374,  241,  502,  379,  380,  381,  382,  383,
  384,  385,  386,  387,  388,  389,  390,  298,  306,  144,
  515,  503,   72,   82,  307,   47,   47,   47,  309,  310,
  512,  514,  319,  145,  146,  147,  437,  519,  520,  521,
  327,  375,   64,  510,  511,  513,   22,  334,  434,  105,
  105,  360,  424,  105,  105,   23,  105,  198,  431,  438,
  661,  435,  662,   24,   25,   26,   27,   28,  151,  105,
  105,   33,   34,  436,   35,   36,   37,   38,   39,   40,
  439,  440,   44,  652,  441,  442,  443,  444,  564,  452,
  564,  105,  445,  570,  571,  572,  573,  574,  575,  576,
  577,  578,  579,  580,  581,  454,  486,  480,  481,  482,
  487,  490,  491,   45,  492,   47,  720,   47,   47,  493,
  494,  495,  595,  496,  497,  211,  296,  214,  498,  499,
  523,  500,  524,  591,  501,  593,  594,  527,  107,  201,
  329,  226,  525,  526,  533,   43,  535,  377,  694,  336,
  537,  696,  538,  540,  541,  543,   72,   72,   72,  544,
   72,   72,   72,  546,   72,  547,  564,  548,   82,  564,
  213,   72,   72,  549,  550,  553,  555,   64,   72,  556,
  557,  201,  562,  201,  308,   72,  149,  312,  558,  566,
  649,  585,  724,   64,  586,  587,  606,  601,   47,  106,
  106,  528,  603,  106,  106,  608,  106,  611,  614,  316,
  617,  201,  623,  559,  560,  561,  659,  150,  624,  106,
  106,  626,  569,  631,   64,   64,  632,  633,   64,   64,
   64,   64,   64,   64,   82,  637,  677,  639,  133,  134,
  135,  106,  136,  137,  138,  641,  139,  642,  643,  148,
  689,  596,  149,  714,  715,  644,  504,  660,  663,   44,
  142,  676,   72,  678,  680,   21,  679,  143,  316,  687,
  702,  705,  706,  707,  708,  710,   72,   72,   72,  711,
  713,  728,  729,  150,  734,  733,   22,  735,  736,   18,
   45,  738,    1,  754,  743,   23,  649,   72,  237,  744,
   94,  640,   95,   24,   25,   26,   27,   28,  745,  453,
   96,   97,   57,  103,  455,  148,  222,  458,  459,  646,
  461,  462,   43,  464,  465,   70,  467,  468,  469,  470,
  471,  221,  321,  474,   19,  476,  477,  478,  479,  236,
  318,  742,  111,   83,  716,  238,  747,  748,  749,   33,
   34,  691,   35,   36,   37,   38,   39,   40,  717,  718,
  719,  230,  730,  427,  674,  675,  428,  451,   20,  362,
  723,   73,  181,   62,   63,  181,  645,   64,   65,   66,
   30,   31,  530,  133,  134,  135,    0,  136,  137,  138,
  704,  139,  507,  181,    0,    0,    0,    0,  140,  141,
    0,    0,    0,    0,    0,  142,  107,  107,    0,    0,
  107,  107,  143,  107,    0,    0,    0,  133,  134,  135,
  712,  136,  137,  138,  181,  139,  107,  107,  536,    0,
    0,    0,  539,    0,    0,  542,  328,    0,  545,  142,
    0,    0,    0,    0,  551,  552,  143,  554,  107,  133,
  134,  135,    0,  136,  137,  138,  181,  139,    0,   72,
    0,    0,    0,   22,  140,  141,    0,  739,  740,  741,
    0,  142,   23,    0,  582,  583,  584,    0,  143,  750,
   24,   25,   26,   27,   28,    0,    0,    0,    0,  144,
    0,    0,   33,   34,    0,   35,   36,   37,   38,   39,
   40,   82,    0,  145,  146,  147,    0,  607,    0,  609,
  610,    0,  612,  613,    0,  615,  616,    0,  618,  619,
  620,  621,  622,    0,    0,  625,    0,  627,  628,  629,
  630,  340,  341,   21,   21,    0,  635,   21,   21,    0,
   21,    0,    0,    0,   25,    1,    2,   72,    0,    3,
    4,    0,    5,   21,   21,  144,    0,   18,   18,    0,
    0,   18,   18,    0,   18,    6,    7,    0,    0,  145,
  146,  147,    0,    0,    0,   21,    0,   18,   18,    0,
  667,    0,    0,  668,    0,    0,  669,    8,    0,  670,
    0,    0,    0,    0,    0,  671,  672,    0,  673,   18,
    0,    0,   19,   19,  181,  181,   19,   19,    0,   19,
    0,    0,    0,    0,  684,  685,  686,    0,    0,    0,
    0,  690,   19,   19,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,   72,   20,   20,    0,    0,
   20,   20,    0,   20,   19,    0,    0,    0,    0,    0,
    0,  181,  181,  181,    0,    0,   20,   20,    0,    0,
  181,    0,  181,  722,    0,  181,  181,  181,  181,  181,
  181,  181,  181,  181,  181,    0,  181,  181,   20,  181,
  181,  181,  181,  181,  181,  181,    0,    0,  181,  181,
  181,  181,  296,  296,  181,    0,    0,    0,  181,  181,
  181,  181,  181,  181,  181,  181,  181,  181,  181,  181,
  181,    0,  181,    0,    0,    0,  133,  134,  135,    0,
  136,  137,  138,  181,  139,    0,   72,    0,    0,    0,
    0,    0,    0,    0,  181,  181,  181,  181,  142,  296,
  296,  296,    0,    0,    0,  143,    0,    0,  296,    0,
  296,    0,    0,  296,  296,  296,  296,  296,  296,  296,
  296,  296,  296,    0,  296,  296,    0,  296,  296,  296,
  296,  296,  296,  296,    0,    0,  296,  296,  296,  296,
  301,  301,  296,    0,    0,    0,  296,  296,  296,  296,
  296,    0,  296,  296,  296,  296,  296,  296,  296,    0,
  296,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  296,   25,   25,   72,    0,   25,   25,    0,   25,
    0,    0,  296,  296,  296,  296,    0,  301,  301,  301,
    0,    0,   25,   25,    0,    0,  301,    0,  301,    0,
    0,  301,  301,  301,  301,  301,  301,  301,  301,  301,
  301,    0,  301,  301,   25,  301,  301,  301,  301,  301,
  301,  301,   44,    0,  301,  301,  301,  301,  286,  286,
  301,    0,    0,    0,  301,  301,  301,  301,  301,    0,
  301,  301,  301,  301,  301,  301,  301,    0,  301,    0,
    0,    0,    0,   45,    0,    0,    0,    0,    0,  301,
   22,   22,   72,    0,   22,   22,    0,   22,    0,    0,
  301,  301,  301,  301,    0,  286,  286,  286,    0,    0,
   22,   22,    0,    0,  286,   43,  286,    0,    0,  286,
  286,  286,  286,  286,  286,  286,  286,  286,  286,    0,
  286,  286,   22,  286,  286,  286,  286,  286,  286,  286,
   44,    0,  286,  286,  286,  286,    0,    0,  286,  267,
  267,    0,  286,  286,  286,  286,  286,    0,  286,  286,
  286,  286,  286,  286,  286,    0,  286,    0,    0,    0,
    0,   45,    0,    0,    0,    0,    0,  286,    0,    0,
   72,    0,    0,    0,    0,    0,    0,    0,  286,  286,
  286,  286,    0,    0,    0,    0,  267,  267,  267,    0,
    0,    0,    0,   43,    0,  267,    0,  267,    0,    0,
  267,  267,  267,  267,  267,  267,  267,  267,  267,  267,
    0,  267,  267,    0,  267,  267,  267,  267,  267,  267,
  267,    0,    0,  267,  267,  267,  267,  264,  264,  267,
    0,    0,    0,  267,  267,  267,  267,  267,    0,  267,
  267,  267,  267,  267,  267,  267,   22,  267,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,  267,    0,
    0,   72,    0,   24,   25,   26,   27,   28,    0,  267,
  267,  267,  267,    0,  264,  264,  264,    0,    0,    0,
    0,    0,    0,  264,  371,  264,    0,    0,  264,  264,
  264,  264,  264,  264,  264,  264,  264,  264,    0,  264,
  264,    0,  264,  264,  264,  264,  264,  264,  264,    0,
    0,  264,  264,  264,  264,  265,  265,  264,    0,    0,
    0,  264,  264,  264,  264,  264,    0,  264,  264,  264,
  264,  264,  264,  264,  164,  264,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,  264,    0,    0,   72,
    0,   24,   25,   26,   27,   28,    0,  264,  264,  264,
  264,    0,  265,  265,  265,    0,    0,    0,    0,    0,
    0,  265,  373,  265,    0,    0,  265,  265,  265,  265,
  265,  265,  265,  265,  265,  265,    0,  265,  265,    0,
  265,  265,  265,  265,  265,  265,  265,   44,    0,  265,
  265,  265,  265,  266,  266,  265,    0,    0,    0,  265,
  265,  265,  265,  265,    0,  265,  265,  265,  265,  265,
  265,  265,    0,  265,    0,    0,    0,    0,   45,    0,
    0,    0,    0,    0,  265,    0,    0,   72,    0,    0,
    0,    0,    0,    0,    0,  265,  265,  265,  265,    0,
  266,  266,  266,    0,    0,    0,    0,    0,    0,  266,
   43,  266,   75,    0,  266,  266,  266,  266,  266,  266,
  266,  266,  266,  266,    0,  266,  266,    0,  266,  266,
  266,  266,  266,  266,  266,   44,    0,  266,  266,  266,
  266,    0,    0,  266,  298,  298,    0,  266,  266,  266,
  266,  266,    0,  266,  266,  266,  266,  266,  266,  266,
    0,  266,    0,    0,    0,    0,   45,    0,    0,    0,
    0,    0,  266,    0,    0,   72,    0,    0,    0,    0,
    0,    0,    0,  266,  266,  266,  266,    0,    0,    0,
    0,  298,  298,  298,    0,    0,    0,    0,   43,    0,
  298,    0,  298,    0,    0,  298,  298,  298,  298,  298,
  298,  298,  298,  298,  298,    0,  298,  298,    0,  298,
  298,  298,  298,  298,  298,  298,    0,    0,  298,  298,
  298,  298,  290,  290,  298,    0,    0,    0,  298,  298,
  298,  298,  298,    0,  298,  298,  298,  298,  298,  298,
  298,   22,  298,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,  298,    0,    0,   72,    0,   24,   25,
   26,   27,   28,    0,  298,  298,  298,  298,    0,  290,
  290,  290,    0,    0,    0,    0,    0,    0,  290,    0,
  290,    0,    0,  290,  290,  290,  290,  290,  290,  290,
  290,  290,  290,    0,  290,  290,    0,  290,  290,  290,
  290,  290,  290,  290,    0,    0,  290,  290,  290,  290,
  282,  282,  290,    0,    0,    0,  290,  290,  290,  290,
  290,    0,  290,  290,  290,  290,  290,  290,  290,   22,
  290,    0,    0,    0,    0,    0,    0,    0,   23,  688,
    0,  290,    0,   24,    0,    0,   24,   25,   26,   27,
   28,    0,  290,  290,  290,  290,    0,  282,  282,  282,
    0,    0,    0,    0,    0,    0,  282,    0,  282,    0,
    0,  282,  282,  282,  282,  282,  282,  282,  282,  282,
  282,    0,  282,  282,    0,  282,  282,  282,  282,  282,
  282,  282,    0,    0,  282,  282,  282,  282,  273,  273,
  282,    0,    0,    0,  282,  282,  282,  282,  282,    0,
  282,  282,  282,  282,  282,  282,  282,    0,  282,    0,
    0,    0,  133,  134,  135,    0,  136,  137,  138,  282,
  139,   82,    0,  311,    0,    0,    0,    0,    0,    0,
  282,  282,  282,  282,  142,  273,  273,  273,    0,  149,
    0,  143,    0,    0,  273,    0,  273,    0,    0,  273,
  273,  273,  273,  273,  273,  273,  273,  273,  273,    0,
  273,  273,    0,  273,  273,  273,  273,  273,  273,  273,
  150,    0,  273,  273,  273,  273,    0,    0,  273,  246,
  246,   82,  273,  273,  273,  273,  273,    0,  273,  273,
  273,  273,  273,  273,  273,    0,  273,    0,    0,  149,
    0,    0,  148,    0,    0,    0,    0,  273,  391,  392,
  393,  394,  395,  396,  397,  398,  399,  400,  273,  273,
  273,  273,    0,    0,    0,    0,  246,  246,  246,    0,
  150,    0,    0,    0,    0,  246,  149,  246,    0,    0,
  246,  246,  246,  246,  246,  246,  246,  246,  246,  246,
    0,  246,  246,    0,  246,  246,  246,  246,  246,  246,
  246,    0,  148,  246,  246,  246,  246,  150,    0,  246,
    0,    0,    0,  246,  246,  246,  246,  246,    0,  246,
  246,  246,  246,  246,  246,  246,    0,  246,    0,    0,
    0,    0,    0,  149,    0,    0,    0,    0,  246,  148,
  125,   24,   24,    0,    0,   24,   24,    0,   24,  246,
  246,  246,  246,    0,    0,  181,    0,    0,    0,   44,
    0,   24,   24,    0,  150,    0,  115,    0,    0,    0,
    0,  182,    0,    0,    0,    0,  133,  134,  135,    0,
  136,  137,  138,   24,  139,   44,    0,    0,    0,    0,
   45,  140,  141,    0,    0,  731,  148,    0,  142,    0,
    0,    0,  183,  184,    0,  143,  185,  186,  187,  188,
  189,  190,    0,    0,    0,    0,   45,    0,    0,    0,
    0,    0,   43,    0,  133,  134,  135,    0,  136,  137,
  138,    0,  139,    0,    0,    0,  133,  134,  135,    0,
  136,  137,  138,  328,  139,    0,  142,    0,   43,  425,
    0,  140,  141,  143,    0,    0,    0,    0,  142,    0,
    0,    0,    0,    0,    0,  143,    0,    0,    0,    0,
    0,    0,    0,  133,  134,  135,    0,  136,  137,  138,
    0,  139,  144,    0,    0,    0,    0,    0,  140,  141,
    0,    0,    0,    0,    0,  142,  145,  146,  147,    0,
    0,    0,  143,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   33,   34,    0,   35,
   36,   37,   38,   39,   40,    0,    0,    0,    0,    0,
  133,  134,  135,    0,  136,  137,  138,  449,  139,    0,
    0,    0,  144,    0,    0,  140,  141,    0,    0,    0,
    0,    0,  142,    0,    0,    0,  145,  146,  147,  143,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,  123,    0,  144,
   24,   25,   26,   27,   28,    0,    0,    0,    0,   22,
    0,    0,  124,  145,  146,  147,    0,  242,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,  133,  134,  135,    0,  136,  137,  138,  114,  139,
    0,    0,    0,    0,    0,  517,    0,    0,    0,    0,
  328,    0,    0,  142,    0,    0,  144,    0,    0,    0,
  143,    0,    0,    0,  243,  244,  245,    0,    0,    0,
  145,  146,  147,  246,    0,  247,    0,    0,  248,  249,
  250,  251,  252,  253,  254,  255,  256,  257,    0,  258,
  259,    0,  260,  261,  262,  263,  264,  265,  266,    0,
    0,  267,  268,  269,  270,  242,    0,  271,    0,    0,
    0,  272,  273,  274,  275,  276,    0,  277,  278,  279,
  280,  281,  282,  283,    0,  284,    0,    0,    0,    0,
    0,    0,    0,  531,    0,    0,  285,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  286,  287,  288,
  289,    0,  243,  244,  245,    0,    0,    0,    0,    0,
    0,  246,    0,  247,    0,    0,  248,  249,  250,  251,
  252,  253,  254,  255,  256,  257,    0,  258,  259,    0,
  260,  261,  262,  263,  264,  265,  266,    0,    0,  267,
  268,  269,  270,  242,    0,  271,    0,    0,    0,  272,
  273,  274,  275,  276,    0,  277,  278,  279,  280,  281,
  282,  283,    0,  284,    0,    0,    0,    0,    0,    0,
    0,  193,    0,    0,  285,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  286,  287,  288,  289,    0,
  243,  244,  245,    0,    0,    0,    0,    0,    0,  246,
    0,  247,    0,    0,  248,  249,  250,  251,  252,  253,
  254,  255,  256,  257,    0,  258,  259,    0,  260,  261,
  262,  263,  264,  265,  266,    0,    0,  267,  268,  269,
  270,  242,    0,  271,    0,    0,    0,  272,  273,  274,
  275,  276,    0,  277,  278,  279,  280,  281,  282,  283,
    0,  284,    0,    0,    0,    0,    0,    0,    0,  191,
    0,    0,  285,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  286,  287,  288,  289,    0,  243,  244,
  245,    0,    0,    0,    0,    0,    0,  246,    0,  247,
    0,    0,  248,  249,  250,  251,  252,  253,  254,  255,
  256,  257,    0,  258,  259,    0,  260,  261,  262,  263,
  264,  265,  266,    0,    0,  267,  268,  269,  270,  193,
    0,  271,    0,    0,    0,  272,  273,  274,  275,  276,
    0,  277,  278,  279,  280,  281,  282,  283,    0,  284,
    0,    0,    0,    0,    0,    0,    0,  194,    0,    0,
  285,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  286,  287,  288,  289,    0,  193,  193,  193,    0,
    0,    0,    0,    0,    0,  193,    0,  193,    0,    0,
  193,  193,  193,  193,  193,  193,  193,  193,  193,  193,
    0,  193,  193,    0,  193,  193,  193,  193,  193,  193,
  193,    0,    0,  193,  193,  193,  193,  191,    0,  193,
    0,    0,    0,  193,  193,  193,  193,  193,    0,  193,
  193,  193,  193,  193,  193,  193,    0,  193,    0,    0,
    0,    0,    0,    0,    0,  192,    0,    0,  193,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  193,
  193,  193,  193,    0,  191,  191,  191,    0,    0,    0,
    0,    0,    0,  191,    0,  191,    0,    0,  191,  191,
  191,  191,  191,  191,  191,  191,  191,  191,    0,  191,
  191,    0,  191,  191,  191,  191,  191,  191,  191,    0,
    0,  191,  191,  191,  191,  194,    0,  191,    0,    0,
    0,  191,  191,  191,  191,  191,    0,  191,  191,  191,
  191,  191,  191,  191,   44,  191,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  191,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  191,  191,  191,
  191,    0,  194,  194,  194,   45,    0,    0,    0,    0,
    0,  194,    0,  194,    0,    0,  194,  194,  194,  194,
  194,  194,  194,  194,  194,  194,    0,  194,  194,    0,
  194,  194,  194,  194,  194,  194,  194,   43,    0,  194,
  194,  194,  194,  192,    0,  194,    0,    0,    0,  194,
  194,  194,  194,  194,    0,  194,  194,  194,  194,  194,
  194,  194,   44,  194,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  194,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  194,  194,  194,  194,    0,
  192,  192,  192,   45,    0,    0,    0,    0,    0,  192,
   44,  192,    0,    0,  192,  192,  192,  192,  192,  192,
  192,  192,  192,  192,    0,  192,  192,    0,  192,  192,
  192,  192,  192,  192,  192,   43,    0,  192,  192,  192,
  192,   45,    0,  192,    0,    0,    0,  192,  192,  192,
  192,  192,    0,  192,  192,  192,  192,  192,  192,  192,
    0,  192,    0,  101,    0,    0,    0,    0,    0,    0,
   44,    0,  192,   43,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  192,  192,  192,  192,    0,   22,    0,
    0,    0,    0,    0,  101,    0,    0,   23,    0,    0,
    0,   45,    0,    0,    0,   24,   25,   26,   27,   28,
  102,    0,    0,    0,    0,   44,   29,    0,    0,    0,
    0,   30,   31,   32,   33,   34,  101,   35,   36,   37,
   38,   39,   40,   43,    0,    0,    0,    0,    0,  648,
    0,  102,   41,   42,    0,    0,   45,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   44,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  102,   44,    0,   22,    0,   43,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,   45,
    0,    0,    0,   24,   25,   26,   27,   28,   44,    0,
    0,    0,    0,    0,    0,   45,    0,    0,    0,    0,
    0,   32,   33,   34,   22,   35,   36,   37,   38,   39,
   40,   43,    0,   23,    0,    0,    0,    0,    0,   45,
    0,   24,   25,   26,   27,   28,    0,   43,   44,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   33,   34,    0,   35,   36,   37,   38,   39,   40,    0,
    0,   43,    0,  167,    0,    0,   44,  101,    0,   45,
    0,    0,    0,    0,   22,    0,  101,    0,    0,    0,
    0,   44,    0,   23,  101,  101,  101,  101,  101,    0,
    0,   24,   25,   26,   27,   28,   44,   45,    0,    0,
    0,   43,   54,  101,  101,    0,  101,  101,  101,  101,
  101,  101,   45,    0,  102,    0,    0,    0,    0,   22,
    0,    0,    0,  102,    0,    0,    0,   45,   23,   43,
   44,  102,  102,  102,  102,  102,   24,   25,   26,   27,
   28,    0,    0,   44,   43,    0,    0,    0,  114,    0,
  102,  102,    0,  102,  102,  102,  102,  102,  102,   43,
    0,   45,   22,  703,    0,    0,    0,    0,    0,   44,
    0,   23,    0,    0,   45,    0,  647,    0,   22,   24,
   25,   26,   27,   28,    0,    0,    0,   23,    0,    0,
    0,    0,  123,   43,    0,   24,   25,   26,   27,   28,
   45,    0,  164,  165,    0,    0,   43,  124,    0,    0,
    0,   23,  166,    0,    0,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,    0,
    0,    0,   43,    0,    0,  133,  134,  135,  313,  136,
  137,  138,   22,  139,    0,    0,    0,    0,    0,    0,
    0,   23,  314,    0,  315,    0,    0,  142,    0,   24,
   25,   26,   27,   28,  143,    0,    0,    0,    0,    0,
  164,  165,  402,  403,    0,    0,    0,    0,    0,   23,
  166,    0,    0,    0,    0,  164,  324,   24,   25,   26,
   27,   28,    0,    0,   23,  325,    0,    0,    0,    0,
   22,    0,   24,   25,   26,   27,   28,    0,    0,   23,
    0,    0,    0,    0,  647,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,   22,    0,    0,
    0,   24,   25,   26,   27,   28,   23,    0,    0,    0,
    0,    0,    0,    0,   24,   25,   26,   27,   28,  281,
  281,    0,    0,  164,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,    0,    0,    0,
   24,   25,   26,   27,   28,  404,  405,  406,  407,    0,
    0,    0,    0,    0,  408,  409,  410,  411,  412,  413,
  414,  415,  416,  417,    0,    0,  281,  281,  281,    0,
    0,    0,    0,    0,    0,  281,    0,  281,    0,    0,
  281,  281,  281,  281,  281,  281,  281,  281,  281,  281,
    0,  281,  281,    0,  281,  281,  281,  281,  281,  281,
  281,    0,    0,  281,  281,  281,  281,  247,  247,  281,
    0,    0,    0,  281,  281,  281,  281,  281,    0,  281,
  281,  281,  281,  281,  281,  281,    0,  281,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  281,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  281,
  281,  281,  281,    0,  247,  247,  247,    0,    0,    0,
    0,    0,    0,  247,    0,  247,    0,    0,  247,  247,
  247,  247,  247,  247,  247,  247,  247,  247,    0,  247,
  247,    0,  247,  247,  247,  247,  247,  247,  247,    0,
    0,  247,  247,  247,  247,  250,  250,  247,    0,    0,
    0,  247,  247,  247,  247,  247,    0,  247,  247,  247,
  247,  247,  247,  247,    0,  247,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  247,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  247,  247,  247,
  247,    0,  250,  250,  250,    0,    0,    0,    0,    0,
    0,  250,    0,  250,    0,    0,  250,  250,  250,  250,
  250,  250,  250,  250,  250,  250,    0,  250,  250,    0,
  250,  250,  250,  250,  250,  250,  250,    0,    0,  250,
  250,  250,  250,  255,  255,  250,    0,    0,    0,  250,
  250,  250,  250,  250,    0,  250,  250,  250,  250,  250,
  250,  250,    0,  250,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  250,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  250,  250,  250,  250,    0,
  255,  255,  255,    0,    0,    0,    0,    0,    0,  255,
    0,  255,    0,    0,  255,  255,  255,  255,  255,  255,
  255,  255,  255,  255,    0,  255,  255,    0,  255,  255,
  255,  255,  255,  255,  255,    0,    0,  255,  255,  255,
  255,  269,  269,  255,    0,    0,    0,  255,  255,  255,
  255,  255,    0,  255,  255,  255,  255,  255,  255,  255,
    0,  255,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  255,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  255,  255,  255,  255,    0,  269,  269,
  269,    0,    0,    0,    0,    0,    0,  269,    0,  269,
    0,    0,  269,  269,  269,  269,  269,  269,  269,  269,
  269,  269,    0,  269,  269,    0,  269,  269,  269,  269,
  269,  269,  269,    0,    0,  269,  269,  269,  269,  251,
  251,  269,    0,    0,    0,  269,  269,  269,  269,  269,
    0,  269,  269,  269,  269,  269,  269,  269,    0,  269,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  269,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  269,  269,  269,  269,    0,  251,  251,  251,    0,
    0,    0,    0,    0,    0,  251,    0,  251,    0,    0,
  251,  251,  251,  251,  251,  251,  251,  251,  251,  251,
    0,  251,  251,    0,  251,  251,  251,  251,  251,  251,
  251,    0,    0,  251,  251,  251,  251,  252,  252,  251,
    0,    0,    0,  251,  251,  251,  251,  251,    0,  251,
  251,  251,  251,  251,  251,  251,    0,  251,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  251,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  251,
  251,  251,  251,    0,  252,  252,  252,    0,    0,    0,
    0,    0,    0,  252,    0,  252,    0,    0,  252,  252,
  252,  252,  252,  252,  252,  252,  252,  252,    0,  252,
  252,    0,  252,  252,  252,  252,  252,  252,  252,    0,
    0,  252,  252,  252,  252,  253,  253,  252,    0,    0,
    0,  252,  252,  252,  252,  252,    0,  252,  252,  252,
  252,  252,  252,  252,    0,  252,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  252,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  252,  252,  252,
  252,    0,  253,  253,  253,    0,    0,    0,    0,    0,
    0,  253,    0,  253,    0,    0,  253,  253,  253,  253,
  253,  253,  253,  253,  253,  253,    0,  253,  253,    0,
  253,  253,  253,  253,  253,  253,  253,    0,    0,  253,
  253,  253,  253,  270,  270,  253,    0,    0,    0,  253,
  253,  253,  253,  253,    0,  253,  253,  253,  253,  253,
  253,  253,    0,  253,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  253,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  253,  253,  253,  253,    0,
  270,  270,  270,    0,    0,    0,    0,    0,    0,  270,
    0,  270,    0,    0,  270,  270,  270,  270,  270,  270,
  270,  270,  270,  270,    0,  270,  270,    0,  270,  270,
  270,  270,  270,  270,  270,    0,    0,  270,  270,  270,
  270,  254,  254,  270,    0,    0,    0,  270,  270,  270,
  270,  270,    0,  270,  270,  270,  270,  270,  270,  270,
    0,  270,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  270,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  270,  270,  270,  270,    0,  254,  254,
  254,    0,    0,    0,    0,    0,    0,  254,    0,  254,
    0,    0,  254,  254,  254,  254,  254,  254,  254,  254,
  254,  254,    0,  254,  254,    0,  254,  254,  254,  254,
  254,  254,  254,    0,    0,  254,  254,  254,  254,  242,
    0,  254,    0,    0,    0,  254,  254,  254,  254,  254,
    0,  254,  254,  254,  254,  254,  254,  254,    0,  254,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  254,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  254,  254,  254,  254,    0,  243,  244,  245,    0,
    0,    0,    0,    0,    0,  246,    0,  247,    0,    0,
  248,  249,  250,  251,  252,  253,  254,  255,  256,  257,
    0,  258,  259,    0,  260,  261,  262,  263,  264,  265,
  266,    0,    0,  267,  268,  269,  270,  197,    0,  271,
    0,    0,    0,  272,  273,  274,  275,  276,    0,  277,
  278,  279,  280,  281,  282,  283,    0,  284,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  285,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  286,
  287,  288,  289,    0,  197,  197,  197,    0,    0,    0,
    0,    0,    0,  197,    0,  197,    0,    0,  197,  197,
  197,  197,  197,  197,  197,  197,  197,  197,    0,  197,
  197,    0,  197,  197,  197,  197,  197,  197,  197,    0,
    0,  197,  197,  197,  197,  198,    0,  197,    0,    0,
    0,  197,  197,  197,  197,  197,    0,  197,  197,  197,
  197,  197,  197,  197,    0,  197,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  197,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  197,  197,  197,
  197,    0,  198,  198,  198,    0,    0,    0,    0,    0,
    0,  198,    0,  198,    0,    0,  198,  198,  198,  198,
  198,  198,  198,  198,  198,  198,    0,  198,  198,    0,
  198,  198,  198,  198,  198,  198,  198,    0,    0,  198,
  198,  198,  198,  199,    0,  198,    0,    0,    0,  198,
  198,  198,  198,  198,    0,  198,  198,  198,  198,  198,
  198,  198,    0,  198,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  198,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  198,  198,  198,  198,    0,
  199,  199,  199,    0,    0,    0,    0,    0,    0,  199,
    0,  199,    0,    0,  199,  199,  199,  199,  199,  199,
  199,  199,  199,  199,    0,  199,  199,    0,  199,  199,
  199,  199,  199,  199,  199,    0,    0,  199,  199,  199,
  199,  200,    0,  199,    0,    0,    0,  199,  199,  199,
  199,  199,    0,  199,  199,  199,  199,  199,  199,  199,
    0,  199,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  199,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  199,  199,  199,  199,    0,  200,  200,
  200,    0,    0,    0,    0,    0,    0,  200,    0,  200,
    0,    0,  200,  200,  200,  200,  200,  200,  200,  200,
  200,  200,    0,  200,  200,    0,  200,  200,  200,  200,
  200,  200,  200,    0,    0,  200,  200,  200,  200,    0,
    0,  200,    0,    0,    0,  200,  200,  200,  200,  200,
    0,  200,  200,  200,  200,  200,  200,  200,    0,  200,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  200,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  200,  200,  200,  200,  247,    0,    0,  248,  249,
  250,  251,  252,  253,  254,  255,  256,  257,    0,  258,
  259,    0,  260,  261,  262,  263,  264,  265,  266,    0,
    0,  267,  268,  269,  270,    0,    0,  271,    0,    0,
    0,  272,  273,  274,  275,  276,    0,  277,  278,  279,
  280,  281,  282,  283,    0,  284,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  285,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  286,  287,  288,
  289,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  445,   40,    6,   60,  123,  121,  123,  131,   41,
  123,  125,   44,  290,   60,  130,   33,    6,    7,  454,
   44,  239,  660,  123,  125,  589,  590,   40,  592,   44,
  123,   42,   44,  216,   48,  218,   43,  276,   44,   44,
   44,  280,  680,   50,   51,   91,  197,   54,   51,   33,
  487,  234,   61,   90,   40,  332,   41,  180,   44,   44,
   67,   50,   51,  178,  179,   54,   40,  123,  266,   83,
   44,   69,   70,   80,  197,   42,   42,  123,   85,  297,
   61,   88,   93,  120,   61,   92,   40,  285,   93,   44,
   97,  655,   99,  657,  658,   93,   62,   41,  533,   88,
   44,  125,  109,  110,   61,  112,   60,   62,  231,  232,
  125,  299,  309,  125,   41,  333,  754,   44,   68,  125,
   70,  125,  272,  121,   91,  364,  563,  270,  271,  566,
  318,  319,   42,  316,   44,  699,  260,   91,   61,  125,
  510,  511,  293,  513,  295,   61,  260,  154,   98,  156,
  100,  125,  294,  295,   60,  432,  274,  257,  274,  260,
  280,  274,  276,  290,  291,  249,   42,  251,   44,  123,
  177,    0,  343,  450,  258,  276,  347,   41,  257,  350,
   44,  274,  309,   40,   41,   91,   42,   44,  359,  196,
   40,  194,   41,  307,  308,   44,  309,  311,  312,  313,
  314,  315,  316,   42,   40,   41,  307,  308,   44,  216,
  311,  312,  313,  314,  315,  316,  309,  123,  264,   41,
   61,  591,   44,  593,  594,  260,   41,  273,  663,   44,
  107,  287,   60,   40,  272,  281,  282,  283,  284,  285,
  247,  248,  249,  250,  251,  252,  253,  254,  255,  256,
  257,  258,  259,  260,  261,  262,  263,  274,  260,  272,
  267,  268,  445,   91,  271,  448,   40,  702,  274,  713,
  274,  148,  149,  150,  288,   41,  427,  123,   44,  286,
  287,  288,   42,  299,   44,  288,  163,  438,  267,  659,
  274,  298,  267,  277,   41,  123,   42,   44,   44,  288,
  451,  347,  123,  257,  258,  259,  123,  261,  262,  263,
   40,  265,  290,  291,  290,  291,  294,  295,  272,  273,
   41,    0,   42,   44,   44,  279,  203,  204,  205,   40,
  123,   41,  286,   40,   44,   41,  343,   42,   44,   44,
  347,  309,   42,  350,   44,  365,  353,   44,  225,   60,
   41,   42,  359,  360,   40,  362,   41,   42,  264,   41,
   42,   41,   42,   40,  371,   61,  243,  273,  245,   58,
  377,   58,  523,   40,  525,  281,  282,  283,  284,  285,
   91,   41,   42,   41,   42,  149,  150,  264,  265,  266,
   58,   62,  269,  274,  401,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,   40,  364,  363,
  424,  418,  123,   42,   44,  422,  423,  424,  364,   44,
  423,  424,  274,  377,  378,  379,  303,  434,  435,  436,
  260,  317,  125,  422,  423,  424,  264,   61,  372,  268,
  269,  347,  405,  272,  273,  273,  275,  274,  274,  257,
  601,  372,  603,  281,  282,  283,  284,  285,   96,  288,
  289,  300,  301,  372,  303,  304,  305,  306,  307,  308,
  364,   44,   60,  588,  257,  364,  364,   44,  485,  273,
  487,  310,   40,  490,  491,  492,  493,  494,  495,  496,
  497,  498,  499,  500,  501,   44,  373,   44,   44,   44,
   44,  372,  372,   91,  372,  512,  689,  514,  515,  372,
  372,  372,  515,  372,  372,  153,  274,  155,  372,  372,
  257,  372,  364,  512,  372,  514,  515,  364,    0,  652,
  713,  169,  257,  257,   44,  123,   44,  365,  653,  266,
   44,  656,   44,   44,   44,   44,  257,  258,  259,   44,
  261,  262,  263,   44,  265,   44,  563,   44,   42,  566,
   44,  272,  273,   44,   44,   44,   44,  260,  279,   44,
   44,  694,  364,  696,  212,  286,   60,  215,   44,   44,
  587,   44,  697,  276,   44,   40,   91,  257,  595,  268,
  269,   41,  257,  272,  273,   44,  275,   44,   44,  606,
   44,  724,   44,  480,  481,  482,  595,   91,   44,  288,
  289,   44,  489,   44,  307,  308,   44,  257,  311,  312,
  313,  314,  315,  316,   42,   44,  364,   44,  257,  258,
  259,  310,  261,  262,  263,   44,  265,   44,   44,  123,
  647,  518,   60,  272,  273,   44,   91,   44,   44,   60,
  279,   44,  363,   44,   44,    0,  364,  286,  665,   44,
   44,  364,  257,  364,  257,   44,  377,  378,  379,   93,
   40,  257,  257,   91,   40,  365,  264,   40,   40,    0,
   91,   40,    0,   44,  372,  273,  693,   40,  691,  372,
  272,  568,  272,  281,  282,  283,  284,  285,  372,  337,
  272,  272,   10,   71,  342,  123,  163,  345,  346,  586,
  348,  349,  123,  351,  352,   19,  354,  355,  356,  357,
  358,  162,  220,  361,    0,  363,  364,  365,  366,  191,
  216,  738,   80,   48,  363,  196,  743,  744,  745,  300,
  301,  649,  303,  304,  305,  306,  307,  308,  377,  378,
  379,  177,  710,  292,  631,  632,  292,  334,    0,  347,
  693,   40,   41,  290,  291,   44,  585,  294,  295,  296,
  297,  298,  448,  257,  258,  259,   -1,  261,  262,  263,
  665,  265,  420,   62,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,  268,  269,   -1,   -1,
  272,  273,  286,  275,   -1,   -1,   -1,  257,  258,  259,
  687,  261,  262,  263,   93,  265,  288,  289,  456,   -1,
   -1,   -1,  460,   -1,   -1,  463,  276,   -1,  466,  279,
   -1,   -1,   -1,   -1,  472,  473,  286,  475,  310,  257,
  258,  259,   -1,  261,  262,  263,  125,  265,   -1,   40,
   -1,   -1,   -1,  264,  272,  273,   -1,  734,  735,  736,
   -1,  279,  273,   -1,  502,  503,  504,   -1,  286,  746,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,  363,
   -1,   -1,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,   42,   -1,  377,  378,  379,   -1,  535,   -1,  537,
  538,   -1,  540,  541,   -1,  543,  544,   -1,  546,  547,
  548,  549,  550,   -1,   -1,  553,   -1,  555,  556,  557,
  558,  332,  333,  268,  269,   -1,  564,  272,  273,   -1,
  275,   -1,   -1,   -1,    0,  268,  269,   40,   -1,  272,
  273,   -1,  275,  288,  289,  363,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,  288,  289,   -1,   -1,  377,
  378,  379,   -1,   -1,   -1,  310,   -1,  288,  289,   -1,
  608,   -1,   -1,  611,   -1,   -1,  614,  310,   -1,  617,
   -1,   -1,   -1,   -1,   -1,  623,  624,   -1,  626,  310,
   -1,   -1,  268,  269,  273,  274,  272,  273,   -1,  275,
   -1,   -1,   -1,   -1,  642,  643,  644,   -1,   -1,   -1,
   -1,  649,  288,  289,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,    0,   -1,   -1,   40,  268,  269,   -1,   -1,
  272,  273,   -1,  275,  310,   -1,   -1,   -1,   -1,   -1,
   -1,  320,  321,  322,   -1,   -1,  288,  289,   -1,   -1,
  329,   -1,  331,  691,   -1,  334,  335,  336,  337,  338,
  339,  340,  341,  342,  343,   -1,  345,  346,  310,  348,
  349,  350,  351,  352,  353,  354,   -1,   -1,  357,  358,
  359,  360,  273,  274,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,  377,  378,
  379,   -1,  381,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,  392,  265,   -1,   40,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  403,  404,  405,  406,  279,  320,
  321,  322,   -1,   -1,   -1,  286,   -1,   -1,  329,   -1,
  331,   -1,   -1,  334,  335,  336,  337,  338,  339,  340,
  341,  342,  343,   -1,  345,  346,   -1,  348,  349,  350,
  351,  352,  353,  354,   -1,   -1,  357,  358,  359,  360,
  273,  274,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,  374,  375,  376,  377,  378,  379,   -1,
  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  392,  268,  269,   40,   -1,  272,  273,   -1,  275,
   -1,   -1,  403,  404,  405,  406,   -1,  320,  321,  322,
   -1,   -1,  288,  289,   -1,   -1,  329,   -1,  331,   -1,
   -1,  334,  335,  336,  337,  338,  339,  340,  341,  342,
  343,   -1,  345,  346,  310,  348,  349,  350,  351,  352,
  353,  354,   60,   -1,  357,  358,  359,  360,  273,  274,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,  374,  375,  376,  377,  378,  379,   -1,  381,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  392,
  268,  269,   40,   -1,  272,  273,   -1,  275,   -1,   -1,
  403,  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,
  288,  289,   -1,   -1,  329,  123,  331,   -1,   -1,  334,
  335,  336,  337,  338,  339,  340,  341,  342,  343,   -1,
  345,  346,  310,  348,  349,  350,  351,  352,  353,  354,
   60,   -1,  357,  358,  359,  360,   -1,   -1,  363,  273,
  274,   -1,  367,  368,  369,  370,  371,   -1,  373,  374,
  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,
  405,  406,   -1,   -1,   -1,   -1,  320,  321,  322,   -1,
   -1,   -1,   -1,  123,   -1,  329,   -1,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,   -1,  357,  358,  359,  360,  273,  274,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,  264,  381,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  392,   -1,
   -1,   40,   -1,  281,  282,  283,  284,  285,   -1,  403,
  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  302,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,  274,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,  264,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,  392,   -1,   -1,   40,
   -1,  281,  282,  283,  284,  285,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,  329,  302,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,   60,   -1,  357,
  358,  359,  360,  273,  274,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   -1,  381,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,  392,   -1,   -1,   40,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,
  123,  331,  125,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,   60,   -1,  357,  358,  359,
  360,   -1,   -1,  363,  273,  274,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,  392,   -1,   -1,   40,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,   -1,   -1,
   -1,  320,  321,  322,   -1,   -1,   -1,   -1,  123,   -1,
  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,  338,
  339,  340,  341,  342,  343,   -1,  345,  346,   -1,  348,
  349,  350,  351,  352,  353,  354,   -1,   -1,  357,  358,
  359,  360,  273,  274,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,  374,  375,  376,  377,  378,
  379,  264,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,  392,   -1,   -1,   40,   -1,  281,  282,
  283,  284,  285,   -1,  403,  404,  405,  406,   -1,  320,
  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,
  331,   -1,   -1,  334,  335,  336,  337,  338,  339,  340,
  341,  342,  343,   -1,  345,  346,   -1,  348,  349,  350,
  351,  352,  353,  354,   -1,   -1,  357,  358,  359,  360,
  273,  274,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,  374,  375,  376,  377,  378,  379,  264,
  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,  392,   -1,    0,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  403,  404,  405,  406,   -1,  320,  321,  322,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,   -1,
   -1,  334,  335,  336,  337,  338,  339,  340,  341,  342,
  343,   -1,  345,  346,   -1,  348,  349,  350,  351,  352,
  353,  354,   -1,   -1,  357,  358,  359,  360,  273,  274,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,  374,  375,  376,  377,  378,  379,   -1,  381,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  392,
  265,   42,   -1,   44,   -1,   -1,   -1,   -1,   -1,   -1,
  403,  404,  405,  406,  279,  320,  321,  322,   -1,   60,
   -1,  286,   -1,   -1,  329,   -1,  331,   -1,   -1,  334,
  335,  336,  337,  338,  339,  340,  341,  342,  343,   -1,
  345,  346,   -1,  348,  349,  350,  351,  352,  353,  354,
   91,   -1,  357,  358,  359,  360,   -1,   -1,  363,  273,
  274,   42,  367,  368,  369,  370,  371,   -1,  373,  374,
  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,   60,
   -1,   -1,  123,   -1,   -1,   -1,   -1,  392,  382,  383,
  384,  385,  386,  387,  388,  389,  390,  391,  403,  404,
  405,  406,   -1,   -1,   -1,   -1,  320,  321,  322,   -1,
   91,   -1,   -1,   -1,   -1,  329,   60,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,  123,  357,  358,  359,  360,   91,   -1,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,  392,  123,
   41,  268,  269,   -1,   -1,  272,  273,   -1,  275,  403,
  404,  405,  406,   -1,   -1,  260,   -1,   -1,   -1,   60,
   -1,  288,  289,   -1,   91,   -1,   41,   -1,   -1,   -1,
   -1,  276,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,  310,  265,   60,   -1,   -1,   -1,   -1,
   91,  272,  273,   -1,   -1,   41,  123,   -1,  279,   -1,
   -1,   -1,  307,  308,   -1,  286,  311,  312,  313,  314,
  315,  316,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,  123,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,  276,  265,   -1,  279,   -1,  123,  125,
   -1,  272,  273,  286,   -1,   -1,   -1,   -1,  279,   -1,
   -1,   -1,   -1,   -1,   -1,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,  363,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,  377,  378,  379,   -1,
   -1,   -1,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  300,  301,   -1,  303,
  304,  305,  306,  307,  308,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,  125,  265,   -1,
   -1,   -1,  363,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   -1,   -1,  377,  378,  379,  286,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,  363,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,  264,
   -1,   -1,  293,  377,  378,  379,   -1,  273,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  257,  258,  259,   -1,  261,  262,  263,  293,  265,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
  276,   -1,   -1,  279,   -1,   -1,  363,   -1,   -1,   -1,
  286,   -1,   -1,   -1,  320,  321,  322,   -1,   -1,   -1,
  377,  378,  379,  329,   -1,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,   -1,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,   -1,   -1,  357,
  358,  359,  360,  273,   -1,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  125,   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,  331,   -1,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,   -1,   -1,  357,  358,  359,
  360,  273,   -1,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,  320,  321,
  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,
   -1,   -1,  334,  335,  336,  337,  338,  339,  340,  341,
  342,  343,   -1,  345,  346,   -1,  348,  349,  350,  351,
  352,  353,  354,   -1,   -1,  357,  358,  359,  360,  273,
   -1,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,  374,  375,  376,  377,  378,  379,   -1,  381,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,  404,  405,  406,   -1,  320,  321,  322,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,   -1,  357,  358,  359,  360,  273,   -1,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,   -1,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   60,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   91,   -1,   -1,   -1,   -1,
   -1,  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,  123,   -1,  357,
  358,  359,  360,  273,   -1,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   60,  381,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   91,   -1,   -1,   -1,   -1,   -1,  329,
   60,  331,   -1,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,  123,   -1,  357,  358,  359,
  360,   91,   -1,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
   60,   -1,  392,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,  264,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,  273,   -1,   -1,
   -1,   91,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   60,   -1,   -1,   -1,   -1,   60,  292,   -1,   -1,   -1,
   -1,  297,  298,  299,  300,  301,  123,  303,  304,  305,
  306,  307,  308,  123,   -1,   -1,   -1,   -1,   -1,   41,
   -1,   91,  318,  319,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   60,   -1,  264,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   91,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,  299,  300,  301,  264,  303,  304,  305,  306,  307,
  308,  123,   -1,  273,   -1,   -1,   -1,   -1,   -1,   91,
   -1,  281,  282,  283,  284,  285,   -1,  123,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  300,  301,   -1,  303,  304,  305,  306,  307,  308,   -1,
   -1,  123,   -1,  125,   -1,   -1,   60,  264,   -1,   91,
   -1,   -1,   -1,   -1,  264,   -1,  273,   -1,   -1,   -1,
   -1,   60,   -1,  273,  281,  282,  283,  284,  285,   -1,
   -1,  281,  282,  283,  284,  285,   60,   91,   -1,   -1,
   -1,  123,  292,  300,  301,   -1,  303,  304,  305,  306,
  307,  308,   91,   -1,  264,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   91,  273,  123,
   60,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,   -1,   -1,   60,  123,   -1,   -1,   -1,  293,   -1,
  300,  301,   -1,  303,  304,  305,  306,  307,  308,  123,
   -1,   91,  264,   93,   -1,   -1,   -1,   -1,   -1,   60,
   -1,  273,   -1,   -1,   91,   -1,  278,   -1,  264,  281,
  282,  283,  284,  285,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,  278,  123,   -1,  281,  282,  283,  284,  285,
   91,   -1,  264,  265,   -1,   -1,  123,  293,   -1,   -1,
   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,  274,   -1,  276,   -1,   -1,  279,   -1,  281,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,
  264,  265,  261,  262,   -1,   -1,   -1,   -1,   -1,  273,
  274,   -1,   -1,   -1,   -1,  264,  265,  281,  282,  283,
  284,  285,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
  264,   -1,  281,  282,  283,  284,  285,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  264,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  273,
  274,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  384,  385,  386,  387,   -1,
   -1,   -1,   -1,   -1,  393,  394,  395,  396,  397,  398,
  399,  400,  401,  402,   -1,   -1,  320,  321,  322,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,   -1,  357,  358,  359,  360,  273,  274,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,  274,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,   -1,   -1,  357,
  358,  359,  360,  273,  274,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,  331,   -1,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,   -1,   -1,  357,  358,  359,
  360,  273,  274,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,  320,  321,
  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,
   -1,   -1,  334,  335,  336,  337,  338,  339,  340,  341,
  342,  343,   -1,  345,  346,   -1,  348,  349,  350,  351,
  352,  353,  354,   -1,   -1,  357,  358,  359,  360,  273,
  274,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,  374,  375,  376,  377,  378,  379,   -1,  381,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,  404,  405,  406,   -1,  320,  321,  322,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,   -1,  357,  358,  359,  360,  273,  274,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,  274,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,   -1,   -1,  357,
  358,  359,  360,  273,  274,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,  331,   -1,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,   -1,   -1,  357,  358,  359,
  360,  273,  274,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,  320,  321,
  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,
   -1,   -1,  334,  335,  336,  337,  338,  339,  340,  341,
  342,  343,   -1,  345,  346,   -1,  348,  349,  350,  351,
  352,  353,  354,   -1,   -1,  357,  358,  359,  360,  273,
   -1,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,  374,  375,  376,  377,  378,  379,   -1,  381,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,  404,  405,  406,   -1,  320,  321,  322,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
   -1,  345,  346,   -1,  348,  349,  350,  351,  352,  353,
  354,   -1,   -1,  357,  358,  359,  360,  273,   -1,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
  374,  375,  376,  377,  378,  379,   -1,  381,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
  404,  405,  406,   -1,  320,  321,  322,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,  273,   -1,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,   -1,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,  331,   -1,   -1,  334,  335,  336,  337,
  338,  339,  340,  341,  342,  343,   -1,  345,  346,   -1,
  348,  349,  350,  351,  352,  353,  354,   -1,   -1,  357,
  358,  359,  360,  273,   -1,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,  374,  375,  376,  377,
  378,  379,   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,  404,  405,  406,   -1,
  320,  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,  331,   -1,   -1,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,   -1,  345,  346,   -1,  348,  349,
  350,  351,  352,  353,  354,   -1,   -1,  357,  358,  359,
  360,  273,   -1,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,  374,  375,  376,  377,  378,  379,
   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,  404,  405,  406,   -1,  320,  321,
  322,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,  331,
   -1,   -1,  334,  335,  336,  337,  338,  339,  340,  341,
  342,  343,   -1,  345,  346,   -1,  348,  349,  350,  351,
  352,  353,  354,   -1,   -1,  357,  358,  359,  360,   -1,
   -1,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,  374,  375,  376,  377,  378,  379,   -1,  381,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,  404,  405,  406,  331,   -1,   -1,  334,  335,
  336,  337,  338,  339,  340,  341,  342,  343,   -1,  345,
  346,   -1,  348,  349,  350,  351,  352,  353,  354,   -1,
   -1,  357,  358,  359,  360,   -1,   -1,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,  374,  375,
  376,  377,  378,  379,   -1,  381,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,  404,  405,
  406,
  };

#line 1103 "Iril/IR/IR.jay"

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
  public const int DSO_LOCAL = 318;
  public const int DSO_PREEMPTABLE = 319;
  public const int RET = 320;
  public const int BR = 321;
  public const int SWITCH = 322;
  public const int INDIRECTBR = 323;
  public const int INVOKE = 324;
  public const int RESUME = 325;
  public const int CATCHSWITCH = 326;
  public const int CATCHRET = 327;
  public const int CLEANUPRET = 328;
  public const int UNREACHABLE = 329;
  public const int FNEG = 330;
  public const int ADD = 331;
  public const int NUW = 332;
  public const int NSW = 333;
  public const int FADD = 334;
  public const int SUB = 335;
  public const int FSUB = 336;
  public const int MUL = 337;
  public const int FMUL = 338;
  public const int UDIV = 339;
  public const int SDIV = 340;
  public const int FDIV = 341;
  public const int UREM = 342;
  public const int SREM = 343;
  public const int FREM = 344;
  public const int SHL = 345;
  public const int LSHR = 346;
  public const int EXACT = 347;
  public const int ASHR = 348;
  public const int AND = 349;
  public const int OR = 350;
  public const int XOR = 351;
  public const int EXTRACTELEMENT = 352;
  public const int INSERTELEMENT = 353;
  public const int SHUFFLEVECTOR = 354;
  public const int EXTRACTVALUE = 355;
  public const int INSERTVALUE = 356;
  public const int ALLOCA = 357;
  public const int LOAD = 358;
  public const int STORE = 359;
  public const int FENCE = 360;
  public const int CMPXCHG = 361;
  public const int ATOMICRMW = 362;
  public const int GETELEMENTPTR = 363;
  public const int ALIGN = 364;
  public const int INBOUNDS = 365;
  public const int INRANGE = 366;
  public const int TRUNC = 367;
  public const int ZEXT = 368;
  public const int SEXT = 369;
  public const int FPTRUNC = 370;
  public const int FPEXT = 371;
  public const int TO = 372;
  public const int FPTOUI = 373;
  public const int FPTOSI = 374;
  public const int UITOFP = 375;
  public const int SITOFP = 376;
  public const int PTRTOINT = 377;
  public const int INTTOPTR = 378;
  public const int BITCAST = 379;
  public const int ADDRSPACECAST = 380;
  public const int ICMP = 381;
  public const int EQ = 382;
  public const int NE = 383;
  public const int UGT = 384;
  public const int UGE = 385;
  public const int ULT = 386;
  public const int ULE = 387;
  public const int SGT = 388;
  public const int SGE = 389;
  public const int SLT = 390;
  public const int SLE = 391;
  public const int FCMP = 392;
  public const int OEQ = 393;
  public const int OGT = 394;
  public const int OGE = 395;
  public const int OLT = 396;
  public const int OLE = 397;
  public const int ONE = 398;
  public const int ORD = 399;
  public const int UEQ = 400;
  public const int UNE = 401;
  public const int UNO = 402;
  public const int PHI = 403;
  public const int SELECT = 404;
  public const int CALL = 405;
  public const int TAIL = 406;
  public const int VA_ARG = 407;
  public const int LANDINGPAD = 408;
  public const int CATCHPAD = 409;
  public const int CLEANUPPAD = 410;
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
