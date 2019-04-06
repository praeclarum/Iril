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
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type nonglobal_value ',' ALIGN INTEGER metadata_kvs",
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
    "FASTCC","SIGNEXT","ZEROEXT","VOLATILE","RETURNED","DEREFERENCEABLE",
    "AVAILABLE_EXTERNALLY","PERSONALITY","SRET","CLEANUP","NONNULL",
    "NOCAPTURE","WRITEONLY","READONLY","READNONE","HIDDEN",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","UNWIND",
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST","DSO_LOCAL",
    "DSO_PREEMPTABLE","RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME",
    "CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD",
    "NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV",
    "UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR","XOR",
    "EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT",
    "FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT",
    "INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE",
    "ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ","OGT","OGE","OLT",
    "OLE","ONE","ORD","UEQ","UNE","UNO","PHI","SELECT","CALL","TAIL",
    "VA_ARG","LANDINGPAD","CATCH","CATCHPAD","CLEANUPPAD",
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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 126 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 130 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 134 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 138 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 142 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 146 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 25:
#line 150 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 154 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 27:
#line 158 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 28:
#line 162 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 29:
#line 166 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 30:
#line 170 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 31:
#line 171 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 32:
#line 175 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 33:
#line 176 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 34:
#line 177 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 35:
#line 178 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 36:
#line 179 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 180 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 38:
#line 184 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 39:
#line 188 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 40:
  case_40();
  break;
case 41:
  case_41();
  break;
case 42:
#line 205 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 43:
#line 206 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 44:
#line 207 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 45:
#line 211 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 46:
#line 215 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 57:
#line 244 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 58:
#line 248 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 59:
#line 255 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 60:
#line 259 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 61:
#line 263 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 62:
#line 267 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 63:
#line 271 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 81:
#line 304 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 82:
#line 308 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 312 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 84:
#line 319 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 323 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 328 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 90:
#line 334 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 91:
#line 335 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 92:
#line 336 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 93:
#line 337 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 94:
#line 341 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 95:
#line 345 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 349 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 97:
#line 353 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 98:
#line 357 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 99:
#line 361 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 365 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 372 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 376 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 384 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 105:
  case_105();
  break;
case 106:
  case_106();
  break;
case 116:
#line 416 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 117:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 118:
#line 424 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 119:
#line 428 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 120:
#line 432 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 121:
#line 439 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 122:
#line 443 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 123:
#line 447 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 128:
#line 458 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 129:
#line 465 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
    }
  break;
case 130:
#line 469 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 131:
#line 473 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 132:
#line 477 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 133:
#line 481 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 137:
#line 491 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 138:
#line 492 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 139:
#line 499 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 140:
#line 503 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 510 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 142:
#line 514 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 143:
#line 518 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 144:
#line 522 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 146:
#line 530 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 534 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 148:
#line 535 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 149:
#line 536 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 150:
#line 537 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 151:
#line 538 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 152:
#line 539 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 153:
#line 540 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 154:
#line 541 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 155:
#line 542 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 156:
#line 543 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 157:
#line 547 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 165:
#line 570 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 166:
#line 571 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 167:
#line 572 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 168:
#line 573 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 169:
#line 574 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 170:
#line 575 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 171:
#line 576 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 172:
#line 577 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 173:
#line 578 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 174:
#line 579 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 175:
#line 583 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 176:
#line 584 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 177:
#line 585 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 178:
#line 586 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 179:
#line 587 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 180:
#line 588 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 181:
#line 589 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 182:
#line 590 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 183:
#line 591 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 184:
#line 592 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 185:
#line 593 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 186:
#line 594 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 187:
#line 595 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 188:
#line 596 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 189:
#line 597 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 190:
#line 598 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 191:
#line 602 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 195:
#line 612 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 196:
#line 616 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 197:
#line 620 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 199:
#line 628 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 200:
#line 632 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 201:
#line 636 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 202:
#line 640 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 204:
#line 648 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 205:
#line 649 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 206:
#line 650 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 207:
#line 651 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 208:
#line 652 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 209:
#line 653 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 210:
#line 654 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 211:
#line 655 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 212:
#line 656 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 213:
#line 663 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 670 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 674 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 216:
#line 681 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 688 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 692 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 699 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 707 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 222:
#line 714 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 718 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 729 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 733 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 740 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 744 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 751 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 759 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 233:
#line 770 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 774 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 781 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 785 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 789 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 793 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 801 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 241:
#line 802 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 242:
#line 809 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 813 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 820 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 245:
#line 824 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 246:
#line 828 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 247:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 248:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 249:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 250:
#line 844 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 252:
#line 849 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 253:
#line 853 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 254:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 255:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 256:
#line 865 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 261:
#line 882 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 886 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 892 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 264:
#line 899 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 910 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 928 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 273:
#line 935 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 946 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 954 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 958 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 280:
#line 966 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 282:
#line 974 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 978 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 982 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 986 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 994 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 998 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1002 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 289:
#line 1006 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 290:
#line 1010 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1014 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 292:
#line 1018 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 293:
#line 1022 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1026 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 295:
#line 1030 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 296:
#line 1034 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 297:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 298:
#line 1042 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 299:
#line 1046 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 300:
#line 1050 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 301:
#line 1054 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 302:
#line 1058 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 303:
#line 1062 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 304:
#line 1066 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 305:
#line 1070 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 306:
#line 1074 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 307:
#line 1078 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 308:
#line 1082 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 309:
#line 1086 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1090 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1094 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1098 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1102 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1106 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1110 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1114 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1118 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1122 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1126 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1130 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1134 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1138 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1142 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1146 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1150 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1154 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1158 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 328:
#line 1162 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1166 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 330:
#line 1170 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 331:
#line 1174 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 332:
#line 1178 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 333:
#line 1182 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 334:
#line 1186 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 335:
#line 1190 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 336:
#line 1194 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1198 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1202 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 339:
#line 1206 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 340:
#line 1210 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1214 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1218 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 343:
#line 1222 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 344:
#line 1226 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1230 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1234 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1238 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 348:
#line 1242 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 349:
#line 1246 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 350:
#line 1250 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 351:
#line 1254 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1258 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1262 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1266 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1270 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1274 "Iril/IR/IR.jay"
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

