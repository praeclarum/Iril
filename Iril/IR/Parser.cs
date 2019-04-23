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
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type nonglobal_value",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage visibility_style global_kind type",
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility_style function_addr global_kind type nonglobal_value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility_style global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
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
//t    "linkage : WEAK_ODR",
//t    "visibility : PRIVATE",
//t    "visibility_style : HIDDEN",
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
//t    "define_header : DEFINE define_header_attributes visibility_style return_type",
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
//t    "block : assignments terminator_assignment",
//t    "block : assignments terminator_assignment metadata_kvs",
//t    "block : terminator_assignment",
//t    "block : terminator_assignment metadata_kvs",
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
//t    "terminator_assignment : terminator_instruction",
//t    "terminator_assignment : LOCAL_SYMBOL '=' invoke_instruction",
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR INTEGER_TYPE value ',' label_value ',' label_value",
//t    "terminator_instruction : RESUME typed_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "terminator_instruction : UNREACHABLE",
//t    "terminator_instruction : invoke_instruction",
//t    "invoke_instruction : INVOKE return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "invoke_instruction : INVOKE parameter_attributes return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "invoke_instruction : INVOKE calling_convention return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "invoke_instruction : INVOKE calling_convention parameter_attributes return_type function_pointer function_args TO label_value UNWIND label_value",
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
//t    "instruction : LANDINGPAD type CATCH typed_value",
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
    "WEAK_ODR","FASTCC","SIGNEXT","ZEROEXT","VOLATILE","RETURNED",
    "DEREFERENCEABLE","AVAILABLE_EXTERNALLY","PERSONALITY","SRET",
    "CLEANUP","NONNULL","NOCAPTURE","WRITEONLY","READONLY","READNONE",
    "HIDDEN","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND",
    "UNWIND","SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST",
    "DSO_LOCAL","DSO_PREEMPTABLE","RET","BR","SWITCH","INDIRECTBR",
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
    "PHI","SELECT","CALL","TAIL","VA_ARG","LANDINGPAD","CATCH","CATCHPAD",
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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 19:
#line 126 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-3+yyTop], isConstant: (bool)yyVals[-3+yyTop]);
    }
  break;
case 20:
#line 130 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 21:
#line 134 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 138 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 23:
#line 142 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 24:
#line 146 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 25:
#line 150 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 26:
#line 154 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 27:
#line 158 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 28:
#line 162 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 29:
#line 166 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 30:
#line 170 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 31:
#line 174 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 32:
#line 178 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 33:
#line 182 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 34:
#line 186 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 35:
#line 187 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 36:
#line 191 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 192 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 193 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 39:
#line 194 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 40:
#line 195 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
#line 196 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 42:
#line 197 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 201 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 205 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
  case_45();
  break;
case 46:
  case_46();
  break;
case 47:
#line 222 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 48:
#line 223 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 49:
#line 224 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 50:
#line 228 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 51:
#line 232 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 62:
#line 261 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 63:
#line 265 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 64:
#line 272 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 65:
#line 276 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 66:
#line 280 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 67:
#line 284 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 68:
#line 288 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 86:
#line 321 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 87:
#line 325 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 88:
#line 329 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 89:
#line 336 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 90:
#line 340 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 92:
#line 345 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 95:
#line 351 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 96:
#line 352 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 97:
#line 353 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 98:
#line 354 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 99:
#line 358 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 100:
#line 362 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 366 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 102:
#line 370 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 103:
#line 374 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 378 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 382 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 106:
#line 389 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 393 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 401 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 110:
  case_110();
  break;
case 111:
  case_111();
  break;
case 121:
#line 433 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 122:
#line 437 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 123:
#line 441 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 124:
#line 445 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 125:
#line 449 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 126:
#line 456 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 127:
#line 460 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 128:
#line 464 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 133:
#line 475 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 134:
#line 482 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 135:
#line 486 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 136:
#line 490 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 137:
#line 494 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 138:
#line 498 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 508 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 143:
#line 509 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 144:
#line 516 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 145:
#line 520 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 146:
#line 527 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 531 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 148:
#line 535 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 149:
#line 539 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 151:
#line 547 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 551 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 153:
#line 552 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 154:
#line 553 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 155:
#line 554 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 156:
#line 555 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 157:
#line 556 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 158:
#line 557 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 159:
#line 558 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 160:
#line 559 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 161:
#line 560 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 162:
#line 564 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 170:
#line 587 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 171:
#line 588 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 172:
#line 589 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 173:
#line 590 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 174:
#line 591 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 175:
#line 592 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 176:
#line 593 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 177:
#line 594 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 178:
#line 595 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 179:
#line 596 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 180:
#line 600 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 181:
#line 601 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 182:
#line 602 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 183:
#line 603 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 184:
#line 604 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 185:
#line 605 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 186:
#line 606 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 187:
#line 607 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 188:
#line 608 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 189:
#line 609 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 190:
#line 610 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 191:
#line 611 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 192:
#line 612 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 193:
#line 613 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 194:
#line 614 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 195:
#line 615 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 196:
#line 619 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 200:
#line 629 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 201:
#line 633 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 202:
#line 637 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 203:
#line 641 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 204:
#line 645 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 205:
#line 649 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 206:
#line 653 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 207:
#line 657 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 665 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 210:
#line 666 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 211:
#line 667 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 212:
#line 668 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 213:
#line 669 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 214:
#line 670 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 215:
#line 671 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 216:
#line 672 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 217:
#line 673 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 218:
#line 680 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 687 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 691 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 221:
#line 698 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 705 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 709 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 716 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 724 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 227:
#line 731 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 735 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 746 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 750 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 757 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 761 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 768 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 772 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 236:
#line 776 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 780 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 238:
#line 787 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 791 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 798 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 802 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 806 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 810 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 818 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 246:
#line 819 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 247:
#line 826 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 830 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 837 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 250:
#line 841 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 251:
#line 845 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 252:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 253:
#line 853 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 254:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 255:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 257:
#line 866 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 258:
#line 870 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 259:
#line 874 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 260:
#line 878 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 261:
#line 882 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 266:
#line 899 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 909 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 269:
#line 916 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 920 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 927 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 945 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 278:
#line 952 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 956 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 963 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 967 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 971 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 975 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 979 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 285:
#line 983 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 287:
#line 991 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 995 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 999 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1003 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1011 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1015 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1019 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 294:
#line 1023 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 295:
#line 1027 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1031 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 297:
#line 1035 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 298:
#line 1039 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1043 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 300:
#line 1047 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 301:
#line 1051 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 302:
#line 1055 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 303:
#line 1059 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 304:
#line 1063 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 305:
#line 1067 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 306:
#line 1071 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 307:
#line 1075 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 308:
#line 1079 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 309:
#line 1083 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 310:
#line 1087 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 311:
#line 1091 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 312:
#line 1095 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 313:
#line 1099 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 314:
#line 1103 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1107 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1111 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1115 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1119 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1123 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1127 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1131 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1135 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1139 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1143 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1147 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1151 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1155 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1159 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1163 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1167 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1171 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1175 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 333:
#line 1179 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1183 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 335:
#line 1187 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 336:
#line 1191 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 337:
#line 1195 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 338:
#line 1199 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1203 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1207 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1211 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1215 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1219 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1223 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1227 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1231 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1235 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1239 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1243 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1247 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1251 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1255 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 353:
#line 1259 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 354:
#line 1263 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1267 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1271 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1275 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1279 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1283 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1287 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1291 "Iril/IR/IR.jay"
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

