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
//t    "metadata_args : metadata_arg",
//t    "metadata_args : metadata_args ',' metadata_arg",
//t    "metadata_arg : SYMBOL ':' metadata_arg_expr",
//t    "metadata_arg : TYPE ':' META_SYMBOL",
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
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_type_args : function_type_arg",
//t    "function_type_args : function_type_args ',' function_type_arg",
//t    "function_type_arg : type",
//t    "function_type_arg : ELLIPSIS",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters attribute_group_refs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters attribute_group_refs ALIGN INTEGER metadata_kvs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs ALIGN INTEGER metadata_kvs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs ALIGN INTEGER personality_function metadata_kvs '{' blocks '}'",
//t    "function_definition : define_header GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "define_header : DEFINE return_type",
//t    "define_header : DEFINE parameter_attribute return_type",
//t    "define_header : DEFINE define_header_attributes return_type",
//t    "define_header : DEFINE define_header_attributes parameter_attribute return_type",
//t    "define_header_attributes : NOALIAS",
//t    "define_header_attributes : runtime_preemption_specifier",
//t    "define_header_attributes : calling_convention",
//t    "define_header_attributes : linkage",
//t    "define_header_attributes : linkage runtime_preemption_specifier",
//t    "define_header_attributes : linkage runtime_preemption_specifier calling_convention",
//t    "define_header_attributes : linkage calling_convention",
//t    "personality_function : PERSONALITY typed_value",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters attribute_group_refs",
//t    "function_declaration : DECLARE NOALIAS return_type GLOBAL_SYMBOL parameters attribute_group_refs",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs",
//t    "function_declaration : DECLARE parameter_attributes return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs",
//t    "function_declaration : DECLARE NOALIAS parameter_attributes return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs",
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
//t    "element_indices : element_index",
//t    "element_indices : element_indices ',' element_index",
//t    "blocks : block",
//t    "blocks : blocks block",
//t    "block : assignments terminator_instruction",
//t    "block : assignments terminator_instruction metadata_kvs",
//t    "block : terminator_instruction",
//t    "block : terminator_instruction metadata_kvs",
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
//t    "terminator_instruction : BR label_value",
//t    "terminator_instruction : BR INTEGER_TYPE value ',' label_value ',' label_value",
//t    "terminator_instruction : INVOKE return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "terminator_instruction : INVOKE calling_convention return_type function_pointer function_args TO label_value UNWIND label_value",
//t    "terminator_instruction : RESUME typed_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "terminator_instruction : UNREACHABLE",
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
//t    "instruction : CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args",
//t    "instruction : TAIL CALL parameter_attribute return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args attribute_group_refs",
//t    "instruction : TAIL CALL calling_convention parameter_attribute return_type function_pointer function_args",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
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
//t    "instruction : INTTOPTR typed_value TO type",
//t    "instruction : LANDINGPAD type CLEANUP",
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
    "NOCAPTURE","WRITEONLY","READONLY","READNONE","ATTRIBUTE_GROUP_REF",
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
    "UNE","UNO","PHI","SELECT","CALL","TAIL","VA_ARG","LANDINGPAD",
    "CATCHPAD","CLEANUPPAD",
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
  case_39();
  break;
case 40:
  case_40();
  break;
case 41:
#line 201 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 42:
#line 202 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 43:
#line 203 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 44:
#line 207 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 45:
#line 211 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 55:
#line 239 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 56:
#line 243 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 57:
#line 250 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 58:
#line 254 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 59:
#line 258 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 60:
#line 262 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 61:
#line 266 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 79:
#line 299 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 80:
#line 303 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 81:
#line 307 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 82:
#line 314 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 83:
#line 318 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 323 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 88:
#line 329 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 89:
#line 330 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 90:
#line 331 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 91:
#line 332 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 92:
#line 336 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 93:
#line 340 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 344 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 95:
#line 348 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 352 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 356 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 363 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 99:
#line 367 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 375 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 102:
  case_102();
  break;
case 103:
  case_103();
  break;
case 104:
  case_104();
  break;
case 105:
  case_105();
  break;
case 106:
  case_106();
  break;
case 107:
  case_107();
  break;
case 108:
  case_108();
  break;
case 109:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 110:
#line 424 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 111:
#line 428 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 112:
#line 432 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 113:
#line 439 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 114:
#line 443 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 115:
#line 447 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 120:
#line 458 "Iril/IR/IR.jay"
  {
        yyVal = yyVals[0+yyTop];
    }
  break;
case 121:
#line 465 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 122:
#line 469 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 123:
#line 473 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 124:
#line 477 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 125:
#line 481 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 126:
#line 485 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 127:
#line 486 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 128:
#line 493 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 129:
#line 497 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 130:
#line 504 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 131:
#line 508 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 132:
#line 512 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 133:
#line 516 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 135:
#line 524 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 136:
#line 528 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 137:
#line 529 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 138:
#line 530 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 139:
#line 531 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 140:
#line 532 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 141:
#line 533 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 142:
#line 534 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 143:
#line 535 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 144:
#line 536 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.StructureReturn; }
  break;
case 145:
#line 537 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoAlias; }
  break;
