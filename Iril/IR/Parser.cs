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
//t    "declare_head : DECLARE",
//t    "declare_head : DECLARE metadata_kvs",
//t    "function_declaration : declare_head return_type GLOBAL_SYMBOL parameters",
//t    "function_declaration : declare_head return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : declare_head NOALIAS return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : declare_head parameter_attributes return_type GLOBAL_SYMBOL parameters declare_tail",
//t    "function_declaration : declare_head NOALIAS parameter_attributes return_type GLOBAL_SYMBOL parameters declare_tail",
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
//t    "parameter_attribute : IMMARG",
//t    "parameter_attribute : READONLY",
//t    "parameter_attribute : WRITEONLY",
//t    "parameter_attribute : READNONE",
//t    "parameter_attribute : SIGNEXT",
//t    "parameter_attribute : ZEROEXT",
//t    "parameter_attribute : RETURNED",
//t    "parameter_attribute : SRET",
//t    "parameter_attribute : SRET '(' type ')'",
//t    "parameter_attribute : NOALIAS",
//t    "parameter_attribute : BYVAL",
//t    "parameter_attribute : BYVAL '(' type ')'",
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
//t    "blocks : lblock",
//t    "blocks : blocks lblock",
//t    "lblock : INTEGER ':' block",
//t    "lblock : block",
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
//t    "function_arg : METADATA type metadata_value",
//t    "function_arg : METADATA META_SYMBOL",
//t    "function_arg : METADATA META_SYMBOL '(' ')'",
//t    "function_arg : METADATA META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_value : constant",
//t    "metadata_value : GLOBAL_SYMBOL",
//t    "metadata_value : LOCAL_SYMBOL",
//t    "metadata_value : SYMBOL",
//t    "metadata_value : INTTOPTR '(' typed_value TO type ')'",
//t    "metadata_value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "metadata_value : BITCAST '(' typed_value TO type ')'",
//t    "metadata_value : PTRTOINT '(' typed_value TO type ')'",
//t    "metadata_value_args : metadata_value_arg",
//t    "metadata_value_args : metadata_value_args ',' metadata_value_arg",
//t    "metadata_value_arg : constant",
//t    "metadata_value_arg : SYMBOL",
//t    "metadata_value_arg : type LOCAL_SYMBOL",
//t    "metadata_value_arg : type UNDEF",
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
//t    "instruction : CALL return_type inline_assembly function_args attribute_group_refs",
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
    "LANDINGPAD","CATCH","CATCHPAD","CLEANUPPAD","NOUNDEF","IMMARG",
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
case 140:
#line 495 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 499 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 503 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 143:
#line 507 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 144:
#line 511 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 148:
#line 521 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 149:
#line 522 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 150:
#line 529 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 533 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 540 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 544 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 548 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 552 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-2+yyTop]);
    }
  break;
case 156:
#line 556 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 157:
#line 560 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 159:
#line 568 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 572 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 161:
#line 573 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 162:
#line 574 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoUndef; }
  break;
case 163:
#line 575 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ImmediateArgument; }
  break;