void case_40()
#line 193 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_41()
#line 198 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_105()
#line 389 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_106()
#line 394 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,   10,
   10,   16,   16,   16,   16,   16,   16,   15,   17,    9,
    9,   18,   18,   18,   18,   18,   19,   22,   22,   23,
   24,   24,   24,   24,   24,   24,   13,   13,    8,    8,
    8,    8,    8,   26,   26,   26,    7,    7,   28,   28,
   28,   28,   28,   28,   28,   28,   28,   28,   28,   28,
    3,    3,    3,   29,   29,   30,   30,   11,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   11,
   31,   31,   32,   32,    4,    4,   35,   35,   35,   35,
   35,   35,   35,   35,   35,   33,   33,   33,   33,   33,
   40,   40,   40,   40,   40,   40,   40,   38,    5,    5,
    5,    5,    5,   44,   44,   44,   34,   34,   45,   45,
   46,   46,   46,   46,   41,   41,   39,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   39,   14,   14,   42,
   42,   37,   37,   47,   48,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   50,   51,   51,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   54,   20,   20,   20,   20,   20,   20,   20,
   20,   20,   55,   27,   27,   56,   53,   53,   25,   57,
   57,   52,   52,   58,   59,   59,   36,   36,   60,   60,
   60,   60,   61,   61,   63,   63,   63,   63,   65,   66,
   66,   67,   67,   68,   68,   68,   68,   68,   68,   68,
   69,   69,   69,   69,   69,   69,   21,   21,   70,   70,
   71,   71,   72,   73,   73,   74,   75,   75,   76,   76,
   43,   77,   62,   62,   78,   78,   78,   78,   78,   78,
   78,   79,   79,   79,   79,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   10,   11,    9,   10,    8,    5,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    3,    3,    3,    3,    6,    5,    1,    1,    3,    1,
    1,    1,    1,    1,    1,    1,    2,    3,    1,    2,
    3,    3,    3,    1,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    3,    1,    1,    1,    4,
    2,    3,    5,    1,    3,    1,    1,    1,    1,    1,
    1,    1,    1,    3,    4,    2,    4,    1,    5,    5,
    1,    3,    1,    1,    7,    8,    1,    2,    4,    3,
    5,    1,    3,    2,    4,    2,    3,    3,    4,    4,
    1,    1,    1,    1,    2,    3,    2,    2,    4,    5,
    6,    6,    7,    1,    2,    1,    3,    2,    1,    3,
    1,    2,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    4,    1,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    6,    9,    6,    6,    3,
    3,    3,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,    2,    1,    2,    1,    3,    2,    1,
    2,    1,    3,    1,    1,    3,    1,    2,    2,    3,
    1,    2,    1,    2,    1,    2,    3,    4,    1,    3,
    2,    1,    3,    2,    3,    3,    3,    2,    4,    5,
    1,    1,    6,    9,    6,    6,    1,    3,    1,    1,
    1,    3,    5,    1,    2,    3,    1,    2,    1,    1,
    1,    1,    1,    3,    2,    7,    2,    2,    7,    1,
    1,    8,    9,    9,   10,    5,    6,    5,    7,    5,
    5,    6,    4,    4,    5,    6,    6,    7,    5,    6,
    6,    6,    7,    5,    6,    7,    7,    8,    4,    4,
    5,    6,    5,    2,    5,    4,    4,    4,    4,    5,
    6,    7,    6,    6,    6,    4,    3,    4,    7,    8,
    5,    6,    5,    5,    6,    3,    4,    5,    6,    7,
    4,    5,    6,    6,    4,    5,    7,    8,    5,    6,
    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   87,   98,   90,   91,   92,   93,   89,  121,   34,
   32,   35,   36,   37,  271,  152,  153,  154,    0,   33,
  155,  147,  148,  150,  149,  151,  160,  161,    0,    0,
    0,   88,    0,    0,    0,    0,    0,  122,  123,    0,
    0,  145,    0,    0,    3,    0,    4,    0,    0,  158,
  159,   30,   31,   38,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   81,    0,    0,    0,    0,    0,    0,
    0,    0,  127,    0,    0,  156,   39,    0,    0,    0,
    0,    0,    0,    0,  146,    0,    0,    0,    5,    6,
    0,    0,    0,    0,    0,    8,    0,    7,    0,    0,
    0,    0,    0,   82,    0,    0,    0,    0,    0,  126,
  104,   94,    0,    0,  101,    0,    0,    0,    0,    0,
    0,    0,  143,  144,  138,    0,    0,  139,  164,    0,
    0,    0,  162,  206,  207,  205,  208,  209,  210,  204,
  195,  212,  211,    0,    0,    0,    0,    0,    0,    0,
    0,  194,    0,    0,    0,    0,    0,    0,    0,    0,
   40,    0,    0,    0,   66,   65,   13,    0,    0,   59,
   64,  157,    0,    0,    0,    0,   97,   95,    0,    0,
    0,    0,    0,  130,    0,    0,    0,   79,   71,   69,
   70,   72,   73,   74,   75,    0,   67,    0,  137,    0,
    0,    0,    0,    0,    0,    0,  114,  163,    0,    0,
    0,    0,  217,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   60,
   14,    0,  191,  193,  192,  214,   99,   83,  100,  102,
  131,    0,    0,  132,    0,    0,   12,   68,  140,    0,
  110,   57,    0,    0,    0,    0,    0,    0,  280,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  227,    0,    0,  233,
    0,  273,  281,    0,    0,  128,    0,    0,    0,    0,
    0,  202,    0,  200,  201,    0,    0,    0,    0,    0,
    0,    0,   53,   56,    0,   51,    0,   42,   54,    0,
   48,   50,   55,   52,   43,   44,   41,   17,   16,   63,
   62,   61,  133,   76,  260,  259,    0,  257,    0,    0,
  278,    0,    0,  275,    0,    0,    0,    0,  277,  269,
  270,    0,    0,  267,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  272,  314,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  165,  166,  167,  168,  169,  170,  171,  172,
  173,  174,    0,  175,  176,  187,  188,  189,  190,  178,
  180,  181,  182,  183,  179,  177,  185,  186,  184,    0,
    0,    0,    0,    0,    0,    0,    0,  105,  228,    0,
  234,    0,    0,   58,    0,  115,    0,    0,    0,    0,
  218,    0,    0,    0,   28,    0,    0,    0,    0,  219,
    0,   80,    0,  111,    0,  274,  213,    0,    0,  239,
    0,    0,    0,    0,    0,    0,  268,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  261,    0,    0,
    0,    0,    0,    0,    0,    0,  327,    0,    0,  106,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   46,    0,   49,  258,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,    0,    0,  224,
  225,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  328,    0,  199,
  196,  198,    0,    0,    0,    0,   45,    0,    0,    0,
  241,    0,    0,  242,    0,    0,    0,    0,  286,    0,
  311,  349,    0,  320,  334,    0,  315,  352,    0,  338,
  313,  354,  346,  342,    0,    0,  331,    0,  291,  290,
  333,  355,    0,    0,    0,    0,  288,    0,    0,  203,
  216,    0,    0,    0,    0,    0,    0,    0,    0,  262,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  264,    0,    0,    0,  244,
  240,    0,    0,    0,    0,    0,  287,  350,  335,  339,
  343,  332,  292,  324,  344,  226,    0,    0,    0,    0,
    0,    0,    0,    0,  220,    0,  222,  323,  312,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  276,
    0,  279,  265,    0,  252,  246,    0,    0,    0,    0,
  251,  247,  245,  243,    0,    0,    0,    0,  289,    0,
  329,    0,  347,    0,  221,    0,  263,  340,    0,    0,
    0,    0,  197,  266,  249,    0,    0,    0,    0,    0,
  282,    0,    0,    0,  330,  348,  223,    0,  250,    0,
    0,    0,    0,  283,  284,    0,    0,    0,    0,    0,
  285,    0,    0,    0,    0,    0,  256,  253,  255,    0,
    0,  254,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  216,  188,  180,   75,
  189,  254,  224,  202,   77,   54,   98,  181,  358,  172,
  377,  360,  361,  362,  363,  190,  775,  217,   86,   87,
  134,  135,   15,  108,  151,  326,  203,  227,   62,   57,
   63,   58,   59,  204,  147,  148,  153,  453,  470,  255,
  510,  776,  234,  721,  384,  638,  777,  631,  632,  327,
  328,  329,  330,  331,  511,  599,  683,  684,  802,  378,
  567,  568,  745,  746,  393,  394,  428,  332,  333,
  };
  protected static readonly short [] yySindex = {          608,
  -23,   93,   25,   65,   72, 3334, 3597, -263,    0,  608,
    0,    0,    0,    0, -126, -123,   96,  124, 1187, -118,
  -19,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  150,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3902, -106,
  -58,    0,  178, -190,  196, 4007, 3395,    0,    0, 3633,
  -28,    0, 3633,  168,    0,  201,    0,   -9,   33,    0,
    0,    0,    0,    0, 4007,  101,   88,  -60,    5,  216,
  -25,  176,   57,    0,  178,  -12,  196,   51, 4007,   62,
  -36,   43,    0, 3567,  196,    0,    0, 4007,  196, 3633,
  -22, 3633,  201,  -13,    0,  228, 2690, -242,    0,    0,
 2533, 4007,  101, 4007,  101,    0,  245,    0, -245,  330,
  282, 3836,  361,    0, 4007, 4007,   28, 4007,  153,    0,
    0,    0,  178,  132,    0,  196,  196,  201,    1, -242,
  201,  857,    0,    0,    0, 2284,  183,    0,    0,   95,
 -108, -225,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   63,  402,  403,  406, 4042, 4042, 4042,
  401,    0, 2533, 4007, 2353, 4007,  390,  394,  395,  260,
    0, -245, 3864,    0,    0,    0,    0,  -20, 2420,    0,
    0,    0,  178,   61,  396,    4,    0,    0, 3730, -242,
  201,   95,   95,    0, -242,  398,  415,    0,    0,    0,
    0,    0,    0,    0,    0,  804,    0, 3459,    0, 3707,
 -193,  182, 5989, -107, 4042,  200,    0,    0,  420, 4042,
 4042, 4042,    0,   29,   98,   37,  102,  417, 2533,  103,
  431, 2498, 3772, 3811,  405,    0, -245,  262,   -4,    0,
    0, 3878,    0,    0,    0,    0,    0,    0,    0,    0,
    0, -242,   95,    0,  221, 3915,    0,    0,    0,  237,
    0,    0,  424, 4042, -135, 4042, 3552, 4042,    0, 2645,
 4007, 2645, 4007, 2645, 4007, 4007,  -16, 4007, 4007, 4007,
 2645, 2575, 2580, 4007, 4007, 4007, 4042, 4042, 4042, 4042,
 4042, 4007, 1008, 3669,  184,  825, 4042, 4042, 4042, 4042,
 4042, 4042, 4042, 4042, 4042, 4042, 4042, 4042, 1105, -199,
 4007, 4007, 3552,   99, 4007, 2691,    0, 5989,  236,    0,
  236,    0,    0,  239, 5989,    0,  208, 4007,  136,  139,
  141,    0, 4042,    0,    0,  267,  152,  483,  271,  155,
  156,  487,    0,    0,  493,    0, 1118,    0,    0,  410,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  264,    0,  208, 6457,
    0,  272, 2631,    0,  498,  546, 3633, 3633,    0,    0,
    0, 2420, 2645,    0, 2420, 2420, 2645, 2420, 2420, 2645,
 2420, 2420, 4007, 2420, 2420, 2420, 2420, 2420, 2645, 4007,
 2420, 4007, 2420, 2420, 2420, 2420,  500,  517,  519,  520,
  521,  114, 4007,  302, 4042,  524,    0,    0, 4007,  310,
  191,  195,  212,  215,  217,  218,  219,  227,  229,  231,
  233,  234,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4007,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4007,
   68, 2420,  546, 4007, 3633, 3552,  -39,    0,    0,  236,
    0,  263,  263,    0, 2789,    0,  318, 4007, 4007, 4007,
    0,  236,  321,  246,    0,  362,  364,  248,  326,    0,
 3811,    0, 3915,    0,  236,    0,    0,  583,  359,    0,
  588,  546,  546, 3633,  585, 2420,    0,  595,  596, 2420,
  597,  598, 2420,  601,  603, 2420,  604,  605,  606,  609,
  631, 2420, 2420,  632, 2420,  633,  634,  635,  639, 4042,
 4042, 4042,  405, 4042,   92,  327, 4007,  641, 4007,  342,
 4042, 4007, 4007, 4007, 4007, 4007, 4007, 4007, 4007, 4007,
 4007, 4007, 4007, 2420, 2420, 2631,  643,    0,  646,  588,
  546,  546, 4007,  546, 4007, 3633,    0, 4042,  263,    0,
 4042,  351,  357,  367,  263,  236,  375,  236,    0,  377,
    0,  268,    0,    0,  263,  359,  607, 3694,  312,  588,
  588,  546, 2631,  648, 2631, 2631,  651, 2631, 2631,  652,
 2631, 2631,  653, 2631, 2631, 2631, 2631, 2631,  656,  657,
 2631,  659, 2631, 2631, 2631, 2631,    0,  660,  661,    0,
    0,  665,  669,  458,  672, 4007, 2420,  673, 4007,  674,
 4042,  675,  178,  178,  178,  178,  178,  178,  178,  178,
  178,  178,  178,  178,  676,  679,  681,  636, 4042,   95,
  588,  588,  546,  588,  546,  546, 3633,    0,  682,    0,
    0,    0,  263,  236,  263,  236,    0,  688, 4007, 3926,
    0,  449,  269,    0,  359,  354,  355,  588,    0, 2631,
    0,    0, 2631,    0,    0, 2631,    0,    0, 2631,    0,
    0,    0,    0,    0, 2631, 2631,    0, 2631,    0,    0,
    0,    0, 4042, 4042,  405,  405,    0,  368,  694,    0,
    0,  369,  700,  371,  704,  -21, 2631, 2631, 2631,    0,
  705,   95,   95,   95,  588,   95,  588,  588,  546,  -21,
  263,  263,  359,  706, 3959,    0,  714,  -40, 2563,    0,
    0, 4000,  435,  359,  359,  376,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  665,  503,  392,  512,
  397,  515,  -21, 4042,    0,  729,    0,    0,    0,  683,
 4042,   95,   95,   95,   95,   95,   95,  588,  279,    0,
  359,    0,    0, 2411,    0,    0,  399,  735,  738,  739,
    0,    0,    0,    0,  359,  460,  463,  359,    0,  531,
    0,  537,    0,  729,    0,  -21,    0,    0,   95,   95,
   95,   95,    0,    0,    0,  284,  755, 4042, 4042, 4042,
    0,  359,  359,  480,    0,    0,    0,   95,    0, 4007,
  428,  430,  432,    0,    0,  359,  346, 4007, 4007, 4007,
    0, 4042,  384,  386,  393,  762,    0,    0,    0,  -21,
  286,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  813,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  775, 3433,  544,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  -11,    0,    0,    0,    0,    0,
  882, 3497,    0,    0,  545,    0,    0,    0,  548,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  781,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  148,    0,    0,  551,  552,    0,    0,  143,
    0,    0,    0,    0,    0,  254,    0,    0,    0, -103,
    0, -102,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  781,    0,  161,    0,    0,    0,    0,    0,
    0,    0,    0,  980,    0,    0,    0,    0,  781,    0,
    0,    0,  -10,  781,    0,  781,    0,    0,    0,    0,
    0,  247,  307,    0,    0, 2623, 2685,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  298,    0,    0,
 -100,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  781,    0,
    0,  781,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  335,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 2887,    0,
 6087,    0,    0,    0,    0,    0,  -97,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  383,    0,    0,
    0,    0,    0,    0,   12,    0,  781,    0,    0,  299,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -95,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  781,    0,    0,  781,  781,    0,  781,  781,    0,
  781,  781,    0,  781,  781,  781,  781,  781,    0,    0,
  781,    0,  781,  781,  781,  781,    0,    0,    0,    0,
    0,  781,    0,  781,    0,    0,    0,    0,    0,  781,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  781,  781,    0,    0,    0,    0,  781,    0,    0, 2985,
    0, 3083, 6185,    0,    0,    0,  781,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 6283,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  781,    0,    0,    0,  781,
    0,    0,  781,    0,    0,  781,    0,    0,    0,    0,
    0,  781,  781,    0,  781,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  781,    0,    0,    0,  781,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  781,  781,    0, 4029,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3181,    0,
    0,  781,  781,  781,  581,    0,    0,  590,    0,    0,
    0,    0,    0,    0, 6381,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4127,    0,    0,    0,    0,  781,    0,    0,    0,
    0,    0, 1078, 1179, 1277, 1375, 1476, 1574, 1672, 1773,
 1871, 1969, 2070, 2168,    0,    0,    0,    0,    0, 4225,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  618,  693,  797, 2361,    0,    0,    0,    0,
    0,  781,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4323,    0, 4421,    0, 4519,    0,    0,    0,    0,
 2369, 2445,    0,    0,    0,    0,  309,  781,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4617,    0,    0,    0,
    0,    0,    0,    0,    0, 4715,    0,    0,    0,    0,
    0, 4813, 4911, 5009, 5107, 5205, 5303,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5401,    0,    0,    0,    0, 5499, 5597,
 5695, 5793,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5891,    0,    0,
    0,    0,    0,    0,    0,    0,  781,    0,    0,    0,
    0,    0,  781,  781,  781,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  816,  748,    0,    0,    0,    0,  645,  647,   87,
   -6,  -64, -252,   40,    0,  812,    0,  586,  602, -177,
 -492,    0,  338,    0, -660,    0,  189,  624,  752,   84,
    0,  662,    0,  -67,    0,  510,  -98, -210,   -2,    0,
  -51,  793,  -32, -163,    0,  640, -139,    0,    0,    0,
  385, -715,  270,    0, -354, -504,   35,  137,  151, -321,
    0,  527,  536,  488,  472, 3634,    0,  119,    0,  370,
    0,  214,    0,  129, -204, -206,    0,    0,  495,
  };
  protected static readonly short [] yyTable = {            53,
   53,   91,   91,   56,  479,  100,  592,  121,  102,  152,
  271,   94,  228,   82,  223,  335,   89,   94,  744,  107,
  112,   93,  108,  252,  789,  113,   94,  109,   86,   86,
  177,  125,   84,   85,  178,  140,  261,   16,   50,  252,
   94,  264,   85,   50,  640,   91,  171,   70,   71,   53,
   53,  221,   52,   53,   64,   52,   53,  814,   76,  130,
  105,  454,  455,  228,  117,  359,  359,  366,  111,   51,
  200,  125,  343,  205,   51,  149,  482,  397,  483,  400,
  343,  228,   85,  225,  744,   19,  409,  133,  376,   55,
   61,   53,  149,   53,  218,   53,  259,  105,  373,  105,
  146,   49,   91,  263,  251,  173,   49,  175,  238,   91,
  241,   35,  124,   84,   85,  225,  113,  115,  193,  194,
  369,  196,  257,  228,  149,   20,  486,   49,  179,  345,
  382,  719,   21,  262,  723,   52,   67,   47,   48,   95,
   99,  343,  129,  101,  861,   66,  104,  150,  226,  383,
   88,   50,  195,  342,  597,   91,   68,  545,  566,  344,
   29,   79,  112,  479,  114,  222,  334,  239,  504,  242,
  107,  112,  198,  108,  348,  199,  113,  352,  109,  500,
  270,  136,   51,  137,   69,  139,  517,   86,  103,   83,
  517,  103,  133,  517,  456,  457,  458,  459,   90,  174,
   86,  176,  517,  460,  461,  462,  463,  464,  465,  466,
  467,  468,  469,  146,   49,  105,  154,  155,  156,   91,
  157,  158,  159,  219,  160,  387,  220,  579,  106,   70,
   71,  795,  796,   72,   73,   94,  357,  357,  162,  585,
  107,  678,  184,  103,  388,  163,  134,   22,  120,  138,
  109,   23,  595,  250,   80,  119,   23,   81,  141,   24,
   25,   26,   27,   28,   24,   25,   26,   27,   28,  250,
   53,  577,  201,  392,  395,  396,  398,  399,  401,  402,
  404,  405,  406,  407,  408,  411,  413,  414,  415,  416,
  475,  116,  110,   86,  141,  422,  424,  141,  122,  430,
  246,  826,  368,  247,  502,  247,  136,  503,  677,  751,
  191,  503,  752,  123,  471,  472,   53,  126,  477,  823,
  474,  376,  816,  359,  839,  376,  862,  503,  128,  816,
  753,  487,  797,  673,  135,  675,  514,  129,  142,   47,
  403,  142,   47,   91,   35,  547,  798,  799,  800,  248,
  142,   91,  248,  551,  774,  184,  233,  233,  233,   91,
  386,  581,   17,   18,   23,  630,  591,   89,   91,  182,
  636,  191,   24,   25,   26,   27,   28,   70,   71,  578,
   53,   53,   21,   91,  105,  641,  516,   91,  790,  852,
  520,  670,   91,  523,   72,   73,  526,  671,   91,  806,
  807,  192,  532,  533,  183,  535,  473,  672,   91,  197,
  129,  129,  149,  336,  129,  129,  546,  129,  339,  340,
  341,  741,  550,  742,  857,   91,  858,   91,   29,   29,
  129,  129,   29,  859,   91,   29,  824,  229,  235,  236,
  372,  230,  231,  576,  237,  232,  564,  243,   29,   29,
  831,  244,  245,  834,  266,  272,  337,  258,  265,  338,
  347,  129,  381,  565,  385,  634,  389,   53,   53,   53,
  512,  513,  573,  575,  350,  346,  349,  844,  845,   29,
  374,  582,  583,  584,  380,  417,  418,  419,  420,  421,
   91,  851,  426,  379,  357,  431,  432,  433,  434,  435,
  436,  437,  438,  439,  440,  441,  442,   53,  169,  222,
  427,  105,  484,  476,  134,  134,  225,  488,  134,  134,
  489,  134,  490,  492,  667,  493,  494,  495,  496,  497,
  498,  491,  499,  501,  134,  134,  334,  630,  630,  170,
  637,  509,  637,  540,  507,  643,  644,  645,  646,  647,
  648,  649,  650,  651,  652,  653,  654,  571,  572,  574,
  541,  732,  542,  543,  544,  134,   53,  549,   53,   53,
  801,  168,  552,  256,  136,  136,  553,  586,  136,  136,
   18,  136,  154,  155,  156,   94,  157,  158,  159,   26,
  160,  682,  228,  554,  136,  136,  555,  602,  556,  557,
  558,  375,  135,  135,  162,  169,  135,  135,  559,  135,
  560,  163,  561,  548,  562,  563,  376,   19,  588,  587,
  589,  590,  135,  135,  382,  136,  596,  598,  603,  637,
  749,  674,  637,  676,  782,  783,  170,  785,  605,  606,
  608,  609,  228,  228,  611,  228,  612,  614,  615,  616,
   21,   21,  617,  135,   21,   21,  663,   21,  665,  666,
   53,  154,  155,  156,  105,  157,  158,  159,  168,  160,
   21,   21,  357,  748,  618,  621,  623,  624,  625,  228,
  228,  228,  626,  162,  639,  819,  658,  820,  821,  659,
  163,  690,   20,  685,  693,  696,  699,  679,  228,  705,
  706,   21,  708,  713,  714,  154,  155,  156,  715,  157,
  158,  159,  716,  160,  717,  718,  722,  724,  726,  727,
  253,  161,  728,  838,  729,  740,  566,  162,  627,  628,
  629,  743,  633,  635,  163,  754,  755,  769,  357,  642,
   96,  768,  770,  771,  772,  682,  105,  773,  781,  791,
  739,   36,   37,  794,   38,   39,  805,  808,   41,  809,
   42,   43,   44,   45,   46,  810,  668,  508,  811,  669,
  812,  813,  816,  827,  828,  817,  515,  829,  830,  518,
  519,  832,  521,  522,  833,  524,  525,  835,  527,  528,
  529,  530,  531,  836,  840,  534,   27,  536,  537,  538,
  539,  846,  154,  155,  156,  860,  157,  158,  159,  848,
  160,  849,    1,  850,   86,  116,  117,  253,  161,  118,
   86,  164,  119,  120,  162,   65,  118,  249,  248,  725,
   78,  163,  367,  847,   86,  165,  166,  167,  593,  268,
  127,  853,  854,  855,  485,  365,   92,  731,   18,   18,
  837,  766,   18,   18,  480,   18,  569,   26,   26,  269,
  260,   26,   26,  481,   26,   86,  767,  505,   18,   18,
  804,  730,  594,  793,  506,    1,    2,   26,   26,    3,
    4,   96,    5,    0,   50,   19,   19,    0,    0,   19,
   19,    0,   19,    0,    0,    6,    7,   86,    0,   18,
  604,  764,  765,    0,  607,   19,   19,  610,   26,    0,
  613,    0,    0,    0,    0,   51,  619,  620,  164,  622,
    0,   96,   96,   96,    0,   96,    8,    0,  267,    0,
    0,    0,  165,  166,  167,    0,   19,    0,    0,    0,
    0,   96,    0,   96,  570,    0,    0,   49,  655,  656,
  657,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   20,   20,  815,    0,   20,   20,    0,   20,    0,  818,
    0,    0,   96,    0,   96,    0,    0,    0,    0,    0,
   20,   20,    0,  600,  601,    0,    0,  689,    0,  691,
  692,    0,  694,  695,    0,  697,  698,    0,  700,  701,
  702,  703,  704,    0,   96,  707,   96,  709,  710,  711,
  712,   20,    0,    0,    0,    0,  841,  842,  843,   87,
  215,  720,    0,  215,    0,    0,    0,    0,    0,    0,
    0,   86,   86,   86,    0,   86,   86,   86,    0,   86,
  856,  215,  661,  662,    0,  664,   86,   86,    0,    0,
    0,    0,    0,   86,    0,    0,    0,    0,    0,    0,
   86,    0,    0,  206,   27,   27,  750,   50,   27,   27,
    0,   27,  215,  688,  757,    0,    0,  758,    0,  207,
  759,    0,    0,  760,   27,   27,    0,    0,   22,  761,
  762,    0,  763,    0,    0,    0,    0,   23,   51,    0,
    0,    0,  215,    0,  215,   24,   25,   26,   27,   28,
    0,  778,  779,  780,    0,   27,  206,   86,  208,  209,
    0,    0,    0,  210,  211,    0,  212,  213,  214,  215,
   49,    0,  207,  803,  735,    0,  737,  738,   96,   96,
   96,    0,   96,   96,   96,    0,   96,   86,    0,   96,
   96,    0,    0,   96,   96,   96,   96,    0,    0,   91,
   96,   86,   86,   86,    0,    0,    0,   96,    0,   96,
   96,  208,  209,   96,    0,    0,  210,  211,    0,  212,
  213,  214,  215,    0,   96,   96,    0,   96,   96,    0,
    0,   96,   96,   96,   96,   96,   96,   96,    0,  429,
   96,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  788,   96,   96,   96,    0,   96,   96,    0,   86,    0,
   96,    0,   96,    0,    0,   96,   96,   96,   96,   96,
   96,   96,   96,   96,   96,    0,   96,   96,    0,   96,
   96,   96,   96,   96,   96,   96,   96,   96,   96,   96,
   96,   96,  215,  215,   96,    0,    0,    0,   96,   96,
   96,   96,   96,    0,   96,   96,   96,   96,   96,   96,
   96,   22,   96,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,   96,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,   96,   96,   96,   96,    0,   96,
   96,    0,    0,    0,    0,    0,    0,    0,    0,  215,
  215,  215,  423,  215,  215,    0,   86,    0,  215,    0,
  215,    0,    0,  215,  215,  215,  215,  215,  215,  215,
  215,  215,  215,    0,  215,  215,    0,  215,  215,  215,
  215,  215,  215,  215,  215,  215,  215,  215,  215,  215,
  351,  351,  215,    0,    0,    0,  215,  215,  215,  215,
  215,  215,  215,  215,  215,  215,  215,  215,  215,    0,
  215,    0,    0,    0,  154,  155,  156,    0,  157,  158,
  159,  215,  160,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  215,  215,  215,  215,  162,  215,    0,    0,
    0,    0,    0,  163,    0,    0,    0,  351,  351,  351,
    0,  351,  351,    0,   86,    0,  351,    0,  351,    0,
    0,  351,  351,  351,  351,  351,  351,  351,  351,  351,
  351,    0,  351,  351,    0,  351,  351,  351,  351,  351,
  351,  351,  351,  351,  351,  351,  351,  351,    0,    0,
  351,  356,  356,    0,  351,  351,  351,  351,  351,    0,
  351,  351,  351,  351,  351,  351,  351,    0,  351,    0,
    0,    0,    0,    0,    0,    0,   70,   71,    0,  351,
   72,   73,   74,   30,   31,   32,   33,   34,    0,    0,
  351,  351,  351,  351,   40,  351,  443,  444,  445,  446,
  447,  448,  449,  450,  451,  452,    0,    0,  356,  356,
  356,    0,  356,  356,    0,   86,    0,  356,    0,  356,
    0,    0,  356,  356,  356,  356,  356,  356,  356,  356,
  356,  356,    0,  356,  356,    0,  356,  356,  356,  356,
  356,  356,  356,  356,  356,  356,  356,  356,  356,  341,
  341,  356,    0,    0,    0,  356,  356,  356,  356,  356,
    0,  356,  356,  356,  356,  356,  356,  356,    0,  356,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  356,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  356,  356,  356,  356,    0,  356,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  341,  341,  341,    0,
  341,  341,    0,   86,    0,  341,    0,  341,    0,    0,
  341,  341,  341,  341,  341,  341,  341,  341,  341,  341,
    0,  341,  341,    0,  341,  341,  341,  341,  341,  341,
  341,  341,  341,  341,  341,  341,  341,  319,  319,  341,
    0,    0,    0,  341,  341,  341,  341,  341,    0,  341,
  341,  341,  341,  341,  341,  341,    0,  341,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  341,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  341,
  341,  341,  341,    0,  341,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  319,  319,  319,    0,  319,  319,
    0,   86,    0,  319,    0,  319,    0,    0,  319,  319,
  319,  319,  319,  319,  319,  319,  319,  319,    0,  319,
  319,    0,  319,  319,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  319,    0,    0,  319,  316,  316,
    0,  319,  319,  319,  319,  319,    0,  319,  319,  319,
  319,  319,  319,  319,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  319,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  319,  319,  319,
  319,    0,  319,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  316,  316,  316,    0,  316,
  316,    0,   86,    0,  316,    0,  316,    0,    0,  316,
  316,  316,  316,  316,  316,  316,  316,  316,  316,    0,
  316,  316,    0,  316,  316,  316,  316,  316,  316,  316,
  316,  316,  316,  316,  316,  316,  317,  317,  316,    0,
    0,    0,  316,  316,  316,  316,  316,    0,  316,  316,
  316,  316,  316,  316,  316,    0,  316,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  316,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  316,  316,
  316,  316,    0,  316,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  317,  317,  317,    0,  317,  317,    0,
   86,    0,  317,    0,  317,    0,    0,  317,  317,  317,
  317,  317,  317,  317,  317,  317,  317,    0,  317,  317,
    0,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  317,  317,  317,  317,  318,  318,  317,    0,    0,    0,
  317,  317,  317,  317,  317,    0,  317,  317,  317,  317,
  317,  317,  317,    0,  317,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  317,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  317,  317,  317,  317,
    0,  317,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  318,  318,  318,    0,  318,  318,    0,   86,    0,
  318,    0,  318,    0,    0,  318,  318,  318,  318,  318,
  318,  318,  318,  318,  318,    0,  318,  318,    0,  318,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  318,
  318,  318,    0,    0,  318,  353,  353,    0,  318,  318,
  318,  318,  318,    0,  318,  318,  318,  318,  318,  318,
  318,    0,  318,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  318,  318,  318,  318,    0,  318,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  353,  353,  353,    0,  353,  353,    0,   86,
    0,  353,    0,  353,    0,    0,  353,  353,  353,  353,
  353,  353,  353,  353,  353,  353,    0,  353,  353,    0,
  353,  353,  353,  353,  353,  353,  353,  353,  353,  353,
  353,  353,  353,  345,  345,  353,    0,    0,    0,  353,
  353,  353,  353,  353,    0,  353,  353,  353,  353,  353,
  353,  353,    0,  353,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  353,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  353,  353,  353,  353,    0,
  353,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  345,  345,  345,    0,  345,  345,    0,   86,    0,  345,
    0,  345,    0,    0,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,    0,  345,  345,    0,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  337,  337,  345,    0,    0,    0,  345,  345,  345,
  345,  345,    0,  345,  345,  345,  345,  345,  345,  345,
    0,  345,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  345,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  345,  345,  345,  345,    0,  345,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  337,  337,
  337,    0,  337,  337,    0,    0,    0,  337,    0,  337,
    0,    0,  337,  337,  337,  337,  337,  337,  337,  337,
  337,  337,    0,  337,  337,   91,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  337,    0,
    0,  337,  326,  326,    0,  337,  337,  337,  337,  337,
    0,  337,  337,  337,  337,  337,  337,  337,    0,  337,
   24,    0,    0,    0,    0,    0,    0,    0,   22,    0,
  337,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  337,  337,  337,  337,    0,  337,    0,    0,    0,
    0,    0,    0,    0,   91,    0,  240,    0,    0,  326,
  326,  326,    0,  326,  326,    0,    0,    0,  326,    0,
  326,    0,  169,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  326,    0,  326,  326,    0,  326,  326,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  326,  326,
  293,  293,  326,  170,   25,    0,  326,  326,  326,  326,
  326,  825,  326,  326,  326,  326,  326,  326,  326,    0,
  326,   91,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  326,    0,    0,    0,  168,    0,    0,    0,  169,
    0,    0,  326,  326,  326,  326,    0,  326,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  293,  293,  293,
    0,  293,  293,    0,    0,    0,  293,    0,  293,    0,
  170,  293,  293,  293,  293,  293,  293,  293,  293,  293,
  293,    0,  293,  293,    0,  293,  293,  293,  293,  293,
  293,  293,  293,  293,  293,  293,  293,  293,    0,   91,
  293,  351,  168,    0,  293,  293,  293,  293,  293,    0,
  293,  293,  293,  293,  293,  293,  293,  169,  293,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  293,
    0,    0,    0,    0,   91,   96,    0,    0,    0,    0,
  293,  293,  293,  293,    0,  293,   36,   37,  170,   38,
   39,    0,  169,   41,    0,   42,   43,   44,   45,   46,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  154,
  155,  156,    0,  157,  158,  159,    0,  160,    0,    0,
  168,    0,  169,  170,    0,  161,    0,    0,   24,   24,
    0,  162,   24,   24,   50,   24,   22,   22,  163,   50,
   22,   22,    0,   22,    0,    0,    0,    0,   24,   24,
    0,    0,    0,  170,    0,  168,   22,   22,    0,    0,
    0,    0,    0,    0,    0,   51,    0,  154,  155,  156,
   51,  157,  158,  159,    0,  160,  154,  155,  156,   24,
  157,  158,  159,    0,  160,  168,  375,   22,    0,  162,
  169,  253,  161,    0,    0,    0,  163,   49,  162,    0,
    0,    0,   49,    0,   50,  163,    0,    0,    0,    0,
    0,    0,   25,   25,    0,    0,   25,   25,    0,   25,
    0,  170,    0,    0,    0,  164,    0,    0,    0,    0,
  145,    0,   25,   25,    0,   51,    0,    0,    0,  165,
  166,  167,    0,    0,    0,    0,    0,   77,    0,   50,
    0,    0,    0,  168,  154,  155,  156,    0,  157,  158,
  159,    0,  160,   25,    0,    0,    0,   49,    0,    0,
  161,    0,    0,    0,    0,    0,  162,    0,    0,    0,
   51,    0,    0,  163,    0,    0,    0,    0,    0,  154,
  155,  156,  164,  157,  158,  159,    0,  160,    0,    0,
    0,    0,    0,    0,    0,  161,  165,  166,  167,   78,
    0,  162,   49,    0,    0,  478,    0,    0,  163,  154,
  155,  156,    0,  157,  158,  159,    0,  160,    0,    0,
    0,    0,    0,    0,  253,  161,    0,    0,   22,    0,
    0,  162,    0,   22,    0,    0,    0,   23,  163,    0,
    0,    0,   23,    0,   96,   24,   25,   26,   27,   28,
   24,   25,   26,   27,   28,   36,   37,    0,   38,   39,
  164,    0,   41,    0,   42,   43,   44,   45,   46,    0,
    0,    0,   77,    0,  165,  166,  167,  154,  155,  156,
    0,  157,  158,  159,    0,  160,    0,    0,   77,    0,
    0,    0,  253,  161,    0,  164,    0,    0,   22,  162,
    0,    0,    0,  580,    0,    0,  163,   23,    0,  165,
  166,  167,    0,    0,    0,   24,   25,   26,   27,   28,
    0,  410,    0,    0,    0,  164,  412,   77,   77,    0,
    0,    0,   77,   77,   78,   77,   77,   77,   77,  165,
  166,  167,    0,   22,    0,    0,    0,    0,    0,    0,
   78,    0,   23,  273,    0,    0,    0,  143,    0,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,  144,    0,    0,    0,  390,  391,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   78,
   78,    0,    0,  164,   78,   78,    0,   78,   78,   78,
   78,  231,    0,    0,    0,    0,    0,  165,  166,  167,
  274,  275,  276,    0,  277,  278,    0,    0,    0,  279,
    0,  280,    0,    0,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,    0,  291,  292,    0,  293,  294,
  295,  296,  297,  298,  299,  300,  301,  302,  303,  304,
  305,  273,    0,  306,    0,    0,    0,  307,  308,  309,
  310,  311,    0,  312,  313,  314,  315,  316,  317,  318,
    0,  319,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  320,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  321,  322,  323,  324,    0,  325,  229,
    0,    0,    0,    0,    0,    0,    0,    0,  274,  275,
  276,    0,  277,  278,    0,    0,    0,  279,    0,  280,
    0,    0,  281,  282,  283,  284,  285,  286,  287,  288,
  289,  290,    0,  291,  292,    0,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  231,
    0,  306,    0,    0,    0,  307,  308,  309,  310,  311,
    0,  312,  313,  314,  315,  316,  317,  318,    0,  319,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  320,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  321,  322,  323,  324,    0,  325,  232,    0,    0,
    0,    0,    0,    0,    0,    0,  231,  231,  231,    0,
  231,  231,    0,    0,    0,  231,    0,  231,    0,    0,
  231,  231,  231,  231,  231,  231,  231,  231,  231,  231,
    0,  231,  231,    0,  231,  231,  231,  231,  231,  231,
  231,  231,  231,  231,  231,  231,  231,  229,    0,  231,
    0,    0,    0,  231,  231,  231,  231,  231,    0,  231,
  231,  231,  231,  231,  231,  231,    0,  231,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  231,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  231,
  231,  231,  231,    0,  231,  230,    0,    0,    0,    0,
    0,    0,    0,    0,  229,  229,  229,    0,  229,  229,
    0,    0,    0,  229,    0,  229,    0,    0,  229,  229,
  229,  229,  229,  229,  229,  229,  229,  229,    0,  229,
  229,    0,  229,  229,  229,  229,  229,  229,  229,  229,
  229,  229,  229,  229,  229,  232,    0,  229,    0,    0,
    0,  229,  229,  229,  229,  229,    0,  229,  229,  229,
  229,  229,  229,  229,    0,  229,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  229,    0,    0,    0,
    0,    0,    0,   50,    0,    0,    0,  229,  229,  229,
  229,    0,  229,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  232,  232,  232,    0,  232,  232,    0,    0,
    0,  232,    0,  232,   51,    0,  232,  232,  232,  232,
  232,  232,  232,  232,  232,  232,    0,  232,  232,    0,
  232,  232,  232,  232,  232,  232,  232,  232,  232,  232,
  232,  232,  232,  230,   50,  232,   49,    0,    0,  232,
  232,  232,  232,  232,    0,  232,  232,  232,  232,  232,
  232,  232,    0,  232,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  232,   51,    0,    0,    0,    0,
    0,    0,  124,    0,    0,  232,  232,  232,  232,    0,
  232,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  230,  230,  230,    0,  230,  230,    0,   49,    0,  230,
    0,  230,    0,  124,  230,  230,  230,  230,  230,  230,
  230,  230,  230,  230,    0,  230,  230,    0,  230,  230,
  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,
  230,    0,    0,  230,    0,  124,  125,  230,  230,  230,
  230,  230,    0,  230,  230,  230,  230,  230,  230,  230,
    0,  230,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  230,    0,    0,    0,    0,  125,    0,    0,
    0,    0,    0,  230,  230,  230,  230,   22,  230,    0,
    0,    0,    0,    0,    0,    0,   23,  132,    0,    0,
    0,   50,    0,    0,   24,   25,   26,   27,   28,  125,
    0,    0,    0,    0,    0,   29,   50,    0,    0,    0,
   30,   31,   32,   33,   34,   35,   36,   37,    0,   38,
   39,   40,   51,   41,    0,   42,   43,   44,   45,   46,
    0,    0,    0,    0,    0,    0,   50,   51,   22,    0,
    0,   47,   48,    0,    0,    0,    0,   23,    0,    0,
    0,    0,    0,    0,   49,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,   96,   51,    0,   49,
    0,    0,   50,    0,    0,    0,  124,   36,   37,    0,
   38,   39,    0,    0,   41,  124,   42,   43,   44,   45,
   46,   97,    0,  124,  124,  124,  124,  124,    0,   49,
    0,    0,    0,   51,  124,    0,    0,    0,   50,    0,
    0,    0,    0,    0,  681,  124,  124,    0,  124,  124,
    0,    0,  124,    0,  124,  124,  124,  124,  124,  124,
   96,    0,    0,   50,    0,   49,    0,    0,    0,   51,
  125,   36,   37,    0,   38,   39,   50,    0,   41,  125,
   42,   43,   44,   45,   46,    0,    0,  125,  125,  125,
  125,  125,    0,    0,   51,    0,    0,    0,  125,   50,
    0,   49,    0,    0,    0,    0,    0,   51,    0,  125,
  125,    0,  125,  125,    0,    0,  125,    0,  125,  125,
  125,  125,  125,  125,    0,   22,   49,    0,    0,    0,
   51,    0,    0,    0,   23,    0,    0,    0,    0,   49,
   22,   50,   24,   25,   26,   27,   28,    0,    0,   23,
    0,    0,    0,   96,    0,    0,    0,   24,   25,   26,
   27,   28,   49,   35,   36,   37,    0,   38,   39,  131,
   22,   41,   51,   42,   43,   44,   45,   46,    0,   23,
   50,    0,    0,    0,    0,    0,    0,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,    0,   60,    0,
    0,    0,    0,    0,   49,   50,   22,    0,    0,   36,
   37,   51,   38,   39,    0,   23,   41,    0,   42,   43,
   44,   45,   46,   24,   25,   26,   27,   28,    0,    0,
    0,    0,    0,   50,   96,    0,   51,    0,    0,    0,
    0,    0,  184,   49,    0,   36,   37,   50,   38,   39,
    0,   23,   41,    0,   42,   43,   44,   45,   46,   24,
   25,   26,   27,   28,   51,    0,    0,   22,   49,    0,
  187,   50,    0,    0,    0,    0,   23,    0,   51,    0,
   22,  680,    0,  425,   24,   25,   26,   27,   28,   23,
    0,    0,    0,    0,  143,   50,   49,   24,   25,   26,
   27,   28,   51,   22,    0,    0,    0,    0,    0,  144,
   49,    0,   23,    0,    0,    0,    0,    0,    0,    0,
   24,   25,   26,   27,   28,    0,   51,    0,   50,    0,
    0,    0,  131,    0,   49,    0,   84,    0,  154,  155,
  156,  353,  157,  158,  159,   22,  354,    0,    0,    0,
    0,    0,    0,    0,   23,  355,    0,  356,   49,   51,
  162,  792,   24,   25,   26,   27,   28,  163,    0,   50,
    0,    0,    0,    0,    0,    0,   50,  154,  155,  156,
  353,  157,  158,  159,   22,  354,    0,    0,    0,    0,
    0,   49,    0,   23,  364,    0,  356,    0,    0,  162,
   51,   24,   25,   26,   27,   28,  163,   51,    0,  184,
  185,   50,    0,    0,    0,    0,    0,    0,   23,  186,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,   49,    0,    0,    0,    0,  184,  185,   49,
    0,    0,   51,    0,    0,    0,   23,  186,    0,    0,
    0,  184,  370,    0,   24,   25,   26,   27,   28,    0,
   23,  371,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,   49,   22,    0,    0,    0,    0,
    0,  154,  155,  156,   23,  157,  158,  159,    0,  160,
    0,    0,   24,   25,   26,   27,   28,    0,    0,   22,
  375,    0,    0,  162,    0,    0,    0,    0,   23,  747,
  163,    0,    0,  660,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,  686,  687,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
   22,    0,   23,    0,    0,    0,    0,  680,    0,   23,
   24,   25,   26,   27,   28,    0,    0,   24,   25,   26,
   27,   28,    0,    0,  733,  734,    0,  736,    0,    0,
    0,  336,  336,    0,    0,  184,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,    0,    0,    0,
    0,  756,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  336,  336,
  336,    0,  336,  336,    0,    0,    0,  336,  784,  336,
  786,  787,  336,  336,  336,  336,  336,  336,  336,  336,
  336,  336,    0,  336,  336,    0,  336,  336,  336,  336,
  336,  336,  336,  336,  336,  336,  336,  336,  336,  310,
  310,  336,    0,    0,    0,  336,  336,  336,  336,  336,
    0,  336,  336,  336,  336,  336,  336,  336,    0,  336,
    0,  822,    0,    0,    0,    0,    0,    0,    0,    0,
  336,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  336,  336,  336,  336,    0,  336,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  310,  310,  310,    0,
  310,  310,    0,    0,    0,  310,    0,  310,    0,    0,
  310,  310,  310,  310,  310,  310,  310,  310,  310,  310,
    0,  310,  310,    0,  310,  310,  310,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,  294,  294,  310,
    0,    0,    0,  310,  310,  310,  310,  310,    0,  310,
  310,  310,  310,  310,  310,  310,    0,  310,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  310,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  310,
  310,  310,  310,    0,  310,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  294,  294,  294,    0,  294,  294,
    0,    0,    0,  294,    0,  294,    0,    0,  294,  294,
  294,  294,  294,  294,  294,  294,  294,  294,    0,  294,
  294,    0,  294,  294,  294,  294,  294,  294,  294,  294,
  294,  294,  294,  294,  294,  299,  299,  294,    0,    0,
    0,  294,  294,  294,  294,  294,    0,  294,  294,  294,
  294,  294,  294,  294,    0,  294,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  294,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  294,  294,  294,
  294,    0,  294,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  299,  299,  299,    0,  299,  299,    0,    0,
    0,  299,    0,  299,    0,    0,  299,  299,  299,  299,
  299,  299,  299,  299,  299,  299,    0,  299,  299,    0,
  299,  299,  299,  299,  299,  299,  299,  299,  299,  299,
  299,  299,  299,  295,  295,  299,    0,    0,    0,  299,
  299,  299,  299,  299,    0,  299,  299,  299,  299,  299,
  299,  299,    0,  299,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  299,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  299,  299,  299,  299,    0,
  299,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  295,  295,  295,    0,  295,  295,    0,    0,    0,  295,
    0,  295,    0,    0,  295,  295,  295,  295,  295,  295,
  295,  295,  295,  295,    0,  295,  295,    0,  295,  295,
  295,  295,  295,  295,  295,  295,  295,  295,  295,  295,
  295,  304,  304,  295,    0,    0,    0,  295,  295,  295,
  295,  295,    0,  295,  295,  295,  295,  295,  295,  295,
    0,  295,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  295,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  295,  295,  295,  295,    0,  295,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  304,  304,
  304,    0,  304,  304,    0,    0,    0,  304,    0,  304,
    0,    0,  304,  304,  304,  304,  304,  304,  304,  304,
  304,  304,    0,  304,  304,    0,  304,  304,  304,  304,
  304,  304,  304,  304,  304,  304,  304,  304,  304,  325,
  325,  304,    0,    0,    0,  304,  304,  304,  304,  304,
    0,  304,  304,  304,  304,  304,  304,  304,    0,  304,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  304,  304,  304,  304,    0,  304,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  325,  325,  325,    0,
  325,  325,    0,    0,    0,  325,    0,  325,    0,    0,
  325,  325,  325,  325,  325,  325,  325,  325,  325,  325,
    0,  325,  325,    0,  325,  325,  325,  325,  325,  325,
  325,  325,  325,  325,  325,  325,  325,  321,  321,  325,
    0,    0,    0,  325,  325,  325,  325,  325,    0,  325,
  325,  325,  325,  325,  325,  325,    0,  325,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  325,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  325,
  325,  325,  325,    0,  325,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  321,  321,  321,    0,  321,  321,
    0,    0,    0,  321,    0,  321,    0,    0,  321,  321,
  321,  321,  321,  321,  321,  321,  321,  321,    0,  321,
  321,    0,  321,  321,  321,  321,  321,  321,  321,  321,
  321,  321,  321,  321,  321,  300,  300,  321,    0,    0,
    0,  321,  321,  321,  321,  321,    0,  321,  321,  321,
  321,  321,  321,  321,    0,  321,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  321,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  321,  321,  321,
  321,    0,  321,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  300,  300,  300,    0,  300,  300,    0,    0,
    0,  300,    0,  300,    0,    0,  300,  300,  300,  300,
  300,  300,  300,  300,  300,  300,    0,  300,  300,    0,
  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  300,  300,  300,  296,  296,  300,    0,    0,    0,  300,
  300,  300,  300,  300,    0,  300,  300,  300,  300,  300,
  300,  300,    0,  300,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  300,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  300,  300,  300,  300,    0,
  300,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  296,  296,  296,    0,  296,  296,    0,    0,    0,  296,
    0,  296,    0,    0,  296,  296,  296,  296,  296,  296,
  296,  296,  296,  296,    0,  296,  296,    0,  296,  296,
  296,  296,  296,  296,  296,  296,  296,  296,  296,  296,
  296,  297,  297,  296,    0,    0,    0,  296,  296,  296,
  296,  296,    0,  296,  296,  296,  296,  296,  296,  296,
    0,  296,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  296,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  296,  296,  296,  296,    0,  296,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  297,  297,
  297,    0,  297,  297,    0,    0,    0,  297,    0,  297,
    0,    0,  297,  297,  297,  297,  297,  297,  297,  297,
  297,  297,    0,  297,  297,    0,  297,  297,  297,  297,
  297,  297,  297,  297,  297,  297,  297,  297,  297,  301,
  301,  297,    0,    0,    0,  297,  297,  297,  297,  297,
    0,  297,  297,  297,  297,  297,  297,  297,    0,  297,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  297,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  297,  297,  297,  297,    0,  297,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  301,  301,  301,    0,
  301,  301,    0,    0,    0,  301,    0,  301,    0,    0,
  301,  301,  301,  301,  301,  301,  301,  301,  301,  301,
    0,  301,  301,    0,  301,  301,  301,  301,  301,  301,
  301,  301,  301,  301,  301,  301,  301,  302,  302,  301,
    0,    0,    0,  301,  301,  301,  301,  301,    0,  301,
  301,  301,  301,  301,  301,  301,    0,  301,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  301,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  301,
  301,  301,  301,    0,  301,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  302,  302,  302,    0,  302,  302,
    0,    0,    0,  302,    0,  302,    0,    0,  302,  302,
  302,  302,  302,  302,  302,  302,  302,  302,    0,  302,
  302,    0,  302,  302,  302,  302,  302,  302,  302,  302,
  302,  302,  302,  302,  302,  305,  305,  302,    0,    0,
    0,  302,  302,  302,  302,  302,    0,  302,  302,  302,
  302,  302,  302,  302,    0,  302,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  302,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  302,  302,  302,
  302,    0,  302,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  305,  305,  305,    0,  305,  305,    0,    0,
    0,  305,    0,  305,    0,    0,  305,  305,  305,  305,
  305,  305,  305,  305,  305,  305,    0,  305,  305,    0,
  305,  305,  305,  305,  305,  305,  305,  305,  305,  305,
  305,  305,  305,  322,  322,  305,    0,    0,    0,  305,
  305,  305,  305,  305,    0,  305,  305,  305,  305,  305,
  305,  305,    0,  305,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  305,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  305,  305,  305,  305,    0,
  305,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  322,  322,  322,    0,  322,  322,    0,    0,    0,  322,
    0,  322,    0,    0,  322,  322,  322,  322,  322,  322,
  322,  322,  322,  322,    0,  322,  322,    0,  322,  322,
  322,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  298,  298,  322,    0,    0,    0,  322,  322,  322,
  322,  322,    0,  322,  322,  322,  322,  322,  322,  322,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  322,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,  322,  322,  322,    0,  322,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  298,  298,
  298,    0,  298,  298,    0,    0,    0,  298,    0,  298,
    0,    0,  298,  298,  298,  298,  298,  298,  298,  298,
  298,  298,    0,  298,  298,    0,  298,  298,  298,  298,
  298,  298,  298,  298,  298,  298,  298,  298,  298,  303,
  303,  298,    0,    0,    0,  298,  298,  298,  298,  298,
    0,  298,  298,  298,  298,  298,  298,  298,    0,  298,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  298,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  298,  298,  298,  298,    0,  298,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  303,  303,  303,    0,
  303,  303,    0,    0,    0,  303,    0,  303,    0,    0,
  303,  303,  303,  303,  303,  303,  303,  303,  303,  303,
    0,  303,  303,    0,  303,  303,  303,  303,  303,  303,
  303,  303,  303,  303,  303,  303,  303,  306,  306,  303,
    0,    0,    0,  303,  303,  303,  303,  303,    0,  303,
  303,  303,  303,  303,  303,  303,    0,  303,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  303,
  303,  303,  303,    0,  303,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  306,  306,  306,    0,  306,  306,
    0,    0,    0,  306,    0,  306,    0,    0,  306,  306,
  306,  306,  306,  306,  306,  306,  306,  306,    0,  306,
  306,    0,  306,  306,  306,  306,  306,  306,  306,  306,
  306,  306,  306,  306,  306,  307,  307,  306,    0,    0,
    0,  306,  306,  306,  306,  306,    0,  306,  306,  306,
  306,  306,  306,  306,    0,  306,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  306,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  306,  306,  306,
  306,    0,  306,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  307,  307,  307,    0,  307,  307,    0,    0,
    0,  307,    0,  307,    0,    0,  307,  307,  307,  307,
  307,  307,  307,  307,  307,  307,    0,  307,  307,    0,
  307,  307,  307,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  307,  308,  308,  307,    0,    0,    0,  307,
  307,  307,  307,  307,    0,  307,  307,  307,  307,  307,
  307,  307,    0,  307,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  307,  307,  307,  307,    0,
  307,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  308,  308,  308,    0,  308,  308,    0,    0,    0,  308,
    0,  308,    0,    0,  308,  308,  308,  308,  308,  308,
  308,  308,  308,  308,    0,  308,  308,    0,  308,  308,
  308,  308,  308,  308,  308,  308,  308,  308,  308,  308,
  308,  273,    0,  308,    0,    0,    0,  308,  308,  308,
  308,  308,    0,  308,  308,  308,  308,  308,  308,  308,
    0,  308,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  308,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  308,  308,  308,  308,    0,  308,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  274,  275,
  276,    0,  277,  278,    0,    0,    0,  279,    0,  280,
    0,    0,  281,  282,  283,  284,  285,  286,  287,  288,
  289,  290,    0,  291,  292,    0,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  235,
    0,  306,    0,    0,    0,  307,  308,  309,  310,  311,
    0,  312,  313,  314,  315,  316,  317,  318,    0,  319,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  320,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  321,  322,  323,  324,    0,  325,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  235,  235,  235,    0,
  235,  235,    0,    0,    0,  235,    0,  235,    0,    0,
  235,  235,  235,  235,  235,  235,  235,  235,  235,  235,
    0,  235,  235,    0,  235,  235,  235,  235,  235,  235,
  235,  235,  235,  235,  235,  235,  235,  236,    0,  235,
    0,    0,    0,  235,  235,  235,  235,  235,    0,  235,
  235,  235,  235,  235,  235,  235,    0,  235,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  235,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  235,
  235,  235,  235,    0,  235,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  236,  236,  236,    0,  236,  236,
    0,    0,    0,  236,    0,  236,    0,    0,  236,  236,
  236,  236,  236,  236,  236,  236,  236,  236,    0,  236,
  236,    0,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,  236,  236,  236,  237,    0,  236,    0,    0,
    0,  236,  236,  236,  236,  236,    0,  236,  236,  236,
  236,  236,  236,  236,    0,  236,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  236,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  236,  236,  236,
  236,    0,  236,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  237,  237,  237,    0,  237,  237,    0,    0,
    0,  237,    0,  237,    0,    0,  237,  237,  237,  237,
  237,  237,  237,  237,  237,  237,    0,  237,  237,    0,
  237,  237,  237,  237,  237,  237,  237,  237,  237,  237,
  237,  237,  237,  238,    0,  237,    0,    0,    0,  237,
  237,  237,  237,  237,    0,  237,  237,  237,  237,  237,
  237,  237,    0,  237,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  237,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  237,  237,  237,  237,    0,
  237,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  238,  238,  238,    0,  238,  238,    0,    0,    0,  238,
    0,  238,    0,    0,  238,  238,  238,  238,  238,  238,
  238,  238,  238,  238,    0,  238,  238,    0,  238,  238,
  238,  238,  238,  238,  238,  238,  238,  238,  238,  238,
  238,    0,    0,  238,    0,    0,    0,  238,  238,  238,
  238,  238,    0,  238,  238,  238,  238,  238,  238,  238,
    0,  238,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  238,    0,    0,    0,    0,    0,    0,    0,
  277,    0,    0,  238,  238,  238,  238,  280,  238,    0,
  281,  282,  283,  284,  285,  286,  287,  288,  289,  290,
    0,  291,  292,    0,  293,  294,  295,  296,  297,  298,
  299,  300,  301,  302,  303,  304,  305,    0,    0,  306,
    0,    0,    0,  307,  308,  309,  310,  311,    0,  312,
  313,  314,  315,  316,  317,  318,    0,  319,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  320,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  321,
  322,  323,  324,    0,  325,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   42,   42,    6,  326,   57,  499,   33,   60,  108,
  221,   40,  152,   33,  123,  123,  123,   40,  679,  123,
  123,   54,  123,   44,  740,  123,   40,  123,   40,   40,
  276,   44,   44,   44,  280,  103,  200,   61,   60,   44,
   40,  205,   49,   60,  549,   42,  111,  290,  291,   56,
   57,  150,   41,   60,  318,   44,   63,  773,   19,   92,
   63,  261,  262,  203,   60,  243,  244,  245,   75,   91,
  138,   44,   44,  141,   91,  318,  329,  282,  331,  284,
   44,  221,   89,  309,  745,   61,  291,   94,  266,    6,
    7,   98,  318,  100,  146,  102,   93,  100,  262,  102,
  107,  123,   42,  202,  125,  112,  123,  114,  173,   42,
  175,  302,  125,  125,  125,  309,   77,   78,  125,  126,
  125,  128,   62,  263,  318,   61,  337,  123,  374,   93,
  266,  636,   61,  201,  639,  124,  260,  328,  329,   56,
   57,   44,    0,   60,  860,  272,   63,  108,  374,  285,
  257,   60,  125,  125,  509,   42,   61,   44,   91,   62,
    0,  280,   76,  485,   78,  274,  274,  174,  379,  176,
  274,  274,   41,  274,  239,   44,  274,  242,  274,  357,
  374,   98,   91,  100,   61,  102,  393,   40,   41,   40,
  397,   44,  199,  400,  394,  395,  396,  397,  257,  113,
   40,  115,  409,  403,  404,  405,  406,  407,  408,  409,
  410,  411,  412,  220,  123,  218,  257,  258,  259,   42,
  261,  262,  263,   41,  265,  277,   44,  480,   61,  290,
  291,  272,  273,  294,  295,   40,  243,  244,  279,  492,
   40,  596,  264,  272,  277,  286,    0,  264,  274,  272,
  260,  273,  505,  274,  274,   40,  273,  277,  272,  281,
  282,  283,  284,  285,  281,  282,  283,  284,  285,  274,
  277,  311,  272,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  291,  292,  293,  294,  295,  296,
  323,  287,  260,   40,   41,  302,  303,   44,  123,  306,
   41,  794,   41,   44,   41,   44,    0,   44,   41,   41,
  122,   44,   44,  257,  321,  322,  323,  267,  325,   41,
  323,  499,   44,  501,   41,  503,   41,   44,  267,   44,
  685,  338,  373,  586,    0,  588,  388,  374,   41,   41,
  357,   44,   44,   42,  302,   44,  387,  388,  389,   41,
  123,   42,   44,   44,  376,  264,  168,  169,  170,   42,
  277,   44,  270,  271,  273,  543,   41,  123,   42,   40,
   44,  183,  281,  282,  283,  284,  285,  290,  291,  419,
  387,  388,    0,   42,  387,   44,  393,   42,  743,   44,
  397,   41,   42,  400,  294,  295,  403,   41,   42,  754,
  755,   41,  409,  410,  123,  412,  323,   41,   42,  257,
  268,  269,  318,  225,  272,  273,  423,  275,  230,  231,
  232,  674,  429,  676,   41,   42,   41,   42,  268,  269,
  288,  289,  272,   41,   42,  275,  791,  375,  169,  170,
  252,   40,   40,  476,   44,   40,  453,   58,  288,  289,
  805,   58,   58,  808,   40,  274,  257,   62,   61,   40,
   44,  319,  274,  470,  276,  374,  278,  474,  475,  476,
  387,  388,  475,  476,   44,  374,  374,  832,  833,  319,
  260,  488,  489,  490,   61,  297,  298,  299,  300,  301,
   42,  846,  304,  257,  501,  307,  308,  309,  310,  311,
  312,  313,  314,  315,  316,  317,  318,  514,   60,  274,
  327,  514,  274,  415,  268,  269,  309,  382,  272,  273,
  382,  275,  382,  257,  576,  374,   44,  257,  374,  374,
   44,  343,   40,  124,  288,  289,  274,  715,  716,   91,
  547,   44,  549,   44,  273,  552,  553,  554,  555,  556,
  557,  558,  559,  560,  561,  562,  563,  474,  475,  476,
   44,  660,   44,   44,   44,  319,  573,   44,  575,  576,
  748,  123,  382,  189,  268,  269,  382,  257,  272,  273,
    0,  275,  257,  258,  259,   40,  261,  262,  263,    0,
  265,  598,  732,  382,  288,  289,  382,  514,  382,  382,
  382,  276,  268,  269,  279,   60,  272,  273,  382,  275,
  382,  286,  382,  425,  382,  382,  794,    0,  257,  374,
  257,  374,  288,  289,  266,  319,   44,   40,   44,  636,
  682,  257,  639,  257,  733,  734,   91,  736,   44,   44,
   44,   44,  782,  783,   44,  785,   44,   44,   44,   44,
  268,  269,   44,  319,  272,  273,  573,  275,  575,  576,
  667,  257,  258,  259,  667,  261,  262,  263,  123,  265,
  288,  289,  679,  680,   44,   44,   44,   44,   44,  819,
  820,  821,   44,  279,   44,  784,   44,  786,  787,   44,
  286,   44,    0,  382,   44,   44,   44,   91,  838,   44,
   44,  319,   44,   44,   44,  257,  258,  259,   44,  261,
  262,  263,   44,  265,  257,   44,   44,   44,   44,   44,
  272,  273,   44,  822,   44,   44,   91,  279,  540,  541,
  542,   44,  544,  545,  286,  382,  382,   44,  745,  551,
  292,  374,  374,   44,  374,  752,  749,   44,   44,   44,
  667,  303,  304,   40,  306,  307,  322,  382,  310,  257,
  312,  313,  314,  315,  316,  374,  578,  383,  257,  581,
  374,  257,   44,  375,   40,   93,  392,   40,   40,  395,
  396,  322,  398,  399,  322,  401,  402,  257,  404,  405,
  406,  407,  408,  257,   40,  411,    0,  413,  414,  415,
  416,  322,  257,  258,  259,   44,  261,  262,  263,  382,
  265,  382,    0,  382,   40,  272,  272,  272,  273,  272,
   40,  373,  272,  272,  279,   10,   79,  183,  182,  641,
   19,  286,  247,  840,   60,  387,  388,  389,  501,  216,
   89,  848,  849,  850,  335,  244,   54,  659,  268,  269,
  816,  715,  272,  273,  328,  275,  472,  268,  269,  220,
  199,  272,  273,  328,  275,   91,  716,  380,  288,  289,
  752,  658,  503,  745,  380,  268,  269,  288,  289,  272,
  273,    0,  275,   -1,   60,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,  288,  289,  123,   -1,  319,
  516,  713,  714,   -1,  520,  288,  289,  523,  319,   -1,
  526,   -1,   -1,   -1,   -1,   91,  532,  533,  373,  535,
   -1,   40,   41,   42,   -1,   44,  319,   -1,  125,   -1,
   -1,   -1,  387,  388,  389,   -1,  319,   -1,   -1,   -1,
   -1,   60,   -1,   62,  473,   -1,   -1,  123,  564,  565,
  566,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  268,  269,  774,   -1,  272,  273,   -1,  275,   -1,  781,
   -1,   -1,   91,   -1,   93,   -1,   -1,   -1,   -1,   -1,
  288,  289,   -1,  512,  513,   -1,   -1,  603,   -1,  605,
  606,   -1,  608,  609,   -1,  611,  612,   -1,  614,  615,
  616,  617,  618,   -1,  123,  621,  125,  623,  624,  625,
  626,  319,   -1,   -1,   -1,   -1,  828,  829,  830,   40,
   41,  637,   -1,   44,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
  852,   62,  571,  572,   -1,  574,  272,  273,   -1,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,   -1,   -1,  260,  268,  269,  682,   60,  272,  273,
   -1,  275,   93,  602,  690,   -1,   -1,  693,   -1,  276,
  696,   -1,   -1,  699,  288,  289,   -1,   -1,  264,  705,
  706,   -1,  708,   -1,   -1,   -1,   -1,  273,   91,   -1,
   -1,   -1,  123,   -1,  125,  281,  282,  283,  284,  285,
   -1,  727,  728,  729,   -1,  319,  260,   40,  315,  316,
   -1,   -1,   -1,  320,  321,   -1,  323,  324,  325,  326,
  123,   -1,  276,  749,  663,   -1,  665,  666,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  373,   -1,  268,
  269,   -1,   -1,  272,  273,  274,  275,   -1,   -1,   42,
  279,  387,  388,  389,   -1,   -1,   -1,  286,   -1,  288,
  289,  315,  316,  292,   -1,   -1,  320,  321,   -1,  323,
  324,  325,  326,   -1,  303,  304,   -1,  306,  307,   -1,
   -1,  310,  311,  312,  313,  314,  315,  316,   -1,  375,
  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  739,  330,  331,  332,   -1,  334,  335,   -1,   40,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,  264,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,  402,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,  413,  414,  415,  416,   -1,  418,
  419,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,  305,  334,  335,   -1,   40,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,  382,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,  402,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,  279,  418,   -1,   -1,
   -1,   -1,   -1,  286,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   40,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,   -1,   -1,
  373,  273,  274,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  290,  291,   -1,  402,
  294,  295,  296,  297,  298,  299,  300,  301,   -1,   -1,
  413,  414,  415,  416,  308,  418,  392,  393,  394,  395,
  396,  397,  398,  399,  400,  401,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   40,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   40,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,  274,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   40,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,   -1,   -1,  373,  273,  274,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   40,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,  274,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   40,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   40,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,   -1,   -1,  373,  273,  274,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   40,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   40,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   42,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,   -1,
   -1,  373,  273,  274,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,    0,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   42,   -1,   44,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   60,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   91,    0,   -1,  377,  378,  379,  380,
  381,   41,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,  123,   -1,   -1,   -1,   60,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   91,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,   -1,   42,
  373,   44,  123,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   60,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   42,  292,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,  303,  304,   91,  306,
  307,   -1,   60,  310,   -1,  312,  313,  314,  315,  316,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
  123,   -1,   60,   91,   -1,  273,   -1,   -1,  268,  269,
   -1,  279,  272,  273,   60,  275,  268,  269,  286,   60,
  272,  273,   -1,  275,   -1,   -1,   -1,   -1,  288,  289,
   -1,   -1,   -1,   91,   -1,  123,  288,  289,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   91,   -1,  257,  258,  259,
   91,  261,  262,  263,   -1,  265,  257,  258,  259,  319,
  261,  262,  263,   -1,  265,  123,  276,  319,   -1,  279,
   60,  272,  273,   -1,   -1,   -1,  286,  123,  279,   -1,
   -1,   -1,  123,   -1,   60,  286,   -1,   -1,   -1,   -1,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,   91,   -1,   -1,   -1,  373,   -1,   -1,   -1,   -1,
   41,   -1,  288,  289,   -1,   91,   -1,   -1,   -1,  387,
  388,  389,   -1,   -1,   -1,   -1,   -1,  125,   -1,   60,
   -1,   -1,   -1,  123,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,  319,   -1,   -1,   -1,  123,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   91,   -1,   -1,  286,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,  373,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  387,  388,  389,  125,
   -1,  279,  123,   -1,   -1,  125,   -1,   -1,  286,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,  264,   -1,
   -1,  279,   -1,  264,   -1,   -1,   -1,  273,  286,   -1,
   -1,   -1,  273,   -1,  292,  281,  282,  283,  284,  285,
  281,  282,  283,  284,  285,  303,  304,   -1,  306,  307,
  373,   -1,  310,   -1,  312,  313,  314,  315,  316,   -1,
   -1,   -1,  260,   -1,  387,  388,  389,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,  276,   -1,
   -1,   -1,  272,  273,   -1,  373,   -1,   -1,  264,  279,
   -1,   -1,   -1,  125,   -1,   -1,  286,  273,   -1,  387,
  388,  389,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,  357,   -1,   -1,   -1,  373,  357,  315,  316,   -1,
   -1,   -1,  320,  321,  260,  323,  324,  325,  326,  387,
  388,  389,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
  276,   -1,  273,  273,   -1,   -1,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  293,   -1,   -1,   -1,  342,  343,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  315,
  316,   -1,   -1,  373,  320,  321,   -1,  323,  324,  325,
  326,  125,   -1,   -1,   -1,   -1,   -1,  387,  388,  389,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,   -1,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
   -1,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,   -1,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,   -1,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   91,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,   60,  373,  123,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   91,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,  123,   -1,  339,
   -1,  341,   -1,   91,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,   -1,   -1,  373,   -1,  123,   60,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,  264,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   41,   -1,   -1,
   -1,   60,   -1,   -1,  281,  282,  283,  284,  285,  123,
   -1,   -1,   -1,   -1,   -1,  292,   60,   -1,   -1,   -1,
  297,  298,  299,  300,  301,  302,  303,  304,   -1,  306,
  307,  308,   91,  310,   -1,  312,  313,  314,  315,  316,
   -1,   -1,   -1,   -1,   -1,   -1,   60,   91,  264,   -1,
   -1,  328,  329,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   -1,   -1,  292,   91,   -1,  123,
   -1,   -1,   60,   -1,   -1,   -1,  264,  303,  304,   -1,
  306,  307,   -1,   -1,  310,  273,  312,  313,  314,  315,
  316,  317,   -1,  281,  282,  283,  284,  285,   -1,  123,
   -1,   -1,   -1,   91,  292,   -1,   -1,   -1,   60,   -1,
   -1,   -1,   -1,   -1,   41,  303,  304,   -1,  306,  307,
   -1,   -1,  310,   -1,  312,  313,  314,  315,  316,  317,
  292,   -1,   -1,   60,   -1,  123,   -1,   -1,   -1,   91,
  264,  303,  304,   -1,  306,  307,   60,   -1,  310,  273,
  312,  313,  314,  315,  316,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   91,   -1,   -1,   -1,  292,   60,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   91,   -1,  303,
  304,   -1,  306,  307,   -1,   -1,  310,   -1,  312,  313,
  314,  315,  316,  317,   -1,  264,  123,   -1,   -1,   -1,
   91,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  123,
  264,   60,  281,  282,  283,  284,  285,   -1,   -1,  273,
   -1,   -1,   -1,  292,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  123,  302,  303,  304,   -1,  306,  307,  293,
  264,  310,   91,  312,  313,  314,  315,  316,   -1,  273,
   60,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,  292,   -1,
   -1,   -1,   -1,   -1,  123,   60,  264,   -1,   -1,  303,
  304,   91,  306,  307,   -1,  273,  310,   -1,  312,  313,
  314,  315,  316,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,   -1,   60,  292,   -1,   91,   -1,   -1,   -1,
   -1,   -1,  264,  123,   -1,  303,  304,   60,  306,  307,
   -1,  273,  310,   -1,  312,  313,  314,  315,  316,  281,
  282,  283,  284,  285,   91,   -1,   -1,  264,  123,   -1,
  125,   60,   -1,   -1,   -1,   -1,  273,   -1,   91,   -1,
  264,  278,   -1,  305,  281,  282,  283,  284,  285,  273,
   -1,   -1,   -1,   -1,  278,   60,  123,  281,  282,  283,
  284,  285,   91,  264,   -1,   -1,   -1,   -1,   -1,  293,
  123,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   91,   -1,   60,   -1,
   -1,   -1,  293,   -1,  123,   -1,  125,   -1,  257,  258,
  259,  260,  261,  262,  263,  264,  265,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,  274,   -1,  276,  123,   91,
  279,   93,  281,  282,  283,  284,  285,  286,   -1,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   60,  257,  258,  259,
  260,  261,  262,  263,  264,  265,   -1,   -1,   -1,   -1,
   -1,  123,   -1,  273,  274,   -1,  276,   -1,   -1,  279,
   91,  281,  282,  283,  284,  285,  286,   91,   -1,  264,
  265,   60,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  123,   -1,   -1,   -1,   -1,  264,  265,  123,
   -1,   -1,   91,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,  264,  265,   -1,  281,  282,  283,  284,  285,   -1,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,  123,  264,   -1,   -1,   -1,   -1,
   -1,  257,  258,  259,  273,  261,  262,  263,   -1,  265,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,  264,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,  273,  274,
  286,   -1,   -1,  570,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,  600,  601,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,  273,
  281,  282,  283,  284,  285,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,  661,  662,   -1,  664,   -1,   -1,
   -1,  273,  274,   -1,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,  688,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,  735,  341,
  737,  738,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,  788,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,  274,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,  274,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,  274,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,  274,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,  274,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,  274,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,  274,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,  274,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,   -1,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
   -1,  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,  273,   -1,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,   -1,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,   -1,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,   -1,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,   -1,   -1,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  334,   -1,   -1,  413,  414,  415,  416,  341,  418,   -1,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,   -1,   -1,  373,
   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,   -1,  418,
  };

