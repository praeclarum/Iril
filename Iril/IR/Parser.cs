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
    "AVAILABLE_EXTERNALLY","PERSONALITY","SRET","NONNULL","NOCAPTURE",
    "WRITEONLY","READONLY","READNONE","ATTRIBUTE_GROUP_REF","ATTRIBUTES",
    "NORECURSE","NOUNWIND","SPECULATABLE","SSP","UWTABLE","ARGMEMONLY",
    "SEQ_CST","DSO_LOCAL","DSO_PREEMPTABLE","RET","BR","SWITCH",
    "INDIRECTBR","INVOKE","RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET",
    "UNREACHABLE","FNEG","ADD","NUW","NSW","FADD","SUB","FSUB","MUL",
    "FMUL","UDIV","SDIV","FDIV","UREM","SREM","FREM","SHL","LSHR","EXACT",
    "ASHR","AND","OR","XOR","EXTRACTELEMENT","INSERTELEMENT",
    "SHUFFLEVECTOR","EXTRACTVALUE","INSERTVALUE","ALLOCA","LOAD","STORE",
    "FENCE","CMPXCHG","ATOMICRMW","GETELEMENTPTR","ALIGN","INBOUNDS",
    "INRANGE","TRUNC","ZEXT","SEXT","FPTRUNC","FPEXT","TO","FPTOUI",
    "FPTOSI","UITOFP","SITOFP","PTRTOINT","INTTOPTR","BITCAST",
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
#line 540 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 153:
#line 563 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 154:
#line 564 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 155:
#line 565 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 156:
#line 566 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 157:
#line 567 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 158:
#line 568 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 159:
#line 569 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 160:
#line 570 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 161:
#line 571 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 162:
#line 572 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 163:
#line 576 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 164:
#line 577 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 165:
#line 578 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 166:
#line 579 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 167:
#line 580 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 168:
#line 581 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 169:
#line 582 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 170:
#line 583 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 171:
#line 584 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 172:
#line 585 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 173:
#line 586 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 174:
#line 587 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 175:
#line 588 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 176:
#line 589 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 177:
#line 590 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 178:
#line 591 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 179:
#line 595 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 183:
#line 605 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 184:
#line 609 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 185:
#line 613 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 186:
#line 617 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 621 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 188:
#line 625 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 189:
#line 629 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 190:
#line 633 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 192:
#line 641 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 193:
#line 642 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 194:
#line 643 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 195:
#line 644 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 196:
#line 645 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 197:
#line 646 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 198:
#line 647 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 199:
#line 648 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 200:
#line 649 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 201:
#line 656 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 663 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 204:
#line 674 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 681 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 685 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 692 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 703 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 707 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 714 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 718 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 725 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 729 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 215:
#line 733 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 737 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 744 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 748 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 759 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 767 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 775 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 225:
#line 776 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 226:
#line 783 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 787 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 794 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 229:
#line 798 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 230:
#line 802 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 231:
#line 806 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 232:
#line 810 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 233:
#line 814 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 234:
#line 818 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 236:
#line 823 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 237:
#line 827 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 238:
#line 831 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 239:
#line 835 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 240:
#line 839 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 245:
#line 856 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 860 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 866 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 248:
#line 873 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 877 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 884 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 902 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 257:
#line 909 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 913 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 917 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 921 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 261:
#line 925 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 262:
#line 932 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 936 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 940 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)null);
    }
  break;
