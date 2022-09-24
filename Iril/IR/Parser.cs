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
    "ATOMIC","MONOTONIC",
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
#line 63 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 67 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 71 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 75 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 79 "Iril/IR/IR.jay"
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
#line 99 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 103 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 112 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 124 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 19:
#line 128 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-3+yyTop], isConstant: (bool)yyVals[-3+yyTop]);
    }
  break;
case 20:
#line 132 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 136 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 140 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 144 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 24:
#line 148 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 152 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 26:
#line 156 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 27:
#line 160 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 28:
#line 164 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 29:
#line 168 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 30:
#line 172 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 31:
#line 176 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 32:
#line 180 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 33:
#line 184 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 34:
#line 188 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 35:
#line 192 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 36:
#line 193 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 197 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 198 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 199 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 40:
#line 200 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
#line 201 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 42:
#line 202 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 203 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 204 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
#line 205 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 46:
#line 209 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 47:
#line 213 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 48:
  case_48();
  break;
case 49:
  case_49();
  break;
case 50:
#line 230 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 51:
#line 231 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 52:
#line 232 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 53:
#line 236 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 54:
#line 240 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 65:
#line 269 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 273 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 280 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 68:
#line 284 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 69:
#line 288 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 70:
#line 292 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 71:
#line 296 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 90:
#line 330 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 91:
#line 334 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 92:
#line 338 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 93:
#line 345 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 349 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 354 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 99:
#line 360 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 100:
#line 361 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 101:
#line 362 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 102:
#line 363 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 103:
#line 367 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 104:
#line 371 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 375 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 106:
#line 379 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 107:
#line 383 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 387 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 109:
#line 391 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 110:
#line 398 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 402 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 410 "Iril/IR/IR.jay"
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
#line 442 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 126:
#line 446 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 127:
#line 450 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 128:
#line 454 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 129:
#line 458 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 130:
#line 465 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 131:
#line 469 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 132:
#line 473 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 137:
#line 484 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 140:
#line 496 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 500 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 504 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 143:
#line 508 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 144:
#line 512 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 148:
#line 522 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 149:
#line 523 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 150:
#line 530 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 534 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 541 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 545 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 549 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 553 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter ((LocalSymbol)yyVals[0+yyTop], (LType)yyVals[-2+yyTop]);
    }
  break;
case 156:
#line 557 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 157:
#line 561 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 159:
#line 569 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 573 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 161:
#line 574 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 162:
#line 575 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoUndef; }
  break;
case 163:
#line 576 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ImmediateArgument; }
  break;
case 164:
#line 577 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 165:
#line 578 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 166:
#line 579 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 167:
#line 580 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 168:
#line 581 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 169:
#line 582 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 170:
#line 583 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 171:
#line 584 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 172:
#line 585 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 173:
#line 586 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 174:
#line 587 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 175:
#line 591 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 176:
#line 595 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 184:
#line 618 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 185:
#line 619 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 186:
#line 620 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 187:
#line 621 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 188:
#line 622 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 189:
#line 623 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 190:
#line 624 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 191:
#line 625 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 192:
#line 626 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 193:
#line 627 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 194:
#line 631 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 195:
#line 632 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 196:
#line 633 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 197:
#line 634 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 198:
#line 635 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 199:
#line 636 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 200:
#line 637 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 201:
#line 638 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 202:
#line 639 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 203:
#line 640 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 204:
#line 641 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 205:
#line 642 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 206:
#line 643 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 207:
#line 644 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 208:
#line 645 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 209:
#line 646 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 210:
#line 650 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 214:
#line 660 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 215:
#line 664 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 216:
#line 668 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 672 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 676 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 219:
#line 680 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 220:
#line 684 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 221:
#line 688 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 222:
#line 692 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 223:
#line 696 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 225:
#line 704 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 226:
#line 705 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 227:
#line 706 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 228:
#line 707 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 229:
#line 708 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 230:
#line 709 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 231:
#line 710 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 232:
#line 711 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 233:
#line 712 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 234:
#line 719 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 726 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 730 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 237:
#line 737 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 744 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 748 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 243:
#line 770 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 774 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 785 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 789 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 796 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 800 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 807 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 251:
#line 811 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 252:
#line 818 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 822 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 254:
#line 826 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 830 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 256:
#line 837 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 841 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 848 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 852 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 856 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 860 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 868 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 264:
#line 869 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 265:
#line 876 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 880 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 887 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 268:
#line 891 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 269:
#line 895 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 270:
#line 899 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 271:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 272:
#line 907 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 274:
#line 915 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 919 "Iril/IR/IR.jay"
  {
        yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 923 "Iril/IR/IR.jay"
  {
        yyVal = new SymbolValue ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 927 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 278:
#line 931 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 279:
#line 935 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 280:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 288:
#line 959 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 963 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 291:
#line 976 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 980 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 987 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1005 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 300:
#line 1012 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1019 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1023 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1030 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1034 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1042 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1046 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 308:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 310:
#line 1058 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1062 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1066 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1070 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1078 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 315:
#line 1082 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 316:
#line 1086 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 317:
#line 1090 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 318:
#line 1094 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 319:
#line 1098 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1102 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 321:
#line 1106 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 322:
#line 1110 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1114 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 324:
#line 1118 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 325:
#line 1122 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 326:
#line 1126 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 327:
#line 1130 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 328:
#line 1134 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 329:
#line 1138 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 330:
#line 1142 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 331:
#line 1146 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 332:
#line 1150 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 333:
#line 1154 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 334:
#line 1158 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 335:
#line 1162 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 336:
#line 1166 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 337:
#line 1170 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 338:
#line 1174 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 339:
#line 1178 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 340:
#line 1182 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 341:
#line 1186 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1190 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1194 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1198 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1202 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1206 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1210 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1214 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1218 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1222 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1226 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1230 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1234 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1238 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1242 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1246 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1250 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1254 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1258 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 360:
#line 1262 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1266 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: false);
    }
  break;
case 362:
#line 1270 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false, isAtomic: true);
    }
  break;
case 363:
#line 1274 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: false);
    }
  break;
case 364:
#line 1278 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 365:
#line 1282 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-6+yyTop], (TypedValue)yyVals[-4+yyTop], isVolatile: true, isAtomic: true);
    }
  break;
case 366:
#line 1286 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 367:
#line 1290 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 368:
#line 1294 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1298 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1302 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1306 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1310 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 373:
#line 1314 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 374:
#line 1318 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 375:
#line 1322 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 376:
#line 1326 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 377:
#line 1330 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 378:
#line 1334 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 379:
#line 1338 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 380:
#line 1342 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 381:
#line 1346 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 382:
#line 1350 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 383:
#line 1354 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 384:
#line 1358 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 385:
#line 1362 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], isAtomic: false);
    }
  break;
case 386:
#line 1366 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-8+yyTop], (Value)yyVals[-7+yyTop], (Value)yyVals[-4+yyTop], isAtomic: true);
    }
  break;
case 387:
#line 1370 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 388:
#line 1374 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 389:
#line 1378 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 390:
#line 1382 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 391:
#line 1386 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 392:
#line 1390 "Iril/IR/IR.jay"
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
#line 81 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 86 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 91 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.AddGlobalVariable(g);
    }

