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
//t    "global_variable : GLOBAL_SYMBOL '=' global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type value ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage global_kind type ',' ALIGN INTEGER",
//t    "global_kind : GLOBAL",
//t    "global_kind : CONSTANT",
//t    "linkage : EXTERNAL",
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
//t    "value : constant",
//t    "value : LOCAL_SYMBOL",
//t    "value : GLOBAL_SYMBOL",
//t    "value : INTTOPTR '(' typed_value TO type ')'",
//t    "value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "value : BITCAST '(' typed_value TO type ')'",
//t    "value : PTRTOINT '(' typed_value TO type ')'",
//t    "value : '<' typed_values '>'",
//t    "value : '[' typed_values ']'",
//t    "value : '{' typed_values '}'",
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
//t    "instruction : AND type value ',' value",
//t    "instruction : ASHR type value ',' value",
//t    "instruction : ASHR EXACT type value ',' value",
//t    "instruction : BITCAST typed_value TO type",
//t    "instruction : CALL return_type function_pointer function_args",
//t    "instruction : CALL calling_convention return_type function_pointer function_args",
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
    "FASTCC","SIGNEXT","ZEROEXT","VOLATILE","RETURNED","NONNULL",
    "NOCAPTURE","WRITEONLY","READONLY","READNONE","ATTRIBUTE_GROUP_REF",
    "ATTRIBUTES","NORECURSE","NOUNWIND","SPECULATABLE","SSP","UWTABLE",
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
    "UNE","UNO","PHI","SELECT","CALL","TAIL","VA_ARG","LANDINGPAD",
    "CATCHPAD","CLEANUPPAD","DEREFERENCEABLE",
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
#line 60 "Iril/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 64 "Iril/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 68 "Iril/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 72 "Iril/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 76 "Iril/IR/IR.jay"
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
#line 96 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 100 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 109 "Iril/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 121 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 125 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 129 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 133 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 137 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 141 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 145 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 149 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 153 "Iril/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 27:
#line 157 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 28:
#line 158 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 162 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 30:
#line 163 "Iril/IR/IR.jay"
  { yyVal = false; }
  break;
case 31:
#line 164 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 32:
#line 165 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 33:
#line 166 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 34:
#line 170 "Iril/IR/IR.jay"
  { yyVal = true; }
  break;
case 35:
  case_35();
  break;
case 36:
  case_36();
  break;
case 37:
#line 187 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 188 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 39:
#line 189 "Iril/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 40:
#line 193 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 41:
#line 197 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 51:
#line 225 "Iril/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 52:
#line 229 "Iril/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 53:
#line 236 "Iril/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 54:
#line 240 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 55:
#line 244 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 56:
#line 248 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 57:
#line 252 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 75:
#line 285 "Iril/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 76:
#line 289 "Iril/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 77:
#line 293 "Iril/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 78:
#line 300 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 79:
#line 304 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 309 "Iril/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 84:
#line 315 "Iril/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 85:
#line 316 "Iril/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 86:
#line 317 "Iril/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 87:
#line 318 "Iril/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 88:
#line 322 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 89:
#line 326 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 90:
#line 330 "Iril/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 91:
#line 334 "Iril/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 92:
#line 338 "Iril/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 342 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 349 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 95:
#line 353 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 97:
#line 361 "Iril/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 98:
  case_98();
  break;
case 99:
  case_99();
  break;
case 100:
  case_100();
  break;
case 101:
  case_101();
  break;
case 102:
  case_102();
  break;
case 103:
  case_103();
  break;
case 104:
#line 401 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 105:
#line 405 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create ((object)true, yyVals[0+yyTop]);
    }
  break;
case 106:
#line 409 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 107:
#line 413 "Iril/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 108:
#line 420 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 109:
#line 424 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 110:
#line 428 "Iril/IR/IR.jay"
  {
        yyVal = true;
    }
  break;
case 115:
#line 439 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 116:
#line 443 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 117:
#line 447 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 118:
#line 451 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 119:
#line 455 "Iril/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 120:
#line 459 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 121:
#line 460 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 122:
#line 467 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 471 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 124:
#line 478 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 125:
#line 482 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 126:
#line 486 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 127:
#line 490 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 129:
#line 498 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 130:
#line 502 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 131:
#line 503 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 132:
#line 504 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 133:
#line 505 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 134:
#line 506 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 135:
#line 507 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 136:
#line 508 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 137:
#line 509 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 138:
#line 513 "Iril/IR/IR.jay"
  {
        yyVal = ParameterAttributes.Dereferenceable;
    }
  break;
case 146:
#line 536 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 147:
#line 537 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 148:
#line 538 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 149:
#line 539 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 150:
#line 540 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 151:
#line 541 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 152:
#line 542 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 153:
#line 543 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 154:
#line 544 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 155:
#line 545 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 156:
#line 549 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 157:
#line 550 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 158:
#line 551 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 159:
#line 552 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 160:
#line 553 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 161:
#line 554 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 162:
#line 555 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 163:
#line 556 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 164:
#line 557 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 165:
#line 558 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 166:
#line 559 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 167:
#line 560 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 168:
#line 561 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 169:
#line 562 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 170:
#line 563 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 171:
#line 564 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 173:
#line 569 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 174:
#line 570 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 175:
#line 574 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 176:
#line 578 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 582 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 178:
#line 586 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 179:
#line 590 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 180:
#line 594 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 181:
#line 598 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 183:
#line 606 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 184:
#line 607 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 185:
#line 608 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 186:
#line 609 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 187:
#line 610 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 188:
#line 611 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 189:
#line 612 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 190:
#line 613 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 191:
#line 614 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 192:
#line 621 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 628 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 632 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 195:
#line 639 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 646 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 650 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 657 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 668 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 672 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 679 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 683 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 690 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 694 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 206:
#line 698 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 702 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 709 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 713 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 720 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 724 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 728 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 732 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 740 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 216:
#line 741 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 217:
#line 748 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 752 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 759 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 220:
#line 763 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 221:
#line 767 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 222:
#line 771 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 223:
#line 775 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 224:
#line 779 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 225:
#line 783 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 227:
#line 788 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 228:
#line 792 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 229:
#line 796 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 230:
#line 800 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 804 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 236:
#line 821 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 825 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 831 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 239:
#line 838 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 842 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 849 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 867 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 248:
#line 874 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 878 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 882 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 886 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 252:
#line 890 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 253:
#line 897 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 901 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 905 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 909 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 913 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 258:
#line 917 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 259:
#line 921 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 925 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 261:
#line 929 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 262:
#line 933 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 263:
#line 937 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 264:
#line 941 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 265:
#line 945 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 266:
#line 949 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 267:
#line 953 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 268:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 269:
#line 961 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 270:
#line 965 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 271:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 973 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 977 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 981 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 985 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 993 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 997 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1001 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1005 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1009 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1013 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1017 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1021 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1025 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1029 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1033 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 288:
#line 1037 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 289:
#line 1041 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 290:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 291:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 303:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 304:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 306:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 307:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1129 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 312:
#line 1133 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 313:
#line 1137 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 314:
#line 1141 "Iril/IR/IR.jay"
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
#line 78 "Iril/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 83 "Iril/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 88 "Iril/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 102 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 111 "Iril/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_35()
#line 175 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_36()
#line 180 "Iril/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_98()
#line 366 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-6+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_99()
#line 371 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-9+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-8+yyTop], (IEnumerable<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_100()
#line 376 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_101()
#line 381 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-7+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1);
    }

void case_102()
#line 386 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-10+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-9+yyTop], (IEnumerable<Parameter>)yyVals[-8+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