case 265:
#line 944 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-5+yyTop], (int)(BigInteger)yyVals[0+yyTop], numElements: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 266:
#line 948 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 952 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 268:
#line 956 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 269:
#line 960 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 964 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 271:
#line 968 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 272:
#line 972 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 273:
#line 976 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 274:
#line 980 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 275:
#line 984 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 276:
#line 988 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 277:
#line 992 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 278:
#line 996 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 279:
#line 1000 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 280:
#line 1004 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 281:
#line 1008 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 282:
#line 1012 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1016 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1020 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1024 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1028 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1032 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1036 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1040 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1044 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1048 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1052 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1056 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1060 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1064 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1068 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1072 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1076 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 299:
#line 1080 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 300:
#line 1084 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 301:
#line 1088 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 302:
#line 1092 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1096 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1100 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1104 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1108 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1112 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1116 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1120 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1124 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1128 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1132 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1136 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1140 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 315:
#line 1144 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 316:
#line 1148 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 317:
#line 1152 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 318:
#line 1156 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 319:
#line 1160 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 320:
#line 1164 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 321:
#line 1168 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 322:
#line 1172 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 323:
#line 1176 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 324:
#line 1180 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 325:
#line 1184 "Iril/IR/IR.jay"
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
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-10+yyTop], (IEnumerable<Parameter>)yyVals[-9+yyTop], (List<Block>)yyVals[-2+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-4+yyTop]);
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
   37,   37,   37,   37,   37,   14,   14,   39,   39,   34,
   34,   44,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   46,   46,   46,   46,   46,   46,   46,   46,
   46,   46,   46,   46,   46,   46,   46,   46,   47,   48,
   48,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   51,   19,   19,   19,   19,   19,   19,   19,   19,   19,
   52,   26,   26,   53,   50,   50,   24,   54,   49,   49,
   35,   35,   55,   55,   55,   55,   56,   56,   58,   58,
   58,   58,   60,   61,   61,   62,   62,   63,   63,   63,
   63,   63,   63,   63,   64,   64,   64,   64,   64,   64,
   20,   20,   65,   65,   66,   66,   67,   68,   68,   69,
   70,   70,   71,   71,   40,   72,   57,   57,   57,   57,
   57,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,   59,   59,   59,   59,   59,
   59,   59,   59,   59,   59,
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
    1,    1,    1,    1,    4,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    6,    9,    6,    6,    3,    3,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    2,    1,    2,    1,    3,    2,    1,    1,    3,
    1,    2,    2,    3,    1,    2,    1,    2,    1,    2,
    3,    4,    1,    3,    2,    1,    3,    2,    3,    3,
    3,    2,    4,    5,    1,    1,    6,    9,    6,    6,
    1,    3,    1,    1,    1,    3,    5,    1,    2,    3,
    1,    2,    1,    1,    1,    1,    2,    7,    2,    7,
    1,    5,    6,    5,    7,    5,    5,    6,    4,    4,
    5,    6,    6,    5,    6,    6,    6,    7,    5,    6,
    7,    4,    5,    6,    5,    2,    5,    4,    4,    4,
    4,    5,    6,    7,    6,    6,    4,    7,    8,    5,
    6,    5,    5,    6,    3,    4,    5,    6,    7,    4,
    5,    6,    6,    4,    5,    7,    8,    5,    6,    4,
    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   85,   95,   88,   89,   90,   91,   87,  113,   34,
   32,   35,   36,   37,  255,  141,  142,  143,    0,   33,
  144,  136,  137,  139,  138,  140,  148,  149,    0,    0,
    0,   86,    0,    0,    0,    0,    0,  114,  115,    0,
    0,  134,    0,    0,    3,    0,    4,    0,    0,  146,
  147,   30,   31,   38,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   79,    0,    0,    0,    0,    0,    0,
   94,    0,  119,    0,    0,    0,    0,    0,    0,    0,
    0,  135,    0,    0,    0,    5,    6,    0,    0,    0,
    0,    0,    8,    0,    7,    0,    0,    0,    0,    0,
   80,    0,    0,    0,    0,  118,  101,   92,    0,    0,
   98,    0,    0,    0,    0,    0,    0,  132,  133,  127,
    0,    0,  128,  152,    0,    0,  150,  194,  195,  193,
  196,  197,  198,  192,  183,  200,  199,    0,    0,    0,
    0,    0,    0,    0,    0,  182,    0,    0,    0,    0,
    0,    0,    0,    0,   39,    0,    0,    0,   64,   63,
   13,    0,    0,   57,   62,  145,    0,    0,    0,    0,
   93,    0,    0,    0,    0,    0,    0,    0,    0,   77,
   69,   67,   68,   70,   71,   72,   73,    0,   65,    0,
  126,    0,    0,    0,    0,    0,    0,  151,    0,    0,
    0,    0,  205,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   58,
   14,    0,  179,  181,  180,  202,   96,   81,   97,   99,
    0,    0,    0,    0,    0,    0,   12,   66,  129,    0,
    0,    0,   55,    0,    0,    0,    0,    0,  261,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  211,    0,    0,  217,    0,    0,    0,
    0,    0,    0,    0,  190,    0,  188,  189,    0,    0,
    0,    0,    0,    0,    0,   52,    0,   50,    0,   41,
   53,    0,   47,   49,   54,   42,   43,   40,   17,   16,
   61,   60,   59,    0,    0,   74,  244,  243,    0,  241,
    0,    0,    0,    0,    0,  259,    0,    0,  257,    0,
  253,  254,    0,    0,  251,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  256,  286,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  153,  154,  155,  156,  157,  158,  159,  160,  161,
  162,    0,  163,  164,  175,  176,  177,  178,  166,  168,
  169,  170,  171,  167,  165,  173,  174,  172,    0,    0,
    0,    0,    0,    0,    0,  102,  212,    0,  218,    0,
    0,   56,    0,    0,    0,    0,    0,  206,    0,    0,
    0,   28,    0,    0,    0,    0,  207,    0,    0,   78,
    0,    0,    0,    0,  105,    0,    0,    0,  201,    0,
    0,    0,    0,  252,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  245,    0,  223,    0,    0,    0,    0,    0,
    0,    0,    0,  104,    0,    0,    0,    0,    0,    0,
    0,    0,   23,    0,   45,    0,   51,   48,  242,  120,
    0,    0,  108,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  282,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  187,  184,  186,    0,    0,    0,    0,   44,    0,    0,
  103,    0,    0,  262,    0,  283,  318,    0,  292,  303,
    0,  287,  321,    0,  307,  285,  323,  315,  311,    0,
    0,  300,    0,  267,  266,  302,  324,    0,    0,  264,
    0,    0,  191,  204,    0,    0,    0,    0,    0,    0,
    0,    0,  246,    0,    0,  225,    0,    0,  226,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  106,    0,    0,    0,    0,  248,  263,  319,  304,  308,
  312,  301,  268,  296,  313,    0,    0,    0,    0,    0,
    0,  208,    0,  209,  295,  284,    0,    0,    0,    0,
    0,  228,  224,    0,    0,    0,  273,    0,    0,  280,
    0,    0,  107,  258,    0,  260,  249,  265,    0,  298,
    0,  316,    0,    0,  247,  309,    0,  236,  230,    0,
    0,    0,    0,  235,  231,  229,  227,    0,  281,  185,
  250,  299,  317,  210,  233,    0,    0,    0,    0,    0,
  234,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  240,  237,  239,    0,    0,  238,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   52,   12,   13,   14,  208,  182,  174,   75,
  183,  244,  217,   76,   77,   54,  175,  340,  166,  359,
  342,  343,  344,  345,  184,  732,  209,   86,   87,  130,
  131,   15,  105,  146,  313,  484,   62,   57,   58,   59,
   63,  142,  143,  147,  432,  449,  245,  545,  733,  224,
  684,  369,  609,  734,  314,  315,  316,  317,  318,  546,
  632,  698,  699,  775,  360,  542,  543,  715,  716,  374,
  375,  407,
  };
  protected static readonly short [] yySindex = {          492,
  -24,  -80,   -3,    3,   20, 3310, 3399, -231,    0,  492,
    0,    0,    0,    0, -182, -158,   76,   95, 1216, -112,
  -23,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  134,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3841, -114,
  -74,    0,  173, -115,  188, 3922, 3496,    0,    0, 3496,
  -28,    0, 3496,  196,    0,  241,    0,   27,   34,    0,
    0,    0,    0,    0, 3922,  -73,  -30,  -77,    5,  266,
  -27,  187,   68,    0,  173,  -10,  188,   52, 3922,   65,
    0,   51,    0, 3625,  188,  188, 3922,  -20, 3496,  241,
  -17,    0,  215, 3599, -138,    0,    0, 2044, 3922,  -73,
 3922,  -73,    0,  253,    0, -265,  346,  272, 3864,  348,
    0, 3922, 3922,    1, 3922,    0,    0,    0,  173,   36,
    0,  188,  241,  -16, -138,  241, 1540,    0,    0,    0,
  118,  144,    0,    0,   90, -109,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   55,  383,  394,
  395, 3936, 3936, 3936,  392,    0, 2044, 3922,  543, 3922,
  379,  381,  382,  252,    0, -265, 3752,    0,    0,    0,
    0,   15, 1980,    0,    0,    0,  173,   93,  388,  -11,
    0, 3726,   90,  241,   90,   90,  -30,  380,  405,    0,
    0,    0,    0,    0,    0,    0,    0,  233,    0, 1289,
    0, 3692,  -70,  177,  201, 4980, -107,    0,  422, 3936,
 3936, 3936,    0,   22,   80,    2,   96,  425, 2044,  100,
  430, 2095, 3804,  202, 1163,    0, -265,  279,   16,    0,
    0, 3869,    0,    0,    0,    0,    0,    0,    0,    0,
   90,  -30,   90,   90,  217, 1074,    0,    0,    0,  221,
 4980,  -98,    0,  205,  419, 3936, -222, 3936,    0,  347,
 3922,  347, 3922,  347, 3922, 3922, 2254, 3922, 3922, 3922,
  347, 2276, 2282, 3922, 3922, 3922, 3936, 3936, 3936, 3922,
 3632, 3638,  159, 2221, 3936, 3936, 3936, 3936, 3936, 3936,
 3936, 3936, 3936, 3936, 3936, 3936, 1298, 3839, 3922, 3922,
 3461,   70, 2268,    0, 4980,  205,    0,  205,  212, 4980,
 3922,  108,  111,  115,    0, 3936,    0,    0,  238,  127,
  456,  244,  132,  135,  458,    0,  467,    0,  985,    0,
    0,  384,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   90,   90,    0,    0,    0,  292,    0,
 -178, 2358, 4980,  -97, 5368,    0,  237, 2198,    0,  469,
    0,    0, 1980,  347,    0, 1980, 1980,  347, 1980, 1980,
  347, 1980, 1980, 3922, 1980, 1980, 1980, 1980, 1980,  347,
 3922, 1980, 3922, 1980, 1980, 1980, 1980,  470,  471,  473,
   31, 3922,  131, 3936,  475,    0,    0, 3922,  157,  141,
  155,  156,  161,  162,  165,  178,  180,  182,  185,  186,
  193,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3922,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3922,   -4,
 1980,  321, 3922, 3496, 3461,    0,    0,  205,    0,  262,
  262,    0, 2447,  256, 3922, 3922, 3922,    0,  205,  280,
  140,    0,  281,  301,  210, 1970,    0, 3834,   90,    0,
 1074, 3936,  -94,  205,    0, 2536, 4980,  205,    0,  544,
  326,  545, 1980,    0,  551,  552, 1980,  553,  554, 1980,
  555,  558, 1980,  561,  562,  564,  565,  566, 1980, 1980,
  568, 1980,  569,  573,  579,  582, 3936, 3936, 3936, 2248,
  282, 3922,  592, 3922,  310, 3936, 3922, 3922, 3922, 3922,
 3922, 3922, 3922, 3922, 3922, 3922, 3922, 3922, 1980, 1980,
 2198,  593,    0,  595,    0,  601,  321,  321, 3922,  321,
 3922, 3496,  262,    0, 3936,  318,  323,  350,  262,  205,
  387,  205,    0,  389,    0,  298,    0,    0,    0,    0,
 4980,  -93,    0, 2625,  262,  326,  557, 2198,  605, 2198,
 2198,  607, 2198, 2198,  608, 2198, 2198,  611, 2198, 2198,
 2198, 2198, 2198,  612,  613, 2198,  615, 2198, 2198, 2198,
 2198,    0,  616,  619,  421,  632, 3922, 1980,  639, 3922,
  640, 3936,  641,  173,  173,  173,  173,  173,  173,  173,
  173,  173,  173,  173,  173,  644,  648,  649,  603, 3936,
 3666,   90,  601,  601,  321,  601,  321,  321, 3922,  651,
    0,    0,    0,  262,  205,  262,  205,    0, 2714, 4980,
    0,  652, 3922,    0, 2198,    0,    0, 2198,    0,    0,
 2198,    0,    0, 2198,    0,    0,    0,    0,    0, 2198,
 2198,    0, 2198,    0,    0,    0,    0, 3936, 3936,    0,
  332,  656,    0,    0,  333,  664,  339,  667, 3936, 2198,
 2198, 2198,    0,  668, 3892,    0, 1891,  299,    0,   90,
   90,   90,  601,   90,  601,  601,  321, 3936,  262,  262,
    0, 2803,  326,  672,  653,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  440,  351,  460,  352,  472,
 3936,    0,  680,    0,    0,    0,  637, 3936,  688, 2006,
 2139,    0,    0, 3907,   90,   90,    0,   90,   90,    0,
  601,  303,    0,    0,  326,    0,    0,    0,  474,    0,
  476,    0,  680, 3936,    0,    0, 2501,    0,    0,  360,
  700,  701,  702,    0,    0,    0,    0,   90,    0,    0,
    0,    0,    0,    0,    0,  304,  703, 3936, 3936, 3936,
    0, 3922,  366,  369,  373,  357, 3922, 3922, 3922, 3936,
  363,  368,  374,  711,    0,    0,    0, 3936,  305,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  757,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  578, 3541,  486,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  105,    0,    0,    0,    0,    0,
    0, 3555,    0,    0,  487,  490,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  707,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  179,    0,
    0,  494,    0,    0,    0,    0,    0,    0,    0,    0,
  277,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  707,    0,   39,    0,
    0,    0,    0,    0,    0,    0,    0,  658,    0,    0,
    0,    0,  707,    0,    0,    0,  158,  707,    0,  707,
    0,    0,    0,    0,    0,   62,    0,  733,  822,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  329,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  707,    0,
    0,  707,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   94,    0,  125,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 2892,    0, 5058,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  216,    0,    0,    0,    0,    0,    8,    0,  707,    0,
    0,  330,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  302,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  707,    0,    0,  707,  707,    0,  707,  707,
    0,  707,  707,    0,  707,  707,  707,  707,  707,    0,
    0,  707,    0,  707,  707,  707,  707,    0,    0,    0,
  707,    0,  707,    0,    0,    0,    0,    0,  707,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  707,
  707,    0,    0,    0,    0,    0,    0, 2981,    0, 3070,
 5136,    0,    0,  707,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  446,    0,
    0,    0,    0,    0,    0,    0,    0, 5214,    0,    0,
    0,    0,  707,    0,    0,    0,  707,    0,    0,  707,
    0,    0,  707,    0,    0,    0,    0,    0,  707,  707,
    0,  707,    0,    0,    0,    0,    0,    0,    0,    0,
  707,    0,    0,    0,  707,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  707,  707,
    0, 3923,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3159,    0,    0,  707,  707,  707,  481,    0,
    0,  542,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5292,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  707,    0,    0,
    0,    0,    0,  747,  842,  931, 1020, 1109, 1198, 1287,
 1376, 1465, 1554, 1643, 1732,    0,    0,    0,    0,    0,
    0, 4012,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  627,  635,  879, 1777,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  707,    0,    0, 4101,
    0, 4190,    0, 4279,    0,    0,    0,    0, 1893, 2147,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4368,    0,    0,    0,    0,    0,  336,  707,
    0,    0,    0,    0, 4457, 4546,    0, 4635, 4724,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4813,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4902,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  707,    0,    0,    0,    0,
  707,  707,  707,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  758,  692,    0,    0,    0,    0,  596,  598,   72,
   -6,  -60, -172,   61,    0,  753,  538,    0, -228, -468,
    0,  306,    0, -611,    0,  -63,  571,  710,   11,    0,
  585,    0,   18,  -59, -242,    0,   -2,    0,  735,  -52,
  -57,    0,  584, -124,    0,    0,    0,  285, -695,  254,
    0, -464, -477,   26, -292,    0,  488,  497,  442, -515,
 -137,    0,   69,    0,  337,    0,  190,    0,  106, -111,
 -181,    0,
  };
  protected static readonly short [] yyTable = {            53,
   53,   93,   99,   56,  341,  118,  347,  566,   89,   82,
  171,   94,  752,  216,  172,  320,   55,   61,  362,   94,
  457,  218,   94,   94,  363,  487,  577,  358,  571,  650,
   91,  633,  634,  122,  636,  763,   16,   91,   29,  126,
  262,  714,   85,  367,  122,  326,  611,  165,   51,   53,
   53,   51,  261,   53,   97,  185,   53,   19,  242,  242,
  102,  121,  368,   20,  114,  326,   95,   96,  108,  457,
   98,  218,   91,  101,  520,  196,  191,  463,   84,  192,
   21,  249,   85,  210,   64,  213,  541,  129,  218,   66,
   53,  364,   53,  122,  328,  214,  102,  141,  223,  223,
  223,   67,  167,  714,  169,  173,  228,  132,  231,  134,
  477,  652,  809,  185,  121,  187,  188,  135,  190,  703,
  486,  705,  706,  326,  123,  189,  218,   49,  218,  682,
  482,   51,  686,  251,   91,  253,   68,  110,  112,  241,
  350,  327,   88,  460,   84,  461,  325,  109,   82,  111,
  193,   70,   71,  197,  247,   69,  322,  323,  324,   91,
  378,  229,  381,  232,  214,  145,  319,   79,  331,  390,
  457,  335,   91,   83,  522,  319,  319,  144,  353,  319,
  319,  168,   90,  170,  211,  129,   35,  212,  483,   17,
   18,  751,  494,  457,  355,  195,  494,   84,   91,  494,
  526,   83,  366,  214,  370,  141,  144,  102,  494,   47,
   48,  252,   70,   71,   91,   21,   72,   73,   84,  100,
   72,   73,  100,  398,  399,  400,  339,   94,  405,   82,
  218,  410,  411,  412,  413,  414,  415,  416,  417,  418,
  419,  420,  421,  100,  574,  144,  117,  358,  754,  341,
   80,  133,  358,   81,  136,  194,  103,  254,  454,   70,
   71,  215,  468,  373,  376,  377,  379,  380,  382,  383,
  385,  386,  387,  388,  389,  392,  394,  395,  396,  397,
  104,  457,   83,  401,  403,  553,  106,  409,  240,  240,
  781,  113,  236,  107,  479,  237,  559,   91,  786,  555,
  260,  124,  450,  451,   53,  116,   29,   29,  453,  119,
   29,  572,  354,   29,  464,  575,   84,  130,  123,  349,
  130,  452,  237,   91,  120,  607,   29,   29,  649,  121,
  121,  125,  480,  121,  121,  481,  121,  137,  648,  743,
  523,  481,  744,  780,  791,  810,  764,  481,  764,  121,
  121,   91,   35,  612,  218,   29,  457,  257,  641,   91,
   94,  122,  122,  642,   91,  122,  122,  493,  122,  131,
   46,  497,  131,   46,  500,   89,  232,  503,  121,  232,
  163,  122,  122,  509,  510,  176,  512,  644,  186,  646,
  643,   91,  123,  123,  177,  521,  123,  123,   91,  123,
  800,  525,  552,  805,   91,  144,   50,  712,  806,   91,
  122,  164,  123,  123,  807,   91,  225,  226,  570,  457,
   36,   37,  220,   38,   39,  539,  219,   41,   42,   43,
   44,   45,   46,  221,  222,  227,  233,   51,  234,  235,
  255,  123,  540,  162,  256,  125,   53,   53,   53,  248,
  263,  549,  551,  602,  603,  604,  606,  264,  556,  557,
  558,  321,  613,  547,  548,  550,  329,  246,  330,   49,
  332,  339,  709,  333,  710,  346,  356,  361,  214,  365,
   18,  455,  406,   21,   21,  462,  465,   21,   21,  466,
   21,  640,  198,  467,  469,  701,  702,  470,  704,  471,
  472,  475,  473,   21,   21,  474,  476,  478,  199,  489,
  561,  774,  491,  517,  518,  608,  519,  608,  524,  527,
  614,  615,  616,  617,  618,  619,  620,  621,  622,  623,
  624,  625,   21,  528,  529,  319,  560,  562,  358,  530,
  531,   26,   53,  532,   53,   53,  200,  201,  688,  639,
  202,  203,  204,  205,  206,  207,  533,  563,  534,  635,
  535,  637,  638,  536,  537,  747,  694,  749,  750,  124,
  124,  538,  700,  124,  124,  218,  124,  148,  149,  150,
  564,  151,  152,  153,   91,  154,  230,  576,  578,  124,
  124,  367,  243,  155,  580,  581,  583,  584,  586,  156,
  608,  587,  163,  608,  589,  590,  157,  591,  592,  593,
   22,  596,  598,  779,  724,  725,  599,   84,  124,   23,
  218,  218,  600,  218,  697,  601,   19,   24,   25,   26,
   27,   28,   53,  164,   20,  610,  629,   84,  630,  741,
  631,  745,  746,  645,  748,  647,  339,  653,  655,  707,
  658,  661,  490,  218,  664,  670,  671,  492,  673,  678,
  495,  496,  679,  498,  499,  162,  501,  502,   84,  504,
  505,  506,  507,  508,  766,  681,  511,  680,  513,  514,
  515,  516,  685,  687,  689,  371,  372,  690,  740,  778,
  158,  691,  692,  541,  708,  713,  758,   85,  203,  727,
   84,  203,  726,  728,  159,  160,  161,  729,  339,  730,
  731,  738,   50,  125,  125,  755,  760,  125,  125,  203,
  125,  759,  761,  764,  793,  794,  795,  767,  762,  765,
  782,  787,  783,  125,  125,  544,  804,  697,  102,  788,
  789,  790,  792,   51,  797,  756,   84,  798,   18,   18,
  203,  799,   18,   18,  808,   18,    1,  109,  110,    1,
    2,  111,  125,    3,    4,  112,    5,   65,   18,   18,
  115,   78,  239,  238,  348,   49,  250,  579,  258,    6,
    7,  582,  203,  568,  585,  796,   84,  588,   92,  784,
  801,  802,  803,  594,  595,  259,  597,   18,  124,  148,
  149,  150,  458,  151,  152,  153,  488,  154,    8,   26,
   26,  459,  777,   26,   26,  155,   26,  569,  693,    0,
  757,  156,    0,  626,  627,  628,    0,    0,  157,   26,
   26,    0,    0,    0,   84,   84,   84,    0,   84,   84,
   84,    0,   84,    0,    0,    0,    0,    0,    0,   84,
   84,    0,    0,    0,    0,    0,   84,   75,   26,    0,
    0,    0,  654,   84,  656,  657,    0,  659,  660,    0,
  662,  663,    0,  665,  666,  667,  668,  669,   27,    0,
  672,   84,  674,  675,  676,  677,    0,    0,    0,    0,
    0,    0,  683,    0,   19,   19,    0,    0,   19,   19,
    0,   19,   20,   20,    0,    0,   20,   20,    0,   20,
    0,    0,  158,    0,   19,   19,   22,    0,    0,    0,
    0,    0,   20,   20,    0,   23,  159,  160,  161,    0,
  203,  203,    0,   24,   25,   26,   27,   28,    0,  717,
    0,    0,  718,   19,    0,  719,   76,   84,  720,    0,
    0,   20,    0,    0,  721,  722,    0,  723,    0,    0,
    0,   84,   84,   84,    0,    0,    0,    0,    0,    0,
   84,    0,    0,    0,  735,  736,  737,    0,    0,    0,
    0,  742,    0,    0,  203,  203,  203,    0,    0,    0,
    0,    0,   75,  203,    0,  203,    0,    0,  203,  203,
  203,  203,  203,  203,  203,  203,  203,  203,   75,  203,
  203,    0,  203,  203,  203,  203,  203,  203,  203,  320,
  320,  203,  203,  203,  203,  776,   91,  203,    0,    0,
    0,  203,  203,  203,  203,  203,  203,  203,  203,  203,
  203,  203,  203,  203,    0,  203,   75,   75,    0,    0,
   75,   75,   75,   75,   75,   75,  203,    0,    0,   84,
    0,    0,    0,    0,    0,    0,    0,  203,  203,  203,
  203,    0,    0,  320,  320,  320,    0,    0,    0,    0,
    0,   76,  320,    0,  320,    0,    0,  320,  320,  320,
  320,  320,  320,  320,  320,  320,  320,   76,  320,  320,
    0,  320,  320,  320,  320,  320,  320,  320,    0,    0,
  320,  320,  320,  320,  325,  325,  320,    0,    0,    0,
  320,  320,  320,  320,  320,    0,  320,  320,  320,  320,
  320,  320,  320,    0,  320,   76,   76,    0,    0,   76,
   76,   76,   76,   76,   76,  320,   27,   27,   84,    0,
   27,   27,    0,   27,    0,    0,  320,  320,  320,  320,
    0,    0,    0,    0,    0,    0,   27,   27,  325,  325,
  325,    0,    0,    0,    0,    0,    0,  325,    0,  325,
    0,    0,  325,  325,  325,  325,  325,  325,  325,  325,
  325,  325,    0,  325,  325,   27,  325,  325,  325,  325,
  325,  325,  325,  310,  310,  325,  325,  325,  325,    0,
    0,  325,    0,    0,    0,  325,  325,  325,  325,  325,
    0,  325,  325,  325,  325,  325,  325,  325,    0,  325,
    0,    0,    0,    0,    0,    0,    0,   84,    0,    0,
  325,  148,  149,  150,    0,  151,  152,  153,    0,  154,
    0,  325,  325,  325,  325,    0,    0,  310,  310,  310,
    0,    0,    0,  156,    0,    0,  310,    0,  310,    0,
  157,  310,  310,  310,  310,  310,  310,  310,  310,  310,
  310,    0,  310,  310,    0,  310,  310,  310,  310,  310,
  310,  310,  291,  291,  310,  310,  310,  310,    0,    0,
  310,    0,    0,    0,  310,  310,  310,  310,  310,    0,
  310,  310,  310,  310,  310,  310,  310,    0,  310,    0,
    0,    0,    0,    0,    0,    0,   84,    0,    0,  310,
  148,  149,  150,    0,  151,  152,  153,    0,  154,    0,
  310,  310,  310,  310,    0,    0,  291,  291,  291,  357,
    0,    0,  156,    0,    0,  291,    0,  291,    0,  157,
  291,  291,  291,  291,  291,  291,  291,  291,  291,  291,
    0,  291,  291,    0,  291,  291,  291,  291,  291,  291,
  291,  288,  288,  291,  291,  291,  291,    0,    0,  291,
    0,    0,    0,  291,  291,  291,  291,  291,    0,  291,
  291,  291,  291,  291,  291,  291,    0,  291,    0,    0,
    0,    0,    0,    0,    0,   84,    0,    0,  291,  148,
  149,  150,    0,  151,  152,  153,    0,  154,    0,  291,
  291,  291,  291,    0,    0,  288,  288,  288,    0,    0,
    0,  156,    0,    0,  288,    0,  288,    0,  157,  288,
  288,  288,  288,  288,  288,  288,  288,  288,  288,    0,
  288,  288,    0,  288,  288,  288,  288,  288,  288,  288,
  289,  289,  288,  288,  288,  288,    0,    0,  288,    0,
    0,    0,  288,  288,  288,  288,  288,    0,  288,  288,
  288,  288,  288,  288,  288,    0,  288,    0,    0,    0,
    0,    0,    0,    0,   84,   70,   71,  288,    0,   72,
   73,   74,   30,   31,   32,   33,   34,    0,  288,  288,
  288,  288,    0,   40,  289,  289,  289,    0,    0,    0,
    0,    0,    0,  289,    0,  289,    0,    0,  289,  289,
  289,  289,  289,  289,  289,  289,  289,  289,    0,  289,
  289,    0,  289,  289,  289,  289,  289,  289,  289,  290,
  290,  289,  289,  289,  289,    0,    0,  289,    0,    0,
    0,  289,  289,  289,  289,  289,    0,  289,  289,  289,
  289,  289,  289,  289,    0,  289,    0,    0,    0,    0,
    0,   36,   37,   84,   38,   39,  289,    0,   41,   42,
   43,   44,   45,   46,    0,    0,    0,  289,  289,  289,
  289,    0,    0,  290,  290,  290,    0,    0,    0,    0,
    0,    0,  290,    0,  290,    0,    0,  290,  290,  290,
  290,  290,  290,  290,  290,  290,  290,    0,  290,  290,
    0,  290,  290,  290,  290,  290,  290,  290,  322,  322,
  290,  290,  290,  290,    0,    0,  290,    0,    0,    0,
  290,  290,  290,  290,  290,    0,  290,  290,  290,  290,
  290,  290,  290,    0,  290,    0,    0,    0,    0,    0,
    0,    0,   84,    0,    0,  290,  422,  423,  424,  425,
  426,  427,  428,  429,  430,  431,  290,  290,  290,  290,
    0,    0,  322,  322,  322,    0,    0,    0,    0,    0,
    0,  322,    0,  322,    0,    0,  322,  322,  322,  322,
  322,  322,  322,  322,  322,  322,    0,  322,  322,    0,
  322,  322,  322,  322,  322,  322,  322,  314,  314,  322,
  322,  322,  322,    0,    0,  322,    0,    0,    0,  322,
  322,  322,  322,  322,    0,  322,  322,  322,  322,  322,
  322,  322,    0,  322,    0,    0,    0,    0,    0,    0,
    0,   84,    0,    0,  322,    0,   24,    0,    0,    0,
    0,    0,    0,    0,    0,  322,  322,  322,  322,    0,
    0,  314,  314,  314,    0,    0,    0,    0,    0,  198,
  314,    0,  314,    0,    0,  314,  314,  314,  314,  314,
  314,  314,  314,  314,  314,  199,  314,  314,    0,  314,
  314,  314,  314,  314,  314,  314,  306,  306,  314,  314,
  314,  314,    0,    0,  314,    0,    0,    0,  314,  314,
  314,  314,  314,    0,  314,  314,  314,  314,  314,  314,
  314,    0,  314,  200,  201,    0,    0,  202,  203,  204,
  205,  206,  207,  314,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  314,  314,  314,  314,    0,    0,
  306,  306,  306,    0,    0,    0,    0,    0,    0,  306,
    0,  306,   22,    0,  306,  306,  306,  306,  306,  306,
  306,  306,  306,  306,    0,  306,  306,    0,  306,  306,
  306,  306,  306,  306,  306,  297,  297,  306,  306,  306,
  306,    0,    0,  306,    0,    0,    0,  306,  306,  306,
  306,  306,   91,  306,  306,  306,  306,  306,  306,  306,
    0,  306,    0,    0,    0,    0,    0,    0,    0,    0,
  163,    0,  306,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  306,  306,  306,  306,    0,    0,  297,
  297,  297,    0,    0,    0,    0,    0,    0,  297,    0,
  297,  164,    0,  297,  297,  297,  297,  297,  297,  297,
  297,  297,  297,    0,  297,  297,    0,  297,  297,  297,
  297,  297,  297,  297,  269,  269,  297,  297,  297,  297,
  565,    0,  297,  162,    0,    0,  297,  297,  297,  297,
  297,   91,  297,  297,  297,  297,  297,  297,  297,    0,
  297,    0,    0,    0,    0,    0,    0,    0,    0,  163,
    0,  297,    0,    0,   24,   24,    0,   91,   24,   24,
    0,   24,  297,  297,  297,  297,    0,    0,  269,  269,
  269,    0,    0,    0,   24,   24,    0,  269,    0,  269,
  164,    0,  269,  269,  269,  269,  269,  269,  269,  269,
  269,  269,    0,  269,  269,   91,  269,  269,  269,  269,
  269,  269,  269,   24,    0,  269,  269,  269,  269,    0,
    0,  269,  162,  163,    0,  269,  269,  269,  269,  269,
    0,  269,  269,  269,  269,  269,  269,  269,    0,  269,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  269,    0,    0,    0,  164,    0,   91,    0,  334,    0,
    0,  269,  269,  269,  269,    0,   25,  148,  149,  150,
    0,  151,  152,  153,  163,  154,    0,    0,    0,    0,
   22,   22,  243,  155,   22,   22,  162,   22,    0,  156,
    0,    0,    0,    0,    0,    0,  157,    0,    0,    0,
   22,   22,    0,    0,    0,  164,    0,    0,    0,    0,
    0,    0,    0,   36,   37,    0,   38,   39,  163,    0,
   41,   42,   43,   44,   45,   46,    0,    0,    0,   22,
    0,    0,    0,    0,    0,    0,    0,  162,    0,    0,
    0,    0,    0,    0,    0,    0,  148,  149,  150,  164,
  151,  152,  153,    0,  154,    0,  148,  149,  150,    0,
  151,  152,  153,    0,  154,  357,    0,    0,  156,    0,
    0,  243,  155,    0,    0,  157,    0,  163,  156,    0,
  158,  162,  148,  149,  150,  157,  151,  152,  153,    0,
  154,    0,    0,    0,  159,  160,  161,  768,  769,    0,
   50,    0,    0,    0,  156,    0,    0,    0,  164,    0,
    0,  157,    0,    0,    0,    0,    0,    0,    0,    0,
  148,  149,  150,    0,  151,  152,  153,   50,  154,    0,
    0,   51,    0,   50,    0,    0,  155,    0,    0,    0,
  162,    0,  156,    0,    0,    0,    0,    0,    0,  157,
    0,    0,    0,    0,    0,   50,    0,    0,   51,    0,
    0,   50,    0,   49,   51,    0,    0,    0,    0,  158,
    0,  148,  149,  150,    0,  151,  152,  153,    0,  154,
    0,    0,    0,  159,  160,  161,   51,  155,    0,    0,
   49,    0,   51,  156,    0,  770,   49,    0,    0,    0,
  157,    0,    0,    0,    0,    0,    0,    0,    0,  771,
  772,  773,  456,    0,    0,  148,  149,  150,   49,  151,
  152,  153,    0,  154,   49,    0,    0,    0,    0,    0,
  243,  155,    0,  158,   25,   25,    0,  156,   25,   25,
    0,   25,    0,    0,  157,    0,    0,  159,  160,  161,
    0,    0,    0,    0,   25,   25,    0,    0,    0,    0,
    0,   36,   37,    0,   38,   39,    0,    0,   41,   42,
   43,   44,   45,   46,  148,  149,  150,    0,  151,  152,
  153,    0,  154,   25,  158,    0,    0,    0,    0,  243,
  155,    0,    0,    0,    0,    0,  156,    0,  159,  160,
  161,    0,  485,  157,   22,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,  158,    0,
    0,  178,    0,    0,    0,    0,    0,   22,    0,    0,
   23,    0,  159,  160,  161,    0,   23,    0,   24,   25,
   26,   27,   28,    0,   24,   25,   26,   27,   28,   22,
  265,  785,    0,    0,    0,   22,    0,    0,   23,    0,
    0,    0,    0,    0,   23,    0,   24,   25,   26,   27,
   28,    0,   24,   25,   26,   27,   28,  158,    0,    0,
    0,  554,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  159,  160,  161,    0,    0,    0,    0,    0,    0,
    0,    0,  408,    0,  266,  267,  268,    0,    0,    0,
    0,    0,    0,  269,    0,  270,    0,  384,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,  605,  281,
  282,    0,  283,  284,  285,  286,  287,  288,  289,  391,
  265,  290,  291,  292,  293,  393,    0,  294,    0,    0,
    0,  295,  296,  297,  298,  299,    0,  300,  301,  302,
  303,  304,  305,  306,    0,  307,    0,    0,    0,    0,
  573,    0,    0,    0,    0,    0,  308,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,  310,  311,
  312,    0,    0,    0,  266,  267,  268,    0,    0,    0,
    0,    0,    0,  269,    0,  270,    0,    0,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,    0,  281,
  282,    0,  283,  284,  285,  286,  287,  288,  289,  265,
    0,  290,  291,  292,  293,    0,    0,  294,    0,    0,
    0,  295,  296,  297,  298,  299,    0,  300,  301,  302,
  303,  304,  305,  306,    0,  307,    0,    0,    0,  651,
    0,    0,    0,    0,    0,    0,  308,  148,  149,  150,
    0,  151,  152,  153,    0,  154,    0,  309,  310,  311,
  312,    0,    0,  266,  267,  268,  357,    0,    0,  156,
    0,    0,  269,    0,  270,    0,  157,  271,  272,  273,
  274,  275,  276,  277,  278,  279,  280,    0,  281,  282,
    0,  283,  284,  285,  286,  287,  288,  289,  265,    0,
  290,  291,  292,  293,    0,    0,  294,    0,    0,    0,
  295,  296,  297,  298,  299,    0,  300,  301,  302,  303,
  304,  305,  306,    0,  307,    0,    0,    0,  711,    0,
    0,    0,    0,    0,    0,  308,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,  310,  311,  312,
    0,    0,  266,  267,  268,    0,    0,    0,    0,    0,
    0,  269,    0,  270,    0,    0,  271,  272,  273,  274,
  275,  276,  277,  278,  279,  280,    0,  281,  282,    0,
  283,  284,  285,  286,  287,  288,  289,  265,    0,  290,
  291,  292,  293,    0,    0,  294,    0,    0,    0,  295,
  296,  297,  298,  299,    0,  300,  301,  302,  303,  304,
  305,  306,    0,  307,    0,    0,    0,  753,    0,    0,
    0,    0,    0,    0,  308,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  309,  310,  311,  312,    0,
    0,  266,  267,  268,    0,    0,    0,    0,    0,    0,
  269,    0,  270,    0,    0,  271,  272,  273,  274,  275,
  276,  277,  278,  279,  280,    0,  281,  282,    0,  283,
  284,  285,  286,  287,  288,  289,  265,    0,  290,  291,
  292,  293,    0,    0,  294,    0,    0,    0,  295,  296,
  297,  298,  299,    0,  300,  301,  302,  303,  304,  305,
  306,    0,  307,    0,    0,    0,  215,    0,    0,    0,
    0,    0,    0,  308,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  309,  310,  311,  312,    0,    0,
  266,  267,  268,    0,    0,    0,    0,    0,    0,  269,
    0,  270,    0,    0,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,    0,  281,  282,    0,  283,  284,
  285,  286,  287,  288,  289,  265,    0,  290,  291,  292,
  293,    0,    0,  294,    0,    0,    0,  295,  296,  297,
  298,  299,    0,  300,  301,  302,  303,  304,  305,  306,
    0,  307,    0,    0,    0,  213,    0,    0,    0,    0,
    0,    0,  308,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  309,  310,  311,  312,    0,    0,  266,
  267,  268,    0,    0,    0,    0,    0,    0,  269,    0,
  270,    0,    0,  271,  272,  273,  274,  275,  276,  277,
  278,  279,  280,    0,  281,  282,    0,  283,  284,  285,
  286,  287,  288,  289,  215,    0,  290,  291,  292,  293,
    0,    0,  294,    0,    0,    0,  295,  296,  297,  298,
  299,    0,  300,  301,  302,  303,  304,  305,  306,    0,
  307,    0,    0,    0,  216,    0,    0,    0,    0,    0,
    0,  308,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  309,  310,  311,  312,    0,    0,  215,  215,
  215,    0,    0,    0,    0,    0,    0,  215,    0,  215,
    0,    0,  215,  215,  215,  215,  215,  215,  215,  215,
  215,  215,    0,  215,  215,    0,  215,  215,  215,  215,
  215,  215,  215,  213,    0,  215,  215,  215,  215,    0,
    0,  215,    0,    0,    0,  215,  215,  215,  215,  215,
    0,  215,  215,  215,  215,  215,  215,  215,    0,  215,
    0,    0,    0,  214,    0,    0,    0,    0,    0,    0,
  215,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  215,  215,  215,  215,    0,    0,  213,  213,  213,
    0,    0,    0,    0,    0,    0,  213,    0,  213,    0,
    0,  213,  213,  213,  213,  213,  213,  213,  213,  213,
  213,    0,  213,  213,    0,  213,  213,  213,  213,  213,
  213,  213,  216,    0,  213,  213,  213,  213,    0,    0,
  213,    0,    0,    0,  213,  213,  213,  213,  213,    0,
  213,  213,  213,  213,  213,  213,  213,    0,  213,   50,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  213,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  213,  213,  213,  213,    0,    0,  216,  216,  216,    0,
   51,    0,    0,    0,    0,  216,    0,  216,    0,    0,
  216,  216,  216,  216,  216,  216,  216,  216,  216,  216,
    0,  216,  216,    0,  216,  216,  216,  216,  216,  216,
  216,  214,   49,  216,  216,  216,  216,    0,    0,  216,
    0,    0,    0,  216,  216,  216,  216,  216,    0,  216,
  216,  216,  216,  216,  216,  216,    0,  216,   50,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  216,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  216,
  216,  216,  216,    0,    0,  214,  214,  214,    0,   51,
    0,    0,    0,    0,  214,    0,  214,    0,    0,  214,
  214,  214,  214,  214,  214,  214,  214,  214,  214,    0,
  214,  214,    0,  214,  214,  214,  214,  214,  214,  214,
   50,   49,  214,  214,  214,  214,    0,    0,  214,    0,
    0,    0,  214,  214,  214,  214,  214,    0,  214,  214,
  214,  214,  214,  214,  214,    0,  214,    0,    0,    0,
    0,   51,    0,    0,    0,   50,    0,  214,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  214,  214,
  214,  214,    0,   22,    0,    0,    0,    0,    0,    0,
    0,    0,   23,   49,    0,    0,   51,    0,    0,    0,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
  116,   29,    0,    0,    0,    0,   30,   31,   32,   33,
   34,   35,   36,   37,  117,   38,   39,   40,   49,   41,
   42,   43,   44,   45,   46,    0,    0,    0,    0,    0,
    0,  116,    0,    0,   47,   48,    0,    0,    0,  140,
    0,    0,    0,    0,    0,  117,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   50,    0,
    0,    0,   22,  116,    0,  128,    0,    0,    0,    0,
    0,   23,    0,    0,    0,    0,    0,  117,    0,   24,
   25,   26,   27,   28,   50,    0,    0,    0,    0,   51,
   60,   50,    0,    0,    0,    0,    0,   50,    0,    0,
    0,   36,   37,    0,   38,   39,  696,    0,   41,   42,
   43,   44,   45,   46,    0,   51,    0,    0,    0,    0,
    0,   49,   51,    0,   22,   50,    0,    0,   51,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,   49,    0,    0,
    0,   50,    0,    0,   49,    0,   51,    0,    0,   22,
   49,    0,   35,   36,   37,    0,   38,   39,   23,    0,
   41,   42,   43,   44,   45,   46,   24,   25,   26,   27,
   28,    0,   51,    0,    0,   50,    0,    0,   49,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   36,   37,
    0,   38,   39,    0,  116,   41,   42,   43,   44,   45,
   46,   50,    0,  116,   49,    0,   51,    0,  117,    0,
    0,  116,  116,  116,  116,  116,    0,  117,    0,    0,
    0,    0,    0,    0,    0,  117,  117,  117,  117,  117,
    0,    0,   51,  116,  116,    0,  116,  116,   49,    0,
  116,  116,  116,  116,  116,  116,    0,  117,  117,    0,
  117,  117,   22,   50,  117,  117,  117,  117,  117,  117,
    0,   23,    0,    0,   49,    0,  138,    0,    0,   24,
   25,   26,   27,   28,    0,    0,    0,    0,   22,    0,
    0,  139,    0,   50,   51,   22,    0,   23,    0,    0,
   50,  178,    0,    0,   23,   24,   25,   26,   27,   28,
   23,    0,   24,   25,   26,   27,   28,  127,   24,   25,
   26,   27,   28,   50,   51,    0,   49,    0,   50,   22,
    0,   51,    0,    0,    0,    0,  402,    0,   23,    0,
    0,    0,  404,  695,    0,    0,   24,   25,   26,   27,
   28,   50,    0,    0,   51,   22,   49,    0,    0,   51,
    0,    0,    0,   49,   23,   84,   50,    0,    0,  138,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,   50,   51,    0,  139,    0,   49,    0,  181,   22,
    0,   49,    0,    0,    0,   50,    0,   51,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,   51,    0,   49,  178,  179,    0,  127,    0,
    0,    0,    0,    0,   23,  180,   51,    0,    0,   49,
    0,    0,   24,   25,   26,   27,   28,    0,    0,    0,
    0,    0,    0,    0,   49,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   49,    0,
  148,  149,  150,  336,  151,  152,  153,   22,  154,    0,
    0,    0,    0,    0,    0,    0,   23,  337,    0,  338,
    0,    0,  156,    0,   24,   25,   26,   27,   28,  157,
  148,  149,  150,  336,  151,  152,  153,   22,  154,  433,
  434,    0,    0,    0,   22,    0,   23,  567,    0,  338,
    0,    0,  156,   23,   24,   25,   26,   27,   28,  157,
    0,   24,   25,   26,   27,   28,    0,  178,  179,    0,
    0,    0,  178,  351,    0,    0,   23,  180,    0,    0,
    0,   23,  352,    0,   24,   25,   26,   27,   28,   24,
   25,   26,   27,   28,    0,   22,    0,    0,    0,    0,
    0,    0,    0,    0,   23,  739,    0,    0,    0,    0,
   22,    0,   24,   25,   26,   27,   28,    0,    0,   23,
    0,    0,    0,    0,  695,   22,    0,   24,   25,   26,
   27,   28,    0,    0,   23,  305,  305,    0,    0,  178,
    0,    0,   24,   25,   26,   27,   28,    0,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,    0,    0,  435,
  436,  437,  438,    0,    0,    0,    0,    0,  439,  440,
  441,  442,  443,  444,  445,  446,  447,  448,    0,  305,
  305,  305,    0,    0,    0,    0,    0,    0,  305,    0,
  305,    0,    0,  305,  305,  305,  305,  305,  305,  305,
  305,  305,  305,    0,  305,  305,    0,  305,  305,  305,
  305,  305,  305,  305,  270,  270,  305,  305,  305,  305,
    0,    0,  305,    0,    0,    0,  305,  305,  305,  305,
  305,    0,  305,  305,  305,  305,  305,  305,  305,    0,
  305,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  305,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  305,  305,  305,  305,    0,    0,  270,  270,
  270,    0,    0,    0,    0,    0,    0,  270,    0,  270,
    0,    0,  270,  270,  270,  270,  270,  270,  270,  270,
  270,  270,    0,  270,  270,    0,  270,  270,  270,  270,
  270,  270,  270,  274,  274,  270,  270,  270,  270,    0,
    0,  270,    0,    0,    0,  270,  270,  270,  270,  270,
    0,  270,  270,  270,  270,  270,  270,  270,    0,  270,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  270,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  270,  270,  270,  270,    0,    0,  274,  274,  274,
    0,    0,    0,    0,    0,    0,  274,    0,  274,    0,
    0,  274,  274,  274,  274,  274,  274,  274,  274,  274,
  274,    0,  274,  274,    0,  274,  274,  274,  274,  274,
  274,  274,  271,  271,  274,  274,  274,  274,    0,    0,
  274,    0,    0,    0,  274,  274,  274,  274,  274,    0,
  274,  274,  274,  274,  274,  274,  274,    0,  274,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  274,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  274,  274,  274,  274,    0,    0,  271,  271,  271,    0,
    0,    0,    0,    0,    0,  271,    0,  271,    0,    0,
  271,  271,  271,  271,  271,  271,  271,  271,  271,  271,
    0,  271,  271,    0,  271,  271,  271,  271,  271,  271,
  271,  279,  279,  271,  271,  271,  271,    0,    0,  271,
    0,    0,    0,  271,  271,  271,  271,  271,    0,  271,
  271,  271,  271,  271,  271,  271,    0,  271,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  271,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  271,
  271,  271,  271,    0,    0,  279,  279,  279,    0,    0,
    0,    0,    0,    0,  279,    0,  279,    0,    0,  279,
  279,  279,  279,  279,  279,  279,  279,  279,  279,    0,
  279,  279,    0,  279,  279,  279,  279,  279,  279,  279,
  293,  293,  279,  279,  279,  279,    0,    0,  279,    0,
    0,    0,  279,  279,  279,  279,  279,    0,  279,  279,
  279,  279,  279,  279,  279,    0,  279,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  279,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  279,  279,
  279,  279,    0,    0,  293,  293,  293,    0,    0,    0,
    0,    0,    0,  293,    0,  293,    0,    0,  293,  293,
  293,  293,  293,  293,  293,  293,  293,  293,    0,  293,
  293,    0,  293,  293,  293,  293,  293,  293,  293,  275,
  275,  293,  293,  293,  293,    0,    0,  293,    0,    0,
    0,  293,  293,  293,  293,  293,    0,  293,  293,  293,
  293,  293,  293,  293,    0,  293,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  293,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  293,  293,  293,
  293,    0,    0,  275,  275,  275,    0,    0,    0,    0,
    0,    0,  275,    0,  275,    0,    0,  275,  275,  275,
  275,  275,  275,  275,  275,  275,  275,    0,  275,  275,
    0,  275,  275,  275,  275,  275,  275,  275,  272,  272,
  275,  275,  275,  275,    0,    0,  275,    0,    0,    0,
  275,  275,  275,  275,  275,    0,  275,  275,  275,  275,
  275,  275,  275,    0,  275,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  275,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  275,  275,  275,  275,
    0,    0,  272,  272,  272,    0,    0,    0,    0,    0,
    0,  272,    0,  272,    0,    0,  272,  272,  272,  272,
  272,  272,  272,  272,  272,  272,    0,  272,  272,    0,
  272,  272,  272,  272,  272,  272,  272,  276,  276,  272,
  272,  272,  272,    0,    0,  272,    0,    0,    0,  272,
  272,  272,  272,  272,    0,  272,  272,  272,  272,  272,
  272,  272,    0,  272,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  272,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  272,  272,  272,  272,    0,
    0,  276,  276,  276,    0,    0,    0,    0,    0,    0,
  276,    0,  276,    0,    0,  276,  276,  276,  276,  276,
  276,  276,  276,  276,  276,    0,  276,  276,    0,  276,
  276,  276,  276,  276,  276,  276,  277,  277,  276,  276,
  276,  276,    0,    0,  276,    0,    0,    0,  276,  276,
  276,  276,  276,    0,  276,  276,  276,  276,  276,  276,
  276,    0,  276,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  276,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  276,  276,  276,  276,    0,    0,
  277,  277,  277,    0,    0,    0,    0,    0,    0,  277,
    0,  277,    0,    0,  277,  277,  277,  277,  277,  277,
  277,  277,  277,  277,    0,  277,  277,    0,  277,  277,
  277,  277,  277,  277,  277,  294,  294,  277,  277,  277,
  277,    0,    0,  277,    0,    0,    0,  277,  277,  277,
  277,  277,    0,  277,  277,  277,  277,  277,  277,  277,
    0,  277,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  277,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  277,  277,  277,  277,    0,    0,  294,
  294,  294,    0,    0,    0,    0,    0,    0,  294,    0,
  294,    0,    0,  294,  294,  294,  294,  294,  294,  294,
  294,  294,  294,    0,  294,  294,    0,  294,  294,  294,
  294,  294,  294,  294,  278,  278,  294,  294,  294,  294,
    0,    0,  294,    0,    0,    0,  294,  294,  294,  294,
  294,    0,  294,  294,  294,  294,  294,  294,  294,    0,
  294,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  294,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  294,  294,  294,  294,    0,    0,  278,  278,
  278,    0,    0,    0,    0,    0,    0,  278,    0,  278,
    0,    0,  278,  278,  278,  278,  278,  278,  278,  278,
  278,  278,  265,  278,  278,    0,  278,  278,  278,  278,
  278,  278,  278,    0,    0,  278,  278,  278,  278,    0,
    0,  278,    0,    0,    0,  278,  278,  278,  278,  278,
    0,  278,  278,  278,  278,  278,  278,  278,    0,  278,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  278,    0,    0,    0,    0,    0,  266,  267,  268,    0,
    0,  278,  278,  278,  278,  269,    0,  270,    0,    0,
  271,  272,  273,  274,  275,  276,  277,  278,  279,  280,
  219,  281,  282,    0,  283,  284,  285,  286,  287,  288,
  289,    0,    0,  290,  291,  292,  293,    0,    0,  294,
    0,    0,    0,  295,  296,  297,  298,  299,    0,  300,
  301,  302,  303,  304,  305,  306,    0,  307,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  308,    0,
    0,    0,    0,    0,  219,  219,  219,    0,    0,  309,
  310,  311,  312,  219,    0,  219,    0,    0,  219,  219,
  219,  219,  219,  219,  219,  219,  219,  219,  220,  219,
  219,    0,  219,  219,  219,  219,  219,  219,  219,    0,
    0,  219,  219,  219,  219,    0,    0,  219,    0,    0,
    0,  219,  219,  219,  219,  219,    0,  219,  219,  219,
  219,  219,  219,  219,    0,  219,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  219,    0,    0,    0,
    0,    0,  220,  220,  220,    0,    0,  219,  219,  219,
  219,  220,    0,  220,    0,    0,  220,  220,  220,  220,
  220,  220,  220,  220,  220,  220,  221,  220,  220,    0,
  220,  220,  220,  220,  220,  220,  220,    0,    0,  220,
  220,  220,  220,    0,    0,  220,    0,    0,    0,  220,
  220,  220,  220,  220,    0,  220,  220,  220,  220,  220,
  220,  220,    0,  220,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  220,    0,    0,    0,    0,    0,
  221,  221,  221,    0,    0,  220,  220,  220,  220,  221,
    0,  221,    0,    0,  221,  221,  221,  221,  221,  221,
  221,  221,  221,  221,  222,  221,  221,    0,  221,  221,
  221,  221,  221,  221,  221,    0,    0,  221,  221,  221,
  221,    0,    0,  221,    0,    0,    0,  221,  221,  221,
  221,  221,    0,  221,  221,  221,  221,  221,  221,  221,
    0,  221,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  221,    0,    0,    0,    0,    0,  222,  222,
  222,    0,    0,  221,  221,  221,  221,  222,    0,  222,
    0,    0,  222,  222,  222,  222,  222,  222,  222,  222,
  222,  222,    0,  222,  222,    0,  222,  222,  222,  222,
  222,  222,  222,    0,    0,  222,  222,  222,  222,    0,
    0,  222,    0,    0,    0,  222,  222,  222,  222,  222,
    0,  222,  222,  222,  222,  222,  222,  222,    0,  222,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  222,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  222,  222,  222,  222,  270,    0,    0,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,    0,  281,
  282,    0,  283,  284,  285,  286,  287,  288,  289,    0,
    0,  290,  291,  292,  293,    0,    0,  294,    0,    0,
    0,  295,  296,  297,  298,  299,    0,  300,  301,  302,
  303,  304,  305,  306,    0,  307,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  308,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,  310,  311,
  312,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   54,   60,    6,  233,   33,  235,  476,  123,   33,
  276,   40,  708,  123,  280,  123,    6,    7,  261,   40,
  313,  146,   40,   40,  123,  123,  491,  256,  123,  123,
   42,  547,  548,   44,  550,  731,   61,   42,    0,   92,
  213,  653,   49,  266,   44,   44,  524,  108,   41,   56,
   57,   44,  123,   60,   57,  119,   63,   61,   44,   44,
   63,    0,  285,   61,   60,   44,   56,   57,   75,  362,
   60,  196,   42,   63,   44,  135,   41,  320,   40,   44,
   61,   93,   89,  141,  316,  145,   91,   94,  213,  272,
   97,  264,   99,    0,   93,  274,   99,  104,  162,  163,
  164,  260,  109,  715,  111,  371,  167,   97,  169,   99,
  339,  576,  808,  177,  125,  122,  123,  100,  125,  635,
  363,  637,  638,   44,    0,  125,  251,  123,  253,  607,
  309,  124,  610,  193,   42,  195,   61,   77,   78,  125,
  125,   62,  257,  316,   40,  318,  125,   76,   44,   78,
  133,  290,  291,  136,   62,   61,  220,  221,  222,   42,
  272,  168,  274,  170,  274,  105,  274,  280,  229,  281,
  463,  232,   42,   40,   44,  274,  274,  316,  242,  274,
  274,  110,  257,  112,   41,  192,  302,   44,  361,  270,
  271,  707,  374,  486,  254,  135,  378,   40,   42,  381,
   44,   44,  266,  274,  268,  212,  316,  210,  390,  325,
  326,  194,  290,  291,   42,    0,  294,  295,   40,   41,
  294,  295,   44,  287,  288,  289,  233,   40,  292,  125,
  355,  295,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  272,  487,  316,  274,  476,  713,  478,
  274,  272,  481,  277,  272,  272,   61,  197,  311,  290,
  291,  371,  326,  270,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
   40,  574,  125,  290,  291,  458,  260,  294,  274,  274,
  755,  287,   41,  260,  354,   44,  469,   42,  767,   44,
  371,    0,  309,  310,  311,   40,  268,  269,  311,  123,
  272,  484,  252,  275,  321,  488,   40,   41,  267,   41,
   44,  311,   44,   42,  257,   44,  288,  289,  571,  268,
  269,  267,   41,  272,  273,   44,  275,  123,   41,   41,
  404,   44,   44,   41,   41,   41,   44,   44,   44,  288,
  289,   42,  302,   44,  479,  317,  649,  125,   41,   42,
   40,  268,  269,   41,   42,  272,  273,  374,  275,   41,
   41,  378,   44,   44,  381,  123,   41,  384,  317,   44,
   60,  288,  289,  390,  391,   40,  393,  560,   41,  562,
   41,   42,  268,  269,  123,  402,  272,  273,   42,  275,
   44,  408,  455,   41,   42,  316,   60,  650,   41,   42,
  317,   91,  288,  289,   41,   42,  163,  164,  482,  712,
  303,  304,   40,  306,  307,  432,  372,  310,  311,  312,
  313,  314,  315,   40,   40,   44,   58,   91,   58,   58,
   61,  317,  449,  123,   40,    0,  453,  454,  455,   62,
  274,  454,  455,  517,  518,  519,  520,  257,  465,  466,
  467,   40,  526,  453,  454,  455,  371,  183,   44,  123,
  371,  478,  645,   44,  647,  274,  260,  257,  274,   61,
    0,  412,  324,  268,  269,  274,  379,  272,  273,  379,
  275,  555,  260,  379,  257,  633,  634,  371,  636,   44,
  257,   44,  371,  288,  289,  371,   40,  124,  276,  273,
  371,  740,   44,   44,   44,  522,   44,  524,   44,  379,
  527,  528,  529,  530,  531,  532,  533,  534,  535,  536,
  537,  538,  317,  379,  379,  274,  257,  257,  767,  379,
  379,    0,  549,  379,  551,  552,  314,  315,  612,  552,
  318,  319,  320,  321,  322,  323,  379,  257,  379,  549,
  379,  551,  552,  379,  379,  703,  630,  705,  706,  268,
  269,  379,  632,  272,  273,  700,  275,  257,  258,  259,
  371,  261,  262,  263,   42,  265,   44,   44,   44,  288,
  289,  266,  272,  273,   44,   44,   44,   44,   44,  279,
  607,   44,   60,  610,   44,   44,  286,   44,   44,   44,
  264,   44,   44,  751,  678,  679,   44,   40,  317,  273,
  745,  746,   44,  748,  631,   44,    0,  281,  282,  283,
  284,  285,  639,   91,    0,   44,   44,   60,   44,  697,
   40,  701,  702,  257,  704,  257,  653,   91,   44,  639,
   44,   44,  368,  778,   44,   44,   44,  373,   44,   44,
  376,  377,   44,  379,  380,  123,  382,  383,   91,  385,
  386,  387,  388,  389,  738,   44,  392,  257,  394,  395,
  396,  397,   44,   44,   44,  339,  340,   44,  695,  749,
  370,   44,   44,   91,   44,   44,  257,   40,   41,   44,
  123,   44,  371,  371,  384,  385,  386,   44,  715,  371,
   44,   44,   60,  268,  269,   44,  257,  272,  273,   62,
  275,  371,  371,   44,  788,  789,  790,   40,  257,   93,
  257,  372,  257,  288,  289,  451,  800,  744,  741,   40,
   40,   40,   40,   91,  379,   93,   40,  379,  268,  269,
   93,  379,  272,  273,   44,  275,    0,  272,  272,  268,
  269,  272,  317,  272,  273,  272,  275,   10,  288,  289,
   79,   19,  177,  176,  237,  123,  192,  493,  208,  288,
  289,  497,  125,  478,  500,  792,   40,  503,   54,  764,
  797,  798,  799,  509,  510,  212,  512,  317,   89,  257,
  258,  259,  315,  261,  262,  263,  365,  265,  317,  268,
  269,  315,  744,  272,  273,  273,  275,  481,  629,   -1,
  715,  279,   -1,  539,  540,  541,   -1,   -1,  286,  288,
  289,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,  125,  317,   -1,
   -1,   -1,  578,  286,  580,  581,   -1,  583,  584,   -1,
  586,  587,   -1,  589,  590,  591,  592,  593,    0,   -1,
  596,   40,  598,  599,  600,  601,   -1,   -1,   -1,   -1,
   -1,   -1,  608,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,   -1,  370,   -1,  288,  289,  264,   -1,   -1,   -1,
   -1,   -1,  288,  289,   -1,  273,  384,  385,  386,   -1,
  273,  274,   -1,  281,  282,  283,  284,  285,   -1,  655,
   -1,   -1,  658,  317,   -1,  661,  125,  370,  664,   -1,
   -1,  317,   -1,   -1,  670,  671,   -1,  673,   -1,   -1,
   -1,  384,  385,  386,   -1,   -1,   -1,   -1,   -1,   -1,
   40,   -1,   -1,   -1,  690,  691,  692,   -1,   -1,   -1,
   -1,  697,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,
   -1,   -1,  260,  336,   -1,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,  276,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,  273,
  274,  364,  365,  366,  367,  741,   42,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,  379,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,  314,  315,   -1,   -1,
  318,  319,  320,  321,  322,  323,  399,   -1,   -1,   40,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,
  413,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,
   -1,  260,  336,   -1,  338,   -1,   -1,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,  276,  352,  353,
   -1,  355,  356,  357,  358,  359,  360,  361,   -1,   -1,
  364,  365,  366,  367,  273,  274,  370,   -1,   -1,   -1,
  374,  375,  376,  377,  378,   -1,  380,  381,  382,  383,
  384,  385,  386,   -1,  388,  314,  315,   -1,   -1,  318,
  319,  320,  321,  322,  323,  399,  268,  269,   40,   -1,
  272,  273,   -1,  275,   -1,   -1,  410,  411,  412,  413,
   -1,   -1,   -1,   -1,   -1,   -1,  288,  289,  327,  328,
  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,   -1,  352,  353,  317,  355,  356,  357,  358,
  359,  360,  361,  273,  274,  364,  365,  366,  367,   -1,
   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,   -1,
  399,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,  329,
   -1,   -1,   -1,  279,   -1,   -1,  336,   -1,  338,   -1,
  286,  341,  342,  343,  344,  345,  346,  347,  348,  349,
  350,   -1,  352,  353,   -1,  355,  356,  357,  358,  359,
  360,  361,  273,  274,  364,  365,  366,  367,   -1,   -1,
  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,
  380,  381,  382,  383,  384,  385,  386,   -1,  388,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,   -1,  399,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
  410,  411,  412,  413,   -1,   -1,  327,  328,  329,  276,
   -1,   -1,  279,   -1,   -1,  336,   -1,  338,   -1,  286,
  341,  342,  343,  344,  345,  346,  347,  348,  349,  350,
   -1,  352,  353,   -1,  355,  356,  357,  358,  359,  360,
  361,  273,  274,  364,  365,  366,  367,   -1,   -1,  370,
   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,
  381,  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   40,   -1,   -1,  399,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,  410,
  411,  412,  413,   -1,   -1,  327,  328,  329,   -1,   -1,
   -1,  279,   -1,   -1,  336,   -1,  338,   -1,  286,  341,
  342,  343,  344,  345,  346,  347,  348,  349,  350,   -1,
  352,  353,   -1,  355,  356,  357,  358,  359,  360,  361,
  273,  274,  364,  365,  366,  367,   -1,   -1,  370,   -1,
   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,  381,
  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   40,  290,  291,  399,   -1,  294,
  295,  296,  297,  298,  299,  300,  301,   -1,  410,  411,
  412,  413,   -1,  308,  327,  328,  329,   -1,   -1,   -1,
   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,   -1,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,  273,
  274,  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,
   -1,  303,  304,   40,  306,  307,  399,   -1,  310,  311,
  312,  313,  314,  315,   -1,   -1,   -1,  410,  411,  412,
  413,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,
   -1,   -1,  336,   -1,  338,   -1,   -1,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,   -1,  352,  353,
   -1,  355,  356,  357,  358,  359,  360,  361,  273,  274,
  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,
  374,  375,  376,  377,  378,   -1,  380,  381,  382,  383,
  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   40,   -1,   -1,  399,  389,  390,  391,  392,
  393,  394,  395,  396,  397,  398,  410,  411,  412,  413,
   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,
   -1,  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,
  345,  346,  347,  348,  349,  350,   -1,  352,  353,   -1,
  355,  356,  357,  358,  359,  360,  361,  273,  274,  364,
  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,
  375,  376,  377,  378,   -1,  380,  381,  382,  383,  384,
  385,  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   40,   -1,   -1,  399,   -1,    0,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,
   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,  260,
  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,
  346,  347,  348,  349,  350,  276,  352,  353,   -1,  355,
  356,  357,  358,  359,  360,  361,  273,  274,  364,  365,
  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,
  376,  377,  378,   -1,  380,  381,  382,  383,  384,  385,
  386,   -1,  388,  314,  315,   -1,   -1,  318,  319,  320,
  321,  322,  323,  399,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,
  327,  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,
   -1,  338,    0,   -1,  341,  342,  343,  344,  345,  346,
  347,  348,  349,  350,   -1,  352,  353,   -1,  355,  356,
  357,  358,  359,  360,  361,  273,  274,  364,  365,  366,
  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,
  377,  378,   42,  380,  381,  382,  383,  384,  385,  386,
   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   60,   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,
  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,
  338,   91,   -1,  341,  342,  343,  344,  345,  346,  347,
  348,  349,  350,   -1,  352,  353,   -1,  355,  356,  357,
  358,  359,  360,  361,  273,  274,  364,  365,  366,  367,
   41,   -1,  370,  123,   -1,   -1,  374,  375,  376,  377,
  378,   42,  380,  381,  382,  383,  384,  385,  386,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
   -1,  399,   -1,   -1,  268,  269,   -1,   42,  272,  273,
   -1,  275,  410,  411,  412,  413,   -1,   -1,  327,  328,
  329,   -1,   -1,   -1,  288,  289,   -1,  336,   -1,  338,
   91,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,   -1,  352,  353,   42,  355,  356,  357,  358,
  359,  360,  361,  317,   -1,  364,  365,  366,  367,   -1,
   -1,  370,  123,   60,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,   -1,   -1,   -1,   91,   -1,   42,   -1,   44,   -1,
   -1,  410,  411,  412,  413,   -1,    0,  257,  258,  259,
   -1,  261,  262,  263,   60,  265,   -1,   -1,   -1,   -1,
  268,  269,  272,  273,  272,  273,  123,  275,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,  286,   -1,   -1,   -1,
  288,  289,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  303,  304,   -1,  306,  307,   60,   -1,
  310,  311,  312,  313,  314,  315,   -1,   -1,   -1,  317,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   91,
  261,  262,  263,   -1,  265,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,  276,   -1,   -1,  279,   -1,
   -1,  272,  273,   -1,   -1,  286,   -1,   60,  279,   -1,
  370,  123,  257,  258,  259,  286,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,  384,  385,  386,  272,  273,   -1,
   60,   -1,   -1,   -1,  279,   -1,   -1,   -1,   91,   -1,
   -1,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,   -1,  261,  262,  263,   60,  265,   -1,
   -1,   91,   -1,   60,   -1,   -1,  273,   -1,   -1,   -1,
  123,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,  286,
   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,   91,   -1,
   -1,   60,   -1,  123,   91,   -1,   -1,   -1,   -1,  370,
   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,   -1,   -1,  384,  385,  386,   91,  273,   -1,   -1,
  123,   -1,   91,  279,   -1,  370,  123,   -1,   -1,   -1,
  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  384,
  385,  386,  125,   -1,   -1,  257,  258,  259,  123,  261,
  262,  263,   -1,  265,  123,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,  370,  268,  269,   -1,  279,  272,  273,
   -1,  275,   -1,   -1,  286,   -1,   -1,  384,  385,  386,
   -1,   -1,   -1,   -1,  288,  289,   -1,   -1,   -1,   -1,
   -1,  303,  304,   -1,  306,  307,   -1,   -1,  310,  311,
  312,  313,  314,  315,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,  317,  370,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,  384,  385,
  386,   -1,  125,  286,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,  370,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
  273,   -1,  384,  385,  386,   -1,  273,   -1,  281,  282,
  283,  284,  285,   -1,  281,  282,  283,  284,  285,  264,
  273,   41,   -1,   -1,   -1,  264,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,  273,   -1,  281,  282,  283,  284,
  285,   -1,  281,  282,  283,  284,  285,  370,   -1,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  384,  385,  386,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  372,   -1,  327,  328,  329,   -1,   -1,   -1,
   -1,   -1,   -1,  336,   -1,  338,   -1,  354,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,  371,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,  354,
  273,  364,  365,  366,  367,  354,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,
  125,   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,
  413,   -1,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,
   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,   -1,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,  273,
   -1,  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,  399,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,  410,  411,  412,
  413,   -1,   -1,  327,  328,  329,  276,   -1,   -1,  279,
   -1,   -1,  336,   -1,  338,   -1,  286,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,   -1,  352,  353,
   -1,  355,  356,  357,  358,  359,  360,  361,  273,   -1,
  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,
  374,  375,  376,  377,  378,   -1,  380,  381,  382,  383,
  384,  385,  386,   -1,  388,   -1,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,
   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,
   -1,  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,
  345,  346,  347,  348,  349,  350,   -1,  352,  353,   -1,
  355,  356,  357,  358,  359,  360,  361,  273,   -1,  364,
  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,
  375,  376,  377,  378,   -1,  380,  381,  382,  383,  384,
  385,  386,   -1,  388,   -1,   -1,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,
   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,
  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,
  346,  347,  348,  349,  350,   -1,  352,  353,   -1,  355,
  356,  357,  358,  359,  360,  361,  273,   -1,  364,  365,
  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,
  376,  377,  378,   -1,  380,  381,  382,  383,  384,  385,
  386,   -1,  388,   -1,   -1,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,
  327,  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,
   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,  346,
  347,  348,  349,  350,   -1,  352,  353,   -1,  355,  356,
  357,  358,  359,  360,  361,  273,   -1,  364,  365,  366,
  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,
  377,  378,   -1,  380,  381,  382,  383,  384,  385,  386,
   -1,  388,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,
  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,
  338,   -1,   -1,  341,  342,  343,  344,  345,  346,  347,
  348,  349,  350,   -1,  352,  353,   -1,  355,  356,  357,
  358,  359,  360,  361,  273,   -1,  364,  365,  366,  367,
   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,
  378,   -1,  380,  381,  382,  383,  384,  385,  386,   -1,
  388,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,
  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,   -1,  352,  353,   -1,  355,  356,  357,  358,
  359,  360,  361,  273,   -1,  364,  365,  366,  367,   -1,
   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,  329,
   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,
   -1,  341,  342,  343,  344,  345,  346,  347,  348,  349,
  350,   -1,  352,  353,   -1,  355,  356,  357,  358,  359,
  360,  361,  273,   -1,  364,  365,  366,  367,   -1,   -1,
  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,
  380,  381,  382,  383,  384,  385,  386,   -1,  388,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  410,  411,  412,  413,   -1,   -1,  327,  328,  329,   -1,
   91,   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,
  341,  342,  343,  344,  345,  346,  347,  348,  349,  350,
   -1,  352,  353,   -1,  355,  356,  357,  358,  359,  360,
  361,  273,  123,  364,  365,  366,  367,   -1,   -1,  370,
   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,
  381,  382,  383,  384,  385,  386,   -1,  388,   60,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,
  411,  412,  413,   -1,   -1,  327,  328,  329,   -1,   91,
   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,  341,
  342,  343,  344,  345,  346,  347,  348,  349,  350,   -1,
  352,  353,   -1,  355,  356,  357,  358,  359,  360,  361,
   60,  123,  364,  365,  366,  367,   -1,   -1,  370,   -1,
   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,  381,
  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   60,   -1,  399,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,
  412,  413,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  123,   -1,   -1,   91,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   60,  292,   -1,   -1,   -1,   -1,  297,  298,  299,  300,
  301,  302,  303,  304,   60,  306,  307,  308,  123,  310,
  311,  312,  313,  314,  315,   -1,   -1,   -1,   -1,   -1,
   -1,   91,   -1,   -1,  325,  326,   -1,   -1,   -1,   41,
   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,  264,  123,   -1,   41,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,  123,   -1,  281,
  282,  283,  284,  285,   60,   -1,   -1,   -1,   -1,   91,
  292,   60,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,  303,  304,   -1,  306,  307,   41,   -1,  310,  311,
  312,  313,  314,  315,   -1,   91,   -1,   -1,   -1,   -1,
   -1,  123,   91,   -1,  264,   60,   -1,   -1,   91,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,  123,   -1,   -1,
   -1,   60,   -1,   -1,  123,   -1,   91,   -1,   -1,  264,
  123,   -1,  302,  303,  304,   -1,  306,  307,  273,   -1,
  310,  311,  312,  313,  314,  315,  281,  282,  283,  284,
  285,   -1,   91,   -1,   -1,   60,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  303,  304,
   -1,  306,  307,   -1,  264,  310,  311,  312,  313,  314,
  315,   60,   -1,  273,  123,   -1,   91,   -1,  264,   -1,
   -1,  281,  282,  283,  284,  285,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   91,  303,  304,   -1,  306,  307,  123,   -1,
  310,  311,  312,  313,  314,  315,   -1,  303,  304,   -1,
  306,  307,  264,   60,  310,  311,  312,  313,  314,  315,
   -1,  273,   -1,   -1,  123,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,  264,   -1,
   -1,  293,   -1,   60,   91,  264,   -1,  273,   -1,   -1,
   60,  264,   -1,   -1,  273,  281,  282,  283,  284,  285,
  273,   -1,  281,  282,  283,  284,  285,  293,  281,  282,
  283,  284,  285,   60,   91,   -1,  123,   -1,   60,  264,
   -1,   91,   -1,   -1,   -1,   -1,  305,   -1,  273,   -1,
   -1,   -1,  305,  278,   -1,   -1,  281,  282,  283,  284,
  285,   60,   -1,   -1,   91,  264,  123,   -1,   -1,   91,
   -1,   -1,   -1,  123,  273,  125,   60,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   60,   91,   -1,  293,   -1,  123,   -1,  125,  264,
   -1,  123,   -1,   -1,   -1,   60,   -1,   91,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   91,   -1,  123,  264,  265,   -1,  293,   -1,
   -1,   -1,   -1,   -1,  273,  274,   91,   -1,   -1,  123,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
  257,  258,  259,  260,  261,  262,  263,  264,  265,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,  276,
   -1,   -1,  279,   -1,  281,  282,  283,  284,  285,  286,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  261,
  262,   -1,   -1,   -1,  264,   -1,  273,  274,   -1,  276,
   -1,   -1,  279,  273,  281,  282,  283,  284,  285,  286,
   -1,  281,  282,  283,  284,  285,   -1,  264,  265,   -1,
   -1,   -1,  264,  265,   -1,   -1,  273,  274,   -1,   -1,
   -1,  273,  274,   -1,  281,  282,  283,  284,  285,  281,
  282,  283,  284,  285,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
  264,   -1,  281,  282,  283,  284,  285,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,  264,   -1,  281,  282,  283,
  284,  285,   -1,   -1,  273,  273,  274,   -1,   -1,  264,
   -1,   -1,  281,  282,  283,  284,  285,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  391,
  392,  393,  394,   -1,   -1,   -1,   -1,   -1,  400,  401,
  402,  403,  404,  405,  406,  407,  408,  409,   -1,  327,
  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,
  338,   -1,   -1,  341,  342,  343,  344,  345,  346,  347,
  348,  349,  350,   -1,  352,  353,   -1,  355,  356,  357,
  358,  359,  360,  361,  273,  274,  364,  365,  366,  367,
   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,
  378,   -1,  380,  381,  382,  383,  384,  385,  386,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,
  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,   -1,  352,  353,   -1,  355,  356,  357,  358,
  359,  360,  361,  273,  274,  364,  365,  366,  367,   -1,
   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,  329,
   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,
   -1,  341,  342,  343,  344,  345,  346,  347,  348,  349,
  350,   -1,  352,  353,   -1,  355,  356,  357,  358,  359,
  360,  361,  273,  274,  364,  365,  366,  367,   -1,   -1,
  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,
  380,  381,  382,  383,  384,  385,  386,   -1,  388,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  410,  411,  412,  413,   -1,   -1,  327,  328,  329,   -1,
   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,
  341,  342,  343,  344,  345,  346,  347,  348,  349,  350,
   -1,  352,  353,   -1,  355,  356,  357,  358,  359,  360,
  361,  273,  274,  364,  365,  366,  367,   -1,   -1,  370,
   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,
  381,  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,
  411,  412,  413,   -1,   -1,  327,  328,  329,   -1,   -1,
   -1,   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,  341,
  342,  343,  344,  345,  346,  347,  348,  349,  350,   -1,
  352,  353,   -1,  355,  356,  357,  358,  359,  360,  361,
  273,  274,  364,  365,  366,  367,   -1,   -1,  370,   -1,
   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,  381,
  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,
  412,  413,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,
   -1,   -1,   -1,  336,   -1,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,   -1,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,  273,
  274,  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,
  413,   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,
   -1,   -1,  336,   -1,  338,   -1,   -1,  341,  342,  343,
  344,  345,  346,  347,  348,  349,  350,   -1,  352,  353,
   -1,  355,  356,  357,  358,  359,  360,  361,  273,  274,
  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,
  374,  375,  376,  377,  378,   -1,  380,  381,  382,  383,
  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,
   -1,   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,
   -1,  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,
  345,  346,  347,  348,  349,  350,   -1,  352,  353,   -1,
  355,  356,  357,  358,  359,  360,  361,  273,  274,  364,
  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,
  375,  376,  377,  378,   -1,  380,  381,  382,  383,  384,
  385,  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,
   -1,  327,  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,
  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,
  346,  347,  348,  349,  350,   -1,  352,  353,   -1,  355,
  356,  357,  358,  359,  360,  361,  273,  274,  364,  365,
  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,
  376,  377,  378,   -1,  380,  381,  382,  383,  384,  385,
  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,
  327,  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,
   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,  346,
  347,  348,  349,  350,   -1,  352,  353,   -1,  355,  356,
  357,  358,  359,  360,  361,  273,  274,  364,  365,  366,
  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,
  377,  378,   -1,  380,  381,  382,  383,  384,  385,  386,
   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,
  328,  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,
  338,   -1,   -1,  341,  342,  343,  344,  345,  346,  347,
  348,  349,  350,   -1,  352,  353,   -1,  355,  356,  357,
  358,  359,  360,  361,  273,  274,  364,  365,  366,  367,
   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,
  378,   -1,  380,  381,  382,  383,  384,  385,  386,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  410,  411,  412,  413,   -1,   -1,  327,  328,
  329,   -1,   -1,   -1,   -1,   -1,   -1,  336,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,  273,  352,  353,   -1,  355,  356,  357,  358,
  359,  360,  361,   -1,   -1,  364,  365,  366,  367,   -1,
   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,   -1,   -1,   -1,   -1,   -1,  327,  328,  329,   -1,
   -1,  410,  411,  412,  413,  336,   -1,  338,   -1,   -1,
  341,  342,  343,  344,  345,  346,  347,  348,  349,  350,
  273,  352,  353,   -1,  355,  356,  357,  358,  359,  360,
  361,   -1,   -1,  364,  365,  366,  367,   -1,   -1,  370,
   -1,   -1,   -1,  374,  375,  376,  377,  378,   -1,  380,
  381,  382,  383,  384,  385,  386,   -1,  388,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,
   -1,   -1,   -1,   -1,  327,  328,  329,   -1,   -1,  410,
  411,  412,  413,  336,   -1,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,  273,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,   -1,
   -1,  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,
   -1,   -1,  327,  328,  329,   -1,   -1,  410,  411,  412,
  413,  336,   -1,  338,   -1,   -1,  341,  342,  343,  344,
  345,  346,  347,  348,  349,  350,  273,  352,  353,   -1,
  355,  356,  357,  358,  359,  360,  361,   -1,   -1,  364,
  365,  366,  367,   -1,   -1,  370,   -1,   -1,   -1,  374,
  375,  376,  377,  378,   -1,  380,  381,  382,  383,  384,
  385,  386,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,
  327,  328,  329,   -1,   -1,  410,  411,  412,  413,  336,
   -1,  338,   -1,   -1,  341,  342,  343,  344,  345,  346,
  347,  348,  349,  350,  273,  352,  353,   -1,  355,  356,
  357,  358,  359,  360,  361,   -1,   -1,  364,  365,  366,
  367,   -1,   -1,  370,   -1,   -1,   -1,  374,  375,  376,
  377,  378,   -1,  380,  381,  382,  383,  384,  385,  386,
   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  399,   -1,   -1,   -1,   -1,   -1,  327,  328,
  329,   -1,   -1,  410,  411,  412,  413,  336,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,  347,  348,
  349,  350,   -1,  352,  353,   -1,  355,  356,  357,  358,
  359,  360,  361,   -1,   -1,  364,  365,  366,  367,   -1,
   -1,  370,   -1,   -1,   -1,  374,  375,  376,  377,  378,
   -1,  380,  381,  382,  383,  384,  385,  386,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  410,  411,  412,  413,  338,   -1,   -1,  341,  342,
  343,  344,  345,  346,  347,  348,  349,  350,   -1,  352,
  353,   -1,  355,  356,  357,  358,  359,  360,  361,   -1,
   -1,  364,  365,  366,  367,   -1,   -1,  370,   -1,   -1,
   -1,  374,  375,  376,  377,  378,   -1,  380,  381,  382,
  383,  384,  385,  386,   -1,  388,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  399,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  410,  411,  412,
  413,
  };

