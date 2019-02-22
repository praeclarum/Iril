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
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I1",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
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
//t    "terminator_instruction : BR I1 value ',' label_value ',' label_value",
//t    "terminator_instruction : RET typed_value",
//t    "terminator_instruction : SWITCH typed_value ',' label_value '[' switch_cases ']'",
//t    "instruction : ADD type value ',' value",
//t    "instruction : ADD wrappings type value ',' value",
//t    "instruction : ALLOCA type ',' ALIGN INTEGER",
//t    "instruction : AND type value ',' value",
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
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
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
    "TYPE","HALF","FLOAT","DOUBLE","I1","I8","I16","I32","I64",
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
case 72:
#line 287 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 73:
#line 288 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 74:
#line 289 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 75:
#line 290 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 76:
#line 291 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 77:
#line 292 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 78:
#line 293 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 79:
#line 294 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 80:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 81:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 83:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 314 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 85:
#line 318 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 86:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 89:
#line 337 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 90:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 93:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 360 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 95:
#line 364 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 372 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 376 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 99:
#line 383 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 387 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 391 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 102:
#line 395 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 103:
#line 396 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 104:
#line 403 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 407 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 414 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 418 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 108:
#line 422 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 109:
#line 426 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 111:
#line 434 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 112:
#line 438 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 113:
#line 439 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 114:
#line 440 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 115:
#line 441 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 116:
#line 442 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 122:
#line 460 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 123:
#line 461 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 124:
#line 462 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 125:
#line 463 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 126:
#line 464 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 127:
#line 465 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 128:
#line 466 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 129:
#line 467 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 130:
#line 468 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 131:
#line 469 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 132:
#line 473 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 133:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 134:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 135:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 136:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 137:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 138:
#line 479 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 139:
#line 480 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 140:
#line 481 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 141:
#line 482 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 142:
#line 483 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 143:
#line 484 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 144:
#line 485 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 145:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 146:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 147:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 149:
#line 493 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 150:
#line 494 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 151:
#line 498 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 502 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 506 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 510 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 514 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 518 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 522 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 530 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 160:
#line 531 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 161:
#line 532 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 162:
#line 533 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 163:
#line 534 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 164:
#line 535 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 165:
#line 536 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 166:
#line 537 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 167:
#line 538 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 168:
#line 545 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 552 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 556 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 171:
#line 563 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 570 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 574 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 581 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 592 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 596 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 614 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 618 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 182:
#line 622 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 626 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 184:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 637 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 644 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 188:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 191:
#line 664 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 192:
#line 665 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 193:
#line 672 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 676 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 683 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 196:
#line 687 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 197:
#line 691 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 198:
#line 695 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 199:
#line 699 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 200:
#line 703 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 201:
#line 707 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 203:
#line 712 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 208:
#line 729 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 739 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 211:
#line 746 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 750 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 757 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 778 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 782 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 786 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 790 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 223:
#line 797 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 801 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 229:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 230:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 231:
#line 829 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 232:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 233:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 234:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 857 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 861 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 865 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 885 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 889 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 247:
#line 893 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 248:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 249:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 905 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 909 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 913 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 929 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 933 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 937 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 941 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 945 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 949 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 262:
#line 953 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 957 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 961 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 965 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 969 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 973 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 977 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 981 "Repil/IR/IR.jay"
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
   11,   11,   11,   11,   11,   25,   25,   26,   26,    4,
    4,    4,    4,    4,    4,    4,    4,    4,    5,    5,
    5,   27,   27,   31,   31,   32,   32,   32,   32,   33,
   33,   34,   34,   34,   34,   34,   14,   14,   28,   28,
   35,   36,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   40,   18,   18,
   18,   18,   18,   18,   18,   18,   18,   41,   21,   21,
   42,   39,   39,   43,   44,   38,   38,   29,   29,   45,
   45,   45,   45,   46,   46,   48,   48,   48,   48,   50,
   51,   51,   52,   52,   53,   53,   53,   53,   53,   53,
   53,   54,   54,   19,   19,   55,   55,   56,   56,   57,
   58,   58,   59,   60,   60,   61,   61,   30,   47,   47,
   47,   47,   49,   49,   49,   49,   49,   49,   49,   49,
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
    1,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    4,    2,    1,    5,    5,    1,    3,    1,    1,    9,
    9,   10,   10,   11,    9,   10,   11,   12,    5,    6,
    6,    3,    2,    1,    3,    1,    2,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    6,    9,    6,    6,    3,    3,    3,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    2,    2,    1,
    2,    1,    3,    2,    1,    1,    3,    1,    2,    2,
    3,    1,    2,    1,    2,    1,    2,    3,    4,    1,
    3,    2,    1,    3,    2,    3,    3,    3,    2,    4,
    5,    1,    1,    1,    3,    1,    1,    1,    3,    5,
    1,    2,    3,    1,    2,    1,    1,    1,    2,    7,
    2,    7,    5,    6,    5,    5,    4,    4,    5,    5,
    6,    5,    6,    4,    5,    6,    5,    5,    4,    4,
    5,    6,    7,    6,    6,    7,    5,    6,    5,    5,
    6,    3,    4,    5,    7,    4,    5,    6,    6,    4,
    7,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   83,   72,   73,   74,   75,   76,   77,   78,   79,
    0,   29,   28,    0,    0,    0,   71,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  117,  118,   26,
   27,   30,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   64,    0,    0,    0,    0,    0,    0,   82,  218,
    0,    0,    0,    0,    0,    0,    0,    5,    6,    0,
    0,    0,    0,    0,    8,    0,    7,    0,    0,    0,
    0,    0,   65,    0,    0,    0,    0,    0,    0,    0,
    0,   89,   80,    0,    0,   86,    0,    0,    0,  161,
  162,  160,  163,  164,  165,  159,  150,  149,  167,  166,
    0,    0,    0,    0,    0,    0,    0,    0,  148,    0,
    0,    0,    0,    0,    0,    0,   31,    0,    0,    0,
   49,   48,   13,    0,    0,   42,   47,    0,    0,    0,
    0,    0,    0,    0,  108,  109,  103,    0,    0,  104,
  121,    0,    0,  119,   81,    0,    0,    0,    0,    0,
    0,   62,   54,   52,   53,   55,   56,   57,   58,    0,
   50,    0,    0,    0,    0,  172,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
   43,   14,    0,  169,    0,   84,   66,   85,    0,    0,
  112,  113,  115,  114,  116,    0,  110,  102,    0,    0,
    0,    0,  120,   87,    0,    0,    0,    0,   12,   51,
    0,    0,    0,    0,  157,    0,  155,  156,    0,    0,
    0,    0,    0,    0,   35,    0,   33,   36,   37,   32,
   17,   16,   46,   45,   44,    0,    0,    0,    0,  111,
  105,    0,    0,   40,    0,    0,   59,  207,  206,    0,
  204,    0,    0,    0,    0,  173,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  178,    0,    0,
  184,    0,    0,    0,    0,    0,   41,    0,   63,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   23,    0,
   39,    0,    0,    0,    0,  221,    0,    0,  219,    0,
  216,  217,    0,    0,  214,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  122,  123,  124,  125,  126,  127,  128,  129,  130,  131,
    0,  132,  133,  144,  145,  146,  147,  135,  137,  138,
  139,  140,  136,  134,  142,  143,  141,    0,    0,    0,
    0,    0,    0,   95,  179,    0,  185,    0,    0,    0,
    0,    0,   90,    0,   91,  205,    0,  154,  151,  153,
    0,    0,    0,    0,   38,   93,    0,    0,  168,    0,
    0,    0,    0,  215,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  208,    0,  190,    0,    0,    0,    0,
    0,   96,    0,    0,   92,    0,    0,    0,   94,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  234,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   97,    0,  175,    0,  176,    0,    0,  223,    0,  235,
  262,    0,  241,  250,    0,  238,  265,  254,  237,  267,
  257,    0,    0,  247,  226,  249,  268,    0,    0,  225,
  158,  171,    0,    0,    0,    0,    0,    0,    0,  209,
    0,    0,  192,    0,    0,  193,    0,  229,    0,    0,
   98,  152,    0,    0,    0,    0,    0,  211,  224,  263,
  251,  258,  248,  245,  259,    0,    0,    0,    0,  244,
  236,    0,    0,    0,    0,  195,    0,  191,    0,    0,
  233,  177,  220,  174,    0,  222,  212,  246,  261,    0,
  210,  255,    0,  203,  197,  202,  198,  196,  194,  213,
  200,    0,  201,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  180,  144,  136,   53,
  145,  506,  222,   54,   55,   39,  137,  129,  270,  146,
  573,  181,   64,   65,  105,  106,  101,  163,  327,   72,
  159,  160,  216,  217,  164,  411,  428,  574,  187,  602,
  359,  548,  626,  575,  328,  329,  330,  331,  332,  507,
  567,  615,  616,  667,  271,  503,  504,  627,  628,  364,
  365,
  };
  protected static readonly short [] yySindex = {          705,
  -54,  -79,    4,   46,   57, 2768, 2697, -196,    0,  705,
    0,    0,    0,    0, -136,   95,  101,  294,  -76,  -23,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3088,    0,    0, 2942, -110,  -87,    0,  132, 2650,  -31,
 3088,  -16,  148,    0,    0,  -28,   -7,    0,    0,    0,
    0,    0, 3088,  -32,   27, -145,  -15,  188,  -22,  115,
  -13,    0,  132,  -25,  217,    3, 3088,   39,    0,    0,
  -11, 3088,  319,  941,   -9,  319,  191,    0,    0, 1468,
 3088,  -32, 3088,  -32,    0,  219,    0, -141,  346,  225,
 2849,  319,    0, 3088, 3088,   -5, 3088,  319,   -8, 1548,
 -172,    0,    0,  132,   96,    0,  319, -172, 2652,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   17,  347,  350,  351, 3114, 3114, 3114,  352,    0, 1468,
 3088, 1468, 3088,  337,  359,  159,    0, -141, 2882,    0,
    0,    0,    0,   -2, 1468,    0,    0,   27,  132,   54,
  356,    2,  111,  319,    0,    0,    0,  -24,  182,    0,
    0,  111, -182,    0,    0, 2824,  111,  111,  111,  360,
  388,    0,    0,    0,    0,    0,    0,    0,    0, -117,
    0,  389, 3114, 3114, 3114,    0,    9,   67,   37,   70,
  390, 1468,  393, 1428, 1531,  162,    0, -141,  214,   36,
    0,    0, 2908,    0,  111,    0,    0,    0, -106,   27,
    0,    0,    0,    0,    0,  263,    0,    0, 2797, -103,
  164, -111,    0,    0,  111,  111,  181,  881,    0,    0,
 3088,   77,   78,   80,    0, 3114,    0,    0,  183,   89,
  408,  108,  109,  411,    0,  422,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  -86, 3635,  -89,  111,    0,
    0, 3635,  -63,    0,  197, 3635,    0,    0,    0,  233,
    0,   75, 3088, 3088, 3088,    0,  201,  221,  119,  226,
  227,  136, 1052, 3635,  -56,  436, 3114, -230, 3114, 2617,
 3088, 2617, 3088, 2617, 3088, 3088, 3088, 3088, 3088, 2617,
  316, 3088, 3088, 3088, 3114, 3114, 3114, 3088, 3088, 3114,
  116, 3114, 3114, 3114, 3114, 3114, 3114, 3114, 3114, 3114,
 2937, 2889, 3088, 3088, 2650,   97, 1536,    0, 3635,  201,
    0,  201, 3635,  -75, 1614, 3635,    0, 1692,    0,  881,
 3114,  258,  284,  295,  228,  201,  242,  201,    0,  244,
    0,  260, 1771, 3635, 4023,    0,  232, 1487,    0,  463,
    0,    0, 1468, 2617,    0, 1468, 1468, 2617, 1468, 1468,
 2617, 1468, 1468, 1468, 1468, 1468, 1468, 2617, 3088, 1468,
 1468, 1468, 1468,  464,  466,  467,  139,  313,  468, 3088,
  364,  145,  149,  150,  151,  153,  154,  155,  156,  158,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3088,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3088,   51, 1468,
   50, 3088, 2650,    0,    0,  201,    0,  228,  228, 1849,
 3635,  -47,    0, 1927,    0,    0,  472,    0,    0,    0,
  228,  201,  228,  201,    0,    0, 2006,  201,    0,  483,
  264,  490, 1468,    0,  496,  499, 1468,  505,  508, 1468,
  511,  512,  514,  515,  528,  530, 1468, 1468,  531,  532,
  533,  534, 3114, 3114, 3114,  223, 3088, 3088,  368, 3114,
 3088, 3088, 3088, 3088, 3088, 3088, 3088, 3088, 3088, 1468,
 1468, 1487,  535,    0,  537,    0,  550,   50,   50, 3088,
  228,    0, 2084, 3635,    0, 3114,  228,  228,    0,  228,
  264,  517, 1487,  552, 1487, 1487,  561, 1487, 1487,  567,
 1487, 1487, 1487, 1487, 1487, 1487,  570,  583, 1487, 1487,
 1487, 1487,    0,  591,  593,  375, 1468,  594,  595, 3114,
  603,  132,  132,  132,  132,  132,  132,  132,  132,  132,
  605,  606,  607,  563, 3114, 2742,  111,  550,  550,   50,
    0, 2162,    0,  289,    0,  613, 3088,    0, 1487,    0,
    0, 1487,    0,    0, 1487,    0,    0,    0,    0,    0,
    0, 1487, 1487,    0,    0,    0,    0, 3114, 3114,    0,
    0,    0,  298,  311,  624, 3114, 1487, 1487, 1487,    0,
  629, 2975,    0, 1363,  302,    0,  111,    0,  111,  550,
    0,    0, 3114,  264,  274,  630, 3026,    0,    0,    0,
    0,    0,    0,    0,    0,  418,  421, 3114,  635,    0,
    0,  587, 3114,  641, 1506,    0,  383,    0, 3061,  111,
    0,    0,    0,    0,  264,    0,    0,    0,    0,  635,
    0,    0, 1130,    0,    0,    0,    0,    0,    0,    0,
    0,  308,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  682,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1340,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    6,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  645,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  180,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  645,
    0,  645,    0,    0,    0,    0,    0,    0,    0,  432,
    0,    0,    0,    0,  645,    0,    0,    0,   42,  645,
    0,  645,    0,    0,    0,    0,    0,  190,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   59, 1269,
 1367,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  645,    0,  645,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  312,    0,    0,    0,    0,
    0,    0,    0,    0,   72,  102,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  273,    0,    0,    0,    0,  338,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  645,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2241,
    0, 3713,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  645,  645,  645,  361,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  645,    0,    0,  645,  645,    0,  645,  645,
    0,  645,  645,  645,  645,  645,  645,    0,    0,  645,
  645,  645,  645,    0,    0,    0,  645,  645,    0,    0,
  645,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  645,  645,
    0,    0,    0,    0,    0, 2319,    0, 2397, 3791,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  392,  435,  444,    0,    0,    0,    0, 3869,    0,    0,
    0,    0,  645,    0,    0,    0,  645,    0,    0,  645,
    0,    0,    0,    0,    0,    0,  645,  645,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  645,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  645,
  645,    0, 3089,    0,    0,    0,    0,    0,    0,    0,
 2476,    0,    0,    0,    0,    0,  544,  622,    0, 3947,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  645,    0,    0,    0,
    0,  510,  588,  675,  753,  831,  924, 1002, 1080, 1167,
    0,    0,    0,    0,    0,    0, 3167,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  645,    0,    0, 3245,    0, 3323,    0,
    0,    0,    0,    0,  645,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3401,    0,
    0,    0,    0,  344,  645,    0,    0,    0,    0, 3479,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3557,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  684,  638,    0,    0,    0,    0,  557,  559,  128,
   -6,   84, 2539,   -4,    0,  681,  502, -125, -280,    0,
  -70,  522,  642,   -1,    0,  545,  262, -104, -179, -282,
    0,  495,  104, -214, -147,    0,    0, -500,  240,    0,
 -447,  234,    0,   98, -312,    0,  391,  394,  369, -424,
 -470,    0,   76,    0,  397,    0,  165,    0,  103, -114,
 -199,
  };
  protected static readonly short [] yyTable = {            38,
   38,  260,  352,  169,   40,   42,   15,  229,   74,   60,
   90,  266,   67,  522,  435,  223,  257,   69,   94,  262,
  147,  223,  435,   74,   38,  435,   74,   63,   74,   61,
   74,   74,   38,  333,   38,  357,  284,   71,   94,   75,
  435,  203,  432,   69,   86,   69,   80,  441,  209,   67,
   82,   84,  236,  358,  186,  186,  186,  220,   99,  336,
   63,  223,  225,  226,   18,   38,  354,  104,  147,  248,
   99,  100,  223,  576,  130,  514,  132,  223,  223,  203,
  236,   69,  335,  568,  569,   68,  338,  149,  150,   74,
  152,  221,   69,  158,  208,   69,  162,  618,  619,   93,
  256,  101,  269,  168,  353,  639,   19,   34,  223,  126,
  236,   43,  232,  233,  234,  206,   69,   20,  341,  151,
   48,   49,  202,   45,  192,  161,  194,  435,  237,  238,
   67,  435,  255,  235,  134,  161,  165,  660,  135,  166,
  127,  502,  170,  205,  435,  620,   66,   48,   49,  651,
  510,   50,   51,  440,  334,   46,  444,  269,  171,  104,
  252,   47,  265,  128,  464,  276,   68,  221,  464,   68,
  221,  464,  125,   69,  457,   35,  653,  368,  464,  371,
   69,   81,  486,   83,  265,  378,  223,  221,  172,  173,
   16,   17,  174,  175,  176,  177,  178,  179,  221,  197,
  435,  161,  198,   57,  161,  259,   36,  670,   77,  131,
  265,  133,  158,  191,  269,  193,  356,  265,  360,   69,
   88,  161,  218,   88,  272,  219,  265,   88,  204,   69,
  106,   78,  161,  106,  384,  385,  386,   91,   34,  389,
   73,  392,  393,  394,  395,  396,  397,  398,  399,  400,
   58,   89,   79,   59,  251,   76,   74,  198,   92,  435,
   98,  513,  107,  154,   50,   51,  342,  343,  344,   95,
  447,  201,   21,  339,   85,  241,  340,  244,  211,  212,
  213,  214,  215,  363,  366,  367,  369,  370,  372,  373,
  374,  375,  376,  377,  380,  381,  382,  383,  448,   69,
  455,  387,  388,  340,  391,   97,  110,  111,  112,  201,
  113,  114,  115,  109,  116,   69,  429,  430,   38,   48,
   49,  117,  118,  431,  449,   69,   99,   99,  119,  622,
   99,   99,  623,   99,  572,  450,   69,  108,  120,  100,
  100,   67,  648,  100,  100,  649,  100,  139,  673,   99,
   99,  340,  107,  148,   69,  107,  487,  463,  100,  153,
   18,  467,  100,  100,  470,  188,  189,   99,  167,  101,
  101,  477,  478,  101,  101,   35,  101,  182,   34,   21,
  100,   34,  672,  489,  199,  138,  183,  199,   22,  184,
  185,   19,  101,  101,  195,  190,   23,   24,   25,   26,
   27,   28,   29,   30,  500,   69,   36,  490,  121,   69,
  101,  550,  543,  544,  545,  210,  196,  207,  161,  551,
  227,  501,  122,  123,  124,   38,   38,  228,  231,  239,
  508,  509,  260,  240,   20,  249,  242,  264,   34,  277,
  267,  460,  126,   25,  273,  274,  462,  275,  278,  465,
  466,  279,  468,  469,  282,  471,  472,  473,  474,  475,
  476,  283,  617,  479,  480,  481,  482,  280,  281,  223,
  337,   70,  170,  127,  221,  170,  390,  346,  347,  605,
  547,  547,  348,  349,  552,  553,  554,  555,  556,  557,
  558,  559,  560,  170,  611,  350,  355,  433,  452,  654,
  454,  265,  223,   38,  459,  125,  461,  483,  570,  484,
  485,  488,  491,  505,  650,  516,  492,  493,  494,  666,
  495,  496,  497,  498,  170,  499,  521,  634,  635,  357,
  110,  111,  112,  523,  113,  114,  115,  269,  116,  525,
   21,   21,  526,   22,   21,   21,  524,   21,  528,   69,
  527,  529,  119,  530,  531,  532,  170,  533,  534,  614,
  537,  538,  120,   21,   21,  211,  212,  213,  214,  215,
  625,  535,  662,  536,  539,  540,  541,  542,  564,   21,
  565,   21,  546,  561,  562,  563,   48,   49,   22,  566,
   50,   51,   52,   32,   33,  579,   23,   24,   25,   26,
   27,   28,   29,   30,  582,  645,  578,  577,  580,  581,
  585,  583,  584,  592,  586,  587,  588,  589,  590,  591,
  625,   24,  594,  595,  596,  597,  593,   69,   18,   18,
  601,  600,   18,   18,  598,   18,  599,  603,  604,  110,
  111,  112,  614,  113,  114,  115,  606,  116,  607,  608,
  609,   18,   18,  502,  117,  118,  624,  636,  379,   19,
   19,  119,  629,   19,   19,  630,   19,  638,  631,   18,
  637,  120,  643,  655,  658,  632,  633,  659,  623,  661,
  663,    1,   19,   19,   69,  211,  212,  213,  214,  215,
  640,  641,  642,   44,   87,  200,  199,  646,   56,  250,
   19,  230,   20,   20,  170,  170,   20,   20,   96,   20,
  224,   25,   25,  261,   69,   25,   25,  647,   25,  436,
  652,  549,  437,  458,  669,   20,   20,    0,  610,  657,
  668,    0,    0,    0,   25,   25,  446,    0,    0,    0,
    0,  121,    0,   20,    0,    0,    0,  170,  170,  170,
    0,    0,   25,    0,    0,  122,  123,  124,  170,    0,
    0,  170,  170,  170,  170,  170,  170,  170,  170,  170,
    0,    0,  170,  170,    0,    0,  170,  170,  170,  170,
  170,  170,  264,  264,  170,  170,  170,    0,    0,    0,
  170,    0,   69,    0,  170,  170,  170,    0,    0,  170,
  170,  170,  170,  170,  170,    0,  170,    0,  170,    0,
    0,   22,   22,    0,    0,   22,   22,    0,   22,  170,
    0,    0,    0,    0,    0,  264,  264,  264,    0,    0,
  170,  170,  170,  170,   22,   22,  264,    0,    0,  264,
  264,  264,  264,  264,  264,  264,  264,  264,    0,    0,
  264,  264,   22,    0,  264,  264,  264,  264,  264,  264,
  269,  269,  264,  264,  264,    0,    0,    0,  264,    0,
   69,    0,  264,  264,  264,    0,    0,    0,  264,  264,
  264,  264,  264,    0,  264,    0,  264,    0,    0,   24,
   24,    0,    0,   24,   24,    0,   24,  264,    0,    0,
    0,    0,    0,  269,  269,  269,    0,    0,  264,  264,
  264,  264,   24,   24,  269,    0,    0,  269,  269,  269,
  269,  269,  269,  269,  269,  269,    0,    0,  269,  269,
   24,    0,  269,  269,  269,  269,  269,  269,    0,    0,
  269,  269,  269,    0,    0,    0,  269,  256,  256,    0,
  269,  269,  269,    0,    0,    0,  269,  269,  269,  269,
  269,    0,  269,   69,  269,    0,    0,    0,    0,    0,
    0,    0,    1,    2,    0,  269,    3,    4,    0,    5,
    0,  103,    0,    0,    0,    0,  269,  269,  269,  269,
  256,  256,  256,    0,    0,    6,    7,    0,    0,    0,
   35,  256,    0,    0,  256,  256,  256,  256,  256,  256,
  256,  256,  256,    8,    0,  256,  256,    0,    0,  256,
  256,  256,  256,  256,  256,  239,  239,  256,  256,  256,
    0,   36,    0,  256,    0,    0,    0,  256,  256,  256,
    0,   69,    0,  256,  256,  256,  256,  256,    0,  256,
    0,  256,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  256,   34,    0,    0,    0,    0,  239,  239,
  239,    0,    0,  256,  256,  256,  256,    0,    0,  239,
    0,    0,  239,  239,  239,  239,  239,  239,  239,  239,
  239,    0,  351,  239,  239,    0,    0,  239,  239,  239,
  239,  239,  239,  240,  240,  239,  239,  239,    0,    0,
    0,  239,    0,    0,    0,  239,  239,  239,    0,   69,
    0,  239,  239,  239,  239,  239,    0,  239,    0,  239,
    0,    0,    0,    0,    0,    0,    0,  110,  111,  112,
  239,  113,  114,  115,    0,  116,  240,  240,  240,    0,
    0,  239,  239,  239,  239,    0,  268,  240,    0,  119,
  240,  240,  240,  240,  240,  240,  240,  240,  240,  120,
  671,  240,  240,    0,    0,  240,  240,  240,  240,  240,
  240,    0,    0,  240,  240,  240,    0,    0,    0,  240,
    0,    0,    0,  240,  240,  240,  266,  266,    0,  240,
  240,  240,  240,  240,   21,  240,   69,  240,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,  240,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,  240,
  240,  240,  240,    0,    0,    0,  102,    0,    0,  266,
  266,  266,    0,    0,    0,    0,    0,    0,    0,    0,
  266,    0,    0,  266,  266,  266,  266,  266,  266,  266,
  266,  266,    0,    0,  266,  266,    0,    0,  266,  266,
  266,  266,  266,  266,  260,  260,  266,  266,  266,    0,
    0,    0,  266,    0,    0,    0,  266,  266,  266,    0,
    0,    0,  266,  266,  266,  266,  266,    0,  266,    0,
  266,    0,    0,    0,    0,    0,    0,    0,  110,  111,
  112,  266,  113,  114,  115,    0,  116,  260,  260,  260,
    0,    0,  266,  266,  266,  266,    0,  268,  260,    0,
  119,  260,  260,  260,  260,  260,  260,  260,  260,  260,
  120,    0,  260,  260,    0,    0,  260,  260,  260,  260,
  260,  260,  253,  253,  260,  260,  260,    0,    0,    0,
  260,    0,    0,    0,  260,  260,  260,    0,    0,    0,
  260,  260,  260,  260,  260,    0,  260,    0,  260,   69,
    0,    0,    0,    0,    0,    0,  110,  111,  112,  260,
  113,  114,  115,   60,  116,  253,  253,  253,    0,   69,
  260,  260,  260,  260,   69,  268,  253,    0,  119,  253,
  253,  253,  253,  253,  253,  253,  253,  253,  120,    0,
  253,  253,  126,    0,  253,  253,  253,  253,  253,  253,
   69,    0,  253,  253,  253,    0,    0,    0,  253,  227,
  227,    0,  253,  253,  253,    0,    0,    0,  253,  253,
  253,  253,  253,  127,  253,    0,  253,    0,    0,    0,
    0,    0,   69,    0,    0,    0,    0,  253,    0,   69,
    0,  243,    0,    0,    0,    0,    0,    0,  253,  253,
  253,  253,  227,  227,  227,  125,    0,  126,    0,    0,
    0,   61,    0,  227,    0,    0,  227,  227,  227,  227,
  227,  227,  227,  227,  227,    0,    0,  227,  227,   69,
    0,  227,  227,  227,  227,  227,  227,    0,  127,  227,
  227,  227,    0,    0,    0,  227,    0,  126,   60,  227,
  227,  227,    0,    0,    0,  227,  227,  227,  227,  227,
    0,  227,    0,  227,   60,    0,  126,   69,    0,    0,
  125,    0,    0,    0,  227,    0,    0,    0,  127,    0,
    0,    0,    0,    0,    0,  227,  227,  227,  227,    0,
    0,    0,    0,    0,   60,   60,    0,  127,   60,   60,
   60,   60,   60,   60,    0,    0,    0,    0,  157,    0,
  125,    0,    0,    0,    0,    0,   69,   69,   69,    0,
   69,   69,   69,    0,   69,    0,    0,   35,    0,  125,
    0,   69,   69,    0,    0,    0,    0,    0,   69,  110,
  111,  112,    0,  113,  114,  115,   61,  116,   69,    0,
    0,    0,    0,    0,  117,  118,    0,    0,   36,    0,
    0,  119,   61,    0,    0,    0,    0,    0,    0,    0,
    0,  120,    0,    0,    0,    0,    0,    0,    0,    0,
  434,    0,    0,    0,    0,  211,  212,  213,  214,  215,
   34,    0,   61,   61,    0,    0,   61,   61,   61,   61,
   61,   61,    0,    0,  110,  111,  112,    0,  113,  114,
  115,    0,  116,    0,    0,    0,    0,    0,   69,  117,
  118,    0,    0,    0,    0,    0,  119,    0,    0,    0,
    0,    0,   69,   69,   69,    0,  120,    0,    0,    0,
    0,  121,    0,    0,  110,  111,  112,    0,  113,  114,
  115,    0,  116,    0,    0,  122,  123,  124,  443,  117,
  118,    0,    0,  110,  111,  112,  119,  113,  114,  115,
    0,  116,    0,    0,    0,    0,  120,    0,  117,  118,
    0,    0,  110,  111,  112,  119,  113,  114,  115,    0,
  116,    0,    0,    0,    0,  120,    0,  664,  665,    0,
    0,    0,    0,    0,  119,    0,  121,  110,  111,  112,
  245,  113,  114,  115,  120,  116,    0,    0,    0,    0,
  122,  123,  124,    0,  246,    0,  247,    0,  286,  119,
    0,   21,    0,    0,    0,    0,  445,    0,    0,  120,
   22,    0,    0,    0,    0,  155,  121,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,    0,
  122,  123,  124,  156,    0,  121,    0,    0,    0,    0,
    0,  287,  288,  289,    0,    0,    0,    0,    0,  122,
  123,  124,  290,    0,    0,  291,  292,  293,  294,  295,
  296,  297,  298,  299,    0,    0,  300,  301,    0,    0,
  302,  303,  304,  305,  306,  307,  286,    0,  308,  309,
  310,    0,    0,    0,  311,  456,    0,    0,  312,  313,
  314,    0,    0,    0,  315,  316,  317,  318,  319,    0,
  320,    0,  321,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,    0,    0,    0,    0,    0,  287,
  288,  289,    0,    0,  323,  324,  325,  326,    0,    0,
  290,    0,    0,  291,  292,  293,  294,  295,  296,  297,
  298,  299,    0,    0,  300,  301,    0,    0,  302,  303,
  304,  305,  306,  307,  286,    0,  308,  309,  310,    0,
    0,    0,  311,  512,    0,    0,  312,  313,  314,    0,
    0,    0,  315,  316,  317,  318,  319,    0,  320,    0,
  321,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  322,    0,    0,    0,    0,    0,  287,  288,  289,
    0,    0,  323,  324,  325,  326,    0,    0,  290,    0,
    0,  291,  292,  293,  294,  295,  296,  297,  298,  299,
    0,    0,  300,  301,    0,    0,  302,  303,  304,  305,
  306,  307,    0,  286,  308,  309,  310,    0,    0,    0,
  311,  515,    0,    0,  312,  313,  314,    0,    0,    0,
  315,  316,  317,  318,  319,    0,  320,    0,  321,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  322,
    0,    0,    0,    0,    0,    0,  287,  288,  289,    0,
  323,  324,  325,  326,    0,    0,    0,  290,    0,    0,
  291,  292,  293,  294,  295,  296,  297,  298,  299,    0,
    0,  300,  301,    0,    0,  302,  303,  304,  305,  306,
  307,  286,    0,  308,  309,  310,    0,    0,    0,  311,
  519,    0,    0,  312,  313,  314,    0,    0,    0,  315,
  316,  317,  318,  319,    0,  320,    0,  321,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  322,    0,
    0,    0,    0,    0,  287,  288,  289,    0,    0,  323,
  324,  325,  326,    0,    0,  290,    0,    0,  291,  292,
  293,  294,  295,  296,  297,  298,  299,    0,    0,  300,
  301,    0,    0,  302,  303,  304,  305,  306,  307,  286,
    0,  308,  309,  310,    0,    0,    0,  311,  571,    0,
    0,  312,  313,  314,    0,    0,    0,  315,  316,  317,
  318,  319,    0,  320,    0,  321,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  322,    0,    0,    0,
    0,    0,  287,  288,  289,    0,    0,  323,  324,  325,
  326,    0,    0,  290,    0,    0,  291,  292,  293,  294,
  295,  296,  297,  298,  299,    0,    0,  300,  301,    0,
    0,  302,  303,  304,  305,  306,  307,    0,  286,  308,
  309,  310,    0,    0,    0,  311,  621,    0,    0,  312,
  313,  314,    0,    0,    0,  315,  316,  317,  318,  319,
    0,  320,    0,  321,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  322,    0,    0,    0,    0,    0,
    0,  287,  288,  289,    0,  323,  324,  325,  326,    0,
    0,    0,  290,    0,    0,  291,  292,  293,  294,  295,
  296,  297,  298,  299,    0,    0,  300,  301,    0,    0,
  302,  303,  304,  305,  306,  307,  286,    0,  308,  309,
  310,    0,    0,    0,  311,  182,    0,    0,  312,  313,
  314,    0,    0,    0,  315,  316,  317,  318,  319,    0,
  320,    0,  321,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  322,    0,    0,    0,    0,    0,  287,
  288,  289,    0,    0,  323,  324,  325,  326,    0,    0,
  290,    0,    0,  291,  292,  293,  294,  295,  296,  297,
  298,  299,    0,    0,  300,  301,    0,    0,  302,  303,
  304,  305,  306,  307,  286,    0,  308,  309,  310,    0,
    0,    0,  311,  180,    0,    0,  312,  313,  314,    0,
    0,    0,  315,  316,  317,  318,  319,    0,  320,    0,
  321,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  322,    0,    0,    0,    0,    0,  287,  288,  289,
    0,    0,  323,  324,  325,  326,    0,    0,  290,    0,
    0,  291,  292,  293,  294,  295,  296,  297,  298,  299,
    0,    0,  300,  301,    0,    0,  302,  303,  304,  305,
  306,  307,    0,  182,  308,  309,  310,    0,    0,    0,
  311,  183,    0,    0,  312,  313,  314,    0,    0,    0,
  315,  316,  317,  318,  319,    0,  320,    0,  321,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  322,
    0,    0,    0,    0,    0,    0,  182,  182,  182,    0,
  323,  324,  325,  326,    0,    0,    0,  182,    0,    0,
  182,  182,  182,  182,  182,  182,  182,  182,  182,    0,
    0,  182,  182,    0,    0,  182,  182,  182,  182,  182,
  182,  180,    0,  182,  182,  182,    0,    0,    0,  182,
  181,    0,    0,  182,  182,  182,    0,    0,    0,  182,
  182,  182,  182,  182,    0,  182,    0,  182,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  182,    0,
    0,    0,    0,    0,  180,  180,  180,    0,    0,  182,
  182,  182,  182,    0,    0,  180,    0,    0,  180,  180,
  180,  180,  180,  180,  180,  180,  180,    0,    0,  180,
  180,    0,    0,  180,  180,  180,  180,  180,  180,  183,
    0,  180,  180,  180,    0,    0,   35,  180,    0,    0,
    0,  180,  180,  180,    0,    0,    0,  180,  180,  180,
  180,  180,    0,  180,    0,  180,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  180,   36,    0,   35,
    0,    0,  183,  183,  183,    0,    0,  180,  180,  180,
  180,    0,    0,  183,    0,    0,  183,  183,  183,  183,
  183,  183,  183,  183,  183,    0,    0,  183,  183,   34,
   36,  183,  183,  183,  183,  183,  183,  258,  181,  183,
  183,  183,    0,    0,    0,  183,   35,    0,  263,  183,
  183,  183,    0,    0,    0,  183,  183,  183,  183,  183,
    0,  183,   34,  183,    0,    0,    0,    0,    0,    0,
    0,    0,  613,    0,  183,    0,    0,   36,    0,    0,
    0,  181,  181,  181,  285,  183,  183,  183,  183,    0,
    0,   35,  181,    0,    0,  181,  181,  181,  181,  181,
  181,  181,  181,  181,    0,  345,  181,  181,    0,   34,
  181,  181,  181,  181,  181,  181,    0,   35,  181,  181,
  181,    0,   36,    0,  181,    0,    0,    0,  181,  181,
  181,    0,    0,    0,  181,  181,  181,  181,  181,    0,
  181,    0,  181,    0,    0,    0,   35,    0,   36,    0,
    0,    0,    0,  181,   34,    0,    0,    0,  438,    0,
  439,    0,  442,    0,  181,  181,  181,  181,    0,    0,
   21,    0,    0,   35,  451,    0,  453,   36,    0,   22,
   34,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,   35,    0,
    0,  170,    0,   21,   36,    0,    0,    0,    0,   34,
    0,    0,   22,    0,    0,    0,    0,  171,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,   36,
    0,   35,    0,    0,  361,  362,   34,    0,    0,    0,
    0,   70,    0,    0,    0,    0,    0,  172,  173,    0,
   21,  174,  175,  176,  177,  178,  179,   35,    0,   22,
    0,   34,   36,  143,  511,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,    0,    0,
  517,   41,  518,    0,    0,    0,  520,    0,   36,    0,
    0,   35,    0,    0,   34,   21,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,  612,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   34,   21,   36,    0,   35,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,    0,
   21,    0,   31,    0,   34,   36,   62,   32,   33,   22,
    0,    0,    0,    0,  155,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   35,    0,   21,    0,    0,
    0,    0,  156,    0,    0,    0,   22,   34,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,  140,  141,    0,    0,   36,    0,  656,  102,
   35,   22,  142,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,    0,    0,
    0,    0,    0,    0,    0,  140,  141,   35,   34,  412,
  413,   36,    0,    0,   22,  142,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,  140,  253,   35,    0,    0,    0,    0,   36,    0,
   22,  254,    0,   34,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,    0,
    0,    0,    0,    0,   36,   21,    0,    0,    0,    0,
   34,    0,    0,    0,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,    0,    0,    0,    0,   34,    0,   21,    0,
    0,    0,    0,    0,    0,    0,    0,   22,  644,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,    0,    0,    0,  414,  415,
  416,  417,    0,    0,    0,    0,    0,  418,  419,  420,
  421,  422,  423,  424,  425,  426,  427,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,  401,  402,  403,  404,  405,  406,
  407,  408,  409,  410,   21,    0,    0,    0,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,  612,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,  252,  252,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,  140,    0,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,  252,  252,  252,    0,    0,    0,
    0,    0,    0,    0,    0,  252,    0,    0,  252,  252,
  252,  252,  252,  252,  252,  252,  252,    0,    0,  252,
  252,    0,    0,  252,  252,  252,  252,  252,  252,  228,
  228,  252,  252,  252,    0,    0,    0,  252,    0,    0,
    0,  252,  252,  252,    0,    0,    0,  252,  252,  252,
  252,  252,    0,  252,    0,  252,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  252,    0,    0,    0,
    0,    0,  228,  228,  228,    0,    0,  252,  252,  252,
  252,    0,    0,  228,    0,    0,  228,  228,  228,  228,
  228,  228,  228,  228,  228,    0,    0,  228,  228,    0,
    0,  228,  228,  228,  228,  228,  228,  230,  230,  228,
  228,  228,    0,    0,    0,  228,    0,    0,    0,  228,
  228,  228,    0,    0,    0,  228,  228,  228,  228,  228,
    0,  228,    0,  228,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  228,    0,    0,    0,    0,    0,
  230,  230,  230,    0,    0,  228,  228,  228,  228,    0,
    0,  230,    0,    0,  230,  230,  230,  230,  230,  230,
  230,  230,  230,    0,    0,  230,  230,    0,    0,  230,
  230,  230,  230,  230,  230,  232,  232,  230,  230,  230,
    0,    0,    0,  230,    0,    0,    0,  230,  230,  230,
    0,    0,    0,  230,  230,  230,  230,  230,    0,  230,
    0,  230,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  230,    0,    0,    0,    0,    0,  232,  232,
  232,    0,    0,  230,  230,  230,  230,    0,    0,  232,
    0,    0,  232,  232,  232,  232,  232,  232,  232,  232,
  232,    0,    0,  232,  232,    0,    0,  232,  232,  232,
  232,  232,  232,  242,  242,  232,  232,  232,    0,    0,
    0,  232,    0,    0,    0,  232,  232,  232,    0,    0,
    0,  232,  232,  232,  232,  232,    0,  232,    0,  232,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  232,    0,    0,    0,    0,    0,  242,  242,  242,    0,
    0,  232,  232,  232,  232,    0,    0,  242,    0,    0,
  242,  242,  242,  242,  242,  242,  242,  242,  242,    0,
    0,  242,  242,    0,    0,  242,  242,  242,  242,  242,
  242,  231,  231,  242,  242,  242,    0,    0,    0,  242,
    0,    0,    0,  242,  242,  242,    0,    0,    0,  242,
  242,  242,  242,  242,    0,  242,    0,  242,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  242,    0,
    0,    0,    0,    0,  231,  231,  231,    0,    0,  242,
  242,  242,  242,    0,    0,  231,    0,    0,  231,  231,
  231,  231,  231,  231,  231,  231,  231,    0,    0,  231,
  231,    0,    0,  231,  231,  231,  231,  231,  231,  243,
  243,  231,  231,  231,    0,    0,    0,  231,    0,    0,
    0,  231,  231,  231,    0,    0,    0,  231,  231,  231,
  231,  231,    0,  231,    0,  231,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  231,    0,    0,    0,
    0,    0,  243,  243,  243,    0,    0,  231,  231,  231,
  231,    0,    0,  243,    0,    0,  243,  243,  243,  243,
  243,  243,  243,  243,  243,    0,    0,  243,  243,    0,
    0,  243,  243,  243,  243,  243,  243,  286,    0,  243,
  243,  243,    0,    0,    0,  243,    0,    0,    0,  243,
  243,  243,    0,    0,    0,  243,  243,  243,  243,  243,
    0,  243,    0,  243,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  243,    0,    0,    0,    0,    0,
  287,  288,  289,    0,    0,  243,  243,  243,  243,    0,
    0,  290,    0,    0,  291,  292,  293,  294,  295,  296,
  297,  298,  299,    0,    0,  300,  301,    0,    0,  302,
  303,  304,  305,  306,  307,  186,    0,  308,  309,  310,
    0,    0,    0,  311,    0,    0,    0,  312,  313,  314,
    0,    0,    0,  315,  316,  317,  318,  319,    0,  320,
    0,  321,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  322,    0,    0,    0,    0,    0,  186,  186,
  186,    0,    0,  323,  324,  325,  326,    0,    0,  186,
    0,    0,  186,  186,  186,  186,  186,  186,  186,  186,
  186,    0,    0,  186,  186,    0,    0,  186,  186,  186,
  186,  186,  186,  187,    0,  186,  186,  186,    0,    0,
    0,  186,    0,    0,    0,  186,  186,  186,    0,    0,
    0,  186,  186,  186,  186,  186,    0,  186,    0,  186,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  186,    0,    0,    0,    0,    0,  187,  187,  187,    0,
    0,  186,  186,  186,  186,    0,    0,  187,    0,    0,
  187,  187,  187,  187,  187,  187,  187,  187,  187,    0,
    0,  187,  187,    0,    0,  187,  187,  187,  187,  187,
  187,  188,    0,  187,  187,  187,    0,    0,    0,  187,
    0,    0,    0,  187,  187,  187,    0,    0,    0,  187,
  187,  187,  187,  187,    0,  187,    0,  187,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  187,    0,
    0,    0,    0,    0,  188,  188,  188,    0,    0,  187,
  187,  187,  187,    0,    0,  188,    0,    0,  188,  188,
  188,  188,  188,  188,  188,  188,  188,    0,    0,  188,
  188,    0,    0,  188,  188,  188,  188,  188,  188,  189,
    0,  188,  188,  188,    0,    0,    0,  188,    0,    0,
    0,  188,  188,  188,    0,    0,    0,  188,  188,  188,
  188,  188,    0,  188,    0,  188,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  188,    0,    0,    0,
    0,    0,  189,  189,  189,    0,    0,  188,  188,  188,
  188,    0,    0,  189,    0,    0,  189,  189,  189,  189,
  189,  189,  189,  189,  189,    0,    0,  189,  189,    0,
    0,  189,  189,  189,  189,  189,  189,    0,    0,  189,
  189,  189,    0,    0,    0,  189,    0,    0,    0,  189,
  189,  189,    0,    0,    0,  189,  189,  189,  189,  189,
    0,  189,    0,  189,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  189,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  189,  189,  189,  189,  290,
    0,    0,  291,  292,  293,  294,  295,  296,  297,  298,
  299,    0,    0,  300,  301,    0,    0,  302,  303,  304,
  305,  306,  307,    0,    0,  308,  309,  310,    0,    0,
    0,  311,    0,    0,    0,  312,  313,  314,    0,    0,
    0,  315,  316,  317,  318,  319,    0,  320,    0,  321,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  322,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  323,  324,  325,  326,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  216,  283,  108,    6,    7,   61,  125,   40,   33,
   33,  123,  123,  461,  327,  163,  123,   42,   44,  123,
   91,  169,  335,   40,   31,  338,   40,   34,   40,   31,
   40,   40,   39,  123,   41,  266,  123,   39,   44,   41,
  353,   44,  325,   42,   60,   40,   53,  123,  153,   44,
   55,   56,   44,  284,  125,  126,  127,  162,    0,  123,
   67,  209,  167,  168,   61,   72,  123,   74,  139,  195,
   72,    0,  220,  521,   81,  123,   83,  225,  226,   44,
   44,   40,  262,  508,  509,   44,  266,   94,   95,   40,
   97,  274,   42,  100,   93,   42,  101,  568,  569,  125,
  205,    0,  228,  108,  284,  606,   61,  123,  256,   60,
   44,  308,  183,  184,  185,   62,   42,   61,   44,  125,
  293,  294,  125,  260,  131,  308,  133,  440,   62,   93,
  125,  444,  203,  125,  276,  308,   41,  638,  280,   44,
   91,   91,  260,  148,  457,  570,  257,  293,  294,  620,
  433,  297,  298,  333,  259,   61,  336,  283,  276,  166,
  125,   61,  274,   80,  364,  236,  125,  274,  368,  257,
  274,  371,  123,   42,  354,   60,  624,  292,  378,  294,
   42,   54,   44,   56,  274,  300,  334,  274,  306,  307,
  270,  271,  310,  311,  312,  313,  314,  315,  274,   41,
  513,  308,   44,  280,  308,  210,   91,  655,   61,   82,
  274,   84,  219,  130,  340,  132,  287,  274,  289,   40,
   41,  308,   41,   44,  231,   44,  274,   40,  145,   40,
   41,  260,  308,   44,  305,  306,  307,  123,  123,  310,
  272,  312,  313,  314,  315,  316,  317,  318,  319,  320,
  274,  274,  260,  277,   41,  272,   40,   44,  272,  572,
  272,  441,  272,  272,  297,  298,  273,  274,  275,  267,
  341,  274,    0,   41,  290,  192,   44,  194,  303,  304,
  305,  306,  307,  290,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,   41,   42,
   41,  308,  309,   44,  311,  267,  257,  258,  259,  274,
  261,  262,  263,  123,  265,   42,  323,  324,  325,  293,
  294,  272,  273,  325,   41,   42,  268,  269,  279,   41,
  272,  273,   44,  275,  514,   41,   42,   76,  289,  268,
  269,  123,   41,  272,  273,   44,  275,  123,   41,  291,
  292,   44,   41,   92,   42,   44,   44,  364,   40,   98,
    0,  368,  291,  292,  371,  126,  127,  309,  107,  268,
  269,  378,  379,  272,  273,   60,  275,  361,   41,  264,
  309,   44,  663,  390,   41,   40,   40,   44,  273,   40,
   40,    0,  291,  292,   58,   44,  281,  282,  283,  284,
  285,  286,  287,  288,  411,   42,   91,   44,  359,   42,
  309,   44,  483,  484,  485,  154,   58,   62,  308,  490,
   61,  428,  373,  374,  375,  432,  433,   40,   40,  360,
  432,  433,  647,   44,    0,  274,   44,  274,  123,  257,
  260,  358,   60,    0,  368,  368,  363,  368,  360,  366,
  367,   44,  369,  370,   44,  372,  373,  374,  375,  376,
  377,   40,  567,  380,  381,  382,  383,  360,  360,  617,
  274,   40,   41,   91,  274,   44,  361,  257,  360,  550,
  487,  488,  257,  257,  491,  492,  493,  494,  495,  496,
  497,  498,  499,   62,  565,  360,   61,  401,  257,  625,
  257,  274,  650,  510,  273,  123,   44,   44,  510,   44,
   44,   44,  368,  430,  619,   44,  368,  368,  368,  645,
  368,  368,  368,  368,   93,  368,   44,  598,  599,  266,
  257,  258,  259,   44,  261,  262,  263,  663,  265,   44,
  268,  269,   44,    0,  272,  273,  463,  275,   44,   40,
  467,   44,  279,  470,   44,   44,  125,   44,   44,  566,
  477,  478,  289,  291,  292,  303,  304,  305,  306,  307,
  577,   44,  643,   44,   44,   44,   44,   44,   44,  264,
   44,  309,  360,  500,  501,  502,  293,  294,  273,   40,
  297,  298,  299,  300,  301,   44,  281,  282,  283,  284,
  285,  286,  287,  288,   44,  612,  523,   91,  525,  526,
   44,  528,  529,   44,  531,  532,  533,  534,  535,  536,
  627,    0,  539,  540,  541,  542,   44,   40,  268,  269,
  547,  257,  272,  273,   44,  275,   44,   44,   44,  257,
  258,  259,  649,  261,  262,  263,   44,  265,   44,   44,
   44,  291,  292,   91,  272,  273,   44,  360,  343,  268,
  269,  279,  579,  272,  273,  582,  275,   44,  585,  309,
  360,  289,   44,   44,  257,  592,  593,  257,   44,   93,
   40,    0,  291,  292,   40,  303,  304,  305,  306,  307,
  607,  608,  609,   10,   57,  139,  138,  614,   18,  198,
  309,  180,  268,  269,  273,  274,  272,  273,   67,  275,
  166,  268,  269,  219,   40,  272,  273,  614,  275,  329,
  623,  488,  329,  355,  649,  291,  292,   -1,  564,  627,
  647,   -1,   -1,   -1,  291,  292,  340,   -1,   -1,   -1,
   -1,  359,   -1,  309,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  309,   -1,   -1,  373,  374,  375,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  273,  274,  353,  354,  355,   -1,   -1,   -1,
  359,   -1,   40,   -1,  363,  364,  365,   -1,   -1,  368,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,  268,  269,   -1,   -1,  272,  273,   -1,  275,  388,
   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,  291,  292,  327,   -1,   -1,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   -1,
  341,  342,  309,   -1,  345,  346,  347,  348,  349,  350,
  273,  274,  353,  354,  355,   -1,   -1,   -1,  359,   -1,
   40,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,  388,   -1,   -1,
   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,
  401,  402,  291,  292,  327,   -1,   -1,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,   -1,  341,  342,
  309,   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,
  353,  354,  355,   -1,   -1,   -1,  359,  273,  274,   -1,
  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,
  373,   -1,  375,   40,  377,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  268,  269,   -1,  388,  272,  273,   -1,  275,
   -1,   41,   -1,   -1,   -1,   -1,  399,  400,  401,  402,
  316,  317,  318,   -1,   -1,  291,  292,   -1,   -1,   -1,
   60,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,  309,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   91,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   40,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,  123,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   41,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,  274,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   40,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  388,  261,  262,  263,   -1,  265,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,  276,  327,   -1,  279,
  330,  331,  332,  333,  334,  335,  336,  337,  338,  289,
   41,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,   -1,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,  273,  274,   -1,  369,
  370,  371,  372,  373,  264,  375,   40,  377,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  388,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,  399,
  400,  401,  402,   -1,   -1,   -1,  296,   -1,   -1,  316,
  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,  274,  353,  354,  355,   -1,
   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,
  259,  388,  261,  262,  263,   -1,  265,  316,  317,  318,
   -1,   -1,  399,  400,  401,  402,   -1,  276,  327,   -1,
  279,  330,  331,  332,  333,  334,  335,  336,  337,  338,
  289,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  273,  274,  353,  354,  355,   -1,   -1,   -1,
  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   40,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  388,
  261,  262,  263,  125,  265,  316,  317,  318,   -1,   60,
  399,  400,  401,  402,   42,  276,  327,   -1,  279,  330,
  331,  332,  333,  334,  335,  336,  337,  338,  289,   -1,
  341,  342,   60,   -1,  345,  346,  347,  348,  349,  350,
   91,   -1,  353,  354,  355,   -1,   -1,   -1,  359,  273,
  274,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   91,  375,   -1,  377,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,  388,   -1,   42,
   -1,   44,   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,
  401,  402,  316,  317,  318,  123,   -1,   60,   -1,   -1,
   -1,  125,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   42,
   -1,  345,  346,  347,  348,  349,  350,   -1,   91,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   60,  260,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,  276,   -1,   60,   42,   -1,   -1,
  123,   -1,   -1,   -1,  388,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,   -1,   -1,   -1,  306,  307,   -1,   91,  310,  311,
  312,  313,  314,  315,   -1,   -1,   -1,   -1,   41,   -1,
  123,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   60,   -1,  123,
   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,  257,
  258,  259,   -1,  261,  262,  263,  260,  265,  289,   -1,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   91,   -1,
   -1,  279,  276,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  125,   -1,   -1,   -1,   -1,  303,  304,  305,  306,  307,
  123,   -1,  306,  307,   -1,   -1,  310,  311,  312,  313,
  314,  315,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,  359,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,  373,  374,  375,   -1,  289,   -1,   -1,   -1,
   -1,  359,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,  373,  374,  375,  125,  272,
  273,   -1,   -1,  257,  258,  259,  279,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,  289,   -1,  272,  273,
   -1,   -1,  257,  258,  259,  279,  261,  262,  263,   -1,
  265,   -1,   -1,   -1,   -1,  289,   -1,  272,  273,   -1,
   -1,   -1,   -1,   -1,  279,   -1,  359,  257,  258,  259,
  260,  261,  262,  263,  289,  265,   -1,   -1,   -1,   -1,
  373,  374,  375,   -1,  274,   -1,  276,   -1,  273,  279,
   -1,  264,   -1,   -1,   -1,   -1,  125,   -1,   -1,  289,
  273,   -1,   -1,   -1,   -1,  278,  359,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,
  373,  374,  375,  296,   -1,  359,   -1,   -1,   -1,   -1,
   -1,  316,  317,  318,   -1,   -1,   -1,   -1,   -1,  373,
  374,  375,  327,   -1,   -1,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  273,   -1,  353,  354,
  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,
  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,
  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,   -1,  353,  354,  355,   -1,
   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   -1,  273,  353,  354,  355,   -1,   -1,   -1,
  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
  399,  400,  401,  402,   -1,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
   -1,  353,  354,  355,   -1,   -1,   -1,  359,  125,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,  273,  353,
  354,  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,  316,  317,  318,   -1,  399,  400,  401,  402,   -1,
   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  273,   -1,  353,  354,
  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,
  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,
  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,   -1,  353,  354,  355,   -1,
   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   -1,  273,  353,  354,  355,   -1,   -1,   -1,
  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
  399,  400,  401,  402,   -1,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
   -1,  353,  354,  355,   -1,   -1,   60,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   91,   -1,   60,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,  123,
   91,  345,  346,  347,  348,  349,  350,  209,  273,  353,
  354,  355,   -1,   -1,   -1,  359,   60,   -1,  220,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,  123,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   41,   -1,  388,   -1,   -1,   91,   -1,   -1,
   -1,  316,  317,  318,  256,  399,  400,  401,  402,   -1,
   -1,   60,  327,   -1,   -1,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,  277,  341,  342,   -1,  123,
  345,  346,  347,  348,  349,  350,   -1,   60,  353,  354,
  355,   -1,   91,   -1,  359,   -1,   -1,   -1,  363,  364,
  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,
  375,   -1,  377,   -1,   -1,   -1,   60,   -1,   91,   -1,
   -1,   -1,   -1,  388,  123,   -1,   -1,   -1,  330,   -1,
  332,   -1,  334,   -1,  399,  400,  401,  402,   -1,   -1,
  264,   -1,   -1,   60,  346,   -1,  348,   91,   -1,  273,
  123,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,   -1,   60,   -1,
   -1,  260,   -1,  264,   91,   -1,   -1,   -1,   -1,  123,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  276,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   91,
   -1,   60,   -1,   -1,  328,  329,  123,   -1,   -1,   -1,
   -1,  302,   -1,   -1,   -1,   -1,   -1,  306,  307,   -1,
  264,  310,  311,  312,  313,  314,  315,   60,   -1,  273,
   -1,  123,   91,  125,  436,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,   -1,
  452,  295,  454,   -1,   -1,   -1,  458,   -1,   91,   -1,
   -1,   60,   -1,   -1,  123,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
  123,  264,   91,   -1,   60,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,
  264,   -1,  295,   -1,  123,   91,  125,  300,  301,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   60,   -1,  264,   -1,   -1,
   -1,   -1,  296,   -1,   -1,   -1,  273,  123,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,  264,  265,   -1,   -1,   91,   -1,   93,  296,
   60,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  265,   60,  123,  261,
  262,   91,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,  264,  265,   60,   -1,   -1,   -1,   -1,   91,   -1,
  273,  274,   -1,  123,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   91,  264,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   -1,   -1,   -1,  380,  381,
  382,  383,   -1,   -1,   -1,   -1,   -1,  389,  390,  391,
  392,  393,  394,  395,  396,  397,  398,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,  378,  379,  380,  381,  382,  383,
  384,  385,  386,  387,  264,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  273,  274,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,  316,  317,  318,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
  274,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,  274,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,  274,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,  274,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
  274,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,   -1,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,
   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,  349,
  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  399,
  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
   -1,  353,  354,  355,   -1,   -1,   -1,  359,   -1,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,  327,
   -1,   -1,  330,  331,  332,  333,  334,  335,  336,  337,
  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,   -1,   -1,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,
   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,  377,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  399,  400,  401,  402,
  };

