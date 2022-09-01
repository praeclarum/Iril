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
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type nonglobal_value ',' SECTION STRING",
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
//t    "linkage : APPENDING",
//t    "linkage : COMMON",
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
//t    "attribute : WRITEONLY",
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
//t    "parameter : type LOCAL_SYMBOL",
//t    "parameter : type parameter_attributes",
//t    "parameter : type parameter_attributes LOCAL_SYMBOL",
//t    "parameter : METADATA",
//t    "parameter : ELLIPSIS",
//t    "parameter_attributes : parameter_attribute",
//t    "parameter_attributes : parameter_attributes parameter_attribute",
//t    "parameter_attribute : NONNULL",
//t    "parameter_attribute : NOCAPTURE",
//t    "parameter_attribute : NOUNDEF",
//t    "parameter_attribute : READONLY",
//t    "parameter_attribute : WRITEONLY",
//t    "parameter_attribute : READNONE",
//t    "parameter_attribute : SIGNEXT",
//t    "parameter_attribute : ZEROEXT",
//t    "parameter_attribute : RETURNED",
//t    "parameter_attribute : SRET",
//t    "parameter_attribute : NOALIAS",
//t    "parameter_attribute : BYVAL",
//t    "parameter_attribute : DEREFERENCEABLE '(' INTEGER ')'",
//t    "parameter_attribute : ALIGN INTEGER",
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
//t    "nonglobal_value : GETELEMENTPTR '(' type ',' typed_value ',' element_indices ')'",
//t    "nonglobal_value : BITCAST '(' typed_value TO type ')'",
//t    "nonglobal_value : PTRTOINT '(' typed_value TO type ')'",
//t    "nonglobal_value : '<' typed_values '>'",
//t    "nonglobal_value : '[' typed_values ']'",
//t    "nonglobal_value : '{' typed_values '}'",
//t    "nonglobal_value : '<' '{' typed_values '}' '>'",
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
//t    "inline_assembly : ASM SIDEEFFECT STRING ',' STRING",
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
//t    "instruction : CALL parameter_attribute return_type function_pointer function_args",
//t    "instruction : CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL calling_convention parameter_attributes return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention parameter_attributes return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type inline_assembly function_args attribute_group_refs",
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
    "SECTION","TYPE","HALF","FLOAT","DOUBLE","X86_FP80","INTEGER_TYPE",
    "ZEROINITIALIZER","OPAQUE","DEFINE","DECLARE","UNNAMED_ADDR",
    "LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS","GLOBAL","CONSTANT",
    "PRIVATE","INTERNAL","EXTERNAL","LINKONCE","LINKONCE_ODR","WEAK",
    "WEAK_ODR","APPENDING","COMMON","FASTCC","SIGNEXT","ZEROEXT",
    "VOLATILE","RETURNED","DEREFERENCEABLE","AVAILABLE_EXTERNALLY",
    "PERSONALITY","SRET","CLEANUP","NONNULL","NOCAPTURE","WRITEONLY",
    "READONLY","READNONE","HIDDEN","BYVAL","ATTRIBUTE_GROUP_REF",
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
    "UNE","UNO","PHI","SELECT","CALL","TAIL","VA_ARG","ASM","SIDEEFFECT",
    "LANDINGPAD","CATCH","CATCHPAD","CLEANUPPAD","NOUNDEF",
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
#line 62 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 66 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 70 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 74 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 78 "Iril/IR/IR.jay"
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
#line 98 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 102 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 111 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 123 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 19:
#line 127 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-3+yyTop], isConstant: (bool)yyVals[-3+yyTop]);
    }
  break;
case 20:
#line 131 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 135 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 139 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 143 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 24:
#line 147 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 151 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 26:
#line 155 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 27:
#line 159 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 28:
#line 163 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 29:
#line 167 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 30:
#line 171 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 31:
#line 175 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 32:
#line 179 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 33:
#line 183 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 34:
#line 187 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 35:
#line 191 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 36:
#line 192 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 196 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 197 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 198 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 40:
#line 199 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
#line 200 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 42:
#line 201 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 202 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 203 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
#line 204 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 46:
#line 208 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 47:
#line 212 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 48:
  case_48();
  break;
case 49:
  case_49();
  break;
case 50:
#line 229 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 51:
#line 230 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 52:
#line 231 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 53:
#line 235 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 54:
#line 239 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 65:
#line 268 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 272 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 279 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 68:
#line 283 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 69:
#line 287 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 70:
#line 291 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 71:
#line 295 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 90:
#line 329 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 91:
#line 333 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 92:
#line 337 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 93:
#line 344 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 348 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 353 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 99:
#line 359 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 100:
#line 360 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 101:
#line 361 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 102:
#line 362 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 103:
#line 366 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 104:
#line 370 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 374 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 106:
#line 378 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 107:
#line 382 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 386 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 109:
#line 390 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 110:
#line 397 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 401 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 409 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 114:
  case_114();
  break;
case 115:
  case_115();
  break;
case 125:
#line 441 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 126:
#line 445 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 127:
#line 449 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 128:
#line 453 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 129:
#line 457 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 130:
#line 464 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 131:
#line 468 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 132:
#line 472 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 137:
#line 483 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 138:
#line 490 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 494 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 140:
#line 498 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 502 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 506 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 146:
#line 516 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 147:
#line 517 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 148:
#line 524 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 528 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 535 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 539 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 543 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 547 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-2+yyTop]);
    }
  break;
case 154:
#line 551 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 155:
#line 555 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 157:
#line 563 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 567 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 159:
#line 568 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 160:
#line 569 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoUndef; }
  break;
case 161:
#line 570 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 162:
#line 571 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 163:
#line 572 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 164:
#line 573 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 165:
#line 574 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 166:
#line 575 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 167:
#line 576 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 168:
#line 577 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 169:
#line 578 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 170:
#line 582 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 171:
#line 586 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 179:
#line 609 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 180:
#line 610 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 181:
#line 611 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 182:
#line 612 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 183:
#line 613 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 184:
#line 614 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 185:
#line 615 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 186:
#line 616 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 187:
#line 617 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 188:
#line 618 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 189:
#line 622 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 190:
#line 623 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 191:
#line 624 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 192:
#line 625 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 193:
#line 626 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 194:
#line 627 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 195:
#line 628 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 196:
#line 629 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 197:
#line 630 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 198:
#line 631 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 199:
#line 632 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 200:
#line 633 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 201:
#line 634 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 202:
#line 635 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 203:
#line 636 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 204:
#line 637 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 205:
#line 641 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 209:
#line 651 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 210:
#line 655 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 659 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 663 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 214:
#line 671 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 215:
#line 675 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 216:
#line 679 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 683 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 687 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 220:
#line 695 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 221:
#line 696 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 222:
#line 697 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 223:
#line 698 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 224:
#line 699 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 225:
#line 700 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 226:
#line 701 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 227:
#line 702 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 228:
#line 703 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 229:
#line 710 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 717 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 721 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 232:
#line 728 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 735 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 739 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 746 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 754 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 238:
#line 761 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 765 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 776 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 780 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 787 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 791 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 798 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 802 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 247:
#line 806 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 810 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 249:
#line 817 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 821 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 828 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 848 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 257:
#line 849 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 258:
#line 856 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 860 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 867 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 261:
#line 871 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 262:
#line 875 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 263:
#line 879 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 264:
#line 883 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 265:
#line 887 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 266:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 268:
#line 896 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 269:
#line 900 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 270:
#line 904 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 271:
#line 908 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 272:
#line 912 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 277:
#line 929 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 933 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 280:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 975 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 289:
#line 982 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 993 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1000 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1004 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1008 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1012 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1016 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 297:
#line 1020 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 299:
#line 1028 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1032 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1036 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1040 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1048 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1052 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1056 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 306:
#line 1060 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 307:
#line 1064 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1068 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 309:
#line 1072 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 310:
#line 1076 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1080 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 312:
#line 1084 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 313:
#line 1088 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 314:
#line 1092 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 315:
#line 1096 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 316:
#line 1100 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 317:
#line 1104 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 318:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 319:
#line 1112 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 320:
#line 1116 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 321:
#line 1120 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 322:
#line 1124 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 323:
#line 1128 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 324:
#line 1132 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 325:
#line 1136 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 326:
#line 1140 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 327:
#line 1144 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 328:
#line 1148 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1152 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1156 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1160 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1164 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1168 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1172 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1176 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1180 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1184 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1188 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1192 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1196 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1200 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1204 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1208 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1212 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1216 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1220 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 347:
#line 1224 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1228 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 349:
#line 1232 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 350:
#line 1236 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 351:
#line 1240 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 352:
#line 1244 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1248 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1252 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1256 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1260 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1264 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1268 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1272 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1276 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1280 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1284 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1288 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1292 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1296 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1300 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 367:
#line 1304 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 368:
#line 1308 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1312 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1316 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1320 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1324 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 373:
#line 1328 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 374:
#line 1332 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 375:
#line 1336 "Iril/IR/IR.jay"
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
#line 80 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 85 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 90 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.AddGlobalVariable(g);
    }

