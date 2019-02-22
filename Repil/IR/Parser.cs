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
//t    "type : X86_FP80",
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
    "TYPE","HALF","FLOAT","DOUBLE","X86_FP80","I1","I8","I16","I32","I64",
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
  { yyVal = FloatType.X86_FP80; }
  break;
case 76:
#line 291 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 77:
#line 292 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 78:
#line 293 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 79:
#line 294 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 80:
#line 295 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 81:
#line 299 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-2+yyTop], Enumerable.Empty<LType>());
    }
  break;
case 82:
#line 303 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 83:
#line 307 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 84:
#line 311 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 85:
#line 315 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 86:
#line 319 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 87:
#line 326 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 88:
#line 330 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 90:
#line 338 "Repil/IR/IR.jay"
  {
        yyVal = VarArgsType.VarArgs;
    }
  break;
case 91:
#line 345 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 92:
#line 349 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 93:
#line 353 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 94:
#line 357 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 95:
#line 361 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop], (SymbolTable<MetaSymbol>)yyVals[-3+yyTop]);
    }
  break;
case 96:
#line 365 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (IEnumerable<Parameter>)yyVals[-4+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 97:
#line 369 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 98:
#line 373 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-7+yyTop], (GlobalSymbol)yyVals[-6+yyTop], (IEnumerable<Parameter>)yyVals[-5+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 99:
#line 377 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-8+yyTop], (GlobalSymbol)yyVals[-7+yyTop], (IEnumerable<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 100:
#line 384 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 101:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-3+yyTop], (GlobalSymbol)yyVals[-2+yyTop], (IEnumerable<Parameter>)yyVals[-1+yyTop]);
    }
  break;
case 102:
#line 392 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-4+yyTop], (GlobalSymbol)yyVals[-3+yyTop], (IEnumerable<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 103:
#line 396 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 104:
#line 397 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Parameter> (); }
  break;
case 105:
#line 404 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 106:
#line 408 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 107:
#line 415 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 108:
#line 419 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 109:
#line 423 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 110:
#line 427 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, VarArgsType.VarArgs);
    }
  break;
case 112:
#line 435 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 113:
#line 439 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 114:
#line 440 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 115:
#line 441 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadOnly; }
  break;
case 116:
#line 442 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 117:
#line 443 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.ReadNone; }
  break;
case 123:
#line 461 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 124:
#line 462 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 125:
#line 463 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 126:
#line 464 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 127:
#line 465 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 128:
#line 466 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 129:
#line 467 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 130:
#line 468 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 131:
#line 469 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 132:
#line 470 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 133:
#line 474 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 134:
#line 475 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 135:
#line 476 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 136:
#line 477 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 137:
#line 478 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 138:
#line 479 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 139:
#line 480 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 140:
#line 481 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 141:
#line 482 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 142:
#line 483 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 143:
#line 484 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 144:
#line 485 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 145:
#line 486 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 146:
#line 487 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 147:
#line 488 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 148:
#line 489 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 150:
#line 494 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 151:
#line 495 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 152:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new IntToPointerValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 153:
#line 503 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerValue ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop], (List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 154:
#line 507 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 155:
#line 511 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointValue ((TypedValue)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 156:
#line 515 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 157:
#line 519 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 158:
#line 523 "Repil/IR/IR.jay"
  {
        yyVal = new StructureConstant ((List<TypedValue>)yyVals[-1+yyTop]);
    }
  break;
case 160:
#line 531 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 161:
#line 532 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 162:
#line 533 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 163:
#line 534 "Repil/IR/IR.jay"
  { yyVal = new HexIntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 164:
#line 535 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 165:
#line 536 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 166:
#line 537 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 167:
#line 538 "Repil/IR/IR.jay"
  { yyVal = ZeroConstant.Zero; }
  break;
case 168:
#line 539 "Repil/IR/IR.jay"
  { yyVal = new BytesConstant ((Symbol)yyVals[0+yyTop]); }
  break;
case 169:
#line 546 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 553 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 557 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue (VoidType.Void, VoidValue.Void);
    }
  break;
case 172:
#line 564 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 582 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 593 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 178:
#line 597 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 179:
#line 604 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 180:
#line 608 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 183:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 185:
#line 634 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 638 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 645 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 649 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 189:
#line 653 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 657 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 192:
#line 665 "Repil/IR/IR.jay"
  { yyVal = yyVals[-1+yyTop]; }
  break;
case 193:
#line 666 "Repil/IR/IR.jay"
  { yyVal = Enumerable.Empty<Argument> (); }
  break;
case 194:
#line 673 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 677 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 684 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 197:
#line 688 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 198:
#line 692 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 199:
#line 696 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 200:
#line 700 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 201:
#line 704 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 202:
#line 708 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-3+yyTop]), (ParameterAttributes)0);
    }
  break;
case 204:
#line 713 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 209:
#line 730 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 210:
#line 734 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 211:
#line 740 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 212:
#line 747 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 213:
#line 751 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 214:
#line 758 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 220:
#line 779 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 221:
#line 783 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 222:
#line 787 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 223:
#line 791 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 224:
#line 798 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 225:
#line 802 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 226:
#line 806 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 227:
#line 810 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 228:
#line 814 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 229:
#line 818 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 230:
#line 822 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], false);
    }
  break;
case 231:
#line 826 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 232:
#line 830 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (IEnumerable<Argument>)yyVals[-1+yyTop], true);
    }
  break;
case 233:
#line 834 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 234:
#line 838 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-2+yyTop], (Value)yyVals[-1+yyTop], (IEnumerable<Argument>)yyVals[0+yyTop], true);
    }
  break;
