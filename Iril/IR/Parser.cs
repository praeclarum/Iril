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
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage visibility_style global_kind type",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value ',' SECTION STRING",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' SECTION STRING",
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility_style function_addr global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility_style global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value ',' ALIGN INTEGER metadata_kvs",
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
//t    "type : type addrspace '*'",
//t    "type : type addrspace '*' ALIGN INTEGER",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "addrspace :",
//t    "addrspace : ADDRSPACE '(' INTEGER ')'",
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
//t    "function_addr_type : UNNAMED_ADDR",
//t    "function_addr_type : LOCAL_UNNAMED_ADDR",
//t    "function_addr : function_addr_type addrspace",
//t    "function_addr : function_addr_type addrspace EXTERNALLY_INITIALIZED",
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
//t    "metadata_value_arg : type SYMBOL",
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
//t    "instruction : ATOMICRMW ADD type value ',' type value SEQ_CST ',' ALIGN INTEGER",
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
//t    "instruction : LOAD ATOMIC type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LOAD VOLATILE type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LOAD ATOMIC VOLATILE type ',' typed_pointer_value ',' ALIGN INTEGER",
//t    "instruction : LOAD ATOMIC VOLATILE type ',' typed_pointer_value MONOTONIC ',' ALIGN INTEGER",
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
//t    "instruction : ATOMICRMW SUB type value ',' type value SEQ_CST ',' ALIGN INTEGER",
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
    "PERSONALITY","SRET","CLEANUP","EXTERNALLY_INITIALIZED","NONNULL",
    "NOCAPTURE","WRITEONLY","READONLY","READNONE","HIDDEN","BYVAL",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","UNWIND",
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST","DSO_LOCAL",
    "DSO_PREEMPTABLE","RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME",
    "CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD",
    "NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV",
    "UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR","XOR",
    "EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","ADDRSPACE","TRUNC",
    "ZEXT","SEXT","FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP",
    "SITOFP","PTRTOINT","INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ",
    "NE","UGT","UGE","ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ",
    "OGT","OGE","OLT","OLE","ONE","ORD","UEQ","UNE","UNO","FAST","PHI",
    "SELECT","CALL","TAIL","VA_ARG","ASM","SIDEEFFECT","LANDINGPAD",
    "CATCH","CATCHPAD","CLEANUPPAD","NOUNDEF","IMMARG","ATOMIC",
    "MONOTONIC",
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
#line 64 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 68 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 72 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 76 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 80 "Iril/IR/IR.jay"
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
#line 100 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 104 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 113 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 125 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 19:
#line 129 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-3+yyTop], isConstant: (bool)yyVals[-3+yyTop]);
    }
  break;
case 20:
#line 133 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 137 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 141 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 145 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 24:
#line 149 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 25:
#line 153 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 157 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 27:
#line 161 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 28:
#line 165 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 29:
#line 169 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 30:
#line 173 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 31:
#line 177 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 32:
#line 181 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 33:
#line 185 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 34:
#line 189 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 35:
#line 193 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 36:
#line 197 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 37:
#line 198 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 202 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 203 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 40:
#line 204 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 41:
#line 205 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 42:
#line 206 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 207 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 208 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
#line 209 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 46:
#line 210 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 47:
#line 214 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 48:
#line 218 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 49:
  case_49();
  break;
case 50:
  case_50();
  break;
case 51:
#line 235 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 52:
#line 236 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 53:
#line 237 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 54:
#line 241 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 55:
#line 245 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 66:
#line 274 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 278 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 285 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 69:
#line 289 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 70:
#line 293 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 71:
#line 297 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 72:
#line 301 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 91:
#line 335 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 92:
#line 339 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 343 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 94:
#line 350 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 95:
#line 354 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 97:
#line 359 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 100:
#line 365 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 101:
#line 366 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 102:
#line 367 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 103:
#line 368 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 104:
#line 372 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 105:
#line 376 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 106:
#line 380 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-2+yyTop], 0);
    }
  break;
case 107:
#line 384 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-4+yyTop], 0);
    }
  break;
case 108:
#line 388 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 392 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 110:
#line 396 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 113:
#line 408 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 114:
#line 412 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 116:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 117:
  case_117();
  break;
case 118:
  case_118();
  break;
case 128:
#line 452 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 129:
#line 456 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 130:
#line 460 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 131:
#line 464 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 132:
#line 468 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 133:
#line 475 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 134:
#line 479 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 135:
#line 483 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 140:
#line 494 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 143:
#line 506 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 144:
#line 510 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 145:
#line 514 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 146:
#line 518 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 147:
#line 522 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 532 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 152:
#line 533 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 153:
#line 540 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 544 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 551 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 555 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 559 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 563 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-2+yyTop]);
    }
  break;
case 159:
#line 567 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 160:
#line 571 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 162:
#line 579 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 583 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 164:
#line 584 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 165:
#line 585 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoUndef; }
  break;
case 166:
#line 586 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ImmediateArgument; }
  break;
case 167:
#line 587 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 168:
#line 588 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 169:
#line 589 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 170:
#line 590 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 171:
#line 591 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 172:
#line 592 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 173:
#line 593 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 174:
#line 594 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 175:
#line 595 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 176:
#line 596 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 177:
#line 597 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 178:
#line 601 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 179:
#line 605 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 189:
#line 633 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 190:
#line 634 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 191:
#line 635 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 192:
#line 636 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 193:
#line 637 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 194:
#line 638 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 195:
#line 639 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 196:
#line 640 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 197:
#line 641 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 198:
#line 642 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 199:
#line 646 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 200:
#line 647 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 201:
#line 648 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 202:
#line 649 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 203:
#line 650 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 204:
#line 651 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 205:
#line 652 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 206:
#line 653 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 207:
#line 654 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 208:
#line 655 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 209:
#line 656 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 210:
#line 657 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 211:
#line 658 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 212:
#line 659 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 213:
#line 660 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 214:
#line 661 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 215:
#line 665 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 219:
#line 675 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 220:
#line 679 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 221:
#line 683 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 222:
#line 687 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 223:
#line 691 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 224:
#line 695 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 225:
#line 699 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 226:
#line 703 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 227:
#line 707 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 228:
#line 711 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 230:
#line 719 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 231:
#line 720 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 232:
#line 721 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 233:
#line 722 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 234:
#line 723 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 235:
#line 724 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 236:
#line 725 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 237:
#line 726 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 238:
#line 727 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 239:
#line 734 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 741 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 745 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 242:
#line 752 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 759 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 770 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 778 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 248:
#line 785 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 789 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 800 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 804 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 811 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 815 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 822 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 256:
#line 826 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 257:
#line 833 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 837 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 259:
#line 841 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 845 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 261:
#line 852 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 856 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 863 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 867 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 871 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 875 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 883 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 269:
#line 884 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 270:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 895 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 902 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 273:
#line 906 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 274:
#line 910 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 275:
#line 914 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 276:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 277:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 279:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 938 "Iril/IR/IR.jay"
  {
        yyVal = new SymbolValue ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 942 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 283:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 284:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 285:
#line 954 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 293:
#line 974 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 978 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 984 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 296:
#line 991 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 995 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1002 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1020 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 305:
#line 1027 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1034 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 313:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 315:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 320:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 321:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 322:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 323:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 324:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 326:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 327:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1129 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 329:
#line 1133 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 330:
#line 1137 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 331:
#line 1141 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 332:
#line 1145 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 333:
#line 1149 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 334:
#line 1153 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 335:
#line 1157 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 336:
#line 1161 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 337:
#line 1165 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 338:
#line 1169 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 339:
#line 1173 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 340:
#line 1177 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 341:
#line 1181 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 342:
#line 1185 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 343:
#line 1189 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 344:
#line 1193 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 345:
#line 1197 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 346:
#line 1201 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1205 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1209 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1213 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1217 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1221 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1225 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1229 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1233 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1237 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1241 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1245 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1249 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1253 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1257 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1261 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1265 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1269 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1273 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 365:
#line 1277 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1281 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: false);
    }
  break;
case 367:
#line 1285 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: true);
    }
  break;
case 368:
#line 1289 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: false);
    }
  break;
case 369:
#line 1293 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 370:
#line 1297 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-6+yyTop], (TypedValue)yyVals[-4+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 371:
#line 1301 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 372:
#line 1305 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 373:
#line 1309 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 374:
#line 1313 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 375:
#line 1317 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 376:
#line 1321 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 377:
#line 1325 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 378:
#line 1329 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 379:
#line 1333 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 380:
#line 1337 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 381:
#line 1341 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 382:
#line 1345 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 383:
#line 1349 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 384:
#line 1353 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 385:
#line 1357 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 386:
#line 1361 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 387:
#line 1365 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 388:
#line 1369 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 389:
#line 1373 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 390:
#line 1377 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 391:
#line 1381 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 392:
#line 1385 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 393:
#line 1389 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 394:
#line 1393 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 395:
#line 1397 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 396:
#line 1401 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 397:
#line 1405 "Iril/IR/IR.jay"
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
#line 82 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 87 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 92 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.AddGlobalVariable(g);
    }