void case_103()
#line 391 "Iril/IR/IR.jay"
{
        var h = (Tuple<object, object>)yyVals[-8+yyTop];
        yyVal = new FunctionDefinition ((LType)h.Item2, (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)h.Item1, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,   10,   10,   16,   16,
   16,   16,   16,   15,    9,    9,   17,   17,   17,   17,
   17,   18,   21,   21,   22,   23,   23,   23,   23,   23,
   13,   13,    8,    8,    8,    8,    8,   25,   25,   25,
    7,    7,   27,   27,   27,   27,   27,   27,   27,   27,
   27,   27,   27,   27,    3,    3,    3,   28,   28,   29,
   29,   11,   11,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   30,   30,   31,   31,    4,    4,    4,
    4,    4,    4,   32,   32,   32,   32,   37,   37,   37,
   37,   37,   37,   37,    5,    5,    5,    5,    5,   33,
   33,   41,   41,   42,   42,   42,   42,   40,   40,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   14,   14,
   38,   38,   34,   34,   43,   44,   44,   44,   44,   44,
   44,   44,   44,   44,   44,   45,   45,   45,   45,   45,
   45,   45,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   48,   19,   19,   19,   19,   19,   19,   19,   19,
   19,   49,   26,   26,   50,   47,   47,   24,   51,   46,
   46,   35,   35,   52,   52,   52,   52,   53,   53,   55,
   55,   55,   55,   57,   58,   58,   59,   59,   60,   60,
   60,   60,   60,   60,   60,   61,   61,   61,   61,   61,
   61,   20,   20,   62,   62,   63,   63,   64,   65,   65,
   66,   67,   67,   68,   68,   39,   69,   54,   54,   54,
   54,   54,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    8,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    3,    3,    3,    3,    6,
    5,    1,    1,    3,    1,    1,    1,    1,    1,    1,
    2,    3,    1,    2,    3,    3,    3,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    3,
    1,    1,    1,    4,    2,    3,    5,    1,    3,    1,
    1,    1,    1,    1,    1,    1,    1,    3,    4,    2,
    1,    5,    5,    1,    3,    1,    1,    7,   10,    8,
    8,   11,    9,    2,    3,    3,    4,    1,    1,    1,
    1,    2,    3,    2,    5,    6,    6,    7,    8,    3,
    2,    1,    3,    1,    2,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    4,    1,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    6,    9,    6,    6,    3,    3,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    2,    2,    1,    2,    1,    3,    2,    1,    1,
    3,    1,    2,    2,    3,    1,    2,    1,    2,    1,
    2,    3,    4,    1,    3,    2,    1,    3,    2,    3,
    3,    3,    2,    4,    5,    1,    1,    6,    9,    6,
    6,    1,    3,    1,    1,    1,    3,    5,    1,    2,
    3,    1,    2,    1,    1,    1,    1,    2,    7,    2,
    7,    1,    5,    6,    5,    5,    5,    6,    4,    4,
    5,    6,    5,    6,    6,    6,    7,    5,    6,    7,
    4,    5,    6,    5,    2,    5,    4,    4,    4,    4,
    5,    6,    7,    6,    6,    4,    7,    8,    5,    6,
    5,    5,    6,    3,    4,    5,    6,    7,    4,    5,
    6,    6,    4,    5,    7,    8,    5,    6,    4,    5,
    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   81,   91,   84,   85,   86,   87,   83,  108,   30,
   29,   31,   32,   33,  246,  135,  136,  137,  130,  131,
  133,  132,  134,  141,  142,    0,    0,    0,    0,   82,
    0,    0,    0,    0,    0,  109,  110,    0,    0,  128,
    0,    0,    3,    0,    4,    0,    0,  139,  140,   27,
   28,   34,    0,    0,    0,    0,    0,    0,    0,    0,
   75,    0,    0,    0,    0,    0,    0,    0,   90,    0,
  114,    0,    0,    0,    0,    0,    0,    0,    0,  129,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,   76,    0,    0,
    0,    0,    0,  113,   97,   88,    0,    0,   94,    0,
    0,    0,    0,    0,    0,  126,  127,  121,    0,    0,
  122,  145,    0,    0,  143,  185,  186,  184,  187,  188,
  189,  183,  174,  173,  191,  190,    0,    0,    0,    0,
    0,    0,    0,    0,  172,    0,    0,    0,    0,    0,
    0,    0,    0,   35,    0,    0,    0,   60,   59,   13,
    0,    0,   53,   58,    0,    0,    0,    0,  138,   89,
    0,    0,    0,    0,    0,    0,    0,    0,   73,   65,
   63,   64,   66,   67,   68,   69,    0,   61,    0,  120,
    0,    0,    0,    0,    0,    0,  144,    0,    0,    0,
    0,  196,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   15,    0,    0,    0,   54,   14,
    0,  193,   92,   77,   93,   95,    0,    0,    0,    0,
    0,    0,   12,   62,  123,    0,    0,    0,   51,    0,
    0,    0,    0,    0,  252,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  202,
    0,    0,  208,    0,    0,    0,    0,    0,    0,    0,
  181,    0,  179,  180,    0,    0,    0,    0,    0,    0,
    0,   48,    0,   46,    0,   37,   49,    0,   43,   45,
   50,   38,   39,   36,   17,   16,   57,   56,   55,    0,
    0,   70,  235,  234,    0,  232,    0,    0,    0,    0,
    0,  250,    0,    0,  248,    0,  244,  245,    0,    0,
  242,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  247,  275,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  146,  147,  148,
  149,  150,  151,  152,  153,  154,  155,    0,  156,  157,
  168,  169,  170,  171,  159,  161,  162,  163,  164,  160,
  158,  166,  167,  165,    0,    0,    0,    0,    0,    0,
    0,   98,  203,    0,  209,    0,    0,   52,    0,    0,
    0,    0,    0,  197,    0,    0,    0,   26,    0,    0,
    0,    0,  198,    0,    0,   74,    0,    0,  101,    0,
    0,    0,  192,    0,    0,    0,    0,  243,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  236,    0,  214,    0,
    0,    0,    0,    0,    0,    0,    0,  100,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    0,   41,    0,
   47,   44,  233,    0,  103,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  271,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  178,  175,  177,    0,    0,    0,    0,   40,    0,
   99,    0,    0,  253,    0,  272,  307,    0,  281,  292,
    0,  276,  310,    0,  296,  274,  312,  304,  300,    0,
    0,  289,    0,  257,  256,  291,  313,    0,    0,  255,
    0,  182,  195,    0,    0,    0,    0,    0,    0,    0,
    0,  237,    0,    0,  216,    0,    0,  217,    0,    0,
  261,    0,    0,    0,    0,    0,    0,    0,    0,  102,
    0,    0,    0,  239,  254,  308,  293,  297,  301,  290,
  258,  285,  302,    0,    0,    0,    0,    0,  199,    0,
  200,  284,  273,    0,    0,    0,    0,  219,    0,  215,
    0,    0,  262,    0,    0,  269,    0,    0,  249,    0,
  251,  240,    0,  287,    0,  305,    0,    0,  238,  298,
    0,  227,  221,    0,    0,    0,    0,  226,  222,  220,
  218,    0,  270,  176,  241,  288,  306,  201,  224,    0,
    0,    0,    0,    0,  225,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  231,  228,
  230,    0,    0,  229,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   50,   12,   13,   14,  207,  181,  173,   73,
  182,  539,  216,   74,   75,   52,  174,  336,  165,  355,
  338,  339,  340,  341,  183,  719,  208,   83,   84,  128,
  129,   15,  103,  144,  309,   60,   55,   56,   57,   61,
  140,  141,  145,  428,  445,  720,  223,  673,  365,  600,
  721,  310,  311,  312,  313,  314,  540,  623,  687,  688,
  759,  356,  536,  537,  703,  704,  370,  371,  403,
  };
  protected static readonly short [] yySindex = {          -92,
  -26, -133,   21,   87,   92,  674,  382, -268,    0,  -92,
    0,    0,    0,    0, -100,  -67,  139,  148,  259,  -62,
   29,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3741,  -98,  -23,  187,    0,
  206, -166,  213, 3785, 2171,    0,    0, 2171,  -30,    0,
 2171,  222,    0,  262,    0,   62,   67,    0,    0,    0,
    0,    0, 3785, -190,  -12,  -79,    9,  300,  -28,  221,
    0,  206,  -14,  213,   83, 3785,   95,  110,    0,   70,
    0, 2663,  213,  213, 3785,  -22, 2171,  262,  -21,    0,
  246,  821, -172,    0,    0, 2401, 3785, -190, 3785, -190,
    0,  254,    0, -242,  339,  270, 3703,    0, 3785, 3785,
  -13, 3785,  341,    0,    0,    0,  206,  116,    0,  213,
  262,  -17, -172,  262, 2425,    0,    0,    0,   22,  154,
    0,    0,   89, -108,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   31,  357,  360,  368,
 3880, 3880, 3880,  366,    0, 2401, 3785, 2337, 3785,  353,
  354,  355,  192,    0, -242, 3604,    0,    0,    0,    0,
  -18, 2401,    0,    0,  206,   88,  352,   24,    0,    0,
 3599,   89,  262,   89,   89,  -12,  358,  381,    0,    0,
    0,    0,    0,    0,    0,    0, 3425,    0,  188,    0,
 3577,  -90,  161,  180, 4776, -110,    0,  398, 3880, 3880,
 3880,    0,   17,   90,  -15,   73,  402, 2401,   80,  414,
 2382, 3637,  185,  621,    0, -242,  203,   14,    0,    0,
 3708,    0,    0,    0,    0,    0,   89,  -12,   89,   89,
  200,  929,    0,    0,    0,  204, 4776, -103,    0,  189,
  401, 3880, -185, 3880,    0, 3423, 3785, 3423, 3785, 3423,
 3785, 3785,  122, 3785, 3785, 3785, 3423, 2268, 2408, 3785,
 3785, 3785, 3880, 3880, 3880, 3785, 3448, 3507,  147,  -44,
 3880, 3880, 3880, 3880, 3880, 3880, 3880, 3880, 3880, 3880,
 3880, 3880,  990, 3684, 3785, 3785, 2111,   66, 2453,    0,
 4776,  189,    0,  189,  196, 4776, 3785,  101,  102,  103,
    0, 3880,    0,    0,  223,  112,  440,  231,  118,  123,
  456,    0,  464,    0, 1028,    0,    0,  384,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   89,
   89,    0,    0,    0,  248,    0,  189, 2544, 4776, -101,
 5216,    0,  233, 2318,    0,  465,    0,    0, 2401, 3423,
    0, 2401, 2401, 3423, 2401, 2401, 3423, 2401, 2401, 3785,
 2401, 2401, 2401, 2401, 2401, 3423, 3785, 2401, 3785, 2401,
 2401, 2401, 2401,  469,  470,  484,   46, 3785,   85, 3880,
  485,    0,    0, 3785,  126,  155,  159,  160,  163,  168,
  171,  176,  177,  186,  198,  207,  209,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3785,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 3785,    1, 2401,  318, 3785, 2171,
 2111,    0,    0,  189,    0,  267,  267,    0, 2635,  166,
 3785, 3785, 3785,    0,  189,  306,  197,    0,  310,  321,
  220, 2448,    0, 3674,   89,    0,  929,  -99,    0, 2726,
 4776,  189,    0,  544,  323,  549, 2401,    0,  550,  555,
 2401,  557,  559, 2401,  562,  564, 2401,  565,  567,  570,
  571,  573, 2401, 2401,  574, 2401,  575,  576,  577,  579,
 3880, 3880, 3880,  258,  175, 3785,  582, 3785,  181, 3880,
 3785, 3785, 3785, 3785, 3785, 3785, 3785, 3785, 3785, 3785,
 3785, 3785, 2401, 2401, 2318,  583,    0,  585,    0,  591,
  318,  318, 3785,  318, 3785, 2171,  267,    0, 3880,  146,
  249,  313,  267,  189,  376,  189,    0,  377,    0,  263,
    0,    0,    0, 4776,    0, 2817,  267,  323,  545, 2318,
  594, 2318, 2318,  595, 2318, 2318,  596, 2318, 2318,  597,
 2318, 2318, 2318, 2318, 2318,  598,  603, 2318,  605, 2318,
 2318, 2318, 2318,    0,  606,  607,  396, 3785, 2401,  610,
 3785,  612, 3880,  613,  206,  206,  206,  206,  206,  206,
  206,  206,  206,  206,  206,  206,  615,  616,  618,  578,
 3880, 3540,   89,  591,  591,  318,  591,  318,  318, 3785,
  624,    0,    0,    0,  267,  189,  267,  189,    0, 2908,
    0,  626, 3785,    0, 2318,    0,    0, 2318,    0,    0,
 2318,    0,    0, 2318,    0,    0,    0,    0,    0, 2318,
 2318,    0, 2318,    0,    0,    0,    0, 3880, 3880,    0,
  627,    0,    0,  305,  631,  309,  633, 3880, 2318, 2318,
 2318,    0,  636, 3756,    0,  451,  273,    0,   89,   89,
    0,  591,   89,  591,  591,  318, 3880,  267,  267,    0,
  323,  638, 3780,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  316,  437,  320,  438, 3880,    0,  652,
    0,    0,    0,  608, 3880,  662,   84,    0, 2057,    0,
 3852,   89,    0,   89,   89,    0,  591,  275,    0,  323,
    0,    0,  446,    0,  447,    0,  652, 3880,    0,    0,
 2494,    0,    0,  337,  666,  667,  675,    0,    0,    0,
    0,   89,    0,    0,    0,    0,    0,    0,    0,  277,
  677, 3880, 3880, 3880,    0, 3785,  344,  345,  346,  268,
 3785, 3785, 3785, 3880,  324,  334,  343,  678,    0,    0,
    0, 3880,  279,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  718,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  588, 2212,  454,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    6,    0,    0,    0,    0,    0,    0,    0, 2263,
    0,    0,  457,  459,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  688,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  162,    0,    0,  460,
    0,    0,    0,    0,    0,    0,    0,    0,  191,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  688,    0,  688,    0,    0,
    0,    0,    0,    0,    0,    0,  794,    0,    0,    0,
    0,  688,    0,    0,   54,  688,    0,  688,    0,    0,
    0,    0,    0,    0,  214,    0, 3841, 3869,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  293,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  688,    0,    0,
  688,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  297,    0,  475,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 2999,    0, 4867,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  600,    0,    0,    0,
    0,    0,   65,    0,  688,    0,    0,  294,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  622,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  688,    0,
    0,  688,  688,    0,  688,  688,    0,  688,  688,    0,
  688,  688,  688,  688,  688,    0,    0,  688,    0,  688,
  688,  688,  688,    0,    0,    0,  688,    0,  688,    0,
    0,    0,    0,    0,  688,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  688,  688,    0,    0,    0,
    0,    0,    0, 3090,    0, 3181, 4958,    0,    0,  688,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  673,    0,    0,    0,    0,    0,
    0, 5049,    0,    0,    0,    0,  688,    0,    0,    0,
  688,    0,    0,  688,    0,    0,  688,    0,    0,    0,
    0,    0,  688,  688,    0,  688,    0,    0,    0,    0,
    0,    0,    0,    0,  688,    0,    0,    0,  688,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  688,  688,    0, 3866,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3272,    0,    0,  688,
  688,  688,  766,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 5140,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  688,    0,
    0,    0,    0,    0,  888,  979, 1073, 1164, 1258, 1349,
 1443, 1534, 1628, 1719, 1813, 1904,    0,    0,    0,    0,
    0,    0, 3957,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  774,  784, 2069,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  688,    0,    0, 4048,    0,
    0,    0, 4139,    0,    0,    0,    0, 2215, 2339,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4230,
    0,    0,    0,    0,    0,  295,  688,    0,    0,    0,
    0, 4321,    0, 4412, 4503,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4594,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4685,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  688,
    0,    0,    0,    0,  688,  688,  688,    0,    0,    0,
    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  723,  659,    0,    0,    0,    0,  563,  566,   59,
   -6,  430, -167,  285,    0,  719,  504,    0, -220, -465,
    0,  271,    0, -601,    0,  132,  535,  660,    2,    0,
  558,    0,  -47, -122, -251,   -2,    0,  699,  -49,  -56,
    0,  541, -127,    0,    0, -643,  228,    0, -458, -447,
    8, -281,    0,  458,  461,  406, -505, -551,    0,   37,
    0,  296,    0,  151,    0,   75,  -76, -212,    0,
  };
  protected static readonly short [] yyTable = {            51,
   51,   97,   91,   54,  116,  358,  560,   53,   59,   92,
  195,  337,  316,  343,  215,   47,  217,   92,   92,  359,
  212,  481,   92,  564,   86,  241,  569,  453,  322,  119,
  119,  354,  257,  170,   16,  624,  625,  171,  627,   82,
  124,  702,   89,   62,  258,   80,   48,   51,   51,   78,
  133,   51,   95,  738,   51,   93,   94,  241,  100,   96,
  322,   80,   99,   89,  459,   89,  106,  217,  112,  247,
  602,  249,  690,  691,  747,  693,  453,  324,   46,   82,
  363,   19,  209,  192,  217,  127,  196,   89,   51,  514,
   51,  535,  360,   80,  100,  139,  130,   79,  132,  364,
  166,  702,  168,   70,   71,   47,  240,  480,   47,  642,
  118,  187,  185,  186,  473,  188,  245,   68,   69,  217,
  692,  217,  694,  695,  172,   89,   89,  351,  516,   89,
   78,   46,  107,  322,  109,   35,   17,   18,  346,  142,
  733,  321,  735,  736,  456,  248,  457,   20,  793,  243,
  671,  323,   21,  675,   44,   45,  190,  488,   85,  191,
  228,  488,  231,  315,  488,  213,  167,   89,  169,  520,
  315,   64,  315,  488,  315,    1,    2,  453,   79,    3,
    4,   47,    5,  213,  127,  763,  632,   89,   47,  478,
  737,  374,   65,  377,  210,    6,    7,  211,  453,   66,
  386,   80,   96,  142,  139,   96,  100,   89,   67,  549,
   68,   69,   48,  115,   70,   71,   89,   77,  598,   22,
    8,  142,   89,  217,  603,  335,   88,  475,   23,  566,
   80,  124,  235,   87,  124,  236,   24,   25,   26,   27,
   28,   98,  739,  345,   46,  115,  236,   89,  184,  131,
  134,  354,   92,  337,  193,  239,  354,  450,  214,  369,
  372,  373,  375,  376,  378,  379,  381,  382,  383,  384,
  385,  388,  390,  391,  392,  393,  256,   68,   69,  397,
  399,  765,  101,  405,  453,  770,  547,  239,  476,  633,
   89,  477,  222,  222,  222,  111,  116,  553,  446,  447,
   51,  102,   78,  639,  449,   79,  477,  184,  448,   89,
  460,  784,  640,  730,  567,  764,  731,  775,  748,  794,
  477,  104,  748,  404,   36,   37,  105,   38,   39,   40,
   41,   42,   43,  125,   42,  223,  125,   42,  223,  114,
  146,  147,  148,  117,  149,  150,  151,  217,  152,  120,
  318,  319,  320,  634,   89,  752,  753,   92,  453,  108,
  110,  122,  155,  487,  789,   89,  123,  491,  135,  156,
  494,   35,  349,  497,  790,   89,   86,  162,  175,  503,
  504,  189,  506,  791,   89,   22,  635,  143,  637,  224,
  225,  515,  176,  362,   23,  366,  219,  519,  218,  220,
  142,  546,   24,   25,   26,   27,   28,  221,  163,  226,
  232,  233,  234,  244,  394,  395,  396,  194,  251,  401,
  252,  533,  406,  407,  408,  409,  410,  411,  412,  413,
  414,  415,  416,  417,  259,   49,  260,  317,  534,  325,
  161,   47,   51,   51,   51,  326,  328,  543,  545,  754,
  541,  542,  544,  464,  550,  551,  552,  329,  342,  352,
  357,  361,  213,  755,  756,  757,  402,  335,  698,  458,
  699,  380,   48,  451,  117,  461,  462,  463,  466,  465,
  250,  115,  115,  467,  469,  115,  115,  468,  115,  470,
   36,   37,   89,   38,   39,   40,   41,   42,   43,  471,
  689,  115,  115,  472,   46,  483,  758,  474,  485,  599,
  162,  599,  511,  512,  605,  606,  607,  608,  609,  610,
  611,  612,  613,  614,  615,  616,  115,  513,  518,  521,
  354,  517,  350,  522,  523,  164,   51,  524,   51,   51,
  315,  163,  525,  630,  626,  526,  628,  629,   68,   69,
  527,  528,   70,   71,   72,   30,   31,   32,   33,   34,
  529,  217,  554,  555,  116,  116,  556,  732,  116,  116,
  734,  116,  530,  161,  146,  147,  148,  557,  149,  150,
  151,  531,  152,  532,  116,  116,  558,  568,  363,  153,
  154,  599,  570,  572,  599,  227,  155,  230,  573,   21,
  575,   49,  576,  156,  217,  578,  217,  579,  581,  116,
  582,  242,  762,  583,  584,  686,  585,  588,  590,  591,
  592,  118,  593,   51,  597,  601,  620,   80,  621,  729,
  622,  696,  636,  638,  217,  643,  335,  645,  648,  651,
  654,  660,  594,  595,  596,   22,  661,   80,  663,  668,
  669,  604,  670,  674,   23,  676,  678,  327,  679,  680,
  331,  681,   24,   25,   26,   27,   28,  697,  535,  701,
  714,  715,  119,   58,  716,  717,  718,  727,   80,  725,
  631,  740,  743,  157,   36,   37,  745,   38,   39,   40,
   41,   42,   43,  744,  746,  748,  335,  158,  159,  160,
  749,  751,  766,  767,  771,  772,  773,  146,  147,  148,
   80,  149,  150,  151,  774,  152,  776,    1,  781,  782,
  783,  792,  153,  154,  686,  104,  100,   80,  105,  155,
  106,  107,   63,   47,  677,  113,  156,   76,  238,  344,
  237,  254,  117,  117,  562,  121,  117,  117,  246,  117,
   90,  255,  683,   36,   37,  768,   38,   39,   40,   41,
   42,   43,  117,  117,   48,   18,  482,  761,  454,  780,
  682,  455,  563,   19,  785,  786,  787,  742,    0,    0,
    0,    0,    0,   20,    0,    0,    0,  117,    0,    0,
    0,    0,    0,  484,    0,   49,   46,    0,  486,  712,
  713,  489,  490,    0,  492,  493,    0,  495,  496,    0,
  498,  499,  500,  501,  502,    0,  157,  505,    0,  507,
  508,  509,  510,    0,    0,    0,    0,    0,    0,    0,
  158,  159,  160,   81,  194,    0,    0,  194,    0,    0,
    0,    0,    0,    0,   80,   80,   80,    0,   80,   80,
   80,    0,   80,    0,    0,  194,  750,    0,    0,   80,
   80,  138,    0,    0,   49,    0,   80,   21,   21,    0,
    0,   21,   21,   80,   21,    0,  538,  146,  147,  148,
   47,  149,  150,  151,    0,  152,  194,   21,   21,  118,
  118,    0,    0,  118,  118,    0,  118,    0,    0,  155,
    0,    0,    0,  777,  778,  779,  156,    0,    0,  118,
  118,   48,   21,    0,    0,  788,  571,    0,  194,    0,
  574,    0,    0,  577,    0,    0,  580,   80,    0,    0,
    0,    0,  586,  587,  118,  589,    0,   22,    0,    0,
  119,  119,    0,   46,  119,  119,   23,  119,    0,    0,
    0,    0,    0,   80,   24,   25,   26,   27,   28,    0,
  119,  119,  617,  618,  619,   29,    0,   80,   80,   80,
   30,   31,   32,   33,   34,   35,   36,   37,    0,   38,
   39,   40,   41,   42,   43,  119,    0,    0,    0,    0,
    0,    0,    0,    0,   44,   45,    0,    0,    0,  644,
    0,  646,  647,    0,  649,  650,    0,  652,  653,    0,
  655,  656,  657,  658,  659,    0,    0,  662,   80,  664,
  665,  666,  667,    0,    0,    0,    0,    0,  672,    0,
    0,    0,    0,   18,   18,    0,    0,   18,   18,    0,
   18,   19,   19,    0,    0,   19,   19,    0,   19,    0,
    0,   20,   20,   18,   18,   20,   20,    0,   20,    0,
    0,   19,   19,    0,    0,    0,  194,  194,    0,   89,
    0,   20,   20,    0,  705,    0,    0,  706,   18,    0,
  707,    0,    0,  708,   22,    0,   19,   49,    0,  709,
  710,    0,  711,   23,    0,    0,   20,    0,  136,    0,
    0,   24,   25,   26,   27,   28,    0,    0,  722,  723,
  724,    0,   80,  137,    0,  728,  194,  194,  194,    0,
    0,    0,    0,    0,    0,  194,    0,  194,    0,    0,
  194,  194,  194,  194,  194,  194,  194,  194,  194,  194,
    0,  194,  194,    0,  194,  194,  194,  194,  194,  194,
  194,    0,    0,  194,  194,  194,  194,    0,  760,  194,
  309,  309,    0,  194,  194,  194,  194,  194,  194,  194,
  194,  194,  194,  194,  194,  194,    0,  194,    0,    0,
    0,    0,    0,    0,    0,  146,  147,  148,  194,  149,
  150,  151,    0,  152,    0,    0,    0,    0,    0,  194,
  194,  194,  194,   80,  353,    0,    0,  155,    0,    0,
  309,  309,  309,    0,  156,    0,    0,    0,    0,  309,
    0,  309,    0,    0,  309,  309,  309,  309,  309,  309,
  309,  309,  309,  309,    0,  309,  309,    0,  309,  309,
  309,  309,  309,  309,  309,    0,    0,  309,  309,  309,
  309,  314,  314,  309,    0,    0,    0,  309,  309,  309,
  309,  309,    0,  309,  309,  309,  309,  309,  309,  309,
    0,  309,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  309,    0,  146,  147,  148,    0,  149,  150,
  151,    0,  152,  309,  309,  309,  309,   80,    0,    0,
    0,  314,  314,  314,    0,    0,  155,    0,    0,    0,
  314,    0,  314,  156,    0,  314,  314,  314,  314,  314,
  314,  314,  314,  314,  314,    0,  314,  314,    0,  314,
  314,  314,  314,  314,  314,  314,    0,    0,  314,  314,
  314,  314,    0,    0,  314,  299,  299,    0,  314,  314,
  314,  314,  314,    0,  314,  314,  314,  314,  314,  314,
  314,    0,  314,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  314,  418,  419,  420,  421,  422,  423,
  424,  425,  426,  427,  314,  314,  314,  314,   80,    0,
    0,    0,    0,    0,    0,  299,  299,  299,    0,    0,
    0,    0,    0,    0,  299,    0,  299,    0,    0,  299,
  299,  299,  299,  299,  299,  299,  299,  299,  299,    0,
  299,  299,    0,  299,  299,  299,  299,  299,  299,  299,
    0,    0,  299,  299,  299,  299,  280,  280,  299,    0,
    0,    0,  299,  299,  299,  299,  299,    0,  299,  299,
  299,  299,  299,  299,  299,    0,  299,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  299,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  299,  299,
  299,  299,   80,    0,    0,    0,  280,  280,  280,    0,
    0,    0,    0,    0,    0,  280,    0,  280,    0,    0,
  280,  280,  280,  280,  280,  280,  280,  280,  280,  280,
    0,  280,  280,    0,  280,  280,  280,  280,  280,  280,
  280,    0,    0,  280,  280,  280,  280,    0,    0,  280,
  277,  277,    0,  280,  280,  280,  280,  280,    0,  280,
  280,  280,  280,  280,  280,  280,    0,  280,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  280,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  280,
  280,  280,  280,   80,    0,    0,    0,    0,    0,    0,
  277,  277,  277,    0,    0,    0,    0,    0,    0,  277,
    0,  277,    0,    0,  277,  277,  277,  277,  277,  277,
  277,  277,  277,  277,    0,  277,  277,    0,  277,  277,
  277,  277,  277,  277,  277,    0,    0,  277,  277,  277,
  277,  278,  278,  277,    0,    0,    0,  277,  277,  277,
  277,  277,    0,  277,  277,  277,  277,  277,  277,  277,
    0,  277,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  277,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  277,  277,  277,  277,   80,    0,    0,
    0,  278,  278,  278,    0,    0,    0,    0,    0,    0,
  278,    0,  278,    0,    0,  278,  278,  278,  278,  278,
  278,  278,  278,  278,  278,    0,  278,  278,    0,  278,
  278,  278,  278,  278,  278,  278,    0,    0,  278,  278,
  278,  278,    0,    0,  278,  279,  279,    0,  278,  278,
  278,  278,  278,    0,  278,  278,  278,  278,  278,  278,
  278,    0,  278,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  278,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  278,  278,  278,  278,   80,    0,
    0,    0,    0,    0,    0,  279,  279,  279,    0,    0,
    0,    0,    0,    0,  279,    0,  279,    0,    0,  279,
  279,  279,  279,  279,  279,  279,  279,  279,  279,    0,
  279,  279,    0,  279,  279,  279,  279,  279,  279,  279,
    0,    0,  279,  279,  279,  279,  311,  311,  279,    0,
    0,    0,  279,  279,  279,  279,  279,    0,  279,  279,
  279,  279,  279,  279,  279,    0,  279,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  279,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  279,  279,
  279,  279,   80,    0,    0,    0,  311,  311,  311,    0,
    0,    0,    0,    0,    0,  311,    0,  311,    0,    0,
  311,  311,  311,  311,  311,  311,  311,  311,  311,  311,
    0,  311,  311,    0,  311,  311,  311,  311,  311,  311,
  311,    0,    0,  311,  311,  311,  311,    0,    0,  311,
  303,  303,    0,  311,  311,  311,  311,  311,    0,  311,
  311,  311,  311,  311,  311,  311,    0,  311,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  311,
  311,  311,  311,   80,    0,    0,    0,    0,    0,    0,
  303,  303,  303,    0,    0,    0,    0,    0,    0,  303,
    0,  303,    0,    0,  303,  303,  303,  303,  303,  303,
  303,  303,  303,  303,    0,  303,  303,    0,  303,  303,
  303,  303,  303,  303,  303,    0,    0,  303,  303,  303,
  303,  295,  295,  303,    0,    0,    0,  303,  303,  303,
  303,  303,    0,  303,  303,  303,  303,  303,  303,  303,
    0,  303,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  303,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  303,  303,  303,  303,    0,    0,    0,
    0,  295,  295,  295,    0,    0,    0,    0,    0,    0,
  295,    0,  295,    0,    0,  295,  295,  295,  295,  295,
  295,  295,  295,  295,  295,    0,  295,  295,   25,  295,
  295,  295,  295,  295,  295,  295,    0,    0,  295,  295,
  295,  295,    0,    0,  295,  286,  286,    0,  295,  295,
  295,  295,  295,    0,  295,  295,  295,  295,  295,  295,
  295,    0,  295,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  295,    0,    0,  162,    0,    0,    0,
    0,    0,    0,    0,  295,  295,  295,  295,    0,    0,
    0,    0,    0,    0,    0,  286,  286,  286,    0,    0,
    0,    0,    0,    0,  286,    0,  286,  163,    0,  286,
  286,  286,  286,  286,  286,  286,  286,  286,  286,    0,
  286,  286,    0,  286,  286,  286,  286,  286,  286,  286,
   47,    0,  286,  286,  286,  286,  259,  259,  286,  161,
    0,    0,  286,  286,  286,  286,  286,    0,  286,  286,
  286,  286,  286,  286,  286,    0,  286,    0,    0,    0,
    0,   48,    0,    0,    0,    0,    0,  286,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,  286,  286,
  286,  286,    0,    0,    0,    0,  259,  259,  259,    0,
   47,    0,    0,   46,    0,  259,    0,  259,    0,    0,
  259,  259,  259,  259,  259,  259,  259,  259,  259,  259,
    0,  259,  259,    0,  259,  259,  259,  259,  259,  259,
  259,   48,    0,  259,  259,  259,  259,    0,    0,  259,
    0,  111,    0,  259,  259,  259,  259,  259,    0,  259,
  259,  259,  259,  259,  259,  259,    0,  259,    0,    0,
    0,    0,    0,   46,    0,    0,    0,    0,  259,    0,
    0,    0,  111,    0,    0,    0,    0,    0,    0,  259,
  259,  259,  259,  146,  147,  148,    0,  149,  150,  151,
    0,  152,  112,    0,    0,    0,    0,   47,  153,  154,
    0,    0,    0,    0,  111,  155,   25,   25,   24,    0,
   25,   25,  156,   25,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  112,    0,    0,   25,   25,   48,   36,
   37,    0,   38,   39,   40,   41,   42,   43,    0,    0,
    0,    0,    0,    0,   22,    0,    0,  162,   89,    0,
  229,   25,    0,   23,    0,  112,    0,    0,    0,    0,
   46,   24,   25,   26,   27,   28,  162,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  163,    0,
    0,    0,   35,   36,   37,    0,   38,   39,   40,   41,
   42,   43,  157,   89,    0,  330,    0,  163,    0,    0,
    0,    0,    0,    0,   22,    0,  158,  159,  160,    0,
  161,  162,   89,   23,    0,    0,    0,    0,    0,    0,
    0,   24,   25,   26,   27,   28,    0,    0,    0,  161,
  162,    0,    0,    0,    0,    0,    0,   47,    0,    0,
   49,    0,  163,   36,   37,  111,   38,   39,   40,   41,
   42,   43,   22,   22,  111,    0,   22,   22,  559,   22,
    0,  163,  111,  111,  111,  111,  111,    0,   48,    0,
    0,    0,   22,   22,  161,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  111,  111,    0,  111,  111,  111,
  111,  111,  111,  161,   49,    0,  112,   22,    0,    0,
   46,   22,    0,    0,  769,  112,    0,    0,    0,    0,
   23,    0,    0,  112,  112,  112,  112,  112,   24,   25,
   26,   27,   28,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  112,  112,    0,  112,  112,
  112,  112,  112,  112,  146,  147,  148,  452,  149,  150,
  151,    0,  152,    0,   49,    0,    0,    0,    0,  153,
  154,    0,    0,  146,  147,  148,  155,  149,  150,  151,
    0,  152,    0,  156,    0,    0,   24,   24,  153,  154,
   24,   24,    0,   24,    0,  155,    0,  387,    0,    0,
    0,    0,  156,    0,    0,  111,   24,   24,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  146,  147,
  148,    0,  149,  150,  151,    0,  152,    0,    0,    0,
    0,   24,    0,  153,  154,    0,    0,  146,  147,  148,
  155,  149,  150,  151,    0,  152,    0,  156,  479,    0,
    0,   22,  153,  154,    0,    0,  112,    0,    0,  155,
   23,    0,    0,  157,  197,    0,  156,    0,   24,   25,
   26,   27,   28,    0,    0,    0,    0,  158,  159,  160,
  198,    0,  157,  126,  146,  147,  148,    0,  149,  150,
  151,    0,  152,    0,    0,    0,  158,  159,  160,    0,
    0,    0,   47,  353,    0,  261,  155,    0,    0,    0,
    0,    0,    0,  156,  199,  200,    0,    0,  201,  202,
  203,  204,  205,  206,    0,    0,    0,  157,    0,    0,
  146,  147,  148,   48,  149,  150,  151,  389,  152,  548,
    0,  158,  159,  160,    0,    0,  157,    0,    0,  353,
    0,    0,  155,    0,    0,  262,  263,  264,    0,  156,
  158,  159,  160,    0,  265,   46,  266,    0,    0,  267,
  268,  269,  270,  271,  272,  273,  274,  275,  276,    0,
  277,  278,    0,  279,  280,  281,  282,  283,  284,  285,
    0,    0,  286,  287,  288,  289,  261,    0,  290,    0,
    0,    0,  291,  292,  293,  294,  295,    0,  296,  297,
  298,  299,  300,  301,  302,    0,  303,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  304,    0,    0,
  565,    0,    0,    0,    0,    0,    0,    0,  305,  306,
  307,  308,    0,    0,    0,    0,  262,  263,  264,    0,
    0,    0,    0,    0,    0,  265,    0,  266,    0,    0,
  267,  268,  269,  270,  271,  272,  273,  274,  275,  276,
    0,  277,  278,    0,  279,  280,  281,  282,  283,  284,
  285,    0,    0,  286,  287,  288,  289,  261,    0,  290,
    0,    0,    0,  291,  292,  293,  294,  295,    0,  296,
  297,  298,  299,  300,  301,  302,   22,  303,    0,    0,
    0,    0,    0,    0,    0,   23,    0,    0,  304,    0,
    0,  641,    0,   24,   25,   26,   27,   28,    0,  305,
  306,  307,  308,    0,    0,  125,    0,  262,  263,  264,
    0,    0,    0,    0,    0,    0,  265,    0,  266,    0,
    0,  267,  268,  269,  270,  271,  272,  273,  274,  275,
  276,    0,  277,  278,    0,  279,  280,  281,  282,  283,
  284,  285,    0,    0,  286,  287,  288,  289,  261,    0,
  290,    0,    0,    0,  291,  292,  293,  294,  295,    0,
  296,  297,  298,  299,  300,  301,  302,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  304,
    0,    0,  700,    0,    0,    0,    0,    0,    0,    0,
  305,  306,  307,  308,    0,    0,    0,    0,  262,  263,
  264,    0,    0,    0,    0,    0,    0,  265,    0,  266,
    0,    0,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,    0,  277,  278,    0,  279,  280,  281,  282,
  283,  284,  285,    0,    0,  286,  287,  288,  289,  261,
    0,  290,    0,    0,    0,  291,  292,  293,  294,  295,
    0,  296,  297,  298,  299,  300,  301,  302,    0,  303,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,  206,    0,    0,    0,    0,    0,    0,
    0,  305,  306,  307,  308,    0,    0,    0,    0,  262,
  263,  264,    0,    0,    0,    0,    0,    0,  265,    0,
  266,    0,    0,  267,  268,  269,  270,  271,  272,  273,
  274,  275,  276,    0,  277,  278,    0,  279,  280,  281,
  282,  283,  284,  285,    0,    0,  286,  287,  288,  289,
  261,    0,  290,    0,    0,    0,  291,  292,  293,  294,
  295,    0,  296,  297,  298,  299,  300,  301,  302,    0,
  303,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  304,    0,    0,  204,    0,    0,    0,    0,    0,
    0,    0,  305,  306,  307,  308,    0,    0,    0,    0,
  262,  263,  264,    0,    0,    0,    0,    0,    0,  265,
    0,  266,    0,    0,  267,  268,  269,  270,  271,  272,
  273,  274,  275,  276,    0,  277,  278,    0,  279,  280,
  281,  282,  283,  284,  285,    0,    0,  286,  287,  288,
  289,  206,    0,  290,    0,    0,    0,  291,  292,  293,
  294,  295,    0,  296,  297,  298,  299,  300,  301,  302,
    0,  303,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  304,    0,    0,  207,    0,    0,    0,    0,
    0,    0,    0,  305,  306,  307,  308,    0,    0,    0,
    0,  206,  206,  206,    0,    0,    0,    0,    0,    0,
  206,    0,  206,    0,    0,  206,  206,  206,  206,  206,
  206,  206,  206,  206,  206,    0,  206,  206,    0,  206,
  206,  206,  206,  206,  206,  206,    0,    0,  206,  206,
  206,  206,  204,    0,  206,    0,    0,    0,  206,  206,
  206,  206,  206,    0,  206,  206,  206,  206,  206,  206,
  206,    0,  206,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  206,    0,    0,  205,    0,    0,    0,
    0,    0,    0,    0,  206,  206,  206,  206,    0,    0,
    0,    0,  204,  204,  204,    0,    0,    0,    0,    0,
    0,  204,    0,  204,    0,    0,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,    0,  204,  204,    0,
  204,  204,  204,  204,  204,  204,  204,    0,    0,  204,
  204,  204,  204,  207,    0,  204,    0,    0,    0,  204,
  204,  204,  204,  204,    0,  204,  204,  204,  204,  204,
  204,  204,    0,  204,    0,    0,    0,    0,    0,    0,
    0,    0,   47,    0,  204,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  204,  204,  204,  204,    0,
    0,    0,    0,  207,  207,  207,    0,   47,    0,    0,
    0,    0,  207,   48,  207,    0,    0,  207,  207,  207,
  207,  207,  207,  207,  207,  207,  207,    0,  207,  207,
    0,  207,  207,  207,  207,  207,  207,  207,   48,    0,
  207,  207,  207,  207,  205,   46,  207,    0,    0,  253,
  207,  207,  207,  207,  207,    0,  207,  207,  207,  207,
  207,  207,  207,    0,  207,    0,   47,    0,    0,    0,
   46,    0,    0,    0,    0,  207,    0,    0,    0,    0,
  685,    0,    0,    0,    0,    0,  207,  207,  207,  207,
    0,    0,    0,    0,  205,  205,  205,   48,    0,   47,
    0,    0,    0,  205,    0,  205,    0,    0,  205,  205,
  205,  205,  205,  205,  205,  205,  205,  205,    0,  205,
  205,    0,  205,  205,  205,  205,  205,  205,  205,   46,
   48,  205,  205,  205,  205,    0,   47,  205,    0,    0,
    0,  205,  205,  205,  205,  205,    0,  205,  205,  205,
  205,  205,  205,  205,    0,  205,    0,    0,   47,    0,
    0,    0,   46,   47,    0,    0,  205,   48,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  205,  205,  205,
  205,    0,    0,    0,  197,    0,   22,    0,    0,   48,
    0,    0,    0,    0,   48,   23,   47,    0,    0,   46,
  198,    0,    0,   24,   25,   26,   27,   28,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,    0,
   23,   46,    0,    0,    0,    0,   46,   48,   24,   25,
   26,   27,   28,   47,  199,  200,    0,    0,  201,  202,
  203,  204,  205,  206,    0,    0,    0,    0,    0,    0,
    0,    0,  398,    0,    0,    0,    0,  367,  368,   46,
    0,    0,   47,    0,   48,    0,    0,   47,    0,    0,
  177,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,   24,   25,   26,
   27,   28,    0,   48,    0,    0,   46,    0,   48,    0,
   47,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,  400,   23,    0,    0,   47,    0,  684,    0,    0,
   24,   25,   26,   27,   28,   46,    0,  180,    0,    0,
   46,   48,    0,    0,    0,    0,    0,    0,    0,   47,
   22,    0,    0,    0,   47,    0,   48,    0,    0,   23,
    0,    0,    0,    0,  136,    0,    0,   24,   25,   26,
   27,   28,   22,   46,    0,   81,    0,  177,  178,  137,
   48,   23,  741,    0,    0,   48,   23,  179,   46,   24,
   25,   26,   27,   28,   24,   25,   26,   27,   28,    0,
    0,  125,    0,  146,  147,  148,  332,  149,  150,  151,
   22,  152,   46,    0,    0,    0,    0,   46,    0,   23,
  333,   47,  334,    0,    0,  155,    0,   24,   25,   26,
   27,   28,  156,    0,    0,    0,    0,    0,    0,    0,
  146,  147,  148,  332,  149,  150,  151,   22,  152,   47,
    0,    0,   48,    0,  429,  430,   23,  561,    0,  334,
    0,    0,  155,    0,   24,   25,   26,   27,   28,  156,
    0,    0,    0,    0,    0,   71,  177,  178,    0,    0,
   48,  177,  347,    0,   46,   23,  179,    0,    0,    0,
   23,  348,    0,   24,   25,   26,   27,   28,   24,   25,
   26,   27,   28,   72,    0,    0,    0,    0,    0,    0,
    0,    0,   46,    0,   22,    0,    0,    0,    0,    0,
    0,    0,    0,   23,    0,    0,    0,    0,    0,   22,
    0,   24,   25,   26,   27,   28,    0,    0,   23,  726,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,   22,    0,    0,    0,    0,   22,    0,
    0,    0,   23,    0,    0,    0,    0,   23,    0,    0,
   24,   25,   26,   27,   28,   24,   25,   26,   27,   28,
  431,  432,  433,  434,    0,    0,    0,    0,    0,  435,
  436,  437,  438,  439,  440,  441,  442,  443,  444,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   71,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   22,   71,    0,    0,    0,
    0,    0,    0,    0,   23,    0,    0,    0,   72,  684,
    0,    0,   24,   25,   26,   27,   28,    0,  294,  294,
    0,    0,    0,  177,   72,    0,    0,    0,    0,    0,
   71,   71,   23,    0,   71,   71,   71,   71,   71,   71,
   24,   25,   26,   27,   28,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   72,   72,
    0,    0,   72,   72,   72,   72,   72,   72,  294,  294,
  294,    0,    0,    0,    0,    0,    0,  294,    0,  294,
    0,    0,  294,  294,  294,  294,  294,  294,  294,  294,
  294,  294,    0,  294,  294,    0,  294,  294,  294,  294,
  294,  294,  294,    0,    0,  294,  294,  294,  294,  260,
  260,  294,    0,    0,    0,  294,  294,  294,  294,  294,
    0,  294,  294,  294,  294,  294,  294,  294,    0,  294,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  294,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  294,  294,  294,  294,    0,    0,    0,    0,  260,
  260,  260,    0,    0,    0,    0,    0,    0,  260,    0,
  260,    0,    0,  260,  260,  260,  260,  260,  260,  260,
  260,  260,  260,    0,  260,  260,    0,  260,  260,  260,
  260,  260,  260,  260,    0,    0,  260,  260,  260,  260,
  263,  263,  260,    0,    0,    0,  260,  260,  260,  260,
  260,    0,  260,  260,  260,  260,  260,  260,  260,    0,
  260,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  260,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  260,  260,  260,  260,    0,    0,    0,    0,
  263,  263,  263,    0,    0,    0,    0,    0,    0,  263,
    0,  263,    0,    0,  263,  263,  263,  263,  263,  263,
  263,  263,  263,  263,    0,  263,  263,    0,  263,  263,
  263,  263,  263,  263,  263,    0,    0,  263,  263,  263,
  263,  268,  268,  263,    0,    0,    0,  263,  263,  263,
  263,  263,    0,  263,  263,  263,  263,  263,  263,  263,
    0,  263,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  263,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  263,  263,  263,  263,    0,    0,    0,
    0,  268,  268,  268,    0,    0,    0,    0,    0,    0,
  268,    0,  268,    0,    0,  268,  268,  268,  268,  268,
  268,  268,  268,  268,  268,    0,  268,  268,    0,  268,
  268,  268,  268,  268,  268,  268,    0,    0,  268,  268,
  268,  268,  282,  282,  268,    0,    0,    0,  268,  268,
  268,  268,  268,    0,  268,  268,  268,  268,  268,  268,
  268,    0,  268,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  268,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  268,  268,  268,  268,    0,    0,
    0,    0,  282,  282,  282,    0,    0,    0,    0,    0,
    0,  282,    0,  282,    0,    0,  282,  282,  282,  282,
  282,  282,  282,  282,  282,  282,    0,  282,  282,    0,
  282,  282,  282,  282,  282,  282,  282,    0,    0,  282,
  282,  282,  282,  264,  264,  282,    0,    0,    0,  282,
  282,  282,  282,  282,    0,  282,  282,  282,  282,  282,
  282,  282,    0,  282,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  282,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  282,  282,  282,  282,    0,
    0,    0,    0,  264,  264,  264,    0,    0,    0,    0,
    0,    0,  264,    0,  264,    0,    0,  264,  264,  264,
  264,  264,  264,  264,  264,  264,  264,    0,  264,  264,
    0,  264,  264,  264,  264,  264,  264,  264,    0,    0,
  264,  264,  264,  264,  265,  265,  264,    0,    0,    0,
  264,  264,  264,  264,  264,    0,  264,  264,  264,  264,
  264,  264,  264,    0,  264,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  264,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  264,  264,  264,  264,
    0,    0,    0,    0,  265,  265,  265,    0,    0,    0,
    0,    0,    0,  265,    0,  265,    0,    0,  265,  265,
  265,  265,  265,  265,  265,  265,  265,  265,    0,  265,
  265,    0,  265,  265,  265,  265,  265,  265,  265,    0,
    0,  265,  265,  265,  265,  266,  266,  265,    0,    0,
    0,  265,  265,  265,  265,  265,    0,  265,  265,  265,
  265,  265,  265,  265,    0,  265,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  265,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  265,  265,  265,
  265,    0,    0,    0,    0,  266,  266,  266,    0,    0,
    0,    0,    0,    0,  266,    0,  266,    0,    0,  266,
  266,  266,  266,  266,  266,  266,  266,  266,  266,    0,
  266,  266,    0,  266,  266,  266,  266,  266,  266,  266,
    0,    0,  266,  266,  266,  266,  283,  283,  266,    0,
    0,    0,  266,  266,  266,  266,  266,    0,  266,  266,
  266,  266,  266,  266,  266,    0,  266,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  266,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  266,  266,
  266,  266,    0,    0,    0,    0,  283,  283,  283,    0,
    0,    0,    0,    0,    0,  283,    0,  283,    0,    0,
  283,  283,  283,  283,  283,  283,  283,  283,  283,  283,
    0,  283,  283,    0,  283,  283,  283,  283,  283,  283,
  283,    0,    0,  283,  283,  283,  283,  267,  267,  283,
    0,    0,    0,  283,  283,  283,  283,  283,    0,  283,
  283,  283,  283,  283,  283,  283,    0,  283,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  283,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  283,
  283,  283,  283,    0,    0,    0,    0,  267,  267,  267,
    0,    0,    0,    0,    0,    0,  267,    0,  267,    0,
    0,  267,  267,  267,  267,  267,  267,  267,  267,  267,
  267,    0,  267,  267,    0,  267,  267,  267,  267,  267,
  267,  267,    0,    0,  267,  267,  267,  267,  261,    0,
  267,    0,    0,    0,  267,  267,  267,  267,  267,    0,
  267,  267,  267,  267,  267,  267,  267,    0,  267,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  267,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  267,  267,  267,  267,    0,    0,    0,    0,  262,  263,
  264,    0,    0,    0,    0,    0,    0,  265,    0,  266,
    0,    0,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,    0,  277,  278,    0,  279,  280,  281,  282,
  283,  284,  285,    0,    0,  286,  287,  288,  289,  210,
    0,  290,    0,    0,    0,  291,  292,  293,  294,  295,
    0,  296,  297,  298,  299,  300,  301,  302,    0,  303,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  305,  306,  307,  308,    0,    0,    0,    0,  210,
  210,  210,    0,    0,    0,    0,    0,    0,  210,    0,
  210,    0,    0,  210,  210,  210,  210,  210,  210,  210,
  210,  210,  210,    0,  210,  210,    0,  210,  210,  210,
  210,  210,  210,  210,    0,    0,  210,  210,  210,  210,
  211,    0,  210,    0,    0,    0,  210,  210,  210,  210,
  210,    0,  210,  210,  210,  210,  210,  210,  210,    0,
  210,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  210,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  210,  210,  210,  210,    0,    0,    0,    0,
  211,  211,  211,    0,    0,    0,    0,    0,    0,  211,
    0,  211,    0,    0,  211,  211,  211,  211,  211,  211,
  211,  211,  211,  211,    0,  211,  211,    0,  211,  211,
  211,  211,  211,  211,  211,    0,    0,  211,  211,  211,
  211,  212,    0,  211,    0,    0,    0,  211,  211,  211,
  211,  211,    0,  211,  211,  211,  211,  211,  211,  211,
    0,  211,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  211,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  211,  211,  211,  211,    0,    0,    0,
    0,  212,  212,  212,    0,    0,    0,    0,    0,    0,
  212,    0,  212,    0,    0,  212,  212,  212,  212,  212,
  212,  212,  212,  212,  212,    0,  212,  212,    0,  212,
  212,  212,  212,  212,  212,  212,    0,    0,  212,  212,
  212,  212,  213,    0,  212,    0,    0,    0,  212,  212,
  212,  212,  212,    0,  212,  212,  212,  212,  212,  212,
  212,    0,  212,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  212,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  212,  212,  212,  212,    0,    0,
    0,    0,  213,  213,  213,    0,    0,    0,    0,    0,
    0,  213,    0,  213,    0,    0,  213,  213,  213,  213,
  213,  213,  213,  213,  213,  213,    0,  213,  213,    0,
  213,  213,  213,  213,  213,  213,  213,    0,    0,  213,
  213,  213,  213,    0,    0,  213,    0,    0,    0,  213,
  213,  213,  213,  213,    0,  213,  213,  213,  213,  213,
  213,  213,    0,  213,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  213,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  213,  213,  213,  213,  266,
    0,    0,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,    0,  277,  278,    0,  279,  280,  281,  282,
  283,  284,  285,    0,    0,  286,  287,  288,  289,    0,
    0,  290,    0,    0,    0,  291,  292,  293,  294,  295,
    0,  296,  297,  298,  299,  300,  301,  302,    0,  303,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  305,  306,  307,  308,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   58,   52,    6,   33,  257,  472,    6,    7,   40,
  133,  232,  123,  234,  123,   60,  144,   40,   40,  123,
  143,  123,   40,  123,  123,   44,  485,  309,   44,   44,
   44,  252,  123,  276,   61,  541,  542,  280,  544,   46,
   90,  643,   42,  312,  212,   40,   91,   54,   55,   44,
   98,   58,   55,  697,   61,   54,   55,   44,   61,   58,
   44,   33,   61,   42,  316,   42,   73,  195,   60,  192,
  518,  194,  624,  625,  718,  627,  358,   93,  123,   86,
  266,   61,  139,  131,  212,   92,  134,   42,   95,   44,
   97,   91,  260,   40,   97,  102,   95,   44,   97,  285,
  107,  703,  109,  294,  295,   41,  125,  359,   44,  568,
  125,  125,  119,  120,  335,  122,   93,  290,  291,  247,
  626,  249,  628,  629,  367,   42,   42,  250,   44,   42,
  125,  123,   74,   44,   76,  302,  270,  271,  125,  312,
  692,  125,  694,  695,  312,  193,  314,   61,  792,   62,
  598,   62,   61,  601,  321,  322,   41,  370,  257,   44,
  167,  374,  169,  274,  377,  274,  108,   42,  110,   44,
  274,  272,  274,  386,  274,  268,  269,  459,  125,  272,
  273,   60,  275,  274,  191,  737,   41,   42,  124,  357,
  696,  268,  260,  270,   41,  288,  289,   44,  480,   61,
  277,   40,   41,  312,  211,   44,  209,   42,   61,   44,
  290,  291,   91,    0,  294,  295,   42,  280,   44,  264,
  313,  312,   42,  351,   44,  232,   40,  350,  273,  481,
   40,   41,   41,  257,   44,   44,  281,  282,  283,  284,
  285,  272,  701,   41,  123,  274,   44,   42,  117,  272,
  272,  472,   40,  474,  272,  274,  477,  307,  367,  266,
  267,  268,  269,  270,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  367,  290,  291,  286,
  287,  740,   61,  290,  566,  751,  454,  274,   41,   41,
   42,   44,  161,  162,  163,  287,    0,  465,  305,  306,
  307,   40,  274,   41,  307,  277,   44,  176,  307,   42,
  317,   44,  564,   41,  482,   41,   44,   41,   44,   41,
   44,  260,   44,  368,  303,  304,  260,  306,  307,  308,
  309,  310,  311,   41,   41,   41,   44,   44,   44,   40,
  257,  258,  259,  123,  261,  262,  263,  475,  265,  267,
  219,  220,  221,   41,   42,  272,  273,   40,  640,   75,
   76,  267,  279,  370,   41,   42,  257,  374,  123,  286,
  377,  302,  241,  380,   41,   42,  123,   60,   40,  386,
  387,   41,  389,   41,   42,  264,  554,  103,  556,  162,
  163,  398,  123,  262,  273,  264,   40,  404,  368,   40,
  312,  451,  281,  282,  283,  284,  285,   40,   91,   44,
   58,   58,   58,   62,  283,  284,  285,  133,   61,  288,
   40,  428,  291,  292,  293,  294,  295,  296,  297,  298,
  299,  300,  301,  302,  274,  414,  257,   40,  445,  367,
  123,   60,  449,  450,  451,   44,  367,  450,  451,  366,
  449,  450,  451,  322,  461,  462,  463,   44,  274,  260,
  257,   61,  274,  380,  381,  382,  320,  474,  636,  274,
  638,  350,   91,  408,    0,  375,  375,  375,  367,  257,
  196,  268,  269,   44,  367,  272,  273,  257,  275,  367,
  303,  304,   42,  306,  307,  308,  309,  310,  311,   44,
  623,  288,  289,   40,  123,  273,  727,  124,   44,  516,
   60,  518,   44,   44,  521,  522,  523,  524,  525,  526,
  527,  528,  529,  530,  531,  532,  313,   44,   44,  375,
  751,  400,  248,  375,  375,  106,  543,  375,  545,  546,
  274,   91,  375,  546,  543,  375,  545,  546,  290,  291,
  375,  375,  294,  295,  296,  297,  298,  299,  300,  301,
  375,  689,  257,  367,  268,  269,  257,  690,  272,  273,
  693,  275,  375,  123,  257,  258,  259,  257,  261,  262,
  263,  375,  265,  375,  288,  289,  367,   44,  266,  272,
  273,  598,   44,   44,  601,  166,  279,  168,   44,    0,
   44,  414,   44,  286,  732,   44,  734,   44,   44,  313,
   44,  182,  735,   44,   44,  622,   44,   44,   44,   44,
   44,    0,   44,  630,  367,   44,   44,   40,   44,  686,
   40,  630,  257,  257,  762,   91,  643,   44,   44,   44,
   44,   44,  511,  512,  513,  264,   44,   60,   44,   44,
   44,  520,  257,   44,  273,   44,   44,  228,   44,   44,
  231,   44,  281,  282,  283,  284,  285,   44,   91,   44,
   44,  367,    0,  292,   44,  367,   44,  684,   91,   44,
  549,   44,  367,  366,  303,  304,  367,  306,  307,  308,
  309,  310,  311,  257,  257,   44,  703,  380,  381,  382,
   93,   40,  257,  257,  368,   40,   40,  257,  258,  259,
  123,  261,  262,  263,   40,  265,   40,    0,  375,  375,
  375,   44,  272,  273,  731,  272,  729,   40,  272,  279,
  272,  272,   10,   60,  603,   77,  286,   19,  176,  236,
  175,  207,  268,  269,  474,   86,  272,  273,  191,  275,
   52,  211,  621,  303,  304,  748,  306,  307,  308,  309,
  310,  311,  288,  289,   91,    0,  361,  731,  311,  776,
  620,  311,  477,    0,  781,  782,  783,  703,   -1,   -1,
   -1,   -1,   -1,    0,   -1,   -1,   -1,  313,   -1,   -1,
   -1,   -1,   -1,  364,   -1,  414,  123,   -1,  369,  668,
  669,  372,  373,   -1,  375,  376,   -1,  378,  379,   -1,
  381,  382,  383,  384,  385,   -1,  366,  388,   -1,  390,
  391,  392,  393,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  380,  381,  382,   40,   41,   -1,   -1,   44,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   62,  725,   -1,   -1,  272,
  273,   41,   -1,   -1,  414,   -1,  279,  268,  269,   -1,
   -1,  272,  273,  286,  275,   -1,  447,  257,  258,  259,
   60,  261,  262,  263,   -1,  265,   93,  288,  289,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,  279,
   -1,   -1,   -1,  772,  773,  774,  286,   -1,   -1,  288,
  289,   91,  313,   -1,   -1,  784,  487,   -1,  125,   -1,
  491,   -1,   -1,  494,   -1,   -1,  497,   40,   -1,   -1,
   -1,   -1,  503,  504,  313,  506,   -1,  264,   -1,   -1,
  268,  269,   -1,  123,  272,  273,  273,  275,   -1,   -1,
   -1,   -1,   -1,  366,  281,  282,  283,  284,  285,   -1,
  288,  289,  533,  534,  535,  292,   -1,  380,  381,  382,
  297,  298,  299,  300,  301,  302,  303,  304,   -1,  306,
  307,  308,  309,  310,  311,  313,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  321,  322,   -1,   -1,   -1,  570,
   -1,  572,  573,   -1,  575,  576,   -1,  578,  579,   -1,
  581,  582,  583,  584,  585,   -1,   -1,  588,   40,  590,
  591,  592,  593,   -1,   -1,   -1,   -1,   -1,  599,   -1,
   -1,   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
   -1,  268,  269,  288,  289,  272,  273,   -1,  275,   -1,
   -1,  288,  289,   -1,   -1,   -1,  273,  274,   -1,   42,
   -1,  288,  289,   -1,  645,   -1,   -1,  648,  313,   -1,
  651,   -1,   -1,  654,  264,   -1,  313,  414,   -1,  660,
  661,   -1,  663,  273,   -1,   -1,  313,   -1,  278,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,  679,  680,
  681,   -1,   40,  293,   -1,  686,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,   -1,  729,  366,
  273,  274,   -1,  370,  371,  372,  373,  374,  375,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  395,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,   40,  276,   -1,   -1,  279,   -1,   -1,
  323,  324,  325,   -1,  286,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,  406,  407,  408,  409,   40,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,  279,   -1,   -1,   -1,
  332,   -1,  334,  286,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,   -1,   -1,  366,  273,  274,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,  385,  386,  387,  388,  389,  390,
  391,  392,  393,  394,  406,  407,  408,  409,   40,   -1,
   -1,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,  274,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,
  408,  409,   40,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,   -1,   -1,  366,
  273,  274,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,   40,   -1,   -1,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   40,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,   -1,   -1,  366,  273,  274,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  407,  408,  409,   40,   -1,
   -1,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,  274,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,
  408,  409,   40,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,   -1,   -1,  366,
  273,  274,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,   40,   -1,   -1,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,    0,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,   -1,   -1,  366,  273,  274,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,   -1,   -1,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   91,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   60,   -1,  360,  361,  362,  363,  273,  274,  366,  123,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,    0,   -1,   -1,   -1,  406,  407,
  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,
   60,   -1,   -1,  123,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   91,   -1,  360,  361,  362,  363,   -1,   -1,  366,
   -1,   60,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  395,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   60,   -1,   -1,   -1,   -1,   60,  272,  273,
   -1,   -1,   -1,   -1,  123,  279,  268,  269,    0,   -1,
  272,  273,  286,  275,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,  288,  289,   91,  303,
  304,   -1,  306,  307,  308,  309,  310,  311,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   60,   42,   -1,
   44,  313,   -1,  273,   -1,  123,   -1,   -1,   -1,   -1,
  123,  281,  282,  283,  284,  285,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,
   -1,   -1,  302,  303,  304,   -1,  306,  307,  308,  309,
  310,  311,  366,   42,   -1,   44,   -1,   91,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,  380,  381,  382,   -1,
  123,   60,   42,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,  123,
   60,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,
  414,   -1,   91,  303,  304,  264,  306,  307,  308,  309,
  310,  311,  268,  269,  273,   -1,  272,  273,   41,  275,
   -1,   91,  281,  282,  283,  284,  285,   -1,   91,   -1,
   -1,   -1,  288,  289,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,  308,
  309,  310,  311,  123,  414,   -1,  264,  313,   -1,   -1,
  123,  264,   -1,   -1,   41,  273,   -1,   -1,   -1,   -1,
  273,   -1,   -1,  281,  282,  283,  284,  285,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  303,  304,   -1,  306,  307,
  308,  309,  310,  311,  257,  258,  259,  125,  261,  262,
  263,   -1,  265,   -1,  414,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,  257,  258,  259,  279,  261,  262,  263,
   -1,  265,   -1,  286,   -1,   -1,  268,  269,  272,  273,
  272,  273,   -1,  275,   -1,  279,   -1,  350,   -1,   -1,
   -1,   -1,  286,   -1,   -1,  414,  288,  289,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
   -1,  313,   -1,  272,  273,   -1,   -1,  257,  258,  259,
  279,  261,  262,  263,   -1,  265,   -1,  286,  125,   -1,
   -1,  264,  272,  273,   -1,   -1,  414,   -1,   -1,  279,
  273,   -1,   -1,  366,  260,   -1,  286,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,  380,  381,  382,
  276,   -1,  366,   41,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,  380,  381,  382,   -1,
   -1,   -1,   60,  276,   -1,  273,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  286,  310,  311,   -1,   -1,  314,  315,
  316,  317,  318,  319,   -1,   -1,   -1,  366,   -1,   -1,
  257,  258,  259,   91,  261,  262,  263,  350,  265,  125,
   -1,  380,  381,  382,   -1,   -1,  366,   -1,   -1,  276,
   -1,   -1,  279,   -1,   -1,  323,  324,  325,   -1,  286,
  380,  381,  382,   -1,  332,  123,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,   -1,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,
  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,  273,   -1,  366,
   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,  264,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,  395,   -1,
   -1,  125,   -1,  281,  282,  283,  284,  285,   -1,  406,
  407,  408,  409,   -1,   -1,  293,   -1,  323,  324,  325,
   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,
   -1,  337,  338,  339,  340,  341,  342,  343,  344,  345,
  346,   -1,  348,  349,   -1,  351,  352,  353,  354,  355,
  356,  357,   -1,   -1,  360,  361,  362,  363,  273,   -1,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,  377,  378,  379,  380,  381,  382,   -1,  384,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,
   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,
  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,
   -1,   -1,  337,  338,  339,  340,  341,  342,  343,  344,
  345,  346,   -1,  348,  349,   -1,  351,  352,  353,  354,
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,  273,
   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,
  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,
  334,   -1,   -1,  337,  338,  339,  340,  341,  342,  343,
  344,  345,  346,   -1,  348,  349,   -1,  351,  352,  353,
  354,  355,  356,  357,   -1,   -1,  360,  361,  362,  363,
  273,   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,  377,  378,  379,  380,  381,  382,   -1,
  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  395,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,   -1,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,  273,   -1,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,   -1,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,
   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,   -1,  348,  349,   -1,
  351,  352,  353,  354,  355,  356,  357,   -1,   -1,  360,
  361,  362,  363,  273,   -1,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,  377,  378,  379,  380,
  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,
   -1,   -1,   -1,  323,  324,  325,   -1,   60,   -1,   -1,
   -1,   -1,  332,   91,  334,   -1,   -1,  337,  338,  339,
  340,  341,  342,  343,  344,  345,  346,   -1,  348,  349,
   -1,  351,  352,  353,  354,  355,  356,  357,   91,   -1,
  360,  361,  362,  363,  273,  123,  366,   -1,   -1,  125,
  370,  371,  372,  373,  374,   -1,  376,  377,  378,  379,
  380,  381,  382,   -1,  384,   -1,   60,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,
   41,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,
   -1,   -1,   -1,   -1,  323,  324,  325,   91,   -1,   60,
   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,  123,
   91,  360,  361,  362,  363,   -1,   60,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   60,   -1,
   -1,   -1,  123,   60,   -1,   -1,  395,   91,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,
  409,   -1,   -1,   -1,  260,   -1,  264,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   91,  273,   60,   -1,   -1,  123,
  276,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  123,   -1,   -1,   -1,   -1,  123,   91,  281,  282,
  283,  284,  285,   60,  310,  311,   -1,   -1,  314,  315,
  316,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  305,   -1,   -1,   -1,   -1,  335,  336,  123,
   -1,   -1,   60,   -1,   91,   -1,   -1,   60,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   91,   -1,   -1,  123,   -1,   91,   -1,
   60,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  305,  273,   -1,   -1,   60,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,  123,   -1,  125,   -1,   -1,
  123,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
  264,   -1,   -1,   -1,   60,   -1,   91,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,  264,  123,   -1,  125,   -1,  264,  265,  293,
   91,  273,   93,   -1,   -1,   91,  273,  274,  123,  281,
  282,  283,  284,  285,  281,  282,  283,  284,  285,   -1,
   -1,  293,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,  123,   -1,   -1,   -1,   -1,  123,   -1,  273,
  274,   60,  276,   -1,   -1,  279,   -1,  281,  282,  283,
  284,  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,  260,  261,  262,  263,  264,  265,   60,
   -1,   -1,   91,   -1,  261,  262,  273,  274,   -1,  276,
   -1,   -1,  279,   -1,  281,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,  125,  264,  265,   -1,   -1,
   91,  264,  265,   -1,  123,  273,  274,   -1,   -1,   -1,
  273,  274,   -1,  281,  282,  283,  284,  285,  281,  282,
  283,  284,  285,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,  264,
   -1,  281,  282,  283,  284,  285,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,  264,   -1,   -1,   -1,   -1,  264,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,   -1,   -1,
  281,  282,  283,  284,  285,  281,  282,  283,  284,  285,
  387,  388,  389,  390,   -1,   -1,   -1,   -1,   -1,  396,
  397,  398,  399,  400,  401,  402,  403,  404,  405,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  260,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  276,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,  260,  278,
   -1,   -1,  281,  282,  283,  284,  285,   -1,  273,  274,
   -1,   -1,   -1,  264,  276,   -1,   -1,   -1,   -1,   -1,
  310,  311,  273,   -1,  314,  315,  316,  317,  318,  319,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  310,  311,
   -1,   -1,  314,  315,  316,  317,  318,  319,  323,  324,
  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,
   -1,   -1,  337,  338,  339,  340,  341,  342,  343,  344,
  345,  346,   -1,  348,  349,   -1,  351,  352,  353,  354,
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,  273,
  274,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,
  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,
  334,   -1,   -1,  337,  338,  339,  340,  341,  342,  343,
  344,  345,  346,   -1,  348,  349,   -1,  351,  352,  353,
  354,  355,  356,  357,   -1,   -1,  360,  361,  362,  363,
  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,  377,  378,  379,  380,  381,  382,   -1,
  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,
   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,   -1,  348,  349,   -1,
  351,  352,  353,  354,  355,  356,  357,   -1,   -1,  360,
  361,  362,  363,  273,  274,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,  377,  378,  379,  380,
  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,
   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,
   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,
  340,  341,  342,  343,  344,  345,  346,   -1,  348,  349,
   -1,  351,  352,  353,  354,  355,  356,  357,   -1,   -1,
  360,  361,  362,  363,  273,  274,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,  377,  378,  379,
  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,
   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,
   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,   -1,
   -1,  360,  361,  362,  363,  273,  274,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,
  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,  274,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,
  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,  273,  274,  366,
   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,
   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,
   -1,  337,  338,  339,  340,  341,  342,  343,  344,  345,
  346,   -1,  348,  349,   -1,  351,  352,  353,  354,  355,
  356,  357,   -1,   -1,  360,  361,  362,  363,  273,   -1,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,  377,  378,  379,  380,  381,  382,   -1,  384,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,
  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,
   -1,   -1,  337,  338,  339,  340,  341,  342,  343,  344,
  345,  346,   -1,  348,  349,   -1,  351,  352,  353,  354,
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,  273,
   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,
  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,
  334,   -1,   -1,  337,  338,  339,  340,  341,  342,  343,
  344,  345,  346,   -1,  348,  349,   -1,  351,  352,  353,
  354,  355,  356,  357,   -1,   -1,  360,  361,  362,  363,
  273,   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,  377,  378,  379,  380,  381,  382,   -1,
  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,   -1,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,  273,   -1,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,
   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,   -1,  348,  349,   -1,
  351,  352,  353,  354,  355,  356,  357,   -1,   -1,  360,
  361,  362,  363,   -1,   -1,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,  377,  378,  379,  380,
  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,  334,
   -1,   -1,  337,  338,  339,  340,  341,  342,  343,  344,
  345,  346,   -1,  348,  349,   -1,  351,  352,  353,  354,
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,   -1,
   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,
  };

#line 1145 "Iril/IR/IR.jay"

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
  public const int NONNULL = 307;
  public const int NOCAPTURE = 308;
  public const int WRITEONLY = 309;
  public const int READONLY = 310;
  public const int READNONE = 311;
  public const int ATTRIBUTE_GROUP_REF = 312;
  public const int ATTRIBUTES = 313;
  public const int NORECURSE = 314;
  public const int NOUNWIND = 315;
  public const int SPECULATABLE = 316;
  public const int SSP = 317;
  public const int UWTABLE = 318;
  public const int ARGMEMONLY = 319;
  public const int SEQ_CST = 320;
  public const int DSO_LOCAL = 321;
  public const int DSO_PREEMPTABLE = 322;
  public const int RET = 323;
  public const int BR = 324;
  public const int SWITCH = 325;
  public const int INDIRECTBR = 326;
  public const int INVOKE = 327;
  public const int RESUME = 328;
  public const int CATCHSWITCH = 329;
  public const int CATCHRET = 330;
  public const int CLEANUPRET = 331;
  public const int UNREACHABLE = 332;
  public const int FNEG = 333;
  public const int ADD = 334;
  public const int NUW = 335;
  public const int NSW = 336;
  public const int FADD = 337;
  public const int SUB = 338;
  public const int FSUB = 339;
  public const int MUL = 340;
  public const int FMUL = 341;
  public const int UDIV = 342;
  public const int SDIV = 343;
  public const int FDIV = 344;
  public const int UREM = 345;
  public const int SREM = 346;
  public const int FREM = 347;
  public const int SHL = 348;
  public const int LSHR = 349;
  public const int EXACT = 350;
  public const int ASHR = 351;
  public const int AND = 352;
  public const int OR = 353;
  public const int XOR = 354;
  public const int EXTRACTELEMENT = 355;
  public const int INSERTELEMENT = 356;
  public const int SHUFFLEVECTOR = 357;
  public const int EXTRACTVALUE = 358;
  public const int INSERTVALUE = 359;
  public const int ALLOCA = 360;
  public const int LOAD = 361;
  public const int STORE = 362;
  public const int FENCE = 363;
  public const int CMPXCHG = 364;
  public const int ATOMICRMW = 365;
  public const int GETELEMENTPTR = 366;
  public const int ALIGN = 367;
  public const int INBOUNDS = 368;
  public const int INRANGE = 369;
  public const int TRUNC = 370;
  public const int ZEXT = 371;
  public const int SEXT = 372;
  public const int FPTRUNC = 373;
  public const int FPEXT = 374;
  public const int TO = 375;
  public const int FPTOUI = 376;
  public const int FPTOSI = 377;
  public const int UITOFP = 378;
  public const int SITOFP = 379;
  public const int PTRTOINT = 380;
  public const int INTTOPTR = 381;
  public const int BITCAST = 382;
  public const int ADDRSPACECAST = 383;
  public const int ICMP = 384;
  public const int EQ = 385;
  public const int NE = 386;
  public const int UGT = 387;
  public const int UGE = 388;
  public const int ULT = 389;
  public const int ULE = 390;
  public const int SGT = 391;
  public const int SGE = 392;
  public const int SLT = 393;
  public const int SLE = 394;
  public const int FCMP = 395;
  public const int OEQ = 396;
  public const int OGT = 397;
  public const int OGE = 398;
  public const int OLT = 399;
  public const int OLE = 400;
  public const int ONE = 401;
  public const int ORD = 402;
  public const int UEQ = 403;
  public const int UNE = 404;
  public const int UNO = 405;
  public const int PHI = 406;
  public const int SELECT = 407;
  public const int CALL = 408;
  public const int TAIL = 409;
  public const int VA_ARG = 410;
  public const int LANDINGPAD = 411;
  public const int CATCHPAD = 412;
  public const int CLEANUPPAD = 413;
  public const int DEREFERENCEABLE = 414;
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