void case_15()
#line 104 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 113 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_48()
#line 217 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_49()
#line 222 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_114()
#line 414 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_115()
#line 419 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    6,    6,   11,   11,   10,   10,   10,   10,
   10,   10,   10,   10,   10,   17,   14,    9,    9,   18,
   18,   18,   18,   18,   19,   22,   22,   23,   24,   24,
   24,   24,   24,   24,   15,   15,    8,    8,    8,    8,
    8,   26,   26,   26,    7,    7,   28,   28,   28,   28,
   28,   28,   28,   28,   28,   28,   28,   28,   28,    3,
    3,    3,   29,   29,   30,   30,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   31,
   31,   32,   32,    4,    4,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   33,   33,   33,   33,   33,   40,
   40,   40,   40,   40,   40,   40,   38,    5,    5,    5,
    5,    5,   44,   44,   44,   34,   34,   45,   45,   46,
   46,   46,   46,   46,   46,   41,   41,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   39,   39,   39,   39,
   39,   16,   16,   42,   42,   37,   37,   47,   48,   48,
   48,   48,   48,   48,   48,   48,   48,   48,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   50,   51,   51,   13,   13,   13,
   13,   13,   13,   13,   13,   13,   13,   13,   54,   20,
   20,   20,   20,   20,   20,   20,   20,   20,   55,   27,
   27,   56,   53,   53,   25,   57,   57,   52,   52,   58,
   59,   59,   36,   36,   60,   60,   60,   60,   61,   61,
   63,   63,   63,   63,   65,   66,   66,   67,   67,   68,
   68,   68,   68,   68,   68,   68,   69,   69,   69,   69,
   69,   69,   21,   21,   70,   70,   71,   71,   72,   73,
   73,   74,   75,   75,   76,   76,   43,   77,   78,   62,
   62,   79,   79,   79,   79,   79,   79,   79,   80,   80,
   80,   80,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    6,    6,    9,
    9,   10,   10,   10,   10,    7,   11,    9,   10,   11,
    9,   10,    8,    5,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    3,
    3,    3,    6,    5,    1,    1,    3,    1,    1,    1,
    1,    1,    1,    1,    2,    3,    1,    2,    3,    3,
    3,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    3,    1,    1,    1,    1,    4,    2,
    3,    5,    1,    3,    1,    1,    1,    1,    1,    1,
    1,    1,    3,    4,    2,    4,    1,    5,    5,    1,
    3,    1,    1,    7,    8,    1,    2,    4,    3,    5,
    1,    3,    2,    4,    2,    3,    3,    4,    4,    1,
    1,    1,    1,    2,    3,    2,    2,    4,    5,    6,
    6,    7,    1,    2,    1,    3,    2,    1,    3,    1,
    2,    2,    3,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    4,
    2,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    6,
    9,    8,    6,    6,    3,    3,    3,    5,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    2,
    1,    2,    1,    3,    2,    1,    2,    1,    3,    1,
    1,    3,    1,    2,    2,    3,    1,    2,    1,    2,
    1,    2,    3,    4,    1,    3,    2,    1,    3,    2,
    3,    3,    3,    2,    4,    5,    1,    1,    6,    9,
    6,    6,    1,    3,    1,    1,    1,    3,    5,    1,
    2,    3,    1,    2,    1,    1,    1,    1,    5,    1,
    3,    2,    7,    2,    2,    7,    1,    1,    8,    9,
    9,   10,    5,    6,    5,    7,    5,    5,    6,    4,
    4,    5,    6,    6,    7,    5,    5,    6,    6,    6,
    7,    5,    6,    7,    7,    8,    6,    4,    4,    5,
    6,    5,    2,    5,    4,    4,    4,    4,    5,    6,
    7,    6,    6,    6,    4,    3,    4,    7,    8,    5,
    6,    5,    5,    6,    3,    4,    5,    6,    7,    4,
    5,    6,    6,    4,    5,    7,    8,    5,    6,    4,
    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   96,  107,   99,  100,  101,  102,   98,  130,   39,
   37,   40,   41,   42,   43,   44,   45,  287,  164,  165,
  166,    0,   38,  167,  158,  159,  162,  161,  163,  169,
  174,  175,    0,  160,    0,    0,    0,   97,    0,    0,
    0,    0,    0,  131,  132,    0,    0,  156,    0,    0,
    3,    0,    4,    0,    0,  172,  173,   35,   36,   46,
   47,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  171,   90,    0,    0,    0,    0,    0,    0,    0,
  136,    0,    0,    0,  168,    0,    0,    0,    0,    0,
    0,    0,  157,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    0,    0,    0,    8,    0,    7,    0,
    0,    0,    0,    0,   91,    0,    0,    0,    0,  135,
    0,  113,  103,    0,    0,  110,    0,    0,    0,    0,
    0,    0,    0,  154,  155,  147,    0,    0,  148,  178,
    0,    0,    0,  176,    0,    0,    0,  222,  223,  221,
  224,  225,  226,  220,  209,  228,  227,    0,    0,    0,
    0,    0,    0,    0,    0,  208,    0,    0,    0,    0,
    0,    0,    0,    0,   48,    0,    0,    0,   74,   73,
   13,    0,    0,   67,   72,  170,    0,    0,    0,    0,
  106,  104,    0,    0,    0,    0,    0,  139,    0,    0,
    0,   88,   87,   79,   77,   78,   80,   81,   82,   83,
    0,   75,  151,    0,  146,    0,    0,    0,    0,    0,
    0,    0,  123,  177,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  233,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   68,   14,    0,  205,  207,  206,  230,  108,   92,  109,
  111,  140,    0,    0,  141,    0,    0,   12,   76,  153,
  149,    0,  119,   65,    0,    0,    0,    0,    0,    0,
  297,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  243,    0,
    0,  249,    0,  290,  298,    0,    0,  137,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  217,    0,
    0,    0,  215,  216,    0,    0,    0,    0,    0,   61,
   64,    0,   59,    0,   50,   62,    0,   56,   58,   63,
   60,   51,   52,   49,   17,   16,   71,   70,   69,  142,
   84,  276,  275,    0,  273,    0,    0,  295,    0,    0,
  292,    0,    0,    0,    0,  294,  285,  286,    0,    0,
  283,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  288,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  179,
  180,  181,  182,  183,  184,  185,  186,  187,  188,    0,
  189,  190,  201,  202,  203,  204,  192,  194,  195,  196,
  197,  193,  191,  199,  200,  198,    0,    0,    0,    0,
    0,    0,    0,    0,  114,  244,    0,  250,    0,    0,
   66,    0,  124,   33,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  234,    0,    0,    0,    0,    0,    0,
    0,  235,    0,   89,    0,  120,    0,  291,  229,    0,
    0,  255,    0,    0,    0,    0,    0,    0,  284,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  277,
    0,    0,    0,    0,    0,    0,    0,    0,  346,    0,
    0,  115,   20,    0,   28,    0,    0,    0,    0,    0,
    0,  218,    0,    0,    0,    0,    0,   54,    0,   57,
  274,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  328,    0,    0,  240,  241,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  347,    0,    0,    0,
    0,  214,  210,  213,    0,   23,    0,    0,   53,    0,
    0,    0,  257,    0,    0,  258,    0,    0,    0,    0,
  303,    0,  330,  368,    0,  339,  353,    0,  334,  371,
    0,  357,  332,  373,  365,  361,    0,    0,  350,    0,
  308,  307,  352,  374,    0,    0,    0,    0,  305,    0,
    0,  219,  232,    0,    0,    0,    0,    0,    0,    0,
    0,  278,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  236,    0,  238,    0,
    0,    0,    0,  280,    0,    0,    0,  260,  256,    0,
    0,    0,    0,    0,  304,  369,  354,  358,  362,  351,
  309,  343,  363,  242,    0,    0,    0,    0,    0,    0,
    0,    0,  342,  331,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  237,  212,    0,  293,
    0,  296,  281,    0,  268,  262,    0,    0,    0,    0,
  267,  263,  261,  259,    0,    0,    0,    0,  306,    0,
  348,    0,  366,    0,  279,  359,    0,    0,    0,    0,
    0,  211,  239,  282,  265,    0,    0,    0,    0,    0,
  299,    0,    0,    0,  349,  367,  289,    0,  266,    0,
    0,    0,    0,  300,  301,    0,    0,    0,    0,    0,
  302,    0,    0,    0,    0,    0,  272,  269,  271,    0,
    0,  270,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   58,   12,   13,   14,  231,  202,  194,   59,
   83,  203,  275,   84,  240,  216,   86,  195,  385,  186,
  404,  387,  388,  389,  390,  204,  787,  232,   95,   96,
  145,  146,   15,  116,  162,  348,  217,  243,   68,   63,
   69,   64,   65,  218,  158,  159,  164,  480,  497,  276,
  542,  788,  255,  763,  411,  675,  789,  668,  669,  349,
  350,  351,  352,  353,  543,  636,  725,  726,  852,  405,
  599,  600,  793,  794,  420,  421,  455,  703,  354,  355,
  };
  protected static readonly short [] yySindex = {         1196,
  -25,  -78,    9,   15,   46,  891, 2940, -212,    0, 1196,
    0,    0,    0,    0, -190, -145,   83,  108, 2140, -107,
  -28,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  140,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -54,    0, 4218,  -90,   28,    0, -136,  186,
  211, 4259,   63,    0,    0, 3006,  -17,    0, 3006,  234,
    0,  313,    0,   64,  101,    0,    0,    0,    0,    0,
    0,  -29, 4259,  -91,  -87,   24,  -48,  325,  -27,  244,
  115,    0,    0,  186,   -5,  211,  124, 4259,  128,   91,
    0,   27, 3321,  211,    0, 4259,  211, 3006,  -16, 3006,
  313,  -12,    0,  287,  448, -237,    0,    0, 4259,  -87,
  -87,  750, 4259,  -87, 4259,  -87,    0,  288,    0, -241,
  367,  293, 4075,  372,    0, 4259, 4259,   18, 4259,    0,
  160,    0,    0,  186,   39,    0,  211,  211,  313,  -11,
 -237,  313, 2304,    0,    0,    0,  -39,   50,    0,    0,
   96, -110, -242,    0, 3175, 4259, 4259,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  -30,  380,  382,
  383, 4302, 4307, 4302,  381,    0,  750, 4259,  750, 4259,
  370,  371,  379,  109,    0, -241, 4158,    0,    0,    0,
    0,  -37, 3138,    0,    0,    0,  186,   11,  378,    2,
    0,    0, 3869, -237,  313,   96,   96,    0, -237,  384,
  409,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -108,    0,    0, 4288,    0, 3766, -220,  185, 6546, -103,
 4302,  196,    0,    0,   82,  419,  186, 3241,  424, 4259,
 4302, 4302, 4302,    0,   21, 4245,    4,    8,   87,  423,
  750,  426,  750, 4006, 4039, 1938,    0, -241,  144,  -26,
    0,    0, 4163,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -237,   96,    0,  212, 1840,    0,    0,    0,
    0,  214,    0,    0,  414, 4302, -188, 4302, 2772, 4302,
    0, 2837, 4259, 2837, 4259, 2837, 4259, 4259, 2690, 4259,
 4259, 4259, 2837, 3185, 3279, 4259, 4259, 4259, 4302, 4302,
 4302, 4302, 4302, 4259, 2789, 3316,  145, 1732, 4302, 4302,
 4302, 4302, 4302, 4302, 4302, 4302, 4302, 4302, 4302, 4302,
 1829, 4000, 4259, 4259, 2772,   56, 4259, 3303,    0, 6546,
  206,    0,  206,    0,    0,  207, 6546,    0,  170,  227,
 -250,  107,  443, 4259,  114,  111,  112,  117,    0, 4302,
 3138,   30,    0,    0,  231,  126,  450,  130,  466,    0,
    0,  471,    0, 1521,    0,    0,  388,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  150,    0,  170, 7035,    0,  240, 3095,
    0,  470, 3072, 3006, 3006,    0,    0,    0, 3138, 2837,
    0, 3138, 3138, 2837, 3138, 3138, 2837, 3138, 3138, 4259,
 3138, 3138, 3138, 3138, 3138, 2837, 4259, 3138, 4259, 3138,
 3138, 3138, 3138,  474,  475,  479,  480,  485,  232, 4259,
  242, 4302,  486,    0,    0, 4259,  300,  146,  151,  154,
  158,  164,  165,  168,  169,  176,  177,  178,  180,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4259,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4259,   -4, 3138, 3072,
 4259, 3006, 2772,  -33,    0,    0,  206,    0,  257,  257,
    0, 3409,    0,    0,  272,  280,  296,  187,  320, 4302,
 4259, 4259, 4259,    0,  495,  206,  312,  189,  315,  191,
  687,    0, 4039,    0, 1840,    0,  206,    0,    0,  530,
  310,    0,  550, 3072, 3072, 3006,  553, 3138,    0,  556,
  562, 3138,  563,  565, 3138,  566,  567, 3138,  570,  572,
  573,  576,  577, 3138, 3138,  578, 3138,  579,  580,  582,
  583, 4302, 4302, 4302, 1938, 4302, 2051,  334, 4259,  584,
 4259,  344, 4302, 4259, 4259, 4259, 4259, 4259, 4259, 4259,
 4259, 4259, 4259, 4259, 4259, 3138, 3138, 3095,  586,    0,
  588,  550, 3072, 3072, 4259, 1188, 4259, 3006,    0, 4302,
  257,    0,    0,  206,    0,  337, 4302,  589,  172,  223,
  290,    0,  257,  206,  385,  206,  387,    0,  183,    0,
    0,  257,  310,  547, 3434,  252,  550,  550, 3072, 3095,
  596, 3095, 3095,  597, 3095, 3095,  602, 3095, 3095,  605,
 3095, 3095, 3095, 3095, 3095,  607,  611, 3095,  612, 3095,
 3095, 3095, 3095,    0,  614,  615,    0,    0,  616,  617,
  405,  619, 4259, 3138,  620, 4259,  621, 4302,  622,  186,
  186,  186,  186,  186,  186,  186,  186,  186,  186,  186,
  186,  624,  625,  632,  604, 4302,   96,  550,  550, 3072,
  256,  550,  550, 3072, 3072, 3006,    0,  257,  206,  652,
  836,    0,    0,    0,  257,    0,  257,  206,    0,  655,
 4259, 4193,    0, 1121,  192,    0,  310,  314,  321,  550,
    0, 3095,    0,    0, 3095,    0,    0, 3095,    0,    0,
 3095,    0,    0,    0,    0,    0, 3095, 3095,    0, 3095,
    0,    0,    0,    0, 4302, 4302, 1938, 1938,    0,  324,
  658,    0,    0,  330,  669,  338,  674,  836, 3095, 3095,
 3095,    0,  675,   96,   96,   96,  550,  460,   96,   96,
  550,  550, 3072,  257,  836, 4302,    0,  194,    0,  257,
  310,  679, 4251,    0,  684, 1283, 2648,    0,    0, 4274,
  398,  310,  310,  340,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  616,  472,  358,  482,  364,  487,
  836,  701,    0,    0,  657, 4302,   96,   96,   96,  710,
   96,   96,   96,   96,  550,  229,    0,    0,  836,    0,
  310,    0,    0, 1623,    0,    0,  375,  716,  717,  718,
    0,    0,    0,    0,  310,  432,  433,  310,    0,  504,
    0,  506,    0,  701,    0,    0,   96,  510,   96,   96,
   96,    0,    0,    0,    0,  284,  733, 4302, 4302, 4302,
    0,  310,  310,  437,    0,    0,    0,   96,    0, 4259,
  392,  393,  395,    0,    0,  310,  345, 4259, 4259, 4259,
    0, 4302,  359,  361,  363,  744,    0,    0,    0,  836,
  285,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  790,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 2823, 2612,
  519,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   -3,    0,    0,    0,    0,    0, 2889,
    0,  978,    0,  524,    0,    0,  528,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  763,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  175,    0,    0,  533,  535,    0,    0,
  179,    0,    0,    0,    0,    0,  250,    0,    0,    0,
 -102,    0, -101,    0,   45,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  763,    0,  763,    0,
    0,    0,    0,    0,    0,    0,    0, 1264,    0,    0,
    0,    0,  763,    0,    0,    0,    7,  763,    0,  763,
    0,    0,    0,    0,    0,  253,  323,    0,    0,  724,
  828,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  289,    0,    0,  -98,    0,    0,    0,
    0,    0,    0,    0,    0,  631,  166,  763,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  763,    0,  763,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  643,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3512,    0, 6649,    0,    0,    0,    0,    0,  -97,    0,
    0,    0,    0,    0,  763,    0,    0,    0,    0,    0,
   -3,    0,    0,    0,    0,    0,    0,    0,  793,    0,
    0,   17,    0,  763,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -96,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  763,    0,
    0,  763,  763,    0,  763,  763,    0,  763,  763,    0,
  763,  763,  763,  763,  763,    0,    0,  763,    0,  763,
  763,  763,  763,    0,    0,    0,    0,    0,  763,    0,
  763,    0,    0,    0,    0,    0,  763,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  763,  763,    0,
    0,    0,    0,  763,    0,    0, 3615,    0, 3718, 6752,
    0,    0,    0,    0,    0,    0,    0,    0,  763,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 6855,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  763,    0,    0,
    0,  763,    0,    0,  763,    0,    0,  763,    0,    0,
    0,    0,    0,  763,  763,    0,  763,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  763,    0,    0,
    0,  763,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  763,  763,    0, 4280,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3821,    0,    0,  861,    0,    0,    0,    0,  763,  763,
  763,    0,  897,    0,    0,    0,    0,    0,    0,    0,
    0, 6958,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4383,    0,
    0,    0,    0,  763,    0,    0,    0,    0,    0,  462,
 1370, 1476, 1583, 1689, 1795, 1902, 2008, 2114, 2221, 2327,
 2433,    0,    0,    0,    0,    0, 4486,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1204, 1215,    0,
    0,    0,    0,    0, 1234,    0, 1316, 1422,    0,    0,
    0,    0,    0,  763,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 4589, 4692, 4795,    0,    0, 4898,    0,
    0,    0,    0, 1529,    0,    0,    0,    0,    0, 2566,
    0,    0,    0,    0,  316,  763,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5001,    0,    0,    0,    0,    0,
    0, 5104,    0,    0,    0,    0, 5207, 5310, 5413,    0,
 5516, 5619, 5722, 5825,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5928,    0,    0, 6031,    0, 6134, 6237,
 6340,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 6443,    0,    0,
    0,    0,    0,    0,    0,    0,  763,    0,    0,    0,
    0,    0,  763,  763,  763,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  799,  734,    0,    0,    0,    0,  627,  642,  824,
  105,   -6, 1269,   42,  -64,   65,    0,  591,  592, -139,
 -523,    0,  327,    0, -675,    0,  352,  633,  764,  133,
    0,  650,    0,  -80,    0,  508,  -82, -223,   -2,    0,
  -47,  807,  -57, -155,    0,  634, -121,    0,    0,    0,
  343, -753,  -94,    0, -376, -531,   29,  110,  113, -337,
    0,  525,  526,  467, -146, 2241,    0,   77,    0,  350,
    0,  184,    0,   85, -192,  -61,    0,    0,    0,  481,
  };
  protected static readonly short [] yyTable = {            60,
   60,  101,  102,   62,   90,  132,  273,  629,  102,  250,
  506,  128,  239,  293,  822,  108,  288,  273,  110,  357,
  116,  121,  103,  103,  117,  122,  118,  103,  103,  515,
  151,  836,   98,  163,  191,   16,   95,  102,  136,  192,
   93,  244,  140,  102,   34,  792,   95,  370,   94,  677,
   94,  370,  102,   76,   77,   60,   60,   60,  282,   60,
   60,  136,   60,  285,  370,  373,  113,  864,  214,   19,
  241,  219,  278,  370,   55,   20,  122,  409,  237,  212,
  160,   72,  213,   85,   95,  160,  598,  272,  257,  258,
  235,   94,  241,  236,  280,  244,  144,  410,  396,   60,
  374,   60,  160,   60,  106,  113,   21,  113,  157,  234,
   70,  424,  165,  427,   73,  244,  187,  792,  189,  135,
  436,   93,   56,  120,  386,  386,  393,  400,  516,  207,
  208,   94,  210,  284,  283,  513,  242,  193,   61,   67,
   60,  761,  209,   74,  765,  369,  121,  403,  124,  267,
  126,  220,  268,   57,  525,  102,  911,  520,  292,  247,
  248,  372,  244,  238,  634,   19,   97,  221,   75,   38,
  356,  116,  121,   87,  506,  117,  122,  118,  138,   91,
  161,  261,  536,  263,  395,   55,  119,  268,  123,  125,
  534,   17,   18,  535,  104,  107,   51,   52,  109,   76,
   77,  112,   92,   78,   79,   95,  144,   78,   79,  222,
  223,  224,  712,  102,   95,  112,  225,  226,  112,  227,
  228,  229,  230,  719,  166,  167,  535,  102,  188,  157,
  190,  113,  799,  233,  838,  800,  271,  839,  147,  127,
  148,  415,  150,  365,  532,   88,  131,  271,   89,  371,
  103,  414,  143,  105,  111,  149,  720,  384,  384,  152,
  215,   76,   77,  713,  102,   78,   79,   39,   40,  872,
   41,   42,  839,  102,   44,  577,   45,   46,   47,   48,
   49,  609,   50,  102,   99,  579,  509,  502,  510,   95,
  150,   81,   60,  150,  114,  419,  422,  423,  425,  426,
  428,  429,  431,  432,  433,  434,  435,  438,  440,  441,
  442,  443,   34,   34,   76,   77,   34,  449,  451,   34,
  876,  457,  145,  117,  889,  912,   22,  535,  839,  152,
  714,  102,  152,   34,   34,   23,  498,  499,   60,   53,
  504,  102,  501,  583,   24,   25,   26,   27,   28,  249,
  801,   55,  115,  602,   55,  105,  264,  519,  549,  264,
  118,  102,  549,  617,  130,  549,  133,  546,   34,   39,
   40,  134,   41,   42,  549,  102,   44,  673,   45,   46,
   47,   48,   49,   81,   50,  102,  102,  678,  902,   54,
  137,  403,  610,  386,  139,  403,   38,  637,  638,  907,
  102,  908,  102,  909,  102,  141,  196,   60,   60,  153,
   98,  113,  206,  548,  840,  197,  211,  552,  160,  251,
  555,  252,  253,  558,  259,  856,  857,  264,  265,  564,
  565,  413,  567,   19,   19,  667,  266,   19,   19,  279,
   19,   53,  611,  578,  286,  608,  138,  138,  287,  582,
  138,  138,  359,  138,   19,   19,  698,  699,  294,  702,
  360,  623,  361,  364,  874,  375,  376,  138,  138,  378,
  406,  401,  632,  596,  407,  503,  454,  500,  881,  238,
  511,  884,  241,  514,  205,  517,  518,  526,  156,   19,
  597,   54,  730,  528,   60,   60,   60,  521,  522,  605,
  607,   95,  138,  523,  527,  894,  895,   56,  529,  530,
  531,  533,  539,  541,  619,  620,  621,  572,  573,  901,
  143,  143,  574,  575,  143,  143,  384,  143,  576,  581,
  356,  613,  584,  254,  254,  254,  614,  585,   57,   60,
  586,  143,  143,  113,  587,  277,  544,  545,  205,  708,
  588,  589,  615,  777,  590,  591,  622,  781,  782,  715,
  706,  717,  592,  593,  594,  616,  595,  625,  624,  627,
   55,  626,  674,  633,  674,  409,  143,  680,  681,  682,
  683,  684,  685,  686,  687,  688,  689,  690,  691,  635,
  145,  145,  358,  709,  145,  145,  640,  145,   60,  642,
   60,   60,  366,  367,  368,  643,  645,  254,  646,  648,
  649,  145,  145,  651,  774,  652,  653,  667,  667,  654,
  655,  658,  660,  661,  399,  662,  663,  676,  724,  695,
   18,  696,  711,  603,  604,  606,  835,  721,  727,  732,
  735,  716,  144,  718,  784,  738,  145,  408,  741,  412,
  747,  416,  244,  790,  748,  750,  851,  755,  756,  757,
  758,  759,  760,  764,  766,  768,  674,  769,  770,  674,
  444,  445,  446,  447,  448,  771,  797,  453,  639,  778,
  458,  459,  460,  461,  462,  463,  464,  465,  466,  467,
  468,  469,  827,  828,  598,  785,  831,  832,  791,   60,
  802,  817,  816,  113,  403,  244,  244,  803,  818,  244,
  244,   22,  819,  277,  384,  796,  820,  821,  826,  830,
   23,  524,  841,  844,  855,  154,  858,  628,  859,   24,
   25,   26,   27,   28,  370,  370,  860,  700,  861,  704,
  705,  155,  862,  863,  839,  244,  867,  244,  244,  865,
  869,  870,  540,  868,  877,  878,  879,  880,  882,  883,
  885,  547,  886,  896,  550,  551,  244,  553,  554,  887,
  556,  557,  890,  559,  560,  561,  562,  563,  898,  899,
  566,  900,  568,  569,  570,  571,  384,  910,  888,    1,
  125,  102,   26,  724,  113,  126,  370,  370,  370,  127,
  370,  370,   95,  580,  128,  370,  129,  370,   71,  183,
  370,  370,  370,  370,  370,  370,  370,  370,  370,  370,
  129,  370,  370,  270,  370,  370,  370,  370,  370,  370,
  370,  370,  370,  370,  370,  370,  370,  269,  783,  370,
  184,  601,   82,  370,  370,  370,  370,  370,   85,  370,
  370,  370,  370,  370,  370,  370,  392,  370,  394,  630,
   31,  138,  281,  289,  512,  100,  814,  873,  370,  291,
  815,  618,  182,  537,  507,  508,  854,  843,  772,  370,
  370,  370,  370,  897,  631,    0,  370,  538,    0,    0,
  641,  903,  904,  905,  644,   56,   21,  647,   18,   18,
  650,    0,   18,   18,    0,   18,  656,  657,    0,  659,
  144,  144,    0,    0,  144,  144,    0,  144,    0,   18,
   18,    0,    0,  664,  665,  666,   57,  670,  672,    0,
    0,  144,  144,    0,  679,    0,    0,    0,  692,  693,
  694,    0,    0,  168,  169,  170,    0,  171,  172,  173,
   56,  174,   86,    0,   18,    0,    0,    0,   55,    0,
    0,  707,  402,    0,    0,  176,  144,    0,  710,    0,
    0,    0,    0,  177,    0,    0,    0,  105,    0,    0,
    0,   57,  731,   85,  733,  734,    0,  736,  737,    0,
  739,  740,    0,  742,  743,  744,  745,  746,    0,   85,
  749,    0,  751,  752,  753,  754,  168,  169,  170,    0,
  171,  172,  173,   55,  174,    0,  762,  105,  105,  105,
    0,  105,  175,    0,    0,    0,    0,    0,  176,  767,
    0,    0,    0,    0,    0,    0,  177,  105,    0,  105,
    0,   85,   85,   85,    0,    0,    0,  773,   85,   85,
    0,   85,   85,   85,   85,    0,    0,    0,    0,    0,
   26,   26,    0,    0,   26,   26,  798,   26,  105,    0,
  105,    0,    0,    0,  805,    0,    0,  806,    0,    0,
  807,   26,   26,  808,    0,    0,    0,   86,    0,  809,
  810,    0,  811,    0,    0,    0,    0,    0,    0,  198,
  105,    0,  105,   86,    0,    0,  812,  813,   23,    0,
    0,  823,  824,  825,    0,    0,   26,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,  178,   31,   31,
    0,    0,   31,   31,    0,   31,    0,  837,    0,  853,
    0,  179,  180,  181,    0,   86,   86,   86,    0,   31,
   31,    0,   86,   86,   22,   86,   86,   86,   86,    0,
    0,    0,  102,   23,   21,   21,    0,    0,   21,   21,
    0,   21,   24,   25,   26,   27,   28,  866,    0,    0,
  183,    0,    0,   29,   31,   21,   21,    0,   30,   31,
   32,   33,   34,   35,   36,   37,   38,   39,   40,    0,
   41,   42,   43,   32,   44,    0,   45,   46,   47,   48,
   49,  184,   50,    0,   29,    0,  786,    0,    0,    0,
   21,    0,    0,   51,   52,    0,    0,  103,    0,  891,
  892,  893,    0,   24,  105,  105,  105,    0,  105,  105,
  105,    0,  105,  182,    0,  105,  105,  183,    0,  105,
  105,  105,  105,  906,    0,    0,  105,    0,    0,    0,
    0,    0,    0,    0,  105,    0,  105,  105,    0,   53,
  105,    0,    0,    0,    0,    0,    0,    0,  184,    0,
    0,    0,    0,    0,  105,  105,    0,  105,  105,    0,
    0,  105,  105,  105,  105,  105,  105,  105,    0,  105,
    0,  105,    0,   96,  231,    0,    0,  231,    0,    0,
  182,    0,  105,  105,  105,   22,  105,  105,    0,   54,
    0,  105,    0,  105,  102,  231,  105,  105,  105,  105,
  105,  105,  105,  105,  105,  105,    0,  105,  105,    0,
  105,  105,  105,  105,  105,  105,  105,  105,  105,  105,
  105,  105,  105,    0,    0,  105,  231,    0,    0,  105,
  105,  105,  105,  105,    0,  105,  105,  105,  105,  105,
  105,  105,    0,  105,    0,    0,    0,  168,  169,  170,
    0,  171,  172,  173,  105,  174,  231,    0,  231,    0,
  185,    0,  274,  175,    0,  105,  105,  105,  105,  176,
  105,    0,  105,  105,    0,    0,  105,  177,    0,   95,
    0,    0,    0,  105,    0,    0,    0,    0,    0,    0,
    0,   25,    0,    0,    0,    0,    0,   39,   40,    0,
   41,   42,    0,  246,   44,    0,   45,   46,   47,   48,
   49,    0,   50,    0,  168,  169,  170,    0,  171,  172,
  173,    0,  174,    0,    0,  260,    0,  262,    0,  274,
  175,    0,    0,    1,    2,    0,  176,    3,    4,    0,
    5,   32,   32,    0,  177,   32,   32,    0,   32,    0,
    0,    0,   29,   29,    6,    7,   29,   29,    0,   29,
    0,    0,   32,   32,    0,    0,    0,    0,  178,   53,
    0,   24,   24,   29,   29,   24,   24,    0,   24,    0,
    0,    0,  179,  180,  181,   95,  363,    0,    0,    8,
    0,    0,   24,   24,    0,    0,    0,   32,   30,  377,
    0,  379,    0,    0,    0,    0,  231,  231,   29,  168,
  169,  170,    0,  171,  172,  173,    0,  174,    0,   54,
    0,    0,    0,    0,  845,  846,    0,   24,    0,    0,
    0,  176,  102,    0,    0,  178,    0,    0,    0,  177,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  179,
  180,  181,    0,   22,   22,    0,    0,   22,   22,    0,
   22,    0,    0,    0,    0,    0,    0,    0,  231,  231,
  231,    0,  231,  231,   22,   22,    0,  231,    0,  231,
  701,    0,  231,  231,  231,  231,  231,  231,  231,  231,
  231,  231,   95,  231,  231,    0,  231,  231,  231,  231,
  231,  231,  231,  231,  231,  231,  231,  231,  231,   22,
    0,  231,  375,  375,    0,  231,  231,  231,  231,  231,
  231,  231,  231,  231,  231,  231,  231,  231,    0,  231,
  847,    0,    0,  875,    0,    0,    0,    0,    0,    0,
  231,    0,    0,    0,  848,  849,  850,    0,    0,    0,
    0,  231,  231,  231,  231,    0,    0,    0,  231,   25,
   25,    0,    0,   25,   25,    0,   25,    0,    0,    0,
    0,    0,    0,    0,  375,  375,  375,    0,  375,  375,
   25,   25,    0,  375,    0,  375,    0,    0,  375,  375,
  375,  375,  375,  375,  375,  375,  375,  375,   95,  375,
  375,    0,  375,  375,  375,  375,  375,  375,  375,  375,
  375,  375,  375,  375,  375,   25,    0,  375,  360,  360,
    0,  375,  375,  375,  375,  375,    0,  375,  375,  375,
  375,  375,  375,  375,    0,  375,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  375,  168,  169,  170,
    0,  171,  172,  173,    0,  174,    0,  375,  375,  375,
  375,   56,    0,    0,  375,    0,   30,   30,    0,  176,
   30,   30,    0,   30,    0,    0,    0,  177,    0,    0,
  360,  360,  360,    0,  360,  360,    0,   30,   30,  360,
    0,  360,   57,    0,  360,  360,  360,  360,  360,  360,
  360,  360,  360,  360,   95,  360,  360,    0,  360,  360,
  360,  360,  360,  360,  360,  360,  360,  360,  360,  360,
  360,    0,   30,  360,   55,  338,  338,  360,  360,  360,
  360,  360,    0,  360,  360,  360,  360,  360,  360,  360,
    0,  360,    0,    0,    0,    0,    0,    0,    0,  168,
  169,  170,  360,  171,  172,  173,    0,  174,    0,    0,
    0,    0,    0,  360,  360,  360,  360,    0,  402,    0,
  360,  176,    0,    0,    0,    0,    0,    0,    0,  177,
    0,    0,    0,    0,    0,    0,    0,  338,  338,  338,
    0,  338,  338,    0,    0,    0,  338,    0,  338,    0,
    0,  338,  338,  338,  338,  338,  338,  338,  338,  338,
  338,   95,  338,  338,    0,  338,  338,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,  338,    0,    0,
  338,  335,  335,    0,  338,  338,  338,  338,  338,    0,
  338,  338,  338,  338,  338,  338,  338,    0,  338,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  338,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
  338,  338,  338,  338,   23,    0,    0,  338,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
    0,    0,    0,  335,  335,  335,    0,  335,  335,    0,
    0,    0,  335,    0,  335,    0,    0,  335,  335,  335,
  335,  335,  335,  335,  335,  335,  335,   95,  335,  335,
    0,  335,  335,  335,  335,  335,  335,  335,  335,  335,
  335,  335,  335,  335,    0,    0,  335,  336,  336,    0,
  335,  335,  335,  335,  335,    0,  335,  335,  335,  335,
  335,  335,  335,    0,  335,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  335,  168,  169,  170,    0,
  171,  172,  173,    0,  174,    0,  335,  335,  335,  335,
   56,  456,    0,  335,    0,  402,    0,    0,  176,    0,
    0,    0,    0,    0,    0,    0,  177,    0,    0,  336,
  336,  336,    0,  336,  336,    0,    0,    0,  336,    0,
  336,   57,    0,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,   95,  336,  336,    0,  336,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  336,
    0,    0,  336,   55,  337,  337,  336,  336,  336,  336,
  336,    0,  336,  336,  336,  336,  336,  336,  336,    0,
  336,    0,    0,    0,  168,  169,  170,    0,  171,  172,
  173,  336,  174,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  336,  336,  336,  336,  176,    0,    0,  336,
    0,    0,    0,    0,  177,  470,  471,  472,  473,  474,
  475,  476,  477,  478,  479,    0,  337,  337,  337,    0,
  337,  337,    0,    0,    0,  337,    0,  337,    0,    0,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
   95,  337,  337,    0,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,    0,    0,  337,
  372,  372,    0,  337,  337,  337,  337,  337,    0,  337,
  337,  337,  337,  337,  337,  337,    0,  337,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  337,    0,
    0,    0,    0,    0,  198,    0,    0,    0,    0,  337,
  337,  337,  337,   23,    0,    0,  337,    0,    0,    0,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,  372,  372,  372,    0,  372,  372,    0,    0,
    0,  372,    0,  372,    0,    0,  372,  372,  372,  372,
  372,  372,  372,  372,  372,  372,   95,  372,  372,    0,
  372,  372,  372,  372,  372,  372,  372,  372,  372,  372,
  372,  372,  372,    0,    0,  372,  364,  364,    0,  372,
  372,  372,  372,  372,    0,  372,  372,  372,  372,  372,
  372,  372,    0,  372,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  372,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  372,  372,  372,  372,  671,
   76,   77,  372,    0,   78,   79,   80,   30,   31,   32,
   33,   34,   35,   36,   37,    0,    0,    0,  364,  364,
  364,   43,  364,  364,    0,    0,    0,  364,    0,  364,
   81,    0,  364,  364,  364,  364,  364,  364,  364,  364,
  364,  364,   95,  364,  364,    0,  364,  364,  364,  364,
  364,  364,  364,  364,  364,  364,  364,  364,  364,    0,
    0,  364,    0,  356,  356,  364,  364,  364,  364,  364,
    0,  364,  364,  364,  364,  364,  364,  364,    0,  364,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  364,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  364,  364,  364,  364,    0,    0,    0,  364,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  356,  356,  356,    0,  356,
  356,    0,    0,  220,  356,   27,  356,    0,    0,  356,
  356,  356,  356,  356,  356,  356,  356,  356,  356,  221,
  356,  356,    0,  356,  356,  356,  356,  356,  356,  356,
  356,  356,  356,  356,  356,  356,    0,    0,  356,  345,
  345,    0,  356,  356,  356,  356,  356,    0,  356,  356,
  356,  356,  356,  356,  356,    0,  356,    0,    0,    0,
    0,  222,  223,  224,    0,    0,    0,  356,  225,  226,
    0,  227,  228,  229,  230,    0,    0,    0,  356,  356,
  356,  356,    0,    0,    0,  356,    0,    0,    0,    0,
    0,   95,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  345,  345,  345,    0,  345,  345,    0,    0,    0,
  345,   95,  345,    0,    0,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,    0,  345,  345,    0,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,   95,    0,  345,  310,  310,  183,  345,  345,
  345,  345,  345,    0,  345,  345,  345,  345,  345,  345,
  345,    0,  345,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  345,   95,    0,    0,    0,  184,    0,
    0,    0,    0,    0,  345,  345,  345,  345,    0,   56,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  310,  310,  310,
  182,  310,  310,    0,    0,    0,  310,    0,  310,    0,
   57,  310,  310,  310,  310,  310,  310,  310,  310,  310,
  310,    0,  310,  310,    0,  310,  310,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,  310,    0,    0,
  310,    0,   55,    0,  310,  310,  310,  310,  310,    0,
  310,  310,  310,  310,  310,  310,  310,    0,  310,    0,
    0,   56,    0,   27,   27,    0,    0,   27,   27,  310,
   27,    0,  697,    0,    0,    0,    0,    0,   56,    0,
  310,  310,  310,  310,   27,   27,    0,  310,    0,    0,
    0,    0,   57,    0,    0,    0,    0,    0,   95,   95,
   95,    0,   95,   95,   95,    0,   95,  728,  729,   57,
    0,    0,  133,   95,   95,    0,    0,    0,    0,   27,
   95,    0,    0,    0,   55,    0,   56,    0,   95,    0,
    0,    0,    0,    0,  168,  169,  170,    0,  171,  172,
  173,   55,  174,  133,    0,    0,    0,    0,    0,  274,
  175,    0,    0,    0,    0,    0,  176,   57,    0,    0,
    0,    0,    0,    0,  177,    0,    0,    0,  775,  776,
  105,    0,  779,  780,    0,  133,    0,    0,  134,    0,
    0,    0,    0,   22,   39,   40,    0,   41,   42,   55,
    0,   44,   23,   45,   46,   47,   48,   49,    0,   50,
  804,   24,   25,   26,   27,   28,    0,    0,    0,  134,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   95,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   56,
    0,    0,    0,   95,   95,   95,    0,    0,    0,    0,
    0,  134,    0,    0,    0,    0,    0,  829,    0,    0,
    0,  833,  834,    0,    0,  178,   53,    0,    0,    0,
   57,    0,    0,    0,   95,   22,    0,    0,    0,  179,
  180,  181,    0,    0,   23,    0,    0,    0,    0,    0,
    0,  430,   22,   24,   25,   26,   27,   28,    0,    0,
    0,   23,   55,    0,  105,   56,    0,    0,    0,    0,
   24,   25,   26,   27,   28,  871,   54,   38,   39,   40,
    0,   41,   42,    0,    0,   44,  133,   45,   46,   47,
   48,   49,    0,   50,    0,  133,   57,  450,    0,    0,
   22,    0,    0,    0,  133,  133,  133,  133,  133,   23,
    0,  103,    0,    0,    0,  133,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,   55,  133,
  133,  183,  133,  133,    0,    0,  133,    0,  133,  133,
  133,  133,  133,  133,  133,    0,    0,    0,    0,    0,
   53,    0,  134,    0,  183,    0,    0,    0,    0,    0,
    0,  134,  184,    0,    0,    0,    0,    0,    0,    0,
  134,  134,  134,  134,  134,    0,    0,    0,    0,  102,
    0,  134,    0,  417,  418,  184,    0,    0,    0,    0,
    0,    0,    0,    0,  182,  134,  134,  183,  134,  134,
   54,  133,  134,   22,  134,  134,  134,  134,  134,  134,
  134,    0,   23,    0,    0,    0,  102,  182,  245,    0,
    0,   24,   25,   26,   27,   28,    0,    0,  184,    0,
    0,    0,   66,    0,  183,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   56,    0,   39,   40,    0,   41,
   42,  133,    0,   44,    0,   45,   46,   47,   48,   49,
  182,   50,    0,    0,    0,  184,    0,  134,    0,   22,
    0,    0,    0,    0,    0,   57,    0,    0,   23,    0,
    0,    0,  102,    0,  362,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,  182,  105,    0,
  183,    0,    0,    0,    0,    0,    0,   55,    0,    0,
    0,    0,   39,   40,    0,   41,   42,  134,   53,   44,
    0,   45,   46,   47,   48,   49,    0,   50,  168,  169,
  170,  184,  171,  172,  173,    0,  174,    0,   56,    0,
    0,    0,    0,  274,  175,    0,    0,    0,    0,    0,
  176,  168,  169,  170,    0,  171,  172,  173,  177,  174,
    0,  143,    0,  182,    0,    0,  274,  175,   54,   57,
    0,    0,    0,  176,    0,   56,    0,    0,    0,    0,
   56,  177,    0,    0,   53,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  168,  169,  170,    0,  171,  172,
  173,   55,  174,    0,    0,    0,   57,    0,    0,  274,
  175,   57,    0,    0,    0,    0,  176,    0,    0,    0,
    0,    0,    0,    0,  177,    0,    0,  505,    0,    0,
    0,  168,  169,  170,   54,  171,  172,  173,   55,  174,
    0,    0,    0,   55,    0,    0,    0,  175,   22,  178,
    0,    0,    0,  176,    0,    0,    0,   23,    0,    0,
    0,  177,    0,  179,  180,  181,   24,   25,   26,   27,
   28,    0,  178,    0,  723,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  179,  180,  181,    0,
    0,    0,    0,   56,    0,    0,    0,  168,  169,  170,
    0,  171,  172,  173,    0,  174,    0,    0,    0,    0,
    0,    0,    0,  175,    0,  178,    0,    0,    0,  176,
    0,    0,    0,    0,   57,    0,    0,  177,    0,  179,
  180,  181,    0,  612,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,  437,    0,    0,    0,
    0,   23,  178,    0,    0,    0,   55,    0,    0,    0,
   24,   25,   26,   27,   28,    0,  179,  180,  181,    0,
    0,    0,    0,    0,    0,  295,    0,    0,    0,  198,
    0,    0,    0,    0,   22,    0,    0,    0,   23,    0,
    0,    0,    0,   23,    0,    0,    0,   24,   25,   26,
   27,   28,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,    0,    0,  142,    0,    0,    0,  178,    0,
    0,    0,    0,    0,  452,    0,    0,    0,    0,    0,
    0,    0,  179,  180,  181,    0,  247,  296,  297,  298,
  439,  299,  300,    0,    0,    0,  301,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
  312,    0,  313,  314,    0,  315,  316,  317,  318,  319,
  320,  321,  322,  323,  324,  325,  326,  327,    0,    0,
  328,  295,    0,    0,  329,  330,  331,  332,  333,    0,
  334,  335,  336,  337,  338,  339,  340,   22,  341,    0,
    0,    0,    0,    0,    0,    0,   23,    0,    0,  342,
    0,  722,    0,    0,    0,   24,   25,   26,   27,   28,
  343,  344,  345,  346,    0,    0,    0,  347,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  245,
    0,    0,    0,  296,  297,  298,    0,  299,  300,    0,
    0,    0,  301,    0,  302,    0,    0,  303,  304,  305,
  306,  307,  308,  309,  310,  311,  312,    0,  313,  314,
    0,  315,  316,  317,  318,  319,  320,  321,  322,  323,
  324,  325,  326,  327,  247,    0,  328,    0,    0,    0,
  329,  330,  331,  332,  333,    0,  334,  335,  336,  337,
  338,  339,  340,    0,  341,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  342,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   56,  343,  344,  345,  346,
    0,    0,    0,  347,    0,    0,    0,    0,    0,    0,
    0,    0,  248,    0,    0,    0,  247,  247,  247,    0,
  247,  247,    0,    0,    0,  247,   57,  247,    0,    0,
  247,  247,  247,  247,  247,  247,  247,  247,  247,  247,
    0,  247,  247,    0,  247,  247,  247,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,  245,   55,  247,
    0,    0,    0,  247,  247,  247,  247,  247,    0,  247,
  247,  247,  247,  247,  247,  247,    0,  247,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  247,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   56,  247,
  247,  247,  247,    0,    0,    0,  247,    0,    0,    0,
    0,    0,    0,    0,    0,  246,    0,    0,    0,  245,
  245,  245,    0,  245,  245,    0,    0,    0,  245,   57,
  245,    0,    0,  245,  245,  245,  245,  245,  245,  245,
  245,  245,  245,    0,  245,  245,    0,  245,  245,  245,
  245,  245,  245,  245,  245,  245,  245,  245,  245,  245,
  248,   55,  245,    0,    0,    0,  245,  245,  245,  245,
  245,    0,  245,  245,  245,  245,  245,  245,  245,    0,
  245,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  245,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,  245,  245,  245,  245,    0,    0,   23,  245,
    0,    0,    0,  154,    0,    0,    0,   24,   25,   26,
   27,   28,  248,  248,  248,    0,  248,  248,    0,  155,
    0,  248,    0,  248,    0,   56,  248,  248,  248,  248,
  248,  248,  248,  248,  248,  248,    0,  248,  248,    0,
  248,  248,  248,  248,  248,  248,  248,  248,  248,  248,
  248,  248,  248,  246,    0,  248,   57,    0,   56,  248,
  248,  248,  248,  248,    0,  248,  248,  248,  248,  248,
  248,  248,    0,  248,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  248,    0,    0,    0,   55,   57,
    0,    0,   22,    0,   56,  248,  248,  248,  248,    0,
    0,   23,  248,    0,    0,    0,    0,    0,    0,    0,
   24,   25,   26,   27,   28,  246,  246,  246,    0,  246,
  246,   55,  142,    0,  246,   57,  246,    0,    0,  246,
  246,  246,  246,  246,  246,  246,  246,  246,  246,    0,
  246,  246,    0,  246,  246,  246,  246,  246,  246,  246,
  246,  246,  246,  246,  246,  246,    0,   55,  246,  201,
    0,    0,  246,  246,  246,  246,  246,    0,  246,  246,
  246,  246,  246,  246,  246,    0,  246,   56,    0,    0,
    0,    0,   56,    0,    0,    0,    0,  246,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  246,  246,
  246,  246,    0,    0,    0,  246,    0,    0,   57,    0,
    0,    0,   56,   57,    0,    0,    0,    0,    0,    0,
  481,  482,  168,  169,  170,  380,  171,  172,  173,   22,
  381,    0,    0,    0,    0,    0,    0,   56,   23,  382,
   55,  383,    0,   57,  176,   55,    0,   24,   25,   26,
   27,   28,  177,    0,    0,  168,  169,  170,  380,  171,
  172,  173,   22,  381,   56,    0,    0,    0,   57,    0,
   56,   23,  391,    0,  383,   55,    0,  176,   56,    0,
   24,   25,   26,   27,   28,  177,    0,    0,    0,    0,
    0,    0,    0,   56,    0,   57,    0,    0,  198,  199,
   55,   57,   93,  842,    0,    0,    0,   23,  200,   57,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,   56,    0,    0,   57,    0,   56,   55,    0,   93,
    0,    0,    0,   55,    0,    0,    0,    0,    0,    0,
    0,   55,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   57,    0,    0,    0,   55,   57,  483,  484,
  485,  486,    0,    0,    0,    0,    0,  487,  488,  489,
  490,  491,  492,  493,  494,  495,  496,    0,    0,    0,
    0,  198,  199,    0,   55,    0,  198,  397,    0,  256,
   23,  200,    0,    0,    0,   23,  398,    0,    0,   24,
   25,   26,   27,   28,   24,   25,   26,   27,   28,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,    0,   23,  795,    0,    0,    0,
    0,    0,    0,    0,   24,   25,   26,   27,   28,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,    0,    0,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,  198,    0,
    0,    0,    0,    0,   22,    0,    0,   23,    0,    0,
    0,    0,   22,   23,    0,    0,   24,   25,   26,   27,
   28,   23,   24,   25,   26,   27,   28,   22,    0,    0,
   24,   25,   26,   27,   28,    0,   23,    0,    0,    0,
    0,  722,  355,  355,    0,   24,   25,   26,   27,   28,
  290,    0,    0,    0,    0,  198,    0,    0,    0,    0,
  198,    0,    0,    0,   23,    0,    0,    0,    0,   23,
  105,    0,    0,   24,   25,   26,   27,   28,   24,   25,
   26,   27,   28,    0,   39,   40,    0,   41,   42,    0,
    0,   44,    0,   45,   46,   47,   48,   49,    0,   50,
    0,    0,    0,    0,  355,  355,  355,    0,  355,  355,
    0,    0,    0,  355,    0,  355,    0,    0,  355,  355,
  355,  355,  355,  355,  355,  355,  355,  355,    0,  355,
  355,    0,  355,  355,  355,  355,  355,  355,  355,  355,
  355,  355,  355,  355,  355,  329,  329,  355,    0,    0,
    0,  355,  355,  355,  355,  355,   53,  355,  355,  355,
  355,  355,  355,  355,    0,  355,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  355,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  355,  355,  355,
  355,    0,    0,    0,  355,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   54,  329,  329,  329,
    0,  329,  329,    0,    0,    0,  329,    0,  329,    0,
    0,  329,  329,  329,  329,  329,  329,  329,  329,  329,
  329,    0,  329,  329,    0,  329,  329,  329,  329,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  311,  311,
  329,    0,    0,    0,  329,  329,  329,  329,  329,    0,
  329,  329,  329,  329,  329,  329,  329,    0,  329,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  329,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  329,  329,  329,  329,    0,    0,    0,  329,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  311,  311,  311,    0,  311,  311,    0,    0,    0,  311,
    0,  311,    0,    0,  311,  311,  311,  311,  311,  311,
  311,  311,  311,  311,    0,  311,  311,    0,  311,  311,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  311,  316,  316,  311,    0,    0,    0,  311,  311,  311,
  311,  311,    0,  311,  311,  311,  311,  311,  311,  311,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  311,  311,  311,  311,    0,    0,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  316,  316,  316,    0,  316,  316,    0,
    0,    0,  316,    0,  316,    0,    0,  316,  316,  316,
  316,  316,  316,  316,  316,  316,  316,    0,  316,  316,
    0,  316,  316,  316,  316,  316,  316,  316,  316,  316,
  316,  316,  316,  316,  317,  317,  316,    0,    0,    0,
  316,  316,  316,  316,  316,    0,  316,  316,  316,  316,
  316,  316,  316,    0,  316,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  316,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  316,  316,  316,  316,
    0,    0,    0,  316,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  317,  317,  317,    0,
  317,  317,    0,    0,    0,  317,    0,  317,    0,    0,
  317,  317,  317,  317,  317,  317,  317,  317,  317,  317,
    0,  317,  317,    0,  317,  317,  317,  317,  317,  317,
  317,  317,  317,  317,  317,  317,  317,  312,  312,  317,
    0,    0,    0,  317,  317,  317,  317,  317,    0,  317,
  317,  317,  317,  317,  317,  317,    0,  317,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  317,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  317,
  317,  317,  317,    0,    0,    0,  317,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  312,  312,    0,  312,  312,    0,    0,    0,  312,    0,
  312,    0,    0,  312,  312,  312,  312,  312,  312,  312,
  312,  312,  312,    0,  312,  312,    0,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  312,  312,  312,  312,
  322,  322,  312,    0,    0,    0,  312,  312,  312,  312,
  312,    0,  312,  312,  312,  312,  312,  312,  312,    0,
  312,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  312,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,  312,  312,  312,    0,    0,    0,  312,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  322,  322,  322,    0,  322,  322,    0,    0,
    0,  322,    0,  322,    0,    0,  322,  322,  322,  322,
  322,  322,  322,  322,  322,  322,    0,  322,  322,    0,
  322,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  322,  344,  344,  322,    0,    0,    0,  322,
  322,  322,  322,  322,    0,  322,  322,  322,  322,  322,
  322,  322,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  322,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  322,  322,  322,  322,    0,
    0,    0,  322,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  344,  344,  344,    0,  344,
  344,    0,    0,    0,  344,    0,  344,    0,    0,  344,
  344,  344,  344,  344,  344,  344,  344,  344,  344,    0,
  344,  344,    0,  344,  344,  344,  344,  344,  344,  344,
  344,  344,  344,  344,  344,  344,  340,  340,  344,    0,
    0,    0,  344,  344,  344,  344,  344,    0,  344,  344,
  344,  344,  344,  344,  344,    0,  344,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  344,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  344,  344,
  344,  344,    0,    0,    0,  344,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  340,  340,
  340,    0,  340,  340,    0,    0,    0,  340,    0,  340,
    0,    0,  340,  340,  340,  340,  340,  340,  340,  340,
  340,  340,    0,  340,  340,    0,  340,  340,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,  340,  318,
  318,  340,    0,    0,    0,  340,  340,  340,  340,  340,
    0,  340,  340,  340,  340,  340,  340,  340,    0,  340,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  340,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  340,  340,  340,  340,    0,    0,    0,  340,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  318,  318,  318,    0,  318,  318,    0,    0,    0,
  318,    0,  318,    0,    0,  318,  318,  318,  318,  318,
  318,  318,  318,  318,  318,    0,  318,  318,    0,  318,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  318,
  318,  318,  313,  313,  318,    0,    0,    0,  318,  318,
  318,  318,  318,    0,  318,  318,  318,  318,  318,  318,
  318,    0,  318,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  318,  318,  318,  318,    0,    0,
    0,  318,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  313,  313,  313,    0,  313,  313,
    0,    0,    0,  313,    0,  313,    0,    0,  313,  313,
  313,  313,  313,  313,  313,  313,  313,  313,    0,  313,
  313,    0,  313,  313,  313,  313,  313,  313,  313,  313,
  313,  313,  313,  313,  313,  314,  314,  313,    0,    0,
    0,  313,  313,  313,  313,  313,    0,  313,  313,  313,
  313,  313,  313,  313,    0,  313,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  313,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  313,  313,  313,
  313,    0,    0,    0,  313,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  314,  314,  314,
    0,  314,  314,    0,    0,    0,  314,    0,  314,    0,
    0,  314,  314,  314,  314,  314,  314,  314,  314,  314,
  314,    0,  314,  314,    0,  314,  314,  314,  314,  314,
  314,  314,  314,  314,  314,  314,  314,  314,  319,  319,
  314,    0,    0,    0,  314,  314,  314,  314,  314,    0,
  314,  314,  314,  314,  314,  314,  314,    0,  314,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  314,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  314,  314,  314,  314,    0,    0,    0,  314,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  319,  319,  319,    0,  319,  319,    0,    0,    0,  319,
    0,  319,    0,    0,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,    0,  319,  319,    0,  319,  319,
  319,  319,  319,  319,  319,  319,  319,  319,  319,  319,
  319,  327,  327,  319,    0,    0,    0,  319,  319,  319,
  319,  319,    0,  319,  319,  319,  319,  319,  319,  319,
    0,  319,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  319,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  319,  319,  319,  319,    0,    0,    0,
  319,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  327,  327,  327,    0,  327,  327,    0,
    0,    0,  327,    0,  327,    0,    0,  327,  327,  327,
  327,  327,  327,  327,  327,  327,  327,    0,  327,  327,
    0,  327,  327,  327,  327,  327,  327,  327,  327,  327,
  327,  327,  327,  327,  320,  320,  327,    0,    0,    0,
  327,  327,  327,  327,  327,    0,  327,  327,  327,  327,
  327,  327,  327,    0,  327,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  327,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  327,  327,  327,  327,
    0,    0,    0,  327,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,  320,  320,    0,
  320,  320,    0,    0,    0,  320,    0,  320,    0,    0,
  320,  320,  320,  320,  320,  320,  320,  320,  320,  320,
    0,  320,  320,    0,  320,  320,  320,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,  323,  323,  320,
    0,    0,    0,  320,  320,  320,  320,  320,    0,  320,
  320,  320,  320,  320,  320,  320,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  320,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  320,
  320,  320,  320,    0,    0,    0,  320,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  323,
  323,  323,    0,  323,  323,    0,    0,    0,  323,    0,
  323,    0,    0,  323,  323,  323,  323,  323,  323,  323,
  323,  323,  323,    0,  323,  323,    0,  323,  323,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  323,  323,
  341,  341,  323,    0,    0,    0,  323,  323,  323,  323,
  323,    0,  323,  323,  323,  323,  323,  323,  323,    0,
  323,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  323,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  323,  323,  323,  323,    0,    0,    0,  323,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  341,  341,  341,    0,  341,  341,    0,    0,
    0,  341,    0,  341,    0,    0,  341,  341,  341,  341,
  341,  341,  341,  341,  341,  341,    0,  341,  341,    0,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
  341,  341,  341,  315,  315,  341,    0,    0,    0,  341,
  341,  341,  341,  341,    0,  341,  341,  341,  341,  341,
  341,  341,    0,  341,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  341,  341,  341,  341,    0,
    0,    0,  341,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  315,  315,  315,    0,  315,
  315,    0,    0,    0,  315,    0,  315,    0,    0,  315,
  315,  315,  315,  315,  315,  315,  315,  315,  315,    0,
  315,  315,    0,  315,  315,  315,  315,  315,  315,  315,
  315,  315,  315,  315,  315,  315,  321,  321,  315,    0,
    0,    0,  315,  315,  315,  315,  315,    0,  315,  315,
  315,  315,  315,  315,  315,    0,  315,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  315,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  315,  315,
  315,  315,    0,    0,    0,  315,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  321,  321,
  321,    0,  321,  321,    0,    0,    0,  321,    0,  321,
    0,    0,  321,  321,  321,  321,  321,  321,  321,  321,
  321,  321,    0,  321,  321,    0,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  321,  321,  321,  321,  324,
  324,  321,    0,    0,    0,  321,  321,  321,  321,  321,
    0,  321,  321,  321,  321,  321,  321,  321,    0,  321,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  321,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  321,  321,  321,  321,    0,    0,    0,  321,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  324,  324,  324,    0,  324,  324,    0,    0,    0,
  324,    0,  324,    0,    0,  324,  324,  324,  324,  324,
  324,  324,  324,  324,  324,    0,  324,  324,    0,  324,
  324,  324,  324,  324,  324,  324,  324,  324,  324,  324,
  324,  324,  325,  325,  324,    0,    0,    0,  324,  324,
  324,  324,  324,    0,  324,  324,  324,  324,  324,  324,
  324,    0,  324,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  324,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  324,  324,  324,  324,    0,    0,
    0,  324,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  325,  325,  325,    0,  325,  325,
    0,    0,    0,  325,    0,  325,    0,    0,  325,  325,
  325,  325,  325,  325,  325,  325,  325,  325,    0,  325,
  325,    0,  325,  325,  325,  325,  325,  325,  325,  325,
  325,  325,  325,  325,  325,  326,  326,  325,    0,    0,
    0,  325,  325,  325,  325,  325,    0,  325,  325,  325,
  325,  325,  325,  325,    0,  325,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  325,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  325,  325,  325,
  325,    0,    0,    0,  325,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  326,  326,  326,
    0,  326,  326,    0,    0,    0,  326,    0,  326,    0,
    0,  326,  326,  326,  326,  326,  326,  326,  326,  326,
  326,    0,  326,  326,    0,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  295,    0,
  326,    0,    0,    0,  326,  326,  326,  326,  326,    0,
  326,  326,  326,  326,  326,  326,  326,    0,  326,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  326,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  326,  326,  326,  326,    0,    0,    0,  326,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  296,  297,  298,    0,  299,  300,    0,    0,    0,  301,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  325,  326,
  327,  251,    0,  328,    0,    0,    0,  329,  330,  331,
  332,  333,    0,  334,  335,  336,  337,  338,  339,  340,
    0,  341,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  342,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  343,  344,  345,  346,    0,    0,    0,
  347,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  251,  251,  251,    0,  251,  251,    0,
    0,    0,  251,    0,  251,    0,    0,  251,  251,  251,
  251,  251,  251,  251,  251,  251,  251,    0,  251,  251,
    0,  251,  251,  251,  251,  251,  251,  251,  251,  251,
  251,  251,  251,  251,  252,    0,  251,    0,    0,    0,
  251,  251,  251,  251,  251,    0,  251,  251,  251,  251,
  251,  251,  251,    0,  251,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  251,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  251,  251,  251,  251,
    0,    0,    0,  251,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  252,  252,  252,    0,
  252,  252,    0,    0,    0,  252,    0,  252,    0,    0,
  252,  252,  252,  252,  252,  252,  252,  252,  252,  252,
    0,  252,  252,    0,  252,  252,  252,  252,  252,  252,
  252,  252,  252,  252,  252,  252,  252,  253,    0,  252,
    0,    0,    0,  252,  252,  252,  252,  252,    0,  252,
  252,  252,  252,  252,  252,  252,    0,  252,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  252,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  252,
  252,  252,  252,    0,    0,    0,  252,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  253,
  253,  253,    0,  253,  253,    0,    0,    0,  253,    0,
  253,    0,    0,  253,  253,  253,  253,  253,  253,  253,
  253,  253,  253,    0,  253,  253,    0,  253,  253,  253,
  253,  253,  253,  253,  253,  253,  253,  253,  253,  253,
  254,    0,  253,    0,    0,    0,  253,  253,  253,  253,
  253,    0,  253,  253,  253,  253,  253,  253,  253,    0,
  253,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  253,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  253,  253,  253,  253,    0,    0,    0,  253,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  254,  254,  254,    0,  254,  254,    0,    0,
    0,  254,    0,  254,    0,    0,  254,  254,  254,  254,
  254,  254,  254,  254,  254,  254,    0,  254,  254,    0,
  254,  254,  254,  254,  254,  254,  254,  254,  254,  254,
  254,  254,  254,    0,    0,  254,    0,    0,    0,  254,
  254,  254,  254,  254,    0,  254,  254,  254,  254,  254,
  254,  254,    0,  254,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  254,    0,    0,    0,    0,    0,
    0,    0,    0,  299,    0,  254,  254,  254,  254,    0,
  302,    0,  254,  303,  304,  305,  306,  307,  308,  309,
  310,  311,  312,    0,  313,  314,    0,  315,  316,  317,
  318,  319,  320,  321,  322,  323,  324,  325,  326,  327,
    0,    0,  328,    0,    0,    0,  329,  330,  331,  332,
  333,    0,  334,  335,  336,  337,  338,  339,  340,    0,
  341,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  343,  344,  345,  346,    0,    0,    0,  347,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   59,   42,    6,   33,   33,   44,  531,   42,   40,
  348,   60,  123,  237,  768,   63,  125,   44,   66,  123,
  123,  123,   40,   40,  123,  123,  123,   40,   40,  280,
  111,  785,  123,  116,  276,   61,   40,   42,   44,  281,
   44,  163,  100,   42,    0,  721,   40,   44,   55,  581,
   44,   44,   42,  291,  292,   62,   63,   41,  214,   66,
   44,   44,   69,  219,   44,   62,   69,  821,  149,   61,
  313,  152,   62,   44,  123,   61,   83,  266,  161,   41,
  323,  272,   44,   19,   40,  323,   91,  125,  183,  184,
   41,   98,  313,   44,   93,  217,  103,  286,  125,  106,
   93,  108,  323,  110,   63,  108,   61,  110,  115,  157,
  323,  304,  119,  306,  260,  237,  123,  793,  125,  125,
  313,  125,   60,   82,  264,  265,  266,  283,  379,  136,
  137,  125,  139,  216,  215,  359,  379,  379,    6,    7,
  124,  673,  125,   61,  676,  125,   82,  287,   84,   41,
   86,  260,   44,   91,  125,   42,  910,   44,  379,  166,
  167,  256,  284,  274,  541,    0,  257,  276,   61,  306,
  274,  274,  274,  281,  512,  274,  274,  274,    0,   40,
  116,  188,  406,  190,   41,  123,   82,   44,   84,   85,
   41,  270,  271,   44,   62,   63,  333,  334,   66,  291,
  292,   69,  257,  295,  296,   40,  213,  295,  296,  318,
  319,  320,   41,   42,   40,   41,  325,  326,   44,  328,
  329,  330,  331,   41,  120,  121,   44,   42,  124,  236,
  126,  234,   41,  273,   41,   44,  274,   44,  106,  288,
  108,  299,  110,  250,  384,  274,  274,  274,  277,  256,
   40,  299,    0,  293,  272,  272,  633,  264,  265,  272,
  272,  291,  292,   41,   42,  295,  296,  307,  308,   41,
  310,  311,   44,   42,  314,   44,  316,  317,  318,  319,
  320,  315,  322,   42,  257,   44,  351,  345,  353,   40,
   41,  321,  299,   44,   61,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  315,  316,
  317,  318,  268,  269,  291,  292,  272,  324,  325,  275,
  844,  328,    0,  260,   41,   41,  264,   44,   44,   41,
   41,   42,   44,  289,  290,  273,  343,  344,  345,  379,
  347,   42,  345,   44,  282,  283,  284,  285,  286,  380,
  727,   41,   40,  500,   44,  293,   41,  364,  420,   44,
  260,   42,  424,   44,   40,  427,  123,  415,  324,  307,
  308,  257,  310,  311,  436,   42,  314,   44,  316,  317,
  318,  319,  320,  321,  322,   42,   42,   44,   44,  429,
  267,  531,  426,  533,  267,  535,  306,  544,  545,   41,
   42,   41,   42,   41,   42,  379,   40,  414,  415,  123,
  123,  414,   41,  420,  791,  123,  257,  424,  323,   40,
  427,   40,   40,  430,   44,  802,  803,   58,   58,  436,
  437,  299,  439,  268,  269,  575,   58,  272,  273,   62,
  275,  379,  507,  450,   61,  503,  268,  269,   40,  456,
  272,  273,  257,  275,  289,  290,  603,  604,  274,  606,
  379,  526,   44,   40,  841,  379,   44,  289,  290,   44,
  257,  260,  537,  480,   61,  420,  332,  345,  855,  274,
  274,  858,  313,  257,  133,  379,   44,  257,   41,  324,
  497,  429,  639,   44,  501,  502,  503,  387,  387,  502,
  503,   40,  324,  387,  379,  882,  883,   60,  379,   44,
   40,  124,  273,   44,  521,  522,  523,   44,   44,  896,
  268,  269,   44,   44,  272,  273,  533,  275,   44,   44,
  274,  260,  387,  182,  183,  184,  257,  387,   91,  546,
  387,  289,  290,  546,  387,  203,  414,  415,  197,  614,
  387,  387,  257,  700,  387,  387,   62,  704,  705,  624,
  608,  626,  387,  387,  387,  379,  387,  379,  257,  379,
  123,  257,  579,   44,  581,  266,  324,  584,  585,  586,
  587,  588,  589,  590,  591,  592,  593,  594,  595,   40,
  268,  269,  241,  257,  272,  273,   44,  275,  605,   44,
  607,  608,  251,  252,  253,   44,   44,  256,   44,   44,
   44,  289,  290,   44,  697,   44,   44,  757,  758,   44,
   44,   44,   44,   44,  273,   44,   44,   44,  635,   44,
    0,   44,   44,  501,  502,  503,  783,   91,  387,   44,
   44,  257,    0,  257,  709,   44,  324,  296,   44,  298,
   44,  300,  774,  718,   44,   44,  796,   44,   44,   44,
   44,  257,   44,   44,   44,   44,  673,   44,   44,  676,
  319,  320,  321,  322,  323,   44,  724,  326,  546,  424,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  775,  776,   91,   44,  779,  780,   44,  706,
  387,   44,  379,  706,  844,  827,  828,  387,  379,  831,
  832,  264,   44,  371,  721,  722,  379,   44,   44,  260,
  273,  370,   44,   40,  327,  278,  387,   41,  257,  282,
  283,  284,  285,  286,  273,  274,  379,  605,  257,  607,
  608,  294,  379,  257,   44,  867,  829,  869,  870,   93,
  833,  834,  410,   44,  380,   40,   40,   40,  327,  327,
  257,  419,  257,  327,  422,  423,  888,  425,  426,  260,
  428,  429,   40,  431,  432,  433,  434,  435,  387,  387,
  438,  387,  440,  441,  442,  443,  793,   44,  871,    0,
  272,   42,    0,  800,  797,  272,  335,  336,  337,  272,
  339,  340,   40,  452,  272,  344,  272,  346,   10,   60,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   87,  360,  361,  197,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  196,  706,  378,
   91,  499,   19,  382,  383,  384,  385,  386,  125,  388,
  389,  390,  391,  392,  393,  394,  265,  396,  268,  533,
    0,   98,  213,  231,  357,   59,  757,  839,  407,  236,
  758,  520,  123,  407,  350,  350,  800,  793,  695,  418,
  419,  420,  421,  890,  535,   -1,  425,  407,   -1,   -1,
  548,  898,  899,  900,  552,   60,    0,  555,  268,  269,
  558,   -1,  272,  273,   -1,  275,  564,  565,   -1,  567,
  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,  289,
  290,   -1,   -1,  572,  573,  574,   91,  576,  577,   -1,
   -1,  289,  290,   -1,  583,   -1,   -1,   -1,  596,  597,
  598,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   60,  265,  125,   -1,  324,   -1,   -1,   -1,  123,   -1,
   -1,  610,  276,   -1,   -1,  279,  324,   -1,  617,   -1,
   -1,   -1,   -1,  287,   -1,   -1,   -1,    0,   -1,   -1,
   -1,   91,  640,  260,  642,  643,   -1,  645,  646,   -1,
  648,  649,   -1,  651,  652,  653,  654,  655,   -1,  276,
  658,   -1,  660,  661,  662,  663,  257,  258,  259,   -1,
  261,  262,  263,  123,  265,   -1,  674,   40,   41,   42,
   -1,   44,  273,   -1,   -1,   -1,   -1,   -1,  279,  678,
   -1,   -1,   -1,   -1,   -1,   -1,  287,   60,   -1,   62,
   -1,  318,  319,  320,   -1,   -1,   -1,  696,  325,  326,
   -1,  328,  329,  330,  331,   -1,   -1,   -1,   -1,   -1,
  268,  269,   -1,   -1,  272,  273,  724,  275,   91,   -1,
   93,   -1,   -1,   -1,  732,   -1,   -1,  735,   -1,   -1,
  738,  289,  290,  741,   -1,   -1,   -1,  260,   -1,  747,
  748,   -1,  750,   -1,   -1,   -1,   -1,   -1,   -1,  264,
  123,   -1,  125,  276,   -1,   -1,  755,  756,  273,   -1,
   -1,  769,  770,  771,   -1,   -1,  324,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,  378,  268,  269,
   -1,   -1,  272,  273,   -1,  275,   -1,  786,   -1,  797,
   -1,  392,  393,  394,   -1,  318,  319,  320,   -1,  289,
  290,   -1,  325,  326,  264,  328,  329,  330,  331,   -1,
   -1,   -1,   42,  273,  268,  269,   -1,   -1,  272,  273,
   -1,  275,  282,  283,  284,  285,  286,  826,   -1,   -1,
   60,   -1,   -1,  293,  324,  289,  290,   -1,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,  308,   -1,
  310,  311,  312,    0,  314,   -1,  316,  317,  318,  319,
  320,   91,  322,   -1,    0,   -1,  381,   -1,   -1,   -1,
  324,   -1,   -1,  333,  334,   -1,   -1,   40,   -1,  878,
  879,  880,   -1,    0,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,  123,   -1,  268,  269,   60,   -1,  272,
  273,  274,  275,  902,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  287,   -1,  289,  290,   -1,  379,
  293,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,
   -1,  314,  315,  316,  317,  318,  319,  320,   -1,  322,
   -1,  324,   -1,   40,   41,   -1,   -1,   44,   -1,   -1,
  123,   -1,  335,  336,  337,    0,  339,  340,   -1,  429,
   -1,  344,   -1,  346,   42,   62,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,   -1,   -1,  378,   93,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  407,  265,  123,   -1,  125,   -1,
  122,   -1,  272,  273,   -1,  418,  419,  420,  421,  279,
  423,   -1,  425,  426,   -1,   -1,  429,  287,   -1,   40,
   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,    0,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,  165,  314,   -1,  316,  317,  318,  319,
  320,   -1,  322,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,  187,   -1,  189,   -1,  272,
  273,   -1,   -1,  268,  269,   -1,  279,  272,  273,   -1,
  275,  268,  269,   -1,  287,  272,  273,   -1,  275,   -1,
   -1,   -1,  268,  269,  289,  290,  272,  273,   -1,  275,
   -1,   -1,  289,  290,   -1,   -1,   -1,   -1,  378,  379,
   -1,  268,  269,  289,  290,  272,  273,   -1,  275,   -1,
   -1,   -1,  392,  393,  394,   40,  248,   -1,   -1,  324,
   -1,   -1,  289,  290,   -1,   -1,   -1,  324,    0,  261,
   -1,  263,   -1,   -1,   -1,   -1,  273,  274,  324,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,  429,
   -1,   -1,   -1,   -1,  272,  273,   -1,  324,   -1,   -1,
   -1,  279,   42,   -1,   -1,  378,   -1,   -1,   -1,  287,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,
  393,  394,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,  289,  290,   -1,  344,   -1,  346,
  423,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   40,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  324,
   -1,  378,  273,  274,   -1,  382,  383,  384,  385,  386,
  387,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
  378,   -1,   -1,   41,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,  392,  393,  394,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
  289,  290,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   40,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  324,   -1,  378,  273,  274,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,  418,  419,  420,
  421,   60,   -1,   -1,  425,   -1,  268,  269,   -1,  279,
  272,  273,   -1,  275,   -1,   -1,   -1,  287,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,  289,  290,  344,
   -1,  346,   91,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   40,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,   -1,  324,  378,  123,  273,  274,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,  407,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,  276,   -1,
  425,  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   40,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,   -1,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,  273,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   40,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   -1,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,  418,  419,  420,  421,
   60,  380,   -1,  425,   -1,  276,   -1,   -1,  279,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   91,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   40,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,   -1,  378,  123,  273,  274,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  407,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,  279,   -1,   -1,  425,
   -1,   -1,   -1,   -1,  287,  397,  398,  399,  400,  401,
  402,  403,  404,  405,  406,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   40,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   -1,   -1,  378,
  273,  274,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,  273,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   40,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,   -1,   -1,  378,  273,  274,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,  379,
  291,  292,  425,   -1,  295,  296,  297,  298,  299,  300,
  301,  302,  303,  304,  305,   -1,   -1,   -1,  335,  336,
  337,  312,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
  321,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   40,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,   -1,
   -1,  378,   -1,  273,  274,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,  260,  344,    0,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  276,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,   -1,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,  318,  319,  320,   -1,   -1,   -1,  407,  325,  326,
   -1,  328,  329,  330,  331,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   60,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   91,   -1,  378,  273,  274,   60,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,  123,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   60,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
  123,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   91,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,   -1,
  378,   -1,  123,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   60,   -1,  268,  269,   -1,   -1,  272,  273,  407,
  275,   -1,  602,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  418,  419,  420,  421,  289,  290,   -1,  425,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  637,  638,   91,
   -1,   -1,   60,  272,  273,   -1,   -1,   -1,   -1,  324,
  279,   -1,   -1,   -1,  123,   -1,   60,   -1,  287,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  123,  265,   91,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   91,   -1,   -1,
   -1,   -1,   -1,   -1,  287,   -1,   -1,   -1,  698,  699,
  293,   -1,  702,  703,   -1,  123,   -1,   -1,   60,   -1,
   -1,   -1,   -1,  264,  307,  308,   -1,  310,  311,  123,
   -1,  314,  273,  316,  317,  318,  319,  320,   -1,  322,
  730,  282,  283,  284,  285,  286,   -1,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,   -1,   -1,  392,  393,  394,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,  777,   -1,   -1,
   -1,  781,  782,   -1,   -1,  378,  379,   -1,   -1,   -1,
   91,   -1,   -1,   -1,  423,  264,   -1,   -1,   -1,  392,
  393,  394,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,  362,  264,  282,  283,  284,  285,  286,   -1,   -1,
   -1,  273,  123,   -1,  293,   60,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,  835,  429,  306,  307,  308,
   -1,  310,  311,   -1,   -1,  314,  264,  316,  317,  318,
  319,  320,   -1,  322,   -1,  273,   91,  309,   -1,   -1,
  264,   -1,   -1,   -1,  282,  283,  284,  285,  286,  273,
   -1,   40,   -1,   -1,   -1,  293,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,  123,  307,
  308,   60,  310,  311,   -1,   -1,  314,   -1,  316,  317,
  318,  319,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
  379,   -1,  264,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   42,
   -1,  293,   -1,  347,  348,   91,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  307,  308,   60,  310,  311,
  429,  379,  314,  264,  316,  317,  318,  319,  320,  321,
  322,   -1,  273,   -1,   -1,   -1,   42,  123,   44,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   91,   -1,
   -1,   -1,  293,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,   -1,  307,  308,   -1,  310,
  311,  429,   -1,  314,   -1,  316,  317,  318,  319,  320,
  123,  322,   -1,   -1,   -1,   91,   -1,  379,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,  273,   -1,
   -1,   -1,   42,   -1,   44,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,  123,  293,   -1,
   60,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,
   -1,   -1,  307,  308,   -1,  310,  311,  429,  379,  314,
   -1,  316,  317,  318,  319,  320,   -1,  322,  257,  258,
  259,   91,  261,  262,  263,   -1,  265,   -1,   60,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,  257,  258,  259,   -1,  261,  262,  263,  287,  265,
   -1,   41,   -1,  123,   -1,   -1,  272,  273,  429,   91,
   -1,   -1,   -1,  279,   -1,   60,   -1,   -1,   -1,   -1,
   60,  287,   -1,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  123,  265,   -1,   -1,   -1,   91,   -1,   -1,  272,
  273,   91,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  287,   -1,   -1,  125,   -1,   -1,
   -1,  257,  258,  259,  429,  261,  262,  263,  123,  265,
   -1,   -1,   -1,  123,   -1,   -1,   -1,  273,  264,  378,
   -1,   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,   -1,
   -1,  287,   -1,  392,  393,  394,  282,  283,  284,  285,
  286,   -1,  378,   -1,   41,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,  393,  394,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,  378,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,   91,   -1,   -1,  287,   -1,  392,
  393,  394,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,  362,   -1,   -1,   -1,
   -1,  273,  378,   -1,   -1,   -1,  123,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,  392,  393,  394,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  282,  283,  284,
  285,  286,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  294,   -1,   -1,   -1,  378,   -1,
   -1,   -1,   -1,   -1,  309,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  392,  393,  394,   -1,  125,  335,  336,  337,
  362,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,   -1,
  378,  273,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,  264,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  407,
   -1,  278,   -1,   -1,   -1,  282,  283,  284,  285,  286,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   60,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  125,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   91,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  273,  123,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,  418,
  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   91,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  273,  123,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,  273,  425,
   -1,   -1,   -1,  278,   -1,   -1,   -1,  282,  283,  284,
  285,  286,  335,  336,  337,   -1,  339,  340,   -1,  294,
   -1,  344,   -1,  346,   -1,   60,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  273,   -1,  378,   91,   -1,   60,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,  123,   91,
   -1,   -1,  264,   -1,   60,  418,  419,  420,  421,   -1,
   -1,  273,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,  335,  336,  337,   -1,  339,
  340,  123,  294,   -1,  344,   91,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  123,  378,  125,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   60,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   91,   -1,
   -1,   -1,   60,   91,   -1,   -1,   -1,   -1,   -1,   -1,
  261,  262,  257,  258,  259,  260,  261,  262,  263,  264,
  265,   -1,   -1,   -1,   -1,   -1,   -1,   60,  273,  274,
  123,  276,   -1,   91,  279,  123,   -1,  282,  283,  284,
  285,  286,  287,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   60,   -1,   -1,   -1,   91,   -1,
   60,  273,  274,   -1,  276,  123,   -1,  279,   60,   -1,
  282,  283,  284,  285,  286,  287,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   91,   -1,   -1,  264,  265,
  123,   91,  125,   93,   -1,   -1,   -1,  273,  274,   91,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   60,   -1,   -1,   91,   -1,   60,  123,   -1,  125,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,  123,   91,  399,  400,
  401,  402,   -1,   -1,   -1,   -1,   -1,  408,  409,  410,
  411,  412,  413,  414,  415,  416,  417,   -1,   -1,   -1,
   -1,  264,  265,   -1,  123,   -1,  264,  265,   -1,  123,
  273,  274,   -1,   -1,   -1,  273,  274,   -1,   -1,  282,
  283,  284,  285,  286,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,  273,   -1,   -1,
   -1,   -1,  264,  273,   -1,   -1,  282,  283,  284,  285,
  286,  273,  282,  283,  284,  285,  286,  264,   -1,   -1,
  282,  283,  284,  285,  286,   -1,  273,   -1,   -1,   -1,
   -1,  278,  273,  274,   -1,  282,  283,  284,  285,  286,
  273,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,
  293,   -1,   -1,  282,  283,  284,  285,  286,  282,  283,
  284,  285,  286,   -1,  307,  308,   -1,  310,  311,   -1,
   -1,  314,   -1,  316,  317,  318,  319,  320,   -1,  322,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,  274,  378,   -1,   -1,
   -1,  382,  383,  384,  385,  386,  379,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  429,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  273,  274,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  273,  274,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,
  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  273,  274,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  273,  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  273,  274,  378,   -1,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,
   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  273,  274,  378,   -1,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  273,  274,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,  274,  378,   -1,   -1,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  273,  274,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  273,  274,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,
  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  273,  274,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  273,  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  273,  274,  378,   -1,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,
   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  273,  274,  378,   -1,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  273,  274,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,  274,  378,   -1,   -1,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  273,   -1,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  273,   -1,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,
  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  273,   -1,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  273,   -1,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,   -1,   -1,  378,   -1,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  339,   -1,  418,  419,  420,  421,   -1,
  346,   -1,  425,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,   -1,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
  };

