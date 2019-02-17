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
//t    "literal_structure : '{' type_list '}'",
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
    "ZEROINITIALIZER","DEFINE","DECLARE","UNNAMED_ADDR",
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
  case_8();
  break;
case 9:
  case_9();
  break;
case 10:
  case_10();
  break;
case 12:
#line 91 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 13:
#line 95 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 14:
  case_14();
  break;
case 15:
#line 104 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 16:
  case_16();
  break;
case 17:
#line 116 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (bool)yyVals[-6+yyTop], (LType)yyVals[-5+yyTop], (Constant)yyVals[-4+yyTop], isPrivate: false);
    }
  break;
case 18:
#line 120 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], (bool)yyVals[-5+yyTop], (LType)yyVals[-4+yyTop], (Constant)yyVals[-3+yyTop], isPrivate: true);
    }
  break;
case 19:
#line 124 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-8+yyTop], (bool)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], null, isPrivate: true);
    }
  break;
case 20:
#line 128 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 21:
#line 129 "Repil/IR/IR.jay"
  { yyVal = true; }
  break;
case 25:
  case_25();
  break;
case 26:
  case_26();
  break;
case 27:
#line 155 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 28:
#line 156 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 29:
#line 157 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 30:
#line 158 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 31:
#line 159 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 32:
#line 163 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 33:
#line 167 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 34:
#line 174 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 35:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 36:
#line 185 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 37:
#line 189 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 38:
#line 193 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
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
case 58:
  case_58();
  break;
