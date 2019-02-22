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
//t    "instruction : TAIL CALL return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer function_args",
//t    "instruction : TAIL CALL calling_convention parameter_attribute return_type function_pointer function_args",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FCMP fcmp_condition type value ',' value",
//t    "instruction : FDIV type value ',' value",
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
    "SPECULATABLE","SSP","UWTABLE","ARGMEMONLY","RET","BR","SWITCH",
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
#line 59 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 63 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 67 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 71 "Repil/IR/IR.jay"
  {
        module.IdentifiedStructures[(Symbol)yyVals[-3+yyTop]] = (StructureType)yyVals[0+yyTop];
    }
  break;
case 8:
#line 75 "Repil/IR/IR.jay"
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
#line 95 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 14:
#line 99 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 15:
  case_15();
  break;
case 16:
#line 108 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 17:
  case_17();
  break;
case 18:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 20:
#line 128 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 21:
#line 132 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-6+yyTop], (LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], isPrivate: (bool)yyVals[-4+yyTop], isExternal: false, isConstant: (bool)yyVals[-2+yyTop]);
    }
  break;
case 22:
#line 136 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: (bool)yyVals[-8+yyTop], isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 23:
#line 140 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: (bool)yyVals[-4+yyTop]);
    }
  break;
case 24:
#line 144 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-10+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-8+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 25:
#line 148 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], isPrivate: false, isExternal: (bool)yyVals[-7+yyTop], isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 26:
#line 152 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 27:
#line 153 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 28:
#line 157 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 29:
#line 158 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 30:
#line 162 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 31:
  case_31();
  break;
case 32:
  case_32();
  break;
case 33:
#line 179 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 34:
#line 180 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 35:
#line 181 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 36:
#line 182 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 37:
#line 183 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 38:
#line 187 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 39:
#line 191 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 40:
#line 198 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 41:
#line 202 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 42:
#line 209 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 43:
#line 213 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 44:
#line 217 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 45:
#line 221 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 46:
#line 225 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 64:
#line 258 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 65:
#line 262 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 66:
#line 266 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 67:
#line 273 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 277 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 70:
#line 282 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 73:
#line 288 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 74:
#line 289 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 75:
#line 290 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 76:
#line 291 "Repil/IR/IR.jay"
  { yyVal = FloatType.X86_FP80; }
  break;
case 77:
#line 295 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 78:
#line 299 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 79:
#line 303 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 80:
#line 307 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 81:
#line 311 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 315 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 322 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 326 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 334 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 87:
#line 341 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 88:
#line 345 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 89:
#line 349 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 90:
#line 353 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 357 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 361 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 365 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 369 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 95:
#line 373 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 377 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 97:
#line 381 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 98:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 99:
#line 392 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 396 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 101:
#line 400 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 102:
#line 401 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 103:
#line 408 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 412 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 423 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 107:
#line 427 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 108:
#line 431 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 110:
#line 439 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 111:
#line 443 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 112:
#line 444 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 113:
#line 445 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 114:
#line 446 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 115:
#line 447 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 116:
#line 448 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 117:
#line 449 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 118:
#line 450 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.Returned; }
  break;
case 124:
#line 468 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 125:
#line 469 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 126:
#line 470 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 127:
#line 471 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 128:
#line 472 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 129:
#line 473 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 130:
#line 474 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 131:
#line 475 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 132:
#line 476 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 133:
#line 477 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 134:
#line 481 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 135:
#line 482 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 136:
#line 483 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 137:
#line 484 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 138:
#line 485 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 139:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 140:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 141:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 142:
#line 489 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 143:
#line 490 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 144:
#line 491 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 145:
#line 492 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 146:
#line 493 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 147:
#line 494 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 148:
#line 495 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 149:
#line 496 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 151:
#line 501 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 152:
#line 502 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 153:
#line 506 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 510 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 514 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 518 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 522 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 526 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 530 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 161:
#line 538 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 162:
#line 539 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 163:
#line 540 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 164:
#line 541 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 165:
#line 542 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 166:
#line 543 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 167:
#line 544 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 168:
#line 545 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 169:
#line 546 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 170:
#line 553 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 564 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 173:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 578 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 582 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 589 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 600 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 604 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 622 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 626 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 184:
#line 630 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 634 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 186:
#line 641 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 645 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 190:
#line 660 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 664 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 193:
#line 672 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 194:
#line 673 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 195:
#line 680 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 684 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 691 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 198:
#line 695 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 199:
#line 699 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 200:
#line 703 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 201:
#line 707 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 202:
#line 711 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 203:
#line 715 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 205:
#line 720 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 210:
#line 737 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 741 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 747 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 213:
#line 754 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 758 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 765 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 786 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 790 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 794 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 798 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 225:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 230:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 231:
#line 829 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 233:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 234:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 235:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 236:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 237:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 238:
#line 857 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 239:
#line 861 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 240:
#line 865 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 885 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 889 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 893 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 905 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 909 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 913 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 255:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 256:
#line 929 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 257:
#line 933 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 258:
#line 937 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 941 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 945 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 949 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 953 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 957 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 961 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 965 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 969 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 973 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 977 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 981 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 985 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 989 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: false);
    }
  break;
case 272:
#line 993 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop], isVolatile: true);
    }
  break;
