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
    "INTEGER","FLOAT_LITERAL","STRING","TRUE","FALSE","UNDEF","VOID",
    "NULL","LABEL","X","SOURCE_FILENAME","TARGET","DATALAYOUT","TRIPLE",
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","META_SYMBOL_DEF",
    "SYMBOL","DISTINCT","METADATA","CONSTANT_BYTES","TYPE","HALF","FLOAT",
    "DOUBLE","I1","I8","I16","I32","I64","ZEROINITIALIZER","DEFINE",
    "DECLARE","UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS",
    "GLOBAL","CONSTANT","PRIVATE","INTERNAL","EXTERNAL","FASTCC",
    "NONNULL","NOCAPTURE","WRITEONLY","READONLY","ATTRIBUTE_GROUP_REF",
    "ATTRIBUTES","NORECURSE","NOUNWIND","READNONE","SPECULATABLE","SSP",
    "UWTABLE","ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR","INVOKE",
    "RESUME","CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG",
    "ADD","NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV",
    "FDIV","UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR",
    "XOR","EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
    "INSERTVALUE","ALLOCA","LOAD","STORE","FENCE","CMPXCHG","ATOMICRMW",
    "GETELEMENTPTR","ALIGN","INBOUNDS","INRANGE","TRUNC","ZEXT","SEXT",
    "FPTRUNC","FPEXT","TO","FPTOUI","FPTOSI","UITOFP","SITOFP","PTRTOINT",
    "INTTOPTR","BITCAST","ADDRSPACECAST","ICMP","EQ","NE","UGT","UGE",
    "ULT","ULE","SGT","SGE","SLT","SLE","FCMP","OEQ","OGT","OGE","OLT",
    "OLE","ONE","ORD","UEQ","UNE","UNO","PHI","SELECT","CALL","TAIL",
    "VA_ARG","LANDINGPAD","CATCHPAD","CLEANUPPAD",
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
  { yyVal = BooleanConstant.True; }
  break;