void case_45()
#line 210 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_46()
#line 215 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_110()
#line 406 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_111()
#line 411 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    6,   11,   11,   10,   10,   10,   10,   10,
   10,   10,   17,   14,    9,    9,   18,   18,   18,   18,
   18,   19,   22,   22,   23,   24,   24,   24,   24,   24,
   24,   15,   15,    8,    8,    8,    8,    8,   26,   26,
   26,    7,    7,   28,   28,   28,   28,   28,   28,   28,
   28,   28,   28,   28,   28,    3,    3,    3,   29,   29,
   30,   30,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   31,   31,   32,   32,    4,
    4,   35,   35,   35,   35,   35,   35,   35,   35,   35,
   33,   33,   33,   33,   33,   40,   40,   40,   40,   40,
   40,   40,   38,    5,    5,    5,    5,    5,   44,   44,
   44,   34,   34,   45,   45,   46,   46,   46,   46,   41,
   41,   39,   39,   39,   39,   39,   39,   39,   39,   39,
   39,   39,   16,   16,   42,   42,   37,   37,   47,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   48,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   50,   51,   51,   13,   13,
   13,   13,   13,   13,   13,   13,   13,   54,   20,   20,
   20,   20,   20,   20,   20,   20,   20,   55,   27,   27,
   56,   53,   53,   25,   57,   57,   52,   52,   58,   59,
   59,   36,   36,   60,   60,   60,   60,   61,   61,   63,
   63,   63,   63,   65,   66,   66,   67,   67,   68,   68,
   68,   68,   68,   68,   68,   69,   69,   69,   69,   69,
   69,   21,   21,   70,   70,   71,   71,   72,   73,   73,
   74,   75,   75,   76,   76,   43,   77,   62,   62,   78,
   78,   78,   78,   78,   78,   78,   79,   79,   79,   79,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    6,    6,    9,
   10,   10,   10,   10,    7,   11,    9,   10,   11,    9,
   10,    8,    5,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    3,    3,    3,    3,    6,
    5,    1,    1,    3,    1,    1,    1,    1,    1,    1,
    1,    2,    3,    1,    2,    3,    3,    3,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    3,    1,    1,    1,    4,    2,    3,    5,    1,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    4,
    2,    4,    1,    5,    5,    1,    3,    1,    1,    7,
    8,    1,    2,    4,    3,    5,    1,    3,    2,    4,
    2,    3,    3,    4,    4,    1,    1,    1,    1,    2,
    3,    2,    2,    4,    5,    6,    6,    7,    1,    2,
    1,    3,    2,    1,    3,    1,    2,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    4,    1,    1,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    6,    9,    6,    6,    3,    3,    3,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    2,    2,    1,
    2,    1,    3,    2,    1,    2,    1,    3,    1,    1,
    3,    1,    2,    2,    3,    1,    2,    1,    2,    1,
    2,    3,    4,    1,    3,    2,    1,    3,    2,    3,
    3,    3,    2,    4,    5,    1,    1,    6,    9,    6,
    6,    1,    3,    1,    1,    1,    3,    5,    1,    2,
    3,    1,    2,    1,    1,    1,    1,    1,    3,    2,
    7,    2,    2,    7,    1,    1,    8,    9,    9,   10,
    5,    6,    5,    7,    5,    5,    6,    4,    4,    5,
    6,    6,    7,    5,    6,    6,    6,    7,    5,    6,
    7,    7,    8,    4,    4,    5,    6,    5,    2,    5,
    4,    4,    4,    4,    5,    6,    7,    6,    6,    6,
    4,    3,    4,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   92,  103,   95,   96,   97,   98,   94,  126,   38,
   36,   39,   40,   41,   42,  276,  157,  158,  159,    0,
   37,  160,  152,  153,  155,  154,  156,  165,  166,    0,
    0,    0,   93,    0,    0,    0,    0,    0,  127,  128,
    0,    0,  150,    0,    0,    3,    0,    4,    0,    0,
  163,  164,   34,   35,   43,   44,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   86,    0,    0,    0,
    0,    0,    0,    0,  132,    0,    0,    0,  161,    0,
    0,    0,    0,    0,    0,    0,  151,    0,    0,    0,
    5,    6,    0,    0,    0,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   87,    0,
    0,    0,    0,  131,    0,  109,   99,    0,    0,  106,
    0,    0,    0,    0,    0,    0,    0,  148,  149,  143,
    0,    0,  144,  169,    0,    0,    0,  167,    0,    0,
    0,  211,  212,  210,  213,  214,  215,  209,  200,  217,
  216,    0,    0,    0,    0,    0,    0,    0,    0,  199,
    0,    0,    0,    0,    0,    0,    0,    0,   45,    0,
    0,    0,   71,   70,   13,    0,    0,   64,   69,  162,
    0,    0,    0,    0,  102,  100,    0,    0,    0,    0,
    0,  135,    0,    0,    0,   84,   76,   74,   75,   77,
   78,   79,   80,    0,   72,    0,  142,    0,    0,    0,
    0,    0,    0,    0,  119,  168,    0,    0,    0,    0,
    0,    0,    0,    0,  222,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   65,   14,    0,  196,  198,  197,  219,  104,   88,  105,
  107,  136,    0,    0,  137,    0,    0,   12,   73,  145,
    0,  115,   62,    0,    0,    0,    0,    0,    0,  285,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  232,    0,    0,
  238,    0,  278,  286,    0,    0,  133,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  207,    0,  205,  206,
    0,    0,    0,    0,    0,   58,   61,    0,   56,    0,
   47,   59,    0,   53,   55,   60,   57,   48,   49,   46,
   17,   16,   68,   67,   66,  138,   81,  265,  264,    0,
  262,    0,    0,  283,    0,    0,  280,    0,    0,    0,
    0,  282,  274,  275,    0,    0,  272,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  277,
  319,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  170,  171,  172,  173,  174,
  175,  176,  177,  178,  179,    0,  180,  181,  192,  193,
  194,  195,  183,  185,  186,  187,  188,  184,  182,  190,
  191,  189,    0,    0,    0,    0,    0,    0,    0,    0,
  110,  233,    0,  239,    0,    0,   63,    0,  120,   32,
    0,    0,    0,    0,    0,    0,    0,  223,    0,    0,
    0,    0,    0,    0,  224,    0,   85,    0,  116,    0,
  279,  218,    0,    0,  244,    0,    0,    0,    0,    0,
    0,  273,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  266,    0,    0,    0,    0,    0,    0,    0,
    0,  332,    0,    0,  111,    0,   27,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   51,    0,   54,
  263,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  314,    0,    0,  229,  230,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  333,    0,    0,    0,  204,  201,
  203,    0,   22,    0,    0,   50,    0,    0,    0,  246,
    0,    0,  247,    0,    0,    0,    0,  291,    0,  316,
  354,    0,  325,  339,    0,  320,  357,    0,  343,  318,
  359,  351,  347,    0,    0,  336,    0,  296,  295,  338,
  360,    0,    0,    0,    0,  293,    0,    0,  208,  221,
    0,    0,    0,    0,    0,    0,    0,    0,  267,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  269,    0,    0,    0,  249,  245,
    0,    0,    0,    0,    0,  292,  355,  340,  344,  348,
  337,  297,  329,  349,  231,    0,    0,    0,    0,    0,
    0,    0,    0,  225,    0,  227,  328,  317,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  281,    0,
  284,  270,    0,  257,  251,    0,    0,    0,    0,  256,
  252,  250,  248,    0,    0,    0,    0,  294,    0,  334,
    0,  352,    0,  226,    0,  268,  345,    0,    0,    0,
    0,  202,  271,  254,    0,    0,    0,    0,    0,  287,
    0,    0,    0,  335,  353,  228,    0,  255,    0,    0,
    0,    0,  288,  289,    0,    0,    0,    0,    0,  290,
    0,    0,    0,    0,    0,  261,  258,  260,    0,    0,
  259,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   53,   12,   13,   14,  224,  196,  188,   54,
   78,  197,  265,   79,  232,  210,   81,  189,  371,  180,
  390,  373,  374,  375,  376,  198,  794,  225,   89,   90,
  139,  140,   15,  110,  156,  337,  211,  235,   63,   58,
   64,   59,   60,  212,  152,  153,  158,  466,  483,  266,
  525,  795,  246,  740,  397,  655,  796,  648,  649,  338,
  339,  340,  341,  342,  526,  616,  702,  703,  821,  391,
  582,  583,  764,  765,  406,  407,  441,  343,  344,
  };
  protected static readonly short [] yySindex = {          606,
  -25,   34,    7,   12,   17, 3376, 3651, -281,    0,  606,
    0,    0,    0,    0, -230, -176,   29,   32, 1089, -165,
  -30,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  116,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3869,
  -98,  -94,    0, -143,  142,  132, 4071, 3436,    0,    0,
 3705,  -22,    0, 3705,  136,    0,  185,    0,  -47,   24,
    0,    0,    0,    0,    0,    0,   49, 4071,  144,   30,
   51,  -41,  228,  -23,  191,   89,    0,  142,  -11,  132,
   52, 4071,   84,   71,    0,   18, 3773,  132,    0, 4071,
  132, 3705,  -20, 3705,  185,  -19,    0,  265, 3767, -126,
    0,    0, 4071,   30,   30, 2567, 4071,   30, 4071,   30,
    0,  280,    0, -252,  369,  300, 3882,  392,    0, 4071,
 4071,   15, 4071,    0,  169,    0,    0,  142,  147,    0,
  132,  132,  185,  -18, -126,  185, 2700,    0,    0,    0,
 1160,  161,    0,    0,  121, -112, -270,    0, 2459, 4071,
 4071,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   83,  407,  417,  430, 4084, 4084, 4084,  427,    0,
 2567, 4071, 2567, 4071,  416,  418,  421,  182,    0, -252,
 3915,    0,    0,    0,    0,  -15, 2493,    0,    0,    0,
  142,    8,  422,    6,    0,    0, 3751, -126,  185,  121,
  121,    0, -126,  424,  450,    0,    0,    0,    0,    0,
    0,    0,    0, -118,    0,  732,    0, 3718, -263,  217,
 6021, -108, 4084,  235,    0,    0,  119,  452,  142, 2558,
  457, 4084, 4084, 4084,    0,   16,   74,   -1,  123,  458,
 2567,  459, 2567, 3818, 3853, 1443,    0, -252,  192,   -5,
    0,    0, 3938,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -126,  121,    0,  246, 1651,    0,    0,    0,
  256,    0,    0,  453, 4084, -133, 4084, 3603, 4084,    0,
 2720, 4071, 2720, 4071, 2720, 4071, 4071, 2637, 4071, 4071,
 4071, 2720, 2680, 2686, 4071, 4071, 4071, 4084, 4084, 4084,
 4084, 4084, 4071, 2727, 3664,  187,  -54, 4084, 4084, 4084,
 4084, 4084, 4084, 4084, 4084, 4084, 4084, 4084, 4084, 1097,
 1217, 4071, 4071, 3603,  100, 4071, 2726,    0, 6021,  243,
    0,  243,    0,    0,  245, 6021,    0,  211,  274,  158,
  159,  478, 4071,  153,  160,  166,    0, 4084,    0,    0,
  284,  167,  500,  177,  513,    0,    0,  519,    0, 1096,
    0,    0,  436,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  194,
    0,  211, 6493,    0,  304, 2666,    0,  529,  742, 3705,
 3705,    0,    0,    0, 2493, 2720,    0, 2493, 2493, 2720,
 2493, 2493, 2720, 2493, 2493, 4071, 2493, 2493, 2493, 2493,
 2493, 2720, 4071, 2493, 4071, 2493, 2493, 2493, 2493,  536,
  539,  542,  544,  545,   62, 4071,  106, 4084,  546,    0,
    0, 4071,  214,  208,  209,  213,  215,  216,  220,  221,
  224,  229,  230,  231,  232,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4071,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4071,   41, 2493,  742, 4071, 3705, 3603,  -29,
    0,    0,  243,    0,  323,  323,    0, 2826,    0,    0,
  359,  360,  225,  293, 4071, 4071, 4071,    0,  243,  361,
  244,  363,  248,  410,    0, 3853,    0, 1651,    0,  243,
    0,    0,  581,  364,    0,  587,  742,  742, 3705,  585,
 2493,    0,  588,  590, 2493,  592,  593, 2493,  594,  595,
 2493,  596,  598,  599,  600,  602, 2493, 2493,  604, 2493,
  605,  614,  619,  621, 4084, 4084, 4084, 1443, 4084, 2612,
  324, 4071,  622, 4071,  331, 4084, 4071, 4071, 4071, 4071,
 4071, 4071, 4071, 4071, 4071, 4071, 4071, 4071, 2493, 2493,
 2666,  626,    0,  630,  587,  742,  742, 4071,  742, 4071,
 3705,    0, 4084,  323,    0,  243,    0,  419, 4084,  316,
  390,  402,  323,  243,  420,  243,  426,    0,  271,    0,
    0,  323,  364,  540, 3789,  296,  587,  587,  742, 2666,
  636, 2666, 2666,  637, 2666, 2666,  643, 2666, 2666,  644,
 2666, 2666, 2666, 2666, 2666,  647,  650, 2666,  651, 2666,
 2666, 2666, 2666,    0,  653,  657,    0,    0,  662,  663,
  451,  665, 4071, 2493,  666, 4071,  667, 4084,  669,  142,
  142,  142,  142,  142,  142,  142,  142,  142,  142,  142,
  142,  670,  676,  679,  638, 4084,  121,  587,  587,  742,
  587,  742,  742, 3705,    0,  323,  243,  686,    0,    0,
    0,  323,    0,  323,  243,    0,  688, 4071, 3999,    0,
 2394,  321,    0,  364,  354,  355,  587,    0, 2666,    0,
    0, 2666,    0,    0, 2666,    0,    0, 2666,    0,    0,
    0,    0,    0, 2666, 2666,    0, 2666,    0,    0,    0,
    0, 4084, 4084, 1443, 1443,    0,  366,  695,    0,    0,
  367,  699,  375,  700, 1321, 2666, 2666, 2666,    0,  705,
  121,  121,  121,  587,  121,  587,  587,  742,  323, 1321,
  323,  364,  707, 4005,    0,  714,  267, 2600,    0,    0,
 4049,  432,  364,  364,  373,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  662,  502,  382,  503,  386,
  512, 1321, 4084,    0,  718,    0,    0,    0,  678, 4084,
  121,  121,  121,  121,  121,  121,  587,  338,    0,  364,
    0,    0,  712,    0,    0,  396,  735,  736,  739,    0,
    0,    0,    0,  364,  462,  463,  364,    0,  524,    0,
  530,    0,  718,    0, 1321,    0,    0,  121,  121,  121,
  121,    0,    0,    0,  343,  749, 4084, 4084, 4084,    0,
  364,  364,  468,    0,    0,    0,  121,    0, 4071,  412,
  414,  415,    0,    0,  364,  385, 4071, 4071, 4071,    0,
 4084,  411,  413,  423,  752,    0,    0,    0, 1321,  358,
    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  803,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3475, 1004,  532,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    1,    0,    0,
    0,    0,    0, 3539,    0,  829,    0,  533,    0,    0,
  537,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  768,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  130,    0,    0,
  547,  548,    0,    0,  108,    0,    0,    0,    0,    0,
  176,    0,    0,    0, -107,    0,  -97,    0,  117,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  768,    0,  768,    0,    0,    0,    0,    0,    0,    0,
    0,  949,    0,    0,    0,    0,  768,    0,    0,    0,
   13,  768,    0,  768,    0,    0,    0,    0,    0,  173,
  200,    0,    0, 3914, 3982,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  370,    0,    0,  -93,    0,
    0,    0,    0,    0,    0,    0,    0,  306,   81,  768,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  768,    0,  768,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  333,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2925,
    0, 6120,    0,    0,    0,    0,    0,  -92,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  495,    0,    0,   20,    0,  768,
    0,    0,  374,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -91,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  768,    0,    0,  768,  768,    0,
  768,  768,    0,  768,  768,    0,  768,  768,  768,  768,
  768,    0,    0,  768,    0,  768,  768,  768,  768,    0,
    0,    0,    0,    0,  768,    0,  768,    0,    0,    0,
    0,    0,  768,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  768,  768,    0,    0,    0,    0,  768,
    0,    0, 3024,    0, 3123, 6219,    0,    0,    0,    0,
    0,    0,    0,  768,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 6318,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  768,    0,    0,    0,  768,    0,    0,  768,    0,    0,
  768,    0,    0,    0,    0,    0,  768,  768,    0,  768,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  768,    0,    0,    0,  768,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  768,  768,
    0, 4041,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3222,    0,  505,    0,    0,    0,  768,
  768,  768,  538,    0,    0,    0,    0,    0,    0,    0,
    0, 6417,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4140,    0,
    0,    0,    0,  768,    0,    0,    0,    0,    0, 1069,
 1172, 1300, 1402, 1504, 1603, 1705, 1807, 1906, 2008, 2110,
 2209,    0,    0,    0,    0,    0, 4239,    0,    0,    0,
    0,    0,    0,    0,    0,  624,  635,    0,    0,    0,
    0,  690,    0,  790,  884,    0,    0,    0,    0,    0,
  768,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4338,    0, 4437,    0, 4536,    0,    0,    0,  966,    0,
  984,    0,    0,    0,    0,  380,  768,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4635,    0,    0,    0,    0,
    0,    0,    0,    0, 4734,    0,    0,    0,    0,    0,
 4833, 4932, 5031, 5130, 5229, 5328,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 5427,    0,    0,    0,    0, 5526, 5625, 5724,
 5823,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5922,    0,    0,    0,
    0,    0,    0,    0,    0,  768,    0,    0,    0,    0,
    0,  768,  768,  768,    0,    0,    0,    0,    0,    0,
    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  802,  734,    0,    0,    0,    0,  623,  627,  799,
  915,   -6,  297,  103, -275,   58,    0,  563,  567, -180,
 -500,    0,  308,    0, -663,    0,  -48,  607,  738,  259,
    0,  625,    0,   46,    0,  482, -101, -217,   -2,    0,
  -56,  780,  -31, -128,    0,  608, -140,    0,    0,    0,
  307, -726,  289,    0, -402, -537,    2,  101,  104, -329,
    0,  501,  504,  448, -440, 3503,    0,   73,    0,  328,
    0,  181,    0,   87,  -81,  -58,    0,    0,  466,
  };
  protected static readonly short [] yyTable = {            55,
   55,  102,   85,   57,  104,   51,  278,  492,  157,  126,
  231,  282,   96,  609,  346,  112,  236,   97,  122,   97,
   97,   97,   95,  185,   92,  117,  657,  186,  263,  113,
  118,  114,  130,  808,  763,   16,   52,   65,  263,  233,
   91,   67,  358,   88,   89,  585,  233,   96,  154,   96,
   55,   55,   91,  229,   55,  154,   90,   55,  130,  358,
   57,  107,  134,   57,  495,  833,  496,   19,   50,  268,
  236,  116,   20,  372,  372,  379,   80,   21,  199,  272,
   19,   50,   96,   68,  275,   88,  617,  618,  236,   69,
  138,  360,   70,   55,  226,   55,  389,   55,  270,  107,
  763,  107,  151,   96,  234,  560,  159,  134,  274,  262,
  181,  281,  183,  129,   82,  738,   33,  358,  742,  382,
   91,  614,  187,  201,  202,   89,  204,  245,  245,  245,
  499,  581,  395,  236,  115,  359,  118,   90,  120,  203,
  357,  214,  199,   57,  386,  678,  679,   96,  681,  562,
  145,  396,  880,  239,  240,   86,   91,  215,   91,   36,
  100,  230,   93,   71,   72,  345,  112,  155,  492,   91,
  108,   97,  139,  108,  519,  251,  117,  253,  707,  114,
  113,  118,  114,   96,  347,   48,   49,  206,  208,  515,
  207,  213,  154,  354,  355,  356,  108,  216,  217,  141,
  138,  227,  218,  219,  228,  220,  221,  222,  223,   22,
  697,  410,  111,  413,  385,   91,  146,  594,   23,  146,
  422,  151,  257,  107,  109,  258,   24,   25,   26,   27,
   28,  400,  381,  603,  517,  258,  394,  518,  398,  754,
  402,  756,  757,   83,  612,  121,   84,  370,  370,  105,
  125,  143,  146,  209,  273,   96,  401,  566,  261,  430,
  431,  432,  433,  434,   56,   62,  439,  124,  261,  444,
  445,  446,  447,  448,  449,  450,  451,  452,  453,  454,
  455,   55,  592,  112,  405,  408,  409,  411,  412,  414,
  415,  417,  418,  419,  420,  421,  424,  426,  427,  428,
  429,  772,  488,   17,   18,   18,  435,  437,   96,  508,
  443,  696,  845,  127,  518,   98,  101,  807,  131,  103,
  686,  442,  106,   73,   74,  484,  485,   55,  692,  490,
  694,  487,  140,  389,   96,  372,  599,  389,   71,   72,
   71,   72,   73,   74,  529,  128,  504,  532,   19,   19,
  133,  532,   19,   19,  532,   19,  689,   96,  141,  809,
  142,  770,  144,  532,  771,   96,   76,  653,   19,   19,
  825,  826,   96,   36,  658,  134,  134,  647,  842,  134,
  134,  835,  134,  858,   33,   33,  518,  147,   33,  563,
  593,   33,  135,   55,   55,  134,  134,  107,  881,  531,
   19,  835,   92,  535,   33,   33,  538,  843,  190,  541,
  147,  759,  179,  147,   52,  547,  548,   52,  550,  761,
  253,  850,  191,  253,  853,  205,   96,  134,  871,  561,
  690,   96,  200,   71,   72,  565,   33,   73,   74,  154,
  139,  139,  691,   96,  139,  139,  242,  139,  863,  864,
  608,  876,   96,  877,   96,  238,  243,  591,  241,  579,
  139,  139,  870,  878,   96,  247,  248,  141,  141,  244,
  249,  141,  141,  254,  141,  255,  580,  250,  256,  252,
   55,   55,   55,  269,  276,  588,  590,  141,  141,  277,
  283,  348,  139,  349,   25,  350,  353,  361,  600,  601,
  602,  362,  364,  267,   30,  387,  644,  645,  646,  370,
  650,  652,  392,  393,  440,  489,  230,  659,  497,  141,
  233,  503,   55,  162,  163,  164,  107,  165,  166,  167,
  500,  168,  501,  502,  684,  505,  352,   20,  814,  815,
  509,  510,  506,  511,  685,  170,  399,  363,  507,  365,
  688,  512,  171,  647,  647,  654,  513,  654,  514,  516,
  660,  661,  662,  663,  664,  665,  666,  667,  668,  669,
  670,  671,  524,   18,   18,  751,  522,   18,   18,  555,
   18,   55,  556,   55,   55,  557,  820,  558,  559,  564,
  567,  568,  486,   18,   18,  569,  345,  570,  571,  598,
  140,  140,  572,  573,  140,  140,  574,  140,  701,  744,
  236,  575,  576,  577,  578,  596,  597,  604,  605,  606,
  140,  140,  607,   31,  613,   18,  615,  750,  620,  395,
  698,  622,  389,  623,   28,  625,  626,  628,  629,  631,
  816,  632,  633,  634,  768,  635,  654,  638,  640,  654,
  801,  802,  140,  804,  817,  818,  819,  641,  527,  528,
  236,  236,  642,  236,  643,  656,  162,  163,  164,  675,
  165,  166,  167,  676,  168,  687,  693,   55,  704,  709,
  712,  107,  695,  783,  784,  388,  715,  718,  170,   23,
  724,  370,  767,  725,  727,  171,  732,  236,  236,  236,
  733,  838,  523,  839,  840,  734,  735,  736,  737,  741,
  743,  530,  745,  746,  533,  534,  236,  536,  537,  747,
  539,  540,  748,  542,  543,  544,  545,  546,  581,  760,
  549,  762,  551,  552,  553,  554,  773,  774,  788,  857,
  787,  789,  790,  792,  834,  586,  587,  589,  800,  791,
  810,  837,  844,  813,  824,  827,  829,  370,  828,  830,
  831,  835,   25,   25,  701,  107,   25,   25,  832,   25,
  836,  846,   30,   30,  847,  848,   30,   30,  849,   30,
  854,   97,   25,   25,  851,  852,  855,  619,  859,   21,
  865,  584,   30,   30,  867,  879,  868,  869,  860,  861,
  862,  177,    1,  121,  122,   20,   20,   91,  123,   20,
   20,   66,   20,  260,   25,  123,  259,   77,  124,  125,
  380,  378,  875,  610,   30,   20,   20,  498,  101,  132,
  279,  271,  178,   94,  785,  280,  856,  621,  786,  493,
  520,  624,  494,  823,  627,  611,  680,  630,  682,  683,
  812,    0,  866,  636,  637,  749,  639,   20,  521,    0,
  872,  873,  874,    0,  176,    0,    0,    0,  101,  101,
  101,    0,  101,    1,    2,    0,    0,    3,    4,    0,
    5,    0,    0,   24,    0,  672,  673,  674,  101,    0,
  101,   31,   31,    6,    7,   31,   31,    0,   31,    0,
    0,    0,   28,   28,    0,    0,   28,   28,    0,   28,
    0,   31,   31,    0,    0,    0,    0,    0,    0,  101,
    0,  101,   28,   28,    0,    8,  708,    0,  710,  711,
    0,  713,  714,    0,  716,  717,    0,  719,  720,  721,
  722,  723,  758,   31,  726,    0,  728,  729,  730,  731,
    0,  101,    0,  101,   28,    0,    0,   23,   23,    0,
  739,   23,   23,    0,   23,   29,    0,    0,  162,  163,
  164,    0,  165,  166,  167,    0,  168,   23,   23,    0,
    0,    0,    0,   26,    0,    0,    0,  388,   92,  220,
  170,  113,  220,  117,  119,    0,    0,  171,  162,  163,
  164,    0,  165,  166,  167,    0,  168,  769,    0,   23,
  220,    0,    0,  264,  169,  776,    0,    0,  777,    0,
  170,  778,    0,   99,  779,    0,    0,  171,  160,  161,
  780,  781,  182,  782,  184,   37,   38,    0,   39,   40,
    0,  220,   42,   91,   43,   44,   45,   46,   47,    0,
    0,    0,  797,  798,  799,    0,    0,   21,   21,    0,
    0,   21,   21,   91,   21,    0,    0,    0,    0,    0,
    0,  220,    0,  220,  822,    0,    0,   21,   21,    0,
    0,    0,    0,    0,    0,  101,  101,  101,    0,  101,
  101,  101,    0,  101,   91,    0,  101,  101,    0,    0,
  101,  101,  101,  101,    0,    0,    0,  101,   91,   21,
    0,    0,    0,    0,  101,  172,  101,  101,    0,    0,
  101,    0,    0,    0,    0,    0,   91,    0,    0,  173,
  174,  175,  101,  101,    0,  101,  101,   96,    0,  101,
  101,  101,  101,  101,  101,  101,    0,    0,  101,    0,
    0,   24,   24,    0,    0,   24,   24,    0,   24,  101,
  101,  101,    0,  101,  101,    0,    0,    0,  101,    0,
  101,   24,   24,  101,  101,  101,  101,  101,  101,  101,
  101,  101,  101,    0,  101,  101,    0,  101,  101,  101,
  101,  101,  101,  101,  101,  101,  101,  101,  101,  101,
    0,   96,  101,   24,    0,    0,  101,  101,  101,  101,
  101,   91,  101,  101,  101,  101,  101,  101,  101,    0,
  101,  220,  220,    0,    0,    0,    0,    0,    0,    0,
    0,  101,    0,   29,   29,    0,    0,   29,   29,    0,
   29,    0,  101,  101,  101,  101,    0,  101,  101,    0,
    0,   26,   26,   29,   29,   26,   26,    0,   26,    0,
   91,   91,   91,    0,   91,   91,   91,    0,   91,    0,
    0,   26,   26,    0,    0,   91,   91,    0,    0,  220,
  220,  220,   91,  220,  220,   29,    0,    0,  220,   91,
  220,    0,    0,  220,  220,  220,  220,  220,  220,  220,
  220,  220,  220,   26,  220,  220,    0,  220,  220,  220,
  220,  220,  220,  220,  220,  220,  220,  220,  220,  220,
    0,    0,  220,    0,    0,    0,  220,  220,  220,  220,
  220,  220,  220,  220,  220,  220,  220,  220,  220,   91,
  220,  356,  356,    0,    0,    0,    0,    0,    0,    0,
    0,  220,  162,  163,  164,    0,  165,  166,  167,    0,
  168,    0,  220,  220,  220,  220,    0,  220,    0,    0,
    0,    0,    0,    0,  170,    0,    0,   91,   71,   72,
   51,  171,   73,   74,   75,   30,   31,   32,   33,   34,
   35,   91,   91,   91,    0,    0,    0,   41,    0,  356,
  356,  356,    0,  356,  356,    0,   76,    0,  356,    0,
  356,   52,    0,  356,  356,  356,  356,  356,  356,  356,
  356,  356,  356,    0,  356,  356,    0,  356,  356,  356,
  356,  356,  356,  356,  356,  356,  356,  356,  356,  356,
    0,   91,  356,   50,  361,  361,  356,  356,  356,  356,
  356,   99,  356,  356,  356,  356,  356,  356,  356,    0,
  356,    0,    0,   37,   38,    0,   39,   40,    0,    0,
   42,  356,   43,   44,   45,   46,   47,  467,  468,    0,
    0,    0,  356,  356,  356,  356,    0,  356,    0,  456,
  457,  458,  459,  460,  461,  462,  463,  464,  465,    0,
    0,    0,  361,  361,  361,    0,  361,  361,    0,    0,
    0,  361,    0,  361,    0,    0,  361,  361,  361,  361,
  361,  361,  361,  361,  361,  361,    0,  361,  361,    0,
  361,  361,  361,  361,  361,  361,  361,  361,  361,  361,
  361,  361,  361,   91,    0,  361,    0,    0,    0,  361,
  361,  361,  361,  361,    0,  361,  361,  361,  361,  361,
  361,  361,    0,  361,    0,    0,    0,    0,    0,    0,
    0,    0,  346,  346,  361,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  192,  361,  361,  361,  361,    0,
  361,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
    0,  469,  470,  471,  472,    0,    0,    0,    0,    0,
  473,  474,  475,  476,  477,  478,  479,  480,  481,  482,
  346,  346,  346,    0,  346,  346,    0,    0,    0,  346,
    0,  346,   91,    0,  346,  346,  346,  346,  346,  346,
  346,  346,  346,  346,    0,  346,  346,    0,  346,  346,
  346,  346,  346,  346,  346,  346,  346,  346,  346,  346,
  346,    0,    0,  346,  324,  324,    0,  346,  346,  346,
  346,  346,    0,  346,  346,  346,  346,  346,  346,  346,
    0,  346,    0,    0,    0,    0,    0,  793,    0,  162,
  163,  164,  346,  165,  166,  167,    0,  168,    0,    0,
    0,    0,    0,  346,  346,  346,  346,    0,  346,    0,
    0,  170,    0,    0,    0,    0,    0,    0,  171,    0,
    0,    0,  324,  324,  324,    0,  324,  324,    0,    0,
    0,  324,    0,  324,   91,    0,  324,  324,  324,  324,
  324,  324,  324,  324,  324,  324,    0,  324,  324,    0,
  324,  324,  324,  324,  324,  324,  324,  324,  324,  324,
  324,  324,  324,    0,    0,  324,  321,  321,    0,  324,
  324,  324,  324,  324,    0,  324,  324,  324,  324,  324,
  324,  324,    0,  324,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  324,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  324,  324,  324,  324,    0,
  324,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  321,  321,  321,    0,  321,  321,
    0,    0,    0,  321,    0,  321,   91,    0,  321,  321,
  321,  321,  321,  321,  321,  321,  321,  321,    0,  321,
  321,    0,  321,  321,  321,  321,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  322,  322,  321,    0,    0,
    0,  321,  321,  321,  321,  321,    0,  321,  321,  321,
  321,  321,  321,  321,    0,  321,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  321,  162,  163,  164,
    0,  165,  166,  167,    0,  168,    0,  321,  321,  321,
  321,    0,  321,    0,    0,    0,  388,    0,    0,  170,
    0,    0,    0,  322,  322,  322,  171,  322,  322,    0,
    0,    0,  322,    0,  322,   91,    0,  322,  322,  322,
  322,  322,  322,  322,  322,  322,  322,    0,  322,  322,
    0,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  322,  322,    0,    0,  322,  323,  323,    0,
  322,  322,  322,  322,  322,    0,  322,  322,  322,  322,
  322,  322,  322,    0,  322,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  322,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  322,  322,  322,  322,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  323,  323,  323,    0,  323,
  323,    0,    0,    0,  323,    0,  323,   91,    0,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  323,    0,
  323,  323,    0,  323,  323,  323,  323,  323,  323,  323,
  323,  323,  323,  323,  323,  323,    0,    0,  323,  358,
  358,    0,  323,  323,  323,  323,  323,    0,  323,  323,
  323,  323,  323,  323,  323,    0,  323,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  323,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  323,  323,
  323,  323,    0,  323,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  358,  358,  358,
    0,  358,  358,    0,    0,    0,  358,    0,  358,   91,
    0,  358,  358,  358,  358,  358,  358,  358,  358,  358,
  358,    0,  358,  358,    0,  358,  358,  358,  358,  358,
  358,  358,  358,  358,  358,  358,  358,  358,  350,  350,
  358,    0,    0,    0,  358,  358,  358,  358,  358,    0,
  358,  358,  358,  358,  358,  358,  358,    0,  358,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  358,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  358,  358,  358,  358,    0,  358,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  350,  350,  350,    0,
  350,  350,    0,    0,    0,  350,    0,  350,   91,    0,
  350,  350,  350,  350,  350,  350,  350,  350,  350,  350,
    0,  350,  350,    0,  350,  350,  350,  350,  350,  350,
  350,  350,  350,  350,  350,  350,  350,    0,    0,  350,
  342,  342,    0,  350,  350,  350,  350,  350,    0,  350,
  350,  350,  350,  350,  350,  350,    0,  350,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  350,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  350,
  350,  350,  350,    0,  350,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  342,  342,
  342,    0,  342,  342,    0,    0,    0,  342,    0,  342,
    0,    0,  342,  342,  342,  342,  342,  342,  342,  342,
  342,  342,    0,  342,  342,    0,  342,  342,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,    0,
    0,  342,  331,  331,    0,  342,  342,  342,  342,  342,
    0,  342,  342,  342,  342,  342,  342,  342,    0,  342,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  342,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,  342,  342,  342,    0,  342,    0,    0,    0,
    0,    0,    0,    0,    0,   96,    0,    0,    0,    0,
  331,  331,  331,    0,  331,  331,    0,    0,    0,  331,
    0,  331,    0,  177,  331,  331,  331,  331,  331,  331,
  331,  331,  331,  331,    0,  331,  331,    0,  331,  331,
  331,  331,  331,  331,  331,  331,  331,  331,  331,  331,
  331,  298,  298,  331,  178,    0,    0,  331,  331,  331,
  331,  331,    0,  331,  331,  331,  331,  331,  331,  331,
   96,  331,  237,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  331,    0,    0,    0,  176,    0,  177,    0,
    0,    0,    0,  331,  331,  331,  331,    0,  331,    0,
    0,    0,    0,    0,   96,    0,    0,    0,    0,  298,
  298,  298,    0,  298,  298,    0,    0,    0,  298,  178,
  298,    0,  177,  298,  298,  298,  298,  298,  298,  298,
  298,  298,  298,    0,  298,  298,    0,  298,  298,  298,
  298,  298,  298,  298,  298,  298,  298,  298,  298,  298,
    0,  176,  298,  178,    0,    0,  298,  298,  298,  298,
  298,    0,  298,  298,  298,  298,  298,  298,  298,   96,
  298,  351,    0,    0,    0,    0,    0,    0,   96,    0,
    0,  298,    0,    0,    0,  176,    0,  177,    0,    0,
    0,    0,  298,  298,  298,  298,  177,  298,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  178,    0,
  162,  163,  164,    0,  165,  166,  167,  178,  168,  177,
    0,    0,    0,    0,    0,  264,  169,    0,    0,    0,
    0,   51,  170,    0,    0,    0,    0,    0,    0,  171,
  176,    0,    0,    0,    0,   99,    0,    0,    0,  176,
  178,    0,    0,    0,    0,    0,   51,   37,   38,    0,
   39,   40,   52,    0,   42,    0,   43,   44,   45,   46,
   47,    0,    0,    0,    0,  162,  163,  164,    0,  165,
  166,  167,  176,  168,    0,  177,    0,   52,    0,    0,
    0,  169,    0,    0,   50,    0,    0,  170,    0,   51,
    0,    0,    0,    0,  171,   51,    0,    0,    0,  162,
  163,  164,    0,  165,  166,  167,  178,  168,    0,   50,
    0,    0,    0,    0,  264,  169,    0,  172,    0,    0,
   52,  170,    0,    0,    0,    0,   52,    0,  171,   51,
    0,  173,  174,  175,    0,    0,   51,    0,  176,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   50,    0,    0,    0,    0,    0,   50,    0,
   52,    0,    0,    0,  162,  163,  164,   52,  165,  166,
  167,    0,  168,  162,  163,  164,    0,  165,  166,  167,
  169,  168,  172,    0,    0,    0,  170,    0,    0,  169,
    0,    0,   50,  171,    0,  170,  173,  174,  175,   50,
  491,    0,  171,    0,    0,    0,  162,  163,  164,    0,
  165,  166,  167,    0,  168,    0,  172,    0,    0,    0,
    0,  264,  169,    0,    0,  192,    0,    0,  170,    0,
  173,  174,  175,    0,   23,  171,    0,    0,    0,    0,
    0,   99,   24,   25,   26,   27,   28,    0,    0,    0,
   22,    0,    0,   37,   38,    0,   39,   40,    0,   23,
   42,    0,   43,   44,   45,   46,   47,   24,   25,   26,
   27,   28,  162,  163,  164,    0,  165,  166,  167,    0,
  168,  172,    0,    0,    0,    0,    0,  264,  169,    0,
  172,    0,    0,   22,  170,  173,  174,  175,    0,   22,
  595,  171,   23,    0,  173,  174,  175,    0,   23,  214,
   24,   25,   26,   27,   28,    0,   24,   25,   26,   27,
   28,    0,    0,  172,    0,  215,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,  651,  173,  174,  175,
   22,    0,   23,    0,  416,    0,    0,    0,  284,   23,
   24,   25,   26,   27,   28,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,  216,  217,    0,    0,    0,
  218,  219,    0,  220,  221,  222,  223,    0,    0,    0,
    0,    0,  436,    0,    0,    0,    0,  423,    0,  172,
    0,    0,    0,  425,    0,    0,    0,    0,    0,  236,
    0,    0,    0,  173,  174,  175,  285,  286,  287,    0,
  288,  289,  403,  404,    0,  290,    0,  291,    0,    0,
  292,  293,  294,  295,  296,  297,  298,  299,  300,  301,
    0,  302,  303,    0,  304,  305,  306,  307,  308,  309,
  310,  311,  312,  313,  314,  315,  316,    0,  284,  317,
    0,    0,    0,  318,  319,  320,  321,  322,    0,  323,
  324,  325,  326,  327,  328,  329,    0,  330,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  331,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  332,
  333,  334,  335,    0,  336,    0,    0,    0,  234,    0,
    0,    0,    0,    0,    0,    0,  285,  286,  287,    0,
  288,  289,    0,    0,    0,  290,    0,  291,    0,    0,
  292,  293,  294,  295,  296,  297,  298,  299,  300,  301,
    0,  302,  303,    0,  304,  305,  306,  307,  308,  309,
  310,  311,  312,  313,  314,  315,  316,  236,    0,  317,
    0,    0,    0,  318,  319,  320,  321,  322,    0,  323,
  324,  325,  326,  327,  328,  329,    0,  330,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  331,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  332,
  333,  334,  335,    0,  336,    0,    0,  237,    0,    0,
    0,    0,    0,    0,    0,  236,  236,  236,    0,  236,
  236,    0,    0,    0,  236,    0,  236,    0,    0,  236,
  236,  236,  236,  236,  236,  236,  236,  236,  236,    0,
  236,  236,    0,  236,  236,  236,  236,  236,  236,  236,
  236,  236,  236,  236,  236,  236,  234,    0,  236,    0,
    0,    0,  236,  236,  236,  236,  236,    0,  236,  236,
  236,  236,  236,  236,  236,    0,  236,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  236,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  236,  236,
  236,  236,    0,  236,    0,    0,  235,    0,    0,    0,
    0,    0,    0,    0,  234,  234,  234,    0,  234,  234,
    0,    0,    0,  234,    0,  234,    0,    0,  234,  234,
  234,  234,  234,  234,  234,  234,  234,  234,    0,  234,
  234,    0,  234,  234,  234,  234,  234,  234,  234,  234,
  234,  234,  234,  234,  234,  237,    0,  234,    0,    0,
    0,  234,  234,  234,  234,  234,    0,  234,  234,  234,
  234,  234,  234,  234,    0,  234,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  234,    0,    0,    0,
    0,    0,    0,    0,    0,   51,    0,  234,  234,  234,
  234,    0,  234,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  237,  237,  237,    0,  237,  237,    0,
    0,    0,  237,    0,  237,    0,   52,  237,  237,  237,
  237,  237,  237,  237,  237,  237,  237,    0,  237,  237,
    0,  237,  237,  237,  237,  237,  237,  237,  237,  237,
  237,  237,  237,  237,  235,   51,  237,    0,   50,    0,
  237,  237,  237,  237,  237,    0,  237,  237,  237,  237,
  237,  237,  237,    0,  237,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  237,   52,    0,    0,    0,
    0,    0,    0,    0,  129,    0,  237,  237,  237,  237,
    0,  237,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  235,  235,  235,    0,  235,  235,   50,    0,
    0,  235,    0,  235,    0,  129,  235,  235,  235,  235,
  235,  235,  235,  235,  235,  235,    0,  235,  235,    0,
  235,  235,  235,  235,  235,  235,  235,  235,  235,  235,
  235,  235,  235,    0,    0,  235,    0,  129,  130,  235,
  235,  235,  235,  235,    0,  235,  235,  235,  235,  235,
  235,  235,    0,  235,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  235,    0,    0,    0,    0,  130,
    0,    0,    0,    0,    0,  235,  235,  235,  235,   22,
  235,    0,    0,    0,    0,    0,    0,    0,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,  130,   51,    0,    0,    0,    0,   29,    0,    0,
    0,    0,   30,   31,   32,   33,   34,   35,   36,   37,
   38,    0,   39,   40,   41,    0,   42,    0,   43,   44,
   45,   46,   47,   52,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,   48,   49,    0,    0,   23,    0,
   51,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,   51,    0,   50,    0,   99,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  129,   37,
   38,   52,   39,   40,    0,    0,   42,  129,   43,   44,
   45,   46,   47,   76,   52,  129,  129,  129,  129,  129,
    0,    0,    0,    0,   51,    0,  129,    0,    0,    0,
    0,    0,    0,   50,    0,    0,    0,   51,  129,  129,
    0,  129,  129,    0,    0,  129,   50,  129,  129,  129,
  129,  129,  129,    0,    0,   52,    0,    0,    0,    0,
    0,    0,  130,    0,    0,    0,    0,  150,   52,    0,
   51,  130,    0,  137,    0,    0,    0,    0,    0,  130,
  130,  130,  130,  130,    0,    0,   51,   50,    0,  700,
  130,    0,   51,    0,    0,    0,    0,    0,    0,    0,
   50,   52,  130,  130,    0,  130,  130,    0,   51,  130,
    0,  130,  130,  130,  130,  130,  130,   52,    0,    0,
    0,    0,    0,   52,    0,    0,   22,    0,    0,    0,
    0,    0,    0,   50,    0,   23,    0,   51,    0,   52,
    0,    0,    0,   24,   25,   26,   27,   28,    0,   50,
    0,    0,    0,    0,   99,   50,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   36,   37,   38,   52,   39,
   40,   50,   51,   42,   22,   43,   44,   45,   46,   47,
    0,    0,    0,   23,    0,    0,    0,  192,   51,    0,
    0,   24,   25,   26,   27,   28,   23,    0,    0,    0,
   50,   51,   61,   52,   24,   25,   26,   27,   28,    0,
    0,    0,    0,    0,   37,   38,    0,   39,   40,   52,
    0,   42,    0,   43,   44,   45,   46,   47,   22,  438,
    0,    0,   52,    0,   51,   50,    0,   23,    0,    0,
    0,   22,    0,    0,    0,   24,   25,   26,   27,   28,
   23,   50,    0,   87,    0,  148,   99,   51,   24,   25,
   26,   27,   28,    0,   50,   52,  195,    0,   37,   38,
  149,   39,   40,    0,   22,   42,    0,   43,   44,   45,
   46,   47,    0,   23,    0,    0,    0,    0,   52,    0,
   22,   24,   25,   26,   27,   28,   22,   50,   82,   23,
    0,    0,    0,  136,  148,   23,    0,   24,   25,   26,
   27,   28,   22,   24,   25,   26,   27,   28,   51,  149,
   50,   23,    0,    0,   51,  136,  699,    0,    0,   24,
   25,   26,   27,   28,  162,  163,  164,  366,  165,  166,
  167,   22,  367,    0,    0,    0,    0,  677,    0,   52,
   23,  368,    0,  369,    0,   52,  170,  811,   24,   25,
   26,   27,   28,  171,    0,    0,   83,    0,   51,  162,
  163,  164,  366,  165,  166,  167,   22,  367,    0,  705,
  706,   50,    0,    0,    0,   23,  377,   50,  369,    0,
   51,  170,   22,   24,   25,   26,   27,   28,  171,   52,
    0,   23,    0,   51,    0,  192,  193,    0,    0,   24,
   25,   26,   27,   28,   23,  194,    0,    0,    0,    0,
    0,   52,   24,   25,   26,   27,   28,    0,    0,    0,
    0,   50,    0,   82,   52,    0,    0,    0,  192,  193,
  752,  753,    0,  755,    0,    0,    0,   23,  194,   82,
    0,    0,    0,   50,    0,   24,   25,   26,   27,   28,
    0,  192,  383,    0,    0,    0,   50,    0,    0,  775,
   23,  384,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,   82,
   82,    0,    0,    0,   82,   82,    0,   82,   82,   82,
   82,   83,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  803,   83,  805,  806,
    0,    0,   22,    0,    0,    0,    0,    0,   22,    0,
    0,   23,  766,    0,    0,    0,    0,   23,    0,   24,
   25,   26,   27,   28,    0,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,    0,   83,   83,    0,
    0,    0,   83,   83,    0,   83,   83,   83,   83,  841,
    0,    0,   22,  341,  341,    0,    0,    0,    0,    0,
    0,   23,    0,    0,    0,    0,  699,    0,    0,   24,
   25,   26,   27,   28,   22,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,  192,    0,    0,
    0,   24,   25,   26,   27,   28,   23,    0,    0,    0,
    0,    0,    0,    0,   24,   25,   26,   27,   28,    0,
    0,  341,  341,  341,    0,  341,  341,    0,    0,    0,
  341,    0,  341,    0,    0,  341,  341,  341,  341,  341,
  341,  341,  341,  341,  341,    0,  341,  341,    0,  341,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
  341,  341,  315,  315,  341,    0,    0,    0,  341,  341,
  341,  341,  341,    0,  341,  341,  341,  341,  341,  341,
  341,    0,  341,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  341,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,  341,  341,  341,    0,  341,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  315,  315,  315,    0,  315,  315,    0,    0,    0,  315,
    0,  315,    0,    0,  315,  315,  315,  315,  315,  315,
  315,  315,  315,  315,    0,  315,  315,    0,  315,  315,
  315,  315,  315,  315,  315,  315,  315,  315,  315,  315,
  315,  299,  299,  315,    0,    0,    0,  315,  315,  315,
  315,  315,    0,  315,  315,  315,  315,  315,  315,  315,
    0,  315,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  315,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  315,  315,  315,  315,    0,  315,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  299,
  299,  299,    0,  299,  299,    0,    0,    0,  299,    0,
  299,    0,    0,  299,  299,  299,  299,  299,  299,  299,
  299,  299,  299,    0,  299,  299,    0,  299,  299,  299,
  299,  299,  299,  299,  299,  299,  299,  299,  299,  299,
  304,  304,  299,    0,    0,    0,  299,  299,  299,  299,
  299,    0,  299,  299,  299,  299,  299,  299,  299,    0,
  299,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  299,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  299,  299,  299,  299,    0,  299,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  304,  304,
  304,    0,  304,  304,    0,    0,    0,  304,    0,  304,
    0,    0,  304,  304,  304,  304,  304,  304,  304,  304,
  304,  304,    0,  304,  304,    0,  304,  304,  304,  304,
  304,  304,  304,  304,  304,  304,  304,  304,  304,  300,
  300,  304,    0,    0,    0,  304,  304,  304,  304,  304,
    0,  304,  304,  304,  304,  304,  304,  304,    0,  304,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  304,  304,  304,  304,    0,  304,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  300,  300,  300,
    0,  300,  300,    0,    0,    0,  300,    0,  300,    0,
    0,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  300,    0,  300,  300,    0,  300,  300,  300,  300,  300,
  300,  300,  300,  300,  300,  300,  300,  300,  309,  309,
  300,    0,    0,    0,  300,  300,  300,  300,  300,    0,
  300,  300,  300,  300,  300,  300,  300,    0,  300,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  300,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  300,  300,  300,  300,    0,  300,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,  309,  309,    0,
  309,  309,    0,    0,    0,  309,    0,  309,    0,    0,
  309,  309,  309,  309,  309,  309,  309,  309,  309,  309,
    0,  309,  309,    0,  309,  309,  309,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,  330,  330,  309,
    0,    0,    0,  309,  309,  309,  309,  309,    0,  309,
  309,  309,  309,  309,  309,  309,    0,  309,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  309,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  309,
  309,  309,  309,    0,  309,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  330,  330,  330,    0,  330,
  330,    0,    0,    0,  330,    0,  330,    0,    0,  330,
  330,  330,  330,  330,  330,  330,  330,  330,  330,    0,
  330,  330,    0,  330,  330,  330,  330,  330,  330,  330,
  330,  330,  330,  330,  330,  330,  326,  326,  330,    0,
    0,    0,  330,  330,  330,  330,  330,    0,  330,  330,
  330,  330,  330,  330,  330,    0,  330,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  330,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  330,  330,
  330,  330,    0,  330,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  326,  326,  326,    0,  326,  326,
    0,    0,    0,  326,    0,  326,    0,    0,  326,  326,
  326,  326,  326,  326,  326,  326,  326,  326,    0,  326,
  326,    0,  326,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  326,  305,  305,  326,    0,    0,
    0,  326,  326,  326,  326,  326,    0,  326,  326,  326,
  326,  326,  326,  326,    0,  326,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  326,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  326,  326,  326,
  326,    0,  326,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  305,  305,  305,    0,  305,  305,    0,
    0,    0,  305,    0,  305,    0,    0,  305,  305,  305,
  305,  305,  305,  305,  305,  305,  305,    0,  305,  305,
    0,  305,  305,  305,  305,  305,  305,  305,  305,  305,
  305,  305,  305,  305,  301,  301,  305,    0,    0,    0,
  305,  305,  305,  305,  305,    0,  305,  305,  305,  305,
  305,  305,  305,    0,  305,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  305,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  305,  305,  305,  305,
    0,  305,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  301,  301,  301,    0,  301,  301,    0,    0,
    0,  301,    0,  301,    0,    0,  301,  301,  301,  301,
  301,  301,  301,  301,  301,  301,    0,  301,  301,    0,
  301,  301,  301,  301,  301,  301,  301,  301,  301,  301,
  301,  301,  301,  302,  302,  301,    0,    0,    0,  301,
  301,  301,  301,  301,    0,  301,  301,  301,  301,  301,
  301,  301,    0,  301,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  301,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  301,  301,  301,  301,    0,
  301,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  302,  302,  302,    0,  302,  302,    0,    0,    0,
  302,    0,  302,    0,    0,  302,  302,  302,  302,  302,
  302,  302,  302,  302,  302,    0,  302,  302,    0,  302,
  302,  302,  302,  302,  302,  302,  302,  302,  302,  302,
  302,  302,  306,  306,  302,    0,    0,    0,  302,  302,
  302,  302,  302,    0,  302,  302,  302,  302,  302,  302,
  302,    0,  302,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  302,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  302,  302,  302,  302,    0,  302,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  306,  306,  306,    0,  306,  306,    0,    0,    0,  306,
    0,  306,    0,    0,  306,  306,  306,  306,  306,  306,
  306,  306,  306,  306,    0,  306,  306,    0,  306,  306,
  306,  306,  306,  306,  306,  306,  306,  306,  306,  306,
  306,  307,  307,  306,    0,    0,    0,  306,  306,  306,
  306,  306,    0,  306,  306,  306,  306,  306,  306,  306,
    0,  306,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  306,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  306,  306,  306,  306,    0,  306,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  307,
  307,  307,    0,  307,  307,    0,    0,    0,  307,    0,
  307,    0,    0,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  307,    0,  307,  307,    0,  307,  307,  307,
  307,  307,  307,  307,  307,  307,  307,  307,  307,  307,
  310,  310,  307,    0,    0,    0,  307,  307,  307,  307,
  307,    0,  307,  307,  307,  307,  307,  307,  307,    0,
  307,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  307,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  307,  307,  307,  307,    0,  307,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  310,  310,
  310,    0,  310,  310,    0,    0,    0,  310,    0,  310,
    0,    0,  310,  310,  310,  310,  310,  310,  310,  310,
  310,  310,    0,  310,  310,    0,  310,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,  310,  310,  327,
  327,  310,    0,    0,    0,  310,  310,  310,  310,  310,
    0,  310,  310,  310,  310,  310,  310,  310,    0,  310,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  310,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  310,  310,  310,  310,    0,  310,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  327,  327,  327,
    0,  327,  327,    0,    0,    0,  327,    0,  327,    0,
    0,  327,  327,  327,  327,  327,  327,  327,  327,  327,
  327,    0,  327,  327,    0,  327,  327,  327,  327,  327,
  327,  327,  327,  327,  327,  327,  327,  327,  303,  303,
  327,    0,    0,    0,  327,  327,  327,  327,  327,    0,
  327,  327,  327,  327,  327,  327,  327,    0,  327,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  327,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  327,  327,  327,  327,    0,  327,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  303,  303,  303,    0,
  303,  303,    0,    0,    0,  303,    0,  303,    0,    0,
  303,  303,  303,  303,  303,  303,  303,  303,  303,  303,
    0,  303,  303,    0,  303,  303,  303,  303,  303,  303,
  303,  303,  303,  303,  303,  303,  303,  308,  308,  303,
    0,    0,    0,  303,  303,  303,  303,  303,    0,  303,
  303,  303,  303,  303,  303,  303,    0,  303,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  303,
  303,  303,  303,    0,  303,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  308,  308,  308,    0,  308,
  308,    0,    0,    0,  308,    0,  308,    0,    0,  308,
  308,  308,  308,  308,  308,  308,  308,  308,  308,    0,
  308,  308,    0,  308,  308,  308,  308,  308,  308,  308,
  308,  308,  308,  308,  308,  308,  311,  311,  308,    0,
    0,    0,  308,  308,  308,  308,  308,    0,  308,  308,
  308,  308,  308,  308,  308,    0,  308,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  308,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  308,  308,
  308,  308,    0,  308,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  311,  311,  311,    0,  311,  311,
    0,    0,    0,  311,    0,  311,    0,    0,  311,  311,
  311,  311,  311,  311,  311,  311,  311,  311,    0,  311,
  311,    0,  311,  311,  311,  311,  311,  311,  311,  311,
  311,  311,  311,  311,  311,  312,  312,  311,    0,    0,
    0,  311,  311,  311,  311,  311,    0,  311,  311,  311,
  311,  311,  311,  311,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  311,  311,  311,
  311,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  312,  312,  312,    0,  312,  312,    0,
    0,    0,  312,    0,  312,    0,    0,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  312,    0,  312,  312,
    0,  312,  312,  312,  312,  312,  312,  312,  312,  312,
  312,  312,  312,  312,  313,  313,  312,    0,    0,    0,
  312,  312,  312,  312,  312,    0,  312,  312,  312,  312,
  312,  312,  312,    0,  312,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  312,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,  312,  312,  312,
    0,  312,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  313,  313,  313,    0,  313,  313,    0,    0,
    0,  313,    0,  313,    0,    0,  313,  313,  313,  313,
  313,  313,  313,  313,  313,  313,    0,  313,  313,    0,
  313,  313,  313,  313,  313,  313,  313,  313,  313,  313,
  313,  313,  313,  284,    0,  313,    0,    0,    0,  313,
  313,  313,  313,  313,    0,  313,  313,  313,  313,  313,
  313,  313,    0,  313,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  313,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  313,  313,  313,  313,    0,
  313,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  285,  286,  287,    0,  288,  289,    0,    0,    0,
  290,    0,  291,    0,    0,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,    0,  302,  303,    0,  304,
  305,  306,  307,  308,  309,  310,  311,  312,  313,  314,
  315,  316,  240,    0,  317,    0,    0,    0,  318,  319,
  320,  321,  322,    0,  323,  324,  325,  326,  327,  328,
  329,    0,  330,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  331,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  332,  333,  334,  335,    0,  336,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  240,  240,  240,    0,  240,  240,    0,    0,    0,  240,
    0,  240,    0,    0,  240,  240,  240,  240,  240,  240,
  240,  240,  240,  240,    0,  240,  240,    0,  240,  240,
  240,  240,  240,  240,  240,  240,  240,  240,  240,  240,
  240,  241,    0,  240,    0,    0,    0,  240,  240,  240,
  240,  240,    0,  240,  240,  240,  240,  240,  240,  240,
    0,  240,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  240,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  240,  240,  240,  240,    0,  240,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  241,
  241,  241,    0,  241,  241,    0,    0,    0,  241,    0,
  241,    0,    0,  241,  241,  241,  241,  241,  241,  241,
  241,  241,  241,    0,  241,  241,    0,  241,  241,  241,
  241,  241,  241,  241,  241,  241,  241,  241,  241,  241,
  242,    0,  241,    0,    0,    0,  241,  241,  241,  241,
  241,    0,  241,  241,  241,  241,  241,  241,  241,    0,
  241,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  241,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  241,  241,  241,  241,    0,  241,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  242,  242,
  242,    0,  242,  242,    0,    0,    0,  242,    0,  242,
    0,    0,  242,  242,  242,  242,  242,  242,  242,  242,
  242,  242,    0,  242,  242,    0,  242,  242,  242,  242,
  242,  242,  242,  242,  242,  242,  242,  242,  242,  243,
    0,  242,    0,    0,    0,  242,  242,  242,  242,  242,
    0,  242,  242,  242,  242,  242,  242,  242,    0,  242,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  242,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  242,  242,  242,  242,    0,  242,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  243,  243,  243,
    0,  243,  243,    0,    0,    0,  243,    0,  243,    0,
    0,  243,  243,  243,  243,  243,  243,  243,  243,  243,
  243,    0,  243,  243,    0,  243,  243,  243,  243,  243,
  243,  243,  243,  243,  243,  243,  243,  243,    0,    0,
  243,    0,    0,    0,  243,  243,  243,  243,  243,    0,
  243,  243,  243,  243,  243,  243,  243,    0,  243,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  243,
    0,    0,    0,    0,    0,    0,    0,  288,    0,    0,
  243,  243,  243,  243,  291,  243,    0,  292,  293,  294,
  295,  296,  297,  298,  299,  300,  301,    0,  302,  303,
    0,  304,  305,  306,  307,  308,  309,  310,  311,  312,
  313,  314,  315,  316,    0,    0,  317,    0,    0,    0,
  318,  319,  320,  321,  322,    0,  323,  324,  325,  326,
  327,  328,  329,    0,  330,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  331,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  332,  333,  334,  335,
    0,  336,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   58,   33,    6,   61,   60,  125,  337,  110,   33,
  123,  229,   42,  514,  123,  123,  157,   40,   60,   40,
   40,   40,   54,  276,  123,  123,  564,  280,   44,  123,
  123,  123,   44,  760,  698,   61,   91,  319,   44,  310,
   40,  272,   44,   50,   44,  486,  310,   42,  319,   42,
   57,   58,   40,  155,   61,  319,   44,   64,   44,   44,
   41,   64,   94,   44,  340,  792,  342,   61,  123,   62,
  211,   78,   61,  254,  255,  256,   19,   61,  127,  208,
    0,  123,   42,  260,  213,   92,  527,  528,  229,   61,
   97,   93,   61,  100,  151,  102,  277,  104,   93,  102,
  764,  104,  109,   42,  375,   44,  113,    0,  210,  125,
  117,  375,  119,  125,  280,  653,    0,   44,  656,  125,
   40,  524,  375,  130,  131,  125,  133,  176,  177,  178,
  348,   91,  266,  274,   77,   62,   79,  125,   81,  125,
  125,  260,  191,  124,  273,  586,  587,   42,  589,   44,
  105,  285,  879,  160,  161,   40,   40,  276,  257,  303,
   58,  274,  257,  290,  291,  274,  274,  110,  498,   40,
   41,   40,    0,   44,  392,  182,  274,  184,  619,   77,
  274,  274,  274,   42,  233,  329,  330,   41,  143,  370,
   44,  146,  319,  242,  243,  244,   61,  316,  317,    0,
  207,   41,  321,  322,   44,  324,  325,  326,  327,  264,
  613,  293,  260,  295,  263,   40,   41,  493,  273,   44,
  302,  228,   41,  226,   40,   44,  281,  282,  283,  284,
  285,  288,   41,  509,   41,   44,  285,   44,  287,  680,
  289,  682,  683,  274,  520,  287,  277,  254,  255,  272,
  274,  272,  272,  272,  209,   42,  288,   44,  274,  308,
  309,  310,  311,  312,    6,    7,  315,   40,  274,  318,
  319,  320,  321,  322,  323,  324,  325,  326,  327,  328,
  329,  288,  312,  260,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  704,  334,  270,  271,    0,  313,  314,   42,  358,
  317,   41,  813,  123,   44,   57,   58,  758,  267,   61,
  596,  376,   64,  294,  295,  332,  333,  334,  604,  336,
  606,  334,    0,  514,   42,  516,   44,  518,  290,  291,
  290,  291,  294,  295,  401,  257,  353,  406,  268,  269,
  267,  410,  272,  273,  413,  275,   41,   42,  100,  762,
  102,   41,  104,  422,   44,   42,  318,   44,  288,  289,
  773,  774,   42,  303,   44,  268,  269,  558,   41,  272,
  273,   44,  275,   41,  268,  269,   44,  123,  272,  438,
  420,  275,  375,  400,  401,  288,  289,  400,   41,  406,
  320,   44,  123,  410,  288,  289,  413,  810,   40,  416,
   41,  687,  116,   44,   41,  422,  423,   44,  425,  695,
   41,  824,  123,   44,  827,  257,   42,  320,   44,  436,
   41,   42,   41,  290,  291,  442,  320,  294,  295,  319,
  268,  269,   41,   42,  272,  273,   40,  275,  851,  852,
   41,   41,   42,   41,   42,  159,   40,  489,  376,  466,
  288,  289,  865,   41,   42,  177,  178,  268,  269,   40,
   44,  272,  273,   58,  275,   58,  483,  181,   58,  183,
  487,  488,  489,   62,   61,  488,  489,  288,  289,   40,
  274,  257,  320,  375,    0,   44,   40,  375,  505,  506,
  507,   44,   44,  197,    0,  260,  555,  556,  557,  516,
  559,  560,  257,   61,  328,  416,  274,  566,  274,  320,
  310,   44,  529,  257,  258,  259,  529,  261,  262,  263,
  257,  265,  375,  375,  591,  383,  240,    0,  272,  273,
  257,  375,  383,   44,  593,  279,  288,  251,  383,  253,
  599,  375,  286,  734,  735,  562,   44,  564,   40,  124,
  567,  568,  569,  570,  571,  572,  573,  574,  575,  576,
  577,  578,   44,  268,  269,  677,  273,  272,  273,   44,
  275,  588,   44,  590,  591,   44,  767,   44,   44,   44,
  383,  383,  334,  288,  289,  383,  274,  383,  383,  375,
  268,  269,  383,  383,  272,  273,  383,  275,  615,  658,
  751,  383,  383,  383,  383,  257,  257,  257,  375,  257,
  288,  289,  375,    0,   44,  320,   40,  676,   44,  266,
   91,   44,  813,   44,    0,   44,   44,   44,   44,   44,
  374,   44,   44,   44,  701,   44,  653,   44,   44,  656,
  752,  753,  320,  755,  388,  389,  390,   44,  400,  401,
  801,  802,   44,  804,   44,   44,  257,  258,  259,   44,
  261,  262,  263,   44,  265,  257,  257,  684,  383,   44,
   44,  684,  257,  732,  733,  276,   44,   44,  279,    0,
   44,  698,  699,   44,   44,  286,   44,  838,  839,  840,
   44,  803,  396,  805,  806,   44,   44,  257,   44,   44,
   44,  405,   44,   44,  408,  409,  857,  411,  412,   44,
  414,  415,   44,  417,  418,  419,  420,  421,   91,   44,
  424,   44,  426,  427,  428,  429,  383,  383,   44,  841,
  375,  375,   44,   44,  793,  487,  488,  489,   44,  375,
   44,  800,   41,   40,  323,  383,  375,  764,  257,  257,
  375,   44,  268,  269,  771,  768,  272,  273,  257,  275,
   93,  376,  268,  269,   40,   40,  272,  273,   40,  275,
  257,   40,  288,  289,  323,  323,  257,  529,   40,    0,
  323,  485,  288,  289,  383,   44,  383,  383,  847,  848,
  849,   60,    0,  272,  272,  268,  269,   40,  272,  272,
  273,   10,  275,  191,  320,   82,  190,   19,  272,  272,
  258,  255,  871,  516,  320,  288,  289,  346,    0,   92,
  224,  207,   91,   54,  734,  228,  835,  531,  735,  339,
  393,  535,  339,  771,  538,  518,  588,  541,  590,  591,
  764,   -1,  859,  547,  548,  675,  550,  320,  393,   -1,
  867,  868,  869,   -1,  123,   -1,   -1,   -1,   40,   41,
   42,   -1,   44,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,   -1,    0,   -1,  579,  580,  581,   60,   -1,
   62,  268,  269,  288,  289,  272,  273,   -1,  275,   -1,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,  288,  289,   -1,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   93,  288,  289,   -1,  320,  620,   -1,  622,  623,
   -1,  625,  626,   -1,  628,  629,   -1,  631,  632,  633,
  634,  635,  684,  320,  638,   -1,  640,  641,  642,  643,
   -1,  123,   -1,  125,  320,   -1,   -1,  268,  269,   -1,
  654,  272,  273,   -1,  275,    0,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  288,  289,   -1,
   -1,   -1,   -1,    0,   -1,   -1,   -1,  276,   40,   41,
  279,   77,   44,   79,   80,   -1,   -1,  286,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  701,   -1,  320,
   62,   -1,   -1,  272,  273,  709,   -1,   -1,  712,   -1,
  279,  715,   -1,  292,  718,   -1,   -1,  286,  114,  115,
  724,  725,  118,  727,  120,  304,  305,   -1,  307,  308,
   -1,   93,  311,   40,  313,  314,  315,  316,  317,   -1,
   -1,   -1,  746,  747,  748,   -1,   -1,  268,  269,   -1,
   -1,  272,  273,   60,  275,   -1,   -1,   -1,   -1,   -1,
   -1,  123,   -1,  125,  768,   -1,   -1,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   91,   -1,  268,  269,   -1,   -1,
  272,  273,  274,  275,   -1,   -1,   -1,  279,   40,  320,
   -1,   -1,   -1,   -1,  286,  374,  288,  289,   -1,   -1,
  292,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,  388,
  389,  390,  304,  305,   -1,  307,  308,   42,   -1,  311,
  312,  313,  314,  315,  316,  317,   -1,   -1,  320,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,  331,
  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,
  342,  288,  289,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
   -1,   42,  374,  320,   -1,   -1,  378,  379,  380,  381,
  382,   40,  384,  385,  386,  387,  388,  389,  390,   -1,
  392,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,  414,  415,  416,  417,   -1,  419,  420,   -1,
   -1,  268,  269,  288,  289,  272,  273,   -1,  275,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,  288,  289,   -1,   -1,  272,  273,   -1,   -1,  331,
  332,  333,  279,  335,  336,  320,   -1,   -1,  340,  286,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,  320,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
   -1,   -1,  374,   -1,   -1,   -1,  378,  379,  380,  381,
  382,  383,  384,  385,  386,  387,  388,  389,  390,   40,
  392,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,
   -1,   -1,   -1,   -1,  279,   -1,   -1,  374,  290,  291,
   60,  286,  294,  295,  296,  297,  298,  299,  300,  301,
  302,  388,  389,  390,   -1,   -1,   -1,  309,   -1,  331,
  332,  333,   -1,  335,  336,   -1,  318,   -1,  340,   -1,
  342,   91,   -1,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
   -1,   40,  374,  123,  273,  274,  378,  379,  380,  381,
  382,  292,  384,  385,  386,  387,  388,  389,  390,   -1,
  392,   -1,   -1,  304,  305,   -1,  307,  308,   -1,   -1,
  311,  403,  313,  314,  315,  316,  317,  261,  262,   -1,
   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,  393,
  394,  395,  396,  397,  398,  399,  400,  401,  402,   -1,
   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,
   -1,  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,   -1,  356,  357,   -1,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,   40,   -1,  374,   -1,   -1,   -1,  378,
  379,  380,  381,  382,   -1,  384,  385,  386,  387,  388,
  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  274,  403,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,  414,  415,  416,  417,   -1,
  419,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,  395,  396,  397,  398,   -1,   -1,   -1,   -1,   -1,
  404,  405,  406,  407,  408,  409,  410,  411,  412,  413,
  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,
   -1,  342,   40,   -1,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,   -1,  356,  357,   -1,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,   -1,   -1,  374,  273,  274,   -1,  378,  379,  380,
  381,  382,   -1,  384,  385,  386,  387,  388,  389,  390,
   -1,  392,   -1,   -1,   -1,   -1,   -1,  377,   -1,  257,
  258,  259,  403,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,
   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,  286,   -1,
   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,
   -1,  340,   -1,  342,   40,   -1,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,   -1,  356,  357,   -1,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,   -1,   -1,  374,  273,  274,   -1,  378,
  379,  380,  381,  382,   -1,  384,  385,  386,  387,  388,
  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,
  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,
   -1,   -1,   -1,  340,   -1,  342,   40,   -1,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  354,   -1,  356,
  357,   -1,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  273,  274,  374,   -1,   -1,
   -1,  378,  379,  380,  381,  382,   -1,  384,  385,  386,
  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  403,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,  414,  415,  416,
  417,   -1,  419,   -1,   -1,   -1,  276,   -1,   -1,  279,
   -1,   -1,   -1,  331,  332,  333,  286,  335,  336,   -1,
   -1,   -1,  340,   -1,  342,   40,   -1,  345,  346,  347,
  348,  349,  350,  351,  352,  353,  354,   -1,  356,  357,
   -1,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,   -1,   -1,  374,  273,  274,   -1,
  378,  379,  380,  381,  382,   -1,  384,  385,  386,  387,
  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,
   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,
  336,   -1,   -1,   -1,  340,   -1,  342,   40,   -1,  345,
  346,  347,  348,  349,  350,  351,  352,  353,  354,   -1,
  356,  357,   -1,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,   -1,   -1,  374,  273,
  274,   -1,  378,  379,  380,  381,  382,   -1,  384,  385,
  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,
  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,
   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,   40,
   -1,  345,  346,  347,  348,  349,  350,  351,  352,  353,
  354,   -1,  356,  357,   -1,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  273,  274,
  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,
  384,  385,  386,  387,  388,  389,  390,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,
  335,  336,   -1,   -1,   -1,  340,   -1,  342,   40,   -1,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
   -1,  356,  357,   -1,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,   -1,   -1,  374,
  273,  274,   -1,  378,  379,  380,  381,  382,   -1,  384,
  385,  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,
  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,
  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,  351,  352,
  353,  354,   -1,  356,  357,   -1,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,   -1,
   -1,  374,  273,  274,   -1,  378,  379,  380,  381,  382,
   -1,  384,  385,  386,  387,  388,  389,  390,   -1,  392,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   42,   -1,   -1,   -1,   -1,
  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,
   -1,  342,   -1,   60,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,   -1,  356,  357,   -1,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  273,  274,  374,   91,   -1,   -1,  378,  379,  380,
  381,  382,   -1,  384,  385,  386,  387,  388,  389,  390,
   42,  392,   44,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  403,   -1,   -1,   -1,  123,   -1,   60,   -1,
   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,
   -1,   -1,   -1,   -1,   42,   -1,   -1,   -1,   -1,  331,
  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,   91,
  342,   -1,   60,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
   -1,  123,  374,   91,   -1,   -1,  378,  379,  380,  381,
  382,   -1,  384,  385,  386,  387,  388,  389,  390,   42,
  392,   44,   -1,   -1,   -1,   -1,   -1,   -1,   42,   -1,
   -1,  403,   -1,   -1,   -1,  123,   -1,   60,   -1,   -1,
   -1,   -1,  414,  415,  416,  417,   60,  419,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,
  257,  258,  259,   -1,  261,  262,  263,   91,  265,   60,
   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,   60,  279,   -1,   -1,   -1,   -1,   -1,   -1,  286,
  123,   -1,   -1,   -1,   -1,  292,   -1,   -1,   -1,  123,
   91,   -1,   -1,   -1,   -1,   -1,   60,  304,  305,   -1,
  307,  308,   91,   -1,  311,   -1,  313,  314,  315,  316,
  317,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,  123,  265,   -1,   60,   -1,   91,   -1,   -1,
   -1,  273,   -1,   -1,  123,   -1,   -1,  279,   -1,   60,
   -1,   -1,   -1,   -1,  286,   60,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,   91,  265,   -1,  123,
   -1,   -1,   -1,   -1,  272,  273,   -1,  374,   -1,   -1,
   91,  279,   -1,   -1,   -1,   -1,   91,   -1,  286,   60,
   -1,  388,  389,  390,   -1,   -1,   60,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,  123,   -1,
   91,   -1,   -1,   -1,  257,  258,  259,   91,  261,  262,
  263,   -1,  265,  257,  258,  259,   -1,  261,  262,  263,
  273,  265,  374,   -1,   -1,   -1,  279,   -1,   -1,  273,
   -1,   -1,  123,  286,   -1,  279,  388,  389,  390,  123,
  125,   -1,  286,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,  374,   -1,   -1,   -1,
   -1,  272,  273,   -1,   -1,  264,   -1,   -1,  279,   -1,
  388,  389,  390,   -1,  273,  286,   -1,   -1,   -1,   -1,
   -1,  292,  281,  282,  283,  284,  285,   -1,   -1,   -1,
  264,   -1,   -1,  304,  305,   -1,  307,  308,   -1,  273,
  311,   -1,  313,  314,  315,  316,  317,  281,  282,  283,
  284,  285,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,  374,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
  374,   -1,   -1,  264,  279,  388,  389,  390,   -1,  264,
  125,  286,  273,   -1,  388,  389,  390,   -1,  273,  260,
  281,  282,  283,  284,  285,   -1,  281,  282,  283,  284,
  285,   -1,   -1,  374,   -1,  276,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,  375,  388,  389,  390,
  264,   -1,  273,   -1,  358,   -1,   -1,   -1,  273,  273,
  281,  282,  283,  284,  285,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   -1,  316,  317,   -1,   -1,   -1,
  321,  322,   -1,  324,  325,  326,  327,   -1,   -1,   -1,
   -1,   -1,  306,   -1,   -1,   -1,   -1,  358,   -1,  374,
   -1,   -1,   -1,  358,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  388,  389,  390,  331,  332,  333,   -1,
  335,  336,  343,  344,   -1,  340,   -1,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
   -1,  356,  357,   -1,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,   -1,  273,  374,
   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,
  385,  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,
  415,  416,  417,   -1,  419,   -1,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,
  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
   -1,  356,  357,   -1,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  273,   -1,  374,
   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,
  385,  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,
  415,  416,  417,   -1,  419,   -1,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,
  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  351,  352,  353,  354,   -1,
  356,  357,   -1,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  273,   -1,  374,   -1,
   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,  385,
  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,
  416,  417,   -1,  419,   -1,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,
   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  354,   -1,  356,
  357,   -1,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  273,   -1,  374,   -1,   -1,
   -1,  378,  379,  380,  381,  382,   -1,  384,  385,  386,
  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   60,   -1,  414,  415,  416,
  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,
   -1,   -1,  340,   -1,  342,   -1,   91,  345,  346,  347,
  348,  349,  350,  351,  352,  353,  354,   -1,  356,  357,
   -1,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  273,   60,  374,   -1,  123,   -1,
  378,  379,  380,  381,  382,   -1,  384,  385,  386,  387,
  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,   91,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,   -1,  414,  415,  416,  417,
   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  331,  332,  333,   -1,  335,  336,  123,   -1,
   -1,  340,   -1,  342,   -1,   91,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,   -1,  356,  357,   -1,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,   -1,   -1,  374,   -1,  123,   60,  378,
  379,  380,  381,  382,   -1,  384,  385,  386,  387,  388,
  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,  264,
  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  123,   60,   -1,   -1,   -1,   -1,  292,   -1,   -1,
   -1,   -1,  297,  298,  299,  300,  301,  302,  303,  304,
  305,   -1,  307,  308,  309,   -1,  311,   -1,  313,  314,
  315,  316,  317,   91,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,  329,  330,   -1,   -1,  273,   -1,
   60,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   60,   -1,  123,   -1,  292,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,  304,
  305,   91,  307,  308,   -1,   -1,  311,  273,  313,  314,
  315,  316,  317,  318,   91,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   60,   -1,  292,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   60,  304,  305,
   -1,  307,  308,   -1,   -1,  311,  123,  313,  314,  315,
  316,  317,  318,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   41,   91,   -1,
   60,  273,   -1,   41,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   60,  123,   -1,   41,
  292,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   91,  304,  305,   -1,  307,  308,   -1,   60,  311,
   -1,  313,  314,  315,  316,  317,  318,   91,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,  273,   -1,   60,   -1,   91,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,  123,
   -1,   -1,   -1,   -1,  292,  123,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  303,  304,  305,   91,  307,
  308,  123,   60,  311,  264,  313,  314,  315,  316,  317,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  264,   60,   -1,
   -1,  281,  282,  283,  284,  285,  273,   -1,   -1,   -1,
  123,   60,  292,   91,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,  304,  305,   -1,  307,  308,   91,
   -1,  311,   -1,  313,  314,  315,  316,  317,  264,  306,
   -1,   -1,   91,   -1,   60,  123,   -1,  273,   -1,   -1,
   -1,  264,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  273,  123,   -1,  125,   -1,  278,  292,   60,  281,  282,
  283,  284,  285,   -1,  123,   91,  125,   -1,  304,  305,
  293,  307,  308,   -1,  264,  311,   -1,  313,  314,  315,
  316,  317,   -1,  273,   -1,   -1,   -1,   -1,   91,   -1,
  264,  281,  282,  283,  284,  285,  264,  123,  125,  273,
   -1,   -1,   -1,  293,  278,  273,   -1,  281,  282,  283,
  284,  285,  264,  281,  282,  283,  284,  285,   60,  293,
  123,  273,   -1,   -1,   60,  293,  278,   -1,   -1,  281,
  282,  283,  284,  285,  257,  258,  259,  260,  261,  262,
  263,  264,  265,   -1,   -1,   -1,   -1,  585,   -1,   91,
  273,  274,   -1,  276,   -1,   91,  279,   93,  281,  282,
  283,  284,  285,  286,   -1,   -1,  125,   -1,   60,  257,
  258,  259,  260,  261,  262,  263,  264,  265,   -1,  617,
  618,  123,   -1,   -1,   -1,  273,  274,  123,  276,   -1,
   60,  279,  264,  281,  282,  283,  284,  285,  286,   91,
   -1,  273,   -1,   60,   -1,  264,  265,   -1,   -1,  281,
  282,  283,  284,  285,  273,  274,   -1,   -1,   -1,   -1,
   -1,   91,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,  123,   -1,  260,   91,   -1,   -1,   -1,  264,  265,
  678,  679,   -1,  681,   -1,   -1,   -1,  273,  274,  276,
   -1,   -1,   -1,  123,   -1,  281,  282,  283,  284,  285,
   -1,  264,  265,   -1,   -1,   -1,  123,   -1,   -1,  707,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,  316,
  317,   -1,   -1,   -1,  321,  322,   -1,  324,  325,  326,
  327,  260,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  754,  276,  756,  757,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  273,  274,   -1,   -1,   -1,   -1,  273,   -1,  281,
  282,  283,  284,  285,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,   -1,
   -1,   -1,  321,  322,   -1,  324,  325,  326,  327,  807,
   -1,   -1,  264,  273,  274,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  264,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,
  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  351,  352,  353,  354,   -1,  356,  357,   -1,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  273,  274,  374,   -1,   -1,   -1,  378,  379,
  380,  381,  382,   -1,  384,  385,  386,  387,  388,  389,
  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,
   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,   -1,  356,  357,   -1,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  273,  274,  374,   -1,   -1,   -1,  378,  379,  380,
  381,  382,   -1,  384,  385,  386,  387,  388,  389,  390,
   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,
  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  273,  274,  374,   -1,   -1,   -1,  378,  379,  380,  381,
  382,   -1,  384,  385,  386,  387,  388,  389,  390,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,
  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,  351,  352,
  353,  354,   -1,  356,  357,   -1,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  273,
  274,  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,
   -1,  384,  385,  386,  387,  388,  389,  390,   -1,  392,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,
   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  351,  352,  353,
  354,   -1,  356,  357,   -1,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  273,  274,
  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,
  384,  385,  386,  387,  388,  389,  390,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,
  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
   -1,  356,  357,   -1,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  273,  274,  374,
   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,
  385,  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,
  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,
  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  351,  352,  353,  354,   -1,
  356,  357,   -1,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  273,  274,  374,   -1,
   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,  385,
  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,
  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,
   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  354,   -1,  356,
  357,   -1,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  273,  274,  374,   -1,   -1,
   -1,  378,  379,  380,  381,  382,   -1,  384,  385,  386,
  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,
  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,
   -1,   -1,  340,   -1,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  351,  352,  353,  354,   -1,  356,  357,
   -1,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  273,  274,  374,   -1,   -1,   -1,
  378,  379,  380,  381,  382,   -1,  384,  385,  386,  387,
  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,
   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,
   -1,  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,   -1,  356,  357,   -1,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  273,  274,  374,   -1,   -1,   -1,  378,
  379,  380,  381,  382,   -1,  384,  385,  386,  387,  388,
  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,
  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,
  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  351,  352,  353,  354,   -1,  356,  357,   -1,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  273,  274,  374,   -1,   -1,   -1,  378,  379,
  380,  381,  382,   -1,  384,  385,  386,  387,  388,  389,
  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,
   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,   -1,  356,  357,   -1,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  273,  274,  374,   -1,   -1,   -1,  378,  379,  380,
  381,  382,   -1,  384,  385,  386,  387,  388,  389,  390,
   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,
  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  273,  274,  374,   -1,   -1,   -1,  378,  379,  380,  381,
  382,   -1,  384,  385,  386,  387,  388,  389,  390,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,
  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,  351,  352,
  353,  354,   -1,  356,  357,   -1,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  273,
  274,  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,
   -1,  384,  385,  386,  387,  388,  389,  390,   -1,  392,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,
   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  351,  352,  353,
  354,   -1,  356,  357,   -1,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  273,  274,
  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,
  384,  385,  386,  387,  388,  389,  390,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,
  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
   -1,  356,  357,   -1,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  273,  274,  374,
   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,
  385,  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,
  415,  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,
  336,   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  351,  352,  353,  354,   -1,
  356,  357,   -1,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  273,  274,  374,   -1,
   -1,   -1,  378,  379,  380,  381,  382,   -1,  384,  385,
  386,  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,
  416,  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,
   -1,   -1,   -1,  340,   -1,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  354,   -1,  356,
  357,   -1,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  273,  274,  374,   -1,   -1,
   -1,  378,  379,  380,  381,  382,   -1,  384,  385,  386,
  387,  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,
  417,   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,
   -1,   -1,  340,   -1,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  351,  352,  353,  354,   -1,  356,  357,
   -1,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  273,  274,  374,   -1,   -1,   -1,
  378,  379,  380,  381,  382,   -1,  384,  385,  386,  387,
  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,
   -1,  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,
   -1,  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,   -1,  356,  357,   -1,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  273,   -1,  374,   -1,   -1,   -1,  378,
  379,  380,  381,  382,   -1,  384,  385,  386,  387,  388,
  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,
  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,
  340,   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  351,  352,  353,  354,   -1,  356,  357,   -1,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  273,   -1,  374,   -1,   -1,   -1,  378,  379,
  380,  381,  382,   -1,  384,  385,  386,  387,  388,  389,
  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  331,  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,
   -1,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,   -1,  356,  357,   -1,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  273,   -1,  374,   -1,   -1,   -1,  378,  379,  380,
  381,  382,   -1,  384,  385,  386,  387,  388,  389,  390,
   -1,  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,
  332,  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,   -1,  356,  357,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  273,   -1,  374,   -1,   -1,   -1,  378,  379,  380,  381,
  382,   -1,  384,  385,  386,  387,  388,  389,  390,   -1,
  392,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,
  333,   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,  351,  352,
  353,  354,   -1,  356,  357,   -1,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  273,
   -1,  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,
   -1,  384,  385,  386,  387,  388,  389,  390,   -1,  392,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  403,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  414,  415,  416,  417,   -1,  419,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  331,  332,  333,
   -1,  335,  336,   -1,   -1,   -1,  340,   -1,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  351,  352,  353,
  354,   -1,  356,  357,   -1,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,   -1,   -1,
  374,   -1,   -1,   -1,  378,  379,  380,  381,  382,   -1,
  384,  385,  386,  387,  388,  389,  390,   -1,  392,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  403,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,   -1,   -1,
  414,  415,  416,  417,  342,  419,   -1,  345,  346,  347,
  348,  349,  350,  351,  352,  353,  354,   -1,  356,  357,
   -1,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,   -1,   -1,  374,   -1,   -1,   -1,
  378,  379,  380,  381,  382,   -1,  384,  385,  386,  387,
  388,  389,  390,   -1,  392,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  403,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  414,  415,  416,  417,
   -1,  419,
  };