case 146:
#line 541 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 154:
#line 564 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 155:
#line 565 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 156:
#line 566 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 157:
#line 567 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 158:
#line 568 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 159:
#line 569 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 160:
#line 570 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 161:
#line 571 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 162:
#line 572 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 163:
#line 573 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 164:
#line 577 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 165:
#line 578 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 166:
#line 579 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 167:
#line 580 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 168:
#line 581 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 169:
#line 582 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 170:
#line 583 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 171:
#line 584 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 172:
#line 585 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 173:
#line 586 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 174:
#line 587 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 175:
#line 588 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 176:
#line 589 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 177:
#line 590 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 178:
#line 591 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 179:
#line 592 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 180:
#line 596 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 184:
#line 606 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 185:
#line 610 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 186:
#line 614 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 618 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 188:
#line 622 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 189:
#line 626 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 190:
#line 630 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 191:
#line 634 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 193:
#line 642 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 194:
#line 643 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 195:
#line 644 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 196:
#line 645 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 197:
#line 646 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 198:
#line 647 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 199:
#line 648 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 200:
#line 649 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 201:
#line 650 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 202:
#line 657 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 664 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 668 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 205:
#line 675 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 682 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 686 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 693 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 704 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 708 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 715 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 719 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 726 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 730 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 216:
#line 734 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 738 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 745 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 749 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 756 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 760 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 764 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 768 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 776 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 226:
#line 777 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 227:
#line 784 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 788 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 795 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 230:
#line 799 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 231:
#line 803 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 232:
#line 807 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 233:
#line 811 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 234:
#line 815 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 235:
#line 819 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 237:
#line 824 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 238:
#line 828 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 239:
#line 832 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 240:
#line 836 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 241:
#line 840 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 246:
#line 857 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 861 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 867 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 249:
#line 874 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 878 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 885 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 903 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 258:
#line 910 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 914 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 918 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 922 "Iril/IR/IR.jay"
  {
        yyVal = new InvokeInstruction ((LType)yyVals[-6+yyTop], (Value)yyVals[-5+yyTop], (IEnumerable<Argument>)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 265:
#line 938 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 266:
#line 945 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 949 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 953 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 269:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 270:
#line 961 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 965 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 272:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 273:
#line 973 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 977 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 275:
#line 981 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 276:
#line 985 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 277:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 278:
#line 993 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 279:
#line 997 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 280:
#line 1001 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 281:
#line 1005 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 282:
#line 1009 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 283:
#line 1013 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 284:
#line 1017 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 285:
#line 1021 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 286:
#line 1025 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 287:
#line 1029 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1033 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1037 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1041 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 304:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 305:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 306:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 307:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 308:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1129 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1133 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1137 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1141 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1145 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1149 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1153 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1157 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1161 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1165 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1169 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 323:
#line 1173 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 324:
#line 1177 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1181 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1185 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1189 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1193 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1197 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 330:
#line 1201 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 331:
#line 1205 "Iril/IR/IR.jay"
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

void case_39()
#line 189 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_40()
#line 194 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_102()
#line 380 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_103()
#line 385 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-9+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-8+yyTop], (IEnumerable<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_104()
#line 390 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_105()
#line 395 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_106()
#line 400 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-10+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-9+yyTop], (IEnumerable<Parameter>)yyVals[-8+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_107()
#line 405 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-11+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-10+yyTop], (IEnumerable<Parameter>)yyVals[-9+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_108()
#line 410 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-8+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,    6,    6,    6,   10,
   10,   16,   16,   16,   16,   16,   16,   15,    9,    9,
   17,   17,   17,   17,   17,   18,   21,   21,   22,   23,
   23,   23,   23,   23,   13,   13,    8,    8,    8,    8,
    8,   25,   25,   25,    7,    7,   27,   27,   27,   27,
   27,   27,   27,   27,   27,   27,   27,   27,    3,    3,
    3,   28,   28,   29,   29,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   30,   30,   31,
   31,    4,    4,    4,    4,    4,    4,    4,   32,   32,
   32,   32,   38,   38,   38,   38,   38,   38,   38,   36,
    5,    5,    5,    5,    5,   33,   33,   42,   42,   43,
   43,   43,   43,   41,   41,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   14,   14,   39,   39,
   34,   34,   44,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   46,   46,   46,   46,   46,   46,   46,
   46,   46,   46,   46,   46,   46,   46,   46,   46,   47,
   48,   48,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   51,   19,   19,   19,   19,   19,   19,   19,   19,
   19,   52,   26,   26,   53,   50,   50,   24,   54,   49,
   49,   35,   35,   55,   55,   55,   55,   56,   56,   58,
   58,   58,   58,   60,   61,   61,   62,   62,   63,   63,
   63,   63,   63,   63,   63,   64,   64,   64,   64,   64,
   64,   20,   20,   65,   65,   66,   66,   67,   68,   68,
   69,   70,   70,   71,   71,   40,   72,   57,   57,   57,
   57,   57,   57,   57,   57,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   10,   11,    9,   10,    8,    5,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    3,    3,    3,    6,    5,    1,    1,    3,    1,    1,
    1,    1,    1,    1,    2,    3,    1,    2,    3,    3,
    3,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    3,    1,    1,    1,    4,    2,    3,
    5,    1,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    3,    4,    2,    1,    5,    5,    1,    3,    1,
    1,    7,   10,    8,    8,   11,   12,    9,    2,    3,
    3,    4,    1,    1,    1,    1,    2,    3,    2,    2,
    5,    6,    6,    7,    8,    3,    2,    1,    3,    1,
    2,    1,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    4,    1,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    6,    9,    6,    6,    3,    3,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    2,    2,    1,    2,    1,    3,    2,    1,    1,
    3,    1,    2,    2,    3,    1,    2,    1,    2,    1,
    2,    3,    4,    1,    3,    2,    1,    3,    2,    3,
    3,    3,    2,    4,    5,    1,    1,    6,    9,    6,
    6,    1,    3,    1,    1,    1,    3,    5,    1,    2,
    3,    1,    2,    1,    1,    1,    1,    2,    7,    8,
    9,    2,    2,    7,    1,    5,    6,    5,    7,    5,
    5,    6,    4,    4,    5,    6,    6,    5,    6,    6,
    6,    7,    5,    6,    7,    7,    4,    5,    6,    5,
    2,    5,    4,    4,    4,    4,    5,    6,    7,    6,
    6,    4,    3,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   85,   95,   88,   89,   90,   91,   87,  113,   34,
   32,   35,   36,   37,  256,  141,  142,  143,    0,   33,
  144,  136,  137,  139,  138,  140,  149,  150,    0,    0,
    0,   86,    0,    0,    0,    0,    0,  114,  115,    0,
    0,  134,    0,    0,    3,    0,    4,    0,    0,  147,
  148,   30,   31,   38,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   79,    0,    0,    0,    0,    0,    0,
   94,    0,  119,    0,    0,  145,    0,    0,    0,    0,
    0,    0,  135,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    8,    0,    7,    0,    0,    0,    0,
    0,   80,    0,    0,    0,    0,  118,  101,   92,    0,
    0,   98,    0,    0,    0,    0,    0,    0,  132,  133,
  127,    0,    0,  128,  153,    0,    0,  151,  195,  196,
  194,  197,  198,  199,  193,  184,  201,  200,    0,    0,
    0,    0,    0,    0,    0,    0,  183,    0,    0,    0,
    0,    0,    0,    0,    0,   39,    0,    0,    0,   64,
   63,   13,    0,    0,   57,   62,  146,    0,    0,    0,
    0,   93,    0,    0,    0,    0,    0,    0,    0,    0,
   77,   69,   67,   68,   70,   71,   72,   73,    0,   65,
    0,  126,    0,    0,    0,    0,    0,    0,  152,    0,
    0,    0,    0,  206,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   58,   14,    0,  180,  182,  181,  203,   96,   81,   97,
   99,    0,    0,    0,    0,    0,    0,   12,   66,  129,
    0,    0,    0,   55,    0,    0,    0,    0,    0,    0,
    0,  265,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  212,    0,    0,
  218,    0,    0,    0,    0,    0,    0,    0,  191,    0,
  189,  190,    0,    0,    0,    0,    0,    0,    0,   52,
    0,   50,    0,   41,   53,    0,   47,   49,   54,   42,
   43,   40,   17,   16,   61,   60,   59,    0,    0,   74,
  245,  244,    0,  242,    0,    0,    0,    0,    0,  263,
    0,    0,  258,    0,    0,    0,  262,  254,  255,    0,
    0,  252,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  257,  291,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  154,  155,
  156,  157,  158,  159,  160,  161,  162,  163,    0,  164,
  165,  176,  177,  178,  179,  167,  169,  170,  171,  172,
  168,  166,  174,  175,  173,    0,    0,    0,    0,    0,
    0,    0,    0,  102,  213,    0,  219,    0,    0,   56,
    0,    0,    0,    0,    0,  207,    0,    0,    0,   28,
    0,    0,    0,    0,  208,    0,    0,   78,    0,    0,
    0,    0,  105,    0,    0,    0,  202,    0,    0,  224,
    0,    0,    0,    0,  253,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  246,    0,    0,    0,    0,    0,    0,
    0,    0,  303,    0,  104,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,   45,    0,   51,   48,  243,
  120,    0,    0,  108,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  287,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  188,  185,  187,    0,    0,    0,    0,
   44,    0,    0,  103,    0,    0,    0,  226,    0,    0,
  227,    0,    0,  266,    0,  288,  324,    0,  297,  309,
    0,  292,  327,    0,  313,  290,  329,  321,  317,    0,
    0,  306,    0,  271,  270,  308,  330,    0,    0,  268,
    0,    0,  192,  205,    0,    0,    0,    0,    0,    0,
    0,    0,  247,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  106,    0,    0,    0,    0,
  249,    0,    0,    0,  229,  225,    0,    0,    0,  267,
  325,  310,  314,  318,  307,  272,  301,  319,    0,    0,
    0,    0,    0,    0,  209,    0,  210,  300,  289,    0,
    0,    0,    0,  277,    0,    0,    0,    0,    0,  107,
  259,    0,  264,  250,    0,  237,  231,    0,    0,    0,
    0,  236,  232,  230,  228,    0,    0,  269,    0,  304,
    0,  322,    0,    0,  248,  315,    0,    0,  286,  186,
  251,  234,    0,    0,    0,    0,    0,  260,    0,  305,
  323,  211,  235,    0,    0,    0,    0,  261,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  241,  238,  240,
    0,    0,  239,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  209,  183,  175,   75,
  184,  245,  218,   76,   77,   54,  176,  344,  167,  363,
  346,  347,  348,  349,  185,  755,  210,   86,   87,  131,
  132,   15,  106,  147,  317,  492,   62,   57,   58,   59,
   63,  143,  144,  148,  439,  456,  246,  500,  756,  225,
  704,  373,  623,  757,  318,  319,  320,  321,  322,  501,
  590,  670,  671,  783,  364,  553,  554,  730,  731,  381,
  382,  414,
  };
  protected static readonly short [] yySindex = {          -76,
    3, -163,   60,   71,   77, 3442, 3564, -231,    0,  -76,
    0,    0,    0,    0, -171, -118,  100,  125,  905, -133,
  -21,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  166,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4010, -101,
  -46,    0,  174, -177,  190, 4110, 3628,    0,    0, 3628,
  -22,    0, 3628,  205,    0,  203,    0,   29,   51,    0,
    0,    0,    0,    0, 4110, -128,  -55,  -69,   -1,  291,
  -20,  216,   85,    0,  174,    2,  190,   90, 4110,  111,
    0,   84,    0, 3735,  190,    0,  190, 4110,  -17, 3628,
  203,  -12,    0,  270,  123, -102,    0,    0, 2197, 4110,
 -128, 4110, -128,    0,  272,    0, -244,  360,  279, 3967,
  369,    0, 4110, 4110,   24, 4110,    0,    0,    0,  174,
  116,    0,  190,  203,   18, -102,  203, 4048,    0,    0,
    0, 2339,  135,    0,    0,   97, -115,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   44,  377,
  379,  380, 4143, 4143, 4143,  378,    0, 2197, 4110, 2133,
 4110,  366,  367,  370,  154,    0, -244, 3962,    0,    0,
    0,    0,  -25,  489,    0,    0,    0,  174,   48,  361,
   -4,    0, 3846,   97,  203,   97,   97,  -55,  368,  390,
    0,    0,    0,    0,    0,    0,    0,    0,  242,    0,
 3581,    0, 3839, -112,  157,  175, 5376,  -99,    0,  394,
 4143, 4143, 4143,    0,   28,   92,   16,   62,  392, 2197,
   66,  396, 2178, 3901,  171,  834,    0, -244,  280,   43,
    0,    0, 3995,    0,    0,    0,    0,    0,    0,    0,
    0,   97,  -55,   97,   97,  183,  595,    0,    0,    0,
  191, 5376,  -97,    0,  173,  388, 4143, -170, 4143, 3823,
 4143,    0, 2083, 4110, 2083, 4110, 2083, 4110, 4110, 2323,
 4110, 4110, 4110, 2083, 2351, 2356, 4110, 4110, 4110, 4143,
 4143, 4143, 4110, 1292, 3759,  126,  -44, 4143, 4143, 4143,
 4143, 4143, 4143, 4143, 4143, 4143, 4143, 4143, 4143, 1633,
 2291, 4110, 4110, 3499,   37, 4110, 2375,    0, 5376,  173,
    0,  173,  179, 5376, 4110,   81,   82,   89,    0, 4143,
    0,    0,  214,   88,  428,  221,  108,  110,  429,    0,
  445,    0,  621,    0,    0,  364,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   97,   97,    0,
    0,    0,  282,    0, -212, 2466, 5376,  -94, 5816,    0,
  217, 2284,    0,  447,  386, 4110,    0,    0,    0,  489,
 2083,    0,  489,  489, 2083,  489,  489, 2083,  489,  489,
 4110,  489,  489,  489,  489,  489, 2083, 4110,  489, 4110,
  489,  489,  489,  489,  448,  449,  450,   86, 4110,  283,
 4143,  451,    0,    0, 4110,  316,  117,  118,  122,  129,
  130,  131,  132,  134,  136,  138,  144,  145,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4110,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4110,   40,  489,  386, 4110,
 3628, 3499,   -7,    0,    0,  173,    0,  215,  215,    0,
 2557,  319, 4110, 4110, 4110,    0,  173,  240,  155,    0,
  273,  289,  172, 2404,    0, 3934,   97,    0,  595, 4143,
  -92,  173,    0, 2648, 5376,  173,    0,  507,  286,    0,
  514,  386,  515,  489,    0,  519,  525,  489,  527,  543,
  489,  545,  546,  489,  550,  551,  569,  570,  572,  489,
  489,  573,  489,  575,  576,  579,  581, 4143, 4143, 4143,
  732,  322, 4110,  582, 4110,  329, 4143, 4110, 4110, 4110,
 4110, 4110, 4110, 4110, 4110, 4110, 4110, 4110, 4110,  489,
  489, 2284,  584,    0,  585,  514,  386,  386, 4110,  386,
 4110, 3628,    0,  215,    0, 4143,  182,  210,  335,  215,
  173,  373,  173,    0,  375,    0,  288,    0,    0,    0,
    0, 5376,  -90,    0, 2739,  215,  286,  548, 3799,  260,
  514, 2284,  598, 2284, 2284,  608, 2284, 2284,  611, 2284,
 2284,  620, 2284, 2284, 2284, 2284, 2284,  623,  624, 2284,
  625, 2284, 2284, 2284, 2284,    0,  627,  629,  418,  632,
 4110,  489,  633, 4110,  635, 4143,  636,  174,  174,  174,
  174,  174,  174,  174,  174,  174,  174,  174,  174,  645,
  651,  652,  607, 4143,   97,  514,  514,  386,  514,  386,
  386, 4110,  659,    0,    0,    0,  215,  173,  215,  173,
    0, 2830, 5376,    0,  660, 4110, 4049,    0, 2045,  292,
    0,  286,  324,    0, 2284,    0,    0, 2284,    0,    0,
 2284,    0,    0, 2284,    0,    0,    0,    0,    0, 2284,
 2284,    0, 2284,    0,    0,    0,    0, 4143, 4143,    0,
  333,  663,    0,    0,  336,  664,  337,  667, 4143, 2284,
 2284, 2284,    0,  672,   97,   97,   97,  514,   97,  514,
  514,  386, 4143,  215,  215,    0, 2921,  286,  676, 4055,
    0,  682, 2013, 2223,    0,    0, 4077,  402,  286,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  468,  353,
  470,  357,  476, 4143,    0,  690,    0,    0,    0,  643,
 4143,   97,   97,    0,   97,   97,   97,  514,  294,    0,
    0,  286,    0,    0, 3789,    0,    0,  371,  699,  700,
  703,    0,    0,    0,    0,  286,  423,    0,  492,    0,
  496,    0,  690, 4143,    0,    0,   97,   97,    0,    0,
    0,    0,  296,  715, 4143, 4143, 4143,    0,  286,    0,
    0,    0,    0, 4110,  376,  382,  383,    0,  330, 4110,
 4110, 4110, 4143,  339,  342,  347,  716,    0,    0,    0,
 4143,  300,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  765,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  456, 3644,  494,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    8,    0,    0,    0,    0,    0,
    0, 3699,    0,    0,  495,    0,  497,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  730,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  160,
    0,    0,  499,    0,    0,    0,    0,    0,    0,    0,
    0,  204,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  730,    0,   27,
    0,    0,    0,    0,    0,    0,    0,    0,  697,    0,
    0,    0,    0,  730,    0,    0,    0,    9,  730,    0,
  730,    0,    0,    0,    0,    0,   25,    0, 3714, 4029,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  306,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  730,
    0,    0,  730,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   80,    0,  169,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3012,
    0, 5467,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  232,    0,    0,    0,    0,    0,
   61,    0,  730,    0,    0,  310,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  309,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  730,
    0,    0,  730,  730,    0,  730,  730,    0,  730,  730,
    0,  730,  730,  730,  730,  730,    0,    0,  730,    0,
  730,  730,  730,  730,    0,    0,    0,  730,    0,  730,
    0,    0,    0,    0,    0,  730,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  730,  730,    0,    0,
    0,    0,  730,    0,    0, 3103,    0, 3194, 5558,    0,
    0,  730,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  365,    0,    0,    0,
    0,    0,    0,    0,    0, 5649,    0,    0,    0,    0,
    0,    0,    0,  730,    0,    0,    0,  730,    0,    0,
  730,    0,    0,  730,    0,    0,    0,    0,    0,  730,
  730,    0,  730,    0,    0,    0,    0,    0,    0,    0,
    0,  730,    0,    0,    0,  730,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  730,
  730,    0, 4102,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3285,    0,    0,  730,  730,  730,  508,
    0,    0,  650,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 5740,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  730,    0,    0,    0,    0,    0,  797,  888,  979,
 1076, 1167, 1258, 1349, 1440, 1531, 1622, 1713, 1804,    0,
    0,    0,    0,    0, 4193,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  662,  674,  686, 1129,
    0,    0,    0,    0,    0,    0,    0,    0,  730,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4284,    0, 4375,    0, 4466,    0,
    0,    0,    0, 1940, 1975,    0,    0,    0,    0,    0,
    0,  315,  730,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4557,    0,    0,    0,    0,
    0, 4648, 4739,    0, 4830, 4921, 5012,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 5103,    0,    0,    0, 5194, 5285,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  730,    0,
    0,    0,    0,  730,  730,  730,    0,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  768,  705,    0,    0,    0,    0,  601,  605,   59,
   -6,  -75, -174,   67,    0,  766,  549,    0, -227, -474,
    0,  302,    0, -627,    0,  301,  577,  704,   14,    0,
  596,    0,  -56, -131, -248,    0,   -2,    0,  740,  -51,
  -58,    0,  586, -141,    0,    0,    0,  298, -679,  248,
    0, 1523, -498,    1, -300,    0,  479,  481,  432, -417,
 1857,    0,   68,    0,  314,    0,  161,    0,   83,  -67,
 -207,    0,
  };
  protected static readonly short [] yyTable = {            53,
   53,  100,   93,   56,  197,  219,  345,  217,  351,  577,
  262,   82,  119,  366,  214,   50,  465,   94,  243,   55,
   61,   89,   94,  324,  121,  367,   29,   94,  495,  362,
  582,  172,  663,  166,   91,  173,  625,   91,  729,  263,
  127,  556,   85,  769,  136,  123,   51,   84,   84,   53,
   53,   82,   83,   53,   98,  219,   53,   94,  115,  330,
  103,  215,  252,   16,  254,  465,   84,  123,  109,   95,
   97,  330,  219,   99,  793,  471,  102,  194,   49,  122,
  198,   91,   85,  211,  591,   64,  243,  130,  250,   91,
  368,   53,  229,   53,  232,  371,  490,  103,  142,  242,
   66,   51,  729,  168,   51,  170,   17,   18,  332,  248,
  219,  133,  219,  135,  372,  485,  188,  189,  494,  191,
   19,   49,  702,  359,   35,  706,  122,   91,  174,  531,
  552,   20,   82,   83,  110,  330,  112,   21,  253,  646,
  647,   67,  649,  111,  113,  468,   79,  469,  190,   47,
   48,  832,  329,  331,  335,   88,  192,  339,  215,  193,
   68,  215,  230,  141,  233,   72,   73,  354,  123,  169,
  465,  171,  146,  505,  323,  212,  323,  505,  213,  323,
  505,  323,   50,  323,   51,   69,  130,   70,   71,  505,
  491,    1,    2,  465,  237,    3,    4,  238,    5,   84,
  100,  145,  196,  100,  145,   83,  142,  385,  103,  388,
   90,    6,    7,   51,  145,   91,  397,  219,  376,   22,
   70,   71,  654,   91,   72,   73,  487,  343,   23,   94,
  718,   21,  720,  721,   70,   71,   24,   25,   26,   27,
   28,    8,  105,   84,  130,   49,  585,  130,  241,  101,
  655,   91,   80,  118,  134,   81,  362,  216,  345,  137,
  261,  362,  461,   53,  255,  104,  380,  383,  384,  386,
  387,  389,  390,  392,  393,  394,  395,  396,  399,  401,
  402,  403,  404,  375,  465,  114,  408,  410,  107,  195,
  416,  564,  121,  121,   29,   29,  121,  121,   29,  121,
  803,   29,  570,  563,  768,  457,  458,   53,  124,  463,
  108,  460,  121,  121,   29,   29,  241,  583,  472,  358,
  353,  586,  488,  238,   91,  489,  533,  459,  661,  415,
  117,  489,  736,  662,  800,  737,  813,  794,  120,  489,
  833,  121,  121,  794,   29,  219,  131,  122,  122,  131,
   46,  122,  122,   46,  122,  233,  124,   91,  233,  537,
   91,  465,  566,   91,  125,  621,  258,  122,  122,   53,
   91,   91,  626,  823,  504,  656,   91,  126,  508,  828,
   91,  511,  829,   91,  514,   35,   22,  830,   91,  502,
  520,  521,  138,  523,   89,   23,  657,  122,  659,  177,
  139,  178,  532,   24,   25,   26,   27,   28,  536,  187,
  562,  226,  227,  145,  727,  140,  221,  220,  222,  223,
  186,  228,  249,  234,  235,   94,  465,  236,  256,  257,
  264,  265,  550,  325,  333,  334,  123,  123,  336,  337,
  123,  123,  360,  123,  350,  164,  215,  365,  369,  551,
  462,  413,  470,   53,   53,   53,  123,  123,  559,  561,
  478,  473,  474,  224,  224,  224,  567,  568,  569,  475,
  477,  479,  483,  557,  558,  560,  165,  480,  186,  343,
  481,  247,  482,  724,  484,  725,  123,  486,  323,  497,
  499,  528,  529,  530,  535,   84,  571,  538,  539,   21,
   21,  199,  540,   21,   21,  782,   21,   18,  163,  541,
  542,  543,  544,  715,  545,   84,  546,  200,  547,   21,
   21,  326,  327,  328,  548,  549,  622,  572,  622,  573,
   91,  628,  629,  630,  631,  632,  633,  634,  635,  636,
  637,  638,  639,  357,  575,  574,   84,  362,  164,   21,
  587,  371,   53,  589,   53,   53,  201,  202,  592,  652,
  203,  204,  594,  205,  206,  207,  208,  370,  595,  374,
  597,  377,  648,  219,  650,  651,  124,  124,   84,  165,
  124,  124,  669,  124,  762,  763,  598,  765,  600,  601,
  405,  406,  407,  603,  604,  412,  124,  124,  417,  418,
  419,  420,  421,  422,  423,  424,  425,  426,  427,  428,
  734,  163,  605,  606,  622,  607,  610,  622,  612,  613,
  219,  219,  614,  219,  615,  624,  124,  643,  644,  658,
  476,  660,  125,  125,  797,  798,  125,  125,  666,  125,
  672,  675,  149,  150,  151,   53,  152,  153,  154,   26,
  155,  678,  125,  125,  681,  219,  219,  244,  156,  343,
  733,   19,   91,  684,  157,  722,  690,  691,  693,  498,
  698,  158,  699,   20,  700,  701,  705,  503,  707,  709,
  506,  507,  125,  509,  510,   27,  512,  513,  710,  515,
  516,  517,  518,  519,  711,  712,  522,  552,  524,  525,
  526,  527,  723,  728,  739,  749,  750,  752,  751,  753,
  754,  534,   84,   84,   84,  761,   84,   84,   84,  772,
   84,  775,  786,  343,  788,  789,  790,   84,   84,  791,
  669,  103,  792,  794,   84,  795,   85,  204,  805,  806,
  204,   84,  807,  809,  804,  149,  150,  151,  810,  152,
  153,  154,  811,  155,  814,  555,  820,  159,  204,  831,
  244,  156,  821,  822,    1,  109,  110,  157,  111,   84,
  112,  160,  161,  162,  158,   18,   18,   65,  240,   18,
   18,  239,   18,  116,   78,  259,  352,  579,  251,  204,
  581,   50,  125,   92,  812,   18,   18,  466,  260,  467,
  496,  593,  580,  713,  785,  596,    0,  819,  599,    0,
    0,  602,  774,  824,  825,  826,    0,  608,  609,    0,
  611,  204,   51,    0,    0,   18,    0,   84,  616,  617,
  618,  620,    0,    0,    0,    0,   84,  627,    0,    0,
    0,   84,   84,   84,    0,    0,    0,  640,  641,  642,
    0,  149,  150,  151,   49,  152,  153,  154,    0,  155,
  159,    0,    0,    0,    0,    0,  653,    0,    0,    0,
  361,    0,    0,  157,  160,  161,  162,  149,  150,  151,
  158,  152,  153,  154,    0,  155,    0,    0,    0,  674,
    0,  676,  677,    0,  679,  680,    0,  682,  683,  157,
  685,  686,  687,  688,  689,    0,  158,  692,    0,  694,
  695,  696,  697,    0,    0,    0,    0,   26,   26,  703,
    0,   26,   26,    0,   26,    0,  708,   84,    0,   19,
   19,    0,    0,   19,   19,    0,   19,   26,   26,    0,
    0,   20,   20,    0,  714,   20,   20,    0,   20,   19,
   19,    0,    0,   27,   27,    0,    0,   27,   27,    0,
   27,   20,   20,    0,    0,    0,  735,   26,    0,  204,
  204,    0,  740,   27,   27,  741,    0,    0,  742,   19,
    0,  743,    0,    0,    0,    0,    0,  744,  745,    0,
  746,   20,    0,    0,    0,  179,    0,    0,  747,  748,
    0,    0,    0,   27,   23,    0,    0,  758,  759,  760,
    0,    0,   24,   25,   26,   27,   28,    0,   84,    0,
    0,    0,    0,    0,    0,  204,  204,  204,    0,  204,
  204,  784,    0,    0,  204,    0,  204,    0,    0,  204,
  204,  204,  204,  204,  204,  204,  204,  204,  204,    0,
  204,  204,    0,  204,  204,  204,  204,  204,  204,  204,
    0,  796,  204,  204,  204,  204,    0,    0,  204,  326,
  326,    0,  204,  204,  204,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,    0,  204,    0,    0,    0,
  149,  150,  151,    0,  152,  153,  154,  204,  155,    0,
    0,    0,    0,    0,  619,  815,  816,  817,  204,  204,
  204,  204,  157,  204,    0,   84,    0,    0,    0,  158,
    0,    0,    0,  827,    0,  326,  326,  326,   24,  326,
  326,    0,    0,    0,  326,    0,  326,    0,    0,  326,
  326,  326,  326,  326,  326,  326,  326,  326,  326,    0,
  326,  326,    0,  326,  326,  326,  326,  326,  326,  326,
  331,  331,  326,  326,  326,  326,    0,    0,  326,    0,
    0,    0,  326,  326,  326,  326,  326,    0,  326,  326,
  326,  326,  326,  326,  326,    0,  326,    0,    0,    0,
    0,    0,    0,    0,   70,   71,    0,  326,   72,   73,
   74,   30,   31,   32,   33,   34,   84,    0,  326,  326,
  326,  326,   40,  326,    0,    0,  331,  331,  331,    0,
  331,  331,    0,    0,    0,  331,    0,  331,    0,    0,
  331,  331,  331,  331,  331,  331,  331,  331,  331,  331,
    0,  331,  331,    0,  331,  331,  331,  331,  331,  331,
  331,  316,  316,  331,  331,  331,  331,    0,    0,  331,
    0,    0,    0,  331,  331,  331,  331,  331,    0,  331,
  331,  331,  331,  331,  331,  331,    0,  331,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  331,    0,
    0,    0,    0,    0,    0,    0,    0,   84,    0,  331,
  331,  331,  331,    0,  331,    0,    0,  316,  316,  316,
    0,  316,  316,    0,    0,    0,  316,    0,  316,    0,
    0,  316,  316,  316,  316,  316,  316,  316,  316,  316,
  316,    0,  316,  316,    0,  316,  316,  316,  316,  316,
  316,  316,    0,    0,  316,  316,  316,  316,  296,  296,
  316,   50,    0,    0,  316,  316,  316,  316,  316,    0,
  316,  316,  316,  316,  316,  316,  316,    0,  316,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  316,
    0,    0,   51,    0,    0,    0,    0,    0,   84,    0,
  316,  316,  316,  316,    0,  316,   24,   24,    0,    0,
   24,   24,    0,   24,  296,  296,  296,    0,  296,  296,
    0,    0,    0,  296,   49,  296,   24,   24,  296,  296,
  296,  296,  296,  296,  296,  296,  296,  296,    0,  296,
  296,    0,  296,  296,  296,  296,  296,  296,  296,  293,
  293,  296,  296,  296,  296,    0,   24,  296,    0,    0,
    0,  296,  296,  296,  296,  296,    0,  296,  296,  296,
  296,  296,  296,  296,    0,  296,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  296,    0,    0,   84,
    0,    0,    0,    0,    0,    0,    0,  296,  296,  296,
  296,    0,  296,    0,    0,  293,  293,  293,    0,  293,
  293,    0,    0,    0,  293,    0,  293,    0,    0,  293,
  293,  293,  293,  293,  293,  293,  293,  293,  293,    0,
  293,  293,    0,  293,  293,  293,  293,  293,  293,  293,
  294,  294,  293,  293,  293,  293,    0,    0,  293,    0,
    0,    0,  293,  293,  293,  293,  293,    0,  293,  293,
  293,  293,  293,  293,  293,   22,  293,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,  293,    0,    0,
   84,    0,   24,   25,   26,   27,   28,    0,  293,  293,
  293,  293,    0,  293,    0,    0,  294,  294,  294,    0,
  294,  294,    0,    0,    0,  294,  409,  294,    0,    0,
  294,  294,  294,  294,  294,  294,  294,  294,  294,  294,
    0,  294,  294,    0,  294,  294,  294,  294,  294,  294,
  294,  295,  295,  294,  294,  294,  294,    0,    0,  294,
    0,    0,    0,  294,  294,  294,  294,  294,    0,  294,
  294,  294,  294,  294,  294,  294,    0,  294,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  294,    0,
    0,   84,    0,    0,    0,    0,    0,    0,    0,  294,
  294,  294,  294,    0,  294,    0,    0,  295,  295,  295,
    0,  295,  295,    0,    0,    0,  295,    0,  295,    0,
    0,  295,  295,  295,  295,  295,  295,  295,  295,  295,
  295,    0,  295,  295,    0,  295,  295,  295,  295,  295,
  295,  295,  328,  328,  295,  295,  295,  295,    0,    0,
  295,    0,    0,    0,  295,  295,  295,  295,  295,    0,
  295,  295,  295,  295,  295,  295,  295,    0,  295,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  295,
    0,    0,   84,    0,    0,    0,    0,    0,    0,    0,
  295,  295,  295,  295,    0,  295,    0,    0,  328,  328,
  328,    0,  328,  328,    0,    0,    0,  328,    0,  328,
    0,    0,  328,  328,  328,  328,  328,  328,  328,  328,
  328,  328,    0,  328,  328,    0,  328,  328,  328,  328,
  328,  328,  328,  320,  320,  328,  328,  328,  328,    0,
    0,  328,    0,    0,    0,  328,  328,  328,  328,  328,
    0,  328,  328,  328,  328,  328,  328,  328,    0,  328,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  328,    0,    0,   84,    0,    0,    0,    0,    0,    0,
    0,  328,  328,  328,  328,    0,  328,    0,    0,  320,
  320,  320,    0,  320,  320,    0,    0,    0,  320,    0,
  320,    0,    0,  320,  320,  320,  320,  320,  320,  320,
  320,  320,  320,    0,  320,  320,    0,  320,  320,  320,
  320,  320,  320,  320,  312,  312,  320,  320,  320,  320,
    0,    0,  320,    0,    0,    0,  320,  320,  320,  320,
  320,    0,  320,  320,  320,  320,  320,  320,  320,    0,
  320,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  320,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,  320,  320,  320,  320,    0,  320,    0,    0,
  312,  312,  312,    0,  312,  312,    0,    0,    0,  312,
    0,  312,    0,    0,  312,  312,  312,  312,  312,  312,
  312,  312,  312,  312,   25,  312,  312,    0,  312,  312,
  312,  312,  312,  312,  312,  302,  302,  312,  312,  312,
  312,    0,    0,  312,    0,    0,    0,  312,  312,  312,
  312,  312,    0,  312,  312,  312,  312,  312,  312,  312,
    0,  312,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  588,  312,  429,  430,  431,  432,  433,  434,  435,
  436,  437,  438,  312,  312,  312,  312,    0,  312,    0,
    0,  302,  302,  302,    0,  302,  302,    0,    0,    0,
  302,    0,  302,    0,   91,  302,  302,  302,  302,  302,
  302,  302,  302,  302,  302,    0,  302,  302,    0,  302,
  302,  302,  302,  302,  302,  302,  273,  273,  302,  302,
  302,  302,    0,    0,  302,    0,   91,    0,  302,  302,
  302,  302,  302,    0,  302,  302,  302,  302,  302,  302,
  302,    0,  302,    0,  164,    0,    0,    0,    0,  665,
    0,    0,    0,  302,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  302,  302,  302,  302,    0,  302,
    0,    0,  273,  273,  273,  165,  273,  273,    0,    0,
    0,  273,   50,  273,    0,    0,  273,  273,  273,  273,
  273,  273,  273,  273,  273,  273,    0,  273,  273,    0,
  273,  273,  273,  273,  273,  273,  273,  163,    0,  273,
  273,  273,  273,   51,   91,  273,  231,    0,    0,  273,
  273,  273,  273,  273,    0,  273,  273,  273,  273,  273,
  273,  273,  164,  273,  738,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  273,   49,    0,   22,   22,    0,
    0,   22,   22,    0,   22,  273,  273,  273,  273,   91,
  273,  338,    0,  165,    0,    0,    0,   22,   22,    0,
    0,    0,    0,    0,    0,    0,    0,  164,   91,    0,
    0,    0,   25,   25,    0,    0,   25,   25,    0,   25,
  771,    0,    0,    0,    0,  163,  164,   22,    0,    0,
    0,  787,   25,   25,    0,    0,    0,    0,  165,  149,
  150,  151,    0,  152,  153,  154,    0,  155,    0,    0,
    0,    0,  164,    0,  776,  777,    0,  165,    0,    0,
    0,  157,   25,    0,  801,    0,    0,    0,  158,    0,
  163,  149,  150,  151,    0,  152,  153,  154,  808,  155,
    0,    0,    0,  165,    0,    0,  244,  156,    0,  163,
    0,    0,    0,  157,    0,    0,    0,    0,    0,    0,
  158,  818,    0,    0,    0,    0,   96,    0,    0,    0,
    0,    0,    0,  164,    0,  163,   22,   36,   37,    0,
   38,   39,    0,    0,   41,   23,   42,   43,   44,   45,
   46,    0,    0,   24,   25,   26,   27,   28,    0,    0,
    0,    0,    0,    0,  165,    0,    0,    0,    0,    0,
   91,    0,   50,    0,  778,    0,    0,    0,    0,  149,
  150,  151,    0,  152,  153,  154,    0,  155,  779,  780,
  781,    0,    0,    0,    0,  156,  163,    0,    0,    0,
   50,  157,  645,   51,    0,   50,  159,    0,  158,    0,
    0,    0,    0,  378,  379,    0,    0,    0,    0,    0,
  160,  161,  162,    0,  149,  150,  151,    0,  152,  153,
  154,   51,  155,    0,  576,   49,   51,  673,    0,    0,
  156,    0,    0,  149,  150,  151,  157,  152,  153,  154,
    0,  155,    0,  158,    0,    0,    0,    0,    0,  156,
    0,    0,    0,   49,    0,  157,    0,    0,   49,  149,
  150,  151,  158,  152,  153,  154,    0,  155,    0,    0,
    0,    0,    0,    0,  244,  156,    0,    0,    0,  464,
    0,  157,  716,  717,  159,  719,    0,    0,  158,    0,
    0,    0,    0,    0,   96,    0,    0,    0,  160,  161,
  162,    0,    0,    0,    0,   36,   37,    0,   38,   39,
    0,    0,   41,    0,   42,   43,   44,   45,   46,    0,
  149,  150,  151,    0,  152,  153,  154,    0,  155,  159,
    0,  440,  441,    0,    0,  244,  156,    0,    0,    0,
    0,    0,  157,  160,  161,  162,    0,    0,  159,  158,
    0,    0,    0,    0,  764,    0,  766,  767,    0,    0,
    0,    0,  160,  161,  162,    0,   22,    0,    0,    0,
  493,    0,    0,    0,  159,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,  160,  161,
  162,    0,    0,    0,   22,    0,    0,    0,    0,   22,
    0,    0,    0,   23,  799,    0,    0,    0,   23,    0,
   96,   24,   25,   26,   27,   28,   24,   25,   26,   27,
   28,   36,   37,    0,   38,   39,    0,  266,   41,    0,
   42,   43,   44,   45,   46,  159,    0,    0,    0,    0,
  149,  150,  151,    0,  152,  153,  154,    0,  155,  160,
  161,  162,    0,    0,    0,    0,    0,    0,  391,  361,
    0,  565,  157,  442,  443,  444,  445,    0,    0,  158,
    0,    0,  446,  447,  448,  449,  450,  451,  452,  453,
  454,  455,    0,  267,  268,  269,  398,  270,  271,    0,
    0,  400,  272,    0,  273,    0,    0,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,    0,  284,  285,
    0,  286,  287,  288,  289,  290,  291,  292,  266,    0,
  293,  294,  295,  296,    0,    0,  297,    0,    0,    0,
  298,  299,  300,  301,  302,    0,  303,  304,  305,  306,
  307,  308,  309,    0,  310,    0,    0,    0,    0,    0,
    0,    0,  584,    0,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,  313,  314,  315,
    0,  316,    0,    0,  267,  268,  269,    0,  270,  271,
    0,    0,    0,  272,    0,  273,    0,    0,  274,  275,
  276,  277,  278,  279,  280,  281,  282,  283,    0,  284,
  285,    0,  286,  287,  288,  289,  290,  291,  292,  266,
    0,  293,  294,  295,  296,    0,    0,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,  304,  305,
  306,  307,  308,  309,    0,  310,    0,    0,    0,    0,
    0,    0,    0,  664,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,  313,  314,
  315,    0,  316,    0,    0,  267,  268,  269,    0,  270,
  271,    0,    0,    0,  272,    0,  273,    0,    0,  274,
  275,  276,  277,  278,  279,  280,  281,  282,  283,    0,
  284,  285,    0,  286,  287,  288,  289,  290,  291,  292,
  266,    0,  293,  294,  295,  296,    0,    0,  297,    0,
    0,    0,  298,  299,  300,  301,  302,    0,  303,  304,
  305,  306,  307,  308,  309,    0,  310,    0,    0,    0,
    0,    0,    0,    0,  726,    0,    0,  311,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  312,  313,
  314,  315,    0,  316,    0,    0,  267,  268,  269,    0,
  270,  271,    0,    0,    0,  272,    0,  273,    0,    0,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
    0,  284,  285,    0,  286,  287,  288,  289,  290,  291,
  292,  266,    0,  293,  294,  295,  296,    0,    0,  297,
    0,    0,    0,  298,  299,  300,  301,  302,    0,  303,
  304,  305,  306,  307,  308,  309,    0,  310,    0,    0,
    0,    0,    0,    0,    0,  770,    0,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  313,  314,  315,    0,  316,    0,    0,  267,  268,  269,
    0,  270,  271,    0,    0,    0,  272,    0,  273,    0,
    0,  274,  275,  276,  277,  278,  279,  280,  281,  282,
  283,    0,  284,  285,    0,  286,  287,  288,  289,  290,
  291,  292,  266,    0,  293,  294,  295,  296,    0,    0,
  297,    0,    0,    0,  298,  299,  300,  301,  302,    0,
  303,  304,  305,  306,  307,  308,  309,    0,  310,    0,
    0,    0,    0,    0,    0,    0,  216,    0,    0,  311,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  312,  313,  314,  315,    0,  316,    0,    0,  267,  268,
  269,    0,  270,  271,    0,    0,    0,  272,    0,  273,
    0,    0,  274,  275,  276,  277,  278,  279,  280,  281,
  282,  283,    0,  284,  285,    0,  286,  287,  288,  289,
  290,  291,  292,  266,    0,  293,  294,  295,  296,    0,
    0,  297,    0,    0,    0,  298,  299,  300,  301,  302,
    0,  303,  304,  305,  306,  307,  308,  309,    0,  310,
    0,    0,    0,    0,    0,    0,    0,  214,    0,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  312,  313,  314,  315,    0,  316,    0,    0,  267,
  268,  269,    0,  270,  271,    0,    0,    0,  272,    0,
  273,    0,    0,  274,  275,  276,  277,  278,  279,  280,
  281,  282,  283,    0,  284,  285,    0,  286,  287,  288,
  289,  290,  291,  292,  216,    0,  293,  294,  295,  296,
    0,    0,  297,    0,    0,    0,  298,  299,  300,  301,
  302,    0,  303,  304,  305,  306,  307,  308,  309,    0,
  310,    0,    0,    0,    0,    0,    0,    0,  217,    0,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,  313,  314,  315,    0,  316,    0,    0,
  216,  216,  216,    0,  216,  216,    0,    0,    0,  216,
    0,  216,    0,    0,  216,  216,  216,  216,  216,  216,
  216,  216,  216,  216,    0,  216,  216,    0,  216,  216,
  216,  216,  216,  216,  216,  214,    0,  216,  216,  216,
  216,    0,    0,  216,    0,    0,    0,  216,  216,  216,
  216,  216,    0,  216,  216,  216,  216,  216,  216,  216,
    0,  216,    0,    0,    0,    0,    0,    0,    0,  215,
    0,    0,  216,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  216,  216,  216,  216,    0,  216,    0,
    0,  214,  214,  214,    0,  214,  214,    0,    0,    0,
  214,    0,  214,    0,    0,  214,  214,  214,  214,  214,
  214,  214,  214,  214,  214,    0,  214,  214,    0,  214,
  214,  214,  214,  214,  214,  214,  217,    0,  214,  214,
  214,  214,    0,    0,  214,    0,    0,    0,  214,  214,
  214,  214,  214,    0,  214,  214,  214,  214,  214,  214,
  214,    0,  214,    0,    0,    0,    0,    0,    0,    0,
    0,   50,    0,  214,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  214,  214,  214,  214,    0,  214,
    0,    0,  217,  217,  217,    0,  217,  217,    0,    0,
    0,  217,   51,  217,    0,    0,  217,  217,  217,  217,
  217,  217,  217,  217,  217,  217,    0,  217,  217,    0,
  217,  217,  217,  217,  217,  217,  217,  215,   50,  217,
  217,  217,  217,    0,   49,  217,    0,    0,    0,  217,
  217,  217,  217,  217,    0,  217,  217,  217,  217,  217,
  217,  217,    0,  217,    0,    0,    0,    0,    0,   51,
    0,    0,    0,    0,  217,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  217,  217,  217,  217,    0,
  217,    0,    0,  215,  215,  215,    0,  215,  215,    0,
    0,   49,  215,   50,  215,    0,    0,  215,  215,  215,
  215,  215,  215,  215,  215,  215,  215,    0,  215,  215,
    0,  215,  215,  215,  215,  215,  215,  215,    0,    0,
  215,  215,  215,  215,   51,    0,  215,    0,    0,    0,
  215,  215,  215,  215,  215,    0,  215,  215,  215,  215,
  215,  215,  215,    0,  215,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  215,   49,   50,    0,    0,
    0,    0,    0,    0,    0,    0,  215,  215,  215,  215,
    0,  215,    0,  116,    0,   22,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,    0,   51,    0,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,    0,   29,  116,    0,    0,    0,   30,   31,
   32,   33,   34,   35,   36,   37,    0,   38,   39,   40,
   49,   41,    0,   42,   43,   44,   45,   46,  117,    0,
    0,    0,   22,    0,    0,    0,  116,    0,   47,   48,
    0,   23,    0,    0,    0,  129,    0,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,  117,
   96,    0,    0,    0,   50,    0,    0,    0,    0,    0,
   35,   36,   37,    0,   38,   39,    0,    0,   41,    0,
   42,   43,   44,   45,   46,    0,    0,    0,   50,    0,
    0,  117,    0,    0,    0,   51,    0,   22,    0,  802,
    0,    0,    0,    0,    0,    0,   23,    0,   75,  668,
    0,    0,    0,    0,   24,   25,   26,   27,   28,   51,
    0,    0,    0,    0,    0,   60,    0,   49,   50,    0,
    0,    0,    0,    0,    0,    0,   36,   37,    0,   38,
   39,    0,   96,   41,    0,   42,   43,   44,   45,   46,
    0,   49,   50,   36,   37,    0,   38,   39,    0,   51,
   41,   22,   42,   43,   44,   45,   46,    0,   50,    0,
   23,    0,    0,    0,    0,   50,    0,  116,   24,   25,
   26,   27,   28,   51,    0,    0,  116,    0,    0,   96,
    0,   49,    0,    0,  116,  116,  116,  116,  116,   51,
   36,   37,    0,   38,   39,  116,   51,   41,    0,   42,
   43,   44,   45,   46,    0,   49,  116,  116,    0,  116,
  116,    0,    0,  116,    0,  116,  116,  116,  116,  116,
   50,   49,  117,    0,    0,    0,    0,    0,   49,    0,
    0,  117,    0,   75,    0,    0,    0,    0,    0,  117,
  117,  117,  117,  117,    0,    0,    0,    0,    0,   75,
  117,   51,    0,   50,    0,    0,    0,    0,   22,    0,
    0,  117,  117,    0,  117,  117,    0,   23,  117,    0,
  117,  117,  117,  117,  117,   24,   25,   26,   27,   28,
    0,   50,  179,   49,   51,    0,   50,  128,   75,   75,
    0,   23,   75,   75,    0,   75,   75,   75,   75,   24,
   25,   26,   27,   28,    0,  149,  150,  151,    0,  152,
  153,  154,   51,  155,   50,    0,   49,   51,    0,    0,
    0,    0,   22,  411,  361,    0,    0,  157,    0,   50,
    0,   23,    0,    0,  158,    0,  667,    0,    0,   24,
   25,   26,   27,   28,   49,   51,   22,    0,    0,   49,
    0,  182,    0,    0,    0,   23,    0,    0,    0,    0,
   51,    0,   22,   24,   25,   26,   27,   28,   50,   22,
    0,   23,    0,    0,   50,    0,  139,   49,   23,   24,
   25,   26,   27,   28,   35,    0,   24,   25,   26,   27,
   28,  140,   49,    0,   84,    0,   50,    0,  128,   51,
    0,    0,    0,    0,    0,   51,    0,  773,    0,    0,
    0,    0,    0,   76,    0,    0,    0,  149,  150,  151,
  340,  152,  153,  154,   22,  155,    0,   51,    0,   50,
    0,   49,    0,   23,  341,    0,  342,   49,    0,  157,
    0,   24,   25,   26,   27,   28,  158,    0,    0,    0,
  149,  150,  151,  340,  152,  153,  154,   22,  155,   49,
   51,    0,   50,    0,    0,    0,   23,  578,    0,  342,
    0,    0,  157,    0,   24,   25,   26,   27,   28,  158,
    0,    0,    0,    0,    0,  179,  180,    0,    0,    0,
  179,  180,   49,   51,   23,  181,    0,    0,    0,   23,
  181,    0,   24,   25,   26,   27,   28,   24,   25,   26,
   27,   28,    0,    0,    0,    0,    0,    0,  179,  355,
    0,    0,    0,    0,    0,   49,    0,   23,  356,    0,
    0,    0,    0,   22,    0,   24,   25,   26,   27,   28,
    0,    0,   23,    0,    0,    0,    0,    0,   76,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   76,    0,    0,  199,    0,    0,
    0,    0,   22,    0,    0,    0,    0,    0,   22,    0,
    0,   23,  732,  200,    0,    0,    0,   23,    0,   24,
   25,   26,   27,   28,    0,   24,   25,   26,   27,   28,
   22,    0,    0,   76,   76,    0,    0,   76,   76,   23,
   76,   76,   76,   76,  667,    0,    0,   24,   25,   26,
   27,   28,  201,  202,    0,    0,  203,  204,    0,  205,
  206,  207,  208,   22,  311,  311,    0,    0,    0,    0,
    0,    0,   23,    0,    0,    0,    0,    0,    0,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  179,    0,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,    0,
  311,  311,  311,    0,  311,  311,    0,    0,    0,  311,
    0,  311,    0,    0,  311,  311,  311,  311,  311,  311,
  311,  311,  311,  311,    0,  311,  311,    0,  311,  311,
  311,  311,  311,  311,  311,  274,  274,  311,  311,  311,
  311,    0,    0,  311,    0,    0,    0,  311,  311,  311,
  311,  311,    0,  311,  311,  311,  311,  311,  311,  311,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  311,  311,  311,  311,    0,  311,    0,
    0,  274,  274,  274,    0,  274,  274,    0,    0,    0,
  274,    0,  274,    0,    0,  274,  274,  274,  274,  274,
  274,  274,  274,  274,  274,    0,  274,  274,    0,  274,
  274,  274,  274,  274,  274,  274,  278,  278,  274,  274,
  274,  274,    0,    0,  274,    0,    0,    0,  274,  274,
  274,  274,  274,    0,  274,  274,  274,  274,  274,  274,
  274,    0,  274,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  274,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  274,  274,  274,  274,    0,  274,
    0,    0,  278,  278,  278,    0,  278,  278,    0,    0,
    0,  278,    0,  278,    0,    0,  278,  278,  278,  278,
  278,  278,  278,  278,  278,  278,    0,  278,  278,    0,
  278,  278,  278,  278,  278,  278,  278,  275,  275,  278,
  278,  278,  278,    0,    0,  278,    0,    0,    0,  278,
  278,  278,  278,  278,    0,  278,  278,  278,  278,  278,
  278,  278,    0,  278,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  278,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  278,  278,  278,  278,    0,
  278,    0,    0,  275,  275,  275,    0,  275,  275,    0,
    0,    0,  275,    0,  275,    0,    0,  275,  275,  275,
  275,  275,  275,  275,  275,  275,  275,    0,  275,  275,
    0,  275,  275,  275,  275,  275,  275,  275,  283,  283,
  275,  275,  275,  275,    0,    0,  275,    0,    0,    0,
  275,  275,  275,  275,  275,    0,  275,  275,  275,  275,
  275,  275,  275,    0,  275,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  275,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  275,  275,  275,  275,
    0,  275,    0,    0,  283,  283,  283,    0,  283,  283,
    0,    0,    0,  283,    0,  283,    0,    0,  283,  283,
  283,  283,  283,  283,  283,  283,  283,  283,    0,  283,
  283,    0,  283,  283,  283,  283,  283,  283,  283,  298,
  298,  283,  283,  283,  283,    0,    0,  283,    0,    0,
    0,  283,  283,  283,  283,  283,    0,  283,  283,  283,
  283,  283,  283,  283,    0,  283,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  283,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  283,  283,  283,
  283,    0,  283,    0,    0,  298,  298,  298,    0,  298,
  298,    0,    0,    0,  298,    0,  298,    0,    0,  298,
  298,  298,  298,  298,  298,  298,  298,  298,  298,    0,
  298,  298,    0,  298,  298,  298,  298,  298,  298,  298,
  279,  279,  298,  298,  298,  298,    0,    0,  298,    0,
    0,    0,  298,  298,  298,  298,  298,    0,  298,  298,
  298,  298,  298,  298,  298,    0,  298,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  298,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  298,  298,
  298,  298,    0,  298,    0,    0,  279,  279,  279,    0,
  279,  279,    0,    0,    0,  279,    0,  279,    0,    0,
  279,  279,  279,  279,  279,  279,  279,  279,  279,  279,
    0,  279,  279,    0,  279,  279,  279,  279,  279,  279,
  279,  276,  276,  279,  279,  279,  279,    0,    0,  279,
    0,    0,    0,  279,  279,  279,  279,  279,    0,  279,
  279,  279,  279,  279,  279,  279,    0,  279,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  279,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  279,
  279,  279,  279,    0,  279,    0,    0,  276,  276,  276,
    0,  276,  276,    0,    0,    0,  276,    0,  276,    0,
    0,  276,  276,  276,  276,  276,  276,  276,  276,  276,
  276,    0,  276,  276,    0,  276,  276,  276,  276,  276,
  276,  276,  280,  280,  276,  276,  276,  276,    0,    0,
  276,    0,    0,    0,  276,  276,  276,  276,  276,    0,
  276,  276,  276,  276,  276,  276,  276,    0,  276,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  276,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  276,  276,  276,  276,    0,  276,    0,    0,  280,  280,
  280,    0,  280,  280,    0,    0,    0,  280,    0,  280,
    0,    0,  280,  280,  280,  280,  280,  280,  280,  280,
  280,  280,    0,  280,  280,    0,  280,  280,  280,  280,
  280,  280,  280,  281,  281,  280,  280,  280,  280,    0,
    0,  280,    0,    0,    0,  280,  280,  280,  280,  280,
    0,  280,  280,  280,  280,  280,  280,  280,    0,  280,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  280,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  280,  280,  280,  280,    0,  280,    0,    0,  281,
  281,  281,    0,  281,  281,    0,    0,    0,  281,    0,
  281,    0,    0,  281,  281,  281,  281,  281,  281,  281,
  281,  281,  281,    0,  281,  281,    0,  281,  281,  281,
  281,  281,  281,  281,  284,  284,  281,  281,  281,  281,
    0,    0,  281,    0,    0,    0,  281,  281,  281,  281,
  281,    0,  281,  281,  281,  281,  281,  281,  281,    0,
  281,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  281,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  281,  281,  281,  281,    0,  281,    0,    0,
  284,  284,  284,    0,  284,  284,    0,    0,    0,  284,
    0,  284,    0,    0,  284,  284,  284,  284,  284,  284,
  284,  284,  284,  284,    0,  284,  284,    0,  284,  284,
  284,  284,  284,  284,  284,  299,  299,  284,  284,  284,
  284,    0,    0,  284,    0,    0,    0,  284,  284,  284,
  284,  284,    0,  284,  284,  284,  284,  284,  284,  284,
    0,  284,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  284,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  284,  284,  284,  284,    0,  284,    0,
    0,  299,  299,  299,    0,  299,  299,    0,    0,    0,
  299,    0,  299,    0,    0,  299,  299,  299,  299,  299,
  299,  299,  299,  299,  299,    0,  299,  299,    0,  299,
  299,  299,  299,  299,  299,  299,  282,  282,  299,  299,
  299,  299,    0,    0,  299,    0,    0,    0,  299,  299,
  299,  299,  299,    0,  299,  299,  299,  299,  299,  299,
  299,    0,  299,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  299,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  299,  299,  299,  299,    0,  299,
    0,    0,  282,  282,  282,    0,  282,  282,    0,    0,
    0,  282,    0,  282,    0,    0,  282,  282,  282,  282,
  282,  282,  282,  282,  282,  282,    0,  282,  282,    0,
  282,  282,  282,  282,  282,  282,  282,  285,  285,  282,
  282,  282,  282,    0,    0,  282,    0,    0,    0,  282,
  282,  282,  282,  282,    0,  282,  282,  282,  282,  282,
  282,  282,    0,  282,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  282,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  282,  282,  282,  282,    0,
  282,    0,    0,  285,  285,  285,    0,  285,  285,    0,
    0,    0,  285,    0,  285,    0,    0,  285,  285,  285,
  285,  285,  285,  285,  285,  285,  285,    0,  285,  285,
    0,  285,  285,  285,  285,  285,  285,  285,  266,    0,
  285,  285,  285,  285,    0,    0,  285,    0,    0,    0,
  285,  285,  285,  285,  285,    0,  285,  285,  285,  285,
  285,  285,  285,    0,  285,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  285,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  285,  285,  285,  285,
    0,  285,    0,    0,  267,  268,  269,    0,  270,  271,
    0,    0,    0,  272,    0,  273,    0,    0,  274,  275,
  276,  277,  278,  279,  280,  281,  282,  283,    0,  284,
  285,    0,  286,  287,  288,  289,  290,  291,  292,  220,
    0,  293,  294,  295,  296,    0,    0,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,  304,  305,
  306,  307,  308,  309,    0,  310,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,  313,  314,
  315,    0,  316,    0,    0,  220,  220,  220,    0,  220,
  220,    0,    0,    0,  220,    0,  220,    0,    0,  220,
  220,  220,  220,  220,  220,  220,  220,  220,  220,    0,
  220,  220,    0,  220,  220,  220,  220,  220,  220,  220,
  221,    0,  220,  220,  220,  220,    0,    0,  220,    0,
    0,    0,  220,  220,  220,  220,  220,    0,  220,  220,
  220,  220,  220,  220,  220,    0,  220,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  220,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  220,  220,
  220,  220,    0,  220,    0,    0,  221,  221,  221,    0,
  221,  221,    0,    0,    0,  221,    0,  221,    0,    0,
  221,  221,  221,  221,  221,  221,  221,  221,  221,  221,
    0,  221,  221,    0,  221,  221,  221,  221,  221,  221,
  221,  222,    0,  221,  221,  221,  221,    0,    0,  221,
    0,    0,    0,  221,  221,  221,  221,  221,    0,  221,
  221,  221,  221,  221,  221,  221,    0,  221,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  221,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  221,
  221,  221,  221,    0,  221,    0,    0,  222,  222,  222,
    0,  222,  222,    0,    0,    0,  222,    0,  222,    0,
    0,  222,  222,  222,  222,  222,  222,  222,  222,  222,
  222,    0,  222,  222,    0,  222,  222,  222,  222,  222,
  222,  222,  223,    0,  222,  222,  222,  222,    0,    0,
  222,    0,    0,    0,  222,  222,  222,  222,  222,    0,
  222,  222,  222,  222,  222,  222,  222,    0,  222,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  222,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  222,  222,  222,  222,    0,  222,    0,    0,  223,  223,
  223,    0,  223,  223,    0,    0,    0,  223,    0,  223,
    0,    0,  223,  223,  223,  223,  223,  223,  223,  223,
  223,  223,    0,  223,  223,    0,  223,  223,  223,  223,
  223,  223,  223,    0,    0,  223,  223,  223,  223,    0,
    0,  223,    0,    0,    0,  223,  223,  223,  223,  223,
    0,  223,  223,  223,  223,  223,  223,  223,    0,  223,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  223,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  223,  223,  223,  223,  273,  223,    0,  274,  275,
  276,  277,  278,  279,  280,  281,  282,  283,    0,  284,
  285,    0,  286,  287,  288,  289,  290,  291,  292,    0,
    0,  293,  294,  295,  296,    0,    0,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,  304,  305,
  306,  307,  308,  309,    0,  310,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,  313,  314,
  315,    0,  316,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   60,   54,    6,  136,  147,  234,  123,  236,  484,
  123,   33,   33,  262,  146,   60,  317,   40,   44,    6,
    7,  123,   40,  123,    0,  123,    0,   40,  123,  257,
  123,  276,  123,  109,   42,  280,  535,   42,  666,  214,
   92,  459,   49,  723,  101,   44,   91,   40,   40,   56,
   57,   44,   44,   60,   57,  197,   63,   40,   60,   44,
   63,  274,  194,   61,  196,  366,   40,   44,   75,   56,
   57,   44,  214,   60,  754,  324,   63,  134,  123,    0,
  137,   42,   89,  142,  502,  317,   44,   94,   93,   42,
  265,   98,  168,  100,  170,  266,  309,  100,  105,  125,
  272,   41,  730,  110,   44,  112,  270,  271,   93,   62,
  252,   98,  254,  100,  285,  343,  123,  124,  367,  126,
   61,  123,  621,  255,  302,  624,  125,   42,  373,   44,
   91,   61,  125,  125,   76,   44,   78,   61,  195,  557,
  558,  260,  560,   77,   78,  320,  280,  322,  125,  327,
  328,  831,  125,   62,  230,  257,   41,  233,  274,   44,
   61,  274,  169,   41,  171,  294,  295,  125,    0,  111,
  471,  113,  106,  381,  274,   41,  274,  385,   44,  274,
  388,  274,   60,  274,  124,   61,  193,  290,  291,  397,
  365,  268,  269,  494,   41,  272,  273,   44,  275,   40,
   41,  317,  136,   44,  317,   40,  213,  275,  211,  277,
  257,  288,  289,   91,  317,   42,  284,  359,  270,  264,
  290,  291,   41,   42,  294,  295,  358,  234,  273,   40,
  648,    0,  650,  651,  290,  291,  281,  282,  283,  284,
  285,  318,   40,   40,   41,  123,  495,   44,  274,  272,
   41,   42,  274,  274,  272,  277,  484,  373,  486,  272,
  373,  489,  314,  270,  198,   61,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  270,  585,  287,  293,  294,  260,  272,
  297,  466,  268,  269,  268,  269,  272,  273,  272,  275,
  775,  275,  477,  311,  722,  312,  313,  314,    0,  316,
  260,  314,  288,  289,  288,  289,  274,  492,  325,  253,
   41,  496,   41,   44,   42,   44,   44,  314,   41,  374,
   40,   44,   41,  582,   41,   44,   41,   44,  123,   44,
   41,  257,  318,   44,  318,  487,   41,  268,  269,   44,
   41,  272,  273,   44,  275,   41,  267,   42,   44,   44,
   42,  662,   44,   42,    0,   44,  125,  288,  289,  376,
   42,   42,   44,   44,  381,   41,   42,  267,  385,   41,
   42,  388,   41,   42,  391,  302,  264,   41,   42,  376,
  397,  398,  123,  400,  123,  273,  571,  318,  573,   40,
  278,  123,  409,  281,  282,  283,  284,  285,  415,   41,
  462,  164,  165,  317,  663,  293,   40,  374,   40,   40,
  120,   44,   62,   58,   58,   40,  727,   58,   61,   40,
  274,  257,  439,   40,  373,   44,  268,  269,  373,   44,
  272,  273,  260,  275,  274,   60,  274,  257,   61,  456,
  414,  326,  274,  460,  461,  462,  288,  289,  461,  462,
  373,  381,  381,  163,  164,  165,  473,  474,  475,  381,
  257,   44,   44,  460,  461,  462,   91,  257,  178,  486,
  373,  184,  373,  658,   40,  660,  318,  124,  274,  273,
   44,   44,   44,   44,   44,   40,  257,  381,  381,  268,
  269,  260,  381,  272,  273,  733,  275,    0,  123,  381,
  381,  381,  381,  645,  381,   60,  381,  276,  381,  288,
  289,  221,  222,  223,  381,  381,  533,  373,  535,  257,
   42,  538,  539,  540,  541,  542,  543,  544,  545,  546,
  547,  548,  549,  243,  373,  257,   91,  775,   60,  318,
   44,  266,  559,   40,  561,  562,  315,  316,   44,  562,
  319,  320,   44,  322,  323,  324,  325,  267,   44,  269,
   44,  271,  559,  715,  561,  562,  268,  269,  123,   91,
  272,  273,  589,  275,  716,  717,   44,  719,   44,   44,
  290,  291,  292,   44,   44,  295,  288,  289,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,  308,  309,
  669,  123,   44,   44,  621,   44,   44,  624,   44,   44,
  762,  763,   44,  765,   44,   44,  318,   44,   44,  257,
  330,  257,  268,  269,  766,  767,  272,  273,   91,  275,
  381,   44,  257,  258,  259,  652,  261,  262,  263,    0,
  265,   44,  288,  289,   44,  797,  798,  272,  273,  666,
  667,    0,   42,   44,  279,  652,   44,   44,   44,  372,
   44,  286,   44,    0,  257,   44,   44,  380,   44,   44,
  383,  384,  318,  386,  387,    0,  389,  390,   44,  392,
  393,  394,  395,  396,   44,   44,  399,   91,  401,  402,
  403,  404,   44,   44,  381,  373,   44,   44,  373,  373,
   44,  411,  257,  258,  259,   44,  261,  262,  263,   44,
  265,   40,  321,  730,  257,  373,  257,  272,  273,  373,
  737,  734,  257,   44,  279,   93,   40,   41,   40,   40,
   44,  286,   40,  321,  374,  257,  258,  259,  257,  261,
  262,  263,  257,  265,   40,  458,  381,  372,   62,   44,
  272,  273,  381,  381,    0,  272,  272,  279,  272,   40,
  272,  386,  387,  388,  286,  268,  269,   10,  178,  272,
  273,  177,  275,   79,   19,  209,  238,  486,  193,   93,
  490,   60,   89,   54,  794,  288,  289,  319,  213,  319,
  369,  504,  489,  643,  737,  508,   -1,  814,  511,   -1,
   -1,  514,  730,  820,  821,  822,   -1,  520,  521,   -1,
  523,  125,   91,   -1,   -1,  318,   -1,  372,  528,  529,
  530,  531,   -1,   -1,   -1,   -1,   40,  537,   -1,   -1,
   -1,  386,  387,  388,   -1,   -1,   -1,  550,  551,  552,
   -1,  257,  258,  259,  123,  261,  262,  263,   -1,  265,
  372,   -1,   -1,   -1,   -1,   -1,  566,   -1,   -1,   -1,
  276,   -1,   -1,  279,  386,  387,  388,  257,  258,  259,
  286,  261,  262,  263,   -1,  265,   -1,   -1,   -1,  592,
   -1,  594,  595,   -1,  597,  598,   -1,  600,  601,  279,
  603,  604,  605,  606,  607,   -1,  286,  610,   -1,  612,
  613,  614,  615,   -1,   -1,   -1,   -1,  268,  269,  622,
   -1,  272,  273,   -1,  275,   -1,  626,   40,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  288,  289,   -1,
   -1,  268,  269,   -1,  644,  272,  273,   -1,  275,  288,
  289,   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,  288,  289,   -1,   -1,   -1,  669,  318,   -1,  273,
  274,   -1,  675,  288,  289,  678,   -1,   -1,  681,  318,
   -1,  684,   -1,   -1,   -1,   -1,   -1,  690,  691,   -1,
  693,  318,   -1,   -1,   -1,  264,   -1,   -1,  698,  699,
   -1,   -1,   -1,  318,  273,   -1,   -1,  710,  711,  712,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   40,   -1,
   -1,   -1,   -1,   -1,   -1,  329,  330,  331,   -1,  333,
  334,  734,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
   -1,  761,  366,  367,  368,  369,   -1,   -1,  372,  273,
  274,   -1,  376,  377,  378,  379,  380,  381,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,  401,  265,   -1,
   -1,   -1,   -1,   -1,  373,  805,  806,  807,  412,  413,
  414,  415,  279,  417,   -1,   40,   -1,   -1,   -1,  286,
   -1,   -1,   -1,  823,   -1,  329,  330,  331,    0,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,  274,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  290,  291,   -1,  401,  294,  295,
  296,  297,  298,  299,  300,  301,   40,   -1,  412,  413,
  414,  415,  308,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,  274,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,   -1,   -1,  366,  367,  368,  369,  273,  274,
  372,   60,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   40,   -1,
  412,  413,  414,  415,   -1,  417,  268,  269,   -1,   -1,
  272,  273,   -1,  275,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,  123,  340,  288,  289,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  273,
  274,  366,  367,  368,  369,   -1,  318,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   40,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,  274,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,  264,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,  401,   -1,   -1,
   40,   -1,  281,  282,  283,  284,  285,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,  305,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,  274,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  273,  274,  366,  367,  368,  369,   -1,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,  273,  274,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  273,  274,  366,  367,  368,  369,
   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,    0,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,    0,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,  274,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  499,  401,  391,  392,  393,  394,  395,  396,  397,
  398,  399,  400,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   42,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  273,  274,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   42,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   60,   -1,   -1,   -1,   -1,  587,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,  329,  330,  331,   91,  333,  334,   -1,   -1,
   -1,  338,   60,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  123,   -1,  366,
  367,  368,  369,   91,   42,  372,   44,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   60,  390,  672,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  123,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,  412,  413,  414,  415,   42,
  417,   44,   -1,   91,   -1,   -1,   -1,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   42,   -1,
   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
  728,   -1,   -1,   -1,   -1,  123,   60,  318,   -1,   -1,
   -1,  739,  288,  289,   -1,   -1,   -1,   -1,   91,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   60,   -1,  272,  273,   -1,   91,   -1,   -1,
   -1,  279,  318,   -1,  772,   -1,   -1,   -1,  286,   -1,
  123,  257,  258,  259,   -1,  261,  262,  263,  786,  265,
   -1,   -1,   -1,   91,   -1,   -1,  272,  273,   -1,  123,
   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,  809,   -1,   -1,   -1,   -1,  292,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,  123,  264,  303,  304,   -1,
  306,  307,   -1,   -1,  310,  273,  312,  313,  314,  315,
  316,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   42,   -1,   60,   -1,  372,   -1,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,  386,  387,
  388,   -1,   -1,   -1,   -1,  273,  123,   -1,   -1,   -1,
   60,  279,  556,   91,   -1,   60,  372,   -1,  286,   -1,
   -1,   -1,   -1,  341,  342,   -1,   -1,   -1,   -1,   -1,
  386,  387,  388,   -1,  257,  258,  259,   -1,  261,  262,
  263,   91,  265,   -1,   41,  123,   91,  591,   -1,   -1,
  273,   -1,   -1,  257,  258,  259,  279,  261,  262,  263,
   -1,  265,   -1,  286,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,  123,   -1,  279,   -1,   -1,  123,  257,
  258,  259,  286,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,  125,
   -1,  279,  646,  647,  372,  649,   -1,   -1,  286,   -1,
   -1,   -1,   -1,   -1,  292,   -1,   -1,   -1,  386,  387,
  388,   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,
   -1,   -1,  310,   -1,  312,  313,  314,  315,  316,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  372,
   -1,  261,  262,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,  386,  387,  388,   -1,   -1,  372,  286,
   -1,   -1,   -1,   -1,  718,   -1,  720,  721,   -1,   -1,
   -1,   -1,  386,  387,  388,   -1,  264,   -1,   -1,   -1,
  125,   -1,   -1,   -1,  372,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  386,  387,
  388,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,  273,  768,   -1,   -1,   -1,  273,   -1,
  292,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,  303,  304,   -1,  306,  307,   -1,  273,  310,   -1,
  312,  313,  314,  315,  316,  372,   -1,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  386,
  387,  388,   -1,   -1,   -1,   -1,   -1,   -1,  356,  276,
   -1,  125,  279,  393,  394,  395,  396,   -1,   -1,  286,
   -1,   -1,  402,  403,  404,  405,  406,  407,  408,  409,
  410,  411,   -1,  329,  330,  331,  356,  333,  334,   -1,
   -1,  356,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  273,   -1,
  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  125,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  273,
   -1,  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,   -1,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  125,   -1,   -1,  401,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,   -1,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  273,   -1,  366,  367,  368,  369,   -1,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,  273,   -1,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  273,   -1,  366,  367,  368,  369,
   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,   -1,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  125,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  273,   -1,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   91,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  273,   60,  366,
  367,  368,  369,   -1,  123,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,  123,  338,   60,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,   -1,   -1,
  366,  367,  368,  369,   91,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  123,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   60,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   91,   -1,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   -1,   -1,  292,   91,   -1,   -1,   -1,  297,  298,
  299,  300,  301,  302,  303,  304,   -1,  306,  307,  308,
  123,  310,   -1,  312,  313,  314,  315,  316,   60,   -1,
   -1,   -1,  264,   -1,   -1,   -1,  123,   -1,  327,  328,
   -1,  273,   -1,   -1,   -1,   41,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   91,
  292,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
  302,  303,  304,   -1,  306,  307,   -1,   -1,  310,   -1,
  312,  313,  314,  315,  316,   -1,   -1,   -1,   60,   -1,
   -1,  123,   -1,   -1,   -1,   91,   -1,  264,   -1,   41,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,  125,   41,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,   91,
   -1,   -1,   -1,   -1,   -1,  292,   -1,  123,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  303,  304,   -1,  306,
  307,   -1,  292,  310,   -1,  312,  313,  314,  315,  316,
   -1,  123,   60,  303,  304,   -1,  306,  307,   -1,   91,
  310,  264,  312,  313,  314,  315,  316,   -1,   60,   -1,
  273,   -1,   -1,   -1,   -1,   60,   -1,  264,  281,  282,
  283,  284,  285,   91,   -1,   -1,  273,   -1,   -1,  292,
   -1,  123,   -1,   -1,  281,  282,  283,  284,  285,   91,
  303,  304,   -1,  306,  307,  292,   91,  310,   -1,  312,
  313,  314,  315,  316,   -1,  123,  303,  304,   -1,  306,
  307,   -1,   -1,  310,   -1,  312,  313,  314,  315,  316,
   60,  123,  264,   -1,   -1,   -1,   -1,   -1,  123,   -1,
   -1,  273,   -1,  260,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,  276,
  292,   91,   -1,   60,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  303,  304,   -1,  306,  307,   -1,  273,  310,   -1,
  312,  313,  314,  315,  316,  281,  282,  283,  284,  285,
   -1,   60,  264,  123,   91,   -1,   60,  293,  315,  316,
   -1,  273,  319,  320,   -1,  322,  323,  324,  325,  281,
  282,  283,  284,  285,   -1,  257,  258,  259,   -1,  261,
  262,  263,   91,  265,   60,   -1,  123,   91,   -1,   -1,
   -1,   -1,  264,  305,  276,   -1,   -1,  279,   -1,   60,
   -1,  273,   -1,   -1,  286,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  123,   91,  264,   -1,   -1,  123,
   -1,  125,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   91,   -1,  264,  281,  282,  283,  284,  285,   60,  264,
   -1,  273,   -1,   -1,   60,   -1,  278,  123,  273,  281,
  282,  283,  284,  285,  302,   -1,  281,  282,  283,  284,
  285,  293,  123,   -1,  125,   -1,   60,   -1,  293,   91,
   -1,   -1,   -1,   -1,   -1,   91,   -1,   93,   -1,   -1,
   -1,   -1,   -1,  125,   -1,   -1,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,   -1,   91,   -1,   60,
   -1,  123,   -1,  273,  274,   -1,  276,  123,   -1,  279,
   -1,  281,  282,  283,  284,  285,  286,   -1,   -1,   -1,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  123,
   91,   -1,   60,   -1,   -1,   -1,  273,  274,   -1,  276,
   -1,   -1,  279,   -1,  281,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,  264,  265,   -1,   -1,   -1,
  264,  265,  123,   91,  273,  274,   -1,   -1,   -1,  273,
  274,   -1,  281,  282,  283,  284,  285,  281,  282,  283,
  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,  264,  265,
   -1,   -1,   -1,   -1,   -1,  123,   -1,  273,  274,   -1,
   -1,   -1,   -1,  264,   -1,  281,  282,  283,  284,  285,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,  260,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  276,   -1,   -1,  260,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  273,  274,  276,   -1,   -1,   -1,  273,   -1,  281,
  282,  283,  284,  285,   -1,  281,  282,  283,  284,  285,
  264,   -1,   -1,  315,  316,   -1,   -1,  319,  320,  273,
  322,  323,  324,  325,  278,   -1,   -1,  281,  282,  283,
  284,  285,  315,  316,   -1,   -1,  319,  320,   -1,  322,
  323,  324,  325,  264,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,  274,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  273,  274,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  273,  274,  366,
  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  273,  274,
  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  273,
  274,  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,  274,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,  274,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  273,  274,  366,  367,  368,  369,   -1,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,  273,  274,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  273,  274,  366,  367,  368,  369,
   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,  274,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  273,  274,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  273,  274,  366,
  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  273,   -1,
  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,
   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  273,
   -1,  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,   -1,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,   -1,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  349,  350,  351,
  352,   -1,  354,  355,   -1,  357,  358,  359,  360,  361,
  362,  363,  273,   -1,  366,  367,  368,  369,   -1,   -1,
  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,
  382,  383,  384,  385,  386,  387,  388,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,   -1,   -1,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,  340,  417,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,   -1,
   -1,  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,  414,
  415,   -1,  417,
  };

#line 1209 "Iril/IR/IR.jay"

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
  public const int ATTRIBUTE_GROUP_REF = 317;
  public const int ATTRIBUTES = 318;
  public const int NORECURSE = 319;
  public const int NOUNWIND = 320;
  public const int UNWIND = 321;
  public const int SPECULATABLE = 322;
  public const int SSP = 323;
  public const int UWTABLE = 324;
  public const int ARGMEMONLY = 325;
  public const int SEQ_CST = 326;
  public const int DSO_LOCAL = 327;
  public const int DSO_PREEMPTABLE = 328;
  public const int RET = 329;
  public const int BR = 330;
  public const int SWITCH = 331;
  public const int INDIRECTBR = 332;
  public const int INVOKE = 333;
  public const int RESUME = 334;
  public const int CATCHSWITCH = 335;
  public const int CATCHRET = 336;
  public const int CLEANUPRET = 337;
  public const int UNREACHABLE = 338;
  public const int FNEG = 339;
  public const int ADD = 340;
  public const int NUW = 341;
  public const int NSW = 342;
  public const int FADD = 343;
  public const int SUB = 344;
  public const int FSUB = 345;
  public const int MUL = 346;
  public const int FMUL = 347;
  public const int UDIV = 348;
  public const int SDIV = 349;
  public const int FDIV = 350;
  public const int UREM = 351;
  public const int SREM = 352;
  public const int FREM = 353;
  public const int SHL = 354;
  public const int LSHR = 355;
  public const int EXACT = 356;
  public const int ASHR = 357;
  public const int AND = 358;
  public const int OR = 359;
  public const int XOR = 360;
  public const int EXTRACTELEMENT = 361;
  public const int INSERTELEMENT = 362;
  public const int SHUFFLEVECTOR = 363;
  public const int EXTRACTVALUE = 364;
  public const int INSERTVALUE = 365;
  public const int ALLOCA = 366;
  public const int LOAD = 367;
  public const int STORE = 368;
  public const int FENCE = 369;
  public const int CMPXCHG = 370;
  public const int ATOMICRMW = 371;
  public const int GETELEMENTPTR = 372;
  public const int ALIGN = 373;
  public const int INBOUNDS = 374;
  public const int INRANGE = 375;
  public const int TRUNC = 376;
  public const int ZEXT = 377;
  public const int SEXT = 378;
  public const int FPTRUNC = 379;
  public const int FPEXT = 380;
  public const int TO = 381;
  public const int FPTOUI = 382;
  public const int FPTOSI = 383;
  public const int UITOFP = 384;
  public const int SITOFP = 385;
  public const int PTRTOINT = 386;
  public const int INTTOPTR = 387;
  public const int BITCAST = 388;
  public const int ADDRSPACECAST = 389;
  public const int ICMP = 390;
  public const int EQ = 391;
  public const int NE = 392;
  public const int UGT = 393;
  public const int UGE = 394;
  public const int ULT = 395;
  public const int ULE = 396;
  public const int SGT = 397;
  public const int SGE = 398;
  public const int SLT = 399;
  public const int SLE = 400;
  public const int FCMP = 401;
  public const int OEQ = 402;
  public const int OGT = 403;
  public const int OGE = 404;
  public const int OLT = 405;
  public const int OLE = 406;
  public const int ONE = 407;
  public const int ORD = 408;
  public const int UEQ = 409;
  public const int UNE = 410;
  public const int UNO = 411;
  public const int PHI = 412;
  public const int SELECT = 413;
  public const int CALL = 414;
  public const int TAIL = 415;
  public const int VA_ARG = 416;
  public const int LANDINGPAD = 417;
  public const int CATCHPAD = 418;
  public const int CLEANUPPAD = 419;
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
