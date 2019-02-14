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
//t    "global_variable : GLOBAL_SYMBOL '=' function_addr GLOBAL type constant ',' ALIGN INTEGER metadata_kvs",
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
//t    "pointer_value : value",
//t    "pointer_value : GETELEMENTPTR INBOUNDS '(' type ',' typed_value ',' element_indices ')'",
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : UNDEF",
//t    "constant : ZEROINITIALIZER",
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
//t    "instruction : CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL return_type function_pointer '(' function_args ')'",
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
//t    "instruction : UITOFP typed_value TO type",
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
    "SYMBOL","DISTINCT","METADATA","TYPE","HALF","FLOAT","DOUBLE","I1",
    "I8","I16","I32","I64","ZEROINITIALIZER","DEFINE","DECLARE",
    "UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NOALIAS","ELLIPSIS","GLOBAL",
    "CONSTANT","NONNULL","NOCAPTURE","WRITEONLY","READONLY",
    "ATTRIBUTE_GROUP_REF","ATTRIBUTES","NORECURSE","NOUNWIND","READNONE",
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
#line 58 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 62 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 66 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 70 "Repil/IR/IR.jay"
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
#line 90 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-4+yyTop]] = new List<object> (0);
    }
  break;
case 13:
#line 94 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 14:
  case_14();
  break;
case 15:
#line 103 "Repil/IR/IR.jay"
  {
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = yyVals[-1+yyTop];
    }
  break;
case 16:
  case_16();
  break;
case 17:
#line 115 "Repil/IR/IR.jay"
  {
        yyVal = new GlobalVariable ((GlobalSymbol)yyVals[-9+yyTop], false, (LType)yyVals[-5+yyTop], (Constant)yyVals[-4+yyTop]);
    }
  break;
case 18:
  case_18();
  break;
case 19:
  case_19();
  break;
case 20:
#line 132 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 21:
#line 133 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 22:
#line 134 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 23:
#line 135 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 24:
#line 136 "Repil/IR/IR.jay"
  { yyVal = Tuple.Create (yyVals[-2+yyTop], yyVals[0+yyTop]); }
  break;
case 25:
#line 140 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-5+yyTop], yyVals[-3+yyTop]);
    }
  break;
case 26:
#line 144 "Repil/IR/IR.jay"
  {
        yyVal = Tuple.Create (yyVals[-4+yyTop], yyVals[-2+yyTop]);
    }
  break;
case 27:
#line 151 "Repil/IR/IR.jay"
  {
        yyVal = NewSyms (yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 28:
#line 155 "Repil/IR/IR.jay"
  {
        yyVal = SymsAdd (yyVals[-2+yyTop], yyVals[-1+yyTop], (MetaSymbol)yyVals[0+yyTop]);
    }
  break;
case 29:
#line 162 "Repil/IR/IR.jay"
  {
        yyVal = NewList (yyVals[0+yyTop]);
    }
  break;
case 30:
#line 166 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], yyVals[0+yyTop]);
    }
  break;
case 31:
#line 170 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 32:
#line 174 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 33:
#line 178 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], yyVals[0+yyTop]);
    }
  break;
case 51:
  case_51();
  break;
case 52:
#line 219 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 53:
#line 223 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 55:
#line 228 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 57:
#line 233 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 58:
#line 234 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 59:
#line 235 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 60:
#line 236 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 61:
#line 237 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 62:
#line 238 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 63:
#line 239 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 64:
#line 240 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 65:
#line 244 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 66:
#line 248 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 67:
#line 252 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 256 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 69:
#line 260 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 70:
#line 267 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 71:
#line 271 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 279 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 74:
#line 286 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 75:
#line 290 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 76:
#line 294 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 77:
#line 298 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], Enumerable.Empty<Parameter> (), (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 78:
#line 302 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 79:
#line 306 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 80:
#line 310 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 81:
#line 317 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 82:
#line 321 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 83:
#line 325 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 84:
#line 332 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 336 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 86:
#line 343 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 87:
#line 347 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 88:
#line 351 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 89:
#line 355 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 91:
#line 363 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 92:
#line 367 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 93:
#line 368 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 94:
#line 369 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 95:
#line 370 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 101:
#line 388 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 102:
#line 389 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 103:
#line 390 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 104:
#line 391 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 105:
#line 392 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 106:
#line 393 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 107:
#line 394 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 108:
#line 395 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 109:
#line 396 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 110:
#line 397 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 111:
#line 401 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 112:
#line 402 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 113:
#line 403 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 114:
#line 404 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 115:
#line 405 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 116:
#line 406 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 117:
#line 407 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 118:
#line 408 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 119:
#line 409 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 120:
#line 410 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 121:
#line 411 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 122:
#line 412 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 123:
#line 413 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 124:
#line 414 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 125:
#line 415 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 126:
#line 416 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 128:
#line 421 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 129:
#line 422 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 131:
#line 430 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 132:
#line 434 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 133:
#line 435 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 134:
#line 436 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 135:
#line 437 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 136:
#line 438 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 137:
#line 439 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 138:
#line 440 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 139:
#line 444 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 140:
#line 448 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 141:
#line 455 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 142:
#line 462 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 143:
#line 466 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 144:
#line 473 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 145:
#line 480 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 146:
#line 484 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 491 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 148:
#line 498 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 502 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 151:
#line 513 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 152:
#line 517 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 153:
#line 524 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 154:
#line 528 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 547 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 554 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 558 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 565 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 569 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 163:
#line 573 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 577 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 166:
#line 588 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 167:
#line 592 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 169:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 170:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 171:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 172:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 173:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 174:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 179:
#line 640 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 644 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 650 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 182:
#line 657 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 661 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 668 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 685 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 689 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 693 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 192:
#line 697 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 193:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 708 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 712 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 716 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 720 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 198:
#line 724 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 199:
#line 728 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 200:
#line 732 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 201:
#line 736 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 202:
#line 740 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 203:
#line 744 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 204:
#line 748 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 205:
#line 752 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 206:
#line 756 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 207:
#line 760 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 208:
#line 764 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 209:
#line 768 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 772 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 776 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 212:
#line 780 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 784 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 788 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 215:
#line 792 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 216:
#line 796 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 217:
#line 800 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 218:
#line 804 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 219:
#line 808 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 812 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 816 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 820 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 824 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 224:
#line 828 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 832 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 836 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 840 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 844 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 229:
#line 848 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 230:
#line 852 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 231:
#line 856 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 232:
#line 860 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 233:
#line 864 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 234:
#line 868 "Repil/IR/IR.jay"
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
#line 72 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 77 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_10()
#line 82 "Repil/IR/IR.jay"
{
        var g = (GlobalVariable)yyVals[0+yyTop];
        module.GlobalVariables[g.Symbol] = g;
    }

void case_14()
#line 96 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-5+yyTop]] = m;
    }

void case_16()
#line 105 "Repil/IR/IR.jay"
{
        var m = SymsAdd (yyVals[-1+yyTop], Symbol.Intern("_f"), yyVals[-3+yyTop]);
        module.Metadata[(Symbol)yyVals[-6+yyTop]] = m;
    }

void case_18()
#line 120 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = NewSyms (t.Item1, t.Item2);
    }

void case_19()
#line 125 "Repil/IR/IR.jay"
{
        var t = (Tuple<object, object>)yyVals[0+yyTop];
        yyVal = SymsAdd (yyVals[-2+yyTop], t.Item1, t.Item2);
    }