#line 1188 "Iril/IR/IR.jay"

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
  public const int NONNULL = 311;
  public const int NOCAPTURE = 312;
  public const int WRITEONLY = 313;
  public const int READONLY = 314;
  public const int READNONE = 315;
  public const int ATTRIBUTE_GROUP_REF = 316;
  public const int ATTRIBUTES = 317;
  public const int NORECURSE = 318;
  public const int NOUNWIND = 319;
  public const int SPECULATABLE = 320;
  public const int SSP = 321;
  public const int UWTABLE = 322;
  public const int ARGMEMONLY = 323;
  public const int SEQ_CST = 324;
  public const int DSO_LOCAL = 325;
  public const int DSO_PREEMPTABLE = 326;
  public const int RET = 327;
  public const int BR = 328;
  public const int SWITCH = 329;
  public const int INDIRECTBR = 330;
  public const int INVOKE = 331;
  public const int RESUME = 332;
  public const int CATCHSWITCH = 333;
  public const int CATCHRET = 334;
  public const int CLEANUPRET = 335;
  public const int UNREACHABLE = 336;
  public const int FNEG = 337;
  public const int ADD = 338;
  public const int NUW = 339;
  public const int NSW = 340;
  public const int FADD = 341;
  public const int SUB = 342;
  public const int FSUB = 343;
  public const int MUL = 344;
  public const int FMUL = 345;
  public const int UDIV = 346;
  public const int SDIV = 347;
  public const int FDIV = 348;
  public const int UREM = 349;
  public const int SREM = 350;
  public const int FREM = 351;
  public const int SHL = 352;
  public const int LSHR = 353;
  public const int EXACT = 354;
  public const int ASHR = 355;
  public const int AND = 356;
  public const int OR = 357;
  public const int XOR = 358;
  public const int EXTRACTELEMENT = 359;
  public const int INSERTELEMENT = 360;
  public const int SHUFFLEVECTOR = 361;
  public const int EXTRACTVALUE = 362;
  public const int INSERTVALUE = 363;
  public const int ALLOCA = 364;
  public const int LOAD = 365;
  public const int STORE = 366;
  public const int FENCE = 367;
  public const int CMPXCHG = 368;
  public const int ATOMICRMW = 369;
  public const int GETELEMENTPTR = 370;
  public const int ALIGN = 371;
  public const int INBOUNDS = 372;
  public const int INRANGE = 373;
  public const int TRUNC = 374;
  public const int ZEXT = 375;
  public const int SEXT = 376;
  public const int FPTRUNC = 377;
  public const int FPEXT = 378;
  public const int TO = 379;
  public const int FPTOUI = 380;
  public const int FPTOSI = 381;
  public const int UITOFP = 382;
  public const int SITOFP = 383;
  public const int PTRTOINT = 384;
  public const int INTTOPTR = 385;
  public const int BITCAST = 386;
  public const int ADDRSPACECAST = 387;
  public const int ICMP = 388;
  public const int EQ = 389;
  public const int NE = 390;
  public const int UGT = 391;
  public const int UGE = 392;
  public const int ULT = 393;
  public const int ULE = 394;
  public const int SGT = 395;
  public const int SGE = 396;
  public const int SLT = 397;
  public const int SLE = 398;
  public const int FCMP = 399;
  public const int OEQ = 400;
  public const int OGT = 401;
  public const int OGE = 402;
  public const int OLT = 403;
  public const int OLE = 404;
  public const int ONE = 405;
  public const int ORD = 406;
  public const int UEQ = 407;
  public const int UNE = 408;
  public const int UNO = 409;
  public const int PHI = 410;
  public const int SELECT = 411;
  public const int CALL = 412;
  public const int TAIL = 413;
  public const int VA_ARG = 414;
  public const int LANDINGPAD = 415;
  public const int CATCHPAD = 416;
  public const int CLEANUPPAD = 417;
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