#line 1295 "Iril/IR/IR.jay"

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
  public const int WEAK_ODR = 302;
  public const int FASTCC = 303;
  public const int SIGNEXT = 304;
  public const int ZEROEXT = 305;
  public const int VOLATILE = 306;
  public const int RETURNED = 307;
  public const int DEREFERENCEABLE = 308;
  public const int AVAILABLE_EXTERNALLY = 309;
  public const int PERSONALITY = 310;
  public const int SRET = 311;
  public const int CLEANUP = 312;
  public const int NONNULL = 313;
  public const int NOCAPTURE = 314;
  public const int WRITEONLY = 315;
  public const int READONLY = 316;
  public const int READNONE = 317;
  public const int HIDDEN = 318;
  public const int ATTRIBUTE_GROUP_REF = 319;
  public const int ATTRIBUTES = 320;
  public const int NORECURSE = 321;
  public const int NOUNWIND = 322;
  public const int UNWIND = 323;
  public const int SPECULATABLE = 324;
  public const int SSP = 325;
  public const int UWTABLE = 326;
  public const int ARGMEMONLY = 327;
  public const int SEQ_CST = 328;
  public const int DSO_LOCAL = 329;
  public const int DSO_PREEMPTABLE = 330;
  public const int RET = 331;
  public const int BR = 332;
  public const int SWITCH = 333;
  public const int INDIRECTBR = 334;
  public const int INVOKE = 335;
  public const int RESUME = 336;
  public const int CATCHSWITCH = 337;
  public const int CATCHRET = 338;
  public const int CLEANUPRET = 339;
  public const int UNREACHABLE = 340;
  public const int FNEG = 341;
  public const int ADD = 342;
  public const int NUW = 343;
  public const int NSW = 344;
  public const int FADD = 345;
  public const int SUB = 346;
  public const int FSUB = 347;
  public const int MUL = 348;
  public const int FMUL = 349;
  public const int UDIV = 350;
  public const int SDIV = 351;
  public const int FDIV = 352;
  public const int UREM = 353;
  public const int SREM = 354;
  public const int FREM = 355;
  public const int SHL = 356;
  public const int LSHR = 357;
  public const int EXACT = 358;
  public const int ASHR = 359;
  public const int AND = 360;
  public const int OR = 361;
  public const int XOR = 362;
  public const int EXTRACTELEMENT = 363;
  public const int INSERTELEMENT = 364;
  public const int SHUFFLEVECTOR = 365;
  public const int EXTRACTVALUE = 366;
  public const int INSERTVALUE = 367;
  public const int ALLOCA = 368;
  public const int LOAD = 369;
  public const int STORE = 370;
  public const int FENCE = 371;
  public const int CMPXCHG = 372;
  public const int ATOMICRMW = 373;
  public const int GETELEMENTPTR = 374;
  public const int ALIGN = 375;
  public const int INBOUNDS = 376;
  public const int INRANGE = 377;
  public const int TRUNC = 378;
  public const int ZEXT = 379;
  public const int SEXT = 380;
  public const int FPTRUNC = 381;
  public const int FPEXT = 382;
  public const int TO = 383;
  public const int FPTOUI = 384;
  public const int FPTOSI = 385;
  public const int UITOFP = 386;
  public const int SITOFP = 387;
  public const int PTRTOINT = 388;
  public const int INTTOPTR = 389;
  public const int BITCAST = 390;
  public const int ADDRSPACECAST = 391;
  public const int ICMP = 392;
  public const int EQ = 393;
  public const int NE = 394;
  public const int UGT = 395;
  public const int UGE = 396;
  public const int ULT = 397;
  public const int ULE = 398;
  public const int SGT = 399;
  public const int SGE = 400;
  public const int SLT = 401;
  public const int SLE = 402;
  public const int FCMP = 403;
  public const int OEQ = 404;
  public const int OGT = 405;
  public const int OGE = 406;
  public const int OLT = 407;
  public const int OLE = 408;
  public const int ONE = 409;
  public const int ORD = 410;
  public const int UEQ = 411;
  public const int UNE = 412;
  public const int UNO = 413;
  public const int PHI = 414;
  public const int SELECT = 415;
  public const int CALL = 416;
  public const int TAIL = 417;
  public const int VA_ARG = 418;
  public const int LANDINGPAD = 419;
  public const int CATCH = 420;
  public const int CATCHPAD = 421;
  public const int CLEANUPPAD = 422;
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
