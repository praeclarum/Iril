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

  protected const int yyFinal = 8;
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
//t    "module_part : ATTRIBUTES ATTRIBUTE_GROUP_REF '=' '{' attributes '}'",
//t    "module_part : META_SYMBOL '=' '!' '{' '}'",
//t    "module_part : META_SYMBOL '=' '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL '=' META_SYMBOL '(' metadata_args ')'",
//t    "module_part : META_SYMBOL '=' DISTINCT '!' '{' metadata '}'",
//t    "module_part : META_SYMBOL '=' DISTINCT META_SYMBOL '(' metadata_args ')'",
//t    "metadata_args : metadata_arg",
//t    "metadata_args : metadata_args ',' metadata_arg",
//t    "metadata_arg : SYMBOL ':' SYMBOL",
//t    "metadata_arg : SYMBOL ':' META_SYMBOL",
//t    "metadata_arg : SYMBOL ':' STRING",
//t    "metadata_arg : SYMBOL ':' constant",
//t    "metadata_arg : TYPE ':' META_SYMBOL",
//t    "metadata_kv : META_SYMBOL META_SYMBOL",
//t    "metadata_kvs : metadata_kv",
//t    "metadata_kvs : metadata_kvs metadata_kv",
//t    "metadata : metadatum",
//t    "metadata : metadata META_SYMBOL",
//t    "metadata : metadata ',' typed_value",
//t    "metadata : metadata ',' META_SYMBOL",
//t    "metadatum : typed_value",
//t    "metadatum : META_SYMBOL",
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
//t    "literal_structure : '{' type_list '}'",
//t    "type_list : type",
//t    "type_list : type_list ',' type",
//t    "type : literal_structure",
//t    "type : VOID",
//t    "type : HALF",
//t    "type : FLOAT",
//t    "type : DOUBLE",
//t    "type : I1",
//t    "type : I8",
//t    "type : I16",
//t    "type : I32",
//t    "type : I64",
//t    "type : type '(' type_list ')'",
//t    "type : type '*'",
//t    "type : LOCAL_SYMBOL",
//t    "type : '<' INTEGER X type '>'",
//t    "type : '[' INTEGER X type ']'",
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_definition : DEFINE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs metadata_kvs '{' blocks '}'",
//t    "function_definition : DEFINE NOALIAS type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs '{' blocks '}'",
//t    "function_declaration : DECLARE type GLOBAL_SYMBOL '(' parameter_list ')' attribute_group_refs",
//t    "function_declaration : DECLARE type GLOBAL_SYMBOL '(' parameter_list ')' function_addr attribute_group_refs",
//t    "parameter_list : parameter",
//t    "parameter_list : parameter_list ',' parameter",
//t    "parameter : type",
//t    "parameter : type parameter_attributes",
//t    "parameter : METADATA",
//t    "parameter_attributes : parameter_attribute",
//t    "parameter_attributes : parameter_attributes parameter_attribute",
//t    "parameter_attribute : NONNULL",
//t    "parameter_attribute : NOCAPTURE",
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
//t    "constant : NULL",
//t    "constant : FLOAT_LITERAL",
//t    "constant : INTEGER",
//t    "constant : TRUE",
//t    "constant : FALSE",
//t    "constant : UNDEF",
//t    "constant : '<' typed_constants '>'",
//t    "label_value : LABEL LOCAL_SYMBOL",
//t    "typed_value : type value",
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
//t    "function_arg : METADATA META_SYMBOL",
//t    "function_arg : METADATA META_SYMBOL '(' ')'",
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
//t    "instruction : CALL type function_pointer '(' function_args ')'",
//t    "instruction : CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : TAIL CALL type function_pointer '(' function_args ')' attribute_group_refs",
//t    "instruction : EXTRACTELEMENT typed_value ',' typed_value",
//t    "instruction : FADD type value ',' value",
//t    "instruction : FCMP fcmp_condition type value ',' value",
//t    "instruction : FMUL type value ',' value",
//t    "instruction : FPTOUI typed_value TO type",
//t    "instruction : FPTOSI typed_value TO type",
//t    "instruction : GETELEMENTPTR type ',' typed_value ',' element_indices",
//t    "instruction : GETELEMENTPTR INBOUNDS type ',' typed_value ',' element_indices",
//t    "instruction : ICMP icmp_condition type value ',' value",
//t    "instruction : INSERTELEMENT typed_value ',' typed_value ',' typed_value",
//t    "instruction : LOAD type ',' typed_value ',' ALIGN INTEGER",
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
//t    "instruction : STORE typed_value ',' typed_value ',' ALIGN INTEGER",
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
    "GLOBAL_SYMBOL","LOCAL_SYMBOL","META_SYMBOL","SYMBOL","DISTINCT",
    "METADATA","TYPE","HALF","FLOAT","DOUBLE","I1","I8","I16","I32","I64",
    "DEFINE","DECLARE","UNNAMED_ADDR","LOCAL_UNNAMED_ADDR","NOALIAS",
    "NONNULL","NOCAPTURE","WRITEONLY","ATTRIBUTE_GROUP_REF","ATTRIBUTES",
    "NORECURSE","NOUNWIND","READNONE","SPECULATABLE","SSP","UWTABLE",
    "ARGMEMONLY","RET","BR","SWITCH","INDIRECTBR","INVOKE","RESUME",
    "CATCHSWITCH","CATCHRET","CLEANUPRET","UNREACHABLE","FNEG","ADD",
    "NUW","NSW","FADD","SUB","FSUB","MUL","FMUL","UDIV","SDIV","FDIV",
    "UREM","SREM","FREM","SHL","LSHR","EXACT","ASHR","AND","OR","XOR",
    "EXTRACTELEMENT","INSERTELEMENT","SHUFFLEVECTOR","EXTRACTVALUE",
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
#line 57 "Repil/IR/IR.jay"
  {
        module.SourceFilename = (string)yyVals[0+yyTop];
    }
  break;
case 5:
#line 61 "Repil/IR/IR.jay"
  {
        module.TargetDatalayout = (string)yyVals[0+yyTop];
    }
  break;
case 6:
#line 65 "Repil/IR/IR.jay"
  {
        module.TargetTriple = (string)yyVals[0+yyTop];
    }
  break;
case 7:
#line 69 "Repil/IR/IR.jay"
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
case 43:
  case_43();
  break;
case 44:
#line 151 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((LType)yyVals[0+yyTop]);
    }
  break;
case 45:
#line 155 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 47:
#line 160 "Repil/IR/IR.jay"
  { yyVal = VoidType.Void; }
  break;
case 48:
#line 161 "Repil/IR/IR.jay"
  { yyVal = FloatType.Half; }
  break;
case 49:
#line 162 "Repil/IR/IR.jay"
  { yyVal = FloatType.Float; }
  break;
case 50:
#line 163 "Repil/IR/IR.jay"
  { yyVal = FloatType.Double; }
  break;
case 51:
#line 164 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I1; }
  break;
case 52:
#line 165 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I8; }
  break;
case 53:
#line 166 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I16; }
  break;
case 54:
#line 167 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I32; }
  break;
case 55:
#line 168 "Repil/IR/IR.jay"
  { yyVal = IntegerType.I64; }
  break;
case 56:
#line 172 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionType ((LType)yyVals[-3+yyTop], (List<LType>)yyVals[-1+yyTop]);
    }
  break;
case 57:
#line 176 "Repil/IR/IR.jay"
  {
        yyVal = new PointerType ((LType)yyVals[-1+yyTop], 0);
    }
  break;
case 58:
#line 180 "Repil/IR/IR.jay"
  {
        yyVal = new NamedType ((Symbol)yyVals[0+yyTop]);
    }
  break;
case 59:
#line 184 "Repil/IR/IR.jay"
  {
        yyVal = new VectorType ((int)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 60:
#line 188 "Repil/IR/IR.jay"
  {
        yyVal = new ArrayType ((long)(BigInteger)yyVals[-3+yyTop], (LType)yyVals[-1+yyTop]);
    }
  break;
case 61:
#line 195 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 62:
#line 199 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-10+yyTop], (GlobalSymbol)yyVals[-9+yyTop], (List<Parameter>)yyVals[-7+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 63:
#line 203 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDefinition ((LType)yyVals[-9+yyTop], (GlobalSymbol)yyVals[-8+yyTop], (List<Parameter>)yyVals[-6+yyTop], (List<Block>)yyVals[-1+yyTop]);
    }
  break;
case 64:
#line 210 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-5+yyTop], (GlobalSymbol)yyVals[-4+yyTop], (List<Parameter>)yyVals[-2+yyTop]);
    }
  break;
case 65:
#line 214 "Repil/IR/IR.jay"
  {
        yyVal = new FunctionDeclaration ((LType)yyVals[-6+yyTop], (GlobalSymbol)yyVals[-5+yyTop], (List<Parameter>)yyVals[-3+yyTop]);
    }
  break;
case 66:
#line 221 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Parameter)yyVals[0+yyTop]);
    }
  break;
case 67:
#line 225 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Parameter)yyVals[0+yyTop]);
    }
  break;