case 273:
#line 997 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 1001 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 275:
#line 1005 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 276:
#line 1009 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 277:
#line 1013 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 278:
#line 1017 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 279:
#line 1021 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 280:
#line 1025 "Repil/IR/IR.jay"
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
#line 77 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_10()
#line 82 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_11()
#line 87 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_15()
#line 101 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_17()
#line 110 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_31()
#line 167 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_32()
#line 172 "Repil/IR/IR.jay"
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
    4,    4,    4,    4,    4,    4,    4,    5,    5,    5,
   27,   27,   32,   32,   33,   33,   33,   33,   34,   34,
   31,   31,   31,   31,   31,   31,   31,   31,   14,   14,
   28,   28,   35,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   36,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   40,
   18,   18,   18,   18,   18,   18,   18,   18,   18,   41,
   21,   21,   42,   39,   39,   43,   44,   38,   38,   29,
   29,   45,   45,   45,   45,   46,   46,   48,   48,   48,
   48,   50,   51,   51,   52,   52,   53,   53,   53,   53,
   53,   53,   53,   54,   54,   19,   19,   55,   55,   56,
   56,   57,   58,   58,   59,   60,   60,   61,   61,   30,
   47,   47,   47,   47,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
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
   11,    9,   10,   11,   11,   13,   12,    5,    6,    6,
    3,    2,    1,    3,    1,    2,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    6,    9,    6,    6,    3,    3,    3,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    2,
    2,    1,    2,    1,    3,    2,    1,    1,    3,    1,
    2,    2,    3,    1,    2,    1,    2,    1,    2,    3,
    4,    1,    3,    2,    1,    3,    2,    3,    3,    3,
    2,    4,    5,    1,    1,    1,    3,    1,    1,    1,
    3,    5,    1,    2,    3,    1,    2,    1,    1,    1,
    2,    7,    2,    7,    5,    6,    5,    5,    5,    6,
    4,    4,    5,    6,    5,    6,    5,    6,    7,    4,
    5,    6,    5,    5,    4,    4,    4,    4,    5,    6,
    7,    6,    6,    7,    8,    5,    6,    5,    5,    6,
    3,    4,    5,    7,    4,    5,    6,    6,    4,    5,
    7,    8,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   80,   73,   74,   75,   76,   72,    0,   29,   28,
    0,    0,    0,   71,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  119,  120,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   64,    0,
    0,    0,    0,    0,    0,   79,  220,    0,    0,    0,
    0,    0,    0,    0,    5,    6,    0,    0,    0,    0,
    0,    8,    0,    7,    0,    0,    0,    0,    0,   65,
    0,    0,    0,    0,    0,  116,  117,  118,  111,  112,
  114,  113,  115,    0,    0,    0,    0,   86,   77,    0,
    0,   83,    0,    0,    0,  163,  164,  162,  165,  166,
  167,  161,  152,  151,  169,  168,    0,    0,    0,    0,
    0,    0,    0,    0,  150,    0,    0,    0,    0,    0,
    0,    0,   31,    0,    0,    0,   49,   48,   13,    0,
    0,   42,   47,    0,    0,    0,    0,    0,    0,    0,
    0,  107,  108,  102,    0,    0,  103,  123,    0,    0,
  121,   78,    0,    0,    0,    0,    0,    0,   62,   54,
   52,   53,   55,   56,   57,   58,    0,   50,    0,    0,
    0,    0,  174,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   15,    0,    0,    0,   43,   14,    0,
  171,    0,   81,   66,   82,    0,    0,    0,    0,  109,
    0,  101,    0,    0,    0,    0,  122,   84,    0,    0,
    0,    0,   12,   51,    0,    0,    0,    0,  159,    0,
  157,  158,    0,    0,    0,    0,    0,    0,   35,    0,
   33,   36,   37,   32,   17,   16,   46,   45,   44,    0,
    0,    0,    0,    0,    0,  110,  104,    0,    0,   40,
    0,    0,   59,  209,  208,    0,  206,    0,    0,    0,
    0,  175,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  180,    0,
    0,  186,    0,    0,    0,    0,    0,    0,   41,    0,
   63,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   23,    0,   39,    0,    0,    0,    0,    0,  223,    0,
    0,  221,    0,  218,  219,    0,    0,  216,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  124,  125,  126,  127,  128,  129,  130,  131,  132,  133,
    0,  134,  135,  146,  147,  148,  149,  137,  139,  140,
  141,  142,  138,  136,  144,  145,  143,    0,    0,    0,
    0,    0,    0,   92,  181,    0,  187,    0,    0,    0,
    0,    0,    0,   87,    0,   88,  207,    0,  156,  153,
  155,    0,    0,    0,    0,   38,   90,    0,    0,    0,
  170,    0,    0,    0,    0,  217,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  210,    0,  192,    0,    0,    0,    0,    0,
    0,   93,    0,    0,    0,   89,    0,    0,    0,   91,
   94,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  240,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   95,    0,    0,  177,    0,  178,    0,    0,  225,
    0,  241,  273,    0,  249,  259,    0,  244,  276,  263,
  243,  278,  270,  266,    0,    0,  256,    0,  229,  228,
  258,  279,    0,    0,  227,    0,  160,  173,    0,    0,
    0,    0,    0,    0,    0,    0,  211,    0,    0,  194,
    0,    0,  195,    0,  233,    0,    0,    0,    0,   97,
    0,  154,    0,    0,    0,    0,    0,  213,  226,  274,
  260,  267,  257,  230,  253,  268,    0,    0,    0,    0,
    0,    0,  252,  242,    0,    0,    0,    0,  197,    0,
  193,    0,  234,    0,  238,    0,   96,  179,  222,  176,
    0,  224,  214,    0,  254,    0,  271,    0,  212,  264,
    0,  205,  199,  204,  200,  198,  196,  239,  215,  255,
  272,  202,    0,  203,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  187,  150,  142,   50,
  151,  535,  226,   51,   52,   36,  143,  135,  276,  152,
  615,  188,   61,   62,  111,  112,  107,  170,  338,   69,
  220,  166,  167,  221,  171,  431,  448,  616,  194,  648,
  372,  584,  676,  617,  339,  340,  341,  342,  343,  536,
  606,  662,  663,  725,  277,  532,  533,  677,  678,  377,
  378,
  };
  protected static readonly short [] yySindex = {          952,
  -47,   72,  -42,   17,   37, 3391, 3457, -271,    0,  952,
    0,    0,    0,    0, -152,   57,   83,  295, -143,  -23,
    0,    0,    0,    0,    0,    0,    0, 3596,    0,    0,
 3534, -117, -102,    0,  107, 3363,  -36, 3596,  -35,  101,
    0,    0,  -87,  -75,    0,    0,    0,    0,    0, 3596,
  119,  116, -108,  -17,  140,  -22,   69,  -13,    0,  107,
  -28,  167,  -46, 3596,    4,    0,    0,   -9, 3226,  192,
 1990,   -5,  192,  142,    0,    0, 1779, 3596,  119, 3596,
  119,    0,  166,    0, -141,  199,  185, 3479,  192,    0,
 3596, 3596,  -20, 3596,  192,    0,    0,    0,    0,    0,
    0,    0,    0,   -4, 3596, 1906, -189,    0,    0,  107,
  112,    0,  192, -189, 1625,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  -41,  287,  309,  321,
 3601, 3601, 3601,  324,    0, 1779, 3596, 1779, 3596,  322,
  326,  149,    0, -141, 3501,    0,    0,    0,    0,    2,
 1779,    0,    0,  116,  107,   19,  320,   14, -189,  192,
   -3,    0,    0,    0,  180,  183,    0,    0,   79, -219,
    0,    0, 3452,   79,   79,   79,  331,  364,    0,    0,
    0,    0,    0,    0,    0,    0, -113,    0,  369, 3601,
 3601, 3601,    0,    5,   59,   -2,   77,  389, 1779,  397,
 1749, 3297,  171,    0, -141,  239,    3,    0,    0, 3507,
    0,   79,    0,    0,    0,   79, -103,  116,  192,    0,
  574,    0, 3429, -100,  178, -110,    0,    0,   79,   79,
  184, 1209,    0,    0, 3596,   84,   85,   88,    0, 3601,
    0,    0,  201,  111,  431,  128,  129,  447,    0,  452,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  -97,
 -219, 4168, -106,   79,  116,    0,    0, 4168, -105,    0,
  220, 4168,    0,    0,    0,  275,    0,   71, 3596, 3596,
 3596,    0,  222,  240,  137,  242,  243,  143,  532, 4168,
  -78,  -69,  440, 3601, -257, 3601, 3221, 3596, 3221, 3596,
 3221, 3596, 3596, 3596, 3596, 3596, 3596, 3221,  260, 1793,
 3596, 3596, 3596, 3601, 3601, 3601, 3596, 3333, 3358,   81,
 3601, 3601, 3601, 3601, 3601, 3601, 3601, 3601, 3601, 3601,
 3601, 1176, 3532, 3596, 3596, 3363,  103, 1879,    0, 4168,
  222,    0,  222, 4168,  -83,   79, 1967, 4168,    0, 2046,
    0, 1209, 3601,  374,  376,  380,  229,  222,  251,  222,
    0,  254,    0,  300, 2134, 4168, 4168, 4560,    0,  258,
   74,    0,  493,    0,    0, 1779, 3221,    0, 1779, 1779,
 3221, 1779, 1779, 3221, 1779, 1779, 1779, 1779, 1779, 1779,
 1779, 3221, 3596, 1779, 3596, 1779, 1779, 1779, 1779,  494,
  496,  502,  186, 3596,  246, 3601,  503, 3596,  273,  181,
  182,  187,  191,  202,  208,  209,  213,  215,  225,  226,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3596,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3596,  -21, 1779,
 1570, 3226, 3363,    0,    0,  222,    0,  229,  229, 2222,
 4168,  -55, -219,    0, 2301,    0,    0,  505,    0,    0,
    0,  229,  222,  229,  222,    0,    0, 2389, 2477,  222,
    0,  508,  302,  509, 1779,    0,  526,  553, 1779,  554,
  556, 1779,  557,  558,  559,  562,  563,  564,  568, 1779,
 1779,  575, 1779,  577,  578,  580,  582, 3601, 3601, 3601,
  257,  306, 3596,  584, 3596,  313, 3601, 3596, 3596, 3596,
 3596, 3596, 3596, 3596, 3596, 3596, 3596, 3596, 1779, 1779,
   74,  585,    0,  586,    0,  587, 1570, 3596, 1570, 3226,
  229,    0, 2556, 4168,  -54,    0, 3601,  229,  229,    0,
    0,  229,  302,  540,   74,  588,   74,   74,  590,   74,
   74,  592,   74,   74,   74,   74,   74,   74,   74,  595,
  598,   74,  606,   74,   74,   74,   74,    0,  614,  620,
  394, 3596, 1779,  623, 3596,  628, 3601,  631,  107,  107,
  107,  107,  107,  107,  107,  107,  107,  107,  107,  633,
  634,  635,  589, 3601, 3304,   79,  587, 1570,  587, 1570,
 3596,    0, 2644, 4168,    0,  328,    0,  639, 3596,    0,
   74,    0,    0,   74,    0,    0,   74,    0,    0,    0,
    0,    0,    0,    0,   74,   74,    0,   74,    0,    0,
    0,    0, 3601, 3601,    0,  640,    0,    0,  325,  641,
  327,  647, 3601,   74,   74,   74,    0,  648, 3547,    0,
 1695,  329,    0,   79,    0,  587,   79,  587, 1570,    0,
 2732,    0, 3601,  302,   -1,  649, 3561,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  334,  441,  336,  442,
 3601,  650,    0,    0,  615, 3601,  660,  866,    0, 1826,
    0, 3574,    0,   79,    0,  587,    0,    0,    0,    0,
  302,    0,    0,  456,    0,  460,    0,  650,    0,    0,
 1602,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  333,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  719,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 1649,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   27,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  680,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  170,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  680,    0,  680,    0,    0,
    0,    0,    0,    0,    0,  495,    0,    0,    0,    0,
  680,    0,    0,    0,   33,  680,    0,  680,    0,    0,
    0,    0,    0,    0,  269,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  122, 1834, 3590,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  680,    0,
  680,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  335,    0,    0,    0,    0,    0,    0,    0,  151,  286,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  393,    0,    0,    0,    0,  355,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  680,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2811,    0, 4247,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  680,  680,  680,  401,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  680,    0,    0,  680,  680,
    0,  680,  680,    0,  680,  680,  680,  680,  680,  680,
  680,    0,    0,  680,    0,  680,  680,  680,  680,    0,
    0,    0,  680,    0,  680,    0,    0,    0,  680,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  680,  680,
    0,    0,    0,    0,    0, 2899,    0, 2987, 4326,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  437,  455,  482,    0,    0,    0,    0,    0, 4405,
    0,    0,    0,    0,  680,    0,    0,    0,  680,    0,
    0,  680,    0,    0,    0,    0,    0,    0,    0,  680,
  680,    0,  680,    0,    0,    0,    0,    0,    0,    0,
    0,  680,    0,    0,    0,  680,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  680,  680,
    0, 3615,    0,    0,    0,    0,    0,    0,    0,    0,
 3066,    0,    0,    0,    0,    0,    0,  510,  617,    0,
    0, 4484,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  680,    0,    0,    0,    0,    0,  583,  662,
  741,  830,  918,  997, 1076, 1165, 1253, 1332, 1411,    0,
    0,    0,    0,    0,    0, 3694,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  680,    0,    0, 3773,    0,    0, 3852,    0,    0,    0,
    0,    0,    0,    0,  680,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3931,    0,    0,    0,    0,  359,  680,    0,    0,
    0,    0,    0, 4010,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4089,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  712,  675,    0,    0,    0,    0,  593,  596,   97,
   -6,   80, 1702,  607,    0,  713,  527, -173, -286,    0,
  -81,  546,  673,   76,    0,  566,  245,  -80, -206, -328,
  -67,    0,  519,   87, -137,    0,    0, -638,  298,    0,
 -387, -439,    0,   73, -290,    0,  405,  409,  384,  -44,
 -384,    0,   51,    0,  404,    0,  155,    0,   82, -182,
  -25,
  };
  protected static readonly short [] yyTable = {            35,
   35,  105,  364,   71,   71,   64,  153,  452,  370,   57,
   87,  233,  272,   15,  692,   91,  344,  348,   18,  262,
   66,   35,  268,   91,   60,  290,   71,  371,  252,   35,
   71,   35,  227,  176,   71,   71,   71,   40,  227,  461,
   66,  240,   83,   77,  366,  210,  210,  455,  240,  193,
  193,  193,  718,  367,  225,   66,  455,   60,  275,  455,
   66,  347,   35,  153,  110,  350,   69,  544,  614,  531,
   67,  136,   69,  138,  455,  586,   68,   19,  217,  227,
  213,   37,   39,  365,  155,  156,  227,  158,  224,  168,
  242,  227,  227,  229,  230,  554,   90,   20,   35,  165,
   45,   46,  240,   58,  157,   31,  215,   42,  236,  237,
  238,   68,   66,   72,  353,  275,  381,   43,  384,  168,
  241,   98,  227,  227,  540,  392,  209,  256,  259,  239,
  199,  260,  201,  132,  140,  261,   54,  460,  141,   63,
   32,  465,  646,   44,  104,  650,  177,   78,   66,   80,
   99,   67,  172,  266,   65,  173,  134,   68,  282,  478,
  479,   74,  178,  271,  133,  618,  110,  271,  271,  455,
  225,   33,   75,  225,  455,  137,  225,  139,  275,   85,
  161,   45,   46,  345,   76,   47,   48,  455,  455,  204,
  225,   88,  205,  179,  180,  271,  131,  181,  182,  183,
  184,  185,  186,   31,  271,  168,   71,  227,  168,   69,
   85,  168,  369,   85,  373,  198,  165,  200,  271,  271,
   92,   66,  665,  222,  667,  168,  223,   66,  278,  511,
  211,  106,  400,  401,  402,   70,   73,  407,  144,  410,
  411,  412,  413,  414,  415,  416,  417,  418,  419,  420,
   55,   86,  455,   56,  543,  116,  117,  118,   89,  119,
  120,  121,   95,  122,  115,  463,  113,  160,  219,   82,
   94,  468,  354,  355,  356,  208,  208,  125,  245,  255,
  248,  703,  205,  705,  126,  100,  709,   66,   64,  513,
  376,  379,  380,  382,  383,  385,  386,  387,  388,  389,
  390,  391,  394,  396,  397,  398,  399,  145,   69,  105,
  403,  405,  105,  409,   66,  351,  517,  114,  352,   32,
  189,  728,  455,  729,  514,  227,  190,  449,  450,   35,
  116,  117,  118,  154,  119,  120,  121,  613,  122,  159,
  476,   16,   17,  352,   21,  123,  124,   66,  191,  582,
   33,  486,  125,   22,   66,  486,  587,  174,  486,  126,
  192,   23,   24,   25,   26,   27,  486,  197,  672,  701,
  485,  673,  702,  734,  489,  106,  352,  492,  106,  202,
  455,  214,   31,  203,  538,  500,  501,  168,  503,   98,
   98,  231,   21,   98,   98,   34,   98,  512,   34,  201,
   18,  516,  201,  232,  218,   45,   46,  671,  235,   98,
   98,  451,   47,   48,  469,   66,  470,   66,   99,   99,
  471,   66,   99,   99,  529,   99,  578,  579,  580,  195,
  196,   98,  244,  127,  733,  588,   19,  243,   99,   99,
  246,  530,  408,  273,  253,   35,   35,  128,  129,  130,
  482,  270,  279,  280,   20,  484,  281,  283,  487,  488,
   99,  490,  491,  265,  493,  494,  495,  496,  497,  498,
  499,  284,  611,  502,  285,  504,  505,  506,  507,   96,
   97,   25,   98,   99,  100,  101,  102,  103,  286,  287,
  288,  289,  607,  349,  609,  225,  358,  359,  360,  361,
  368,  710,  271,  362,  453,  652,  583,  473,  583,   22,
  475,  589,  590,  591,  592,  593,  594,  595,  596,  597,
  598,  599,  658,   21,  724,  664,  227,  537,  539,  534,
  481,   35,   22,   35,   70,  172,  483,  508,  172,  509,
   23,   24,   25,   26,   27,  510,  515,  275,  547,  518,
  519,  553,  555,  100,  100,  520,  172,  100,  100,  521,
  100,  685,  686,  666,  556,  668,  227,  370,  559,  557,
  522,  562,  363,  100,  100,  583,  523,  524,  583,  570,
  571,  525,  573,  526,   45,   46,  704,  172,   47,   48,
   49,   29,   30,  527,  528,  100,  558,  560,  661,  561,
  563,  564,  565,  393,   35,  566,  567,  568,  600,  601,
  602,  569,  675,  608,  720,  610,   24,  581,  572,  172,
  574,  575,   69,  576,  706,  577,  605,  585,  603,  604,
  619,  621,  266,  624,  620,  627,  622,  623,  635,  625,
  626,  636,  628,  629,  630,  631,  632,  633,  634,  638,
  645,  637,  698,  639,  640,  641,  642,  643,   79,   81,
   21,   21,  647,  644,   21,   21,  649,   21,   18,   18,
  675,  651,   18,   18,  653,   18,  654,  655,  656,  531,
   21,   21,  674,  687,  689,  688,  669,  690,   18,   18,
  691,  696,  711,  673,  714,  661,  716,  715,  717,  721,
  679,   69,   21,  680,   19,   19,  681,  719,   19,   19,
   18,   19,  730,  169,  682,  683,  731,  684,    1,   69,
  175,   41,   20,   20,   19,   19,   20,   20,   84,   20,
   53,  254,  234,  693,  694,  695,   93,  207,  228,  206,
  699,  267,   20,   20,  456,  708,   19,  700,  457,   25,
   25,  480,  727,   25,   25,  467,   25,  657,  713,    0,
  212,    0,    0,    0,   20,  216,    0,  172,  172,   25,
   25,    0,    0,    0,    0,    0,    0,   22,   22,  726,
   69,   22,   22,    0,   22,    0,    0,    0,  116,  117,
  118,   25,  119,  120,  121,    0,  122,   22,   22,    0,
    0,    0,    0,    0,    0,    0,    0,  274,    0,    0,
  125,  172,  172,  172,    0,    0,    0,  126,    0,   22,
    0,    0,  172,    0,  264,  172,  172,  172,  172,  172,
  172,  172,  172,  172,  172,    0,  172,  172,    0,  172,
  172,  172,  172,  172,  172,  172,    0,    0,  172,  172,
  172,    0,    0,    0,  172,  275,  275,    0,  172,  172,
  172,  172,  172,  172,  172,  172,  172,  172,  172,   69,
  172,  346,  172,   96,   97,    0,   98,   99,  100,  101,
  102,  103,    0,  172,   24,   24,    0,    0,   24,   24,
    0,   24,    0,    0,  172,  172,  172,  172,    0,  275,
  275,  275,    0,    0,   24,   24,    0,   66,    0,    0,
  275,    0,    0,  275,  275,  275,  275,  275,  275,  275,
  275,  275,  275,    0,  275,  275,   24,  275,  275,  275,
  275,  275,  275,  275,  280,  280,  275,  275,  275,    0,
    0,    0,  275,    0,    0,    0,  275,  275,  275,  275,
  275,    0,  275,  275,  275,  275,  275,   69,  275,    0,
  275,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  275,    0,    0,    0,    0,    0,    0,  280,  280,
  280,    0,  275,  275,  275,  275,    0,    0,    0,  280,
    0,    0,  280,  280,  280,  280,  280,  280,  280,  280,
  280,  280,    0,  280,  280,    0,  280,  280,  280,  280,
  280,  280,  280,  265,  265,  280,  280,  280,    0,    0,
    0,  280,    0,    0,    0,  280,  280,  280,  280,  280,
    0,  280,  280,  280,  280,  280,   69,  280,    0,  280,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  280,    0,    0,    0,    0,    0,    0,  265,  265,  265,
    0,  280,  280,  280,  280,    0,    0,    0,  265,    0,
    0,  265,  265,  265,  265,  265,  265,  265,  265,  265,
  265,    0,  265,  265,    0,  265,  265,  265,  265,  265,
  265,  265,    0,    0,  265,  265,  265,    0,    0,    0,
  265,    0,  248,  248,  265,  265,  265,  265,  265,    0,
  265,  265,  265,  265,  265,   69,  265,    0,  265,    0,
    0,    0,  116,  117,  118,    0,  119,  120,  121,  265,
  122,    0,    0,    0,    0,    0,    0,  722,  723,    0,
  265,  265,  265,  265,  125,    0,  248,  248,  248,    0,
    0,  126,    0,    0,    0,    0,    0,  248,    0,    0,
  248,  248,  248,  248,  248,  248,  248,  248,  248,  248,
    0,  248,  248,    0,  248,  248,  248,  248,  248,  248,
  248,    0,    0,  248,  248,  248,    0,    0,    0,  248,
  245,  245,    0,  248,  248,  248,  248,  248,    0,  248,
  248,  248,  248,  248,   69,  248,    0,  248,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  248,    1,
    2,    0,    0,    3,    4,    0,    5,    0,    0,  248,
  248,  248,  248,    0,  245,  245,  245,    0,    0,    6,
    7,    0,    0,    0,    0,  245,    0,    0,  245,  245,
  245,  245,  245,  245,  245,  245,  245,  245,    0,  245,
  245,    8,  245,  245,  245,  245,  245,  245,  245,  246,
  246,  245,  245,  245,    0,    0,    0,  245,    0,    0,
    0,  245,  245,  245,  245,  245,    0,  245,  245,  245,
  245,  245,   69,  245,    0,  245,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  245,    0,    0,    0,
    0,    0,    0,  246,  246,  246,    0,  245,  245,  245,
  245,    0,    0,    0,  246,    0,    0,  246,  246,  246,
  246,  246,  246,  246,  246,  246,  246,    0,  246,  246,
    0,  246,  246,  246,  246,  246,  246,  246,  247,  247,
  246,  246,  246,    0,    0,    0,  246,    0,    0,    0,
  246,  246,  246,  246,  246,    0,  246,  246,  246,  246,
  246,   69,  246,    0,  246,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  246,    0,    0,    0,    0,
    0,    0,  247,  247,  247,    0,  246,  246,  246,  246,
    0,    0,    0,  247,    0,    0,  247,  247,  247,  247,
  247,  247,  247,  247,  247,  247,    0,  247,  247,    0,
  247,  247,  247,  247,  247,  247,  247,    0,    0,  247,
  247,  247,    0,    0,    0,  247,    0,  277,  277,  247,
  247,  247,  247,  247,    0,  247,  247,  247,  247,  247,
   69,  247,    0,  247,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  247,  116,  117,  118,    0,  119,
  120,  121,    0,  122,    0,  247,  247,  247,  247,    0,
    0,  277,  277,  277,  274,    0,    0,  125,    0,    0,
    0,    0,  277,    0,  126,  277,  277,  277,  277,  277,
  277,  277,  277,  277,  277,    0,  277,  277,    0,  277,
  277,  277,  277,  277,  277,  277,    0,    0,  277,  277,
  277,    0,    0,    0,  277,  269,  269,    0,  277,  277,
  277,  277,  277,    0,  277,  277,  277,  277,  277,    0,
  277,    0,  277,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  277,  421,  422,  423,  424,  425,  426,
  427,  428,  429,  430,  277,  277,  277,  277,    0,  269,
  269,  269,    0,    0,    0,    0,    0,    0,    0,    0,
  269,    0,    0,  269,  269,  269,  269,  269,  269,  269,
  269,  269,  269,    0,  269,  269,    0,  269,  269,  269,
  269,  269,  269,  269,  262,  262,  269,  269,  269,   71,
    0,    0,  269,    0,    0,    0,  269,  269,  269,  269,
  269,    0,  269,  269,  269,  269,  269,    0,  269,  132,
  269,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  269,  732,    0,    0,    0,    0,    0,  262,  262,
  262,    0,  269,  269,  269,  269,    0,    0,    0,  262,
  133,    0,  262,  262,  262,  262,  262,  262,  262,  262,
  262,  262,    0,  262,  262,    0,  262,  262,  262,  262,
  262,  262,  262,  231,  231,  262,  262,  262,   69,    0,
    0,  262,  131,    0,    0,  262,  262,  262,  262,  262,
    0,  262,  262,  262,  262,  262,    0,  262,   69,  262,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  262,    0,    0,    0,    0,    0,    0,  231,  231,  231,
    0,  262,  262,  262,  262,    0,   66,    0,  231,   69,
    0,  231,  231,  231,  231,  231,  231,  231,  231,  231,
  231,    0,  231,  231,  132,  231,  231,  231,  231,  231,
  231,  231,    0,    0,  231,  231,  231,    0,    0,    0,
  231,   69,    0,    0,  231,  231,  231,  231,  231,    0,
  231,  231,  231,  231,  231,  133,  231,    0,  231,    0,
   66,    0,  247,    0,    0,    0,    0,    0,    0,  231,
    0,    0,    0,    0,    0,    0,    0,    0,  132,    0,
  231,  231,  231,  231,    0,    0,    0,  131,    0,    0,
   66,    0,    0,    0,    0,    0,  116,  117,  118,    0,
  119,  120,  121,    0,  122,    0,    0,    0,  132,  133,
    0,  123,  124,    0,    0,    0,    0,    0,  125,    0,
    0,    0,   32,    0,    0,  126,    0,    0,  116,  117,
  118,    0,  119,  120,  121,    0,  122,    0,    0,  133,
    0,  131,    0,    0,    0,    0,    0,  274,    0,    0,
  125,    0,    0,   33,  177,  132,    0,  126,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  178,  131,    0,    0,    0,   69,   69,   69,    0,   69,
   69,   69,    0,   69,    0,   31,  133,    0,  263,    0,
   69,   69,    0,    0,    0,  269,    0,   69,    0,  127,
    0,  179,  180,    0,   69,  181,  182,  183,  184,  185,
  186,    0,    0,  128,  129,  130,  164,    0,  131,    0,
    0,  116,  117,  118,    0,  119,  120,  121,   60,  122,
    0,  291,  292,    0,    0,   32,  123,  124,    0,    0,
    0,    0,    0,  125,    0,    0,    0,    0,    0,    0,
  126,    0,    0,    0,  357,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   96,   97,   33,   98,   99,  100,
  101,  102,  103,  454,    0,  116,  117,  118,   69,  119,
  120,  121,    0,  122,    0,    0,    0,    0,    0,    0,
  123,  124,   69,   69,   69,    0,    0,  125,   31,    0,
  109,    0,    0,    0,  126,  116,  117,  118,    0,  119,
  120,  121,  458,  122,  459,    0,  462,    0,    0,   32,
  123,  124,    0,    0,  127,    0,   21,  125,    0,  472,
    0,  474,    0,    0,  126,   22,    0,    0,  128,  129,
  130,    0,    0,   23,   24,   25,   26,   27,    0,    0,
   33,    0,  116,  117,  118,    0,  119,  120,  121,    0,
  122,  464,    0,   60,    0,    0,    0,  123,  124,    0,
    0,    0,    0,    0,  125,    0,    0,    0,  127,   60,
    0,  126,   31,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  128,  129,  130,   96,   97,    0,   98,   99,
  100,  101,  102,  103,    0,    0,  395,    0,  127,    0,
   60,   60,    0,    0,   60,   60,   60,   60,   60,   60,
    0,  293,  128,  129,  130,    0,    0,  541,    0,    0,
    0,    0,    0,    0,  545,    0,    0,    0,    0,   21,
  466,    0,    0,    0,  548,    0,  549,    0,   22,    0,
    0,  552,    0,  162,    0,  127,   23,   24,   25,   26,
   27,    0,    0,    0,    0,  294,  295,  296,  163,  128,
  129,  130,    0,    0,    0,    0,  297,    0,    0,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,    0,
  308,  309,    0,  310,  311,  312,  313,  314,  315,  316,
    0,    0,  317,  318,  319,    0,    0,    0,  320,  293,
    0,    0,  321,  322,  323,  324,  325,    0,  326,  327,
  328,  329,  330,   21,  331,    0,  332,    0,  477,    0,
    0,    0,   22,    0,    0,    0,    0,  333,    0,    0,
   23,   24,   25,   26,   27,    0,    0,    0,  334,  335,
  336,  337,  108,  294,  295,  296,    0,    0,    0,    0,
    0,    0,    0,    0,  297,    0,    0,  298,  299,  300,
  301,  302,  303,  304,  305,  306,  307,    0,  308,  309,
    0,  310,  311,  312,  313,  314,  315,  316,  293,    0,
  317,  318,  319,    0,    0,    0,  320,    0,    0,    0,
  321,  322,  323,  324,  325,    0,  326,  327,  328,  329,
  330,    0,  331,    0,  332,    0,  542,    0,    0,    0,
    0,    0,    0,    0,    0,  333,    0,    0,    0,    0,
    0,    0,  294,  295,  296,    0,  334,  335,  336,  337,
    0,    0,    0,  297,    0,    0,  298,  299,  300,  301,
  302,  303,  304,  305,  306,  307,    0,  308,  309,    0,
  310,  311,  312,  313,  314,  315,  316,    0,    0,  317,
  318,  319,    0,    0,    0,  320,  293,    0,    0,  321,
  322,  323,  324,  325,    0,  326,  327,  328,  329,  330,
    0,  331,    0,  332,    0,  546,    0,    0,    0,    0,
    0,    0,    0,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  334,  335,  336,  337,    0,
  294,  295,  296,    0,    0,    0,    0,    0,    0,    0,
    0,  297,    0,    0,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  307,    0,  308,  309,    0,  310,  311,
  312,  313,  314,  315,  316,    0,    0,  317,  318,  319,
    0,    0,    0,  320,  293,    0,    0,  321,  322,  323,
  324,  325,    0,  326,  327,  328,  329,  330,    0,  331,
    0,  332,    0,  550,    0,    0,    0,    0,    0,    0,
    0,    0,  333,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  334,  335,  336,  337,    0,  294,  295,
  296,    0,    0,    0,    0,    0,    0,    0,    0,  297,
    0,    0,  298,  299,  300,  301,  302,  303,  304,  305,
  306,  307,    0,  308,  309,    0,  310,  311,  312,  313,
  314,  315,  316,  293,    0,  317,  318,  319,    0,    0,
    0,  320,    0,    0,    0,  321,  322,  323,  324,  325,
    0,  326,  327,  328,  329,  330,    0,  331,    0,  332,
    0,  551,    0,    0,    0,    0,    0,    0,    0,    0,
  333,    0,    0,    0,    0,    0,    0,  294,  295,  296,
    0,  334,  335,  336,  337,    0,    0,    0,  297,    0,
    0,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,    0,  308,  309,    0,  310,  311,  312,  313,  314,
  315,  316,    0,    0,  317,  318,  319,    0,    0,    0,
  320,  293,    0,    0,  321,  322,  323,  324,  325,    0,
  326,  327,  328,  329,  330,    0,  331,    0,  332,    0,
  612,    0,    0,    0,    0,    0,    0,    0,    0,  333,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  334,  335,  336,  337,    0,  294,  295,  296,    0,    0,
    0,    0,    0,    0,    0,    0,  297,    0,    0,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,    0,
  308,  309,    0,  310,  311,  312,  313,  314,  315,  316,
    0,    0,  317,  318,  319,    0,    0,    0,  320,  293,
    0,    0,  321,  322,  323,  324,  325,    0,  326,  327,
  328,  329,  330,    0,  331,    0,  332,    0,  670,    0,
    0,    0,    0,    0,    0,    0,    0,  333,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  334,  335,
  336,  337,    0,  294,  295,  296,    0,    0,    0,    0,
    0,    0,    0,    0,  297,    0,    0,  298,  299,  300,
  301,  302,  303,  304,  305,  306,  307,    0,  308,  309,
    0,  310,  311,  312,  313,  314,  315,  316,  293,    0,
  317,  318,  319,    0,    0,    0,  320,    0,    0,    0,
  321,  322,  323,  324,  325,    0,  326,  327,  328,  329,
  330,    0,  331,    0,  332,    0,  707,    0,    0,    0,
    0,    0,    0,    0,    0,  333,    0,    0,    0,    0,
    0,    0,  294,  295,  296,    0,  334,  335,  336,  337,
    0,    0,    0,  297,    0,    0,  298,  299,  300,  301,
  302,  303,  304,  305,  306,  307,    0,  308,  309,    0,
  310,  311,  312,  313,  314,  315,  316,    0,    0,  317,
  318,  319,    0,    0,    0,  320,  293,    0,    0,  321,
  322,  323,  324,  325,    0,  326,  327,  328,  329,  330,
    0,  331,    0,  332,    0,  184,    0,    0,    0,    0,
    0,    0,    0,    0,  333,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  334,  335,  336,  337,    0,
  294,  295,  296,    0,    0,    0,    0,    0,    0,    0,
    0,  297,    0,    0,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  307,    0,  308,  309,    0,  310,  311,
  312,  313,  314,  315,  316,    0,    0,  317,  318,  319,
    0,    0,    0,  320,  293,    0,    0,  321,  322,  323,
  324,  325,    0,  326,  327,  328,  329,  330,    0,  331,
    0,  332,    0,  182,    0,    0,    0,    0,    0,    0,
    0,    0,  333,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  334,  335,  336,  337,    0,  294,  295,
  296,    0,    0,    0,    0,    0,    0,    0,    0,  297,
    0,    0,  298,  299,  300,  301,  302,  303,  304,  305,
  306,  307,    0,  308,  309,    0,  310,  311,  312,  313,
  314,  315,  316,  184,    0,  317,  318,  319,    0,    0,
    0,  320,    0,    0,    0,  321,  322,  323,  324,  325,
    0,  326,  327,  328,  329,  330,    0,  331,    0,  332,
    0,  185,    0,    0,    0,    0,    0,    0,    0,    0,
  333,    0,    0,    0,    0,    0,    0,  184,  184,  184,
    0,  334,  335,  336,  337,    0,    0,    0,  184,    0,
    0,  184,  184,  184,  184,  184,  184,  184,  184,  184,
  184,    0,  184,  184,    0,  184,  184,  184,  184,  184,
  184,  184,    0,    0,  184,  184,  184,    0,    0,    0,
  184,  182,    0,    0,  184,  184,  184,  184,  184,    0,
  184,  184,  184,  184,  184,    0,  184,    0,  184,    0,
  183,    0,    0,    0,    0,    0,    0,    0,    0,  184,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  184,  184,  184,  184,    0,  182,  182,  182,    0,    0,
    0,    0,    0,    0,    0,    0,  182,    0,    0,  182,
  182,  182,  182,  182,  182,  182,  182,  182,  182,    0,
  182,  182,    0,  182,  182,  182,  182,  182,  182,  182,
    0,    0,  182,  182,  182,    0,    0,    0,  182,  185,
    0,    0,  182,  182,  182,  182,  182,    0,  182,  182,
  182,  182,  182,    0,  182,    0,  182,    0,    0,    0,
   32,    0,    0,    0,    0,   32,    0,  182,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  182,  182,
  182,  182,    0,  185,  185,  185,    0,    0,    0,    0,
    0,   33,    0,    0,  185,    0,   33,  185,  185,  185,
  185,  185,  185,  185,  185,  185,  185,    0,  185,  185,
    0,  185,  185,  185,  185,  185,  185,  185,  183,    0,
  185,  185,  185,   31,  660,    0,  185,    0,   31,    0,
  185,  185,  185,  185,  185,    0,  185,  185,  185,  185,
  185,    0,  185,   32,  185,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  185,    0,    0,    0,    0,
    0,    0,  183,  183,  183,    0,  185,  185,  185,  185,
    0,    0,   32,  183,   33,    0,  183,  183,  183,  183,
  183,  183,  183,  183,  183,  183,    0,  183,  183,    0,
  183,  183,  183,  183,  183,  183,  183,   32,    0,  183,
  183,  183,   32,   33,    0,  183,   31,    0,    0,  183,
  183,  183,  183,  183,    0,  183,  183,  183,  183,  183,
    0,  183,    0,  183,    0,    0,    0,    0,   33,    0,
   32,    0,    0,   33,  183,   31,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  183,  183,  183,  183,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   31,   33,    0,    0,   21,   31,    0,    0,   32,   21,
    0,    0,    0,   22,    0,    0,    0,    0,   22,    0,
    0,   23,   24,   25,   26,   27,   23,   24,   25,   26,
   27,   32,    0,   31,    0,    0,   32,    0,    0,   33,
    0,    0,    0,    0,    0,   96,   97,    0,   98,   99,
  100,  101,  102,  103,    0,    0,    0,    0,   32,    0,
    0,    0,   33,    0,    0,    0,    0,   33,    0,  374,
  375,   31,    0,  116,  117,  118,  249,  119,  120,  121,
   32,  122,    0,    0,    0,    0,   32,   21,    0,   33,
  250,    0,  251,    0,   31,  125,   22,    0,    0,   31,
    0,  659,  126,    0,   23,   24,   25,   26,   27,    0,
    0,   33,    0,   32,    0,    0,   21,   33,    0,    0,
    0,   31,    0,  149,    0,   22,   32,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,    0,    0,
   32,  146,    0,   31,   33,    0,   21,    0,    0,   31,
   22,    0,    0,   32,  404,   22,    0,   33,   23,   24,
   25,   26,   27,   23,   24,   25,   26,   27,    0,    0,
    0,   33,    0,  712,   21,   32,   31,    0,   59,  406,
   32,   67,    0,   22,   33,    0,    0,    0,    0,   31,
    0,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,    0,   28,   31,    0,    0,   33,   29,   30,    0,
    0,   33,   21,    0,    0,    0,   31,    0,    0,    0,
    0,   22,    0,    0,    0,    0,  162,    0,    0,   23,
   24,   25,   26,   27,   61,   21,    0,    0,   31,    0,
   21,  163,    0,   31,   22,    0,    0,    0,    0,   22,
    0,    0,   23,   24,   25,   26,   27,   23,   24,   25,
   26,   27,  146,  147,  108,    0,    0,    0,   38,    0,
    0,   22,  148,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,  146,  147,    0,    0,    0,    0,
  146,  257,    0,   22,  148,    0,    0,    0,    0,   22,
  258,   23,   24,   25,   26,   27,    0,   23,   24,   25,
   26,   27,  432,  433,    0,    0,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
   21,    0,    0,    0,   23,   24,   25,   26,   27,   22,
  697,    0,    0,    0,   21,    0,    0,   23,   24,   25,
   26,   27,    0,   22,    0,    0,    0,   21,    0,    0,
    0,   23,   24,   25,   26,   27,   22,    0,    0,   61,
    0,  659,    0,    0,   23,   24,   25,   26,   27,   21,
    0,    0,    0,    0,  146,   61,    0,    0,   22,    0,
    0,    0,    0,   22,    0,    0,   23,   24,   25,   26,
   27,   23,   24,   25,   26,   27,    0,  261,  261,    0,
    0,    0,    0,    0,    0,    0,   61,   61,    0,    0,
   61,   61,   61,   61,   61,   61,    0,    0,    0,    0,
    0,    0,  434,  435,  436,  437,    0,    0,    0,    0,
    0,  438,  439,  440,  441,  442,  443,  444,  445,  446,
  447,  261,  261,  261,    0,    0,    0,    0,    0,    0,
    0,    0,  261,    0,    0,  261,  261,  261,  261,  261,
  261,  261,  261,  261,  261,    0,  261,  261,    0,  261,
  261,  261,  261,  261,  261,  261,  232,  232,  261,  261,
  261,    0,    0,    0,  261,    0,    0,    0,  261,  261,
  261,  261,  261,    0,  261,  261,  261,  261,  261,    0,
  261,    0,  261,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  261,    0,    0,    0,    0,    0,    0,
  232,  232,  232,    0,  261,  261,  261,  261,    0,    0,
    0,  232,    0,    0,  232,  232,  232,  232,  232,  232,
  232,  232,  232,  232,    0,  232,  232,    0,  232,  232,
  232,  232,  232,  232,  232,  235,  235,  232,  232,  232,
    0,    0,    0,  232,    0,    0,    0,  232,  232,  232,
  232,  232,    0,  232,  232,  232,  232,  232,    0,  232,
    0,  232,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  232,    0,    0,    0,    0,    0,    0,  235,
  235,  235,    0,  232,  232,  232,  232,    0,    0,    0,
  235,    0,    0,  235,  235,  235,  235,  235,  235,  235,
  235,  235,  235,    0,  235,  235,    0,  235,  235,  235,
  235,  235,  235,  235,  237,  237,  235,  235,  235,    0,
    0,    0,  235,    0,    0,    0,  235,  235,  235,  235,
  235,    0,  235,  235,  235,  235,  235,    0,  235,    0,
  235,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  235,    0,    0,    0,    0,    0,    0,  237,  237,
  237,    0,  235,  235,  235,  235,    0,    0,    0,  237,
    0,    0,  237,  237,  237,  237,  237,  237,  237,  237,
  237,  237,    0,  237,  237,    0,  237,  237,  237,  237,
  237,  237,  237,  250,  250,  237,  237,  237,    0,    0,
    0,  237,    0,    0,    0,  237,  237,  237,  237,  237,
    0,  237,  237,  237,  237,  237,    0,  237,    0,  237,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  237,    0,    0,    0,    0,    0,    0,  250,  250,  250,
    0,  237,  237,  237,  237,    0,    0,    0,  250,    0,
    0,  250,  250,  250,  250,  250,  250,  250,  250,  250,
  250,    0,  250,  250,    0,  250,  250,  250,  250,  250,
  250,  250,  236,  236,  250,  250,  250,    0,    0,    0,
  250,    0,    0,    0,  250,  250,  250,  250,  250,    0,
  250,  250,  250,  250,  250,    0,  250,    0,  250,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  250,
    0,    0,    0,    0,    0,    0,  236,  236,  236,    0,
  250,  250,  250,  250,    0,    0,    0,  236,    0,    0,
  236,  236,  236,  236,  236,  236,  236,  236,  236,  236,
    0,  236,  236,    0,  236,  236,  236,  236,  236,  236,
  236,  251,  251,  236,  236,  236,    0,    0,    0,  236,
    0,    0,    0,  236,  236,  236,  236,  236,    0,  236,
  236,  236,  236,  236,    0,  236,    0,  236,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  236,    0,
    0,    0,    0,    0,    0,  251,  251,  251,    0,  236,
  236,  236,  236,    0,    0,    0,  251,    0,    0,  251,
  251,  251,  251,  251,  251,  251,  251,  251,  251,    0,
  251,  251,    0,  251,  251,  251,  251,  251,  251,  251,
  293,    0,  251,  251,  251,    0,    0,    0,  251,    0,
    0,    0,  251,  251,  251,  251,  251,    0,  251,  251,
  251,  251,  251,    0,  251,    0,  251,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  251,    0,    0,
    0,    0,    0,    0,  294,  295,  296,    0,  251,  251,
  251,  251,    0,    0,    0,  297,    0,    0,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,    0,  308,
  309,    0,  310,  311,  312,  313,  314,  315,  316,  188,
    0,  317,  318,  319,    0,    0,    0,  320,    0,    0,
    0,  321,  322,  323,  324,  325,    0,  326,  327,  328,
  329,  330,    0,  331,    0,  332,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  333,    0,    0,    0,
    0,    0,    0,  188,  188,  188,    0,  334,  335,  336,
  337,    0,    0,    0,  188,    0,    0,  188,  188,  188,
  188,  188,  188,  188,  188,  188,  188,    0,  188,  188,
    0,  188,  188,  188,  188,  188,  188,  188,  189,    0,
  188,  188,  188,    0,    0,    0,  188,    0,    0,    0,
  188,  188,  188,  188,  188,    0,  188,  188,  188,  188,
  188,    0,  188,    0,  188,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  188,    0,    0,    0,    0,
    0,    0,  189,  189,  189,    0,  188,  188,  188,  188,
    0,    0,    0,  189,    0,    0,  189,  189,  189,  189,
  189,  189,  189,  189,  189,  189,    0,  189,  189,    0,
  189,  189,  189,  189,  189,  189,  189,  190,    0,  189,
  189,  189,    0,    0,    0,  189,    0,    0,    0,  189,
  189,  189,  189,  189,    0,  189,  189,  189,  189,  189,
    0,  189,    0,  189,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  189,    0,    0,    0,    0,    0,
    0,  190,  190,  190,    0,  189,  189,  189,  189,    0,
    0,    0,  190,    0,    0,  190,  190,  190,  190,  190,
  190,  190,  190,  190,  190,    0,  190,  190,    0,  190,
  190,  190,  190,  190,  190,  190,  191,    0,  190,  190,
  190,    0,    0,    0,  190,    0,    0,    0,  190,  190,
  190,  190,  190,    0,  190,  190,  190,  190,  190,    0,
  190,    0,  190,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  190,    0,    0,    0,    0,    0,    0,
  191,  191,  191,    0,  190,  190,  190,  190,    0,    0,
    0,  191,    0,    0,  191,  191,  191,  191,  191,  191,
  191,  191,  191,  191,    0,  191,  191,    0,  191,  191,
  191,  191,  191,  191,  191,    0,    0,  191,  191,  191,
    0,    0,    0,  191,    0,    0,    0,  191,  191,  191,
  191,  191,    0,  191,  191,  191,  191,  191,    0,  191,
    0,  191,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  191,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  191,  191,  191,  191,  297,    0,    0,
  298,  299,  300,  301,  302,  303,  304,  305,  306,  307,
    0,  308,  309,    0,  310,  311,  312,  313,  314,  315,
  316,    0,    0,  317,  318,  319,    0,    0,    0,  320,
    0,    0,    0,  321,  322,  323,  324,  325,    0,  326,
  327,  328,  329,  330,    0,  331,    0,  332,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  333,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  334,
  335,  336,  337,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   69,  289,   40,   40,  123,   88,  336,  266,   33,
   33,  125,  123,   61,  653,   44,  123,  123,   61,  123,
   42,   28,  123,   44,   31,  123,   40,  285,  202,   36,
   40,   38,  170,  114,   40,   40,   40,  309,  176,  123,
   42,   44,   60,   50,  123,   44,   44,  338,   44,  131,
  132,  133,  691,  123,  274,   42,  347,   64,  232,  350,
   42,  268,   69,  145,   71,  272,   40,  123,  123,   91,
   44,   78,   40,   80,  365,  515,   44,   61,  159,  217,
   62,    6,    7,  290,   91,   92,  224,   94,  169,  309,
   93,  229,  230,  174,  175,  483,  125,   61,  105,  106,
  290,  291,   44,   28,  125,  123,   93,  260,  190,  191,
  192,   36,   42,   38,   44,  289,  299,   61,  301,  309,
   62,    0,  260,  261,  453,  308,  125,  125,  210,  125,
  137,  212,  139,   60,  276,  216,  280,  344,  280,  257,
   60,  348,  582,   61,   69,  585,  260,   51,   42,   53,
    0,  125,   41,  221,  257,   44,   77,  125,  240,  366,
  367,   61,  276,  274,   91,  553,  173,  274,  274,  460,
  274,   91,  260,  274,  465,   79,  274,   81,  352,   40,
  105,  290,  291,  264,  260,  294,  295,  478,  479,   41,
  274,  123,   44,  307,  308,  274,  123,  311,  312,  313,
  314,  315,  316,  123,  274,  309,   40,  345,  309,   40,
   41,  309,  294,   44,  296,  136,  223,  138,  274,  274,
  267,   42,  607,   41,  609,  309,   44,   42,  235,   44,
  151,   40,  314,  315,  316,  272,  272,  319,   40,  321,
  322,  323,  324,  325,  326,  327,  328,  329,  330,  331,
  274,  274,  543,  277,  461,  257,  258,  259,  272,  261,
  262,  263,  272,  265,  123,  346,  272,  272,  272,  287,
  267,  353,  279,  280,  281,  274,  274,  279,  199,   41,
  201,  666,   44,  668,  286,    0,  674,   42,  123,   44,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  308,  309,  310,  311,  312,  313,  123,   40,   41,
  317,  318,   44,  320,   42,   41,   44,   73,   44,   60,
  362,  706,  613,  711,  406,  463,   40,  334,  335,  336,
  257,  258,  259,   89,  261,  262,  263,  544,  265,   95,
   41,  270,  271,   44,  264,  272,  273,   42,   40,   44,
   91,  377,  279,  273,   42,  381,   44,  113,  384,  286,
   40,  281,  282,  283,  284,  285,  392,   44,   41,   41,
  377,   44,   44,   41,  381,   41,   44,  384,   44,   58,
  671,   62,  123,   58,  452,  392,  393,  309,  395,  268,
  269,   61,    0,  272,  273,   41,  275,  404,   44,   41,
    0,  408,   44,   40,  160,  290,  291,  614,   40,  288,
  289,  336,  294,  295,   41,   42,   41,   42,  268,  269,
   41,   42,  272,  273,  431,  275,  508,  509,  510,  132,
  133,  310,   44,  360,  721,  517,    0,  361,  288,  289,
   44,  448,  362,  260,  274,  452,  453,  374,  375,  376,
  371,  274,  369,  369,    0,  376,  369,  257,  379,  380,
  310,  382,  383,  219,  385,  386,  387,  388,  389,  390,
  391,  361,  540,  394,   44,  396,  397,  398,  399,  300,
  301,    0,  303,  304,  305,  306,  307,  308,  361,  361,
   44,   40,  537,  274,  539,  274,  257,  361,  257,  257,
   61,  675,  274,  361,  402,  587,  513,  257,  515,    0,
  257,  518,  519,  520,  521,  522,  523,  524,  525,  526,
  527,  528,  604,  264,  698,  606,  664,  452,  453,  450,
  273,  538,  273,  540,   40,   41,   44,   44,   44,   44,
  281,  282,  283,  284,  285,   44,   44,  721,   44,  369,
  369,   44,   44,  268,  269,  369,   62,  272,  273,  369,
  275,  643,  644,  608,  485,  610,  704,  266,  489,   44,
  369,  492,   41,  288,  289,  582,  369,  369,  585,  500,
  501,  369,  503,  369,  290,  291,  667,   93,  294,  295,
  296,  297,  298,  369,  369,  310,   44,   44,  605,   44,
   44,   44,   44,  344,  611,   44,   44,   44,  529,  530,
  531,   44,  619,  538,  696,  540,    0,  361,   44,  125,
   44,   44,   40,   44,  669,   44,   40,   44,   44,   44,
   91,   44,  700,   44,  555,   44,  557,  558,   44,  560,
  561,   44,  563,  564,  565,  566,  567,  568,  569,   44,
  257,  572,  659,  574,  575,  576,  577,   44,   52,   53,
  268,  269,  583,   44,  272,  273,   44,  275,  268,  269,
  677,   44,  272,  273,   44,  275,   44,   44,   44,   91,
  288,  289,   44,   44,   44,  361,  611,  361,  288,  289,
   44,   44,   44,   44,  361,  702,  361,  257,  257,   40,
  621,   40,  310,  624,  268,  269,  627,   93,  272,  273,
  310,  275,  257,  107,  635,  636,  257,  638,    0,   40,
  114,   10,  268,  269,  288,  289,  272,  273,   54,  275,
   18,  205,  187,  654,  655,  656,   64,  145,  173,  144,
  661,  223,  288,  289,  340,  673,  310,  661,  340,  268,
  269,  368,  702,  272,  273,  352,  275,  603,  677,   -1,
  154,   -1,   -1,   -1,  310,  159,   -1,  273,  274,  288,
  289,   -1,   -1,   -1,   -1,   -1,   -1,  268,  269,  700,
   40,  272,  273,   -1,  275,   -1,   -1,   -1,  257,  258,
  259,  310,  261,  262,  263,   -1,  265,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  276,   -1,   -1,
  279,  317,  318,  319,   -1,   -1,   -1,  286,   -1,  310,
   -1,   -1,  328,   -1,  218,  331,  332,  333,  334,  335,
  336,  337,  338,  339,  340,   -1,  342,  343,   -1,  345,
  346,  347,  348,  349,  350,  351,   -1,   -1,  354,  355,
  356,   -1,   -1,   -1,  360,  273,  274,   -1,  364,  365,
  366,  367,  368,  369,  370,  371,  372,  373,  374,   40,
  376,  265,  378,  300,  301,   -1,  303,  304,  305,  306,
  307,  308,   -1,  389,  268,  269,   -1,   -1,  272,  273,
   -1,  275,   -1,   -1,  400,  401,  402,  403,   -1,  317,
  318,  319,   -1,   -1,  288,  289,   -1,   42,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,  340,   -1,  342,  343,  310,  345,  346,  347,
  348,  349,  350,  351,  273,  274,  354,  355,  356,   -1,
   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,
  368,   -1,  370,  371,  372,  373,  374,   40,  376,   -1,
  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,
  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,   -1,  342,  343,   -1,  345,  346,  347,  348,
  349,  350,  351,  273,  274,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,  368,
   -1,  370,  371,  372,  373,  374,   40,  376,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,   -1,  342,  343,   -1,  345,  346,  347,  348,  349,
  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,   -1,  273,  274,  364,  365,  366,  367,  368,   -1,
  370,  371,  372,  373,  374,   40,  376,   -1,  378,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  389,
  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
  400,  401,  402,  403,  279,   -1,  317,  318,  319,   -1,
   -1,  286,   -1,   -1,   -1,   -1,   -1,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,  340,
   -1,  342,  343,   -1,  345,  346,  347,  348,  349,  350,
  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,
  273,  274,   -1,  364,  365,  366,  367,  368,   -1,  370,
  371,  372,  373,  374,   40,  376,   -1,  378,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,   -1,  400,
  401,  402,  403,   -1,  317,  318,  319,   -1,   -1,  288,
  289,   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,  340,   -1,  342,
  343,  310,  345,  346,  347,  348,  349,  350,  351,  273,
  274,  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,
   -1,  364,  365,  366,  367,  368,   -1,  370,  371,  372,
  373,  374,   40,  376,   -1,  378,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,
   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,
  403,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,   -1,  342,  343,
   -1,  345,  346,  347,  348,  349,  350,  351,  273,  274,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,  367,  368,   -1,  370,  371,  372,  373,
  374,   40,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,   -1,  342,  343,   -1,
  345,  346,  347,  348,  349,  350,  351,   -1,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,  273,  274,  364,
  365,  366,  367,  368,   -1,  370,  371,  372,  373,  374,
   40,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,  400,  401,  402,  403,   -1,
   -1,  317,  318,  319,  276,   -1,   -1,  279,   -1,   -1,
   -1,   -1,  328,   -1,  286,  331,  332,  333,  334,  335,
  336,  337,  338,  339,  340,   -1,  342,  343,   -1,  345,
  346,  347,  348,  349,  350,  351,   -1,   -1,  354,  355,
  356,   -1,   -1,   -1,  360,  273,  274,   -1,  364,  365,
  366,  367,  368,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,  379,  380,  381,  382,  383,  384,
  385,  386,  387,  388,  400,  401,  402,  403,   -1,  317,
  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,  340,   -1,  342,  343,   -1,  345,  346,  347,
  348,  349,  350,  351,  273,  274,  354,  355,  356,   40,
   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,
  368,   -1,  370,  371,  372,  373,  374,   -1,  376,   60,
  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   41,   -1,   -1,   -1,   -1,   -1,  317,  318,
  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,
   91,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,   -1,  342,  343,   -1,  345,  346,  347,  348,
  349,  350,  351,  273,  274,  354,  355,  356,   40,   -1,
   -1,  360,  123,   -1,   -1,  364,  365,  366,  367,  368,
   -1,  370,  371,  372,  373,  374,   -1,  376,   60,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   42,   -1,  328,   91,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,   -1,  342,  343,   60,  345,  346,  347,  348,  349,
  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,  123,   -1,   -1,  364,  365,  366,  367,  368,   -1,
  370,  371,  372,  373,  374,   91,  376,   -1,  378,   -1,
   42,   -1,   44,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
  400,  401,  402,  403,   -1,   -1,   -1,  123,   -1,   -1,
   42,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   60,   91,
   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,
   -1,   -1,   60,   -1,   -1,  286,   -1,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,   91,
   -1,  123,   -1,   -1,   -1,   -1,   -1,  276,   -1,   -1,
  279,   -1,   -1,   91,  260,   60,   -1,  286,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  276,  123,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,  123,   91,   -1,  217,   -1,
  272,  273,   -1,   -1,   -1,  224,   -1,  279,   -1,  360,
   -1,  307,  308,   -1,  286,  311,  312,  313,  314,  315,
  316,   -1,   -1,  374,  375,  376,   41,   -1,  123,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,  125,  265,
   -1,  260,  261,   -1,   -1,   60,  272,  273,   -1,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,   -1,   -1,   -1,  283,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  300,  301,   91,  303,  304,  305,
  306,  307,  308,  125,   -1,  257,  258,  259,  360,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,  374,  375,  376,   -1,   -1,  279,  123,   -1,
   41,   -1,   -1,   -1,  286,  257,  258,  259,   -1,  261,
  262,  263,  341,  265,  343,   -1,  345,   -1,   -1,   60,
  272,  273,   -1,   -1,  360,   -1,  264,  279,   -1,  358,
   -1,  360,   -1,   -1,  286,  273,   -1,   -1,  374,  375,
  376,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   91,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,  125,   -1,  260,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,  360,  276,
   -1,  286,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  374,  375,  376,  300,  301,   -1,  303,  304,
  305,  306,  307,  308,   -1,   -1,  344,   -1,  360,   -1,
  307,  308,   -1,   -1,  311,  312,  313,  314,  315,  316,
   -1,  273,  374,  375,  376,   -1,   -1,  456,   -1,   -1,
   -1,   -1,   -1,   -1,  463,   -1,   -1,   -1,   -1,  264,
  125,   -1,   -1,   -1,  473,   -1,  475,   -1,  273,   -1,
   -1,  480,   -1,  278,   -1,  360,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,  317,  318,  319,  293,  374,
  375,  376,   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,  340,   -1,
  342,  343,   -1,  345,  346,  347,  348,  349,  350,  351,
   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,  273,
   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,  371,
  372,  373,  374,  264,  376,   -1,  378,   -1,  125,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  389,   -1,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,  400,  401,
  402,  403,  293,  317,  318,  319,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,   -1,  342,  343,
   -1,  345,  346,  347,  348,  349,  350,  351,  273,   -1,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,  367,  368,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,   -1,  342,  343,   -1,
  345,  346,  347,  348,  349,  350,  351,   -1,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,  273,   -1,   -1,  364,
  365,  366,  367,  368,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  400,  401,  402,  403,   -1,
  317,  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,  340,   -1,  342,  343,   -1,  345,  346,
  347,  348,  349,  350,  351,   -1,   -1,  354,  355,  356,
   -1,   -1,   -1,  360,  273,   -1,   -1,  364,  365,  366,
  367,  368,   -1,  370,  371,  372,  373,  374,   -1,  376,
   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  400,  401,  402,  403,   -1,  317,  318,
  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,   -1,  342,  343,   -1,  345,  346,  347,  348,
  349,  350,  351,  273,   -1,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,  368,
   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,  378,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,   -1,  342,  343,   -1,  345,  346,  347,  348,  349,
  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,  273,   -1,   -1,  364,  365,  366,  367,  368,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  400,  401,  402,  403,   -1,  317,  318,  319,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,  340,   -1,
  342,  343,   -1,  345,  346,  347,  348,  349,  350,  351,
   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,  273,
   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,  371,
  372,  373,  374,   -1,  376,   -1,  378,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,  401,
  402,  403,   -1,  317,  318,  319,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,   -1,  342,  343,
   -1,  345,  346,  347,  348,  349,  350,  351,  273,   -1,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,  367,  368,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,   -1,  342,  343,   -1,
  345,  346,  347,  348,  349,  350,  351,   -1,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,  273,   -1,   -1,  364,
  365,  366,  367,  368,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  400,  401,  402,  403,   -1,
  317,  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,  340,   -1,  342,  343,   -1,  345,  346,
  347,  348,  349,  350,  351,   -1,   -1,  354,  355,  356,
   -1,   -1,   -1,  360,  273,   -1,   -1,  364,  365,  366,
  367,  368,   -1,  370,  371,  372,  373,  374,   -1,  376,
   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  400,  401,  402,  403,   -1,  317,  318,
  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,   -1,  342,  343,   -1,  345,  346,  347,  348,
  349,  350,  351,  273,   -1,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,  368,
   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,  378,
   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,   -1,  342,  343,   -1,  345,  346,  347,  348,  349,
  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,  273,   -1,   -1,  364,  365,  366,  367,  368,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  400,  401,  402,  403,   -1,  317,  318,  319,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,  340,   -1,
  342,  343,   -1,  345,  346,  347,  348,  349,  350,  351,
   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,  273,
   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,  371,
  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   -1,   60,   -1,  389,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,  401,
  402,  403,   -1,  317,  318,  319,   -1,   -1,   -1,   -1,
   -1,   91,   -1,   -1,  328,   -1,   91,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,   -1,  342,  343,
   -1,  345,  346,  347,  348,  349,  350,  351,  273,   -1,
  354,  355,  356,  123,   41,   -1,  360,   -1,  123,   -1,
  364,  365,  366,  367,  368,   -1,  370,  371,  372,  373,
  374,   -1,  376,   60,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   60,  328,   91,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,   -1,  342,  343,   -1,
  345,  346,  347,  348,  349,  350,  351,   60,   -1,  354,
  355,  356,   60,   91,   -1,  360,  123,   -1,   -1,  364,
  365,  366,  367,  368,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   91,   -1,
   60,   -1,   -1,   91,  389,  123,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  400,  401,  402,  403,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,   91,   -1,   -1,  264,  123,   -1,   -1,   60,  264,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  273,   -1,
   -1,  281,  282,  283,  284,  285,  281,  282,  283,  284,
  285,   60,   -1,  123,   -1,   -1,   60,   -1,   -1,   91,
   -1,   -1,   -1,   -1,   -1,  300,  301,   -1,  303,  304,
  305,  306,  307,  308,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,   91,   -1,   -1,   -1,   -1,   91,   -1,  329,
  330,  123,   -1,  257,  258,  259,  260,  261,  262,  263,
   60,  265,   -1,   -1,   -1,   -1,   60,  264,   -1,   91,
  274,   -1,  276,   -1,  123,  279,  273,   -1,   -1,  123,
   -1,  278,  286,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   91,   -1,   60,   -1,   -1,  264,   91,   -1,   -1,
   -1,  123,   -1,  125,   -1,  273,   60,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,
   60,  264,   -1,  123,   91,   -1,  264,   -1,   -1,  123,
  273,   -1,   -1,   60,  302,  273,   -1,   91,  281,  282,
  283,  284,  285,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   91,   -1,   93,  264,   60,  123,   -1,  125,  302,
   60,  299,   -1,  273,   91,   -1,   -1,   -1,   -1,  123,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,   -1,  292,  123,   -1,   -1,   91,  297,  298,   -1,
   -1,   91,  264,   -1,   -1,   -1,  123,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  125,  264,   -1,   -1,  123,   -1,
  264,  293,   -1,  123,  273,   -1,   -1,   -1,   -1,  273,
   -1,   -1,  281,  282,  283,  284,  285,  281,  282,  283,
  284,  285,  264,  265,  293,   -1,   -1,   -1,  292,   -1,
   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  264,  265,   -1,   -1,   -1,   -1,
  264,  265,   -1,  273,  274,   -1,   -1,   -1,   -1,  273,
  274,  281,  282,  283,  284,  285,   -1,  281,  282,  283,
  284,  285,  261,  262,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
  264,   -1,   -1,   -1,  281,  282,  283,  284,  285,  273,
  274,   -1,   -1,   -1,  264,   -1,   -1,  281,  282,  283,
  284,  285,   -1,  273,   -1,   -1,   -1,  264,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  273,   -1,   -1,  260,
   -1,  278,   -1,   -1,  281,  282,  283,  284,  285,  264,
   -1,   -1,   -1,   -1,  264,  276,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  273,   -1,   -1,  281,  282,  283,  284,
  285,  281,  282,  283,  284,  285,   -1,  273,  274,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  307,  308,   -1,   -1,
  311,  312,  313,  314,  315,  316,   -1,   -1,   -1,   -1,
   -1,   -1,  381,  382,  383,  384,   -1,   -1,   -1,   -1,
   -1,  390,  391,  392,  393,  394,  395,  396,  397,  398,
  399,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,  340,   -1,  342,  343,   -1,  345,
  346,  347,  348,  349,  350,  351,  273,  274,  354,  355,
  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,
  366,  367,  368,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
  317,  318,  319,   -1,  400,  401,  402,  403,   -1,   -1,
   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,  340,   -1,  342,  343,   -1,  345,  346,
  347,  348,  349,  350,  351,  273,  274,  354,  355,  356,
   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,
  367,  368,   -1,  370,  371,  372,  373,  374,   -1,  376,
   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,
  318,  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,  340,   -1,  342,  343,   -1,  345,  346,  347,
  348,  349,  350,  351,  273,  274,  354,  355,  356,   -1,
   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,
  368,   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,
  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,
  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,  340,   -1,  342,  343,   -1,  345,  346,  347,  348,
  349,  350,  351,  273,  274,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,  367,  368,
   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
  340,   -1,  342,  343,   -1,  345,  346,  347,  348,  349,
  350,  351,  273,  274,  354,  355,  356,   -1,   -1,   -1,
  360,   -1,   -1,   -1,  364,  365,  366,  367,  368,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,
  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,  340,
   -1,  342,  343,   -1,  345,  346,  347,  348,  349,  350,
  351,  273,  274,  354,  355,  356,   -1,   -1,   -1,  360,
   -1,   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,
  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,
   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,  400,
  401,  402,  403,   -1,   -1,   -1,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,  340,   -1,
  342,  343,   -1,  345,  346,  347,  348,  349,  350,  351,
  273,   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,
   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,  371,
  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,
   -1,   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,
  402,  403,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,  340,   -1,  342,
  343,   -1,  345,  346,  347,  348,  349,  350,  351,  273,
   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,
   -1,  364,  365,  366,  367,  368,   -1,  370,  371,  372,
  373,  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,
   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,
  403,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,  340,   -1,  342,  343,
   -1,  345,  346,  347,  348,  349,  350,  351,  273,   -1,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,  367,  368,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,  340,   -1,  342,  343,   -1,
  345,  346,  347,  348,  349,  350,  351,  273,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,
  365,  366,  367,  368,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,  317,  318,  319,   -1,  400,  401,  402,  403,   -1,
   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,  340,   -1,  342,  343,   -1,  345,
  346,  347,  348,  349,  350,  351,  273,   -1,  354,  355,
  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,
  366,  367,  368,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
  317,  318,  319,   -1,  400,  401,  402,  403,   -1,   -1,
   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,  340,   -1,  342,  343,   -1,  345,  346,
  347,  348,  349,  350,  351,   -1,   -1,  354,  355,  356,
   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,
  367,  368,   -1,  370,  371,  372,  373,  374,   -1,  376,
   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  400,  401,  402,  403,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,  340,
   -1,  342,  343,   -1,  345,  346,  347,  348,  349,  350,
  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,
   -1,   -1,   -1,  364,  365,  366,  367,  368,   -1,  370,
  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,
  401,  402,  403,
  };

