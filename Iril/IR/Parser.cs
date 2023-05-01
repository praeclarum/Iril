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
//t    "nonglobal_value : ADDRSPACECAST '(' typed_value TO type ')'",
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
case 235:
#line 727 "Iril/IR/IR.jay"
  {
        yyVal = new AddrSpaceCastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 237:
#line 735 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 238:
#line 736 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 239:
#line 737 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 240:
#line 738 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 241:
#line 739 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 242:
#line 740 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 243:
#line 741 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 244:
#line 742 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 245:
#line 743 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 246:
#line 750 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 757 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 761 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 249:
#line 768 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 775 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 779 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 786 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 794 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 255:
#line 801 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 805 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 816 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 820 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 827 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 831 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 838 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 263:
#line 842 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 264:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 853 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 266:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 268:
#line 868 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 872 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 879 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 883 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 887 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 899 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 276:
#line 900 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 277:
#line 907 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 911 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 280:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 281:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 282:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 283:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 284:
#line 938 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 286:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 954 "Iril/IR/IR.jay"
  {
        yyVal = new SymbolValue ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 958 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 290:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 291:
#line 966 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 292:
#line 970 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 300:
#line 990 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 994 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1000 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 303:
#line 1007 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1011 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1018 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1036 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 312:
#line 1043 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1054 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 320:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
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
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = false;
    }
  break;
case 327:
#line 1112 "Iril/IR/IR.jay"
  {
        yyVal = true;
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
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 330:
#line 1127 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 331:
#line 1131 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 332:
#line 1135 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 333:
#line 1139 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1143 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 335:
#line 1147 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 336:
#line 1151 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1155 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 338:
#line 1159 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 339:
#line 1163 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 340:
#line 1167 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 341:
#line 1171 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 344:
#line 1183 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 345:
#line 1187 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 348:
#line 1199 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 349:
#line 1203 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 352:
#line 1215 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 353:
#line 1219 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 356:
#line 1231 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1235 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1239 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1243 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1247 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1251 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1255 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1259 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1263 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1267 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1271 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1275 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1287 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1291 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1295 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 373:
#line 1299 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 374:
#line 1303 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 375:
#line 1307 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 376:
#line 1311 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: false);
    }
  break;
case 377:
#line 1315 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: true);
    }
  break;
case 378:
#line 1319 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: false);
    }
  break;
