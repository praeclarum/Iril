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
    "READONLY","READNONE","HIDDEN","ATTRIBUTE_GROUP_REF","ATTRIBUTES",
    "NORECURSE","NOUNWIND","UNWIND","SPECULATABLE","SSP","UWTABLE",
    "ARGMEMONLY","SEQ_CST","DSO_LOCAL","DSO_PREEMPTABLE","RET","BR",
    "SWITCH","INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET",
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
  { yyVal = true; }
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
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
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
#line 570 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 166:
#line 574 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Align8;
    }
  break;
case 174:
#line 597 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 175:
#line 598 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 176:
#line 599 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 177:
#line 600 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 178:
#line 601 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 179:
#line 602 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 180:
#line 603 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 181:
#line 604 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 182:
#line 605 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 183:
#line 606 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 184:
#line 610 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 185:
#line 611 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 186:
#line 612 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 187:
#line 613 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 188:
#line 614 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 189:
#line 615 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 190:
#line 616 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 191:
#line 617 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 192:
#line 618 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 193:
#line 619 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 194:
#line 620 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 195:
#line 621 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 196:
#line 622 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 197:
#line 623 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 199:
#line 625 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 200:
#line 629 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 204:
#line 639 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 205:
#line 643 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 206:
#line 647 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 207:
#line 651 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 655 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 659 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 663 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 675 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 214:
#line 676 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 215:
#line 677 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 216:
#line 678 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 217:
#line 679 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 218:
#line 680 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 219:
#line 681 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 220:
#line 682 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 221:
#line 683 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 222:
#line 690 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 697 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 701 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 225:
#line 708 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 715 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 719 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 726 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 734 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 231:
#line 741 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 745 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 756 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 760 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 767 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 771 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 778 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 782 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 240:
#line 786 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 790 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 242:
#line 797 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 801 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 808 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 812 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 816 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 820 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 828 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 250:
#line 829 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 251:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 847 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 254:
#line 851 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 255:
#line 855 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 256:
#line 859 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 257:
#line 863 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 258:
#line 867 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 259:
#line 871 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 261:
#line 876 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 262:
#line 880 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 263:
#line 884 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 264:
#line 888 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 265:
#line 892 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 270:
#line 909 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 913 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 919 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 273:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 937 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 955 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 282:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new InlineAssemblyValue ((string)yyVals[-2+yyTop], (string)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 973 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 980 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 984 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 988 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 992 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 996 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 290:
#line 1000 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 292:
#line 1008 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1012 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1016 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1020 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1028 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1032 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1036 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 299:
#line 1040 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 300:
#line 1044 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1048 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 302:
#line 1052 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 303:
#line 1056 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1060 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 305:
#line 1064 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 306:
#line 1068 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 307:
#line 1072 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 308:
#line 1076 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 309:
#line 1080 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 310:
#line 1084 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 311:
#line 1088 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 312:
#line 1092 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 313:
#line 1096 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 314:
#line 1100 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 315:
#line 1104 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 316:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 317:
#line 1112 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 318:
#line 1116 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 319:
#line 1120 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 320:
#line 1124 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 321:
#line 1128 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1132 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1136 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1140 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1144 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1148 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1152 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1156 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1160 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1164 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1168 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 332:
#line 1172 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 333:
#line 1176 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1180 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1184 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1188 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1192 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1196 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1200 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 340:
#line 1204 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1208 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 342:
#line 1212 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 343:
#line 1216 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 344:
#line 1220 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 345:
#line 1224 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1228 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1232 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1236 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 349:
#line 1240 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1244 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1248 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1252 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1256 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1260 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1264 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1268 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1272 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 358:
#line 1276 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 359:
#line 1280 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 360:
#line 1284 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 361:
#line 1288 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 362:
#line 1292 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 363:
#line 1296 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 364:
#line 1300 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 365:
#line 1304 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 366:
#line 1308 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 367:
#line 1312 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 368:
#line 1316 "Iril/IR/IR.jay"
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
   39,   39,   39,   39,   39,   39,   16,   16,   42,   42,
   37,   37,   47,   48,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   50,
   51,   51,   13,   13,   13,   13,   13,   13,   13,   13,
   13,   54,   20,   20,   20,   20,   20,   20,   20,   20,
   20,   55,   27,   27,   56,   53,   53,   25,   57,   57,
   52,   52,   58,   59,   59,   36,   36,   60,   60,   60,
   60,   61,   61,   63,   63,   63,   63,   65,   66,   66,
   67,   67,   68,   68,   68,   68,   68,   68,   68,   69,
   69,   69,   69,   69,   69,   21,   21,   70,   70,   71,
   71,   72,   73,   73,   74,   75,   75,   76,   76,   43,
   77,   78,   62,   62,   79,   79,   79,   79,   79,   79,
   79,   80,   80,   80,   80,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,
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
    1,    1,    1,    1,    4,    2,    1,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    6,    9,    6,    6,    3,    3,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    2,    2,    1,    2,    1,    3,    2,    1,    2,
    1,    3,    1,    1,    3,    1,    2,    2,    3,    1,
    2,    1,    2,    1,    2,    3,    4,    1,    3,    2,
    1,    3,    2,    3,    3,    3,    2,    4,    5,    1,
    1,    6,    9,    6,    6,    1,    3,    1,    1,    1,
    3,    5,    1,    2,    3,    1,    2,    1,    1,    1,
    1,    5,    1,    3,    2,    7,    2,    2,    7,    1,
    1,    8,    9,    9,   10,    5,    6,    5,    7,    5,
    5,    6,    4,    4,    5,    6,    6,    7,    5,    5,
    6,    6,    6,    7,    5,    6,    7,    7,    8,    6,
    4,    4,    5,    6,    5,    2,    5,    4,    4,    4,
    4,    5,    6,    7,    6,    6,    6,    4,    3,    4,
    7,    8,    5,    6,    5,    5,    6,    3,    4,    5,
    6,    7,    4,    5,    6,    6,    4,    5,    7,    8,
    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   95,  106,   98,   99,  100,  101,   97,  129,   39,
   37,   40,   41,   42,   43,   44,   45,  280,  160,  161,
  162,    0,   38,  163,  155,  156,  158,  157,  159,  169,
  170,    0,    0,    0,    0,   96,    0,    0,    0,    0,
    0,  130,  131,    0,    0,  153,    0,    0,    3,    0,
    4,    0,    0,  167,  168,   35,   36,   46,   47,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  166,
   89,    0,    0,    0,    0,    0,    0,    0,  135,    0,
    0,    0,  164,    0,    0,    0,    0,    0,    0,    0,
  154,    0,    0,    0,    5,    6,    0,    0,    0,    0,
    0,    0,    0,    0,    8,    0,    7,    0,    0,    0,
    0,    0,   90,    0,    0,    0,    0,  134,    0,  112,
  102,    0,    0,  109,    0,    0,    0,    0,    0,    0,
    0,  151,  152,  146,    0,    0,  147,  173,    0,    0,
    0,  171,    0,    0,    0,  215,  216,  214,  217,  218,
  219,  213,  204,  221,  220,    0,    0,    0,    0,    0,
    0,    0,    0,  203,    0,    0,    0,    0,    0,    0,
    0,    0,   48,    0,    0,    0,   74,   73,   13,    0,
    0,   67,   72,  165,    0,    0,    0,    0,  105,  103,
    0,    0,    0,    0,    0,  138,    0,    0,    0,   87,
   79,   77,   78,   80,   81,   82,   83,    0,   75,    0,
  145,    0,    0,    0,    0,    0,    0,    0,  122,  172,
    0,    0,    0,    0,    0,    0,    0,    0,  226,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   15,    0,    0,    0,   68,   14,    0,  200,  202,  201,
  223,  107,   91,  108,  110,  139,    0,    0,  140,    0,
    0,   12,   76,  148,    0,  118,   65,    0,    0,    0,
    0,    0,    0,  290,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  236,    0,    0,  242,    0,  283,  291,    0,    0,
  136,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  211,    0,  209,  210,    0,    0,    0,    0,    0,   61,
   64,    0,   59,    0,   50,   62,    0,   56,   58,   63,
   60,   51,   52,   49,   17,   16,   71,   70,   69,  141,
   84,  269,  268,    0,  266,    0,    0,  288,    0,    0,
  285,    0,    0,    0,    0,  287,  278,  279,    0,    0,
  276,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  281,  326,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  174,
  175,  176,  177,  178,  179,  180,  181,  182,  183,    0,
  184,  185,  196,  197,  198,  199,  187,  189,  190,  191,
  192,  188,  186,  194,  195,  193,    0,    0,    0,    0,
    0,    0,    0,    0,  113,  237,    0,  243,    0,    0,
   66,    0,  123,   33,    0,    0,    0,    0,    0,    0,
    0,    0,  227,    0,    0,    0,    0,    0,    0,  228,
    0,   88,    0,  119,    0,  284,  222,    0,    0,  248,
    0,    0,    0,    0,    0,    0,  277,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  270,    0,    0,
    0,    0,    0,    0,    0,    0,  339,    0,    0,  114,
   20,    0,   28,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   54,    0,   57,  267,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  321,
    0,    0,  233,  234,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,  208,  205,  207,    0,
   23,    0,    0,   53,    0,    0,    0,  250,    0,    0,
  251,    0,    0,    0,    0,  296,    0,  323,  361,    0,
  332,  346,    0,  327,  364,    0,  350,  325,  366,  358,
  354,    0,    0,  343,    0,  301,  300,  345,  367,    0,
    0,    0,    0,  298,    0,    0,  212,  225,    0,    0,
    0,    0,    0,    0,    0,    0,  271,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  273,    0,    0,    0,  253,  249,
    0,    0,    0,    0,    0,  297,  362,  347,  351,  355,
  344,  302,  336,  356,  235,    0,    0,    0,    0,    0,
    0,    0,    0,  229,    0,  231,  335,  324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  286,    0,  289,  274,    0,  261,  255,    0,    0,    0,
    0,  260,  256,  254,  252,    0,    0,    0,    0,  299,
    0,  341,    0,  359,    0,  230,    0,  272,  352,    0,
    0,    0,    0,    0,  206,  275,  258,    0,    0,    0,
    0,    0,  292,    0,    0,    0,  342,  360,  232,  282,
    0,  259,    0,    0,    0,    0,  293,  294,    0,    0,
    0,    0,    0,  295,    0,    0,    0,    0,    0,  265,
  262,  264,    0,    0,  263,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   56,   12,   13,   14,  228,  200,  192,   57,
   81,  201,  269,   82,  236,  214,   84,  193,  375,  184,
  394,  377,  378,  379,  380,  202,  804,  229,   93,   94,
  143,  144,   15,  114,  160,  341,  215,  239,   66,   61,
   67,   62,   63,  216,  156,  157,  162,  470,  487,  270,
  530,  805,  250,  748,  401,  661,  806,  654,  655,  342,
  343,  344,  345,  346,  531,  622,  710,  711,  833,  395,
  587,  588,  774,  775,  410,  411,  445,  689,  347,  348,
  };
  protected static readonly short [] yySindex = {          199,
  -21, -179,    7,   17,   83, 3002, 3312, -278,    0,  199,
    0,    0,    0,    0, -223, -109,  100,  126, 7067,  -86,
  -27,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  162,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -35, 4140,  -97,   -9,    0, -141,  221,  273, 4249,
 3067,    0,    0, 3351,  -18,    0, 3351,  262,    0,  287,
    0,   73,   95,    0,    0,    0,    0,    0,    0,   62,
 4249,  136,  -66,  -19,  -51,  322,  -23,  249,  123,    0,
    0,  221,  -12,  273,  129, 4249,  152,  116,    0,   51,
 1989,  273,    0, 4249,  273, 3351,  -17, 3351,  287,  -16,
    0,  316, 1866, -107,    0,    0, 4249,  -66,  -66, 2842,
 4249,  -66, 4249,  -66,    0,  319,    0, -238,  406,  324,
 3202,  409,    0, 4249, 4249,   12, 4249,    0,  196,    0,
    0,  221,  108,    0,  273,  273,  287,  -15, -107,  287,
 3244,    0,    0,    0,  228,  131,    0,    0,  130, -108,
 -253,    0, 2681, 4249, 4249,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   75,  415,  422,  429, 4255,
 4255, 4255,  426,    0, 2842, 4249, 2842, 4249,  417,  418,
  424,  142,    0, -238, 3207,    0,    0,    0,    0,  -36,
 2759,    0,    0,    0,  221,    9,  421,   -8,    0,    0,
 2481, -107,  287,  130,  130,    0, -107,  419,  444,    0,
    0,    0,    0,    0,    0,    0,    0,  755,    0, 1457,
    0, 2358, -243,  218, 6452, -106, 4255,  239,    0,    0,
  120,  456,  221, 2829,  461, 4255, 4255, 4255,    0,   20,
  111,   15,  124,  459, 2842,  465, 2842, 4038, 4071,  577,
    0, -238,  160,   -5,    0,    0, 4106,    0,    0,    0,
    0,    0,    0,    0,    0,    0, -107,  130,    0,  250,
   63,    0,    0,    0,  263,    0,    0,  458, 4255, -192,
 4255, 3145, 4255,    0, 2784, 4249, 2784, 4249, 2784, 4249,
 4249, 1497, 4249, 4249, 4249, 2784, 1620, 1743, 4249, 4249,
 4249, 4255, 4255, 4255, 4255, 4255, 4249, 2081, 2204,  194,
 1097, 4255, 4255, 4255, 4255, 4255, 4255, 4255, 4255, 4255,
 4255, 4255, 4255,  745, 4020, 4249, 4249, 3145,  107, 4249,
 3343,    0, 6452,  253,    0,  253,    0,    0,  257, 6452,
    0,  217,  276, -259,  156,  496, 4249,  155,  157,  163,
    0, 4255,    0,    0,  295,  173,  509,  180,  510,    0,
    0,  515,    0,  986,    0,    0,  435,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  177,    0,  217, 6937,    0,  289, 2952,
    0,  534, 1017, 3351, 3351,    0,    0,    0, 2759, 2784,
    0, 2759, 2759, 2784, 2759, 2759, 2784, 2759, 2759, 4249,
 2759, 2759, 2759, 2759, 2759, 2784, 4249, 2759, 4249, 2759,
 2759, 2759, 2759,  535,  538,  539,  540,  541,  170, 4249,
  190, 4255,  542,    0,    0, 4249,  202,  205,  206,  214,
  219,  222,  223,  224,  226,  227,  231,  236,  237,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4249,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4249,   35, 2759, 1017,
 4249, 3351, 3145,  -28,    0,    0,  253,    0,  314,  314,
    0, 3445,    0,    0,  347,  346,  357,  252,  275, 4249,
 4249, 4249,    0,  253,  375,  255,  378,  258, 2115,    0,
 4071,    0,   63,    0,  253,    0,    0,  594,  373,    0,
  600, 1017, 1017, 3351,  601, 2759,    0,  602,  604, 2759,
  607,  610, 2759,  611,  613, 2759,  616,  617,  622,  623,
  625, 2759, 2759,  627, 2759,  628,  632,  635,  637, 4255,
 4255, 4255,  577, 4255, 1374,  344, 4249,  638, 4249,  368,
 4255, 4249, 4249, 4249, 4249, 4249, 4249, 4249, 4249, 4249,
 4249, 4249, 4249, 2759, 2759, 2952,  641,    0,  645,  600,
 1017, 1017, 4249,  102, 4249, 3351,    0, 4255,  314,    0,
    0,  253,    0,  387, 4255,  307,  351,  365,  314,  253,
  434,  253,  440,    0,  266,    0,    0,  314,  373,  612,
 2238,  318,  600,  600, 1017, 2952,  658, 2952, 2952,  661,
 2952, 2952,  662, 2952, 2952,  664, 2952, 2952, 2952, 2952,
 2952,  665,  666, 2952,  668, 2952, 2952, 2952, 2952,    0,
  669,  670,    0,    0,  674,  675,  463,  677, 4249, 2759,
  679, 4249,  683, 4255,  690,  221,  221,  221,  221,  221,
  221,  221,  221,  221,  221,  221,  221,  693,  700,  702,
  660, 4255,  130,  600,  600, 1017,  330,  600,  600, 1017,
 1017, 3351,    0,  314,  253,  710,    0,    0,    0,  314,
    0,  314,  253,    0,  712, 4249, 2573,    0, 2607,  294,
    0,  373,  372,  374,  600,    0, 2952,    0,    0, 2952,
    0,    0, 2952,    0,    0, 2952,    0,    0,    0,    0,
    0, 2952, 2952,    0, 2952,    0,    0,    0,    0, 4255,
 4255,  577,  577,    0,  379,  715,    0,    0,  383,  719,
  386,  723,  -24, 2952, 2952, 2952,    0,  727,  130,  130,
  130,  600,  514,  130,  130,  600,  600, 1017,  314,  -24,
  314,  373,  736, 4187,    0,  744, 1258, 2881,    0,    0,
 4214,  462,  373,  373,  403,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  674,  537,  413,  543,  425,
  548,  -24, 4255,    0,  752,    0,    0,    0,  716, 4255,
  130,  130,  130,  764,  130,  130,  130,  130,  600,  296,
    0,  373,    0,    0, 2124,    0,    0,  431,  771,  775,
  776,    0,    0,    0,    0,  373,  491,  495,  373,    0,
  565,    0,  566,    0,  752,    0,  -24,    0,    0,  130,
  567,  130,  130,  130,    0,    0,    0,  303,  786, 4255,
 4255, 4255,    0,  373,  373,  504,    0,    0,    0,    0,
  130,    0, 4249,  455,  457,  464,    0,    0,  373,  371,
 4249, 4249, 4249,    0, 4255,  376,  384,  394,  803,    0,
    0,    0,  -24,  327,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  848,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3232,  520,  579,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   -3,    0,    0,    0,    0,    0, 3272,    0,  844,
    0,  582,    0,    0,  583,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  809,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  183,    0,    0,  586,  587,    0,    0,  188,    0,
    0,    0,    0,    0,  195,    0,    0,    0, -105,    0,
 -103,    0,  101,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  809,    0,  809,    0,    0,    0,
    0,    0,    0,    0,    0,  967,    0,    0,    0,    0,
  809,    0,    0,    0,    2,  809,    0,  809,    0,    0,
    0,    0,    0,  329,  352,    0,    0, 2837, 2927,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  338,
    0,    0,  -96,    0,    0,    0,    0,    0,    0,    0,
    0,  405,  176,  809,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  809,    0,  809,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  497,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3547,    0, 6554,    0,    0,    0,    0,
    0,  -95,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  529,    0,
    0,   22,    0,  809,    0,    0,  343,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -93,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  809,    0,
    0,  809,  809,    0,  809,  809,    0,  809,  809,    0,
  809,  809,  809,  809,  809,    0,    0,  809,    0,  809,
  809,  809,  809,    0,    0,    0,    0,    0,  809,    0,
  809,    0,    0,    0,    0,    0,  809,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  809,  809,    0,
    0,    0,    0,  809,    0,    0, 3649,    0, 3751, 6656,
    0,    0,    0,    0,    0,    0,    0,    0,  809,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 6758,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  809,    0,    0,    0,  809,
    0,    0,  809,    0,    0,  809,    0,    0,    0,    0,
    0,  809,  809,    0,  809,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  809,    0,    0,    0,  809,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  809,  809,    0, 4208,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3853,    0,
    0,  556,    0,    0,    0,  809,  809,  809,  593,    0,
    0,    0,    0,    0,    0,    0,    0, 6860,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4310,    0,    0,    0,    0,  809,
    0,    0,    0,    0,    0, 1090, 1213, 1336, 1459, 1582,
 1705, 1828, 1951, 2074, 2197, 2320, 2443,    0,    0,    0,
    0,    0, 4412,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  649,  707,    0,    0,    0,    0,  731,
    0,  773, 1019,    0,    0,    0,    0,    0,  809,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4514, 4616,
 4718,    0,    0, 4820,    0,    0,    0,    0, 1099,    0,
 1128,    0,    0,    0,    0,  359,  809,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4922,    0,    0,    0,    0,
    0,    0,    0,    0, 5024,    0,    0,    0,    0,    0,
 5126, 5228, 5330,    0, 5432, 5534, 5636, 5738,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5840,    0,    0,    0,    0, 5942,
    0, 6044, 6146, 6248,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 6350,    0,    0,    0,    0,    0,    0,    0,    0,  809,
    0,    0,    0,    0,    0,  809,  809,  809,    0,    0,
    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  861,  788,    0,    0,    0,    0,  686,  680,  859,
  546,   -6, -101,   77, -294,   34,    0,  631,  630, -178,
 -516,    0,  366,    0, -677,    0,  -48,  667,  794,  103,
    0,  685,    0,  -74,    0,  549, -102, -222,   -2,    0,
  -59,  841,  -50, -124,    0,  673, -128,    0,    0,    0,
  317, -757,  256,    0, -406, -538,   53,  165,  166, -325,
    0,  571,  572,  511,  300, 2366,    0,  138,    0,  397,
    0,  229,    0,  149,  -89, -220,    0,    0,    0,  530,
  };
  protected static readonly short [] yyTable = {            58,
   58,  106,  615,   60,  108,   88,   99,  267,  126,  130,
  286,  161,  820,  100,  235,  496,  350,  115,  183,  120,
  505,  101,  101,  101,  101,   96,  116,  121,  773,  117,
  663,  134,  240,  100,  149,   54,   94,  189,  267,   16,
   92,   94,  190,   68,  845,   93,   92,  138,   70,  499,
  100,  500,   83,   58,   58,  134,  233,   58,  362,  237,
   58,  242,   60,  362,  111,   60,   55,   19,  158,  237,
  272,   53,  212,  399,  120,  217,  100,   20,  158,  376,
  376,  383,  203,  254,  274,  256,  240,  276,  266,   92,
   17,   18,  279,  400,  142,  230,  773,   58,   53,   58,
   34,   58,  393,  111,  240,  111,  155,  364,   59,   65,
  163,  278,  133,  119,  185,  122,  187,  124,  506,  386,
  746,   92,  620,  750,  238,  586,   93,  205,  206,  503,
  208,  249,  249,  249,  285,  894,  207,  104,  277,  191,
   94,  101,  356,   21,  361,   60,  203,  159,  210,  240,
   71,  211,  390,  367,  362,  369,  118,  243,  244,   95,
   72,  181,  102,  105,   38,  234,  107,  349,  115,  110,
  120,  231,  363,  524,  232,   19,  496,  116,  121,  255,
  117,  257,  261,   74,   75,  262,   73,  137,  351,  537,
   50,   51,  182,  537,   85,  520,  537,  358,  359,  360,
  385,   89,  599,  262,  142,  537,  145,  414,  146,  417,
  148,  100,  705,  565,  158,   94,  426,  522,  389,  609,
  523,   90,   94,  111,  180,  155,  111,  111,   76,   77,
  618,  100,  404,  567,   94,  149,  125,  265,  149,  196,
  398,  405,  402,  100,  406,  571,   86,   97,   23,   87,
  129,  374,  374,  109,  147,  150,  213,   24,   25,   26,
   27,   28,  100,  434,  435,  436,  437,  438,  265,  100,
  443,   74,   75,  448,  449,  450,  451,  452,  453,  454,
  455,  456,  457,  458,  459,   58,  597,  492,  409,  412,
  413,  415,  416,  418,  419,  421,  422,  423,  424,  425,
  428,  430,  431,  432,  433,  782,  704,  694,  858,  523,
  439,  441,  101,  513,  447,  700,  100,  702,  605,  166,
  167,  168,  112,  169,  170,  171,  113,  172,  142,  488,
  489,   58,  115,  494,  780,  491,  855,  781,  392,  847,
  393,  174,  376,  872,  393,  534,  523,  697,  100,  175,
  509,  144,   74,   75,  116,  803,   76,   77,  166,  167,
  168,  128,  169,  170,  171,  821,  172,  895,   34,   34,
  847,  131,   34,  268,  173,   34,  837,  838,  150,  132,
  174,  150,   79,   55,  653,  100,   55,  659,  175,   34,
   34,  698,  100,  568,  403,  135,  598,   58,   58,  257,
  769,  111,  257,  536,   18,  699,  100,  540,  771,  100,
  543,  664,  100,  546,  885,  856,  890,  100,  137,  552,
  553,   38,  555,   34,  891,  100,   74,   75,  139,  863,
   76,   77,  866,  566,  892,  100,  251,  252,  151,  570,
  490,   96,  596,   19,   19,  194,  195,   19,   19,  204,
   19,  158,  209,  245,  246,  137,  137,  877,  878,  137,
  137,  247,  137,  584,   19,   19,    1,    2,  248,  253,
    3,    4,  884,    5,  258,  259,  137,  137,  176,  280,
  585,  260,  273,  281,   58,   58,   58,    6,    7,  593,
  595,  287,  177,  178,  179,  352,  143,  353,   19,  354,
  357,  365,  366,  606,  607,  608,  532,  533,  368,  391,
  137,  650,  651,  652,  374,  656,  658,  271,  397,  396,
  103,    8,  665,  687,  444,  493,  234,   58,   26,  237,
  501,  111,  504,  507,   39,   40,  692,   41,   42,  508,
  510,   44,  511,   45,   46,   47,   48,   49,  512,  693,
  515,  514,  516,  518,  519,   31,  696,  517,  521,   94,
  660,  527,  660,  653,  653,  666,  667,  668,  669,  670,
  671,  672,  673,  674,  675,  676,  677,  529,  560,   94,
  759,  561,  562,  563,  564,  569,   58,  349,   58,   58,
  572,  573,   21,  591,  592,  594,  142,  142,  832,  574,
  142,  142,  602,  142,  575,   52,  601,  576,  577,  578,
   94,  579,  580,  603,  709,  752,  581,  142,  142,  144,
  144,  582,  583,  144,  144,  117,  144,  121,  123,  604,
  240,  610,  611,  758,  612,  613,  625,  619,  399,  621,
  144,  144,   94,  695,  626,  628,  393,  629,   32,  778,
  631,  142,  660,  632,  634,  660,  635,  811,  812,  637,
  638,  815,  816,  164,  165,  639,  640,  186,  641,  188,
  644,  646,   18,   18,  144,  647,   18,   18,  648,   18,
  649,  662,  240,  240,  681,   58,  240,  240,  682,  111,
  701,  793,  794,   18,   18,  686,  703,  690,  691,  374,
  777,  717,  706,  712,  720,  723,   29,  726,  732,  733,
  850,  735,  740,  741,  852,  853,  528,  742,  743,  744,
  745,  240,  749,  240,  240,  535,  751,   18,  538,  539,
   24,  541,  542,  753,  544,  545,  754,  547,  548,  549,
  550,  551,  240,  755,  554,  756,  556,  557,  558,  559,
  586,  871,  763,  770,  846,  772,  797,  783,  798,  784,
  799,  849,  800,  801,  143,  143,  802,  374,  143,  143,
  810,  143,   22,  814,  709,  111,   94,   94,   94,  822,
   94,   94,   94,  825,   94,  143,  143,  836,  839,  590,
  841,   94,   94,  840,  768,  847,   26,   26,   94,  842,
   26,   26,  843,   26,  844,  589,   94,  851,  848,  859,
  860,  874,  875,  876,  861,  862,  864,   26,   26,  143,
  865,  867,  868,   31,   31,  873,  870,   31,   31,  879,
   31,  623,  624,  166,  167,  168,  889,  169,  170,  171,
  881,  172,  882,  104,   31,   31,  893,    1,   94,  883,
  124,   26,  627,  125,  126,  174,  630,  127,  128,  633,
   21,   21,  636,  175,   21,   21,  880,   21,  642,  643,
   69,  645,  127,  263,  886,  887,  888,   80,   31,  282,
  264,   21,   21,  104,  104,  104,  616,  104,  382,  136,
  684,  685,  384,  688,  283,  275,   94,   98,  502,  869,
  678,  679,  680,  104,  284,  104,  795,  525,  796,  757,
   94,   94,   94,  497,  498,   21,   32,   32,  835,  617,
   32,   32,  824,   32,  715,    0,  526,    0,    0,    0,
    0,    0,    0,    0,  104,    0,  104,   32,   32,    0,
    0,   94,  716,    0,  718,  719,    0,  721,  722,    0,
  724,  725,    0,  727,  728,  729,  730,  731,    0,    0,
  734,    0,  736,  737,  738,  739,  104,    0,  104,    0,
    0,   32,    0,    0,   29,   29,  747,    0,   29,   29,
    0,   29,    0,    0,    0,  762,    0,    0,    0,  766,
  767,    0,    0,    0,    0,   29,   29,    0,   24,   24,
    0,    0,   24,   24,    0,   24,   95,  224,    0,    0,
  224,    0,    0,    0,  218,    0,    0,    0,   25,   24,
   24,    0,    0,    0,    0,  779,    0,  100,  224,   29,
  219,    0,    0,  786,    0,    0,  787,    0,    0,  788,
   22,   22,  789,    0,   22,   22,    0,   22,  790,  791,
    0,  792,    0,   24,    0,    0,  101,    0,    0,  224,
    0,   22,   22,    0,    0,    0,    0,  819,    0,    0,
  807,  808,  809,  220,  221,    0,  181,    0,  222,  223,
    0,  224,  225,  226,  227,    0,    0,    0,    0,  224,
    0,  224,    0,    0,  834,   22,    0,    0,   30,    0,
  104,  104,  104,    0,  104,  104,  104,  182,  104,    0,
    0,  104,  104,    0,    0,  104,  104,  104,  104,    0,
    0,    0,  104,    0,    0,    0,    0,   27,    0,   94,
  104,    0,  104,  104,    0,    0,  104,    0,    0,  180,
  460,  461,  462,  463,  464,  465,  466,  467,  468,  469,
  104,  104,    0,  104,  104,    0,   54,  104,  104,  104,
  104,  104,  104,  104,    0,    0,  104,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  104,  104,  104,
    0,  104,  104,    0,    0,    0,  104,   55,  104,    0,
    0,  104,  104,  104,  104,  104,  104,  104,  104,  104,
  104,    0,  104,  104,    0,  104,  104,  104,  104,  104,
  104,  104,  104,  104,  104,  104,  104,  104,    0,   53,
  104,    0,    0,    0,  104,  104,  104,  104,  104,    0,
  104,  104,  104,  104,  104,  104,  104,    0,  104,  224,
  224,    0,  166,  167,  168,    0,  169,  170,  171,  104,
  172,    0,   94,    0,    0,    0,    0,    0,    0,    0,
  104,  104,  104,  104,  174,  104,    0,  104,  104,    0,
    0,    0,  175,  166,  167,  168,    0,  169,  170,  171,
    0,  172,    0,    0,    0,    0,   25,   25,  268,  173,
   25,   25,    0,   25,    0,  174,    0,    0,    0,  100,
  224,  224,  224,  175,  224,  224,    0,   25,   25,  224,
    0,  224,    0,    0,  224,  224,  224,  224,  224,  224,
  224,  224,  224,  224,    0,  224,  224,    0,  224,  224,
  224,  224,  224,  224,  224,  224,  224,  224,  224,  224,
  224,   25,    0,  224,    0,    0,    0,  224,  224,  224,
  224,  224,  224,  224,  224,  224,  224,  224,  224,  224,
   22,  224,  363,  363,    0,    0,   30,   30,    0,   23,
   30,   30,  224,   30,    0,   94,    0,    0,   24,   25,
   26,   27,   28,  224,  224,  224,  224,   30,   30,    0,
  224,    0,    0,  176,    0,   27,   27,    0,    0,   27,
   27,    0,   27,    0,    0,    0,    0,  177,  178,  179,
    0,    0,    0,    0,    0,    0,   27,   27,    0,    0,
    0,   30,    0,  363,  363,  363,    0,  363,  363,    0,
    0,    0,  363,   54,  363,    0,    0,  363,  363,  363,
  363,  363,  363,  363,  363,  363,  363,    0,  363,  363,
   27,  363,  363,  363,  363,  363,  363,  363,  363,  363,
  363,  363,  363,  363,   55,    0,  363,    0,    0,    0,
  363,  363,  363,  363,  363,  446,  363,  363,  363,  363,
  363,  363,  363,    0,  363,  368,  368,    0,    0,    0,
    0,    0,    0,    0,    0,  363,   53,    0,   94,    0,
    0,    0,    0,    0,    0,    0,  363,  363,  363,  363,
    0,    0,    0,  363,  166,  167,  168,    0,  169,  170,
  171,    0,  172,    0,    0,    0,    0,    0,    0,  826,
  827,    0,    0,    0,    0,    0,  174,    0,    0,    0,
    0,    0,    0,    0,  175,    0,  368,  368,  368,    0,
  368,  368,    0,    0,    0,  368,   54,  368,    0,    0,
  368,  368,  368,  368,  368,  368,  368,  368,  368,  368,
    0,  368,  368,    0,  368,  368,  368,  368,  368,  368,
  368,  368,  368,  368,  368,  368,  368,   55,    0,  368,
    0,    0,    0,  368,  368,  368,  368,  368,    0,  368,
  368,  368,  368,  368,  368,  368,    0,  368,  353,  353,
    0,    0,    0,    0,    0,    0,    0,    0,  368,   53,
    0,   94,    0,    0,    0,    0,    0,    0,    0,  368,
  368,  368,  368,    0,  828,    0,  368,  196,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    0,  829,  830,
  831,    0,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  353,
  353,  353,    0,  353,  353,    0,    0,    0,  353,   54,
  353,    0,    0,  353,  353,  353,  353,  353,  353,  353,
  353,  353,  353,    0,  353,  353,    0,  353,  353,  353,
  353,  353,  353,  353,  353,  353,  353,  353,  353,  353,
   55,    0,  353,    0,    0,    0,  353,  353,  353,  353,
  353,    0,  353,  353,  353,  353,  353,  353,  353,    0,
  353,  331,  331,    0,    0,    0,    0,    0,    0,    0,
    0,  353,   53,    0,   94,    0,    0,    0,    0,  103,
    0,  657,  353,  353,  353,  353,    0,    0,    0,  353,
   22,    0,    0,   39,   40,    0,   41,   42,    0,   23,
   44,    0,   45,   46,   47,   48,   49,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  331,  331,  331,    0,  331,  331,    0,    0,
    0,  331,   54,  331,    0,    0,  331,  331,  331,  331,
  331,  331,  331,  331,  331,  331,    0,  331,  331,    0,
  331,  331,  331,  331,  331,  331,  331,  331,  331,  331,
  331,  331,  331,   55,   52,  331,    0,    0,    0,  331,
  331,  331,  331,  331,    0,  331,  331,  331,  331,  331,
  331,  331,    0,  331,  328,  328,    0,  420,    0,    0,
    0,    0,    0,    0,  331,   53,    0,   94,    0,    0,
    0,    0,    0,    0,    0,  331,  331,  331,  331,    0,
    0,    0,  331,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,  154,    0,    0,    0,
    0,    0,    0,    0,    0,  328,  328,  328,    0,  328,
  328,    0,    0,    0,  328,   54,  328,    0,    0,  328,
  328,  328,  328,  328,  328,  328,  328,  328,  328,    0,
  328,  328,    0,  328,  328,  328,  328,  328,  328,  328,
  328,  328,  328,  328,  328,  328,   55,    0,  328,    0,
    0,    0,  328,  328,  328,  328,  328,    0,  328,  328,
  328,  328,  328,  328,  328,    0,  328,  329,  329,    0,
  427,    0,    0,    0,    0,    0,    0,  328,   53,    0,
   94,    0,    0,    0,    0,    0,    0,    0,  328,  328,
  328,  328,    0,    0,    0,  328,   22,    0,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,    0,   24,   25,   26,   27,   28,  141,
    0,    0,    0,    0,    0,    0,    0,    0,  329,  329,
  329,    0,  329,  329,    0,    0,    0,  329,   54,  329,
    0,    0,  329,  329,  329,  329,  329,  329,  329,  329,
  329,  329,    0,  329,  329,    0,  329,  329,  329,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  329,   55,
    0,  329,    0,    0,    0,  329,  329,  329,  329,  329,
    0,  329,  329,  329,  329,  329,  329,  329,    0,  329,
  330,  330,    0,  429,    0,    0,    0,    0,    0,    0,
  329,   53,    0,   94,    0,    0,    0,    0,    0,    0,
    0,  329,  329,  329,  329,    0,    0,    0,  329,   22,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   54,    0,    0,  152,    0,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,  614,    0,    0,    0,  153,
    0,  330,  330,  330,  857,  330,  330,    0,    0,    0,
  330,   55,  330,    0,    0,  330,  330,  330,  330,  330,
  330,  330,  330,  330,  330,    0,  330,  330,    0,  330,
  330,  330,  330,  330,  330,  330,  330,  330,  330,  330,
  330,  330,    0,   53,  330,    0,    0,    0,  330,  330,
  330,  330,  330,    0,  330,  330,  330,  330,  330,  330,
  330,    0,  330,  365,  365,    0,    0,    0,    0,    0,
    0,    0,    0,  330,    0,    0,   94,    0,    0,    0,
    0,    0,    0,    0,  330,  330,  330,  330,    0,    0,
    0,  330,   22,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,   54,    0,    0,    0,    0,    0,    0,
   24,   25,   26,   27,   28,    0,    0,    0,  708,    0,
    0,    0,  140,    0,  365,  365,  365,    0,  365,  365,
    0,    0,    0,  365,   55,  365,    0,   54,  365,  365,
  365,  365,  365,  365,  365,  365,  365,  365,    0,  365,
  365,    0,  365,  365,  365,  365,  365,  365,  365,  365,
  365,  365,  365,  365,  365,    0,   53,  365,   55,    0,
    0,  365,  365,  365,  365,  365,    0,  365,  365,  365,
  365,  365,  365,  365,   22,  365,  357,  357,    0,    0,
    0,    0,    0,   23,    0,    0,  365,    0,    0,   94,
   53,    0,   24,   25,   26,   27,   28,  365,  365,  365,
  365,  166,  167,  168,  365,  169,  170,  171,    0,  172,
  166,  167,  168,    0,  169,  170,  171,    0,  172,  440,
  392,    0,    0,  174,    0,    0,    0,    0,    0,  392,
    0,  175,  174,    0,    0,    0,    0,  357,  357,  357,
  175,  357,  357,    0,    0,    0,  357,   54,  357,    0,
    0,  357,  357,  357,  357,  357,  357,  357,  357,  357,
  357,    0,  357,  357,    0,  357,  357,  357,  357,  357,
  357,  357,  357,  357,  357,  357,  357,  357,   55,    0,
  357,    0,    0,    0,  357,  357,  357,  357,  357,    0,
  357,  357,  357,  357,  357,  357,  357,  196,  357,  349,
  349,    0,    0,    0,    0,    0,   23,    0,    0,  357,
   53,    0,   94,    0,    0,   24,   25,   26,   27,   28,
  357,  357,  357,  357,    0,    0,    0,  357,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,  442,    0,    0,  707,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,    0,
  349,  349,  349,    0,  349,  349,    0,    0,    0,  349,
   54,  349,    0,    0,  349,  349,  349,  349,  349,  349,
  349,  349,  349,  349,    0,  349,  349,    0,  349,  349,
  349,  349,  349,  349,  349,  349,  349,  349,  349,  349,
  349,   55,    0,  349,    0,    0,    0,  349,  349,  349,
  349,  349,    0,  349,  349,  349,  349,  349,  349,  349,
    0,  349,  338,  338,    0,    0,    0,    0,    0,    0,
    0,    0,  349,   53,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  349,  349,  349,  349,    0,    0,    0,
  349,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,   54,    0,    0,  152,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,  100,    0,
    0,  153,    0,  338,  338,  338,    0,  338,  338,    0,
    0,    0,  338,   55,  338,    0,  181,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,    0,  338,  338,
    0,  338,  338,  338,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,    0,   53,  338,  182,    0,    0,
  338,  338,  338,  338,  338,    0,  338,  338,  338,  338,
  338,  338,  338,    0,  338,  303,  303,    0,    0,    0,
    0,    0,  100,    0,  241,  338,    0,    0,    0,  180,
    0,    0,    0,    0,    0,    0,  338,  338,  338,  338,
  181,    0,    0,  338,   22,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,  182,    0,    0,  140,    0,  303,  303,  303,    0,
  303,  303,    0,    0,    0,  303,    0,  303,    0,    0,
  303,  303,  303,  303,  303,  303,  303,  303,  303,  303,
  100,  303,  303,  180,  303,  303,  303,  303,  303,  303,
  303,  303,  303,  303,  303,  303,  303,    0,  181,  303,
    0,    0,    0,  303,  303,  303,  303,  303,    0,  303,
  303,  303,  303,  303,  303,  303,   22,  303,    0,    0,
    0,    0,    0,   54,    0,   23,  776,    0,  303,  182,
    0,    0,    0,    0,   24,   25,   26,   27,   28,  303,
  303,  303,  303,  166,  167,  168,  303,  169,  170,  171,
  100,  172,  355,    0,   55,    0,    0,    0,  268,  173,
    0,  180,    0,  100,    0,  174,    0,    0,  181,    0,
    0,    0,    0,  175,    0,    0,    0,    0,    0,  103,
    0,  181,    0,    0,    0,    0,   53,    0,    0,    0,
    0,    0,    0,   39,   40,    0,   41,   42,    0,  182,
   44,    0,   45,   46,   47,   48,   49,    0,    0,    0,
    0,    0,  182,    0,    0,    0,    0,  166,  167,  168,
  181,  169,  170,  171,    0,  172,    0,    0,    0,    0,
    0,  180,    0,  173,    0,  683,    0,    0,    0,  174,
    0,   85,    0,    0,  180,    0,    0,  175,    0,    0,
    0,  182,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  176,   52,    0,    0,    0,  713,  714,
    0,    0,    0,    0,    0,    0,    0,  177,  178,  179,
    0,    0,    0,  180,    0,    0,    0,    0,    0,    0,
    0,  181,    0,    0,    0,  166,  167,  168,    0,  169,
  170,  171,    0,  172,    0,    0,    0,    0,    0,    0,
  268,  173,    0,    0,    0,    0,    0,  174,    0,    0,
    0,    0,  182,    0,    0,  175,    0,   22,    0,  760,
  761,   86,    0,  764,  765,    0,   23,  176,    0,    0,
    0,   54,    0,    0,    0,   24,   25,   26,   27,   28,
    0,  177,  178,  179,  180,    0,    0,    0,    0,    0,
  785,    0,    0,    0,    0,  166,  167,  168,    0,  169,
  170,  171,   55,  172,    0,    0,   85,    0,  166,  167,
  168,  173,  169,  170,  171,    0,  172,  174,    0,    0,
    0,    0,   85,    0,  173,  175,    0,    0,    0,    0,
  174,    0,    0,    0,   53,    0,   54,  813,  175,  407,
  408,  817,  818,    0,    0,  176,    0,  166,  167,  168,
    0,  169,  170,  171,    0,  172,    0,    0,    0,  177,
  178,  179,  268,  173,    0,   85,   85,   55,    0,  174,
   85,   85,    0,   85,   85,   85,   85,  175,    0,    0,
    0,    0,    0,  103,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  854,    0,   86,   39,   40,   53,
   41,   42,    0,    0,   44,    0,   45,   46,   47,   48,
   49,    0,   86,    0,   54,  176,    0,    0,  166,  167,
  168,    0,  169,  170,  171,    0,  172,    0,  176,  177,
  178,  179,    0,  268,  173,    0,    0,    0,    0,    0,
  174,    0,  177,  178,  179,   55,    0,    0,  175,    0,
    0,    0,    0,    0,    0,   86,   86,    0,    0,    0,
   86,   86,    0,   86,   86,   86,   86,  176,   52,    0,
    0,   54,    0,    0,    0,   22,   54,   53,    0,    0,
    0,  177,  178,  179,   23,    0,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
    0,  132,   55,    0,   29,    0,    0,   55,    0,   30,
   31,   32,   33,   34,   35,   36,   37,   38,   39,   40,
    0,   41,   42,   43,    0,   44,    0,   45,   46,   47,
   48,   49,  132,    0,   53,    0,  199,    0,  176,   53,
   22,  133,    0,   50,   51,    0,    0,    0,    0,   23,
    0,    0,  177,  178,  179,    0,    0,    0,   24,   25,
   26,   27,   28,    0,  132,    0,    0,    0,    0,  103,
    0,    0,  133,    0,    0,    0,    0,    0,    0,    0,
    0,   54,    0,   39,   40,    0,   41,   42,    0,   52,
   44,    0,   45,   46,   47,   48,   49,   79,    0,    0,
    0,    0,    0,    0,  133,    0,    0,    0,    0,    0,
    0,    0,   55,    0,    0,    0,    0,    0,   22,    0,
   54,    0,    0,    0,    0,    0,    0,   23,    0,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,   53,    0,    0,  103,    0,    0,
    0,   55,    0,    0,   52,    0,    0,    0,    0,    0,
   38,   39,   40,    0,   41,   42,    0,    0,   44,    0,
   45,   46,   47,   48,   49,  196,  197,  495,    0,    0,
  196,  197,    0,   53,   23,  198,    0,    0,    0,   23,
  198,    0,    0,   24,   25,   26,   27,   28,   24,   25,
   26,   27,   28,    0,    0,  132,    0,    0,    0,    0,
    0,    0,    0,  218,  132,    0,    0,    0,    0,    0,
    0,    0,    0,  132,  132,  132,  132,  132,    0,  219,
    0,    0,   52,    0,  132,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  133,    0,    0,  132,  132,
    0,  132,  132,    0,  133,  132,    0,  132,  132,  132,
  132,  132,  132,  133,  133,  133,  133,  133,    0,    0,
    0,    0,  220,  221,  133,    0,    0,  222,  223,  600,
  224,  225,  226,  227,    0,   22,    0,    0,  133,  133,
    0,  133,  133,    0,   23,  133,    0,  133,  133,  133,
  133,  133,  133,   24,   25,   26,   27,   28,    0,    0,
    0,    0,    0,    0,   64,    0,    0,    0,    0,  132,
    0,    0,    0,    0,   22,  288,    0,    0,   39,   40,
    0,   41,   42,   23,    0,   44,    0,   45,   46,   47,
   48,   49,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,    0,  103,    0,    0,    0,    0,    0,  133,
    0,    0,    0,    0,    0,    0,    0,   39,   40,    0,
   41,   42,    0,    0,   44,    0,   45,   46,   47,   48,
   49,  240,    0,    0,    0,    0,  289,  290,  291,    0,
  292,  293,    0,    0,    0,  294,    0,  295,    0,   52,
  296,  297,  298,  299,  300,  301,  302,  303,  304,  305,
    0,  306,  307,    0,  308,  309,  310,  311,  312,  313,
  314,  315,  316,  317,  318,  319,  320,  288,    0,  321,
    0,    0,    0,  322,  323,  324,  325,  326,   52,  327,
  328,  329,  330,  331,  332,  333,    0,  334,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  335,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  336,
  337,  338,  339,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,  238,    0,    0,    0,    0,  289,  290,
  291,    0,  292,  293,    0,    0,    0,  294,    0,  295,
    0,    0,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,    0,  306,  307,    0,  308,  309,  310,  311,
  312,  313,  314,  315,  316,  317,  318,  319,  320,  240,
    0,  321,    0,    0,    0,  322,  323,  324,  325,  326,
    0,  327,  328,  329,  330,  331,  332,  333,    0,  334,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  335,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  336,  337,  338,  339,    0,    0,    0,  340,    0,
    0,    0,    0,    0,    0,  241,    0,    0,    0,    0,
  240,  240,  240,    0,  240,  240,    0,    0,    0,  240,
    0,  240,    0,    0,  240,  240,  240,  240,  240,  240,
  240,  240,  240,  240,    0,  240,  240,    0,  240,  240,
  240,  240,  240,  240,  240,  240,  240,  240,  240,  240,
  240,  238,    0,  240,    0,    0,    0,  240,  240,  240,
  240,  240,    0,  240,  240,  240,  240,  240,  240,  240,
    0,  240,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  240,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  240,  240,  240,  240,    0,    0,    0,
  240,    0,    0,    0,    0,    0,    0,  239,    0,    0,
    0,    0,  238,  238,  238,    0,  238,  238,    0,    0,
    0,  238,    0,  238,    0,    0,  238,  238,  238,  238,
  238,  238,  238,  238,  238,  238,    0,  238,  238,    0,
  238,  238,  238,  238,  238,  238,  238,  238,  238,  238,
  238,  238,  238,  241,    0,  238,    0,    0,    0,  238,
  238,  238,  238,  238,    0,  238,  238,  238,  238,  238,
  238,  238,    0,  238,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  238,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  238,  238,  238,  238,    0,
    0,    0,  238,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  241,  241,  241,    0,  241,  241,
    0,    0,    0,  241,    0,  241,    0,   54,  241,  241,
  241,  241,  241,  241,  241,  241,  241,  241,    0,  241,
  241,    0,  241,  241,  241,  241,  241,  241,  241,  241,
  241,  241,  241,  241,  241,  239,    0,  241,   55,    0,
   54,  241,  241,  241,  241,  241,    0,  241,  241,  241,
  241,  241,  241,  241,    0,  241,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  241,    0,    0,    0,
   53,   55,    0,    0,    0,   54,    0,  241,  241,  241,
  241,    0,    0,    0,  241,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  239,  239,  239,    0,
  239,  239,    0,   53,    0,  239,   55,  239,    0,   54,
  239,  239,  239,  239,  239,  239,  239,  239,  239,  239,
    0,  239,  239,    0,  239,  239,  239,  239,  239,  239,
  239,  239,  239,  239,  239,  239,  239,    0,   53,  239,
   55,    0,    0,  239,  239,  239,  239,  239,    0,  239,
  239,  239,  239,  239,  239,  239,   54,  239,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  239,    0,
    0,    0,   53,    0,   91,    0,    0,    0,    0,  239,
  239,  239,  239,   54,    0,    0,  239,   55,    0,  823,
  471,  472,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  166,  167,  168,  370,  169,  170,
  171,   22,  371,    0,   55,    0,    0,    0,   54,   53,
   23,  372,    0,  373,   54,    0,  174,    0,    0,   24,
   25,   26,   27,   28,  175,    0,    0,  166,  167,  168,
  370,  169,  170,  171,   22,  371,   53,    0,    0,   55,
    0,    0,    0,   23,  381,   55,  373,    0,    0,  174,
    0,    0,   24,   25,   26,   27,   28,  175,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  196,
  387,   53,    0,    0,    0,    0,    0,   53,   23,  388,
    0,    0,    0,    0,    0,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,  473,  474,  475,
  476,   24,   25,   26,   27,   28,  477,  478,  479,  480,
  481,  482,  483,  484,  485,  486,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,   22,    0,    0,
  348,  348,    0,    0,    0,    0,   23,    0,    0,    0,
    0,  707,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,    0,    0,  196,    0,
    0,   23,    0,    0,    0,    0,    0,   23,    0,    0,
   24,   25,   26,   27,   28,    0,   24,   25,   26,   27,
   28,  348,  348,  348,    0,  348,  348,    0,    0,    0,
  348,    0,  348,    0,    0,  348,  348,  348,  348,  348,
  348,  348,  348,  348,  348,    0,  348,  348,    0,  348,
  348,  348,  348,  348,  348,  348,  348,  348,  348,  348,
  348,  348,  322,  322,  348,    0,    0,    0,  348,  348,
  348,  348,  348,    0,  348,  348,  348,  348,  348,  348,
  348,    0,  348,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  348,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  348,  348,  348,  348,    0,    0,
    0,  348,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,  322,  322,    0,  322,  322,    0,
    0,    0,  322,    0,  322,    0,    0,  322,  322,  322,
  322,  322,  322,  322,  322,  322,  322,    0,  322,  322,
    0,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  322,  322,  304,  304,  322,    0,    0,    0,
  322,  322,  322,  322,  322,    0,  322,  322,  322,  322,
  322,  322,  322,    0,  322,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  322,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  322,  322,  322,  322,
    0,    0,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,  304,  304,    0,  304,
  304,    0,    0,    0,  304,    0,  304,    0,    0,  304,
  304,  304,  304,  304,  304,  304,  304,  304,  304,    0,
  304,  304,    0,  304,  304,  304,  304,  304,  304,  304,
  304,  304,  304,  304,  304,  304,  309,  309,  304,    0,
    0,    0,  304,  304,  304,  304,  304,    0,  304,  304,
  304,  304,  304,  304,  304,    0,  304,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  304,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  304,  304,
  304,  304,    0,    0,    0,  304,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,  309,  309,
    0,  309,  309,    0,    0,    0,  309,    0,  309,    0,
    0,  309,  309,  309,  309,  309,  309,  309,  309,  309,
  309,    0,  309,  309,    0,  309,  309,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,  309,  310,  310,
  309,    0,    0,    0,  309,  309,  309,  309,  309,    0,
  309,  309,  309,  309,  309,  309,  309,    0,  309,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  309,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  309,  309,  309,  309,    0,    0,    0,  309,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  310,
  310,  310,    0,  310,  310,    0,    0,    0,  310,    0,
  310,    0,    0,  310,  310,  310,  310,  310,  310,  310,
  310,  310,  310,    0,  310,  310,    0,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,  310,  310,  310,
  305,  305,  310,    0,    0,    0,  310,  310,  310,  310,
  310,    0,  310,  310,  310,  310,  310,  310,  310,    0,
  310,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  310,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  310,  310,  310,  310,    0,    0,    0,  310,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  305,  305,  305,    0,  305,  305,    0,    0,    0,
  305,    0,  305,    0,    0,  305,  305,  305,  305,  305,
  305,  305,  305,  305,  305,    0,  305,  305,    0,  305,
  305,  305,  305,  305,  305,  305,  305,  305,  305,  305,
  305,  305,  315,  315,  305,    0,    0,    0,  305,  305,
  305,  305,  305,    0,  305,  305,  305,  305,  305,  305,
  305,    0,  305,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  305,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  305,  305,  305,  305,    0,    0,
    0,  305,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  315,  315,  315,    0,  315,  315,    0,
    0,    0,  315,    0,  315,    0,    0,  315,  315,  315,
  315,  315,  315,  315,  315,  315,  315,    0,  315,  315,
    0,  315,  315,  315,  315,  315,  315,  315,  315,  315,
  315,  315,  315,  315,  337,  337,  315,    0,    0,    0,
  315,  315,  315,  315,  315,    0,  315,  315,  315,  315,
  315,  315,  315,    0,  315,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  315,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  315,  315,  315,  315,
    0,    0,    0,  315,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  337,  337,  337,    0,  337,
  337,    0,    0,    0,  337,    0,  337,    0,    0,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  337,    0,
  337,  337,    0,  337,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  333,  333,  337,    0,
    0,    0,  337,  337,  337,  337,  337,    0,  337,  337,
  337,  337,  337,  337,  337,    0,  337,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  337,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  337,  337,
  337,  337,    0,    0,    0,  337,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  333,  333,  333,
    0,  333,  333,    0,    0,    0,  333,    0,  333,    0,
    0,  333,  333,  333,  333,  333,  333,  333,  333,  333,
  333,    0,  333,  333,    0,  333,  333,  333,  333,  333,
  333,  333,  333,  333,  333,  333,  333,  333,  311,  311,
  333,    0,    0,    0,  333,  333,  333,  333,  333,    0,
  333,  333,  333,  333,  333,  333,  333,    0,  333,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  333,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  333,  333,  333,  333,    0,    0,    0,  333,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  311,
  311,  311,    0,  311,  311,    0,    0,    0,  311,    0,
  311,    0,    0,  311,  311,  311,  311,  311,  311,  311,
  311,  311,  311,    0,  311,  311,    0,  311,  311,  311,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  306,  306,  311,    0,    0,    0,  311,  311,  311,  311,
  311,    0,  311,  311,  311,  311,  311,  311,  311,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  311,  311,  311,  311,    0,    0,    0,  311,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  306,  306,  306,    0,  306,  306,    0,    0,    0,
  306,    0,  306,    0,    0,  306,  306,  306,  306,  306,
  306,  306,  306,  306,  306,    0,  306,  306,    0,  306,
  306,  306,  306,  306,  306,  306,  306,  306,  306,  306,
  306,  306,  307,  307,  306,    0,    0,    0,  306,  306,
  306,  306,  306,    0,  306,  306,  306,  306,  306,  306,
  306,    0,  306,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  306,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  306,  306,  306,  306,    0,    0,
    0,  306,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  307,  307,  307,    0,  307,  307,    0,
    0,    0,  307,    0,  307,    0,    0,  307,  307,  307,
  307,  307,  307,  307,  307,  307,  307,    0,  307,  307,
    0,  307,  307,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  307,  307,  312,  312,  307,    0,    0,    0,
  307,  307,  307,  307,  307,    0,  307,  307,  307,  307,
  307,  307,  307,    0,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  307,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  307,  307,  307,  307,
    0,    0,    0,  307,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  312,  312,  312,    0,  312,
  312,    0,    0,    0,  312,    0,  312,    0,    0,  312,
  312,  312,  312,  312,  312,  312,  312,  312,  312,    0,
  312,  312,    0,  312,  312,  312,  312,  312,  312,  312,
  312,  312,  312,  312,  312,  312,  320,  320,  312,    0,
    0,    0,  312,  312,  312,  312,  312,    0,  312,  312,
  312,  312,  312,  312,  312,    0,  312,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  312,  312,
  312,  312,    0,    0,    0,  312,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  320,  320,  320,
    0,  320,  320,    0,    0,    0,  320,    0,  320,    0,
    0,  320,  320,  320,  320,  320,  320,  320,  320,  320,
  320,    0,  320,  320,    0,  320,  320,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,  320,  313,  313,
  320,    0,    0,    0,  320,  320,  320,  320,  320,    0,
  320,  320,  320,  320,  320,  320,  320,    0,  320,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  320,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  320,  320,  320,  320,    0,    0,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  313,
  313,  313,    0,  313,  313,    0,    0,    0,  313,    0,
  313,    0,    0,  313,  313,  313,  313,  313,  313,  313,
  313,  313,  313,    0,  313,  313,    0,  313,  313,  313,
  313,  313,  313,  313,  313,  313,  313,  313,  313,  313,
  316,  316,  313,    0,    0,    0,  313,  313,  313,  313,
  313,    0,  313,  313,  313,  313,  313,  313,  313,    0,
  313,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  313,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  313,  313,  313,  313,    0,    0,    0,  313,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  316,  316,  316,    0,  316,  316,    0,    0,    0,
  316,    0,  316,    0,    0,  316,  316,  316,  316,  316,
  316,  316,  316,  316,  316,    0,  316,  316,    0,  316,
  316,  316,  316,  316,  316,  316,  316,  316,  316,  316,
  316,  316,  334,  334,  316,    0,    0,    0,  316,  316,
  316,  316,  316,    0,  316,  316,  316,  316,  316,  316,
  316,    0,  316,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  316,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  316,  316,  316,  316,    0,    0,
    0,  316,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  334,  334,  334,    0,  334,  334,    0,
    0,    0,  334,    0,  334,    0,    0,  334,  334,  334,
  334,  334,  334,  334,  334,  334,  334,    0,  334,  334,
    0,  334,  334,  334,  334,  334,  334,  334,  334,  334,
  334,  334,  334,  334,  308,  308,  334,    0,    0,    0,
  334,  334,  334,  334,  334,    0,  334,  334,  334,  334,
  334,  334,  334,    0,  334,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  334,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  334,  334,  334,  334,
    0,    0,    0,  334,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  308,  308,  308,    0,  308,
  308,    0,    0,    0,  308,    0,  308,    0,    0,  308,
  308,  308,  308,  308,  308,  308,  308,  308,  308,    0,
  308,  308,    0,  308,  308,  308,  308,  308,  308,  308,
  308,  308,  308,  308,  308,  308,  314,  314,  308,    0,
    0,    0,  308,  308,  308,  308,  308,    0,  308,  308,
  308,  308,  308,  308,  308,    0,  308,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  308,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  308,  308,
  308,  308,    0,    0,    0,  308,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  314,  314,  314,
    0,  314,  314,    0,    0,    0,  314,    0,  314,    0,
    0,  314,  314,  314,  314,  314,  314,  314,  314,  314,
  314,    0,  314,  314,    0,  314,  314,  314,  314,  314,
  314,  314,  314,  314,  314,  314,  314,  314,  317,  317,
  314,    0,    0,    0,  314,  314,  314,  314,  314,    0,
  314,  314,  314,  314,  314,  314,  314,    0,  314,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  314,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  314,  314,  314,  314,    0,    0,    0,  314,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  317,
  317,  317,    0,  317,  317,    0,    0,    0,  317,    0,
  317,    0,    0,  317,  317,  317,  317,  317,  317,  317,
  317,  317,  317,    0,  317,  317,    0,  317,  317,  317,
  317,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  318,  318,  317,    0,    0,    0,  317,  317,  317,  317,
  317,    0,  317,  317,  317,  317,  317,  317,  317,    0,
  317,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  317,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  317,  317,  317,  317,    0,    0,    0,  317,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  318,  318,  318,    0,  318,  318,    0,    0,    0,
  318,    0,  318,    0,    0,  318,  318,  318,  318,  318,
  318,  318,  318,  318,  318,    0,  318,  318,    0,  318,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  318,
  318,  318,  319,  319,  318,    0,    0,    0,  318,  318,
  318,  318,  318,    0,  318,  318,  318,  318,  318,  318,
  318,    0,  318,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  318,  318,  318,  318,    0,    0,
    0,  318,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  319,  319,  319,    0,  319,  319,    0,
    0,    0,  319,    0,  319,    0,    0,  319,  319,  319,
  319,  319,  319,  319,  319,  319,  319,    0,  319,  319,
    0,  319,  319,  319,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  288,    0,  319,    0,    0,    0,
  319,  319,  319,  319,  319,    0,  319,  319,  319,  319,
  319,  319,  319,    0,  319,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  319,  319,  319,  319,
    0,    0,    0,  319,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  289,  290,  291,    0,  292,
  293,    0,    0,    0,  294,    0,  295,    0,    0,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,    0,
  306,  307,    0,  308,  309,  310,  311,  312,  313,  314,
  315,  316,  317,  318,  319,  320,  244,    0,  321,    0,
    0,    0,  322,  323,  324,  325,  326,    0,  327,  328,
  329,  330,  331,  332,  333,    0,  334,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  335,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  336,  337,
  338,  339,    0,    0,    0,  340,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  244,  244,  244,
    0,  244,  244,    0,    0,    0,  244,    0,  244,    0,
    0,  244,  244,  244,  244,  244,  244,  244,  244,  244,
  244,    0,  244,  244,    0,  244,  244,  244,  244,  244,
  244,  244,  244,  244,  244,  244,  244,  244,  245,    0,
  244,    0,    0,    0,  244,  244,  244,  244,  244,    0,
  244,  244,  244,  244,  244,  244,  244,    0,  244,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  244,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  244,  244,  244,  244,    0,    0,    0,  244,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  245,
  245,  245,    0,  245,  245,    0,    0,    0,  245,    0,
  245,    0,    0,  245,  245,  245,  245,  245,  245,  245,
  245,  245,  245,    0,  245,  245,    0,  245,  245,  245,
  245,  245,  245,  245,  245,  245,  245,  245,  245,  245,
  246,    0,  245,    0,    0,    0,  245,  245,  245,  245,
  245,    0,  245,  245,  245,  245,  245,  245,  245,    0,
  245,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  245,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  245,  245,  245,  245,    0,    0,    0,  245,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  246,  246,  246,    0,  246,  246,    0,    0,    0,
  246,    0,  246,    0,    0,  246,  246,  246,  246,  246,
  246,  246,  246,  246,  246,    0,  246,  246,    0,  246,
  246,  246,  246,  246,  246,  246,  246,  246,  246,  246,
  246,  246,  247,    0,  246,    0,    0,    0,  246,  246,
  246,  246,  246,    0,  246,  246,  246,  246,  246,  246,
  246,    0,  246,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  246,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  246,  246,  246,  246,    0,    0,
    0,  246,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  247,  247,  247,    0,  247,  247,    0,
    0,    0,  247,    0,  247,    0,    0,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,    0,  247,  247,
    0,  247,  247,  247,  247,  247,  247,  247,  247,  247,
  247,  247,  247,  247,    0,    0,  247,    0,    0,    0,
  247,  247,  247,  247,  247,    0,  247,  247,  247,  247,
  247,  247,  247,    0,  247,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  247,    0,    0,    0,    0,
    0,    0,    0,    0,  292,    0,  247,  247,  247,  247,
    0,  295,    0,  247,  296,  297,  298,  299,  300,  301,
  302,  303,  304,  305,    0,  306,  307,    0,  308,  309,
  310,  311,  312,  313,  314,  315,  316,  317,  318,  319,
  320,    0,    0,  321,    0,    0,    0,  322,  323,  324,
  325,  326,    0,  327,  328,  329,  330,  331,  332,  333,
    0,  334,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  335,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  336,  337,  338,  339,   74,   75,    0,
  340,   76,   77,   78,   30,   31,   32,   33,   34,   35,
   36,   37,    0,    0,    0,    0,    0,    0,   43,    0,
    0,    0,    0,    0,    0,    0,    0,   79,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   61,  519,    6,   64,   33,   57,   44,   60,   33,
  233,  114,  770,   42,  123,  341,  123,  123,  120,  123,
  280,   40,   40,   40,   40,  123,  123,  123,  706,  123,
  569,   44,  161,   42,  109,   60,   40,  276,   44,   61,
   44,   40,  281,  322,  802,   44,   53,   98,  272,  344,
   42,  346,   19,   60,   61,   44,  159,   64,   44,  313,
   67,  163,   41,   44,   67,   44,   91,   61,  322,  313,
   62,  123,  147,  266,   81,  150,   42,   61,  322,  258,
  259,  260,  131,  185,   93,  187,  215,  212,  125,   96,
  270,  271,  217,  286,  101,  155,  774,  104,  123,  106,
    0,  108,  281,  106,  233,  108,  113,   93,    6,    7,
  117,  214,  125,   80,  121,   82,  123,   84,  378,  125,
  659,  125,  529,  662,  378,   91,  125,  134,  135,  352,
  137,  180,  181,  182,  378,  893,  125,   61,  213,  378,
   40,   40,  244,   61,  125,  124,  195,  114,   41,  278,
  260,   44,  277,  255,   44,  257,   80,  164,  165,  257,
   61,   60,   60,   61,  306,  274,   64,  274,  274,   67,
  274,   41,   62,  396,   44,    0,  502,  274,  274,  186,
  274,  188,   41,  291,  292,   44,   61,    0,  237,  410,
  332,  333,   91,  414,  281,  374,  417,  246,  247,  248,
   41,   40,  497,   44,  211,  426,  104,  297,  106,  299,
  108,   42,  619,   44,  322,   40,  306,   41,  267,  514,
   44,  257,   40,   41,  123,  232,   44,  230,  295,  296,
  525,   42,  292,   44,   40,   41,  288,  274,   44,  264,
  289,  292,  291,   42,  293,   44,  274,  257,  273,  277,
  274,  258,  259,  272,  272,  272,  272,  282,  283,  284,
  285,  286,   42,  312,  313,  314,  315,  316,  274,   42,
  319,  291,  292,  322,  323,  324,  325,  326,  327,  328,
  329,  330,  331,  332,  333,  292,  315,  338,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  712,   41,  602,  825,   44,
  317,  318,   40,  362,  321,  610,   42,  612,   44,  257,
  258,  259,   61,  261,  262,  263,   40,  265,    0,  336,
  337,  338,  260,  340,   41,  338,   41,   44,  276,   44,
  519,  279,  521,   41,  523,  405,   44,   41,   42,  287,
  357,    0,  291,  292,  260,  380,  295,  296,  257,  258,
  259,   40,  261,  262,  263,  772,  265,   41,  268,  269,
   44,  123,  272,  272,  273,  275,  783,  784,   41,  257,
  279,   44,  321,   41,  563,   42,   44,   44,  287,  289,
  290,   41,   42,  442,  292,  267,  425,  404,  405,   41,
  695,  404,   44,  410,    0,   41,   42,  414,  703,   42,
  417,   44,   42,  420,   44,  822,   41,   42,  267,  426,
  427,  306,  429,  323,   41,   42,  291,  292,  378,  836,
  295,  296,  839,  440,   41,   42,  181,  182,  123,  446,
  338,  123,  493,  268,  269,   40,  123,  272,  273,   41,
  275,  322,  257,  379,   40,  268,  269,  864,  865,  272,
  273,   40,  275,  470,  289,  290,  268,  269,   40,   44,
  272,  273,  879,  275,   58,   58,  289,  290,  377,   61,
  487,   58,   62,   40,  491,  492,  493,  289,  290,  492,
  493,  274,  391,  392,  393,  257,    0,  378,  323,   44,
   40,  378,   44,  510,  511,  512,  404,  405,   44,  260,
  323,  560,  561,  562,  521,  564,  565,  201,   61,  257,
  293,  323,  571,  422,  331,  419,  274,  534,    0,  313,
  274,  534,  257,  378,  307,  308,  596,  310,  311,   44,
  386,  314,  386,  316,  317,  318,  319,  320,  386,  598,
  378,  257,   44,   44,   40,    0,  605,  378,  124,   40,
  567,  273,  569,  742,  743,  572,  573,  574,  575,  576,
  577,  578,  579,  580,  581,  582,  583,   44,   44,   60,
  683,   44,   44,   44,   44,   44,  593,  274,  595,  596,
  386,  386,    0,  491,  492,  493,  268,  269,  777,  386,
  272,  273,  257,  275,  386,  378,  260,  386,  386,  386,
   91,  386,  386,  257,  621,  664,  386,  289,  290,  268,
  269,  386,  386,  272,  273,   80,  275,   82,   83,  378,
  759,  257,  378,  682,  257,  378,  534,   44,  266,   40,
  289,  290,  123,  257,   44,   44,  825,   44,    0,  709,
   44,  323,  659,   44,   44,  662,   44,  760,  761,   44,
   44,  764,  765,  118,  119,   44,   44,  122,   44,  124,
   44,   44,  268,  269,  323,   44,  272,  273,   44,  275,
   44,   44,  811,  812,   44,  692,  815,  816,   44,  692,
  257,  740,  741,  289,  290,  593,  257,  595,  596,  706,
  707,   44,   91,  386,   44,   44,    0,   44,   44,   44,
  813,   44,   44,   44,  817,  818,  400,   44,   44,  257,
   44,  850,   44,  852,  853,  409,   44,  323,  412,  413,
    0,  415,  416,   44,  418,  419,   44,  421,  422,  423,
  424,  425,  871,   44,  428,   44,  430,  431,  432,  433,
   91,  854,  423,   44,  803,   44,  378,  386,   44,  386,
  378,  810,   44,  378,  268,  269,   44,  774,  272,  273,
   44,  275,    0,  260,  781,  778,  257,  258,  259,   44,
  261,  262,  263,   40,  265,  289,  290,  326,  386,  490,
  378,  272,  273,  257,  692,   44,  268,  269,  279,  257,
  272,  273,  378,  275,  257,  489,  287,   44,   93,  379,
   40,  860,  861,  862,   40,   40,  326,  289,  290,  323,
  326,  257,  257,  268,  269,   40,  260,  272,  273,  326,
  275,  532,  533,  257,  258,  259,  885,  261,  262,  263,
  386,  265,  386,    0,  289,  290,   44,    0,   40,  386,
  272,  323,  536,  272,  272,  279,  540,  272,  272,  543,
  268,  269,  546,  287,  272,  273,  873,  275,  552,  553,
   10,  555,   85,  194,  881,  882,  883,   19,  323,  125,
  195,  289,  290,   40,   41,   42,  521,   44,  259,   96,
  591,  592,  262,  594,  228,  211,  377,   57,  350,  847,
  584,  585,  586,   60,  232,   62,  742,  397,  743,  681,
  391,  392,  393,  343,  343,  323,  268,  269,  781,  523,
  272,  273,  774,  275,  625,   -1,  397,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   93,  289,  290,   -1,
   -1,  422,  626,   -1,  628,  629,   -1,  631,  632,   -1,
  634,  635,   -1,  637,  638,  639,  640,  641,   -1,   -1,
  644,   -1,  646,  647,  648,  649,  123,   -1,  125,   -1,
   -1,  323,   -1,   -1,  268,  269,  660,   -1,  272,  273,
   -1,  275,   -1,   -1,   -1,  686,   -1,   -1,   -1,  690,
  691,   -1,   -1,   -1,   -1,  289,  290,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,   40,   41,   -1,   -1,
   44,   -1,   -1,   -1,  260,   -1,   -1,   -1,    0,  289,
  290,   -1,   -1,   -1,   -1,  709,   -1,   42,   62,  323,
  276,   -1,   -1,  717,   -1,   -1,  720,   -1,   -1,  723,
  268,  269,  726,   -1,  272,  273,   -1,  275,  732,  733,
   -1,  735,   -1,  323,   -1,   -1,   40,   -1,   -1,   93,
   -1,  289,  290,   -1,   -1,   -1,   -1,  768,   -1,   -1,
  754,  755,  756,  319,  320,   -1,   60,   -1,  324,  325,
   -1,  327,  328,  329,  330,   -1,   -1,   -1,   -1,  123,
   -1,  125,   -1,   -1,  778,  323,   -1,   -1,    0,   -1,
  257,  258,  259,   -1,  261,  262,  263,   91,  265,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,  274,  275,   -1,
   -1,   -1,  279,   -1,   -1,   -1,   -1,    0,   -1,   40,
  287,   -1,  289,  290,   -1,   -1,  293,   -1,   -1,  123,
  396,  397,  398,  399,  400,  401,  402,  403,  404,  405,
  307,  308,   -1,  310,  311,   -1,   60,  314,  315,  316,
  317,  318,  319,  320,   -1,   -1,  323,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   91,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,   -1,  123,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,  273,
  274,   -1,  257,  258,  259,   -1,  261,  262,  263,  406,
  265,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,  279,  422,   -1,  424,  425,   -1,
   -1,   -1,  287,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,  268,  269,  272,  273,
  272,  273,   -1,  275,   -1,  279,   -1,   -1,   -1,   42,
  334,  335,  336,  287,  338,  339,   -1,  289,  290,  343,
   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,  353,
  354,  355,  356,  357,   -1,  359,  360,   -1,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  323,   -1,  377,   -1,   -1,   -1,  381,  382,  383,
  384,  385,  386,  387,  388,  389,  390,  391,  392,  393,
  264,  395,  273,  274,   -1,   -1,  268,  269,   -1,  273,
  272,  273,  406,  275,   -1,   40,   -1,   -1,  282,  283,
  284,  285,  286,  417,  418,  419,  420,  289,  290,   -1,
  424,   -1,   -1,  377,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,   -1,   -1,  391,  392,  393,
   -1,   -1,   -1,   -1,   -1,   -1,  289,  290,   -1,   -1,
   -1,  323,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   60,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
  323,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,   91,   -1,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,  379,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,  123,   -1,   40,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  287,   -1,  334,  335,  336,   -1,
  338,  339,   -1,   -1,   -1,  343,   60,  345,   -1,   -1,
  348,  349,  350,  351,  352,  353,  354,  355,  356,  357,
   -1,  359,  360,   -1,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,   91,   -1,  377,
   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,
  388,  389,  390,  391,  392,  393,   -1,  395,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  123,
   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,
  418,  419,  420,   -1,  377,   -1,  424,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,  391,  392,
  393,   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   60,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
   91,   -1,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  123,   -1,   40,   -1,   -1,   -1,   -1,  293,
   -1,  378,  417,  418,  419,  420,   -1,   -1,   -1,  424,
  264,   -1,   -1,  307,  308,   -1,  310,  311,   -1,  273,
  314,   -1,  316,  317,  318,  319,  320,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,
   -1,  343,   60,  345,   -1,   -1,  348,  349,  350,  351,
  352,  353,  354,  355,  356,  357,   -1,  359,  360,   -1,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,   91,  378,  377,   -1,   -1,   -1,  381,
  382,  383,  384,  385,   -1,  387,  388,  389,  390,  391,
  392,  393,   -1,  395,  273,  274,   -1,  361,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  123,   -1,   40,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,
   -1,   -1,  424,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  282,  283,  284,  285,  286,   41,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   60,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,   91,   -1,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,  273,  274,   -1,
  361,   -1,   -1,   -1,   -1,   -1,   -1,  406,  123,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,   41,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,
  336,   -1,  338,  339,   -1,   -1,   -1,  343,   60,  345,
   -1,   -1,  348,  349,  350,  351,  352,  353,  354,  355,
  356,  357,   -1,  359,  360,   -1,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,   91,
   -1,  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,
   -1,  387,  388,  389,  390,  391,  392,  393,   -1,  395,
  273,  274,   -1,  361,   -1,   -1,   -1,   -1,   -1,   -1,
  406,  123,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   60,   -1,   -1,  278,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   41,   -1,   -1,   -1,  294,
   -1,  334,  335,  336,   41,  338,  339,   -1,   -1,   -1,
  343,   91,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,   -1,  123,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,  273,  274,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   40,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   41,   -1,
   -1,   -1,  294,   -1,  334,  335,  336,   -1,  338,  339,
   -1,   -1,   -1,  343,   91,  345,   -1,   60,  348,  349,
  350,  351,  352,  353,  354,  355,  356,  357,   -1,  359,
  360,   -1,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,   -1,  123,  377,   91,   -1,
   -1,  381,  382,  383,  384,  385,   -1,  387,  388,  389,
  390,  391,  392,  393,  264,  395,  273,  274,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,  406,   -1,   -1,   40,
  123,   -1,  282,  283,  284,  285,  286,  417,  418,  419,
  420,  257,  258,  259,  424,  261,  262,  263,   -1,  265,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  309,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,  276,
   -1,  287,  279,   -1,   -1,   -1,   -1,  334,  335,  336,
  287,  338,  339,   -1,   -1,   -1,  343,   60,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,   91,   -1,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,  264,  395,  273,
  274,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  406,
  123,   -1,   40,   -1,   -1,  282,  283,  284,  285,  286,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,  309,   -1,   -1,  278,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,
  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,
   60,  345,   -1,   -1,  348,  349,  350,  351,  352,  353,
  354,  355,  356,  357,   -1,  359,  360,   -1,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,   91,   -1,  377,   -1,   -1,   -1,  381,  382,  383,
  384,  385,   -1,  387,  388,  389,  390,  391,  392,  393,
   -1,  395,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,
  424,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   60,   -1,   -1,  278,   -1,   -1,   -1,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,   42,   -1,
   -1,  294,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   91,  345,   -1,   60,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,   -1,  123,  377,   91,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,  273,  274,   -1,   -1,   -1,
   -1,   -1,   42,   -1,   44,  406,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   60,   -1,   -1,  424,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   91,   -1,   -1,  294,   -1,  334,  335,  336,   -1,
  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,
  348,  349,  350,  351,  352,  353,  354,  355,  356,  357,
   42,  359,  360,  123,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,   -1,   60,  377,
   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,
  388,  389,  390,  391,  392,  393,  264,  395,   -1,   -1,
   -1,   -1,   -1,   60,   -1,  273,  274,   -1,  406,   91,
   -1,   -1,   -1,   -1,  282,  283,  284,  285,  286,  417,
  418,  419,  420,  257,  258,  259,  424,  261,  262,  263,
   42,  265,   44,   -1,   91,   -1,   -1,   -1,  272,  273,
   -1,  123,   -1,   42,   -1,  279,   -1,   -1,   60,   -1,
   -1,   -1,   -1,  287,   -1,   -1,   -1,   -1,   -1,  293,
   -1,   60,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,  307,  308,   -1,  310,  311,   -1,   91,
  314,   -1,  316,  317,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,  257,  258,  259,
   60,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,  123,   -1,  273,   -1,  590,   -1,   -1,   -1,  279,
   -1,  125,   -1,   -1,  123,   -1,   -1,  287,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  377,  378,   -1,   -1,   -1,  623,  624,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  391,  392,  393,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,
   -1,   -1,   91,   -1,   -1,  287,   -1,  264,   -1,  684,
  685,  125,   -1,  688,  689,   -1,  273,  377,   -1,   -1,
   -1,   60,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,  391,  392,  393,  123,   -1,   -1,   -1,   -1,   -1,
  715,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   91,  265,   -1,   -1,  260,   -1,  257,  258,
  259,  273,  261,  262,  263,   -1,  265,  279,   -1,   -1,
   -1,   -1,  276,   -1,  273,  287,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,  123,   -1,   60,  762,  287,  346,
  347,  766,  767,   -1,   -1,  377,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,  391,
  392,  393,  272,  273,   -1,  319,  320,   91,   -1,  279,
  324,  325,   -1,  327,  328,  329,  330,  287,   -1,   -1,
   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  819,   -1,  260,  307,  308,  123,
  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,
  320,   -1,  276,   -1,   60,  377,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,  377,  391,
  392,  393,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,  391,  392,  393,   91,   -1,   -1,  287,   -1,
   -1,   -1,   -1,   -1,   -1,  319,  320,   -1,   -1,   -1,
  324,  325,   -1,  327,  328,  329,  330,  377,  378,   -1,
   -1,   60,   -1,   -1,   -1,  264,   60,  123,   -1,   -1,
   -1,  391,  392,  393,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   60,   91,   -1,  293,   -1,   -1,   91,   -1,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,  308,
   -1,  310,  311,  312,   -1,  314,   -1,  316,  317,  318,
  319,  320,   91,   -1,  123,   -1,  125,   -1,  377,  123,
  264,   60,   -1,  332,  333,   -1,   -1,   -1,   -1,  273,
   -1,   -1,  391,  392,  393,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,  123,   -1,   -1,   -1,   -1,  293,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,  307,  308,   -1,  310,  311,   -1,  378,
  314,   -1,  316,  317,  318,  319,  320,  321,   -1,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   60,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,  285,
  286,   -1,   -1,   -1,  123,   -1,   -1,  293,   -1,   -1,
   -1,   91,   -1,   -1,  378,   -1,   -1,   -1,   -1,   -1,
  306,  307,  308,   -1,  310,  311,   -1,   -1,  314,   -1,
  316,  317,  318,  319,  320,  264,  265,  125,   -1,   -1,
  264,  265,   -1,  123,  273,  274,   -1,   -1,   -1,  273,
  274,   -1,   -1,  282,  283,  284,  285,  286,  282,  283,
  284,  285,  286,   -1,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  260,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  282,  283,  284,  285,  286,   -1,  276,
   -1,   -1,  378,   -1,  293,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,  307,  308,
   -1,  310,  311,   -1,  273,  314,   -1,  316,  317,  318,
  319,  320,  321,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,  319,  320,  293,   -1,   -1,  324,  325,  125,
  327,  328,  329,  330,   -1,  264,   -1,   -1,  307,  308,
   -1,  310,  311,   -1,  273,  314,   -1,  316,  317,  318,
  319,  320,  321,  282,  283,  284,  285,  286,   -1,   -1,
   -1,   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,  378,
   -1,   -1,   -1,   -1,  264,  273,   -1,   -1,  307,  308,
   -1,  310,  311,  273,   -1,  314,   -1,  316,  317,  318,
  319,  320,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,  293,   -1,   -1,   -1,   -1,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,
  310,  311,   -1,   -1,  314,   -1,  316,  317,  318,  319,
  320,  125,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,
  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,  378,
  348,  349,  350,  351,  352,  353,  354,  355,  356,  357,
   -1,  359,  360,   -1,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  273,   -1,  377,
   -1,   -1,   -1,  381,  382,  383,  384,  385,  378,  387,
  388,  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,
  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,  334,  335,
  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,
   -1,   -1,  348,  349,  350,  351,  352,  353,  354,  355,
  356,  357,   -1,  359,  360,   -1,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,  273,
   -1,  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,
   -1,  387,  388,  389,  390,  391,  392,  393,   -1,  395,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,
   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,  353,
  354,  355,  356,  357,   -1,  359,  360,   -1,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,  273,   -1,  377,   -1,   -1,   -1,  381,  382,  383,
  384,  385,   -1,  387,  388,  389,  390,  391,  392,  393,
   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,
  424,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,
   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,
   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,
  352,  353,  354,  355,  356,  357,   -1,  359,  360,   -1,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  371,
  372,  373,  374,  273,   -1,  377,   -1,   -1,   -1,  381,
  382,  383,  384,  385,   -1,  387,  388,  389,  390,  391,
  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,
   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,
   -1,   -1,   -1,  343,   -1,  345,   -1,   60,  348,  349,
  350,  351,  352,  353,  354,  355,  356,  357,   -1,  359,
  360,   -1,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  371,  372,  373,  374,  273,   -1,  377,   91,   -1,
   60,  381,  382,  383,  384,  385,   -1,  387,  388,  389,
  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,
  123,   91,   -1,   -1,   -1,   60,   -1,  417,  418,  419,
  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,
  338,  339,   -1,  123,   -1,  343,   91,  345,   -1,   60,
  348,  349,  350,  351,  352,  353,  354,  355,  356,  357,
   -1,  359,  360,   -1,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,   -1,  123,  377,
   91,   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,
  388,  389,  390,  391,  392,  393,   60,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,
   -1,   -1,  123,   -1,  125,   -1,   -1,   -1,   -1,  417,
  418,  419,  420,   60,   -1,   -1,  424,   91,   -1,   93,
  261,  262,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,  260,  261,  262,
  263,  264,  265,   -1,   91,   -1,   -1,   -1,   60,  123,
  273,  274,   -1,  276,   60,   -1,  279,   -1,   -1,  282,
  283,  284,  285,  286,  287,   -1,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  123,   -1,   -1,   91,
   -1,   -1,   -1,  273,  274,   91,  276,   -1,   -1,  279,
   -1,   -1,  282,  283,  284,  285,  286,  287,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
  265,  123,   -1,   -1,   -1,   -1,   -1,  123,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  398,  399,  400,
  401,  282,  283,  284,  285,  286,  407,  408,  409,  410,
  411,  412,  413,  414,  415,  416,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,  264,   -1,   -1,
  273,  274,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  278,   -1,   -1,   -1,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
  282,  283,  284,  285,  286,   -1,  282,  283,  284,  285,
  286,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,  274,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  273,  274,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  273,  274,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  273,  274,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  273,  274,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,  274,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  273,  274,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  273,  274,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  273,  274,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  273,  274,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,  274,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  273,  274,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  273,  274,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  273,  274,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  273,  274,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,  274,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  273,  274,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  273,  274,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  273,  274,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  273,  274,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,  274,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,  273,   -1,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,  419,  420,
   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  334,  335,  336,   -1,  338,
  339,   -1,   -1,   -1,  343,   -1,  345,   -1,   -1,  348,
  349,  350,  351,  352,  353,  354,  355,  356,  357,   -1,
  359,  360,   -1,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  273,   -1,  377,   -1,
   -1,   -1,  381,  382,  383,  384,  385,   -1,  387,  388,
  389,  390,  391,  392,  393,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  417,  418,
  419,  420,   -1,   -1,   -1,  424,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,  335,  336,
   -1,  338,  339,   -1,   -1,   -1,  343,   -1,  345,   -1,
   -1,  348,  349,  350,  351,  352,  353,  354,  355,  356,
  357,   -1,  359,  360,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  273,   -1,
  377,   -1,   -1,   -1,  381,  382,  383,  384,  385,   -1,
  387,  388,  389,  390,  391,  392,  393,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  417,  418,  419,  420,   -1,   -1,   -1,  424,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,
  335,  336,   -1,  338,  339,   -1,   -1,   -1,  343,   -1,
  345,   -1,   -1,  348,  349,  350,  351,  352,  353,  354,
  355,  356,  357,   -1,  359,  360,   -1,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  371,  372,  373,  374,
  273,   -1,  377,   -1,   -1,   -1,  381,  382,  383,  384,
  385,   -1,  387,  388,  389,  390,  391,  392,  393,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  417,  418,  419,  420,   -1,   -1,   -1,  424,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  334,  335,  336,   -1,  338,  339,   -1,   -1,   -1,
  343,   -1,  345,   -1,   -1,  348,  349,  350,  351,  352,
  353,  354,  355,  356,  357,   -1,  359,  360,   -1,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  273,   -1,  377,   -1,   -1,   -1,  381,  382,
  383,  384,  385,   -1,  387,  388,  389,  390,  391,  392,
  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  417,  418,  419,  420,   -1,   -1,
   -1,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  334,  335,  336,   -1,  338,  339,   -1,
   -1,   -1,  343,   -1,  345,   -1,   -1,  348,  349,  350,
  351,  352,  353,  354,  355,  356,  357,   -1,  359,  360,
   -1,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  371,  372,  373,  374,   -1,   -1,  377,   -1,   -1,   -1,
  381,  382,  383,  384,  385,   -1,  387,  388,  389,  390,
  391,  392,  393,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  338,   -1,  417,  418,  419,  420,
   -1,  345,   -1,  424,  348,  349,  350,  351,  352,  353,
  354,  355,  356,  357,   -1,  359,  360,   -1,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,   -1,   -1,  377,   -1,   -1,   -1,  381,  382,  383,
  384,  385,   -1,  387,  388,  389,  390,  391,  392,  393,
   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  417,  418,  419,  420,  291,  292,   -1,
  424,  295,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,   -1,   -1,   -1,   -1,   -1,   -1,  312,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  321,
  };

