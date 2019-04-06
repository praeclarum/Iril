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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-5+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 19:
#line 126 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 130 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 21:
#line 134 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 22:
#line 138 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 23:
#line 142 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 24:
#line 146 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 25:
#line 150 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 26:
#line 154 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 27:
#line 158 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 28:
#line 162 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 29:
#line 166 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 30:
#line 170 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-4+yyTop], (LType)yyVals[0+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-2+yyTop], isConstant: (bool)yyVals[-1+yyTop]);
    }
  break;
case 31:
#line 174 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 32:
#line 175 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 33:
#line 179 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 34:
#line 180 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 35:
#line 181 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 36:
#line 182 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 37:
#line 183 "Iril/IR/IR.jay"
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
#line 192 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 41:
  case_41();
  break;
case 42:
  case_42();
  break;
case 43:
#line 209 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 44:
#line 210 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 45:
#line 211 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 46:
#line 215 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 47:
#line 219 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 58:
#line 248 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 59:
#line 252 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 60:
#line 259 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 61:
#line 263 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
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
case 64:
#line 275 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 82:
#line 308 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 83:
#line 312 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 84:
#line 316 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 85:
#line 323 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 327 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 332 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 91:
#line 338 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 92:
#line 339 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 93:
#line 340 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 94:
#line 341 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 95:
#line 345 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 96:
#line 349 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 353 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 98:
#line 357 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-3+yyTop], 0);
    }
  break;
case 99:
#line 361 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 100:
#line 365 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 369 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 102:
#line 376 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 103:
#line 380 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 388 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 106:
  case_106();
  break;
case 107:
  case_107();
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
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 119:
#line 428 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 120:
#line 432 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 121:
#line 436 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
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
case 124:
#line 451 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 129:
#line 462 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 130:
#line 469 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-2+yyTop], (GlobalSymbol)yyVals[-1+yyTop], (IEnumerable<Parameter>)yyVals[0+yyTop]);
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
case 134:
#line 485 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 138:
#line 495 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 139:
#line 496 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 140:
#line 503 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 141:
#line 507 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 142:
#line 514 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 143:
#line 518 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 144:
#line 522 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 145:
#line 526 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 147:
#line 534 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 538 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 149:
#line 539 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 150:
#line 540 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 151:
#line 541 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 152:
#line 542 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 153:
#line 543 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 154:
#line 544 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 155:
#line 545 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 156:
#line 546 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 157:
#line 547 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 158:
#line 551 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 166:
#line 574 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 167:
#line 575 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 168:
#line 576 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 169:
#line 577 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 170:
#line 578 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 171:
#line 579 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 172:
#line 580 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 173:
#line 581 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 174:
#line 582 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 175:
#line 583 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 176:
#line 587 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 177:
#line 588 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 178:
#line 589 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 179:
#line 590 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 180:
#line 591 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 181:
#line 592 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 182:
#line 593 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 183:
#line 594 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 184:
#line 595 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 185:
#line 596 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 186:
#line 597 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 187:
#line 598 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 188:
#line 599 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 189:
#line 600 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 190:
#line 601 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 191:
#line 602 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 192:
#line 606 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 196:
#line 616 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 197:
#line 620 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 198:
#line 624 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 199:
#line 628 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 200:
#line 632 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 201:
#line 636 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 202:
#line 640 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 203:
#line 644 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 205:
#line 652 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 206:
#line 653 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 207:
#line 654 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 208:
#line 655 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 209:
#line 656 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 210:
#line 657 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 211:
#line 658 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 212:
#line 659 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 213:
#line 660 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 214:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 674 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 678 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 217:
#line 685 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 692 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 696 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 703 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 711 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 223:
#line 718 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 722 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 733 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 737 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 744 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 748 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 759 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 232:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 767 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (Assignment)yyVals[-1+yyTop]);
    }
  break;
case 234:
#line 774 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 778 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 785 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 789 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 793 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 797 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 805 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 242:
#line 806 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 243:
#line 813 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 817 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 824 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 246:
#line 828 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 247:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 248:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 249:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 250:
#line 844 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 251:
#line 848 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 253:
#line 853 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 254:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 255:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 256:
#line 865 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 257:
#line 869 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 262:
#line 886 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 890 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 896 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 265:
#line 903 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 907 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 914 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 932 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 274:
#line 939 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment (LocalSymbol.None, (Instruction)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 943 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 950 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 954 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 958 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 962 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 966 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 281:
#line 970 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
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
#line 990 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
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
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1006 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 290:
#line 1010 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 291:
#line 1014 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1018 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 293:
#line 1022 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 294:
#line 1026 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 297:
#line 1038 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 298:
#line 1042 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 302:
#line 1058 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 303:
#line 1062 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 304:
#line 1066 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 307:
#line 1078 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 308:
#line 1082 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 309:
#line 1086 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 310:
#line 1090 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1094 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractValueInstruction ((TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1098 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1102 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1106 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1110 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1114 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1118 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1122 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1126 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1130 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1134 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1146 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1150 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1154 "Iril/IR/IR.jay"
  {
        yyVal = new InsertValueInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<Value>)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1158 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1162 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 329:
#line 1166 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1170 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 331:
#line 1174 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 332:
#line 1178 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 333:
#line 1182 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 334:
#line 1186 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 337:
#line 1198 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 338:
#line 1202 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 341:
#line 1214 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 342:
#line 1218 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 345:
#line 1230 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 346:
#line 1234 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 347:
#line 1238 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 348:
#line 1242 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 349:
#line 1246 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
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
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 352:
#line 1258 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 353:
#line 1262 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 354:
#line 1266 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 355:
#line 1270 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 356:
#line 1274 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 357:
#line 1278 "Iril/IR/IR.jay"
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

