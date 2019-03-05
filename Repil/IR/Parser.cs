// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Repil/IR/IR.jay"
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Linq;

using Repil.Types;

#pragma warning disable 219,414

namespace Repil.IR
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
//t    "visibility : PRIVATE",
//t    "metadata_args : metadata_arg",
//t    "metadata_args : metadata_args ',' metadata_arg",
//t    "metadata_arg : SYMBOL ':' SYMBOL",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL",
//t    "metadata_arg : SYMBOL ':' STRING",
//t    "metadata_arg : SYMBOL ':' constant",
//t    "metadata_arg : SYMBOL ':' typed_constant",
//t    "metadata_arg : TYPE ':' META_SYMBOL",
//t    "metadata_arg : ALIGN ':' constant",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' metadata_value_args ')'",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL '(' ')'",
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
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage parameter_attribute return_type GLOBAL_SYMBOL parameters attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention parameter_attribute return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
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
    null,null,null,null,"'{'",null,"'}'",null,null,null,null,null,null,
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
    "PRIVATE","INTERNAL","EXTERNAL","FASTCC","SIGNEXT","ZEROEXT",
    "VOLATILE","RETURNED","NONNULL","NOCAPTURE","WRITEONLY","READONLY",
    "READNONE","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND",
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","SEQ_CST","RET","BR",
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
#line 60 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 64 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 68 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 72 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 76 "Repil/IR/IR.jay"
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
#line 96 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 100 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 109 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 121 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 125 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 129 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 133 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 137 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 141 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 145 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 149 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 153 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-7+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-5+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 27:
#line 157 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 28:
#line 158 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 162 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 30:
#line 163 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 31:
#line 167 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 32:
  case_32();
  break;
case 33:
  case_33();
  break;
case 34:
#line 184 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 185 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 186 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 187 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 188 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 39:
#line 189 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 40:
#line 190 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 41:
#line 194 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 42:
#line 198 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 43:
#line 205 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 44:
#line 209 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 45:
#line 216 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 46:
#line 220 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 47:
#line 224 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 48:
#line 228 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 49:
#line 232 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 67:
#line 265 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 68:
#line 269 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 69:
#line 273 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 70:
#line 280 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 71:
#line 284 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 289 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 76:
#line 295 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 77:
#line 296 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 78:
#line 297 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 79:
#line 298 "Repil/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 80:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 81:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 83:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 318 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 85:
#line 322 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 86:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 89:
#line 341 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 90:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true);
    }
  break;
case 91:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 93:
#line 360 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true);
    }
  break;
case 94:
#line 364 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 95:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-7+yyTop]);
    }
  break;
case 96:
#line 372 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-8+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 97:
#line 376 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 98:
#line 380 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 99:
#line 384 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop]);
    }
  break;
case 100:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-11+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 101:
#line 392 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-10+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 102:
#line 399 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 103:
#line 403 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 104:
#line 407 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 105:
#line 411 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 106:
#line 412 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 107:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 423 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 430 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 434 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 111:
#line 438 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 112:
#line 442 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 114:
#line 450 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 115:
#line 454 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 116:
#line 455 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 117:
#line 456 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 118:
#line 457 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 119:
#line 458 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 120:
#line 459 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 121:
#line 460 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 122:
#line 461 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 128:
#line 479 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 129:
#line 480 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 130:
#line 481 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 131:
#line 482 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 132:
#line 483 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 133:
#line 484 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 134:
#line 485 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 135:
#line 486 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 136:
#line 487 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 137:
#line 488 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 138:
#line 492 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 139:
#line 493 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 140:
#line 494 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 141:
#line 495 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 142:
#line 496 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 143:
#line 497 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 144:
#line 498 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 145:
#line 499 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 146:
#line 500 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 147:
#line 501 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 148:
#line 502 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 149:
#line 503 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 150:
#line 504 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 151:
#line 505 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 152:
#line 506 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 153:
#line 507 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 155:
#line 512 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 156:
#line 513 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 157:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 525 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 160:
#line 529 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 161:
#line 533 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 162:
#line 537 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 163:
#line 541 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 165:
#line 549 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 166:
#line 550 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 167:
#line 551 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 168:
#line 552 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 169:
#line 553 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 170:
#line 554 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 171:
#line 555 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 172:
#line 556 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 173:
#line 557 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 174:
#line 564 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 177:
#line 582 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 589 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 593 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 600 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 622 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 626 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 637 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 188:
#line 641 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 645 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 190:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 663 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 667 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 671 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 675 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 683 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 198:
#line 684 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 199:
#line 691 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 695 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 702 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 202:
#line 706 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 203:
#line 710 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 204:
#line 714 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 205:
#line 718 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 206:
#line 722 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 207:
#line 726 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 209:
#line 731 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 210:
#line 735 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 739 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 743 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 747 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 764 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 774 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 221:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 785 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 810 "Repil/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 230:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 829 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 234:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = UnreachableInstruction.Unreachable;
    }
  break;
