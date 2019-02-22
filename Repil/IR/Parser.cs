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
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr global_kind type constant ',' ALIGN INTEGER metadata_kvs",
//t    "global_variable : GLOBAL_SYMBOL '=' visibility function_addr global_kind type constant ',' ALIGN INTEGER",
//t    "global_variable : GLOBAL_SYMBOL '=' linkage function_addr global_kind type ',' ALIGN INTEGER",
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
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' ')' attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE linkage calling_convention return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "function_declaration : DECLARE NOALIAS return_type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "function_declaration : DECLARE return_type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs",
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
//t    "constant : '<' typed_constants '>'",
//t    "constant : '[' typed_constants ']'",
//t    "constant : '{' typed_values '}'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
//t    "typed_value : VOID",
//t    "typed_pointer_value : type pointer_value",
//t    "typed_values : typed_value",
//t    "typed_values : typed_values ',' typed_value",
//t    "typed_constant : type constant",
//t    "typed_constants : typed_constant",
//t    "typed_constants : typed_constants ',' typed_constant",
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
//t    "function_args : function_arg",
//t    "function_args : function_args ',' function_arg",
//t    "function_arg : type value",
//t    "function_arg : type parameter_attributes value",
//t    "function_arg : METADATA type LOCAL_SYMBOL",
//t    "function_arg : METADATA type constant",
//t    "function_arg : METADATA META_SYMBOL",
//t    "function_arg : METADATA META_SYMBOL '(' ')'",
//t    "function_arg : METADATA META_SYMBOL '(' metadata_value_args ')'",
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
//t    "instruction : CALL return_type function_pointer '(' function_args ')'",
//t    "instruction : CALL calling_convention return_type function_pointer '(' function_args ')'",
//t    "instruction : CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')'",
//t    "instruction : TAIL CALL calling_convention return_type function_pointer '(' function_args ')'",
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
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-5+yyTop], (Constant)yyVals[-4+yyTop], isPrivate: false, isExternal: false, isConstant: (bool)yyVals[-6+yyTop]);
    }
  break;
case 19:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (LType)yyVals[-4+yyTop], (Constant)yyVals[-3+yyTop], isPrivate: (bool)yyVals[-7+yyTop], isExternal: false, isConstant: (bool)yyVals[-5+yyTop]);
    }
  break;
case 20:
#line 128 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: false, isExternal: (bool)yyVals[-6+yyTop], isConstant: false);
    }
  break;
case 21:
#line 132 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 22:
#line 133 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 23:
#line 137 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 24:
#line 138 "Repil/IR/IR.jay"
  { yyVal = false; }
  break;
case 25:
#line 142 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 26:
  case_26();
  break;
case 27:
  case_27();
  break;
case 28:
#line 159 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 29:
#line 160 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 30:
#line 161 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 31:
#line 162 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 32:
#line 163 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 33:
#line 167 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 34:
#line 171 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 35:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 36:
#line 182 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 37:
#line 189 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 38:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 39:
#line 197 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 40:
#line 201 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 41:
#line 205 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 59:
#line 238 "Repil/IR/IR.jay"
  {
        yyVal = LiteralStructureType.Empty;
    }
  break;