case 379:
#line 1323 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 380:
#line 1327 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-6+yyTop], (TypedValue)yyVals[-4+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 381:
#line 1331 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 382:
#line 1335 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 383:
#line 1339 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 386:
#line 1351 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 387:
#line 1355 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 390:
#line 1367 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 391:
#line 1371 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 394:
#line 1383 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 395:
#line 1387 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 396:
#line 1391 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 397:
#line 1395 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 398:
#line 1399 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
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
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 401:
#line 1411 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 402:
#line 1415 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 403:
#line 1419 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 404:
#line 1423 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 405:
#line 1427 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 406:
#line 1431 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 407:
#line 1435 "Iril/IR/IR.jay"
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
   55,   55,   55,   55,   55,   58,   20,   20,   20,   20,
   20,   20,   20,   20,   20,   59,   27,   27,   60,   57,
   57,   25,   61,   61,   56,   56,   62,   63,   63,   38,
   38,   64,   64,   65,   65,   65,   65,   66,   66,   68,
   68,   68,   68,   70,   71,   71,   72,   72,   73,   73,
   73,   73,   73,   73,   74,   74,   74,   74,   74,   74,
   74,   74,   21,   21,   75,   75,   75,   75,   75,   76,
   76,   77,   78,   78,   79,   80,   80,   81,   81,   45,
   82,   83,   67,   67,   84,   84,   84,   84,   84,   84,
   84,   85,   85,   85,   85,   86,   86,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,   69,   69,   69,
   69,   69,   69,   69,   69,   69,   69,
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
    3,    3,    3,    5,    6,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    2,    2,    1,    2,    1,
    3,    2,    1,    2,    1,    3,    1,    1,    3,    1,
    2,    3,    1,    2,    3,    1,    2,    1,    2,    1,
    2,    3,    4,    1,    3,    2,    1,    3,    2,    3,
    3,    2,    4,    5,    1,    1,    1,    1,    6,    9,
    6,    6,    1,    3,    1,    1,    2,    2,    2,    1,
    3,    5,    1,    2,    3,    1,    2,    1,    1,    1,
    1,    5,    1,    3,    2,    7,    2,    2,    7,    1,
    1,    8,    9,    9,   10,    0,    1,    5,    6,   11,
    5,    7,    5,    5,    6,    4,    4,    5,    5,    6,
    6,    7,    5,    5,    6,    6,    7,    6,    7,    5,
    6,    7,    7,    8,    6,    4,    4,    6,    7,    6,
    2,    6,    4,    4,    4,    4,    6,    6,    7,    6,
    6,    6,    4,    3,    4,    7,    8,    8,    9,   10,
    5,    6,    5,    5,    6,    3,    4,    5,    6,    8,
    4,    5,    6,    6,    4,    5,    7,    8,    5,    6,
   11,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,    0,   98,  109,  101,  102,  103,  104,  100,  137,
   41,   39,   42,   43,   44,   45,   46,   47,  310,  174,
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
    0,    0,    0,    0,    0,    0,    0,    0,  239,  240,
  238,  241,  242,  243,  237,  221,  225,  245,  244,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  224,  222,
  223,    0,    0,    0,    0,  187,    0,    0,    0,    0,
   50,    0,    0,    0,   76,   75,   13,    0,    0,   69,
   74,  182,  178,  181,    0,    0,    0,    0,    0,    0,
  106,    0,    0,    0,   90,   89,   81,   79,   80,   82,
   83,   84,   85,    0,   77,  160,    0,  155,    0,    0,
    0,    0,    0,    0,    0,  129,  193,    0,    0,    0,
    0,  148,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  250,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   15,    0,    0,
    0,   70,   14,    0,  247,  110,   94,  111,  114,  108,
  116,    0,    0,   12,   78,  162,  158,    0,    0,  124,
    0,    0,    0,    0,    0,    0,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  260,  263,    0,    0,
  268,    0,  313,  321,    0,  131,  144,    0,  149,    0,
    0,  150,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  233,    0,    0,    0,  231,  232,    0,    0,
    0,    0,    0,   63,   66,    0,   61,    0,   52,   64,
    0,   58,   60,   65,   62,   53,   54,   51,   17,   16,
   73,   72,   71,   86,  296,    0,  295,    0,  293,  126,
    0,    0,    0,  318,    0,    0,  315,    0,    0,    0,
    0,  317,  308,  309,    0,    0,  306,  327,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  311,  361,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  195,
  196,  197,  198,  199,  200,  201,  202,  203,  204,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  119,  261,
    0,  269,    0,    0,    0,  130,  151,   35,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  251,    0,
    0,    0,    0,    0,    0,    0,  252,    0,  299,  297,
  298,   91,    0,  125,  262,    0,  314,  246,    0,    0,
  274,    0,    0,    0,    0,    0,    0,  307,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  205,
  206,  217,  218,  219,  220,  208,  210,  211,  212,  213,
  209,  207,  215,  216,  214,    0,    0,    0,  300,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  374,    0,    0,  120,   20,    0,   30,    0,    0,    0,
    0,    0,    0,    0,    0,  234,    0,    0,    0,    0,
    0,   56,    0,   59,  294,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  356,    0,    0,
  257,  258,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  375,    0,    0,    0,
    0,    0,  230,  226,  229,  235,    0,   25,    0,    0,
   55,    0,    0,    0,  276,    0,    0,  277,    0,    0,
    0,    0,  328,    0,    0,  399,    0,    0,  384,    0,
    0,  403,    0,  388,    0,  405,  396,  392,    0,    0,
  381,    0,  334,  333,  383,  406,    0,    0,    0,    0,
  331,    0,    0,    0,    0,  236,  249,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  301,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  253,    0,  255,    0,    0,
    0,    0,  303,    0,    0,  279,    0,  275,    0,    0,
    0,    0,    0,  329,  358,  400,  367,  385,  362,  389,
  360,  393,  382,  335,  371,  394,  259,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  370,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  254,  228,    0,  316,    0,
  319,  304,    0,  286,  287,  288,    0,    0,    0,    0,
  285,  281,  280,  278,    0,    0,    0,    0,  332,    0,
    0,    0,    0,  376,    0,  397,    0,    0,    0,  359,
  302,    0,  312,    0,    0,    0,    0,    0,   22,  227,
  256,  305,  283,    0,    0,    0,    0,    0,  322,    0,
    0,    0,  378,    0,    0,  377,  398,    0,    0,  390,
    0,  284,    0,    0,    0,    0,  323,  324,    0,    0,
  379,    0,    0,    0,    0,    0,    0,  325,  380,    0,
    0,    0,    0,    0,    0,  330,  401,    0,  292,  289,
  291,    0,    0,  290,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   60,   12,   13,   14,  254,  228,  220,   61,
   88,  229,  571,   89,  270,   69,   91,  221,  419,  209,
  438,  421,  422,  423,  424,  230,  856,  255,  105,  106,
  164,  113,   93,  165,   15,  124,  178,  376,  271,  266,
   75,   65,   76,   66,   67,   16,  272,  174,  175,   94,
  180,  520,  646,  210,  211,  857,  286,  827,  447,  731,
  858,  722,  723,  377,  378,  379,  380,  381,  382,  572,
  690,  787,  788,  932,  439,  648,  649,  862,  863,  456,
  457,  493,  653,  383,  384,  459,
  };
  protected static readonly short [] yySindex = {          586,
   28,  -50,   40,   46,   57, 3002, -150, -280,    0,  586,
    0,    0,    0,    0, -129, 3649, -124,   93,   99, 1783,
  -79,   -5,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  126,    0,  168,    0,    0,    0,    0,    0,
  173,    0,    0,  -25,    0,    0, 4137,  -85,    6,    0,
  -35, -108,  209, 5184, 3364,    0,    0,    5,   13,  222,
    0,  248, 3720,  -14,    0, 3720,    0,   34,   52,    0,
    0,    0,    0,    0,    0,  273,  242, 5184,  -40,  -53,
 -199,   24,    0, -108,    9,  301,    4,  219,   95, 5184,
 5184,    0,    0, -108,   36,  209,   80, 5184,  113,   84,
    0,  348,  353, 3860,  209,    0, 5184,  209, 3720,    0,
  125,  280, 1430, -205,    8, 3720,  248,   10,    0,    0,
    0,  144, 5184,  -53,  -53, 3992, 5184,  -53, 5184,  -53,
    0,   89,    0,  290,    0, -197,  375,  300, 4383,  383,
  -34,  -30,    0, 5184, 5184,   55, 5184,    0,  182,   85,
    0,    0, -108,  101,    0,  209,  209,    0,  804,    0,
    0,    0, 5078,  216,    0,    0,  106,  -62, -136,    0,
  248,   12, -205,  248,  421,  902, 5184, 5184,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -24,
  424,  426,  429,  432, 5192, 5209, 5192,  430,    0,    0,
    0, 3992, 5184, 3992, 5184,    0,  417,  418,  419,  264,
    0, -197, 4647,    0,    0,    0,    0,  -15, 3992,    0,
    0,    0,    0,    0, -108,  -39,  423,  -68,  438,  229,
    0, 4257,  433,  447,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  176,    0,    0, 4484,    0, 4241, -134,
 5190,  -60,  231, 5192,  236,    0,    0, -205,  248,  106,
  106,    0, -205,    0,  118,  455, -108, 1567,  461, 5184,
 5192, 5192, 5192, 5192,    0,   78, 4951,   21,   82,  133,
  472, 3992,  474, 3992, 5003, 5063,  694,    0, -197,  270,
  -13,    0,    0, 4911,    0,    0,    0,    0,    0,    0,
    0,  266, 5096,    0,    0,    0,    0,  268,  265,    0,
  471,  483, 5192,  -36, 5192, 3443, 5192,    0, 3340,  115,
 3340,  115, 3340,  115, 5184,  564,  115, 5184, 5184, 3340,
 3060, 3156, 5184, 5184, 5184, 5192, 5192, 5192, 5192, 5192,
 5184,  -45, 3786,  213, -160,  537, 5192, 5192, 5192, 5192,
 5192, 5192, 5192, 5192, 5192, 5192, 5192, 5192, 1406,  115,
 5184,  115, 3443,  124, 5184, 4094,    0,    0, 7862, -150,
    0, -150,    0,    0, 5190,    0,    0,  235,    0, -205,
  106,    0,  292, -224,  174,  512, 5184,  -27,  172,  177,
  178,  179,    0, 5192, 3992,   90,    0,    0,  305,  190,
  527,  199,  529,    0,    0,  542,    0,  779,    0,    0,
  456,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, -173,    0,  277,    0,    0,
  235, 7862, 8357,    0,  310, 4032,    0,  554, 3931, 3720,
 3720,    0,    0,    0, 3992, 3340,    0,    0, 5184, 3992,
 3340, 5184, 3992, 3340, 5184, 3992, 5184, 3992, 5184, 3992,
 3992, 3992, 3340, 5184, 3992, 5184, 3992, 3992, 3992, 3992,
  556,  561,  563,  565,  566,  -17, 5184, 4056,  -12, 5192,
  579,    0,    0, 5184, 5184, 5184,  -11,  195,  237,  244,
  245,  247,  249,  250,  254,  257,  258,  259,  260,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 5184,
 2026,  -67, 5184,  995, 5184, 3720, 3279, -255,    0,    0,
 -150,    0,   13,   13, 4218,    0,    0,    0,  371,  380,
  395, -216,   -9, 5192, 5184, 5184, 5184, 5184,    0,  594,
 -150,  400,  279,  401,  281, 4120,    0, 5063,    0,    0,
    0,    0, 5096,    0,    0, -150,    0,    0,  624,  403,
    0,  630, 3931, 3931, 3720,  627, 3992,    0, 3992,  628,
 3992, 3992,  629, 3992, 3992,  632, 3992,  635, 3992,  638,
  640,  641, 3992, 3992,  644, 3992,  647,  656,  657,  658,
 5192, 5192, 5192,  694, 5192, 1005,   -8, 5184,   -4, 5184,
  659, 5184, 3992, 3992,   -2, 5192, 5184, 5184, 5184, 5184,
 5184, 5184, 5184, 5184, 5184, 5184, 5184, 5184, 3992,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 5184, 4032,  660,    0, 3992,
  283,  630,  630, 3931, 3931, 5184, 5184,  995, 5184, 3720,
    0, 5192,   13,    0,    0, -150,    0,  445,  449, 5192,
  663,  -29,  -28,  -23,  -22,    0,   13, -150,  451, -150,
  454,    0,  278,    0,    0,   13,  403,  622, 3956,  325,
  630,  630, 3931, 4032,  671,  672, 4032,  673,  675, 4032,
  680,  681, 4032,  692, 4032,  695, 4032, 4032, 4032,  696,
  697, 4032,  698, 4032, 4032, 4032, 4032,    0,  699,  701,
    0,    0,  702,  703,  481,  706, 5184,    1, 5184, 3992,
  707, 5184,  712,  713,  714, 5192,  715, -108, -108, -108,
 -108, -108, -108, -108, -108, -108, -108, -108, -108,  717,
 3992,  718,  674,  720,  506,  106,  106,  630,  630, 3931,
 3931,  630,  630, 3931, 3931, 3720,    0,   13,  724, -150,
  727,  413,    0,    0,    0,    0,   13,    0,   13, -150,
    0,  729, 5184, 5120,    0, 3096,  287,    0,  403,  385,
  394,  630,    0, 4032, 4032,    0, 4032, 4032,    0, 4032,
 4032,    0, 4032,    0, 4032,    0,    0,    0, 4032, 4032,
    0, 4032,    0,    0,    0,    0, 5192, 5192,  694,  694,
    0,  404,  741, 5184,  746,    0,    0,  411,  750,  428,
 5184, 5184,  754,  413, 4032,  759, 4032,    0, 5192,  765,
  106,  106,  106,  106,  630,  630,  106,  106,  630,  630,
 3931,  431,   13,  413, 5192,    0,  307,    0,   13,  403,
  768, 5158,    0,  773, 4414,    0, 3237,    0, 5150,  486,
  403,  403,  427,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  702,  558,  437,
  -42,  444,  570,  452,  578, 3992, 3992,  413,  792,    0,
 4032,  748,  795,  580,  106,  106,  106,  106,  106,  106,
  106,  106,  630,  585,  376,    0,    0,  413,    0,  403,
    0,    0, 4944,    0,    0,    0,  464,  811,  812,  813,
    0,    0,    0,    0,  403,  532,  534,  403,    0,  600,
  821,  489,  609,    0,  616,    0,  544,  548,  792,    0,
    0, 5192,    0,  106,  106,  106,  106,  106,    0,    0,
    0,    0,    0,  388,  847, 5192, 5192, 5192,    0,  403,
  403,  567,    0,  511,  639,    0,    0,  848,  853,    0,
  106,    0, 5184,  510,  513,  520,    0,    0,  403,  653,
    0,  521,  536,    3, 5184, 5184, 5184,    0,    0,  655,
  662, 5192,  -21,  -20,  -19,    0,    0,  876,    0,    0,
    0,  413,  396,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0, 3791,    0,    0,  921,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  606,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  621,    0,    0,    0,    0,    0,
 1287,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3517,  747,  651,    0,    0,    0,    0,    0, 3855,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  606,    0,  606,    0,
  606,    0,    0, 1775,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  123,    0,    0,    0,    0,    0, 3585,
    0,    0,    0,    0,  654,    0,    0,  661,    0,    0,
    0,    0,    0,  606,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  284,    0,    0,    0,    0,
    0,  480,    0,    0,    0,    0,    0,    0,    0,    0,
  284,  284,    0,    0,    0,    0,    0,    0,    0, 1108,
    0,    0,  262,    0,    0,  664,  665,    0,    0,    0,
    0,    0,  370,    0,    0,    0,  -57,    0,  -52,    0,
    0,    0,  599,    0,    0,  129,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  284,    0,  284,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 1395,    0,    0,    0,    0,  284,    0,
    0,    0,    0,    0,  152,  284,    0,  284,    0,    0,
    0,    0, 1885, 2246,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  397,    0,    0,  -51,
    0,    0,    0,    0,    0,    0,    0,  606,    0,  788,
  975,    0,  606, 2136,    0, 1069,  153,  284,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  284,    0,  284,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 5239,
    0, 5239,    0, 5239,    0,    0, 5239,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2387,
    0, 5239,    0,    0,    0,    0,    0,    0,    0, 4358,
    0, 7966,    0,    0,    0,    0,    0,  -49,    0,  606,
 1310,    0,    0,    0,    0,    0,    0,  284,    0,    0,
    0,    0,    0,    0,  123,    0,    0,    0,    0,    0,
    0,    0, 1348,    0,    0,   94,    0,  284,    0,    0,
  405,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  284,    0,    0,    0,    0,
  -48,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  284,    0,    0,    0,    0,  284,
    0,    0,  284,    0,    0,  284,    0,  284,    0,  284,
  284,  284,    0,    0,  284,    0,  284,  284,  284,  284,
    0,    0,    0,    0,    0,  284,    0,    0,  284,    0,
    0,    0,    0,    0,    0,    0,  284,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  284,    0,    0,    0,    0,    0,  284,    0,    0,
 4482,    0, 4622, 8070,    0,    0,    0,    0,    0,    0,
    0,    0,  284,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 8174,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  284,    0,  284,    0,
  284,  284,    0,  284,  284,    0,  284,    0,  284,    0,
    0,    0,  284,  284,    0,  284,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  284,    0,  284,    0,
    0,    0,  284,  284,  284,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  284,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5297,    0,  284,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4746,    0,    0, 1361,    0,    0,    0,    0,
    0,  284,  284,  284,  284,    0, 1372,    0,    0,    0,
    0,    0,    0,    0,    0, 8278,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 5404,    0,    0,    0,    0,  284,    0,  284,
    0,    0,    0,    0,    0,    0,    0, 1521, 1630, 1756,
 1882, 1991, 2117, 2243, 2352, 2478, 2604, 2713, 2839,    0,
  284,    0,    0,    0,    0, 5511,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1665, 1901, 1920,
    0,    0,    0,    0,    0,    0, 2262,    0, 2281, 2497,
    0,    0,    0,    0,    0,  284,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5618, 5725, 5832, 5939,    0,    0, 6046,    0,    0,    0,
    0,    0, 2623,    0,    0,    0,    0,    0, 2642,    0,
    0,    0,    0,  416,  284,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 6153,    0,    0,
    0,    0,    0,    0,    0,  284,  284,    0, 6260,    0,
    0,    0,    0,    0, 6367, 6474, 6581,    0, 6688, 6795,
 6902, 7009,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 7116,    0,
    0,    0,    0, 7223, 7330, 7437, 7544, 7651,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 7758,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  284,    0,    0,    0,    0,    0,    0,
    0,    0,  284,  284,  284,    0,    0,    0,    0,    0,
    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  915,  835,    0,    0,    0,    0,  711,  721,  925,
  540,   -6,  508,   50,  142, -135,    0,  648,  652, -156,
 -555,    0,  391,    0, -715,    0,  228,  704,  842,   33,
    0,  110,    0,  719,    0,  -93,    0,  575, -115, -230,
   -3,    0,  -59,  904,  -56,    0, -192,    0,  708,    0,
 -175,    0,    0,    0,    0, -777,   -1,    0, -562, -559,
   48,  150,  162, -335,  528,    0,  593,  605,  549, -469,
 3094,    0,  127,    0,  434,    0,  241,    0,  136,   54,
 -188,    0,  337,    0,  559,  193,
  };
  protected static readonly short [] yyTable = {            62,
  683,  942,   64,  267,  111,  119,  233,  688,  179,   62,
  234,  773,  774,  126,   58,  280,  544,  775,  776, 1009,
 1010, 1011,  306,  647,  308,  114,  606,   98,  304,  320,
  304,  610,  616,  183,  670,  727,  148,  108,   63,  729,
  530,  736,  262,   70,  824,   59, 1002,  114,   74,  114,
  104,  114,  733,  158,  652,  539,  899,   62,   62,  661,
  261,  260,  385,  668,  404,  121,   62,  861,  144,   62,
  127,  122,  129,  128,  123,  389,  915,   57,  217,  154,
  392,  136,  407,  218,  267,   80,   81,  268,   17,  559,
  273,   80,   81,  151,  152,  267,  115,  118,  154,  560,
   20,  104,  561,  691,  692,  125,   21,  163,  128,  303,
   62,  430,   62,  257,  117,  129,  173,   22,  176,   62,
  949,  404,  129,   68,  782,  404,  186,  112,   36,   92,
  212,   57,  214,  404,   62,   77,  134,   62,  420,  420,
  427,  241,   72,  263,  242,  318,  861,  235,  236,  166,
  238,  167,   19,   78,  391,  540,  437,  536,  182,   79,
  153,   90,   97,  669,  113,   99,   95,  823,   97,  825,
  113,  107,  829,  662,  408,  390,  264,   86,  264,  237,
  277,  278,  219,   86,  758,  759,  494,  176,  762,  176,
  495,   97,   97,  113,  113,   96,   92,  537,   92,  530,
   92,   95,  403,  142,  288,  289,  292,  100,  294,  112,
  564,   68,  101,  121,  550,  267,  121,   62,   23,   18,
   19,  127,  122,  792,  128,  123,  870,   24,  135,  445,
  138,  102,  140,   92, 1013,  163,   25,   26,   27,   28,
   29,   82,   83,  265,  533,  319,  534,   95,  114,  446,
   80,   81,  173,  129,   82,   83,  258,  127,  302,  259,
  302,  557,  109,  487,  891,  177,  450,  578,   96,  451,
   39,   97,  578,  398,  112,  578,   96,  147,  120,  181,
  405,  184,  122,  269,  578,  406,  121,  123,  418,  418,
  845,  846,   92,  130,  849,  850,  143,  919,   52,   53,
  314,   97,  117,  113,  298,  117,  436,  299,  936,  937,
  429,  131,  132,  299,  112,  112,  526,  562,  781,   62,
  563,  563,  455,   97,  460,  113,  463,  868,  466,  468,
  869,  470,  471,  472,  475,  477,  478,  479,  480,  141,
  146,  149,   86,  112,  486,  489,  155,  917,  112,  497,
  918,  150,  112,  112,  112,  112,  279,  962,  449,  112,
  112,  112,  112,  112,  522,  112,   62,  964,  528,  525,
  112,  112,  969,  112,  112,  972,  231,   92,  112,  157,
  112,  913,   92,  112,  461,  112,  464,  159,  488,   39,
  543,  575,  941,  473,  160,  663,   36,   36,  168,  437,
  185,  420,  169,   36,  216,  524,  437,  987,  988,   97,
  159,  113,  108,  159,  222,  677,  960,   36,   36,  918,
   19,   19,  223,  232,   19,   19,  998,   19,  982,  176,
  686,  563,  285,  285,  285,  243, 1014,  161,  239,  918,
  161,   19,   19,   62,   62,   57,  129,  721,   57,  577,
  231,  244,  579,   36,  581,  582,  282,  584,  585,  282,
  587,  274,  589,  281,  240,  282,  593,  594,  283,  596,
  660,  284,   58,  290,  295,  296,  297,   19,  309,  186,
  607,  609,  573,  574,  307,  310,  313,  613,  614,  615,
  386,  387,  388,  312,  245,  246,  247,  393,  394,   92,
  397,  248,  249,   59,  250,  251,  252,  253,  399,  400,
  401,  402,  409,  629,  285,  410,  650,  412,   62,   62,
   62,  441,  656,  659,  462,  434,  465,  440,  442,  469,
  768,  433,   80,   81,  458,   57,   82,   83,  672,  673,
  674,  675,  777,  443,  779,  492,  527,  264,  538,  436,
  444,  418,  448,  541,  452,  542,  436,  654,  655,  658,
  545,  551,  521,   85,  523,  546,  547,  548,   62,  552,
  553,  129,  555,  481,  482,  483,  484,  485,  554,  558,
  491,  556,  568,  617,  498,  499,  500,  501,  502,  503,
  504,  505,  506,  507,  508,  509,   58,  570,  147,  601,
  766,  728,  186,  730,  602,  730,  603,  693,  604,  605,
  738,  739,  740,  741,  742,  743,  744,  745,  746,  747,
  748,  749,  612,   58,   86,  618,  133,   59,  137,  139,
  665,  549,  619,  620,  853,  621,  666,  622,  623,  751,
  841,  842,  624,  208,  859,  625,  626,  627,  628,   62,
   62,  667,   62,   62,   59,  676,  678,  680,  679,   57,
  681,  177,  721,  721,  177,  267,  267,  687,  445,  689,
  694,  697,  700,  187,  188,  703,  224,  213,  705,  215,
  177,  707,  786,  708,  709,   24,   57,  712,  760,  761,
  714,  764,  765,  276,   25,   26,   27,   28,   29,  715,
  716,  717,  732,  753,  769,  770,  772,  778,  931,  755,
  780,  177,  783,  789,  794,  795,  797,  611,  798,  291,
  730,  293,  730,  800,  801,  730,  867,  905,  906,  267,
  267,  909,  910,  267,  267,  803,  305,  821,  805,  809,
  810,  812,  817,  177,  818,  819,  820,  186,  186,  822,
  828,  186,  186,  186,  186,  830,  831,  832,  834,   62,
  835,  837,  129,  839,  647,  840,  437,  852,  186,  186,
  854,  671,  860,  871,  186,  186,  418,  865,  267,  267,
  267,  267,  872,  889,  890,  396,   97,  152,  113,  892,
  893,  954,  955,  894,  855,  956,  957,  898,  851,  411,
   23,  413,  901,  186,  186,  267,   97,  895,  904,   24,
  914,  920,  923,  935,  939,  938,  940,  730,   25,   26,
   27,   28,   29,  943,  896,  897,  944,   23,  718,  719,
  720,  945,  724,  726,  946,  918,   24,   97,  952,  953,
  951,  959,  981,  737,  965,   25,   26,   27,   28,   29,
  966,  967,  968,    1,    2,  418,  973,    3,    4,  970,
    5,  971,  786,  129,  974,  976,  147,  147,  975,   97,
  147,  147,  977,  147,    6,    7,  978,  177,  177,  177,
  979,  177,  177,  177,  177,  177,  983,  147,  147,  767,
  990,  992,  177,  177,  989,  991,  993,  771,  995,  177,
 1000,  996,  177,  177,  177,  177,  177,  177,  997,  999,
    8, 1006,  305,  177,  113, 1001,  436,  496, 1007, 1012,
    1,  113,  132,  147,   71,  133,  467,  177,  177,  145,
  177,  177,  134,  301,  177,  135,  136,  177,  177,  177,
  177,  177,  300,  177,   87,  275,  428,  426,  684,  156,
  189,  190,  191,  569,  192,  193,  194,  315,  195,  535,
  311,  206,  576,  833,  110,  961,  317,  580,  887,  565,
  583,  531,  198,  586,  154,  588,  994,  590,  591,  592,
  199,  888,  595,  532,  597,  598,  599,  600, 1003, 1004,
 1005,  566,  207,  838,  763,  934,  685,  922,    0,  177,
  177,  567,    0,   97,   97,   97,    0,   97,   97,   97,
    0,   97,    0,    0,  177,  177,  177,  177,   97,   97,
    0,    0,    0,    0,  205,   97,    0,    0,    0,    0,
    0,    0,    0,   97,  114,  189,  190,  191,    0,  192,
  193,  194,    0,  195,  885,  886,    0,    0,    0,    0,
    0,    0,  177,  177,  206,  152,  152,  198,    0,  152,
  152,    0,  152,  243,   58,  199,  903,    0,   18,    0,
    0,    0,    0,    0,    0,    0,  152,  152,    0,  244,
    0,    0,  916,    0,  695,  207,  696,    0,  698,  699,
    0,  701,  702,    0,  704,   59,  706,    0,    0,    0,
  710,  711,    0,  713,    0,    0,    0,  107,    0,    0,
    0,    0,  152,    0,    0,    0,    0,  205,    0,    0,
  734,  735,  245,  246,  247,   97,    0,   57,    0,  248,
  249,    0,  250,  251,  252,  253,  750,    0,    0,    0,
   97,   97,   97,   97,    0,    0,    0,  107,  107,  107,
    0,  107,    0,    0,  752,    0,    0,  754,  189,  190,
  191,  112,  192,  193,  194,    0,  195,  107,    0,  107,
    0,    0,   97,  196,  197,    0,    0,    0,    0,  980,
  198,    0,    0,    0,    0,    0,    0,    0,  199,    0,
    0,    0,    0,  984,  985,  986,    0,    0,  107,    0,
  107,  793,    0,    0,  796,    0,    0,  799,    0,    0,
  802,    0,  804,    0,  806,  807,  808,    0,    0,  811,
    0,  813,  814,  815,  816,    0,    0,    0,    0, 1008,
  107,    0,  107,    0,    0,    0,    0,  826,    0,    0,
    0,    0,  154,  154,    0,    0,  154,  154,    0,  154,
    0,  189,  190,  191,    0,  192,  193,  194,  836,  195,
    0,    0,    0,  154,  154,    0,  196,  197,  224,    0,
    0,    0,    0,  198,    0,    0,    0,   24,    0,    0,
  200,  199,    0,    0,  112,    0,   25,   26,   27,   28,
   29,    0,    0,  866,    0,  201,  202,  203,  204,  154,
    0,  874,  875,    0,  876,  877,    0,  878,  879,  153,
  880,    0,  881,    0,    0,    0,  882,  883,    0,  884,
    0,    0,    0,    0,    0,    0,    0,  180,    0,    0,
  180,    0,    0,    0,    0,    0,   18,   18,    0,    0,
   18,   18,  900,   18,  902,    0,  180,   28,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   18,   18,    0,
   33,    0,    0,    0,  107,  107,  107,    0,  107,  107,
  107,   23,  107,  200,  933,  107,  107,  180,    0,  107,
  107,  107,  107,  107,  725,    0,  107,    0,  201,  202,
  203,  204,    0,   18,  107,    0,  107,  107,    0,    0,
  107,    0,    0,  947,  948,    0,    0,    0,  950,  180,
    0,    0,    0,    0,  107,  107,    0,  107,  107,    0,
  651,  107,  107,    0,  107,  107,  107,  107,  107,    0,
  107,    0,  107,    0,   98,  248,    0,    0,  248,    0,
    0,    0,    0,  107,  107,  107,    0,  107,  107,    0,
    0,    0,  107,    0,  107,    0,  248,  107,  107,  107,
  107,  107,  107,  107,  107,  107,  107,    0,  107,  107,
  172,  107,  107,  107,  107,  107,  107,  107,  107,  107,
  107,  107,  107,  107,    0,  107,  107,  248,    0,   58,
  107,  107,  107,  107,  107,  107,    0,  107,  107,  107,
  107,  107,  107,  107,  107,  107,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  107,  248,    0,  248,
   59,    0,    0,    0,    0,    0,    0,    0,  107,  107,
  107,  107,    0,  107,    0,  107,  107,    0,    0,  107,
  107,    0,    0,  180,  180,  180,    0,  180,  180,  180,
  180,  180,   57,    0,    0,    0,    0,    0,  180,  180,
   97,    0,  113,    0,    0,  180,    0,    0,  180,  180,
  180,  180,  180,  180,    0,    0,    0,  153,  153,  180,
    0,  153,  153,    0,  153,    0,    0,    0,    0,    0,
    0,    0,    0,  180,  180,    0,  180,  180,  153,  153,
  180,    0,    0,  180,  180,  180,  180,  180,    0,  180,
  395,    0,    0,    0,    0,   28,   28,    0,    0,   28,
   28,    0,   28,    0,    0,    0,  206,    0,   33,   33,
    0,    0,   33,   33,  153,   33,   28,   28,    0,   23,
   23,    0,    0,   23,   23,    0,   23,    0,    0,   33,
   33,  248,    0,    0,    0,    0,    0,  207,    0,    0,
   23,   23,    0,    0,   34,  180,  180,  248,  248,   97,
    0,  113,   28,    0,    0,    0,    0,    0,    0,    0,
  180,  180,  180,  180,    0,   33,    0,    0,    0,  205,
    0,    0,    0,   23,    0,    0,   23,    0,    0,    0,
    0,    0,   24,    0,    0,    0,    0,  170,    0,    0,
    0,   25,   26,   27,   28,   29,    0,    0,  180,  180,
    0,    0,    0,  171,    0,    0,    0,    0,    0,    0,
  248,  248,  248,    0,  248,  248,    0,    0,    0,  248,
    0,  248,    0,    0,  248,  248,  248,  248,  248,  248,
  248,  248,  248,  248,    0,  248,  248,    0,  248,  248,
  248,  248,  248,  248,  248,  248,  248,  248,  248,  248,
  248,    0,  248,  248,  113,    0,    0,    0,  248,  248,
  248,  248,  248,  248,  248,  248,  248,  248,  248,  248,
  248,    0,  248,  402,  402,   97,    0,  113,    0,    0,
    0,    0,    0,  248,  510,  511,  512,  513,  514,  515,
  516,  517,  518,  519,    0,  248,  248,  248,  248,    0,
    0,    0,  248,  189,  190,  191,    0,  192,  193,  194,
    0,  195,    0,    0,    0,    0,    0,    0,  196,  197,
    0,    0,    0,    0,    0,  198,    0,    0,    0,    0,
    0,    0,    0,  199,    0,    0,  402,  402,  402,    0,
  402,  402,    0,    0,    0,  402,    0,  402,    0,    0,
  402,  402,  402,  402,  402,  402,  402,  402,  402,  402,
    0,  402,  402,    0,  402,  402,  402,  402,  402,  402,
  402,  402,  402,  402,  402,  402,  402,  113,  402,  402,
   21,    0,  407,  407,  402,  402,  402,  402,  402,    0,
  402,  402,  402,  402,  402,  402,  402,    0,  402,   31,
    0,   97,    0,  113,    0,    0,    0,    0,    0,  402,
    0,    0,   34,   34,    0,    0,   34,   34,    0,   34,
    0,  402,  402,  402,  402,  200,    0,    0,  402,  112,
    0,    0,    0,   34,   34,    0,    0,    0,    0,    0,
  201,  202,  203,  204,    0,  407,  407,  407,    0,  407,
  407,    0,    0,    0,  407,    0,  407,    0,    0,  407,
  407,  407,  407,  407,  407,  407,  407,  407,  407,   34,
  407,  407,    0,  407,  407,  407,  407,  407,  407,  407,
  407,  407,  407,  407,  407,  407,    0,  407,  407,   87,
    0,    0,    0,  407,  407,  407,  407,  407,    0,  407,
  407,  407,  407,  407,  407,  407,    0,  407,  391,  391,
   97,    0,  113,    0,    0,    0,    0,    0,  407,    0,
    0,    0,  113,  113,    0,    0,  113,  113,  113,  113,
  407,  407,  407,  407,    0,    0,    0,  407,    0,    0,
    0,    0,    0,  113,  113,    0,    0,    0,    0,  113,
  113,    0,    0,   80,   81,    0,    0,   82,   83,   84,
   31,   32,   33,   34,   35,   36,   37,   38,    0,    0,
  113,  391,  391,  391,   44,  391,  391,    0,  113,  113,
  391,    0,  391,    0,   85,  391,  391,  391,  391,  391,
  391,  391,  391,  391,  391,    0,  391,  391,    0,  391,
  391,  391,  391,  391,  391,  391,  391,  391,  391,  391,
  391,  391,    0,  391,  391,  112,    0,    0,    0,  391,
  391,  391,  391,  391,   87,  391,  391,  391,  391,  391,
  391,  391,    0,  391,  366,  366,   97,    0,  113,    0,
   87,    0,    0,    0,  391,   86,    0,    0,   21,   21,
    0,    0,   21,   21,    0,   21,  391,  391,  391,  391,
    0,    0,    0,  391,    0,    0,    0,   31,   31,   21,
   21,   31,   31,    0,   31,    0,    0,    0,    0,    0,
    0,    0,    0,   87,   87,   87,    0,    0,   31,   31,
   87,   87,    0,   87,   87,   87,   87,  366,  366,  366,
    0,  366,  366,    0,    0,   21,  366,    0,  366,    0,
    0,  366,  366,  366,  366,  366,  366,  366,  366,  366,
  366,    0,  366,  366,   31,  366,  366,  366,  366,  366,
  366,  366,  366,  366,  366,  366,  366,  366,  112,  366,
  366,   26,    0,  363,  363,  366,  366,  366,  366,  366,
    0,  366,  366,  366,  366,  366,  366,  366,    0,  366,
   24,    0,   97,    0,  113,    0,  630,  631,    0,    0,
  366,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  366,  366,  366,  366,    0,    0,    0,  366,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  363,  363,  363,    0,
  363,  363,    0,    0,    0,  363,    0,  363,    0,    0,
  363,  363,  363,  363,  363,  363,  363,  363,  363,  363,
    0,  363,  363,    0,  363,  363,  363,  363,  363,  363,
  363,  363,  363,  363,  363,  363,  363,    0,  363,  363,
   88,    0,    0,    0,  363,  363,  363,  363,  363,    0,
  363,  363,  363,  363,  363,  363,  363,    0,  363,  364,
  364,   97,    0,  113,    0,    0,    0,    0,    0,  363,
    0,    0,    0,  112,  112,    0,    0,  112,  112,  112,
  112,  363,  363,  363,  363,    0,    0,    0,  363,    0,
    0,    0,    0,    0,  112,  112,  632,  633,  634,  635,
  112,  112,    0,    0,    0,  636,  637,  638,  639,  640,
  641,  642,  643,  644,  645,    0,    0,    0,    0,    0,
    0,  114,  364,  364,  364,    0,  364,  364,    0,  112,
  112,  364,    0,  364,    0,    0,  364,  364,  364,  364,
  364,  364,  364,  364,  364,  364,    0,  364,  364,    0,
  364,  364,  364,  364,  364,  364,  364,  364,  364,  364,
  364,  364,  364,    0,  364,  364,   27,    0,    0,    0,
  364,  364,  364,  364,  364,   88,  364,  364,  364,  364,
  364,  364,  364,    0,  364,  365,  365,   97,    0,  113,
    0,   88,    0,    0,    0,  364,    0,    0,    0,   26,
   26,    0,    0,   26,   26,    0,   26,  364,  364,  364,
  364,    0,    0,    0,  364,    0,    0,    0,   24,   24,
   26,   26,   24,   24,    0,   24,    0,    0,    0,    0,
    0,    0,    0,    0,   88,   88,   88,    0,    0,   24,
   24,   88,   88,    0,   88,   88,   88,   88,  365,  365,
  365,    0,  365,  365,    0,    0,   26,  365,    0,  365,
    0,    0,  365,  365,  365,  365,  365,  365,  365,  365,
  365,  365,    0,  365,  365,   24,  365,  365,  365,  365,
  365,  365,  365,  365,  365,  365,  365,  365,  365,    0,
  365,  365,   32,    0,  404,  404,  365,  365,  365,  365,
  365,    0,  365,  365,  365,  365,  365,  365,  365,    0,
  365,   29,    0,   97,    0,  113,    0,  326,  326,    0,
    0,  365,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  365,  365,  365,  365,    0,    0,    0,
  365,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  404,  404,  404,
    0,  404,  404,    0,    0,    0,  404,    0,  404,    0,
    0,  404,  404,  404,  404,  404,  404,  404,  404,  404,
  404,    0,  404,  404,    0,  404,  404,  404,  404,  404,
  404,  404,  404,  404,  404,  404,  404,  404,    0,  404,
  404,    0,    0,    0,    0,  404,  404,  404,  404,  404,
    0,  404,  404,  404,  404,  404,  404,  404,    0,  404,
  395,  395,   97,    0,  113,    0,    0,    0,    0,    0,
  404,    0,    0,    0,   27,   27,    0,    0,   27,   27,
    0,   27,  404,  404,  404,  404,    0,    0,    0,  404,
    0,    0,    0,    0,    0,   27,   27,  326,  326,  326,
  326,    0,    0,    0,    0,    0,  326,  326,  326,  326,
  326,  326,  326,  326,  326,  326,    0,    0,    0,    0,
    0,    0,    0,  395,  395,  395,    0,  395,  395,    0,
    0,   27,  395,    0,  395,    0,    0,  395,  395,  395,
  395,  395,  395,  395,  395,  395,  395,    0,  395,  395,
    0,  395,  395,  395,  395,  395,  395,  395,  395,  395,
  395,  395,  395,  395,    0,  395,  395,    0,    0,    0,
    0,  395,  395,  395,  395,  395,    0,  395,  395,  395,
  395,  395,  395,  395,    0,  395,  387,  387,   97,    0,
  113,    0,    0,    0,    0,    0,  395,    0,    0,    0,
   32,   32,    0,    0,   32,   32,    0,   32,  395,  395,
  395,  395,    0,    0,    0,  395,    0,    0,    0,   29,
   29,   32,   32,   29,   29,    0,   29,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   29,   29,    0,    0,    0,    0,    0,    0,    0,  387,
  387,  387,    0,  387,  387,    0,    0,   32,  387,    0,
  387,    0,    0,  387,  387,  387,  387,  387,  387,  387,
  387,  387,  387,    0,  387,  387,   29,  387,  387,  387,
  387,  387,  387,  387,  387,  387,  387,  387,  387,  387,
    0,  387,  387,    0,    0,  373,  373,  387,  387,  387,
  387,  387,    0,  387,  387,  387,  387,  387,  387,  387,
    0,  387,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  387,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  387,  387,  387,  387,    0,    0,
    0,  387,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  373,  373,
  373,    0,  373,  373,    0,    0,    0,  373,    0,  373,
    0,   58,  373,  373,  373,  373,  373,  373,  373,  373,
  373,  373,    0,  373,  373,    0,  373,  373,  373,  373,
  373,  373,  373,  373,  373,  373,  373,  373,  373,    0,
  373,  373,   59,    0,    0,    0,  373,  373,  373,  373,
  373,    0,  373,  373,  373,  373,  373,  373,  373,    0,
  373,  336,  336,    0,    0,    0,    0,    0,    0,   58,
    0,  373,    0,    0,   57,    0,    0,    0,    0,    0,
    0,    0,    0,  373,  373,  373,  373,    0,    0,    0,
  373,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   59,    0,    0,    0,    0,  206,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  336,  336,  336,    0,  336,  336,
    0,    0,   57,  336,    0,  336,  207,    0,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,    0,  336,
  336,    0,  336,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,  336,  336,   58,  336,  336,  205,    0,
    0,    0,  336,  336,  336,  336,  336,    0,  336,  336,
  336,  336,  336,  336,  336,    0,  336,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   59,  336,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  336,
  336,  336,  336,    0,    0,   23,  336,    0,    0,    0,
    0,    0,    0,    0,   24,    0,    0,    0,   57,    0,
    0,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,   30,    0,  206,    0,    0,   31,
   32,   33,   34,   35,   36,   37,   38,   39,   40,   41,
    0,   42,   43,   44,    0,   45,    0,    0,   46,   47,
   48,   49,   50,   23,   51,    0,    0,  207,    0,    0,
    0,    0,   24,    0,    0,   52,   53,    0,   58,    0,
    0,   25,   26,   27,   28,   29,    0,    0,    0,    0,
    0,    0,  189,  190,  191,    0,  192,  193,  194,  205,
  195,    0,    0,    0,    0,    0,    0,  196,  197,   59,
    0,    0,    0,    0,  198,    0,    0,    0,    0,    0,
    0,   54,  199,    0,    0,    0,    0,    0,  116,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   58,
    0,   57,   40,   41,    0,   42,   43,    0,    0,   45,
    0,    0,   46,   47,   48,   49,   50,    0,   51,   23,
    0,    0,  474,   58,    0,    0,    0,    0,   24,    0,
   59,    0,    0,   55,   56,    0,    0,   25,   26,   27,
   28,   29,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   59,    0,    0,    0,    0,    0,
    0,    0,   57,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  200,   54,    0,    0,  112,    0,
    0,    0,    0,    0,    0,    0,   57,    0,    0,  201,
  202,  203,  204,  189,  190,  191,    0,  192,  193,  194,
    0,  195,   58,    0,    0,    0,    0,    0,  196,  197,
    0,    0,    0,    0,    0,  198,    0,    0,  476,    0,
    0,    0,    0,  199,    0,    0,    0,   55,   56,  116,
    0,    0,    0,   59,    0,    0,    0,    0,    0,    0,
    0,    0,   23,   40,   41,    0,   42,   43,    0,    0,
   45,   24,    0,   46,   47,   48,   49,   50,    0,   51,
   25,   26,   27,   28,   29,   57,    0,    0,    0,    0,
    0,  116,    0,    0,    0,    0,  140,    0,    0,    0,
    0,    0,    0,    0,   39,   40,   41,    0,   42,   43,
    0,    0,   45,    0,    0,   46,   47,   48,   49,   50,
    0,   51,    0,   23,    0,    0,    0,  140,    0,    0,
    0,    0,   24,    0,    0,  200,   54,    0,    0,    0,
    0,   25,   26,   27,   28,   29,    0,   23,    0,    0,
  201,  202,  203,  204,    0,    0,   24,    0,    0,  140,
    0,    0,    0,    0,  141,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,  116,    0,   54,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   55,   56,
   40,   41,    0,   42,   43,  141,    0,   45,    0,    0,
   46,   47,   48,   49,   50,   85,   51,  453,  454,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  657,    0,
    0,    0,    0,    0,    0,    0,   23,  141,   58,    0,
   55,   56,    0,    0,    0,   24,    0,    0,    0,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,    0,    0,    0,    0,  116,    0,    0,    0,   59,
    0,    0,    0,   54,    0,  756,  757,    0,   39,   40,
   41,    0,   42,   43,    0,    0,   45,    0,    0,   46,
   47,   48,   49,   50,    0,   51,    0,    0,    0,    0,
    0,   57,    0,    0,    0,    0,    0,    0,    0,   58,
  140,    0,    0,    0,  790,  791,    0,    0,    0,  140,
    0,    0,    0,    0,    0,   55,   56,    0,  140,  140,
  140,  140,  140,    0,    0,    0,    0,    0,    0,  140,
   59,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   54,  140,  140,    0,  140,  140,    0,    0,
  140,    0,    0,  140,  140,  140,  140,  140,  140,  140,
    0,    0,   57,    0,    0,   58,    0,    0,  141,    0,
  145,  843,  844,    0,    0,  847,  848,  141,    0,    0,
    0,    0,    0,    0,    0,    0,  141,  141,  141,  141,
  141,    0,    0,    0,   55,   56,   59,  141,    0,    0,
    0,  145,    0,    0,    0,  873,    0,    0,    0,    0,
    0,  141,  141,    0,  141,  141,  140,    0,  141,    0,
  162,  141,  141,  141,  141,  141,  141,  141,   57,    0,
    0,    0,   23,  145,  146,    0,    0,    0,    0,   58,
    0,   24,    0,    0,    0,    0,    0,    0,    0,    0,
   25,   26,   27,   28,   29,    0,    0,    0,  907,  908,
    0,   73,  911,  912,    0,  146,    0,    0,  140,  140,
   59,    0,    0,    0,    0,   40,   41,    0,   42,   43,
    0,    0,   45,    0,  141,   46,   47,   48,   49,   50,
  114,   51,    0,    0,    0,    0,    0,  146,    0,    0,
    0,    0,   57,   23,    0,    0,    0,    0,    0,    0,
  206,    0,   24,    0,    0,    0,  785,    0,    0,    0,
    0,   25,   26,   27,   28,   29,  958,    0,    0,    0,
    0,    0,  116,    0,    0,   58,  141,  141,    0,    0,
    0,  207,    0,    0,    0,    0,   40,   41,   54,   42,
   43,    0,    0,   45,    0,    0,   46,   47,   48,   49,
   50,    0,   51,    0,    0,    0,   59,    0,    0,  224,
    0,  206,    0,  205,  145,    0,    0,    0,   24,    0,
    0,    0,    0,  145,    0,    0,    0,   25,   26,   27,
   28,   29,  145,  145,  145,  145,  145,    0,   57,    0,
   55,   56,  207,  145,    0,    0,    0,    0,    0,    0,
    0,  206,    0,    0,  490,    0,    0,  145,  145,   54,
  145,  145,    0,    0,  145,    0,    0,  145,  145,  145,
  145,  145,    0,  145,  205,   58,    0,    0,  146,    0,
    0,    0,  207,   23,    0,    0,    0,  146,    0,    0,
    0,    0,   24,    0,    0,    0,  146,  146,  146,  146,
  146,   25,   26,   27,   28,   29,   59,  146,    0,    0,
    0,   55,   56,  161,  205,    0,    0,    0,    0,    0,
  682,  146,  146,    0,  146,  146,    0,    0,  146,    0,
  145,  146,  146,  146,  146,  146,    0,  146,   57,   58,
    0,    0,    0,    0,    0,    0,    0,  189,  190,  191,
    0,  192,  193,  194,    0,  195,   58,    0,    0,    0,
    0,    0,  196,  197,    0,    0,    0,    0,    0,  198,
   59,    0,    0,    0,    0,    0,    0,  199,  529,   23,
    0,    0,  145,  145,    0,    0,    0,   59,   24,    0,
    0,    0,    0,  784,  146,    0,    0,   25,   26,   27,
   28,   29,   57,    0,    0,    0,    0,    0,  189,  190,
  191,    0,  192,  193,  194,    0,  195,    0,    0,   57,
    0,  103,    0,  196,  197,    0,    0,    0,    0,    0,
  198,    0,    0,    0,    0,    0,    0,    0,  199,    0,
    0,    0,    0,    0,    0,    0,  146,  146,  189,  190,
  191,    0,  192,  193,  194,    0,  195,    0,    0,    0,
   58,    0,    0,  196,  197,    0,    0,    0,    0,  200,
  198,    0,    0,    0,    0,    0,   58,    0,  199,   23,
    0,    0,    0,    0,  201,  202,  203,  204,   24,    0,
    0,   59,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,  664,    0,    0,    0,    0,   59,    0,    0,
  321,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   57,  608,    0,  322,    0,    0,    0,
  200,    0,    0,    0,  112,    0,  189,  190,  191,   57,
  192,  193,  194,   23,  195,  201,  202,  203,  204,    0,
    0,    0,   24,    0,    0,  435,    0,    0,  198,    0,
   23,   25,   26,   27,   28,   29,  199,    0,    0,   24,
  200,    0,    0,    0,    0,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,  201,  202,  203,  204,  323,
  324,  325,    0,  326,  327,    0,    0,    0,  328,    0,
  329,    0,   58,  330,  331,  332,  333,  334,  335,  336,
  337,  338,  339,    0,  340,  341,    0,  342,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,  354,
    0,  355,  356,   59,  321,    0,    0,  357,  358,  359,
  360,  361,  266,  362,  363,  364,  365,  366,  367,  368,
  322,  369,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  370,    0,   23,   57,    0,  227,    0,    0,
    0,    0,    0,   24,  371,  372,  373,  374,  170,    0,
   23,  375,   25,   26,   27,   28,   29,    0,    0,   24,
    0,    0,    0,    0,  171,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,    0,    0,    0,
  161,    0,    0,  323,  324,  325,    0,  326,  327,    0,
    0,    0,  328,    0,  329,    0,    0,  330,  331,  332,
  333,  334,  335,  336,  337,  338,  339,    0,  340,  341,
    0,  342,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,  354,    0,  355,  356,    0,    0,    0,
    0,  357,  358,  359,  360,  361,  264,  362,  363,  364,
  365,  366,  367,  368,  266,  369,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  370,    0,    0,    0,
  266,    0,    0,    0,    0,    0,    0,    0,  371,  372,
  373,  374,    0,    0,    0,  375,  224,  225,    0,    0,
    0,    0,    0,    0,    0,   24,  226,    0,    0,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,    0,
  189,  190,  191,    0,  192,  193,  194,    0,  195,    0,
    0,    0,    0,    0,    0,  924,  925,    0,    0,  926,
    0,    0,  198,  266,  266,  266,    0,  266,  266,    0,
  199,    0,  266,    0,  266,    0,   58,  266,  266,  266,
  266,  266,  266,  266,  266,  266,  266,    0,  266,  266,
    0,  266,  266,  266,  266,  266,  266,  266,  266,  266,
  266,  266,  266,  266,    0,  266,  266,   59,  264,    0,
    0,  266,  266,  266,  266,  266,  267,  266,  266,  266,
  266,  266,  266,  266,  264,  266,  316,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  266,    0,    0,   57,
    0,    0,    0,    0,    0,    0,  116,    0,  266,  266,
  266,  266,    0,    0,    0,  266,    0,    0,    0,    0,
   40,   41,  927,   42,   43,    0,  112,   45,    0,    0,
   46,   47,   48,   49,   50,    0,   51,  928,  929,  930,
    0,    0,    0,    0,    0,    0,    0,  264,  264,  264,
    0,  264,  264,    0,    0,    0,  264,    0,  264,    0,
    0,  264,  264,  264,  264,  264,  264,  264,  264,  264,
  264,    0,  264,  264,    0,  264,  264,  264,  264,  264,
  264,  264,  264,  264,  264,  264,  264,  264,    0,  264,
  264,    0,    0,   54,    0,  264,  264,  264,  264,  264,
  265,  264,  264,  264,  264,  264,  264,  264,  267,  264,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  264,    0,    0,    0,  267,    0,    0,    0,    0,    0,
    0,    0,  264,  264,  264,  264,    0,    0,    0,  264,
  224,  225,    0,    0,    0,   55,   56,    0,    0,   24,
  226,    0,    0,    0,    0,    0,    0,    0,   25,   26,
   27,   28,   29,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  267,  267,  267,
    0,  267,  267,    0,    0,    0,  267,    0,  267,    0,
   58,  267,  267,  267,  267,  267,  267,  267,  267,  267,
  267,    0,  267,  267,  963,  267,  267,  267,  267,  267,
  267,  267,  267,  267,  267,  267,  267,  267,    0,  267,
  267,   59,  265,   58,    0,  267,  267,  267,  267,  267,
   58,  267,  267,  267,  267,  267,  267,  267,  265,  267,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  267,    0,    0,   57,   59,    0,    0,    0,    0,    0,
    0,   59,  267,  267,  267,  267,    0,    0,    0,  267,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   58,    0,    0,    0,   57,    0,    0,    0,
    0,    0,    0,   57,    0,  103,    0,    0,    0,    0,
    0,  265,  265,  265,    0,  265,  265,    0,    0,    0,
  265,    0,  265,   59,    0,  265,  265,  265,  265,  265,
  265,  265,  265,  265,  265,    0,  265,  265,    0,  265,
  265,  265,  265,  265,  265,  265,  265,  265,  265,  265,
  265,  265,   58,  265,  265,   57,    0,    0,    0,  265,
  265,  265,  265,  265,    0,  265,  265,  265,  265,  265,
  265,  265,    0,  265,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   59,  265,   58,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  265,  265,  265,  265,
    0,    0,    0,  265,  224,  431,    0,    0,    0,   58,
    0,    0,    0,   24,  432,   57,   59,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,    0,
  189,  190,  191,    0,  192,  193,  194,   23,  195,   58,
   59,    0,    0,    0,  224,    0,   24,   58,   57,  435,
    0,    0,  198,   24,    0,   25,   26,   27,   28,   29,
  199,    0,   25,   26,   27,   28,   29,    0,    0,    0,
   59,    0,   57,   58,    0,    0,    0,    0,   59,    0,
  921,   58,    0,    0,    0,    0,    0,    0,    0,  189,
  190,  191,  414,  192,  193,  194,   23,  415,   58,    0,
    0,    0,   57,    0,   59,   24,  416,    0,  417,    0,
   57,  198,   59,    0,   25,   26,   27,   28,   29,  199,
    0,    0,    0,    0,    0,    0,    0,    0,  326,   59,
    0,    0,    0,    0,    0,    0,   57,    0,    0,    0,
    0,    0,    0,    0,   57,    0,    0,    0,    0,  189,
  190,  191,  414,  192,  193,  194,   23,  415,    0,  326,
    0,  287,    0,    0,    0,   24,  425,    0,  417,    0,
    0,  198,    0,    0,   25,   26,   27,   28,   29,  199,
  256,    0,  189,  190,  191,    0,  192,  193,  194,   23,
  195,  326,    0,    0,    0,    0,    0,    0,   24,    0,
  116,  435,    0,    0,  198,    0,    0,   25,   26,   27,
   28,   29,  199,   23,   40,   41,    0,   42,   43,    0,
    0,   45,   24,  864,   46,   47,   48,   49,   50,    0,
   51,   25,   26,   27,   28,   29,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,   23,   24,    0,    0,    0,    0,  784,    0,    0,
   24,   25,   26,   27,   28,   29,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,  321,   23,    0,    0,
    0,    0,    0,    0,    0,  224,   24,   54,    0,    0,
  112,    0,  322,    0,   24,   25,   26,   27,   28,   29,
    0,    0,  224,   25,   26,   27,   28,   29,    0,    0,
    0,   24,    0,    0,    0,    0,    0,    0,    0,    0,
   25,   26,   27,   28,   29,    0,    0,    0,    0,    0,
    0,    0,  326,    0,    0,    0,    0,    0,    0,   55,
   56,  326,    0,    0,    0,    0,    0,    0,    0,    0,
  326,  326,  326,  326,  326,  323,  324,  325,    0,  326,
  327,    0,    0,    0,  328,    0,  329,    0,    0,  330,
  331,  332,  333,  334,  335,  336,  337,  338,  339,    0,
  340,  341,    0,  342,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,  354,    0,  355,  356,  386,
  386,    0,    0,  357,  358,  359,  360,  361,    0,  362,
  363,  364,  365,  366,  367,  368,    0,  369,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  370,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  371,  372,  373,  374,    0,    0,    0,  375,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  386,  386,  386,    0,  386,  386,    0,    0,
    0,  386,    0,  386,    0,    0,  386,  386,  386,  386,
  386,  386,  386,  386,  386,  386,    0,  386,  386,    0,
  386,  386,  386,  386,  386,  386,  386,  386,  386,  386,
  386,  386,  386,    0,  386,  386,  357,  357,    0,    0,
  386,  386,  386,  386,  386,    0,  386,  386,  386,  386,
  386,  386,  386,    0,  386,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  386,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  386,  386,  386,
  386,    0,    0,    0,  386,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  357,
  357,  357,    0,  357,  357,    0,    0,    0,  357,    0,
  357,    0,    0,  357,  357,  357,  357,  357,  357,  357,
  357,  357,  357,    0,  357,  357,    0,  357,  357,  357,
  357,  357,  357,  357,  357,  357,  357,  357,  357,  357,
    0,  357,  357,  337,  337,    0,    0,  357,  357,  357,
  357,  357,    0,  357,  357,  357,  357,  357,  357,  357,
    0,  357,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  357,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  357,  357,  357,  357,    0,    0,
    0,  357,    0,    0,    0,    0,    0,    0,    0,    0,
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
  338,    0,  338,  338,  344,  344,    0,    0,  338,  338,
  338,  338,  338,    0,  338,  338,  338,  338,  338,  338,
  338,    0,  338,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  338,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,  338,  338,  338,    0,
    0,    0,  338,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  344,  344,  344,
    0,  344,  344,    0,    0,    0,  344,    0,  344,    0,
    0,  344,  344,  344,  344,  344,  344,  344,  344,  344,
  344,    0,  344,  344,    0,  344,  344,  344,  344,  344,
  344,  344,  344,  344,  344,  344,  344,  344,    0,  344,
  344,  339,  339,    0,    0,  344,  344,  344,  344,  344,
    0,  344,  344,  344,  344,  344,  344,  344,    0,  344,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  344,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  344,  344,  344,  344,    0,    0,    0,  344,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  339,  339,  339,    0,  339,  339,
    0,    0,    0,  339,    0,  339,    0,    0,  339,  339,
  339,  339,  339,  339,  339,  339,  339,  339,    0,  339,
  339,    0,  339,  339,  339,  339,  339,  339,  339,  339,
  339,  339,  339,  339,  339,    0,  339,  339,  350,  350,
    0,    0,  339,  339,  339,  339,  339,    0,  339,  339,
  339,  339,  339,  339,  339,    0,  339,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  339,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  339,
  339,  339,  339,    0,    0,    0,  339,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  350,  350,  350,    0,  350,  350,    0,    0,    0,
  350,    0,  350,    0,    0,  350,  350,  350,  350,  350,
  350,  350,  350,  350,  350,    0,  350,  350,    0,  350,
  350,  350,  350,  350,  350,  350,  350,  350,  350,  350,
  350,  350,    0,  350,  350,  372,  372,    0,    0,  350,
  350,  350,  350,  350,    0,  350,  350,  350,  350,  350,
  350,  350,    0,  350,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  350,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  350,  350,  350,  350,
    0,    0,    0,  350,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  372,  372,
  372,    0,  372,  372,    0,    0,    0,  372,    0,  372,
    0,    0,  372,  372,  372,  372,  372,  372,  372,  372,
  372,  372,    0,  372,  372,    0,  372,  372,  372,  372,
  372,  372,  372,  372,  372,  372,  372,  372,  372,    0,
  372,  372,  368,  368,    0,    0,  372,  372,  372,  372,
  372,    0,  372,  372,  372,  372,  372,  372,  372,    0,
  372,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  372,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  372,  372,  372,  372,    0,    0,    0,
  372,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  368,  368,  368,    0,  368,
  368,    0,    0,    0,  368,    0,  368,    0,    0,  368,
  368,  368,  368,  368,  368,  368,  368,  368,  368,    0,
  368,  368,    0,  368,  368,  368,  368,  368,  368,  368,
  368,  368,  368,  368,  368,  368,    0,  368,  368,  345,
  345,    0,    0,  368,  368,  368,  368,  368,    0,  368,
  368,  368,  368,  368,  368,  368,    0,  368,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  368,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  368,  368,  368,  368,    0,    0,    0,  368,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  345,  345,  345,    0,  345,  345,    0,    0,
    0,  345,    0,  345,    0,    0,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,    0,  345,  345,    0,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,    0,  345,  345,  340,  340,    0,    0,
  345,  345,  345,  345,  345,    0,  345,  345,  345,  345,
  345,  345,  345,    0,  345,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  345,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  345,  345,  345,
  345,    0,    0,    0,  345,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  340,
  340,  340,    0,  340,  340,    0,    0,    0,  340,    0,
  340,    0,    0,  340,  340,  340,  340,  340,  340,  340,
  340,  340,  340,    0,  340,  340,    0,  340,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,  340,  340,
    0,  340,  340,  341,  341,    0,    0,  340,  340,  340,
  340,  340,    0,  340,  340,  340,  340,  340,  340,  340,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  340,  340,  340,  340,    0,    0,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  341,  341,  341,    0,
  341,  341,    0,    0,    0,  341,    0,  341,    0,    0,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
    0,  341,  341,    0,  341,  341,  341,  341,  341,  341,
  341,  341,  341,  341,  341,  341,  341,    0,  341,  341,
  346,  346,    0,    0,  341,  341,  341,  341,  341,    0,
  341,  341,  341,  341,  341,  341,  341,    0,  341,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  341,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  341,  341,  341,  341,    0,    0,    0,  341,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  346,  346,  346,    0,  346,  346,    0,
    0,    0,  346,    0,  346,    0,    0,  346,  346,  346,
  346,  346,  346,  346,  346,  346,  346,    0,  346,  346,
    0,  346,  346,  346,  346,  346,  346,  346,  346,  346,
  346,  346,  346,  346,    0,  346,  346,  355,  355,    0,
    0,  346,  346,  346,  346,  346,    0,  346,  346,  346,
  346,  346,  346,  346,    0,  346,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  346,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  346,  346,
  346,  346,    0,    0,    0,  346,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  355,  355,  355,    0,  355,  355,    0,    0,    0,  355,
    0,  355,    0,    0,  355,  355,  355,  355,  355,  355,
  355,  355,  355,  355,    0,  355,  355,    0,  355,  355,
  355,  355,  355,  355,  355,  355,  355,  355,  355,  355,
  355,    0,  355,  355,  348,  348,    0,    0,  355,  355,
  355,  355,  355,    0,  355,  355,  355,  355,  355,  355,
  355,    0,  355,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  355,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  355,  355,  355,  355,    0,
    0,    0,  355,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  348,  348,  348,
    0,  348,  348,    0,    0,    0,  348,    0,  348,    0,
    0,  348,  348,  348,  348,  348,  348,  348,  348,  348,
  348,    0,  348,  348,    0,  348,  348,  348,  348,  348,
  348,  348,  348,  348,  348,  348,  348,  348,    0,  348,
  348,  351,  351,    0,    0,  348,  348,  348,  348,  348,
    0,  348,  348,  348,  348,  348,  348,  348,    0,  348,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  348,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  348,  348,  348,  348,    0,    0,    0,  348,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  351,  351,  351,    0,  351,  351,
    0,    0,    0,  351,    0,  351,    0,    0,  351,  351,
  351,  351,  351,  351,  351,  351,  351,  351,    0,  351,
  351,    0,  351,  351,  351,  351,  351,  351,  351,  351,
  351,  351,  351,  351,  351,    0,  351,  351,  369,  369,
    0,    0,  351,  351,  351,  351,  351,    0,  351,  351,
  351,  351,  351,  351,  351,    0,  351,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  351,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  351,
  351,  351,  351,    0,    0,    0,  351,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  369,  369,  369,    0,  369,  369,    0,    0,    0,
  369,    0,  369,    0,    0,  369,  369,  369,  369,  369,
  369,  369,  369,  369,  369,    0,  369,  369,    0,  369,
  369,  369,  369,  369,  369,  369,  369,  369,  369,  369,
  369,  369,    0,  369,  369,  342,  342,    0,    0,  369,
  369,  369,  369,  369,    0,  369,  369,  369,  369,  369,
  369,  369,    0,  369,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  369,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  369,  369,  369,  369,
    0,    0,    0,  369,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  342,  342,
  342,    0,  342,  342,    0,    0,    0,  342,    0,  342,
    0,    0,  342,  342,  342,  342,  342,  342,  342,  342,
  342,  342,    0,  342,  342,    0,  342,  342,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,    0,
  342,  342,  347,  347,    0,    0,  342,  342,  342,  342,
  342,    0,  342,  342,  342,  342,  342,  342,  342,    0,
  342,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  342,  342,  342,  342,    0,    0,    0,
  342,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  347,  347,  347,    0,  347,
  347,    0,    0,    0,  347,    0,  347,    0,    0,  347,
  347,  347,  347,  347,  347,  347,  347,  347,  347,    0,
  347,  347,    0,  347,  347,  347,  347,  347,  347,  347,
  347,  347,  347,  347,  347,  347,    0,  347,  347,  349,
  349,    0,    0,  347,  347,  347,  347,  347,    0,  347,
  347,  347,  347,  347,  347,  347,    0,  347,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  347,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  347,  347,  347,  347,    0,    0,    0,  347,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  349,  349,  349,    0,  349,  349,    0,    0,
    0,  349,    0,  349,    0,    0,  349,  349,  349,  349,
  349,  349,  349,  349,  349,  349,    0,  349,  349,    0,
  349,  349,  349,  349,  349,  349,  349,  349,  349,  349,
  349,  349,  349,    0,  349,  349,  352,  352,    0,    0,
  349,  349,  349,  349,  349,    0,  349,  349,  349,  349,
  349,  349,  349,    0,  349,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  349,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  349,  349,  349,
  349,    0,    0,    0,  349,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  352,
  352,  352,    0,  352,  352,    0,    0,    0,  352,    0,
  352,    0,    0,  352,  352,  352,  352,  352,  352,  352,
  352,  352,  352,    0,  352,  352,    0,  352,  352,  352,
  352,  352,  352,  352,  352,  352,  352,  352,  352,  352,
    0,  352,  352,  353,  353,    0,    0,  352,  352,  352,
  352,  352,    0,  352,  352,  352,  352,  352,  352,  352,
    0,  352,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  352,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  352,  352,  352,  352,    0,    0,
    0,  352,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  353,  353,  353,    0,
  353,  353,    0,    0,    0,  353,    0,  353,    0,    0,
  353,  353,  353,  353,  353,  353,  353,  353,  353,  353,
    0,  353,  353,    0,  353,  353,  353,  353,  353,  353,
  353,  353,  353,  353,  353,  353,  353,    0,  353,  353,
  354,  354,    0,    0,  353,  353,  353,  353,  353,    0,
  353,  353,  353,  353,  353,  353,  353,    0,  353,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  353,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  353,  353,  353,  353,    0,    0,    0,  353,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  354,  354,  354,    0,  354,  354,    0,
    0,    0,  354,    0,  354,    0,    0,  354,  354,  354,
  354,  354,  354,  354,  354,  354,  354,    0,  354,  354,
    0,  354,  354,  354,  354,  354,  354,  354,  354,  354,
  354,  354,  354,  354,  322,  354,  354,    0,    0,    0,
    0,  354,  354,  354,  354,  354,    0,  354,  354,  354,
  354,  354,  354,  354,    0,  354,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  354,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  354,  354,
  354,  354,    0,    0,    0,  354,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  323,  324,  325,
    0,  326,  327,    0,    0,    0,  328,    0,  329,    0,
    0,  330,  331,  332,  333,  334,  335,  336,  337,  338,
  339,    0,  340,  341,    0,  342,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  354,  270,  355,
  356,    0,    0,    0,    0,  357,  358,  359,  360,  361,
    0,  362,  363,  364,  365,  366,  367,  368,    0,  369,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  370,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  371,  372,  373,  374,    0,    0,    0,  375,
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
  273,  272,  272,    0,    0,    0,    0,  272,  272,  272,
  272,  272,    0,  272,  272,  272,  272,  272,  272,  272,
    0,  272,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  272,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  272,  272,  272,  272,    0,    0,
    0,  272,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  273,  273,  273,    0,  273,  273,    0,
    0,    0,  273,    0,  273,    0,    0,  273,  273,  273,
  273,  273,  273,  273,  273,  273,  273,    0,  273,  273,
    0,  273,  273,  273,  273,  273,  273,  273,  273,  273,
  273,  273,  273,  273,    0,  273,  273,    0,    0,    0,
    0,  273,  273,  273,  273,  273,    0,  273,  273,  273,
  273,  273,  273,  273,    0,  273,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  273,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  326,    0,  273,  273,
  273,  273,    0,  329,    0,  273,  330,  331,  332,  333,
  334,  335,  336,  337,  338,  339,    0,  340,  341,    0,
  342,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,  354,    0,  355,  356,    0,    0,    0,    0,
  357,  358,  359,  360,  361,    0,  362,  363,  364,  365,
  366,  367,  368,    0,  369,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  370,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  371,  372,  373,
  374,    0,    0,    0,  375,
  };
  protected static readonly short [] yyCheck = {             6,
  556,   44,    6,  179,   61,   65,   41,  570,  124,   16,
   41,   41,   41,   73,   60,   40,   44,   41,   41,   41,
   41,   41,   62,   91,   93,   40,   44,   33,   44,  260,
   44,   44,   44,  127,   44,   44,   33,  123,    6,   44,
  376,   44,  178,  324,   44,   91,   44,   40,   16,   40,
   57,   40,  612,  110,  524,  280,  834,   64,   65,  315,
  123,  177,  123,  280,   44,  123,   73,  783,   60,   76,
  123,  123,   76,  123,  123,  268,  854,  123,  276,   44,
  273,   88,   62,  281,  260,  291,  292,  181,   61,  263,
  184,  291,  292,  100,  101,  271,   64,   65,   44,  273,
   61,  108,  276,  573,  574,   73,   61,  114,   76,  125,
  117,  125,  119,  173,   65,  119,  123,   61,  324,  126,
  898,   44,  126,  274,  687,   44,  133,  383,    0,   20,
  137,  123,  139,   44,   41,  260,   87,   44,  295,  296,
  297,   41,  272,  280,   44,  280,  862,  154,  155,  117,
  157,  119,    0,   61,  270,  380,  313,  388,  126,   61,
  125,   20,   40,  380,   42,   40,   44,  727,   40,  729,
   42,  257,  732,  429,   93,  269,  313,  383,  313,  125,
  187,  188,  380,  383,  654,  655,  347,  324,  658,  324,
  351,   40,   40,   42,   42,   44,   87,  390,   89,  535,
   91,  281,  125,   94,  206,  207,  213,   40,  215,  383,
  441,  274,   40,  274,  125,  391,  274,  124,  264,  270,
  271,  274,  274,  693,  274,  274,  789,  273,   87,  266,
   89,  257,   91,  124, 1012,  242,  282,  283,  284,  285,
  286,  295,  296,  380,  380,  380,  382,  125,   40,  286,
  291,  292,  259,  257,  295,  296,   41,  272,  274,   44,
  274,  418,  257,  309,  824,  124,  326,  456,  274,  326,
  306,  277,  461,  280,  383,  464,  125,  274,  274,  272,
  287,  272,   61,  272,  473,  287,  274,   40,  295,  296,
  760,  761,  183,  260,  764,  765,  288,  860,  334,  335,
  125,   40,   41,   42,   41,   44,  313,   44,  871,  872,
   41,  260,   40,   44,  383,  383,  373,   41,   41,  326,
   44,   44,  329,   40,  331,   42,  333,   41,  335,  336,
   44,  338,  339,  340,  341,  342,  343,  344,  345,  316,
   40,  123,  383,  383,  351,  352,  267,   41,  383,  356,
   44,  257,  383,  383,  383,  383,  381,  920,  326,  383,
  383,  383,  383,  383,  371,  383,  373,  923,  375,  373,
  383,  383,  935,  383,  383,  938,  149,  268,  383,  267,
  383,  851,  273,  383,  331,  383,  333,   40,  434,  306,
  397,  451,  435,  340,   42,  531,  268,  269,  274,  556,
  257,  558,  123,  275,  316,  373,  563,  970,  971,   40,
   41,   42,  123,   44,   40,  551,   41,  289,  290,   44,
  268,  269,  123,   41,  272,  273,  989,  275,   41,  324,
  566,   44,  205,  206,  207,  260,   41,   41,  257,   44,
   44,  289,  290,  450,  451,   41,  450,  604,   44,  456,
  223,  276,  459,  325,  461,  462,   41,  464,  465,   44,
  467,   41,  469,   40,  380,   40,  473,  474,   40,  476,
  527,   40,   60,   44,   58,   58,   58,  325,   41,    0,
  487,  488,  450,  451,   62,  257,   40,  494,  495,  496,
  260,  264,  257,   61,  319,  320,  321,  380,   44,  390,
   40,  326,  327,   91,  329,  330,  331,  332,  281,  282,
  283,  284,  380,  520,  287,   44,  523,   44,  525,  526,
  527,  257,  526,  527,  332,  260,  334,  260,   58,  337,
  666,  304,  291,  292,  420,  123,  295,  296,  545,  546,
  547,  548,  678,   61,  680,  333,  423,  313,  257,  556,
  323,  558,  325,  380,  327,   44,  563,  525,  526,  527,
  389,  257,  370,  322,  372,  389,  389,  389,  575,  380,
   44,  575,   44,  346,  347,  348,  349,  350,  380,  124,
  353,   40,  273,  389,  357,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,   60,   44,    0,   44,
  660,  608,  123,  610,   44,  612,   44,  575,   44,   44,
  617,  618,  619,  620,  621,  622,  623,  624,  625,  626,
  627,  628,   44,   60,  383,  389,   87,   91,   89,   90,
  260,  404,  389,  389,  770,  389,  257,  389,  389,  646,
  756,  757,  389,  136,  780,  389,  389,  389,  389,  656,
  657,  257,  659,  660,   91,   62,  257,  257,  380,  123,
  380,   41,  819,  820,   44,  841,  842,   44,  266,   40,
   44,   44,   44,  134,  135,   44,  264,  138,   44,  140,
   60,   44,  689,   44,   44,  273,  123,   44,  656,  657,
   44,  659,  660,  186,  282,  283,  284,  285,  286,   44,
   44,   44,   44,   44,  260,  257,   44,  257,  865,  427,
  257,   91,   91,  389,   44,   44,   44,  490,   44,  212,
  727,  214,  729,   44,   44,  732,  786,  843,  844,  905,
  906,  847,  848,  909,  910,   44,  229,  257,   44,   44,
   44,   44,   44,  123,   44,   44,   44,  268,  269,   44,
   44,  272,  273,  274,  275,   44,   44,   44,   44,  766,
   44,   44,  766,   44,   91,  260,  923,   44,  289,  290,
   44,  544,   44,  389,  295,  296,  783,  784,  954,  955,
  956,  957,  389,  380,   44,  278,   40,    0,   42,   44,
  380,  907,  908,   44,  382,  911,  912,   44,  766,  292,
  264,  294,   44,  324,  325,  981,   60,  380,   44,  273,
  380,   44,   40,  328,  257,  389,  380,  824,  282,  283,
  284,  285,  286,  380,  831,  832,  257,  264,  601,  602,
  603,  380,  605,  606,  257,   44,  273,   91,   44,  260,
   93,  257,  958,  616,  381,  282,  283,  284,  285,  286,
   40,   40,   40,  268,  269,  862,  257,  272,  273,  328,
  275,  328,  869,  867,   44,  257,  268,  269,  380,  123,
  272,  273,  257,  275,  289,  290,  333,  257,  258,  259,
  333,  261,  262,  263,  264,  265,   40,  289,  290,  662,
  380,   44,  272,  273,  328,  257,   44,  670,  389,  279,
  380,  389,  282,  283,  284,  285,  286,  287,  389,  257,
  325,  257,  405,  293,  316,  380,  923,  381,  257,   44,
    0,  316,  272,  325,   10,  272,  363,  307,  308,   95,
  310,  311,  272,  223,  314,  272,  272,  317,  318,  319,
  320,  321,  222,  323,   20,   44,  299,  296,  558,  108,
  257,  258,  259,  446,  261,  262,  263,  254,  265,  385,
  242,   60,  455,  736,   61,  918,  259,  460,  819,  442,
  463,  379,  279,  466,    0,  468,  983,  470,  471,  472,
  287,  820,  475,  379,  477,  478,  479,  480,  995,  996,
  997,  443,   91,  753,  658,  869,  563,  862,   -1,  379,
  380,  443,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,  394,  395,  396,  397,  272,  273,
   -1,   -1,   -1,   -1,  123,  279,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  287,   40,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  817,  818,   -1,   -1,   -1,   -1,
   -1,   -1,  432,  433,   60,  268,  269,  279,   -1,  272,
  273,   -1,  275,  260,   60,  287,  839,   -1,    0,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  289,  290,   -1,  276,
   -1,   -1,  855,   -1,  577,   91,  579,   -1,  581,  582,
   -1,  584,  585,   -1,  587,   91,  589,   -1,   -1,   -1,
  593,  594,   -1,  596,   -1,   -1,   -1,    0,   -1,   -1,
   -1,   -1,  325,   -1,   -1,   -1,   -1,  123,   -1,   -1,
  613,  614,  319,  320,  321,  379,   -1,  123,   -1,  326,
  327,   -1,  329,  330,  331,  332,  629,   -1,   -1,   -1,
  394,  395,  396,  397,   -1,   -1,   -1,   40,   41,   42,
   -1,   44,   -1,   -1,  647,   -1,   -1,  650,  257,  258,
  259,  383,  261,  262,  263,   -1,  265,   60,   -1,   62,
   -1,   -1,  426,  272,  273,   -1,   -1,   -1,   -1,  952,
  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,
   -1,   -1,   -1,  966,  967,  968,   -1,   -1,   91,   -1,
   93,  694,   -1,   -1,  697,   -1,   -1,  700,   -1,   -1,
  703,   -1,  705,   -1,  707,  708,  709,   -1,   -1,  712,
   -1,  714,  715,  716,  717,   -1,   -1,   -1,   -1, 1002,
  123,   -1,  125,   -1,   -1,   -1,   -1,  730,   -1,   -1,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,  257,  258,  259,   -1,  261,  262,  263,  751,  265,
   -1,   -1,   -1,  289,  290,   -1,  272,  273,  264,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,   -1,
  379,  287,   -1,   -1,  383,   -1,  282,  283,  284,  285,
  286,   -1,   -1,  786,   -1,  394,  395,  396,  397,  325,
   -1,  794,  795,   -1,  797,  798,   -1,  800,  801,    0,
  803,   -1,  805,   -1,   -1,   -1,  809,  810,   -1,  812,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,   -1,
   44,   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,  835,  275,  837,   -1,   60,    0,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  289,  290,   -1,
    0,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,    0,  265,  379,  867,  268,  269,   91,   -1,  272,
  273,  274,  275,  276,  380,   -1,  279,   -1,  394,  395,
  396,  397,   -1,  325,  287,   -1,  289,  290,   -1,   -1,
  293,   -1,   -1,  896,  897,   -1,   -1,   -1,  901,  123,
   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,
  426,  314,  315,   -1,  317,  318,  319,  320,  321,   -1,
  323,   -1,  325,   -1,   40,   41,   -1,   -1,   44,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   62,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   41,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   93,   -1,   60,
  383,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,  397,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,  123,   -1,  125,
   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,  426,   -1,  428,  429,   -1,   -1,  432,
  433,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
  264,  265,  123,   -1,   -1,   -1,   -1,   -1,  272,  273,
   40,   -1,   42,   -1,   -1,  279,   -1,   -1,  282,  283,
  284,  285,  286,  287,   -1,   -1,   -1,  268,  269,  293,
   -1,  272,  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,  289,  290,
  314,   -1,   -1,  317,  318,  319,  320,  321,   -1,  323,
   44,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,   -1,   60,   -1,  268,  269,
   -1,   -1,  272,  273,  325,  275,  289,  290,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,  289,
  290,  257,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,
  289,  290,   -1,   -1,    0,  379,  380,  273,  274,   40,
   -1,   42,  325,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  394,  395,  396,  397,   -1,  325,   -1,   -1,   -1,  123,
   -1,   -1,   -1,  264,   -1,   -1,  325,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,  432,  433,
   -1,   -1,   -1,  294,   -1,   -1,   -1,   -1,   -1,   -1,
  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,
   -1,  347,   -1,   -1,  350,  351,  352,  353,  354,  355,
  356,  357,  358,  359,   -1,  361,  362,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  376,   -1,  378,  379,    0,   -1,   -1,   -1,  384,  385,
  386,  387,  388,  389,  390,  391,  392,  393,  394,  395,
  396,   -1,  398,  273,  274,   40,   -1,   42,   -1,   -1,
   -1,   -1,   -1,  409,  399,  400,  401,  402,  403,  404,
  405,  406,  407,  408,   -1,  421,  422,  423,  424,   -1,
   -1,   -1,  428,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  287,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,  123,  378,  379,
    0,   -1,  273,  274,  384,  385,  386,  387,  388,   -1,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,    0,
   -1,   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,  409,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,  421,  422,  423,  424,  379,   -1,   -1,  428,  383,
   -1,   -1,   -1,  289,  290,   -1,   -1,   -1,   -1,   -1,
  394,  395,  396,  397,   -1,  336,  337,  338,   -1,  340,
  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,  350,
  351,  352,  353,  354,  355,  356,  357,  358,  359,  325,
  361,  362,   -1,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  376,   -1,  378,  379,  125,
   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,
  391,  392,  393,  394,  395,  396,   -1,  398,  273,  274,
   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,  409,   -1,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,  274,  275,
  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,   -1,
   -1,   -1,   -1,  289,  290,   -1,   -1,   -1,   -1,  295,
  296,   -1,   -1,  291,  292,   -1,   -1,  295,  296,  297,
  298,  299,  300,  301,  302,  303,  304,  305,   -1,   -1,
  316,  336,  337,  338,  312,  340,  341,   -1,  324,  325,
  345,   -1,  347,   -1,  322,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   -1,  378,  379,    0,   -1,   -1,   -1,  384,
  385,  386,  387,  388,  260,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,  273,  274,   40,   -1,   42,   -1,
  276,   -1,   -1,   -1,  409,  383,   -1,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,  421,  422,  423,  424,
   -1,   -1,   -1,  428,   -1,   -1,   -1,  268,  269,  289,
  290,  272,  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  319,  320,  321,   -1,   -1,  289,  290,
  326,  327,   -1,  329,  330,  331,  332,  336,  337,  338,
   -1,  340,  341,   -1,   -1,  325,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,  325,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,  123,  378,
  379,    0,   -1,  273,  274,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
    0,   -1,   40,   -1,   42,   -1,  261,  262,   -1,   -1,
  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,   -1,
  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,   -1,
  350,  351,  352,  353,  354,  355,  356,  357,  358,  359,
   -1,  361,  362,   -1,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  376,   -1,  378,  379,
  125,   -1,   -1,   -1,  384,  385,  386,  387,  388,   -1,
  390,  391,  392,  393,  394,  395,  396,   -1,  398,  273,
  274,   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,  409,
   -1,   -1,   -1,  268,  269,   -1,   -1,  272,  273,  274,
  275,  421,  422,  423,  424,   -1,   -1,   -1,  428,   -1,
   -1,   -1,   -1,   -1,  289,  290,  401,  402,  403,  404,
  295,  296,   -1,   -1,   -1,  410,  411,  412,  413,  414,
  415,  416,  417,  418,  419,   -1,   -1,   -1,   -1,   -1,
   -1,  316,  336,  337,  338,   -1,  340,  341,   -1,  324,
  325,  345,   -1,  347,   -1,   -1,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,    0,   -1,   -1,   -1,
  384,  385,  386,  387,  388,  260,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,  273,  274,   40,   -1,   42,
   -1,  276,   -1,   -1,   -1,  409,   -1,   -1,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  421,  422,  423,
  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,  268,  269,
  289,  290,  272,  273,   -1,  275,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  319,  320,  321,   -1,   -1,  289,
  290,  326,  327,   -1,  329,  330,  331,  332,  336,  337,
  338,   -1,  340,  341,   -1,   -1,  325,  345,   -1,  347,
   -1,   -1,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  359,   -1,  361,  362,  325,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,   -1,
  378,  379,    0,   -1,  273,  274,  384,  385,  386,  387,
  388,   -1,  390,  391,  392,  393,  394,  395,  396,   -1,
  398,    0,   -1,   40,   -1,   42,   -1,  261,  262,   -1,
   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   -1,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   -1,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,   -1,   -1,   -1,   -1,  384,  385,  386,  387,  388,
   -1,  390,  391,  392,  393,  394,  395,  396,   -1,  398,
  273,  274,   40,   -1,   42,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,  289,  290,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,
  414,  415,  416,  417,  418,  419,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,  325,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,  273,  274,   40,   -1,
   42,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
  268,  269,   -1,   -1,  272,  273,   -1,  275,  421,  422,
  423,  424,   -1,   -1,   -1,  428,   -1,   -1,   -1,  268,
  269,  289,  290,  272,  273,   -1,  275,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  289,  290,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,
  337,  338,   -1,  340,  341,   -1,   -1,  325,  345,   -1,
  347,   -1,   -1,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,  325,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   -1,   -1,  273,  274,  384,  385,  386,
  387,  388,   -1,  390,  391,  392,  393,  394,  395,  396,
   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
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
  398,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,  409,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  421,  422,  423,  424,   -1,   -1,   -1,
  428,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  336,  337,  338,   -1,  340,  341,
   -1,   -1,  123,  345,   -1,  347,   91,   -1,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  359,   -1,  361,
  362,   -1,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  376,   60,  378,  379,  123,   -1,
   -1,   -1,  384,  385,  386,  387,  388,   -1,  390,  391,
  392,  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   91,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,
  422,  423,  424,   -1,   -1,  264,  428,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,  123,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   60,   -1,   -1,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,  308,
   -1,  310,  311,  312,   -1,  314,   -1,   -1,  317,  318,
  319,  320,  321,  264,  323,   -1,   -1,   91,   -1,   -1,
   -1,   -1,  273,   -1,   -1,  334,  335,   -1,   60,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  123,
  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   91,
   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,
   -1,  380,  287,   -1,   -1,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,  123,  307,  308,   -1,  310,  311,   -1,   -1,  314,
   -1,   -1,  317,  318,  319,  320,  321,   -1,  323,  264,
   -1,   -1,  363,   60,   -1,   -1,   -1,   -1,  273,   -1,
   91,   -1,   -1,  432,  433,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  379,  380,   -1,   -1,  383,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,  394,
  395,  396,  397,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   60,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,  363,   -1,
   -1,   -1,   -1,  287,   -1,   -1,   -1,  432,  433,  293,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,  307,  308,   -1,  310,  311,   -1,   -1,
  314,  273,   -1,  317,  318,  319,  320,  321,   -1,  323,
  282,  283,  284,  285,  286,  123,   -1,   -1,   -1,   -1,
   -1,  293,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  306,  307,  308,   -1,  310,  311,
   -1,   -1,  314,   -1,   -1,  317,  318,  319,  320,  321,
   -1,  323,   -1,  264,   -1,   -1,   -1,   91,   -1,   -1,
   -1,   -1,  273,   -1,   -1,  379,  380,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,  264,   -1,   -1,
  394,  395,  396,  397,   -1,   -1,  273,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   60,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,  380,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  432,  433,
  307,  308,   -1,  310,  311,   91,   -1,  314,   -1,   -1,
  317,  318,  319,  320,  321,  322,  323,  348,  349,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  420,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,  123,   60,   -1,
  432,  433,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   91,
   -1,   -1,   -1,  380,   -1,  652,  653,   -1,  306,  307,
  308,   -1,  310,  311,   -1,   -1,  314,   -1,   -1,  317,
  318,  319,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
  264,   -1,   -1,   -1,  691,  692,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,  432,  433,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,
   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  380,  307,  308,   -1,  310,  311,   -1,   -1,
  314,   -1,   -1,  317,  318,  319,  320,  321,  322,  323,
   -1,   -1,  123,   -1,   -1,   60,   -1,   -1,  264,   -1,
   60,  758,  759,   -1,   -1,  762,  763,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,  432,  433,   91,  293,   -1,   -1,
   -1,   91,   -1,   -1,   -1,  792,   -1,   -1,   -1,   -1,
   -1,  307,  308,   -1,  310,  311,  380,   -1,  314,   -1,
   41,  317,  318,  319,  320,  321,  322,  323,  123,   -1,
   -1,   -1,  264,  123,   60,   -1,   -1,   -1,   -1,   60,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,  845,  846,
   -1,  293,  849,  850,   -1,   91,   -1,   -1,  432,  433,
   91,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,   -1,  314,   -1,  380,  317,  318,  319,  320,  321,
   40,  323,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,
   -1,   -1,  123,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   60,   -1,  273,   -1,   -1,   -1,   41,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,  913,   -1,   -1,   -1,
   -1,   -1,  293,   -1,   -1,   60,  432,  433,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,  307,  308,  380,  310,
  311,   -1,   -1,  314,   -1,   -1,  317,  318,  319,  320,
  321,   -1,  323,   -1,   -1,   -1,   91,   -1,   -1,  264,
   -1,   60,   -1,  123,  264,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,  282,  283,  284,
  285,  286,  282,  283,  284,  285,  286,   -1,  123,   -1,
  432,  433,   91,  293,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,  309,   -1,   -1,  307,  308,  380,
  310,  311,   -1,   -1,  314,   -1,   -1,  317,  318,  319,
  320,  321,   -1,  323,  123,   60,   -1,   -1,  264,   -1,
   -1,   -1,   91,  264,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,  282,  283,  284,  285,
  286,  282,  283,  284,  285,  286,   91,  293,   -1,   -1,
   -1,  432,  433,  294,  123,   -1,   -1,   -1,   -1,   -1,
   41,  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,
  380,  317,  318,  319,  320,  321,   -1,  323,  123,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   60,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,
   91,   -1,   -1,   -1,   -1,   -1,   -1,  287,  125,  264,
   -1,   -1,  432,  433,   -1,   -1,   -1,   91,  273,   -1,
   -1,   -1,   -1,  278,  380,   -1,   -1,  282,  283,  284,
  285,  286,  123,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,  123,
   -1,  125,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  432,  433,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
   60,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,  379,
  279,   -1,   -1,   -1,   -1,   -1,   60,   -1,  287,  264,
   -1,   -1,   -1,   -1,  394,  395,  396,  397,  273,   -1,
   -1,   91,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,  125,   -1,   -1,   -1,   -1,   91,   -1,   -1,
  257,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  123,  309,   -1,  273,   -1,   -1,   -1,
  379,   -1,   -1,   -1,  383,   -1,  257,  258,  259,  123,
  261,  262,  263,  264,  265,  394,  395,  396,  397,   -1,
   -1,   -1,  273,   -1,   -1,  276,   -1,   -1,  279,   -1,
  264,  282,  283,  284,  285,  286,  287,   -1,   -1,  273,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,  394,  395,  396,  397,  336,
  337,  338,   -1,  340,  341,   -1,   -1,   -1,  345,   -1,
  347,   -1,   60,  350,  351,  352,  353,  354,  355,  356,
  357,  358,  359,   -1,  361,  362,   -1,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
   -1,  378,  379,   91,  257,   -1,   -1,  384,  385,  386,
  387,  388,  125,  390,  391,  392,  393,  394,  395,  396,
  273,  398,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  409,   -1,  264,  123,   -1,  125,   -1,   -1,
   -1,   -1,   -1,  273,  421,  422,  423,  424,  278,   -1,
  264,  428,  282,  283,  284,  285,  286,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  294,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  294,   -1,   -1,  336,  337,  338,   -1,  340,  341,   -1,
   -1,   -1,  345,   -1,  347,   -1,   -1,  350,  351,  352,
  353,  354,  355,  356,  357,  358,  359,   -1,  361,  362,
   -1,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,  125,  390,  391,  392,
  393,  394,  395,  396,  257,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,
  423,  424,   -1,   -1,   -1,  428,  264,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
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
  264,  265,   -1,   -1,   -1,  432,  433,   -1,   -1,  273,
  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  336,  337,  338,
   -1,  340,  341,   -1,   -1,   -1,  345,   -1,  347,   -1,
   60,  350,  351,  352,  353,  354,  355,  356,  357,  358,
  359,   -1,  361,  362,   41,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,   -1,  378,
  379,   91,  257,   60,   -1,  384,  385,  386,  387,  388,
   60,  390,  391,  392,  393,  394,  395,  396,  273,  398,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  409,   -1,   -1,  123,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   91,  421,  422,  423,  424,   -1,   -1,   -1,  428,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,  125,   -1,   -1,   -1,   -1,
   -1,  336,  337,  338,   -1,  340,  341,   -1,   -1,   -1,
  345,   -1,  347,   91,   -1,  350,  351,  352,  353,  354,
  355,  356,  357,  358,  359,   -1,  361,  362,   -1,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  376,   60,  378,  379,  123,   -1,   -1,   -1,  384,
  385,  386,  387,  388,   -1,  390,  391,  392,  393,  394,
  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   91,  409,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,  424,
   -1,   -1,   -1,  428,  264,  265,   -1,   -1,   -1,   60,
   -1,   -1,   -1,  273,  274,  123,   91,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,  264,  265,   60,
   91,   -1,   -1,   -1,  264,   -1,  273,   60,  123,  276,
   -1,   -1,  279,  273,   -1,  282,  283,  284,  285,  286,
  287,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   91,   -1,  123,   60,   -1,   -1,   -1,   -1,   91,   -1,
   93,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,  260,  261,  262,  263,  264,  265,   60,   -1,
   -1,   -1,  123,   -1,   91,  273,  274,   -1,  276,   -1,
  123,  279,   91,   -1,  282,  283,  284,  285,  286,  287,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   91,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  257,
  258,  259,  260,  261,  262,  263,  264,  265,   -1,   91,
   -1,  123,   -1,   -1,   -1,  273,  274,   -1,  276,   -1,
   -1,  279,   -1,   -1,  282,  283,  284,  285,  286,  287,
  273,   -1,  257,  258,  259,   -1,  261,  262,  263,  264,
  265,  123,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
  293,  276,   -1,   -1,  279,   -1,   -1,  282,  283,  284,
  285,  286,  287,  264,  307,  308,   -1,  310,  311,   -1,
   -1,  314,  273,  274,  317,  318,  319,  320,  321,   -1,
  323,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
  273,  282,  283,  284,  285,  286,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,  257,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  273,  380,   -1,   -1,
  383,   -1,  273,   -1,  273,  282,  283,  284,  285,  286,
   -1,   -1,  264,  282,  283,  284,  285,  286,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,  432,
  433,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,  336,  337,  338,   -1,  340,
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
  373,  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,
   -1,  384,  385,  386,  387,  388,   -1,  390,  391,  392,
  393,  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  340,   -1,  421,  422,
  423,  424,   -1,  347,   -1,  428,  350,  351,  352,  353,
  354,  355,  356,  357,  358,  359,   -1,  361,  362,   -1,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  376,   -1,  378,  379,   -1,   -1,   -1,   -1,
  384,  385,  386,  387,  388,   -1,  390,  391,  392,  393,
  394,  395,  396,   -1,  398,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  421,  422,  423,
  424,   -1,   -1,   -1,  428,
  };

#line 1439 "Iril/IR/IR.jay"

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