#line 1278 "Iril/IR/IR.jay"

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
  public const int FASTCC = 302;
  public const int SIGNEXT = 303;
  public const int ZEROEXT = 304;
  public const int VOLATILE = 305;
  public const int RETURNED = 306;
  public const int DEREFERENCEABLE = 307;
  public const int AVAILABLE_EXTERNALLY = 308;
  public const int PERSONALITY = 309;
  public const int SRET = 310;
  public const int CLEANUP = 311;
  public const int NONNULL = 312;
  public const int NOCAPTURE = 313;
  public const int WRITEONLY = 314;
  public const int READONLY = 315;
  public const int READNONE = 316;
  public const int HIDDEN = 317;
  public const int ATTRIBUTE_GROUP_REF = 318;
  public const int ATTRIBUTES = 319;
  public const int NORECURSE = 320;
  public const int NOUNWIND = 321;
  public const int UNWIND = 322;
  public const int SPECULATABLE = 323;
  public const int SSP = 324;
  public const int UWTABLE = 325;
  public const int ARGMEMONLY = 326;
  public const int SEQ_CST = 327;
  public const int DSO_LOCAL = 328;
  public const int DSO_PREEMPTABLE = 329;
  public const int RET = 330;
  public const int BR = 331;
  public const int SWITCH = 332;
  public const int INDIRECTBR = 333;
  public const int INVOKE = 334;
  public const int RESUME = 335;
  public const int CATCHSWITCH = 336;
  public const int CATCHRET = 337;
  public const int CLEANUPRET = 338;
  public const int UNREACHABLE = 339;
  public const int FNEG = 340;
  public const int ADD = 341;
  public const int NUW = 342;
  public const int NSW = 343;
  public const int FADD = 344;
  public const int SUB = 345;
  public const int FSUB = 346;
  public const int MUL = 347;
  public const int FMUL = 348;
  public const int UDIV = 349;
  public const int SDIV = 350;
  public const int FDIV = 351;
  public const int UREM = 352;
  public const int SREM = 353;
  public const int FREM = 354;
  public const int SHL = 355;
  public const int LSHR = 356;
  public const int EXACT = 357;
  public const int ASHR = 358;
  public const int AND = 359;
  public const int OR = 360;
  public const int XOR = 361;
  public const int EXTRACTELEMENT = 362;
  public const int INSERTELEMENT = 363;
  public const int SHUFFLEVECTOR = 364;
  public const int EXTRACTVALUE = 365;
  public const int INSERTVALUE = 366;
  public const int ALLOCA = 367;
  public const int LOAD = 368;
  public const int STORE = 369;
  public const int FENCE = 370;
  public const int CMPXCHG = 371;
  public const int ATOMICRMW = 372;
  public const int GETELEMENTPTR = 373;
  public const int ALIGN = 374;
  public const int INBOUNDS = 375;
  public const int INRANGE = 376;
  public const int TRUNC = 377;
  public const int ZEXT = 378;
  public const int SEXT = 379;
  public const int FPTRUNC = 380;
  public const int FPEXT = 381;
  public const int TO = 382;
  public const int FPTOUI = 383;
  public const int FPTOSI = 384;
  public const int UITOFP = 385;
  public const int SITOFP = 386;
  public const int PTRTOINT = 387;
  public const int INTTOPTR = 388;
  public const int BITCAST = 389;
  public const int ADDRSPACECAST = 390;
  public const int ICMP = 391;
  public const int EQ = 392;
  public const int NE = 393;
  public const int UGT = 394;
  public const int UGE = 395;
  public const int ULT = 396;
  public const int ULE = 397;
  public const int SGT = 398;
  public const int SGE = 399;
  public const int SLT = 400;
  public const int SLE = 401;
  public const int FCMP = 402;
  public const int OEQ = 403;
  public const int OGT = 404;
  public const int OGE = 405;
  public const int OLT = 406;
  public const int OLE = 407;
  public const int ONE = 408;
  public const int ORD = 409;
  public const int UEQ = 410;
  public const int UNE = 411;
  public const int UNO = 412;
  public const int PHI = 413;
  public const int SELECT = 414;
  public const int CALL = 415;
  public const int TAIL = 416;
  public const int VA_ARG = 417;
  public const int LANDINGPAD = 418;
  public const int CATCH = 419;
  public const int CATCHPAD = 420;
  public const int CLEANUPPAD = 421;
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
