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
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' SECTION STRING ',' ALIGN INTEGER",
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
//t    "type : type optional_addrspace '*'",
//t    "type : type optional_addrspace '*' ALIGN INTEGER",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "addrspace : ADDRSPACE '(' INTEGER ')'",
//t    "optional_addrspace :",
//t    "optional_addrspace : ADDRSPACE '(' INTEGER ')'",
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
//t    "define_tail : function_addr attribute_group_refs SECTION STRING",
//t    "define_tail : attribute_group_refs",
//t    "define_tail : attribute_group_refs ALIGN INTEGER",
//t    "define_tail : attribute_group_refs personality_function",
//t    "define_tail : attribute_group_refs ALIGN INTEGER personality_function",
//t    "define_tail : attribute_group_refs SECTION STRING",
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
//t    "function_addr : function_addr_type optional_addrspace",
//t    "function_addr : function_addr_type optional_addrspace EXTERNALLY_INITIALIZED",
//t    "function_addr : optional_addrspace EXTERNALLY_INITIALIZED",
//t    "function_addr : addrspace",
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
//t    "optional_fast :",
//t    "optional_fast : FAST",
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
//t    "instruction : TAIL CALL FAST return_type function_pointer function_args attribute_group_refs",
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
//t    "instruction : FADD optional_fast type value ',' value",
//t    "instruction : FCMP optional_fast fcmp_condition type value ',' value",
//t    "instruction : FDIV optional_fast type value ',' value",
//t    "instruction : FENCE atomic_constraint",
//t    "instruction : FMUL optional_fast type value ',' value",
//t    "instruction : FPEXT typed_value TO type",
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
//t    "instruction : FPTRUNC typed_value TO type",
//t    "instruction : FSUB optional_fast type value ',' value",
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
//t    "instruction : SELECT optional_fast type value ',' typed_value ',' typed_value",
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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 22:
#line 141 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-12+yyTop], (LType)yyVals[-7+yyTop], (Value)yyVals[-6+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-8+yyTop]);
    }
  break;
case 23:
#line 145 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 24:
#line 149 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 153 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 26:
#line 157 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 27:
#line 161 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 28:
#line 165 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 29:
#line 169 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 30:
#line 173 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 31:
#line 177 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 32:
#line 181 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 33:
#line 185 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 34:
#line 189 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 35:
#line 193 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 36:
#line 197 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 37:
#line 201 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 38:
#line 202 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 206 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 40:
#line 207 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
#line 208 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 42:
#line 209 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 210 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 211 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
#line 212 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 46:
#line 213 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 47:
#line 214 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 48:
#line 218 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 49:
#line 222 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 50:
  case_50();
  break;
case 51:
  case_51();
  break;
case 52:
#line 239 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 53:
#line 240 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 54:
#line 241 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 55:
#line 245 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 56:
#line 249 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 67:
#line 278 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 282 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 69:
#line 289 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 70:
#line 293 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
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
case 73:
#line 305 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 92:
#line 339 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 93:
#line 343 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 347 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 95:
#line 354 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 358 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 363 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 101:
#line 369 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 102:
#line 370 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 103:
#line 371 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 104:
#line 372 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 105:
#line 376 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 106:
#line 380 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 107:
#line 384 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-2+yyTop], 0);
    }
  break;
case 108:
#line 388 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-4+yyTop], 0);
    }
  break;
case 109:
#line 392 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 396 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 111:
#line 400 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 115:
#line 416 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 116:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 428 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 119:
  case_119();
  break;
case 120:
  case_120();
  break;
case 132:
#line 462 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 133:
#line 466 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 134:
#line 470 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 135:
#line 474 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 136:
#line 478 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 137:
#line 485 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 138:
#line 489 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 139:
#line 493 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 144:
#line 504 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 147:
#line 516 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 520 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 149:
#line 524 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 150:
#line 528 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 532 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 542 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 156:
#line 543 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 157:
#line 550 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 554 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 561 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 565 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 161:
#line 569 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 162:
#line 573 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-2+yyTop]);
    }
  break;
case 163:
#line 577 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 164:
#line 581 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 166:
#line 589 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 593 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 168:
#line 594 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 169:
#line 595 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoUndef; }
  break;
case 170:
#line 596 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ImmediateArgument; }
  break;
case 171:
#line 597 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 172:
#line 598 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 173:
#line 599 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 174:
#line 600 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 175:
#line 601 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 176:
#line 602 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 177:
#line 603 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 178:
#line 604 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 179:
#line 605 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 180:
#line 606 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 181:
#line 607 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 182:
#line 611 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 183:
#line 615 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 195:
#line 645 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 196:
#line 646 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 197:
#line 647 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 198:
#line 648 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 199:
#line 649 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 200:
#line 650 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 201:
#line 651 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 202:
#line 652 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 203:
#line 653 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 204:
#line 654 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 205:
#line 658 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 206:
#line 659 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 207:
#line 660 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 208:
#line 661 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 209:
#line 662 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 210:
#line 663 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 211:
#line 664 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 212:
#line 665 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 213:
#line 666 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 214:
#line 667 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 215:
#line 668 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 216:
#line 669 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 217:
#line 670 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 218:
#line 671 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 219:
#line 672 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 220:
#line 673 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 221:
#line 677 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 225:
#line 687 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 226:
#line 691 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 227:
#line 695 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 228:
#line 699 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 229:
#line 703 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 230:
#line 707 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 711 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 232:
#line 715 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 233:
#line 719 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 234:
#line 723 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 236:
#line 731 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 237:
#line 732 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 238:
#line 733 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 239:
#line 734 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 240:
#line 735 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 241:
#line 736 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 242:
#line 737 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 243:
#line 738 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 244:
#line 739 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 245:
#line 746 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 753 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 757 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 248:
#line 764 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 771 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 775 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 782 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 790 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 254:
#line 797 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 801 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 812 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 816 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 823 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 827 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 834 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 262:
#line 838 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 263:
#line 845 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 265:
#line 853 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 267:
#line 864 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 868 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 875 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 879 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 883 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 887 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 895 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 275:
#line 896 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 276:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 907 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 914 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 279:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 280:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 281:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 282:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 283:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 285:
#line 942 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = new SymbolValue ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 954 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 289:
#line 958 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 290:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 291:
#line 966 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 299:
#line 986 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 990 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 996 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 302:
#line 1003 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1007 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1014 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1032 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 311:
#line 1039 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1046 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 319:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 321:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1104 "Iril/IR/IR.jay"
  {
        yyVal = false;
    }
  break;
case 326:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 327:
#line 1115 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 328:
#line 1119 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 329:
#line 1123 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 330:
#line 1127 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 331:
#line 1131 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 332:
#line 1135 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1139 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 334:
#line 1143 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 335:
#line 1147 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1151 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 337:
#line 1155 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 338:
#line 1159 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 339:
#line 1163 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 340:
#line 1167 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 341:
#line 1171 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 342:
#line 1175 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 343:
#line 1179 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 344:
#line 1183 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 345:
#line 1187 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 346:
#line 1191 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 347:
#line 1195 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 348:
#line 1199 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 349:
#line 1203 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 350:
#line 1207 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 351:
#line 1211 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 352:
#line 1215 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 353:
#line 1219 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 354:
#line 1223 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 355:
#line 1227 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1231 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1235 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1239 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1243 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1247 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1251 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1255 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1259 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1263 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1267 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1271 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1275 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 368:
#line 1279 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1283 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1287 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1291 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1295 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 373:
#line 1299 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 374:
#line 1303 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 375:
#line 1307 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: false);
    }
  break;
case 376:
#line 1311 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: true);
    }
  break;
case 377:
#line 1315 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: false);
    }
  break;
case 378:
#line 1319 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 379:
#line 1323 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-6+yyTop], (TypedValue)yyVals[-4+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 380:
#line 1327 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 381:
#line 1331 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 382:
#line 1335 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 383:
#line 1339 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 384:
#line 1343 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 385:
#line 1347 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 386:
#line 1351 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 387:
#line 1355 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 388:
#line 1359 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 389:
#line 1363 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 390:
#line 1367 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 391:
#line 1371 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 392:
#line 1375 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 393:
#line 1379 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 394:
#line 1383 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 395:
#line 1387 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 396:
#line 1391 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 397:
#line 1395 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 398:
#line 1399 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 399:
#line 1403 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 400:
#line 1407 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 401:
#line 1411 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 402:
#line 1415 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 403:
#line 1419 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 404:
#line 1423 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 405:
#line 1427 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 406:
#line 1431 "Iril/IR/IR.jay"
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