void case_15()
#line 106 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 115 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_49()
#line 223 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_50()
#line 228 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_117()
#line 425 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_118()
#line 430 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    6,    6,    6,   11,   11,   10,   10,   10,
   10,   10,   10,   10,   10,   10,   17,   14,    9,    9,
   18,   18,   18,   18,   18,   19,   22,   22,   23,   24,
   24,   24,   24,   24,   24,   16,   16,    8,    8,    8,
    8,    8,   26,   26,   26,    7,    7,   28,   28,   28,
   28,   28,   28,   28,   28,   28,   28,   28,   28,   28,
    3,    3,    3,   29,   29,   30,   30,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   32,   32,   31,   31,   33,   33,    4,    4,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   34,   34,   34,
   34,   34,   41,   41,   41,   41,   41,   41,   41,   39,
   45,   45,    5,    5,    5,    5,    5,   46,   46,   46,
   35,   35,   47,   47,   48,   48,   48,   48,   48,   48,
   42,   42,   40,   40,   40,   40,   40,   40,   40,   40,
   40,   40,   40,   40,   40,   40,   40,   40,   40,   49,
   49,   15,   15,   43,   43,   38,   38,   50,   51,   51,
   51,   51,   51,   51,   51,   51,   51,   51,   52,   52,
   52,   52,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   53,   13,   13,   54,   54,   54,
   54,   54,   54,   54,   54,   54,   54,   54,   57,   20,
   20,   20,   20,   20,   20,   20,   20,   20,   58,   27,
   27,   59,   56,   56,   25,   60,   60,   55,   55,   61,
   62,   62,   37,   37,   63,   63,   64,   64,   64,   64,
   65,   65,   67,   67,   67,   67,   69,   70,   70,   71,
   71,   72,   72,   72,   72,   72,   72,   73,   73,   73,
   73,   73,   73,   73,   73,   21,   21,   74,   74,   74,
   74,   74,   75,   75,   76,   77,   77,   78,   79,   79,
   80,   80,   44,   81,   82,   66,   66,   83,   83,   83,
   83,   83,   83,   83,   84,   84,   84,   84,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,   68,   68,   68,
   68,   68,   68,   68,   68,   68,   68,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    6,    6,    9,
   10,    9,   10,   10,   10,   10,    7,   11,    9,   10,
   11,    9,   10,    8,    5,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    3,    3,    3,    6,    5,    1,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    2,    3,    1,    2,    3,
    3,    3,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    3,    1,    1,    1,    1,    4,
    2,    3,    5,    1,    3,    1,    1,    1,    1,    1,
    1,    1,    1,    3,    4,    3,    5,    1,    5,    5,
    0,    4,    1,    3,    1,    1,    7,    8,    1,    2,
    4,    3,    5,    1,    3,    2,    4,    2,    3,    3,
    4,    4,    1,    1,    1,    1,    2,    3,    2,    2,
    1,    2,    4,    5,    6,    6,    7,    1,    2,    1,
    3,    2,    1,    3,    1,    2,    2,    3,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    4,    1,    1,    4,    4,    2,    1,
    1,    2,    3,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    6,
    9,    8,    6,    6,    3,    3,    3,    5,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    2,
    1,    2,    1,    3,    2,    1,    2,    1,    3,    1,
    1,    3,    1,    2,    3,    1,    2,    3,    1,    2,
    1,    2,    1,    2,    3,    4,    1,    3,    2,    1,
    3,    2,    3,    3,    2,    4,    5,    1,    1,    1,
    1,    6,    9,    6,    6,    1,    3,    1,    1,    2,
    2,    2,    1,    3,    5,    1,    2,    3,    1,    2,
    1,    1,    1,    1,    5,    1,    3,    2,    7,    2,
    2,    7,    1,    1,    8,    9,    9,   10,    5,    6,
   11,    5,    7,    5,    5,    6,    4,    4,    5,    5,
    6,    6,    7,    5,    5,    6,    6,    6,    7,    5,
    6,    7,    7,    8,    6,    4,    4,    5,    6,    5,
    2,    5,    4,    4,    4,    4,    5,    6,    7,    6,
    6,    6,    4,    3,    4,    7,    8,    8,    9,   10,
    5,    6,    5,    5,    6,    3,    4,    5,    6,    7,
    4,    5,    6,    6,    4,    5,    7,    8,    5,    6,
   11,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,    0,   97,  108,  100,  101,  102,  103,   99,  133,
   40,   38,   41,   42,   43,   44,   45,   46,  303,  170,
  171,  172,    0,   39,    0,  163,  164,  168,  167,  169,
    0,  184,  185,    0,  165,  166,    0,    0,    0,   98,
    0,    0,    0,    0,    0,  134,  135,    0,    0,    0,
    3,    0,    0,    0,  161,    0,    4,    0,    0,  180,
  181,   36,   37,   47,   48,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  179,   91,
    0,    0,    0,    0,    0,    0,    0,  139,    0,    0,
    0,    0,  175,    0,    0,    0,   66,    0,    0,    0,
    0,    0,    0,    0,    0,  162,    5,    6,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    8,    0,    7,
    0,    0,    0,    0,    0,    0,    0,   92,    0,    0,
    0,    0,  138,    0,    0,  116,  104,    0,    0,  113,
    0,    0,   67,    0,  159,  160,  152,    0,    0,  153,
  188,    0,    0,    0,  186,    0,    0,    0,    0,    0,
    0,    0,  232,  233,  231,  234,  235,  236,  230,  215,
  219,  238,  237,    0,    0,    0,    0,    0,    0,    0,
    0,  218,  216,  217,    0,    0,    0,    0,  183,    0,
    0,    0,    0,   49,    0,    0,    0,   75,   74,   13,
    0,    0,   68,   73,  178,  174,  177,    0,    0,    0,
    0,    0,    0,  105,    0,    0,    0,   89,   88,   80,
   78,   79,   81,   82,   83,   84,    0,   76,  156,    0,
  151,    0,    0,    0,    0,    0,    0,  126,  187,    0,
    0,    0,    0,  144,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  243,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   15,    0,    0,
    0,   69,   14,    0,  240,  109,   93,  110,  112,  107,
  114,    0,    0,   12,   77,  158,  154,    0,  122,    0,
    0,    0,    0,    0,    0,    0,  313,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  253,  256,    0,    0,  261,
    0,  306,  314,    0,  140,    0,  145,    0,    0,  146,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  227,
    0,    0,    0,  225,  226,    0,    0,    0,    0,    0,
   62,   65,    0,   60,    0,   51,   63,    0,   57,   59,
   64,   61,   52,   53,   50,   17,   16,   72,   71,   70,
   85,  289,    0,  288,    0,  286,    0,    0,    0,  311,
    0,    0,  308,    0,    0,    0,    0,  310,  301,  302,
    0,    0,  299,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  304,  351,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  189,  190,  191,  192,  193,  194,
  195,  196,  197,  198,    0,  199,  200,  211,  212,  213,
  214,  202,  204,  205,  206,  207,  203,  201,  209,  210,
  208,    0,    0,    0,    0,    0,    0,    0,    0,  117,
  254,    0,  262,    0,    0,    0,  127,  147,   34,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  244,    0,
    0,    0,    0,    0,    0,    0,  245,    0,  292,  290,
  291,   90,    0,  123,  255,    0,  307,  239,    0,    0,
  267,    0,    0,    0,    0,    0,    0,  300,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  293,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  364,    0,    0,  118,   20,    0,   29,
    0,    0,    0,    0,    0,    0,    0,  228,    0,    0,
    0,    0,    0,   55,    0,   58,  287,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  346,
    0,    0,  250,  251,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  365,    0,   21,
    0,    0,    0,  224,  220,  223,    0,   24,    0,    0,
   54,    0,    0,    0,  269,    0,    0,  270,    0,    0,
    0,    0,  319,    0,  348,  389,    0,  357,  374,    0,
  352,  393,    0,  378,  350,  395,  386,  382,    0,    0,
  371,    0,  325,  324,  373,  396,    0,    0,    0,    0,
  322,    0,    0,    0,    0,  229,  242,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  294,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  246,    0,  248,    0,    0,    0,    0,
  296,    0,    0,  272,    0,  268,    0,    0,    0,    0,
    0,  320,  390,  375,  379,  383,  372,  326,  361,  384,
  252,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  360,  349,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  247,  222,    0,
  309,    0,  312,  297,    0,  279,  280,  281,    0,    0,
    0,    0,  278,  274,  273,  271,    0,    0,    0,    0,
  323,    0,    0,    0,    0,  366,    0,  387,    0,    0,
    0,  295,  380,  305,    0,    0,    0,    0,  221,  249,
  298,  276,    0,    0,    0,    0,    0,  315,    0,    0,
    0,  368,    0,    0,  367,  388,    0,    0,    0,  277,
    0,    0,    0,    0,  316,  317,    0,    0,  369,    0,
    0,    0,    0,    0,    0,  318,  370,    0,    0,    0,
    0,    0,    0,  321,  391,    0,  285,  282,  284,    0,
    0,  283,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   60,   12,   13,   14,  247,  221,  213,   61,
   87,  222,  571,   88,  262,   69,   90,  214,  406,  202,
  425,  408,  409,  410,  411,  223,  834,  248,  102,  103,
  159,  110,  160,   15,  121,  173,  365,  263,  258,   75,
   65,   76,   66,   67,   16,  264,  169,  170,   91,  175,
  505,  522,  203,  204,  835,  276,  807,  433,  713,  836,
  704,  705,  366,  367,  368,  369,  370,  371,  572,  672,
  767,  768,  904,  426,  632,  633,  840,  841,  442,  443,
  478,  637,  372,  373,
  };
  protected static readonly short [] yySindex = {          896,
   18,  -72,   27,   97,  113, 3088, -224, -146,    0,  896,
    0,    0,    0,    0,  -90, 3466,  -70,  149,  174, 1029,
  -87,   -4,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  204,    0,  211,    0,    0,    0,    0,    0,
  217,    0,    0,   10,    0,    0, 3287,  -71,   17,    0,
  -98, -115,  242, 5043,  564,    0,    0,   20,   29,  232,
    0,  271, 3544,  -13,    0, 3544,    0,   78,  102,    0,
    0,    0,    0,    0,    0,  143, 5043,  216,   48,  -29,
 -115,    2,  330,    7,  210,  121, 5043, 5043,    0,    0,
 -115,   72,  242,  116, 5043,  125,   90,    0,  354,  356,
 3988,  242,    0, 5043,  242, 3544,    0,  128,  281, 3233,
 -170,    4, 3544,  271,    5,    0,    0,    0, 5043,   48,
   48, 3749, 5043,   48, 5043,   48,   93,    0,  288,    0,
 -174,  373,  299, 4815,  382,  -37,  -36,    0, 5043, 5043,
   82, 5043,    0,  171,   49,    0,    0, -115,  173,    0,
  242,  242,    0,  494,    0,    0,    0, 5181,  190,    0,
    0,  108,  -59, -162,    0,  271,    6, -170,  271, 1558,
 5043, 5043,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -28,  404,  405,  409, 5067, 5101, 5067,
  406,    0,    0,    0, 3749, 5043, 3749, 5043,    0,  401,
  413,  424,  209,    0, -174, 4691,    0,    0,    0,    0,
    9, 3749,    0,    0,    0,    0,    0, -115,  -34,  426,
  -52,  448,  233,    0, 4619,  437,  463,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  826,    0,    0, 4224,
    0, 4387,   28, 5057,  -54, 5067,  247,    0,    0, -170,
  271,  108,  108,    0, -170,  130,  461, -115, 3722,  477,
 5043, 5067, 5067, 5067,    0,   86, 4927,  104,   19,  138,
  485, 3749,  491, 3749, 4808, 4859,  819,    0, -174,  220,
   11,    0,    0, 4899,    0,    0,    0,    0,    0,    0,
    0,  278, 4892,    0,    0,    0,    0,  289,    0,  493,
  487, 5067, -151, 5067,  962, 5067,    0, 3596, 5043, 3596,
 5043, 3596, 5043, 5043, 1910, 5043, 5043, 5043, 3596, 2271,
 2632, 5043, 5043, 5043, 5067, 5067, 5067, 5067, 5067, 5043,
  -43, 3811,  221, -179, 1232, 5067, 5067, 5067, 5067, 5067,
 5067, 5067, 5067, 5067, 5067, 5067, 5067, 1231, 2377, 5043,
 5043,  962,  132, 5043, 3834,    0,    0, 7643, -224,    0,
 -224,    0,    0, 5057,    0,  240,    0, -170,  108,    0,
  301, -209,  176,  518, 5043,  -38,  179,  184,  185,    0,
 5067, 3749,   88,    0,    0,  306,  195,  534,  199,  538,
    0,    0,  544,    0,  -78,    0,    0,  464,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -145,    0,  228,    0,  240, 7643, 8138,    0,
  316, 3782,    0,  547,  473, 3544, 3544,    0,    0,    0,
 3749, 3596,    0, 3749, 3749, 3596, 3749, 3749, 3596, 3749,
 3749, 5043, 3749, 3749, 3749, 3749, 3749, 3596, 5043, 3749,
 5043, 3749, 3749, 3749, 3749,  548,  549,  551,  555,  556,
  -14, 5043, 3851,  -12, 5067,  557,    0,    0, 5043, 5043,
 5043,  -11,  214,  236,  273,  277,  280,  290,  291,  292,
  294,  297,  302,  303,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5043,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 5043,  -73, 3749, 3024, 5043, 3544,  962, -238,    0,
    0, -224,    0,   29,   29, 3958,    0,    0,    0,  350,
  425,  428, -176,  -10, 5067, 5043, 5043, 5043,    0,  608,
 -224,  433,  313,  440,  314, 4684,    0, 4859,    0,    0,
    0,    0, 4892,    0,    0, -224,    0,    0,  654,  436,
    0,  666,  473,  473, 3544,  660, 3749,    0,  663,  677,
 3749,  678,  679, 3749,  682,  684, 3749,  685,  689,  693,
  695,  696, 3749, 3749,  699, 3749,  703,  704,  705,  706,
 5067, 5067, 5067,  819, 5067, 1421,   -9, 5043,   -8, 5043,
  707, 5043, 3749, 3749,   -7, 5067, 5043, 5043, 5043, 5043,
 5043, 5043, 5043, 5043, 5043, 5043, 5043, 5043, 3749, 3749,
 3782,  709,    0,  711,  283,  666,  666,  473,  473, 5043,
 3024, 5043, 3544,    0, 5067,   29,    0,    0, -224,    0,
  496,  502, 5067,  717,  -22,  -20,  -19,    0,   29, -224,
  508, -224,  509,    0,  251,    0,    0,   29,  436,  680,
 4123,  387,  666,  666,  473, 3782,  733, 3782, 3782,  739,
 3782, 3782,  740, 3782, 3782,  746, 3782, 3782, 3782, 3782,
 3782,  749,  753, 3782,  756, 3782, 3782, 3782, 3782,    0,
  759,  763,    0,    0,  766,  768,  554,  773, 5043,   -2,
 5043, 3749,  774, 5043,  775,  778,  783, 5067,  785, -115,
 -115, -115, -115, -115, -115, -115, -115, -115, -115, -115,
 -115,  786,  789,  791,  745, 5067,  579,  108,  108,  666,
  666,  473,  666,  666,  473,  473, 3544,    0,   29,    0,
 -224,  796,  133,    0,    0,    0,   29,    0,   29, -224,
    0,  801, 5043, 4932,    0, 3169,  257,    0,  436,  454,
  462,  666,    0, 3782,    0,    0, 3782,    0,    0, 3782,
    0,    0, 3782,    0,    0,    0,    0,    0, 3782, 3782,
    0, 3782,    0,    0,    0,    0, 5067, 5067,  819,  819,
    0,  474,  809, 5043,  811,    0,    0,  476,  816,  481,
 5043, 5043,  822,  133, 3782, 3782, 3782,    0,  829,  832,
  108,  108,  108,  108,  666,  108,  108,  666,  666,  473,
   29,  133, 5067,    0,  263,    0,   29,  436,  833, 4983,
    0,  830, 4154,    0, 3274,    0, 5017,  560,  436,  436,
  501,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  766,  634,  512,  -42,  513,  640,  521,  643, 3749,
 3749,  133,  858,    0,    0,  810, 5067,  644,  108,  108,
  108,  108,  108,  108,  108,  666,  296,    0,    0,  133,
    0,  436,    0,    0, 4775,    0,    0,    0,  524,  868,
  874,  876,    0,    0,    0,    0,  436,  589,  591,  436,
    0,  661,  877,  540,  667,    0,  668,    0,  593,  596,
  858,    0,    0,    0,  108,  108,  108,  108,    0,    0,
    0,    0,  343,  883, 5067, 5067, 5067,    0,  436,  436,
  603,    0,  550,  683,    0,    0,  890,  891,  108,    0,
 5043,  552,  553,  558,    0,    0,  436,  686,    0,  568,
  569,   -1, 5043, 5043, 5043,    0,    0,  698,  712, 5067,
  -18,  -16,  -15,    0,    0,  906,    0,    0,    0,  133,
  410,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0, 3602,    0,    0,  956,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  741,    0,    0,    0,    0,    0,
 1278,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3323, 3070,  688,    0,    0,    0,    0,    0, 3644,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  799,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  361,    0,    0,    0,    0,    0, 3391,    0,    0,    0,
    0,  690,    0,    0,  694,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   99,    0,    0,    0,    0,  152,    0,    0,    0,
    0,    0,    0,    0,    0,   99,   99,    0,    0,    0,
    0,    0,    0,    0, 1099,    0,    0,  490,    0,    0,
  700,  716,    0,    0,    0,    0,    0,  503,    0,    0,
    0,  -51,    0,  -49,    0,    0,    0,  189,    0,   61,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   99,    0,   99,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 1386,    0,    0,    0,
    0,   99,    0,    0,    0,    0,    0,  370,   99,    0,
   99,    0,    0,    0,    0, 1750, 2111,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  456,
    0,    0,  -48,    0,    0,    0,    0,    0,    0,    0,
    0,  308,  388,    0,    0,    0,  399,  212,   99,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   99,    0,   99,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4098,    0,
 7747,    0,    0,    0,    0,  -47,    0,    0,  506,    0,
    0,    0,    0,    0,    0,   99,    0,    0,    0,    0,
    0,  361,    0,    0,    0,    0,    0,    0,    0,  519,
    0,    0,   45,    0,   99,    0,    0,  482,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   99,    0,    0,    0,  -41,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   99,    0,    0,   99,   99,    0,   99,   99,    0,   99,
   99,    0,   99,   99,   99,   99,   99,    0,    0,   99,
    0,   99,   99,   99,   99,    0,    0,    0,    0,    0,
   99,    0,    0,   99,    0,    0,    0,    0,    0,    0,
    0,   99,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   99,   99,    0,    0,    0,    0,   99,    0,
    0, 4222,    0, 4362, 7851,    0,    0,    0,    0,    0,
    0,    0,    0,   99,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 7955,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   99,    0,    0,    0,
   99,    0,    0,   99,    0,    0,   99,    0,    0,    0,
    0,    0,   99,   99,    0,   99,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   99,    0,   99,    0,
    0,    0,   99,   99,   99,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   99,   99,
    0, 5184,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4486,    0,    0,  590,    0,
    0,    0,    0,    0,   99,   99,   99,    0,  638,    0,
    0,    0,    0,    0,    0,    0,    0, 8059,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5292,    0,    0,    0,    0,   99,
    0,   99,    0,    0,    0,    0,    0,    0,    0, 1512,
 1621, 1747, 1873, 1982, 2108, 2234, 2343, 2469, 2595, 2704,
 2830,    0,    0,    0,    0,    0,    0, 5399,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  664,    0,
  718,    0,    0,    0,    0,    0,  767,    0, 1108, 1301,
    0,    0,    0,    0,    0,   99,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5506, 5613, 5720, 5827,    0, 5934,    0,    0,    0,    0,
 1339,    0,    0,    0,    0,    0, 1352,    0,    0,    0,
    0,  495,   99,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 6041,    0,    0,    0,    0,    0,    0,    0,   99,
   99,    0, 6148,    0,    0,    0,    0,    0, 6255, 6362,
 6469, 6576, 6683, 6790, 6897,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 7004,    0,    0,    0, 7111, 7218, 7325, 7432,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 7539,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   99,    0,    0,    0,    0,    0,    0,    0,    0,
   99,   99,   99,    0,    0,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  968,  887,    0,    0,    0,    0,  765,  777,  974,
  565,   -6,  520,   73,   67, -166,    0,  720,  715, -272,
 -518,    0,  452,    0, -698,    0,  293,  748,  907,   33,
    0,  924,  781,    0,  -39,    0,  645, -112, -199,   -3,
    0,  -57,  957,  -60,    0, -182,    0,  769,    0, -163,
    0,    0,    0,    0, -748, -104,    0, -550, -544,  127,
  231,  237, -341,  604,    0,  665,  670,  612,  602, -580,
    0,  198,    0,  468,    0,  311,    0,  207,  -33,   14,
    0,  422,    0,  621,
  };
  protected static readonly short [] yyTable = {            62,
  108,  914,   64,  226,  227,  545,  255,  116,  174,   62,
  259,  271,  407,  407,  414,  123,   58,  631,  754,  670,
  755,  756,  977,  531,  978,  979,  111,  296,   95,  606,
  424,  610,  616,  653,  709,  711,  718,  665,   63,  143,
  298,  804,  970,  111,  111,  111,  153,   59,   74,   68,
  101,  105,  294,  309,  294,  738,  739,   62,   62,  253,
   35,  139,  391,  254,  839,  873,   62,  715,  374,   62,
  540,  119,  126,  124,  120,  125,  644,  377,   17,   57,
  132,  121,  380,  887,  178,   61,   89,   20,   61,  259,
  146,  147,  770,  771,  278,  279,  112,  115,  101,  259,
   96,  210,  111,  651,  158,  122,  211,   62,  125,   62,
  250,  395,  126,  168,  431,  149,   62,  559,  762,  126,
   80,   81,  180,  921,   57,  149,  205,  560,  207,  391,
  561,  391,  557,  293,  432,  417,  260,  114,   96,  265,
  111,  839,  228,  229,  109,  231,  161,  391,  162,  379,
  256,  182,  131,  171,  134,  177,  136,   21,  130,  823,
  824,  171,  826,  827,  803,  394,  805,  479,   61,  809,
  541,  480,  393,   22,  268,  269,  537,   70,  183,  184,
  185,   72,  186,  187,  188,  104,  189,  172,  143,   77,
  645,  851,   58,   92,  531,  538,  148,   18,   19,  282,
  192,  284,  534,  652,  535,  212,  230,   39,  193,   78,
  390,   19,  550,  234,   68,  259,  235,  257,  848,  118,
   23,  378,  119,   59,  124,  120,  125,  564,  158,   24,
  251,  981,  121,  252,   79,   52,   53,  109,   25,   26,
   27,   28,   29,   96,  881,  168,  126,  884,  885,  288,
   97,   96,  289,  111,  437,   57,   98,  436,  124,  865,
  416,   80,   81,  289,  386,  472,   99,  109,  562,   93,
  392,  563,   94,  106,  182,  176,  179,  261,  405,  405,
  142,  111,  292,  424,  292,  407,  446,  891,  449,  138,
  424,  761,  119,  117,  563,  458,  423,  846,  908,  909,
  847,  527,  118,  889,  109,  928,  890,  148,   62,  109,
  120,  441,  444,  445,  447,  448,  450,  451,  453,  454,
  455,  456,  457,  460,  462,  463,  464,  465,   35,   35,
  109,  703,  144,  471,  474,   35,  929,  127,  482,  890,
  256,  931,   82,   83,  109,  109,  109,  435,  109,   35,
   35,  171,  270,  523,  524,   62,  938,  529,  526,  941,
  109,  128,  109,  109,  109,  646,  109,  109,  109,  141,
  109,  109,  109,  109,  109,  109,  933,  145,  544,  575,
  109,  109,  150,  950,  659,   35,  563,  150,  955,  956,
  473,  152,  913,  154,  525,   39,  217,  155,   18,  668,
   96,  163,  111,  164,   94,   24,  966,  308,  209,   96,
  105,  111,  215,   95,   25,   26,   27,   28,   29,  182,
  182,  216,  225,  182,  182,  182,  182,  232,  233,   62,
   62,  171,  126,   80,   81,  577,  224,   82,   83,  581,
  182,  182,  584,  272,  273,  587,  182,  182,  274,  280,
  982,  593,  594,  890,  596,  578,  143,  143,  285,  578,
  143,  143,  578,  143,   85,  607,  609,  643,  573,  574,
  286,  578,  613,  614,  615,  182,  182,  143,  143,   19,
   19,  287,  749,   19,   19,   94,   19,  297,  299,  300,
  275,  275,  275,  757,   95,  759,  157,  302,  629,  157,
   19,   19,  303,  376,  382,  149,   80,   81,  224,  381,
   82,   83,  111,  143,  833,  630,  385,  396,   27,   62,
   62,   62,   56,  640,  642,   56,  703,  703,  397,   96,
  115,  111,  199,  115,  399,  275,   19,  421,  275,  655,
  656,  657,   96,  155,  111,  427,  155,  429,  375,  423,
  428,  405,  256,  477,  528,  542,  423,  539,  638,  639,
  641,  543,  551,  200,  387,  388,  389,  546,   62,  275,
  903,  126,  547,  548,  552,  148,  148,  553,  554,  148,
  148,  555,  148,  556,  831,  747,  420,  558,  568,   32,
  570,  601,  602,  837,  603,  198,  148,  148,  604,  605,
  612,  710,  617,  712,  430,  712,  434,  675,  438,  648,
  720,  721,  722,  723,  724,  725,  726,  727,  728,  729,
  730,  731,  424,   58,  618,  821,  822,  466,  467,  468,
  469,  470,  148,   62,  476,   62,   62,   22,  483,  484,
  485,  486,  487,  488,  489,  490,  491,  492,  493,  494,
  129,  201,  133,  135,   59,  150,  150,  259,  259,  150,
  150,  619,  150,   33,  766,  620,   18,   18,  621,  658,
   18,   18,  742,   18,  745,  746,  150,  150,  622,  623,
  624,  649,  625,  549,  650,  626,   57,   18,   18,  660,
  627,  628,  661,  663,  181,  182,  662,  669,  206,  267,
  208,  431,  712,  676,  712,  671,  678,  712,  845,  737,
  879,  880,  150,  882,  883,  259,  259,   30,  259,  259,
  679,  681,  682,   18,  281,  684,  283,  685,  687,  183,
  184,  185,  688,  186,  187,  188,  689,  189,  690,  691,
   62,  295,  694,  126,  190,  191,  696,  697,  698,  699,
  714,  192,  735,  236,  736,  750,  405,  843,  751,  193,
  753,  259,  259,  259,  758,  760,   25,  611,  925,  237,
  763,  926,  927,  149,  149,  769,  774,  149,  149,  830,
  149,  173,  777,  780,  173,  259,   27,   27,  384,  783,
   27,   27,  789,   27,  149,  149,  790,  712,  111,  792,
  173,  398,  797,  400,  870,  871,  798,   27,   27,  799,
  801,  800,  238,  239,  240,  949,  802,  808,  810,  241,
  242,  811,  243,  244,  245,  246,  812,   23,  814,  815,
  149,  173,  816,  405,  817,  631,   24,  654,  820,  832,
  766,  126,  849,   27,  838,   25,   26,   27,   28,   29,
  850,  194,  864,  863,  866,  867,  113,   32,   32,  868,
  869,   32,   32,  173,   32,  872,  195,  196,  197,  895,
   40,   41,  877,   42,   43,  878,  892,   45,   32,   32,
   46,   47,   48,   49,   50,   85,   51,  907,  423,  910,
  911,  912,  915,  700,  701,  702,  916,  706,  708,  918,
  917,  890,  922,  924,  934,   22,   22,  935,  719,   22,
   22,  295,   22,  936,   32,  937,  939,  942,  940,  944,
  943,  111,  951,  945,  946,  947,   22,   22,  948,  958,
  957,   33,   33,  960,  961,   33,   33,  748,   33,  959,
  963,  964,  967,   54,  962,  752,  965,  968,  969,  980,
  304,  569,   33,   33,  974,    1,  971,  972,  973,  128,
  576,  129,   22,  579,  580,  130,  582,  583,  975,  585,
  586,  131,  588,  589,  590,  591,  592,   71,  140,  595,
  291,  597,  598,  599,  600,   30,   30,  132,   33,   30,
   30,  290,   30,   86,  305,   55,   56,  173,  173,  173,
  413,  173,  173,  173,  173,  173,   30,   30,  415,  666,
  813,  151,  173,  173,  137,  301,  930,  107,  536,  173,
  307,   58,  173,  173,  173,  173,  173,  173,  819,  861,
  667,  565,  532,  173,   25,   25,  862,  533,   25,   25,
  566,   25,   30,  634,  906,  818,  894,  173,  173,  567,
  173,  173,   59,    0,  173,   25,   25,  173,  173,  173,
  173,  173,  744,  173,    0,    0,  111,  111,    0,    0,
  111,  111,  111,  111,    0,  183,  184,  185,    0,  186,
  187,  188,    0,  189,   57,  236,    0,  111,  111,  859,
  860,   25,    0,  111,  111,    0,  677,  192,  106,    0,
  680,  237,    0,  683,    0,  193,  686,   23,    0,    0,
    0,    0,  692,  693,  111,  695,    0,    0,    0,  173,
  173,    0,  111,  111,    0,  888,  636,    0,    0,    0,
    0,    0,  716,  717,  173,  173,  173,    0,  106,  106,
  106,    0,  106,    0,  238,  239,  240,    0,  732,  733,
  734,  241,  242,    0,  243,  244,  245,  246,  106,    0,
  106,    0,    0,    1,    2,    0,    0,    3,    4,  923,
    5,    0,  173,  173,  673,  674,    0,    0,    0,    0,
    0,    0,    0,    0,    6,    7,    0,    0,    0,  106,
    0,  106,    0,    0,    0,  773,    0,  775,  776,    0,
  778,  779,    0,  781,  782,    0,  784,  785,  786,  787,
  788,    0,    0,  791,    0,  793,  794,  795,  796,    0,
    8,  106,    0,  106,    0,   23,    0,  952,  953,  954,
    0,  806,    0,    0,   24,    0,    0,    0,    0,  740,
  741,    0,  743,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,  113,    0,    0,    0,    0,    0,
    0,    0,  976,    0,    0,    0,    0,   39,   40,   41,
    0,   42,   43,    0,    0,   45,  772,    0,   46,   47,
   48,   49,   50,    0,   51,  844,    0,    0,    0,    0,
    0,   58,    0,  852,    0,    0,  853,    0,    0,  854,
   26,    0,  855,    0,    0,    0,    0,    0,  856,  857,
    0,  858,    0,    0,    0,    0,    0,    0,  176,   80,
   81,  176,   59,   82,   83,   84,   31,   32,   33,   34,
   35,   36,   37,   38,  874,  875,  876,  176,   31,    0,
   44,   54,    0,  825,    0,    0,  828,  829,    0,    0,
   85,   28,    0,    0,   57,  106,  106,  106,    0,  106,
  106,  106,    0,  106,  905,    0,  106,  106,  176,    0,
  106,  106,  106,  106,  106,   23,   23,  106,    0,   23,
   23,    0,   23,    0,    0,  106,    0,  106,  106,  919,
  920,  106,    0,   55,   56,    0,   23,   23,    0,    0,
  176,    0,    0,    0,    0,  106,  106,    0,  106,  106,
    0,    0,  106,  106,    0,  106,  106,  106,  106,  106,
    0,  106,    0,  106,    0,   97,  241,    0,    0,  241,
    0,  886,   23,    0,  106,  106,  106,    0,  106,  106,
    0,    0,    0,  106,    0,  106,    0,  241,  106,  106,
  106,  106,  106,  106,  106,  106,  106,  106,    0,  106,
  106,    0,  106,  106,  106,  106,  106,  106,  106,  106,
  106,  106,  106,  106,  106,    0,  106,  106,  241,    0,
   58,  106,  106,  106,  106,  106,  106,    0,  106,  106,
  106,  106,  106,  106,  106,   23,  106,    0,    0,    0,
    0,    0,    0,    0,   24,    0,    0,  106,  241,    0,
  241,   59,    0,   25,   26,   27,   28,   29,    0,  106,
  106,  106,  106,    0,  106,    0,  106,  106,    0,    0,
  106,  106,    0,    0,  176,  176,  176,    0,  176,  176,
  176,  176,  176,   57,    0,    0,    0,    0,    0,  176,
  176,   96,    0,  111,    0,    0,  176,    0,    0,  176,
  176,  176,  176,  176,  176,    0,    0,    0,   26,   26,
  176,    0,   26,   26,    0,   26,    0,    0,    0,    0,
    0,    0,    0,    0,  176,  176,    0,  176,  176,   26,
   26,  176,    0,    0,  176,  176,  176,  176,  176,    0,
  176,  266,    0,    0,    0,    0,   31,   31,    0,    0,
   31,   31,  481,   31,    0,    0,    0,  199,    0,   28,
   28,    0,    0,   28,   28,   26,   28,   31,   31,  495,
  496,  497,  498,  499,  500,  501,  502,  503,  504,    0,
   28,   28,  241,    0,    0,    0,    0,    0,  200,    0,
    0,    0,    0,    0,    0,    0,  176,  176,  241,  241,
   96,    0,  111,   31,    0,    0,    0,    0,    0,    0,
    0,  176,  176,  176,    0,    0,   28,    0,    0,    0,
  198,    0,    0,    0,  217,    0,    0,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,  176,
  176,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  241,  241,  241,    0,  241,  241,    0,    0,    0,
  241,    0,  241,    0,    0,  241,  241,  241,  241,  241,
  241,  241,  241,  241,  241,    0,  241,  241,    0,  241,
  241,  241,  241,  241,  241,  241,  241,  241,  241,  241,
  241,  241,    0,  241,  241,    0,    0,    0,    0,  241,
  241,  241,  241,  241,  241,  241,  241,  241,  241,  241,
  241,  241,    0,  241,  392,  392,   96,    0,  111,    0,
    0,    0,    0,    0,  241,    0,    0,    0,    0,    0,
  707,    0,    0,    0,    0,    0,  241,  241,  241,  241,
    0,    0,    0,  241,  183,  184,  185,    0,  186,  187,
  188,    0,  189,    0,    0,    0,    0,    0,    0,  190,
  191,    0,    0,    0,    0,    0,  192,    0,    0,    0,
    0,    0,    0,    0,  193,    0,    0,  392,  392,  392,
    0,  392,  392,    0,    0,    0,  392,    0,  392,    0,
    0,  392,  392,  392,  392,  392,  392,  392,  392,  392,
  392,    0,  392,  392,   86,  392,  392,  392,  392,  392,
  392,  392,  392,  392,  392,  392,  392,  392,    0,  392,
  392,    0,    0,  397,  397,  392,  392,  392,  392,  392,
    0,  392,  392,  392,  392,  392,  392,  392,    0,  392,
    0,    0,   96,    0,  111,    0,    0,    0,    0,    0,
  392,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  392,  392,  392,  392,  194,    0,    0,  392,
  109,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  195,  196,  197,    0,    0,  397,  397,  397,    0,
  397,  397,    0,    0,    0,  397,    0,  397,    0,   58,
  397,  397,  397,  397,  397,  397,  397,  397,  397,  397,
    0,  397,  397,    0,  397,  397,  397,  397,  397,  397,
  397,  397,  397,  397,  397,  397,  397,    0,  397,  397,
   59,    0,    0,    0,  397,  397,  397,  397,  397,   86,
  397,  397,  397,  397,  397,  397,  397,    0,  397,  381,
  381,   96,    0,  111,    0,   86,    0,    0,    0,  397,
    0,    0,   57,    0,    0,    0,    0,    0,    0,    0,
    0,  397,  397,  397,  397,    0,    0,    0,  397,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   86,   86,
   86,    0,    0,    0,    0,   86,   86,    0,   86,   86,
   86,   86,  381,  381,  381,    0,  381,  381,    0,    0,
    0,  381,    0,  381,    0,    0,  381,  381,  381,  381,
  381,  381,  381,  381,  381,  381,    0,  381,  381,    0,
  381,  381,  381,  381,  381,  381,  381,  381,  381,  381,
  381,  381,  381,    0,  381,  381,    0,    0,    0,    0,
  381,  381,  381,  381,  381,    0,  381,  381,  381,  381,
  381,  381,  381,    0,  381,  356,  356,   96,    0,  111,
    0,    0,    0,    0,    0,  381,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  381,  381,  381,
  381,    0,    0,   23,  381,    0,    0,    0,    0,    0,
    0,    0,   24,    0,    0,    0,    0,    0,    0,    0,
    0,   25,   26,   27,   28,   29,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  356,  356,
  356,    0,  356,  356,    0,    0,    0,  356,    0,  356,
    0,    0,  356,  356,  356,  356,  356,  356,  356,  356,
  356,  356,    0,  356,  356,   87,  356,  356,  356,  356,
  356,  356,  356,  356,  356,  356,  356,  356,  356,    0,
  356,  356,    0,    0,  353,  353,  356,  356,  356,  356,
  356,    0,  356,  356,  356,  356,  356,  356,  356,    0,
  356,    0,  452,   96,    0,  111,    0,    0,    0,    0,
    0,  356,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  356,  356,  356,  356,    0,    0,    0,
  356,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  353,  353,  353,
    0,  353,  353,    0,    0,    0,  353,    0,  353,    0,
   58,  353,  353,  353,  353,  353,  353,  353,  353,  353,
  353,    0,  353,  353,    0,  353,  353,  353,  353,  353,
  353,  353,  353,  353,  353,  353,  353,  353,    0,  353,
  353,   59,    0,    0,    0,  353,  353,  353,  353,  353,
   87,  353,  353,  353,  353,  353,  353,  353,    0,  353,
  354,  354,   96,    0,  111,    0,   87,    0,    0,    0,
  353,    0,    0,   57,    0,    0,    0,    0,    0,    0,
    0,    0,  353,  353,  353,  353,    0,    0,    0,  353,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   87,
   87,   87,    0,    0,    0,    0,   87,   87,    0,   87,
   87,   87,   87,  354,  354,  354,    0,  354,  354,    0,
    0,    0,  354,    0,  354,    0,    0,  354,  354,  354,
  354,  354,  354,  354,  354,  354,  354,    0,  354,  354,
    0,  354,  354,  354,  354,  354,  354,  354,  354,  354,
  354,  354,  354,  354,    0,  354,  354,    0,    0,    0,
    0,  354,  354,  354,  354,  354,    0,  354,  354,  354,
  354,  354,  354,  354,    0,  354,  355,  355,   96,    0,
  111,    0,    0,    0,    0,    0,  354,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  354,  354,
  354,  354,    0,    0,   23,  354,    0,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  355,
  355,  355,    0,  355,  355,    0,    0,    0,  355,    0,
  355,    0,    0,  355,  355,  355,  355,  355,  355,  355,
  355,  355,  355,    0,  355,  355,    0,  355,  355,  355,
  355,  355,  355,  355,  355,  355,  355,  355,  355,  355,
    0,  355,  355,    0,    0,  394,  394,  355,  355,  355,
  355,  355,    0,  355,  355,  355,  355,  355,  355,  355,
    0,  355,    0,  459,   96,    0,  111,  506,  507,    0,
    0,    0,  355,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  355,  355,  355,  355,    0,    0,
    0,  355,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  394,  394,
  394,    0,  394,  394,    0,    0,    0,  394,    0,  394,
    0,   58,  394,  394,  394,  394,  394,  394,  394,  394,
  394,  394,    0,  394,  394,    0,  394,  394,  394,  394,
  394,  394,  394,  394,  394,  394,  394,  394,  394,    0,
  394,  394,   59,    0,    0,    0,  394,  394,  394,  394,
  394,    0,  394,  394,  394,  394,  394,  394,  394,    0,
  394,  385,  385,   96,    0,  111,    0,    0,    0,    0,
    0,  394,    0,    0,   57,    0,    0,    0,    0,    0,
    0,    0,    0,  394,  394,  394,  394,    0,    0,    0,
  394,    0,    0,    0,    0,    0,    0,  508,  509,  510,
  511,    0,    0,    0,    0,    0,  512,  513,  514,  515,
  516,  517,  518,  519,  520,  521,    0,    0,    0,    0,
    0,    0,    0,    0,  385,  385,  385,    0,  385,  385,
    0,    0,    0,  385,    0,  385,    0,    0,  385,  385,
  385,  385,  385,  385,  385,  385,  385,  385,    0,  385,
  385,    0,  385,  385,  385,  385,  385,  385,  385,  385,
  385,  385,  385,  385,  385,    0,  385,  385,    0,    0,
    0,    0,  385,  385,  385,  385,  385,    0,  385,  385,
  385,  385,  385,  385,  385,    0,  385,  377,  377,   96,
    0,  111,    0,    0,    0,    0,    0,  385,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  385,
  385,  385,  385,    0,    0,   23,  385,    0,    0,    0,
    0,    0,    0,    0,   24,    0,    0,    0,    0,    0,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  377,  377,  377,    0,  377,  377,    0,    0,    0,  377,
    0,  377,    0,    0,  377,  377,  377,  377,  377,  377,
  377,  377,  377,  377,    0,  377,  377,    0,  377,  377,
  377,  377,  377,  377,  377,  377,  377,  377,  377,  377,
  377,    0,  377,  377,    0,    0,  363,  363,  377,  377,
  377,  377,  377,    0,  377,  377,  377,  377,  377,  377,
  377,    0,  377,    0,  461,    0,    0,    0,    0,    0,
    0,    0,    0,  377,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  377,  377,  377,  377,    0,
    0,    0,  377,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  363,
  363,  363,    0,  363,  363,    0,    0,    0,  363,    0,
  363,    0,    0,  363,  363,  363,  363,  363,  363,  363,
  363,  363,  363,  111,  363,  363,    0,  363,  363,  363,
  363,  363,  363,  363,  363,  363,  363,  363,  363,  363,
    0,  363,  363,  199,    0,    0,    0,  363,  363,  363,
  363,  363,    0,  363,  363,  363,  363,  363,  363,  363,
    0,  363,  327,  327,    0,    0,    0,    0,    0,   96,
    0,  111,  363,    0,  200,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  363,  363,  363,  363,    0,   96,
    0,  363,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  198,   58,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   96,    0,    0,    0,    0,  327,  327,  327,    0,  327,
  327,    0,    0,    0,  327,    0,  327,    0,   59,  327,
  327,  327,  327,  327,  327,  327,  327,  327,  327,    0,
  327,  327,   96,  327,  327,  327,  327,  327,  327,  327,
  327,  327,  327,  327,  327,  327,    0,  327,  327,    0,
   57,    0,    0,  327,  327,  327,  327,  327,    0,  327,
  327,  327,  327,  327,  327,  327,    0,  327,  199,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  327,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  327,  327,  327,  327,    0,    0,    0,  327,    0,  200,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  167,    0,    0,    0,    0,    0,    0,
  183,  184,  185,    0,  186,  187,  188,    0,  189,    0,
    0,  198,   58,    0,    0,  190,  191,    0,    0,    0,
    0,    0,  192,    0,    0,    0,    0,    0,    0,    0,
  193,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   59,    0,    0,   96,   96,   96,    0,
   96,   96,   96,  199,   96,    0,    0,    0,    0,    0,
    0,   96,   96,    0,    0,    0,   58,    0,   96,    0,
    0,   23,    0,    0,    0,   57,   96,    0,    0,    0,
   24,    0,    0,    0,  200,    0,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,   59,    0,    0,
   30,    0,  136,    0,    0,   31,   32,   33,   34,   35,
   36,   37,   38,   39,   40,   41,  198,   42,   43,   44,
    0,   45,  194,    0,   46,   47,   48,   49,   50,   57,
   51,  100,    0,  136,    0,    0,    0,  195,  196,  197,
    0,   52,   53,    0,    0,  183,  184,  185,    0,  186,
  187,  188,    0,  189,    0,    0,    0,    0,    0,    0,
  190,  191,    0,    0,    0,  136,    0,  192,   96,  635,
  137,    0,    0,    0,    0,  193,    0,    0,    0,    0,
    0,  113,    0,   96,   96,   96,    0,   54,    0,    0,
    0,    0,    0,    0,    0,   40,   41,    0,   42,   43,
    0,  137,   45,    0,    0,   46,   47,   48,   49,   50,
    0,   51,    0,    0,    0,   96,   23,    0,    0,    0,
    0,    0,    0,    0,    0,   24,    0,    0,    0,    0,
  165,    0,    0,  137,   25,   26,   27,   28,   29,   55,
   56,    0,    0,    0,    0,   58,  166,    0,    0,    0,
  183,  184,  185,    0,  186,  187,  188,    0,  189,    0,
    0,    0,    0,    0,    0,  190,  191,  194,   54,    0,
   23,  109,  192,    0,    0,    0,   59,    0,    0,   24,
  193,    0,  195,  196,  197,    0,  113,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,    0,    0,    0,
   40,   41,    0,   42,   43,    0,  136,   45,   57,    0,
   46,   47,   48,   49,   50,  136,   51,    0,    0,    0,
   55,   56,    0,   58,  136,  136,  136,  136,  136,    0,
    0,    0,    0,    0,    0,  136,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  136,
  136,    0,  136,  136,   59,    0,  136,    0,    0,  136,
  136,  136,  136,  136,  136,  136,    0,    0,    0,    0,
    0,    0,  194,   54,  137,   58,    0,    0,    0,    0,
    0,  141,    0,  137,    0,    0,   57,  195,  196,  197,
    0,    0,  137,  137,  137,  137,  137,    0,    0,    0,
    0,    0,    0,  137,    0,    0,   59,    0,    0,    0,
    0,    0,  141,    0,    0,    0,    0,  137,  137,    0,
  137,  137,  136,  142,  137,   55,   56,  137,  137,  137,
  137,  137,  137,  137,    0,    0,    0,    0,   57,    0,
    0,    0,    0,    0,  141,    0,    0,    0,    0,   23,
    0,    0,    0,    0,  142,    0,    0,    0,   24,    0,
    0,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,    0,    0,  136,  136,    0,    0,   73,    0,
    0,    0,    0,    0,    0,  383,  142,    0,    0,    0,
  137,    0,   40,   41,    0,   42,   43,    0,    0,   45,
    0,  199,   46,   47,   48,   49,   50,    0,   51,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   23,  199,    0,
    0,    0,  200,    0,    0,    0,   24,    0,    0,    0,
    0,    0,  137,  137,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,  113,    0,    0,  200,
    0,  199,    0,    0,  198,   54,    0,    0,    0,    0,
   40,   41,    0,   42,   43,    0,    0,   45,    0,   23,
   46,   47,   48,   49,   50,  141,   51,    0,   24,    0,
   58,  198,  200,    0,  141,    0,    0,   25,   26,   27,
   28,   29,    0,  141,  141,  141,  141,  141,    0,    0,
    0,    0,    0,    0,  141,    0,    0,   55,   56,    0,
    0,   59,    0,    0,  198,    0,    0,  142,  141,  141,
   58,  141,  141,    0,    0,  141,  142,    0,  141,  141,
  141,  141,  141,   54,  141,  142,  142,  142,  142,  142,
    0,    0,    0,   57,    0,    0,  142,    0,    0,    0,
    0,   59,    0,  439,  440,    0,    0,    0,    0,    0,
  142,  142,    0,  142,  142,    0,    0,  142,  530,    0,
  142,  142,  142,  142,  142,    0,  142,    0,    0,    0,
    0,    0,    0,   57,    0,   55,   56,    0,  183,  184,
  185,  141,  186,  187,  188,    0,  189,    0,    0,    0,
    0,    0,    0,  190,  191,    0,    0,    0,    0,    0,
  192,    0,    0,    0,    0,  183,  184,  185,  193,  186,
  187,  188,    0,  189,    0,    0,    0,    0,    0,    0,
  190,  191,    0,  142,    0,    0,    0,  192,  157,    0,
    0,    0,    0,  141,  141,  193,    0,    0,  183,  184,
  185,    0,  186,  187,  188,    0,  189,   58,    0,    0,
    0,    0,    0,  190,  191,    0,    0,    0,    0,    0,
  192,    0,    0,    0,    0,    0,    0,    0,  193,    0,
    0,    0,    0,    0,  217,  142,  142,    0,   59,    0,
    0,    0,  647,   24,    0,    0,    0,    0,    0,    0,
  310,    0,   25,   26,   27,   28,   29,    0,    0,    0,
  194,    0,    0,    0,  109,    0,  311,    0,    0,    0,
   57,    0,    0,    0,   23,  195,  196,  197,    0,  475,
    0,    0,    0,   24,    0,    0,    0,  194,    0,    0,
    0,  109,   25,   26,   27,   28,   29,    0,    0,    0,
    0,    0,  195,  196,  197,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  608,
  194,    0,    0,  765,    0,    0,    0,    0,    0,  312,
  313,  314,    0,  315,  316,  195,  196,  197,  317,    0,
  318,    0,   58,  319,  320,  321,  322,  323,  324,  325,
  326,  327,  328,    0,  329,  330,    0,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,  342,  343,
    0,  344,  345,   59,  310,    0,    0,  346,  347,  348,
  349,  350,  259,  351,  352,  353,  354,  355,  356,  357,
  311,  358,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  359,    0,    0,   57,    0,    0,    0,    0,
    0,   23,    0,    0,  360,  361,  362,  363,    0,    0,
   24,  364,    0,    0,    0,    0,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,    0,    0,    0,
    0,  156,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  312,  313,  314,    0,  315,  316,    0,
    0,    0,  317,    0,  318,    0,    0,  319,  320,  321,
  322,  323,  324,  325,  326,  327,  328,    0,  329,  330,
    0,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,    0,  344,  345,    0,    0,    0,
    0,  346,  347,  348,  349,  350,  257,  351,  352,  353,
  354,  355,  356,  357,  259,  358,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  359,    0,    0,    0,
  259,    0,    0,    0,    0,    0,    0,    0,  360,  361,
  362,  363,    0,    0,    0,  364,   23,    0,    0,    0,
    0,    0,    0,    0,    0,   24,    0,    0,    0,    0,
  764,    0,    0,    0,   25,   26,   27,   28,   29,    0,
  183,  184,  185,    0,  186,  187,  188,    0,  189,    0,
    0,    0,    0,    0,    0,  896,  897,    0,    0,  898,
    0,    0,  192,  259,  259,  259,    0,  259,  259,    0,
  193,    0,  259,    0,  259,    0,   58,  259,  259,  259,
  259,  259,  259,  259,  259,  259,  259,    0,  259,  259,
    0,  259,  259,  259,  259,  259,  259,  259,  259,  259,
  259,  259,  259,  259,    0,  259,  259,   59,  257,    0,
    0,  259,  259,  259,  259,  259,  260,  259,  259,  259,
  259,  259,  259,  259,  257,  259,  306,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  259,    0,    0,   57,
    0,    0,    0,    0,    0,    0,  113,    0,  259,  259,
  259,  259,    0,    0,    0,  259,    0,    0,    0,    0,
   40,   41,  899,   42,   43,    0,  109,   45,    0,    0,
   46,   47,   48,   49,   50,    0,   51,  900,  901,  902,
    0,    0,    0,    0,    0,    0,    0,  257,  257,  257,
    0,  257,  257,    0,    0,    0,  257,    0,  257,    0,
    0,  257,  257,  257,  257,  257,  257,  257,  257,  257,
  257,    0,  257,  257,    0,  257,  257,  257,  257,  257,
  257,  257,  257,  257,  257,  257,  257,  257,    0,  257,
  257,    0,    0,   54,    0,  257,  257,  257,  257,  257,
  258,  257,  257,  257,  257,  257,  257,  257,  260,  257,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  257,    0,    0,    0,  260,    0,    0,    0,    0,    0,
    0,    0,  257,  257,  257,  257,    0,    0,    0,  257,
   23,    0,    0,    0,    0,   55,   56,    0,    0,   24,
    0,    0,    0,    0,  165,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,    0,   58,    0,
  166,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  260,  260,  260,
    0,  260,  260,    0,    0,    0,  260,    0,  260,   59,
    0,  260,  260,  260,  260,  260,  260,  260,  260,  260,
  260,    0,  260,  260,  664,  260,  260,  260,  260,  260,
  260,  260,  260,  260,  260,  260,  260,  260,    0,  260,
  260,   57,  258,   58,    0,  260,  260,  260,  260,  260,
   58,  260,  260,  260,  260,  260,  260,  260,  258,  260,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  260,    0,    0,    0,   59,    0,    0,    0,    0,    0,
    0,   59,  260,  260,  260,  260,    0,    0,    0,  260,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   57,    0,    0,    0,
    0,    0,    0,   57,    0,  932,    0,    0,    0,    0,
    0,  258,  258,  258,    0,  258,  258,    0,    0,    0,
  258,    0,  258,    0,   58,  258,  258,  258,  258,  258,
  258,  258,  258,  258,  258,    0,  258,  258,    0,  258,
  258,  258,  258,  258,  258,  258,  258,  258,  258,  258,
  258,  258,    0,  258,  258,   59,    0,   58,    0,  258,
  258,  258,  258,  258,   58,  258,  258,  258,  258,  258,
  258,  258,   23,  258,    0,    0,    0,    0,    0,    0,
    0,   24,    0,    0,  258,    0,    0,   57,   59,    0,
   25,   26,   27,   28,   29,   59,  258,  258,  258,  258,
    0,    0,  156,  258,    0,    0,    0,    0,   58,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   57,    0,    0,    0,    0,    0,    0,   57,    0,  220,
  183,  184,  185,    0,  186,  187,  188,   23,  189,   59,
    0,   58,    0,    0,  217,  218,   24,    0,   58,  422,
    0,    0,  192,   24,  219,   25,   26,   27,   28,   29,
  193,    0,   25,   26,   27,   28,   29,    0,    0,    0,
    0,   57,   59,    0,    0,    0,   58,    0,    0,   59,
    0,   58,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   57,    0,    0,   59,    0,    0,
    0,   57,   59,    0,    0,    0,    0,    0,    0,    0,
    0,  183,  184,  185,    0,  186,  187,  188,   23,  189,
    0,    0,   58,    0,    0,    0,    0,   24,    0,   57,
  422,  100,    0,  192,   57,    0,   25,   26,   27,   28,
   29,  193,    0,    0,  183,  184,  185,  401,  186,  187,
  188,   23,  402,   59,    0,  893,   58,    0,  217,  218,
   24,  403,    0,  404,    0,    0,  192,   24,  219,   25,
   26,   27,   28,   29,  193,    0,   25,   26,   27,   28,
   29,    0,   58,    0,    0,   57,    0,   59,    0,    0,
    0,    0,    0,    0,    0,  183,  184,  185,  401,  186,
  187,  188,   23,  402,    0,    0,   58,    0,    0,    0,
    0,   24,  412,   59,  404,    0,    0,  192,    0,   57,
   25,   26,   27,   28,   29,  193,    0,    0,  183,  184,
  185,    0,  186,  187,  188,   23,  189,   59,    0,    0,
   58,    0,  217,  418,   24,   57,    0,  422,    0,    0,
  192,   24,  419,   25,   26,   27,   28,   29,  193,    0,
   25,   26,   27,   28,   29,    0,    0,    0,    0,   57,
  217,   59,    0,    0,    0,   23,    0,    0,    0,   24,
    0,    0,    0,    0,   24,  842,    0,    0,   25,   26,
   27,   28,   29,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,  277,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,    0,    0,    0,    0,   24,    0,    0,    0,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,    0,  764,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,   23,    0,    0,    0,
    0,    0,    0,  310,    0,   24,    0,    0,    0,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,  311,
  217,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,    0,    0,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  217,    0,    0,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,    0,
    0,    0,  312,  313,  314,    0,  315,  316,    0,    0,
    0,  317,    0,  318,    0,    0,  319,  320,  321,  322,
  323,  324,  325,  326,  327,  328,    0,  329,  330,    0,
  331,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,  342,  343,    0,  344,  345,    0,    0,    0,    0,
  346,  347,  348,  349,  350,    0,  351,  352,  353,  354,
  355,  356,  357,  249,  358,    0,  376,  376,    0,    0,
    0,    0,    0,    0,    0,  359,    0,    0,    0,    0,
    0,    0,    0,  113,    0,    0,    0,  360,  361,  362,
  363,    0,    0,    0,  364,    0,    0,   40,   41,    0,
   42,   43,    0,    0,   45,    0,    0,   46,   47,   48,
   49,   50,    0,   51,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  376,
  376,  376,    0,  376,  376,    0,    0,    0,  376,    0,
  376,    0,    0,  376,  376,  376,  376,  376,  376,  376,
  376,  376,  376,    0,  376,  376,    0,  376,  376,  376,
  376,  376,  376,  376,  376,  376,  376,  376,  376,  376,
   54,  376,  376,  109,  347,  347,    0,  376,  376,  376,
  376,  376,    0,  376,  376,  376,  376,  376,  376,  376,
    0,  376,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  376,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  376,  376,  376,  376,    0,    0,
    0,  376,   55,   56,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  347,  347,  347,
    0,  347,  347,    0,    0,    0,  347,    0,  347,    0,
    0,  347,  347,  347,  347,  347,  347,  347,  347,  347,
  347,    0,  347,  347,    0,  347,  347,  347,  347,  347,
  347,  347,  347,  347,  347,  347,  347,  347,    0,  347,
  347,  328,  328,    0,    0,  347,  347,  347,  347,  347,
    0,  347,  347,  347,  347,  347,  347,  347,    0,  347,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  347,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  347,  347,  347,  347,    0,    0,    0,  347,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  328,  328,  328,    0,  328,  328,
    0,    0,    0,  328,    0,  328,    0,    0,  328,  328,
  328,  328,  328,  328,  328,  328,  328,  328,    0,  328,
  328,    0,  328,  328,  328,  328,  328,  328,  328,  328,
  328,  328,  328,  328,  328,    0,  328,  328,  334,  334,
    0,    0,  328,  328,  328,  328,  328,    0,  328,  328,
  328,  328,  328,  328,  328,    0,  328,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  328,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  328,
  328,  328,  328,    0,    0,    0,  328,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  334,  334,  334,    0,  334,  334,    0,    0,    0,
  334,    0,  334,    0,    0,  334,  334,  334,  334,  334,
  334,  334,  334,  334,  334,    0,  334,  334,    0,  334,
  334,  334,  334,  334,  334,  334,  334,  334,  334,  334,
  334,  334,    0,  334,  334,  329,  329,    0,    0,  334,
  334,  334,  334,  334,    0,  334,  334,  334,  334,  334,
  334,  334,    0,  334,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  334,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  334,  334,  334,  334,
    0,    0,    0,  334,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  329,  329,
  329,    0,  329,  329,    0,    0,    0,  329,    0,  329,
    0,    0,  329,  329,  329,  329,  329,  329,  329,  329,
  329,  329,    0,  329,  329,    0,  329,  329,  329,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  329,    0,
  329,  329,  335,  335,    0,    0,  329,  329,  329,  329,
  329,    0,  329,  329,  329,  329,  329,  329,  329,    0,
  329,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  329,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  329,  329,  329,  329,    0,    0,    0,
  329,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  335,  335,  335,    0,  335,
  335,    0,    0,    0,  335,    0,  335,    0,    0,  335,
  335,  335,  335,  335,  335,  335,  335,  335,  335,    0,
  335,  335,    0,  335,  335,  335,  335,  335,  335,  335,
  335,  335,  335,  335,  335,  335,    0,  335,  335,  330,
  330,    0,    0,  335,  335,  335,  335,  335,    0,  335,
  335,  335,  335,  335,  335,  335,    0,  335,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  335,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  335,  335,  335,  335,    0,    0,    0,  335,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  330,  330,  330,    0,  330,  330,    0,    0,
    0,  330,    0,  330,    0,    0,  330,  330,  330,  330,
  330,  330,  330,  330,  330,  330,    0,  330,  330,    0,
  330,  330,  330,  330,  330,  330,  330,  330,  330,  330,
  330,  330,  330,    0,  330,  330,  340,  340,    0,    0,
  330,  330,  330,  330,  330,    0,  330,  330,  330,  330,
  330,  330,  330,    0,  330,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  330,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  330,  330,  330,
  330,    0,    0,    0,  330,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  340,
  340,  340,    0,  340,  340,    0,    0,    0,  340,    0,
  340,    0,    0,  340,  340,  340,  340,  340,  340,  340,
  340,  340,  340,    0,  340,  340,    0,  340,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,  340,  340,
    0,  340,  340,  362,  362,    0,    0,  340,  340,  340,
  340,  340,    0,  340,  340,  340,  340,  340,  340,  340,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  340,  340,  340,  340,    0,    0,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  362,  362,  362,    0,
  362,  362,    0,    0,    0,  362,    0,  362,    0,    0,
  362,  362,  362,  362,  362,  362,  362,  362,  362,  362,
    0,  362,  362,    0,  362,  362,  362,  362,  362,  362,
  362,  362,  362,  362,  362,  362,  362,    0,  362,  362,
  358,  358,    0,    0,  362,  362,  362,  362,  362,    0,
  362,  362,  362,  362,  362,  362,  362,    0,  362,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  362,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  362,  362,  362,  362,    0,    0,    0,  362,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  358,  358,  358,    0,  358,  358,    0,
    0,    0,  358,    0,  358,    0,    0,  358,  358,  358,
  358,  358,  358,  358,  358,  358,  358,    0,  358,  358,
    0,  358,  358,  358,  358,  358,  358,  358,  358,  358,
  358,  358,  358,  358,    0,  358,  358,  336,  336,    0,
    0,  358,  358,  358,  358,  358,    0,  358,  358,  358,
  358,  358,  358,  358,    0,  358,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  358,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  358,  358,
  358,  358,    0,    0,    0,  358,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  336,  336,  336,    0,  336,  336,    0,    0,    0,  336,
    0,  336,    0,    0,  336,  336,  336,  336,  336,  336,
  336,  336,  336,  336,    0,  336,  336,    0,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  336,
  336,    0,  336,  336,  331,  331,    0,    0,  336,  336,
  336,  336,  336,    0,  336,  336,  336,  336,  336,  336,
  336,    0,  336,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  336,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  336,  336,  336,  336,    0,
    0,    0,  336,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  331,  331,  331,
    0,  331,  331,    0,    0,    0,  331,    0,  331,    0,
    0,  331,  331,  331,  331,  331,  331,  331,  331,  331,
  331,    0,  331,  331,    0,  331,  331,  331,  331,  331,
  331,  331,  331,  331,  331,  331,  331,  331,    0,  331,
  331,  332,  332,    0,    0,  331,  331,  331,  331,  331,
    0,  331,  331,  331,  331,  331,  331,  331,    0,  331,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  331,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  331,  331,  331,  331,    0,    0,    0,  331,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  332,  332,  332,    0,  332,  332,
    0,    0,    0,  332,    0,  332,    0,    0,  332,  332,
  332,  332,  332,  332,  332,  332,  332,  332,    0,  332,
  332,    0,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,  332,  332,  332,    0,  332,  332,  337,  337,
    0,    0,  332,  332,  332,  332,  332,    0,  332,  332,
  332,  332,  332,  332,  332,    0,  332,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  332,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  332,
  332,  332,  332,    0,    0,    0,  332,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  337,  337,  337,    0,  337,  337,    0,    0,    0,
  337,    0,  337,    0,    0,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,    0,  337,  337,    0,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
  337,  337,    0,  337,  337,  345,  345,    0,    0,  337,
  337,  337,  337,  337,    0,  337,  337,  337,  337,  337,
  337,  337,    0,  337,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  337,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  337,  337,  337,  337,
    0,    0,    0,  337,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  345,  345,
  345,    0,  345,  345,    0,    0,    0,  345,    0,  345,
    0,    0,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,    0,  345,  345,    0,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,    0,
  345,  345,  338,  338,    0,    0,  345,  345,  345,  345,
  345,    0,  345,  345,  345,  345,  345,  345,  345,    0,
  345,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  345,  345,  345,  345,    0,    0,    0,
  345,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,  338,  338,    0,  338,
  338,    0,    0,    0,  338,    0,  338,    0,    0,  338,
  338,  338,  338,  338,  338,  338,  338,  338,  338,    0,
  338,  338,    0,  338,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,  338,  338,    0,  338,  338,  341,
  341,    0,    0,  338,  338,  338,  338,  338,    0,  338,
  338,  338,  338,  338,  338,  338,    0,  338,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  338,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  338,  338,  338,  338,    0,    0,    0,  338,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  341,  341,  341,    0,  341,  341,    0,    0,
    0,  341,    0,  341,    0,    0,  341,  341,  341,  341,
  341,  341,  341,  341,  341,  341,    0,  341,  341,    0,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
  341,  341,  341,    0,  341,  341,  359,  359,    0,    0,
  341,  341,  341,  341,  341,    0,  341,  341,  341,  341,
  341,  341,  341,    0,  341,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  341,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  341,  341,  341,
  341,    0,    0,    0,  341,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  359,
  359,  359,    0,  359,  359,    0,    0,    0,  359,    0,
  359,    0,    0,  359,  359,  359,  359,  359,  359,  359,
  359,  359,  359,    0,  359,  359,    0,  359,  359,  359,
  359,  359,  359,  359,  359,  359,  359,  359,  359,  359,
    0,  359,  359,  333,  333,    0,    0,  359,  359,  359,
  359,  359,    0,  359,  359,  359,  359,  359,  359,  359,
    0,  359,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  359,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  359,  359,  359,  359,    0,    0,
    0,  359,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  333,  333,  333,    0,
  333,  333,    0,    0,    0,  333,    0,  333,    0,    0,
  333,  333,  333,  333,  333,  333,  333,  333,  333,  333,
    0,  333,  333,    0,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  333,  333,  333,  333,    0,  333,  333,
  339,  339,    0,    0,  333,  333,  333,  333,  333,    0,
  333,  333,  333,  333,  333,  333,  333,    0,  333,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  333,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  333,  333,  333,  333,    0,    0,    0,  333,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  339,  339,  339,    0,  339,  339,    0,
    0,    0,  339,    0,  339,    0,    0,  339,  339,  339,
  339,  339,  339,  339,  339,  339,  339,    0,  339,  339,
    0,  339,  339,  339,  339,  339,  339,  339,  339,  339,
  339,  339,  339,  339,    0,  339,  339,  342,  342,    0,
    0,  339,  339,  339,  339,  339,    0,  339,  339,  339,
  339,  339,  339,  339,    0,  339,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  339,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  339,  339,
  339,  339,    0,    0,    0,  339,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  342,  342,  342,    0,  342,  342,    0,    0,    0,  342,
    0,  342,    0,    0,  342,  342,  342,  342,  342,  342,
  342,  342,  342,  342,    0,  342,  342,    0,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,  342,
  342,    0,  342,  342,  343,  343,    0,    0,  342,  342,
  342,  342,  342,    0,  342,  342,  342,  342,  342,  342,
  342,    0,  342,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  342,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  342,  342,  342,  342,    0,
    0,    0,  342,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  343,  343,  343,
    0,  343,  343,    0,    0,    0,  343,    0,  343,    0,
    0,  343,  343,  343,  343,  343,  343,  343,  343,  343,
  343,    0,  343,  343,    0,  343,  343,  343,  343,  343,
  343,  343,  343,  343,  343,  343,  343,  343,    0,  343,
  343,  344,  344,    0,    0,  343,  343,  343,  343,  343,
    0,  343,  343,  343,  343,  343,  343,  343,    0,  343,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  343,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  343,  343,  343,  343,    0,    0,    0,  343,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  344,  344,  344,    0,  344,  344,
    0,    0,    0,  344,    0,  344,    0,    0,  344,  344,
  344,  344,  344,  344,  344,  344,  344,  344,    0,  344,
  344,    0,  344,  344,  344,  344,  344,  344,  344,  344,
  344,  344,  344,  344,  344,  311,  344,  344,    0,    0,
    0,    0,  344,  344,  344,  344,  344,    0,  344,  344,
  344,  344,  344,  344,  344,    0,  344,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  344,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  344,
  344,  344,  344,    0,    0,    0,  344,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  312,  313,
  314,    0,  315,  316,    0,    0,    0,  317,    0,  318,
    0,    0,  319,  320,  321,  322,  323,  324,  325,  326,
  327,  328,    0,  329,  330,    0,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,  342,  343,  263,
  344,  345,    0,    0,    0,    0,  346,  347,  348,  349,
  350,    0,  351,  352,  353,  354,  355,  356,  357,    0,
  358,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  359,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  360,  361,  362,  363,    0,    0,    0,
  364,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  263,  263,  263,    0,  263,  263,    0,    0,
    0,  263,    0,  263,    0,    0,  263,  263,  263,  263,
  263,  263,  263,  263,  263,  263,    0,  263,  263,    0,
  263,  263,  263,  263,  263,  263,  263,  263,  263,  263,
  263,  263,  263,  264,  263,  263,    0,    0,    0,    0,
  263,  263,  263,  263,  263,    0,  263,  263,  263,  263,
  263,  263,  263,    0,  263,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  263,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  263,  263,  263,
  263,    0,    0,    0,  263,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  264,  264,  264,    0,
  264,  264,    0,    0,    0,  264,    0,  264,    0,    0,
  264,  264,  264,  264,  264,  264,  264,  264,  264,  264,
    0,  264,  264,    0,  264,  264,  264,  264,  264,  264,
  264,  264,  264,  264,  264,  264,  264,  265,  264,  264,
    0,    0,    0,    0,  264,  264,  264,  264,  264,    0,
  264,  264,  264,  264,  264,  264,  264,    0,  264,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  264,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  264,  264,  264,  264,    0,    0,    0,  264,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  265,  265,  265,    0,  265,  265,    0,    0,    0,  265,
    0,  265,    0,    0,  265,  265,  265,  265,  265,  265,
  265,  265,  265,  265,    0,  265,  265,    0,  265,  265,
  265,  265,  265,  265,  265,  265,  265,  265,  265,  265,
  265,  266,  265,  265,    0,    0,    0,    0,  265,  265,
  265,  265,  265,    0,  265,  265,  265,  265,  265,  265,
  265,    0,  265,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  265,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  265,  265,  265,  265,    0,
    0,    0,  265,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  266,  266,  266,    0,  266,  266,
    0,    0,    0,  266,    0,  266,    0,    0,  266,  266,
  266,  266,  266,  266,  266,  266,  266,  266,    0,  266,
  266,    0,  266,  266,  266,  266,  266,  266,  266,  266,
  266,  266,  266,  266,  266,    0,  266,  266,    0,    0,
    0,    0,  266,  266,  266,  266,  266,    0,  266,  266,
  266,  266,  266,  266,  266,    0,  266,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  266,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  315,    0,  266,
  266,  266,  266,    0,  318,    0,  266,  319,  320,  321,
  322,  323,  324,  325,  326,  327,  328,    0,  329,  330,
    0,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,  342,  343,    0,  344,  345,    0,    0,    0,
    0,  346,  347,  348,  349,  350,    0,  351,  352,  353,
  354,  355,  356,  357,    0,  358,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  359,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  360,  361,
  362,  363,    0,    0,    0,  364,
  };
  protected static readonly short [] yyCheck = {             6,
   61,   44,    6,   41,   41,   44,  173,   65,  121,   16,
  174,   40,  285,  286,  287,   73,   60,   91,   41,  570,
   41,   41,   41,  365,   41,   41,   40,   62,   33,   44,
  303,   44,   44,   44,   44,   44,   44,  556,    6,   33,
   93,   44,   44,   40,   40,   40,  107,   91,   16,  274,
   57,  123,   44,  253,   44,  636,  637,   64,   65,  172,
    0,   60,   44,  123,  763,  814,   73,  612,  123,   76,
  280,  123,   76,  123,  123,  123,  315,  260,   61,  123,
   87,  123,  265,  832,  124,   41,   20,   61,   44,  253,
   97,   98,  673,  674,  199,  200,   64,   65,  105,  263,
   40,  276,   42,  280,  111,   73,  281,  114,   76,  116,
  168,   93,  116,  120,  266,   44,  123,  263,  669,  123,
  291,  292,  129,  872,  123,   44,  133,  273,  135,   44,
  276,   44,  405,  125,  286,  125,  176,   65,   40,  179,
   42,  840,  149,  150,  383,  152,  114,   44,  116,  262,
  313,    0,   86,  324,   88,  123,   90,   61,   86,  740,
  741,  324,  743,  744,  709,   62,  711,  347,  124,  714,
  380,  351,  277,   61,  181,  182,  376,  324,  257,  258,
  259,  272,  261,  262,  263,  257,  265,  121,    0,  260,
  429,  772,   60,  281,  536,  378,  125,  270,  271,  206,
  279,  208,  369,  380,  371,  380,  125,  306,  287,   61,
  125,    0,  125,   41,  274,  379,   44,  380,  769,  274,
  264,  261,  274,   91,  274,  274,  274,  427,  235,  273,
   41,  980,  274,   44,   61,  334,  335,  383,  282,  283,
  284,  285,  286,   40,  825,  252,  250,  828,  829,   41,
   40,   40,   44,   42,  315,  123,   40,  315,  272,  804,
   41,  291,  292,   44,  271,  309,  257,  383,   41,  274,
  277,   44,  277,  257,  123,  272,  272,  272,  285,  286,
  274,   40,  274,  556,  274,  558,  320,  838,  322,  288,
  563,   41,   61,  274,   44,  329,  303,   41,  849,  850,
   44,  362,  274,   41,  383,  886,   44,    0,  315,  383,
   40,  318,  319,  320,  321,  322,  323,  324,  325,  326,
  327,  328,  329,  330,  331,  332,  333,  334,  268,  269,
  383,  604,  123,  340,  341,  275,   41,  260,  345,   44,
  313,  892,  295,  296,  383,  383,  383,  315,  383,  289,
  290,  324,  381,  360,  361,  362,  907,  364,  362,  910,
  383,  260,  383,  383,  383,  532,  383,  383,  383,   40,
  383,  383,  383,  383,  383,  383,  895,  257,  385,  437,
  383,  383,  267,   41,  551,  325,   44,    0,  939,  940,
  434,  267,  435,   40,  362,  306,  264,   42,    0,  566,
   40,  274,   42,  123,   44,  273,  957,  380,  316,   40,
  123,   42,   40,   44,  282,  283,  284,  285,  286,  268,
  269,  123,   41,  272,  273,  274,  275,  257,  380,  436,
  437,  324,  436,  291,  292,  442,  144,  295,  296,  446,
  289,  290,  449,   40,   40,  452,  295,  296,   40,   44,
   41,  458,  459,   44,  461,  442,  268,  269,   58,  446,
  272,  273,  449,  275,  322,  472,  473,  528,  436,  437,
   58,  458,  479,  480,  481,  324,  325,  289,  290,  268,
  269,   58,  649,  272,  273,  125,  275,   62,   41,  257,
  198,  199,  200,  660,  125,  662,   41,   61,  505,   44,
  289,  290,   40,  257,   44,    0,  291,  292,  216,  380,
  295,  296,   40,  325,  382,  522,   40,  380,    0,  526,
  527,  528,   41,  527,  528,   44,  799,  800,   44,   40,
   41,   42,   60,   44,   44,   41,  325,  260,   44,  546,
  547,  548,   40,   41,   42,  257,   44,   61,  256,  556,
   58,  558,  313,  333,  423,  380,  563,  257,  526,  527,
  528,   44,  257,   91,  272,  273,  274,  389,  575,  277,
  843,  575,  389,  389,  380,  268,  269,   44,  380,  272,
  273,   44,  275,   40,  751,  643,  294,  124,  273,    0,
   44,   44,   44,  760,   44,  123,  289,  290,   44,   44,
   44,  608,  389,  610,  312,  612,  314,  575,  316,  260,
  617,  618,  619,  620,  621,  622,  623,  624,  625,  626,
  627,  628,  895,   60,  389,  738,  739,  335,  336,  337,
  338,  339,  325,  640,  342,  642,  643,    0,  346,  347,
  348,  349,  350,  351,  352,  353,  354,  355,  356,  357,
   86,  132,   88,   89,   91,  268,  269,  821,  822,  272,
  273,  389,  275,    0,  671,  389,  268,  269,  389,   62,
  272,  273,  640,  275,  642,  643,  289,  290,  389,  389,
  389,  257,  389,  391,  257,  389,  123,  289,  290,  257,
  389,  389,  380,  380,  130,  131,  257,   44,  134,  180,
  136,  266,  709,   44,  711,   40,   44,  714,  766,  427,
  823,  824,  325,  826,  827,  879,  880,    0,  882,  883,
   44,   44,   44,  325,  205,   44,  207,   44,   44,  257,
  258,  259,   44,  261,  262,  263,   44,  265,   44,   44,
  747,  222,   44,  747,  272,  273,   44,   44,   44,   44,
   44,  279,   44,  260,   44,  260,  763,  764,  257,  287,
   44,  925,  926,  927,  257,  257,    0,  475,  881,  276,
   91,  884,  885,  268,  269,  389,   44,  272,  273,  747,
  275,   41,   44,   44,   44,  949,  268,  269,  269,   44,
  272,  273,   44,  275,  289,  290,   44,  804,    0,   44,
   60,  282,   44,  284,  811,  812,   44,  289,  290,   44,
  257,   44,  319,  320,  321,  928,   44,   44,   44,  326,
  327,   44,  329,  330,  331,  332,   44,  264,   44,   44,
  325,   91,   44,  840,   44,   91,  273,  545,  260,   44,
  847,  845,  389,  325,   44,  282,  283,  284,  285,  286,
  389,  379,   44,  380,   44,  380,  293,  268,  269,   44,
  380,  272,  273,  123,  275,   44,  394,  395,  396,   40,
  307,  308,   44,  310,  311,   44,   44,  314,  289,  290,
  317,  318,  319,  320,  321,  322,  323,  328,  895,  389,
  257,  380,  380,  601,  602,  603,  257,  605,  606,  257,
  380,   44,   93,  260,  381,  268,  269,   40,  616,  272,
  273,  392,  275,   40,  325,   40,  328,  257,  328,  380,
   44,  123,   40,  257,  257,  333,  289,  290,  333,  380,
  328,  268,  269,   44,   44,  272,  273,  645,  275,  257,
  389,  389,  257,  380,  951,  653,  389,  380,  380,   44,
  125,  432,  289,  290,  257,    0,  963,  964,  965,  272,
  441,  272,  325,  444,  445,  272,  447,  448,  257,  450,
  451,  272,  453,  454,  455,  456,  457,   10,   92,  460,
  216,  462,  463,  464,  465,  268,  269,  272,  325,  272,
  273,  215,  275,   20,  247,  432,  433,  257,  258,  259,
  286,  261,  262,  263,  264,  265,  289,  290,  289,  558,
  718,  105,  272,  273,   91,  235,  890,   61,  374,  279,
  252,   60,  282,  283,  284,  285,  286,  287,  736,  799,
  563,  428,  368,  293,  268,  269,  800,  368,  272,  273,
  429,  275,  325,  524,  847,  735,  840,  307,  308,  429,
  310,  311,   91,   -1,  314,  289,  290,  317,  318,  319,
  320,  321,  641,  323,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,  274,  275,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  123,  260,   -1,  289,  290,  797,
  798,  325,   -1,  295,  296,   -1,  577,  279,    0,   -1,
  581,  276,   -1,  584,   -1,  287,  587,    0,   -1,   -1,
   -1,   -1,  593,  594,  316,  596,   -1,   -1,   -1,  379,
  380,   -1,  324,  325,   -1,  833,  525,   -1,   -1,   -1,
   -1,   -1,  613,  614,  394,  395,  396,   -1,   40,   41,
   42,   -1,   44,   -1,  319,  320,  321,   -1,  629,  630,
  631,  326,  327,   -1,  329,  330,  331,  332,   60,   -1,
   62,   -1,   -1,  268,  269,   -1,   -1,  272,  273,  877,
  275,   -1,  432,  433,  573,  574,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  289,  290,   -1,   -1,   -1,   91,
   -1,   93,   -1,   -1,   -1,  676,   -1,  678,  679,   -1,
  681,  682,   -1,  684,  685,   -1,  687,  688,  689,  690,
  691,   -1,   -1,  694,   -1,  696,  697,  698,  699,   -1,
  325,  123,   -1,  125,   -1,  264,   -1,  935,  936,  937,
   -1,  712,   -1,   -1,  273,   -1,   -1,   -1,   -1,  638,
  639,   -1,  641,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  970,   -1,   -1,   -1,   -1,  306,  307,  308,
   -1,  310,  311,   -1,   -1,  314,  675,   -1,  317,  318,
  319,  320,  321,   -1,  323,  766,   -1,   -1,   -1,   -1,
   -1,   60,   -1,  774,   -1,   -1,  777,   -1,   -1,  780,
    0,   -1,  783,   -1,   -1,   -1,   -1,   -1,  789,  790,
   -1,  792,   -1,   -1,   -1,   -1,   -1,   -1,   41,  291,
  292,   44,   91,  295,  296,  297,  298,  299,  300,  301,
  302,  303,  304,  305,  815,  816,  817,   60,    0,   -1,
  312,  380,   -1,  742,   -1,   -1,  745,  746,   -1,   -1,
  322,    0,   -1,   -1,  123,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  845,   -1,  268,  269,   91,   -1,
  272,  273,  274,  275,  276,  268,  269,  279,   -1,  272,
  273,   -1,  275,   -1,   -1,  287,   -1,  289,  290,  870,
  871,  293,   -1,  432,  433,   -1,  289,  290,   -1,   -1,
  123,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,   -1,  314,  315,   -1,  317,  318,  319,  320,  321,
   -1,  323,   -1,  325,   -1,   40,   41,   -1,   -1,   44,
   -1,  830,  325,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   62,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,   93,   -1,
   60,  383,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,  264,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,  409,  123,   -1,
  125,   91,   -1,  282,  283,  284,  285,  286,   -1,  421,
  422,  423,  424,   -1,  426,   -1,  428,  429,   -1,   -1,
  432,  433,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  264,  265,  123,   -1,   -1,   -1,   -1,   -1,  272,
  273,   40,   -1,   42,   -1,   -1,  279,   -1,   -1,  282,
  283,  284,  285,  286,  287,   -1,   -1,   -1,  268,  269,
  293,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,  289,
  290,  314,   -1,   -1,  317,  318,  319,  320,  321,   -1,
  323,   44,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,  381,  275,   -1,   -1,   -1,   60,   -1,  268,
  269,   -1,   -1,  272,  273,  325,  275,  289,  290,  399,
  400,  401,  402,  403,  404,  405,  406,  407,  408,   -1,
  289,  290,  257,   -1,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  379,  380,  273,  274,
   40,   -1,   42,  325,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  394,  395,  396,   -1,   -1,  325,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,  432,
  433,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,   -1,   -1,   -1,   -1,  384,
  385,  386,  387,  388,  389,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,  273,  274,   40,   -1,   42,   -1,
   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,
  380,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  287,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,  125,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,   -1,   -1,  273,  274,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,  379,   -1,   -1,  428,
  383,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  394,  395,  396,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   60,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,   -1,  378,  379,
   91,   -1,   -1,   -1,  384,  385,  386,  387,  388,  260,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,  273,
  274,   40,   -1,   42,   -1,  276,   -1,   -1,   -1,  409,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  319,  320,
  321,   -1,   -1,   -1,   -1,  326,  327,   -1,  329,  330,
  331,  332,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,  273,  274,   40,   -1,   42,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,  264,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,  125,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,   -1,   -1,  273,  274,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,   -1,  363,   40,   -1,   42,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   60,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,   91,   -1,   -1,   -1,  384,  385,  386,  387,  388,
  260,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
  273,  274,   40,   -1,   42,   -1,  276,   -1,   -1,   -1,
  409,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  319,
  320,  321,   -1,   -1,   -1,   -1,  326,  327,   -1,  329,
  330,  331,  332,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,  273,  274,   40,   -1,
   42,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,  264,  428,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   -1,   -1,  273,  274,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,  363,   40,   -1,   42,  261,  262,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   60,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,   91,   -1,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,  273,  274,   40,   -1,   42,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,
  414,  415,  416,  417,  418,  419,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,  273,  274,   40,
   -1,   42,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,  264,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,   -1,   -1,  273,  274,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,   -1,  363,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   40,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   60,   -1,   -1,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,  273,  274,   -1,   -1,   -1,   -1,   -1,   40,
   -1,   42,  409,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   60,
   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   91,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,  123,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,   -1,
  123,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   91,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   41,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,  123,   60,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  287,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   60,  265,   -1,   -1,   -1,   -1,   -1,
   -1,  272,  273,   -1,   -1,   -1,   60,   -1,  279,   -1,
   -1,  264,   -1,   -1,   -1,  123,  287,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   91,   -1,   -1,
  293,   -1,   60,   -1,   -1,  298,  299,  300,  301,  302,
  303,  304,  305,  306,  307,  308,  123,  310,  311,  312,
   -1,  314,  379,   -1,  317,  318,  319,  320,  321,  123,
  323,  125,   -1,   91,   -1,   -1,   -1,  394,  395,  396,
   -1,  334,  335,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,  123,   -1,  279,  379,  426,
   60,   -1,   -1,   -1,   -1,  287,   -1,   -1,   -1,   -1,
   -1,  293,   -1,  394,  395,  396,   -1,  380,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,   91,  314,   -1,   -1,  317,  318,  319,  320,  321,
   -1,  323,   -1,   -1,   -1,  426,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  278,   -1,   -1,  123,  282,  283,  284,  285,  286,  432,
  433,   -1,   -1,   -1,   -1,   60,  294,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   -1,   -1,  272,  273,  379,  380,   -1,
  264,  383,  279,   -1,   -1,   -1,   91,   -1,   -1,  273,
  287,   -1,  394,  395,  396,   -1,  293,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  307,  308,   -1,  310,  311,   -1,  264,  314,  123,   -1,
  317,  318,  319,  320,  321,  273,  323,   -1,   -1,   -1,
  432,  433,   -1,   60,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,   91,   -1,  314,   -1,   -1,  317,
  318,  319,  320,  321,  322,  323,   -1,   -1,   -1,   -1,
   -1,   -1,  379,  380,  264,   60,   -1,   -1,   -1,   -1,
   -1,   60,   -1,  273,   -1,   -1,  123,  394,  395,  396,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,  293,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,  307,  308,   -1,
  310,  311,  380,   60,  314,  432,  433,  317,  318,  319,
  320,  321,  322,  323,   -1,   -1,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,  432,  433,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,   44,  123,   -1,   -1,   -1,
  380,   -1,  307,  308,   -1,  310,  311,   -1,   -1,  314,
   -1,   60,  317,  318,  319,  320,  321,   -1,  323,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   60,   -1,
   -1,   -1,   91,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,  432,  433,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   91,
   -1,   60,   -1,   -1,  123,  380,   -1,   -1,   -1,   -1,
  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,  264,
  317,  318,  319,  320,  321,  264,  323,   -1,  273,   -1,
   60,  123,   91,   -1,  273,   -1,   -1,  282,  283,  284,
  285,  286,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   -1,  432,  433,   -1,
   -1,   91,   -1,   -1,  123,   -1,   -1,  264,  307,  308,
   60,  310,  311,   -1,   -1,  314,  273,   -1,  317,  318,
  319,  320,  321,  380,  323,  282,  283,  284,  285,  286,
   -1,   -1,   -1,  123,   -1,   -1,  293,   -1,   -1,   -1,
   -1,   91,   -1,  348,  349,   -1,   -1,   -1,   -1,   -1,
  307,  308,   -1,  310,  311,   -1,   -1,  314,  125,   -1,
  317,  318,  319,  320,  321,   -1,  323,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,  432,  433,   -1,  257,  258,
  259,  380,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,  257,  258,  259,  287,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,  380,   -1,   -1,   -1,  279,   41,   -1,
   -1,   -1,   -1,  432,  433,  287,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   60,   -1,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,
   -1,   -1,   -1,   -1,  264,  432,  433,   -1,   91,   -1,
   -1,   -1,  125,  273,   -1,   -1,   -1,   -1,   -1,   -1,
  257,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  379,   -1,   -1,   -1,  383,   -1,  273,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  264,  394,  395,  396,   -1,  309,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  379,   -1,   -1,
   -1,  383,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,  394,  395,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  309,
  379,   -1,   -1,   41,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,  394,  395,  396,  345,   -1,
  347,   -1,   60,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   91,  257,   -1,   -1,  384,  385,  386,
  387,  388,  125,  390,  391,  392,  393,  394,  395,  396,
  273,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
  273,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,  125,  390,  391,  392,
  393,  394,  395,  396,  257,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  278,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,  276,
   -1,   -1,  279,  336,  337,  338,   -1,  340,  341,   -1,
  287,   -1,  345,   -1,  347,   -1,   60,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   91,  257,   -1,
   -1,  384,  385,  386,  387,  388,  125,  390,  391,  392,
  393,  394,  395,  396,  273,  398,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,
  307,  308,  379,  310,  311,   -1,  383,  314,   -1,   -1,
  317,  318,  319,  320,  321,   -1,  323,  394,  395,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,   -1,   -1,  380,   -1,  384,  385,  386,  387,  388,
  125,  390,  391,  392,  393,  394,  395,  396,  257,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
  264,   -1,   -1,   -1,   -1,  432,  433,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   91,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   41,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,  123,  257,   60,   -1,  384,  385,  386,  387,  388,
   60,  390,  391,  392,  393,  394,  395,  396,  273,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   91,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   41,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   60,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,   91,   -1,   60,   -1,  384,
  385,  386,  387,  388,   60,  390,  391,  392,  393,  394,
  395,  396,  264,  398,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,  409,   -1,   -1,  123,   91,   -1,
  282,  283,  284,  285,  286,   91,  421,  422,  423,  424,
   -1,   -1,  294,  428,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,  125,
  257,  258,  259,   -1,  261,  262,  263,  264,  265,   91,
   -1,   60,   -1,   -1,  264,  265,  273,   -1,   60,  276,
   -1,   -1,  279,  273,  274,  282,  283,  284,  285,  286,
  287,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,  123,   91,   -1,   -1,   -1,   60,   -1,   -1,   91,
   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   91,   -1,   -1,
   -1,  123,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,  264,  265,
   -1,   -1,   60,   -1,   -1,   -1,   -1,  273,   -1,  123,
  276,  125,   -1,  279,  123,   -1,  282,  283,  284,  285,
  286,  287,   -1,   -1,  257,  258,  259,  260,  261,  262,
  263,  264,  265,   91,   -1,   93,   60,   -1,  264,  265,
  273,  274,   -1,  276,   -1,   -1,  279,  273,  274,  282,
  283,  284,  285,  286,  287,   -1,  282,  283,  284,  285,
  286,   -1,   60,   -1,   -1,  123,   -1,   91,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   -1,   -1,   60,   -1,   -1,   -1,
   -1,  273,  274,   91,  276,   -1,   -1,  279,   -1,  123,
  282,  283,  284,  285,  286,  287,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,  264,  265,   91,   -1,   -1,
   60,   -1,  264,  265,  273,  123,   -1,  276,   -1,   -1,
  279,  273,  274,  282,  283,  284,  285,  286,  287,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,  123,
  264,   91,   -1,   -1,   -1,  264,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,  282,  283,
  284,  285,  286,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,  257,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,  273,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,  273,  398,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  293,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,   -1,  314,   -1,   -1,  317,  318,  319,
  320,  321,   -1,  323,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
  380,  378,  379,  383,  273,  274,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
   -1,  428,  432,  433,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,  273,  274,   -1,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,  273,  274,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,  273,  274,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,  273,
  274,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,  273,  274,   -1,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,   -1,  378,  379,
  273,  274,   -1,   -1,  384,  385,  386,  387,  388,   -1,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,  273,  274,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,  273,  274,   -1,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,  273,  274,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,  273,  274,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,  273,
  274,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,  273,  274,   -1,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,   -1,  378,  379,
  273,  274,   -1,   -1,  384,  385,  386,  387,  388,   -1,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,  273,  274,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,  273,  274,   -1,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,  273,  378,  379,   -1,   -1,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,  273,
  378,  379,   -1,   -1,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,  273,  378,  379,   -1,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,  273,  378,  379,
   -1,   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,  273,  378,  379,   -1,   -1,   -1,   -1,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  340,   -1,  421,
  422,  423,  424,   -1,  347,   -1,  428,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,
  };

