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
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL parameters function_addr attribute_group_refs '{' blocks '}'",
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
    "NONNULL","NOCAPTURE","WRITEONLY","READONLY","READNONE",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND",
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
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 369 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 95:
#line 373 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 380 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 384 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 99:
#line 392 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 100:
#line 393 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 101:
#line 400 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 102:
#line 404 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 103:
#line 411 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 415 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 105:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 106:
#line 423 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 108:
#line 431 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 109:
#line 435 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 110:
#line 436 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 111:
#line 437 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 112:
#line 438 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 113:
#line 439 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 114:
#line 440 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.SignExtend; }
  break;
case 115:
#line 441 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ZeroExtend; }
  break;
case 121:
#line 459 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 122:
#line 460 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 123:
#line 461 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 124:
#line 462 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 125:
#line 463 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 126:
#line 464 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 127:
#line 465 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 128:
#line 466 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 129:
#line 467 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 130:
#line 468 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 131:
#line 472 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 132:
#line 473 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 133:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 134:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 135:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 136:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 137:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 138:
#line 479 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 139:
#line 480 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 140:
#line 481 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 141:
#line 482 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 142:
#line 483 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 143:
#line 484 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 144:
#line 485 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 145:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 146:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 148:
#line 492 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 149:
#line 493 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 150:
#line 497 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 501 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 505 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 509 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 513 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 529 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 159:
#line 530 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 160:
#line 531 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 161:
#line 532 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 162:
#line 533 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 163:
#line 534 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 164:
#line 535 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 165:
#line 536 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 166:
#line 537 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 167:
#line 544 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 551 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 555 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 170:
#line 562 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 573 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 580 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 591 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 602 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 606 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 613 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 617 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 181:
#line 621 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 625 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 183:
#line 632 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 636 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 643 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 647 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 651 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 655 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 190:
#line 663 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 191:
#line 664 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 192:
#line 671 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 675 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 682 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 195:
#line 686 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 196:
#line 690 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 197:
#line 694 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 198:
#line 698 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 199:
#line 702 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 200:
#line 706 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 202:
#line 711 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 207:
#line 728 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 732 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 738 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 210:
#line 745 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 749 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 756 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 785 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 789 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 222:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 227:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 228:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 230:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 231:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 232:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 233:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 234:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 235:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 236:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 251:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 252:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 253:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 952 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 956 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 960 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 964 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 968 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 972 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 267:
#line 976 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 980 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 984 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 988 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 992 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 272:
#line 996 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 273:
#line 1000 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 274:
#line 1004 "Repil/IR/IR.jay"
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
    4,    4,    4,    4,    4,    5,    5,    5,   27,   27,
   31,   31,   32,   32,   32,   32,   33,   33,   34,   34,
   34,   34,   34,   34,   34,   14,   14,   28,   28,   35,
   36,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   40,   18,   18,   18,
   18,   18,   18,   18,   18,   18,   41,   21,   21,   42,
   39,   39,   43,   44,   38,   38,   29,   29,   45,   45,
   45,   45,   46,   46,   48,   48,   48,   48,   50,   51,
   51,   52,   52,   53,   53,   53,   53,   53,   53,   53,
   54,   54,   19,   19,   55,   55,   56,   56,   57,   58,
   58,   59,   60,   60,   61,   61,   30,   47,   47,   47,
   47,   49,   49,   49,   49,   49,   49,   49,   49,   49,
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
    1,    3,    3,    3,    3,    3,    3,    6,    5,    2,
    3,    1,    2,    3,    3,    3,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    3,    1,
    1,    1,    4,    2,    3,    5,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    3,    4,    2,    1,
    5,    5,    1,    3,    1,    1,    9,    9,   10,   10,
   11,    9,   10,   11,   12,    5,    6,    6,    3,    2,
    1,    3,    1,    2,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    6,
    9,    6,    6,    3,    3,    3,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    2,    2,    1,    2,
    1,    3,    2,    1,    1,    3,    1,    2,    2,    3,
    1,    2,    1,    2,    1,    2,    3,    4,    1,    3,
    2,    1,    3,    2,    3,    3,    3,    2,    4,    5,
    1,    1,    1,    3,    1,    1,    1,    3,    5,    1,
    2,    3,    1,    2,    1,    1,    1,    2,    7,    2,
    7,    5,    6,    5,    5,    5,    6,    4,    4,    5,
    6,    5,    6,    5,    6,    4,    5,    6,    5,    5,
    4,    4,    4,    4,    5,    6,    7,    6,    6,    7,
    5,    6,    5,    5,    6,    3,    4,    5,    7,    4,
    5,    6,    6,    4,    5,    7,    5,    6,    4,    5,
    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   80,   73,   74,   75,   76,   72,    0,   29,   28,
    0,    0,    0,   71,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  116,  117,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   64,    0,
    0,    0,    0,    0,    0,   79,  217,    0,    0,    0,
    0,    0,    0,    0,    5,    6,    0,    0,    0,    0,
    0,    8,    0,    7,    0,    0,    0,    0,    0,   65,
    0,    0,    0,    0,    0,    0,    0,    0,   86,   77,
    0,    0,   83,    0,    0,    0,  160,  161,  159,  162,
  163,  164,  158,  149,  148,  166,  165,    0,    0,    0,
    0,    0,    0,    0,    0,  147,    0,    0,    0,    0,
    0,    0,    0,   31,    0,    0,    0,   49,   48,   13,
    0,    0,   42,   47,    0,    0,    0,    0,    0,    0,
    0,  105,  106,  100,    0,    0,  101,  120,    0,    0,
  118,   78,    0,    0,    0,    0,    0,    0,   62,   54,
   52,   53,   55,   56,   57,   58,    0,   50,    0,    0,
    0,    0,  171,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   15,    0,    0,    0,   43,   14,    0,
  168,    0,   81,   66,   82,    0,    0,  114,  115,  109,
  110,  112,  111,  113,    0,  107,   99,    0,    0,    0,
    0,  119,   84,    0,    0,    0,    0,   12,   51,    0,
    0,    0,    0,  156,    0,  154,  155,    0,    0,    0,
    0,    0,    0,   35,    0,   33,   36,   37,   32,   17,
   16,   46,   45,   44,    0,    0,    0,    0,  108,  102,
    0,    0,   40,    0,    0,   59,  206,  205,    0,  203,
    0,    0,    0,    0,  172,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  177,    0,    0,  183,    0,    0,    0,    0,    0,   41,
    0,   63,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   23,    0,   39,    0,    0,    0,    0,  220,    0,
    0,  218,    0,  215,  216,    0,    0,  213,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  121,  122,
  123,  124,  125,  126,  127,  128,  129,  130,    0,  131,
  132,  143,  144,  145,  146,  134,  136,  137,  138,  139,
  135,  133,  141,  142,  140,    0,    0,    0,    0,    0,
    0,   92,  178,    0,  184,    0,    0,    0,    0,    0,
   87,    0,   88,  204,    0,  153,  150,  152,    0,    0,
    0,    0,   38,   90,    0,    0,  167,    0,    0,    0,
    0,  214,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  207,    0,  189,    0,
    0,    0,    0,    0,    0,   93,    0,    0,   89,    0,
    0,    0,   91,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  236,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   94,    0,  174,    0,  175,    0,    0,  222,    0,  237,
  267,    0,  245,  254,    0,  240,  270,  258,  239,  272,
  265,  261,    0,    0,  251,    0,  226,  225,  253,  273,
    0,    0,  224,  157,  170,    0,    0,    0,    0,    0,
    0,    0,  208,    0,    0,  191,    0,    0,  192,    0,
  230,    0,    0,    0,   95,  151,    0,    0,    0,    0,
    0,  210,  223,  268,  255,  262,  252,  227,  249,  263,
    0,    0,    0,    0,  248,  238,    0,    0,    0,    0,
  194,    0,  190,    0,  231,    0,  235,  176,  219,  173,
    0,  221,  211,  250,  266,    0,  209,  259,    0,  202,
  196,  201,  197,  195,  193,  212,  199,    0,  200,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  177,  141,  133,   50,
  142,  519,  221,   51,   52,   36,  134,  126,  269,  143,
  593,  178,   61,   62,  102,  103,   98,  160,  330,   69,
  156,  157,  215,  216,  161,  419,  436,  594,  184,  625,
  362,  565,  650,  595,  331,  332,  333,  334,  335,  520,
  586,  638,  639,  693,  270,  516,  517,  651,  652,  367,
  368,
  };
  protected static readonly short [] yySindex = {          896,
   29, -142,   43,   57,   78, 3092, 3156, -225,    0,  896,
    0,    0,    0,    0,  -71,   85,  117,  376, -109,  -28,
    0,    0,    0,    0,    0,    0,    0,  498,    0,    0,
 3172,  -89,  -54,    0,  169, 3032,  -27,  498,  -21,  161,
    0,    0,  -33,  -12,    0,    0,    0,    0,    0,  498,
  -77, -138,  -81,  -15,  203,  -18,  129,  -17,    0,  169,
   11,  261,   47,  498,   51,    0,    0,  -14,  498,  270,
 2938,  -13,  270,  211,    0,    0, 1719,  498,  -77,  498,
  -77,    0,  214,    0, -139,  320,  240, 3186,  270,    0,
  498,  498,   15,  498,  270,   -2, 1906, -148,    0,    0,
  169,  133,    0,  270, -148,  767,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    6,  330,  333,
  334, 3269, 3269, 3269,  323,    0, 1719,  498, 1719,  498,
  317,  324,  154,    0, -139, 3212,    0,    0,    0,    0,
    7, 1719,    0,    0, -138,  169,   69,  316,   34,   76,
  270,    0,    0,    0,  -26,  160,    0,    0,   76, -232,
    0,    0, 3150,   76,   76,   76,  325,  354,    0,    0,
    0,    0,    0,    0,    0,    0, 1703,    0,  356, 3269,
 3269, 3269,    0,   33,   82,   13,   38,  360, 1719,  362,
 1679, 4337,  136,    0, -139,  175,    8,    0,    0, 3234,
    0,   76,    0,    0,    0, -114, -138,    0,    0,    0,
    0,    0,    0,    0,  121,    0,    0, 3122, -111,  138,
 -105,    0,    0,   76,   76,  147, 2999,    0,    0,  498,
   48,   49,   62,    0, 3269,    0,    0,  157,   72,  396,
   84,   87,  403,    0,  410,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  -99, 3822,  -90,   76,    0,    0,
 3822,  -88,    0,  180, 3822,    0,    0,    0,  179,    0,
  146,  498,  498,  498,    0,  181,  199,   98,  204,  205,
  100,  488, 3822,  -87,  402, 3269, -170, 3269,  -20,  498,
  -20,  498,  -20,  498,  498,  498,  498,  498,  498,  -20,
  475,  725,  498,  498,  498, 3269, 3269, 3269,  498,  498,
 3269,  -43, 3269, 3269, 3269, 3269, 3269, 3269, 3269, 3269,
 3269, 3269, 3269,  950, 3200,  498,  498, 3032,   66, 1886,
    0, 3822,  181,    0,  181, 3822,  -92, 1980, 3822,    0,
 2057,    0, 2999, 3269,  195,  283,  308,  193,  181,  212,
  181,    0,  216,    0,  185, 2134, 3822, 4206,    0,  206,
 1837,    0,  432,    0,    0, 1719,  -20,    0, 1719, 1719,
  -20, 1719, 1719,  -20, 1719, 1719, 1719, 1719, 1719, 1719,
 1719,  -20,  498, 1719,  498, 1719, 1719, 1719, 1719,  436,
  437,  438,  155,  227,  441,  498,  286,  119,  128,  140,
  144,  145,  148,  150,  165,  166,  170,  171,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  498,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  498,   26, 1719,  418, 2986,
 3032,    0,    0,  181,    0,  193,  193, 2211, 3822,  -82,
    0, 2288,    0,    0,  443,    0,    0,    0,  193,  181,
  193,  181,    0,    0, 2365,  181,    0,  444,  247,  470,
 1719,    0,  478,  480, 1719,  483,  484, 1719,  487,  492,
  500,  503,  511,  512,  519, 1719, 1719,  523, 1719,  525,
  526,  527,  528, 3269, 3269, 3269,  217,  498,  498,  315,
 3269,  498,  498,  498,  498,  498,  498,  498,  498,  498,
  498,  498, 1719, 1719, 1837,  531,    0,  537,    0,  542,
  418,  498,  418,  498,  193,    0, 2442, 3822,    0, 3269,
  193,  193,    0,  193,  247,  493, 1837,  539, 1837, 1837,
  541, 1837, 1837,  543, 1837, 1837, 1837, 1837, 1837, 1837,
 1837,  544,  546, 1837,  553, 1837, 1837, 1837, 1837,    0,
  557,  562,  355, 1719,  570,  574, 3269,  575,  169,  169,
  169,  169,  169,  169,  169,  169,  169,  169,  169,  576,
  578,  581,  535, 3269, 3086,   76,  542,  418,  542,  418,
    0, 2519,    0,  190,    0,  583,  498,    0, 1837,    0,
    0, 1837,    0,    0, 1837,    0,    0,    0,    0,    0,
    0,    0, 1837, 1837,    0, 1837,    0,    0,    0,    0,
 3269, 3269,    0,    0,    0,  269,  271,  587, 3269, 1837,
 1837, 1837,    0,  595,  710,    0, 1629,  191,    0,   76,
    0,  542,   76,  542,    0,    0, 3269,  247,  375,  598,
 3239,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  393,  395, 3269,  609,    0,    0,  564, 3269,  615,  563,
    0, 1783,    0, 3262,    0,   76,    0,    0,    0,    0,
  247,    0,    0,    0,    0,  609,    0,    0, 1868,    0,
    0,    0,    0,    0,    0,    0,    0,  213,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  656,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 1596,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    9,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  618,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  110,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  618,    0,  618,    0,
    0,    0,    0,    0,    0,    0,  524,    0,    0,    0,
    0,  618,    0,    0,    0,   10,  618,    0,  618,    0,
    0,    0,    0,    0,  139,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   83, 1965, 3253,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  618,    0,
  618,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  268,    0,    0,    0,    0,    0,
    0,    0,    0,  112,  120,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  130,
    0,    0,    0,    0,  275,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  618,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 2596,    0, 3899,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  618,  618,  618,  164,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  618,    0,    0,  618,  618,
    0,  618,  618,    0,  618,  618,  618,  618,  618,  618,
  618,    0,    0,  618,    0,  618,  618,  618,  618,    0,
    0,    0,  618,  618,    0,    0,  618,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  618,  618,    0,    0,
    0,    0,    0, 2673,    0, 2750, 3976,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  176,  202,
  305,    0,    0,    0,    0, 4053,    0,    0,    0,    0,
  618,    0,    0,    0,  618,    0,    0,  618,    0,    0,
    0,    0,    0,    0,    0,  618,  618,    0,  618,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  618,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  618,  618,    0, 3283,    0,    0,    0,    0,
    0,    0,    0,    0, 2827,    0,    0,    0,    0,    0,
  327,  335,    0, 4130,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  618,    0,    0,    0,    0,  601,  688,
  775,  862,  939, 1026, 1113, 1200, 1277, 1364, 1451,    0,
    0,    0,    0,    0,    0, 3360,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  618,    0,    0, 3437,
    0,    0, 3514,    0,    0,    0,    0,    0,  618,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3591,    0,    0,    0,    0,  282,  618,
    0,    0,    0,    0,    0, 3668,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3745,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  653,  610,    0,    0,    0,    0,  529,  534,   16,
   -6,  173, 1698,   40,    0,  660,  489, -181, -278,    0,
   25,  505,  621,    1,    0,  532,   21, -103, -255, -285,
    0,  471,   55, -212, -146,    0,    0, -540,  230,    0,
 -448,  197,    0,   52, 1418,    0,  366,  368,  336, -423,
 -523,    0,   27,    0,  359,    0,  122,    0,   56, -186,
 -201,
  };
  protected static readonly short [] yyTable = {            35,
   35,  166,  259,  355,   57,  338,   37,   39,  256,  341,
  247,  261,   71,  222,   87,   66,   32,  265,   71,  222,
  536,   35,   71,  283,   60,   71,   71,  356,   58,   35,
  449,   35,  336,   64,  339,  357,   68,   71,   72,   32,
  528,  220,  440,   77,   83,  268,  206,   33,   69,   69,
  200,  200,   67,   68,   91,  219,  235,   60,   91,  222,
  224,  225,   35,  641,  101,  643,   78,   66,   80,   96,
   33,  127,  222,  129,  158,   66,  235,  222,  222,   31,
  448,   40,   96,  452,  146,  147,  596,  149,  664,   15,
  155,   79,   81,  105,  128,  360,  130,  587,  255,  589,
  268,  465,   31,   18,  371,  237,  374,   31,  222,  145,
   66,   97,  144,  382,  361,  150,  515,   19,  675,   98,
  677,  189,  686,  191,  164,  235,  205,   16,   17,   21,
  203,  199,  251,   67,   68,   90,  131,  159,   20,  148,
  132,   45,   46,  236,  165,   43,  183,  183,  183,   69,
   85,   45,   46,   85,  337,  524,  101,  234,  158,  220,
  144,  268,  220,   18,  642,  472,  644,   63,  264,  472,
   54,  207,  472,  162,  220,   19,  163,   44,   69,  103,
  472,  220,  103,  264,  202,  264,  264,   66,   42,  344,
  222,  264,  158,  527,  194,  158,   66,  195,  497,  679,
  217,   20,   65,  218,  231,  232,  233,  158,   45,   46,
   66,  155,   47,   48,  158,  250,   47,   48,  195,  342,
   21,   74,  343,  271,  254,  463,   75,  522,  343,   22,
  646,  673,  696,  647,  674,  456,   66,   23,   24,   25,
   26,   27,   85,   21,   70,   55,  258,   76,   56,  125,
   73,   88,   22,  699,   89,   86,  343,   95,  104,  275,
   23,   24,   25,   26,   27,  345,  346,  347,   66,  151,
  498,   82,  592,  208,  209,  210,  211,  212,  213,  214,
  198,  198,  366,  369,  370,  372,  373,  375,  376,  377,
  378,  379,  380,  381,  384,  386,  387,  388,  389,  188,
   71,  190,  393,  394,   25,  397,  364,  365,  104,   97,
  359,  104,  363,   92,  201,   34,  396,   94,   34,  437,
  438,   35,  198,  457,   66,  198,   22,   66,  439,  501,
  390,  391,  392,  106,   24,  395,   64,  398,  399,  400,
  401,  402,  403,  404,  405,  406,  407,  408,  458,   66,
   96,   96,  185,  186,   96,   96,   66,   96,  567,  135,
  471,  240,  136,  243,  475,  179,  187,  478,  455,  180,
   96,   96,  181,  182,  192,  486,  487,  204,  489,   97,
   97,  193,  158,   97,   97,  226,   97,   98,   98,  500,
   96,   98,   98,  227,   98,  230,  238,   21,   21,   97,
   97,   21,   21,  239,   21,  241,  266,   98,   98,  248,
  698,  263,  513,  276,  272,  273,   66,   21,   21,   97,
  208,  209,  210,  211,  212,  213,  214,   98,  274,  514,
  277,   18,   18,   35,   35,   18,   18,   21,   18,  278,
  521,  523,  279,   19,   19,  280,  281,   19,   19,  282,
   19,   18,   18,  340,  220,  349,  350,   71,  353,  259,
  351,  352,  358,   19,   19,  441,  264,  680,  460,   20,
   20,   18,  462,   20,   20,  469,   20,  123,  467,  494,
  495,  496,  640,   19,  499,  502,  530,  535,  692,   20,
   20,  564,  564,  222,  503,  569,  570,  571,  572,  573,
  574,  575,  576,  577,  578,  579,  504,  268,  124,   20,
  505,  506,  360,  537,  507,   35,  508,   35,  560,  561,
  562,  539,  588,  540,  590,  568,  542,  543,  354,  222,
  545,  509,  510,  468,   32,  546,  511,  512,  470,  676,
  122,  473,  474,  547,  476,  477,  548,  479,  480,  481,
  482,  483,  484,  485,  549,  550,  488,   32,  490,  491,
  492,  493,  551,   70,  169,   33,  554,  169,  556,  557,
  558,  559,   25,   25,  583,  563,   25,   25,  637,   25,
  584,  585,  599,  597,  602,  169,  605,  613,   33,  614,
  649,  628,   25,   25,   22,   22,  616,   31,   22,   22,
  621,   22,   24,   24,   66,  622,   24,   24,  634,   24,
  518,  623,   25,  626,   22,   22,  169,  627,  629,  630,
   31,  631,   24,   24,  632,  515,  648,  661,  670,  662,
  663,  107,  108,  109,   22,  110,  111,  112,  668,  113,
   69,  681,   24,  538,  649,  659,  660,  541,  169,  684,
  544,  685,  647,  116,  689,    1,  687,   69,  552,  553,
  117,  555,   41,   84,  197,   45,   46,  637,  196,   47,
   48,   49,   29,   30,  107,  108,  109,   53,  110,  111,
  112,  229,  113,  249,   93,  580,  581,  582,  260,  114,
  115,  672,  688,  466,  223,  566,  116,  444,  678,  445,
  695,  454,    0,  117,  633,    0,  683,    0,    0,  598,
    0,  600,  601,    0,  603,  604,    0,  606,  607,  608,
  609,  610,  611,  612,    0,    0,  615,   69,  617,  618,
  619,  620,    0,    0,    0,    0,  624,    0,   21,    0,
    0,    0,    0,    0,  107,  108,  109,   22,  110,  111,
  112,    0,  113,    0,    0,   23,   24,   25,   26,   27,
    0,   21,    0,  267,    0,    0,  116,    0,    0,   32,
   22,  653,    0,  117,  654,  118,    0,  655,   23,   24,
   25,   26,   27,    0,   32,  656,  657,    0,  658,  119,
  120,  121,    0,    0,    0,    0,  169,  169,    0,    0,
   33,    0,  665,  666,  667,    0,    0,    0,    0,  671,
    0,    0,    0,    0,   69,   33,  383,    0,    0,  107,
  108,  109,    0,  110,  111,  112,    0,  113,    0,    0,
    0,    0,   31,    0,  690,  691,    0,    0,  169,  169,
  169,  116,    0,    0,  694,    0,    0,   31,  117,  169,
    0,    0,  169,  169,  169,  169,  169,  169,  169,  169,
  169,  169,    0,  169,  169,    0,  169,  169,  169,  169,
  169,  169,  169,  269,  269,  169,  169,  169,    0,    0,
    0,  169,    0,    0,    0,  169,  169,  169,  169,  169,
  169,  169,  169,  169,  169,  169,    0,  169,    0,  169,
    0,   69,    0,    0,    0,    0,    0,    0,    0,    0,
  169,    0,    0,    0,    0,  269,  269,  269,    0,    0,
    0,  169,  169,  169,  169,    0,  269,    0,    0,  269,
  269,  269,  269,  269,  269,  269,  269,  269,  269,    0,
  269,  269,    0,  269,  269,  269,  269,  269,  269,  269,
    0,    0,  269,  269,  269,    0,    0,    0,  269,    0,
  274,  274,  269,  269,  269,  269,  269,    0,  269,  269,
  269,  269,  269,   21,  269,    0,  269,    0,   69,    0,
    0,    0,   22,  669,    0,    0,    0,  269,   21,    0,
   23,   24,   25,   26,   27,    0,    0,   22,  269,  269,
  269,  269,  274,  274,  274,   23,   24,   25,   26,   27,
    0,    0,    0,  274,    0,    0,  274,  274,  274,  274,
  274,  274,  274,  274,  274,  274,  167,  274,  274,    0,
  274,  274,  274,  274,  274,  274,  274,    0,    0,  274,
  274,  274,  168,    0,    0,  274,    0,  260,  260,  274,
  274,  274,  274,  274,    0,  274,  274,  274,  274,  274,
    0,  274,    0,  274,    0,   69,  385,    0,    0,    0,
    0,  169,  170,    0,  274,  171,  172,  173,  174,  175,
  176,    0,    0,    0,    0,  274,  274,  274,  274,  260,
  260,  260,    0,    0,    0,    0,    0,    0,    0,    0,
  260,    0,    0,  260,  260,  260,  260,  260,  260,  260,
  260,  260,  260,    0,  260,  260,    0,  260,  260,  260,
  260,  260,  260,  260,    0,    0,  260,  260,  260,    0,
    0,    0,  260,    0,  244,  244,  260,  260,  260,  260,
  260,    0,  260,  260,  260,  260,  260,    0,  260,    0,
  260,    0,   69,    0,    0,    0,    0,    0,    0,    0,
    0,  260,    0,    1,    2,    0,    0,    3,    4,    0,
    5,    0,  260,  260,  260,  260,  244,  244,  244,    0,
    0,    0,    0,    6,    7,    0,    0,  244,    0,    0,
  244,  244,  244,  244,  244,  244,  244,  244,  244,  244,
    0,  244,  244,    8,  244,  244,  244,  244,  244,  244,
  244,  241,  241,  244,  244,  244,    0,    0,    0,  244,
    0,    0,    0,  244,  244,  244,  244,  244,    0,  244,
  244,  244,  244,  244,    0,  244,    0,  244,    0,   69,
    0,    0,    0,    0,    0,    0,    0,    0,  244,    0,
    0,    0,    0,  241,  241,  241,    0,    0,    0,  244,
  244,  244,  244,    0,  241,    0,    0,  241,  241,  241,
  241,  241,  241,  241,  241,  241,  241,    0,  241,  241,
    0,  241,  241,  241,  241,  241,  241,  241,    0,    0,
  241,  241,  241,    0,    0,    0,  241,    0,  242,  242,
  241,  241,  241,  241,  241,    0,  241,  241,  241,  241,
  241,    0,  241,    0,  241,    0,   69,    0,    0,    0,
    0,    0,    0,    0,    0,  241,  409,  410,  411,  412,
  413,  414,  415,  416,  417,  418,  241,  241,  241,  241,
  242,  242,  242,    0,    0,    0,    0,    0,    0,    0,
    0,  242,    0,    0,  242,  242,  242,  242,  242,  242,
  242,  242,  242,  242,    0,  242,  242,    0,  242,  242,
  242,  242,  242,  242,  242,    0,    0,  242,  242,  242,
    0,    0,    0,  242,    0,  243,  243,  242,  242,  242,
  242,  242,    0,  242,  242,  242,  242,  242,    0,  242,
    0,  242,    0,   69,    0,    0,    0,    0,    0,    0,
    0,    0,  242,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  242,  242,  242,  242,  243,  243,  243,
    0,    0,    0,    0,    0,    0,    0,    0,  243,    0,
    0,  243,  243,  243,  243,  243,  243,  243,  243,  243,
  243,    0,  243,  243,    0,  243,  243,  243,  243,  243,
  243,  243,    0,    0,  243,  243,  243,    0,    0,    0,
  243,    0,  271,  271,  243,  243,  243,  243,  243,    0,
  243,  243,  243,  243,  243,    0,  243,    0,  243,    0,
   69,    0,    0,    0,    0,    0,    0,    0,    0,  243,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  243,  243,  243,  243,  271,  271,  271,    0,    0,    0,
    0,    0,    0,    0,    0,  271,    0,    0,  271,  271,
  271,  271,  271,  271,  271,  271,  271,  271,    0,  271,
  271,    0,  271,  271,  271,  271,  271,  271,  271,  264,
  264,  271,  271,  271,    0,    0,    0,  271,    0,    0,
    0,  271,  271,  271,  271,  271,    0,  271,  271,  271,
  271,  271,    0,  271,    0,  271,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  271,    0,    0,    0,
    0,  264,  264,  264,    0,    0,    0,  271,  271,  271,
  271,    0,  264,    0,    0,  264,  264,  264,  264,  264,
  264,  264,  264,  264,  264,    0,  264,  264,    0,  264,
  264,  264,  264,  264,  264,  264,    0,    0,  264,  264,
  264,    0,    0,    0,  264,   69,  257,  257,  264,  264,
  264,  264,  264,    0,  264,  264,  264,  264,  264,    0,
  264,    0,  264,    0,    0,   69,    0,    0,    0,    0,
    0,    0,    0,  264,    0,    0,    0,    0,    0,    0,
   66,    0,    0,    0,  264,  264,  264,  264,  257,  257,
  257,    0,    0,    0,    0,    0,   69,    0,  123,  257,
    0,    0,  257,  257,  257,  257,  257,  257,  257,  257,
  257,  257,    0,  257,  257,    0,  257,  257,  257,  257,
  257,  257,  257,    0,    0,  257,  257,  257,   69,  124,
   66,  257,  242,  228,  228,  257,  257,  257,  257,  257,
    0,  257,  257,  257,  257,  257,    0,  257,  123,  257,
    0,    0,    0,    0,    0,    0,    0,  443,    0,    0,
  257,  122,    0,    0,    0,  443,    0,    0,  443,    0,
   66,  257,  257,  257,  257,  228,  228,  228,    0,  124,
    0,    0,    0,  443,    0,    0,  228,    0,  123,  228,
  228,  228,  228,  228,  228,  228,  228,  228,  228,    0,
  228,  228,    0,  228,  228,  228,  228,  228,  228,  228,
    0,  122,  228,  228,  228,    0,    0,    0,  228,  124,
    0,    0,  228,  228,  228,  228,  228,    0,  228,  228,
  228,  228,  228,    0,  228,    0,  228,  228,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  228,    0,    0,
    0,  122,  123,    0,    0,    0,    0,    0,  228,  228,
  228,  228,   69,   69,   69,    0,   69,   69,   69,    0,
   69,    0,    0,    0,    0,  443,    0,   69,   69,  443,
    0,    0,    0,  124,   69,    0,    0,    0,    0,    0,
    0,   69,  443,    0,    0,  107,  108,  109,    0,  110,
  111,  112,    0,  113,    0,    0,  123,    0,    0,    0,
  114,  115,    0,  257,    0,  122,    0,  116,  697,    0,
    0,    0,    0,    0,  117,    0,  262,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  124,  208,  209,
  210,  211,  212,  213,  214,  107,  108,  109,    0,  110,
  111,  112,    0,  113,  443,    0,  154,    0,    0,    0,
  114,  115,  284,   69,    0,    0,    0,  116,    0,  122,
    0,    0,  167,    0,  117,   32,    0,   69,   69,   69,
    0,    0,    0,  348,    0,  107,  108,  109,  168,  110,
  111,  112,    0,  113,    0,    0,  118,    0,    0,    0,
  114,  115,    0,    0,    0,    0,   33,  116,    0,    0,
  119,  120,  121,    0,  117,    0,    0,  169,  170,  443,
  442,  171,  172,  173,  174,  175,  176,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   31,    0,
  446,    0,  447,    0,  450,    0,  118,    0,    0,  107,
  108,  109,    0,  110,  111,  112,  459,  113,  461,    0,
  119,  120,  121,    0,  114,  115,    0,    0,    0,    0,
    0,  116,    0,    0,    0,    0,    0,    0,  117,    0,
    0,    0,    0,    0,    0,    0,  118,    0,    0,    0,
    0,    0,  208,  209,  210,  211,  212,  213,  214,   60,
  119,  120,  121,  107,  108,  109,    0,  110,  111,  112,
    0,  113,    0,    0,  451,    0,    0,    0,  114,  115,
    0,    0,    0,    0,    0,  116,    0,    0,    0,    0,
    0,    0,  117,    0,  107,  108,  109,    0,  110,  111,
  112,    0,  113,    0,    0,    0,    0,    0,    0,    0,
  118,  525,    0,  267,    0,    0,  116,    0,    0,    0,
    0,    0,    0,  117,  119,  120,  121,  531,  285,  532,
    0,    0,    0,  534,    0,    0,    0,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,  453,    0,  152,    0,    0,   23,   24,   25,   26,
   27,    0,    0,    0,  118,    0,    0,    0,  153,    0,
  286,  287,  288,    0,    0,    0,    0,    0,  119,  120,
  121,  289,    0,    0,  290,  291,  292,  293,  294,  295,
  296,  297,  298,  299,   60,  300,  301,    0,  302,  303,
  304,  305,  306,  307,  308,    0,    0,  309,  310,  311,
   60,    0,    0,  312,    0,    0,    0,  313,  314,  315,
  316,  317,  285,  318,  319,  320,  321,  322,  464,  323,
    0,  324,    0,    0,    0,    0,    0,    0,    0,   60,
   60,    0,  325,   60,   60,   60,   60,   60,   60,    0,
    0,    0,    0,  326,  327,  328,  329,    0,    0,    0,
    0,    0,    0,    0,  286,  287,  288,    0,    0,    0,
    0,    0,    0,    0,    0,  289,    0,    0,  290,  291,
  292,  293,  294,  295,  296,  297,  298,  299,    0,  300,
  301,    0,  302,  303,  304,  305,  306,  307,  308,  285,
    0,  309,  310,  311,    0,  526,    0,  312,    0,    0,
    0,  313,  314,  315,  316,  317,    0,  318,  319,  320,
  321,  322,    0,  323,    0,  324,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  325,    0,    0,    0,
    0,  286,  287,  288,    0,    0,    0,  326,  327,  328,
  329,    0,  289,    0,    0,  290,  291,  292,  293,  294,
  295,  296,  297,  298,  299,    0,  300,  301,    0,  302,
  303,  304,  305,  306,  307,  308,  285,    0,  309,  310,
  311,    0,  529,    0,  312,    0,    0,    0,  313,  314,
  315,  316,  317,    0,  318,  319,  320,  321,  322,    0,
  323,    0,  324,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  325,    0,    0,    0,    0,  286,  287,
  288,    0,    0,    0,  326,  327,  328,  329,    0,  289,
    0,    0,  290,  291,  292,  293,  294,  295,  296,  297,
  298,  299,    0,  300,  301,    0,  302,  303,  304,  305,
  306,  307,  308,  285,    0,  309,  310,  311,    0,  533,
    0,  312,    0,    0,    0,  313,  314,  315,  316,  317,
    0,  318,  319,  320,  321,  322,    0,  323,    0,  324,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  325,    0,    0,    0,    0,  286,  287,  288,    0,    0,
    0,  326,  327,  328,  329,    0,  289,    0,    0,  290,
  291,  292,  293,  294,  295,  296,  297,  298,  299,    0,
  300,  301,    0,  302,  303,  304,  305,  306,  307,  308,
  285,    0,  309,  310,  311,    0,  591,    0,  312,    0,
    0,    0,  313,  314,  315,  316,  317,    0,  318,  319,
  320,  321,  322,    0,  323,    0,  324,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  325,    0,    0,
    0,    0,  286,  287,  288,    0,    0,    0,  326,  327,
  328,  329,    0,  289,    0,    0,  290,  291,  292,  293,
  294,  295,  296,  297,  298,  299,    0,  300,  301,    0,
  302,  303,  304,  305,  306,  307,  308,  285,    0,  309,
  310,  311,    0,  645,    0,  312,    0,    0,    0,  313,
  314,  315,  316,  317,    0,  318,  319,  320,  321,  322,
    0,  323,    0,  324,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  325,    0,    0,    0,    0,  286,
  287,  288,    0,    0,    0,  326,  327,  328,  329,    0,
  289,    0,    0,  290,  291,  292,  293,  294,  295,  296,
  297,  298,  299,    0,  300,  301,    0,  302,  303,  304,
  305,  306,  307,  308,  285,    0,  309,  310,  311,    0,
  181,    0,  312,    0,    0,    0,  313,  314,  315,  316,
  317,    0,  318,  319,  320,  321,  322,    0,  323,    0,
  324,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  325,    0,    0,    0,    0,  286,  287,  288,    0,
    0,    0,  326,  327,  328,  329,    0,  289,    0,    0,
  290,  291,  292,  293,  294,  295,  296,  297,  298,  299,
    0,  300,  301,    0,  302,  303,  304,  305,  306,  307,
  308,  285,    0,  309,  310,  311,    0,  179,    0,  312,
    0,    0,    0,  313,  314,  315,  316,  317,    0,  318,
  319,  320,  321,  322,    0,  323,    0,  324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  325,    0,
    0,    0,    0,  286,  287,  288,    0,    0,    0,  326,
  327,  328,  329,    0,  289,    0,    0,  290,  291,  292,
  293,  294,  295,  296,  297,  298,  299,    0,  300,  301,
    0,  302,  303,  304,  305,  306,  307,  308,  181,    0,
  309,  310,  311,    0,  182,    0,  312,    0,    0,    0,
  313,  314,  315,  316,  317,    0,  318,  319,  320,  321,
  322,    0,  323,    0,  324,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  325,    0,    0,    0,    0,
  181,  181,  181,    0,    0,    0,  326,  327,  328,  329,
    0,  181,    0,    0,  181,  181,  181,  181,  181,  181,
  181,  181,  181,  181,    0,  181,  181,    0,  181,  181,
  181,  181,  181,  181,  181,  179,    0,  181,  181,  181,
    0,  180,    0,  181,    0,    0,    0,  181,  181,  181,
  181,  181,    0,  181,  181,  181,  181,  181,    0,  181,
    0,  181,    0,    0,    0,    0,    0,    0,  100,    0,
    0,    0,  181,    0,    0,    0,    0,  179,  179,  179,
    0,    0,    0,  181,  181,  181,  181,   32,  179,    0,
    0,  179,  179,  179,  179,  179,  179,  179,  179,  179,
  179,    0,  179,  179,    0,  179,  179,  179,  179,  179,
  179,  179,  182,    0,  179,  179,  179,    0,   33,    0,
  179,    0,    0,    0,  179,  179,  179,  179,  179,    0,
  179,  179,  179,  179,  179,   32,  179,    0,  179,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  179,
   31,    0,    0,    0,  182,  182,  182,    0,    0,    0,
  179,  179,  179,  179,    0,  182,   33,    0,  182,  182,
  182,  182,  182,  182,  182,  182,  182,  182,    0,  182,
  182,   32,  182,  182,  182,  182,  182,  182,  182,  180,
    0,  182,  182,  182,    0,    0,    0,  182,   31,    0,
    0,  182,  182,  182,  182,  182,    0,  182,  182,  182,
  182,  182,   33,  182,    0,  182,  636,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  182,    0,    0,    0,
    0,  180,  180,  180,    0,   32,    0,  182,  182,  182,
  182,   32,  180,    0,   31,  180,  180,  180,  180,  180,
  180,  180,  180,  180,  180,    0,  180,  180,    0,  180,
  180,  180,  180,  180,  180,  180,   33,    0,  180,  180,
  180,   32,   33,    0,  180,    0,    0,    0,  180,  180,
  180,  180,  180,    0,  180,  180,  180,  180,  180,    0,
  180,   21,  180,    0,    0,    0,    0,    0,   31,   32,
   22,    0,   33,  180,   31,   32,    0,    0,   23,   24,
   25,   26,   27,    0,  180,  180,  180,  180,    0,    0,
   99,   32,    0,    0,    0,    0,    0,    0,    0,    0,
   33,    0,    0,    0,   31,   32,   33,    0,    0,   21,
    0,    0,    0,    0,    0,  107,  108,  109,   22,  110,
  111,  112,   33,  113,    0,    0,   23,   24,   25,   26,
   27,   32,   31,    0,  267,    0,   33,  116,   31,    0,
    0,    0,    0,    0,  117,  208,  209,  210,  211,  212,
  213,  214,    0,   32,   31,   21,   59,    0,   32,    0,
    0,    0,   33,    0,   22,    0,    0,    0,   31,    0,
  140,    0,   23,   24,   25,   26,   27,    0,    0,    0,
    0,   32,    0,    0,   33,    0,    0,    0,   32,   33,
   67,  682,    0,    0,   31,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   21,
    0,    0,   33,    0,    0,   21,   31,    0,   22,   33,
    0,   31,    0,  635,   22,    0,   23,   24,   25,   26,
   27,    0,   23,   24,   25,   26,   27,   61,    0,    0,
    0,    0,    0,   28,   31,   21,    0,    0,   29,   30,
    0,   31,    0,    0,   22,    0,    0,    0,    0,  152,
    0,    0,   23,   24,   25,   26,   27,    0,    0,    0,
    0,    0,    0,   21,  153,    0,    0,    0,    0,   21,
    0,    0,   22,    0,    0,    0,    0,    0,   22,    0,
   23,   24,   25,   26,   27,   21,   23,   24,   25,   26,
   27,    0,   99,    0,   22,    0,    0,   38,    0,  137,
  138,    0,   23,   24,   25,   26,   27,    0,   22,  139,
  420,  421,    0,    0,    0,    0,   23,   24,   25,   26,
   27,    0,    0,    0,    0,  137,  138,    0,    0,    0,
    0,    0,    0,    0,   22,  139,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,  137,  252,    0,
    0,    0,   21,    0,    0,    0,   22,  253,    0,    0,
    0,   22,   61,    0,   23,   24,   25,   26,   27,   23,
   24,   25,   26,   27,    0,   21,    0,    0,   61,    0,
    0,    0,  137,    0,   22,    0,    0,    0,    0,  635,
    0,   22,   23,   24,   25,   26,   27,    0,    0,   23,
   24,   25,   26,   27,    0,  256,  256,   61,   61,    0,
    0,   61,   61,   61,   61,   61,   61,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  422,  423,
  424,  425,    0,    0,    0,    0,    0,  426,  427,  428,
  429,  430,  431,  432,  433,  434,  435,  256,  256,  256,
    0,    0,    0,    0,    0,    0,    0,    0,  256,    0,
    0,  256,  256,  256,  256,  256,  256,  256,  256,  256,
  256,    0,  256,  256,    0,  256,  256,  256,  256,  256,
  256,  256,  229,  229,  256,  256,  256,    0,    0,    0,
  256,    0,    0,    0,  256,  256,  256,  256,  256,    0,
  256,  256,  256,  256,  256,    0,  256,    0,  256,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  256,
    0,    0,    0,    0,  229,  229,  229,    0,    0,    0,
  256,  256,  256,  256,    0,  229,    0,    0,  229,  229,
  229,  229,  229,  229,  229,  229,  229,  229,    0,  229,
  229,    0,  229,  229,  229,  229,  229,  229,  229,  232,
  232,  229,  229,  229,    0,    0,    0,  229,    0,    0,
    0,  229,  229,  229,  229,  229,    0,  229,  229,  229,
  229,  229,    0,  229,    0,  229,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  229,    0,    0,    0,
    0,  232,  232,  232,    0,    0,    0,  229,  229,  229,
  229,    0,  232,    0,    0,  232,  232,  232,  232,  232,
  232,  232,  232,  232,  232,    0,  232,  232,    0,  232,
  232,  232,  232,  232,  232,  232,  234,  234,  232,  232,
  232,    0,    0,    0,  232,    0,    0,    0,  232,  232,
  232,  232,  232,    0,  232,  232,  232,  232,  232,    0,
  232,    0,  232,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  232,    0,    0,    0,    0,  234,  234,
  234,    0,    0,    0,  232,  232,  232,  232,    0,  234,
    0,    0,  234,  234,  234,  234,  234,  234,  234,  234,
  234,  234,    0,  234,  234,    0,  234,  234,  234,  234,
  234,  234,  234,  246,  246,  234,  234,  234,    0,    0,
    0,  234,    0,    0,    0,  234,  234,  234,  234,  234,
    0,  234,  234,  234,  234,  234,    0,  234,    0,  234,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  234,    0,    0,    0,    0,  246,  246,  246,    0,    0,
    0,  234,  234,  234,  234,    0,  246,    0,    0,  246,
  246,  246,  246,  246,  246,  246,  246,  246,  246,    0,
  246,  246,    0,  246,  246,  246,  246,  246,  246,  246,
  233,  233,  246,  246,  246,    0,    0,    0,  246,    0,
    0,    0,  246,  246,  246,  246,  246,    0,  246,  246,
  246,  246,  246,    0,  246,    0,  246,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  246,    0,    0,
    0,    0,  233,  233,  233,    0,    0,    0,  246,  246,
  246,  246,    0,  233,    0,    0,  233,  233,  233,  233,
  233,  233,  233,  233,  233,  233,    0,  233,  233,    0,
  233,  233,  233,  233,  233,  233,  233,  247,  247,  233,
  233,  233,    0,    0,    0,  233,    0,    0,    0,  233,
  233,  233,  233,  233,    0,  233,  233,  233,  233,  233,
    0,  233,    0,  233,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  233,    0,    0,    0,    0,  247,
  247,  247,    0,    0,    0,  233,  233,  233,  233,    0,
  247,    0,    0,  247,  247,  247,  247,  247,  247,  247,
  247,  247,  247,    0,  247,  247,    0,  247,  247,  247,
  247,  247,  247,  247,  285,    0,  247,  247,  247,    0,
    0,    0,  247,    0,    0,    0,  247,  247,  247,  247,
  247,    0,  247,  247,  247,  247,  247,    0,  247,    0,
  247,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  247,    0,    0,    0,    0,  286,  287,  288,    0,
    0,    0,  247,  247,  247,  247,    0,  289,    0,    0,
  290,  291,  292,  293,  294,  295,  296,  297,  298,  299,
    0,  300,  301,    0,  302,  303,  304,  305,  306,  307,
  308,  185,    0,  309,  310,  311,    0,    0,    0,  312,
    0,    0,    0,  313,  314,  315,  316,  317,    0,  318,
  319,  320,  321,  322,    0,  323,    0,  324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  325,    0,
    0,    0,    0,  185,  185,  185,    0,    0,    0,  326,
  327,  328,  329,    0,  185,    0,    0,  185,  185,  185,
  185,  185,  185,  185,  185,  185,  185,    0,  185,  185,
    0,  185,  185,  185,  185,  185,  185,  185,  186,    0,
  185,  185,  185,    0,    0,    0,  185,    0,    0,    0,
  185,  185,  185,  185,  185,    0,  185,  185,  185,  185,
  185,    0,  185,    0,  185,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  185,    0,    0,    0,    0,
  186,  186,  186,    0,    0,    0,  185,  185,  185,  185,
    0,  186,    0,    0,  186,  186,  186,  186,  186,  186,
  186,  186,  186,  186,    0,  186,  186,    0,  186,  186,
  186,  186,  186,  186,  186,  187,    0,  186,  186,  186,
    0,    0,    0,  186,    0,    0,    0,  186,  186,  186,
  186,  186,    0,  186,  186,  186,  186,  186,    0,  186,
    0,  186,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  186,    0,    0,    0,    0,  187,  187,  187,
    0,    0,    0,  186,  186,  186,  186,    0,  187,    0,
    0,  187,  187,  187,  187,  187,  187,  187,  187,  187,
  187,    0,  187,  187,    0,  187,  187,  187,  187,  187,
  187,  187,  188,    0,  187,  187,  187,    0,    0,    0,
  187,    0,    0,    0,  187,  187,  187,  187,  187,    0,
  187,  187,  187,  187,  187,    0,  187,    0,  187,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  187,
    0,    0,    0,    0,  188,  188,  188,    0,    0,    0,
  187,  187,  187,  187,    0,  188,    0,    0,  188,  188,
  188,  188,  188,  188,  188,  188,  188,  188,    0,  188,
  188,    0,  188,  188,  188,  188,  188,  188,  188,    0,
    0,  188,  188,  188,    0,    0,    0,  188,    0,    0,
    0,  188,  188,  188,  188,  188,    0,  188,  188,  188,
  188,  188,    0,  188,    0,  188,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  188,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  188,  188,  188,
  188,  289,    0,    0,  290,  291,  292,  293,  294,  295,
  296,  297,  298,  299,    0,  300,  301,    0,  302,  303,
  304,  305,  306,  307,  308,    0,    0,  309,  310,  311,
    0,    0,    0,  312,    0,    0,    0,  313,  314,  315,
  316,  317,    0,  318,  319,  320,  321,  322,    0,  323,
    0,  324,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  325,  107,  108,  109,  244,  110,  111,  112,
    0,  113,    0,  326,  327,  328,  329,    0,    0,    0,
  245,    0,  246,    0,    0,  116,    0,    0,    0,    0,
    0,    0,  117,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  105,  215,  282,   33,  261,    6,    7,  123,  265,
  192,  123,   40,  160,   33,   42,   60,  123,   40,  166,
  469,   28,   40,  123,   31,   40,   40,  283,   28,   36,
  123,   38,  123,  123,  123,  123,   36,   40,   38,   60,
  123,  274,  328,   50,   60,  227,  150,   91,   40,   40,
   44,   44,   44,   44,   44,  159,   44,   64,   44,  206,
  164,  165,   69,  587,   71,  589,   51,   42,   53,   69,
   91,   78,  219,   80,  307,   42,   44,  224,  225,  123,
  336,  307,    0,  339,   91,   92,  535,   94,  629,   61,
   97,   52,   53,   73,   79,  266,   81,  521,  202,  523,
  282,  357,  123,   61,  291,   93,  293,  123,  255,   89,
   42,    0,   88,  300,  285,   95,   91,   61,  642,    0,
  644,  128,  663,  130,  104,   44,   93,  270,  271,    0,
   62,  125,  125,  125,  125,  125,  276,   98,   61,  125,
  280,  290,  291,   62,  105,   61,  122,  123,  124,   40,
   41,  290,  291,   44,  258,  441,  163,  125,  307,  274,
  136,  343,  274,    0,  588,  367,  590,  257,  274,  371,
  280,  151,  374,   41,  274,    0,   44,   61,   40,   41,
  382,  274,   44,  274,  145,  274,  274,   42,  260,   44,
  337,  274,  307,  449,   41,  307,   42,   44,   44,  648,
   41,    0,  257,   44,  180,  181,  182,  307,  290,  291,
   42,  218,  294,  295,  307,   41,  294,  295,   44,   41,
  264,   61,   44,  230,  200,   41,  260,  440,   44,  273,
   41,   41,  681,   44,   44,   41,   42,  281,  282,  283,
  284,  285,   40,  264,  272,  274,  207,  260,  277,   77,
  272,  123,  273,   41,  272,  274,   44,  272,  272,  235,
  281,  282,  283,  284,  285,  272,  273,  274,   42,  272,
   44,  287,  528,  300,  301,  302,  303,  304,  305,  306,
  274,  274,  289,  290,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  127,
   40,  129,  309,  310,    0,  312,  327,  328,   41,   40,
  286,   44,  288,  267,  142,   41,  360,  267,   44,  326,
  327,  328,   41,   41,   42,   44,    0,   42,  328,   44,
  306,  307,  308,  123,    0,  311,  123,  313,  314,  315,
  316,  317,  318,  319,  320,  321,  322,  323,   41,   42,
  268,  269,  123,  124,  272,  273,   42,  275,   44,   40,
  367,  189,  123,  191,  371,  360,   44,  374,  344,   40,
  288,  289,   40,   40,   58,  382,  383,   62,  385,  268,
  269,   58,  307,  272,  273,   61,  275,  268,  269,  396,
  308,  272,  273,   40,  275,   40,  359,  268,  269,  288,
  289,  272,  273,   44,  275,   44,  260,  288,  289,  274,
  689,  274,  419,  257,  367,  367,   42,  288,  289,  308,
  300,  301,  302,  303,  304,  305,  306,  308,  367,  436,
  359,  268,  269,  440,  441,  272,  273,  308,  275,   44,
  440,  441,  359,  268,  269,  359,   44,  272,  273,   40,
  275,  288,  289,  274,  274,  257,  359,   40,  359,  672,
  257,  257,   61,  288,  289,  400,  274,  649,  257,  268,
  269,  308,  257,  272,  273,   44,  275,   60,  273,   44,
   44,   44,  586,  308,   44,  367,   44,   44,  670,  288,
  289,  498,  499,  640,  367,  502,  503,  504,  505,  506,
  507,  508,  509,  510,  511,  512,  367,  689,   91,  308,
  367,  367,  266,   44,  367,  522,  367,  524,  494,  495,
  496,   44,  522,   44,  524,  501,   44,   44,   41,  676,
   44,  367,  367,  361,   60,   44,  367,  367,  366,  643,
  123,  369,  370,   44,  372,  373,   44,  375,  376,  377,
  378,  379,  380,  381,   44,   44,  384,   60,  386,  387,
  388,  389,   44,   40,   41,   91,   44,   44,   44,   44,
   44,   44,  268,  269,   44,  359,  272,  273,  585,  275,
   44,   40,   44,   91,   44,   62,   44,   44,   91,   44,
  597,  567,  288,  289,  268,  269,   44,  123,  272,  273,
   44,  275,  268,  269,   42,   44,  272,  273,  584,  275,
  438,  257,  308,   44,  288,  289,   93,   44,   44,   44,
  123,   44,  288,  289,   44,   91,   44,  359,  635,  359,
   44,  257,  258,  259,  308,  261,  262,  263,   44,  265,
   40,   44,  308,  471,  651,  621,  622,  475,  125,  257,
  478,  257,   44,  279,   40,    0,   93,   40,  486,  487,
  286,  489,   10,   54,  136,  290,  291,  674,  135,  294,
  295,  296,  297,  298,  257,  258,  259,   18,  261,  262,
  263,  177,  265,  195,   64,  513,  514,  515,  218,  272,
  273,  637,  668,  358,  163,  499,  279,  332,  647,  332,
  674,  343,   -1,  286,  583,   -1,  651,   -1,   -1,  537,
   -1,  539,  540,   -1,  542,  543,   -1,  545,  546,  547,
  548,  549,  550,  551,   -1,   -1,  554,   40,  556,  557,
  558,  559,   -1,   -1,   -1,   -1,  564,   -1,  264,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,  273,  261,  262,
  263,   -1,  265,   -1,   -1,  281,  282,  283,  284,  285,
   -1,  264,   -1,  276,   -1,   -1,  279,   -1,   -1,   60,
  273,  599,   -1,  286,  602,  358,   -1,  605,  281,  282,
  283,  284,  285,   -1,   60,  613,  614,   -1,  616,  372,
  373,  374,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
   91,   -1,  630,  631,  632,   -1,   -1,   -1,   -1,  637,
   -1,   -1,   -1,   -1,   40,   91,  342,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   -1,   -1,  123,   -1,  272,  273,   -1,   -1,  315,  316,
  317,  279,   -1,   -1,  672,   -1,   -1,  123,  286,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,  340,  341,   -1,  343,  344,  345,  346,
  347,  348,  349,  273,  274,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,
  367,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
  340,  341,   -1,  343,  344,  345,  346,  347,  348,  349,
   -1,   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
  273,  274,  362,  363,  364,  365,  366,   -1,  368,  369,
  370,  371,  372,  264,  374,   -1,  376,   -1,   40,   -1,
   -1,   -1,  273,  274,   -1,   -1,   -1,  387,  264,   -1,
  281,  282,  283,  284,  285,   -1,   -1,  273,  398,  399,
  400,  401,  315,  316,  317,  281,  282,  283,  284,  285,
   -1,   -1,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,  338,  260,  340,  341,   -1,
  343,  344,  345,  346,  347,  348,  349,   -1,   -1,  352,
  353,  354,  276,   -1,   -1,  358,   -1,  273,  274,  362,
  363,  364,  365,  366,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   40,  342,   -1,   -1,   -1,
   -1,  305,  306,   -1,  387,  309,  310,  311,  312,  313,
  314,   -1,   -1,   -1,   -1,  398,  399,  400,  401,  315,
  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,  340,  341,   -1,  343,  344,  345,
  346,  347,  348,  349,   -1,   -1,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,  273,  274,  362,  363,  364,  365,
  366,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,  268,  269,   -1,   -1,  272,  273,   -1,
  275,   -1,  398,  399,  400,  401,  315,  316,  317,   -1,
   -1,   -1,   -1,  288,  289,   -1,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,  340,  341,  308,  343,  344,  345,  346,  347,  348,
  349,  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   40,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,  340,  341,
   -1,  343,  344,  345,  346,  347,  348,  349,   -1,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,  273,  274,
  362,  363,  364,  365,  366,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   40,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,  377,  378,  379,  380,
  381,  382,  383,  384,  385,  386,  398,  399,  400,  401,
  315,  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,  340,  341,   -1,  343,  344,
  345,  346,  347,  348,  349,   -1,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,  273,  274,  362,  363,  364,
  365,  366,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  398,  399,  400,  401,  315,  316,  317,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,  340,  341,   -1,  343,  344,  345,  346,  347,
  348,  349,   -1,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,  273,  274,  362,  363,  364,  365,  366,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  398,  399,  400,  401,  315,  316,  317,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,  340,
  341,   -1,  343,  344,  345,  346,  347,  348,  349,  273,
  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,  365,  366,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,  340,  341,   -1,  343,
  344,  345,  346,  347,  348,  349,   -1,   -1,  352,  353,
  354,   -1,   -1,   -1,  358,   40,  273,  274,  362,  363,
  364,  365,  366,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,   -1,   -1,
   42,   -1,   -1,   -1,  398,  399,  400,  401,  315,  316,
  317,   -1,   -1,   -1,   -1,   -1,   91,   -1,   60,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,  340,  341,   -1,  343,  344,  345,  346,
  347,  348,  349,   -1,   -1,  352,  353,  354,  123,   91,
   42,  358,   44,  273,  274,  362,  363,  364,  365,  366,
   -1,  368,  369,  370,  371,  372,   -1,  374,   60,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,   -1,   -1,
  387,  123,   -1,   -1,   -1,  338,   -1,   -1,  341,   -1,
   42,  398,  399,  400,  401,  315,  316,  317,   -1,   91,
   -1,   -1,   -1,  356,   -1,   -1,  326,   -1,   60,  329,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
  340,  341,   -1,  343,  344,  345,  346,  347,  348,  349,
   -1,  123,  352,  353,  354,   -1,   -1,   -1,  358,   91,
   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,  123,   60,   -1,   -1,   -1,   -1,   -1,  398,  399,
  400,  401,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,  448,   -1,  272,  273,  452,
   -1,   -1,   -1,   91,  279,   -1,   -1,   -1,   -1,   -1,
   -1,  286,  465,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   60,   -1,   -1,   -1,
  272,  273,   -1,  206,   -1,  123,   -1,  279,   41,   -1,
   -1,   -1,   -1,   -1,  286,   -1,  219,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   91,  300,  301,
  302,  303,  304,  305,  306,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  527,   -1,   41,   -1,   -1,   -1,
  272,  273,  255,  358,   -1,   -1,   -1,  279,   -1,  123,
   -1,   -1,  260,   -1,  286,   60,   -1,  372,  373,  374,
   -1,   -1,   -1,  276,   -1,  257,  258,  259,  276,  261,
  262,  263,   -1,  265,   -1,   -1,  358,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   91,  279,   -1,   -1,
  372,  373,  374,   -1,  286,   -1,   -1,  305,  306,  592,
  125,  309,  310,  311,  312,  313,  314,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
  333,   -1,  335,   -1,  337,   -1,  358,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,  349,  265,  351,   -1,
  372,  373,  374,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,  286,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  358,   -1,   -1,   -1,
   -1,   -1,  300,  301,  302,  303,  304,  305,  306,  125,
  372,  373,  374,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,  125,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,   -1,  286,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  358,  444,   -1,  276,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  286,  372,  373,  374,  460,  273,  462,
   -1,   -1,   -1,  466,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,  125,   -1,  278,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,  358,   -1,   -1,   -1,  293,   -1,
  315,  316,  317,   -1,   -1,   -1,   -1,   -1,  372,  373,
  374,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  338,  260,  340,  341,   -1,  343,  344,
  345,  346,  347,  348,  349,   -1,   -1,  352,  353,  354,
  276,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
  365,  366,  273,  368,  369,  370,  371,  372,  125,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  305,
  306,   -1,  387,  309,  310,  311,  312,  313,  314,   -1,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,  340,
  341,   -1,  343,  344,  345,  346,  347,  348,  349,  273,
   -1,  352,  353,  354,   -1,  125,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,  365,  366,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,  340,  341,   -1,  343,
  344,  345,  346,  347,  348,  349,  273,   -1,  352,  353,
  354,   -1,  125,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,  365,  366,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,  340,  341,   -1,  343,  344,  345,  346,
  347,  348,  349,  273,   -1,  352,  353,  354,   -1,  125,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
  340,  341,   -1,  343,  344,  345,  346,  347,  348,  349,
  273,   -1,  352,  353,  354,   -1,  125,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,  340,  341,   -1,
  343,  344,  345,  346,  347,  348,  349,  273,   -1,  352,
  353,  354,   -1,  125,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,  365,  366,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,  340,  341,   -1,  343,  344,  345,
  346,  347,  348,  349,  273,   -1,  352,  353,  354,   -1,
  125,   -1,  358,   -1,   -1,   -1,  362,  363,  364,  365,
  366,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,  340,  341,   -1,  343,  344,  345,  346,  347,  348,
  349,  273,   -1,  352,  353,  354,   -1,  125,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,  340,  341,
   -1,  343,  344,  345,  346,  347,  348,  349,  273,   -1,
  352,  353,  354,   -1,  125,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,  365,  366,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,  340,  341,   -1,  343,  344,
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   -1,  125,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
  365,  366,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   60,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,  340,  341,   -1,  343,  344,  345,  346,  347,
  348,  349,  273,   -1,  352,  353,  354,   -1,   91,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,
  368,  369,  370,  371,  372,   60,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
  123,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   91,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,  340,
  341,   60,  343,  344,  345,  346,  347,  348,  349,  273,
   -1,  352,  353,  354,   -1,   -1,   -1,  358,  123,   -1,
   -1,  362,  363,  364,  365,  366,   -1,  368,  369,  370,
  371,  372,   91,  374,   -1,  376,   41,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   60,   -1,  398,  399,  400,
  401,   60,  326,   -1,  123,  329,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,  340,  341,   -1,  343,
  344,  345,  346,  347,  348,  349,   91,   -1,  352,  353,
  354,   60,   91,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,  365,  366,   -1,  368,  369,  370,  371,  372,   -1,
  374,  264,  376,   -1,   -1,   -1,   -1,   -1,  123,   60,
  273,   -1,   91,  387,  123,   60,   -1,   -1,  281,  282,
  283,  284,  285,   -1,  398,  399,  400,  401,   -1,   -1,
  293,   60,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   -1,  123,   60,   91,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  273,  261,
  262,  263,   91,  265,   -1,   -1,  281,  282,  283,  284,
  285,   60,  123,   -1,  276,   -1,   91,  279,  123,   -1,
   -1,   -1,   -1,   -1,  286,  300,  301,  302,  303,  304,
  305,  306,   -1,   60,  123,  264,  125,   -1,   60,   -1,
   -1,   -1,   91,   -1,  273,   -1,   -1,   -1,  123,   -1,
  125,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   91,   -1,   -1,   -1,   60,   91,
  299,   93,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   91,   -1,   -1,  264,  123,   -1,  273,   91,
   -1,  123,   -1,  278,  273,   -1,  281,  282,  283,  284,
  285,   -1,  281,  282,  283,  284,  285,  125,   -1,   -1,
   -1,   -1,   -1,  292,  123,  264,   -1,   -1,  297,  298,
   -1,  123,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,
   -1,   -1,   -1,  264,  293,   -1,   -1,   -1,   -1,  264,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,  273,   -1,
  281,  282,  283,  284,  285,  264,  281,  282,  283,  284,
  285,   -1,  293,   -1,  273,   -1,   -1,  292,   -1,  264,
  265,   -1,  281,  282,  283,  284,  285,   -1,  273,  274,
  261,  262,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,   -1,   -1,   -1,   -1,  264,  265,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  264,  265,   -1,
   -1,   -1,  264,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,  273,  260,   -1,  281,  282,  283,  284,  285,  281,
  282,  283,  284,  285,   -1,  264,   -1,   -1,  276,   -1,
   -1,   -1,  264,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,  273,  281,  282,  283,  284,  285,   -1,   -1,  281,
  282,  283,  284,  285,   -1,  273,  274,  305,  306,   -1,
   -1,  309,  310,  311,  312,  313,  314,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  379,  380,
  381,  382,   -1,   -1,   -1,   -1,   -1,  388,  389,  390,
  391,  392,  393,  394,  395,  396,  397,  315,  316,  317,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,  340,  341,   -1,  343,  344,  345,  346,  347,
  348,  349,  273,  274,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,  340,
  341,   -1,  343,  344,  345,  346,  347,  348,  349,  273,
  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,  365,  366,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,  340,  341,   -1,  343,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,  365,  366,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,  340,  341,   -1,  343,  344,  345,  346,
  347,  348,  349,  273,  274,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
  340,  341,   -1,  343,  344,  345,  346,  347,  348,  349,
  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,  340,  341,   -1,
  343,  344,  345,  346,  347,  348,  349,  273,  274,  352,
  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,  365,  366,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,  340,  341,   -1,  343,  344,  345,
  346,  347,  348,  349,  273,   -1,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,  365,
  366,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,  340,  341,   -1,  343,  344,  345,  346,  347,  348,
  349,  273,   -1,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,  340,  341,
   -1,  343,  344,  345,  346,  347,  348,  349,  273,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,  365,  366,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,  340,  341,   -1,  343,  344,
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
  365,  366,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,  340,  341,   -1,  343,  344,  345,  346,  347,
  348,  349,  273,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,  365,  366,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,  340,
  341,   -1,  343,  344,  345,  346,  347,  348,  349,   -1,
   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,  365,  366,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  398,  399,  400,
  401,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,  340,  341,   -1,  343,  344,
  345,  346,  347,  348,  349,   -1,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
  365,  366,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,  257,  258,  259,  260,  261,  262,  263,
   -1,  265,   -1,  398,  399,  400,  401,   -1,   -1,   -1,
  274,   -1,  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,
   -1,   -1,  286,
  };