case 235:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 240:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 241:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 243:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 244:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 245:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 246:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 247:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 248:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 249:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 250:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 251:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 252:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 253:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 952 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 956 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 960 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 964 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 968 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 972 "Repil/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 976 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 270:
#line 980 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 271:
#line 984 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 272:
#line 988 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 273:
#line 992 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 996 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1000 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1004 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 1008 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 1012 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1016 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1020 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1024 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1028 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1032 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1036 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1040 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1044 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1048 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 288:
#line 1052 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 289:
#line 1056 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1060 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1064 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1068 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1072 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1076 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1080 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 296:
#line 1084 "Repil/IR/IR.jay"
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
#line 78 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 83 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 88 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 102 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 111 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_32()
#line 172 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_33()
#line 177 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,    6,   10,   10,   16,   16,
   15,    9,    9,   17,   17,   17,   17,   17,   17,   17,
   17,   17,   13,   13,    8,    8,    8,    8,    8,   21,
   21,   21,    7,    7,   23,   23,   23,   23,   23,   23,
   23,   23,   23,   23,   23,   23,    3,    3,    3,   24,
   24,   25,   25,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   26,   26,   27,   27,    4,
    4,    4,    4,    4,    4,    4,    4,    4,    4,    4,
    4,    5,    5,    5,   28,   28,   33,   33,   34,   34,
   34,   34,   35,   35,   31,   31,   31,   31,   31,   31,
   31,   31,   14,   14,   29,   29,   36,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   38,   38,   38,
   38,   38,   38,   38,   38,   38,   38,   38,   38,   38,
   38,   38,   38,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   41,   18,   18,   18,   18,   18,   18,
   18,   18,   18,   42,   22,   22,   43,   40,   40,   19,
   44,   39,   39,   30,   30,   45,   45,   45,   45,   46,
   46,   48,   48,   48,   48,   50,   51,   51,   52,   52,
   53,   53,   53,   53,   53,   53,   53,   54,   54,   54,
   54,   54,   54,   20,   20,   55,   55,   56,   56,   57,
   58,   58,   59,   60,   60,   61,   61,   32,   62,   47,
   47,   47,   47,   47,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    8,    1,    1,    1,    1,
    1,    1,    3,    3,    3,    3,    3,    3,    3,    3,
    6,    5,    2,    3,    1,    2,    3,    3,    3,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    3,    1,    1,    1,    4,    2,    3,    5,    1,
    3,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    4,    2,    1,    5,    5,    1,    3,    1,    1,    9,
    9,   10,   10,   11,    9,   10,   11,   11,   11,   13,
   12,    5,    6,    6,    3,    2,    1,    3,    1,    2,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    6,    9,    6,    6,
    3,    3,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    2,    1,    2,    1,    3,    2,
    1,    1,    3,    1,    2,    2,    3,    1,    2,    1,
    2,    1,    2,    3,    4,    1,    3,    2,    1,    3,
    2,    3,    3,    3,    2,    4,    5,    1,    1,    6,
    9,    6,    6,    1,    3,    1,    1,    1,    3,    5,
    1,    2,    3,    1,    2,    1,    1,    1,    1,    2,
    7,    2,    7,    1,    5,    6,    5,    5,    5,    6,
    4,    4,    5,    6,    5,    6,    6,    6,    7,    5,
    6,    7,    4,    5,    6,    5,    2,    5,    4,    4,
    4,    4,    5,    6,    7,    6,    6,    4,    7,    8,
    5,    6,    5,    5,    6,    3,    4,    5,    6,    7,
    4,    5,    6,    6,    4,    5,    7,    8,    5,    6,
    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   73,   83,   76,   77,   78,   79,   75,    0,   30,   29,
    0,    0,    0,   74,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  123,  124,   27,   28,   31,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   67,    0,
    0,    0,    0,    0,    0,   82,  228,  120,  121,  122,
  115,  116,  118,  117,  119,    0,    0,    0,    0,    0,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   68,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   89,
   80,    0,    0,   86,    0,    0,    0,  167,  168,  166,
  169,  170,  171,  165,  156,  155,  173,  172,    0,    0,
    0,    0,    0,    0,    0,    0,  154,    0,    0,    0,
    0,    0,    0,    0,    0,   32,    0,    0,    0,   52,
   51,   13,    0,    0,   45,   50,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  111,  112,  106,    0,    0,
  107,  127,    0,    0,  125,   81,    0,    0,    0,    0,
    0,    0,   65,   57,   55,   56,   58,   59,   60,   61,
    0,   53,    0,    0,    0,    0,  178,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   15,
    0,    0,    0,   46,   14,    0,  175,    0,   84,   69,
   85,    0,    0,    0,    0,    0,  113,    0,  105,    0,
    0,    0,    0,  126,   87,    0,    0,    0,    0,   12,
   54,    0,    0,    0,    0,  163,    0,  161,  162,    0,
    0,    0,    0,    0,    0,    0,   36,    0,   34,    0,
   37,   38,   39,   40,   33,   17,   16,   49,   48,   47,
    0,    0,    0,    0,    0,    0,    0,  114,  108,    0,
    0,   43,    0,    0,   62,  217,  216,    0,  214,    0,
    0,    0,    0,  179,    0,    0,    0,   26,    0,    0,
    0,    0,  180,    0,    0,    0,    0,    0,    0,    0,
  234,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  184,    0,    0,  190,    0,
    0,    0,    0,    0,    0,    0,   44,    0,   66,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   42,    0,    0,    0,    0,    0,  232,    0,    0,  230,
    0,  226,  227,    0,    0,  224,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  229,  257,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  128,  129,  130,  131,  132,  133,  134,  135,
  136,  137,    0,  138,  139,  150,  151,  152,  153,  141,
  143,  144,  145,  146,  142,  140,  148,  149,  147,    0,
    0,    0,    0,    0,    0,    0,   95,  185,    0,  191,
    0,    0,    0,    0,    0,    0,    0,   90,    0,   91,
  215,    0,  160,  157,  159,    0,    0,    0,    0,   41,
   93,    0,    0,    0,  174,    0,    0,    0,    0,  225,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  218,    0,
  196,    0,    0,    0,    0,    0,    0,    0,    0,   96,
    0,    0,    0,    0,   92,    0,    0,    0,   94,   98,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  253,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   97,   99,    0,    0,  181,    0,
  182,    0,    0,  235,    0,  254,  289,    0,  263,  274,
    0,  258,  292,    0,  278,  256,  294,  286,  282,    0,
    0,  271,    0,  239,  238,  273,  295,    0,    0,  237,
    0,  164,  177,    0,    0,    0,    0,    0,    0,    0,
    0,  219,    0,    0,  198,    0,    0,  199,    0,    0,
  243,    0,    0,    0,    0,    0,  101,    0,  158,    0,
    0,    0,    0,  221,  236,  290,  275,  279,  283,  272,
  240,  267,  284,    0,    0,    0,    0,    0,    0,  266,
  255,    0,    0,    0,    0,  201,    0,  197,    0,    0,
  244,    0,    0,  251,    0,  100,  183,  231,    0,  233,
  222,    0,  269,    0,  287,    0,  220,  280,    0,  209,
  203,    0,    0,    0,    0,  208,  204,  202,  200,    0,
  252,  223,  270,  288,  206,    0,    0,    0,    0,    0,
  207,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  213,  210,  212,    0,    0,  211,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  191,  153,  145,   50,
  154,  561,  233,   51,   52,   36,  146,  137,  712,  288,
  155,  649,  192,   61,   62,  113,  114,  109,  174,  355,
  227,   78,  170,  171,  228,  175,  453,  470,  650,  198,
  683,  390,  614,  651,  356,  357,  358,  359,  360,  562,
  637,  697,  698,  767,  289,  558,  559,  713,  714,  395,
  396,  428,
  };
  protected static readonly short [] yySindex = {          986,
  -40, -154,   29,   39,   69, 3794, 3845, -141,    0,  986,
    0,    0,    0,    0,  -77,  119,  142,  629,  -63,   -5,
    0,    0,    0,    0,    0,    0,    0, 4057,    0,    0,
 3943, -103,  -25,    0,  185, 3627,  -33, 4057,  -31,  221,
    0,    0,  -18,   24,    0,    0,    0,    0,    0, 4057,
 -172,  -96,    6,  -54,  252,  -30,  176,  -27,    0,  185,
   11,  265,   57, 4057,   64,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -26, 4057, 3708,  326, 2383,
   -1,  326,  213,    0,    0,  115, 4057, -172, 4057, -172,
    0,  262,    0, -196,  350,  269, 3905,  326,    0, 4057,
 4057,   12, 4057,  326,    1,    3, 4057, 2377,  -80,    0,
    0,  185,  132,    0,  326,  -80, 2217,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   32,  357,
  360,  364, 4090, 4090, 4090,  365,    0,  115, 4057, 1947,
 4057,  355,  368,  373,  144,    0, -196, 3799,    0,    0,
    0,    0,   -7,  115,    0,    0,  -96,  185,   45,  371,
   26,  -80,  326,  326,    5,    0,    0,    0,  693,  146,
    0,    0,  113, -189,    0,    0, 3829,  113,  113,  113,
  378,  404,    0,    0,    0,    0,    0,    0,    0,    0,
 3970,    0,  405, 4090, 4090, 4090,    0,   20,   44,   10,
   84,  410,  115,   87,  411, 1966, 3882,  184, 1326,    0,
 -196,  181,    4,    0,    0, 3910,    0,  113,    0,    0,
    0,  113, -113,  113,  -96,  326,    0,  783,    0, 3823,
 -111,  187, -104,    0,    0,  113,  113,  203, 1085,    0,
    0, 4057,   80,   95,   97,    0, 4090,    0,    0,  209,
  116,  427,  222,  118,  122,  437,    0,  445,    0,  406,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -107, -189, 4891,  -90, -189,  113,  -96,    0,    0, 4891,
  -88,    0,  219, 4891,    0,    0,    0,  190,    0,   96,
 4057, 4057, 4057,    0,  220,  231,  133,    0,  241,  243,
  134, 1842,    0, 4891,  -83,  -81,  436, 4090, -174, 4090,
    0, 3593, 4057, 3593, 4057, 3593, 4057, 4057, 2006, 4057,
 4057, 4057, 3593, 2064, 2080, 4057, 4057, 4057, 4090, 4090,
 4090, 4057, 2120, 2290,  188,  307, 4090, 4090, 4090, 4090,
 4090, 4090, 4090, 4090, 4090, 4090, 4090, 4090, 1300, 4006,
 4057, 4057, 3627,   98, 2115,    0, 4891,  220,    0,  220,
 4891,  -74, -105,  113, 2250, 4891,    0, 2360,    0, 1085,
 4090,  318,  361,  370,  230,  220,  249,  220,    0,  250,
    0,  235, 2440, 4891, 4891, 5287,    0,  237, 2053,    0,
  467,    0,    0,  115, 3593,    0,  115,  115, 3593,  115,
  115, 3593,  115,  115, 4057,  115,  115,  115,  115,  115,
 3593, 4057,  115, 4057,  115,  115,  115,  115,  470,  472,
  473,  137, 4057,  226, 4090,  474,    0,    0, 4057,  239,
  149,  150,  151,  153,  155,  156,  157,  160,  161,  163,
  166,  182,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4057,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4057,
   37,  115,  649, 4057, 3708, 3627,    0,    0,  220,    0,
  230,  230, 2520, 4891, 4891,  -66, -189,    0, 2617,    0,
    0,  478,    0,    0,    0,  230,  220,  230,  220,    0,
    0, 2697, 2777,  220,    0,  510,  289,  512,  115,    0,
  514,  520,  115,  523,  526,  115,  528,  529,  115,  530,
  531,  532,  533,  535,  115,  115,  538,  115,  539,  540,
  541,  542, 4090, 4090, 4090,  225,  281, 4057,  550, 4057,
  339, 4090, 4057, 4057, 4057, 4057, 4057, 4057, 4057, 4057,
 4057, 4057, 4057, 4057,  115,  115, 2053,  555,    0,  556,
    0,  563,  649,  649, 4057,  649, 4057, 3708,  230,    0,
 2874, 2954, 4891,  -56,    0, 4090,  230,  230,    0,    0,
  230,  289,  504, 2053,  571, 2053, 2053,  572, 2053, 2053,
  574, 2053, 2053,  579, 2053, 2053, 2053, 2053, 2053,  580,
  581, 2053,  582, 2053, 2053, 2053, 2053,    0,  583,  584,
  372, 4057,  115,  588, 4057,  589, 4090,  591,  185,  185,
  185,  185,  185,  185,  185,  185,  185,  185,  185,  185,
  592,  593,  595,  552, 4090, 3772,  113,  563,  563,  649,
  563,  649,  649, 4057,    0,    0, 3034, 4891,    0,  247,
    0,  597, 4057,    0, 2053,    0,    0, 2053,    0,    0,
 2053,    0,    0, 2053,    0,    0,    0,    0,    0, 2053,
 2053,    0, 2053,    0,    0,    0,    0, 4090, 4090,    0,
  611,    0,    0,  294,  614,  299,  618, 4090, 2053, 2053,
 2053,    0,  628, 3938,    0, 1889,  296,    0,  113,  113,
    0,  563,  113,  563,  563,  649,    0, 3131,    0, 4090,
  289,  633, 4028,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  316,  422,  322,  423, 4090,  638,    0,
    0,  594, 4090,  646,  387,    0, 1996,    0, 3971,  113,
    0,  113,  113,    0,  563,    0,    0,    0,  289,    0,
    0,  433,    0,  434,    0,  638,    0,    0, 1880,    0,
    0,  330,  654,  655,  659,    0,    0,    0,    0,  113,
    0,    0,    0,    0,    0,  297,  660, 4090, 4090, 4090,
    0, 4057,  331,  332,  333,  342, 4057, 4057, 4057, 4090,
  374,  377,  395,  661,    0,    0,    0, 4090,  311,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  704,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 1806,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -17,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  670,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  103,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  670,    0,  670,
    0,    0,    0,    0,    0,    0,    0,    0,  613,    0,
    0,    0,    0,  670,    0,    0,    0,   33,  670,    0,
  670,    0,    0,    0,    0,    0,    0,    0,  179,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   60,
 4011, 4071,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  670,    0,    0,  670,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,    0,    0,
    0,    0,    0,    0,    0,  152,  214,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  240,    0,    0,    0,    0,    0,  313,    0,  670,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  670,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3211,    0, 4971,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  670,  670,  670,  477,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  670,    0,    0,  670,  670,    0,  670,
  670,    0,  670,  670,    0,  670,  670,  670,  670,  670,
    0,    0,  670,    0,  670,  670,  670,  670,    0,    0,
    0,  670,    0,  670,    0,    0,    0,    0,    0,  670,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  670,  670,    0,    0,    0,    0,    0,    0, 3291,    0,
 3388, 5051,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  551,  560,  569,    0,    0,
    0,    0,    0, 5131,    0,    0,    0,    0,  670,    0,
    0,    0,  670,    0,    0,  670,    0,    0,  670,    0,
    0,    0,    0,    0,  670,  670,    0,  670,    0,    0,
    0,    0,    0,    0,    0,    0,  670,    0,    0,    0,
  670,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  670,  670,    0, 4091,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3468,    0,
    0,    0,    0,    0,    0,    0,  578, 1764,    0,    0,
 5211,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  670,    0,    0,    0,    0,    0,  702,  782,
  862,  951, 1040, 1120, 1200, 1289, 1378, 1458, 1538, 1627,
    0,    0,    0,    0,    0,    0, 4171,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  670,    0,    0, 4251,    0,
    0,    0, 4331,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4411,    0,
    0,    0,    0,  320,  670,    0,    0,    0,    0, 4491,
    0, 4571, 4651,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4731,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 4811,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  670,    0,    0,    0,    0,
  670,  670,  670,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  705,  658,    0,    0,    0,    0,  566,  575,  109,
   -6,  204, 2063,   73,    0,  698,  508, -178,  517, -300,
    0,  -82,  534,  657,   -2,    0,  549,  139,  -69, -234,
  -19, -345,    0,  497,   35,  -59,    0,    0, -650,  308,
    0, -496, -516,   18, -209,    0,  379,  380,  348, -501,
 -270,    0,    2,    0,  369,    0,  110,    0,   30, -225,
  -20,    0,
  };
  protected static readonly short [] yyTable = {            35,
   35,  382,   96,   37,   39,   92,   80,  475,   80,  273,
  583,  280,   80,   80,  156,  304,   77,  485,  284,   64,
   15,   35,   72,  616,   60,   58,   70,   57,  261,   35,
  264,   35,  361,   76,  366,   81,  216,  729,   80,  384,
   80,  385,   80,   86,   80,  365,  180,  216,  484,  368,
  197,  197,  197,  247,  100,  100,  573,   60,  107,  102,
  287,  638,  639,  247,  641,  156,  648,   66,   31,  383,
   35,   35,   72,  112,  105,  106,   71,  756,   66,  142,
  138,  303,  140,  143,  232,  652,   66,  247,  399,   18,
  402,  388,  223,  158,  159,  681,  161,  411,  685,   19,
   35,  169,  249,  231,  165,  248,  219,   70,  236,  237,
  389,  243,  244,  245,  234,   16,   17,  215,  221,  172,
  234,   47,   48,  287,   88,   90,  483,  557,  267,   20,
  568,  489,  203,  270,  206,   99,  160,   66,  702,  371,
  704,  705,   72,   88,  246,  478,   88,  799,  271,  502,
  503,  103,  272,   63,  275,  478,   66,   71,  478,   87,
  232,   89,  232,  234,  294,  144,  232,   40,  232,  283,
  112,  234,  176,  478,  134,  177,  234,  234,   66,   43,
  536,  173,   42,  283,  210,  283,  229,  211,  179,  230,
  283,  287,  283,   45,   46,  172,  139,  172,  141,  283,
  260,  172,   44,  172,  745,  135,  363,  283,  278,   45,
   46,  234,  234,  104,  748,  234,   54,  283,   72,  109,
  116,  266,  109,  169,  211,  387,   66,  391,  172,  218,
  369,   65,   91,  370,  222,  290,  157,  133,   79,   21,
   82,   84,  162,   95,   98,  104,  419,  420,  421,  571,
  572,  426,  772,  178,  431,  432,  433,  434,  435,  436,
  437,  438,  439,  440,  441,  442,  214,   66,   55,  538,
  115,   56,  163,  478,  164,  500,  226,  214,  370,  478,
   66,   83,  542,   85,  372,  373,  374,  709,  492,  136,
  710,   94,  478,  478,  487,   45,   46,  276,   97,   47,
   48,  224,  225,  234,   80,  394,  397,  398,  400,  401,
  403,  404,  406,  407,  408,  409,  410,  413,  415,  416,
  417,  418,   66,  101,  612,  422,  424,  102,  102,  430,
  103,  102,  102,  474,  102,  117,  738,  781,  647,  739,
  370,  202,  539,  205,  471,  472,   35,  102,  102,  364,
  473,  800,  110,   35,  710,  110,   35,  217,  493,   66,
  205,  478,  478,  205,  277,  108,   32,  700,  701,  102,
  703,  118,  119,  120,  510,  121,  122,  123,  510,  124,
   66,  510,  617,   66,   64,  790,  125,  126,  509,  147,
  510,  148,  513,  127,  193,  516,  194,   33,  519,  195,
  128,  494,   66,  196,  525,  526,  252,  528,  201,  256,
  495,   66,  207,  708,  795,   66,  537,  796,   66,  103,
  103,  172,  541,  103,  103,  208,  103,  234,   66,   31,
  209,  741,  220,  743,  744,  797,   66,  478,  238,  103,
  103,  199,  200,  239,  242,  250,  555,   66,  253,  291,
  608,  609,  610,  251,  254,  565,  567,  263,  776,  618,
  282,  103,  285,  556,  292,  295,  293,   35,   35,   35,
  297,  563,  564,  566,  771,  129,   18,  296,  298,  299,
  301,  104,  104,  300,  302,  104,  104,  376,  104,  130,
  131,  132,  367,  232,  377,  380,  386,  378,  478,  379,
  476,  104,  104,  283,  427,  497,  499,   21,   21,  505,
  507,   21,   21,  533,   21,  534,  535,  540,  543,  544,
  545,  576,  546,  104,  547,  548,  549,   21,   21,  550,
  551,  613,  552,  613,  687,  553,  619,  620,  621,  622,
  623,  624,  625,  626,  627,  628,  629,  630,  644,   21,
   19,  554,  693,  582,  388,  584,  766,  586,   35,   20,
   35,   35,  640,  587,  642,  643,  589,  699,   25,  590,
   21,  592,  593,  595,  596,  597,  598,   22,  599,   22,
  287,  602,  604,  605,  606,  607,  611,   23,   24,   25,
   26,   27,  506,  615,  653,  722,  723,  508,  634,  635,
  511,  512,  636,  514,  515,  613,  517,  518,  613,  520,
  521,  522,  523,  524,  655,  658,  527,  661,  529,  530,
  531,  532,  664,  670,  671,  673,  678,  679,  680,  696,
  740,  684,  686,  742,  688,  689,  690,   35,  691,  234,
  711,  706,  557,  118,  119,  120,  260,  121,  122,  123,
  758,  124,   73,  176,  724,  725,  176,  726,  760,  761,
  727,  728,  118,  119,  120,  127,  121,  122,  123,  429,
  124,  733,  128,  770,  176,  560,  749,  752,  753,  755,
  234,  710,  234,  754,  127,  759,  757,  735,   80,  773,
  774,  128,  777,  778,  779,  783,  784,  785,  780,  782,
  787,  788,  789,    1,  798,  176,  260,  794,  134,   72,
  234,   93,  585,  213,   41,   53,  588,  278,  265,  591,
  102,  212,  594,  262,  241,  235,  279,  747,  600,  601,
  737,  603,  696,  504,   66,  479,  480,  176,  491,  135,
  769,   72,  751,  692,   18,   18,    0,  762,   18,   18,
    0,   18,    0,    0,    0,    0,    0,    0,  631,  632,
  633,  763,  764,  765,   18,   18,    0,    0,    0,    0,
    0,  133,    0,    0,    0,  786,    0,    0,    0,    0,
  791,  792,  793,    0,    0,    0,   18,  654,    0,  656,
  657,    0,  659,  660,    0,  662,  663,    0,  665,  666,
  667,  668,  669,    0,    0,  672,    0,  674,  675,  676,
  677,    0,    0,    0,    0,    0,  682,    0,   19,   19,
    0,   72,   19,   19,    0,   19,    0,   20,   20,    0,
    0,   20,   20,    0,   20,    0,   25,   25,   19,   19,
   25,   25,    0,   25,    0,   22,   22,   20,   20,   22,
   22,    0,   22,    0,    0,    0,   25,   25,  715,    0,
   19,  716,    0,    0,  717,   22,   22,  718,    0,   20,
    0,    0,    0,  719,  720,    0,  721,    0,   25,    0,
    0,    0,    0,    0,    0,  176,  176,   22,    0,    0,
    0,    0,  730,  731,  732,    0,    0,    0,    0,  736,
    0,   72,    0,    0,    0,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    0,    0,    0,    0,   45,   46,
  125,  126,   47,   48,   49,   29,   30,  127,    0,    0,
  176,  176,  176,    0,  128,    0,    0,    0,    0,  176,
  768,  176,    0,    0,  176,  176,  176,  176,  176,  176,
  176,  176,  176,  176,    0,  176,  176,    0,  176,  176,
  176,  176,  176,  176,  176,    0,    0,  176,  176,  176,
  176,    0,    0,  176,  291,  291,    0,  176,  176,  176,
  176,  176,  176,  176,  176,  176,  176,  176,  176,  176,
   72,  176,   68,   69,    0,   70,   71,   72,   73,   74,
   75,    0,  176,    0,    0,    0,    0,    0,    0,  129,
    0,    0,    0,  176,  176,  176,  176,    0,    0,  291,
  291,  291,    0,  130,  131,  132,    0,    0,  291,    0,
  291,    0,    0,  291,  291,  291,  291,  291,  291,  291,
  291,  291,  291,    0,  291,  291,    0,  291,  291,  291,
  291,  291,  291,  291,  296,  296,  291,  291,  291,  291,
    0,    0,  291,    0,    0,    0,  291,  291,  291,  291,
  291,    0,  291,  291,  291,  291,  291,  291,  291,   72,
  291,    0,   68,   69,    0,   70,   71,   72,   73,   74,
   75,  291,    0,    0,    0,    0,    0,    0,    0,  296,
  296,  296,  291,  291,  291,  291,    0,    0,  296,    0,
  296,    0,    0,  296,  296,  296,  296,  296,  296,  296,
  296,  296,  296,    0,  296,  296,    0,  296,  296,  296,
  296,  296,  296,  296,  281,  281,  296,  296,  296,  296,
    0,    0,  296,    0,    0,    0,  296,  296,  296,  296,
  296,    0,  296,  296,  296,  296,  296,  296,  296,   72,
  296,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  296,    0,    0,    0,    0,    0,    0,    0,  281,
  281,  281,  296,  296,  296,  296,    0,    0,  281,    0,
  281,    0,    0,  281,  281,  281,  281,  281,  281,  281,
  281,  281,  281,    0,  281,  281,    0,  281,  281,  281,
  281,  281,  281,  281,    0,    0,  281,  281,  281,  281,
    0,    0,  281,  262,  262,    0,  281,  281,  281,  281,
  281,    0,  281,  281,  281,  281,  281,  281,  281,   72,
  281,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  281,    0,    1,    2,    0,    0,    3,    4,    0,
    5,    0,  281,  281,  281,  281,    0,    0,  262,  262,
  262,    0,    0,    6,    7,    0,    0,  262,    0,  262,
    0,    0,  262,  262,  262,  262,  262,  262,  262,  262,
  262,  262,    0,  262,  262,    8,  262,  262,  262,  262,
  262,  262,  262,    0,    0,  262,  262,  262,  262,    0,
    0,  262,  259,  259,    0,  262,  262,  262,  262,  262,
    0,  262,  262,  262,  262,  262,  262,  262,   72,  262,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  262,  118,  119,  120,    0,  121,  122,  123,    0,  124,
    0,  262,  262,  262,  262,    0,    0,  259,  259,  259,
  286,    0,    0,  127,    0,    0,  259,    0,  259,    0,
  128,  259,  259,  259,  259,  259,  259,  259,  259,  259,
  259,    0,  259,  259,    0,  259,  259,  259,  259,  259,
  259,  259,  260,  260,  259,  259,  259,  259,    0,    0,
  259,    0,    0,    0,  259,  259,  259,  259,  259,    0,
  259,  259,  259,  259,  259,  259,  259,   72,  259,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  259,
    0,    0,    0,    0,    0,    0,    0,  260,  260,  260,
  259,  259,  259,  259,    0,    0,  260,    0,  260,    0,
    0,  260,  260,  260,  260,  260,  260,  260,  260,  260,
  260,    0,  260,  260,    0,  260,  260,  260,  260,  260,
  260,  260,  261,  261,  260,  260,  260,  260,    0,    0,
  260,    0,    0,    0,  260,  260,  260,  260,  260,    0,
  260,  260,  260,  260,  260,  260,  260,   72,  260,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  260,
    0,    0,    0,    0,    0,    0,    0,  261,  261,  261,
  260,  260,  260,  260,    0,    0,  261,    0,  261,    0,
    0,  261,  261,  261,  261,  261,  261,  261,  261,  261,
  261,    0,  261,  261,    0,  261,  261,  261,  261,  261,
  261,  261,    0,    0,  261,  261,  261,  261,    0,    0,
  261,  293,  293,    0,  261,  261,  261,  261,  261,    0,
  261,  261,  261,  261,  261,  261,  261,   72,  261,    0,
    0,    0,  118,  119,  120,    0,  121,  122,  123,  261,
  124,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  261,  261,  261,  261,  127,    0,  293,  293,  293,    0,
    0,  128,    0,    0,    0,  293,    0,  293,    0,    0,
  293,  293,  293,  293,  293,  293,  293,  293,  293,  293,
    0,  293,  293,    0,  293,  293,  293,  293,  293,  293,
  293,    0,    0,  293,  293,  293,  293,    0,    0,  293,
  285,  285,    0,  293,  293,  293,  293,  293,    0,  293,
  293,  293,  293,  293,  293,  293,   72,  293,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  293,  443,
  444,  445,  446,  447,  448,  449,  450,  451,  452,  293,
  293,  293,  293,    0,    0,  285,  285,  285,    0,    0,
    0,    0,    0,    0,  285,    0,  285,    0,    0,  285,
  285,  285,  285,  285,  285,  285,  285,  285,  285,    0,
  285,  285,    0,  285,  285,  285,  285,  285,  285,  285,
  277,  277,  285,  285,  285,  285,    0,    0,  285,    0,
    0,    0,  285,  285,  285,  285,  285,    0,  285,  285,
  285,  285,  285,  285,  285,    0,  285,    0,    0,    0,
    0,    0,    0,   24,    0,    0,    0,  285,    0,    0,
    0,    0,    0,    0,    0,  277,  277,  277,  285,  285,
  285,  285,    0,    0,  277,    0,  277,    0,    0,  277,
  277,  277,  277,  277,  277,  277,  277,  277,  277,    0,
  277,  277,    0,  277,  277,  277,  277,  277,  277,  277,
  268,  268,  277,  277,  277,  277,    0,    0,  277,    0,
    0,    0,  277,  277,  277,  277,  277,    0,  277,  277,
  277,  277,  277,  277,  277,    0,  277,    0,    0,    0,
    0,    0,    0,    0,    0,   72,    0,  277,    0,    0,
    0,    0,    0,    0,    0,  268,  268,  268,  277,  277,
  277,  277,    0,    0,  268,   72,  268,    0,    0,  268,
  268,  268,  268,  268,  268,  268,  268,  268,  268,    0,
  268,  268,  381,  268,  268,  268,  268,  268,  268,  268,
    0,    0,  268,  268,  268,  268,   72,    0,  268,  241,
  241,    0,  268,  268,  268,  268,  268,    0,  268,  268,
  268,  268,  268,  268,  268,    0,  268,    0,    0,    0,
  775,    0,    0,    0,    0,    0,    0,  268,   72,    0,
   66,    0,    0,    0,    0,    0,    0,    0,  268,  268,
  268,  268,    0,    0,  241,  241,  241,    0,  134,    0,
    0,    0,    0,  241,    0,  241,    0,    0,  241,  241,
  241,  241,  241,  241,  241,  241,  241,  241,    0,  241,
  241,    0,  241,  241,  241,  241,  241,  241,  241,  135,
    0,  241,  241,  241,  241,    0,    0,  241,   66,    0,
  204,  241,  241,  241,  241,  241,    0,  241,  241,  241,
  241,  241,  241,  241,    0,  241,  134,   66,    0,  255,
    0,  133,    0,    0,    0,    0,  241,    0,    0,    0,
    0,    0,    0,    0,    0,  134,    0,  241,  241,  241,
  241,   24,   24,    0,    0,   24,   24,  135,   24,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   24,   24,    0,    0,  134,  135,    0,    0,    0,
    0,    0,   72,   72,   72,   32,   72,   72,   72,  133,
   72,    0,    0,   24,    0,    0,    0,   72,   72,    0,
    0,    0,    0,    0,   72,    0,  135,    0,  133,    0,
    0,   72,    0,    0,    0,    0,   33,    0,  118,  119,
  120,    0,  121,  122,  123,    0,  124,    0,    0,    0,
    0,    0,  134,    0,    0,    0,    0,  286,  133,    0,
  127,    0,    0,   32,    0,    0,    0,  128,   31,    0,
    0,    0,    0,    0,    0,    0,  118,  119,  120,   32,
  121,  122,  123,  135,  124,  118,  119,  120,    0,  121,
  122,  123,    0,  124,   33,  286,    0,    0,  127,    0,
  125,  126,    0,    0,    0,  128,   72,  127,    0,    0,
   33,    0,    0,    0,  128,  133,    0,    0,    0,   32,
   72,   72,   72,    0,    0,    0,   31,    0,   68,   69,
    0,   70,   71,   72,   73,   74,   75,    0,    0,    0,
    0,    0,   31,  118,  119,  120,    0,  121,  122,  123,
   33,  124,    0,    0,    0,    0,    0,    0,  125,  126,
    0,    0,  118,  119,  120,  127,  121,  122,  123,    0,
  124,    0,  128,    0,    0,    0,    0,  125,  126,  477,
    0,    0,   31,    0,  127,    0,    0,    0,    0,  129,
    0,  128,  118,  119,  120,    0,  121,  122,  123,    0,
  124,    0,    0,  130,  131,  132,    0,  125,  126,   21,
    0,    0,    0,    0,  127,    0,    0,    0,   22,    0,
    0,  128,    0,    0,    0,  274,   23,   24,   25,   26,
   27,    0,    0,  281,    0,   68,   69,    0,   70,   71,
   72,   73,   74,   75,    0,    0,    0,  129,    0,  118,
  119,  120,    0,  121,  122,  123,    0,  124,    0,    0,
    0,  130,  131,  132,  125,  126,  129,   21,    0,    0,
    0,  127,    0,  305,  306,    0,   22,  362,  128,    0,
  130,  131,  132,   21,   23,   24,   25,   26,   27,   32,
  405,    0,   22,    0,    0,    0,  129,  375,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,    0,    0,
  130,  131,  132,    0,  488,    0,    0,    0,    0,    0,
   33,    0,    0,   21,    0,    0,    0,  307,    0,    0,
    0,    0,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,  412,    0,
    0,    0,   31,  129,    0,    0,    0,  168,    0,    0,
  481,  423,  482,  111,  414,  486,    0,  130,  131,  132,
    0,    0,  308,  309,  310,    0,   32,    0,  496,    0,
  498,  311,   32,  312,    0,    0,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,    0,  323,  324,    0,
  325,  326,  327,  328,  329,  330,  331,   33,    0,  332,
  333,  334,  335,   33,    0,  336,  181,    0,    0,  337,
  338,  339,  340,  341,  490,  342,  343,  344,  345,  346,
  347,  348,  182,  349,    0,    0,    0,    0,    0,   31,
    0,    0,    0,    0,  350,   31,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  351,  352,  353,  354,    0,
    0,    0,  307,  183,  184,    0,    0,  185,  186,  187,
  188,  189,  190,    0,    0,    0,    0,    0,    0,    0,
    0,  569,    0,    0,    0,    0,    0,    0,    0,  574,
    0,    0,    0,  149,    0,    0,    0,    0,    0,  577,
    0,  578,   22,    0,  501,    0,  581,  308,  309,  310,
   23,   24,   25,   26,   27,    0,  311,    0,  312,    0,
    0,  313,  314,  315,  316,  317,  318,  319,  320,  321,
  322,  425,  323,  324,    0,  325,  326,  327,  328,  329,
  330,  331,    0,    0,  332,  333,  334,  335,    0,    0,
  336,    0,    0,    0,  337,  338,  339,  340,  341,    0,
  342,  343,  344,  345,  346,  347,  348,    0,  349,    0,
    0,    0,  307,    0,    0,    0,    0,    0,    0,  350,
   21,    0,    0,    0,  570,    0,   21,    0,    0,   22,
  351,  352,  353,  354,  166,   22,    0,   23,   24,   25,
   26,   27,    0,   23,   24,   25,   26,   27,    0,  167,
    0,    0,    0,    0,    0,  110,    0,  308,  309,  310,
    0,    0,    0,    0,    0,    0,  311,    0,  312,    0,
    0,  313,  314,  315,  316,  317,  318,  319,  320,  321,
  322,    0,  323,  324,    0,  325,  326,  327,  328,  329,
  330,  331,  307,    0,  332,  333,  334,  335,    0,    0,
  336,    0,    0,    0,  337,  338,  339,  340,  341,    0,
  342,  343,  344,  345,  346,  347,  348,    0,  349,    0,
    0,  575,    0,    0,    0,    0,    0,    0,    0,  350,
    0,    0,    0,    0,    0,    0,    0,  308,  309,  310,
  351,  352,  353,  354,    0,    0,  311,    0,  312,    0,
    0,  313,  314,  315,  316,  317,  318,  319,  320,  321,
  322,    0,  323,  324,    0,  325,  326,  327,  328,  329,
  330,  331,  307,    0,  332,  333,  334,  335,    0,    0,
  336,    0,    0,    0,  337,  338,  339,  340,  341,    0,
  342,  343,  344,  345,  346,  347,  348,    0,  349,    0,
    0,  579,    0,    0,    0,    0,    0,    0,    0,  350,
    0,    0,    0,    0,    0,    0,    0,  308,  309,  310,
  351,  352,  353,  354,    0,    0,  311,    0,  312,    0,
    0,  313,  314,  315,  316,  317,  318,  319,  320,  321,
  322,    0,  323,  324,    0,  325,  326,  327,  328,  329,
  330,  331,    0,    0,  332,  333,  334,  335,    0,    0,
  336,    0,    0,    0,  337,  338,  339,  340,  341,  307,
  342,  343,  344,  345,  346,  347,  348,    0,  349,    0,
    0,  580,    0,    0,    0,    0,    0,    0,    0,  350,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  351,  352,  353,  354,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  308,  309,  310,    0,    0,    0,
    0,    0,    0,  311,    0,  312,    0,    0,  313,  314,
  315,  316,  317,  318,  319,  320,  321,  322,    0,  323,
  324,    0,  325,  326,  327,  328,  329,  330,  331,  307,
    0,  332,  333,  334,  335,    0,    0,  336,    0,    0,
    0,  337,  338,  339,  340,  341,    0,  342,  343,  344,
  345,  346,  347,  348,    0,  349,    0,    0,  645,    0,
    0,    0,    0,    0,    0,    0,  350,    0,    0,    0,
    0,    0,    0,    0,  308,  309,  310,  351,  352,  353,
  354,    0,    0,  311,    0,  312,    0,    0,  313,  314,
  315,  316,  317,  318,  319,  320,  321,  322,    0,  323,
  324,    0,  325,  326,  327,  328,  329,  330,  331,  307,
    0,  332,  333,  334,  335,    0,    0,  336,    0,    0,
    0,  337,  338,  339,  340,  341,    0,  342,  343,  344,
  345,  346,  347,  348,    0,  349,    0,    0,  646,    0,
    0,    0,    0,    0,    0,    0,  350,    0,    0,    0,
    0,    0,    0,    0,  308,  309,  310,  351,  352,  353,
  354,    0,    0,  311,    0,  312,    0,    0,  313,  314,
  315,  316,  317,  318,  319,  320,  321,  322,    0,  323,
  324,    0,  325,  326,  327,  328,  329,  330,  331,    0,
    0,  332,  333,  334,  335,    0,    0,  336,    0,    0,
    0,  337,  338,  339,  340,  341,  307,  342,  343,  344,
  345,  346,  347,  348,    0,  349,    0,    0,  707,    0,
    0,    0,    0,    0,    0,    0,  350,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  351,  352,  353,
  354,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  308,  309,  310,    0,    0,    0,    0,    0,    0,
  311,    0,  312,    0,    0,  313,  314,  315,  316,  317,
  318,  319,  320,  321,  322,    0,  323,  324,    0,  325,
  326,  327,  328,  329,  330,  331,  307,    0,  332,  333,
  334,  335,    0,    0,  336,    0,    0,    0,  337,  338,
  339,  340,  341,    0,  342,  343,  344,  345,  346,  347,
  348,    0,  349,    0,    0,  746,    0,    0,    0,    0,
    0,    0,    0,  350,    0,    0,    0,    0,    0,    0,
    0,  308,  309,  310,  351,  352,  353,  354,    0,    0,
  311,    0,  312,    0,    0,  313,  314,  315,  316,  317,
  318,  319,  320,  321,  322,    0,  323,  324,    0,  325,
  326,  327,  328,  329,  330,  331,  307,    0,  332,  333,
  334,  335,    0,    0,  336,    0,    0,    0,  337,  338,
  339,  340,  341,    0,  342,  343,  344,  345,  346,  347,
  348,    0,  349,    0,    0,  188,    0,    0,    0,    0,
    0,    0,    0,  350,    0,    0,    0,    0,    0,    0,
    0,  308,  309,  310,  351,  352,  353,  354,    0,    0,
  311,    0,  312,    0,    0,  313,  314,  315,  316,  317,
  318,  319,  320,  321,  322,    0,  323,  324,    0,  325,
  326,  327,  328,  329,  330,  331,    0,    0,  332,  333,
  334,  335,    0,    0,  336,    0,    0,    0,  337,  338,
  339,  340,  341,  307,  342,  343,  344,  345,  346,  347,
  348,    0,  349,    0,    0,  186,    0,    0,    0,    0,
    0,    0,    0,  350,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  351,  352,  353,  354,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  308,  309,
  310,    0,    0,    0,    0,    0,    0,  311,    0,  312,
    0,    0,  313,  314,  315,  316,  317,  318,  319,  320,
  321,  322,    0,  323,  324,    0,  325,  326,  327,  328,
  329,  330,  331,  188,    0,  332,  333,  334,  335,    0,
    0,  336,    0,    0,    0,  337,  338,  339,  340,  341,
    0,  342,  343,  344,  345,  346,  347,  348,    0,  349,
    0,    0,  189,    0,    0,    0,    0,    0,    0,    0,
  350,    0,    0,    0,    0,    0,    0,    0,  188,  188,
  188,  351,  352,  353,  354,    0,    0,  188,    0,  188,
    0,    0,  188,  188,  188,  188,  188,  188,  188,  188,
  188,  188,    0,  188,  188,    0,  188,  188,  188,  188,
  188,  188,  188,  186,    0,  188,  188,  188,  188,    0,
    0,  188,    0,    0,    0,  188,  188,  188,  188,  188,
    0,  188,  188,  188,  188,  188,  188,  188,    0,  188,
    0,    0,  187,    0,    0,    0,    0,    0,    0,    0,
  188,    0,    0,    0,    0,    0,    0,    0,  186,  186,
  186,  188,  188,  188,  188,    0,    0,  186,    0,  186,
    0,    0,  186,  186,  186,  186,  186,  186,  186,  186,
  186,  186,    0,  186,  186,    0,  186,  186,  186,  186,
  186,  186,  186,    0,    0,  186,  186,  186,  186,    0,
    0,  186,   32,    0,    0,  186,  186,  186,  186,  186,
  189,  186,  186,  186,  186,  186,  186,  186,    0,  186,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  186,    0,    0,   33,    0,    0,   32,    0,    0,    0,
    0,  186,  186,  186,  186,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  189,  189,  189,    0,    0,
    0,    0,    0,    0,  189,   31,  189,   33,    0,  189,
  189,  189,  189,  189,  189,  189,  189,  189,  189,    0,
  189,  189,    0,  189,  189,  189,  189,  189,  189,  189,
  187,    0,  189,  189,  189,  189,    0,    0,  189,   31,
    0,    0,  189,  189,  189,  189,  189,    0,  189,  189,
  189,  189,  189,  189,  189,    0,  189,   32,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  189,    0,    0,
    0,    0,    0,    0,    0,  187,  187,  187,  189,  189,
  189,  189,    0,    0,  187,    0,  187,    0,   33,  187,
  187,  187,  187,  187,  187,  187,  187,  187,  187,    0,
  187,  187,  695,  187,  187,  187,  187,  187,  187,  187,
    0,    0,  187,  187,  187,  187,    0,    0,  187,    0,
   31,   32,  187,  187,  187,  187,  187,    0,  187,  187,
  187,  187,  187,  187,  187,    0,  187,    0,    0,    0,
    0,    0,    0,   32,    0,    0,   21,  187,   32,    0,
    0,    0,   33,    0,    0,   22,    0,    0,  187,  187,
  187,  187,    0,   23,   24,   25,   26,   27,    0,    0,
    0,    0,   32,    0,   33,    0,    0,    0,   32,   33,
   21,    0,    0,    0,   31,    0,    0,    0,    0,   22,
    0,    0,    0,    0,   32,    0,    0,   23,   24,   25,
   26,   27,    0,   33,    0,    0,   31,    0,    0,   33,
    0,   31,  392,  393,    0,   67,   68,   69,    0,   70,
   71,   72,   73,   74,   75,   33,    0,    0,    0,    0,
    0,   32,    0,    0,    0,   31,    0,    0,    0,    0,
    0,   31,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   32,    0,    0,   31,    0,   32,
    0,   21,   33,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,    0,    0,   33,    0,   32,    0,    0,
   33,    0,   32,    0,   31,    0,    0,   68,   69,    0,
   70,   71,   72,   73,   74,   75,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   31,   33,  152,
   32,    0,   31,   33,    0,   21,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,  694,
    0,    0,   23,   24,   25,   26,   27,   21,    0,    0,
   31,   33,  149,  150,    0,   31,   22,   59,    0,    0,
    0,   22,  151,    0,   23,   24,   25,   26,   27,   23,
   24,   25,   26,   27,    0,   28,   21,   32,    0,    0,
   29,   30,   21,   31,  240,   22,    0,    0,    0,    0,
  166,   22,    0,   23,   24,   25,   26,   27,   21,   23,
   24,   25,   26,   27,    0,  167,   32,   22,   33,    0,
  750,  110,    0,    0,    0,   23,   24,   25,   26,   27,
    0,    0,    0,    0,    0,   63,   38,    0,  118,  119,
  120,  257,  121,  122,  123,   21,  124,   33,    0,   32,
   31,    0,    0,    0,   22,  258,    0,  259,    0,    0,
  127,    0,   23,   24,   25,   26,   27,  128,  149,  150,
    0,    0,    0,  149,  268,    0,    0,   22,  151,   31,
   33,    0,   22,  269,    0,   23,   24,   25,   26,   27,
   23,   24,   25,   26,   27,   64,    0,    0,    0,    0,
    0,   21,    0,    0,    0,    0,   21,    0,    0,    0,
   22,  734,   31,    0,    0,   22,    0,    0,   23,   24,
   25,   26,   27,   23,   24,   25,   26,   27,    0,  181,
    0,    0,    0,    0,   21,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,  182,    0,    0,  694,    0,
    0,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  454,  455,    0,    0,
   63,    0,    0,    0,    0,    0,  183,  184,    0,    0,
  185,  186,  187,  188,  189,  190,   63,    0,    0,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,    0,    0,    0,    0,   63,   63,    0,
   21,   63,   63,   63,   63,   63,   63,    0,    0,   22,
   64,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,    0,    0,    0,    0,   64,    0,    0,    0,
    0,    0,    0,  149,    0,    0,    0,    0,    0,    0,
    0,    0,   22,  276,  276,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,    0,    0,   64,   64,    0,
    0,   64,   64,   64,   64,   64,   64,  456,  457,  458,
  459,    0,    0,    0,    0,    0,  460,  461,  462,  463,
  464,  465,  466,  467,  468,  469,    0,    0,  276,  276,
  276,    0,    0,    0,    0,    0,    0,  276,    0,  276,
    0,    0,  276,  276,  276,  276,  276,  276,  276,  276,
  276,  276,    0,  276,  276,    0,  276,  276,  276,  276,
  276,  276,  276,  242,  242,  276,  276,  276,  276,    0,
    0,  276,    0,    0,    0,  276,  276,  276,  276,  276,
    0,  276,  276,  276,  276,  276,  276,  276,    0,  276,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  276,    0,    0,    0,    0,    0,    0,    0,  242,  242,
  242,  276,  276,  276,  276,    0,    0,  242,    0,  242,
    0,    0,  242,  242,  242,  242,  242,  242,  242,  242,
  242,  242,    0,  242,  242,    0,  242,  242,  242,  242,
  242,  242,  242,  245,  245,  242,  242,  242,  242,    0,
    0,  242,    0,    0,    0,  242,  242,  242,  242,  242,
    0,  242,  242,  242,  242,  242,  242,  242,    0,  242,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  242,    0,    0,    0,    0,    0,    0,    0,  245,  245,
  245,  242,  242,  242,  242,    0,    0,  245,    0,  245,
    0,    0,  245,  245,  245,  245,  245,  245,  245,  245,
  245,  245,    0,  245,  245,    0,  245,  245,  245,  245,
  245,  245,  245,  250,  250,  245,  245,  245,  245,    0,
    0,  245,    0,    0,    0,  245,  245,  245,  245,  245,
    0,  245,  245,  245,  245,  245,  245,  245,    0,  245,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  245,    0,    0,    0,    0,    0,    0,    0,  250,  250,
  250,  245,  245,  245,  245,    0,    0,  250,    0,  250,
    0,    0,  250,  250,  250,  250,  250,  250,  250,  250,
  250,  250,    0,  250,  250,    0,  250,  250,  250,  250,
  250,  250,  250,  264,  264,  250,  250,  250,  250,    0,
    0,  250,    0,    0,    0,  250,  250,  250,  250,  250,
    0,  250,  250,  250,  250,  250,  250,  250,    0,  250,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  250,    0,    0,    0,    0,    0,    0,    0,  264,  264,
  264,  250,  250,  250,  250,    0,    0,  264,    0,  264,
    0,    0,  264,  264,  264,  264,  264,  264,  264,  264,
  264,  264,    0,  264,  264,    0,  264,  264,  264,  264,
  264,  264,  264,  246,  246,  264,  264,  264,  264,    0,
    0,  264,    0,    0,    0,  264,  264,  264,  264,  264,
    0,  264,  264,  264,  264,  264,  264,  264,    0,  264,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  264,    0,    0,    0,    0,    0,    0,    0,  246,  246,
  246,  264,  264,  264,  264,    0,    0,  246,    0,  246,
    0,    0,  246,  246,  246,  246,  246,  246,  246,  246,
  246,  246,    0,  246,  246,    0,  246,  246,  246,  246,
  246,  246,  246,  247,  247,  246,  246,  246,  246,    0,
    0,  246,    0,    0,    0,  246,  246,  246,  246,  246,
    0,  246,  246,  246,  246,  246,  246,  246,    0,  246,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  246,    0,    0,    0,    0,    0,    0,    0,  247,  247,
  247,  246,  246,  246,  246,    0,    0,  247,    0,  247,
    0,    0,  247,  247,  247,  247,  247,  247,  247,  247,
  247,  247,    0,  247,  247,    0,  247,  247,  247,  247,
  247,  247,  247,  248,  248,  247,  247,  247,  247,    0,
    0,  247,    0,    0,    0,  247,  247,  247,  247,  247,
    0,  247,  247,  247,  247,  247,  247,  247,    0,  247,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  247,    0,    0,    0,    0,    0,    0,    0,  248,  248,
  248,  247,  247,  247,  247,    0,    0,  248,    0,  248,
    0,    0,  248,  248,  248,  248,  248,  248,  248,  248,
  248,  248,    0,  248,  248,    0,  248,  248,  248,  248,
  248,  248,  248,  265,  265,  248,  248,  248,  248,    0,
    0,  248,    0,    0,    0,  248,  248,  248,  248,  248,
    0,  248,  248,  248,  248,  248,  248,  248,    0,  248,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  248,    0,    0,    0,    0,    0,    0,    0,  265,  265,
  265,  248,  248,  248,  248,    0,    0,  265,    0,  265,
    0,    0,  265,  265,  265,  265,  265,  265,  265,  265,
  265,  265,    0,  265,  265,    0,  265,  265,  265,  265,
  265,  265,  265,  249,  249,  265,  265,  265,  265,    0,
    0,  265,    0,    0,    0,  265,  265,  265,  265,  265,
    0,  265,  265,  265,  265,  265,  265,  265,    0,  265,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  265,    0,    0,    0,    0,    0,    0,    0,  249,  249,
  249,  265,  265,  265,  265,    0,    0,  249,    0,  249,
    0,    0,  249,  249,  249,  249,  249,  249,  249,  249,
  249,  249,    0,  249,  249,    0,  249,  249,  249,  249,
  249,  249,  249,  307,    0,  249,  249,  249,  249,    0,
    0,  249,    0,    0,    0,  249,  249,  249,  249,  249,
    0,  249,  249,  249,  249,  249,  249,  249,    0,  249,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  249,    0,    0,    0,    0,    0,    0,    0,  308,  309,
  310,  249,  249,  249,  249,    0,    0,  311,    0,  312,
    0,    0,  313,  314,  315,  316,  317,  318,  319,  320,
  321,  322,    0,  323,  324,    0,  325,  326,  327,  328,
  329,  330,  331,  192,    0,  332,  333,  334,  335,    0,
    0,  336,    0,    0,    0,  337,  338,  339,  340,  341,
    0,  342,  343,  344,  345,  346,  347,  348,    0,  349,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  350,    0,    0,    0,    0,    0,    0,    0,  192,  192,
  192,  351,  352,  353,  354,    0,    0,  192,    0,  192,
    0,    0,  192,  192,  192,  192,  192,  192,  192,  192,
  192,  192,    0,  192,  192,    0,  192,  192,  192,  192,
  192,  192,  192,  193,    0,  192,  192,  192,  192,    0,
    0,  192,    0,    0,    0,  192,  192,  192,  192,  192,
    0,  192,  192,  192,  192,  192,  192,  192,    0,  192,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  192,    0,    0,    0,    0,    0,    0,    0,  193,  193,
  193,  192,  192,  192,  192,    0,    0,  193,    0,  193,
    0,    0,  193,  193,  193,  193,  193,  193,  193,  193,
  193,  193,    0,  193,  193,    0,  193,  193,  193,  193,
  193,  193,  193,  194,    0,  193,  193,  193,  193,    0,
    0,  193,    0,    0,    0,  193,  193,  193,  193,  193,
    0,  193,  193,  193,  193,  193,  193,  193,    0,  193,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  193,    0,    0,    0,    0,    0,    0,    0,  194,  194,
  194,  193,  193,  193,  193,    0,    0,  194,    0,  194,
    0,    0,  194,  194,  194,  194,  194,  194,  194,  194,
  194,  194,    0,  194,  194,    0,  194,  194,  194,  194,
  194,  194,  194,  195,    0,  194,  194,  194,  194,    0,
    0,  194,    0,    0,    0,  194,  194,  194,  194,  194,
    0,  194,  194,  194,  194,  194,  194,  194,    0,  194,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  194,    0,    0,    0,    0,    0,    0,    0,  195,  195,
  195,  194,  194,  194,  194,    0,    0,  195,    0,  195,
    0,    0,  195,  195,  195,  195,  195,  195,  195,  195,
  195,  195,    0,  195,  195,    0,  195,  195,  195,  195,
  195,  195,  195,    0,    0,  195,  195,  195,  195,    0,
    0,  195,    0,    0,    0,  195,  195,  195,  195,  195,
    0,  195,  195,  195,  195,  195,  195,  195,    0,  195,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  195,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  195,  195,  195,  195,  312,    0,    0,  313,  314,
  315,  316,  317,  318,  319,  320,  321,  322,    0,  323,
  324,    0,  325,  326,  327,  328,  329,  330,  331,    0,
    0,  332,  333,  334,  335,    0,    0,  336,    0,    0,
    0,  337,  338,  339,  340,  341,    0,  342,  343,  344,
  345,  346,  347,  348,    0,  349,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  350,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  351,  352,  353,
  354,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  302,   33,    6,    7,   60,   40,  353,   40,  123,
  507,  123,   40,   40,   97,  123,   36,  123,  123,  123,
   61,   28,   40,  540,   31,   28,   44,   33,  207,   36,
  209,   38,  123,   36,  123,   38,   44,  688,   40,  123,
   40,  123,   40,   50,   40,  280,  116,   44,  123,  284,
  133,  134,  135,   44,   44,   44,  123,   64,   78,    0,
  239,  563,  564,   44,  566,  148,  123,   42,  123,  304,
   77,   78,   40,   80,   77,   78,   44,  728,   42,  276,
   87,  260,   89,  280,  274,  582,   42,   44,  314,   61,
  316,  266,  162,  100,  101,  612,  103,  323,  615,   61,
  107,  108,   93,  173,  107,   62,   62,  125,  178,  179,
  285,  194,  195,  196,  174,  270,  271,  125,   93,  309,
  180,  294,  295,  302,   52,   53,  361,   91,  125,   61,
  476,  366,  139,  216,  141,  125,  125,   42,  640,   44,
  642,  643,   40,   41,  125,  355,   44,  798,  218,  384,
  385,    0,  222,  257,  224,  365,   42,  125,  368,   51,
  274,   53,  274,  223,  247,  362,  274,  309,  274,  274,
  177,  231,   41,  383,   60,   44,  236,  237,   42,   61,
   44,  109,  260,  274,   41,  274,   41,   44,  116,   44,
  274,  370,  274,  290,  291,  309,   88,  309,   90,  274,
  207,  309,   61,  309,  706,   91,  276,  274,  228,  290,
  291,  271,  272,    0,  711,  275,  280,  274,   40,   41,
   82,   41,   44,  230,   44,  308,   42,  310,  309,  157,
   41,  257,  287,   44,  162,  242,   98,  123,  272,    0,
  272,  260,  104,  274,  272,  272,  329,  330,  331,  484,
  485,  334,  749,  115,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,  347,  348,  274,   42,  274,   44,
  272,  277,  272,  483,  272,   41,  272,  274,   44,  489,
   42,   61,   44,  260,  291,  292,  293,   41,  371,   86,
   44,   40,  502,  503,  364,  290,  291,  225,  123,  294,
  295,  163,  164,  363,   40,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  325,  326,
  327,  328,   42,  267,   44,  332,  333,  268,  269,  336,
  267,  272,  273,  353,  275,  123,   41,   41,  573,   44,
   44,  138,  425,  140,  351,  352,  353,  288,  289,  277,
  353,   41,   41,   41,   44,   44,   44,  154,   41,   42,
   41,  571,  572,   44,  226,   40,   60,  638,  639,  310,
  641,  257,  258,  259,  395,  261,  262,  263,  399,  265,
   42,  402,   44,   42,  123,   44,  272,  273,  395,   40,
  411,  123,  399,  279,  363,  402,   40,   91,  405,   40,
  286,   41,   42,   40,  411,  412,  203,  414,   44,  206,
   41,   42,   58,  648,   41,   42,  423,   41,   42,  268,
  269,  309,  429,  272,  273,   58,  275,  487,   42,  123,
   58,  702,   62,  704,  705,   41,   42,  647,   61,  288,
  289,  134,  135,   40,   40,  362,  453,   42,  362,  370,
  533,  534,  535,   44,   44,  475,  476,  274,  759,  542,
  274,  310,  260,  470,  370,  257,  370,  474,  475,  476,
   44,  474,  475,  476,  745,  361,    0,  362,  257,  362,
   44,  268,  269,  362,   40,  272,  273,  257,  275,  375,
  376,  377,  274,  274,  362,  362,   61,  257,  708,  257,
  403,  288,  289,  274,  317,  257,  257,  268,  269,  273,
   44,  272,  273,   44,  275,   44,   44,   44,  370,  370,
  370,   44,  370,  310,  370,  370,  370,  288,  289,  370,
  370,  538,  370,  540,  617,  370,  543,  544,  545,  546,
  547,  548,  549,  550,  551,  552,  553,  554,  568,  310,
    0,  370,  635,   44,  266,   44,  735,   44,  565,    0,
  567,  568,  565,   44,  567,  568,   44,  637,    0,   44,
  264,   44,   44,   44,   44,   44,   44,    0,   44,  273,
  759,   44,   44,   44,   44,   44,  362,  281,  282,  283,
  284,  285,  389,   44,   91,  678,  679,  394,   44,   44,
  397,  398,   40,  400,  401,  612,  403,  404,  615,  406,
  407,  408,  409,  410,   44,   44,  413,   44,  415,  416,
  417,  418,   44,   44,   44,   44,   44,   44,  257,  636,
  700,   44,   44,  703,   44,   44,   44,  644,   44,  699,
   44,  644,   91,  257,  258,  259,  653,  261,  262,  263,
  733,  265,   40,   41,   44,  362,   44,   44,  272,  273,
  362,   44,  257,  258,  259,  279,  261,  262,  263,  363,
  265,   44,  286,  743,   62,  472,   44,  362,  257,  257,
  740,   44,  742,  362,  279,   40,   93,  694,   40,  257,
  257,  286,  363,   40,   40,  778,  779,  780,   40,   40,
  370,  370,  370,    0,   44,   93,  713,  790,   60,   40,
  770,   54,  509,  148,   10,   18,  513,  737,  211,  516,
   64,  147,  519,  207,  191,  177,  230,  710,  525,  526,
  696,  528,  739,  386,   42,  357,  357,  125,  370,   91,
  739,   40,  713,  634,  268,  269,   -1,  361,  272,  273,
   -1,  275,   -1,   -1,   -1,   -1,   -1,   -1,  555,  556,
  557,  375,  376,  377,  288,  289,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,  782,   -1,   -1,   -1,   -1,
  787,  788,  789,   -1,   -1,   -1,  310,  584,   -1,  586,
  587,   -1,  589,  590,   -1,  592,  593,   -1,  595,  596,
  597,  598,  599,   -1,   -1,  602,   -1,  604,  605,  606,
  607,   -1,   -1,   -1,   -1,   -1,  613,   -1,  268,  269,
   -1,   40,  272,  273,   -1,  275,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,   -1,  268,  269,  288,  289,
  272,  273,   -1,  275,   -1,  268,  269,  288,  289,  272,
  273,   -1,  275,   -1,   -1,   -1,  288,  289,  655,   -1,
  310,  658,   -1,   -1,  661,  288,  289,  664,   -1,  310,
   -1,   -1,   -1,  670,  671,   -1,  673,   -1,  310,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  274,  310,   -1,   -1,
   -1,   -1,  689,  690,  691,   -1,   -1,   -1,   -1,  696,
   -1,   40,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,  290,  291,
  272,  273,  294,  295,  296,  297,  298,  279,   -1,   -1,
  318,  319,  320,   -1,  286,   -1,   -1,   -1,   -1,  327,
  737,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,
  368,  369,  370,  371,  372,  373,  374,  375,  376,  377,
   40,  379,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,  361,
   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,  318,
  319,  320,   -1,  375,  376,  377,   -1,   -1,  327,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,  273,  274,  355,  356,  357,  358,
   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   40,
  379,   -1,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,
  319,  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,  273,  274,  355,  356,  357,  358,
   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   40,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,
  319,  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   40,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,
  320,   -1,   -1,  288,  289,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,  310,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,  273,  274,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   40,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,  320,
  276,   -1,   -1,  279,   -1,   -1,  327,   -1,  329,   -1,
  286,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,  274,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,  274,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,  273,  274,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  390,
  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,  279,   -1,  318,  319,  320,   -1,
   -1,  286,   -1,   -1,   -1,  327,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,
  273,  274,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   40,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,  380,
  381,  382,  383,  384,  385,  386,  387,  388,  389,  401,
  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
  273,  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,    0,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,
  403,  404,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
  273,  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   40,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,
  403,  404,   -1,   -1,  327,   60,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   41,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   91,   -1,  361,  273,
  274,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   41,   -1,   -1,   -1,   -1,   -1,   -1,  390,  123,   -1,
   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,   -1,   -1,  318,  319,  320,   -1,   60,   -1,
   -1,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   91,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   42,   -1,
   44,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   60,   42,   -1,   44,
   -1,  123,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   60,   -1,  401,  402,  403,
  404,  268,  269,   -1,   -1,  272,  273,   91,  275,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  288,  289,   -1,   -1,   60,   91,   -1,   -1,   -1,
   -1,   -1,  257,  258,  259,   60,  261,  262,  263,  123,
  265,   -1,   -1,  310,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,   -1,   91,   -1,  123,   -1,
   -1,  286,   -1,   -1,   -1,   -1,   91,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,  276,  123,   -1,
  279,   -1,   -1,   60,   -1,   -1,   -1,  286,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   60,
  261,  262,  263,   91,  265,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   91,  276,   -1,   -1,  279,   -1,
  272,  273,   -1,   -1,   -1,  286,  361,  279,   -1,   -1,
   91,   -1,   -1,   -1,  286,  123,   -1,   -1,   -1,   60,
  375,  376,  377,   -1,   -1,   -1,  123,   -1,  300,  301,
   -1,  303,  304,  305,  306,  307,  308,   -1,   -1,   -1,
   -1,   -1,  123,  257,  258,  259,   -1,  261,  262,  263,
   91,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,  257,  258,  259,  279,  261,  262,  263,   -1,
  265,   -1,  286,   -1,   -1,   -1,   -1,  272,  273,  125,
   -1,   -1,  123,   -1,  279,   -1,   -1,   -1,   -1,  361,
   -1,  286,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,  375,  376,  377,   -1,  272,  273,  264,
   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,  273,   -1,
   -1,  286,   -1,   -1,   -1,  223,  281,  282,  283,  284,
  285,   -1,   -1,  231,   -1,  300,  301,   -1,  303,  304,
  305,  306,  307,  308,   -1,   -1,   -1,  361,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,  375,  376,  377,  272,  273,  361,  264,   -1,   -1,
   -1,  279,   -1,  271,  272,   -1,  273,  275,  286,   -1,
  375,  376,  377,  264,  281,  282,  283,  284,  285,   60,
  345,   -1,  273,   -1,   -1,   -1,  361,  295,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
  375,  376,  377,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,  264,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,  345,   -1,
   -1,   -1,  123,  361,   -1,   -1,   -1,   41,   -1,   -1,
  358,  302,  360,   41,  345,  363,   -1,  375,  376,  377,
   -1,   -1,  318,  319,  320,   -1,   60,   -1,  376,   -1,
  378,  327,   60,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   91,   -1,  355,
  356,  357,  358,   91,   -1,  361,  260,   -1,   -1,  365,
  366,  367,  368,  369,  125,  371,  372,  373,  374,  375,
  376,  377,  276,  379,   -1,   -1,   -1,   -1,   -1,  123,
   -1,   -1,   -1,   -1,  390,  123,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,   -1,
   -1,   -1,  273,  307,  308,   -1,   -1,  311,  312,  313,
  314,  315,  316,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  479,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  487,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,  497,
   -1,  499,  273,   -1,  125,   -1,  504,  318,  319,  320,
  281,  282,  283,  284,  285,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,  302,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,  390,
  264,   -1,   -1,   -1,  125,   -1,  264,   -1,   -1,  273,
  401,  402,  403,  404,  278,  273,   -1,  281,  282,  283,
  284,  285,   -1,  281,  282,  283,  284,  285,   -1,  293,
   -1,   -1,   -1,   -1,   -1,  293,   -1,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,  273,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,  327,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,  273,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
  327,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
  327,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
  327,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,  273,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   60,   -1,   -1,  365,  366,  367,  368,  369,
  273,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   91,   -1,   -1,   60,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,  327,  123,  329,   91,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
  273,   -1,  355,  356,  357,  358,   -1,   -1,  361,  123,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   60,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,
  403,  404,   -1,   -1,  327,   -1,  329,   -1,   91,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   41,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,
  123,   60,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,  264,  390,   60,   -1,
   -1,   -1,   91,   -1,   -1,  273,   -1,   -1,  401,  402,
  403,  404,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,   60,   -1,   91,   -1,   -1,   -1,   60,   91,
  264,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   60,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   91,   -1,   -1,  123,   -1,   -1,   91,
   -1,  123,  330,  331,   -1,  299,  300,  301,   -1,  303,
  304,  305,  306,  307,  308,   91,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,   -1,   -1,  123,   -1,   60,
   -1,  264,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   91,   -1,   60,   -1,   -1,
   91,   -1,   60,   -1,  123,   -1,   -1,  300,  301,   -1,
  303,  304,  305,  306,  307,  308,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   91,  125,
   60,   -1,  123,   91,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  264,   -1,   -1,
  123,   91,  264,  265,   -1,  123,  273,  125,   -1,   -1,
   -1,  273,  274,   -1,  281,  282,  283,  284,  285,  281,
  282,  283,  284,  285,   -1,  292,  264,   60,   -1,   -1,
  297,  298,  264,  123,  125,  273,   -1,   -1,   -1,   -1,
  278,  273,   -1,  281,  282,  283,  284,  285,  264,  281,
  282,  283,  284,  285,   -1,  293,   60,  273,   91,   -1,
   93,  293,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   -1,  125,  292,   -1,  257,  258,
  259,  260,  261,  262,  263,  264,  265,   91,   -1,   60,
  123,   -1,   -1,   -1,  273,  274,   -1,  276,   -1,   -1,
  279,   -1,  281,  282,  283,  284,  285,  286,  264,  265,
   -1,   -1,   -1,  264,  265,   -1,   -1,  273,  274,  123,
   91,   -1,  273,  274,   -1,  281,  282,  283,  284,  285,
  281,  282,  283,  284,  285,  125,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
  273,  274,  123,   -1,   -1,  273,   -1,   -1,  281,  282,
  283,  284,  285,  281,  282,  283,  284,  285,   -1,  260,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,  276,   -1,   -1,  278,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  261,  262,   -1,   -1,
  260,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,   -1,
  311,  312,  313,  314,  315,  316,  276,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,  307,  308,   -1,
  264,  311,  312,  313,  314,  315,  316,   -1,   -1,  273,
  260,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   -1,   -1,  276,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  273,  274,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,  307,  308,   -1,
   -1,  311,  312,  313,  314,  315,  316,  382,  383,  384,
  385,   -1,   -1,   -1,   -1,   -1,  391,  392,  393,  394,
  395,  396,  397,  398,  399,  400,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,  327,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,
  };