case 235:
#line 842 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 236:
#line 846 "Repil/IR/IR.jay"
  {
        yyVal = new FaddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 237:
#line 850 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 238:
#line 854 "Repil/IR/IR.jay"
  {
        yyVal = new FdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 239:
#line 858 "Repil/IR/IR.jay"
  {
        yyVal = new FmulInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 240:
#line 862 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 241:
#line 866 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 242:
#line 870 "Repil/IR/IR.jay"
  {
        yyVal = new FsubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 243:
#line 874 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 244:
#line 878 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 245:
#line 882 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 246:
#line 886 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 247:
#line 890 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 248:
#line 894 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 249:
#line 898 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 250:
#line 902 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 251:
#line 906 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 252:
#line 910 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 253:
#line 914 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 254:
#line 918 "Repil/IR/IR.jay"
  {
        yyVal = new PtrtointInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 255:
#line 922 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 256:
#line 926 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 257:
#line 930 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 258:
#line 934 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 259:
#line 938 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 260:
#line 942 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 261:
#line 946 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 262:
#line 950 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction (value: (TypedValue)yyVals[-5+yyTop], pointer: (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 263:
#line 954 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 264:
#line 958 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 265:
#line 962 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 266:
#line 966 "Repil/IR/IR.jay"
  {
        yyVal = new UdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 267:
#line 970 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 268:
#line 974 "Repil/IR/IR.jay"
  {
        yyVal = new UremInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 269:
#line 978 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 270:
#line 982 "Repil/IR/IR.jay"
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
   11,   11,   11,   11,   11,   11,   25,   25,   26,   26,
    4,    4,    4,    4,    4,    4,    4,    4,    4,    5,
    5,    5,   27,   27,   31,   31,   32,   32,   32,   32,
   33,   33,   34,   34,   34,   34,   34,   14,   14,   28,
   28,   35,   36,   36,   36,   36,   36,   36,   36,   36,
   36,   36,   37,   37,   37,   37,   37,   37,   37,   37,
   37,   37,   37,   37,   37,   37,   37,   37,   12,   12,
   12,   12,   12,   12,   12,   12,   12,   12,   40,   18,
   18,   18,   18,   18,   18,   18,   18,   18,   41,   21,
   21,   42,   39,   39,   43,   44,   38,   38,   29,   29,
   45,   45,   45,   45,   46,   46,   48,   48,   48,   48,
   50,   51,   51,   52,   52,   53,   53,   53,   53,   53,
   53,   53,   54,   54,   19,   19,   55,   55,   56,   56,
   57,   58,   58,   59,   60,   60,   61,   61,   30,   47,
   47,   47,   47,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
   49,   49,   49,   49,   49,   49,   49,   49,   49,   49,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    4,    1,    1,
    1,    6,    5,    6,    6,    7,    7,    9,   10,   10,
    7,   11,    9,   11,   10,    1,    1,    1,    1,    1,
    1,    3,    3,    3,    3,    3,    3,    6,    5,    2,
    3,    1,    2,    3,    3,    3,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    3,    1,
    1,    1,    4,    2,    3,    5,    1,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    3,    4,    2,    1,    5,    5,    1,    3,    1,    1,
    9,    9,   10,   10,   11,    9,   10,   11,   12,    5,
    6,    6,    3,    2,    1,    3,    1,    2,    1,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    2,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    6,    9,    6,    6,    3,    3,    3,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    2,    2,
    1,    2,    1,    3,    2,    1,    1,    3,    1,    2,
    2,    3,    1,    2,    1,    2,    1,    2,    3,    4,
    1,    3,    2,    1,    3,    2,    3,    3,    3,    2,
    4,    5,    1,    1,    1,    3,    1,    1,    1,    3,
    5,    1,    2,    3,    1,    2,    1,    1,    1,    2,
    7,    2,    7,    5,    6,    5,    5,    4,    4,    5,
    5,    6,    5,    6,    4,    5,    6,    5,    5,    4,
    4,    5,    6,    7,    6,    6,    7,    5,    6,    5,
    5,    6,    3,    4,    5,    7,    4,    5,    6,    6,
    4,    7,    5,    6,    4,    5,    4,    5,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    2,    9,   10,   11,    0,    0,    0,    0,    0,    0,
   70,   84,   72,   73,   74,   75,   76,   77,   78,   79,
   80,    0,   29,   28,    0,    0,    0,   71,    0,    0,
    0,    0,    0,    0,    3,    4,    0,    0,  118,  119,
   26,   27,   30,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   64,    0,    0,    0,    0,    0,    0,   83,
  219,    0,    0,    0,    0,    0,    0,    0,    5,    6,
    0,    0,    0,    0,    0,    8,    0,    7,    0,    0,
    0,    0,    0,   65,    0,    0,    0,    0,    0,    0,
    0,    0,   90,   81,    0,    0,   87,    0,    0,    0,
  162,  163,  161,  164,  165,  166,  160,  151,  150,  168,
  167,    0,    0,    0,    0,    0,    0,    0,    0,  149,
    0,    0,    0,    0,    0,    0,    0,   31,    0,    0,
    0,   49,   48,   13,    0,    0,   42,   47,    0,    0,
    0,    0,    0,    0,    0,  109,  110,  104,    0,    0,
  105,  122,    0,    0,  120,   82,    0,    0,    0,    0,
    0,    0,   62,   54,   52,   53,   55,   56,   57,   58,
    0,   50,    0,    0,    0,    0,  173,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   15,    0,    0,
    0,   43,   14,    0,  170,    0,   85,   66,   86,    0,
    0,  113,  114,  116,  115,  117,    0,  111,  103,    0,
    0,    0,    0,  121,   88,    0,    0,    0,    0,   12,
   51,    0,    0,    0,    0,  158,    0,  156,  157,    0,
    0,    0,    0,    0,    0,   35,    0,   33,   36,   37,
   32,   17,   16,   46,   45,   44,    0,    0,    0,    0,
  112,  106,    0,    0,   40,    0,    0,   59,  208,  207,
    0,  205,    0,    0,    0,    0,  174,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  179,    0,
    0,  185,    0,    0,    0,    0,    0,   41,    0,   63,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   23,
    0,   39,    0,    0,    0,    0,  222,    0,    0,  220,
    0,  217,  218,    0,    0,  215,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  123,  124,  125,  126,  127,  128,  129,  130,  131,
  132,    0,  133,  134,  145,  146,  147,  148,  136,  138,
  139,  140,  141,  137,  135,  143,  144,  142,    0,    0,
    0,    0,    0,    0,   96,  180,    0,  186,    0,    0,
    0,    0,    0,   91,    0,   92,  206,    0,  155,  152,
  154,    0,    0,    0,    0,   38,   94,    0,    0,  169,
    0,    0,    0,    0,  216,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  209,    0,  191,    0,    0,    0,
    0,    0,   97,    0,    0,   93,    0,    0,    0,   95,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  235,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   98,    0,  176,    0,  177,    0,    0,  224,    0,
  236,  263,    0,  242,  251,    0,  239,  266,  255,  238,
  268,  258,    0,    0,  248,  227,  250,  269,    0,    0,
  226,  159,  172,    0,    0,    0,    0,    0,    0,    0,
  210,    0,    0,  193,    0,    0,  194,    0,  230,    0,
    0,   99,  153,    0,    0,    0,    0,    0,  212,  225,
  264,  252,  259,  249,  246,  260,    0,    0,    0,    0,
  245,  237,    0,    0,    0,    0,  196,    0,  192,    0,
    0,  234,  178,  221,  175,    0,  223,  213,  247,  262,
    0,  211,  256,    0,  204,  198,  203,  199,  197,  195,
  214,  201,    0,  202,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   38,   12,   13,   14,  181,  145,  137,   54,
  146,  507,  223,   55,   56,   40,  138,  130,  271,  147,
  574,  182,   65,   66,  106,  107,  102,  164,  328,   73,
  160,  161,  217,  218,  165,  412,  429,  575,  188,  603,
  360,  549,  627,  576,  329,  330,  331,  332,  333,  508,
  568,  616,  617,  668,  272,  504,  505,  628,  629,  365,
  366,
  };
  protected static readonly short [] yySindex = {         1374,
   25,   30,   38,   82,   86, 2859, 2947, -256,    0, 1374,
    0,    0,    0,    0, -152,   92,   97,   37, -108,  -22,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3177,    0,    0, 3066, -109,  -77,    0,  142, 2830,
  -30, 3177,  -19,  127,    0,    0,  -33,  -21,    0,    0,
    0,    0,    0, 3177,   53,   22,  -91,  -51,  195,  -18,
  131,  -13,    0,  142,   11,  227,    6, 3177,   43,    0,
    0,  -12, 3177,  281, 2772,  -10,  281,  203,    0,    0,
 1448, 3177,   53, 3177,   53,    0,  207,    0, -163,  321,
  241, 2974,  281,    0, 3177, 3177,   24, 3177,  281,   -8,
 2717, -132,    0,    0,  142,  -25,    0,  281, -132,  290,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   15,  344,  346,  353, 3205, 3205, 3205,  360,    0,
 1448, 3177, 1448, 3177,  347,  349,  145,    0, -163, 3002,
    0,    0,    0,    0,  -40, 1448,    0,    0,   22,  142,
   59,  355,   18,   99,  281,    0,    0,    0,  -24,  178,
    0,    0,   99, -191,    0,    0, 2920,   99,   99,   99,
  348,  378,    0,    0,    0,    0,    0,    0,    0,    0,
  398,    0,  379, 3205, 3205, 3205,    0,   27,   47,   19,
   70,  376, 1448,  388, 1400, 4275,  162,    0, -163,  180,
    2,    0,    0, 3030,    0,   99,    0,    0,    0,  -99,
   22,    0,    0,    0,    0,    0,   95,    0,    0, 2886,
  -98,  163,  -83,    0,    0,   99,   99,  179, 1361,    0,
    0, 3177,   69,   71,   72,    0, 3205,    0,    0,  185,
   83,  401,   85,  108,  403,    0,  416,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  -84, 3750,  -79,   99,
    0,    0, 3750,  -78,    0,  196, 3750,    0,    0,    0,
  188,    0,   91, 3177, 3177, 3177,    0,  198,  206,  112,
  218,  219,  113,  459, 3750,  -69,  417, 3205, -135, 3205,
 1483, 3177, 1483, 3177, 1483, 3177, 3177, 3177, 3177, 3177,
 1483,  307, 3177, 3177, 3177, 3205, 3205, 3205, 3177, 3177,
 3205,   60, 3205, 3205, 3205, 3205, 3205, 3205, 3205, 3205,
 3205, 1527, 3114, 3177, 3177, 2830,   75, 1516,    0, 3750,
  198,    0,  198, 3750,  -81, 1606, 3750,    0, 1685,    0,
 1361, 3205,  327,  338,  340,  210,  198,  223,  198,    0,
  238,    0,  217, 1773, 3750, 4142,    0,  224, 1467,    0,
  454,    0,    0, 1448, 1483,    0, 1448, 1448, 1483, 1448,
 1448, 1483, 1448, 1448, 1448, 1448, 1448, 1448, 1483, 3177,
 1448, 1448, 1448, 1448,  457,  460,  462,  122,  155,  463,
 3177,  189,  134,  139,  140,  141,  144,  150,  151,  152,
  153,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 3177,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, 3177,   31,
 1448,  274, 3177, 2830,    0,    0,  198,    0,  210,  210,
 1861, 3750,  -62,    0, 1940,    0,    0,  467,    0,    0,
    0,  210,  198,  210,  198,    0,    0, 2028,  198,    0,
  470,  258,  481, 1448,    0,  482,  483, 1448,  494,  496,
 1448,  497,  498,  499,  500,  501,  505, 1448, 1448,  507,
  510,  512,  513, 3205, 3205, 3205,  197, 3177, 3177,  318,
 3205, 3177, 3177, 3177, 3177, 3177, 3177, 3177, 3177, 3177,
 1448, 1448, 1467,  515,    0,  516,    0,  525,  274,  274,
 3177,  210,    0, 2116, 3750,    0, 3205,  210,  210,    0,
  210,  258,  476, 1467,  524, 1467, 1467,  526, 1467, 1467,
  529, 1467, 1467, 1467, 1467, 1467, 1467,  531,  532, 1467,
 1467, 1467, 1467,    0,  533,  534,  312, 1448,  537,  539,
 3205,  540,  142,  142,  142,  142,  142,  142,  142,  142,
  142,  555,  556,  565,  491, 3205, 2800,   99,  525,  525,
  274,    0, 2195,    0,  222,    0,  568, 3177,    0, 1467,
    0,    0, 1467,    0,    0, 1467,    0,    0,    0,    0,
    0,    0, 1467, 1467,    0,    0,    0,    0, 3205, 3205,
    0,    0,    0,  254,  267,  585, 3205, 1467, 1467, 1467,
    0,  586, 3096,    0, 1323,  230,    0,   99,    0,   99,
  525,    0,    0, 3205,  258, 1536,  587, 3122,    0,    0,
    0,    0,    0,    0,    0,    0,  380,  383, 3205,  589,
    0,    0,  542, 3205,  596, 2686,    0,   50,    0, 3150,
   99,    0,    0,    0,    0,  258,    0,    0,    0,    0,
  589,    0,    0,  538,    0,    0,    0,    0,    0,    0,
    0,    0,  234,    0,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  641,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  613,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   35,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  606,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  130,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  606,    0,  606,    0,    0,    0,    0,    0,    0,    0,
  409,    0,    0,    0,    0,  606,    0,    0,    0,   56,
  606,    0,  606,    0,    0,    0,    0,    0,  138,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  103,
 2655, 2712,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  606,    0,  606,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  261,    0,    0,    0,
    0,    0,    0,    0,    0,  119,  370,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  784,    0,    0,    0,    0,  284,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  606,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 2283,    0, 3829,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  606,  606,  606,  863,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  606,    0,    0,  606,  606,    0,  606,
  606,    0,  606,  606,  606,  606,  606,  606,    0,    0,
  606,  606,  606,  606,    0,    0,    0,  606,  606,    0,
    0,  606,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  606,
  606,    0,    0,    0,    0,    0, 2371,    0, 2450, 3908,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  952, 1031, 1119,    0,    0,    0,    0, 3987,    0,
    0,    0,    0,  606,    0,    0,    0,  606,    0,    0,
  606,    0,    0,    0,    0,    0,    0,  606,  606,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  606,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  606,  606,    0, 3197,    0,    0,    0,    0,    0,    0,
    0, 2538,    0,    0,    0,    0,    0, 1277, 1342,    0,
 4066,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  606,    0,    0,
    0,    0,  488,  583,  662,  750,  829,  918,  997, 1085,
 1164,    0,    0,    0,    0,    0,    0, 3276,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  606,    0,    0, 3355,    0, 3434,
    0,    0,    0,    0,    0,  606,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 3513,
    0,    0,    0,    0,  309,  606,    0,    0,    0,    0,
 3592,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 3671,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  637,  594,    0,    0,    0,    0,  514,  517,  102,
   -6,   84, 1347,   -5,    0,  639,  456, -147, -281,    0,
  -70,  478,  592,    1,    0,  502,   46, -104, -250, -279,
    0,  441,   51, -215, -133,    0,    0, -509,  262,    0,
 -456,  176,    0,   44, -316,    0,  341,  342,  319, -403,
 -489,    0,   26,    0,  343,    0,  114,    0,   57, -155,
 -156,
  };
  protected static readonly short [] yyTable = {            39,
   39,  261,  353,  204,  170,  523,   41,   43,   87,   75,
   61,  436,  336,   68,   91,  166,  339,   70,  167,  436,
   75,  148,  436,  258,  263,   39,   75,   75,   64,   75,
  224,   75,   62,   39,  354,   39,  224,  436,  285,  267,
   72,  442,   76,  334,  337,  204,  433,   81,  249,  210,
   83,   85,   44,  355,   95,  187,  187,  187,  221,   70,
  515,   64,  237,  226,  227,  577,   39,   95,  105,  148,
  237,   35,   70,  100,   69,  131,  224,  133,   67,  619,
  620,  270,  222,  441,  203,   15,  445,  224,  150,  151,
  237,  153,  224,  224,  159,   69,  163,  640,   18,   68,
   70,  257,  100,  169,  458,  569,  570,   46,  238,  127,
  209,  239,  135,  233,  234,  235,  136,  162,  101,   36,
  207,  503,  109,  224,  436,  193,  253,  195,  436,  661,
  358,  652,   70,  256,  342,   94,  270,  369,  149,  372,
  128,  436,   19,  206,  154,  379,   20,   67,  152,  359,
   37,  236,   47,  168,  511,  335,   82,   48,   84,   67,
  105,   49,   50,   70,  129,  487,  277,  621,  654,   69,
   89,   58,  126,   89,  222,  222,  162,   69,  107,   69,
   68,  107,   35,   70,  132,  198,  134,   78,  199,  222,
  266,  514,  222,  270,  266,  266,   70,  436,  488,  671,
  211,  224,   49,   50,  266,  260,   51,   52,  465,  162,
  162,  266,  465,  159,  192,  465,  194,  357,  219,  361,
  252,  220,  465,  199,  162,  273,   79,  162,  340,  205,
   70,  341,  491,  202,   89,  385,  386,  387,   80,   86,
  390,   74,  393,  394,  395,  396,  397,  398,  399,  400,
  401,   59,   77,   92,   60,   90,  436,  456,   93,   99,
  341,  108,  623,  155,  573,  624,   75,  343,  344,  345,
  649,  448,   96,  650,  674,  202,  242,  341,  245,  212,
  213,  214,  215,  216,  364,  367,  368,  370,  371,  373,
  374,  375,  376,  377,  378,  381,  382,  383,  384,   16,
   17,  108,  388,  389,  108,  392,  111,  112,  113,   98,
  114,  115,  116,   75,  117,   49,   50,  430,  431,   39,
  101,  118,  119,   21,   34,  110,  432,   34,  120,   68,
   49,   50,   22,  127,   51,   52,   53,   33,   34,  121,
   23,   24,   25,   26,   27,   28,   29,   30,   31,  200,
   51,   52,  200,  212,  213,  214,  215,  216,  464,   70,
  139,  551,  468,  140,  128,  471,   36,  449,   70,  102,
  100,  100,  478,  479,  100,  100,  183,  100,  450,   70,
  451,   70,  673,  184,  490,  185,  101,  101,  189,  190,
  101,  101,  186,  101,  100,  100,  126,   37,  212,  213,
  214,  215,  216,  191,  196,  501,  197,  162,  228,  122,
  101,  101,  100,  544,  545,  546,  208,  229,  232,  241,
  552,  391,  502,  123,  124,  125,   39,   39,  101,   35,
  240,  243,  261,  509,  510,  250,  265,  274,  268,  275,
  276,  278,  461,  279,  280,  281,  283,  463,   70,  171,
  466,  467,  171,  469,  470,  284,  472,  473,  474,  475,
  476,  477,  347,  618,  480,  481,  482,  483,  282,  338,
  171,  222,  348,  351,  349,  350,  434,  356,  655,  453,
  606,  548,  548,  266,  224,  553,  554,  555,  556,  557,
  558,  559,  560,  561,  455,  612,  460,  462,  667,  352,
  484,  171,  492,  485,   39,  486,  489,  493,  494,  495,
  517,  571,  496,  522,  506,  651,  270,  224,  497,  498,
  499,  500,  230,  358,  524,  526,  527,   69,  635,  636,
  111,  112,  113,  171,  114,  115,  116,  529,  117,  530,
  532,  533,  534,  535,  536,  118,  119,  525,  537,  171,
  540,  528,  120,  541,  531,  542,  543,  547,  565,  566,
  615,  538,  539,  121,  567,  172,  578,  580,  601,  583,
   21,  626,  586,  663,  593,  594,  599,  600,  672,   22,
  604,  503,  605,  607,  562,  563,  564,   23,   24,   25,
   26,   27,   28,   29,   30,   31,  173,  174,  608,  609,
  175,  176,  177,  178,  179,  180,  646,  579,  610,  581,
  582,  625,  584,  585,  637,  587,  588,  589,  590,  591,
  592,  626,   69,  595,  596,  597,  598,  638,  639,  644,
  656,  602,  624,  122,  662,  664,  659,  102,  102,  660,
    1,  102,  102,  615,  102,   69,   45,  123,  124,  125,
  380,   88,   69,  201,  251,  200,   57,  171,  231,   97,
  262,  102,  102,  630,  550,  648,  631,  653,  225,  632,
  437,  438,   69,  172,  459,  670,  633,  634,  611,  102,
    0,  171,  171,  447,  658,    0,    0,    0,    0,    0,
    0,  641,  642,  643,    0,    0,    0,    0,  647,    0,
    0,   69,    0,   69,  173,  174,    0,    0,  175,  176,
  177,  178,  179,  180,    0,  111,  112,  113,    0,  114,
  115,  116,    0,  117,    0,  171,  171,  171,    0,    0,
    0,  669,    0,    0,  269,   69,  171,  120,    0,  171,
  171,  171,  171,  171,  171,  171,  171,  171,  121,    0,
  171,  171,    0,    0,  171,  171,  171,  171,  171,  171,
  265,  265,  171,  171,  171,    0,    0,    0,  171,    0,
    0,    0,  171,  171,  171,    0,    0,  171,  171,  171,
  171,  171,  171,   21,  171,    0,  171,    0,    0,   69,
    0,    0,    0,    0,  111,  112,  113,  171,  114,  115,
  116,    0,  117,    0,  265,  265,  265,    0,  171,  171,
  171,  171,    0,  269,    0,  265,  120,    0,  265,  265,
  265,  265,  265,  265,  265,  265,  265,  121,    0,  265,
  265,    0,    0,  265,  265,  265,  265,  265,  265,    0,
    0,  265,  265,  265,    0,    0,    0,  265,    0,    0,
    0,  265,  265,  265,    0,  270,  270,  265,  265,  265,
  265,  265,   18,  265,    0,  265,    0,    0,   69,   69,
   69,   69,    0,   69,   69,   69,  265,   69,    0,    0,
    0,    0,    0,    0,   69,   69,    0,  265,  265,  265,
  265,   69,    0,    0,    0,    0,    0,    0,    0,  270,
  270,  270,   69,    0,    0,    0,    0,    0,    0,    0,
  270,    0,    0,  270,  270,  270,  270,  270,  270,  270,
  270,  270,    0,    0,  270,  270,    0,    0,  270,  270,
  270,  270,  270,  270,  257,  257,  270,  270,  270,    0,
    0,    0,  270,    0,    0,    0,  270,  270,  270,    0,
    0,   19,  270,  270,  270,  270,  270,   69,  270,    0,
  270,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  270,   69,    0,    0,    0,    0,    0,  257,  257,
  257,    0,  270,  270,  270,  270,   69,   69,   69,  257,
    0,    0,  257,  257,  257,  257,  257,  257,  257,  257,
  257,    0,    0,  257,  257,    0,    0,  257,  257,  257,
  257,  257,  257,    0,    0,  257,  257,  257,    0,    0,
    0,  257,  240,  240,    0,  257,  257,  257,    0,    0,
   20,  257,  257,  257,  257,  257,   69,  257,    0,  257,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  257,   21,   21,    0,    0,   21,   21,    0,   21,    0,
    0,  257,  257,  257,  257,    0,  240,  240,  240,    0,
    0,    0,    0,    0,    0,   21,   21,  240,    0,    0,
  240,  240,  240,  240,  240,  240,  240,  240,  240,    0,
    0,  240,  240,   21,    0,  240,  240,  240,  240,  240,
  240,  241,  241,  240,  240,  240,    0,    0,    0,  240,
    0,    0,    0,  240,  240,  240,    0,    0,   25,  240,
  240,  240,  240,  240,   69,  240,    0,  240,    0,    0,
   18,   18,    0,    0,   18,   18,    0,   18,  240,    0,
    0,    0,    0,    0,    0,  241,  241,  241,    0,  240,
  240,  240,  240,    0,   18,   18,  241,    0,    0,  241,
  241,  241,  241,  241,  241,  241,  241,  241,    0,    0,
  241,  241,   18,    0,  241,  241,  241,  241,  241,  241,
    0,    0,  241,  241,  241,    0,    0,    0,  241,    0,
  267,  267,  241,  241,  241,    0,    0,    0,  241,  241,
  241,  241,  241,   69,  241,    0,  241,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  241,    0,   19,
   19,    0,    0,   19,   19,    0,   19,    0,  241,  241,
  241,  241,    0,    0,  267,  267,  267,    0,    0,    0,
    0,    0,    0,   19,   19,  267,    0,    0,  267,  267,
  267,  267,  267,  267,  267,  267,  267,    0,    0,  267,
  267,   19,    0,  267,  267,  267,  267,  267,  267,  261,
  261,  267,  267,  267,    0,    0,   22,  267,    0,    0,
    0,  267,  267,  267,    0,    0,    0,  267,  267,  267,
  267,  267,    0,  267,    0,  267,    0,    0,   20,   20,
    0,    0,   20,   20,    0,   20,  267,    0,    0,    0,
    0,    0,    0,  261,  261,  261,    0,  267,  267,  267,
  267,    0,   20,   20,  261,    0,    0,  261,  261,  261,
  261,  261,  261,  261,  261,  261,    0,    0,  261,  261,
   20,   24,  261,  261,  261,  261,  261,  261,    0,    0,
  261,  261,  261,    0,    0,    0,  261,  254,  254,    0,
  261,  261,  261,    0,   70,    0,  261,  261,  261,  261,
  261,    0,  261,    0,  261,    0,    0,    0,    0,    0,
    0,    0,  127,    0,    0,  261,   25,   25,    0,    0,
   25,   25,    0,   25,    0,    0,  261,  261,  261,  261,
    0,  254,  254,  254,    0,    0,    0,    0,    0,    0,
   25,   25,  254,  128,    0,  254,  254,  254,  254,  254,
  254,  254,  254,  254,    0,    0,  254,  254,   25,    0,
  254,  254,  254,  254,  254,  254,  228,  228,  254,  254,
  254,   70,    0,  244,  254,  126,    0,    0,  254,  254,
  254,    0,    0,    0,  254,  254,  254,  254,  254,  127,
  254,    0,  254,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  254,    0,    0,    0,    0,    0,    0,
  228,  228,  228,    0,  254,  254,  254,  254,    0,   70,
  128,  228,    0,    0,  228,  228,  228,  228,  228,  228,
  228,  228,  228,    0,    0,  228,  228,  127,    0,  228,
  228,  228,  228,  228,  228,    0,    0,  228,  228,  228,
    0,    0,  126,  228,    0,    0,  127,  228,  228,  228,
    0,    0,    0,  228,  228,  228,  228,  228,  128,  228,
    0,  228,   36,    0,   22,   22,    0,    0,   22,   22,
    0,   22,  228,    0,    0,    0,  259,  128,    0,    0,
    0,    0,    0,  228,  228,  228,  228,  264,   22,   22,
  126,    0,    0,   37,    0,    0,    0,   70,    0,  111,
  112,  113,    0,  114,  115,  116,   22,  117,    0,  126,
    0,    0,    0,    0,  118,  119,    0,    0,    0,    0,
    0,  120,    0,  286,    0,   35,    0,    0,    0,   24,
   24,    0,  121,   24,   24,    0,   24,  111,  112,  113,
    0,  114,  115,  116,  346,  117,  212,  213,  214,  215,
  216,    0,    0,   24,   24,    0,  269,    0,    0,  120,
  435,    1,    2,    0,    0,    3,    4,    0,    5,    0,
  121,   24,    0,    0,    0,    0,  111,  112,  113,    0,
  114,  115,  116,    0,  117,    6,    7,    0,    0,    0,
    0,  118,  119,    0,    0,    0,    0,  439,  120,  440,
    0,  443,  122,    8,    0,    0,    0,    0,    0,  121,
    0,    0,    0,  452,    0,  454,  123,  124,  125,    0,
    0,    0,    0,    0,  111,  112,  113,    0,  114,  115,
  116,    0,  117,    0,    0,    0,    0,    0,    0,  118,
  119,    0,    0,  111,  112,  113,  120,  114,  115,  116,
  444,  117,    0,    0,    0,    0,    0,  121,  118,  119,
    0,    0,    0,    0,    0,  120,   21,    0,    0,    0,
    0,    0,    0,    0,    0,   22,  121,    0,    0,  122,
    0,    0,    0,   23,   24,   25,   26,   27,   28,   29,
   30,   31,    0,  123,  124,  125,    0,    0,    0,    0,
    0,    0,    0,  512,    0,    0,    0,    0,  287,    0,
    0,    0,  111,  112,  113,    0,  114,  115,  116,  518,
  117,  519,    0,    0,    0,  521,    0,  122,    0,  446,
    0,  362,  363,    0,  120,    0,    0,    0,    0,    0,
    0,  123,  124,  125,    0,  121,  122,    0,    0,    0,
    0,    0,  288,  289,  290,    0,    0,    0,    0,    0,
  123,  124,  125,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  298,  299,  300,    0,    0,  301,  302,    0,
    0,  303,  304,  305,  306,  307,  308,    0,    0,  309,
  310,  311,    0,    0,    0,  312,    0,    0,  287,  313,
  314,  315,    0,    0,    0,  316,  317,  318,  319,  320,
    0,  321,    0,  322,    0,    0,    0,  457,    0,    0,
    0,    0,    0,    0,  323,  402,  403,  404,  405,  406,
  407,  408,  409,  410,  411,  324,  325,  326,  327,    0,
    0,    0,  288,  289,  290,    0,    0,    0,    0,    0,
    0,    0,    0,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  298,  299,  300,    0,    0,  301,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  287,    0,  309,
  310,  311,    0,    0,    0,  312,    0,    0,    0,  313,
  314,  315,    0,    0,    0,  316,  317,  318,  319,  320,
    0,  321,    0,  322,    0,  513,    0,    0,    0,    0,
    0,    0,    0,    0,  323,    0,    0,    0,    0,    0,
    0,  288,  289,  290,    0,  324,  325,  326,  327,    0,
    0,    0,  291,    0,    0,  292,  293,  294,  295,  296,
  297,  298,  299,  300,    0,    0,  301,  302,    0,    0,
  303,  304,  305,  306,  307,  308,    0,    0,  309,  310,
  311,    0,    0,    0,  312,  287,    0,    0,  313,  314,
  315,    0,    0,    0,  316,  317,  318,  319,  320,    0,
  321,    0,  322,    0,  516,    0,    0,    0,    0,    0,
    0,    0,    0,  323,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  324,  325,  326,  327,    0,  288,
  289,  290,    0,    0,    0,    0,    0,    0,    0,    0,
  291,    0,    0,  292,  293,  294,  295,  296,  297,  298,
  299,  300,    0,    0,  301,  302,    0,    0,  303,  304,
  305,  306,  307,  308,    0,    0,  309,  310,  311,    0,
    0,    0,  312,  287,    0,    0,  313,  314,  315,    0,
    0,    0,  316,  317,  318,  319,  320,    0,  321,    0,
  322,    0,  520,    0,    0,    0,    0,    0,    0,    0,
    0,  323,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  324,  325,  326,  327,    0,  288,  289,  290,
    0,    0,    0,    0,    0,    0,    0,    0,  291,    0,
    0,  292,  293,  294,  295,  296,  297,  298,  299,  300,
    0,    0,  301,  302,    0,    0,  303,  304,  305,  306,
  307,  308,  287,    0,  309,  310,  311,    0,    0,    0,
  312,    0,    0,    0,  313,  314,  315,    0,    0,    0,
  316,  317,  318,  319,  320,    0,  321,    0,  322,    0,
  572,    0,    0,    0,    0,    0,    0,    0,    0,  323,
    0,    0,    0,    0,    0,    0,  288,  289,  290,    0,
  324,  325,  326,  327,    0,    0,    0,  291,    0,    0,
  292,  293,  294,  295,  296,  297,  298,  299,  300,    0,
    0,  301,  302,    0,    0,  303,  304,  305,  306,  307,
  308,    0,    0,  309,  310,  311,    0,    0,    0,  312,
  287,    0,    0,  313,  314,  315,    0,    0,    0,  316,
  317,  318,  319,  320,    0,  321,    0,  322,    0,  622,
    0,    0,    0,    0,    0,    0,    0,    0,  323,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  324,
  325,  326,  327,    0,  288,  289,  290,    0,    0,    0,
    0,    0,    0,    0,    0,  291,    0,    0,  292,  293,
  294,  295,  296,  297,  298,  299,  300,    0,    0,  301,
  302,    0,    0,  303,  304,  305,  306,  307,  308,    0,
    0,  309,  310,  311,    0,    0,    0,  312,  287,    0,
    0,  313,  314,  315,    0,    0,    0,  316,  317,  318,
  319,  320,    0,  321,    0,  322,    0,  183,    0,    0,
    0,    0,    0,    0,    0,    0,  323,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  324,  325,  326,
  327,    0,  288,  289,  290,    0,    0,    0,    0,    0,
    0,    0,    0,  291,    0,    0,  292,  293,  294,  295,
  296,  297,  298,  299,  300,    0,    0,  301,  302,    0,
    0,  303,  304,  305,  306,  307,  308,  287,    0,  309,
  310,  311,    0,    0,    0,  312,    0,    0,    0,  313,
  314,  315,    0,    0,    0,  316,  317,  318,  319,  320,
    0,  321,    0,  322,    0,  181,    0,    0,    0,    0,
    0,    0,    0,    0,  323,    0,    0,    0,    0,    0,
    0,  288,  289,  290,    0,  324,  325,  326,  327,    0,
    0,    0,  291,    0,    0,  292,  293,  294,  295,  296,
  297,  298,  299,  300,    0,    0,  301,  302,    0,    0,
  303,  304,  305,  306,  307,  308,    0,    0,  309,  310,
  311,    0,    0,    0,  312,  183,    0,    0,  313,  314,
  315,    0,    0,    0,  316,  317,  318,  319,  320,    0,
  321,    0,  322,    0,  184,    0,    0,    0,    0,    0,
    0,    0,    0,  323,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  324,  325,  326,  327,    0,  183,
  183,  183,    0,    0,    0,    0,    0,    0,    0,    0,
  183,    0,    0,  183,  183,  183,  183,  183,  183,  183,
  183,  183,    0,    0,  183,  183,    0,    0,  183,  183,
  183,  183,  183,  183,    0,    0,  183,  183,  183,    0,
    0,    0,  183,  181,    0,    0,  183,  183,  183,    0,
    0,    0,  183,  183,  183,  183,  183,    0,  183,    0,
  183,    0,  182,    0,    0,    0,    0,    0,    0,    0,
    0,  183,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  183,  183,  183,  183,    0,  181,  181,  181,
    0,    0,    0,    0,    0,    0,    0,    0,  181,    0,
    0,  181,  181,  181,  181,  181,  181,  181,  181,  181,
    0,    0,  181,  181,    0,    0,  181,  181,  181,  181,
  181,  181,  184,    0,  181,  181,  181,   70,    0,    0,
  181,    0,    0,    0,  181,  181,  181,    0,    0,    0,
  181,  181,  181,  181,  181,    0,  181,    0,  181,    0,
    0,    0,    0,    0,    0,    0,    0,  158,    0,  181,
    0,    0,    0,    0,    0,    0,  184,  184,  184,    0,
  181,  181,  181,  181,    0,    0,   36,  184,    0,   60,
  184,  184,  184,  184,  184,  184,  184,  184,  184,    0,
    0,  184,  184,    0,    0,  184,  184,  184,  184,  184,
  184,    0,    0,  184,  184,  184,    0,   37,    0,  184,
  182,    0,  104,  184,  184,  184,    0,    0,    0,  184,
  184,  184,  184,  184,    0,  184,    0,  184,    0,    0,
    0,   36,    0,    0,    0,    0,   61,    0,  184,   35,
  614,    0,    0,    0,    0,    0,    0,    0,    0,  184,
  184,  184,  184,    0,  182,  182,  182,    0,    0,   36,
    0,    0,   37,    0,    0,  182,    0,    0,  182,  182,
  182,  182,  182,  182,  182,  182,  182,    0,    0,  182,
  182,    0,    0,  182,  182,  182,  182,  182,  182,   36,
   37,  182,  182,  182,   35,    0,    0,  182,    0,    0,
    0,  182,  182,  182,    0,    0,    0,  182,  182,  182,
  182,  182,    0,  182,   60,  182,    0,    0,   36,    0,
   37,    0,   35,    0,    0,    0,  182,    0,    0,    0,
   60,    0,    0,    0,    0,    0,    0,  182,  182,  182,
  182,    0,  111,  112,  113,   36,  114,  115,  116,   37,
  117,    0,   35,    0,    0,    0,    0,  665,  666,    0,
    0,   60,   60,    0,  120,   60,   60,   60,   60,   60,
   60,   61,    0,    0,    0,  121,   37,    0,    0,   36,
   21,   35,    0,    0,    0,    0,    0,   61,    0,   22,
    0,    0,    0,    0,  156,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   31,   36,    0,   35,    0,
   37,    0,    0,  157,    0,    0,    0,    0,   61,   61,
    0,    0,   61,   61,   61,   61,   61,   61,    0,    0,
    0,    0,    0,   36,    0,   21,    0,   37,    0,    0,
    0,    0,   35,    0,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   31,   36,    0,   21,   37,    0,    0,    0,  103,   35,
    0,    0,   22,    0,    0,    0,    0,  613,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   31,   36,
    0,    0,   37,   21,    0,    0,   35,    0,  144,    0,
    0,    0,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   31,    0,
   37,    0,   21,    0,   35,   36,    0,    0,    0,    0,
    0,   22,   71,    0,    0,    0,    0,    0,    0,   23,
   24,   25,   26,   27,   28,   29,   30,   31,    0,   21,
    0,    0,   35,    0,   32,   36,   37,    0,   22,   33,
   34,    0,    0,  156,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,   31,    0,    0,    0,    0,    0,
    0,   36,  157,   21,    0,    0,   37,    0,   35,    0,
   63,    0,   22,    0,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   31,   36,
   21,    0,   37,    0,  657,    0,  103,    0,   35,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   31,   36,  141,  142,    0,
   37,    0,   42,    0,   35,    0,   22,  143,    0,    0,
    0,    0,    0,    0,   23,   24,   25,   26,   27,   28,
   29,   30,   31,    0,   36,  141,  142,   37,    0,    0,
    0,    0,   35,    0,   22,  143,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   31,    0,    0,  141,  254,   37,    0,    0,    0,   35,
    0,    0,   22,  255,    0,    0,    0,    0,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   31,    0,
    0,    0,    0,    0,    0,    0,    0,   35,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,    0,
    0,    0,    0,    0,    0,    0,   23,   24,   25,   26,
   27,   28,   29,   30,   31,    0,    0,    0,    0,   21,
    0,    0,    0,    0,    0,    0,    0,    0,   22,  645,
    0,    0,    0,    0,  413,  414,   23,   24,   25,   26,
   27,   28,   29,   30,   31,   21,    0,    0,    0,    0,
    0,    0,    0,    0,   22,    0,    0,    0,    0,    0,
    0,    0,   23,   24,   25,   26,   27,   28,   29,   30,
   31,    0,    0,   21,    0,    0,    0,    0,    0,    0,
    0,    0,   22,    0,    0,    0,    0,  613,    0,    0,
   23,   24,   25,   26,   27,   28,   29,   30,   31,    0,
   21,    0,    0,    0,    0,    0,    0,    0,    0,   22,
    0,    0,    0,    0,    0,    0,    0,   23,   24,   25,
   26,   27,   28,   29,   30,   31,    0,    0,  141,  253,
  253,    0,    0,    0,    0,    0,    0,   22,    0,    0,
    0,    0,    0,    0,    0,   23,   24,   25,   26,   27,
   28,   29,   30,   31,  415,  416,  417,  418,    0,    0,
    0,    0,    0,  419,  420,  421,  422,  423,  424,  425,
  426,  427,  428,  253,  253,  253,    0,    0,    0,    0,
    0,    0,    0,    0,  253,    0,    0,  253,  253,  253,
  253,  253,  253,  253,  253,  253,    0,    0,  253,  253,
    0,    0,  253,  253,  253,  253,  253,  253,  229,  229,
  253,  253,  253,    0,    0,    0,  253,    0,    0,    0,
  253,  253,  253,    0,    0,    0,  253,  253,  253,  253,
  253,    0,  253,    0,  253,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  253,    0,    0,    0,    0,
    0,    0,  229,  229,  229,    0,  253,  253,  253,  253,
    0,    0,    0,  229,    0,    0,  229,  229,  229,  229,
  229,  229,  229,  229,  229,    0,    0,  229,  229,    0,
    0,  229,  229,  229,  229,  229,  229,  231,  231,  229,
  229,  229,    0,    0,    0,  229,    0,    0,    0,  229,
  229,  229,    0,    0,    0,  229,  229,  229,  229,  229,
    0,  229,    0,  229,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  229,    0,    0,    0,    0,    0,
    0,  231,  231,  231,    0,  229,  229,  229,  229,    0,
    0,    0,  231,    0,    0,  231,  231,  231,  231,  231,
  231,  231,  231,  231,    0,    0,  231,  231,    0,    0,
  231,  231,  231,  231,  231,  231,  233,  233,  231,  231,
  231,    0,    0,    0,  231,    0,    0,    0,  231,  231,
  231,    0,    0,    0,  231,  231,  231,  231,  231,    0,
  231,    0,  231,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  231,    0,    0,    0,    0,    0,    0,
  233,  233,  233,    0,  231,  231,  231,  231,    0,    0,
    0,  233,    0,    0,  233,  233,  233,  233,  233,  233,
  233,  233,  233,    0,    0,  233,  233,    0,    0,  233,
  233,  233,  233,  233,  233,  243,  243,  233,  233,  233,
    0,    0,    0,  233,    0,    0,    0,  233,  233,  233,
    0,    0,    0,  233,  233,  233,  233,  233,    0,  233,
    0,  233,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  233,    0,    0,    0,    0,    0,    0,  243,
  243,  243,    0,  233,  233,  233,  233,    0,    0,    0,
  243,    0,    0,  243,  243,  243,  243,  243,  243,  243,
  243,  243,    0,    0,  243,  243,    0,    0,  243,  243,
  243,  243,  243,  243,  232,  232,  243,  243,  243,    0,
    0,    0,  243,    0,    0,    0,  243,  243,  243,    0,
    0,    0,  243,  243,  243,  243,  243,    0,  243,    0,
  243,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  243,    0,    0,    0,    0,    0,    0,  232,  232,
  232,    0,  243,  243,  243,  243,    0,    0,    0,  232,
    0,    0,  232,  232,  232,  232,  232,  232,  232,  232,
  232,    0,    0,  232,  232,    0,    0,  232,  232,  232,
  232,  232,  232,  244,  244,  232,  232,  232,    0,    0,
    0,  232,    0,    0,    0,  232,  232,  232,    0,    0,
    0,  232,  232,  232,  232,  232,    0,  232,    0,  232,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  232,    0,    0,    0,    0,    0,    0,  244,  244,  244,
    0,  232,  232,  232,  232,    0,    0,    0,  244,    0,
    0,  244,  244,  244,  244,  244,  244,  244,  244,  244,
    0,    0,  244,  244,    0,    0,  244,  244,  244,  244,
  244,  244,  287,    0,  244,  244,  244,    0,    0,    0,
  244,    0,    0,    0,  244,  244,  244,    0,    0,    0,
  244,  244,  244,  244,  244,    0,  244,    0,  244,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  244,
    0,    0,    0,    0,    0,    0,  288,  289,  290,    0,
  244,  244,  244,  244,    0,    0,    0,  291,    0,    0,
  292,  293,  294,  295,  296,  297,  298,  299,  300,    0,
    0,  301,  302,    0,    0,  303,  304,  305,  306,  307,
  308,  187,    0,  309,  310,  311,    0,    0,    0,  312,
    0,    0,    0,  313,  314,  315,    0,    0,    0,  316,
  317,  318,  319,  320,    0,  321,    0,  322,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  323,    0,
    0,    0,    0,    0,    0,  187,  187,  187,    0,  324,
  325,  326,  327,    0,    0,    0,  187,    0,    0,  187,
  187,  187,  187,  187,  187,  187,  187,  187,    0,    0,
  187,  187,    0,    0,  187,  187,  187,  187,  187,  187,
  188,    0,  187,  187,  187,    0,    0,    0,  187,    0,
    0,    0,  187,  187,  187,    0,    0,    0,  187,  187,
  187,  187,  187,    0,  187,    0,  187,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  187,    0,    0,
    0,    0,    0,    0,  188,  188,  188,    0,  187,  187,
  187,  187,    0,    0,    0,  188,    0,    0,  188,  188,
  188,  188,  188,  188,  188,  188,  188,    0,    0,  188,
  188,    0,    0,  188,  188,  188,  188,  188,  188,  189,
    0,  188,  188,  188,    0,    0,    0,  188,    0,    0,
    0,  188,  188,  188,    0,    0,    0,  188,  188,  188,
  188,  188,    0,  188,    0,  188,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  188,    0,    0,    0,
    0,    0,    0,  189,  189,  189,    0,  188,  188,  188,
  188,    0,    0,    0,  189,    0,    0,  189,  189,  189,
  189,  189,  189,  189,  189,  189,    0,    0,  189,  189,
    0,    0,  189,  189,  189,  189,  189,  189,  190,    0,
  189,  189,  189,    0,    0,    0,  189,    0,    0,    0,
  189,  189,  189,    0,    0,    0,  189,  189,  189,  189,
  189,    0,  189,    0,  189,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  189,    0,    0,    0,    0,
    0,    0,  190,  190,  190,    0,  189,  189,  189,  189,
    0,    0,    0,  190,    0,    0,  190,  190,  190,  190,
  190,  190,  190,  190,  190,    0,    0,  190,  190,    0,
    0,  190,  190,  190,  190,  190,  190,    0,    0,  190,
  190,  190,    0,    0,    0,  190,    0,    0,    0,  190,
  190,  190,    0,    0,    0,  190,  190,  190,  190,  190,
    0,  190,    0,  190,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  190,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  190,  190,  190,  190,  291,
    0,    0,  292,  293,  294,  295,  296,  297,  298,  299,
  300,    0,    0,  301,  302,    0,    0,  303,  304,  305,
  306,  307,  308,    0,    0,  309,  310,  311,    0,    0,
    0,  312,    0,    0,    0,  313,  314,  315,    0,    0,
    0,  316,  317,  318,  319,  320,    0,  321,    0,  322,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  323,  111,  112,  113,  246,  114,  115,  116,    0,  117,
    0,  324,  325,  326,  327,    0,    0,    0,  247,    0,
  248,    0,    0,  120,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  121,
  };
  protected static readonly short [] yyCheck = {             6,
    7,  217,  284,   44,  109,  462,    6,    7,   60,   40,
   33,  328,  263,  123,   33,   41,  267,   42,   44,  336,
   40,   92,  339,  123,  123,   32,   40,   40,   35,   40,
  164,   40,   32,   40,  285,   42,  170,  354,  123,  123,
   40,  123,   42,  123,  123,   44,  326,   54,  196,  154,
   56,   57,  309,  123,   44,  126,  127,  128,  163,   42,
  123,   68,   44,  168,  169,  522,   73,   44,   75,  140,
   44,  123,   42,   73,   40,   82,  210,   84,   44,  569,
  570,  229,  274,  334,  125,   61,  337,  221,   95,   96,
   44,   98,  226,  227,  101,   40,  102,  607,   61,   44,
   42,  206,    0,  109,  355,  509,  510,  260,   62,   60,
   93,   93,  276,  184,  185,  186,  280,  309,    0,   60,
   62,   91,   77,  257,  441,  132,  125,  134,  445,  639,
  266,  621,   42,  204,   44,  125,  284,  293,   93,  295,
   91,  458,   61,  149,   99,  301,   61,  257,  125,  285,
   91,  125,   61,  108,  434,  260,   55,   61,   57,  125,
  167,  294,  295,   42,   81,   44,  237,  571,  625,   40,
   41,  280,  123,   44,  274,  274,  309,   40,   41,  257,
  125,   44,  123,   42,   83,   41,   85,   61,   44,  274,
  274,  442,  274,  341,  274,  274,   42,  514,   44,  656,
  155,  335,  294,  295,  274,  211,  298,  299,  365,  309,
  309,  274,  369,  220,  131,  372,  133,  288,   41,  290,
   41,   44,  379,   44,  309,  232,  260,  309,   41,  146,
   42,   44,   44,  274,   40,  306,  307,  308,  260,  291,
  311,  272,  313,  314,  315,  316,  317,  318,  319,  320,
  321,  274,  272,  123,  277,  274,  573,   41,  272,  272,
   44,  272,   41,  272,  515,   44,   40,  274,  275,  276,
   41,  342,  267,   44,   41,  274,  193,   44,  195,  304,
  305,  306,  307,  308,  291,  292,  293,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  270,
  271,   41,  309,  310,   44,  312,  257,  258,  259,  267,
  261,  262,  263,   40,  265,  294,  295,  324,  325,  326,
   40,  272,  273,  264,   41,  123,  326,   44,  279,  123,
  294,  295,  273,   60,  298,  299,  300,  301,  302,  290,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   41,
  298,  299,   44,  304,  305,  306,  307,  308,  365,   42,
   40,   44,  369,  123,   91,  372,   60,   41,   42,    0,
  268,  269,  379,  380,  272,  273,  362,  275,   41,   42,
   41,   42,  664,   40,  391,   40,  268,  269,  127,  128,
  272,  273,   40,  275,  292,  293,  123,   91,  304,  305,
  306,  307,  308,   44,   58,  412,   58,  309,   61,  360,
  292,  293,  310,  484,  485,  486,   62,   40,   40,   44,
  491,  362,  429,  374,  375,  376,  433,  434,  310,  123,
  361,   44,  648,  433,  434,  274,  274,  369,  260,  369,
  369,  257,  359,  361,   44,  361,   44,  364,   40,   41,
  367,  368,   44,  370,  371,   40,  373,  374,  375,  376,
  377,  378,  257,  568,  381,  382,  383,  384,  361,  274,
   62,  274,  361,  361,  257,  257,  402,   61,  626,  257,
  551,  488,  489,  274,  618,  492,  493,  494,  495,  496,
  497,  498,  499,  500,  257,  566,  273,   44,  646,   41,
   44,   93,  369,   44,  511,   44,   44,  369,  369,  369,
   44,  511,  369,   44,  431,  620,  664,  651,  369,  369,
  369,  369,  125,  266,   44,   44,   44,   40,  599,  600,
  257,  258,  259,  125,  261,  262,  263,   44,  265,   44,
   44,   44,   44,   44,   44,  272,  273,  464,   44,  260,
   44,  468,  279,   44,  471,   44,   44,  361,   44,   44,
  567,  478,  479,  290,   40,  276,   91,   44,  257,   44,
  264,  578,   44,  644,   44,   44,   44,   44,   41,  273,
   44,   91,   44,   44,  501,  502,  503,  281,  282,  283,
  284,  285,  286,  287,  288,  289,  307,  308,   44,   44,
  311,  312,  313,  314,  315,  316,  613,  524,   44,  526,
  527,   44,  529,  530,  361,  532,  533,  534,  535,  536,
  537,  628,   40,  540,  541,  542,  543,  361,   44,   44,
   44,  548,   44,  360,   93,   40,  257,  268,  269,  257,
    0,  272,  273,  650,  275,   40,   10,  374,  375,  376,
  344,   58,   40,  140,  199,  139,   18,  260,  181,   68,
  220,  292,  293,  580,  489,  615,  583,  624,  167,  586,
  330,  330,   60,  276,  356,  650,  593,  594,  565,  310,
   -1,  273,  274,  341,  628,   -1,   -1,   -1,   -1,   -1,
   -1,  608,  609,  610,   -1,   -1,   -1,   -1,  615,   -1,
   -1,   40,   -1,   91,  307,  308,   -1,   -1,  311,  312,
  313,  314,  315,  316,   -1,  257,  258,  259,   -1,  261,
  262,  263,   -1,  265,   -1,  317,  318,  319,   -1,   -1,
   -1,  648,   -1,   -1,  276,  123,  328,  279,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,  290,   -1,
  342,  343,   -1,   -1,  346,  347,  348,  349,  350,  351,
  273,  274,  354,  355,  356,   -1,   -1,   -1,  360,   -1,
   -1,   -1,  364,  365,  366,   -1,   -1,  369,  370,  371,
  372,  373,  374,    0,  376,   -1,  378,   -1,   -1,   40,
   -1,   -1,   -1,   -1,  257,  258,  259,  389,  261,  262,
  263,   -1,  265,   -1,  317,  318,  319,   -1,  400,  401,
  402,  403,   -1,  276,   -1,  328,  279,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,  290,   -1,  342,
  343,   -1,   -1,  346,  347,  348,  349,  350,  351,   -1,
   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,
   -1,  364,  365,  366,   -1,  273,  274,  370,  371,  372,
  373,  374,    0,  376,   -1,  378,   -1,   -1,   40,  257,
  258,  259,   -1,  261,  262,  263,  389,  265,   -1,   -1,
   -1,   -1,   -1,   -1,  272,  273,   -1,  400,  401,  402,
  403,  279,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  317,
  318,  319,  290,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,
  348,  349,  350,  351,  273,  274,  354,  355,  356,   -1,
   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,   -1,
   -1,    0,  370,  371,  372,  373,  374,   40,  376,   -1,
  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,  360,   -1,   -1,   -1,   -1,   -1,  317,  318,
  319,   -1,  400,  401,  402,  403,  374,  375,  376,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,
  349,  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,
   -1,  360,  273,  274,   -1,  364,  365,  366,   -1,   -1,
    0,  370,  371,  372,  373,  374,   40,  376,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
   -1,  400,  401,  402,  403,   -1,  317,  318,  319,   -1,
   -1,   -1,   -1,   -1,   -1,  292,  293,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,   -1,
   -1,  342,  343,  310,   -1,  346,  347,  348,  349,  350,
  351,  273,  274,  354,  355,  356,   -1,   -1,   -1,  360,
   -1,   -1,   -1,  364,  365,  366,   -1,   -1,    0,  370,
  371,  372,  373,  374,   40,  376,   -1,  378,   -1,   -1,
  268,  269,   -1,   -1,  272,  273,   -1,  275,  389,   -1,
   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,  400,
  401,  402,  403,   -1,  292,  293,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,   -1,   -1,
  342,  343,  310,   -1,  346,  347,  348,  349,  350,  351,
   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,
  273,  274,  364,  365,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   40,  376,   -1,  378,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,  268,
  269,   -1,   -1,  272,  273,   -1,  275,   -1,  400,  401,
  402,  403,   -1,   -1,  317,  318,  319,   -1,   -1,   -1,
   -1,   -1,   -1,  292,  293,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,   -1,   -1,  342,
  343,  310,   -1,  346,  347,  348,  349,  350,  351,  273,
  274,  354,  355,  356,   -1,   -1,    0,  360,   -1,   -1,
   -1,  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,   -1,  378,   -1,   -1,  268,  269,
   -1,   -1,  272,  273,   -1,  275,  389,   -1,   -1,   -1,
   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,
  403,   -1,  292,  293,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,   -1,   -1,  342,  343,
  310,    0,  346,  347,  348,  349,  350,  351,   -1,   -1,
  354,  355,  356,   -1,   -1,   -1,  360,  273,  274,   -1,
  364,  365,  366,   -1,   42,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   60,   -1,   -1,  389,  268,  269,   -1,   -1,
  272,  273,   -1,  275,   -1,   -1,  400,  401,  402,  403,
   -1,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,
  292,  293,  328,   91,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,   -1,   -1,  342,  343,  310,   -1,
  346,  347,  348,  349,  350,  351,  273,  274,  354,  355,
  356,   42,   -1,   44,  360,  123,   -1,   -1,  364,  365,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   60,
  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
  317,  318,  319,   -1,  400,  401,  402,  403,   -1,   42,
   91,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,   -1,   -1,  342,  343,   60,   -1,  346,
  347,  348,  349,  350,  351,   -1,   -1,  354,  355,  356,
   -1,   -1,  123,  360,   -1,   -1,   60,  364,  365,  366,
   -1,   -1,   -1,  370,  371,  372,  373,  374,   91,  376,
   -1,  378,   60,   -1,  268,  269,   -1,   -1,  272,  273,
   -1,  275,  389,   -1,   -1,   -1,  210,   91,   -1,   -1,
   -1,   -1,   -1,  400,  401,  402,  403,  221,  292,  293,
  123,   -1,   -1,   91,   -1,   -1,   -1,   42,   -1,  257,
  258,  259,   -1,  261,  262,  263,  310,  265,   -1,  123,
   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,
   -1,  279,   -1,  257,   -1,  123,   -1,   -1,   -1,  268,
  269,   -1,  290,  272,  273,   -1,  275,  257,  258,  259,
   -1,  261,  262,  263,  278,  265,  304,  305,  306,  307,
  308,   -1,   -1,  292,  293,   -1,  276,   -1,   -1,  279,
  125,  268,  269,   -1,   -1,  272,  273,   -1,  275,   -1,
  290,  310,   -1,   -1,   -1,   -1,  257,  258,  259,   -1,
  261,  262,  263,   -1,  265,  292,  293,   -1,   -1,   -1,
   -1,  272,  273,   -1,   -1,   -1,   -1,  331,  279,  333,
   -1,  335,  360,  310,   -1,   -1,   -1,   -1,   -1,  290,
   -1,   -1,   -1,  347,   -1,  349,  374,  375,  376,   -1,
   -1,   -1,   -1,   -1,  257,  258,  259,   -1,  261,  262,
  263,   -1,  265,   -1,   -1,   -1,   -1,   -1,   -1,  272,
  273,   -1,   -1,  257,  258,  259,  279,  261,  262,  263,
  125,  265,   -1,   -1,   -1,   -1,   -1,  290,  272,  273,
   -1,   -1,   -1,   -1,   -1,  279,  264,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  273,  290,   -1,   -1,  360,
   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,  287,
  288,  289,   -1,  374,  375,  376,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  437,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,  257,  258,  259,   -1,  261,  262,  263,  453,
  265,  455,   -1,   -1,   -1,  459,   -1,  360,   -1,  125,
   -1,  329,  330,   -1,  279,   -1,   -1,   -1,   -1,   -1,
   -1,  374,  375,  376,   -1,  290,  360,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,
  374,  375,  376,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,
   -1,  346,  347,  348,  349,  350,  351,   -1,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,  273,  364,
  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,   -1,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,  389,  379,  380,  381,  382,  383,
  384,  385,  386,  387,  388,  400,  401,  402,  403,   -1,
   -1,   -1,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,
   -1,  346,  347,  348,  349,  350,  351,  273,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,
  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,  317,  318,  319,   -1,  400,  401,  402,  403,   -1,
   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,   -1,
  346,  347,  348,  349,  350,  351,   -1,   -1,  354,  355,
  356,   -1,   -1,   -1,  360,  273,   -1,   -1,  364,  365,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  400,  401,  402,  403,   -1,  317,
  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,
  348,  349,  350,  351,   -1,   -1,  354,  355,  356,   -1,
   -1,   -1,  360,  273,   -1,   -1,  364,  365,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,
  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  400,  401,  402,  403,   -1,  317,  318,  319,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,
  350,  351,  273,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,   -1,   -1,   -1,  364,  365,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,
  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,   -1,
   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,  350,
  351,   -1,   -1,  354,  355,  356,   -1,   -1,   -1,  360,
  273,   -1,   -1,  364,  365,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,  125,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,
  401,  402,  403,   -1,  317,  318,  319,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,   -1,   -1,  342,
  343,   -1,   -1,  346,  347,  348,  349,  350,  351,   -1,
   -1,  354,  355,  356,   -1,   -1,   -1,  360,  273,   -1,
   -1,  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,   -1,  378,   -1,  125,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,  401,  402,
  403,   -1,  317,  318,  319,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,
   -1,  346,  347,  348,  349,  350,  351,  273,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,
  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,  317,  318,  319,   -1,  400,  401,  402,  403,   -1,
   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,   -1,
  346,  347,  348,  349,  350,  351,   -1,   -1,  354,  355,
  356,   -1,   -1,   -1,  360,  273,   -1,   -1,  364,  365,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  400,  401,  402,  403,   -1,  317,
  318,  319,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,
  348,  349,  350,  351,   -1,   -1,  354,  355,  356,   -1,
   -1,   -1,  360,  273,   -1,   -1,  364,  365,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,
  378,   -1,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  400,  401,  402,  403,   -1,  317,  318,  319,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,
  350,  351,  273,   -1,  354,  355,  356,   42,   -1,   -1,
  360,   -1,   -1,   -1,  364,  365,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   41,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,
  400,  401,  402,  403,   -1,   -1,   60,  328,   -1,  125,
  331,  332,  333,  334,  335,  336,  337,  338,  339,   -1,
   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,  350,
  351,   -1,   -1,  354,  355,  356,   -1,   91,   -1,  360,
  273,   -1,   41,  364,  365,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,
   -1,   60,   -1,   -1,   -1,   -1,  125,   -1,  389,  123,
   41,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  400,
  401,  402,  403,   -1,  317,  318,  319,   -1,   -1,   60,
   -1,   -1,   91,   -1,   -1,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,   -1,   -1,  342,
  343,   -1,   -1,  346,  347,  348,  349,  350,  351,   60,
   91,  354,  355,  356,  123,   -1,   -1,  360,   -1,   -1,
   -1,  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,  260,  378,   -1,   -1,   60,   -1,
   91,   -1,  123,   -1,   -1,   -1,  389,   -1,   -1,   -1,
  276,   -1,   -1,   -1,   -1,   -1,   -1,  400,  401,  402,
  403,   -1,  257,  258,  259,   60,  261,  262,  263,   91,
  265,   -1,  123,   -1,   -1,   -1,   -1,  272,  273,   -1,
   -1,  307,  308,   -1,  279,  311,  312,  313,  314,  315,
  316,  260,   -1,   -1,   -1,  290,   91,   -1,   -1,   60,
  264,  123,   -1,   -1,   -1,   -1,   -1,  276,   -1,  273,
   -1,   -1,   -1,   -1,  278,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,  289,   60,   -1,  123,   -1,
   91,   -1,   -1,  297,   -1,   -1,   -1,   -1,  307,  308,
   -1,   -1,  311,  312,  313,  314,  315,  316,   -1,   -1,
   -1,   -1,   -1,   60,   -1,  264,   -1,   91,   -1,   -1,
   -1,   -1,  123,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
  289,   60,   -1,  264,   91,   -1,   -1,   -1,  297,  123,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   60,
   -1,   -1,   91,  264,   -1,   -1,  123,   -1,  125,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   -1,
   91,   -1,  264,   -1,  123,   60,   -1,   -1,   -1,   -1,
   -1,  273,  303,   -1,   -1,   -1,   -1,   -1,   -1,  281,
  282,  283,  284,  285,  286,  287,  288,  289,   -1,  264,
   -1,   -1,  123,   -1,  296,   60,   91,   -1,  273,  301,
  302,   -1,   -1,  278,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,  289,   -1,   -1,   -1,   -1,   -1,
   -1,   60,  297,  264,   -1,   -1,   91,   -1,  123,   -1,
  125,   -1,  273,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   60,
  264,   -1,   91,   -1,   93,   -1,  297,   -1,  123,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,  289,   60,  264,  265,   -1,
   91,   -1,  296,   -1,  123,   -1,  273,  274,   -1,   -1,
   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,  286,
  287,  288,  289,   -1,   60,  264,  265,   91,   -1,   -1,
   -1,   -1,  123,   -1,  273,  274,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
  289,   -1,   -1,  264,  265,   91,   -1,   -1,   -1,  123,
   -1,   -1,  273,  274,   -1,   -1,   -1,   -1,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  123,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,
  285,  286,  287,  288,  289,   -1,   -1,   -1,   -1,  264,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,  274,
   -1,   -1,   -1,   -1,  261,  262,  281,  282,  283,  284,
  285,  286,  287,  288,  289,  264,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  273,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  281,  282,  283,  284,  285,  286,  287,  288,
  289,   -1,   -1,  264,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  273,   -1,   -1,   -1,   -1,  278,   -1,   -1,
  281,  282,  283,  284,  285,  286,  287,  288,  289,   -1,
  264,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  273,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  281,  282,  283,
  284,  285,  286,  287,  288,  289,   -1,   -1,  264,  273,
  274,   -1,   -1,   -1,   -1,   -1,   -1,  273,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  281,  282,  283,  284,  285,
  286,  287,  288,  289,  381,  382,  383,  384,   -1,   -1,
   -1,   -1,   -1,  390,  391,  392,  393,  394,  395,  396,
  397,  398,  399,  317,  318,  319,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,   -1,   -1,  342,  343,
   -1,   -1,  346,  347,  348,  349,  350,  351,  273,  274,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,
   -1,  346,  347,  348,  349,  350,  351,  273,  274,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,
  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,  317,  318,  319,   -1,  400,  401,  402,  403,   -1,
   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,
  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,   -1,
  346,  347,  348,  349,  350,  351,  273,  274,  354,  355,
  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,
  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,
  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,
  317,  318,  319,   -1,  400,  401,  402,  403,   -1,   -1,
   -1,  328,   -1,   -1,  331,  332,  333,  334,  335,  336,
  337,  338,  339,   -1,   -1,  342,  343,   -1,   -1,  346,
  347,  348,  349,  350,  351,  273,  274,  354,  355,  356,
   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,
   -1,   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,
   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,
  318,  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,
  328,   -1,   -1,  331,  332,  333,  334,  335,  336,  337,
  338,  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,
  348,  349,  350,  351,  273,  274,  354,  355,  356,   -1,
   -1,   -1,  360,   -1,   -1,   -1,  364,  365,  366,   -1,
   -1,   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,
  378,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,
  319,   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,
  349,  350,  351,  273,  274,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,
   -1,  331,  332,  333,  334,  335,  336,  337,  338,  339,
   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,
  350,  351,  273,   -1,  354,  355,  356,   -1,   -1,   -1,
  360,   -1,   -1,   -1,  364,  365,  366,   -1,   -1,   -1,
  370,  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,
   -1,   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,
  400,  401,  402,  403,   -1,   -1,   -1,  328,   -1,   -1,
  331,  332,  333,  334,  335,  336,  337,  338,  339,   -1,
   -1,  342,  343,   -1,   -1,  346,  347,  348,  349,  350,
  351,  273,   -1,  354,  355,  356,   -1,   -1,   -1,  360,
   -1,   -1,   -1,  364,  365,  366,   -1,   -1,   -1,  370,
  371,  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,
   -1,   -1,   -1,   -1,   -1,  317,  318,  319,   -1,  400,
  401,  402,  403,   -1,   -1,   -1,  328,   -1,   -1,  331,
  332,  333,  334,  335,  336,  337,  338,  339,   -1,   -1,
  342,  343,   -1,   -1,  346,  347,  348,  349,  350,  351,
  273,   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,
   -1,   -1,  364,  365,  366,   -1,   -1,   -1,  370,  371,
  372,  373,  374,   -1,  376,   -1,  378,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,
   -1,   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,
  402,  403,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,
  333,  334,  335,  336,  337,  338,  339,   -1,   -1,  342,
  343,   -1,   -1,  346,  347,  348,  349,  350,  351,  273,
   -1,  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,
   -1,  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,
  373,  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,
   -1,   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,
  403,   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,
  334,  335,  336,  337,  338,  339,   -1,   -1,  342,  343,
   -1,   -1,  346,  347,  348,  349,  350,  351,  273,   -1,
  354,  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,
  364,  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,
  374,   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,
   -1,   -1,  317,  318,  319,   -1,  400,  401,  402,  403,
   -1,   -1,   -1,  328,   -1,   -1,  331,  332,  333,  334,
  335,  336,  337,  338,  339,   -1,   -1,  342,  343,   -1,
   -1,  346,  347,  348,  349,  350,  351,   -1,   -1,  354,
  355,  356,   -1,   -1,   -1,  360,   -1,   -1,   -1,  364,
  365,  366,   -1,   -1,   -1,  370,  371,  372,  373,  374,
   -1,  376,   -1,  378,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  389,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  400,  401,  402,  403,  328,
   -1,   -1,  331,  332,  333,  334,  335,  336,  337,  338,
  339,   -1,   -1,  342,  343,   -1,   -1,  346,  347,  348,
  349,  350,  351,   -1,   -1,  354,  355,  356,   -1,   -1,
   -1,  360,   -1,   -1,   -1,  364,  365,  366,   -1,   -1,
   -1,  370,  371,  372,  373,  374,   -1,  376,   -1,  378,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  389,  257,  258,  259,  260,  261,  262,  263,   -1,  265,
   -1,  400,  401,  402,  403,   -1,   -1,   -1,  274,   -1,
  276,   -1,   -1,  279,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  290,
  };

#line 986 "Repil/IR/IR.jay"

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
  public const int I1 = 285;
  public const int I8 = 286;
  public const int I16 = 287;
  public const int I32 = 288;
  public const int I64 = 289;
  public const int ZEROINITIALIZER = 290;
  public const int OPAQUE = 291;
  public const int DEFINE = 292;
  public const int DECLARE = 293;
  public const int UNNAMED_ADDR = 294;
  public const int LOCAL_UNNAMED_ADDR = 295;
  public const int NOALIAS = 296;
  public const int ELLIPSIS = 297;
  public const int GLOBAL = 298;
  public const int CONSTANT = 299;
  public const int PRIVATE = 300;
  public const int INTERNAL = 301;
  public const int EXTERNAL = 302;
  public const int FASTCC = 303;
  public const int NONNULL = 304;
  public const int NOCAPTURE = 305;
  public const int WRITEONLY = 306;
  public const int READONLY = 307;
  public const int READNONE = 308;
  public const int ATTRIBUTE_GROUP_REF = 309;
  public const int ATTRIBUTES = 310;
  public const int NORECURSE = 311;
  public const int NOUNWIND = 312;
  public const int SPECULATABLE = 313;
  public const int SSP = 314;
  public const int UWTABLE = 315;
  public const int ARGMEMONLY = 316;
  public const int RET = 317;
  public const int BR = 318;
  public const int SWITCH = 319;
  public const int INDIRECTBR = 320;
  public const int INVOKE = 321;
  public const int RESUME = 322;
  public const int CATCHSWITCH = 323;
  public const int CATCHRET = 324;
  public const int CLEANUPRET = 325;
  public const int UNREACHABLE = 326;
  public const int FNEG = 327;
  public const int ADD = 328;
  public const int NUW = 329;
  public const int NSW = 330;
  public const int FADD = 331;
  public const int SUB = 332;
  public const int FSUB = 333;
  public const int MUL = 334;
  public const int FMUL = 335;
  public const int UDIV = 336;
  public const int SDIV = 337;
  public const int FDIV = 338;
  public const int UREM = 339;
  public const int SREM = 340;
  public const int FREM = 341;
  public const int SHL = 342;
  public const int LSHR = 343;
  public const int EXACT = 344;
  public const int ASHR = 345;
  public const int AND = 346;
  public const int OR = 347;
  public const int XOR = 348;
  public const int EXTRACTELEMENT = 349;
  public const int INSERTELEMENT = 350;
  public const int SHUFFLEVECTOR = 351;
  public const int EXTRACTVALUE = 352;
  public const int INSERTVALUE = 353;
  public const int ALLOCA = 354;
  public const int LOAD = 355;
  public const int STORE = 356;
  public const int FENCE = 357;
  public const int CMPXCHG = 358;
  public const int ATOMICRMW = 359;
  public const int GETELEMENTPTR = 360;
  public const int ALIGN = 361;
  public const int INBOUNDS = 362;
  public const int INRANGE = 363;
  public const int TRUNC = 364;
  public const int ZEXT = 365;
  public const int SEXT = 366;
  public const int FPTRUNC = 367;
  public const int FPEXT = 368;
  public const int TO = 369;
  public const int FPTOUI = 370;
  public const int FPTOSI = 371;
  public const int UITOFP = 372;
  public const int SITOFP = 373;
  public const int PTRTOINT = 374;
  public const int INTTOPTR = 375;
  public const int BITCAST = 376;
  public const int ADDRSPACECAST = 377;
  public const int ICMP = 378;
  public const int EQ = 379;
  public const int NE = 380;
  public const int UGT = 381;
  public const int UGE = 382;
  public const int ULT = 383;
  public const int ULE = 384;
  public const int SGT = 385;
  public const int SGE = 386;
  public const int SLT = 387;
  public const int SLE = 388;
  public const int FCMP = 389;
  public const int OEQ = 390;
  public const int OGT = 391;
  public const int OGE = 392;
  public const int OLT = 393;
  public const int OLE = 394;
  public const int ONE = 395;
  public const int ORD = 396;
  public const int UEQ = 397;
  public const int UNE = 398;
  public const int UNO = 399;
  public const int PHI = 400;
  public const int SELECT = 401;
  public const int CALL = 402;
  public const int TAIL = 403;
  public const int VA_ARG = 404;
  public const int LANDINGPAD = 405;
  public const int CATCHPAD = 406;
  public const int CLEANUPPAD = 407;
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