case 164:
#line 576 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 165:
#line 577 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 166:
#line 578 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 167:
#line 579 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 168:
#line 580 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 169:
#line 581 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 170:
#line 582 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 171:
#line 583 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 172:
#line 584 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 173:
#line 585 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 174:
#line 586 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 175:
#line 590 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 176:
#line 594 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 184:
#line 617 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 185:
#line 618 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 186:
#line 619 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 187:
#line 620 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 188:
#line 621 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 189:
#line 622 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 190:
#line 623 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 191:
#line 624 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 192:
#line 625 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 193:
#line 626 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 194:
#line 630 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 195:
#line 631 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 196:
#line 632 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 197:
#line 633 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 198:
#line 634 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 199:
#line 635 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 200:
#line 636 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 201:
#line 637 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 202:
#line 638 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 203:
#line 639 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 204:
#line 640 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 205:
#line 641 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 206:
#line 642 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 207:
#line 643 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 208:
#line 644 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 209:
#line 645 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 210:
#line 649 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 214:
#line 659 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 215:
#line 663 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 216:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 671 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 675 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 219:
#line 679 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 220:
#line 683 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 221:
#line 687 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 222:
#line 691 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 223:
#line 695 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 225:
#line 703 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 226:
#line 704 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 227:
#line 705 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 228:
#line 706 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 229:
#line 707 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 230:
#line 708 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 231:
#line 709 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 232:
#line 710 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 233:
#line 711 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 234:
#line 718 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 725 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 729 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 237:
#line 736 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 743 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 747 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 754 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 762 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 243:
#line 769 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 773 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 784 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 788 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 795 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 799 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 806 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 251:
#line 810 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 252:
#line 817 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 821 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 254:
#line 825 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 829 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 256:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 847 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 851 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 855 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 859 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 867 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 264:
#line 868 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 265:
#line 875 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 879 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 886 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 268:
#line 890 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 269:
#line 894 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 270:
#line 898 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 271:
#line 902 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 272:
#line 906 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 274:
#line 914 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = new SymbolValue ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 278:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 279:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 280:
#line 938 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 287:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 961 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 967 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 290:
#line 974 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 978 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 985 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1003 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 299:
#line 1010 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1017 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1021 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1028 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1032 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1036 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1040 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1044 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 307:
#line 1048 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 309:
#line 1056 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1060 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1064 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1068 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1076 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1080 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1084 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 316:
#line 1088 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 317:
#line 1092 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1096 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 319:
#line 1100 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 320:
#line 1104 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 322:
#line 1112 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 323:
#line 1116 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 324:
#line 1120 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 325:
#line 1124 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 326:
#line 1128 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 327:
#line 1132 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 328:
#line 1136 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 329:
#line 1140 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 330:
#line 1144 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 331:
#line 1148 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 332:
#line 1152 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 333:
#line 1156 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 334:
#line 1160 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 335:
#line 1164 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 336:
#line 1168 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 337:
#line 1172 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 338:
#line 1176 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 339:
#line 1180 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1184 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1188 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1192 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1196 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1200 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1204 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1208 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1212 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1216 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1220 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1224 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1228 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1232 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1236 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1240 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1244 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1248 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1252 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 358:
#line 1256 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1260 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 360:
#line 1264 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 361:
#line 1268 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 362:
#line 1272 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 363:
#line 1276 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1280 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1284 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1288 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1292 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 368:
#line 1296 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1300 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1304 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1308 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1312 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 373:
#line 1316 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 374:
#line 1320 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 375:
#line 1324 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 376:
#line 1328 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 377:
#line 1332 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 378:
#line 1336 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 379:
#line 1340 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 380:
#line 1344 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 381:
#line 1348 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 382:
#line 1352 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 383:
#line 1356 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 384:
#line 1360 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 385:
#line 1364 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 386:
#line 1368 "Iril/IR/IR.jay"
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
   40,   40,   40,   40,   40,   40,   38,   44,   44,    5,
    5,    5,    5,    5,   45,   45,   45,   34,   34,   46,
   46,   47,   47,   47,   47,   47,   47,   41,   41,   39,
   39,   39,   39,   39,   39,   39,   39,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   16,   16,   42,   42,
   37,   37,   48,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   50,   50,   50,   50,   50,   50,   50,
   50,   50,   50,   50,   50,   50,   50,   50,   50,   51,
   52,   52,   13,   13,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   55,   20,   20,   20,   20,   20,   20,
   20,   20,   20,   56,   27,   27,   57,   54,   54,   25,
   58,   58,   53,   53,   59,   60,   60,   36,   36,   61,
   61,   62,   62,   62,   62,   63,   63,   65,   65,   65,
   65,   67,   68,   68,   69,   69,   70,   70,   70,   70,
   70,   70,   71,   71,   71,   71,   71,   71,   71,   71,
   21,   21,   72,   72,   72,   72,   73,   73,   74,   75,
   75,   76,   77,   77,   78,   78,   43,   79,   80,   64,
   64,   81,   81,   81,   81,   81,   81,   81,   82,   82,
   82,   82,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,
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
    1,    1,    1,    2,    3,    2,    2,    1,    2,    4,
    5,    6,    6,    7,    1,    2,    1,    3,    2,    1,
    3,    1,    2,    2,    3,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    4,    1,    1,    4,    4,    2,    1,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    6,    9,    8,    6,    6,    3,
    3,    3,    5,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    2,    1,    2,    1,    3,    2,
    1,    2,    1,    3,    1,    1,    3,    1,    2,    3,
    1,    2,    3,    1,    2,    1,    2,    1,    2,    3,
    4,    1,    3,    2,    1,    3,    2,    3,    3,    2,
    4,    5,    1,    1,    1,    1,    6,    9,    6,    6,
    1,    3,    1,    1,    2,    2,    1,    3,    5,    1,
    2,    3,    1,    2,    1,    1,    1,    1,    5,    1,
    3,    2,    7,    2,    2,    7,    1,    1,    8,    9,
    9,   10,    5,    6,    5,    7,    5,    5,    6,    4,
    4,    5,    5,    6,    6,    7,    5,    5,    6,    6,
    6,    7,    5,    6,    7,    7,    8,    6,    4,    4,
    5,    6,    5,    2,    5,    4,    4,    4,    4,    5,
    6,    7,    6,    6,    6,    4,    3,    4,    7,    8,
    5,    6,    5,    5,    6,    3,    4,    5,    6,    7,
    4,    5,    6,    6,    4,    5,    7,    8,    5,    6,
    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,    0,   96,  107,   99,  100,  101,  102,   98,  130,
   39,   37,   40,   41,   42,   43,   44,   45,  297,  167,
  168,  169,    0,   38,    0,  160,  161,  165,  164,  166,
    0,  179,  180,    0,  162,  163,    0,    0,    0,   97,
    0,    0,    0,    0,    0,  131,  132,    0,    0,    0,
    3,    0,    0,    0,  158,    0,    4,    0,    0,  177,
  178,   35,   36,   46,   47,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  176,   90,    0,
    0,    0,    0,    0,    0,    0,  136,    0,    0,    0,
  172,    0,    0,    0,   65,    0,    0,    0,    0,    0,
    0,    0,    0,  159,    5,    6,    0,    0,    0,    0,
    0,    0,    0,    0,    8,    0,    7,    0,    0,    0,
    0,    0,    0,    0,   91,    0,    0,    0,    0,  135,
    0,  113,  103,    0,    0,  110,    0,    0,   66,    0,
  156,  157,  149,    0,    0,  150,  183,    0,    0,    0,
  181,    0,    0,    0,    0,    0,    0,    0,  227,  228,
  226,  229,  230,  231,  225,  214,  233,  232,    0,    0,
    0,    0,    0,    0,    0,    0,  213,    0,    0,    0,
    0,    0,    0,    0,    0,   48,    0,    0,    0,   74,
   73,   13,    0,    0,   67,   72,  175,  171,  174,    0,
    0,    0,    0,  106,  104,    0,    0,    0,   88,   87,
   79,   77,   78,   80,   81,   82,   83,    0,   75,  153,
    0,  148,    0,    0,    0,    0,    0,    0,  123,  182,
    0,    0,    0,    0,  141,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  238,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   15,    0,
    0,    0,   68,   14,    0,  210,  212,  211,  235,  108,
   92,  109,  111,    0,    0,   12,   76,  155,  151,    0,
  119,    0,    0,    0,    0,    0,    0,    0,  307,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  248,  251,    0,    0,
  256,    0,  300,  308,    0,  137,    0,  142,    0,    0,
  143,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  222,    0,    0,    0,  220,  221,    0,    0,    0,    0,
    0,   61,   64,    0,   59,    0,   50,   62,    0,   56,
   58,   63,   60,   51,   52,   49,   17,   16,   71,   70,
   69,   84,  284,    0,  283,    0,  281,    0,    0,    0,
  305,    0,    0,  302,    0,    0,    0,    0,  304,  295,
  296,    0,    0,  293,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  298,  344,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  184,  185,  186,  187,  188,  189,  190,  191,
  192,  193,    0,  194,  195,  206,  207,  208,  209,  197,
  199,  200,  201,  202,  198,  196,  204,  205,  203,    0,
    0,    0,    0,    0,    0,    0,    0,  114,  249,    0,
  257,    0,    0,    0,  124,  144,   33,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  239,    0,    0,    0,
    0,    0,    0,    0,  240,    0,  286,  285,   89,    0,
  120,  250,    0,  301,  234,    0,    0,  262,    0,    0,
    0,    0,    0,    0,  294,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  287,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  357,    0,    0,  115,
   20,    0,   28,    0,    0,    0,    0,    0,    0,  223,
    0,    0,    0,    0,    0,   54,    0,   57,  282,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  339,    0,    0,  245,  246,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  358,    0,    0,    0,    0,
  219,  215,  218,    0,   23,    0,    0,   53,    0,    0,
    0,  264,    0,    0,  265,    0,    0,    0,    0,  313,
    0,  341,  379,    0,  350,  364,    0,  345,  382,    0,
  368,  343,  384,  376,  372,    0,    0,  361,    0,  318,
  317,  363,  385,    0,    0,    0,    0,  315,    0,    0,
  224,  237,    0,    0,    0,    0,    0,    0,    0,    0,
  288,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  241,    0,  243,    0,
    0,    0,    0,  290,    0,    0,    0,  267,  263,    0,
    0,    0,    0,    0,  314,  380,  365,  369,  373,  362,
  319,  354,  374,  247,    0,    0,    0,    0,    0,    0,
    0,    0,  353,  342,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  242,  217,    0,  303,
    0,  306,  291,    0,  274,  275,  276,    0,    0,    0,
    0,  273,  269,  268,  266,    0,    0,    0,    0,  316,
    0,  359,    0,  377,    0,  289,  370,  299,    0,    0,
    0,    0,  216,  244,  292,  271,    0,    0,    0,    0,
    0,  309,    0,    0,    0,  360,  378,    0,  272,    0,
    0,    0,    0,  310,  311,    0,    0,    0,    0,    0,
  312,    0,    0,    0,    0,    0,  280,  277,  279,    0,
    0,  278,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   60,   12,   13,   14,  238,  213,  205,   61,
   87,  214,  287,   88,   69,  253,   90,  206,  397,  197,
  416,  399,  400,  401,  402,  215,  807,  239,  101,  102,
  155,  156,   15,  119,  169,  356,  254,  249,   75,   65,
   76,   66,   67,   16,  255,  165,  166,  171,  493,  510,
  288,  558,  808,  267,  782,  424,  693,  809,  686,  687,
  357,  358,  359,  360,  361,  362,  559,  654,  744,  745,
  873,  417,  615,  616,  813,  814,  433,  434,  468,  620,
  363,  364,
  };
  protected static readonly short [] yySindex = {          -67,
  -15, -122,   21,   56,   72, 3097, -217, -237,    0,  -67,
    0,    0,    0,    0, -176, 3540,  -97,  107,  142, 2711,
  -68,  -26,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  198,    0,  202,    0,    0,    0,    0,    0,
  210,    0,    0,    1,    0,    0, 1568, -104,   66,    0,
 -106,  219,  228, 4908, 3264,    0,    0,  113,  126,  320,
    0,  238, 3623,  -20,    0, 3623,    0,  148,  164,    0,
    0,    0,    0,    0,    0, -117, 4908,  -81,    4,   44,
  -47,  396,  -21,  331,  204, 4908, 4908,    0,    0,  219,
    5,  228,  207, 4908,  208,  172,    0,  101, 2064,  228,
    0, 4908,  228, 3623,    0,  209,  359, 1932, -131,  -17,
 3623,  238,  -13,    0,    0,    0, 4908,    4,    4, 3803,
 4908,    4, 4908,    4,    0,  361,    0, -234,  448,  368,
 4052,  452,  332,  351,    0, 4908, 4908,   11, 4908,    0,
  237,    0,    0,  219,  191,    0,  228,  228,    0, 1251,
    0,    0,    0,   87,  236,    0,    0,  173, -101, -214,
    0,  238,  -12, -131,  238, 1604, 4908, 4908,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -36,  455,
  457,  458, 4934, 4960, 4934,  456,    0, 3803, 4908, 3803,
 4908,  441,  443,  444,  243,    0, -234, 1825,    0,    0,
    0,    0,  -30,  974,    0,    0,    0,    0,    0,  219,
   18,  450,   35,    0,    0, 1435,  442,  478,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1988,    0,    0,
 2372,    0, 3907, -167, 4914,  -94, 4934,  262,    0,    0,
 -131,  238,  173,  173,    0, -131,  141,  477,  219, 3841,
  482, 4908, 4934, 4934, 4934,    0,   25, 2965,   39,   -4,
  144,  483, 3803,  488, 3803, 4817, 4850, 1362,    0, -234,
  252,  -28,    0,    0, 4188,    0,    0,    0,    0,    0,
    0,    0,    0,  273, 4883,    0,    0,    0,    0,  280,
    0,  481,  484, 4934, -179, 4934, 3334, 4934,    0, 1551,
 4908, 1551, 4908, 1551, 4908, 4908, 1494, 4908, 4908, 4908,
 1551, 1545, 1722, 4908, 4908, 4908, 4934, 4934, 4934, 4934,
 4934, 4908, 3236, 3329,  211,  156, 4934, 4934, 4934, 4934,
 4934, 4934, 4934, 4934, 4934, 4934, 4934, 4934,   13, 3812,
 4908, 4908, 3334,  122, 4908, 3903,    0,    0, 7386, -217,
    0, -217,    0,    0, 4914,    0,  234,    0, -131,  173,
    0,  291, -242,  170,  506, 4908,  125,  167,  168,  176,
    0, 4934,  974,   29,    0,    0,  294,  181,  522,  188,
  524,    0,    0,  529,    0,  718,    0,    0,  446,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -34,    0,  288,    0,  234, 7386, 7875,
    0,  300, 3776,    0,  531,  928, 3623, 3623,    0,    0,
    0,  974, 1551,    0,  974,  974, 1551,  974,  974, 1551,
  974,  974, 4908,  974,  974,  974,  974,  974, 1551, 4908,
  974, 4908,  974,  974,  974,  974,  532,  534,  535,  536,
  537,  146, 4908,  154, 4934,  538,    0,    0, 4908,  177,
  196,  199,  203,  205,  220,  221,  223,  226,  235,  244,
  248,  249,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4908,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4908,
   32,  974,  486, 4908, 3623, 3334,  -40,    0,    0, -217,
    0,  126,  126, 4025,    0,    0,    0,  346,  328,  366,
  245,  325, 4934, 4908, 4908, 4908,    0,  559, -217,  373,
  246,  380,  259, 4728,    0, 4850,    0,    0,    0, 4883,
    0,    0, -217,    0,    0,  595,  374,    0,  601,  928,
  928, 3623,  600,  974,    0,  602,  604,  974,  606,  607,
  974,  608,  609,  974,  611,  612,  613,  614,  617,  974,
  974,  621,  974,  623,  625,  627,  629, 4934, 4934, 4934,
 1362, 4934,  493,  381, 4908,  633, 4908,  386, 4934, 4908,
 4908, 4908, 4908, 4908, 4908, 4908, 4908, 4908, 4908, 4908,
 4908,  974,  974, 3776,  634,    0,  638,  260,  601,  601,
  928,  928, 4908,  486, 4908, 3623,    0, 4934,  126,    0,
    0, -217,    0,  388, 4934,  639,  407,  409,  411,    0,
  126, -217,  405, -217,  430,    0,  307,    0,    0,  126,
  374,  598, 2162,  299,  601,  601,  928, 3776,  646, 3776,
 3776,  674, 3776, 3776,  676, 3776, 3776,  677, 3776, 3776,
 3776, 3776, 3776,  679,  681, 3776,  682, 3776, 3776, 3776,
 3776,    0,  683,  684,    0,    0,  685,  686,  474,  688,
 4908,  974,  689, 4908,  692, 4934,  697,  219,  219,  219,
  219,  219,  219,  219,  219,  219,  219,  219,  219,  698,
  706,  708,  662, 4934,  494,  173,  173,  601,  601,  928,
  601,  601,  928,  928, 3623,    0,  126, -217,  711,  -19,
    0,    0,    0,  126,    0,  126, -217,    0,  717, 4908,
 4313,    0, 1481,  309,    0,  374,  375,  376,  601,    0,
 3776,    0,    0, 3776,    0,    0, 3776,    0,    0, 3776,
    0,    0,    0,    0,    0, 3776, 3776,    0, 3776,    0,
    0,    0,    0, 4934, 4934, 1362, 1362,    0,  385,  724,
    0,    0,  390,  728,  395,  737,  -19, 3776, 3776, 3776,
    0,  738,  740,  173,  173,  173,  173,  601,  173,  173,
  601,  601,  928,  126,  -19, 4934,    0,  317,    0,  126,
  374,  743, 4744,    0,  748,  573, 3175,    0,    0, 4440,
  463,  374,  374,  404,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  685,  540,  415,  543,  427,  558,
  -19,  774,    0,    0,  726, 4934,  562,  173,  173,  173,
  173,  173,  173,  173,  601,  322,    0,    0,  -19,    0,
  374,    0,    0, 4783,    0,    0,    0,  453,  786,  788,
  797,    0,    0,    0,    0,  374,  512,  513,  374,    0,
  587,    0,  591,    0,  774,    0,    0,    0,  173,  173,
  173,  173,    0,    0,    0,    0,  324,  810, 4934, 4934,
 4934,    0,  374,  374,  526,    0,    0,  173,    0, 4908,
  464,  467,  468,    0,    0,  374,  403, 4908, 4908, 4908,
    0, 4934,  423,  429,  431,  812,    0,    0,    0,  -19,
  334,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0, 3665,    0,    0,  857,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 1214,    0,    0,    0,    0,    0,
 1393,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3413,  783,  599,    0,    0,    0,    0,    0, 3707,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   10,
    0,    0,    0,    0,    0, 3498,    0, 1039,    0,  603,
    0,    0,  605,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  198,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  198,  198,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  190,    0,    0,  616,  620,    0,    0,
    0,    0,    0,  242,    0,    0,    0,  -93,    0,  -92,
    0,    0,    0,  187,    0,   53,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  198,    0,  198,
    0,    0,    0,    0,    0,    0,    0,    0, 1569,    0,
    0,    0,    0,  198,    0,    0,    0,    0,    0,   22,
  198,    0,  198,    0,    0,    0, 2217, 2556,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  338,    0,    0,  -91,    0,    0,    0,    0,    0,    0,
    0,    0,  217,  391,    0,    0,    0,  552,   65,  198,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  198,    0,  198,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4155,
    0, 7489,    0,    0,    0,    0,  -90,    0,    0,  593,
    0,    0,    0,    0,    0,    0,  198,    0,    0,    0,
    0,    0,   10,    0,    0,    0,    0,    0,    0,    0,
  618,    0,    0,   27,    0,  198,    0,    0,  344,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  198,    0,    0,    0,  -88,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  198,    0,    0,  198,  198,    0,  198,  198,    0,
  198,  198,    0,  198,  198,  198,  198,  198,    0,    0,
  198,    0,  198,  198,  198,  198,    0,    0,    0,    0,
    0,  198,    0,  198,    0,    0,    0,    0,    0,  198,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  198,  198,    0,    0,    0,    0,  198,    0,    0, 4277,
    0, 4407, 7592,    0,    0,    0,    0,    0,    0,    0,
    0,  198,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 7695,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  198,    0,    0,    0,  198,    0,    0,
  198,    0,    0,  198,    0,    0,    0,    0,    0,  198,
  198,    0,  198,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  198,    0,    0,    0,  198,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  198,  198,    0, 5017,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4529,    0,
    0,  626,    0,    0,    0,    0,  198,  198,  198,    0,
  654,    0,    0,    0,    0,    0,    0,    0,    0, 7798,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5120,    0,    0,    0,
    0,  198,    0,    0,    0,    0,    0, 1676, 1779, 1905,
 2031, 2134, 2241, 2370, 2473, 2580, 2709, 2812, 2919,    0,
    0,    0,    0,    0,    0, 5223,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  817,  847,    0,    0,
    0,    0,    0,  930,    0,  955, 1000,    0,    0,    0,
    0,    0,  198,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5326, 5429, 5532, 5635,    0, 5738,    0,
    0,    0,    0, 1270,    0,    0,    0,    0,    0, 1278,
    0,    0,    0,    0,  358,  198,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5841,    0,    0,    0,    0,    0,
    0, 5944,    0,    0,    0,    0,    0, 6047, 6150, 6253,
 6356, 6459, 6562, 6665,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 6768,    0,    0,    0, 6871, 6974,
 7077, 7180,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 7283,    0,    0,
    0,    0,    0,    0,    0,    0,  198,    0,    0,    0,
    0,    0,  198,  198,  198,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  849,  772,    0,    0,    0,    0,  659,  663,  853,
   30,   -6,  -66,   -2, -163,   36,    0,  622,  619, -252,
 -533,    0,  335,    0, -719,    0,  364,  651,  780,  160,
    0,  693,    0,  -61,    0,  545,  -83, -229,   -1,    0,
  -64,  839,  -58,    0, -212,    0,  660, -152,    0,    0,
    0,  357, -753, -116,    0, -520, -552,   46,  135,  143,
 -339,  499,    0,  565,  574,  514, -189,  353,    0,  112,
    0,  389,    0,  222,    0,  123,  -24, -164,    0,  321,
    0,  521,
  };
  protected static readonly short [] yyTable = {            62,
  114,  108,  107,  262,   64,  246,   94,  108,  121,   62,
  647,  140,  136,  285,  301,  285,  519,  250,  104,  109,
  812,  245,  109,  398,  398,  405,  109,  109,  365,  116,
  121,  117,  122,  842,  118,  170,  652,  528,  368,  382,
   58,  202,  415,  371,  695,   17,  203,  150,  146,   95,
  100,  856,   34,   93,  146,   89,   68,   62,   62,  108,
  174,   95,  112,  196,   19,   94,   62,   60,  382,   62,
   60,   59,  382,  108,  124,   57,  108,  269,  270,  290,
  130,   20,  382,  128,  244,   70,  422,  885,  386,  143,
  144,  250,   95,  812,  284,   72,  408,  100,  247,  241,
  385,  250,  154,   57,   95,   62,  423,   62,  167,  258,
  251,  164,  124,  256,   62,  127,   21,  131,  133,  124,
  176,  129,  614,  132,  198,  134,  200,  292,  108,  145,
  739,  272,   22,  274,   93,  222,  529,  525,  780,  220,
  221,  784,  223,  545,  204,  247,   94,   18,   19,  381,
   60,  384,  103,  538,  168,  167,  526,  177,  178,   80,
   81,  199,   77,  201,  248,   63,  108,   78,  533,  370,
  259,  260,   68,   80,   81,   74,  931,   82,   83,  116,
  116,  121,  117,  122,  519,  118,  140,  108,  551,  593,
  369,  167,  273,  375,  275,  108,  522,  595,  523,   39,
    1,    2,   79,   85,    3,    4,  389,    5,  391,   80,
   81,  300,   91,   82,   83,   58,  145,  250,  108,  154,
  599,    6,    7,  110,  113,  821,   52,   53,  547,   95,
  112,  225,  120,  112,  226,  123,  164,   95,  548,  124,
  135,   96,  427,  283,  209,  283,   59,   92,  428,   97,
   93,  122,  139,   24,  172,  377,    8,   98,  175,  252,
  108,  383,   25,   26,   27,   28,   29,  109,  565,  396,
  396,  157,  565,  158,  627,  565,  242,  118,   57,  243,
  173,   95,  152,  279,  565,  152,  280,  437,  414,  440,
  860,  415,  407,  398,  515,  280,  449,  415,   82,   83,
   62,  877,  878,  432,  435,  436,  438,  439,  441,  442,
  444,  445,  446,  447,  448,  451,  453,  454,  455,  456,
   34,   34,  105,  619,   34,  462,  464,   34,  549,  470,
  897,  550,   19,   19,   80,   81,   19,   19,  685,   19,
  895,   34,   34,  261,  511,  512,   62,  738,  517,  819,
  550,  514,  820,   19,   19,  902,  629,  858,  905,  240,
  859,  806,  893,  562,  909,  859,  108,  550,  635,  532,
  655,  656,  218,  108,  932,  641,   34,  859,  154,  111,
  117,  154,  914,  915,   55,  628,  115,   55,   19,  650,
  147,  219,  108,   40,   41,  921,   42,   43,  270,  116,
   45,  270,   46,   47,   48,   49,   50,  125,   51,  483,
  484,  485,  486,  487,  488,  489,  490,  491,  492,   23,
   62,   62,  108,  126,  691,  124,  564,  108,   24,  696,
  568,  718,  719,  571,  721,  138,  574,   25,   26,   27,
   28,   29,  580,  581,  108,  583,  922,  731,  108,  732,
  108,  733,  108,  141,  140,  140,  594,  626,  140,  140,
  142,  140,  598,  927,  108,   54,  426,  749,  727,  928,
  108,  929,  108,  147,  149,  140,  140,   39,  734,  151,
  736,  160,  159,  104,  145,  145,  612,  207,  145,  145,
  208,  145,  217,  224,  263,  167,  264,  265,  276,  271,
  277,  278,  294,  613,  216,  145,  145,   62,   62,   62,
  140,  291,  513,  623,  625,   55,   56,  295,  367,  372,
  373,  376,  387,  685,  685,  109,  388,  637,  638,  639,
  798,  390,  412,  801,  802,  469,  418,  414,  419,  396,
  145,  516,  467,  414,  420,  194,  247,  527,  530,  531,
  539,   18,   58,  534,  535,   62,  266,  266,  266,  540,
  124,  725,  536,  872,  804,  541,  542,  543,  544,  546,
  289,  216,  555,  810,  557,  588,  195,  589,  590,  591,
  592,  597,  600,   59,  632,  601,  560,  561,  692,  602,
  692,  603,  146,  698,  699,  700,  701,  702,  703,  704,
  705,  706,  707,  708,  709,  631,  604,  605,  193,  606,
  366,  415,  607,  855,  108,   57,   62,   26,   62,   62,
  640,  608,  633,  634,  643,   31,  378,  379,  380,  642,
  609,  266,  794,  795,  610,  611,  644,  645,  651,  422,
  653,  250,  250,  658,  728,  660,  743,  661,  411,  663,
  664,  666,  667,   21,  669,  670,  671,  672,  147,  147,
  673,  735,  147,  147,  676,  147,  678,  421,  679,  425,
  680,  429,  681,  621,  622,  624,  694,  713,  817,  147,
  147,  714,  730,  715,  692,  746,  737,  692,  740,  751,
  457,  458,  459,  460,  461,  250,  250,  466,  250,  250,
  471,  472,  473,  474,  475,  476,  477,  478,  479,  480,
  481,  482,  848,  849,  147,  851,  852,  754,   62,  757,
  760,  657,  766,  124,  767,  769,  774,  775,  776,  777,
  778,  779,  783,  396,  816,  785,  250,  250,  250,  289,
  787,  788,  179,  180,  181,  537,  182,  183,  184,  789,
  185,  790,  614,  793,  805,  250,  209,  286,  186,  108,
  811,  822,  823,  836,  187,   24,  889,  837,  838,  890,
  891,  839,  188,  840,   25,   26,   27,   28,   29,  556,
  841,  846,  720,  847,  723,  724,  861,  864,  563,  876,
  879,  566,  567,  881,  569,  570,  880,  572,  573,  882,
  575,  576,  577,  578,  579,  883,  396,  582,  908,  584,
  585,  586,  587,  743,  884,  124,   32,  859,  886,   18,
   18,  888,   95,   18,   18,  899,   18,  900,  596,  179,
  180,  181,  898,  182,  183,  184,  901,  185,  903,  904,
   18,   18,   95,  906,  865,  866,   29,  907,  867,  910,
  918,  187,  916,  919,  920,  930,    1,  414,   71,  188,
  146,  146,  137,  189,  146,  146,  282,  146,  617,  281,
  125,  689,   86,   95,  126,   18,  127,  190,  191,  192,
  648,  146,  146,  148,  803,   26,   26,  128,  297,   26,
   26,  129,   26,   31,   31,  404,  636,   31,   31,  106,
   31,  406,  299,  917,  894,   95,   26,   26,  618,  524,
  834,  923,  924,  925,   31,   31,  146,  552,  293,  835,
  659,   21,   21,  520,  662,   21,   21,  665,   21,   24,
  668,  875,  521,  553,  791,  863,  674,  675,  649,  677,
  554,   26,   21,   21,  722,    0,    0,    0,    0,   31,
  868,  682,  683,  684,   22,  688,  690,    0,    0,    0,
    0,    0,  697,    0,  869,  870,  871,  109,  710,  711,
  712,  716,  717,    0,  179,  180,  181,   21,  182,  183,
  184,    0,  185,    0,    0,    0,    0,  194,    0,    0,
    0,  726,    0,    0,    0,    0,  187,    0,  729,   25,
    0,    0,    0,    0,  188,    0,    0,  747,  748,    0,
    0,    0,    0,    0,  750,  108,  752,  753,  195,  755,
  756,    0,  758,  759,    0,  761,  762,  763,  764,  765,
    0,    0,  768,  194,  770,  771,  772,  773,  105,   95,
   95,   95,    0,   95,   95,   95,    0,   95,  781,    0,
  193,    0,    0,    0,   95,   95,    0,    0,    0,  786,
    0,   95,    0,    0,  195,    0,    0,    0,    0,   95,
  796,  797,    0,  799,  800,    0,    0,  792,  105,  105,
  105,    0,  105,    0,   32,   32,    0,    0,   32,   32,
    0,   32,    0,    0,    0,    0,  193,    0,  105,  818,
  105,  824,    0,    0,    0,   32,   32,  825,    0,    0,
  826,    0,    0,  827,   29,   29,  828,    0,   29,   29,
    0,   29,  829,  830,    0,  831,    0,    0,    0,  105,
    0,  105,    0,    0,    0,   29,   29,  832,  833,    0,
   32,    0,    0,    0,  843,  844,  845,    0,    0,    0,
  850,    0,    0,  853,  854,    0,    0,    0,    0,    0,
   95,  105,    0,  105,    0,    0,    0,    0,    0,  857,
   29,    0,    0,  874,   95,   95,   95,    0,    0,    0,
    0,    0,    0,    0,  179,  180,  181,    0,  182,  183,
  184,    0,  185,    0,    0,    0,    0,   24,   24,  286,
  186,   24,   24,    0,   24,   95,  187,  892,    0,  887,
    0,    0,    0,    0,  188,    0,    0,    0,   24,   24,
    0,    0,   22,   22,    0,    0,   22,   22,    0,   22,
  179,  180,  181,    0,  182,  183,  184,    0,  185,    0,
    0,    0,    0,   22,   22,  286,  186,    0,    0,    0,
    0,    0,  187,   24,  170,    0,    0,  170,    0,    0,
  188,    0,  911,  912,  913,    0,    0,   25,   25,   30,
    0,   25,   25,  170,   25,    0,    0,   27,   22,    0,
    0,    0,    0,    0,    0,  926,    0,    0,   25,   25,
    0,    0,    0,    0,    0,  105,  105,  105,    0,  105,
  105,  105,    0,  105,  170,  189,  105,  105,    0,    0,
  105,  105,  105,  105,  105,    0,    0,  105,    0,  190,
  191,  192,    0,   25,    0,  105,    0,  105,  105,    0,
    0,  105,    0,    0,    0,    0,  170,    0,    0,    0,
    0,    0,    0,    0,    0,  105,  105,    0,  105,  105,
    0,  189,  105,  105,  105,  105,  105,  105,  105,    0,
  105,    0,  105,    0,    0,  190,  191,  192,    0,    0,
    0,    0,    0,  105,  105,  105,    0,  105,  105,    0,
    0,    0,  105,    0,  105,    0,    0,  105,  105,  105,
  105,  105,  105,  105,  105,  105,  105,    0,  105,  105,
    0,  105,  105,  105,  105,  105,  105,  105,  105,  105,
  105,  105,  105,  105,    0,    0,  105,    0,    0,    0,
  105,  105,  105,  105,  105,    0,  105,  105,  105,  105,
  105,  105,  105,  173,  105,    0,  173,    0,    0,    0,
    0,    0,    0,    0,    0,  105,    0,    0,    0,    0,
    0,    0,  173,    0,    0,    0,  105,  105,  105,  105,
    0,  105,    0,  105,  105,    0,    0,  105,  105,    0,
  170,  170,  170,    0,  170,  170,  170,  170,  170,    0,
    0,    0,    0,  173,    0,  170,  170,    0,    0,    0,
    0,    0,  170,    0,   58,  170,  170,  170,  170,  170,
  170,    0,    0,    0,    0,    0,  170,    0,    0,    0,
  227,    0,    0,    0,    0,  173,    0,    0,    0,    0,
  170,  170,  108,  170,  170,   59,  228,  170,    0,  170,
  170,  170,  170,  170,    0,  170,    0,   30,   30,    0,
  194,   30,   30,    0,   30,   27,   27,    0,    0,   27,
   27,    0,   27,   58,    0,    0,    0,   57,   30,   30,
    0,    0,    0,    0,    0,    0,   27,   27,  229,  230,
  231,  195,    0,    0,    0,  232,  233,    0,  234,  235,
  236,  237,    0,    0,   59,    0,    0,    0,    0,    0,
    0,  170,  170,   30,    0,    0,    0,    0,    0,    0,
    0,   27,    0,  193,   58,  170,  170,  170,   96,  236,
   58,    0,  236,    0,    0,    0,   57,    0,  179,  180,
  181,    0,  182,  183,  184,    0,  185,   58,    0,    0,
  236,    0,    0,    0,    0,   59,    0,    0,    0,    0,
  187,   59,  170,  170,    0,  108,    0,  257,  188,  173,
  173,  173,    0,  173,  173,  173,  173,  173,   59,    0,
    0,  236,    0,  194,  173,  173,    0,   57,    0,    0,
    0,  173,    0,   57,  173,  173,  173,  173,  173,  173,
    0,    0,    0,    0,    0,  173,    0,    0,    0,    0,
   57,  236,   99,  236,  195,    0,    0,    0,   23,  173,
  173,    0,  173,  173,    0,    0,  173,   24,  173,  173,
  173,  173,  173,    0,  173,   95,   25,   26,   27,   28,
   29,    0,    0,    0,    0,    0,  193,    0,  152,    0,
    0,    0,    0,    0,    0,    0,    0,  179,  180,  181,
    0,  182,  183,  184,    0,  185,    0,    0,    0,    0,
    0,    0,  286,  186,    0,    0,    0,   23,    0,  187,
    0,    0,    0,    0,    0,    0,   24,  188,    0,    0,
  173,  173,    0,  111,    0,   25,   26,   27,   28,   29,
    0,   58,    0,    0,  173,  173,  173,   40,   41,    0,
   42,   43,    0,    0,   45,    0,   46,   47,   48,   49,
   50,    0,   51,    0,    0,    0,    0,    0,   23,    0,
    0,    0,   59,    0,   23,    0,    0,   24,   95,    0,
    0,  173,  173,   24,    0,  236,   25,   26,   27,   28,
   29,   23,   25,   26,   27,   28,   29,    0,    0,    0,
   24,  236,  236,    0,   57,    0,    0,    0,    0,   25,
   26,   27,   28,   29,    0,  443,    0,    0,  189,   54,
  179,  180,  181,    0,  182,  183,  184,    0,  185,    0,
    0,    0,  190,  191,  192,    0,  186,    0,    0,    0,
    0,    0,  187,    0,   58,    0,    0,    0,    0,    0,
  188,    0,    0,    0,    0,    0,    0,  430,  431,    0,
    0,    0,    0,  236,  236,  236,  450,  236,  236,   55,
   56,    0,  236,    0,  236,   59,    0,  236,  236,  236,
  236,  236,  236,  236,  236,  236,  236,    0,  236,  236,
    0,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,  236,  236,   95,    0,  236,   57,  381,  381,
  236,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,  236,    0,  236,    0,    0,    0,    0,    0,
    0,    0,  163,    0,    0,  236,    0,    0,    0,    0,
    0,  189,    0,    0,    0,   23,  236,  236,  236,  236,
    0,   58,    0,  236,   24,  190,  191,  192,    0,    0,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
  381,  381,  381,    0,  381,  381,    0,    0,    0,  381,
    0,  381,   59,    0,  381,  381,  381,  381,  381,  381,
  381,  381,  381,  381,    0,  381,  381,    0,  381,  381,
  381,  381,  381,  381,  381,  381,  381,  381,  381,  381,
  381,  386,  386,  381,   57,    0,    0,  381,  381,  381,
  381,  381,    0,  381,  381,  381,  381,  381,  381,  381,
   95,  381,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  381,  452,    0,    0,    0,    0,  209,  210,
    0,    0,    0,  381,  381,  381,  381,   24,  211,    0,
  381,    0,    0,    0,  153,    0,   25,   26,   27,   28,
   29,    0,  296,  386,  386,  386,    0,  386,  386,    0,
    0,    0,  386,   58,  386,    0,    0,  386,  386,  386,
  386,  386,  386,  386,  386,  386,  386,    0,  386,  386,
    0,  386,  386,  386,  386,  386,  386,  386,  386,  386,
  386,  386,  386,  386,   59,    0,  386,    0,    0,    0,
  386,  386,  386,  386,  386,    0,  386,  386,  386,  386,
  386,  386,  386,   95,  386,    0,    0,  371,  371,    0,
    0,    0,    0,    0,    0,  386,   57,    0,    0,    0,
    0,    0,    0,    0,    0,   23,  386,  386,  386,  386,
    0,    0,  742,  386,   24,    0,    0,    0,    0,  161,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,   58,    0,    0,    0,  162,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  371,
  371,  371,    0,  371,  371,    0,    0,  227,  371,    0,
  371,    0,   59,  371,  371,  371,  371,  371,  371,  371,
  371,  371,  371,  228,  371,  371,    0,  371,  371,  371,
  371,  371,  371,  371,  371,  371,  371,  371,  371,  371,
   95,    0,  371,    0,   57,    0,  371,  371,  371,  371,
  371,    0,  371,  371,  371,  371,  371,  371,  371,    0,
  371,    0,    0,  349,  349,  229,  230,  231,    0,    0,
    0,  371,  232,  233,    0,  234,  235,  236,  237,    0,
    0,    0,  371,  371,  371,  371,    0,   23,    0,  371,
    0,    0,    0,    0,    0,    0,   24,    0,    0,    0,
    0,   85,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,    0,  152,    0,    0,
    0,    0,    0,    0,    0,  349,  349,  349,    0,  349,
  349,    0,    0,    0,  349,    0,  349,    0,    0,  349,
  349,  349,  349,  349,  349,  349,  349,  349,  349,    0,
  349,  349,    0,  349,  349,  349,  349,  349,  349,  349,
  349,  349,  349,  349,  349,  349,  346,  346,  349,   95,
    0,    0,  349,  349,  349,  349,  349,    0,  349,  349,
  349,  349,  349,  349,  349,   23,  349,    0,    0,    0,
    0,    0,    0,    0,   24,    0,    0,  349,    0,  741,
    0,    0,    0,   25,   26,   27,   28,   29,  349,  349,
  349,  349,    0,    0,    0,  349,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  346,  346,
  346,    0,  346,  346,    0,    0,   85,  346,    0,  346,
    0,    0,  346,  346,  346,  346,  346,  346,  346,  346,
  346,  346,   85,  346,  346,    0,  346,  346,  346,  346,
  346,  346,  346,  346,  346,  346,  346,  346,  346,    0,
    0,  346,   95,  347,  347,  346,  346,  346,  346,  346,
    0,  346,  346,  346,  346,  346,  346,  346,    0,  346,
    0,    0,    0,    0,   85,   85,   85,    0,    0,    0,
  346,   85,   85,    0,   85,   85,   85,   85,    0,    0,
    0,  346,  346,  346,  346,    0,    0,    0,  346,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  347,  347,  347,    0,  347,
  347,    0,    0,    0,  347,    0,  347,    0,    0,  347,
  347,  347,  347,  347,  347,  347,  347,  347,  347,    0,
  347,  347,    0,  347,  347,  347,  347,  347,  347,  347,
  347,  347,  347,  347,  347,  347,    0,    0,  347,   95,
    0,    0,  347,  347,  347,  347,  347,    0,  347,  347,
  347,  347,  347,  347,  347,    0,  347,    0,    0,    0,
    0,    0,  348,  348,  298,    0,    0,  347,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  347,  347,
  347,  347,    0,    0,  111,  347,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   40,   41,
   86,   42,   43,    0,    0,   45,    0,   46,   47,   48,
   49,   50,    0,   51,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  348,  348,  348,    0,  348,  348,
    0,    0,    0,  348,    0,  348,    0,    0,  348,  348,
  348,  348,  348,  348,  348,  348,  348,  348,    0,  348,
  348,    0,  348,  348,  348,  348,  348,  348,  348,  348,
  348,  348,  348,  348,  348,  383,  383,  348,   95,    0,
   54,  348,  348,  348,  348,  348,    0,  348,  348,  348,
  348,  348,  348,  348,    0,  348,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  348,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  348,  348,  348,
  348,    0,    0,    0,  348,    0,    0,    0,    0,    0,
   55,   56,    0,    0,    0,    0,    0,  383,  383,  383,
    0,  383,  383,    0,    0,   86,  383,    0,  383,    0,
    0,  383,  383,  383,  383,  383,  383,  383,  383,  383,
  383,   86,  383,  383,    0,  383,  383,  383,  383,  383,
  383,  383,  383,  383,  383,  383,  383,  383,    0,    0,
  383,   95,  375,  375,  383,  383,  383,  383,  383,    0,
  383,  383,  383,  383,  383,  383,  383,    0,  383,    0,
    0,    0,    0,   86,   86,   86,    0,    0,    0,  383,
   86,   86,    0,   86,   86,   86,   86,    0,    0,    0,
  383,  383,  383,  383,    0,    0,    0,  383,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  375,  375,  375,    0,  375,  375,
    0,    0,    0,  375,    0,  375,    0,    0,  375,  375,
  375,  375,  375,  375,  375,  375,  375,  375,    0,  375,
  375,    0,  375,  375,  375,  375,  375,  375,  375,  375,
  375,  375,  375,  375,  375,    0,    0,  375,   95,    0,
    0,  375,  375,  375,  375,  375,    0,  375,  375,  375,
  375,  375,  375,  375,    0,  375,    0,    0,    0,    0,
    0,  367,  367,    0,    0,    0,  375,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  375,  375,  375,
  375,   80,   81,    0,  375,   82,   83,   84,   31,   32,
   33,   34,   35,   36,   37,   38,    0,    0,    0,    0,
    0,    0,   44,    0,   58,    0,    0,    0,    0,    0,
    0,   85,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  367,  367,  367,    0,  367,  367,    0,
    0,    0,  367,    0,  367,   59,    0,  367,  367,  367,
  367,  367,  367,  367,  367,  367,  367,    0,  367,  367,
    0,  367,  367,  367,  367,  367,  367,  367,  367,  367,
  367,  367,  367,  367,  356,  356,  367,   57,    0,   99,
  367,  367,  367,  367,  367,    0,  367,  367,  367,  367,
  367,  367,  367,    0,  367,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  367,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  367,  367,  367,  367,
    0,    0,    0,  367,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  356,  356,  356,    0,
  356,  356,    0,    0,    0,  356,   58,  356,    0,    0,
  356,  356,  356,  356,  356,  356,  356,  356,  356,  356,
    0,  356,  356,    0,  356,  356,  356,  356,  356,  356,
  356,  356,  356,  356,  356,  356,  356,   59,    0,  356,
    0,  320,  320,  356,  356,  356,  356,  356,    0,  356,
  356,  356,  356,  356,  356,  356,    0,  356,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  356,   57,
    0,    0,    0,    0,    0,    0,    0,    0,  209,  356,
  356,  356,  356,    0,  194,    0,  356,   24,    0,    0,
    0,    0,    0,    0,    0,    0,   25,   26,   27,   28,
   29,    0,    0,  320,  320,  320,    0,  320,  320,    0,
    0,    0,  320,    0,  320,  195,    0,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,    0,  320,  320,
    0,  320,  320,  320,  320,  320,  320,  320,  320,  320,
  320,  320,  320,  320,    0,   58,  320,  193,    0,    0,
  320,  320,  320,  320,  320,    0,  320,  320,  320,  320,
  320,  320,  320,    0,  320,    0,    0,    0,    0,    0,
    0,    0,    0,   58,    0,  320,   59,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,  320,  320,  320,
    0,    0,    0,  320,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   59,    0,    0,    0,   57,    0,
   23,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,    0,    0,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,   57,    0,   58,   30,
    0,    0,    0,   58,   31,   32,   33,   34,   35,   36,
   37,   38,   39,   40,   41,    0,   42,   43,   44,    0,
   45,    0,   46,   47,   48,   49,   50,    0,   51,   59,
    0,    0,    0,    0,   59,    0,    0,    0,    0,   52,
   53,  179,  180,  181,    0,  182,  183,  184,    0,  185,
    0,    0,    0,    0,    0,    0,  286,  186,    0,    0,
    0,   57,    0,  187,    0,    0,   57,    0,    0,    0,
    0,  188,    0,    0,    0,    0,    0,  111,    0,    0,
    0,    0,  133,    0,    0,   54,    0,    0,    0,    0,
    0,   40,   41,    0,   42,   43,    0,    0,   45,    0,
   46,   47,   48,   49,   50,    0,   51,    0,    0,   23,
    0,    0,    0,  133,    0,    0,    0,    0,   24,    0,
    0,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,    0,    0,    0,   55,   56,   23,    0,    0,
    0,    0,    0,    0,    0,  133,   24,    0,    0,    0,
    0,    0,    0,    0,  463,   25,   26,   27,   28,   29,
    0,    0,  189,   54,    0,    0,  111,  134,    0,    0,
    0,    0,    0,    0,    0,    0,  190,  191,  192,    0,
   40,   41,    0,   42,   43,    0,    0,   45,    0,   46,
   47,   48,   49,   50,   85,   51,    0,    0,  134,    0,
    0,    0,  209,    0,    0,    0,    0,   23,    0,   58,
    0,   24,    0,   55,   56,    0,   24,    0,    0,    0,
   25,   26,   27,   28,   29,   25,   26,   27,   28,   29,
  134,    0,    0,    0,    0,    0,  111,    0,    0,    0,
   59,    0,    0,    0,    0,    0,    0,  465,    0,   39,
   40,   41,   54,   42,   43,    0,    0,   45,    0,   46,
   47,   48,   49,   50,    0,   51,    0,    0,    0,    0,
    0,    0,   57,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  133,    0,    0,    0,
    0,    0,   58,    0,    0,  133,    0,    0,    0,    0,
    0,    0,   55,   56,  133,  133,  133,  133,  133,    0,
    0,    0,    0,    0,    0,  133,    0,    0,    0,    0,
    0,    0,   54,   59,    0,    0,    0,    0,    0,  133,
  133,    0,  133,  133,  138,    0,  133,    0,  133,  133,
  133,  133,  133,  133,  133,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   57,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  138,    0,    0,    0,    0,
    0,  134,   55,   56,    0,    0,  139,    0,    0,    0,
  134,    0,    0,    0,    0,    0,    0,    0,    0,  134,
  134,  134,  134,  134,    0,    0,    0,  138,    0,    0,
  134,  133,    0,    0,    0,    0,    0,  139,    0,    0,
    0,    0,    0,   23,  134,  134,    0,  134,  134,    0,
    0,  134,   24,  134,  134,  134,  134,  134,  134,  134,
    0,   25,   26,   27,   28,   29,    0,    0,    0,  139,
    0,    0,   73,    0,    0,  194,    0,    0,    0,    0,
    0,  133,  133,    0,  108,    0,   40,   41,    0,   42,
   43,    0,    0,   45,    0,   46,   47,   48,   49,   50,
    0,   51,  194,    0,    0,    0,  195,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  134,    0,    0,    0,
    0,    0,  108,    0,  374,    0,   23,    0,    0,    0,
    0,    0,    0,  195,    0,   24,    0,    0,  193,    0,
  194,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,    0,    0,    0,    0,  111,    0,    0,   54,    0,
    0,    0,    0,    0,    0,  193,  134,  134,  138,   40,
   41,  195,   42,   43,    0,    0,   45,  138,   46,   47,
   48,   49,   50,    0,   51,    0,  138,  138,  138,  138,
  138,    0,    0,    0,    0,    0,    0,  138,    0,    0,
    0,    0,    0,  193,    0,    0,   58,    0,   55,   56,
  139,  138,  138,    0,  138,  138,    0,    0,  138,  139,
  138,  138,  138,  138,  138,    0,  138,    0,  139,  139,
  139,  139,  139,    0,    0,    0,    0,   59,    0,  139,
    0,   54,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  139,  139,    0,  139,  139,    0,    0,
  139,    0,  139,  139,  139,  139,  139,  518,  139,   57,
    0,    0,  179,  180,  181,    0,  182,  183,  184,    0,
  185,    0,    0,  138,    0,    0,    0,  286,  186,    0,
    0,   55,   56,    0,  187,    0,    0,    0,    0,  179,
  180,  181,  188,  182,  183,  184,    0,  185,    0,    0,
    0,    0,  494,  495,    0,  186,    0,    0,    0,    0,
    0,  187,    0,    0,    0,  139,    0,    0,    0,  188,
    0,    0,    0,  138,  138,    0,    0,  179,  180,  181,
    0,  182,  183,  184,    0,  185,    0,    0,    0,    0,
    0,   58,    0,  186,    0,    0,    0,    0,    0,  187,
    0,    0,    0,    0,    0,    0,    0,  188,    0,    0,
    0,    0,    0,    0,    0,  139,  139,    0,    0,    0,
    0,    0,   59,    0,    0,    0,    0,    0,    0,  630,
    0,    0,    0,  189,    0,    0,    0,    0,    0,  302,
    0,    0,    0,    0,    0,    0,    0,  190,  191,  192,
   23,    0,    0,    0,   57,  303,  212,    0,    0,   24,
  189,    0,    0,    0,  161,    0,    0,    0,   25,   26,
   27,   28,   29,    0,  190,  191,  192,    0,    0,    0,
  162,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  496,  497,  498,  499,    0,    0,    0,    0,  189,  500,
  501,  502,  503,  504,  505,  506,  507,  508,  509,    0,
    0,    0,  190,  191,  192,    0,    0,  304,  305,  306,
    0,  307,  308,    0,    0,    0,  309,   58,  310,    0,
    0,  311,  312,  313,  314,  315,  316,  317,  318,  319,
  320,    0,  321,  322,    0,  323,  324,  325,  326,  327,
  328,  329,  330,  331,  332,  333,  334,  335,   59,  254,
  336,  302,    0,    0,  337,  338,  339,  340,  341,    0,
  342,  343,  344,  345,  346,  347,  348,  303,  349,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  350,
   57,    0,    0,    0,    0,  209,  210,    0,    0,    0,
  351,  352,  353,  354,   24,  211,    0,  355,    0,    0,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  304,
  305,  306,    0,  307,  308,    0,    0,    0,  309,    0,
  310,    0,   58,  311,  312,  313,  314,  315,  316,  317,
  318,  319,  320,    0,  321,  322,    0,  323,  324,  325,
  326,  327,  328,  329,  330,  331,  332,  333,  334,  335,
    0,  252,  336,   59,    0,    0,  337,  338,  339,  340,
  341,  254,  342,  343,  344,  345,  346,  347,  348,    0,
  349,    0,    0,    0,    0,    0,    0,  254,    0,    0,
    0,  350,    0,    0,    0,   57,    0,    0,    0,    0,
    0,    0,  351,  352,  353,  354,    0,    0,    0,  355,
    0,  209,  409,    0,    0,    0,    0,    0,    0,    0,
   24,  410,    0,    0,    0,    0,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  254,
  254,  254,    0,  254,  254,    0,    0,    0,  254,   58,
  254,    0,    0,  254,  254,  254,  254,  254,  254,  254,
  254,  254,  254,    0,  254,  254,    0,  254,  254,  254,
  254,  254,  254,  254,  254,  254,  254,  254,  254,  254,
   59,  255,  254,  252,    0,    0,  254,  254,  254,  254,
  254,    0,  254,  254,  254,  254,  254,  254,  254,  252,
  254,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  254,   57,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  254,  254,  254,  254,   23,    0,    0,  254,
    0,    0,    0,    0,    0,   24,  815,    0,    0,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  252,  252,  252,    0,  252,  252,    0,    0,    0,
  252,    0,  252,    0,    0,  252,  252,  252,  252,  252,
  252,  252,  252,  252,  252,    0,  252,  252,    0,  252,
  252,  252,  252,  252,  252,  252,  252,  252,  252,  252,
  252,  252,    0,  253,  252,    0,    0,    0,  252,  252,
  252,  252,  252,  255,  252,  252,  252,  252,  252,  252,
  252,    0,  252,    0,    0,    0,    0,    0,    0,  255,
    0,    0,    0,  252,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  252,  252,  252,  252,    0,    0,
    0,  252,    0,   23,    0,    0,    0,    0,    0,    0,
    0,    0,   24,    0,    0,    0,    0,  741,    0,    0,
    0,   25,   26,   27,   28,   29,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  255,  255,  255,    0,  255,  255,    0,    0,    0,
  255,    0,  255,    0,    0,  255,  255,  255,  255,  255,
  255,  255,  255,  255,  255,    0,  255,  255,  646,  255,
  255,  255,  255,  255,  255,  255,  255,  255,  255,  255,
  255,  255,    0,    0,  255,  253,    0,   58,  255,  255,
  255,  255,  255,    0,  255,  255,  255,  255,  255,  255,
  255,  253,  255,   58,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  255,    0,    0,    0,    0,   59,    0,
    0,    0,    0,  896,  255,  255,  255,  255,    0,    0,
    0,  255,    0,    0,   59,    0,  862,    0,    0,    0,
    0,    0,   58,    0,    0,    0,    0,    0,    0,    0,
   57,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  253,  253,  253,   57,  253,  253,    0,
    0,    0,  253,   59,  253,    0,   58,  253,  253,  253,
  253,  253,  253,  253,  253,  253,  253,    0,  253,  253,
    0,  253,  253,  253,  253,  253,  253,  253,  253,  253,
  253,  253,  253,  253,    0,   57,  253,   59,    0,   58,
  253,  253,  253,  253,  253,    0,  253,  253,  253,  253,
  253,  253,  253,    0,  253,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  253,    0,    0,    0,   57,
   59,    0,   58,    0,    0,    0,  253,  253,  253,  253,
    0,    0,    0,  253,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   58,    0,    0,
    0,    0,   57,   59,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  179,  180,  181,    0,  182,  183,
  184,   23,  185,   58,    0,    0,    0,    0,   59,    0,
   24,    0,    0,  413,    0,   57,  187,   23,    0,   25,
   26,   27,   28,   29,  188,    0,   24,    0,    0,   58,
    0,    0,    0,    0,   59,   25,   26,   27,   28,   29,
   57,    0,    0,    0,    0,    0,    0,    0,    0,  179,
  180,  181,    0,  182,  183,  184,   23,  185,    0,    0,
   59,    0,    0,    0,    0,   24,   57,    0,  413,    0,
    0,  187,    0,    0,   25,   26,   27,   28,   29,  188,
    0,    0,    0,  179,  180,  181,  392,  182,  183,  184,
   23,  393,  268,    0,    0,    0,    0,    0,    0,   24,
  394,    0,  395,    0,    0,  187,    0,    0,   25,   26,
   27,   28,   29,  188,    0,    0,  179,  180,  181,  392,
  182,  183,  184,   23,  393,    0,    0,    0,    0,    0,
    0,    0,   24,  403,    0,  395,    0,    0,  187,    0,
    0,   25,   26,   27,   28,   29,  188,    0,    0,  179,
  180,  181,    0,  182,  183,  184,   23,  185,    0,    0,
    0,    0,    0,    0,    0,   24,    0,    0,  413,    0,
    0,  187,    0,    0,   25,   26,   27,   28,   29,  188,
  302,   23,    0,    0,    0,    0,    0,    0,    0,    0,
   24,    0,    0,    0,    0,    0,  303,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,  209,    0,    0,
    0,    0,    0,    0,    0,    0,   24,    0,    0,    0,
    0,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,  209,    0,    0,    0,    0,    0,    0,
    0,    0,   24,    0,    0,    0,    0,    0,    0,    0,
    0,   25,   26,   27,   28,   29,    0,    0,  304,  305,
  306,    0,  307,  308,    0,    0,    0,  309,    0,  310,
    0,    0,  311,  312,  313,  314,  315,  316,  317,  318,
  319,  320,    0,  321,  322,    0,  323,  324,  325,  326,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  366,
  366,  336,    0,    0,    0,  337,  338,  339,  340,  341,
    0,  342,  343,  344,  345,  346,  347,  348,    0,  349,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  350,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  351,  352,  353,  354,    0,    0,    0,  355,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  366,  366,  366,    0,  366,  366,    0,    0,    0,
  366,    0,  366,    0,    0,  366,  366,  366,  366,  366,
  366,  366,  366,  366,  366,    0,  366,  366,    0,  366,
  366,  366,  366,  366,  366,  366,  366,  366,  366,  366,
  366,  366,  340,  340,  366,    0,    0,    0,  366,  366,
  366,  366,  366,    0,  366,  366,  366,  366,  366,  366,
  366,    0,  366,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  366,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  366,  366,  366,  366,    0,    0,
    0,  366,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  340,  340,  340,    0,  340,  340,
    0,    0,    0,  340,    0,  340,    0,    0,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,    0,  340,
  340,    0,  340,  340,  340,  340,  340,  340,  340,  340,
  340,  340,  340,  340,  340,  321,  321,  340,    0,    0,
    0,  340,  340,  340,  340,  340,    0,  340,  340,  340,
  340,  340,  340,  340,    0,  340,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  340,  340,  340,
  340,    0,    0,    0,  340,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  321,  321,  321,
    0,  321,  321,    0,    0,    0,  321,    0,  321,    0,
    0,  321,  321,  321,  321,  321,  321,  321,  321,  321,
  321,    0,  321,  321,    0,  321,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  321,  321,  321,  327,  327,
  321,    0,    0,    0,  321,  321,  321,  321,  321,    0,
  321,  321,  321,  321,  321,  321,  321,    0,  321,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  321,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  321,  321,  321,  321,    0,    0,    0,  321,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  327,  327,  327,    0,  327,  327,    0,    0,    0,  327,
    0,  327,    0,    0,  327,  327,  327,  327,  327,  327,
  327,  327,  327,  327,    0,  327,  327,    0,  327,  327,
  327,  327,  327,  327,  327,  327,  327,  327,  327,  327,
  327,  322,  322,  327,    0,    0,    0,  327,  327,  327,
  327,  327,    0,  327,  327,  327,  327,  327,  327,  327,
    0,  327,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  327,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  327,  327,  327,  327,    0,    0,    0,
  327,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,  322,  322,    0,  322,  322,    0,
    0,    0,  322,    0,  322,    0,    0,  322,  322,  322,
  322,  322,  322,  322,  322,  322,  322,    0,  322,  322,
    0,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  322,  322,  328,  328,  322,    0,    0,    0,
  322,  322,  322,  322,  322,    0,  322,  322,  322,  322,
  322,  322,  322,    0,  322,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  322,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  322,  322,  322,  322,
    0,    0,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  328,  328,  328,    0,
  328,  328,    0,    0,    0,  328,    0,  328,    0,    0,
  328,  328,  328,  328,  328,  328,  328,  328,  328,  328,
    0,  328,  328,    0,  328,  328,  328,  328,  328,  328,
  328,  328,  328,  328,  328,  328,  328,  323,  323,  328,
    0,    0,    0,  328,  328,  328,  328,  328,    0,  328,
  328,  328,  328,  328,  328,  328,    0,  328,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  328,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  328,
  328,  328,  328,    0,    0,    0,  328,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  323,
  323,  323,    0,  323,  323,    0,    0,    0,  323,    0,
  323,    0,    0,  323,  323,  323,  323,  323,  323,  323,
  323,  323,  323,    0,  323,  323,    0,  323,  323,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  323,  323,
  333,  333,  323,    0,    0,    0,  323,  323,  323,  323,
  323,    0,  323,  323,  323,  323,  323,  323,  323,    0,
  323,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  323,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  323,  323,  323,  323,    0,    0,    0,  323,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  333,  333,  333,    0,  333,  333,    0,    0,
    0,  333,    0,  333,    0,    0,  333,  333,  333,  333,
  333,  333,  333,  333,  333,  333,    0,  333,  333,    0,
  333,  333,  333,  333,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  355,  355,  333,    0,    0,    0,  333,
  333,  333,  333,  333,    0,  333,  333,  333,  333,  333,
  333,  333,    0,  333,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  333,  333,  333,  333,    0,
    0,    0,  333,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  355,  355,  355,    0,  355,
  355,    0,    0,    0,  355,    0,  355,    0,    0,  355,
  355,  355,  355,  355,  355,  355,  355,  355,  355,    0,
  355,  355,    0,  355,  355,  355,  355,  355,  355,  355,
  355,  355,  355,  355,  355,  355,  351,  351,  355,    0,
    0,    0,  355,  355,  355,  355,  355,    0,  355,  355,
  355,  355,  355,  355,  355,    0,  355,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  355,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  355,  355,
  355,  355,    0,    0,    0,  355,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  351,  351,
  351,    0,  351,  351,    0,    0,    0,  351,    0,  351,
    0,    0,  351,  351,  351,  351,  351,  351,  351,  351,
  351,  351,    0,  351,  351,    0,  351,  351,  351,  351,
  351,  351,  351,  351,  351,  351,  351,  351,  351,  329,
  329,  351,    0,    0,    0,  351,  351,  351,  351,  351,
    0,  351,  351,  351,  351,  351,  351,  351,    0,  351,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  351,  351,  351,  351,    0,    0,    0,  351,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  329,  329,  329,    0,  329,  329,    0,    0,    0,
  329,    0,  329,    0,    0,  329,  329,  329,  329,  329,
  329,  329,  329,  329,  329,    0,  329,  329,    0,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  329,  329,
  329,  329,  324,  324,  329,    0,    0,    0,  329,  329,
  329,  329,  329,    0,  329,  329,  329,  329,  329,  329,
  329,    0,  329,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  329,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  329,  329,  329,  329,    0,    0,
    0,  329,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  324,  324,  324,    0,  324,  324,
    0,    0,    0,  324,    0,  324,    0,    0,  324,  324,
  324,  324,  324,  324,  324,  324,  324,  324,    0,  324,
  324,    0,  324,  324,  324,  324,  324,  324,  324,  324,
  324,  324,  324,  324,  324,  325,  325,  324,    0,    0,
    0,  324,  324,  324,  324,  324,    0,  324,  324,  324,
  324,  324,  324,  324,    0,  324,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  324,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  324,  324,  324,
  324,    0,    0,    0,  324,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  325,  325,  325,
    0,  325,  325,    0,    0,    0,  325,    0,  325,    0,
    0,  325,  325,  325,  325,  325,  325,  325,  325,  325,
  325,    0,  325,  325,    0,  325,  325,  325,  325,  325,
  325,  325,  325,  325,  325,  325,  325,  325,  330,  330,
  325,    0,    0,    0,  325,  325,  325,  325,  325,    0,
  325,  325,  325,  325,  325,  325,  325,    0,  325,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  325,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  325,  325,  325,  325,    0,    0,    0,  325,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  330,  330,  330,    0,  330,  330,    0,    0,    0,  330,
    0,  330,    0,    0,  330,  330,  330,  330,  330,  330,
  330,  330,  330,  330,    0,  330,  330,    0,  330,  330,
  330,  330,  330,  330,  330,  330,  330,  330,  330,  330,
  330,  338,  338,  330,    0,    0,    0,  330,  330,  330,
  330,  330,    0,  330,  330,  330,  330,  330,  330,  330,
    0,  330,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  330,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  330,  330,  330,  330,    0,    0,    0,
  330,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  338,  338,  338,    0,  338,  338,    0,
    0,    0,  338,    0,  338,    0,    0,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,    0,  338,  338,
    0,  338,  338,  338,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,  331,  331,  338,    0,    0,    0,
  338,  338,  338,  338,  338,    0,  338,  338,  338,  338,
  338,  338,  338,    0,  338,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  338,  338,  338,  338,
    0,    0,    0,  338,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  331,  331,  331,    0,
  331,  331,    0,    0,    0,  331,    0,  331,    0,    0,
  331,  331,  331,  331,  331,  331,  331,  331,  331,  331,
    0,  331,  331,    0,  331,  331,  331,  331,  331,  331,
  331,  331,  331,  331,  331,  331,  331,  334,  334,  331,
    0,    0,    0,  331,  331,  331,  331,  331,    0,  331,
  331,  331,  331,  331,  331,  331,    0,  331,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  331,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  331,
  331,  331,  331,    0,    0,    0,  331,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  334,
  334,  334,    0,  334,  334,    0,    0,    0,  334,    0,
  334,    0,    0,  334,  334,  334,  334,  334,  334,  334,
  334,  334,  334,    0,  334,  334,    0,  334,  334,  334,
  334,  334,  334,  334,  334,  334,  334,  334,  334,  334,
  352,  352,  334,    0,    0,    0,  334,  334,  334,  334,
  334,    0,  334,  334,  334,  334,  334,  334,  334,    0,
  334,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  334,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  334,  334,  334,  334,    0,    0,    0,  334,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  352,  352,  352,    0,  352,  352,    0,    0,
    0,  352,    0,  352,    0,    0,  352,  352,  352,  352,
  352,  352,  352,  352,  352,  352,    0,  352,  352,    0,
  352,  352,  352,  352,  352,  352,  352,  352,  352,  352,
  352,  352,  352,  326,  326,  352,    0,    0,    0,  352,
  352,  352,  352,  352,    0,  352,  352,  352,  352,  352,
  352,  352,    0,  352,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  352,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  352,  352,  352,  352,    0,
    0,    0,  352,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  326,  326,  326,    0,  326,
  326,    0,    0,    0,  326,    0,  326,    0,    0,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  326,    0,
  326,  326,    0,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  326,  326,  332,  332,  326,    0,
    0,    0,  326,  326,  326,  326,  326,    0,  326,  326,
  326,  326,  326,  326,  326,    0,  326,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  326,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  326,  326,
  326,  326,    0,    0,    0,  326,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  332,  332,
  332,    0,  332,  332,    0,    0,    0,  332,    0,  332,
    0,    0,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,    0,  332,  332,    0,  332,  332,  332,  332,
  332,  332,  332,  332,  332,  332,  332,  332,  332,  335,
  335,  332,    0,    0,    0,  332,  332,  332,  332,  332,
    0,  332,  332,  332,  332,  332,  332,  332,    0,  332,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  332,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  332,  332,  332,  332,    0,    0,    0,  332,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  335,  335,  335,    0,  335,  335,    0,    0,    0,
  335,    0,  335,    0,    0,  335,  335,  335,  335,  335,
  335,  335,  335,  335,  335,    0,  335,  335,    0,  335,
  335,  335,  335,  335,  335,  335,  335,  335,  335,  335,
  335,  335,  336,  336,  335,    0,    0,    0,  335,  335,
  335,  335,  335,    0,  335,  335,  335,  335,  335,  335,
  335,    0,  335,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  335,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  335,  335,  335,  335,    0,    0,
    0,  335,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  336,  336,  336,    0,  336,  336,
    0,    0,    0,  336,    0,  336,    0,    0,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,    0,  336,
  336,    0,  336,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,  336,  336,  337,  337,  336,    0,    0,
    0,  336,  336,  336,  336,  336,    0,  336,  336,  336,
  336,  336,  336,  336,    0,  336,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  336,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  336,  336,  336,
  336,    0,    0,    0,  336,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  337,  337,  337,
    0,  337,  337,    0,    0,    0,  337,    0,  337,    0,
    0,  337,  337,  337,  337,  337,  337,  337,  337,  337,
  337,    0,  337,  337,    0,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  303,    0,
  337,    0,    0,    0,  337,  337,  337,  337,  337,    0,
  337,  337,  337,  337,  337,  337,  337,    0,  337,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  337,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  337,  337,  337,  337,    0,    0,    0,  337,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,  305,  306,    0,  307,  308,    0,    0,    0,  309,
    0,  310,    0,    0,  311,  312,  313,  314,  315,  316,
  317,  318,  319,  320,    0,  321,  322,    0,  323,  324,
  325,  326,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  258,    0,  336,    0,    0,    0,  337,  338,  339,
  340,  341,    0,  342,  343,  344,  345,  346,  347,  348,
    0,  349,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  350,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  351,  352,  353,  354,    0,    0,    0,
  355,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  258,  258,  258,    0,  258,  258,    0,
    0,    0,  258,    0,  258,    0,    0,  258,  258,  258,
  258,  258,  258,  258,  258,  258,  258,    0,  258,  258,
    0,  258,  258,  258,  258,  258,  258,  258,  258,  258,
  258,  258,  258,  258,  259,    0,  258,    0,    0,    0,
  258,  258,  258,  258,  258,    0,  258,  258,  258,  258,
  258,  258,  258,    0,  258,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  258,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  258,  258,  258,  258,
    0,    0,    0,  258,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  259,  259,  259,    0,
  259,  259,    0,    0,    0,  259,    0,  259,    0,    0,
  259,  259,  259,  259,  259,  259,  259,  259,  259,  259,
    0,  259,  259,    0,  259,  259,  259,  259,  259,  259,
  259,  259,  259,  259,  259,  259,  259,  260,    0,  259,
    0,    0,    0,  259,  259,  259,  259,  259,    0,  259,
  259,  259,  259,  259,  259,  259,    0,  259,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  259,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  259,
  259,  259,  259,    0,    0,    0,  259,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  260,
  260,  260,    0,  260,  260,    0,    0,    0,  260,    0,
  260,    0,    0,  260,  260,  260,  260,  260,  260,  260,
  260,  260,  260,    0,  260,  260,    0,  260,  260,  260,
  260,  260,  260,  260,  260,  260,  260,  260,  260,  260,
  261,    0,  260,    0,    0,    0,  260,  260,  260,  260,
  260,    0,  260,  260,  260,  260,  260,  260,  260,    0,
  260,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  260,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  260,  260,  260,  260,    0,    0,    0,  260,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  261,  261,  261,    0,  261,  261,    0,    0,
    0,  261,    0,  261,    0,    0,  261,  261,  261,  261,
  261,  261,  261,  261,  261,  261,    0,  261,  261,    0,
  261,  261,  261,  261,  261,  261,  261,  261,  261,  261,
  261,  261,  261,    0,    0,  261,    0,    0,    0,  261,
  261,  261,  261,  261,    0,  261,  261,  261,  261,  261,
  261,  261,    0,  261,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  261,    0,    0,    0,    0,    0,
    0,    0,    0,  307,    0,  261,  261,  261,  261,    0,
  310,    0,  261,  311,  312,  313,  314,  315,  316,  317,
  318,  319,  320,    0,  321,  322,    0,  323,  324,  325,
  326,  327,  328,  329,  330,  331,  332,  333,  334,  335,
    0,    0,  336,    0,    0,    0,  337,  338,  339,  340,
  341,    0,  342,  343,  344,  345,  346,  347,  348,    0,
  349,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  350,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  351,  352,  353,  354,    0,    0,    0,  355,
  };
  protected static readonly short [] yyCheck = {             6,
   65,   42,   61,   40,    6,  169,   33,   42,   73,   16,
  544,   33,   60,   44,  244,   44,  356,  170,  123,   40,
  740,  123,   40,  276,  277,  278,   40,   40,  123,  123,
  123,  123,  123,  787,  123,  119,  557,  280,  251,   44,
   60,  276,  295,  256,  597,   61,  281,  106,   44,   40,
   57,  805,    0,   44,   44,   20,  274,   64,   65,   42,
  122,   40,   65,  130,    0,   44,   73,   41,   44,   76,
   44,   91,   44,   42,   76,  123,   42,  194,  195,   62,
   87,   61,   44,   86,  168,  323,  266,  841,   93,   96,
   97,  244,   40,  813,  125,  272,  125,  104,  313,  164,
   62,  254,  109,  123,   40,  112,  286,  114,  323,  176,
  172,  118,  114,  175,  121,   86,   61,   88,   89,  121,
  127,   86,   91,   88,  131,   90,  133,   93,   42,  125,
  651,  198,   61,  200,  125,  125,  379,  367,  691,  146,
  147,  694,  149,  396,  379,  313,  125,  270,  271,  125,
  124,  268,  257,  125,  119,  323,  369,  128,  129,  291,
  292,  132,  260,  134,  379,    6,   42,   61,   44,  253,
  177,  178,  274,  291,  292,   16,  930,  295,  296,  274,
  274,  274,  274,  274,  524,  274,    0,   42,  418,   44,
  252,  323,  199,  260,  201,   42,  360,   44,  362,  306,
  268,  269,   61,  321,  272,  273,  273,  275,  275,  291,
  292,  379,  281,  295,  296,   60,    0,  370,   42,  226,
   44,  289,  290,   64,   65,  746,  333,  334,  263,   40,
   41,   41,   73,   44,   44,   76,  243,   40,  273,  241,
  288,   40,  307,  274,  264,  274,   91,  274,  307,   40,
  277,  272,  274,  273,  272,  262,  324,  257,  272,  272,
   42,  268,  282,  283,  284,  285,  286,   40,  433,  276,
  277,  112,  437,  114,  315,  440,   41,   40,  123,   44,
  121,   40,   41,   41,  449,   44,   44,  312,  295,  314,
  811,  544,   41,  546,  353,   44,  321,  550,  295,  296,
  307,  822,  823,  310,  311,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  325,  326,
  268,  269,  257,  513,  272,  332,  333,  275,   41,  336,
  864,   44,  268,  269,  291,  292,  272,  273,  591,  275,
  861,  289,  290,  380,  351,  352,  353,   41,  355,   41,
   44,  353,   44,  289,  290,  876,  520,   41,  879,  273,
   44,  381,   41,  428,   41,   44,   42,   44,   44,  376,
  560,  561,   41,   42,   41,  539,  324,   44,   41,  293,
   61,   44,  903,  904,   41,  426,  274,   44,  324,  553,
    0,   41,   42,  307,  308,  916,  310,  311,   41,  274,
  314,   44,  316,  317,  318,  319,  320,  260,  322,  397,
  398,  399,  400,  401,  402,  403,  404,  405,  406,  264,
  427,  428,   42,  260,   44,  427,  433,   42,  273,   44,
  437,  621,  622,  440,  624,   40,  443,  282,  283,  284,
  285,  286,  449,  450,   42,  452,   44,   41,   42,   41,
   42,   41,   42,  123,  268,  269,  463,  516,  272,  273,
  257,  275,  469,   41,   42,  379,  307,  657,  632,   41,
   42,   41,   42,  267,  267,  289,  290,  306,  642,  379,
  644,  123,  274,  123,  268,  269,  493,   40,  272,  273,
  123,  275,   41,  257,   40,  323,   40,   40,   58,   44,
   58,   58,   61,  510,  141,  289,  290,  514,  515,  516,
  324,   62,  353,  515,  516,  429,  430,   40,  257,  379,
   44,   40,  379,  776,  777,   40,   44,  534,  535,  536,
  720,   44,  260,  723,  724,  380,  257,  544,   58,  546,
  324,  420,  332,  550,   61,   60,  313,  257,  379,   44,
  257,    0,   60,  387,  387,  562,  193,  194,  195,  379,
  562,  626,  387,  816,  728,   44,  379,   44,   40,  124,
  214,  208,  273,  737,   44,   44,   91,   44,   44,   44,
   44,   44,  387,   91,  257,  387,  427,  428,  595,  387,
  597,  387,    0,  600,  601,  602,  603,  604,  605,  606,
  607,  608,  609,  610,  611,  260,  387,  387,  123,  387,
  247,  864,  387,  803,   42,  123,  623,    0,  625,  626,
   62,  387,  257,  379,  379,    0,  263,  264,  265,  257,
  387,  268,  716,  717,  387,  387,  257,  379,   44,  266,
   40,  794,  795,   44,  257,   44,  653,   44,  285,   44,
   44,   44,   44,    0,   44,   44,   44,   44,  268,  269,
   44,  257,  272,  273,   44,  275,   44,  304,   44,  306,
   44,  308,   44,  514,  515,  516,   44,   44,  743,  289,
  290,   44,   44,  424,  691,  387,  257,  694,   91,   44,
  327,  328,  329,  330,  331,  848,  849,  334,  851,  852,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
  347,  348,  796,  797,  324,  799,  800,   44,  725,   44,
   44,  562,   44,  725,   44,   44,   44,   44,   44,   44,
  257,   44,   44,  740,  741,   44,  889,  890,  891,  383,
   44,   44,  257,  258,  259,  382,  261,  262,  263,   44,
  265,   44,   91,  260,   44,  908,  264,  272,  273,   42,
   44,  387,  387,  379,  279,  273,  850,   44,  379,  853,
  854,   44,  287,  379,  282,  283,  284,  285,  286,  423,
   44,   44,  623,   44,  625,  626,   44,   40,  432,  327,
  387,  435,  436,  379,  438,  439,  257,  441,  442,  257,
  444,  445,  446,  447,  448,  379,  813,  451,  892,  453,
  454,  455,  456,  820,  257,  817,    0,   44,   93,  268,
  269,  260,   40,  272,  273,   40,  275,   40,  465,  257,
  258,  259,  380,  261,  262,  263,   40,  265,  327,  327,
  289,  290,   60,  257,  272,  273,    0,  257,  276,   40,
  387,  279,  327,  387,  387,   44,    0,  864,   10,  287,
  268,  269,   91,  378,  272,  273,  208,  275,  512,  207,
  272,  379,   20,   91,  272,  324,  272,  392,  393,  394,
  546,  289,  290,  104,  725,  268,  269,  272,  238,  272,
  273,  272,  275,  268,  269,  277,  533,  272,  273,   61,
  275,  280,  243,  910,  859,  123,  289,  290,  423,  365,
  776,  918,  919,  920,  289,  290,  324,  419,  226,  777,
  564,  268,  269,  359,  568,  272,  273,  571,  275,    0,
  574,  820,  359,  420,  713,  813,  580,  581,  550,  583,
  420,  324,  289,  290,  624,   -1,   -1,   -1,   -1,  324,
  378,  588,  589,  590,    0,  592,  593,   -1,   -1,   -1,
   -1,   -1,  599,   -1,  392,  393,  394,   40,  612,  613,
  614,  619,  620,   -1,  257,  258,  259,  324,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,  628,   -1,   -1,   -1,   -1,  279,   -1,  635,    0,
   -1,   -1,   -1,   -1,  287,   -1,   -1,  655,  656,   -1,
   -1,   -1,   -1,   -1,  658,   42,  660,  661,   91,  663,
  664,   -1,  666,  667,   -1,  669,  670,  671,  672,  673,
   -1,   -1,  676,   60,  678,  679,  680,  681,    0,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,  692,   -1,
  123,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,  696,
   -1,  279,   -1,   -1,   91,   -1,   -1,   -1,   -1,  287,
  718,  719,   -1,  721,  722,   -1,   -1,  714,   40,   41,
   42,   -1,   44,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,   -1,   -1,   -1,   -1,  123,   -1,   60,  743,
   62,  749,   -1,   -1,   -1,  289,  290,  751,   -1,   -1,
  754,   -1,   -1,  757,  268,  269,  760,   -1,  272,  273,
   -1,  275,  766,  767,   -1,  769,   -1,   -1,   -1,   91,
   -1,   93,   -1,   -1,   -1,  289,  290,  774,  775,   -1,
  324,   -1,   -1,   -1,  788,  789,  790,   -1,   -1,   -1,
  798,   -1,   -1,  801,  802,   -1,   -1,   -1,   -1,   -1,
  378,  123,   -1,  125,   -1,   -1,   -1,   -1,   -1,  806,
  324,   -1,   -1,  817,  392,  393,  394,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,  268,  269,  272,
  273,  272,  273,   -1,  275,  423,  279,  855,   -1,  846,
   -1,   -1,   -1,   -1,  287,   -1,   -1,   -1,  289,  290,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,  289,  290,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,  324,   41,   -1,   -1,   44,   -1,   -1,
  287,   -1,  899,  900,  901,   -1,   -1,  268,  269,    0,
   -1,  272,  273,   60,  275,   -1,   -1,    0,  324,   -1,
   -1,   -1,   -1,   -1,   -1,  922,   -1,   -1,  289,  290,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   91,  378,  268,  269,   -1,   -1,
  272,  273,  274,  275,  276,   -1,   -1,  279,   -1,  392,
  393,  394,   -1,  324,   -1,  287,   -1,  289,  290,   -1,
   -1,  293,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,  378,  314,  315,  316,  317,  318,  319,  320,   -1,
  322,   -1,  324,   -1,   -1,  392,  393,  394,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   41,  396,   -1,   44,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,  423,   -1,  425,  426,   -1,   -1,  429,  430,   -1,
  257,  258,  259,   -1,  261,  262,  263,  264,  265,   -1,
   -1,   -1,   -1,   91,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   60,  282,  283,  284,  285,  286,
  287,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,
  260,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
  307,  308,   42,  310,  311,   91,  276,  314,   -1,  316,
  317,  318,  319,  320,   -1,  322,   -1,  268,  269,   -1,
   60,  272,  273,   -1,  275,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   60,   -1,   -1,   -1,  123,  289,  290,
   -1,   -1,   -1,   -1,   -1,   -1,  289,  290,  318,  319,
  320,   91,   -1,   -1,   -1,  325,  326,   -1,  328,  329,
  330,  331,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   -1,  378,  379,  324,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  324,   -1,  123,   60,  392,  393,  394,   40,   41,
   60,   -1,   44,   -1,   -1,   -1,  123,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   60,   -1,   -1,
   62,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
  279,   91,  429,  430,   -1,   42,   -1,   44,  287,  257,
  258,  259,   -1,  261,  262,  263,  264,  265,   91,   -1,
   -1,   93,   -1,   60,  272,  273,   -1,  123,   -1,   -1,
   -1,  279,   -1,  123,  282,  283,  284,  285,  286,  287,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,
  123,  123,  125,  125,   91,   -1,   -1,   -1,  264,  307,
  308,   -1,  310,  311,   -1,   -1,  314,  273,  316,  317,
  318,  319,  320,   -1,  322,   40,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,  123,   -1,  294,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,  264,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  287,   -1,   -1,
  378,  379,   -1,  293,   -1,  282,  283,  284,  285,  286,
   -1,   60,   -1,   -1,  392,  393,  394,  307,  308,   -1,
  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,
  320,   -1,  322,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   91,   -1,  264,   -1,   -1,  273,   40,   -1,
   -1,  429,  430,  273,   -1,  257,  282,  283,  284,  285,
  286,  264,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  273,  273,  274,   -1,  123,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,  362,   -1,   -1,  378,  379,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,  392,  393,  394,   -1,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   60,   -1,   -1,   -1,   -1,   -1,
  287,   -1,   -1,   -1,   -1,   -1,   -1,  347,  348,   -1,
   -1,   -1,   -1,  335,  336,  337,  362,  339,  340,  429,
  430,   -1,  344,   -1,  346,   91,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   40,   -1,  378,  123,  273,  274,
  382,  383,  384,  385,  386,  387,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   41,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,  378,   -1,   -1,   -1,  264,  418,  419,  420,  421,
   -1,   60,   -1,  425,  273,  392,  393,  394,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   91,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  273,  274,  378,  123,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   40,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,  362,   -1,   -1,   -1,   -1,  264,  265,
   -1,   -1,   -1,  418,  419,  420,  421,  273,  274,   -1,
  425,   -1,   -1,   -1,   41,   -1,  282,  283,  284,  285,
  286,   -1,  125,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   60,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   91,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   40,  396,   -1,   -1,  273,  274,   -1,
   -1,   -1,   -1,   -1,   -1,  407,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  418,  419,  420,  421,
   -1,   -1,   41,  425,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   60,   -1,   -1,   -1,  294,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,  260,  344,   -1,
  346,   -1,   91,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  276,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   40,   -1,  378,   -1,  123,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,  273,  274,  318,  319,  320,   -1,   -1,
   -1,  407,  325,  326,   -1,  328,  329,  330,  331,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,  264,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  125,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  294,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  273,  274,  378,   40,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,  264,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,  407,   -1,  278,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,  260,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  276,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,   -1,
   -1,  378,   40,  273,  274,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,
  407,  325,  326,   -1,  328,  329,  330,  331,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,   -1,  378,   40,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,  273,  274,  273,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,  293,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,
  125,  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,
  319,  320,   -1,  322,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,  274,  378,   40,   -1,
  379,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
  429,  430,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,  260,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  276,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,   -1,
  378,   40,  273,  274,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,  407,
  325,  326,   -1,  328,  329,  330,  331,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,   -1,   -1,  378,   40,   -1,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,  273,  274,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,  291,  292,   -1,  425,  295,  296,  297,  298,  299,
  300,  301,  302,  303,  304,  305,   -1,   -1,   -1,   -1,
   -1,   -1,  312,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,  321,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   91,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,  123,   -1,  125,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   60,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   91,   -1,  378,
   -1,  273,  274,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,  123,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,  418,
  419,  420,  421,   -1,   60,   -1,  425,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   91,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   60,  378,  123,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,  407,   91,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,  123,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,  123,   -1,   60,  293,
   -1,   -1,   -1,   60,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  307,  308,   -1,  310,  311,  312,   -1,
  314,   -1,  316,  317,  318,  319,  320,   -1,  322,   91,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  333,
  334,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,
   -1,  123,   -1,  279,   -1,   -1,  123,   -1,   -1,   -1,
   -1,  287,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  379,   -1,   -1,   -1,   -1,
   -1,  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,
  316,  317,  318,  319,  320,   -1,  322,   -1,   -1,  264,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,  429,  430,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  123,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  309,  282,  283,  284,  285,  286,
   -1,   -1,  378,  379,   -1,   -1,  293,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  392,  393,  394,   -1,
  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,  316,
  317,  318,  319,  320,  321,  322,   -1,   -1,   91,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,  264,   -1,   60,
   -1,  273,   -1,  429,  430,   -1,  273,   -1,   -1,   -1,
  282,  283,  284,  285,  286,  282,  283,  284,  285,  286,
  123,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,   -1,   -1,  309,   -1,  306,
  307,  308,  379,  310,  311,   -1,   -1,  314,   -1,  316,
  317,  318,  319,  320,   -1,  322,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,  429,  430,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,
   -1,   -1,  379,   91,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,   60,   -1,  314,   -1,  316,  317,
  318,  319,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,  264,  429,  430,   -1,   -1,   60,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,  123,   -1,   -1,
  293,  379,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,   -1,   -1,  264,  307,  308,   -1,  310,  311,   -1,
   -1,  314,  273,  316,  317,  318,  319,  320,  321,  322,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,  123,
   -1,   -1,  293,   -1,   -1,   60,   -1,   -1,   -1,   -1,
   -1,  429,  430,   -1,   42,   -1,  307,  308,   -1,  310,
  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,  320,
   -1,  322,   60,   -1,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   42,   -1,   44,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   91,   -1,  273,   -1,   -1,  123,   -1,
   60,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,  379,   -1,
   -1,   -1,   -1,   -1,   -1,  123,  429,  430,  264,  307,
  308,   91,  310,  311,   -1,   -1,  314,  273,  316,  317,
  318,  319,  320,   -1,  322,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   60,   -1,  429,  430,
  264,  307,  308,   -1,  310,  311,   -1,   -1,  314,  273,
  316,  317,  318,  319,  320,   -1,  322,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   91,   -1,  293,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,   -1,
  314,   -1,  316,  317,  318,  319,  320,  125,  322,  123,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,  379,   -1,   -1,   -1,  272,  273,   -1,
   -1,  429,  430,   -1,  279,   -1,   -1,   -1,   -1,  257,
  258,  259,  287,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,  261,  262,   -1,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,   -1,   -1,  379,   -1,   -1,   -1,  287,
   -1,   -1,   -1,  429,  430,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   60,   -1,  273,   -1,   -1,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  429,  430,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  378,   -1,   -1,   -1,   -1,   -1,  257,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,  393,  394,
  264,   -1,   -1,   -1,  123,  273,  125,   -1,   -1,  273,
  378,   -1,   -1,   -1,  278,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,  392,  393,  394,   -1,   -1,   -1,
  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,  400,  401,  402,   -1,   -1,   -1,   -1,  378,  408,
  409,  410,  411,  412,  413,  414,  415,  416,  417,   -1,
   -1,   -1,  392,  393,  394,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   60,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   91,  125,
  378,  257,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,  273,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
  123,   -1,   -1,   -1,   -1,  264,  265,   -1,   -1,   -1,
  418,  419,  420,  421,  273,  274,   -1,  425,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   60,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  125,  378,   91,   -1,   -1,  382,  383,  384,  385,
  386,  257,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,  407,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   60,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   91,  125,  378,  257,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,  273,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,  264,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  125,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,  257,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   41,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,   -1,  378,  257,   -1,   60,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,  273,  396,   60,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   41,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   91,   -1,   93,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,  123,  339,  340,   -1,
   -1,   -1,  344,   91,  346,   -1,   60,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  123,  378,   91,   -1,   60,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,  123,
   91,   -1,   60,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,   -1,  123,   91,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  264,  265,   60,   -1,   -1,   -1,   -1,   91,   -1,
  273,   -1,   -1,  276,   -1,  123,  279,  264,   -1,  282,
  283,  284,  285,  286,  287,   -1,  273,   -1,   -1,   60,
   -1,   -1,   -1,   -1,   91,  282,  283,  284,  285,  286,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,  264,  265,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  273,  123,   -1,  276,   -1,
   -1,  279,   -1,   -1,  282,  283,  284,  285,  286,  287,
   -1,   -1,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,  123,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  274,   -1,  276,   -1,   -1,  279,   -1,   -1,  282,  283,
  284,  285,  286,  287,   -1,   -1,  257,  258,  259,  260,
  261,  262,  263,  264,  265,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  274,   -1,  276,   -1,   -1,  279,   -1,
   -1,  282,  283,  284,  285,  286,  287,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,  264,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  276,   -1,
   -1,  279,   -1,   -1,  282,  283,  284,  285,  286,  287,
  257,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,  335,  336,
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

#line 1372 "Iril/IR/IR.jay"

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
  public const int IMMARG = 430;
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