case 146:
#line 473 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 147:
#line 474 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 148:
#line 475 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 149:
#line 476 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 150:
#line 480 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 151:
#line 484 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 152:
#line 491 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 498 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 502 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 155:
#line 509 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 516 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 157:
#line 520 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 527 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 159:
#line 534 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 538 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 549 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 553 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 560 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 165:
#line 564 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 166:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 168:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 170:
#line 590 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 594 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 601 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 605 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 174:
#line 609 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 613 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 177:
#line 624 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 628 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 180:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 181:
#line 643 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 182:
#line 647 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 183:
#line 651 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 184:
#line 655 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 185:
#line 659 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 190:
#line 676 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 680 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 686 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 193:
#line 693 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 697 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 201:
#line 725 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 202:
#line 729 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 733 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 737 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 205:
#line 744 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 748 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 752 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 756 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 760 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 764 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 211:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 212:
#line 772 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 213:
#line 776 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 214:
#line 780 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 215:
#line 784 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 216:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 217:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 229:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 230:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 231:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 235:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 868 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 872 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 876 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 880 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 884 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 888 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 892 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 896 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 244:
#line 900 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 904 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 908 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 912 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 248:
#line 916 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 249:
#line 920 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 250:
#line 924 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 928 "Repil/IR/IR.jay"
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
   13,   41,   20,   20,   42,   40,   40,   43,   39,   39,
   44,   37,   37,   28,   28,   45,   45,   45,   45,   46,
   46,   48,   48,   48,   48,   50,   51,   51,   52,   52,
   52,   52,   52,   52,   52,   18,   18,   53,   53,   54,
   54,   55,   56,   56,   57,   58,   58,   59,   59,   29,
   47,   47,   47,   47,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,
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
    1,    1,    1,    1,    1,    1,    1,    1,    1,    3,
    3,    2,    2,    1,    2,    1,    3,    2,    1,    3,
    1,    1,    3,    1,    2,    2,    3,    1,    2,    1,
    2,    1,    2,    3,    4,    1,    1,    3,    2,    3,
    3,    3,    2,    4,    5,    1,    3,    1,    1,    1,
    3,    5,    1,    2,    3,    1,    2,    1,    1,    1,
    2,    7,    2,    7,    5,    6,    5,    5,    4,    6,
    7,    7,    8,    7,    8,    4,    5,    6,    5,    5,
    4,    4,    5,    6,    7,    6,    6,    7,    5,    6,
    5,    5,    6,    3,    4,    5,    7,    4,    5,    6,
    6,    4,    7,    5,    6,    4,    5,    4,    5,    5,
    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   62,   74,   64,   65,   66,   67,   68,   69,   70,   71,
    0,   23,   22,    0,    0,    0,   63,    0,    0,    0,
    0,    0,    0,    3,    4,    0,    0,  105,  106,   24,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   73,  200,    0,    0,    0,    0,    0,
    0,    5,    6,   20,   21,    0,    0,    0,    7,    0,
    0,    0,    0,    0,   58,    0,    0,    0,    0,    0,
   80,    0,    0,   77,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   25,    0,    0,    0,   43,   42,   12,
    0,    0,   36,   41,    0,    0,    0,    0,    0,   97,
   98,    0,    0,    0,   93,   72,    0,    0,    0,    0,
    0,   56,   46,   47,   48,   49,   50,   51,   52,    0,
   44,  144,  143,  145,  146,  147,  142,  149,  148,    0,
    0,    0,    0,    0,    0,    0,   14,    0,    0,    0,
   37,   13,    0,  138,  137,    0,    0,  136,  153,    0,
   75,   76,    0,  109,    0,    0,  107,  101,  102,  104,
  103,    0,   99,    0,    0,   78,    0,    0,    0,    0,
   11,   45,  156,    0,    0,    0,  159,    0,    0,    0,
   29,    0,   27,   30,   31,   26,   16,   15,   40,   39,
   38,    0,    0,    0,    0,    0,    0,    0,  108,  100,
    0,    0,   94,    0,    0,    0,   53,  189,  188,    0,
  186,  151,    0,  158,    0,  150,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   34,    0,    0,    0,    0,
    0,    0,   57,    0,  157,  160,    0,    0,   19,   33,
    0,    0,    0,    0,    0,    0,   35,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  164,
    0,    0,  170,    0,    0,    0,    0,  187,    0,   18,
   32,    0,    0,    0,    0,    0,    0,    0,  203,    0,
    0,  201,    0,  198,  199,    0,    0,  196,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  110,  111,  112,  113,  114,  115,  116,
  117,  118,  119,    0,  120,  121,  132,  133,  134,  135,
  123,  125,  126,  127,  128,  124,  122,  130,  131,  129,
    0,    0,    0,    0,    0,    0,   84,  165,    0,  171,
    0,    0,    0,    0,    0,    0,  139,    0,    0,    0,
    0,   83,    0,  152,    0,    0,    0,    0,  197,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  190,    0,
  176,    0,    0,    0,    0,    0,   81,    0,   82,    0,
   86,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  216,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   85,  161,    0,  162,   87,   88,
    0,    0,    0,  205,    0,  217,  244,    0,  223,  232,
    0,  220,  247,  236,  219,  249,  239,    0,    0,  229,
  208,  231,  250,    0,    0,  207,  141,  155,    0,    0,
    0,    0,    0,    0,    0,  191,    0,    0,    0,    0,
  177,    0,    0,    0,  140,    0,   89,    0,    0,    0,
  193,  206,  245,  233,  240,  230,  227,  241,    0,    0,
    0,    0,  226,  218,    0,    0,    0,    0,    0,  179,
    0,    0,    0,    0,    0,  163,  202,    0,  204,  194,
  228,  243,    0,  192,  237,    0,  181,  182,  180,    0,
  178,  211,    0,    0,  195,  184,    0,    0,  215,  185,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   37,   12,   13,   14,  140,  111,  103,   51,
   76,  112,  168,  218,   52,   39,  104,  230,  113,  536,
  141,   60,   61,   93,   94,  124,  176,  309,   66,  125,
  182,  183,  177,  384,  401,  471,  537,  568,  196,  194,
  332,  513,  589,  538,  310,  311,  312,  313,  314,  472,
  580,  581,  231,  468,  469,  590,  591,  337,  338,
  };
  protected static readonly short [] yySindex = {          241,
   50, -145,   75,   80,   98, 2599, 2702, -272,    0,  241,
    0,    0,    0,    0,  -79,  140,  152, -135,  -59,  -22,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2933,    0,    0, 2933,  -31,    7,    0,  190,  -64,  -27,
 2933,  -25,  204,    0,    0,   23,   39,    0,    0,    0,
 -196, -143, -143,  170,  257,  -21,  188,  -18,  190,    2,
  270,   49,   62,    0,    0, 2933,  276, 2673,  -16,  296,
  221,    0,    0,    0,    0, 2933, -196, -196,    0,  -81,
  312,  230, 2766,  316,    0, 2933, 2933, 2933,  -10, 2473,
    0,  190,   94,    0,  325, 2638,  291, 2542, 2933, 2933,
  311,  315,  133,    0,  -81, 2803,    0,    0,    0,    0,
  -34,  428,    0,    0, 2638,  190,   -4,   10,  335,    0,
    0, -185,  -13,  151,    0,    0, 2673, 2638,  155,  324,
  347,    0,    0,    0,    0,    0,    0,    0,    0,  359,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 2968,
 2933,  346, 2542,   34, 2745,  118,    0,  -81,  156,  -28,
    0,    0, 2839,    0,    0,   33,  354,    0,    0,  161,
    0,    0, 2638,    0,   91, -242,    0,    0,    0,    0,
    0,   38,    0, -185, 2638,    0,  168, -185,  138, 2577,
    0,    0,    0,    3, 2542,   30,    0,   45,  360,   47,
    0,  368,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  371, 2968, -143,  186, -242,  139, -102,    0,    0,
   91, -242,    0,   91,   91,   91,    0,    0,    0,  187,
    0,    0, 2968,    0, 2933,    0,  158,   59,  162, 1375,
 2933,   56,   91, -143,  -97,    0,  157, 3500,  -86,  -89,
   91,   91,    0, 2577,    0,    0,  160,  166,    0,    0,
  196,  239, 2933,  -82,   91, 3500,    0,  367, 2968, -160,
 2968, 2425, 2933, 2425, 2933, 2425, 2933, 2933, 2933, 2933,
 2933, 2425, 1338, 2933, 2933, 2933, 2968, 2968, 2968, 2933,
 2933, 2968,  622, 2968, 2968, 2968, 2968, 2968, 2968, 2968,
 2968, 2968,  827, 2878, 2933, 2933, 2564,   35, 1359,    0,
 3500,  160,    0,  160, 3500,  -87, 3500,    0,  163,    0,
    0, 2968,  131, 3500,  -84,  -80, 1436, 3884,    0,  167,
 1314,    0,  391,    0,    0,  428, 2425,    0,  428,  428,
 2425,  428,  428, 2425,  428,  428,  428,  428,  428,  428,
 2425, 2933,  428,  428,  428,  428,  393,  394,  396,  306,
  307,  400, 2933,  318,   79,   83,  100,  102,  105,  106,
  111,  113,  117,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 2933,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2933,   11,  428,  110, 2933, 2564,    0,    0,  160,    0,
  163,  163, 1513, 3500, 1590,  416,    0, 1667, 3500, 3500,
  -83,    0,  160,    0,  420,  213,  441,  428,    0,  443,
  447,  428,  450,  457,  428,  458,  463,  466,  467,  472,
  473,  428,  428,  475,  476,  477,  478, 2968, 2968, 2968,
  169, 2933, 2933,  322, 2968, 2933, 2933, 2933, 2933, 2933,
 2933, 2933, 2933, 2933,  428,  428, 1314,  479,    0,  480,
    0,  488,  110,  110, 2933,  163,    0, 1744,    0, 2968,
    0, 1821, 1898, 3500,  163,  213,  438, 1314,  489, 1314,
 1314,  490, 1314, 1314,  491, 1314, 1314, 1314, 1314, 1314,
 1314,  492,  495, 1314, 1314, 1314, 1314,    0,  497,  499,
  285,  428,  500,  501, 2968,  504,  190,  190,  190,  190,
  190,  190,  190,  190,  190,  505,  508,  510,  465, 2968,
 2873,  517,  518,  110,    0,    0,  215,    0,    0,    0,
 1975,  515, 2933,    0, 1314,    0,    0, 1314,    0,    0,
 1314,    0,    0,    0,    0,    0,    0, 1314, 1314,    0,
    0,    0,    0, 2968, 2968,    0,    0,    0,  210,  211,
  526, 2968, 1314, 1314, 1314,    0,  527, 2889, 1292,  219,
    0, 2873, 2873,  522,    0, 2968,    0,  213,  529, 2917,
    0,    0,    0,    0,    0,    0,    0,    0,  323,  326,
 2968,  535,    0,    0,  493, 2968,  547, 2459,  -54,    0,
   91, 2873,  251,  261, 2873,    0,    0,  213,    0,    0,
    0,    0,  535,    0,    0, 2520,    0,    0,    0,   91,
    0,    0,   91,  263,    0,    0,  265,   91,    0,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  588,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  303,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    4,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  103,    0,    0,    0,    0,    0,  554,    0,    0,
    0,    0,    0,    0,    0,    0,  421,    0,    0,    0,
    0,  554,    0,    0,    0,    5,  554,  554,    0,    0,
    0,    0,  125,    0,    0,    0,    0,    0,    0, 1231,
 2874,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  554,  554,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  288,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  554,    0,    0,    0,    0,    0,
    0,  289,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   87,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  112,  142,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  554,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 2052,    0, 3577,    0,    0,    0,    0,  153,    0,
    0,    0,  554,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  554,    0,    0,  554,  554,
    0,  554,  554,    0,  554,  554,  554,  554,  554,  554,
    0,    0,  554,  554,  554,  554,    0,    0,    0,  554,
  554,    0,    0,  554,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  554,  554,    0,    0,    0,    0,    0, 2129,    0,
 2206, 3654,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3731,    0,    0,    0,    0,  554,    0,    0,
    0,  554,    0,    0,  554,    0,    0,    0,    0,    0,
    0,  554,  554,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  554,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  554,  554,    0, 2961,    0,    0,
    0,    0,    0,    0,    0, 2283,    0,    0,    0,    0,
    0,    0,    0,    0, 3808,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  554,    0,    0,    0,    0,  498,  585,  662,  739,
  816,  903,  980, 1057, 1134,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  554,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3038,    0,    0,    0,    0,  293,  554,    0,    0,
 3115,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3192,    0,    0,    0,    0,    0,    0, 3269,
    0,    0, 3346,    0,    0,    0,    0, 3423,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  586,  543,    0,    0,    0,    0,  502,  506,  -33,
  165,   -6,  -94, 2815,    0,  587,  449, -237,    0,   25,
  470,    0,    1,    0,  485,  -40, -170, -198, -290,  429,
   40, -180, -112,    0,    0,  150, -515,    0,    0,    0,
 -403,  171, -101,   27, -300,    0,  309,  310,  294, -402,
 -517,   14,  369,    0,   99,    0,   37, -197, -183,
  };
  protected static readonly short [] yyTable = {            38,
   38,  220,  261,  152,  216,  151,   40,   42,  408,  163,
   57,   82,   68,  222,   68,  163,  405,  226,   77,   78,
  248,   68,  487,   68,   38,  266,  408,   59,   64,   68,
  217,   58,   43,  317,   38,  414,  315,   64,  419,  484,
  324,   69,  420,   61,   61,   86,  233,   59,   60,  197,
  249,   64,   64,  251,  252,  129,  602,  171,  199,   38,
  204,   92,  174,  219,  613,  614,   89,  327,  150,   98,
  532,  533,  264,  235,  170,   64,  341,  200,  344,  116,
  117,  118,  542,  123,  351,  623,   90,  187,  175,  123,
  162,  236,  153,  154,  326,  229,  208,  634,   74,   75,
  234,  467,  172,  219,  330,   48,   49,  114,  123,  219,
   15,   91,  408,  219,  408,  475,  413,  408,  415,  174,
   92,  123,  331,   16,   17,  418,   85,  232,   59,   60,
  114,  584,  215,  256,  126,   18,  219,  127,  219,  219,
   19,   92,   61,   79,  195,  229,   79,   48,   49,   68,
  221,  219,   17,  429,  225,   48,   49,  429,   20,  229,
  429,   50,   32,   33,   61,   95,  123,  429,   95,  151,
  247,  417,   64,  157,  193,  247,  158,  408,  123,   45,
  243,  408,  408,  247,  617,  247,  217,  211,  247,  247,
  217,  184,  217,  101,  185,  188,  207,  102,  185,  158,
   46,  214,  142,  143,  185,  144,  145,  146,  224,  147,
  265,  185,   47,  219,  635,  478,  164,  165,  174,   54,
  482,  483,  174,  148,  174,   62,  244,  253,  195,  185,
  254,   64,  150,  149,  262,   65,  321,  242,  161,  254,
  408,   99,  100,   67,  161,   70,  178,  179,  180,  181,
   55,   81,   84,   56,   95,  585,  323,  255,  586,  611,
  119,  169,  612,   63,   71,  336,  339,  340,  342,  343,
  345,  346,  347,  348,  349,  350,  353,  354,  355,  356,
   64,   72,  322,  360,  361,  541,  364,  178,  179,  180,
  181,  632,   34,  329,  612,  333,   80,   73,  402,  403,
   38,  633,  166,  639,  612,  640,  612,  404,  254,   68,
   83,  357,  358,  359,   87,   90,  362,  167,  365,  366,
  367,  368,  369,  370,  371,  372,  373,   88,   96,   28,
  428,   96,   28,  183,  432,   96,  183,  435,  178,  179,
  180,  181,   61,   97,  442,  443,  416,   64,   64,  451,
  452,  105,  106,   90,   90,  115,  454,   90,   90,   64,
   90,  455,   61,   64,  128,  515,  142,  143,  155,  144,
  145,  146,  156,  147,  173,   90,   90,  465,   91,   91,
  164,  165,   91,   91,  189,   91,  190,  148,  637,  198,
  205,  212,   90,  213,  466,  174,  227,  149,   38,   38,
   91,   91,  237,  238,  239,  473,  474,  240,   92,   92,
  241,  246,   92,   92,  257,   92,  258,   91,  259,   17,
   17,  263,  320,   17,   17,   61,   17,  328,  220,  267,
   92,   92,  217,  406,  426,  247,  448,  449,  424,  450,
  630,   17,   17,  453,  456,  512,  512,   92,  457,  517,
  518,  519,  520,  521,  522,  523,  524,  525,   17,  480,
   62,  154,  638,  486,  154,  458,  166,  459,   38,   64,
  460,  461,  508,  509,  510,  534,  462,  330,  463,  516,
  425,  167,  464,  191,  488,  427,  490,  151,  430,  431,
  491,  433,  434,  493,  436,  437,  438,  439,  440,  441,
  494,  496,  444,  445,  446,  447,  497,    1,    2,  498,
  499,    3,    4,  628,    5,  500,  501,  219,  504,  505,
  506,  507,  529,  530,  579,  219,  511,  531,  543,    6,
    7,  229,  545,  548,  551,  558,  195,   61,  559,  571,
  564,  566,  565,  569,  570,  154,    8,  572,  573,  130,
  150,  574,  470,  575,  577,  467,  582,  583,  588,   61,
   61,  615,   61,   61,   61,  131,   61,  599,  600,  601,
  606,  608,  618,   61,   61,  579,  579,  489,  586,  621,
   61,  492,  622,  195,  495,  624,  626,    1,  597,  598,
   61,  502,  503,   61,  132,   44,   79,  133,  134,  135,
  136,  137,  138,  139,   53,  579,  206,  160,  579,  192,
  159,  186,  616,  223,  526,  527,  528,  130,  609,  409,
  410,  423,  318,  514,   61,  631,  620,  576,    0,    0,
  625,    0,    0,  131,    0,    0,    0,  544,    0,  546,
  547,    0,  549,  550,    0,  552,  553,  554,  555,  556,
  557,    0,    0,  560,  561,  562,  563,    0,    0,   61,
    0,  567,  132,    0,    0,  133,  134,  135,  136,  137,
  138,  139,    0,    0,   61,    0,    0,    0,    0,    0,
    0,   35,    0,    0,  142,  143,    0,  144,  145,  146,
    0,  147,  154,  154,  592,    0,    0,  593,  164,  165,
  594,   61,    0,    0,    0,  148,    0,  595,  596,    0,
    0,    0,   36,    0,    0,  149,    0,    0,    0,    0,
    0,    0,  603,  604,  605,    0,    0,    0,  610,    0,
    0,    0,    0,    0,  154,  154,  154,    0,    0,    0,
    0,    0,    0,    0,   34,  154,    0,    0,  154,  154,
  154,  154,  154,  154,  154,  154,  154,    0,  629,  154,
  154,    0,    0,  154,  154,  154,  154,  154,  154,  246,
  246,  154,  154,  154,    0,    0,    0,  154,   61,    0,
    0,  154,  154,  154,  166,    0,  154,  154,  154,  154,
  154,  154,    0,  154,    0,  154,    0,    0,    0,  167,
    0,    0,    0,    0,    0,    0,  154,    0,    0,    0,
    0,  246,  246,  246,    0,    0,    0,  154,  154,  154,
  154,    0,  246,    0,    0,  246,  246,  246,  246,  246,
  246,  246,  246,  246,    0,    0,  246,  246,    0,    0,
  246,  246,  246,  246,  246,  246,    0,    0,  246,  246,
  246,    0,    0,    0,  246,   61,  251,  251,  246,  246,
  246,    0,    0,    0,  246,  246,  246,  246,  246,    0,
  246,    0,  246,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  246,   21,    0,    0,    0,    0,    0,
    0,    0,    0,   22,  246,  246,  246,  246,  251,  251,
  251,   23,   24,   25,   26,   27,   28,   29,   30,  251,
    0,    0,  251,  251,  251,  251,  251,  251,  251,  251,
  251,    0,    0,  251,  251,    0,    0,  251,  251,  251,
  251,  251,  251,  238,  238,  251,  251,  251,    0,    0,
    0,  251,   61,    0,    0,  251,  251,  251,    0,    0,
    0,  251,  251,  251,  251,  251,    0,  251,    0,  251,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  251,    0,    0,    0,    0,  238,  238,  238,    0,    0,
  363,  251,  251,  251,  251,    0,  238,    0,    0,  238,
  238,  238,  238,  238,  238,  238,  238,  238,    0,    0,
  238,  238,    0,    0,  238,  238,  238,  238,  238,  238,
  221,  221,  238,  238,  238,    0,    0,    0,  238,   61,
    0,    0,  238,  238,  238,    0,    0,    0,  238,  238,
  238,  238,  238,    0,  238,    0,  238,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  238,    0,    0,
    0,    0,  221,  221,  221,    0,    0,    0,  238,  238,
  238,  238,    0,  221,    0,    0,  221,  221,  221,  221,
  221,  221,  221,  221,  221,    0,    0,  221,  221,    0,
    0,  221,  221,  221,  221,  221,  221,  222,  222,  221,
  221,  221,    0,    0,    0,  221,   61,    0,    0,  221,
  221,  221,    0,    0,    0,  221,  221,  221,  221,  221,
    0,  221,    0,  221,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  221,    0,    0,    0,    0,  222,
  222,  222,    0,    0,    0,  221,  221,  221,  221,    0,
  222,    0,    0,  222,  222,  222,  222,  222,  222,  222,
  222,  222,    0,    0,  222,  222,    0,    0,  222,  222,
  222,  222,  222,  222,    0,    0,  222,  222,  222,    0,
    0,    0,  222,   61,  248,  248,  222,  222,  222,    0,
    0,    0,  222,  222,  222,  222,  222,    0,  222,    0,
  222,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  222,  374,  375,  376,  377,  378,  379,  380,  381,
  382,  383,  222,  222,  222,  222,  248,  248,  248,    0,
    0,    0,    0,    0,    0,    0,    0,  248,    0,    0,
  248,  248,  248,  248,  248,  248,  248,  248,  248,    0,
    0,  248,  248,    0,    0,  248,  248,  248,  248,  248,
  248,  242,  242,  248,  248,  248,    0,    0,    0,  248,
    0,    0,    0,  248,  248,  248,    0,    0,    0,  248,
  248,  248,  248,  248,    0,  248,    0,  248,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  248,    0,
    0,    0,    0,  242,  242,  242,    0,    0,    0,  248,
  248,  248,  248,    0,  242,    0,    0,  242,  242,  242,
  242,  242,  242,  242,  242,  242,    0,    0,  242,  242,
    0,    0,  242,  242,  242,  242,  242,  242,  235,  235,
  242,  242,  242,   64,    0,    0,  242,    0,    0,    0,
  242,  242,  242,    0,    0,    0,  242,  242,  242,  242,
  242,  151,  242,    0,  242,   54,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  242,    0,    0,    0,    0,
  235,  235,  235,  151,    0,    0,  242,  242,  242,  242,
    0,  235,    0,    0,  235,  235,  235,  235,  235,  235,
  235,  235,  235,    0,    0,  235,  235,   35,    0,  235,
  235,  235,  235,  235,  235,  209,  209,  235,  235,  235,
    0,    0,    0,  235,  150,  260,    0,  235,  235,  235,
    0,    0,    0,  235,  235,  235,  235,  235,   36,  235,
    0,  235,    0,    0,  151,    0,  150,    0,    0,    0,
    0,    0,  235,    0,    0,    0,    0,  209,  209,  209,
    0,    0,    0,  235,  235,  235,  235,    0,  209,    0,
   34,  209,  209,  209,  209,  209,  209,  209,  209,  209,
    0,    0,  209,  209,    0,    0,  209,  209,  209,  209,
  209,  209,    0,  407,  209,  209,  209,    0,    0,   54,
  209,    0,    0,    0,  209,  209,  209,  150,    0,    0,
  209,  209,  209,  209,  209,   54,  209,    0,  209,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  209,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  209,  209,  209,  209,   54,    0,    0,   54,   54,   54,
   54,   54,   54,   54,    0,    0,    0,    0,  142,  143,
    0,  144,  145,  146,    0,  147,    0,    0,    0,    0,
  422,    0,  164,  165,    0,    0,    0,    0,    0,  148,
  142,  143,    0,  144,  145,  146,    0,  147,    0,  149,
    0,    0,    0,    0,  164,  165,    0,    0,    0,    0,
    0,  148,  178,  179,  180,  181,    0,    0,    0,    0,
   21,  149,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,    0,    0,
  268,  142,  143,    0,  144,  145,  146,  477,  147,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  166,  228,
    0,    0,  148,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  149,  167,    0,    0,    0,    0,    0,    0,
  166,    0,  269,  270,  271,    0,    0,    0,  352,    0,
    0,    0,    0,  272,    0,  167,  273,  274,  275,  276,
  277,  278,  279,  280,  281,    0,    0,  282,  283,    0,
    0,  284,  285,  286,  287,  288,  289,  268,    0,  290,
  291,  292,    0,    0,  479,  293,    0,    0,    0,  294,
  295,  296,    0,    0,    0,  297,  298,  299,  300,  301,
    0,  302,    0,  303,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  304,    0,    0,    0,    0,  269,
  270,  271,    0,    0,    0,  305,  306,  307,  308,    0,
  272,    0,    0,  273,  274,  275,  276,  277,  278,  279,
  280,  281,    0,    0,  282,  283,    0,    0,  284,  285,
  286,  287,  288,  289,  268,    0,  290,  291,  292,    0,
    0,  481,  293,    0,    0,    0,  294,  295,  296,    0,
    0,    0,  297,  298,  299,  300,  301,    0,  302,    0,
  303,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  304,    0,    0,    0,    0,  269,  270,  271,    0,
    0,    0,  305,  306,  307,  308,    0,  272,    0,    0,
  273,  274,  275,  276,  277,  278,  279,  280,  281,    0,
    0,  282,  283,    0,    0,  284,  285,  286,  287,  288,
  289,  268,    0,  290,  291,  292,    0,    0,  535,  293,
    0,    0,    0,  294,  295,  296,    0,    0,    0,  297,
  298,  299,  300,  301,    0,  302,    0,  303,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  304,    0,
    0,    0,    0,  269,  270,  271,    0,    0,    0,  305,
  306,  307,  308,    0,  272,    0,    0,  273,  274,  275,
  276,  277,  278,  279,  280,  281,    0,    0,  282,  283,
    0,    0,  284,  285,  286,  287,  288,  289,  268,    0,
  290,  291,  292,    0,    0,  539,  293,    0,    0,    0,
  294,  295,  296,    0,    0,    0,  297,  298,  299,  300,
  301,    0,  302,    0,  303,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,    0,    0,    0,    0,
  269,  270,  271,    0,    0,    0,  305,  306,  307,  308,
    0,  272,    0,    0,  273,  274,  275,  276,  277,  278,
  279,  280,  281,    0,    0,  282,  283,    0,    0,  284,
  285,  286,  287,  288,  289,  268,    0,  290,  291,  292,
    0,    0,  540,  293,    0,    0,    0,  294,  295,  296,
    0,    0,    0,  297,  298,  299,  300,  301,    0,  302,
    0,  303,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  304,    0,    0,    0,    0,  269,  270,  271,
    0,    0,    0,  305,  306,  307,  308,    0,  272,    0,
    0,  273,  274,  275,  276,  277,  278,  279,  280,  281,
    0,    0,  282,  283,    0,    0,  284,  285,  286,  287,
  288,  289,  268,    0,  290,  291,  292,    0,    0,  587,
  293,    0,    0,    0,  294,  295,  296,    0,    0,    0,
  297,  298,  299,  300,  301,    0,  302,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  304,
    0,    0,    0,    0,  269,  270,  271,    0,    0,    0,
  305,  306,  307,  308,    0,  272,    0,    0,  273,  274,
  275,  276,  277,  278,  279,  280,  281,    0,    0,  282,
  283,    0,    0,  284,  285,  286,  287,  288,  289,  268,
    0,  290,  291,  292,    0,    0,  168,  293,    0,    0,
    0,  294,  295,  296,    0,    0,    0,  297,  298,  299,
  300,  301,    0,  302,    0,  303,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  304,    0,    0,    0,
    0,  269,  270,  271,    0,    0,    0,  305,  306,  307,
  308,    0,  272,    0,    0,  273,  274,  275,  276,  277,
  278,  279,  280,  281,    0,    0,  282,  283,    0,    0,
  284,  285,  286,  287,  288,  289,  268,    0,  290,  291,
  292,    0,    0,  166,  293,    0,    0,    0,  294,  295,
  296,    0,    0,    0,  297,  298,  299,  300,  301,    0,
  302,    0,  303,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  304,    0,    0,    0,    0,  269,  270,
  271,    0,    0,    0,  305,  306,  307,  308,    0,  272,
    0,    0,  273,  274,  275,  276,  277,  278,  279,  280,
  281,    0,    0,  282,  283,    0,    0,  284,  285,  286,
  287,  288,  289,  168,    0,  290,  291,  292,    0,    0,
  169,  293,    0,    0,    0,  294,  295,  296,    0,    0,
    0,  297,  298,  299,  300,  301,    0,  302,    0,  303,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  304,    0,    0,    0,    0,  168,  168,  168,    0,    0,
    0,  305,  306,  307,  308,    0,  168,    0,    0,  168,
  168,  168,  168,  168,  168,  168,  168,  168,    0,    0,
  168,  168,    0,    0,  168,  168,  168,  168,  168,  168,
  166,    0,  168,  168,  168,    0,    0,  167,  168,    0,
    0,    0,  168,  168,  168,    0,    0,    0,  168,  168,
  168,  168,  168,    0,  168,    0,  168,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  168,    0,    0,
    0,    0,  166,  166,  166,    0,    0,    0,  168,  168,
  168,  168,    0,  166,    0,    0,  166,  166,  166,  166,
  166,  166,  166,  166,  166,    0,    0,  166,  166,    0,
    0,  166,  166,  166,  166,  166,  166,  169,    0,  166,
  166,  166,    0,    0,   35,  166,    0,    0,    0,  166,
  166,  166,    0,    0,    0,  166,  166,  166,  166,  166,
   64,  166,    0,  166,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  122,  166,   36,    0,    0,  151,  169,
  169,  169,    0,    0,    0,  166,  166,  166,  166,    0,
  169,    0,   35,  169,  169,  169,  169,  169,  169,  169,
  169,  169,    0,    0,  169,  169,    0,   34,  169,  169,
  169,  169,  169,  169,  167,    0,  169,  169,  169,    0,
  636,    0,  169,   36,    0,    0,  169,  169,  169,    0,
    0,    0,  169,  169,  169,  169,  169,    0,  169,  151,
  169,  150,    0,   64,    0,    0,    0,    0,    0,    0,
    0,  169,    0,    0,    0,   34,  167,  167,  167,    0,
    0,  151,  169,  169,  169,  169,    0,  167,    0,    0,
  167,  167,  167,  167,  167,  167,  167,  167,  167,    0,
    0,  167,  167,   35,    0,  167,  167,  167,  167,  167,
  167,    0,    0,  167,  167,  167,  151,    0,    0,  167,
    0,    0,  150,  167,  167,  167,    0,    0,    0,  167,
  167,  167,  167,  167,   36,  167,    0,  167,   35,    0,
    0,    0,    0,    0,  150,    0,    0,    0,  167,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  167,
  167,  167,  167,    0,    0,    0,   34,   21,    0,   36,
    0,    0,    0,    0,    0,    0,   22,   35,    0,  150,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,  142,  143,    0,  144,  145,
  146,   34,  147,    0,    0,    0,    0,    0,   36,    0,
  627,    0,   35,    0,    0,   21,  148,    0,    0,    0,
    0,    0,    0,    0,   22,    0,  149,    0,    0,  120,
  334,  335,   23,   24,   25,   26,   27,   28,   29,   30,
   34,   35,    0,   36,    0,    0,  121,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  142,  143,    0,  144,
  145,  146,    0,  147,    0,    0,    0,    0,    0,    0,
    0,    0,   36,    0,  228,   34,    0,  148,  142,  143,
    0,  144,  145,  146,  151,  147,    0,  149,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  148,
    0,    0,    0,    0,   34,   35,   21,    0,    0,  149,
    0,    0,    0,  142,  143,   22,  144,  145,  146,    0,
  147,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,  228,    0,    0,  148,    0,   36,    0,    0,    0,
    0,   21,   35,   65,  149,    0,    0,  150,    0,    0,
   22,    0,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,    0,   34,    0,
  110,   31,    0,   36,    0,    0,   32,   33,   35,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,  120,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   34,    0,    0,    0,   36,
    0,  121,   35,    0,    0,   21,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,   35,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,   34,    0,   36,   21,    0,   91,    0,    0,    0,
    0,    0,    0,   22,    0,    0,   35,    0,    0,   36,
    0,   23,   24,   25,   26,   27,   28,   29,   30,    0,
    0,    0,   35,    0,   41,   34,    0,    0,   55,    0,
    0,  142,  143,  201,  144,  145,  146,   36,  147,  619,
    0,   34,    0,    0,    0,    0,    0,  202,    0,  203,
    0,    0,  148,   36,    0,    0,    0,   35,  107,  108,
  245,    0,  149,    0,    0,    0,  250,   22,  109,   34,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,    0,    0,   34,    0,    0,   36,    0,
    0,    0,    0,  316,    0,  107,  108,    0,    0,    0,
    0,  319,    0,    0,   22,  109,    0,    0,  325,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   34,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  107,  209,    0,    0,    0,    0,    0,    0,    0,
   22,  210,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,  411,    0,  412,    0,
    0,    0,   55,    0,    0,   21,    0,  385,  386,    0,
  421,    0,    0,    0,   22,    0,    0,    0,   55,  578,
    0,   21,   23,   24,   25,   26,   27,   28,   29,   30,
   22,  607,    0,    0,    0,    0,    0,    0,   23,   24,
   25,   26,   27,   28,   29,   30,    0,   55,    0,   21,
   55,   55,   55,   55,   55,   55,   55,    0,   22,    0,
    0,    0,    0,    0,    0,   21,   23,   24,   25,   26,
   27,   28,   29,   30,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
    0,    0,    0,  476,    0,    0,    0,    0,    0,    0,
  107,    0,  234,  234,    0,    0,    0,  485,    0,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,  387,  388,  389,  390,    0,
    0,    0,    0,    0,  391,  392,  393,  394,  395,  396,
  397,  398,  399,  400,  234,  234,  234,    0,    0,    0,
    0,    0,    0,    0,    0,  234,    0,    0,  234,  234,
  234,  234,  234,  234,  234,  234,  234,    0,    0,  234,
  234,    0,    0,  234,  234,  234,  234,  234,  234,  224,
  224,  234,  234,  234,    0,    0,    0,  234,    0,    0,
    0,  234,  234,  234,    0,    0,    0,  234,  234,  234,
  234,  234,    0,  234,    0,  234,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  234,    0,    0,    0,
    0,  224,  224,  224,    0,    0,    0,  234,  234,  234,
  234,    0,  224,    0,    0,  224,  224,  224,  224,  224,
  224,  224,  224,  224,    0,    0,  224,  224,    0,    0,
  224,  224,  224,  224,  224,  224,  210,  210,  224,  224,
  224,    0,    0,    0,  224,    0,    0,    0,  224,  224,
  224,    0,    0,    0,  224,  224,  224,  224,  224,    0,
  224,    0,  224,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  224,    0,    0,    0,    0,  210,  210,
  210,    0,    0,    0,  224,  224,  224,  224,    0,  210,
    0,    0,  210,  210,  210,  210,  210,  210,  210,  210,
  210,    0,    0,  210,  210,    0,    0,  210,  210,  210,
  210,  210,  210,  225,  225,  210,  210,  210,    0,    0,
    0,  210,    0,    0,    0,  210,  210,  210,    0,    0,
    0,  210,  210,  210,  210,  210,    0,  210,    0,  210,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  210,    0,    0,    0,    0,  225,  225,  225,    0,    0,
    0,  210,  210,  210,  210,    0,  225,    0,    0,  225,
  225,  225,  225,  225,  225,  225,  225,  225,    0,    0,
  225,  225,    0,    0,  225,  225,  225,  225,  225,  225,
  212,  212,  225,  225,  225,    0,    0,    0,  225,    0,
    0,    0,  225,  225,  225,    0,    0,    0,  225,  225,
  225,  225,  225,    0,  225,    0,  225,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  225,    0,    0,
    0,    0,  212,  212,  212,    0,    0,    0,  225,  225,
  225,  225,    0,  212,    0,    0,  212,  212,  212,  212,
  212,  212,  212,  212,  212,    0,    0,  212,  212,    0,
    0,  212,  212,  212,  212,  212,  212,  214,  214,  212,
  212,  212,    0,    0,    0,  212,    0,    0,    0,  212,
  212,  212,    0,    0,    0,  212,  212,  212,  212,  212,
    0,  212,    0,  212,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  212,    0,    0,    0,    0,  214,
  214,  214,    0,    0,    0,  212,  212,  212,  212,    0,
  214,    0,    0,  214,  214,  214,  214,  214,  214,  214,
  214,  214,    0,    0,  214,  214,    0,    0,  214,  214,
  214,  214,  214,  214,  213,  213,  214,  214,  214,    0,
    0,    0,  214,    0,    0,    0,  214,  214,  214,    0,
    0,    0,  214,  214,  214,  214,  214,    0,  214,    0,
  214,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  214,    0,    0,    0,    0,  213,  213,  213,    0,
    0,    0,  214,  214,  214,  214,    0,  213,    0,    0,
  213,  213,  213,  213,  213,  213,  213,  213,  213,    0,
    0,  213,  213,    0,    0,  213,  213,  213,  213,  213,
  213,  268,    0,  213,  213,  213,    0,    0,    0,  213,
    0,    0,    0,  213,  213,  213,    0,    0,    0,  213,
  213,  213,  213,  213,    0,  213,    0,  213,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  213,    0,
    0,    0,    0,  269,  270,  271,    0,    0,    0,  213,
  213,  213,  213,    0,  272,    0,    0,  273,  274,  275,
  276,  277,  278,  279,  280,  281,    0,    0,  282,  283,
    0,    0,  284,  285,  286,  287,  288,  289,  172,    0,
  290,  291,  292,    0,    0,    0,  293,    0,    0,    0,
  294,  295,  296,    0,    0,    0,  297,  298,  299,  300,
  301,    0,  302,    0,  303,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  304,    0,    0,    0,    0,
  172,  172,  172,    0,    0,    0,  305,  306,  307,  308,
    0,  172,    0,    0,  172,  172,  172,  172,  172,  172,
  172,  172,  172,    0,    0,  172,  172,    0,    0,  172,
  172,  172,  172,  172,  172,  173,    0,  172,  172,  172,
    0,    0,    0,  172,    0,    0,    0,  172,  172,  172,
    0,    0,    0,  172,  172,  172,  172,  172,    0,  172,
    0,  172,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  172,    0,    0,    0,    0,  173,  173,  173,
    0,    0,    0,  172,  172,  172,  172,    0,  173,    0,
    0,  173,  173,  173,  173,  173,  173,  173,  173,  173,
    0,    0,  173,  173,    0,    0,  173,  173,  173,  173,
  173,  173,  174,    0,  173,  173,  173,    0,    0,    0,
  173,    0,    0,    0,  173,  173,  173,    0,    0,    0,
  173,  173,  173,  173,  173,    0,  173,    0,  173,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  173,
    0,    0,    0,    0,  174,  174,  174,    0,    0,    0,
  173,  173,  173,  173,    0,  174,    0,    0,  174,  174,
  174,  174,  174,  174,  174,  174,  174,    0,    0,  174,
  174,    0,    0,  174,  174,  174,  174,  174,  174,  175,
    0,  174,  174,  174,    0,    0,    0,  174,    0,    0,
    0,  174,  174,  174,    0,    0,    0,  174,  174,  174,
  174,  174,    0,  174,    0,  174,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  174,    0,    0,    0,
    0,  175,  175,  175,    0,    0,    0,  174,  174,  174,
  174,    0,  175,    0,    0,  175,  175,  175,  175,  175,
  175,  175,  175,  175,    0,    0,  175,  175,    0,    0,
  175,  175,  175,  175,  175,  175,    0,    0,  175,  175,
  175,    0,    0,    0,  175,    0,    0,    0,  175,  175,
  175,    0,    0,    0,  175,  175,  175,  175,  175,    0,
  175,    0,  175,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  175,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  175,  175,  175,  175,  272,    0,
    0,  273,  274,  275,  276,  277,  278,  279,  280,  281,
    0,    0,  282,  283,    0,    0,  284,  285,  286,  287,
  288,  289,    0,    0,  290,  291,  292,    0,    0,    0,
  293,    0,    0,    0,  294,  295,  296,    0,    0,    0,
  297,  298,  299,  300,  301,    0,  302,    0,  303,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  304,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  305,  306,  307,  308,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  182,  240,   98,  175,   60,    6,    7,  309,   44,
   33,   33,   40,  184,   40,   44,  307,  188,   52,   53,
  123,   40,  426,   40,   31,  123,  327,   34,   42,   40,
  273,   31,  305,  123,   41,  123,  123,   42,  123,  123,
  123,   41,  123,   40,   40,   44,   44,   44,   44,  151,
  221,   42,   42,  224,  225,   96,  572,   62,  153,   66,
  155,   68,  305,  176,  582,  583,   66,  266,  123,   76,
  473,  474,  243,   44,  115,   42,  274,   44,  276,   86,
   87,   88,  486,   90,  282,  601,    0,  128,  122,   96,
  125,   62,   99,  100,  265,  190,  125,  615,  295,  296,
  195,   91,   93,  216,  265,  291,  292,   83,  115,  222,
   61,    0,  413,  226,  415,  406,  315,  418,  317,  305,
  127,  128,  283,  269,  270,  324,  125,  125,  125,  125,
  106,  534,  173,  235,   41,   61,  249,   44,  251,  252,
   61,    0,   40,   41,  151,  240,   44,  291,  292,   40,
  184,  264,    0,  337,  188,  291,  292,  341,   61,  254,
  344,  297,  298,  299,   40,   41,  173,  351,   44,   60,
  273,   41,   42,   41,  150,  273,   44,  478,  185,  259,
  214,  482,  483,  273,  588,  273,  273,  163,  273,  273,
  273,   41,  273,  275,   44,   41,   41,  279,   44,   44,
   61,   41,  257,  258,   44,  260,  261,  262,   41,  264,
  244,   44,   61,  326,  618,  414,  271,  272,  305,  279,
  419,  420,  305,  278,  305,  257,   41,   41,  235,   44,
   44,   42,  123,  288,  241,  300,   41,  213,  273,   44,
  541,   77,   78,  271,  273,  271,  301,  302,  303,  304,
  273,  273,  271,  276,  271,   41,  263,  233,   44,   41,
  271,  112,   44,  257,   61,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
   42,  259,   44,  290,  291,  484,  293,  301,  302,  303,
  304,   41,  123,  269,   44,  271,   40,  259,  305,  306,
  307,   41,  357,   41,   44,   41,   44,  307,   44,   40,
  123,  287,  288,  289,  266,   40,  292,  372,  294,  295,
  296,  297,  298,  299,  300,  301,  302,  266,   41,   41,
  337,   44,   44,   41,  341,   40,   44,  344,  301,  302,
  303,  304,   40,  123,  351,  352,  322,   42,   42,   44,
   44,   40,  123,  267,  268,   40,  363,  271,  272,   42,
  274,   44,   60,   42,   40,   44,  257,  258,   58,  260,
  261,  262,   58,  264,   40,  289,  290,  384,  267,  268,
  271,  272,  271,  272,   61,  274,   40,  278,  626,   44,
  273,  359,  306,   40,  401,  305,  259,  288,  405,  406,
  289,  290,  358,   44,  358,  405,  406,   40,  267,  268,
   40,  273,  271,  272,  257,  274,  358,  306,  257,  267,
  268,  366,  257,  271,  272,  123,  274,   61,  609,  273,
  289,  290,  273,  399,   44,  273,   44,   44,  272,   44,
  611,  289,  290,   44,  366,  452,  453,  306,  366,  456,
  457,  458,  459,  460,  461,  462,  463,  464,  306,   44,
   40,   41,  633,   44,   44,  366,  357,  366,  475,   42,
  366,  366,  448,  449,  450,  475,  366,  265,  366,  455,
  331,  372,  366,  125,   44,  336,   44,   60,  339,  340,
   44,  342,  343,   44,  345,  346,  347,  348,  349,  350,
   44,   44,  353,  354,  355,  356,   44,  267,  268,   44,
   44,  271,  272,  608,  274,   44,   44,  630,   44,   44,
   44,   44,   44,   44,  531,  638,  358,   40,   91,  289,
  290,  626,   44,   44,   44,   44,  543,   40,   44,  515,
   44,  257,   44,   44,   44,  125,  306,   44,   44,  259,
  123,   44,  403,   44,  530,   91,   40,   40,   44,  257,
  258,   40,  260,  261,  262,  275,  264,  358,  358,   44,
   44,  578,   44,  271,  272,  582,  583,  428,   44,  257,
  278,  432,  257,  590,  435,   93,   40,    0,  564,  565,
  288,  442,  443,   40,  304,   10,   54,  307,  308,  309,
  310,  311,  312,  313,   18,  612,  158,  106,  615,  140,
  105,  127,  586,  185,  465,  466,  467,  259,  579,  311,
  311,  328,  254,  453,   40,  612,  590,  529,   -1,   -1,
  606,   -1,   -1,  275,   -1,   -1,   -1,  488,   -1,  490,
  491,   -1,  493,  494,   -1,  496,  497,  498,  499,  500,
  501,   -1,   -1,  504,  505,  506,  507,   -1,   -1,  357,
   -1,  512,  304,   -1,   -1,  307,  308,  309,  310,  311,
  312,  313,   -1,   -1,  372,   -1,   -1,   -1,   -1,   -1,
   -1,   60,   -1,   -1,  257,  258,   -1,  260,  261,  262,
   -1,  264,  272,  273,  545,   -1,   -1,  548,  271,  272,
  551,   40,   -1,   -1,   -1,  278,   -1,  558,  559,   -1,
   -1,   -1,   91,   -1,   -1,  288,   -1,   -1,   -1,   -1,
   -1,   -1,  573,  574,  575,   -1,   -1,   -1,  579,   -1,
   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,  609,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
  273,  351,  352,  353,   -1,   -1,   -1,  357,   40,   -1,
   -1,  361,  362,  363,  357,   -1,  366,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,  372,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,   -1,   -1,  351,  352,
  353,   -1,   -1,   -1,  357,   40,  272,  273,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,  263,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  272,  397,  398,  399,  400,  314,  315,
  316,  280,  281,  282,  283,  284,  285,  286,  287,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  272,  273,  351,  352,  353,   -1,   -1,
   -1,  357,   40,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,
  359,  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  272,  273,  351,  352,  353,   -1,   -1,   -1,  357,   40,
   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,
   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,
  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,  273,  351,
  352,  353,   -1,   -1,   -1,  357,   40,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,   -1,   -1,  351,  352,  353,   -1,
   -1,   -1,  357,   40,  272,  273,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,  376,  377,  378,  379,  380,  381,  382,  383,
  384,  385,  397,  398,  399,  400,  314,  315,  316,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,  273,  351,  352,  353,   -1,   -1,   -1,  357,
   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  272,  273,
  351,  352,  353,   42,   -1,   -1,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   60,  373,   -1,  375,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
  314,  315,  316,   60,   -1,   -1,  397,  398,  399,  400,
   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   60,   -1,  343,
  344,  345,  346,  347,  348,  272,  273,  351,  352,  353,
   -1,   -1,   -1,  357,  123,   41,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   91,  373,
   -1,  375,   -1,   -1,   60,   -1,  123,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
  123,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,   -1,  125,  351,  352,  353,   -1,   -1,  259,
  357,   -1,   -1,   -1,  361,  362,  363,  123,   -1,   -1,
  367,  368,  369,  370,  371,  275,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  397,  398,  399,  400,  304,   -1,   -1,  307,  308,  309,
  310,  311,  312,  313,   -1,   -1,   -1,   -1,  257,  258,
   -1,  260,  261,  262,   -1,  264,   -1,   -1,   -1,   -1,
  125,   -1,  271,  272,   -1,   -1,   -1,   -1,   -1,  278,
  257,  258,   -1,  260,  261,  262,   -1,  264,   -1,  288,
   -1,   -1,   -1,   -1,  271,  272,   -1,   -1,   -1,   -1,
   -1,  278,  301,  302,  303,  304,   -1,   -1,   -1,   -1,
  263,  288,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,  282,
  283,  284,  285,  286,  287,   -1,   -1,   -1,   -1,   -1,
  272,  257,  258,   -1,  260,  261,  262,  125,  264,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  357,  275,
   -1,   -1,  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  288,  372,   -1,   -1,   -1,   -1,   -1,   -1,
  357,   -1,  314,  315,  316,   -1,   -1,   -1,  341,   -1,
   -1,   -1,   -1,  325,   -1,  372,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,   -1,  351,
  352,  353,   -1,   -1,  125,  357,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  272,   -1,  351,  352,  353,   -1,
   -1,  125,  357,   -1,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,
   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,   -1,  351,  352,  353,   -1,   -1,  125,  357,
   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  272,   -1,
  351,  352,  353,   -1,   -1,  125,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,
   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  272,   -1,  351,  352,  353,
   -1,   -1,  125,  357,   -1,   -1,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  272,   -1,  351,  352,  353,   -1,   -1,  125,
  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,
  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
   -1,  351,  352,  353,   -1,   -1,  125,  357,   -1,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  272,   -1,  351,  352,
  353,   -1,   -1,  125,  357,   -1,   -1,   -1,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,
  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  272,   -1,  351,  352,  353,   -1,   -1,
  125,  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,
   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  272,   -1,  351,  352,  353,   -1,   -1,  125,  357,   -1,
   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,
   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,
  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,   -1,  351,
  352,  353,   -1,   -1,   60,  357,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   42,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   41,  386,   91,   -1,   -1,   60,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   60,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,  123,  343,  344,
  345,  346,  347,  348,  272,   -1,  351,  352,  353,   -1,
   41,   -1,  357,   91,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   60,
  375,  123,   -1,   42,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,  123,  314,  315,  316,   -1,
   -1,   60,  397,  398,  399,  400,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   60,   -1,  343,  344,  345,  346,  347,
  348,   -1,   -1,  351,  352,  353,   60,   -1,   -1,  357,
   -1,   -1,  123,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   91,  373,   -1,  375,   60,   -1,
   -1,   -1,   -1,   -1,  123,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,   -1,   -1,  123,  263,   -1,   91,
   -1,   -1,   -1,   -1,   -1,   -1,  272,   60,   -1,  123,
   -1,   -1,   -1,   -1,  280,  281,  282,  283,  284,  285,
  286,  287,   -1,   -1,   -1,  257,  258,   -1,  260,  261,
  262,  123,  264,   -1,   -1,   -1,   -1,   -1,   91,   -1,
  272,   -1,   60,   -1,   -1,  263,  278,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  272,   -1,  288,   -1,   -1,  277,
  326,  327,  280,  281,  282,  283,  284,  285,  286,  287,
  123,   60,   -1,   91,   -1,   -1,  294,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,   -1,  260,
  261,  262,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   91,   -1,  275,  123,   -1,  278,  257,  258,
   -1,  260,  261,  262,   60,  264,   -1,  288,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  278,
   -1,   -1,   -1,   -1,  123,   60,  263,   -1,   -1,  288,
   -1,   -1,   -1,  257,  258,  272,  260,  261,  262,   -1,
  264,   -1,   -1,  280,  281,  282,  283,  284,  285,  286,
  287,  275,   -1,   -1,  278,   -1,   91,   -1,   -1,   -1,
   -1,  263,   60,  300,  288,   -1,   -1,  123,   -1,   -1,
  272,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,   -1,   -1,  123,   -1,
  125,  293,   -1,   91,   -1,   -1,  298,  299,   60,   -1,
  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   -1,   -1,   -1,   -1,  277,   -1,   -1,  280,  281,  282,
  283,  284,  285,  286,  287,  123,   -1,   -1,   -1,   91,
   -1,  294,   60,   -1,   -1,  263,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  272,   -1,   -1,   -1,   60,   -1,
   -1,   -1,  280,  281,  282,  283,  284,  285,  286,  287,
   -1,  123,   -1,   91,  263,   -1,  294,   -1,   -1,   -1,
   -1,   -1,   -1,  272,   -1,   -1,   60,   -1,   -1,   91,
   -1,  280,  281,  282,  283,  284,  285,  286,  287,   -1,
   -1,   -1,   60,   -1,  293,  123,   -1,   -1,  125,   -1,
   -1,  257,  258,  259,  260,  261,  262,   91,  264,   93,
   -1,  123,   -1,   -1,   -1,   -1,   -1,  273,   -1,  275,
   -1,   -1,  278,   91,   -1,   -1,   -1,   60,  263,  264,
  216,   -1,  288,   -1,   -1,   -1,  222,  272,  273,  123,
   -1,   -1,   -1,   -1,   -1,  280,  281,  282,  283,  284,
  285,  286,  287,   -1,   -1,  123,   -1,   -1,   91,   -1,
   -1,   -1,   -1,  249,   -1,  263,  264,   -1,   -1,   -1,
   -1,  257,   -1,   -1,  272,  273,   -1,   -1,  264,   -1,
   -1,   -1,  280,  281,  282,  283,  284,  285,  286,  287,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  263,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,  312,   -1,  314,   -1,
   -1,   -1,  259,   -1,   -1,  263,   -1,  260,  261,   -1,
  326,   -1,   -1,   -1,  272,   -1,   -1,   -1,  275,  277,
   -1,  263,  280,  281,  282,  283,  284,  285,  286,  287,
  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,
  282,  283,  284,  285,  286,  287,   -1,  304,   -1,  263,
  307,  308,  309,  310,  311,  312,  313,   -1,  272,   -1,
   -1,   -1,   -1,   -1,   -1,  263,  280,  281,  282,  283,
  284,  285,  286,  287,  272,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  280,  281,  282,  283,  284,  285,  286,  287,
   -1,   -1,   -1,  409,   -1,   -1,   -1,   -1,   -1,   -1,
  263,   -1,  272,  273,   -1,   -1,   -1,  423,   -1,  272,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  280,  281,  282,
  283,  284,  285,  286,  287,  378,  379,  380,  381,   -1,
   -1,   -1,   -1,   -1,  387,  388,  389,  390,  391,  392,
  393,  394,  395,  396,  314,  315,  316,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
  273,  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,  272,  273,  351,  352,
  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,
  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,
   -1,   -1,  328,  329,  330,  331,  332,  333,  334,  335,
  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,
  346,  347,  348,  272,  273,  351,  352,  353,   -1,   -1,
   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,
   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,  375,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,
   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,
  329,  330,  331,  332,  333,  334,  335,  336,   -1,   -1,
  339,  340,   -1,   -1,  343,  344,  345,  346,  347,  348,
  272,  273,  351,  352,  353,   -1,   -1,   -1,  357,   -1,
   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,
  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,
   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,
  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,
  332,  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,
   -1,  343,  344,  345,  346,  347,  348,  272,  273,  351,
  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,
   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,
  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,   -1,
  325,   -1,   -1,  328,  329,  330,  331,  332,  333,  334,
  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,  344,
  345,  346,  347,  348,  272,  273,  351,  352,  353,   -1,
   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,   -1,
   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,   -1,
  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,   -1,
   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,   -1,
  328,  329,  330,  331,  332,  333,  334,  335,  336,   -1,
   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,  347,
  348,  272,   -1,  351,  352,  353,   -1,   -1,   -1,  357,
   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,  367,
  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,
   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,  397,
  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,  330,
  331,  332,  333,  334,  335,  336,   -1,   -1,  339,  340,
   -1,   -1,  343,  344,  345,  346,  347,  348,  272,   -1,
  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,
  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,  370,
  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,
  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,  400,
   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,  333,
  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,  343,
  344,  345,  346,  347,  348,  272,   -1,  351,  352,  353,
   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,  362,  363,
   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,  373,
   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  386,   -1,   -1,   -1,   -1,  314,  315,  316,
   -1,   -1,   -1,  397,  398,  399,  400,   -1,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,  272,   -1,  351,  352,  353,   -1,   -1,   -1,
  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,  314,  315,  316,   -1,   -1,   -1,
  397,  398,  399,  400,   -1,  325,   -1,   -1,  328,  329,
  330,  331,  332,  333,  334,  335,  336,   -1,   -1,  339,
  340,   -1,   -1,  343,  344,  345,  346,  347,  348,  272,
   -1,  351,  352,  353,   -1,   -1,   -1,  357,   -1,   -1,
   -1,  361,  362,  363,   -1,   -1,   -1,  367,  368,  369,
  370,  371,   -1,  373,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,   -1,   -1,   -1,
   -1,  314,  315,  316,   -1,   -1,   -1,  397,  398,  399,
  400,   -1,  325,   -1,   -1,  328,  329,  330,  331,  332,
  333,  334,  335,  336,   -1,   -1,  339,  340,   -1,   -1,
  343,  344,  345,  346,  347,  348,   -1,   -1,  351,  352,
  353,   -1,   -1,   -1,  357,   -1,   -1,   -1,  361,  362,
  363,   -1,   -1,   -1,  367,  368,  369,  370,  371,   -1,
  373,   -1,  375,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  386,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  397,  398,  399,  400,  325,   -1,
   -1,  328,  329,  330,  331,  332,  333,  334,  335,  336,
   -1,   -1,  339,  340,   -1,   -1,  343,  344,  345,  346,
  347,  348,   -1,   -1,  351,  352,  353,   -1,   -1,   -1,
  357,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,   -1,
  367,  368,  369,  370,  371,   -1,  373,   -1,  375,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  386,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  397,  398,  399,  400,
  };

