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
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type nonglobal_value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type nonglobal_value",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type nonglobal_value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type nonglobal_value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type",
//t    "global_kind : GLOBAL",
//t    "global_kind : CONSTANT",
//t    "linkage : EXTERNAL",
//t    "linkage : AVAILABLE_EXTERNALLY",
//t    "linkage : INTERNAL",
//t    "linkage : LINKONCE",
//t    "linkage : LINKONCE_ODR",
//t    "linkage : WEAK",
//t    "visibility : PRIVATE",
//t    "metadata_args : metadata_arg",
//t    "metadata_args : metadata_args ',' metadata_arg",
//t    "metadata_arg : SYMBOL ':' metadata_arg_expr",
//t    "metadata_arg : TYPE ':' metadata_arg_expr",
//t    "metadata_arg : ALIGN ':' constant",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' ')'",
//t    "metadata_arg_expr : metadata_arg_or_expr",
//t    "metadata_arg_or_expr : metadata_arg_and_expr",
//t    "metadata_arg_or_expr : metadata_arg_or_expr '|' metadata_arg_and_expr",
//t    "metadata_arg_and_expr : metadata_arg_primary",
//t    "metadata_arg_primary : SYMBOL",
//t    "metadata_arg_primary : META_SYMBOL",
//t    "metadata_arg_primary : STRING",
//t    "metadata_arg_primary : constant",
//t    "metadata_arg_primary : typed_constant",
//t    "metadata_arg_primary : NULL",
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
//t    "type : type '*' ALIGN INTEGER",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_type_args : function_type_arg",
//t    "function_type_args : function_type_args ',' function_type_arg",
//t    "function_type_arg : type",
//t    "function_type_arg : ELLIPSIS",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters define_tail '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters define_tail metadata_kvs '{' blocks '}'",
//t    "define_tail : function_addr",
//t    "define_tail : function_addr attribute_group_refs",
//t    "define_tail : function_addr attribute_group_refs ALIGN INTEGER",
//t    "define_tail : function_addr attribute_group_refs personality_function",
//t    "define_tail : function_addr attribute_group_refs ALIGN INTEGER personality_function",
//t    "define_tail : attribute_group_refs",
//t    "define_tail : attribute_group_refs ALIGN INTEGER",
//t    "define_tail : attribute_group_refs personality_function",
//t    "define_tail : attribute_group_refs ALIGN INTEGER personality_function",
//t    "define_header : DEFINE return_type",
//t    "define_header : DEFINE parameter_attribute return_type",
//t    "define_header : DEFINE define_header_attributes return_type",
//t    "define_header : DEFINE define_header_attributes parameter_attributes return_type",
//t    "define_header_attributes : NOALIAS",
//t    "define_header_attributes : runtime_preemption_specifier",
//t    "define_header_attributes : calling_convention",
//t    "define_header_attributes : linkage",
//t    "define_header_attributes : linkage runtime_preemption_specifier",
//t    "define_header_attributes : linkage runtime_preemption_specifier calling_convention",
//t    "define_header_attributes : linkage calling_convention",
//t    "personality_function : PERSONALITY typed_value",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : DECLARE NOALIAS return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : DECLARE parameter_attributes return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : DECLARE NOALIAS parameter_attributes return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "declare_tail : function_addr",
//t    "declare_tail : function_addr attribute_group_refs",
//t    "declare_tail : attribute_group_refs",
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
//t    "parameter_attribute : SRET",
//t    "parameter_attribute : NOALIAS",
//t    "parameter_attribute : DEREFERENCEABLE '(' INTEGER ')'",
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
//t    "global_value : GLOBAL_SYMBOL",
//t    "value : global_value",
//t    "value : nonglobal_value",
//t    "nonglobal_value : constant",
//t    "nonglobal_value : LOCAL_SYMBOL",
//t    "nonglobal_value : INTTOPTR '(' typed_value TO type ')'",
//t    "nonglobal_value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "nonglobal_value : BITCAST '(' typed_value TO type ')'",
//t    "nonglobal_value : PTRTOINT '(' typed_value TO type ')'",
//t    "nonglobal_value : '<' typed_values '>'",
//t    "nonglobal_value : '[' typed_values ']'",
//t    "nonglobal_value : '{' typed_values '}'",
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
//t    "element_index : INRANGE typed_value",
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "index : constant",
//t    "indices : index",
//t    "indices : indices ',' index",
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
//t    "terminator_instruction : INVOKE return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "terminator_instruction : INVOKE calling_convention return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "terminator_instruction : RESUME typed_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "terminator_instruction : UNREACHABLE",
//t    "instruction : ADD type value ',' value",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : ALLOCA type ',' typed_value ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
//t    "instruction : ASHR type value ',' value",
//t    "instruction : ASHR EXACT type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL return_type function_pointer function_args",
//t    "instruction : CALL calling_convention return_type function_pointer function_args",
//t    "instruction : CALL calling_convention return_type function_pointer function_args attribute_group_refs",
//t    "instruction : CALL calling_convention parameter_attribute return_type function_pointer function_args",
//t    "instruction : CALL calling_convention parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL calling_convention parameter_attributes return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention parameter_attributes return_type function_pointer function_args attribute_group_refs",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : EXTRACTVALUE typed_value ',' indices",
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
//t    "instruction : INSERTVALUE typed_value ',' typed_value ',' indices",
//t    "instruction : INTTOPTR typed_value TO type",
//t    "instruction : LANDINGPAD type CLEANUP",
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
    null,null,null,null,"'{'","'|'","'}'",null,null,null,null,null,null,
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
    "PRIVATE","INTERNAL","EXTERNAL","LINKONCE","LINKONCE_ODR","WEAK",
    "FASTCC","SIGNEXT","ZEROEXT","VOLATILE","RETURNED","DEREFERENCEABLE",
    "AVAILABLE_EXTERNALLY","PERSONALITY","SRET","CLEANUP","NONNULL",
    "NOCAPTURE","WRITEONLY","READONLY","READNONE","ATTRIBUTE_GROUP_REF",
    "ATTRIBUTES","NORECURSE","NOUNWIND","UNWIND","SPECULATABLE","SSP",
    "UWTABLE","ARGMEMONLY","SEQ_CST","DSO_LOCAL","DSO_PREEMPTABLE","RET",
    "BR","SWITCH","INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET",
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
              SyntaxError(yyname (yyToken), String.Join(", ", yyExpecting(yyState)));
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
              throw new yyParser.yyException("Irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("Irrecoverable syntax error at end-of-file");
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
#line 61 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 65 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 69 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 73 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 77 "Iril/IR/IR.jay"
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
#line 97 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 101 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 110 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 122 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 126 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 130 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 134 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 138 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 142 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 146 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 25:
#line 150 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 154 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 27:
#line 158 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 28:
#line 162 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 29:
#line 166 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 30:
#line 170 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 31:
#line 171 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 32:
#line 175 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 33:
#line 176 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 34:
#line 177 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 35:
#line 178 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 36:
#line 179 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 180 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 184 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
  case_39();
  break;
case 40:
  case_40();
  break;
case 41:
#line 201 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 42:
#line 202 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 43:
#line 203 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 44:
#line 207 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 45:
#line 211 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 56:
#line 240 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 57:
#line 244 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 58:
#line 251 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 59:
#line 255 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 60:
#line 259 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 61:
#line 263 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 62:
#line 267 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 80:
#line 300 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 81:
#line 304 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 308 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 83:
#line 315 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 319 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 324 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 89:
#line 330 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 90:
#line 331 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 91:
#line 332 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 92:
#line 333 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 93:
#line 337 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 94:
#line 341 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 95:
#line 345 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 96:
#line 349 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 97:
#line 353 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 357 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 99:
#line 361 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 368 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 372 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 103:
#line 380 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 104:
  case_104();
  break;
case 105:
  case_105();
  break;
case 115:
#line 412 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 116:
#line 416 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 117:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 118:
#line 424 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 119:
#line 431 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 120:
#line 435 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 121:
#line 439 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 126:
#line 450 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 127:
#line 457 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 128:
#line 461 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 129:
#line 465 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 130:
#line 469 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 131:
#line 473 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 135:
#line 483 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 136:
#line 484 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 137:
#line 491 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 138:
#line 495 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 502 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 140:
#line 506 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 510 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 142:
#line 514 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 144:
#line 522 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 145:
#line 526 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 146:
#line 527 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 147:
#line 528 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 148:
#line 529 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 149:
#line 530 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 150:
#line 531 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 151:
#line 532 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 152:
#line 533 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 153:
#line 534 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 154:
#line 535 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 155:
#line 539 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 163:
#line 562 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 164:
#line 563 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 165:
#line 564 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 166:
#line 565 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 167:
#line 566 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 168:
#line 567 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 169:
#line 568 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 170:
#line 569 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 171:
#line 570 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 172:
#line 571 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 173:
#line 575 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 174:
#line 576 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 175:
#line 577 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 176:
#line 578 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 177:
#line 579 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 178:
#line 580 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 179:
#line 581 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 180:
#line 582 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 181:
#line 583 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 182:
#line 584 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 183:
#line 585 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 184:
#line 586 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 185:
#line 587 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 186:
#line 588 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 187:
#line 589 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 188:
#line 590 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 189:
#line 594 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 193:
#line 604 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 194:
#line 608 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 195:
#line 612 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 196:
#line 616 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 197:
#line 620 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 199:
#line 628 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 200:
#line 632 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 202:
#line 640 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 203:
#line 641 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 204:
#line 642 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 205:
#line 643 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 206:
#line 644 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 207:
#line 645 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 208:
#line 646 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 209:
#line 647 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 210:
#line 648 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 211:
#line 655 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 662 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 666 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 214:
#line 673 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 680 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 684 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 691 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 699 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 220:
#line 706 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 710 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 721 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 725 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 732 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 736 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 743 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 747 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 229:
#line 751 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 762 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 766 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 773 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 777 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 781 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 785 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 793 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 239:
#line 794 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 240:
#line 801 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 805 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 812 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 243:
#line 816 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 244:
#line 820 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 245:
#line 824 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 246:
#line 828 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 247:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 248:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 250:
#line 841 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 251:
#line 845 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 252:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 253:
#line 853 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 254:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 259:
#line 874 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 878 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 884 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 262:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 895 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 902 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 920 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 271:
#line 927 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 931 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 935 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 943 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 947 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 951 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 278:
#line 955 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 279:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 966 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 970 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 282:
#line 974 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 283:
#line 978 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 982 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 285:
#line 986 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 286:
#line 990 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 994 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 288:
#line 998 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 289:
#line 1002 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 290:
#line 1006 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 291:
#line 1010 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 292:
#line 1014 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 293:
#line 1018 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 294:
#line 1022 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 295:
#line 1026 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 296:
#line 1030 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 297:
#line 1034 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 298:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 299:
#line 1042 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 300:
#line 1046 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 301:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 302:
#line 1054 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1058 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1062 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1066 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1070 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1074 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1078 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1082 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1086 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1090 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1094 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1098 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1102 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1106 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1110 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1114 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1118 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1122 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1126 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 321:
#line 1130 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 322:
#line 1134 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 323:
#line 1138 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 324:
#line 1142 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 325:
#line 1146 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1150 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1154 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1158 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1162 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1166 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1170 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1174 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1178 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1182 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1186 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1190 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1194 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1198 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1202 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 340:
#line 1206 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 341:
#line 1210 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1214 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1218 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1222 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1226 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1230 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1234 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1238 "Iril/IR/IR.jay"
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
#line 79 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 84 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 89 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 103 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 112 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_39()
#line 189 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_40()
#line 194 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_104()
#line 385 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_105()
#line 390 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,   10,
   10,   16,   16,   16,   16,   16,   16,   15,    9,    9,
   17,   17,   17,   17,   17,   18,   21,   21,   22,   23,
   23,   23,   23,   23,   23,   13,   13,    8,    8,    8,
    8,    8,   25,   25,   25,    7,    7,   27,   27,   27,
   27,   27,   27,   27,   27,   27,   27,   27,   27,    3,
    3,    3,   28,   28,   29,   29,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   30,
   30,   31,   31,    4,    4,   34,   34,   34,   34,   34,
   34,   34,   34,   34,   32,   32,   32,   32,   39,   39,
   39,   39,   39,   39,   39,   37,    5,    5,    5,    5,
    5,   43,   43,   43,   33,   33,   44,   44,   45,   45,
   45,   45,   40,   40,   38,   38,   38,   38,   38,   38,
   38,   38,   38,   38,   38,   14,   14,   41,   41,   36,
   36,   46,   47,   47,   47,   47,   47,   47,   47,   47,
   47,   47,   48,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   49,   50,
   50,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   53,   19,   19,   19,   19,   19,   19,   19,   19,   19,
   54,   26,   26,   55,   52,   52,   24,   56,   56,   51,
   51,   57,   58,   58,   35,   35,   59,   59,   59,   59,
   60,   60,   62,   62,   62,   62,   64,   65,   65,   66,
   66,   67,   67,   67,   67,   67,   67,   67,   68,   68,
   68,   68,   68,   68,   20,   20,   69,   69,   70,   70,
   71,   72,   72,   73,   74,   74,   75,   75,   42,   76,
   61,   61,   61,   61,   61,   61,   61,   61,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,   63,   63,
   63,   63,   63,   63,   63,   63,   63,   63,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   10,   11,    9,   10,    8,    5,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    3,    3,    3,    6,    5,    1,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    2,    3,    1,    2,    3,
    3,    3,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    3,    1,    1,    1,    4,    2,
    3,    5,    1,    3,    1,    1,    1,    1,    1,    1,
    1,    1,    3,    4,    2,    4,    1,    5,    5,    1,
    3,    1,    1,    7,    8,    1,    2,    4,    3,    5,
    1,    3,    2,    4,    2,    3,    3,    4,    1,    1,
    1,    1,    2,    3,    2,    2,    4,    5,    6,    6,
    7,    1,    2,    1,    3,    2,    1,    3,    1,    2,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    4,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    6,    9,    6,    6,    3,    3,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    2,    1,    2,    1,    3,    2,    1,    2,    1,
    3,    1,    1,    3,    1,    2,    2,    3,    1,    2,
    1,    2,    1,    2,    3,    4,    1,    3,    2,    1,
    3,    2,    3,    3,    3,    2,    4,    5,    1,    1,
    6,    9,    6,    6,    1,    3,    1,    1,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    1,    1,
    2,    7,    8,    9,    2,    2,    7,    1,    5,    6,
    5,    7,    5,    5,    6,    4,    4,    5,    6,    6,
    7,    5,    6,    6,    6,    7,    5,    6,    7,    7,
    8,    4,    4,    5,    6,    5,    2,    5,    4,    4,
    4,    4,    5,    6,    7,    6,    6,    6,    4,    3,
    7,    8,    5,    6,    5,    5,    6,    3,    4,    5,
    6,    7,    4,    5,    6,    6,    4,    5,    7,    8,
    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   86,   97,   89,   90,   91,   92,   88,  119,   34,
   32,   35,   36,   37,  269,  150,  151,  152,    0,   33,
  153,  145,  146,  148,  147,  149,  158,  159,    0,    0,
    0,   87,    0,    0,    0,    0,    0,  120,  121,    0,
    0,  143,    0,    0,    3,    0,    4,    0,    0,  156,
  157,   30,   31,   38,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   80,    0,    0,    0,    0,    0,    0,
    0,    0,  125,    0,    0,  154,    0,    0,    0,    0,
    0,    0,  144,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    8,    0,    7,    0,    0,    0,    0,
    0,   81,    0,    0,    0,    0,    0,  124,  103,   93,
    0,    0,  100,    0,    0,    0,    0,    0,    0,  141,
  142,  136,    0,    0,  137,  162,    0,    0,    0,  160,
  204,  205,  203,  206,  207,  208,  202,  193,  210,  209,
    0,    0,    0,    0,    0,    0,    0,    0,  192,    0,
    0,    0,    0,    0,    0,    0,    0,   39,    0,    0,
    0,   65,   64,   13,    0,    0,   58,   63,  155,    0,
    0,    0,    0,   96,   94,    0,    0,    0,    0,    0,
  128,    0,    0,    0,   78,   70,   68,   69,   71,   72,
   73,   74,    0,   66,    0,  135,    0,    0,    0,    0,
    0,    0,    0,  113,  161,    0,    0,    0,    0,  215,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   15,    0,    0,    0,   59,   14,    0,  189,
  191,  190,  212,   98,   82,   99,  101,  129,    0,    0,
  130,    0,    0,   12,   67,  138,    0,  109,   56,    0,
    0,    0,    0,    0,    0,  278,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  225,    0,    0,  231,    0,    0,    0,
  126,    0,    0,    0,    0,    0,  200,    0,  198,  199,
    0,    0,    0,    0,    0,    0,    0,   52,   55,    0,
   50,    0,   41,   53,    0,   47,   49,   54,   51,   42,
   43,   40,   17,   16,   62,   61,   60,  131,   75,  258,
  257,    0,  255,    0,    0,  276,    0,    0,  271,    0,
    0,    0,  275,  267,  268,    0,    0,  265,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  270,  307,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  163,  164,  165,  166,
  167,  168,  169,  170,  171,  172,    0,  173,  174,  185,
  186,  187,  188,  176,  178,  179,  180,  181,  177,  175,
  183,  184,  182,    0,    0,    0,    0,    0,    0,    0,
    0,  104,  226,    0,  232,    0,    0,   57,    0,  114,
    0,    0,    0,    0,  216,    0,    0,    0,   28,    0,
    0,    0,    0,  217,    0,   79,    0,  110,    0,  211,
    0,    0,  237,    0,    0,    0,    0,  266,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  259,    0,
    0,    0,    0,    0,    0,    0,    0,  320,    0,  105,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   45,    0,   48,  256,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  302,    0,    0,  222,  223,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  197,  194,  196,    0,
    0,    0,    0,   44,    0,    0,    0,  239,    0,    0,
  240,    0,    0,  279,    0,  304,  341,    0,  313,  326,
    0,  308,  344,    0,  330,  306,  346,  338,  334,    0,
    0,  323,    0,  284,  283,  325,  347,    0,    0,    0,
    0,  281,    0,    0,  201,  214,    0,    0,    0,    0,
    0,    0,    0,    0,  260,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  262,    0,    0,    0,  242,  238,    0,    0,    0,  280,
  342,  327,  331,  335,  324,  285,  317,  336,  224,    0,
    0,    0,    0,    0,    0,    0,    0,  218,    0,  220,
  316,  305,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  272,    0,  277,  263,    0,  250,  244,    0,
    0,    0,    0,  249,  245,  243,  241,    0,    0,  282,
    0,  321,    0,  339,    0,  219,    0,  261,  332,    0,
    0,    0,    0,  195,  264,  247,    0,    0,    0,    0,
    0,  273,    0,  322,  340,  221,    0,  248,    0,    0,
    0,    0,  274,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  254,  251,  253,    0,    0,  252,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  213,  185,  177,   75,
  186,  251,  221,  199,   77,   54,  178,  353,  169,  372,
  355,  356,  357,  358,  187,  758,  214,   86,   87,  132,
  133,   15,  106,  148,  323,  200,  224,   62,   57,   63,
   58,   59,  201,  144,  145,  150,  447,  464,  252,  503,
  759,  231,  706,  379,  626,  760,  619,  620,  324,  325,
  326,  327,  328,  504,  589,  670,  671,  785,  373,  558,
  559,  730,  731,  387,  388,  422,
  };
  protected static readonly short [] yySindex = {          839,
   -5, -108,   14,   23,   40, 3286, 3382, -224,    0,  839,
    0,    0,    0,    0, -137, -117,  103,  112, 1266,  -65,
  -31,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  154,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3818, -107,
  -37,    0,  188, -136,  198, 3932, 3443,    0,    0, 3443,
  -27,    0, 3443,  191,    0,  229,    0,   31,   39,    0,
    0,    0,    0,    0, 3932,   35,   34,  -68,  -46,  263,
  -24,  219,   52,    0,  188,  -13,  198,   48, 3932,   98,
   11,   73,    0, 3577,  198,    0,  198, 3443,  -23, 3443,
  229,  -21,    0,  272, 2786, -142,    0,    0, 2514, 3932,
   35, 3932,   35,    0,  276,    0, -234,  367,  301, 3751,
  386,    0, 3932, 3932,    8, 3932,  174,    0,    0,    0,
  188,   75,    0,  198,  229,  -16, -142,  229,  429,    0,
    0,    0, 2318,  113,    0,    0,  131, -102, -262,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   63,  411,  412,  413, 3937, 3937, 3937,  410,    0, 2514,
 3932,  649, 3932,  402,  407,  414,  139,    0, -234, 3756,
    0,    0,    0,    0,  -15,  998,    0,    0,    0,  188,
   17,  394,    2,    0,    0, 3653, -142,  229,  131,  131,
    0, -142,  408,  430,    0,    0,    0,    0,    0,    0,
    0,    0, 3455,    0, 3248,    0, 3646, -231,  197, 5835,
  -98, 3937,  216,    0,    0,  434, 3937, 3937, 3937,    0,
   30,   19,   38,  102,  441, 2514,  115,  442, 2480, 3691,
 3726,  345,    0, -234,  157,   28,    0,    0, 3784,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -142,  131,
    0,  242, 1204,    0,    0,    0,  246,    0,    0,  443,
 3937, -221, 3937, 3620, 3937,    0,  606, 3932,  606, 3932,
  606, 3932, 3932,  502, 3932, 3932, 3932,  606, 2589, 2611,
 3932, 3932, 3932, 3937, 3937, 3937, 3937, 3937, 3932, 2639,
 2644,  179, 2560, 3937, 3937, 3937, 3937, 3937, 3937, 3937,
 3937, 3937, 3937, 3937, 3937,  789, 2315, 3932, 3932, 3346,
   92, 3932, 2625,    0, 5835,  235,    0,  235,  236, 5835,
    0,  208, 3932,  140,  142,  144,    0, 3937,    0,    0,
  261,  153,  485,  273,  158,  160,  506,    0,    0,  511,
    0, 2533,    0,    0,  435,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  184,    0,  208, 6299,    0,  290,  -55,    0,  520,
  532, 3932,    0,    0,    0,  998,  606,    0,  998,  998,
  606,  998,  998,  606,  998,  998, 3932,  998,  998,  998,
  998,  998,  606, 3932,  998, 3932,  998,  998,  998,  998,
  522,  536,  537,  539,  542,  172, 3932,  266, 3937,  543,
    0,    0, 3932,  319,  195,  207,  209,  214,  215,  218,
  220,  228,  230,  231,  232,  233,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3932,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3932,   79,  998,  532, 3932, 3443, 3346,
  -22,    0,    0,  235,    0,  315,  315,    0, 2744,    0,
  348, 3932, 3932, 3932,    0,  235,  340,  221,    0,  360,
  372,  253, 2770,    0, 3726,    0, 1204,    0,  235,    0,
  588,  373,    0,  593,  532,  590,  998,    0,  591,  594,
  998,  598,  601,  998,  602,  603,  998,  608,  614,  615,
  621,  623,  998,  998,  628,  998,  629,  630,  631,  632,
 3937, 3937, 3937,  345, 3937,   55,  374, 3932,  633, 3932,
  377, 3937, 3932, 3932, 3932, 3932, 3932, 3932, 3932, 3932,
 3932, 3932, 3932, 3932,  998,  998,  -55,  634,    0,  636,
  593,  532,  532, 3932,  532, 3932, 3443,    0,  315,    0,
 3937,  293,  306,  337,  315,  235,  424,  235,    0,  425,
    0,  196,    0,    0,  315,  373,  592, 3613,  304,  593,
  -55,  643,  -55,  -55,  644,  -55,  -55,  646,  -55,  -55,
  648,  -55,  -55,  -55,  -55,  -55,  650,  651,  -55,  652,
  -55,  -55,  -55,  -55,    0,  657,  658,    0,    0,  659,
  662,  450,  664, 3932,  998,  666, 3932,  671, 3937,  672,
  188,  188,  188,  188,  188,  188,  188,  188,  188,  188,
  188,  188,  676,  679,  681,  622, 3937,  131,  593,  593,
  532,  593,  532,  532, 3443,  682,    0,    0,    0,  315,
  235,  315,  235,    0,  683, 3932, 3856,    0, 2382,  213,
    0,  373,  349,    0,  -55,    0,    0,  -55,    0,    0,
  -55,    0,    0,  -55,    0,    0,    0,    0,    0,  -55,
  -55,    0,  -55,    0,    0,    0,    0, 3937, 3937,  345,
  345,    0,  361,  689,    0,    0,  362,  692,  364,  697,
  -20,  -55,  -55,  -55,    0,  698,  131,  131,  131,  593,
  131,  593,  593,  532,  -20,  315,  315,  373,  699, 3903,
    0,  706,  312, 2544,    0,    0, 3880,  439,  373,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  659,
  493,  383,  516,  390,  523,  -20, 3937,    0,  737,    0,
    0,    0,  695, 3937,  131,  131,  131,  131,  131,  131,
  593,  254,    0,  373,    0,    0, 3606,    0,    0,  418,
  742,  756,  758,    0,    0,    0,    0,  373,  480,    0,
  549,    0,  550,    0,  737,    0,  -20,    0,    0,  131,
  131,  131,  131,    0,    0,    0,  282,  768, 3937, 3937,
 3937,    0,  373,    0,    0,    0,  131,    0, 3932,  428,
  431,  433,    0,  378, 3932, 3932, 3932, 3937,  398,  401,
  403,  766,    0,    0,    0,  -20,  300,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  816,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 2328, 3481,  545,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    9,    0,    0,    0,    0,    0,
  861, 3517,    0,    0,  551,    0,  554,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  782,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  192,    0,    0,  558,    0,    0,   32,    0,    0,    0,
    0,    0,  226,    0,    0,    0,  -97,    0,  -95,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  782,
    0,  105,    0,    0,    0,    0,    0,    0,    0,    0,
  961,    0,    0,    0,    0,  782,    0,    0,    0,   26,
  782,    0,  782,    0,    0,    0,    0,    0,  114,  137,
    0,    0, 3832, 3855,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  305,    0,    0,  -89,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  782,    0,    0,  782,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  161,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 2841,    0, 5932,    0,    0,
    0,  -88,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  239,    0,    0,    0,    0,    0,    0,  -11,
    0,  782,    0,    0,  326,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -86,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  782,    0,    0,  782,  782,
    0,  782,  782,    0,  782,  782,    0,  782,  782,  782,
  782,  782,    0,    0,  782,    0,  782,  782,  782,  782,
    0,    0,    0,    0,    0,  782,    0,  782,    0,    0,
    0,    0,    0,  782,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  782,  782,    0,    0,    0,    0,
  782,    0,    0, 2938,    0, 3035, 6029,    0,    0,    0,
  782,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 6126,    0,
    0,    0,    0,    0,    0,    0,  782,    0,    0,    0,
  782,    0,    0,  782,    0,    0,  782,    0,    0,    0,
    0,    0,  782,  782,    0,  782,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  782,    0,    0,    0,
  782,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  782,  782,    0, 3895,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3132,    0,
    0,  782,  782,  782,  247,    0,    0,  368,    0,    0,
    0,    0,    0,    0, 6223,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3992,
    0,    0,    0,    0,  782,    0,    0,    0,    0,    0,
 1058, 1158, 1258, 1355, 1455, 1555, 1652, 1752, 1852, 1949,
 2049, 2149,    0,    0,    0,    0,    0, 4089,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  556,
  578,  669,  738,    0,    0,    0,    0,    0,  782,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4186,    0, 4283,    0,
 4380,    0,    0,    0,    0,  799,  827,    0,    0,    0,
    0,  328,  782,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4477,
    0,    0,    0,    0,    0,    0,    0,    0, 4574,    0,
    0,    0,    0,    0, 4671, 4768, 4865, 4962, 5059, 5156,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5253,    0,    0,    0,    0, 5350,
 5447, 5544, 5641,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5738,    0,    0,    0,
    0,    0,    0,  782,    0,    0,    0,    0,  782,  782,
  782,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  822,  755,    0,    0,    0,    0,  656,  660,   82,
   -6,  120, -318,   46,    0,  818,  596,  597, -133, -481,
    0,  346,    0, -643,    0,  186,  635,  753,   84,    0,
  647,    0,  -62,    0,  519,  -99, -200,   -2,    0,  -54,
  798,  -32, -100,    0,  637, -138,    0,    0,    0,  369,
 -698,  280,    0, -417, -499,   58,  156,  159, -308,    0,
  534,  538,  482,  149, -523,    0,  125,    0,  371,    0,
  223,    0,  134,  -92, -182,    0,
  };
  protected static readonly short [] yyTable = {            53,
   53,   82,   98,   56,  166,  100,  149,  476,  119,  477,
  225,  582,   94,  115,  473,   89,   94,  268,   94,   91,
  220,   93,  729,   94,  330,  106,  772,  111,  249,   51,
  123,  127,   51,  107,  112,  167,  108,  648,  137,   50,
  628,  174,   85,   91,  377,  175,  222,  218,   85,   53,
   53,  123,   83,   53,  146,   16,   53,  795,   91,  128,
  103,  225,  338,  378,   76,   85,  673,  165,  109,   84,
   51,  249,  197,  338,   19,  202,   49,  222,  254,  225,
  339,  338,   85,   20,  587,  146,  729,  131,  215,   55,
   61,   53,   64,   53,  256,  103,  258,  103,  143,  260,
   21,  261,   49,  170,   29,  172,  354,  354,  361,  248,
  223,  122,   51,  132,   50,  195,  190,  191,  196,  193,
   91,  225,  111,  113,  704,  718,  719,  708,  721,  371,
  340,  480,  192,   83,   66,  259,  134,  837,  176,   95,
   97,  267,   67,   99,   85,   51,  102,   70,   71,   88,
   84,  147,  364,  216,  337,  569,  217,  110,  368,  112,
  133,   17,   18,   68,  236,   35,  239,  575,  665,  557,
  473,  219,   69,  498,  146,  329,  106,   49,  111,  243,
  585,  134,  244,  136,  107,  112,  391,  108,  394,  131,
   47,   48,  171,   83,  173,  403,  767,  363,  769,  770,
  244,  151,  152,  153,  508,  154,  155,  156,  508,  157,
  143,  508,  103,   91,   79,  536,  250,  158,  494,   90,
  508,   70,   71,  159,  496,   72,   73,  497,  168,   91,
  160,   85,  102,  352,  352,  102,  664,   94,   21,  497,
  114,  382,   80,  181,  101,   81,   18,  803,  135,  118,
  138,  104,   23,  736,  738,  198,  737,  660,  247,  662,
   24,   25,   26,   27,   28,   85,  139,   53,  105,  139,
  386,  389,  390,  392,  393,  395,  396,  398,  399,  400,
  401,  402,  405,  407,  408,  409,  410,  469,  568,  235,
  107,  238,  416,  418,  804,  807,  424,  797,  108,  127,
  127,  247,  117,  127,  127,  188,  127,   91,  121,  538,
  773,  465,  466,   53,  124,  471,  161,  468,  181,  127,
  127,  789,  818,   70,   71,  497,  481,   23,   72,   73,
  162,  163,  164,  657,   91,   24,   25,   26,   27,   28,
  838,  120,  726,  797,  727,  140,  658,   91,  140,  127,
  230,  230,  230,   91,  757,  343,  805,  381,  347,  371,
   91,  354,  542,  371,  126,  188,   46,   26,  246,   46,
  812,  246,   29,   29,   35,   53,   29,  659,   91,   29,
  507,  132,  132,  127,  511,  132,  132,  514,  132,   91,
  517,  571,   29,   29,  139,  823,  523,  524,   89,  526,
  618,  132,  132,  467,  134,  134,  179,  331,  134,  134,
  537,  134,  334,  335,  336,   91,  541,  624,   91,   91,
  629,  828,   29,  180,  134,  134,  189,  622,  133,  133,
  194,  132,  133,  133,  367,  133,  226,  567,  833,   91,
  555,  834,   91,  835,   91,  232,  233,  146,  133,  133,
  227,  228,  229,  234,  134,  255,  376,  556,  380,  240,
  383,   53,   53,   53,  241,  505,  564,  566,  262,  263,
  269,  242,  332,  333,  341,  572,  573,  574,  133,  411,
  412,  413,  414,  415,  342,  345,  420,  344,  352,  425,
  426,  427,  428,  429,  430,  431,  432,  433,  434,  435,
  436,  369,  374,  375,  421,  470,   21,   21,  219,  478,
   21,   21,  655,   21,   18,   18,  222,  486,   18,   18,
  482,   18,  483,  485,  484,  487,   21,   21,  488,  489,
  490,  625,  491,  625,   18,   18,  631,  632,  633,  634,
  635,  636,  637,  638,  639,  640,  641,  642,  717,  492,
  493,  562,  563,  565,  253,   19,   21,   53,  495,   53,
   53,   50,  500,  502,   18,  531,  618,  618,  151,  152,
  153,   94,  154,  155,  156,  543,  157,   20,  225,  532,
  533,  669,  534,  778,  779,  535,  540,  544,  329,  545,
  159,  166,   51,  577,  546,  547,  576,  160,  548,  784,
  549,  151,  152,  153,  539,  154,  155,  156,  550,  157,
  551,  552,  553,  554,  734,  561,  578,  625,  765,  766,
  625,  768,  167,  159,   49,  580,  225,  225,  579,  225,
  160,  586,  588,  591,  593,   26,   26,  594,  377,   26,
   26,  596,   26,  371,  597,  599,  600,  651,   53,  653,
  654,  602,  103,  590,  165,   26,   26,  603,  604,  352,
  733,  225,  225,  225,  605,   50,  606,  800,   27,  801,
  802,  609,  611,  612,  613,  614,  627,  646,  225,  647,
  661,  663,  666,  780,  672,   26,  675,  678,  203,  681,
   91,  684,  237,  690,  691,  693,   51,  781,  782,  783,
  698,  699,  700,  817,  204,  701,  702,  703,  166,  707,
  649,  650,  557,  652,  709,  711,  615,  616,  617,  712,
  621,  623,  713,  352,  714,  725,  728,  630,   49,  739,
  669,  103,  752,  751,  753,  754,  755,   24,  724,  167,
  756,  764,  774,  205,  206,  777,  501,  207,  208,  790,
  209,  210,  211,  212,  506,  791,  656,  509,  510,  788,
  512,  513,  793,  515,  516,   22,  518,  519,  520,  521,
  522,  165,  792,  525,   23,  527,  528,  529,  530,  794,
  797,  809,   24,   25,   26,   27,   28,  798,  151,  152,
  153,  808,  154,  155,  156,  810,  157,  811,   22,  720,
  813,  722,  723,  250,  158,  814,  815,  819,  825,  836,
  159,  826,  824,  827,  710,    1,  115,  160,  829,  830,
  831,   85,  116,   19,   19,  117,   25,   19,   19,  118,
   19,   65,  716,  116,  560,  246,   78,  360,  245,  362,
  583,  125,  257,   19,   19,   20,   20,  265,  479,   20,
   20,   92,   20,  266,  816,  749,  499,  397,  474,  750,
   95,  787,  475,  776,    0,   20,   20,  584,  715,   22,
    0,    0,  771,   19,    0,  592,    0,    0,   23,  595,
    0,    0,  598,  747,  748,  601,   24,   25,   26,   27,
   28,  607,  608,    0,  610,   20,    0,    0,    0,    0,
   95,   95,   95,  161,   95,  151,  152,  153,    0,  154,
  155,  156,    0,  157,    0,    0,    0,  162,  163,  164,
   95,  158,   95,  643,  644,  645,    0,  159,    0,    0,
    0,    0,    0,    0,  160,    0,   27,   27,    0,    0,
   27,   27,  796,   27,    0,    0,  384,  385,    0,  799,
    0,   95,    0,   95,    0,    0,   27,   27,    0,  674,
    0,  676,  677,    0,  679,  680,    0,  682,  683,    0,
  685,  686,  687,  688,  689,    0,    0,  692,    0,  694,
  695,  696,  697,   95,    0,   95,   27,    0,    0,    0,
    0,    0,    0,  705,  820,  821,  822,    0,    0,    0,
   86,  213,    0,    0,  213,   24,   24,    0,    0,   24,
   24,    0,   24,  832,    0,    0,    0,    0,    0,    0,
  161,    0,  213,    0,    0,   24,   24,    0,    0,    0,
    0,    0,    0,    0,  162,  163,  164,  735,    0,   91,
    0,    0,    0,  740,    0,    0,  741,    0,    0,  742,
    0,    0,  743,  213,    0,   24,    0,  166,  744,  745,
    0,  746,    0,    0,    0,    0,   22,   22,    0,    0,
   22,   22,    0,   22,    0,    0,    0,    0,    0,    0,
  761,  762,  763,  213,    0,  213,   22,   22,  167,    0,
    0,    0,    0,    0,   25,   25,    0,   85,   25,   25,
    0,   25,  786,    0,    0,    0,    1,    2,    0,    0,
    3,    4,    0,    5,   25,   25,   22,   95,   95,   95,
  165,   95,   95,   95,    0,   95,    6,    7,   95,   95,
    0,    0,   95,   95,   95,   95,    0,    0,    0,   95,
    0,    0,    0,    0,   25,    0,   95,    0,   95,   95,
    0,    0,   95,    0,    0,    0,    8,    0,    0,    0,
    0,    0,    0,   95,   95,    0,   95,   95,    0,    0,
   95,   95,   95,   95,   95,   95,   95,    0,   95,  437,
  438,  439,  440,  441,  442,  443,  444,  445,  446,   95,
   95,   95,    0,   95,   95,    0,    0,   85,   95,    0,
   95,    0,    0,   95,   95,   95,   95,   95,   95,   95,
   95,   95,   95,    0,   95,   95,    0,   95,   95,   95,
   95,   95,   95,   95,   95,   95,   95,   95,   95,   95,
    0,    0,   95,  213,  213,    0,   95,   95,   95,   95,
   95,    0,   95,   95,   95,   95,   95,   95,   95,    0,
   95,    0,    0,    0,  151,  152,  153,    0,  154,  155,
  156,   95,  157,    0,    0,    0,    0,    0,    0,  250,
  158,    0,   95,   95,   95,   95,  159,   95,    0,    0,
    0,    0,    0,  160,    0,    0,    0,    0,    0,  213,
  213,  213,    0,  213,  213,    0,    0,   85,  213,    0,
  213,    0,    0,  213,  213,  213,  213,  213,  213,  213,
  213,  213,  213,    0,  213,  213,    0,  213,  213,  213,
  213,  213,  213,  213,  213,  213,  213,  213,  213,  213,
  343,  343,  213,    0,    0,    0,  213,  213,  213,  213,
  213,  213,  213,  213,  213,  213,  213,  213,  213,    0,
  213,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  213,    0,    0,    0,    0,    0,    0,    0,  161,
    0,    0,  213,  213,  213,  213,    0,  213,    0,    0,
    0,    0,    0,  162,  163,  164,  343,  343,  343,    0,
  343,  343,    0,    0,   85,  343,    0,  343,    0,    0,
  343,  343,  343,  343,  343,  343,  343,  343,  343,  343,
    0,  343,  343,    0,  343,  343,  343,  343,  343,  343,
  343,  343,  343,  343,  343,  343,  343,    0,    0,  343,
  348,  348,    0,  343,  343,  343,  343,  343,    0,  343,
  343,  343,  343,  343,  343,  343,    0,  343,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  343,    0,
  151,  152,  153,    0,  154,  155,  156,    0,  157,  343,
  343,  343,  343,    0,  343,    0,    0,    0,    0,  370,
    0,    0,  159,    0,    0,    0,  348,  348,  348,  160,
  348,  348,    0,    0,   85,  348,    0,  348,    0,    0,
  348,  348,  348,  348,  348,  348,  348,  348,  348,  348,
    0,  348,  348,    0,  348,  348,  348,  348,  348,  348,
  348,  348,  348,  348,  348,  348,  348,    0,    0,  348,
  333,  333,    0,  348,  348,  348,  348,  348,    0,  348,
  348,  348,  348,  348,  348,  348,    0,  348,    0,    0,
    0,    0,    0,    0,    0,   70,   71,    0,  348,   72,
   73,   74,   30,   31,   32,   33,   34,    0,    0,  348,
  348,  348,  348,   40,  348,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  333,  333,  333,    0,
  333,  333,    0,    0,   85,  333,    0,  333,    0,    0,
  333,  333,  333,  333,  333,  333,  333,  333,  333,  333,
    0,  333,  333,    0,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  333,  333,  333,  333,  312,  312,  333,
    0,    0,    0,  333,  333,  333,  333,  333,    0,  333,
  333,  333,  333,  333,  333,  333,    0,  333,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  333,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  333,
  333,  333,  333,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,  312,  312,  312,    0,  312,  312,    0,
    0,   85,  312,    0,  312,    0,    0,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  312,    0,  312,  312,
    0,  312,  312,  312,  312,  312,  312,  312,  312,  312,
  312,  312,  312,  312,    0,    0,  312,  309,  309,    0,
  312,  312,  312,  312,  312,    0,  312,  312,  312,  312,
  312,  312,  312,    0,  312,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  312,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,  312,  312,  312,
    0,  312,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  309,  309,  309,    0,  309,  309,    0,
    0,   85,  309,    0,  309,    0,    0,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,    0,  309,  309,
    0,  309,  309,  309,  309,  309,  309,  309,  309,  309,
  309,  309,  309,  309,    0,    0,  309,  310,  310,    0,
  309,  309,  309,  309,  309,    0,  309,  309,  309,  309,
  309,  309,  309,    0,  309,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  309,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,  309,  309,  309,
    0,  309,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  310,  310,  310,    0,  310,  310,    0,
    0,   85,  310,    0,  310,    0,    0,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,    0,  310,  310,
    0,  310,  310,  310,  310,  310,  310,  310,  310,  310,
  310,  310,  310,  310,  311,  311,  310,    0,    0,    0,
  310,  310,  310,  310,  310,    0,  310,  310,  310,  310,
  310,  310,  310,    0,  310,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  310,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  310,  310,  310,  310,
    0,  310,    0,    0,    0,    0,    0,    0,    0,    0,
  311,  311,  311,    0,  311,  311,    0,    0,   85,  311,
    0,  311,    0,    0,  311,  311,  311,  311,  311,  311,
  311,  311,  311,  311,    0,  311,  311,    0,  311,  311,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  311,    0,    0,  311,  345,  345,    0,  311,  311,  311,
  311,  311,    0,  311,  311,  311,  311,  311,  311,  311,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  311,  311,  311,  311,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  345,  345,  345,    0,  345,  345,    0,    0,   85,  345,
    0,  345,    0,    0,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,    0,  345,  345,    0,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,    0,    0,  345,  337,  337,    0,  345,  345,  345,
  345,  345,    0,  345,  345,  345,  345,  345,  345,  345,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  345,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  345,  345,  345,  345,    0,  345,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  337,  337,  337,    0,  337,  337,    0,    0,   85,  337,
    0,  337,    0,    0,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,    0,  337,  337,    0,  337,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
  337,  329,  329,  337,    0,    0,    0,  337,  337,  337,
  337,  337,    0,  337,  337,  337,  337,  337,  337,  337,
    0,  337,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  337,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  337,  337,  337,  337,    0,  337,    0,
    0,    0,    0,    0,    0,    0,    0,  329,  329,  329,
    0,  329,  329,    0,    0,    0,  329,    0,  329,    0,
    0,  329,  329,  329,  329,  329,  329,  329,  329,  329,
  329,    0,  329,  329,    0,  329,  329,  329,  329,  329,
  329,  329,  329,  329,  329,  329,  329,  329,    0,    0,
  329,  319,  319,    0,  329,  329,  329,  329,  329,    0,
  329,  329,  329,  329,  329,  329,  329,    0,  329,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  329,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   91,
  329,  329,  329,  329,    0,  329,    0,   85,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  319,  319,  319,
    0,  319,  319,    0,    0,    0,  319,   85,  319,    0,
    0,  319,  319,  319,  319,  319,  319,  319,  319,  319,
  319,    0,  319,  319,    0,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  319,  319,  319,  319,   85,    0,
  319,  286,  286,   91,  319,  319,  319,  319,  319,    0,
  319,  319,  319,  319,  319,  319,  319,    0,  319,    0,
    0,  166,    0,    0,    0,    0,    0,    0,    0,  319,
   85,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  319,  319,  319,  319,    0,  319,    0,    0,    0,    0,
    0,    0,  167,    0,    0,    0,    0,  286,  286,  286,
    0,  286,  286,    0,    0,    0,  286,    0,  286,    0,
    0,  286,  286,  286,  286,  286,  286,  286,  286,  286,
  286,    0,  286,  286,  165,  286,  286,  286,  286,  286,
  286,  286,  286,  286,  286,  286,  286,  286,    0,    0,
  286,   91,    0,  346,  286,  286,  286,  286,  286,    0,
  286,  286,  286,  286,  286,  286,  286,    0,  286,  166,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  286,
    0,    0,    0,    0,    0,   91,    0,    0,    0,    0,
  286,  286,  286,  286,    0,  286,    0,    0,    0,    0,
  167,    0,    0,  166,   91,  448,  449,    0,    0,    0,
    0,    0,    0,    0,   85,   85,   85,    0,   85,   85,
   85,    0,   85,    0,    0,    0,    0,    0,    0,   85,
   85,    0,  165,  166,  167,    0,   85,    0,    0,   96,
    0,    0,    0,   85,    0,    0,    0,    0,    0,   50,
   36,   37,    0,   38,   39,    0,    0,   41,    0,   42,
   43,   44,   45,   46,  167,    0,  165,    0,  151,  152,
  153,    0,  154,  155,  156,    0,  157,    0,   50,    0,
   51,    0,    0,  250,  158,    0,    0,    0,    0,    0,
  159,    0,    0,    0,    0,    0,  165,  160,    0,    0,
   50,    0,    0,   96,    0,    0,    0,    0,    0,   51,
    0,    0,   49,    0,   36,   37,    0,   38,   39,    0,
    0,   41,    0,   42,   43,   44,   45,   46,   50,   85,
    0,   51,    0,   50,    0,    0,    0,  450,  451,  452,
  453,   49,    0,   85,   85,   85,  454,  455,  456,  457,
  458,  459,  460,  461,  462,  463,    0,    0,    0,   51,
    0,    0,    0,   49,   51,    0,  151,  152,  153,    0,
  154,  155,  156,    0,  157,    0,    0,    0,    0,  472,
    0,    0,  158,  161,    0,    0,    0,    0,  159,    0,
    0,   49,    0,    0,    0,  160,   49,  162,  163,  164,
  151,  152,  153,    0,  154,  155,  156,    0,  157,    0,
    0,    0,    0,    0,    0,    0,  158,    0,    0,  151,
  152,  153,  159,  154,  155,  156,    0,  157,    0,  160,
  151,  152,  153,    0,  154,  155,  156,    0,  157,    0,
  581,  159,    0,    0,    0,  250,  158,    0,  160,    0,
    0,    0,  159,   22,    0,    0,  142,    0,    0,  160,
    0,    0,   23,    0,    0,   96,    0,    0,    0,    0,
   24,   25,   26,   27,   28,   50,   36,   37,    0,   38,
   39,  161,   22,   41,    0,   42,   43,   44,   45,   46,
    0,   23,    0,    0,    0,  162,  163,  164,  570,   24,
   25,   26,   27,   28,   22,    0,   51,    0,    0,    0,
    0,    0,    0,   23,    0,  161,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,  270,    0,  162,
  163,  164,   22,    0,    0,    0,    0,  181,   49,    0,
    0,   23,    0,    0,    0,  161,   23,    0,    0,   24,
   25,   26,   27,   28,   24,   25,   26,   27,   28,  162,
  163,  164,    0,  423,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  417,  404,    0,    0,    0,  419,    0,
    0,    0,    0,  271,  272,  273,    0,  274,  275,    0,
    0,    0,  276,    0,  277,  229,  406,  278,  279,  280,
  281,  282,  283,  284,  285,  286,  287,    0,  288,  289,
    0,  290,  291,  292,  293,  294,  295,  296,  297,  298,
  299,  300,  301,  302,    0,    0,  303,    0,    0,    0,
  304,  305,  306,  307,  308,    0,  309,  310,  311,  312,
  313,  314,  315,    0,  316,    0,  270,    0,    0,    0,
    0,    0,    0,    0,    0,  317,  151,  152,  153,    0,
  154,  155,  156,    0,  157,    0,  318,  319,  320,  321,
    0,  322,    0,    0,    0,  370,    0,    0,  159,   22,
    0,    0,    0,    0,    0,  160,    0,    0,   23,    0,
    0,    0,  227,  140,    0,    0,   24,   25,   26,   27,
   28,    0,  271,  272,  273,    0,  274,  275,  141,    0,
    0,  276,    0,  277,    0,    0,  278,  279,  280,  281,
  282,  283,  284,  285,  286,  287,    0,  288,  289,    0,
  290,  291,  292,  293,  294,  295,  296,  297,  298,  299,
  300,  301,  302,  229,    0,  303,    0,    0,    0,  304,
  305,  306,  307,  308,    0,  309,  310,  311,  312,  313,
  314,  315,    0,  316,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  317,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  318,  319,  320,  321,  230,
  322,    0,    0,    0,    0,    0,    0,    0,    0,  229,
  229,  229,    0,  229,  229,    0,    0,    0,  229,    0,
  229,    0,    0,  229,  229,  229,  229,  229,  229,  229,
  229,  229,  229,    0,  229,  229,    0,  229,  229,  229,
  229,  229,  229,  229,  229,  229,  229,  229,  229,  229,
  227,    0,  229,    0,    0,    0,  229,  229,  229,  229,
  229,    0,  229,  229,  229,  229,  229,  229,  229,    0,
  229,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  229,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  229,  229,  229,  229,  228,  229,    0,    0,
    0,    0,    0,    0,    0,    0,  227,  227,  227,    0,
  227,  227,    0,    0,    0,  227,    0,  227,    0,    0,
  227,  227,  227,  227,  227,  227,  227,  227,  227,  227,
    0,  227,  227,    0,  227,  227,  227,  227,  227,  227,
  227,  227,  227,  227,  227,  227,  227,  230,    0,  227,
    0,    0,    0,  227,  227,  227,  227,  227,    0,  227,
  227,  227,  227,  227,  227,  227,    0,  227,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  227,    0,
    0,    0,    0,    0,    0,   50,    0,    0,    0,  227,
  227,  227,  227,    0,  227,    0,    0,    0,    0,    0,
    0,    0,    0,  230,  230,  230,    0,  230,  230,    0,
    0,    0,  230,    0,  230,    0,   51,  230,  230,  230,
  230,  230,  230,  230,  230,  230,  230,    0,  230,  230,
    0,  230,  230,  230,  230,  230,  230,  230,  230,  230,
  230,  230,  230,  230,  228,   50,  230,    0,   49,    0,
  230,  230,  230,  230,  230,    0,  230,  230,  230,  230,
  230,  230,  230,    0,  230,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  230,   51,    0,    0,    0,
    0,   50,    0,    0,    0,    0,  230,  230,  230,  230,
    0,  230,    0,    0,    0,    0,    0,    0,    0,    0,
  228,  228,  228,    0,  228,  228,    0,    0,   49,  228,
    0,  228,   51,    0,  228,  228,  228,  228,  228,  228,
  228,  228,  228,  228,    0,  228,  228,    0,  228,  228,
  228,  228,  228,  228,  228,  228,  228,  228,  228,  228,
  228,    0,   50,  228,   49,    0,    0,  228,  228,  228,
  228,  228,    0,  228,  228,  228,  228,  228,  228,  228,
    0,  228,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  228,   51,    0,    0,    0,    0,    0,   96,
  122,    0,    0,  228,  228,  228,  228,    0,  228,   22,
   36,   37,    0,   38,   39,    0,    0,   41,   23,   42,
   43,   44,   45,   46,    0,   49,   24,   25,   26,   27,
   28,  122,    0,    0,    0,    0,  123,   29,    0,  264,
    0,    0,   30,   31,   32,   33,   34,   35,   36,   37,
    0,   38,   39,   40,    0,   41,    0,   42,   43,   44,
   45,   46,    0,  122,    0,    0,    0,  123,    0,   22,
    0,    0,   47,   48,    0,    0,    0,  130,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,   50,   96,    0,  123,
    0,    0,    0,    0,    0,   22,  806,   35,   36,   37,
    0,   38,   39,  668,   23,   41,    0,   42,   43,   44,
   45,   46,   24,   25,   26,   27,   28,   51,    0,    0,
    0,    0,   50,   60,    0,    0,    0,    0,    0,   50,
    0,    0,    0,    0,   36,   37,    0,   38,   39,    0,
    0,   41,    0,   42,   43,   44,   45,   46,    0,   49,
    0,    0,    0,   51,    0,   50,   22,    0,    0,    0,
   51,    0,   50,    0,  203,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
  204,    0,    0,    0,   96,   49,   51,    0,    0,    0,
    0,    0,   49,   51,  122,   36,   37,    0,   38,   39,
   50,    0,   41,  122,   42,   43,   44,   45,   46,    0,
    0,  122,  122,  122,  122,  122,    0,    0,   49,  205,
  206,    0,  122,  207,  208,   49,  209,  210,  211,  212,
  123,   51,    0,  122,  122,   50,  122,  122,    0,  123,
  122,    0,  122,  122,  122,  122,  122,  123,  123,  123,
  123,  123,    0,    0,    0,    0,    0,    0,  123,    0,
   50,    0,    0,   49,    0,   50,   51,    0,    0,  123,
  123,    0,  123,  123,    0,    0,  123,    0,  123,  123,
  123,  123,  123,    0,    0,    0,    0,    0,    0,    0,
   22,   51,    0,   50,    0,    0,   51,    0,   49,   23,
    0,    0,    0,    0,    0,    0,    0,   24,   25,   26,
   27,   28,  151,  152,  153,    0,  154,  155,  156,  129,
  157,    0,    0,   49,   51,  184,   22,   50,   49,    0,
    0,  370,    0,   22,  159,   23,    0,    0,    0,    0,
  667,  160,   23,   24,   25,   26,   27,   28,    0,    0,
   24,   25,   26,   27,   28,    0,   49,    0,   51,   22,
    0,    0,    0,    0,    0,   50,   22,    0,   23,    0,
    0,   35,    0,  140,    0,   23,   24,   25,   26,   27,
   28,    0,    0,   24,   25,   26,   27,   28,  141,   50,
   49,    0,   84,    0,    0,  129,   51,  151,  152,  153,
  348,  154,  155,  156,   22,  349,   76,    0,    0,    0,
    0,    0,   50,   23,  350,    0,  351,    0,    0,  159,
   51,   24,   25,   26,   27,   28,  160,    0,   49,   77,
    0,    0,  151,  152,  153,  348,  154,  155,  156,   22,
  349,   50,    0,   51,    0,  775,   50,    0,   23,  359,
    0,  351,   49,    0,  159,    0,   24,   25,   26,   27,
   28,  160,    0,    0,  181,  182,    0,    0,    0,  181,
  182,    0,   51,   23,  183,   49,    0,   51,   23,  183,
    0,   24,   25,   26,   27,   28,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,  181,  365,    0,
    0,    0,    0,    0,   49,    0,   23,  366,    0,   49,
    0,    0,    0,    0,   24,   25,   26,   27,   28,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,   76,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,   76,    0,    0,
    0,    0,    0,    0,   77,    0,    0,    0,    0,   22,
    0,    0,    0,    0,    0,    0,    0,    0,   23,  732,
   77,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,   22,    0,    0,   76,   76,    0,    0,
   76,   76,   23,   76,   76,   76,   76,  667,    0,    0,
   24,   25,   26,   27,   28,    0,   22,  328,  328,   77,
   77,    0,    0,   77,   77,   23,   77,   77,   77,   77,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
  181,    0,    0,    0,   23,    0,    0,    0,    0,   23,
    0,    0,   24,   25,   26,   27,   28,   24,   25,   26,
   27,   28,    0,  328,  328,  328,    0,  328,  328,    0,
    0,    0,  328,    0,  328,    0,    0,  328,  328,  328,
  328,  328,  328,  328,  328,  328,  328,    0,  328,  328,
    0,  328,  328,  328,  328,  328,  328,  328,  328,  328,
  328,  328,  328,  328,  303,  303,  328,    0,    0,    0,
  328,  328,  328,  328,  328,    0,  328,  328,  328,  328,
  328,  328,  328,    0,  328,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  328,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  328,  328,  328,  328,
    0,  328,    0,    0,    0,    0,    0,    0,    0,    0,
  303,  303,  303,    0,  303,  303,    0,    0,    0,  303,
    0,  303,    0,    0,  303,  303,  303,  303,  303,  303,
  303,  303,  303,  303,    0,  303,  303,    0,  303,  303,
  303,  303,  303,  303,  303,  303,  303,  303,  303,  303,
  303,  287,  287,  303,    0,    0,    0,  303,  303,  303,
  303,  303,    0,  303,  303,  303,  303,  303,  303,  303,
    0,  303,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  303,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  303,  303,  303,  303,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,  287,  287,  287,
    0,  287,  287,    0,    0,    0,  287,    0,  287,    0,
    0,  287,  287,  287,  287,  287,  287,  287,  287,  287,
  287,    0,  287,  287,    0,  287,  287,  287,  287,  287,
  287,  287,  287,  287,  287,  287,  287,  287,  292,  292,
  287,    0,    0,    0,  287,  287,  287,  287,  287,    0,
  287,  287,  287,  287,  287,  287,  287,    0,  287,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  287,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  287,  287,  287,  287,    0,  287,    0,    0,    0,    0,
    0,    0,    0,    0,  292,  292,  292,    0,  292,  292,
    0,    0,    0,  292,    0,  292,    0,    0,  292,  292,
  292,  292,  292,  292,  292,  292,  292,  292,    0,  292,
  292,    0,  292,  292,  292,  292,  292,  292,  292,  292,
  292,  292,  292,  292,  292,  288,  288,  292,    0,    0,
    0,  292,  292,  292,  292,  292,    0,  292,  292,  292,
  292,  292,  292,  292,    0,  292,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  292,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  292,  292,  292,
  292,    0,  292,    0,    0,    0,    0,    0,    0,    0,
    0,  288,  288,  288,    0,  288,  288,    0,    0,    0,
  288,    0,  288,    0,    0,  288,  288,  288,  288,  288,
  288,  288,  288,  288,  288,    0,  288,  288,    0,  288,
  288,  288,  288,  288,  288,  288,  288,  288,  288,  288,
  288,  288,  297,  297,  288,    0,    0,    0,  288,  288,
  288,  288,  288,    0,  288,  288,  288,  288,  288,  288,
  288,    0,  288,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  288,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  288,  288,  288,  288,    0,  288,
    0,    0,    0,    0,    0,    0,    0,    0,  297,  297,
  297,    0,  297,  297,    0,    0,    0,  297,    0,  297,
    0,    0,  297,  297,  297,  297,  297,  297,  297,  297,
  297,  297,    0,  297,  297,    0,  297,  297,  297,  297,
  297,  297,  297,  297,  297,  297,  297,  297,  297,  318,
  318,  297,    0,    0,    0,  297,  297,  297,  297,  297,
    0,  297,  297,  297,  297,  297,  297,  297,    0,  297,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  297,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  297,  297,  297,  297,    0,  297,    0,    0,    0,
    0,    0,    0,    0,    0,  318,  318,  318,    0,  318,
  318,    0,    0,    0,  318,    0,  318,    0,    0,  318,
  318,  318,  318,  318,  318,  318,  318,  318,  318,    0,
  318,  318,    0,  318,  318,  318,  318,  318,  318,  318,
  318,  318,  318,  318,  318,  318,  314,  314,  318,    0,
    0,    0,  318,  318,  318,  318,  318,    0,  318,  318,
  318,  318,  318,  318,  318,    0,  318,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  318,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  318,  318,
  318,  318,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,  314,  314,  314,    0,  314,  314,    0,    0,
    0,  314,    0,  314,    0,    0,  314,  314,  314,  314,
  314,  314,  314,  314,  314,  314,    0,  314,  314,    0,
  314,  314,  314,  314,  314,  314,  314,  314,  314,  314,
  314,  314,  314,  293,  293,  314,    0,    0,    0,  314,
  314,  314,  314,  314,    0,  314,  314,  314,  314,  314,
  314,  314,    0,  314,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  314,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  314,  314,  314,  314,    0,
  314,    0,    0,    0,    0,    0,    0,    0,    0,  293,
  293,  293,    0,  293,  293,    0,    0,    0,  293,    0,
  293,    0,    0,  293,  293,  293,  293,  293,  293,  293,
  293,  293,  293,    0,  293,  293,    0,  293,  293,  293,
  293,  293,  293,  293,  293,  293,  293,  293,  293,  293,
  289,  289,  293,    0,    0,    0,  293,  293,  293,  293,
  293,    0,  293,  293,  293,  293,  293,  293,  293,    0,
  293,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  293,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  293,  293,  293,  293,    0,  293,    0,    0,
    0,    0,    0,    0,    0,    0,  289,  289,  289,    0,
  289,  289,    0,    0,    0,  289,    0,  289,    0,    0,
  289,  289,  289,  289,  289,  289,  289,  289,  289,  289,
    0,  289,  289,    0,  289,  289,  289,  289,  289,  289,
  289,  289,  289,  289,  289,  289,  289,  290,  290,  289,
    0,    0,    0,  289,  289,  289,  289,  289,    0,  289,
  289,  289,  289,  289,  289,  289,    0,  289,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  289,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  289,
  289,  289,  289,    0,  289,    0,    0,    0,    0,    0,
    0,    0,    0,  290,  290,  290,    0,  290,  290,    0,
    0,    0,  290,    0,  290,    0,    0,  290,  290,  290,
  290,  290,  290,  290,  290,  290,  290,    0,  290,  290,
    0,  290,  290,  290,  290,  290,  290,  290,  290,  290,
  290,  290,  290,  290,  294,  294,  290,    0,    0,    0,
  290,  290,  290,  290,  290,    0,  290,  290,  290,  290,
  290,  290,  290,    0,  290,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  290,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  290,  290,  290,  290,
    0,  290,    0,    0,    0,    0,    0,    0,    0,    0,
  294,  294,  294,    0,  294,  294,    0,    0,    0,  294,
    0,  294,    0,    0,  294,  294,  294,  294,  294,  294,
  294,  294,  294,  294,    0,  294,  294,    0,  294,  294,
  294,  294,  294,  294,  294,  294,  294,  294,  294,  294,
  294,  295,  295,  294,    0,    0,    0,  294,  294,  294,
  294,  294,    0,  294,  294,  294,  294,  294,  294,  294,
    0,  294,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  294,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  294,  294,  294,  294,    0,  294,    0,
    0,    0,    0,    0,    0,    0,    0,  295,  295,  295,
    0,  295,  295,    0,    0,    0,  295,    0,  295,    0,
    0,  295,  295,  295,  295,  295,  295,  295,  295,  295,
  295,    0,  295,  295,    0,  295,  295,  295,  295,  295,
  295,  295,  295,  295,  295,  295,  295,  295,  298,  298,
  295,    0,    0,    0,  295,  295,  295,  295,  295,    0,
  295,  295,  295,  295,  295,  295,  295,    0,  295,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  295,  295,  295,  295,    0,  295,    0,    0,    0,    0,
    0,    0,    0,    0,  298,  298,  298,    0,  298,  298,
    0,    0,    0,  298,    0,  298,    0,    0,  298,  298,
  298,  298,  298,  298,  298,  298,  298,  298,    0,  298,
  298,    0,  298,  298,  298,  298,  298,  298,  298,  298,
  298,  298,  298,  298,  298,  315,  315,  298,    0,    0,
    0,  298,  298,  298,  298,  298,    0,  298,  298,  298,
  298,  298,  298,  298,    0,  298,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  298,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  298,  298,  298,
  298,    0,  298,    0,    0,    0,    0,    0,    0,    0,
    0,  315,  315,  315,    0,  315,  315,    0,    0,    0,
  315,    0,  315,    0,    0,  315,  315,  315,  315,  315,
  315,  315,  315,  315,  315,    0,  315,  315,    0,  315,
  315,  315,  315,  315,  315,  315,  315,  315,  315,  315,
  315,  315,  291,  291,  315,    0,    0,    0,  315,  315,
  315,  315,  315,    0,  315,  315,  315,  315,  315,  315,
  315,    0,  315,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  315,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  315,  315,  315,  315,    0,  315,
    0,    0,    0,    0,    0,    0,    0,    0,  291,  291,
  291,    0,  291,  291,    0,    0,    0,  291,    0,  291,
    0,    0,  291,  291,  291,  291,  291,  291,  291,  291,
  291,  291,    0,  291,  291,    0,  291,  291,  291,  291,
  291,  291,  291,  291,  291,  291,  291,  291,  291,  296,
  296,  291,    0,    0,    0,  291,  291,  291,  291,  291,
    0,  291,  291,  291,  291,  291,  291,  291,    0,  291,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  291,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  291,  291,  291,  291,    0,  291,    0,    0,    0,
    0,    0,    0,    0,    0,  296,  296,  296,    0,  296,
  296,    0,    0,    0,  296,    0,  296,    0,    0,  296,
  296,  296,  296,  296,  296,  296,  296,  296,  296,    0,
  296,  296,    0,  296,  296,  296,  296,  296,  296,  296,
  296,  296,  296,  296,  296,  296,  299,  299,  296,    0,
    0,    0,  296,  296,  296,  296,  296,    0,  296,  296,
  296,  296,  296,  296,  296,    0,  296,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  296,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  296,  296,
  296,  296,    0,  296,    0,    0,    0,    0,    0,    0,
    0,    0,  299,  299,  299,    0,  299,  299,    0,    0,
    0,  299,    0,  299,    0,    0,  299,  299,  299,  299,
  299,  299,  299,  299,  299,  299,    0,  299,  299,    0,
  299,  299,  299,  299,  299,  299,  299,  299,  299,  299,
  299,  299,  299,  300,  300,  299,    0,    0,    0,  299,
  299,  299,  299,  299,    0,  299,  299,  299,  299,  299,
  299,  299,    0,  299,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  299,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  299,  299,  299,  299,    0,
  299,    0,    0,    0,    0,    0,    0,    0,    0,  300,
  300,  300,    0,  300,  300,    0,    0,    0,  300,    0,
  300,    0,    0,  300,  300,  300,  300,  300,  300,  300,
  300,  300,  300,    0,  300,  300,    0,  300,  300,  300,
  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  301,  301,  300,    0,    0,    0,  300,  300,  300,  300,
  300,    0,  300,  300,  300,  300,  300,  300,  300,    0,
  300,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  300,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  300,  300,  300,  300,    0,  300,    0,    0,
    0,    0,    0,    0,    0,    0,  301,  301,  301,    0,
  301,  301,    0,    0,    0,  301,    0,  301,    0,    0,
  301,  301,  301,  301,  301,  301,  301,  301,  301,  301,
    0,  301,  301,    0,  301,  301,  301,  301,  301,  301,
  301,  301,  301,  301,  301,  301,  301,  270,    0,  301,
    0,    0,    0,  301,  301,  301,  301,  301,    0,  301,
  301,  301,  301,  301,  301,  301,    0,  301,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  301,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  301,
  301,  301,  301,    0,  301,    0,    0,    0,    0,    0,
    0,    0,    0,  271,  272,  273,    0,  274,  275,    0,
    0,    0,  276,    0,  277,    0,    0,  278,  279,  280,
  281,  282,  283,  284,  285,  286,  287,    0,  288,  289,
    0,  290,  291,  292,  293,  294,  295,  296,  297,  298,
  299,  300,  301,  302,  233,    0,  303,    0,    0,    0,
  304,  305,  306,  307,  308,    0,  309,  310,  311,  312,
  313,  314,  315,    0,  316,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  317,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  318,  319,  320,  321,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
  233,  233,  233,    0,  233,  233,    0,    0,    0,  233,
    0,  233,    0,    0,  233,  233,  233,  233,  233,  233,
  233,  233,  233,  233,    0,  233,  233,    0,  233,  233,
  233,  233,  233,  233,  233,  233,  233,  233,  233,  233,
  233,  234,    0,  233,    0,    0,    0,  233,  233,  233,
  233,  233,    0,  233,  233,  233,  233,  233,  233,  233,
    0,  233,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  233,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  233,  233,  233,  233,    0,  233,    0,
    0,    0,    0,    0,    0,    0,    0,  234,  234,  234,
    0,  234,  234,    0,    0,    0,  234,    0,  234,    0,
    0,  234,  234,  234,  234,  234,  234,  234,  234,  234,
  234,    0,  234,  234,    0,  234,  234,  234,  234,  234,
  234,  234,  234,  234,  234,  234,  234,  234,  235,    0,
  234,    0,    0,    0,  234,  234,  234,  234,  234,    0,
  234,  234,  234,  234,  234,  234,  234,    0,  234,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  234,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  234,  234,  234,  234,    0,  234,    0,    0,    0,    0,
    0,    0,    0,    0,  235,  235,  235,    0,  235,  235,
    0,    0,    0,  235,    0,  235,    0,    0,  235,  235,
  235,  235,  235,  235,  235,  235,  235,  235,    0,  235,
  235,    0,  235,  235,  235,  235,  235,  235,  235,  235,
  235,  235,  235,  235,  235,  236,    0,  235,    0,    0,
    0,  235,  235,  235,  235,  235,    0,  235,  235,  235,
  235,  235,  235,  235,    0,  235,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  235,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  235,  235,  235,
  235,    0,  235,    0,    0,    0,    0,    0,    0,    0,
    0,  236,  236,  236,    0,  236,  236,    0,    0,    0,
  236,    0,  236,    0,    0,  236,  236,  236,  236,  236,
  236,  236,  236,  236,  236,    0,  236,  236,    0,  236,
  236,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,    0,    0,  236,    0,    0,    0,  236,  236,
  236,  236,  236,    0,  236,  236,  236,  236,  236,  236,
  236,    0,  236,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  236,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  236,  236,  236,  236,  277,  236,
    0,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,    0,  288,  289,    0,  290,  291,  292,  293,  294,
  295,  296,  297,  298,  299,  300,  301,  302,    0,    0,
  303,    0,    0,    0,  304,  305,  306,  307,  308,    0,
  309,  310,  311,  312,  313,  314,  315,    0,  316,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  317,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  318,  319,  320,  321,    0,  322,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   33,   57,    6,   60,   60,  106,  326,   33,  328,
  149,  493,   40,   60,  323,  123,   40,  218,   40,   42,
  123,   54,  666,   40,  123,  123,  725,  123,   44,   41,
   44,    0,   44,  123,  123,   91,  123,  561,  101,   60,
  540,  276,   49,   42,  266,  280,  309,  147,   40,   56,
   57,   44,   44,   60,  317,   61,   63,  756,   42,   92,
   63,  200,   44,  285,   19,   40,  590,  123,   75,   44,
   91,   44,  135,   44,   61,  138,  123,  309,   62,  218,
   62,   44,   89,   61,  502,  317,  730,   94,  143,    6,
    7,   98,  317,  100,   93,   98,  197,  100,  105,  199,
   61,  202,  123,  110,    0,  112,  240,  241,  242,  125,
  373,  125,  124,    0,   60,   41,  123,  124,   44,  126,
   42,  260,   77,   78,  624,  649,  650,  627,  652,  263,
   93,  332,  125,  125,  272,  198,    0,  836,  373,   56,
   57,  373,  260,   60,   40,   91,   63,  290,  291,  257,
  125,  106,  125,   41,  125,  474,   44,   76,  259,   78,
    0,  270,  271,   61,  171,  302,  173,  486,  586,   91,
  479,  274,   61,  374,  317,  274,  274,  123,  274,   41,
  499,   98,   44,  100,  274,  274,  279,  274,  281,  196,
  327,  328,  111,   40,  113,  288,  720,   41,  722,  723,
   44,  257,  258,  259,  387,  261,  262,  263,  391,  265,
  217,  394,  215,   42,  280,   44,  272,  273,  352,  257,
  403,  290,  291,  279,   41,  294,  295,   44,  109,   42,
  286,   40,   41,  240,  241,   44,   41,   40,    0,   44,
  287,  274,  274,  264,  272,  277,    0,  771,  272,  274,
  272,   61,  273,   41,  672,  272,   44,  576,  274,  578,
  281,  282,  283,  284,  285,   40,   41,  274,   40,   44,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  291,  292,  293,  320,  311,  170,
  260,  172,  299,  300,   41,  777,  303,   44,  260,  268,
  269,  274,   40,  272,  273,  120,  275,   42,  257,   44,
  728,  318,  319,  320,  267,  322,  372,  320,  264,  288,
  289,  739,   41,  290,  291,   44,  333,  273,  294,  295,
  386,  387,  388,   41,   42,  281,  282,  283,  284,  285,
   41,  123,  661,   44,  663,   41,   41,   42,   44,  318,
  165,  166,  167,   42,  375,  236,  774,  274,  239,  493,
   42,  495,   44,  497,  267,  180,   41,    0,   41,   44,
  788,   44,  268,  269,  302,  382,  272,   41,   42,  275,
  387,  268,  269,  373,  391,  272,  273,  394,  275,   42,
  397,   44,  288,  289,  123,  813,  403,  404,  123,  406,
  534,  288,  289,  320,  268,  269,   40,  222,  272,  273,
  417,  275,  227,  228,  229,   42,  423,   44,   42,   42,
   44,   44,  318,  123,  288,  289,   41,  373,  268,  269,
  257,  318,  272,  273,  249,  275,  374,  470,   41,   42,
  447,   41,   42,   41,   42,  166,  167,  317,  288,  289,
   40,   40,   40,   44,  318,   62,  271,  464,  273,   58,
  275,  468,  469,  470,   58,  382,  469,  470,   61,   40,
  274,   58,  257,   40,  373,  482,  483,  484,  318,  294,
  295,  296,  297,  298,   44,   44,  301,  373,  495,  304,
  305,  306,  307,  308,  309,  310,  311,  312,  313,  314,
  315,  260,  257,   61,  326,  414,  268,  269,  274,  274,
  272,  273,  567,  275,  268,  269,  309,  257,  272,  273,
  381,  275,  381,  338,  381,  373,  288,  289,   44,  257,
  373,  538,  373,  540,  288,  289,  543,  544,  545,  546,
  547,  548,  549,  550,  551,  552,  553,  554,  648,   44,
   40,  468,  469,  470,  186,    0,  318,  564,  124,  566,
  567,   60,  273,   44,  318,   44,  700,  701,  257,  258,
  259,   40,  261,  262,  263,  381,  265,    0,  717,   44,
   44,  588,   44,  272,  273,   44,   44,  381,  274,  381,
  279,   60,   91,  373,  381,  381,  257,  286,  381,  733,
  381,  257,  258,  259,  419,  261,  262,  263,  381,  265,
  381,  381,  381,  381,  669,  467,  257,  624,  718,  719,
  627,  721,   91,  279,  123,  373,  765,  766,  257,  768,
  286,   44,   40,   44,   44,  268,  269,   44,  266,  272,
  273,   44,  275,  777,   44,   44,   44,  564,  655,  566,
  567,   44,  655,  505,  123,  288,  289,   44,   44,  666,
  667,  800,  801,  802,   44,   60,   44,  767,    0,  769,
  770,   44,   44,   44,   44,   44,   44,   44,  817,   44,
  257,  257,   91,  372,  381,  318,   44,   44,  260,   44,
   42,   44,   44,   44,   44,   44,   91,  386,  387,  388,
   44,   44,   44,  803,  276,   44,  257,   44,   60,   44,
  562,  563,   91,  565,   44,   44,  531,  532,  533,   44,
  535,  536,   44,  730,   44,   44,   44,  542,  123,  381,
  737,  734,   44,  373,  373,   44,  373,    0,  655,   91,
   44,   44,   44,  315,  316,   40,  378,  319,  320,  257,
  322,  323,  324,  325,  386,  373,  571,  389,  390,  321,
  392,  393,  373,  395,  396,  264,  398,  399,  400,  401,
  402,  123,  257,  405,  273,  407,  408,  409,  410,  257,
   44,   40,  281,  282,  283,  284,  285,   93,  257,  258,
  259,  374,  261,  262,  263,   40,  265,   40,    0,  651,
  321,  653,  654,  272,  273,  257,  257,   40,  381,   44,
  279,  381,  819,  381,  629,    0,  272,  286,  825,  826,
  827,   40,  272,  268,  269,  272,    0,  272,  273,  272,
  275,   10,  647,   79,  466,  180,   19,  241,  179,  244,
  495,   89,  196,  288,  289,  268,  269,  213,  330,  272,
  273,   54,  275,  217,  797,  700,  375,  356,  325,  701,
    0,  737,  325,  730,   -1,  288,  289,  497,  646,  264,
   -1,   -1,  724,  318,   -1,  507,   -1,   -1,  273,  511,
   -1,   -1,  514,  698,  699,  517,  281,  282,  283,  284,
  285,  523,  524,   -1,  526,  318,   -1,   -1,   -1,   -1,
   40,   41,   42,  372,   44,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,  386,  387,  388,
   60,  273,   62,  555,  556,  557,   -1,  279,   -1,   -1,
   -1,   -1,   -1,   -1,  286,   -1,  268,  269,   -1,   -1,
  272,  273,  757,  275,   -1,   -1,  341,  342,   -1,  764,
   -1,   91,   -1,   93,   -1,   -1,  288,  289,   -1,  591,
   -1,  593,  594,   -1,  596,  597,   -1,  599,  600,   -1,
  602,  603,  604,  605,  606,   -1,   -1,  609,   -1,  611,
  612,  613,  614,  123,   -1,  125,  318,   -1,   -1,   -1,
   -1,   -1,   -1,  625,  809,  810,  811,   -1,   -1,   -1,
   40,   41,   -1,   -1,   44,  268,  269,   -1,   -1,  272,
  273,   -1,  275,  828,   -1,   -1,   -1,   -1,   -1,   -1,
  372,   -1,   62,   -1,   -1,  288,  289,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,  387,  388,  669,   -1,   42,
   -1,   -1,   -1,  675,   -1,   -1,  678,   -1,   -1,  681,
   -1,   -1,  684,   93,   -1,  318,   -1,   60,  690,  691,
   -1,  693,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,   -1,
  712,  713,  714,  123,   -1,  125,  288,  289,   91,   -1,
   -1,   -1,   -1,   -1,  268,  269,   -1,   40,  272,  273,
   -1,  275,  734,   -1,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,  288,  289,  318,  257,  258,  259,
  123,  261,  262,  263,   -1,  265,  288,  289,  268,  269,
   -1,   -1,  272,  273,  274,  275,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,  318,   -1,  286,   -1,  288,  289,
   -1,   -1,  292,   -1,   -1,   -1,  318,   -1,   -1,   -1,
   -1,   -1,   -1,  303,  304,   -1,  306,  307,   -1,   -1,
  310,  311,  312,  313,  314,  315,  316,   -1,  318,  391,
  392,  393,  394,  395,  396,  397,  398,  399,  400,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   40,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
   -1,   -1,  372,  273,  274,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  401,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,  412,  413,  414,  415,  279,  417,   -1,   -1,
   -1,   -1,   -1,  286,   -1,   -1,   -1,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   40,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,  381,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  372,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
   -1,   -1,   -1,  386,  387,  388,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   40,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,   -1,   -1,  372,
  273,  274,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  412,
  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,  276,
   -1,   -1,  279,   -1,   -1,   -1,  329,  330,  331,  286,
  333,  334,   -1,   -1,   40,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,   -1,   -1,  372,
  273,  274,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  290,  291,   -1,  401,  294,
  295,  296,  297,  298,  299,  300,  301,   -1,   -1,  412,
  413,  414,  415,  308,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   40,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  273,  274,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   40,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,   -1,   -1,  372,  273,  274,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   40,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,   -1,   -1,  372,  273,  274,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   40,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  273,  274,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   40,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,   -1,   -1,  372,  273,  274,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   40,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,   -1,   -1,  372,  273,  274,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   40,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,   -1,   -1,
  372,  273,  274,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   42,
  412,  413,  414,  415,   -1,  417,   -1,   40,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   60,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,   91,   -1,
  372,  273,  274,   42,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,  123,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,   -1,   -1,
  372,   42,   -1,   44,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   42,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   60,   42,  261,  262,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,  123,   60,   91,   -1,  279,   -1,   -1,  292,
   -1,   -1,   -1,  286,   -1,   -1,   -1,   -1,   -1,   60,
  303,  304,   -1,  306,  307,   -1,   -1,  310,   -1,  312,
  313,  314,  315,  316,   91,   -1,  123,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   60,   -1,
   91,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,   -1,  123,  286,   -1,   -1,
   60,   -1,   -1,  292,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,  123,   -1,  303,  304,   -1,  306,  307,   -1,
   -1,  310,   -1,  312,  313,  314,  315,  316,   60,  372,
   -1,   91,   -1,   60,   -1,   -1,   -1,  393,  394,  395,
  396,  123,   -1,  386,  387,  388,  402,  403,  404,  405,
  406,  407,  408,  409,  410,  411,   -1,   -1,   -1,   91,
   -1,   -1,   -1,  123,   91,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,  125,
   -1,   -1,  273,  372,   -1,   -1,   -1,   -1,  279,   -1,
   -1,  123,   -1,   -1,   -1,  286,  123,  386,  387,  388,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  257,
  258,  259,  279,  261,  262,  263,   -1,  265,   -1,  286,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   41,  279,   -1,   -1,   -1,  272,  273,   -1,  286,   -1,
   -1,   -1,  279,  264,   -1,   -1,   41,   -1,   -1,  286,
   -1,   -1,  273,   -1,   -1,  292,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   60,  303,  304,   -1,  306,
  307,  372,  264,  310,   -1,  312,  313,  314,  315,  316,
   -1,  273,   -1,   -1,   -1,  386,  387,  388,  125,  281,
  282,  283,  284,  285,  264,   -1,   91,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,  372,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,  273,   -1,  386,
  387,  388,  264,   -1,   -1,   -1,   -1,  264,  123,   -1,
   -1,  273,   -1,   -1,   -1,  372,  273,   -1,   -1,  281,
  282,  283,  284,  285,  281,  282,  283,  284,  285,  386,
  387,  388,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  305,  356,   -1,   -1,   -1,  305,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,  125,  356,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,  276,   -1,   -1,  279,  264,
   -1,   -1,   -1,   -1,   -1,  286,   -1,   -1,  273,   -1,
   -1,   -1,  125,  278,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  329,  330,  331,   -1,  333,  334,  293,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  273,   -1,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,  125,
  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  273,   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,  125,  417,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  273,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   91,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  273,   60,  372,   -1,  123,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   91,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,  123,  338,
   -1,  340,   91,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,   -1,   60,  372,  123,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   91,   -1,   -1,   -1,   -1,   -1,  292,
   60,   -1,   -1,  412,  413,  414,  415,   -1,  417,  264,
  303,  304,   -1,  306,  307,   -1,   -1,  310,  273,  312,
  313,  314,  315,  316,   -1,  123,  281,  282,  283,  284,
  285,   91,   -1,   -1,   -1,   -1,   60,  292,   -1,  125,
   -1,   -1,  297,  298,  299,  300,  301,  302,  303,  304,
   -1,  306,  307,  308,   -1,  310,   -1,  312,  313,  314,
  315,  316,   -1,  123,   -1,   -1,   -1,   91,   -1,  264,
   -1,   -1,  327,  328,   -1,   -1,   -1,   41,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   60,  292,   -1,  123,
   -1,   -1,   -1,   -1,   -1,  264,   41,  302,  303,  304,
   -1,  306,  307,   41,  273,  310,   -1,  312,  313,  314,
  315,  316,  281,  282,  283,  284,  285,   91,   -1,   -1,
   -1,   -1,   60,  292,   -1,   -1,   -1,   -1,   -1,   60,
   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,   -1,
   -1,  310,   -1,  312,  313,  314,  315,  316,   -1,  123,
   -1,   -1,   -1,   91,   -1,   60,  264,   -1,   -1,   -1,
   91,   -1,   60,   -1,  260,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
  276,   -1,   -1,   -1,  292,  123,   91,   -1,   -1,   -1,
   -1,   -1,  123,   91,  264,  303,  304,   -1,  306,  307,
   60,   -1,  310,  273,  312,  313,  314,  315,  316,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,  123,  315,
  316,   -1,  292,  319,  320,  123,  322,  323,  324,  325,
  264,   91,   -1,  303,  304,   60,  306,  307,   -1,  273,
  310,   -1,  312,  313,  314,  315,  316,  281,  282,  283,
  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,  292,   -1,
   60,   -1,   -1,  123,   -1,   60,   91,   -1,   -1,  303,
  304,   -1,  306,  307,   -1,   -1,  310,   -1,  312,  313,
  314,  315,  316,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   91,   -1,   60,   -1,   -1,   91,   -1,  123,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  257,  258,  259,   -1,  261,  262,  263,  293,
  265,   -1,   -1,  123,   91,  125,  264,   60,  123,   -1,
   -1,  276,   -1,  264,  279,  273,   -1,   -1,   -1,   -1,
  278,  286,  273,  281,  282,  283,  284,  285,   -1,   -1,
  281,  282,  283,  284,  285,   -1,  123,   -1,   91,  264,
   -1,   -1,   -1,   -1,   -1,   60,  264,   -1,  273,   -1,
   -1,  302,   -1,  278,   -1,  273,  281,  282,  283,  284,
  285,   -1,   -1,  281,  282,  283,  284,  285,  293,   60,
  123,   -1,  125,   -1,   -1,  293,   91,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  125,   -1,   -1,   -1,
   -1,   -1,   60,  273,  274,   -1,  276,   -1,   -1,  279,
   91,  281,  282,  283,  284,  285,  286,   -1,  123,  125,
   -1,   -1,  257,  258,  259,  260,  261,  262,  263,  264,
  265,   60,   -1,   91,   -1,   93,   60,   -1,  273,  274,
   -1,  276,  123,   -1,  279,   -1,  281,  282,  283,  284,
  285,  286,   -1,   -1,  264,  265,   -1,   -1,   -1,  264,
  265,   -1,   91,  273,  274,  123,   -1,   91,  273,  274,
   -1,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,  264,  265,   -1,
   -1,   -1,   -1,   -1,  123,   -1,  273,  274,   -1,  123,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  260,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,  276,   -1,   -1,
   -1,   -1,   -1,   -1,  260,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
  276,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,  264,   -1,   -1,  315,  316,   -1,   -1,
  319,  320,  273,  322,  323,  324,  325,  278,   -1,   -1,
  281,  282,  283,  284,  285,   -1,  264,  273,  274,  315,
  316,   -1,   -1,  319,  320,  273,  322,  323,  324,  325,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,
   -1,   -1,  281,  282,  283,  284,  285,  281,  282,  283,
  284,  285,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  273,  274,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  273,  274,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  273,  274,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  273,  274,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  273,
  274,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  273,  274,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  273,  274,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  273,  274,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  273,  274,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  273,  274,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  273,  274,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  273,  274,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  273,
  274,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  273,  274,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  273,  274,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  273,  274,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  273,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  273,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  273,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  273,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  273,   -1,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,  340,  417,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,   -1,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,
  };

