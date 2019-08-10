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
case 89:
#line 327 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 90:
#line 331 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType (false, (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 335 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 92:
#line 342 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 93:
#line 346 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 95:
#line 351 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 98:
#line 357 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 99:
#line 358 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 100:
#line 359 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 101:
#line 360 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 102:
#line 364 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 103:
#line 368 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 104:
#line 372 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 105:
#line 376 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 106:
#line 380 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 384 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 108:
#line 388 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 109:
#line 395 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 399 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 407 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 113:
  case_113();
  break;
case 114:
  case_114();
  break;
case 124:
#line 439 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 125:
#line 443 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 126:
#line 447 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 127:
#line 451 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 128:
#line 455 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 129:
#line 462 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 130:
#line 466 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 131:
#line 470 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 136:
#line 481 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 137:
#line 488 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 138:
#line 492 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 139:
#line 496 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 140:
#line 500 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 504 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 145:
#line 514 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 146:
#line 515 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 147:
#line 522 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 526 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 533 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 537 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 541 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 152:
#line 545 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 154:
#line 553 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 557 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 156:
#line 558 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 157:
#line 559 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 158:
#line 560 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 159:
#line 561 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 160:
#line 562 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 161:
#line 563 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 162:
#line 564 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 163:
#line 565 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 164:
#line 566 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 165:
#line 567 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Byval; }
  break;
case 166:
#line 571 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 167:
#line 575 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 175:
#line 598 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 176:
#line 599 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 177:
#line 600 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 178:
#line 601 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 179:
#line 602 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 180:
#line 603 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 181:
#line 604 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 182:
#line 605 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 183:
#line 606 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 184:
#line 607 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 185:
#line 611 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 186:
#line 612 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 187:
#line 613 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 188:
#line 614 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 189:
#line 615 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 190:
#line 616 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 191:
#line 617 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 192:
#line 618 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 193:
#line 619 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 194:
#line 620 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 195:
#line 621 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 196:
#line 622 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 197:
#line 623 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 199:
#line 625 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 200:
#line 626 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 201:
#line 630 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 205:
#line 640 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 206:
#line 644 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 207:
#line 648 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 652 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 656 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 660 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 664 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 668 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 672 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 214:
#line 676 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-2+yyTop]);
    }
  break;
case 216:
#line 684 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 217:
#line 685 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 218:
#line 686 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 219:
#line 687 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 220:
#line 688 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 221:
#line 689 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 222:
#line 690 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 223:
#line 691 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 224:
#line 692 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 225:
#line 699 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 706 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 710 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 228:
#line 717 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 724 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 728 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 735 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 743 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 234:
#line 750 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 754 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 765 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 769 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 776 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 780 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 787 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 791 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 243:
#line 795 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 799 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 245:
#line 806 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 810 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 817 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 821 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 825 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 829 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 837 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 253:
#line 838 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 254:
#line 845 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 856 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 257:
#line 860 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 258:
#line 864 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 259:
#line 868 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 260:
#line 872 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 261:
#line 876 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 262:
#line 880 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 264:
#line 885 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 265:
#line 889 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 266:
#line 893 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 267:
#line 897 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 268:
#line 901 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 273:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 928 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 276:
#line 935 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 964 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 285:
#line 971 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 978 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 982 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 993 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 997 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1001 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1005 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 293:
#line 1009 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 295:
#line 1017 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1021 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1025 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1029 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1037 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1041 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 302:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 303:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 305:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 306:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 308:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 309:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 310:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 311:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 312:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 313:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 314:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 315:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 316:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 317:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 318:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 319:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 320:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 321:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 322:
#line 1129 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 323:
#line 1133 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 324:
#line 1137 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1141 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1145 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1149 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1153 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1157 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1161 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1165 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1169 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1173 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1177 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1181 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1185 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1189 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1193 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1197 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1201 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1205 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1209 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 343:
#line 1213 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1217 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 345:
#line 1221 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 346:
#line 1225 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 347:
#line 1229 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 348:
#line 1233 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1237 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1241 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1245 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1249 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1253 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1257 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1261 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1265 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1269 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1273 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1277 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 360:
#line 1281 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 361:
#line 1285 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1289 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 363:
#line 1293 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 364:
#line 1297 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1301 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1305 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1309 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 368:
#line 1313 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 369:
#line 1317 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 370:
#line 1321 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 371:
#line 1325 "Iril/IR/IR.jay"
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

