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
case 141:
#line 465 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 143:
#line 473 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 144:
#line 474 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 145:
#line 475 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 146:
#line 476 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 147:
#line 477 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 148:
#line 478 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 149:
#line 479 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 150:
#line 480 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 151:
#line 481 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 152:
#line 485 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 489 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 493 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 500 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 157:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 158:
#line 518 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 525 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 529 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 536 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 547 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 562 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 573 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 580 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 584 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 171:
#line 588 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 592 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 173:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 610 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 614 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 618 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 622 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 180:
#line 633 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 637 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 644 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 183:
#line 648 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 184:
#line 652 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 185:
#line 656 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 186:
#line 660 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 187:
#line 664 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 188:
#line 668 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 193:
#line 685 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 689 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 695 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 196:
#line 702 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 706 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 713 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 734 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 738 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 742 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 746 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
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
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 761 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 765 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 769 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 215:
#line 781 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 216:
#line 785 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
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
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 219:
#line 797 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 801 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 805 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 809 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 813 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 817 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 821 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 825 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 837 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 841 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 845 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 232:
#line 849 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 233:
#line 853 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 234:
#line 857 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
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
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 869 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 873 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 877 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 881 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 885 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
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
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 897 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 901 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 905 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
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
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 917 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 921 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 925 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 929 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 933 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 937 "Repil/IR/IR.jay"
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
   36,   38,   13,   13,   13,   13,   13,   13,   13,   13,
   13,   13,   13,   13,   41,   20,   20,   42,   40,   40,
   43,   39,   39,   44,   37,   37,   28,   28,   45,   45,
   45,   45,   46,   46,   48,   48,   48,   48,   50,   51,
   51,   52,   52,   52,   52,   52,   52,   52,   18,   18,
   53,   53,   54,   54,   55,   56,   56,   57,   58,   58,
   59,   59,   29,   47,   47,   47,   47,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,
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
    6,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    2,    2,    1,    2,    1,    3,
    2,    1,    3,    1,    1,    3,    1,    2,    2,    3,
    1,    2,    1,    2,    1,    2,    3,    4,    1,    1,
    3,    2,    3,    3,    3,    2,    4,    5,    1,    3,
    1,    1,    1,    3,    5,    1,    2,    3,    1,    2,
    1,    1,    1,    2,    7,    2,    7,    5,    6,    5,
    5,    4,    6,    7,    7,    8,    7,    8,    4,    5,
    6,    5,    5,    4,    4,    5,    6,    7,    6,    6,
    7,    5,    6,    5,    5,    6,    3,    4,    5,    7,
    4,    5,    6,    6,    4,    7,    5,    6,    4,    5,
    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   62,   74,   64,   65,   66,   67,   68,   69,   70,   71,
    0,   23,   22,    0,    0,    0,   63,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  105,  106,   24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   73,  203,    0,    0,    0,    0,    0,
    0,    5,    6,   20,   21,    0,    0,    0,    7,    0,
    0,    0,    0,    0,   58,    0,    0,    0,    0,    0,
   80,    0,    0,   77,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   25,    0,    0,    0,   43,   42,   12,
    0,    0,   36,   41,    0,    0,    0,    0,    0,   97,
   98,    0,    0,    0,   93,   72,    0,    0,    0,    0,
    0,   56,   46,   47,   48,   49,   50,   51,   52,    0,
   44,  145,  146,  144,  147,  148,  149,  143,  151,  150,
    0,    0,    0,    0,    0,    0,    0,    0,   14,    0,
    0,    0,   37,   13,    0,  138,  137,    0,    0,    0,
  136,  156,    0,   75,   76,    0,  109,    0,    0,  107,
  101,  102,  104,  103,    0,   99,    0,    0,   78,    0,
    0,    0,    0,   11,   45,  159,    0,    0,    0,  162,
    0,    0,    0,    0,   29,    0,   27,   30,   31,   26,
   16,   15,   40,   39,   38,    0,    0,    0,    0,    0,
    0,    0,    0,  108,  100,    0,    0,   94,    0,    0,
    0,   53,  192,  191,    0,  189,  154,    0,  161,    0,
  152,  153,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   34,    0,    0,    0,    0,    0,    0,   57,
    0,  160,  163,    0,    0,   19,   33,    0,    0,    0,
    0,    0,    0,    0,   35,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  167,    0,    0,
  173,    0,    0,    0,    0,  190,    0,   18,   32,    0,
    0,    0,    0,    0,    0,    0,    0,  206,    0,    0,
  204,    0,  201,  202,    0,    0,  199,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  110,  111,  112,  113,  114,  115,  116,  117,
  118,  119,    0,  120,  121,  132,  133,  134,  135,  123,
  125,  126,  127,  128,  124,  122,  130,  131,  129,    0,
    0,    0,    0,    0,    0,   84,  168,    0,  174,    0,
    0,    0,    0,    0,    0,  139,  141,    0,    0,    0,
    0,   83,    0,  155,    0,    0,    0,    0,  200,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  193,    0,
  179,    0,    0,    0,    0,    0,   81,    0,   82,    0,
   86,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  219,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   85,  164,    0,  165,   87,   88,
    0,    0,    0,  208,    0,  220,  247,    0,  226,  235,
    0,  223,  250,  239,  222,  252,  242,    0,    0,  232,
  211,  234,  253,    0,    0,  210,  142,  158,    0,    0,
    0,    0,    0,    0,    0,  194,    0,    0,    0,    0,
  180,    0,    0,    0,  140,    0,   89,    0,    0,    0,
  196,  209,  248,  236,  243,  233,  230,  244,    0,    0,
    0,    0,  229,  221,    0,    0,    0,    0,    0,  182,
    0,    0,    0,    0,    0,  166,  205,    0,  207,  197,
  231,  246,    0,  195,  240,    0,  184,  185,  183,    0,
  181,  214,    0,    0,  198,  187,    0,    0,  218,  188,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  140,  111,  103,   51,
   76,  112,  171,  223,   52,   39,  104,  235,  113,  546,
  141,   60,   61,   93,   94,  124,  179,  317,   66,  125,
  185,  186,  180,  393,  410,  481,  547,  578,  199,  197,
  341,  523,  200,  548,  318,  319,  320,  321,  322,  482,
  590,  591,  236,  478,  479,  600,  601,  346,  347,
  };
  protected static readonly short [] yySindex = {          251,
   10, -140,   53,   75,   84, 2700, 2764, -262,    0,  251,
    0,    0,    0,    0,  -83,   92,  130,  -95,  -94,  -14,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 1607,    0,    0, 1607,  -55,  -49,    0,  172,  -77,  -32,
 1607,  -28,  175,    0,    0,  -18,    6,    0,    0,    0,
  -25,   25,   25,  123,  256,  -13,  183,  -27,  172,   -7,
  284,   33,   62,    0,    0, 1607,  293, 2738,  -17,  313,
  242,    0,    0,    0,    0, 1607,  -25,  -25,    0, -184,
  331,  263, 2790,  350,    0, 1607, 1607, 1607,    1, 1388,
    0,  172,   99,    0,  355, 2561,  677, 1371, 1607, 1607,
  341,  343,  100,    0, -184, 2818,    0,    0,    0,    0,
   23, 1429,    0,    0, 2561,  172,   11,  -15,  363,    0,
    0, -218,  -35,  110,    0,    0, 2738, 2561,  135,  344,
  374,    0,    0,    0,    0,    0,    0,    0,    0,  258,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2971, 1607, 1607,  373, 1371,  115, 2686,  144,    0, -184,
  149,   41,    0,    0, 2846,    0,    0,   59,  382,  386,
    0,    0,  151,    0,    0, 2561,    0,  122, -206,    0,
    0,    0,    0,    0, -130,    0, -218, 2561,    0,  165,
 -218,  169, 1774,    0,    0,    0,    4, 1371,   -4,    0,
    5,   71,  389,   76,    0,  394,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  396, 2971, 2971,   25,  166,
 -206,  164, -105,    0,    0,  122, -206,    0,  122,  122,
  122,    0,    0,    0,  177,    0,    0, 2971,    0, 1607,
    0,    0,  182,   81,  184, 2601, 1607,   77,   78,  122,
   25,  -91,    0,  174, 3503, -112,  -90,  122,  122,    0,
 1774,    0,    0,  176,  192,    0,    0,  181,  119, 1607,
 1607, -107,  122, 3503,    0,  390, 2971, -201, 2971, 1494,
 1607, 1494, 1607, 1494, 1607, 1607, 1607, 1607, 1607, 1494,
  398, 1607, 1607, 1607, 2971, 2971, 2971, 1607, 1607, 2971,
  -34, 2971, 2971, 2971, 2971, 2971, 2971, 2971, 2971, 2971,
  692, 2881, 1607, 1607,  554,   52, 1483,    0, 3503,  176,
    0,  176, 3503,  -89, 3503,    0,  179,    0,    0, 2971,
  319,  322, 3503,  -87, -106, 1570, 3887,    0,  196, 1475,
    0,  410,    0,    0, 1429, 1494,    0, 1429, 1429, 1494,
 1429, 1429, 1494, 1429, 1429, 1429, 1429, 1429, 1429, 1494,
 1607, 1429, 1429, 1429, 1429,  411,  428,  429,  299,  306,
  431, 1607,  314,  109,  114,  121,  124,  127,  129,  133,
  136,  143,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 1607,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 1607,
   14, 1429,   73, 1607,  554,    0,    0,  176,    0,  179,
  179, 1647, 3503, 1725,  438,    0,    0, 1802, 3503, 3503,
  -85,    0,  176,    0,  442,  226,  467, 1429,    0,  472,
  473, 1429,  481,  483, 1429,  489,  495,  499,  501,  506,
  508, 1429, 1429,  509,  510,  512,  515, 2971, 2971, 2971,
  202, 1607, 1607,  315, 2971, 1607, 1607, 1607, 1607, 1607,
 1607, 1607, 1607, 1607, 1429, 1429, 1475,  520,    0,  521,
    0,  533,   73,   73, 1607,  179,    0, 1879,    0, 2971,
    0, 1956, 2033, 3503,  179,  226,  484, 1475,  530, 1475,
 1475,  532, 1475, 1475,  534, 1475, 1475, 1475, 1475, 1475,
 1475,  535,  536, 1475, 1475, 1475, 1475,    0,  537,  539,
  320, 1429,  540,  541, 2971,  545,  172,  172,  172,  172,
  172,  172,  172,  172,  172,  546,  547,  549,  505, 2971,
 2872,  558,  559,   73,    0,    0,  187,    0,    0,    0,
 2110,  556, 1607,    0, 1475,    0,    0, 1475,    0,    0,
 1475,    0,    0,    0,    0,    0,    0, 1475, 1475,    0,
    0,    0,    0, 2971, 2971,    0,    0,    0,  245,  246,
  557, 2971, 1475, 1475, 1475,    0,  562, 2907, 1342,  188,
    0, 2872, 2872,  568,    0, 2971,    0,  226,  565, 2936,
    0,    0,    0,    0,    0,    0,    0,    0,  353,  354,
 2971,  569,    0,    0,  522, 2971,  577, 2677,  -46,    0,
  122, 2872,  194,  250, 2872,    0,    0,  226,    0,    0,
    0,    0,  569,    0,    0, 2638,    0,    0,    0,  122,
    0,    0,  122,  260,    0,    0,  270,  122,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  618,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  437,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    2,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   66,    0,    0,    0,    0,    0,  580,    0,    0,
    0,    0,    0,    0,    0,    0,  430,    0,    0,    0,
    0,  580,    0,    0,    0,    3,  580,  580,    0,    0,
    0,    0,   79,    0,    0,    0,    0,    0,    0,  851,
 1481,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  580,  580,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  275,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  580,    0,    0,
    0,    0,    0,    0,    0,  281,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   30,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  116,  125,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  580,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2187,
    0, 3580,    0,    0,    0,    0,  152,    0,    0,    0,
  580,  580,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  580,    0,    0,  580,  580,    0,
  580,  580,    0,  580,  580,  580,  580,  580,  580,    0,
    0,  580,  580,  580,  580,    0,    0,    0,  580,  580,
    0,    0,  580,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  580,  580,    0,    0,    0,    0,    0, 2264,    0, 2341,
 3657,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3734,    0,    0,    0,    0,  580,    0,    0,
    0,  580,    0,    0,  580,    0,    0,    0,    0,    0,
    0,  580,  580,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  580,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  580,  580,    0, 2964,    0,    0,
    0,    0,    0,    0,    0, 2418,    0,    0,    0,    0,
    0,    0,    0,    0, 3811,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  580,    0,    0,    0,    0,  517,  594,  681,  768,
  855,  932, 1019, 1106, 1193,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  580,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3041,    0,    0,    0,    0,  298,  580,    0,    0,
 3118,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3195,    0,    0,    0,    0,    0,    0, 3272,
    0,    0, 3349,    0,    0,    0,    0, 3426,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  611,  570,    0,    0,    0,    0,  516,  518,  -31,
  333,   -6,  -96, 1392,    0,  610,  469, -240,    0,   72,
  490,    0,   -2,    0,  504,  -65,  -92,  213, -300,  444,
   44, -182, -155,    0,    0,  150, -503,    0,  482,    0,
 -427,  178, -230,   42,   56,    0,  318,  321,  302, -432,
 -538,   22,  388,    0,  113,    0,   55, -158, -211,
  };
  protected static readonly short [] yyTable = {            38,
   38,  154,  225,   40,   42,  268,   64,   68,  497,  263,
  323,   68,   68,  152,  414,  333,  430,  255,   57,   82,
   77,   78,   68,  224,   38,   35,   64,   59,   58,   90,
  129,  274,  325,  423,   38,  429,   86,  494,   69,  240,
   68,   61,   61,   43,  153,   59,   60,  238,  240,  173,
  542,  543,   64,  623,  624,   64,   36,  241,  203,   38,
  208,   92,  190,   89,  339,  224,  165,  222,  552,   98,
   15,  224,  174,   48,   49,  224,  151,  175,  612,  116,
  117,  118,  340,  123,  165,  221,  644,  177,   34,  123,
  178,  101,  155,  156,  227,  102,  234,  242,  231,  177,
  224,  239,  224,  224,  477,   61,   79,  633,  123,   79,
  220,  594,   68,   18,  485,   91,  224,   85,   61,   95,
   92,  123,   95,  350,   92,  353,   59,   60,  237,   16,
   17,  360,  152,  256,  439,   19,  258,  259,  439,  126,
  159,  439,  127,  160,   20,  198,  198,  164,  439,  234,
  187,   17,   46,  188,  114,  226,   64,  272,  204,  230,
   64,  222,  330,  153,  234,  212,  222,  222,  254,  123,
  627,  181,  182,  183,  184,  191,   45,  114,  188,  224,
  335,  123,  254,  254,  254,   54,  254,  250,  254,  211,
   47,  219,  160,  177,  188,  151,   48,   49,  177,  177,
  645,   62,   50,   32,   33,  229,  251,   63,  188,  188,
  142,  143,  144,   64,  145,  146,  147,  260,  148,  273,
  261,  329,  196,   65,  261,  166,  167,  595,  621,   21,
  596,  622,  149,  198,  642,   71,  215,  622,   22,   67,
  269,   72,  150,   70,   84,   34,   23,   24,   25,   26,
   27,   28,   29,   30,   95,  181,  182,  183,  184,   55,
   81,  172,   56,  331,  332,   73,  181,  182,  183,  184,
   74,   75,  119,  345,  348,  349,  351,  352,  354,  355,
  356,  357,  358,  359,  362,  363,  364,  365,  248,  249,
  643,  369,  370,  622,  373,   80,  163,   90,   90,   87,
  649,   90,   90,  622,   90,   83,  411,  412,   38,  262,
  650,  168,  413,  261,  163,   96,   48,   49,   96,   90,
   90,   28,  599,   68,   28,  372,  169,  170,   88,  142,
  143,  144,   90,  145,  146,  147,   90,  148,  186,  438,
   64,  186,  461,  442,  166,  167,  445,   64,  338,  462,
  342,  149,   96,  452,  453,   64,   64,  465,  525,  426,
   64,  150,  427,   64,   97,  464,  366,  367,  368,  599,
  105,  371,  417,  374,  375,  376,  377,  378,  379,  380,
  381,  382,  194,   91,   91,  106,  475,   91,   91,  115,
   91,  417,   92,   92,  128,  647,   92,   92,  157,   92,
  158,  425,  176,  476,  192,   91,   91,   38,   38,   99,
  100,  483,  484,  193,   92,   92,  202,  209,  216,   17,
   17,  217,   91,   17,   17,  218,   17,  177,  232,  243,
  168,   92,  244,  246,  245,  247,  225,  253,  264,  265,
  266,   17,   17,  270,  271,  169,  170,  275,  328,  222,
  337,  415,  254,  436,  458,  522,  522,   35,   17,  527,
  528,  529,  530,  531,  532,  533,  534,  535,  434,   62,
  157,  459,  460,  157,  463,  466,   61,  417,   38,  417,
  467,  490,  544,  417,  224,  496,  336,  468,   36,  435,
  469,  339,  224,  470,  437,  471,   61,  440,  441,  472,
  443,  444,  473,  446,  447,  448,  449,  450,  451,  474,
  498,  454,  455,  456,  457,  500,  501,  130,    1,    2,
   34,  638,    3,    4,  503,    5,  504,   61,  640,  518,
  519,  520,  506,  131,  589,  422,  526,  424,  507,  234,
    6,    7,  508,  417,  509,  428,  198,  417,  417,  510,
  648,  511,  514,  515,  157,  516,   61,    8,  517,   61,
  521,  480,  132,  539,  540,  133,  134,  135,  136,  137,
  138,  139,  541,  555,  553,  558,  576,  561,  568,  569,
  574,  618,  575,  579,  580,  589,  589,  499,  582,  583,
  584,  502,  585,  198,  505,  477,  581,  592,  593,  598,
  611,  512,  513,  609,  610,  616,  417,  625,  628,  631,
  632,  587,  596,   35,  634,  589,  636,    1,  589,   61,
   44,  162,  161,   79,  536,  537,  538,   53,  210,  195,
  189,  228,  619,   61,  201,  488,  418,  626,  433,  419,
  524,  492,  493,  641,   36,  607,  608,  554,  326,  556,
  557,  586,  559,  560,  630,  562,  563,  564,  565,  566,
  567,   21,    0,  570,  571,  572,  573,    0,    0,    0,
   22,  577,    0,    0,    0,    0,   34,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,  635,    0,    0,
    0,    0,    0,   61,   61,   61,    0,   61,   61,   61,
    0,   61,  157,  157,  602,    0,  551,  603,   61,   61,
  604,    0,    0,    0,    0,   61,    0,  605,  606,    0,
   61,    0,    0,    0,    0,   61,    0,    0,    0,    0,
    0,    0,  613,  614,  615,    0,    0,    0,  620,  361,
    0,    0,    0,    0,  157,  157,  157,    0,    0,    0,
    0,    0,    0,    0,    0,  157,    0,    0,  157,  157,
  157,  157,  157,  157,  157,  157,  157,    0,  639,  157,
  157,    0,    0,  157,  157,  157,  157,  157,  157,    0,
    0,  157,  157,  157,    0,    0,    0,  157,    0,  249,
  249,  157,  157,  157,   61,    0,  157,  157,  157,  157,
  157,  157,    0,  157,    0,  157,    0,   61,    0,   61,
   61,    0,    0,    0,    0,    0,  157,   21,    0,    0,
    0,    0,    0,    0,    0,    0,   22,  157,  157,  157,
  157,  249,  249,  249,   23,   24,   25,   26,   27,   28,
   29,   30,  249,    0,    0,  249,  249,  249,  249,  249,
  249,  249,  249,  249,   65,    0,  249,  249,    0,    0,
  249,  249,  249,  249,  249,  249,  254,  254,  249,  249,
  249,    0,    0,    0,  249,    0,    0,    0,  249,  249,
  249,    0,    0,    0,  249,  249,  249,  249,  249,    0,
  249,    0,  249,    0,   61,    0,    0,    0,    0,    0,
    0,    0,    0,  249,    0,    0,    0,    0,  254,  254,
  254,    0,    0,    0,  249,  249,  249,  249,    0,  254,
    0,    0,  254,  254,  254,  254,  254,  254,  254,  254,
  254,    0,    0,  254,  254,    0,  130,  254,  254,  254,
  254,  254,  254,    0,    0,  254,  254,  254,    0,    0,
    0,  254,  131,  241,  241,  254,  254,  254,    0,    0,
    0,  254,  254,  254,  254,  254,    0,  254,    0,  254,
    0,   61,    0,    0,    0,   54,    0,    0,    0,    0,
  254,  132,    0,    0,  133,  134,  135,  136,  137,  138,
  139,  254,  254,  254,  254,  241,  241,  241,    0,    0,
    0,    0,    0,    0,    0,    0,  241,    0,    0,  241,
  241,  241,  241,  241,  241,  241,  241,  241,    0,    0,
  241,  241,    0,    0,  241,  241,  241,  241,  241,  241,
    0,    0,  241,  241,  241,    0,    0,    0,  241,    0,
  224,  224,  241,  241,  241,    0,    0,    0,  241,  241,
  241,  241,  241,    0,  241,    0,  241,    0,   61,    0,
    0,    0,    0,    0,    0,    0,    0,  241,  383,  384,
  385,  386,  387,  388,  389,  390,  391,  392,  241,  241,
  241,  241,  224,  224,  224,    0,    0,    0,    0,    0,
    0,    0,    0,  224,    0,    0,  224,  224,  224,  224,
  224,  224,  224,  224,  224,    0,    0,  224,  224,    0,
   54,  224,  224,  224,  224,  224,  224,    0,    0,  224,
  224,  224,    0,    0,    0,  224,   54,  225,  225,  224,
  224,  224,    0,    0,    0,  224,  224,  224,  224,  224,
    0,  224,    0,  224,    0,   61,    0,    0,    0,    0,
    0,    0,    0,    0,  224,   54,    0,    0,   54,   54,
   54,   54,   54,   54,   54,  224,  224,  224,  224,  225,
  225,  225,    0,    0,    0,    0,    0,    0,    0,    0,
  225,    0,    0,  225,  225,  225,  225,  225,  225,  225,
  225,  225,    0,    0,  225,  225,    0,    0,  225,  225,
  225,  225,  225,  225,  251,  251,  225,  225,  225,    0,
    0,    0,  225,    0,    0,    0,  225,  225,  225,    0,
    0,    0,  225,  225,  225,  225,  225,    0,  225,    0,
  225,    0,   61,    0,    0,    0,    0,    0,    0,    0,
    0,  225,    0,    0,    0,    0,  251,  251,  251,    0,
    0,    0,  225,  225,  225,  225,    0,  251,    0,    0,
  251,  251,  251,  251,  251,  251,  251,  251,  251,    0,
    0,  251,  251,    0,    0,  251,  251,  251,  251,  251,
  251,    0,    0,  251,  251,  251,    0,    0,    0,  251,
    0,  245,  245,  251,  251,  251,    0,    0,    0,  251,
  251,  251,  251,  251,    0,  251,    0,  251,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  251,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  251,
  251,  251,  251,  245,  245,  245,    0,    0,    0,    0,
    0,    0,    0,    0,  245,    0,    0,  245,  245,  245,
  245,  245,  245,  245,  245,  245,    0,    0,  245,  245,
    0,    0,  245,  245,  245,  245,  245,  245,    0,    0,
  245,  245,  245,    0,    0,    0,  245,    0,  238,  238,
  245,  245,  245,   64,    0,    0,  245,  245,  245,  245,
  245,    0,  245,    0,  245,    0,    0,    0,    0,    0,
    0,  152,    0,    0,    0,  245,    0,    0,    0,    0,
    0,    0,   64,    0,    0,    0,  245,  245,  245,  245,
  238,  238,  238,    0,    0,    0,    0,    0,  122,    0,
  152,  238,  153,    0,  238,  238,  238,  238,  238,  238,
  238,  238,  238,    0,    0,  238,  238,   35,    0,  238,
  238,  238,  238,  238,  238,    0,    0,  238,  238,  238,
    0,  153,    0,  238,  151,  212,  212,  238,  238,  238,
   64,    0,    0,  238,  238,  238,  238,  238,   36,  238,
    0,  238,    0,    0,    0,    0,    0,    0,  152,    0,
    0,    0,  238,  151,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  238,  238,  238,  238,  212,  212,  212,
   34,    0,    0,    0,    0,    0,    0,    0,  212,  153,
    0,  212,  212,  212,  212,  212,  212,  212,  212,  212,
    0,    0,  212,  212,  152,    0,  212,  212,  212,  212,
  212,  212,    0,    0,  212,  212,  212,    0,    0,    0,
  212,  151,    0,   35,  212,  212,  212,    0,    0,    0,
  212,  212,  212,  212,  212,  153,  212,    0,  212,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  212,
    0,    0,    0,    0,   36,    0,    0,    0,    0,    0,
  212,  212,  212,  212,    0,    0,    0,  151,  142,  143,
  144,    0,  145,  146,  147,   55,  148,  416,    0,    0,
    0,    0,  252,  166,  167,    0,   34,    0,  257,    0,
  149,    0,    0,    0,    0,    0,    0,  142,  143,  144,
  150,  145,  146,  147,    0,  148,    0,    0,    0,    0,
    0,    0,    0,  181,  182,  183,  184,  324,    0,  149,
    0,   21,    0,    0,    0,  327,    0,    0,    0,  150,
   22,    0,    0,  334,    0,  120,   35,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,    0,
    0,    0,  121,    0,    0,  142,  143,  144,    0,  145,
  146,  147,    0,  148,  432,    0,    0,   36,    0,  168,
  166,  167,    0,    0,    0,    0,    0,  149,    0,    0,
    0,  420,    0,  421,  169,  170,    0,  150,    0,    0,
    0,    0,    0,    0,    0,    0,  431,    0,    0,   34,
    0,  142,  143,  144,    0,  145,  146,  147,    0,  148,
   55,    0,    0,    0,    0,    0,  166,  167,    0,    0,
    0,    0,    0,  149,    0,  276,   55,   21,    0,    0,
    0,    0,    0,  150,    0,    0,   22,    0,    0,    0,
    0,  487,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,   55,  168,    0,   55,   55,
   55,   55,   55,   55,   55,    0,    0,  277,  278,  279,
    0,  169,  170,    0,    0,    0,    0,    0,  280,  486,
    0,  281,  282,  283,  284,  285,  286,  287,  288,  289,
  343,  344,  290,  291,  495,    0,  292,  293,  294,  295,
  296,  297,  168,  152,  298,  299,  300,    0,    0,    0,
  301,    0,  276,    0,  302,  303,  304,  169,  170,  489,
  305,  306,  307,  308,  309,    0,  310,    0,  311,    0,
    0,    0,    0,    0,  153,    0,    0,    0,    0,  312,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
  313,  314,  315,  316,  277,  278,  279,   23,   24,   25,
   26,   27,   28,   29,   30,  280,  151,    0,  281,  282,
  283,  284,  285,  286,  287,  288,  289,    0,    0,  290,
  291,    0,    0,  292,  293,  294,  295,  296,  297,  276,
    0,  298,  299,  300,    0,    0,  491,  301,    0,    0,
    0,  302,  303,  304,    0,    0,    0,  305,  306,  307,
  308,  309,    0,  310,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,    0,    0,    0,
    0,  277,  278,  279,    0,    0,    0,  313,  314,  315,
  316,    0,  280,    0,    0,  281,  282,  283,  284,  285,
  286,  287,  288,  289,    0,    0,  290,  291,    0,    0,
  292,  293,  294,  295,  296,  297,    0,  276,  298,  299,
  300,    0,    0,  545,  301,    0,    0,    0,  302,  303,
  304,    0,    0,    0,  305,  306,  307,  308,  309,    0,
  310,    0,  311,    0,    0,    0,    0,    0,    0,    0,
  142,  143,  144,  312,  145,  146,  147,    0,  148,  277,
  278,  279,    0,    0,  313,  314,  315,  316,    0,  233,
  280,    0,  149,  281,  282,  283,  284,  285,  286,  287,
  288,  289,  150,    0,  290,  291,    0,    0,  292,  293,
  294,  295,  296,  297,  276,    0,  298,  299,  300,    0,
  549,    0,  301,    0,    0,    0,  302,  303,  304,    0,
    0,    0,  305,  306,  307,  308,  309,    0,  310,    0,
  311,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  312,    0,    0,    0,    0,  277,  278,  279,    0,
    0,    0,  313,  314,  315,  316,    0,  280,    0,    0,
  281,  282,  283,  284,  285,  286,  287,  288,  289,    0,
    0,  290,  291,    0,    0,  292,  293,  294,  295,  296,
  297,  276,    0,  298,  299,  300,    0,  550,    0,  301,
    0,    0,    0,  302,  303,  304,    0,    0,    0,  305,
  306,  307,  308,  309,    0,  310,    0,  311,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  312,    0,
    0,    0,    0,  277,  278,  279,    0,    0,    0,  313,
  314,  315,  316,    0,  280,    0,    0,  281,  282,  283,
  284,  285,  286,  287,  288,  289,    0,    0,  290,  291,
    0,    0,  292,  293,  294,  295,  296,  297,  276,    0,
  298,  299,  300,    0,  597,    0,  301,    0,    0,    0,
  302,  303,  304,    0,    0,    0,  305,  306,  307,  308,
  309,    0,  310,    0,  311,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  312,    0,    0,    0,    0,
  277,  278,  279,    0,    0,    0,  313,  314,  315,  316,
    0,  280,    0,    0,  281,  282,  283,  284,  285,  286,
  287,  288,  289,    0,    0,  290,  291,    0,    0,  292,
  293,  294,  295,  296,  297,  276,    0,  298,  299,  300,
    0,  171,    0,  301,    0,    0,    0,  302,  303,  304,
    0,    0,    0,  305,  306,  307,  308,  309,    0,  310,
    0,  311,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  312,    0,    0,    0,    0,  277,  278,  279,
    0,    0,    0,  313,  314,  315,  316,    0,  280,    0,
    0,  281,  282,  283,  284,  285,  286,  287,  288,  289,
    0,    0,  290,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  276,    0,  298,  299,  300,    0,  169,    0,
  301,    0,    0,    0,  302,  303,  304,    0,    0,    0,
  305,  306,  307,  308,  309,    0,  310,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
    0,    0,    0,    0,  277,  278,  279,    0,    0,    0,
  313,  314,  315,  316,    0,  280,    0,    0,  281,  282,
  283,  284,  285,  286,  287,  288,  289,    0,    0,  290,
  291,    0,    0,  292,  293,  294,  295,  296,  297,  171,
    0,  298,  299,  300,    0,  172,    0,  301,    0,    0,
    0,  302,  303,  304,    0,    0,    0,  305,  306,  307,
  308,  309,    0,  310,    0,  311,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  312,    0,    0,    0,
    0,  171,  171,  171,    0,    0,    0,  313,  314,  315,
  316,    0,  171,    0,    0,  171,  171,  171,  171,  171,
  171,  171,  171,  171,    0,    0,  171,  171,    0,    0,
  171,  171,  171,  171,  171,  171,  169,    0,  171,  171,
  171,    0,  170,    0,  171,    0,    0,    0,  171,  171,
  171,    0,    0,    0,  171,  171,  171,  171,  171,    0,
  171,    0,  171,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  171,    0,    0,    0,    0,  169,  169,
  169,    0,    0,    0,  171,  171,  171,  171,    0,  169,
    0,    0,  169,  169,  169,  169,  169,  169,  169,  169,
  169,    0,    0,  169,  169,    0,    0,  169,  169,  169,
  169,  169,  169,  172,    0,  169,  169,  169,    0,    0,
   35,  169,    0,    0,    0,  169,  169,  169,    0,    0,
    0,  169,  169,  169,  169,  169,    0,  169,    0,  169,
    0,  267,    0,    0,    0,    0,    0,    0,    0,    0,
  169,   36,    0,    0,    0,  172,  172,  172,    0,    0,
  152,  169,  169,  169,  169,    0,  172,    0,    0,  172,
  172,  172,  172,  172,  172,  172,  172,  172,  646,    0,
  172,  172,    0,   34,  172,  172,  172,  172,  172,  172,
  170,  153,  172,  172,  172,    0,    0,  152,  172,    0,
    0,    0,  172,  172,  172,    0,    0,    0,  172,  172,
  172,  172,  172,    0,  172,    0,  172,    0,   64,    0,
    0,    0,    0,  151,    0,    0,    0,  172,  153,    0,
    0,    0,  170,  170,  170,    0,  152,    0,  172,  172,
  172,  172,    0,  170,    0,  152,  170,  170,  170,  170,
  170,  170,  170,  170,  170,    0,    0,  170,  170,   35,
  151,  170,  170,  170,  170,  170,  170,  153,    0,  170,
  170,  170,    0,    0,    0,  170,  153,    0,    0,  170,
  170,  170,    0,    0,    0,  170,  170,  170,  170,  170,
   36,  170,    0,  170,    0,    0,    0,   35,    0,  151,
    0,    0,    0,    0,  170,    0,    0,    0,  151,    0,
    0,    0,    0,    0,    0,  170,  170,  170,  170,    0,
    0,    0,   34,   35,   21,    0,    0,    0,   36,    0,
    0,    0,    0,   22,    0,    0,    0,    0,  120,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,   35,
    0,    0,    0,    0,   36,  121,    0,  142,  143,  144,
   34,  145,  146,  147,    0,  148,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  233,   35,    0,  149,
   36,    0,    0,    0,    0,    0,   34,    0,    0,  150,
    0,    0,    0,    0,  142,  143,  144,    0,  145,  146,
  147,    0,  148,    0,    0,   35,    0,    0,   36,    0,
    0,    0,   34,  233,  110,    0,  149,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  150,    0,    0,    0,
    0,   35,    0,  142,  143,  144,   36,  145,  146,  147,
   34,  148,  142,  143,  144,  205,  145,  146,  147,  637,
  148,    0,    0,    0,    0,  149,    0,    0,    0,  206,
    0,  207,   36,   21,  149,  150,   35,    0,   34,    0,
    0,    0,   22,    0,  150,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,    0,    0,   31,   34,   35,    0,   36,   32,   33,
    0,   21,    0,    0,    0,    0,    0,    0,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,   36,   21,  629,   34,
   35,    0,   91,    0,    0,    0,   22,    0,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,  107,  108,    0,    0,   41,   34,    0,
    0,   36,   22,  109,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,    0,    0,
    0,  107,  108,    0,    0,    0,    0,    0,    0,    0,
   22,  109,    0,   34,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,    0,  107,
  213,    0,    0,    0,    0,    0,    0,    0,   22,  214,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,   21,    0,    0,    0,    0,
    0,  394,  395,    0,   22,    0,    0,    0,    0,  588,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
  617,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  107,    0,  237,  237,    0,    0,
    0,    0,    0,   22,    0,    0,    0,    0,    0,    0,
    0,   23,   24,   25,   26,   27,   28,   29,   30,  396,
  397,  398,  399,    0,    0,    0,    0,    0,  400,  401,
  402,  403,  404,  405,  406,  407,  408,  409,  237,  237,
  237,    0,    0,    0,    0,    0,    0,    0,    0,  237,
    0,    0,  237,  237,  237,  237,  237,  237,  237,  237,
  237,    0,    0,  237,  237,    0,    0,  237,  237,  237,
  237,  237,  237,  227,  227,  237,  237,  237,    0,    0,
    0,  237,    0,    0,    0,  237,  237,  237,    0,    0,
    0,  237,  237,  237,  237,  237,    0,  237,    0,  237,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  237,    0,    0,    0,    0,  227,  227,  227,    0,    0,
    0,  237,  237,  237,  237,    0,  227,    0,    0,  227,
  227,  227,  227,  227,  227,  227,  227,  227,    0,    0,
  227,  227,    0,    0,  227,  227,  227,  227,  227,  227,
  213,  213,  227,  227,  227,    0,    0,    0,  227,    0,
    0,    0,  227,  227,  227,    0,    0,    0,  227,  227,
  227,  227,  227,    0,  227,    0,  227,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  227,    0,    0,
    0,    0,  213,  213,  213,    0,    0,    0,  227,  227,
  227,  227,    0,  213,    0,    0,  213,  213,  213,  213,
  213,  213,  213,  213,  213,    0,    0,  213,  213,    0,
    0,  213,  213,  213,  213,  213,  213,  228,  228,  213,
  213,  213,    0,    0,    0,  213,    0,    0,    0,  213,
  213,  213,    0,    0,    0,  213,  213,  213,  213,  213,
    0,  213,    0,  213,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  213,    0,    0,    0,    0,  228,
  228,  228,    0,    0,    0,  213,  213,  213,  213,    0,
  228,    0,    0,  228,  228,  228,  228,  228,  228,  228,
  228,  228,    0,    0,  228,  228,    0,    0,  228,  228,
  228,  228,  228,  228,  215,  215,  228,  228,  228,    0,
    0,    0,  228,    0,    0,    0,  228,  228,  228,    0,
    0,    0,  228,  228,  228,  228,  228,    0,  228,    0,
  228,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  228,    0,    0,    0,    0,  215,  215,  215,    0,
    0,    0,  228,  228,  228,  228,    0,  215,    0,    0,
  215,  215,  215,  215,  215,  215,  215,  215,  215,    0,
    0,  215,  215,    0,    0,  215,  215,  215,  215,  215,
  215,  217,  217,  215,  215,  215,    0,    0,    0,  215,
    0,    0,    0,  215,  215,  215,    0,    0,    0,  215,
  215,  215,  215,  215,    0,  215,    0,  215,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  215,    0,
    0,    0,    0,  217,  217,  217,    0,    0,    0,  215,
  215,  215,  215,    0,  217,    0,    0,  217,  217,  217,
  217,  217,  217,  217,  217,  217,    0,    0,  217,  217,
    0,    0,  217,  217,  217,  217,  217,  217,  216,  216,
  217,  217,  217,    0,    0,    0,  217,    0,    0,    0,
  217,  217,  217,    0,    0,    0,  217,  217,  217,  217,
  217,    0,  217,    0,  217,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  217,    0,    0,    0,    0,
  216,  216,  216,    0,    0,    0,  217,  217,  217,  217,
    0,  216,    0,    0,  216,  216,  216,  216,  216,  216,
  216,  216,  216,    0,    0,  216,  216,    0,    0,  216,
  216,  216,  216,  216,  216,  276,    0,  216,  216,  216,
    0,    0,    0,  216,    0,    0,    0,  216,  216,  216,
    0,    0,    0,  216,  216,  216,  216,  216,    0,  216,
    0,  216,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  216,    0,    0,    0,    0,  277,  278,  279,
    0,    0,    0,  216,  216,  216,  216,    0,  280,    0,
    0,  281,  282,  283,  284,  285,  286,  287,  288,  289,
    0,    0,  290,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  175,    0,  298,  299,  300,    0,    0,    0,
  301,    0,    0,    0,  302,  303,  304,    0,    0,    0,
  305,  306,  307,  308,  309,    0,  310,    0,  311,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  312,
    0,    0,    0,    0,  175,  175,  175,    0,    0,    0,
  313,  314,  315,  316,    0,  175,    0,    0,  175,  175,
  175,  175,  175,  175,  175,  175,  175,    0,    0,  175,
  175,    0,    0,  175,  175,  175,  175,  175,  175,  176,
    0,  175,  175,  175,    0,    0,    0,  175,    0,    0,
    0,  175,  175,  175,    0,    0,    0,  175,  175,  175,
  175,  175,    0,  175,    0,  175,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  175,    0,    0,    0,
    0,  176,  176,  176,    0,    0,    0,  175,  175,  175,
  175,    0,  176,    0,    0,  176,  176,  176,  176,  176,
  176,  176,  176,  176,    0,    0,  176,  176,    0,    0,
  176,  176,  176,  176,  176,  176,  177,    0,  176,  176,
  176,    0,    0,    0,  176,    0,    0,    0,  176,  176,
  176,    0,    0,    0,  176,  176,  176,  176,  176,    0,
  176,    0,  176,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  176,    0,    0,    0,    0,  177,  177,
  177,    0,    0,    0,  176,  176,  176,  176,    0,  177,
    0,    0,  177,  177,  177,  177,  177,  177,  177,  177,
  177,    0,    0,  177,  177,    0,    0,  177,  177,  177,
  177,  177,  177,  178,    0,  177,  177,  177,    0,    0,
    0,  177,    0,    0,    0,  177,  177,  177,    0,    0,
    0,  177,  177,  177,  177,  177,    0,  177,    0,  177,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  177,    0,    0,    0,    0,  178,  178,  178,    0,    0,
    0,  177,  177,  177,  177,    0,  178,    0,    0,  178,
  178,  178,  178,  178,  178,  178,  178,  178,    0,    0,
  178,  178,    0,    0,  178,  178,  178,  178,  178,  178,
    0,    0,  178,  178,  178,    0,    0,    0,  178,    0,
    0,    0,  178,  178,  178,    0,    0,    0,  178,  178,
  178,  178,  178,    0,  178,    0,  178,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  178,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  178,  178,
  178,  178,  280,    0,    0,  281,  282,  283,  284,  285,
  286,  287,  288,  289,    0,    0,  290,  291,    0,    0,
  292,  293,  294,  295,  296,  297,    0,    0,  298,  299,
  300,    0,    0,    0,  301,    0,    0,    0,  302,  303,
  304,    0,    0,    0,  305,  306,  307,  308,  309,    0,
  310,    0,  311,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  312,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  313,  314,  315,  316,
  };
  protected static readonly short [] yyCheck = {             6,
    7,   98,  185,    6,    7,  246,   42,   40,  436,  240,
  123,   40,   40,   60,  315,  123,  123,  123,   33,   33,
   52,   53,   40,  179,   31,   60,   42,   34,   31,    0,
   96,  123,  123,  123,   41,  123,   44,  123,   41,   44,
   40,   40,   40,  306,   91,   44,   44,   44,   44,  115,
  483,  484,   42,  592,  593,   42,   91,   62,  155,   66,
  157,   68,  128,   66,  266,  221,   44,  274,  496,   76,
   61,  227,   62,  292,  293,  231,  123,   93,  582,   86,
   87,   88,  284,   90,   44,  178,  625,  306,  123,   96,
  122,  276,   99,  100,  187,  280,  193,   93,  191,  306,
  256,  198,  258,  259,   91,   40,   41,  611,  115,   44,
  176,  544,   40,   61,  415,    0,  272,  125,   40,   41,
  127,  128,   44,  282,    0,  284,  125,  125,  125,  270,
  271,  290,   60,  226,  346,   61,  229,  230,  350,   41,
   41,  353,   44,   44,   61,  152,  153,  125,  360,  246,
   41,    0,   61,   44,   83,  187,   42,  250,   44,  191,
   42,  274,   44,   91,  261,  125,  274,  274,  274,  176,
  598,  302,  303,  304,  305,   41,  260,  106,   44,  335,
  273,  188,  274,  274,  274,  280,  274,  219,  274,   41,
   61,   41,   44,  306,   44,  123,  292,  293,  306,  306,
  628,  257,  298,  299,  300,   41,   41,  257,   44,   44,
  257,  258,  259,   42,  261,  262,  263,   41,  265,  251,
   44,   41,  151,  301,   44,  272,  273,   41,   41,  264,
   44,   44,  279,  240,   41,   61,  165,   44,  273,  272,
  247,  260,  289,  272,  272,  123,  281,  282,  283,  284,
  285,  286,  287,  288,  272,  302,  303,  304,  305,  274,
  274,  112,  277,  270,  271,  260,  302,  303,  304,  305,
  296,  297,  272,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,  291,  292,  293,  294,  217,  218,
   41,  298,  299,   44,  301,   40,  274,  268,  269,  267,
   41,  272,  273,   44,  275,  123,  313,  314,  315,  238,
   41,  358,  315,   44,  274,   41,  292,  293,   44,  290,
  291,   41,  553,   40,   44,  360,  373,  374,  267,  257,
  258,  259,   40,  261,  262,  263,  307,  265,   41,  346,
   42,   44,   44,  350,  272,  273,  353,   42,  277,   44,
  279,  279,   40,  360,  361,   42,   42,   44,   44,   41,
   42,  289,   41,   42,  123,  372,  295,  296,  297,  600,
   40,  300,  317,  302,  303,  304,  305,  306,  307,  308,
  309,  310,  125,  268,  269,  123,  393,  272,  273,   40,
  275,  336,  268,  269,   40,  636,  272,  273,   58,  275,
   58,  330,   40,  410,   61,  290,  291,  414,  415,   77,
   78,  414,  415,   40,  290,  291,   44,  274,  360,  268,
  269,   40,  307,  272,  273,   40,  275,  306,  260,  359,
  358,  307,   44,   40,  359,   40,  619,  274,  257,  359,
  257,  290,  291,  367,  367,  373,  374,  274,  257,  274,
   61,  400,  274,   44,   44,  462,  463,   60,  307,  466,
  467,  468,  469,  470,  471,  472,  473,  474,  273,   40,
   41,   44,   44,   44,   44,  367,   40,  422,  485,  424,
  367,   44,  485,  428,  640,   44,  274,  367,   91,  340,
  367,  266,  648,  367,  345,  367,   60,  348,  349,  367,
  351,  352,  367,  354,  355,  356,  357,  358,  359,  367,
   44,  362,  363,  364,  365,   44,   44,  260,  268,  269,
  123,  618,  272,  273,   44,  275,   44,   91,  621,  458,
  459,  460,   44,  276,  541,  323,  465,  325,   44,  636,
  290,  291,   44,  488,   44,  333,  553,  492,  493,   44,
  643,   44,   44,   44,  125,   44,   40,  307,   44,  123,
  359,  412,  305,   44,   44,  308,  309,  310,  311,  312,
  313,  314,   40,   44,   91,   44,  257,   44,   44,   44,
   44,  588,   44,   44,   44,  592,  593,  438,   44,   44,
   44,  442,   44,  600,  445,   91,  525,   40,   40,   44,
   44,  452,  453,  359,  359,   44,  551,   40,   44,  257,
  257,  540,   44,   60,   93,  622,   40,    0,  625,   40,
   10,  106,  105,   54,  475,  476,  477,   18,  160,  140,
  127,  188,  589,   40,  153,  423,  319,  596,  337,  319,
  463,  429,  430,  622,   91,  574,  575,  498,  261,  500,
  501,  539,  503,  504,  600,  506,  507,  508,  509,  510,
  511,  264,   -1,  514,  515,  516,  517,   -1,   -1,   -1,
  273,  522,   -1,   -1,   -1,   -1,  123,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,  616,   -1,   -1,
   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,
   -1,  265,  273,  274,  555,   -1,  494,  558,  272,  273,
  561,   -1,   -1,   -1,   -1,  279,   -1,  568,  569,   -1,
   40,   -1,   -1,   -1,   -1,  289,   -1,   -1,   -1,   -1,
   -1,   -1,  583,  584,  585,   -1,   -1,   -1,  589,  342,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,  619,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,   -1,
   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,  273,
  274,  362,  363,  364,  358,   -1,  367,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   40,   -1,  373,
  374,   -1,   -1,   -1,   -1,   -1,  387,  264,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  273,  398,  399,  400,
  401,  315,  316,  317,  281,  282,  283,  284,  285,  286,
  287,  288,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,  301,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,  274,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   40,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,
  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,
   -1,   -1,  329,  330,  331,  332,  333,  334,  335,  336,
  337,   -1,   -1,  340,  341,   -1,  260,  344,  345,  346,
  347,  348,  349,   -1,   -1,  352,  353,  354,   -1,   -1,
   -1,  358,  276,  273,  274,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   40,   -1,   -1,   -1,  125,   -1,   -1,   -1,   -1,
  387,  305,   -1,   -1,  308,  309,  310,  311,  312,  313,
  314,  398,  399,  400,  401,  315,  316,  317,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,   -1,   -1,  344,  345,  346,  347,  348,  349,
   -1,   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
  273,  274,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   40,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,  377,  378,
  379,  380,  381,  382,  383,  384,  385,  386,  398,  399,
  400,  401,  315,  316,  317,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  326,   -1,   -1,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,
  260,  344,  345,  346,  347,  348,  349,   -1,   -1,  352,
  353,  354,   -1,   -1,   -1,  358,  276,  273,  274,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   40,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,  305,   -1,   -1,  308,  309,
  310,  311,  312,  313,  314,  398,  399,  400,  401,  315,
  316,  317,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,  274,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   40,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
  329,  330,  331,  332,  333,  334,  335,  336,  337,   -1,
   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,  348,
  349,   -1,   -1,  352,  353,  354,   -1,   -1,   -1,  358,
   -1,  273,  274,  362,  363,  364,   -1,   -1,   -1,  368,
  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  398,
  399,  400,  401,  315,  316,  317,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  326,   -1,   -1,  329,  330,  331,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
   -1,   -1,  344,  345,  346,  347,  348,  349,   -1,   -1,
  352,  353,  354,   -1,   -1,   -1,  358,   -1,  273,  274,
  362,  363,  364,   42,   -1,   -1,  368,  369,  370,  371,
  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,
   -1,   -1,   42,   -1,   -1,   -1,  398,  399,  400,  401,
  315,  316,  317,   -1,   -1,   -1,   -1,   -1,   41,   -1,
   60,  326,   91,   -1,  329,  330,  331,  332,  333,  334,
  335,  336,  337,   -1,   -1,  340,  341,   60,   -1,  344,
  345,  346,  347,  348,  349,   -1,   -1,  352,  353,  354,
   -1,   91,   -1,  358,  123,  273,  274,  362,  363,  364,
   42,   -1,   -1,  368,  369,  370,  371,  372,   91,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   60,   -1,
   -1,   -1,  387,  123,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  398,  399,  400,  401,  315,  316,  317,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,   91,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   60,   -1,  344,  345,  346,  347,
  348,  349,   -1,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,  123,   -1,   60,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   91,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,   -1,   -1,  123,  257,  258,
  259,   -1,  261,  262,  263,  125,  265,  125,   -1,   -1,
   -1,   -1,  221,  272,  273,   -1,  123,   -1,  227,   -1,
  279,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  289,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  302,  303,  304,  305,  256,   -1,  279,
   -1,  264,   -1,   -1,   -1,  264,   -1,   -1,   -1,  289,
  273,   -1,   -1,  272,   -1,  278,   60,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,
   -1,   -1,  295,   -1,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,  125,   -1,   -1,   91,   -1,  358,
  272,  273,   -1,   -1,   -1,   -1,   -1,  279,   -1,   -1,
   -1,  320,   -1,  322,  373,  374,   -1,  289,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  335,   -1,   -1,  123,
   -1,  257,  258,  259,   -1,  261,  262,  263,   -1,  265,
  260,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,
   -1,   -1,   -1,  279,   -1,  273,  276,  264,   -1,   -1,
   -1,   -1,   -1,  289,   -1,   -1,  273,   -1,   -1,   -1,
   -1,  125,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,   -1,   -1,  305,  358,   -1,  308,  309,
  310,  311,  312,  313,  314,   -1,   -1,  315,  316,  317,
   -1,  373,  374,   -1,   -1,   -1,   -1,   -1,  326,  418,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
  327,  328,  340,  341,  433,   -1,  344,  345,  346,  347,
  348,  349,  358,   60,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,  273,   -1,  362,  363,  364,  373,  374,  125,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   91,   -1,   -1,   -1,   -1,  387,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  398,  399,  400,  401,  315,  316,  317,  281,  282,  283,
  284,  285,  286,  287,  288,  326,  123,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,   -1,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  273,
   -1,  352,  353,  354,   -1,   -1,  125,  358,   -1,   -1,
   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,   -1,  273,  352,  353,
  354,   -1,   -1,  125,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  257,  258,  259,  387,  261,  262,  263,   -1,  265,  315,
  316,  317,   -1,   -1,  398,  399,  400,  401,   -1,  276,
  326,   -1,  279,  329,  330,  331,  332,  333,  334,  335,
  336,  337,  289,   -1,  340,  341,   -1,   -1,  344,  345,
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
  347,  348,  349,  273,   -1,  352,  353,  354,   -1,   -1,
   60,  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,
   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,  376,
   -1,   41,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  387,   91,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,
   60,  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,
  330,  331,  332,  333,  334,  335,  336,  337,   41,   -1,
  340,  341,   -1,  123,  344,  345,  346,  347,  348,  349,
  273,   91,  352,  353,  354,   -1,   -1,   60,  358,   -1,
   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   42,   -1,
   -1,   -1,   -1,  123,   -1,   -1,   -1,  387,   91,   -1,
   -1,   -1,  315,  316,  317,   -1,   60,   -1,  398,  399,
  400,  401,   -1,  326,   -1,   60,  329,  330,  331,  332,
  333,  334,  335,  336,  337,   -1,   -1,  340,  341,   60,
  123,  344,  345,  346,  347,  348,  349,   91,   -1,  352,
  353,  354,   -1,   -1,   -1,  358,   91,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   91,  374,   -1,  376,   -1,   -1,   -1,   60,   -1,  123,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,  123,   -1,
   -1,   -1,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
   -1,   -1,  123,   60,  264,   -1,   -1,   -1,   91,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,   60,
   -1,   -1,   -1,   -1,   91,  295,   -1,  257,  258,  259,
  123,  261,  262,  263,   -1,  265,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  276,   60,   -1,  279,
   91,   -1,   -1,   -1,   -1,   -1,  123,   -1,   -1,  289,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   60,   -1,   -1,   91,   -1,
   -1,   -1,  123,  276,  125,   -1,  279,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  289,   -1,   -1,   -1,
   -1,   60,   -1,  257,  258,  259,   91,  261,  262,  263,
  123,  265,  257,  258,  259,  260,  261,  262,  263,  273,
  265,   -1,   -1,   -1,   -1,  279,   -1,   -1,   -1,  274,
   -1,  276,   91,  264,  279,  289,   60,   -1,  123,   -1,
   -1,   -1,  273,   -1,  289,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,   -1,   -1,  294,  123,   60,   -1,   91,  299,  300,
   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   91,  264,   93,  123,
   60,   -1,  295,   -1,   -1,   -1,  273,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,   -1,  264,  265,   -1,   -1,  294,  123,   -1,
   -1,   91,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,   -1,   -1,
   -1,  264,  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  273,  274,   -1,  123,   -1,   -1,   -1,   -1,  281,  282,
  283,  284,  285,  286,  287,  288,   -1,   -1,   -1,  264,
  265,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,  264,   -1,   -1,   -1,   -1,
   -1,  261,  262,   -1,  273,   -1,   -1,   -1,   -1,  278,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
  274,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  264,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  281,  282,  283,  284,  285,  286,  287,  288,  379,
  380,  381,  382,   -1,   -1,   -1,   -1,   -1,  388,  389,
  390,  391,  392,  393,  394,  395,  396,  397,  315,  316,
  317,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  326,
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
   -1,  344,  345,  346,  347,  348,  349,  273,  274,  352,
  353,  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,
  363,  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,
   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,
  316,  317,   -1,   -1,   -1,  398,  399,  400,  401,   -1,
  326,   -1,   -1,  329,  330,  331,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,   -1,   -1,  344,  345,
  346,  347,  348,  349,  273,  274,  352,  353,  354,   -1,
   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,   -1,
   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,   -1,
  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,   -1,
   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,   -1,
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
  345,  346,  347,  348,  349,  273,   -1,  352,  353,  354,
   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,  364,
   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,  374,
   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  387,   -1,   -1,   -1,   -1,  315,  316,  317,
   -1,   -1,   -1,  398,  399,  400,  401,   -1,  326,   -1,
   -1,  329,  330,  331,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,   -1,   -1,  344,  345,  346,  347,
  348,  349,  273,   -1,  352,  353,  354,   -1,   -1,   -1,
  358,   -1,   -1,   -1,  362,  363,  364,   -1,   -1,   -1,
  368,  369,  370,  371,  372,   -1,  374,   -1,  376,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,
   -1,   -1,   -1,   -1,  315,  316,  317,   -1,   -1,   -1,
  398,  399,  400,  401,   -1,  326,   -1,   -1,  329,  330,
  331,  332,  333,  334,  335,  336,  337,   -1,   -1,  340,
  341,   -1,   -1,  344,  345,  346,  347,  348,  349,  273,
   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,   -1,
   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,  370,
  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,   -1,
   -1,  315,  316,  317,   -1,   -1,   -1,  398,  399,  400,
  401,   -1,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,  273,   -1,  352,  353,
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
   -1,   -1,  352,  353,  354,   -1,   -1,   -1,  358,   -1,
   -1,   -1,  362,  363,  364,   -1,   -1,   -1,  368,  369,
  370,  371,  372,   -1,  374,   -1,  376,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  387,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  398,  399,
  400,  401,  326,   -1,   -1,  329,  330,  331,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,   -1,   -1,
  344,  345,  346,  347,  348,  349,   -1,   -1,  352,  353,
  354,   -1,   -1,   -1,  358,   -1,   -1,   -1,  362,  363,
  364,   -1,   -1,   -1,  368,  369,  370,  371,  372,   -1,
  374,   -1,  376,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  387,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  398,  399,  400,  401,
  };

#line 941 "Repil/IR/IR.jay"

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