#line 1242 "Iril/IR/IR.jay"

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
  public const int LINKONCE = 299;
  public const int LINKONCE_ODR = 300;
  public const int WEAK = 301;
  public const int FASTCC = 302;
  public const int SIGNEXT = 303;
  public const int ZEROEXT = 304;
  public const int VOLATILE = 305;
  public const int RETURNED = 306;
  public const int DEREFERENCEABLE = 307;
  public const int AVAILABLE_EXTERNALLY = 308;
  public const int PERSONALITY = 309;
  public const int SRET = 310;
  public const int CLEANUP = 311;
  public const int NONNULL = 312;
  public const int NOCAPTURE = 313;
  public const int WRITEONLY = 314;
  public const int READONLY = 315;
  public const int READNONE = 316;
  public const int ATTRIBUTE_GROUP_REF = 317;
  public const int ATTRIBUTES = 318;
  public const int NORECURSE = 319;
  public const int NOUNWIND = 320;
  public const int UNWIND = 321;
  public const int SPECULATABLE = 322;
  public const int SSP = 323;
  public const int UWTABLE = 324;
  public const int ARGMEMONLY = 325;
  public const int SEQ_CST = 326;
  public const int DSO_LOCAL = 327;
  public const int DSO_PREEMPTABLE = 328;
  public const int RET = 329;
  public const int BR = 330;
  public const int SWITCH = 331;
  public const int INDIRECTBR = 332;
  public const int INVOKE = 333;
  public const int RESUME = 334;
  public const int CATCHSWITCH = 335;
  public const int CATCHRET = 336;
  public const int CLEANUPRET = 337;
  public const int UNREACHABLE = 338;
  public const int FNEG = 339;
  public const int ADD = 340;
  public const int NUW = 341;
  public const int NSW = 342;
  public const int FADD = 343;
  public const int SUB = 344;
  public const int FSUB = 345;
  public const int MUL = 346;
  public const int FMUL = 347;
  public const int UDIV = 348;
  public const int SDIV = 349;
  public const int FDIV = 350;
  public const int UREM = 351;
  public const int SREM = 352;
  public const int FREM = 353;
  public const int SHL = 354;
  public const int LSHR = 355;
  public const int EXACT = 356;
  public const int ASHR = 357;
  public const int AND = 358;
  public const int OR = 359;
  public const int XOR = 360;
  public const int EXTRACTELEMENT = 361;
  public const int INSERTELEMENT = 362;
  public const int SHUFFLEVECTOR = 363;
  public const int EXTRACTVALUE = 364;
  public const int INSERTVALUE = 365;
  public const int ALLOCA = 366;
  public const int LOAD = 367;
  public const int STORE = 368;
  public const int FENCE = 369;
  public const int CMPXCHG = 370;
  public const int ATOMICRMW = 371;
  public const int GETELEMENTPTR = 372;
  public const int ALIGN = 373;
  public const int INBOUNDS = 374;
  public const int INRANGE = 375;
  public const int TRUNC = 376;
  public const int ZEXT = 377;
  public const int SEXT = 378;
  public const int FPTRUNC = 379;
  public const int FPEXT = 380;
  public const int TO = 381;
  public const int FPTOUI = 382;
  public const int FPTOSI = 383;
  public const int UITOFP = 384;
  public const int SITOFP = 385;
  public const int PTRTOINT = 386;
  public const int INTTOPTR = 387;
  public const int BITCAST = 388;
  public const int ADDRSPACECAST = 389;
  public const int ICMP = 390;
  public const int EQ = 391;
  public const int NE = 392;
  public const int UGT = 393;
  public const int UGE = 394;
  public const int ULT = 395;
  public const int ULE = 396;
  public const int SGT = 397;
  public const int SGE = 398;
  public const int SLT = 399;
  public const int SLE = 400;
  public const int FCMP = 401;
  public const int OEQ = 402;
  public const int OGT = 403;
  public const int OGE = 404;
  public const int OLT = 405;
  public const int OLE = 406;
  public const int ONE = 407;
  public const int ORD = 408;
  public const int UEQ = 409;
  public const int UNE = 410;
  public const int UNO = 411;
  public const int PHI = 412;
  public const int SELECT = 413;
  public const int CALL = 414;
  public const int TAIL = 415;
  public const int VA_ARG = 416;
  public const int LANDINGPAD = 417;
  public const int CATCHPAD = 418;
  public const int CLEANUPPAD = 419;
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
