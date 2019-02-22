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
//t    "metadata_arg : TYPE ':' META_SYMBOL",
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
//t    "instruction : TAIL CALL return_type function_pointer function_args attribute_group_refs",
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
#line 188 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 39:
#line 192 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 40:
#line 199 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 203 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 42:
#line 210 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 43:
#line 214 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 44:
#line 218 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 45:
#line 222 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 46:
#line 226 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 64:
#line 259 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 65:
#line 263 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 66:
#line 267 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 67:
#line 274 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 278 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 283 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 73:
#line 289 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 74:
#line 290 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 75:
#line 291 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 76:
#line 292 "Repil/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 77:
#line 296 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 78:
#line 300 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 79:
#line 304 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 80:
#line 308 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 312 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 316 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 323 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 327 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 335 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 87:
#line 342 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 88:
#line 346 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 89:
#line 350 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 90:
#line 354 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 358 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 362 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 366 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 370 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 95:
#line 374 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 96:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 98:
#line 386 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 99:
#line 393 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 397 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 401 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 102:
#line 405 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 103:
#line 406 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 104:
#line 413 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 417 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 424 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 428 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 108:
#line 432 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 109:
#line 436 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 111:
#line 444 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 448 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 113:
#line 449 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 114:
#line 450 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 115:
#line 451 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 116:
#line 452 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 117:
#line 453 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 118:
#line 454 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 119:
#line 455 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 125:
#line 473 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 126:
#line 474 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 127:
#line 475 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 128:
#line 476 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 129:
#line 477 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 130:
#line 478 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 131:
#line 479 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 132:
#line 480 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 133:
#line 481 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 134:
#line 482 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 135:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 136:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 137:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 138:
#line 489 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 139:
#line 490 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 140:
#line 491 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 141:
#line 492 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 142:
#line 493 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 143:
#line 494 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 144:
#line 495 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 145:
#line 496 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 146:
#line 497 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 147:
#line 498 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 148:
#line 499 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 149:
#line 500 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 150:
#line 501 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 152:
#line 506 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 153:
#line 507 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 154:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 527 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 531 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 160:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 162:
#line 543 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 163:
#line 544 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 164:
#line 545 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 165:
#line 546 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 166:
#line 547 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 167:
#line 548 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 168:
#line 549 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 169:
#line 550 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 170:
#line 551 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 171:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 565 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 174:
#line 576 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 587 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 594 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 605 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 609 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 616 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 620 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 185:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 646 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 650 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 657 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 661 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 191:
#line 665 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 669 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 194:
#line 677 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 195:
#line 678 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 196:
#line 685 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 689 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 696 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 199:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 200:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 201:
#line 708 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 202:
#line 712 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 203:
#line 716 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 204:
#line 720 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 206:
#line 725 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 207:
#line 729 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 209:
#line 737 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 741 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 215:
#line 758 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 762 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 218:
#line 775 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 779 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 786 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 804 "Repil/IR/IR.jay"
  { yyVal = AtomicConstraint.SequentiallyConsistent; }
  break;