#line 1008 "Repil/IR/IR.jay"

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
  public const int NONNULL = 302;
  public const int NOCAPTURE = 303;
  public const int WRITEONLY = 304;
  public const int READONLY = 305;
  public const int READNONE = 306;
  public const int ATTRIBUTE_GROUP_REF = 307;
  public const int ATTRIBUTES = 308;
  public const int NORECURSE = 309;
  public const int NOUNWIND = 310;
  public const int SPECULATABLE = 311;
  public const int SSP = 312;
  public const int UWTABLE = 313;
  public const int ARGMEMONLY = 314;
  public const int RET = 315;
  public const int BR = 316;
  public const int SWITCH = 317;
  public const int INDIRECTBR = 318;
  public const int INVOKE = 319;
  public const int RESUME = 320;
  public const int CATCHSWITCH = 321;
  public const int CATCHRET = 322;
  public const int CLEANUPRET = 323;
  public const int UNREACHABLE = 324;
  public const int FNEG = 325;
  public const int ADD = 326;
  public const int NUW = 327;
  public const int NSW = 328;
  public const int FADD = 329;
  public const int SUB = 330;
  public const int FSUB = 331;
  public const int MUL = 332;
  public const int FMUL = 333;
  public const int UDIV = 334;
  public const int SDIV = 335;
  public const int FDIV = 336;
  public const int UREM = 337;
  public const int SREM = 338;
  public const int FREM = 339;
  public const int SHL = 340;
  public const int LSHR = 341;
  public const int EXACT = 342;
  public const int ASHR = 343;
  public const int AND = 344;
  public const int OR = 345;
  public const int XOR = 346;
  public const int EXTRACTELEMENT = 347;
  public const int INSERTELEMENT = 348;
  public const int SHUFFLEVECTOR = 349;
  public const int EXTRACTVALUE = 350;
  public const int INSERTVALUE = 351;
  public const int ALLOCA = 352;
  public const int LOAD = 353;
  public const int STORE = 354;
  public const int FENCE = 355;
  public const int CMPXCHG = 356;
  public const int ATOMICRMW = 357;
  public const int GETELEMENTPTR = 358;
  public const int ALIGN = 359;
  public const int INBOUNDS = 360;
  public const int INRANGE = 361;
  public const int TRUNC = 362;
  public const int ZEXT = 363;
  public const int SEXT = 364;
  public const int FPTRUNC = 365;
  public const int FPEXT = 366;
  public const int TO = 367;
  public const int FPTOUI = 368;
  public const int FPTOSI = 369;
  public const int UITOFP = 370;
  public const int SITOFP = 371;
  public const int PTRTOINT = 372;
  public const int INTTOPTR = 373;
  public const int BITCAST = 374;
  public const int ADDRSPACECAST = 375;
  public const int ICMP = 376;
  public const int EQ = 377;
  public const int NE = 378;
  public const int UGT = 379;
  public const int UGE = 380;
  public const int ULT = 381;
  public const int ULE = 382;
  public const int SGT = 383;
  public const int SGE = 384;
  public const int SLT = 385;
  public const int SLE = 386;
  public const int FCMP = 387;
  public const int OEQ = 388;
  public const int OGT = 389;
  public const int OGE = 390;
  public const int OLT = 391;
  public const int OLE = 392;
  public const int ONE = 393;
  public const int ORD = 394;
  public const int UEQ = 395;
  public const int UNE = 396;
  public const int UNO = 397;
  public const int PHI = 398;
  public const int SELECT = 399;
  public const int CALL = 400;
  public const int TAIL = 401;
  public const int VA_ARG = 402;
  public const int LANDINGPAD = 403;
  public const int CATCHPAD = 404;
  public const int CLEANUPPAD = 405;
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
