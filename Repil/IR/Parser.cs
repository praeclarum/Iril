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
  { yyVal = false; }
  break;
case 27:
#line 154 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 28:
#line 158 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 159 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 30:
#line 163 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 31:
  case_31();
  break;
case 32:
  case_32();
  break;
case 33:
#line 180 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 34:
#line 181 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 182 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 183 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 184 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 185 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 39:
#line 186 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 40:
#line 190 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 41:
#line 194 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 42:
#line 201 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 43:
#line 205 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 44:
#line 212 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 45:
#line 216 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 46:
#line 220 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
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
case 66:
#line 261 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 67:
#line 265 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 68:
#line 269 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 69:
#line 276 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 280 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 72:
#line 285 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 75:
#line 291 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 76:
#line 292 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 77:
#line 293 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 78:
#line 294 "Repil/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 79:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 80:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 81:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 82:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 83:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 84:
#line 318 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 85:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 337 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 89:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true);
    }
  break;
case 90:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 91:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true);
    }
  break;
case 93:
#line 360 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: true, (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 364 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-7+yyTop]);
    }
  break;
case 95:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-8+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 96:
#line 372 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 97:
#line 376 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 98:
#line 380 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-9+yyTop]);
    }
  break;
case 99:
#line 384 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-11+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 100:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], isExternal: (bool)yyVals[-10+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 101:
#line 395 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
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
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 104:
#line 407 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 105:
#line 408 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 106:
#line 415 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 426 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 430 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 110:
#line 434 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 111:
#line 438 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 113:
#line 446 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 114:
#line 450 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 115:
#line 451 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 116:
#line 452 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 117:
#line 453 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 118:
#line 454 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 119:
#line 455 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 120:
#line 456 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 121:
#line 457 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 127:
#line 475 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 128:
#line 476 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 129:
#line 477 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 130:
#line 478 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 131:
#line 479 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 132:
#line 480 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 133:
#line 481 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 134:
#line 482 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 135:
#line 483 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 136:
#line 484 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 137:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 138:
#line 489 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 139:
#line 490 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 140:
#line 491 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 141:
#line 492 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 142:
#line 493 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 143:
#line 494 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 144:
#line 495 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 145:
#line 496 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 146:
#line 497 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 147:
#line 498 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 148:
#line 499 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 149:
#line 500 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 150:
#line 501 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 151:
#line 502 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 152:
#line 503 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 154:
#line 508 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 155:
#line 509 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 156:
#line 513 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 525 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 160:
#line 529 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 161:
#line 533 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 162:
#line 537 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 164:
#line 545 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 165:
#line 546 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 166:
#line 547 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 167:
#line 548 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 168:
#line 549 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 169:
#line 550 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 170:
#line 551 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 171:
#line 552 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 172:
#line 553 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 173:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 176:
#line 578 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 585 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 589 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 596 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 618 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 622 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 629 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 637 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 641 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 189:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 659 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 663 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 667 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 671 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 679 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 197:
#line 680 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 198:
#line 687 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 199:
#line 691 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 200:
#line 698 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 201:
#line 702 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 202:
#line 706 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 203:
#line 710 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 204:
#line 714 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 205:
#line 718 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 206:
#line 722 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 208:
#line 727 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 209:
#line 731 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 735 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 739 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 743 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 217:
#line 760 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 764 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 770 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 220:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 806 "Repil/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 229:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 233:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 238:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 239:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 241:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 244:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 245:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 246:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
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
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 952 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 956 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 960 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 964 "Repil/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 968 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 268:
#line 972 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 269:
#line 976 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 270:
#line 980 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 271:
#line 984 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 988 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 992 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 996 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1000 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1004 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 1008 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 1012 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1016 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1020 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 281:
#line 1024 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 282:
#line 1028 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1032 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1036 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1040 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 286:
#line 1044 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 287:
#line 1048 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1052 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1056 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1060 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1064 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1068 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1072 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1076 "Repil/IR/IR.jay"
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

