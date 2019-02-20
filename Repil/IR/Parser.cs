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
        yyVal = new ArrayConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 489 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 496 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 157:
#line 514 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 521 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 525 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 532 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 554 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 565 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 576 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 580 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 170:
#line 584 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 588 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 172:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 606 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 610 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 176:
#line 614 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 618 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 179:
#line 629 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 640 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 182:
#line 644 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 183:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 184:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 185:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 186:
#line 660 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 187:
#line 664 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 192:
#line 681 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 685 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 691 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 195:
#line 698 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 702 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 709 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 730 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 734 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 738 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 742 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
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
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 757 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 761 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 765 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 214:
#line 777 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 215:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 218:
#line 793 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 797 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 801 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 833 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 231:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 232:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 233:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 865 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 893 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
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
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 913 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 929 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 933 "Repil/IR/IR.jay"
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
   13,   13,   13,   41,   20,   20,   42,   40,   40,   43,
   39,   39,   44,   37,   37,   28,   28,   45,   45,   45,
   45,   46,   46,   48,   48,   48,   48,   50,   51,   51,
   52,   52,   52,   52,   52,   52,   52,   18,   18,   53,
   53,   54,   54,   55,   56,   56,   57,   58,   58,   59,
   59,   29,   47,   47,   47,   47,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,
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
    3,    3,    3,    2,    2,    1,    2,    1,    3,    2,
    1,    3,    1,    1,    3,    1,    2,    2,    3,    1,
    2,    1,    2,    1,    2,    3,    4,    1,    1,    3,
    2,    3,    3,    3,    2,    4,    5,    1,    3,    1,
    1,    1,    3,    5,    1,    2,    3,    1,    2,    1,
    1,    1,    2,    7,    2,    7,    5,    6,    5,    5,
    4,    6,    7,    7,    8,    7,    8,    4,    5,    6,
    5,    5,    4,    4,    5,    6,    7,    6,    6,    7,
    5,    6,    5,    5,    6,    3,    4,    5,    7,    4,
    5,    6,    6,    4,    7,    5,    6,    4,    5,    4,
    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   62,   74,   64,   65,   66,   67,   68,   69,   70,   71,
    0,   23,   22,    0,    0,    0,   63,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  105,  106,   24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   73,  202,    0,    0,    0,    0,    0,
    0,    5,    6,   20,   21,    0,    0,    0,    7,    0,
    0,    0,    0,    0,   58,    0,    0,    0,    0,    0,
   80,    0,    0,   77,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   25,    0,    0,    0,   43,   42,   12,
    0,    0,   36,   41,    0,    0,    0,    0,    0,   97,
   98,    0,    0,    0,   93,   72,    0,    0,    0,    0,
    0,   56,   46,   47,   48,   49,   50,   51,   52,    0,
   44,  144,  145,  143,  146,  147,  148,  142,  150,  149,
    0,    0,    0,    0,    0,    0,    0,    0,   14,    0,
    0,    0,   37,   13,    0,  138,  137,    0,    0,  136,
  155,    0,   75,   76,    0,  109,    0,    0,  107,  101,
  102,  104,  103,    0,   99,    0,    0,   78,    0,    0,
    0,    0,   11,   45,  158,    0,    0,    0,  161,    0,
    0,    0,    0,   29,    0,   27,   30,   31,   26,   16,
   15,   40,   39,   38,    0,    0,    0,    0,    0,    0,
    0,  108,  100,    0,    0,   94,    0,    0,    0,   53,
  191,  190,    0,  188,  153,    0,  160,    0,  151,  152,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   34,
    0,    0,    0,    0,    0,    0,   57,    0,  159,  162,
    0,    0,   19,   33,    0,    0,    0,    0,    0,    0,
   35,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  166,    0,    0,  172,    0,    0,    0,
    0,  189,    0,   18,   32,    0,    0,    0,    0,    0,
    0,    0,  205,    0,    0,  203,    0,  200,  201,    0,
    0,  198,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  110,  111,  112,
  113,  114,  115,  116,  117,  118,  119,    0,  120,  121,
  132,  133,  134,  135,  123,  125,  126,  127,  128,  124,
  122,  130,  131,  129,    0,    0,    0,    0,    0,    0,
   84,  167,    0,  173,    0,    0,    0,    0,    0,    0,
  139,    0,    0,    0,    0,   83,    0,  154,    0,    0,
    0,    0,  199,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  192,    0,  178,    0,    0,    0,    0,    0,
   81,    0,   82,    0,   86,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  218,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   85,  163,
    0,  164,   87,   88,    0,    0,    0,  207,    0,  219,
  246,    0,  225,  234,    0,  222,  249,  238,  221,  251,
  241,    0,    0,  231,  210,  233,  252,    0,    0,  209,
  141,  157,    0,    0,    0,    0,    0,    0,    0,  193,
    0,    0,    0,    0,  179,    0,    0,    0,  140,    0,
   89,    0,    0,    0,  195,  208,  247,  235,  242,  232,
  229,  243,    0,    0,    0,    0,  228,  220,    0,    0,
    0,    0,    0,  181,    0,    0,    0,    0,    0,  165,
  204,    0,  206,  196,  230,  245,    0,  194,  239,    0,
  183,  184,  182,    0,  180,  213,    0,    0,  197,  186,
    0,    0,  217,  187,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  140,  111,  103,   51,
   76,  112,  170,  221,   52,   39,  104,  233,  113,  540,
  141,   60,   61,   93,   94,  124,  178,  313,   66,  125,
  184,  185,  179,  388,  405,  475,  541,  572,  198,  196,
  336,  517,  199,  542,  314,  315,  316,  317,  318,  476,
  584,  585,  234,  472,  473,  594,  595,  341,  342,
  };
  protected static readonly short [] yySindex = {          288,
    3, -185,   75,   88,   97, 2705, 2811, -140,    0,  288,
    0,    0,    0,    0,  -74,  140,  152,  -51,  -40,  -23,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3098,    0,    0, 3098,   29,   45,    0,  202,  -44,  -29,
 3098,  -27,  266,    0,    0,   80,   85,    0,    0,    0,
 -117,  -73,  -73,  231,  318,  -24,  246,  -26,  202,   -7,
  332,  113,  121,    0,    0, 3098,  344, 2785,  -16,  347,
  271,    0,    0,    0,    0, 3098, -117, -117,    0, -121,
  361,  279, 2892,  365,    0, 3098, 3098, 3098,  -13, 2554,
    0,  202,   30,    0,  366, 2752,  -83, 2660, 3098, 3098,
  354,  357,   35,    0, -121, 2920,    0,    0,    0,    0,
  -14, 1375,    0,    0, 2752,  202,   41,    6,  369,    0,
    0,  -95,  -36,   64,    0,    0, 2785, 2752,   83,  353,
  376,    0,    0,    0,    0,    0,    0,    0,    0,  295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3123, 3098, 3098,  373, 2660,   72, 2849,  144,    0, -121,
  120,  -10,    0,    0, 2953,    0,    0,   59,  388,    0,
    0,  134,    0,    0, 2752,    0,  124, -234,    0,    0,
    0,    0,    0,   -9,    0,  -95, 2752,    0,  166,  -95,
  171, 2883,    0,    0,    0,   -5, 2660,   69,    0,   11,
   73,  390,   76,    0,  398,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  399, 3123,  -73,  174, -234,  170,
 -102,    0,    0,  124, -234,    0,  124,  124,  124,    0,
    0,    0,  193,    0,    0, 3123,    0, 3098,    0,    0,
  183,   86,  185, 1565, 3098,   79,  124,  -73,  -87,    0,
  173, 3655, -101,  -85,  124,  124,    0, 2883,    0,    0,
  175,  191,    0,    0,  221,  194, 3098, -100,  124, 3655,
    0,  403, 3123, -235, 3123, 2506, 3098, 2506, 3098, 2506,
 3098, 3098, 3098, 3098, 3098, 2506, 1293, 3098, 3098, 3098,
 3123, 3123, 3123, 3098, 3098, 3123,  405, 3123, 3123, 3123,
 3123, 3123, 3123, 3123, 3123, 3123,  679, 3033, 3098, 3098,
 2678,   66, 1438,    0, 3655,  175,    0,  175, 3655,  -82,
 3655,    0,  198,    0,    0, 3123,  265, 3655,  -80,  -90,
 1516, 4039,    0,  195,  459,    0,  430,    0,    0, 1375,
 2506,    0, 1375, 1375, 2506, 1375, 1375, 2506, 1375, 1375,
 1375, 1375, 1375, 1375, 2506, 3098, 1375, 1375, 1375, 1375,
  431,  435,  438,  302,  326,  439, 3098,  351,  118,  122,
  123,  125,  126,  132,  139,  145,  146,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 3098,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 3098,   21, 1375,   94, 3098, 2678,
    0,    0,  175,    0,  198,  198, 1593, 3655, 1670,  440,
    0, 1747, 3655, 3655,  -78,    0,  175,    0,  443,  248,
  473, 1375,    0,  474,  479, 1375,  482,  486, 1375,  487,
  488,  489,  491,  494,  495, 1375, 1375,  496,  500,  501,
  502, 3123, 3123, 3123,  188, 3098, 3098,  356, 3123, 3098,
 3098, 3098, 3098, 3098, 3098, 3098, 3098, 3098, 1375, 1375,
  459,  504,    0,  505,    0,  512,   94,   94, 3098,  198,
    0, 1824,    0, 3123,    0, 1901, 1978, 3655,  198,  248,
  462,  459,  520,  459,  459,  521,  459,  459,  522,  459,
  459,  459,  459,  459,  459,  523,  524,  459,  459,  459,
  459,    0,  525,  526,  315, 1375,  529,  530, 3123,  531,
  202,  202,  202,  202,  202,  202,  202,  202,  202,  533,
  540,  545,  503, 3123, 2990,  551,  552,   94,    0,    0,
  256,    0,    0,    0, 2055,  549, 3098,    0,  459,    0,
    0,  459,    0,    0,  459,    0,    0,    0,    0,    0,
    0,  459,  459,    0,    0,    0,    0, 3123, 3123,    0,
    0,    0,  237,  240,  557, 3123,  459,  459,  459,    0,
  558, 3026, 1340,  257,    0, 2990, 2990,  571,    0, 3123,
    0,  248,  568, 3059,    0,    0,    0,    0,    0,    0,
    0,    0,  358,  359, 3123,  570,    0,    0,  532, 3123,
  577, 2599, 1404,    0,  124, 2990,  270,  287, 2990,    0,
    0,  248,    0,    0,    0,    0,  570,    0,    0, 2622,
    0,    0,    0,  124,    0,    0,  124,  292,    0,    0,
  293,  124,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  618,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  164,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   12,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   98,    0,    0,    0,    0,    0,  579,    0,    0,
    0,    0,    0,    0,    0,    0,  437,    0,    0,    0,
    0,  579,    0,    0,    0,   25,  579,  579,    0,    0,
    0,    0,  168,    0,    0,    0,    0,    0,    0,  363,
 1414,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  579,  579,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  297,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  579,    0,    0,    0,
    0,    0,    0,    0,  319,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   57,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  106,  117,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  579,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 2132,    0, 3732,    0,    0,
    0,    0,  252,    0,    0,    0,  579,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  579,
    0,    0,  579,  579,    0,  579,  579,    0,  579,  579,
  579,  579,  579,  579,    0,    0,  579,  579,  579,  579,
    0,    0,    0,  579,  579,    0,    0,  579,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  579,  579,    0,    0,    0,
    0,    0, 2209,    0, 2286, 3809,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3886,    0,    0,    0,
    0,  579,    0,    0,    0,  579,    0,    0,  579,    0,
    0,    0,    0,    0,    0,  579,  579,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  579,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  579,  579,
    0, 3116,    0,    0,    0,    0,    0,    0,    0, 2363,
    0,    0,    0,    0,    0,    0,    0,    0, 3963,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  579,    0,    0,    0,    0,
  514,  591,  668,  772,  849,  926, 1003, 1107, 1184,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  579,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3193,    0,    0,    0,    0,
  321,  579,    0,    0, 3270,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 3347,    0,    0,    0,
    0,    0,    0, 3424,    0,    0, 3501,    0,    0,    0,
    0, 3578,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  614,  572,    0,    0,    0,    0,  527,  535,  -34,
  299,   -6,  -96, -128,    0,  610,  469, -239,    0,   17,
  490,    0,    1,    0,  507,   48, -174, -253, -291,  445,
   52, -180, -127,    0,    0,  151, -518,    0,  483,    0,
 -401,  180, -223,   51, -287,    0,  323,  327,  312, -431,
 -509,   31,  392,    0,  130,    0,   63, -135, -188,
  };
  protected static readonly short [] yyTable = {            38,
   38,  154,  219,  223,  265,   64,   40,   42,   82,   57,
   68,  225,   68,   68,  260,  229,  331,   77,   78,  409,
  252,  319,  328,   68,   38,  412,   68,   59,  491,  165,
  334,   58,  424,  165,   38,  270,   86,  321,  236,  220,
  418,   69,  423,  412,  488,  536,  537,   64,  335,  253,
  222,   61,  255,  256,  238,   59,   90,  606,  202,   38,
  207,   92,   64,   15,   61,  417,   89,  419,   60,   98,
  126,  176,  268,  127,  422,  159,  617,  618,  160,  116,
  117,  118,   64,  123,   16,   17,  627,  177,  546,  123,
  249,  222,  155,  156,  330,  232,  254,  222,  174,  114,
  237,  222,  173,  240,  186,   91,  588,  187,  123,  638,
  164,  471,  238,   64,  211,  203,   92,   85,  479,  235,
   92,  123,  114,  190,  320,  222,  187,  222,  222,  412,
  239,  412,  323,   68,  412,   18,   59,   61,   79,  329,
  222,   79,  345,  129,  348,  197,  197,  232,   19,   60,
  355,  224,  433,  152,  101,  228,  433,   20,  102,  433,
  210,  232,  172,  160,  482,   43,  433,  195,  123,  486,
  487,  251,  220,  220,  217,  189,  130,  187,   74,   75,
  123,  214,  247,  220,  153,   45,  251,  415,  251,  416,
  621,  251,  131,  251,  412,  251,   48,   49,  412,  412,
   46,  425,  222,   61,  176,  176,  227,   61,   95,  187,
  176,   95,   47,  269,  248,  176,  151,  187,   48,   49,
  639,  132,  218,   61,  133,  134,  135,  136,  137,  138,
  139,  197,  246,  257,  545,   64,  258,  326,  266,   54,
   48,   49,   67,   64,   70,   84,   50,   32,   33,   81,
   55,   17,  259,   56,   61,   95,   65,  412,  119,  163,
  327,  325,  171,  163,  258,  180,  181,  182,  183,  340,
  343,  344,  346,  347,  349,  350,  351,  352,  353,  354,
  357,  358,  359,  360,  480,   62,   61,  364,  365,  333,
  368,  337,  180,  181,  182,  183,  589,  615,  489,  590,
  616,   63,  406,  407,   38,  421,   64,  361,  362,  363,
  636,  408,  366,  616,  369,  370,  371,  372,  373,  374,
  375,  376,  377,  593,   90,   90,   71,  637,   90,   90,
  616,   90,  643,  644,  432,  616,  258,   96,  436,   72,
   96,  439,  420,   64,   73,  455,   90,   90,  446,  447,
  142,  143,  144,   34,  145,  146,  147,   80,  148,   28,
  458,  185,   28,   90,  185,  166,  167,   64,   83,  456,
  593,   68,  149,   91,   91,   99,  100,   91,   91,   87,
   91,  469,  150,   90,   92,   92,   96,   88,   92,   92,
  641,   92,   64,   97,  459,   91,   91,   64,  470,  519,
  105,  106,   38,   38,  115,  128,   92,   92,  175,  477,
  478,  157,   91,  191,  158,  192,  201,  208,  215,  193,
   61,   61,   61,   92,   61,   61,   61,  216,   61,  176,
  230,  241,  223,  242,  243,   61,   61,  244,  245,  261,
  634,  263,   61,  250,  262,  267,  271,  324,  220,  516,
  516,  168,   61,  521,  522,  523,  524,  525,  526,  527,
  528,  529,  642,  332,   35,  410,  169,  428,  512,  513,
  514,  251,   38,  430,  452,  520,   62,  156,  453,  538,
  156,  454,  457,  484,  460,  429,  490,   54,  461,  462,
  431,  463,  464,  434,  435,   36,  437,  438,  465,  440,
  441,  442,  443,  444,  445,  466,  222,  448,  449,  450,
  451,  467,  468,  334,  222,  632,  492,  494,  152,   17,
   17,   61,  495,   17,   17,  497,   17,   34,  583,  498,
  500,  501,  502,  232,  503,  575,   61,  504,  505,  508,
  197,   17,   17,  509,  510,  511,  515,  533,  534,  153,
  581,  535,  547,   61,  130,    1,    2,  474,   17,    3,
    4,  156,    5,  549,  552,  555,  562,  563,  568,  569,
  131,  570,  573,  574,  576,  612,  577,    6,    7,  583,
  583,  151,  493,  578,  601,  602,  496,  197,  579,  499,
  586,  587,  592,  471,    8,  603,  506,  507,  604,  132,
  605,  610,  133,  134,  135,  136,  137,  138,  139,  583,
  619,  622,  583,  590,  625,  626,  630,    1,   61,  530,
  531,  532,   54,   44,  628,   79,  629,   53,  209,  194,
   61,  226,  162,  188,  613,  200,  518,  413,   54,  161,
  620,  414,  548,  427,  550,  551,  635,  553,  554,  322,
  556,  557,  558,  559,  560,  561,  624,    0,  564,  565,
  566,  567,  580,    0,    0,    0,  571,   54,   21,    0,
   54,   54,   54,   54,   54,   54,   54,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,    0,    0,    0,    0,  596,
    0,    0,  597,    0,    0,  598,    0,   61,    0,  156,
  156,    0,  599,  600,    0,  142,  143,  144,    0,  145,
  146,  147,    0,  148,    0,    0,    0,  607,  608,  609,
  166,  167,    0,  614,    0,    0,    0,  149,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  150,    0,    0,
    0,  156,  156,  156,    0,    0,    0,    0,    0,    0,
    0,    0,  156,  633,  367,  156,  156,  156,  156,  156,
  156,  156,  156,  156,    0,    0,  156,  156,    0,    0,
  156,  156,  156,  156,  156,  156,  248,  248,  156,  156,
  156,    0,    0,    0,  156,    0,    0,    0,  156,  156,
  156,    0,    0,  156,  156,  156,  156,  156,  156,    0,
  156,   61,  156,    0,    0,    0,  168,    0,    0,    0,
    0,    0,    0,  156,    0,    0,    0,    0,  248,  248,
  248,  169,    0,    0,  156,  156,  156,  156,    0,  248,
    0,    0,  248,  248,  248,  248,  248,  248,  248,  248,
  248,    0,    0,  248,  248,    0,    0,  248,  248,  248,
  248,  248,  248,  253,  253,  248,  248,  248,    0,    0,
    0,  248,    0,    0,    0,  248,  248,  248,    0,    0,
    0,  248,  248,  248,  248,  248,    0,  248,   61,  248,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  248,    0,    0,    0,    0,  253,  253,  253,    0,    0,
    0,  248,  248,  248,  248,    0,  253,    0,    0,  253,
  253,  253,  253,  253,  253,  253,  253,  253,    0,    0,
  253,  253,    0,    0,  253,  253,  253,  253,  253,  253,
  240,  240,  253,  253,  253,    0,    0,    0,  253,    0,
    0,    0,  253,  253,  253,    0,    0,    0,  253,  253,
  253,  253,  253,    0,  253,   61,  253,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  253,    0,    0,
    0,    0,  240,  240,  240,    0,    0,    0,  253,  253,
  253,  253,    0,  240,    0,    0,  240,  240,  240,  240,
  240,  240,  240,  240,  240,    0,    0,  240,  240,    0,
    0,  240,  240,  240,  240,  240,  240,    0,    0,  240,
  240,  240,    0,    0,    0,  240,    0,    0,    0,  240,
  240,  240,    0,    0,    0,  240,  240,  240,  240,  240,
    0,  240,   61,  240,  223,  223,    0,    0,    0,    0,
    0,    0,    0,    0,  240,  378,  379,  380,  381,  382,
  383,  384,  385,  386,  387,  240,  240,  240,  240,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  223,  223,  223,    0,
    0,    0,    0,    0,    0,    0,    0,  223,    0,    0,
  223,  223,  223,  223,  223,  223,  223,  223,  223,    0,
    0,  223,  223,    0,    0,  223,  223,  223,  223,  223,
  223,  224,  224,  223,  223,  223,    0,    0,    0,  223,
    0,    0,    0,  223,  223,  223,    0,    0,    0,  223,
  223,  223,  223,  223,    0,  223,   61,  223,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  223,    0,
    0,    0,    0,  224,  224,  224,    0,    0,    0,  223,
  223,  223,  223,    0,  224,    0,    0,  224,  224,  224,
  224,  224,  224,  224,  224,  224,    0,    0,  224,  224,
    0,    0,  224,  224,  224,  224,  224,  224,  250,  250,
  224,  224,  224,    0,    0,    0,  224,    0,    0,    0,
  224,  224,  224,    0,    0,    0,  224,  224,  224,  224,
  224,    0,  224,   61,  224,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  224,    0,    0,    0,    0,
  250,  250,  250,    0,    0,    0,  224,  224,  224,  224,
    0,  250,    0,    0,  250,  250,  250,  250,  250,  250,
  250,  250,  250,    0,    0,  250,  250,    0,    0,  250,
  250,  250,  250,  250,  250,  244,  244,  250,  250,  250,
    0,    0,    0,  250,    0,    0,    0,  250,  250,  250,
    0,    0,    0,  250,  250,  250,  250,  250,    0,  250,
    0,  250,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  250,    0,    0,    0,    0,  244,  244,  244,
    0,    0,    0,  250,  250,  250,  250,    0,  244,    0,
    0,  244,  244,  244,  244,  244,  244,  244,  244,  244,
    0,    0,  244,  244,    0,    0,  244,  244,  244,  244,
  244,  244,   35,    0,  244,  244,  244,    0,    0,    0,
  244,    0,    0,    0,  244,  244,  244,    0,    0,    0,
  244,  244,  244,  244,  244,    0,  244,    0,  244,  237,
  237,   64,    0,   36,    0,    0,    0,    0,    0,  244,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  152,
  244,  244,  244,  244,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   34,   64,    0,    0,    0,
    0,  237,  237,  237,    0,    0,    0,    0,    0,    0,
  153,    0,  237,    0,  152,  237,  237,  237,  237,  237,
  237,  237,  237,  237,    0,    0,  237,  237,    0,    0,
  237,  237,  237,  237,  237,  237,  211,  211,  237,  237,
  237,    0,  151,  152,  237,  153,    0,    0,  237,  237,
  237,    0,    0,    0,  237,  237,  237,  237,  237,    0,
  237,    0,  237,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  237,  153,    0,    0,  151,  211,  211,
  211,    0,    0,    0,  237,  237,  237,  237,    0,  211,
    0,    0,  211,  211,  211,  211,  211,  211,  211,  211,
  211,    0,    0,  211,  211,    0,  151,  211,  211,  211,
  211,  211,  211,    0,    0,  211,  211,  211,   55,    0,
    0,  211,    0,    0,    0,  211,  211,  211,    0,    0,
    0,  211,  211,  211,  211,  211,   21,  211,    0,  211,
    0,    0,  411,    0,    0,   22,    0,    0,    0,    0,
  211,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,  211,  211,  211,  211,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  142,  143,  144,    0,
  145,  146,  147,    0,  148,  264,    0,    0,    0,    0,
    0,  166,  167,    0,    0,    0,    0,    0,  149,    0,
    0,    0,    0,    0,  152,    0,    0,    0,  150,    0,
    0,  142,  143,  144,  356,  145,  146,  147,    0,  148,
  426,  180,  181,  182,  183,    0,  166,  167,    0,    0,
    0,    0,    0,  149,    0,  153,    0,    0,    0,    0,
  142,  143,  144,  150,  145,  146,  147,    0,  148,    0,
    0,    0,    0,   55,    0,  166,  167,    0,    0,    0,
    0,    0,  149,    0,    0,    0,    0,  151,    0,   55,
    0,    0,  150,    0,    0,    0,    0,  168,    0,    0,
    0,    0,    0,    0,    0,  180,  181,  182,  183,    0,
  272,    0,  169,    0,    0,    0,    0,  481,   55,    0,
    0,   55,   55,   55,   55,   55,   55,   55,    0,    0,
    0,    0,  168,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  169,    0,    0,
    0,    0,  273,  274,  275,    0,    0,    0,    0,    0,
    0,  168,    0,  276,    0,    0,  277,  278,  279,  280,
  281,  282,  283,  284,  285,    0,  169,  286,  287,    0,
    0,  288,  289,  290,  291,  292,  293,    0,  272,  294,
  295,  296,    0,    0,  483,  297,    0,    0,    0,  298,
  299,  300,    0,    0,    0,  301,  302,  303,  304,  305,
    0,  306,    0,  307,    0,    0,    0,    0,    0,    0,
    0,  142,  143,  144,  308,  145,  146,  147,    0,  148,
  273,  274,  275,    0,    0,  309,  310,  311,  312,    0,
  231,  276,    0,  149,  277,  278,  279,  280,  281,  282,
  283,  284,  285,  150,    0,  286,  287,    0,    0,  288,
  289,  290,  291,  292,  293,  272,    0,  294,  295,  296,
    0,  485,    0,  297,    0,    0,    0,  298,  299,  300,
    0,    0,    0,  301,  302,  303,  304,  305,    0,  306,
    0,  307,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  308,    0,    0,    0,    0,  273,  274,  275,
    0,    0,    0,  309,  310,  311,  312,    0,  276,    0,
    0,  277,  278,  279,  280,  281,  282,  283,  284,  285,
    0,    0,  286,  287,    0,    0,  288,  289,  290,  291,
  292,  293,  272,    0,  294,  295,  296,    0,  539,    0,
  297,    0,    0,    0,  298,  299,  300,    0,    0,    0,
  301,  302,  303,  304,  305,    0,  306,    0,  307,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  308,
    0,    0,    0,    0,  273,  274,  275,    0,    0,    0,
  309,  310,  311,  312,    0,  276,    0,    0,  277,  278,
  279,  280,  281,  282,  283,  284,  285,    0,    0,  286,
  287,    0,    0,  288,  289,  290,  291,  292,  293,  272,
    0,  294,  295,  296,    0,  543,    0,  297,    0,    0,
    0,  298,  299,  300,    0,    0,    0,  301,  302,  303,
  304,  305,    0,  306,    0,  307,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  308,    0,    0,    0,
    0,  273,  274,  275,    0,    0,    0,  309,  310,  311,
  312,    0,  276,    0,    0,  277,  278,  279,  280,  281,
  282,  283,  284,  285,    0,    0,  286,  287,    0,    0,
  288,  289,  290,  291,  292,  293,  272,    0,  294,  295,
  296,    0,  544,    0,  297,    0,    0,    0,  298,  299,
  300,    0,    0,    0,  301,  302,  303,  304,  305,    0,
  306,    0,  307,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  308,    0,    0,    0,    0,  273,  274,
  275,    0,    0,    0,  309,  310,  311,  312,    0,  276,
    0,    0,  277,  278,  279,  280,  281,  282,  283,  284,
  285,    0,    0,  286,  287,    0,    0,  288,  289,  290,
  291,  292,  293,  272,    0,  294,  295,  296,    0,  591,
    0,  297,    0,    0,    0,  298,  299,  300,    0,    0,
    0,  301,  302,  303,  304,  305,    0,  306,    0,  307,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  308,    0,    0,    0,    0,  273,  274,  275,    0,    0,
    0,  309,  310,  311,  312,    0,  276,    0,    0,  277,
  278,  279,  280,  281,  282,  283,  284,  285,    0,    0,
  286,  287,    0,    0,  288,  289,  290,  291,  292,  293,
  272,    0,  294,  295,  296,    0,  170,    0,  297,    0,
    0,    0,  298,  299,  300,    0,    0,    0,  301,  302,
  303,  304,  305,    0,  306,    0,  307,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  308,    0,    0,
    0,    0,  273,  274,  275,    0,    0,    0,  309,  310,
  311,  312,    0,  276,    0,    0,  277,  278,  279,  280,
  281,  282,  283,  284,  285,    0,    0,  286,  287,    0,
    0,  288,  289,  290,  291,  292,  293,  272,    0,  294,
  295,  296,    0,  168,    0,  297,    0,    0,    0,  298,
  299,  300,    0,    0,    0,  301,  302,  303,  304,  305,
    0,  306,    0,  307,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  308,    0,    0,    0,    0,  273,
  274,  275,    0,    0,    0,  309,  310,  311,  312,    0,
  276,    0,    0,  277,  278,  279,  280,  281,  282,  283,
  284,  285,    0,    0,  286,  287,    0,    0,  288,  289,
  290,  291,  292,  293,  170,    0,  294,  295,  296,    0,
  171,    0,  297,    0,    0,    0,  298,  299,  300,    0,
    0,    0,  301,  302,  303,  304,  305,    0,  306,    0,
  307,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  308,    0,    0,    0,    0,  170,  170,  170,    0,
    0,    0,  309,  310,  311,  312,    0,  170,    0,    0,
  170,  170,  170,  170,  170,  170,  170,  170,  170,    0,
    0,  170,  170,    0,    0,  170,  170,  170,  170,  170,
  170,  168,    0,  170,  170,  170,    0,  169,    0,  170,
    0,    0,    0,  170,  170,  170,    0,    0,    0,  170,
  170,  170,  170,  170,    0,  170,    0,  170,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  170,    0,
    0,    0,    0,  168,  168,  168,    0,    0,    0,  170,
  170,  170,  170,    0,  168,    0,    0,  168,  168,  168,
  168,  168,  168,  168,  168,  168,    0,    0,  168,  168,
    0,    0,  168,  168,  168,  168,  168,  168,  171,    0,
  168,  168,  168,    0,    0,   35,  168,    0,    0,    0,
  168,  168,  168,    0,    0,    0,  168,  168,  168,  168,
  168,    0,  168,    0,  168,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  122,  168,   36,    0,    0,    0,
  171,  171,  171,    0,    0,    0,  168,  168,  168,  168,
    0,  171,    0,   35,  171,  171,  171,  171,  171,  171,
  171,  171,  171,    0,    0,  171,  171,    0,   34,  171,
  171,  171,  171,  171,  171,  169,    0,  171,  171,  171,
   64,    0,    0,  171,   36,    0,    0,  171,  171,  171,
    0,    0,    0,  171,  171,  171,  171,  171,  152,  171,
    0,  171,  640,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  171,    0,    0,    0,   34,  169,  169,  169,
    0,  152,    0,  171,  171,  171,  171,    0,  169,  153,
    0,  169,  169,  169,  169,  169,  169,  169,  169,  169,
    0,   64,  169,  169,    0,    0,  169,  169,  169,  169,
  169,  169,  153,    0,  169,  169,  169,    0,    0,  152,
  169,  151,    0,    0,  169,  169,  169,    0,    0,    0,
  169,  169,  169,  169,  169,    0,  169,   35,  169,    0,
    0,    0,    0,    0,  151,    0,    0,    0,    0,  169,
  153,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  169,  169,  169,  169,   35,    0,    0,    0,   36,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,  151,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,   36,    0,    0,    0,    0,
   34,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   35,    0,    0,    0,    0,    0,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,   34,    0,    0,
    0,  120,  338,  339,   23,   24,   25,   26,   27,   28,
   29,   30,   36,    0,   35,    0,    0,    0,  121,    0,
    0,    0,    0,    0,    0,  142,  143,  144,    0,  145,
  146,  147,    0,  148,    0,    0,    0,    0,    0,    0,
   35,  631,    0,    0,   34,   36,    0,  149,  142,  143,
  144,    0,  145,  146,  147,    0,  148,  150,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  231,    0,    0,
  149,   36,    0,    0,    0,    0,    0,   34,  152,    0,
  150,    0,    0,    0,    0,    0,  142,  143,  144,    0,
  145,  146,  147,    0,  148,    0,    0,    0,    0,    0,
    0,    0,    0,   34,    0,    0,    0,    0,  149,  153,
    0,   21,  152,    0,    0,    0,    0,    0,  150,    0,
   22,   35,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,   21,    0,
    0,  151,    0,  153,    0,    0,    0,   22,   65,   35,
    0,    0,   36,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,    0,    0,    0,   31,    0,
    0,    0,    0,   32,   33,  151,    0,    0,    0,    0,
   36,    0,   35,    0,   34,   21,  110,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,  120,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,   34,   36,    0,    0,  121,    0,   21,   35,
    0,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,   21,   34,    0,    0,    0,   91,
   36,    0,    0,   22,    0,   35,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
    0,    0,    0,    0,   41,  142,  143,  144,  204,  145,
  146,  147,   34,  148,    0,    0,   36,    0,   35,    0,
    0,    0,  205,    0,  206,    0,    0,  149,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  150,    0,  142,
  143,  144,    0,  145,  146,  147,    0,  148,   34,   36,
    0,  623,    0,    0,    0,  107,  108,   35,  231,    0,
    0,  149,    0,    0,   22,  109,    0,    0,    0,    0,
    0,  150,   23,   24,   25,   26,   27,   28,   29,   30,
    0,   34,   35,  107,  108,    0,    0,    0,   36,    0,
    0,    0,   22,  109,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,    0,   36,    0,    0,  107,  212,    0,    0,
   34,    0,    0,    0,    0,   22,  213,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,    0,    0,    0,    0,   34,    0,    0,    0,    0,
    0,    0,    0,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,    0,  582,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   21,
    0,    0,    0,  389,  390,    0,    0,    0,   22,  611,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,    0,    0,
    0,    0,   21,    0,    0,    0,    0,    0,    0,    0,
    0,   22,    0,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,  107,    0,  236,  236,
    0,    0,    0,    0,    0,   22,    0,    0,    0,    0,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,  391,  392,  393,  394,    0,    0,    0,    0,    0,
  395,  396,  397,  398,  399,  400,  401,  402,  403,  404,
  236,  236,  236,    0,    0,    0,    0,    0,    0,    0,
    0,  236,    0,    0,  236,  236,  236,  236,  236,  236,
  236,  236,  236,    0,    0,  236,  236,    0,    0,  236,
  236,  236,  236,  236,  236,  226,  226,  236,  236,  236,
    0,    0,    0,  236,    0,    0,    0,  236,  236,  236,
    0,    0,    0,  236,  236,  236,  236,  236,    0,  236,
    0,  236,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  236,    0,    0,    0,    0,  226,  226,  226,
    0,    0,    0,  236,  236,  236,  236,    0,  226,    0,
    0,  226,  226,  226,  226,  226,  226,  226,  226,  226,
    0,    0,  226,  226,    0,    0,  226,  226,  226,  226,
  226,  226,  212,  212,  226,  226,  226,    0,    0,    0,
  226,    0,    0,    0,  226,  226,  226,    0,    0,    0,
  226,  226,  226,  226,  226,    0,  226,    0,  226,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  226,
    0,    0,    0,    0,  212,  212,  212,    0,    0,    0,
  226,  226,  226,  226,    0,  212,    0,    0,  212,  212,
  212,  212,  212,  212,  212,  212,  212,    0,    0,  212,
  212,    0,    0,  212,  212,  212,  212,  212,  212,  227,
  227,  212,  212,  212,    0,    0,    0,  212,    0,    0,
    0,  212,  212,  212,    0,    0,    0,  212,  212,  212,
  212,  212,    0,  212,    0,  212,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  212,    0,    0,    0,
    0,  227,  227,  227,    0,    0,    0,  212,  212,  212,
  212,    0,  227,    0,    0,  227,  227,  227,  227,  227,
  227,  227,  227,  227,    0,    0,  227,  227,    0,    0,
  227,  227,  227,  227,  227,  227,  214,  214,  227,  227,
  227,    0,    0,    0,  227,    0,    0,    0,  227,  227,
  227,    0,    0,    0,  227,  227,  227,  227,  227,    0,
  227,    0,  227,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  227,    0,    0,    0,    0,  214,  214,
  214,    0,    0,    0,  227,  227,  227,  227,    0,  214,
    0,    0,  214,  214,  214,  214,  214,  214,  214,  214,
  214,    0,    0,  214,  214,    0,    0,  214,  214,  214,
  214,  214,  214,  216,  216,  214,  214,  214,    0,    0,
    0,  214,    0,    0,    0,  214,  214,  214,    0,    0,
    0,  214,  214,  214,  214,  214,    0,  214,    0,  214,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  214,    0,    0,    0,    0,  216,  216,  216,    0,    0,
    0,  214,  214,  214,  214,    0,  216,    0,    0,  216,
  216,  216,  216,  216,  216,  216,  216,  216,    0,    0,
  216,  216,    0,    0,  216,  216,  216,  216,  216,  216,
  215,  215,  216,  216,  216,    0,    0,    0,  216,    0,
    0,    0,  216,  216,  216,    0,    0,    0,  216,  216,
  216,  216,  216,    0,  216,    0,  216,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  216,    0,    0,
    0,    0,  215,  215,  215,    0,    0,    0,  216,  216,
  216,  216,    0,  215,    0,    0,  215,  215,  215,  215,
  215,  215,  215,  215,  215,    0,    0,  215,  215,    0,
    0,  215,  215,  215,  215,  215,  215,  272,    0,  215,
  215,  215,    0,    0,    0,  215,    0,    0,    0,  215,
  215,  215,    0,    0,    0,  215,  215,  215,  215,  215,
    0,  215,    0,  215,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  215,    0,    0,    0,    0,  273,
  274,  275,    0,    0,    0,  215,  215,  215,  215,    0,
  276,    0,    0,  277,  278,  279,  280,  281,  282,  283,
  284,  285,    0,    0,  286,  287,    0,    0,  288,  289,
  290,  291,  292,  293,  174,    0,  294,  295,  296,    0,
    0,    0,  297,    0,    0,    0,  298,  299,  300,    0,
    0,    0,  301,  302,  303,  304,  305,    0,  306,    0,
  307,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  308,    0,    0,    0,    0,  174,  174,  174,    0,
    0,    0,  309,  310,  311,  312,    0,  174,    0,    0,
  174,  174,  174,  174,  174,  174,  174,  174,  174,    0,
    0,  174,  174,    0,    0,  174,  174,  174,  174,  174,
  174,  175,    0,  174,  174,  174,    0,    0,    0,  174,
    0,    0,    0,  174,  174,  174,    0,    0,    0,  174,
  174,  174,  174,  174,    0,  174,    0,  174,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  174,    0,
    0,    0,    0,  175,  175,  175,    0,    0,    0,  174,
  174,  174,  174,    0,  175,    0,    0,  175,  175,  175,
  175,  175,  175,  175,  175,  175,    0,    0,  175,  175,
    0,    0,  175,  175,  175,  175,  175,  175,  176,    0,
  175,  175,  175,    0,    0,    0,  175,    0,    0,    0,
  175,  175,  175,    0,    0,    0,  175,  175,  175,  175,
  175,    0,  175,    0,  175,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  175,    0,    0,    0,    0,
  176,  176,  176,    0,    0,    0,  175,  175,  175,  175,
    0,  176,    0,    0,  176,  176,  176,  176,  176,  176,
  176,  176,  176,    0,    0,  176,  176,    0,    0,  176,
  176,  176,  176,  176,  176,  177,    0,  176,  176,  176,
    0,    0,    0,  176,    0,    0,    0,  176,  176,  176,
    0,    0,    0,  176,  176,  176,  176,  176,    0,  176,
    0,  176,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  176,    0,    0,    0,    0,  177,  177,  177,
    0,    0,    0,  176,  176,  176,  176,    0,  177,    0,
    0,  177,  177,  177,  177,  177,  177,  177,  177,  177,
    0,    0,  177,  177,    0,    0,  177,  177,  177,  177,
  177,  177,    0,    0,  177,  177,  177,    0,    0,    0,
  177,    0,    0,    0,  177,  177,  177,    0,    0,    0,
  177,  177,  177,  177,  177,    0,  177,    0,  177,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  177,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  177,  177,  177,  177,  276,    0,    0,  277,  278,  279,
  280,  281,  282,  283,  284,  285,    0,    0,  286,  287,
    0,    0,  288,  289,  290,  291,  292,  293,    0,    0,
  294,  295,  296,    0,    0,    0,  297,    0,    0,    0,
  298,  299,  300,    0,    0,    0,  301,  302,  303,  304,
  305,    0,  306,    0,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  308,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  309,  310,  311,  312,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   98,  177,  184,  244,   42,    6,    7,   33,   33,
   40,  186,   40,   40,  238,  190,  270,   52,   53,  311,
  123,  123,  123,   40,   31,  313,   40,   34,  430,   44,
  266,   31,  123,   44,   41,  123,   44,  123,   44,  274,
  123,   41,  123,  331,  123,  477,  478,   42,  284,  224,
  178,   40,  227,  228,   44,   44,    0,  576,  155,   66,
  157,   68,   42,   61,   40,  319,   66,  321,   44,   76,
   41,  306,  247,   44,  328,   41,  586,  587,   44,   86,
   87,   88,   42,   90,  270,  271,  605,  122,  490,   96,
  219,  219,   99,  100,  269,  192,  225,  225,   93,   83,
  197,  229,   62,   93,   41,    0,  538,   44,  115,  619,
  125,   91,   44,   42,  125,   44,    0,  125,  410,  125,
  127,  128,  106,   41,  253,  253,   44,  255,  256,  417,
   62,  419,  261,   40,  422,   61,  125,   40,   41,  268,
  268,   44,  278,   96,  280,  152,  153,  244,   61,  125,
  286,  186,  341,   60,  276,  190,  345,   61,  280,  348,
   41,  258,  115,   44,  418,  306,  355,  151,  175,  423,
  424,  274,  274,  274,   41,  128,  260,   44,  296,  297,
  187,  165,  217,  274,   91,  260,  274,  316,  274,  318,
  592,  274,  276,  274,  482,  274,  292,  293,  486,  487,
   61,  330,  330,   40,  306,  306,   41,   40,   41,   44,
  306,   44,   61,  248,   41,  306,  123,   44,  292,  293,
  622,  305,  175,   60,  308,  309,  310,  311,  312,  313,
  314,  238,  216,   41,  488,   42,   44,   44,  245,  280,
  292,  293,  272,   42,  272,  272,  298,  299,  300,  274,
  274,    0,  236,  277,   91,  272,  301,  545,  272,  274,
  267,   41,  112,  274,   44,  302,  303,  304,  305,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  413,  257,  123,  294,  295,  273,
  297,  275,  302,  303,  304,  305,   41,   41,  427,   44,
   44,  257,  309,  310,  311,   41,   42,  291,  292,  293,
   41,  311,  296,   44,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  547,  268,  269,   61,   41,  272,  273,
   44,  275,   41,   41,  341,   44,   44,   41,  345,  260,
   44,  348,  326,   42,  260,   44,  290,  291,  355,  356,
  257,  258,  259,  123,  261,  262,  263,   40,  265,   41,
  367,   41,   44,  307,   44,  272,  273,   42,  123,   44,
  594,   40,  279,  268,  269,   77,   78,  272,  273,  267,
  275,  388,  289,   40,  268,  269,   40,  267,  272,  273,
  630,  275,   42,  123,   44,  290,  291,   42,  405,   44,
   40,  123,  409,  410,   40,   40,  290,  291,   40,  409,
  410,   58,  307,   61,   58,   40,   44,  274,  360,  125,
  257,  258,  259,  307,  261,  262,  263,   40,  265,  306,
  260,  359,  613,   44,  359,  272,  273,   40,   40,  257,
  615,  257,  279,  274,  359,  367,  274,  257,  274,  456,
  457,  358,  289,  460,  461,  462,  463,  464,  465,  466,
  467,  468,  637,   61,   60,  400,  373,  273,  452,  453,
  454,  274,  479,   44,   44,  459,   40,   41,   44,  479,
   44,   44,   44,   44,  367,  335,   44,  125,  367,  367,
  340,  367,  367,  343,  344,   91,  346,  347,  367,  349,
  350,  351,  352,  353,  354,  367,  634,  357,  358,  359,
  360,  367,  367,  266,  642,  612,   44,   44,   60,  268,
  269,  358,   44,  272,  273,   44,  275,  123,  535,   44,
   44,   44,   44,  630,   44,  519,  373,   44,   44,   44,
  547,  290,  291,   44,   44,   44,  359,   44,   44,   91,
  534,   40,   91,   40,  260,  268,  269,  407,  307,  272,
  273,  125,  275,   44,   44,   44,   44,   44,   44,   44,
  276,  257,   44,   44,   44,  582,   44,  290,  291,  586,
  587,  123,  432,   44,  568,  569,  436,  594,   44,  439,
   40,   40,   44,   91,  307,  359,  446,  447,  359,  305,
   44,   44,  308,  309,  310,  311,  312,  313,  314,  616,
   40,   44,  619,   44,  257,  257,   40,    0,   40,  469,
  470,  471,  260,   10,   93,   54,  610,   18,  160,  140,
   40,  187,  106,  127,  583,  153,  457,  315,  276,  105,
  590,  315,  492,  332,  494,  495,  616,  497,  498,  258,
  500,  501,  502,  503,  504,  505,  594,   -1,  508,  509,
  510,  511,  533,   -1,   -1,   -1,  516,  305,  264,   -1,
  308,  309,  310,  311,  312,  313,  314,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,  549,
   -1,   -1,  552,   -1,   -1,  555,   -1,   40,   -1,  273,
  274,   -1,  562,  563,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,  577,  578,  579,
  272,  273,   -1,  583,   -1,   -1,   -1,  279,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  289,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  326,  613,  360,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,  367,  368,  369,  370,  371,  372,   -1,
  374,   40,  376,   -1,   -1,   -1,  358,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,  373,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,
  347,  348,  349,  273,  274,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   40,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   40,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,   -1,   -1,  352,
  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   40,  376,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,  377,  378,  379,  380,  381,
  382,  383,  384,  385,  386,  398,  399,  400,  401,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,
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
  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  273,  274,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,   60,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,  273,
  274,   42,   -1,   91,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   60,
  398,  399,  400,  401,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  123,   42,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,
   91,   -1,  326,   -1,   60,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,  123,   60,  358,   91,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   91,   -1,   -1,  123,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,  123,  344,  345,  346,
  347,  348,  349,   -1,   -1,  352,  353,  354,  125,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,  264,  374,   -1,  376,
   -1,   -1,  125,   -1,   -1,  273,   -1,   -1,   -1,   -1,
  387,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  398,  399,  400,  401,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   41,   -1,   -1,   -1,   -1,
   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,
   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,  289,   -1,
   -1,  257,  258,  259,  342,  261,  262,  263,   -1,  265,
  125,  302,  303,  304,  305,   -1,  272,  273,   -1,   -1,
   -1,   -1,   -1,  279,   -1,   91,   -1,   -1,   -1,   -1,
  257,  258,  259,  289,  261,  262,  263,   -1,  265,   -1,
   -1,   -1,   -1,  260,   -1,  272,  273,   -1,   -1,   -1,
   -1,   -1,  279,   -1,   -1,   -1,   -1,  123,   -1,  276,
   -1,   -1,  289,   -1,   -1,   -1,   -1,  358,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  302,  303,  304,  305,   -1,
  273,   -1,  373,   -1,   -1,   -1,   -1,  125,  305,   -1,
   -1,  308,  309,  310,  311,  312,  313,  314,   -1,   -1,
   -1,   -1,  358,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  373,   -1,   -1,
   -1,   -1,  315,  316,  317,   -1,   -1,   -1,   -1,   -1,
   -1,  358,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,  373,  340,  341,   -1,
   -1,  344,  345,  346,  347,  348,  349,   -1,  273,  352,
  353,  354,   -1,   -1,  125,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  257,  258,  259,  387,  261,  262,  263,   -1,  265,
  315,  316,  317,   -1,   -1,  398,  399,  400,  401,   -1,
  276,  326,   -1,  279,  329,  330,  331,  332,  333,  334,
  335,  336,  337,  289,   -1,  340,  341,   -1,   -1,  344,
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
  353,  354,   -1,  125,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,   -1,  352,  353,  354,   -1,
  125,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,  273,   -1,  352,  353,  354,   -1,  125,   -1,  358,
   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,  398,
  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,  273,   -1,
  352,  353,  354,   -1,   -1,   60,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   41,  387,   91,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   60,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   -1,  123,  344,
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   42,   -1,   -1,  358,   91,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   60,  374,
   -1,  376,   41,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,  123,  315,  316,  317,
   -1,   60,   -1,  398,  399,  400,  401,   -1,  326,   91,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   42,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,   91,   -1,  352,  353,  354,   -1,   -1,   60,
  358,  123,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   60,  376,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  387,
   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  398,  399,  400,  401,   60,   -1,   -1,   -1,   91,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,  123,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,   91,   -1,   -1,   -1,   -1,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,   -1,   -1,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  123,   -1,   -1,
   -1,  278,  327,  328,  281,  282,  283,  284,  285,  286,
  287,  288,   91,   -1,   60,   -1,   -1,   -1,  295,   -1,
   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,
   60,  273,   -1,   -1,  123,   91,   -1,  279,  257,  258,
  259,   -1,  261,  262,  263,   -1,  265,  289,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  276,   -1,   -1,
  279,   91,   -1,   -1,   -1,   -1,   -1,  123,   60,   -1,
  289,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  279,   91,
   -1,  264,   60,   -1,   -1,   -1,   -1,   -1,  289,   -1,
  273,   60,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,  264,   -1,
   -1,  123,   -1,   91,   -1,   -1,   -1,  273,  301,   60,
   -1,   -1,   91,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,   -1,   -1,   -1,   -1,  294,   -1,
   -1,   -1,   -1,  299,  300,  123,   -1,   -1,   -1,   -1,
   91,   -1,   60,   -1,  123,  264,  125,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,  123,   91,   -1,   -1,  295,   -1,  264,   60,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,   -1,  264,  123,   -1,   -1,   -1,  295,
   91,   -1,   -1,  273,   -1,   60,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,   -1,
   -1,   -1,   -1,   -1,  294,  257,  258,  259,  260,  261,
  262,  263,  123,  265,   -1,   -1,   91,   -1,   60,   -1,
   -1,   -1,  274,   -1,  276,   -1,   -1,  279,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  289,   -1,  257,
  258,  259,   -1,  261,  262,  263,   -1,  265,  123,   91,
   -1,   93,   -1,   -1,   -1,  264,  265,   60,  276,   -1,
   -1,  279,   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,  289,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,  123,   60,  264,  265,   -1,   -1,   -1,   91,   -1,
   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   -1,   -1,   91,   -1,   -1,  264,  265,   -1,   -1,
  123,   -1,   -1,   -1,   -1,  273,  274,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,  261,  262,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,  264,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  379,  380,  381,  382,   -1,   -1,   -1,   -1,   -1,
  388,  389,  390,  391,  392,  393,  394,  395,  396,  397,
  315,  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
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
  347,  348,  349,  273,  274,  352,  353,  354,   -1,   -1,
   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
  273,  274,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
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
   -1,   -1,  344,  345,  346,  347,  348,  349,  273,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,
   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,   -1,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  398,  399,  400,  401,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,   -1,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,
  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  398,  399,  400,  401,
  };

#line 937 "Repil/IR/IR.jay"

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