void case_15()
#line 105 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 114 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_48()
#line 218 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_49()
#line 223 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_114()
#line 415 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_115()
#line 420 "Iril/IR/IR.jay"
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
   13,   13,   52,   52,   52,   52,   52,   52,   52,   52,
   52,   52,   52,   55,   20,   20,   20,   20,   20,   20,
   20,   20,   20,   56,   27,   27,   57,   54,   54,   25,
   58,   58,   53,   53,   59,   60,   60,   36,   36,   61,
   61,   62,   62,   62,   62,   63,   63,   65,   65,   65,
   65,   67,   68,   68,   69,   69,   70,   70,   70,   70,
   70,   70,   71,   71,   71,   71,   71,   71,   71,   71,
   21,   21,   72,   72,   72,   72,   72,   73,   73,   74,
   75,   75,   76,   77,   77,   78,   78,   43,   79,   80,
   64,   64,   81,   81,   81,   81,   81,   81,   81,   82,
   82,   82,   82,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,   66,   66,   66,   66,   66,   66,   66,   66,
   66,   66,
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
    1,    3,    1,    1,    2,    2,    2,    1,    3,    5,
    1,    2,    3,    1,    2,    1,    1,    1,    1,    5,
    1,    3,    2,    7,    2,    2,    7,    1,    1,    8,
    9,    9,   10,    5,    6,   11,    5,    7,    5,    5,
    6,    4,    4,    5,    5,    6,    6,    7,    5,    5,
    6,    6,    6,    7,    5,    6,    7,    7,    8,    6,
    4,    4,    5,    6,    5,    2,    5,    4,    4,    4,
    4,    5,    6,    7,    6,    6,    6,    4,    3,    4,
    7,    8,    8,    9,   10,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,   11,    4,    5,    4,    5,
    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,    0,   96,  107,   99,  100,  101,  102,   98,  130,
   39,   37,   40,   41,   42,   43,   44,   45,  298,  167,
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
  226,  229,  230,  231,  225,  210,  214,  233,  232,    0,
    0,    0,    0,    0,    0,    0,    0,  213,  211,  212,
    0,    0,    0,    0,    0,    0,    0,    0,   48,    0,
    0,    0,   74,   73,   13,    0,    0,   67,   72,  175,
  171,  174,    0,    0,    0,    0,  106,  104,    0,    0,
    0,   88,   87,   79,   77,   78,   80,   81,   82,   83,
    0,   75,  153,    0,  148,    0,    0,    0,    0,    0,
    0,  123,  182,    0,    0,    0,    0,  141,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  238,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   15,    0,    0,    0,   68,   14,    0,  235,  108,
   92,  109,  111,    0,    0,   12,   76,  155,  151,    0,
  119,    0,    0,    0,    0,    0,    0,    0,  308,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  248,  251,    0,
    0,  256,    0,  301,  309,    0,  137,    0,  142,    0,
    0,  143,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  222,    0,    0,    0,  220,  221,    0,    0,    0,
    0,    0,   61,   64,    0,   59,    0,   50,   62,    0,
   56,   58,   63,   60,   51,   52,   49,   17,   16,   71,
   70,   69,   84,  284,    0,  283,    0,  281,    0,    0,
    0,  306,    0,    0,  303,    0,    0,    0,    0,  305,
  296,  297,    0,    0,  294,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  299,  346,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  184,  185,  186,  187,
  188,  189,  190,  191,  192,  193,    0,  194,  195,  206,
  207,  208,  209,  197,  199,  200,  201,  202,  198,  196,
  204,  205,  203,    0,    0,    0,    0,    0,    0,    0,
    0,  114,  249,    0,  257,    0,    0,    0,  124,  144,
   33,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  239,    0,    0,    0,    0,    0,    0,    0,  240,    0,
  287,  285,  286,   89,    0,  120,  250,    0,  302,  234,
    0,    0,  262,    0,    0,    0,    0,    0,    0,  295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  288,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  359,    0,    0,  115,   20,
    0,   28,    0,    0,    0,    0,    0,    0,  223,    0,
    0,    0,    0,    0,   54,    0,   57,  282,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  341,    0,    0,  245,  246,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  360,    0,
    0,    0,    0,  219,  215,  218,    0,   23,    0,    0,
   53,    0,    0,    0,  264,    0,    0,  265,    0,    0,
    0,    0,  314,    0,  343,  384,    0,  352,  369,    0,
  347,  388,    0,  373,  345,  390,  381,  377,    0,    0,
  366,    0,  320,  319,  368,  391,    0,    0,    0,    0,
  317,    0,    0,    0,    0,  224,  237,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  289,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  241,    0,  243,    0,    0,    0,    0,
  291,    0,    0,  267,    0,  263,    0,    0,    0,    0,
    0,  315,  385,  370,  374,  378,  367,  321,  356,  379,
  247,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  355,  344,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  242,  217,    0,
  304,    0,  307,  292,    0,  274,  275,  276,    0,    0,
    0,    0,  273,  269,  268,  266,    0,    0,    0,    0,
  318,    0,    0,    0,    0,  361,    0,  382,    0,    0,
    0,  290,  375,  300,    0,    0,    0,    0,  216,  244,
  293,  271,    0,    0,    0,    0,    0,  310,    0,    0,
    0,  363,    0,    0,  362,  383,    0,    0,    0,  272,
    0,    0,    0,    0,  311,  312,    0,    0,  364,    0,
    0,    0,    0,    0,    0,  313,  365,    0,    0,    0,
    0,    0,    0,  316,  386,    0,  280,  277,  279,    0,
    0,  278,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   60,   12,   13,   14,  241,  216,  208,   61,
   87,  217,  563,   88,   69,  256,   90,  209,  398,  198,
  417,  400,  401,  402,  403,  218,  824,  242,  101,  102,
  155,  156,   15,  119,  169,  357,  257,  252,   75,   65,
   76,   66,   67,   16,  258,  165,  166,  171,  497,  514,
  199,  200,  825,  270,  797,  425,  704,  826,  695,  696,
  358,  359,  360,  361,  362,  363,  564,  663,  757,  758,
  894,  418,  624,  625,  830,  831,  434,  435,  470,  629,
  364,  365,
  };
  protected static readonly short [] yySindex = {          735,
   -7,    6,   -1,   35,   40, 3082, -140, -213,    0,  735,
    0,    0,    0,    0, -121, 3544, -103,  101,  108, 3218,
  -81,  -25,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  155,    0,  166,    0,    0,    0,    0,    0,
  172,    0,    0,  -72,    0,    0, 4836,  -93,  -29,    0,
 -229,  192,  202, 4962, 3272,    0,    0,  -21,   19,  209,
    0,  260, 3617,  -17,    0, 3617,    0,   63,   77,    0,
    0,    0,    0,    0,    0,  122, 4962,  -59, -120,  145,
  -49,  305,  -20,  236,  118, 4962, 4962,    0,    0,  192,
   -5,  202,  120, 4962,  143,  109,    0,   41, 3287,  202,
    0, 4962,  202, 3617,    0,  152,  318, 3807, -118,  -16,
 3617,  260,  -15,    0,    0,    0, 4962, -120, -120,  967,
 4962, -120, 4962, -120,    0,  323,    0, -231,  415,  334,
 4064,  421,  339,  392,    0, 4962, 4962,    4, 4962,    0,
  207,    0,    0,  192,  142,    0,  202,  202,    0,  930,
    0,    0,    0,   89,  181,    0,    0,  147,  -96, -221,
    0,  260,  -14, -118,  260, 1033, 4962, 4962,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -26,
  432,  438,  441, 4988, 5011, 4988,  444,    0,    0,    0,
  967, 4962,  967, 4962,  424,  434,  435,  186,    0, -231,
 4326,    0,    0,    0,    0,  -12,  967,    0,    0,    0,
    0,    0,  192,   45,  428,   -4,    0,    0, 3823,  442,
  459,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -110,    0,    0, 4165,    0, 3928, -214, 4963,  -95, 4988,
  245,    0,    0, -118,  260,  147,  147,    0, -118,  125,
  462,  192, 1687,  467, 4962, 4988, 4988, 4988,    0,   11,
 4859,   20,  -13,  130,  466,  967,  476,  967, 4744, 4778,
 1910,    0, -231,  220,  -11,    0,    0, 4640,    0,    0,
    0,    0,    0,  251, 4811,    0,    0,    0,    0,  264,
    0,  464,  463, 4988, -127, 4988, 3346, 4988,    0, 3405,
 4962, 3405, 4962, 3405, 4962, 4962, 2125, 4962, 4962, 4962,
 3405, 2444, 2763, 4962, 4962, 4962, 4988, 4988, 4988, 4988,
 4988, 4962,  -38, 3361,  191, -183, 1810, 4988, 4988, 4988,
 4988, 4988, 4988, 4988, 4988, 4988, 4988, 4988, 4988,  596,
  974, 4962, 4962, 3346,  105, 4962, 3779,    0,    0, 7504,
 -140,    0, -140,    0,    0, 4963,    0,  213,    0, -118,
  147,    0,  270, -234,  149,  487, 4962,  100,  150,  151,
  153,    0, 4988,  967,   21,    0,    0,  279,  160,  497,
  164,  503,    0,    0,  508,    0,  727,    0,    0,  429,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    5,    0,  242,    0,  213, 7504,
 7993,    0,  284, 1060,    0,  515,  293, 3617, 3617,    0,
    0,    0,  967, 3405,    0,  967,  967, 3405,  967,  967,
 3405,  967,  967, 4962,  967,  967,  967,  967,  967, 3405,
 4962,  967, 4962,  967,  967,  967,  967,  516,  518,  519,
  530,  531,  243, 4962, 3483,  250, 4988,  532,    0,    0,
 4962, 4962, 4962,  286,  194,  195,  196,  197,  200,  201,
  203,  204,  205,  208,  214,  215,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4962,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 4962,   26,  967,  637, 4962, 3617, 3346,
  -35,    0,    0, -140,    0,   19,   19, 3901,    0,    0,
    0,  319,  321,  342,  221,  290, 4988, 4962, 4962, 4962,
    0,  553, -140,  359,  238,  361,  240, 4624,    0, 4778,
    0,    0,    0,    0, 4811,    0,    0, -140,    0,    0,
  576,  355,    0,  587,  293,  293, 3617,  586,  967,    0,
  588,  589,  967,  590,  594,  967,  595,  600,  967,  603,
  604,  605,  606,  607,  967,  967,  608,  967,  609,  610,
  613,  614, 4988, 4988, 4988, 1910, 4988,  382,  324, 4962,
  332, 4962,  615, 4962,  967,  967,  360, 4988, 4962, 4962,
 4962, 4962, 4962, 4962, 4962, 4962, 4962, 4962, 4962, 4962,
  967,  967, 1060,  616,    0,  617,  219,  587,  587,  293,
  293, 4962,  637, 4962, 3617,    0, 4988,   19,    0,    0,
 -140,    0,  406, 4988,  625,  398,  408,  410,    0,   19,
 -140,  413, -140,  416,    0,  280,    0,    0,   19,  355,
  540, 3536,  285,  587,  587,  293, 1060,  630, 1060, 1060,
  631, 1060, 1060,  632, 1060, 1060,  635, 1060, 1060, 1060,
 1060, 1060,  639,  645, 1060,  646, 1060, 1060, 1060, 1060,
    0,  647,  649,    0,    0,  651,  654,  443,  682, 4962,
  377, 4962,  967,  683, 4962,  686,  689,  690, 4988,  692,
  192,  192,  192,  192,  192,  192,  192,  192,  192,  192,
  192,  192,  695,  696,  699,  655, 4988,  485,  147,  147,
  587,  587,  293,  587,  587,  293,  293, 3617,    0,   19,
 -140,  706,  212,    0,    0,    0,   19,    0,   19, -140,
    0,  709, 4962, 4875,    0, 1543,  281,    0,  355,  294,
  367,  587,    0, 1060,    0,    0, 1060,    0,    0, 1060,
    0,    0, 1060,    0,    0,    0,    0,    0, 1060, 1060,
    0, 1060,    0,    0,    0,    0, 4988, 4988, 1910, 1910,
    0,  376,  712, 4962,  719,    0,    0,  386,  722,  391,
 4962, 4962,  728,  212, 1060, 1060, 1060,    0,  729,  730,
  147,  147,  147,  147,  587,  147,  147,  587,  587,  293,
   19,  212, 4988,    0,  308,    0,   19,  355,  732, 4921,
    0,  731,  547,    0, 3169,    0, 4955,  450,  355,  355,
  393,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  651,  521,  400,  -42,  402,  527,  411,  534,  967,
  967,  212,  743,    0,    0,  704, 4988,  529,  147,  147,
  147,  147,  147,  147,  147,  587,  314,    0,    0,  212,
    0,  355,    0,    0, 4710,    0,    0,    0,  418,  752,
  759,  761,    0,    0,    0,    0,  355,  480,  486,  355,
    0,  546,  770,  436,  560,    0,  561,    0,  489,  490,
  743,    0,    0,    0,  147,  147,  147,  147,    0,    0,
    0,    0,  320,  785, 4988, 4988, 4988,    0,  355,  355,
  500,    0,  454,  580,    0,    0,  794,  799,  147,    0,
 4962,  457,  458,  460,    0,    0,  355,  591,    0,  470,
  471,  385, 4962, 4962, 4962,    0,    0,  601,  602, 4988,
  412,  419,  433,    0,    0,  802,    0,    0,    0,  212,
  326,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0, 3683,    0,    0,  853,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  823,    0,    0,    0,    0,    0,
 1398,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3410,  671,  579,    0,    0,    0,    0,    0, 3747,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,    0, 3477,    0, 1223,    0,  582,
    0,    0,  612,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  155,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  155,  155,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  163,    0,    0,  618,  620,    0,    0,
    0,    0,    0,  173,    0,    0,    0,  -94,    0,  -86,
    0,    0,    0,  469,    0,   88,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  155,    0,  155,    0,    0,    0,    0,    0,    0,    0,
    0, 1657,    0,    0,    0,    0,  155,    0,    0,    0,
    0,    0,   31,  155,    0,  155,    0,    0,    0,  851,
 1148,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  344,    0,    0,  -83,    0,    0,    0,
    0,    0,    0,    0,    0,  567,  597,    0,    0,    0,
  688,  211,  155,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  155,    0,  155,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4041,    0, 7607,    0,    0,    0,    0,  -82,    0,    0,
  920,    0,    0,    0,    0,    0,    0,  155,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0, 1068,    0,    0,   28,    0,  155,    0,    0,  345,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  155,    0,    0,    0,  -80,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  155,    0,    0,  155,  155,    0,  155,  155,
    0,  155,  155,    0,  155,  155,  155,  155,  155,    0,
    0,  155,    0,  155,  155,  155,  155,    0,    0,    0,
    0,    0,  155,    0,    0,  155,    0,    0,    0,    0,
    0,    0,    0,  155,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  155,  155,    0,    0,    0,    0,
  155,    0,    0, 4163,    0, 4303, 7710,    0,    0,    0,
    0,    0,    0,    0,    0,  155,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 7813,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  155,    0,
    0,    0,  155,    0,    0,  155,    0,    0,  155,    0,
    0,    0,    0,    0,  155,  155,    0,  155,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  155,    0,
  155,    0,    0,    0,  155,  155,  155,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  155,  155,    0, 5069,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4425,    0,    0,
 1125,    0,    0,    0,    0,  155,  155,  155,    0, 1421,
    0,    0,    0,    0,    0,    0,    0,    0, 7916,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 5175,    0,    0,    0,    0,
  155,    0,  155,    0,    0,    0,    0,    0,    0,    0,
 1763, 1869, 1976, 2082, 2188, 2295, 2401, 2507, 2614, 2720,
 2826, 2933,    0,    0,    0,    0,    0,    0, 5281,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 1464,
 1483,    0,    0,    0,    0,    0, 1494,    0, 1551, 1599,
    0,    0,    0,    0,    0,  155,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5387, 5493, 5599, 5705,    0, 5811,    0,    0,    0,    0,
 1610,    0,    0,    0,    0,    0, 1618,    0,    0,    0,
    0,  351,  155,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 5917,    0,    0,    0,    0,    0,    0,    0,  155,
  155,    0, 6023,    0,    0,    0,    0,    0, 6129, 6235,
 6341, 6447, 6553, 6659, 6765,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 6871,    0,    0,    0, 6977, 7083, 7189, 7295,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 7401,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  155,    0,    0,    0,    0,    0,    0,    0,    0,
  155,  155,  155,    0,    0,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  850,  772,    0,    0,    0,    0,  650,  658,  854,
  210,   -6,  599,   30, -164,   36,    0,  593,  598, -260,
 -542,    0,  330,    0, -711,    0,  374,  640,  767,  117,
    0,  644,    0,  -78,    0,  522, -107, -230,   -3,    0,
  -64,  821,  -57,    0, -202,    0,  643,  -87,    0,    0,
    0,    0, -786, -117,    0, -499, -570,   13,   96,  107,
 -341,  481,    0,  543,  544,  484, -364, 2599,    0,   69,
    0,  352,    0,  182,    0,   82, -228, -159,    0,  282,
    0,  492,
  };
  protected static readonly short [] yyTable = {            62,
  114,  904,   64,  107,  249,  656,  108,   94,  121,   62,
  136,  170,  140,  265,  296,  523,  301,  863,  399,  399,
  406,   58,  109,  109,  109,  109,  248,  366,  116,  104,
  383,  288,  288,  706,  416,  877,  121,  108,  146,  117,
  122,  829,  118,  174,  205,  532,  108,  146,  150,  206,
  100,  369,   59,   17,  383,   89,  372,   62,   62,   20,
  247,   95,  661,  383,  383,   93,   62,  108,   60,   62,
   95,   60,  124,   57,   94,  911,   39,  272,  273,  387,
  130,  386,  253,  438,   57,  441,  108,   34,  292,  143,
  144,  250,  450,  254,  112,   21,  259,  100,  250,  244,
   22,  167,  154,   52,   53,   62,  290,   62,  167,   70,
  124,  164,  287,  409,   62,  128,  623,  124,  829,  145,
  176,  129,   63,  132,  201,  134,  203,   95,  225,  793,
  108,  795,   74,   68,  799,  382,  549,  529,  423,  223,
  224,  108,  226,  537,  533,  542,   93,  207,  371,  230,
   72,   60,  628,  385,  168,   94,   77,  251,  424,  253,
  752,   78,  471,  103,  300,  231,  472,  530,   79,  253,
  262,  263,   80,   81,   82,   83,  370,   68,  116,  116,
  110,  113,  228,  971,   98,  229,  523,  121,  556,  120,
  117,  122,  123,  118,   95,  276,  526,  278,  527,   91,
  664,  665,   95,  112,  167,   96,  112,  232,  233,  234,
   19,   97,   95,  152,  235,  236,  152,  237,  238,  239,
  240,  245,  154,  855,  246,   23,  282,  105,  157,  283,
  158,   80,   81,  108,   24,   82,   83,  173,  135,  164,
  124,  109,  428,   25,   26,   27,   28,   29,   92,  429,
   95,   93,  115,  139,  122,  172,  175,  255,  378,  838,
  408,  286,  286,  283,  384,  731,  732,  551,  734,  117,
  464,   58,  397,  397,  570,   18,   19,  552,  570,  636,
  553,  570,  554,  253,  108,  555,  598,  416,  415,  399,
  570,  108,  116,  602,  416,  127,  519,  131,  133,  118,
   62,  762,   59,  433,  436,  437,  439,  440,  442,  443,
  445,  446,  447,  448,  449,  452,  454,  455,  456,  457,
  751,  836,  125,  555,  837,  463,  466,  108,  881,  608,
  474,  108,  109,  644,   57,  694,  126,  177,  178,  898,
  899,  202,  923,  204,  138,  515,  516,   62,  879,  521,
  518,  880,  195,  264,  919,   34,   34,  880,  141,  638,
  940,  243,   34,  555,  567,  108,  972,  700,  815,  880,
  536,  818,  819,  108,  142,  702,   34,   34,  650,  221,
  108,  111,  921,  196,  154,   55,  147,  154,   55,  903,
  637,  270,  465,  659,  270,   40,   41,  928,   42,   43,
  931,  108,   45,  709,   46,   47,   48,   49,   50,  149,
   51,   34,   80,   81,   39,  194,   82,   83,  108,  151,
  794,   62,   62,  427,  124,  159,  108,  569,  960,  945,
  946,  573,  222,  108,  576,   80,   81,  579,  744,  108,
  160,   58,   85,  585,  586,  104,  588,  956,  745,  108,
  746,  108,  967,  108,  210,  876,  211,  599,  601,  968,
  108,  220,  635,  227,  605,  606,  607,   54,  140,  167,
  517,  266,   59,  969,  108,  212,  740,  267,   19,   19,
  268,  279,   19,   19,   24,   19,  747,  274,  749,  291,
  621,  280,  281,   25,   26,   27,   28,   29,  295,   19,
   19,  368,  294,  373,   57,  374,  377,  622,  388,  389,
  413,   62,   62,   62,  219,  632,  634,   55,   56,  391,
  419,  420,  469,  421,  520,  250,  531,  534,  694,  694,
  535,  646,  647,  648,   19,  543,  538,  539,  544,  540,
  545,  415,  546,  397,  565,  566,  547,  548,  415,  179,
  180,  181,  550,  182,  183,  184,  560,  185,  562,  593,
   62,  594,  595,  124,  186,  187,  145,  269,  269,  269,
  738,  188,  893,  596,  597,  604,  821,  641,  640,  189,
  609,  610,  611,  612,  219,  827,  613,  614,  108,  615,
  616,  617,  823,  701,  618,  703,  147,  703,  642,  643,
  619,  620,  711,  712,  713,  714,  715,  716,  717,  718,
  719,  720,  721,  722,  649,  651,  652,  653,  654,  660,
  423,  811,  812,  367,  416,   62,  662,   62,   62,  667,
  753,  669,  670,  672,  630,  631,  633,  673,  675,  379,
  380,  381,  728,  676,  269,  212,  678,  679,  680,  681,
  682,  685,  687,  688,   24,  756,  689,  690,  705,  726,
  727,  412,  741,   25,   26,   27,   28,   29,  743,  748,
  190,  759,  750,  764,  767,  770,  109,  422,  773,  426,
  839,  430,  779,  666,  191,  192,  193,   18,  780,  782,
  787,  835,  788,  703,  789,  703,  195,  790,  703,  791,
  458,  459,  460,  461,  462,  869,  870,  468,  872,  873,
   95,  475,  476,  477,  478,  479,  480,  481,  482,  483,
  484,  485,  486,  253,  253,  792,  798,  196,  197,  800,
   95,   62,  801,  802,  124,  804,  140,  140,  805,  806,
  140,  140,  807,  140,  810,  623,  397,  833,  733,  822,
  736,  737,  828,  840,  853,  854,  541,  140,  140,  194,
  698,   95,  856,  915,  857,  858,  916,  917,  108,  859,
  885,  862,  867,  868,  261,  882,  897,  901,  902,  900,
  905,  253,  253,  906,  253,  253,  880,  703,  914,  907,
  908,  925,  140,   95,  860,  861,  912,  924,  926,  275,
  927,  277,  932,  179,  180,  181,  929,  182,  183,  184,
  939,  185,  930,  933,  934,  289,  935,  936,  886,  887,
  937,  938,  888,  397,  941,  188,  947,  253,  253,  253,
  756,  124,  948,  189,  145,  145,  949,  950,  145,  145,
  603,  145,  951,  953,  954,  970,  955,  957,  958,  959,
  125,  253,    1,  126,  820,  145,  145,  964,  965,   71,
  285,  376,  137,  170,  147,  147,  170,  284,  147,  147,
  148,  147,  293,   86,  390,  407,  392,  405,  415,  657,
  297,  106,  170,  127,  851,  147,  147,  528,  299,  128,
  145,  129,  920,  179,  180,  181,  852,  182,  183,  184,
  557,  185,  524,  525,  558,  896,  658,  808,  186,  187,
  645,  884,  559,  170,  735,  188,    0,    0,    0,  146,
  147,    0,    0,  189,  889,    0,    0,   95,   95,   95,
    0,   95,   95,   95,  952,   95,    0,    0,  890,  891,
  892,    0,   95,   95,    0,  170,  961,  962,  963,   95,
    0,    0,    0,    0,    0,   18,   18,   95,    0,   18,
   18,    0,   18,    0,    0,    0,  691,  692,  693,    0,
  697,  699,    0,    0,    0,   85,   18,   18,    0,    0,
    0,  710,  289,  179,  180,  181,    0,  182,  183,  184,
    0,  185,  487,  488,  489,  490,  491,  492,  493,  494,
  495,  496,    1,    2,    0,  188,    3,    4,  108,    5,
  739,   18,    0,  189,  190,    0,    0,  742,    0,    0,
    0,    0,  561,    6,    7,    0,  195,    0,  191,  192,
  193,  568,    0,    0,  571,  572,    0,  574,  575,    0,
  577,  578,    0,  580,  581,  582,  583,  584,   95,    0,
  587,    0,  589,  590,  591,  592,    0,  196,    8,  627,
    0,    0,   95,   95,   95,    0,    0,   26,    0,    0,
    0,    0,    0,    0,  108,    0,  260,    0,    0,  170,
  170,  170,  803,  170,  170,  170,  170,  170,    0,  194,
    0,    0,  195,   95,  170,  170,    0,    0,    0,    0,
  809,  170,    0,    0,  170,  170,  170,  170,  170,  170,
   85,    0,    0,    0,  626,  170,    0,    0,    0,  195,
    0,    0,    0,  196,   31,    0,   85,    0,    0,  170,
  170,    0,  170,  170,    0,    0,  170,    0,  170,  170,
  170,  170,  170,    0,  170,    0,    0,    0,    0,    0,
  196,    0,    0,    0,    0,  194,    0,    0,    0,    0,
  849,  850,    0,    0,    0,    0,    0,  668,   85,   85,
   85,  671,    0,    0,  674,   85,   85,  677,   85,   85,
   85,   85,  194,  683,  684,    0,  686,  146,  146,  230,
    0,  146,  146,    0,  146,    0,  878,    0,    0,    0,
  170,  170,    0,  707,  708,  231,    0,    0,  146,  146,
    0,    0,    0,    0,  170,  170,  170,    0,    0,  723,
  724,  725,  105,  179,  180,  181,    0,  182,  183,  184,
    0,  185,    0,    0,  498,  499,    0,    0,  186,  187,
  913,    0,    0,  146,    0,  188,    0,  232,  233,  234,
    0,  170,  170,  189,  235,  236,    0,  237,  238,  239,
  240,    0,  105,  105,  105,  763,  105,  765,  766,    0,
  768,  769,   86,  771,  772,    0,  774,  775,  776,  777,
  778,    0,  105,  781,  105,  783,  784,  785,  786,  179,
  180,  181,    0,  182,  183,  184,    0,  185,  942,  943,
  944,  796,    0,    0,  186,  187,    0,    0,    0,    0,
    0,  188,    0,  105,    0,  105,  179,  180,  181,  189,
  182,  183,  184,    0,  185,    0,    0,    0,    0,    0,
    0,  186,  187,  966,    0,   26,   26,    0,  188,   26,
   26,    0,   26,    0,  190,  105,  189,  105,    0,    0,
    0,    0,    0,    0,  834,    0,   26,   26,  191,  192,
  193,    0,  842,    0,    0,  843,    0,    0,  844,    0,
    0,  845,  500,  501,  502,  503,    0,  846,  847,    0,
  848,  504,  505,  506,  507,  508,  509,  510,  511,  512,
  513,   26,   31,   31,    0,    0,   31,   31,    0,   31,
    0,    0,    0,  864,  865,  866,    0,   86,    0,    0,
  190,    0,    0,   31,   31,    0,    0,    0,    0,    0,
   21,    0,    0,   86,  191,  192,  193,    0,    0,    0,
    0,    0,    0,  895,    0,    0,    0,  190,  173,    0,
    0,  173,    0,    0,    0,    0,    0,    0,   31,    0,
    0,  191,  192,  193,    0,    0,    0,  173,  909,  910,
    0,    0,    0,   32,    0,   86,   86,   86,    0,    0,
    0,    0,   86,   86,    0,   86,   86,   86,   86,  105,
  105,  105,   29,  105,  105,  105,    0,  105,  173,    0,
  105,  105,    0,   24,  105,  105,  105,  105,  105,    0,
    0,  105,    0,    0,    0,    0,    0,    0,    0,  105,
    0,  105,  105,    0,    0,  105,    0,    0,    0,    0,
  173,    0,    0,    0,    0,    0,    0,    0,    0,  105,
  105,    0,  105,  105,    0,    0,  105,  105,  105,  105,
  105,  105,  105,    0,  105,    0,  105,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,  105,  105,  105,
    0,  105,  105,    0,    0,    0,  105,    0,  105,    0,
    0,  105,  105,  105,  105,  105,  105,  105,  105,  105,
  105,    0,  105,  105,  108,  105,  105,  105,  105,  105,
  105,  105,  105,  105,  105,  105,  105,  105,   25,  105,
  105,    0,  195,    0,  105,  105,  105,  105,  105,   30,
  105,  105,  105,  105,  105,  105,  105,   27,  105,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  105,
    0,    0,    0,  196,    0,    0,    0,    0,    0,    0,
  105,  105,  105,  105,    0,  105,    0,  105,  105,    0,
    0,  105,  105,    0,  173,  173,  173,    0,  173,  173,
  173,  173,  173,    0,    0,  194,    0,    0,    0,  173,
  173,    0,    0,    0,    0,    0,  173,    0,    0,  173,
  173,  173,  173,  173,  173,    0,    0,    0,   21,   21,
  173,    0,   21,   21,    0,   21,   96,  236,    0,    0,
  236,    0,    0,    0,  173,  173,    0,  173,  173,   21,
   21,  173,    0,  173,  173,  173,  173,  173,  236,  173,
    0,    0,    0,    0,    0,    0,    0,    0,  108,    0,
  375,   32,   32,    0,    0,   32,   32,    0,   32,    0,
    0,    0,    0,    0,   21,    0,  195,    0,    0,  236,
   29,   29,   32,   32,   29,   29,    0,   29,    0,    0,
    0,   24,   24,    0,    0,   24,   24,    0,   24,    0,
    0,   29,   29,    0,    0,  173,  173,  196,    0,  236,
    0,  236,   24,   24,    0,    0,    0,   32,    0,  173,
  173,  173,    0,    0,    0,    0,    0,    0,    0,  179,
  180,  181,   95,  182,  183,  184,   29,  185,    0,  194,
    0,    0,    0,    0,  186,  187,    0,   24,   22,   22,
    0,  188,   22,   22,    0,   22,  173,  173,    0,  189,
    0,    0,    0,    0,    0,  111,    0,    0,    0,   22,
   22,    0,    0,    0,    0,    0,    0,    0,    0,   40,
   41,    0,   42,   43,    0,    0,   45,    0,   46,   47,
   48,   49,   50,    0,   51,    0,   25,   25,    0,   58,
   25,   25,    0,   25,   22,    0,    0,   30,   30,    0,
    0,   30,   30,    0,   30,   27,   27,   25,   25,   27,
   27,    0,   27,    0,    0,    0,    0,    0,   30,   30,
   59,    0,    0,    0,    0,    0,   27,   27,   95,    0,
    0,    0,    0,  236,    0,    0,    0,    0,    0,    0,
  190,   54,   25,    0,    0,    0,    0,    0,    0,  236,
  236,    0,   57,   30,  191,  192,  193,    0,    0,    0,
    0,   27,    0,  179,  180,  181,    0,  182,  183,  184,
    0,  185,    0,    0,    0,    0,    0,    0,  186,  187,
    0,    0,    0,    0,    0,  188,    0,    0,    0,    0,
    0,   55,   56,  189,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  236,  236,  236,    0,  236,  236,    0,    0,    0,
  236,    0,  236,    0,    0,  236,  236,  236,  236,  236,
  236,  236,  236,  236,  236,   95,  236,  236,    0,  236,
  236,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,    0,  236,  236,  387,  387,    0,  236,  236,
  236,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,    0,  236,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  236,  190,    0,    0,    0,    0,    0,
    0,    0,    0,   23,  236,  236,  236,  236,  191,  192,
  193,  236,   24,    0,    0,    0,    0,    0,    0,    0,
    0,   25,   26,   27,   28,   29,    0,  387,  387,  387,
    0,  387,  387,    0,    0,    0,  387,    0,  387,    0,
    0,  387,  387,  387,  387,  387,  387,  387,  387,  387,
  387,   95,  387,  387,    0,  387,  387,  387,  387,  387,
  387,  387,  387,  387,  387,  387,  387,  387,    0,  387,
  387,  392,  392,    0,  387,  387,  387,  387,  387,    0,
  387,  387,  387,  387,  387,  387,  387,    0,  387,    0,
    0,    0,    0,    0,    0,    0,  179,  180,  181,  387,
  182,  183,  184,    0,  185,    0,    0,    0,    0,    0,
  387,  387,  387,  387,   58,    0,    0,  387,  188,  473,
    0,    0,    0,    0,    0,    0,  189,    0,    0,    0,
    0,    0,    0,  392,  392,  392,    0,  392,  392,    0,
    0,    0,  392,    0,  392,   59,    0,  392,  392,  392,
  392,  392,  392,  392,  392,  392,  392,   95,  392,  392,
    0,  392,  392,  392,  392,  392,  392,  392,  392,  392,
  392,  392,  392,  392,    0,  392,  392,   57,  376,  376,
  392,  392,  392,  392,  392,    0,  392,  392,  392,  392,
  392,  392,  392,    0,  392,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  392,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  392,  392,  392,  392,
    0,    0,    0,  392,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  376,  376,  376,    0,  376,  376,    0,    0,    0,  376,
    0,  376,    0,    0,  376,  376,  376,  376,  376,  376,
  376,  376,  376,  376,   95,  376,  376,    0,  376,  376,
  376,  376,  376,  376,  376,  376,  376,  376,  376,  376,
  376,    0,  376,  376,  351,  351,    0,  376,  376,  376,
  376,  376,    0,  376,  376,  376,  376,  376,  376,  376,
    0,  376,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  376,    0,    0,    0,    0,    0,   23,    0,
    0,    0,    0,  376,  376,  376,  376,   24,    0,    0,
  376,    0,    0,    0,    0,    0,   25,   26,   27,   28,
   29,    0,    0,    0,    0,    0,  351,  351,  351,    0,
  351,  351,    0,    0,    0,  351,    0,  351,    0,    0,
  351,  351,  351,  351,  351,  351,  351,  351,  351,  351,
   95,  351,  351,    0,  351,  351,  351,  351,  351,  351,
  351,  351,  351,  351,  351,  351,  351,    0,  351,  351,
  348,  348,    0,  351,  351,  351,  351,  351,    0,  351,
  351,  351,  351,  351,  351,  351,    0,  351,    0,    0,
    0,    0,    0,    0,    0,    0,  444,    0,  351,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  351,
  351,  351,  351,   58,    0,    0,  351,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  348,  348,  348,    0,  348,  348,    0,    0,
    0,  348,    0,  348,   59,    0,  348,  348,  348,  348,
  348,  348,  348,  348,  348,  348,   95,  348,  348,    0,
  348,  348,  348,  348,  348,  348,  348,  348,  348,  348,
  348,  348,  348,    0,  348,  348,   57,  349,  349,  348,
  348,  348,  348,  348,    0,  348,  348,  348,  348,  348,
  348,  348,    0,  348,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  348,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  348,  348,  348,  348,    0,
    0,    0,  348,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  349,
  349,  349,    0,  349,  349,    0,    0,    0,  349,    0,
  349,    0,    0,  349,  349,  349,  349,  349,  349,  349,
  349,  349,  349,   95,  349,  349,    0,  349,  349,  349,
  349,  349,  349,  349,  349,  349,  349,  349,  349,  349,
    0,  349,  349,  350,  350,    0,  349,  349,  349,  349,
  349,    0,  349,  349,  349,  349,  349,  349,  349,    0,
  349,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  349,    0,    0,    0,    0,    0,   23,    0,    0,
    0,    0,  349,  349,  349,  349,   24,    0,    0,  349,
    0,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,  350,  350,  350,    0,  350,
  350,    0,    0,    0,  350,    0,  350,    0,    0,  350,
  350,  350,  350,  350,  350,  350,  350,  350,  350,   95,
  350,  350,    0,  350,  350,  350,  350,  350,  350,  350,
  350,  350,  350,  350,  350,  350,    0,  350,  350,  389,
  389,    0,  350,  350,  350,  350,  350,    0,  350,  350,
  350,  350,  350,  350,  350,    0,  350,    0,    0,    0,
    0,    0,    0,    0,    0,  451,    0,  350,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  350,  350,
  350,  350,   58,    0,    0,  350,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  389,  389,  389,    0,  389,  389,    0,    0,    0,
  389,    0,  389,   59,    0,  389,  389,  389,  389,  389,
  389,  389,  389,  389,  389,   95,  389,  389,    0,  389,
  389,  389,  389,  389,  389,  389,  389,  389,  389,  389,
  389,  389,    0,  389,  389,   57,  380,  380,  389,  389,
  389,  389,  389,    0,  389,  389,  389,  389,  389,  389,
  389,    0,  389,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  389,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  389,  389,  389,  389,    0,    0,
    0,  389,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  380,  380,
  380,    0,  380,  380,    0,    0,    0,  380,    0,  380,
    0,    0,  380,  380,  380,  380,  380,  380,  380,  380,
  380,  380,   95,  380,  380,    0,  380,  380,  380,  380,
  380,  380,  380,  380,  380,  380,  380,  380,  380,    0,
  380,  380,  372,  372,    0,  380,  380,  380,  380,  380,
    0,  380,  380,  380,  380,  380,  380,  380,    0,  380,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  380,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,  380,  380,  380,  380,   24,    0,    0,  380,    0,
    0,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,    0,    0,    0,  372,  372,  372,    0,  372,  372,
    0,    0,    0,  372,    0,  372,    0,    0,  372,  372,
  372,  372,  372,  372,  372,  372,  372,  372,    0,  372,
  372,    0,  372,  372,  372,  372,  372,  372,  372,  372,
  372,  372,  372,  372,  372,    0,  372,  372,  358,  358,
    0,  372,  372,  372,  372,  372,    0,  372,  372,  372,
  372,  372,  372,  372,    0,  372,    0,    0,    0,    0,
    0,    0,    0,    0,  453,    0,  372,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  372,  372,  372,
  372,   58,    0,    0,  372,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  358,  358,  358,    0,  358,  358,    0,    0,    0,  358,
    0,  358,   59,    0,  358,  358,  358,  358,  358,  358,
  358,  358,  358,  358,    0,  358,  358,    0,  358,  358,
  358,  358,  358,  358,  358,  358,  358,  358,  358,  358,
  358,    0,  358,  358,   57,  322,  322,  358,  358,  358,
  358,  358,    0,  358,  358,  358,  358,  358,  358,  358,
    0,  358,    0,    0,    0,    0,  729,  730,  195,    0,
    0,    0,  358,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  358,  358,  358,  358,    0,    0,    0,
  358,    0,    0,    0,    0,    0,    0,    0,    0,  196,
    0,    0,  760,  761,    0,    0,    0,  322,  322,  322,
    0,  322,  322,    0,    0,    0,  322,    0,  322,    0,
    0,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  194,  322,  322,    0,  322,  322,  322,  322,  322,
  322,  322,  322,  322,  322,  322,  322,  322,    0,  322,
  322,    0,    0,    0,  322,  322,  322,  322,  322,    0,
  322,  322,  322,  322,  322,  322,  322,  153,  322,  813,
  814,   58,  816,  817,    0,    0,    0,    0,    0,  322,
    0,    0,    0,    0,    0,   23,   58,    0,    0,    0,
  322,  322,  322,  322,   24,    0,    0,  322,    0,    0,
  841,    0,   59,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,   30,    0,    0,   59,    0,   31,
   32,   33,   34,   35,   36,   37,   38,   39,   40,   41,
    0,   42,   43,   44,   57,   45,    0,   46,   47,   48,
   49,   50,    0,   51,    0,   58,    0,    0,    0,   57,
    0,    0,    0,  871,   52,   53,  874,  875,    0,    0,
   58,    0,    0,    0,    0,  179,  180,  181,    0,  182,
  183,  184,    0,  185,    0,    0,   59,    0,    0,    0,
  186,  187,    0,    0,    0,    0,    0,  188,    0,    0,
    0,   59,    0,    0,    0,  189,    0,    0,    0,    0,
   54,  111,    0,    0,   58,    0,    0,    0,   57,  133,
    0,    0,    0,    0,  918,   40,   41,    0,   42,   43,
    0,    0,   45,   57,   46,   47,   48,   49,   50,    0,
   51,    0,    0,    0,    0,   59,    0,    0,    0,    0,
  133,    0,    0,    0,    0,    0,    0,    0,   80,   81,
   55,   56,   82,   83,   84,   31,   32,   33,   34,   35,
   36,   37,   38,    0,    0,    0,    0,   57,    0,   44,
    0,    0,  133,    0,    0,   23,  134,    0,   85,    0,
    0,    0,   58,    0,   24,    0,  190,   54,    0,    0,
   23,    0,    0,   25,   26,   27,   28,   29,    0,   24,
  191,  192,  193,    0,  111,    0,    0,  134,   25,   26,
   27,   28,   29,   59,    0,    0,  755,    0,   40,   41,
  152,   42,   43,    0,    0,   45,    0,   46,   47,   48,
   49,   50,   85,   51,    0,   58,    0,   55,   56,  134,
    0,    0,    0,   58,    0,   57,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,    0,   24,    0,
    0,    0,    0,    0,  212,    0,   59,   25,   26,   27,
   28,   29,    0,   24,   59,    0,    0,    0,  111,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,    0,
   54,   39,   40,   41,    0,   42,   43,    0,   57,   45,
    0,   46,   47,   48,   49,   50,   57,   51,   23,  467,
    0,    0,    0,  133,    0,    0,   58,   24,    0,    0,
    0,    0,  133,    0,    0,    0,   25,   26,   27,   28,
   29,  133,  133,  133,  133,  133,    0,    0,    0,    0,
   55,   56,  133,    0,    0,    0,    0,   59,    0,    0,
    0,    0,    0,    0,    0,    0,  133,  133,    0,  133,
  133,    0,    0,  133,   54,  133,  133,  133,  133,  133,
  133,  133,    0,    0,    0,    0,    0,    0,    0,   57,
  134,    0,  138,    0,    0,    0,   23,    0,    0,  134,
    0,  431,  432,    0,    0,   24,    0,    0,  134,  134,
  134,  134,  134,    0,   25,   26,   27,   28,   29,  134,
    0,    0,    0,  138,   55,   56,    0,    0,    0,    0,
    0,    0,    0,  134,  134,    0,  134,  134,  133,    0,
  134,  600,  134,  134,  134,  134,  134,  134,  134,   23,
    0,    0,    0,    0,    0,  138,  139,   23,   24,    0,
    0,    0,    0,  754,    0,    0,   24,   25,   26,   27,
   28,   29,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,   73,  139,  133,  133,
    0,    0,    0,    0,    0,    0,    0,  163,    0,    0,
   40,   41,    0,   42,   43,  134,    0,   45,    0,   46,
   47,   48,   49,   50,    0,   51,   58,    0,    0,  139,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,   58,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,    0,    0,    0,    0,   59,   25,   26,
   27,   28,   29,  522,    0,  134,  134,    0,    0,  111,
    0,    0,    0,   59,    0,    0,    0,    0,    0,    0,
    0,    0,   54,   40,   41,    0,   42,   43,    0,   57,
   45,    0,   46,   47,   48,   49,   50,    0,   51,    0,
    0,    0,    0,    0,    0,   57,  138,    0,    0,    0,
    0,    0,    0,    0,    0,  138,    0,    0,    0,    0,
    0,    0,    0,    0,  138,  138,  138,  138,  138,    0,
    0,    0,   55,   56,    0,  138,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   58,    0,  138,
  138,    0,  138,  138,    0,   54,  138,    0,  138,  138,
  138,  138,  138,    0,  138,    0,    0,    0,    0,    0,
  139,    0,    0,    0,    0,    0,    0,    0,   59,  139,
    0,    0,    0,    0,    0,  639,    0,    0,  139,  139,
  139,  139,  139,    0,    0,  302,    0,    0,    0,  139,
    0,    0,    0,    0,    0,   55,   56,    0,    0,    0,
   57,  303,    0,  139,  139,    0,  139,  139,    0,    0,
  139,  138,  139,  139,  139,  139,  139,    0,  139,    0,
   23,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,    0,  161,    0,   23,    0,   25,   26,
   27,   28,   29,    0,    0,   24,    0,    0,    0,    0,
  162,    0,    0,    0,   25,   26,   27,   28,   29,    0,
    0,  138,  138,  304,  305,  306,  152,  307,  308,    0,
    0,    0,  309,   58,  310,  139,    0,  311,  312,  313,
  314,  315,  316,  317,  318,  319,  320,    0,  321,  322,
    0,  323,  324,  325,  326,  327,  328,  329,  330,  331,
  332,  333,  334,  335,   59,  336,  337,  302,    0,    0,
  338,  339,  340,  341,  342,  254,  343,  344,  345,  346,
  347,  348,  349,  303,  350,  139,  139,    0,    0,    0,
    0,    0,    0,    0,    0,  351,   57,    0,  215,    0,
    0,   23,    0,    0,    0,    0,  352,  353,  354,  355,
   24,    0,    0,  356,    0,  161,    0,    0,    0,   25,
   26,   27,   28,   29,    0,    0,    0,    0,    0,    0,
    0,  162,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,  305,  306,    0,  307,
  308,    0,    0,    0,  309,    0,  310,    0,    0,  311,
  312,  313,  314,  315,  316,  317,  318,  319,  320,    0,
  321,  322,    0,  323,  324,  325,  326,  327,  328,  329,
  330,  331,  332,  333,  334,  335,    0,  336,  337,    0,
    0,    0,  338,  339,  340,  341,  342,  252,  343,  344,
  345,  346,  347,  348,  349,    0,  350,  254,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  351,    0,    0,
    0,    0,    0,  254,    0,    0,    0,    0,  352,  353,
  354,  355,    0,    0,    0,  356,    0,  212,  213,    0,
    0,    0,    0,    0,    0,    0,   24,  214,    0,    0,
    0,    0,    0,    0,    0,   25,   26,   27,   28,   29,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  254,  254,  254,    0,  254,
  254,    0,    0,    0,  254,   58,  254,    0,    0,  254,
  254,  254,  254,  254,  254,  254,  254,  254,  254,    0,
  254,  254,    0,  254,  254,  254,  254,  254,  254,  254,
  254,  254,  254,  254,  254,  254,   59,  254,  254,  252,
    0,    0,  254,  254,  254,  254,  254,  255,  254,  254,
  254,  254,  254,  254,  254,  252,  254,  298,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  254,   57,    0,
    0,    0,    0,    0,    0,    0,    0,  111,  254,  254,
  254,  254,    0,    0,    0,  254,    0,    0,    0,    0,
    0,   40,   41,    0,   42,   43,    0,    0,   45,    0,
   46,   47,   48,   49,   50,    0,   51,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  252,  252,  252,
    0,  252,  252,    0,    0,    0,  252,    0,  252,    0,
    0,  252,  252,  252,  252,  252,  252,  252,  252,  252,
  252,    0,  252,  252,    0,  252,  252,  252,  252,  252,
  252,  252,  252,  252,  252,  252,  252,  252,    0,  252,
  252,    0,    0,   54,  252,  252,  252,  252,  252,  253,
  252,  252,  252,  252,  252,  252,  252,    0,  252,  255,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  252,
    0,    0,    0,    0,    0,  255,    0,    0,    0,    0,
  252,  252,  252,  252,    0,    0,    0,  252,    0,  212,
  213,    0,    0,   55,   56,    0,    0,    0,   24,  214,
    0,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  255,  255,  255,
    0,  255,  255,    0,    0,    0,  255,    0,  255,    0,
    0,  255,  255,  255,  255,  255,  255,  255,  255,  255,
  255,    0,  255,  255,  655,  255,  255,  255,  255,  255,
  255,  255,  255,  255,  255,  255,  255,  255,    0,  255,
  255,  253,    0,   58,  255,  255,  255,  255,  255,    0,
  255,  255,  255,  255,  255,  255,  255,  253,  255,   58,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  255,
    0,    0,    0,    0,   59,    0,    0,    0,    0,    0,
  255,  255,  255,  255,    0,    0,    0,  255,    0,    0,
   59,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   57,    0,    0,    0,
  922,    0,    0,    0,    0,    0,    0,    0,    0,  253,
  253,  253,   57,  253,  253,    0,    0,    0,  253,   58,
  253,    0,    0,  253,  253,  253,  253,  253,  253,  253,
  253,  253,  253,    0,  253,  253,    0,  253,  253,  253,
  253,  253,  253,  253,  253,  253,  253,  253,  253,  253,
   59,  253,  253,   58,    0,    0,  253,  253,  253,  253,
  253,    0,  253,  253,  253,  253,  253,  253,  253,    0,
  253,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  253,   57,    0,   59,    0,    0,   58,    0,    0,
    0,    0,  253,  253,  253,  253,    0,    0,    0,  253,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   57,    0,   59,    0,
   58,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  179,  180,  181,    0,  182,  183,  184,   23,  185,    0,
    0,    0,    0,    0,    0,   58,   24,    0,    0,  414,
   57,   59,  188,  212,  410,   25,   26,   27,   28,   29,
  189,    0,   24,  411,    0,    0,    0,    0,   58,    0,
    0,   25,   26,   27,   28,   29,   59,    0,    0,    0,
    0,    0,    0,   57,   58,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   59,
    0,    0,    0,    0,    0,    0,    0,    0,   57,    0,
   99,    0,    0,    0,    0,   59,  179,  180,  181,    0,
  182,  183,  184,   23,  185,    0,    0,    0,    0,    0,
   58,   57,   24,   99,    0,  414,    0,    0,  188,    0,
    0,   25,   26,   27,   28,   29,  189,   57,    0,    0,
  179,  180,  181,  393,  182,  183,  184,   23,  394,    0,
    0,   59,    0,  883,   58,    0,   24,  395,    0,  396,
    0,   58,  188,    0,    0,   25,   26,   27,   28,   29,
  189,    0,    0,    0,  179,  180,  181,  393,  182,  183,
  184,   23,  394,   57,    0,   59,    0,   58,    0,    0,
   24,  404,   59,  396,    0,    0,  188,    0,    0,   25,
   26,   27,   28,   29,  189,    0,    0,  179,  180,  181,
   58,  182,  183,  184,   23,  185,    0,   57,   59,    0,
    0,    0,    0,   24,   57,    0,  414,    0,    0,  188,
    0,    0,   25,   26,   27,   28,   29,  189,    0,   23,
    0,   59,    0,    0,    0,    0,    0,    0,   24,    0,
   57,    0,    0,    0,    0,    0,    0,   25,   26,   27,
   28,   29,  212,    0,    0,    0,    0,    0,    0,    0,
    0,   24,    0,  271,    0,    0,    0,    0,   23,    0,
   25,   26,   27,   28,   29,    0,    0,   24,  832,    0,
    0,    0,    0,    0,    0,    0,   25,   26,   27,   28,
   29,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   23,  302,
    0,    0,    0,    0,    0,   23,    0,   24,    0,    0,
    0,    0,  754,    0,   24,  303,   25,   26,   27,   28,
   29,    0,    0,   25,   26,   27,   28,   29,    0,    0,
    0,  212,    0,    0,    0,    0,    0,    0,    0,    0,
   24,    0,    0,    0,    0,    0,    0,    0,    0,   25,
   26,   27,   28,   29,  212,    0,    0,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,    0,    0,    0,
    0,    0,   25,   26,   27,   28,   29,  304,  305,  306,
    0,  307,  308,    0,    0,    0,  309,    0,  310,    0,
    0,  311,  312,  313,  314,  315,  316,  317,  318,  319,
  320,    0,  321,  322,    0,  323,  324,  325,  326,  327,
  328,  329,  330,  331,  332,  333,  334,  335,    0,  336,
  337,  371,  371,    0,  338,  339,  340,  341,  342,    0,
  343,  344,  345,  346,  347,  348,  349,    0,  350,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  351,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  352,  353,  354,  355,    0,    0,    0,  356,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  371,  371,  371,    0,  371,  371,    0,
    0,    0,  371,    0,  371,    0,    0,  371,  371,  371,
  371,  371,  371,  371,  371,  371,  371,    0,  371,  371,
    0,  371,  371,  371,  371,  371,  371,  371,  371,  371,
  371,  371,  371,  371,    0,  371,  371,  342,  342,    0,
  371,  371,  371,  371,  371,    0,  371,  371,  371,  371,
  371,  371,  371,    0,  371,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  371,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  371,  371,  371,  371,
    0,    0,    0,  371,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  342,
  342,  342,    0,  342,  342,    0,    0,    0,  342,    0,
  342,    0,    0,  342,  342,  342,  342,  342,  342,  342,
  342,  342,  342,    0,  342,  342,    0,  342,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,  342,
    0,  342,  342,  323,  323,    0,  342,  342,  342,  342,
  342,    0,  342,  342,  342,  342,  342,  342,  342,    0,
  342,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  342,  342,  342,  342,    0,    0,    0,  342,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  323,  323,  323,    0,  323,
  323,    0,    0,    0,  323,    0,  323,    0,    0,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  323,    0,
  323,  323,    0,  323,  323,  323,  323,  323,  323,  323,
  323,  323,  323,  323,  323,  323,    0,  323,  323,  329,
  329,    0,  323,  323,  323,  323,  323,    0,  323,  323,
  323,  323,  323,  323,  323,    0,  323,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  323,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  323,  323,
  323,  323,    0,    0,    0,  323,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  329,  329,  329,    0,  329,  329,    0,    0,    0,
  329,    0,  329,    0,    0,  329,  329,  329,  329,  329,
  329,  329,  329,  329,  329,    0,  329,  329,    0,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  329,  329,
  329,  329,    0,  329,  329,  324,  324,    0,  329,  329,
  329,  329,  329,    0,  329,  329,  329,  329,  329,  329,
  329,    0,  329,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  329,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  329,  329,  329,  329,    0,    0,
    0,  329,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  324,  324,  324,
    0,  324,  324,    0,    0,    0,  324,    0,  324,    0,
    0,  324,  324,  324,  324,  324,  324,  324,  324,  324,
  324,    0,  324,  324,    0,  324,  324,  324,  324,  324,
  324,  324,  324,  324,  324,  324,  324,  324,    0,  324,
  324,  330,  330,    0,  324,  324,  324,  324,  324,    0,
  324,  324,  324,  324,  324,  324,  324,    0,  324,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  324,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  324,  324,  324,  324,    0,    0,    0,  324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  330,  330,  330,    0,  330,  330,    0,
    0,    0,  330,    0,  330,    0,    0,  330,  330,  330,
  330,  330,  330,  330,  330,  330,  330,    0,  330,  330,
    0,  330,  330,  330,  330,  330,  330,  330,  330,  330,
  330,  330,  330,  330,    0,  330,  330,  325,  325,    0,
  330,  330,  330,  330,  330,    0,  330,  330,  330,  330,
  330,  330,  330,    0,  330,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  330,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  330,  330,  330,  330,
    0,    0,    0,  330,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  325,
  325,  325,    0,  325,  325,    0,    0,    0,  325,    0,
  325,    0,    0,  325,  325,  325,  325,  325,  325,  325,
  325,  325,  325,    0,  325,  325,    0,  325,  325,  325,
  325,  325,  325,  325,  325,  325,  325,  325,  325,  325,
    0,  325,  325,  335,  335,    0,  325,  325,  325,  325,
  325,    0,  325,  325,  325,  325,  325,  325,  325,    0,
  325,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  325,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  325,  325,  325,  325,    0,    0,    0,  325,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  335,  335,  335,    0,  335,
  335,    0,    0,    0,  335,    0,  335,    0,    0,  335,
  335,  335,  335,  335,  335,  335,  335,  335,  335,    0,
  335,  335,    0,  335,  335,  335,  335,  335,  335,  335,
  335,  335,  335,  335,  335,  335,    0,  335,  335,  357,
  357,    0,  335,  335,  335,  335,  335,    0,  335,  335,
  335,  335,  335,  335,  335,    0,  335,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  335,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  335,  335,
  335,  335,    0,    0,    0,  335,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  357,  357,  357,    0,  357,  357,    0,    0,    0,
  357,    0,  357,    0,    0,  357,  357,  357,  357,  357,
  357,  357,  357,  357,  357,    0,  357,  357,    0,  357,
  357,  357,  357,  357,  357,  357,  357,  357,  357,  357,
  357,  357,    0,  357,  357,  353,  353,    0,  357,  357,
  357,  357,  357,    0,  357,  357,  357,  357,  357,  357,
  357,    0,  357,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  357,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  357,  357,  357,  357,    0,    0,
    0,  357,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  353,  353,  353,
    0,  353,  353,    0,    0,    0,  353,    0,  353,    0,
    0,  353,  353,  353,  353,  353,  353,  353,  353,  353,
  353,    0,  353,  353,    0,  353,  353,  353,  353,  353,
  353,  353,  353,  353,  353,  353,  353,  353,    0,  353,
  353,  331,  331,    0,  353,  353,  353,  353,  353,    0,
  353,  353,  353,  353,  353,  353,  353,    0,  353,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  353,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  353,  353,  353,  353,    0,    0,    0,  353,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  331,  331,  331,    0,  331,  331,    0,
    0,    0,  331,    0,  331,    0,    0,  331,  331,  331,
  331,  331,  331,  331,  331,  331,  331,    0,  331,  331,
    0,  331,  331,  331,  331,  331,  331,  331,  331,  331,
  331,  331,  331,  331,    0,  331,  331,  326,  326,    0,
  331,  331,  331,  331,  331,    0,  331,  331,  331,  331,
  331,  331,  331,    0,  331,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  331,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  331,  331,  331,  331,
    0,    0,    0,  331,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  326,
  326,  326,    0,  326,  326,    0,    0,    0,  326,    0,
  326,    0,    0,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  326,    0,  326,  326,    0,  326,  326,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  326,  326,
    0,  326,  326,  327,  327,    0,  326,  326,  326,  326,
  326,    0,  326,  326,  326,  326,  326,  326,  326,    0,
  326,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  326,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  326,  326,  326,  326,    0,    0,    0,  326,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  327,  327,  327,    0,  327,
  327,    0,    0,    0,  327,    0,  327,    0,    0,  327,
  327,  327,  327,  327,  327,  327,  327,  327,  327,    0,
  327,  327,    0,  327,  327,  327,  327,  327,  327,  327,
  327,  327,  327,  327,  327,  327,    0,  327,  327,  332,
  332,    0,  327,  327,  327,  327,  327,    0,  327,  327,
  327,  327,  327,  327,  327,    0,  327,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  327,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  327,  327,
  327,  327,    0,    0,    0,  327,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  332,  332,  332,    0,  332,  332,    0,    0,    0,
  332,    0,  332,    0,    0,  332,  332,  332,  332,  332,
  332,  332,  332,  332,  332,    0,  332,  332,    0,  332,
  332,  332,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,    0,  332,  332,  340,  340,    0,  332,  332,
  332,  332,  332,    0,  332,  332,  332,  332,  332,  332,
  332,    0,  332,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  332,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  332,  332,  332,  332,    0,    0,
    0,  332,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  340,  340,  340,
    0,  340,  340,    0,    0,    0,  340,    0,  340,    0,
    0,  340,  340,  340,  340,  340,  340,  340,  340,  340,
  340,    0,  340,  340,    0,  340,  340,  340,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,    0,  340,
  340,  333,  333,    0,  340,  340,  340,  340,  340,    0,
  340,  340,  340,  340,  340,  340,  340,    0,  340,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  340,  340,  340,  340,    0,    0,    0,  340,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  333,  333,  333,    0,  333,  333,    0,
    0,    0,  333,    0,  333,    0,    0,  333,  333,  333,
  333,  333,  333,  333,  333,  333,  333,    0,  333,  333,
    0,  333,  333,  333,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  333,    0,  333,  333,  336,  336,    0,
  333,  333,  333,  333,  333,    0,  333,  333,  333,  333,
  333,  333,  333,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  333,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  333,  333,  333,  333,
    0,    0,    0,  333,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  336,
  336,  336,    0,  336,  336,    0,    0,    0,  336,    0,
  336,    0,    0,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,    0,  336,  336,    0,  336,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  336,
    0,  336,  336,  354,  354,    0,  336,  336,  336,  336,
  336,    0,  336,  336,  336,  336,  336,  336,  336,    0,
  336,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  336,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  336,  336,  336,  336,    0,    0,    0,  336,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  354,  354,  354,    0,  354,
  354,    0,    0,    0,  354,    0,  354,    0,    0,  354,
  354,  354,  354,  354,  354,  354,  354,  354,  354,    0,
  354,  354,    0,  354,  354,  354,  354,  354,  354,  354,
  354,  354,  354,  354,  354,  354,    0,  354,  354,  328,
  328,    0,  354,  354,  354,  354,  354,    0,  354,  354,
  354,  354,  354,  354,  354,    0,  354,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  354,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  354,  354,
  354,  354,    0,    0,    0,  354,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  328,  328,  328,    0,  328,  328,    0,    0,    0,
  328,    0,  328,    0,    0,  328,  328,  328,  328,  328,
  328,  328,  328,  328,  328,    0,  328,  328,    0,  328,
  328,  328,  328,  328,  328,  328,  328,  328,  328,  328,
  328,  328,    0,  328,  328,  334,  334,    0,  328,  328,
  328,  328,  328,    0,  328,  328,  328,  328,  328,  328,
  328,    0,  328,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  328,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  328,  328,  328,  328,    0,    0,
    0,  328,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  334,  334,  334,
    0,  334,  334,    0,    0,    0,  334,    0,  334,    0,
    0,  334,  334,  334,  334,  334,  334,  334,  334,  334,
  334,    0,  334,  334,    0,  334,  334,  334,  334,  334,
  334,  334,  334,  334,  334,  334,  334,  334,    0,  334,
  334,  337,  337,    0,  334,  334,  334,  334,  334,    0,
  334,  334,  334,  334,  334,  334,  334,    0,  334,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  334,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  334,  334,  334,  334,    0,    0,    0,  334,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  337,  337,  337,    0,  337,  337,    0,
    0,    0,  337,    0,  337,    0,    0,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,    0,  337,  337,
    0,  337,  337,  337,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,    0,  337,  337,  338,  338,    0,
  337,  337,  337,  337,  337,    0,  337,  337,  337,  337,
  337,  337,  337,    0,  337,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  337,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  337,  337,  337,  337,
    0,    0,    0,  337,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  338,
  338,  338,    0,  338,  338,    0,    0,    0,  338,    0,
  338,    0,    0,  338,  338,  338,  338,  338,  338,  338,
  338,  338,  338,    0,  338,  338,    0,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,  338,  338,  338,
    0,  338,  338,  339,  339,    0,  338,  338,  338,  338,
  338,    0,  338,  338,  338,  338,  338,  338,  338,    0,
  338,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  338,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  338,  338,  338,  338,    0,    0,    0,  338,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  339,  339,  339,    0,  339,
  339,    0,    0,    0,  339,    0,  339,    0,    0,  339,
  339,  339,  339,  339,  339,  339,  339,  339,  339,    0,
  339,  339,    0,  339,  339,  339,  339,  339,  339,  339,
  339,  339,  339,  339,  339,  339,  303,  339,  339,    0,
    0,    0,  339,  339,  339,  339,  339,    0,  339,  339,
  339,  339,  339,  339,  339,    0,  339,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  339,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  339,  339,
  339,  339,    0,    0,    0,  339,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  304,  305,
  306,    0,  307,  308,    0,    0,    0,  309,    0,  310,
    0,    0,  311,  312,  313,  314,  315,  316,  317,  318,
  319,  320,    0,  321,  322,    0,  323,  324,  325,  326,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  258,
  336,  337,    0,    0,    0,  338,  339,  340,  341,  342,
    0,  343,  344,  345,  346,  347,  348,  349,    0,  350,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  352,  353,  354,  355,    0,    0,    0,  356,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  258,  258,  258,    0,  258,  258,    0,    0,    0,
  258,    0,  258,    0,    0,  258,  258,  258,  258,  258,
  258,  258,  258,  258,  258,    0,  258,  258,    0,  258,
  258,  258,  258,  258,  258,  258,  258,  258,  258,  258,
  258,  258,  259,  258,  258,    0,    0,    0,  258,  258,
  258,  258,  258,    0,  258,  258,  258,  258,  258,  258,
  258,    0,  258,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  258,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  258,  258,  258,  258,    0,    0,
    0,  258,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  259,  259,  259,    0,  259,  259,
    0,    0,    0,  259,    0,  259,    0,    0,  259,  259,
  259,  259,  259,  259,  259,  259,  259,  259,    0,  259,
  259,    0,  259,  259,  259,  259,  259,  259,  259,  259,
  259,  259,  259,  259,  259,  260,  259,  259,    0,    0,
    0,  259,  259,  259,  259,  259,    0,  259,  259,  259,
  259,  259,  259,  259,    0,  259,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  259,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  259,  259,  259,
  259,    0,    0,    0,  259,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  260,  260,  260,
    0,  260,  260,    0,    0,    0,  260,    0,  260,    0,
    0,  260,  260,  260,  260,  260,  260,  260,  260,  260,
  260,    0,  260,  260,    0,  260,  260,  260,  260,  260,
  260,  260,  260,  260,  260,  260,  260,  260,  261,  260,
  260,    0,    0,    0,  260,  260,  260,  260,  260,    0,
  260,  260,  260,  260,  260,  260,  260,    0,  260,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  260,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  260,  260,  260,  260,    0,    0,    0,  260,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  261,  261,  261,    0,  261,  261,    0,    0,    0,  261,
    0,  261,    0,    0,  261,  261,  261,  261,  261,  261,
  261,  261,  261,  261,    0,  261,  261,    0,  261,  261,
  261,  261,  261,  261,  261,  261,  261,  261,  261,  261,
  261,    0,  261,  261,    0,    0,    0,  261,  261,  261,
  261,  261,    0,  261,  261,  261,  261,  261,  261,  261,
    0,  261,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  261,    0,    0,    0,    0,    0,    0,    0,
    0,  307,    0,  261,  261,  261,  261,    0,  310,    0,
  261,  311,  312,  313,  314,  315,  316,  317,  318,  319,
  320,    0,  321,  322,    0,  323,  324,  325,  326,  327,
  328,  329,  330,  331,  332,  333,  334,  335,    0,  336,
  337,    0,    0,    0,  338,  339,  340,  341,  342,    0,
  343,  344,  345,  346,  347,  348,  349,    0,  350,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  351,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  352,  353,  354,  355,    0,    0,    0,  356,
  };
  protected static readonly short [] yyCheck = {             6,
   65,   44,    6,   61,  169,  548,   42,   33,   73,   16,
   60,  119,   33,   40,  125,  357,  247,  804,  279,  280,
  281,   60,   40,   40,   40,   40,  123,  123,  123,  123,
   44,   44,   44,  604,  295,  822,  123,   42,   44,  123,
  123,  753,  123,  122,  276,  280,   42,   44,  106,  281,
   57,  254,   91,   61,   44,   20,  259,   64,   65,   61,
  168,   40,  562,   44,   44,   44,   73,   42,   41,   76,
   40,   44,   76,  123,   44,  862,  306,  195,  196,   93,
   87,   62,  170,  312,  123,  314,   42,    0,   93,   96,
   97,  313,  321,  172,   65,   61,  175,  104,  313,  164,
   61,  323,  109,  333,  334,  112,   62,  114,  323,  323,
  114,  118,  125,  125,  121,   86,   91,  121,  830,  125,
  127,   86,    6,   88,  131,   90,  133,   40,  125,  700,
   42,  702,   16,  274,  705,  125,  397,  368,  266,  146,
  147,   42,  149,   44,  379,  125,  125,  379,  256,  260,
  272,  124,  517,  271,  119,  125,  260,  379,  286,  247,
  660,   61,  346,  257,  379,  276,  350,  370,   61,  257,
  177,  178,  291,  292,  295,  296,  255,  274,  274,  274,
   64,   65,   41,  970,  257,   44,  528,  274,  419,   73,
  274,  274,   76,  274,   40,  202,  361,  204,  363,  281,
  565,  566,   40,   41,  323,   40,   44,  318,  319,  320,
    0,   40,   40,   41,  325,  326,   44,  328,  329,  330,
  331,   41,  229,  794,   44,  264,   41,  257,  112,   44,
  114,  291,  292,   42,  273,  295,  296,  121,  288,  246,
  244,   40,  307,  282,  283,  284,  285,  286,  274,  307,
   40,  277,  274,  274,  272,  272,  272,  272,  265,  759,
   41,  274,  274,   44,  271,  630,  631,  263,  633,   61,
  309,   60,  279,  280,  434,  270,  271,  273,  438,  315,
  276,  441,   41,  371,   42,   44,   44,  548,  295,  550,
  450,   42,  274,   44,  555,   86,  354,   88,   89,   40,
  307,  666,   91,  310,  311,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  325,  326,
   41,   41,  260,   44,   44,  332,  333,   42,  828,   44,
  337,   42,   40,   44,  123,  596,  260,  128,  129,  839,
  840,  132,  885,  134,   40,  352,  353,  354,   41,  356,
  354,   44,   60,  380,   41,  268,  269,   44,  123,  524,
   41,  273,  275,   44,  429,   42,   41,   44,  733,   44,
  377,  736,  737,   42,  257,   44,  289,  290,  543,   41,
   42,  293,  882,   91,   41,   41,  267,   44,   44,  432,
  426,   41,  431,  558,   44,  307,  308,  897,  310,  311,
  900,   42,  314,   44,  316,  317,  318,  319,  320,  267,
  322,  324,  291,  292,  306,  123,  295,  296,   42,  379,
   44,  428,  429,  307,  428,  274,   42,  434,   44,  929,
  930,  438,   41,   42,  441,  291,  292,  444,   41,   42,
  123,   60,  321,  450,  451,  123,  453,  947,   41,   42,
   41,   42,   41,   42,   40,  820,  123,  464,  465,   41,
   42,   41,  520,  257,  471,  472,  473,  379,    0,  323,
  354,   40,   91,   41,   42,  264,  641,   40,  268,  269,
   40,   58,  272,  273,  273,  275,  651,   44,  653,   62,
  497,   58,   58,  282,  283,  284,  285,  286,   40,  289,
  290,  257,   61,  379,  123,   44,   40,  514,  379,   44,
  260,  518,  519,  520,  141,  519,  520,  429,  430,   44,
  257,   58,  332,   61,  420,  313,  257,  379,  789,  790,
   44,  538,  539,  540,  324,  257,  387,  387,  379,  387,
   44,  548,  379,  550,  428,  429,   44,   40,  555,  257,
  258,  259,  124,  261,  262,  263,  273,  265,   44,   44,
  567,   44,   44,  567,  272,  273,    0,  194,  195,  196,
  635,  279,  833,   44,   44,   44,  741,  257,  260,  287,
  387,  387,  387,  387,  211,  750,  387,  387,   42,  387,
  387,  387,  381,  600,  387,  602,    0,  604,  257,  379,
  387,  387,  609,  610,  611,  612,  613,  614,  615,  616,
  617,  618,  619,  620,   62,  257,  379,  257,  379,   44,
  266,  729,  730,  250,  885,  632,   40,  634,  635,   44,
   91,   44,   44,   44,  518,  519,  520,   44,   44,  266,
  267,  268,  424,   44,  271,  264,   44,   44,   44,   44,
   44,   44,   44,   44,  273,  662,   44,   44,   44,   44,
   44,  288,  257,  282,  283,  284,  285,  286,   44,  257,
  378,  387,  257,   44,   44,   44,   40,  304,   44,  306,
  387,  308,   44,  567,  392,  393,  394,    0,   44,   44,
   44,  756,   44,  700,   44,  702,   60,   44,  705,  257,
  327,  328,  329,  330,  331,  813,  814,  334,  816,  817,
   40,  338,  339,  340,  341,  342,  343,  344,  345,  346,
  347,  348,  349,  811,  812,   44,   44,   91,  130,   44,
   60,  738,   44,   44,  738,   44,  268,  269,   44,   44,
  272,  273,   44,  275,  260,   91,  753,  754,  632,   44,
  634,  635,   44,  387,  379,   44,  383,  289,  290,  123,
  379,   91,   44,  871,  379,   44,  874,  875,   42,  379,
   40,   44,   44,   44,  176,   44,  327,  257,  379,  387,
  379,  869,  870,  257,  872,  873,   44,  794,  260,  379,
  257,   40,  324,  123,  801,  802,   93,  380,   40,  201,
   40,  203,  257,  257,  258,  259,  327,  261,  262,  263,
  918,  265,  327,   44,  379,  217,  257,  257,  272,  273,
  332,  332,  276,  830,   40,  279,  327,  915,  916,  917,
  837,  835,  379,  287,  268,  269,  257,   44,  272,  273,
  467,  275,   44,  387,  387,   44,  387,  257,  379,  379,
  272,  939,    0,  272,  738,  289,  290,  257,  257,   10,
  211,  263,   91,   41,  268,  269,   44,  210,  272,  273,
  104,  275,  229,   20,  276,  283,  278,  280,  885,  550,
  241,   61,   60,  272,  789,  289,  290,  366,  246,  272,
  324,  272,  880,  257,  258,  259,  790,  261,  262,  263,
  420,  265,  360,  360,  421,  837,  555,  726,  272,  273,
  537,  830,  421,   91,  633,  279,   -1,   -1,   -1,    0,
  324,   -1,   -1,  287,  378,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  941,  265,   -1,   -1,  392,  393,
  394,   -1,  272,  273,   -1,  123,  953,  954,  955,  279,
   -1,   -1,   -1,   -1,   -1,  268,  269,  287,   -1,  272,
  273,   -1,  275,   -1,   -1,   -1,  593,  594,  595,   -1,
  597,  598,   -1,   -1,   -1,  125,  289,  290,   -1,   -1,
   -1,  608,  384,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,  397,  398,  399,  400,  401,  402,  403,  404,
  405,  406,  268,  269,   -1,  279,  272,  273,   42,  275,
  637,  324,   -1,  287,  378,   -1,   -1,  644,   -1,   -1,
   -1,   -1,  424,  289,  290,   -1,   60,   -1,  392,  393,
  394,  433,   -1,   -1,  436,  437,   -1,  439,  440,   -1,
  442,  443,   -1,  445,  446,  447,  448,  449,  378,   -1,
  452,   -1,  454,  455,  456,  457,   -1,   91,  324,  423,
   -1,   -1,  392,  393,  394,   -1,   -1,    0,   -1,   -1,
   -1,   -1,   -1,   -1,   42,   -1,   44,   -1,   -1,  257,
  258,  259,  709,  261,  262,  263,  264,  265,   -1,  123,
   -1,   -1,   60,  423,  272,  273,   -1,   -1,   -1,   -1,
  727,  279,   -1,   -1,  282,  283,  284,  285,  286,  287,
  260,   -1,   -1,   -1,  516,  293,   -1,   -1,   -1,   60,
   -1,   -1,   -1,   91,    0,   -1,  276,   -1,   -1,  307,
  308,   -1,  310,  311,   -1,   -1,  314,   -1,  316,  317,
  318,  319,  320,   -1,  322,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
  787,  788,   -1,   -1,   -1,   -1,   -1,  569,  318,  319,
  320,  573,   -1,   -1,  576,  325,  326,  579,  328,  329,
  330,  331,  123,  585,  586,   -1,  588,  268,  269,  260,
   -1,  272,  273,   -1,  275,   -1,  823,   -1,   -1,   -1,
  378,  379,   -1,  605,  606,  276,   -1,   -1,  289,  290,
   -1,   -1,   -1,   -1,  392,  393,  394,   -1,   -1,  621,
  622,  623,    0,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,  261,  262,   -1,   -1,  272,  273,
  867,   -1,   -1,  324,   -1,  279,   -1,  318,  319,  320,
   -1,  429,  430,  287,  325,  326,   -1,  328,  329,  330,
  331,   -1,   40,   41,   42,  667,   44,  669,  670,   -1,
  672,  673,  125,  675,  676,   -1,  678,  679,  680,  681,
  682,   -1,   60,  685,   62,  687,  688,  689,  690,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,  925,  926,
  927,  703,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,   91,   -1,   93,  257,  258,  259,  287,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,
   -1,  272,  273,  960,   -1,  268,  269,   -1,  279,  272,
  273,   -1,  275,   -1,  378,  123,  287,  125,   -1,   -1,
   -1,   -1,   -1,   -1,  756,   -1,  289,  290,  392,  393,
  394,   -1,  764,   -1,   -1,  767,   -1,   -1,  770,   -1,
   -1,  773,  399,  400,  401,  402,   -1,  779,  780,   -1,
  782,  408,  409,  410,  411,  412,  413,  414,  415,  416,
  417,  324,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,   -1,   -1,  805,  806,  807,   -1,  260,   -1,   -1,
  378,   -1,   -1,  289,  290,   -1,   -1,   -1,   -1,   -1,
    0,   -1,   -1,  276,  392,  393,  394,   -1,   -1,   -1,
   -1,   -1,   -1,  835,   -1,   -1,   -1,  378,   41,   -1,
   -1,   44,   -1,   -1,   -1,   -1,   -1,   -1,  324,   -1,
   -1,  392,  393,  394,   -1,   -1,   -1,   60,  860,  861,
   -1,   -1,   -1,    0,   -1,  318,  319,  320,   -1,   -1,
   -1,   -1,  325,  326,   -1,  328,  329,  330,  331,  257,
  258,  259,    0,  261,  262,  263,   -1,  265,   91,   -1,
  268,  269,   -1,    0,  272,  273,  274,  275,  276,   -1,
   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,
   -1,  289,  290,   -1,   -1,  293,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,   -1,   -1,  314,  315,  316,  317,
  318,  319,  320,   -1,  322,   -1,  324,   -1,   -1,   -1,
    0,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   42,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,    0,  377,
  378,   -1,   60,   -1,  382,  383,  384,  385,  386,    0,
  388,  389,  390,  391,  392,  393,  394,    0,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,  423,   -1,  425,  426,   -1,
   -1,  429,  430,   -1,  257,  258,  259,   -1,  261,  262,
  263,  264,  265,   -1,   -1,  123,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,  282,
  283,  284,  285,  286,  287,   -1,   -1,   -1,  268,  269,
  293,   -1,  272,  273,   -1,  275,   40,   41,   -1,   -1,
   44,   -1,   -1,   -1,  307,  308,   -1,  310,  311,  289,
  290,  314,   -1,  316,  317,  318,  319,  320,   62,  322,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   42,   -1,
   44,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
   -1,   -1,   -1,   -1,  324,   -1,   60,   -1,   -1,   93,
  268,  269,  289,  290,  272,  273,   -1,  275,   -1,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
   -1,  289,  290,   -1,   -1,  378,  379,   91,   -1,  123,
   -1,  125,  289,  290,   -1,   -1,   -1,  324,   -1,  392,
  393,  394,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,   40,  261,  262,  263,  324,  265,   -1,  123,
   -1,   -1,   -1,   -1,  272,  273,   -1,  324,  268,  269,
   -1,  279,  272,  273,   -1,  275,  429,  430,   -1,  287,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,  289,
  290,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,   -1,   -1,  314,   -1,  316,  317,
  318,  319,  320,   -1,  322,   -1,  268,  269,   -1,   60,
  272,  273,   -1,  275,  324,   -1,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,  268,  269,  289,  290,  272,
  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,  289,  290,
   91,   -1,   -1,   -1,   -1,   -1,  289,  290,   40,   -1,
   -1,   -1,   -1,  257,   -1,   -1,   -1,   -1,   -1,   -1,
  378,  379,  324,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  274,   -1,  123,  324,  392,  393,  394,   -1,   -1,   -1,
   -1,  324,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,  429,  430,  287,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   40,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  273,  274,   -1,  382,  383,
  384,  385,  386,  387,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,  418,  419,  420,  421,  392,  393,
  394,  425,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   40,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  407,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   60,   -1,   -1,  425,  279,  380,
   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   91,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   40,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  123,  273,  274,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   40,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,   -1,  377,  378,  273,  274,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,  273,   -1,   -1,
  425,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   40,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   -1,  377,  378,
  273,  274,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  362,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   60,   -1,   -1,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   91,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   40,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,   -1,  377,  378,  123,  273,  274,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,
   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   40,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,  273,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   40,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  362,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   60,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   91,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   40,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  123,  273,  274,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   40,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,   -1,
  377,  378,  273,  274,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,  273,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,   -1,  377,  378,  273,  274,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  362,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   60,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   91,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,   -1,  377,  378,  123,  273,  274,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,  628,  629,   60,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,
  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,  664,  665,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,  123,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   41,  396,  731,
  732,   60,  734,  735,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,  264,   60,   -1,   -1,   -1,
  418,  419,  420,  421,  273,   -1,   -1,  425,   -1,   -1,
  762,   -1,   91,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   -1,   91,   -1,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,  308,
   -1,  310,  311,  312,  123,  314,   -1,  316,  317,  318,
  319,  320,   -1,  322,   -1,   60,   -1,   -1,   -1,  123,
   -1,   -1,   -1,  815,  333,  334,  818,  819,   -1,   -1,
   60,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   91,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,
   -1,   91,   -1,   -1,   -1,  287,   -1,   -1,   -1,   -1,
  379,  293,   -1,   -1,   60,   -1,   -1,   -1,  123,   60,
   -1,   -1,   -1,   -1,  876,  307,  308,   -1,  310,  311,
   -1,   -1,  314,  123,  316,  317,  318,  319,  320,   -1,
  322,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  291,  292,
  429,  430,  295,  296,  297,  298,  299,  300,  301,  302,
  303,  304,  305,   -1,   -1,   -1,   -1,  123,   -1,  312,
   -1,   -1,  123,   -1,   -1,  264,   60,   -1,  321,   -1,
   -1,   -1,   60,   -1,  273,   -1,  378,  379,   -1,   -1,
  264,   -1,   -1,  282,  283,  284,  285,  286,   -1,  273,
  392,  393,  394,   -1,  293,   -1,   -1,   91,  282,  283,
  284,  285,  286,   91,   -1,   -1,   41,   -1,  307,  308,
  294,  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,
  319,  320,  321,  322,   -1,   60,   -1,  429,  430,  123,
   -1,   -1,   -1,   60,   -1,  123,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   91,  282,  283,  284,
  285,  286,   -1,  273,   91,   -1,   -1,   -1,  293,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  379,  306,  307,  308,   -1,  310,  311,   -1,  123,  314,
   -1,  316,  317,  318,  319,  320,  123,  322,  264,  309,
   -1,   -1,   -1,  264,   -1,   -1,   60,  273,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,  282,  283,  284,  285,
  286,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
  429,  430,  293,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,
  311,   -1,   -1,  314,  379,  316,  317,  318,  319,  320,
  321,  322,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,
  264,   -1,   60,   -1,   -1,   -1,  264,   -1,   -1,  273,
   -1,  347,  348,   -1,   -1,  273,   -1,   -1,  282,  283,
  284,  285,  286,   -1,  282,  283,  284,  285,  286,  293,
   -1,   -1,   -1,   91,  429,  430,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,  379,   -1,
  314,  309,  316,  317,  318,  319,  320,  321,  322,  264,
   -1,   -1,   -1,   -1,   -1,  123,   60,  264,  273,   -1,
   -1,   -1,   -1,  278,   -1,   -1,  273,  282,  283,  284,
  285,  286,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,  293,   91,  429,  430,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,   -1,
  307,  308,   -1,  310,  311,  379,   -1,  314,   -1,  316,
  317,  318,  319,  320,   -1,  322,   60,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,  282,  283,
  284,  285,  286,  125,   -1,  429,  430,   -1,   -1,  293,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  379,  307,  308,   -1,  310,  311,   -1,  123,
  314,   -1,  316,  317,  318,  319,  320,   -1,  322,   -1,
   -1,   -1,   -1,   -1,   -1,  123,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   -1,  429,  430,   -1,  293,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,  307,
  308,   -1,  310,  311,   -1,  379,  314,   -1,  316,  317,
  318,  319,  320,   -1,  322,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,  273,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,  257,   -1,   -1,   -1,  293,
   -1,   -1,   -1,   -1,   -1,  429,  430,   -1,   -1,   -1,
  123,  273,   -1,  307,  308,   -1,  310,  311,   -1,   -1,
  314,  379,  316,  317,  318,  319,  320,   -1,  322,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,  264,   -1,  282,  283,
  284,  285,  286,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  294,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,  429,  430,  335,  336,  337,  294,  339,  340,   -1,
   -1,   -1,  344,   60,  346,  379,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   91,  377,  378,  257,   -1,   -1,
  382,  383,  384,  385,  386,  125,  388,  389,  390,  391,
  392,  393,  394,  273,  396,  429,  430,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,  123,   -1,  125,   -1,
   -1,  264,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
  273,   -1,   -1,  425,   -1,  278,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,   -1,
   -1,   -1,  382,  383,  384,  385,  386,  125,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,  257,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,  264,  265,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   60,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   91,  377,  378,  257,
   -1,   -1,  382,  383,  384,  385,  386,  125,  388,  389,
  390,  391,  392,  393,  394,  273,  396,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  293,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,
  316,  317,  318,  319,  320,   -1,  322,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,   -1,   -1,  379,  382,  383,  384,  385,  386,  125,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,  257,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,  264,
  265,   -1,   -1,  429,  430,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   41,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  257,   -1,   60,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,  273,  396,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   41,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,  123,  339,  340,   -1,   -1,   -1,  344,   60,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   91,  377,  378,   60,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,  123,   -1,   91,   -1,   -1,   60,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   91,   -1,
   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,  264,  265,   -1,
   -1,   -1,   -1,   -1,   -1,   60,  273,   -1,   -1,  276,
  123,   91,  279,  264,  265,  282,  283,  284,  285,  286,
  287,   -1,  273,  274,   -1,   -1,   -1,   -1,   60,   -1,
   -1,  282,  283,  284,  285,  286,   91,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   60,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
  125,   -1,   -1,   -1,   -1,   91,  257,  258,  259,   -1,
  261,  262,  263,  264,  265,   -1,   -1,   -1,   -1,   -1,
   60,  123,  273,  125,   -1,  276,   -1,   -1,  279,   -1,
   -1,  282,  283,  284,  285,  286,  287,  123,   -1,   -1,
  257,  258,  259,  260,  261,  262,  263,  264,  265,   -1,
   -1,   91,   -1,   93,   60,   -1,  273,  274,   -1,  276,
   -1,   60,  279,   -1,   -1,  282,  283,  284,  285,  286,
  287,   -1,   -1,   -1,  257,  258,  259,  260,  261,  262,
  263,  264,  265,  123,   -1,   91,   -1,   60,   -1,   -1,
  273,  274,   91,  276,   -1,   -1,  279,   -1,   -1,  282,
  283,  284,  285,  286,  287,   -1,   -1,  257,  258,  259,
   60,  261,  262,  263,  264,  265,   -1,  123,   91,   -1,
   -1,   -1,   -1,  273,  123,   -1,  276,   -1,   -1,  279,
   -1,   -1,  282,  283,  284,  285,  286,  287,   -1,  264,
   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,  123,   -1,   -1,   -1,   -1,  264,   -1,
  282,  283,  284,  285,  286,   -1,   -1,  273,  274,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,  257,
   -1,   -1,   -1,   -1,   -1,  264,   -1,  273,   -1,   -1,
   -1,   -1,  278,   -1,  273,  273,  282,  283,  284,  285,
  286,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  273,  274,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  273,  274,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  273,  274,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  377,  378,  273,
  274,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  377,  378,  273,  274,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  377,  378,  273,  274,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
   -1,  377,  378,  273,  274,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  273,  377,  378,   -1,
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
  377,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  273,  377,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,  377,  378,   -1,   -1,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  273,  377,
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
  375,   -1,  377,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  339,   -1,  418,  419,  420,  421,   -1,  346,   -1,
  425,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   -1,  377,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,
  };

#line 1394 "Iril/IR/IR.jay"

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
  public const int ATOMIC = 431;
  public const int MONOTONIC = 432;
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