#line 1320 "Iril/IR/IR.jay"

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
  public const int ATTRIBUTE_GROUP_REF = 322;
  public const int ATTRIBUTES = 323;
  public const int NORECURSE = 324;
  public const int NOUNWIND = 325;
  public const int UNWIND = 326;
  public const int SPECULATABLE = 327;
  public const int SSP = 328;
  public const int UWTABLE = 329;
  public const int ARGMEMONLY = 330;
  public const int SEQ_CST = 331;
  public const int DSO_LOCAL = 332;
  public const int DSO_PREEMPTABLE = 333;
  public const int RET = 334;
  public const int BR = 335;
  public const int SWITCH = 336;
  public const int INDIRECTBR = 337;
  public const int INVOKE = 338;
  public const int RESUME = 339;
  public const int CATCHSWITCH = 340;
  public const int CATCHRET = 341;
  public const int CLEANUPRET = 342;
  public const int UNREACHABLE = 343;
  public const int FNEG = 344;
  public const int ADD = 345;
  public const int NUW = 346;
  public const int NSW = 347;
  public const int FADD = 348;
  public const int SUB = 349;
  public const int FSUB = 350;
  public const int MUL = 351;
  public const int FMUL = 352;
  public const int UDIV = 353;
  public const int SDIV = 354;
  public const int FDIV = 355;
  public const int UREM = 356;
  public const int SREM = 357;
  public const int FREM = 358;
  public const int SHL = 359;
  public const int LSHR = 360;
  public const int EXACT = 361;
  public const int ASHR = 362;
  public const int AND = 363;
  public const int OR = 364;
  public const int XOR = 365;
  public const int EXTRACTELEMENT = 366;
  public const int INSERTELEMENT = 367;
  public const int SHUFFLEVECTOR = 368;
  public const int EXTRACTVALUE = 369;
  public const int INSERTVALUE = 370;
  public const int ALLOCA = 371;
  public const int LOAD = 372;
  public const int STORE = 373;
  public const int FENCE = 374;
  public const int CMPXCHG = 375;
  public const int ATOMICRMW = 376;
  public const int GETELEMENTPTR = 377;
  public const int ALIGN = 378;
  public const int INBOUNDS = 379;
  public const int INRANGE = 380;
  public const int TRUNC = 381;
  public const int ZEXT = 382;
  public const int SEXT = 383;
  public const int FPTRUNC = 384;
  public const int FPEXT = 385;
  public const int TO = 386;
  public const int FPTOUI = 387;
  public const int FPTOSI = 388;
  public const int UITOFP = 389;
  public const int SITOFP = 390;
  public const int PTRTOINT = 391;
  public const int INTTOPTR = 392;
  public const int BITCAST = 393;
  public const int ADDRSPACECAST = 394;
  public const int ICMP = 395;
  public const int EQ = 396;
  public const int NE = 397;
  public const int UGT = 398;
  public const int UGE = 399;
  public const int ULT = 400;
  public const int ULE = 401;
  public const int SGT = 402;
  public const int SGE = 403;
  public const int SLT = 404;
  public const int SLE = 405;
  public const int FCMP = 406;
  public const int OEQ = 407;
  public const int OGT = 408;
  public const int OGE = 409;
  public const int OLT = 410;
  public const int OLE = 411;
  public const int ONE = 412;
  public const int ORD = 413;
  public const int UEQ = 414;
  public const int UNE = 415;
  public const int UNO = 416;
  public const int PHI = 417;
  public const int SELECT = 418;
  public const int CALL = 419;
  public const int TAIL = 420;
  public const int VA_ARG = 421;
  public const int ASM = 422;
  public const int SIDEEFFECT = 423;
  public const int LANDINGPAD = 424;
  public const int CATCH = 425;
  public const int CATCHPAD = 426;
  public const int CLEANUPPAD = 427;
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