#line 985 "Repil/IR/IR.jay"

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
  public const int I1 = 284;
  public const int I8 = 285;
  public const int I16 = 286;
  public const int I32 = 287;
  public const int I64 = 288;
  public const int ZEROINITIALIZER = 289;
  public const int OPAQUE = 290;
  public const int DEFINE = 291;
  public const int DECLARE = 292;
  public const int UNNAMED_ADDR = 293;
  public const int LOCAL_UNNAMED_ADDR = 294;
  public const int NOALIAS = 295;
  public const int ELLIPSIS = 296;
  public const int GLOBAL = 297;
  public const int CONSTANT = 298;
  public const int PRIVATE = 299;
  public const int INTERNAL = 300;
  public const int EXTERNAL = 301;
  public const int FASTCC = 302;
  public const int NONNULL = 303;
  public const int NOCAPTURE = 304;
  public const int WRITEONLY = 305;
  public const int READONLY = 306;
  public const int READNONE = 307;
  public const int ATTRIBUTE_GROUP_REF = 308;
  public const int ATTRIBUTES = 309;
  public const int NORECURSE = 310;
  public const int NOUNWIND = 311;
  public const int SPECULATABLE = 312;
  public const int SSP = 313;
  public const int UWTABLE = 314;
  public const int ARGMEMONLY = 315;
  public const int RET = 316;
  public const int BR = 317;
  public const int SWITCH = 318;
  public const int INDIRECTBR = 319;
  public const int INVOKE = 320;
  public const int RESUME = 321;
  public const int CATCHSWITCH = 322;
  public const int CATCHRET = 323;
  public const int CLEANUPRET = 324;
  public const int UNREACHABLE = 325;
  public const int FNEG = 326;
  public const int ADD = 327;
  public const int NUW = 328;
  public const int NSW = 329;
  public const int FADD = 330;
  public const int SUB = 331;
  public const int FSUB = 332;
  public const int MUL = 333;
  public const int FMUL = 334;
  public const int UDIV = 335;
  public const int SDIV = 336;
  public const int FDIV = 337;
  public const int UREM = 338;
  public const int SREM = 339;
  public const int FREM = 340;
  public const int SHL = 341;
  public const int LSHR = 342;
  public const int EXACT = 343;
  public const int ASHR = 344;
  public const int AND = 345;
  public const int OR = 346;
  public const int XOR = 347;
  public const int EXTRACTELEMENT = 348;
  public const int INSERTELEMENT = 349;
  public const int SHUFFLEVECTOR = 350;
  public const int EXTRACTVALUE = 351;
  public const int INSERTVALUE = 352;
  public const int ALLOCA = 353;
  public const int LOAD = 354;
  public const int STORE = 355;
  public const int FENCE = 356;
  public const int CMPXCHG = 357;
  public const int ATOMICRMW = 358;
  public const int GETELEMENTPTR = 359;
  public const int ALIGN = 360;
  public const int INBOUNDS = 361;
  public const int INRANGE = 362;
  public const int TRUNC = 363;
  public const int ZEXT = 364;
  public const int SEXT = 365;
  public const int FPTRUNC = 366;
  public const int FPEXT = 367;
  public const int TO = 368;
  public const int FPTOUI = 369;
  public const int FPTOSI = 370;
  public const int UITOFP = 371;
  public const int SITOFP = 372;
  public const int PTRTOINT = 373;
  public const int INTTOPTR = 374;
  public const int BITCAST = 375;
  public const int ADDRSPACECAST = 376;
  public const int ICMP = 377;
  public const int EQ = 378;
  public const int NE = 379;
  public const int UGT = 380;
  public const int UGE = 381;
  public const int ULT = 382;
  public const int ULE = 383;
  public const int SGT = 384;
  public const int SGE = 385;
  public const int SLT = 386;
  public const int SLE = 387;
  public const int FCMP = 388;
  public const int OEQ = 389;
  public const int OGT = 390;
  public const int OGE = 391;
  public const int OLT = 392;
  public const int OLE = 393;
  public const int ONE = 394;
  public const int ORD = 395;
  public const int UEQ = 396;
  public const int UNE = 397;
  public const int UNO = 398;
  public const int PHI = 399;
  public const int SELECT = 400;
  public const int CALL = 401;
  public const int TAIL = 402;
  public const int VA_ARG = 403;
  public const int LANDINGPAD = 404;
  public const int CATCHPAD = 405;
  public const int CLEANUPPAD = 406;
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