void case_41()
#line 197 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_42()
#line 202 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_106()
#line 393 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_107()
#line 398 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,    6,
   11,   11,   10,   10,   10,   10,   10,   10,   16,   17,
    9,    9,   18,   18,   18,   18,   18,   19,   22,   22,
   23,   24,   24,   24,   24,   24,   24,   14,   14,    8,
    8,    8,    8,    8,   26,   26,   26,    7,    7,   28,
   28,   28,   28,   28,   28,   28,   28,   28,   28,   28,
   28,    3,    3,    3,   29,   29,   30,   30,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   31,   31,   32,   32,    4,    4,   35,   35,   35,
   35,   35,   35,   35,   35,   35,   33,   33,   33,   33,
   33,   40,   40,   40,   40,   40,   40,   40,   38,    5,
    5,    5,    5,    5,   44,   44,   44,   34,   34,   45,
   45,   46,   46,   46,   46,   41,   41,   39,   39,   39,
   39,   39,   39,   39,   39,   39,   39,   39,   15,   15,
   42,   42,   37,   37,   47,   48,   48,   48,   48,   48,
   48,   48,   48,   48,   48,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   50,   51,   51,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   54,   20,   20,   20,   20,   20,   20,
   20,   20,   20,   55,   27,   27,   56,   53,   53,   25,
   57,   57,   52,   52,   58,   59,   59,   36,   36,   60,
   60,   60,   60,   61,   61,   63,   63,   63,   63,   65,
   66,   66,   67,   67,   68,   68,   68,   68,   68,   68,
   68,   69,   69,   69,   69,   69,   69,   21,   21,   70,
   70,   71,   71,   72,   73,   73,   74,   75,   75,   76,
   76,   43,   77,   62,   62,   78,   78,   78,   78,   78,
   78,   78,   79,   79,   79,   79,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,   64,   64,   64,
   64,   64,   64,   64,   64,   64,   64,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    6,    9,   10,
   10,    7,   11,    9,   10,   11,    9,   10,    8,    5,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    3,    6,    5,    1,    1,    3,
    1,    1,    1,    1,    1,    1,    1,    2,    3,    1,
    2,    3,    3,    3,    1,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    3,    1,    1,    1,
    4,    2,    3,    5,    1,    3,    1,    1,    1,    1,
    1,    1,    1,    1,    3,    4,    2,    4,    1,    5,
    5,    1,    3,    1,    1,    7,    8,    1,    2,    4,
    3,    5,    1,    3,    2,    4,    2,    3,    3,    4,
    4,    1,    1,    1,    1,    2,    3,    2,    2,    4,
    5,    6,    6,    7,    1,    2,    1,    3,    2,    1,
    3,    1,    2,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    4,    1,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    6,    9,    6,    6,
    3,    3,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    2,    1,    2,    1,    3,    2,
    1,    2,    1,    3,    1,    1,    3,    1,    2,    2,
    3,    1,    2,    1,    2,    1,    2,    3,    4,    1,
    3,    2,    1,    3,    2,    3,    3,    3,    2,    4,
    5,    1,    1,    6,    9,    6,    6,    1,    3,    1,
    1,    1,    3,    5,    1,    2,    3,    1,    2,    1,
    1,    1,    1,    1,    3,    2,    7,    2,    2,    7,
    1,    1,    8,    9,    9,   10,    5,    6,    5,    7,
    5,    5,    6,    4,    4,    5,    6,    6,    7,    5,
    6,    6,    6,    7,    5,    6,    7,    7,    8,    4,
    4,    5,    6,    5,    2,    5,    4,    4,    4,    4,
    5,    6,    7,    6,    6,    6,    4,    3,    4,    7,
    8,    5,    6,    5,    5,    6,    3,    4,    5,    6,
    7,    4,    5,    6,    6,    4,    5,    7,    8,    5,
    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   88,   99,   91,   92,   93,   94,   90,  122,   35,
   33,   36,   37,   38,  272,  153,  154,  155,    0,   34,
  156,  148,  149,  151,  150,  152,  161,  162,    0,    0,
    0,   89,    0,    0,    0,    0,    0,  123,  124,    0,
    0,  146,    0,    0,    3,    0,    4,    0,    0,  159,
  160,   31,   32,   39,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   82,    0,    0,    0,    0,    0,    0,
    0,  128,    0,    0,    0,  157,   40,    0,    0,    0,
    0,    0,    0,    0,  147,    0,    0,    0,    5,    6,
    0,    0,    0,    0,    0,    8,    0,    7,    0,    0,
    0,    0,    0,   83,    0,    0,    0,    0,  127,    0,
  105,   95,    0,    0,  102,    0,    0,    0,    0,    0,
    0,    0,  144,  145,  139,    0,    0,  140,  165,    0,
    0,    0,  163,    0,    0,  207,  208,  206,  209,  210,
  211,  205,  196,  213,  212,    0,    0,    0,    0,    0,
    0,    0,    0,  195,    0,    0,    0,    0,    0,    0,
   41,    0,    0,    0,   67,   66,   13,    0,    0,   60,
   65,  158,    0,    0,    0,    0,   98,   96,    0,    0,
    0,    0,    0,  131,    0,    0,    0,   80,   72,   70,
   71,   73,   74,   75,   76,    0,   68,    0,  138,    0,
    0,    0,    0,    0,    0,    0,  115,  164,    0,    0,
    0,    0,    0,    0,    0,  218,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   61,
   14,    0,  192,  194,  193,  215,  100,   84,  101,  103,
  132,    0,    0,  133,    0,    0,   12,   69,  141,    0,
  111,   58,    0,    0,    0,    0,    0,    0,  281,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  228,    0,    0,  234,
    0,  274,  282,    0,    0,  129,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  203,    0,  201,  202,    0,
    0,    0,   54,   57,    0,   52,    0,   43,   55,    0,
   49,   51,   56,   53,   44,   45,   42,   17,   16,   64,
   63,   62,  134,   77,  261,  260,    0,  258,    0,    0,
  279,    0,    0,  276,    0,    0,    0,    0,  278,  270,
  271,    0,    0,  268,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  273,  315,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  166,  167,  168,  169,  170,  171,  172,  173,
  174,  175,    0,  176,  177,  188,  189,  190,  191,  179,
  181,  182,  183,  184,  180,  178,  186,  187,  185,    0,
    0,    0,    0,    0,    0,    0,    0,  106,  229,    0,
  235,    0,    0,   59,    0,  116,   29,    0,    0,    0,
    0,    0,    0,    0,  219,    0,    0,    0,    0,  220,
    0,   81,    0,  112,    0,  275,  214,    0,    0,  240,
    0,    0,    0,    0,    0,    0,  269,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  262,    0,    0,
    0,    0,    0,    0,    0,    0,  328,    0,    0,  107,
    0,   24,    0,    0,    0,    0,    0,    0,    0,    0,
   47,    0,   50,  259,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  310,    0,    0,  225,
  226,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  329,    0,    0,
    0,  200,  197,  199,    0,    0,   46,    0,    0,    0,
  242,    0,    0,  243,    0,    0,    0,    0,  287,    0,
  312,  350,    0,  321,  335,    0,  316,  353,    0,  339,
  314,  355,  347,  343,    0,    0,  332,    0,  292,  291,
  334,  356,    0,    0,    0,    0,  289,    0,    0,  204,
  217,    0,    0,    0,    0,    0,    0,    0,    0,  263,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  265,    0,    0,    0,  245,
  241,    0,    0,    0,    0,    0,  288,  351,  336,  340,
  344,  333,  293,  325,  345,  227,    0,    0,    0,    0,
    0,    0,    0,    0,  221,    0,  223,  324,  313,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  277,
    0,  280,  266,    0,  253,  247,    0,    0,    0,    0,
  252,  248,  246,  244,    0,    0,    0,    0,  290,    0,
  330,    0,  348,    0,  222,    0,  264,  341,    0,    0,
    0,    0,  198,  267,  250,    0,    0,    0,    0,    0,
  283,    0,    0,    0,  331,  349,  224,    0,  251,    0,
    0,    0,    0,  284,  285,    0,    0,    0,    0,    0,
  286,    0,    0,    0,    0,    0,  257,  254,  256,    0,
    0,  255,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  216,  188,  180,   53,
   76,  189,  254,  224,  202,   78,   98,  181,  358,  174,
  377,  360,  361,  362,  363,  190,  775,  217,   86,   87,
  134,  135,   15,  108,  151,  326,  203,  227,   62,   57,
   63,   58,   59,  204,  147,  148,  153,  453,  470,  255,
  510,  776,  237,  721,  384,  638,  777,  631,  632,  327,
  328,  329,  330,  331,  511,  599,  683,  684,  802,  378,
  567,  568,  745,  746,  393,  394,  428,  332,  333,
  };
  protected static readonly short [] yySindex = {          921,
   11,  -88,   28,   32,   43, 3320, 3583, -276,    0,  921,
    0,    0,    0,    0, -224, -128,  112,  138, 1124,  -46,
  -26,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  185,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3895, -104,
  -27,    0, -151,  222,  228, 3995, 3381,    0,    0, 3619,
  -32,    0, 3619,  211,    0,  259,    0,   46,   91,    0,
    0,    0,    0,    0,   55, 3995, -127, -101,    8,  324,
  -19,  251,  123,    0,  222,   27,  228,  119, 3995,  133,
   82,    0,   14, 2562,  228,    0,    0, 3995,  228, 3619,
  -22, 3619,  259,  -14,    0,  279, 2701, -228,    0,    0,
 3995, -127, 2536, 3995, -127,    0,  282,    0, -231,  367,
  288, 3855,  381,    0, 3995, 3995,   30, 3995,    0,  181,
    0,    0,  222,  121,    0,  228,  228,  259,  -12, -228,
  259, 2458,    0,    0,    0,  656,  165,    0,    0,  107,
 -108, -279,    0, 2382, 3995,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   56,  400,  401,  403, 4000,
 4000, 4000,  402,    0, 2536, 3995,  387,  391,  392,  174,
    0, -231, 3861,    0,    0,    0,    0,  -13, 2452,    0,
    0,    0,  222,   92,  382,   10,    0,    0, 3748, -228,
  259,  107,  107,    0, -228,  390,  415,    0,    0,    0,
    0,    0,    0,    0,    0, -113,    0, 4030,    0, 3723,
 -188,  188, 5996, -103, 4000,  202,    0,    0,   89,  421,
 2486,  426, 4000, 4000, 4000,    0,   34,  117,   42,   93,
  427, 2536, 3789, 3819, 1051,    0, -231,  180,   -8,    0,
    0, 3890,    0,    0,    0,    0,    0,    0,    0,    0,
    0, -228,  107,    0,  212,  241,    0,    0,    0,  219,
    0,    0,  414, 4000, -179, 4000, 3538, 4000,    0,  815,
 3995,  815, 3995,  815, 3995, 3995, 2546, 3995, 3995, 3995,
  815, 2569, 2663, 3995, 3995, 3995, 4000, 4000, 4000, 4000,
 4000, 3995, 3553, 3655,  151,  662, 4000, 4000, 4000, 4000,
 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 1222, 3920,
 3995, 3995, 3538,   64, 3995, 2677,    0, 5996,  206,    0,
  206,    0,    0,  215, 5996,    0,  175,  233,  111,  122,
  450, 3995,  115,  125,  142,    0, 4000,    0,    0,  244,
  139,  461,    0,    0,  483,    0,  853,    0,    0,  404,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  182,    0,  175, 6464,
    0,  253, 2418,    0,  485,  -16, 3619, 3619,    0,    0,
    0, 2452,  815,    0, 2452, 2452,  815, 2452, 2452,  815,
 2452, 2452, 3995, 2452, 2452, 2452, 2452, 2452,  815, 3995,
 2452, 3995, 2452, 2452, 2452, 2452,  486,  487,  488,  494,
  495,   40, 3995,   74, 4000,  496,    0,    0, 3995,  210,
  162,  163,  176,  177,  179,  184,  193,  194,  196,  204,
  205,  207,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3995,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3995,
   57, 2452,  -16, 3995, 3619, 3538,  -20,    0,    0,  206,
    0,  268,  268,    0, 2775,    0,    0,  306,  320,  214,
  250, 3995, 3995, 3995,    0,  206,  327,  216, 2694,    0,
 3819,    0,  241,    0,  206,    0,    0,  549,  328,    0,
  555,  -16,  -16, 3619,  552, 2452,    0,  553,  554, 2452,
  556,  557, 2452,  560,  564, 2452,  565,  566,  567,  568,
  570, 2452, 2452,  572, 2452,  574,  575,  576,  577, 4000,
 4000, 4000, 1051, 4000,  298,  269, 3995,  578, 3995,  276,
 4000, 3995, 3995, 3995, 3995, 3995, 3995, 3995, 3995, 3995,
 3995, 3995, 3995, 2452, 2452, 2418,  579,    0,  580,  555,
  -16,  -16, 3995,  -16, 3995, 3619,    0, 4000,  268,    0,
  206,    0,  368, 4000,  318,  337,  373,  268,  206,  372,
    0,  195,    0,    0,  268,  328,  540, 3710,  277,  555,
  555,  -16, 2418,  591, 2418, 2418,  614, 2418, 2418,  616,
 2418, 2418,  618, 2418, 2418, 2418, 2418, 2418,  619,  620,
 2418,  623, 2418, 2418, 2418, 2418,    0,  624,  625,    0,
    0,  626,  627,  418,  634, 3995, 2452,  638, 3995,  639,
 4000,  644,  222,  222,  222,  222,  222,  222,  222,  222,
  222,  222,  222,  222,  645,  646,  649,  605, 4000,  107,
  555,  555,  -16,  555,  -16,  -16, 3619,    0,  268,  206,
  653,    0,    0,    0,  268,  206,    0,  655, 3995, 3919,
    0,  543,  218,    0,  328,  321,  323,  555,    0, 2418,
    0,    0, 2418,    0,    0, 2418,    0,    0, 2418,    0,
    0,    0,    0,    0, 2418, 2418,    0, 2418,    0,    0,
    0,    0, 4000, 4000, 1051, 1051,    0,  334,  658,    0,
    0,  335,  666,  338,  667,  252, 2418, 2418, 2418,    0,
  674,  107,  107,  107,  555,  107,  555,  555,  -16,  268,
  252,  268,  328,  675, 3943,    0,  684,  104, 2604,    0,
    0, 3965,  405,  328,  328,  343,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  626,  469,  354,  472,
  359,  477,  252, 4000,    0,  691,    0,    0,    0,  647,
 4000,  107,  107,  107,  107,  107,  107,  555,  281,    0,
  328,    0,    0, 3686,    0,    0,  363,  701,  703,  704,
    0,    0,    0,    0,  328,  423,  428,  328,    0,  491,
    0,  497,    0,  691,    0,  252,    0,    0,  107,  107,
  107,  107,    0,    0,    0,  294,  712, 4000, 4000, 4000,
    0,  328,  328,  433,    0,    0,    0,  107,    0, 3995,
  374,  375,  380,    0,    0,  328,  326, 3995, 3995, 3995,
    0, 4000,  378,  386,  393,  714,    0,    0,    0,  252,
  303,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  763,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3419,   69,  493,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   20,    0,    0,    0,    0,    0,
 3483,    0,  913,    0,  498,    0,    0,    0,  501,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  726,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  160,    0,    0,  507,  514,    0,    0,   35,
    0,    0,    0,    0,    0,  312,    0,    0,    0, -100,
    0,  -98,    0,  141,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  726,    0,    0,    0,    0,    0,
    0,    0,    0, 1015,    0,    0,    0,    0,  726,    0,
    0,    0,   25,  726,    0,  726,    0,    0,    0,    0,
    0,  164,  412,    0,    0, 3981, 4032,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  351,    0,    0,
  -94,    0,    0,    0,    0,    0,    0,    0,    0,  448,
  726,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  726,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  599,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 2873,    0,
 6094,    0,    0,    0,    0,    0,  -90,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  632,    0,    0,   15,    0,  726,    0,    0,  352,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -89,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  726,    0,    0,  726,  726,    0,  726,  726,    0,
  726,  726,    0,  726,  726,  726,  726,  726,    0,    0,
  726,    0,  726,  726,  726,  726,    0,    0,    0,    0,
    0,  726,    0,  726,    0,    0,    0,    0,    0,  726,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  726,  726,    0,    0,    0,    0,  726,    0,    0, 2971,
    0, 3069, 6192,    0,    0,    0,    0,    0,    0,    0,
  726,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 6290,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  726,    0,    0,    0,  726,
    0,    0,  726,    0,    0,  726,    0,    0,    0,    0,
    0,  726,  726,    0,  726,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  726,    0,    0,    0,  726,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  726,  726,    0, 4036,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3167,    0,
  742,    0,    0,    0,  726,  726,  726,  751,    0,    0,
    0,    0,    0,    0, 6388,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4134,    0,    0,    0,    0,  726,    0,    0,    0,
    0,    0, 1113, 1211, 1312, 1410, 1508, 1609, 1707, 1805,
 1906, 2004, 2102, 2203,    0,    0,    0,    0,    0, 4232,
    0,    0,    0,    0,    0,    0,    0,    0,  776,  818,
    0,    0,    0,    0,  854,  876,    0,    0,    0,    0,
    0,  726,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4330,    0, 4428,    0, 4526,    0,    0,    0, 1050,
    0, 1249,    0,    0,    0,    0,  357,  726,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4624,    0,    0,    0,
    0,    0,    0,    0,    0, 4722,    0,    0,    0,    0,
    0, 4820, 4918, 5016, 5114, 5212, 5310,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5408,    0,    0,    0,    0, 5506, 5604,
 5702, 5800,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5898,    0,    0,
    0,    0,    0,    0,    0,    0,  726,    0,    0,    0,
    0,    0,  726,  726,  726,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  759,  709,    0,    0,    0,    0,  610,  612,  777,
  120,   -6,  -73, -252,   36,    0,    0,  548,  559, -234,
 -493,    0,  308,    0, -632,    0,  339,  583,  718,  131,
    0,  611,    0,  -65,    0,  478,  -92, -204,   -2,    0,
  -55,  764,  -50, -120,    0,  592, -106,    0,    0,    0,
  376, -720,  168,    0,    6, -512,    3,  105,  108, -313,
    0,  499,  500,  441, -446, 3551,    0,   71,    0,  322,
    0,  172,    0,   81,  -86,  -95,    0,    0,  451,
  };
  protected static readonly short [] yyTable = {            54,
   54,  100,   92,   56,  102,  592,   82,   94,  359,  359,
  366,  267,  479,  121,  223,  152,  271,   94,   89,  335,
  789,   93,  108,   94,  113,   94,  570,   94,  109,  225,
  252,  376,  114,  110,  130,  252,  640,  140,  149,  173,
  129,   64,   85,  171,  177,  228,  744,   66,  178,   54,
   54,   93,  814,   54,   77,   53,   54,  221,   53,   87,
  105,   70,   71,   85,   87,  600,  601,  117,   86,  113,
  125,   16,  200,  125,  172,  205,  482,  347,  483,  261,
  230,   93,   85,  545,  264,  347,  382,  133,   19,  149,
  218,   54,   20,   54,  226,   54,  228,  105,   93,  105,
  146,  241,  259,   21,  154,  383,  170,  175,   87,  263,
  112,  251,  744,  115,  228,   93,  369,  547,  193,  194,
  225,  196,  500,  719,  661,  662,  723,  664,   87,  149,
   49,   67,  486,   93,  349,  262,   55,   61,   53,  861,
   30,  373,  179,  150,   85,   93,  206,  566,  231,   86,
   35,  124,   88,  257,  195,  688,  228,  341,  346,   87,
  347,  198,  207,  135,  199,  222,   72,   73,  352,  242,
  334,  479,   68,  108,  504,  113,   47,   48,  348,  109,
   87,   17,   18,  114,  110,  270,   95,   99,   70,   71,
  101,   87,  133,  104,  111,  397,  114,  400,   69,   87,
  104,  208,  209,  104,  409,  219,  210,  211,  220,  212,
  213,  214,  215,  146,  246,  105,  735,  247,  737,  738,
  368,  387,  502,  247,   83,  503,  388,  579,  136,   90,
  137,  155,  139,   79,  176,  677,  357,  357,  503,  103,
  156,  157,  158,  588,  159,  160,  161,   80,  162,  138,
   81,   93,  595,  551,  120,  253,  163,  141,  751,  201,
  250,  752,  164,   93,  376,  250,  359,   94,  376,  165,
   54,  106,  475,  392,  395,  396,  398,  399,  401,  402,
  404,  405,  406,  407,  408,  411,  413,  414,  415,  416,
  577,   93,  788,  584,  116,  422,  424,  517,  107,  430,
  826,  517,  130,  130,  517,  109,  130,  130,  630,  130,
   93,   50,  636,  517,  471,  472,   54,   93,  477,  641,
  474,  823,  130,  130,  816,   87,   87,   87,  669,   87,
   87,   87,  514,   87,  839,  491,  675,  503,  238,  239,
   87,   87,   51,  862,   70,   71,  816,   87,   72,   73,
  110,   87,  142,  130,   87,  142,  166,   50,  672,   93,
  156,  157,  158,  119,  159,  160,  161,   93,  162,  852,
  167,  168,  169,  122,   49,  795,  796,  673,   93,  123,
   54,   54,  164,   35,  105,  126,  516,  130,   51,  165,
  520,  143,   48,  523,  143,   48,  526,  249,  578,  128,
  249,  142,  532,  533,   89,  535,  182,  386,   30,   30,
  183,  137,   30,  674,   93,   30,  546,  740,  857,   93,
   49,  192,  550,  742,  149,  576,  858,   93,   30,   30,
  232,  135,  135,  859,   93,  135,  135,  197,  135,  233,
  234,   87,  235,  258,  243,  240,  564,   18,  244,  245,
  265,  135,  135,  473,  266,   87,   87,   87,  337,   30,
  191,  272,  338,  565,  339,  342,  350,   54,   54,   54,
  351,  374,  573,  575,  380,  379,  797,  427,  476,  222,
  630,  630,  135,  225,  488,  585,  586,  587,  484,  487,
  798,  799,  800,  490,  357,  489,  492,  156,  157,  158,
  496,  159,  160,  161,  498,  162,  493,   54,  236,  236,
  236,  105,  497,  801,  597,  184,  375,  512,  513,  164,
  667,  191,  499,  494,   23,  507,  165,  501,  509,  540,
  541,  542,   24,   25,   26,   27,   28,  543,  544,  549,
  637,  334,  637,  552,  553,  643,  644,  645,  646,  647,
  648,  649,  650,  651,  652,  653,  654,  554,  555,  376,
  556,  184,  581,  336,  256,  557,   54,  732,   54,   54,
   23,  343,  344,  345,  558,  559,  582,  560,   24,   25,
   26,   27,   28,  589,   93,  561,  562,  583,  563,  590,
  372,  682,  596,  382,  598,  603,  605,  606,  136,  608,
  609,  678,  171,  611,  571,  572,  574,  612,  614,  615,
  616,  617,  381,  618,  385,  621,  389,  623,  624,  625,
  626,  639,  658,  659,  670,  228,  749,  774,  676,  637,
  679,   22,  637,  172,  690,  417,  418,  419,  420,  421,
  782,  783,  426,  785,  602,  431,  432,  433,  434,  435,
  436,  437,  438,  439,  440,  441,  442,  693,  685,  696,
   54,  699,  705,  706,  105,  170,  708,  713,  714,  715,
  716,  634,  357,  748,  717,  228,  228,  718,  228,  137,
  137,  722,  724,  137,  137,  495,  137,  726,  727,  728,
  753,  819,  729,  820,  821,  566,  741,   93,  743,  137,
  137,  769,  754,  663,  755,  665,  666,  768,  770,  771,
  773,  772,  228,  228,  228,   18,   18,  781,  791,   18,
   18,   50,   18,  794,  808,  809,  805,  810,  811,  838,
  137,  228,  812,  813,  816,   18,   18,  827,  357,  817,
  828,   27,  829,  830,  832,  682,  105,  835,  790,  833,
   19,  840,   51,  836,  846,  848,  849,  860,  508,  806,
  807,  850,    1,  548,  117,   87,   18,  515,   65,  118,
  518,  519,  119,  521,  522,   28,  524,  525,  120,  527,
  528,  529,  530,  531,   49,  121,  534,  118,  536,  537,
  538,  539,  249,  248,  367,   75,  824,  739,  268,  156,
  157,  158,  365,  159,  160,  161,  127,  162,  593,  260,
  831,  269,  485,  834,  253,  163,   91,   25,  837,  766,
  505,  164,  804,  767,  594,  793,  480,  481,  165,  730,
  506,    0,    0,  847,   96,    0,    0,  844,  845,    0,
    0,  853,  854,  855,    0,   36,   37,  569,   38,   39,
    0,  851,   41,   20,   42,   43,   44,   45,   46,    0,
    0,    0,    0,    0,    0,    0,  136,  136,    0,    0,
  136,  136,    0,  136,   50,   21,    0,    0,  627,  628,
  629,    0,  633,  635,    0,    0,  136,  136,    0,  642,
    0,  604,    0,    0,   93,  607,    0,    0,  610,   22,
   22,  613,    0,   22,   22,   51,   22,  619,  620,    0,
  622,    0,   97,    0,    0,  166,  668,  136,    0,   22,
   22,    0,  671,    0,    0,   22,    0,    0,    0,  167,
  168,  169,    0,    0,   23,    0,    0,   49,    0,  655,
  656,  657,   24,   25,   26,   27,   28,   96,    0,    0,
   22,    0,   97,   97,   97,    0,   97,    0,   36,   37,
    0,   38,   39,    0,    0,   41,    0,   42,   43,   44,
   45,   46,   97,    0,   97,    0,    0,    0,  689,  725,
  691,  692,    0,  694,  695,    0,  697,  698,    0,  700,
  701,  702,  703,  704,    0,    0,  707,  731,  709,  710,
  711,  712,    0,   97,    0,   97,    0,    0,    0,   27,
   27,    0,  720,   27,   27,    0,   27,    0,   19,   19,
    0,    0,   19,   19,    0,   19,    0,    0,    0,   27,
   27,    0,    0,    0,    0,   97,  429,   97,   19,   19,
    0,    0,    0,   28,   28,    0,    0,   28,   28,   26,
   28,  764,  765,    0,   88,  216,    0,  750,  216,    0,
   27,    0,    0,   28,   28,  757,    0,    0,  758,   19,
    0,  759,    0,    0,  760,    0,  216,    0,   22,    0,
  761,  762,    0,  763,    0,   25,   25,   23,    0,   25,
   25,    0,   25,    0,   28,   24,   25,   26,   27,   28,
    0,    0,  778,  779,  780,   25,   25,  216,    0,  156,
  157,  158,  815,  159,  160,  161,    0,  162,    0,  818,
    0,   20,   20,    0,  803,   20,   20,    0,   20,    0,
    0,  164,    0,    0,    0,    0,   25,  216,  165,  216,
    0,   20,   20,   21,   21,    0,    0,   21,   21,    0,
   21,    0,   87,    0,    0,    0,  390,  391,    0,    0,
    0,    0,    0,   21,   21,    0,  841,  842,  843,   97,
   97,   97,   20,   97,   97,   97,    0,   97,    0,    0,
   97,   97,    0,    0,   97,   97,   97,   97,    1,    2,
  856,   97,    3,    4,   21,    5,    0,    0,   97,    0,
   97,   97,    0,    0,   97,    0,    0,    0,    6,    7,
    0,    0,    0,    0,    0,   97,   97,    0,   97,   97,
    0,    0,   97,   97,   97,   97,   97,   97,   97,    0,
    0,   97,    0,    0,    0,    0,    0,    0,    0,    8,
    0,    0,   97,   97,   97,    0,   97,   97,   23,    0,
   87,   97,    0,   97,    0,    0,   97,   97,   97,   97,
   97,   97,   97,   97,   97,   97,    0,   97,   97,    0,
   97,   97,   97,   97,   97,   97,   97,   97,   97,   97,
   97,   97,   97,    0,    0,   97,    0,  216,  216,   97,
   97,   97,   97,   97,    0,   97,   97,   97,   97,   97,
   97,   97,    0,   97,    0,    0,    0,  156,  157,  158,
    0,  159,  160,  161,   97,  162,    0,   26,   26,    0,
    0,   26,   26,    0,   26,   97,   97,   97,   97,  164,
   97,   97,    0,    0,    0,    0,  165,   26,   26,    0,
    0,    0,    0,    0,  216,  216,  216,    0,  216,  216,
    0,   87,    0,  216,    0,  216,    0,    0,  216,  216,
  216,  216,  216,  216,  216,  216,  216,  216,   26,  216,
  216,    0,  216,  216,  216,  216,  216,  216,  216,  216,
  216,  216,  216,  216,  216,  352,  352,  216,    0,    0,
    0,  216,  216,  216,  216,  216,  216,  216,  216,  216,
  216,  216,  216,  216,    0,  216,    0,    0,    0,    0,
    0,    0,    0,   70,   71,    0,  216,   72,   73,   74,
   30,   31,   32,   33,   34,    0,    0,  216,  216,  216,
  216,   40,  216,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  352,  352,  352,    0,  352,  352,    0,   87,
    0,  352,    0,  352,    0,    0,  352,  352,  352,  352,
  352,  352,  352,  352,  352,  352,    0,  352,  352,    0,
  352,  352,  352,  352,  352,  352,  352,  352,  352,  352,
  352,  352,  352,  357,  357,  352,    0,    0,    0,  352,
  352,  352,  352,  352,    0,  352,  352,  352,  352,  352,
  352,  352,    0,  352,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  352,    0,   23,   23,    0,    0,
   23,   23,    0,   23,    0,  352,  352,  352,  352,    0,
  352,    0,    0,    0,    0,    0,   23,   23,    0,    0,
  357,  357,  357,    0,  357,  357,    0,   87,    0,  357,
    0,  357,    0,    0,  357,  357,  357,  357,  357,  357,
  357,  357,  357,  357,    0,  357,  357,   23,  357,  357,
  357,  357,  357,  357,  357,  357,  357,  357,  357,  357,
  357,    0,    0,  357,  342,  342,    0,  357,  357,  357,
  357,  357,    0,  357,  357,  357,  357,  357,  357,  357,
    0,  357,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  357,  443,  444,  445,  446,  447,  448,  449,
  450,  451,  452,  357,  357,  357,  357,    0,  357,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  342,  342,  342,    0,  342,  342,    0,   87,    0,
  342,    0,  342,    0,    0,  342,  342,  342,  342,  342,
  342,  342,  342,  342,  342,    0,  342,  342,    0,  342,
  342,  342,  342,  342,  342,  342,  342,  342,  342,  342,
  342,  342,  320,  320,  342,    0,    0,    0,  342,  342,
  342,  342,  342,    0,  342,  342,  342,  342,  342,  342,
  342,    0,  342,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  342,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  342,  342,  342,  342,    0,  342,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  320,
  320,  320,    0,  320,  320,    0,   87,    0,  320,    0,
  320,    0,    0,  320,  320,  320,  320,  320,  320,  320,
  320,  320,  320,    0,  320,  320,    0,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,  320,  320,  320,
  317,  317,  320,    0,    0,    0,  320,  320,  320,  320,
  320,    0,  320,  320,  320,  320,  320,  320,  320,    0,
  320,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  320,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  320,  320,  320,  320,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  317,  317,  317,
    0,  317,  317,    0,   87,    0,  317,    0,  317,    0,
    0,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  317,    0,  317,  317,    0,  317,  317,  317,  317,  317,
  317,  317,  317,  317,  317,  317,  317,  317,    0,    0,
  317,  318,  318,    0,  317,  317,  317,  317,  317,    0,
  317,  317,  317,  317,  317,  317,  317,    0,  317,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  317,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  317,  317,  317,  317,    0,  317,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  318,  318,
  318,    0,  318,  318,    0,   87,    0,  318,    0,  318,
    0,    0,  318,  318,  318,  318,  318,  318,  318,  318,
  318,  318,    0,  318,  318,    0,  318,  318,  318,  318,
  318,  318,  318,  318,  318,  318,  318,  318,  318,  319,
  319,  318,    0,    0,    0,  318,  318,  318,  318,  318,
    0,  318,  318,  318,  318,  318,  318,  318,    0,  318,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  318,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  318,  318,  318,  318,    0,  318,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  319,  319,  319,    0,
  319,  319,    0,   87,    0,  319,    0,  319,    0,    0,
  319,  319,  319,  319,  319,  319,  319,  319,  319,  319,
    0,  319,  319,    0,  319,  319,  319,  319,  319,  319,
  319,  319,  319,  319,  319,  319,  319,  354,  354,  319,
    0,    0,    0,  319,  319,  319,  319,  319,    0,  319,
  319,  319,  319,  319,  319,  319,    0,  319,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  319,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  319,
  319,  319,  319,    0,  319,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  354,  354,  354,    0,  354,  354,
    0,   87,    0,  354,    0,  354,    0,    0,  354,  354,
  354,  354,  354,  354,  354,  354,  354,  354,    0,  354,
  354,    0,  354,  354,  354,  354,  354,  354,  354,  354,
  354,  354,  354,  354,  354,    0,    0,  354,  346,  346,
    0,  354,  354,  354,  354,  354,    0,  354,  354,  354,
  354,  354,  354,  354,    0,  354,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  354,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  354,  354,  354,
  354,    0,  354,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  346,  346,  346,    0,  346,
  346,    0,   87,    0,  346,    0,  346,    0,    0,  346,
  346,  346,  346,  346,  346,  346,  346,  346,  346,    0,
  346,  346,    0,  346,  346,  346,  346,  346,  346,  346,
  346,  346,  346,  346,  346,  346,  338,  338,  346,    0,
    0,    0,  346,  346,  346,  346,  346,    0,  346,  346,
  346,  346,  346,  346,  346,    0,  346,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  346,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  346,  346,
  346,  346,    0,  346,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  338,  338,  338,    0,  338,  338,    0,
    0,    0,  338,    0,  338,    0,    0,  338,  338,  338,
  338,  338,  338,  338,  338,  338,  338,    0,  338,  338,
    0,  338,  338,  338,  338,  338,  338,  338,  338,  338,
  338,  338,  338,  338,  327,  327,  338,    0,    0,    0,
  338,  338,  338,  338,  338,    0,  338,  338,  338,  338,
  338,  338,  338,    0,  338,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  338,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  338,  338,  338,  338,
    0,  338,    0,   93,    0,  229,    0,    0,    0,    0,
    0,  327,  327,  327,    0,  327,  327,    0,    0,    0,
  327,  171,  327,    0,    0,  327,  327,  327,  327,  327,
  327,  327,  327,  327,  327,    0,  327,  327,    0,  327,
  327,  327,  327,  327,  327,  327,  327,  327,  327,  327,
  327,  327,  172,    0,  327,  294,  294,  171,  327,  327,
  327,  327,  327,    0,  327,  327,  327,  327,  327,  327,
  327,    0,  327,   93,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  327,  170,    0,    0,    0,  172,    0,
    0,  171,    0,    0,  327,  327,  327,  327,    0,  327,
    0,    0,    0,    0,    0,    0,    0,   93,    0,  340,
    0,    0,  294,  294,  294,    0,  294,  294,    0,    0,
  170,  294,  172,  294,    0,  171,  294,  294,  294,  294,
  294,  294,  294,  294,  294,  294,    0,  294,  294,    0,
  294,  294,  294,  294,  294,  294,  294,  294,  294,  294,
  294,  294,  294,    0,  170,  294,  172,   93,    0,  294,
  294,  294,  294,  294,    0,  294,  294,  294,  294,  294,
  294,  294,    0,  294,    0,  171,    0,    0,    0,    0,
    0,    0,  132,    0,  294,   50,    0,    0,  170,    0,
    0,    0,    0,    0,    0,  294,  294,  294,  294,    0,
  294,   50,    0,    0,    0,    0,  172,    0,   50,    0,
    0,    0,    0,    0,    0,    0,   51,    0,  156,  157,
  158,    0,  159,  160,  161,    0,  162,    0,    0,    0,
    0,    0,   51,    0,  163,    0,    0,    0,  170,   51,
  164,    0,    0,  171,    0,    0,    0,  165,   49,    0,
    0,    0,    0,    0,  156,  157,  158,    0,  159,  160,
  161,    0,  162,    0,   49,    0,    0,    0,    0,  253,
  163,   49,    0,    0,  172,    0,  164,    0,    0,    0,
    0,    0,    0,  165,    0,    0,    0,    0,  156,  157,
  158,    0,  159,  160,  161,    0,  162,  206,    0,    0,
    0,    0,   50,  253,  163,    0,  170,    0,    0,    0,
  164,    0,    0,  207,  591,    0,    0,  165,    0,    0,
    0,  145,  156,  157,  158,    0,  159,  160,  161,    0,
  162,    0,    0,   51,  166,    0,    0,    0,  163,    0,
   50,    0,    0,    0,  164,    0,    0,    0,  167,  168,
  169,  165,  208,  209,    0,    0,    0,  210,  211,    0,
  212,  213,  214,  215,    0,   49,    0,    0,    0,    0,
  166,   51,  156,  157,  158,    0,  159,  160,  161,    0,
  162,  478,    0,    0,  167,  168,  169,    0,  163,   22,
    0,    0,    0,    0,  164,    0,    0,    0,   23,    0,
    0,  165,    0,   49,  166,   22,   24,   25,   26,   27,
   28,    0,   22,    0,   23,    0,    0,    0,  167,  168,
  169,   23,   24,   25,   26,   27,   28,    0,    0,   24,
   25,   26,   27,   28,  131,    0,    0,    0,  166,    0,
  156,  157,  158,    0,  159,  160,  161,    0,  162,    0,
    0,    0,  167,  168,  169,  253,  163,    0,    0,    0,
    0,    0,  164,    0,    0,    0,    0,    0,    0,  165,
    0,    0,    0,    0,    0,   96,    0,    0,    0,  580,
    0,    0,  403,    0,    0,    0,   36,   37,  166,   38,
   39,    0,    0,   41,    0,   42,   43,   44,   45,   46,
    0,    0,  167,  168,  169,  410,   22,    0,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,  273,
  156,  157,  158,    0,  159,  160,  161,    0,  162,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,  375,
    0,    0,  164,   23,    0,    0,  166,    0,  143,  165,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
  167,  168,  169,  144,    0,    0,    0,  232,    0,    0,
    0,    0,    0,    0,    0,    0,  274,  275,  276,    0,
  277,  278,    0,    0,    0,  279,    0,  280,    0,  412,
  281,  282,  283,  284,  285,  286,  287,  288,  289,  290,
    0,  291,  292,    0,  293,  294,  295,  296,  297,  298,
  299,  300,  301,  302,  303,  304,  305,  273,    0,  306,
    0,    0,    0,  307,  308,  309,  310,  311,    0,  312,
  313,  314,  315,  316,  317,  318,    0,  319,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  320,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  321,
  322,  323,  324,    0,  325,  230,    0,    0,    0,    0,
    0,    0,    0,    0,  274,  275,  276,    0,  277,  278,
    0,    0,    0,  279,    0,  280,    0,    0,  281,  282,
  283,  284,  285,  286,  287,  288,  289,  290,    0,  291,
  292,    0,  293,  294,  295,  296,  297,  298,  299,  300,
  301,  302,  303,  304,  305,  232,    0,  306,    0,    0,
    0,  307,  308,  309,  310,  311,    0,  312,  313,  314,
  315,  316,  317,  318,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  321,  322,  323,
  324,    0,  325,  233,    0,    0,    0,    0,    0,    0,
    0,    0,  232,  232,  232,    0,  232,  232,    0,    0,
    0,  232,    0,  232,    0,    0,  232,  232,  232,  232,
  232,  232,  232,  232,  232,  232,    0,  232,  232,    0,
  232,  232,  232,  232,  232,  232,  232,  232,  232,  232,
  232,  232,  232,  230,    0,  232,    0,    0,    0,  232,
  232,  232,  232,  232,    0,  232,  232,  232,  232,  232,
  232,  232,    0,  232,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  232,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  232,  232,  232,  232,    0,
  232,  231,    0,    0,    0,    0,    0,    0,    0,    0,
  230,  230,  230,    0,  230,  230,    0,    0,    0,  230,
    0,  230,    0,    0,  230,  230,  230,  230,  230,  230,
  230,  230,  230,  230,    0,  230,  230,    0,  230,  230,
  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,
  230,  233,    0,  230,    0,    0,    0,  230,  230,  230,
  230,  230,    0,  230,  230,  230,  230,  230,  230,  230,
    0,  230,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  230,    0,    0,    0,    0,    0,    0,   50,
    0,    0,    0,  230,  230,  230,  230,    0,  230,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  233,  233,
  233,    0,  233,  233,    0,    0,    0,  233,    0,  233,
   51,    0,  233,  233,  233,  233,  233,  233,  233,  233,
  233,  233,    0,  233,  233,    0,  233,  233,  233,  233,
  233,  233,  233,  233,  233,  233,  233,  233,  233,  231,
   50,  233,   49,    0,    0,  233,  233,  233,  233,  233,
    0,  233,  233,  233,  233,  233,  233,  233,    0,  233,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  233,   51,    0,    0,    0,    0,    0,    0,  125,    0,
    0,  233,  233,  233,  233,    0,  233,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  231,  231,  231,    0,
  231,  231,    0,   49,    0,  231,    0,  231,    0,  125,
  231,  231,  231,  231,  231,  231,  231,  231,  231,  231,
    0,  231,  231,    0,  231,  231,  231,  231,  231,  231,
  231,  231,  231,  231,  231,  231,  231,    0,    0,  231,
    0,  125,  126,  231,  231,  231,  231,  231,    0,  231,
  231,  231,  231,  231,  231,  231,    0,  231,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  231,    0,
    0,    0,    0,  126,    0,    0,    0,    0,    0,  231,
  231,  231,  231,   22,  231,    0,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,   50,    0,    0,
   24,   25,   26,   27,   28,  126,    0,    0,    0,    0,
    0,   29,   50,    0,    0,    0,   30,   31,   32,   33,
   34,   35,   36,   37,    0,   38,   39,   40,   51,   41,
    0,   42,   43,   44,   45,   46,    0,    0,    0,    0,
    0,    0,   50,   51,   22,    0,    0,   47,   48,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,    0,
   49,   24,   25,   26,   27,   28,    0,    0,    0,    0,
    0,    0,   96,   51,    0,   49,    0,    0,   50,    0,
    0,    0,  125,   36,   37,    0,   38,   39,    0,    0,
   41,  125,   42,   43,   44,   45,   46,   97,    0,  125,
  125,  125,  125,  125,    0,   49,    0,    0,    0,   51,
  125,    0,    0,    0,   50,    0,    0,    0,    0,    0,
    0,  125,  125,    0,  125,  125,  825,    0,  125,    0,
  125,  125,  125,  125,  125,  125,    0,    0,    0,    0,
    0,   49,    0,    0,    0,   51,  126,    0,    0,    0,
  681,    0,    0,    0,    0,  126,    0,    0,    0,    0,
    0,    0,    0,  126,  126,  126,  126,  126,    0,   50,
    0,    0,    0,    0,  126,    0,    0,   49,    0,    0,
    0,    0,   50,    0,    0,  126,  126,    0,  126,  126,
    0,    0,  126,    0,  126,  126,  126,  126,  126,  126,
   51,   22,    0,    0,    0,    0,    0,   50,    0,    0,
   23,    0,    0,   51,    0,    0,   22,    0,   24,   25,
   26,   27,   28,    0,    0,   23,    0,    0,    0,   96,
    0,    0,   49,   24,   25,   26,   27,   28,   51,   35,
   36,   37,    0,   38,   39,   49,   22,   41,   50,   42,
   43,   44,   45,   46,    0,   23,    0,  423,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
   49,    0,    0,    0,   60,    0,    0,    0,   50,   51,
    0,    0,   22,    0,    0,   36,   37,    0,   38,   39,
    0,   23,   41,    0,   42,   43,   44,   45,   46,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,   51,
   96,   49,    0,    0,   50,    0,    0,    0,  184,    0,
   50,   36,   37,    0,   38,   39,    0,   23,   41,    0,
   42,   43,   44,   45,   46,   24,   25,   26,   27,   28,
    0,   49,  156,  157,  158,   51,  159,  160,  161,   50,
  162,   51,    0,    0,   50,    0,    0,    0,    0,  425,
    0,  375,    0,    0,  164,    0,    0,    0,    0,    0,
    0,  165,    0,   22,    0,    0,    0,   49,   50,  187,
   51,    0,   23,   49,    0,   51,   22,  680,    0,    0,
   24,   25,   26,   27,   28,   23,    0,    0,    0,    0,
  143,    0,   50,   24,   25,   26,   27,   28,    0,   51,
    0,   22,   49,    0,    0,  144,    0,   49,    0,   84,
   23,    0,    0,    0,   50,    0,    0,    0,   24,   25,
   26,   27,   28,   51,    0,  792,    0,    0,    0,    0,
  131,   49,    0,    0,    0,  156,  157,  158,  353,  159,
  160,  161,   22,  354,   50,   51,    0,    0,    0,   50,
    0,   23,  355,    0,  356,   49,    0,  164,    0,   24,
   25,   26,   27,   28,  165,  156,  157,  158,  353,  159,
  160,  161,   22,  354,    0,   51,    0,   49,    0,    0,
   51,   23,  364,    0,  356,    0,    0,  164,    0,   24,
   25,   26,   27,   28,  165,   78,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   49,  184,  185,
  660,    0,   49,    0,  184,  185,    0,   23,  186,    0,
    0,    0,    0,   23,  186,   24,   25,   26,   27,   28,
    0,   24,   25,   26,   27,   28,    0,    0,    0,    0,
  686,  687,    0,  184,  370,    0,   79,    0,   22,    0,
    0,    0,   23,  371,    0,    0,    0,   23,    0,    0,
   24,   25,   26,   27,   28,   24,   25,   26,   27,   28,
  454,  455,   22,    0,    0,    0,    0,    0,    0,    0,
    0,   23,  747,    0,    0,    0,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,   22,    0,    0,    0,
    0,  733,  734,    0,  736,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,   22,    0,
    0,    0,    0,    0,    0,    0,    0,   23,  756,    0,
   78,    0,  680,    0,    0,   24,   25,   26,   27,   28,
    0,    0,    0,    0,    0,    0,   78,    0,   22,    0,
    0,    0,    0,  184,    0,    0,    0,   23,    0,    0,
    0,    0,   23,    0,    0,   24,   25,   26,   27,   28,
   24,   25,   26,   27,   28,  784,    0,  786,  787,    0,
    0,   79,    0,    0,    0,   78,   78,    0,    0,    0,
   78,   78,    0,   78,   78,   78,   78,   79,  337,  337,
    0,    0,    0,  456,  457,  458,  459,    0,    0,    0,
    0,   96,  460,  461,  462,  463,  464,  465,  466,  467,
  468,  469,   36,   37,    0,   38,   39,    0,  822,   41,
    0,   42,   43,   44,   45,   46,   79,   79,    0,    0,
    0,   79,   79,    0,   79,   79,   79,   79,    0,    0,
    0,    0,    0,    0,    0,  337,  337,  337,    0,  337,
  337,    0,    0,    0,  337,    0,  337,    0,    0,  337,
  337,  337,  337,  337,  337,  337,  337,  337,  337,    0,
  337,  337,    0,  337,  337,  337,  337,  337,  337,  337,
  337,  337,  337,  337,  337,  337,  311,  311,  337,    0,
    0,    0,  337,  337,  337,  337,  337,    0,  337,  337,
  337,  337,  337,  337,  337,    0,  337,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  337,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  337,  337,
  337,  337,    0,  337,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  311,  311,  311,    0,  311,  311,    0,
    0,    0,  311,    0,  311,    0,    0,  311,  311,  311,
  311,  311,  311,  311,  311,  311,  311,    0,  311,  311,
    0,  311,  311,  311,  311,  311,  311,  311,  311,  311,
  311,  311,  311,  311,  295,  295,  311,    0,    0,    0,
  311,  311,  311,  311,  311,    0,  311,  311,  311,  311,
  311,  311,  311,    0,  311,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  311,  311,  311,  311,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  295,  295,  295,    0,  295,  295,    0,    0,    0,
  295,    0,  295,    0,    0,  295,  295,  295,  295,  295,
  295,  295,  295,  295,  295,    0,  295,  295,    0,  295,
  295,  295,  295,  295,  295,  295,  295,  295,  295,  295,
  295,  295,  300,  300,  295,    0,    0,    0,  295,  295,
  295,  295,  295,    0,  295,  295,  295,  295,  295,  295,
  295,    0,  295,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  295,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  295,  295,  295,  295,    0,  295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  300,
  300,  300,    0,  300,  300,    0,    0,    0,  300,    0,
  300,    0,    0,  300,  300,  300,  300,  300,  300,  300,
  300,  300,  300,    0,  300,  300,    0,  300,  300,  300,
  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  296,  296,  300,    0,    0,    0,  300,  300,  300,  300,
  300,    0,  300,  300,  300,  300,  300,  300,  300,    0,
  300,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  300,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  300,  300,  300,  300,    0,  300,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  296,  296,  296,
    0,  296,  296,    0,    0,    0,  296,    0,  296,    0,
    0,  296,  296,  296,  296,  296,  296,  296,  296,  296,
  296,    0,  296,  296,    0,  296,  296,  296,  296,  296,
  296,  296,  296,  296,  296,  296,  296,  296,  305,  305,
  296,    0,    0,    0,  296,  296,  296,  296,  296,    0,
  296,  296,  296,  296,  296,  296,  296,    0,  296,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  296,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  296,  296,  296,  296,    0,  296,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  305,  305,  305,    0,  305,
  305,    0,    0,    0,  305,    0,  305,    0,    0,  305,
  305,  305,  305,  305,  305,  305,  305,  305,  305,    0,
  305,  305,    0,  305,  305,  305,  305,  305,  305,  305,
  305,  305,  305,  305,  305,  305,  326,  326,  305,    0,
    0,    0,  305,  305,  305,  305,  305,    0,  305,  305,
  305,  305,  305,  305,  305,    0,  305,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  305,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  305,  305,
  305,  305,    0,  305,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  326,  326,  326,    0,  326,  326,    0,
    0,    0,  326,    0,  326,    0,    0,  326,  326,  326,
  326,  326,  326,  326,  326,  326,  326,    0,  326,  326,
    0,  326,  326,  326,  326,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  322,  322,  326,    0,    0,    0,
  326,  326,  326,  326,  326,    0,  326,  326,  326,  326,
  326,  326,  326,    0,  326,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  326,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  326,  326,  326,  326,
    0,  326,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  322,  322,  322,    0,  322,  322,    0,    0,    0,
  322,    0,  322,    0,    0,  322,  322,  322,  322,  322,
  322,  322,  322,  322,  322,    0,  322,  322,    0,  322,
  322,  322,  322,  322,  322,  322,  322,  322,  322,  322,
  322,  322,  301,  301,  322,    0,    0,    0,  322,  322,
  322,  322,  322,    0,  322,  322,  322,  322,  322,  322,
  322,    0,  322,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  322,  322,  322,  322,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  301,
  301,  301,    0,  301,  301,    0,    0,    0,  301,    0,
  301,    0,    0,  301,  301,  301,  301,  301,  301,  301,
  301,  301,  301,    0,  301,  301,    0,  301,  301,  301,
  301,  301,  301,  301,  301,  301,  301,  301,  301,  301,
  297,  297,  301,    0,    0,    0,  301,  301,  301,  301,
  301,    0,  301,  301,  301,  301,  301,  301,  301,    0,
  301,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  301,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  301,  301,  301,  301,    0,  301,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  297,  297,  297,
    0,  297,  297,    0,    0,    0,  297,    0,  297,    0,
    0,  297,  297,  297,  297,  297,  297,  297,  297,  297,
  297,    0,  297,  297,    0,  297,  297,  297,  297,  297,
  297,  297,  297,  297,  297,  297,  297,  297,  298,  298,
  297,    0,    0,    0,  297,  297,  297,  297,  297,    0,
  297,  297,  297,  297,  297,  297,  297,    0,  297,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  297,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  297,  297,  297,  297,    0,  297,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  298,  298,  298,    0,  298,
  298,    0,    0,    0,  298,    0,  298,    0,    0,  298,
  298,  298,  298,  298,  298,  298,  298,  298,  298,    0,
  298,  298,    0,  298,  298,  298,  298,  298,  298,  298,
  298,  298,  298,  298,  298,  298,  302,  302,  298,    0,
    0,    0,  298,  298,  298,  298,  298,    0,  298,  298,
  298,  298,  298,  298,  298,    0,  298,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  298,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  298,  298,
  298,  298,    0,  298,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  302,  302,  302,    0,  302,  302,    0,
    0,    0,  302,    0,  302,    0,    0,  302,  302,  302,
  302,  302,  302,  302,  302,  302,  302,    0,  302,  302,
    0,  302,  302,  302,  302,  302,  302,  302,  302,  302,
  302,  302,  302,  302,  303,  303,  302,    0,    0,    0,
  302,  302,  302,  302,  302,    0,  302,  302,  302,  302,
  302,  302,  302,    0,  302,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  302,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  302,  302,  302,  302,
    0,  302,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  303,  303,  303,    0,  303,  303,    0,    0,    0,
  303,    0,  303,    0,    0,  303,  303,  303,  303,  303,
  303,  303,  303,  303,  303,    0,  303,  303,    0,  303,
  303,  303,  303,  303,  303,  303,  303,  303,  303,  303,
  303,  303,  306,  306,  303,    0,    0,    0,  303,  303,
  303,  303,  303,    0,  303,  303,  303,  303,  303,  303,
  303,    0,  303,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  303,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  303,  303,  303,  303,    0,  303,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  306,
  306,  306,    0,  306,  306,    0,    0,    0,  306,    0,
  306,    0,    0,  306,  306,  306,  306,  306,  306,  306,
  306,  306,  306,    0,  306,  306,    0,  306,  306,  306,
  306,  306,  306,  306,  306,  306,  306,  306,  306,  306,
  323,  323,  306,    0,    0,    0,  306,  306,  306,  306,
  306,    0,  306,  306,  306,  306,  306,  306,  306,    0,
  306,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  306,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  306,  306,  306,  306,    0,  306,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  323,  323,  323,
    0,  323,  323,    0,    0,    0,  323,    0,  323,    0,
    0,  323,  323,  323,  323,  323,  323,  323,  323,  323,
  323,    0,  323,  323,    0,  323,  323,  323,  323,  323,
  323,  323,  323,  323,  323,  323,  323,  323,  299,  299,
  323,    0,    0,    0,  323,  323,  323,  323,  323,    0,
  323,  323,  323,  323,  323,  323,  323,    0,  323,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  323,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,  323,  323,  323,    0,  323,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  299,  299,  299,    0,  299,
  299,    0,    0,    0,  299,    0,  299,    0,    0,  299,
  299,  299,  299,  299,  299,  299,  299,  299,  299,    0,
  299,  299,    0,  299,  299,  299,  299,  299,  299,  299,
  299,  299,  299,  299,  299,  299,  304,  304,  299,    0,
    0,    0,  299,  299,  299,  299,  299,    0,  299,  299,
  299,  299,  299,  299,  299,    0,  299,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  299,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  299,  299,
  299,  299,    0,  299,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  304,  304,  304,    0,  304,  304,    0,
    0,    0,  304,    0,  304,    0,    0,  304,  304,  304,
  304,  304,  304,  304,  304,  304,  304,    0,  304,  304,
    0,  304,  304,  304,  304,  304,  304,  304,  304,  304,
  304,  304,  304,  304,  307,  307,  304,    0,    0,    0,
  304,  304,  304,  304,  304,    0,  304,  304,  304,  304,
  304,  304,  304,    0,  304,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  304,  304,  304,  304,
    0,  304,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  307,  307,  307,    0,  307,  307,    0,    0,    0,
  307,    0,  307,    0,    0,  307,  307,  307,  307,  307,
  307,  307,  307,  307,  307,    0,  307,  307,    0,  307,
  307,  307,  307,  307,  307,  307,  307,  307,  307,  307,
  307,  307,  308,  308,  307,    0,    0,    0,  307,  307,
  307,  307,  307,    0,  307,  307,  307,  307,  307,  307,
  307,    0,  307,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  307,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  307,  307,  307,  307,    0,  307,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  308,
  308,  308,    0,  308,  308,    0,    0,    0,  308,    0,
  308,    0,    0,  308,  308,  308,  308,  308,  308,  308,
  308,  308,  308,    0,  308,  308,    0,  308,  308,  308,
  308,  308,  308,  308,  308,  308,  308,  308,  308,  308,
  309,  309,  308,    0,    0,    0,  308,  308,  308,  308,
  308,    0,  308,  308,  308,  308,  308,  308,  308,    0,
  308,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  308,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  308,  308,  308,  308,    0,  308,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,  309,  309,
    0,  309,  309,    0,    0,    0,  309,    0,  309,    0,
    0,  309,  309,  309,  309,  309,  309,  309,  309,  309,
  309,    0,  309,  309,    0,  309,  309,  309,  309,  309,
  309,  309,  309,  309,  309,  309,  309,  309,  273,    0,
  309,    0,    0,    0,  309,  309,  309,  309,  309,    0,
  309,  309,  309,  309,  309,  309,  309,    0,  309,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  309,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  309,  309,  309,  309,    0,  309,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  274,  275,  276,    0,  277,
  278,    0,    0,    0,  279,    0,  280,    0,    0,  281,
  282,  283,  284,  285,  286,  287,  288,  289,  290,    0,
  291,  292,    0,  293,  294,  295,  296,  297,  298,  299,
  300,  301,  302,  303,  304,  305,  236,    0,  306,    0,
    0,    0,  307,  308,  309,  310,  311,    0,  312,  313,
  314,  315,  316,  317,  318,    0,  319,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  320,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  321,  322,
  323,  324,    0,  325,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  236,  236,  236,    0,  236,  236,    0,
    0,    0,  236,    0,  236,    0,    0,  236,  236,  236,
  236,  236,  236,  236,  236,  236,  236,    0,  236,  236,
    0,  236,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  236,  236,  236,  237,    0,  236,    0,    0,    0,
  236,  236,  236,  236,  236,    0,  236,  236,  236,  236,
  236,  236,  236,    0,  236,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  236,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  236,  236,  236,  236,
    0,  236,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  237,  237,  237,    0,  237,  237,    0,    0,    0,
  237,    0,  237,    0,    0,  237,  237,  237,  237,  237,
  237,  237,  237,  237,  237,    0,  237,  237,    0,  237,
  237,  237,  237,  237,  237,  237,  237,  237,  237,  237,
  237,  237,  238,    0,  237,    0,    0,    0,  237,  237,
  237,  237,  237,    0,  237,  237,  237,  237,  237,  237,
  237,    0,  237,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  237,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  237,  237,  237,  237,    0,  237,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  238,
  238,  238,    0,  238,  238,    0,    0,    0,  238,    0,
  238,    0,    0,  238,  238,  238,  238,  238,  238,  238,
  238,  238,  238,    0,  238,  238,    0,  238,  238,  238,
  238,  238,  238,  238,  238,  238,  238,  238,  238,  238,
  239,    0,  238,    0,    0,    0,  238,  238,  238,  238,
  238,    0,  238,  238,  238,  238,  238,  238,  238,    0,
  238,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  238,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  238,  238,  238,  238,    0,  238,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  239,  239,  239,
    0,  239,  239,    0,    0,    0,  239,    0,  239,    0,
    0,  239,  239,  239,  239,  239,  239,  239,  239,  239,
  239,    0,  239,  239,    0,  239,  239,  239,  239,  239,
  239,  239,  239,  239,  239,  239,  239,  239,    0,    0,
  239,    0,    0,    0,  239,  239,  239,  239,  239,    0,
  239,  239,  239,  239,  239,  239,  239,    0,  239,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  239,
    0,    0,    0,    0,    0,    0,    0,  277,    0,    0,
  239,  239,  239,  239,  280,  239,    0,  281,  282,  283,
  284,  285,  286,  287,  288,  289,  290,    0,  291,  292,
    0,  293,  294,  295,  296,  297,  298,  299,  300,  301,
  302,  303,  304,  305,    0,    0,  306,    0,    0,    0,
  307,  308,  309,  310,  311,    0,  312,  313,  314,  315,
  316,  317,  318,    0,  319,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  320,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  321,  322,  323,  324,
    0,  325,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   57,   53,    6,   60,  499,   33,   40,  243,  244,
  245,  125,  326,   33,  123,  108,  221,   40,  123,  123,
  741,   42,  123,   40,  123,   40,  473,   40,  123,  309,
   44,  266,  123,  123,    0,   44,  549,  103,  318,  113,
   91,  318,   49,   60,  276,  152,  679,  272,  280,   56,
   57,   42,  773,   60,   19,   41,   63,  150,   44,   40,
   63,  290,  291,   44,   40,  512,  513,   60,   44,   76,
   44,   61,  138,   44,   91,  141,  329,   44,  331,  200,
  154,   42,   89,   44,  205,   44,  266,   94,   61,  318,
  146,   98,   61,  100,  374,  102,  203,  100,   42,  102,
  107,  175,   93,   61,  111,  285,  123,  114,   40,  202,
   75,  125,  745,   78,  221,   42,  125,   44,  125,  126,
  309,  128,  357,  636,  571,  572,  639,  574,   60,  318,
  123,  260,  337,   42,   93,  201,    6,    7,  124,  860,
    0,  262,  374,  108,  125,   42,  260,   91,  155,  125,
  302,  125,  257,   62,  125,  602,  263,  231,  125,   91,
   44,   41,  276,    0,   44,  274,  294,  295,  242,  176,
  274,  485,   61,  274,  379,  274,  328,  329,   62,  274,
   40,  270,  271,  274,  274,  374,   56,   57,  290,  291,
   60,  123,  199,   63,   75,  282,   77,  284,   61,   40,
   41,  315,  316,   44,  291,   41,  320,  321,   44,  323,
  324,  325,  326,  220,   41,  218,  663,   44,  665,  666,
   41,  277,   41,   44,   40,   44,  277,  480,   98,  257,
  100,  112,  102,  280,  115,   41,  243,  244,   44,  272,
  257,  258,  259,  496,  261,  262,  263,  274,  265,  272,
  277,   42,  505,   44,  274,  272,  273,  272,   41,  272,
  274,   44,  279,   42,  499,  274,  501,   40,  503,  286,
  277,   61,  323,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  291,  292,  293,  294,  295,  296,
  311,   42,  739,   44,  287,  302,  303,  393,   40,  306,
  794,  397,  268,  269,  400,  260,  272,  273,  543,  275,
   42,   60,   44,  409,  321,  322,  323,   42,  325,   44,
  323,   41,  288,  289,   44,  257,  258,  259,  581,  261,
  262,  263,  388,  265,   41,  342,  589,   44,  171,  172,
  272,  273,   91,   41,  290,  291,   44,  279,  294,  295,
  260,   40,   41,  319,  286,   44,  373,   60,   41,   42,
  257,  258,  259,   40,  261,  262,  263,   42,  265,   44,
  387,  388,  389,  123,  123,  272,  273,   41,   42,  257,
  387,  388,  279,  302,  387,  267,  393,  374,   91,  286,
  397,   41,   41,  400,   44,   44,  403,   41,  419,  267,
   44,  123,  409,  410,  123,  412,   40,  277,  268,  269,
  123,    0,  272,   41,   42,  275,  423,  670,   41,   42,
  123,   41,  429,  676,  318,  476,   41,   42,  288,  289,
  375,  268,  269,   41,   42,  272,  273,  257,  275,   40,
   40,  373,   40,   62,   58,   44,  453,    0,   58,   58,
   61,  288,  289,  323,   40,  387,  388,  389,  257,  319,
  122,  274,  374,  470,   44,   40,  374,  474,  475,  476,
   44,  260,  475,  476,   61,  257,  373,  327,  415,  274,
  715,  716,  319,  309,  374,  492,  493,  494,  274,  257,
  387,  388,  389,   44,  501,  374,  382,  257,  258,  259,
  257,  261,  262,  263,   44,  265,  382,  514,  170,  171,
  172,  514,  374,  748,  509,  264,  276,  387,  388,  279,
  576,  183,   40,  382,  273,  273,  286,  124,   44,   44,
   44,   44,  281,  282,  283,  284,  285,   44,   44,   44,
  547,  274,  549,  382,  382,  552,  553,  554,  555,  556,
  557,  558,  559,  560,  561,  562,  563,  382,  382,  794,
  382,  264,  257,  225,  189,  382,  573,  660,  575,  576,
  273,  233,  234,  235,  382,  382,  257,  382,  281,  282,
  283,  284,  285,  257,   42,  382,  382,  374,  382,  374,
  252,  598,   44,  266,   40,   44,   44,   44,    0,   44,
   44,  596,   60,   44,  474,  475,  476,   44,   44,   44,
   44,   44,  274,   44,  276,   44,  278,   44,   44,   44,
   44,   44,   44,   44,  257,  732,  682,  376,  257,  636,
   91,    0,  639,   91,   44,  297,  298,  299,  300,  301,
  733,  734,  304,  736,  514,  307,  308,  309,  310,  311,
  312,  313,  314,  315,  316,  317,  318,   44,  382,   44,
  667,   44,   44,   44,  667,  123,   44,   44,   44,   44,
   44,  374,  679,  680,  257,  782,  783,   44,  785,  268,
  269,   44,   44,  272,  273,  347,  275,   44,   44,   44,
  685,  784,   44,  786,  787,   91,   44,   42,   44,  288,
  289,   44,  382,  573,  382,  575,  576,  374,  374,   44,
   44,  374,  819,  820,  821,  268,  269,   44,   44,  272,
  273,   60,  275,   40,  382,  257,  322,  374,  257,  822,
  319,  838,  374,  257,   44,  288,  289,  375,  745,   93,
   40,    0,   40,   40,  322,  752,  749,  257,  743,  322,
    0,   40,   91,  257,  322,  382,  382,   44,  383,  754,
  755,  382,    0,  425,  272,   40,  319,  392,   10,  272,
  395,  396,  272,  398,  399,    0,  401,  402,  272,  404,
  405,  406,  407,  408,  123,  272,  411,   79,  413,  414,
  415,  416,  183,  182,  247,   19,  791,  667,  216,  257,
  258,  259,  244,  261,  262,  263,   89,  265,  501,  199,
  805,  220,  335,  808,  272,  273,   53,    0,  816,  715,
  380,  279,  752,  716,  503,  745,  328,  328,  286,  658,
  380,   -1,   -1,  840,  292,   -1,   -1,  832,  833,   -1,
   -1,  848,  849,  850,   -1,  303,  304,  472,  306,  307,
   -1,  846,  310,    0,  312,  313,  314,  315,  316,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   60,    0,   -1,   -1,  540,  541,
  542,   -1,  544,  545,   -1,   -1,  288,  289,   -1,  551,
   -1,  516,   -1,   -1,   42,  520,   -1,   -1,  523,  268,
  269,  526,   -1,  272,  273,   91,  275,  532,  533,   -1,
  535,   -1,    0,   -1,   -1,  373,  578,  319,   -1,  288,
  289,   -1,  584,   -1,   -1,  264,   -1,   -1,   -1,  387,
  388,  389,   -1,   -1,  273,   -1,   -1,  123,   -1,  564,
  565,  566,  281,  282,  283,  284,  285,  292,   -1,   -1,
  319,   -1,   40,   41,   42,   -1,   44,   -1,  303,  304,
   -1,  306,  307,   -1,   -1,  310,   -1,  312,  313,  314,
  315,  316,   60,   -1,   62,   -1,   -1,   -1,  603,  641,
  605,  606,   -1,  608,  609,   -1,  611,  612,   -1,  614,
  615,  616,  617,  618,   -1,   -1,  621,  659,  623,  624,
  625,  626,   -1,   91,   -1,   93,   -1,   -1,   -1,  268,
  269,   -1,  637,  272,  273,   -1,  275,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,  288,
  289,   -1,   -1,   -1,   -1,  123,  375,  125,  288,  289,
   -1,   -1,   -1,  268,  269,   -1,   -1,  272,  273,    0,
  275,  713,  714,   -1,   40,   41,   -1,  682,   44,   -1,
  319,   -1,   -1,  288,  289,  690,   -1,   -1,  693,  319,
   -1,  696,   -1,   -1,  699,   -1,   62,   -1,  264,   -1,
  705,  706,   -1,  708,   -1,  268,  269,  273,   -1,  272,
  273,   -1,  275,   -1,  319,  281,  282,  283,  284,  285,
   -1,   -1,  727,  728,  729,  288,  289,   93,   -1,  257,
  258,  259,  774,  261,  262,  263,   -1,  265,   -1,  781,
   -1,  268,  269,   -1,  749,  272,  273,   -1,  275,   -1,
   -1,  279,   -1,   -1,   -1,   -1,  319,  123,  286,  125,
   -1,  288,  289,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,   40,   -1,   -1,   -1,  342,  343,   -1,   -1,
   -1,   -1,   -1,  288,  289,   -1,  828,  829,  830,  257,
  258,  259,  319,  261,  262,  263,   -1,  265,   -1,   -1,
  268,  269,   -1,   -1,  272,  273,  274,  275,  268,  269,
  852,  279,  272,  273,  319,  275,   -1,   -1,  286,   -1,
  288,  289,   -1,   -1,  292,   -1,   -1,   -1,  288,  289,
   -1,   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,
   -1,   -1,  310,  311,  312,  313,  314,  315,  316,   -1,
   -1,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  319,
   -1,   -1,  330,  331,  332,   -1,  334,  335,    0,   -1,
   40,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,   -1,   -1,  373,   -1,  273,  274,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  402,  265,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,  413,  414,  415,  416,  279,
  418,  419,   -1,   -1,   -1,   -1,  286,  288,  289,   -1,
   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,
   -1,   40,   -1,  339,   -1,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  350,  351,  352,  353,  319,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,  369,  370,  273,  274,  373,   -1,   -1,
   -1,  377,  378,  379,  380,  381,  382,  383,  384,  385,
  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  290,  291,   -1,  402,  294,  295,  296,
  297,  298,  299,  300,  301,   -1,   -1,  413,  414,  415,
  416,  308,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   40,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,  413,  414,  415,  416,   -1,
  418,   -1,   -1,   -1,   -1,   -1,  288,  289,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   40,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,  319,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,   -1,   -1,  373,  273,  274,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,  392,  393,  394,  395,  396,  397,  398,
  399,  400,  401,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   40,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   40,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   40,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,   -1,   -1,
  373,  273,  274,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
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
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   42,   -1,   44,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   60,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,   91,   -1,  373,  273,  274,   60,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   42,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,  123,   -1,   -1,   -1,   91,   -1,
   -1,   60,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   42,   -1,   44,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
  123,  339,   91,  341,   -1,   60,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,   -1,  123,  373,   91,   42,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   41,   -1,  402,   60,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,   60,   -1,   -1,   -1,   -1,   91,   -1,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
   -1,   -1,   91,   -1,  273,   -1,   -1,   -1,  123,   91,
  279,   -1,   -1,   60,   -1,   -1,   -1,  286,  123,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,  123,   -1,   -1,   -1,   -1,  272,
  273,  123,   -1,   -1,   91,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  286,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  260,   -1,   -1,
   -1,   -1,   60,  272,  273,   -1,  123,   -1,   -1,   -1,
  279,   -1,   -1,  276,   41,   -1,   -1,  286,   -1,   -1,
   -1,   41,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,   91,  373,   -1,   -1,   -1,  273,   -1,
   60,   -1,   -1,   -1,  279,   -1,   -1,   -1,  387,  388,
  389,  286,  315,  316,   -1,   -1,   -1,  320,  321,   -1,
  323,  324,  325,  326,   -1,  123,   -1,   -1,   -1,   -1,
  373,   91,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,  125,   -1,   -1,  387,  388,  389,   -1,  273,  264,
   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,
   -1,  286,   -1,  123,  373,  264,  281,  282,  283,  284,
  285,   -1,  264,   -1,  273,   -1,   -1,   -1,  387,  388,
  389,  273,  281,  282,  283,  284,  285,   -1,   -1,  281,
  282,  283,  284,  285,  293,   -1,   -1,   -1,  373,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,  387,  388,  389,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,  286,
   -1,   -1,   -1,   -1,   -1,  292,   -1,   -1,   -1,  125,
   -1,   -1,  357,   -1,   -1,   -1,  303,  304,  373,  306,
  307,   -1,   -1,  310,   -1,  312,  313,  314,  315,  316,
   -1,   -1,  387,  388,  389,  357,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,  273,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,  276,
   -1,   -1,  279,  273,   -1,   -1,  373,   -1,  278,  286,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
  387,  388,  389,  293,   -1,   -1,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,  357,
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
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,
  416,   -1,  418,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,
   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  350,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  369,  370,  273,   -1,  373,   -1,   -1,   -1,  377,
  378,  379,  380,  381,   -1,  383,  384,  385,  386,  387,
  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,
  418,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,
   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  350,  351,  352,  353,   -1,  355,  356,   -1,  358,  359,
  360,  361,  362,  363,  364,  365,  366,  367,  368,  369,
  370,  273,   -1,  373,   -1,   -1,   -1,  377,  378,  379,
  380,  381,   -1,  383,  384,  385,  386,  387,  388,  389,
   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,
   91,   -1,  344,  345,  346,  347,  348,  349,  350,  351,
  352,  353,   -1,  355,  356,   -1,  358,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  369,  370,  273,
   60,  373,  123,   -1,   -1,  377,  378,  379,  380,  381,
   -1,  383,  384,  385,  386,  387,  388,  389,   -1,  391,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  402,   91,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,
  334,  335,   -1,  123,   -1,  339,   -1,  341,   -1,   91,
  344,  345,  346,  347,  348,  349,  350,  351,  352,  353,
   -1,  355,  356,   -1,  358,  359,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,   -1,   -1,  373,
   -1,  123,   60,  377,  378,  379,  380,  381,   -1,  383,
  384,  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,  413,
  414,  415,  416,  264,  418,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   60,   -1,   -1,
  281,  282,  283,  284,  285,  123,   -1,   -1,   -1,   -1,
   -1,  292,   60,   -1,   -1,   -1,  297,  298,  299,  300,
  301,  302,  303,  304,   -1,  306,  307,  308,   91,  310,
   -1,  312,  313,  314,  315,  316,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   91,  264,   -1,   -1,  328,  329,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
  123,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,   -1,  292,   91,   -1,  123,   -1,   -1,   60,   -1,
   -1,   -1,  264,  303,  304,   -1,  306,  307,   -1,   -1,
  310,  273,  312,  313,  314,  315,  316,  317,   -1,  281,
  282,  283,  284,  285,   -1,  123,   -1,   -1,   -1,   91,
  292,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
   -1,  303,  304,   -1,  306,  307,   41,   -1,  310,   -1,
  312,  313,  314,  315,  316,  317,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   91,  264,   -1,   -1,   -1,
   41,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   60,
   -1,   -1,   -1,   -1,  292,   -1,   -1,  123,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  303,  304,   -1,  306,  307,
   -1,   -1,  310,   -1,  312,  313,  314,  315,  316,  317,
   91,  264,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,
  273,   -1,   -1,   91,   -1,   -1,  264,   -1,  281,  282,
  283,  284,  285,   -1,   -1,  273,   -1,   -1,   -1,  292,
   -1,   -1,  123,  281,  282,  283,  284,  285,   91,  302,
  303,  304,   -1,  306,  307,  123,  264,  310,   60,  312,
  313,  314,  315,  316,   -1,  273,   -1,  305,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
  123,   -1,   -1,   -1,  292,   -1,   -1,   -1,   60,   91,
   -1,   -1,  264,   -1,   -1,  303,  304,   -1,  306,  307,
   -1,  273,  310,   -1,  312,  313,  314,  315,  316,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   91,
  292,  123,   -1,   -1,   60,   -1,   -1,   -1,  264,   -1,
   60,  303,  304,   -1,  306,  307,   -1,  273,  310,   -1,
  312,  313,  314,  315,  316,  281,  282,  283,  284,  285,
   -1,  123,  257,  258,  259,   91,  261,  262,  263,   60,
  265,   91,   -1,   -1,   60,   -1,   -1,   -1,   -1,  305,
   -1,  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,
   -1,  286,   -1,  264,   -1,   -1,   -1,  123,   60,  125,
   91,   -1,  273,  123,   -1,   91,  264,  278,   -1,   -1,
  281,  282,  283,  284,  285,  273,   -1,   -1,   -1,   -1,
  278,   -1,   60,  281,  282,  283,  284,  285,   -1,   91,
   -1,  264,  123,   -1,   -1,  293,   -1,  123,   -1,  125,
  273,   -1,   -1,   -1,   60,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   91,   -1,   93,   -1,   -1,   -1,   -1,
  293,  123,   -1,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   60,   91,   -1,   -1,   -1,   60,
   -1,  273,  274,   -1,  276,  123,   -1,  279,   -1,  281,
  282,  283,  284,  285,  286,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   -1,   91,   -1,  123,   -1,   -1,
   91,  273,  274,   -1,  276,   -1,   -1,  279,   -1,  281,
  282,  283,  284,  285,  286,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,  264,  265,
  570,   -1,  123,   -1,  264,  265,   -1,  273,  274,   -1,
   -1,   -1,   -1,  273,  274,  281,  282,  283,  284,  285,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
  600,  601,   -1,  264,  265,   -1,  125,   -1,  264,   -1,
   -1,   -1,  273,  274,   -1,   -1,   -1,  273,   -1,   -1,
  281,  282,  283,  284,  285,  281,  282,  283,  284,  285,
  261,  262,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,  264,   -1,   -1,   -1,
   -1,  661,  662,   -1,  664,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  688,   -1,
  260,   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   -1,   -1,  276,   -1,  264,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,  273,   -1,   -1,  281,  282,  283,  284,  285,
  281,  282,  283,  284,  285,  735,   -1,  737,  738,   -1,
   -1,  260,   -1,   -1,   -1,  315,  316,   -1,   -1,   -1,
  320,  321,   -1,  323,  324,  325,  326,  276,  273,  274,
   -1,   -1,   -1,  394,  395,  396,  397,   -1,   -1,   -1,
   -1,  292,  403,  404,  405,  406,  407,  408,  409,  410,
  411,  412,  303,  304,   -1,  306,  307,   -1,  788,  310,
   -1,  312,  313,  314,  315,  316,  315,  316,   -1,   -1,
   -1,  320,  321,   -1,  323,  324,  325,  326,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,  274,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  273,  274,
  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,  274,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  273,  274,
  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,  274,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  273,  274,
  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,  274,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,  274,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,  274,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,  274,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,  273,   -1,
  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  413,  414,  415,  416,   -1,  418,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  332,   -1,  334,
  335,   -1,   -1,   -1,  339,   -1,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,  369,  370,  273,   -1,  373,   -1,
   -1,   -1,  377,  378,  379,  380,  381,   -1,  383,  384,
  385,  386,  387,  388,  389,   -1,  391,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,
  415,  416,   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  332,   -1,  334,  335,   -1,
   -1,   -1,  339,   -1,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  273,   -1,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  332,   -1,  334,  335,   -1,   -1,   -1,
  339,   -1,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  350,  351,  352,  353,   -1,  355,  356,   -1,  358,
  359,  360,  361,  362,  363,  364,  365,  366,  367,  368,
  369,  370,  273,   -1,  373,   -1,   -1,   -1,  377,  378,
  379,  380,  381,   -1,  383,  384,  385,  386,  387,  388,
  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  413,  414,  415,  416,   -1,  418,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,  332,   -1,  334,  335,   -1,   -1,   -1,  339,   -1,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  350,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,  368,  369,  370,
  273,   -1,  373,   -1,   -1,   -1,  377,  378,  379,  380,
  381,   -1,  383,  384,  385,  386,  387,  388,  389,   -1,
  391,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  402,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  413,  414,  415,  416,   -1,  418,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,  332,
   -1,  334,  335,   -1,   -1,   -1,  339,   -1,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  369,  370,   -1,   -1,
  373,   -1,   -1,   -1,  377,  378,  379,  380,  381,   -1,
  383,  384,  385,  386,  387,  388,  389,   -1,  391,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  402,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  334,   -1,   -1,
  413,  414,  415,  416,  341,  418,   -1,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  369,  370,   -1,   -1,  373,   -1,   -1,   -1,
  377,  378,  379,  380,  381,   -1,  383,  384,  385,  386,
  387,  388,  389,   -1,  391,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  402,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  413,  414,  415,  416,
   -1,  418,
  };

#line 1282 "Iril/IR/IR.jay"

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
