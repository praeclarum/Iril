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
    "LANDINGPAD","CATCH","CATCHPAD","CLEANUPPAD",
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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 134 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 22:
#line 138 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 142 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 24:
#line 146 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 150 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 26:
#line 154 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 27:
#line 158 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 28:
#line 162 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 29:
#line 166 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 30:
#line 170 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 31:
#line 174 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 32:
#line 178 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 33:
#line 182 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 34:
#line 186 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 35:
#line 190 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 36:
#line 191 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 195 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 196 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 197 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 40:
#line 198 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
#line 199 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 42:
#line 200 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 43:
#line 201 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 44:
#line 202 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 45:
#line 203 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 46:
#line 207 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 47:
#line 211 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 48:
  case_48();
  break;
case 49:
  case_49();
  break;
case 50:
#line 228 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 51:
#line 229 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 52:
#line 230 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 53:
#line 234 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 54:
#line 238 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 65:
#line 267 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 66:
#line 271 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 278 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 68:
#line 282 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 69:
#line 286 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 70:
#line 290 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 71:
#line 294 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 90:
#line 328 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 91:
#line 332 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 92:
#line 336 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 93:
#line 343 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 347 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 352 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 99:
#line 358 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 100:
#line 359 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 101:
#line 360 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 102:
#line 361 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 103:
#line 365 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 104:
#line 369 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 373 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 106:
#line 377 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 107:
#line 381 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 385 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 109:
#line 389 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 110:
#line 396 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 400 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 408 "Iril/IR/IR.jay"
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
#line 440 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 126:
#line 444 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 127:
#line 448 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 128:
#line 452 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 129:
#line 456 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 130:
#line 463 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 131:
#line 467 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 132:
#line 471 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 137:
#line 482 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 138:
#line 489 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 493 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 140:
#line 497 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 501 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 505 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 146:
#line 515 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 147:
#line 516 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 148:
#line 523 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 527 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 534 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 538 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 542 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 153:
#line 546 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 155:
#line 554 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 558 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 157:
#line 559 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 158:
#line 560 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 159:
#line 561 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 160:
#line 562 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 161:
#line 563 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 162:
#line 564 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 163:
#line 565 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 164:
#line 566 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 165:
#line 567 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 166:
#line 568 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 167:
#line 572 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 168:
#line 576 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 176:
#line 599 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 177:
#line 600 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 178:
#line 601 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 179:
#line 602 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 180:
#line 603 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 181:
#line 604 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 182:
#line 605 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 183:
#line 606 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 184:
#line 607 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 185:
#line 608 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 186:
#line 612 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 187:
#line 613 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 188:
#line 614 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 189:
#line 615 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 190:
#line 616 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 191:
#line 617 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 192:
#line 618 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 193:
#line 619 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 194:
#line 620 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 195:
#line 621 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 196:
#line 622 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 197:
#line 623 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 199:
#line 625 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 200:
#line 626 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 201:
#line 627 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 202:
#line 631 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 206:
#line 641 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 207:
#line 645 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 649 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 653 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 657 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 661 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 665 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 669 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 214:
#line 673 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 215:
#line 677 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 217:
#line 685 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 218:
#line 686 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 219:
#line 687 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 220:
#line 688 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 221:
#line 689 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 222:
#line 690 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 223:
#line 691 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 224:
#line 692 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 225:
#line 693 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 226:
#line 700 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 707 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 711 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 229:
#line 718 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 725 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 729 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 736 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 744 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 235:
#line 751 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 766 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 770 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 777 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 781 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 788 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 792 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 244:
#line 796 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 800 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 246:
#line 807 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 811 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 818 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 822 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 826 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 830 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 838 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 254:
#line 839 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 255:
#line 846 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 850 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 258:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 259:
#line 865 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 260:
#line 869 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 261:
#line 873 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 262:
#line 877 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 263:
#line 881 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 265:
#line 886 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 266:
#line 890 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 267:
#line 894 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 268:
#line 898 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 269:
#line 902 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 274:
#line 919 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 923 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 929 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 277:
#line 936 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 940 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 947 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 965 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 286:
#line 972 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 979 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 983 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 990 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 994 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 998 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1002 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1006 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 294:
#line 1010 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 296:
#line 1018 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1022 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1026 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1030 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1042 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1046 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 303:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 304:
#line 1054 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1058 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 306:
#line 1062 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 307:
#line 1066 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1070 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 309:
#line 1074 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 310:
#line 1078 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 311:
#line 1082 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 312:
#line 1086 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 313:
#line 1090 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 314:
#line 1094 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 315:
#line 1098 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 316:
#line 1102 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 317:
#line 1106 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 318:
#line 1110 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 319:
#line 1114 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 320:
#line 1118 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 321:
#line 1122 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 322:
#line 1126 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 323:
#line 1130 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 324:
#line 1134 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 325:
#line 1138 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1142 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1146 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1150 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1154 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1158 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1162 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1166 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1170 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1174 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1178 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1182 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1186 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1190 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1194 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1198 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1202 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1206 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1210 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 344:
#line 1214 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1218 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 346:
#line 1222 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 347:
#line 1226 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 348:
#line 1230 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 349:
#line 1234 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1238 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1242 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1246 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1250 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1254 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1258 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1262 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1266 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1270 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1274 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1278 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1282 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1286 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1290 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 364:
#line 1294 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 365:
#line 1298 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1302 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1306 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 368:
#line 1310 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1314 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1318 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1322 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 372:
#line 1326 "Iril/IR/IR.jay"
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
        module.AddGlobalVariable(g);
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

