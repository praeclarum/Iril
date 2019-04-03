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
        yyVal = new ResumeInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 926 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 930 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 264:
#line 934 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 265:
#line 941 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 268:
#line 953 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 269:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 961 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 271:
#line 965 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 272:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 973 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 276:
#line 985 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 277:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 280:
#line 1001 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 281:
#line 1005 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 282:
#line 1009 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
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
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1025 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1029 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1033 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1037 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1041 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new LandingPadInstruction ((LType)yyVals[-1+yyTop]);
    }
  break;
case 302:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 303:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 304:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 305:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 306:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1129 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1133 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1137 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1141 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1145 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 317:
#line 1149 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 318:
#line 1153 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1157 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1161 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 321:
#line 1165 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 322:
#line 1169 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1173 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1177 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1181 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 326:
#line 1185 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 327:
#line 1189 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 328:
#line 1193 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 329:
#line 1197 "Iril/IR/IR.jay"
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
   57,   57,   57,   57,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,
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
    2,    2,    7,    1,    5,    6,    5,    7,    5,    5,
    6,    4,    4,    5,    6,    6,    5,    6,    6,    6,
    7,    5,    6,    7,    4,    5,    6,    5,    2,    5,
    4,    4,    4,    4,    5,    6,    7,    6,    6,    4,
    3,    7,    8,    5,    6,    5,    5,    6,    3,    4,
    5,    6,    7,    4,    5,    6,    6,    4,    5,    7,
    8,    5,    6,    4,    5,    4,    5,    5,    4,
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
    0,  264,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  212,    0,    0,
  218,    0,    0,    0,    0,    0,    0,    0,  191,    0,
  189,  190,    0,    0,    0,    0,    0,    0,    0,   52,
    0,   50,    0,   41,   53,    0,   47,   49,   54,   42,
   43,   40,   17,   16,   61,   60,   59,    0,    0,   74,
  245,  244,    0,  242,    0,    0,    0,    0,    0,  262,
    0,    0,  258,    0,    0,  261,  254,  255,    0,    0,
  252,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  257,  289,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  154,  155,  156,
  157,  158,  159,  160,  161,  162,  163,    0,  164,  165,
  176,  177,  178,  179,  167,  169,  170,  171,  172,  168,
  166,  174,  175,  173,    0,    0,    0,    0,    0,    0,
    0,    0,  102,  213,    0,  219,    0,    0,   56,    0,
    0,    0,    0,    0,  207,    0,    0,    0,   28,    0,
    0,    0,    0,  208,    0,    0,   78,    0,    0,    0,
    0,  105,    0,    0,    0,  202,    0,    0,  224,    0,
    0,    0,  253,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  246,    0,    0,    0,    0,    0,    0,    0,    0,
  301,    0,  104,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,   45,    0,   51,   48,  243,  120,    0,
    0,  108,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  285,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  188,  185,  187,    0,    0,    0,    0,   44,    0,    0,
  103,    0,    0,    0,  226,    0,    0,  227,    0,  265,
    0,  286,  322,    0,  295,  307,    0,  290,  325,    0,
  311,  288,  327,  319,  315,    0,    0,  304,    0,  270,
  269,  306,  328,    0,    0,  267,    0,    0,  192,  205,
    0,    0,    0,    0,    0,    0,    0,    0,  247,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  106,    0,    0,    0,    0,  249,    0,    0,    0,
  229,  225,    0,    0,  266,  323,  308,  312,  316,  305,
  271,  299,  317,    0,    0,    0,    0,    0,    0,  209,
    0,  210,  298,  287,    0,    0,    0,    0,  276,    0,
    0,  283,    0,    0,  107,  259,    0,  263,  250,    0,
  237,  231,    0,    0,    0,    0,  236,  232,  230,  228,
    0,  268,    0,  302,    0,  320,    0,    0,  248,  313,
    0,  284,  186,  251,  234,    0,    0,    0,    0,    0,
  260,  303,  321,  211,  235,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  241,  238,
  240,    0,    0,  239,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  209,  183,  175,   75,
  184,  245,  218,   76,   77,   54,  176,  344,  167,  363,
  346,  347,  348,  349,  185,  750,  210,   86,   87,  131,
  132,   15,  106,  147,  317,  491,   62,   57,   58,   59,
   63,  143,  144,  148,  438,  455,  246,  499,  751,  225,
  700,  373,  620,  752,  318,  319,  320,  321,  322,  500,
  588,  667,  668,  778,  364,  551,  552,  726,  727,  380,
  381,  413,
  };
  protected static readonly short [] yySindex = {          -76,
  -48,  -60,  -25,  -16,   83, 3493, 3610, -210,    0,  -76,
    0,    0,    0,    0, -124, -104,  110,  120, 1723,  -79,
   51,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  154,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4010,  -89,
  -27,    0,  198, -149,  208, 4142, 3657,    0,    0, 3657,
  -14,    0, 3657,  235,    0,  219,    0,   43,   57,    0,
    0,    0,    0,    0, 4142,  -70,   89,  -73,  -41,  273,
  -24,  215,   74,    0,  198,    8,  208,   90, 4142,   94,
    0,   65,    0, 3663,  208,    0,  208, 4142,  -12, 3657,
  219,   -7,    0,  315,  722, -159,    0,    0, 2230, 4142,
  -70, 4142,  -70,    0,  316,    0, -238,  402,  320, 3971,
  405,    0, 4142, 4142,   36, 4142,    0,    0,    0,  198,
   75,    0,  208,  219,   20, -159,  219,  240,    0,    0,
    0, 3393,  129,    0,    0,  131,  -74,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   77,  412,
  417,  425, 4147, 4147, 4147,  426,    0, 2230, 4142,   97,
 4142,  413,  415,  420,  164,    0, -238, 3976,    0,    0,
    0,    0,   12, 2038,    0,    0,    0,  198,   72,  419,
   16,    0,  104,  131,  219,  131,  131,   89,  427,  447,
    0,    0,    0,    0,    0,    0,    0,    0,  392,    0,
 1963,    0, 3859,   26,  217,  232, 5198, -107,    0,  452,
 4147, 4147, 4147,    0,   52,  -33,   53,  123,  449, 2230,
  124,  458, 2192, 3902,  229,  927,    0, -238,  175,   30,
    0,    0, 4004,    0,    0,    0,    0,    0,    0,    0,
    0,  131,   89,  131,  131,  245, 3398,    0,    0,    0,
  249, 5198, -105,    0,  234,  448, 4147, -173, 4147, 4142,
 4147,    0, 3409, 4142, 3409, 4142, 3409, 4142, 4142, 2238,
 4142, 4142, 4142, 3409, 2253, 2377, 4142, 4142, 4142, 4147,
 4147, 4147, 4142, 1103, 3623,  184,  -28, 4147, 4147, 4147,
 4147, 4147, 4147, 4147, 4147, 4147, 4147, 4147, 4147, 1832,
 3929, 4142, 4142, 3563,   98, 4142, 2334,    0, 5198,  234,
    0,  234,  237, 5198, 4142,  137,  142,  143,    0, 4147,
    0,    0,  262,  153,  484,  288,  156,  170,  502,    0,
  507,    0,  831,    0,    0,  428,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  131,  131,    0,
    0,    0,  253,    0, -208, 2432, 5198, -100, 5638,    0,
  276, 2367,    0,  506, 1985,    0,    0,    0, 2038, 3409,
    0, 2038, 2038, 3409, 2038, 2038, 3409, 2038, 2038, 4142,
 2038, 2038, 2038, 2038, 2038, 3409, 4142, 2038, 4142, 2038,
 2038, 2038, 2038,  513,  517,  527,   86, 4142,  267, 4147,
  529,    0,    0, 4142,  307,  195,  196,  197,  199,  201,
  204,  206,  207,  212,  214,  228,  236,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4142,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4142,   63, 2038, 1985, 4142, 3657,
 3563,  -22,    0,    0,  234,    0,  305,  305,    0, 2523,
  329, 4142, 4142, 4142,    0,  234,  339,  246,    0,  353,
  361,  250, 2218,    0, 3935,  131,    0, 3398, 4147,  -99,
  234,    0, 2615, 5198,  234,    0,  583,  364,    0,  591,
  595, 2038,    0,  597,  600, 2038,  601,  602, 2038,  604,
  606, 2038,  607,  611,  615,  616,  617, 2038, 2038,  618,
 2038,  619,  621,  623,  625, 4147, 4147, 4147,  -50,  370,
 4142,  626, 4142,  373, 4147, 4142, 4142, 4142, 4142, 4142,
 4142, 4142, 4142, 4142, 4142, 4142, 4142, 2038, 2038, 2367,
  627,    0,  628,  591, 1985, 1985, 4142, 1985, 4142, 3657,
    0,  305,    0, 4147,  379,  381,  383,  305,  234,  416,
  234,    0,  418,    0,  261,    0,    0,    0,    0, 5198,
  -98,    0, 2706,  305,  364,  585, 3808,  296, 2367,  635,
 2367, 2367,  636, 2367, 2367,  638, 2367, 2367,  642, 2367,
 2367, 2367, 2367, 2367,  645,  648, 2367,  654, 2367, 2367,
 2367, 2367,    0,  655,  657,  453,  669, 4142, 2038,  677,
 4142,  680, 4147,  682,  198,  198,  198,  198,  198,  198,
  198,  198,  198,  198,  198,  198,  685,  688,  689,  646,
 4147,  131,  591,  591, 1985,  591, 1985, 1985, 4142,  692,
    0,    0,    0,  305,  234,  305,  234,    0, 2797, 5198,
    0,  698, 4142, 4045,    0, 2126,  304,    0,  364,    0,
 2367,    0,    0, 2367,    0,    0, 2367,    0,    0, 2367,
    0,    0,    0,    0,    0, 2367, 2367,    0, 2367,    0,
    0,    0,    0, 4147, 4147,    0,  375,  699,    0,    0,
  380,  702,  382,  710, 4147, 2367, 2367, 2367,    0,  712,
  131,  131,  131,  591,  131,  591,  591, 1985, 4147,  305,
  305,    0, 2888,  364,  713, 4078,    0,  718,  363, 2283,
    0,    0, 4114,  441,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  508,  391,  509,  394,  512, 4147,    0,
  726,    0,    0,    0,  681, 4147,  131,  131,    0,  131,
  131,    0,  591,  306,    0,    0,  364,    0,    0, 3473,
    0,    0,  397,  735,  738,  740,    0,    0,    0,    0,
  364,    0,  524,    0,  528,    0,  726, 4147,    0,    0,
  131,    0,    0,    0,    0,  322,  744, 4147, 4147, 4147,
    0,    0,    0,    0,    0, 4142,  406,  408,  410,  374,
 4142, 4142, 4142, 4147,  387,  389,  393,  742,    0,    0,
    0, 4147,  331,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  794,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 2076, 3693,  525,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    4,    0,    0,    0,    0,    0,
    0, 3738,    0,    0,  530,    0,  531,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  759,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  286,
    0,    0,  537,    0,    0,    0,    0,    0,    0,    0,
    0,  300,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  759,    0,   46,
    0,    0,    0,    0,    0,    0,    0,    0,  690,    0,
    0,    0,    0,  759,    0,    0,    0,   41,  759,    0,
  759,    0,    0,    0,    0,    0,   64,    0, 3787, 3988,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  759,
    0,    0,  759,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  138,    0,  172,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2979,
    0, 5289,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  226,    0,    0,    0,    0,    0,
   59,    0,  759,    0,    0,  357,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  365,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  759,    0,
    0,  759,  759,    0,  759,  759,    0,  759,  759,    0,
  759,  759,  759,  759,  759,    0,    0,  759,    0,  759,
  759,  759,  759,    0,    0,    0,  759,    0,  759,    0,
    0,    0,    0,    0,  759,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  759,  759,    0,    0,    0,
    0,  759,    0,    0, 3070,    0, 3161, 5380,    0,    0,
  759,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  450,    0,    0,    0,    0,
    0,    0,    0,    0, 5471,    0,    0,    0,    0,    0,
    0,  759,    0,    0,    0,  759,    0,    0,  759,    0,
    0,  759,    0,    0,    0,    0,    0,  759,  759,    0,
  759,    0,    0,    0,    0,    0,    0,    0,    0,  759,
    0,    0,    0,  759,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  759,  759,    0,
 4106,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3252,    0,    0,  759,  759,  759,  472,    0,    0,
  504,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 5562,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  759,    0,
    0,    0,    0,    0,  790,  887,  978, 1069, 1160, 1251,
 1342, 1433, 1524, 1615, 1706, 1797,    0,    0,    0,    0,
    0, 4197,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  569,  592,  656,  666,    0,    0,    0,
    0,    0,    0,    0,    0,  759,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4288,    0, 4379,    0, 4470,    0,    0,    0,    0,  678,
 1933,    0,    0,    0,    0,    0,    0,  359,  759,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4561,    0,    0,    0,    0,    0, 4652, 4743,    0, 4834,
 4925,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5016,    0,    0,    0,
 5107,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  759,
    0,    0,    0,    0,  759,  759,  759,    0,    0,    0,
    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  791,  731,    0,    0,    0,    0,  634,  639,  126,
   -6,   14, -175,  -47,    0,  798,  580,    0, -222, -475,
    0,  334,    0, -584,    0,  299,  620,  743,   15,    0,
  631,    0,  -10, -129, -256,    0,   -2,    0,  777,  -52,
  -55,    0,  622, -144,    0,    0,    0,  302, -672,  272,
    0, -483, -496,   45, -290,    0,  520,  521,  467, -396,
  152,    0,  113,    0,  355,    0,  209,    0,  121,  -86,
 -244,    0,
  };
  protected static readonly short [] yyTable = {            53,
   53,   93,  219,   56,  100,  366,  197,  575,  119,   50,
  330,  345,   16,  351,  586,  324,  214,  367,  115,   91,
   55,   61,  494,  580,  660,   94,  464,   94,  331,  111,
  113,   50,   94,   89,  362,   19,  622,  172,  263,  127,
   51,  173,   85,   84,   20,   29,  764,   82,  217,   53,
   53,  123,  219,   53,   98,  243,   53,   91,  146,   94,
  103,  554,   51,  121,  252,  215,  254,  470,  109,  219,
   95,   97,   49,  243,   99,  464,  787,  102,  725,  123,
   84,   49,   85,   82,   83,   84,  211,  130,  196,  368,
  136,   53,  371,   53,   49,  330,  330,  103,  142,   51,
  489,  662,   51,  168,   91,  170,   64,  219,  250,  219,
  493,  372,  133,   91,  135,  192,  188,  189,  193,  191,
  484,  698,  166,  194,  702,  359,  198,   91,   82,  529,
   70,   71,  122,  248,  174,  503,  242,  122,   91,  503,
  231,  725,  503,   21,  467,  332,  468,   66,  262,  823,
  255,  503,   35,  550,  354,   67,  164,  145,  643,  644,
  190,  646,  230,   50,  233,   83,  323,   88,  323,  212,
   68,  123,  213,  323,  323,  323,  329,   47,   48,  464,
   69,  229,   51,  232,  253,  734,  130,  165,  384,  490,
  387,    1,    2,   83,   51,    3,    4,  396,    5,  215,
   79,  110,  464,  112,  237,  358,  142,  238,  103,   17,
   18,    6,    7,  179,  219,  353,   70,   71,  238,  163,
   72,   73,   23,   72,   73,   21,   49,  343,  486,   90,
   24,   25,   26,   27,   28,   22,  169,  583,  171,   91,
  766,    8,  145,  335,   23,  114,  339,   94,  714,  118,
  716,  717,   24,   25,   26,   27,   28,  101,  105,  134,
  362,  460,  345,   53,  137,  362,  379,  382,  383,  385,
  386,  388,  389,  391,  392,  393,  394,  395,  398,  400,
  401,  402,  403,  794,  375,  241,  407,  409,  561,  562,
  415,  195,  464,  487,  796,  104,  488,  801,  216,  215,
  568,  658,  107,  241,  488,  456,  457,   53,   91,  462,
  531,  459,  117,   29,   29,  581,  108,   29,  471,  584,
   29,  763,  616,  659,   80,   84,  100,   81,  458,  100,
  121,  121,  121,   29,   29,  121,  121,  120,  121,   84,
  130,  219,  145,  130,  732,  414,  793,  733,   91,  788,
  535,  121,  121,  149,  150,  151,  124,  152,  153,  154,
  126,  155,  805,   29,  124,  488,   35,   22,  464,  156,
   91,  824,  564,  502,  788,  157,   23,  506,   70,   71,
  509,  121,  158,  512,   24,   25,   26,   27,   28,  518,
  519,  131,  521,  654,  131,  656,  128,   46,  261,  233,
   46,  530,  233,  723,   91,  122,  122,  534,  560,  122,
  122,   91,  122,  618,   91,   91,  623,  814,  186,  651,
   91,  652,   91,  653,   91,  122,  122,  819,   91,  820,
   91,  548,  464,  821,   91,  226,  227,  138,   89,  123,
  123,  177,  178,  123,  123,  187,  123,  145,  549,  125,
  220,  221,   53,   53,   53,  122,  222,  557,  559,  123,
  123,  224,  224,  224,  223,  565,  566,  567,  159,  228,
  234,   18,  235,  555,  556,  558,  186,  236,  343,  720,
  249,  721,  160,  161,  162,  247,  257,  256,  265,  123,
  264,  325,  334,   21,   21,  333,  336,   21,   21,  199,
   21,  337,  350,   26,  360,  365,  777,  215,  369,  412,
  469,  461,  711,   21,   21,  200,  258,  472,  476,  326,
  327,  328,  473,  474,  619,  477,  619,  478,  480,  625,
  626,  627,  628,  629,  630,  631,  632,  633,  634,  635,
  636,  357,  481,   21,  479,  482,  483,  362,  496,  498,
   53,  485,   53,   53,  201,  202,  526,  649,  203,  204,
  527,  205,  206,  207,  208,  370,  219,  374,   19,  376,
  528,  645,  533,  647,  648,  536,  537,  538,  323,  539,
  666,  540,  757,  758,  541,  760,  542,  543,  404,  405,
  406,   20,  544,  411,  545,  569,  416,  417,  418,  419,
  420,  421,  422,  423,  424,  425,  426,  427,  546,  571,
  730,  619,  219,  219,  619,  219,  547,  572,  570,  149,
  150,  151,  573,  152,  153,  154,  585,  155,  475,  371,
  587,  791,  124,  124,  771,  772,  124,  124,  589,  124,
  591,  157,   53,  592,  594,  595,  219,  597,  158,  598,
  600,  199,  124,  124,  601,   27,  343,  729,  602,  603,
  604,  607,  609,  718,  610,   24,  611,  200,  612,  621,
  640,  641,  655,  497,  657,  663,  669,   22,  671,  674,
  501,  677,  124,  504,  505,  680,  507,  508,  686,  510,
  511,  687,  513,  514,  515,  516,  517,  689,  694,  520,
  695,  522,  523,  524,  525,  642,  201,  202,  532,  696,
  203,  204,  697,  205,  206,  207,  208,  125,  125,  343,
  701,  125,  125,  703,  125,  705,  666,  103,  706,   85,
  204,  707,  708,  204,  773,  719,  550,  125,  125,   18,
   18,  724,  745,   18,   18,  747,   18,  744,  774,  775,
  776,  204,  746,  749,  748,  756,  767,  770,  553,   18,
   18,  781,  141,  783,  782,  784,  785,  125,  786,  788,
  797,   26,   26,  789,  798,   26,   26,  799,   26,  800,
  802,   50,  204,  806,  803,  822,  811,  579,  812,   18,
  813,   26,   26,    1,  712,  713,  109,  715,   84,  810,
   65,  110,  111,  590,  815,  816,  817,  593,  112,  116,
  596,  240,   51,  599,  204,  239,   78,  352,  577,  605,
  606,   26,  608,  251,  613,  614,  615,  617,  259,   84,
   92,  125,  804,  624,  260,  495,   19,   19,  465,  466,
   19,   19,  578,   19,   49,  780,  769,    0,  709,  637,
  638,  639,    0,    0,    0,    0,   19,   19,    0,   20,
   20,    0,  650,   20,   20,  759,   20,  761,  762,    0,
    0,    0,   91,    0,    0,    0,    0,    0,    0,   20,
   20,    0,    0,    0,    0,    0,   19,    0,    0,    0,
  670,    0,  672,  673,    0,  675,  676,    0,  678,  679,
    0,  681,  682,  683,  684,  685,    0,    0,  688,   20,
  690,  691,  692,  693,  792,    0,    0,    0,    0,    0,
  699,  704,    0,   27,   27,    0,   84,   27,   27,    0,
   27,    0,    0,   24,   24,    0,    0,   24,   24,  710,
   24,    0,    0,   27,   27,   22,   22,    0,    0,   22,
   22,    0,   22,   24,   24,    0,    0,    0,    0,    0,
    0,    0,  204,  204,    0,   22,   22,  731,    0,    0,
    0,    0,  735,   27,    0,  736,    0,    0,  737,    0,
    0,  738,    0,   24,    0,   22,    0,  739,  740,    0,
  741,    0,  742,  743,   23,   22,    0,    0,    0,  139,
    0,    0,   24,   25,   26,   27,   28,  753,  754,  755,
    0,    0,    0,    0,  140,    0,    0,   84,  204,  204,
  204,    0,  204,  204,    0,    0,    0,  204,    0,  204,
    0,  779,  204,  204,  204,  204,  204,  204,  204,  204,
  204,  204,    0,  204,  204,    0,  204,  204,  204,  204,
  204,  204,  204,    0,  790,  204,  204,  204,  204,    0,
    0,  204,  324,  324,    0,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,  204,  204,    0,  204,
    0,    0,    0,    0,    0,    0,    0,  149,  150,  151,
  204,  152,  153,  154,    0,  155,  807,  808,  809,    0,
    0,  204,  204,  204,  204,    0,  204,    0,   84,  157,
    0,    0,  818,    0,    0,    0,  158,    0,  324,  324,
  324,    0,  324,  324,    0,    0,    0,  324,    0,  324,
    0,    0,  324,  324,  324,  324,  324,  324,  324,  324,
  324,  324,    0,  324,  324,    0,  324,  324,  324,  324,
  324,  324,  324,    0,    0,  324,  324,  324,  324,  329,
  329,  324,   50,    0,    0,  324,  324,  324,  324,  324,
    0,  324,  324,  324,  324,  324,  324,  324,    0,  324,
    0,    0,    0,  149,  150,  151,    0,  152,  153,  154,
  324,  155,    0,   51,    0,    0,    0,    0,    0,   84,
    0,  324,  324,  324,  324,  157,  324,    0,    0,    0,
    0,    0,  158,    0,    0,  329,  329,  329,    0,  329,
  329,    0,    0,    0,  329,   49,  329,    0,    0,  329,
  329,  329,  329,  329,  329,  329,  329,  329,  329,    0,
  329,  329,    0,  329,  329,  329,  329,  329,  329,  329,
  314,  314,  329,  329,  329,  329,    0,    0,  329,    0,
    0,    0,  329,  329,  329,  329,  329,    0,  329,  329,
  329,  329,  329,  329,  329,    0,  329,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  329,    0,    0,
   84,    0,    0,    0,    0,    0,    0,    0,  329,  329,
  329,  329,    0,  329,    0,    0,  314,  314,  314,    0,
  314,  314,    0,    0,    0,  314,    0,  314,    0,    0,
  314,  314,  314,  314,  314,  314,  314,  314,  314,  314,
    0,  314,  314,    0,  314,  314,  314,  314,  314,  314,
  314,  294,  294,  314,  314,  314,  314,    0,    0,  314,
    0,    0,    0,  314,  314,  314,  314,  314,    0,  314,
  314,  314,  314,  314,  314,  314,   22,  314,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,  314,    0,
    0,   84,    0,   24,   25,   26,   27,   28,    0,  314,
  314,  314,  314,    0,  314,    0,    0,  294,  294,  294,
    0,  294,  294,    0,    0,    0,  294,  408,  294,    0,
    0,  294,  294,  294,  294,  294,  294,  294,  294,  294,
  294,    0,  294,  294,    0,  294,  294,  294,  294,  294,
  294,  294,  291,  291,  294,  294,  294,  294,    0,    0,
  294,    0,    0,    0,  294,  294,  294,  294,  294,    0,
  294,  294,  294,  294,  294,  294,  294,    0,  294,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  294,
    0,    0,   84,    0,    0,    0,    0,    0,    0,    0,
  294,  294,  294,  294,    0,  294,    0,    0,  291,  291,
  291,    0,  291,  291,    0,    0,    0,  291,    0,  291,
    0,    0,  291,  291,  291,  291,  291,  291,  291,  291,
  291,  291,    0,  291,  291,    0,  291,  291,  291,  291,
  291,  291,  291,  292,  292,  291,  291,  291,  291,    0,
    0,  291,    0,    0,    0,  291,  291,  291,  291,  291,
    0,  291,  291,  291,  291,  291,  291,  291,    0,  291,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  291,    0,    0,   84,    0,    0,    0,    0,    0,    0,
    0,  291,  291,  291,  291,    0,  291,    0,    0,  292,
  292,  292,    0,  292,  292,    0,    0,    0,  292,    0,
  292,    0,    0,  292,  292,  292,  292,  292,  292,  292,
  292,  292,  292,    0,  292,  292,    0,  292,  292,  292,
  292,  292,  292,  292,  293,  293,  292,  292,  292,  292,
    0,    0,  292,    0,    0,    0,  292,  292,  292,  292,
  292,    0,  292,  292,  292,  292,  292,  292,  292,    0,
  292,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  292,    0,    0,   84,    0,    0,    0,    0,    0,
    0,    0,  292,  292,  292,  292,    0,  292,    0,    0,
  293,  293,  293,    0,  293,  293,    0,    0,    0,  293,
    0,  293,    0,    0,  293,  293,  293,  293,  293,  293,
  293,  293,  293,  293,    0,  293,  293,    0,  293,  293,
  293,  293,  293,  293,  293,  326,  326,  293,  293,  293,
  293,    0,    0,  293,    0,    0,    0,  293,  293,  293,
  293,  293,    0,  293,  293,  293,  293,  293,  293,  293,
    0,  293,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  293,    0,    0,   84,    0,    0,    0,    0,
    0,    0,    0,  293,  293,  293,  293,    0,  293,    0,
    0,  326,  326,  326,    0,  326,  326,    0,    0,    0,
  326,    0,  326,    0,    0,  326,  326,  326,  326,  326,
  326,  326,  326,  326,  326,    0,  326,  326,    0,  326,
  326,  326,  326,  326,  326,  326,  318,  318,  326,  326,
  326,  326,    0,    0,  326,    0,    0,    0,  326,  326,
  326,  326,  326,    0,  326,  326,  326,  326,  326,  326,
  326,    0,  326,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  326,    0,    0,   84,    0,    0,    0,
    0,    0,    0,    0,  326,  326,  326,  326,    0,  326,
    0,    0,  318,  318,  318,    0,  318,  318,    0,    0,
    0,  318,    0,  318,    0,    0,  318,  318,  318,  318,
  318,  318,  318,  318,  318,  318,    0,  318,  318,    0,
  318,  318,  318,  318,  318,  318,  318,  310,  310,  318,
  318,  318,  318,    0,    0,  318,    0,    0,    0,  318,
  318,  318,  318,  318,    0,  318,  318,  318,  318,  318,
  318,  318,    0,  318,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  318,    0,    0,    0,    0,    0,
    0,    0,   25,    0,    0,  318,  318,  318,  318,    0,
  318,    0,    0,  310,  310,  310,    0,  310,  310,    0,
    0,    0,  310,    0,  310,    0,    0,  310,  310,  310,
  310,  310,  310,  310,  310,  310,  310,    0,  310,  310,
    0,  310,  310,  310,  310,  310,  310,  310,  300,  300,
  310,  310,  310,  310,    0,    0,  310,    0,    0,    0,
  310,  310,  310,  310,  310,    0,  310,  310,  310,  310,
  310,  310,  310,    0,  310,    0,    0,    0,    0,    0,
    0,    0,   70,   71,    0,  310,   72,   73,   74,   30,
   31,   32,   33,   34,   94,    0,  310,  310,  310,  310,
   40,  310,    0,    0,  300,  300,  300,    0,  300,  300,
    0,    0,    0,  300,  164,  300,    0,    0,  300,  300,
  300,  300,  300,  300,  300,  300,  300,  300,    0,  300,
  300,    0,  300,  300,  300,  300,  300,  300,  300,  272,
  272,  300,  300,  300,  300,  165,    0,  300,    0,   91,
    0,  300,  300,  300,  300,  300,    0,  300,  300,  300,
  300,  300,  300,  300,    0,  300,    0,  164,    0,    0,
    0,    0,    0,    0,    0,    0,  300,  163,    0,    0,
    0,    0,    0,    0,    0,   84,    0,  300,  300,  300,
  300,    0,  300,    0,    0,  272,  272,  272,  165,  272,
  272,    0,    0,    0,  272,   84,  272,    0,    0,  272,
  272,  272,  272,  272,  272,  272,  272,  272,  272,    0,
  272,  272,    0,  272,  272,  272,  272,  272,  272,  272,
  163,    0,  272,  272,  272,  272,   84,   91,  272,    0,
    0,    0,  272,  272,  272,  272,  272,    0,  272,  272,
  272,  272,  272,  272,  272,  164,  272,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  272,   84,    0,
   25,   25,    0,    0,   25,   25,    0,   25,  272,  272,
  272,  272,    0,  272,    0,    0,  165,    0,    0,    0,
   25,   25,  428,  429,  430,  431,  432,  433,  434,  435,
  436,  437,    0,   91,    0,  338,    0,    0,    0,    0,
    0,  149,  150,  151,    0,  152,  153,  154,  163,  155,
   25,  164,    0,    0,   96,    0,  244,  156,  574,    0,
    0,    0,    0,  157,    0,   36,   37,    0,   38,   39,
  158,   91,   41,    0,   42,   43,   44,   45,   46,    0,
    0,    0,  165,    0,    0,    0,    0,    0,    0,  164,
    0,    0,    0,    0,  149,  150,  151,   50,  152,  153,
  154,    0,  155,    0,    0,    0,    0,    0,    0,  244,
  156,    0,   50,    0,  163,    0,  157,    0,    0,    0,
  165,    0,    0,  158,    0,    0,    0,    0,   51,    0,
    0,    0,   84,   84,   84,    0,   84,   84,   84,    0,
   84,    0,  164,   51,    0,    0,    0,   84,   84,    0,
    0,    0,  163,    0,   84,    0,  159,    0,    0,    0,
   49,   84,    0,    0,    0,    0,    0,    0,    0,    0,
  160,  161,  162,  165,    0,   49,    0,    0,    0,    0,
    0,    0,  149,  150,  151,    0,  152,  153,  154,    0,
  155,    0,    0,    0,    0,    0,    0,  244,  156,    0,
    0,    0,    0,    0,  157,  163,    0,    0,    0,  159,
    0,  158,    0,    0,    0,    0,    0,   96,    0,    0,
    0,    0,    0,  160,  161,  162,  164,    0,   36,   37,
    0,   38,   39,    0,    0,   41,   50,   42,   43,   44,
   45,   46,    0,    0,    0,    0,    0,   84,  149,  150,
  151,    0,  152,  153,  154,    0,  155,  165,  463,    0,
    0,   84,   84,   84,  156,    0,    0,   51,    0,    0,
  157,    0,    0,    0,  149,  150,  151,  158,  152,  153,
  154,    0,  155,    0,    0,    0,  149,  150,  151,  163,
  152,  153,  154,  361,  155,    0,  157,  159,    0,   49,
    0,   22,  156,  158,    0,    0,    0,    0,  157,    0,
   23,  160,  161,  162,    0,  158,   22,    0,   24,   25,
   26,   27,   28,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,  149,
  150,  151,    0,  152,  153,  154,    0,  155,    0,    0,
    0,    0,    0,    0,  244,  156,  492,    0,    0,    0,
    0,  157,    0,  159,    0,    0,    0,    0,  158,    0,
    0,    0,    0,    0,   96,    0,    0,  160,  161,  162,
    0,    0,    0,    0,    0,   36,   37,    0,   38,   39,
    0,    0,   41,  390,   42,   43,   44,   45,   46,    0,
    0,  159,    0,    0,    0,    0,  266,    0,  397,    0,
    0,    0,    0,    0,    0,  160,  161,  162,    0,    0,
    0,    0,    0,  149,  150,  151,    0,  152,  153,  154,
    0,  155,    0,    0,    0,    0,    0,    0,  244,  156,
   22,    0,    0,    0,    0,  157,    0,  563,    0,   23,
    0,    0,  158,    0,  159,    0,    0,   24,   25,   26,
   27,   28,  267,  268,  269,    0,  270,  271,  160,  161,
  162,  272,    0,  273,    0,    0,  274,  275,  276,  277,
  278,  279,  280,  281,  282,  283,    0,  284,  285,    0,
  286,  287,  288,  289,  290,  291,  292,    0,    0,  293,
  294,  295,  296,    0,  266,  297,    0,    0,    0,  298,
  299,  300,  301,  302,    0,  303,  304,  305,  306,  307,
  308,  309,    0,  310,    0,    0,    0,    0,    0,    0,
    0,    0,  399,    0,  311,    0,    0,    0,  159,  582,
    0,    0,    0,    0,    0,  312,  313,  314,  315,    0,
  316,    0,  160,  161,  162,    0,    0,    0,    0,    0,
  267,  268,  269,    0,  270,  271,    0,    0,    0,  272,
    0,  273,    0,    0,  274,  275,  276,  277,  278,  279,
  280,  281,  282,  283,    0,  284,  285,    0,  286,  287,
  288,  289,  290,  291,  292,  266,    0,  293,  294,  295,
  296,    0,    0,  297,    0,    0,    0,  298,  299,  300,
  301,  302,    0,  303,  304,  305,  306,  307,  308,  309,
    0,  310,    0,    0,    0,    0,    0,    0,    0,    0,
  661,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  312,  313,  314,  315,    0,  316,    0,
    0,  267,  268,  269,    0,  270,  271,    0,    0,    0,
  272,    0,  273,    0,    0,  274,  275,  276,  277,  278,
  279,  280,  281,  282,  283,    0,  284,  285,    0,  286,
  287,  288,  289,  290,  291,  292,    0,  266,  293,  294,
  295,  296,    0,    0,  297,    0,    0,    0,  298,  299,
  300,  301,  302,    0,  303,  304,  305,  306,  307,  308,
  309,    0,  310,    0,    0,    0,    0,    0,    0,    0,
    0,  722,    0,  311,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  312,  313,  314,  315,    0,  316,
    0,    0,    0,  267,  268,  269,    0,  270,  271,    0,
    0,    0,  272,    0,  273,    0,    0,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,    0,  284,  285,
    0,  286,  287,  288,  289,  290,  291,  292,  266,    0,
  293,  294,  295,  296,    0,    0,  297,    0,    0,    0,
  298,  299,  300,  301,  302,    0,  303,  304,  305,  306,
  307,  308,  309,    0,  310,    0,    0,    0,    0,    0,
    0,    0,  765,    0,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,  313,  314,  315,
    0,  316,    0,    0,  267,  268,  269,    0,  270,  271,
    0,    0,    0,  272,    0,  273,    0,    0,  274,  275,
  276,  277,  278,  279,  280,  281,  282,  283,    0,  284,
  285,    0,  286,  287,  288,  289,  290,  291,  292,  266,
    0,  293,  294,  295,  296,    0,    0,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,  304,  305,
  306,  307,  308,  309,    0,  310,    0,    0,    0,    0,
    0,    0,    0,  216,    0,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,  313,  314,
  315,    0,  316,    0,    0,  267,  268,  269,    0,  270,
  271,    0,    0,    0,  272,    0,  273,    0,    0,  274,
  275,  276,  277,  278,  279,  280,  281,  282,  283,    0,
  284,  285,    0,  286,  287,  288,  289,  290,  291,  292,
  266,    0,  293,  294,  295,  296,    0,    0,  297,    0,
    0,    0,  298,  299,  300,  301,  302,    0,  303,  304,
  305,  306,  307,  308,  309,    0,  310,    0,    0,    0,
    0,    0,    0,    0,  214,    0,    0,  311,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  312,  313,
  314,  315,    0,  316,    0,    0,  267,  268,  269,    0,
  270,  271,    0,    0,    0,  272,    0,  273,    0,    0,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
    0,  284,  285,    0,  286,  287,  288,  289,  290,  291,
  292,  216,    0,  293,  294,  295,  296,    0,    0,  297,
    0,    0,    0,  298,  299,  300,  301,  302,    0,  303,
  304,  305,  306,  307,  308,  309,    0,  310,    0,    0,
    0,    0,    0,    0,    0,  217,    0,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  313,  314,  315,    0,  316,    0,    0,  216,  216,  216,
    0,  216,  216,    0,    0,    0,  216,    0,  216,    0,
    0,  216,  216,  216,  216,  216,  216,  216,  216,  216,
  216,    0,  216,  216,    0,  216,  216,  216,  216,  216,
  216,  216,  214,    0,  216,  216,  216,  216,    0,    0,
  216,    0,    0,    0,  216,  216,  216,  216,  216,    0,
  216,  216,  216,  216,  216,  216,  216,    0,  216,    0,
    0,    0,    0,    0,    0,    0,  215,    0,    0,  216,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  216,  216,  216,  216,    0,  216,    0,    0,  214,  214,
  214,    0,  214,  214,    0,    0,    0,  214,    0,  214,
    0,    0,  214,  214,  214,  214,  214,  214,  214,  214,
  214,  214,    0,  214,  214,    0,  214,  214,  214,  214,
  214,  214,  214,  217,   91,  214,  214,  214,  214,    0,
    0,  214,    0,    0,    0,  214,  214,  214,  214,  214,
    0,  214,  214,  214,  214,  214,  214,  214,    0,  214,
    0,    0,    0,    0,    0,    0,    0,    0,   50,    0,
  214,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  214,  214,  214,  214,    0,  214,    0,    0,  217,
  217,  217,    0,  217,  217,    0,    0,    0,  217,   51,
  217,    0,    0,  217,  217,  217,  217,  217,  217,  217,
  217,  217,  217,  795,  217,  217,    0,  217,  217,  217,
  217,  217,  217,  217,  215,    0,  217,  217,  217,  217,
    0,   49,  217,    0,    0,    0,  217,  217,  217,  217,
  217,    0,  217,  217,  217,  217,  217,  217,  217,    0,
  217,    0,   50,    0,    0,    0,    0,    0,    0,    0,
    0,  217,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  217,  217,  217,  217,    0,  217,    0,    0,
  215,  215,  215,   51,  215,  215,    0,    0,    0,  215,
    0,  215,    0,    0,  215,  215,  215,  215,  215,  215,
  215,  215,  215,  215,    0,  215,  215,    0,  215,  215,
  215,  215,  215,  215,  215,   49,    0,  215,  215,  215,
  215,    0,   50,  215,    0,    0,    0,  215,  215,  215,
  215,  215,    0,  215,  215,  215,  215,  215,  215,  215,
    0,  215,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  215,   51,  149,  150,  151,    0,  152,  153,
  154,    0,  155,  215,  215,  215,  215,    0,  215,   50,
    0,    0,   22,  361,    0,    0,  157,    0,    0,    0,
    0,   23,   50,  158,   96,   49,    0,    0,    0,   24,
   25,   26,   27,   28,    0,   36,   37,    0,   38,   39,
   51,    0,   41,  129,   42,   43,   44,   45,   46,    0,
    0,    0,    0,   51,    0,    0,   50,    0,    0,    0,
    0,    0,   50,    0,    0,    0,    0,    0,    0,  149,
  150,  151,   49,  152,  153,  154,    0,  155,    0,    0,
    0,    0,    0,    0,    0,   49,    0,   51,  361,  377,
  378,  157,  116,   51,    0,    0,   22,    0,  158,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,    0,   49,
    0,    0,    0,  116,   29,   49,    0,    0,    0,   30,
   31,   32,   33,   34,   35,   36,   37,  117,   38,   39,
   40,    0,   41,    0,   42,   43,   44,   45,   46,    0,
    0,    0,    0,    0,    0,  116,    0,    0,    0,   47,
   48,    0,    0,    0,    0,    0,   22,    0,  117,    0,
    0,    0,    0,    0,    0,   23,    0,    0,    0,    0,
    0,    0,    0,   24,   25,   26,   27,   28,  665,    0,
    0,    0,    0,    0,   96,    0,    0,    0,    0,    0,
  117,    0,    0,    0,   35,   36,   37,   50,   38,   39,
    0,    0,   41,   22,   42,   43,   44,   45,   46,    0,
    0,    0,   23,    0,    0,    0,  179,    0,    0,    0,
   24,   25,   26,   27,   28,   23,    0,    0,   51,    0,
    0,   60,    0,   24,   25,   26,   27,   28,    0,    0,
    0,   75,   36,   37,    0,   38,   39,    0,   50,   41,
   22,   42,   43,   44,   45,   46,   22,  410,    0,   23,
   49,    0,    0,    0,    0,   23,    0,   24,   25,   26,
   27,   28,    0,   24,   25,   26,   27,   28,   96,   51,
    0,    0,    0,    0,    0,  128,  116,    0,    0,   36,
   37,   50,   38,   39,    0,  116,   41,    0,   42,   43,
   44,   45,   46,  116,  116,  116,  116,  116,    0,    0,
    0,   49,    0,    0,  116,    0,    0,    0,    0,    0,
    0,    0,   51,    0,   50,  116,  116,    0,  116,  116,
    0,  117,  116,    0,  116,  116,  116,  116,  116,    0,
  117,    0,    0,    0,    0,    0,    0,    0,  117,  117,
  117,  117,  117,    0,   49,   51,    0,    0,    0,  117,
   50,    0,    0,    0,    0,   50,    0,    0,    0,    0,
  117,  117,    0,  117,  117,    0,   75,  117,    0,  117,
  117,  117,  117,  117,    0,    0,    0,   49,    0,    0,
    0,   51,   75,   50,    0,    0,   51,    0,    0,   50,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,    0,    0,  664,    0,    0,   24,   25,
   26,   27,   28,   49,   51,  182,    0,    0,   49,    0,
   51,   75,   75,    0,   50,   75,   75,    0,   75,   75,
   75,   75,   76,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,   49,    0,    0,    0,
    0,   23,   49,    0,   84,   51,  139,   50,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,    0,
    0,  140,    0,    0,    0,    0,    0,    0,  149,  150,
  151,  340,  152,  153,  154,   22,  155,   49,   51,    0,
  768,    0,    0,   50,   23,  341,    0,  342,    0,    0,
  157,    0,   24,   25,   26,   27,   28,  158,    0,  439,
  440,  149,  150,  151,  340,  152,  153,  154,   22,  155,
   49,   50,    0,    0,   51,    0,   50,   23,  576,    0,
  342,    0,    0,  157,    0,   24,   25,   26,   27,   28,
  158,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   51,    0,  179,  180,   49,   51,    0,  179,
  180,    0,    0,   23,  181,    0,    0,   76,   23,  181,
    0,   24,   25,   26,   27,   28,   24,   25,   26,   27,
   28,    0,    0,   76,   49,    0,    0,  179,  355,   49,
    0,    0,    0,   22,    0,    0,   23,  356,    0,    0,
    0,    0,   23,    0,   24,   25,   26,   27,   28,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,   76,   76,    0,    0,   76,   76,   22,   76,
   76,   76,   76,    0,    0,    0,    0,   23,  728,    0,
    0,  441,  442,  443,  444,   24,   25,   26,   27,   28,
  445,  446,  447,  448,  449,  450,  451,  452,  453,  454,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,    0,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   22,  309,  309,
    0,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,  664,    0,    0,   24,   25,   26,   27,   28,    0,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
  179,    0,    0,    0,   23,    0,    0,    0,    0,   23,
    0,    0,   24,   25,   26,   27,   28,   24,   25,   26,
   27,   28,    0,    0,  309,  309,  309,    0,  309,  309,
    0,    0,    0,  309,    0,  309,    0,    0,  309,  309,
  309,  309,  309,  309,  309,  309,  309,  309,    0,  309,
  309,    0,  309,  309,  309,  309,  309,  309,  309,  273,
  273,  309,  309,  309,  309,    0,    0,  309,    0,    0,
    0,  309,  309,  309,  309,  309,    0,  309,  309,  309,
  309,  309,  309,  309,    0,  309,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,  309,  309,
  309,    0,  309,    0,    0,  273,  273,  273,    0,  273,
  273,    0,    0,    0,  273,    0,  273,    0,    0,  273,
  273,  273,  273,  273,  273,  273,  273,  273,  273,    0,
  273,  273,    0,  273,  273,  273,  273,  273,  273,  273,
  277,  277,  273,  273,  273,  273,    0,    0,  273,    0,
    0,    0,  273,  273,  273,  273,  273,    0,  273,  273,
  273,  273,  273,  273,  273,    0,  273,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  273,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  273,  273,
  273,  273,    0,  273,    0,    0,  277,  277,  277,    0,
  277,  277,    0,    0,    0,  277,    0,  277,    0,    0,
  277,  277,  277,  277,  277,  277,  277,  277,  277,  277,
    0,  277,  277,    0,  277,  277,  277,  277,  277,  277,
  277,  274,  274,  277,  277,  277,  277,    0,    0,  277,
    0,    0,    0,  277,  277,  277,  277,  277,    0,  277,
  277,  277,  277,  277,  277,  277,    0,  277,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  277,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  277,
  277,  277,  277,    0,  277,    0,    0,  274,  274,  274,
    0,  274,  274,    0,    0,    0,  274,    0,  274,    0,
    0,  274,  274,  274,  274,  274,  274,  274,  274,  274,
  274,    0,  274,  274,    0,  274,  274,  274,  274,  274,
  274,  274,  282,  282,  274,  274,  274,  274,    0,    0,
  274,    0,    0,    0,  274,  274,  274,  274,  274,    0,
  274,  274,  274,  274,  274,  274,  274,    0,  274,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  274,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  274,  274,  274,  274,    0,  274,    0,    0,  282,  282,
  282,    0,  282,  282,    0,    0,    0,  282,    0,  282,
    0,    0,  282,  282,  282,  282,  282,  282,  282,  282,
  282,  282,    0,  282,  282,    0,  282,  282,  282,  282,
  282,  282,  282,  296,  296,  282,  282,  282,  282,    0,
    0,  282,    0,    0,    0,  282,  282,  282,  282,  282,
    0,  282,  282,  282,  282,  282,  282,  282,    0,  282,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  282,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  282,  282,  282,  282,    0,  282,    0,    0,  296,
  296,  296,    0,  296,  296,    0,    0,    0,  296,    0,
  296,    0,    0,  296,  296,  296,  296,  296,  296,  296,
  296,  296,  296,    0,  296,  296,    0,  296,  296,  296,
  296,  296,  296,  296,  278,  278,  296,  296,  296,  296,
    0,    0,  296,    0,    0,    0,  296,  296,  296,  296,
  296,    0,  296,  296,  296,  296,  296,  296,  296,    0,
  296,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  296,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  296,  296,  296,  296,    0,  296,    0,    0,
  278,  278,  278,    0,  278,  278,    0,    0,    0,  278,
    0,  278,    0,    0,  278,  278,  278,  278,  278,  278,
  278,  278,  278,  278,    0,  278,  278,    0,  278,  278,
  278,  278,  278,  278,  278,  275,  275,  278,  278,  278,
  278,    0,    0,  278,    0,    0,    0,  278,  278,  278,
  278,  278,    0,  278,  278,  278,  278,  278,  278,  278,
    0,  278,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  278,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  278,  278,  278,  278,    0,  278,    0,
    0,  275,  275,  275,    0,  275,  275,    0,    0,    0,
  275,    0,  275,    0,    0,  275,  275,  275,  275,  275,
  275,  275,  275,  275,  275,    0,  275,  275,    0,  275,
  275,  275,  275,  275,  275,  275,  279,  279,  275,  275,
  275,  275,    0,    0,  275,    0,    0,    0,  275,  275,
  275,  275,  275,    0,  275,  275,  275,  275,  275,  275,
  275,    0,  275,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  275,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  275,  275,  275,  275,    0,  275,
    0,    0,  279,  279,  279,    0,  279,  279,    0,    0,
    0,  279,    0,  279,    0,    0,  279,  279,  279,  279,
  279,  279,  279,  279,  279,  279,    0,  279,  279,    0,
  279,  279,  279,  279,  279,  279,  279,  280,  280,  279,
  279,  279,  279,    0,    0,  279,    0,    0,    0,  279,
  279,  279,  279,  279,    0,  279,  279,  279,  279,  279,
  279,  279,    0,  279,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  279,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  279,  279,  279,  279,    0,
  279,    0,    0,  280,  280,  280,    0,  280,  280,    0,
    0,    0,  280,    0,  280,    0,    0,  280,  280,  280,
  280,  280,  280,  280,  280,  280,  280,    0,  280,  280,
    0,  280,  280,  280,  280,  280,  280,  280,  297,  297,
  280,  280,  280,  280,    0,    0,  280,    0,    0,    0,
  280,  280,  280,  280,  280,    0,  280,  280,  280,  280,
  280,  280,  280,    0,  280,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  280,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  280,  280,  280,  280,
    0,  280,    0,    0,  297,  297,  297,    0,  297,  297,
    0,    0,    0,  297,    0,  297,    0,    0,  297,  297,
  297,  297,  297,  297,  297,  297,  297,  297,    0,  297,
  297,    0,  297,  297,  297,  297,  297,  297,  297,  281,
  281,  297,  297,  297,  297,    0,    0,  297,    0,    0,
    0,  297,  297,  297,  297,  297,    0,  297,  297,  297,
  297,  297,  297,  297,    0,  297,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  297,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  297,  297,  297,
  297,    0,  297,    0,    0,  281,  281,  281,    0,  281,
  281,    0,    0,    0,  281,    0,  281,    0,    0,  281,
  281,  281,  281,  281,  281,  281,  281,  281,  281,    0,
  281,  281,    0,  281,  281,  281,  281,  281,  281,  281,
  266,    0,  281,  281,  281,  281,    0,    0,  281,    0,
    0,    0,  281,  281,  281,  281,  281,    0,  281,  281,
  281,  281,  281,  281,  281,    0,  281,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  281,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  281,  281,
  281,  281,    0,  281,    0,    0,  267,  268,  269,    0,
  270,  271,    0,    0,    0,  272,    0,  273,    0,    0,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
    0,  284,  285,    0,  286,  287,  288,  289,  290,  291,
  292,  220,    0,  293,  294,  295,  296,    0,    0,  297,
    0,    0,    0,  298,  299,  300,  301,  302,    0,  303,
  304,  305,  306,  307,  308,  309,    0,  310,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  313,  314,  315,    0,  316,    0,    0,  220,  220,  220,
    0,  220,  220,    0,    0,    0,  220,    0,  220,    0,
    0,  220,  220,  220,  220,  220,  220,  220,  220,  220,
  220,    0,  220,  220,    0,  220,  220,  220,  220,  220,
  220,  220,  221,    0,  220,  220,  220,  220,    0,    0,
  220,    0,    0,    0,  220,  220,  220,  220,  220,    0,
  220,  220,  220,  220,  220,  220,  220,    0,  220,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  220,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  220,  220,  220,  220,    0,  220,    0,    0,  221,  221,
  221,    0,  221,  221,    0,    0,    0,  221,    0,  221,
    0,    0,  221,  221,  221,  221,  221,  221,  221,  221,
  221,  221,    0,  221,  221,    0,  221,  221,  221,  221,
  221,  221,  221,  222,    0,  221,  221,  221,  221,    0,
    0,  221,    0,    0,    0,  221,  221,  221,  221,  221,
    0,  221,  221,  221,  221,  221,  221,  221,    0,  221,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  221,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  221,  221,  221,  221,    0,  221,    0,    0,  222,
  222,  222,    0,  222,  222,    0,    0,    0,  222,    0,
  222,    0,    0,  222,  222,  222,  222,  222,  222,  222,
  222,  222,  222,    0,  222,  222,    0,  222,  222,  222,
  222,  222,  222,  222,  223,    0,  222,  222,  222,  222,
    0,    0,  222,    0,    0,    0,  222,  222,  222,  222,
  222,    0,  222,  222,  222,  222,  222,  222,  222,    0,
  222,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  222,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  222,  222,  222,  222,    0,  222,    0,    0,
  223,  223,  223,    0,  223,  223,    0,    0,    0,  223,
    0,  223,    0,    0,  223,  223,  223,  223,  223,  223,
  223,  223,  223,  223,    0,  223,  223,    0,  223,  223,
  223,  223,  223,  223,  223,    0,    0,  223,  223,  223,
  223,    0,    0,  223,    0,    0,    0,  223,  223,  223,
  223,  223,    0,  223,  223,  223,  223,  223,  223,  223,
    0,  223,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  223,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  223,  223,  223,  223,  273,  223,    0,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
    0,  284,  285,    0,  286,  287,  288,  289,  290,  291,
  292,    0,    0,  293,  294,  295,  296,    0,    0,  297,
    0,    0,    0,  298,  299,  300,  301,  302,    0,  303,
  304,  305,  306,  307,  308,  309,    0,  310,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
  313,  314,  315,    0,  316,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   54,  147,    6,   60,  262,  136,  483,   33,   60,
   44,  234,   61,  236,  498,  123,  146,  123,   60,   42,
    6,    7,  123,  123,  123,   40,  317,   40,   62,   77,
   78,   60,   40,  123,  257,   61,  533,  276,  214,   92,
   91,  280,   49,   40,   61,    0,  719,   44,  123,   56,
   57,   44,  197,   60,   57,   44,   63,   42,  106,   40,
   63,  458,   91,    0,  194,  274,  196,  324,   75,  214,
   56,   57,  123,   44,   60,  366,  749,   63,  663,   44,
   40,  123,   89,   33,   44,   40,  142,   94,  136,  265,
  101,   98,  266,  100,  123,   44,   44,  100,  105,   41,
  309,  585,   44,  110,   42,  112,  317,  252,   93,  254,
  367,  285,   98,   42,  100,   41,  123,  124,   44,  126,
  343,  618,  109,  134,  621,  255,  137,   42,  125,   44,
  290,  291,  125,   62,  373,  380,  125,    0,   42,  384,
   44,  726,  387,   61,  320,   93,  322,  272,  123,  822,
  198,  396,  302,   91,  125,  260,   60,  317,  555,  556,
  125,  558,  169,   60,  171,  125,  274,  257,  274,   41,
   61,    0,   44,  274,  274,  274,  125,  327,  328,  470,
   61,  168,  124,  170,  195,  669,  193,   91,  275,  365,
  277,  268,  269,   40,   91,  272,  273,  284,  275,  274,
  280,   76,  493,   78,   41,  253,  213,   44,  211,  270,
  271,  288,  289,  264,  359,   41,  290,  291,   44,  123,
  294,  295,  273,  294,  295,    0,  123,  234,  358,  257,
  281,  282,  283,  284,  285,  264,  111,  494,  113,   42,
  724,  318,  317,  230,  273,  287,  233,   40,  645,  274,
  647,  648,  281,  282,  283,  284,  285,  272,   40,  272,
  483,  314,  485,  270,  272,  488,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  767,  270,  274,  293,  294,  311,  465,
  297,  272,  583,   41,  770,   61,   44,  781,  373,  274,
  476,   41,  260,  274,   44,  312,  313,  314,   42,  316,
   44,  314,   40,  268,  269,  491,  260,  272,  325,  495,
  275,  718,  373,  580,  274,   40,   41,  277,  314,   44,
  257,  268,  269,  288,  289,  272,  273,  123,  275,   40,
   41,  486,  317,   44,   41,  374,   41,   44,   42,   44,
   44,  288,  289,  257,  258,  259,  267,  261,  262,  263,
  267,  265,   41,  318,    0,   44,  302,  264,  659,  273,
   42,   41,   44,  380,   44,  279,  273,  384,  290,  291,
  387,  318,  286,  390,  281,  282,  283,  284,  285,  396,
  397,   41,  399,  569,   44,  571,  293,   41,  373,   41,
   44,  408,   44,  660,   42,  268,  269,  414,  461,  272,
  273,   42,  275,   44,   42,   42,   44,   44,  120,   41,
   42,   41,   42,   41,   42,  288,  289,   41,   42,   41,
   42,  438,  723,   41,   42,  164,  165,  123,  123,  268,
  269,   40,  123,  272,  273,   41,  275,  317,  455,    0,
  374,   40,  459,  460,  461,  318,   40,  460,  461,  288,
  289,  163,  164,  165,   40,  472,  473,  474,  372,   44,
   58,    0,   58,  459,  460,  461,  178,   58,  485,  655,
   62,  657,  386,  387,  388,  184,   40,   61,  257,  318,
  274,   40,   44,  268,  269,  373,  373,  272,  273,  260,
  275,   44,  274,    0,  260,  257,  729,  274,   61,  326,
  274,  414,  642,  288,  289,  276,  125,  381,  257,  221,
  222,  223,  381,  381,  531,  373,  533,   44,  373,  536,
  537,  538,  539,  540,  541,  542,  543,  544,  545,  546,
  547,  243,  373,  318,  257,   44,   40,  770,  273,   44,
  557,  124,  559,  560,  315,  316,   44,  560,  319,  320,
   44,  322,  323,  324,  325,  267,  711,  269,    0,  271,
   44,  557,   44,  559,  560,  381,  381,  381,  274,  381,
  587,  381,  712,  713,  381,  715,  381,  381,  290,  291,
  292,    0,  381,  295,  381,  257,  298,  299,  300,  301,
  302,  303,  304,  305,  306,  307,  308,  309,  381,  257,
  666,  618,  757,  758,  621,  760,  381,  257,  373,  257,
  258,  259,  373,  261,  262,  263,   44,  265,  330,  266,
   40,  761,  268,  269,  272,  273,  272,  273,   44,  275,
   44,  279,  649,   44,   44,   44,  791,   44,  286,   44,
   44,  260,  288,  289,   44,    0,  663,  664,   44,   44,
   44,   44,   44,  649,   44,    0,   44,  276,   44,   44,
   44,   44,  257,  372,  257,   91,  381,    0,   44,   44,
  379,   44,  318,  382,  383,   44,  385,  386,   44,  388,
  389,   44,  391,  392,  393,  394,  395,   44,   44,  398,
   44,  400,  401,  402,  403,  554,  315,  316,  410,  257,
  319,  320,   44,  322,  323,  324,  325,  268,  269,  726,
   44,  272,  273,   44,  275,   44,  733,  730,   44,   40,
   41,   44,   44,   44,  372,   44,   91,  288,  289,  268,
  269,   44,   44,  272,  273,   44,  275,  373,  386,  387,
  388,   62,  373,   44,  373,   44,   44,   40,  457,  288,
  289,  321,   41,  373,  257,  257,  373,  318,  257,   44,
  374,  268,  269,   93,   40,  272,  273,   40,  275,   40,
  257,   60,   93,   40,  257,   44,  381,  489,  381,  318,
  381,  288,  289,    0,  643,  644,  272,  646,   40,  806,
   10,  272,  272,  502,  811,  812,  813,  506,  272,   79,
  509,  178,   91,  512,  125,  177,   19,  238,  485,  518,
  519,  318,  521,  193,  526,  527,  528,  529,  209,   40,
   54,   89,  788,  535,  213,  369,  268,  269,  319,  319,
  272,  273,  488,  275,  123,  733,  726,   -1,  640,  548,
  549,  550,   -1,   -1,   -1,   -1,  288,  289,   -1,  268,
  269,   -1,  564,  272,  273,  714,  275,  716,  717,   -1,
   -1,   -1,   42,   -1,   -1,   -1,   -1,   -1,   -1,  288,
  289,   -1,   -1,   -1,   -1,   -1,  318,   -1,   -1,   -1,
  589,   -1,  591,  592,   -1,  594,  595,   -1,  597,  598,
   -1,  600,  601,  602,  603,  604,   -1,   -1,  607,  318,
  609,  610,  611,  612,  763,   -1,   -1,   -1,   -1,   -1,
  619,  623,   -1,  268,  269,   -1,   40,  272,  273,   -1,
  275,   -1,   -1,  268,  269,   -1,   -1,  272,  273,  641,
  275,   -1,   -1,  288,  289,  268,  269,   -1,   -1,  272,
  273,   -1,  275,  288,  289,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  274,   -1,  288,  289,  666,   -1,   -1,
   -1,   -1,  671,  318,   -1,  674,   -1,   -1,  677,   -1,
   -1,  680,   -1,  318,   -1,  264,   -1,  686,  687,   -1,
  689,   -1,  694,  695,  273,  318,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  706,  707,  708,
   -1,   -1,   -1,   -1,  293,   -1,   -1,   40,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,  730,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,   -1,  756,  366,  367,  368,  369,   -1,
   -1,  372,  273,  274,   -1,  376,  377,  378,  379,  380,
  381,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  401,  261,  262,  263,   -1,  265,  798,  799,  800,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   40,  279,
   -1,   -1,  814,   -1,   -1,   -1,  286,   -1,  329,  330,
  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  349,  350,
  351,  352,   -1,  354,  355,   -1,  357,  358,  359,  360,
  361,  362,  363,   -1,   -1,  366,  367,  368,  369,  273,
  274,  372,   60,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
  401,  265,   -1,   91,   -1,   -1,   -1,   -1,   -1,   40,
   -1,  412,  413,  414,  415,  279,  417,   -1,   -1,   -1,
   -1,   -1,  286,   -1,   -1,  329,  330,  331,   -1,  333,
  334,   -1,   -1,   -1,  338,  123,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  273,  274,  366,  367,  368,  369,   -1,   -1,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,  413,
  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,   -1,
  333,  334,   -1,   -1,   -1,  338,   -1,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,  273,  274,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,  264,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  401,   -1,
   -1,   40,   -1,  281,  282,  283,  284,  285,   -1,  412,
  413,  414,  415,   -1,  417,   -1,   -1,  329,  330,  331,
   -1,  333,  334,   -1,   -1,   -1,  338,  305,  340,   -1,
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
   -1,  401,   -1,   -1,   40,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,  274,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   40,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,  273,  274,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,   -1,   -1,   40,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,
   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,  273,  274,  366,
  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,    0,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  349,  350,  351,  352,   -1,  354,  355,
   -1,  357,  358,  359,  360,  361,  362,  363,  273,  274,
  366,  367,  368,  369,   -1,   -1,  372,   -1,   -1,   -1,
  376,  377,  378,  379,  380,   -1,  382,  383,  384,  385,
  386,  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  290,  291,   -1,  401,  294,  295,  296,  297,
  298,  299,  300,  301,   40,   -1,  412,  413,  414,  415,
  308,  417,   -1,   -1,  329,  330,  331,   -1,  333,  334,
   -1,   -1,   -1,  338,   60,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  349,  350,  351,  352,   -1,  354,
  355,   -1,  357,  358,  359,  360,  361,  362,  363,  273,
  274,  366,  367,  368,  369,   91,   -1,  372,   -1,   42,
   -1,  376,  377,  378,  379,  380,   -1,  382,  383,  384,
  385,  386,  387,  388,   -1,  390,   -1,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,  123,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   40,   -1,  412,  413,  414,
  415,   -1,  417,   -1,   -1,  329,  330,  331,   91,  333,
  334,   -1,   -1,   -1,  338,   60,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  349,  350,  351,  352,   -1,
  354,  355,   -1,  357,  358,  359,  360,  361,  362,  363,
  123,   -1,  366,  367,  368,  369,   91,   42,  372,   -1,
   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,  383,
  384,  385,  386,  387,  388,   60,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  123,   -1,
  268,  269,   -1,   -1,  272,  273,   -1,  275,  412,  413,
  414,  415,   -1,  417,   -1,   -1,   91,   -1,   -1,   -1,
  288,  289,  391,  392,  393,  394,  395,  396,  397,  398,
  399,  400,   -1,   42,   -1,   44,   -1,   -1,   -1,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,  123,  265,
  318,   60,   -1,   -1,  292,   -1,  272,  273,   41,   -1,
   -1,   -1,   -1,  279,   -1,  303,  304,   -1,  306,  307,
  286,   42,  310,   -1,  312,  313,  314,  315,  316,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,   -1,   -1,   -1,  257,  258,  259,   60,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   60,   -1,  123,   -1,  279,   -1,   -1,   -1,
   91,   -1,   -1,  286,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   60,   91,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,  123,   -1,  279,   -1,  372,   -1,   -1,   -1,
  123,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,  387,  388,   91,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,  123,   -1,   -1,   -1,  372,
   -1,  286,   -1,   -1,   -1,   -1,   -1,  292,   -1,   -1,
   -1,   -1,   -1,  386,  387,  388,   60,   -1,  303,  304,
   -1,  306,  307,   -1,   -1,  310,   60,  312,  313,  314,
  315,  316,   -1,   -1,   -1,   -1,   -1,  372,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   91,  125,   -1,
   -1,  386,  387,  388,  273,   -1,   -1,   91,   -1,   -1,
  279,   -1,   -1,   -1,  257,  258,  259,  286,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,  257,  258,  259,  123,
  261,  262,  263,  276,  265,   -1,  279,  372,   -1,  123,
   -1,  264,  273,  286,   -1,   -1,   -1,   -1,  279,   -1,
  273,  386,  387,  388,   -1,  286,  264,   -1,  281,  282,
  283,  284,  285,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,   -1,  272,  273,  125,   -1,   -1,   -1,
   -1,  279,   -1,  372,   -1,   -1,   -1,   -1,  286,   -1,
   -1,   -1,   -1,   -1,  292,   -1,   -1,  386,  387,  388,
   -1,   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,
   -1,   -1,  310,  356,  312,  313,  314,  315,  316,   -1,
   -1,  372,   -1,   -1,   -1,   -1,  273,   -1,  356,   -1,
   -1,   -1,   -1,   -1,   -1,  386,  387,  388,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
  264,   -1,   -1,   -1,   -1,  279,   -1,  125,   -1,  273,
   -1,   -1,  286,   -1,  372,   -1,   -1,  281,  282,  283,
  284,  285,  329,  330,  331,   -1,  333,  334,  386,  387,
  388,  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  349,  350,  351,  352,   -1,  354,  355,   -1,
  357,  358,  359,  360,  361,  362,  363,   -1,   -1,  366,
  367,  368,  369,   -1,  273,  372,   -1,   -1,   -1,  376,
  377,  378,  379,  380,   -1,  382,  383,  384,  385,  386,
  387,  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  356,   -1,  401,   -1,   -1,   -1,  372,  125,
   -1,   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,
  417,   -1,  386,  387,  388,   -1,   -1,   -1,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  273,   -1,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  125,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,
   -1,  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,
  338,   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  349,  350,  351,  352,   -1,  354,  355,   -1,  357,
  358,  359,  360,  361,  362,  363,   -1,  273,  366,  367,
  368,  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,
  378,  379,  380,   -1,  382,  383,  384,  385,  386,  387,
  388,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  125,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  412,  413,  414,  415,   -1,  417,
   -1,   -1,   -1,  329,  330,  331,   -1,  333,  334,   -1,
   -1,   -1,  338,   -1,  340,   -1,   -1,  343,  344,  345,
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
  361,  362,  363,  273,   42,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   91,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   41,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  273,   -1,  366,  367,  368,  369,
   -1,  123,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   91,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,  123,   -1,  366,  367,  368,
  369,   -1,   60,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   91,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,  412,  413,  414,  415,   -1,  417,   60,
   -1,   -1,  264,  276,   -1,   -1,  279,   -1,   -1,   -1,
   -1,  273,   60,  286,  292,  123,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,  303,  304,   -1,  306,  307,
   91,   -1,  310,   41,  312,  313,  314,  315,  316,   -1,
   -1,   -1,   -1,   91,   -1,   -1,   60,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,  123,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  123,   -1,   91,  276,  341,
  342,  279,   60,   91,   -1,   -1,  264,   -1,  286,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,  123,
   -1,   -1,   -1,   91,  292,  123,   -1,   -1,   -1,  297,
  298,  299,  300,  301,  302,  303,  304,   60,  306,  307,
  308,   -1,  310,   -1,  312,  313,  314,  315,  316,   -1,
   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,  327,
  328,   -1,   -1,   -1,   -1,   -1,  264,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   41,   -1,
   -1,   -1,   -1,   -1,  292,   -1,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  302,  303,  304,   60,  306,  307,
   -1,   -1,  310,  264,  312,  313,  314,  315,  316,   -1,
   -1,   -1,  273,   -1,   -1,   -1,  264,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  273,   -1,   -1,   91,   -1,
   -1,  292,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,  125,  303,  304,   -1,  306,  307,   -1,   60,  310,
  264,  312,  313,  314,  315,  316,  264,  305,   -1,  273,
  123,   -1,   -1,   -1,   -1,  273,   -1,  281,  282,  283,
  284,  285,   -1,  281,  282,  283,  284,  285,  292,   91,
   -1,   -1,   -1,   -1,   -1,  293,  264,   -1,   -1,  303,
  304,   60,  306,  307,   -1,  273,  310,   -1,  312,  313,
  314,  315,  316,  281,  282,  283,  284,  285,   -1,   -1,
   -1,  123,   -1,   -1,  292,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,   60,  303,  304,   -1,  306,  307,
   -1,  264,  310,   -1,  312,  313,  314,  315,  316,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,  123,   91,   -1,   -1,   -1,  292,
   60,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,
  303,  304,   -1,  306,  307,   -1,  260,  310,   -1,  312,
  313,  314,  315,  316,   -1,   -1,   -1,  123,   -1,   -1,
   -1,   91,  276,   60,   -1,   -1,   91,   -1,   -1,   60,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,
  283,  284,  285,  123,   91,  125,   -1,   -1,  123,   -1,
   91,  315,  316,   -1,   60,  319,  320,   -1,  322,  323,
  324,  325,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,  273,  123,   -1,  125,   91,  278,   60,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  293,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,  260,  261,  262,  263,  264,  265,  123,   91,   -1,
   93,   -1,   -1,   60,  273,  274,   -1,  276,   -1,   -1,
  279,   -1,  281,  282,  283,  284,  285,  286,   -1,  261,
  262,  257,  258,  259,  260,  261,  262,  263,  264,  265,
  123,   60,   -1,   -1,   91,   -1,   60,  273,  274,   -1,
  276,   -1,   -1,  279,   -1,  281,  282,  283,  284,  285,
  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,  264,  265,  123,   91,   -1,  264,
  265,   -1,   -1,  273,  274,   -1,   -1,  260,  273,  274,
   -1,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,   -1,   -1,  276,  123,   -1,   -1,  264,  265,  123,
   -1,   -1,   -1,  264,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,  273,   -1,  281,  282,  283,  284,  285,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  315,  316,   -1,   -1,  319,  320,  264,  322,
  323,  324,  325,   -1,   -1,   -1,   -1,  273,  274,   -1,
   -1,  393,  394,  395,  396,  281,  282,  283,  284,  285,
  402,  403,  404,  405,  406,  407,  408,  409,  410,  411,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,
   -1,   -1,  281,  282,  283,  284,  285,  281,  282,  283,
  284,  285,   -1,   -1,  329,  330,  331,   -1,  333,  334,
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
  361,  362,  363,  273,   -1,  366,  367,  368,  369,   -1,
   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,  380,
   -1,  382,  383,  384,  385,  386,  387,  388,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,  329,
  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,   -1,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  349,
  350,  351,  352,   -1,  354,  355,   -1,  357,  358,  359,
  360,  361,  362,  363,  273,   -1,  366,  367,  368,  369,
   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,  379,
  380,   -1,  382,  383,  384,  385,  386,  387,  388,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  412,  413,  414,  415,   -1,  417,   -1,   -1,
  329,  330,  331,   -1,  333,  334,   -1,   -1,   -1,  338,
   -1,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  349,  350,  351,  352,   -1,  354,  355,   -1,  357,  358,
  359,  360,  361,  362,  363,   -1,   -1,  366,  367,  368,
  369,   -1,   -1,  372,   -1,   -1,   -1,  376,  377,  378,
  379,  380,   -1,  382,  383,  384,  385,  386,  387,  388,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  412,  413,  414,  415,  340,  417,   -1,
  343,  344,  345,  346,  347,  348,  349,  350,  351,  352,
   -1,  354,  355,   -1,  357,  358,  359,  360,  361,  362,
  363,   -1,   -1,  366,  367,  368,  369,   -1,   -1,  372,
   -1,   -1,   -1,  376,  377,  378,  379,  380,   -1,  382,
  383,  384,  385,  386,  387,  388,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  412,
  413,  414,  415,   -1,  417,
  };

#line 1201 "Iril/IR/IR.jay"

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
