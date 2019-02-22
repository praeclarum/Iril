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
    "PRIVATE","INTERNAL","EXTERNAL","FASTCC","NONNULL","NOCAPTURE",
    "WRITEONLY","READONLY","READNONE","ATTRIBUTE_GROUP_REF","ATTRIBUTES",
    "NORECURSE","NOUNWIND","SPECULATABLE","SSP","UWTABLE","ARGMEMONLY",
    "RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME","CATCHSWITCH",
    "CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD","NUW","NSW","FADD",
    "SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV","UREM","SREM","FREM",
    "SHL","LSHR","EXACT","ASHR","AND","OR","XOR","EXTRACTELEMENT",
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
case 119:
#line 457 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 120:
#line 458 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 121:
#line 459 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 122:
#line 460 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 123:
#line 461 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 124:
#line 462 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 125:
#line 463 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 126:
#line 464 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 127:
#line 465 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 128:
#line 466 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 129:
#line 470 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 130:
#line 471 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 131:
#line 472 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 132:
#line 473 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 133:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 134:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 135:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 136:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 137:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 138:
#line 479 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 139:
#line 480 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 140:
#line 481 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 141:
#line 482 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 142:
#line 483 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 143:
#line 484 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 144:
#line 485 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 146:
#line 490 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 147:
#line 491 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 148:
#line 495 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 149:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 150:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 527 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 157:
#line 528 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 158:
#line 529 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 159:
#line 530 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 160:
#line 531 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 161:
#line 532 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 162:
#line 533 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 163:
#line 534 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 164:
#line 535 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 165:
#line 542 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 549 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 553 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 168:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 578 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 589 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 593 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 600 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 604 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 179:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 181:
#line 630 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 634 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 641 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 645 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 185:
#line 649 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 653 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 188:
#line 661 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 189:
#line 662 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 190:
#line 669 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 673 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 680 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 193:
#line 684 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 194:
#line 688 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 195:
#line 692 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 196:
#line 696 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 197:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 198:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 200:
#line 709 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 205:
#line 726 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 730 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 736 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 208:
#line 743 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 747 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 754 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 775 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 779 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 783 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 787 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 220:
#line 794 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 798 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 802 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 806 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 810 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 225:
#line 814 "Repil/IR/IR.jay"
  {
        yyVal = new AshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 226:
#line 818 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 822 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 228:
#line 826 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 229:
#line 830 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 230:
#line 834 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 231:
#line 838 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 232:
#line 842 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 233:
#line 846 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 850 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 854 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 858 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 862 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 866 "Repil/IR/IR.jay"
  {
        yyVal = new FpextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 870 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 874 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 878 "Repil/IR/IR.jay"
  {
        yyVal = new FptruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 882 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 886 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 890 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 894 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 898 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 902 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 248:
#line 906 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 249:
#line 910 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 250:
#line 914 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 918 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 922 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 926 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 930 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 934 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 938 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 942 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 946 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 950 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 954 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 958 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 962 "Repil/IR/IR.jay"
  {
        yyVal = new SremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 966 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 264:
#line 970 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 974 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 978 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 982 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 986 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 990 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 994 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 271:
#line 998 "Repil/IR/IR.jay"
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
   34,   34,   34,   14,   14,   28,   28,   35,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   40,   18,   18,   18,   18,   18,
   18,   18,   18,   18,   41,   21,   21,   42,   39,   39,
   43,   44,   38,   38,   29,   29,   45,   45,   45,   45,
   46,   46,   48,   48,   48,   48,   50,   51,   51,   52,
   52,   53,   53,   53,   53,   53,   53,   53,   54,   54,
   19,   19,   55,   55,   56,   56,   57,   58,   58,   59,
   60,   60,   61,   61,   30,   47,   47,   47,   47,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,
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
    1,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    6,    9,    6,
    6,    3,    3,    3,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    2,    2,    1,    2,    1,    3,
    2,    1,    1,    3,    1,    2,    2,    3,    1,    2,
    1,    2,    1,    2,    3,    4,    1,    3,    2,    1,
    3,    2,    3,    3,    3,    2,    4,    5,    1,    1,
    1,    3,    1,    1,    1,    3,    5,    1,    2,    3,
    1,    2,    1,    1,    1,    2,    7,    2,    7,    5,
    6,    5,    5,    5,    6,    4,    4,    5,    5,    6,
    5,    6,    4,    5,    6,    5,    5,    4,    4,    4,
    4,    5,    6,    7,    6,    6,    7,    5,    6,    5,
    5,    6,    3,    4,    5,    7,    4,    5,    6,    6,
    4,    5,    7,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   80,   73,   74,   75,   76,   72,    0,   29,   28,
    0,    0,    0,   71,    0,    0,    0,    0,    0,    0,
    3,    4,    0,    0,  114,  115,   26,   27,   30,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   64,    0,
    0,    0,    0,    0,    0,   79,  215,    0,    0,    0,
    0,    0,    0,    0,    5,    6,    0,    0,    0,    0,
    0,    8,    0,    7,    0,    0,    0,    0,    0,   65,
    0,    0,    0,    0,    0,    0,    0,    0,   86,   77,
    0,    0,   83,    0,    0,    0,  158,  159,  157,  160,
  161,  162,  156,  147,  146,  164,  163,    0,    0,    0,
    0,    0,    0,    0,    0,  145,    0,    0,    0,    0,
    0,    0,    0,   31,    0,    0,    0,   49,   48,   13,
    0,    0,   42,   47,    0,    0,    0,    0,    0,    0,
    0,  105,  106,  100,    0,    0,  101,  118,    0,    0,
  116,   78,    0,    0,    0,    0,    0,    0,   62,   54,
   52,   53,   55,   56,   57,   58,    0,   50,    0,    0,
    0,    0,  169,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   15,    0,    0,    0,   43,   14,    0,
  166,    0,   81,   66,   82,    0,    0,  109,  110,  112,
  111,  113,    0,  107,   99,    0,    0,    0,    0,  117,
   84,    0,    0,    0,    0,   12,   51,    0,    0,    0,
    0,  154,    0,  152,  153,    0,    0,    0,    0,    0,
    0,   35,    0,   33,   36,   37,   32,   17,   16,   46,
   45,   44,    0,    0,    0,    0,  108,  102,    0,    0,
   40,    0,    0,   59,  204,  203,    0,  201,    0,    0,
    0,    0,  170,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  175,    0,
    0,  181,    0,    0,    0,    0,    0,   41,    0,   63,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,   39,    0,    0,    0,    0,  218,    0,    0,  216,
    0,  213,  214,    0,    0,  211,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  119,  120,  121,  122,
  123,  124,  125,  126,  127,  128,    0,  129,  130,  141,
  142,  143,  144,  132,  134,  135,  136,  137,  133,  131,
  139,  140,  138,    0,    0,    0,    0,    0,    0,   92,
  176,    0,  182,    0,    0,    0,    0,    0,   87,    0,
   88,  202,    0,  151,  148,  150,    0,    0,    0,    0,
   38,   90,    0,    0,  165,    0,    0,    0,    0,  212,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  205,    0,  187,    0,    0,    0,
    0,    0,   93,    0,    0,   89,    0,    0,    0,   91,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  233,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   94,    0,  172,    0,
  173,    0,    0,  220,    0,  234,  264,    0,  242,  251,
    0,  237,  267,  255,  236,  269,  262,  258,    0,    0,
  248,    0,  224,  223,  250,  270,    0,    0,  222,  155,
  168,    0,    0,    0,    0,    0,    0,    0,  206,    0,
    0,  189,    0,    0,  190,    0,  228,    0,    0,   95,
  149,    0,    0,    0,    0,    0,  208,  221,  265,  252,
  259,  249,  225,  246,  260,    0,    0,    0,    0,  245,
  235,    0,    0,    0,    0,  192,    0,  188,    0,    0,
  232,  174,  217,  171,    0,  219,  209,  247,  263,    0,
  207,  256,    0,  200,  194,  199,  195,  193,  191,  210,
  197,    0,  198,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   34,   12,   13,   14,  177,  141,  133,   50,
  142,  517,  219,   51,   52,   36,  134,  126,  267,  143,
  589,  178,   61,   62,  102,  103,   98,  160,  328,   69,
  156,  157,  213,  214,  161,  417,  434,  590,  184,  621,
  360,  562,  645,  591,  329,  330,  331,  332,  333,  518,
  583,  634,  635,  687,  268,  514,  515,  646,  647,  365,
  366,
  };
  protected static readonly short [] yySindex = {         1813,
  -45, -129,  -18,    3,   29, 2903, 3050, -201,    0, 1813,
    0,    0,    0,    0,  -95,   85,  112,   49, -101,  -26,
    0,    0,    0,    0,    0,    0,    0, 3158,    0,    0,
 3123, -114,  -34,    0,  185, 1066,  -22, 3158,  -20,  172,
    0,    0,    2,   45,    0,    0,    0,    0,    0, 3158,
  -87, -140, -119,   -8,  207,  -25,  131,  -19,    0,  185,
  -32,  238,   62, 3158,   70,    0,    0,  -17, 3158,  276,
 2980,  -16,  276,  147,    0,    0,  567, 3158,  -87, 3158,
  -87,    0,  233,    0, -180,  317,  235, 3074,  276,    0,
 3158, 3158,    9, 3158,  276,  -13,  388, -153,    0,    0,
  185,  115,    0,  276, -153, 4243,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   12,  322,  328,
  333, 3165, 3165, 3165,  334,    0,  567, 3158,  567, 3158,
  318,  321,  120,    0, -180, 3079,    0,    0,    0,    0,
   -5,  567,    0,    0, -140,  185,    4,  320,   26,   78,
  276,    0,    0,    0,  -27,  137,    0,    0,   78, -218,
    0,    0, 3037,   78,   78,   78,  323,  345,    0,    0,
    0,    0,    0,    0,    0,    0, 3069,    0,  347, 3165,
 3165, 3165,    0,   10,   16,   25,   34,  351,  567,  355,
 1696, 4299,  123,    0, -180,  191,    6,    0,    0, 3101,
    0,   78,    0,    0,    0,  -92, -140,    0,    0,    0,
    0,    0,  -98,    0,    0, 2957,  -90,  126, -106,    0,
    0,   78,   78,  142, 4325,    0,    0, 3158,   36,   38,
   39,    0, 3165,    0,    0,  148,   51,  368,   57,   58,
  372,    0,  381,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -85, 3766, -104,   78,    0,    0, 3766,  -94,
    0,  152, 3766,    0,    0,    0,  217,    0,   86, 3158,
 3158, 3158,    0,  157,  165,   73,  178,  181,   77,  259,
 3766,  -86,  384, 3165, -130, 3165, 1774, 3158, 1774, 3158,
 1774, 3158, 3158, 3158, 3158, 3158, 3158, 1774,  360, 1661,
 3158, 3158, 3158, 3165, 3165, 3165, 3158, 3158, 3165,  353,
 3165, 3165, 3165, 3165, 3165, 3165, 3165, 3165, 3165, 3165,
 3165, 1149, 3127, 3158, 3158, 1066,   54, 1810,    0, 3766,
  157,    0,  157, 3766,  -81, 1886, 3766,    0, 1962,    0,
 4325, 3165,  257,  308,  310,  179,  157,  197,  157,    0,
  200,    0,  219, 2038, 3766, 4146,    0,  186, 1760,    0,
  414,    0,    0,  567, 1774,    0,  567,  567, 1774,  567,
  567, 1774,  567,  567,  567,  567,  567,  567,  567, 1774,
 3158,  567, 3158,  567,  567,  567,  567,  416,  417,  420,
  141,  170,  421, 3158,  273,  101,  104,  108,  110,  116,
  117,  119,  127,  128,  143,  144,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3158,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3158,   20,  567,   69, 3158, 1066,    0,
    0,  157,    0,  179,  179, 2114, 3766,  -83,    0, 2190,
    0,    0,  433,    0,    0,    0,  179,  157,  179,  157,
    0,    0, 2266,  157,    0,  441,  220,  463,  567,    0,
  466,  468,  567,  479,  481,  567,  482,  484,  485,  488,
  489,  490,  492,  567,  567,  493,  567,  496,  497,  502,
  505, 3165, 3165, 3165,  203, 3158, 3158,  279, 3165, 3158,
 3158, 3158, 3158, 3158, 3158, 3158, 3158, 3158, 3158, 3158,
  567,  567, 1760,  519,    0,  525,    0,  474,   69,   69,
 3158,  179,    0, 2342, 3766,    0, 3165,  179,  179,    0,
  179,  220,  480, 1760,  528, 1760, 1760,  529, 1760, 1760,
  531, 1760, 1760, 1760, 1760, 1760, 1760, 1760,  535,  536,
 1760,  538, 1760, 1760, 1760, 1760,    0,  539,  540,  329,
  567,  541,  548, 3165,  552,  185,  185,  185,  185,  185,
  185,  185,  185,  185,  185,  185,  554,  556,  557,  483,
 3165, 3021,   78,  474,  474,   69,    0, 2418,    0,  227,
    0,  558, 3158,    0, 1760,    0,    0, 1760,    0,    0,
 1760,    0,    0,    0,    0,    0,    0,    0, 1760, 1760,
    0, 1760,    0,    0,    0,    0, 3165, 3165,    0,    0,
    0,  246,  249,  563, 3165, 1760, 1760, 1760,    0,  564,
 3129,    0, 1616,  262,    0,   78,    0,   78,  474,    0,
    0, 3165,  220, 2868,  566, 3136,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  356,  357, 3165,  568,    0,
    0,  522, 3165,  579, 1582,    0, 1741,    0, 3152,   78,
    0,    0,    0,    0,  220,    0,    0,    0,    0,  568,
    0,    0, 1650,    0,    0,    0,    0,    0,    0,    0,
    0,  269,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  620,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  332,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    1,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  581,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  156,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  581,    0,  581,    0,
    0,    0,    0,    0,    0,    0,  537,    0,    0,    0,
    0,  581,    0,    0,    0,   15,  581,    0,  581,    0,
    0,    0,    0,    0,  177,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   92, 3168, 3191,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  581,    0,
  581,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  270,    0,    0,    0,    0,    0,    0,    0,
    0,  121,  174,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  199,    0,    0,
    0,    0,  292,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  581,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2494,    0, 3842,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  581,  581,  581,  407,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  581,    0,    0,  581,  581,    0,  581,
  581,    0,  581,  581,  581,  581,  581,  581,  581,    0,
    0,  581,    0,  581,  581,  581,  581,    0,    0,    0,
  581,  581,    0,    0,  581,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  581,  581,    0,    0,    0,    0,
    0, 2570,    0, 2646, 3918,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  470,  478,  530,    0,
    0,    0,    0, 3994,    0,    0,    0,    0,  581,    0,
    0,    0,  581,    0,    0,  581,    0,    0,    0,    0,
    0,    0,    0,  581,  581,    0,  581,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  581,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  581,  581,    0, 3210,    0,    0,    0,    0,    0,    0,
    0, 2722,    0,    0,    0,    0,    0,  892, 1234,    0,
 4070,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  581,    0,    0,    0,    0,  617,  697,  777,  879,  959,
 1039, 1119, 1221, 1301, 1381, 1461,    0,    0,    0,    0,
    0,    0, 3290,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  581,    0,    0, 3370,    0, 3450,    0,    0,
    0,    0,    0,  581,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3530,    0,
    0,    0,    0,  294,  581,    0,    0,    0,    0, 3610,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3690,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  612,  569,    0,    0,    0,    0,  495,  494,   22,
   -6,  180, 1631,   18,    0,  610,  437, -113, -277,    0,
  -75,  462,  582,   -2,    0,  487,   44,  -70, -253, -312,
    0,  431,   21, -211, -109,    0,    0, -548,  230,    0,
 -456,  151,    0,   13, -138,    0,  330,  338,  295, -437,
 -486,    0,  -10,    0,  336,    0,   94,    0,   32, -144,
 -203,
  };
  protected static readonly short [] yyTable = {            35,
   35,  257,  353,   37,   39,  336,   57,   87,   64,  339,
  533,   91,  144,  438,   66,   15,  263,   71,  334,   71,
   71,   35,   71,   71,   60,   58,   71,  354,  337,   35,
  254,   35,  259,   68,  166,   72,  355,  281,  200,  525,
   69,  447,   18,   77,   67,   66,  183,  183,  183,  200,
  220,   83,   91,  233,   69,  218,  220,   60,   68,  233,
  144,   66,   35,   19,  101,  203,   96,   66,  233,   79,
   81,  127,   78,  129,   80,  592,  659,  234,  245,  206,
  446,  584,  585,  450,  146,  147,  158,  149,  217,   20,
  155,   96,   90,  222,  223,  131,  220,  637,  638,  132,
  128,  463,  130,   40,  229,  230,  231,  220,   71,  680,
  513,  266,  220,  220,   31,  159,  105,  235,  205,  199,
   97,  189,  165,  191,  252,   67,  521,   66,  123,  342,
  249,  253,  145,  148,  232,  358,   45,   46,  150,   68,
   16,   17,   63,  220,  369,   43,  372,  164,  639,   45,
   46,  158,  671,  380,  359,  162,  101,  273,  163,  124,
  194,  470,  202,  195,   42,  470,  266,  262,  470,  262,
   45,   46,   44,   98,   47,   48,  470,  215,   54,  262,
  216,  218,   66,  218,  495,  335,  673,  262,  218,  441,
  262,  122,  218,  524,  207,   69,   85,  441,   21,   85,
  441,  208,  209,  210,  211,  212,   47,   48,  357,  155,
  361,   66,  158,  496,  158,  441,   69,  103,  690,  158,
  103,  269,   65,  158,  256,  220,   66,  266,  388,  389,
  390,  248,   74,  393,  195,  396,  397,  398,  399,  400,
  401,  402,  403,  404,  405,  406,   85,   55,   86,   70,
   56,   73,   89,   88,   95,  104,  125,  340,  151,  461,
  341,   75,  341,  343,  344,  345,  453,  641,  198,  106,
  642,  588,  208,  209,  210,  211,  212,   71,   82,  198,
  364,  367,  368,  370,  371,  373,  374,  375,  376,  377,
  378,  379,  382,  384,  385,  386,  387,  454,   66,  352,
  391,  392,  668,  395,   76,  669,  188,  441,  190,  693,
  104,  441,  341,  104,   66,   97,  499,  435,  436,   35,
   66,  201,  564,  437,  441,  107,  108,  109,   92,  110,
  111,  112,   34,  113,  196,   34,   94,  196,   45,   46,
  114,  115,   47,   48,   49,   29,   30,  116,  455,   66,
  456,   66,  185,  186,  117,   64,  135,  136,  469,   96,
   96,  180,  473,   96,   96,  476,   96,  181,  238,  179,
  241,   69,  182,  484,  485,  192,  487,  187,  193,   96,
   96,  204,  158,  224,  225,  441,  228,  498,   97,   97,
  236,   69,   97,   97,  237,   97,  246,   96,  239,  261,
  270,  264,  271,  272,  274,  692,   18,  275,   97,   97,
  511,  276,   32,  277,  278,  279,  557,  558,  559,   32,
  280,  347,   69,  565,  118,  338,   97,  512,  154,  348,
  218,   35,   35,  351,  349,  519,  520,  350,  119,  120,
  121,   98,   98,   33,  356,   98,   98,   32,   98,  441,
   33,  439,  262,  458,   69,  257,  460,  467,  465,  492,
  493,   98,   98,  494,  497,  500,   21,   21,  501,   19,
   21,   21,  502,   21,  503,   31,  527,   20,   33,   98,
  504,  505,   31,  506,  532,  358,   21,   21,  624,  561,
  561,  507,  508,  566,  567,  568,  569,  570,  571,  572,
  573,  574,  575,  576,   21,  630,  534,  509,  510,  536,
   31,  537,  636,  582,   35,  107,  108,  109,  586,  110,
  111,  112,  539,  113,  540,  542,  220,  543,  544,   25,
  674,  545,  546,  547,  265,  548,  551,  116,  466,  553,
  554,  654,  655,  468,  117,  555,  471,  472,  556,  474,
  475,  686,  477,  478,  479,  480,  481,  482,  483,  560,
  220,  486,  580,  488,  489,  490,  491,  670,  581,  266,
  593,  595,  598,  513,  601,  633,   70,  167,  609,  610,
  167,  612,  617,  618,  622,  619,  644,  682,   69,   69,
   69,  623,   69,   69,   69,  625,   69,  626,  167,  627,
  628,  643,  656,   69,   69,  657,  658,  663,   66,  675,
   69,  642,  678,  679,  681,  516,   21,   69,  683,    1,
   69,   41,   84,   21,  665,   22,  123,   53,  196,  167,
  197,  247,   22,   23,   24,   25,   26,   27,  227,  644,
   23,   24,   25,   26,   27,   93,  258,  563,  535,  221,
  464,   21,  538,  667,  672,  541,   69,  124,  689,  442,
   22,  167,  633,  549,  550,  152,  552,  443,   23,   24,
   25,   26,   27,  629,   18,   18,  452,  677,   18,   18,
  153,   18,    0,    0,    0,    0,    0,   69,    0,  122,
  577,  578,  579,    0,   18,   18,    0,    0,    0,  381,
    0,   69,   69,   69,    0,    0,    0,    0,    0,    0,
  394,    0,   18,  594,    0,  596,  597,    0,  599,  600,
    0,  602,  603,  604,  605,  606,  607,  608,    0,    0,
  611,    0,  613,  614,  615,  616,   69,   19,   19,    0,
  620,   19,   19,    0,   19,   20,   20,    0,    0,   20,
   20,    0,   20,    0,    0,    0,    0,   19,   19,    0,
    0,    0,    0,    0,    0,   20,   20,    0,    0,    0,
    0,    0,    0,    0,  648,   19,    0,  649,    0,    0,
  650,    0,    0,   20,    0,    0,    0,    0,  651,  652,
    0,  653,    0,    0,    0,    0,    0,   25,   25,    0,
    0,   25,   25,    0,   25,  660,  661,  662,    0,  167,
  167,    0,  666,    0,    0,    0,   69,   25,   25,    0,
    0,    0,    0,  107,  108,  109,    0,  110,  111,  112,
    0,  113,    0,    0,    0,   25,    0,    0,  114,  115,
    0,    0,    0,    0,    0,  116,  688,    0,    0,  167,
  167,  167,  117,    0,    0,    0,    0,    0,    0,    0,
  167,    0,    0,  167,  167,  167,  167,  167,  167,  167,
  167,  167,  167,    0,  167,  167,    0,  167,  167,  167,
  167,  167,  167,  167,    0,    0,  167,  167,  167,  266,
  266,   22,  167,    0,    0,    0,  167,  167,  167,  167,
  167,  167,  167,  167,  167,  167,  167,    0,  167,    0,
  167,    0,    0,    0,    0,    0,    0,    0,   69,    0,
    0,  167,  118,    0,    0,    0,    0,    0,    0,  266,
  266,  266,  167,  167,  167,  167,  119,  120,  121,    0,
  266,    0,    0,  266,  266,  266,  266,  266,  266,  266,
  266,  266,  266,    0,  266,  266,    0,  266,  266,  266,
  266,  266,  266,  266,    0,    0,  266,  266,  266,  271,
  271,    0,  266,    0,    0,    0,  266,  266,  266,  266,
  266,    0,  266,  266,  266,  266,  266,    0,  266,    0,
  266,    0,    0,    0,    0,    0,    0,    0,   69,    0,
    0,  266,    0,    0,    0,    0,    0,    0,    0,  271,
  271,  271,  266,  266,  266,  266,    0,    0,    0,    0,
  271,    0,    0,  271,  271,  271,  271,  271,  271,  271,
  271,  271,  271,    0,  271,  271,    0,  271,  271,  271,
  271,  271,  271,  271,    0,    0,  271,  271,  271,  257,
  257,    0,  271,    0,    0,    0,  271,  271,  271,  271,
  271,    0,  271,  271,  271,  271,  271,    0,  271,    0,
  271,    0,    0,    0,    0,    0,    0,    0,   69,    0,
    0,  271,    0,    0,    0,    0,    0,    0,    0,  257,
  257,  257,  271,  271,  271,  271,    0,    0,    0,    0,
  257,    0,    0,  257,  257,  257,  257,  257,  257,  257,
  257,  257,  257,    0,  257,  257,    0,  257,  257,  257,
  257,  257,  257,  257,    0,   32,  257,  257,  257,    0,
    0,    0,  257,    0,    0,    0,  257,  257,  257,  257,
  257,    0,  257,  257,  257,  257,  257,    0,  257,    0,
  257,  241,  241,    0,    0,    0,   33,    0,   69,   22,
   22,  257,    0,   22,   22,    0,   22,    0,    0,    0,
    0,    0,  257,  257,  257,  257,    0,    0,    0,   22,
   22,    0,    0,    0,    0,    0,    0,    0,   31,    0,
    0,  241,  241,  241,    0,    0,    0,   22,    0,    0,
    0,    0,  241,    0,    0,  241,  241,  241,  241,  241,
  241,  241,  241,  241,  241,    0,  241,  241,    0,  241,
  241,  241,  241,  241,  241,  241,    0,    0,  241,  241,
  241,  238,  238,   24,  241,    0,    0,    0,  241,  241,
  241,  241,  241,    0,  241,  241,  241,  241,  241,    0,
  241,    0,  241,    0,    0,    0,    0,    0,    0,    0,
   69,    0,    0,  241,    0,    0,    0,    0,    0,    0,
    0,  238,  238,  238,  241,  241,  241,  241,    0,    0,
    0,    0,  238,    0,    0,  238,  238,  238,  238,  238,
  238,  238,  238,  238,  238,    0,  238,  238,    0,  238,
  238,  238,  238,  238,  238,  238,    0,    0,  238,  238,
  238,  239,  239,    0,  238,    0,    0,    0,  238,  238,
  238,  238,  238,    0,  238,  238,  238,  238,  238,   21,
  238,    0,  238,    0,    0,    0,    0,    0,   22,    0,
   69,    0,    0,  238,    0,    0,   23,   24,   25,   26,
   27,  239,  239,  239,  238,  238,  238,  238,    0,    0,
    0,    0,  239,    0,   67,  239,  239,  239,  239,  239,
  239,  239,  239,  239,  239,    0,  239,  239,    0,  239,
  239,  239,  239,  239,  239,  239,    0,    0,  239,  239,
  239,  240,  240,    0,  239,    0,    0,    0,  239,  239,
  239,  239,  239,    0,  239,  239,  239,  239,  239,    0,
  239,    0,  239,    0,    0,    0,    0,    0,    0,    0,
   69,    0,    0,  239,    0,    0,    0,    0,    0,    0,
    0,  240,  240,  240,  239,  239,  239,  239,    0,    0,
    0,    0,  240,    0,    0,  240,  240,  240,  240,  240,
  240,  240,  240,  240,  240,    0,  240,  240,    0,  240,
  240,  240,  240,  240,  240,  240,    0,    0,  240,  240,
  240,    0,    0,    0,  240,    0,    0,    0,  240,  240,
  240,  240,  240,    0,  240,  240,  240,  240,  240,    0,
  240,    0,  240,  268,  268,    0,    0,    0,    0,    0,
   69,   24,   24,  240,    0,   24,   24,    0,   24,    0,
    0,    0,    0,    0,  240,  240,  240,  240,    0,    0,
    0,   24,   24,  407,  408,  409,  410,  411,  412,  413,
  414,  415,  416,  268,  268,  268,    0,    0,    0,   24,
    0,    0,    0,    0,  268,    0,    0,  268,  268,  268,
  268,  268,  268,  268,  268,  268,  268,    0,  268,  268,
    0,  268,  268,  268,  268,  268,  268,  268,    0,    0,
  268,  268,  268,  261,  261,    0,  268,    0,    0,    0,
  268,  268,  268,  268,  268,    0,  268,  268,  268,  268,
  268,    0,  268,    0,  268,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  268,    0,    0,    0,    0,
    0,    0,    0,  261,  261,  261,  268,  268,  268,  268,
    0,    0,    0,   66,  261,    0,    0,  261,  261,  261,
  261,  261,  261,  261,  261,  261,  261,    0,  261,  261,
    0,  261,  261,  261,  261,  261,  261,  261,    0,    0,
  261,  261,  261,  254,  254,    0,  261,   66,    0,    0,
  261,  261,  261,  261,  261,    0,  261,  261,  261,  261,
  261,    0,  261,    0,  261,  123,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  261,    0,    0,    0,    0,
  691,    0,    0,  254,  254,  254,  261,  261,  261,  261,
    0,    0,    0,    0,  254,    0,  124,  254,  254,  254,
  254,  254,  254,  254,  254,  254,  254,    0,  254,  254,
   32,  254,  254,  254,  254,  254,  254,  254,    0,    0,
  254,  254,  254,  226,  226,    0,  254,   66,  122,  240,
  254,  254,  254,  254,  254,    0,  254,  254,  254,  254,
  254,   33,  254,    0,  254,  123,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  254,    0,    0,    0,    0,
    0,    0,    0,  226,  226,  226,  254,  254,  254,  254,
    0,    0,    0,   31,  226,    0,  124,  226,  226,  226,
  226,  226,  226,  226,  226,  226,  226,    0,  226,  226,
  123,  226,  226,  226,  226,  226,  226,  226,    0,    0,
  226,  226,  226,    0,    0,    0,  226,    0,  122,  123,
  226,  226,  226,  226,  226,    0,  226,  226,  226,  226,
  226,  124,  226,   32,  226,    0,  255,    0,  107,  108,
  109,    0,  110,  111,  112,  226,  113,  260,    0,    0,
  124,    0,    0,  684,  685,    0,  226,  226,  226,  226,
  116,    0,    0,  122,   33,    0,    0,  117,    0,    0,
    0,    0,  107,  108,  109,    0,  110,  111,  112,    0,
  113,    0,  122,  282,    0,    0,    0,  114,  115,    0,
    0,    0,    0,    0,  116,    0,   31,    0,    0,    0,
    0,  117,    0,    0,  346,    0,  107,  108,  109,    0,
  110,  111,  112,    0,  113,  208,  209,  210,  211,  212,
    0,    0,    0,    0,   21,  265,    0,    0,  116,    0,
    0,    0,    0,   22,  440,  117,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,    0,  107,  108,  109,    0,  110,  111,  112,    0,
  113,  444,    0,  445,    0,  448,    0,  114,  115,    0,
    0,  118,    0,    0,  116,    0,    0,  457,    0,  459,
    0,  117,    0,    0,    0,  119,  120,  121,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  107,  108,  109,
  383,  110,  111,  112,    0,  113,    0,    0,    0,    0,
  449,    0,  114,  115,    0,    0,  107,  108,  109,  116,
  110,  111,  112,    0,  113,    0,  117,    0,    0,    0,
    0,  114,  115,    0,    0,    0,    0,   21,  116,    0,
  208,  209,  210,  211,  212,  117,   22,    0,    0,    0,
    0,  118,    0,    0,   23,   24,   25,   26,   27,    0,
    0,    0,    0,    0,    0,  119,  120,  121,    0,    0,
    0,    0,  522,    0,    0,    0,    0,    0,    0,    0,
    1,    2,  283,    0,    3,    4,  451,    5,  528,    0,
  529,    0,    0,    0,  531,    0,  118,    0,  362,  363,
    6,    7,    0,    0,    0,    0,    0,    0,    0,    0,
  119,  120,  121,    0,    0,  118,    0,    0,    8,    0,
    0,    0,  284,  285,  286,    0,    0,    0,    0,  119,
  120,  121,    0,  287,    0,    0,  288,  289,  290,  291,
  292,  293,  294,  295,  296,  297,    0,  298,  299,    0,
  300,  301,  302,  303,  304,  305,  306,    0,  283,  307,
  308,  309,  462,    0,    0,  310,    0,    0,    0,  311,
  312,  313,  314,  315,    0,  316,  317,  318,  319,  320,
    0,  321,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  323,    0,    0,    0,  284,  285,
  286,    0,    0,    0,    0,  324,  325,  326,  327,  287,
    0,    0,  288,  289,  290,  291,  292,  293,  294,  295,
  296,  297,    0,  298,  299,    0,  300,  301,  302,  303,
  304,  305,  306,    0,  283,  307,  308,  309,  523,    0,
    0,  310,    0,    0,    0,  311,  312,  313,  314,  315,
    0,  316,  317,  318,  319,  320,    0,  321,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,    0,    0,    0,  284,  285,  286,    0,    0,    0,
    0,  324,  325,  326,  327,  287,    0,    0,  288,  289,
  290,  291,  292,  293,  294,  295,  296,  297,    0,  298,
  299,    0,  300,  301,  302,  303,  304,  305,  306,    0,
  283,  307,  308,  309,  526,    0,    0,  310,    0,    0,
    0,  311,  312,  313,  314,  315,    0,  316,  317,  318,
  319,  320,    0,  321,    0,  322,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  323,    0,    0,    0,
  284,  285,  286,    0,    0,    0,    0,  324,  325,  326,
  327,  287,    0,    0,  288,  289,  290,  291,  292,  293,
  294,  295,  296,  297,    0,  298,  299,    0,  300,  301,
  302,  303,  304,  305,  306,    0,  283,  307,  308,  309,
  530,    0,    0,  310,    0,    0,    0,  311,  312,  313,
  314,  315,    0,  316,  317,  318,  319,  320,    0,  321,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  323,    0,    0,    0,  284,  285,  286,    0,
    0,    0,    0,  324,  325,  326,  327,  287,    0,    0,
  288,  289,  290,  291,  292,  293,  294,  295,  296,  297,
    0,  298,  299,    0,  300,  301,  302,  303,  304,  305,
  306,    0,  283,  307,  308,  309,  587,    0,    0,  310,
    0,    0,    0,  311,  312,  313,  314,  315,    0,  316,
  317,  318,  319,  320,    0,  321,    0,  322,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  323,    0,
    0,    0,  284,  285,  286,    0,    0,    0,    0,  324,
  325,  326,  327,  287,    0,    0,  288,  289,  290,  291,
  292,  293,  294,  295,  296,  297,    0,  298,  299,    0,
  300,  301,  302,  303,  304,  305,  306,    0,  283,  307,
  308,  309,  640,    0,    0,  310,    0,    0,    0,  311,
  312,  313,  314,  315,    0,  316,  317,  318,  319,  320,
    0,  321,    0,  322,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  323,    0,    0,    0,  284,  285,
  286,    0,    0,    0,    0,  324,  325,  326,  327,  287,
    0,    0,  288,  289,  290,  291,  292,  293,  294,  295,
  296,  297,    0,  298,  299,    0,  300,  301,  302,  303,
  304,  305,  306,    0,  283,  307,  308,  309,  179,    0,
    0,  310,    0,    0,    0,  311,  312,  313,  314,  315,
    0,  316,  317,  318,  319,  320,    0,  321,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,    0,    0,    0,  284,  285,  286,    0,    0,    0,
    0,  324,  325,  326,  327,  287,    0,    0,  288,  289,
  290,  291,  292,  293,  294,  295,  296,  297,    0,  298,
  299,    0,  300,  301,  302,  303,  304,  305,  306,    0,
  283,  307,  308,  309,  177,    0,    0,  310,    0,    0,
    0,  311,  312,  313,  314,  315,    0,  316,  317,  318,
  319,  320,    0,  321,    0,  322,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  323,    0,    0,    0,
  284,  285,  286,    0,    0,    0,    0,  324,  325,  326,
  327,  287,    0,    0,  288,  289,  290,  291,  292,  293,
  294,  295,  296,  297,    0,  298,  299,    0,  300,  301,
  302,  303,  304,  305,  306,    0,  179,  307,  308,  309,
  180,    0,    0,  310,    0,    0,    0,  311,  312,  313,
  314,  315,    0,  316,  317,  318,  319,  320,    0,  321,
    0,  322,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  323,    0,    0,    0,  179,  179,  179,    0,
    0,    0,    0,  324,  325,  326,  327,  179,    0,    0,
  179,  179,  179,  179,  179,  179,  179,  179,  179,  179,
    0,  179,  179,    0,  179,  179,  179,  179,  179,  179,
  179,    0,  177,  179,  179,  179,  178,    0,    0,  179,
    0,    0,    0,  179,  179,  179,  179,  179,    0,  179,
  179,  179,  179,  179,    0,  179,    0,  179,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  179,    0,
    0,    0,  177,  177,  177,    0,    0,    0,    0,  179,
  179,  179,  179,  177,    0,    0,  177,  177,  177,  177,
  177,  177,  177,  177,  177,  177,    0,  177,  177,   66,
  177,  177,  177,  177,  177,  177,  177,    0,  180,  177,
  177,  177,    0,    0,    0,  177,    0,    0,    0,  177,
  177,  177,  177,  177,    0,  177,  177,  177,  177,  177,
    0,  177,    0,  177,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  177,    0,    0,    0,  180,  180,
  180,    0,   32,    0,    0,  177,  177,  177,  177,  180,
    0,    0,  180,  180,  180,  180,  180,  180,  180,  180,
  180,  180,    0,  180,  180,    0,  180,  180,  180,  180,
  180,  180,  180,   33,  178,  180,  180,  180,    0,    0,
    0,  180,    0,    0,    0,  180,  180,  180,  180,  180,
    0,  180,  180,  180,  180,  180,   32,  180,    0,  180,
  100,    0,    0,    0,    0,   31,    0,    0,    0,    0,
  180,    0,    0,    0,  178,  178,  178,    0,    0,   32,
    0,  180,  180,  180,  180,  178,    0,   33,  178,  178,
  178,  178,  178,  178,  178,  178,  178,  178,    0,  178,
  178,  632,  178,  178,  178,  178,  178,  178,  178,    0,
   33,  178,  178,  178,    0,    0,    0,  178,    0,   31,
   32,  178,  178,  178,  178,  178,    0,  178,  178,  178,
  178,  178,    0,  178,    0,  178,   32,    0,    0,    0,
    0,    0,   31,    0,    0,    0,  178,    0,    0,   32,
    0,   33,    0,    0,    0,    0,    0,  178,  178,  178,
  178,    0,    0,    0,  107,  108,  109,   33,  110,  111,
  112,    0,  113,   32,    0,    0,    0,    0,   32,    0,
   33,    0,    0,   31,    0,    0,  116,    0,    0,    0,
    0,    0,    0,  117,    0,    0,    0,    0,    0,   31,
   32,    0,    0,    0,   33,    0,   21,    0,    0,   33,
    0,    0,   31,    0,    0,   22,    0,    0,    0,    0,
    0,    0,   32,   23,   24,   25,   26,   27,   32,    0,
    0,   33,    0,  226,   28,   32,   31,    0,  140,   29,
   30,   31,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   32,    0,   33,    0,    0,    0,   32,    0,   33,
   21,    0,    0,   31,   32,    0,   33,    0,  676,   22,
    0,    0,    0,    0,  152,    0,    0,   23,   24,   25,
   26,   27,   33,   21,    0,   31,    0,   59,   33,  153,
    0,   31,   22,    0,    0,   33,    0,    0,   31,    0,
   23,   24,   25,   26,   27,    0,    0,    0,    0,    0,
    0,    0,   99,    0,   31,    0,    0,    0,    0,    0,
   31,    0,    0,    0,   21,    0,    0,   31,    0,    0,
    0,    0,   60,   22,    0,    0,    0,    0,  631,    0,
   21,   23,   24,   25,   26,   27,    0,    0,    0,   22,
    0,    0,    0,   21,    0,   61,    0,   23,   24,   25,
   26,   27,   22,    0,    0,    0,    0,    0,  167,   99,
   23,   24,   25,   26,   27,    0,    0,  137,  138,    0,
    0,   38,  137,  138,  168,    0,   22,  139,    0,    0,
    0,   22,  139,    0,   23,   24,   25,   26,   27,   23,
   24,   25,   26,   27,  137,  250,    0,    0,    0,    0,
    0,  169,  170,   22,  251,  171,  172,  173,  174,  175,
  176,   23,   24,   25,   26,   27,   21,  418,  419,    0,
    0,    0,   21,    0,    0,   22,    0,    0,    0,   21,
    0,   22,  664,   23,   24,   25,   26,   27,   22,   23,
   24,   25,   26,   27,    0,   21,   23,   24,   25,   26,
   27,   21,    0,    0,   22,    0,    0,   60,  137,  631,
   22,    0,   23,   24,   25,   26,   27,   22,   23,   24,
   25,   26,   27,   60,    0,   23,   24,   25,   26,   27,
   61,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   61,    0,    0,    0,
   60,   60,    0,    0,   60,   60,   60,   60,   60,   60,
    0,    0,  253,  253,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   61,   61,    0,    0,   61,   61,   61,
   61,   61,   61,  420,  421,  422,  423,    0,    0,    0,
    0,    0,  424,  425,  426,  427,  428,  429,  430,  431,
  432,  433,  253,  253,  253,    0,    0,    0,    0,    0,
    0,    0,    0,  253,    0,    0,  253,  253,  253,  253,
  253,  253,  253,  253,  253,  253,    0,  253,  253,    0,
  253,  253,  253,  253,  253,  253,  253,    0,    0,  253,
  253,  253,  227,  227,    0,  253,    0,    0,    0,  253,
  253,  253,  253,  253,    0,  253,  253,  253,  253,  253,
    0,  253,    0,  253,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  253,    0,    0,    0,    0,    0,
    0,    0,  227,  227,  227,  253,  253,  253,  253,    0,
    0,    0,    0,  227,    0,    0,  227,  227,  227,  227,
  227,  227,  227,  227,  227,  227,    0,  227,  227,    0,
  227,  227,  227,  227,  227,  227,  227,    0,    0,  227,
  227,  227,  229,  229,    0,  227,    0,    0,    0,  227,
  227,  227,  227,  227,    0,  227,  227,  227,  227,  227,
    0,  227,    0,  227,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  227,    0,    0,    0,    0,    0,
    0,    0,  229,  229,  229,  227,  227,  227,  227,    0,
    0,    0,    0,  229,    0,    0,  229,  229,  229,  229,
  229,  229,  229,  229,  229,  229,    0,  229,  229,    0,
  229,  229,  229,  229,  229,  229,  229,    0,    0,  229,
  229,  229,  231,  231,    0,  229,    0,    0,    0,  229,
  229,  229,  229,  229,    0,  229,  229,  229,  229,  229,
    0,  229,    0,  229,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  229,    0,    0,    0,    0,    0,
    0,    0,  231,  231,  231,  229,  229,  229,  229,    0,
    0,    0,    0,  231,    0,    0,  231,  231,  231,  231,
  231,  231,  231,  231,  231,  231,    0,  231,  231,    0,
  231,  231,  231,  231,  231,  231,  231,    0,    0,  231,
  231,  231,  243,  243,    0,  231,    0,    0,    0,  231,
  231,  231,  231,  231,    0,  231,  231,  231,  231,  231,
    0,  231,    0,  231,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  231,    0,    0,    0,    0,    0,
    0,    0,  243,  243,  243,  231,  231,  231,  231,    0,
    0,    0,    0,  243,    0,    0,  243,  243,  243,  243,
  243,  243,  243,  243,  243,  243,    0,  243,  243,    0,
  243,  243,  243,  243,  243,  243,  243,    0,    0,  243,
  243,  243,  230,  230,    0,  243,    0,    0,    0,  243,
  243,  243,  243,  243,    0,  243,  243,  243,  243,  243,
    0,  243,    0,  243,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  243,    0,    0,    0,    0,    0,
    0,    0,  230,  230,  230,  243,  243,  243,  243,    0,
    0,    0,    0,  230,    0,    0,  230,  230,  230,  230,
  230,  230,  230,  230,  230,  230,    0,  230,  230,    0,
  230,  230,  230,  230,  230,  230,  230,    0,    0,  230,
  230,  230,  244,  244,    0,  230,    0,    0,    0,  230,
  230,  230,  230,  230,    0,  230,  230,  230,  230,  230,
    0,  230,    0,  230,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  230,    0,    0,    0,    0,    0,
    0,    0,  244,  244,  244,  230,  230,  230,  230,    0,
    0,    0,    0,  244,    0,    0,  244,  244,  244,  244,
  244,  244,  244,  244,  244,  244,    0,  244,  244,    0,
  244,  244,  244,  244,  244,  244,  244,    0,  283,  244,
  244,  244,    0,    0,    0,  244,    0,    0,    0,  244,
  244,  244,  244,  244,    0,  244,  244,  244,  244,  244,
    0,  244,    0,  244,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  244,    0,    0,    0,  284,  285,
  286,    0,    0,    0,    0,  244,  244,  244,  244,  287,
    0,    0,  288,  289,  290,  291,  292,  293,  294,  295,
  296,  297,    0,  298,  299,    0,  300,  301,  302,  303,
  304,  305,  306,    0,  183,  307,  308,  309,    0,    0,
    0,  310,    0,    0,    0,  311,  312,  313,  314,  315,
    0,  316,  317,  318,  319,  320,    0,  321,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,    0,    0,    0,  183,  183,  183,    0,    0,    0,
    0,  324,  325,  326,  327,  183,    0,    0,  183,  183,
  183,  183,  183,  183,  183,  183,  183,  183,    0,  183,
  183,    0,  183,  183,  183,  183,  183,  183,  183,    0,
  184,  183,  183,  183,    0,    0,    0,  183,    0,    0,
    0,  183,  183,  183,  183,  183,    0,  183,  183,  183,
  183,  183,    0,  183,    0,  183,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  183,    0,    0,    0,
  184,  184,  184,    0,    0,    0,    0,  183,  183,  183,
  183,  184,    0,    0,  184,  184,  184,  184,  184,  184,
  184,  184,  184,  184,    0,  184,  184,    0,  184,  184,
  184,  184,  184,  184,  184,    0,  185,  184,  184,  184,
    0,    0,    0,  184,    0,    0,    0,  184,  184,  184,
  184,  184,    0,  184,  184,  184,  184,  184,    0,  184,
    0,  184,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  184,    0,    0,    0,  185,  185,  185,    0,
    0,    0,    0,  184,  184,  184,  184,  185,    0,    0,
  185,  185,  185,  185,  185,  185,  185,  185,  185,  185,
    0,  185,  185,    0,  185,  185,  185,  185,  185,  185,
  185,    0,  186,  185,  185,  185,    0,    0,    0,  185,
    0,    0,    0,  185,  185,  185,  185,  185,    0,  185,
  185,  185,  185,  185,    0,  185,    0,  185,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  185,    0,
    0,    0,  186,  186,  186,    0,    0,    0,    0,  185,
  185,  185,  185,  186,    0,    0,  186,  186,  186,  186,
  186,  186,  186,  186,  186,  186,    0,  186,  186,    0,
  186,  186,  186,  186,  186,  186,  186,    0,    0,  186,
  186,  186,    0,    0,    0,  186,    0,    0,    0,  186,
  186,  186,  186,  186,    0,  186,  186,  186,  186,  186,
    0,  186,    0,  186,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  186,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  186,  186,  186,  186,  287,
    0,    0,  288,  289,  290,  291,  292,  293,  294,  295,
  296,  297,    0,  298,  299,    0,  300,  301,  302,  303,
  304,  305,  306,    0,    0,  307,  308,  309,    0,    0,
    0,  310,  167,    0,    0,  311,  312,  313,  314,  315,
    0,  316,  317,  318,  319,  320,    0,  321,  168,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  324,  325,  326,  327,  169,  170,    0,    0,  171,
  172,  173,  174,  175,  176,  107,  108,  109,  242,  110,
  111,  112,    0,  113,    0,    0,    0,    0,    0,    0,
    0,    0,  243,    0,  244,    0,    0,  116,    0,    0,
    0,  107,  108,  109,  117,  110,  111,  112,    0,  113,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  265,    0,    0,  116,    0,    0,    0,    0,    0,    0,
  117,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  213,  280,    6,    7,  259,   33,   33,  123,  263,
  467,   44,   88,  326,   42,   61,  123,   40,  123,   40,
   40,   28,   40,   40,   31,   28,   40,  281,  123,   36,
  123,   38,  123,   36,  105,   38,  123,  123,   44,  123,
   40,  123,   61,   50,   44,   42,  122,  123,  124,   44,
  160,   60,   44,   44,   40,  274,  166,   64,   44,   44,
  136,   42,   69,   61,   71,   62,   69,   42,   44,   52,
   53,   78,   51,   80,   53,  532,  625,   62,  192,  150,
  334,  519,  520,  337,   91,   92,  305,   94,  159,   61,
   97,    0,  125,  164,  165,  276,  206,  584,  585,  280,
   79,  355,   81,  305,  180,  181,  182,  217,   40,  658,
   91,  225,  222,  223,  123,   98,   73,   93,   93,  125,
    0,  128,  105,  130,  200,  125,  439,   42,   60,   44,
  125,  202,   89,  125,  125,  266,  290,  291,   95,  125,
  270,  271,  257,  253,  289,   61,  291,  104,  586,  290,
  291,  305,  639,  298,  285,   41,  163,  233,   44,   91,
   41,  365,  145,   44,  260,  369,  280,  274,  372,  274,
  290,  291,   61,    0,  294,  295,  380,   41,  280,  274,
   44,  274,   42,  274,   44,  256,  643,  274,  274,  328,
  274,  123,  274,  447,  151,   40,   41,  336,    0,   44,
  339,  300,  301,  302,  303,  304,  294,  295,  284,  216,
  286,   42,  305,   44,  305,  354,   40,   41,  675,  305,
   44,  228,  257,  305,  207,  335,   42,  341,  304,  305,
  306,   41,   61,  309,   44,  311,  312,  313,  314,  315,
  316,  317,  318,  319,  320,  321,   40,  274,  274,  272,
  277,  272,  272,  123,  272,  272,   77,   41,  272,   41,
   44,  260,   44,  270,  271,  272,  342,   41,  274,  123,
   44,  525,  300,  301,  302,  303,  304,   40,  287,  274,
  287,  288,  289,  290,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,   41,   42,   41,
  307,  308,   41,  310,  260,   44,  127,  446,  129,   41,
   41,  450,   44,   44,   42,   40,   44,  324,  325,  326,
   42,  142,   44,  326,  463,  257,  258,  259,  267,  261,
  262,  263,   41,  265,   41,   44,  267,   44,  290,  291,
  272,  273,  294,  295,  296,  297,  298,  279,   41,   42,
   41,   42,  123,  124,  286,  123,   40,  123,  365,  268,
  269,   40,  369,  272,  273,  372,  275,   40,  189,  358,
  191,   40,   40,  380,  381,   58,  383,   44,   58,  288,
  289,   62,  305,   61,   40,  524,   40,  394,  268,  269,
  357,   60,  272,  273,   44,  275,  274,  306,   44,  274,
  365,  260,  365,  365,  257,  683,    0,  357,  288,  289,
  417,   44,   60,  357,  357,   44,  492,  493,  494,   60,
   40,  257,   91,  499,  356,  274,  306,  434,   41,  357,
  274,  438,  439,  357,  257,  438,  439,  257,  370,  371,
  372,  268,  269,   91,   61,  272,  273,   60,  275,  588,
   91,  398,  274,  257,  123,  667,  257,   44,  273,   44,
   44,  288,  289,   44,   44,  365,  268,  269,  365,    0,
  272,  273,  365,  275,  365,  123,   44,    0,   91,  306,
  365,  365,  123,  365,   44,  266,  288,  289,  564,  496,
  497,  365,  365,  500,  501,  502,  503,  504,  505,  506,
  507,  508,  509,  510,  306,  581,   44,  365,  365,   44,
  123,   44,  583,   40,  521,  257,  258,  259,  521,  261,
  262,  263,   44,  265,   44,   44,  636,   44,   44,    0,
  644,   44,   44,   44,  276,   44,   44,  279,  359,   44,
   44,  617,  618,  364,  286,   44,  367,  368,   44,  370,
  371,  665,  373,  374,  375,  376,  377,  378,  379,  357,
  670,  382,   44,  384,  385,  386,  387,  638,   44,  683,
   91,   44,   44,   91,   44,  582,   40,   41,   44,   44,
   44,   44,   44,   44,   44,  257,  593,  663,  257,  258,
  259,   44,  261,  262,  263,   44,  265,   44,   62,   44,
   44,   44,  357,  272,  273,  357,   44,   44,   42,   44,
  279,   44,  257,  257,   93,  436,  264,  286,   40,    0,
   40,   10,   54,  264,  631,  273,   60,   18,  135,   93,
  136,  195,  273,  281,  282,  283,  284,  285,  177,  646,
  281,  282,  283,  284,  285,   64,  216,  497,  469,  163,
  356,  264,  473,  633,  642,  476,   40,   91,  669,  330,
  273,  125,  669,  484,  485,  278,  487,  330,  281,  282,
  283,  284,  285,  580,  268,  269,  341,  646,  272,  273,
  293,  275,   -1,   -1,   -1,   -1,   -1,  356,   -1,  123,
  511,  512,  513,   -1,  288,  289,   -1,   -1,   -1,  340,
   -1,  370,  371,  372,   -1,   -1,   -1,   -1,   -1,   -1,
  358,   -1,  306,  534,   -1,  536,  537,   -1,  539,  540,
   -1,  542,  543,  544,  545,  546,  547,  548,   -1,   -1,
  551,   -1,  553,  554,  555,  556,   40,  268,  269,   -1,
  561,  272,  273,   -1,  275,  268,  269,   -1,   -1,  272,
  273,   -1,  275,   -1,   -1,   -1,   -1,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,  288,  289,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  595,  306,   -1,  598,   -1,   -1,
  601,   -1,   -1,  306,   -1,   -1,   -1,   -1,  609,  610,
   -1,  612,   -1,   -1,   -1,   -1,   -1,  268,  269,   -1,
   -1,  272,  273,   -1,  275,  626,  627,  628,   -1,  273,
  274,   -1,  633,   -1,   -1,   -1,   40,  288,  289,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,  306,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,  667,   -1,   -1,  313,
  314,  315,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,  338,  339,   -1,  341,  342,  343,
  344,  345,  346,  347,   -1,   -1,  350,  351,  352,  273,
  274,    0,  356,   -1,   -1,   -1,  360,  361,  362,  363,
  364,  365,  366,  367,  368,  369,  370,   -1,  372,   -1,
  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,
   -1,  385,  356,   -1,   -1,   -1,   -1,   -1,   -1,  313,
  314,  315,  396,  397,  398,  399,  370,  371,  372,   -1,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,  338,  339,   -1,  341,  342,  343,
  344,  345,  346,  347,   -1,   -1,  350,  351,  352,  273,
  274,   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,
  364,   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,
  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,
   -1,  385,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  313,
  314,  315,  396,  397,  398,  399,   -1,   -1,   -1,   -1,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,  338,  339,   -1,  341,  342,  343,
  344,  345,  346,  347,   -1,   -1,  350,  351,  352,  273,
  274,   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,
  364,   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,
  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   40,   -1,
   -1,  385,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  313,
  314,  315,  396,  397,  398,  399,   -1,   -1,   -1,   -1,
  324,   -1,   -1,  327,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,  338,  339,   -1,  341,  342,  343,
  344,  345,  346,  347,   -1,   60,  350,  351,  352,   -1,
   -1,   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,
  364,   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,
  374,  273,  274,   -1,   -1,   -1,   91,   -1,   40,  268,
  269,  385,   -1,  272,  273,   -1,  275,   -1,   -1,   -1,
   -1,   -1,  396,  397,  398,  399,   -1,   -1,   -1,  288,
  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
   -1,  313,  314,  315,   -1,   -1,   -1,  306,   -1,   -1,
   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,  338,  339,   -1,  341,
  342,  343,  344,  345,  346,  347,   -1,   -1,  350,  351,
  352,  273,  274,    0,  356,   -1,   -1,   -1,  360,  361,
  362,  363,  364,   -1,  366,  367,  368,  369,  370,   -1,
  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   40,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  313,  314,  315,  396,  397,  398,  399,   -1,   -1,
   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,  338,  339,   -1,  341,
  342,  343,  344,  345,  346,  347,   -1,   -1,  350,  351,
  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,  361,
  362,  363,  364,   -1,  366,  367,  368,  369,  370,  264,
  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   40,   -1,   -1,  385,   -1,   -1,  281,  282,  283,  284,
  285,  313,  314,  315,  396,  397,  398,  399,   -1,   -1,
   -1,   -1,  324,   -1,  299,  327,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,  338,  339,   -1,  341,
  342,  343,  344,  345,  346,  347,   -1,   -1,  350,  351,
  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,  361,
  362,  363,  364,   -1,  366,  367,  368,  369,  370,   -1,
  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   40,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  313,  314,  315,  396,  397,  398,  399,   -1,   -1,
   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,  338,  339,   -1,  341,
  342,  343,  344,  345,  346,  347,   -1,   -1,  350,  351,
  352,   -1,   -1,   -1,  356,   -1,   -1,   -1,  360,  361,
  362,  363,  364,   -1,  366,  367,  368,  369,  370,   -1,
  372,   -1,  374,  273,  274,   -1,   -1,   -1,   -1,   -1,
   40,  268,  269,  385,   -1,  272,  273,   -1,  275,   -1,
   -1,   -1,   -1,   -1,  396,  397,  398,  399,   -1,   -1,
   -1,  288,  289,  375,  376,  377,  378,  379,  380,  381,
  382,  383,  384,  313,  314,  315,   -1,   -1,   -1,  306,
   -1,   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,  338,  339,
   -1,  341,  342,  343,  344,  345,  346,  347,   -1,   -1,
  350,  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,
  360,  361,  362,  363,  364,   -1,  366,  367,  368,  369,
  370,   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  313,  314,  315,  396,  397,  398,  399,
   -1,   -1,   -1,   42,  324,   -1,   -1,  327,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,  338,  339,
   -1,  341,  342,  343,  344,  345,  346,  347,   -1,   -1,
  350,  351,  352,  273,  274,   -1,  356,   42,   -1,   -1,
  360,  361,  362,  363,  364,   -1,  366,  367,  368,  369,
  370,   -1,  372,   -1,  374,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,
   41,   -1,   -1,  313,  314,  315,  396,  397,  398,  399,
   -1,   -1,   -1,   -1,  324,   -1,   91,  327,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,  338,  339,
   60,  341,  342,  343,  344,  345,  346,  347,   -1,   -1,
  350,  351,  352,  273,  274,   -1,  356,   42,  123,   44,
  360,  361,  362,  363,  364,   -1,  366,  367,  368,  369,
  370,   91,  372,   -1,  374,   60,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  313,  314,  315,  396,  397,  398,  399,
   -1,   -1,   -1,  123,  324,   -1,   91,  327,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,  338,  339,
   60,  341,  342,  343,  344,  345,  346,  347,   -1,   -1,
  350,  351,  352,   -1,   -1,   -1,  356,   -1,  123,   60,
  360,  361,  362,  363,  364,   -1,  366,  367,  368,  369,
  370,   91,  372,   60,  374,   -1,  206,   -1,  257,  258,
  259,   -1,  261,  262,  263,  385,  265,  217,   -1,   -1,
   91,   -1,   -1,  272,  273,   -1,  396,  397,  398,  399,
  279,   -1,   -1,  123,   91,   -1,   -1,  286,   -1,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,   -1,  123,  253,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,   -1,  123,   -1,   -1,   -1,
   -1,  286,   -1,   -1,  274,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,  300,  301,  302,  303,  304,
   -1,   -1,   -1,   -1,  264,  276,   -1,   -1,  279,   -1,
   -1,   -1,   -1,  273,  125,  286,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,
  265,  331,   -1,  333,   -1,  335,   -1,  272,  273,   -1,
   -1,  356,   -1,   -1,  279,   -1,   -1,  347,   -1,  349,
   -1,  286,   -1,   -1,   -1,  370,  371,  372,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  340,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
  125,   -1,  272,  273,   -1,   -1,  257,  258,  259,  279,
  261,  262,  263,   -1,  265,   -1,  286,   -1,   -1,   -1,
   -1,  272,  273,   -1,   -1,   -1,   -1,  264,  279,   -1,
  300,  301,  302,  303,  304,  286,  273,   -1,   -1,   -1,
   -1,  356,   -1,   -1,  281,  282,  283,  284,  285,   -1,
   -1,   -1,   -1,   -1,   -1,  370,  371,  372,   -1,   -1,
   -1,   -1,  442,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  268,  269,  273,   -1,  272,  273,  125,  275,  458,   -1,
  460,   -1,   -1,   -1,  464,   -1,  356,   -1,  325,  326,
  288,  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  370,  371,  372,   -1,   -1,  356,   -1,   -1,  306,   -1,
   -1,   -1,  313,  314,  315,   -1,   -1,   -1,   -1,  370,
  371,  372,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,  273,  350,
  351,  352,  125,   -1,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,  313,  314,
  315,   -1,   -1,   -1,   -1,  396,  397,  398,  399,  324,
   -1,   -1,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,  338,  339,   -1,  341,  342,  343,  344,
  345,  346,  347,   -1,  273,  350,  351,  352,  125,   -1,
   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,  364,
   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,  374,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  385,   -1,   -1,   -1,  313,  314,  315,   -1,   -1,   -1,
   -1,  396,  397,  398,  399,  324,   -1,   -1,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,  338,
  339,   -1,  341,  342,  343,  344,  345,  346,  347,   -1,
  273,  350,  351,  352,  125,   -1,   -1,  356,   -1,   -1,
   -1,  360,  361,  362,  363,  364,   -1,  366,  367,  368,
  369,  370,   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,
  313,  314,  315,   -1,   -1,   -1,   -1,  396,  397,  398,
  399,  324,   -1,   -1,  327,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,  338,  339,   -1,  341,  342,
  343,  344,  345,  346,  347,   -1,  273,  350,  351,  352,
  125,   -1,   -1,  356,   -1,   -1,   -1,  360,  361,  362,
  363,  364,   -1,  366,  367,  368,  369,  370,   -1,  372,
   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  385,   -1,   -1,   -1,  313,  314,  315,   -1,
   -1,   -1,   -1,  396,  397,  398,  399,  324,   -1,   -1,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,  338,  339,   -1,  341,  342,  343,  344,  345,  346,
  347,   -1,  273,  350,  351,  352,  125,   -1,   -1,  356,
   -1,   -1,   -1,  360,  361,  362,  363,  364,   -1,  366,
  367,  368,  369,  370,   -1,  372,   -1,  374,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,
   -1,   -1,  313,  314,  315,   -1,   -1,   -1,   -1,  396,
  397,  398,  399,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,  273,  350,
  351,  352,  125,   -1,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,  313,  314,
  315,   -1,   -1,   -1,   -1,  396,  397,  398,  399,  324,
   -1,   -1,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,  338,  339,   -1,  341,  342,  343,  344,
  345,  346,  347,   -1,  273,  350,  351,  352,  125,   -1,
   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,  364,
   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,  374,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  385,   -1,   -1,   -1,  313,  314,  315,   -1,   -1,   -1,
   -1,  396,  397,  398,  399,  324,   -1,   -1,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,  338,
  339,   -1,  341,  342,  343,  344,  345,  346,  347,   -1,
  273,  350,  351,  352,  125,   -1,   -1,  356,   -1,   -1,
   -1,  360,  361,  362,  363,  364,   -1,  366,  367,  368,
  369,  370,   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,
  313,  314,  315,   -1,   -1,   -1,   -1,  396,  397,  398,
  399,  324,   -1,   -1,  327,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,  338,  339,   -1,  341,  342,
  343,  344,  345,  346,  347,   -1,  273,  350,  351,  352,
  125,   -1,   -1,  356,   -1,   -1,   -1,  360,  361,  362,
  363,  364,   -1,  366,  367,  368,  369,  370,   -1,  372,
   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  385,   -1,   -1,   -1,  313,  314,  315,   -1,
   -1,   -1,   -1,  396,  397,  398,  399,  324,   -1,   -1,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,  338,  339,   -1,  341,  342,  343,  344,  345,  346,
  347,   -1,  273,  350,  351,  352,  125,   -1,   -1,  356,
   -1,   -1,   -1,  360,  361,  362,  363,  364,   -1,  366,
  367,  368,  369,  370,   -1,  372,   -1,  374,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,
   -1,   -1,  313,  314,  315,   -1,   -1,   -1,   -1,  396,
  397,  398,  399,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   42,
  341,  342,  343,  344,  345,  346,  347,   -1,  273,  350,
  351,  352,   -1,   -1,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,  313,  314,
  315,   -1,   60,   -1,   -1,  396,  397,  398,  399,  324,
   -1,   -1,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,  338,  339,   -1,  341,  342,  343,  344,
  345,  346,  347,   91,  273,  350,  351,  352,   -1,   -1,
   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,  364,
   -1,  366,  367,  368,  369,  370,   60,  372,   -1,  374,
   41,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
  385,   -1,   -1,   -1,  313,  314,  315,   -1,   -1,   60,
   -1,  396,  397,  398,  399,  324,   -1,   91,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,  338,
  339,   41,  341,  342,  343,  344,  345,  346,  347,   -1,
   91,  350,  351,  352,   -1,   -1,   -1,  356,   -1,  123,
   60,  360,  361,  362,  363,  364,   -1,  366,  367,  368,
  369,  370,   -1,  372,   -1,  374,   60,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,  385,   -1,   -1,   60,
   -1,   91,   -1,   -1,   -1,   -1,   -1,  396,  397,  398,
  399,   -1,   -1,   -1,  257,  258,  259,   91,  261,  262,
  263,   -1,  265,   60,   -1,   -1,   -1,   -1,   60,   -1,
   91,   -1,   -1,  123,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  286,   -1,   -1,   -1,   -1,   -1,  123,
   60,   -1,   -1,   -1,   91,   -1,  264,   -1,   -1,   91,
   -1,   -1,  123,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   60,  281,  282,  283,  284,  285,   60,   -1,
   -1,   91,   -1,  125,  292,   60,  123,   -1,  125,  297,
  298,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   91,   -1,   -1,   -1,   60,   -1,   91,
  264,   -1,   -1,  123,   60,   -1,   91,   -1,   93,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,   91,  264,   -1,  123,   -1,  125,   91,  293,
   -1,  123,  273,   -1,   -1,   91,   -1,   -1,  123,   -1,
  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  293,   -1,  123,   -1,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  264,   -1,   -1,  123,   -1,   -1,
   -1,   -1,  125,  273,   -1,   -1,   -1,   -1,  278,   -1,
  264,  281,  282,  283,  284,  285,   -1,   -1,   -1,  273,
   -1,   -1,   -1,  264,   -1,  125,   -1,  281,  282,  283,
  284,  285,  273,   -1,   -1,   -1,   -1,   -1,  260,  293,
  281,  282,  283,  284,  285,   -1,   -1,  264,  265,   -1,
   -1,  292,  264,  265,  276,   -1,  273,  274,   -1,   -1,
   -1,  273,  274,   -1,  281,  282,  283,  284,  285,  281,
  282,  283,  284,  285,  264,  265,   -1,   -1,   -1,   -1,
   -1,  303,  304,  273,  274,  307,  308,  309,  310,  311,
  312,  281,  282,  283,  284,  285,  264,  261,  262,   -1,
   -1,   -1,  264,   -1,   -1,  273,   -1,   -1,   -1,  264,
   -1,  273,  274,  281,  282,  283,  284,  285,  273,  281,
  282,  283,  284,  285,   -1,  264,  281,  282,  283,  284,
  285,  264,   -1,   -1,  273,   -1,   -1,  260,  264,  278,
  273,   -1,  281,  282,  283,  284,  285,  273,  281,  282,
  283,  284,  285,  276,   -1,  281,  282,  283,  284,  285,
  260,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  276,   -1,   -1,   -1,
  303,  304,   -1,   -1,  307,  308,  309,  310,  311,  312,
   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  303,  304,   -1,   -1,  307,  308,  309,
  310,  311,  312,  377,  378,  379,  380,   -1,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  390,  391,  392,  393,
  394,  395,  313,  314,  315,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,  273,  274,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  313,  314,  315,  396,  397,  398,  399,   -1,
   -1,   -1,   -1,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,  273,  350,
  351,  352,   -1,   -1,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,  313,  314,
  315,   -1,   -1,   -1,   -1,  396,  397,  398,  399,  324,
   -1,   -1,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,  338,  339,   -1,  341,  342,  343,  344,
  345,  346,  347,   -1,  273,  350,  351,  352,   -1,   -1,
   -1,  356,   -1,   -1,   -1,  360,  361,  362,  363,  364,
   -1,  366,  367,  368,  369,  370,   -1,  372,   -1,  374,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  385,   -1,   -1,   -1,  313,  314,  315,   -1,   -1,   -1,
   -1,  396,  397,  398,  399,  324,   -1,   -1,  327,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,  338,
  339,   -1,  341,  342,  343,  344,  345,  346,  347,   -1,
  273,  350,  351,  352,   -1,   -1,   -1,  356,   -1,   -1,
   -1,  360,  361,  362,  363,  364,   -1,  366,  367,  368,
  369,  370,   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,
  313,  314,  315,   -1,   -1,   -1,   -1,  396,  397,  398,
  399,  324,   -1,   -1,  327,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,  338,  339,   -1,  341,  342,
  343,  344,  345,  346,  347,   -1,  273,  350,  351,  352,
   -1,   -1,   -1,  356,   -1,   -1,   -1,  360,  361,  362,
  363,  364,   -1,  366,  367,  368,  369,  370,   -1,  372,
   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  385,   -1,   -1,   -1,  313,  314,  315,   -1,
   -1,   -1,   -1,  396,  397,  398,  399,  324,   -1,   -1,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,  338,  339,   -1,  341,  342,  343,  344,  345,  346,
  347,   -1,  273,  350,  351,  352,   -1,   -1,   -1,  356,
   -1,   -1,   -1,  360,  361,  362,  363,  364,   -1,  366,
  367,  368,  369,  370,   -1,  372,   -1,  374,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  385,   -1,
   -1,   -1,  313,  314,  315,   -1,   -1,   -1,   -1,  396,
  397,  398,  399,  324,   -1,   -1,  327,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,  338,  339,   -1,
  341,  342,  343,  344,  345,  346,  347,   -1,   -1,  350,
  351,  352,   -1,   -1,   -1,  356,   -1,   -1,   -1,  360,
  361,  362,  363,  364,   -1,  366,  367,  368,  369,  370,
   -1,  372,   -1,  374,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  385,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  396,  397,  398,  399,  324,
   -1,   -1,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,  338,  339,   -1,  341,  342,  343,  344,
  345,  346,  347,   -1,   -1,  350,  351,  352,   -1,   -1,
   -1,  356,  260,   -1,   -1,  360,  361,  362,  363,  364,
   -1,  366,  367,  368,  369,  370,   -1,  372,  276,  374,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  385,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  396,  397,  398,  399,  303,  304,   -1,   -1,  307,
  308,  309,  310,  311,  312,  257,  258,  259,  260,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  274,   -1,  276,   -1,   -1,  279,   -1,   -1,
   -1,  257,  258,  259,  286,  261,  262,  263,   -1,  265,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
  286,
  };

#line 1002 "Repil/IR/IR.jay"

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
  public const int NONNULL = 300;
  public const int NOCAPTURE = 301;
  public const int WRITEONLY = 302;
  public const int READONLY = 303;
  public const int READNONE = 304;
  public const int ATTRIBUTE_GROUP_REF = 305;
  public const int ATTRIBUTES = 306;
  public const int NORECURSE = 307;
  public const int NOUNWIND = 308;
  public const int SPECULATABLE = 309;
  public const int SSP = 310;
  public const int UWTABLE = 311;
  public const int ARGMEMONLY = 312;
  public const int RET = 313;
  public const int BR = 314;
  public const int SWITCH = 315;
  public const int INDIRECTBR = 316;
  public const int INVOKE = 317;
  public const int RESUME = 318;
  public const int CATCHSWITCH = 319;
  public const int CATCHRET = 320;
  public const int CLEANUPRET = 321;
  public const int UNREACHABLE = 322;
  public const int FNEG = 323;
  public const int ADD = 324;
  public const int NUW = 325;
  public const int NSW = 326;
  public const int FADD = 327;
  public const int SUB = 328;
  public const int FSUB = 329;
  public const int MUL = 330;
  public const int FMUL = 331;
  public const int UDIV = 332;
  public const int SDIV = 333;
  public const int FDIV = 334;
  public const int UREM = 335;
  public const int SREM = 336;
  public const int FREM = 337;
  public const int SHL = 338;
  public const int LSHR = 339;
  public const int EXACT = 340;
  public const int ASHR = 341;
  public const int AND = 342;
  public const int OR = 343;
  public const int XOR = 344;
  public const int EXTRACTELEMENT = 345;
  public const int INSERTELEMENT = 346;
  public const int SHUFFLEVECTOR = 347;
  public const int EXTRACTVALUE = 348;
  public const int INSERTVALUE = 349;
  public const int ALLOCA = 350;
  public const int LOAD = 351;
  public const int STORE = 352;
  public const int FENCE = 353;
  public const int CMPXCHG = 354;
  public const int ATOMICRMW = 355;
  public const int GETELEMENTPTR = 356;
  public const int ALIGN = 357;
  public const int INBOUNDS = 358;
  public const int INRANGE = 359;
  public const int TRUNC = 360;
  public const int ZEXT = 361;
  public const int SEXT = 362;
  public const int FPTRUNC = 363;
  public const int FPEXT = 364;
  public const int TO = 365;
  public const int FPTOUI = 366;
  public const int FPTOSI = 367;
  public const int UITOFP = 368;
  public const int SITOFP = 369;
  public const int PTRTOINT = 370;
  public const int INTTOPTR = 371;
  public const int BITCAST = 372;
  public const int ADDRSPACECAST = 373;
  public const int ICMP = 374;
  public const int EQ = 375;
  public const int NE = 376;
  public const int UGT = 377;
  public const int UGE = 378;
  public const int ULT = 379;
  public const int ULE = 380;
  public const int SGT = 381;
  public const int SGE = 382;
  public const int SLT = 383;
  public const int SLE = 384;
  public const int FCMP = 385;
  public const int OEQ = 386;
  public const int OGT = 387;
  public const int OGE = 388;
  public const int OLT = 389;
  public const int OLE = 390;
  public const int ONE = 391;
  public const int ORD = 392;
  public const int UEQ = 393;
  public const int UNE = 394;
  public const int UNO = 395;
  public const int PHI = 396;
  public const int SELECT = 397;
  public const int CALL = 398;
  public const int TAIL = 399;
  public const int VA_ARG = 400;
  public const int LANDINGPAD = 401;
  public const int CATCHPAD = 402;
  public const int CLEANUPPAD = 403;
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