#line 1088 "Repil/IR/IR.jay"

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
  public const int FASTCC = 299;
  public const int SIGNEXT = 300;
  public const int ZEROEXT = 301;
  public const int VOLATILE = 302;
  public const int RETURNED = 303;
  public const int NONNULL = 304;
  public const int NOCAPTURE = 305;
  public const int WRITEONLY = 306;
  public const int READONLY = 307;
  public const int READNONE = 308;
  public const int ATTRIBUTE_GROUP_REF = 309;
  public const int ATTRIBUTES = 310;
  public const int NORECURSE = 311;
  public const int NOUNWIND = 312;
  public const int SPECULATABLE = 313;
  public const int SSP = 314;
  public const int UWTABLE = 315;
  public const int ARGMEMONLY = 316;
  public const int SEQ_CST = 317;
  public const int RET = 318;
  public const int BR = 319;
  public const int SWITCH = 320;
  public const int INDIRECTBR = 321;
  public const int INVOKE = 322;
  public const int RESUME = 323;
  public const int CATCHSWITCH = 324;
  public const int CATCHRET = 325;
  public const int CLEANUPRET = 326;
  public const int UNREACHABLE = 327;
  public const int FNEG = 328;
  public const int ADD = 329;
  public const int NUW = 330;
  public const int NSW = 331;
  public const int FADD = 332;
  public const int SUB = 333;
  public const int FSUB = 334;
  public const int MUL = 335;
  public const int FMUL = 336;
  public const int UDIV = 337;
  public const int SDIV = 338;
  public const int FDIV = 339;
  public const int UREM = 340;
  public const int SREM = 341;
  public const int FREM = 342;
  public const int SHL = 343;
  public const int LSHR = 344;
  public const int EXACT = 345;
  public const int ASHR = 346;
  public const int AND = 347;
  public const int OR = 348;
  public const int XOR = 349;
  public const int EXTRACTELEMENT = 350;
  public const int INSERTELEMENT = 351;
  public const int SHUFFLEVECTOR = 352;
  public const int EXTRACTVALUE = 353;
  public const int INSERTVALUE = 354;
  public const int ALLOCA = 355;
  public const int LOAD = 356;
  public const int STORE = 357;
  public const int FENCE = 358;
  public const int CMPXCHG = 359;
  public const int ATOMICRMW = 360;
  public const int GETELEMENTPTR = 361;
  public const int ALIGN = 362;
  public const int INBOUNDS = 363;
  public const int INRANGE = 364;
  public const int TRUNC = 365;
  public const int ZEXT = 366;
  public const int SEXT = 367;
  public const int FPTRUNC = 368;
  public const int FPEXT = 369;
  public const int TO = 370;
  public const int FPTOUI = 371;
  public const int FPTOSI = 372;
  public const int UITOFP = 373;
  public const int SITOFP = 374;
  public const int PTRTOINT = 375;
  public const int INTTOPTR = 376;
  public const int BITCAST = 377;
  public const int ADDRSPACECAST = 378;
  public const int ICMP = 379;
  public const int EQ = 380;
  public const int NE = 381;
  public const int UGT = 382;
  public const int UGE = 383;
  public const int ULT = 384;
  public const int ULE = 385;
  public const int SGT = 386;
  public const int SGE = 387;
  public const int SLT = 388;
  public const int SLE = 389;
  public const int FCMP = 390;
  public const int OEQ = 391;
  public const int OGT = 392;
  public const int OGE = 393;
  public const int OLT = 394;
  public const int OLE = 395;
  public const int ONE = 396;
  public const int ORD = 397;
  public const int UEQ = 398;
  public const int UNE = 399;
  public const int UNO = 400;
  public const int PHI = 401;
  public const int SELECT = 402;
  public const int CALL = 403;
  public const int TAIL = 404;
  public const int VA_ARG = 405;
  public const int LANDINGPAD = 406;
  public const int CATCHPAD = 407;
  public const int CLEANUPPAD = 408;
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