case 68:
#line 232 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[0+yyTop]);
    }
  break;
case 69:
#line 236 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, (LType)yyVals[-1+yyTop]);
    }
  break;
case 70:
#line 240 "Repil/IR/IR.jay"
  {
        yyVal = new Parameter (LocalSymbol.None, IntegerType.I32);
    }
  break;
case 72:
#line 248 "Repil/IR/IR.jay"
  {
        yyVal = ((ParameterAttributes)yyVals[-1+yyTop]) | ((ParameterAttributes)yyVals[0+yyTop]);
    }
  break;
case 73:
#line 252 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NonNull; }
  break;
case 74:
#line 253 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.NoCapture; }
  break;
case 75:
#line 254 "Repil/IR/IR.jay"
  { yyVal = ParameterAttributes.WriteOnly; }
  break;
case 81:
#line 272 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.Equal; }
  break;
case 82:
#line 273 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.NotEqual; }
  break;
case 83:
#line 274 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThan; }
  break;
case 84:
#line 275 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedGreaterThanOrEqual; }
  break;
case 85:
#line 276 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThan; }
  break;
case 86:
#line 277 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.UnsignedLessThanOrEqual; }
  break;
case 87:
#line 278 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThan; }
  break;
case 88:
#line 279 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedGreaterThanOrEqual; }
  break;
case 89:
#line 280 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThan; }
  break;
case 90:
#line 281 "Repil/IR/IR.jay"
  { yyVal = IcmpCondition.SignedLessThanOrEqual; }
  break;
case 91:
#line 285 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.True; }
  break;
case 92:
#line 286 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.False; }
  break;
case 93:
#line 287 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Ordered; }
  break;
case 94:
#line 288 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedEqual; }
  break;
case 95:
#line 289 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedNotEqual; }
  break;
case 96:
#line 290 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThan; }
  break;
case 97:
#line 291 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedGreaterThanOrEqual; }
  break;
case 98:
#line 292 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThan; }
  break;
case 99:
#line 293 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.OrderedLessThanOrEqual; }
  break;
case 100:
#line 294 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.Unordered; }
  break;
case 101:
#line 295 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedEqual; }
  break;
case 102:
#line 296 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedNotEqual; }
  break;
case 103:
#line 297 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThan; }
  break;
case 104:
#line 298 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedGreaterThanOrEqual; }
  break;
case 105:
#line 299 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThan; }
  break;
case 106:
#line 300 "Repil/IR/IR.jay"
  { yyVal = FcmpCondition.UnorderedLessThanOrEqual; }
  break;
case 108:
#line 305 "Repil/IR/IR.jay"
  { yyVal = new LocalValue ((LocalSymbol)yyVals[0+yyTop]); }
  break;
case 109:
#line 306 "Repil/IR/IR.jay"
  { yyVal = new GlobalValue ((GlobalSymbol)yyVals[0+yyTop]); }
  break;
case 110:
#line 310 "Repil/IR/IR.jay"
  { yyVal = NullConstant.Null; }
  break;
case 111:
#line 311 "Repil/IR/IR.jay"
  { yyVal = new FloatConstant ((double)yyVals[0+yyTop]); }
  break;
case 112:
#line 312 "Repil/IR/IR.jay"
  { yyVal = new IntegerConstant ((BigInteger)yyVals[0+yyTop]); }
  break;
case 113:
#line 313 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.True; }
  break;
case 114:
#line 314 "Repil/IR/IR.jay"
  { yyVal = BooleanConstant.False; }
  break;
case 115:
#line 315 "Repil/IR/IR.jay"
  { yyVal = UndefinedConstant.Undefined; }
  break;
case 116:
#line 319 "Repil/IR/IR.jay"
  {
        yyVal = new VectorConstant ((List<TypedConstant>)yyVals[-1+yyTop]);
    }
  break;
case 117:
#line 326 "Repil/IR/IR.jay"
  {
        yyVal = new LabelValue ((LocalSymbol)yyVals[0+yyTop]);
    }
  break;
case 118:
#line 333 "Repil/IR/IR.jay"
  {
        yyVal = new TypedValue ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 119:
#line 340 "Repil/IR/IR.jay"
  {
        yyVal = new TypedConstant ((LType)yyVals[-1+yyTop], (Constant)yyVals[0+yyTop]);
    }
  break;
case 120:
#line 347 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 121:
#line 351 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedConstant)yyVals[0+yyTop]);
    }
  break;
case 123:
#line 362 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 124:
#line 366 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 125:
#line 373 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Block)yyVals[0+yyTop]);
    }
  break;
case 126:
#line 377 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Block)yyVals[0+yyTop]);
    }
  break;
case 127:
#line 384 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-1+yyTop], (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 128:
#line 388 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, (List<Assignment>)yyVals[-2+yyTop], (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 129:
#line 392 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[0+yyTop]);
    }
  break;
case 130:
#line 396 "Repil/IR/IR.jay"
  {
        yyVal = new Block (LocalSymbol.None, Enumerable.Empty<Assignment>(), (TerminatorInstruction)yyVals[-1+yyTop]);
    }
  break;
case 131:
#line 403 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Assignment)yyVals[0+yyTop]);
    }
  break;
case 132:
#line 407 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (Assignment)yyVals[0+yyTop]);
    }
  break;
case 133:
#line 414 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[0+yyTop]);
    }
  break;
case 134:
#line 418 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((Instruction)yyVals[-1+yyTop]);
    }
  break;
case 135:
#line 422 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-2+yyTop], (Instruction)yyVals[0+yyTop]);
    }
  break;
case 136:
#line 426 "Repil/IR/IR.jay"
  {
        yyVal = new Assignment ((LocalSymbol)yyVals[-3+yyTop], (Instruction)yyVals[-1+yyTop]);
    }
  break;
case 138:
#line 437 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((Argument)yyVals[0+yyTop]);
    }
  break;
case 139:
#line 441 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (Argument)yyVals[0+yyTop]);
    }
  break;
case 140:
#line 448 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], (Value)yyVals[0+yyTop], (ParameterAttributes)0);
    }
  break;
case 141:
#line 452 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], ParameterAttributes.NonNull);
    }
  break;