void case_31()
#line 168 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_32()
#line 173 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
    6,    6,    6,    6,    6,   10,   10,   16,   16,   15,
    9,    9,   17,   17,   17,   17,   17,   17,   17,   17,
   17,   13,   13,    8,    8,    8,    8,    8,   21,   21,
   21,    7,    7,   23,   23,   23,   23,   23,   23,   23,
   23,   23,   23,   23,   23,    3,    3,    3,   24,   24,
   25,   25,   11,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   26,   26,   27,   27,    4,    4,
    4,    4,    4,    4,    4,    4,    4,    4,    4,    4,
    5,    5,    5,   28,   28,   33,   33,   34,   34,   34,
   34,   35,   35,   31,   31,   31,   31,   31,   31,   31,
   31,   14,   14,   29,   29,   36,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   38,   38,   38,   38,
   38,   38,   38,   38,   38,   38,   38,   38,   38,   38,
   38,   38,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   41,   18,   18,   18,   18,   18,   18,   18,
   18,   18,   42,   22,   22,   43,   40,   40,   19,   44,
   39,   39,   30,   30,   45,   45,   45,   45,   46,   46,
   48,   48,   48,   48,   50,   51,   51,   52,   52,   53,
   53,   53,   53,   53,   53,   53,   54,   54,   54,   54,
   54,   54,   20,   20,   55,   55,   56,   56,   57,   58,
   58,   59,   60,   60,   61,   61,   32,   62,   47,   47,
   47,   47,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    3,    3,    3,    3,    3,    6,
    5,    2,    3,    1,    2,    3,    3,    3,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    3,    1,    1,    1,    4,    2,    3,    5,    1,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    3,    4,
    2,    1,    5,    5,    1,    3,    1,    1,    9,    9,
   10,   10,   11,    9,   10,   11,   11,   11,   13,   12,
    5,    6,    6,    3,    2,    1,    3,    1,    2,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    6,    9,    6,    6,    3,
    3,    3,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,    2,    1,    2,    1,    3,    2,    1,
    1,    3,    1,    2,    2,    3,    1,    2,    1,    2,
    1,    2,    3,    4,    1,    3,    2,    1,    3,    2,
    3,    3,    3,    2,    4,    5,    1,    1,    6,    9,
    6,    6,    1,    3,    1,    1,    1,    3,    5,    1,
    2,    3,    1,    2,    1,    1,    1,    1,    2,    7,
    2,    7,    5,    6,    5,    5,    5,    6,    4,    4,
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
   72,   82,   75,   76,   77,   78,   74,    0,   29,   28,
    0,    0,    0,   73,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  122,  123,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   66,    0,
    0,    0,    0,    0,    0,   81,  227,  119,  120,  121,
  114,  115,  117,  116,  118,    0,    0,    0,    0,    0,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   67,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   88,
   79,    0,    0,   85,    0,    0,    0,  166,  167,  165,
  168,  169,  170,  164,  155,  154,  172,  171,    0,    0,
    0,    0,    0,    0,    0,    0,  153,    0,    0,    0,
    0,    0,    0,    0,    0,   31,    0,    0,    0,   51,
   50,   13,    0,    0,   44,   49,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  110,  111,  105,    0,    0,
  106,  126,    0,    0,  124,   80,    0,    0,    0,    0,
    0,    0,   64,   56,   54,   55,   57,   58,   59,   60,
    0,   52,    0,    0,    0,    0,  177,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   15,    0,
    0,    0,   45,   14,    0,  174,    0,   83,   68,   84,
    0,    0,    0,    0,    0,  112,    0,  104,    0,    0,
    0,    0,  125,   86,    0,    0,    0,    0,   12,   53,
    0,    0,    0,    0,  162,    0,  160,  161,    0,    0,
    0,    0,    0,    0,   35,    0,   33,    0,   36,   37,
   38,   39,   32,   17,   16,   48,   47,   46,    0,    0,
    0,    0,    0,    0,    0,  113,  107,    0,    0,   42,
    0,    0,   61,  216,  215,    0,  213,    0,    0,    0,
    0,  178,    0,    0,    0,    0,    0,    0,    0,  179,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  183,    0,    0,  189,    0,    0,    0,    0,    0,
    0,    0,   43,    0,   65,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,   41,    0,    0,    0,
    0,    0,  231,    0,    0,  229,    0,  225,  226,    0,
    0,  223,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  228,  255,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  127,  128,
  129,  130,  131,  132,  133,  134,  135,  136,    0,  137,
  138,  149,  150,  151,  152,  140,  142,  143,  144,  145,
  141,  139,  147,  148,  146,    0,    0,    0,    0,    0,
    0,    0,   94,  184,    0,  190,    0,    0,    0,    0,
    0,    0,    0,   89,    0,   90,  214,    0,  159,  156,
  158,    0,    0,    0,    0,   40,   92,    0,    0,    0,
  173,    0,    0,    0,    0,  224,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  217,    0,  195,    0,    0,    0,
    0,    0,    0,    0,    0,   95,    0,    0,    0,    0,
   91,    0,    0,    0,   93,   97,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  251,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   96,   98,    0,    0,  180,    0,  181,    0,    0,  233,
    0,  252,  287,    0,  261,  272,    0,  256,  290,    0,
  276,  254,  292,  284,  280,    0,    0,  269,    0,  237,
  236,  271,  293,    0,    0,  235,    0,  163,  176,    0,
    0,    0,    0,    0,    0,    0,    0,  218,    0,    0,
  197,    0,    0,  198,    0,    0,  241,    0,    0,    0,
    0,    0,  100,    0,  157,    0,    0,    0,    0,  220,
  234,  288,  273,  277,  281,  270,  238,  265,  282,    0,
    0,    0,    0,    0,    0,  264,  253,    0,    0,    0,
    0,  200,    0,  196,    0,    0,  242,    0,    0,  249,
    0,   99,  182,  230,    0,  232,  221,    0,  267,    0,
  285,    0,  219,  278,    0,  208,  202,    0,    0,    0,
    0,  207,  203,  201,  199,    0,  250,  222,  268,  286,
  205,    0,    0,    0,    0,    0,  206,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  212,  209,  211,    0,    0,  210,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  191,  153,  145,   50,
  154,  557,  232,   51,   52,   36,  146,  137,  708,  286,
  155,  645,  192,   61,   62,  113,  114,  109,  174,  351,
  226,   78,  170,  171,  227,  175,  449,  466,  646,  198,
  679,  386,  610,  647,  352,  353,  354,  355,  356,  558,
  633,  693,  694,  763,  287,  554,  555,  709,  710,  391,
  392,  424,
  };
  protected static readonly short [] yySindex = {          167,
  -41, -192,   32,   66,   75, 3744, 3816, -246,    0,  167,
    0,    0,    0,    0, -187,   84,   92,  360, -107,  -26,
    0,    0,    0,    0,    0,    0,    0, 4018,    0,    0,
 3956, -109,  -76,    0,  169, 3613,  -34, 4018,  -29,  155,
    0,    0,  -35,   33,    0,    0,    0,    0,    0, 4018,
   -7, -205,  -62,  -57,  232,  -24,  196,   -3,    0,  169,
  -25,  289,   77, 4018,   83,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   -1, 4018, 3677,  315, 3590,
    1,  315,  292,    0,    0, 1992, 4018,   -7, 4018,   -7,
    0,  298,    0, -172,  342,  302, 3912,  315,    0, 4018,
 4018,  -15, 4018,  315,    2,    3, 4018, 3529, -103,    0,
    0,  169,   79,    0,  315, -103, 3843,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   40,  404,
  405,  406, 4044, 4044, 4044,  403,    0, 1992, 4018, 1992,
 4018,  393,  396,  401,  166,    0, -172, 3927,    0,    0,
    0,    0,    4, 1992,    0,    0, -205,  169,   20,  399,
   26, -103,  315,  315,    5,    0,    0,    0,   -9,  168,
    0,    0,  153, -197,    0,    0, 3618,  153,  153,  153,
  402,  427,    0,    0,    0,    0,    0,    0,    0,    0,
  -94,    0,  431, 4044, 4044, 4044,    0,   12,   63,   13,
  110,  430, 1992,  432,  114, 3863,  204, 1391,    0, -172,
  190,    7,    0,    0, 3942,    0,  153,    0,    0,    0,
  153, -115,  153, -205,  315,    0,  460,    0, 3770, -113,
  206, -111,    0,    0,  153,  153,  223, 1150,    0,    0,
 4018,  112,  115,  116,    0, 4044,    0,    0,  227,  130,
  449,  136,  137,  456,    0,  461,    0, 1053,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -110, -197,
 4864, -102, -197,  153, -205,    0,    0, 4864,  -99,    0,
  228, 4864,    0,    0,    0,  290,    0,  118, 4018, 4018,
 4018,    0,  229,  249,  145,  253,  255,  156,  556,    0,
 4864,  -96,  -95,  458, 4044, -186, 4044, 2056, 4018, 2056,
 4018, 2056, 4018, 4018,  300, 4018, 4018, 4018, 2056,  720,
 2029, 4018, 4018, 4018, 4044, 4044, 4044, 4018, 3501, 3728,
  200,  124, 4044, 4044, 4044, 4044, 4044, 4044, 4044, 4044,
 4044, 4044, 4044, 4044, 1365, 3981, 4018, 4018, 3613,  117,
 2070,    0, 4864,  229,    0,  229, 4864,  -88, -105,  153,
 2157, 4864,    0, 2237,    0, 1150, 4044,  259,  328,  357,
  248,  229,  266,  229,    0,  269,    0,  294, 2326, 4864,
 4864, 5260,    0,  254, 1897,    0,  485,    0,    0, 1992,
 2056,    0, 1992, 1992, 2056, 1992, 1992, 2056, 1992, 1992,
 4018, 1992, 1992, 1992, 1992, 1992, 2056, 4018, 1992, 4018,
 1992, 1992, 1992, 1992,  487,  505,  509,  197, 4018,  222,
 4044,  512,    0,    0, 4018,  238,  195,  199,  201,  202,
  205,  208,  216,  218,  220,  221,  225,  226,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4018,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4018,   18, 1992,  350, 4018,
 3677, 3613,    0,    0,  229,    0,  248,  248, 2413, 4864,
 4864,  -77, -197,    0, 2493,    0,    0,  522,    0,    0,
    0,  248,  229,  248,  229,    0,    0, 2582, 2669,  229,
    0,  526,  308,  548, 1992,    0,  554,  555, 1992,  559,
  560, 1992,  562,  566, 1992,  570,  572,  574,  580,  581,
 1992, 1992,  583, 1992,  584,  587,  588,  589, 4044, 4044,
 4044,  273,  283, 4018,  593, 4018,  352, 4044, 4018, 4018,
 4018, 4018, 4018, 4018, 4018, 4018, 4018, 4018, 4018, 4018,
 1992, 1992, 1897,  595,    0,  596,    0,  601,  350,  350,
 4018,  350, 4018, 3677,  248,    0, 2749, 2838, 4864,  -73,
    0, 4044,  248,  248,    0,    0,  248,  308,  551, 1897,
  602, 1897, 1897,  605, 1897, 1897,  615, 1897, 1897,  616,
 1897, 1897, 1897, 1897, 1897,  618,  619, 1897,  624, 1897,
 1897, 1897, 1897,    0,  627,  633,  343, 4018, 1992,  639,
 4018,  642, 4044,  647,  169,  169,  169,  169,  169,  169,
  169,  169,  169,  169,  169,  169,  648,  652,  653,  553,
 4044, 3715,  153,  601,  601,  350,  601,  350,  350, 4018,
    0,    0, 2925, 4864,    0,  305,    0,  654, 4018,    0,
 1897,    0,    0, 1897,    0,    0, 1897,    0,    0, 1897,
    0,    0,    0,    0,    0, 1897, 1897,    0, 1897,    0,
    0,    0,    0, 4044, 4044,    0,  655,    0,    0,  312,
  656,  344,  657, 4044, 1897, 1897, 1897,    0,  658, 3971,
    0, 1931,  307,    0,  153,  153,    0,  601,  153,  601,
  601,  350,    0, 3005,    0, 4044,  308,  660, 3984,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  346,
  408,  348,  455, 4044,  669,    0,    0,  622, 4044,  677,
  473,    0, 2018,    0, 4011,  153,    0,  153,  153,    0,
  601,    0,    0,    0,  308,    0,    0,  463,    0,  464,
    0,  669,    0,    0, 2087,    0,    0,  361,  683,  688,
  693,    0,    0,    0,    0,  153,    0,    0,    0,    0,
    0,  313,  697, 4044, 4044, 4044,    0, 4018,  369,  371,
  372,  380, 4018, 4018, 4018, 4044,  370,  387,  391,  699,
    0,    0,    0, 4044,  322,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  747,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  713,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   52,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  708,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   99,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  708,    0,  708,
    0,    0,    0,    0,    0,    0,    0,    0,  678,    0,
    0,    0,    0,  708,    0,    0,    0,   78,  708,    0,
  708,    0,    0,    0,    0,    0,    0,    0,  280,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   64,
 4028, 4038,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  708,    0,  708,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  324,    0,    0,    0,
    0,    0,    0,    0,  236,  279,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  521,    0,    0,    0,    0,  339,    0,  708,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  708,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3094,    0, 4944,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  708,  708,  708,
  621,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  708,
    0,    0,  708,  708,    0,  708,  708,    0,  708,  708,
    0,  708,  708,  708,  708,  708,    0,    0,  708,    0,
  708,  708,  708,  708,    0,    0,    0,  708,    0,  708,
    0,    0,    0,    0,    0,  708,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  708,  708,    0,    0,
    0,    0,    0,    0, 3181,    0, 3261, 5024,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  630, 1829, 1838,    0,    0,    0,    0,    0, 5104,
    0,    0,    0,    0,  708,    0,    0,    0,  708,    0,
    0,  708,    0,    0,  708,    0,    0,    0,    0,    0,
  708,  708,    0,  708,    0,    0,    0,    0,    0,    0,
    0,    0,  708,    0,    0,    0,  708,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  708,  708,    0, 4064,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 3350,    0,    0,    0,    0,    0,
    0,    0, 1909, 1933,    0,    0, 5184,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  708,    0,
    0,    0,    0,    0,  767,  847,  927, 1016, 1105, 1185,
 1265, 1354, 1443, 1523, 1603, 1692,    0,    0,    0,    0,
    0,    0, 4144,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  708,    0,    0, 4224,    0,    0,    0, 4304,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4384,    0,    0,    0,    0,  340,
  708,    0,    0,    0,    0, 4464,    0, 4544, 4624,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4704,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 4784,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  708,    0,    0,    0,    0,  708,  708,  708,    0,
    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  739,  696,    0,    0,    0,    0,  603,  607,   38,
   -6,  276, 3473,  600,    0,  737,  546, -168,  552, -297,
    0,  -81,  578,  706,   -2,    0,  597,  263,  -32, -213,
  -19, -334,    0,  547,   90, -119,    0,    0, -637,  303,
    0, -480, -487,   69, -209,    0,  433,  434,  410, -142,
 -121,    0,   48,    0,  418,    0,  165,    0,   93, -117,
 -215,    0,
  };
  protected static readonly short [] yyTable = {            35,
   35,  378,   92,   37,   39,   80,   57,  271,   96,  278,
   80,  282,  301,   64,  471,  156,   77,  481,  100,   15,
  357,   35,  579,  362,   60,   58,  380,  381,  100,   35,
  239,   35,   66,   76,  480,   81,   80,  259,   80,  262,
   80,   80,   80,   86,   80,  569,  725,  215,  612,  644,
  215,  197,  197,  197,  233,  246,  246,   60,  107,   66,
  233,   66,   40,  101,  361,   31,  156,   66,  364,  285,
   35,   35,   42,  112,  105,  106,  231,   16,   17,  384,
  138,  218,  140,  180,   45,   46,  752,  379,   87,  300,
   89,   71,   18,  158,  159,   69,  161,  648,  385,   99,
   35,  169,  233,  142,  165,  248,  246,  143,  553,  160,
  233,  172,  242,  243,  244,  233,  233,   71,  220,  176,
  677,   70,  177,  681,  247,  139,   19,  141,  214,  222,
  285,  265,  203,  268,  205,   20,  245,  564,   71,   87,
  230,  474,   87,  479,   43,  235,  236,   63,  485,  233,
  233,  474,   44,  233,  474,   66,  795,  253,  231,   66,
  231,  367,  281,  231,  292,  181,  498,  499,  231,  474,
  112,  281,   54,  134,  281,  506,   69,  281,  281,  506,
   65,  182,  506,   32,  269,  281,   45,   46,  270,  144,
  273,  506,  395,  172,  398,  172,  281,  285,  172,  258,
  281,  407,   70,  172,  135,  172,  209,  276,  228,  210,
   66,  229,  183,  184,   33,   83,  185,  186,  187,  188,
  189,  190,  169,  383,   84,  387,  744,   45,   46,   91,
  264,   47,   48,  210,  288,  102,  133,   79,   66,  233,
  532,  359,   82,  415,  416,  417,   31,   55,  422,   95,
   56,  427,  428,  429,  430,  431,  432,  433,  434,  435,
  436,  437,  438,   66,  768,  534,  567,  568,   98,  474,
  104,   94,  115,  163,  164,  474,  225,  213,  103,   66,
  213,  538,  368,  369,  370,  488,   47,   48,  474,  474,
   68,   69,   85,   70,   71,   72,   73,   74,   75,  489,
   66,  390,  393,  394,  396,  397,  399,  400,  402,  403,
  404,  405,  406,  409,  411,  412,  413,  414,   97,   71,
  108,  418,  420,  108,   66,  426,  608,  483,   80,  470,
  365,  101,  101,  366,  496,  101,  101,  366,  101,  535,
  467,  468,   35,  101,  116,  705,  469,  734,  706,  103,
  735,  101,  101,  777,  108,  643,  366,  474,  474,   32,
  157,  136,  796,  233,  109,  706,  162,  109,  490,   66,
  118,  119,  120,  101,  121,  122,  123,  178,  124,   34,
  204,  147,   34,  204,  505,  125,  126,   21,  509,   80,
   33,  512,  127,   66,  515,  613,   22,  491,   66,  128,
  521,  522,  193,  524,   23,   24,   25,   26,   27,  134,
  791,   66,  533,  202,  117,  204,  634,  635,  537,  637,
   64,   66,   31,  786,  148,  223,  224,  792,   66,  216,
  704,  793,   66,  474,    1,    2,  199,  200,    3,    4,
  135,    5,  551,  194,  195,  196,  201,  604,  605,  606,
  206,  561,  563,  207,    6,    7,  614,  772,  208,  552,
  219,  172,  237,   35,   35,   35,  238,  559,  560,  562,
  241,  249,  133,  250,  129,  252,    8,  261,  251,  280,
  254,  289,  283,  293,  290,  291,  425,  275,  130,  131,
  132,  294,  295,  698,  474,  700,  701,  296,  297,  298,
  299,  363,  231,  102,  102,  372,  373,  102,  102,  374,
  102,  375,  696,  697,   66,  699,  423,  376,  382,  472,
   21,  281,  493,  102,  102,  495,  501,  609,  503,  609,
  529,  683,  615,  616,  617,  618,  619,  620,  621,  622,
  623,  624,  625,  626,  640,  102,  103,  103,  530,  689,
  103,  103,  531,  103,   35,  536,   35,   35,  636,  741,
  638,  639,  762,   21,  539,  572,  103,  103,  540,  578,
  541,  542,   22,  384,  543,  233,  737,  544,  739,  740,
   23,   24,   25,   26,   27,  545,  285,  546,  103,  547,
  548,  580,  718,  719,  549,  550,  377,  582,  583,  676,
  695,  609,  585,  586,  609,  588,  118,  119,  120,  589,
  121,  122,  123,  591,  124,  592,  233,  593,  233,  767,
   18,  125,  126,  594,  595,  692,  598,  600,  127,   19,
  601,  602,  603,   35,  607,  128,  611,  702,  630,  631,
  632,  649,  258,  553,  401,  651,  233,  754,  654,   45,
   46,   88,   90,   47,   48,   49,   29,   30,  657,  660,
  502,  666,  667,  736,  749,  504,  738,  669,  507,  508,
  674,  510,  511,  721,  513,  514,  675,  516,  517,  518,
  519,  520,  680,  731,  523,  682,  525,  526,  527,  528,
  684,  685,  779,  780,  781,  686,  687,  707,  720,  722,
  724,  729,  258,  745,  790,  723,  766,  748,  173,  750,
  129,  751,  706,  276,  753,  179,  755,   72,  175,  769,
  770,  175,  774,  773,  130,  131,  132,  775,  692,  118,
  119,  120,  776,  121,  122,  123,  778,  124,  783,  175,
  784,  785,  794,  556,  756,  757,    1,   71,   41,   93,
  212,  127,   71,  211,   53,  263,  217,  260,  128,   68,
   69,  221,   70,   71,   72,   73,   74,   75,  240,  102,
  175,  782,   71,  234,  743,  277,  787,  788,  789,   32,
  581,  733,  765,  487,  584,  475,  476,  587,   21,   21,
  590,  500,   21,   21,  688,   21,  596,  597,    0,  599,
    0,  747,  175,   71,    0,    0,   71,    0,   21,   21,
   33,    0,  118,  119,  120,    0,  121,  122,  123,    0,
  124,    0,    0,  274,    0,    0,  627,  628,  629,    0,
   21,  284,    0,  758,  127,   71,    0,    0,    0,    0,
    0,  128,   31,    0,    0,    0,    0,  759,  760,  761,
    0,    0,    0,    0,    0,  650,    0,  652,  653,    0,
  655,  656,    0,  658,  659,    0,  661,  662,  663,  664,
  665,    0,    0,  668,  360,  670,  671,  672,  673,    0,
    0,    0,    0,    0,  678,    0,   71,    0,   18,   18,
    0,    0,   18,   18,    0,   18,    0,   19,   19,    0,
    0,   19,   19,    0,   19,    0,    0,    0,   18,   18,
    0,    0,    0,    0,    0,    0,    0,   19,   19,    0,
    0,    0,    0,    0,    0,    0,  711,    0,    0,  712,
   18,    0,  713,    0,    0,  714,    0,    0,    0,   19,
    0,  715,  716,    0,  717,    0,    0,    0,    0,    0,
  175,  175,    0,    0,    0,    0,    0,    0,    0,    0,
  726,  727,  728,    0,    0,    0,   71,  732,    0,   71,
   71,   71,    0,   71,   71,   71,    0,   71,    0,    0,
    0,    0,    0,   21,   71,   71,    0,    0,    0,    0,
    0,   71,   22,    0,    0,  175,  175,  175,   71,    0,
   23,   24,   25,   26,   27,    0,  175,    0,  764,  175,
  175,  175,  175,  175,  175,  175,  175,  175,  175,    0,
  175,  175,    0,  175,  175,  175,  175,  175,  175,  175,
    0,    0,  175,  175,  175,  175,    0,    0,  175,  289,
  289,    0,  175,  175,  175,  175,  175,  175,  175,  175,
  175,  175,  175,  175,  175,   71,  175,    0,    0,    0,
    0,    0,    0,    0,  408,    0,    0,  175,    0,    0,
    0,    0,    0,   71,    0,    0,    0,    0,  175,  175,
  175,  175,    0,    0,  289,  289,  289,   71,   71,   71,
    0,    0,    0,    0,   66,  289,    0,    0,  289,  289,
  289,  289,  289,  289,  289,  289,  289,  289,    0,  289,
  289,    0,  289,  289,  289,  289,  289,  289,  289,  294,
  294,  289,  289,  289,  289,    0,    0,  289,    0,    0,
    0,  289,  289,  289,  289,  289,    0,  289,  289,  289,
  289,  289,  289,  289,   71,  289,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  289,    0,    0,    0,
    0,    0,    0,    0,  294,  294,  294,  289,  289,  289,
  289,    0,    0,    0,    0,  294,    0,    0,  294,  294,
  294,  294,  294,  294,  294,  294,  294,  294,    0,  294,
  294,    0,  294,  294,  294,  294,  294,  294,  294,  279,
  279,  294,  294,  294,  294,    0,    0,  294,    0,    0,
    0,  294,  294,  294,  294,  294,    0,  294,  294,  294,
  294,  294,  294,  294,   71,  294,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  294,    0,    0,    0,
    0,    0,    0,    0,  279,  279,  279,  294,  294,  294,
  294,    0,    0,    0,    0,  279,    0,    0,  279,  279,
  279,  279,  279,  279,  279,  279,  279,  279,    0,  279,
  279,    0,  279,  279,  279,  279,  279,  279,  279,    0,
    0,  279,  279,  279,  279,    0,    0,  279,  260,  260,
    0,  279,  279,  279,  279,  279,    0,  279,  279,  279,
  279,  279,  279,  279,   71,  279,    0,    0,    0,  118,
  119,  120,    0,  121,  122,  123,  279,  124,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  279,  279,  279,
  279,  127,    0,  260,  260,  260,    0,    0,  128,    0,
    0,    0,    0,    0,  260,    0,    0,  260,  260,  260,
  260,  260,  260,  260,  260,  260,  260,    0,  260,  260,
    0,  260,  260,  260,  260,  260,  260,  260,    0,    0,
  260,  260,  260,  260,    0,    0,  260,  257,  257,    0,
  260,  260,  260,  260,  260,    0,  260,  260,  260,  260,
  260,  260,  260,   71,  260,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  260,  118,  119,  120,    0,
  121,  122,  123,    0,  124,    0,  260,  260,  260,  260,
    0,    0,  257,  257,  257,  284,    0,    0,  127,    0,
    0,    0,    0,  257,    0,  128,  257,  257,  257,  257,
  257,  257,  257,  257,  257,  257,    0,  257,  257,    0,
  257,  257,  257,  257,  257,  257,  257,  258,  258,  257,
  257,  257,  257,    0,    0,  257,    0,    0,    0,  257,
  257,  257,  257,  257,    0,  257,  257,  257,  257,  257,
  257,  257,   71,  257,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  257,    0,    0,    0,    0,    0,
    0,    0,  258,  258,  258,  257,  257,  257,  257,    0,
    0,    0,    0,  258,    0,    0,  258,  258,  258,  258,
  258,  258,  258,  258,  258,  258,    0,  258,  258,    0,
  258,  258,  258,  258,  258,  258,  258,  259,  259,  258,
  258,  258,  258,    0,    0,  258,    0,    0,    0,  258,
  258,  258,  258,  258,    0,  258,  258,  258,  258,  258,
  258,  258,   71,  258,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  258,    0,    0,    0,    0,    0,
    0,    0,  259,  259,  259,  258,  258,  258,  258,    0,
    0,    0,    0,  259,    0,    0,  259,  259,  259,  259,
  259,  259,  259,  259,  259,  259,    0,  259,  259,    0,
  259,  259,  259,  259,  259,  259,  259,    0,    0,  259,
  259,  259,  259,    0,    0,  259,  291,  291,    0,  259,
  259,  259,  259,  259,    0,  259,  259,  259,  259,  259,
  259,  259,   71,  259,    0,    0,    0,  118,  119,  120,
    0,  121,  122,  123,  259,  124,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  259,  259,  259,  259,  127,
    0,  291,  291,  291,    0,    0,  128,    0,    0,    0,
    0,    0,  291,    0,    0,  291,  291,  291,  291,  291,
  291,  291,  291,  291,  291,    0,  291,  291,    0,  291,
  291,  291,  291,  291,  291,  291,    0,    0,  291,  291,
  291,  291,    0,    0,  291,  283,  283,    0,  291,  291,
  291,  291,  291,    0,  291,  291,  291,  291,  291,  291,
  291,   71,  291,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  291,  439,  440,  441,  442,  443,  444,
  445,  446,  447,  448,  291,  291,  291,  291,    0,    0,
  283,  283,  283,    0,    0,    0,    0,    0,    0,    0,
    0,  283,    0,    0,  283,  283,  283,  283,  283,  283,
  283,  283,  283,  283,    0,  283,  283,    0,  283,  283,
  283,  283,  283,  283,  283,  275,  275,  283,  283,  283,
  283,    0,    0,  283,    0,    0,    0,  283,  283,  283,
  283,  283,    0,  283,  283,  283,  283,  283,  283,  283,
    0,  283,    0,    0,    0,    0,    0,    0,   20,    0,
    0,    0,  283,    0,    0,    0,    0,   25,    0,    0,
  275,  275,  275,  283,  283,  283,  283,    0,    0,    0,
    0,  275,    0,    0,  275,  275,  275,  275,  275,  275,
  275,  275,  275,  275,    0,  275,  275,    0,  275,  275,
  275,  275,  275,  275,  275,  266,  266,  275,  275,  275,
  275,    0,    0,  275,    0,    0,    0,  275,  275,  275,
  275,  275,    0,  275,  275,  275,  275,  275,  275,  275,
    0,  275,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,  275,    0,    0,    0,    0,    0,    0,    0,
  266,  266,  266,  275,  275,  275,  275,    0,    0,    0,
    0,  266,   24,    0,  266,  266,  266,  266,  266,  266,
  266,  266,  266,  266,    0,  266,  266,    0,  266,  266,
  266,  266,  266,  266,  266,    0,  134,  266,  266,  266,
  266,    0,    0,  266,  239,  239,    0,  266,  266,  266,
  266,  266,   66,  266,  266,  266,  266,  266,  266,  266,
    0,  266,    0,    0,    0,    0,    0,  135,    0,    0,
  134,    0,  266,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  266,  266,  266,  266,    0,    0,  239,
  239,  239,    0,    0,    0,    0,    0,    0,    0,  133,
  239,  135,    0,  239,  239,  239,  239,  239,  239,  239,
  239,  239,  239,   66,  239,  239,    0,  239,  239,  239,
  239,  239,  239,  239,    0,    0,  239,  239,  239,  239,
    0,  134,  239,  133,    0,    0,  239,  239,  239,  239,
  239,    0,  239,  239,  239,  239,  239,  239,  239,    0,
  239,    0,    0,    0,    0,    0,    0,  134,    0,    0,
    0,  239,  135,    0,    0,    0,    0,    0,   32,    0,
    0,    0,  239,  239,  239,  239,   20,   20,    0,    0,
   20,   20,    0,   20,    0,   25,   25,    0,  135,   25,
   25,    0,   25,    0,  133,   32,   20,   20,    0,   33,
    0,    0,    0,    0,    0,   25,   25,  771,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   20,    0,
  133,    0,    0,    0,    0,    0,   33,   25,    0,    0,
    0,   31,    0,  118,  119,  120,    0,  121,  122,  123,
    0,  124,    0,    0,    0,    0,    0,    0,  125,  126,
    0,    0,    0,    0,    0,  127,   22,   22,   31,    0,
   22,   22,  128,   22,    0,    0,    0,  118,  119,  120,
    0,  121,  122,  123,  473,  124,   22,   22,    0,    0,
   24,   24,  125,  126,   24,   24,    0,   24,    0,  127,
    0,    0,    0,    0,    0,    0,  128,    0,   22,    0,
   24,   24,    0,    0,    0,    0,    0,    0,    0,    0,
   68,   69,    0,   70,   71,   72,   73,   74,   75,    0,
    0,    0,   24,    0,    0,    0,    0,    0,  118,  119,
  120,    0,  121,  122,  123,    0,  124,  129,    0,    0,
    0,    0,    0,  125,  126,    0,    0,    0,    0,    0,
  127,  130,  131,  132,  118,  119,  120,  128,  121,  122,
  123,  484,  124,    0,    0,    0,    0,    0,    0,  125,
  126,  129,   21,    0,    0,    0,  127,    0,    0,    0,
    0,   22,    0,  128,    0,  130,  131,  132,    0,   23,
   24,   25,   26,   27,    0,    0,    0,   68,   69,   21,
   70,   71,   72,   73,   74,   75,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,    0,  304,  118,  119,  120,    0,  121,  122,  123,
    0,  124,  129,    0,    0,    0,    0,    0,    0,    0,
    0,  486,  284,    0,    0,  127,  130,  131,  132,    0,
    0,    0,  128,  410,    0,    0,    0,    0,  129,    0,
    0,    0,    0,    0,    0,  388,  389,  305,  306,  307,
    0,    0,  130,  131,  132,    0,    0,    0,  308,    0,
    0,  309,  310,  311,  312,  313,  314,  315,  316,  317,
  318,    0,  319,  320,    0,  321,  322,  323,  324,  325,
  326,  327,    0,    0,  328,  329,  330,  331,    0,  304,
  332,    0,    0,    0,  333,  334,  335,  336,  337,    0,
  338,  339,  340,  341,  342,  343,  344,    0,  345,    0,
  497,    0,    0,    0,    0,    0,    0,    0,    0,  346,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  347,  348,  349,  350,  305,  306,  307,    0,    0,    0,
    0,    0,    0,    0,    0,  308,    0,    0,  309,  310,
  311,  312,  313,  314,  315,  316,  317,  318,    0,  319,
  320,    0,  321,  322,  323,  324,  325,  326,  327,  304,
    0,  328,  329,  330,  331,    0,    0,  332,    0,    0,
    0,  333,  334,  335,  336,  337,    0,  338,  339,  340,
  341,  342,  343,  344,    0,  345,    0,  566,    0,    0,
    0,    0,    0,    0,    0,    0,  346,    0,    0,    0,
    0,    0,    0,    0,  305,  306,  307,  347,  348,  349,
  350,    0,    0,    0,    0,  308,    0,    0,  309,  310,
  311,  312,  313,  314,  315,  316,  317,  318,    0,  319,
  320,    0,  321,  322,  323,  324,  325,  326,  327,    0,
    0,  328,  329,  330,  331,    0,    0,  332,  304,    0,
    0,  333,  334,  335,  336,  337,    0,  338,  339,  340,
  341,  342,  343,  344,    0,  345,    0,  571,    0,    0,
    0,    0,    0,    0,    0,    0,  346,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  347,  348,  349,
  350,    0,    0,  305,  306,  307,    0,    0,    0,    0,
    0,    0,    0,    0,  308,    0,    0,  309,  310,  311,
  312,  313,  314,  315,  316,  317,  318,    0,  319,  320,
    0,  321,  322,  323,  324,  325,  326,  327,    0,    0,
  328,  329,  330,  331,    0,  304,  332,    0,    0,    0,
  333,  334,  335,  336,  337,    0,  338,  339,  340,  341,
  342,  343,  344,    0,  345,    0,  575,    0,    0,    0,
    0,    0,    0,    0,    0,  346,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  347,  348,  349,  350,
  305,  306,  307,    0,    0,    0,    0,    0,    0,    0,
    0,  308,    0,    0,  309,  310,  311,  312,  313,  314,
  315,  316,  317,  318,    0,  319,  320,    0,  321,  322,
  323,  324,  325,  326,  327,  304,    0,  328,  329,  330,
  331,    0,    0,  332,    0,    0,    0,  333,  334,  335,
  336,  337,    0,  338,  339,  340,  341,  342,  343,  344,
    0,  345,    0,  576,    0,    0,    0,    0,    0,    0,
    0,    0,  346,    0,    0,    0,    0,    0,    0,    0,
  305,  306,  307,  347,  348,  349,  350,    0,    0,    0,
    0,  308,    0,    0,  309,  310,  311,  312,  313,  314,
  315,  316,  317,  318,    0,  319,  320,    0,  321,  322,
  323,  324,  325,  326,  327,    0,    0,  328,  329,  330,
  331,    0,    0,  332,  304,    0,    0,  333,  334,  335,
  336,  337,    0,  338,  339,  340,  341,  342,  343,  344,
    0,  345,    0,  641,    0,    0,    0,    0,    0,    0,
    0,    0,  346,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  347,  348,  349,  350,    0,    0,  305,
  306,  307,    0,    0,    0,    0,    0,    0,    0,    0,
  308,    0,    0,  309,  310,  311,  312,  313,  314,  315,
  316,  317,  318,    0,  319,  320,    0,  321,  322,  323,
  324,  325,  326,  327,    0,    0,  328,  329,  330,  331,
    0,  304,  332,    0,    0,    0,  333,  334,  335,  336,
  337,    0,  338,  339,  340,  341,  342,  343,  344,    0,
  345,    0,  642,    0,    0,    0,    0,    0,    0,    0,
    0,  346,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  347,  348,  349,  350,  305,  306,  307,    0,
    0,    0,    0,    0,    0,    0,    0,  308,    0,    0,
  309,  310,  311,  312,  313,  314,  315,  316,  317,  318,
    0,  319,  320,    0,  321,  322,  323,  324,  325,  326,
  327,  304,    0,  328,  329,  330,  331,    0,    0,  332,
    0,    0,    0,  333,  334,  335,  336,  337,    0,  338,
  339,  340,  341,  342,  343,  344,    0,  345,    0,  703,
    0,    0,    0,    0,    0,    0,    0,    0,  346,    0,
    0,    0,    0,    0,    0,    0,  305,  306,  307,  347,
  348,  349,  350,    0,    0,    0,    0,  308,    0,    0,
  309,  310,  311,  312,  313,  314,  315,  316,  317,  318,
    0,  319,  320,    0,  321,  322,  323,  324,  325,  326,
  327,    0,    0,  328,  329,  330,  331,    0,    0,  332,
  304,    0,    0,  333,  334,  335,  336,  337,    0,  338,
  339,  340,  341,  342,  343,  344,    0,  345,    0,  742,
    0,    0,    0,    0,    0,    0,    0,    0,  346,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  347,
  348,  349,  350,    0,    0,  305,  306,  307,    0,    0,
    0,    0,    0,    0,    0,    0,  308,    0,    0,  309,
  310,  311,  312,  313,  314,  315,  316,  317,  318,    0,
  319,  320,    0,  321,  322,  323,  324,  325,  326,  327,
    0,    0,  328,  329,  330,  331,    0,  304,  332,    0,
    0,    0,  333,  334,  335,  336,  337,    0,  338,  339,
  340,  341,  342,  343,  344,    0,  345,    0,  187,    0,
    0,    0,    0,    0,    0,    0,    0,  346,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  347,  348,
  349,  350,  305,  306,  307,    0,    0,    0,    0,    0,
    0,    0,    0,  308,    0,    0,  309,  310,  311,  312,
  313,  314,  315,  316,  317,  318,    0,  319,  320,    0,
  321,  322,  323,  324,  325,  326,  327,  304,    0,  328,
  329,  330,  331,    0,    0,  332,    0,    0,    0,  333,
  334,  335,  336,  337,    0,  338,  339,  340,  341,  342,
  343,  344,    0,  345,    0,  185,    0,    0,    0,    0,
    0,    0,    0,    0,  346,    0,    0,    0,    0,    0,
    0,    0,  305,  306,  307,  347,  348,  349,  350,    0,
    0,    0,    0,  308,    0,    0,  309,  310,  311,  312,
  313,  314,  315,  316,  317,  318,    0,  319,  320,    0,
  321,  322,  323,  324,  325,  326,  327,    0,    0,  328,
  329,  330,  331,    0,    0,  332,  187,    0,    0,  333,
  334,  335,  336,  337,    0,  338,  339,  340,  341,  342,
  343,  344,    0,  345,    0,  188,    0,    0,    0,    0,
    0,    0,    0,    0,  346,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  347,  348,  349,  350,    0,
    0,  187,  187,  187,    0,    0,    0,    0,    0,    0,
    0,    0,  187,    0,    0,  187,  187,  187,  187,  187,
  187,  187,  187,  187,  187,    0,  187,  187,    0,  187,
  187,  187,  187,  187,  187,  187,    0,    0,  187,  187,
  187,  187,    0,  185,  187,    0,    0,    0,  187,  187,
  187,  187,  187,    0,  187,  187,  187,  187,  187,  187,
  187,    0,  187,    0,  186,    0,    0,    0,    0,    0,
    0,    0,    0,  187,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  187,  187,  187,  187,  185,  185,
  185,    0,    0,    0,    0,    0,    0,    0,    0,  185,
    0,    0,  185,  185,  185,  185,  185,  185,  185,  185,
  185,  185,    0,  185,  185,    0,  185,  185,  185,  185,
  185,  185,  185,  188,    0,  185,  185,  185,  185,    0,
    0,  185,    0,    0,    0,  185,  185,  185,  185,  185,
    0,  185,  185,  185,  185,  185,  185,  185,    0,  185,
   32,    0,    0,    0,    0,    0,    0,    0,    0,  168,
  185,    0,    0,    0,    0,    0,    0,    0,  188,  188,
  188,  185,  185,  185,  185,    0,    0,    0,   32,  188,
    0,   33,  188,  188,  188,  188,  188,  188,  188,  188,
  188,  188,    0,  188,  188,    0,  188,  188,  188,  188,
  188,  188,  188,    0,    0,  188,  188,  188,  188,   33,
    0,  188,  186,   31,    0,  188,  188,  188,  188,  188,
  111,  188,  188,  188,  188,  188,  188,  188,    0,  188,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   32,
  188,   31,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  188,  188,  188,  188,    0,    0,  186,  186,  186,
    0,    0,   32,    0,    0,    0,    0,   32,  186,    0,
   33,  186,  186,  186,  186,  186,  186,  186,  186,  186,
  186,    0,  186,  186,  272,  186,  186,  186,  186,  186,
  186,  186,  279,   33,  186,  186,  186,  186,   33,    0,
  186,    0,   31,    0,  186,  186,  186,  186,  186,    0,
  186,  186,  186,  186,  186,  186,  186,    0,  186,    0,
    0,    0,    0,    0,    0,   31,   32,    0,    0,  186,
   31,  302,  303,    0,    0,  358,    0,    0,    0,    0,
  186,  186,  186,  186,    0,  691,    0,    0,    0,    0,
    0,    0,    0,    0,   21,  371,    0,   33,    0,    0,
    0,    0,    0,   22,   32,    0,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,    0,   32,    0,    0,
    0,    0,   21,    0,    0,    0,    0,    0,    0,   31,
    0,   22,  419,   32,    0,   33,  166,    0,    0,   23,
   24,   25,   26,   27,    0,    0,    0,    0,   33,    0,
    0,  167,    0,    0,    0,    0,  477,    0,  478,   32,
    0,  482,    0,    0,   33,    0,    0,   31,    0,    0,
    0,    0,    0,    0,  492,    0,  494,    0,    0,    0,
   31,    0,    0,   21,    0,    0,    0,    0,    0,    0,
   33,    0,   22,    0,    0,    0,   31,    0,    0,    0,
   23,   24,   25,   26,   27,   32,   21,    0,    0,    0,
    0,   21,  110,    0,    0,   22,    0,    0,    0,    0,
   22,    0,   31,   23,   24,   25,   26,   27,   23,   24,
   25,   26,   27,    0,    0,    0,   33,    0,    0,    0,
  110,   67,   68,   69,    0,   70,   71,   72,   73,   74,
   75,    0,   32,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   31,    0,
   21,    0,    0,    0,    0,    0,    0,  565,    0,   22,
    0,    0,    0,   33,    0,  570,    0,   23,   24,   25,
   26,   27,    0,    0,    0,  573,    0,  574,    0,    0,
    0,   32,  577,    0,    0,    0,   68,   69,   21,   70,
   71,   72,   73,   74,   75,   31,   32,   22,    0,    0,
    0,  149,  690,    0,    0,   23,   24,   25,   26,   27,
   22,   32,   33,    0,    0,    0,    0,   21,   23,   24,
   25,   26,   27,    0,    0,   32,   22,   33,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,  421,
   32,    0,   33,   21,   31,   28,  152,    0,    0,    0,
   29,   30,   22,   32,    0,    0,   33,  166,    0,   31,
   23,   24,   25,   26,   27,    0,    0,    0,    0,    0,
    0,   33,  167,    0,   31,    0,    0,    0,    0,    0,
   32,    0,    0,    0,   33,    0,  746,   32,   31,   21,
   59,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,   31,    0,    0,   23,   24,   25,   26,
   27,   33,  181,   32,    0,    0,   31,   38,   33,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  182,  118,
  119,  120,  255,  121,  122,  123,   21,  124,    0,    0,
    0,    0,    0,   31,   33,   22,  256,    0,  257,    0,
   31,  127,    0,   23,   24,   25,   26,   27,  128,  183,
  184,    0,   62,  185,  186,  187,  188,  189,  190,    0,
    0,    0,   63,    0,    0,    0,   31,    0,    0,    0,
    0,    0,    0,    0,    0,  149,  150,    0,    0,    0,
    0,    0,    0,    0,   22,  151,    0,    0,    0,    0,
  149,  150,   23,   24,   25,   26,   27,    0,    0,   22,
  151,    0,    0,    0,    0,  149,  266,   23,   24,   25,
   26,   27,    0,    0,   22,  267,    0,    0,    0,   21,
    0,    0,   23,   24,   25,   26,   27,    0,   22,    0,
    0,    0,    0,    0,   21,    0,   23,   24,   25,   26,
   27,  450,  451,   22,  730,    0,    0,   21,    0,    0,
    0,   23,   24,   25,   26,   27,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,    0,
    0,    0,    0,    0,   21,    0,    0,    0,    0,    0,
    0,   21,    0,   22,    0,    0,    0,   62,  690,    0,
   22,   23,   24,   25,   26,   27,    0,   63,   23,   24,
   25,   26,   27,   62,    0,    0,    0,  149,    0,    0,
    0,    0,    0,   63,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,    0,
    0,    0,    0,    0,   62,   62,  274,  274,   62,   62,
   62,   62,   62,   62,   63,   63,    0,    0,   63,   63,
   63,   63,   63,   63,    0,    0,    0,    0,    0,    0,
    0,    0,  452,  453,  454,  455,    0,    0,    0,    0,
    0,  456,  457,  458,  459,  460,  461,  462,  463,  464,
  465,  274,  274,  274,    0,    0,    0,    0,    0,    0,
    0,    0,  274,    0,    0,  274,  274,  274,  274,  274,
  274,  274,  274,  274,  274,    0,  274,  274,    0,  274,
  274,  274,  274,  274,  274,  274,  240,  240,  274,  274,
  274,  274,    0,    0,  274,    0,    0,    0,  274,  274,
  274,  274,  274,    0,  274,  274,  274,  274,  274,  274,
  274,    0,  274,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  274,    0,    0,    0,    0,    0,    0,
    0,  240,  240,  240,  274,  274,  274,  274,    0,    0,
    0,    0,  240,    0,    0,  240,  240,  240,  240,  240,
  240,  240,  240,  240,  240,    0,  240,  240,    0,  240,
  240,  240,  240,  240,  240,  240,  243,  243,  240,  240,
  240,  240,    0,    0,  240,    0,    0,    0,  240,  240,
  240,  240,  240,    0,  240,  240,  240,  240,  240,  240,
  240,    0,  240,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  240,    0,    0,    0,    0,    0,    0,
    0,  243,  243,  243,  240,  240,  240,  240,    0,    0,
    0,    0,  243,    0,    0,  243,  243,  243,  243,  243,
  243,  243,  243,  243,  243,    0,  243,  243,    0,  243,
  243,  243,  243,  243,  243,  243,  248,  248,  243,  243,
  243,  243,    0,    0,  243,    0,    0,    0,  243,  243,
  243,  243,  243,    0,  243,  243,  243,  243,  243,  243,
  243,    0,  243,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  243,    0,    0,    0,    0,    0,    0,
    0,  248,  248,  248,  243,  243,  243,  243,    0,    0,
    0,    0,  248,    0,    0,  248,  248,  248,  248,  248,
  248,  248,  248,  248,  248,    0,  248,  248,    0,  248,
  248,  248,  248,  248,  248,  248,  262,  262,  248,  248,
  248,  248,    0,    0,  248,    0,    0,    0,  248,  248,
  248,  248,  248,    0,  248,  248,  248,  248,  248,  248,
  248,    0,  248,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  248,    0,    0,    0,    0,    0,    0,
    0,  262,  262,  262,  248,  248,  248,  248,    0,    0,
    0,    0,  262,    0,    0,  262,  262,  262,  262,  262,
  262,  262,  262,  262,  262,    0,  262,  262,    0,  262,
  262,  262,  262,  262,  262,  262,  244,  244,  262,  262,
  262,  262,    0,    0,  262,    0,    0,    0,  262,  262,
  262,  262,  262,    0,  262,  262,  262,  262,  262,  262,
  262,    0,  262,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  262,    0,    0,    0,    0,    0,    0,
    0,  244,  244,  244,  262,  262,  262,  262,    0,    0,
    0,    0,  244,    0,    0,  244,  244,  244,  244,  244,
  244,  244,  244,  244,  244,    0,  244,  244,    0,  244,
  244,  244,  244,  244,  244,  244,  245,  245,  244,  244,
  244,  244,    0,    0,  244,    0,    0,    0,  244,  244,
  244,  244,  244,    0,  244,  244,  244,  244,  244,  244,
  244,    0,  244,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  244,    0,    0,    0,    0,    0,    0,
    0,  245,  245,  245,  244,  244,  244,  244,    0,    0,
    0,    0,  245,    0,    0,  245,  245,  245,  245,  245,
  245,  245,  245,  245,  245,    0,  245,  245,    0,  245,
  245,  245,  245,  245,  245,  245,  246,  246,  245,  245,
  245,  245,    0,    0,  245,    0,    0,    0,  245,  245,
  245,  245,  245,    0,  245,  245,  245,  245,  245,  245,
  245,    0,  245,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  245,    0,    0,    0,    0,    0,    0,
    0,  246,  246,  246,  245,  245,  245,  245,    0,    0,
    0,    0,  246,    0,    0,  246,  246,  246,  246,  246,
  246,  246,  246,  246,  246,    0,  246,  246,    0,  246,
  246,  246,  246,  246,  246,  246,  263,  263,  246,  246,
  246,  246,    0,    0,  246,    0,    0,    0,  246,  246,
  246,  246,  246,    0,  246,  246,  246,  246,  246,  246,
  246,    0,  246,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  246,    0,    0,    0,    0,    0,    0,
    0,  263,  263,  263,  246,  246,  246,  246,    0,    0,
    0,    0,  263,    0,    0,  263,  263,  263,  263,  263,
  263,  263,  263,  263,  263,    0,  263,  263,    0,  263,
  263,  263,  263,  263,  263,  263,  247,  247,  263,  263,
  263,  263,    0,    0,  263,    0,    0,    0,  263,  263,
  263,  263,  263,    0,  263,  263,  263,  263,  263,  263,
  263,    0,  263,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  263,    0,    0,    0,    0,    0,    0,
    0,  247,  247,  247,  263,  263,  263,  263,    0,    0,
    0,    0,  247,    0,    0,  247,  247,  247,  247,  247,
  247,  247,  247,  247,  247,    0,  247,  247,    0,  247,
  247,  247,  247,  247,  247,  247,  304,    0,  247,  247,
  247,  247,    0,    0,  247,    0,    0,    0,  247,  247,
  247,  247,  247,    0,  247,  247,  247,  247,  247,  247,
  247,    0,  247,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  247,    0,    0,    0,    0,    0,    0,
    0,  305,  306,  307,  247,  247,  247,  247,    0,    0,
    0,    0,  308,    0,    0,  309,  310,  311,  312,  313,
  314,  315,  316,  317,  318,    0,  319,  320,    0,  321,
  322,  323,  324,  325,  326,  327,  191,    0,  328,  329,
  330,  331,    0,    0,  332,    0,    0,    0,  333,  334,
  335,  336,  337,    0,  338,  339,  340,  341,  342,  343,
  344,    0,  345,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  346,    0,    0,    0,    0,    0,    0,
    0,  191,  191,  191,  347,  348,  349,  350,    0,    0,
    0,    0,  191,    0,    0,  191,  191,  191,  191,  191,
  191,  191,  191,  191,  191,    0,  191,  191,    0,  191,
  191,  191,  191,  191,  191,  191,  192,    0,  191,  191,
  191,  191,    0,    0,  191,    0,    0,    0,  191,  191,
  191,  191,  191,    0,  191,  191,  191,  191,  191,  191,
  191,    0,  191,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  191,    0,    0,    0,    0,    0,    0,
    0,  192,  192,  192,  191,  191,  191,  191,    0,    0,
    0,    0,  192,    0,    0,  192,  192,  192,  192,  192,
  192,  192,  192,  192,  192,    0,  192,  192,    0,  192,
  192,  192,  192,  192,  192,  192,  193,    0,  192,  192,
  192,  192,    0,    0,  192,    0,    0,    0,  192,  192,
  192,  192,  192,    0,  192,  192,  192,  192,  192,  192,
  192,    0,  192,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  192,    0,    0,    0,    0,    0,    0,
    0,  193,  193,  193,  192,  192,  192,  192,    0,    0,
    0,    0,  193,    0,    0,  193,  193,  193,  193,  193,
  193,  193,  193,  193,  193,    0,  193,  193,    0,  193,
  193,  193,  193,  193,  193,  193,  194,    0,  193,  193,
  193,  193,    0,    0,  193,    0,    0,    0,  193,  193,
  193,  193,  193,    0,  193,  193,  193,  193,  193,  193,
  193,    0,  193,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  193,    0,    0,    0,    0,    0,    0,
    0,  194,  194,  194,  193,  193,  193,  193,    0,    0,
    0,    0,  194,    0,    0,  194,  194,  194,  194,  194,
  194,  194,  194,  194,  194,    0,  194,  194,    0,  194,
  194,  194,  194,  194,  194,  194,    0,    0,  194,  194,
  194,  194,    0,    0,  194,    0,    0,    0,  194,  194,
  194,  194,  194,    0,  194,  194,  194,  194,  194,  194,
  194,    0,  194,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  194,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  194,  194,  194,  194,  308,    0,
    0,  309,  310,  311,  312,  313,  314,  315,  316,  317,
  318,    0,  319,  320,    0,  321,  322,  323,  324,  325,
  326,  327,    0,    0,  328,  329,  330,  331,    0,    0,
  332,    0,    0,    0,  333,  334,  335,  336,  337,    0,
  338,  339,  340,  341,  342,  343,  344,    0,  345,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  346,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  347,  348,  349,  350,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  299,   60,    6,    7,   40,   33,  123,   33,  123,
   40,  123,  123,  123,  349,   97,   36,  123,   44,   61,
  123,   28,  503,  123,   31,   28,  123,  123,   44,   36,
  125,   38,   42,   36,  123,   38,   40,  206,   40,  208,
   40,   40,   40,   50,   40,  123,  684,   44,  536,  123,
   44,  133,  134,  135,  174,   44,   44,   64,   78,   42,
  180,   42,  309,    0,  278,  123,  148,   42,  282,  238,
   77,   78,  260,   80,   77,   78,  274,  270,  271,  266,
   87,   62,   89,  116,  290,  291,  724,  301,   51,  258,
   53,   40,   61,  100,  101,   44,  103,  578,  285,  125,
  107,  108,  222,  276,  107,   93,   44,  280,   91,  125,
  230,  309,  194,  195,  196,  235,  236,   40,   93,   41,
  608,   44,   44,  611,   62,   88,   61,   90,  125,  162,
  299,  125,  139,  215,  141,   61,  125,  472,   40,   41,
  173,  351,   44,  357,   61,  178,  179,  257,  362,  269,
  270,  361,   61,  273,  364,   42,  794,   44,  274,   42,
  274,   44,  274,  274,  246,  260,  380,  381,  274,  379,
  177,  274,  280,   60,  274,  391,  125,  274,  274,  395,
  257,  276,  398,   60,  217,  274,  290,  291,  221,  362,
  223,  407,  310,  309,  312,  309,  274,  366,  309,  206,
  274,  319,  125,  309,   91,  309,   41,  227,   41,   44,
   42,   44,  307,  308,   91,   61,  311,  312,  313,  314,
  315,  316,  229,  305,  260,  307,  707,  290,  291,  287,
   41,  294,  295,   44,  241,    0,  123,  272,   42,  359,
   44,  274,  272,  325,  326,  327,  123,  274,  330,  274,
  277,  333,  334,  335,  336,  337,  338,  339,  340,  341,
  342,  343,  344,   42,  745,   44,  480,  481,  272,  479,
  272,   40,  272,  272,  272,  485,  272,  274,    0,   42,
  274,   44,  289,  290,  291,  367,  294,  295,  498,  499,
  300,  301,  260,  303,  304,  305,  306,  307,  308,   41,
   42,  308,  309,  310,  311,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  123,   40,
   41,  328,  329,   44,   42,  332,   44,  360,   40,  349,
   41,  268,  269,   44,   41,  272,  273,   44,  275,  421,
  347,  348,  349,  267,   82,   41,  349,   41,   44,  267,
   44,  288,  289,   41,   40,  569,   44,  567,  568,   60,
   98,   86,   41,  483,   41,   44,  104,   44,   41,   42,
  257,  258,  259,  310,  261,  262,  263,  115,  265,   41,
   41,   40,   44,   44,  391,  272,  273,  264,  395,   40,
   91,  398,  279,   42,  401,   44,  273,   41,   42,  286,
  407,  408,  363,  410,  281,  282,  283,  284,  285,   60,
   41,   42,  419,  138,  123,  140,  559,  560,  425,  562,
  123,   42,  123,   44,  123,  163,  164,   41,   42,  154,
  644,   41,   42,  643,  268,  269,  134,  135,  272,  273,
   91,  275,  449,   40,   40,   40,   44,  529,  530,  531,
   58,  471,  472,   58,  288,  289,  538,  755,   58,  466,
   62,  309,   61,  470,  471,  472,   40,  470,  471,  472,
   40,  362,  123,   44,  361,   44,  310,  274,  203,  274,
  205,  370,  260,  257,  370,  370,  363,  225,  375,  376,
  377,  362,   44,  636,  704,  638,  639,  362,  362,   44,
   40,  274,  274,  268,  269,  257,  362,  272,  273,  257,
  275,  257,  634,  635,   42,  637,  317,  362,   61,  403,
    0,  274,  257,  288,  289,  257,  273,  534,   44,  536,
   44,  613,  539,  540,  541,  542,  543,  544,  545,  546,
  547,  548,  549,  550,  564,  310,  268,  269,   44,  631,
  272,  273,   44,  275,  561,   44,  563,  564,  561,  702,
  563,  564,  731,  264,  370,   44,  288,  289,  370,   44,
  370,  370,  273,  266,  370,  695,  698,  370,  700,  701,
  281,  282,  283,  284,  285,  370,  755,  370,  310,  370,
  370,   44,  674,  675,  370,  370,   41,   44,   44,  257,
  633,  608,   44,   44,  611,   44,  257,  258,  259,   44,
  261,  262,  263,   44,  265,   44,  736,   44,  738,  741,
    0,  272,  273,   44,   44,  632,   44,   44,  279,    0,
   44,   44,   44,  640,  362,  286,   44,  640,   44,   44,
   40,   91,  649,   91,  345,   44,  766,  729,   44,  290,
  291,   52,   53,  294,  295,  296,  297,  298,   44,   44,
  385,   44,   44,  696,  257,  390,  699,   44,  393,  394,
   44,  396,  397,  362,  399,  400,   44,  402,  403,  404,
  405,  406,   44,  690,  409,   44,  411,  412,  413,  414,
   44,   44,  774,  775,  776,   44,   44,   44,   44,   44,
   44,   44,  709,   44,  786,  362,  739,  362,  109,  362,
  361,  257,   44,  733,   93,  116,   40,   40,   41,  257,
  257,   44,   40,  363,  375,  376,  377,   40,  735,  257,
  258,  259,   40,  261,  262,  263,   40,  265,  370,   62,
  370,  370,   44,  468,  272,  273,    0,   40,   10,   54,
  148,  279,   40,  147,   18,  210,  157,  206,  286,  300,
  301,  162,  303,  304,  305,  306,  307,  308,  191,   64,
   93,  778,   60,  177,  706,  229,  783,  784,  785,   60,
  505,  692,  735,  366,  509,  353,  353,  512,  268,  269,
  515,  382,  272,  273,  630,  275,  521,  522,   -1,  524,
   -1,  709,  125,   91,   -1,   -1,   40,   -1,  288,  289,
   91,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,  224,   -1,   -1,  551,  552,  553,   -1,
  310,  276,   -1,  361,  279,  123,   -1,   -1,   -1,   -1,
   -1,  286,  123,   -1,   -1,   -1,   -1,  375,  376,  377,
   -1,   -1,   -1,   -1,   -1,  580,   -1,  582,  583,   -1,
  585,  586,   -1,  588,  589,   -1,  591,  592,  593,  594,
  595,   -1,   -1,  598,  275,  600,  601,  602,  603,   -1,
   -1,   -1,   -1,   -1,  609,   -1,   40,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,   -1,   -1,   -1,  288,  289,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  651,   -1,   -1,  654,
  310,   -1,  657,   -1,   -1,  660,   -1,   -1,   -1,  310,
   -1,  666,  667,   -1,  669,   -1,   -1,   -1,   -1,   -1,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  685,  686,  687,   -1,   -1,   -1,   40,  692,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,   -1,  264,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,  273,   -1,   -1,  318,  319,  320,  286,   -1,
  281,  282,  283,  284,  285,   -1,  329,   -1,  733,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,  273,
  274,   -1,  365,  366,  367,  368,  369,  370,  371,  372,
  373,  374,  375,  376,  377,   40,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  345,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,  361,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,   -1,   -1,  318,  319,  320,  375,  376,  377,
   -1,   -1,   -1,   -1,   42,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   40,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   40,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,  273,  274,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   40,  379,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,  390,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,  279,   -1,  318,  319,  320,   -1,   -1,  286,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   -1,   -1,
  355,  356,  357,  358,   -1,   -1,  361,  273,  274,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   40,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,  401,  402,  403,  404,
   -1,   -1,  318,  319,  320,  276,   -1,   -1,  279,   -1,
   -1,   -1,   -1,  329,   -1,  286,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   40,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,  274,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   40,  379,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,   -1,  361,  273,  274,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   40,  379,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  390,  265,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,  279,
   -1,  318,  319,  320,   -1,   -1,  286,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,  273,  274,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   40,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,  380,  381,  382,  383,  384,  385,
  386,  387,  388,  389,  401,  402,  403,  404,   -1,   -1,
  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,  273,  274,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,    0,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,    0,   -1,   -1,
  318,  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,  273,  274,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,    0,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  318,  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,  329,    0,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   60,  355,  356,  357,
  358,   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,
  368,  369,   42,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   60,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,
  329,   91,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   42,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   60,  361,  123,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,  390,   91,   -1,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,  401,  402,  403,  404,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,  268,  269,   -1,   91,  272,
  273,   -1,  275,   -1,  123,   60,  288,  289,   -1,   91,
   -1,   -1,   -1,   -1,   -1,  288,  289,   41,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  310,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   91,  310,   -1,   -1,
   -1,  123,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,  268,  269,  123,   -1,
  272,  273,  286,  275,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,  125,  265,  288,  289,   -1,   -1,
  268,  269,  272,  273,  272,  273,   -1,  275,   -1,  279,
   -1,   -1,   -1,   -1,   -1,   -1,  286,   -1,  310,   -1,
  288,  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  300,  301,   -1,  303,  304,  305,  306,  307,  308,   -1,
   -1,   -1,  310,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  361,   -1,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,  375,  376,  377,  257,  258,  259,  286,  261,  262,
  263,  125,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,  361,  264,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,  273,   -1,  286,   -1,  375,  376,  377,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,  300,  301,  264,
  303,  304,  305,  306,  307,  308,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  273,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,  361,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  125,  276,   -1,   -1,  279,  375,  376,  377,   -1,
   -1,   -1,  286,  345,   -1,   -1,   -1,   -1,  361,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,  318,  319,  320,
   -1,   -1,  375,  376,  377,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,  273,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,  273,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   -1,   -1,
  355,  356,  357,  358,   -1,  273,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,
  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,  273,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  318,  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,  273,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,  273,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,  402,  403,  404,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,
  273,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,  273,  361,   -1,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,  273,   -1,  355,
  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  318,  319,  320,  401,  402,  403,  404,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,   -1,  361,  273,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,   -1,
   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,   -1,   60,  329,
   -1,   91,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   91,
   -1,  361,  273,  123,   -1,  365,  366,  367,  368,  369,
   41,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
  390,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,  320,
   -1,   -1,   60,   -1,   -1,   -1,   -1,   60,  329,   -1,
   91,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,  222,  346,  347,  348,  349,  350,
  351,  352,  230,   91,  355,  356,  357,  358,   91,   -1,
  361,   -1,  123,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,   -1,   -1,   -1,   -1,  123,   60,   -1,   -1,  390,
  123,  269,  270,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,   -1,   41,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,  293,   -1,   91,   -1,   -1,
   -1,   -1,   -1,  273,   60,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   60,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,  123,
   -1,  273,  302,   60,   -1,   91,  278,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   91,   -1,
   -1,  293,   -1,   -1,   -1,   -1,  354,   -1,  356,   60,
   -1,  359,   -1,   -1,   91,   -1,   -1,  123,   -1,   -1,
   -1,   -1,   -1,   -1,  372,   -1,  374,   -1,   -1,   -1,
  123,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,  273,   -1,   -1,   -1,  123,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   60,  264,   -1,   -1,   -1,
   -1,  264,  293,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  273,   -1,  123,  281,  282,  283,  284,  285,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   91,   -1,   -1,   -1,
  293,  299,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,  475,   -1,  273,
   -1,   -1,   -1,   91,   -1,  483,   -1,  281,  282,  283,
  284,  285,   -1,   -1,   -1,  493,   -1,  495,   -1,   -1,
   -1,   60,  500,   -1,   -1,   -1,  300,  301,  264,  303,
  304,  305,  306,  307,  308,  123,   60,  273,   -1,   -1,
   -1,  264,  278,   -1,   -1,  281,  282,  283,  284,  285,
  273,   60,   91,   -1,   -1,   -1,   -1,  264,  281,  282,
  283,  284,  285,   -1,   -1,   60,  273,   91,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  302,
   60,   -1,   91,  264,  123,  292,  125,   -1,   -1,   -1,
  297,  298,  273,   60,   -1,   -1,   91,  278,   -1,  123,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   91,  293,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   91,   -1,   93,   60,  123,  264,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  123,   -1,   -1,  281,  282,  283,  284,
  285,   91,  260,   60,   -1,   -1,  123,  292,   91,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  276,  257,
  258,  259,  260,  261,  262,  263,  264,  265,   -1,   -1,
   -1,   -1,   -1,  123,   91,  273,  274,   -1,  276,   -1,
  123,  279,   -1,  281,  282,  283,  284,  285,  286,  307,
  308,   -1,  125,  311,  312,  313,  314,  315,  316,   -1,
   -1,   -1,  125,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  265,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
  264,  265,  281,  282,  283,  284,  285,   -1,   -1,  273,
  274,   -1,   -1,   -1,   -1,  264,  265,  281,  282,  283,
  284,  285,   -1,   -1,  273,  274,   -1,   -1,   -1,  264,
   -1,   -1,  281,  282,  283,  284,  285,   -1,  273,   -1,
   -1,   -1,   -1,   -1,  264,   -1,  281,  282,  283,  284,
  285,  261,  262,  273,  274,   -1,   -1,  264,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,
   -1,  264,   -1,  273,   -1,   -1,   -1,  260,  278,   -1,
  273,  281,  282,  283,  284,  285,   -1,  260,  281,  282,
  283,  284,  285,  276,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,  276,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,  307,  308,  273,  274,  311,  312,
  313,  314,  315,  316,  307,  308,   -1,   -1,  311,  312,
  313,  314,  315,  316,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  382,  383,  384,  385,   -1,   -1,   -1,   -1,
   -1,  391,  392,  393,  394,  395,  396,  397,  398,  399,
  400,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,  274,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,
  };

#line 1080 "Repil/IR/IR.jay"

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