case 227:
#line 811 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 815 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 819 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 823 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 231:
#line 830 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 834 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 838 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 842 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 846 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 236:
#line 850 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 237:
#line 854 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 858 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 239:
#line 862 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 240:
#line 866 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 241:
#line 870 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 242:
#line 874 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 243:
#line 878 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 244:
#line 882 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 245:
#line 886 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 246:
#line 890 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 247:
#line 894 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 898 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 902 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 906 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 910 "Repil/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 914 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 918 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 922 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 926 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 930 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 934 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 938 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 942 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 946 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 950 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 954 "Repil/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 958 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 264:
#line 962 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 265:
#line 966 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 266:
#line 970 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 267:
#line 974 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 978 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 982 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 986 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 990 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 994 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 998 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 1002 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1006 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1010 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 1014 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 1018 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1022 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1026 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 281:
#line 1030 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 282:
#line 1034 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1038 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1042 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1046 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1050 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 287:
#line 1054 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 288:
#line 1058 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 289:
#line 1062 "Repil/IR/IR.jay"
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
    9,    9,   17,   17,   17,   17,   17,   17,   17,   13,
   13,    8,    8,    8,    8,    8,   20,   20,   20,    7,
    7,   22,   22,   22,   22,   22,   22,   22,   22,   22,
   22,   22,   22,    3,    3,    3,   23,   23,   24,   24,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   11,
   11,   11,   25,   25,   26,   26,    4,    4,    4,    4,
    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,
    5,   27,   27,   32,   32,   33,   33,   33,   33,   34,
   34,   30,   30,   30,   30,   30,   30,   30,   30,   14,
   14,   28,   28,   35,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   36,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   40,   18,   18,   18,   18,   18,   18,   18,   18,   18,
   41,   21,   21,   42,   39,   39,   43,   44,   38,   38,
   29,   29,   45,   45,   45,   45,   46,   46,   48,   48,
   48,   48,   50,   51,   51,   52,   52,   53,   53,   53,
   53,   53,   53,   53,   54,   54,   54,   54,   54,   54,
   19,   19,   55,   55,   56,   56,   57,   58,   58,   59,
   60,   60,   61,   61,   31,   62,   47,   47,   47,   47,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    3,    3,    3,    6,    5,    2,
    3,    1,    2,    3,    3,    3,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    3,    1,
    1,    1,    4,    2,    3,    5,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    4,    2,    1,
    5,    5,    1,    3,    1,    1,    9,    9,   10,   10,
   11,    9,   10,   11,   11,   11,   13,   12,    5,    6,
    6,    3,    2,    1,    3,    1,    2,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
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
    5,    6,    5,    5,    5,    6,    4,    4,    5,    6,
    5,    6,    7,    5,    6,    7,    4,    5,    6,    5,
    2,    5,    4,    4,    4,    4,    5,    6,    7,    6,
    6,    4,    7,    8,    5,    6,    5,    5,    6,    3,
    4,    5,    7,    4,    5,    6,    6,    4,    5,    7,
    8,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   80,   73,   74,   75,   76,   72,    0,   29,   28,
    0,    0,    0,   71,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  120,  121,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   64,    0,
    0,    0,    0,    0,    0,   79,  225,  117,  118,  119,
  112,  113,  115,  114,  116,    0,    0,    0,    0,    0,
    0,    0,    0,    5,    6,    0,    0,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   65,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   86,
   77,    0,    0,   83,    0,    0,    0,  164,  165,  163,
  166,  167,  168,  162,  153,  152,  170,  169,    0,    0,
    0,    0,    0,    0,    0,    0,  151,    0,    0,    0,
    0,    0,    0,    0,   31,    0,    0,    0,   49,   48,
   13,    0,    0,   42,   47,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  108,  109,  103,    0,    0,  104,
  124,    0,    0,  122,   78,    0,    0,    0,    0,    0,
    0,   62,   54,   52,   53,   55,   56,   57,   58,    0,
   50,    0,    0,    0,    0,  175,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   43,   14,    0,  172,    0,   81,   66,   82,    0,    0,
    0,    0,    0,  110,    0,  102,    0,    0,    0,    0,
  123,   84,    0,    0,    0,    0,   12,   51,    0,    0,
    0,    0,  160,    0,  158,  159,    0,    0,    0,    0,
    0,    0,   35,    0,   33,   36,   37,   32,   17,   16,
   46,   45,   44,    0,    0,    0,    0,    0,    0,    0,
  111,  105,    0,    0,   40,    0,    0,   59,  214,  213,
    0,  211,    0,    0,    0,    0,  176,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  181,    0,    0,  187,    0,
    0,    0,    0,    0,    0,    0,   41,    0,   63,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   39,    0,    0,    0,    0,    0,  229,    0,    0,  227,
    0,  223,  224,    0,    0,  221,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  226,  251,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  125,  126,  127,  128,  129,  130,  131,  132,  133,
  134,    0,  135,  136,  147,  148,  149,  150,  138,  140,
  141,  142,  143,  139,  137,  145,  146,  144,    0,    0,
    0,    0,    0,    0,   92,  182,    0,  188,    0,    0,
    0,    0,    0,    0,    0,   87,    0,   88,  212,    0,
  157,  154,  156,    0,    0,    0,    0,   38,   90,    0,
    0,    0,  171,    0,    0,    0,    0,  222,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  215,    0,  193,    0,    0,
    0,    0,    0,    0,    0,   93,    0,    0,    0,    0,
   89,    0,    0,    0,   91,   95,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  247,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   94,   96,
    0,    0,  178,    0,  179,    0,    0,  231,    0,  248,
  282,    0,  257,  268,    0,  252,  285,  272,  250,  287,
  279,  275,    0,    0,  265,    0,  235,  234,  267,  288,
    0,    0,  233,    0,  161,  174,    0,    0,    0,    0,
    0,    0,    0,    0,  216,    0,    0,  195,    0,    0,
  196,    0,  239,    0,    0,    0,    0,    0,   98,    0,
  155,    0,    0,    0,    0,    0,  218,  232,  283,  269,
  276,  266,  236,  261,  277,    0,    0,    0,    0,    0,
    0,  260,  249,    0,    0,    0,    0,  198,    0,  194,
    0,  240,    0,    0,  245,    0,   97,  180,  228,  177,
    0,  230,  219,    0,  263,    0,  280,    0,  217,  273,
    0,  206,  200,    0,    0,    0,    0,  205,  201,  199,
  197,    0,  246,  220,  264,  281,  203,    0,    0,    0,
    0,    0,  204,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  210,  207,  209,    0,
    0,  208,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  190,  152,  144,   50,
  153,  548,  230,   51,   52,   36,  145,  137,  281,  154,
  633,  191,   61,   62,  113,  114,  109,  173,  345,  224,
   78,  169,  170,  225,  174,  442,  459,  634,  197,  666,
  380,  599,  695,  635,  346,  347,  348,  349,  350,  549,
  622,  680,  681,  749,  282,  545,  546,  696,  697,  385,
  386,  417,
  };
  protected static readonly short [] yySindex = {          622,
  -49, -217,  -19,   80,   88, 3754, 3829, -216,    0,  622,
    0,    0,    0,    0, -146,   94,  122,  202,  -93,  -16,
    0,    0,    0,    0,    0,    0,    0, 3981,    0,    0,
 3886, -115, -137,    0,  154, 3553,  -30, 3981,  -27,  131,
    0,    0,  -50,  -38,    0,    0,    0,    0,    0, 3981,
 -163,   62,   53,  -57,  160,  -28,  105,  -17,    0,  154,
  -18,  217,   -2, 3981,   19,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   -5, 3981, 3633,  279, 3599,
   -4,  279,  203,    0,    0, 2019, 3981, -163, 3981, -163,
    0,  246,    0, -124,  336,  265, 3834,  279,    0, 3981,
 3981,   25, 3981,  279,   -1,    3, 3981, 2104, -162,    0,
    0,  154,  141,    0,  279, -162,  440,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   37,  390,
  412,  421, 4003, 4003, 4003,  402,    0, 2019, 3981, 2019,
 3981,  424,  433,  274,    0, -124, 3859,    0,    0,    0,
    0,  -10, 2019,    0,    0,   62,  154,   82,  408,    5,
 -162,  279,  279,    9,    0,    0,    0,  -13,  337,    0,
    0,  164, -201,    0,    0, 3807,  164,  164,  164,  448,
  467,    0,    0,    0,    0,    0,    0,    0,    0, -109,
    0,  470, 4003, 4003, 4003,    0,   32,    8,   41,  149,
  471, 2019,  472, 1986, 2144,  240,    0, -124,  346,   15,
    0,    0, 3864,    0,  164,    0,    0,    0,  164, -108,
  164,   62,  279,    0,  903,    0, 3784,  -96,  243, -116,
    0,    0,  164,  164,  258,  376,    0,    0, 3981,  169,
  176,  179,    0, 4003,    0,    0,  263,  161,  499,  191,
  192,  511,    0,  516,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -58, -201, 4637, -103, -201,  164,   62,
    0,    0, 4637, -102,    0,  287, 4637,    0,    0,    0,
  383,    0,  173, 3981, 3981, 3981,    0,  288,  307,  205,
  311,  312,  208, 1892, 4637,  -66,  -62,  510, 4003, -160,
 4003,  -46, 3981,  -46, 3981,  -46, 3981, 3981, 3981, 3981,
 3981, 3981,  -46,   57,  522, 3981, 3981, 3981, 4003, 4003,
 4003, 3981, 3690, 3732,  255,  675, 4003, 4003, 4003, 4003,
 4003, 4003, 4003, 4003, 4003, 4003, 4003, 4003, 1003, 3914,
 3981, 3981, 3748,  171, 2143,    0, 4637,  288,    0,  288,
 4637,  -60,  -43,  164, 2223, 4637,    0, 2303,    0,  376,
 4003,  134,  231,  351,  302,  288,  320,  288,    0,  321,
    0,  399, 2400, 4637, 4637, 5033,    0,  314, 2092,    0,
  536,    0,    0, 2019,  -46,    0, 2019, 2019,  -46, 2019,
 2019,  -46, 2019, 2019, 2019, 2019, 2019, 2019, 2019,  -46,
 3981, 2019, 3981, 2019, 2019, 2019, 2019,  540,  541,  542,
  283, 3981,  290, 4003,  544,    0,    0, 3981,  338,  220,
  223,  225,  226,  238,  241,  248,  249,  266,  272,  273,
  276,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3981,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3981,   45,
 2019,  222, 3633, 3553,    0,    0,  288,    0,  302,  302,
 2480, 4637, 4637,  -48, -201,    0, 2560,    0,    0,  548,
    0,    0,    0,  302,  288,  302,  288,    0,    0, 2657,
 2737,  288,    0,  600,  381,  607, 2019,    0,  609,  615,
 2019,  626,  629, 2019,  634,  635,  636,  637,  638,  639,
  642, 2019, 2019,  645, 2019,  649,  650,  651,  652, 4003,
 4003, 4003,  286,  363, 3981,  655, 3981,  367, 4003, 3981,
 3981, 3981, 3981, 3981, 3981, 3981, 3981, 3981, 3981, 3981,
 3981, 2019, 2019, 2092,  659,    0,  665,    0,  616,  222,
 3981,  222, 3981, 3633,  302,    0, 2817, 2914, 4637,  -11,
    0, 4003,  302,  302,    0,    0,  302,  381,  619, 2092,
  669, 2092, 2092,  678, 2092, 2092,  679, 2092, 2092, 2092,
 2092, 2092, 2092, 2092,  680,  681, 2092,  682, 2092, 2092,
 2092, 2092,    0,  685,  686,  475, 3981, 2019,  689, 3981,
  690, 4003,  692,  154,  154,  154,  154,  154,  154,  154,
  154,  154,  154,  154,  154,  694,  696,  697,  653, 4003,
 2052,  164,  616,  222,  616,  222,  222, 3981,    0,    0,
 2994, 4637,    0,  404,    0,  698, 3981,    0, 2092,    0,
    0, 2092,    0,    0, 2092,    0,    0,    0,    0,    0,
    0,    0, 2092, 2092,    0, 2092,    0,    0,    0,    0,
 4003, 4003,    0,  699,    0,    0,  384,  706,  395,  715,
 4003, 2092, 2092, 2092,    0,  716, 3900,    0, 1827,  406,
    0,  164,    0,  616,  164,  616,  616,  222,    0, 3074,
    0, 4003,  381,   -9,  717, 3933,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  400,  513,  413,  515, 4003,
  732,    0,    0,  684, 4003,  738,  558,    0, 2038,    0,
 3959,    0,  164,  164,    0,  616,    0,    0,    0,    0,
  381,    0,    0,  523,    0,  524,    0,  732,    0,    0,
 1935,    0,    0,  416,  744,  747,  748,    0,    0,    0,
    0,  164,    0,    0,    0,    0,    0,  410,  749, 4003,
 4003, 4003,    0, 3981,  423,  426,  430,  446, 3981, 3981,
 3981, 4003,  374,  436,  461,  750,    0,    0,    0, 4003,
  415,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  797,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  344,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   56,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  759,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  150,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  759,    0,  759,
    0,    0,    0,    0,    0,    0,    0,  644,    0,    0,
    0,    0,  759,    0,    0,    0,   59,  759,    0,  759,
    0,    0,    0,    0,    0,    0,    0,  331,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  145,  313,
 1925,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  759,    0,  759,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  425,    0,    0,    0,    0,    0,
    0,    0,  153,  269,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  429,    0,
    0,    0,    0,  427,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  759,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3171,    0, 4717,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  759,  759,  759,  439,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  759,    0,    0,  759,  759,    0,  759,
  759,    0,  759,  759,  759,  759,  759,  759,  759,    0,
    0,  759,    0,  759,  759,  759,  759,    0,    0,    0,
  759,    0,  759,    0,    0,    0,    0,    0,  759,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  759,
  759,    0,    0,    0,    0,    0, 3251,    0, 3331, 4797,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  604,  612,  768,    0,    0,    0,    0,
    0, 4877,    0,    0,    0,    0,  759,    0,    0,    0,
  759,    0,    0,  759,    0,    0,    0,    0,    0,    0,
    0,  759,  759,    0,  759,    0,    0,    0,    0,    0,
    0,    0,    0,  759,    0,    0,    0,  759,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  759,  759,    0, 3997,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 3428,    0,    0,    0,    0,    0,
    0,    0,  858,  947,    0,    0, 4957,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  759,    0,    0,
    0,    0,    0,  733,  823,  912,  992, 1081, 1161, 1250,
 1330, 1419, 1499, 1588, 1668,    0,    0,    0,    0,    0,
    0, 4077,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  759,    0,
    0, 4157,    0,    0, 4237,    0,    0,    0,    0,    0,
    0,    0,    0,  759,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 4317,    0,    0,    0,    0,  445,  759,    0,    0,    0,
    0,    0, 4397,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4477,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 4557,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  759,    0,    0,
    0,    0,  759,  759,  759,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  791,  754,    0,    0,    0,    0,  662,  664,  144,
   -6,  270, 1954,  167,    0,  793,  610, -181, -292,    0,
   30,  632,  760,   12,    0,  656,    6, -110, -213,  -32,
 -334,    0,  598,  147, -142,    0,    0, -592,  371,    0,
 -484, -411,    0,  135, -317,    0,  481,  482,  457, -228,
 -222,    0,  113,    0,  476,    0,  216,    0,  142, -167,
  -15,    0,
  };
  protected static readonly short [] yyTable = {            35,
   35,  372,   92,   77,   96,  179,  277,   64,  463,   80,
  569,   15,   80,   32,  266,  237,   57,   37,   39,  351,
  356,   35,   80,  256,   60,  100,  273,  466,   66,   35,
  231,   35,   66,  213,   80,   80,  231,  466,   80,   58,
  466,   18,   80,   86,   33,  107,   66,   76,   80,   81,
  220,  244,   16,   17,  280,  466,  374,   60,  213,  355,
  375,  228,  472,  358,  295,   31,  233,  234,  100,  245,
   35,   35,  229,  112,  559,  244,   31,  231,  711,  473,
  138,  373,  140,  636,  244,  231,   66,  116,  105,  106,
  231,  231,   40,  157,  158,   69,  160,  218,   69,   67,
   35,  168,   68,  156,  264,  378,   99,  171,  265,  161,
  268,  632,  280,   42,  212,  601,   32,  738,  164,   65,
  177,  231,  231,   66,  379,  231,  155,   45,   46,  554,
   47,   48,  202,  246,  204,  544,  389,  471,  392,  260,
   19,   63,  477,  216,   99,  400,  171,   33,   20,  159,
  180,  142,  100,  466,   43,  143,  243,  276,  353,  466,
  490,  491,  196,  196,  196,  229,  181,  221,  222,  112,
  276,  276,  466,  466,  481,   66,  155,  229,  280,   31,
   67,  175,   44,   68,  176,  664,   54,  781,  668,   69,
   85,   83,  271,   85,   87,   66,   89,  182,  183,   94,
  171,  184,  185,  186,  187,  188,  189,  276,  729,   84,
  231,  276,  171,  276,   66,  229,  361,   21,   88,   90,
  168,   85,  240,  241,  242,  276,   22,   97,  270,   91,
  229,  139,  283,  141,   23,   24,   25,   26,   27,  466,
  466,   79,  263,  475,   82,   95,  754,  118,  119,  120,
  171,  121,  122,  123,   98,  124,   80,   55,  557,  558,
   56,   80,  276,  211,  101,  171,  104,  115,  101,  127,
  162,  482,   66,  287,  163,  172,  128,  362,  363,  364,
  223,  134,  178,  382,  383,  103,   68,   69,  211,   70,
   71,   72,   73,   74,   75,  384,  387,  388,  390,  391,
  393,  394,  395,  396,  397,  398,  399,  402,  404,  405,
  406,  407,  135,  466,  207,  411,  413,  208,  108,  419,
   21,  623,  215,  625,   66,  117,  523,  219,  377,   22,
  381,   66,  231,  525,  460,  461,   35,   23,   24,   25,
   26,   27,   45,   46,  133,  631,   47,   48,  408,  409,
  410,   45,   46,  415,  462,  136,  420,  421,  422,  423,
  424,  425,  426,  427,  428,  429,  430,  431,   64,  498,
   69,  106,  466,  498,  106,  146,  498,  226,  497,   66,
  227,  529,  501,   69,  498,  504,  259,  147,  269,  208,
  480,  483,   66,  512,  513,  684,  515,  686,  687,  192,
  683,  401,  685,   69,   66,  524,  597,  201,   66,  203,
  602,  528,   99,   99,  777,   66,   99,   99,  690,   99,
  100,  100,  214,  359,  100,  100,  360,  100,   21,  193,
  551,  553,   99,   99,   69,  542,  354,   60,   18,  488,
  100,  100,  360,  526,  691,  200,  720,  692,  758,  721,
  763,  194,  543,  360,   99,  782,   35,   35,  692,  726,
  195,  722,  100,  724,  725,  107,   69,   34,  107,  217,
   34,  249,  171,  252,  550,  552,  778,   66,  118,  119,
  120,  205,  121,  122,  123,  202,  124,   66,  202,  772,
  206,   45,   46,  125,  126,   47,   48,   49,   29,   30,
  127,  779,   66,  753,  198,  199,  236,  128,  235,  239,
  247,  682,  730,  257,  248,  250,  275,  278,  598,  288,
  598,  628,  289,  604,  605,  606,  607,  608,  609,  610,
  611,  612,  613,  614,  615,  748,  101,  101,  284,  231,
  101,  101,  290,  101,   35,  285,   35,   35,  286,  593,
  594,  595,  291,  292,  293,  294,  101,  101,  603,  280,
  357,  229,  624,  366,  626,  627,  367,  368,  369,  370,
  376,  416,   60,  464,  723,  276,  485,  487,  101,  495,
  231,   32,  129,  520,  521,  522,  493,  527,   60,  530,
  598,  562,  531,  598,  532,  533,  130,  131,  132,   66,
   69,   69,   69,   19,   69,   69,   69,  534,   69,  231,
  535,   20,   33,  752,  679,   69,   69,  536,  537,   60,
   60,   35,   69,   60,   60,   60,   60,   60,   60,   69,
  694,  670,  118,  119,  120,  538,  121,  122,  123,  688,
  124,  539,  540,  568,   31,  541,  378,  596,  494,  676,
  570,  279,  572,  496,  127,  621,  499,  500,  573,  502,
  503,  128,  505,  506,  507,  508,  509,  510,  511,  575,
  717,  514,  576,  516,  517,  518,  519,  578,  579,  580,
  581,  582,  583,   70,  173,  584,  271,  173,  587,  694,
  704,  705,  589,  590,  591,  592,   21,   21,  600,  180,
   21,   21,  619,   21,   69,  173,   18,   18,  620,  637,
   18,   18,  639,   18,  679,  181,   21,   21,   69,   69,
   69,  642,  645,  653,  654,  656,   18,   18,  661,  662,
  547,  663,  667,  669,   32,  671,  173,  672,   21,  673,
  674,  693,  706,  544,  740,  707,  182,  183,   18,  708,
  184,  185,  186,  187,  188,  189,  709,  768,  710,  715,
  731,  734,  773,  774,  775,   33,  571,   25,  173,  735,
  574,  737,   69,  577,  736,  692,  739,  741,  759,  755,
  756,  585,  586,  760,  588,   21,  761,  762,  764,  765,
  766,  767,  769,  780,   22,  770,    1,   31,   69,  771,
   41,  776,   23,   24,   25,   26,   27,   93,  210,  209,
   53,  616,  617,  618,  118,  119,  120,  258,  121,  122,
  123,  238,  124,  102,  272,  719,  728,  467,  468,  742,
  743,  232,  492,  751,  675,  479,  127,  733,    0,  638,
    0,  640,  641,  128,  643,  644,    0,  646,  647,  648,
  649,  650,  651,  652,    0,    0,  655,   22,  657,  658,
  659,  660,   69,    0,    0,    0,  403,  665,    0,    0,
    0,   19,   19,    0,    0,   19,   19,    0,   19,   20,
   20,    0,    0,   20,   20,    0,   20,    0,    0,    1,
    2,   19,   19,    3,    4,    0,    5,    0,    0,   20,
   20,    0,    0,    0,    0,    0,    0,    0,  698,    6,
    7,  699,    0,   19,  700,    0,  173,  173,  744,    0,
    0,   20,  701,  702,    0,  703,    0,    0,    0,    0,
    0,    8,  745,  746,  747,    0,    0,    0,   21,    0,
    0,  712,  713,  714,    0,    0,   24,   22,  718,    0,
    0,   69,    0,    0,    0,   23,   24,   25,   26,   27,
    0,  173,  173,  173,    0,    0,    0,    0,    0,    0,
    0,    0,  173,    0,    0,  173,  173,  173,  173,  173,
  173,  173,  173,  173,  173,    0,  173,  173,  750,  173,
  173,  173,  173,  173,  173,  173,    0,    0,  173,  173,
  173,  173,    0,    0,  173,  284,  284,    0,  173,  173,
  173,  173,  173,  173,  173,  173,  173,  173,  173,  173,
  173,    0,  173,    0,    0,    0,    0,    0,    0,    0,
    0,   69,    0,  173,    0,   25,   25,  418,    0,   25,
   25,    0,   25,    0,  173,  173,  173,  173,    0,    0,
  284,  284,  284,    0,    0,   25,   25,    0,    0,    0,
    0,  284,    0,    0,  284,  284,  284,  284,  284,  284,
  284,  284,  284,  284,    0,  284,  284,   25,  284,  284,
  284,  284,  284,  284,  284,    0,    0,  284,  284,  284,
  284,    0,    0,  284,    0,  289,  289,  284,  284,  284,
  284,  284,    0,  284,  284,  284,  284,  284,  284,  284,
    0,  284,    0,    0,    0,    0,    0,    0,    0,    0,
   69,    0,  284,    0,    0,   22,   22,    0,    0,   22,
   22,    0,   22,  284,  284,  284,  284,    0,    0,    0,
  289,  289,  289,    0,    0,   22,   22,    0,    0,    0,
    0,  289,    0,    0,  289,  289,  289,  289,  289,  289,
  289,  289,  289,  289,    0,  289,  289,   22,  289,  289,
  289,  289,  289,  289,  289,    0,    0,  289,  289,  289,
  289,    0,    0,  289,  274,  274,    0,  289,  289,  289,
  289,  289,    0,  289,  289,  289,  289,  289,  289,  289,
   69,  289,   68,   69,    0,   70,   71,   72,   73,   74,
   75,    0,  289,    0,   24,   24,    0,    0,   24,   24,
    0,   24,    0,  289,  289,  289,  289,    0,    0,  274,
  274,  274,    0,    0,   24,   24,    0,    0,    0,    0,
  274,    0,    0,  274,  274,  274,  274,  274,  274,  274,
  274,  274,  274,    0,  274,  274,   24,  274,  274,  274,
  274,  274,  274,  274,  256,  256,  274,  274,  274,  274,
    0,    0,  274,    0,    0,    0,  274,  274,  274,  274,
  274,    0,  274,  274,  274,  274,  274,  274,  274,   69,
  274,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  274,    0,    0,    0,    0,    0,    0,    0,  256,
  256,  256,  274,  274,  274,  274,    0,    0,    0,    0,
  256,    0,    0,  256,  256,  256,  256,  256,  256,  256,
  256,  256,  256,    0,  256,  256,    0,  256,  256,  256,
  256,  256,  256,  256,    0,    0,  256,  256,  256,  256,
    0,    0,  256,  253,  253,    0,  256,  256,  256,  256,
  256,    0,  256,  256,  256,  256,  256,  256,  256,   69,
  256,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  256,  432,  433,  434,  435,  436,  437,  438,  439,
  440,  441,  256,  256,  256,  256,    0,    0,  253,  253,
  253,    0,    0,    0,    0,    0,    0,    0,    0,  253,
    0,    0,  253,  253,  253,  253,  253,  253,  253,  253,
  253,  253,    0,  253,  253,    0,  253,  253,  253,  253,
  253,  253,  253,  254,  254,  253,  253,  253,  253,    0,
    0,  253,    0,    0,    0,  253,  253,  253,  253,  253,
    0,  253,  253,  253,  253,  253,  253,  253,   69,  253,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  253,    0,    0,    0,    0,    0,    0,    0,  254,  254,
  254,  253,  253,  253,  253,    0,    0,    0,    0,  254,
    0,    0,  254,  254,  254,  254,  254,  254,  254,  254,
  254,  254,    0,  254,  254,    0,  254,  254,  254,  254,
  254,  254,  254,    0,    0,  254,  254,  254,  254,    0,
    0,  254,  255,  255,    0,  254,  254,  254,  254,  254,
    0,  254,  254,  254,  254,  254,  254,  254,   69,  254,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  254,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  254,  254,  254,  254,    0,    0,  255,  255,  255,
    0,    0,    0,    0,    0,    0,    0,    0,  255,    0,
    0,  255,  255,  255,  255,  255,  255,  255,  255,  255,
  255,    0,  255,  255,    0,  255,  255,  255,  255,  255,
  255,  255,  286,  286,  255,  255,  255,  255,    0,    0,
  255,    0,    0,    0,  255,  255,  255,  255,  255,    0,
  255,  255,  255,  255,  255,  255,  255,   69,  255,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  255,
    0,    0,    0,    0,    0,    0,    0,  286,  286,  286,
  255,  255,  255,  255,    0,    0,    0,    0,  286,    0,
    0,  286,  286,  286,  286,  286,  286,  286,  286,  286,
  286,    0,  286,  286,    0,  286,  286,  286,  286,  286,
  286,  286,    0,    0,  286,  286,  286,  286,    0,    0,
  286,  278,  278,    0,  286,  286,  286,  286,  286,    0,
  286,  286,  286,  286,  286,  286,  286,   69,  286,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  286,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  286,  286,  286,  286,    0,    0,  278,  278,  278,    0,
    0,    0,    0,    0,    0,    0,    0,  278,    0,    0,
  278,  278,  278,  278,  278,  278,  278,  278,  278,  278,
    0,  278,  278,    0,  278,  278,  278,  278,  278,  278,
  278,  271,  271,  278,  278,  278,  278,    0,    0,  278,
    0,    0,    0,  278,  278,  278,  278,  278,    0,  278,
  278,  278,  278,  278,  278,  278,    0,  278,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  278,    0,
    0,    0,    0,    0,    0,    0,  271,  271,  271,  278,
  278,  278,  278,    0,    0,    0,    0,  271,    0,    0,
  271,  271,  271,  271,  271,  271,  271,  271,  271,  271,
    0,  271,  271,    0,  271,  271,  271,  271,  271,  271,
  271,    0,    0,  271,  271,  271,  271,    0,    0,  271,
  262,  262,    0,  271,  271,  271,  271,  271,   66,  271,
  271,  271,  271,  271,  271,  271,    0,  271,    0,    0,
    0,    0,    0,    0,    0,    0,  134,    0,  271,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  271,
  271,  271,  271,    0,    0,  262,  262,  262,    0,    0,
    0,    0,    0,    0,    0,    0,  262,  135,    0,  262,
  262,  262,  262,  262,  262,  262,  262,  262,  262,    0,
  262,  262,  371,  262,  262,  262,  262,  262,  262,  262,
  237,  237,  262,  262,  262,  262,    0,    0,  262,  133,
    0,    0,  262,  262,  262,  262,  262,    0,  262,  262,
  262,  262,  262,  262,  262,    0,  262,    0,    0,    0,
    0,    0,    0,    0,    0,  757,    0,  262,    0,    0,
    0,    0,    0,    0,    0,  237,  237,  237,  262,  262,
  262,  262,    0,    0,    0,    0,  237,    0,    0,  237,
  237,  237,  237,  237,  237,  237,  237,  237,  237,    0,
  237,  237,    0,  237,  237,  237,  237,  237,  237,  237,
    0,    0,  237,  237,  237,  237,    0,   66,  237,  251,
    0,    0,  237,  237,  237,  237,  237,    0,  237,  237,
  237,  237,  237,  237,  237,  134,  237,    0,    0,   61,
    0,    0,    0,    0,    0,    0,    0,  237,    0,    0,
   66,    0,    0,    0,    0,    0,    0,    0,  237,  237,
  237,  237,    0,    0,    0,    0,  135,    0,  134,    0,
    0,    0,    0,  118,  119,  120,    0,  121,  122,  123,
    0,  124,  678,    0,    0,    0,    0,  134,  125,  126,
    0,    0,    0,    0,    0,  127,    0,    0,  133,  135,
    0,   32,  128,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   68,   69,  135,   70,
   71,   72,   73,   74,   75,    0,    0,    0,    0,    0,
    0,  133,   33,    0,  167,    0,    0,    0,  118,  119,
  120,  134,  121,  122,  123,    0,  124,    0,    0,    0,
  133,    0,    0,   32,    0,    0,    0,  279,    0,    0,
  127,    0,    0,  267,   31,    0,    0,  128,    0,    0,
    0,  274,  135,    0,   61,    0,    0,  129,    0,    0,
    0,  118,  119,  120,   33,  121,  122,  123,    0,  124,
   61,  130,  131,  132,    0,    0,    0,    0,    0,    0,
  279,    0,    0,  127,  133,    0,    0,  296,  297,    0,
  128,  352,    0,    0,    0,    0,   31,    0,    0,    0,
    0,   61,   61,    0,    0,   61,   61,   61,   61,   61,
   61,  365,  118,  119,  120,    0,  121,  122,  123,    0,
  124,    0,    0,    0,    0,    0,    0,  125,  126,    0,
    0,    0,    0,    0,  127,    0,    0,  465,    0,    0,
    0,  128,    0,    0,    0,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    0,    0,    0,    0,    0,    0,
  125,  126,    0,    0,  118,  119,  120,  127,  121,  122,
  123,  469,  124,  470,  128,    0,  474,    0,    0,  125,
  126,    0,    0,    0,    0,   21,  127,    0,    0,  484,
    0,  486,    0,  128,   22,    0,    0,    0,    0,  677,
    0,    0,   23,   24,   25,   26,   27,   68,   69,    0,
   70,   71,   72,   73,   74,   75,  129,  476,  118,  119,
  120,    0,  121,  122,  123,    0,  124,    0,    0,    0,
  130,  131,  132,  125,  126,    0,    0,   21,    0,    0,
  127,    0,    0,    0,    0,    0,   22,  128,    0,  129,
    0,  165,    0,    0,   23,   24,   25,   26,   27,    0,
    0,    0,    0,  130,  131,  132,  166,    0,  129,    0,
  118,  119,  120,  253,  121,  122,  123,    0,  124,    0,
    0,    0,  130,  131,  132,  298,    0,  254,    0,  255,
  555,    0,  127,    0,    0,    0,    0,  478,  560,  128,
    0,    0,    0,    0,    0,    0,    0,    0,  563,    0,
  564,    0,    0,    0,    0,  567,    0,    0,    0,    0,
    0,    0,  129,    0,    0,    0,    0,    0,    0,    0,
  299,  300,  301,    0,    0,    0,  130,  131,  132,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,  298,    0,  322,  323,  324,
  325,    0,    0,  326,    0,    0,    0,  327,  328,  329,
  330,  331,    0,  332,  333,  334,  335,  336,  337,  338,
    0,  339,    0,    0,  489,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
  299,  300,  301,  341,  342,  343,  344,    0,    0,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,  298,    0,  322,  323,  324,
  325,    0,    0,  326,    0,    0,    0,  327,  328,  329,
  330,  331,    0,  332,  333,  334,  335,  336,  337,  338,
    0,  339,    0,    0,  556,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
  299,  300,  301,  341,  342,  343,  344,    0,    0,    0,
    0,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,    0,    0,  322,  323,  324,
  325,    0,    0,  326,    0,    0,    0,  327,  328,  329,
  330,  331,  298,  332,  333,  334,  335,  336,  337,  338,
    0,  339,    0,    0,  561,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  341,  342,  343,  344,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  299,  300,  301,
    0,    0,    0,    0,    0,    0,    0,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
  312,    0,  313,  314,    0,  315,  316,  317,  318,  319,
  320,  321,  298,    0,  322,  323,  324,  325,    0,    0,
  326,    0,    0,    0,  327,  328,  329,  330,  331,    0,
  332,  333,  334,  335,  336,  337,  338,    0,  339,    0,
    0,  565,    0,    0,    0,    0,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,  299,  300,  301,
  341,  342,  343,  344,    0,    0,    0,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
  312,    0,  313,  314,    0,  315,  316,  317,  318,  319,
  320,  321,  298,    0,  322,  323,  324,  325,    0,    0,
  326,    0,    0,    0,  327,  328,  329,  330,  331,    0,
  332,  333,  334,  335,  336,  337,  338,    0,  339,    0,
    0,  566,    0,    0,    0,    0,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,  299,  300,  301,
  341,  342,  343,  344,    0,    0,    0,    0,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  309,  310,  311,
  312,    0,  313,  314,    0,  315,  316,  317,  318,  319,
  320,  321,    0,    0,  322,  323,  324,  325,    0,    0,
  326,    0,    0,    0,  327,  328,  329,  330,  331,  298,
  332,  333,  334,  335,  336,  337,  338,    0,  339,    0,
    0,  629,    0,    0,    0,    0,    0,    0,    0,  340,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  341,  342,  343,  344,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  299,  300,  301,    0,    0,    0,
    0,    0,    0,    0,    0,  302,    0,    0,  303,  304,
  305,  306,  307,  308,  309,  310,  311,  312,    0,  313,
  314,    0,  315,  316,  317,  318,  319,  320,  321,  298,
    0,  322,  323,  324,  325,    0,    0,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,  333,  334,
  335,  336,  337,  338,    0,  339,    0,    0,  630,    0,
    0,    0,    0,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,  299,  300,  301,  341,  342,  343,
  344,    0,    0,    0,    0,  302,    0,    0,  303,  304,
  305,  306,  307,  308,  309,  310,  311,  312,    0,  313,
  314,    0,  315,  316,  317,  318,  319,  320,  321,  298,
    0,  322,  323,  324,  325,    0,    0,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,  333,  334,
  335,  336,  337,  338,    0,  339,    0,    0,  689,    0,
    0,    0,    0,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,  299,  300,  301,  341,  342,  343,
  344,    0,    0,    0,    0,  302,    0,    0,  303,  304,
  305,  306,  307,  308,  309,  310,  311,  312,    0,  313,
  314,    0,  315,  316,  317,  318,  319,  320,  321,    0,
    0,  322,  323,  324,  325,    0,    0,  326,    0,    0,
    0,  327,  328,  329,  330,  331,  298,  332,  333,  334,
  335,  336,  337,  338,    0,  339,    0,    0,  727,    0,
    0,    0,    0,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  341,  342,  343,
  344,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  299,  300,  301,    0,    0,    0,    0,    0,    0,
    0,    0,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  309,  310,  311,  312,    0,  313,  314,    0,  315,
  316,  317,  318,  319,  320,  321,  298,    0,  322,  323,
  324,  325,    0,    0,  326,    0,    0,    0,  327,  328,
  329,  330,  331,    0,  332,  333,  334,  335,  336,  337,
  338,    0,  339,    0,    0,  185,    0,    0,    0,    0,
    0,    0,    0,  340,    0,    0,    0,    0,    0,    0,
    0,  299,  300,  301,  341,  342,  343,  344,    0,    0,
    0,    0,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  309,  310,  311,  312,    0,  313,  314,    0,  315,
  316,  317,  318,  319,  320,  321,  298,    0,  322,  323,
  324,  325,    0,    0,  326,    0,    0,    0,  327,  328,
  329,  330,  331,    0,  332,  333,  334,  335,  336,  337,
  338,    0,  339,    0,    0,  183,    0,    0,    0,    0,
    0,    0,    0,  340,    0,    0,    0,    0,    0,    0,
    0,  299,  300,  301,  341,  342,  343,  344,    0,    0,
    0,    0,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  309,  310,  311,  312,    0,  313,  314,    0,  315,
  316,  317,  318,  319,  320,  321,    0,    0,  322,  323,
  324,  325,    0,    0,  326,    0,    0,    0,  327,  328,
  329,  330,  331,  185,  332,  333,  334,  335,  336,  337,
  338,    0,  339,    0,    0,  186,    0,    0,    0,    0,
    0,    0,    0,  340,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  341,  342,  343,  344,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  185,  185,
  185,    0,    0,    0,    0,    0,    0,    0,    0,  185,
    0,    0,  185,  185,  185,  185,  185,  185,  185,  185,
  185,  185,    0,  185,  185,    0,  185,  185,  185,  185,
  185,  185,  185,  183,    0,  185,  185,  185,  185,    0,
    0,  185,    0,    0,    0,  185,  185,  185,  185,  185,
    0,  185,  185,  185,  185,  185,  185,  185,    0,  185,
    0,    0,  184,    0,    0,    0,    0,    0,    0,    0,
  185,    0,    0,    0,    0,    0,    0,    0,  183,  183,
  183,  185,  185,  185,  185,    0,    0,    0,    0,  183,
    0,    0,  183,  183,  183,  183,  183,  183,  183,  183,
  183,  183,    0,  183,  183,    0,  183,  183,  183,  183,
  183,  183,  183,  186,    0,  183,  183,  183,  183,    0,
    0,  183,   32,    0,    0,  183,  183,  183,  183,  183,
    0,  183,  183,  183,  183,  183,  183,  183,    0,  183,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  111,
  183,    0,    0,   33,    0,    0,    0,    0,  186,  186,
  186,  183,  183,  183,  183,    0,    0,    0,   32,  186,
    0,    0,  186,  186,  186,  186,  186,  186,  186,  186,
  186,  186,    0,  186,  186,   31,  186,  186,  186,  186,
  186,  186,  186,    0,    0,  186,  186,  186,  186,   33,
    0,  186,   32,    0,    0,  186,  186,  186,  186,  186,
  184,  186,  186,  186,  186,  186,  186,  186,    0,  186,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  186,   31,    0,   33,    0,    0,    0,    0,    0,    0,
    0,  186,  186,  186,  186,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  184,  184,  184,    0,   32,
    0,    0,    0,    0,    0,   31,  184,    0,    0,  184,
  184,  184,  184,  184,  184,  184,  184,  184,  184,    0,
  184,  184,    0,  184,  184,  184,  184,  184,  184,  184,
   33,    0,  184,  184,  184,  184,    0,    0,  184,    0,
    0,   32,  184,  184,  184,  184,  184,    0,  184,  184,
  184,  184,  184,  184,  184,    0,  184,   32,    0,    0,
    0,    0,   31,   32,    0,    0,   21,  184,    0,    0,
    0,    0,   33,    0,    0,   22,    0,    0,  184,  184,
  184,  184,    0,   23,   24,   25,   26,   27,   33,    0,
    0,    0,    0,   32,   33,    0,    0,    0,    0,    0,
    0,   67,   68,   69,   31,   70,   71,   72,   73,   74,
   75,    0,   21,    0,    0,    0,   32,    0,    0,    0,
   31,   22,    0,    0,   33,    0,   31,    0,    0,   23,
   24,   25,   26,   27,    0,    0,    0,    0,   32,    0,
    0,  110,    0,   32,    0,    0,   21,   33,    0,    0,
    0,    0,    0,    0,    0,   22,   31,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,   32,   33,
    0,    0,    0,   32,   33,    0,    0,    0,    0,   31,
    0,    0,   68,   69,    0,   70,   71,   72,   73,   74,
   75,    0,    0,    0,    0,   32,    0,    0,    0,   33,
    0,   31,    0,   21,   33,    0,   31,    0,  151,   32,
    0,    0,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,    0,   33,    0,    0,    0,
    0,   31,    0,    0,    0,    0,   31,    0,    0,    0,
   33,  412,   32,    0,    0,  148,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,   31,    0,
   59,   21,   23,   24,   25,   26,   27,   21,   32,    0,
   22,    0,   31,   33,    0,  732,   22,    0,   23,   24,
   25,   26,   27,  414,   23,   24,   25,   26,   27,    0,
   32,    0,    0,    0,    0,   28,   67,   21,    0,   33,
   29,   30,    0,    0,    0,   31,   22,    0,    0,    0,
    0,  165,   32,    0,   23,   24,   25,   26,   27,    0,
   21,   33,    0,    0,    0,    0,  166,    0,    0,   22,
    0,   31,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   21,   33,    0,    0,    0,  148,  149,  110,
    0,   22,    0,   31,    0,    0,   22,  150,    0,   23,
   24,   25,   26,   27,   23,   24,   25,   26,   27,    0,
   38,    0,  148,  149,    0,   31,    0,  148,  261,    0,
    0,   22,  150,    0,    0,    0,   22,  262,    0,   23,
   24,   25,   26,   27,   23,   24,   25,   26,   27,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,   21,    0,    0,   23,   24,   25,   26,
   27,    0,   22,  716,  443,  444,    0,    0,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   21,    0,    0,    0,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,    0,    0,
    0,    0,   21,    0,    0,    0,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,  677,    0,    0,   23,
   24,   25,   26,   27,   21,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,  148,    0,    0,  270,
  270,    0,    0,    0,    0,   22,    0,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,    0,    0,
    0,    0,    0,    0,    0,  445,  446,  447,  448,    0,
    0,    0,    0,    0,  449,  450,  451,  452,  453,  454,
  455,  456,  457,  458,  270,  270,  270,    0,    0,    0,
    0,    0,    0,    0,    0,  270,    0,    0,  270,  270,
  270,  270,  270,  270,  270,  270,  270,  270,    0,  270,
  270,    0,  270,  270,  270,  270,  270,  270,  270,  238,
  238,  270,  270,  270,  270,    0,    0,  270,    0,    0,
    0,  270,  270,  270,  270,  270,    0,  270,  270,  270,
  270,  270,  270,  270,    0,  270,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  270,    0,    0,    0,
    0,    0,    0,    0,  238,  238,  238,  270,  270,  270,
  270,    0,    0,    0,    0,  238,    0,    0,  238,  238,
  238,  238,  238,  238,  238,  238,  238,  238,    0,  238,
  238,    0,  238,  238,  238,  238,  238,  238,  238,  241,
  241,  238,  238,  238,  238,    0,    0,  238,    0,    0,
    0,  238,  238,  238,  238,  238,    0,  238,  238,  238,
  238,  238,  238,  238,    0,  238,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  238,    0,    0,    0,
    0,    0,    0,    0,  241,  241,  241,  238,  238,  238,
  238,    0,    0,    0,    0,  241,    0,    0,  241,  241,
  241,  241,  241,  241,  241,  241,  241,  241,    0,  241,
  241,    0,  241,  241,  241,  241,  241,  241,  241,  244,
  244,  241,  241,  241,  241,    0,    0,  241,    0,    0,
    0,  241,  241,  241,  241,  241,    0,  241,  241,  241,
  241,  241,  241,  241,    0,  241,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  241,    0,    0,    0,
    0,    0,    0,    0,  244,  244,  244,  241,  241,  241,
  241,    0,    0,    0,    0,  244,    0,    0,  244,  244,
  244,  244,  244,  244,  244,  244,  244,  244,    0,  244,
  244,    0,  244,  244,  244,  244,  244,  244,  244,  258,
  258,  244,  244,  244,  244,    0,    0,  244,    0,    0,
    0,  244,  244,  244,  244,  244,    0,  244,  244,  244,
  244,  244,  244,  244,    0,  244,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  244,    0,    0,    0,
    0,    0,    0,    0,  258,  258,  258,  244,  244,  244,
  244,    0,    0,    0,    0,  258,    0,    0,  258,  258,
  258,  258,  258,  258,  258,  258,  258,  258,    0,  258,
  258,    0,  258,  258,  258,  258,  258,  258,  258,  242,
  242,  258,  258,  258,  258,    0,    0,  258,    0,    0,
    0,  258,  258,  258,  258,  258,    0,  258,  258,  258,
  258,  258,  258,  258,    0,  258,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  258,    0,    0,    0,
    0,    0,    0,    0,  242,  242,  242,  258,  258,  258,
  258,    0,    0,    0,    0,  242,    0,    0,  242,  242,
  242,  242,  242,  242,  242,  242,  242,  242,    0,  242,
  242,    0,  242,  242,  242,  242,  242,  242,  242,  259,
  259,  242,  242,  242,  242,    0,    0,  242,    0,    0,
    0,  242,  242,  242,  242,  242,    0,  242,  242,  242,
  242,  242,  242,  242,    0,  242,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  242,    0,    0,    0,
    0,    0,    0,    0,  259,  259,  259,  242,  242,  242,
  242,    0,    0,    0,    0,  259,    0,    0,  259,  259,
  259,  259,  259,  259,  259,  259,  259,  259,    0,  259,
  259,    0,  259,  259,  259,  259,  259,  259,  259,  243,
  243,  259,  259,  259,  259,    0,    0,  259,    0,    0,
    0,  259,  259,  259,  259,  259,    0,  259,  259,  259,
  259,  259,  259,  259,    0,  259,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  259,    0,    0,    0,
    0,    0,    0,    0,  243,  243,  243,  259,  259,  259,
  259,    0,    0,    0,    0,  243,    0,    0,  243,  243,
  243,  243,  243,  243,  243,  243,  243,  243,    0,  243,
  243,    0,  243,  243,  243,  243,  243,  243,  243,  298,
    0,  243,  243,  243,  243,    0,    0,  243,    0,    0,
    0,  243,  243,  243,  243,  243,    0,  243,  243,  243,
  243,  243,  243,  243,    0,  243,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  243,    0,    0,    0,
    0,    0,    0,    0,  299,  300,  301,  243,  243,  243,
  243,    0,    0,    0,    0,  302,    0,    0,  303,  304,
  305,  306,  307,  308,  309,  310,  311,  312,    0,  313,
  314,    0,  315,  316,  317,  318,  319,  320,  321,  189,
    0,  322,  323,  324,  325,    0,    0,  326,    0,    0,
    0,  327,  328,  329,  330,  331,    0,  332,  333,  334,
  335,  336,  337,  338,    0,  339,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  340,    0,    0,    0,
    0,    0,    0,    0,  189,  189,  189,  341,  342,  343,
  344,    0,    0,    0,    0,  189,    0,    0,  189,  189,
  189,  189,  189,  189,  189,  189,  189,  189,    0,  189,
  189,    0,  189,  189,  189,  189,  189,  189,  189,  190,
    0,  189,  189,  189,  189,    0,    0,  189,    0,    0,
    0,  189,  189,  189,  189,  189,    0,  189,  189,  189,
  189,  189,  189,  189,    0,  189,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  189,    0,    0,    0,
    0,    0,    0,    0,  190,  190,  190,  189,  189,  189,
  189,    0,    0,    0,    0,  190,    0,    0,  190,  190,
  190,  190,  190,  190,  190,  190,  190,  190,    0,  190,
  190,    0,  190,  190,  190,  190,  190,  190,  190,  191,
    0,  190,  190,  190,  190,    0,    0,  190,    0,    0,
    0,  190,  190,  190,  190,  190,    0,  190,  190,  190,
  190,  190,  190,  190,    0,  190,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  190,    0,    0,    0,
    0,    0,    0,    0,  191,  191,  191,  190,  190,  190,
  190,    0,    0,    0,    0,  191,    0,    0,  191,  191,
  191,  191,  191,  191,  191,  191,  191,  191,    0,  191,
  191,    0,  191,  191,  191,  191,  191,  191,  191,  192,
    0,  191,  191,  191,  191,    0,    0,  191,    0,    0,
    0,  191,  191,  191,  191,  191,    0,  191,  191,  191,
  191,  191,  191,  191,    0,  191,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  191,    0,    0,    0,
    0,    0,    0,    0,  192,  192,  192,  191,  191,  191,
  191,    0,    0,    0,    0,  192,    0,    0,  192,  192,
  192,  192,  192,  192,  192,  192,  192,  192,    0,  192,
  192,    0,  192,  192,  192,  192,  192,  192,  192,    0,
    0,  192,  192,  192,  192,    0,    0,  192,    0,    0,
    0,  192,  192,  192,  192,  192,    0,  192,  192,  192,
  192,  192,  192,  192,    0,  192,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  192,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  192,  192,  192,
  192,  302,    0,    0,  303,  304,  305,  306,  307,  308,
  309,  310,  311,  312,    0,  313,  314,    0,  315,  316,
  317,  318,  319,  320,  321,    0,    0,  322,  323,  324,
  325,    0,    0,  326,    0,    0,    0,  327,  328,  329,
  330,  331,    0,  332,  333,  334,  335,  336,  337,  338,
    0,  339,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  340,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  341,  342,  343,  344,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  294,   60,   36,   33,  116,  123,  123,  343,   40,
  495,   61,   40,   60,  123,  125,   33,    6,    7,  123,
  123,   28,   40,  205,   31,   44,  123,  345,   42,   36,
  173,   38,   42,   44,   40,   40,  179,  355,   40,   28,
  358,   61,   40,   50,   91,   78,   42,   36,   40,   38,
  161,   44,  270,  271,  236,  373,  123,   64,   44,  273,
  123,  172,  123,  277,  123,  123,  177,  178,   44,   62,
   77,   78,  274,   80,  123,   44,  123,  220,  671,  123,
   87,  295,   89,  568,   44,  228,   42,   82,   77,   78,
  233,  234,  309,  100,  101,   40,  103,   93,   40,   44,
  107,  108,   44,   98,  215,  266,  125,  309,  219,  104,
  221,  123,  294,  260,  125,  527,   60,  710,  107,  257,
  115,  264,  265,   42,  285,  268,   97,  290,  291,  464,
  294,  295,  139,   93,  141,   91,  304,  351,  306,  125,
   61,  257,  356,   62,    0,  313,  309,   91,   61,  125,
  260,  276,    0,  471,   61,  280,  125,  274,  269,  477,
  374,  375,  133,  134,  135,  274,  276,  162,  163,  176,
  274,  274,  490,  491,   41,   42,  147,  274,  360,  123,
  125,   41,   61,  125,   44,  597,  280,  780,  600,   40,
   41,   61,  225,   44,   51,   42,   53,  307,  308,   40,
  309,  311,  312,  313,  314,  315,  316,  274,  693,  260,
  353,  274,  309,  274,   42,  274,   44,  264,   52,   53,
  227,  260,  193,  194,  195,  274,  273,  123,  223,  287,
  274,   88,  239,   90,  281,  282,  283,  284,  285,  557,
  558,  272,  213,  354,  272,  274,  731,  257,  258,  259,
  309,  261,  262,  263,  272,  265,   40,  274,  472,  473,
  277,   40,  274,  274,  267,  309,  272,  272,    0,  279,
  272,   41,   42,  244,  272,  109,  286,  284,  285,  286,
  272,   60,  116,  330,  331,  267,  300,  301,  274,  303,
  304,  305,  306,  307,  308,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  314,  315,  316,
  317,  318,   91,  631,   41,  322,  323,   44,   40,  326,
  264,  550,  156,  552,   42,  123,   44,  161,  299,  273,
  301,   42,  475,   44,  341,  342,  343,  281,  282,  283,
  284,  285,  290,  291,  123,  559,  294,  295,  319,  320,
  321,  290,  291,  324,  343,   86,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,  123,  385,
   40,   41,  690,  389,   44,   40,  392,   41,  385,   42,
   44,   44,  389,   40,  400,  392,   41,  123,  222,   44,
  361,   41,   42,  400,  401,  624,  403,  626,  627,  363,
  623,  345,  625,   60,   42,  412,   44,  138,   42,  140,
   44,  418,  268,  269,   41,   42,  272,  273,  632,  275,
  268,  269,  153,   41,  272,  273,   44,  275,    0,   40,
  463,  464,  288,  289,   91,  442,  270,  125,    0,   41,
  288,  289,   44,  414,   41,   44,   41,   44,  741,   44,
   41,   40,  459,   44,  310,   41,  463,  464,   44,  688,
   40,  684,  310,  686,  687,   41,  123,   41,   44,   62,
   44,  202,  309,  204,  463,  464,   41,   42,  257,  258,
  259,   58,  261,  262,  263,   41,  265,   42,   44,   44,
   58,  290,  291,  272,  273,  294,  295,  296,  297,  298,
  279,   41,   42,  726,  134,  135,   40,  286,   61,   40,
  362,  622,  694,  274,   44,   44,  274,  260,  525,  257,
  527,  554,  362,  530,  531,  532,  533,  534,  535,  536,
  537,  538,  539,  540,  541,  717,  268,  269,  370,  682,
  272,  273,   44,  275,  551,  370,  553,  554,  370,  520,
  521,  522,  362,  362,   44,   40,  288,  289,  529,  741,
  274,  274,  551,  257,  553,  554,  362,  257,  257,  362,
   61,  317,  260,  403,  685,  274,  257,  257,  310,   44,
  723,   60,  361,   44,   44,   44,  273,   44,  276,  370,
  597,   44,  370,  600,  370,  370,  375,  376,  377,   42,
  257,  258,  259,    0,  261,  262,  263,  370,  265,  752,
  370,    0,   91,  724,  621,  272,  273,  370,  370,  307,
  308,  628,  279,  311,  312,  313,  314,  315,  316,  286,
  637,  602,  257,  258,  259,  370,  261,  262,  263,  628,
  265,  370,  370,   44,  123,  370,  266,  362,  379,  620,
   44,  276,   44,  384,  279,   40,  387,  388,   44,  390,
  391,  286,  393,  394,  395,  396,  397,  398,  399,   44,
  677,  402,   44,  404,  405,  406,  407,   44,   44,   44,
   44,   44,   44,   40,   41,   44,  719,   44,   44,  696,
  661,  662,   44,   44,   44,   44,  268,  269,   44,  260,
  272,  273,   44,  275,  361,   62,  268,  269,   44,   91,
  272,  273,   44,  275,  721,  276,  288,  289,  375,  376,
  377,   44,   44,   44,   44,   44,  288,  289,   44,   44,
  461,  257,   44,   44,   60,   44,   93,   44,  310,   44,
   44,   44,   44,   91,  715,  362,  307,  308,  310,   44,
  311,  312,  313,  314,  315,  316,  362,  764,   44,   44,
   44,  362,  769,  770,  771,   91,  497,    0,  125,  257,
  501,  257,   40,  504,  362,   44,   93,   40,  363,  257,
  257,  512,  513,   40,  515,  264,   40,   40,   40,  760,
  761,  762,  370,   44,  273,  370,    0,  123,   40,  370,
   10,  772,  281,  282,  283,  284,  285,   54,  147,  146,
   18,  542,  543,  544,  257,  258,  259,  208,  261,  262,
  263,  190,  265,   64,  227,  679,  692,  347,  347,  272,
  273,  176,  376,  721,  619,  360,  279,  696,   -1,  570,
   -1,  572,  573,  286,  575,  576,   -1,  578,  579,  580,
  581,  582,  583,  584,   -1,   -1,  587,    0,  589,  590,
  591,  592,   40,   -1,   -1,   -1,  345,  598,   -1,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,  268,
  269,  288,  289,  272,  273,   -1,  275,   -1,   -1,  288,
  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  639,  288,
  289,  642,   -1,  310,  645,   -1,  273,  274,  361,   -1,
   -1,  310,  653,  654,   -1,  656,   -1,   -1,   -1,   -1,
   -1,  310,  375,  376,  377,   -1,   -1,   -1,  264,   -1,
   -1,  672,  673,  674,   -1,   -1,    0,  273,  679,   -1,
   -1,   40,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,  719,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,  273,  274,   -1,  365,  366,
  367,  368,  369,  370,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   40,   -1,  390,   -1,  268,  269,  363,   -1,  272,
  273,   -1,  275,   -1,  401,  402,  403,  404,   -1,   -1,
  318,  319,  320,   -1,   -1,  288,  289,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,  310,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,  273,  274,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   40,   -1,  390,   -1,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,  401,  402,  403,  404,   -1,   -1,   -1,
  318,  319,  320,   -1,   -1,  288,  289,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,  310,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   40,  379,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,   -1,  390,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,   -1,  401,  402,  403,  404,   -1,   -1,  318,
  319,  320,   -1,   -1,  288,  289,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,  310,  346,  347,  348,
  349,  350,  351,  352,  273,  274,  355,  356,  357,  358,
   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   40,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,
  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   40,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,  380,  381,  382,  383,  384,  385,  386,  387,
  388,  389,  401,  402,  403,  404,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,  274,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   40,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,  273,  274,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   40,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,  274,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,  273,  274,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   -1,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,
  273,  274,   -1,  365,  366,  367,  368,  369,   42,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   91,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   41,  346,  347,  348,  349,  350,  351,  352,
  273,  274,  355,  356,  357,  358,   -1,   -1,  361,  123,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   41,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,
  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,   42,  361,   44,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   60,  379,   -1,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,
   42,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,   -1,   -1,   -1,   -1,   91,   -1,   60,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   41,   -1,   -1,   -1,   -1,   60,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,  123,   91,
   -1,   60,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  300,  301,   91,  303,
  304,  305,  306,  307,  308,   -1,   -1,   -1,   -1,   -1,
   -1,  123,   91,   -1,   41,   -1,   -1,   -1,  257,  258,
  259,   60,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
  123,   -1,   -1,   60,   -1,   -1,   -1,  276,   -1,   -1,
  279,   -1,   -1,  220,  123,   -1,   -1,  286,   -1,   -1,
   -1,  228,   91,   -1,  260,   -1,   -1,  361,   -1,   -1,
   -1,  257,  258,  259,   91,  261,  262,  263,   -1,  265,
  276,  375,  376,  377,   -1,   -1,   -1,   -1,   -1,   -1,
  276,   -1,   -1,  279,  123,   -1,   -1,  264,  265,   -1,
  286,  268,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,  307,  308,   -1,   -1,  311,  312,  313,  314,  315,
  316,  288,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,   -1,   -1,  125,   -1,   -1,
   -1,  286,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,  257,  258,  259,  279,  261,  262,
  263,  348,  265,  350,  286,   -1,  353,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,  264,  279,   -1,   -1,  366,
   -1,  368,   -1,  286,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  300,  301,   -1,
  303,  304,  305,  306,  307,  308,  361,  125,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   -1,
  375,  376,  377,  272,  273,   -1,   -1,  264,   -1,   -1,
  279,   -1,   -1,   -1,   -1,   -1,  273,  286,   -1,  361,
   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,  375,  376,  377,  293,   -1,  361,   -1,
  257,  258,  259,  260,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,  375,  376,  377,  273,   -1,  274,   -1,  276,
  467,   -1,  279,   -1,   -1,   -1,   -1,  125,  475,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  485,   -1,
  487,   -1,   -1,   -1,   -1,  492,   -1,   -1,   -1,   -1,
   -1,   -1,  361,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  318,  319,  320,   -1,   -1,   -1,  375,  376,  377,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,  273,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  318,  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,  273,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  318,  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,  273,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,  273,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,  273,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,  273,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,  273,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   60,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,
  390,   -1,   -1,   91,   -1,   -1,   -1,   -1,  318,  319,
  320,  401,  402,  403,  404,   -1,   -1,   -1,   60,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,  123,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   91,
   -1,  361,   60,   -1,   -1,  365,  366,  367,  368,  369,
  273,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,  123,   -1,   91,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  318,  319,  320,   -1,   60,
   -1,   -1,   -1,   -1,   -1,  123,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   91,   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,
   -1,   60,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   60,   -1,   -1,
   -1,   -1,  123,   60,   -1,   -1,  264,  390,   -1,   -1,
   -1,   -1,   91,   -1,   -1,  273,   -1,   -1,  401,  402,
  403,  404,   -1,  281,  282,  283,  284,  285,   91,   -1,
   -1,   -1,   -1,   60,   91,   -1,   -1,   -1,   -1,   -1,
   -1,  299,  300,  301,  123,  303,  304,  305,  306,  307,
  308,   -1,  264,   -1,   -1,   -1,   60,   -1,   -1,   -1,
  123,  273,   -1,   -1,   91,   -1,  123,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,   60,   -1,
   -1,  293,   -1,   60,   -1,   -1,  264,   91,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  123,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   60,   91,
   -1,   -1,   -1,   60,   91,   -1,   -1,   -1,   -1,  123,
   -1,   -1,  300,  301,   -1,  303,  304,  305,  306,  307,
  308,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,   91,
   -1,  123,   -1,  264,   91,   -1,  123,   -1,  125,   60,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   91,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   91,  302,   60,   -1,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,  123,   -1,
  125,  264,  281,  282,  283,  284,  285,  264,   60,   -1,
  273,   -1,  123,   91,   -1,   93,  273,   -1,  281,  282,
  283,  284,  285,  302,  281,  282,  283,  284,  285,   -1,
   60,   -1,   -1,   -1,   -1,  292,  299,  264,   -1,   91,
  297,  298,   -1,   -1,   -1,  123,  273,   -1,   -1,   -1,
   -1,  278,   60,   -1,  281,  282,  283,  284,  285,   -1,
  264,   91,   -1,   -1,   -1,   -1,  293,   -1,   -1,  273,
   -1,  123,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  264,   91,   -1,   -1,   -1,  264,  265,  293,
   -1,  273,   -1,  123,   -1,   -1,  273,  274,   -1,  281,
  282,  283,  284,  285,  281,  282,  283,  284,  285,   -1,
  292,   -1,  264,  265,   -1,  123,   -1,  264,  265,   -1,
   -1,  273,  274,   -1,   -1,   -1,  273,  274,   -1,  281,
  282,  283,  284,  285,  281,  282,  283,  284,  285,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  264,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  273,  274,  261,  262,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  264,   -1,   -1,  273,
  274,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  382,  383,  384,  385,   -1,
   -1,   -1,   -1,   -1,  391,  392,  393,  394,  395,  396,
  397,  398,  399,  400,  318,  319,  320,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
  274,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,  273,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,
  };

#line 1066 "Repil/IR/IR.jay"

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