void case_48()
#line 216 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_49()
#line 221 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_114()
#line 413 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_115()
#line 418 "Iril/IR/IR.jay"
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
   46,   46,   46,   41,   41,   39,   39,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   39,   39,   16,   16,
   42,   42,   37,   37,   47,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   50,   51,   51,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   13,   13,   54,   20,   20,   20,   20,
   20,   20,   20,   20,   20,   55,   27,   27,   56,   53,
   53,   25,   57,   57,   52,   52,   58,   59,   59,   36,
   36,   60,   60,   60,   60,   61,   61,   63,   63,   63,
   63,   65,   66,   66,   67,   67,   68,   68,   68,   68,
   68,   68,   68,   69,   69,   69,   69,   69,   69,   21,
   21,   70,   70,   71,   71,   72,   73,   73,   74,   75,
   75,   76,   76,   43,   77,   78,   62,   62,   79,   79,
   79,   79,   79,   79,   79,   80,   80,   80,   80,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,
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
    2,    1,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    4,    2,    1,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    6,    9,    8,    6,
    6,    3,    3,    3,    5,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    2,    2,    1,    2,    1,
    3,    2,    1,    2,    1,    3,    1,    1,    3,    1,
    2,    2,    3,    1,    2,    1,    2,    1,    2,    3,
    4,    1,    3,    2,    1,    3,    2,    3,    3,    3,
    2,    4,    5,    1,    1,    6,    9,    6,    6,    1,
    3,    1,    1,    1,    3,    5,    1,    2,    3,    1,
    2,    1,    1,    1,    1,    5,    1,    3,    2,    7,
    2,    2,    7,    1,    1,    8,    9,    9,   10,    5,
    6,    5,    7,    5,    5,    6,    4,    4,    5,    6,
    6,    7,    5,    5,    6,    6,    6,    7,    5,    6,
    7,    7,    8,    6,    4,    4,    5,    6,    5,    2,
    5,    4,    4,    4,    4,    5,    6,    7,    6,    6,
    6,    4,    3,    4,    7,    8,    5,    6,    5,    5,
    6,    3,    4,    5,    6,    7,    4,    5,    6,    6,
    4,    5,    7,    8,    5,    6,    4,    5,    4,    5,
    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   96,  107,   99,  100,  101,  102,   98,  130,   39,
   37,   40,   41,   42,   43,   44,   45,  284,  161,  162,
  163,    0,   38,  164,  156,  157,  159,  158,  160,  166,
  171,  172,    0,    0,    0,    0,   97,    0,    0,    0,
    0,    0,  131,  132,    0,    0,  154,    0,    0,    3,
    0,    4,    0,    0,  169,  170,   35,   36,   46,   47,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  168,   90,    0,    0,    0,    0,    0,    0,    0,  136,
    0,    0,    0,  165,    0,    0,    0,    0,    0,    0,
    0,  155,    0,    0,    0,    5,    6,    0,    0,    0,
    0,    0,    0,    0,    0,    8,    0,    7,    0,    0,
    0,    0,    0,   91,    0,    0,    0,    0,  135,    0,
  113,  103,    0,    0,  110,    0,    0,    0,    0,    0,
    0,    0,  152,  153,  147,    0,    0,  148,  175,    0,
    0,    0,  173,    0,    0,    0,  219,  220,  218,  221,
  222,  223,  217,  206,  225,  224,    0,    0,    0,    0,
    0,    0,    0,    0,  205,    0,    0,    0,    0,    0,
    0,    0,    0,   48,    0,    0,    0,   74,   73,   13,
    0,    0,   67,   72,  167,    0,    0,    0,    0,  106,
  104,    0,    0,    0,    0,    0,  139,    0,    0,    0,
   88,   87,   79,   77,   78,   80,   81,   82,   83,    0,
   75,    0,  146,    0,    0,    0,    0,    0,    0,    0,
  123,  174,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  230,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   15,    0,    0,    0,   68,   14,
    0,  202,  204,  203,  227,  108,   92,  109,  111,  140,
    0,    0,  141,    0,    0,   12,   76,  149,    0,  119,
   65,    0,    0,    0,    0,    0,    0,  294,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  240,    0,    0,  246,    0,
  287,  295,    0,    0,  137,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  214,    0,    0,    0,  212,
  213,    0,    0,    0,    0,    0,   61,   64,    0,   59,
    0,   50,   62,    0,   56,   58,   63,   60,   51,   52,
   49,   17,   16,   71,   70,   69,  142,   84,  273,  272,
    0,  270,    0,    0,  292,    0,    0,  289,    0,    0,
    0,    0,  291,  282,  283,    0,    0,  280,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  285,  330,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  176,  177,  178,  179,
  180,  181,  182,  183,  184,  185,    0,  186,  187,  198,
  199,  200,  201,  189,  191,  192,  193,  194,  190,  188,
  196,  197,  195,    0,    0,    0,    0,    0,    0,    0,
    0,  114,  241,    0,  247,    0,    0,   66,    0,  124,
   33,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  231,    0,    0,    0,    0,    0,    0,    0,  232,    0,
   89,    0,  120,    0,  288,  226,    0,    0,  252,    0,
    0,    0,    0,    0,    0,  281,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  274,    0,    0,    0,
    0,    0,    0,    0,    0,  343,    0,    0,  115,   20,
    0,   28,    0,    0,    0,    0,    0,    0,  215,    0,
    0,    0,    0,    0,   54,    0,   57,  271,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  325,    0,    0,  237,  238,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  344,    0,    0,    0,    0,  211,  207,
  210,    0,   23,    0,    0,   53,    0,    0,    0,  254,
    0,    0,  255,    0,    0,    0,    0,  300,    0,  327,
  365,    0,  336,  350,    0,  331,  368,    0,  354,  329,
  370,  362,  358,    0,    0,  347,    0,  305,  304,  349,
  371,    0,    0,    0,    0,  302,    0,    0,  216,  229,
    0,    0,    0,    0,    0,    0,    0,    0,  275,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  233,    0,  235,    0,    0,    0,    0,
  277,    0,    0,    0,  257,  253,    0,    0,    0,    0,
    0,  301,  366,  351,  355,  359,  348,  306,  340,  360,
  239,    0,    0,    0,    0,    0,    0,    0,    0,  339,
  328,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  234,  209,    0,  290,    0,  293,  278,
    0,  265,  259,    0,    0,    0,    0,  264,  260,  258,
  256,    0,    0,    0,    0,  303,    0,  345,    0,  363,
    0,  276,  356,    0,    0,    0,    0,    0,  208,  236,
  279,  262,    0,    0,    0,    0,    0,  296,    0,    0,
    0,  346,  364,  286,    0,  263,    0,    0,    0,    0,
  297,  298,    0,    0,    0,    0,    0,  299,    0,    0,
    0,    0,    0,  269,  266,  268,    0,    0,  267,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   57,   12,   13,   14,  230,  201,  193,   58,
   82,  202,  273,   83,  238,  215,   85,  194,  382,  185,
  401,  384,  385,  386,  387,  203,  784,  231,   94,   95,
  144,  145,   15,  115,  161,  345,  216,  241,   67,   62,
   68,   63,   64,  217,  157,  158,  163,  477,  494,  274,
  539,  785,  253,  760,  408,  672,  786,  665,  666,  346,
  347,  348,  349,  350,  540,  633,  722,  723,  849,  402,
  596,  597,  790,  791,  417,  418,  452,  700,  351,  352,
  };
  protected static readonly short [] yySindex = {         1319,
   28, -119,   33,   50,   76, 2971, 3293, -281,    0, 1319,
    0,    0,    0,    0, -131, -103,  101,  121, 2027,  -72,
  -28,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  174,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -24, 4221,  -91,  -12,    0, -122,  196,  215,
 4342, 3059,    0,    0, 3359,  -21,    0, 3359,  200,    0,
  248,    0,   52,   57,    0,    0,    0,    0,    0,    0,
 -101, 4342,   73, -123,  -10,  -44,  285,  -16,  207,   82,
    0,    0,  196,   -5,  215,   77, 4342,   99,   66,    0,
   15, 3365,  215,    0, 4342,  215, 3359,  -20, 3359,  248,
  -19,    0,  262, 2177, -204,    0,    0, 4342, -123, -123,
 2834, 4342, -123, 4342, -123,    0,  273,    0, -241,  360,
  278, 4085,  367,    0, 4342, 4342,    6, 4342,    0,  156,
    0,    0,  196,   41,    0,  215,  215,  248,  -18, -204,
  248, 1162,    0,    0,    0,  147,  152,    0,    0,  116,
 -105, -256,    0,  761, 4342, 4342,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  -29,  411,  412,  416,
 4365, 4372, 4365,  415,    0, 2834, 4342, 2834, 4342,  402,
  410,  414,  172,    0, -241, 4188,    0,    0,    0,    0,
    3, 1116,    0,    0,    0,  196,   10,  408,    7,    0,
    0, 3915, -204,  248,  116,  116,    0, -204,  413,  433,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 1436,
    0, 1661,    0, 3812, -221,  201, 6590, -100, 4365,  222,
    0,    0,  102,  436,  196, 2784,  443, 4342, 4365, 4365,
 4365,    0,   31, 4249,   36,   -7,  106,  442, 2834,  445,
 2834, 4121, 4155, 1577,    0, -241,  185,   40,    0,    0,
 4216,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -204,  116,    0,  227, 2166,    0,    0,    0,  233,    0,
    0,  430, 4365, -144, 4365, 3125, 4365,    0, 1822, 4342,
 1822, 4342, 1822, 4342, 4342, 2935, 4342, 4342, 4342, 1822,
 2977, 3043, 4342, 4342, 4342, 4365, 4365, 4365, 4365, 4365,
 4342, 4018, 4052,  163,  150, 4365, 4365, 4365, 4365, 4365,
 4365, 4365, 4365, 4365, 4365, 4365, 4365,  158, 1215, 4342,
 4342, 3125,   85, 4342, 3351,    0, 6590,  228,    0,  228,
    0,    0,  232, 6590,    0,  183,  251, -244,  130,  466,
 4342,  144,  124,  128,  129,    0, 4365, 1116,   46,    0,
    0,  263,  142,  478,  148,  479,    0,    0,  488,    0,
  938,    0,    0,  405,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  219,    0,  183, 7079,    0,  258,  575,    0,  492,  359,
 3359, 3359,    0,    0,    0, 1116, 1822,    0, 1116, 1116,
 1822, 1116, 1116, 1822, 1116, 1116, 4342, 1116, 1116, 1116,
 1116, 1116, 1822, 4342, 1116, 4342, 1116, 1116, 1116, 1116,
  494,  496,  498,  499,  500,  155, 4342,  223, 4365,  501,
    0,    0, 4342,  306,  145,  160,  162,  164,  166,  178,
  179,  180,  184,  186,  187,  203,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4342,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 4342,    2, 1116,  359, 4342, 3359, 3125,
  -35,    0,    0,  228,    0,  276,  276,    0, 3455,    0,
    0,  337,  343,  354,  226,  310, 4365, 4342, 4342, 4342,
    0,  545,  228,  356,  229,  357,  236,  463,    0, 4155,
    0, 2166,    0,  228,    0,    0,  581,  353,    0,  587,
  359,  359, 3359,  585, 1116,    0,  586,  589, 1116,  590,
  592, 1116,  593,  595, 1116,  596,  597,  598,  599,  607,
 1116, 1116,  608, 1116,  609,  610,  614,  615, 4365, 4365,
 4365, 1577, 4365, 1467,  351, 4342,  616, 4342,  382, 4365,
 4342, 4342, 4342, 4342, 4342, 4342, 4342, 4342, 4342, 4342,
 4342, 4342, 1116, 1116,  575,  617,    0,  619,  587,  359,
  359, 4342,   84, 4342, 3359,    0, 4365,  276,    0,    0,
  228,    0,  391, 4365,  621,  161,  339,  396,    0,  276,
  228,  417,  228,  419,    0,  269,    0,    0,  276,  353,
  582, 3482,  290,  587,  587,  359,  575,  648,  575,  575,
  649,  575,  575,  652,  575,  575,  655,  575,  575,  575,
  575,  575,  658,  661,  575,  662,  575,  575,  575,  575,
    0,  665,  666,    0,    0,  667,  670,  458,  673, 4342,
 1116,  679, 4342,  683, 4365,  685,  196,  196,  196,  196,
  196,  196,  196,  196,  196,  196,  196,  196,  686,  687,
  688,  642, 4365,  116,  587,  587,  359,  311,  587,  587,
  359,  359, 3359,    0,  276,  228,  690,  -14,    0,    0,
    0,  276,    0,  276,  228,    0,  694, 4342, 4254,    0,
 2691,  280,    0,  353,  368,  369,  587,    0,  575,    0,
    0,  575,    0,    0,  575,    0,    0,  575,    0,    0,
    0,    0,    0,  575,  575,    0,  575,    0,    0,    0,
    0, 4365, 4365, 1577, 1577,    0,  362,  703,    0,    0,
  375,  714,  384,  716,  -14,  575,  575,  575,    0,  721,
  116,  116,  116,  587,  508,  116,  116,  587,  587,  359,
  276,  -14, 4365,    0,  288,    0,  276,  353,  727, 4300,
    0,  734,  125, 2853,    0,    0, 4336,  450,  353,  353,
  409,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  667,  538,  423,  547,  427,  550,  -14,  764,    0,
    0,  717, 4365,  116,  116,  116,  765,  116,  116,  116,
  116,  587,  332,    0,    0,  -14,    0,  353,    0,    0,
  699,    0,    0,  431,  772,  773,  776,    0,    0,    0,
    0,  353,  490,  491,  353,    0,  562,    0,  563,    0,
  764,    0,    0,  116,  564,  116,  116,  116,    0,    0,
    0,    0,  333,  785, 4365, 4365, 4365,    0,  353,  353,
  503,    0,    0,    0,  116,    0, 4342,  440,  441,  444,
    0,    0,  353,  387, 4342, 4342, 4342,    0, 4365,  401,
  404,  407,  791,    0,    0,    0,  -14,  348,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  839,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3176,  676,  569,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   29,    0,    0,    0,    0,    0, 3242,    0,
  973,    0,  570,    0,    0,  573,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  810,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  195,    0,    0,  579,  583,    0,    0,  525,
    0,    0,    0,    0,    0,  318,    0,    0,    0,  -98,
    0,  -97,    0,  554,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  810,    0,  810,    0,    0,
    0,    0,    0,    0,    0,    0, 1079,    0,    0,    0,
    0,  810,    0,    0,    0,   30,  810,    0,  810,    0,
    0,    0,    0,    0,  604,  628,    0,    0, 1791, 1872,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  366,    0,    0,  -96,    0,    0,    0,    0,    0,
    0,    0,    0,  783,  320,  810,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  810,    0,
  810,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  853,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3558,    0, 6693,
    0,    0,    0,    0,    0,  -94,    0,    0,    0,    0,
    0,  810,    0,    0,    0,    0,    0,   29,    0,    0,
    0,    0,    0,    0,    0,  861,    0,    0,   19,    0,
  810,    0,    0,  376,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -89,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  810,    0,    0,  810,  810,
    0,  810,  810,    0,  810,  810,    0,  810,  810,  810,
  810,  810,    0,    0,  810,    0,  810,  810,  810,  810,
    0,    0,    0,    0,    0,  810,    0,  810,    0,    0,
    0,    0,    0,  810,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  810,  810,    0,    0,    0,    0,
  810,    0,    0, 3661,    0, 3764, 6796,    0,    0,    0,
    0,    0,    0,    0,    0,  810,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 6899,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  810,    0,    0,    0,  810,    0,
    0,  810,    0,    0,  810,    0,    0,    0,    0,    0,
  810,  810,    0,  810,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  810,    0,    0,    0,  810,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  810,  810,    0, 4324,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3867,    0,    0,
  892,    0,    0,    0,    0,  810,  810,  810,    0,  919,
    0,    0,    0,    0,    0,    0,    0,    0, 7002,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4427,    0,    0,    0,    0,
  810,    0,    0,    0,    0,    0, 1182, 1309, 1434, 1537,
 1664, 1789, 1892, 2019, 2144, 2247, 2374, 2499,    0,    0,
    0,    0,    0, 4530,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  981, 1717,    0,    0,    0,    0,
    0, 2072,    0, 2670, 2693,    0,    0,    0,    0,    0,
  810,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4633, 4736, 4839,    0,    0, 4942,    0,    0,    0,    0,
 2750,    0,    0,    0,    0,    0, 2833,    0,    0,    0,
    0,  381,  810,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 5045,    0,    0,    0,    0,    0,    0, 5148,    0,
    0,    0,    0, 5251, 5354, 5457,    0, 5560, 5663, 5766,
 5869,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5972,    0,    0, 6075,    0, 6178, 6281, 6384,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 6487,    0,    0,    0,    0,    0,
    0,    0,    0,  810,    0,    0,    0,    0,    0,  810,
  810,  810,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  846,  771,    0,    0,    0,    0,  663,  668,  845,
  208,   -6,  -38,   44, -336,   32,    0,  600,  602, -254,
 -515,    0,  328,    0, -680,    0,  352,  637,  774,  157,
    0,  656,    0,  -80,    0,  516,  -82, -211,   -2,    0,
  -59,  816,  -56, -160,    0,  641, -121,    0,    0,    0,
  350, -737, -118,    0, -477, -524,   47,  126,  127, -330,
    0,  539,  540,  481, 2450, -495,    0,   91,    0,  372,
    0,  206,    0,  117,  -86, -190,    0,    0,    0,  502,
  };
  protected static readonly short [] yyTable = {            59,
   59,  100,  107,   61,   89,  109,  101,  383,  383,  390,
  248,  506,  626,  507,  503,  127,  131,  237,  102,  102,
  102,  102,  354,  290,  116,  121,  117,  819,  122,  150,
  400,   97,  162,  118,  190,  512,  367,  789,  135,  191,
  242,   69,  139,  101,  833,   55,  271,   93,  101,  135,
   84,  101,  280,  674,   59,   59,  239,  283,   59,   60,
  631,   59,   60,  255,  256,  112,  159,  213,   95,   95,
  218,  276,   93,   94,  367,  121,   56,  235,   54,  367,
  861,  211,  184,  271,  212,  371,   75,   76,   16,  367,
   93,  239,  595,   19,  242,  143,  232,  370,   59,  278,
   59,  159,   59,  694,  112,  105,  112,  156,   54,  789,
   20,  164,  120,  242,  123,  186,  125,  188,  159,  134,
  397,  406,  240,  102,  119,  244,  529,  270,  206,  207,
  208,  209,  282,  281,  513,  369,   21,  192,  725,  726,
   71,  407,   60,  182,  510,  758,  160,  258,  762,  260,
   17,   18,  717,   93,   94,  366,   72,  289,  245,  246,
  242,   73,   60,   66,  393,   96,  101,  608,  236,  908,
  522,   77,   78,  353,  183,  116,  121,  117,  503,  122,
  259,   74,  261,   38,  118,  101,  620,  517,  101,   75,
   76,  533,  233,   77,   78,  234,  101,  629,  574,  772,
  773,  709,  101,  776,  777,  143,  181,  360,   86,   55,
   51,   52,  265,   90,  421,  266,  424,  103,  106,   80,
  374,  108,  376,  433,  111,  392,  546,  156,  266,  112,
  546,  801,   91,  546,   95,  112,  411,  101,  112,  412,
   56,  362,  546,  126,   98,   87,  798,  368,   88,  197,
  110,  148,  151,  214,  102,  381,  381,  130,   23,  531,
  113,  146,  532,  147,  101,  149,  576,   24,   25,   26,
   27,   28,   54,  400,  705,  383,  269,  400,  826,  606,
   75,   76,  830,  831,  712,  499,  714,  114,  118,   59,
  122,  124,  416,  419,  420,  422,  423,  425,  426,  428,
  429,  430,  431,  432,  435,  437,  438,  439,  440,  716,
  837,  116,  532,  269,  446,  448,  117,  664,  454,   19,
  796,  853,  854,  797,  129,  873,  165,  166,  835,  132,
  187,  836,  189,  495,  496,   59,  868,  501,  133,  498,
  167,  168,  169,  136,  170,  171,  172,  101,  173,  580,
  247,  101,  543,  614,  516,  272,  174,   95,  150,   95,
  871,  150,  175,   75,   76,  138,  783,   77,   78,  781,
  176,   38,  869,  886,  878,  836,  532,  881,  787,  710,
  101,  167,  168,  169,  152,  170,  171,  172,  909,  173,
  607,  836,  101,  140,  670,   97,  842,  843,  102,  195,
  196,  891,  892,  175,   59,   59,  151,  205,  112,  151,
  545,  176,  210,   22,  549,  898,   55,  552,  182,   55,
  555,  261,   23,  101,  261,  675,  561,  562,  101,  564,
  899,   24,   25,   26,   27,   28,  711,  101,  159,  104,
  575,  904,  101,  605,  905,  101,  579,  906,  101,  183,
  249,  250,  410,   39,   40,  251,   41,   42,  257,  262,
   44,  177,   45,   46,   47,   48,   49,  263,   50,  277,
  593,  264,  285,  284,  291,  178,  179,  180,  356,  358,
  357,  181,  361,  204,  372,  373,  398,  594,  375,  403,
  404,   59,   59,   59,  451,  239,  602,  604,  497,  664,
  664,  236,  844,  625,  500,  508,  698,  511,  514,  515,
  518,  616,  617,  618,  519,  520,  845,  846,  847,  523,
  524,  525,  527,  381,  138,   53,  526,  528,  530,  453,
  536,  581,  252,  252,  252,  538,   59,  569,  848,  570,
  112,  571,  572,  573,  578,  703,  582,  204,  583,  353,
  584,  275,  585,   34,  467,  468,  469,  470,  471,  472,
  473,  474,  475,  476,  586,  587,  588,  541,  542,  671,
  589,  671,  590,  591,  677,  678,  679,  680,  681,  682,
  683,  684,  685,  686,  687,  688,  400,   19,   19,  592,
  355,   19,   19,   95,   19,   59,  610,   59,   59,  611,
  363,  364,  365,  143,  613,  252,  619,  622,   19,   19,
  612,  771,  621,  623,  624,  167,  168,  169,  406,  170,
  171,  172,  396,  173,  630,  721,  632,  145,  637,  639,
  272,  174,  640,  642,  182,  643,  645,  175,  646,  648,
  649,  650,  651,   19,  405,  176,  409,  706,  413,  242,
  652,  655,  657,  658,  600,  601,  603,  659,  660,  673,
  692,  794,  693,  671,  708,  183,  671,  441,  442,  443,
  444,  445,  718,  713,  450,  715,  724,  455,  456,  457,
  458,  459,  460,  461,  462,  463,  464,  465,  466,  824,
  825,  729,  732,  828,  829,  735,   59,  181,  738,  636,
  112,  744,  242,  242,  745,  747,  242,  242,  752,  753,
  754,  381,  793,  755,  756,   95,  757,  275,  521,  167,
  168,  169,  761,  170,  171,  172,  763,  173,  765,  766,
  767,  768,  595,  782,  775,   95,  177,  788,  399,  872,
  813,  175,  242,  864,  242,  242,  814,  866,  867,  176,
  178,  179,  180,  815,  799,  800,  537,  816,  697,  818,
  701,  702,  817,  242,  823,  544,   95,  827,  547,  548,
  838,  550,  551,  841,  553,  554,  852,  556,  557,  558,
  559,  560,   18,  381,  563,  885,  565,  566,  567,  568,
  721,  112,  138,  138,  856,  855,  138,  138,   95,  138,
  577,  857,  101,  858,  243,  859,  860,  836,  865,  862,
  874,  875,  876,  138,  138,  877,  879,  880,  882,  883,
  182,   34,   34,  884,  887,   34,  895,  896,   34,  893,
  897,  167,  168,  169,  907,  170,  171,  172,    1,  173,
  125,  126,   34,   34,  127,  598,  272,  174,  138,   95,
  128,  183,  144,  175,  129,   70,  128,  627,  268,  780,
   26,  176,  267,   81,  389,  391,  287,  279,  615,  509,
  137,  143,  143,   99,  288,  143,  143,   34,  143,  811,
  894,  812,  870,  181,  534,  504,  505,  851,  900,  901,
  902,   31,  143,  143,  638,  145,  145,  769,  641,  145,
  145,  644,  145,  628,  647,  535,  840,    0,    0,    0,
  653,  654,    0,  656,    0,    0,  145,  145,   21,    0,
  661,  662,  663,    0,  667,  669,    0,  143,    0,    0,
    0,  676,   95,   95,   95,    0,   95,   95,   95,    0,
   95,    0,  689,  690,  691,    0,    0,   95,   95,    0,
    0,  145,  177,    0,   95,  167,  168,  169,  704,  170,
  171,  172,   95,  173,    0,  707,  178,  179,  180,    0,
    0,    0,  105,    0,  399,    0,    0,  175,    0,  101,
   32,    0,    0,    0,    0,  176,  728,    0,  730,  731,
    0,  733,  734,    0,  736,  737,    0,  739,  740,  741,
  742,  743,    0,    0,  746,    0,  748,  749,  750,  751,
    0,    0,  105,  105,  105,    0,  105,  167,  168,  169,
  759,  170,  171,  172,    0,  173,  764,    0,    0,    0,
    0,    0,  105,  174,  105,    0,    0,    0,    0,  175,
    0,    0,    0,    0,  770,    0,    0,  176,    0,    0,
   18,   18,    0,   95,   18,   18,    0,   18,    0,    0,
    0,    0,    0,  105,    0,  105,    0,   95,   95,   95,
  795,   18,   18,    0,    0,    0,    0,    0,  802,    0,
    0,  803,    0,    0,  804,    0,    0,  805,    0,    0,
    0,    0,    0,  806,  807,  105,  808,  105,   95,    0,
    0,    0,    0,  809,  810,    0,   18,    0,    0,    0,
    0,    0,    0,    0,    0,  820,  821,  822,   96,  228,
  144,  144,  228,    0,  144,  144,    0,  144,   26,   26,
    0,    0,   26,   26,  834,   26,    0,    0,  177,    0,
  228,  144,  144,  850,    0,    0,    0,    0,    0,   26,
   26,    0,  178,  179,  180,    0,    0,  101,    0,   31,
   31,    0,    0,   31,   31,    0,   31,    0,    0,    0,
    0,  228,    0,    0,  863,  182,  144,    0,    0,    0,
   31,   31,    0,    0,   26,    0,   21,   21,    0,    0,
   21,   21,    0,   21,  167,  168,  169,    0,  170,  171,
  172,  228,  173,  228,    0,    0,  183,   21,   21,    0,
    0,    0,    0,    0,    0,   31,  175,    0,    0,    0,
    0,   95,    0,    0,  176,    0,  888,  889,  890,  105,
  105,  105,    0,  105,  105,  105,    0,  105,  181,    0,
  105,  105,   21,    0,  105,  105,  105,  105,   32,   32,
  903,  105,   32,   32,    0,   32,    0,    0,    0,  105,
    0,  105,  105,    0,    0,  105,    0,    0,    0,   32,
   32,    0,    0,    0,    0,    0,    0,    0,    0,  105,
  105,    0,  105,  105,    0,    0,  105,  105,  105,  105,
  105,  105,  105,    0,  105,    0,  105,    0,    0,    0,
    0,    0,    0,    0,   32,    0,    0,  105,  105,  105,
    0,  105,  105,    0,    0,    0,  105,    0,  105,    0,
    0,  105,  105,  105,  105,  105,  105,  105,  105,  105,
  105,    0,  105,  105,    0,  105,  105,  105,  105,  105,
  105,  105,  105,  105,  105,  105,  105,  105,   95,    0,
  105,  228,  228,    0,  105,  105,  105,  105,  105,    0,
  105,  105,  105,  105,  105,  105,  105,    0,  105,    0,
    0,    0,  167,  168,  169,    0,  170,  171,  172,  105,
  173,    0,    0,    0,    0,    0,    0,  272,  174,    0,
  105,  105,  105,  105,  175,  105,    0,  105,  105,    0,
    0,    0,  176,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  228,  228,  228,    0,  228,  228,    0,
    0,  219,  228,    0,  228,    0,    0,  228,  228,  228,
  228,  228,  228,  228,  228,  228,  228,  220,  228,  228,
    0,  228,  228,  228,  228,  228,  228,  228,  228,  228,
  228,  228,  228,  228,  367,  367,  228,    0,    0,    0,
  228,  228,  228,  228,  228,  228,  228,  228,  228,  228,
  228,  228,  228,   95,  228,  478,  479,    0,    0,  221,
  222,  223,    0,    0,    0,  228,  224,  225,    0,  226,
  227,  228,  229,  177,    0,    0,  228,  228,  228,  228,
    0,    0,    0,  228,    0,    0,    0,  178,  179,  180,
    0,    0,    0,    0,    0,    0,  367,  367,  367,    0,
  367,  367,    0,    0,    0,  367,   55,  367,    0,    0,
  367,  367,  367,  367,  367,  367,  367,  367,  367,  367,
    0,  367,  367,    0,  367,  367,  367,  367,  367,  367,
  367,  367,  367,  367,  367,  367,  367,   56,    0,  367,
  286,    0,    0,  367,  367,  367,  367,  367,    0,  367,
  367,  367,  367,  367,  367,  367,   95,  367,    0,    0,
    0,  372,  372,    0,    0,    0,    1,    2,  367,   54,
    3,    4,    0,    5,    0,    0,    0,    0,    0,  367,
  367,  367,  367,    0,    0,    0,  367,    6,    7,    0,
    0,    0,    0,  480,  481,  482,  483,    0,    0,    0,
    0,    0,  484,  485,  486,  487,  488,  489,  490,  491,
  492,  493,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    8,  372,  372,  372,    0,  372,  372,    0,
    0,    0,  372,    0,  372,    0,    0,  372,  372,  372,
  372,  372,  372,  372,  372,  372,  372,    0,  372,  372,
    0,  372,  372,  372,  372,  372,  372,  372,  372,  372,
  372,  372,  372,  372,    0,    0,  372,    0,    0,    0,
  372,  372,  372,  372,  372,  219,  372,  372,  372,  372,
  372,  372,  372,   95,  372,    0,  357,  357,    0,    0,
    0,  220,    0,    0,    0,  372,   29,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  372,  372,  372,  372,
  197,    0,    0,  372,    0,    0,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,  221,  222,  223,    0,    0,    0,    0,
  224,  225,    0,  226,  227,  228,  229,    0,  357,  357,
  357,    0,  357,  357,    0,    0,    0,  357,    0,  357,
    0,    0,  357,  357,  357,  357,  357,  357,  357,  357,
  357,  357,    0,  357,  357,    0,  357,  357,  357,  357,
  357,  357,  357,  357,  357,  357,  357,  357,  357,  335,
  335,  357,    0,    0,    0,  357,  357,  357,  357,  357,
    0,  357,  357,  357,  357,  357,  357,  357,   95,  357,
    0,    0,    0,  167,  168,  169,    0,  170,  171,  172,
  357,  173,    0,    0,    0,  668,    0,    0,    0,    0,
    0,  357,  357,  357,  357,  175,    0,    0,  357,    0,
    0,    0,    0,  176,    0,    0,    0,    0,    0,    0,
    0,  335,  335,  335,    0,  335,  335,    0,    0,    0,
  335,   55,  335,    0,    0,  335,  335,  335,  335,  335,
  335,  335,  335,  335,  335,    0,  335,  335,    0,  335,
  335,  335,  335,  335,  335,  335,  335,  335,  335,  335,
  335,  335,   56,    0,  335,   85,    0,    0,  335,  335,
  335,  335,  335,    0,  335,  335,  335,  335,  335,  335,
  335,   95,  335,    0,    0,    0,  332,  332,    0,    0,
    0,    0,    0,  335,   54,    0,    0,    0,    0,    0,
    0,    0,    0,  104,  335,  335,  335,  335,    0,    0,
    0,  335,    0,    0,    0,    0,    0,   39,   40,    0,
   41,   42,    0,    0,   44,    0,   45,   46,   47,   48,
   49,    0,   50,    0,   29,   29,    0,    0,   29,   29,
    0,   29,    0,    0,    0,    0,   86,    0,  332,  332,
  332,    0,  332,  332,    0,   29,   29,  332,    0,  332,
    0,    0,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,    0,  332,  332,    0,  332,  332,  332,  332,
  332,  332,  332,  332,  332,  332,  332,  332,  332,   53,
   29,  332,    0,    0,    0,  332,  332,  332,  332,  332,
   85,  332,  332,  332,  332,  332,  332,  332,   95,  332,
    0,  333,  333,    0,    0,    0,   85,    0,    0,    0,
  332,   24,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  332,  332,  332,  332,   22,    0,    0,  332,    0,
    0,    0,    0,    0,   23,    0,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,   85,   85,
   85,    0,    0,    0,    0,   85,   85,    0,   85,   85,
   85,   85,    0,  333,  333,  333,    0,  333,  333,    0,
    0,   86,  333,    0,  333,    0,    0,  333,  333,  333,
  333,  333,  333,  333,  333,  333,  333,   86,  333,  333,
    0,  333,  333,  333,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  333,  334,  334,  333,    0,  414,  415,
  333,  333,  333,  333,  333,    0,  333,  333,  333,  333,
  333,  333,  333,   95,  333,    0,    0,    0,    0,   86,
   86,   86,    0,    0,    0,  333,   86,   86,    0,   86,
   86,   86,   86,    0,    0,    0,  333,  333,  333,  333,
    0,    0,    0,  333,    0,    0,    0,  155,    0,    0,
    0,    0,    0,    0,    0,    0,  334,  334,  334,    0,
  334,  334,    0,    0,    0,  334,   55,  334,    0,    0,
  334,  334,  334,  334,  334,  334,  334,  334,  334,  334,
    0,  334,  334,    0,  334,  334,  334,  334,  334,  334,
  334,  334,  334,  334,  334,  334,  334,   56,    0,  334,
    0,    0,    0,  334,  334,  334,  334,  334,    0,  334,
  334,  334,  334,  334,  334,  334,   95,  334,    0,    0,
    0,  369,  369,    0,    0,    0,    0,    0,  334,   54,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  334,
  334,  334,  334,    0,    0,    0,  334,   75,   76,    0,
    0,   77,   78,   79,   30,   31,   32,   33,   34,   35,
   36,   37,    0,    0,    0,    0,    0,    0,   43,   24,
   24,    0,    0,   24,   24,    0,   24,   80,    0,    0,
    0,    0,    0,  369,  369,  369,    0,  369,  369,    0,
   24,   24,  369,    0,  369,    0,    0,  369,  369,  369,
  369,  369,  369,  369,  369,  369,  369,    0,  369,  369,
    0,  369,  369,  369,  369,  369,  369,  369,  369,  369,
  369,  369,  369,  369,    0,   24,  369,    0,    0,    0,
  369,  369,  369,  369,  369,    0,  369,  369,  369,  369,
  369,  369,  369,   95,  369,    0,  361,  361,    0,    0,
    0,    0,  167,  168,  169,  369,  170,  171,  172,    0,
  173,    0,    0,    0,    0,    0,  369,  369,  369,  369,
   22,  399,    0,  369,  175,    0,    0,    0,    0,   23,
    0,    0,  176,    0,  153,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,    0,
  154,    0,    0,    0,    0,    0,    0,    0,  361,  361,
  361,    0,  361,  361,    0,    0,    0,  361,    0,  361,
    0,    0,  361,  361,  361,  361,  361,  361,  361,  361,
  361,  361,    0,  361,  361,    0,  361,  361,  361,  361,
  361,  361,  361,  361,  361,  361,  361,  361,  361,  353,
  353,  361,    0,    0,    0,  361,  361,  361,  361,  361,
    0,  361,  361,  361,  361,  361,  361,  361,   95,  361,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  361,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  361,  361,  361,  361,    0,    0,    0,  361,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  353,  353,  353,    0,  353,  353,    0,    0,    0,
  353,    0,  353,    0,    0,  353,  353,  353,  353,  353,
  353,  353,  353,  353,  353,    0,  353,  353,    0,  353,
  353,  353,  353,  353,  353,  353,  353,  353,  353,  353,
  353,  353,    0,    0,  353,    0,    0,    0,  353,  353,
  353,  353,  353,    0,  353,  353,  353,  353,  353,  353,
  353,    0,  353,    0,    0,    0,  342,  342,    0,    0,
    0,    0,    0,  353,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  353,  353,  353,  353,    0,   22,
    0,  353,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   25,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  342,  342,
  342,    0,  342,  342,    0,    0,    0,  342,    0,  342,
    0,    0,  342,  342,  342,  342,  342,  342,  342,  342,
  342,  342,  101,  342,  342,    0,  342,  342,  342,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,   30,
  182,  342,    0,    0,    0,  342,  342,  342,  342,  342,
    0,  342,  342,  342,  342,  342,  342,  342,    0,  342,
    0,  307,  307,    0,    0,    0,    0,    0,    0,    0,
  342,  183,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,  342,  342,  342,    0,    0,    0,  342,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  181,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  101,    0,  359,    0,    0,
    0,    0,   27,  307,  307,  307,    0,  307,  307,    0,
    0,    0,  307,  182,  307,    0,    0,  307,  307,  307,
  307,  307,  307,  307,  307,  307,  307,    0,  307,  307,
    0,  307,  307,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  307,  307,  183,  101,  307,    0,    0,    0,
  307,  307,  307,  307,  307,    0,  307,  307,  307,  307,
  307,  307,  307,  182,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  307,  181,    0,    0,    0,
    0,    0,  182,    0,    0,    0,  307,  307,  307,  307,
    0,    0,    0,  307,  183,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   22,   22,    0,
    0,   22,   22,  183,   22,    0,  599,  167,  168,  169,
    0,  170,  171,  172,    0,  173,  181,    0,   22,   22,
   25,   25,  272,  174,   25,   25,    0,   25,    0,  175,
    0,    0,    0,    0,    0,  181,    0,  176,    0,    0,
    0,   25,   25,  104,    0,    0,    0,    0,    0,    0,
  634,  635,    0,   22,   55,    0,    0,   39,   40,    0,
   41,   42,    0,    0,   44,    0,   45,   46,   47,   48,
   49,    0,   50,    0,    0,    0,   25,   30,   30,    0,
    0,   30,   30,    0,   30,   56,    0,    0,    0,    0,
   55,    0,    0,    0,    0,    0,   55,    0,   30,   30,
  167,  168,  169,    0,  170,  171,  172,    0,  173,  695,
  696,    0,  699,    0,    0,    0,  174,   54,    0,    0,
    0,   56,  175,    0,    0,    0,    0,   56,  177,   53,
  176,    0,    0,   30,    0,    0,    0,    0,    0,    0,
    0,    0,  178,  179,  180,  727,    0,    0,    0,    0,
  167,  168,  169,   54,  170,  171,  172,    0,  173,   54,
   27,   27,   55,    0,   27,   27,  174,   27,    0,  167,
  168,  169,  175,  170,  171,  172,    0,  173,   55,    0,
  176,   27,   27,    0,  272,  174,    0,    0,    0,    0,
    0,  175,    0,   56,    0,    0,    0,    0,    0,  176,
    0,    0,    0,    0,    0,  104,  774,    0,    0,   56,
  778,  779,    0,    0,    0,    0,   27,    0,    0,   39,
   40,  177,   41,   42,    0,   54,   44,    0,   45,   46,
   47,   48,   49,    0,   50,  178,  179,  180,    0,    0,
    0,   54,    0,    0,   55,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,    0,   23,    0,    0,
    0,  177,    0,    0,    0,   56,   24,   25,   26,   27,
   28,    0,    0,    0,    0,  178,  179,  180,    0,  832,
  177,   53,    0,    0,   22,  133,    0,    0,    0,    0,
   22,    0,    0,   23,  178,  179,  180,   54,    0,   23,
    0,    0,   24,   25,   26,   27,   28,    0,   24,   25,
   26,   27,   28,   29,    0,    0,  133,    0,   30,   31,
   32,   33,   34,   35,   36,   37,   38,   39,   40,    0,
   41,   42,   43,    0,   44,    0,   45,   46,   47,   48,
   49,    0,   50,    0,    0,    0,  427,    0,  133,    0,
    0,  134,    0,   51,   52,    0,   22,    0,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,   22,    0,   24,   25,   26,   27,   28,    0,
    0,   23,  134,    0,    0,    0,    0,    0,  434,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,   53,
    0,  104,   55,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  134,   39,   40,    0,   41,   42,
    0,    0,   44,    0,   45,   46,   47,   48,   49,   80,
   50,    0,    0,   56,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,    0,   23,    0,    0,
    0,    0,    0,    0,  436,  142,   24,   25,   26,   27,
   28,    0,    0,    0,    0,   54,    0,  104,   55,    0,
    0,    0,    0,    0,   55,    0,    0,    0,    0,    0,
   38,   39,   40,    0,   41,   42,    0,   53,   44,  133,
   45,   46,   47,   48,   49,    0,   50,    0,  133,   56,
    0,    0,    0,    0,    0,   56,    0,  133,  133,  133,
  133,  133,    0,    0,    0,    0,    0,    0,  133,    0,
    0,    0,    0,    0,    0,  502,    0,    0,    0,    0,
    0,   54,  133,  133,    0,  133,  133,   54,    0,  133,
    0,  133,  133,  133,  133,  133,  133,  133,    0,    0,
    0,    0,    0,   53,    0,  134,    0,    0,    0,    0,
    0,    0,    0,    0,  134,    0,    0,    0,    0,    0,
    0,    0,  720,  134,  134,  134,  134,  134,    0,    0,
    0,    0,    0,    0,  134,    0,    0,    0,    0,    0,
    0,   55,    0,    0,    0,    0,    0,    0,  134,  134,
    0,  134,  134,    0,  133,  134,   22,  134,  134,  134,
  134,  134,  134,  134,    0,   23,    0,    0,    0,    0,
    0,    0,   56,    0,   24,   25,   26,   27,   28,  609,
    0,    0,    0,    0,    0,   65,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   39,
   40,    0,   41,   42,   54,    0,   44,    0,   45,   46,
   47,   48,   49,    0,   50,    0,    0,    0,    0,    0,
  134,    0,   22,  292,    0,    0,    0,    0,   22,    0,
    0,   23,    0,    0,    0,    0,    0,   23,    0,    0,
   24,   25,   26,   27,   28,    0,   24,   25,   26,   27,
   28,  104,    0,    0,    0,    0,    0,    0,  141,    0,
    0,    0,    0,    0,    0,   39,   40,    0,   41,   42,
    0,   53,   44,    0,   45,   46,   47,   48,   49,    0,
   50,    0,  244,    0,    0,  293,  294,  295,    0,  296,
  297,    0,    0,    0,  298,    0,  299,    0,    0,  300,
  301,  302,  303,  304,  305,  306,  307,  308,  309,    0,
  310,  311,    0,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,  322,  323,  324,    0,  292,  325,    0,
    0,    0,  326,  327,  328,  329,  330,   53,  331,  332,
  333,  334,  335,  336,  337,   22,  338,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,  339,    0,  719,
    0,    0,    0,   24,   25,   26,   27,   28,  340,  341,
  342,  343,    0,    0,    0,  344,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  242,    0,    0,    0,  293,
  294,  295,    0,  296,  297,    0,    0,    0,  298,    0,
  299,    0,    0,  300,  301,  302,  303,  304,  305,  306,
  307,  308,  309,    0,  310,  311,    0,  312,  313,  314,
  315,  316,  317,  318,  319,  320,  321,  322,  323,  324,
  244,    0,  325,    0,    0,    0,  326,  327,  328,  329,
  330,    0,  331,  332,  333,  334,  335,  336,  337,    0,
  338,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  339,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   55,  340,  341,  342,  343,    0,    0,    0,  344,
    0,    0,    0,    0,    0,    0,    0,    0,  245,    0,
    0,    0,  244,  244,  244,    0,  244,  244,    0,    0,
    0,  244,   56,  244,    0,    0,  244,  244,  244,  244,
  244,  244,  244,  244,  244,  244,    0,  244,  244,    0,
  244,  244,  244,  244,  244,  244,  244,  244,  244,  244,
  244,  244,  244,  242,   54,  244,    0,    0,    0,  244,
  244,  244,  244,  244,    0,  244,  244,  244,  244,  244,
  244,  244,    0,  244,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  244,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   55,  244,  244,  244,  244,    0,
    0,    0,  244,    0,    0,    0,    0,    0,    0,    0,
    0,  243,    0,    0,    0,  242,  242,  242,    0,  242,
  242,    0,    0,    0,  242,   56,  242,    0,    0,  242,
  242,  242,  242,  242,  242,  242,  242,  242,  242,    0,
  242,  242,    0,  242,  242,  242,  242,  242,  242,  242,
  242,  242,  242,  242,  242,  242,  245,   54,  242,    0,
    0,    0,  242,  242,  242,  242,  242,    0,  242,  242,
  242,  242,  242,  242,  242,    0,  242,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  242,    0,    0,
    0,    0,    0,    0,    0,   22,    0,   55,  242,  242,
  242,  242,    0,    0,   23,  242,    0,    0,    0,  153,
    0,    0,    0,   24,   25,   26,   27,   28,  245,  245,
  245,    0,  245,  245,    0,  154,    0,  245,   56,  245,
    0,   55,  245,  245,  245,  245,  245,  245,  245,  245,
  245,  245,    0,  245,  245,    0,  245,  245,  245,  245,
  245,  245,  245,  245,  245,  245,  245,  245,  245,  243,
   54,  245,   56,    0,   55,  245,  245,  245,  245,  245,
    0,  245,  245,  245,  245,  245,  245,  245,    0,  245,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  245,    0,    0,    0,   54,   56,    0,    0,   22,    0,
   55,  245,  245,  245,  245,    0,    0,   23,  245,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,  243,  243,  243,    0,  243,  243,   54,  141,  200,
  243,   56,  243,    0,   55,  243,  243,  243,  243,  243,
  243,  243,  243,  243,  243,    0,  243,  243,    0,  243,
  243,  243,  243,  243,  243,  243,  243,  243,  243,  243,
  243,  243,    0,   54,  243,   56,    0,   55,  243,  243,
  243,  243,  243,    0,  243,  243,  243,  243,  243,  243,
  243,    0,  243,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  243,    0,   55,    0,   54,   56,    0,
   55,   22,    0,    0,  243,  243,  243,  243,    0,    0,
   23,  243,    0,    0,    0,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,   56,    0,   55,    0,
   54,   56,    0,   55,    0,  197,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,  447,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,   54,   56,
    0,    0,    0,   54,   56,   92,    0,    0,  197,  198,
    0,    0,    0,    0,    0,    0,    0,   23,  199,   55,
  449,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,   54,    0,   92,    0,    0,   54,  167,  168,  169,
  377,  170,  171,  172,   22,  378,    0,    0,    0,    0,
   56,    0,  839,   23,  379,   55,  380,    0,    0,  175,
    0,   55,   24,   25,   26,   27,   28,  176,    0,    0,
    0,  167,  168,  169,  377,  170,  171,  172,   22,  378,
    0,    0,   54,    0,   55,    0,   56,   23,  388,    0,
  380,   55,   56,  175,    0,    0,   24,   25,   26,   27,
   28,  176,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  197,  198,    0,    0,   56,    0,    0,   54,    0,
   23,  199,   56,    0,   54,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,  197,
  394,    0,    0,    0,   22,    0,    0,   54,   23,  395,
    0,    0,    0,   23,  254,    0,    0,   24,   25,   26,
   27,   28,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,  197,    0,    0,    0,    0,   22,    0,    0,
    0,   23,    0,    0,    0,    0,   23,  792,    0,    0,
   24,   25,   26,   27,   28,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  352,  352,    0,   22,
    0,    0,    0,    0,    0,   22,    0,    0,   23,    0,
    0,    0,    0,  719,   23,    0,    0,   24,   25,   26,
   27,   28,    0,   24,   25,   26,   27,   28,  197,    0,
    0,    0,    0,    0,    0,  197,    0,   23,    0,    0,
    0,    0,    0,    0,   23,    0,   24,   25,   26,   27,
   28,    0,    0,   24,   25,   26,   27,   28,  352,  352,
  352,    0,  352,  352,    0,    0,    0,  352,    0,  352,
    0,    0,  352,  352,  352,  352,  352,  352,  352,  352,
  352,  352,    0,  352,  352,    0,  352,  352,  352,  352,
  352,  352,  352,  352,  352,  352,  352,  352,  352,  326,
  326,  352,    0,    0,    0,  352,  352,  352,  352,  352,
    0,  352,  352,  352,  352,  352,  352,  352,    0,  352,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  352,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  352,  352,  352,  352,    0,    0,    0,  352,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  326,  326,  326,    0,  326,  326,    0,    0,    0,
  326,    0,  326,    0,    0,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  326,    0,  326,  326,    0,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  308,  308,  326,    0,    0,    0,  326,  326,
  326,  326,  326,    0,  326,  326,  326,  326,  326,  326,
  326,    0,  326,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  326,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  326,  326,  326,  326,    0,    0,
    0,  326,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  308,  308,  308,    0,  308,  308,
    0,    0,    0,  308,    0,  308,    0,    0,  308,  308,
  308,  308,  308,  308,  308,  308,  308,  308,    0,  308,
  308,    0,  308,  308,  308,  308,  308,  308,  308,  308,
  308,  308,  308,  308,  308,  313,  313,  308,    0,    0,
    0,  308,  308,  308,  308,  308,    0,  308,  308,  308,
  308,  308,  308,  308,    0,  308,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  308,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  308,  308,  308,
  308,    0,    0,    0,  308,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  313,  313,  313,
    0,  313,  313,    0,    0,    0,  313,    0,  313,    0,
    0,  313,  313,  313,  313,  313,  313,  313,  313,  313,
  313,    0,  313,  313,    0,  313,  313,  313,  313,  313,
  313,  313,  313,  313,  313,  313,  313,  313,  314,  314,
  313,    0,    0,    0,  313,  313,  313,  313,  313,    0,
  313,  313,  313,  313,  313,  313,  313,    0,  313,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  313,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  313,  313,  313,  313,    0,    0,    0,  313,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  314,  314,  314,    0,  314,  314,    0,    0,    0,  314,
    0,  314,    0,    0,  314,  314,  314,  314,  314,  314,
  314,  314,  314,  314,    0,  314,  314,    0,  314,  314,
  314,  314,  314,  314,  314,  314,  314,  314,  314,  314,
  314,  309,  309,  314,    0,    0,    0,  314,  314,  314,
  314,  314,    0,  314,  314,  314,  314,  314,  314,  314,
    0,  314,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  314,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  314,  314,  314,  314,    0,    0,    0,
  314,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  309,  309,  309,    0,  309,  309,    0,
    0,    0,  309,    0,  309,    0,    0,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,    0,  309,  309,
    0,  309,  309,  309,  309,  309,  309,  309,  309,  309,
  309,  309,  309,  309,  319,  319,  309,    0,    0,    0,
  309,  309,  309,  309,  309,    0,  309,  309,  309,  309,
  309,  309,  309,    0,  309,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  309,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,  309,  309,  309,
    0,    0,    0,  309,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  319,  319,  319,    0,
  319,  319,    0,    0,    0,  319,    0,  319,    0,    0,
  319,  319,  319,  319,  319,  319,  319,  319,  319,  319,
    0,  319,  319,    0,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  319,  319,  319,  341,  341,  319,
    0,    0,    0,  319,  319,  319,  319,  319,    0,  319,
  319,  319,  319,  319,  319,  319,    0,  319,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  319,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  319,
  319,  319,  319,    0,    0,    0,  319,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  341,
  341,  341,    0,  341,  341,    0,    0,    0,  341,    0,
  341,    0,    0,  341,  341,  341,  341,  341,  341,  341,
  341,  341,  341,    0,  341,  341,    0,  341,  341,  341,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
  337,  337,  341,    0,    0,    0,  341,  341,  341,  341,
  341,    0,  341,  341,  341,  341,  341,  341,  341,    0,
  341,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  341,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  341,  341,  341,  341,    0,    0,    0,  341,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  337,  337,  337,    0,  337,  337,    0,    0,
    0,  337,    0,  337,    0,    0,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,    0,  337,  337,    0,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  315,  315,  337,    0,    0,    0,  337,
  337,  337,  337,  337,    0,  337,  337,  337,  337,  337,
  337,  337,    0,  337,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  337,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  337,  337,  337,  337,    0,
    0,    0,  337,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  315,  315,  315,    0,  315,
  315,    0,    0,    0,  315,    0,  315,    0,    0,  315,
  315,  315,  315,  315,  315,  315,  315,  315,  315,    0,
  315,  315,    0,  315,  315,  315,  315,  315,  315,  315,
  315,  315,  315,  315,  315,  315,  310,  310,  315,    0,
    0,    0,  315,  315,  315,  315,  315,    0,  315,  315,
  315,  315,  315,  315,  315,    0,  315,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  315,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  315,  315,
  315,  315,    0,    0,    0,  315,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  310,  310,
  310,    0,  310,  310,    0,    0,    0,  310,    0,  310,
    0,    0,  310,  310,  310,  310,  310,  310,  310,  310,
  310,  310,    0,  310,  310,    0,  310,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,  310,  310,  311,
  311,  310,    0,    0,    0,  310,  310,  310,  310,  310,
    0,  310,  310,  310,  310,  310,  310,  310,    0,  310,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  310,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  310,  310,  310,  310,    0,    0,    0,  310,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  311,  311,  311,    0,  311,  311,    0,    0,    0,
  311,    0,  311,    0,    0,  311,  311,  311,  311,  311,
  311,  311,  311,  311,  311,    0,  311,  311,    0,  311,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  311,  311,  316,  316,  311,    0,    0,    0,  311,  311,
  311,  311,  311,    0,  311,  311,  311,  311,  311,  311,
  311,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  311,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  311,  311,  311,  311,    0,    0,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  316,  316,  316,    0,  316,  316,
    0,    0,    0,  316,    0,  316,    0,    0,  316,  316,
  316,  316,  316,  316,  316,  316,  316,  316,    0,  316,
  316,    0,  316,  316,  316,  316,  316,  316,  316,  316,
  316,  316,  316,  316,  316,  324,  324,  316,    0,    0,
    0,  316,  316,  316,  316,  316,    0,  316,  316,  316,
  316,  316,  316,  316,    0,  316,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  316,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  316,  316,  316,
  316,    0,    0,    0,  316,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  324,  324,  324,
    0,  324,  324,    0,    0,    0,  324,    0,  324,    0,
    0,  324,  324,  324,  324,  324,  324,  324,  324,  324,
  324,    0,  324,  324,    0,  324,  324,  324,  324,  324,
  324,  324,  324,  324,  324,  324,  324,  324,  317,  317,
  324,    0,    0,    0,  324,  324,  324,  324,  324,    0,
  324,  324,  324,  324,  324,  324,  324,    0,  324,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  324,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  324,  324,  324,  324,    0,    0,    0,  324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  317,  317,  317,    0,  317,  317,    0,    0,    0,  317,
    0,  317,    0,    0,  317,  317,  317,  317,  317,  317,
  317,  317,  317,  317,    0,  317,  317,    0,  317,  317,
  317,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  317,  320,  320,  317,    0,    0,    0,  317,  317,  317,
  317,  317,    0,  317,  317,  317,  317,  317,  317,  317,
    0,  317,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  317,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  317,  317,  317,  317,    0,    0,    0,
  317,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  320,  320,  320,    0,  320,  320,    0,
    0,    0,  320,    0,  320,    0,    0,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,    0,  320,  320,
    0,  320,  320,  320,  320,  320,  320,  320,  320,  320,
  320,  320,  320,  320,  338,  338,  320,    0,    0,    0,
  320,  320,  320,  320,  320,    0,  320,  320,  320,  320,
  320,  320,  320,    0,  320,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  320,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,  320,  320,  320,
    0,    0,    0,  320,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  338,  338,  338,    0,
  338,  338,    0,    0,    0,  338,    0,  338,    0,    0,
  338,  338,  338,  338,  338,  338,  338,  338,  338,  338,
    0,  338,  338,    0,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,  312,  312,  338,
    0,    0,    0,  338,  338,  338,  338,  338,    0,  338,
  338,  338,  338,  338,  338,  338,    0,  338,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  338,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  338,
  338,  338,  338,    0,    0,    0,  338,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  312,  312,    0,  312,  312,    0,    0,    0,  312,    0,
  312,    0,    0,  312,  312,  312,  312,  312,  312,  312,
  312,  312,  312,    0,  312,  312,    0,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  312,  312,  312,  312,
  318,  318,  312,    0,    0,    0,  312,  312,  312,  312,
  312,    0,  312,  312,  312,  312,  312,  312,  312,    0,
  312,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  312,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,  312,  312,  312,    0,    0,    0,  312,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  318,  318,  318,    0,  318,  318,    0,    0,
    0,  318,    0,  318,    0,    0,  318,  318,  318,  318,
  318,  318,  318,  318,  318,  318,    0,  318,  318,    0,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  318,
  318,  318,  318,  321,  321,  318,    0,    0,    0,  318,
  318,  318,  318,  318,    0,  318,  318,  318,  318,  318,
  318,  318,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  318,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  318,  318,  318,  318,    0,
    0,    0,  318,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  321,  321,  321,    0,  321,
  321,    0,    0,    0,  321,    0,  321,    0,    0,  321,
  321,  321,  321,  321,  321,  321,  321,  321,  321,    0,
  321,  321,    0,  321,  321,  321,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  321,  322,  322,  321,    0,
    0,    0,  321,  321,  321,  321,  321,    0,  321,  321,
  321,  321,  321,  321,  321,    0,  321,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  321,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  321,  321,
  321,  321,    0,    0,    0,  321,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  322,  322,
  322,    0,  322,  322,    0,    0,    0,  322,    0,  322,
    0,    0,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,    0,  322,  322,    0,  322,  322,  322,  322,
  322,  322,  322,  322,  322,  322,  322,  322,  322,  323,
  323,  322,    0,    0,    0,  322,  322,  322,  322,  322,
    0,  322,  322,  322,  322,  322,  322,  322,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  322,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  322,  322,  322,  322,    0,    0,    0,  322,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  323,  323,  323,    0,  323,  323,    0,    0,    0,
  323,    0,  323,    0,    0,  323,  323,  323,  323,  323,
  323,  323,  323,  323,  323,    0,  323,  323,    0,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  323,  323,
  323,  323,  292,    0,  323,    0,    0,    0,  323,  323,
  323,  323,  323,    0,  323,  323,  323,  323,  323,  323,
  323,    0,  323,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  323,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  323,  323,  323,  323,    0,    0,
    0,  323,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  293,  294,  295,    0,  296,  297,
    0,    0,    0,  298,    0,  299,    0,    0,  300,  301,
  302,  303,  304,  305,  306,  307,  308,  309,    0,  310,
  311,    0,  312,  313,  314,  315,  316,  317,  318,  319,
  320,  321,  322,  323,  324,  248,    0,  325,    0,    0,
    0,  326,  327,  328,  329,  330,    0,  331,  332,  333,
  334,  335,  336,  337,    0,  338,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  339,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  340,  341,  342,
  343,    0,    0,    0,  344,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  248,  248,  248,
    0,  248,  248,    0,    0,    0,  248,    0,  248,    0,
    0,  248,  248,  248,  248,  248,  248,  248,  248,  248,
  248,    0,  248,  248,    0,  248,  248,  248,  248,  248,
  248,  248,  248,  248,  248,  248,  248,  248,  249,    0,
  248,    0,    0,    0,  248,  248,  248,  248,  248,    0,
  248,  248,  248,  248,  248,  248,  248,    0,  248,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  248,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  248,  248,  248,  248,    0,    0,    0,  248,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  249,  249,  249,    0,  249,  249,    0,    0,    0,  249,
    0,  249,    0,    0,  249,  249,  249,  249,  249,  249,
  249,  249,  249,  249,    0,  249,  249,    0,  249,  249,
  249,  249,  249,  249,  249,  249,  249,  249,  249,  249,
  249,  250,    0,  249,    0,    0,    0,  249,  249,  249,
  249,  249,    0,  249,  249,  249,  249,  249,  249,  249,
    0,  249,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  249,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  249,  249,  249,  249,    0,    0,    0,
  249,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  250,  250,  250,    0,  250,  250,    0,
    0,    0,  250,    0,  250,    0,    0,  250,  250,  250,
  250,  250,  250,  250,  250,  250,  250,    0,  250,  250,
    0,  250,  250,  250,  250,  250,  250,  250,  250,  250,
  250,  250,  250,  250,  251,    0,  250,    0,    0,    0,
  250,  250,  250,  250,  250,    0,  250,  250,  250,  250,
  250,  250,  250,    0,  250,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  250,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  250,  250,  250,  250,
    0,    0,    0,  250,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  251,  251,  251,    0,
  251,  251,    0,    0,    0,  251,    0,  251,    0,    0,
  251,  251,  251,  251,  251,  251,  251,  251,  251,  251,
    0,  251,  251,    0,  251,  251,  251,  251,  251,  251,
  251,  251,  251,  251,  251,  251,  251,    0,    0,  251,
    0,    0,    0,  251,  251,  251,  251,  251,    0,  251,
  251,  251,  251,  251,  251,  251,    0,  251,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  251,    0,
    0,    0,    0,    0,    0,    0,    0,  296,    0,  251,
  251,  251,  251,    0,  299,    0,  251,  300,  301,  302,
  303,  304,  305,  306,  307,  308,  309,    0,  310,  311,
    0,  312,  313,  314,  315,  316,  317,  318,  319,  320,
  321,  322,  323,  324,    0,    0,  325,    0,    0,    0,
  326,  327,  328,  329,  330,    0,  331,  332,  333,  334,
  335,  336,  337,    0,  338,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  339,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  340,  341,  342,  343,
    0,    0,    0,  344,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   58,   62,    6,   33,   65,   42,  262,  263,  264,
   40,  348,  528,  350,  345,   60,   33,  123,   40,   40,
   40,   40,  123,  235,  123,  123,  123,  765,  123,  110,
  285,  123,  115,  123,  276,  280,   44,  718,   44,  281,
  162,  323,   99,   42,  782,   60,   44,   54,   42,   44,
   19,   42,  213,  578,   61,   62,  313,  218,   65,   41,
  538,   68,   44,  182,  183,   68,  323,  148,   40,   40,
  151,   62,   44,   44,   44,   82,   91,  160,  123,   44,
  818,   41,  121,   44,   44,   93,  291,  292,   61,   44,
   97,  313,   91,   61,  216,  102,  156,   62,  105,   93,
  107,  323,  109,  599,  107,   62,  109,  114,  123,  790,
   61,  118,   81,  235,   83,  122,   85,  124,  323,  125,
  281,  266,  379,   40,   81,  164,  381,  125,  135,  136,
  125,  138,  215,  214,  379,  254,   61,  379,  634,  635,
  272,  286,  124,   60,  356,  670,  115,  186,  673,  188,
  270,  271,  630,  125,  125,  125,  260,  379,  165,  166,
  282,   61,    6,    7,  125,  257,   42,  504,  274,  907,
  125,  295,  296,  274,   91,  274,  274,  274,  509,  274,
  187,   61,  189,  306,  274,   42,  523,   44,   42,  291,
  292,  403,   41,  295,  296,   44,   42,  534,   44,  695,
  696,   41,   42,  699,  700,  212,  123,  246,  281,   60,
  333,  334,   41,   40,  301,   44,  303,   61,   62,  321,
  259,   65,  261,  310,   68,   41,  417,  234,   44,  232,
  421,  727,  257,  424,   40,   41,  296,   42,   44,  296,
   91,  248,  433,  288,  257,  274,  724,  254,  277,  264,
  272,  272,  272,  272,   40,  262,  263,  274,  273,   41,
   61,  105,   44,  107,   42,  109,   44,  282,  283,  284,
  285,  286,  123,  528,  611,  530,  274,  532,  774,  315,
  291,  292,  778,  779,  621,  342,  623,   40,   81,  296,
   83,   84,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  315,   41,
  788,  260,   44,  274,  321,  322,  260,  572,  325,    0,
   41,  799,  800,   44,   40,  841,  119,  120,   41,  123,
  123,   44,  125,  340,  341,  342,  832,  344,  257,  342,
  257,  258,  259,  267,  261,  262,  263,   42,  265,   44,
  380,   42,  412,   44,  361,  272,  273,   40,   41,   40,
  838,   44,  279,  291,  292,  267,  381,  295,  296,  706,
  287,  306,   41,   41,  852,   44,   44,  855,  715,   41,
   42,  257,  258,  259,  123,  261,  262,  263,   41,  265,
  426,   44,   42,  379,   44,  123,  272,  273,   40,   40,
  123,  879,  880,  279,  411,  412,   41,   41,  411,   44,
  417,  287,  257,  264,  421,  893,   41,  424,   60,   44,
  427,   41,  273,   42,   44,   44,  433,  434,   42,  436,
   44,  282,  283,  284,  285,  286,   41,   42,  323,  293,
  447,   41,   42,  500,   41,   42,  453,   41,   42,   91,
   40,   40,  296,  307,  308,   40,  310,  311,   44,   58,
  314,  378,  316,  317,  318,  319,  320,   58,  322,   62,
  477,   58,   40,   61,  274,  392,  393,  394,  257,   44,
  379,  123,   40,  132,  379,   44,  260,  494,   44,  257,
   61,  498,  499,  500,  332,  313,  499,  500,  342,  754,
  755,  274,  378,   41,  420,  274,  423,  257,  379,   44,
  387,  518,  519,  520,  387,  387,  392,  393,  394,  257,
  379,   44,   44,  530,    0,  379,  379,   40,  124,  380,
  273,  387,  181,  182,  183,   44,  543,   44,  793,   44,
  543,   44,   44,   44,   44,  605,  387,  196,  387,  274,
  387,  202,  387,    0,  397,  398,  399,  400,  401,  402,
  403,  404,  405,  406,  387,  387,  387,  411,  412,  576,
  387,  578,  387,  387,  581,  582,  583,  584,  585,  586,
  587,  588,  589,  590,  591,  592,  841,  268,  269,  387,
  239,  272,  273,   40,  275,  602,  260,  604,  605,  257,
  249,  250,  251,    0,  379,  254,   62,  379,  289,  290,
  257,  694,  257,  257,  379,  257,  258,  259,  266,  261,
  262,  263,  271,  265,   44,  632,   40,    0,   44,   44,
  272,  273,   44,   44,   60,   44,   44,  279,   44,   44,
   44,   44,   44,  324,  293,  287,  295,  257,  297,  771,
   44,   44,   44,   44,  498,  499,  500,   44,   44,   44,
   44,  721,   44,  670,   44,   91,  673,  316,  317,  318,
  319,  320,   91,  257,  323,  257,  387,  326,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  772,
  773,   44,   44,  776,  777,   44,  703,  123,   44,  543,
  703,   44,  824,  825,   44,   44,  828,  829,   44,   44,
   44,  718,  719,   44,  257,   40,   44,  368,  367,  257,
  258,  259,   44,  261,  262,  263,   44,  265,   44,   44,
   44,   44,   91,   44,  424,   60,  378,   44,  276,   41,
  379,  279,  864,  826,  866,  867,   44,  830,  831,  287,
  392,  393,  394,  379,  387,  387,  407,   44,  602,   44,
  604,  605,  379,  885,   44,  416,   91,  260,  419,  420,
   44,  422,  423,   40,  425,  426,  327,  428,  429,  430,
  431,  432,    0,  790,  435,  868,  437,  438,  439,  440,
  797,  794,  268,  269,  257,  387,  272,  273,  123,  275,
  449,  379,   42,  257,   44,  379,  257,   44,   44,   93,
  380,   40,   40,  289,  290,   40,  327,  327,  257,  257,
   60,  268,  269,  260,   40,  272,  387,  387,  275,  327,
  387,  257,  258,  259,   44,  261,  262,  263,    0,  265,
  272,  272,  289,  290,  272,  496,  272,  273,  324,   40,
  272,   91,    0,  279,  272,   10,   86,  530,  196,  703,
    0,  287,  195,   19,  263,  266,  230,  212,  517,  354,
   97,  268,  269,   58,  234,  272,  273,  324,  275,  754,
  887,  755,  836,  123,  404,  347,  347,  797,  895,  896,
  897,    0,  289,  290,  545,  268,  269,  692,  549,  272,
  273,  552,  275,  532,  555,  404,  790,   -1,   -1,   -1,
  561,  562,   -1,  564,   -1,   -1,  289,  290,    0,   -1,
  569,  570,  571,   -1,  573,  574,   -1,  324,   -1,   -1,
   -1,  580,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,  593,  594,  595,   -1,   -1,  272,  273,   -1,
   -1,  324,  378,   -1,  279,  257,  258,  259,  607,  261,
  262,  263,  287,  265,   -1,  614,  392,  393,  394,   -1,
   -1,   -1,    0,   -1,  276,   -1,   -1,  279,   -1,   42,
    0,   -1,   -1,   -1,   -1,  287,  637,   -1,  639,  640,
   -1,  642,  643,   -1,  645,  646,   -1,  648,  649,  650,
  651,  652,   -1,   -1,  655,   -1,  657,  658,  659,  660,
   -1,   -1,   40,   41,   42,   -1,   44,  257,  258,  259,
  671,  261,  262,  263,   -1,  265,  675,   -1,   -1,   -1,
   -1,   -1,   60,  273,   62,   -1,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,  693,   -1,   -1,  287,   -1,   -1,
  268,  269,   -1,  378,  272,  273,   -1,  275,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   93,   -1,  392,  393,  394,
  721,  289,  290,   -1,   -1,   -1,   -1,   -1,  729,   -1,
   -1,  732,   -1,   -1,  735,   -1,   -1,  738,   -1,   -1,
   -1,   -1,   -1,  744,  745,  123,  747,  125,  423,   -1,
   -1,   -1,   -1,  752,  753,   -1,  324,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  766,  767,  768,   40,   41,
  268,  269,   44,   -1,  272,  273,   -1,  275,  268,  269,
   -1,   -1,  272,  273,  783,  275,   -1,   -1,  378,   -1,
   62,  289,  290,  794,   -1,   -1,   -1,   -1,   -1,  289,
  290,   -1,  392,  393,  394,   -1,   -1,   42,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,
   -1,   93,   -1,   -1,  823,   60,  324,   -1,   -1,   -1,
  289,  290,   -1,   -1,  324,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,  257,  258,  259,   -1,  261,  262,
  263,  123,  265,  125,   -1,   -1,   91,  289,  290,   -1,
   -1,   -1,   -1,   -1,   -1,  324,  279,   -1,   -1,   -1,
   -1,   40,   -1,   -1,  287,   -1,  875,  876,  877,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,  123,   -1,
  268,  269,  324,   -1,  272,  273,  274,  275,  268,  269,
  899,  279,  272,  273,   -1,  275,   -1,   -1,   -1,  287,
   -1,  289,  290,   -1,   -1,  293,   -1,   -1,   -1,  289,
  290,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,   -1,   -1,  314,  315,  316,  317,
  318,  319,  320,   -1,  322,   -1,  324,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  324,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   40,   -1,
  378,  273,  274,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  407,
  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
  418,  419,  420,  421,  279,  423,   -1,  425,  426,   -1,
   -1,   -1,  287,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,  260,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  276,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,  387,  388,  389,  390,  391,
  392,  393,  394,   40,  396,  261,  262,   -1,   -1,  318,
  319,  320,   -1,   -1,   -1,  407,  325,  326,   -1,  328,
  329,  330,  331,  378,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,  392,  393,  394,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   60,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   91,   -1,  378,
  125,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   40,  396,   -1,   -1,
   -1,  273,  274,   -1,   -1,   -1,  268,  269,  407,  123,
  272,  273,   -1,  275,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,   -1,  425,  289,  290,   -1,
   -1,   -1,   -1,  399,  400,  401,  402,   -1,   -1,   -1,
   -1,   -1,  408,  409,  410,  411,  412,  413,  414,  415,
  416,  417,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  324,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,  260,  388,  389,  390,  391,
  392,  393,  394,   40,  396,   -1,  273,  274,   -1,   -1,
   -1,  276,   -1,   -1,   -1,  407,    0,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
  264,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,  318,  319,  320,   -1,   -1,   -1,   -1,
  325,  326,   -1,  328,  329,  330,  331,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   40,  396,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
  407,  265,   -1,   -1,   -1,  379,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,  279,   -1,   -1,  425,   -1,
   -1,   -1,   -1,  287,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   60,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   91,   -1,  378,  125,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   40,  396,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,  407,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  293,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,
  320,   -1,  322,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,   -1,   -1,   -1,   -1,  125,   -1,  335,  336,
  337,   -1,  339,  340,   -1,  289,  290,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  379,
  324,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
  260,  388,  389,  390,  391,  392,  393,  394,   40,  396,
   -1,  273,  274,   -1,   -1,   -1,  276,   -1,   -1,   -1,
  407,    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,  264,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  318,  319,
  320,   -1,   -1,   -1,   -1,  325,  326,   -1,  328,  329,
  330,  331,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,  260,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,  276,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,   -1,  347,  348,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   40,  396,   -1,   -1,   -1,   -1,  318,
  319,  320,   -1,   -1,   -1,  407,  325,  326,   -1,  328,
  329,  330,  331,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   41,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   60,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   91,   -1,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   40,  396,   -1,   -1,
   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,  407,  123,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,   -1,  425,  291,  292,   -1,
   -1,  295,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,   -1,   -1,   -1,   -1,   -1,   -1,  312,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  321,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
  289,  290,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,  324,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   40,  396,   -1,  273,  274,   -1,   -1,
   -1,   -1,  257,  258,  259,  407,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
  264,  276,   -1,  425,  279,   -1,   -1,   -1,   -1,  273,
   -1,   -1,  287,   -1,  278,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   40,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,   -1,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,    0,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   42,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,    0,
   60,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   42,   -1,   44,   -1,   -1,
   -1,   -1,    0,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   60,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   91,   42,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   60,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,  123,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,
   -1,  272,  273,   91,  275,   -1,  497,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,  123,   -1,  289,  290,
  268,  269,  272,  273,  272,  273,   -1,  275,   -1,  279,
   -1,   -1,   -1,   -1,   -1,  123,   -1,  287,   -1,   -1,
   -1,  289,  290,  293,   -1,   -1,   -1,   -1,   -1,   -1,
  541,  542,   -1,  324,   60,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,
  320,   -1,  322,   -1,   -1,   -1,  324,  268,  269,   -1,
   -1,  272,  273,   -1,  275,   91,   -1,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   -1,   -1,   60,   -1,  289,  290,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  600,
  601,   -1,  603,   -1,   -1,   -1,  273,  123,   -1,   -1,
   -1,   91,  279,   -1,   -1,   -1,   -1,   91,  378,  379,
  287,   -1,   -1,  324,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  392,  393,  394,  636,   -1,   -1,   -1,   -1,
  257,  258,  259,  123,  261,  262,  263,   -1,  265,  123,
  268,  269,   60,   -1,  272,  273,  273,  275,   -1,  257,
  258,  259,  279,  261,  262,  263,   -1,  265,   60,   -1,
  287,  289,  290,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,   91,   -1,   -1,   -1,   -1,   -1,  287,
   -1,   -1,   -1,   -1,   -1,  293,  697,   -1,   -1,   91,
  701,  702,   -1,   -1,   -1,   -1,  324,   -1,   -1,  307,
  308,  378,  310,  311,   -1,  123,  314,   -1,  316,  317,
  318,  319,  320,   -1,  322,  392,  393,  394,   -1,   -1,
   -1,  123,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,  378,   -1,   -1,   -1,   91,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,  392,  393,  394,   -1,  780,
  378,  379,   -1,   -1,  264,   60,   -1,   -1,   -1,   -1,
  264,   -1,   -1,  273,  392,  393,  394,  123,   -1,  273,
   -1,   -1,  282,  283,  284,  285,  286,   -1,  282,  283,
  284,  285,  286,  293,   -1,   -1,   91,   -1,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,  308,   -1,
  310,  311,  312,   -1,  314,   -1,  316,  317,  318,  319,
  320,   -1,  322,   -1,   -1,   -1,  362,   -1,  123,   -1,
   -1,   60,   -1,  333,  334,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,  282,  283,  284,  285,  286,   -1,
   -1,  273,   91,   -1,   -1,   -1,   -1,   -1,  362,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,  379,
   -1,  293,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  307,  308,   -1,  310,  311,
   -1,   -1,  314,   -1,  316,  317,  318,  319,  320,  321,
  322,   -1,   -1,   91,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,  362,   41,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,  123,   -1,  293,   60,   -1,
   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
  306,  307,  308,   -1,  310,  311,   -1,  379,  314,  264,
  316,  317,  318,  319,  320,   -1,  322,   -1,  273,   91,
   -1,   -1,   -1,   -1,   -1,   91,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,  123,  307,  308,   -1,  310,  311,  123,   -1,  314,
   -1,  316,  317,  318,  319,  320,  321,  322,   -1,   -1,
   -1,   -1,   -1,  379,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   41,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,
   -1,  310,  311,   -1,  379,  314,  264,  316,  317,  318,
  319,  320,  321,  322,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,  282,  283,  284,  285,  286,  125,
   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,
  308,   -1,  310,  311,  123,   -1,  314,   -1,  316,  317,
  318,  319,  320,   -1,  322,   -1,   -1,   -1,   -1,   -1,
  379,   -1,  264,  273,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
  282,  283,  284,  285,  286,   -1,  282,  283,  284,  285,
  286,  293,   -1,   -1,   -1,   -1,   -1,   -1,  294,   -1,
   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,
   -1,  379,  314,   -1,  316,  317,  318,  319,  320,   -1,
  322,   -1,  125,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,  273,  378,   -1,
   -1,   -1,  382,  383,  384,  385,  386,  379,  388,  389,
  390,  391,  392,  393,  394,  264,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,  407,   -1,  278,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,  335,
  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,
  346,   -1,   -1,  349,  350,  351,  352,  353,  354,  355,
  356,  357,  358,   -1,  360,  361,   -1,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  375,
  273,   -1,  378,   -1,   -1,   -1,  382,  383,  384,  385,
  386,   -1,  388,  389,  390,  391,  392,  393,  394,   -1,
  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,  418,  419,  420,  421,   -1,   -1,   -1,  425,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   91,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  273,  123,  378,   -1,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,  418,  419,  420,  421,   -1,
   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  125,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   91,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,   -1,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,  273,  123,  378,   -1,
   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   60,  418,  419,
  420,  421,   -1,   -1,  273,  425,   -1,   -1,   -1,  278,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  335,  336,
  337,   -1,  339,  340,   -1,  294,   -1,  344,   91,  346,
   -1,   60,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,   -1,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  123,  378,   91,   -1,   60,  382,  383,  384,  385,  386,
   -1,  388,  389,  390,  391,  392,  393,  394,   -1,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  407,   -1,   -1,   -1,  123,   91,   -1,   -1,  264,   -1,
   60,  418,  419,  420,  421,   -1,   -1,  273,  425,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,  335,  336,  337,   -1,  339,  340,  123,  294,  125,
  344,   91,  346,   -1,   60,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,  123,  378,   91,   -1,   60,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   60,   -1,  123,   91,   -1,
   60,  264,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
  273,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   91,   -1,   60,   -1,
  123,   91,   -1,   60,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,  309,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  123,   91,
   -1,   -1,   -1,  123,   91,  125,   -1,   -1,  264,  265,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   60,
  309,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,  123,   -1,  125,   -1,   -1,  123,  257,  258,  259,
  260,  261,  262,  263,  264,  265,   -1,   -1,   -1,   -1,
   91,   -1,   93,  273,  274,   60,  276,   -1,   -1,  279,
   -1,   60,  282,  283,  284,  285,  286,  287,   -1,   -1,
   -1,  257,  258,  259,  260,  261,  262,  263,  264,  265,
   -1,   -1,  123,   -1,   60,   -1,   91,  273,  274,   -1,
  276,   60,   91,  279,   -1,   -1,  282,  283,  284,  285,
  286,  287,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,  265,   -1,   -1,   91,   -1,   -1,  123,   -1,
  273,  274,   91,   -1,  123,   -1,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,  264,
  265,   -1,   -1,   -1,  264,   -1,   -1,  123,  273,  274,
   -1,   -1,   -1,  273,  123,   -1,   -1,  282,  283,  284,
  285,  286,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
  282,  283,  284,  285,  286,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,  264,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  278,  273,   -1,   -1,  282,  283,  284,
  285,  286,   -1,  282,  283,  284,  285,  286,  264,   -1,
   -1,   -1,   -1,   -1,   -1,  264,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,  282,  283,  284,  285,
  286,   -1,   -1,  282,  283,  284,  285,  286,  335,  336,
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
  374,  375,  273,   -1,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,   -1,  378,   -1,   -1,
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
  369,  370,  371,  372,  373,  374,  375,   -1,   -1,  378,
   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  339,   -1,  418,
  419,  420,  421,   -1,  346,   -1,  425,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   -1,  378,   -1,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,
  };

#line 1330 "Iril/IR/IR.jay"

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
