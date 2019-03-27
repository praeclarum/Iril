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
              Console.WriteLine(String.Format ("syntax error, got token `{0}' expecting: {1}",
                                yyname (yyToken),
                                String.Join(", ", yyExpecting(yyState))));
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
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
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
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 119:
#line 452 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 120:
#line 459 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 121:
#line 463 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 122:
#line 470 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 474 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 124:
#line 478 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 125:
#line 482 "Iril/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 127:
#line 490 "Iril/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 128:
#line 494 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 129:
#line 495 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 130:
#line 496 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 131:
#line 497 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 132:
#line 498 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 133:
#line 499 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 134:
#line 500 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 135:
#line 501 "Iril/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 143:
#line 524 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 144:
#line 525 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 145:
#line 526 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 146:
#line 527 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 147:
#line 528 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 148:
#line 529 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 149:
#line 530 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 150:
#line 531 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 151:
#line 532 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 152:
#line 533 "Iril/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 153:
#line 537 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 154:
#line 538 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 155:
#line 539 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 156:
#line 540 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 157:
#line 541 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 158:
#line 542 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 159:
#line 543 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 160:
#line 544 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 161:
#line 545 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 162:
#line 546 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 163:
#line 547 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 164:
#line 548 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 165:
#line 549 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 166:
#line 550 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 167:
#line 551 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 168:
#line 552 "Iril/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 170:
#line 557 "Iril/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 171:
#line 558 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 172:
#line 562 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 173:
#line 566 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 174:
#line 570 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 175:
#line 574 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 176:
#line 578 "Iril/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 582 "Iril/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 178:
#line 586 "Iril/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 180:
#line 594 "Iril/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 181:
#line 595 "Iril/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 182:
#line 596 "Iril/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 183:
#line 597 "Iril/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 184:
#line 598 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 185:
#line 599 "Iril/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 186:
#line 600 "Iril/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 187:
#line 601 "Iril/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 188:
#line 602 "Iril/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 189:
#line 609 "Iril/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 616 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 620 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 192:
#line 627 "Iril/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 634 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 638 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 645 "Iril/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 656 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 660 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 199:
#line 667 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 671 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 678 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 682 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 203:
#line 686 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 690 "Iril/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 205:
#line 697 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 701 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 708 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 712 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 716 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 720 "Iril/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 728 "Iril/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 213:
#line 729 "Iril/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 214:
#line 736 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 740 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 747 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 217:
#line 751 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 218:
#line 755 "Iril/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
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
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 221:
#line 767 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 222:
#line 771 "Iril/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 224:
#line 776 "Iril/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 225:
#line 780 "Iril/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 226:
#line 784 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 227:
#line 788 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 228:
#line 792 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 233:
#line 809 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 813 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 819 "Iril/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 236:
#line 826 "Iril/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 830 "Iril/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 837 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 855 "Iril/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 245:
#line 862 "Iril/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 866 "Iril/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 870 "Iril/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 874 "Iril/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 249:
#line 878 "Iril/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 250:
#line 885 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 889 "Iril/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 893 "Iril/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 897 "Iril/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 901 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 255:
#line 905 "Iril/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 256:
#line 909 "Iril/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 913 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 258:
#line 917 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 259:
#line 921 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 260:
#line 925 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 261:
#line 929 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 262:
#line 933 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 263:
#line 937 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 264:
#line 941 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 265:
#line 945 "Iril/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 268:
#line 957 "Iril/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 961 "Iril/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 965 "Iril/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 969 "Iril/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 973 "Iril/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 977 "Iril/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 981 "Iril/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 985 "Iril/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 989 "Iril/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 993 "Iril/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 997 "Iril/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1001 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1005 "Iril/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1009 "Iril/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1013 "Iril/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1017 "Iril/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1021 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 285:
#line 1025 "Iril/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 286:
#line 1029 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 287:
#line 1033 "Iril/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 288:
#line 1037 "Iril/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1041 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1045 "Iril/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1049 "Iril/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1053 "Iril/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1057 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1061 "Iril/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1065 "Iril/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1069 "Iril/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 297:
#line 1073 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 298:
#line 1077 "Iril/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 299:
#line 1081 "Iril/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 300:
#line 1085 "Iril/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 301:
#line 1089 "Iril/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 302:
#line 1093 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 303:
#line 1097 "Iril/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 304:
#line 1101 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 305:
#line 1105 "Iril/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 306:
#line 1109 "Iril/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 307:
#line 1113 "Iril/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 308:
#line 1117 "Iril/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 309:
#line 1121 "Iril/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 310:
#line 1125 "Iril/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 311:
#line 1129 "Iril/IR/IR.jay"
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
   37,   37,   37,   37,    5,    5,    5,   33,   33,   40,
   40,   41,   41,   41,   41,   42,   42,   36,   36,   36,
   36,   36,   36,   36,   36,   14,   14,   38,   38,   34,
   34,   43,   44,   44,   44,   44,   44,   44,   44,   44,
   44,   44,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   45,   45,   45,   45,   45,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   48,   19,
   19,   19,   19,   19,   19,   19,   19,   19,   49,   26,
   26,   50,   47,   47,   24,   51,   46,   46,   35,   35,
   52,   52,   52,   52,   53,   53,   55,   55,   55,   55,
   57,   58,   58,   59,   59,   60,   60,   60,   60,   60,
   60,   60,   61,   61,   61,   61,   61,   61,   20,   20,
   62,   62,   63,   63,   64,   65,   65,   66,   67,   67,
   68,   68,   39,   69,   54,   54,   54,   54,   54,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,   56,   56,   56,   56,   56,   56,   56,   56,   56,
   56,
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
    1,    2,    3,    2,    5,    6,    6,    3,    2,    1,
    3,    1,    2,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    6,    9,    6,    6,    3,    3,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    2,
    1,    2,    1,    3,    2,    1,    1,    3,    1,    2,
    2,    3,    1,    2,    1,    2,    1,    2,    3,    4,
    1,    3,    2,    1,    3,    2,    3,    3,    3,    2,
    4,    5,    1,    1,    6,    9,    6,    6,    1,    3,
    1,    1,    1,    3,    5,    1,    2,    3,    1,    2,
    1,    1,    1,    1,    2,    7,    2,    7,    1,    5,
    6,    5,    5,    5,    6,    4,    4,    5,    6,    5,
    6,    6,    6,    7,    5,    6,    7,    4,    5,    6,
    5,    2,    5,    4,    4,    4,    4,    5,    6,    7,
    6,    6,    4,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    6,    7,    4,    5,    6,    6,    4,
    5,    7,    8,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
    0,   81,   91,   84,   85,   86,   87,   83,  108,   30,
   29,   31,   32,   33,  243,  133,  134,  135,  128,  129,
  131,  130,  132,  138,  139,    0,    0,    0,   82,    0,
    0,    0,    0,    0,  109,  110,    0,    0,    0,    3,
    0,    4,    0,    0,  136,  137,   27,   28,   34,    0,
    0,    0,    0,    0,    0,    0,    0,   75,    0,    0,
    0,    0,    0,    0,   90,    0,  114,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    8,    0,    7,    0,    0,    0,    0,
   76,    0,    0,    0,    0,  113,   97,   88,    0,    0,
   94,    0,    0,    0,    0,  124,  125,  119,    0,    0,
  120,  142,    0,    0,  140,  182,  183,  181,  184,  185,
  186,  180,  171,  170,  188,  187,    0,    0,    0,    0,
    0,    0,    0,    0,  169,    0,    0,    0,    0,    0,
    0,    0,    0,   35,    0,    0,    0,   60,   59,   13,
    0,    0,   53,   58,    0,    0,    0,    0,   89,    0,
    0,    0,    0,    0,    0,   73,   65,   63,   64,   66,
   67,   68,   69,    0,   61,  126,    0,  118,    0,    0,
    0,    0,    0,    0,  141,    0,    0,    0,    0,  193,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   15,    0,    0,    0,   54,   14,    0,  190,
   92,   77,   93,   95,    0,    0,    0,    0,   12,   62,
  127,  121,    0,    0,    0,   51,    0,    0,    0,    0,
    0,  249,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  199,    0,    0,  205,
    0,    0,    0,    0,    0,    0,    0,  178,    0,  176,
  177,    0,    0,    0,    0,    0,    0,    0,   48,    0,
   46,    0,   37,   49,    0,   43,   45,   50,   38,   39,
   36,   17,   16,   57,   56,   55,   70,  232,  231,    0,
  229,    0,    0,    0,    0,    0,  247,    0,    0,  245,
    0,  241,  242,    0,    0,  239,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  244,  272,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  143,  144,  145,  146,  147,  148,  149,  150,
  151,  152,    0,  153,  154,  165,  166,  167,  168,  156,
  158,  159,  160,  161,  157,  155,  163,  164,  162,    0,
    0,    0,    0,    0,    0,    0,   98,  200,    0,  206,
    0,    0,   52,    0,    0,    0,    0,    0,  194,    0,
    0,    0,   26,    0,    0,    0,    0,  195,    0,   74,
    0,    0,  101,    0,    0,    0,  189,    0,    0,    0,
    0,  240,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  233,    0,  211,    0,    0,    0,    0,    0,    0,    0,
    0,  100,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,   41,    0,   47,   44,  230,    0,  103,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  268,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  175,  172,  174,    0,    0,
    0,    0,   40,    0,   99,    0,    0,  250,    0,  269,
  304,    0,  278,  289,    0,  273,  307,    0,  293,  271,
  309,  301,  297,    0,    0,  286,    0,  254,  253,  288,
  310,    0,    0,  252,    0,  179,  192,    0,    0,    0,
    0,    0,    0,    0,    0,  234,    0,    0,  213,    0,
    0,  214,    0,    0,  258,    0,    0,    0,    0,    0,
    0,    0,    0,  102,    0,    0,    0,  236,  251,  305,
  290,  294,  298,  287,  255,  282,  299,    0,    0,    0,
    0,    0,  196,    0,  197,  281,  270,    0,    0,    0,
    0,  216,    0,  212,    0,    0,  259,    0,    0,  266,
    0,    0,  246,    0,  248,  237,    0,  284,    0,  302,
    0,    0,  235,  295,    0,  224,  218,    0,    0,    0,
    0,  223,  219,  217,  215,    0,  267,  173,  238,  285,
  303,  198,  221,    0,    0,    0,    0,    0,  222,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  228,  225,  227,    0,    0,  226,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   49,   12,   13,   14,  194,  171,  163,   70,
  172,  523,  204,   71,   72,   51,  164,  323,  155,  340,
  325,  326,  327,  328,  173,  703,  195,   80,   81,  120,
  121,   15,   96,  134,  296,  196,   54,   55,   56,  130,
  131,  197,  135,  413,  430,  704,  211,  657,  350,  584,
  705,  297,  298,  299,  300,  301,  524,  607,  671,  672,
  743,  341,  520,  521,  687,  688,  355,  356,  388,
  };
  protected static readonly short [] yySindex = {         1956,
   14, -187,   29,   54,   82, 3159, 3503, -213,    0, 1956,
    0,    0,    0,    0,  -96,  -97,  122,  137,  901,  -73,
    5,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3681, -112,  -44,    0,  275,
 -175,  268, 3736, 3307,    0,    0, 3736,  -33,  283,    0,
  307,    0,   91,   94,    0,    0,    0,    0,    0, 3736,
 -123,  -72, -153,  -47,  318,  -28,  237,    0,  275,  -21,
  268,  100, 3736,  106,    0,   68,    0, 3243,  268,  268,
 3736,  -31,  307,  259, 2199, -212,    0,    0,  552, 3736,
 -123, 3736, -123,    0,  261,    0, -244,  345,  265, 3516,
    0, 3736, 3736,  -20, 3736,    0,    0,    0,  275,   27,
    0,  268,  307, -212, 2073,    0,    0,    0,  227,   87,
    0,    0,   78, -101,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   30,  353,  355,  356,
 3769, 3769, 3769,  358,    0,  552, 3736,  727, 3736,  339,
  342,  346,   95,    0, -244, 3616,    0,    0,    0,    0,
  -32,  552,    0,    0,  275,   93,  341,   25,    0, 3478,
   78,   78,   78,  344,  366,    0,    0,    0,    0,    0,
    0,    0,    0,  530,    0,    0,  -83,    0, 3427,  -95,
  134,  154, 4642, -109,    0,  370, 3769, 3769, 3769,    0,
   -1,   88,  -13,   45,  371,  552,   46,  373, 2067, 3559,
  144,  839,    0, -244,  120,   -9,    0,    0, 3630,    0,
    0,    0,    0,    0,   78,   78,  159, 2041,    0,    0,
    0,    0,  169, 4642, -107,    0,  157,  375, 3769, -225,
 3769,    0,  420, 3736,  420, 3736,  420, 3736, 3736,  685,
 3736, 3736, 3736,  420, 2172, 2186, 3736, 3736, 3736, 3769,
 3769, 3769, 3736, 3215, 3414,  107,   21, 3769, 3769, 3769,
 3769, 3769, 3769, 3769, 3769, 3769, 3769, 3769, 3769, 1538,
 1815, 3736, 3736, 3250,   24, 2189,    0, 4642,  157,    0,
  157,  171, 4642, 3736,   71,   76,   77,    0, 3769,    0,
    0,  197,   89,  411,  201,   92,   98,  416,    0,  421,
    0,  433,    0,    0,  338,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  153,
    0,  157, 2280, 4642, -106, 5082,    0,  196,  -57,    0,
  423,    0,    0,  552,  420,    0,  552,  552,  420,  552,
  552,  420,  552,  552, 3736,  552,  552,  552,  552,  552,
  420, 3736,  552, 3736,  552,  552,  552,  552,  426,  427,
  435,  148, 3736,  194, 3769,  440,    0,    0, 3736,  257,
  110,  111,  112,  114,  115,  116,  139,  140,  141,  142,
  150,  152,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3736,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3736,
   28,  552,   57, 3736, 3307, 3250,    0,    0,  157,    0,
  223,  223,    0, 2371,  284, 3736, 3736, 3736,    0,  157,
  262,  165,    0,  285,  289,  199,  471,    0, 3593,    0,
 2041, -104,    0, 2462, 4642,  157,    0,  478,  295,  520,
  552,    0,  526,  527,  552,  528,  529,  552,  531,  533,
  552,  537,  538,  540,  541,  542,  552,  552,  543,  552,
  545,  546,  554,  557, 3769, 3769, 3769,  207,  291, 3736,
  559, 3736,  308, 3769, 3736, 3736, 3736, 3736, 3736, 3736,
 3736, 3736, 3736, 3736, 3736, 3736,  552,  552,  -57,  565,
    0,  566,    0,  548,   57,   57, 3736,   57, 3736, 3307,
  223,    0, 3769,  202,  232,  248,  223,  157,  354,  157,
    0,  357,    0,  155,    0,    0,    0, 4642,    0, 2553,
  223,  295,  524,  -57,  572,  -57,  -57,  573,  -57,  -57,
  574,  -57,  -57,  575,  -57,  -57,  -57,  -57,  -57,  579,
  580,  -57,  582,  -57,  -57,  -57,  -57,    0,  583,  584,
  372, 3736,  552,  586, 3736,  589, 3769,  590,  275,  275,
  275,  275,  275,  275,  275,  275,  275,  275,  275,  275,
  591,  592,  595,  555, 3769, 3450,   78,  548,  548,   57,
  548,   57,   57, 3736,  601,    0,    0,    0,  223,  157,
  223,  157,    0, 2644,    0,  603, 3736,    0,  -57,    0,
    0,  -57,    0,    0,  -57,    0,    0,  -57,    0,    0,
    0,    0,    0,  -57,  -57,    0,  -57,    0,    0,    0,
    0, 3769, 3769,    0,  604,    0,    0,  282,  606,  286,
  607, 3769,  -57,  -57,  -57,    0,  612, 3646,    0, 1976,
  168,    0,   78,   78,    0,  548,   78,  548,  548,   57,
 3769,  223,  223,    0,  295,  616, 3686,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  294,  406,  299,
  408, 3769,    0,  628,    0,    0,    0,  596, 3769,  657,
 1995,    0, 2107,    0, 3714,   78,    0,   78,   78,    0,
  548,  189,    0,  295,    0,    0,  442,    0,  450,    0,
  628, 3769,    0,    0, 3286,    0,    0,  340,  666,  670,
  673,    0,    0,    0,    0,   78,    0,    0,    0,    0,
    0,    0,    0,  191,  680, 3769, 3769, 3769,    0, 3736,
  348,  350,  351,  313, 3736, 3736, 3736, 3769,  271,  298,
  322,  683,    0,    0,    0, 3769,  193,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  731,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  334,
 3348,  463,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    9,    0,
    0,    0,    0,    0,    0, 3361,    0,    0,  467,  468,
    0,    0,    0,    0,    0,    0,    0,    0,  703,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  145,    0,
    0,  469,    0,    0,    0,    0,    0,    0,  147,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  703,    0,  703,    0,    0,
    0,    0,    0,    0,    0,    0,  708,    0,    0,    0,
    0,  703,    0,    0,   15,  703,    0,  703,    0,    0,
    0,    0,    8,  544,  701,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  234,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  703,    0,    0,  703,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   73,  103,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 2735,    0,
 4733,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  175,    0,    0,    0,    0,    0,  -23,
    0,  703,    0,    0,  266,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  703,    0,    0,  703,  703,    0,  703,
  703,    0,  703,  703,    0,  703,  703,  703,  703,  703,
    0,    0,  703,    0,  703,  703,  703,  703,    0,    0,
    0,  703,    0,  703,    0,    0,    0,    0,    0,  703,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  703,  703,    0,    0,    0,    0,    0,    0, 2826,    0,
 2917, 4824,    0,    0,  703,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4915,    0,    0,    0,    0,
  703,    0,    0,    0,  703,    0,    0,  703,    0,    0,
  703,    0,    0,    0,    0,    0,  703,  703,    0,  703,
    0,    0,    0,    0,    0,    0,    0,    0,  703,    0,
    0,    0,  703,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  703,  703,    0, 3732,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3008,    0,    0,  703,  703,  703,  369,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5006,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  703,    0,    0,    0,    0,    0,  799,  890,
  981, 1072, 1163, 1254, 1345, 1436, 1527, 1618, 1709, 1800,
    0,    0,    0,    0,    0,    0, 3823,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  449,  622,
  631,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  703,
    0,    0, 3914,    0,    0,    0, 4005,    0,    0,    0,
    0,  640, 2003,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 4096,    0,    0,    0,    0,    0,  293,
  703,    0,    0,    0,    0, 4187,    0, 4278, 4369,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4460,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4551,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  703,    0,    0,    0,    0,  703,  703,
  703,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  734,  672,    0,    0,    0,    0,  585,  588,   81,
   -6,  310, -139,   53,    0,  739,  539,    0, -166, -451,
    0,  309,    0, -585,    0,  269,  578,  684,   38,    0,
  594,    0,  -60,  -94, -134,   -2,    0,  724,  -49,    0,
  581,  108, -114,    0,    0, -656,  228,    0, -454, -452,
   47, -286,    0,  479,  484,  437, -499, -116,    0,   69,
    0,  325,    0,  185,    0,  105,  -98, -297,    0,
  };
  protected static readonly short [] yyTable = {            50,
   50,   87,  152,   53,  109,  544,   88,  115,   88,  438,
   83,  229,  105,  303,  553,  344,  465,   47,  548,  205,
   47,  203,  112,  112,  722,  608,  609,  244,  611,  183,
  309,  160,  124,  153,  229,  161,  116,   77,  200,   79,
  348,  686,  309,   52,   58,  731,   50,   50,   80,  586,
   50,   91,   78,  324,   80,  330,  438,  472,   79,  349,
  245,  472,  181,   99,  472,  151,   85,  179,  205,   85,
  180,  339,  116,  472,   16,   46,   79,   65,   66,  311,
   47,  119,   17,   18,   50,  205,  235,  236,  129,   19,
   89,   90,  228,  156,   92,  158,   88,  626,   59,  132,
   47,  686,  117,  111,  177,  175,  176,  345,  178,  343,
  676,   48,  678,  679,   20,  333,  152,  233,  519,  777,
  205,  205,  162,  308,  101,  103,   35,  198,  122,  655,
  199,  309,  659,   78,   85,  223,   65,   66,  224,   79,
   67,   68,   21,   46,   82,   44,   45,  153,  133,  310,
  216,  100,  219,  102,  231,  458,  359,  438,  362,  441,
  332,  442,   62,  224,  302,  371,  302,  302,  444,  302,
   67,   68,  201,  119,   21,   61,  182,  438,  201,  151,
  721,  157,   63,  159,   80,   96,   80,  122,   96,   85,
  122,  498,  129,  460,  241,  623,  461,   64,  461,  136,
  137,  138,  462,  139,  140,  141,   74,  142,  714,  464,
  132,  715,   84,  322,  143,  144,  132,   65,   66,   36,
   37,  145,   38,   39,   40,   41,   42,   43,  146,  748,
  723,  759,  732,  778,  461,   85,  732,  500,   93,  104,
  123,  227,  616,   85,  435,  108,  354,  357,  358,  360,
  361,  363,  364,  366,  367,  368,  369,  370,  373,  375,
  376,  377,  378,  438,  227,  202,  382,  384,   85,  749,
  390,  243,  617,   85,  123,  115,  115,  123,   75,  115,
  115,   76,  115,  754,   22,  431,  432,   50,  618,   85,
  339,  434,  324,   23,  339,  115,  115,  445,   85,  531,
  504,   24,   25,   26,   27,   28,   42,   88,  147,   42,
  537,  773,   85,  136,  137,  138,   85,  139,  140,  141,
  115,  142,  148,  149,  150,   85,  551,  533,  143,  144,
  550,  433,   85,  220,  582,  145,  220,  438,  774,   85,
  116,  116,  146,   94,  116,  116,   95,  116,  471,   85,
   97,  587,  475,   98,   85,  478,  768,  107,  481,  110,
  116,  116,  775,   85,  487,  488,  113,  490,   18,   35,
  117,  117,  115,   80,  117,  117,  499,  117,  174,  212,
  213,  125,  503,   83,  165,  116,  530,  166,  389,  132,
  117,  117,  207,   80,  208,  209,  220,  206,  619,  221,
  621,  214,  232,  222,  237,  238,  517,  246,  154,  304,
  247,  312,  315,  624,  313,  117,  316,  329,  337,  210,
  210,  210,  147,  518,   80,  342,  387,   50,   50,   50,
  201,  436,  527,  529,  174,  346,  148,  149,  150,  534,
  535,  536,   21,   21,  443,  446,   21,   21,   19,   21,
  447,  448,  322,  450,  452,  451,   80,  453,  454,  456,
  457,  459,   21,   21,  455,  215,  469,  218,  467,  495,
  496,  525,  526,  528,   85,  305,  306,  307,  497,   47,
  682,  230,  683,  502,  505,  506,  507,   21,  508,  509,
  510,  674,  675,  583,  677,  583,  302,  336,  589,  590,
  591,  592,  593,  594,  595,  596,  597,  598,  599,  600,
   48,  543,  673,  511,  512,  513,  514,  347,  538,  351,
   50,  552,   50,   50,  515,  314,  516,  614,  318,   36,
   37,  539,   38,   39,   40,   41,   42,   43,  379,  380,
  381,  540,   46,  386,  742,  541,  391,  392,  393,  394,
  395,  396,  397,  398,  399,  400,  401,  402,  205,  717,
  348,  719,  720,  554,  610,  542,  612,  613,  339,  556,
  557,  559,  560,  581,  562,  583,  563,  449,  583,  716,
  565,  566,  718,  567,  568,  569,  572,  606,  574,  575,
   80,   80,   80,   85,   80,   80,   80,  576,   80,  670,
  577,  205,  585,  205,  747,   80,   80,   50,  604,  605,
  620,  152,   80,  622,  627,  629,  632,  635,  638,   80,
  322,   20,  644,  645,  746,  647,  652,  653,  654,  658,
   25,  205,  660,  662,  663,  664,   18,   18,  665,   22,
   18,   18,  153,   18,  681,  519,  685,  698,  699,  700,
  702,  680,  701,  501,  239,  709,   18,   18,  468,  724,
  727,  711,  728,  470,  730,  729,  473,  474,   71,  476,
  477,  732,  479,  480,  151,  482,  483,  484,  485,  486,
  322,   18,  489,   22,  491,  492,  493,  494,  733,  136,
  137,  138,   23,  139,  140,  141,  735,  142,  750,   80,
   24,   25,   26,   27,   28,  756,  751,  755,  670,  757,
  241,  145,  758,   80,   80,   80,   19,   19,  146,  760,
   19,   19,  765,   19,  766,  767,  776,  136,  137,  138,
    1,  139,  140,  141,  104,  142,   19,   19,  105,  106,
  107,  522,   80,   60,   47,  106,  338,   81,  191,  145,
  226,  191,  225,  764,  352,  353,  146,   73,  769,  770,
  771,   19,  331,  578,  579,  580,  114,  546,   85,  191,
  217,  240,  588,  234,   86,   48,  439,  713,  752,  242,
  555,  440,  466,  745,  558,  547,  152,  561,  666,  184,
  564,  726,    0,    0,    0,    0,  570,  571,    0,  573,
  191,  615,    0,   71,    0,  185,    0,   46,  136,  137,
  138,    0,  139,  140,  141,    0,  142,  153,    0,   71,
    0,    0,    0,  143,  144,   72,  601,  602,  603,    0,
  145,    0,  191,    0,    0,    0,    0,  146,   80,  186,
  187,    0,    0,  188,  189,  190,  191,  192,  193,  151,
    0,    0,    0,   71,   71,  661,    0,   71,   71,   71,
   71,   71,   71,  628,    0,  630,  631,    0,  633,  634,
    0,  636,  637,  667,  639,  640,  641,  642,  643,    0,
    0,  646,    0,  648,  649,  650,  651,    0,    0,   20,
   20,    0,  656,   20,   20,    0,   20,    0,   25,   25,
    0,    0,   25,   25,    0,   25,    0,   22,   22,   20,
   20,   22,   22,    0,   22,    0,    0,  147,   25,   25,
  696,  697,    0,    0,    0,    0,    0,   22,   22,   80,
    0,  148,  149,  150,   20,    0,    0,    0,  689,    0,
    0,  690,    0,   25,  691,    0,    0,  692,   22,    0,
    0,    0,   22,  693,  694,    0,  695,   23,    0,    0,
   72,    0,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,  706,  707,  708,    0,   72,  734,    0,  712,
  191,  191,    0,  136,  137,  138,    0,  139,  140,  141,
    0,  142,    0,    0,    0,    0,    0,    0,  143,  144,
    0,    0,    0,    0,    0,  145,    0,    0,    0,    0,
   72,   72,  146,    0,   72,   72,   72,   72,   72,   72,
   80,    0,  744,    0,  761,  762,  763,    0,    0,    0,
  191,  191,  191,    0,  365,    0,  772,    0,    0,  191,
    0,  191,    0,    0,  191,  191,  191,  191,  191,  191,
  191,  191,  191,  191,    0,  191,  191,    0,  191,  191,
  191,  191,  191,  191,  191,    0,    0,  191,  191,  191,
  191,  306,  306,  191,    0,    0,    0,  191,  191,  191,
  191,  191,  191,  191,  191,  191,  191,  191,  191,  191,
    0,  191,  147,    0,    0,  136,  137,  138,    0,  139,
  140,  141,  191,  142,    0,    0,  148,  149,  150,    0,
    0,   80,    0,  191,  191,  191,  191,  145,    0,    0,
    0,  306,  306,  306,  146,    0,    0,    0,    0,    0,
  306,    0,  306,    0,    0,  306,  306,  306,  306,  306,
  306,  306,  306,  306,  306,    0,  306,  306,    0,  306,
  306,  306,  306,  306,  306,  306,    0,    0,  306,  306,
  306,  306,  311,  311,  306,    0,    0,    0,  306,  306,
  306,  306,  306,    0,  306,  306,  306,  306,  306,  306,
  306,    0,  306,    0,    0,    0,    0,    0,    0,    0,
   65,   66,    0,  306,   67,   68,   69,   30,   31,   32,
   33,   34,   80,    0,  306,  306,  306,  306,    0,    0,
    0,    0,  311,  311,  311,    0,    0,    0,    0,    0,
    0,  311,    0,  311,    0,    0,  311,  311,  311,  311,
  311,  311,  311,  311,  311,  311,    0,  311,  311,    0,
  311,  311,  311,  311,  311,  311,  311,    0,    0,  311,
  311,  311,  311,  296,  296,  311,    0,    0,    0,  311,
  311,  311,  311,  311,    0,  311,  311,  311,  311,  311,
  311,  311,    0,  311,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  311,    0,    0,    0,    0,    0,
    0,    0,    0,   80,    0,  311,  311,  311,  311,    0,
    0,    0,    0,  296,  296,  296,    0,    0,    0,    0,
    0,    0,  296,    0,  296,    0,    0,  296,  296,  296,
  296,  296,  296,  296,  296,  296,  296,    0,  296,  296,
    0,  296,  296,  296,  296,  296,  296,  296,    0,    0,
  296,  296,  296,  296,  277,  277,  296,    0,    0,    0,
  296,  296,  296,  296,  296,    0,  296,  296,  296,  296,
  296,  296,  296,    0,  296,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  296,    0,    0,    0,    0,
    0,    0,    0,    0,   80,    0,  296,  296,  296,  296,
    0,    0,    0,    0,  277,  277,  277,    0,    0,    0,
    0,    0,    0,  277,    0,  277,    0,    0,  277,  277,
  277,  277,  277,  277,  277,  277,  277,  277,    0,  277,
  277,    0,  277,  277,  277,  277,  277,  277,  277,    0,
    0,  277,  277,  277,  277,  274,  274,  277,    0,    0,
    0,  277,  277,  277,  277,  277,    0,  277,  277,  277,
  277,  277,  277,  277,    0,  277,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  277,    0,    0,    0,
    0,    0,    0,    0,    0,   80,    0,  277,  277,  277,
  277,    0,    0,    0,    0,  274,  274,  274,    0,    0,
    0,    0,    0,    0,  274,    0,  274,    0,    0,  274,
  274,  274,  274,  274,  274,  274,  274,  274,  274,    0,
  274,  274,    0,  274,  274,  274,  274,  274,  274,  274,
    0,    0,  274,  274,  274,  274,  275,  275,  274,    0,
    0,    0,  274,  274,  274,  274,  274,    0,  274,  274,
  274,  274,  274,  274,  274,    0,  274,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  274,    0,    0,
    0,    0,    0,    0,    0,    0,   80,    0,  274,  274,
  274,  274,    0,    0,    0,    0,  275,  275,  275,    0,
    0,    0,    0,    0,    0,  275,    0,  275,    0,    0,
  275,  275,  275,  275,  275,  275,  275,  275,  275,  275,
    0,  275,  275,    0,  275,  275,  275,  275,  275,  275,
  275,    0,    0,  275,  275,  275,  275,  276,  276,  275,
    0,    0,    0,  275,  275,  275,  275,  275,    0,  275,
  275,  275,  275,  275,  275,  275,    0,  275,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  275,    0,
    0,    0,    0,    0,    0,    0,    0,   80,    0,  275,
  275,  275,  275,    0,    0,    0,    0,  276,  276,  276,
    0,    0,    0,    0,    0,    0,  276,    0,  276,    0,
    0,  276,  276,  276,  276,  276,  276,  276,  276,  276,
  276,    0,  276,  276,    0,  276,  276,  276,  276,  276,
  276,  276,    0,    0,  276,  276,  276,  276,  308,  308,
  276,    0,    0,    0,  276,  276,  276,  276,  276,    0,
  276,  276,  276,  276,  276,  276,  276,    0,  276,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  276,
    0,    0,    0,    0,    0,    0,    0,    0,   80,    0,
  276,  276,  276,  276,    0,    0,    0,    0,  308,  308,
  308,    0,    0,    0,    0,    0,    0,  308,    0,  308,
    0,    0,  308,  308,  308,  308,  308,  308,  308,  308,
  308,  308,    0,  308,  308,    0,  308,  308,  308,  308,
  308,  308,  308,    0,    0,  308,  308,  308,  308,  300,
  300,  308,    0,    0,    0,  308,  308,  308,  308,  308,
    0,  308,  308,  308,  308,  308,  308,  308,    0,  308,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  308,    0,    0,    0,    0,    0,    0,    0,    0,   80,
    0,  308,  308,  308,  308,    0,    0,    0,    0,  300,
  300,  300,    0,    0,    0,    0,    0,    0,  300,    0,
  300,    0,    0,  300,  300,  300,  300,  300,  300,  300,
  300,  300,  300,    0,  300,  300,    0,  300,  300,  300,
  300,  300,  300,  300,    0,    0,  300,  300,  300,  300,
  292,  292,  300,    0,    0,    0,  300,  300,  300,  300,
  300,    0,  300,  300,  300,  300,  300,  300,  300,    0,
  300,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  300,  403,  404,  405,  406,  407,  408,  409,  410,
  411,  412,  300,  300,  300,  300,    0,    0,    0,    0,
  292,  292,  292,    0,    0,    0,    0,    0,    0,  292,
    0,  292,    0,    0,  292,  292,  292,  292,  292,  292,
  292,  292,  292,  292,    0,  292,  292,    0,  292,  292,
  292,  292,  292,  292,  292,    0,    0,  292,  292,  292,
  292,  283,  283,  292,    0,    0,    0,  292,  292,  292,
  292,  292,    0,  292,  292,  292,  292,  292,  292,  292,
    0,  292,   24,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  292,    0,    0,    0,    0,   85,    0,    0,
    0,    0,    0,  292,  292,  292,  292,    0,    0,    0,
    0,  283,  283,  283,    0,  152,   85,    0,    0,    0,
  283,    0,  283,    0,    0,  283,  283,  283,  283,  283,
  283,  283,  283,  283,  283,    0,  283,  283,    0,  283,
  283,  283,  283,  283,  283,  283,  153,    0,  283,  283,
  283,  283,  256,  256,  283,  414,  415,    0,  283,  283,
  283,  283,  283,    0,  283,  283,  283,  283,  283,  283,
  283,    0,  283,    0,    0,    0,    0,    0,  151,    0,
    0,    0,    0,  283,    0,    0,    0,    0,   85,    0,
  317,    0,    0,    0,  283,  283,  283,  283,    0,    0,
    0,    0,  256,  256,  256,    0,  152,    0,    0,    0,
    0,  256,    0,  256,    0,    0,  256,  256,  256,  256,
  256,  256,  256,  256,  256,  256,    0,  256,  256,    0,
  256,  256,  256,  256,  256,  256,  256,  153,    0,  256,
  256,  256,  256,    0,    0,  256,  152,    0,    0,  256,
  256,  256,  256,  256,    0,  256,  256,  256,  256,  256,
  256,  256,    0,  256,    0,    0,    0,    0,    0,  151,
    0,    0,    0,    0,  256,    0,    0,  153,    0,    0,
    0,  416,  417,  418,  419,  256,  256,  256,  256,    0,
  420,  421,  422,  423,  424,  425,  426,  427,  428,  429,
    0,    0,    0,    1,    2,    0,    0,    3,    4,  151,
    5,   47,  136,  137,  138,    0,  139,  140,  141,  128,
  142,    0,    0,    6,    7,   47,    0,  143,  144,    0,
    0,  136,  137,  138,  145,  139,  140,  141,   47,  142,
    0,  146,   48,    0,    0,    0,  736,  737,    8,    0,
   24,   24,    0,  145,   24,   24,   48,   24,   36,   37,
  146,   38,   39,   40,   41,   42,   43,    0,    0,   48,
   24,   24,    0,    0,   46,    0,    0,  136,  137,  138,
    0,  139,  140,  141,    0,  142,    0,    0,   46,    0,
    0,    0,    0,  437,    0,   24,  338,    0,    0,  145,
    0,   46,    0,  136,  137,  138,  146,  139,  140,  141,
    0,  142,  184,    0,    0,    0,    0,    0,  143,  144,
    0,  147,    0,    0,    0,  145,    0,    0,  185,    0,
    0,    0,  146,    0,    0,  148,  149,  150,    0,    0,
  738,    0,    0,  136,  137,  138,    0,  139,  140,  141,
    0,  142,    0,    0,  739,  740,  741,    0,  143,  144,
    0,    0,  186,  187,    0,  145,  188,  189,  190,  191,
  192,  193,  146,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  463,    0,    0,    0,    0,   36,
   37,    0,   38,   39,   40,   41,   42,   43,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  147,    0,    0,   22,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,  148,  149,  150,   22,
    0,    0,   24,   25,   26,   27,   28,    0,   23,    0,
    0,  248,   22,    0,    0,    0,   24,   25,   26,   27,
   28,   23,  147,    0,    0,    0,  126,    0,    0,   24,
   25,   26,   27,   28,    0,    0,  148,  149,  150,    0,
    0,  127,    0,    0,    0,  532,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  249,  250,  251,    0,    0,    0,    0,    0,    0,
  252,  372,  253,    0,    0,  254,  255,  256,  257,  258,
  259,  260,  261,  262,  263,  374,  264,  265,    0,  266,
  267,  268,  269,  270,  271,  272,    0,    0,  273,  274,
  275,  276,  248,    0,  277,    0,    0,    0,  278,  279,
  280,  281,  282,    0,  283,  284,  285,  286,  287,  288,
  289,    0,  290,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  291,    0,    0,  549,    0,    0,    0,
    0,    0,    0,    0,  292,  293,  294,  295,    0,    0,
    0,    0,  249,  250,  251,    0,    0,    0,    0,    0,
    0,  252,    0,  253,    0,    0,  254,  255,  256,  257,
  258,  259,  260,  261,  262,  263,    0,  264,  265,    0,
  266,  267,  268,  269,  270,  271,  272,    0,    0,  273,
  274,  275,  276,  248,    0,  277,    0,    0,    0,  278,
  279,  280,  281,  282,    0,  283,  284,  285,  286,  287,
  288,  289,    0,  290,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  291,    0,    0,  625,    0,    0,
    0,    0,    0,    0,    0,  292,  293,  294,  295,    0,
    0,    0,    0,  249,  250,  251,    0,    0,    0,    0,
    0,    0,  252,    0,  253,    0,    0,  254,  255,  256,
  257,  258,  259,  260,  261,  262,  263,    0,  264,  265,
    0,  266,  267,  268,  269,  270,  271,  272,    0,    0,
  273,  274,  275,  276,  248,    0,  277,    0,    0,    0,
  278,  279,  280,  281,  282,    0,  283,  284,  285,  286,
  287,  288,  289,    0,  290,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  291,    0,    0,  684,    0,
    0,    0,    0,    0,    0,    0,  292,  293,  294,  295,
    0,    0,    0,    0,  249,  250,  251,    0,    0,    0,
    0,    0,    0,  252,    0,  253,    0,    0,  254,  255,
  256,  257,  258,  259,  260,  261,  262,  263,    0,  264,
  265,    0,  266,  267,  268,  269,  270,  271,  272,    0,
    0,  273,  274,  275,  276,  248,    0,  277,    0,    0,
    0,  278,  279,  280,  281,  282,    0,  283,  284,  285,
  286,  287,  288,  289,    0,  290,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  291,    0,    0,  203,
    0,    0,    0,    0,    0,    0,    0,  292,  293,  294,
  295,    0,    0,    0,    0,  249,  250,  251,    0,    0,
    0,    0,    0,    0,  252,    0,  253,    0,    0,  254,
  255,  256,  257,  258,  259,  260,  261,  262,  263,    0,
  264,  265,    0,  266,  267,  268,  269,  270,  271,  272,
    0,    0,  273,  274,  275,  276,  248,    0,  277,    0,
    0,    0,  278,  279,  280,  281,  282,    0,  283,  284,
  285,  286,  287,  288,  289,    0,  290,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  291,    0,    0,
  201,    0,    0,    0,    0,    0,    0,    0,  292,  293,
  294,  295,    0,    0,    0,    0,  249,  250,  251,    0,
    0,    0,    0,    0,    0,  252,    0,  253,    0,    0,
  254,  255,  256,  257,  258,  259,  260,  261,  262,  263,
    0,  264,  265,    0,  266,  267,  268,  269,  270,  271,
  272,    0,    0,  273,  274,  275,  276,  203,    0,  277,
    0,    0,    0,  278,  279,  280,  281,  282,    0,  283,
  284,  285,  286,  287,  288,  289,    0,  290,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  291,    0,
    0,  204,    0,    0,    0,    0,    0,    0,    0,  292,
  293,  294,  295,    0,    0,    0,    0,  203,  203,  203,
    0,    0,    0,    0,    0,    0,  203,    0,  203,    0,
    0,  203,  203,  203,  203,  203,  203,  203,  203,  203,
  203,    0,  203,  203,    0,  203,  203,  203,  203,  203,
  203,  203,    0,    0,  203,  203,  203,  203,  201,    0,
  203,    0,    0,    0,  203,  203,  203,  203,  203,    0,
  203,  203,  203,  203,  203,  203,  203,    0,  203,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  203,
    0,    0,  202,    0,    0,    0,    0,    0,    0,    0,
  203,  203,  203,  203,    0,    0,    0,    0,  201,  201,
  201,    0,    0,    0,    0,    0,    0,  201,    0,  201,
    0,    0,  201,  201,  201,  201,  201,  201,  201,  201,
  201,  201,    0,  201,  201,    0,  201,  201,  201,  201,
  201,  201,  201,    0,    0,  201,  201,  201,  201,  204,
    0,  201,    0,    0,    0,  201,  201,  201,  201,  201,
    0,  201,  201,  201,  201,  201,  201,  201,    0,  201,
    0,    0,    0,    0,    0,    0,    0,    0,   47,    0,
  201,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  201,  201,  201,  201,    0,    0,    0,    0,  204,
  204,  204,    0,    0,    0,    0,    0,    0,  204,   48,
  204,    0,    0,  204,  204,  204,  204,  204,  204,  204,
  204,  204,  204,    0,  204,  204,    0,  204,  204,  204,
  204,  204,  204,  204,   47,    0,  204,  204,  204,  204,
  202,   46,  204,  118,    0,    0,  204,  204,  204,  204,
  204,    0,  204,  204,  204,  204,  204,  204,  204,    0,
  204,    0,   47,    0,    0,   48,    0,    0,    0,   47,
    0,  204,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  204,  204,  204,  204,  753,    0,    0,    0,
  202,  202,  202,   48,    0,    0,    0,   46,    0,  202,
   48,  202,    0,    0,  202,  202,  202,  202,  202,  202,
  202,  202,  202,  202,    0,  202,  202,    0,  202,  202,
  202,  202,  202,  202,  202,   46,   47,  202,  202,  202,
  202,    0,   46,  202,    0,    0,    0,  202,  202,  202,
  202,  202,    0,  202,  202,  202,  202,  202,  202,  202,
    0,  202,    0,    0,    0,    0,    0,   48,    0,    0,
    0,    0,  202,    0,    0,    0,    0,  111,    0,    0,
    0,    0,    0,  202,  202,  202,  202,    0,    0,    0,
  112,    0,   22,    0,    0,    0,    0,    0,    0,   46,
    0,   23,    0,    0,    0,    0,    0,    0,  111,   24,
   25,   26,   27,   28,    0,    0,    0,    0,    0,    0,
   29,  112,    0,    0,    0,   30,   31,   32,   33,   34,
   35,   36,   37,    0,   38,   39,   40,   41,   42,   43,
  111,    0,    0,   47,    0,    0,    0,    0,   22,   44,
   45,    0,    0,  112,    0,    0,   47,   23,    0,    0,
  669,    0,    0,    0,    0,   24,   25,   26,   27,   28,
    0,    0,    0,    0,   48,    0,   22,    0,    0,   47,
    0,    0,    0,   22,    0,   23,    0,   48,    0,  383,
    0,    0,   23,   24,   25,   26,   27,   28,    0,    0,
   24,   25,   26,   27,   28,  117,   46,   47,    0,    0,
   48,    0,  136,  137,  138,    0,  139,  140,  141,   46,
  142,   35,   36,   37,    0,   38,   39,   40,   41,   42,
   43,  338,   47,    0,  145,    0,    0,    0,   48,    0,
   22,  146,   46,    0,    0,   47,    0,    0,    0,   23,
    0,    0,    0,    0,    0,    0,    0,   24,   25,   26,
   27,   28,    0,   48,    0,    0,    0,    0,    0,    0,
   46,    0,    0,    0,    0,    0,   48,    0,    0,   36,
   37,  111,   38,   39,   40,   41,   42,   43,   47,    0,
  111,    0,    0,    0,  112,   46,    0,    0,  111,  111,
  111,  111,  111,  112,    0,    0,    0,    0,   46,    0,
  170,  112,  112,  112,  112,  112,    0,    0,    0,   48,
  111,  111,   47,  111,  111,  111,  111,  111,  111,    0,
    0,    0,    0,  112,  112,    0,  112,  112,  112,  112,
  112,  112,    0,    0,    0,   47,    0,  167,    0,    0,
    0,   46,    0,   48,    0,    0,   23,    0,    0,   47,
   22,    0,    0,    0,   24,   25,   26,   27,   28,   23,
    0,    0,    0,    0,  126,   47,   48,   24,   25,   26,
   27,   28,    0,   22,    0,   46,    0,    0,  385,  127,
   48,    0,   23,    0,    0,    0,    0,  668,    0,    0,
   24,   25,   26,   27,   28,    0,   48,    0,   46,    0,
   47,   22,    0,    0,    0,   47,    0,    0,    0,    0,
   23,    0,   46,    0,    0,    0,    0,    0,   24,   25,
   26,   27,   28,    0,    0,    0,   22,    0,   46,    0,
  117,   48,    0,   47,    0,   23,   48,    0,  725,  167,
  168,    0,    0,   24,   25,   26,   27,   28,   23,  169,
    0,    0,    0,    0,   57,   47,   24,   25,   26,   27,
   28,    0,    0,   46,   48,   78,    0,    0,   46,    0,
    0,    0,    0,    0,    0,  136,  137,  138,  319,  139,
  140,  141,   22,  142,    0,    0,   48,    0,   47,    0,
    0,   23,  320,    0,  321,    0,   46,  145,    0,   24,
   25,   26,   27,   28,  146,    0,    0,    0,    0,  136,
  137,  138,  319,  139,  140,  141,   22,  142,   46,   48,
    0,    0,    0,    0,    0,   23,  545,    0,  321,    0,
    0,  145,    0,   24,   25,   26,   27,   28,  146,  167,
  168,    0,    0,    0,    0,    0,    0,    0,   23,  169,
    0,   46,    0,  167,  334,    0,   24,   25,   26,   27,
   28,    0,   23,  335,    0,    0,    0,    0,    0,   22,
   24,   25,   26,   27,   28,    0,    0,    0,   23,  710,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,   22,
    0,    0,    0,   23,    0,    0,    0,    0,   23,    0,
    0,   24,   25,   26,   27,   28,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,    0,   23,    0,    0,    0,
    0,  668,    0,    0,   24,   25,   26,   27,   28,   22,
    0,    0,    0,    0,  291,  291,    0,    0,   23,    0,
    0,    0,    0,    0,    0,    0,   24,   25,   26,   27,
   28,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  167,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,    0,    0,    0,    0,    0,    0,   24,
   25,   26,   27,   28,  291,  291,  291,    0,    0,    0,
    0,    0,    0,  291,    0,  291,    0,    0,  291,  291,
  291,  291,  291,  291,  291,  291,  291,  291,    0,  291,
  291,    0,  291,  291,  291,  291,  291,  291,  291,    0,
    0,  291,  291,  291,  291,  257,  257,  291,    0,    0,
    0,  291,  291,  291,  291,  291,    0,  291,  291,  291,
  291,  291,  291,  291,    0,  291,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  291,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  291,  291,  291,
  291,    0,    0,    0,    0,  257,  257,  257,    0,    0,
    0,    0,    0,    0,  257,    0,  257,    0,    0,  257,
  257,  257,  257,  257,  257,  257,  257,  257,  257,    0,
  257,  257,    0,  257,  257,  257,  257,  257,  257,  257,
    0,    0,  257,  257,  257,  257,  260,  260,  257,    0,
    0,    0,  257,  257,  257,  257,  257,    0,  257,  257,
  257,  257,  257,  257,  257,    0,  257,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  257,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  257,  257,
  257,  257,    0,    0,    0,    0,  260,  260,  260,    0,
    0,    0,    0,    0,    0,  260,    0,  260,    0,    0,
  260,  260,  260,  260,  260,  260,  260,  260,  260,  260,
    0,  260,  260,    0,  260,  260,  260,  260,  260,  260,
  260,    0,    0,  260,  260,  260,  260,  265,  265,  260,
    0,    0,    0,  260,  260,  260,  260,  260,    0,  260,
  260,  260,  260,  260,  260,  260,    0,  260,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  260,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  260,
  260,  260,  260,    0,    0,    0,    0,  265,  265,  265,
    0,    0,    0,    0,    0,    0,  265,    0,  265,    0,
    0,  265,  265,  265,  265,  265,  265,  265,  265,  265,
  265,    0,  265,  265,    0,  265,  265,  265,  265,  265,
  265,  265,    0,    0,  265,  265,  265,  265,  279,  279,
  265,    0,    0,    0,  265,  265,  265,  265,  265,    0,
  265,  265,  265,  265,  265,  265,  265,    0,  265,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  265,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  265,  265,  265,  265,    0,    0,    0,    0,  279,  279,
  279,    0,    0,    0,    0,    0,    0,  279,    0,  279,
    0,    0,  279,  279,  279,  279,  279,  279,  279,  279,
  279,  279,    0,  279,  279,    0,  279,  279,  279,  279,
  279,  279,  279,    0,    0,  279,  279,  279,  279,  261,
  261,  279,    0,    0,    0,  279,  279,  279,  279,  279,
    0,  279,  279,  279,  279,  279,  279,  279,    0,  279,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  279,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  279,  279,  279,  279,    0,    0,    0,    0,  261,
  261,  261,    0,    0,    0,    0,    0,    0,  261,    0,
  261,    0,    0,  261,  261,  261,  261,  261,  261,  261,
  261,  261,  261,    0,  261,  261,    0,  261,  261,  261,
  261,  261,  261,  261,    0,    0,  261,  261,  261,  261,
  262,  262,  261,    0,    0,    0,  261,  261,  261,  261,
  261,    0,  261,  261,  261,  261,  261,  261,  261,    0,
  261,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  261,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  261,  261,  261,  261,    0,    0,    0,    0,
  262,  262,  262,    0,    0,    0,    0,    0,    0,  262,
    0,  262,    0,    0,  262,  262,  262,  262,  262,  262,
  262,  262,  262,  262,    0,  262,  262,    0,  262,  262,
  262,  262,  262,  262,  262,    0,    0,  262,  262,  262,
  262,  263,  263,  262,    0,    0,    0,  262,  262,  262,
  262,  262,    0,  262,  262,  262,  262,  262,  262,  262,
    0,  262,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  262,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  262,  262,  262,  262,    0,    0,    0,
    0,  263,  263,  263,    0,    0,    0,    0,    0,    0,
  263,    0,  263,    0,    0,  263,  263,  263,  263,  263,
  263,  263,  263,  263,  263,    0,  263,  263,    0,  263,
  263,  263,  263,  263,  263,  263,    0,    0,  263,  263,
  263,  263,  280,  280,  263,    0,    0,    0,  263,  263,
  263,  263,  263,    0,  263,  263,  263,  263,  263,  263,
  263,    0,  263,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  263,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  263,  263,  263,  263,    0,    0,
    0,    0,  280,  280,  280,    0,    0,    0,    0,    0,
    0,  280,    0,  280,    0,    0,  280,  280,  280,  280,
  280,  280,  280,  280,  280,  280,    0,  280,  280,    0,
  280,  280,  280,  280,  280,  280,  280,    0,    0,  280,
  280,  280,  280,  264,  264,  280,    0,    0,    0,  280,
  280,  280,  280,  280,    0,  280,  280,  280,  280,  280,
  280,  280,    0,  280,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  280,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  280,  280,  280,  280,    0,
    0,    0,    0,  264,  264,  264,    0,    0,    0,    0,
    0,    0,  264,    0,  264,    0,    0,  264,  264,  264,
  264,  264,  264,  264,  264,  264,  264,    0,  264,  264,
    0,  264,  264,  264,  264,  264,  264,  264,    0,    0,
  264,  264,  264,  264,  248,    0,  264,    0,    0,    0,
  264,  264,  264,  264,  264,    0,  264,  264,  264,  264,
  264,  264,  264,    0,  264,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  264,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  264,  264,  264,  264,
    0,    0,    0,    0,  249,  250,  251,    0,    0,    0,
    0,    0,    0,  252,    0,  253,    0,    0,  254,  255,
  256,  257,  258,  259,  260,  261,  262,  263,    0,  264,
  265,    0,  266,  267,  268,  269,  270,  271,  272,    0,
    0,  273,  274,  275,  276,  207,    0,  277,    0,    0,
    0,  278,  279,  280,  281,  282,    0,  283,  284,  285,
  286,  287,  288,  289,    0,  290,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  291,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  292,  293,  294,
  295,    0,    0,    0,    0,  207,  207,  207,    0,    0,
    0,    0,    0,    0,  207,    0,  207,    0,    0,  207,
  207,  207,  207,  207,  207,  207,  207,  207,  207,    0,
  207,  207,    0,  207,  207,  207,  207,  207,  207,  207,
    0,    0,  207,  207,  207,  207,  208,    0,  207,    0,
    0,    0,  207,  207,  207,  207,  207,    0,  207,  207,
  207,  207,  207,  207,  207,    0,  207,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  207,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  207,  207,
  207,  207,    0,    0,    0,    0,  208,  208,  208,    0,
    0,    0,    0,    0,    0,  208,    0,  208,    0,    0,
  208,  208,  208,  208,  208,  208,  208,  208,  208,  208,
    0,  208,  208,    0,  208,  208,  208,  208,  208,  208,
  208,    0,    0,  208,  208,  208,  208,  209,    0,  208,
    0,    0,    0,  208,  208,  208,  208,  208,    0,  208,
  208,  208,  208,  208,  208,  208,    0,  208,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  208,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  208,
  208,  208,  208,    0,    0,    0,    0,  209,  209,  209,
    0,    0,    0,    0,    0,    0,  209,    0,  209,    0,
    0,  209,  209,  209,  209,  209,  209,  209,  209,  209,
  209,    0,  209,  209,    0,  209,  209,  209,  209,  209,
  209,  209,    0,    0,  209,  209,  209,  209,  210,    0,
  209,    0,    0,    0,  209,  209,  209,  209,  209,    0,
  209,  209,  209,  209,  209,  209,  209,    0,  209,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  209,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  209,  209,  209,  209,    0,    0,    0,    0,  210,  210,
  210,    0,    0,    0,    0,    0,    0,  210,    0,  210,
    0,    0,  210,  210,  210,  210,  210,  210,  210,  210,
  210,  210,    0,  210,  210,    0,  210,  210,  210,  210,
  210,  210,  210,    0,    0,  210,  210,  210,  210,    0,
    0,  210,    0,    0,    0,  210,  210,  210,  210,  210,
    0,  210,  210,  210,  210,  210,  210,  210,    0,  210,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  210,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  210,  210,  210,  210,  253,    0,    0,  254,  255,
  256,  257,  258,  259,  260,  261,  262,  263,    0,  264,
  265,    0,  266,  267,  268,  269,  270,  271,  272,    0,
    0,  273,  274,  275,  276,    0,    0,  277,    0,    0,
    0,  278,  279,  280,  281,  282,    0,  283,  284,  285,
  286,  287,  288,  289,    0,  290,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  291,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  292,  293,  294,
  295,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   51,   60,    6,   33,  457,   40,    0,   40,  296,
  123,   44,   60,  123,  469,  123,  123,   41,  123,  134,
   44,  123,   44,   44,  681,  525,  526,  123,  528,  124,
   44,  276,   93,   91,   44,  280,   86,   33,  133,   46,
  266,  627,   44,    6,    7,  702,   53,   54,   40,  502,
   57,   54,   44,  220,   40,  222,  343,  355,   44,  285,
  200,  359,  123,   70,  362,  123,   42,   41,  183,   42,
   44,  238,    0,  371,   61,  123,   83,  290,  291,   93,
   60,   88,  270,  271,   91,  200,  181,  182,   95,   61,
   53,   54,  125,  100,   57,  102,   40,  552,  312,  312,
  124,  687,    0,  125,  125,  112,  113,  247,  115,  244,
  610,   91,  612,  613,   61,  125,   60,   93,   91,  776,
  235,  236,  367,  125,   72,   73,  302,   41,   91,  582,
   44,   44,  585,  125,   42,   41,  290,  291,   44,  125,
  294,  295,   61,  123,  257,  321,  322,   91,   96,   62,
  157,   71,  159,   73,   62,  322,  255,  444,  257,  299,
   41,  301,  260,   44,  274,  264,  274,  274,  303,  274,
  294,  295,  274,  180,    0,  272,  124,  464,  274,  123,
  680,  101,   61,  103,   40,   41,   40,   41,   44,   42,
   44,   44,  199,   41,  197,   41,   44,   61,   44,  257,
  258,  259,  342,  261,  262,  263,  280,  265,   41,  344,
  312,   44,  257,  220,  272,  273,  312,  290,  291,  303,
  304,  279,  306,  307,  308,  309,  310,  311,  286,   41,
  685,   41,   44,   41,   44,   42,   44,   44,  272,  287,
  272,  274,   41,   42,  294,  274,  253,  254,  255,  256,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  266,
  267,  268,  269,  550,  274,  367,  273,  274,   42,  724,
  277,  367,   41,   42,   41,  268,  269,   44,  274,  272,
  273,  277,  275,  735,  264,  292,  293,  294,   41,   42,
  457,  294,  459,  273,  461,  288,  289,  304,   42,  439,
   44,  281,  282,  283,  284,  285,   41,   40,  366,   44,
  450,   41,   42,  257,  258,  259,   42,  261,  262,  263,
  313,  265,  380,  381,  382,   42,  466,   44,  272,  273,
  465,  294,   42,   41,   44,  279,   44,  624,   41,   42,
  268,  269,  286,   61,  272,  273,   40,  275,  355,   42,
  260,   44,  359,  260,   42,  362,   44,   40,  365,  123,
  288,  289,   41,   42,  371,  372,  267,  374,    0,  302,
  268,  269,  267,   40,  272,  273,  383,  275,  110,  152,
  153,  123,  389,  123,   40,  313,  436,  123,  368,  312,
  288,  289,   40,   60,   40,   40,   58,  368,  538,   58,
  540,   44,   62,   58,   61,   40,  413,  274,   99,   40,
  257,  367,  367,  548,   44,  313,   44,  274,  260,  151,
  152,  153,  366,  430,   91,  257,  320,  434,  435,  436,
  274,  408,  435,  436,  166,   61,  380,  381,  382,  446,
  447,  448,  268,  269,  274,  375,  272,  273,    0,  275,
  375,  375,  459,  257,   44,  367,  123,  257,  367,   44,
   40,  124,  288,  289,  367,  156,   44,  158,  273,   44,
   44,  434,  435,  436,   42,  207,  208,  209,   44,   60,
  620,  172,  622,   44,  375,  375,  375,  313,  375,  375,
  375,  608,  609,  500,  611,  502,  274,  229,  505,  506,
  507,  508,  509,  510,  511,  512,  513,  514,  515,  516,
   91,   41,  607,  375,  375,  375,  375,  249,  257,  251,
  527,   44,  529,  530,  375,  216,  375,  530,  219,  303,
  304,  367,  306,  307,  308,  309,  310,  311,  270,  271,
  272,  257,  123,  275,  711,  257,  278,  279,  280,  281,
  282,  283,  284,  285,  286,  287,  288,  289,  673,  676,
  266,  678,  679,   44,  527,  367,  529,  530,  735,   44,
   44,   44,   44,  367,   44,  582,   44,  309,  585,  674,
   44,   44,  677,   44,   44,   44,   44,   40,   44,   44,
  257,  258,  259,   42,  261,  262,  263,   44,  265,  606,
   44,  716,   44,  718,  721,  272,  273,  614,   44,   44,
  257,   60,  279,  257,   91,   44,   44,   44,   44,  286,
  627,    0,   44,   44,  719,   44,   44,   44,  257,   44,
    0,  746,   44,   44,   44,   44,  268,  269,   44,    0,
  272,  273,   91,  275,   44,   91,   44,   44,  367,   44,
   44,  614,  367,  385,  125,   44,  288,  289,  349,   44,
  367,  668,  257,  354,  257,  367,  357,  358,  125,  360,
  361,   44,  363,  364,  123,  366,  367,  368,  369,  370,
  687,  313,  373,  264,  375,  376,  377,  378,   93,  257,
  258,  259,  273,  261,  262,  263,   40,  265,  257,  366,
  281,  282,  283,  284,  285,   40,  257,  368,  715,   40,
  713,  279,   40,  380,  381,  382,  268,  269,  286,   40,
  272,  273,  375,  275,  375,  375,   44,  257,  258,  259,
    0,  261,  262,  263,  272,  265,  288,  289,  272,  272,
  272,  432,   40,   10,   60,   74,  276,   40,   41,  279,
  166,   44,  165,  760,  335,  336,  286,   19,  765,  766,
  767,  313,  224,  495,  496,  497,   83,  459,   42,   62,
   44,  194,  504,  180,   51,   91,  298,  670,  732,  199,
  471,  298,  346,  715,  475,  461,   60,  478,  604,  260,
  481,  687,   -1,   -1,   -1,   -1,  487,  488,   -1,  490,
   93,  533,   -1,  260,   -1,  276,   -1,  123,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   91,   -1,  276,
   -1,   -1,   -1,  272,  273,  125,  517,  518,  519,   -1,
  279,   -1,  125,   -1,   -1,   -1,   -1,  286,   40,  310,
  311,   -1,   -1,  314,  315,  316,  317,  318,  319,  123,
   -1,   -1,   -1,  310,  311,  587,   -1,  314,  315,  316,
  317,  318,  319,  554,   -1,  556,  557,   -1,  559,  560,
   -1,  562,  563,  605,  565,  566,  567,  568,  569,   -1,
   -1,  572,   -1,  574,  575,  576,  577,   -1,   -1,  268,
  269,   -1,  583,  272,  273,   -1,  275,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,   -1,  268,  269,  288,
  289,  272,  273,   -1,  275,   -1,   -1,  366,  288,  289,
  652,  653,   -1,   -1,   -1,   -1,   -1,  288,  289,   40,
   -1,  380,  381,  382,  313,   -1,   -1,   -1,  629,   -1,
   -1,  632,   -1,  313,  635,   -1,   -1,  638,  264,   -1,
   -1,   -1,  313,  644,  645,   -1,  647,  273,   -1,   -1,
  260,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,  663,  664,  665,   -1,  276,  709,   -1,  670,
  273,  274,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,
  310,  311,  286,   -1,  314,  315,  316,  317,  318,  319,
   40,   -1,  713,   -1,  756,  757,  758,   -1,   -1,   -1,
  323,  324,  325,   -1,  350,   -1,  768,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,  375,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,  366,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,  395,  265,   -1,   -1,  380,  381,  382,   -1,
   -1,   40,   -1,  406,  407,  408,  409,  279,   -1,   -1,
   -1,  323,  324,  325,  286,   -1,   -1,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   -1,   -1,  360,  361,
  362,  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  290,  291,   -1,  395,  294,  295,  296,  297,  298,  299,
  300,  301,   40,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,
   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,   -1,  348,  349,   -1,
  351,  352,  353,  354,  355,  356,  357,   -1,   -1,  360,
  361,  362,  363,  273,  274,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,  377,  378,  379,  380,
  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   40,   -1,  406,  407,  408,  409,   -1,
   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,
   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,
  340,  341,  342,  343,  344,  345,  346,   -1,  348,  349,
   -1,  351,  352,  353,  354,  355,  356,  357,   -1,   -1,
  360,  361,  362,  363,  273,  274,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,  377,  378,  379,
  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   40,   -1,  406,  407,  408,  409,
   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,
   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,   -1,
   -1,  360,  361,  362,  363,  273,  274,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   40,   -1,  406,  407,  408,
  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,  274,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,  406,  407,
  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,  273,  274,  366,
   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,  406,
  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,
   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,
   -1,  337,  338,  339,  340,  341,  342,  343,  344,  345,
  346,   -1,  348,  349,   -1,  351,  352,  353,  354,  355,
  356,  357,   -1,   -1,  360,  361,  362,  363,  273,  274,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,  377,  378,  379,  380,  381,  382,   -1,  384,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,
  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,
  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,
   -1,   -1,  337,  338,  339,  340,  341,  342,  343,  344,
  345,  346,   -1,  348,  349,   -1,  351,  352,  353,  354,
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,  273,
  274,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,
   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,
  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   -1,
  334,   -1,   -1,  337,  338,  339,  340,  341,  342,  343,
  344,  345,  346,   -1,  348,  349,   -1,  351,  352,  353,
  354,  355,  356,  357,   -1,   -1,  360,  361,  362,  363,
  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,  377,  378,  379,  380,  381,  382,   -1,
  384,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  395,  385,  386,  387,  388,  389,  390,  391,  392,
  393,  394,  406,  407,  408,  409,   -1,   -1,   -1,   -1,
  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,
   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,   -1,   -1,  360,  361,  362,
  363,  273,  274,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,    0,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   42,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   60,   42,   -1,   -1,   -1,
  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,   -1,  348,  349,   -1,  351,
  352,  353,  354,  355,  356,  357,   91,   -1,  360,  361,
  362,  363,  273,  274,  366,  261,  262,   -1,  370,  371,
  372,  373,  374,   -1,  376,  377,  378,  379,  380,  381,
  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,  123,   -1,
   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,   42,   -1,
   44,   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,
   -1,   -1,  323,  324,  325,   -1,   60,   -1,   -1,   -1,
   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,  340,
  341,  342,  343,  344,  345,  346,   -1,  348,  349,   -1,
  351,  352,  353,  354,  355,  356,  357,   91,   -1,  360,
  361,  362,  363,   -1,   -1,  366,   60,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,  377,  378,  379,  380,
  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,  395,   -1,   -1,   91,   -1,   -1,
   -1,  387,  388,  389,  390,  406,  407,  408,  409,   -1,
  396,  397,  398,  399,  400,  401,  402,  403,  404,  405,
   -1,   -1,   -1,  268,  269,   -1,   -1,  272,  273,  123,
  275,   60,  257,  258,  259,   -1,  261,  262,  263,   41,
  265,   -1,   -1,  288,  289,   60,   -1,  272,  273,   -1,
   -1,  257,  258,  259,  279,  261,  262,  263,   60,  265,
   -1,  286,   91,   -1,   -1,   -1,  272,  273,  313,   -1,
  268,  269,   -1,  279,  272,  273,   91,  275,  303,  304,
  286,  306,  307,  308,  309,  310,  311,   -1,   -1,   91,
  288,  289,   -1,   -1,  123,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   -1,   -1,  123,   -1,
   -1,   -1,   -1,  125,   -1,  313,  276,   -1,   -1,  279,
   -1,  123,   -1,  257,  258,  259,  286,  261,  262,  263,
   -1,  265,  260,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,  366,   -1,   -1,   -1,  279,   -1,   -1,  276,   -1,
   -1,   -1,  286,   -1,   -1,  380,  381,  382,   -1,   -1,
  366,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,  380,  381,  382,   -1,  272,  273,
   -1,   -1,  310,  311,   -1,  279,  314,  315,  316,  317,
  318,  319,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,  303,
  304,   -1,  306,  307,  308,  309,  310,  311,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  366,   -1,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,  380,  381,  382,  264,
   -1,   -1,  281,  282,  283,  284,  285,   -1,  273,   -1,
   -1,  273,  264,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  273,  366,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,  380,  381,  382,   -1,
   -1,  293,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  323,  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,
  332,  350,  334,   -1,   -1,  337,  338,  339,  340,  341,
  342,  343,  344,  345,  346,  350,  348,  349,   -1,  351,
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
   -1,   -1,   -1,   -1,  395,   -1,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,   -1,
   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,   -1,
   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,  339,
  340,  341,  342,  343,  344,  345,  346,   -1,  348,  349,
   -1,  351,  352,  353,  354,  355,  356,  357,   -1,   -1,
  360,  361,  362,  363,  273,   -1,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,  377,  378,  379,
  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,
   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,
   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,   -1,
   -1,  360,  361,  362,  363,  273,   -1,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,
  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
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
  377,  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,
  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,
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
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,
  324,  325,   -1,   -1,   -1,   -1,   -1,   -1,  332,   91,
  334,   -1,   -1,  337,  338,  339,  340,  341,  342,  343,
  344,  345,  346,   -1,  348,  349,   -1,  351,  352,  353,
  354,  355,  356,  357,   60,   -1,  360,  361,  362,  363,
  273,  123,  366,   41,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,  377,  378,  379,  380,  381,  382,   -1,
  384,   -1,   60,   -1,   -1,   91,   -1,   -1,   -1,   60,
   -1,  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  406,  407,  408,  409,   41,   -1,   -1,   -1,
  323,  324,  325,   91,   -1,   -1,   -1,  123,   -1,  332,
   91,  334,   -1,   -1,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,   -1,  348,  349,   -1,  351,  352,
  353,  354,  355,  356,  357,  123,   60,  360,  361,  362,
  363,   -1,  123,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  377,  378,  379,  380,  381,  382,
   -1,  384,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,   -1,  395,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,   -1,   -1,  406,  407,  408,  409,   -1,   -1,   -1,
   60,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,  123,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   91,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,
  292,   91,   -1,   -1,   -1,  297,  298,  299,  300,  301,
  302,  303,  304,   -1,  306,  307,  308,  309,  310,  311,
  123,   -1,   -1,   60,   -1,   -1,   -1,   -1,  264,  321,
  322,   -1,   -1,  123,   -1,   -1,   60,  273,   -1,   -1,
   41,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   91,   -1,  264,   -1,   -1,   60,
   -1,   -1,   -1,  264,   -1,  273,   -1,   91,   -1,  305,
   -1,   -1,  273,  281,  282,  283,  284,  285,   -1,   -1,
  281,  282,  283,  284,  285,  293,  123,   60,   -1,   -1,
   91,   -1,  257,  258,  259,   -1,  261,  262,  263,  123,
  265,  302,  303,  304,   -1,  306,  307,  308,  309,  310,
  311,  276,   60,   -1,  279,   -1,   -1,   -1,   91,   -1,
  264,  286,  123,   -1,   -1,   60,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,  303,
  304,  264,  306,  307,  308,  309,  310,  311,   60,   -1,
  273,   -1,   -1,   -1,  264,  123,   -1,   -1,  281,  282,
  283,  284,  285,  273,   -1,   -1,   -1,   -1,  123,   -1,
  125,  281,  282,  283,  284,  285,   -1,   -1,   -1,   91,
  303,  304,   60,  306,  307,  308,  309,  310,  311,   -1,
   -1,   -1,   -1,  303,  304,   -1,  306,  307,  308,  309,
  310,  311,   -1,   -1,   -1,   60,   -1,  264,   -1,   -1,
   -1,  123,   -1,   91,   -1,   -1,  273,   -1,   -1,   60,
  264,   -1,   -1,   -1,  281,  282,  283,  284,  285,  273,
   -1,   -1,   -1,   -1,  278,   60,   91,  281,  282,  283,
  284,  285,   -1,  264,   -1,  123,   -1,   -1,  305,  293,
   91,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   91,   -1,  123,   -1,
   60,  264,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,
  273,   -1,  123,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,  264,   -1,  123,   -1,
  293,   91,   -1,   60,   -1,  273,   91,   -1,   93,  264,
  265,   -1,   -1,  281,  282,  283,  284,  285,  273,  274,
   -1,   -1,   -1,   -1,  292,   60,  281,  282,  283,  284,
  285,   -1,   -1,  123,   91,  125,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,   -1,   -1,   91,   -1,   60,   -1,
   -1,  273,  274,   -1,  276,   -1,  123,  279,   -1,  281,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,  257,
  258,  259,  260,  261,  262,  263,  264,  265,  123,   91,
   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,  276,   -1,
   -1,  279,   -1,  281,  282,  283,  284,  285,  286,  264,
  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,  123,   -1,  264,  265,   -1,  281,  282,  283,  284,
  285,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,  264,
  281,  282,  283,  284,  285,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,   -1,
   -1,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,  264,
   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  323,  324,  325,   -1,   -1,   -1,
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
  356,  357,   -1,   -1,  360,  361,  362,  363,  273,  274,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,  377,  378,  379,  380,  381,  382,   -1,  384,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  406,  407,  408,  409,   -1,   -1,   -1,   -1,  323,  324,
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
  360,  361,  362,  363,  273,   -1,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,  377,  378,  379,
  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,  409,
   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,   -1,
   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,   -1,
   -1,  360,  361,  362,  363,  273,   -1,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,
  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,   -1,
   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,  337,
  338,  339,  340,  341,  342,  343,  344,  345,  346,   -1,
  348,  349,   -1,  351,  352,  353,  354,  355,  356,  357,
   -1,   -1,  360,  361,  362,  363,  273,   -1,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,  377,
  378,  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,
  408,  409,   -1,   -1,   -1,   -1,  323,  324,  325,   -1,
   -1,   -1,   -1,   -1,   -1,  332,   -1,  334,   -1,   -1,
  337,  338,  339,  340,  341,  342,  343,  344,  345,  346,
   -1,  348,  349,   -1,  351,  352,  353,  354,  355,  356,
  357,   -1,   -1,  360,  361,  362,  363,  273,   -1,  366,
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
  355,  356,  357,   -1,   -1,  360,  361,  362,  363,   -1,
   -1,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,  377,  378,  379,  380,  381,  382,   -1,  384,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  395,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  406,  407,  408,  409,  334,   -1,   -1,  337,  338,
  339,  340,  341,  342,  343,  344,  345,  346,   -1,  348,
  349,   -1,  351,  352,  353,  354,  355,  356,  357,   -1,
   -1,  360,  361,  362,  363,   -1,   -1,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,  377,  378,
  379,  380,  381,  382,   -1,  384,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  395,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  406,  407,  408,
  409,
  };

#line 1133 "Iril/IR/IR.jay"

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