#line 1409 "Iril/IR/IR.jay"

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
  public const int EXTERNALLY_INITIALIZED = 316;
  public const int NONNULL = 317;
  public const int NOCAPTURE = 318;
  public const int WRITEONLY = 319;
  public const int READONLY = 320;
  public const int READNONE = 321;
  public const int HIDDEN = 322;
  public const int BYVAL = 323;
  public const int ATTRIBUTE_GROUP_REF = 324;
  public const int ATTRIBUTES = 325;
  public const int NORECURSE = 326;
  public const int NOUNWIND = 327;
  public const int UNWIND = 328;
  public const int SPECULATABLE = 329;
  public const int SSP = 330;
  public const int UWTABLE = 331;
  public const int ARGMEMONLY = 332;
  public const int SEQ_CST = 333;
  public const int DSO_LOCAL = 334;
  public const int DSO_PREEMPTABLE = 335;
  public const int RET = 336;
  public const int BR = 337;
  public const int SWITCH = 338;
  public const int INDIRECTBR = 339;
  public const int INVOKE = 340;
  public const int RESUME = 341;
  public const int CATCHSWITCH = 342;
  public const int CATCHRET = 343;
  public const int CLEANUPRET = 344;
  public const int UNREACHABLE = 345;
  public const int FNEG = 346;
  public const int ADD = 347;
  public const int NUW = 348;
  public const int NSW = 349;
  public const int FADD = 350;
  public const int SUB = 351;
  public const int FSUB = 352;
  public const int MUL = 353;
  public const int FMUL = 354;
  public const int UDIV = 355;
  public const int SDIV = 356;
  public const int FDIV = 357;
  public const int UREM = 358;
  public const int SREM = 359;
  public const int FREM = 360;
  public const int SHL = 361;
  public const int LSHR = 362;
  public const int EXACT = 363;
  public const int ASHR = 364;
  public const int AND = 365;
  public const int OR = 366;
  public const int XOR = 367;
  public const int EXTRACTELEMENT = 368;
  public const int INSERTELEMENT = 369;
  public const int SHUFFLEVECTOR = 370;
  public const int EXTRACTVALUE = 371;
  public const int INSERTVALUE = 372;
  public const int ALLOCA = 373;
  public const int LOAD = 374;
  public const int STORE = 375;
  public const int FENCE = 376;
  public const int CMPXCHG = 377;
  public const int ATOMICRMW = 378;
  public const int GETELEMENTPTR = 379;
  public const int ALIGN = 380;
  public const int INBOUNDS = 381;
  public const int INRANGE = 382;
  public const int ADDRSPACE = 383;
  public const int TRUNC = 384;
  public const int ZEXT = 385;
  public const int SEXT = 386;
  public const int FPTRUNC = 387;
  public const int FPEXT = 388;
  public const int TO = 389;
  public const int FPTOUI = 390;
  public const int FPTOSI = 391;
  public const int UITOFP = 392;
  public const int SITOFP = 393;
  public const int PTRTOINT = 394;
  public const int INTTOPTR = 395;
  public const int BITCAST = 396;
  public const int ADDRSPACECAST = 397;
  public const int ICMP = 398;
  public const int EQ = 399;
  public const int NE = 400;
  public const int UGT = 401;
  public const int UGE = 402;
  public const int ULT = 403;
  public const int ULE = 404;
  public const int SGT = 405;
  public const int SGE = 406;
  public const int SLT = 407;
  public const int SLE = 408;
  public const int FCMP = 409;
  public const int OEQ = 410;
  public const int OGT = 411;
  public const int OGE = 412;
  public const int OLT = 413;
  public const int OLE = 414;
  public const int ONE = 415;
  public const int ORD = 416;
  public const int UEQ = 417;
  public const int UNE = 418;
  public const int UNO = 419;
  public const int FAST = 420;
  public const int PHI = 421;
  public const int SELECT = 422;
  public const int CALL = 423;
  public const int TAIL = 424;
  public const int VA_ARG = 425;
  public const int ASM = 426;
  public const int SIDEEFFECT = 427;
  public const int LANDINGPAD = 428;
  public const int CATCH = 429;
  public const int CATCHPAD = 430;
  public const int CLEANUPPAD = 431;
  public const int NOUNDEF = 432;
  public const int IMMARG = 433;
  public const int ATOMIC = 434;
  public const int MONOTONIC = 435;
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