void case_50()
#line 227 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_51()
#line 232 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_119()
#line 433 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_120()
#line 438 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,
    6,    6,    6,    6,    6,    6,   11,   11,   10,   10,
   10,   10,   10,   10,   10,   10,   10,   17,   14,    9,
    9,   18,   18,   18,   18,   18,   19,   22,   22,   23,
   24,   24,   24,   24,   24,   24,   16,   16,    8,    8,
    8,    8,    8,   26,   26,   26,    7,    7,   28,   28,
   28,   28,   28,   28,   28,   28,   28,   28,   28,   28,
   28,    3,    3,    3,   29,   29,   30,   30,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   33,   32,   32,   31,   31,   34,   34,    4,    4,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   35,   35,   35,   35,   35,   42,   42,   42,   42,
   42,   42,   42,   40,   46,   46,    5,    5,    5,    5,
    5,   47,   47,   47,   36,   36,   48,   48,   49,   49,
   49,   49,   49,   49,   43,   43,   41,   41,   41,   41,
   41,   41,   41,   41,   41,   41,   41,   41,   41,   41,
   41,   41,   41,   50,   50,   15,   15,   15,   15,   44,
   44,   39,   39,   51,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   52,   53,   53,   53,   53,   53,   53,
   53,   53,   53,   53,   53,   53,   53,   53,   53,   53,
   54,   13,   13,   55,   55,   55,   55,   55,   55,   55,
   55,   55,   55,   55,   58,   20,   20,   20,   20,   20,
   20,   20,   20,   20,   59,   27,   27,   60,   57,   57,
   25,   61,   61,   56,   56,   62,   63,   63,   38,   38,
   64,   64,   65,   65,   65,   65,   66,   66,   68,   68,
   68,   68,   70,   71,   71,   72,   72,   73,   73,   73,
   73,   73,   73,   74,   74,   74,   74,   74,   74,   74,
   74,   21,   21,   75,   75,   75,   75,   75,   76,   76,
   77,   78,   78,   79,   80,   80,   81,   81,   45,   82,
   83,   67,   67,   84,   84,   84,   84,   84,   84,   84,
   85,   85,   85,   85,   86,   86,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    6,    6,    9,
   10,   13,    9,   10,   10,   10,   10,    7,   11,    9,
   10,   11,    9,   10,    8,    5,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    3,    3,    3,    3,    6,    5,    1,    1,    3,    1,
    1,    1,    1,    1,    1,    1,    2,    3,    1,    2,
    3,    3,    3,    1,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    3,    1,    1,    1,    1,
    4,    2,    3,    5,    1,    3,    1,    1,    1,    1,
    1,    1,    1,    1,    3,    4,    3,    5,    1,    5,
    5,    4,    0,    4,    1,    3,    1,    1,    7,    8,
    1,    2,    4,    3,    5,    4,    1,    3,    2,    4,
    3,    2,    3,    3,    4,    4,    1,    1,    1,    1,
    2,    3,    2,    2,    1,    2,    4,    5,    6,    6,
    7,    1,    2,    1,    3,    2,    1,    3,    1,    2,
    2,    3,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    4,    1,    1,
    4,    4,    2,    1,    1,    2,    3,    2,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    6,    9,    8,    6,    6,
    3,    3,    3,    5,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    2,    2,    1,    2,    1,    3,
    2,    1,    2,    1,    3,    1,    1,    3,    1,    2,
    3,    1,    2,    3,    1,    2,    1,    2,    1,    2,
    3,    4,    1,    3,    2,    1,    3,    2,    3,    3,
    2,    4,    5,    1,    1,    1,    1,    6,    9,    6,
    6,    1,    3,    1,    1,    2,    2,    2,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    1,    1,
    5,    1,    3,    2,    7,    2,    2,    7,    1,    1,
    8,    9,    9,   10,    0,    1,    5,    6,   11,    5,
    7,    5,    5,    6,    4,    4,    5,    5,    6,    6,
    7,    5,    5,    6,    6,    7,    6,    7,    5,    6,
    7,    7,    8,    6,    4,    4,    6,    7,    6,    2,
    6,    4,    4,    4,    4,    6,    6,    7,    6,    6,
    6,    4,    3,    4,    7,    8,    8,    9,   10,    5,
    6,    5,    5,    6,    3,    4,    5,    6,    8,    4,
    5,    6,    6,    4,    5,    7,    8,    5,    6,   11,
    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,    0,   98,  109,  101,  102,  103,  104,  100,  137,
   41,   39,   42,   43,   44,   45,   46,   47,  309,  174,
  175,  176,    0,   40,    0,  167,  168,  172,  171,  173,
    0,  190,  191,    0,  169,  170,    0,    0,    0,   99,
    0,    0,    0,    0,    0,  138,  139,    0,    0,    0,
    3,    0,    0,    0,  165,    0,    4,    0,    0,  184,
  185,   37,   38,   48,   49,    0,    0,    0,    0,    0,
    0,    0,  189,    0,    0,    0,    0,    0,    0,    0,
    0,  183,   92,    0,    0,    0,    0,    0,    0,    0,
  143,    0,    0,    0,    0,  179,    0,    0,    0,   67,
    0,    0,    0,    0,    0,    0,    0,    0,  166,    5,
    6,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  188,    0,    8,    0,    7,    0,    0,    0,    0,    0,
    0,    0,   93,    0,    0,    0,    0,  142,    0,    0,
  118,  105,    0,    0,  115,    0,    0,   68,    0,  163,
  164,  156,    0,    0,  157,  194,    0,    0,    0,  192,
    0,    0,    0,    0,    0,    0,    0,    0,  238,  239,
  237,  240,  241,  242,  236,  221,  225,  244,  243,    0,
    0,    0,    0,    0,    0,    0,    0,  224,  222,  223,
    0,    0,    0,    0,  187,    0,    0,    0,    0,   50,
    0,    0,    0,   76,   75,   13,    0,    0,   69,   74,
  182,  178,  181,    0,    0,    0,    0,    0,    0,  106,
    0,    0,    0,   90,   89,   81,   79,   80,   82,   83,
   84,   85,    0,   77,  160,    0,  155,    0,    0,    0,
    0,    0,    0,    0,  129,  193,    0,    0,    0,    0,
  148,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  249,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   70,
   14,    0,  246,  110,   94,  111,  114,  108,  116,    0,
    0,   12,   78,  162,  158,    0,    0,  124,    0,    0,
    0,    0,    0,    0,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  259,  262,    0,    0,  267,    0,
  312,  320,    0,  131,  144,    0,  149,    0,    0,  150,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  233,
    0,    0,    0,  231,  232,    0,    0,    0,    0,    0,
   63,   66,    0,   61,    0,   52,   64,    0,   58,   60,
   65,   62,   53,   54,   51,   17,   16,   73,   72,   71,
   86,  295,    0,  294,    0,  292,  126,    0,    0,    0,
  317,    0,    0,  314,    0,    0,    0,    0,  316,  307,
  308,    0,    0,  305,  326,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  310,  360,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  195,  196,  197,  198,
  199,  200,  201,  202,  203,  204,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  119,  260,    0,  268,    0,
    0,    0,  130,  151,   35,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  250,    0,    0,    0,    0,    0,
    0,    0,  251,    0,  298,  296,  297,   91,    0,  125,
  261,    0,  313,  245,    0,    0,  273,    0,    0,    0,
    0,    0,    0,  306,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  205,  206,  217,  218,  219,
  220,  208,  210,  211,  212,  213,  209,  207,  215,  216,
  214,    0,    0,    0,  299,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  373,    0,    0,  120,
   20,    0,   30,    0,    0,    0,    0,    0,    0,    0,
  234,    0,    0,    0,    0,    0,   56,    0,   59,  293,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  355,    0,    0,  256,  257,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  374,    0,    0,    0,    0,    0,  230,  226,  229,
    0,   25,    0,    0,   55,    0,    0,    0,  275,    0,
    0,  276,    0,    0,    0,    0,  327,    0,    0,  398,
    0,    0,  383,    0,    0,  402,    0,  387,    0,  404,
  395,  391,    0,    0,  380,    0,  333,  332,  382,  405,
    0,    0,    0,    0,  330,    0,    0,    0,    0,  235,
  248,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  300,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  252,
    0,  254,    0,    0,    0,    0,  302,    0,    0,  278,
    0,  274,    0,    0,    0,    0,    0,  328,  357,  399,
  366,  384,  361,  388,  359,  392,  381,  334,  370,  393,
  258,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  369,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  253,
  228,    0,  315,    0,  318,  303,    0,  285,  286,  287,
    0,    0,    0,    0,  284,  280,  279,  277,    0,    0,
    0,    0,  331,    0,    0,    0,    0,  375,    0,  396,
    0,    0,    0,  358,  301,    0,  311,    0,    0,    0,
    0,    0,   22,  227,  255,  304,  282,    0,    0,    0,
    0,    0,  321,    0,    0,    0,  377,    0,    0,  376,
  397,    0,    0,  389,    0,  283,    0,    0,    0,    0,
  322,  323,    0,    0,  378,    0,    0,    0,    0,    0,
    0,  324,  379,    0,    0,    0,    0,    0,    0,  329,
  400,    0,  291,  288,  290,    0,    0,  289,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   60,   12,   13,   14,  253,  227,  219,   61,
   88,  228,  567,   89,  269,   69,   91,  220,  416,  208,
  435,  418,  419,  420,  421,  229,  850,  254,  105,  106,
  164,  113,   93,  165,   15,  124,  178,  374,  270,  265,
   75,   65,   76,   66,   67,   16,  271,  174,  175,   94,
  180,  517,  642,  209,  210,  851,  284,  821,  444,  726,
  852,  717,  718,  375,  376,  377,  378,  379,  380,  568,
  685,  781,  782,  926,  436,  644,  645,  856,  857,  453,
  454,  490,  649,  381,  382,  456,
  };
  protected static readonly short [] yySindex = {          635,
   23, -129,   31,   53,   67, 3134, -218, -166,    0,  635,
    0,    0,    0,    0,  -80, 3729,  -86,  170,  177, 1554,
  -51,  -13,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  164,    0,  197,    0,    0,    0,    0,    0,
  209,    0,    0,   14,    0,    0, 4920,  -91,   19,    0,
 -112, -102,  245, 5291, 3456,    0,    0,   24,   26,  225,
    0,  251, 3794,  -14,    0, 3794,    0,   32,   33,    0,
    0,    0,    0,    0,    0,  264,  -63, 5291,  178,  -98,
 -105,    1,    0, -102,  -16,  269,   -5,  201,   93, 5291,
 5291,    0,    0, -102,   -7,  245,   85, 5291,  105,   79,
    0,  350,  360, 4148,  245,    0, 5291,  245, 3794,    0,
  130,  307, 4274,  -81,   -4, 3794,  251,   12,    0,    0,
    0,  151, 5291,  -98,  -98, 4030, 5291,  -98, 5291,  -98,
    0,  119,    0,  316,    0, -190,  411,  331, 4960,  416,
  -36,  -32,    0, 5291, 5291,   54, 5291,    0,  214,   83,
    0,    0, -102,   71,    0,  245,  245,    0,  169,    0,
    0,    0, 5429,  112,    0,    0,  142,  -96, -203,    0,
  251,   17,  -81,  251,  435,  826, 5291, 5291,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -11,
  437,  440,  441, 5318, 5324, 5318,  438,    0,    0,    0,
 4030, 5291, 4030, 5291,    0,  425,  434,  436,  195,    0,
 -190, 5181,    0,    0,    0,    0,   20, 4030,    0,    0,
    0,    0,    0, -102,  -44,  443,  -72,  456,  246,    0,
 4280,  447,  469,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  848,    0,    0, 1886,    0, 5014, -184, 5305,
  -90,  253, 5318,  258,    0,    0,  -81,  251,  142,  142,
    0,  -81,    0,  144,  481, -102, 3997,  495, 5291, 5318,
 5318, 5318,    0,   75, 5204,   82,   45,  153,  496, 4030,
  497, 4030, 5074, 5107,  552,    0, -190,  206,   21,    0,
    0, 5197,    0,    0,    0,    0,    0,    0,    0,  279,
 5140,    0,    0,    0,    0,  282,  290,    0,  491,  494,
 5318, -183, 5318, 3498, 5318,    0, 1378,  147, 1378,  147,
 1378,  147, 5291, 3587,  147, 5291, 5291, 1378, 3936, 4074,
 5291, 5291, 5291, 5318, 5318, 5318, 5318, 5318, 5291,  111,
 4416,  244, -247,  778, 5318, 5318, 5318, 5318, 5318, 5318,
 5318, 5318, 5318, 5318, 5318, 5318,  611,  147, 5291,  147,
 3498,  146, 5291, 4127,    0,    0, 7998, -218,    0, -218,
    0,    0, 5305,    0,    0,  268,    0,  -81,  142,    0,
  325, -230,  210,  542, 5291,  -42,  200,  202,  208,    0,
 5318, 4030,   78,    0,    0,  342,  224,  561,  226,  576,
    0,    0,  581,    0,  301,    0,    0,  499,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -188,    0,  216,    0,    0,  268, 7998, 8493,
    0,  351, 4064,    0,  582, 1133, 3794, 3794,    0,    0,
    0, 4030, 1378,    0,    0, 5291, 4030, 1378, 5291, 4030,
 1378, 5291, 4030, 5291, 4030, 5291, 4030, 4030, 4030, 1378,
 5291, 4030, 5291, 4030, 4030, 4030, 4030,  583,  585,  586,
  588,  589,  -38, 5291, 4542,  -29, 5318,  593,    0,    0,
 5291, 5291, 5291,  -10,  242,  255,  256,  259,  262,  263,
  265,  273,  274,  276,  278,  280,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5291,  295,  -77, 5291,
  626, 5291, 3794, 3407, -236,    0,    0, -218,    0,   26,
   26, 4253,    0,    0,    0,  365,  383,  386, -200,   -2,
 5318, 5291, 5291, 5291,    0,  591, -218,  400,  296,  401,
  299, 4953,    0, 5107,    0,    0,    0,    0, 5140,    0,
    0, -218,    0,    0,  631,  414,    0,  642, 1133, 1133,
 3794,  639, 4030,    0, 4030,  641, 4030, 4030,  643, 4030,
 4030,  644, 4030,  645, 4030,  646,  648,  649, 4030, 4030,
  650, 4030,  651,  657,  659,  671, 5318, 5318, 5318,  552,
 5318, 2843,    3, 5291,    4, 5291,  676, 5291, 4030, 4030,
    5, 5318, 5291, 5291, 5291, 5291, 5291, 5291, 5291, 5291,
 5291, 5291, 5291, 5291, 4030,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 5291, 4064,  678,    0, 4030,  304,  642,  642, 1133,
 1133, 5291, 5291,  626, 5291, 3794,    0, 5318,   26,    0,
    0, -218,    0,  472,  476, 5318,  703,  -30,  -28,  -25,
    0,   26, -218,  512, -218,  513,    0,  222,    0,    0,
   26,  414,  660, 4668,  359,  642,  642, 1133, 4064,  706,
  708, 4064,  709,  710, 4064,  712,  723, 4064,  729, 4064,
  730, 4064, 4064, 4064,  731,  732, 4064,  733, 4064, 4064,
 4064, 4064,    0,  734,  735,    0,    0,  737,  740,  528,
  742, 5291,   16, 5291, 4030,  743, 5291,  744,  745,  750,
 5318,  751, -102, -102, -102, -102, -102, -102, -102, -102,
 -102, -102, -102, -102,  754, 4030,  755,  711,  761,  546,
  142,  142,  642,  642, 1133, 1133,  642,  642, 1133, 1133,
 3794,    0,   26,  763, -218,  764, 1684,    0,    0,    0,
   26,    0,   26, -218,    0,  772, 5291, 5231,    0, 3217,
  236,    0,  414,  432,  433,  642,    0, 4064, 4064,    0,
 4064, 4064,    0, 4064, 4064,    0, 4064,    0, 4064,    0,
    0,    0, 4064, 4064,    0, 4064,    0,    0,    0,    0,
 5318, 5318,  552,  552,    0,  445,  782, 5291,  785,    0,
    0,  450,  789,  454, 5291, 5291,  791, 1684, 4064,  792,
 4064,    0, 5318,  793,  142,  142,  142,  142,  642,  642,
  142,  142,  642,  642, 1133,  465,   26, 1684, 5318,    0,
  272,    0,   26,  414,  802, 5283,    0,  807, 2149,    0,
 3365,    0, 5254,  520,  414,  414,  460,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  737,  594,  473,  -43,  474,  595,  483,  604, 4030,
 4030, 1684,  820,    0, 4064,  779,  827,  613,  142,  142,
  142,  142,  142,  142,  142,  142,  642,  618,  285,    0,
    0, 1684,    0,  414,    0,    0, 4997,    0,    0,    0,
  498,  838,  850,  852,    0,    0,    0,    0,  414,  565,
  566,  414,    0,  638,  853,  516,  658,    0,  661,    0,
  567,  569,  820,    0,    0, 5318,    0,  142,  142,  142,
  142,  142,    0,    0,    0,    0,    0,  305,  866, 5318,
 5318, 5318,    0,  414,  414,  598,    0,  534,  664,    0,
    0,  872,  879,    0,  142,    0, 5291,  538,  539,  540,
    0,    0,  414,  673,    0,  553,  554,   18, 5291, 5291,
 5291,    0,    0,  675,  682, 5318,  -24,  -22,  -19,    0,
    0,  891,    0,    0,    0, 1684,  339,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0, 3865,    0,    0,  945,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  632,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  878,    0,    0,    0,    0,    0,
 1431,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3573,  -17,  679,    0,    0,    0,    0,    0, 3930,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  632,    0,  632,    0,
  632,    0,    0, 1523,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  257,    0,    0,    0,    0,    0, 3662,
    0,    0,    0,    0,  680,    0,    0,  681,    0,    0,
    0,    0,    0,  632,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  176,    0,    0,    0,    0,
    0,  668,    0,    0,    0,    0,    0,    0,    0,    0,
  176,  176,    0,    0,    0,    0,    0,    0,    0, 1245,
    0,    0,  490,    0,    0,  683,  684,    0,    0,    0,
    0,    0,  510,    0,    0,    0,  -85,    0,  -57,    0,
    0,    0,  254,    0,    0,  159,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  176,    0,  176,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 1532,    0,    0,    0,    0,  176,    0,    0,
    0,    0,    0,  298,  176,    0,  176,    0,    0,    0,
    0, 1057, 1168,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  380,    0,    0,  -55,    0,
    0,    0,    0,    0,    0,    0,  632,    0,  303,  366,
    0,  632, 3282,    0,  493,  143,  176,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  176,
    0,  176,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5351,    0, 5351,
    0, 5351,    0,    0, 5351,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1890,    0, 5351,
    0,    0,    0,    0,    0,    0,    0, 4379,    0, 8102,
    0,    0,    0,    0,    0,  -54,    0,  632,  587,    0,
    0,    0,    0,    0,    0,  176,    0,    0,    0,    0,
    0,  257,    0,    0,    0,    0,    0,    0,    0,  757,
    0,    0,   49,    0,  176,    0,    0,  396,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  176,    0,    0,    0,    0,  -47,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  176,    0,    0,    0,    0,  176,    0,    0,  176,
    0,    0,  176,    0,  176,    0,  176,  176,  176,    0,
    0,  176,    0,  176,  176,  176,  176,    0,    0,    0,
    0,    0,  176,    0,    0,  176,    0,    0,    0,    0,
    0,    0,    0,  176,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  176,    0,
    0,    0,    0,    0,  176,    0,    0, 4505,    0, 4631,
 8206,    0,    0,    0,    0,    0,    0,    0,    0,  176,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 8310,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  176,    0,  176,    0,  176,  176,    0,  176,
  176,    0,  176,    0,  176,    0,    0,    0,  176,  176,
    0,  176,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  176,    0,  176,    0,    0,    0,  176,  176,
  176,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  176,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5432,    0,  176,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4757,    0,
    0,  979,    0,    0,    0,    0,    0,  176,  176,  176,
    0, 1009,    0,    0,    0,    0,    0,    0,    0,    0,
 8414,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5540,    0,    0,
    0,    0,  176,    0,  176,    0,    0,    0,    0,    0,
    0,    0, 1639, 1746, 1889, 1996, 2103, 2246, 2353, 2460,
 2603, 2710, 2817, 2960,    0,  176,    0,    0,    0,    0,
 5647,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 1055, 1146, 1433,    0,    0,    0,    0,    0,
 1492,    0, 1784, 3122,    0,    0,    0,    0,    0,  176,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5754, 5861, 5968, 6075,    0,    0,
 6182,    0,    0,    0,    0,    0, 3373,    0,    0,    0,
    0,    0, 3575,    0,    0,    0,    0,  402,  176,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 6289,    0,    0,    0,    0,    0,    0,    0,  176,
  176,    0, 6396,    0,    0,    0,    0,    0, 6503, 6610,
 6717,    0, 6824, 6931, 7038, 7145,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 7252,    0,    0,    0,    0, 7359, 7466, 7573,
 7680, 7787,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 7894,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  176,    0,    0,
    0,    0,    0,    0,    0,    0,  176,  176,  176,    0,
    0,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  937,  855,    0,    0,    0,    0,  746,  738,  934,
  372,   -6,  654,   22,   81, -171,    0,  669,  688, -240,
 -548,    0,  413,    0, -680,    0,  379,  717,  864,  148,
    0,   43,    0,  747,    0, -103,    0,  592,  -78, -224,
   -3,    0,  -34,  913,  -49,    0, -227,    0,  728,    0,
 -107,    0,    0,    0,    0, -723,  -70,    0, -558, -567,
   77,  174,  180, -344,  551,    0,  619,  620,  555,  311,
 2816,    0,  135,    0,  444,    0,  252,    0,  150,   88,
  -48,    0,  348,    0,  564,   68,
  };
  protected static readonly short [] yyTable = {            62,
  936,  541,   64,  678,  232,  602,  261,  683,  233,   62,
  768,  111,  769,  643,  606,  770, 1003,  304, 1004,   98,
  306, 1005,   97,  183,  113,  114,  260,  148,  279,  527,
  119,  108,  383,  612,  318,  114,  154,  121,  126,  387,
  728,  666,   97,  144,  390,  179,  722,  724,  731,  536,
  104,  114,  417,  417,  424,   68,  114,   62,   62,  818,
  158,  996,   92,  302,  302,  127,   62,  122,  128,   62,
  434,  266,  129,   97,  555,  123,  262,  267,  657,  664,
  272,  136,  442,   17,  556,  216,  117,  557,  401,   62,
  217,   20,   62,  151,  152,  316,  855,  154,  259,  491,
   90,  104,  443,  492,  893,   97,   57,  163,  134,  263,
   62,  240,   62,   21,  241,  129,  173,  153,  401,   62,
  176,  401,  129,  776,  909,  401,  186,   22,  263,   92,
  211,   92,  213,   92,  286,  287,  142,  405,  256,  176,
   18,   19,   19,  404,  301,  427,  112,  234,  235,  537,
  237,  266,  257,   63,  817,  258,  819,   70,   36,  823,
  534,  533,  266,   74,  388,  107,   92,  135,  943,  138,
   58,  140,   62,   77,  553,  855,  264,   68,  236,  665,
  276,  277,   97,  121,  113,   80,   81,  527,  121,  218,
  389,   72,  658,   39,  112,  317,   82,   83,   97,  400,
  113,   59,  546,   99,  177,  290,  530,  292,  531,   80,
   81,  115,  118,  560,  403,   97,  127,  113,  122,  128,
  125,   52,   53,  128,  864,   92,  123,   80,   81,   95,
   78,   82,   83,   57,  163,  296,  100,   79,  297,   97,
   97,   97,  176,   97,   97,   97,  426,   97,  101,  297,
  885,  173,  129,  147,   97,   97,  558,  127,   85,  559,
   96,   97,  775,   97,  166,  559,  167,  181,  147,   97,
  102,  143,  396,  182,  448,  109,  862,   86,  402,  863,
  112,  266, 1007,  184,  114,  122,  415,  415,  268,  447,
  123,  130,  131,  300,  300,  913,   97,  120,  113,  121,
   95,   86,  152,  132,  433,  112,  930,  931,  146,   92,
  112,  434,  911,  417,   92,  912,  141,   62,  434,   86,
  452,  523,  457,  149,  460,  954,  463,  465,  912,  467,
  468,  469,  472,  474,  475,  476,  477,   97,  112,  113,
  112,   96,  483,  486,  112,  976,  112,  494,  559,  150,
  112,  155,  112,  112,  112,  956,  659,  112,  112,  716,
  112,   97,  519,  112,   62,  154,  525,  522,  958,  278,
  963,  157,  112,  966,   23,  672,   97,   97,   97, 1008,
  112,   95,  912,   24,   39,  112,  112,  112,  540,  159,
  681,  935,   25,   26,   27,   28,   29,  459,  112,  462,
  112,  160,  466,  168,  574,  981,  982,  185,   97,  574,
   19,   19,  574,  571,   19,   19,  458,   19,  461,  484,
  161,  574,   96,  161,  992,  470,   36,   36,  242,  169,
   92,   19,   19,   36,  215,  518,   57,  520,  108,   57,
   62,   62,  281,  129,  243,  281,  573,   36,   36,  575,
  221,  577,  578,  222,  580,  581,  231,  583,  133,  585,
  137,  139,  239,  589,  590,  176,  592,   19,   80,   81,
  238,  446,   82,   83,  656,  273,  280,  603,  605,  281,
  282,  288,  293,   36,  609,  610,  611,  244,  245,  246,
  763,  294,   18,  295,  247,  248,  307,  249,  250,  251,
  252,  771,  308,  773,  305,  187,  188,  310,  311,  212,
  625,  214,  384,  646,  386,   62,   62,   62,  521,  652,
  655,  147,  147,  391,  392,  147,  147,  230,  147,   97,
  117,  113,  406,  117,  395,  668,  669,  670,  431,  407,
  409,  437,  147,  147,  485,  433,  438,  415,  439,   97,
  159,  113,  433,  159,  440,  626,  627,  189,  190,  191,
   86,  192,  193,  194,   62,  195,  455,  129,  524,  113,
  152,  152,  716,  716,  152,  152,  489,  152,  147,  198,
  263,  535,  283,  283,  283,  539,  153,  199,  542,  538,
  543,  152,  152,  847,  569,  570,  544,  723,  547,  725,
  230,  725,  853,  548,  549,  550,  733,  734,  735,  736,
  737,  738,  739,  740,  741,  742,  743,  744,  925,  551,
  552,  761,  554,  564,  661,  566,  597,  152,  598,  599,
  613,  600,  601,  154,  154,  746,  608,  154,  154,  662,
  154,  385,  663,  614,  615,   62,   62,  616,   62,   62,
  617,  618,  671,  619,  154,  154,  673,  675,  397,  398,
  399,  620,  621,  283,  622,  114,  623,  186,  624,  650,
  651,  654,  835,  836,  682,  674,  434,  780,  676,  442,
  430,  684,  689,  112,  692,  205,  695,  698,  700,  702,
  154,  703,  704,  707,  709,  628,  629,  630,  631,  441,
  710,  445,  711,  449,  632,  633,  634,  635,  636,  637,
  638,  639,  640,  641,  712,  725,  206,  725,  688,  727,
  725,  748,  478,  479,  480,  481,  482,  266,  266,  488,
  750,  764,  765,  495,  496,  497,  498,  499,  500,  501,
  502,  503,  504,  505,  506,  861,  767,  783,  204,  788,
  777,  789,  791,  792,   62,  794,   28,  129,  899,  900,
   18,   18,  903,  904,   18,   18,  795,   18,  772,  774,
  415,  859,  797,  799,  803,  804,  806,  811,  812,  545,
  813,   18,   18,  814,  815,  816,  822,  824,  825,  207,
  186,  266,  266,  826,  828,  266,  266,  829,  831,  755,
  756,  643,  759,  760,  833,  834,  846,  848,  189,  190,
  191,  725,  192,  193,  194,  854,  195,   18,  890,  891,
  865,  866,  948,  949,  883,  884,  950,  951,  886,  887,
  198,  648,  888,  889,  892,  895,  898,   58,  199,  275,
  266,  266,  266,  266,  908,  914,  917,  929,  932,  415,
  933,  938,  934,  937,  153,  153,  780,  129,  153,  153,
  940,  153,  939,  912,  289,  607,  291,  266,   59,  274,
  946,  945,  947,  975,  953,  153,  153,  960,  959,  686,
  687,  303,  189,  190,  191,  205,  192,  193,  194,  961,
  195,  962,  964,  965,  967,  969,  968,  196,  197,  972,
   57,  973,    1,    2,  198,  977,    3,    4,  845,    5,
  433,  153,  199,  984,  970,  986,  206,  971,  177,  667,
  985,  177,  987,    6,    7,  983,  989,  990,  991,  993,
  394, 1000,  994,  995, 1006,  186,  186,  177, 1001,  186,
  186,  186,  186,  408,    1,  410,   71,  113,  204,  145,
  132,  133,  134,   87,  135,  136,  186,  186,  298,    8,
  753,  754,  186,  186,  757,  425,  679,  299,  177,  313,
  988,  156,  312,  110,  532,  713,  714,  715,   33,  719,
  721,  423,  997,  998,  999,  315,  881,  309,  955,  561,
  732,  186,  186,  882,  562,  528,  529,  928,  786,  832,
  177,  758,  680,  563,  200,  916,    0,    0,   23,  507,
  508,  509,  510,  511,  512,  513,  514,  515,  516,  201,
  202,  203,    0,    0,   28,   28,    0,    0,   28,   28,
    0,   28,    0,    0,    0,    0,  762,    0,    0,    0,
    0,   23,    0,    0,  766,   28,   28,    0,    0,    0,
   24,  647,    0,    0,   34,  303,    0,    0,    0,   25,
   26,   27,   28,   29,    0,  839,  840,    0,    0,  843,
  844,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   28,  189,  190,  191,    0,  192,  193,  194,    0,
  195,    0,    0,    0,    0,    0,  565,  196,  197,    0,
    0,    0,    0,    0,  198,  572,    0,  242,    0,  827,
  576,    0,  199,  579,    0,    0,  582,    0,  584,    0,
  586,  587,  588,  243,    0,  591,    0,  593,  594,  595,
  596,    0,    0,    0,  177,  177,  177,    0,  177,  177,
  177,  177,  177,    0,    0,   21,    0,    0,    0,  177,
  177,    0,    0,    0,    0,  907,  177,    0,  493,  177,
  177,  177,  177,  177,  177,    0,  244,  245,  246,    0,
  177,    0,  114,  247,  248,    0,  249,  250,  251,  252,
    0,   87,    0,    0,  177,  177,    0,  177,  177,  879,
  880,  177,  205,    0,  177,  177,  177,  177,  177,    0,
  177,    0,    0,    0,  200,    0,    0,    0,  112,    0,
    0,  897,    0,    0,    0,    0,    0,    0,    0,  201,
  202,  203,    0,  206,    0,    0,  690,  910,  691,    0,
  693,  694,    0,  696,  697,    0,  699,    0,  701,    0,
    0,    0,  705,  706,  107,  708,   33,   33,    0,    0,
   33,   33,    0,   33,    0,  204,  177,  177,    0,    0,
    0,    0,  729,  730,    0,    0,    0,   33,   33,    0,
    0,  177,  177,  177,    0,    0,   23,   23,  745,    0,
   23,   23,    0,   23,  107,  107,  107,    0,  107,    0,
    0,    0,   88,    0,    0,    0,  747,   23,   23,  749,
    0,    0,    0,   33,  107,    0,  107,    0,    0,  177,
  177,    0,    0,    0,    0,    0,   87,    0,    0,    0,
    0,    0,   34,   34,  974,    0,   34,   34,    0,   34,
    0,    0,   87,   23,    0,  107,    0,  107,  978,  979,
  980,    0,  787,   34,   34,  790,    0,    0,  793,    0,
    0,  796,    0,  798,    0,  800,  801,  802,    0,    0,
  805,    0,  807,  808,  809,  810,    0,  107,    0,  107,
    0,    0,    0,    0, 1002,   87,   87,   87,  820,   34,
    0,    0,   87,   87,    0,   87,   87,   87,   87,  189,
  190,  191,    0,  192,  193,  194,    0,  195,    0,  830,
    0,    0,    0,    0,  196,  197,    0,    0,    0,    0,
    0,  198,    0,   21,   21,    0,    0,   21,   21,  199,
   21,    0,    0,    0,    0,    0,    0,   88,    0,    0,
    0,    0,   31,  860,   21,   21,    0,   58,    0,    0,
    0,  868,  869,   88,  870,  871,    0,  872,  873,    0,
  874,    0,  875,    0,    0,    0,  876,  877,    0,  878,
    0,    0,    0,    0,    0,    0,    0,    0,   59,    0,
   21,  180,    0,    0,  180,    0,    0,    0,    0,    0,
    0,    0,  894,    0,  896,    0,   88,   88,   88,    0,
  180,   26,    0,   88,   88,    0,   88,   88,   88,   88,
   57,  107,  107,  107,    0,  107,  107,  107,    0,  107,
    0,  200,  107,  107,  927,    0,  107,  107,  107,  107,
  107,  180,  113,  107,    0,    0,  201,  202,  203,    0,
    0,  107,    0,  107,  107,    0,    0,  107,    0,    0,
    0,    0,    0,  941,  942,    0,    0,    0,  944,    0,
    0,  107,  107,  180,  107,  107,    0,    0,  107,  107,
    0,  107,  107,  107,  107,  107,    0,  107,    0,  107,
    0,   98,  247,    0,    0,  247,    0,    0,    0,    0,
  107,  107,  107,    0,  107,  107,    0,    0,    0,  107,
    0,  107,    0,  247,  107,  107,  107,  107,  107,  107,
  107,  107,  107,  107,    0,  107,  107,    0,  107,  107,
  107,  107,  107,  107,  107,  107,  107,  107,  107,  107,
  107,    0,  107,  107,  247,    0,    0,  107,  107,  107,
  107,  107,  107,    0,  107,  107,  107,  107,  107,  107,
  107,   23,  107,    0,    0,  113,    0,    0,    0,    0,
   24,    0,    0,  107,  247,    0,  247,    0,    0,   25,
   26,   27,   28,   29,    0,  107,  107,  107,  107,    0,
  107,    0,  107,  107,    0,    0,  107,  107,   97,    0,
  113,    0,    0,    0,    0,    0,    0,  180,  180,  180,
    0,  180,  180,  180,  180,  180,    0,    0,    0,    0,
   31,   31,  180,  180,   31,   31,    0,   31,    0,  180,
    0,    0,  180,  180,  180,  180,  180,  180,    0,    0,
    0,   31,   31,  180,    0,  450,  451,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  180,  180,    0,
  180,  180,    0,   58,  180,    0,    0,  180,  180,  180,
  180,  180,    0,  180,    0,    0,    0,   31,    0,   26,
   26,    0,    0,   26,   26,    0,   26,    0,    0,    0,
    0,    0,    0,    0,   59,    0,    0,    0,    0,    0,
   26,   26,    0,   24,    0,   97,    0,  113,  247,    0,
  113,  113,    0,    0,  113,  113,  113,  113,    0,    0,
    0,    0,    0,    0,  247,  247,   57,    0,    0,  180,
  180,  113,  113,    0,    0,    0,   26,  113,  113,    0,
    0,    0,    0,    0,  180,  180,  180,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  113,    0,
    0,    0,    0,    0,   80,   81,  113,  113,   82,   83,
   84,   31,   32,   33,   34,   35,   36,   37,   38,    0,
    0,    0,  180,  180,    0,   44,    0,  247,  247,  247,
    0,  247,  247,    0,    0,   85,  247,    0,  247,    0,
    0,  247,  247,  247,  247,  247,  247,  247,  247,  247,
  247,    0,  247,  247,    0,  247,  247,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,  247,    0,  247,
  247,  401,  401,    0,    0,  247,  247,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,  247,   97,  247,
  113,    0,    0,    0,    0,    0,   86,    0,    0,    0,
  247,    0,    0,    0,    0,    0,    0,  223,    0,    0,
    0,    0,  247,  247,  247,  247,   24,    0,    0,  247,
    0,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,  401,  401,  401,    0,  401,  401,
    0,    0,    0,  401,    0,  401,    0,    0,  401,  401,
  401,  401,  401,  401,  401,  401,  401,  401,    0,  401,
  401,    0,  401,  401,  401,  401,  401,  401,  401,  401,
  401,  401,  401,  401,  401,    0,  401,  401,  406,  406,
    0,    0,  401,  401,  401,  401,  401,    0,  401,  401,
  401,  401,  401,  401,  401,   97,  401,  113,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  401,    0,    0,
    0,   24,   24,    0,    0,   24,   24,    0,   24,  401,
  401,  401,  401,    0,    0,  849,  401,    0,    0,    0,
    0,    0,   24,   24,    0,    0,    0,    0,    0,    0,
    0,  406,  406,  406,    0,  406,  406,    0,    0,    0,
  406,    0,  406,    0,    0,  406,  406,  406,  406,  406,
  406,  406,  406,  406,  406,    0,  406,  406,   24,  406,
  406,  406,  406,  406,  406,  406,  406,  406,  406,  406,
  406,  406,    0,  406,  406,    0,    0,    0,    0,  406,
  406,  406,  406,  406,    0,  406,  406,  406,  406,  406,
  406,  406,   97,  406,  113,    0,    0,    0,    0,    0,
  325,  325,    0,    0,  406,    0,    0,    0,  314,    0,
    0,  390,  390,    0,    0,    0,  406,  406,  406,  406,
    0,    0,    0,  406,    0,    0,    0,    0,  116,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   40,   41,    0,   42,   43,    0,    0,   45,
    0,    0,   46,   47,   48,   49,   50,    0,   51,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  390,  390,  390,    0,  390,  390,
    0,    0,    0,  390,    0,  390,    0,    0,  390,  390,
  390,  390,  390,  390,  390,  390,  390,  390,    0,  390,
  390,    0,  390,  390,  390,  390,  390,  390,  390,  390,
  390,  390,  390,  390,  390,   54,  390,  390,  365,  365,
    0,    0,  390,  390,  390,  390,  390,    0,  390,  390,
  390,  390,  390,  390,  390,   97,  390,  113,    0,    0,
  325,  325,  325,  325,    0,    0,    0,  390,    0,  325,
  325,  325,  325,  325,  325,  325,  325,  325,  325,  390,
  390,  390,  390,    0,    0,    0,  390,   55,   56,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  365,  365,  365,    0,  365,  365,    0,    0,    0,
  365,    0,  365,    0,    0,  365,  365,  365,  365,  365,
  365,  365,  365,  365,  365,    0,  365,  365,    0,  365,
  365,  365,  365,  365,  365,  365,  365,  365,  365,  365,
  365,  365,    0,  365,  365,  362,  362,    0,    0,  365,
  365,  365,  365,  365,    0,  365,  365,  365,  365,  365,
  365,  365,   97,  365,  113,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  365,  189,  190,  191,    0,  192,
  193,  194,    0,  195,    0,    0,  365,  365,  365,  365,
  918,  919,    0,  365,  920,    0,    0,  198,    0,    0,
    0,    0,    0,    0,    0,  199,    0,    0,  362,  362,
  362,    0,  362,  362,    0,    0,    0,  362,    0,  362,
    0,    0,  362,  362,  362,  362,  362,  362,  362,  362,
  362,  362,    0,  362,  362,    0,  362,  362,  362,  362,
  362,  362,  362,  362,  362,  362,  362,  362,  362,    0,
  362,  362,    0,    0,    0,    0,  362,  362,  362,  362,
  362,    0,  362,  362,  362,  362,  362,  362,  362,   97,
  362,  113,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  362,    0,    0,    0,    0,    0,    0,  363,  363,
    0,    0,    0,  362,  362,  362,  362,  921,    0,    0,
  362,  112,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  922,  923,  924,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  363,  363,  363,    0,  363,  363,    0,    0,    0,
  363,    0,  363,    0,    0,  363,  363,  363,  363,  363,
  363,  363,  363,  363,  363,    0,  363,  363,    0,  363,
  363,  363,  363,  363,  363,  363,  363,  363,  363,  363,
  363,  363,    0,  363,  363,  364,  364,    0,    0,  363,
  363,  363,  363,  363,    0,  363,  363,  363,  363,  363,
  363,  363,   97,  363,  113,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  363,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  363,  363,  363,  363,
    0,    0,    0,  363,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  364,  364,
  364,    0,  364,  364,    0,    0,    0,  364,    0,  364,
    0,    0,  364,  364,  364,  364,  364,  364,  364,  364,
  364,  364,    0,  364,  364,    0,  364,  364,  364,  364,
  364,  364,  364,  364,  364,  364,  364,  364,  364,    0,
  364,  364,  403,  403,    0,    0,  364,  364,  364,  364,
  364,    0,  364,  364,  364,  364,  364,  364,  364,   97,
  364,  113,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  364,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  364,  364,  364,  364,    0,    0,    0,
  364,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  403,  403,  403,    0,  403,
  403,    0,    0,    0,  403,    0,  403,    0,    0,  403,
  403,  403,  403,  403,  403,  403,  403,  403,  403,    0,
  403,  403,    0,  403,  403,  403,  403,  403,  403,  403,
  403,  403,  403,  403,  403,  403,    0,  403,  403,    0,
    0,    0,    0,  403,  403,  403,  403,  403,    0,  403,
  403,  403,  403,  403,  403,  403,   97,  403,  113,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  403,    0,
    0,    0,    0,    0,    0,  394,  394,    0,    0,    0,
  403,  403,  403,  403,    0,    0,    0,  403,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   58,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   59,    0,    0,    0,    0,  394,  394,
  394,    0,  394,  394,    0,    0,    0,  394,    0,  394,
    0,    0,  394,  394,  394,  394,  394,  394,  394,  394,
  394,  394,    0,  394,  394,   57,  394,  394,  394,  394,
  394,  394,  394,  394,  394,  394,  394,  394,  394,    0,
  394,  394,  386,  386,    0,    0,  394,  394,  394,  394,
  394,    0,  394,  394,  394,  394,  394,  394,  394,   97,
  394,  113,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  394,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  394,  394,  394,  394,    0,    0,    0,
  394,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  386,  386,  386,    0,  386,
  386,    0,    0,    0,  386,    0,  386,    0,    0,  386,
  386,  386,  386,  386,  386,  386,  386,  386,  386,    0,
  386,  386,    0,  386,  386,  386,  386,  386,  386,  386,
  386,  386,  386,  386,  386,  386,    0,  386,  386,  372,
  372,    0,    0,  386,  386,  386,  386,  386,    0,  386,
  386,  386,  386,  386,  386,  386,  223,  386,    0,    0,
    0,    0,    0,    0,    0,   24,    0,    0,  386,    0,
    0,   27,    0,    0,   25,   26,   27,   28,   29,    0,
  386,  386,  386,  386,    0,    0,    0,  386,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  372,  372,  372,    0,  372,  372,    0,    0,
    0,  372,    0,  372,    0,    0,  372,  372,  372,  372,
  372,  372,  372,  372,  372,  372,    0,  372,  372,    0,
  372,  372,  372,  372,  372,  372,  372,  372,  372,  372,
  372,  372,  372,   58,  372,  372,    0,    0,    0,    0,
  372,  372,  372,  372,  372,    0,  372,  372,  372,  372,
  372,  372,  372,    0,  372,    0,    0,    0,    0,    0,
    0,    0,  720,    0,   59,  372,    0,    0,    0,    0,
    0,    0,  335,  335,    0,    0,    0,  372,  372,  372,
  372,    0,    0,    0,  372,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   57,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  205,    0,    0,    0,
    0,  112,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  335,  335,  335,    0,  335,
  335,    0,    0,    0,  335,    0,  335,  206,    0,  335,
  335,  335,  335,  335,  335,  335,  335,  335,  335,    0,
  335,  335,    0,  335,  335,  335,  335,  335,  335,  335,
  335,  335,  335,  335,  335,  335,    0,  335,  335,  204,
    0,    0,    0,  335,  335,  335,  335,  335,    0,  335,
  335,  335,  335,  335,  335,  335,    0,  335,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  335,    0,
    0,    0,   32,    0,    0,    0,    0,    0,    0,    0,
  335,  335,  335,  335,    0,    0,    0,  335,    0,   27,
   27,    0,    0,   27,   27,    0,   27,   23,    0,    0,
    0,    0,    0,    0,  112,    0,   24,    0,    0,    0,
   27,   27,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,  205,    0,   30,    0,    0,    0,
    0,   31,   32,   33,   34,   35,   36,   37,   38,   39,
   40,   41,    0,   42,   43,   44,   27,   45,    0,    0,
   46,   47,   48,   49,   50,  206,   51,    0,    0,    0,
    0,    0,    0,  751,  752,    0,   58,   52,   53,    0,
    0,    0,    0,  189,  190,  191,    0,  192,  193,  194,
    0,  195,    0,    0,    0,    0,    0,  204,  196,  197,
    0,    0,    0,    0,    0,  198,    0,   59,    0,    0,
    0,  784,  785,  199,    0,    0,    0,    0,    0,  116,
    0,    0,    0,   54,    0,   58,    0,    0,    0,    0,
    0,    0,    0,   40,   41,    0,   42,   43,    0,   57,
   45,    0,    0,   46,   47,   48,   49,   50,    0,   51,
    0,    0,    0,    0,    0,    0,   59,    0,    0,  112,
  112,    0,    0,  112,  112,  112,  112,   58,    0,    0,
    0,    0,    0,    0,    0,   55,   56,    0,  837,  838,
  112,  112,  841,  842,   29,    0,  112,  112,   57,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   59,    0,
    0,    0,    0,    0,    0,  200,   54,  114,    0,  112,
    0,  867,    0,    0,    0,  112,  112,    0,    0,    0,
  201,  202,  203,    0,    0,    0,    0,    0,    0,    0,
   57,  189,  190,  191,    0,  192,  193,  194,    0,  195,
    0,    0,  140,    0,    0,    0,  196,  197,    0,    0,
   32,   32,    0,  198,   32,   32,   58,   32,   55,   56,
    0,  199,    0,    0,  901,  902,    0,  116,  905,  906,
    0,   32,   32,  140,    0,    0,    0,    0,    0,    0,
   23,   40,   41,    0,   42,   43,    0,   59,   45,   24,
    0,   46,   47,   48,   49,   50,    0,   51,   25,   26,
   27,   28,   29,    0,    0,  140,    0,   32,    0,  116,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   57,
    0,    0,   39,   40,   41,    0,   42,   43,    0,   23,
   45,  141,  952,   46,   47,   48,   49,   50,   24,   51,
    0,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,    0,  200,   54,    0,    0,    0,  116,    0,
    0,    0,  141,    0,    0,    0,    0,    0,  201,  202,
  203,   23,   40,   41,    0,   42,   43,    0,    0,   45,
   24,    0,   46,   47,   48,   49,   50,   85,   51,   25,
   26,   27,   28,   29,  141,    0,   54,    0,   58,    0,
  116,    0,    0,    0,    0,    0,   55,   56,    0,    0,
    0,    0,    0,   39,   40,   41,    0,   42,   43,    0,
    0,   45,    0,    0,   46,   47,   48,   49,   50,   59,
   51,    0,    0,    0,    0,    0,  653,    0,    0,    0,
    0,    0,    0,    0,    0,   54,  140,    0,   55,   56,
    0,    0,   29,   29,    0,  140,   29,   29,    0,   29,
   23,   57,    0,   58,  140,  140,  140,  140,  140,   24,
    0,    0,    0,   29,   29,  140,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,   54,    0,  140,
  140,    0,  140,  140,   59,    0,  140,   55,   56,  140,
  140,  140,  140,  140,  140,  140,    0,    0,    0,   29,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   57,    0,    0,    0,
    0,    0,    0,    0,  145,  141,    0,    0,    0,   55,
   56,    0,    0,    0,  141,    0,    0,    0,    0,    0,
    0,    0,    0,  141,  141,  141,  141,  141,    0,  464,
    0,    0,  140,    0,  141,  145,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  141,  141,
    0,  141,  141,    0,    0,  141,    0,    0,  141,  141,
  141,  141,  141,  141,  141,    0,    0,  145,    0,  146,
    0,    0,   23,    0,    0,   58,    0,    0,    0,    0,
    0,   24,    0,    0,  140,  140,    0,    0,    0,    0,
   25,   26,   27,   28,   29,    0,    0,    0,    0,    0,
  146,   73,    0,    0,    0,    0,   59,    0,    0,    0,
    0,    0,    0,    0,    0,   40,   41,    0,   42,   43,
  393,  141,   45,    0,    0,   46,   47,   48,   49,   50,
    0,   51,  146,    0,    0,    0,  205,   23,   57,    0,
    0,    0,    0,    0,    0,    0,   24,    0,    0,    0,
    0,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,  116,  206,    0,  205,
    0,    0,    0,  141,  141,    0,    0,    0,    0,    0,
   40,   41,    0,   42,   43,    0,    0,   45,   54,    0,
   46,   47,   48,   49,   50,    0,   51,    0,    0,  204,
  206,    0,    0,  205,    0,    0,    0,    0,  145,    0,
    0,    0,    0,   58,    0,    0,    0,  145,    0,    0,
    0,    0,    0,    0,    0,    0,  145,  145,  145,  145,
  145,    0,  204,    0,  206,    0,    0,  145,    0,    0,
   55,   56,    0,    0,   59,    0,    0,    0,    0,    0,
    0,  145,  145,   54,  145,  145,    0,    0,  145,    0,
    0,  145,  145,  145,  145,  145,  204,  145,  162,    0,
    0,    0,    0,  146,    0,    0,   57,    0,    0,   23,
    0,    0,  146,    0,    0,    0,    0,   58,   24,    0,
    0,  146,  146,  146,  146,  146,    0,   25,   26,   27,
   28,   29,  146,    0,    0,   55,   56,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  146,  146,   59,  146,
  146,    0,    0,  146,  145,    0,  146,  146,  146,  146,
  146,  526,  146,  189,  190,  191,    0,  192,  193,  194,
    0,  195,    0,    0,    0,    0,    0,    0,  196,  197,
   57,    0,    0,    0,    0,  198,    0,    0,    0,    0,
    0,    0,    0,  199,    0,    0,  189,  190,  191,    0,
  192,  193,  194,    0,  195,    0,  145,  145,  471,    0,
    0,  196,  197,    0,    0,    0,    0,    0,  198,  146,
    0,    0,    0,    0,  172,    0,  199,    0,    0,    0,
  189,  190,  191,    0,  192,  193,  194,    0,  195,    0,
    0,    0,    0,   58,    0,  196,  197,   23,    0,   58,
    0,    0,  198,    0,    0,    0,   24,    0,    0,    0,
  199,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,  146,  146,    0,   59,    0,    0,    0,    0,    0,
   59,    0,    0,    0,    0,  200,    0,  660,    0,  112,
    0,    0,    0,  319,    0,    0,    0,    0,    0,    0,
  201,  202,  203,    0,    0,    0,   57,    0,    0,  320,
    0,    0,   57,    0,    0,    0,    0,    0,  200,    0,
    0,   23,  112,    0,    0,    0,    0,    0,    0,    0,
   24,    0,    0,  201,  202,  203,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,  473,    0,    0,    0,
    0,  161,  200,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  201,  202,  203,
    0,    0,  321,  322,  323,    0,  324,  325,    0,    0,
    0,  326,    0,  327,    0,   58,  328,  329,  330,  331,
  332,  333,  334,  335,  336,  337,    0,  338,  339,    0,
  340,  341,  342,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  265,  353,  354,   59,    0,    0,  319,
  355,  356,  357,  358,  359,    0,  360,  361,  362,  363,
  364,  365,  366,    0,  367,  320,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  368,    0,   23,   57,    0,
    0,    0,    0,   23,    0,    0,   24,  369,  370,  371,
  372,  170,   24,    0,  373,   25,   26,   27,   28,   29,
    0,   25,   26,   27,   28,   29,    0,  171,    0,    0,
    0,    0,    0,  161,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  321,  322,
  323,    0,  324,  325,    0,    0,    0,  326,    0,  327,
    0,   58,  328,  329,  330,  331,  332,  333,  334,  335,
  336,  337,    0,  338,  339,    0,  340,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  263,
  353,  354,   59,    0,    0,  265,  355,  356,  357,  358,
  359,    0,  360,  361,  362,  363,  364,  365,  366,    0,
  367,  265,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  368,    0,    0,   57,    0,    0,    0,    0,    0,
    0,    0,    0,  369,  370,  371,  372,    0,    0,  223,
  373,    0,    0,    0,    0,    0,    0,    0,   24,    0,
    0,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,    0,    0,    0,    0,    0,    0,  779,    0,
    0,    0,    0,    0,  265,  265,  265,    0,  265,  265,
    0,    0,    0,  265,  487,  265,    0,   58,  265,  265,
  265,  265,  265,  265,  265,  265,  265,  265,    0,  265,
  265,    0,  265,  265,  265,  265,  265,  265,  265,  265,
  265,  265,  265,  265,  265,  266,  265,  265,   59,    0,
    0,  263,  265,  265,  265,  265,  265,    0,  265,  265,
  265,  265,  265,  265,  265,    0,  265,  263,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  265,    0,    0,
   57,    0,    0,    0,    0,    0,    0,    0,    0,  265,
  265,  265,  265,    0,    0,   23,  265,    0,    0,    0,
    0,    0,    0,    0,   24,    0,    0,    0,    0,    0,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  263,  263,  263,    0,  263,  263,    0,    0,    0,  263,
  604,  263,    0,    0,  263,  263,  263,  263,  263,  263,
  263,  263,  263,  263,    0,  263,  263,    0,  263,  263,
  263,  263,  263,  263,  263,  263,  263,  263,  263,  263,
  263,  264,  263,  263,    0,    0,    0,  266,  263,  263,
  263,  263,  263,    0,  263,  263,  263,  263,  263,  263,
  263,    0,  263,  266,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  263,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  263,  263,  263,  263,    0,
    0,   23,  263,    0,    0,    0,    0,    0,    0,    0,
   24,    0,    0,    0,    0,  778,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  266,  266,  266,    0,
  266,  266,    0,    0,    0,  266,    0,  266,    0,   58,
  266,  266,  266,  266,  266,  266,  266,  266,  266,  266,
    0,  266,  266,  677,  266,  266,  266,  266,  266,  266,
  266,  266,  266,  266,  266,  266,  266,    0,  266,  266,
   59,    0,   58,  264,  266,  266,  266,  266,  266,   58,
  266,  266,  266,  266,  266,  266,  266,    0,  266,  264,
    0,    0,    0,    0,    0,    0,    0,  957,    0,  266,
    0,    0,   57,   59,  103,    0,    0,    0,    0,    0,
   59,  266,  266,  266,  266,    0,   58,    0,  266,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   58,    0,   57,    0,    0,    0,    0,
    0,    0,   57,    0,  226,    0,    0,   59,    0,    0,
    0,    0,  264,  264,  264,    0,  264,  264,    0,    0,
    0,  264,    0,  264,   59,    0,  264,  264,  264,  264,
  264,  264,  264,  264,  264,  264,    0,  264,  264,   57,
  264,  264,  264,  264,  264,  264,  264,  264,  264,  264,
  264,  264,  264,   58,  264,  264,   57,    0,    0,    0,
  264,  264,  264,  264,  264,    0,  264,  264,  264,  264,
  264,  264,  264,    0,  264,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   59,  264,   58,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  264,  264,  264,
  264,    0,    0,   23,  264,    0,    0,    0,    0,    0,
    0,    0,   24,    0,    0,    0,   57,   59,    0,   58,
    0,   25,   26,   27,   28,   29,    0,    0,    0,  189,
  190,  191,    0,  192,  193,  194,   23,  195,    0,    0,
    0,    0,    0,  223,  224,   24,    0,    0,  432,   57,
   59,  198,   24,  225,   25,   26,   27,   28,   29,  199,
   58,   25,   26,   27,   28,   29,    0,    0,    0,    0,
    0,    0,    0,  189,  190,  191,   58,  192,  193,  194,
   23,  195,   57,   58,    0,    0,    0,    0,    0,   24,
    0,   59,  432,    0,    0,  198,    0,   23,   25,   26,
   27,   28,   29,  199,    0,    0,   24,   59,    0,    0,
   58,  170,    0,    0,   59,   25,   26,   27,   28,   29,
    0,    0,    0,   57,    0,    0,    0,  171,    0,    0,
    0,    0,    0,   58,    0,    0,    0,    0,    0,   57,
    0,   59,    0,    0,    0,    0,   57,    0,  103,    0,
  189,  190,  191,  411,  192,  193,  194,   23,  412,    0,
    0,    0,   58,    0,   59,    0,   24,  413,    0,  414,
   58,    0,  198,   57,    0,   25,   26,   27,   28,   29,
  199,    0,    0,  189,  190,  191,  411,  192,  193,  194,
   23,  412,    0,   59,    0,  915,   57,   58,    0,   24,
  422,   59,  414,   58,    0,  198,    0,    0,   25,   26,
   27,   28,   29,  199,    0,    0,  189,  190,  191,    0,
  192,  193,  194,   23,  195,   57,    0,    0,   59,    0,
  325,    0,   24,   57,   59,  432,    0,    0,  198,    0,
    0,   25,   26,   27,   28,   29,  199,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   57,  325,    0,    0,  223,  224,  285,    0,    0,    0,
    0,    0,    0,   24,  225,    0,    0,    0,    0,    0,
  223,  428,   25,   26,   27,   28,   29,  223,    0,   24,
  429,    0,    0,  325,    0,    0,   24,    0,   25,   26,
   27,   28,   29,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,   23,    0,    0,    0,    0,    0,
    0,    0,    0,   24,  858,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,   23,    0,    0,
    0,    0,    0,    0,    0,    0,   24,    0,    0,    0,
    0,  778,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,    0,    0,    0,   23,   24,    0,    0,    0,    0,
    0,  319,    0,   24,   25,   26,   27,   28,   29,    0,
    0,    0,   25,   26,   27,   28,   29,  320,    0,    0,
    0,  223,    0,    0,    0,    0,    0,  223,    0,    0,
   24,    0,    0,    0,    0,    0,   24,    0,    0,   25,
   26,   27,   28,   29,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,  325,    0,    0,    0,    0,    0,
    0,    0,    0,  325,    0,    0,    0,    0,    0,    0,
    0,    0,  325,  325,  325,  325,  325,    0,    0,    0,
  321,  322,  323,    0,  324,  325,    0,    0,    0,  326,
    0,  327,    0,    0,  328,  329,  330,  331,  332,  333,
  334,  335,  336,  337,    0,  338,  339,    0,  340,  341,
  342,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,    0,  353,  354,    0,    0,    0,    0,  355,  356,
  357,  358,  359,    0,  360,  361,  362,  363,  364,  365,
  366,  255,  367,    0,  385,  385,    0,    0,    0,    0,
    0,    0,    0,  368,    0,    0,    0,    0,    0,    0,
    0,  116,    0,    0,    0,  369,  370,  371,  372,    0,
    0,    0,  373,    0,    0,   40,   41,    0,   42,   43,
    0,    0,   45,    0,    0,   46,   47,   48,   49,   50,
    0,   51,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  385,  385,  385,
    0,  385,  385,    0,    0,    0,  385,    0,  385,    0,
    0,  385,  385,  385,  385,  385,  385,  385,  385,  385,
  385,    0,  385,  385,    0,  385,  385,  385,  385,  385,
  385,  385,  385,  385,  385,  385,  385,  385,   54,  385,
  385,  112,  356,  356,    0,  385,  385,  385,  385,  385,
    0,  385,  385,  385,  385,  385,  385,  385,    0,  385,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  385,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  385,  385,  385,  385,    0,    0,    0,  385,
   55,   56,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  356,  356,  356,    0,  356,
  356,    0,    0,    0,  356,    0,  356,    0,    0,  356,
  356,  356,  356,  356,  356,  356,  356,  356,  356,    0,
  356,  356,    0,  356,  356,  356,  356,  356,  356,  356,
  356,  356,  356,  356,  356,  356,    0,  356,  356,  336,
  336,    0,    0,  356,  356,  356,  356,  356,    0,  356,
  356,  356,  356,  356,  356,  356,    0,  356,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  356,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  356,  356,  356,  356,    0,    0,    0,  356,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  336,  336,  336,    0,  336,  336,    0,    0,
    0,  336,    0,  336,    0,    0,  336,  336,  336,  336,
  336,  336,  336,  336,  336,  336,    0,  336,  336,    0,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,    0,  336,  336,  342,  342,    0,    0,
  336,  336,  336,  336,  336,    0,  336,  336,  336,  336,
  336,  336,  336,    0,  336,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  336,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  336,  336,  336,
  336,    0,    0,    0,  336,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  342,
  342,  342,    0,  342,  342,    0,    0,    0,  342,    0,
  342,    0,    0,  342,  342,  342,  342,  342,  342,  342,
  342,  342,  342,    0,  342,  342,    0,  342,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,  342,
    0,  342,  342,  337,  337,    0,    0,  342,  342,  342,
  342,  342,    0,  342,  342,  342,  342,  342,  342,  342,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  342,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  342,  342,  342,  342,    0,    0,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  337,  337,  337,    0,
  337,  337,    0,    0,    0,  337,    0,  337,    0,    0,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
    0,  337,  337,    0,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,    0,  337,  337,
  343,  343,    0,    0,  337,  337,  337,  337,  337,    0,
  337,  337,  337,  337,  337,  337,  337,    0,  337,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  337,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  337,  337,  337,  337,    0,    0,    0,  337,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  343,  343,  343,    0,  343,  343,    0,
    0,    0,  343,    0,  343,    0,    0,  343,  343,  343,
  343,  343,  343,  343,  343,  343,  343,    0,  343,  343,
    0,  343,  343,  343,  343,  343,  343,  343,  343,  343,
  343,  343,  343,  343,    0,  343,  343,  338,  338,    0,
    0,  343,  343,  343,  343,  343,    0,  343,  343,  343,
  343,  343,  343,  343,    0,  343,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  343,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  343,  343,
  343,  343,    0,    0,    0,  343,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  338,  338,  338,    0,  338,  338,    0,    0,    0,  338,
    0,  338,    0,    0,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,    0,  338,  338,    0,  338,  338,
  338,  338,  338,  338,  338,  338,  338,  338,  338,  338,
  338,    0,  338,  338,  349,  349,    0,    0,  338,  338,
  338,  338,  338,    0,  338,  338,  338,  338,  338,  338,
  338,    0,  338,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  338,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,  338,  338,  338,    0,
    0,    0,  338,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  349,  349,  349,
    0,  349,  349,    0,    0,    0,  349,    0,  349,    0,
    0,  349,  349,  349,  349,  349,  349,  349,  349,  349,
  349,    0,  349,  349,    0,  349,  349,  349,  349,  349,
  349,  349,  349,  349,  349,  349,  349,  349,    0,  349,
  349,  371,  371,    0,    0,  349,  349,  349,  349,  349,
    0,  349,  349,  349,  349,  349,  349,  349,    0,  349,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  349,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  349,  349,  349,  349,    0,    0,    0,  349,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  371,  371,  371,    0,  371,  371,
    0,    0,    0,  371,    0,  371,    0,    0,  371,  371,
  371,  371,  371,  371,  371,  371,  371,  371,    0,  371,
  371,    0,  371,  371,  371,  371,  371,  371,  371,  371,
  371,  371,  371,  371,  371,    0,  371,  371,  367,  367,
    0,    0,  371,  371,  371,  371,  371,    0,  371,  371,
  371,  371,  371,  371,  371,    0,  371,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  371,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  371,
  371,  371,  371,    0,    0,    0,  371,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  367,  367,  367,    0,  367,  367,    0,    0,    0,
  367,    0,  367,    0,    0,  367,  367,  367,  367,  367,
  367,  367,  367,  367,  367,    0,  367,  367,    0,  367,
  367,  367,  367,  367,  367,  367,  367,  367,  367,  367,
  367,  367,    0,  367,  367,  344,  344,    0,    0,  367,
  367,  367,  367,  367,    0,  367,  367,  367,  367,  367,
  367,  367,    0,  367,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  367,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  367,  367,  367,  367,
    0,    0,    0,  367,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  344,  344,
  344,    0,  344,  344,    0,    0,    0,  344,    0,  344,
    0,    0,  344,  344,  344,  344,  344,  344,  344,  344,
  344,  344,    0,  344,  344,    0,  344,  344,  344,  344,
  344,  344,  344,  344,  344,  344,  344,  344,  344,    0,
  344,  344,  339,  339,    0,    0,  344,  344,  344,  344,
  344,    0,  344,  344,  344,  344,  344,  344,  344,    0,
  344,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  344,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  344,  344,  344,  344,    0,    0,    0,
  344,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  339,  339,  339,    0,  339,
  339,    0,    0,    0,  339,    0,  339,    0,    0,  339,
  339,  339,  339,  339,  339,  339,  339,  339,  339,    0,
  339,  339,    0,  339,  339,  339,  339,  339,  339,  339,
  339,  339,  339,  339,  339,  339,    0,  339,  339,  340,
  340,    0,    0,  339,  339,  339,  339,  339,    0,  339,
  339,  339,  339,  339,  339,  339,    0,  339,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  339,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  339,  339,  339,  339,    0,    0,    0,  339,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,  340,  340,    0,  340,  340,    0,    0,
    0,  340,    0,  340,    0,    0,  340,  340,  340,  340,
  340,  340,  340,  340,  340,  340,    0,  340,  340,    0,
  340,  340,  340,  340,  340,  340,  340,  340,  340,  340,
  340,  340,  340,    0,  340,  340,  345,  345,    0,    0,
  340,  340,  340,  340,  340,    0,  340,  340,  340,  340,
  340,  340,  340,    0,  340,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  340,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  340,  340,  340,
  340,    0,    0,    0,  340,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  345,
  345,  345,    0,  345,  345,    0,    0,    0,  345,    0,
  345,    0,    0,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,    0,  345,  345,    0,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
    0,  345,  345,  354,  354,    0,    0,  345,  345,  345,
  345,  345,    0,  345,  345,  345,  345,  345,  345,  345,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  345,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  345,  345,  345,  345,    0,    0,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  354,  354,  354,    0,
  354,  354,    0,    0,    0,  354,    0,  354,    0,    0,
  354,  354,  354,  354,  354,  354,  354,  354,  354,  354,
    0,  354,  354,    0,  354,  354,  354,  354,  354,  354,
  354,  354,  354,  354,  354,  354,  354,    0,  354,  354,
  347,  347,    0,    0,  354,  354,  354,  354,  354,    0,
  354,  354,  354,  354,  354,  354,  354,    0,  354,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  354,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  354,  354,  354,  354,    0,    0,    0,  354,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  347,  347,  347,    0,  347,  347,    0,
    0,    0,  347,    0,  347,    0,    0,  347,  347,  347,
  347,  347,  347,  347,  347,  347,  347,    0,  347,  347,
    0,  347,  347,  347,  347,  347,  347,  347,  347,  347,
  347,  347,  347,  347,    0,  347,  347,  350,  350,    0,
    0,  347,  347,  347,  347,  347,    0,  347,  347,  347,
  347,  347,  347,  347,    0,  347,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  347,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  347,  347,
  347,  347,    0,    0,    0,  347,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  350,  350,  350,    0,  350,  350,    0,    0,    0,  350,
    0,  350,    0,    0,  350,  350,  350,  350,  350,  350,
  350,  350,  350,  350,    0,  350,  350,    0,  350,  350,
  350,  350,  350,  350,  350,  350,  350,  350,  350,  350,
  350,    0,  350,  350,  368,  368,    0,    0,  350,  350,
  350,  350,  350,    0,  350,  350,  350,  350,  350,  350,
  350,    0,  350,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  350,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  350,  350,  350,  350,    0,
    0,    0,  350,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  368,  368,  368,
    0,  368,  368,    0,    0,    0,  368,    0,  368,    0,
    0,  368,  368,  368,  368,  368,  368,  368,  368,  368,
  368,    0,  368,  368,    0,  368,  368,  368,  368,  368,
  368,  368,  368,  368,  368,  368,  368,  368,    0,  368,
  368,  341,  341,    0,    0,  368,  368,  368,  368,  368,
    0,  368,  368,  368,  368,  368,  368,  368,    0,  368,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  368,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  368,  368,  368,  368,    0,    0,    0,  368,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,  341,  341,    0,  341,  341,
    0,    0,    0,  341,    0,  341,    0,    0,  341,  341,
  341,  341,  341,  341,  341,  341,  341,  341,    0,  341,
  341,    0,  341,  341,  341,  341,  341,  341,  341,  341,
  341,  341,  341,  341,  341,    0,  341,  341,  346,  346,
    0,    0,  341,  341,  341,  341,  341,    0,  341,  341,
  341,  341,  341,  341,  341,    0,  341,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  341,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  341,
  341,  341,  341,    0,    0,    0,  341,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  346,  346,  346,    0,  346,  346,    0,    0,    0,
  346,    0,  346,    0,    0,  346,  346,  346,  346,  346,
  346,  346,  346,  346,  346,    0,  346,  346,    0,  346,
  346,  346,  346,  346,  346,  346,  346,  346,  346,  346,
  346,  346,    0,  346,  346,  348,  348,    0,    0,  346,
  346,  346,  346,  346,    0,  346,  346,  346,  346,  346,
  346,  346,    0,  346,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  346,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  346,  346,  346,  346,
    0,    0,    0,  346,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  348,  348,
  348,    0,  348,  348,    0,    0,    0,  348,    0,  348,
    0,    0,  348,  348,  348,  348,  348,  348,  348,  348,
  348,  348,    0,  348,  348,    0,  348,  348,  348,  348,
  348,  348,  348,  348,  348,  348,  348,  348,  348,    0,
  348,  348,  351,  351,    0,    0,  348,  348,  348,  348,
  348,    0,  348,  348,  348,  348,  348,  348,  348,    0,
  348,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  348,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  348,  348,  348,  348,    0,    0,    0,
  348,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  351,  351,  351,    0,  351,
  351,    0,    0,    0,  351,    0,  351,    0,    0,  351,
  351,  351,  351,  351,  351,  351,  351,  351,  351,    0,
  351,  351,    0,  351,  351,  351,  351,  351,  351,  351,
  351,  351,  351,  351,  351,  351,    0,  351,  351,  352,
  352,    0,    0,  351,  351,  351,  351,  351,    0,  351,
  351,  351,  351,  351,  351,  351,    0,  351,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  351,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,  351,  351,  351,    0,    0,    0,  351,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  352,  352,  352,    0,  352,  352,    0,    0,
    0,  352,    0,  352,    0,    0,  352,  352,  352,  352,
  352,  352,  352,  352,  352,  352,    0,  352,  352,    0,
  352,  352,  352,  352,  352,  352,  352,  352,  352,  352,
  352,  352,  352,    0,  352,  352,  353,  353,    0,    0,
  352,  352,  352,  352,  352,    0,  352,  352,  352,  352,
  352,  352,  352,    0,  352,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  352,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  352,  352,  352,
  352,    0,    0,    0,  352,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  353,
  353,  353,    0,  353,  353,    0,    0,    0,  353,    0,
  353,    0,    0,  353,  353,  353,  353,  353,  353,  353,
  353,  353,  353,    0,  353,  353,    0,  353,  353,  353,
  353,  353,  353,  353,  353,  353,  353,  353,  353,  353,
  320,  353,  353,    0,    0,    0,    0,  353,  353,  353,
  353,  353,    0,  353,  353,  353,  353,  353,  353,  353,
    0,  353,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  353,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  353,  353,  353,  353,    0,    0,
    0,  353,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  321,  322,  323,    0,  324,  325,    0,
    0,    0,  326,    0,  327,    0,    0,  328,  329,  330,
  331,  332,  333,  334,  335,  336,  337,    0,  338,  339,
    0,  340,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  269,  353,  354,    0,    0,    0,
    0,  355,  356,  357,  358,  359,    0,  360,  361,  362,
  363,  364,  365,  366,    0,  367,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  368,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  369,  370,
  371,  372,    0,    0,    0,  373,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  269,  269,  269,
    0,  269,  269,    0,    0,    0,  269,    0,  269,    0,
    0,  269,  269,  269,  269,  269,  269,  269,  269,  269,
  269,    0,  269,  269,    0,  269,  269,  269,  269,  269,
  269,  269,  269,  269,  269,  269,  269,  269,  270,  269,
  269,    0,    0,    0,    0,  269,  269,  269,  269,  269,
    0,  269,  269,  269,  269,  269,  269,  269,    0,  269,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  269,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  269,  269,  269,  269,    0,    0,    0,  269,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  270,  270,  270,    0,  270,  270,    0,    0,    0,
  270,    0,  270,    0,    0,  270,  270,  270,  270,  270,
  270,  270,  270,  270,  270,    0,  270,  270,    0,  270,
  270,  270,  270,  270,  270,  270,  270,  270,  270,  270,
  270,  270,  271,  270,  270,    0,    0,    0,    0,  270,
  270,  270,  270,  270,    0,  270,  270,  270,  270,  270,
  270,  270,    0,  270,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  270,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  270,  270,  270,  270,
    0,    0,    0,  270,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  271,  271,  271,    0,  271,
  271,    0,    0,    0,  271,    0,  271,    0,    0,  271,
  271,  271,  271,  271,  271,  271,  271,  271,  271,    0,
  271,  271,    0,  271,  271,  271,  271,  271,  271,  271,
  271,  271,  271,  271,  271,  271,  272,  271,  271,    0,
    0,    0,    0,  271,  271,  271,  271,  271,    0,  271,
  271,  271,  271,  271,  271,  271,    0,  271,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  271,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  271,  271,  271,  271,    0,    0,    0,  271,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  272,
  272,  272,    0,  272,  272,    0,    0,    0,  272,    0,
  272,    0,    0,  272,  272,  272,  272,  272,  272,  272,
  272,  272,  272,    0,  272,  272,    0,  272,  272,  272,
  272,  272,  272,  272,  272,  272,  272,  272,  272,  272,
    0,  272,  272,    0,    0,    0,    0,  272,  272,  272,
  272,  272,    0,  272,  272,  272,  272,  272,  272,  272,
    0,  272,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  272,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  324,    0,  272,  272,  272,  272,    0,  327,
    0,  272,  328,  329,  330,  331,  332,  333,  334,  335,
  336,  337,    0,  338,  339,    0,  340,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,    0,
  353,  354,    0,    0,    0,    0,  355,  356,  357,  358,
  359,    0,  360,  361,  362,  363,  364,  365,  366,    0,
  367,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  368,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  369,  370,  371,  372,    0,    0,    0,
  373,
  };
  protected static readonly short [] yyCheck = {             6,
   44,   44,    6,  552,   41,   44,  178,  566,   41,   16,
   41,   61,   41,   91,   44,   41,   41,   62,   41,   33,
   93,   41,   40,  127,   42,   40,  123,   33,   40,  374,
   65,  123,  123,   44,  259,   40,   44,  123,   73,  267,
  608,   44,   60,   60,  272,  124,   44,   44,   44,  280,
   57,   40,  293,  294,  295,  274,   40,   64,   65,   44,
  110,   44,   20,   44,   44,  123,   73,  123,  123,   76,
  311,  179,   76,   91,  263,  123,  280,  181,  315,  280,
  184,   88,  266,   61,  273,  276,   65,  276,   44,   41,
  281,   61,   44,  100,  101,  280,  777,   44,  177,  347,
   20,  108,  286,  351,  828,  123,  123,  114,   87,  313,
  117,   41,  119,   61,   44,  119,  123,  125,   44,  126,
  324,   44,  126,  682,  848,   44,  133,   61,  313,   87,
  137,   89,  139,   91,  205,  206,   94,   93,  173,  324,
  270,  271,    0,   62,  125,  125,  383,  154,  155,  380,
  157,  259,   41,    6,  722,   44,  724,  324,    0,  727,
  388,  386,  270,   16,  268,  257,  124,   87,  892,   89,
   60,   91,  124,  260,  415,  856,  380,  274,  125,  380,
  187,  188,   40,  274,   42,  291,  292,  532,  274,  380,
  269,  272,  429,  306,  383,  380,  295,  296,   40,  125,
   42,   91,  125,   40,  124,  212,  378,  214,  380,  291,
  292,   64,   65,  438,  285,   40,  274,   42,  274,  274,
   73,  334,  335,   76,  783,  183,  274,  291,  292,  281,
   61,  295,  296,  123,  241,   41,   40,   61,   44,  257,
  258,  259,  324,  261,  262,  263,   41,  265,   40,   44,
  818,  258,  256,    0,  272,  273,   41,  272,  322,   44,
  274,  279,   41,  277,  117,   44,  119,  272,  274,  287,
  257,  288,  279,  126,  324,  257,   41,  383,  285,   44,
  383,  389, 1006,  272,   40,   61,  293,  294,  272,  324,
   40,  260,  260,  274,  274,  854,   40,  274,   42,  274,
   44,  383,    0,   40,  311,  383,  865,  866,   40,  267,
  383,  552,   41,  554,  272,   44,  316,  324,  559,  383,
  327,  371,  329,  123,  331,   41,  333,  334,   44,  336,
  337,  338,  339,  340,  341,  342,  343,   40,  383,   42,
  383,   44,  349,  350,  383,   41,  383,  354,   44,  257,
  383,  267,  383,  383,  383,  914,  528,  383,  383,  600,
  383,  379,  369,  383,  371,    0,  373,  371,  917,  381,
  929,  267,  383,  932,  264,  547,  394,  395,  396,   41,
  383,  125,   44,  273,  306,  383,  383,  383,  395,   40,
  562,  435,  282,  283,  284,  285,  286,  330,  383,  332,
  383,   42,  335,  274,  453,  964,  965,  257,  426,  458,
  268,  269,  461,  448,  272,  273,  329,  275,  331,  309,
   41,  470,  125,   44,  983,  338,  268,  269,  260,  123,
  388,  289,  290,  275,  316,  368,   41,  370,  123,   44,
  447,  448,   41,  447,  276,   44,  453,  289,  290,  456,
   40,  458,  459,  123,  461,  462,   41,  464,   87,  466,
   89,   90,  380,  470,  471,  324,  473,  325,  291,  292,
  257,  324,  295,  296,  524,   41,   40,  484,  485,   40,
   40,   44,   58,  325,  491,  492,  493,  319,  320,  321,
  662,   58,    0,   58,  326,  327,   41,  329,  330,  331,
  332,  673,  257,  675,   62,  134,  135,   61,   40,  138,
  517,  140,  260,  520,  257,  522,  523,  524,  371,  523,
  524,  268,  269,  380,   44,  272,  273,  149,  275,   40,
   41,   42,  380,   44,   40,  542,  543,  544,  260,   44,
   44,  260,  289,  290,  434,  552,  257,  554,   58,   40,
   41,   42,  559,   44,   61,  261,  262,  257,  258,  259,
  383,  261,  262,  263,  571,  265,  420,  571,  423,  316,
  268,  269,  813,  814,  272,  273,  333,  275,  325,  279,
  313,  257,  204,  205,  206,   44,    0,  287,  389,  380,
  389,  289,  290,  765,  447,  448,  389,  604,  257,  606,
  222,  608,  774,  380,   44,  380,  613,  614,  615,  616,
  617,  618,  619,  620,  621,  622,  623,  624,  859,   44,
   40,  656,  124,  273,  260,   44,   44,  325,   44,   44,
  389,   44,   44,  268,  269,  642,   44,  272,  273,  257,
  275,  263,  257,  389,  389,  652,  653,  389,  655,  656,
  389,  389,   62,  389,  289,  290,  257,  257,  280,  281,
  282,  389,  389,  285,  389,   40,  389,    0,  389,  522,
  523,  524,  751,  752,   44,  380,  917,  684,  380,  266,
  302,   40,   44,  383,   44,   60,   44,   44,   44,   44,
  325,   44,   44,   44,   44,  401,  402,  403,  404,  321,
   44,  323,   44,  325,  410,  411,  412,  413,  414,  415,
  416,  417,  418,  419,   44,  722,   91,  724,  571,   44,
  727,   44,  344,  345,  346,  347,  348,  835,  836,  351,
  427,  260,  257,  355,  356,  357,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  780,   44,  389,  123,   44,
   91,   44,   44,   44,  761,   44,    0,  761,  837,  838,
  268,  269,  841,  842,  272,  273,   44,  275,  257,  257,
  777,  778,   44,   44,   44,   44,   44,   44,   44,  401,
   44,  289,  290,   44,  257,   44,   44,   44,   44,  136,
  123,  899,  900,   44,   44,  903,  904,   44,   44,  652,
  653,   91,  655,  656,   44,  260,   44,   44,  257,  258,
  259,  818,  261,  262,  263,   44,  265,  325,  825,  826,
  389,  389,  901,  902,  380,   44,  905,  906,   44,  380,
  279,  521,   44,  380,   44,   44,   44,   60,  287,  186,
  948,  949,  950,  951,  380,   44,   40,  328,  389,  856,
  257,  257,  380,  380,  268,  269,  863,  861,  272,  273,
  257,  275,  380,   44,  211,  487,  213,  975,   91,   44,
   44,   93,  260,  952,  257,  289,  290,   40,  381,  569,
  570,  228,  257,  258,  259,   60,  261,  262,  263,   40,
  265,   40,  328,  328,  257,  380,   44,  272,  273,  333,
  123,  333,  268,  269,  279,   40,  272,  273,  761,  275,
  917,  325,  287,  380,  257,   44,   91,  257,   41,  541,
  257,   44,   44,  289,  290,  328,  389,  389,  389,  257,
  277,  257,  380,  380,   44,  268,  269,   60,  257,  272,
  273,  274,  275,  290,    0,  292,   10,  316,  123,   95,
  272,  272,  272,   20,  272,  272,  289,  290,  221,  325,
  650,  651,  295,  296,  654,  297,  554,  222,   91,  253,
  977,  108,  125,   61,  383,  597,  598,  599,    0,  601,
  602,  294,  989,  990,  991,  258,  813,  241,  912,  439,
  612,  324,  325,  814,  440,  377,  377,  863,  688,  748,
  123,  654,  559,  440,  379,  856,   -1,   -1,    0,  399,
  400,  401,  402,  403,  404,  405,  406,  407,  408,  394,
  395,  396,   -1,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,   -1,   -1,   -1,   -1,  658,   -1,   -1,   -1,
   -1,  264,   -1,   -1,  666,  289,  290,   -1,   -1,   -1,
  273,  426,   -1,   -1,    0,  402,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,  755,  756,   -1,   -1,  759,
  760,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  325,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,   -1,  443,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,  452,   -1,  260,   -1,  731,
  457,   -1,  287,  460,   -1,   -1,  463,   -1,  465,   -1,
  467,  468,  469,  276,   -1,  472,   -1,  474,  475,  476,
  477,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  264,  265,   -1,   -1,    0,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,  845,  279,   -1,  381,  282,
  283,  284,  285,  286,  287,   -1,  319,  320,  321,   -1,
  293,   -1,   40,  326,  327,   -1,  329,  330,  331,  332,
   -1,  125,   -1,   -1,  307,  308,   -1,  310,  311,  811,
  812,  314,   60,   -1,  317,  318,  319,  320,  321,   -1,
  323,   -1,   -1,   -1,  379,   -1,   -1,   -1,  383,   -1,
   -1,  833,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  394,
  395,  396,   -1,   91,   -1,   -1,  573,  849,  575,   -1,
  577,  578,   -1,  580,  581,   -1,  583,   -1,  585,   -1,
   -1,   -1,  589,  590,    0,  592,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,  123,  379,  380,   -1,   -1,
   -1,   -1,  609,  610,   -1,   -1,   -1,  289,  290,   -1,
   -1,  394,  395,  396,   -1,   -1,  268,  269,  625,   -1,
  272,  273,   -1,  275,   40,   41,   42,   -1,   44,   -1,
   -1,   -1,  125,   -1,   -1,   -1,  643,  289,  290,  646,
   -1,   -1,   -1,  325,   60,   -1,   62,   -1,   -1,  432,
  433,   -1,   -1,   -1,   -1,   -1,  260,   -1,   -1,   -1,
   -1,   -1,  268,  269,  946,   -1,  272,  273,   -1,  275,
   -1,   -1,  276,  325,   -1,   91,   -1,   93,  960,  961,
  962,   -1,  689,  289,  290,  692,   -1,   -1,  695,   -1,
   -1,  698,   -1,  700,   -1,  702,  703,  704,   -1,   -1,
  707,   -1,  709,  710,  711,  712,   -1,  123,   -1,  125,
   -1,   -1,   -1,   -1,  996,  319,  320,  321,  725,  325,
   -1,   -1,  326,  327,   -1,  329,  330,  331,  332,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,  746,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,  268,  269,   -1,   -1,  272,  273,  287,
  275,   -1,   -1,   -1,   -1,   -1,   -1,  260,   -1,   -1,
   -1,   -1,    0,  780,  289,  290,   -1,   60,   -1,   -1,
   -1,  788,  789,  276,  791,  792,   -1,  794,  795,   -1,
  797,   -1,  799,   -1,   -1,   -1,  803,  804,   -1,  806,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,
  325,   41,   -1,   -1,   44,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  829,   -1,  831,   -1,  319,  320,  321,   -1,
   60,    0,   -1,  326,  327,   -1,  329,  330,  331,  332,
  123,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,  379,  268,  269,  861,   -1,  272,  273,  274,  275,
  276,   91,    0,  279,   -1,   -1,  394,  395,  396,   -1,
   -1,  287,   -1,  289,  290,   -1,   -1,  293,   -1,   -1,
   -1,   -1,   -1,  890,  891,   -1,   -1,   -1,  895,   -1,
   -1,  307,  308,  123,  310,  311,   -1,   -1,  314,  315,
   -1,  317,  318,  319,  320,  321,   -1,  323,   -1,  325,
   -1,   40,   41,   -1,   -1,   44,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   62,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,   93,   -1,   -1,  383,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,  264,  398,   -1,   -1,  123,   -1,   -1,   -1,   -1,
  273,   -1,   -1,  409,  123,   -1,  125,   -1,   -1,  282,
  283,  284,  285,  286,   -1,  421,  422,  423,  424,   -1,
  426,   -1,  428,  429,   -1,   -1,  432,  433,   40,   -1,
   42,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  264,  265,   -1,   -1,   -1,   -1,
  268,  269,  272,  273,  272,  273,   -1,  275,   -1,  279,
   -1,   -1,  282,  283,  284,  285,  286,  287,   -1,   -1,
   -1,  289,  290,  293,   -1,  348,  349,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,   60,  314,   -1,   -1,  317,  318,  319,
  320,  321,   -1,  323,   -1,   -1,   -1,  325,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
  289,  290,   -1,    0,   -1,   40,   -1,   42,  257,   -1,
  268,  269,   -1,   -1,  272,  273,  274,  275,   -1,   -1,
   -1,   -1,   -1,   -1,  273,  274,  123,   -1,   -1,  379,
  380,  289,  290,   -1,   -1,   -1,  325,  295,  296,   -1,
   -1,   -1,   -1,   -1,  394,  395,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,   -1,
   -1,   -1,   -1,   -1,  291,  292,  324,  325,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,   -1,
   -1,   -1,  432,  433,   -1,  312,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,  322,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,  273,  274,   -1,   -1,  384,  385,  386,  387,  388,
  389,  390,  391,  392,  393,  394,  395,  396,   40,  398,
   42,   -1,   -1,   -1,   -1,   -1,  383,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,  273,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   -1,  378,  379,  273,  274,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   40,  398,   42,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,  421,
  422,  423,  424,   -1,   -1,  382,  428,   -1,   -1,   -1,
   -1,   -1,  289,  290,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,  325,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,   -1,   -1,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   40,  398,   42,   -1,   -1,   -1,   -1,   -1,
  261,  262,   -1,   -1,  409,   -1,   -1,   -1,  273,   -1,
   -1,  273,  274,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  307,  308,   -1,  310,  311,   -1,   -1,  314,
   -1,   -1,  317,  318,  319,  320,  321,   -1,  323,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,  380,  378,  379,  273,  274,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   40,  398,   42,   -1,   -1,
  401,  402,  403,  404,   -1,   -1,   -1,  409,   -1,  410,
  411,  412,  413,  414,  415,  416,  417,  418,  419,  421,
  422,  423,  424,   -1,   -1,   -1,  428,  432,  433,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   40,  398,   42,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  409,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,  421,  422,  423,  424,
  272,  273,   -1,  428,  276,   -1,   -1,  279,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  287,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,   -1,   -1,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   40,
  398,   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,  421,  422,  423,  424,  379,   -1,   -1,
  428,  383,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  394,  395,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,  273,  274,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   40,  398,   42,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,  273,  274,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   40,
  398,   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,   -1,
   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   40,  398,   42,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,  123,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,  273,  274,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   40,
  398,   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,  273,
  274,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,  264,  398,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  409,   -1,
   -1,    0,   -1,   -1,  282,  283,  284,  285,  286,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   60,  378,  379,   -1,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  380,   -1,   91,  409,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  274,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,
   -1,    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   91,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,  123,
   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  264,   -1,   -1,
   -1,   -1,   -1,   -1,  123,   -1,  273,   -1,   -1,   -1,
  289,  290,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   60,   -1,  293,   -1,   -1,   -1,
   -1,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,   -1,  310,  311,  312,  325,  314,   -1,   -1,
  317,  318,  319,  320,  321,   91,  323,   -1,   -1,   -1,
   -1,   -1,   -1,  648,  649,   -1,   60,  334,  335,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,  123,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   91,   -1,   -1,
   -1,  686,  687,  287,   -1,   -1,   -1,   -1,   -1,  293,
   -1,   -1,   -1,  380,   -1,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,  123,
  314,   -1,   -1,  317,  318,  319,  320,  321,   -1,  323,
   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,  268,
  269,   -1,   -1,  272,  273,  274,  275,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  432,  433,   -1,  753,  754,
  289,  290,  757,  758,    0,   -1,  295,  296,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,  379,  380,  316,   -1,  383,
   -1,  786,   -1,   -1,   -1,  324,  325,   -1,   -1,   -1,
  394,  395,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,   -1,   60,   -1,   -1,   -1,  272,  273,   -1,   -1,
  268,  269,   -1,  279,  272,  273,   60,  275,  432,  433,
   -1,  287,   -1,   -1,  839,  840,   -1,  293,  843,  844,
   -1,  289,  290,   91,   -1,   -1,   -1,   -1,   -1,   -1,
  264,  307,  308,   -1,  310,  311,   -1,   91,  314,  273,
   -1,  317,  318,  319,  320,  321,   -1,  323,  282,  283,
  284,  285,  286,   -1,   -1,  123,   -1,  325,   -1,  293,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,
   -1,   -1,  306,  307,  308,   -1,  310,  311,   -1,  264,
  314,   60,  907,  317,  318,  319,  320,  321,  273,  323,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,  379,  380,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  394,  395,
  396,  264,  307,  308,   -1,  310,  311,   -1,   -1,  314,
  273,   -1,  317,  318,  319,  320,  321,  322,  323,  282,
  283,  284,  285,  286,  123,   -1,  380,   -1,   60,   -1,
  293,   -1,   -1,   -1,   -1,   -1,  432,  433,   -1,   -1,
   -1,   -1,   -1,  306,  307,  308,   -1,  310,  311,   -1,
   -1,  314,   -1,   -1,  317,  318,  319,  320,  321,   91,
  323,   -1,   -1,   -1,   -1,   -1,  420,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  380,  264,   -1,  432,  433,
   -1,   -1,  268,  269,   -1,  273,  272,  273,   -1,  275,
  264,  123,   -1,   60,  282,  283,  284,  285,  286,  273,
   -1,   -1,   -1,  289,  290,  293,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,  380,   -1,  307,
  308,   -1,  310,  311,   91,   -1,  314,  432,  433,  317,
  318,  319,  320,  321,  322,  323,   -1,   -1,   -1,  325,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,  264,   -1,   -1,   -1,  432,
  433,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,  363,
   -1,   -1,  380,   -1,  293,   91,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,
   -1,  310,  311,   -1,   -1,  314,   -1,   -1,  317,  318,
  319,  320,  321,  322,  323,   -1,   -1,  123,   -1,   60,
   -1,   -1,  264,   -1,   -1,   60,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,  432,  433,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,
   91,  293,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   44,  380,  314,   -1,   -1,  317,  318,  319,  320,  321,
   -1,  323,  123,   -1,   -1,   -1,   60,  264,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,  293,   91,   -1,   60,
   -1,   -1,   -1,  432,  433,   -1,   -1,   -1,   -1,   -1,
  307,  308,   -1,  310,  311,   -1,   -1,  314,  380,   -1,
  317,  318,  319,  320,  321,   -1,  323,   -1,   -1,  123,
   91,   -1,   -1,   60,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,  123,   -1,   91,   -1,   -1,  293,   -1,   -1,
  432,  433,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   -1,  307,  308,  380,  310,  311,   -1,   -1,  314,   -1,
   -1,  317,  318,  319,  320,  321,  123,  323,   41,   -1,
   -1,   -1,   -1,  264,   -1,   -1,  123,   -1,   -1,  264,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   60,  273,   -1,
   -1,  282,  283,  284,  285,  286,   -1,  282,  283,  284,
  285,  286,  293,   -1,   -1,  432,  433,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   91,  310,
  311,   -1,   -1,  314,  380,   -1,  317,  318,  319,  320,
  321,  125,  323,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
  123,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  287,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,  432,  433,  363,   -1,
   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,  380,
   -1,   -1,   -1,   -1,   41,   -1,  287,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   60,   -1,  272,  273,  264,   -1,   60,
   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,   -1,   -1,
  287,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,  432,  433,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  379,   -1,  125,   -1,  383,
   -1,   -1,   -1,  257,   -1,   -1,   -1,   -1,   -1,   -1,
  394,  395,  396,   -1,   -1,   -1,  123,   -1,   -1,  273,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,  379,   -1,
   -1,  264,  383,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,  394,  395,  396,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,  363,   -1,   -1,   -1,
   -1,  294,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  394,  395,  396,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   -1,   60,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,  125,  378,  379,   91,   -1,   -1,  257,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,  264,  123,   -1,
   -1,   -1,   -1,  264,   -1,   -1,  273,  421,  422,  423,
  424,  278,  273,   -1,  428,  282,  283,  284,  285,  286,
   -1,  282,  283,  284,  285,  286,   -1,  294,   -1,   -1,
   -1,   -1,   -1,  294,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,
  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,
   -1,   60,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,  125,
  378,  379,   91,   -1,   -1,  257,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,  264,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,   -1,  345,  309,  347,   -1,   60,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,  125,  378,  379,   91,   -1,
   -1,  257,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,  264,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
  309,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,  125,  378,  379,   -1,   -1,   -1,  257,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,  264,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   60,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   41,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,   -1,  378,  379,
   91,   -1,   60,  257,  384,  385,  386,  387,  388,   60,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,  409,
   -1,   -1,  123,   91,  125,   -1,   -1,   -1,   -1,   -1,
   91,  421,  422,  423,  424,   -1,   60,   -1,  428,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,  125,   -1,   -1,   91,   -1,   -1,
   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,
   -1,  345,   -1,  347,   91,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,  123,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   60,  378,  379,  123,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,  409,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,  264,  428,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,  123,   91,   -1,   60,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,  264,  265,   -1,   -1,
   -1,   -1,   -1,  264,  265,  273,   -1,   -1,  276,  123,
   91,  279,  273,  274,  282,  283,  284,  285,  286,  287,
   60,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,   60,  261,  262,  263,
  264,  265,  123,   60,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   91,  276,   -1,   -1,  279,   -1,  264,  282,  283,
  284,  285,  286,  287,   -1,   -1,  273,   91,   -1,   -1,
   60,  278,   -1,   -1,   91,  282,  283,  284,  285,  286,
   -1,   -1,   -1,  123,   -1,   -1,   -1,  294,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,  123,
   -1,   91,   -1,   -1,   -1,   -1,  123,   -1,  125,   -1,
  257,  258,  259,  260,  261,  262,  263,  264,  265,   -1,
   -1,   -1,   60,   -1,   91,   -1,  273,  274,   -1,  276,
   60,   -1,  279,  123,   -1,  282,  283,  284,  285,  286,
  287,   -1,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,   -1,   91,   -1,   93,  123,   60,   -1,  273,
  274,   91,  276,   60,   -1,  279,   -1,   -1,  282,  283,
  284,  285,  286,  287,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,  264,  265,  123,   -1,   -1,   91,   -1,
   60,   -1,  273,  123,   91,  276,   -1,   -1,  279,   -1,
   -1,  282,  283,  284,  285,  286,  287,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   91,   -1,   -1,  264,  265,  123,   -1,   -1,   -1,
   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,
  264,  265,  282,  283,  284,  285,  286,  264,   -1,  273,
  274,   -1,   -1,  123,   -1,   -1,  273,   -1,  282,  283,
  284,  285,  286,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  278,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,  273,   -1,   -1,   -1,   -1,
   -1,  257,   -1,  273,  282,  283,  284,  285,  286,   -1,
   -1,   -1,  282,  283,  284,  285,  286,  273,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  282,
  283,  284,  285,  286,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,   -1,   -1,   -1,   -1,  384,  385,
  386,  387,  388,   -1,  390,  391,  392,  393,  394,  395,
  396,  273,  398,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  293,   -1,   -1,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,   -1,  314,   -1,   -1,  317,  318,  319,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,  380,  378,
  379,  383,  273,  274,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
  432,  433,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
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
  273,  378,  379,   -1,   -1,   -1,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,
   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,  273,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,  273,  378,
  379,   -1,   -1,   -1,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,  273,  378,  379,   -1,   -1,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,   -1,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,  273,  378,  379,   -1,
   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   -1,   -1,   -1,   -1,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  340,   -1,  421,  422,  423,  424,   -1,  347,
   -1,  428,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,   -1,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,   -1,   -1,   -1,   -1,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,
  };

#line 1435 "Iril/IR/IR.jay"

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