void case_51()
#line 209 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    6,    9,    9,   14,
   14,   14,   14,   14,   14,   14,   13,   13,    8,    8,
    8,    8,    8,   16,   16,   16,    7,    7,   18,   18,
   18,   18,   18,   18,   18,   18,   18,   18,   18,   18,
    3,   19,   19,   20,   20,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   11,   11,   21,
   21,   22,   22,    4,    4,    4,    4,    4,    4,    4,
    5,    5,    5,   23,   23,   26,   26,   26,   26,   27,
   27,   28,   28,   28,   28,   10,   10,   24,   24,   29,
   30,   30,   30,   30,   30,   30,   30,   30,   30,   30,
   31,   31,   31,   31,   31,   31,   31,   31,   31,   31,
   31,   31,   31,   31,   31,   31,   32,   32,   32,   33,
   33,   12,   12,   12,   12,   12,   12,   12,   12,   12,
   37,   17,   17,   38,   36,   36,   39,   35,   35,   40,
   34,   34,   25,   25,   41,   41,   41,   41,   42,   42,
   44,   44,   44,   44,   46,   47,   47,   48,   48,   48,
   48,   48,   48,   48,   15,   15,   49,   49,   50,   50,
   51,   52,   52,   53,   54,   54,   55,   55,   43,   43,
   43,   43,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   45,   45,   45,   45,   45,   45,   45,
   45,   45,   45,   45,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    1,
    6,    5,    6,    6,    7,    7,   10,    1,    3,    3,
    3,    3,    3,    3,    6,    5,    2,    3,    1,    2,
    3,    3,    3,    1,    1,    1,    1,    2,    1,    1,
    1,    1,    1,    1,    1,    3,    1,    1,    1,    4,
    3,    1,    3,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    4,    2,    1,    5,    5,    1,
    3,    1,    1,   11,   11,   11,   10,   12,   12,   13,
    7,    8,    8,    1,    3,    1,    2,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    9,    1,    1,    1,    1,    1,    1,    1,    3,    3,
    2,    2,    1,    2,    1,    3,    2,    1,    3,    1,
    1,    3,    1,    2,    2,    3,    1,    2,    1,    2,
    1,    2,    3,    4,    1,    1,    3,    2,    3,    3,
    3,    2,    4,    5,    1,    3,    1,    1,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    2,    7,
    2,    7,    5,    6,    5,    5,    4,    6,    7,    8,
    7,    4,    5,    6,    5,    5,    4,    4,    5,    6,
    7,    6,    6,    7,    5,    6,    5,    5,    6,    3,
    5,    7,    4,    5,    6,    6,    4,    7,    5,    6,
    4,    4,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    8,    9,   10,    0,    0,    0,    0,    0,    0,
   55,   67,   57,   58,   59,   60,   61,   62,   63,   64,
    0,    0,    0,    0,   56,    0,    0,    0,    0,    0,
    3,    4,    0,    0,   96,   97,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   66,    0,    0,
    0,    0,    0,    5,    6,    0,    7,    0,    0,    0,
    0,    0,   51,    0,    0,    0,    0,   73,    0,    0,
   70,    0,    0,    0,    0,    0,    0,    0,   18,    0,
    0,    0,   36,   35,   12,    0,    0,   29,   34,    0,
    0,    0,    0,   88,   89,    0,    0,    0,   84,   65,
    0,    0,    0,    0,    0,   49,   39,   40,   41,   42,
   43,   44,   45,    0,   37,  134,  133,  135,  136,  137,
  132,  138,    0,    0,    0,    0,    0,   14,    0,    0,
    0,   30,   13,    0,  129,  128,  127,  142,    0,   68,
   69,  100,    0,    0,   98,   92,   93,   95,   94,    0,
   90,    0,    0,   71,    0,    0,    0,    0,   11,   38,
  145,    0,    0,    0,  148,    0,   22,    0,   20,   23,
   24,   19,   16,   15,   33,   32,   31,    0,    0,    0,
    0,   99,   91,    0,    0,   85,    0,    0,    0,   46,
  178,  177,    0,  175,  140,    0,  147,    0,  139,    0,
    0,    0,    0,   27,    0,    0,    0,    0,    0,    0,
   50,    0,  146,  149,    0,   26,    0,    0,    0,   28,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  153,
    0,    0,  159,    0,    0,    0,    0,  176,    0,   25,
    0,    0,    0,    0,  191,    0,    0,  189,    0,  187,
  188,    0,    0,  185,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  101,  102,  103,  104,
  105,  106,  107,  108,  109,  110,    0,  111,  112,  123,
  124,  125,  126,  114,  116,  117,  118,  119,  115,  113,
  121,  122,  120,    0,    0,    0,    0,    0,   77,  154,
    0,  160,    0,    0,    0,    0,    0,    0,    0,   76,
    0,  141,    0,    0,    0,    0,  186,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  179,    0,  165,    0,    0,    0,   74,
    0,   75,   79,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  202,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   78,
   80,    0,    0,  193,    0,  203,  229,    0,  209,  218,
    0,  206,  221,  205,  224,    0,    0,  215,  196,  217,
  233,    0,    0,  195,    0,  130,  144,    0,    0,    0,
    0,    0,    0,    0,  180,    0,    0,    0,    0,  166,
    0,    0,    0,    0,  182,  194,  230,  219,  225,  216,
  213,  226,    0,    0,    0,    0,  150,    0,  151,  212,
  204,    0,    0,    0,    0,    0,  168,    0,    0,    0,
  190,    0,  192,  183,    0,  214,  228,    0,    0,  181,
  222,    0,  170,  171,  169,    0,  167,    0,  184,    0,
  152,  173,    0,    0,    0,  174,    0,    0,    0,  131,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   35,   12,   13,   14,  124,   96,   88,   47,
   97,  147,  191,   89,  203,   98,  527,  125,   54,   55,
   80,   81,  108,  154,  269,  109,  160,  161,  155,  337,
  354,  416,  497,  528,  174,  172,  288,  451,  513,  529,
  270,  271,  272,  273,  274,  417,  509,  510,  204,  413,
  414,  514,  515,  293,  294,
  };
  protected static readonly short [] yySindex = {         -121,
  -10, -108,   12,   22,   29, 2228, 2258, -192,    0, -121,
    0,    0,    0,    0, -147,   56,   75,  -91, -129,  -26,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2441, 2441,  -82,  -69,    0,  147,  -22, 2441,  -19,  144,
    0,    0,  -48,   -8,    0,    0,  -51,  133,  217,  -20,
  135,  -17,  147,  -25,  236,   18,   24,    0,  240, 2186,
  -16,  248,  141,    0,    0, 2441,    0, -213,  257,  182,
 2293,  270,    0, 2441, 2441, 2441, 2062,    0,  147,   23,
    0,  280, 2171, 2440,   80,  256,  263,   97,    0, -213,
 2336,    0,    0,    0,    0,  -11,  462,    0,    0, 2171,
  147,   33,    6,    0,    0, -136,  -30,  104,    0,    0,
 2186, 2171,  140,  271,  295,    0,    0,    0,    0,    0,
    0,    0,    0,  224,    0,    0,    0,    0,    0,    0,
    0,    0, 2491, 2441,  299,  388,   66,    0, -213,  157,
   -2,    0,    0, 2369,    0,    0,    0,    0,  169,    0,
    0,    0,   47, -214,    0,    0,    0,    0,    0, -102,
    0, -136, 2171,    0,  181, -136,   91, 1049,    0,    0,
    0,   -5,   80,   14,    0,    4,    0,  311,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  -91, -214,   83,
 -107,    0,    0,   47, -214,    0,   47,   47,   47,    0,
    0,    0,  231,    0,    0, 2491,    0, 2441,    0,  101,
 2041,   47, -103,    0,   86, 3028, -117,  -96,   47,   47,
    0, 1049,    0,    0,   89,    0,  233, -115, 3028,    0,
  302, 2491, -208, 2491,  314, 2441,  314, 2441,  314, 2441,
 2441, 2441,  314,  205, 2441, 2441, 2441, 2491, 2491, 2491,
 2441, 2441, 2491, 1095, 2491, 2491, 2491, 2491, 2491, 2491,
 2491, 2491,  432, 2406, 2441, 2441, 2441,  -29, 1080,    0,
 3028,   89,    0,   89, 3028,  -95, 3028,    0,   93,    0,
 3028,  -94, 1157, 3412,    0,   92, 1589,    0,  324,    0,
    0,  462,  314,    0,  462,  462,  314,  462,  462,  314,
  462,  462,  462,  462,  314, 2441,  462,  462,  462,  462,
  325,  326,  327,   82,  129,  328, 2441,  162,   15,   16,
   17,   19,   20,   25,   28,   30,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 2441,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 2441,   10,  462,  781, 2441,    0,    0,
   89,    0,   93,   93, 1234, 3028, 1321, 1398, 3028,    0,
   89,    0,  331,  117,  339,  462,    0,  346,  350,  462,
  351,  360,  462,  362,  364,  365,  372,  462,  462,  375,
  376,  379,  394, 2491, 2491, 2491,   87, 2441, 2441,  184,
 2491, 2441, 2441, 2441, 2441, 2441, 2441, 2441, 2441,  462,
  462, 1589,  395,    0,  398,    0,  403,  781,   93,    0,
 1475,    0,    0, 1562,   93,  117,  354, 1589,  405, 1589,
 1589,  406, 1589, 1589,  409, 1589, 1589, 1589, 1589,  412,
  413, 1589, 1589, 1589, 1589,    0,  414,  426,  190,   21,
  427,  429, 2491,  430,  147,  147,  147,  147,  147,  147,
  147,  147,  431,  434,  435,  374, 2491, 2308,  436,    0,
    0,  450, 2441,    0, 1589,    0,    0, 1589,    0,    0,
 1589,    0,    0,    0,    0, 1589, 1589,    0,    0,    0,
    0, 2491, 2491,    0,  142,    0,    0,  145,  150,  451,
 2491, 1589, 1589, 1589,    0,  453, 2402, 2025,  245,    0,
 2308,  117,  456, 2426,    0,    0,    0,    0,    0,    0,
    0,    0,  467,  253,  255, 2491,    0,  469,    0,    0,
    0,  423, 2491,  477, 2128, 2148,    0,   47, 2308,  254,
    0,  117,    0,    0, 2441,    0,    0,  469, 2491,    0,
    0, 2116,    0,    0,    0,   47,    0,   47,    0,  200,
    0,    0,  260,   47, 2491,    0,  474, 2491,  274,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  519,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 1348,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   -4,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  119,    0,
    0,    0,    0,    0,  480,    0,    0,    0,    0,    0,
    0,  344,    0,    0,    0,    0,  480,    0,    0,    0,
    9,  480,  480,    0,    0,    0,  146,    0,    0,    0,
    0,    0,    0,  316, 2055,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  281,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  480,    0,    0,    0,    0,  285,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   35,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   45,  143,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 1639,    0, 3105,    0,    0,    0,    0,  192,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  480,    0,    0,  480,  480,    0,  480,  480,    0,
  480,  480,  480,  480,    0,    0,  480,  480,  480,  480,
    0,    0,    0,  480,  480,    0,    0,  480,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  480,  480,    0,    0,    0,    0,
 1716,    0, 1803, 3182,    0,    0,    0,    0,    0,    0,
 3259,    0,    0,    0,    0,  480,    0,    0,    0,  480,
    0,    0,  480,    0,    0,    0,    0,  480,  480,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  480,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  480,
  480,    0, 2489,    0,    0,    0,    0,    0, 1880,    0,
    0,    0,    0,    0, 3336,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  480,
    0,    0,    0,    0,  421,  508,  585,  663,  750,  827,
  904,  992,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  480,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 2566,    0,    0,
    0,    0,    0,  286,  480,    0,    0, 2643,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 2720,    0,    0,
    0,    0,    0,    0,    0, 2797,    0, 2874,    0,  480,
    0,    0,    0, 2951,    0,    0,    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  511,  486,    0,    0,    0,    0,  444,  452,  -84,
   -6,  -80,  -98,  386, -207,    0,  -41,  416,    0,    3,
    0,  433,  -28, -151, -142,  380,   37, -157, -110,    0,
    0,  126,    0, -488,    0,    0, -360,  148,  -97,   -3,
 -252,    0,  278,  279,  267,  134,   42,   27,  333,    0,
   95,    0,   44, -141, -168,
  };
  protected static readonly short [] yyTable = {            36,
   36,  189,  193,  227,  135,  275,   51,  281,   37,   39,
  195,   58,   70,  427,  199,  216,  360,   60,   74,  229,
   60,  153,   60,   60,   36,   53,  277,  366,  369,   99,
  360,   36,  144,   52,   81,   54,  175,  548,  206,   52,
   61,  144,  217,  192,   82,  219,  220,   58,   54,   99,
   15,   58,   53,   79,  113,  180,  286,  208,  190,   85,
  228,   86,   58,  110,   87,  472,  111,  101,  102,  103,
  107,  149,   18,  287,   58,  209,  107,  194,  192,  569,
  134,  198,   19,  165,  192,  152,  283,  202,  192,   20,
  213,  171,  207,  107,  150,  297,  218,  300,  151,   73,
  412,  305,  187,  212,   79,  107,  192,   40,  192,  192,
  224,   42,  360,  143,  360,  360,   43,  192,  276,  205,
   52,   58,  184,   58,  377,  397,  279,  173,  377,  282,
  202,  377,  365,   53,  367,   44,  377,  138,  368,  134,
  139,  202,   83,  133,  162,    1,    2,  163,   48,    3,
    4,  541,    5,   45,   46,  190,  107,  190,   54,   72,
   16,   17,   72,  152,  223,  215,    6,    7,  360,  215,
   58,  360,  398,  363,   56,  364,  215,  215,  215,    8,
  166,  559,  152,  163,  152,   54,   86,   57,   58,   86,
  285,   17,  289,  156,  157,  158,  159,  183,   45,   46,
  139,  173,  133,   58,   63,  401,  311,  312,  313,  188,
   64,  316,  163,  319,  320,  321,  322,  323,  324,  325,
  326,  197,  148,  421,  163,   58,  424,  453,  292,  295,
  296,  298,  299,  301,  302,  303,  304,  307,  308,  309,
  310,   58,   66,  565,  314,  315,   49,  318,   59,   50,
   65,   62,   69,   72,   82,   32,   68,   71,  355,  356,
   36,  142,  419,   84,   33,  156,  157,  158,  159,  357,
  142,  221,  425,  280,  222,   60,  222,  126,  127,   77,
  128,  129,  130,   75,  131,  538,  376,   83,  539,   76,
  380,  145,  146,  383,  558,   34,   90,  539,  388,  389,
  566,   81,   81,  222,   91,   81,   81,  132,   81,  100,
  400,   82,   82,  136,  570,   82,   82,  549,   82,  112,
  137,   87,   81,   81,   87,   21,  172,   32,   21,  172,
  410,  167,   82,   82,  168,   81,  126,  127,  181,  128,
  129,  130,  176,  131,  563,   82,  152,  411,  169,  200,
  211,   36,  446,  447,  448,  214,  210,  225,  230,  454,
  418,  190,  284,  372,  358,  215,  132,  374,  394,  395,
  396,  399,  495,   33,  426,  402,  403,  404,  193,  405,
  406,  286,  428,   55,  143,  407,  556,  143,  408,  430,
  409,  450,  450,  431,  433,  455,  456,  457,  458,  459,
  460,  461,  462,  434,   34,  436,  564,  437,  438,   83,
   83,  500,  373,   83,   83,  439,   83,  375,  442,  443,
  378,  379,  444,  381,  382,  506,  384,  385,  386,  387,
   83,   83,  390,  391,  392,  393,   32,  445,  466,  449,
   47,  467,  468,   83,  473,  192,  494,  134,  475,  478,
  521,  522,  481,  192,  554,  486,  487,  492,   17,   17,
   54,  508,   17,   17,  412,   17,  173,   21,  143,  493,
  498,  202,  499,  501,  502,  511,   22,  503,  504,   17,
   17,  415,  114,   23,   24,   25,   26,   27,   28,   29,
   30,  551,   17,  512,  526,  523,  533,  524,  115,  542,
  535,  429,  525,   58,  508,  432,  545,  173,  435,  546,
  133,  547,  549,  440,  441,  550,  552,  568,    1,   54,
   41,  134,  116,  567,  182,  117,  118,  119,  120,  121,
  122,  123,  508,   67,  141,  463,  464,  465,  560,  170,
  306,  140,  196,  164,  536,  561,  452,   54,  361,  362,
  371,  469,  540,  474,  278,  476,  477,  544,  479,  480,
  505,  482,  483,  484,  485,  557,    0,  488,  489,  490,
  491,    0,    0,    0,   47,  496,   21,    0,    0,    0,
    0,    0,    0,    0,  133,   22,    0,    0,    0,    0,
   47,    0,   23,   24,   25,   26,   27,   28,   29,   30,
  516,    0,    0,  517,    0,    0,  518,    0,    0,    0,
    0,  519,  520,    0,   47,  143,  143,   47,   47,   47,
   47,   47,   47,   47,   54,    0,    0,  530,  531,  532,
    0,    0,    0,  537,  290,  291,    0,    0,    0,    0,
    0,    0,    0,    0,  126,  127,  177,  128,  129,  130,
    0,  131,  143,  143,  143,    0,    0,    0,    0,    0,
  178,  555,  179,  143,    0,    0,  143,  143,  143,  143,
  143,    0,  143,  143,  132,    0,    0,  143,  143,    0,
    0,  143,  143,  143,  143,  143,  143,    0,    0,  143,
  143,  143,  231,  231,    0,  143,    0,    0,    0,  143,
  143,  143,   54,    0,  143,  143,  143,  143,  143,    0,
    0,  143,    0,  143,    0,    0,    0,    0,  126,  127,
    0,  128,  129,  130,  143,  131,    0,    0,    0,  231,
  231,  231,  145,  146,    0,  143,  143,  143,  143,    0,
  231,    0,    0,  231,  231,  231,  231,  231,  132,  231,
  231,    0,    0,    0,  231,  231,    0,    0,  231,  231,
  231,  231,  231,  231,    0,    0,  231,  231,  231,    0,
    0,    0,  231,    0,    0,    0,  231,  231,  231,  234,
  234,    0,  231,  231,  231,  231,    0,    0,  231,   54,
  231,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  231,  327,  328,  329,  330,  331,  332,  333,  334,
  335,  336,  231,  231,  231,  231,  234,  234,  234,    0,
   60,    0,    0,    0,    0,    0,    0,  234,    0,    0,
  234,  234,  234,  234,  234,    0,  234,  234,    0,    0,
  134,  234,  234,    0,    0,  234,  234,  234,  234,  234,
  234,    0,    0,  234,  234,  234,  223,  223,    0,  234,
    0,    0,    0,  234,  234,  234,   54,    0,    0,  234,
  234,  234,  234,    0,    0,  234,    0,  234,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  234,    0,
    0,    0,    0,  223,  223,  223,    0,    0,    0,  234,
  234,  234,  234,  133,  223,    0,    0,  223,  223,  223,
  223,  223,    0,  223,  223,    0,    0,    0,  223,  223,
    0,    0,  223,  223,  223,  223,  223,  223,    0,    0,
  223,  223,  223,    0,  207,  207,  223,    0,    0,    0,
  223,  223,  223,   54,    0,    0,  223,  223,  223,  223,
    0,    0,  223,    0,  223,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  223,    0,    0,    0,    0,
    0,  207,  207,  207,    0,    0,  223,  223,  223,  223,
    0,    0,  207,    0,    0,  207,  207,  207,  207,  207,
    0,  207,  207,    0,    0,    0,  207,  207,    0,    0,
  207,  207,  207,  207,  207,  207,    0,    0,  207,  207,
  207,    0,    0,    0,  207,    0,    0,    0,  207,  207,
  207,  208,  208,    0,  207,  207,  207,  207,    0,    0,
  207,   54,  207,    0,    0,    0,    0,  126,  127,    0,
  128,  129,  130,  207,  131,    0,    0,    0,    0,    0,
    0,  145,  146,    0,  207,  207,  207,  207,  208,  208,
  208,    0,    0,    0,    0,    0,    0,  132,    0,  208,
    0,    0,  208,  208,  208,  208,  208,    0,  208,  208,
    0,    0,    0,  208,  208,    0,    0,  208,  208,  208,
  208,  208,  208,    0,    0,  208,  208,  208,  232,  232,
    0,  208,    0,    0,    0,  208,  208,  208,  134,    0,
    0,  208,  208,  208,  208,    0,    0,  208,    0,  208,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  208,    0,    0,    0,    0,  232,  232,  232,    0,    0,
    0,  208,  208,  208,  208,    0,  232,    0,    0,  232,
  232,  232,  232,  232,   33,  232,  232,    0,    0,    0,
  232,  232,    0,    0,  232,  232,  232,  232,  232,  232,
    0,  133,  232,  232,  232,  227,  227,    0,  232,    0,
    0,    0,  232,  232,  232,   34,    0,    0,  232,  232,
  232,  232,    0,    0,  232,    0,  232,    0,    0,    0,
    0,    0,    0,    0,  359,    0,    0,  232,    0,    0,
    0,    0,  227,  227,  227,    0,    0,   32,  232,  232,
  232,  232,    0,  227,    0,    0,  227,  227,  227,  227,
  227,    0,  227,  227,    0,    0,    0,  227,  227,    0,
    0,  227,  227,  227,  227,  227,  227,    0,    0,  227,
  227,  227,    0,    0,    0,  227,    0,    0,    0,  227,
  227,  227,    0,  197,  197,  227,  227,  227,  227,    0,
    0,  227,    0,  227,    0,    0,    0,    0,    0,    0,
    0,  370,    0,    0,  227,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  227,  227,  227,  227,    0,
  197,  197,  197,    0,    0,  126,  127,    0,  128,  129,
  130,  197,  131,    0,  197,  197,  197,  197,  197,    0,
  197,  197,    0,  201,    0,  197,  197,    0,    0,  197,
  197,  197,  197,  197,  197,  132,    0,  197,  197,  197,
    0,    0,    0,  197,    0,    0,    0,  197,  197,  197,
    0,  231,    0,  197,  197,  197,  197,   21,  420,  197,
    0,  197,    0,    0,    0,    0,   22,    0,    0,    0,
    0,    0,  197,   23,   24,   25,   26,   27,   28,   29,
   30,    0,    0,  197,  197,  197,  197,   54,  232,  233,
  234,    0,    0,    0,    0,    0,    0,    0,    0,  235,
    0,    0,  236,  237,  238,  239,  240,   54,  241,  242,
    0,    0,    0,  243,  244,    0,    0,  245,  246,  247,
  248,  249,  250,    0,    0,  251,  252,  253,  231,    0,
    0,  254,    0,    0,    0,  255,  256,  257,    0,    0,
    0,  258,  259,  260,  261,  422,    0,  262,  317,  263,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  264,    0,    0,    0,    0,  232,  233,  234,    0,    0,
   54,  265,  266,  267,  268,    0,  235,    0,    0,  236,
  237,  238,  239,  240,    0,  241,  242,    0,    0,    0,
  243,  244,    0,    0,  245,  246,  247,  248,  249,  250,
    0,    0,  251,  252,  253,  231,    0,    0,  254,    0,
    0,    0,  255,  256,  257,    0,    0,    0,  258,  259,
  260,  261,  423,    0,  262,    0,  263,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  264,    0,    0,
    0,    0,  232,  233,  234,    0,    0,    0,  265,  266,
  267,  268,    0,  235,    0,    0,  236,  237,  238,  239,
  240,    0,  241,  242,    0,    0,    0,  243,  244,    0,
    0,  245,  246,  247,  248,  249,  250,    0,    0,  251,
  252,  253,    0,    0,    0,  254,    0,    0,    0,  255,
  256,  257,  231,    0,    0,  258,  259,  260,  261,  470,
    0,  262,    0,  263,   54,   54,    0,   54,   54,   54,
    0,   54,    0,    0,  264,    0,    0,    0,   54,   54,
    0,    0,    0,    0,    0,  265,  266,  267,  268,  232,
  233,  234,    0,    0,   54,    0,    0,    0,    0,    0,
  235,    0,    0,  236,  237,  238,  239,  240,  134,  241,
  242,    0,    0,    0,  243,  244,    0,    0,  245,  246,
  247,  248,  249,  250,    0,    0,  251,  252,  253,  231,
    0,    0,  254,    0,    0,    0,  255,  256,  257,    0,
    0,    0,  258,  259,  260,  261,  471,    0,  262,    0,
  263,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  264,    0,    0,    0,    0,  232,  233,  234,    0,
    0,  133,  265,  266,  267,  268,    0,  235,    0,    0,
  236,  237,  238,  239,  240,    0,  241,  242,    0,    0,
    0,  243,  244,    0,    0,  245,  246,  247,  248,  249,
  250,    0,    0,  251,  252,  253,  231,    0,    0,  254,
    0,    0,    0,  255,  256,  257,    0,    0,    0,  258,
  259,  260,  261,  157,    0,  262,    0,  263,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  264,    0,
    0,    0,    0,  232,  233,  234,    0,    0,    0,  265,
  266,  267,  268,    0,  235,    0,    0,  236,  237,  238,
  239,  240,    0,  241,  242,    0,    0,    0,  243,  244,
    0,    0,  245,  246,  247,  248,  249,  250,    0,    0,
  251,  252,  253,    0,    0,    0,  254,    0,    0,    0,
  255,  256,  257,  231,    0,    0,  258,  259,  260,  261,
  155,    0,  262,    0,  263,  126,  127,    0,  128,  129,
  130,    0,  131,    0,    0,  264,    0,    0,    0,  145,
  146,    0,    0,    0,    0,    0,  265,  266,  267,  268,
  232,  233,  234,    0,    0,  132,    0,    0,    0,    0,
    0,  235,    0,    0,  236,  237,  238,  239,  240,    0,
  241,  242,    0,    0,    0,  243,  244,    0,    0,  245,
  246,  247,  248,  249,  250,    0,    0,  251,  252,  253,
  157,    0,    0,  254,    0,    0,    0,  255,  256,  257,
    0,    0,    0,  258,  259,  260,  261,  158,    0,  262,
    0,  263,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  264,    0,    0,    0,    0,  157,  157,  157,
    0,    0,    0,  265,  266,  267,  268,    0,  157,    0,
    0,  157,  157,  157,  157,  157,    0,  157,  157,    0,
    0,    0,  157,  157,    0,    0,  157,  157,  157,  157,
  157,  157,    0,    0,  157,  157,  157,  155,    0,    0,
  157,    0,    0,    0,  157,  157,  157,    0,    0,    0,
  157,  157,  157,  157,  156,    0,  157,    0,  157,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  157,
    0,    0,    0,    0,  155,  155,  155,    0,    0,    0,
  157,  157,  157,  157,    0,  155,    0,    0,  155,  155,
  155,  155,  155,    0,  155,  155,    0,    0,    0,  155,
  155,    0,    0,  155,  155,  155,  155,  155,  155,    0,
    0,  155,  155,  155,    0,    0,   58,  155,    0,    0,
    0,  155,  155,  155,  158,    0,    0,  155,  155,  155,
  155,  226,    0,  155,  134,  155,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  155,    0,    0,    0,
  134,    0,  106,    0,    0,    0,    0,  155,  155,  155,
  155,  158,  158,  158,    0,    0,    0,    0,    0,    0,
    0,   33,  158,    0,    0,  158,  158,  158,  158,  158,
    0,  158,  158,    0,    0,    0,  158,  158,    0,    0,
  158,  158,  158,  158,  158,  158,    0,  133,  158,  158,
  158,  156,   34,    0,  158,    0,  562,    0,  158,  158,
  158,    0,    0,  133,  158,  158,  158,  158,    0,   58,
  158,    0,  158,    0,    0,  134,    0,    0,    0,   48,
    0,    0,    0,  158,   32,    0,    0,  134,  156,  156,
  156,    0,    0,    0,  158,  158,  158,  158,    0,  156,
    0,    0,  156,  156,  156,  156,  156,  134,  156,  156,
    0,    0,    0,  156,  156,    0,    0,  156,  156,  156,
  156,  156,  156,    0,    0,  156,  156,  156,    0,    0,
   33,  156,    0,    0,    0,  156,  156,  156,  133,    0,
    0,  156,  156,  156,  156,   33,    0,  156,    0,  156,
  133,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  156,   34,    0,    0,    0,    0,    0,    0,    0,    0,
  133,  156,  156,  156,  156,    0,   34,    0,    0,    0,
    0,  126,  127,    0,  128,  129,  130,   33,  131,    0,
    0,    0,    0,   32,    0,  145,  146,  126,  127,    0,
  128,  129,  130,    0,  131,    0,    0,    0,   32,    0,
    0,  132,    0,   48,    0,  201,    0,   33,   34,    0,
  156,  157,  158,  159,   21,    0,    0,  132,    0,   48,
    0,    0,    0,   22,    0,    0,    0,    0,  104,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   34,    0,
   32,    0,   33,   48,  105,    0,   48,   48,   48,   48,
   48,   48,   48,    0,    0,    0,    0,   33,    0,    0,
    0,    0,  126,  127,    0,  128,  129,  130,    0,  131,
   32,    0,    0,   34,  126,  127,    0,  128,  129,  130,
  201,  131,    0,    0,    0,   33,    0,    0,   34,  553,
    0,    0,  132,    0,  126,  127,    0,  128,  129,  130,
    0,  131,    0,    0,  132,   32,    0,   95,  145,  146,
    0,    0,    0,    0,    0,    0,   34,    0,   33,    0,
   32,    0,    0,   21,  132,    0,    0,    0,    0,    0,
    0,    0,   22,  156,  157,  158,  159,  104,   21,   23,
   24,   25,   26,   27,   28,   29,   30,   22,   32,   34,
    0,   33,    0,  105,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,    0,   78,    0,
    0,    0,    0,    0,    0,   33,    0,    0,    0,    0,
   21,   32,   34,    0,    0,    0,    0,    0,    0,   22,
   33,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,   34,    0,  543,   31,
   21,    0,    0,    0,   32,    0,    0,    0,    0,   22,
    0,   34,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,   32,   38,
   33,    0,    0,    0,    0,   92,   93,    0,    0,    0,
    0,    0,    0,   32,   22,   94,    0,    0,    0,    0,
   21,   23,   24,   25,   26,   27,   28,   29,   30,   22,
    0,   34,    0,    0,  507,    0,   23,   24,   25,   26,
   27,   28,   29,   30,    0,    0,    0,    0,   92,   93,
    0,    0,    0,    0,    0,    0,    0,   22,   94,    0,
    0,    0,    0,   32,   23,   24,   25,   26,   27,   28,
   29,   30,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   92,  185,    0,    0,    0,    0,    0,    0,    0,
   22,  186,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   21,  338,  339,    0,    0,    0,
    0,    0,    0,   22,  534,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   21,    0,
    0,    0,    0,    0,    0,    0,    0,   22,  114,    0,
    0,    0,    0,   21,   23,   24,   25,   26,   27,   28,
   29,   30,   22,    0,  115,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  116,    0,
    0,  117,  118,  119,  120,  121,  122,  123,    0,    0,
    0,    0,    0,   92,    0,    0,    0,    0,    0,    0,
  220,  220,   22,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,    0,  340,  341,
  342,  343,    0,    0,    0,    0,    0,  344,  345,  346,
  347,  348,  349,  350,  351,  352,  353,  220,  220,  220,
    0,    0,    0,    0,    0,    0,    0,    0,  220,    0,
    0,  220,  220,  220,  220,  220,    0,  220,  220,    0,
    0,    0,  220,  220,    0,    0,  220,  220,  220,  220,
  220,  220,    0,    0,  220,  220,  220,  210,  210,    0,
  220,    0,    0,    0,  220,  220,  220,    0,    0,    0,
  220,  220,  220,  220,    0,    0,  220,    0,  220,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  220,
    0,    0,    0,    0,  210,  210,  210,    0,    0,    0,
  220,  220,  220,  220,    0,  210,    0,    0,  210,  210,
  210,  210,  210,    0,  210,  210,    0,    0,    0,  210,
  210,    0,    0,  210,  210,  210,  210,  210,  210,    0,
    0,  210,  210,  210,  198,  198,    0,  210,    0,    0,
    0,  210,  210,  210,    0,    0,    0,  210,  210,  210,
  210,    0,    0,  210,    0,  210,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  210,    0,    0,    0,
    0,  198,  198,  198,    0,    0,    0,  210,  210,  210,
  210,    0,  198,    0,    0,  198,  198,  198,  198,  198,
    0,  198,  198,    0,    0,    0,  198,  198,    0,    0,
  198,  198,  198,  198,  198,  198,    0,    0,  198,  198,
  198,  211,  211,    0,  198,    0,    0,    0,  198,  198,
  198,    0,    0,    0,  198,  198,  198,  198,    0,    0,
  198,    0,  198,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  198,    0,    0,    0,    0,  211,  211,
  211,    0,    0,    0,  198,  198,  198,  198,    0,  211,
    0,    0,  211,  211,  211,  211,  211,    0,  211,  211,
    0,    0,    0,  211,  211,    0,    0,  211,  211,  211,
  211,  211,  211,    0,    0,  211,  211,  211,  199,  199,
    0,  211,    0,    0,    0,  211,  211,  211,    0,    0,
    0,  211,  211,  211,  211,    0,    0,  211,    0,  211,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  211,    0,    0,    0,    0,  199,  199,  199,    0,    0,
    0,  211,  211,  211,  211,    0,  199,    0,    0,  199,
  199,  199,  199,  199,    0,  199,  199,    0,    0,    0,
  199,  199,    0,    0,  199,  199,  199,  199,  199,  199,
    0,    0,  199,  199,  199,  201,  201,    0,  199,    0,
    0,    0,  199,  199,  199,    0,    0,    0,  199,  199,
  199,  199,    0,    0,  199,    0,  199,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  199,    0,    0,
    0,    0,  201,  201,  201,    0,    0,    0,  199,  199,
  199,  199,    0,  201,    0,    0,  201,  201,  201,  201,
  201,    0,  201,  201,    0,    0,    0,  201,  201,    0,
    0,  201,  201,  201,  201,  201,  201,    0,    0,  201,
  201,  201,  200,  200,    0,  201,    0,    0,    0,  201,
  201,  201,    0,    0,    0,  201,  201,  201,  201,    0,
    0,  201,    0,  201,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  201,    0,    0,    0,    0,  200,
  200,  200,    0,    0,    0,  201,  201,  201,  201,    0,
  200,    0,    0,  200,  200,  200,  200,  200,    0,  200,
  200,    0,    0,    0,  200,  200,    0,    0,  200,  200,
  200,  200,  200,  200,    0,    0,  200,  200,  200,  231,
    0,    0,  200,    0,    0,    0,  200,  200,  200,    0,
    0,    0,  200,  200,  200,  200,    0,    0,  200,    0,
  200,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  200,    0,    0,    0,    0,  232,  233,  234,    0,
    0,    0,  200,  200,  200,  200,    0,  235,    0,    0,
  236,  237,  238,  239,  240,    0,  241,  242,    0,    0,
    0,  243,  244,    0,    0,  245,  246,  247,  248,  249,
  250,    0,    0,  251,  252,  253,  161,    0,    0,  254,
    0,    0,    0,  255,  256,  257,    0,    0,    0,  258,
  259,  260,  261,    0,    0,  262,    0,  263,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  264,    0,
    0,    0,    0,  161,  161,  161,    0,    0,    0,  265,
  266,  267,  268,    0,  161,    0,    0,  161,  161,  161,
  161,  161,    0,  161,  161,    0,    0,    0,  161,  161,
    0,    0,  161,  161,  161,  161,  161,  161,    0,    0,
  161,  161,  161,  162,    0,    0,  161,    0,    0,    0,
  161,  161,  161,    0,    0,    0,  161,  161,  161,  161,
    0,    0,  161,    0,  161,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  161,    0,    0,    0,    0,
  162,  162,  162,    0,    0,    0,  161,  161,  161,  161,
    0,  162,    0,    0,  162,  162,  162,  162,  162,    0,
  162,  162,    0,    0,    0,  162,  162,    0,    0,  162,
  162,  162,  162,  162,  162,    0,    0,  162,  162,  162,
  163,    0,    0,  162,    0,    0,    0,  162,  162,  162,
    0,    0,    0,  162,  162,  162,  162,    0,    0,  162,
    0,  162,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  162,    0,    0,    0,    0,  163,  163,  163,
    0,    0,    0,  162,  162,  162,  162,    0,  163,    0,
    0,  163,  163,  163,  163,  163,    0,  163,  163,    0,
    0,    0,  163,  163,    0,    0,  163,  163,  163,  163,
  163,  163,    0,    0,  163,  163,  163,  164,    0,    0,
  163,    0,    0,    0,  163,  163,  163,    0,    0,    0,
  163,  163,  163,  163,    0,    0,  163,    0,  163,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  163,
    0,    0,    0,    0,  164,  164,  164,    0,    0,    0,
  163,  163,  163,  163,    0,  164,    0,    0,  164,  164,
  164,  164,  164,    0,  164,  164,    0,    0,    0,  164,
  164,    0,    0,  164,  164,  164,  164,  164,  164,    0,
    0,  164,  164,  164,    0,    0,    0,  164,    0,    0,
    0,  164,  164,  164,    0,    0,    0,  164,  164,  164,
  164,    0,    0,  164,    0,  164,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  164,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  164,  164,  164,
  164,  235,    0,    0,  236,  237,  238,  239,  240,    0,
  241,  242,    0,    0,    0,  243,  244,    0,    0,  245,
  246,  247,  248,  249,  250,    0,    0,  251,  252,  253,
    0,    0,    0,  254,    0,    0,    0,  255,  256,  257,
    0,    0,    0,  258,  259,  260,  261,    0,    0,  262,
    0,  263,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  264,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  265,  266,  267,  268,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  153,  160,  211,   85,  123,   33,  123,    6,    7,
  162,   42,   33,  374,  166,  123,  269,   40,   44,  123,
   40,  106,   40,   40,   31,   32,  123,  123,  123,   71,
  283,   38,   44,   31,    0,   40,  134,  526,   44,   44,
   38,   44,  194,  154,    0,  197,  198,   42,   40,   91,
   61,   42,   44,   60,   83,  136,  265,   44,  273,   66,
  212,  275,   42,   41,  278,  426,   44,   74,   75,   76,
   77,  100,   61,  282,   42,   62,   83,  162,  189,  568,
   60,  166,   61,  112,  195,  300,  229,  168,  199,   61,
  189,  133,  173,  100,   62,  237,  195,  239,   93,  125,
   91,  243,  144,  188,  111,  112,  217,  300,  219,  220,
  208,  259,  365,  125,  367,  368,   61,  228,  217,  125,
  125,   42,  125,   42,  293,   44,  225,  134,  297,  228,
  211,  300,  275,  125,  277,   61,  305,   41,  281,   60,
   44,  222,    0,  123,   41,  267,  268,   44,  278,  271,
  272,  512,  274,  290,  291,  273,  163,  273,   40,   41,
  269,  270,   44,  300,  206,  273,  288,  289,  421,  273,
   42,  424,   44,  272,  257,  274,  273,  273,  273,  301,
   41,  542,  300,   44,  300,   40,   41,  257,   42,   44,
  232,    0,  234,  296,  297,  298,  299,   41,  290,  291,
   44,  208,  123,   42,   61,   44,  248,  249,  250,   41,
  259,  253,   44,  255,  256,  257,  258,  259,  260,  261,
  262,   41,   97,  366,   44,   42,  369,   44,  235,  236,
  237,  238,  239,  240,  241,  242,  243,  244,  245,  246,
  247,   42,  294,   44,  251,  252,  273,  254,  271,  276,
  259,  271,  273,  271,  271,  123,   40,  123,  265,  266,
  267,  273,  361,  123,   60,  296,  297,  298,  299,  267,
  273,   41,  371,   41,   44,   40,   44,  257,  258,   40,
  260,  261,  262,  266,  264,   41,  293,   40,   44,  266,
  297,  271,  272,  300,   41,   91,   40,   44,  305,  306,
   41,  267,  268,   44,  123,  271,  272,  287,  274,   40,
  317,  267,  268,   58,   41,  271,  272,   44,  274,   40,
   58,   41,  288,  289,   44,   41,   41,  123,   44,   44,
  337,   61,  288,  289,   40,  301,  257,  258,  273,  260,
  261,  262,   44,  264,  552,  301,  300,  354,  125,  259,
   40,  358,  394,  395,  396,  273,  353,  257,  273,  401,
  358,  273,   61,  272,  394,  273,  287,   44,   44,   44,
   44,   44,  352,   60,   44,  361,  361,  361,  536,  361,
  361,  265,   44,   40,   41,  361,  538,   44,  361,   44,
  361,  398,  399,   44,   44,  402,  403,  404,  405,  406,
  407,  408,  409,   44,   91,   44,  558,   44,   44,  267,
  268,  453,  287,  271,  272,   44,  274,  292,   44,   44,
  295,  296,   44,  298,  299,  467,  301,  302,  303,  304,
  288,  289,  307,  308,  309,  310,  123,   44,   44,  353,
  125,   44,   40,  301,   91,  556,  257,   60,   44,   44,
  492,  493,   44,  564,  535,   44,   44,   44,  267,  268,
   40,  468,  271,  272,   91,  274,  473,  263,  125,   44,
   44,  552,   44,   44,   44,   40,  272,   44,   44,  288,
  289,  356,  259,  279,  280,  281,  282,  283,  284,  285,
  286,  533,  301,   44,   44,  354,   44,  353,  275,   44,
  507,  376,  353,   42,  511,  380,   40,  514,  383,  257,
  123,  257,   44,  388,  389,   93,   40,   44,    0,   40,
   10,   60,  299,  565,  139,  302,  303,  304,  305,  306,
  307,  308,  539,   48,   91,  410,  411,  412,  545,  124,
  336,   90,  163,  111,  508,  549,  399,   40,  271,  271,
  284,  418,  511,  428,  222,  430,  431,  514,  433,  434,
  466,  436,  437,  438,  439,  539,   -1,  442,  443,  444,
  445,   -1,   -1,   -1,  259,  450,  263,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  123,  272,   -1,   -1,   -1,   -1,
  275,   -1,  279,  280,  281,  282,  283,  284,  285,  286,
  475,   -1,   -1,  478,   -1,   -1,  481,   -1,   -1,   -1,
   -1,  486,  487,   -1,  299,  272,  273,  302,  303,  304,
  305,  306,  307,  308,   40,   -1,   -1,  502,  503,  504,
   -1,   -1,   -1,  508,  321,  322,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,  260,  261,  262,
   -1,  264,  309,  310,  311,   -1,   -1,   -1,   -1,   -1,
  273,  536,  275,  320,   -1,   -1,  323,  324,  325,  326,
  327,   -1,  329,  330,  287,   -1,   -1,  334,  335,   -1,
   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,  346,
  347,  348,  272,  273,   -1,  352,   -1,   -1,   -1,  356,
  357,  358,   40,   -1,  361,  362,  363,  364,  365,   -1,
   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,  257,  258,
   -1,  260,  261,  262,  381,  264,   -1,   -1,   -1,  309,
  310,  311,  271,  272,   -1,  392,  393,  394,  395,   -1,
  320,   -1,   -1,  323,  324,  325,  326,  327,  287,  329,
  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,
  340,  341,  342,  343,   -1,   -1,  346,  347,  348,   -1,
   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,  272,
  273,   -1,  362,  363,  364,  365,   -1,   -1,  368,   40,
  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  381,  371,  372,  373,  374,  375,  376,  377,  378,
  379,  380,  392,  393,  394,  395,  309,  310,  311,   -1,
   40,   -1,   -1,   -1,   -1,   -1,   -1,  320,   -1,   -1,
  323,  324,  325,  326,  327,   -1,  329,  330,   -1,   -1,
   60,  334,  335,   -1,   -1,  338,  339,  340,  341,  342,
  343,   -1,   -1,  346,  347,  348,  272,  273,   -1,  352,
   -1,   -1,   -1,  356,  357,  358,   40,   -1,   -1,  362,
  363,  364,  365,   -1,   -1,  368,   -1,  370,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,
   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,  392,
  393,  394,  395,  123,  320,   -1,   -1,  323,  324,  325,
  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,
   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,
  346,  347,  348,   -1,  272,  273,  352,   -1,   -1,   -1,
  356,  357,  358,   40,   -1,   -1,  362,  363,  364,  365,
   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,   -1,
   -1,  309,  310,  311,   -1,   -1,  392,  393,  394,  395,
   -1,   -1,  320,   -1,   -1,  323,  324,  325,  326,  327,
   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,
  338,  339,  340,  341,  342,  343,   -1,   -1,  346,  347,
  348,   -1,   -1,   -1,  352,   -1,   -1,   -1,  356,  357,
  358,  272,  273,   -1,  362,  363,  364,  365,   -1,   -1,
  368,   40,  370,   -1,   -1,   -1,   -1,  257,  258,   -1,
  260,  261,  262,  381,  264,   -1,   -1,   -1,   -1,   -1,
   -1,  271,  272,   -1,  392,  393,  394,  395,  309,  310,
  311,   -1,   -1,   -1,   -1,   -1,   -1,  287,   -1,  320,
   -1,   -1,  323,  324,  325,  326,  327,   -1,  329,  330,
   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,
  341,  342,  343,   -1,   -1,  346,  347,  348,  272,  273,
   -1,  352,   -1,   -1,   -1,  356,  357,  358,   60,   -1,
   -1,  362,  363,  364,  365,   -1,   -1,  368,   -1,  370,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  381,   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,
   -1,  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,
  324,  325,  326,  327,   60,  329,  330,   -1,   -1,   -1,
  334,  335,   -1,   -1,  338,  339,  340,  341,  342,  343,
   -1,  123,  346,  347,  348,  272,  273,   -1,  352,   -1,
   -1,   -1,  356,  357,  358,   91,   -1,   -1,  362,  363,
  364,  365,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  125,   -1,   -1,  381,   -1,   -1,
   -1,   -1,  309,  310,  311,   -1,   -1,  123,  392,  393,
  394,  395,   -1,  320,   -1,   -1,  323,  324,  325,  326,
  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,
   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,   -1,   -1,   -1,  356,
  357,  358,   -1,  272,  273,  362,  363,  364,  365,   -1,
   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  125,   -1,   -1,  381,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  392,  393,  394,  395,   -1,
  309,  310,  311,   -1,   -1,  257,  258,   -1,  260,  261,
  262,  320,  264,   -1,  323,  324,  325,  326,  327,   -1,
  329,  330,   -1,  275,   -1,  334,  335,   -1,   -1,  338,
  339,  340,  341,  342,  343,  287,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,
   -1,  272,   -1,  362,  363,  364,  365,  263,  125,  368,
   -1,  370,   -1,   -1,   -1,   -1,  272,   -1,   -1,   -1,
   -1,   -1,  381,  279,  280,  281,  282,  283,  284,  285,
  286,   -1,   -1,  392,  393,  394,  395,   40,  309,  310,
  311,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  320,
   -1,   -1,  323,  324,  325,  326,  327,   60,  329,  330,
   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,
  341,  342,  343,   -1,   -1,  346,  347,  348,  272,   -1,
   -1,  352,   -1,   -1,   -1,  356,  357,  358,   -1,   -1,
   -1,  362,  363,  364,  365,  125,   -1,  368,  354,  370,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  381,   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,
  123,  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,
  324,  325,  326,  327,   -1,  329,  330,   -1,   -1,   -1,
  334,  335,   -1,   -1,  338,  339,  340,  341,  342,  343,
   -1,   -1,  346,  347,  348,  272,   -1,   -1,  352,   -1,
   -1,   -1,  356,  357,  358,   -1,   -1,   -1,  362,  363,
  364,  365,  125,   -1,  368,   -1,  370,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,
   -1,   -1,  309,  310,  311,   -1,   -1,   -1,  392,  393,
  394,  395,   -1,  320,   -1,   -1,  323,  324,  325,  326,
  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,
   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,  346,
  347,  348,   -1,   -1,   -1,  352,   -1,   -1,   -1,  356,
  357,  358,  272,   -1,   -1,  362,  363,  364,  365,  125,
   -1,  368,   -1,  370,  257,  258,   -1,  260,  261,  262,
   -1,  264,   -1,   -1,  381,   -1,   -1,   -1,  271,  272,
   -1,   -1,   -1,   -1,   -1,  392,  393,  394,  395,  309,
  310,  311,   -1,   -1,  287,   -1,   -1,   -1,   -1,   -1,
  320,   -1,   -1,  323,  324,  325,  326,  327,   60,  329,
  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,
  340,  341,  342,  343,   -1,   -1,  346,  347,  348,  272,
   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,   -1,
   -1,   -1,  362,  363,  364,  365,  125,   -1,  368,   -1,
  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  381,   -1,   -1,   -1,   -1,  309,  310,  311,   -1,
   -1,  123,  392,  393,  394,  395,   -1,  320,   -1,   -1,
  323,  324,  325,  326,  327,   -1,  329,  330,   -1,   -1,
   -1,  334,  335,   -1,   -1,  338,  339,  340,  341,  342,
  343,   -1,   -1,  346,  347,  348,  272,   -1,   -1,  352,
   -1,   -1,   -1,  356,  357,  358,   -1,   -1,   -1,  362,
  363,  364,  365,  125,   -1,  368,   -1,  370,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,
   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,  392,
  393,  394,  395,   -1,  320,   -1,   -1,  323,  324,  325,
  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,
   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,
  346,  347,  348,   -1,   -1,   -1,  352,   -1,   -1,   -1,
  356,  357,  358,  272,   -1,   -1,  362,  363,  364,  365,
  125,   -1,  368,   -1,  370,  257,  258,   -1,  260,  261,
  262,   -1,  264,   -1,   -1,  381,   -1,   -1,   -1,  271,
  272,   -1,   -1,   -1,   -1,   -1,  392,  393,  394,  395,
  309,  310,  311,   -1,   -1,  287,   -1,   -1,   -1,   -1,
   -1,  320,   -1,   -1,  323,  324,  325,  326,  327,   -1,
  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,
  339,  340,  341,  342,  343,   -1,   -1,  346,  347,  348,
  272,   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,
   -1,   -1,   -1,  362,  363,  364,  365,  125,   -1,  368,
   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  381,   -1,   -1,   -1,   -1,  309,  310,  311,
   -1,   -1,   -1,  392,  393,  394,  395,   -1,  320,   -1,
   -1,  323,  324,  325,  326,  327,   -1,  329,  330,   -1,
   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,  341,
  342,  343,   -1,   -1,  346,  347,  348,  272,   -1,   -1,
  352,   -1,   -1,   -1,  356,  357,  358,   -1,   -1,   -1,
  362,  363,  364,  365,  125,   -1,  368,   -1,  370,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,
   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,
  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,  324,
  325,  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,
  335,   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,
   -1,  346,  347,  348,   -1,   -1,   42,  352,   -1,   -1,
   -1,  356,  357,  358,  272,   -1,   -1,  362,  363,  364,
  365,   41,   -1,  368,   60,  370,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,
   60,   -1,   41,   -1,   -1,   -1,   -1,  392,  393,  394,
  395,  309,  310,  311,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   60,  320,   -1,   -1,  323,  324,  325,  326,  327,
   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,
  338,  339,  340,  341,  342,  343,   -1,  123,  346,  347,
  348,  272,   91,   -1,  352,   -1,   41,   -1,  356,  357,
  358,   -1,   -1,  123,  362,  363,  364,  365,   -1,   42,
  368,   -1,  370,   -1,   -1,   60,   -1,   -1,   -1,  125,
   -1,   -1,   -1,  381,  123,   -1,   -1,   60,  309,  310,
  311,   -1,   -1,   -1,  392,  393,  394,  395,   -1,  320,
   -1,   -1,  323,  324,  325,  326,  327,   60,  329,  330,
   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,
  341,  342,  343,   -1,   -1,  346,  347,  348,   -1,   -1,
   60,  352,   -1,   -1,   -1,  356,  357,  358,  123,   -1,
   -1,  362,  363,  364,  365,   60,   -1,  368,   -1,  370,
  123,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  381,   91,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  123,  392,  393,  394,  395,   -1,   91,   -1,   -1,   -1,
   -1,  257,  258,   -1,  260,  261,  262,   60,  264,   -1,
   -1,   -1,   -1,  123,   -1,  271,  272,  257,  258,   -1,
  260,  261,  262,   -1,  264,   -1,   -1,   -1,  123,   -1,
   -1,  287,   -1,  259,   -1,  275,   -1,   60,   91,   -1,
  296,  297,  298,  299,  263,   -1,   -1,  287,   -1,  275,
   -1,   -1,   -1,  272,   -1,   -1,   -1,   -1,  277,   -1,
  279,  280,  281,  282,  283,  284,  285,  286,   91,   -1,
  123,   -1,   60,  299,  293,   -1,  302,  303,  304,  305,
  306,  307,  308,   -1,   -1,   -1,   -1,   60,   -1,   -1,
   -1,   -1,  257,  258,   -1,  260,  261,  262,   -1,  264,
  123,   -1,   -1,   91,  257,  258,   -1,  260,  261,  262,
  275,  264,   -1,   -1,   -1,   60,   -1,   -1,   91,  272,
   -1,   -1,  287,   -1,  257,  258,   -1,  260,  261,  262,
   -1,  264,   -1,   -1,  287,  123,   -1,  125,  271,  272,
   -1,   -1,   -1,   -1,   -1,   -1,   91,   -1,   60,   -1,
  123,   -1,   -1,  263,  287,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  272,  296,  297,  298,  299,  277,  263,  279,
  280,  281,  282,  283,  284,  285,  286,  272,  123,   91,
   -1,   60,   -1,  293,  279,  280,  281,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,  293,   -1,
   -1,   -1,   -1,   -1,   -1,   60,   -1,   -1,   -1,   -1,
  263,  123,   91,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   60,   -1,   -1,   -1,   -1,   -1,  279,  280,  281,  282,
  283,  284,  285,  286,   -1,   -1,   91,   -1,   93,  292,
  263,   -1,   -1,   -1,  123,   -1,   -1,   -1,   -1,  272,
   -1,   91,   -1,   -1,   -1,   -1,  279,  280,  281,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,  123,  292,
   60,   -1,   -1,   -1,   -1,  263,  264,   -1,   -1,   -1,
   -1,   -1,   -1,  123,  272,  273,   -1,   -1,   -1,   -1,
  263,  279,  280,  281,  282,  283,  284,  285,  286,  272,
   -1,   91,   -1,   -1,  277,   -1,  279,  280,  281,  282,
  283,  284,  285,  286,   -1,   -1,   -1,   -1,  263,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,
   -1,   -1,   -1,  123,  279,  280,  281,  282,  283,  284,
  285,  286,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  263,  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,   -1,  279,  280,  281,
  282,  283,  284,  285,  286,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  263,  260,  261,   -1,   -1,   -1,
   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,
  279,  280,  281,  282,  283,  284,  285,  286,  263,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,  259,   -1,
   -1,   -1,   -1,  263,  279,  280,  281,  282,  283,  284,
  285,  286,  272,   -1,  275,   -1,   -1,   -1,   -1,  279,
  280,  281,  282,  283,  284,  285,  286,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  299,   -1,
   -1,  302,  303,  304,  305,  306,  307,  308,   -1,   -1,
   -1,   -1,   -1,  263,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,  272,   -1,   -1,   -1,   -1,   -1,   -1,  279,
  280,  281,  282,  283,  284,  285,  286,   -1,  373,  374,
  375,  376,   -1,   -1,   -1,   -1,   -1,  382,  383,  384,
  385,  386,  387,  388,  389,  390,  391,  309,  310,  311,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  320,   -1,
   -1,  323,  324,  325,  326,  327,   -1,  329,  330,   -1,
   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,  341,
  342,  343,   -1,   -1,  346,  347,  348,  272,  273,   -1,
  352,   -1,   -1,   -1,  356,  357,  358,   -1,   -1,   -1,
  362,  363,  364,  365,   -1,   -1,  368,   -1,  370,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,
   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,
  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,  324,
  325,  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,
  335,   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,
   -1,  346,  347,  348,  272,  273,   -1,  352,   -1,   -1,
   -1,  356,  357,  358,   -1,   -1,   -1,  362,  363,  364,
  365,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,
   -1,  309,  310,  311,   -1,   -1,   -1,  392,  393,  394,
  395,   -1,  320,   -1,   -1,  323,  324,  325,  326,  327,
   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,
  338,  339,  340,  341,  342,  343,   -1,   -1,  346,  347,
  348,  272,  273,   -1,  352,   -1,   -1,   -1,  356,  357,
  358,   -1,   -1,   -1,  362,  363,  364,  365,   -1,   -1,
  368,   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  381,   -1,   -1,   -1,   -1,  309,  310,
  311,   -1,   -1,   -1,  392,  393,  394,  395,   -1,  320,
   -1,   -1,  323,  324,  325,  326,  327,   -1,  329,  330,
   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,
  341,  342,  343,   -1,   -1,  346,  347,  348,  272,  273,
   -1,  352,   -1,   -1,   -1,  356,  357,  358,   -1,   -1,
   -1,  362,  363,  364,  365,   -1,   -1,  368,   -1,  370,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  381,   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,
   -1,  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,
  324,  325,  326,  327,   -1,  329,  330,   -1,   -1,   -1,
  334,  335,   -1,   -1,  338,  339,  340,  341,  342,  343,
   -1,   -1,  346,  347,  348,  272,  273,   -1,  352,   -1,
   -1,   -1,  356,  357,  358,   -1,   -1,   -1,  362,  363,
  364,  365,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,
   -1,   -1,  309,  310,  311,   -1,   -1,   -1,  392,  393,
  394,  395,   -1,  320,   -1,   -1,  323,  324,  325,  326,
  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,   -1,
   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,  346,
  347,  348,  272,  273,   -1,  352,   -1,   -1,   -1,  356,
  357,  358,   -1,   -1,   -1,  362,  363,  364,  365,   -1,
   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,   -1,  309,
  310,  311,   -1,   -1,   -1,  392,  393,  394,  395,   -1,
  320,   -1,   -1,  323,  324,  325,  326,  327,   -1,  329,
  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,  339,
  340,  341,  342,  343,   -1,   -1,  346,  347,  348,  272,
   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,   -1,
   -1,   -1,  362,  363,  364,  365,   -1,   -1,  368,   -1,
  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  381,   -1,   -1,   -1,   -1,  309,  310,  311,   -1,
   -1,   -1,  392,  393,  394,  395,   -1,  320,   -1,   -1,
  323,  324,  325,  326,  327,   -1,  329,  330,   -1,   -1,
   -1,  334,  335,   -1,   -1,  338,  339,  340,  341,  342,
  343,   -1,   -1,  346,  347,  348,  272,   -1,   -1,  352,
   -1,   -1,   -1,  356,  357,  358,   -1,   -1,   -1,  362,
  363,  364,  365,   -1,   -1,  368,   -1,  370,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,
   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,  392,
  393,  394,  395,   -1,  320,   -1,   -1,  323,  324,  325,
  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,  335,
   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,   -1,
  346,  347,  348,  272,   -1,   -1,  352,   -1,   -1,   -1,
  356,  357,  358,   -1,   -1,   -1,  362,  363,  364,  365,
   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,   -1,
  309,  310,  311,   -1,   -1,   -1,  392,  393,  394,  395,
   -1,  320,   -1,   -1,  323,  324,  325,  326,  327,   -1,
  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,
  339,  340,  341,  342,  343,   -1,   -1,  346,  347,  348,
  272,   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,
   -1,   -1,   -1,  362,  363,  364,  365,   -1,   -1,  368,
   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  381,   -1,   -1,   -1,   -1,  309,  310,  311,
   -1,   -1,   -1,  392,  393,  394,  395,   -1,  320,   -1,
   -1,  323,  324,  325,  326,  327,   -1,  329,  330,   -1,
   -1,   -1,  334,  335,   -1,   -1,  338,  339,  340,  341,
  342,  343,   -1,   -1,  346,  347,  348,  272,   -1,   -1,
  352,   -1,   -1,   -1,  356,  357,  358,   -1,   -1,   -1,
  362,  363,  364,  365,   -1,   -1,  368,   -1,  370,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  381,
   -1,   -1,   -1,   -1,  309,  310,  311,   -1,   -1,   -1,
  392,  393,  394,  395,   -1,  320,   -1,   -1,  323,  324,
  325,  326,  327,   -1,  329,  330,   -1,   -1,   -1,  334,
  335,   -1,   -1,  338,  339,  340,  341,  342,  343,   -1,
   -1,  346,  347,  348,   -1,   -1,   -1,  352,   -1,   -1,
   -1,  356,  357,  358,   -1,   -1,   -1,  362,  363,  364,
  365,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  381,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  392,  393,  394,
  395,  320,   -1,   -1,  323,  324,  325,  326,  327,   -1,
  329,  330,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,
  339,  340,  341,  342,  343,   -1,   -1,  346,  347,  348,
   -1,   -1,   -1,  352,   -1,   -1,   -1,  356,  357,  358,
   -1,   -1,   -1,  362,  363,  364,  365,   -1,   -1,  368,
   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  381,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  392,  393,  394,  395,
  };

