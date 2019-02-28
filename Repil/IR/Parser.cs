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
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 239:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 240:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
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
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 246:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 247:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 248:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 252:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new FenceInstruction ((AtomicConstraint)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 960 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 964 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 968 "Repil/IR/IR.jay"
  {
        yyVal = new InttoptrInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 972 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 269:
#line 976 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 270:
#line 980 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 271:
#line 984 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 272:
#line 988 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1000 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1004 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1016 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1020 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 283:
#line 1032 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 284:
#line 1036 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 285:
#line 1040 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 286:
#line 1044 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 287:
#line 1048 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
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
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 290:
#line 1060 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 291:
#line 1064 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 292:
#line 1068 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 293:
#line 1072 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 294:
#line 1076 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 295:
#line 1080 "Repil/IR/IR.jay"
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
   47,   47,   47,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,
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
    7,    2,    7,    5,    6,    5,    5,    5,    6,    4,
    4,    5,    6,    5,    6,    6,    6,    7,    5,    6,
    7,    4,    5,    6,    5,    2,    5,    4,    4,    4,
    4,    5,    6,    7,    6,    6,    4,    7,    8,    5,
    6,    5,    5,    6,    3,    4,    5,    6,    7,    4,
    5,    6,    6,    4,    5,    7,    8,    5,    6,    4,
    5,    4,    5,    5,    4,
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
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  184,    0,    0,  190,    0,    0,
    0,    0,    0,    0,    0,   44,    0,   66,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   23,    0,   42,
    0,    0,    0,    0,    0,  232,    0,    0,  230,    0,
  226,  227,    0,    0,  224,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  229,  256,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  128,  129,  130,  131,  132,  133,  134,  135,  136,
  137,    0,  138,  139,  150,  151,  152,  153,  141,  143,
  144,  145,  146,  142,  140,  148,  149,  147,    0,    0,
    0,    0,    0,    0,    0,   95,  185,    0,  191,    0,
    0,    0,    0,    0,    0,    0,   90,    0,   91,  215,
    0,  160,  157,  159,    0,    0,    0,    0,   41,   93,
    0,    0,    0,  174,    0,    0,    0,    0,  225,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  218,    0,  196,
    0,    0,    0,    0,    0,    0,    0,    0,   96,    0,
    0,    0,    0,   92,    0,    0,    0,   94,   98,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  252,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   97,   99,    0,    0,  181,    0,  182,
    0,    0,  234,    0,  253,  288,    0,  262,  273,    0,
  257,  291,    0,  277,  255,  293,  285,  281,    0,    0,
  270,    0,  238,  237,  272,  294,    0,    0,  236,    0,
  164,  177,    0,    0,    0,    0,    0,    0,    0,    0,
  219,    0,    0,  198,    0,    0,  199,    0,    0,  242,
    0,    0,    0,    0,    0,  101,    0,  158,    0,    0,
    0,    0,  221,  235,  289,  274,  278,  282,  271,  239,
  266,  283,    0,    0,    0,    0,    0,    0,  265,  254,
    0,    0,    0,    0,  201,    0,  197,    0,    0,  243,
    0,    0,  250,    0,  100,  183,  231,    0,  233,  222,
    0,  268,    0,  286,    0,  220,  279,    0,  209,  203,
    0,    0,    0,    0,  208,  204,  202,  200,    0,  251,
  223,  269,  287,  206,    0,    0,    0,    0,    0,  207,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  213,  210,  212,    0,    0,  211,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  191,  153,  145,   50,
  154,  560,  233,   51,   52,   36,  146,  137,  711,  288,
  155,  648,  192,   61,   62,  113,  114,  109,  174,  354,
  227,   78,  170,  171,  228,  175,  452,  469,  649,  198,
  682,  389,  613,  650,  355,  356,  357,  358,  359,  561,
  636,  696,  697,  766,  289,  557,  558,  712,  713,  394,
  395,  427,
  };
  protected static readonly short [] yySindex = {         1755,
   30, -163,   54,   66,   82, 3698, 3795, -177,    0, 1755,
    0,    0,    0,    0, -121,   90,   99,  295,  -93,  -24,
    0,    0,    0,    0,    0,    0,    0, 3980,    0,    0,
 3912,  -75,  -66,    0,  161, 2181,  -34, 3980,  -20,  151,
    0,    0,  -30,  -18,    0,    0,    0,    0,    0, 3980,
 -118,   32,  106,  -46,  240,   -5,  226,   -1,    0,  161,
   -7,  256,   35, 3980,   86,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    2, 3980, 3600,  298, 2134,
    3,  298,  236,    0,    0, 1990, 3980, -118, 3980, -118,
    0,  245,    0, -213,  333,  264, 3877,  298,    0, 3980,
 3980,   12, 3980,  298,    6,    7, 3980, 2127, -205,    0,
    0,  161,   79,    0,  298, -205, 2118,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   16,  363,
  371,  383, 4009, 4009, 4009,  387,    0, 1990, 3980, 1898,
 3980,  356,  379,  381,   85,    0, -213, 3883,    0,    0,
    0,    0,  -25, 1990,    0,    0,   32,  161,   47,  395,
   64, -205,  298,  298,   10,    0,    0,    0,  556,  173,
    0,    0,  140, -219,    0,    0, 3769,  140,  140,  140,
  403,  422,    0,    0,    0,    0,    0,    0,    0,    0,
  613,    0,  426, 4009, 4009, 4009,    0,   13,  135,  -31,
  108,  434, 1990,  123,  442, 1954, 3834,  228, 1879,    0,
 -213,  179,   20,    0,    0, 3906,    0,  140,    0,    0,
    0,  140, -120,  140,   32,  298,    0,  408,    0, 3735,
 -107,  231, -111,    0,    0,  140,  140,  246, 1799,    0,
    0, 3980,  143,  145,  146,    0, 4009,    0,    0,  252,
  155,  479,  267,  164,  167,  486,    0,  511,    0, 1847,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  -96, -219, 4796, -105, -219,  140,   32,    0,    0, 4796,
 -100,    0,  279, 4796,    0,    0,    0,  199,    0,   38,
 3980, 3980, 3980,    0,  280,  299,  193,    0,  300,  306,
  205,   78,    0, 4796,  -99,  -94,  507, 4009, -168, 4009,
 1815, 3980, 1815, 3980, 1815, 3980, 3980,  144, 3980, 3980,
 3980, 1815,  237,  726, 3980, 3980, 3980, 4009, 4009, 4009,
 3980, 2174, 3671,  253,    8, 4009, 4009, 4009, 4009, 4009,
 4009, 4009, 4009, 4009, 4009, 4009, 4009,  111, 3913, 3980,
 3980, 2181,  166, 2187,    0, 4796,  280,    0,  280, 4796,
  -78,  -90,  140, 2274, 4796,    0, 2354,    0, 1799, 4009,
  378,  392,  394,  297,  280,  318,  280,    0,  322,    0,
  229, 2443, 4796, 4796, 5192,    0,  324, 2075,    0,  555,
    0,    0, 1990, 1815,    0, 1990, 1990, 1815, 1990, 1990,
 1815, 1990, 1990, 3980, 1990, 1990, 1990, 1990, 1990, 1815,
 3980, 1990, 3980, 1990, 1990, 1990, 1990,  558,  559,  563,
  124, 3980,  139, 4009,  565,    0,    0, 3980,  224,  230,
  242,  249,  261,  266,  274,  278,  281,  284,  285,  287,
  293,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3980,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3980,   37,
 1990,  104, 3980, 3600, 2181,    0,    0,  280,    0,  297,
  297, 2530, 4796, 4796,  -74, -219,    0, 2610,    0,    0,
  572,    0,    0,    0,  297,  280,  297,  280,    0,    0,
 2699, 2786,  280,    0,  578,  344,  605, 1990,    0,  621,
  622, 1990,  624,  625, 1990,  627,  629, 1990,  631,  633,
  637,  640,  642, 1990, 1990,  650, 1990,  656,  658,  660,
  674, 4009, 4009, 4009,  310,  290, 3980,  675, 3980,  349,
 4009, 3980, 3980, 3980, 3980, 3980, 3980, 3980, 3980, 3980,
 3980, 3980, 3980, 1990, 1990, 2075,  677,    0,  678,    0,
  683,  104,  104, 3980,  104, 3980, 3600,  297,    0, 2866,
 2955, 4796,  -69,    0, 4009,  297,  297,    0,    0,  297,
  344,  638, 2075,  680, 2075, 2075,  690, 2075, 2075,  691,
 2075, 2075,  695, 2075, 2075, 2075, 2075, 2075,  696,  700,
 2075,  702, 2075, 2075, 2075, 2075,    0,  706,  710,  500,
 3980, 1990,  714, 3980,  715, 4009,  716,  161,  161,  161,
  161,  161,  161,  161,  161,  161,  161,  161,  161,  717,
  720,  721,  676, 4009, 3646,  140,  683,  683,  104,  683,
  104,  104, 3980,    0,    0, 3042, 4796,    0,  257,    0,
  722, 3980,    0, 2075,    0,    0, 2075,    0,    0, 2075,
    0,    0, 2075,    0,    0,    0,    0,    0, 2075, 2075,
    0, 2075,    0,    0,    0,    0, 4009, 4009,    0,  727,
    0,    0,  415,  734,  417,  739, 4009, 2075, 2075, 2075,
    0,  740, 3934,    0,  490,  259,    0,  140,  140,    0,
  683,  140,  683,  683,  104,    0, 3122,    0, 4009,  344,
  741, 3941,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  425,  513,  427,  535, 4009,  744,    0,    0,
  707, 4009,  759, 1335,    0, 2020,    0, 3967,  140,    0,
  140,  140,    0,  683,    0,    0,    0,  344,    0,    0,
  545,    0,  548,    0,  744,    0,    0, 1790,    0,    0,
  448,  778,  780,  785,    0,    0,    0,    0,  140,    0,
    0,    0,    0,    0,  286,  787, 4009, 4009, 4009,    0,
 3980,  458,  459,  462,  388, 3980, 3980, 3980, 4009,  401,
  412,  433,  789,    0,    0,    0, 4009,  345,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  836,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  315,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   25,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  797,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  307,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  797,    0,  797,
    0,    0,    0,    0,    0,    0,    0,    0,  612,    0,
    0,    0,    0,  797,    0,    0,    0,   81,  797,    0,
  797,    0,    0,    0,    0,    0,    0,    0,  340,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  172,
 1807, 1893,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  797,    0,    0,  797,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  358,    0,    0,
    0,    0,    0,    0,    0,  215,  239,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  370,    0,    0,    0,    0,    0,  368,    0,  797,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  797,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3211,    0, 4876,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  797,  797,  797,  410,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  797,    0,    0,  797,  797,    0,  797,  797,
    0,  797,  797,    0,  797,  797,  797,  797,  797,    0,
    0,  797,    0,  797,  797,  797,  797,    0,    0,    0,
  797,    0,  797,    0,    0,    0,    0,    0,  797,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  797,
  797,    0,    0,    0,    0,    0,    0, 3298,    0, 3378,
 4956,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  566,  628,  826,    0,    0,    0,
    0,    0, 5036,    0,    0,    0,    0,  797,    0,    0,
    0,  797,    0,    0,  797,    0,    0,  797,    0,    0,
    0,    0,    0,  797,  797,    0,  797,    0,    0,    0,
    0,    0,    0,    0,    0,  797,    0,    0,    0,  797,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  797,  797,    0, 3996,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3467,    0,    0,
    0,    0,    0,    0,    0,  995, 1084,    0,    0, 5116,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  797,    0,    0,    0,    0,    0,  701,  791,  871,
  960, 1049, 1129, 1209, 1298, 1387, 1467, 1547, 1636,    0,
    0,    0,    0,    0,    0, 4076,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  797,    0,    0, 4156,    0,    0,
    0, 4236,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4316,    0,    0,
    0,    0,  374,  797,    0,    0,    0,    0, 4396,    0,
 4476, 4556,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 4636,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 4716,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  797,    0,    0,    0,    0,  797,
  797,  797,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  830,  788,    0,    0,    0,    0,  697,  699,   71,
   -6,  218, 2015,  531,    0,  825,  636, -199,  641, -300,
    0,  -82,  653,  786,   -2,    0,  681,   58,  -32, -273,
  -19, -317,    0,  623,  157,  -38,    0,    0, -649,  342,
    0, -465, -518,  159, -294,    0,  514,  515,  484, -410,
   24,    0,  136,    0,  508,    0,  247,    0,  170,  -97,
  -16,    0,
  };
  protected static readonly short [] yyTable = {            35,
   35,  381,  273,   37,   39,   80,  364,  261,   57,  264,
  367,  284,  247,   92,  156,  280,   77,  360,  216,   80,
  615,   35,  365,  383,   60,   58,  304,   96,  384,   35,
  382,   35,  484,   76,  474,   81,  100,  728,   80,  287,
  582,   80,   80,   86,  483,   80,   80,   64,  572,   80,
  197,  197,  197,  647,  232,  100,  247,   60,  107,  477,
  303,  249,  142,  216,   72,  156,  143,   32,   70,  477,
   35,   35,  477,  112,  105,  106,   31,  755,   66,   66,
  138,  370,  140,  180,   45,   46,  482,  477,   66,  172,
   15,  488,  680,  158,  159,  684,  161,  387,   33,  215,
   35,  169,  287,  172,  165,   66,   16,   17,  219,  501,
  502,  243,  244,  245,   18,  651,  388,   99,  380,  176,
   72,   87,  177,   89,   71,  210,   19,  556,  211,  223,
   31,   40,  203,  270,  206,  234,  160,  246,   42,  116,
  231,  234,   20,   80,  267,  236,  237,  798,  144,   70,
   43,  637,  638,  232,  640,  157,  221,  567,  139,   44,
  141,  162,  283,  134,  294,   66,  232,  535,  283,  287,
  112,  102,  178,  283,  283,   47,   48,  232,  247,  283,
   66,   63,  537,  232,  234,  271,   54,  477,  172,  272,
   65,  275,  234,  477,  135,  283,  248,  234,  234,  283,
  260,  172,   66,   32,  283,   71,  477,  477,  278,  570,
  571,   83,  172,  229,  103,  398,  230,  401,  172,  266,
  224,  225,  211,  169,  410,  386,  133,  390,  701,   84,
  703,  704,  234,  234,   33,  290,  234,   79,  104,  368,
   91,   85,  369,  362,  747,  418,  419,  420,  214,   55,
  425,   82,   56,  430,  431,  432,  433,  434,  435,  436,
  437,  438,  439,  440,  441,   66,   31,  541,   95,  499,
   98,   21,  369,  104,  115,  477,  477,  163,  164,   94,
   22,  226,  771,  277,  371,  372,  373,  491,   23,   24,
   25,   26,   27,  214,  744,   80,   32,  708,  646,  737,
  709,  101,  738,  136,  393,  396,  397,  399,  400,  402,
  403,  405,  406,  407,  408,  409,  412,  414,  415,  416,
  417,   45,   46,  234,  421,  423,  780,   33,  429,  369,
  486,   66,  473,  611,  118,  119,  120,  108,  121,  122,
  123,  538,  124,  470,  471,   35,   72,   88,   97,  472,
   88,  477,  103,  286,   72,  202,  127,  205,  117,   31,
  118,  119,  120,  128,  121,  122,  123,   64,  124,   21,
  428,  217,  147,  707,   72,  125,  126,  509,  193,   72,
  109,  509,  127,  109,  509,  799,  148,  508,  709,  128,
   66,  512,  616,  509,  515,   45,   46,  518,  110,   47,
   48,  110,  194,  524,  525,   72,  527,   21,   35,   18,
  195,   35,  477,  207,  205,  536,   22,  205,  492,   66,
  252,  540,  196,  256,   23,   24,   25,   26,   27,   66,
  201,  789,  493,   66,  494,   66,  208,   72,  209,  102,
  102,  794,   66,  102,  102,  554,  102,  234,  172,  607,
  608,  609,  795,   66,  564,  566,  220,  775,  617,  102,
  102,  239,  555,  238,  129,  242,   35,   35,   35,  250,
  562,  563,  565,  796,   66,  199,  200,  251,  130,  131,
  132,  102,  103,  103,  253,  254,  103,  103,  404,  103,
  442,  443,  444,  445,  446,  447,  448,  449,  450,  451,
   21,  263,  103,  103,  282,  285,  104,  104,  295,   22,
  104,  104,  291,  104,  292,  293,  296,   23,   24,   25,
   26,   27,  297,  298,  103,  299,  104,  104,  300,  301,
  612,   66,  612,  686,  765,  618,  619,  620,  621,  622,
  623,  624,  625,  626,  627,  628,  629,  643,  104,  134,
  302,  692,  366,  232,  376,  375,  377,   35,  287,   35,
   35,  639,  378,  641,  642,   19,  379,  385,  475,  426,
  283,   72,   72,   72,  496,   72,   72,   72,  498,   72,
  135,  411,   88,   90,   45,   46,   72,   72,   47,   48,
   49,   29,   30,   72,  721,  722,  504,   66,  506,  542,
   72,  532,  533,  698,  612,  505,  534,  612,  539,  387,
  507,  543,  133,  510,  511,  575,  513,  514,  544,  516,
  517,  581,  519,  520,  521,  522,  523,   20,  695,  526,
  545,  528,  529,  530,  531,  546,   35,   21,   21,  173,
  705,   21,   21,  547,   21,  260,  179,  548,  583,  757,
  549,   73,  176,  550,  551,  176,  552,   21,   21,  234,
  699,  700,  553,  702,  585,  586,  739,  588,  589,  741,
  591,  610,  592,  176,  594,   72,  595,   18,   18,   21,
  596,   18,   18,  597,   18,  598,  734,  218,  559,   72,
   72,   72,  222,  601,  782,  783,  784,   18,   18,  603,
  234,  604,  234,  605,  176,  260,  793,   68,   69,  769,
   70,   71,   72,   73,   74,   75,  278,  606,  614,   18,
  633,  634,  635,  654,  740,  584,  742,  743,  652,  587,
  234,  695,  590,  657,  660,  593,  176,  240,  663,  669,
   72,  599,  600,  670,  602,  672,  118,  119,  120,  677,
  121,  122,  123,  678,  124,  276,  679,  683,  685,  687,
  688,  125,  126,  689,  690,  710,  556,  770,  127,  752,
  723,  630,  631,  632,  785,  128,  724,  725,  726,  790,
  791,  792,  727,  732,  748,   32,  751,  709,  753,   68,
   69,  754,   70,   71,   72,   73,   74,   75,  758,  756,
  653,  772,  655,  656,  773,  658,  659,  363,  661,  662,
  776,  664,  665,  666,  667,  668,   33,  777,  671,  778,
  673,  674,  675,  676,  779,   25,  781,  786,  787,  681,
   72,  788,  797,   19,   19,    1,   72,   19,   19,   41,
   19,   93,   53,  241,  213,  212,  265,  262,   31,  102,
  129,  736,  279,   19,   19,   68,   69,  235,   70,   71,
   72,   73,   74,   75,  130,  131,  132,  746,  503,  478,
  479,  714,  181,  768,  715,   19,  490,  716,    0,  691,
  717,  750,    0,    0,  176,  176,  718,  719,  182,  720,
    0,    0,    0,    0,    0,   20,   20,    0,    0,   20,
   20,    0,   20,    0,    0,  729,  730,  731,    0,    0,
   72,    0,  735,    0,    0,   20,   20,    0,    0,  183,
  184,    0,    0,  185,  186,  187,  188,  189,  190,  176,
  176,  176,    0,    0,    0,    0,    0,   20,    0,    0,
  176,    0,    0,  176,  176,  176,  176,  176,  176,  176,
  176,  176,  176,  767,  176,  176,    0,  176,  176,  176,
  176,  176,  176,  176,    0,    0,  176,  176,  176,  176,
    0,    0,  176,  290,  290,    0,  176,  176,  176,  176,
  176,  176,  176,  176,  176,  176,  176,  176,  176,   21,
  176,    0,    0,    0,   22,    0,    0,    0,   22,   72,
    0,  176,    0,    0,    0,    0,   23,   24,   25,   26,
   27,    0,  176,  176,  176,  176,    0,    0,  290,  290,
  290,    0,    0,    0,    0,    0,    0,    0,    0,  290,
    0,    0,  290,  290,  290,  290,  290,  290,  290,  290,
  290,  290,    0,  290,  290,    0,  290,  290,  290,  290,
  290,  290,  290,    0,    0,  290,  290,  290,  290,    0,
    0,  290,    0,  295,  295,  290,  290,  290,  290,  290,
  413,  290,  290,  290,  290,  290,  290,  290,    0,  290,
    0,    0,    0,   24,    0,    0,    0,    0,   72,    0,
  290,    0,    0,   25,   25,    0,    0,   25,   25,    0,
   25,  290,  290,  290,  290,    0,    0,    0,  295,  295,
  295,    0,    0,   25,   25,    0,    0,    0,    0,  295,
    0,    0,  295,  295,  295,  295,  295,  295,  295,  295,
  295,  295,    0,  295,  295,   25,  295,  295,  295,  295,
  295,  295,  295,  280,  280,  295,  295,  295,  295,    0,
    0,  295,    0,    0,    0,  295,  295,  295,  295,  295,
    0,  295,  295,  295,  295,  295,  295,  295,   72,  295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  295,    0,    0,    0,    0,    0,    0,    0,  280,  280,
  280,  295,  295,  295,  295,    0,    0,    0,    0,  280,
    0,    0,  280,  280,  280,  280,  280,  280,  280,  280,
  280,  280,    0,  280,  280,    0,  280,  280,  280,  280,
  280,  280,  280,    0,    0,  280,  280,  280,  280,    0,
    0,  280,  261,  261,    0,  280,  280,  280,  280,  280,
    0,  280,  280,  280,  280,  280,  280,  280,   72,  280,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  280,    0,   22,   22,    0,    0,   22,   22,    0,   22,
    0,  280,  280,  280,  280,    0,    0,  261,  261,  261,
    0,    0,   22,   22,    0,    0,    0,    0,  261,    0,
    0,  261,  261,  261,  261,  261,  261,  261,  261,  261,
  261,    0,  261,  261,   22,  261,  261,  261,  261,  261,
  261,  261,    0,    0,  261,  261,  261,  261,    0,    0,
  261,  258,  258,    0,  261,  261,  261,  261,  261,    0,
  261,  261,  261,  261,  261,  261,  261,   72,  261,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  261,
    0,   24,   24,    0,    0,   24,   24,    0,   24,    0,
  261,  261,  261,  261,    0,    0,  258,  258,  258,    0,
    0,   24,   24,    0,    0,    0,   66,  258,    0,    0,
  258,  258,  258,  258,  258,  258,  258,  258,  258,  258,
    0,  258,  258,   24,  258,  258,  258,  258,  258,  258,
  258,  259,  259,  258,  258,  258,  258,    0,    0,  258,
    0,    0,    0,  258,  258,  258,  258,  258,    0,  258,
  258,  258,  258,  258,  258,  258,   72,  258,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  258,    0,
    0,    0,    0,    0,    0,    0,  259,  259,  259,  258,
  258,  258,  258,    0,    0,    0,    0,  259,    0,    0,
  259,  259,  259,  259,  259,  259,  259,  259,  259,  259,
    0,  259,  259,    0,  259,  259,  259,  259,  259,  259,
  259,  260,  260,  259,  259,  259,  259,    0,    0,  259,
    0,    0,    0,  259,  259,  259,  259,  259,    0,  259,
  259,  259,  259,  259,  259,  259,   72,  259,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  259,    0,
    0,    0,    0,    0,    0,    0,  260,  260,  260,  259,
  259,  259,  259,    0,    0,    0,    0,  260,    0,    0,
  260,  260,  260,  260,  260,  260,  260,  260,  260,  260,
    0,  260,  260,    0,  260,  260,  260,  260,  260,  260,
  260,    0,    0,  260,  260,  260,  260,    0,    0,  260,
  292,  292,    0,  260,  260,  260,  260,  260,    0,  260,
  260,  260,  260,  260,  260,  260,   72,  260,    0,    0,
    0,  118,  119,  120,    0,  121,  122,  123,  260,  124,
    0,    0,    0,    0,    0,    0,  759,  760,    0,  260,
  260,  260,  260,  127,    0,  292,  292,  292,    0,    0,
  128,    0,    0,    0,    0,    0,  292,    0,    0,  292,
  292,  292,  292,  292,  292,  292,  292,  292,  292,    0,
  292,  292,    0,  292,  292,  292,  292,  292,  292,  292,
    0,    0,  292,  292,  292,  292,    0,    0,  292,  284,
  284,    0,  292,  292,  292,  292,  292,    0,  292,  292,
  292,  292,  292,  292,  292,   72,  292,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  292,    0,    0,
    0,    0,    0,    0,    0,  761,    0,    0,  292,  292,
  292,  292,    0,    0,  284,  284,  284,    0,    0,  762,
  763,  764,    0,    0,    0,  284,    0,    0,  284,  284,
  284,  284,  284,  284,  284,  284,  284,  284,    0,  284,
  284,    0,  284,  284,  284,  284,  284,  284,  284,  276,
  276,  284,  284,  284,  284,    0,    0,  284,    0,    0,
    0,  284,  284,  284,  284,  284,    0,  284,  284,  284,
  284,  284,  284,  284,    0,  284,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  284,    0,    0,    0,
    0,    0,    0,    0,  276,  276,  276,  284,  284,  284,
  284,    0,    0,    0,    0,  276,    0,    0,  276,  276,
  276,  276,  276,  276,  276,  276,  276,  276,    0,  276,
  276,    0,  276,  276,  276,  276,  276,  276,  276,  267,
  267,  276,  276,  276,  276,    0,    0,  276,    0,    0,
  774,  276,  276,  276,  276,  276,    0,  276,  276,  276,
  276,  276,  276,  276,    0,  276,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  276,    0,    0,    0,
    0,    0,    0,    0,  267,  267,  267,  276,  276,  276,
  276,    0,    0,    0,   32,  267,    0,    0,  267,  267,
  267,  267,  267,  267,  267,  267,  267,  267,   66,  267,
  267,    0,  267,  267,  267,  267,  267,  267,  267,    0,
    0,  267,  267,  267,  267,   33,    0,  267,  240,  240,
    0,  267,  267,  267,  267,  267,    0,  267,  267,  267,
  267,  267,  267,  267,    0,  267,    0,    0,    0,    0,
    0,   63,    0,    0,    0,    0,  267,   31,    0,   66,
    0,  204,    0,    0,    0,    0,    0,  267,  267,  267,
  267,    0,    0,  240,  240,  240,    0,  134,    0,    0,
    0,    0,    0,    0,  240,    0,    0,  240,  240,  240,
  240,  240,  240,  240,  240,  240,  240,    0,  240,  240,
    0,  240,  240,  240,  240,  240,  240,  240,  135,    0,
  240,  240,  240,  240,    0,   66,  240,  255,    0,    0,
  240,  240,  240,  240,  240,    0,  240,  240,  240,  240,
  240,  240,  240,  134,  240,    0,    0,   64,    0,    0,
  133,    0,    1,    2,    0,  240,    3,    4,    0,    5,
    0,   66,    0,    0,    0,    0,  240,  240,  240,  240,
    0,    0,    6,    7,  135,    0,  118,  119,  120,  134,
  121,  122,  123,    0,  124,  118,  119,  120,    0,  121,
  122,  123,    0,  124,    8,  286,   63,    0,  127,    0,
    0,    0,    0,    0,  286,  128,  133,  127,   21,  134,
  135,    0,   63,    0,  128,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
    0,    0,    0,  118,  119,  120,    0,  121,  122,  123,
  135,  124,  133,   63,   63,    0,    0,   63,   63,   63,
   63,   63,   63,    0,    0,  127,    0,    0,    0,    0,
    0,    0,  128,    0,  134,  118,  119,  120,    0,  121,
  122,  123,  133,  124,  391,  392,    0,    0,    0,    0,
    0,    0,   64,    0,  118,  119,  120,  127,  121,  122,
  123,    0,  124,    0,  128,  135,    0,  168,   64,  125,
  126,    0,    0,    0,  111,    0,  127,    0,    0,    0,
    0,    0,    0,  128,    0,    0,   32,    0,    0,    0,
    0,    0,    0,   32,    0,    0,    0,  133,    0,   64,
   64,    0,    0,   64,   64,   64,   64,   64,   64,    0,
  118,  119,  120,    0,  121,  122,  123,   33,  124,    0,
    0,    0,    0,    0,   33,  125,  126,    0,    0,    0,
    0,    0,  127,   32,    0,    0,    0,  274,    0,  128,
   32,    0,    0,    0,    0,  281,  118,  119,  120,   31,
  121,  122,  123,    0,  124,    0,   31,    0,  129,    0,
    0,  125,  126,    0,   33,    0,    0,    0,  127,    0,
    0,   33,  130,  131,  132,  128,  118,  119,  120,    0,
  121,  122,  123,    0,  124,  305,  306,    0,    0,  361,
    0,  125,  126,    0,    0,    0,   31,    0,  127,    0,
    0,    0,    0,   31,    0,  128,    0,    0,    0,  374,
    0,  476,    0,    0,  129,    0,    0,    0,    0,   68,
   69,    0,   70,   71,   72,   73,   74,   75,  130,  131,
  132,  118,  119,  120,    0,  121,  122,  123,    0,  124,
    0,    0,    0,    0,    0,    0,  125,  126,    0,    0,
  129,    0,    0,  127,    0,    0,    0,    0,    0,    0,
  128,    0,    0,    0,  130,  131,  132,    0,    0,    0,
    0,  480,    0,  481,    0,    0,  485,  181,    0,    0,
  129,    0,    0,    0,    0,    0,    0,    0,    0,  495,
   21,  497,    0,  182,  130,  131,  132,   21,  487,   22,
    0,    0,    0,    0,  166,    0,   22,   23,   24,   25,
   26,   27,    0,    0,   23,   24,   25,   26,   27,  167,
    0,    0,    0,    0,  183,  184,  110,    0,  185,  186,
  187,  188,  189,  190,    0,  129,    0,   21,    0,    0,
    0,    0,    0,    0,   21,    0,   22,    0,    0,  130,
  131,  132,    0,   22,   23,   24,   25,   26,   27,  307,
    0,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  422,    0,    0,  489,   67,
   68,   69,    0,   70,   71,   72,   73,   74,   75,    0,
    0,    0,  568,    0,    0,    0,    0,    0,    0,    0,
  573,    0,    0,    0,  308,  309,  310,    0,    0,    0,
  576,    0,  577,    0,    0,  311,    0,  580,  312,  313,
  314,  315,  316,  317,  318,  319,  320,  321,    0,  322,
  323,    0,  324,  325,  326,  327,  328,  329,  330,    0,
    0,  331,  332,  333,  334,    0,  307,  335,    0,    0,
    0,  336,  337,  338,  339,  340,    0,  341,  342,  343,
  344,  345,  346,  347,    0,  348,    0,  500,    0,    0,
    0,    0,    0,    0,    0,    0,  349,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  350,  351,  352,
  353,  308,  309,  310,    0,    0,    0,    0,    0,    0,
    0,    0,  311,    0,    0,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,    0,  322,  323,    0,  324,
  325,  326,  327,  328,  329,  330,  307,    0,  331,  332,
  333,  334,    0,    0,  335,    0,    0,    0,  336,  337,
  338,  339,  340,    0,  341,  342,  343,  344,  345,  346,
  347,    0,  348,    0,  569,    0,    0,    0,    0,    0,
    0,    0,    0,  349,    0,    0,    0,    0,    0,    0,
    0,  308,  309,  310,  350,  351,  352,  353,    0,    0,
    0,    0,  311,    0,    0,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,    0,  322,  323,    0,  324,
  325,  326,  327,  328,  329,  330,    0,    0,  331,  332,
  333,  334,    0,    0,  335,  307,    0,    0,  336,  337,
  338,  339,  340,    0,  341,  342,  343,  344,  345,  346,
  347,    0,  348,    0,  574,    0,    0,    0,    0,    0,
    0,    0,    0,  349,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  350,  351,  352,  353,    0,    0,
  308,  309,  310,    0,    0,    0,    0,    0,    0,    0,
    0,  311,    0,    0,  312,  313,  314,  315,  316,  317,
  318,  319,  320,  321,    0,  322,  323,    0,  324,  325,
  326,  327,  328,  329,  330,    0,    0,  331,  332,  333,
  334,    0,  307,  335,    0,    0,    0,  336,  337,  338,
  339,  340,    0,  341,  342,  343,  344,  345,  346,  347,
    0,  348,    0,  578,    0,    0,    0,    0,    0,    0,
    0,    0,  349,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  350,  351,  352,  353,  308,  309,  310,
    0,    0,    0,    0,    0,    0,    0,    0,  311,    0,
    0,  312,  313,  314,  315,  316,  317,  318,  319,  320,
  321,    0,  322,  323,    0,  324,  325,  326,  327,  328,
  329,  330,  307,    0,  331,  332,  333,  334,    0,    0,
  335,    0,    0,    0,  336,  337,  338,  339,  340,    0,
  341,  342,  343,  344,  345,  346,  347,    0,  348,    0,
  579,    0,    0,    0,    0,    0,    0,    0,    0,  349,
    0,    0,    0,    0,    0,    0,    0,  308,  309,  310,
  350,  351,  352,  353,    0,    0,    0,    0,  311,    0,
    0,  312,  313,  314,  315,  316,  317,  318,  319,  320,
  321,    0,  322,  323,    0,  324,  325,  326,  327,  328,
  329,  330,    0,    0,  331,  332,  333,  334,    0,    0,
  335,  307,    0,    0,  336,  337,  338,  339,  340,    0,
  341,  342,  343,  344,  345,  346,  347,    0,  348,    0,
  644,    0,    0,    0,    0,    0,    0,    0,    0,  349,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  350,  351,  352,  353,    0,    0,  308,  309,  310,    0,
    0,    0,    0,    0,    0,    0,    0,  311,    0,    0,
  312,  313,  314,  315,  316,  317,  318,  319,  320,  321,
    0,  322,  323,    0,  324,  325,  326,  327,  328,  329,
  330,    0,    0,  331,  332,  333,  334,    0,  307,  335,
    0,    0,    0,  336,  337,  338,  339,  340,    0,  341,
  342,  343,  344,  345,  346,  347,    0,  348,    0,  645,
    0,    0,    0,    0,    0,    0,    0,    0,  349,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  350,
  351,  352,  353,  308,  309,  310,    0,    0,    0,    0,
    0,    0,    0,    0,  311,    0,    0,  312,  313,  314,
  315,  316,  317,  318,  319,  320,  321,    0,  322,  323,
    0,  324,  325,  326,  327,  328,  329,  330,  307,    0,
  331,  332,  333,  334,    0,    0,  335,    0,    0,    0,
  336,  337,  338,  339,  340,    0,  341,  342,  343,  344,
  345,  346,  347,    0,  348,    0,  706,    0,    0,    0,
    0,    0,    0,    0,    0,  349,    0,    0,    0,    0,
    0,    0,    0,  308,  309,  310,  350,  351,  352,  353,
    0,    0,    0,    0,  311,    0,    0,  312,  313,  314,
  315,  316,  317,  318,  319,  320,  321,    0,  322,  323,
    0,  324,  325,  326,  327,  328,  329,  330,    0,    0,
  331,  332,  333,  334,    0,    0,  335,  307,    0,    0,
  336,  337,  338,  339,  340,    0,  341,  342,  343,  344,
  345,  346,  347,    0,  348,    0,  745,    0,    0,    0,
    0,    0,    0,    0,    0,  349,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  350,  351,  352,  353,
    0,    0,  308,  309,  310,    0,    0,    0,    0,    0,
    0,    0,    0,  311,    0,    0,  312,  313,  314,  315,
  316,  317,  318,  319,  320,  321,    0,  322,  323,    0,
  324,  325,  326,  327,  328,  329,  330,    0,    0,  331,
  332,  333,  334,    0,  307,  335,    0,    0,    0,  336,
  337,  338,  339,  340,    0,  341,  342,  343,  344,  345,
  346,  347,    0,  348,    0,  188,    0,    0,    0,    0,
    0,    0,    0,    0,  349,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  350,  351,  352,  353,  308,
  309,  310,    0,    0,    0,    0,    0,    0,    0,    0,
  311,    0,    0,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,    0,  322,  323,    0,  324,  325,  326,
  327,  328,  329,  330,  307,    0,  331,  332,  333,  334,
    0,    0,  335,    0,    0,    0,  336,  337,  338,  339,
  340,    0,  341,  342,  343,  344,  345,  346,  347,    0,
  348,    0,  186,    0,    0,    0,    0,    0,    0,    0,
    0,  349,    0,    0,    0,    0,    0,    0,    0,  308,
  309,  310,  350,  351,  352,  353,    0,    0,    0,    0,
  311,    0,    0,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,    0,  322,  323,    0,  324,  325,  326,
  327,  328,  329,  330,    0,    0,  331,  332,  333,  334,
    0,    0,  335,  188,    0,    0,  336,  337,  338,  339,
  340,    0,  341,  342,  343,  344,  345,  346,  347,    0,
  348,    0,  189,    0,    0,    0,    0,    0,    0,    0,
    0,  349,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  350,  351,  352,  353,    0,    0,  188,  188,
  188,    0,    0,    0,    0,    0,    0,    0,    0,  188,
    0,    0,  188,  188,  188,  188,  188,  188,  188,  188,
  188,  188,    0,  188,  188,    0,  188,  188,  188,  188,
  188,  188,  188,    0,    0,  188,  188,  188,  188,    0,
  186,  188,    0,    0,    0,  188,  188,  188,  188,  188,
    0,  188,  188,  188,  188,  188,  188,  188,    0,  188,
    0,  187,    0,    0,    0,    0,    0,    0,    0,    0,
  188,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  188,  188,  188,  188,  186,  186,  186,    0,    0,
    0,    0,    0,    0,    0,    0,  186,    0,    0,  186,
  186,  186,  186,  186,  186,  186,  186,  186,  186,    0,
  186,  186,    0,  186,  186,  186,  186,  186,  186,  186,
  189,    0,  186,  186,  186,  186,    0,    0,  186,   32,
    0,    0,  186,  186,  186,  186,  186,    0,  186,  186,
  186,  186,  186,  186,  186,    0,  186,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  694,  186,    0,    0,
   33,    0,    0,    0,    0,  189,  189,  189,  186,  186,
  186,  186,    0,    0,    0,   32,  189,    0,    0,  189,
  189,  189,  189,  189,  189,  189,  189,  189,  189,    0,
  189,  189,   31,  189,  189,  189,  189,  189,  189,  189,
   32,    0,  189,  189,  189,  189,   33,    0,  189,  187,
    0,    0,  189,  189,  189,  189,  189,    0,  189,  189,
  189,  189,  189,  189,  189,    0,  189,   32,    0,    0,
    0,   33,    0,    0,    0,    0,    0,  189,   31,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  189,  189,
  189,  189,    0,    0,  187,  187,  187,    0,   33,    0,
    0,    0,    0,   31,   32,  187,    0,    0,  187,  187,
  187,  187,  187,  187,  187,  187,  187,  187,    0,  187,
  187,    0,  187,  187,  187,  187,  187,  187,  187,    0,
   31,  187,  187,  187,  187,   33,    0,  187,   32,    0,
    0,  187,  187,  187,  187,  187,    0,  187,  187,  187,
  187,  187,  187,  187,    0,  187,    0,    0,    0,    0,
    0,    0,    0,    0,   32,    0,  187,   31,    0,   33,
    0,    0,    0,   21,    0,    0,    0,  187,  187,  187,
  187,    0,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   33,    0,    0,    0,    0,
    0,   31,    0,   32,    0,    0,    0,    0,    0,   68,
   69,    0,   70,   71,   72,   73,   74,   75,    0,   21,
    0,    0,    0,    0,    0,    0,    0,   31,   22,    0,
    0,    0,    0,  693,   33,    0,   23,   24,   25,   26,
   27,    0,    0,    0,  149,    0,   32,    0,    0,    0,
    0,    0,   32,   22,    0,    0,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,   31,    0,    0,    0,
    0,   21,    0,    0,    0,   32,    0,   33,    0,    0,
   22,   32,  424,   33,    0,    0,    0,    0,   23,   24,
   25,   26,   27,    0,    0,    0,    0,    0,    0,   28,
    0,    0,    0,   32,   29,   30,   33,    0,   21,   31,
   32,  152,   33,    0,    0,   31,    0,   22,    0,    0,
    0,    0,  166,    0,    0,   23,   24,   25,   26,   27,
    0,    0,    0,    0,   33,    0,   32,  167,   31,    0,
    0,   33,   21,  749,   31,    0,   59,    0,    0,   32,
    0,   22,    0,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,    0,    0,   31,   33,   21,    0,
    0,  110,    0,   31,    0,    0,    0,   22,   32,    0,
   33,    0,    0,    0,    0,   23,   24,   25,   26,   27,
    0,    0,    0,    0,    0,    0,   38,    0,    0,   31,
  118,  119,  120,  257,  121,  122,  123,   21,  124,   33,
    0,    0,   31,    0,    0,    0,   22,  258,    0,  259,
    0,    0,  127,    0,   23,   24,   25,   26,   27,  128,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   31,    0,    0,    0,    0,    0,    0,    0,    0,
  149,  150,    0,    0,    0,    0,  149,  150,    0,   22,
  151,    0,    0,    0,    0,   22,  151,   23,   24,   25,
   26,   27,    0,   23,   24,   25,   26,   27,    0,  149,
  268,    0,    0,  453,  454,   21,    0,    0,   22,  269,
    0,    0,    0,    0,   22,    0,   23,   24,   25,   26,
   27,    0,   23,   24,   25,   26,   27,   21,    0,    0,
    0,    0,    0,    0,   21,    0,   22,  733,    0,    0,
    0,    0,    0,   22,   23,   24,   25,   26,   27,    0,
    0,   23,   24,   25,   26,   27,    0,    0,    0,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,   21,  693,    0,    0,   23,   24,   25,
   26,   27,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,  275,  275,
    0,    0,  149,    0,    0,    0,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,  455,  456,  457,  458,    0,    0,
    0,    0,    0,  459,  460,  461,  462,  463,  464,  465,
  466,  467,  468,  275,  275,  275,    0,    0,    0,    0,
    0,    0,    0,    0,  275,    0,    0,  275,  275,  275,
  275,  275,  275,  275,  275,  275,  275,    0,  275,  275,
    0,  275,  275,  275,  275,  275,  275,  275,  241,  241,
  275,  275,  275,  275,    0,    0,  275,    0,    0,    0,
  275,  275,  275,  275,  275,    0,  275,  275,  275,  275,
  275,  275,  275,    0,  275,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  275,    0,    0,    0,    0,
    0,    0,    0,  241,  241,  241,  275,  275,  275,  275,
    0,    0,    0,    0,  241,    0,    0,  241,  241,  241,
  241,  241,  241,  241,  241,  241,  241,    0,  241,  241,
    0,  241,  241,  241,  241,  241,  241,  241,  244,  244,
  241,  241,  241,  241,    0,    0,  241,    0,    0,    0,
  241,  241,  241,  241,  241,    0,  241,  241,  241,  241,
  241,  241,  241,    0,  241,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  241,    0,    0,    0,    0,
    0,    0,    0,  244,  244,  244,  241,  241,  241,  241,
    0,    0,    0,    0,  244,    0,    0,  244,  244,  244,
  244,  244,  244,  244,  244,  244,  244,    0,  244,  244,
    0,  244,  244,  244,  244,  244,  244,  244,  249,  249,
  244,  244,  244,  244,    0,    0,  244,    0,    0,    0,
  244,  244,  244,  244,  244,    0,  244,  244,  244,  244,
  244,  244,  244,    0,  244,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  244,    0,    0,    0,    0,
    0,    0,    0,  249,  249,  249,  244,  244,  244,  244,
    0,    0,    0,    0,  249,    0,    0,  249,  249,  249,
  249,  249,  249,  249,  249,  249,  249,    0,  249,  249,
    0,  249,  249,  249,  249,  249,  249,  249,  263,  263,
  249,  249,  249,  249,    0,    0,  249,    0,    0,    0,
  249,  249,  249,  249,  249,    0,  249,  249,  249,  249,
  249,  249,  249,    0,  249,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  249,    0,    0,    0,    0,
    0,    0,    0,  263,  263,  263,  249,  249,  249,  249,
    0,    0,    0,    0,  263,    0,    0,  263,  263,  263,
  263,  263,  263,  263,  263,  263,  263,    0,  263,  263,
    0,  263,  263,  263,  263,  263,  263,  263,  245,  245,
  263,  263,  263,  263,    0,    0,  263,    0,    0,    0,
  263,  263,  263,  263,  263,    0,  263,  263,  263,  263,
  263,  263,  263,    0,  263,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  263,    0,    0,    0,    0,
    0,    0,    0,  245,  245,  245,  263,  263,  263,  263,
    0,    0,    0,    0,  245,    0,    0,  245,  245,  245,
  245,  245,  245,  245,  245,  245,  245,    0,  245,  245,
    0,  245,  245,  245,  245,  245,  245,  245,  246,  246,
  245,  245,  245,  245,    0,    0,  245,    0,    0,    0,
  245,  245,  245,  245,  245,    0,  245,  245,  245,  245,
  245,  245,  245,    0,  245,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  245,    0,    0,    0,    0,
    0,    0,    0,  246,  246,  246,  245,  245,  245,  245,
    0,    0,    0,    0,  246,    0,    0,  246,  246,  246,
  246,  246,  246,  246,  246,  246,  246,    0,  246,  246,
    0,  246,  246,  246,  246,  246,  246,  246,  247,  247,
  246,  246,  246,  246,    0,    0,  246,    0,    0,    0,
  246,  246,  246,  246,  246,    0,  246,  246,  246,  246,
  246,  246,  246,    0,  246,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  246,    0,    0,    0,    0,
    0,    0,    0,  247,  247,  247,  246,  246,  246,  246,
    0,    0,    0,    0,  247,    0,    0,  247,  247,  247,
  247,  247,  247,  247,  247,  247,  247,    0,  247,  247,
    0,  247,  247,  247,  247,  247,  247,  247,  264,  264,
  247,  247,  247,  247,    0,    0,  247,    0,    0,    0,
  247,  247,  247,  247,  247,    0,  247,  247,  247,  247,
  247,  247,  247,    0,  247,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  247,    0,    0,    0,    0,
    0,    0,    0,  264,  264,  264,  247,  247,  247,  247,
    0,    0,    0,    0,  264,    0,    0,  264,  264,  264,
  264,  264,  264,  264,  264,  264,  264,    0,  264,  264,
    0,  264,  264,  264,  264,  264,  264,  264,  248,  248,
  264,  264,  264,  264,    0,    0,  264,    0,    0,    0,
  264,  264,  264,  264,  264,    0,  264,  264,  264,  264,
  264,  264,  264,    0,  264,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  264,    0,    0,    0,    0,
    0,    0,    0,  248,  248,  248,  264,  264,  264,  264,
    0,    0,    0,    0,  248,    0,    0,  248,  248,  248,
  248,  248,  248,  248,  248,  248,  248,    0,  248,  248,
    0,  248,  248,  248,  248,  248,  248,  248,  307,    0,
  248,  248,  248,  248,    0,    0,  248,    0,    0,    0,
  248,  248,  248,  248,  248,    0,  248,  248,  248,  248,
  248,  248,  248,    0,  248,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  248,    0,    0,    0,    0,
    0,    0,    0,  308,  309,  310,  248,  248,  248,  248,
    0,    0,    0,    0,  311,    0,    0,  312,  313,  314,
  315,  316,  317,  318,  319,  320,  321,    0,  322,  323,
    0,  324,  325,  326,  327,  328,  329,  330,  192,    0,
  331,  332,  333,  334,    0,    0,  335,    0,    0,    0,
  336,  337,  338,  339,  340,    0,  341,  342,  343,  344,
  345,  346,  347,    0,  348,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  349,    0,    0,    0,    0,
    0,    0,    0,  192,  192,  192,  350,  351,  352,  353,
    0,    0,    0,    0,  192,    0,    0,  192,  192,  192,
  192,  192,  192,  192,  192,  192,  192,    0,  192,  192,
    0,  192,  192,  192,  192,  192,  192,  192,  193,    0,
  192,  192,  192,  192,    0,    0,  192,    0,    0,    0,
  192,  192,  192,  192,  192,    0,  192,  192,  192,  192,
  192,  192,  192,    0,  192,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  192,    0,    0,    0,    0,
    0,    0,    0,  193,  193,  193,  192,  192,  192,  192,
    0,    0,    0,    0,  193,    0,    0,  193,  193,  193,
  193,  193,  193,  193,  193,  193,  193,    0,  193,  193,
    0,  193,  193,  193,  193,  193,  193,  193,  194,    0,
  193,  193,  193,  193,    0,    0,  193,    0,    0,    0,
  193,  193,  193,  193,  193,    0,  193,  193,  193,  193,
  193,  193,  193,    0,  193,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  193,    0,    0,    0,    0,
    0,    0,    0,  194,  194,  194,  193,  193,  193,  193,
    0,    0,    0,    0,  194,    0,    0,  194,  194,  194,
  194,  194,  194,  194,  194,  194,  194,    0,  194,  194,
    0,  194,  194,  194,  194,  194,  194,  194,  195,    0,
  194,  194,  194,  194,    0,    0,  194,    0,    0,    0,
  194,  194,  194,  194,  194,    0,  194,  194,  194,  194,
  194,  194,  194,    0,  194,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  194,    0,    0,    0,    0,
    0,    0,    0,  195,  195,  195,  194,  194,  194,  194,
    0,    0,    0,    0,  195,    0,    0,  195,  195,  195,
  195,  195,  195,  195,  195,  195,  195,    0,  195,  195,
    0,  195,  195,  195,  195,  195,  195,  195,    0,    0,
  195,  195,  195,  195,    0,    0,  195,    0,    0,    0,
  195,  195,  195,  195,  195,    0,  195,  195,  195,  195,
  195,  195,  195,    0,  195,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  195,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  195,  195,  195,  195,
  311,    0,    0,  312,  313,  314,  315,  316,  317,  318,
  319,  320,  321,    0,  322,  323,    0,  324,  325,  326,
  327,  328,  329,  330,    0,    0,  331,  332,  333,  334,
    0,    0,  335,    0,    0,    0,  336,  337,  338,  339,
  340,    0,  341,  342,  343,  344,  345,  346,  347,    0,
  348,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  349,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  350,  351,  352,  353,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  302,  123,    6,    7,   40,  280,  207,   33,  209,
  284,  123,   44,   60,   97,  123,   36,  123,   44,   40,
  539,   28,  123,  123,   31,   28,  123,   33,  123,   36,
  304,   38,  123,   36,  352,   38,   44,  687,   40,  239,
  506,   40,   40,   50,  123,   40,   40,  123,  123,   40,
  133,  134,  135,  123,  274,   44,   44,   64,   78,  354,
  260,   93,  276,   44,   40,  148,  280,   60,   44,  364,
   77,   78,  367,   80,   77,   78,  123,  727,   42,   42,
   87,   44,   89,  116,  290,  291,  360,  382,   42,  309,
   61,  365,  611,  100,  101,  614,  103,  266,   91,  125,
  107,  108,  302,  309,  107,   42,  270,  271,   62,  383,
  384,  194,  195,  196,   61,  581,  285,  125,   41,   41,
   40,   51,   44,   53,   44,   41,   61,   91,   44,  162,
  123,  309,  139,  216,  141,  174,  125,  125,  260,   82,
  173,  180,   61,   40,  125,  178,  179,  797,  362,  125,
   61,  562,  563,  274,  565,   98,   93,  475,   88,   61,
   90,  104,  274,   60,  247,   42,  274,   44,  274,  369,
  177,    0,  115,  274,  274,  294,  295,  274,   44,  274,
   42,  257,   44,  274,  223,  218,  280,  482,  309,  222,
  257,  224,  231,  488,   91,  274,   62,  236,  237,  274,
  207,  309,   42,   60,  274,  125,  501,  502,  228,  483,
  484,   61,  309,   41,    0,  313,   44,  315,  309,   41,
  163,  164,   44,  230,  322,  308,  123,  310,  639,  260,
  641,  642,  271,  272,   91,  242,  275,  272,    0,   41,
  287,  260,   44,  276,  710,  328,  329,  330,  274,  274,
  333,  272,  277,  336,  337,  338,  339,  340,  341,  342,
  343,  344,  345,  346,  347,   42,  123,   44,  274,   41,
  272,  264,   44,  272,  272,  570,  571,  272,  272,   40,
  273,  272,  748,  226,  291,  292,  293,  370,  281,  282,
  283,  284,  285,  274,  705,   40,   60,   41,  572,   41,
   44,  267,   44,   86,  311,  312,  313,  314,  315,  316,
  317,  318,  319,  320,  321,  322,  323,  324,  325,  326,
  327,  290,  291,  362,  331,  332,   41,   91,  335,   44,
  363,   42,  352,   44,  257,  258,  259,   40,  261,  262,
  263,  424,  265,  350,  351,  352,   40,   41,  123,  352,
   44,  646,  267,  276,   40,  138,  279,  140,  123,  123,
  257,  258,  259,  286,  261,  262,  263,  123,  265,    0,
  363,  154,   40,  647,   60,  272,  273,  394,  363,   40,
   41,  398,  279,   44,  401,   41,  123,  394,   44,  286,
   42,  398,   44,  410,  401,  290,  291,  404,   41,  294,
  295,   44,   40,  410,  411,   91,  413,  264,   41,    0,
   40,   44,  707,   58,   41,  422,  273,   44,   41,   42,
  203,  428,   40,  206,  281,  282,  283,  284,  285,   42,
   44,   44,   41,   42,   41,   42,   58,  123,   58,  268,
  269,   41,   42,  272,  273,  452,  275,  486,  309,  532,
  533,  534,   41,   42,  474,  475,   62,  758,  541,  288,
  289,   40,  469,   61,  361,   40,  473,  474,  475,  362,
  473,  474,  475,   41,   42,  134,  135,   44,  375,  376,
  377,  310,  268,  269,  362,   44,  272,  273,  345,  275,
  380,  381,  382,  383,  384,  385,  386,  387,  388,  389,
  264,  274,  288,  289,  274,  260,  268,  269,  257,  273,
  272,  273,  370,  275,  370,  370,  362,  281,  282,  283,
  284,  285,   44,  257,  310,  362,  288,  289,  362,   44,
  537,   42,  539,  616,  734,  542,  543,  544,  545,  546,
  547,  548,  549,  550,  551,  552,  553,  567,  310,   60,
   40,  634,  274,  274,  362,  257,  257,  564,  758,  566,
  567,  564,  257,  566,  567,    0,  362,   61,  403,  317,
  274,  257,  258,  259,  257,  261,  262,  263,  257,  265,
   91,  345,   52,   53,  290,  291,  272,  273,  294,  295,
  296,  297,  298,  279,  677,  678,  273,   42,   44,  370,
  286,   44,   44,  636,  611,  388,   44,  614,   44,  266,
  393,  370,  123,  396,  397,   44,  399,  400,  370,  402,
  403,   44,  405,  406,  407,  408,  409,    0,  635,  412,
  370,  414,  415,  416,  417,  370,  643,  268,  269,  109,
  643,  272,  273,  370,  275,  652,  116,  370,   44,  732,
  370,   40,   41,  370,  370,   44,  370,  288,  289,  698,
  637,  638,  370,  640,   44,   44,  699,   44,   44,  702,
   44,  362,   44,   62,   44,  361,   44,  268,  269,  310,
   44,  272,  273,   44,  275,   44,  693,  157,  471,  375,
  376,  377,  162,   44,  777,  778,  779,  288,  289,   44,
  739,   44,  741,   44,   93,  712,  789,  300,  301,  742,
  303,  304,  305,  306,  307,  308,  736,   44,   44,  310,
   44,   44,   40,   44,  701,  508,  703,  704,   91,  512,
  769,  738,  515,   44,   44,  518,  125,  125,   44,   44,
   40,  524,  525,   44,  527,   44,  257,  258,  259,   44,
  261,  262,  263,   44,  265,  225,  257,   44,   44,   44,
   44,  272,  273,   44,   44,   44,   91,  744,  279,  257,
   44,  554,  555,  556,  781,  286,  362,   44,  362,  786,
  787,  788,   44,   44,   44,   60,  362,   44,  362,  300,
  301,  257,  303,  304,  305,  306,  307,  308,   40,   93,
  583,  257,  585,  586,  257,  588,  589,  277,  591,  592,
  363,  594,  595,  596,  597,  598,   91,   40,  601,   40,
  603,  604,  605,  606,   40,    0,   40,  370,  370,  612,
   40,  370,   44,  268,  269,    0,   40,  272,  273,   10,
  275,   54,   18,  191,  148,  147,  211,  207,  123,   64,
  361,  695,  230,  288,  289,  300,  301,  177,  303,  304,
  305,  306,  307,  308,  375,  376,  377,  709,  385,  356,
  356,  654,  260,  738,  657,  310,  369,  660,   -1,  633,
  663,  712,   -1,   -1,  273,  274,  669,  670,  276,  672,
   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,  688,  689,  690,   -1,   -1,
   40,   -1,  695,   -1,   -1,  288,  289,   -1,   -1,  307,
  308,   -1,   -1,  311,  312,  313,  314,  315,  316,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,  310,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,  736,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   -1,  361,  273,  274,   -1,  365,  366,  367,  368,
  369,  370,  371,  372,  373,  374,  375,  376,  377,  264,
  379,   -1,   -1,   -1,    0,   -1,   -1,   -1,  273,   40,
   -1,  390,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
   -1,  361,   -1,  273,  274,  365,  366,  367,  368,  369,
  345,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,   -1,   -1,    0,   -1,   -1,   -1,   -1,   40,   -1,
  390,   -1,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,  401,  402,  403,  404,   -1,   -1,   -1,  318,  319,
  320,   -1,   -1,  288,  289,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,  310,  346,  347,  348,  349,
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
  390,   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,
   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,  320,
   -1,   -1,  288,  289,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,  310,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,  273,  274,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   40,  379,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
  401,  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,
   -1,  288,  289,   -1,   -1,   -1,   42,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,  310,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   -1,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   40,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,  273,  274,  355,  356,  357,  358,   -1,   -1,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   40,  379,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,  401,
  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,
  273,  274,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   40,  379,   -1,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,  390,  265,
   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,  401,
  402,  403,  404,  279,   -1,  318,  319,  320,   -1,   -1,
  286,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
   -1,   -1,  355,  356,  357,  358,   -1,   -1,  361,  273,
  274,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   40,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  361,   -1,   -1,  401,  402,
  403,  404,   -1,   -1,  318,  319,  320,   -1,   -1,  375,
  376,  377,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,
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
   41,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,
  404,   -1,   -1,   -1,   60,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   42,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   91,   -1,  361,  273,  274,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,  125,   -1,   -1,   -1,   -1,  390,  123,   -1,   42,
   -1,   44,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,   -1,  318,  319,  320,   -1,   60,   -1,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   91,   -1,
  355,  356,  357,  358,   -1,   42,  361,   44,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   60,  379,   -1,   -1,  125,   -1,   -1,
  123,   -1,  268,  269,   -1,  390,  272,  273,   -1,  275,
   -1,   42,   -1,   -1,   -1,   -1,  401,  402,  403,  404,
   -1,   -1,  288,  289,   91,   -1,  257,  258,  259,   60,
  261,  262,  263,   -1,  265,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  310,  276,  260,   -1,  279,   -1,
   -1,   -1,   -1,   -1,  276,  286,  123,  279,  264,   60,
   91,   -1,  276,   -1,  286,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   91,  265,  123,  307,  308,   -1,   -1,  311,  312,  313,
  314,  315,  316,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,   -1,  286,   -1,   60,  257,  258,  259,   -1,  261,
  262,  263,  123,  265,  330,  331,   -1,   -1,   -1,   -1,
   -1,   -1,  260,   -1,  257,  258,  259,  279,  261,  262,
  263,   -1,  265,   -1,  286,   91,   -1,   41,  276,  272,
  273,   -1,   -1,   -1,   41,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  286,   -1,   -1,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   60,   -1,   -1,   -1,  123,   -1,  307,
  308,   -1,   -1,  311,  312,  313,  314,  315,  316,   -1,
  257,  258,  259,   -1,  261,  262,  263,   91,  265,   -1,
   -1,   -1,   -1,   -1,   91,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   60,   -1,   -1,   -1,  223,   -1,  286,
   60,   -1,   -1,   -1,   -1,  231,  257,  258,  259,  123,
  261,  262,  263,   -1,  265,   -1,  123,   -1,  361,   -1,
   -1,  272,  273,   -1,   91,   -1,   -1,   -1,  279,   -1,
   -1,   91,  375,  376,  377,  286,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,  271,  272,   -1,   -1,  275,
   -1,  272,  273,   -1,   -1,   -1,  123,   -1,  279,   -1,
   -1,   -1,   -1,  123,   -1,  286,   -1,   -1,   -1,  295,
   -1,  125,   -1,   -1,  361,   -1,   -1,   -1,   -1,  300,
  301,   -1,  303,  304,  305,  306,  307,  308,  375,  376,
  377,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,
  361,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,   -1,   -1,   -1,  375,  376,  377,   -1,   -1,   -1,
   -1,  357,   -1,  359,   -1,   -1,  362,  260,   -1,   -1,
  361,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,
  264,  377,   -1,  276,  375,  376,  377,  264,  125,  273,
   -1,   -1,   -1,   -1,  278,   -1,  273,  281,  282,  283,
  284,  285,   -1,   -1,  281,  282,  283,  284,  285,  293,
   -1,   -1,   -1,   -1,  307,  308,  293,   -1,  311,  312,
  313,  314,  315,  316,   -1,  361,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,  273,   -1,   -1,  375,
  376,  377,   -1,  273,  281,  282,  283,  284,  285,  273,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  302,   -1,   -1,  125,  299,
  300,  301,   -1,  303,  304,  305,  306,  307,  308,   -1,
   -1,   -1,  478,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  486,   -1,   -1,   -1,  318,  319,  320,   -1,   -1,   -1,
  496,   -1,  498,   -1,   -1,  329,   -1,  503,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
   -1,  355,  356,  357,  358,   -1,  273,  361,   -1,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,
  404,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,  273,   -1,  355,  356,
  357,  358,   -1,   -1,  361,   -1,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  318,  319,  320,  401,  402,  403,  404,   -1,   -1,
   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,
  337,  338,  339,  340,  341,   -1,  343,  344,   -1,  346,
  347,  348,  349,  350,  351,  352,   -1,   -1,  355,  356,
  357,  358,   -1,   -1,  361,  273,   -1,   -1,  365,  366,
  367,  368,  369,   -1,  371,  372,  373,  374,  375,  376,
  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  401,  402,  403,  404,   -1,   -1,
  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
  338,  339,  340,  341,   -1,  343,  344,   -1,  346,  347,
  348,  349,  350,  351,  352,   -1,   -1,  355,  356,  357,
  358,   -1,  273,  361,   -1,   -1,   -1,  365,  366,  367,
  368,  369,   -1,  371,  372,  373,  374,  375,  376,  377,
   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  401,  402,  403,  404,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,  273,   -1,  355,  356,  357,  358,   -1,   -1,
  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,  319,  320,
  401,  402,  403,  404,   -1,   -1,   -1,   -1,  329,   -1,
   -1,  332,  333,  334,  335,  336,  337,  338,  339,  340,
  341,   -1,  343,  344,   -1,  346,  347,  348,  349,  350,
  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,   -1,
  361,  273,   -1,   -1,  365,  366,  367,  368,  369,   -1,
  371,  372,  373,  374,  375,  376,  377,   -1,  379,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  401,  402,  403,  404,   -1,   -1,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,  338,  339,  340,  341,
   -1,  343,  344,   -1,  346,  347,  348,  349,  350,  351,
  352,   -1,   -1,  355,  356,  357,  358,   -1,  273,  361,
   -1,   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,
  372,  373,  374,  375,  376,  377,   -1,  379,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  390,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,
  402,  403,  404,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   -1,   -1,
  355,  356,  357,  358,   -1,   -1,  361,  273,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,
   -1,   -1,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,  338,  339,  340,  341,   -1,  343,  344,   -1,
  346,  347,  348,  349,  350,  351,  352,   -1,   -1,  355,
  356,  357,  358,   -1,  273,  361,   -1,   -1,   -1,  365,
  366,  367,  368,  369,   -1,  371,  372,  373,  374,  375,
  376,  377,   -1,  379,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,  273,   -1,  355,  356,  357,  358,
   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  318,
  319,  320,  401,  402,  403,  404,   -1,   -1,   -1,   -1,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   -1,  361,  273,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,  402,  403,  404,   -1,   -1,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,  338,  339,
  340,  341,   -1,  343,  344,   -1,  346,  347,  348,  349,
  350,  351,  352,   -1,   -1,  355,  356,  357,  358,   -1,
  273,  361,   -1,   -1,   -1,  365,  366,  367,  368,  369,
   -1,  371,  372,  373,  374,  375,  376,  377,   -1,  379,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  401,  402,  403,  404,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,   -1,  346,  347,  348,  349,  350,  351,  352,
  273,   -1,  355,  356,  357,  358,   -1,   -1,  361,   60,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   41,  390,   -1,   -1,
   91,   -1,   -1,   -1,   -1,  318,  319,  320,  401,  402,
  403,  404,   -1,   -1,   -1,   60,  329,   -1,   -1,  332,
  333,  334,  335,  336,  337,  338,  339,  340,  341,   -1,
  343,  344,  123,  346,  347,  348,  349,  350,  351,  352,
   60,   -1,  355,  356,  357,  358,   91,   -1,  361,  273,
   -1,   -1,  365,  366,  367,  368,  369,   -1,  371,  372,
  373,  374,  375,  376,  377,   -1,  379,   60,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,   -1,  390,  123,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,
  403,  404,   -1,   -1,  318,  319,  320,   -1,   91,   -1,
   -1,   -1,   -1,  123,   60,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,  338,  339,  340,  341,   -1,  343,
  344,   -1,  346,  347,  348,  349,  350,  351,  352,   -1,
  123,  355,  356,  357,  358,   91,   -1,  361,   60,   -1,
   -1,  365,  366,  367,  368,  369,   -1,  371,  372,  373,
  374,  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   60,   -1,  390,  123,   -1,   91,
   -1,   -1,   -1,  264,   -1,   -1,   -1,  401,  402,  403,
  404,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   91,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   60,   -1,   -1,   -1,   -1,   -1,  300,
  301,   -1,  303,  304,  305,  306,  307,  308,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,  273,   -1,
   -1,   -1,   -1,  278,   91,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,  264,   -1,   60,   -1,   -1,   -1,
   -1,   -1,   60,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  123,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   60,   -1,   91,   -1,   -1,
  273,   60,  302,   91,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,   -1,   -1,   -1,   -1,   -1,   -1,  292,
   -1,   -1,   -1,   60,  297,  298,   91,   -1,  264,  123,
   60,  125,   91,   -1,   -1,  123,   -1,  273,   -1,   -1,
   -1,   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   91,   -1,   60,  293,  123,   -1,
   -1,   91,  264,   93,  123,   -1,  125,   -1,   -1,   60,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,   -1,   -1,  123,   91,  264,   -1,
   -1,  293,   -1,  123,   -1,   -1,   -1,  273,   60,   -1,
   91,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
   -1,   -1,   -1,   -1,   -1,   -1,  292,   -1,   -1,  123,
  257,  258,  259,  260,  261,  262,  263,  264,  265,   91,
   -1,   -1,  123,   -1,   -1,   -1,  273,  274,   -1,  276,
   -1,   -1,  279,   -1,  281,  282,  283,  284,  285,  286,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,  265,   -1,   -1,   -1,   -1,  264,  265,   -1,  273,
  274,   -1,   -1,   -1,   -1,  273,  274,  281,  282,  283,
  284,  285,   -1,  281,  282,  283,  284,  285,   -1,  264,
  265,   -1,   -1,  261,  262,  264,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,  273,   -1,  281,  282,  283,  284,
  285,   -1,  281,  282,  283,  284,  285,  264,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,  273,  281,  282,  283,  284,  285,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,  264,  278,   -1,   -1,  281,  282,  283,
  284,  285,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,  273,  274,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  382,  383,  384,  385,   -1,   -1,
   -1,   -1,   -1,  391,  392,  393,  394,  395,  396,  397,
  398,  399,  400,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,  274,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,  273,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  318,  319,  320,  401,  402,  403,  404,
   -1,   -1,   -1,   -1,  329,   -1,   -1,  332,  333,  334,
  335,  336,  337,  338,  339,  340,  341,   -1,  343,  344,
   -1,  346,  347,  348,  349,  350,  351,  352,   -1,   -1,
  355,  356,  357,  358,   -1,   -1,  361,   -1,   -1,   -1,
  365,  366,  367,  368,  369,   -1,  371,  372,  373,  374,
  375,  376,  377,   -1,  379,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  390,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  401,  402,  403,  404,
  329,   -1,   -1,  332,  333,  334,  335,  336,  337,  338,
  339,  340,  341,   -1,  343,  344,   -1,  346,  347,  348,
  349,  350,  351,  352,   -1,   -1,  355,  356,  357,  358,
   -1,   -1,  361,   -1,   -1,   -1,  365,  366,  367,  368,
  369,   -1,  371,  372,  373,  374,  375,  376,  377,   -1,
  379,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  390,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  401,  402,  403,  404,
  };

#line 1084 "Repil/IR/IR.jay"

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