case 59:
#line 242 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 60:
#line 246 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 62:
#line 251 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 64:
#line 256 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 65:
#line 257 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 66:
#line 258 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 67:
#line 259 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 68:
#line 260 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 69:
#line 261 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 70:
#line 262 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 71:
#line 263 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 72:
#line 267 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 73:
#line 271 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 74:
#line 275 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 75:
#line 279 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 76:
#line 283 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 77:
#line 290 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 78:
#line 294 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 80:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 81:
#line 309 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 82:
#line 313 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 83:
#line 317 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 84:
#line 321 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 85:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 86:
#line 329 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 87:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 88:
#line 337 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 89:
#line 341 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 90:
#line 348 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 91:
#line 352 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 92:
#line 356 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 93:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 94:
#line 367 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 95:
#line 374 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 96:
#line 378 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 382 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 98:
#line 386 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 100:
#line 394 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 101:
#line 398 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 102:
#line 399 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 103:
#line 400 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 104:
#line 401 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 110:
#line 419 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 111:
#line 420 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 112:
#line 421 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 113:
#line 422 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 114:
#line 423 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 115:
#line 424 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 116:
#line 425 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 117:
#line 426 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 118:
#line 427 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 119:
#line 428 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 120:
#line 432 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 121:
#line 433 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 122:
#line 434 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 123:
#line 435 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 124:
#line 436 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 125:
#line 437 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 126:
#line 438 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 127:
#line 439 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 128:
#line 440 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 129:
#line 441 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 130:
#line 442 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 131:
#line 443 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 132:
#line 444 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 133:
#line 445 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 134:
#line 446 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 135:
#line 447 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 137:
#line 452 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 138:
#line 453 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 139:
#line 457 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 140:
#line 461 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 142:
#line 469 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 143:
#line 470 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 144:
#line 471 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 145:
#line 472 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 146:
#line 473 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 147:
#line 474 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 148:
#line 475 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 149:
#line 476 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 150:
#line 477 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 151:
#line 481 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 485 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 492 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 156:
#line 510 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 157:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 528 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 550 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 554 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 561 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 565 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 572 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 576 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 169:
#line 580 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 584 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 171:
#line 591 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 602 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 606 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 175:
#line 610 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 614 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 178:
#line 625 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 629 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 636 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 181:
#line 640 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 182:
#line 644 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 183:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 184:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 185:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 186:
#line 660 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 191:
#line 677 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 681 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 687 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 194:
#line 694 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 698 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 705 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 726 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 730 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 734 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 738 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 206:
#line 745 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 749 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 753 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 757 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 761 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 765 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 212:
#line 769 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 213:
#line 773 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 214:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 215:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 216:
#line 785 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 217:
#line 789 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 793 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 797 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 801 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 829 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 230:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 231:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 232:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 857 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 861 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 865 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 885 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 889 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 893 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 245:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 905 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 909 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 913 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 929 "Repil/IR/IR.jay"
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
void case_8()
#line 73 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 78 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_10()
#line 83 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_14()
#line 97 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_16()
#line 106 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_25()
#line 143 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_26()
#line 148 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_58()
#line 232 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    6,    6,    6,   11,
   11,   16,   16,   15,    9,    9,   17,   17,   17,   17,
   17,   17,   17,   14,   14,    8,    8,    8,    8,    8,
   19,   19,   19,    7,    7,   21,   21,   21,   21,   21,
   21,   21,   21,   21,   21,   21,   21,    3,   22,   22,
   23,   23,   12,   12,   12,   12,   12,   12,   12,   12,
   12,   12,   12,   12,   12,   12,   24,   24,   25,   25,
    4,    4,    4,    4,    4,    4,    4,    4,    4,    5,
    5,    5,   26,   26,   30,   30,   30,   30,   31,   31,
   32,   32,   32,   32,   10,   10,   27,   27,   33,   34,
   34,   34,   34,   34,   34,   34,   34,   34,   34,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,   36,   36,   36,   36,   36,
   38,   13,   13,   13,   13,   13,   13,   13,   13,   13,
   13,   13,   41,   20,   20,   42,   40,   40,   43,   39,
   39,   44,   37,   37,   28,   28,   45,   45,   45,   45,
   46,   46,   48,   48,   48,   48,   50,   51,   51,   52,
   52,   52,   52,   52,   52,   52,   18,   18,   53,   53,
   54,   54,   55,   56,   56,   57,   58,   58,   59,   59,
   29,   47,   47,   47,   47,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    1,
    6,    5,    6,    6,    7,    7,   10,   10,    9,    1,
    1,    1,    1,    1,    1,    3,    3,    3,    3,    3,
    3,    6,    5,    2,    3,    1,    2,    3,    3,    3,
    1,    1,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    3,    1,    1,    1,    4,    3,    1,    3,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    4,    2,    1,    5,    5,    1,    3,    1,    1,
   11,   11,   11,   10,   12,   12,   13,   13,   14,    7,
    8,    8,    1,    3,    1,    2,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    6,    9,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    3,    3,    2,    2,    1,    2,    1,    3,    2,    1,
    3,    1,    1,    3,    1,    2,    2,    3,    1,    2,
    1,    2,    1,    2,    3,    4,    1,    1,    3,    2,
    3,    3,    3,    2,    4,    5,    1,    3,    1,    1,
    1,    3,    5,    1,    2,    3,    1,    2,    1,    1,
    1,    2,    7,    2,    7,    5,    6,    5,    5,    4,
    6,    7,    7,    8,    7,    8,    4,    5,    6,    5,
    5,    4,    4,    5,    6,    7,    6,    6,    7,    5,
    6,    5,    5,    6,    3,    4,    5,    7,    4,    5,
    6,    6,    4,    7,    5,    6,    4,    5,    4,    5,
    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   62,   74,   64,   65,   66,   67,   68,   69,   70,   71,
    0,   23,   22,    0,    0,    0,   63,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  105,  106,   24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   73,  201,    0,    0,    0,    0,    0,
    0,    5,    6,   20,   21,    0,    0,    0,    7,    0,
    0,    0,    0,    0,   58,    0,    0,    0,    0,    0,
   80,    0,    0,   77,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   25,    0,    0,    0,   43,   42,   12,
    0,    0,   36,   41,    0,    0,    0,    0,    0,   97,
   98,    0,    0,    0,   93,   72,    0,    0,    0,    0,
    0,   56,   46,   47,   48,   49,   50,   51,   52,    0,
   44,  144,  145,  143,  146,  147,  148,  142,  150,  149,
    0,    0,    0,    0,    0,    0,    0,   14,    0,    0,
    0,   37,   13,    0,  138,  137,    0,    0,  136,  154,
    0,   75,   76,    0,  109,    0,    0,  107,  101,  102,
  104,  103,    0,   99,    0,    0,   78,    0,    0,    0,
    0,   11,   45,  157,    0,    0,    0,  160,    0,    0,
    0,   29,    0,   27,   30,   31,   26,   16,   15,   40,
   39,   38,    0,    0,    0,    0,    0,    0,    0,  108,
  100,    0,    0,   94,    0,    0,    0,   53,  190,  189,
    0,  187,  152,    0,  159,    0,  151,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   34,    0,    0,    0,
    0,    0,    0,   57,    0,  158,  161,    0,    0,   19,
   33,    0,    0,    0,    0,    0,    0,   35,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  165,    0,    0,  171,    0,    0,    0,    0,  188,    0,
   18,   32,    0,    0,    0,    0,    0,    0,    0,  204,
    0,    0,  202,    0,  199,  200,    0,    0,  197,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  110,  111,  112,  113,  114,  115,
  116,  117,  118,  119,    0,  120,  121,  132,  133,  134,
  135,  123,  125,  126,  127,  128,  124,  122,  130,  131,
  129,    0,    0,    0,    0,    0,    0,   84,  166,    0,
  172,    0,    0,    0,    0,    0,    0,  139,    0,    0,
    0,    0,   83,    0,  153,    0,    0,    0,    0,  198,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  191,
    0,  177,    0,    0,    0,    0,    0,   81,    0,   82,
    0,   86,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  217,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   85,  162,    0,  163,   87,
   88,    0,    0,    0,  206,    0,  218,  245,    0,  224,
  233,    0,  221,  248,  237,  220,  250,  240,    0,    0,
  230,  209,  232,  251,    0,    0,  208,  141,  156,    0,
    0,    0,    0,    0,    0,    0,  192,    0,    0,    0,
    0,  178,    0,    0,    0,  140,    0,   89,    0,    0,
    0,  194,  207,  246,  234,  241,  231,  228,  242,    0,
    0,    0,    0,  227,  219,    0,    0,    0,    0,    0,
  180,    0,    0,    0,    0,    0,  164,  203,    0,  205,
  195,  229,  244,    0,  193,  238,    0,  182,  183,  181,
    0,  179,  212,    0,    0,  196,  185,    0,    0,  216,
  186,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  140,  111,  103,   51,
   76,  112,  169,  219,   52,   39,  104,  231,  113,  537,
  141,   60,   61,   93,   94,  124,  177,  310,   66,  125,
  183,  184,  178,  385,  402,  472,  538,  569,  197,  195,
  333,  514,  590,  539,  311,  312,  313,  314,  315,  473,
  581,  582,  232,  469,  470,  591,  592,  338,  339,
  };
  protected static readonly short [] yySindex = {          256,
  -35,   12,   -2,    5,   22, 2564, 2657, -209,    0,  256,
    0,    0,    0,    0, -137,   51,   72,   99, -133,  -23,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2976,    0,    0, 2976, -114, -106,    0,  132, -109,  -31,
 2976,  -17,  118,    0,    0,  -73,  -59,    0,    0,    0,
    2,   19,   19,   84,  185,  -22,   89,  -16,  132,   -1,
  198,   20,   38,    0,    0, 2976,  277, 2632,   -9,  295,
  206,    0,    0,    0,    0, 2976,    2,    2,    0, -148,
  305,  238, 2770,  325,    0, 2976, 2976, 2976,   -7,  342,
    0,  132,   32,    0,  335, 2602,  719, 2550, 2976, 2976,
  322,  332,   34,    0, -148, 2808,    0,    0,    0,    0,
   -8, 1342,    0,    0, 2602,  132,   15,   -4,  347,    0,
    0, -136,  -13,  114,    0,    0, 2632, 2602,  131,  350,
  375,    0,    0,    0,    0,    0,    0,    0,    0,  -91,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3009, 2976,  372, 2550,  117, 2699,  143,    0, -148,  140,
   10,    0,    0, 2842,    0,    0,   58,  379,    0,    0,
  165,    0,    0, 2602,    0,  115, -206,    0,    0,    0,
    0,    0,   48,    0, -136, 2602,    0,  172, -136,  162,
 2737,    0,    0,    0,   14, 2550,   74,    0,   64,  380,
   66,    0,  386,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  390, 3009,   19,  183, -206,  157, -111,    0,
    0,  115, -206,    0,  115,  115,  115,    0,    0,    0,
  196,    0,    0, 3009,    0, 2976,    0,  177,   76,  179,
 1591, 2976,   70,  115,   19, -110,    0,  166, 3541,  -96,
 -107,  115,  115,    0, 2737,    0,    0,  167,  181,    0,
    0,  216,  149, 2976,  -74,  115, 3541,    0,  378, 3009,
 -140, 3009, 2455, 2976, 2455, 2976, 2455, 2976, 2976, 2976,
 2976, 2976, 2455,  -38, 2976, 2976, 2976, 3009, 3009, 3009,
 2976, 2976, 3009, 1276, 3009, 3009, 3009, 3009, 3009, 3009,
 3009, 3009, 3009,  661, 2919, 2976, 2976, 1404,   43, 1387,
    0, 3541,  167,    0,  167, 3541, -103, 3541,    0,  176,
    0,    0, 3009,  314, 3541,  -86,  -72, 1464, 3925,    0,
  188, 1380,    0,  416,    0,    0, 1342, 2455,    0, 1342,
 1342, 2455, 1342, 1342, 2455, 1342, 1342, 1342, 1342, 1342,
 1342, 2455, 2976, 1342, 1342, 1342, 1342,  418,  422,  423,
  187,  329,  424, 2976,  330,  102,  104,  106,  112,  116,
  119,  121,  122,  125,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 2976,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 2976,   29, 1342,  105, 2976, 1404,    0,    0,  167,
    0,  176,  176, 1542, 3541, 1619,  436,    0, 1696, 3541,
 3541,  -76,    0,  167,    0,  440,  219,  451, 1342,    0,
  458,  459, 1342,  464,  465, 1342,  468,  469,  474,  476,
  477,  479, 1342, 1342,  492,  493,  495,  496, 3009, 3009,
 3009,  184, 2976, 2976,  351, 3009, 2976, 2976, 2976, 2976,
 2976, 2976, 2976, 2976, 2976, 1342, 1342, 1380,  498,    0,
  500,    0,  505,  105,  105, 2976,  176,    0, 1773,    0,
 3009,    0, 1850, 1927, 3541,  176,  219,  457, 1380,  506,
 1380, 1380,  507, 1380, 1380,  508, 1380, 1380, 1380, 1380,
 1380, 1380,  509,  511, 1380, 1380, 1380, 1380,    0,  513,
  514,  302, 1342,  516,  517, 3009,  518,  132,  132,  132,
  132,  132,  132,  132,  132,  132,  520,  521,  525,  484,
 3009, 2886,  536,  540,  105,    0,    0,  220,    0,    0,
    0, 2004,  543, 2976,    0, 1380,    0,    0, 1380,    0,
    0, 1380,    0,    0,    0,    0,    0,    0, 1380, 1380,
    0,    0,    0,    0, 3009, 3009,    0,    0,    0,  225,
  230,  548, 3009, 1380, 1380, 1380,    0,  551, 2914, 1308,
  252,    0, 2886, 2886,  556,    0, 3009,    0,  219,  553,
 2947,    0,    0,    0,    0,    0,    0,    0,    0,  343,
  344, 3009,  558,    0,    0,  510, 3009,  564, 2531,  412,
    0,  115, 2886,  262,  266, 2886,    0,    0,  219,    0,
    0,    0,    0,  558,    0,    0, 2495,    0,    0,    0,
  115,    0,    0,  115,  275,    0,    0,  289,  115,    0,
    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  605,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  309,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -26,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  142,    0,    0,    0,    0,    0,  568,    0,    0,
    0,    0,    0,    0,    0,    0,  405,    0,    0,    0,
    0,  568,    0,    0,    0,    4,  568,  568,    0,    0,
    0,    0,  164,    0,    0,    0,    0,    0,    0, 2709,
 2800,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  568,  568,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  290,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  568,    0,    0,    0,    0,
    0,    0,  299,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   69,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  113,  137,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  568,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 2081,    0, 3618,    0,    0,    0,    0,  242,
    0,    0,    0,  568,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  568,    0,    0,  568,
  568,    0,  568,  568,    0,  568,  568,  568,  568,  568,
  568,    0,    0,  568,  568,  568,  568,    0,    0,    0,
  568,  568,    0,    0,  568,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  568,  568,    0,    0,    0,    0,    0, 2158,
    0, 2235, 3695,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3772,    0,    0,    0,    0,  568,    0,
    0,    0,  568,    0,    0,  568,    0,    0,    0,    0,
    0,    0,  568,  568,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  568,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  568,  568,    0, 3002,    0,
    0,    0,    0,    0,    0,    0, 2312,    0,    0,    0,
    0,    0,    0,    0,    0, 3849,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  568,    0,    0,    0,    0,  482,  559,  636,
  740,  817,  894,  971, 1075, 1152,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  568,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3079,    0,    0,    0,    0,  313,  568,    0,
    0, 3156,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 3233,    0,    0,    0,    0,    0,    0,
 3310,    0,    0, 3387,    0,    0,    0,    0, 3464,    0,
    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  599,  557,    0,    0,    0,    0,  515,  526,  -12,
  336,   -6,  -93, -116,    0,  594,  454, -238,    0,   25,
  494,    0,    1,    0,  487,  -24, -170, 1213, -291,  433,
   42, -181, -112,    0,    0,  150, -523,    0,    0,    0,
 -423,  182, -122,   46, -289,    0,  323,  326,  311, -429,
 -497,   30,  391,    0,  123,    0,   63, -123, -250,
  };
  protected static readonly short [] yyTable = {            38,
   38,  221,  262,  488,  153,  217,   40,   42,   68,   57,
   82,  249,  267,   61,  223,  318,  406,   59,  227,  415,
  409,   35,   68,   68,   38,   15,  316,   59,   64,  198,
   68,   58,   68,  192,   38,  164,  420,   64,  409,   77,
   78,   69,   86,   61,  533,  534,  485,   60,  325,  603,
  421,  250,   36,  164,  252,  253,   64,  234,   18,   38,
  200,   92,  205,  543,  220,   19,   89,  218,   90,   98,
   64,  129,  126,  265,  158,  127,  172,  159,  624,  116,
  117,  118,   20,  123,   34,  614,  615,  430,  173,  123,
  171,  430,  154,  155,  430,  327,   43,  230,   59,  175,
  246,  430,  235,  188,  220,  585,  251,  114,  123,  176,
  220,   46,   91,  257,  220,  476,  163,  236,  635,  468,
   92,  123,   45,   85,  409,  331,  409,  101,   60,  409,
  114,  102,   47,  317,  209,  237,   92,  220,  233,  220,
  220,  320,   62,  332,   68,  196,   54,  230,  326,  216,
   63,  342,  220,  345,  185,   48,   49,  186,   64,  352,
  201,  230,  248,  248,  152,  618,  248,  123,  130,  175,
  248,  189,  222,   64,  186,  194,  226,  218,   71,  123,
  208,   61,   79,  159,  131,   79,   72,  248,  212,  409,
   64,   65,  323,  409,  409,  636,  412,  248,  413,  218,
   73,  218,  244,   61,   95,  215,   34,   95,  186,  175,
  422,   83,  225,  132,  220,  186,  133,  134,  135,  136,
  137,  138,  139,  245,   80,   21,  186,  151,   64,  196,
  452,  175,  266,  175,   22,  263,  254,   68,  243,  255,
   67,   17,   23,   24,   25,   26,   27,   28,   29,   30,
   55,   81,  409,   56,   70,   84,  322,  324,  256,  255,
  586,  170,   95,  587,  119,  162,  337,  340,  341,  343,
  344,  346,  347,  348,  349,  350,  351,  354,  355,  356,
  357,   16,   17,  162,  361,  362,   87,  365,  179,  180,
  181,  182,  612,  477,  330,  613,  334,   74,   75,  403,
  404,   38,  633,  353,   88,  613,  634,  486,  405,  613,
   48,   49,  358,  359,  360,  640,   90,  363,  613,  366,
  367,  368,  369,  370,  371,  372,  373,  374,   97,  641,
   96,  429,  255,   96,   96,  433,   90,   90,  436,   28,
   90,   90,   28,   90,  105,  443,  444,  417,   61,  179,
  180,  181,  182,  184,  418,   64,  184,  455,   90,   90,
  106,  142,  143,  144,  115,  145,  146,  147,   61,  148,
   64,   64,  453,  456,  128,   90,  165,  166,  466,  156,
   91,   91,  122,  149,   91,   91,  174,   91,  638,  157,
   48,   49,   64,  150,  516,  467,   50,   32,   33,   38,
   38,   35,   91,   91,   92,   92,  474,  475,   92,   92,
  190,   92,   99,  100,  191,  199,  206,  213,  214,   91,
  175,  228,  238,  239,  240,  241,   92,   92,  221,  242,
  247,   61,   36,  258,  259,  260,  264,  321,  329,  268,
  218,  631,  407,   92,   62,  155,  513,  513,  155,  248,
  518,  519,  520,  521,  522,  523,  524,  525,  526,  427,
  425,  449,  167,  639,   34,  450,  451,  454,  457,   38,
  458,  152,  459,  509,  510,  511,  535,  168,  460,  481,
  517,  426,  461,  487,  331,  462,  428,  463,  464,  431,
  432,  465,  434,  435,  489,  437,  438,  439,  440,  441,
  442,  491,  492,  445,  446,  447,  448,  494,  495,   17,
   17,  497,  498,   17,   17,  629,   17,  499,  220,  500,
  501,   61,  502,    1,    2,  580,  220,    3,    4,  155,
    5,   17,   17,  230,  151,  505,  506,  196,  507,  508,
  572,  530,  512,  531,  532,    6,    7,  544,   17,  546,
  549,  552,  559,  471,  560,  578,  565,  566,  567,  570,
  571,  573,    8,  574,  575,   61,   61,   61,  576,   61,
   61,   61,  609,   61,  468,  583,  580,  580,  490,  584,
   61,   61,  493,  600,  196,  496,  589,   61,  601,  598,
  599,  602,  503,  504,  607,  616,  619,   61,   61,  622,
  623,  587,  625,  627,    1,   21,  580,   61,   44,  580,
   79,   53,  207,  187,   22,  527,  528,  529,  224,  120,
  161,  610,   23,   24,   25,   26,   27,   28,   29,   30,
  160,  626,  617,  193,  410,  515,  121,  411,  545,  424,
  547,  548,  632,  550,  551,  319,  553,  554,  555,  556,
  557,  558,  577,  621,  561,  562,  563,  564,    0,    0,
    0,    0,  568,    0,    0,    0,   61,    0,  142,  143,
  144,    0,  145,  146,  147,   61,  148,  155,  155,    0,
    0,   61,    0,  165,  166,    0,    0,    0,    0,    0,
  149,    0,    0,    0,    0,  593,    0,    0,  594,    0,
  150,  595,    0,    0,    0,    0,    0,    0,  596,  597,
    0,    0,    0,  179,  180,  181,  182,    0,    0,  155,
  155,  155,    0,  604,  605,  606,    0,    0,    0,  611,
  155,    0,    0,  155,  155,  155,  155,  155,  155,  155,
  155,  155,    0,    0,  155,  155,    0,    0,  155,  155,
  155,  155,  155,  155,  247,  247,  155,  155,  155,  630,
    0,    0,  155,    0,    0,    0,  155,  155,  155,  167,
    0,  155,  155,  155,  155,  155,  155,    0,  155,   61,
  155,    0,    0,    0,  168,    0,    0,    0,    0,    0,
    0,  155,    0,    0,    0,    0,  247,  247,  247,    0,
    0,    0,  155,  155,  155,  155,    0,  247,    0,    0,
  247,  247,  247,  247,  247,  247,  247,  247,  247,    0,
    0,  247,  247,    0,    0,  247,  247,  247,  247,  247,
  247,  252,  252,  247,  247,  247,    0,    0,    0,  247,
    0,    0,    0,  247,  247,  247,    0,    0,    0,  247,
  247,  247,  247,  247,    0,  247,   61,  247,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  247,    0,
    0,    0,    0,  252,  252,  252,    0,    0,    0,  247,
  247,  247,  247,    0,  252,    0,    0,  252,  252,  252,
  252,  252,  252,  252,  252,  252,    0,    0,  252,  252,
    0,    0,  252,  252,  252,  252,  252,  252,  239,  239,
  252,  252,  252,    0,    0,    0,  252,    0,    0,    0,
  252,  252,  252,    0,    0,    0,  252,  252,  252,  252,
  252,    0,  252,   61,  252,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  252,    0,    0,    0,    0,
  239,  239,  239,    0,    0,    0,  252,  252,  252,  252,
    0,  239,    0,    0,  239,  239,  239,  239,  239,  239,
  239,  239,  239,    0,    0,  239,  239,    0,  130,  239,
  239,  239,  239,  239,  239,    0,    0,  239,  239,  239,
    0,    0,    0,  239,  131,    0,    0,  239,  239,  239,
    0,    0,    0,  239,  239,  239,  239,  239,    0,  239,
   61,  239,  222,  222,    0,    0,    0,    0,    0,    0,
    0,    0,  239,  132,    0,    0,  133,  134,  135,  136,
  137,  138,  139,  239,  239,  239,  239,  375,  376,  377,
  378,  379,  380,  381,  382,  383,  384,    0,    0,    0,
    0,    0,    0,    0,  222,  222,  222,    0,    0,    0,
    0,    0,    0,    0,    0,  222,    0,    0,  222,  222,
  222,  222,  222,  222,  222,  222,  222,    0,    0,  222,
  222,    0,    0,  222,  222,  222,  222,  222,  222,  223,
  223,  222,  222,  222,    0,    0,    0,  222,    0,    0,
    0,  222,  222,  222,    0,    0,    0,  222,  222,  222,
  222,  222,    0,  222,   61,  222,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  222,    0,    0,    0,
    0,  223,  223,  223,    0,    0,    0,  222,  222,  222,
  222,    0,  223,    0,    0,  223,  223,  223,  223,  223,
  223,  223,  223,  223,    0,    0,  223,  223,    0,    0,
  223,  223,  223,  223,  223,  223,  249,  249,  223,  223,
  223,    0,    0,    0,  223,    0,    0,    0,  223,  223,
  223,    0,    0,    0,  223,  223,  223,  223,  223,    0,
  223,   61,  223,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  223,    0,    0,    0,    0,  249,  249,
  249,    0,    0,    0,  223,  223,  223,  223,    0,  249,
    0,    0,  249,  249,  249,  249,  249,  249,  249,  249,
  249,    0,    0,  249,  249,    0,    0,  249,  249,  249,
  249,  249,  249,  243,  243,  249,  249,  249,    0,    0,
    0,  249,    0,    0,    0,  249,  249,  249,    0,    0,
    0,  249,  249,  249,  249,  249,    0,  249,    0,  249,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  249,    0,    0,    0,    0,  243,  243,  243,    0,    0,
    0,  249,  249,  249,  249,    0,  243,    0,    0,  243,
  243,  243,  243,  243,  243,  243,  243,  243,    0,    0,
  243,  243,    0,    0,  243,  243,  243,  243,  243,  243,
    0,    0,  243,  243,  243,    0,    0,    0,  243,    0,
    0,    0,  243,  243,  243,   35,    0,    0,  243,  243,
  243,  243,  243,    0,  243,    0,  243,  236,  236,   64,
    0,    0,    0,    0,    0,    0,    0,  243,    0,    0,
    0,    0,    0,    0,    0,    0,   36,  152,  243,  243,
  243,  243,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   64,    0,    0,    0,    0,    0,  236,
  236,  236,    0,    0,    0,    0,    0,    0,   34,    0,
  236,  152,    0,  236,  236,  236,  236,  236,  236,  236,
  236,  236,    0,    0,  236,  236,    0,    0,  236,  236,
  236,  236,  236,  236,  210,  210,  236,  236,  236,    0,
  151,    0,  236,    0,    0,    0,  236,  236,  236,  152,
    0,    0,  236,  236,  236,  236,  236,    0,  236,    0,
  236,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  236,    0,   35,  151,    0,  210,  210,  210,    0,
    0,    0,  236,  236,  236,  236,    0,  210,    0,  328,
  210,  210,  210,  210,  210,  210,  210,  210,  210,    0,
    0,  210,  210,    0,   36,  210,  210,  210,  210,  210,
  210,    0,  151,  210,  210,  210,    0,    0,    0,  210,
    0,  408,    0,  210,  210,  210,    0,    0,    0,  210,
  210,  210,  210,  210,    0,  210,   34,  210,  414,    0,
  416,    0,    0,    0,    0,    0,    0,  419,  210,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,  210,
  210,  210,  210,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,  142,  143,  144,    0,  145,  146,
  147,    0,  148,    0,    0,    0,    0,    0,    0,  165,
  166,    0,    0,    0,    0,    0,  149,    0,  423,    0,
    0,    0,    0,    0,    0,    0,  150,    0,  142,  143,
  144,    0,  145,  146,  147,    0,  148,    0,    0,  179,
  180,  181,  182,  165,  166,    0,    0,    0,    0,    0,
  149,    0,    0,    0,    0,    0,    0,  479,    0,    0,
  150,  261,  483,  484,    0,  364,  142,  143,  144,    0,
  145,  146,  147,    0,  148,    0,    0,    0,    0,    0,
  152,  165,  166,    0,    0,    0,    0,    0,  149,  269,
    0,    0,    0,    0,    0,  167,  478,   21,  150,    0,
    0,    0,    0,    0,    0,    0,   22,    0,    0,    0,
  168,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,  542,    0,  167,
    0,  270,  271,  272,   65,    0,    0,    0,    0,    0,
    0,    0,  273,  151,  168,  274,  275,  276,  277,  278,
  279,  280,  281,  282,    0,    0,  283,  284,    0,    0,
  285,  286,  287,  288,  289,  290,  269,  167,  291,  292,
  293,    0,    0,  480,  294,    0,    0,    0,  295,  296,
  297,    0,  168,    0,  298,  299,  300,  301,  302,    0,
  303,    0,  304,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  305,    0,    0,    0,    0,  270,  271,
  272,    0,    0,    0,  306,  307,  308,  309,    0,  273,
    0,    0,  274,  275,  276,  277,  278,  279,  280,  281,
  282,    0,    0,  283,  284,    0,    0,  285,  286,  287,
  288,  289,  290,    0,  269,  291,  292,  293,    0,    0,
  482,  294,    0,    0,    0,  295,  296,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,    0,  304,
    0,    0,    0,    0,    0,    0,    0,  142,  143,  144,
  305,  145,  146,  147,    0,  148,  270,  271,  272,    0,
    0,  306,  307,  308,  309,    0,  229,  273,    0,  149,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  150,
    0,  283,  284,    0,    0,  285,  286,  287,  288,  289,
  290,  269,    0,  291,  292,  293,    0,  536,    0,  294,
    0,    0,    0,  295,  296,  297,    0,    0,    0,  298,
  299,  300,  301,  302,    0,  303,    0,  304,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  305,    0,
    0,    0,    0,  270,  271,  272,    0,    0,    0,  306,
  307,  308,  309,    0,  273,    0,    0,  274,  275,  276,
  277,  278,  279,  280,  281,  282,    0,    0,  283,  284,
    0,    0,  285,  286,  287,  288,  289,  290,  269,    0,
  291,  292,  293,    0,  540,    0,  294,    0,    0,    0,
  295,  296,  297,    0,    0,    0,  298,  299,  300,  301,
  302,    0,  303,    0,  304,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  305,    0,    0,    0,    0,
  270,  271,  272,    0,    0,    0,  306,  307,  308,  309,
    0,  273,    0,    0,  274,  275,  276,  277,  278,  279,
  280,  281,  282,    0,    0,  283,  284,    0,    0,  285,
  286,  287,  288,  289,  290,  269,    0,  291,  292,  293,
    0,  541,    0,  294,    0,    0,    0,  295,  296,  297,
    0,    0,    0,  298,  299,  300,  301,  302,    0,  303,
    0,  304,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  305,    0,    0,    0,    0,  270,  271,  272,
    0,    0,    0,  306,  307,  308,  309,    0,  273,    0,
    0,  274,  275,  276,  277,  278,  279,  280,  281,  282,
    0,    0,  283,  284,    0,    0,  285,  286,  287,  288,
  289,  290,  269,    0,  291,  292,  293,    0,  588,    0,
  294,    0,    0,    0,  295,  296,  297,    0,    0,    0,
  298,  299,  300,  301,  302,    0,  303,    0,  304,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  305,
    0,    0,    0,    0,  270,  271,  272,    0,    0,    0,
  306,  307,  308,  309,    0,  273,    0,    0,  274,  275,
  276,  277,  278,  279,  280,  281,  282,    0,    0,  283,
  284,    0,    0,  285,  286,  287,  288,  289,  290,  269,
    0,  291,  292,  293,    0,  169,    0,  294,    0,    0,
    0,  295,  296,  297,    0,    0,    0,  298,  299,  300,
  301,  302,    0,  303,    0,  304,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  305,    0,    0,    0,
    0,  270,  271,  272,    0,    0,    0,  306,  307,  308,
  309,    0,  273,    0,    0,  274,  275,  276,  277,  278,
  279,  280,  281,  282,    0,    0,  283,  284,    0,    0,
  285,  286,  287,  288,  289,  290,  269,    0,  291,  292,
  293,    0,  167,    0,  294,    0,    0,    0,  295,  296,
  297,    0,    0,    0,  298,  299,  300,  301,  302,    0,
  303,    0,  304,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  305,    0,    0,    0,    0,  270,  271,
  272,    0,    0,    0,  306,  307,  308,  309,    0,  273,
    0,    0,  274,  275,  276,  277,  278,  279,  280,  281,
  282,    0,    0,  283,  284,    0,    0,  285,  286,  287,
  288,  289,  290,  169,    0,  291,  292,  293,    0,  170,
    0,  294,    0,    0,    0,  295,  296,  297,    0,    0,
    0,  298,  299,  300,  301,  302,    0,  303,    0,  304,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  305,    0,    0,    0,    0,  169,  169,  169,    0,    0,
    0,  306,  307,  308,  309,    0,  169,    0,    0,  169,
  169,  169,  169,  169,  169,  169,  169,  169,    0,    0,
  169,  169,    0,    0,  169,  169,  169,  169,  169,  169,
  167,    0,  169,  169,  169,    0,  168,    0,  169,    0,
    0,    0,  169,  169,  169,    0,    0,    0,  169,  169,
  169,  169,  169,    0,  169,    0,  169,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  169,    0,    0,
    0,    0,  167,  167,  167,    0,    0,    0,  169,  169,
  169,  169,    0,  167,    0,    0,  167,  167,  167,  167,
  167,  167,  167,  167,  167,    0,    0,  167,  167,    0,
    0,  167,  167,  167,  167,  167,  167,  170,    0,  167,
  167,  167,    0,    0,   35,  167,    0,    0,    0,  167,
  167,  167,    0,    0,    0,  167,  167,  167,  167,  167,
    0,  167,    0,  167,    0,  637,    0,    0,    0,    0,
    0,    0,    0,    0,  167,   36,    0,    0,    0,  170,
  170,  170,    0,    0,  152,  167,  167,  167,  167,    0,
  170,    0,    0,  170,  170,  170,  170,  170,  170,  170,
  170,  170,   64,    0,  170,  170,    0,   34,  170,  170,
  170,  170,  170,  170,  168,    0,  170,  170,  170,    0,
  152,   64,  170,    0,    0,    0,  170,  170,  170,    0,
    0,    0,  170,  170,  170,  170,  170,    0,  170,  152,
  170,    0,    0,    0,    0,    0,    0,  151,    0,    0,
    0,  170,    0,   35,    0,    0,  168,  168,  168,    0,
    0,    0,  170,  170,  170,  170,    0,  168,    0,    0,
  168,  168,  168,  168,  168,  168,  168,  168,  168,    0,
    0,  168,  168,  151,   36,  168,  168,  168,  168,  168,
  168,   35,    0,  168,  168,  168,    0,    0,    0,  168,
    0,    0,  151,  168,  168,  168,    0,    0,    0,  168,
  168,  168,  168,  168,    0,  168,   34,  168,    0,    0,
    0,   35,   36,    0,    0,    0,    0,    0,  168,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  168,
  168,  168,  168,    0,    0,    0,   35,    0,   21,    0,
    0,    0,   36,    0,   34,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,    0,    0,   36,    0,    0,
    0,  142,  143,  144,   34,  145,  146,  147,  152,  148,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  229,    0,    0,  149,    0,    0,    0,    0,    0,   34,
    0,  335,  336,  150,    0,    0,    0,  142,  143,  144,
    0,  145,  146,  147,    0,  148,  152,    0,    0,    0,
    0,    0,    0,  628,    0,    0,  142,  143,  144,  149,
  145,  146,  147,    0,  148,    0,    0,    0,    0,  150,
    0,  151,    0,    0,    0,    0,    0,   21,  149,   35,
    0,    0,    0,   54,    0,    0,   22,    0,  150,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,   31,    0,  151,
   36,    0,   32,   33,    0,   21,    0,   35,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,  120,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,   34,    0,  110,   21,  121,    0,   36,    0,
    0,   35,    0,    0,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   21,    0,    0,    0,   55,    0,   91,    0,    0,   22,
   34,    0,   36,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   35,    0,    0,    0,    0,
   41,    0,    0,    0,    0,  142,  143,  144,  202,  145,
  146,  147,    0,  148,   34,    0,    0,    0,   54,    0,
    0,    0,  203,   35,  204,    0,   36,  149,    0,    0,
    0,    0,    0,    0,   54,    0,    0,  150,    0,    0,
    0,    0,    0,  142,  143,  144,    0,  145,  146,  147,
    0,  148,    0,    0,   36,    0,   35,    0,   34,    0,
    0,    0,  229,   54,    0,  149,   54,   54,   54,   54,
   54,   54,   54,    0,    0,  150,    0,    0,    0,    0,
    0,    0,    0,  107,  108,   35,   34,   36,    0,  620,
    0,    0,   22,  109,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,   55,
    0,    0,    0,    0,    0,    0,   36,    0,   35,   34,
    0,  107,  108,    0,    0,   55,    0,    0,    0,    0,
   22,  109,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,   34,   36,
    0,    0,    0,    0,   55,  107,  210,   55,   55,   55,
   55,   55,   55,   55,   22,  211,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,   34,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,  579,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,   21,    0,  386,
  387,    0,    0,    0,    0,    0,   22,  608,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,    0,    0,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,    0,    0,
    0,    0,  107,    0,  235,  235,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,  388,  389,  390,
  391,    0,    0,    0,    0,    0,  392,  393,  394,  395,
  396,  397,  398,  399,  400,  401,  235,  235,  235,    0,
    0,    0,    0,    0,    0,    0,    0,  235,    0,    0,
  235,  235,  235,  235,  235,  235,  235,  235,  235,    0,
    0,  235,  235,    0,    0,  235,  235,  235,  235,  235,
  235,  225,  225,  235,  235,  235,    0,    0,    0,  235,
    0,    0,    0,  235,  235,  235,    0,    0,    0,  235,
  235,  235,  235,  235,    0,  235,    0,  235,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  235,    0,
    0,    0,    0,  225,  225,  225,    0,    0,    0,  235,
  235,  235,  235,    0,  225,    0,    0,  225,  225,  225,
  225,  225,  225,  225,  225,  225,    0,    0,  225,  225,
    0,    0,  225,  225,  225,  225,  225,  225,  211,  211,
  225,  225,  225,    0,    0,    0,  225,    0,    0,    0,
  225,  225,  225,    0,    0,    0,  225,  225,  225,  225,
  225,    0,  225,    0,  225,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  225,    0,    0,    0,    0,
  211,  211,  211,    0,    0,    0,  225,  225,  225,  225,
    0,  211,    0,    0,  211,  211,  211,  211,  211,  211,
  211,  211,  211,    0,    0,  211,  211,    0,    0,  211,
  211,  211,  211,  211,  211,  226,  226,  211,  211,  211,
    0,    0,    0,  211,    0,    0,    0,  211,  211,  211,
    0,    0,    0,  211,  211,  211,  211,  211,    0,  211,
    0,  211,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  211,    0,    0,    0,    0,  226,  226,  226,
    0,    0,    0,  211,  211,  211,  211,    0,  226,    0,
    0,  226,  226,  226,  226,  226,  226,  226,  226,  226,
    0,    0,  226,  226,    0,    0,  226,  226,  226,  226,
  226,  226,  213,  213,  226,  226,  226,    0,    0,    0,
  226,    0,    0,    0,  226,  226,  226,    0,    0,    0,
  226,  226,  226,  226,  226,    0,  226,    0,  226,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  226,
    0,    0,    0,    0,  213,  213,  213,    0,    0,    0,
  226,  226,  226,  226,    0,  213,    0,    0,  213,  213,
  213,  213,  213,  213,  213,  213,  213,    0,    0,  213,
  213,    0,    0,  213,  213,  213,  213,  213,  213,  215,
  215,  213,  213,  213,    0,    0,    0,  213,    0,    0,
    0,  213,  213,  213,    0,    0,    0,  213,  213,  213,
  213,  213,    0,  213,    0,  213,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  213,    0,    0,    0,
    0,  215,  215,  215,    0,    0,    0,  213,  213,  213,
  213,    0,  215,    0,    0,  215,  215,  215,  215,  215,
  215,  215,  215,  215,    0,    0,  215,  215,    0,    0,
  215,  215,  215,  215,  215,  215,  214,  214,  215,  215,
  215,    0,    0,    0,  215,    0,    0,    0,  215,  215,
  215,    0,    0,    0,  215,  215,  215,  215,  215,    0,
  215,    0,  215,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  215,    0,    0,    0,    0,  214,  214,
  214,    0,    0,    0,  215,  215,  215,  215,    0,  214,
    0,    0,  214,  214,  214,  214,  214,  214,  214,  214,
  214,    0,    0,  214,  214,    0,    0,  214,  214,  214,
  214,  214,  214,  269,    0,  214,  214,  214,    0,    0,
    0,  214,    0,    0,    0,  214,  214,  214,    0,    0,
    0,  214,  214,  214,  214,  214,    0,  214,    0,  214,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  214,    0,    0,    0,    0,  270,  271,  272,    0,    0,
    0,  214,  214,  214,  214,    0,  273,    0,    0,  274,
  275,  276,  277,  278,  279,  280,  281,  282,    0,    0,
  283,  284,    0,    0,  285,  286,  287,  288,  289,  290,
  173,    0,  291,  292,  293,    0,    0,    0,  294,    0,
    0,    0,  295,  296,  297,    0,    0,    0,  298,  299,
  300,  301,  302,    0,  303,    0,  304,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  305,    0,    0,
    0,    0,  173,  173,  173,    0,    0,    0,  306,  307,
  308,  309,    0,  173,    0,    0,  173,  173,  173,  173,
  173,  173,  173,  173,  173,    0,    0,  173,  173,    0,
    0,  173,  173,  173,  173,  173,  173,  174,    0,  173,
  173,  173,    0,    0,    0,  173,    0,    0,    0,  173,
  173,  173,    0,    0,    0,  173,  173,  173,  173,  173,
    0,  173,    0,  173,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  173,    0,    0,    0,    0,  174,
  174,  174,    0,    0,    0,  173,  173,  173,  173,    0,
  174,    0,    0,  174,  174,  174,  174,  174,  174,  174,
  174,  174,    0,    0,  174,  174,    0,    0,  174,  174,
  174,  174,  174,  174,  175,    0,  174,  174,  174,    0,
    0,    0,  174,    0,    0,    0,  174,  174,  174,    0,
    0,    0,  174,  174,  174,  174,  174,    0,  174,    0,
  174,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  174,    0,    0,    0,    0,  175,  175,  175,    0,
    0,    0,  174,  174,  174,  174,    0,  175,    0,    0,
  175,  175,  175,  175,  175,  175,  175,  175,  175,    0,
    0,  175,  175,    0,    0,  175,  175,  175,  175,  175,
  175,  176,    0,  175,  175,  175,    0,    0,    0,  175,
    0,    0,    0,  175,  175,  175,    0,    0,    0,  175,
  175,  175,  175,  175,    0,  175,    0,  175,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  175,    0,
    0,    0,    0,  176,  176,  176,    0,    0,    0,  175,
  175,  175,  175,    0,  176,    0,    0,  176,  176,  176,
  176,  176,  176,  176,  176,  176,    0,    0,  176,  176,
    0,    0,  176,  176,  176,  176,  176,  176,    0,    0,
  176,  176,  176,    0,    0,    0,  176,    0,    0,    0,
  176,  176,  176,    0,    0,    0,  176,  176,  176,  176,
  176,    0,  176,    0,  176,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  176,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  176,  176,  176,  176,
  273,    0,    0,  274,  275,  276,  277,  278,  279,  280,
  281,  282,    0,    0,  283,  284,    0,    0,  285,  286,
  287,  288,  289,  290,    0,    0,  291,  292,  293,    0,
    0,    0,  294,    0,    0,    0,  295,  296,  297,    0,
    0,    0,  298,  299,  300,  301,  302,    0,  303,    0,
  304,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  305,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  306,  307,  308,  309,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  183,  241,  427,   98,  176,    6,    7,   40,   33,
   33,  123,  123,   40,  185,  123,  308,   44,  189,  123,
  310,   60,   40,   40,   31,   61,  123,   34,   42,  152,
   40,   31,   40,  125,   41,   44,  123,   42,  328,   52,
   53,   41,   44,   40,  474,  475,  123,   44,  123,  573,
  123,  222,   91,   44,  225,  226,   42,   44,   61,   66,
  154,   68,  156,  487,  177,   61,   66,  274,    0,   76,
   42,   96,   41,  244,   41,   44,   62,   44,  602,   86,
   87,   88,   61,   90,  123,  583,  584,  338,   93,   96,
  115,  342,   99,  100,  345,  266,  306,  191,  125,  306,
  217,  352,  196,  128,  217,  535,  223,   83,  115,  122,
  223,   61,    0,  236,  227,  407,  125,   44,  616,   91,
  127,  128,  260,  125,  414,  266,  416,  276,  125,  419,
  106,  280,   61,  250,  125,   62,    0,  250,  125,  252,
  253,  258,  257,  284,   40,  152,  280,  241,  265,  174,
  257,  275,  265,  277,   41,  292,  293,   44,   42,  283,
   44,  255,  274,  274,   60,  589,  274,  174,  260,  306,
  274,   41,  185,   42,   44,  151,  189,  274,   61,  186,
   41,   40,   41,   44,  276,   44,  260,  274,  164,  479,
   42,  301,   44,  483,  484,  619,  313,  274,  315,  274,
  260,  274,  215,   40,   41,   41,  123,   44,   44,  306,
  327,  123,   41,  305,  327,   44,  308,  309,  310,  311,
  312,  313,  314,   41,   40,  264,   44,  123,   42,  236,
   44,  306,  245,  306,  273,  242,   41,   40,  214,   44,
  272,    0,  281,  282,  283,  284,  285,  286,  287,  288,
  274,  274,  542,  277,  272,  272,   41,  264,  234,   44,
   41,  112,  272,   44,  272,  274,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  270,  271,  274,  291,  292,  267,  294,  302,  303,
  304,  305,   41,  410,  270,   44,  272,  296,  297,  306,
  307,  308,   41,  342,  267,   44,   41,  424,  308,   44,
  292,  293,  288,  289,  290,   41,   40,  293,   44,  295,
  296,  297,  298,  299,  300,  301,  302,  303,  123,   41,
   41,  338,   44,   44,   40,  342,  268,  269,  345,   41,
  272,  273,   44,  275,   40,  352,  353,  323,   40,  302,
  303,  304,  305,   41,   41,   42,   44,  364,  290,  291,
  123,  257,  258,  259,   40,  261,  262,  263,   60,  265,
   42,   42,   44,   44,   40,  307,  272,  273,  385,   58,
  268,  269,   41,  279,  272,  273,   40,  275,  627,   58,
  292,  293,   42,  289,   44,  402,  298,  299,  300,  406,
  407,   60,  290,  291,  268,  269,  406,  407,  272,  273,
   61,  275,   77,   78,   40,   44,  274,  360,   40,  307,
  306,  260,  359,   44,  359,   40,  290,  291,  610,   40,
  274,  123,   91,  257,  359,  257,  367,  257,   61,  274,
  274,  612,  400,  307,   40,   41,  453,  454,   44,  274,
  457,  458,  459,  460,  461,  462,  463,  464,  465,   44,
  273,   44,  358,  634,  123,   44,   44,   44,  367,  476,
  367,   60,  367,  449,  450,  451,  476,  373,  367,   44,
  456,  332,  367,   44,  266,  367,  337,  367,  367,  340,
  341,  367,  343,  344,   44,  346,  347,  348,  349,  350,
  351,   44,   44,  354,  355,  356,  357,   44,   44,  268,
  269,   44,   44,  272,  273,  609,  275,   44,  631,   44,
   44,   40,   44,  268,  269,  532,  639,  272,  273,  125,
  275,  290,  291,  627,  123,   44,   44,  544,   44,   44,
  516,   44,  359,   44,   40,  290,  291,   91,  307,   44,
   44,   44,   44,  404,   44,  531,   44,   44,  257,   44,
   44,   44,  307,   44,   44,  257,  258,  259,   44,  261,
  262,  263,  579,  265,   91,   40,  583,  584,  429,   40,
  272,  273,  433,  359,  591,  436,   44,  279,  359,  565,
  566,   44,  443,  444,   44,   40,   44,  289,   40,  257,
  257,   44,   93,   40,    0,  264,  613,   40,   10,  616,
   54,   18,  159,  127,  273,  466,  467,  468,  186,  278,
  106,  580,  281,  282,  283,  284,  285,  286,  287,  288,
  105,  607,  587,  140,  312,  454,  295,  312,  489,  329,
  491,  492,  613,  494,  495,  255,  497,  498,  499,  500,
  501,  502,  530,  591,  505,  506,  507,  508,   -1,   -1,
   -1,   -1,  513,   -1,   -1,   -1,  358,   -1,  257,  258,
  259,   -1,  261,  262,  263,   40,  265,  273,  274,   -1,
   -1,  373,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,  546,   -1,   -1,  549,   -1,
  289,  552,   -1,   -1,   -1,   -1,   -1,   -1,  559,  560,
   -1,   -1,   -1,  302,  303,  304,  305,   -1,   -1,  315,
  316,  317,   -1,  574,  575,  576,   -1,   -1,   -1,  580,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,  274,  352,  353,  354,  610,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,  358,
   -1,  367,  368,  369,  370,  371,  372,   -1,  374,   40,
  376,   -1,   -1,   -1,  373,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   40,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  273,  274,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   40,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   -1,  260,  344,
  345,  346,  347,  348,  349,   -1,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,  276,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   40,  376,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,  305,   -1,   -1,  308,  309,  310,  311,
  312,  313,  314,  398,  399,  400,  401,  377,  378,  379,
  380,  381,  382,  383,  384,  385,  386,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,   -1,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  273,
  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,
  371,  372,   -1,  374,   40,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   40,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  273,  274,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
   -1,   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,   60,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,  273,  274,   42,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   91,   60,  398,  399,
  400,  401,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   42,   -1,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,
  326,   60,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,  274,  352,  353,  354,   -1,
  123,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   60,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   60,  123,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,  267,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   91,  344,  345,  346,  347,  348,
  349,   -1,  123,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,  125,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,  123,  376,  316,   -1,
  318,   -1,   -1,   -1,   -1,   -1,   -1,  325,  387,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  398,
  399,  400,  401,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,  125,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  289,   -1,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,   -1,   -1,  302,
  303,  304,  305,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,   -1,   -1,   -1,   -1,   -1,   -1,  415,   -1,   -1,
  289,   41,  420,  421,   -1,  360,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,
   60,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,  273,
   -1,   -1,   -1,   -1,   -1,  358,  125,  264,  289,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,
  373,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,   -1,   -1,  485,   -1,  358,
   -1,  315,  316,  317,  301,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  326,  123,  373,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  358,  352,  353,
  354,   -1,   -1,  125,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,  373,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,   -1,  273,  352,  353,  354,   -1,   -1,
  125,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  387,  261,  262,  263,   -1,  265,  315,  316,  317,   -1,
   -1,  398,  399,  400,  401,   -1,  276,  326,   -1,  279,
  329,  330,  331,  332,  333,  334,  335,  336,  337,  289,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  273,   -1,  352,  353,  354,   -1,  125,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  273,   -1,
  352,  353,  354,   -1,  125,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   -1,  125,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  273,   -1,  352,  353,  354,   -1,  125,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,   -1,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  273,
   -1,  352,  353,  354,   -1,  125,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,   -1,  352,  353,
  354,   -1,  125,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  273,   -1,  352,  353,  354,   -1,  125,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  273,   -1,  352,  353,  354,   -1,  125,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  273,   -1,  352,
  353,  354,   -1,   -1,   60,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   41,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   91,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   60,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   42,   -1,  340,  341,   -1,  123,  344,  345,
  346,  347,  348,  349,  273,   -1,  352,  353,  354,   -1,
   60,   42,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   60,
  376,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,
   -1,  387,   -1,   60,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,  123,   91,  344,  345,  346,  347,  348,
  349,   60,   -1,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,  123,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,  123,  376,   -1,   -1,
   -1,   60,   91,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,   -1,   -1,   60,   -1,  264,   -1,
   -1,   -1,   91,   -1,  123,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   -1,   -1,   91,   -1,   -1,
   -1,  257,  258,  259,  123,  261,  262,  263,   60,  265,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,  123,
   -1,  327,  328,  289,   -1,   -1,   -1,  257,  258,  259,
   -1,  261,  262,  263,   -1,  265,   60,   -1,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,  257,  258,  259,  279,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,  289,
   -1,  123,   -1,   -1,   -1,   -1,   -1,  264,  279,   60,
   -1,   -1,   -1,  125,   -1,   -1,  273,   -1,  289,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,   -1,   -1,  294,   -1,  123,
   91,   -1,  299,  300,   -1,  264,   -1,   60,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,  123,   -1,  125,  264,  295,   -1,   91,   -1,
   -1,   60,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
  264,   -1,   -1,   -1,  125,   -1,  295,   -1,   -1,  273,
  123,   -1,   91,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   60,   -1,   -1,   -1,   -1,
  294,   -1,   -1,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,   -1,  265,  123,   -1,   -1,   -1,  260,   -1,
   -1,   -1,  274,   60,  276,   -1,   91,  279,   -1,   -1,
   -1,   -1,   -1,   -1,  276,   -1,   -1,  289,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,   -1,   -1,   91,   -1,   60,   -1,  123,   -1,
   -1,   -1,  276,  305,   -1,  279,  308,  309,  310,  311,
  312,  313,  314,   -1,   -1,  289,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,  265,   60,  123,   91,   -1,   93,
   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,  260,
   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,   60,  123,
   -1,  264,  265,   -1,   -1,  276,   -1,   -1,   -1,   -1,
  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,  123,   91,
   -1,   -1,   -1,   -1,  305,  264,  265,  308,  309,  310,
  311,  312,  313,  314,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,   -1,   -1,  264,   -1,  261,
  262,   -1,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,  379,  380,  381,
  382,   -1,   -1,   -1,   -1,   -1,  388,  389,  390,  391,
  392,  393,  394,  395,  396,  397,  315,  316,  317,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  273,  274,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  273,  274,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  273,  274,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,   -1,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  273,
  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  273,   -1,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  273,   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,  273,   -1,  352,
  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,   -1,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  273,   -1,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,   -1,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  398,  399,  400,  401,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,   -1,   -1,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  398,  399,  400,  401,
  };

#line 933 "Repil/IR/IR.jay"

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
  public const int DEFINE = 290;
  public const int DECLARE = 291;
  public const int UNNAMED_ADDR = 292;
  public const int LOCAL_UNNAMED_ADDR = 293;
  public const int NOALIAS = 294;
  public const int ELLIPSIS = 295;
  public const int GLOBAL = 296;
  public const int CONSTANT = 297;
  public const int PRIVATE = 298;
  public const int INTERNAL = 299;
  public const int EXTERNAL = 300;
  public const int FASTCC = 301;
  public const int NONNULL = 302;
  public const int NOCAPTURE = 303;
  public const int WRITEONLY = 304;
  public const int READONLY = 305;
  public const int ATTRIBUTE_GROUP_REF = 306;
  public const int ATTRIBUTES = 307;
  public const int NORECURSE = 308;
  public const int NOUNWIND = 309;
  public const int READNONE = 310;
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