#line 1029 "Repil/IR/IR.jay"

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
  public const int RET = 317;
  public const int BR = 318;
  public const int SWITCH = 319;
  public const int INDIRECTBR = 320;
  public const int INVOKE = 321;
  public const int RESUME = 322;
  public const int CATCHSWITCH = 323;
  public const int CATCHRET = 324;
  public const int CLEANUPRET = 325;
  public const int UNREACHABLE = 326;
  public const int FNEG = 327;
  public const int ADD = 328;
  public const int NUW = 329;
  public const int NSW = 330;
  public const int FADD = 331;
  public const int SUB = 332;
  public const int FSUB = 333;
  public const int MUL = 334;
  public const int FMUL = 335;
  public const int UDIV = 336;
  public const int SDIV = 337;
  public const int FDIV = 338;
  public const int UREM = 339;
  public const int SREM = 340;
  public const int FREM = 341;
  public const int SHL = 342;
  public const int LSHR = 343;
  public const int EXACT = 344;
  public const int ASHR = 345;
  public const int AND = 346;
  public const int OR = 347;
  public const int XOR = 348;
  public const int EXTRACTELEMENT = 349;
  public const int INSERTELEMENT = 350;
  public const int SHUFFLEVECTOR = 351;
  public const int EXTRACTVALUE = 352;
  public const int INSERTVALUE = 353;
  public const int ALLOCA = 354;
  public const int LOAD = 355;
  public const int STORE = 356;
  public const int FENCE = 357;
  public const int CMPXCHG = 358;
  public const int ATOMICRMW = 359;
  public const int GETELEMENTPTR = 360;
  public const int ALIGN = 361;
  public const int INBOUNDS = 362;
  public const int INRANGE = 363;
  public const int TRUNC = 364;
  public const int ZEXT = 365;
  public const int SEXT = 366;
  public const int FPTRUNC = 367;
  public const int FPEXT = 368;
  public const int TO = 369;
  public const int FPTOUI = 370;
  public const int FPTOSI = 371;
  public const int UITOFP = 372;
  public const int SITOFP = 373;
  public const int PTRTOINT = 374;
  public const int INTTOPTR = 375;
  public const int BITCAST = 376;
  public const int ADDRSPACECAST = 377;
  public const int ICMP = 378;
  public const int EQ = 379;
  public const int NE = 380;
  public const int UGT = 381;
  public const int UGE = 382;
  public const int ULT = 383;
  public const int ULE = 384;
  public const int SGT = 385;
  public const int SGE = 386;
  public const int SLT = 387;
  public const int SLE = 388;
  public const int FCMP = 389;
  public const int OEQ = 390;
  public const int OGT = 391;
  public const int OGE = 392;
  public const int OLT = 393;
  public const int OLE = 394;
  public const int ONE = 395;
  public const int ORD = 396;
  public const int UEQ = 397;
  public const int UNE = 398;
  public const int UNO = 399;
  public const int PHI = 400;
  public const int SELECT = 401;
  public const int CALL = 402;
  public const int TAIL = 403;
  public const int VA_ARG = 404;
  public const int LANDINGPAD = 405;
  public const int CATCHPAD = 406;
  public const int CLEANUPPAD = 407;
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