void case_113()
#line 412 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_114()
#line 417 "Iril/IR/IR.jay"
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
   28,   28,   28,   28,   28,   28,   28,   28,    3,    3,
    3,   29,   29,   30,   30,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   31,   31,
   32,   32,    4,    4,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   33,   33,   33,   33,   33,   40,   40,
   40,   40,   40,   40,   40,   38,    5,    5,    5,    5,
    5,   44,   44,   44,   34,   34,   45,   45,   46,   46,
   46,   46,   41,   41,   39,   39,   39,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   39,   16,   16,   42,
   42,   37,   37,   47,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   50,   51,   51,   13,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   13,   54,   20,   20,   20,   20,   20,
   20,   20,   20,   20,   55,   27,   27,   56,   53,   53,
   25,   57,   57,   52,   52,   58,   59,   59,   36,   36,
   60,   60,   60,   60,   61,   61,   63,   63,   63,   63,
   65,   66,   66,   67,   67,   68,   68,   68,   68,   68,
   68,   68,   69,   69,   69,   69,   69,   69,   21,   21,
   70,   70,   71,   71,   72,   73,   73,   74,   75,   75,
   76,   76,   43,   77,   78,   62,   62,   79,   79,   79,
   79,   79,   79,   79,   80,   80,   80,   80,   64,   64,
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
    9,   10,   10,   10,   10,    7,   11,    9,   10,   11,
    9,   10,    8,    5,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    3,
    3,    3,    6,    5,    1,    1,    3,    1,    1,    1,
    1,    1,    1,    1,    2,    3,    1,    2,    3,    3,
    3,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    3,    1,    1,    1,    4,    2,    3,
    5,    1,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    3,    4,    2,    4,    1,    5,    5,    1,    3,
    1,    1,    7,    8,    1,    2,    4,    3,    5,    1,
    3,    2,    4,    2,    3,    3,    4,    4,    1,    1,
    1,    1,    2,    3,    2,    2,    4,    5,    6,    6,
    7,    1,    2,    1,    3,    2,    1,    3,    1,    2,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    4,    2,    1,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    6,    9,    8,    6,    6,
    3,    3,    3,    5,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    2,    2,    1,    2,    1,    3,
    2,    1,    2,    1,    3,    1,    1,    3,    1,    2,
    2,    3,    1,    2,    1,    2,    1,    2,    3,    4,
    1,    3,    2,    1,    3,    2,    3,    3,    3,    2,
    4,    5,    1,    1,    6,    9,    6,    6,    1,    3,
    1,    1,    1,    3,    5,    1,    2,    3,    1,    2,
    1,    1,    1,    1,    5,    1,    3,    2,    7,    2,
    2,    7,    1,    1,    8,    9,    9,   10,    5,    6,
    5,    7,    5,    5,    6,    4,    4,    5,    6,    6,
    7,    5,    5,    6,    6,    6,    7,    5,    6,    7,
    7,    8,    6,    4,    4,    5,    6,    5,    2,    5,
    4,    4,    4,    4,    5,    6,    7,    6,    6,    6,
    4,    3,    4,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   95,  106,   98,   99,  100,  101,   97,  129,   39,
   37,   40,   41,   42,   43,   44,   45,  283,  160,  161,
  162,    0,   38,  163,  155,  156,  158,  157,  159,  165,
  170,  171,    0,    0,    0,    0,   96,    0,    0,    0,
    0,    0,  130,  131,    0,    0,  153,    0,    0,    3,
    0,    4,    0,    0,  168,  169,   35,   36,   46,   47,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  167,   89,    0,    0,    0,    0,    0,    0,    0,  135,
    0,    0,    0,  164,    0,    0,    0,    0,    0,    0,
    0,  154,    0,    0,    0,    5,    6,    0,    0,    0,
    0,    0,    0,    0,    0,    8,    0,    7,    0,    0,
    0,    0,    0,   90,    0,    0,    0,    0,  134,    0,
  112,  102,    0,    0,  109,    0,    0,    0,    0,    0,
    0,    0,  151,  152,  146,    0,    0,  147,  174,    0,
    0,    0,  172,    0,    0,    0,  218,  219,  217,  220,
  221,  222,  216,  205,  224,  223,    0,    0,    0,    0,
    0,    0,    0,    0,  204,    0,    0,    0,    0,    0,
    0,    0,    0,   48,    0,    0,    0,   74,   73,   13,
    0,    0,   67,   72,  166,    0,    0,    0,    0,  105,
  103,    0,    0,    0,    0,    0,  138,    0,    0,    0,
   87,   79,   77,   78,   80,   81,   82,   83,    0,   75,
    0,  145,    0,    0,    0,    0,    0,    0,    0,  122,
  173,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  229,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   15,    0,    0,    0,   68,   14,    0,
  201,  203,  202,  226,  107,   91,  108,  110,  139,    0,
    0,  140,    0,    0,   12,   76,  148,    0,  118,   65,
    0,    0,    0,    0,    0,    0,  293,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  239,    0,    0,  245,    0,  286,
  294,    0,    0,  136,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  213,    0,    0,    0,  211,  212,
    0,    0,    0,    0,    0,   61,   64,    0,   59,    0,
   50,   62,    0,   56,   58,   63,   60,   51,   52,   49,
   17,   16,   71,   70,   69,  141,   84,  272,  271,    0,
  269,    0,    0,  291,    0,    0,  288,    0,    0,    0,
    0,  290,  281,  282,    0,    0,  279,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  284,
  329,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  175,  176,  177,  178,  179,
  180,  181,  182,  183,  184,    0,  185,  186,  197,  198,
  199,  200,  188,  190,  191,  192,  193,  189,  187,  195,
  196,  194,    0,    0,    0,    0,    0,    0,    0,    0,
  113,  240,    0,  246,    0,    0,   66,    0,  123,   33,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  230,
    0,    0,    0,    0,    0,    0,    0,  231,    0,   88,
    0,  119,    0,  287,  225,    0,    0,  251,    0,    0,
    0,    0,    0,    0,  280,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  273,    0,    0,    0,    0,
    0,    0,    0,    0,  342,    0,    0,  114,   20,    0,
   28,    0,    0,    0,    0,    0,    0,  214,    0,    0,
    0,    0,    0,   54,    0,   57,  270,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  324,
    0,    0,  236,  237,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  343,    0,    0,    0,    0,  210,  206,  209,
    0,   23,    0,    0,   53,    0,    0,    0,  253,    0,
    0,  254,    0,    0,    0,    0,  299,    0,  326,  364,
    0,  335,  349,    0,  330,  367,    0,  353,  328,  369,
  361,  357,    0,    0,  346,    0,  304,  303,  348,  370,
    0,    0,    0,    0,  301,    0,    0,  215,  228,    0,
    0,    0,    0,    0,    0,    0,    0,  274,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  232,    0,  234,    0,    0,    0,    0,  276,
    0,    0,    0,  256,  252,    0,    0,    0,    0,    0,
  300,  365,  350,  354,  358,  347,  305,  339,  359,  238,
    0,    0,    0,    0,    0,    0,    0,    0,  338,  327,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  233,  208,    0,  289,    0,  292,  277,    0,
  264,  258,    0,    0,    0,    0,  263,  259,  257,  255,
    0,    0,    0,    0,  302,    0,  344,    0,  362,    0,
  275,  355,    0,    0,    0,    0,    0,  207,  235,  278,
  261,    0,    0,    0,    0,    0,  295,    0,    0,    0,
  345,  363,  285,    0,  262,    0,    0,    0,    0,  296,
  297,    0,    0,    0,    0,    0,  298,    0,    0,    0,
    0,    0,  268,  265,  267,    0,    0,  266,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   57,   12,   13,   14,  229,  201,  193,   58,
   82,  202,  272,   83,  237,  215,   85,  194,  381,  185,
  400,  383,  384,  385,  386,  203,  783,  230,   94,   95,
  144,  145,   15,  115,  161,  344,  216,  240,   67,   62,
   68,   63,   64,  217,  157,  158,  163,  476,  493,  273,
  538,  784,  252,  759,  407,  671,  785,  664,  665,  345,
  346,  347,  348,  349,  539,  632,  721,  722,  848,  401,
  595,  596,  789,  790,  416,  417,  451,  699,  350,  351,
  };
  protected static readonly short [] yySindex = {          666,
  -22, -156,   32,   39,   62, 2934, 3236, -274,    0,  666,
    0,    0,    0,    0, -201, -171,   66,   75, 2428, -140,
  -17,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  124,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -61, 4255,  -99,  -53,    0, -239,  174,  194,
 4392, 3000,    0,    0, 3320,  -21,    0, 3320,  182,    0,
  208,    0,   -7,   64,    0,    0,    0,    0,    0,    0,
  140, 4392,  210, -107,  -69,  -39,  289,  -16,  217,   87,
    0,    0,  174,   10,  194,   92, 4392,  113,   86,    0,
   40, 3380,  194,    0, 4392,  194, 3320,  -20, 3320,  208,
  -11,    0,  276,  112, -130,    0,    0, 4392, -107, -107,
 2808, 4392, -107, 4392, -107,    0,  284,    0, -229,  371,
  310, 4191,  403,    0, 4392, 4392,   26, 4392,    0,  214,
    0,    0,  174,  122,    0,  194,  194,  208,    4, -130,
  208,  512,    0,    0,    0,  131,  127,    0,    0,  142,
 -105, -202,    0, 2662, 4392, 4392,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  -30,  427,  439,  448,
 4397, 4427, 4397,  454,    0, 2808, 4392, 2808, 4392,  450,
  456,  458,  161,    0, -229, 4221,    0,    0,    0,    0,
  -12, 2741,    0,    0,    0,  174,   44,  455,    9,    0,
    0, 3817, -130,  208,  142,  142,    0, -130,  457,  479,
    0,    0,    0,    0,    0,    0,    0,    0, -111,    0,
 2068,    0, 3500, -166,  246, 6650, -100, 4397,  264,    0,
    0,  143,  481,  174, 2774,  486, 4392, 4397, 4397, 4397,
    0,   30, 4263,   20,   24,  148,  484, 2808,  485, 2808,
 4111, 4157, 1181,    0, -229,  238,    6,    0,    0, 4226,
    0,    0,    0,    0,    0,    0,    0,    0,    0, -130,
  142,    0,  270, 1639,    0,    0,    0,  274,    0,    0,
  471, 4397,  -74, 4397, 3067, 4397,    0, 3304, 4392, 3304,
 4392, 3304, 4392, 4392, 1425, 4392, 4392, 4392, 3304, 3152,
 3297, 4392, 4392, 4392, 4397, 4397, 4397, 4397, 4397, 4392,
 4023, 4057,  205,  885, 4397, 4397, 4397, 4397, 4397, 4397,
 4397, 4397, 4397, 4397, 4397, 4397,  162, 1628, 4392, 4392,
 3067,  119, 4392, 3356,    0, 6650,  267,    0,  267,    0,
    0,  268, 6650,    0,  233,  290, -245,  170,  506, 4392,
   43,  166,  167,  183,    0, 4397, 2741,   31,    0,    0,
  330,  209,  545,  213,  549,    0,    0,  554,    0, 1415,
    0,    0,  472,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  287,
    0,  233, 7139,    0,  326, 2898,    0,  559, 1070, 3320,
 3320,    0,    0,    0, 2741, 3304,    0, 2741, 2741, 3304,
 2741, 2741, 3304, 2741, 2741, 4392, 2741, 2741, 2741, 2741,
 2741, 3304, 4392, 2741, 4392, 2741, 2741, 2741, 2741,  560,
  562,  564,  565,  566,  188, 4392,  294, 4397,  568,    0,
    0, 4392,  309,  226,  227,  228,  231,  232,  239,  240,
  241,  247,  248,  249,  252,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4392,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4392,   37, 2741, 1070, 4392, 3320, 3067,  -37,
    0,    0,  267,    0,  367,  367,    0, 3460,    0,    0,
  373,  385,  386,  266,  342, 4397, 4392, 4392, 4392,    0,
  585,  267,  392,  271,  394,  273,  588,    0, 4157,    0,
 1639,    0,  267,    0,    0,  609,  388,    0,  615, 1070,
 1070, 3320,  612, 2741,    0,  613,  614, 2741,  618,  620,
 2741,  621,  632, 2741,  651,  653,  657,  660,  662, 2741,
 2741,  663, 2741,  665,  669,  670,  671, 4397, 4397, 4397,
 1181, 4397,  -14,  349, 4392,  672, 4392,  381, 4397, 4392,
 4392, 4392, 4392, 4392, 4392, 4392, 4392, 4392, 4392, 4392,
 4392, 2741, 2741, 2898,  673,    0,  677,  615, 1070, 1070,
 4392,   84, 4392, 3320,    0, 4397,  367,    0,    0,  267,
    0,  402, 4397,  678,  414,  416,  441,    0,  367,  267,
  467,  267,  468,    0,  314,    0,    0,  367,  388,  619,
 1520,  341,  615,  615, 1070, 2898,  685, 2898, 2898,  689,
 2898, 2898,  691, 2898, 2898,  694, 2898, 2898, 2898, 2898,
 2898,  695,  696, 2898,  697, 2898, 2898, 2898, 2898,    0,
  698,  700,    0,    0,  701,  702,  492,  706, 4392, 2741,
  709, 4392,  711, 4397,  715,  174,  174,  174,  174,  174,
  174,  174,  174,  174,  174,  174,  174,  720,  721,  723,
  679, 4397,  142,  615,  615, 1070,  347,  615,  615, 1070,
 1070, 3320,    0,  367,  267,  729,  -19,    0,    0,    0,
  367,    0,  367,  267,    0,  730, 4392, 4269,    0, 2594,
  320,    0,  388,  399,  405,  615,    0, 2898,    0,    0,
 2898,    0,    0, 2898,    0,    0, 2898,    0,    0,    0,
    0,    0, 2898, 2898,    0, 2898,    0,    0,    0,    0,
 4397, 4397, 1181, 1181,    0,  396,  735,    0,    0,  397,
  736,  417,  745,  -19, 2898, 2898, 2898,    0,  749,  142,
  142,  142,  615,  537,  142,  142,  615,  615, 1070,  367,
  -19, 4397,    0,  331,    0,  367,  388,  757, 4344,    0,
  762,  829, 2831,    0,    0, 4369,  476,  388,  388,  418,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  701,  552,  433,  558,  442,  567,  -19,  778,    0,    0,
  713, 4397,  142,  142,  142,  785,  142,  142,  142,  142,
  615,  338,    0,    0,  -19,    0,  388,    0,    0, 1310,
    0,    0,  459,  790,  796,  804,    0,    0,    0,    0,
  388,  507,  521,  388,    0,  595,    0,  597,    0,  778,
    0,    0,  142,  606,  142,  142,  142,    0,    0,    0,
    0,  359,  815, 4397, 4397, 4397,    0,  388,  388,  542,
    0,    0,    0,  142,    0, 4392,  483,  487,  489,    0,
    0,  388,  410, 4392, 4392, 4392,    0, 4397,  444,  453,
  462,  833,    0,    0,    0,  -19,  368,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  878,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3083,  600,  610,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   17,    0,    0,    0,    0,    0, 3166,    0,
  927,    0,  611,    0,    0,  622,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  841,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  272,    0,    0,  623,  626,    0,    0,  200,
    0,    0,    0,    0,    0,  279,    0,    0,    0,  -98,
    0,  -96,    0,   98,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  841,    0,  841,    0,    0,
    0,    0,    0,    0,    0,    0, 1034,    0,    0,    0,
    0,  841,    0,    0,    0,   29,  841,    0,  841,    0,
    0,    0,    0,    0,  283,  348,    0,    0,  432,  822,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  372,    0,    0,  -92,    0,    0,    0,    0,    0,    0,
    0,    0,  430,  191,  841,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  841,    0,  841,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  509,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3563,    0, 6753,    0,
    0,    0,    0,    0,  -89,    0,    0,    0,    0,    0,
  841,    0,    0,    0,    0,    0,   17,    0,    0,    0,
    0,    0,    0,    0,  624,    0,    0,   -4,    0,  841,
    0,    0,  377,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -87,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  841,    0,    0,  841,  841,    0,
  841,  841,    0,  841,  841,    0,  841,  841,  841,  841,
  841,    0,    0,  841,    0,  841,  841,  841,  841,    0,
    0,    0,    0,    0,  841,    0,  841,    0,    0,    0,
    0,    0,  841,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  841,  841,    0,    0,    0,    0,  841,
    0,    0, 3666,    0, 3769, 6856,    0,    0,    0,    0,
    0,    0,    0,    0,  841,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 6959,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  841,    0,    0,    0,  841,    0,    0,
  841,    0,    0,  841,    0,    0,    0,    0,    0,  841,
  841,    0,  841,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  841,    0,    0,    0,  841,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  841,  841,    0, 4384,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3872,    0,    0,  732,
    0,    0,    0,    0,  841,  841,  841,    0,  794,    0,
    0,    0,    0,    0,    0,    0,    0, 7062,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4487,    0,    0,    0,    0,  841,
    0,    0,    0,    0,    0, 1137, 1263, 1389, 1492, 1599,
 1728, 1831, 1938, 2067, 2170, 2277, 2406,    0,    0,    0,
    0,    0, 4590,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  856, 1270,    0,    0,    0,    0,    0,
 1396,    0, 1736, 1869,    0,    0,    0,    0,    0,  841,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4693,
 4796, 4899,    0,    0, 5002,    0,    0,    0,    0, 2075,
    0,    0,    0,    0,    0, 2208,    0,    0,    0,    0,
  393,  841,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5105,    0,    0,    0,    0,    0,    0, 5208,    0,    0,
    0,    0, 5311, 5414, 5517,    0, 5620, 5723, 5826, 5929,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 6032,
    0,    0, 6135,    0, 6238, 6341, 6444,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 6547,    0,    0,    0,    0,    0,    0,
    0,    0,  841,    0,    0,    0,    0,    0,  841,  841,
  841,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  875,  800,    0,    0,    0,    0,  704,  707,  872,
  207,   -6,  123,  118, -336,    7,    0,  636,  641, -254,
 -515,    0,  378,    0, -679,    0,  352,  680,  809,  133,
    0,  699,    0,  -68,    0,  555,  -82, -212,   -2,    0,
  -59,  852,  -56, -155,    0,  682,  -97,    0,    0,    0,
  389, -736,   28,    0, -477, -524,   77,  163,  164, -329,
    0,  571,  573,  520,  364, 2245,    0,  130,    0,  398,
    0,  237,    0,  141,  -76, -195,    0,    0,    0,  529,
  };
  protected static readonly short [] yyTable = {            59,
   59,  100,  107,   61,  101,  109,  382,  382,  389,  247,
  505,  625,  506,  285,  502,   89,  131,  236,  102,  102,
  127,  289,  353,   97,  115,   84,  120,  818,  102,  399,
  116,  270,  162,  121,  511,  117,   60,  788,   16,   60,
   55,  150,  139,  102,  832,   55,  190,   93,   69,  270,
  101,  191,  673,  135,   59,   59,   94,  279,   59,  630,
   92,   59,  282,  366,  241,  112,   38,  366,   94,  135,
   71,   56,   93,  366,  366,  121,   56,  234,  101,  213,
  860,  369,  218,   54,  101,  101,  516,  120,   72,  123,
   93,  125,   19,   51,   52,  143,  231,   34,   59,   20,
   59,  277,   59,   54,  112,  275,  112,  156,   54,  788,
  238,  164,  269,   17,   18,  186,  370,  188,  241,   60,
  159,  160,   21,  102,  396,  528,   73,  594,  206,  207,
  392,  209,  281,  512,  134,   74,  241,   94,   60,   66,
   86,   92,  509,  182,  757,  280,  238,  761,  219,  192,
  208,  716,  155,   93,  365,  521,  159,   96,  244,  245,
   75,   76,  211,   90,  220,  212,  607,  232,  235,  907,
  233,   55,  101,  352,  183,  115,  239,  120,  502,  105,
  258,  116,  260,  241,  121,  619,  117,   77,   78,  532,
   19,  405,  159,  103,  106,   91,  628,  108,  119,  137,
  111,  264,   56,   98,  265,  143,  181,  221,  222,  254,
  255,  406,  288,  223,  224,  101,  225,  226,  227,  228,
  545,   75,   76,  420,  545,  423,  156,  545,  112,  101,
   94,  573,  432,  102,   54,  410,  545,  146,  411,  147,
  361,  149,  113,  184,  197,  797,  367,  114,  126,  197,
  110,  148,  116,   23,  380,  380,   87,  130,   23,   88,
  151,  268,   24,   25,   26,   27,   28,   24,   25,   26,
   27,   28,  399,  704,  382,  214,  399,  605,  391,  268,
  368,  265,  142,  711,  498,  713,  243,  118,   59,  122,
  124,  415,  418,  419,  421,  422,  424,  425,  427,  428,
  429,  430,  431,  434,  436,  437,  438,  439,  257,  836,
  259,   94,  111,  445,  447,  111,  663,  453,   94,  149,
  852,  853,  149,  117,  872,  165,  166,  530,  129,  187,
  531,  189,  494,  495,   59,  101,  500,  575,  497,  132,
  167,  168,  169,  133,  170,  171,  172,  144,  173,  246,
  101,  542,  579,  515,  715,  271,  174,  531,  136,  870,
  795,  782,  175,  796,  667,   34,   34,  359,  780,   34,
  176,  834,   34,  877,  835,   22,  880,  786,  868,  138,
  373,  835,  375,  101,   23,  613,   34,   34,  606,  153,
  101,   38,  669,   24,   25,   26,   27,   28,  152,  885,
  890,  891,  531,   59,   59,  154,   97,  112,  908,  544,
  195,  835,  150,  548,  897,  150,  551,   55,  140,  554,
   55,   34,  101,  104,  674,  560,  561,  409,  563,   18,
   75,   76,  196,  260,   77,   78,  260,   39,   40,  574,
   41,   42,  604,  205,   44,  578,   45,   46,   47,   48,
   49,  101,   50,  898,  708,  101,  709,  101,   19,   19,
   80,  177,   19,   19,  159,   19,  248,  137,  137,  592,
  210,  137,  137,  496,  137,  178,  179,  180,  249,   19,
   19,  710,  101,  204,  903,  101,  593,  250,  137,  137,
   59,   59,   59,  904,  101,  601,  603,  256,  663,  663,
   75,   76,  905,  101,   77,   78,  697,  261,  143,   53,
  615,  616,  617,  262,   19,  263,  276,  283,  284,  290,
  355,  356,  380,  137,  357,  360,  371,  372,  374,  397,
  402,  403,  251,  251,  251,   59,  450,  847,  499,  112,
  235,  507,  540,  541,  702,  238,  510,  204,  513,  514,
  142,  142,  517,  518,  142,  142,   85,  142,  466,  467,
  468,  469,  470,  471,  472,  473,  474,  475,  670,  519,
  670,  142,  142,  676,  677,  678,  679,  680,  681,  682,
  683,  684,  685,  686,  687,  399,  522,  523,  524,  354,
  274,  525,  526,  527,   59,  529,   59,   59,  535,  362,
  363,  364,  537,  568,  251,  569,  142,  570,  571,  572,
  770,  577,  580,  581,  582,  144,  144,  583,  584,  144,
  144,  395,  144,   26,  720,  585,  586,  587,  624,  599,
  600,  602,  609,  588,  589,  590,  144,  144,  591,   94,
  352,  610,  611,  404,  612,  408,  618,  412,  620,  621,
  622,  623,  629,  405,  631,  636,  638,  639,  705,   94,
  793,  641,  670,  642,  644,  670,  440,  441,  442,  443,
  444,  144,  241,  449,  635,  645,  454,  455,  456,  457,
  458,  459,  460,  461,  462,  463,  464,  465,  823,  824,
   94,   85,  827,  828,  647,   59,  648,   18,   18,  112,
  649,   18,   18,  650,   18,  651,  654,   85,  656,  717,
  380,  792,  657,  658,  659,  672,  691,  520,   18,   18,
  692,  707,   94,  712,  714,  241,  241,  723,  728,  241,
  241,   31,  731,  696,  734,  700,  701,  737,  743,  744,
  746,  751,  863,  752,  753,  754,  865,  866,  755,  756,
   85,   85,  760,   18,  762,  274,   85,   85,  764,   85,
   85,   85,   85,  765,  766,  241,  767,  241,  241,  594,
  774,  219,  781,  787,  812,  814,  143,  143,  813,  815,
  143,  143,  380,  143,  884,  798,  241,  220,  817,  720,
  112,  799,  822,   21,  536,  816,  826,  143,  143,  576,
  837,  840,  851,  543,  854,  861,  546,  547,  855,  549,
  550,  856,  552,  553,  857,  555,  556,  557,  558,  559,
  858,  835,  562,  859,  564,  565,  566,  567,  864,  874,
  221,  222,  143,  878,  779,  875,  223,  224,  873,  225,
  226,  227,  228,  876,  167,  168,  169,  879,  170,  171,
  172,  881,  173,  882,  886,   32,   94,   94,   94,  598,
   94,   94,   94,  398,   94,  883,  175,  614,  892,  894,
  101,   94,   94,  895,  176,  896,  906,    1,   94,  893,
   94,  124,  125,  597,   70,  128,   94,  899,  900,  901,
   81,   26,   26,  126,  127,   26,   26,  128,   26,  267,
  390,  266,  388,  633,  634,  137,  626,  508,  286,   99,
  278,  869,   26,   26,  287,  810,  503,  811,  504,  660,
  661,  662,  533,  666,  668,  850,  104,  768,  627,  839,
  675,  534,  637,    1,    2,    0,  640,    3,    4,  643,
    5,    0,  646,    0,   55,    0,   86,   26,  652,  653,
    0,  655,    0,    0,    6,    7,    0,  703,    0,    0,
    0,    0,  694,  695,  706,  698,  104,  104,  104,    0,
  104,    0,    0,    0,    0,   56,    0,   94,    0,    0,
  688,  689,  690,    0,    0,    0,  104,    0,  104,    8,
    0,   94,   94,   94,    0,    0,    0,    0,  726,   31,
   31,    0,    0,   31,   31,    0,   31,   54,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  104,    0,  104,
   31,   31,   94,    0,  727,  763,  729,  730,    0,  732,
  733,    0,  735,  736,    0,  738,  739,  740,  741,  742,
    0,    0,  745,  769,  747,  748,  749,  750,    0,  104,
    0,  104,    0,    0,    0,   31,    0,    0,  758,  773,
    0,   21,   21,  777,  778,   21,   21,    0,   21,    0,
    0,    0,    0,   95,  227,    0,    0,  227,    0,    0,
    0,   86,   21,   21,    0,  167,  168,  169,    0,  170,
  171,  172,    0,  173,    0,  227,    0,   86,    0,    0,
  841,  842,  808,  809,    0,    0,    0,  175,  794,  102,
    0,    0,    0,    0,    0,  176,  801,   21,    0,  802,
    0,    0,  803,   32,   32,  804,  227,   32,   32,  182,
   32,  805,  806,  833,  807,    0,    0,    0,    0,    0,
   86,   86,  831,    0,   32,   32,   86,   86,   22,   86,
   86,   86,   86,  819,  820,  821,  227,   23,  227,    0,
  183,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,  862,    0,    0,   94,    0,    0,   32,
    0,  849,    0,  104,  104,  104,    0,  104,  104,  104,
    0,  104,  181,    0,  104,  104,    0,    0,  104,  104,
  104,  104,    0,    0,    0,  104,  843,    0,    0,    0,
    0,    0,    0,  104,    0,  104,  104,    0,    0,  104,
  844,  845,  846,    0,    0,  887,  888,  889,    0,    0,
    0,    0,    0,  104,  104,    0,  104,  104,    0,    0,
  104,  104,  104,  104,  104,  104,  104,    0,  104,  902,
  104,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  104,  104,  104,  452,  104,  104,    0,    0,   29,
  104,    0,  104,    0,    0,  104,  104,  104,  104,  104,
  104,  104,  104,  104,  104,    0,  104,  104,    0,  104,
  104,  104,  104,  104,  104,  104,  104,  104,  104,  104,
  104,  104,   94,    0,  104,    0,  227,  227,  104,  104,
  104,  104,  104,    0,  104,  104,  104,  104,  104,  104,
  104,    0,  104,    0,    0,    0,  167,  168,  169,    0,
  170,  171,  172,  104,  173,    0,    0,    0,    0,    0,
    0,  271,  174,    0,  104,  104,  104,  104,  175,  104,
  871,  104,  104,    0,    0,    0,  176,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  227,  227,
  227,    0,  227,  227,    0,    0,    0,  227,    0,  227,
    0,    0,  227,  227,  227,  227,  227,  227,  227,  227,
  227,  227,    0,  227,  227,   24,  227,  227,  227,  227,
  227,  227,  227,  227,  227,  227,  227,  227,  227,  366,
  366,  227,    0,    0,    0,  227,  227,  227,  227,  227,
  227,  227,  227,  227,  227,  227,  227,  227,   94,  227,
    0,    0,    0,    0,    0,    0,    0,  167,  168,  169,
  227,  170,  171,  172,    0,  173,    0,  177,    0,    0,
    0,  227,  227,  227,  227,    0,  101,    0,  227,  175,
    0,  178,  179,  180,    0,    0,    0,  176,    0,    0,
    0,  366,  366,  366,    0,  366,  366,    0,    0,    0,
  366,    0,  366,    0,   55,  366,  366,  366,  366,  366,
  366,  366,  366,  366,  366,    0,  366,  366,    0,  366,
  366,  366,  366,  366,  366,  366,  366,  366,  366,  366,
  366,  366,    0,    0,  366,   56,    0,    0,  366,  366,
  366,  366,  366,    0,  366,  366,  366,  366,  366,  366,
  366,   94,  366,    0,    0,  371,  371,   29,   29,    0,
    0,   29,   29,  366,   29,    0,    0,   54,    0,    0,
    0,    0,    0,    0,  366,  366,  366,  366,   29,   29,
  719,  366,    0,    0,    0,    0,  167,  168,  169,    0,
  170,  171,  172,    0,  173,    0,    0,    0,    0,   55,
    0,    0,    0,    0,    0,  398,    0,    0,  175,    0,
    0,    0,    0,   29,    0,    0,  176,  371,  371,  371,
    0,  371,  371,    0,    0,    0,  371,    0,  371,    0,
   56,  371,  371,  371,  371,  371,  371,  371,  371,  371,
  371,    0,  371,  371,    0,  371,  371,  371,  371,  371,
  371,  371,  371,  371,  371,  371,  371,  371,   94,    0,
  371,    0,   54,    0,  371,  371,  371,  371,  371,    0,
  371,  371,  371,  371,  371,  371,  371,    0,  371,    0,
    0,  356,  356,   24,   24,    0,    0,   24,   24,  371,
   24,  167,  168,  169,    0,  170,  171,  172,    0,  173,
  371,  371,  371,  371,   24,   24,    0,  371,   22,    0,
    0,    0,    0,  175,    0,    0,    0,   23,    0,    0,
    0,  176,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,    0,    0,   24,
    0,    0,    0,  356,  356,  356,    0,  356,  356,    0,
    0,    0,  356,    0,  356,   22,    0,  356,  356,  356,
  356,  356,  356,  356,  356,  356,  356,    0,  356,  356,
    0,  356,  356,  356,  356,  356,  356,  356,  356,  356,
  356,  356,  356,  356,  334,  334,  356,   94,    0,    0,
  356,  356,  356,  356,  356,    0,  356,  356,  356,  356,
  356,  356,  356,   22,  356,    0,  426,    0,    0,    0,
    0,    0,   23,    0,    0,  356,    0,  718,    0,    0,
    0,   24,   25,   26,   27,   28,  356,  356,  356,  356,
    0,    0,    0,  356,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  334,  334,  334,    0,
  334,  334,    0,    0,    0,  334,    0,  334,    0,    0,
  334,  334,  334,  334,  334,  334,  334,  334,  334,  334,
    0,  334,  334,    0,  334,  334,  334,  334,  334,  334,
  334,  334,  334,  334,  334,  334,  334,    0,   25,  334,
   94,  331,  331,  334,  334,  334,  334,  334,    0,  334,
  334,  334,  334,  334,  334,  334,    0,  334,  477,  478,
    0,    0,    0,    0,    0,  167,  168,  169,  334,  170,
  171,  172,    0,  173,    0,    0,    0,    0,    0,  334,
  334,  334,  334,    0,  398,    0,  334,  175,    0,    0,
    0,    0,    0,    0,    0,  176,    0,    0,    0,    0,
    0,    0,    0,  331,  331,  331,    0,  331,  331,    0,
    0,    0,  331,    0,  331,    0,    0,  331,  331,  331,
  331,  331,  331,  331,  331,  331,  331,    0,  331,  331,
    0,  331,  331,  331,  331,  331,  331,  331,  331,  331,
  331,  331,  331,  331,    0,    0,  331,   94,    0,    0,
  331,  331,  331,  331,  331,    0,  331,  331,  331,  331,
  331,  331,  331,    0,  331,    0,    0,    0,    0,    0,
  332,  332,    0,   22,   22,  331,    0,   22,   22,    0,
   22,    0,    0,    0,    0,    0,  331,  331,  331,  331,
    0,    0,    0,  331,   22,   22,  479,  480,  481,  482,
    0,    0,    0,    0,    0,  483,  484,  485,  486,  487,
  488,  489,  490,  491,  492,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,  332,  332,  332,    0,  332,  332,    0,    0,
    0,  332,    0,  332,   30,    0,  332,  332,  332,  332,
  332,  332,  332,  332,  332,  332,    0,  332,  332,    0,
  332,  332,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,  332,  333,  333,  332,   94,    0,    0,  332,
  332,  332,  332,  332,    0,  332,  332,  332,  332,  332,
  332,  332,    0,  332,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  332,    0,   25,   25,    0,    0,
   25,   25,    0,   25,    0,  332,  332,  332,  332,    0,
    0,    0,  332,    0,    0,    0,    0,   25,   25,    0,
    0,    0,    0,    0,    0,  333,  333,  333,    0,  333,
  333,    0,    0,    0,  333,    0,  333,    0,    0,  333,
  333,  333,  333,  333,  333,  333,  333,  333,  333,    0,
  333,  333,   25,  333,  333,  333,  333,  333,  333,  333,
  333,  333,  333,  333,  333,  333,    0,   27,  333,   94,
  368,  368,  333,  333,  333,  333,  333,    0,  333,  333,
  333,  333,  333,  333,  333,    0,  333,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  333,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  333,  333,
  333,  333,    0,    0,    0,  333,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  368,  368,  368,    0,  368,  368,    0,    0,
    0,  368,    0,  368,    0,    0,  368,  368,  368,  368,
  368,  368,  368,  368,  368,  368,    0,  368,  368,    0,
  368,  368,  368,  368,  368,  368,  368,  368,  368,  368,
  368,  368,  368,    0,    0,  368,   94,    0,    0,  368,
  368,  368,  368,  368,    0,  368,  368,  368,  368,  368,
  368,  368,    0,  368,    0,    0,    0,    0,    0,  360,
  360,    0,   30,   30,  368,    0,   30,   30,    0,   30,
    0,    0,    0,    0,    0,  368,  368,  368,  368,    0,
  104,    0,  368,   30,   30,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   39,   40,    0,   41,   42,    0,
    0,   44,    0,   45,   46,   47,   48,   49,    0,   50,
    0,    0,    0,    0,    0,    0,    0,    0,   30,    0,
    0,  360,  360,  360,    0,  360,  360,    0,    0,    0,
  360,    0,  360,    0,    0,  360,  360,  360,  360,  360,
  360,  360,  360,  360,  360,    0,  360,  360,    0,  360,
  360,  360,  360,  360,  360,  360,  360,  360,  360,  360,
  360,  360,  352,  352,  360,   94,   53,    0,  360,  360,
  360,  360,  360,    0,  360,  360,  360,  360,  360,  360,
  360,    0,  360,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  360,    0,   27,   27,    0,    0,   27,
   27,    0,   27,    0,  360,  360,  360,  360,    0,    0,
    0,  360,    0,    0,    0,    0,   27,   27,    0,    0,
    0,    0,    0,    0,  352,  352,  352,    0,  352,  352,
    0,    0,    0,  352,    0,  352,    0,    0,  352,  352,
  352,  352,  352,  352,  352,  352,  352,  352,    0,  352,
  352,   27,  352,  352,  352,  352,  352,  352,  352,  352,
  352,  352,  352,  352,  352,    0,    0,  352,    0,  341,
  341,  352,  352,  352,  352,  352,    0,  352,  352,  352,
  352,  352,  352,  352,    0,  352,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  352,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  352,  352,  352,
  352,    0,    0,    0,  352,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  341,  341,  341,    0,  341,  341,    0,    0,    0,
  341,    0,  341,    0,    0,  341,  341,  341,  341,  341,
  341,  341,  341,  341,  341,  101,  341,  341,    0,  341,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
  341,  341,    0,  182,  341,    0,    0,    0,  341,  341,
  341,  341,  341,    0,  341,  341,  341,  341,  341,  341,
  341,    0,  341,    0,    0,    0,    0,    0,  306,  306,
    0,    0,    0,  341,  183,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,  341,  341,  341,    0,    0,
    0,  341,    0,  101,    0,  242,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  181,    0,   75,   76,
    0,  182,   77,   78,   79,   30,   31,   32,   33,   34,
   35,   36,   37,    0,    0,    0,    0,    0,    0,   43,
  306,  306,  306,    0,  306,  306,    0,    0,   80,  306,
    0,  306,  183,    0,  306,  306,  306,  306,  306,  306,
  306,  306,  306,  306,    0,  306,  306,    0,  306,  306,
  306,  306,  306,  306,  306,  306,  306,  306,  306,  306,
  306,    0,  101,  306,  181,    0,    0,  306,  306,  306,
  306,  306,    0,  306,  306,  306,  306,  306,  306,  306,
  182,  306,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  306,    0,    0,  101,    0,  358,    0,    0,
    0,    0,    0,  306,  306,  306,  306,    0,    0,    0,
  306,  183,    0,  182,    0,    0,    0,    0,    0,    0,
    0,    0,  693,    0,    0,    0,    0,    0,    0,  101,
  167,  168,  169,    0,  170,  171,  172,    0,  173,    0,
    0,    0,    0,  181,  183,  271,  174,  182,    0,    0,
    0,    0,  175,    0,    0,    0,    0,  724,  725,    0,
  176,    0,    0,    0,    0,    0,  104,    0,    0,    0,
  182,    0,    0,    0,    0,    0,  181,    0,  183,    0,
   39,   40,    0,   41,   42,    0,    0,   44,    0,   45,
   46,   47,   48,   49,    0,   50,    0,    0,  167,  168,
  169,  183,  170,  171,  172,    0,  173,    0,    0,    0,
  181,    0,    0,    0,  174,    0,    0,    0,  771,  772,
  175,    0,  775,  776,    0,    0,    0,    0,  176,    0,
    0,    0,    0,  181,    0,    0,    0,  182,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  800,  177,   53,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  178,  179,  180,  183,    0,
    0,    0,    0,   55,    0,    0,    0,  167,  168,  169,
    0,  170,  171,  172,    0,  173,    0,    0,    0,    0,
    0,    0,  271,  174,    0,    0,    0,  825,    0,  175,
  181,  829,  830,    0,   56,    0,    0,  176,    0,    0,
  167,  168,  169,    0,  170,  171,  172,    0,  173,  177,
    0,    0,    0,    0,    0,    0,  174,    0,    0,    0,
    0,    0,  175,  178,  179,  180,   54,    0,    0,   55,
  176,    0,    0,    0,  167,  168,  169,    0,  170,  171,
  172,    0,  173,    0,    0,  867,    0,    0,    0,    0,
  174,    0,    0,    0,    0,    0,  175,  167,  168,  169,
   56,  170,  171,  172,  176,  173,    0,    0,    0,    0,
    0,    0,  271,  174,    0,    0,    0,    0,    0,  175,
    0,    0,    0,    0,    0,    0,    0,  176,  177,    0,
    0,    0,   54,  104,    0,    0,   55,    0,    0,    0,
    0,    0,  178,  179,  180,    0,    0,   39,   40,    0,
   41,   42,  132,    0,   44,    0,   45,   46,   47,   48,
   49,  177,   50,    0,  167,  168,  169,   56,  170,  171,
  172,    0,  173,    0,    0,  178,  179,  180,    0,  271,
  174,    0,    0,  132,    0,    0,  175,    0,    0,    0,
    0,    0,    0,    0,  176,  177,    0,    0,    0,   54,
    0,    0,    0,    0,    0,    0,    0,   22,    0,  178,
  179,  180,    0,    0,    0,  132,   23,    0,  177,   53,
    0,   55,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,  178,  179,  180,  133,   29,    0,    0,    0,
    0,   30,   31,   32,   33,   34,   35,   36,   37,   38,
   39,   40,   56,   41,   42,   43,    0,   44,    0,   45,
   46,   47,   48,   49,    0,   50,  133,    0,    0,    0,
    0,    0,    0,   22,    0,    0,   51,   52,    0,    0,
    0,    0,   23,    0,   54,  177,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,  133,  178,
  179,  180,  104,    0,    0,   55,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   39,   40,    0,   41,
   42,    0,   53,   44,    0,   45,   46,   47,   48,   49,
   80,   50,    0,    0,    0,    0,   56,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,  132,    0,   24,   25,
   26,   27,   28,    0,    0,  132,   55,    0,   54,  104,
    0,    0,    0,   55,  132,  132,  132,  132,  132,    0,
    0,    0,   38,   39,   40,  132,   41,   42,   53,   55,
   44,    0,   45,   46,   47,   48,   49,   56,   50,  132,
  132,    0,  132,  132,   56,    0,  132,    0,  132,  132,
  132,  132,  132,  132,  132,    0,    0,    0,    0,    0,
   56,    0,    0,    0,    0,   22,    0,    0,    0,   54,
  142,    0,    0,    0,   23,    0,   54,    0,    0,  133,
    0,    0,    0,   24,   25,   26,   27,   28,  133,   55,
    0,    0,   54,    0,    0,   53,    0,  133,  133,  133,
  133,  133,    0,    0,    0,    0,    0,    0,  133,    0,
    0,  132,    0,    0,    0,    0,    0,    0,    0,    0,
   56,    0,  133,  133,    0,  133,  133,    0,    0,  133,
  501,  133,  133,  133,  133,  133,  133,  133,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,   54,    0,    0,    0,    0,    0,   23,    0,
    0,    0,    0,  433,    0,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,    0,   65,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   39,   40,  133,   41,   42,    0,    0,   44,
    0,   45,   46,   47,   48,   49,    0,   50,    0,   55,
   22,    0,    0,    0,    0,    0,    0,   22,    0,   23,
    0,    0,    0,    0,    0,    0,   23,    0,   24,   25,
   26,   27,   28,   22,  608,   24,   25,   26,   27,   28,
   56,    0,   23,    0,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
    0,    0,  104,    0,   53,    0,    0,    0,    0,    0,
    0,    0,   54,    0,    0,    0,   39,   40,  291,   41,
   42,    0,    0,   44,    0,   45,   46,   47,   48,   49,
    0,   50,    0,   22,    0,    0,    0,    0,    0,    0,
  413,  414,   23,    0,    0,    0,    0,    0,  435,    0,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
    0,    0,    0,  141,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  243,    0,    0,
  292,  293,  294,    0,  295,  296,    0,    0,   53,  297,
    0,  298,    0,    0,  299,  300,  301,  302,  303,  304,
  305,  306,  307,  308,    0,  309,  310,    0,  311,  312,
  313,  314,  315,  316,  317,  318,  319,  320,  321,  322,
  323,    0,  291,  324,    0,    0,    0,  325,  326,  327,
  328,  329,    0,  330,  331,  332,  333,  334,  335,  336,
    0,  337,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  338,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,  339,  340,  341,  342,  153,    0,    0,
  343,   24,   25,   26,   27,   28,    0,    0,    0,    0,
  241,    0,    0,  154,  292,  293,  294,    0,  295,  296,
    0,    0,    0,  297,    0,  298,    0,    0,  299,  300,
  301,  302,  303,  304,  305,  306,  307,  308,    0,  309,
  310,    0,  311,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,  322,  323,  243,    0,  324,    0,    0,
    0,  325,  326,  327,  328,  329,    0,  330,  331,  332,
  333,  334,  335,  336,    0,  337,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  338,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   55,  339,  340,  341,
  342,    0,    0,    0,  343,    0,    0,    0,    0,    0,
    0,    0,    0,  244,    0,    0,    0,  243,  243,  243,
    0,  243,  243,    0,    0,    0,  243,   56,  243,    0,
    0,  243,  243,  243,  243,  243,  243,  243,  243,  243,
  243,    0,  243,  243,    0,  243,  243,  243,  243,  243,
  243,  243,  243,  243,  243,  243,  243,  243,  241,   54,
  243,    0,    0,    0,  243,  243,  243,  243,  243,    0,
  243,  243,  243,  243,  243,  243,  243,    0,  243,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  243,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  243,  243,  243,  243,    0,    0,    0,  243,    0,    0,
    0,    0,    0,    0,    0,    0,  242,    0,    0,    0,
  241,  241,  241,    0,  241,  241,    0,    0,    0,  241,
    0,  241,    0,    0,  241,  241,  241,  241,  241,  241,
  241,  241,  241,  241,    0,  241,  241,    0,  241,  241,
  241,  241,  241,  241,  241,  241,  241,  241,  241,  241,
  241,  244,    0,  241,    0,    0,    0,  241,  241,  241,
  241,  241,    0,  241,  241,  241,  241,  241,  241,  241,
    0,  241,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  241,    0,    0,    0,    0,    0,    0,    0,
   22,    0,   55,  241,  241,  241,  241,    0,    0,   23,
  241,    0,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,  244,  244,  244,    0,  244,  244,    0,
  141,    0,  244,   56,  244,    0,   55,  244,  244,  244,
  244,  244,  244,  244,  244,  244,  244,    0,  244,  244,
    0,  244,  244,  244,  244,  244,  244,  244,  244,  244,
  244,  244,  244,  244,  242,   54,  244,   56,    0,    0,
  244,  244,  244,  244,  244,    0,  244,  244,  244,  244,
  244,  244,  244,    0,  244,    0,    0,    0,    0,    0,
   55,    0,    0,    0,    0,  244,    0,    0,    0,   54,
    0,    0,    0,    0,    0,    0,  244,  244,  244,  244,
    0,    0,    0,  244,    0,    0,    0,    0,    0,    0,
    0,   56,    0,    0,    0,    0,  242,  242,  242,    0,
  242,  242,    0,    0,    0,  242,   55,  242,    0,    0,
  242,  242,  242,  242,  242,  242,  242,  242,  242,  242,
    0,  242,  242,   54,  242,  242,  242,  242,  242,  242,
  242,  242,  242,  242,  242,  242,  242,   56,    0,  242,
   55,    0,    0,  242,  242,  242,  242,  242,    0,  242,
  242,  242,  242,  242,  242,  242,    0,  242,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  242,   54,
   55,   56,    0,    0,    0,   55,   22,    0,    0,  242,
  242,  242,  242,    0,    0,   23,  242,    0,    0,    0,
    0,    0,    0,    0,   24,   25,   26,   27,   28,    0,
    0,   56,    0,   54,   55,  200,   56,    0,    0,    0,
  197,    0,   55,    0,    0,    0,    0,    0,   55,   23,
    0,  446,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,   54,    0,   56,    0,    0,   54,    0,
    0,    0,    0,   56,    0,    0,    0,    0,    0,   56,
    0,    0,    0,    0,    0,  448,    0,  167,  168,  169,
  376,  170,  171,  172,   22,  377,    0,   54,    0,   92,
    0,    0,    0,   23,  378,   54,  379,   92,    0,  175,
    0,   54,   24,   25,   26,   27,   28,  176,    0,    0,
    0,    0,    0,   55,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  167,  168,  169,  376,  170,  171,  172,
   22,  377,    0,    0,    0,    0,    0,    0,   55,   23,
  387,    0,  379,    0,   56,  175,  838,    0,   24,   25,
   26,   27,   28,  176,    0,    0,    0,    0,    0,    0,
    0,   55,    0,    0,  197,  198,   55,    0,    0,   56,
    0,    0,    0,   23,  199,    0,   54,    0,    0,    0,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,   56,    0,  197,  198,   55,   56,    0,  197,
  393,   54,    0,   23,  199,    0,    0,    0,   23,  394,
    0,    0,   24,   25,   26,   27,   28,   24,   25,   26,
   27,   28,    0,    0,   54,    0,    0,   56,   22,   54,
    0,    0,    0,    0,    0,    0,  197,   23,    0,    0,
    0,    0,   22,    0,    0,   23,   24,   25,   26,   27,
   28,   23,  791,    0,   24,   25,   26,   27,   28,  253,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,    0,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,   22,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,    0,    0,    0,  718,    0,    0,    0,
   24,   25,   26,   27,   28,   22,  351,  351,    0,    0,
  197,    0,    0,    0,   23,    0,    0,    0,    0,   23,
    0,    0,    0,   24,   25,   26,   27,   28,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,    0,
  197,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,  351,  351,
  351,    0,  351,  351,    0,    0,    0,  351,    0,  351,
    0,    0,  351,  351,  351,  351,  351,  351,  351,  351,
  351,  351,    0,  351,  351,    0,  351,  351,  351,  351,
  351,  351,  351,  351,  351,  351,  351,  351,  351,  325,
  325,  351,    0,    0,    0,  351,  351,  351,  351,  351,
    0,  351,  351,  351,  351,  351,  351,  351,    0,  351,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  351,  351,  351,  351,    0,    0,    0,  351,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  325,  325,  325,    0,  325,  325,    0,    0,    0,
  325,    0,  325,    0,    0,  325,  325,  325,  325,  325,
  325,  325,  325,  325,  325,    0,  325,  325,    0,  325,
  325,  325,  325,  325,  325,  325,  325,  325,  325,  325,
  325,  325,  307,  307,  325,    0,    0,    0,  325,  325,
  325,  325,  325,    0,  325,  325,  325,  325,  325,  325,
  325,    0,  325,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  325,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  325,  325,  325,  325,    0,    0,
    0,  325,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  307,  307,  307,    0,  307,  307,
    0,    0,    0,  307,    0,  307,    0,    0,  307,  307,
  307,  307,  307,  307,  307,  307,  307,  307,    0,  307,
  307,    0,  307,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  307,  307,  307,  312,  312,  307,    0,    0,
    0,  307,  307,  307,  307,  307,    0,  307,  307,  307,
  307,  307,  307,  307,    0,  307,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  307,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  307,  307,  307,
  307,    0,    0,    0,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,  312,  312,
    0,  312,  312,    0,    0,    0,  312,    0,  312,    0,
    0,  312,  312,  312,  312,  312,  312,  312,  312,  312,
  312,    0,  312,  312,    0,  312,  312,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  312,  312,  313,  313,
  312,    0,    0,    0,  312,  312,  312,  312,  312,    0,
  312,  312,  312,  312,  312,  312,  312,    0,  312,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  312,  312,  312,  312,    0,    0,    0,  312,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  313,  313,  313,    0,  313,  313,    0,    0,    0,  313,
    0,  313,    0,    0,  313,  313,  313,  313,  313,  313,
  313,  313,  313,  313,    0,  313,  313,    0,  313,  313,
  313,  313,  313,  313,  313,  313,  313,  313,  313,  313,
  313,  308,  308,  313,    0,    0,    0,  313,  313,  313,
  313,  313,    0,  313,  313,  313,  313,  313,  313,  313,
    0,  313,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  313,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  313,  313,  313,  313,    0,    0,    0,
  313,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  308,  308,  308,    0,  308,  308,    0,
    0,    0,  308,    0,  308,    0,    0,  308,  308,  308,
  308,  308,  308,  308,  308,  308,  308,    0,  308,  308,
    0,  308,  308,  308,  308,  308,  308,  308,  308,  308,
  308,  308,  308,  308,  318,  318,  308,    0,    0,    0,
  308,  308,  308,  308,  308,    0,  308,  308,  308,  308,
  308,  308,  308,    0,  308,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  308,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  308,  308,  308,  308,
    0,    0,    0,  308,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  318,  318,  318,    0,
  318,  318,    0,    0,    0,  318,    0,  318,    0,    0,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  318,
    0,  318,  318,    0,  318,  318,  318,  318,  318,  318,
  318,  318,  318,  318,  318,  318,  318,  340,  340,  318,
    0,    0,    0,  318,  318,  318,  318,  318,    0,  318,
  318,  318,  318,  318,  318,  318,    0,  318,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  318,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  318,
  318,  318,  318,    0,    0,    0,  318,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  340,
  340,  340,    0,  340,  340,    0,    0,    0,  340,    0,
  340,    0,    0,  340,  340,  340,  340,  340,  340,  340,
  340,  340,  340,    0,  340,  340,    0,  340,  340,  340,
  340,  340,  340,  340,  340,  340,  340,  340,  340,  340,
  336,  336,  340,    0,    0,    0,  340,  340,  340,  340,
  340,    0,  340,  340,  340,  340,  340,  340,  340,    0,
  340,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  340,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,  340,  340,  340,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  336,  336,  336,    0,  336,  336,    0,    0,
    0,  336,    0,  336,    0,    0,  336,  336,  336,  336,
  336,  336,  336,  336,  336,  336,    0,  336,  336,    0,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  336,
  336,  336,  336,  314,  314,  336,    0,    0,    0,  336,
  336,  336,  336,  336,    0,  336,  336,  336,  336,  336,
  336,  336,    0,  336,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  336,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  336,  336,  336,  336,    0,
    0,    0,  336,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  314,  314,  314,    0,  314,
  314,    0,    0,    0,  314,    0,  314,    0,    0,  314,
  314,  314,  314,  314,  314,  314,  314,  314,  314,    0,
  314,  314,    0,  314,  314,  314,  314,  314,  314,  314,
  314,  314,  314,  314,  314,  314,  309,  309,  314,    0,
    0,    0,  314,  314,  314,  314,  314,    0,  314,  314,
  314,  314,  314,  314,  314,    0,  314,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  314,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  314,  314,
  314,  314,    0,    0,    0,  314,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  309,  309,
  309,    0,  309,  309,    0,    0,    0,  309,    0,  309,
    0,    0,  309,  309,  309,  309,  309,  309,  309,  309,
  309,  309,    0,  309,  309,    0,  309,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,  309,  309,  310,
  310,  309,    0,    0,    0,  309,  309,  309,  309,  309,
    0,  309,  309,  309,  309,  309,  309,  309,    0,  309,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  309,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  309,  309,  309,  309,    0,    0,    0,  309,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  310,  310,  310,    0,  310,  310,    0,    0,    0,
  310,    0,  310,    0,    0,  310,  310,  310,  310,  310,
  310,  310,  310,  310,  310,    0,  310,  310,    0,  310,
  310,  310,  310,  310,  310,  310,  310,  310,  310,  310,
  310,  310,  315,  315,  310,    0,    0,    0,  310,  310,
  310,  310,  310,    0,  310,  310,  310,  310,  310,  310,
  310,    0,  310,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  310,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  310,  310,  310,  310,    0,    0,
    0,  310,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  315,  315,  315,    0,  315,  315,
    0,    0,    0,  315,    0,  315,    0,    0,  315,  315,
  315,  315,  315,  315,  315,  315,  315,  315,    0,  315,
  315,    0,  315,  315,  315,  315,  315,  315,  315,  315,
  315,  315,  315,  315,  315,  323,  323,  315,    0,    0,
    0,  315,  315,  315,  315,  315,    0,  315,  315,  315,
  315,  315,  315,  315,    0,  315,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  315,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  315,  315,  315,
  315,    0,    0,    0,  315,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  323,  323,  323,
    0,  323,  323,    0,    0,    0,  323,    0,  323,    0,
    0,  323,  323,  323,  323,  323,  323,  323,  323,  323,
  323,    0,  323,  323,    0,  323,  323,  323,  323,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  316,  316,
  323,    0,    0,    0,  323,  323,  323,  323,  323,    0,
  323,  323,  323,  323,  323,  323,  323,    0,  323,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  323,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,  323,  323,  323,    0,    0,    0,  323,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  316,  316,  316,    0,  316,  316,    0,    0,    0,  316,
    0,  316,    0,    0,  316,  316,  316,  316,  316,  316,
  316,  316,  316,  316,    0,  316,  316,    0,  316,  316,
  316,  316,  316,  316,  316,  316,  316,  316,  316,  316,
  316,  319,  319,  316,    0,    0,    0,  316,  316,  316,
  316,  316,    0,  316,  316,  316,  316,  316,  316,  316,
    0,  316,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  316,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  316,  316,  316,  316,    0,    0,    0,
  316,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  319,  319,  319,    0,  319,  319,    0,
    0,    0,  319,    0,  319,    0,    0,  319,  319,  319,
  319,  319,  319,  319,  319,  319,  319,    0,  319,  319,
    0,  319,  319,  319,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  337,  337,  319,    0,    0,    0,
  319,  319,  319,  319,  319,    0,  319,  319,  319,  319,
  319,  319,  319,    0,  319,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  319,  319,  319,  319,
    0,    0,    0,  319,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  337,  337,  337,    0,
  337,  337,    0,    0,    0,  337,    0,  337,    0,    0,
  337,  337,  337,  337,  337,  337,  337,  337,  337,  337,
    0,  337,  337,    0,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,  311,  311,  337,
    0,    0,    0,  337,  337,  337,  337,  337,    0,  337,
  337,  337,  337,  337,  337,  337,    0,  337,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  337,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  337,
  337,  337,  337,    0,    0,    0,  337,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  311,
  311,  311,    0,  311,  311,    0,    0,    0,  311,    0,
  311,    0,    0,  311,  311,  311,  311,  311,  311,  311,
  311,  311,  311,    0,  311,  311,    0,  311,  311,  311,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  317,  317,  311,    0,    0,    0,  311,  311,  311,  311,
  311,    0,  311,  311,  311,  311,  311,  311,  311,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  311,  311,  311,  311,    0,    0,    0,  311,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  317,  317,  317,    0,  317,  317,    0,    0,
    0,  317,    0,  317,    0,    0,  317,  317,  317,  317,
  317,  317,  317,  317,  317,  317,    0,  317,  317,    0,
  317,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  317,  317,  317,  320,  320,  317,    0,    0,    0,  317,
  317,  317,  317,  317,    0,  317,  317,  317,  317,  317,
  317,  317,    0,  317,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  317,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  317,  317,  317,  317,    0,
    0,    0,  317,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  320,  320,  320,    0,  320,
  320,    0,    0,    0,  320,    0,  320,    0,    0,  320,
  320,  320,  320,  320,  320,  320,  320,  320,  320,    0,
  320,  320,    0,  320,  320,  320,  320,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  321,  321,  320,    0,
    0,    0,  320,  320,  320,  320,  320,    0,  320,  320,
  320,  320,  320,  320,  320,    0,  320,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  320,  320,
  320,  320,    0,    0,    0,  320,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  321,  321,
  321,    0,  321,  321,    0,    0,    0,  321,    0,  321,
    0,    0,  321,  321,  321,  321,  321,  321,  321,  321,
  321,  321,    0,  321,  321,    0,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  321,  321,  321,  321,  322,
  322,  321,    0,    0,    0,  321,  321,  321,  321,  321,
    0,  321,  321,  321,  321,  321,  321,  321,    0,  321,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  321,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  321,  321,  321,  321,    0,    0,    0,  321,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  322,  322,  322,    0,  322,  322,    0,    0,    0,
  322,    0,  322,    0,    0,  322,  322,  322,  322,  322,
  322,  322,  322,  322,  322,    0,  322,  322,    0,  322,
  322,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  291,    0,  322,    0,    0,    0,  322,  322,
  322,  322,  322,    0,  322,  322,  322,  322,  322,  322,
  322,    0,  322,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  322,  322,  322,  322,    0,    0,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  292,  293,  294,    0,  295,  296,
    0,    0,    0,  297,    0,  298,    0,    0,  299,  300,
  301,  302,  303,  304,  305,  306,  307,  308,    0,  309,
  310,    0,  311,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,  322,  323,  247,    0,  324,    0,    0,
    0,  325,  326,  327,  328,  329,    0,  330,  331,  332,
  333,  334,  335,  336,    0,  337,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  338,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  339,  340,  341,
  342,    0,    0,    0,  343,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  247,  247,  247,
    0,  247,  247,    0,    0,    0,  247,    0,  247,    0,
    0,  247,  247,  247,  247,  247,  247,  247,  247,  247,
  247,    0,  247,  247,    0,  247,  247,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,  247,  248,    0,
  247,    0,    0,    0,  247,  247,  247,  247,  247,    0,
  247,  247,  247,  247,  247,  247,  247,    0,  247,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  247,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  247,  247,  247,  247,    0,    0,    0,  247,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  248,  248,  248,    0,  248,  248,    0,    0,    0,  248,
    0,  248,    0,    0,  248,  248,  248,  248,  248,  248,
  248,  248,  248,  248,    0,  248,  248,    0,  248,  248,
  248,  248,  248,  248,  248,  248,  248,  248,  248,  248,
  248,  249,    0,  248,    0,    0,    0,  248,  248,  248,
  248,  248,    0,  248,  248,  248,  248,  248,  248,  248,
    0,  248,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  248,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  248,  248,  248,  248,    0,    0,    0,
  248,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  249,  249,  249,    0,  249,  249,    0,
    0,    0,  249,    0,  249,    0,    0,  249,  249,  249,
  249,  249,  249,  249,  249,  249,  249,    0,  249,  249,
    0,  249,  249,  249,  249,  249,  249,  249,  249,  249,
  249,  249,  249,  249,  250,    0,  249,    0,    0,    0,
  249,  249,  249,  249,  249,    0,  249,  249,  249,  249,
  249,  249,  249,    0,  249,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  249,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  249,  249,  249,  249,
    0,    0,    0,  249,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  250,  250,  250,    0,
  250,  250,    0,    0,    0,  250,    0,  250,    0,    0,
  250,  250,  250,  250,  250,  250,  250,  250,  250,  250,
    0,  250,  250,    0,  250,  250,  250,  250,  250,  250,
  250,  250,  250,  250,  250,  250,  250,    0,    0,  250,
    0,    0,    0,  250,  250,  250,  250,  250,    0,  250,
  250,  250,  250,  250,  250,  250,    0,  250,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  250,    0,
    0,    0,    0,    0,    0,    0,    0,  295,    0,  250,
  250,  250,  250,    0,  298,    0,  250,  299,  300,  301,
  302,  303,  304,  305,  306,  307,  308,    0,  309,  310,
    0,  311,  312,  313,  314,  315,  316,  317,  318,  319,
  320,  321,  322,  323,    0,    0,  324,    0,    0,    0,
  325,  326,  327,  328,  329,    0,  330,  331,  332,  333,
  334,  335,  336,    0,  337,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  339,  340,  341,  342,
    0,    0,    0,  343,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   58,   62,    6,   42,   65,  261,  262,  263,   40,
  347,  527,  349,  125,  344,   33,   33,  123,   40,   40,
   60,  234,  123,  123,  123,   19,  123,  764,   40,  284,
  123,   44,  115,  123,  280,  123,   41,  717,   61,   44,
   60,  110,   99,   40,  781,   60,  276,   54,  323,   44,
   42,  281,  577,   44,   61,   62,   40,  213,   65,  537,
   44,   68,  218,   44,  162,   68,  306,   44,   40,   44,
  272,   91,   44,   44,   44,   82,   91,  160,   42,  148,
  817,   62,  151,  123,   42,   42,   44,   81,  260,   83,
   97,   85,   61,  333,  334,  102,  156,    0,  105,   61,
  107,   93,  109,  123,  107,   62,  109,  114,  123,  789,
  313,  118,  125,  270,  271,  122,   93,  124,  216,  124,
  323,  115,   61,   40,  280,  380,   61,   91,  135,  136,
  125,  138,  215,  379,  125,   61,  234,   40,    6,    7,
  281,  125,  355,   60,  669,  214,  313,  672,  260,  379,
  125,  629,   41,  125,  125,  125,  323,  257,  165,  166,
  291,  292,   41,   40,  276,   44,  503,   41,  274,  906,
   44,   60,   42,  274,   91,  274,  379,  274,  508,   62,
  187,  274,  189,  281,  274,  522,  274,  295,  296,  402,
    0,  266,  323,   61,   62,  257,  533,   65,   81,    0,
   68,   41,   91,  257,   44,  212,  123,  319,  320,  182,
  183,  286,  379,  325,  326,   42,  328,  329,  330,  331,
  416,  291,  292,  300,  420,  302,  233,  423,  231,   42,
   40,   44,  309,   40,  123,  295,  432,  105,  295,  107,
  247,  109,   61,  121,  264,  723,  253,   40,  288,  264,
  272,  272,  260,  273,  261,  262,  274,  274,  273,  277,
  272,  274,  282,  283,  284,  285,  286,  282,  283,  284,
  285,  286,  527,  610,  529,  272,  531,  315,   41,  274,
  253,   44,    0,  620,  341,  622,  164,   81,  295,   83,
   84,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  186,  787,
  188,   40,   41,  320,  321,   44,  571,  324,   40,   41,
  798,  799,   44,  260,  840,  119,  120,   41,   40,  123,
   44,  125,  339,  340,  341,   42,  343,   44,  341,  123,
  257,  258,  259,  257,  261,  262,  263,    0,  265,  380,
   42,  411,   44,  360,   41,  272,  273,   44,  267,  837,
   41,  381,  279,   44,  379,  268,  269,  245,  705,  272,
  287,   41,  275,  851,   44,  264,  854,  714,   41,  267,
  258,   44,  260,   42,  273,   44,  289,  290,  426,  278,
   42,  306,   44,  282,  283,  284,  285,  286,  123,   41,
  878,  879,   44,  410,  411,  294,  123,  410,   41,  416,
   40,   44,   41,  420,  892,   44,  423,   41,  379,  426,
   44,  324,   42,  293,   44,  432,  433,  295,  435,    0,
  291,  292,  123,   41,  295,  296,   44,  307,  308,  446,
  310,  311,  499,   41,  314,  452,  316,  317,  318,  319,
  320,   42,  322,   44,   41,   42,   41,   42,  268,  269,
  321,  378,  272,  273,  323,  275,   40,  268,  269,  476,
  257,  272,  273,  341,  275,  392,  393,  394,   40,  289,
  290,   41,   42,  132,   41,   42,  493,   40,  289,  290,
  497,  498,  499,   41,   42,  498,  499,   44,  753,  754,
  291,  292,   41,   42,  295,  296,  423,   58,    0,  379,
  517,  518,  519,   58,  324,   58,   62,   61,   40,  274,
  257,  379,  529,  324,   44,   40,  379,   44,   44,  260,
  257,   61,  181,  182,  183,  542,  332,  792,  420,  542,
  274,  274,  410,  411,  604,  313,  257,  196,  379,   44,
  268,  269,  387,  387,  272,  273,  125,  275,  397,  398,
  399,  400,  401,  402,  403,  404,  405,  406,  575,  387,
  577,  289,  290,  580,  581,  582,  583,  584,  585,  586,
  587,  588,  589,  590,  591,  840,  257,  379,   44,  238,
  202,  379,   44,   40,  601,  124,  603,  604,  273,  248,
  249,  250,   44,   44,  253,   44,  324,   44,   44,   44,
  693,   44,  387,  387,  387,  268,  269,  387,  387,  272,
  273,  270,  275,    0,  631,  387,  387,  387,   41,  497,
  498,  499,  260,  387,  387,  387,  289,  290,  387,   40,
  274,  257,  257,  292,  379,  294,   62,  296,  257,  379,
  257,  379,   44,  266,   40,   44,   44,   44,  257,   60,
  720,   44,  669,   44,   44,  672,  315,  316,  317,  318,
  319,  324,  770,  322,  542,   44,  325,  326,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,  771,  772,
   91,  260,  775,  776,   44,  702,   44,  268,  269,  702,
   44,  272,  273,   44,  275,   44,   44,  276,   44,   91,
  717,  718,   44,   44,   44,   44,   44,  366,  289,  290,
   44,   44,  123,  257,  257,  823,  824,  387,   44,  827,
  828,    0,   44,  601,   44,  603,  604,   44,   44,   44,
   44,   44,  825,   44,   44,   44,  829,  830,  257,   44,
  319,  320,   44,  324,   44,  367,  325,  326,   44,  328,
  329,  330,  331,   44,   44,  863,   44,  865,  866,   91,
  424,  260,   44,   44,  379,  379,  268,  269,   44,   44,
  272,  273,  789,  275,  867,  387,  884,  276,   44,  796,
  793,  387,   44,    0,  406,  379,  260,  289,  290,  448,
   44,   40,  327,  415,  387,   93,  418,  419,  257,  421,
  422,  379,  424,  425,  257,  427,  428,  429,  430,  431,
  379,   44,  434,  257,  436,  437,  438,  439,   44,   40,
  319,  320,  324,  327,  702,   40,  325,  326,  380,  328,
  329,  330,  331,   40,  257,  258,  259,  327,  261,  262,
  263,  257,  265,  257,   40,    0,  257,  258,  259,  496,
  261,  262,  263,  276,  265,  260,  279,  516,  327,  387,
   42,  272,  273,  387,  287,  387,   44,    0,  279,  886,
   40,  272,  272,  495,   10,   86,  287,  894,  895,  896,
   19,  268,  269,  272,  272,  272,  273,  272,  275,  196,
  265,  195,  262,  540,  541,   97,  529,  353,  229,   58,
  212,  835,  289,  290,  233,  753,  346,  754,  346,  568,
  569,  570,  403,  572,  573,  796,    0,  691,  531,  789,
  579,  403,  544,  268,  269,   -1,  548,  272,  273,  551,
  275,   -1,  554,   -1,   60,   -1,  125,  324,  560,  561,
   -1,  563,   -1,   -1,  289,  290,   -1,  606,   -1,   -1,
   -1,   -1,  599,  600,  613,  602,   40,   41,   42,   -1,
   44,   -1,   -1,   -1,   -1,   91,   -1,  378,   -1,   -1,
  592,  593,  594,   -1,   -1,   -1,   60,   -1,   62,  324,
   -1,  392,  393,  394,   -1,   -1,   -1,   -1,  635,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  123,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,   93,
  289,  290,  423,   -1,  636,  674,  638,  639,   -1,  641,
  642,   -1,  644,  645,   -1,  647,  648,  649,  650,  651,
   -1,   -1,  654,  692,  656,  657,  658,  659,   -1,  123,
   -1,  125,   -1,   -1,   -1,  324,   -1,   -1,  670,  696,
   -1,  268,  269,  700,  701,  272,  273,   -1,  275,   -1,
   -1,   -1,   -1,   40,   41,   -1,   -1,   44,   -1,   -1,
   -1,  260,  289,  290,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   62,   -1,  276,   -1,   -1,
  272,  273,  751,  752,   -1,   -1,   -1,  279,  720,   40,
   -1,   -1,   -1,   -1,   -1,  287,  728,  324,   -1,  731,
   -1,   -1,  734,  268,  269,  737,   93,  272,  273,   60,
  275,  743,  744,  782,  746,   -1,   -1,   -1,   -1,   -1,
  319,  320,  779,   -1,  289,  290,  325,  326,  264,  328,
  329,  330,  331,  765,  766,  767,  123,  273,  125,   -1,
   91,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,  822,   -1,   -1,   40,   -1,   -1,  324,
   -1,  793,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,  123,   -1,  268,  269,   -1,   -1,  272,  273,
  274,  275,   -1,   -1,   -1,  279,  378,   -1,   -1,   -1,
   -1,   -1,   -1,  287,   -1,  289,  290,   -1,   -1,  293,
  392,  393,  394,   -1,   -1,  874,  875,  876,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,   -1,
  314,  315,  316,  317,  318,  319,  320,   -1,  322,  898,
  324,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,  380,  339,  340,   -1,   -1,    0,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   40,   -1,  378,   -1,  273,  274,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,  407,  265,   -1,   -1,   -1,   -1,   -1,
   -1,  272,  273,   -1,  418,  419,  420,  421,  279,  423,
   41,  425,  426,   -1,   -1,   -1,  287,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,
  337,   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,
   -1,   -1,  349,  350,  351,  352,  353,  354,  355,  356,
  357,  358,   -1,  360,  361,    0,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  273,
  274,  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,
  387,  388,  389,  390,  391,  392,  393,  394,   40,  396,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  407,  261,  262,  263,   -1,  265,   -1,  378,   -1,   -1,
   -1,  418,  419,  420,  421,   -1,   42,   -1,  425,  279,
   -1,  392,  393,  394,   -1,   -1,   -1,  287,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   60,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,   -1,  378,   91,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   40,  396,   -1,   -1,  273,  274,  268,  269,   -1,
   -1,  272,  273,  407,  275,   -1,   -1,  123,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,  289,  290,
   41,  425,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   60,
   -1,   -1,   -1,   -1,   -1,  276,   -1,   -1,  279,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  287,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,
   91,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,   40,   -1,
  378,   -1,  123,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,  273,  274,  268,  269,   -1,   -1,  272,  273,  407,
  275,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
  418,  419,  420,  421,  289,  290,   -1,  425,  264,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,   -1,
   -1,  287,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  324,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,    0,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  274,  378,   40,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,  264,  396,   -1,  362,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,  407,   -1,  278,   -1,   -1,
   -1,  282,  283,  284,  285,  286,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,   -1,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   -1,    0,  378,
   40,  273,  274,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,  261,  262,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  407,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,  418,
  419,  420,  421,   -1,  276,   -1,  425,  279,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  287,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,
   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,   -1,   -1,  378,   40,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
  273,  274,   -1,  268,  269,  407,   -1,  272,  273,   -1,
  275,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,  289,  290,  399,  400,  401,  402,
   -1,   -1,   -1,   -1,   -1,  408,  409,  410,  411,  412,
  413,  414,  415,  416,  417,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  324,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,    0,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  273,  274,  378,   40,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  407,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,  418,  419,  420,  421,   -1,
   -1,   -1,  425,   -1,   -1,   -1,   -1,  289,  290,   -1,
   -1,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,
  340,   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,
  350,  351,  352,  353,  354,  355,  356,  357,  358,   -1,
  360,  361,  324,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  375,   -1,    0,  378,   40,
  273,  274,  382,  383,  384,  385,  386,   -1,  388,  389,
  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,
  420,  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,
   -1,  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,
  353,  354,  355,  356,  357,  358,   -1,  360,  361,   -1,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,   -1,   -1,  378,   40,   -1,   -1,  382,
  383,  384,  385,  386,   -1,  388,  389,  390,  391,  392,
  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,  273,
  274,   -1,  268,  269,  407,   -1,  272,  273,   -1,  275,
   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,
  293,   -1,  425,  289,  290,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,
   -1,  314,   -1,  316,  317,  318,  319,  320,   -1,  322,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  324,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   -1,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,  273,  274,  378,   40,  379,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  407,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   -1,   -1,   -1,  289,  290,   -1,   -1,
   -1,   -1,   -1,   -1,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,  324,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,   -1,   -1,  378,   -1,  273,
  274,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,
  344,   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,
  354,  355,  356,  357,  358,   42,  360,  361,   -1,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  375,   -1,   60,  378,   -1,   -1,   -1,  382,  383,
  384,  385,  386,   -1,  388,  389,  390,  391,  392,  393,
  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,  407,   91,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,
   -1,  425,   -1,   42,   -1,   44,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,  291,  292,
   -1,   60,  295,  296,  297,  298,  299,  300,  301,  302,
  303,  304,  305,   -1,   -1,   -1,   -1,   -1,   -1,  312,
  335,  336,  337,   -1,  339,  340,   -1,   -1,  321,  344,
   -1,  346,   91,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,   -1,   42,  378,  123,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   60,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   42,   -1,   44,   -1,   -1,
   -1,   -1,   -1,  418,  419,  420,  421,   -1,   -1,   -1,
  425,   91,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  598,   -1,   -1,   -1,   -1,   -1,   -1,   42,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,  123,   91,  272,  273,   60,   -1,   -1,
   -1,   -1,  279,   -1,   -1,   -1,   -1,  633,  634,   -1,
  287,   -1,   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   -1,   -1,  123,   -1,   91,   -1,
  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,  316,
  317,  318,  319,  320,   -1,  322,   -1,   -1,  257,  258,
  259,   91,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  273,   -1,   -1,   -1,  694,  695,
  279,   -1,  698,  699,   -1,   -1,   -1,   -1,  287,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  726,  378,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  392,  393,  394,   91,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,  773,   -1,  279,
  123,  777,  778,   -1,   91,   -1,   -1,  287,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  378,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,  279,  392,  393,  394,  123,   -1,   -1,   60,
  287,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,  831,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,  279,  257,  258,  259,
   91,  261,  262,  263,  287,  265,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  287,  378,   -1,
   -1,   -1,  123,  293,   -1,   -1,   60,   -1,   -1,   -1,
   -1,   -1,  392,  393,  394,   -1,   -1,  307,  308,   -1,
  310,  311,   60,   -1,  314,   -1,  316,  317,  318,  319,
  320,  378,  322,   -1,  257,  258,  259,   91,  261,  262,
  263,   -1,  265,   -1,   -1,  392,  393,  394,   -1,  272,
  273,   -1,   -1,   91,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  287,  378,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,  392,
  393,  394,   -1,   -1,   -1,  123,  273,   -1,  378,  379,
   -1,   60,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,  392,  393,  394,   60,  293,   -1,   -1,   -1,
   -1,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,   91,  310,  311,  312,   -1,  314,   -1,  316,
  317,  318,  319,  320,   -1,  322,   91,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,  333,  334,   -1,   -1,
   -1,   -1,  273,   -1,  123,  378,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,  123,  392,
  393,  394,  293,   -1,   -1,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,  310,
  311,   -1,  379,  314,   -1,  316,  317,  318,  319,  320,
  321,  322,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,  282,  283,
  284,  285,  286,   -1,   -1,  273,   60,   -1,  123,  293,
   -1,   -1,   -1,   60,  282,  283,  284,  285,  286,   -1,
   -1,   -1,  306,  307,  308,  293,  310,  311,  379,   60,
  314,   -1,  316,  317,  318,  319,  320,   91,  322,  307,
  308,   -1,  310,  311,   91,   -1,  314,   -1,  316,  317,
  318,  319,  320,  321,  322,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,  123,
   41,   -1,   -1,   -1,  273,   -1,  123,   -1,   -1,  264,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  273,   60,
   -1,   -1,  123,   -1,   -1,  379,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,  307,  308,   -1,  310,  311,   -1,   -1,  314,
  125,  316,  317,  318,  319,  320,  321,  322,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  362,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  307,  308,  379,  310,  311,   -1,   -1,  314,
   -1,  316,  317,  318,  319,  320,   -1,  322,   -1,   60,
  264,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,  282,  283,
  284,  285,  286,  264,  125,  282,  283,  284,  285,  286,
   91,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,  293,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,  307,  308,  273,  310,
  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,  320,
   -1,  322,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
  347,  348,  273,   -1,   -1,   -1,   -1,   -1,  362,   -1,
   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  294,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,  379,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,   -1,  273,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  418,  419,  420,  421,  278,   -1,   -1,
  425,  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,
  125,   -1,   -1,  294,  335,  336,  337,   -1,  339,  340,
   -1,   -1,   -1,  344,   -1,  346,   -1,   -1,  349,  350,
  351,  352,  353,  354,  355,  356,  357,  358,   -1,  360,
  361,   -1,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  375,  273,   -1,  378,   -1,   -1,
   -1,  382,  383,  384,  385,  386,   -1,  388,  389,  390,
  391,  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   60,  418,  419,  420,
  421,   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,   -1,  335,  336,  337,
   -1,  339,  340,   -1,   -1,   -1,  344,   91,  346,   -1,
   -1,  349,  350,  351,  352,  353,  354,  355,  356,  357,
  358,   -1,  360,  361,   -1,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  273,  123,
  378,   -1,   -1,   -1,  382,  383,  384,  385,  386,   -1,
  388,  389,  390,  391,  392,  393,  394,   -1,  396,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  418,  419,  420,  421,   -1,   -1,   -1,  425,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,
  335,  336,  337,   -1,  339,  340,   -1,   -1,   -1,  344,
   -1,  346,   -1,   -1,  349,  350,  351,  352,  353,  354,
  355,  356,  357,  358,   -1,  360,  361,   -1,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  375,  273,   -1,  378,   -1,   -1,   -1,  382,  383,  384,
  385,  386,   -1,  388,  389,  390,  391,  392,  393,  394,
   -1,  396,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  407,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   60,  418,  419,  420,  421,   -1,   -1,  273,
  425,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,  335,  336,  337,   -1,  339,  340,   -1,
  294,   -1,  344,   91,  346,   -1,   60,  349,  350,  351,
  352,  353,  354,  355,  356,  357,  358,   -1,  360,  361,
   -1,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  375,  273,  123,  378,   91,   -1,   -1,
  382,  383,  384,  385,  386,   -1,  388,  389,  390,  391,
  392,  393,  394,   -1,  396,   -1,   -1,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   -1,  407,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  418,  419,  420,  421,
   -1,   -1,   -1,  425,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,  335,  336,  337,   -1,
  339,  340,   -1,   -1,   -1,  344,   60,  346,   -1,   -1,
  349,  350,  351,  352,  353,  354,  355,  356,  357,  358,
   -1,  360,  361,  123,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,   91,   -1,  378,
   60,   -1,   -1,  382,  383,  384,  385,  386,   -1,  388,
  389,  390,  391,  392,  393,  394,   -1,  396,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  407,  123,
   60,   91,   -1,   -1,   -1,   60,  264,   -1,   -1,  418,
  419,  420,  421,   -1,   -1,  273,  425,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,
   -1,   91,   -1,  123,   60,  125,   91,   -1,   -1,   -1,
  264,   -1,   60,   -1,   -1,   -1,   -1,   -1,   60,  273,
   -1,  309,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,  123,   -1,   91,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   -1,  309,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,   -1,  123,   -1,  125,
   -1,   -1,   -1,  273,  274,  123,  276,  125,   -1,  279,
   -1,  123,  282,  283,  284,  285,  286,  287,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,   60,  273,
  274,   -1,  276,   -1,   91,  279,   93,   -1,  282,  283,
  284,  285,  286,  287,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,  264,  265,   60,   -1,   -1,   91,
   -1,   -1,   -1,  273,  274,   -1,  123,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   91,   -1,  264,  265,   60,   91,   -1,  264,
  265,  123,   -1,  273,  274,   -1,   -1,   -1,  273,  274,
   -1,   -1,  282,  283,  284,  285,  286,  282,  283,  284,
  285,  286,   -1,   -1,  123,   -1,   -1,   91,  264,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  264,  273,   -1,   -1,
   -1,   -1,  264,   -1,   -1,  273,  282,  283,  284,  285,
  286,  273,  274,   -1,  282,  283,  284,  285,  286,  123,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,
  282,  283,  284,  285,  286,  264,  273,  274,   -1,   -1,
  264,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,  282,  283,  284,  285,  286,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,  335,  336,
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

#line 1329 "Iril/IR/IR.jay"

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