case 60:
#line 242 "Repil/IR/IR.jay"
  {
        yyVal = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 61:
#line 246 "Repil/IR/IR.jay"
  {
        yyVal = new PackedStructureType ((List<LType>)yyVals[-2+yyTop]);
    }
  break;
case 62:
#line 253 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 63:
#line 257 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 65:
#line 262 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 67:
#line 267 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 68:
#line 268 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 69:
#line 269 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 70:
#line 270 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 71:
#line 271 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 72:
#line 272 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 73:
#line 273 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 74:
#line 274 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 75:
#line 278 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 76:
#line 282 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 77:
#line 286 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 78:
#line 290 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 79:
#line 294 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 80:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 81:
#line 305 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 82:
#line 309 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 84:
#line 317 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 85:
#line 324 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 86:
#line 328 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 87:
#line 332 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 88:
#line 336 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 89:
#line 340 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 90:
#line 344 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 91:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 92:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 93:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 94:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 95:
#line 367 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 96:
#line 371 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 97:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 98:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 99:
#line 389 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 100:
#line 393 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 397 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 102:
#line 401 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 104:
#line 409 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 105:
#line 413 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 106:
#line 414 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 107:
#line 415 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 108:
#line 416 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 114:
#line 434 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 115:
#line 435 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 116:
#line 436 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 117:
#line 437 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 118:
#line 438 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 119:
#line 439 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 120:
#line 440 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 121:
#line 441 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 122:
#line 442 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 123:
#line 443 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 124:
#line 447 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 125:
#line 448 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 126:
#line 449 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 127:
#line 450 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 128:
#line 451 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 129:
#line 452 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 130:
#line 453 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 131:
#line 454 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 132:
#line 455 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 133:
#line 456 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 134:
#line 457 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 135:
#line 458 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 136:
#line 459 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 137:
#line 460 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 138:
#line 461 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 139:
#line 462 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 141:
#line 467 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 142:
#line 468 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 143:
#line 472 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 144:
#line 476 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 145:
#line 480 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 147:
#line 488 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 148:
#line 489 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 149:
#line 490 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 150:
#line 491 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 151:
#line 492 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 152:
#line 493 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 153:
#line 494 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 154:
#line 495 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 155:
#line 496 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 156:
#line 500 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 504 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 508 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 522 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 526 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 162:
#line 533 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 540 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 544 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 551 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 562 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 573 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 577 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 584 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 588 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 175:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 614 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 618 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 625 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 629 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 181:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 637 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 184:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 659 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 187:
#line 663 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 188:
#line 667 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 189:
#line 671 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 190:
#line 675 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 191:
#line 679 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 192:
#line 683 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 197:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 199:
#line 710 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 200:
#line 717 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 721 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 728 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 749 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 753 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 757 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 761 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 772 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 776 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 215:
#line 780 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 216:
#line 784 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 218:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 219:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 220:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 221:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 222:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 223:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 236:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 237:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 238:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 251:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 928 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 932 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 936 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 940 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 944 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 948 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 952 "Repil/IR/IR.jay"
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

void case_26()
#line 147 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_27()
#line 152 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    6,    6,    6,
   11,   11,   16,   16,   15,    9,    9,   17,   17,   17,
   17,   17,   17,   17,   14,   14,    8,    8,    8,    8,
    8,   19,   19,   19,    7,    7,   21,   21,   21,   21,
   21,   21,   21,   21,   21,   21,   21,   21,    3,    3,
    3,   22,   22,   23,   23,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   24,   24,   25,   25,    4,    4,    4,    4,    4,    4,
    4,    4,    4,    5,    5,    5,   26,   26,   30,   30,
   30,   30,   31,   31,   32,   32,   32,   32,   10,   10,
   27,   27,   33,   34,   34,   34,   34,   34,   34,   34,
   34,   34,   34,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   35,   36,
   36,   36,   36,   36,   36,   38,   13,   13,   13,   13,
   13,   13,   13,   13,   13,   13,   13,   13,   41,   20,
   20,   42,   40,   40,   43,   39,   39,   44,   37,   37,
   28,   28,   45,   45,   45,   45,   46,   46,   48,   48,
   48,   48,   50,   51,   51,   52,   52,   52,   52,   52,
   52,   52,   18,   18,   53,   53,   54,   54,   55,   56,
   56,   57,   58,   58,   59,   59,   29,   47,   47,   47,
   47,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,   10,   10,    9,
    1,    1,    1,    1,    1,    1,    3,    3,    3,    3,
    3,    3,    6,    5,    2,    3,    1,    2,    3,    3,
    3,    1,    1,    1,    1,    2,    1,    1,    1,    1,
    1,    1,    1,    3,    1,    1,    1,    4,    2,    3,
    5,    1,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    3,    4,    2,    1,    5,    5,
    1,    3,    1,    1,   11,   11,   11,   10,   12,   12,
   13,   13,   14,    7,    8,    8,    1,    3,    1,    2,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    6,    9,    6,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    3,    3,    3,    2,    2,
    1,    2,    1,    3,    2,    1,    3,    1,    1,    3,
    1,    2,    2,    3,    1,    2,    1,    2,    1,    2,
    3,    4,    1,    1,    3,    2,    3,    3,    3,    2,
    4,    5,    1,    3,    1,    1,    1,    3,    5,    1,
    2,    3,    1,    2,    1,    1,    1,    2,    7,    2,
    7,    5,    6,    5,    5,    4,    6,    7,    7,    8,
    7,    8,    4,    5,    6,    5,    5,    4,    4,    5,
    6,    7,    6,    6,    7,    5,    6,    5,    5,    6,
    3,    4,    5,    7,    4,    5,    6,    6,    4,    7,
    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   65,   78,   67,   68,   69,   70,   71,   72,   73,   74,
    0,   24,   23,    0,    0,    0,   66,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  109,  110,   25,
    0,    0,    0,    0,    0,    0,    0,    0,   59,    0,
    0,    0,    0,    0,    0,   77,  207,    0,    0,    0,
    0,    0,    0,    5,    6,   21,   22,    0,    0,    0,
    8,    0,    7,    0,    0,    0,    0,    0,   60,    0,
    0,    0,    0,    0,    0,   84,   75,    0,    0,   81,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   26,
    0,    0,    0,   44,   43,   13,    0,    0,   37,   42,
    0,    0,    0,    0,    0,    0,  101,  102,    0,    0,
    0,   97,   76,    0,    0,    0,    0,    0,   57,   47,
   48,   49,   50,   51,   52,   53,    0,   45,  149,  150,
  148,  151,  152,  153,  147,  155,  154,    0,    0,    0,
    0,    0,    0,    0,    0,   15,    0,    0,    0,   38,
   14,    0,  142,  141,    0,    0,    0,  140,  160,    0,
   79,   61,   80,    0,  113,    0,    0,  111,  105,  106,
  108,  107,    0,  103,    0,    0,   82,    0,    0,    0,
    0,   12,   46,  163,    0,    0,    0,  166,    0,    0,
    0,    0,   30,    0,   28,   31,   32,   27,   17,   16,
   41,   40,   39,    0,    0,    0,    0,    0,    0,    0,
    0,  112,  104,    0,    0,   98,    0,    0,    0,   54,
  196,  195,    0,  193,  158,    0,  165,    0,  156,  157,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   35,    0,    0,    0,    0,    0,    0,   58,    0,  164,
  167,    0,    0,   20,   34,    0,    0,    0,    0,    0,
    0,    0,   36,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  171,    0,    0,  177,    0,
    0,    0,    0,  194,    0,   19,   33,    0,    0,    0,
    0,    0,    0,    0,    0,  210,    0,    0,  208,    0,
  205,  206,    0,    0,  203,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  114,  115,  116,  117,  118,  119,  120,  121,  122,  123,
    0,  124,  125,  136,  137,  138,  139,  127,  129,  130,
  131,  132,  128,  126,  134,  135,  133,    0,    0,    0,
    0,    0,    0,   88,  172,    0,  178,    0,    0,    0,
    0,    0,    0,  143,  145,    0,    0,    0,    0,   87,
    0,  159,    0,    0,    0,    0,  204,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  197,    0,  183,    0,
    0,    0,    0,    0,   85,    0,   86,    0,   90,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  223,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   89,  168,    0,  169,   91,   92,    0,    0,
    0,  212,    0,  224,  251,    0,  230,  239,    0,  227,
  254,  243,  226,  256,  246,    0,    0,  236,  215,  238,
  257,    0,    0,  214,  146,  162,    0,    0,    0,    0,
    0,    0,    0,  198,    0,    0,    0,    0,  184,    0,
    0,    0,  144,    0,   93,    0,    0,    0,  200,  213,
  252,  240,  247,  237,  234,  248,    0,    0,    0,    0,
  233,  225,    0,    0,    0,    0,    0,  186,    0,    0,
    0,    0,    0,  170,  209,    0,  211,  201,  235,  250,
    0,  199,  244,    0,  188,  189,  187,    0,  185,  218,
    0,    0,  202,  191,    0,    0,  222,  192,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  147,  117,  109,   51,
   78,  118,  178,  231,   52,   39,  110,  243,  119,  554,
  148,   61,   62,   99,  100,  131,  187,  325,   68,  132,
  193,  194,  188,  401,  418,  489,  555,  586,  207,  205,
  349,  531,  208,  556,  326,  327,  328,  329,  330,  490,
  598,  599,  244,  486,  487,  608,  609,  354,  355,
  };
  protected static readonly short [] yySindex = {         -134,
   -2, -144,   40,   55,   76, 2754, 2829, -271,    0, -134,
    0,    0,    0,    0,  -69,  136,  140,  107,  -63,  -27,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3155,    0,    0, 3044, -105,   45,    0,  190,  -32,  -26,
 3155,  -24,  243,    0,    0,   81,   93,    0,    0,    0,
 -122,  -36,  -36,  -31,  269,  -23,  209,  -18,    0,  190,
   -5,  317,   99, 3155,  104,    0,    0, 3155,  333, 2588,
  -17,  340,  259,    0,    0,    0,    0, 3155, -122, -122,
    0,  274,    0, -185,  364,  286, 2920,  375,    0, 3155,
 3155,    8, 3155,  -16, 2542,    0,    0,  190,  106,    0,
  379, 2787,  589, 2743, 3155, 3155,  362,  376,  115,    0,
 -185, 2955,    0,    0,    0,    0,   44, 1384,    0,    0,
 2787,  190,  -21,  330,  -15,  399,    0,    0, -189,  -29,
  152,    0,    0, 2803, 2787,  162,  380,  400,    0,    0,
    0,    0,    0,    0,    0,    0, 1421,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3171, 3155, 3155,
  404, 2743,  120, 2871,  169,    0, -185,  177,   69,    0,
    0, 3003,    0,    0,   83,  410,  411,    0,    0,  182,
    0,    0,    0, 2787,    0,  145, -240,    0,    0,    0,
    0,    0,  -25,    0, -189, 2787,    0,  183, -189,  193,
 2894,    0,    0,    0,   11, 2743,   24,    0,   13,   95,
  412,   97,    0,  418,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  419, 3171, 3171,  -36,  295, -240,  203,
  -90,    0,    0,  145, -240,    0,  145,  145,  145,    0,
    0,    0,  305,    0,    0, 3171,    0, 3155,    0,    0,
  221,  122,  228,   49, 3155,  121,  123,  145,  -36,  -85,
    0,  214, 3679, -103,  -76,  145,  145,    0, 2894,    0,
    0,  216,  236,    0,    0,  306,  124, 3155, 3155,  -97,
  145, 3679,    0,  434, 3171, -141, 3171,  358, 3155,  358,
 3155,  358, 3155, 3155, 3155, 3155, 3155,  358,  -20, 3155,
 3155, 3155, 3171, 3171, 3171, 3155, 3155, 3171,  -48, 3171,
 3171, 3171, 3171, 3171, 3171, 3171, 3171, 3171,  320, 1235,
 3155, 3155,  800,   96, 1452,    0, 3679,  216,    0,  216,
 3679,  -75, 3679,    0,  222,    0,    0, 3171,  279,  285,
 3679,  -74,  -93, 1539, 4067,    0,  225, 1488,    0,  457,
    0,    0, 1384,  358,    0, 1384, 1384,  358, 1384, 1384,
  358, 1384, 1384, 1384, 1384, 1384, 1384,  358, 3155, 1384,
 1384, 1384, 1384,  461,  462,  465,  163,  186,  468, 3155,
  199,  158,  166,  168,  170,  174,  176,  180,  185,  188,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3155,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3155,    2, 1384,
   72, 3155,  800,    0,    0,  216,    0,  222,  222, 1617,
 3679, 1695,  476,    0,    0, 1774, 3679, 3679,  -72,    0,
  216,    0,  486,  265,  493, 1384,    0,  506,  510, 1384,
  513,  514, 1384,  515,  516,  517,  518,  519,  520, 1384,
 1384,  523,  529,  530,  531, 3171, 3171, 3171,  219, 3155,
 3155,  298, 3171, 3155, 3155, 3155, 3155, 3155, 3155, 3155,
 3155, 3155, 1384, 1384, 1488,  539,    0,  541,    0,  546,
   72,   72, 3155,  222,    0, 1852,    0, 3171,    0, 1930,
 2009, 3679,  222,  265,  496, 1488,  549, 1488, 1488,  552,
 1488, 1488,  555, 1488, 1488, 1488, 1488, 1488, 1488,  556,
  559, 1488, 1488, 1488, 1488,    0,  563,  565,  353, 1384,
  569,  570, 3171,  571,  190,  190,  190,  190,  190,  190,
  190,  190,  190,  572,  573,  574,  528, 3171, 3077,  580,
  583,   72,    0,    0,  324,    0,    0,    0, 2087,  581,
 3155,    0, 1488,    0,    0, 1488,    0,    0, 1488,    0,
    0,    0,    0,    0,    0, 1488, 1488,    0,    0,    0,
    0, 3171, 3171,    0,    0,    0,  266,  268,  585, 3171,
 1488, 1488, 1488,    0,  586, 3104, 1334,  326,    0, 3077,
 3077,  592,    0, 3171,    0,  265,  594, 3130,    0,    0,
    0,    0,    0,    0,    0,    0,  390,  393, 3171,  604,
    0,    0,  540, 3171,  609, 2698, 1417,    0,  145, 3077,
  331,  352, 3077,    0,    0,  265,    0,    0,    0,    0,
  604,    0,    0, 2660,    0,    0,    0,  145,    0,    0,
  145,  384,    0,    0,  385,  145,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  651,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  319,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   26,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  105,    0,    0,
    0,    0,    0,  612,    0,    0,    0,    0,    0,    0,
    0,    0,  422,    0,    0,    0,    0,  612,    0,    0,
    0,   36,  612,    0,  612,    0,    0,    0,    0,  142,
    0,    0,    0,    0,    0,    0, 2589, 2631,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  612,  612,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  386,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  612,    0,    0,    0,    0,
    0,    0,    0,  391,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  130,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  211,  260,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  612,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 2165,    0, 3757,
    0,    0,    0,    0,  297,    0,    0,    0,  612,  612,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  612,    0,    0,  612,  612,    0,  612,  612,
    0,  612,  612,  612,  612,  612,  612,    0,    0,  612,
  612,  612,  612,    0,    0,    0,  612,  612,    0,    0,
  612,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  612,  612,
    0,    0,    0,    0,    0, 2244,    0, 2322, 3835,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3913,    0,    0,    0,    0,  612,    0,    0,    0,  612,
    0,    0,  612,    0,    0,    0,    0,    0,    0,  612,
  612,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  612,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  612,  612,    0, 3133,    0,    0,    0,    0,
    0,    0,    0, 2400,    0,    0,    0,    0,    0,    0,
    0,    0, 3991,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  612,
    0,    0,    0,    0,  509,  597,  675,  762,  849,  936,
 1014, 1101, 1188,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  612,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3211,
    0,    0,    0,    0,  392,  612,    0,    0, 3289,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3367,    0,    0,    0,    0,    0,    0, 3445,    0,    0,
 3523,    0,    0,    0,    0, 3601,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  643,  600,    0,    0,    0,    0,  560,  547,   -7,
  334,   -6,  -99, 1340,    0,  646,  494, -250,    0,   73,
  524,  613,    1,    0,  545,   -4, -184,   23, -304,  484,
   85, -190, -156,    0,    0,  151, -496,    0,  525,    0,
 -427,  212, -239,   80, -288,    0,  361,  363,  344, -431,
 -519,   61,  423,    0,  161,    0,  101, -125, -139,
  };
  protected static readonly short [] yyTable = {            38,
   38,  229,  233,  276,  161,   57,   40,   42,  271,   86,
  235,   35,   66,   70,  239,   70,  505,   64,  422,  331,
   66,   70,   70,   70,   38,  341,   66,   60,   82,  438,
  232,   58,  263,  230,   38,   43,  425,  282,   90,   35,
  181,   71,   36,   66,   79,   80,  333,  431,  437,  264,
  502,   90,  266,  267,  246,  425,  248,   60,   15,  550,
  551,   38,  211,   98,  216,   64,  185,  248,   94,   62,
   36,  104,  232,  280,   34,   64,  560,  183,  232,   63,
  631,  632,  232,  122,  123,  249,  125,  172,  130,  275,
  107,   34,  485,  620,  108,  130,  343,  136,  162,  163,
   18,  242,   34,   48,   49,  250,  247,  232,  159,  232,
  232,   70,  172,  652,  130,   19,  180,  185,  493,   89,
  602,  186,  641,  232,  347,   16,   17,   98,  130,   94,
  198,  159,  124,    1,    2,  245,   20,    3,    4,  160,
    5,  425,  348,  425,   64,   83,  133,  425,   83,  134,
   62,   63,  206,  206,  242,  166,    6,    7,  167,  120,
   63,   66,  160,  212,  358,   66,  361,  338,  171,  242,
  230,  158,  368,    8,   76,   77,  230,  130,  635,  228,
  230,   64,   99,  262,  120,   99,  232,  234,  262,  130,
   45,  238,  195,  220,  158,  196,   46,  262,  262,  262,
   47,  262,  199,  185,   66,  196,  469,  425,  653,  185,
   95,  425,  425,  185,  447,   21,   54,  219,  447,  258,
  167,  447,  227,  237,   22,  196,  196,   66,  447,  470,
  204,   66,   23,   24,   25,   26,   27,   28,   29,   30,
   66,  206,  473,   21,  223,   69,   55,   72,  277,   56,
   85,  281,   22,   88,  101,  126,   48,   49,   81,   96,
   23,   24,   25,   26,   27,   28,   29,   30,  179,   67,
  425,  339,  340,  189,  190,  191,  192,  189,  190,  191,
  192,  353,  356,  357,  359,  360,  362,  363,  364,  365,
  366,  367,  370,  371,  372,  373,   18,  256,  257,  377,
  378,   65,  381,   73,  344,  149,  150,  151,   84,  152,
  153,  154,  380,  155,  419,  420,   38,  170,  270,  434,
   66,  607,  369,  421,  241,  435,   66,  156,  149,  150,
  151,   87,  152,  153,  154,  259,  155,  157,  196,   66,
   74,  533,  170,  173,  174,  268,  337,  446,  269,  269,
  156,  450,   75,  430,  453,  432,   70,  346,   64,  350,
  157,  460,  461,  436,  603,   91,  629,  604,  607,  630,
   93,  650,   95,  472,  630,  374,  375,  376,   64,  102,
  379,  103,  382,  383,  384,  385,  386,  387,  388,  389,
  390,  182,  651,  655,  483,  630,   64,   94,   94,   48,
   49,   94,   94,  111,   94,   50,   32,   33,  112,   64,
  433,  484,  105,  106,  121,   38,   38,   35,  135,  164,
   94,   94,  491,  492,  657,  658,  100,  630,  269,  100,
  175,   29,  190,  165,   29,  190,  233,   94,  184,  201,
  200,   64,  217,  224,  648,  176,  177,  210,   36,  225,
  226,  185,  240,  496,  251,  252,  253,  254,  255,  500,
  501,   65,  161,  530,  530,  161,  656,  535,  536,  537,
  538,  539,  540,  541,  542,  543,  261,  272,   95,   95,
   34,  273,   95,   95,  274,   95,   38,  283,  278,  230,
  279,  232,  336,  552,  345,  262,  423,  442,  443,  232,
  444,   95,   95,  445,  466,  467,  448,  449,  468,  451,
  452,  471,  454,  455,  456,  457,  458,  459,   95,  498,
  462,  463,  464,  465,  559,  474,  646,   96,   96,  504,
  347,   96,   96,  475,   96,  476,  506,  477,  526,  527,
  528,  478,  597,  479,  242,  534,  161,  480,   64,  508,
   96,   96,  481,  509,  206,  482,  511,  512,  514,  515,
  516,  517,  518,  519,   18,   18,  522,   96,   18,   18,
  488,   18,  523,  524,  525,   64,   64,   64,  529,   64,
   64,   64,  547,   64,  548,  549,  561,   18,   18,  626,
   64,   64,  563,  597,  597,  566,  507,   64,  569,  576,
  510,  206,  577,  513,   18,  589,  582,   64,  583,  584,
  520,  521,  587,  588,  590,  591,  592,  593,  485,  600,
  595,   21,  601,  597,  606,  617,  597,  618,  619,  624,
   22,  633,  642,  544,  545,  546,   64,  636,   23,   24,
   25,   26,   27,   28,   29,   30,  639,  604,  644,  640,
    1,   64,   44,   83,  615,  616,  562,  168,  564,  565,
  218,  567,  568,   53,  570,  571,  572,  573,  574,  575,
  203,  169,  578,  579,  580,  581,   92,   64,  197,  236,
  585,  627,  532,  634,  209,  351,  352,  426,  441,  427,
  649,  334,   64,   64,  161,  161,  643,  391,  392,  393,
  394,  395,  396,  397,  398,  399,  400,  594,  638,    0,
    0,    0,    0,  610,   64,    0,  611,    0,    0,  612,
    0,    0,    0,    0,    0,    0,  613,  614,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  161,  161,  161,
    0,  621,  622,  623,    0,    0,    0,  628,  161,    0,
    0,  161,  161,  161,  161,  161,  161,  161,  161,  161,
    0,    0,  161,  161,    0,    0,  161,  161,  161,  161,
  161,  161,    0,    0,  161,  161,  161,  647,    0,    0,
  161,  253,  253,    0,  161,  161,  161,    0,    0,  161,
  161,  161,  161,  161,  161,    0,  161,    0,  161,    0,
    0,   64,    0,    0,    0,    0,    0,    0,    0,  161,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  161,  161,  161,  161,  253,  253,  253,    0,    0,    0,
    0,    0,    0,    0,    0,  253,    0,    0,  253,  253,
  253,  253,  253,  253,  253,  253,  253,    0,  137,  253,
  253,    0,    0,  253,  253,  253,  253,  253,  253,   35,
    0,  253,  253,  253,  138,    0,    0,  253,    0,  258,
  258,  253,  253,  253,    0,    0,    0,  253,  253,  253,
  253,  253,    0,  253,    0,  253,    0,    0,   64,    0,
   36,    0,    0,    0,  139,    0,  253,  140,  141,  142,
  143,  144,  145,  146,    0,    0,    0,  253,  253,  253,
  253,    0,  258,  258,  258,    0,    0,    0,    0,    0,
    0,    0,   34,  258,    0,    0,  258,  258,  258,  258,
  258,  258,  258,  258,  258,    0,    0,  258,  258,    0,
    0,  258,  258,  258,  258,  258,  258,  245,  245,  258,
  258,  258,    0,    0,    0,  258,    0,    0,    0,  258,
  258,  258,    0,    0,    0,  258,  258,  258,  258,  258,
    0,  258,    0,  258,    0,   64,    0,    0,    0,    0,
    0,    0,    0,    0,  258,    0,    0,    0,    0,    0,
  245,  245,  245,    0,    0,  258,  258,  258,  258,    0,
    0,  245,    0,    0,  245,  245,  245,  245,  245,  245,
  245,  245,  245,    0,    0,  245,  245,    0,    0,  245,
  245,  245,  245,  245,  245,    0,    0,  245,  245,  245,
    0,    0,    0,  245,  228,  228,    0,  245,  245,  245,
    0,    0,    0,  245,  245,  245,  245,  245,    0,  245,
    0,  245,    0,   64,    0,    0,    0,    0,    0,    0,
    0,    0,  245,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,  245,  245,  245,  245,  228,  228,  228,
   23,   24,   25,   26,   27,   28,   29,   30,  228,    0,
    0,  228,  228,  228,  228,  228,  228,  228,  228,  228,
    0,   67,  228,  228,    0,    0,  228,  228,  228,  228,
  228,  228,    0,    0,  228,  228,  228,    0,    0,    0,
  228,  229,  229,    0,  228,  228,  228,    0,    0,    0,
  228,  228,  228,  228,  228,    0,  228,    0,  228,    0,
   64,    0,    0,    0,    0,    0,    0,    0,    0,  228,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  228,  228,  228,  228,  229,  229,  229,    0,    0,    0,
    0,    0,    0,    0,    0,  229,    0,    0,  229,  229,
  229,  229,  229,  229,  229,  229,  229,    0,    0,  229,
  229,    0,    0,  229,  229,  229,  229,  229,  229,    0,
    0,  229,  229,  229,    0,    0,    0,  229,  255,  255,
    0,  229,  229,  229,    0,    0,    0,  229,  229,  229,
  229,  229,    0,  229,    0,  229,    0,   64,    0,    0,
    0,    0,    0,    0,    0,    0,  229,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  229,  229,  229,
  229,  255,  255,  255,    0,    0,    0,    0,    0,    0,
    0,    0,  255,    0,    0,  255,  255,  255,  255,  255,
  255,  255,  255,  255,    0,    0,  255,  255,    0,    0,
  255,  255,  255,  255,  255,  255,  249,  249,  255,  255,
  255,    0,    0,    0,  255,    0,    0,    0,  255,  255,
  255,    0,    0,    0,  255,  255,  255,  255,  255,    0,
  255,    0,  255,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  255,    0,    0,    0,    0,    0,  249,
  249,  249,    0,    0,  255,  255,  255,  255,    0,    0,
  249,    0,    0,  249,  249,  249,  249,  249,  249,  249,
  249,  249,    0,    0,  249,  249,    0,    0,  249,  249,
  249,  249,  249,  249,    0,    0,  249,  249,  249,    0,
    0,    0,  249,  242,  242,   66,  249,  249,  249,    0,
    0,    0,  249,  249,  249,  249,  249,    0,  249,    0,
  249,    0,    0,  159,    0,    0,    0,    0,    0,    0,
    0,  249,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  249,  249,  249,  249,  242,  242,  242,    0,
    0,    0,    0,    0,  160,   66,    0,  242,    0,    0,
  242,  242,  242,  242,  242,  242,  242,  242,  242,    0,
    0,  242,  242,  159,    0,  242,  242,  242,  242,  242,
  242,    0,    0,  242,  242,  242,  158,    0,    0,  242,
  216,  216,    0,  242,  242,  242,    0,    0,    0,  242,
  242,  242,  242,  242,  160,  242,  159,  242,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  242,    0,
    0,    0,    0,    0,    0,  402,  403,    0,    0,  242,
  242,  242,  242,  216,  216,  216,  158,  160,    0,    0,
    0,    0,    0,    0,  216,    0,    0,  216,  216,  216,
  216,  216,  216,  216,  216,  216,    0,    0,  216,  216,
    0,    0,  216,  216,  216,  216,  216,  216,    0,  158,
  216,  216,  216,    0,    0,  202,  216,  159,    0,    0,
  216,  216,  216,    0,    0,    0,  216,  216,  216,  216,
  216,    0,  216,    0,  216,    0,    0,    0,  260,    0,
    0,    0,    0,    0,  265,  216,  424,    0,  160,    0,
    0,    0,    0,    0,    0,    0,  216,  216,  216,  216,
  149,  150,  151,    0,  152,  153,  154,    0,  155,    0,
    0,    0,    0,  332,    0,  173,  174,    0,    0,    0,
  158,  335,  156,    0,  404,  405,  406,  407,    0,  342,
    0,    0,  157,  408,  409,  410,  411,  412,  413,  414,
  415,  416,  417,    0,    0,    0,  189,  190,  191,  192,
  149,  150,  151,    0,  152,  153,  154,    0,  155,    0,
    0,    0,    0,    0,    0,  173,  174,    0,    0,    0,
    0,    0,  156,  440,    0,    0,    0,  428,    0,  429,
    0,    0,  157,  149,  150,  151,    0,  152,  153,  154,
  137,  155,  439,    0,    0,    0,    0,    0,  173,  174,
    0,    0,  175,    0,    0,  156,  138,    0,    0,    0,
    0,    0,    0,    0,    0,  157,    0,  176,  177,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  189,
  190,  191,  192,    0,  284,    0,  139,    0,    0,  140,
  141,  142,  143,  144,  145,  146,    0,    0,    0,    0,
    0,  495,  175,    0,  149,  150,  151,    0,  152,  153,
  154,    0,  155,    0,    0,    0,    0,  176,  177,  173,
  174,    0,    0,    0,    0,  494,  156,  285,  286,  287,
    0,    0,    0,    0,    0,  175,  157,    0,  288,    0,
  503,  289,  290,  291,  292,  293,  294,  295,  296,  297,
  176,  177,  298,  299,    0,    0,  300,  301,  302,  303,
  304,  305,    0,    0,  306,  307,  308,    0,    0,    0,
  309,  284,    0,    0,  310,  311,  312,    0,    0,  497,
  313,  314,  315,  316,  317,    0,  318,    0,  319,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  320,
    0,    0,    0,    0,    0,    0,  175,    0,    0,    0,
  321,  322,  323,  324,  285,  286,  287,    0,    0,    0,
    0,  176,  177,    0,    0,  288,    0,    0,  289,  290,
  291,  292,  293,  294,  295,  296,  297,    0,    0,  298,
  299,    0,    0,  300,  301,  302,  303,  304,  305,  284,
    0,  306,  307,  308,    0,    0,    0,  309,  499,    0,
    0,  310,  311,  312,    0,    0,    0,  313,  314,  315,
  316,  317,    0,  318,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,    0,    0,    0,
    0,    0,  285,  286,  287,    0,    0,  321,  322,  323,
  324,    0,    0,  288,    0,    0,  289,  290,  291,  292,
  293,  294,  295,  296,  297,    0,    0,  298,  299,    0,
    0,  300,  301,  302,  303,  304,  305,  284,    0,  306,
  307,  308,    0,    0,    0,  309,  553,    0,    0,  310,
  311,  312,    0,    0,    0,  313,  314,  315,  316,  317,
    0,  318,    0,  319,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  320,    0,    0,    0,    0,    0,
  285,  286,  287,    0,    0,  321,  322,  323,  324,    0,
    0,  288,    0,    0,  289,  290,  291,  292,  293,  294,
  295,  296,  297,    0,    0,  298,  299,    0,    0,  300,
  301,  302,  303,  304,  305,    0,  284,  306,  307,  308,
    0,    0,    0,  309,  557,    0,    0,  310,  311,  312,
    0,    0,    0,  313,  314,  315,  316,  317,    0,  318,
    0,  319,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  320,    0,    0,    0,    0,    0,    0,  285,
  286,  287,    0,  321,  322,  323,  324,    0,    0,    0,
  288,    0,    0,  289,  290,  291,  292,  293,  294,  295,
  296,  297,    0,    0,  298,  299,    0,    0,  300,  301,
  302,  303,  304,  305,  284,    0,  306,  307,  308,    0,
    0,    0,  309,  558,    0,    0,  310,  311,  312,    0,
    0,    0,  313,  314,  315,  316,  317,    0,  318,    0,
  319,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  320,    0,    0,    0,    0,    0,  285,  286,  287,
    0,    0,  321,  322,  323,  324,    0,    0,  288,    0,
    0,  289,  290,  291,  292,  293,  294,  295,  296,  297,
    0,    0,  298,  299,    0,    0,  300,  301,  302,  303,
  304,  305,  284,    0,  306,  307,  308,    0,    0,    0,
  309,  605,    0,    0,  310,  311,  312,    0,    0,    0,
  313,  314,  315,  316,  317,    0,  318,    0,  319,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  320,
    0,    0,    0,    0,    0,  285,  286,  287,    0,    0,
  321,  322,  323,  324,    0,    0,  288,    0,    0,  289,
  290,  291,  292,  293,  294,  295,  296,  297,    0,    0,
  298,  299,    0,    0,  300,  301,  302,  303,  304,  305,
    0,  284,  306,  307,  308,    0,    0,    0,  309,  175,
    0,    0,  310,  311,  312,    0,    0,    0,  313,  314,
  315,  316,  317,    0,  318,    0,  319,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  320,    0,    0,
    0,    0,    0,    0,  285,  286,  287,    0,  321,  322,
  323,  324,    0,    0,    0,  288,    0,    0,  289,  290,
  291,  292,  293,  294,  295,  296,  297,    0,    0,  298,
  299,    0,    0,  300,  301,  302,  303,  304,  305,  284,
    0,  306,  307,  308,    0,    0,    0,  309,  173,    0,
    0,  310,  311,  312,    0,    0,    0,  313,  314,  315,
  316,  317,    0,  318,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,    0,    0,    0,
    0,    0,  285,  286,  287,    0,    0,  321,  322,  323,
  324,    0,    0,  288,    0,    0,  289,  290,  291,  292,
  293,  294,  295,  296,  297,    0,    0,  298,  299,    0,
    0,  300,  301,  302,  303,  304,  305,  175,    0,  306,
  307,  308,    0,    0,    0,  309,  176,    0,    0,  310,
  311,  312,    0,    0,    0,  313,  314,  315,  316,  317,
    0,  318,    0,  319,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  320,    0,    0,    0,    0,    0,
  175,  175,  175,    0,    0,  321,  322,  323,  324,    0,
    0,  175,    0,    0,  175,  175,  175,  175,  175,  175,
  175,  175,  175,    0,    0,  175,  175,    0,    0,  175,
  175,  175,  175,  175,  175,    0,  173,  175,  175,  175,
    0,    0,    0,  175,  174,    0,    0,  175,  175,  175,
    0,    0,    0,  175,  175,  175,  175,  175,    0,  175,
    0,  175,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  175,    0,    0,    0,    0,    0,    0,  173,
  173,  173,    0,  175,  175,  175,  175,    0,    0,    0,
  173,    0,    0,  173,  173,  173,  173,  173,  173,  173,
  173,  173,  129,    0,  173,  173,    0,    0,  173,  173,
  173,  173,  173,  173,  176,    0,  173,  173,  173,    0,
    0,   35,  173,    0,    0,    0,  173,  173,  173,    0,
    0,    0,  173,  173,  173,  173,  173,    0,  173,    0,
  173,    0,    0,    0,    0,    0,    0,    0,   97,    0,
    0,  173,   36,    0,    0,    0,    0,  176,  176,  176,
    0,    0,  173,  173,  173,  173,    0,   35,  176,    0,
    0,  176,  176,  176,  176,  176,  176,  176,  176,  176,
    0,    0,  176,  176,   34,    0,  176,  176,  176,  176,
  176,  176,  174,    0,  176,  176,  176,    0,   36,    0,
  176,    0,    0,    0,  176,  176,  176,    0,    0,    0,
  176,  176,  176,  176,  176,    0,  176,    0,  176,    0,
  654,    0,    0,    0,    0,    0,    0,    0,    0,  176,
   34,    0,    0,   55,    0,  174,  174,  174,    0,  159,
  176,  176,  176,  176,    0,    0,  174,    0,    0,  174,
  174,  174,  174,  174,  174,  174,  174,  174,    0,   66,
  174,  174,    0,    0,  174,  174,  174,  174,  174,  174,
  160,    0,  174,  174,  174,   56,    0,  159,  174,    0,
    0,    0,  174,  174,  174,    0,    0,    0,  174,  174,
  174,  174,  174,    0,  174,    0,  174,    0,    0,    0,
    0,    0,  158,    0,   66,    0,    0,  174,  160,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  174,  174,
  174,  174,  159,    0,    0,   21,    0,    0,    0,    0,
    0,    0,    0,   35,   22,    0,    0,    0,    0,  127,
  158,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,    0,  160,    0,    0,    0,  128,    0,    0,
    0,    0,    0,    0,   36,    0,   35,    0,   55,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,   35,    0,   55,  158,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,   34,   36,    0,    0,
    0,    0,    0,   96,    0,    0,    0,    0,   35,    0,
   56,    0,    0,   36,   55,    0,    0,   55,   55,   55,
   55,   55,   55,   55,    0,    0,   56,    0,    0,   34,
    0,    0,    0,    0,    0,    0,  149,  150,  151,   36,
  152,  153,  154,    0,  155,   34,    0,    0,    0,    0,
  159,    0,    0,    0,    0,  241,   56,    0,  156,   56,
   56,   56,   56,   56,   56,   56,    0,    0,  157,    0,
    0,   34,    0,  159,  149,  150,  151,    0,  152,  153,
  154,  160,  155,    0,    0,    0,    0,    0,    0,    0,
  645,    0,    0,    0,    0,    0,  156,    0,    0,   35,
    0,    0,    0,    0,  160,    0,  157,    0,    0,    0,
    0,    0,    0,  158,    0,    0,    0,    0,    0,  149,
  150,  151,    0,  152,  153,  154,    0,  155,    0,    0,
   36,    0,    0,    0,   35,    0,  158,   21,    0,    0,
    0,  156,    0,    0,    0,    0,   22,    0,    0,    0,
    0,  157,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,   34,    0,  116,   36,    0,    0,   31,    0,
   21,    0,    0,   32,   33,    0,    0,    0,    0,   22,
    0,    0,   35,    0,  127,    0,   21,   23,   24,   25,
   26,   27,   28,   29,   30,   22,    0,   34,    0,    0,
    0,    0,  128,   23,   24,   25,   26,   27,   28,   29,
   30,    0,   21,   36,    0,    0,    0,    0,   96,    0,
    0,   22,    0,   35,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,    0,    0,
    0,    0,    0,   41,    0,   34,    0,  149,  150,  151,
  213,  152,  153,  154,   36,  155,   35,    0,    0,    0,
    0,    0,    0,    0,  214,    0,  215,    0,    0,  156,
  149,  150,  151,    0,  152,  153,  154,    0,  155,  157,
    0,    0,    0,   35,    0,    0,   34,   36,   59,  241,
    0,    0,  156,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  157,  113,  114,    0,    0,    0,    0,   35,
    0,    0,   22,  115,   36,    0,    0,    0,    0,   34,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,    0,    0,   35,    0,    0,    0,  113,  114,
   36,    0,  637,    0,    0,    0,   34,   22,  115,    0,
   35,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,   36,    0,    0,    0,    0,
    0,    0,   34,    0,    0,    0,    0,    0,    0,    0,
    0,   36,    0,    0,    0,    0,  113,  221,    0,    0,
    0,    0,    0,    0,    0,   22,  222,   34,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,    0,    0,   34,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,    0,    0,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,  596,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,  625,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,  241,  241,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   21,    0,
    0,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,  113,   23,   24,   25,   26,   27,
   28,   29,   30,   22,    0,    0,    0,    0,  241,  241,
  241,   23,   24,   25,   26,   27,   28,   29,   30,  241,
    0,    0,  241,  241,  241,  241,  241,  241,  241,  241,
  241,    0,    0,  241,  241,    0,    0,  241,  241,  241,
  241,  241,  241,  231,  231,  241,  241,  241,    0,    0,
    0,  241,    0,    0,    0,  241,  241,  241,    0,    0,
    0,  241,  241,  241,  241,  241,    0,  241,    0,  241,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  241,    0,    0,    0,    0,    0,  231,  231,  231,    0,
    0,  241,  241,  241,  241,    0,    0,  231,    0,    0,
  231,  231,  231,  231,  231,  231,  231,  231,  231,    0,
    0,  231,  231,    0,    0,  231,  231,  231,  231,  231,
  231,  217,  217,  231,  231,  231,    0,    0,    0,  231,
    0,    0,    0,  231,  231,  231,    0,    0,    0,  231,
  231,  231,  231,  231,    0,  231,    0,  231,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  231,    0,
    0,    0,    0,    0,  217,  217,  217,    0,    0,  231,
  231,  231,  231,    0,    0,  217,    0,    0,  217,  217,
  217,  217,  217,  217,  217,  217,  217,    0,    0,  217,
  217,    0,    0,  217,  217,  217,  217,  217,  217,  232,
  232,  217,  217,  217,    0,    0,    0,  217,    0,    0,
    0,  217,  217,  217,    0,    0,    0,  217,  217,  217,
  217,  217,    0,  217,    0,  217,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  217,    0,    0,    0,
    0,    0,  232,  232,  232,    0,    0,  217,  217,  217,
  217,    0,    0,  232,    0,    0,  232,  232,  232,  232,
  232,  232,  232,  232,  232,    0,    0,  232,  232,    0,
    0,  232,  232,  232,  232,  232,  232,  219,  219,  232,
  232,  232,    0,    0,    0,  232,    0,    0,    0,  232,
  232,  232,    0,    0,    0,  232,  232,  232,  232,  232,
    0,  232,    0,  232,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  232,    0,    0,    0,    0,    0,
  219,  219,  219,    0,    0,  232,  232,  232,  232,    0,
    0,  219,    0,    0,  219,  219,  219,  219,  219,  219,
  219,  219,  219,    0,    0,  219,  219,    0,    0,  219,
  219,  219,  219,  219,  219,  221,  221,  219,  219,  219,
    0,    0,    0,  219,    0,    0,    0,  219,  219,  219,
    0,    0,    0,  219,  219,  219,  219,  219,    0,  219,
    0,  219,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  219,    0,    0,    0,    0,    0,  221,  221,
  221,    0,    0,  219,  219,  219,  219,    0,    0,  221,
    0,    0,  221,  221,  221,  221,  221,  221,  221,  221,
  221,    0,    0,  221,  221,    0,    0,  221,  221,  221,
  221,  221,  221,  220,  220,  221,  221,  221,    0,    0,
    0,  221,    0,    0,    0,  221,  221,  221,    0,    0,
    0,  221,  221,  221,  221,  221,    0,  221,    0,  221,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  221,    0,    0,    0,    0,    0,  220,  220,  220,    0,
    0,  221,  221,  221,  221,    0,    0,  220,    0,    0,
  220,  220,  220,  220,  220,  220,  220,  220,  220,    0,
    0,  220,  220,    0,    0,  220,  220,  220,  220,  220,
  220,  284,    0,  220,  220,  220,    0,    0,    0,  220,
    0,    0,    0,  220,  220,  220,    0,    0,    0,  220,
  220,  220,  220,  220,    0,  220,    0,  220,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  220,    0,
    0,    0,    0,    0,  285,  286,  287,    0,    0,  220,
  220,  220,  220,    0,    0,  288,    0,    0,  289,  290,
  291,  292,  293,  294,  295,  296,  297,    0,    0,  298,
  299,    0,    0,  300,  301,  302,  303,  304,  305,  179,
    0,  306,  307,  308,    0,    0,    0,  309,    0,    0,
    0,  310,  311,  312,    0,    0,    0,  313,  314,  315,
  316,  317,    0,  318,    0,  319,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  320,    0,    0,    0,
    0,    0,  179,  179,  179,    0,    0,  321,  322,  323,
  324,    0,    0,  179,    0,    0,  179,  179,  179,  179,
  179,  179,  179,  179,  179,    0,    0,  179,  179,    0,
    0,  179,  179,  179,  179,  179,  179,  180,    0,  179,
  179,  179,    0,    0,    0,  179,    0,    0,    0,  179,
  179,  179,    0,    0,    0,  179,  179,  179,  179,  179,
    0,  179,    0,  179,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  179,    0,    0,    0,    0,    0,
  180,  180,  180,    0,    0,  179,  179,  179,  179,    0,
    0,  180,    0,    0,  180,  180,  180,  180,  180,  180,
  180,  180,  180,    0,    0,  180,  180,    0,    0,  180,
  180,  180,  180,  180,  180,  181,    0,  180,  180,  180,
    0,    0,    0,  180,    0,    0,    0,  180,  180,  180,
    0,    0,    0,  180,  180,  180,  180,  180,    0,  180,
    0,  180,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  180,    0,    0,    0,    0,    0,  181,  181,
  181,    0,    0,  180,  180,  180,  180,    0,    0,  181,
    0,    0,  181,  181,  181,  181,  181,  181,  181,  181,
  181,    0,    0,  181,  181,    0,    0,  181,  181,  181,
  181,  181,  181,  182,    0,  181,  181,  181,    0,    0,
    0,  181,    0,    0,    0,  181,  181,  181,    0,    0,
    0,  181,  181,  181,  181,  181,    0,  181,    0,  181,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  181,    0,    0,    0,    0,    0,  182,  182,  182,    0,
    0,  181,  181,  181,  181,    0,    0,  182,    0,    0,
  182,  182,  182,  182,  182,  182,  182,  182,  182,    0,
    0,  182,  182,    0,    0,  182,  182,  182,  182,  182,
  182,    0,    0,  182,  182,  182,    0,    0,    0,  182,
    0,    0,    0,  182,  182,  182,    0,    0,    0,  182,
  182,  182,  182,  182,    0,  182,    0,  182,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  182,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  182,
  182,  182,  182,  288,    0,    0,  289,  290,  291,  292,
  293,  294,  295,  296,  297,    0,    0,  298,  299,    0,
    0,  300,  301,  302,  303,  304,  305,    0,    0,  306,
  307,  308,    0,    0,    0,  309,    0,    0,    0,  310,
  311,  312,    0,    0,    0,  313,  314,  315,  316,  317,
    0,  318,    0,  319,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  320,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  321,  322,  323,  324,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  186,  193,  254,  104,   33,    6,    7,  248,   33,
  195,   60,   42,   40,  199,   40,  444,  123,  323,  123,
   42,   40,   40,   40,   31,  123,   42,   34,   60,  123,
  187,   31,  123,  274,   41,  307,  325,  123,   44,   60,
   62,   41,   91,   42,   52,   53,  123,  123,  123,  234,
  123,   44,  237,  238,   44,  344,   44,   64,   61,  491,
  492,   68,  162,   70,  164,   40,  307,   44,   68,   44,
   91,   78,  229,  258,  123,   40,  504,   93,  235,   44,
  600,  601,  239,   90,   91,   62,   93,   44,   95,   41,
  276,  123,   91,  590,  280,  102,  281,  102,  105,  106,
   61,  201,  123,  293,  294,   93,  206,  264,   60,  266,
  267,   40,   44,  633,  121,   61,  121,  307,  423,  125,
  552,  129,  619,  280,  266,  270,  271,  134,  135,    0,
  135,   60,  125,  268,  269,  125,   61,  272,  273,   91,
  275,  430,  284,  432,   40,   41,   41,  436,   44,   44,
  125,  257,  159,  160,  254,   41,  291,  292,   44,   87,
  125,   42,   91,   44,  290,   42,  292,   44,  125,  269,
  274,  123,  298,  308,  297,  298,  274,  184,  606,  184,
  274,   40,   41,  274,  112,   44,  343,  195,  274,  196,
  260,  199,   41,  125,  123,   44,   61,  274,  274,  274,
   61,  274,   41,  307,   42,   44,   44,  496,  636,  307,
    0,  500,  501,  307,  354,  264,  280,   41,  358,  227,
   44,  361,   41,   41,  273,   44,   44,   42,  368,   44,
  158,   42,  281,  282,  283,  284,  285,  286,  287,  288,
   42,  248,   44,  264,  172,  272,  274,  272,  255,  277,
  274,  259,  273,  272,  272,  272,  293,  294,  290,    0,
  281,  282,  283,  284,  285,  286,  287,  288,  118,  302,
  559,  278,  279,  303,  304,  305,  306,  303,  304,  305,
  306,  288,  289,  290,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,    0,  225,  226,  306,
  307,  257,  309,   61,  282,  257,  258,  259,   40,  261,
  262,  263,  361,  265,  321,  322,  323,  274,  246,   41,
   42,  561,  343,  323,  276,   41,   42,  279,  257,  258,
  259,  123,  261,  262,  263,   41,  265,  289,   44,   42,
  260,   44,  274,  272,  273,   41,   41,  354,   44,   44,
  279,  358,  260,  331,  361,  333,   40,  285,   40,  287,
  289,  368,  369,  341,   41,  267,   41,   44,  608,   44,
  267,   41,   40,  380,   44,  303,  304,  305,   60,   40,
  308,  123,  310,  311,  312,  313,  314,  315,  316,  317,
  318,   62,   41,  644,  401,   44,  123,  268,  269,  293,
  294,  272,  273,   40,  275,  299,  300,  301,  123,   91,
  338,  418,   79,   80,   40,  422,  423,   60,   40,   58,
  291,  292,  422,  423,   41,   41,   41,   44,   44,   44,
  359,   41,   41,   58,   44,   44,  627,  308,   40,   40,
   61,  123,  274,  361,  629,  374,  375,   44,   91,   40,
   40,  307,  260,  431,  360,   44,  360,   40,   40,  437,
  438,   40,   41,  470,  471,   44,  651,  474,  475,  476,
  477,  478,  479,  480,  481,  482,  274,  257,  268,  269,
  123,  360,  272,  273,  257,  275,  493,  274,  368,  274,
  368,  648,  257,  493,   61,  274,  401,  273,  348,  656,
   44,  291,  292,  353,   44,   44,  356,  357,   44,  359,
  360,   44,  362,  363,  364,  365,  366,  367,  308,   44,
  370,  371,  372,  373,  502,  368,  626,  268,  269,   44,
  266,  272,  273,  368,  275,  368,   44,  368,  466,  467,
  468,  368,  549,  368,  644,  473,  125,  368,   40,   44,
  291,  292,  368,   44,  561,  368,   44,   44,   44,   44,
   44,   44,   44,   44,  268,  269,   44,  308,  272,  273,
  420,  275,   44,   44,   44,  257,  258,  259,  360,  261,
  262,  263,   44,  265,   44,   40,   91,  291,  292,  596,
  272,  273,   44,  600,  601,   44,  446,  279,   44,   44,
  450,  608,   44,  453,  308,  533,   44,  289,   44,  257,
  460,  461,   44,   44,   44,   44,   44,   44,   91,   40,
  548,  264,   40,  630,   44,  360,  633,  360,   44,   44,
  273,   40,   93,  483,  484,  485,   40,   44,  281,  282,
  283,  284,  285,  286,  287,  288,  257,   44,   40,  257,
    0,   40,   10,   54,  582,  583,  506,  111,  508,  509,
  167,  511,  512,   18,  514,  515,  516,  517,  518,  519,
  147,  112,  522,  523,  524,  525,   64,  359,  134,  196,
  530,  597,  471,  604,  160,  328,  329,  327,  345,  327,
  630,  269,  374,  375,  273,  274,  624,  378,  379,  380,
  381,  382,  383,  384,  385,  386,  387,  547,  608,   -1,
   -1,   -1,   -1,  563,   40,   -1,  566,   -1,   -1,  569,
   -1,   -1,   -1,   -1,   -1,   -1,  576,  577,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,  591,  592,  593,   -1,   -1,   -1,  597,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   -1,   -1,  353,  354,  355,  627,   -1,   -1,
  359,  273,  274,   -1,  363,  364,  365,   -1,   -1,  368,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,  400,  401,  402,  316,  317,  318,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,  260,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,   60,
   -1,  353,  354,  355,  276,   -1,   -1,  359,   -1,  273,
  274,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   -1,   40,   -1,
   91,   -1,   -1,   -1,  306,   -1,  388,  309,  310,  311,
  312,  313,  314,  315,   -1,   -1,   -1,  399,  400,  401,
  402,   -1,  316,  317,  318,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  123,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,  273,  274,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   40,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,
   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,  335,
  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,
  346,  347,  348,  349,  350,   -1,   -1,  353,  354,  355,
   -1,   -1,   -1,  359,  273,  274,   -1,  363,  364,  365,
   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,
   -1,  377,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  388,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,  399,  400,  401,  402,  316,  317,  318,
  281,  282,  283,  284,  285,  286,  287,  288,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,  302,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   -1,   -1,  353,  354,  355,   -1,   -1,   -1,
  359,  273,  274,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  399,  400,  401,  402,  316,  317,  318,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  327,   -1,   -1,  330,  331,
  332,  333,  334,  335,  336,  337,  338,   -1,   -1,  341,
  342,   -1,   -1,  345,  346,  347,  348,  349,  350,   -1,
   -1,  353,  354,  355,   -1,   -1,   -1,  359,  273,  274,
   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,
  372,  373,   -1,  375,   -1,  377,   -1,   40,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,  401,
  402,  316,  317,  318,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  327,   -1,   -1,  330,  331,  332,  333,  334,
  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,   -1,
  345,  346,  347,  348,  349,  350,  273,  274,  353,  354,
  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,  364,
  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,   -1,
  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,  316,
  317,  318,   -1,   -1,  399,  400,  401,  402,   -1,   -1,
  327,   -1,   -1,  330,  331,  332,  333,  334,  335,  336,
  337,  338,   -1,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,   -1,   -1,  353,  354,  355,   -1,
   -1,   -1,  359,  273,  274,   42,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   60,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  388,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  399,  400,  401,  402,  316,  317,  318,   -1,
   -1,   -1,   -1,   -1,   91,   42,   -1,  327,   -1,   -1,
  330,  331,  332,  333,  334,  335,  336,  337,  338,   -1,
   -1,  341,  342,   60,   -1,  345,  346,  347,  348,  349,
  350,   -1,   -1,  353,  354,  355,  123,   -1,   -1,  359,
  273,  274,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   91,  375,   60,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,   -1,  261,  262,   -1,   -1,  399,
  400,  401,  402,  316,  317,  318,  123,   91,   -1,   -1,
   -1,   -1,   -1,   -1,  327,   -1,   -1,  330,  331,  332,
  333,  334,  335,  336,  337,  338,   -1,   -1,  341,  342,
   -1,   -1,  345,  346,  347,  348,  349,  350,   -1,  123,
  353,  354,  355,   -1,   -1,  125,  359,   60,   -1,   -1,
  363,  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,
  373,   -1,  375,   -1,  377,   -1,   -1,   -1,  229,   -1,
   -1,   -1,   -1,   -1,  235,  388,  125,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,  264,   -1,  272,  273,   -1,   -1,   -1,
  123,  272,  279,   -1,  380,  381,  382,  383,   -1,  280,
   -1,   -1,  289,  389,  390,  391,  392,  393,  394,  395,
  396,  397,  398,   -1,   -1,   -1,  303,  304,  305,  306,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,  125,   -1,   -1,   -1,  328,   -1,  330,
   -1,   -1,  289,  257,  258,  259,   -1,  261,  262,  263,
  260,  265,  343,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,  359,   -1,   -1,  279,  276,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  289,   -1,  374,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  303,
  304,  305,  306,   -1,  273,   -1,  306,   -1,   -1,  309,
  310,  311,  312,  313,  314,  315,   -1,   -1,   -1,   -1,
   -1,  125,  359,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,  374,  375,  272,
  273,   -1,   -1,   -1,   -1,  426,  279,  316,  317,  318,
   -1,   -1,   -1,   -1,   -1,  359,  289,   -1,  327,   -1,
  441,  330,  331,  332,  333,  334,  335,  336,  337,  338,
  374,  375,  341,  342,   -1,   -1,  345,  346,  347,  348,
  349,  350,   -1,   -1,  353,  354,  355,   -1,   -1,   -1,
  359,  273,   -1,   -1,  363,  364,  365,   -1,   -1,  125,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
   -1,   -1,   -1,   -1,   -1,   -1,  359,   -1,   -1,   -1,
  399,  400,  401,  402,  316,  317,  318,   -1,   -1,   -1,
   -1,  374,  375,   -1,   -1,  327,   -1,   -1,  330,  331,
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
  337,  338,   41,   -1,  341,  342,   -1,   -1,  345,  346,
  347,  348,  349,  350,  273,   -1,  353,  354,  355,   -1,
   -1,   60,  359,   -1,   -1,   -1,  363,  364,  365,   -1,
   -1,   -1,  369,  370,  371,  372,  373,   -1,  375,   -1,
  377,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,
   -1,  388,   91,   -1,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  399,  400,  401,  402,   -1,   60,  327,   -1,
   -1,  330,  331,  332,  333,  334,  335,  336,  337,  338,
   -1,   -1,  341,  342,  123,   -1,  345,  346,  347,  348,
  349,  350,  273,   -1,  353,  354,  355,   -1,   91,   -1,
  359,   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,
  369,  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,
   41,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,
  123,   -1,   -1,  125,   -1,  316,  317,  318,   -1,   60,
  399,  400,  401,  402,   -1,   -1,  327,   -1,   -1,  330,
  331,  332,  333,  334,  335,  336,  337,  338,   -1,   42,
  341,  342,   -1,   -1,  345,  346,  347,  348,  349,  350,
   91,   -1,  353,  354,  355,  125,   -1,   60,  359,   -1,
   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,  370,
  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   42,   -1,   -1,  388,   91,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,  400,
  401,  402,   60,   -1,   -1,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   60,  273,   -1,   -1,   -1,   -1,  278,
  123,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,   -1,   91,   -1,   -1,   -1,  296,   -1,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   60,   -1,  260,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   60,   -1,  276,  123,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,  123,   91,   -1,   -1,
   -1,   -1,   -1,  296,   -1,   -1,   -1,   -1,   60,   -1,
  260,   -1,   -1,   91,  306,   -1,   -1,  309,  310,  311,
  312,  313,  314,  315,   -1,   -1,  276,   -1,   -1,  123,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   91,
  261,  262,  263,   -1,  265,  123,   -1,   -1,   -1,   -1,
   60,   -1,   -1,   -1,   -1,  276,  306,   -1,  279,  309,
  310,  311,  312,  313,  314,  315,   -1,   -1,  289,   -1,
   -1,  123,   -1,   60,  257,  258,  259,   -1,  261,  262,
  263,   91,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,   60,
   -1,   -1,   -1,   -1,   91,   -1,  289,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,
   91,   -1,   -1,   -1,   60,   -1,  123,  264,   -1,   -1,
   -1,  279,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  289,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,  123,   -1,  125,   91,   -1,   -1,  295,   -1,
  264,   -1,   -1,  300,  301,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   60,   -1,  278,   -1,  264,  281,  282,  283,
  284,  285,  286,  287,  288,  273,   -1,  123,   -1,   -1,
   -1,   -1,  296,  281,  282,  283,  284,  285,  286,  287,
  288,   -1,  264,   91,   -1,   -1,   -1,   -1,  296,   -1,
   -1,  273,   -1,   60,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,
   -1,   -1,   -1,  295,   -1,  123,   -1,  257,  258,  259,
  260,  261,  262,  263,   91,  265,   60,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  274,   -1,  276,   -1,   -1,  279,
  257,  258,  259,   -1,  261,  262,  263,   -1,  265,  289,
   -1,   -1,   -1,   60,   -1,   -1,  123,   91,  125,  276,
   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  289,  264,  265,   -1,   -1,   -1,   -1,   60,
   -1,   -1,  273,  274,   91,   -1,   -1,   -1,   -1,  123,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,  264,  265,
   91,   -1,   93,   -1,   -1,   -1,  123,  273,  274,   -1,
   60,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   91,   -1,   -1,   -1,   -1,
   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   91,   -1,   -1,   -1,   -1,  264,  265,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  274,  123,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,   -1,   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,  273,  274,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,  264,  281,  282,  283,  284,  285,
  286,  287,  288,  273,   -1,   -1,   -1,   -1,  316,  317,
  318,  281,  282,  283,  284,  285,  286,  287,  288,  327,
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
  350,   -1,   -1,  353,  354,  355,   -1,   -1,   -1,  359,
   -1,   -1,   -1,  363,  364,  365,   -1,   -1,   -1,  369,
  370,  371,  372,  373,   -1,  375,   -1,  377,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  388,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  399,
  400,  401,  402,  327,   -1,   -1,  330,  331,  332,  333,
  334,  335,  336,  337,  338,   -1,   -1,  341,  342,   -1,
   -1,  345,  346,  347,  348,  349,  350,   -1,   -1,  353,
  354,  355,   -1,   -1,   -1,  359,   -1,   -1,   -1,  363,
  364,  365,   -1,   -1,   -1,  369,  370,  371,  372,  373,
   -1,  375,   -1,  377,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  388,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  399,  400,  401,  402,
  };

#line 956 "Repil/IR/IR.jay"

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
