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
    "WRITEONLY","READONLY","ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE",
    "NOUNWIND","READNONE","SPECULATABLE","SSP","UWTABLE","ARGMEMONLY",
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
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 96:
#line 368 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 375 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 379 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 99:
#line 383 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 100:
#line 387 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 101:
#line 388 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 102:
#line 395 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 103:
#line 399 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 104:
#line 406 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 410 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 106:
#line 414 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 107:
#line 418 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 109:
#line 426 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 110:
#line 430 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 111:
#line 431 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 112:
#line 432 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 113:
#line 433 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 119:
#line 451 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 120:
#line 452 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 121:
#line 453 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 122:
#line 454 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 123:
#line 455 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 124:
#line 456 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 125:
#line 457 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 126:
#line 458 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 127:
#line 459 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 128:
#line 460 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 129:
#line 464 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 130:
#line 465 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 131:
#line 466 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 132:
#line 467 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 133:
#line 468 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 134:
#line 469 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 135:
#line 470 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 136:
#line 471 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 137:
#line 472 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 138:
#line 473 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 139:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 140:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 141:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 142:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 143:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 144:
#line 479 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 146:
#line 484 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 147:
#line 485 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 148:
#line 489 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 149:
#line 493 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 150:
#line 497 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 501 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 505 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 509 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 517 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 156:
#line 518 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 157:
#line 519 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 158:
#line 520 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 159:
#line 521 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 160:
#line 522 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 161:
#line 523 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 162:
#line 524 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 163:
#line 525 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 164:
#line 532 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 167:
#line 550 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 557 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 561 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 568 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 590 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 594 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 601 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 605 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 178:
#line 609 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 613 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 180:
#line 620 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 624 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 184:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 643 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 187:
#line 651 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 188:
#line 652 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 189:
#line 659 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 663 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 670 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 192:
#line 674 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 193:
#line 678 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
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
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 196:
#line 690 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 197:
#line 694 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 199:
#line 699 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 204:
#line 716 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 720 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 726 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 207:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 737 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 744 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 765 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 769 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 773 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 219:
#line 784 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 225:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 226:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 227:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 228:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 229:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 230:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 243:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 244:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 245:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 258:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 952 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 956 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 263:
#line 960 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 964 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 968 "Repil/IR/IR.jay"
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
    4,    4,    4,    4,    4,    4,    5,    5,    5,   27,
   27,   31,   31,   32,   32,   32,   32,   33,   33,   34,
   34,   34,   34,   14,   14,   28,   28,   35,   36,   36,
   36,   36,   36,   36,   36,   36,   36,   36,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   40,   18,   18,   18,   18,   18,   18,
   18,   18,   18,   41,   21,   21,   42,   39,   39,   43,
   44,   38,   38,   29,   29,   45,   45,   45,   45,   46,
   46,   48,   48,   48,   48,   50,   51,   51,   52,   52,
   53,   53,   53,   53,   53,   53,   53,   54,   54,   19,
   19,   55,   55,   56,   56,   57,   58,   58,   59,   60,
   60,   61,   61,   30,   47,   47,   47,   47,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,
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
    9,   10,   10,   11,   11,   12,    5,    6,    6,    3,
    2,    1,    3,    1,    2,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    2,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    6,    9,    6,
    3,    3,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    2,    1,    2,    1,    3,    2,
    1,    1,    3,    1,    2,    2,    3,    1,    2,    1,
    2,    1,    2,    3,    4,    1,    3,    2,    1,    3,
    2,    3,    3,    3,    2,    4,    5,    1,    1,    1,
    3,    1,    1,    1,    3,    5,    1,    2,    3,    1,
    2,    1,    1,    1,    2,    7,    2,    7,    5,    6,
    5,    5,    4,    4,    5,    5,    6,    5,    6,    4,
    5,    6,    5,    5,    4,    4,    5,    6,    7,    6,
    6,    7,    5,    6,    5,    5,    6,    3,    4,    5,
    7,    4,    5,    6,    6,    4,    7,    5,    6,    4,
    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   83,   72,   73,   74,   75,   76,   77,   78,   79,
    0,   29,   28,    0,    0,    0,   71,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  114,  115,   26,
   27,   30,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   64,    0,    0,    0,    0,    0,    0,   82,  214,
    0,    0,    0,    0,    0,    0,    5,    6,    0,    0,
    0,    0,    0,    8,    0,    7,    0,    0,    0,    0,
    0,   65,    0,    0,    0,    0,    0,    0,    0,   89,
   80,    0,    0,   86,    0,    0,    0,  157,  158,  156,
  159,  160,  161,  155,  147,  146,  163,  162,    0,    0,
    0,    0,    0,    0,    0,  145,    0,    0,    0,    0,
    0,    0,    0,   31,    0,    0,    0,   49,   48,   13,
    0,    0,   42,   47,    0,    0,    0,    0,    0,    0,
  106,  107,  101,    0,    0,  102,  118,    0,    0,  116,
   81,    0,    0,    0,    0,    0,    0,   62,   52,   53,
   54,   55,   56,   57,   58,    0,   50,    0,    0,    0,
  168,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   15,    0,    0,    0,   43,   14,    0,  165,    0,
   84,   66,   85,    0,  110,  111,  113,  112,    0,  108,
  100,    0,    0,    0,    0,  117,   87,    0,    0,    0,
    0,   12,   51,    0,    0,    0,  153,    0,  151,  152,
    0,    0,    0,    0,    0,    0,   35,    0,   33,   36,
   37,   32,   17,   16,   46,   45,   44,    0,    0,  109,
  103,    0,    0,   40,    0,    0,   59,  203,  202,    0,
  200,    0,    0,    0,  169,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  174,    0,    0,
  180,    0,    0,   41,    0,   63,    0,    0,    0,    0,
    0,    0,    0,    0,   23,    0,   39,    0,    0,    0,
    0,    0,    0,  217,    0,    0,  215,    0,  212,  213,
    0,    0,  210,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  119,  120,
  121,  122,  123,  124,  125,  126,  127,  128,    0,  129,
  130,  141,  142,  143,  144,  132,  134,  135,  136,  137,
  133,  131,  139,  140,  138,    0,    0,    0,    0,    0,
    0,   90,  175,    0,  181,    0,    0,    0,   91,  201,
    0,  148,  150,    0,    0,    0,    0,   38,   93,    0,
    0,    0,    0,  164,    0,    0,    0,    0,  211,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  204,    0,
  186,    0,    0,    0,    0,    0,   92,    0,    0,    0,
   94,   95,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  230,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  171,    0,  172,   96,    0,    0,
  219,    0,  231,  258,    0,  237,  246,    0,  234,  261,
  250,  233,  263,  253,    0,    0,  243,  222,  245,  264,
    0,    0,  221,  154,  167,    0,    0,    0,    0,    0,
    0,    0,  205,    0,    0,  188,    0,    0,  189,    0,
  225,    0,    0,  149,    0,    0,    0,    0,    0,  207,
  220,  259,  247,  254,  244,  241,  255,    0,    0,    0,
    0,  240,  232,    0,    0,    0,    0,  191,    0,  187,
    0,    0,  229,  173,  216,  170,    0,  218,  208,  242,
  257,    0,  206,  251,    0,  199,  193,  198,  194,  192,
  190,  209,  196,    0,  197,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  176,  141,  133,   53,
  142,  491,  215,   54,   55,   39,  134,  126,  260,  143,
  555,  177,   64,   65,  103,  104,   99,  159,  317,   71,
  155,  156,  209,  210,  160,  399,  416,  556,  182,  585,
  347,  532,  608,  557,  318,  319,  320,  321,  322,  492,
  551,  598,  599,  649,  261,  488,  489,  609,  610,  352,
  353,
  };
  protected static readonly short [] yySindex = {         1160,
   -3,  -39,   64,   68,   76, 2695, 2744, -244,    0, 1160,
    0,    0,    0,    0,  -92,  102,  132,  106, -136,  -27,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3008,    0,    0, 2897, -118,  -70,    0,  194,  -62,  -19,
 3008,  -18,  183,    0,    0,   13,   36,    0,    0,    0,
    0,    0, 3008, -188,  -91,  -85,    4,  216,  -29,  144,
  -10,    0,  194,    1,  249,   38, 3008,   72,    0,    0,
 3008,  307, 2605,   -9,  307,  236,    0,    0, 1466, 3008,
 -188, 3008, -188,    0,  241,    0, -194,  328,  255, 2805,
  307,    0, 3008, 3008,    7, 3008,   -8, 2576, -134,    0,
    0,  194,   92,    0,  307, -134, -198,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   20,  344,
  347, 3036, 3036, 3036,  348,    0, 1466, 3008, 1466, 3008,
  336,  338,  105,    0, -194, 2838,    0,    0,    0,    0,
   25, 1466,    0,    0,  -91,  194,   33,  339,   10,  307,
    0,    0,    0,  -34,  113,    0,    0,   91, -226,    0,
    0, 2728,   91,   91,   91,  357,  369,    0,    0,    0,
    0,    0,    0,    0,    0, 1537,    0,  381, 3036, 3036,
    0,    9,   58,   45,   77,  396, 1466,  398, 1447, 2518,
  169,    0, -194,  134,   26,    0,    0, 2864,    0,   91,
    0,    0,    0,  -91,    0,    0,    0,    0,   39,    0,
    0, 2631, -112,  171, -116,    0,    0,   91,   91,  184,
  843,    0,    0, 3008,   78,   80,    0, 3036,    0,    0,
  192,   90,  407,   95,   97,  408,    0,  418,    0,    0,
    0,    0,    0,    0,    0,    0,    0, -100,   91,    0,
    0, 3555, -103,    0,  185, 3555,    0,    0,    0,  157,
    0,  139, 3008, 3008,    0,  186,  205,  103,  207,  208,
  108,  -24, 3555,  -90,  -97,  420, 3036, -161, 3036, 2480,
 3008, 2480, 3008, 2480, 3008, 3008, 3008, 3008, 3008, 2480,
  903, 3008, 3008, 3008, 3036, 3036, 3036, 3008, 3008, 3036,
  660, 3036, 3036, 3036, 3036, 3036, 3036, 3036, 3036, 3036,
  -68,  303, 3008, 3008, 2529,   81, 1555,    0, 3555,  186,
    0,  186, 3555,    0, 1634,    0,  843, 3036,  219,  341,
  209,  186,  227,  186,    0,  229,    0,  170, 1712, 3555,
 3555,  -89, 3943,    0,  215, 1520,    0,  446,    0,    0,
 1466, 2480,    0, 1466, 1466, 2480, 1466, 1466, 2480, 1466,
 1466, 1466, 1466, 1466, 1466, 2480, 3008, 1466, 1466, 1466,
 1466,  447,  448,  449,  150,  177,  450, 3008,  280,  127,
  128,  129,  131,  136,  137,  138,  140,  141,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3008,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3008,   49, 1466, 1302, 3008,
 2529,    0,    0,  186,    0,  209,  209, 1790,    0,    0,
  456,    0,    0,  209,  186,  209,  186,    0,    0, 1869,
 1947, 3555,  186,    0,  463,  245,  469, 1466,    0,  471,
  474, 1466,  476,  477, 1466,  478,  479,  482,  485,  486,
  489, 1466, 1466,  491,  494,  495,  498, 3036, 3036, 3036,
  156, 3008, 3008,  335, 3036, 3008, 3008, 3008, 3008, 3008,
 3008, 3008, 3008, 3008, 1466, 1466, 1520,  502,    0,  508,
    0,  513, 1302, 1302, 3008,  209,    0, 3036,  209,  209,
    0,    0, 2025,  209,  245,  464, 1520,  512, 1520, 1520,
  514, 1520, 1520,  517, 1520, 1520, 1520, 1520, 1520, 1520,
  518,  519, 1520, 1520, 1520, 1520,    0,  522,  524,  300,
 1466,  526,  529, 3036,  539,  194,  194,  194,  194,  194,
  194,  194,  194,  194,  540,  547,  548,  503, 3036, 2659,
   91,  513,  513, 1302,    0,  262,    0,    0,  553, 3008,
    0, 1520,    0,    0, 1520,    0,    0, 1520,    0,    0,
    0,    0,    0,    0, 1520, 1520,    0,    0,    0,    0,
 3036, 3036,    0,    0,    0,  238,  244,  557, 3036, 1520,
 1520, 1520,    0,  561, 2913,    0, 1395,  287,    0,   91,
    0,   91,  513,    0, 3036,  245, 1325,  562, 2958,    0,
    0,    0,    0,    0,    0,    0,    0,  350,  353, 3036,
  567,    0,    0,  521, 3036,  576, 1262,    0, 1485,    0,
 2983,   91,    0,    0,    0,    0,  245,    0,    0,    0,
    0,  567,    0,    0,  600,    0,    0,    0,    0,    0,
    0,    0,    0,  292,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  617,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1358,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   -4,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  580,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  126,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  580,    0,  580,    0,
    0,    0,    0,    0,    0,    0,  394,    0,    0,    0,
    0,  580,    0,    0,    0,    6,  580,    0,  580,    0,
    0,    0,    0,  202,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   29, 2514, 2896,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  580,    0,  580,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  294,    0,
    0,    0,    0,    0,    0,    0,    0,   57,   83,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   94,    0,    0,    0,    0,  313,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  580,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2104,
    0, 3633,    0,    0,    0,    0,    0,    0,  580,  580,
  259,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  580,    0,    0,  580,  580,    0,  580,  580,    0,  580,
  580,  580,  580,  580,  580,    0,    0,  580,  580,  580,
  580,    0,    0,    0,  580,  580,    0,    0,  580,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  580,  580,    0,    0,
    0,    0,    0, 2182,    0, 2260, 3711,    0,    0,    0,
    0,    0,    0,  268,  327,  340,    0,    0,    0,    0,
    0,    0, 3789,    0,    0,    0,    0,  580,    0,    0,
    0,  580,    0,    0,  580,    0,    0,    0,    0,    0,
    0,  580,  580,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  580,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  580,  580,    0, 3009,    0,    0,
    0,    0,    0,    0,    0, 2339,    0,    0,  371,  397,
    0,    0,    0, 3867,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  580,    0,    0,    0,    0,  472,  550,  637,  715,  793,
  886,  964, 1042, 1129,    0,    0,    0,    0,    0,    0,
 3087,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  580,    0,    0, 3165,
    0, 3243,    0,    0,    0,    0,  580,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3321,    0,    0,    0,    0,  329,  580,    0,    0,    0,
    0, 3399,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3477,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  611,  565,    0,    0,    0,    0,  487,  493,   99,
   -6,   62, 2628,  235,    0,  608,  436, -166, -269,    0,
  -80,  457,  569,    8,    0,  480,    2, -104, -176, -302,
    0,  422,   48, -193, -147,    0,    0, -516,  288,    0,
 -437,  174,    0,   44, -298,    0,  331,  332,  312, -456,
 -468,    0,   27,    0,  330,    0,  112,    0,   47,  -94,
 -180,
  };
  protected static readonly short [] yyTable = {            38,
   38,  165,  338,   89,   67,   60,  256,   69,  506,  144,
  252,  216,  420,   40,   42,  250,  337,  216,  423,  323,
   73,   73,  273,  240,   38,  341,  423,   63,   97,   73,
   73,   73,  340,  442,   38,   69,  552,  553,   61,   67,
  423,  181,  181,  181,   93,   69,   79,  214,   74,   68,
   93,   69,  228,  213,  259,  144,   98,   15,  218,  219,
   63,  166,   43,   85,   38,  216,  102,  559,  198,  198,
  216,  216,  621,  127,   69,  129,  106,  167,   97,  325,
  157,  131,   99,  601,  602,  132,  146,  147,  228,  149,
   69,  154,  145,   21,  201,  248,  339,  603,  225,  226,
  216,  228,  203,  642,  345,  259,  163,  168,   50,   51,
  169,  170,  171,  172,  173,  174,  175,  247,  495,  229,
   67,  187,  346,  189,   18,   92,   34,  216,   19,  423,
   68,  148,  161,  227,  633,  162,   20,  230,   66,  487,
  125,  423,  423,   57,  275,  192,  428,  265,  193,  197,
  244,  204,   80,  211,   82,  102,  212,  255,   48,   49,
  259,  214,   46,  440,  441,   69,   88,   45,  635,   88,
  255,  449,  157,  214,  243,  449,  214,  193,  449,  128,
   69,  130,  328,  255,  255,  449,   68,  356,  186,  359,
  188,   69,   47,  471,  157,  366,  344,  326,  348,  652,
  327,   48,   49,  199,  423,  154,  157,   48,   49,  157,
  438,   50,   51,  327,  372,  373,  374,  262,   69,  377,
  472,  380,  381,  382,  383,  384,  385,  386,  387,  388,
   16,   17,  108,  109,  110,   69,  111,  112,  113,   70,
  114,   69,  104,   76,   88,  104,   58,  431,  233,   59,
  236,  258,   72,   75,  117,   87,  329,  330,   18,  432,
   69,   91,  105,  150,  118,  503,   90,   19,  205,  206,
  207,  208,   77,  351,  354,  355,  357,  358,  360,  361,
  362,  363,  364,  365,  368,  369,  370,  371,   73,   81,
   83,  375,  376,   84,  379,   78,   97,   97,  196,  196,
   97,   97,  604,   97,   94,  605,  417,  418,   38,  389,
  390,  391,  392,  393,  394,  395,  396,  397,  398,   97,
   97,   69,  419,  475,   98,   98,   20,  630,   98,   98,
  631,   98,  655,  158,  105,  327,   97,  105,   96,   25,
  164,  205,  206,  207,  208,  448,   98,   98,   98,  452,
   99,   99,  455,   34,   99,   99,   34,   99,  107,  462,
  463,   21,   21,   67,   98,   21,   21,  135,   21,  195,
   22,  474,  195,   99,   99,  654,   69,  136,  534,  200,
  178,  433,   69,  179,   21,   21,  180,  527,  528,  529,
   99,  185,  485,  190,  535,  191,   24,  157,   48,   49,
  202,   21,   50,   51,   52,   32,   33,  445,  221,  486,
  183,  184,  447,   38,   38,  450,  451,  220,  453,  454,
  224,  456,  457,  458,  459,  460,  461,  493,  494,  464,
  465,  466,  467,   70,  166,  250,  231,  166,  249,  232,
  636,  234,  241,  257,  254,  263,  600,  264,  266,  267,
  268,  271,  216,  588,  269,  166,  270,  272,  324,  214,
  648,  332,  333,  334,  335,  531,  531,  336,  594,  536,
  537,  538,  539,  540,  541,  542,  543,  544,  259,  490,
  343,  421,  255,  435,  216,  437,  166,  444,   38,  446,
  468,  469,  470,  473,  476,  477,  478,  632,  479,  498,
  616,  617,  554,  480,  481,  482,  505,  483,  484,  508,
  345,   69,  507,  511,  509,  530,  514,  510,  166,  512,
  513,  515,  516,  521,  522,  517,   18,   18,  518,  519,
   18,   18,  520,   18,  523,   19,   19,  524,  525,   19,
   19,  526,   19,  597,  644,  548,  545,  546,  547,   18,
   18,  549,  550,  607,  560,  562,  583,  565,   19,   19,
  568,  575,  576,  400,  401,  581,   18,  582,  561,  586,
  563,  564,  587,  566,  567,   19,  569,  570,  571,  572,
  573,  574,  589,  590,  577,  578,  579,  580,  627,   69,
  591,  592,  584,  487,   20,   20,  606,  618,   20,   20,
  620,   20,  607,  619,  625,  637,  640,   25,   25,  641,
  605,   25,   25,  643,   25,  645,    1,   20,   20,   69,
   44,   86,  195,  611,  597,   56,  612,  194,  242,  613,
   25,   25,  223,  251,   20,   95,  614,  615,   22,   22,
  653,  217,   22,   22,  629,   22,  533,   25,  634,  424,
  425,  622,  623,  624,  443,  639,  430,  651,  628,  593,
    0,   22,   22,    0,   24,   24,  166,  166,   24,   24,
    0,   24,    0,    0,    0,    0,   69,    0,   22,    0,
    0,    0,  402,  403,  404,  405,    0,   24,   24,    0,
  650,  406,  407,  408,  409,  410,  411,  412,  413,  414,
  415,    0,    0,    0,   24,    0,    0,    0,    0,  166,
  166,  166,    0,    0,    0,    0,    0,    0,    0,   35,
  166,    0,    0,  166,  166,  166,  166,  166,  166,  166,
  166,  166,    0,    0,  166,  166,    0,    0,  166,  166,
  166,  166,  166,  166,  260,  260,  166,  166,  166,    0,
   36,    0,  166,    0,   69,    0,  166,  166,  166,    0,
    0,  166,  166,  166,  166,  166,  166,    0,  166,    0,
  166,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  166,   34,    0,    0,    0,    0,  260,  260,  260,
    0,    0,  166,  166,  166,  166,    0,    0,  260,    0,
    0,  260,  260,  260,  260,  260,  260,  260,  260,  260,
    0,    0,  260,  260,    0,    0,  260,  260,  260,  260,
  260,  260,  265,  265,  260,  260,  260,    0,    0,    0,
  260,    0,   69,    0,  260,  260,  260,    0,    0,    0,
  260,  260,  260,  260,  260,    0,  260,    0,  260,    0,
    0,    0,    0,    0,    0,    0,  108,  109,  110,  260,
  111,  112,  113,    0,  114,  265,  265,  265,    0,    0,
  260,  260,  260,  260,    0,  258,  265,    0,  117,  265,
  265,  265,  265,  265,  265,  265,  265,  265,  118,    0,
  265,  265,    0,    0,  265,  265,  265,  265,  265,  265,
    0,    0,  265,  265,  265,    0,    0,    0,  265,  252,
  252,    0,  265,  265,  265,    0,    0,    0,  265,  265,
  265,  265,  265,   21,  265,   69,  265,    0,    0,    0,
    0,    0,   22,    0,    0,    0,    0,  265,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,  265,  265,
  265,  265,  252,  252,  252,    0,    0,    0,    0,    0,
    0,    0,   35,  252,    0,    0,  252,  252,  252,  252,
  252,  252,  252,  252,  252,    0,    0,  252,  252,    0,
    0,  252,  252,  252,  252,  252,  252,  235,  235,  252,
  252,  252,    0,   36,    0,  252,    0,    0,    0,  252,
  252,  252,    0,   69,    0,  252,  252,  252,  252,  252,
    0,  252,    0,  252,    0,    0,    0,    0,    0,    0,
  378,    0,    0,    0,  252,   34,    0,    0,    0,    0,
  235,  235,  235,    0,    0,  252,  252,  252,  252,    0,
    0,  235,    0,    0,  235,  235,  235,  235,  235,  235,
  235,  235,  235,    0,    0,  235,  235,    0,    0,  235,
  235,  235,  235,  235,  235,  236,  236,  235,  235,  235,
    0,    0,    0,  235,    0,    0,    0,  235,  235,  235,
    0,   69,    0,  235,  235,  235,  235,  235,    0,  235,
    0,  235,    0,    0,    0,    0,    0,    0,    0,  108,
  109,  110,  235,  111,  112,  113,    0,  114,  236,  236,
  236,    0,    0,  235,  235,  235,  235,    0,  258,  236,
    0,  117,  236,  236,  236,  236,  236,  236,  236,  236,
  236,  118,    0,  236,  236,    0,    0,  236,  236,  236,
  236,  236,  236,    0,    0,  236,  236,  236,    0,    0,
    0,  236,    0,    0,    0,  236,  236,  236,  262,  262,
    0,  236,  236,  236,  236,  236,   21,  236,   69,  236,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
  236,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,  236,  236,  236,  236,    0,    0,    0,    0,    0,
    0,  262,  262,  262,    0,    0,    0,    0,    0,    0,
    0,    0,  262,    0,    0,  262,  262,  262,  262,  262,
  262,  262,  262,  262,    0,    0,  262,  262,    0,    0,
  262,  262,  262,  262,  262,  262,  256,  256,  262,  262,
  262,    0,    0,    0,  262,  367,    0,    0,  262,  262,
  262,    0,    0,    0,  262,  262,  262,  262,  262,    0,
  262,    0,  262,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  262,    0,    0,    0,    0,    0,  256,
  256,  256,    0,    0,  262,  262,  262,  262,    0,    0,
  256,    0,    0,  256,  256,  256,  256,  256,  256,  256,
  256,  256,    0,   69,  256,  256,    0,    0,  256,  256,
  256,  256,  256,  256,  249,  249,  256,  256,  256,    0,
    0,    0,  256,    0,    0,    0,  256,  256,  256,    0,
    0,    0,  256,  256,  256,  256,  256,    0,  256,    0,
  256,   73,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  256,    0,    0,    0,    0,    0,  249,  249,  249,
    0,  123,  256,  256,  256,  256,   69,    0,  249,    0,
    0,  249,  249,  249,  249,  249,  249,  249,  249,  249,
    0,    0,  249,  249,    0,    0,  249,  249,  249,  249,
  249,  249,  124,    0,  249,  249,  249,   69,    0,    0,
  249,  223,  223,    0,  249,  249,  249,    0,    0,    0,
  249,  249,  249,  249,  249,    0,  249,   69,  249,    0,
    0,    0,    0,    0,  122,    0,    0,    1,    2,  249,
    0,    3,    4,    0,    5,    0,   69,    0,    0,    0,
  249,  249,  249,  249,  223,  223,  223,    0,   69,    0,
    6,    7,    0,    0,  123,  223,    0,    0,  223,  223,
  223,  223,  223,  223,  223,  223,  223,    8,    0,  223,
  223,    0,    0,  223,  223,  223,  223,  223,  223,    0,
   69,  223,  223,  223,    0,  124,    0,  223,   69,    0,
  235,  223,  223,  223,    0,    0,    0,  223,  223,  223,
  223,  223,    0,  223,    0,  223,  123,   69,    0,    0,
    0,    0,    0,    0,    0,    0,  223,  122,  108,  109,
  110,    0,  111,  112,  113,  123,  114,  223,  223,  223,
  223,    0,    0,  646,  647,    0,    0,  124,    0,    0,
  117,    0,    0,    0,  123,    0,    0,    0,    0,    0,
  118,    0,    0,    0,    0,    0,  124,    0,  108,  109,
  110,    0,  111,  112,  113,    0,  114,    0,    0,  122,
    0,    0,    0,  115,  116,  124,    0,    0,    0,  123,
  117,  108,  109,  110,    0,  111,  112,  113,  122,  114,
  118,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  117,    0,    0,    0,  122,    0,    0,
  124,    0,    0,  118,   69,   69,   69,    0,   69,   69,
   69,    0,   69,    0,    0,    0,    0,    0,    0,   69,
   69,    0,    0,    0,    0,    0,   69,    0,    0,    0,
    0,    0,  122,    0,    0,    0,   69,    0,    0,    0,
    0,  108,  109,  110,    0,  111,  112,  113,    0,  114,
  119,  222,    0,    0,    0,    0,  115,  116,    0,    0,
    0,    0,    0,  117,    0,  120,  121,    0,    0,  422,
    0,    0,    0,  118,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  205,  206,  207,
  208,    0,    0,  108,  109,  110,    0,  111,  112,  113,
    0,  114,    0,    0,    0,    0,   69,    0,  115,  116,
    0,    0,  108,  109,  110,  117,  111,  112,  113,    0,
  114,   69,   69,    0,    0,  118,    0,  115,  116,    0,
    0,  108,  109,  110,  117,  111,  112,  113,    0,  114,
    0,    0,    0,  119,  118,    0,  115,  116,  429,    0,
    0,    0,    0,  117,    0,    0,    0,    0,  120,  121,
    0,    0,    0,  118,    0,    0,  108,  109,  110,    0,
  111,  112,  113,    0,  114,    0,    0,  205,  206,  207,
  208,  115,  116,    0,    0,    0,  166,    0,  117,    0,
    0,    0,    0,    0,    0,  119,    0,    0,  118,    0,
    0,    0,  167,    0,    0,    0,    0,    0,    0,    0,
  120,  121,    0,    0,  119,    0,    0,  276,    0,    0,
    0,    0,    0,    0,    0,    0,  439,    0,    0,  120,
  121,    0,  168,  119,    0,  169,  170,  171,  172,  173,
  174,  175,    0,    0,    0,    0,    0,    0,  120,  121,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  277,  278,  279,    0,    0,    0,    0,    0,  119,    0,
    0,  280,    0,    0,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  120,  121,  290,  291,    0,    0,  292,
  293,  294,  295,  296,  297,    0,  276,  298,  299,  300,
    0,    0,    0,  301,  497,    0,    0,  302,  303,  304,
    0,    0,    0,  305,  306,  307,  308,  309,    0,  310,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,    0,    0,    0,    0,    0,    0,  277,
  278,  279,    0,  313,  314,  315,  316,    0,    0,    0,
  280,    0,    0,  281,  282,  283,  284,  285,  286,  287,
  288,  289,    0,    0,  290,  291,    0,    0,  292,  293,
  294,  295,  296,  297,  276,    0,  298,  299,  300,    0,
    0,    0,  301,  501,    0,    0,  302,  303,  304,    0,
    0,    0,  305,  306,  307,  308,  309,    0,  310,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  312,    0,    0,    0,    0,    0,  277,  278,  279,
    0,    0,  313,  314,  315,  316,    0,    0,  280,    0,
    0,  281,  282,  283,  284,  285,  286,  287,  288,  289,
    0,    0,  290,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  276,    0,  298,  299,  300,    0,    0,    0,
  301,  502,    0,    0,  302,  303,  304,    0,    0,    0,
  305,  306,  307,  308,  309,    0,  310,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
    0,    0,    0,    0,    0,  277,  278,  279,    0,    0,
  313,  314,  315,  316,    0,    0,  280,    0,    0,  281,
  282,  283,  284,  285,  286,  287,  288,  289,    0,    0,
  290,  291,    0,    0,  292,  293,  294,  295,  296,  297,
    0,  276,  298,  299,  300,    0,    0,    0,  301,  558,
    0,    0,  302,  303,  304,    0,    0,    0,  305,  306,
  307,  308,  309,    0,  310,    0,  311,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  312,    0,    0,
    0,    0,    0,    0,  277,  278,  279,    0,  313,  314,
  315,  316,    0,    0,    0,  280,    0,    0,  281,  282,
  283,  284,  285,  286,  287,  288,  289,    0,    0,  290,
  291,    0,    0,  292,  293,  294,  295,  296,  297,  276,
    0,  298,  299,  300,    0,    0,    0,  301,  178,    0,
    0,  302,  303,  304,    0,    0,    0,  305,  306,  307,
  308,  309,    0,  310,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,    0,    0,    0,
    0,    0,  277,  278,  279,    0,    0,  313,  314,  315,
  316,    0,    0,  280,    0,    0,  281,  282,  283,  284,
  285,  286,  287,  288,  289,    0,    0,  290,  291,    0,
    0,  292,  293,  294,  295,  296,  297,  276,    0,  298,
  299,  300,    0,    0,    0,  301,  176,    0,    0,  302,
  303,  304,    0,    0,    0,  305,  306,  307,  308,  309,
    0,  310,    0,  311,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  312,    0,    0,    0,    0,    0,
  277,  278,  279,    0,    0,  313,  314,  315,  316,    0,
    0,  280,    0,    0,  281,  282,  283,  284,  285,  286,
  287,  288,  289,    0,    0,  290,  291,    0,    0,  292,
  293,  294,  295,  296,  297,    0,  178,  298,  299,  300,
    0,    0,    0,  301,  179,    0,    0,  302,  303,  304,
    0,    0,    0,  305,  306,  307,  308,  309,    0,  310,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,    0,    0,    0,    0,    0,    0,  178,
  178,  178,    0,  313,  314,  315,  316,    0,    0,    0,
  178,    0,    0,  178,  178,  178,  178,  178,  178,  178,
  178,  178,    0,    0,  178,  178,    0,    0,  178,  178,
  178,  178,  178,  178,  176,    0,  178,  178,  178,    0,
    0,    0,  178,  177,    0,    0,  178,  178,  178,    0,
    0,    0,  178,  178,  178,  178,  178,    0,  178,    0,
  178,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  178,    0,    0,    0,    0,    0,  176,  176,  176,
    0,    0,  178,  178,  178,  178,    0,    0,  176,    0,
    0,  176,  176,  176,  176,  176,  176,  176,  176,  176,
    0,    0,  176,  176,    0,    0,  176,  176,  176,  176,
  176,  176,  179,    0,  176,  176,  176,    0,    0,   35,
  176,    0,    0,    0,  176,  176,  176,    0,    0,    0,
  176,  176,  176,  176,  176,    0,  176,    0,  176,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  176,
   36,    0,    0,    0,    0,  179,  179,  179,    0,    0,
  176,  176,  176,  176,    0,    0,  179,    0,   35,  179,
  179,  179,  179,  179,  179,  179,  179,  179,    0,    0,
  179,  179,   34,    0,  179,  179,  179,  179,  179,  179,
    0,  177,  179,  179,  179,    0,  153,    0,  179,   36,
    0,    0,  179,  179,  179,    0,    0,    0,  179,  179,
  179,  179,  179,    0,  179,   35,  179,    0,   60,    0,
    0,    0,    0,    0,    0,  101,    0,  179,    0,    0,
    0,   34,    0,    0,  177,  177,  177,    0,  179,  179,
  179,  179,    0,    0,   35,  177,   36,    0,  177,  177,
  177,  177,  177,  177,  177,  177,  177,    0,    0,  177,
  177,    0,    0,  177,  177,  177,  177,  177,  177,    0,
   35,  177,  177,  177,    0,   36,    0,  177,   34,  596,
    0,  177,  177,  177,    0,    0,    0,  177,  177,  177,
  177,  177,    0,  177,    0,  177,    0,    0,   35,    0,
    0,   36,    0,    0,    0,    0,  177,   34,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  177,  177,  177,
  177,    0,    0,   21,    0,    0,    0,    0,    0,   36,
    0,    0,   22,   34,   35,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,    0,   60,  108,  109,  110,  237,  111,  112,
  113,   34,  114,    0,    0,   36,    0,   35,    0,   60,
    0,  238,   21,  239,    0,    0,  117,    0,    0,    0,
    0,   22,    0,   35,    0,    0,  118,  349,  350,   23,
   24,   25,   26,   27,   28,   29,   30,   34,   36,   60,
    0,    0,   60,   60,   60,   60,   60,   60,   60,    0,
   70,    0,    0,    0,   36,    0,    0,    0,    0,   21,
  253,    0,    0,    0,    0,    0,    0,    0,   22,    0,
   34,    0,    0,  151,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,   35,    0,   34,    0,   21,    0,
    0,  152,    0,    0,    0,  274,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,  331,   21,   36,    0,   35,    0,    0,
  100,    0,  342,   22,    0,    0,    0,    0,  151,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
    0,    0,   21,   35,    0,    0,  152,   34,   36,  140,
    0,   22,    0,    0,    0,    0,  595,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,  426,    0,  427,
    0,    0,    0,    0,   36,    0,   35,    0,   21,  434,
   34,  436,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,   35,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,    0,   34,   36,    0,   31,
    0,   21,    0,    0,   32,   33,    0,    0,    0,    0,
   22,    0,    0,   36,    0,    0,    0,   21,   23,   24,
   25,   26,   27,   28,   29,   30,   22,   35,    0,   34,
   61,   62,    0,  100,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,   34,    0,    0,   41,    0,
    0,    0,   35,    0,    0,    0,    0,    0,   36,    0,
  638,  496,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  499,    0,  500,    0,    0,   35,  137,  138,
  504,    0,    0,   36,    0,    0,    0,   22,  139,    0,
   34,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,   35,    0,    0,   36,    0,
    0,  137,  138,    0,    0,   34,    0,    0,    0,    0,
   22,  139,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,   36,  137,  245,    0,
   34,    0,    0,    0,    0,    0,   22,  246,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,   61,    0,    0,   34,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,   61,    0,    0,    0,    0,   21,   23,   24,   25,
   26,   27,   28,   29,   30,   22,  626,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,   61,    0,    0,   61,   61,   61,   61,   61,   61,
   61,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,   21,    0,    0,    0,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
  595,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,  248,  248,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,  137,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,  248,  248,  248,    0,    0,    0,
    0,    0,    0,    0,    0,  248,    0,    0,  248,  248,
  248,  248,  248,  248,  248,  248,  248,    0,    0,  248,
  248,    0,    0,  248,  248,  248,  248,  248,  248,  224,
  224,  248,  248,  248,    0,    0,    0,  248,    0,    0,
    0,  248,  248,  248,    0,    0,    0,  248,  248,  248,
  248,  248,    0,  248,    0,  248,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  248,    0,    0,    0,
    0,    0,  224,  224,  224,    0,    0,  248,  248,  248,
  248,    0,    0,  224,    0,    0,  224,  224,  224,  224,
  224,  224,  224,  224,  224,    0,    0,  224,  224,    0,
    0,  224,  224,  224,  224,  224,  224,  226,  226,  224,
  224,  224,    0,    0,    0,  224,    0,    0,    0,  224,
  224,  224,    0,    0,    0,  224,  224,  224,  224,  224,
    0,  224,    0,  224,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  224,    0,    0,    0,    0,    0,
  226,  226,  226,    0,    0,  224,  224,  224,  224,    0,
    0,  226,    0,    0,  226,  226,  226,  226,  226,  226,
  226,  226,  226,    0,    0,  226,  226,    0,    0,  226,
  226,  226,  226,  226,  226,  228,  228,  226,  226,  226,
    0,    0,    0,  226,    0,    0,    0,  226,  226,  226,
    0,    0,    0,  226,  226,  226,  226,  226,    0,  226,
    0,  226,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  226,    0,    0,    0,    0,    0,  228,  228,
  228,    0,    0,  226,  226,  226,  226,    0,    0,  228,
    0,    0,  228,  228,  228,  228,  228,  228,  228,  228,
  228,    0,    0,  228,  228,    0,    0,  228,  228,  228,
  228,  228,  228,  238,  238,  228,  228,  228,    0,    0,
    0,  228,    0,    0,    0,  228,  228,  228,    0,    0,
    0,  228,  228,  228,  228,  228,    0,  228,    0,  228,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  228,    0,    0,    0,    0,    0,  238,  238,  238,    0,
    0,  228,  228,  228,  228,    0,    0,  238,    0,    0,
  238,  238,  238,  238,  238,  238,  238,  238,  238,    0,
    0,  238,  238,    0,    0,  238,  238,  238,  238,  238,
  238,  227,  227,  238,  238,  238,    0,    0,    0,  238,
    0,    0,    0,  238,  238,  238,    0,    0,    0,  238,
  238,  238,  238,  238,    0,  238,    0,  238,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  238,    0,
    0,    0,    0,    0,  227,  227,  227,    0,    0,  238,
  238,  238,  238,    0,    0,  227,    0,    0,  227,  227,
  227,  227,  227,  227,  227,  227,  227,    0,    0,  227,
  227,    0,    0,  227,  227,  227,  227,  227,  227,  239,
  239,  227,  227,  227,    0,    0,    0,  227,    0,    0,
    0,  227,  227,  227,    0,    0,    0,  227,  227,  227,
  227,  227,    0,  227,    0,  227,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  227,    0,    0,    0,
    0,    0,  239,  239,  239,    0,    0,  227,  227,  227,
  227,    0,    0,  239,    0,    0,  239,  239,  239,  239,
  239,  239,  239,  239,  239,    0,    0,  239,  239,    0,
    0,  239,  239,  239,  239,  239,  239,  276,    0,  239,
  239,  239,    0,    0,    0,  239,    0,    0,    0,  239,
  239,  239,    0,    0,    0,  239,  239,  239,  239,  239,
    0,  239,    0,  239,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  239,    0,    0,    0,    0,    0,
  277,  278,  279,    0,    0,  239,  239,  239,  239,    0,
    0,  280,    0,    0,  281,  282,  283,  284,  285,  286,
  287,  288,  289,    0,    0,  290,  291,    0,    0,  292,
  293,  294,  295,  296,  297,  182,    0,  298,  299,  300,
    0,    0,    0,  301,    0,    0,    0,  302,  303,  304,
    0,    0,    0,  305,  306,  307,  308,  309,    0,  310,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,    0,    0,    0,    0,    0,  182,  182,
  182,    0,    0,  313,  314,  315,  316,    0,    0,  182,
    0,    0,  182,  182,  182,  182,  182,  182,  182,  182,
  182,    0,    0,  182,  182,    0,    0,  182,  182,  182,
  182,  182,  182,  183,    0,  182,  182,  182,    0,    0,
    0,  182,    0,    0,    0,  182,  182,  182,    0,    0,
    0,  182,  182,  182,  182,  182,    0,  182,    0,  182,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  182,    0,    0,    0,    0,    0,  183,  183,  183,    0,
    0,  182,  182,  182,  182,    0,    0,  183,    0,    0,
  183,  183,  183,  183,  183,  183,  183,  183,  183,    0,
    0,  183,  183,    0,    0,  183,  183,  183,  183,  183,
  183,  184,    0,  183,  183,  183,    0,    0,    0,  183,
    0,    0,    0,  183,  183,  183,    0,    0,    0,  183,
  183,  183,  183,  183,    0,  183,    0,  183,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  183,    0,
    0,    0,    0,    0,  184,  184,  184,    0,    0,  183,
  183,  183,  183,    0,    0,  184,    0,    0,  184,  184,
  184,  184,  184,  184,  184,  184,  184,    0,    0,  184,
  184,    0,    0,  184,  184,  184,  184,  184,  184,  185,
    0,  184,  184,  184,    0,    0,    0,  184,    0,    0,
    0,  184,  184,  184,    0,    0,    0,  184,  184,  184,
  184,  184,    0,  184,    0,  184,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  184,    0,    0,    0,
    0,    0,  185,  185,  185,    0,    0,  184,  184,  184,
  184,    0,    0,  185,    0,    0,  185,  185,  185,  185,
  185,  185,  185,  185,  185,    0,    0,  185,  185,    0,
    0,  185,  185,  185,  185,  185,  185,    0,    0,  185,
  185,  185,    0,    0,    0,  185,    0,    0,    0,  185,
  185,  185,    0,    0,    0,  185,  185,  185,  185,  185,
    0,  185,    0,  185,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  185,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  185,  185,  185,  185,  280,
    0,    0,  281,  282,  283,  284,  285,  286,  287,  288,
  289,    0,    0,  290,  291,    0,    0,  292,  293,  294,
  295,  296,  297,    0,    0,  298,  299,  300,    0,    0,
    0,  301,    0,    0,    0,  302,  303,  304,    0,    0,
    0,  305,  306,  307,  308,  309,    0,  310,    0,  311,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  312,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  313,  314,  315,  316,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  106,  272,   33,  123,   33,  123,   42,  446,   90,
  123,  159,  315,    6,    7,  209,   41,  165,  317,  123,
   40,   40,  123,  190,   31,  123,  325,   34,    0,   40,
   40,   40,  123,  123,   41,   40,  493,  494,   31,   44,
  339,  122,  123,  124,   44,   40,   53,  274,   41,   44,
   44,   42,   44,  158,  221,  136,    0,   61,  163,  164,
   67,  260,  307,   60,   71,  213,   73,  505,   44,   44,
  218,  219,  589,   80,   42,   82,   75,  276,   71,  256,
  307,  276,    0,  552,  553,  280,   93,   94,   44,   96,
   42,   98,   91,    0,   62,  200,  273,  554,  179,  180,
  248,   44,   93,  620,  266,  272,  105,  306,  297,  298,
  309,  310,  311,  312,  313,  314,  315,  198,  421,   62,
  125,  128,  284,  130,   61,  125,  123,  275,   61,  428,
  125,  125,   41,  125,  603,   44,   61,   93,  257,   91,
   79,  440,  441,  280,  249,   41,  323,  228,   44,  125,
  125,  150,   54,   41,   56,  162,   44,  274,  293,  294,
  327,  274,   61,  340,  341,   40,   41,  260,  606,   44,
  274,  352,  307,  274,   41,  356,  274,   44,  359,   81,
   42,   83,   44,  274,  274,  366,  257,  282,  127,  284,
  129,   42,   61,   44,  307,  290,  277,   41,  279,  637,
   44,  293,  294,  142,  503,  212,  307,  293,  294,  307,
   41,  297,  298,   44,  295,  296,  297,  224,   42,  300,
   44,  302,  303,  304,  305,  306,  307,  308,  309,  310,
  270,  271,  257,  258,  259,   42,  261,  262,  263,  302,
  265,   40,   41,   61,  274,   44,  274,  328,  187,  277,
  189,  276,  272,  272,  279,   40,  263,  264,    0,   41,
   42,  272,  272,  272,  289,  442,  123,    0,  303,  304,
  305,  306,  260,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  291,  292,  293,  294,   40,   55,
   56,  298,  299,  290,  301,  260,  268,  269,  274,  274,
  272,  273,   41,  275,  267,   44,  313,  314,  315,  378,
  379,  380,  381,  382,  383,  384,  385,  386,  387,  291,
  292,   42,  315,   44,  268,  269,    0,   41,  272,  273,
   44,  275,   41,   99,   41,   44,  308,   44,  267,    0,
  106,  303,  304,  305,  306,  352,   40,  291,  292,  356,
  268,  269,  359,   41,  272,  273,   44,  275,  123,  366,
  367,  268,  269,  123,  308,  272,  273,   40,  275,   41,
    0,  378,   44,  291,  292,  645,   42,  123,   44,  145,
  361,   41,   42,   40,  291,  292,   40,  468,  469,  470,
  308,   44,  399,   58,  475,   58,    0,  307,  293,  294,
   62,  308,  297,  298,  299,  300,  301,  346,   40,  416,
  123,  124,  351,  420,  421,  354,  355,   61,  357,  358,
   40,  360,  361,  362,  363,  364,  365,  420,  421,  368,
  369,  370,  371,   40,   41,  629,  360,   44,  204,   44,
  607,   44,  274,  260,  274,  368,  551,  368,  257,  360,
   44,   44,  600,  534,  360,   62,  360,   40,  274,  274,
  627,  257,  360,  257,  257,  472,  473,  360,  549,  476,
  477,  478,  479,  480,  481,  482,  483,  484,  645,  418,
   61,  401,  274,  257,  632,  257,   93,  273,  495,   44,
   44,   44,   44,   44,  368,  368,  368,  602,  368,   44,
  581,  582,  495,  368,  368,  368,   44,  368,  368,  448,
  266,   40,   44,  452,   44,  360,  455,   44,  125,   44,
   44,   44,   44,  462,  463,   44,  268,  269,   44,   44,
  272,  273,   44,  275,   44,  268,  269,   44,   44,  272,
  273,   44,  275,  550,  625,   44,  485,  486,  487,  291,
  292,   44,   40,  560,   91,   44,  257,   44,  291,  292,
   44,   44,   44,  261,  262,   44,  308,   44,  507,   44,
  509,  510,   44,  512,  513,  308,  515,  516,  517,  518,
  519,  520,   44,   44,  523,  524,  525,  526,  595,   40,
   44,   44,  531,   91,  268,  269,   44,  360,  272,  273,
   44,  275,  609,  360,   44,   44,  257,  268,  269,  257,
   44,  272,  273,   93,  275,   40,    0,  291,  292,   40,
   10,   57,  136,  562,  631,   18,  565,  135,  193,  568,
  291,  292,  176,  212,  308,   67,  575,  576,  268,  269,
   41,  162,  272,  273,  597,  275,  473,  308,  605,  319,
  319,  590,  591,  592,  343,  609,  327,  631,  597,  548,
   -1,  291,  292,   -1,  268,  269,  273,  274,  272,  273,
   -1,  275,   -1,   -1,   -1,   -1,   40,   -1,  308,   -1,
   -1,   -1,  380,  381,  382,  383,   -1,  291,  292,   -1,
  629,  389,  390,  391,  392,  393,  394,  395,  396,  397,
  398,   -1,   -1,   -1,  308,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,  274,  353,  354,  355,   -1,
   91,   -1,  359,   -1,   40,   -1,  363,  364,  365,   -1,
   -1,  368,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  388,  123,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  399,  400,  401,  402,   -1,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,  273,  274,  353,  354,  355,   -1,   -1,   -1,
  359,   -1,   40,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,  388,
  261,  262,  263,   -1,  265,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,   -1,  276,  327,   -1,  279,  330,
  331,  332,  333,  334,  335,  336,  337,  338,  289,   -1,
  341,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
   -1,   -1,  353,  354,  355,   -1,   -1,   -1,  359,  273,
  274,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,  264,  375,   40,  377,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  388,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  399,  400,
  401,  402,  316,  317,  318,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   60,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,  274,  353,
  354,  355,   -1,   91,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   40,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
  361,   -1,   -1,   -1,  388,  123,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,  273,  274,  353,  354,  355,
   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,
   -1,   40,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,  388,  261,  262,  263,   -1,  265,  316,  317,
  318,   -1,   -1,  399,  400,  401,  402,   -1,  276,  327,
   -1,  279,  330,  331,  332,  333,  334,  335,  336,  337,
  338,  289,   -1,  341,  342,   -1,   -1,  345,  346,  347,
  348,  349,  350,   -1,   -1,  353,  354,  355,   -1,   -1,
   -1,  359,   -1,   -1,   -1,  363,  364,  365,  273,  274,
   -1,  369,  370,  371,  372,  373,  264,  375,   40,  377,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  388,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  399,  400,  401,  402,   -1,   -1,   -1,   -1,   -1,
   -1,  316,  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  273,  274,  353,  354,
  355,   -1,   -1,   -1,  359,  343,   -1,   -1,  363,  364,
  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,
  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   42,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,  274,  353,  354,  355,   -1,
   -1,   -1,  359,   -1,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,   60,  399,  400,  401,  402,   42,   -1,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   91,   -1,  353,  354,  355,   40,   -1,   -1,
  359,  273,  274,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   60,  377,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,  268,  269,  388,
   -1,  272,  273,   -1,  275,   -1,   42,   -1,   -1,   -1,
  399,  400,  401,  402,  316,  317,  318,   -1,   91,   -1,
  291,  292,   -1,   -1,   60,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,  308,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,   -1,
  123,  353,  354,  355,   -1,   91,   -1,  359,   42,   -1,
   44,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   60,   42,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,  123,  257,  258,
  259,   -1,  261,  262,  263,   60,  265,  399,  400,  401,
  402,   -1,   -1,  272,  273,   -1,   -1,   91,   -1,   -1,
  279,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,
  289,   -1,   -1,   -1,   -1,   -1,   91,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,  123,
   -1,   -1,   -1,  272,  273,   91,   -1,   -1,   -1,   60,
  279,  257,  258,  259,   -1,  261,  262,  263,  123,  265,
  289,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,  123,   -1,   -1,
   91,   -1,   -1,  289,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,  289,   -1,   -1,   -1,
   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
  359,  125,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,
   -1,   -1,   -1,  279,   -1,  374,  375,   -1,   -1,  125,
   -1,   -1,   -1,  289,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  303,  304,  305,
  306,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   -1,   -1,  359,   -1,  272,  273,
   -1,   -1,  257,  258,  259,  279,  261,  262,  263,   -1,
  265,  374,  375,   -1,   -1,  289,   -1,  272,  273,   -1,
   -1,  257,  258,  259,  279,  261,  262,  263,   -1,  265,
   -1,   -1,   -1,  359,  289,   -1,  272,  273,  125,   -1,
   -1,   -1,   -1,  279,   -1,   -1,   -1,   -1,  374,  375,
   -1,   -1,   -1,  289,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,  303,  304,  305,
  306,  272,  273,   -1,   -1,   -1,  260,   -1,  279,   -1,
   -1,   -1,   -1,   -1,   -1,  359,   -1,   -1,  289,   -1,
   -1,   -1,  276,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  374,  375,   -1,   -1,  359,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  125,   -1,   -1,  374,
  375,   -1,  306,  359,   -1,  309,  310,  311,  312,  313,
  314,  315,   -1,   -1,   -1,   -1,   -1,   -1,  374,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,   -1,   -1,   -1,  359,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,  374,  375,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,   -1,  273,  353,  354,  355,
   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,  399,  400,  401,  402,   -1,   -1,   -1,
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
  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,   -1,
  359,  125,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   -1,
  341,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
   -1,  273,  353,  354,  355,   -1,   -1,   -1,  359,  125,
   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,
   -1,   -1,   -1,   -1,  316,  317,  318,   -1,  399,  400,
  401,  402,   -1,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,  273,
   -1,  353,  354,  355,   -1,   -1,   -1,  359,  125,   -1,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,  316,  317,  318,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,  125,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,   -1,  273,  353,  354,  355,
   -1,   -1,   -1,  359,  125,   -1,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,  399,  400,  401,  402,   -1,   -1,   -1,
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
  349,  350,  273,   -1,  353,  354,  355,   -1,   -1,   60,
  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   91,   -1,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,
  399,  400,  401,  402,   -1,   -1,  327,   -1,   60,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   -1,
  341,  342,  123,   -1,  345,  346,  347,  348,  349,  350,
   -1,  273,  353,  354,  355,   -1,   41,   -1,  359,   91,
   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,   60,  377,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   41,   -1,  388,   -1,   -1,
   -1,  123,   -1,   -1,  316,  317,  318,   -1,  399,  400,
  401,  402,   -1,   -1,   60,  327,   91,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,   -1,
   60,  353,  354,  355,   -1,   91,   -1,  359,  123,   41,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   60,   -1,
   -1,   91,   -1,   -1,   -1,   -1,  388,  123,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,  401,
  402,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   91,
   -1,   -1,  273,  123,   60,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   -1,   -1,  260,  257,  258,  259,  260,  261,  262,
  263,  123,  265,   -1,   -1,   91,   -1,   60,   -1,  276,
   -1,  274,  264,  276,   -1,   -1,  279,   -1,   -1,   -1,
   -1,  273,   -1,   60,   -1,   -1,  289,  328,  329,  281,
  282,  283,  284,  285,  286,  287,  288,  123,   91,  306,
   -1,   -1,  309,  310,  311,  312,  313,  314,  315,   -1,
  302,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  264,
  213,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
  123,   -1,   -1,  278,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   60,   -1,  123,   -1,  264,   -1,
   -1,  296,   -1,   -1,   -1,  248,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,  266,  264,   91,   -1,   60,   -1,   -1,
  296,   -1,  275,  273,   -1,   -1,   -1,   -1,  278,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,   -1,
   -1,   -1,  264,   60,   -1,   -1,  296,  123,   91,  125,
   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,  320,   -1,  322,
   -1,   -1,   -1,   -1,   91,   -1,   60,   -1,  264,  332,
  123,  334,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   -1,  123,   91,   -1,  295,
   -1,  264,   -1,   -1,  300,  301,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   91,   -1,   -1,   -1,  264,  281,  282,
  283,  284,  285,  286,  287,  288,  273,   60,   -1,  123,
  125,  125,   -1,  296,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,  123,   -1,   -1,  295,   -1,
   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   91,   -1,
   93,  424,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  435,   -1,  437,   -1,   -1,   60,  264,  265,
  443,   -1,   -1,   91,   -1,   -1,   -1,  273,  274,   -1,
  123,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   60,   -1,   -1,   91,   -1,
   -1,  264,  265,   -1,   -1,  123,   -1,   -1,   -1,   -1,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   91,  264,  265,   -1,
  123,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,  260,   -1,   -1,  123,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,  276,   -1,   -1,   -1,   -1,  264,  281,  282,  283,
  284,  285,  286,  287,  288,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  306,   -1,   -1,  309,  310,  311,  312,  313,  314,
  315,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  278,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  273,  274,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,  316,  317,  318,   -1,   -1,   -1,
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

#line 972 "Repil/IR/IR.jay"

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
  public const int ATTRIBUTE_GROUP_REF = 307;
  public const int ATTRIBUTES = 308;
  public const int NORECURSE = 309;
  public const int NOUNWIND = 310;
  public const int READNONE = 311;
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