case 142:
#line 456 "Repil/IR/IR.jay"
  {
        yyVal = new Argument ((LType)yyVals[-1+yyTop], new LocalValue ((LocalSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 143:
#line 460 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[0+yyTop]), (ParameterAttributes)0);
    }
  break;
case 144:
#line 464 "Repil/IR/IR.jay"
  {
        yyVal = new Argument (IntegerType.I32, new MetaValue ((MetaSymbol)yyVals[-2+yyTop]), (ParameterAttributes)0);
    }
  break;
case 145:
#line 471 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((PhiValue)yyVals[0+yyTop]);
    }
  break;
case 146:
#line 475 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-2+yyTop], (PhiValue)yyVals[0+yyTop]);
    }
  break;
case 147:
#line 481 "Repil/IR/IR.jay"
  {
        yyVal = new PhiValue ((Value)yyVals[-3+yyTop], (Value)yyVals[-1+yyTop]);
    }
  break;
case 148:
#line 488 "Repil/IR/IR.jay"
  {
        yyVal = NewList ((SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 149:
#line 492 "Repil/IR/IR.jay"
  {
        yyVal = ListAdd (yyVals[-1+yyTop], (SwitchCase)yyVals[0+yyTop]);
    }
  break;
case 150:
#line 499 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchCase ((TypedConstant)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 155:
#line 516 "Repil/IR/IR.jay"
  {
        yyVal = new UnconditionalBrInstruction ((LabelValue)yyVals[0+yyTop]);
    }
  break;
case 156:
#line 520 "Repil/IR/IR.jay"
  {
        yyVal = new ConditionalBrInstruction ((Value)yyVals[-4+yyTop], (LabelValue)yyVals[-2+yyTop], (LabelValue)yyVals[0+yyTop]);
    }
  break;
case 157:
#line 524 "Repil/IR/IR.jay"
  {
        yyVal = new RetInstruction ((TypedValue)yyVals[0+yyTop]);
    }
  break;
case 158:
#line 528 "Repil/IR/IR.jay"
  {
        yyVal = new SwitchInstruction ((TypedValue)yyVals[-5+yyTop], (LabelValue)yyVals[-3+yyTop], (List<SwitchCase>)yyVals[-1+yyTop]);
    }
  break;
case 159:
#line 535 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 160:
#line 539 "Repil/IR/IR.jay"
  {
        yyVal = new AddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 161:
#line 543 "Repil/IR/IR.jay"
  {
        yyVal = new AllocaInstruction ((LType)yyVals[-3+yyTop], (int)(BigInteger)yyVals[0+yyTop]);
    }
  break;
case 162:
#line 547 "Repil/IR/IR.jay"
  {
        yyVal = new AndInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 163:
#line 551 "Repil/IR/IR.jay"
  {
        yyVal = new BitcastInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 164:
#line 555 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-4+yyTop], (Value)yyVals[-3+yyTop], (List<Argument>)yyVals[-1+yyTop], false);
    }
  break;
case 165:
#line 559 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], false);
    }
  break;
case 166:
#line 563 "Repil/IR/IR.jay"
  {
        yyVal = new CallInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (List<Argument>)yyVals[-2+yyTop], true);
    }
  break;
case 167:
#line 567 "Repil/IR/IR.jay"
  {
        yyVal = new ExtractElementInstruction ((TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 168:
#line 571 "Repil/IR/IR.jay"
  {
        yyVal = new FloatAddInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 169:
#line 575 "Repil/IR/IR.jay"
  {
        yyVal = new FcmpInstruction ((FcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 170:
#line 579 "Repil/IR/IR.jay"
  {
        yyVal = new FloatMultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 171:
#line 583 "Repil/IR/IR.jay"
  {
        yyVal = new FptouiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 172:
#line 587 "Repil/IR/IR.jay"
  {
        yyVal = new FptosiInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 173:
#line 591 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 174:
#line 595 "Repil/IR/IR.jay"
  {
        yyVal = new GetElementPointerInstruction ((LType)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (List<TypedValue>)yyVals[0+yyTop]);
    }
  break;
case 175:
#line 599 "Repil/IR/IR.jay"
  {
        yyVal = new IcmpInstruction ((IcmpCondition)yyVals[-4+yyTop], (LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 176:
#line 603 "Repil/IR/IR.jay"
  {
        yyVal = new InsertElementInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 177:
#line 607 "Repil/IR/IR.jay"
  {
        yyVal = new LoadInstruction ((LType)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 178:
#line 611 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], false);
    }
  break;
case 179:
#line 615 "Repil/IR/IR.jay"
  {
        yyVal = new LshrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop], true);
    }
  break;
case 180:
#line 619 "Repil/IR/IR.jay"
  {
        yyVal = new OrInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 181:
#line 623 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 182:
#line 627 "Repil/IR/IR.jay"
  {
        yyVal = new MultiplyInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 183:
#line 631 "Repil/IR/IR.jay"
  {
        yyVal = new PhiInstruction ((LType)yyVals[-1+yyTop], (List<PhiValue>)yyVals[0+yyTop]);
    }
  break;
case 184:
#line 635 "Repil/IR/IR.jay"
  {
        yyVal = new SdivInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 185:
#line 639 "Repil/IR/IR.jay"
  {
        yyVal = new SelectInstruction ((LType)yyVals[-5+yyTop], (Value)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 186:
#line 643 "Repil/IR/IR.jay"
  {
        yyVal = new SextInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 187:
#line 647 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 188:
#line 651 "Repil/IR/IR.jay"
  {
        yyVal = new ShlInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 189:
#line 655 "Repil/IR/IR.jay"
  {
        yyVal = new ShuffleVectorInstruction ((TypedValue)yyVals[-4+yyTop], (TypedValue)yyVals[-2+yyTop], (TypedValue)yyVals[0+yyTop]);
    }
  break;
case 190:
#line 659 "Repil/IR/IR.jay"
  {
        yyVal = new SitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 191:
#line 663 "Repil/IR/IR.jay"
  {
        yyVal = new StoreInstruction ((TypedValue)yyVals[-5+yyTop], (TypedValue)yyVals[-3+yyTop]);
    }
  break;
case 192:
#line 667 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 193:
#line 671 "Repil/IR/IR.jay"
  {
        yyVal = new SubInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 194:
#line 675 "Repil/IR/IR.jay"
  {
        yyVal = new TruncInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 195:
#line 679 "Repil/IR/IR.jay"
  {
        yyVal = new UitofpInstruction ((TypedValue)yyVals[-2+yyTop], (LType)yyVals[0+yyTop]);
    }
  break;
case 196:
#line 683 "Repil/IR/IR.jay"
  {
        yyVal = new XorInstruction ((LType)yyVals[-3+yyTop], (Value)yyVals[-2+yyTop], (Value)yyVals[0+yyTop]);
    }
  break;
case 197:
#line 687 "Repil/IR/IR.jay"
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
#line 71 "Repil/IR/IR.jay"
{
        var f = (FunctionDefinition)yyVals[0+yyTop];
        module.FunctionDefinitions[f.Symbol] = f;
    }

void case_9()
#line 76 "Repil/IR/IR.jay"
{
        var f = (FunctionDeclaration)yyVals[0+yyTop];
        module.FunctionDeclarations[f.Symbol] = f;
    }

void case_43()
#line 141 "Repil/IR/IR.jay"
{
        var s = new LiteralStructureType ((List<LType>)yyVals[-1+yyTop]);
        yyVal = s;
    }

#line default
   static readonly short [] yyLhs  = {              -1,
    0,    1,    1,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    8,    8,    9,    9,    9,
    9,    9,   11,   12,   12,    7,    7,    7,    7,   13,
   13,    6,    6,   15,   15,   15,   15,   15,   15,   15,
   15,   15,    3,   16,   16,   17,   17,   17,   17,   17,
   17,   17,   17,   17,   17,   17,   17,   17,   17,   17,
    4,    4,    4,    5,    5,   18,   18,   22,   22,   22,
   23,   23,   24,   24,   24,   19,   19,   20,   20,   25,
   26,   26,   26,   26,   26,   26,   26,   26,   26,   26,
   27,   27,   27,   27,   27,   27,   27,   27,   27,   27,
   27,   27,   27,   27,   27,   27,   28,   28,   28,   10,
   10,   10,   10,   10,   10,   10,   30,   14,   31,   29,
   29,   32,   33,   33,   21,   21,   34,   34,   34,   34,
   35,   35,   37,   37,   37,   37,   39,   40,   40,   41,
   41,   41,   41,   41,   42,   42,   43,   44,   44,   45,
   46,   46,   47,   47,   36,   36,   36,   36,   38,   38,
   38,   38,   38,   38,   38,   38,   38,   38,   38,   38,
   38,   38,   38,   38,   38,   38,   38,   38,   38,   38,
   38,   38,   38,   38,   38,   38,   38,   38,   38,   38,
   38,   38,   38,   38,   38,   38,   38,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    2,    3,    4,    4,    4,    1,    1,    6,
    5,    6,    6,    7,    7,    1,    3,    3,    3,    3,
    3,    3,    2,    1,    2,    1,    2,    3,    3,    1,
    1,    1,    2,    1,    1,    1,    1,    1,    1,    1,
    3,    1,    3,    1,    3,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    4,    2,    1,    5,    5,
   11,   12,   12,    7,    8,    1,    3,    1,    2,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    3,    2,    2,    2,    1,
    3,    1,    1,    3,    1,    2,    2,    3,    1,    2,
    1,    2,    1,    2,    3,    4,    1,    1,    3,    2,
    3,    3,    2,    4,    1,    3,    5,    1,    2,    3,
    1,    2,    1,    1,    2,    7,    2,    7,    5,    6,
    5,    5,    4,    6,    7,    8,    4,    5,    6,    5,
    4,    4,    6,    7,    6,    6,    7,    5,    6,    5,
    5,    6,    3,    5,    7,    4,    5,    6,    6,    4,
    7,    5,    6,    4,    4,    5,    4,
  };
   static readonly short [] yyDefRed = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    2,
    8,    9,    0,    0,    0,    0,    0,   47,   58,   48,
   49,   50,   51,   52,   53,   54,   55,    0,    0,    0,
    0,   46,    0,    0,    0,    3,    4,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   57,    0,    0,    5,    6,    7,    0,    0,    0,    0,
    0,   43,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   16,    0,    0,   31,   11,    0,   26,   30,
    0,    0,    0,    0,    0,   70,    0,    0,   66,   56,
    0,    0,   34,   35,   36,   37,   38,   39,   40,    0,
   32,    0,    0,   13,    0,    0,    0,   27,   12,    0,
  112,  111,  113,  114,  115,  110,  109,  108,    0,  107,
  118,    0,   59,   60,   73,   74,   75,    0,   71,    0,
    0,    0,    0,   10,   33,   20,   19,   18,   21,   22,
   17,   15,   14,   29,   28,    0,    0,  120,    0,   72,
   76,   77,    0,   67,   80,    0,    0,   78,   41,  119,
    0,  116,    0,    0,    0,   79,  121,    0,    0,    0,
   24,    0,    0,   23,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  125,    0,    0,  131,    0,    0,   25,    0,    0,
  157,    0,    0,  155,    0,  153,  154,    0,    0,  151,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   81,   82,   83,   84,   85,   86,   87,   88,   89,   90,
    0,   91,   92,  103,  104,  105,  106,   94,   96,   97,
   98,   99,   95,   93,  101,  102,  100,    0,    0,    0,
    0,    0,   61,  126,    0,  132,    0,    0,    0,   63,
    0,  117,    0,    0,    0,    0,  152,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  145,    0,  137,    0,    0,    0,   62,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  167,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  159,    0,  168,  192,    0,  181,    0,  170,
  184,  187,    0,    0,  178,  162,  180,  196,    0,    0,
  161,    0,    0,    0,    0,    0,    0,    0,  146,    0,
    0,    0,    0,  138,    0,    0,    0,    0,  148,  160,
  193,  182,  188,  179,  176,  189,    0,    0,    0,  122,
  123,    0,  175,  169,    0,    0,    0,    0,    0,  140,
    0,    0,    0,  156,    0,  158,  149,  177,  191,    0,
    0,  147,  185,    0,  142,  141,    0,  139,    0,  150,
  124,  144,    0,
  };
  protected static readonly short [] yyDgoto  = {             8,
    9,   10,   32,   11,   12,  100,   78,   72,   73,  120,
  171,  172,   79,  440,  101,   45,   81,   88,  153,  157,
  211,   89,  128,  129,  158,  271,  288,  344,  147,  224,
  427,  441,  442,  212,  213,  214,  215,  216,  345,  423,
  424,  341,  342,  428,  429,  229,  230,
  };
  protected static readonly short [] yySindex = {         -198,
  -18, -110,   -8,   29,  839, 1078, -193,    0, -198,    0,
    0,    0, -156,   47,   56, -158,  -26,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1078, 1078, -123,
 -118,    0,  -12,   -6,   89,    0,    0, -107,  -93,   45,
  159,  -30,   63,   -1,   -7,   41,  -66,  -61,  178, 1078,
    0,  187,   96,    0,    0,    0, -144,  191,  102,  877,
  192,    0, 1078, 1078, 1078,  915,   97,  915,  344,  180,
  181,  114,    0, -144,  954,    0,    0,  -40,    0,    0,
   31,  915,   41,   25,    2,    0,  -31,  128,    0,    0,
  129,  202,    0,    0,    0,    0,    0,    0,    0,  268,
    0,  224,  -32,    0, -144,  146,  -23,    0,    0,  977,
    0,    0,    0,    0,    0,    0,    0,    0, 1078,    0,
    0,  151,    0,    0,    0,    0,    0,  -16,    0,  -95,
  915, -182,  -17,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  111,   24,    0,  -95,    0,
    0,    0,  -21,    0,    0,  -21,  -21,    0,    0,    0,
 1078,    0,  -21,  -77,  -21,    0,    0,  -97,   -9, 2125,
    0, -106, 2125,    0,  217, 1078, -189, 1078,  780, 1078,
  780,  780, 1078, 1078,  780,  125, 1078, 1078, 1078, 1078,
 1078, 1078, 1078, 1078, 1078,  -27, 1078, 1078, 1078, 1078,
 1078, 1078, 1078, 1078,  837,   81, 1078, 1078, 1078, -109,
  197,    0, 2125,    9,    0,    9, 2125,    0,  273, 2491,
    0,   18,  -50,    0,  250,    0,    0,   31,  780,    0,
   31,   31,  780,   31,  780,   31,   31,   31,  780, 1078,
   31,   31,   31,   31,  253,  254,  262,   38,   69,  269,
 1078,   80,  -43,  -19,   -4,   -3,   -2,    1,    5,   10,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 1078,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 1078,   14,   31,
   31, 1078,    0,    0,    9,    0,    9,    9,  349,    0,
    9,    0,  290,   70,  293,   31,    0,  299,  302,   31,
  303,   31,  304,  305,  306,   31,   31,  313,  317,  318,
  320, 1078, 1078, 1078,   19, 1078, 1078,   85, 1078, 1078,
 1078, 1078, 1078, 1078, 1078, 1078, 1078,   31,   31,  -50,
  326,    0,  330,    0,  336,   31,    9,    0,    9,   70,
  286,  -50,  334,  -50,  -50,  337,  -50,  338,  -50,  -50,
  -50,  342,  348,  -50,  -50,  -50,  -50,    0,  350,  351,
  123,  352,  355, 1078,  356,   41,   41,   41,   41,   41,
   41,   41,   41,  357,  358,  370,  324, 1078,  991,  377,
  374, 1078,    0,  -50,    0,    0,  -50,    0,  -50,    0,
    0,    0,  -50,  -50,    0,    0,    0,    0, 1078, 1078,
    0,   74,   75,  375, 1078,  -50,  -50,  -50,    0,  380,
 1019,  814,  157,    0,  991,   70,  382, 1043,    0,    0,
    0,    0,    0,    0,    0,    0,  172,  174, 1078,    0,
    0,  391,    0,    0,  343, 1078,  401,  -28,  763,    0,
  -21,  991,  165,    0,   70,    0,    0,    0,    0,  391,
 1078,    0,    0,  402,    0,    0,  -21,    0,  -21,    0,
    0,    0,  -21,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,  442,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    6,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    7,    0,    0,    0,  179,    0,    0,    0,
    0,  421,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  185,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   13,    0,    0,    0,
    0,    0,    0,    0,   72,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  425,    0, 2201,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  501,    0,  577, 2277,    0,    0,
 2353,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 1061,    0,    0,    0,    0,    0,  653,    0, 2429,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 1137, 1213, 1289, 1365, 1441,
 1517, 1593, 1669,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0, 1745,    0,    0,    0,    0,  196,    0,    0,    0,
 1821,    0,    0,    0,    0,    0,    0,    0,    0, 1897,
    0,    0,    0,    0,    0,    0, 1973,    0,    0,    0,
    0,    0, 2049,
  };
  protected static readonly short [] yyGindex = {            0,
    0,  435,  405,    0,    0,    0,  371,  378,  363,  -84,
 -134, -185,    0,  -55,  353,  404,   -5,   11,  -83, -131,
 -133,  325,   49, -126, -149,    0,    0,   73,    0, -298,
  -62,   12,   36, -184,    0,  265,  266,  260,  141,   67,
   42,    0,  106,    0,   68,  -20, -135,
  };
  protected static readonly short [] yyTable = {            33,
   34,  150,   59,  110,   80,  351,   43,  166,   50,  119,
   51,   50,   64,   51,  166,  166,  217,  139,  166,   80,
  110,  164,   44,   46,  165,  173,  294,   50,  297,   51,
  298,  168,   30,   50,  294,   51,   63,  218,   50,  219,
   51,   50,   13,   51,   46,  170,   44,   45,  156,   44,
   45,  391,   16,   50,  145,   51,  148,   83,   84,   85,
   87,  160,   87,   31,   50,  163,   51,  161,    1,    2,
   50,   65,   51,    3,    4,  222,   87,   50,   91,   51,
   50,  325,   51,  299,  109,  162,  123,    5,    6,   17,
  119,  223,  122,  307,  124,   29,    7,  307,  167,  307,
   35,  143,   37,  307,  340,  151,  152,   38,   50,  347,
   51,  155,  326,  146,  294,  349,   39,   62,   40,   50,
  221,   51,  225,  329,   50,   87,   51,  454,  374,   70,
   44,   45,   71,   47,  245,  246,  247,   90,   48,  250,
   63,  253,  254,  255,  256,  257,  258,  259,  260,   53,
   50,   54,   51,  121,  104,  146,  470,  105,   14,   15,
  233,  235,  218,  218,  239,   55,  169,   29,  130,  132,
  119,  131,  131,  228,  231,  232,  234,  236,  237,  238,
  241,  242,  243,  244,   30,   60,  142,  248,  249,  105,
  252,  149,  151,  152,  131,  169,  155,  451,   57,   64,
  452,  289,  290,  291,   65,  469,  111,  112,  452,  113,
  114,  115,  218,  116,  218,   31,  155,   66,   69,   68,
  117,  118,   68,  306,   75,   69,   68,  310,   69,  312,
   74,   82,  108,  316,  317,   18,  143,  102,  103,  143,
  140,  159,   58,  465,   19,  328,   41,   29,   42,  108,
   20,   21,   22,   23,   24,   25,   26,   27,   49,  125,
  126,  127,  133,  174,   52,  338,  368,  369,  370,   61,
  372,  373,  155,  375,  125,  126,  127,  220,  292,   64,
   64,  169,  339,  119,   64,   64,  346,  111,  112,  302,
  113,  114,  115,  304,  116,  303,  322,  323,   64,   64,
  305,  117,  118,  308,  309,  324,  311,   64,  313,  314,
  315,  330,  327,  318,  319,  320,  321,  166,  414,  467,
  251,  293,  150,  166,  376,  377,  378,  379,  380,  381,
  382,  383,  420,  350,  222,  331,  352,  473,   65,   65,
  272,  273,  354,   65,   65,  355,  357,  359,  360,  361,
  332,  333,  334,  435,  436,  335,  364,   65,   65,  336,
  365,  366,  343,  367,  337,  371,   65,  111,  112,  387,
  113,  114,  115,  388,  116,  389,  392,  394,  353,  411,
  397,  399,  356,  422,  358,  403,  146,   18,  362,  363,
  463,  404,  134,  409,  410,  412,   19,  300,  413,  415,
  416,  417,   20,   21,   22,   23,   24,   25,   26,   27,
  384,  385,  386,  418,  340,  448,  425,  426,  439,  422,
  437,  438,  146,  446,  393,  455,  395,  396,  458,  398,
  459,  400,  401,  402,  461,  462,  405,  406,  407,  408,
  464,    1,  472,   36,   56,  107,  422,  274,  275,  276,
  277,  106,  135,   67,  240,  154,  278,  279,  280,  281,
  282,  283,  284,  285,  286,  287,  430,  141,  175,  431,
  449,  432,  471,  348,  460,  433,  434,  295,  296,  301,
  111,  112,  136,  113,  114,  115,  390,  116,  443,  444,
  445,  453,  419,  468,  450,  457,  137,  138,    0,  176,
  177,  178,    0,    0,    0,    0,    0,    0,    0,    0,
  179,    0,    0,  180,  181,    0,  182,  183,    0,  184,
    0,  466,    0,    0,  185,  186,   92,    0,  187,  188,
  189,  190,  191,  192,    0,    0,  193,  194,  195,    0,
    0,    0,  196,    0,  175,   42,  197,  198,  199,  129,
    0,    0,  200,  201,  202,  203,    0,    0,  204,    0,
  205,    0,    0,   93,   94,   95,   96,   97,   98,   99,
    0,  206,    0,    0,    0,  176,  177,  178,    0,    0,
    0,    0,  207,  208,  209,  210,  179,    0,    0,  180,
  181,    0,  182,  183,    0,  184,    0,    0,    0,    0,
  185,  186,   92,    0,  187,  188,  189,  190,  191,  192,
    0,    0,  193,  194,  195,    0,    0,    0,  196,    0,
  175,    0,  197,  198,  199,  127,    0,    0,  200,  201,
  202,  203,    0,    0,  204,    0,  205,    0,    0,   93,
   94,   95,   96,   97,   98,   99,    0,  206,    0,    0,
    0,  176,  177,  178,    0,    0,    0,    0,  207,  208,
  209,  210,  179,    0,    0,  180,  181,    0,  182,  183,
    0,  184,    0,    0,    0,    0,  185,  186,    0,   42,
  187,  188,  189,  190,  191,  192,    0,    0,  193,  194,
  195,    0,    0,    0,  196,    0,  129,    0,  197,  198,
  199,  130,    0,    0,  200,  201,  202,  203,    0,    0,
  204,    0,  205,    0,    0,    0,   42,   42,   42,   42,
   42,   42,   42,  206,    0,    0,    0,  129,  129,  129,
    0,    0,    0,    0,  207,  208,  209,  210,  129,    0,
    0,  129,  129,    0,  129,  129,    0,  129,    0,    0,
    0,    0,  129,  129,    0,    0,  129,  129,  129,  129,
  129,  129,    0,    0,  129,  129,  129,    0,    0,    0,
  129,    0,  127,    0,  129,  129,  129,  128,    0,    0,
  129,  129,  129,  129,    0,    0,  129,    0,  129,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  129,
    0,    0,    0,  127,  127,  127,    0,    0,    0,    0,
  129,  129,  129,  129,  127,    0,    0,  127,  127,    0,
  127,  127,  119,  127,    0,    0,    0,    0,  127,  127,
    0,    0,  127,  127,  127,  127,  127,  127,    0,   30,
  127,  127,  127,    0,    0,    0,  127,    0,  130,    0,
  127,  127,  127,   50,    0,   51,  127,  127,  127,  127,
    0,    0,  127,    0,  127,    0,    0,    0,    0,    0,
   31,    0,    0,  119,    0,  127,    0,    0,    0,  130,
  130,  130,    0,    0,    0,    0,  127,  127,  127,  127,
  130,    0,    0,  130,  130,    0,  130,  130,   30,  130,
    0,    0,   29,    0,  130,  130,    0,    0,  130,  130,
  130,  130,  130,  130,    0,    0,  130,  130,  130,    0,
    0,    0,  130,    0,  128,    0,  130,  130,  130,   31,
    0,    0,  130,  130,  130,  130,   30,    0,  130,    0,
  130,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  130,    0,    0,    0,  128,  128,  128,    0,    0,
    0,   29,  130,  130,  130,  130,  128,   31,    0,  128,
  128,    0,  128,  128,   30,  128,    0,    0,    0,    0,
  128,  128,    0,    0,  128,  128,  128,  128,  128,  128,
    0,    0,  128,  128,  128,    0,    0,    0,  128,   29,
    0,   77,  128,  128,  128,   31,    0,    0,  128,  128,
  128,  128,    0,   30,  128,    0,  128,    0,    0,  111,
  112,    0,  113,  114,  115,    0,  116,  128,    0,    0,
    0,    0,    0,  117,  118,    0,   30,   29,  128,  128,
  128,  128,   18,    0,   31,    0,    0,    0,    0,    0,
   30,   19,    0,  125,  126,  127,    0,   20,   21,   22,
   23,   24,   25,   26,   27,    0,    0,   31,    0,    0,
  111,  112,    0,  113,  114,  115,   29,  116,   30,    0,
    0,   31,    0,    0,  117,  118,    0,    0,    0,    0,
    0,    0,    0,    0,  226,  227,    0,    0,    0,   29,
    0,   18,   30,    0,  125,  126,  127,    0,    0,   31,
   19,    0,    0,   29,    0,    0,   20,   21,   22,   23,
   24,   25,   26,   27,    0,    0,    0,    0,   28,    0,
    0,    0,    0,   31,    0,  456,    0,   30,    0,   18,
    0,   29,    0,    0,    0,    0,    0,    0,   19,   76,
    0,    0,    0,    0,   20,   21,   22,   23,   24,   25,
   26,   27,    0,    0,    0,   29,    0,    0,   31,    0,
    0,    0,    0,    0,    0,    0,    0,   18,    0,    0,
    0,    0,    0,    0,    0,    0,   19,    0,    0,    0,
   86,    0,   20,   21,   22,   23,   24,   25,   26,   27,
   29,  261,  262,  263,  264,  265,  266,  267,  268,  269,
  270,    0,    0,    0,    0,    0,   18,    0,    0,    0,
    0,    0,    0,    0,    0,   19,   76,    0,    0,    0,
    0,   20,   21,   22,   23,   24,   25,   26,   27,   18,
    0,    0,    0,    0,    0,    0,    0,    0,   19,  144,
    0,    0,    0,   18,   20,   21,   22,   23,   24,   25,
   26,   27,   19,    0,    0,    0,  421,    0,   20,   21,
   22,   23,   24,   25,   26,   27,    0,    0,    0,    0,
    0,   18,    0,    0,    0,    0,    0,    0,    0,    0,
   19,  447,    0,    0,    0,    0,   20,   21,   22,   23,
   24,   25,   26,   27,    0,   18,    0,    0,    0,    0,
    0,    0,    0,    0,   19,    0,    0,    0,    0,    0,
   20,   21,   22,   23,   24,   25,   26,   27,    0,    0,
    0,    0,  183,  183,    0,    0,    0,    0,    0,    0,
   18,    0,    0,    0,    0,    0,    0,    0,    0,   19,
    0,    0,    0,    0,    0,   20,   21,   22,   23,   24,
   25,   26,   27,  183,  183,  183,    0,    0,    0,    0,
    0,    0,    0,    0,  183,    0,    0,  183,  183,    0,
  183,  183,    0,  183,    0,    0,    0,    0,  183,  183,
    0,    0,  183,  183,  183,  183,  183,  183,    0,    0,
  183,  183,  183,    0,    0,    0,  183,    0,  194,  194,
  183,  183,  183,    0,    0,    0,  183,  183,  183,  183,
    0,    0,  183,    0,  183,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  183,    0,    0,    0,  194,
  194,  194,    0,    0,    0,    0,  183,  183,  183,  183,
  194,    0,    0,  194,  194,    0,  194,  194,    0,  194,
    0,    0,    0,    0,  194,  194,    0,    0,  194,  194,
  194,  194,  194,  194,    0,    0,  194,  194,  194,    0,
    0,    0,  194,    0,  197,  197,  194,  194,  194,    0,
    0,    0,  194,  194,  194,  194,    0,    0,  194,    0,
  194,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  194,    0,    0,    0,  197,  197,  197,    0,    0,
    0,    0,  194,  194,  194,  194,  197,    0,    0,  197,
  197,    0,  197,  197,    0,  197,    0,    0,    0,    0,
  197,  197,    0,    0,  197,  197,  197,  197,  197,  197,
    0,    0,  197,  197,  197,    0,    0,    0,  197,    0,
  186,  186,  197,  197,  197,    0,    0,    0,  197,  197,
  197,  197,    0,    0,  197,    0,  197,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  197,    0,    0,
    0,  186,  186,  186,    0,    0,    0,    0,  197,  197,
  197,  197,  186,    0,    0,  186,  186,    0,  186,  186,
    0,  186,    0,    0,    0,    0,  186,  186,    0,    0,
  186,  186,  186,  186,  186,  186,    0,    0,  186,  186,
  186,    0,    0,    0,  186,    0,  171,  171,  186,  186,
  186,    0,    0,    0,  186,  186,  186,  186,    0,    0,
  186,    0,  186,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  186,    0,    0,    0,  171,  171,  171,
    0,    0,    0,    0,  186,  186,  186,  186,  171,    0,
    0,  171,  171,    0,  171,  171,    0,  171,    0,    0,
    0,    0,  171,  171,    0,    0,  171,  171,  171,  171,
  171,  171,    0,    0,  171,  171,  171,    0,    0,    0,
  171,    0,  172,  172,  171,  171,  171,    0,    0,    0,
  171,  171,  171,  171,    0,    0,  171,    0,  171,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  171,
    0,    0,    0,  172,  172,  172,    0,    0,    0,    0,
  171,  171,  171,  171,  172,    0,    0,  172,  172,    0,
  172,  172,    0,  172,    0,    0,    0,    0,  172,  172,
    0,    0,  172,  172,  172,  172,  172,  172,    0,    0,
  172,  172,  172,    0,    0,    0,  172,    0,  195,  195,
  172,  172,  172,    0,    0,    0,  172,  172,  172,  172,
    0,    0,  172,    0,  172,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  172,    0,    0,    0,  195,
  195,  195,    0,    0,    0,    0,  172,  172,  172,  172,
  195,    0,    0,  195,  195,    0,  195,  195,    0,  195,
    0,    0,    0,    0,  195,  195,    0,    0,  195,  195,
  195,  195,  195,  195,    0,    0,  195,  195,  195,    0,
    0,    0,  195,    0,  190,  190,  195,  195,  195,    0,
    0,    0,  195,  195,  195,  195,    0,    0,  195,    0,
  195,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  195,    0,    0,    0,  190,  190,  190,    0,    0,
    0,    0,  195,  195,  195,  195,  190,    0,    0,  190,
  190,    0,  190,  190,    0,  190,    0,    0,    0,    0,
  190,  190,    0,    0,  190,  190,  190,  190,  190,  190,
    0,    0,  190,  190,  190,    0,    0,    0,  190,    0,
  163,  163,  190,  190,  190,    0,    0,    0,  190,  190,
  190,  190,    0,    0,  190,    0,  190,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  190,    0,    0,
    0,  163,  163,  163,    0,    0,    0,    0,  190,  190,
  190,  190,  163,    0,    0,  163,  163,    0,  163,  163,
    0,  163,    0,    0,    0,    0,  163,  163,    0,    0,
  163,  163,  163,  163,  163,  163,    0,    0,  163,  163,
  163,    0,    0,    0,  163,    0,  173,  173,  163,  163,
  163,    0,    0,    0,  163,  163,  163,  163,    0,    0,
  163,    0,  163,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  163,    0,    0,    0,  173,  173,  173,
    0,    0,    0,    0,  163,  163,  163,  163,  173,    0,
    0,  173,  173,    0,  173,  173,    0,  173,    0,    0,
    0,    0,  173,  173,    0,    0,  173,  173,  173,  173,
  173,  173,    0,    0,  173,  173,  173,    0,    0,    0,
  173,    0,  164,  164,  173,  173,  173,    0,    0,    0,
  173,  173,  173,  173,    0,    0,  173,    0,  173,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  173,
    0,    0,    0,  164,  164,  164,    0,    0,    0,    0,
  173,  173,  173,  173,  164,    0,    0,  164,  164,    0,
  164,  164,    0,  164,    0,    0,    0,    0,  164,  164,
    0,    0,  164,  164,  164,  164,  164,  164,    0,    0,
  164,  164,  164,    0,    0,    0,  164,    0,  174,  174,
  164,  164,  164,    0,    0,    0,  164,  164,  164,  164,
    0,    0,  164,    0,  164,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  164,    0,    0,    0,  174,
  174,  174,    0,    0,    0,    0,  164,  164,  164,  164,
  174,    0,    0,  174,  174,    0,  174,  174,    0,  174,
    0,    0,    0,    0,  174,  174,    0,    0,  174,  174,
  174,  174,  174,  174,    0,    0,  174,  174,  174,    0,
    0,    0,  174,    0,  165,  165,  174,  174,  174,    0,
    0,    0,  174,  174,  174,  174,    0,    0,  174,    0,
  174,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  174,    0,    0,    0,  165,  165,  165,    0,    0,
    0,    0,  174,  174,  174,  174,  165,    0,    0,  165,
  165,    0,  165,  165,    0,  165,    0,    0,    0,    0,
  165,  165,    0,    0,  165,  165,  165,  165,  165,  165,
    0,    0,  165,  165,  165,    0,    0,    0,  165,    0,
  166,  166,  165,  165,  165,    0,    0,    0,  165,  165,
  165,  165,    0,    0,  165,    0,  165,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  165,    0,    0,
    0,  166,  166,  166,    0,    0,    0,    0,  165,  165,
  165,  165,  166,    0,    0,  166,  166,    0,  166,  166,
    0,  166,    0,    0,    0,    0,  166,  166,    0,    0,
  166,  166,  166,  166,  166,  166,    0,    0,  166,  166,
  166,    0,    0,    0,  166,    0,  175,    0,  166,  166,
  166,    0,    0,    0,  166,  166,  166,  166,    0,    0,
  166,    0,  166,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  166,    0,    0,    0,  176,  177,  178,
    0,    0,    0,    0,  166,  166,  166,  166,  179,    0,
    0,  180,  181,    0,  182,  183,    0,  184,    0,    0,
    0,    0,  185,  186,    0,    0,  187,  188,  189,  190,
  191,  192,    0,    0,  193,  194,  195,    0,    0,    0,
  196,    0,  133,    0,  197,  198,  199,    0,    0,    0,
  200,  201,  202,  203,    0,    0,  204,    0,  205,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  206,
    0,    0,    0,  133,  133,  133,    0,    0,    0,    0,
  207,  208,  209,  210,  133,    0,    0,  133,  133,    0,
  133,  133,    0,  133,    0,    0,    0,    0,  133,  133,
    0,    0,  133,  133,  133,  133,  133,  133,    0,    0,
  133,  133,  133,    0,    0,    0,  133,    0,  134,    0,
  133,  133,  133,    0,    0,    0,  133,  133,  133,  133,
    0,    0,  133,    0,  133,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  133,    0,    0,    0,  134,
  134,  134,    0,    0,    0,    0,  133,  133,  133,  133,
  134,    0,    0,  134,  134,    0,  134,  134,    0,  134,
    0,    0,    0,    0,  134,  134,    0,    0,  134,  134,
  134,  134,  134,  134,    0,    0,  134,  134,  134,    0,
    0,    0,  134,    0,  135,    0,  134,  134,  134,    0,
    0,    0,  134,  134,  134,  134,    0,    0,  134,    0,
  134,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  134,    0,    0,    0,  135,  135,  135,    0,    0,
    0,    0,  134,  134,  134,  134,  135,    0,    0,  135,
  135,    0,  135,  135,    0,  135,    0,    0,    0,    0,
  135,  135,    0,    0,  135,  135,  135,  135,  135,  135,
    0,    0,  135,  135,  135,    0,    0,    0,  135,    0,
  136,    0,  135,  135,  135,    0,    0,    0,  135,  135,
  135,  135,    0,    0,  135,    0,  135,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  135,    0,    0,
    0,  136,  136,  136,    0,    0,    0,    0,  135,  135,
  135,  135,  136,    0,    0,  136,  136,    0,  136,  136,
    0,  136,    0,    0,    0,    0,  136,  136,    0,    0,
  136,  136,  136,  136,  136,  136,    0,    0,  136,  136,
  136,    0,    0,    0,  136,    0,    0,    0,  136,  136,
  136,    0,    0,    0,  136,  136,  136,  136,    0,    0,
  136,    0,  136,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  136,  179,    0,    0,  180,  181,    0,
  182,  183,    0,  184,  136,  136,  136,  136,  185,  186,
    0,    0,  187,  188,  189,  190,  191,  192,    0,    0,
  193,  194,  195,    0,    0,    0,  196,    0,    0,    0,
  197,  198,  199,    0,    0,    0,  200,  201,  202,  203,
    0,    0,  204,    0,  205,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  206,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  207,  208,  209,  210,
  };
  protected static readonly short [] yyCheck = {             5,
    6,  128,   33,   44,   60,  304,   33,  157,   40,   60,
   42,   40,    0,   42,  164,  165,  123,  102,  168,   75,
   44,  153,   28,   29,  156,  123,  211,   40,  214,   42,
  216,  163,   60,   40,  219,   42,   44,  172,   40,  173,
   42,   40,   61,   42,   50,  123,   41,   41,  132,   44,
   44,  350,   61,   40,  110,   42,  119,   63,   64,   65,
   66,  146,   68,   91,   40,  149,   42,   44,  267,  268,
   40,    0,   42,  272,  273,  265,   82,   40,   68,   42,
   40,   44,   42,  217,  125,   62,   62,  286,  287,   61,
   60,  281,   82,  229,   93,  123,  295,  233,  161,  235,
  294,  125,  259,  239,   91,  288,  289,   61,   40,  295,
   42,  294,   44,  119,  299,  301,   61,  125,  277,   40,
  176,   42,  178,   44,   40,  131,   42,  426,   44,  274,
  125,  125,  277,  257,  190,  191,  192,   41,  257,  195,
   44,  197,  198,  199,  200,  201,  202,  203,  204,   61,
   40,  259,   42,   81,   41,  161,  455,   44,  269,  270,
  181,  182,  297,  298,  185,  259,  273,  123,   41,   41,
   60,   44,   44,  179,  180,  181,  182,  183,  184,  185,
  186,  187,  188,  189,   60,  123,   41,  193,  194,   44,
  196,   41,  288,  289,   44,  273,  294,   41,   40,  266,
   44,  207,  208,  209,  266,   41,  257,  258,   44,  260,
  261,  262,  347,  264,  349,   91,  294,   40,  123,   41,
  271,  272,   44,  229,  123,   41,   40,  233,   44,  235,
   40,   40,  273,  239,  240,  263,   41,   58,   58,   44,
  273,  259,  273,  272,  272,  251,  273,  123,  275,  273,
  278,  279,  280,  281,  282,  283,  284,  285,  271,  291,
  292,  293,   61,  273,  271,  271,  322,  323,  324,  271,
  326,  327,  294,  329,  291,  292,  293,   61,  388,  267,
  268,  273,  288,   60,  272,  273,  292,  257,  258,  272,
  260,  261,  262,   44,  264,  223,   44,   44,  286,  287,
  228,  271,  272,  231,  232,   44,  234,  295,  236,  237,
  238,  355,   44,  241,  242,  243,  244,  467,  374,  451,
  348,  125,  449,  473,  330,  331,  332,  333,  334,  335,
  336,  337,  388,   44,  265,  355,   44,  469,  267,  268,
  260,  261,   44,  272,  273,   44,   44,   44,   44,   44,
  355,  355,  355,  409,  410,  355,   44,  286,  287,  355,
   44,   44,  290,   44,  355,  347,  295,  257,  258,   44,
  260,  261,  262,   44,  264,   40,   91,   44,  306,  257,
   44,   44,  310,  389,  312,   44,  392,  263,  316,  317,
  446,   44,  125,   44,   44,   44,  272,  125,   44,   44,
   44,   44,  278,  279,  280,  281,  282,  283,  284,  285,
  338,  339,  340,   44,   91,  421,   40,   44,   44,  425,
  347,  347,  428,   44,  352,   44,  354,  355,  257,  357,
  257,  359,  360,  361,   44,   93,  364,  365,  366,  367,
   40,    0,   41,    9,   40,   75,  452,  367,  368,  369,
  370,   74,  100,   50,  330,  131,  376,  377,  378,  379,
  380,  381,  382,  383,  384,  385,  394,  105,  272,  397,
  422,  399,  461,  125,  439,  403,  404,  213,  213,  220,
  257,  258,  259,  260,  261,  262,  346,  264,  416,  417,
  418,  425,  387,  452,  422,  428,  273,  274,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   -1,  323,
   -1,  449,   -1,   -1,  328,  329,  259,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,  125,  350,  351,  352,  125,
   -1,   -1,  356,  357,  358,  359,   -1,   -1,  362,   -1,
  364,   -1,   -1,  296,  297,  298,  299,  300,  301,  302,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  314,   -1,   -1,  317,
  318,   -1,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
  328,  329,  259,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,   -1,
  272,   -1,  350,  351,  352,  125,   -1,   -1,  356,  357,
  358,  359,   -1,   -1,  362,   -1,  364,   -1,   -1,  296,
  297,  298,  299,  300,  301,  302,   -1,  375,   -1,   -1,
   -1,  303,  304,  305,   -1,   -1,   -1,   -1,  386,  387,
  388,  389,  314,   -1,   -1,  317,  318,   -1,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,  328,  329,   -1,  259,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
  342,   -1,   -1,   -1,  346,   -1,  272,   -1,  350,  351,
  352,  125,   -1,   -1,  356,  357,  358,  359,   -1,   -1,
  362,   -1,  364,   -1,   -1,   -1,  296,  297,  298,  299,
  300,  301,  302,  375,   -1,   -1,   -1,  303,  304,  305,
   -1,   -1,   -1,   -1,  386,  387,  388,  389,  314,   -1,
   -1,  317,  318,   -1,  320,  321,   -1,  323,   -1,   -1,
   -1,   -1,  328,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,  342,   -1,   -1,   -1,
  346,   -1,  272,   -1,  350,  351,  352,  125,   -1,   -1,
  356,  357,  358,  359,   -1,   -1,  362,   -1,  364,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,
   -1,   -1,   -1,  303,  304,  305,   -1,   -1,   -1,   -1,
  386,  387,  388,  389,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   60,  323,   -1,   -1,   -1,   -1,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   60,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,  272,   -1,
  350,  351,  352,   40,   -1,   42,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   91,   -1,   -1,   60,   -1,  375,   -1,   -1,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   60,  323,
   -1,   -1,  123,   -1,  328,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,   -1,  350,  351,  352,   91,
   -1,   -1,  356,  357,  358,  359,   60,   -1,  362,   -1,
  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,  123,  386,  387,  388,  389,  314,   91,   -1,  317,
  318,   -1,  320,  321,   60,  323,   -1,   -1,   -1,   -1,
  328,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,  123,
   -1,  125,  350,  351,  352,   91,   -1,   -1,  356,  357,
  358,  359,   -1,   60,  362,   -1,  364,   -1,   -1,  257,
  258,   -1,  260,  261,  262,   -1,  264,  375,   -1,   -1,
   -1,   -1,   -1,  271,  272,   -1,   60,  123,  386,  387,
  388,  389,  263,   -1,   91,   -1,   -1,   -1,   -1,   -1,
   60,  272,   -1,  291,  292,  293,   -1,  278,  279,  280,
  281,  282,  283,  284,  285,   -1,   -1,   91,   -1,   -1,
  257,  258,   -1,  260,  261,  262,  123,  264,   60,   -1,
   -1,   91,   -1,   -1,  271,  272,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  315,  316,   -1,   -1,   -1,  123,
   -1,  263,   60,   -1,  291,  292,  293,   -1,   -1,   91,
  272,   -1,   -1,  123,   -1,   -1,  278,  279,  280,  281,
  282,  283,  284,  285,   -1,   -1,   -1,   -1,  290,   -1,
   -1,   -1,   -1,   91,   -1,   93,   -1,   60,   -1,  263,
   -1,  123,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,   -1,  278,  279,  280,  281,  282,  283,
  284,  285,   -1,   -1,   -1,  123,   -1,   -1,   91,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  263,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  272,   -1,   -1,   -1,
  276,   -1,  278,  279,  280,  281,  282,  283,  284,  285,
  123,  365,  366,  367,  368,  369,  370,  371,  372,  373,
  374,   -1,   -1,   -1,   -1,   -1,  263,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  272,  273,   -1,   -1,   -1,
   -1,  278,  279,  280,  281,  282,  283,  284,  285,  263,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,  273,
   -1,   -1,   -1,  263,  278,  279,  280,  281,  282,  283,
  284,  285,  272,   -1,   -1,   -1,  276,   -1,  278,  279,
  280,  281,  282,  283,  284,  285,   -1,   -1,   -1,   -1,
   -1,  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  272,  273,   -1,   -1,   -1,   -1,  278,  279,  280,  281,
  282,  283,  284,  285,   -1,  263,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  272,   -1,   -1,   -1,   -1,   -1,
  278,  279,  280,  281,  282,  283,  284,  285,   -1,   -1,
   -1,   -1,  272,  273,   -1,   -1,   -1,   -1,   -1,   -1,
  263,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  272,
   -1,   -1,   -1,   -1,   -1,  278,  279,  280,  281,  282,
  283,  284,  285,  303,  304,  305,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   -1,  323,   -1,   -1,   -1,   -1,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,  272,  273,
  350,  351,  352,   -1,   -1,   -1,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   -1,  323,
   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,  273,  350,  351,  352,   -1,
   -1,   -1,  356,  357,  358,  359,   -1,   -1,  362,   -1,
  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  314,   -1,   -1,  317,
  318,   -1,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
  328,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,   -1,
  272,  273,  350,  351,  352,   -1,   -1,   -1,  356,  357,
  358,  359,   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,
   -1,  303,  304,  305,   -1,   -1,   -1,   -1,  386,  387,
  388,  389,  314,   -1,   -1,  317,  318,   -1,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
  342,   -1,   -1,   -1,  346,   -1,  272,  273,  350,  351,
  352,   -1,   -1,   -1,  356,  357,  358,  359,   -1,   -1,
  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,  304,  305,
   -1,   -1,   -1,   -1,  386,  387,  388,  389,  314,   -1,
   -1,  317,  318,   -1,  320,  321,   -1,  323,   -1,   -1,
   -1,   -1,  328,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,  342,   -1,   -1,   -1,
  346,   -1,  272,  273,  350,  351,  352,   -1,   -1,   -1,
  356,  357,  358,  359,   -1,   -1,  362,   -1,  364,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,
   -1,   -1,   -1,  303,  304,  305,   -1,   -1,   -1,   -1,
  386,  387,  388,  389,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   -1,  323,   -1,   -1,   -1,   -1,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,  272,  273,
  350,  351,  352,   -1,   -1,   -1,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   -1,  323,
   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,  273,  350,  351,  352,   -1,
   -1,   -1,  356,  357,  358,  359,   -1,   -1,  362,   -1,
  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  314,   -1,   -1,  317,
  318,   -1,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
  328,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,   -1,
  272,  273,  350,  351,  352,   -1,   -1,   -1,  356,  357,
  358,  359,   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,
   -1,  303,  304,  305,   -1,   -1,   -1,   -1,  386,  387,
  388,  389,  314,   -1,   -1,  317,  318,   -1,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
  342,   -1,   -1,   -1,  346,   -1,  272,  273,  350,  351,
  352,   -1,   -1,   -1,  356,  357,  358,  359,   -1,   -1,
  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,  304,  305,
   -1,   -1,   -1,   -1,  386,  387,  388,  389,  314,   -1,
   -1,  317,  318,   -1,  320,  321,   -1,  323,   -1,   -1,
   -1,   -1,  328,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,  342,   -1,   -1,   -1,
  346,   -1,  272,  273,  350,  351,  352,   -1,   -1,   -1,
  356,  357,  358,  359,   -1,   -1,  362,   -1,  364,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,
   -1,   -1,   -1,  303,  304,  305,   -1,   -1,   -1,   -1,
  386,  387,  388,  389,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   -1,  323,   -1,   -1,   -1,   -1,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,  272,  273,
  350,  351,  352,   -1,   -1,   -1,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   -1,  323,
   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,  273,  350,  351,  352,   -1,
   -1,   -1,  356,  357,  358,  359,   -1,   -1,  362,   -1,
  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  314,   -1,   -1,  317,
  318,   -1,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
  328,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,   -1,
  272,  273,  350,  351,  352,   -1,   -1,   -1,  356,  357,
  358,  359,   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,
   -1,  303,  304,  305,   -1,   -1,   -1,   -1,  386,  387,
  388,  389,  314,   -1,   -1,  317,  318,   -1,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
  342,   -1,   -1,   -1,  346,   -1,  272,   -1,  350,  351,
  352,   -1,   -1,   -1,  356,  357,  358,  359,   -1,   -1,
  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,  304,  305,
   -1,   -1,   -1,   -1,  386,  387,  388,  389,  314,   -1,
   -1,  317,  318,   -1,  320,  321,   -1,  323,   -1,   -1,
   -1,   -1,  328,  329,   -1,   -1,  332,  333,  334,  335,
  336,  337,   -1,   -1,  340,  341,  342,   -1,   -1,   -1,
  346,   -1,  272,   -1,  350,  351,  352,   -1,   -1,   -1,
  356,  357,  358,  359,   -1,   -1,  362,   -1,  364,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,
   -1,   -1,   -1,  303,  304,  305,   -1,   -1,   -1,   -1,
  386,  387,  388,  389,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   -1,  323,   -1,   -1,   -1,   -1,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,  272,   -1,
  350,  351,  352,   -1,   -1,   -1,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,   -1,  303,
  304,  305,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  314,   -1,   -1,  317,  318,   -1,  320,  321,   -1,  323,
   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,  332,  333,
  334,  335,  336,  337,   -1,   -1,  340,  341,  342,   -1,
   -1,   -1,  346,   -1,  272,   -1,  350,  351,  352,   -1,
   -1,   -1,  356,  357,  358,  359,   -1,   -1,  362,   -1,
  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  375,   -1,   -1,   -1,  303,  304,  305,   -1,   -1,
   -1,   -1,  386,  387,  388,  389,  314,   -1,   -1,  317,
  318,   -1,  320,  321,   -1,  323,   -1,   -1,   -1,   -1,
  328,  329,   -1,   -1,  332,  333,  334,  335,  336,  337,
   -1,   -1,  340,  341,  342,   -1,   -1,   -1,  346,   -1,
  272,   -1,  350,  351,  352,   -1,   -1,   -1,  356,  357,
  358,  359,   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,
   -1,  303,  304,  305,   -1,   -1,   -1,   -1,  386,  387,
  388,  389,  314,   -1,   -1,  317,  318,   -1,  320,  321,
   -1,  323,   -1,   -1,   -1,   -1,  328,  329,   -1,   -1,
  332,  333,  334,  335,  336,  337,   -1,   -1,  340,  341,
  342,   -1,   -1,   -1,  346,   -1,   -1,   -1,  350,  351,
  352,   -1,   -1,   -1,  356,  357,  358,  359,   -1,   -1,
  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  375,  314,   -1,   -1,  317,  318,   -1,
  320,  321,   -1,  323,  386,  387,  388,  389,  328,  329,
   -1,   -1,  332,  333,  334,  335,  336,  337,   -1,   -1,
  340,  341,  342,   -1,   -1,   -1,  346,   -1,   -1,   -1,
  350,  351,  352,   -1,   -1,   -1,  356,  357,  358,  359,
   -1,   -1,  362,   -1,  364,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  375,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  386,  387,  388,  389,
  };

#line 691 "Repil/IR/IR.jay"

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
  public const int SYMBOL = 274;
  public const int DISTINCT = 275;
  public const int METADATA = 276;
  public const int TYPE = 277;
  public const int HALF = 278;
  public const int FLOAT = 279;
  public const int DOUBLE = 280;
  public const int I1 = 281;
  public const int I8 = 282;
  public const int I16 = 283;
  public const int I32 = 284;
  public const int I64 = 285;
  public const int DEFINE = 286;
  public const int DECLARE = 287;
  public const int UNNAMED_ADDR = 288;
  public const int LOCAL_UNNAMED_ADDR = 289;
  public const int NOALIAS = 290;
  public const int NONNULL = 291;
  public const int NOCAPTURE = 292;
  public const int WRITEONLY = 293;
  public const int ATTRIBUTE_GROUP_REF = 294;
  public const int ATTRIBUTES = 295;
  public const int NORECURSE = 296;
  public const int NOUNWIND = 297;
  public const int READNONE = 298;
  public const int SPECULATABLE = 299;
  public const int SSP = 300;
  public const int UWTABLE = 301;
  public const int ARGMEMONLY = 302;
  public const int RET = 303;
  public const int BR = 304;
  public const int SWITCH = 305;
  public const int INDIRECTBR = 306;
  public const int INVOKE = 307;
  public const int RESUME = 308;
  public const int CATCHSWITCH = 309;
  public const int CATCHRET = 310;
  public const int CLEANUPRET = 311;
  public const int UNREACHABLE = 312;
  public const int FNEG = 313;
  public const int ADD = 314;
  public const int NUW = 315;
  public const int NSW = 316;
  public const int FADD = 317;
  public const int SUB = 318;
  public const int FSUB = 319;
  public const int MUL = 320;
  public const int FMUL = 321;
  public const int UDIV = 322;
  public const int SDIV = 323;
  public const int FDIV = 324;
  public const int UREM = 325;
  public const int SREM = 326;
  public const int FREM = 327;
  public const int SHL = 328;
  public const int LSHR = 329;
  public const int EXACT = 330;
  public const int ASHR = 331;
  public const int AND = 332;
  public const int OR = 333;
  public const int XOR = 334;
  public const int EXTRACTELEMENT = 335;
  public const int INSERTELEMENT = 336;
  public const int SHUFFLEVECTOR = 337;
  public const int EXTRACTVALUE = 338;
  public const int INSERTVALUE = 339;
  public const int ALLOCA = 340;
  public const int LOAD = 341;
  public const int STORE = 342;
  public const int FENCE = 343;
  public const int CMPXCHG = 344;
  public const int ATOMICRMW = 345;
  public const int GETELEMENTPTR = 346;
  public const int ALIGN = 347;
  public const int INBOUNDS = 348;
  public const int INRANGE = 349;
  public const int TRUNC = 350;
  public const int ZEXT = 351;
  public const int SEXT = 352;
  public const int FPTRUNC = 353;
  public const int FPEXT = 354;
  public const int TO = 355;
  public const int FPTOUI = 356;
  public const int FPTOSI = 357;
  public const int UITOFP = 358;
  public const int SITOFP = 359;
  public const int PTRTOINT = 360;
  public const int INTTOPTR = 361;
  public const int BITCAST = 362;
  public const int ADDRSPACECAST = 363;
  public const int ICMP = 364;
  public const int EQ = 365;
  public const int NE = 366;
  public const int UGT = 367;
  public const int UGE = 368;
  public const int ULT = 369;
  public const int ULE = 370;
  public const int SGT = 371;
  public const int SGE = 372;
  public const int SLT = 373;
  public const int SLE = 374;
  public const int FCMP = 375;
  public const int OEQ = 376;
  public const int OGT = 377;
  public const int OGE = 378;
  public const int OLT = 379;
  public const int OLE = 380;
  public const int ONE = 381;
  public const int ORD = 382;
  public const int UEQ = 383;
  public const int UNE = 384;
  public const int UNO = 385;
  public const int PHI = 386;
  public const int SELECT = 387;
  public const int CALL = 388;
  public const int TAIL = 389;
  public const int VA_ARG = 390;
  public const int LANDINGPAD = 391;
  public const int CATCHPAD = 392;
  public const int CLEANUPPAD = 393;
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