#line 1340 "Iril/IR/IR.jay"

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
  public const int SECTION = 280;
  public const int TYPE = 281;
  public const int HALF = 282;
  public const int FLOAT = 283;
  public const int DOUBLE = 284;
  public const int X86_FP80 = 285;
  public const int INTEGER_TYPE = 286;
  public const int ZEROINITIALIZER = 287;
  public const int OPAQUE = 288;
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
  public const int LINKONCE = 300;
  public const int LINKONCE_ODR = 301;
  public const int WEAK = 302;
  public const int WEAK_ODR = 303;
  public const int APPENDING = 304;
  public const int COMMON = 305;
  public const int FASTCC = 306;
  public const int SIGNEXT = 307;
  public const int ZEROEXT = 308;
  public const int VOLATILE = 309;
  public const int RETURNED = 310;
  public const int DEREFERENCEABLE = 311;
  public const int AVAILABLE_EXTERNALLY = 312;
  public const int PERSONALITY = 313;
  public const int SRET = 314;
  public const int CLEANUP = 315;
  public const int NONNULL = 316;
  public const int NOCAPTURE = 317;
  public const int WRITEONLY = 318;
  public const int READONLY = 319;
  public const int READNONE = 320;
  public const int HIDDEN = 321;
  public const int BYVAL = 322;
  public const int ATTRIBUTE_GROUP_REF = 323;
  public const int ATTRIBUTES = 324;
  public const int NORECURSE = 325;
  public const int NOUNWIND = 326;
  public const int UNWIND = 327;
  public const int SPECULATABLE = 328;
  public const int SSP = 329;
  public const int UWTABLE = 330;
  public const int ARGMEMONLY = 331;
  public const int SEQ_CST = 332;
  public const int DSO_LOCAL = 333;
  public const int DSO_PREEMPTABLE = 334;
  public const int RET = 335;
  public const int BR = 336;
  public const int SWITCH = 337;
  public const int INDIRECTBR = 338;
  public const int INVOKE = 339;
  public const int RESUME = 340;
  public const int CATCHSWITCH = 341;
  public const int CATCHRET = 342;
  public const int CLEANUPRET = 343;
  public const int UNREACHABLE = 344;
  public const int FNEG = 345;
  public const int ADD = 346;
  public const int NUW = 347;
  public const int NSW = 348;
  public const int FADD = 349;
  public const int SUB = 350;
  public const int FSUB = 351;
  public const int MUL = 352;
  public const int FMUL = 353;
  public const int UDIV = 354;
  public const int SDIV = 355;
  public const int FDIV = 356;
  public const int UREM = 357;
  public const int SREM = 358;
  public const int FREM = 359;
  public const int SHL = 360;
  public const int LSHR = 361;
  public const int EXACT = 362;
  public const int ASHR = 363;
  public const int AND = 364;
  public const int OR = 365;
  public const int XOR = 366;
  public const int EXTRACTELEMENT = 367;
  public const int INSERTELEMENT = 368;
  public const int SHUFFLEVECTOR = 369;
  public const int EXTRACTVALUE = 370;
  public const int INSERTVALUE = 371;
  public const int ALLOCA = 372;
  public const int LOAD = 373;
  public const int STORE = 374;
  public const int FENCE = 375;
  public const int CMPXCHG = 376;
  public const int ATOMICRMW = 377;
  public const int GETELEMENTPTR = 378;
  public const int ALIGN = 379;
  public const int INBOUNDS = 380;
  public const int INRANGE = 381;
  public const int TRUNC = 382;
  public const int ZEXT = 383;
  public const int SEXT = 384;
  public const int FPTRUNC = 385;
  public const int FPEXT = 386;
  public const int TO = 387;
  public const int FPTOUI = 388;
  public const int FPTOSI = 389;
  public const int UITOFP = 390;
  public const int SITOFP = 391;
  public const int PTRTOINT = 392;
  public const int INTTOPTR = 393;
  public const int BITCAST = 394;
  public const int ADDRSPACECAST = 395;
  public const int ICMP = 396;
  public const int EQ = 397;
  public const int NE = 398;
  public const int UGT = 399;
  public const int UGE = 400;
  public const int ULT = 401;
  public const int ULE = 402;
  public const int SGT = 403;
  public const int SGE = 404;
  public const int SLT = 405;
  public const int SLE = 406;
  public const int FCMP = 407;
  public const int OEQ = 408;
  public const int OGT = 409;
  public const int OGE = 410;
  public const int OLT = 411;
  public const int OLE = 412;
  public const int ONE = 413;
  public const int ORD = 414;
  public const int UEQ = 415;
  public const int UNE = 416;
  public const int UNO = 417;
  public const int PHI = 418;
  public const int SELECT = 419;
  public const int CALL = 420;
  public const int TAIL = 421;
  public const int VA_ARG = 422;
  public const int ASM = 423;
  public const int SIDEEFFECT = 424;
  public const int LANDINGPAD = 425;
  public const int CATCH = 426;
  public const int CATCHPAD = 427;
  public const int CLEANUPPAD = 428;
  public const int NOUNDEF = 429;
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