#line 932 "Repil/IR/IR.jay"

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
  public const int FLOAT_LITERAL = 258;
  public const int STRING = 259;
  public const int TRUE = 260;
  public const int FALSE = 261;
  public const int UNDEF = 262;
  public const int VOID = 263;
  public const int NULL = 264;
  public const int LABEL = 265;
  public const int X = 266;
  public const int SOURCE_FILENAME = 267;
  public const int TARGET = 268;
  public const int DATALAYOUT = 269;
  public const int TRIPLE = 270;
  public const int GLOBAL_SYMBOL = 271;
  public const int LOCAL_SYMBOL = 272;
  public const int META_SYMBOL = 273;
  public const int META_SYMBOL_DEF = 274;
  public const int SYMBOL = 275;
  public const int DISTINCT = 276;
  public const int METADATA = 277;
  public const int CONSTANT_BYTES = 278;
  public const int TYPE = 279;
  public const int HALF = 280;
  public const int FLOAT = 281;
  public const int DOUBLE = 282;
  public const int I1 = 283;
  public const int I8 = 284;
  public const int I16 = 285;
  public const int I32 = 286;
  public const int I64 = 287;
  public const int ZEROINITIALIZER = 288;
  public const int DEFINE = 289;
  public const int DECLARE = 290;
  public const int UNNAMED_ADDR = 291;
  public const int LOCAL_UNNAMED_ADDR = 292;
  public const int NOALIAS = 293;
  public const int ELLIPSIS = 294;
  public const int GLOBAL = 295;
  public const int CONSTANT = 296;
  public const int PRIVATE = 297;
  public const int INTERNAL = 298;
  public const int EXTERNAL = 299;
  public const int FASTCC = 300;
  public const int NONNULL = 301;
  public const int NOCAPTURE = 302;
  public const int WRITEONLY = 303;
  public const int READONLY = 304;
  public const int ATTRIBUTE_GROUP_REF = 305;
  public const int ATTRIBUTES = 306;
  public const int NORECURSE = 307;
  public const int NOUNWIND = 308;
  public const int READNONE = 309;
  public const int SPECULATABLE = 310;
  public const int SSP = 311;
  public const int UWTABLE = 312;
  public const int ARGMEMONLY = 313;
  public const int RET = 314;
  public const int BR = 315;
  public const int SWITCH = 316;
  public const int INDIRECTBR = 317;
  public const int INVOKE = 318;
  public const int RESUME = 319;
  public const int CATCHSWITCH = 320;
  public const int CATCHRET = 321;
  public const int CLEANUPRET = 322;
  public const int UNREACHABLE = 323;
  public const int FNEG = 324;
  public const int ADD = 325;
  public const int NUW = 326;
  public const int NSW = 327;
  public const int FADD = 328;
  public const int SUB = 329;
  public const int FSUB = 330;
  public const int MUL = 331;
  public const int FMUL = 332;
  public const int UDIV = 333;
  public const int SDIV = 334;
  public const int FDIV = 335;
  public const int UREM = 336;
  public const int SREM = 337;
  public const int FREM = 338;
  public const int SHL = 339;
  public const int LSHR = 340;
  public const int EXACT = 341;
  public const int ASHR = 342;
  public const int AND = 343;
  public const int OR = 344;
  public const int XOR = 345;
  public const int EXTRACTELEMENT = 346;
  public const int INSERTELEMENT = 347;
  public const int SHUFFLEVECTOR = 348;
  public const int EXTRACTVALUE = 349;
  public const int INSERTVALUE = 350;
  public const int ALLOCA = 351;
  public const int LOAD = 352;
  public const int STORE = 353;
  public const int FENCE = 354;
  public const int CMPXCHG = 355;
  public const int ATOMICRMW = 356;
  public const int GETELEMENTPTR = 357;
  public const int ALIGN = 358;
  public const int INBOUNDS = 359;
  public const int INRANGE = 360;
  public const int TRUNC = 361;
  public const int ZEXT = 362;
  public const int SEXT = 363;
  public const int FPTRUNC = 364;
  public const int FPEXT = 365;
  public const int TO = 366;
  public const int FPTOUI = 367;
  public const int FPTOSI = 368;
  public const int UITOFP = 369;
  public const int SITOFP = 370;
  public const int PTRTOINT = 371;
  public const int INTTOPTR = 372;
  public const int BITCAST = 373;
  public const int ADDRSPACECAST = 374;
  public const int ICMP = 375;
  public const int EQ = 376;
  public const int NE = 377;
  public const int UGT = 378;
  public const int UGE = 379;
  public const int ULT = 380;
  public const int ULE = 381;
  public const int SGT = 382;
  public const int SGE = 383;
  public const int SLT = 384;
  public const int SLE = 385;
  public const int FCMP = 386;
  public const int OEQ = 387;
  public const int OGT = 388;
  public const int OGE = 389;
  public const int OLT = 390;
  public const int OLE = 391;
  public const int ONE = 392;
  public const int ORD = 393;
  public const int UEQ = 394;
  public const int UNE = 395;
  public const int UNO = 396;
  public const int PHI = 397;
  public const int SELECT = 398;
  public const int CALL = 399;
  public const int TAIL = 400;
  public const int VA_ARG = 401;
  public const int LANDINGPAD = 402;
  public const int CATCHPAD = 403;
  public const int CLEANUPPAD = 404;
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