#line 872 "Repil/IR/IR.jay"

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
  public const int TYPE = 278;
  public const int HALF = 279;
  public const int FLOAT = 280;
  public const int DOUBLE = 281;
  public const int I1 = 282;
  public const int I8 = 283;
  public const int I16 = 284;
  public const int I32 = 285;
  public const int I64 = 286;
  public const int ZEROINITIALIZER = 287;
  public const int DEFINE = 288;
  public const int DECLARE = 289;
  public const int UNNAMED_ADDR = 290;
  public const int LOCAL_UNNAMED_ADDR = 291;
  public const int NOALIAS = 292;
  public const int ELLIPSIS = 293;
  public const int GLOBAL = 294;
  public const int CONSTANT = 295;
  public const int NONNULL = 296;
  public const int NOCAPTURE = 297;
  public const int WRITEONLY = 298;
  public const int READONLY = 299;
  public const int ATTRIBUTE_GROUP_REF = 300;
  public const int ATTRIBUTES = 301;
  public const int NORECURSE = 302;
  public const int NOUNWIND = 303;
  public const int READNONE = 304;
  public const int SPECULATABLE = 305;
  public const int SSP = 306;
  public const int UWTABLE = 307;
  public const int ARGMEMONLY = 308;
  public const int RET = 309;
  public const int BR = 310;
  public const int SWITCH = 311;
  public const int INDIRECTBR = 312;
  public const int INVOKE = 313;
  public const int RESUME = 314;
  public const int CATCHSWITCH = 315;
  public const int CATCHRET = 316;
  public const int CLEANUPRET = 317;
  public const int UNREACHABLE = 318;
  public const int FNEG = 319;
  public const int ADD = 320;
  public const int NUW = 321;
  public const int NSW = 322;
  public const int FADD = 323;
  public const int SUB = 324;
  public const int FSUB = 325;
  public const int MUL = 326;
  public const int FMUL = 327;
  public const int UDIV = 328;
  public const int SDIV = 329;
  public const int FDIV = 330;
  public const int UREM = 331;
  public const int SREM = 332;
  public const int FREM = 333;
  public const int SHL = 334;
  public const int LSHR = 335;
  public const int EXACT = 336;
  public const int ASHR = 337;
  public const int AND = 338;
  public const int OR = 339;
  public const int XOR = 340;
  public const int EXTRACTELEMENT = 341;
  public const int INSERTELEMENT = 342;
  public const int SHUFFLEVECTOR = 343;
  public const int EXTRACTVALUE = 344;
  public const int INSERTVALUE = 345;
  public const int ALLOCA = 346;
  public const int LOAD = 347;
  public const int STORE = 348;
  public const int FENCE = 349;
  public const int CMPXCHG = 350;
  public const int ATOMICRMW = 351;
  public const int GETELEMENTPTR = 352;
  public const int ALIGN = 353;
  public const int INBOUNDS = 354;
  public const int INRANGE = 355;
  public const int TRUNC = 356;
  public const int ZEXT = 357;
  public const int SEXT = 358;
  public const int FPTRUNC = 359;
  public const int FPEXT = 360;
  public const int TO = 361;
  public const int FPTOUI = 362;
  public const int FPTOSI = 363;
  public const int UITOFP = 364;
  public const int SITOFP = 365;
  public const int PTRTOINT = 366;
  public const int INTTOPTR = 367;
  public const int BITCAST = 368;
  public const int ADDRSPACECAST = 369;
  public const int ICMP = 370;
  public const int EQ = 371;
  public const int NE = 372;
  public const int UGT = 373;
  public const int UGE = 374;
  public const int ULT = 375;
  public const int ULE = 376;
  public const int SGT = 377;
  public const int SGE = 378;
  public const int SLT = 379;
  public const int SLE = 380;
  public const int FCMP = 381;
  public const int OEQ = 382;
  public const int OGT = 383;
  public const int OGE = 384;
  public const int OLT = 385;
  public const int OLE = 386;
  public const int ONE = 387;
  public const int ORD = 388;
  public const int UEQ = 389;
  public const int UNE = 390;
  public const int UNO = 391;
  public const int PHI = 392;
  public const int SELECT = 393;
  public const int CALL = 394;
  public const int TAIL = 395;
  public const int VA_ARG = 396;
  public const int LANDINGPAD = 397;
  public const int CATCHPAD = 398;
  public const int CLEANUPPAD = 399;
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
