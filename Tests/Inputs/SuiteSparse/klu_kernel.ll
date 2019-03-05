; ModuleID = 'klu_kernel.c'
source_filename = "klu_kernel.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

@SuiteSparse_config = external local_unnamed_addr global %struct.SuiteSparse_config_struct, align 8
@.str = private unnamed_addr constant [19 x i8] c"input: lusize %d \0A\00", align 1
@__FUNCTION__.klu_kernel = private unnamed_addr constant [11 x i8] c"klu_kernel\00", align 1
@.str.1 = private unnamed_addr constant [13 x i8] c"klu_kernel.c\00", align 1
@.str.2 = private unnamed_addr constant [46 x i8] c"\0A\0A==================================== k: %d\0A\00", align 1
@.str.3 = private unnamed_addr constant [33 x i8] c"lup %d lusize %g lup+nunits: %g\0A\00", align 1
@.str.4 = private unnamed_addr constant [36 x i8] c"Matrix is too large (Int overflow)\0A\00", align 1
@.str.5 = private unnamed_addr constant [26 x i8] c"Matrix is too large (LU)\0A\00", align 1
@.str.6 = private unnamed_addr constant [19 x i8] c"inc LU to %d done\0A\00", align 1
@.str.7 = private unnamed_addr constant [43 x i8] c"k %d, diagrow = %d, UNFLIP (diagrow) = %d\0A\00", align 1
@.str.8 = private unnamed_addr constant [24 x i8] c"\0Ak %d : Pivot row %d : \00", align 1
@.str.9 = private unnamed_addr constant [6 x i8] c" (%g)\00", align 1
@.str.10 = private unnamed_addr constant [5 x i8] c" (0)\00", align 1
@.str.11 = private unnamed_addr constant [47 x i8] c">>>>>>>>>>>>>>>>> pivrow %d k %d off-diagonal\0A\00", align 1
@__FUNCTION__.lsolve_symbolic = private unnamed_addr constant [16 x i8] c"lsolve_symbolic\00", align 1
@.str.12 = private unnamed_addr constant [39 x i8] c"\0A ===== DFS at node %d in b, inew: %d\0A\00", align 1
@__FUNCTION__.dfs = private unnamed_addr constant [4 x i8] c"dfs\00", align 1
@.str.13 = private unnamed_addr constant [28 x i8] c"[ start dfs at %d : new %d\0A\00", align 1
@.str.14 = private unnamed_addr constant [31 x i8] c"  end   dfs at %d ] head : %d\0A\00", align 1
@__FUNCTION__.construct_column = private unnamed_addr constant [17 x i8] c"construct_column\00", align 1
@__FUNCTION__.lsolve_numeric = private unnamed_addr constant [15 x i8] c"lsolve_numeric\00", align 1
@.str.15 = private unnamed_addr constant [10 x i8] c"check %d\0A\00", align 1
@.str.16 = private unnamed_addr constant [21 x i8] c"Got pivotal row: %d\0A\00", align 1
@.str.17 = private unnamed_addr constant [43 x i8] c"%d is pruned: %d. Lpend[j] %d Lip[j+1] %d\0A\00", align 1

; Function Attrs: nounwind ssp uwtable
define i64 @klu_kernel(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32* nocapture readonly, i64, i32* nocapture, i32* nocapture, double** nocapture, double* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, double* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32, i32* nocapture readonly, double* nocapture readonly, i32* nocapture, i32* nocapture, double* nocapture, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !14 {
  %29 = alloca double, align 8
  %30 = alloca double, align 8
  %31 = alloca i32, align 4
  %32 = alloca i32, align 4
  call void @llvm.dbg.value(metadata i32 %0, metadata !58, metadata !DIExpression()), !dbg !114
  call void @llvm.dbg.value(metadata i32* %1, metadata !59, metadata !DIExpression()), !dbg !115
  call void @llvm.dbg.value(metadata i32* %2, metadata !60, metadata !DIExpression()), !dbg !116
  call void @llvm.dbg.value(metadata double* %3, metadata !61, metadata !DIExpression()), !dbg !117
  call void @llvm.dbg.value(metadata i32* %4, metadata !62, metadata !DIExpression()), !dbg !118
  call void @llvm.dbg.value(metadata i64 %5, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i32* %6, metadata !64, metadata !DIExpression()), !dbg !120
  call void @llvm.dbg.value(metadata i32* %7, metadata !65, metadata !DIExpression()), !dbg !121
  call void @llvm.dbg.value(metadata double** %8, metadata !66, metadata !DIExpression()), !dbg !122
  call void @llvm.dbg.value(metadata double* %9, metadata !67, metadata !DIExpression()), !dbg !123
  call void @llvm.dbg.value(metadata i32* %10, metadata !68, metadata !DIExpression()), !dbg !124
  call void @llvm.dbg.value(metadata i32* %11, metadata !69, metadata !DIExpression()), !dbg !125
  call void @llvm.dbg.value(metadata i32* %12, metadata !70, metadata !DIExpression()), !dbg !126
  call void @llvm.dbg.value(metadata i32* %13, metadata !71, metadata !DIExpression()), !dbg !127
  call void @llvm.dbg.value(metadata i32* %14, metadata !72, metadata !DIExpression()), !dbg !128
  call void @llvm.dbg.value(metadata i32* %15, metadata !73, metadata !DIExpression()), !dbg !129
  call void @llvm.dbg.value(metadata double* %16, metadata !74, metadata !DIExpression()), !dbg !130
  call void @llvm.dbg.value(metadata i32* %17, metadata !75, metadata !DIExpression()), !dbg !131
  call void @llvm.dbg.value(metadata i32* %18, metadata !76, metadata !DIExpression()), !dbg !132
  call void @llvm.dbg.value(metadata i32* %19, metadata !77, metadata !DIExpression()), !dbg !133
  call void @llvm.dbg.value(metadata i32* %20, metadata !78, metadata !DIExpression()), !dbg !134
  call void @llvm.dbg.value(metadata i32 %21, metadata !79, metadata !DIExpression()), !dbg !135
  call void @llvm.dbg.value(metadata i32* %22, metadata !80, metadata !DIExpression()), !dbg !136
  call void @llvm.dbg.value(metadata double* %23, metadata !81, metadata !DIExpression()), !dbg !137
  call void @llvm.dbg.value(metadata i32* %24, metadata !82, metadata !DIExpression()), !dbg !138
  call void @llvm.dbg.value(metadata i32* %25, metadata !83, metadata !DIExpression()), !dbg !139
  call void @llvm.dbg.value(metadata double* %26, metadata !84, metadata !DIExpression()), !dbg !140
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %27, metadata !85, metadata !DIExpression()), !dbg !141
  %33 = bitcast double* %29 to i8*, !dbg !142
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %33) #4, !dbg !142
  %34 = bitcast double* %30 to i8*, !dbg !143
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %34) #4, !dbg !143
  %35 = bitcast i32* %31 to i8*, !dbg !144
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %35) #4, !dbg !144
  call void @llvm.dbg.value(metadata i32 0, metadata !100, metadata !DIExpression()), !dbg !145
  store i32 0, i32* %31, align 4, !dbg !145, !tbaa !146
  %36 = bitcast i32* %32 to i8*, !dbg !144
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %36) #4, !dbg !144
  %37 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 7, !dbg !150
  %38 = load i32, i32* %37, align 8, !dbg !150, !tbaa !151
  call void @llvm.dbg.value(metadata i32 %38, metadata !106, metadata !DIExpression()), !dbg !156
  %39 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 0, !dbg !157
  %40 = load double, double* %39, align 8, !dbg !157, !tbaa !158
  call void @llvm.dbg.value(metadata double %40, metadata !90, metadata !DIExpression()), !dbg !159
  %41 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 1, !dbg !160
  %42 = load double, double* %41, align 8, !dbg !160, !tbaa !161
  call void @llvm.dbg.value(metadata double %42, metadata !91, metadata !DIExpression()), !dbg !162
  store i32 0, i32* %14, align 4, !dbg !163, !tbaa !146
  store i32 0, i32* %15, align 4, !dbg !164, !tbaa !146
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !86, metadata !DIExpression()), !dbg !165
  store double 0.000000e+00, double* %29, align 8, !dbg !166, !tbaa !168
  %43 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !169, !tbaa !172
  %44 = icmp eq i32 (i8*, ...)* %43, null, !dbg !169
  br i1 %44, label %47, label %45, !dbg !174

; <label>:45:                                     ; preds = %28
  %46 = tail call i32 (i8*, ...) %43(i8* getelementptr inbounds ([19 x i8], [19 x i8]* @.str, i64 0, i64 0), i64 %5) #4, !dbg !175
  br label %47, !dbg !175

; <label>:47:                                     ; preds = %28, %45
  %48 = load double*, double** %8, align 8, !dbg !177, !tbaa !178
  %49 = bitcast double* %48 to i8*, !dbg !179
  call void @llvm.dbg.value(metadata double* %48, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i32 0, metadata !103, metadata !DIExpression()), !dbg !180
  store i32 0, i32* %32, align 4, !dbg !181, !tbaa !146
  call void @llvm.dbg.value(metadata i32 0, metadata !104, metadata !DIExpression()), !dbg !182
  call void @llvm.dbg.value(metadata i32 0, metadata !96, metadata !DIExpression()), !dbg !183
  %50 = icmp sgt i32 %0, 0, !dbg !184
  br i1 %50, label %51, label %73, !dbg !187

; <label>:51:                                     ; preds = %47
  %52 = zext i32 %0 to i64
  br label %53, !dbg !187

; <label>:53:                                     ; preds = %53, %51
  %54 = phi i64 [ 0, %51 ], [ %58, %53 ]
  call void @llvm.dbg.value(metadata i64 %54, metadata !96, metadata !DIExpression()), !dbg !183
  %55 = getelementptr inbounds double, double* %16, i64 %54, !dbg !188
  store double 0.000000e+00, double* %55, align 8, !dbg !188, !tbaa !168
  %56 = getelementptr inbounds i32, i32* %18, i64 %54, !dbg !191
  store i32 -1, i32* %56, align 4, !dbg !192, !tbaa !146
  %57 = getelementptr inbounds i32, i32* %20, i64 %54, !dbg !193
  store i32 -1, i32* %57, align 4, !dbg !194, !tbaa !146
  %58 = add nuw nsw i64 %54, 1, !dbg !195
  call void @llvm.dbg.value(metadata i32 undef, metadata !96, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !183
  %59 = icmp eq i64 %58, %52, !dbg !184
  br i1 %59, label %60, label %53, !dbg !187, !llvm.loop !196

; <label>:60:                                     ; preds = %53
  call void @llvm.dbg.value(metadata i32 0, metadata !96, metadata !DIExpression()), !dbg !183
  %61 = icmp sgt i32 %0, 0, !dbg !198
  br i1 %61, label %62, label %73, !dbg !201

; <label>:62:                                     ; preds = %60
  %63 = zext i32 %0 to i64
  br label %64, !dbg !201

; <label>:64:                                     ; preds = %64, %62
  %65 = phi i64 [ 0, %62 ], [ %71, %64 ]
  call void @llvm.dbg.value(metadata i64 %65, metadata !96, metadata !DIExpression()), !dbg !183
  %66 = getelementptr inbounds i32, i32* %7, i64 %65, !dbg !202
  %67 = trunc i64 %65 to i32, !dbg !204
  store i32 %67, i32* %66, align 4, !dbg !204, !tbaa !146
  %68 = getelementptr inbounds i32, i32* %6, i64 %65, !dbg !205
  %69 = trunc i64 %65 to i32, !dbg !206
  %70 = sub i32 -2, %69, !dbg !206
  store i32 %70, i32* %68, align 4, !dbg !206, !tbaa !146
  %71 = add nuw nsw i64 %65, 1, !dbg !207
  call void @llvm.dbg.value(metadata i32 undef, metadata !96, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !183
  %72 = icmp eq i64 %71, %63, !dbg !198
  br i1 %72, label %74, label %64, !dbg !201, !llvm.loop !208

; <label>:73:                                     ; preds = %60, %47
  store i32 0, i32* %24, align 4, !dbg !210, !tbaa !146
  call void @llvm.dbg.value(metadata i32 0, metadata !96, metadata !DIExpression()), !dbg !183
  call void @llvm.dbg.value(metadata i64 %5, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata double* %48, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i32 0, metadata !104, metadata !DIExpression()), !dbg !182
  br label %353, !dbg !211

; <label>:74:                                     ; preds = %64
  store i32 0, i32* %24, align 4, !dbg !210, !tbaa !146
  call void @llvm.dbg.value(metadata i32 0, metadata !96, metadata !DIExpression()), !dbg !183
  call void @llvm.dbg.value(metadata i64 %5, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata double* %48, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i32 0, metadata !104, metadata !DIExpression()), !dbg !182
  %75 = icmp sgt i32 %0, 0, !dbg !212
  br i1 %75, label %76, label %353, !dbg !211

; <label>:76:                                     ; preds = %74
  %77 = sitofp i32 %0 to double
  %78 = shl nsw i32 %0, 2
  %79 = sitofp i32 %78 to double
  %80 = shl nsw i32 %0, 1
  %81 = sitofp i32 %80 to double
  %82 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 12
  %83 = bitcast double** %8 to i8**
  %84 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 11
  %85 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 11
  %86 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 14
  %87 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 15
  %88 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 10
  %89 = bitcast double* %29 to i64*
  %90 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 16
  %91 = sext i32 %21 to i64, !dbg !211
  %92 = sext i32 %0 to i64, !dbg !211
  br label %93, !dbg !211

; <label>:93:                                     ; preds = %76, %308
  %94 = phi i64 [ 0, %76 ], [ %321, %308 ]
  %95 = phi i64 [ %5, %76 ], [ %172, %308 ]
  %96 = phi double* [ %48, %76 ], [ %171, %308 ]
  %97 = phi i8* [ %49, %76 ], [ %170, %308 ]
  %98 = phi i32 [ 0, %76 ], [ %282, %308 ]
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata double* %96, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i32 %98, metadata !104, metadata !DIExpression()), !dbg !182
  call void @llvm.dbg.value(metadata i64 %94, metadata !96, metadata !DIExpression()), !dbg !183
  tail call void @ftrace(i8* getelementptr inbounds ([11 x i8], [11 x i8]* @__FUNCTION__.klu_kernel, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 761) #4, !dbg !213
  %99 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !214, !tbaa !172
  %100 = icmp eq i32 (i8*, ...)* %99, null, !dbg !214
  br i1 %100, label %104, label %101, !dbg !217

; <label>:101:                                    ; preds = %93
  %102 = trunc i64 %94 to i32, !dbg !218
  %103 = tail call i32 (i8*, ...) %99(i8* getelementptr inbounds ([46 x i8], [46 x i8]* @.str.2, i64 0, i64 0), i32 %102) #4, !dbg !218
  br label %104, !dbg !218

; <label>:104:                                    ; preds = %93, %101
  %105 = trunc i64 %94 to i32, !dbg !220
  %106 = sitofp i32 %105 to double, !dbg !220
  %107 = fsub double %77, %106, !dbg !220
  %108 = fmul double %107, 4.000000e+00, !dbg !220
  %109 = fmul double %108, 1.250000e-01, !dbg !220
  %110 = tail call double @llvm.ceil.f64(double %109), !dbg !220
  %111 = fmul double %106, 4.000000e+00, !dbg !221
  %112 = fmul double %111, 1.250000e-01, !dbg !221
  %113 = tail call double @llvm.ceil.f64(double %112), !dbg !221
  %114 = fadd double %113, %110, !dbg !222
  %115 = fmul double %107, 8.000000e+00, !dbg !223
  %116 = fmul double %115, 1.250000e-01, !dbg !223
  %117 = tail call double @llvm.ceil.f64(double %116), !dbg !223
  %118 = fadd double %117, %114, !dbg !224
  %119 = fmul double %106, 8.000000e+00, !dbg !225
  %120 = fmul double %119, 1.250000e-01, !dbg !225
  %121 = tail call double @llvm.ceil.f64(double %120), !dbg !225
  %122 = fadd double %121, %118, !dbg !226
  call void @llvm.dbg.value(metadata double %122, metadata !89, metadata !DIExpression()), !dbg !227
  %123 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !228, !tbaa !172
  %124 = icmp eq i32 (i8*, ...)* %123, null, !dbg !228
  br i1 %124, label %130, label %125, !dbg !231

; <label>:125:                                    ; preds = %104
  %126 = uitofp i64 %95 to double, !dbg !232
  %127 = sitofp i32 %98 to double, !dbg !232
  %128 = fadd double %122, %127, !dbg !232
  %129 = tail call i32 (i8*, ...) %123(i8* getelementptr inbounds ([33 x i8], [33 x i8]* @.str.3, i64 0, i64 0), i32 %98, double %126, double %128) #4, !dbg !232
  br label %130, !dbg !232

; <label>:130:                                    ; preds = %104, %125
  %131 = sitofp i32 %98 to double, !dbg !234
  %132 = fadd double %122, %131, !dbg !235
  call void @llvm.dbg.value(metadata double %132, metadata !88, metadata !DIExpression()), !dbg !236
  %133 = uitofp i64 %95 to double, !dbg !237
  %134 = fcmp ogt double %132, %133, !dbg !239
  br i1 %134, label %135, label %169, !dbg !240

; <label>:135:                                    ; preds = %130
  tail call void @ftrace(i8* getelementptr inbounds ([11 x i8], [11 x i8]* @__FUNCTION__.klu_kernel, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 779) #4, !dbg !241
  %136 = fmul double %42, %133, !dbg !243
  %137 = fadd double %136, %79, !dbg !244
  %138 = fadd double %137, 1.000000e+00, !dbg !245
  call void @llvm.dbg.value(metadata double %138, metadata !88, metadata !DIExpression()), !dbg !236
  %139 = fmul double %138, 0x3FF0000002AF31DC, !dbg !246
  %140 = fcmp ugt double %139, 0x41DFFFFFFFC00000, !dbg !246
  %141 = fcmp uno double %138, 0.000000e+00, !dbg !246
  %142 = or i1 %141, %140, !dbg !246
  br i1 %142, label %143, label %150, !dbg !246

; <label>:143:                                    ; preds = %135
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  %144 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !248, !tbaa !172
  %145 = icmp eq i32 (i8*, ...)* %144, null, !dbg !248
  br i1 %145, label %148, label %146, !dbg !252

; <label>:146:                                    ; preds = %143
  %147 = tail call i32 (i8*, ...) %144(i8* getelementptr inbounds ([36 x i8], [36 x i8]* @.str.4, i64 0, i64 0)) #4, !dbg !253
  br label %148, !dbg !253

; <label>:148:                                    ; preds = %143, %146
  %149 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 11, !dbg !255
  store i32 -4, i32* %149, align 4, !dbg !256, !tbaa !257
  br label %359, !dbg !258

; <label>:150:                                    ; preds = %135
  %151 = fadd double %136, %81, !dbg !259
  %152 = fadd double %151, 1.000000e+00, !dbg !260
  %153 = fptoui double %152 to i64, !dbg !261
  call void @llvm.dbg.value(metadata i64 %153, metadata !108, metadata !DIExpression()), !dbg !262
  %154 = bitcast double* %96 to i8*, !dbg !263
  %155 = tail call i8* @klu_realloc(i64 %153, i64 %95, i64 8, i8* %154, %struct.klu_common_struct* %27) #4, !dbg !264
  %156 = bitcast i8* %155 to double*, !dbg !264
  call void @llvm.dbg.value(metadata double* %156, metadata !95, metadata !DIExpression()), !dbg !179
  %157 = load i32, i32* %82, align 8, !dbg !265, !tbaa !266
  %158 = add nsw i32 %157, 1, !dbg !265
  store i32 %158, i32* %82, align 8, !dbg !265, !tbaa !266
  store i8* %155, i8** %83, align 8, !dbg !267, !tbaa !178
  %159 = load i32, i32* %84, align 4, !dbg !268, !tbaa !257
  %160 = icmp eq i32 %159, -2, !dbg !270
  %161 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !271, !tbaa !172
  %162 = icmp eq i32 (i8*, ...)* %161, null, !dbg !271
  br i1 %160, label %163, label %166, !dbg !274

; <label>:163:                                    ; preds = %150
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i64 %95, metadata !63, metadata !DIExpression()), !dbg !119
  br i1 %162, label %359, label %164, !dbg !275

; <label>:164:                                    ; preds = %163
  %165 = tail call i32 (i8*, ...) %161(i8* getelementptr inbounds ([26 x i8], [26 x i8]* @.str.5, i64 0, i64 0)) #4, !dbg !278
  br label %359, !dbg !278

; <label>:166:                                    ; preds = %150
  call void @llvm.dbg.value(metadata i64 %153, metadata !63, metadata !DIExpression()), !dbg !119
  br i1 %162, label %169, label %167, !dbg !281

; <label>:167:                                    ; preds = %166
  %168 = tail call i32 (i8*, ...) %161(i8* getelementptr inbounds ([19 x i8], [19 x i8]* @.str.6, i64 0, i64 0), i64 %153) #4, !dbg !282
  br label %169, !dbg !282

; <label>:169:                                    ; preds = %166, %167, %130
  %170 = phi i8* [ %155, %167 ], [ %155, %166 ], [ %97, %130 ], !dbg !284
  %171 = phi double* [ %156, %167 ], [ %156, %166 ], [ %96, %130 ], !dbg !284
  %172 = phi i64 [ %153, %167 ], [ %153, %166 ], [ %95, %130 ]
  %173 = getelementptr inbounds i32, i32* %12, i64 %94, !dbg !285
  store i32 %98, i32* %173, align 4, !dbg !286, !tbaa !146
  %174 = trunc i64 %94 to i32, !dbg !287
  %175 = tail call fastcc i32 @lsolve_symbolic(i32 %0, i32 %174, i32* %1, i32* %2, i32* %4, i32* nonnull %6, i32* %17, i32* %18, i32* %20, i32* %19, double* %171, i32 %98, i32* %10, i32* %12, i32 %21, i32* %22), !dbg !287
  call void @llvm.dbg.value(metadata i32 %175, metadata !105, metadata !DIExpression()), !dbg !288
  tail call void @ftrace(i8* getelementptr inbounds ([11 x i8], [11 x i8]* @__FUNCTION__.klu_kernel, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 849) #4, !dbg !289
  %176 = trunc i64 %94 to i32, !dbg !290
  tail call fastcc void @construct_column(i32 %176, i32* %1, i32* %2, double* %3, i32* %4, double* %16, i32 %21, i32* %22, double* %23, i32 %38, i32* %24, i32* %25, double* %26), !dbg !290
  tail call fastcc void @lsolve_numeric(i32* nonnull %6, double* %171, i32* %17, i32* %12, i32 %175, i32 %0, i32* %10, double* %16), !dbg !291
  %177 = getelementptr inbounds i32, i32* %7, i64 %94, !dbg !292
  %178 = load i32, i32* %177, align 4, !dbg !292, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %178, metadata !102, metadata !DIExpression()), !dbg !293
  %179 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !294, !tbaa !172
  %180 = icmp eq i32 (i8*, ...)* %179, null, !dbg !294
  br i1 %180, label %187, label %181, !dbg !297

; <label>:181:                                    ; preds = %169
  %182 = icmp slt i32 %178, -1, !dbg !298
  %183 = sub i32 -2, %178, !dbg !298
  %184 = select i1 %182, i32 %183, i32 %178, !dbg !298
  %185 = trunc i64 %94 to i32, !dbg !298
  %186 = tail call i32 (i8*, ...) %179(i8* getelementptr inbounds ([43 x i8], [43 x i8]* @.str.7, i64 0, i64 0), i32 %185, i32 %178, i32 %184) #4, !dbg !298
  br label %187, !dbg !298

; <label>:187:                                    ; preds = %169, %181
  call void @llvm.dbg.value(metadata double* %29, metadata !86, metadata !DIExpression()), !dbg !165
  call void @llvm.dbg.value(metadata double* %30, metadata !87, metadata !DIExpression()), !dbg !300
  call void @llvm.dbg.value(metadata i32* %31, metadata !100, metadata !DIExpression()), !dbg !145
  call void @llvm.dbg.value(metadata i32* %32, metadata !103, metadata !DIExpression()), !dbg !180
  %188 = trunc i64 %94 to i32, !dbg !301
  %189 = call fastcc i32 @lpivot(i32 %178, i32* nonnull %31, double* nonnull %29, double* nonnull %30, double %40, double* %16, double* %171, i32* nonnull %12, i32* %10, i32 %188, i32 %0, i32* %6, i32* nonnull %32, %struct.klu_common_struct* %27), !dbg !301
  %190 = icmp eq i32 %189, 0, !dbg !301
  br i1 %190, label %191, label %202, !dbg !303

; <label>:191:                                    ; preds = %187
  store i32 1, i32* %85, align 4, !dbg !304, !tbaa !257
  %192 = load i32, i32* %86, align 8, !dbg !306, !tbaa !308
  %193 = icmp eq i32 %192, -1, !dbg !309
  br i1 %193, label %194, label %199, !dbg !310

; <label>:194:                                    ; preds = %191
  %195 = add nsw i64 %94, %91, !dbg !311
  %196 = trunc i64 %195 to i32, !dbg !313
  store i32 %196, i32* %86, align 8, !dbg !313, !tbaa !308
  %197 = getelementptr inbounds i32, i32* %4, i64 %195, !dbg !314
  %198 = load i32, i32* %197, align 4, !dbg !314, !tbaa !146
  store i32 %198, i32* %87, align 4, !dbg !315, !tbaa !316
  br label %199, !dbg !317

; <label>:199:                                    ; preds = %194, %191
  %200 = load i32, i32* %88, align 8, !dbg !318, !tbaa !320
  %201 = icmp eq i32 %200, 0, !dbg !321
  br i1 %201, label %202, label %359, !dbg !322

; <label>:202:                                    ; preds = %199, %187
  %203 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !323, !tbaa !172
  %204 = icmp eq i32 (i8*, ...)* %203, null, !dbg !323
  br i1 %204, label %210, label %205, !dbg !326

; <label>:205:                                    ; preds = %202
  %206 = load i32, i32* %31, align 4, !dbg !327, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %206, metadata !100, metadata !DIExpression()), !dbg !145
  %207 = trunc i64 %94 to i32, !dbg !327
  %208 = tail call i32 (i8*, ...) %203(i8* getelementptr inbounds ([24 x i8], [24 x i8]* @.str.8, i64 0, i64 0), i32 %207, i32 %206) #4, !dbg !327
  %209 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !329, !tbaa !172
  br label %210, !dbg !327

; <label>:210:                                    ; preds = %202, %205
  %211 = phi i32 (i8*, ...)* [ null, %202 ], [ %209, %205 ], !dbg !329
  %212 = load double, double* %29, align 8, !dbg !335, !tbaa !168
  call void @llvm.dbg.value(metadata double %212, metadata !86, metadata !DIExpression()), !dbg !165
  %213 = fcmp une double %212, 0.000000e+00, !dbg !335
  %214 = icmp ne i32 (i8*, ...)* %211, null, !dbg !329
  br i1 %213, label %215, label %218, !dbg !336

; <label>:215:                                    ; preds = %210
  br i1 %214, label %216, label %221, !dbg !337

; <label>:216:                                    ; preds = %215
  %217 = tail call i32 (i8*, ...) %211(i8* getelementptr inbounds ([6 x i8], [6 x i8]* @.str.9, i64 0, i64 0), double %212) #4, !dbg !338
  br label %221, !dbg !338

; <label>:218:                                    ; preds = %210
  br i1 %214, label %219, label %221, !dbg !340

; <label>:219:                                    ; preds = %218
  %220 = tail call i32 (i8*, ...) %211(i8* getelementptr inbounds ([5 x i8], [5 x i8]* @.str.10, i64 0, i64 0)) #4, !dbg !343
  br label %221, !dbg !343

; <label>:221:                                    ; preds = %218, %219, %215, %216
  %222 = load i32, i32* %173, align 4, !dbg !346, !tbaa !146
  %223 = getelementptr inbounds i32, i32* %10, i64 %94, !dbg !347
  %224 = load i32, i32* %223, align 4, !dbg !347, !tbaa !146
  %225 = sext i32 %224 to i64, !dbg !347
  %226 = shl nsw i64 %225, 2, !dbg !347
  %227 = add nsw i64 %226, 7, !dbg !347
  %228 = lshr i64 %227, 3, !dbg !347
  %229 = trunc i64 %228 to i32, !dbg !346
  %230 = add i32 %224, %222, !dbg !346
  %231 = add i32 %230, %229, !dbg !346
  %232 = getelementptr inbounds i32, i32* %13, i64 %94, !dbg !348
  store i32 %231, i32* %232, align 4, !dbg !349, !tbaa !146
  %233 = load i32, i32* %223, align 4, !dbg !350, !tbaa !146
  %234 = sext i32 %233 to i64, !dbg !350
  %235 = shl nsw i64 %234, 2, !dbg !350
  %236 = add nsw i64 %235, 7, !dbg !350
  %237 = lshr i64 %236, 3, !dbg !350
  %238 = trunc i64 %237 to i32, !dbg !351
  call void @llvm.dbg.value(metadata i32 undef, metadata !104, metadata !DIExpression()), !dbg !182
  %239 = sub nsw i32 %0, %175, !dbg !352
  %240 = getelementptr inbounds i32, i32* %11, i64 %94, !dbg !353
  store i32 %239, i32* %240, align 4, !dbg !354, !tbaa !146
  %241 = load i32, i32* %232, align 4, !dbg !355, !tbaa !146
  %242 = sext i32 %241 to i64, !dbg !355
  %243 = getelementptr inbounds double, double* %171, i64 %242, !dbg !355
  call void @llvm.dbg.value(metadata double* %243, metadata !109, metadata !DIExpression()), !dbg !355
  call void @llvm.dbg.value(metadata i32 %239, metadata !107, metadata !DIExpression()), !dbg !356
  %244 = bitcast double* %243 to i32*, !dbg !355
  call void @llvm.dbg.value(metadata i32* %244, metadata !94, metadata !DIExpression()), !dbg !357
  %245 = sext i32 %239 to i64, !dbg !355
  %246 = shl nsw i64 %245, 2, !dbg !355
  %247 = add nsw i64 %246, 7, !dbg !355
  %248 = lshr i64 %247, 3, !dbg !355
  %249 = getelementptr inbounds double, double* %243, i64 %248, !dbg !355
  call void @llvm.dbg.value(metadata double* %249, metadata !92, metadata !DIExpression()), !dbg !358
  call void @llvm.dbg.value(metadata i32 %175, metadata !97, metadata !DIExpression()), !dbg !359
  call void @llvm.dbg.value(metadata i32 0, metadata !98, metadata !DIExpression()), !dbg !360
  %250 = icmp slt i32 %175, %0, !dbg !361
  br i1 %250, label %251, label %272, !dbg !364

; <label>:251:                                    ; preds = %221
  %252 = sext i32 %175 to i64, !dbg !364
  %253 = sub i32 %0, %175, !dbg !364
  %254 = zext i32 %253 to i64
  br label %255, !dbg !364

; <label>:255:                                    ; preds = %255, %251
  %256 = phi i64 [ 0, %251 ], [ %270, %255 ]
  %257 = phi i64 [ %252, %251 ], [ %269, %255 ]
  call void @llvm.dbg.value(metadata i64 %257, metadata !97, metadata !DIExpression()), !dbg !359
  call void @llvm.dbg.value(metadata i64 %256, metadata !98, metadata !DIExpression()), !dbg !360
  %258 = getelementptr inbounds i32, i32* %17, i64 %257, !dbg !365
  %259 = load i32, i32* %258, align 4, !dbg !365, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %259, metadata !99, metadata !DIExpression()), !dbg !367
  %260 = sext i32 %259 to i64, !dbg !368
  %261 = getelementptr inbounds i32, i32* %6, i64 %260, !dbg !368
  %262 = load i32, i32* %261, align 4, !dbg !368, !tbaa !146
  %263 = getelementptr inbounds i32, i32* %244, i64 %256, !dbg !369
  store i32 %262, i32* %263, align 4, !dbg !370, !tbaa !146
  %264 = getelementptr inbounds double, double* %16, i64 %260, !dbg !371
  %265 = bitcast double* %264 to i64*, !dbg !371
  %266 = load i64, i64* %265, align 8, !dbg !371, !tbaa !168
  %267 = getelementptr inbounds double, double* %249, i64 %256, !dbg !372
  %268 = bitcast double* %267 to i64*, !dbg !373
  store i64 %266, i64* %268, align 8, !dbg !373, !tbaa !168
  store double 0.000000e+00, double* %264, align 8, !dbg !374, !tbaa !168
  %269 = add nsw i64 %257, 1, !dbg !376
  %270 = add nuw nsw i64 %256, 1, !dbg !377
  call void @llvm.dbg.value(metadata i32 undef, metadata !97, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !359
  call void @llvm.dbg.value(metadata i32 undef, metadata !98, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !360
  %271 = icmp eq i64 %270, %254, !dbg !361
  br i1 %271, label %272, label %255, !dbg !364, !llvm.loop !378

; <label>:272:                                    ; preds = %255, %221
  %273 = load i32, i32* %240, align 4, !dbg !380, !tbaa !146
  %274 = sext i32 %273 to i64, !dbg !380
  %275 = shl nsw i64 %274, 2, !dbg !380
  %276 = add nsw i64 %275, 7, !dbg !380
  %277 = lshr i64 %276, 3, !dbg !380
  %278 = trunc i64 %277 to i32, !dbg !381
  %279 = add i32 %233, %98, !dbg !381
  %280 = add i32 %279, %238, !dbg !351
  %281 = add i32 %280, %273, !dbg !351
  %282 = add i32 %281, %278, !dbg !381
  %283 = load i64, i64* %89, align 8, !dbg !382, !tbaa !168
  call void @llvm.dbg.value(metadata double* %29, metadata !86, metadata !DIExpression(DW_OP_deref)), !dbg !165
  %284 = getelementptr inbounds double, double* %9, i64 %94, !dbg !383
  %285 = bitcast double* %284 to i64*, !dbg !384
  store i64 %283, i64* %285, align 8, !dbg !384, !tbaa !168
  %286 = load i32, i32* %31, align 4, !dbg !385, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  %287 = icmp eq i32 %286, %178, !dbg !387
  br i1 %287, label %308, label %288, !dbg !388

; <label>:288:                                    ; preds = %272
  %289 = load i32, i32* %90, align 8, !dbg !389, !tbaa !391
  %290 = add nsw i32 %289, 1, !dbg !389
  store i32 %290, i32* %90, align 8, !dbg !389, !tbaa !391
  %291 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !392, !tbaa !172
  %292 = icmp eq i32 (i8*, ...)* %291, null, !dbg !392
  br i1 %292, label %296, label %293, !dbg !395

; <label>:293:                                    ; preds = %288
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  %294 = trunc i64 %94 to i32, !dbg !396
  %295 = tail call i32 (i8*, ...) %291(i8* getelementptr inbounds ([47 x i8], [47 x i8]* @.str.11, i64 0, i64 0), i32 %286, i32 %294) #4, !dbg !396
  br label %296, !dbg !396

; <label>:296:                                    ; preds = %288, %293
  %297 = sext i32 %178 to i64, !dbg !398
  %298 = getelementptr inbounds i32, i32* %6, i64 %297, !dbg !398
  %299 = load i32, i32* %298, align 4, !dbg !398, !tbaa !146
  %300 = icmp slt i32 %299, 0, !dbg !400
  br i1 %300, label %301, label %308, !dbg !401

; <label>:301:                                    ; preds = %296
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  %302 = sext i32 %286 to i64, !dbg !402
  %303 = getelementptr inbounds i32, i32* %6, i64 %302, !dbg !402
  %304 = load i32, i32* %303, align 4, !dbg !402, !tbaa !146
  %305 = sub i32 -2, %304, !dbg !402
  call void @llvm.dbg.value(metadata i32 %305, metadata !101, metadata !DIExpression()), !dbg !404
  %306 = sext i32 %305 to i64, !dbg !405
  %307 = getelementptr inbounds i32, i32* %7, i64 %306, !dbg !405
  store i32 %178, i32* %307, align 4, !dbg !406, !tbaa !146
  store i32 %304, i32* %298, align 4, !dbg !407, !tbaa !146
  br label %308, !dbg !408

; <label>:308:                                    ; preds = %272, %296, %301
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  store i32 %286, i32* %177, align 4, !dbg !409, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  %309 = sext i32 %286 to i64, !dbg !410
  %310 = getelementptr inbounds i32, i32* %6, i64 %309, !dbg !410
  %311 = trunc i64 %94 to i32, !dbg !411
  store i32 %311, i32* %310, align 4, !dbg !411, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %286, metadata !100, metadata !DIExpression()), !dbg !145
  %312 = trunc i64 %94 to i32, !dbg !412
  tail call fastcc void @prune(i32* %20, i32* %6, i32 %312, i32 %286, double* %171, i32* %13, i32* %12, i32* nonnull %11, i32* %10), !dbg !412
  %313 = load i32, i32* %223, align 4, !dbg !413, !tbaa !146
  %314 = add nsw i32 %313, 1, !dbg !414
  %315 = load i32, i32* %14, align 4, !dbg !415, !tbaa !146
  %316 = add nsw i32 %314, %315, !dbg !415
  store i32 %316, i32* %14, align 4, !dbg !415, !tbaa !146
  %317 = load i32, i32* %240, align 4, !dbg !416, !tbaa !146
  %318 = add nsw i32 %317, 1, !dbg !417
  %319 = load i32, i32* %15, align 4, !dbg !418, !tbaa !146
  %320 = add nsw i32 %318, %319, !dbg !418
  store i32 %320, i32* %15, align 4, !dbg !418, !tbaa !146
  %321 = add nuw nsw i64 %94, 1, !dbg !419
  call void @llvm.dbg.value(metadata i64 %172, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata double* %171, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i32 %282, metadata !104, metadata !DIExpression()), !dbg !182
  call void @llvm.dbg.value(metadata i32 undef, metadata !96, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !183
  %322 = icmp slt i64 %321, %92, !dbg !212
  br i1 %322, label %93, label %323, !dbg !211, !llvm.loop !420

; <label>:323:                                    ; preds = %308
  %324 = sext i32 %282 to i64, !dbg !422
  call void @llvm.dbg.value(metadata double* %171, metadata !95, metadata !DIExpression()), !dbg !179
  call void @llvm.dbg.value(metadata i64 %172, metadata !63, metadata !DIExpression()), !dbg !119
  call void @llvm.dbg.value(metadata i32 0, metadata !97, metadata !DIExpression()), !dbg !359
  %325 = icmp sgt i32 %0, 0, !dbg !422
  br i1 %325, label %326, label %353, !dbg !425

; <label>:326:                                    ; preds = %323
  %327 = zext i32 %0 to i64
  br label %328, !dbg !425

; <label>:328:                                    ; preds = %350, %326
  %329 = phi i64 [ 0, %326 ], [ %351, %350 ]
  call void @llvm.dbg.value(metadata i64 %329, metadata !97, metadata !DIExpression()), !dbg !359
  %330 = getelementptr inbounds i32, i32* %12, i64 %329, !dbg !426
  %331 = load i32, i32* %330, align 4, !dbg !426, !tbaa !146
  %332 = sext i32 %331 to i64, !dbg !428
  %333 = getelementptr inbounds double, double* %171, i64 %332, !dbg !428
  %334 = bitcast double* %333 to i32*, !dbg !429
  call void @llvm.dbg.value(metadata i32* %334, metadata !93, metadata !DIExpression()), !dbg !430
  call void @llvm.dbg.value(metadata i32 0, metadata !98, metadata !DIExpression()), !dbg !360
  %335 = getelementptr inbounds i32, i32* %10, i64 %329, !dbg !431
  %336 = load i32, i32* %335, align 4, !dbg !431, !tbaa !146
  %337 = icmp sgt i32 %336, 0, !dbg !434
  br i1 %337, label %338, label %350, !dbg !435

; <label>:338:                                    ; preds = %328
  br label %339, !dbg !436

; <label>:339:                                    ; preds = %338, %339
  %340 = phi i64 [ %346, %339 ], [ 0, %338 ]
  call void @llvm.dbg.value(metadata i64 %340, metadata !98, metadata !DIExpression()), !dbg !360
  %341 = getelementptr inbounds i32, i32* %334, i64 %340, !dbg !436
  %342 = load i32, i32* %341, align 4, !dbg !436, !tbaa !146
  %343 = sext i32 %342 to i64, !dbg !438
  %344 = getelementptr inbounds i32, i32* %6, i64 %343, !dbg !438
  %345 = load i32, i32* %344, align 4, !dbg !438, !tbaa !146
  store i32 %345, i32* %341, align 4, !dbg !439, !tbaa !146
  %346 = add nuw nsw i64 %340, 1, !dbg !440
  call void @llvm.dbg.value(metadata i32 undef, metadata !98, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !360
  %347 = load i32, i32* %335, align 4, !dbg !431, !tbaa !146
  %348 = sext i32 %347 to i64, !dbg !434
  %349 = icmp slt i64 %346, %348, !dbg !434
  br i1 %349, label %339, label %350, !dbg !435, !llvm.loop !441

; <label>:350:                                    ; preds = %339, %328
  %351 = add nuw nsw i64 %329, 1, !dbg !443
  call void @llvm.dbg.value(metadata i32 undef, metadata !97, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !359
  %352 = icmp eq i64 %351, %327, !dbg !422
  br i1 %352, label %353, label %328, !dbg !425, !llvm.loop !444

; <label>:353:                                    ; preds = %350, %74, %73, %323
  %354 = phi i64 [ %172, %323 ], [ %5, %73 ], [ %5, %74 ], [ %172, %350 ]
  %355 = phi i8* [ %170, %323 ], [ %49, %73 ], [ %49, %74 ], [ %170, %350 ]
  %356 = phi i64 [ %324, %323 ], [ 0, %73 ], [ 0, %74 ], [ %324, %350 ]
  tail call void @ftrace(i8* getelementptr inbounds ([11 x i8], [11 x i8]* @__FUNCTION__.klu_kernel, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 1020) #4, !dbg !446
  call void @llvm.dbg.value(metadata i64 %324, metadata !108, metadata !DIExpression()), !dbg !262
  %357 = tail call i8* @klu_realloc(i64 %356, i64 %354, i64 8, i8* %355, %struct.klu_common_struct* %27) #4, !dbg !447
  call void @llvm.dbg.value(metadata i8* %357, metadata !95, metadata !DIExpression()), !dbg !179
  %358 = bitcast double** %8 to i8**, !dbg !448
  store i8* %357, i8** %358, align 8, !dbg !448, !tbaa !178
  br label %359, !dbg !449

; <label>:359:                                    ; preds = %199, %164, %163, %353, %148
  %360 = phi i64 [ %95, %148 ], [ %356, %353 ], [ %95, %163 ], [ %95, %164 ], [ %172, %199 ], !dbg !284
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %36) #4, !dbg !450
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %35) #4, !dbg !450
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %34) #4, !dbg !450
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %33) #4, !dbg !450
  ret i64 %360, !dbg !450
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

declare void @ftrace(i8*, i8*, i32) local_unnamed_addr #2

; Function Attrs: nounwind readnone speculatable
declare double @llvm.ceil.f64(double) #3

declare i8* @klu_realloc(i64, i64, i64, i8*, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define internal fastcc i32 @lsolve_symbolic(i32, i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture, i32* nocapture, i32* nocapture readonly, i32* nocapture, double* nocapture, i32, i32* nocapture, i32* nocapture readonly, i32, i32* nocapture readonly) unnamed_addr #0 !dbg !451 {
  %17 = alloca i32, align 4
  call void @llvm.dbg.value(metadata i32 %0, metadata !455, metadata !DIExpression()), !dbg !479
  call void @llvm.dbg.value(metadata i32 %1, metadata !456, metadata !DIExpression()), !dbg !480
  call void @llvm.dbg.value(metadata i32* %2, metadata !457, metadata !DIExpression()), !dbg !481
  call void @llvm.dbg.value(metadata i32* %3, metadata !458, metadata !DIExpression()), !dbg !482
  call void @llvm.dbg.value(metadata i32* %4, metadata !459, metadata !DIExpression()), !dbg !483
  call void @llvm.dbg.value(metadata i32* %5, metadata !460, metadata !DIExpression()), !dbg !484
  call void @llvm.dbg.value(metadata i32* %6, metadata !461, metadata !DIExpression()), !dbg !485
  call void @llvm.dbg.value(metadata i32* %7, metadata !462, metadata !DIExpression()), !dbg !486
  call void @llvm.dbg.value(metadata i32* %8, metadata !463, metadata !DIExpression()), !dbg !487
  call void @llvm.dbg.value(metadata i32* %9, metadata !464, metadata !DIExpression()), !dbg !488
  call void @llvm.dbg.value(metadata double* %10, metadata !465, metadata !DIExpression()), !dbg !489
  call void @llvm.dbg.value(metadata i32 %11, metadata !466, metadata !DIExpression()), !dbg !490
  call void @llvm.dbg.value(metadata i32* %12, metadata !467, metadata !DIExpression()), !dbg !491
  call void @llvm.dbg.value(metadata i32* %13, metadata !468, metadata !DIExpression()), !dbg !492
  call void @llvm.dbg.value(metadata i32 %14, metadata !469, metadata !DIExpression()), !dbg !493
  call void @llvm.dbg.value(metadata i32* %15, metadata !470, metadata !DIExpression()), !dbg !494
  %18 = bitcast i32* %17 to i8*, !dbg !495
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %18) #4, !dbg !495
  call void @llvm.dbg.value(metadata i32 %0, metadata !477, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i32 0, metadata !478, metadata !DIExpression()), !dbg !497
  store i32 0, i32* %17, align 4, !dbg !498, !tbaa !146
  %19 = sext i32 %11 to i64, !dbg !499
  %20 = getelementptr inbounds double, double* %10, i64 %19, !dbg !499
  %21 = bitcast double* %20 to i32*, !dbg !500
  call void @llvm.dbg.value(metadata i32* %21, metadata !471, metadata !DIExpression()), !dbg !501
  %22 = add nsw i32 %14, %1, !dbg !502
  call void @llvm.dbg.value(metadata i32 %22, metadata !476, metadata !DIExpression()), !dbg !503
  %23 = sext i32 %22 to i64, !dbg !504
  %24 = getelementptr inbounds i32, i32* %4, i64 %23, !dbg !504
  %25 = load i32, i32* %24, align 4, !dbg !504, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %25, metadata !475, metadata !DIExpression()), !dbg !505
  %26 = add nsw i32 %25, 1, !dbg !506
  %27 = sext i32 %26 to i64, !dbg !507
  %28 = getelementptr inbounds i32, i32* %2, i64 %27, !dbg !507
  %29 = load i32, i32* %28, align 4, !dbg !507, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %29, metadata !474, metadata !DIExpression()), !dbg !508
  %30 = sext i32 %25 to i64, !dbg !509
  %31 = getelementptr inbounds i32, i32* %2, i64 %30, !dbg !509
  %32 = load i32, i32* %31, align 4, !dbg !509, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %32, metadata !473, metadata !DIExpression()), !dbg !511
  call void @llvm.dbg.value(metadata i32 %0, metadata !477, metadata !DIExpression()), !dbg !496
  %33 = icmp slt i32 %32, %29, !dbg !512
  br i1 %33, label %34, label %75, !dbg !514

; <label>:34:                                     ; preds = %16
  %35 = sext i32 %32 to i64, !dbg !514
  br label %36, !dbg !514

; <label>:36:                                     ; preds = %70, %34
  %37 = phi i64 [ %35, %34 ], [ %72, %70 ]
  %38 = phi i32 [ %0, %34 ], [ %71, %70 ]
  call void @llvm.dbg.value(metadata i32 %38, metadata !477, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i64 %37, metadata !473, metadata !DIExpression()), !dbg !511
  tail call void @ftrace(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__FUNCTION__.lsolve_symbolic, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 174) #4, !dbg !515
  %39 = getelementptr inbounds i32, i32* %3, i64 %37, !dbg !517
  %40 = load i32, i32* %39, align 4, !dbg !517, !tbaa !146
  %41 = sext i32 %40 to i64, !dbg !518
  %42 = getelementptr inbounds i32, i32* %15, i64 %41, !dbg !518
  %43 = load i32, i32* %42, align 4, !dbg !518, !tbaa !146
  %44 = sub nsw i32 %43, %14, !dbg !519
  call void @llvm.dbg.value(metadata i32 %44, metadata !472, metadata !DIExpression()), !dbg !520
  %45 = icmp slt i32 %44, 0, !dbg !521
  br i1 %45, label %70, label %46, !dbg !523

; <label>:46:                                     ; preds = %36
  %47 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !524, !tbaa !172
  %48 = icmp eq i32 (i8*, ...)* %47, null, !dbg !524
  br i1 %48, label %54, label %49, !dbg !527

; <label>:49:                                     ; preds = %46
  %50 = sext i32 %44 to i64, !dbg !528
  %51 = getelementptr inbounds i32, i32* %5, i64 %50, !dbg !528
  %52 = load i32, i32* %51, align 4, !dbg !528, !tbaa !146
  %53 = tail call i32 (i8*, ...) %47(i8* getelementptr inbounds ([39 x i8], [39 x i8]* @.str.12, i64 0, i64 0), i32 %44, i32 %52) #4, !dbg !528
  br label %54, !dbg !528

; <label>:54:                                     ; preds = %46, %49
  %55 = sext i32 %44 to i64, !dbg !530
  %56 = getelementptr inbounds i32, i32* %7, i64 %55, !dbg !530
  %57 = load i32, i32* %56, align 4, !dbg !530, !tbaa !146
  %58 = icmp eq i32 %57, %1, !dbg !532
  br i1 %58, label %70, label %59, !dbg !533

; <label>:59:                                     ; preds = %54
  tail call void @ftrace(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__FUNCTION__.lsolve_symbolic, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 182) #4, !dbg !534
  %60 = getelementptr inbounds i32, i32* %5, i64 %55, !dbg !536
  %61 = load i32, i32* %60, align 4, !dbg !536, !tbaa !146
  %62 = icmp sgt i32 %61, -1, !dbg !538
  br i1 %62, label %63, label %65, !dbg !539

; <label>:63:                                     ; preds = %59
  tail call void @ftrace(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__FUNCTION__.lsolve_symbolic, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 185) #4, !dbg !540
  call void @llvm.dbg.value(metadata i32* %17, metadata !478, metadata !DIExpression()), !dbg !497
  %64 = call fastcc i32 @dfs(i32 %44, i32 %1, i32* nonnull %5, i32* %12, i32* %13, i32* %6, i32* nonnull %7, i32* %8, i32 %38, double* %10, i32* %21, i32* nonnull %17, i32* %9), !dbg !542
  call void @llvm.dbg.value(metadata i32 %64, metadata !477, metadata !DIExpression()), !dbg !496
  br label %70, !dbg !543

; <label>:65:                                     ; preds = %59
  tail call void @ftrace(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__FUNCTION__.lsolve_symbolic, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 191) #4, !dbg !544
  store i32 %1, i32* %56, align 4, !dbg !546, !tbaa !146
  %66 = load i32, i32* %17, align 4, !dbg !547, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %66, metadata !478, metadata !DIExpression()), !dbg !497
  %67 = sext i32 %66 to i64, !dbg !548
  %68 = getelementptr inbounds i32, i32* %21, i64 %67, !dbg !548
  store i32 %44, i32* %68, align 4, !dbg !549, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %66, metadata !478, metadata !DIExpression()), !dbg !497
  %69 = add nsw i32 %66, 1, !dbg !550
  call void @llvm.dbg.value(metadata i32 %69, metadata !478, metadata !DIExpression()), !dbg !497
  store i32 %69, i32* %17, align 4, !dbg !550, !tbaa !146
  br label %70

; <label>:70:                                     ; preds = %54, %65, %63, %36
  %71 = phi i32 [ %38, %36 ], [ %64, %63 ], [ %38, %65 ], [ %38, %54 ], !dbg !551
  %72 = add nsw i64 %37, 1, !dbg !552
  call void @llvm.dbg.value(metadata i32 %71, metadata !477, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i32 undef, metadata !473, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !511
  %73 = trunc i64 %72 to i32, !dbg !512
  %74 = icmp eq i32 %29, %73, !dbg !512
  br i1 %74, label %75, label %36, !dbg !514, !llvm.loop !553

; <label>:75:                                     ; preds = %70, %16
  %76 = phi i32 [ %0, %16 ], [ %71, %70 ]
  call void @llvm.dbg.value(metadata i32 %76, metadata !477, metadata !DIExpression()), !dbg !496
  tail call void @ftrace(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__FUNCTION__.lsolve_symbolic, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 200) #4, !dbg !555
  %77 = load i32, i32* %17, align 4, !dbg !556, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %77, metadata !478, metadata !DIExpression()), !dbg !497
  %78 = sext i32 %1 to i64, !dbg !557
  %79 = getelementptr inbounds i32, i32* %12, i64 %78, !dbg !557
  store i32 %77, i32* %79, align 4, !dbg !558, !tbaa !146
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %18) #4, !dbg !559
  ret i32 %76, !dbg !560
}

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @construct_column(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32* nocapture readonly, double* nocapture, i32, i32* nocapture readonly, double* nocapture readonly, i32, i32* nocapture, i32* nocapture, double* nocapture) unnamed_addr #0 !dbg !561 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !565, metadata !DIExpression()), !dbg !586
  call void @llvm.dbg.value(metadata i32* %1, metadata !566, metadata !DIExpression()), !dbg !587
  call void @llvm.dbg.value(metadata i32* %2, metadata !567, metadata !DIExpression()), !dbg !588
  call void @llvm.dbg.value(metadata double* %3, metadata !568, metadata !DIExpression()), !dbg !589
  call void @llvm.dbg.value(metadata i32* %4, metadata !569, metadata !DIExpression()), !dbg !590
  call void @llvm.dbg.value(metadata double* %5, metadata !570, metadata !DIExpression()), !dbg !591
  call void @llvm.dbg.value(metadata i32 %6, metadata !571, metadata !DIExpression()), !dbg !592
  call void @llvm.dbg.value(metadata i32* %7, metadata !572, metadata !DIExpression()), !dbg !593
  call void @llvm.dbg.value(metadata double* %8, metadata !573, metadata !DIExpression()), !dbg !594
  call void @llvm.dbg.value(metadata i32 %9, metadata !574, metadata !DIExpression()), !dbg !595
  call void @llvm.dbg.value(metadata i32* %10, metadata !575, metadata !DIExpression()), !dbg !596
  call void @llvm.dbg.value(metadata i32* %11, metadata !576, metadata !DIExpression()), !dbg !597
  call void @llvm.dbg.value(metadata double* %12, metadata !577, metadata !DIExpression()), !dbg !598
  %14 = add nsw i32 %6, %0, !dbg !599
  call void @llvm.dbg.value(metadata i32 %14, metadata !583, metadata !DIExpression()), !dbg !600
  %15 = sext i32 %14 to i64, !dbg !601
  %16 = getelementptr inbounds i32, i32* %10, i64 %15, !dbg !601
  %17 = load i32, i32* %16, align 4, !dbg !601, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %17, metadata !584, metadata !DIExpression()), !dbg !602
  %18 = getelementptr inbounds i32, i32* %4, i64 %15, !dbg !603
  %19 = load i32, i32* %18, align 4, !dbg !603, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %19, metadata !582, metadata !DIExpression()), !dbg !604
  %20 = add nsw i32 %19, 1, !dbg !605
  %21 = sext i32 %20 to i64, !dbg !606
  %22 = getelementptr inbounds i32, i32* %1, i64 %21, !dbg !606
  %23 = load i32, i32* %22, align 4, !dbg !606, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %23, metadata !581, metadata !DIExpression()), !dbg !607
  %24 = icmp slt i32 %9, 1, !dbg !608
  br i1 %24, label %25, label %60, !dbg !610

; <label>:25:                                     ; preds = %13
  tail call void @ftrace(i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__FUNCTION__.construct_column, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 255) #4, !dbg !611
  %26 = sext i32 %19 to i64, !dbg !613
  %27 = getelementptr inbounds i32, i32* %1, i64 %26, !dbg !613
  %28 = load i32, i32* %27, align 4, !dbg !613, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %28, metadata !580, metadata !DIExpression()), !dbg !615
  call void @llvm.dbg.value(metadata i32 %17, metadata !584, metadata !DIExpression()), !dbg !602
  %29 = icmp slt i32 %28, %23, !dbg !616
  br i1 %29, label %30, label %95, !dbg !618

; <label>:30:                                     ; preds = %25
  %31 = sext i32 %28 to i64, !dbg !618
  %32 = sext i32 %23 to i64
  br label %33, !dbg !618

; <label>:33:                                     ; preds = %56, %30
  %34 = phi i64 [ %31, %30 ], [ %58, %56 ]
  %35 = phi i32 [ %17, %30 ], [ %57, %56 ]
  call void @llvm.dbg.value(metadata i32 %35, metadata !584, metadata !DIExpression()), !dbg !602
  call void @llvm.dbg.value(metadata i64 %34, metadata !580, metadata !DIExpression()), !dbg !615
  %36 = getelementptr inbounds i32, i32* %2, i64 %34, !dbg !619
  %37 = load i32, i32* %36, align 4, !dbg !619, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %37, metadata !585, metadata !DIExpression()), !dbg !621
  %38 = sext i32 %37 to i64, !dbg !622
  %39 = getelementptr inbounds i32, i32* %7, i64 %38, !dbg !622
  %40 = load i32, i32* %39, align 4, !dbg !622, !tbaa !146
  %41 = sub nsw i32 %40, %6, !dbg !623
  call void @llvm.dbg.value(metadata i32 %41, metadata !579, metadata !DIExpression()), !dbg !624
  %42 = getelementptr inbounds double, double* %3, i64 %34, !dbg !625
  %43 = bitcast double* %42 to i64*, !dbg !625
  %44 = load i64, i64* %43, align 8, !dbg !625, !tbaa !168
  call void @llvm.dbg.value(metadata double* %42, metadata !578, metadata !DIExpression(DW_OP_deref)), !dbg !626
  %45 = icmp slt i32 %41, 0, !dbg !627
  br i1 %45, label %46, label %52, !dbg !629

; <label>:46:                                     ; preds = %33
  %47 = sext i32 %35 to i64, !dbg !630
  %48 = getelementptr inbounds i32, i32* %11, i64 %47, !dbg !630
  store i32 %37, i32* %48, align 4, !dbg !632, !tbaa !146
  %49 = getelementptr inbounds double, double* %12, i64 %47, !dbg !633
  %50 = bitcast double* %49 to i64*, !dbg !634
  store i64 %44, i64* %50, align 8, !dbg !634, !tbaa !168
  %51 = add nsw i32 %35, 1, !dbg !635
  call void @llvm.dbg.value(metadata i32 %51, metadata !584, metadata !DIExpression()), !dbg !602
  br label %56, !dbg !636

; <label>:52:                                     ; preds = %33
  %53 = sext i32 %41 to i64, !dbg !637
  %54 = getelementptr inbounds double, double* %5, i64 %53, !dbg !637
  %55 = bitcast double* %54 to i64*, !dbg !639
  store i64 %44, i64* %55, align 8, !dbg !639, !tbaa !168
  br label %56

; <label>:56:                                     ; preds = %46, %52
  %57 = phi i32 [ %51, %46 ], [ %35, %52 ], !dbg !640
  %58 = add nsw i64 %34, 1, !dbg !641
  call void @llvm.dbg.value(metadata i32 %57, metadata !584, metadata !DIExpression()), !dbg !602
  call void @llvm.dbg.value(metadata i32 undef, metadata !580, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !615
  %59 = icmp eq i64 %58, %32, !dbg !616
  br i1 %59, label %95, label %33, !dbg !618, !llvm.loop !642

; <label>:60:                                     ; preds = %13
  tail call void @ftrace(i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__FUNCTION__.construct_column, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 279) #4, !dbg !644
  %61 = sext i32 %19 to i64, !dbg !646
  %62 = getelementptr inbounds i32, i32* %1, i64 %61, !dbg !646
  %63 = load i32, i32* %62, align 4, !dbg !646, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %63, metadata !580, metadata !DIExpression()), !dbg !615
  call void @llvm.dbg.value(metadata i32 %17, metadata !584, metadata !DIExpression()), !dbg !602
  %64 = icmp slt i32 %63, %23, !dbg !648
  br i1 %64, label %65, label %95, !dbg !650

; <label>:65:                                     ; preds = %60
  %66 = sext i32 %63 to i64, !dbg !650
  %67 = sext i32 %23 to i64
  br label %68, !dbg !650

; <label>:68:                                     ; preds = %91, %65
  %69 = phi i64 [ %66, %65 ], [ %93, %91 ]
  %70 = phi i32 [ %17, %65 ], [ %92, %91 ]
  call void @llvm.dbg.value(metadata i32 %70, metadata !584, metadata !DIExpression()), !dbg !602
  call void @llvm.dbg.value(metadata i64 %69, metadata !580, metadata !DIExpression()), !dbg !615
  %71 = getelementptr inbounds i32, i32* %2, i64 %69, !dbg !651
  %72 = load i32, i32* %71, align 4, !dbg !651, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %72, metadata !585, metadata !DIExpression()), !dbg !621
  %73 = sext i32 %72 to i64, !dbg !653
  %74 = getelementptr inbounds i32, i32* %7, i64 %73, !dbg !653
  %75 = load i32, i32* %74, align 4, !dbg !653, !tbaa !146
  %76 = sub nsw i32 %75, %6, !dbg !654
  call void @llvm.dbg.value(metadata i32 %76, metadata !579, metadata !DIExpression()), !dbg !624
  %77 = getelementptr inbounds double, double* %3, i64 %69, !dbg !655
  %78 = load double, double* %77, align 8, !dbg !655, !tbaa !168
  call void @llvm.dbg.value(metadata double %78, metadata !578, metadata !DIExpression()), !dbg !626
  %79 = getelementptr inbounds double, double* %8, i64 %73, !dbg !656
  %80 = load double, double* %79, align 8, !dbg !656, !tbaa !168
  %81 = fdiv double %78, %80, !dbg !656
  call void @llvm.dbg.value(metadata double %81, metadata !578, metadata !DIExpression()), !dbg !626
  %82 = icmp slt i32 %76, 0, !dbg !658
  br i1 %82, label %83, label %88, !dbg !660

; <label>:83:                                     ; preds = %68
  %84 = sext i32 %70 to i64, !dbg !661
  %85 = getelementptr inbounds i32, i32* %11, i64 %84, !dbg !661
  store i32 %72, i32* %85, align 4, !dbg !663, !tbaa !146
  %86 = getelementptr inbounds double, double* %12, i64 %84, !dbg !664
  store double %81, double* %86, align 8, !dbg !665, !tbaa !168
  %87 = add nsw i32 %70, 1, !dbg !666
  call void @llvm.dbg.value(metadata i32 %87, metadata !584, metadata !DIExpression()), !dbg !602
  br label %91, !dbg !667

; <label>:88:                                     ; preds = %68
  %89 = sext i32 %76 to i64, !dbg !668
  %90 = getelementptr inbounds double, double* %5, i64 %89, !dbg !668
  store double %81, double* %90, align 8, !dbg !670, !tbaa !168
  br label %91

; <label>:91:                                     ; preds = %83, %88
  %92 = phi i32 [ %87, %83 ], [ %70, %88 ], !dbg !640
  %93 = add nsw i64 %69, 1, !dbg !671
  call void @llvm.dbg.value(metadata i32 %92, metadata !584, metadata !DIExpression()), !dbg !602
  call void @llvm.dbg.value(metadata i32 undef, metadata !580, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !615
  %94 = icmp eq i64 %93, %67, !dbg !648
  br i1 %94, label %95, label %68, !dbg !650, !llvm.loop !672

; <label>:95:                                     ; preds = %91, %56, %60, %25
  %96 = phi i32 [ %17, %25 ], [ %17, %60 ], [ %57, %56 ], [ %92, %91 ], !dbg !674
  call void @llvm.dbg.value(metadata i32 %96, metadata !584, metadata !DIExpression()), !dbg !602
  tail call void @ftrace(i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__FUNCTION__.construct_column, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 303) #4, !dbg !675
  %97 = add nsw i32 %14, 1, !dbg !676
  %98 = sext i32 %97 to i64, !dbg !677
  %99 = getelementptr inbounds i32, i32* %10, i64 %98, !dbg !677
  store i32 %96, i32* %99, align 4, !dbg !678, !tbaa !146
  ret void, !dbg !679
}

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @lsolve_numeric(i32* nocapture readonly, double* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32, i32, i32* nocapture readonly, double* nocapture) unnamed_addr #0 !dbg !680 {
  call void @llvm.dbg.value(metadata i32* %0, metadata !684, metadata !DIExpression()), !dbg !705
  call void @llvm.dbg.value(metadata double* %1, metadata !685, metadata !DIExpression()), !dbg !706
  call void @llvm.dbg.value(metadata i32* %2, metadata !686, metadata !DIExpression()), !dbg !707
  call void @llvm.dbg.value(metadata i32* %3, metadata !687, metadata !DIExpression()), !dbg !708
  call void @llvm.dbg.value(metadata i32 %4, metadata !688, metadata !DIExpression()), !dbg !709
  call void @llvm.dbg.value(metadata i32 %5, metadata !689, metadata !DIExpression()), !dbg !710
  call void @llvm.dbg.value(metadata i32* %6, metadata !690, metadata !DIExpression()), !dbg !711
  call void @llvm.dbg.value(metadata double* %7, metadata !691, metadata !DIExpression()), !dbg !712
  call void @llvm.dbg.value(metadata i32 %4, metadata !696, metadata !DIExpression()), !dbg !713
  %9 = icmp slt i32 %4, %5, !dbg !714
  br i1 %9, label %10, label %54, !dbg !715

; <label>:10:                                     ; preds = %8
  %11 = sext i32 %4 to i64, !dbg !715
  br label %12, !dbg !715

; <label>:12:                                     ; preds = %50, %10
  %13 = phi i64 [ %11, %10 ], [ %51, %50 ]
  call void @llvm.dbg.value(metadata i64 %13, metadata !696, metadata !DIExpression()), !dbg !713
  tail call void @ftrace(i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__FUNCTION__.lsolve_numeric, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 344) #4, !dbg !716
  %14 = getelementptr inbounds i32, i32* %2, i64 %13, !dbg !717
  %15 = load i32, i32* %14, align 4, !dbg !717, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %15, metadata !697, metadata !DIExpression()), !dbg !718
  %16 = sext i32 %15 to i64, !dbg !719
  %17 = getelementptr inbounds i32, i32* %0, i64 %16, !dbg !719
  %18 = load i32, i32* %17, align 4, !dbg !719, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %18, metadata !698, metadata !DIExpression()), !dbg !720
  %19 = getelementptr inbounds double, double* %7, i64 %16, !dbg !721
  %20 = load double, double* %19, align 8, !dbg !721, !tbaa !168
  call void @llvm.dbg.value(metadata double %20, metadata !692, metadata !DIExpression()), !dbg !722
  %21 = sext i32 %18 to i64, !dbg !723
  %22 = getelementptr inbounds i32, i32* %3, i64 %21, !dbg !723
  %23 = load i32, i32* %22, align 4, !dbg !723, !tbaa !146
  %24 = sext i32 %23 to i64, !dbg !723
  %25 = getelementptr inbounds double, double* %1, i64 %24, !dbg !723
  call void @llvm.dbg.value(metadata double* %25, metadata !700, metadata !DIExpression()), !dbg !723
  %26 = getelementptr inbounds i32, i32* %6, i64 %21, !dbg !723
  %27 = load i32, i32* %26, align 4, !dbg !723, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %27, metadata !699, metadata !DIExpression()), !dbg !724
  %28 = bitcast double* %25 to i32*, !dbg !723
  call void @llvm.dbg.value(metadata i32* %28, metadata !694, metadata !DIExpression()), !dbg !725
  %29 = sext i32 %27 to i64, !dbg !723
  %30 = shl nsw i64 %29, 2, !dbg !723
  %31 = add nsw i64 %30, 7, !dbg !723
  %32 = lshr i64 %31, 3, !dbg !723
  %33 = getelementptr inbounds double, double* %25, i64 %32, !dbg !723
  call void @llvm.dbg.value(metadata double* %33, metadata !693, metadata !DIExpression()), !dbg !726
  call void @llvm.dbg.value(metadata i32 0, metadata !695, metadata !DIExpression()), !dbg !727
  %34 = icmp sgt i32 %27, 0, !dbg !728
  br i1 %34, label %35, label %50, !dbg !731

; <label>:35:                                     ; preds = %12
  %36 = zext i32 %27 to i64
  br label %37, !dbg !731

; <label>:37:                                     ; preds = %37, %35
  %38 = phi i64 [ 0, %35 ], [ %48, %37 ]
  call void @llvm.dbg.value(metadata i64 %38, metadata !695, metadata !DIExpression()), !dbg !727
  %39 = getelementptr inbounds double, double* %33, i64 %38, !dbg !732
  %40 = load double, double* %39, align 8, !dbg !732, !tbaa !168
  %41 = fmul double %20, %40, !dbg !732
  %42 = getelementptr inbounds i32, i32* %28, i64 %38, !dbg !732
  %43 = load i32, i32* %42, align 4, !dbg !732, !tbaa !146
  %44 = sext i32 %43 to i64, !dbg !732
  %45 = getelementptr inbounds double, double* %7, i64 %44, !dbg !732
  %46 = load double, double* %45, align 8, !dbg !732, !tbaa !168
  %47 = fsub double %46, %41, !dbg !732
  store double %47, double* %45, align 8, !dbg !732, !tbaa !168
  %48 = add nuw nsw i64 %38, 1, !dbg !735
  call void @llvm.dbg.value(metadata i32 undef, metadata !695, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !727
  %49 = icmp eq i64 %48, %36, !dbg !728
  br i1 %49, label %50, label %37, !dbg !731, !llvm.loop !736

; <label>:50:                                     ; preds = %37, %12
  %51 = add nsw i64 %13, 1, !dbg !738
  call void @llvm.dbg.value(metadata i32 undef, metadata !696, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !713
  %52 = trunc i64 %51 to i32, !dbg !714
  %53 = icmp eq i32 %52, %5, !dbg !714
  br i1 %53, label %54, label %12, !dbg !715, !llvm.loop !739

; <label>:54:                                     ; preds = %50, %8
  ret void, !dbg !741
}

; Function Attrs: nounwind ssp uwtable
define internal fastcc i32 @lpivot(i32, i32* nocapture, double* nocapture, double* nocapture, double, double* nocapture, double* nocapture, i32* nocapture readonly, i32* nocapture, i32, i32, i32* nocapture readonly, i32* nocapture, %struct.klu_common_struct* nocapture readonly) unnamed_addr #0 !dbg !742 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !746, metadata !DIExpression()), !dbg !778
  call void @llvm.dbg.value(metadata i32* %1, metadata !747, metadata !DIExpression()), !dbg !779
  call void @llvm.dbg.value(metadata double* %2, metadata !748, metadata !DIExpression()), !dbg !780
  call void @llvm.dbg.value(metadata double* %3, metadata !749, metadata !DIExpression()), !dbg !781
  call void @llvm.dbg.value(metadata double %4, metadata !750, metadata !DIExpression()), !dbg !782
  call void @llvm.dbg.value(metadata double* %5, metadata !751, metadata !DIExpression()), !dbg !783
  call void @llvm.dbg.value(metadata double* %6, metadata !752, metadata !DIExpression()), !dbg !784
  call void @llvm.dbg.value(metadata i32* %7, metadata !753, metadata !DIExpression()), !dbg !785
  call void @llvm.dbg.value(metadata i32* %8, metadata !754, metadata !DIExpression()), !dbg !786
  call void @llvm.dbg.value(metadata i32 %9, metadata !755, metadata !DIExpression()), !dbg !787
  call void @llvm.dbg.value(metadata i32 %10, metadata !756, metadata !DIExpression()), !dbg !788
  call void @llvm.dbg.value(metadata i32* %11, metadata !757, metadata !DIExpression()), !dbg !789
  call void @llvm.dbg.value(metadata i32* %12, metadata !758, metadata !DIExpression()), !dbg !790
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %13, metadata !759, metadata !DIExpression()), !dbg !791
  call void @llvm.dbg.value(metadata i32 -1, metadata !769, metadata !DIExpression()), !dbg !792
  %15 = sext i32 %9 to i64, !dbg !793
  %16 = getelementptr inbounds i32, i32* %8, i64 %15, !dbg !793
  %17 = load i32, i32* %16, align 4, !dbg !793, !tbaa !146
  %18 = icmp eq i32 %17, 0, !dbg !795
  br i1 %18, label %19, label %54, !dbg !796

; <label>:19:                                     ; preds = %14
  %20 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %13, i64 0, i32 10, !dbg !797
  %21 = load i32, i32* %20, align 8, !dbg !797, !tbaa !320
  %22 = icmp eq i32 %21, 0, !dbg !800
  br i1 %22, label %23, label %161, !dbg !801

; <label>:23:                                     ; preds = %19
  %24 = load i32, i32* %12, align 4, !dbg !802, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %24, metadata !772, metadata !DIExpression()), !dbg !804
  %25 = icmp slt i32 %24, %10, !dbg !805
  br i1 %25, label %26, label %51, !dbg !807

; <label>:26:                                     ; preds = %23
  %27 = sext i32 %24 to i64, !dbg !807
  %28 = sext i32 %10 to i64, !dbg !807
  br label %29, !dbg !807

; <label>:29:                                     ; preds = %26, %46
  %30 = phi i64 [ %27, %26 ], [ %47, %46 ]
  call void @llvm.dbg.value(metadata i64 %30, metadata !772, metadata !DIExpression()), !dbg !804
  %31 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !808, !tbaa !172
  %32 = icmp eq i32 (i8*, ...)* %31, null, !dbg !808
  br i1 %32, label %36, label %33, !dbg !812

; <label>:33:                                     ; preds = %29
  %34 = trunc i64 %30 to i32, !dbg !813
  %35 = tail call i32 (i8*, ...) %31(i8* getelementptr inbounds ([10 x i8], [10 x i8]* @.str.15, i64 0, i64 0), i32 %34) #4, !dbg !813
  br label %36, !dbg !813

; <label>:36:                                     ; preds = %29, %33
  %37 = getelementptr inbounds i32, i32* %11, i64 %30, !dbg !815
  %38 = load i32, i32* %37, align 4, !dbg !815, !tbaa !146
  %39 = icmp slt i32 %38, 0, !dbg !817
  br i1 %39, label %40, label %46, !dbg !818

; <label>:40:                                     ; preds = %36
  call void @llvm.dbg.value(metadata i64 %30, metadata !772, metadata !DIExpression()), !dbg !804
  call void @llvm.dbg.value(metadata i64 %30, metadata !772, metadata !DIExpression()), !dbg !804
  call void @llvm.dbg.value(metadata i64 %30, metadata !772, metadata !DIExpression()), !dbg !804
  %41 = trunc i64 %30 to i32, !dbg !804
  call void @llvm.dbg.value(metadata i32 %41, metadata !772, metadata !DIExpression()), !dbg !804
  call void @llvm.dbg.value(metadata i64 %30, metadata !772, metadata !DIExpression()), !dbg !804
  call void @llvm.dbg.value(metadata i32 undef, metadata !769, metadata !DIExpression()), !dbg !792
  %42 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !819, !tbaa !172
  %43 = icmp eq i32 (i8*, ...)* %42, null, !dbg !819
  br i1 %43, label %51, label %44, !dbg !823

; <label>:44:                                     ; preds = %40
  %45 = tail call i32 (i8*, ...) %42(i8* getelementptr inbounds ([21 x i8], [21 x i8]* @.str.16, i64 0, i64 0), i32 %41) #4, !dbg !824
  br label %51, !dbg !824

; <label>:46:                                     ; preds = %36
  %47 = add nsw i64 %30, 1, !dbg !826
  call void @llvm.dbg.value(metadata i32 undef, metadata !772, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !804
  %48 = icmp slt i64 %47, %28, !dbg !805
  br i1 %48, label %29, label %49, !dbg !807, !llvm.loop !827

; <label>:49:                                     ; preds = %46
  %50 = trunc i64 %47 to i32, !dbg !829
  br label %51, !dbg !829

; <label>:51:                                     ; preds = %49, %23, %40, %44
  %52 = phi i32 [ %41, %44 ], [ %41, %40 ], [ %24, %23 ], [ %50, %49 ]
  %53 = phi i32 [ %41, %44 ], [ %41, %40 ], [ -1, %23 ], [ -1, %49 ], !dbg !830
  call void @llvm.dbg.value(metadata i32 %53, metadata !769, metadata !DIExpression()), !dbg !792
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !761, metadata !DIExpression()), !dbg !831
  store i32 %53, i32* %1, align 4, !dbg !829, !tbaa !146
  store double 0.000000e+00, double* %2, align 8, !dbg !832, !tbaa !168
  store double 0.000000e+00, double* %3, align 8, !dbg !833, !tbaa !168
  store i32 %52, i32* %12, align 4, !dbg !834, !tbaa !146
  br label %161, !dbg !835

; <label>:54:                                     ; preds = %14
  call void @llvm.dbg.value(metadata i32 -1, metadata !768, metadata !DIExpression()), !dbg !836
  call void @llvm.dbg.value(metadata i32 -1, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !763, metadata !DIExpression()), !dbg !838
  %55 = add nsw i32 %17, -1, !dbg !839
  call void @llvm.dbg.value(metadata i32 %55, metadata !766, metadata !DIExpression()), !dbg !840
  %56 = getelementptr inbounds i32, i32* %7, i64 %15, !dbg !841
  %57 = load i32, i32* %56, align 4, !dbg !841, !tbaa !146
  %58 = sext i32 %57 to i64, !dbg !841
  %59 = getelementptr inbounds double, double* %6, i64 %58, !dbg !841
  call void @llvm.dbg.value(metadata double* %59, metadata !774, metadata !DIExpression()), !dbg !841
  call void @llvm.dbg.value(metadata i32 %17, metadata !773, metadata !DIExpression()), !dbg !842
  %60 = bitcast double* %59 to i32*, !dbg !841
  call void @llvm.dbg.value(metadata i32* %60, metadata !770, metadata !DIExpression()), !dbg !843
  %61 = sext i32 %55 to i64, !dbg !844
  %62 = getelementptr inbounds i32, i32* %60, i64 %61, !dbg !844
  %63 = load i32, i32* %62, align 4, !dbg !844, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %63, metadata !771, metadata !DIExpression()), !dbg !845
  store i32 %55, i32* %16, align 4, !dbg !846, !tbaa !146
  %64 = load i32, i32* %56, align 4, !dbg !847, !tbaa !146
  %65 = sext i32 %64 to i64, !dbg !847
  %66 = getelementptr inbounds double, double* %6, i64 %65, !dbg !847
  call void @llvm.dbg.value(metadata double* %66, metadata !776, metadata !DIExpression()), !dbg !847
  call void @llvm.dbg.value(metadata i32 %55, metadata !773, metadata !DIExpression()), !dbg !842
  %67 = bitcast double* %66 to i32*, !dbg !847
  call void @llvm.dbg.value(metadata i32* %67, metadata !770, metadata !DIExpression()), !dbg !843
  %68 = shl nsw i64 %61, 2, !dbg !847
  %69 = add nsw i64 %68, 7, !dbg !847
  %70 = lshr i64 %69, 3, !dbg !847
  %71 = getelementptr inbounds double, double* %66, i64 %70, !dbg !847
  call void @llvm.dbg.value(metadata double* %71, metadata !762, metadata !DIExpression()), !dbg !848
  call void @llvm.dbg.value(metadata i32 0, metadata !765, metadata !DIExpression()), !dbg !849
  call void @llvm.dbg.value(metadata i32 -1, metadata !768, metadata !DIExpression()), !dbg !836
  call void @llvm.dbg.value(metadata i32 -1, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !763, metadata !DIExpression()), !dbg !838
  %72 = icmp sgt i32 %17, 1, !dbg !850
  br i1 %72, label %73, label %98, !dbg !853

; <label>:73:                                     ; preds = %54
  %74 = zext i32 %55 to i64
  br label %75, !dbg !853

; <label>:75:                                     ; preds = %75, %73
  %76 = phi i64 [ 0, %73 ], [ %96, %75 ]
  %77 = phi i32 [ -1, %73 ], [ %91, %75 ]
  %78 = phi i32 [ -1, %73 ], [ %95, %75 ]
  %79 = phi double [ -1.000000e+00, %73 ], [ %93, %75 ]
  call void @llvm.dbg.value(metadata i32 %77, metadata !768, metadata !DIExpression()), !dbg !836
  call void @llvm.dbg.value(metadata i32 %78, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata i64 %76, metadata !765, metadata !DIExpression()), !dbg !849
  call void @llvm.dbg.value(metadata double %79, metadata !763, metadata !DIExpression()), !dbg !838
  %80 = getelementptr inbounds i32, i32* %67, i64 %76, !dbg !854
  %81 = load i32, i32* %80, align 4, !dbg !854, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %81, metadata !766, metadata !DIExpression()), !dbg !840
  %82 = sext i32 %81 to i64, !dbg !856
  %83 = getelementptr inbounds double, double* %5, i64 %82, !dbg !856
  %84 = load double, double* %83, align 8, !dbg !856, !tbaa !168
  call void @llvm.dbg.value(metadata double %84, metadata !760, metadata !DIExpression()), !dbg !857
  store double 0.000000e+00, double* %83, align 8, !dbg !858, !tbaa !168
  %85 = getelementptr inbounds double, double* %71, i64 %76, !dbg !860
  store double %84, double* %85, align 8, !dbg !861, !tbaa !168
  %86 = fcmp olt double %84, 0.000000e+00, !dbg !862
  %87 = fsub double -0.000000e+00, %84, !dbg !862
  %88 = select i1 %86, double %87, double %84, !dbg !862
  call void @llvm.dbg.value(metadata double %88, metadata !764, metadata !DIExpression()), !dbg !864
  %89 = icmp eq i32 %81, %0, !dbg !865
  %90 = trunc i64 %76 to i32, !dbg !867
  %91 = select i1 %89, i32 %90, i32 %77, !dbg !867
  %92 = fcmp ogt double %88, %79, !dbg !868
  call void @llvm.dbg.value(metadata double %88, metadata !763, metadata !DIExpression()), !dbg !838
  call void @llvm.dbg.value(metadata i64 %76, metadata !767, metadata !DIExpression()), !dbg !837
  %93 = select i1 %92, double %88, double %79, !dbg !870
  %94 = trunc i64 %76 to i32, !dbg !870
  %95 = select i1 %92, i32 %94, i32 %78, !dbg !870
  %96 = add nuw nsw i64 %76, 1, !dbg !871
  call void @llvm.dbg.value(metadata i32 %91, metadata !768, metadata !DIExpression()), !dbg !836
  call void @llvm.dbg.value(metadata i32 %95, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata i32 undef, metadata !765, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !849
  call void @llvm.dbg.value(metadata double %93, metadata !763, metadata !DIExpression()), !dbg !838
  %97 = icmp eq i64 %96, %74, !dbg !850
  br i1 %97, label %98, label %75, !dbg !853, !llvm.loop !872

; <label>:98:                                     ; preds = %75, %54
  %99 = phi double [ -1.000000e+00, %54 ], [ %93, %75 ]
  %100 = phi i32 [ -1, %54 ], [ %95, %75 ]
  %101 = phi i32 [ -1, %54 ], [ %91, %75 ]
  call void @llvm.dbg.value(metadata double %99, metadata !763, metadata !DIExpression()), !dbg !838
  call void @llvm.dbg.value(metadata i32 %100, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata i32 %101, metadata !768, metadata !DIExpression()), !dbg !836
  %102 = sext i32 %63 to i64, !dbg !874
  %103 = getelementptr inbounds double, double* %5, i64 %102, !dbg !874
  %104 = load double, double* %103, align 8, !dbg !874, !tbaa !168
  %105 = fcmp olt double %104, 0.000000e+00, !dbg !874
  %106 = fsub double -0.000000e+00, %104, !dbg !874
  %107 = select i1 %105, double %106, double %104, !dbg !874
  call void @llvm.dbg.value(metadata double %107, metadata !764, metadata !DIExpression()), !dbg !864
  %108 = fcmp ogt double %107, %99, !dbg !876
  call void @llvm.dbg.value(metadata double %107, metadata !763, metadata !DIExpression()), !dbg !838
  call void @llvm.dbg.value(metadata i32 -1, metadata !767, metadata !DIExpression()), !dbg !837
  %109 = select i1 %108, double %107, double %99, !dbg !878
  %110 = select i1 %108, i32 -1, i32 %100, !dbg !878
  call void @llvm.dbg.value(metadata i32 %110, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata double %109, metadata !763, metadata !DIExpression()), !dbg !838
  %111 = icmp eq i32 %63, %0, !dbg !879
  br i1 %111, label %112, label %115, !dbg !881

; <label>:112:                                    ; preds = %98
  %113 = fmul double %109, %4, !dbg !882
  %114 = fcmp ult double %107, %113, !dbg !885
  br i1 %114, label %126, label %139, !dbg !886

; <label>:115:                                    ; preds = %98
  %116 = icmp eq i32 %101, -1, !dbg !887
  br i1 %116, label %126, label %117, !dbg !889

; <label>:117:                                    ; preds = %115
  %118 = sext i32 %101 to i64, !dbg !890
  %119 = getelementptr inbounds double, double* %71, i64 %118, !dbg !890
  %120 = load double, double* %119, align 8, !dbg !890, !tbaa !168
  %121 = fcmp olt double %120, 0.000000e+00, !dbg !890
  %122 = fsub double -0.000000e+00, %120, !dbg !890
  %123 = select i1 %121, double %122, double %120, !dbg !890
  call void @llvm.dbg.value(metadata double %123, metadata !764, metadata !DIExpression()), !dbg !864
  %124 = fmul double %109, %4, !dbg !893
  %125 = fcmp ult double %123, %124, !dbg !895
  br i1 %125, label %126, label %128, !dbg !896

; <label>:126:                                    ; preds = %112, %117, %115
  call void @llvm.dbg.value(metadata i32 %110, metadata !767, metadata !DIExpression()), !dbg !837
  call void @llvm.dbg.value(metadata double %109, metadata !763, metadata !DIExpression()), !dbg !838
  %127 = icmp eq i32 %110, -1, !dbg !897
  br i1 %127, label %139, label %128, !dbg !899

; <label>:128:                                    ; preds = %117, %126
  %129 = phi i32 [ %110, %126 ], [ %101, %117 ]
  %130 = phi double [ %109, %126 ], [ %123, %117 ]
  %131 = sext i32 %129 to i64, !dbg !900
  %132 = getelementptr inbounds i32, i32* %67, i64 %131, !dbg !900
  %133 = load i32, i32* %132, align 4, !dbg !900, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %133, metadata !769, metadata !DIExpression()), !dbg !792
  %134 = getelementptr inbounds double, double* %71, i64 %131, !dbg !902
  %135 = load double, double* %134, align 8, !dbg !902, !tbaa !168
  call void @llvm.dbg.value(metadata double %135, metadata !761, metadata !DIExpression()), !dbg !831
  store i32 %63, i32* %132, align 4, !dbg !903, !tbaa !146
  %136 = bitcast double* %103 to i64*, !dbg !904
  %137 = load i64, i64* %136, align 8, !dbg !904, !tbaa !168
  %138 = bitcast double* %134 to i64*, !dbg !905
  store i64 %137, i64* %138, align 8, !dbg !905, !tbaa !168
  br label %139, !dbg !906

; <label>:139:                                    ; preds = %112, %126, %128
  %140 = phi double [ %130, %128 ], [ %109, %126 ], [ %107, %112 ]
  %141 = phi i32 [ %133, %128 ], [ %63, %126 ], [ %63, %112 ], !dbg !907
  %142 = phi double [ %135, %128 ], [ %104, %126 ], [ %104, %112 ], !dbg !907
  call void @llvm.dbg.value(metadata double %142, metadata !761, metadata !DIExpression()), !dbg !831
  call void @llvm.dbg.value(metadata i32 %141, metadata !769, metadata !DIExpression()), !dbg !792
  store double 0.000000e+00, double* %103, align 8, !dbg !909, !tbaa !168
  store i32 %141, i32* %1, align 4, !dbg !911, !tbaa !146
  store double %142, double* %2, align 8, !dbg !912, !tbaa !168
  store double %140, double* %3, align 8, !dbg !913, !tbaa !168
  %143 = fcmp oeq double %142, 0.000000e+00, !dbg !914
  br i1 %143, label %144, label %148, !dbg !916

; <label>:144:                                    ; preds = %139
  %145 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %13, i64 0, i32 10, !dbg !917
  %146 = load i32, i32* %145, align 8, !dbg !917, !tbaa !320
  %147 = icmp eq i32 %146, 0, !dbg !918
  br i1 %147, label %148, label %161, !dbg !919

; <label>:148:                                    ; preds = %144, %139
  call void @llvm.dbg.value(metadata i32 0, metadata !765, metadata !DIExpression()), !dbg !849
  %149 = load i32, i32* %16, align 4, !dbg !920, !tbaa !146
  %150 = icmp sgt i32 %149, 0, !dbg !923
  br i1 %150, label %151, label %161, !dbg !924

; <label>:151:                                    ; preds = %148
  %152 = load i32, i32* %16, align 4, !tbaa !146
  %153 = sext i32 %152 to i64, !dbg !924
  br label %154, !dbg !924

; <label>:154:                                    ; preds = %151, %154
  %155 = phi i64 [ 0, %151 ], [ %159, %154 ]
  call void @llvm.dbg.value(metadata i64 %155, metadata !765, metadata !DIExpression()), !dbg !849
  %156 = getelementptr inbounds double, double* %71, i64 %155, !dbg !925
  %157 = load double, double* %156, align 8, !dbg !925, !tbaa !168
  %158 = fdiv double %157, %142, !dbg !925
  store double %158, double* %156, align 8, !dbg !925, !tbaa !168
  %159 = add nuw nsw i64 %155, 1, !dbg !928
  call void @llvm.dbg.value(metadata i32 undef, metadata !765, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !849
  %160 = icmp slt i64 %159, %153, !dbg !923
  br i1 %160, label %154, label %161, !dbg !924, !llvm.loop !929

; <label>:161:                                    ; preds = %154, %148, %144, %19, %51
  %162 = phi i32 [ 0, %51 ], [ 0, %19 ], [ 0, %144 ], [ 1, %148 ], [ 1, %154 ], !dbg !830
  ret i32 %162, !dbg !931
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @prune(i32* nocapture, i32* nocapture readonly, i32, i32, double* nocapture, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly) unnamed_addr #0 !dbg !932 {
  call void @llvm.dbg.value(metadata i32* %0, metadata !936, metadata !DIExpression()), !dbg !967
  call void @llvm.dbg.value(metadata i32* %1, metadata !937, metadata !DIExpression()), !dbg !968
  call void @llvm.dbg.value(metadata i32 %2, metadata !938, metadata !DIExpression()), !dbg !969
  call void @llvm.dbg.value(metadata i32 %3, metadata !939, metadata !DIExpression()), !dbg !970
  call void @llvm.dbg.value(metadata double* %4, metadata !940, metadata !DIExpression()), !dbg !971
  call void @llvm.dbg.value(metadata i32* %5, metadata !941, metadata !DIExpression()), !dbg !972
  call void @llvm.dbg.value(metadata i32* %6, metadata !942, metadata !DIExpression()), !dbg !973
  call void @llvm.dbg.value(metadata i32* %7, metadata !943, metadata !DIExpression()), !dbg !974
  call void @llvm.dbg.value(metadata i32* %8, metadata !944, metadata !DIExpression()), !dbg !975
  %10 = sext i32 %2 to i64, !dbg !976
  %11 = getelementptr inbounds i32, i32* %5, i64 %10, !dbg !976
  %12 = load i32, i32* %11, align 4, !dbg !976, !tbaa !146
  %13 = sext i32 %12 to i64, !dbg !976
  %14 = getelementptr inbounds double, double* %4, i64 %13, !dbg !976
  call void @llvm.dbg.value(metadata double* %14, metadata !958, metadata !DIExpression()), !dbg !976
  %15 = getelementptr inbounds i32, i32* %7, i64 %10, !dbg !976
  %16 = load i32, i32* %15, align 4, !dbg !976, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %16, metadata !957, metadata !DIExpression()), !dbg !977
  %17 = bitcast double* %14 to i32*, !dbg !976
  call void @llvm.dbg.value(metadata i32* %17, metadata !949, metadata !DIExpression()), !dbg !978
  call void @llvm.dbg.value(metadata i32 0, metadata !950, metadata !DIExpression()), !dbg !979
  %18 = icmp sgt i32 %16, 0, !dbg !980
  br i1 %18, label %19, label %102, !dbg !981

; <label>:19:                                     ; preds = %9
  %20 = zext i32 %16 to i64
  br label %21, !dbg !981

; <label>:21:                                     ; preds = %99, %19
  %22 = phi i64 [ 0, %19 ], [ %100, %99 ]
  call void @llvm.dbg.value(metadata i64 %22, metadata !950, metadata !DIExpression()), !dbg !979
  %23 = getelementptr inbounds i32, i32* %17, i64 %22, !dbg !982
  %24 = load i32, i32* %23, align 4, !dbg !982, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %24, metadata !952, metadata !DIExpression()), !dbg !983
  %25 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !984, !tbaa !172
  %26 = icmp eq i32 (i8*, ...)* %25, null, !dbg !984
  br i1 %26, label %38, label %27, !dbg !987

; <label>:27:                                     ; preds = %21
  %28 = sext i32 %24 to i64, !dbg !988
  %29 = getelementptr inbounds i32, i32* %0, i64 %28, !dbg !988
  %30 = load i32, i32* %29, align 4, !dbg !988, !tbaa !146
  %31 = icmp ne i32 %30, -1, !dbg !988
  %32 = zext i1 %31 to i32, !dbg !988
  %33 = add nsw i32 %24, 1, !dbg !988
  %34 = sext i32 %33 to i64, !dbg !988
  %35 = getelementptr inbounds i32, i32* %6, i64 %34, !dbg !988
  %36 = load i32, i32* %35, align 4, !dbg !988, !tbaa !146
  %37 = tail call i32 (i8*, ...) %25(i8* getelementptr inbounds ([43 x i8], [43 x i8]* @.str.17, i64 0, i64 0), i32 %24, i32 %32, i32 %30, i32 %36) #4, !dbg !988
  br label %38, !dbg !988

; <label>:38:                                     ; preds = %21, %27
  %39 = sext i32 %24 to i64, !dbg !990
  %40 = getelementptr inbounds i32, i32* %0, i64 %39, !dbg !990
  %41 = load i32, i32* %40, align 4, !dbg !990, !tbaa !146
  %42 = icmp eq i32 %41, -1, !dbg !991
  br i1 %42, label %43, label %99, !dbg !992

; <label>:43:                                     ; preds = %38
  %44 = getelementptr inbounds i32, i32* %6, i64 %39, !dbg !993
  %45 = load i32, i32* %44, align 4, !dbg !993, !tbaa !146
  %46 = sext i32 %45 to i64, !dbg !993
  %47 = getelementptr inbounds double, double* %4, i64 %46, !dbg !993
  call void @llvm.dbg.value(metadata double* %47, metadata !960, metadata !DIExpression()), !dbg !993
  %48 = getelementptr inbounds i32, i32* %8, i64 %39, !dbg !993
  %49 = load i32, i32* %48, align 4, !dbg !993, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %49, metadata !956, metadata !DIExpression()), !dbg !994
  %50 = bitcast double* %47 to i32*, !dbg !993
  call void @llvm.dbg.value(metadata i32* %50, metadata !948, metadata !DIExpression()), !dbg !995
  %51 = sext i32 %49 to i64, !dbg !993
  %52 = shl nsw i64 %51, 2, !dbg !993
  %53 = add nsw i64 %52, 7, !dbg !993
  %54 = lshr i64 %53, 3, !dbg !993
  %55 = getelementptr inbounds double, double* %47, i64 %54, !dbg !993
  call void @llvm.dbg.value(metadata double* %55, metadata !946, metadata !DIExpression()), !dbg !996
  call void @llvm.dbg.value(metadata i32 0, metadata !953, metadata !DIExpression()), !dbg !997
  %56 = icmp sgt i32 %49, 0, !dbg !998
  br i1 %56, label %57, label %99, !dbg !1001

; <label>:57:                                     ; preds = %43
  %58 = sext i32 %49 to i64, !dbg !1001
  br label %61, !dbg !1001

; <label>:59:                                     ; preds = %61
  call void @llvm.dbg.value(metadata i32 undef, metadata !953, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !997
  %60 = icmp slt i64 %66, %58, !dbg !998
  br i1 %60, label %61, label %99, !dbg !1001, !llvm.loop !1002

; <label>:61:                                     ; preds = %57, %59
  %62 = phi i64 [ 0, %57 ], [ %66, %59 ]
  call void @llvm.dbg.value(metadata i64 %62, metadata !953, metadata !DIExpression()), !dbg !997
  %63 = getelementptr inbounds i32, i32* %50, i64 %62, !dbg !1004
  %64 = load i32, i32* %63, align 4, !dbg !1004, !tbaa !146
  %65 = icmp eq i32 %64, %3, !dbg !1007
  %66 = add nuw nsw i64 %62, 1, !dbg !1008
  call void @llvm.dbg.value(metadata i32 undef, metadata !953, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !997
  br i1 %65, label %67, label %59, !dbg !1009

; <label>:67:                                     ; preds = %61
  call void @llvm.dbg.value(metadata i32 0, metadata !954, metadata !DIExpression()), !dbg !1010
  call void @llvm.dbg.value(metadata i32 %49, metadata !955, metadata !DIExpression()), !dbg !1011
  %68 = icmp sgt i32 %49, 0, !dbg !1012
  br i1 %68, label %69, label %97, !dbg !1014

; <label>:69:                                     ; preds = %67
  br label %70, !dbg !1015

; <label>:70:                                     ; preds = %69, %93
  %71 = phi i32 [ %95, %93 ], [ 0, %69 ]
  %72 = phi i32 [ %94, %93 ], [ %49, %69 ]
  call void @llvm.dbg.value(metadata i32 %71, metadata !954, metadata !DIExpression()), !dbg !1010
  call void @llvm.dbg.value(metadata i32 %72, metadata !955, metadata !DIExpression()), !dbg !1011
  %73 = sext i32 %71 to i64, !dbg !1015
  %74 = getelementptr inbounds i32, i32* %50, i64 %73, !dbg !1015
  %75 = load i32, i32* %74, align 4, !dbg !1015, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %75, metadata !951, metadata !DIExpression()), !dbg !1017
  %76 = sext i32 %75 to i64, !dbg !1018
  %77 = getelementptr inbounds i32, i32* %1, i64 %76, !dbg !1018
  %78 = load i32, i32* %77, align 4, !dbg !1018, !tbaa !146
  %79 = icmp sgt i32 %78, -1, !dbg !1020
  br i1 %79, label %80, label %82, !dbg !1021

; <label>:80:                                     ; preds = %70
  %81 = add nsw i32 %71, 1, !dbg !1022
  call void @llvm.dbg.value(metadata i32 %81, metadata !954, metadata !DIExpression()), !dbg !1010
  br label %93, !dbg !1024

; <label>:82:                                     ; preds = %70
  %83 = add nsw i32 %72, -1, !dbg !1025
  call void @llvm.dbg.value(metadata i32 %83, metadata !955, metadata !DIExpression()), !dbg !1011
  %84 = sext i32 %83 to i64, !dbg !1027
  %85 = getelementptr inbounds i32, i32* %50, i64 %84, !dbg !1027
  %86 = load i32, i32* %85, align 4, !dbg !1027, !tbaa !146
  store i32 %86, i32* %74, align 4, !dbg !1028, !tbaa !146
  store i32 %75, i32* %85, align 4, !dbg !1029, !tbaa !146
  %87 = getelementptr inbounds double, double* %55, i64 %73, !dbg !1030
  %88 = bitcast double* %87 to i64*, !dbg !1030
  %89 = load i64, i64* %88, align 8, !dbg !1030, !tbaa !168
  call void @llvm.dbg.value(metadata double* %87, metadata !945, metadata !DIExpression(DW_OP_deref)), !dbg !1031
  %90 = getelementptr inbounds double, double* %55, i64 %84, !dbg !1032
  %91 = bitcast double* %90 to i64*, !dbg !1032
  %92 = load i64, i64* %91, align 8, !dbg !1032, !tbaa !168
  store i64 %92, i64* %88, align 8, !dbg !1033, !tbaa !168
  store i64 %89, i64* %91, align 8, !dbg !1034, !tbaa !168
  br label %93

; <label>:93:                                     ; preds = %82, %80
  %94 = phi i32 [ %72, %80 ], [ %83, %82 ], !dbg !1035
  %95 = phi i32 [ %81, %80 ], [ %71, %82 ], !dbg !1036
  call void @llvm.dbg.value(metadata i32 %95, metadata !954, metadata !DIExpression()), !dbg !1010
  call void @llvm.dbg.value(metadata i32 %94, metadata !955, metadata !DIExpression()), !dbg !1011
  %96 = icmp slt i32 %95, %94, !dbg !1012
  br i1 %96, label %70, label %97, !dbg !1014, !llvm.loop !1037

; <label>:97:                                     ; preds = %93, %67
  %98 = phi i32 [ %49, %67 ], [ %94, %93 ]
  call void @llvm.dbg.value(metadata i32 %98, metadata !955, metadata !DIExpression()), !dbg !1011
  store i32 %98, i32* %40, align 4, !dbg !1039, !tbaa !146
  br label %99, !dbg !1040

; <label>:99:                                     ; preds = %59, %43, %38, %97
  %100 = add nuw nsw i64 %22, 1, !dbg !1041
  call void @llvm.dbg.value(metadata i32 undef, metadata !950, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !979
  %101 = icmp eq i64 %100, %20, !dbg !980
  br i1 %101, label %102, label %21, !dbg !981, !llvm.loop !1042

; <label>:102:                                    ; preds = %99, %9
  ret void, !dbg !1044
}

; Function Attrs: nounwind ssp uwtable
define internal fastcc i32 @dfs(i32, i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture, i32* nocapture, i32* nocapture readonly, i32, double* nocapture readonly, i32* nocapture, i32* nocapture, i32* nocapture) unnamed_addr #0 !dbg !1045 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !1049, metadata !DIExpression()), !dbg !1068
  call void @llvm.dbg.value(metadata i32 %1, metadata !1050, metadata !DIExpression()), !dbg !1069
  call void @llvm.dbg.value(metadata i32* %2, metadata !1051, metadata !DIExpression()), !dbg !1070
  call void @llvm.dbg.value(metadata i32* %3, metadata !1052, metadata !DIExpression()), !dbg !1071
  call void @llvm.dbg.value(metadata i32* %4, metadata !1053, metadata !DIExpression()), !dbg !1072
  call void @llvm.dbg.value(metadata i32* %5, metadata !1054, metadata !DIExpression()), !dbg !1073
  call void @llvm.dbg.value(metadata i32* %6, metadata !1055, metadata !DIExpression()), !dbg !1074
  call void @llvm.dbg.value(metadata i32* %7, metadata !1056, metadata !DIExpression()), !dbg !1075
  call void @llvm.dbg.value(metadata i32 %8, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata double* %9, metadata !1058, metadata !DIExpression()), !dbg !1077
  call void @llvm.dbg.value(metadata i32* %10, metadata !1059, metadata !DIExpression()), !dbg !1078
  call void @llvm.dbg.value(metadata i32* %11, metadata !1060, metadata !DIExpression()), !dbg !1079
  call void @llvm.dbg.value(metadata i32* %12, metadata !1061, metadata !DIExpression()), !dbg !1080
  %14 = load i32, i32* %11, align 4, !dbg !1081, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %14, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 0, metadata !1065, metadata !DIExpression()), !dbg !1083
  store i32 %0, i32* %5, align 4, !dbg !1084, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %14, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 0, metadata !1065, metadata !DIExpression()), !dbg !1083
  call void @llvm.dbg.value(metadata i32 %8, metadata !1057, metadata !DIExpression()), !dbg !1076
  br label %15, !dbg !1085

; <label>:15:                                     ; preds = %13, %99
  %16 = phi i32 [ %14, %13 ], [ %100, %99 ]
  %17 = phi i32 [ 0, %13 ], [ %102, %99 ]
  %18 = phi i32 [ %8, %13 ], [ %101, %99 ]
  call void @llvm.dbg.value(metadata i32 %16, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %17, metadata !1065, metadata !DIExpression()), !dbg !1083
  call void @llvm.dbg.value(metadata i32 %18, metadata !1057, metadata !DIExpression()), !dbg !1076
  tail call void @ftrace(i8* getelementptr inbounds ([4 x i8], [4 x i8]* @__FUNCTION__.dfs, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 54) #4, !dbg !1086
  %19 = sext i32 %17 to i64, !dbg !1088
  %20 = getelementptr inbounds i32, i32* %5, i64 %19, !dbg !1088
  %21 = load i32, i32* %20, align 4, !dbg !1088, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %21, metadata !1049, metadata !DIExpression()), !dbg !1068
  %22 = sext i32 %21 to i64, !dbg !1089
  %23 = getelementptr inbounds i32, i32* %2, i64 %22, !dbg !1089
  %24 = load i32, i32* %23, align 4, !dbg !1089, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %24, metadata !1064, metadata !DIExpression()), !dbg !1090
  %25 = getelementptr inbounds i32, i32* %6, i64 %22, !dbg !1091
  %26 = load i32, i32* %25, align 4, !dbg !1091, !tbaa !146
  %27 = icmp eq i32 %26, %1, !dbg !1093
  br i1 %27, label %44, label %28, !dbg !1094

; <label>:28:                                     ; preds = %15
  store i32 %1, i32* %25, align 4, !dbg !1095, !tbaa !146
  %29 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !1097, !tbaa !172
  %30 = icmp eq i32 (i8*, ...)* %29, null, !dbg !1097
  br i1 %30, label %33, label %31, !dbg !1100

; <label>:31:                                     ; preds = %28
  %32 = tail call i32 (i8*, ...) %29(i8* getelementptr inbounds ([28 x i8], [28 x i8]* @.str.13, i64 0, i64 0), i32 %21, i32 %24) #4, !dbg !1101
  br label %33, !dbg !1101

; <label>:33:                                     ; preds = %28, %31
  %34 = sext i32 %24 to i64, !dbg !1103
  %35 = getelementptr inbounds i32, i32* %7, i64 %34, !dbg !1103
  %36 = load i32, i32* %35, align 4, !dbg !1103, !tbaa !146
  %37 = icmp eq i32 %36, -1, !dbg !1104
  br i1 %37, label %38, label %41, !dbg !1105

; <label>:38:                                     ; preds = %33
  %39 = getelementptr inbounds i32, i32* %3, i64 %34, !dbg !1106
  %40 = load i32, i32* %39, align 4, !dbg !1106, !tbaa !146
  br label %41, !dbg !1105

; <label>:41:                                     ; preds = %33, %38
  %42 = phi i32 [ %40, %38 ], [ %36, %33 ], !dbg !1105
  %43 = getelementptr inbounds i32, i32* %12, i64 %19, !dbg !1107
  store i32 %42, i32* %43, align 4, !dbg !1108, !tbaa !146
  br label %44, !dbg !1109

; <label>:44:                                     ; preds = %15, %41
  %45 = sext i32 %24 to i64, !dbg !1110
  %46 = getelementptr inbounds i32, i32* %4, i64 %45, !dbg !1110
  %47 = load i32, i32* %46, align 4, !dbg !1110, !tbaa !146
  %48 = sext i32 %47 to i64, !dbg !1111
  %49 = getelementptr inbounds double, double* %9, i64 %48, !dbg !1111
  %50 = bitcast double* %49 to i32*, !dbg !1112
  call void @llvm.dbg.value(metadata i32* %50, metadata !1067, metadata !DIExpression()), !dbg !1113
  %51 = getelementptr inbounds i32, i32* %12, i64 %19, !dbg !1114
  %52 = load i32, i32* %51, align 4, !dbg !1116, !tbaa !146
  %53 = add nsw i32 %52, -1, !dbg !1116
  store i32 %53, i32* %51, align 4, !dbg !1116, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %53, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %16, metadata !1066, metadata !DIExpression()), !dbg !1082
  %54 = icmp sgt i32 %52, 0, !dbg !1118
  br i1 %54, label %55, label %86, !dbg !1120

; <label>:55:                                     ; preds = %44
  %56 = sext i32 %52 to i64, !dbg !1120
  %57 = add nsw i64 %56, -1, !dbg !1120
  br label %58, !dbg !1120

; <label>:58:                                     ; preds = %55, %80
  %59 = phi i64 [ %57, %55 ], [ %82, %80 ]
  %60 = phi i32 [ %16, %55 ], [ %81, %80 ]
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i64 %59, metadata !1063, metadata !DIExpression()), !dbg !1117
  %61 = getelementptr inbounds i32, i32* %50, i64 %59, !dbg !1121
  %62 = load i32, i32* %61, align 4, !dbg !1121, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %62, metadata !1062, metadata !DIExpression()), !dbg !1123
  %63 = sext i32 %62 to i64, !dbg !1124
  %64 = getelementptr inbounds i32, i32* %6, i64 %63, !dbg !1124
  %65 = load i32, i32* %64, align 4, !dbg !1124, !tbaa !146
  %66 = icmp eq i32 %65, %1, !dbg !1126
  br i1 %66, label %80, label %67, !dbg !1127

; <label>:67:                                     ; preds = %58
  %68 = getelementptr inbounds i32, i32* %2, i64 %63, !dbg !1128
  %69 = load i32, i32* %68, align 4, !dbg !1128, !tbaa !146
  %70 = icmp sgt i32 %69, -1, !dbg !1131
  br i1 %70, label %71, label %76, !dbg !1132

; <label>:71:                                     ; preds = %67
  call void @llvm.dbg.value(metadata i64 %59, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i64 %59, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i64 %59, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  %72 = trunc i64 %59 to i32, !dbg !1082
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %72, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i64 %59, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %60, metadata !1066, metadata !DIExpression()), !dbg !1082
  store i32 %72, i32* %51, align 4, !dbg !1133, !tbaa !146
  %73 = add nsw i32 %17, 1, !dbg !1135
  call void @llvm.dbg.value(metadata i32 %73, metadata !1065, metadata !DIExpression()), !dbg !1083
  %74 = sext i32 %73 to i64, !dbg !1136
  %75 = getelementptr inbounds i32, i32* %5, i64 %74, !dbg !1136
  store i32 %62, i32* %75, align 4, !dbg !1137, !tbaa !146
  call void @llvm.dbg.value(metadata i32 %17, metadata !1065, metadata !DIExpression()), !dbg !1083
  br label %99, !dbg !1138

; <label>:76:                                     ; preds = %67
  store i32 %1, i32* %64, align 4, !dbg !1139, !tbaa !146
  %77 = sext i32 %60 to i64, !dbg !1141
  %78 = getelementptr inbounds i32, i32* %10, i64 %77, !dbg !1141
  store i32 %62, i32* %78, align 4, !dbg !1142, !tbaa !146
  %79 = add nsw i32 %60, 1, !dbg !1143
  call void @llvm.dbg.value(metadata i32 %79, metadata !1066, metadata !DIExpression()), !dbg !1082
  br label %80, !dbg !1144

; <label>:80:                                     ; preds = %58, %76
  %81 = phi i32 [ %79, %76 ], [ %60, %58 ], !dbg !1145
  %82 = add i64 %59, -1, !dbg !1146
  call void @llvm.dbg.value(metadata i32 %81, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 undef, metadata !1063, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !1117
  %83 = icmp sgt i64 %59, 0, !dbg !1118
  br i1 %83, label %58, label %84, !dbg !1120, !llvm.loop !1147

; <label>:84:                                     ; preds = %80
  call void @llvm.dbg.value(metadata i32 %81, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %81, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %81, metadata !1066, metadata !DIExpression()), !dbg !1082
  %85 = trunc i64 %82 to i32, !dbg !1082
  call void @llvm.dbg.value(metadata i32 %81, metadata !1066, metadata !DIExpression()), !dbg !1082
  br label %86, !dbg !1149

; <label>:86:                                     ; preds = %84, %44
  %87 = phi i32 [ %53, %44 ], [ %85, %84 ]
  %88 = phi i32 [ %16, %44 ], [ %81, %84 ]
  call void @llvm.dbg.value(metadata i32 %87, metadata !1063, metadata !DIExpression()), !dbg !1117
  call void @llvm.dbg.value(metadata i32 %88, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %17, metadata !1065, metadata !DIExpression()), !dbg !1083
  %89 = icmp eq i32 %87, -1, !dbg !1149
  br i1 %89, label %90, label %99, !dbg !1138

; <label>:90:                                     ; preds = %86
  %91 = add nsw i32 %17, -1, !dbg !1151
  call void @llvm.dbg.value(metadata i32 %91, metadata !1065, metadata !DIExpression()), !dbg !1083
  %92 = add nsw i32 %18, -1, !dbg !1153
  call void @llvm.dbg.value(metadata i32 %92, metadata !1057, metadata !DIExpression()), !dbg !1076
  %93 = sext i32 %92 to i64, !dbg !1154
  %94 = getelementptr inbounds i32, i32* %5, i64 %93, !dbg !1154
  store i32 %21, i32* %94, align 4, !dbg !1155, !tbaa !146
  %95 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !1156, !tbaa !172
  %96 = icmp eq i32 (i8*, ...)* %95, null, !dbg !1156
  br i1 %96, label %99, label %97, !dbg !1159

; <label>:97:                                     ; preds = %90
  %98 = tail call i32 (i8*, ...) %95(i8* getelementptr inbounds ([31 x i8], [31 x i8]* @.str.14, i64 0, i64 0), i32 %21, i32 %91) #4, !dbg !1160
  br label %99, !dbg !1160

; <label>:99:                                     ; preds = %71, %90, %97, %86
  %100 = phi i32 [ %88, %97 ], [ %88, %90 ], [ %88, %86 ], [ %60, %71 ]
  %101 = phi i32 [ %92, %97 ], [ %92, %90 ], [ %18, %86 ], [ %18, %71 ]
  %102 = phi i32 [ %91, %97 ], [ %91, %90 ], [ %17, %86 ], [ %73, %71 ], !dbg !1162
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %102, metadata !1065, metadata !DIExpression()), !dbg !1083
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  %103 = icmp sgt i32 %102, -1, !dbg !1163
  br i1 %103, label %15, label %104, !dbg !1085, !llvm.loop !1164

; <label>:104:                                    ; preds = %99
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata i32 %101, metadata !1057, metadata !DIExpression()), !dbg !1076
  call void @llvm.dbg.value(metadata i32 %100, metadata !1066, metadata !DIExpression()), !dbg !1082
  tail call void @ftrace(i8* getelementptr inbounds ([4 x i8], [4 x i8]* @__FUNCTION__.dfs, i64 0, i64 0), i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.1, i64 0, i64 0), i32 112) #4, !dbg !1166
  store i32 %100, i32* %11, align 4, !dbg !1167, !tbaa !146
  ret i32 %101, !dbg !1168
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #3

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind readnone speculatable }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!9, !10, !11, !12}
!llvm.ident = !{!13}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_kernel.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5, !6, !8}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!6 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !7, size: 64)
!7 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!8 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !5, size: 64)
!9 = !{i32 2, !"Dwarf Version", i32 4}
!10 = !{i32 2, !"Debug Info Version", i32 3}
!11 = !{i32 1, !"wchar_size", i32 4}
!12 = !{i32 7, !"PIC Level", i32 2}
!13 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!14 = distinct !DISubprogram(name: "klu_kernel", scope: !1, file: !1, line: 640, type: !15, isLocal: false, isDefinition: true, scopeLine: 686, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !57)
!15 = !DISubroutineType(types: !16)
!16 = !{!17, !7, !6, !6, !8, !6, !17, !6, !6, !20, !8, !6, !6, !6, !6, !6, !6, !8, !6, !6, !6, !6, !7, !6, !8, !6, !6, !8, !24}
!17 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !18, line: 62, baseType: !19)
!18 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!19 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!20 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !21, size: 64)
!21 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !22, size: 64)
!22 = !DIDerivedType(tag: DW_TAG_typedef, name: "Unit", file: !23, line: 253, baseType: !5)
!23 = !DIFile(filename: "./klu_version.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!24 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !25, size: 64)
!25 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !26, line: 207, baseType: !27)
!26 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!27 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !26, line: 137, size: 1280, elements: !28)
!28 = !{!29, !30, !31, !32, !33, !34, !35, !36, !37, !42, !43, !44, !45, !46, !47, !48, !49, !50, !51, !52, !53, !54, !55, !56}
!29 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !27, file: !26, line: 144, baseType: !5, size: 64)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !27, file: !26, line: 145, baseType: !5, size: 64, offset: 64)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !27, file: !26, line: 146, baseType: !5, size: 64, offset: 128)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !27, file: !26, line: 147, baseType: !5, size: 64, offset: 192)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !27, file: !26, line: 148, baseType: !5, size: 64, offset: 256)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !27, file: !26, line: 150, baseType: !7, size: 32, offset: 320)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !27, file: !26, line: 151, baseType: !7, size: 32, offset: 352)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !27, file: !26, line: 153, baseType: !7, size: 32, offset: 384)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !27, file: !26, line: 157, baseType: !38, size: 64, offset: 448)
!38 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !39, size: 64)
!39 = !DISubroutineType(types: !40)
!40 = !{!7, !7, !6, !6, !6, !41}
!41 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !27, size: 64)
!42 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !27, file: !26, line: 162, baseType: !4, size: 64, offset: 512)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !27, file: !26, line: 164, baseType: !7, size: 32, offset: 576)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !27, file: !26, line: 177, baseType: !7, size: 32, offset: 608)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !27, file: !26, line: 178, baseType: !7, size: 32, offset: 640)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !27, file: !26, line: 180, baseType: !7, size: 32, offset: 672)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !27, file: !26, line: 185, baseType: !7, size: 32, offset: 704)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !27, file: !26, line: 191, baseType: !7, size: 32, offset: 736)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !27, file: !26, line: 196, baseType: !7, size: 32, offset: 768)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !27, file: !26, line: 198, baseType: !5, size: 64, offset: 832)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !27, file: !26, line: 199, baseType: !5, size: 64, offset: 896)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !27, file: !26, line: 200, baseType: !5, size: 64, offset: 960)
!53 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !27, file: !26, line: 201, baseType: !5, size: 64, offset: 1024)
!54 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !27, file: !26, line: 202, baseType: !5, size: 64, offset: 1088)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !27, file: !26, line: 204, baseType: !17, size: 64, offset: 1152)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !27, file: !26, line: 205, baseType: !17, size: 64, offset: 1216)
!57 = !{!58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68, !69, !70, !71, !72, !73, !74, !75, !76, !77, !78, !79, !80, !81, !82, !83, !84, !85, !86, !87, !88, !89, !90, !91, !92, !93, !94, !95, !96, !97, !98, !99, !100, !101, !102, !103, !104, !105, !106, !107, !108, !109}
!58 = !DILocalVariable(name: "n", arg: 1, scope: !14, file: !1, line: 643, type: !7)
!59 = !DILocalVariable(name: "Ap", arg: 2, scope: !14, file: !1, line: 644, type: !6)
!60 = !DILocalVariable(name: "Ai", arg: 3, scope: !14, file: !1, line: 645, type: !6)
!61 = !DILocalVariable(name: "Ax", arg: 4, scope: !14, file: !1, line: 646, type: !8)
!62 = !DILocalVariable(name: "Q", arg: 5, scope: !14, file: !1, line: 647, type: !6)
!63 = !DILocalVariable(name: "lusize", arg: 6, scope: !14, file: !1, line: 648, type: !17)
!64 = !DILocalVariable(name: "Pinv", arg: 7, scope: !14, file: !1, line: 651, type: !6)
!65 = !DILocalVariable(name: "P", arg: 8, scope: !14, file: !1, line: 653, type: !6)
!66 = !DILocalVariable(name: "p_LU", arg: 9, scope: !14, file: !1, line: 655, type: !20)
!67 = !DILocalVariable(name: "Udiag", arg: 10, scope: !14, file: !1, line: 656, type: !8)
!68 = !DILocalVariable(name: "Llen", arg: 11, scope: !14, file: !1, line: 657, type: !6)
!69 = !DILocalVariable(name: "Ulen", arg: 12, scope: !14, file: !1, line: 658, type: !6)
!70 = !DILocalVariable(name: "Lip", arg: 13, scope: !14, file: !1, line: 659, type: !6)
!71 = !DILocalVariable(name: "Uip", arg: 14, scope: !14, file: !1, line: 660, type: !6)
!72 = !DILocalVariable(name: "lnz", arg: 15, scope: !14, file: !1, line: 661, type: !6)
!73 = !DILocalVariable(name: "unz", arg: 16, scope: !14, file: !1, line: 662, type: !6)
!74 = !DILocalVariable(name: "X", arg: 17, scope: !14, file: !1, line: 664, type: !8)
!75 = !DILocalVariable(name: "Stack", arg: 18, scope: !14, file: !1, line: 667, type: !6)
!76 = !DILocalVariable(name: "Flag", arg: 19, scope: !14, file: !1, line: 668, type: !6)
!77 = !DILocalVariable(name: "Ap_pos", arg: 20, scope: !14, file: !1, line: 669, type: !6)
!78 = !DILocalVariable(name: "Lpend", arg: 21, scope: !14, file: !1, line: 672, type: !6)
!79 = !DILocalVariable(name: "k1", arg: 22, scope: !14, file: !1, line: 675, type: !7)
!80 = !DILocalVariable(name: "PSinv", arg: 23, scope: !14, file: !1, line: 676, type: !6)
!81 = !DILocalVariable(name: "Rs", arg: 24, scope: !14, file: !1, line: 677, type: !8)
!82 = !DILocalVariable(name: "Offp", arg: 25, scope: !14, file: !1, line: 680, type: !6)
!83 = !DILocalVariable(name: "Offi", arg: 26, scope: !14, file: !1, line: 681, type: !6)
!84 = !DILocalVariable(name: "Offx", arg: 27, scope: !14, file: !1, line: 682, type: !8)
!85 = !DILocalVariable(name: "Common", arg: 28, scope: !14, file: !1, line: 684, type: !24)
!86 = !DILocalVariable(name: "pivot", scope: !14, file: !1, line: 687, type: !5)
!87 = !DILocalVariable(name: "abs_pivot", scope: !14, file: !1, line: 688, type: !5)
!88 = !DILocalVariable(name: "xsize", scope: !14, file: !1, line: 688, type: !5)
!89 = !DILocalVariable(name: "nunits", scope: !14, file: !1, line: 688, type: !5)
!90 = !DILocalVariable(name: "tol", scope: !14, file: !1, line: 688, type: !5)
!91 = !DILocalVariable(name: "memgrow", scope: !14, file: !1, line: 688, type: !5)
!92 = !DILocalVariable(name: "Ux", scope: !14, file: !1, line: 689, type: !8)
!93 = !DILocalVariable(name: "Li", scope: !14, file: !1, line: 690, type: !6)
!94 = !DILocalVariable(name: "Ui", scope: !14, file: !1, line: 690, type: !6)
!95 = !DILocalVariable(name: "LU", scope: !14, file: !1, line: 691, type: !21)
!96 = !DILocalVariable(name: "k", scope: !14, file: !1, line: 692, type: !7)
!97 = !DILocalVariable(name: "p", scope: !14, file: !1, line: 692, type: !7)
!98 = !DILocalVariable(name: "i", scope: !14, file: !1, line: 692, type: !7)
!99 = !DILocalVariable(name: "j", scope: !14, file: !1, line: 692, type: !7)
!100 = !DILocalVariable(name: "pivrow", scope: !14, file: !1, line: 692, type: !7)
!101 = !DILocalVariable(name: "kbar", scope: !14, file: !1, line: 692, type: !7)
!102 = !DILocalVariable(name: "diagrow", scope: !14, file: !1, line: 692, type: !7)
!103 = !DILocalVariable(name: "firstrow", scope: !14, file: !1, line: 692, type: !7)
!104 = !DILocalVariable(name: "lup", scope: !14, file: !1, line: 692, type: !7)
!105 = !DILocalVariable(name: "top", scope: !14, file: !1, line: 692, type: !7)
!106 = !DILocalVariable(name: "scale", scope: !14, file: !1, line: 692, type: !7)
!107 = !DILocalVariable(name: "len", scope: !14, file: !1, line: 692, type: !7)
!108 = !DILocalVariable(name: "newlusize", scope: !14, file: !1, line: 693, type: !17)
!109 = !DILocalVariable(name: "xp", scope: !110, file: !1, line: 922, type: !21)
!110 = distinct !DILexicalBlock(scope: !111, file: !1, line: 922, column: 9)
!111 = distinct !DILexicalBlock(scope: !112, file: !1, line: 760, column: 5)
!112 = distinct !DILexicalBlock(scope: !113, file: !1, line: 759, column: 5)
!113 = distinct !DILexicalBlock(scope: !14, file: !1, line: 759, column: 5)
!114 = !DILocation(line: 643, column: 9, scope: !14)
!115 = !DILocation(line: 644, column: 9, scope: !14)
!116 = !DILocation(line: 645, column: 9, scope: !14)
!117 = !DILocation(line: 646, column: 11, scope: !14)
!118 = !DILocation(line: 647, column: 9, scope: !14)
!119 = !DILocation(line: 648, column: 12, scope: !14)
!120 = !DILocation(line: 651, column: 9, scope: !14)
!121 = !DILocation(line: 653, column: 9, scope: !14)
!122 = !DILocation(line: 655, column: 12, scope: !14)
!123 = !DILocation(line: 656, column: 11, scope: !14)
!124 = !DILocation(line: 657, column: 9, scope: !14)
!125 = !DILocation(line: 658, column: 9, scope: !14)
!126 = !DILocation(line: 659, column: 9, scope: !14)
!127 = !DILocation(line: 660, column: 9, scope: !14)
!128 = !DILocation(line: 661, column: 10, scope: !14)
!129 = !DILocation(line: 662, column: 10, scope: !14)
!130 = !DILocation(line: 664, column: 11, scope: !14)
!131 = !DILocation(line: 667, column: 9, scope: !14)
!132 = !DILocation(line: 668, column: 9, scope: !14)
!133 = !DILocation(line: 669, column: 9, scope: !14)
!134 = !DILocation(line: 672, column: 9, scope: !14)
!135 = !DILocation(line: 675, column: 9, scope: !14)
!136 = !DILocation(line: 676, column: 9, scope: !14)
!137 = !DILocation(line: 677, column: 12, scope: !14)
!138 = !DILocation(line: 680, column: 9, scope: !14)
!139 = !DILocation(line: 681, column: 9, scope: !14)
!140 = !DILocation(line: 682, column: 11, scope: !14)
!141 = !DILocation(line: 684, column: 17, scope: !14)
!142 = !DILocation(line: 687, column: 5, scope: !14)
!143 = !DILocation(line: 688, column: 5, scope: !14)
!144 = !DILocation(line: 692, column: 5, scope: !14)
!145 = !DILocation(line: 692, column: 21, scope: !14)
!146 = !{!147, !147, i64 0}
!147 = !{!"int", !148, i64 0}
!148 = !{!"omnipotent char", !149, i64 0}
!149 = !{!"Simple C/C++ TBAA"}
!150 = !DILocation(line: 700, column: 21, scope: !14)
!151 = !{!152, !147, i64 48}
!152 = !{!"klu_common_struct", !153, i64 0, !153, i64 8, !153, i64 16, !153, i64 24, !153, i64 32, !147, i64 40, !147, i64 44, !147, i64 48, !154, i64 56, !154, i64 64, !147, i64 72, !147, i64 76, !147, i64 80, !147, i64 84, !147, i64 88, !147, i64 92, !147, i64 96, !153, i64 104, !153, i64 112, !153, i64 120, !153, i64 128, !153, i64 136, !155, i64 144, !155, i64 152}
!153 = !{!"double", !148, i64 0}
!154 = !{!"any pointer", !148, i64 0}
!155 = !{!"long", !148, i64 0}
!156 = !DILocation(line: 692, column: 68, scope: !14)
!157 = !DILocation(line: 701, column: 19, scope: !14)
!158 = !{!152, !153, i64 0}
!159 = !DILocation(line: 688, column: 38, scope: !14)
!160 = !DILocation(line: 702, column: 23, scope: !14)
!161 = !{!152, !153, i64 8}
!162 = !DILocation(line: 688, column: 43, scope: !14)
!163 = !DILocation(line: 703, column: 10, scope: !14)
!164 = !DILocation(line: 704, column: 10, scope: !14)
!165 = !DILocation(line: 687, column: 11, scope: !14)
!166 = !DILocation(line: 705, column: 5, scope: !167)
!167 = distinct !DILexicalBlock(scope: !14, file: !1, line: 705, column: 5)
!168 = !{!153, !153, i64 0}
!169 = !DILocation(line: 711, column: 5, scope: !170)
!170 = distinct !DILexicalBlock(scope: !171, file: !1, line: 711, column: 5)
!171 = distinct !DILexicalBlock(scope: !14, file: !1, line: 711, column: 5)
!172 = !{!173, !154, i64 32}
!173 = !{!"SuiteSparse_config_struct", !154, i64 0, !154, i64 8, !154, i64 16, !154, i64 24, !154, i64 32, !154, i64 40, !154, i64 48}
!174 = !DILocation(line: 711, column: 5, scope: !171)
!175 = !DILocation(line: 711, column: 5, scope: !176)
!176 = distinct !DILexicalBlock(scope: !170, file: !1, line: 711, column: 5)
!177 = !DILocation(line: 713, column: 10, scope: !14)
!178 = !{!154, !154, i64 0}
!179 = !DILocation(line: 691, column: 11, scope: !14)
!180 = !DILocation(line: 692, column: 48, scope: !14)
!181 = !DILocation(line: 719, column: 14, scope: !14)
!182 = !DILocation(line: 692, column: 58, scope: !14)
!183 = !DILocation(line: 692, column: 9, scope: !14)
!184 = !DILocation(line: 722, column: 20, scope: !185)
!185 = distinct !DILexicalBlock(scope: !186, file: !1, line: 722, column: 5)
!186 = distinct !DILexicalBlock(scope: !14, file: !1, line: 722, column: 5)
!187 = !DILocation(line: 722, column: 5, scope: !186)
!188 = !DILocation(line: 725, column: 9, scope: !189)
!189 = distinct !DILexicalBlock(scope: !190, file: !1, line: 725, column: 9)
!190 = distinct !DILexicalBlock(scope: !185, file: !1, line: 723, column: 5)
!191 = !DILocation(line: 726, column: 9, scope: !190)
!192 = !DILocation(line: 726, column: 18, scope: !190)
!193 = !DILocation(line: 727, column: 9, scope: !190)
!194 = !DILocation(line: 727, column: 19, scope: !190)
!195 = !DILocation(line: 722, column: 27, scope: !185)
!196 = distinct !{!196, !187, !197}
!197 = !DILocation(line: 728, column: 5, scope: !186)
!198 = !DILocation(line: 735, column: 20, scope: !199)
!199 = distinct !DILexicalBlock(scope: !200, file: !1, line: 735, column: 5)
!200 = distinct !DILexicalBlock(scope: !14, file: !1, line: 735, column: 5)
!201 = !DILocation(line: 735, column: 5, scope: !200)
!202 = !DILocation(line: 737, column: 9, scope: !203)
!203 = distinct !DILexicalBlock(scope: !199, file: !1, line: 736, column: 5)
!204 = !DILocation(line: 737, column: 15, scope: !203)
!205 = !DILocation(line: 738, column: 9, scope: !203)
!206 = !DILocation(line: 738, column: 18, scope: !203)
!207 = !DILocation(line: 735, column: 27, scope: !199)
!208 = distinct !{!208, !201, !209}
!209 = !DILocation(line: 739, column: 5, scope: !200)
!210 = !DILocation(line: 741, column: 14, scope: !14)
!211 = !DILocation(line: 759, column: 5, scope: !113)
!212 = !DILocation(line: 759, column: 20, scope: !112)
!213 = !DILocation(line: 761, column: 9, scope: !111)
!214 = !DILocation(line: 763, column: 9, scope: !215)
!215 = distinct !DILexicalBlock(scope: !216, file: !1, line: 763, column: 9)
!216 = distinct !DILexicalBlock(scope: !111, file: !1, line: 763, column: 9)
!217 = !DILocation(line: 763, column: 9, scope: !216)
!218 = !DILocation(line: 763, column: 9, scope: !219)
!219 = distinct !DILexicalBlock(scope: !215, file: !1, line: 763, column: 9)
!220 = !DILocation(line: 770, column: 18, scope: !111)
!221 = !DILocation(line: 770, column: 40, scope: !111)
!222 = !DILocation(line: 770, column: 38, scope: !111)
!223 = !DILocation(line: 771, column: 18, scope: !111)
!224 = !DILocation(line: 770, column: 56, scope: !111)
!225 = !DILocation(line: 771, column: 42, scope: !111)
!226 = !DILocation(line: 771, column: 40, scope: !111)
!227 = !DILocation(line: 688, column: 30, scope: !14)
!228 = !DILocation(line: 774, column: 9, scope: !229)
!229 = distinct !DILexicalBlock(scope: !230, file: !1, line: 774, column: 9)
!230 = distinct !DILexicalBlock(scope: !111, file: !1, line: 774, column: 9)
!231 = !DILocation(line: 774, column: 9, scope: !230)
!232 = !DILocation(line: 774, column: 9, scope: !233)
!233 = distinct !DILexicalBlock(scope: !229, file: !1, line: 774, column: 9)
!234 = !DILocation(line: 776, column: 18, scope: !111)
!235 = !DILocation(line: 776, column: 32, scope: !111)
!236 = !DILocation(line: 688, column: 23, scope: !14)
!237 = !DILocation(line: 777, column: 21, scope: !238)
!238 = distinct !DILexicalBlock(scope: !111, file: !1, line: 777, column: 13)
!239 = !DILocation(line: 777, column: 19, scope: !238)
!240 = !DILocation(line: 777, column: 13, scope: !111)
!241 = !DILocation(line: 779, column: 13, scope: !242)
!242 = distinct !DILexicalBlock(scope: !238, file: !1, line: 778, column: 9)
!243 = !DILocation(line: 782, column: 30, scope: !242)
!244 = !DILocation(line: 782, column: 50, scope: !242)
!245 = !DILocation(line: 782, column: 56, scope: !242)
!246 = !DILocation(line: 783, column: 17, scope: !247)
!247 = distinct !DILexicalBlock(scope: !242, file: !1, line: 783, column: 17)
!248 = !DILocation(line: 785, column: 17, scope: !249)
!249 = distinct !DILexicalBlock(scope: !250, file: !1, line: 785, column: 17)
!250 = distinct !DILexicalBlock(scope: !251, file: !1, line: 785, column: 17)
!251 = distinct !DILexicalBlock(scope: !247, file: !1, line: 784, column: 13)
!252 = !DILocation(line: 785, column: 17, scope: !250)
!253 = !DILocation(line: 785, column: 17, scope: !254)
!254 = distinct !DILexicalBlock(scope: !249, file: !1, line: 785, column: 17)
!255 = !DILocation(line: 786, column: 25, scope: !251)
!256 = !DILocation(line: 786, column: 32, scope: !251)
!257 = !{!152, !147, i64 76}
!258 = !DILocation(line: 787, column: 17, scope: !251)
!259 = !DILocation(line: 789, column: 42, scope: !242)
!260 = !DILocation(line: 789, column: 48, scope: !242)
!261 = !DILocation(line: 789, column: 25, scope: !242)
!262 = !DILocation(line: 693, column: 12, scope: !14)
!263 = !DILocation(line: 791, column: 65, scope: !242)
!264 = !DILocation(line: 791, column: 18, scope: !242)
!265 = !DILocation(line: 792, column: 29, scope: !242)
!266 = !{!152, !147, i64 80}
!267 = !DILocation(line: 793, column: 19, scope: !242)
!268 = !DILocation(line: 794, column: 25, scope: !269)
!269 = distinct !DILexicalBlock(scope: !242, file: !1, line: 794, column: 17)
!270 = !DILocation(line: 794, column: 32, scope: !269)
!271 = !DILocation(line: 0, scope: !272)
!272 = distinct !DILexicalBlock(scope: !273, file: !1, line: 800, column: 13)
!273 = distinct !DILexicalBlock(scope: !242, file: !1, line: 800, column: 13)
!274 = !DILocation(line: 794, column: 17, scope: !242)
!275 = !DILocation(line: 796, column: 17, scope: !276)
!276 = distinct !DILexicalBlock(scope: !277, file: !1, line: 796, column: 17)
!277 = distinct !DILexicalBlock(scope: !269, file: !1, line: 795, column: 13)
!278 = !DILocation(line: 796, column: 17, scope: !279)
!279 = distinct !DILexicalBlock(scope: !280, file: !1, line: 796, column: 17)
!280 = distinct !DILexicalBlock(scope: !276, file: !1, line: 796, column: 17)
!281 = !DILocation(line: 800, column: 13, scope: !273)
!282 = !DILocation(line: 800, column: 13, scope: !283)
!283 = distinct !DILexicalBlock(scope: !272, file: !1, line: 800, column: 13)
!284 = !DILocation(line: 0, scope: !14)
!285 = !DILocation(line: 807, column: 9, scope: !111)
!286 = !DILocation(line: 807, column: 17, scope: !111)
!287 = !DILocation(line: 822, column: 15, scope: !111)
!288 = !DILocation(line: 692, column: 63, scope: !14)
!289 = !DILocation(line: 849, column: 9, scope: !111)
!290 = !DILocation(line: 855, column: 9, scope: !111)
!291 = !DILocation(line: 862, column: 9, scope: !111)
!292 = !DILocation(line: 883, column: 19, scope: !111)
!293 = !DILocation(line: 692, column: 39, scope: !14)
!294 = !DILocation(line: 884, column: 9, scope: !295)
!295 = distinct !DILexicalBlock(scope: !296, file: !1, line: 884, column: 9)
!296 = distinct !DILexicalBlock(scope: !111, file: !1, line: 884, column: 9)
!297 = !DILocation(line: 884, column: 9, scope: !296)
!298 = !DILocation(line: 884, column: 9, scope: !299)
!299 = distinct !DILexicalBlock(scope: !295, file: !1, line: 884, column: 9)
!300 = !DILocation(line: 688, column: 12, scope: !14)
!301 = !DILocation(line: 888, column: 14, scope: !302)
!302 = distinct !DILexicalBlock(scope: !111, file: !1, line: 888, column: 13)
!303 = !DILocation(line: 888, column: 13, scope: !111)
!304 = !DILocation(line: 892, column: 28, scope: !305)
!305 = distinct !DILexicalBlock(scope: !302, file: !1, line: 890, column: 9)
!306 = !DILocation(line: 893, column: 25, scope: !307)
!307 = distinct !DILexicalBlock(scope: !305, file: !1, line: 893, column: 17)
!308 = !{!152, !147, i64 88}
!309 = !DILocation(line: 893, column: 40, scope: !307)
!310 = !DILocation(line: 893, column: 17, scope: !305)
!311 = !DILocation(line: 895, column: 43, scope: !312)
!312 = distinct !DILexicalBlock(scope: !307, file: !1, line: 894, column: 13)
!313 = !DILocation(line: 895, column: 40, scope: !312)
!314 = !DILocation(line: 896, column: 40, scope: !312)
!315 = !DILocation(line: 896, column: 38, scope: !312)
!316 = !{!152, !147, i64 92}
!317 = !DILocation(line: 897, column: 13, scope: !312)
!318 = !DILocation(line: 898, column: 25, scope: !319)
!319 = distinct !DILexicalBlock(scope: !305, file: !1, line: 898, column: 17)
!320 = !{!152, !147, i64 72}
!321 = !DILocation(line: 898, column: 17, scope: !319)
!322 = !DILocation(line: 898, column: 17, scope: !305)
!323 = !DILocation(line: 907, column: 9, scope: !324)
!324 = distinct !DILexicalBlock(scope: !325, file: !1, line: 907, column: 9)
!325 = distinct !DILexicalBlock(scope: !111, file: !1, line: 907, column: 9)
!326 = !DILocation(line: 907, column: 9, scope: !325)
!327 = !DILocation(line: 907, column: 9, scope: !328)
!328 = distinct !DILexicalBlock(scope: !324, file: !1, line: 907, column: 9)
!329 = !DILocation(line: 908, column: 9, scope: !330)
!330 = distinct !DILexicalBlock(scope: !331, file: !1, line: 908, column: 9)
!331 = distinct !DILexicalBlock(scope: !332, file: !1, line: 908, column: 9)
!332 = distinct !DILexicalBlock(scope: !333, file: !1, line: 908, column: 9)
!333 = distinct !DILexicalBlock(scope: !334, file: !1, line: 908, column: 9)
!334 = distinct !DILexicalBlock(scope: !111, file: !1, line: 908, column: 9)
!335 = !DILocation(line: 908, column: 9, scope: !333)
!336 = !DILocation(line: 908, column: 9, scope: !334)
!337 = !DILocation(line: 908, column: 9, scope: !331)
!338 = !DILocation(line: 908, column: 9, scope: !339)
!339 = distinct !DILexicalBlock(scope: !330, file: !1, line: 908, column: 9)
!340 = !DILocation(line: 908, column: 9, scope: !341)
!341 = distinct !DILexicalBlock(scope: !342, file: !1, line: 908, column: 9)
!342 = distinct !DILexicalBlock(scope: !333, file: !1, line: 908, column: 9)
!343 = !DILocation(line: 908, column: 9, scope: !344)
!344 = distinct !DILexicalBlock(scope: !345, file: !1, line: 908, column: 9)
!345 = distinct !DILexicalBlock(scope: !341, file: !1, line: 908, column: 9)
!346 = !DILocation(line: 913, column: 19, scope: !111)
!347 = !DILocation(line: 913, column: 29, scope: !111)
!348 = !DILocation(line: 913, column: 9, scope: !111)
!349 = !DILocation(line: 913, column: 17, scope: !111)
!350 = !DILocation(line: 917, column: 16, scope: !111)
!351 = !DILocation(line: 917, column: 13, scope: !111)
!352 = !DILocation(line: 919, column: 22, scope: !111)
!353 = !DILocation(line: 919, column: 9, scope: !111)
!354 = !DILocation(line: 919, column: 18, scope: !111)
!355 = !DILocation(line: 922, column: 9, scope: !110)
!356 = !DILocation(line: 692, column: 75, scope: !14)
!357 = !DILocation(line: 690, column: 15, scope: !14)
!358 = !DILocation(line: 689, column: 12, scope: !14)
!359 = !DILocation(line: 692, column: 12, scope: !14)
!360 = !DILocation(line: 692, column: 15, scope: !14)
!361 = !DILocation(line: 923, column: 33, scope: !362)
!362 = distinct !DILexicalBlock(scope: !363, file: !1, line: 923, column: 9)
!363 = distinct !DILexicalBlock(scope: !111, file: !1, line: 923, column: 9)
!364 = !DILocation(line: 923, column: 9, scope: !363)
!365 = !DILocation(line: 925, column: 17, scope: !366)
!366 = distinct !DILexicalBlock(scope: !362, file: !1, line: 924, column: 9)
!367 = !DILocation(line: 692, column: 18, scope: !14)
!368 = !DILocation(line: 926, column: 22, scope: !366)
!369 = !DILocation(line: 926, column: 13, scope: !366)
!370 = !DILocation(line: 926, column: 20, scope: !366)
!371 = !DILocation(line: 927, column: 22, scope: !366)
!372 = !DILocation(line: 927, column: 13, scope: !366)
!373 = !DILocation(line: 927, column: 20, scope: !366)
!374 = !DILocation(line: 928, column: 13, scope: !375)
!375 = distinct !DILexicalBlock(scope: !366, file: !1, line: 928, column: 13)
!376 = !DILocation(line: 923, column: 40, scope: !362)
!377 = !DILocation(line: 923, column: 45, scope: !362)
!378 = distinct !{!378, !364, !379}
!379 = !DILocation(line: 929, column: 9, scope: !363)
!380 = !DILocation(line: 932, column: 16, scope: !111)
!381 = !DILocation(line: 932, column: 13, scope: !111)
!382 = !DILocation(line: 935, column: 21, scope: !111)
!383 = !DILocation(line: 935, column: 9, scope: !111)
!384 = !DILocation(line: 935, column: 19, scope: !111)
!385 = !DILocation(line: 944, column: 13, scope: !386)
!386 = distinct !DILexicalBlock(scope: !111, file: !1, line: 944, column: 13)
!387 = !DILocation(line: 944, column: 20, scope: !386)
!388 = !DILocation(line: 944, column: 13, scope: !111)
!389 = !DILocation(line: 947, column: 29, scope: !390)
!390 = distinct !DILexicalBlock(scope: !386, file: !1, line: 945, column: 9)
!391 = !{!152, !147, i64 96}
!392 = !DILocation(line: 948, column: 13, scope: !393)
!393 = distinct !DILexicalBlock(scope: !394, file: !1, line: 948, column: 13)
!394 = distinct !DILexicalBlock(scope: !390, file: !1, line: 948, column: 13)
!395 = !DILocation(line: 948, column: 13, scope: !394)
!396 = !DILocation(line: 948, column: 13, scope: !397)
!397 = distinct !DILexicalBlock(scope: !393, file: !1, line: 948, column: 13)
!398 = !DILocation(line: 950, column: 17, scope: !399)
!399 = distinct !DILexicalBlock(scope: !390, file: !1, line: 950, column: 17)
!400 = !DILocation(line: 950, column: 32, scope: !399)
!401 = !DILocation(line: 950, column: 17, scope: !390)
!402 = !DILocation(line: 956, column: 24, scope: !403)
!403 = distinct !DILexicalBlock(scope: !399, file: !1, line: 951, column: 13)
!404 = !DILocation(line: 692, column: 33, scope: !14)
!405 = !DILocation(line: 957, column: 17, scope: !403)
!406 = !DILocation(line: 957, column: 26, scope: !403)
!407 = !DILocation(line: 958, column: 32, scope: !403)
!408 = !DILocation(line: 959, column: 13, scope: !403)
!409 = !DILocation(line: 961, column: 15, scope: !111)
!410 = !DILocation(line: 962, column: 9, scope: !111)
!411 = !DILocation(line: 962, column: 23, scope: !111)
!412 = !DILocation(line: 984, column: 9, scope: !111)
!413 = !DILocation(line: 986, column: 17, scope: !111)
!414 = !DILocation(line: 986, column: 26, scope: !111)
!415 = !DILocation(line: 986, column: 14, scope: !111)
!416 = !DILocation(line: 987, column: 17, scope: !111)
!417 = !DILocation(line: 987, column: 26, scope: !111)
!418 = !DILocation(line: 987, column: 14, scope: !111)
!419 = !DILocation(line: 759, column: 27, scope: !112)
!420 = distinct !{!420, !211, !421}
!421 = !DILocation(line: 988, column: 5, scope: !113)
!422 = !DILocation(line: 994, column: 20, scope: !423)
!423 = distinct !DILexicalBlock(scope: !424, file: !1, line: 994, column: 5)
!424 = distinct !DILexicalBlock(scope: !14, file: !1, line: 994, column: 5)
!425 = !DILocation(line: 994, column: 5, scope: !424)
!426 = !DILocation(line: 996, column: 28, scope: !427)
!427 = distinct !DILexicalBlock(scope: !423, file: !1, line: 995, column: 5)
!428 = !DILocation(line: 996, column: 26, scope: !427)
!429 = !DILocation(line: 996, column: 14, scope: !427)
!430 = !DILocation(line: 690, column: 10, scope: !14)
!431 = !DILocation(line: 997, column: 26, scope: !432)
!432 = distinct !DILexicalBlock(scope: !433, file: !1, line: 997, column: 9)
!433 = distinct !DILexicalBlock(scope: !427, file: !1, line: 997, column: 9)
!434 = !DILocation(line: 997, column: 24, scope: !432)
!435 = !DILocation(line: 997, column: 9, scope: !433)
!436 = !DILocation(line: 999, column: 28, scope: !437)
!437 = distinct !DILexicalBlock(scope: !432, file: !1, line: 998, column: 9)
!438 = !DILocation(line: 999, column: 22, scope: !437)
!439 = !DILocation(line: 999, column: 20, scope: !437)
!440 = !DILocation(line: 997, column: 38, scope: !432)
!441 = distinct !{!441, !435, !442}
!442 = !DILocation(line: 1000, column: 9, scope: !433)
!443 = !DILocation(line: 994, column: 27, scope: !423)
!444 = distinct !{!444, !425, !445}
!445 = !DILocation(line: 1001, column: 5, scope: !424)
!446 = !DILocation(line: 1020, column: 5, scope: !14)
!447 = !DILocation(line: 1025, column: 10, scope: !14)
!448 = !DILocation(line: 1026, column: 11, scope: !14)
!449 = !DILocation(line: 1027, column: 5, scope: !14)
!450 = !DILocation(line: 1028, column: 1, scope: !14)
!451 = distinct !DISubprogram(name: "lsolve_symbolic", scope: !1, file: !1, line: 124, type: !452, isLocal: true, isDefinition: true, scopeLine: 157, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !454)
!452 = !DISubroutineType(types: !453)
!453 = !{!7, !7, !7, !6, !6, !6, !6, !6, !6, !6, !6, !21, !7, !6, !6, !7, !6}
!454 = !{!455, !456, !457, !458, !459, !460, !461, !462, !463, !464, !465, !466, !467, !468, !469, !470, !471, !472, !473, !474, !475, !476, !477, !478}
!455 = !DILocalVariable(name: "n", arg: 1, scope: !451, file: !1, line: 127, type: !7)
!456 = !DILocalVariable(name: "k", arg: 2, scope: !451, file: !1, line: 128, type: !7)
!457 = !DILocalVariable(name: "Ap", arg: 3, scope: !451, file: !1, line: 129, type: !6)
!458 = !DILocalVariable(name: "Ai", arg: 4, scope: !451, file: !1, line: 130, type: !6)
!459 = !DILocalVariable(name: "Q", arg: 5, scope: !451, file: !1, line: 131, type: !6)
!460 = !DILocalVariable(name: "Pinv", arg: 6, scope: !451, file: !1, line: 132, type: !6)
!461 = !DILocalVariable(name: "Stack", arg: 7, scope: !451, file: !1, line: 136, type: !6)
!462 = !DILocalVariable(name: "Flag", arg: 8, scope: !451, file: !1, line: 139, type: !6)
!463 = !DILocalVariable(name: "Lpend", arg: 9, scope: !451, file: !1, line: 144, type: !6)
!464 = !DILocalVariable(name: "Ap_pos", arg: 10, scope: !451, file: !1, line: 145, type: !6)
!465 = !DILocalVariable(name: "LU", arg: 11, scope: !451, file: !1, line: 147, type: !21)
!466 = !DILocalVariable(name: "lup", arg: 12, scope: !451, file: !1, line: 148, type: !7)
!467 = !DILocalVariable(name: "Llen", arg: 13, scope: !451, file: !1, line: 149, type: !6)
!468 = !DILocalVariable(name: "Lip", arg: 14, scope: !451, file: !1, line: 150, type: !6)
!469 = !DILocalVariable(name: "k1", arg: 15, scope: !451, file: !1, line: 154, type: !7)
!470 = !DILocalVariable(name: "PSinv", arg: 16, scope: !451, file: !1, line: 155, type: !6)
!471 = !DILocalVariable(name: "Lik", scope: !451, file: !1, line: 158, type: !6)
!472 = !DILocalVariable(name: "i", scope: !451, file: !1, line: 159, type: !7)
!473 = !DILocalVariable(name: "p", scope: !451, file: !1, line: 159, type: !7)
!474 = !DILocalVariable(name: "pend", scope: !451, file: !1, line: 159, type: !7)
!475 = !DILocalVariable(name: "oldcol", scope: !451, file: !1, line: 159, type: !7)
!476 = !DILocalVariable(name: "kglobal", scope: !451, file: !1, line: 159, type: !7)
!477 = !DILocalVariable(name: "top", scope: !451, file: !1, line: 159, type: !7)
!478 = !DILocalVariable(name: "l_length", scope: !451, file: !1, line: 159, type: !7)
!479 = !DILocation(line: 127, column: 9, scope: !451)
!480 = !DILocation(line: 128, column: 9, scope: !451)
!481 = !DILocation(line: 129, column: 9, scope: !451)
!482 = !DILocation(line: 130, column: 9, scope: !451)
!483 = !DILocation(line: 131, column: 9, scope: !451)
!484 = !DILocation(line: 132, column: 9, scope: !451)
!485 = !DILocation(line: 136, column: 9, scope: !451)
!486 = !DILocation(line: 139, column: 9, scope: !451)
!487 = !DILocation(line: 144, column: 9, scope: !451)
!488 = !DILocation(line: 145, column: 9, scope: !451)
!489 = !DILocation(line: 147, column: 10, scope: !451)
!490 = !DILocation(line: 148, column: 9, scope: !451)
!491 = !DILocation(line: 149, column: 9, scope: !451)
!492 = !DILocation(line: 150, column: 9, scope: !451)
!493 = !DILocation(line: 154, column: 9, scope: !451)
!494 = !DILocation(line: 155, column: 9, scope: !451)
!495 = !DILocation(line: 159, column: 5, scope: !451)
!496 = !DILocation(line: 159, column: 38, scope: !451)
!497 = !DILocation(line: 159, column: 43, scope: !451)
!498 = !DILocation(line: 162, column: 14, scope: !451)
!499 = !DILocation(line: 163, column: 23, scope: !451)
!500 = !DILocation(line: 163, column: 11, scope: !451)
!501 = !DILocation(line: 158, column: 10, scope: !451)
!502 = !DILocation(line: 169, column: 17, scope: !451)
!503 = !DILocation(line: 159, column: 29, scope: !451)
!504 = !DILocation(line: 170, column: 14, scope: !451)
!505 = !DILocation(line: 159, column: 21, scope: !451)
!506 = !DILocation(line: 171, column: 22, scope: !451)
!507 = !DILocation(line: 171, column: 12, scope: !451)
!508 = !DILocation(line: 159, column: 15, scope: !451)
!509 = !DILocation(line: 172, column: 14, scope: !510)
!510 = distinct !DILexicalBlock(scope: !451, file: !1, line: 172, column: 5)
!511 = !DILocation(line: 159, column: 12, scope: !451)
!512 = !DILocation(line: 172, column: 30, scope: !513)
!513 = distinct !DILexicalBlock(scope: !510, file: !1, line: 172, column: 5)
!514 = !DILocation(line: 172, column: 5, scope: !510)
!515 = !DILocation(line: 174, column: 9, scope: !516)
!516 = distinct !DILexicalBlock(scope: !513, file: !1, line: 173, column: 5)
!517 = !DILocation(line: 175, column: 20, scope: !516)
!518 = !DILocation(line: 175, column: 13, scope: !516)
!519 = !DILocation(line: 175, column: 28, scope: !516)
!520 = !DILocation(line: 159, column: 9, scope: !451)
!521 = !DILocation(line: 176, column: 15, scope: !522)
!522 = distinct !DILexicalBlock(scope: !516, file: !1, line: 176, column: 13)
!523 = !DILocation(line: 176, column: 13, scope: !516)
!524 = !DILocation(line: 179, column: 9, scope: !525)
!525 = distinct !DILexicalBlock(scope: !526, file: !1, line: 179, column: 9)
!526 = distinct !DILexicalBlock(scope: !516, file: !1, line: 179, column: 9)
!527 = !DILocation(line: 179, column: 9, scope: !526)
!528 = !DILocation(line: 179, column: 9, scope: !529)
!529 = distinct !DILexicalBlock(scope: !525, file: !1, line: 179, column: 9)
!530 = !DILocation(line: 180, column: 13, scope: !531)
!531 = distinct !DILexicalBlock(scope: !516, file: !1, line: 180, column: 13)
!532 = !DILocation(line: 180, column: 22, scope: !531)
!533 = !DILocation(line: 180, column: 13, scope: !516)
!534 = !DILocation(line: 182, column: 13, scope: !535)
!535 = distinct !DILexicalBlock(scope: !531, file: !1, line: 181, column: 9)
!536 = !DILocation(line: 183, column: 17, scope: !537)
!537 = distinct !DILexicalBlock(scope: !535, file: !1, line: 183, column: 17)
!538 = !DILocation(line: 183, column: 26, scope: !537)
!539 = !DILocation(line: 183, column: 17, scope: !535)
!540 = !DILocation(line: 185, column: 17, scope: !541)
!541 = distinct !DILexicalBlock(scope: !537, file: !1, line: 184, column: 13)
!542 = !DILocation(line: 186, column: 23, scope: !541)
!543 = !DILocation(line: 188, column: 13, scope: !541)
!544 = !DILocation(line: 191, column: 17, scope: !545)
!545 = distinct !DILexicalBlock(scope: !537, file: !1, line: 190, column: 13)
!546 = !DILocation(line: 193, column: 26, scope: !545)
!547 = !DILocation(line: 194, column: 22, scope: !545)
!548 = !DILocation(line: 194, column: 17, scope: !545)
!549 = !DILocation(line: 194, column: 32, scope: !545)
!550 = !DILocation(line: 195, column: 25, scope: !545)
!551 = !DILocation(line: 0, scope: !451)
!552 = !DILocation(line: 172, column: 40, scope: !513)
!553 = distinct !{!553, !514, !554}
!554 = !DILocation(line: 198, column: 5, scope: !510)
!555 = !DILocation(line: 200, column: 5, scope: !451)
!556 = !DILocation(line: 202, column: 16, scope: !451)
!557 = !DILocation(line: 202, column: 5, scope: !451)
!558 = !DILocation(line: 202, column: 14, scope: !451)
!559 = !DILocation(line: 204, column: 1, scope: !451)
!560 = !DILocation(line: 203, column: 5, scope: !451)
!561 = distinct !DISubprogram(name: "construct_column", scope: !1, file: !1, line: 215, type: !562, isLocal: true, isDefinition: true, scopeLine: 240, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !564)
!562 = !DISubroutineType(types: !563)
!563 = !{null, !7, !6, !6, !8, !6, !8, !7, !6, !8, !7, !6, !6, !8}
!564 = !{!565, !566, !567, !568, !569, !570, !571, !572, !573, !574, !575, !576, !577, !578, !579, !580, !581, !582, !583, !584, !585}
!565 = !DILocalVariable(name: "k", arg: 1, scope: !561, file: !1, line: 218, type: !7)
!566 = !DILocalVariable(name: "Ap", arg: 2, scope: !561, file: !1, line: 219, type: !6)
!567 = !DILocalVariable(name: "Ai", arg: 3, scope: !561, file: !1, line: 220, type: !6)
!568 = !DILocalVariable(name: "Ax", arg: 4, scope: !561, file: !1, line: 221, type: !8)
!569 = !DILocalVariable(name: "Q", arg: 5, scope: !561, file: !1, line: 222, type: !6)
!570 = !DILocalVariable(name: "X", arg: 6, scope: !561, file: !1, line: 225, type: !8)
!571 = !DILocalVariable(name: "k1", arg: 7, scope: !561, file: !1, line: 230, type: !7)
!572 = !DILocalVariable(name: "PSinv", arg: 8, scope: !561, file: !1, line: 231, type: !6)
!573 = !DILocalVariable(name: "Rs", arg: 9, scope: !561, file: !1, line: 232, type: !8)
!574 = !DILocalVariable(name: "scale", arg: 10, scope: !561, file: !1, line: 233, type: !7)
!575 = !DILocalVariable(name: "Offp", arg: 11, scope: !561, file: !1, line: 236, type: !6)
!576 = !DILocalVariable(name: "Offi", arg: 12, scope: !561, file: !1, line: 237, type: !6)
!577 = !DILocalVariable(name: "Offx", arg: 13, scope: !561, file: !1, line: 238, type: !8)
!578 = !DILocalVariable(name: "aik", scope: !561, file: !1, line: 241, type: !5)
!579 = !DILocalVariable(name: "i", scope: !561, file: !1, line: 242, type: !7)
!580 = !DILocalVariable(name: "p", scope: !561, file: !1, line: 242, type: !7)
!581 = !DILocalVariable(name: "pend", scope: !561, file: !1, line: 242, type: !7)
!582 = !DILocalVariable(name: "oldcol", scope: !561, file: !1, line: 242, type: !7)
!583 = !DILocalVariable(name: "kglobal", scope: !561, file: !1, line: 242, type: !7)
!584 = !DILocalVariable(name: "poff", scope: !561, file: !1, line: 242, type: !7)
!585 = !DILocalVariable(name: "oldrow", scope: !561, file: !1, line: 242, type: !7)
!586 = !DILocation(line: 218, column: 9, scope: !561)
!587 = !DILocation(line: 219, column: 9, scope: !561)
!588 = !DILocation(line: 220, column: 9, scope: !561)
!589 = !DILocation(line: 221, column: 11, scope: !561)
!590 = !DILocation(line: 222, column: 9, scope: !561)
!591 = !DILocation(line: 225, column: 11, scope: !561)
!592 = !DILocation(line: 230, column: 9, scope: !561)
!593 = !DILocation(line: 231, column: 9, scope: !561)
!594 = !DILocation(line: 232, column: 12, scope: !561)
!595 = !DILocation(line: 233, column: 9, scope: !561)
!596 = !DILocation(line: 236, column: 9, scope: !561)
!597 = !DILocation(line: 237, column: 9, scope: !561)
!598 = !DILocation(line: 238, column: 11, scope: !561)
!599 = !DILocation(line: 248, column: 17, scope: !561)
!600 = !DILocation(line: 242, column: 29, scope: !561)
!601 = !DILocation(line: 249, column: 12, scope: !561)
!602 = !DILocation(line: 242, column: 38, scope: !561)
!603 = !DILocation(line: 250, column: 14, scope: !561)
!604 = !DILocation(line: 242, column: 21, scope: !561)
!605 = !DILocation(line: 251, column: 22, scope: !561)
!606 = !DILocation(line: 251, column: 12, scope: !561)
!607 = !DILocation(line: 242, column: 15, scope: !561)
!608 = !DILocation(line: 253, column: 15, scope: !609)
!609 = distinct !DILexicalBlock(scope: !561, file: !1, line: 253, column: 9)
!610 = !DILocation(line: 253, column: 9, scope: !561)
!611 = !DILocation(line: 255, column: 9, scope: !612)
!612 = distinct !DILexicalBlock(scope: !609, file: !1, line: 254, column: 5)
!613 = !DILocation(line: 258, column: 18, scope: !614)
!614 = distinct !DILexicalBlock(scope: !612, file: !1, line: 258, column: 9)
!615 = !DILocation(line: 242, column: 12, scope: !561)
!616 = !DILocation(line: 258, column: 34, scope: !617)
!617 = distinct !DILexicalBlock(scope: !614, file: !1, line: 258, column: 9)
!618 = !DILocation(line: 258, column: 9, scope: !614)
!619 = !DILocation(line: 260, column: 22, scope: !620)
!620 = distinct !DILexicalBlock(scope: !617, file: !1, line: 259, column: 9)
!621 = !DILocation(line: 242, column: 44, scope: !561)
!622 = !DILocation(line: 261, column: 17, scope: !620)
!623 = !DILocation(line: 261, column: 32, scope: !620)
!624 = !DILocation(line: 242, column: 9, scope: !561)
!625 = !DILocation(line: 262, column: 19, scope: !620)
!626 = !DILocation(line: 241, column: 11, scope: !561)
!627 = !DILocation(line: 263, column: 19, scope: !628)
!628 = distinct !DILexicalBlock(scope: !620, file: !1, line: 263, column: 17)
!629 = !DILocation(line: 263, column: 17, scope: !620)
!630 = !DILocation(line: 266, column: 17, scope: !631)
!631 = distinct !DILexicalBlock(scope: !628, file: !1, line: 264, column: 13)
!632 = !DILocation(line: 266, column: 29, scope: !631)
!633 = !DILocation(line: 267, column: 17, scope: !631)
!634 = !DILocation(line: 267, column: 29, scope: !631)
!635 = !DILocation(line: 268, column: 21, scope: !631)
!636 = !DILocation(line: 269, column: 13, scope: !631)
!637 = !DILocation(line: 273, column: 17, scope: !638)
!638 = distinct !DILexicalBlock(scope: !628, file: !1, line: 271, column: 13)
!639 = !DILocation(line: 273, column: 23, scope: !638)
!640 = !DILocation(line: 0, scope: !561)
!641 = !DILocation(line: 258, column: 44, scope: !617)
!642 = distinct !{!642, !618, !643}
!643 = !DILocation(line: 275, column: 9, scope: !614)
!644 = !DILocation(line: 279, column: 9, scope: !645)
!645 = distinct !DILexicalBlock(scope: !609, file: !1, line: 278, column: 5)
!646 = !DILocation(line: 282, column: 18, scope: !647)
!647 = distinct !DILexicalBlock(scope: !645, file: !1, line: 282, column: 9)
!648 = !DILocation(line: 282, column: 34, scope: !649)
!649 = distinct !DILexicalBlock(scope: !647, file: !1, line: 282, column: 9)
!650 = !DILocation(line: 282, column: 9, scope: !647)
!651 = !DILocation(line: 284, column: 22, scope: !652)
!652 = distinct !DILexicalBlock(scope: !649, file: !1, line: 283, column: 9)
!653 = !DILocation(line: 285, column: 17, scope: !652)
!654 = !DILocation(line: 285, column: 32, scope: !652)
!655 = !DILocation(line: 286, column: 19, scope: !652)
!656 = !DILocation(line: 287, column: 13, scope: !657)
!657 = distinct !DILexicalBlock(scope: !652, file: !1, line: 287, column: 13)
!658 = !DILocation(line: 288, column: 19, scope: !659)
!659 = distinct !DILexicalBlock(scope: !652, file: !1, line: 288, column: 17)
!660 = !DILocation(line: 288, column: 17, scope: !652)
!661 = !DILocation(line: 291, column: 17, scope: !662)
!662 = distinct !DILexicalBlock(scope: !659, file: !1, line: 289, column: 13)
!663 = !DILocation(line: 291, column: 29, scope: !662)
!664 = !DILocation(line: 292, column: 17, scope: !662)
!665 = !DILocation(line: 292, column: 29, scope: !662)
!666 = !DILocation(line: 293, column: 21, scope: !662)
!667 = !DILocation(line: 294, column: 13, scope: !662)
!668 = !DILocation(line: 298, column: 17, scope: !669)
!669 = distinct !DILexicalBlock(scope: !659, file: !1, line: 296, column: 13)
!670 = !DILocation(line: 298, column: 23, scope: !669)
!671 = !DILocation(line: 282, column: 44, scope: !649)
!672 = distinct !{!672, !650, !673}
!673 = !DILocation(line: 300, column: 9, scope: !647)
!674 = !DILocation(line: 249, column: 10, scope: !561)
!675 = !DILocation(line: 303, column: 5, scope: !561)
!676 = !DILocation(line: 304, column: 18, scope: !561)
!677 = !DILocation(line: 304, column: 5, scope: !561)
!678 = !DILocation(line: 304, column: 22, scope: !561)
!679 = !DILocation(line: 305, column: 1, scope: !561)
!680 = distinct !DISubprogram(name: "lsolve_numeric", scope: !1, file: !1, line: 317, type: !681, isLocal: true, isDefinition: true, scopeLine: 335, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !683)
!681 = !DISubroutineType(types: !682)
!682 = !{null, !6, !21, !6, !6, !7, !7, !6, !8}
!683 = !{!684, !685, !686, !687, !688, !689, !690, !691, !692, !693, !694, !695, !696, !697, !698, !699, !700}
!684 = !DILocalVariable(name: "Pinv", arg: 1, scope: !680, file: !1, line: 320, type: !6)
!685 = !DILocalVariable(name: "LU", arg: 2, scope: !680, file: !1, line: 322, type: !21)
!686 = !DILocalVariable(name: "Stack", arg: 3, scope: !680, file: !1, line: 323, type: !6)
!687 = !DILocalVariable(name: "Lip", arg: 4, scope: !680, file: !1, line: 324, type: !6)
!688 = !DILocalVariable(name: "top", arg: 5, scope: !680, file: !1, line: 325, type: !7)
!689 = !DILocalVariable(name: "n", arg: 6, scope: !680, file: !1, line: 326, type: !7)
!690 = !DILocalVariable(name: "Llen", arg: 7, scope: !680, file: !1, line: 327, type: !6)
!691 = !DILocalVariable(name: "X", arg: 8, scope: !680, file: !1, line: 330, type: !8)
!692 = !DILocalVariable(name: "xj", scope: !680, file: !1, line: 336, type: !5)
!693 = !DILocalVariable(name: "Lx", scope: !680, file: !1, line: 337, type: !8)
!694 = !DILocalVariable(name: "Li", scope: !680, file: !1, line: 338, type: !6)
!695 = !DILocalVariable(name: "p", scope: !680, file: !1, line: 339, type: !7)
!696 = !DILocalVariable(name: "s", scope: !680, file: !1, line: 339, type: !7)
!697 = !DILocalVariable(name: "j", scope: !680, file: !1, line: 339, type: !7)
!698 = !DILocalVariable(name: "jnew", scope: !680, file: !1, line: 339, type: !7)
!699 = !DILocalVariable(name: "len", scope: !680, file: !1, line: 339, type: !7)
!700 = !DILocalVariable(name: "xp", scope: !701, file: !1, line: 350, type: !21)
!701 = distinct !DILexicalBlock(scope: !702, file: !1, line: 350, column: 9)
!702 = distinct !DILexicalBlock(scope: !703, file: !1, line: 343, column: 5)
!703 = distinct !DILexicalBlock(scope: !704, file: !1, line: 342, column: 5)
!704 = distinct !DILexicalBlock(scope: !680, file: !1, line: 342, column: 5)
!705 = !DILocation(line: 320, column: 9, scope: !680)
!706 = !DILocation(line: 322, column: 11, scope: !680)
!707 = !DILocation(line: 323, column: 9, scope: !680)
!708 = !DILocation(line: 324, column: 9, scope: !680)
!709 = !DILocation(line: 325, column: 9, scope: !680)
!710 = !DILocation(line: 326, column: 9, scope: !680)
!711 = !DILocation(line: 327, column: 9, scope: !680)
!712 = !DILocation(line: 330, column: 11, scope: !680)
!713 = !DILocation(line: 339, column: 12, scope: !680)
!714 = !DILocation(line: 342, column: 22, scope: !703)
!715 = !DILocation(line: 342, column: 5, scope: !704)
!716 = !DILocation(line: 344, column: 9, scope: !702)
!717 = !DILocation(line: 346, column: 13, scope: !702)
!718 = !DILocation(line: 339, column: 15, scope: !680)
!719 = !DILocation(line: 347, column: 16, scope: !702)
!720 = !DILocation(line: 339, column: 18, scope: !680)
!721 = !DILocation(line: 349, column: 14, scope: !702)
!722 = !DILocation(line: 336, column: 11, scope: !680)
!723 = !DILocation(line: 350, column: 9, scope: !701)
!724 = !DILocation(line: 339, column: 24, scope: !680)
!725 = !DILocation(line: 338, column: 10, scope: !680)
!726 = !DILocation(line: 337, column: 12, scope: !680)
!727 = !DILocation(line: 339, column: 9, scope: !680)
!728 = !DILocation(line: 352, column: 24, scope: !729)
!729 = distinct !DILexicalBlock(scope: !730, file: !1, line: 352, column: 9)
!730 = distinct !DILexicalBlock(scope: !702, file: !1, line: 352, column: 9)
!731 = !DILocation(line: 352, column: 9, scope: !730)
!732 = !DILocation(line: 355, column: 13, scope: !733)
!733 = distinct !DILexicalBlock(scope: !734, file: !1, line: 355, column: 13)
!734 = distinct !DILexicalBlock(scope: !729, file: !1, line: 353, column: 9)
!735 = !DILocation(line: 352, column: 33, scope: !729)
!736 = distinct !{!736, !731, !737}
!737 = !DILocation(line: 356, column: 9, scope: !730)
!738 = !DILocation(line: 342, column: 29, scope: !703)
!739 = distinct !{!739, !715, !740}
!740 = !DILocation(line: 357, column: 5, scope: !704)
!741 = !DILocation(line: 358, column: 1, scope: !680)
!742 = distinct !DISubprogram(name: "lpivot", scope: !1, file: !1, line: 367, type: !743, isLocal: true, isDefinition: true, scopeLine: 387, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !745)
!743 = !DISubroutineType(types: !744)
!744 = !{!7, !7, !6, !8, !8, !5, !8, !21, !6, !6, !7, !7, !6, !6, !24}
!745 = !{!746, !747, !748, !749, !750, !751, !752, !753, !754, !755, !756, !757, !758, !759, !760, !761, !762, !763, !764, !765, !766, !767, !768, !769, !770, !771, !772, !773, !774, !776}
!746 = !DILocalVariable(name: "diagrow", arg: 1, scope: !742, file: !1, line: 369, type: !7)
!747 = !DILocalVariable(name: "p_pivrow", arg: 2, scope: !742, file: !1, line: 370, type: !6)
!748 = !DILocalVariable(name: "p_pivot", arg: 3, scope: !742, file: !1, line: 371, type: !8)
!749 = !DILocalVariable(name: "p_abs_pivot", arg: 4, scope: !742, file: !1, line: 372, type: !8)
!750 = !DILocalVariable(name: "tol", arg: 5, scope: !742, file: !1, line: 373, type: !5)
!751 = !DILocalVariable(name: "X", arg: 6, scope: !742, file: !1, line: 374, type: !8)
!752 = !DILocalVariable(name: "LU", arg: 7, scope: !742, file: !1, line: 375, type: !21)
!753 = !DILocalVariable(name: "Lip", arg: 8, scope: !742, file: !1, line: 376, type: !6)
!754 = !DILocalVariable(name: "Llen", arg: 9, scope: !742, file: !1, line: 377, type: !6)
!755 = !DILocalVariable(name: "k", arg: 10, scope: !742, file: !1, line: 378, type: !7)
!756 = !DILocalVariable(name: "n", arg: 11, scope: !742, file: !1, line: 379, type: !7)
!757 = !DILocalVariable(name: "Pinv", arg: 12, scope: !742, file: !1, line: 381, type: !6)
!758 = !DILocalVariable(name: "p_firstrow", arg: 13, scope: !742, file: !1, line: 384, type: !6)
!759 = !DILocalVariable(name: "Common", arg: 14, scope: !742, file: !1, line: 385, type: !24)
!760 = !DILocalVariable(name: "x", scope: !742, file: !1, line: 388, type: !5)
!761 = !DILocalVariable(name: "pivot", scope: !742, file: !1, line: 388, type: !5)
!762 = !DILocalVariable(name: "Lx", scope: !742, file: !1, line: 388, type: !8)
!763 = !DILocalVariable(name: "abs_pivot", scope: !742, file: !1, line: 389, type: !5)
!764 = !DILocalVariable(name: "xabs", scope: !742, file: !1, line: 389, type: !5)
!765 = !DILocalVariable(name: "p", scope: !742, file: !1, line: 390, type: !7)
!766 = !DILocalVariable(name: "i", scope: !742, file: !1, line: 390, type: !7)
!767 = !DILocalVariable(name: "ppivrow", scope: !742, file: !1, line: 390, type: !7)
!768 = !DILocalVariable(name: "pdiag", scope: !742, file: !1, line: 390, type: !7)
!769 = !DILocalVariable(name: "pivrow", scope: !742, file: !1, line: 390, type: !7)
!770 = !DILocalVariable(name: "Li", scope: !742, file: !1, line: 390, type: !6)
!771 = !DILocalVariable(name: "last_row_index", scope: !742, file: !1, line: 390, type: !7)
!772 = !DILocalVariable(name: "firstrow", scope: !742, file: !1, line: 390, type: !7)
!773 = !DILocalVariable(name: "len", scope: !742, file: !1, line: 390, type: !7)
!774 = !DILocalVariable(name: "xp", scope: !775, file: !1, line: 424, type: !21)
!775 = distinct !DILexicalBlock(scope: !742, file: !1, line: 424, column: 5)
!776 = !DILocalVariable(name: "xp", scope: !777, file: !1, line: 429, type: !21)
!777 = distinct !DILexicalBlock(scope: !742, file: !1, line: 429, column: 5)
!778 = !DILocation(line: 369, column: 9, scope: !742)
!779 = !DILocation(line: 370, column: 10, scope: !742)
!780 = !DILocation(line: 371, column: 12, scope: !742)
!781 = !DILocation(line: 372, column: 13, scope: !742)
!782 = !DILocation(line: 373, column: 12, scope: !742)
!783 = !DILocation(line: 374, column: 11, scope: !742)
!784 = !DILocation(line: 375, column: 11, scope: !742)
!785 = !DILocation(line: 376, column: 9, scope: !742)
!786 = !DILocation(line: 377, column: 9, scope: !742)
!787 = !DILocation(line: 378, column: 9, scope: !742)
!788 = !DILocation(line: 379, column: 9, scope: !742)
!789 = !DILocation(line: 381, column: 9, scope: !742)
!790 = !DILocation(line: 384, column: 10, scope: !742)
!791 = !DILocation(line: 385, column: 17, scope: !742)
!792 = !DILocation(line: 390, column: 31, scope: !742)
!793 = !DILocation(line: 393, column: 9, scope: !794)
!794 = distinct !DILexicalBlock(scope: !742, file: !1, line: 393, column: 9)
!795 = !DILocation(line: 393, column: 18, scope: !794)
!796 = !DILocation(line: 393, column: 9, scope: !742)
!797 = !DILocation(line: 396, column: 21, scope: !798)
!798 = distinct !DILexicalBlock(scope: !799, file: !1, line: 396, column: 13)
!799 = distinct !DILexicalBlock(scope: !794, file: !1, line: 394, column: 5)
!800 = !DILocation(line: 396, column: 13, scope: !798)
!801 = !DILocation(line: 396, column: 13, scope: !799)
!802 = !DILocation(line: 400, column: 25, scope: !803)
!803 = distinct !DILexicalBlock(scope: !799, file: !1, line: 400, column: 9)
!804 = !DILocation(line: 390, column: 60, scope: !742)
!805 = !DILocation(line: 400, column: 48, scope: !806)
!806 = distinct !DILexicalBlock(scope: !803, file: !1, line: 400, column: 9)
!807 = !DILocation(line: 400, column: 9, scope: !803)
!808 = !DILocation(line: 402, column: 13, scope: !809)
!809 = distinct !DILexicalBlock(scope: !810, file: !1, line: 402, column: 13)
!810 = distinct !DILexicalBlock(scope: !811, file: !1, line: 402, column: 13)
!811 = distinct !DILexicalBlock(scope: !806, file: !1, line: 401, column: 9)
!812 = !DILocation(line: 402, column: 13, scope: !810)
!813 = !DILocation(line: 402, column: 13, scope: !814)
!814 = distinct !DILexicalBlock(scope: !809, file: !1, line: 402, column: 13)
!815 = !DILocation(line: 403, column: 17, scope: !816)
!816 = distinct !DILexicalBlock(scope: !811, file: !1, line: 403, column: 17)
!817 = !DILocation(line: 403, column: 33, scope: !816)
!818 = !DILocation(line: 403, column: 17, scope: !811)
!819 = !DILocation(line: 407, column: 17, scope: !820)
!820 = distinct !DILexicalBlock(scope: !821, file: !1, line: 407, column: 17)
!821 = distinct !DILexicalBlock(scope: !822, file: !1, line: 407, column: 17)
!822 = distinct !DILexicalBlock(scope: !816, file: !1, line: 404, column: 13)
!823 = !DILocation(line: 407, column: 17, scope: !821)
!824 = !DILocation(line: 407, column: 17, scope: !825)
!825 = distinct !DILexicalBlock(scope: !820, file: !1, line: 407, column: 17)
!826 = !DILocation(line: 400, column: 62, scope: !806)
!827 = distinct !{!827, !807, !828}
!828 = !DILocation(line: 410, column: 9, scope: !803)
!829 = !DILocation(line: 413, column: 19, scope: !799)
!830 = !DILocation(line: 0, scope: !742)
!831 = !DILocation(line: 388, column: 14, scope: !742)
!832 = !DILocation(line: 414, column: 18, scope: !799)
!833 = !DILocation(line: 415, column: 22, scope: !799)
!834 = !DILocation(line: 416, column: 21, scope: !799)
!835 = !DILocation(line: 417, column: 9, scope: !799)
!836 = !DILocation(line: 390, column: 24, scope: !742)
!837 = !DILocation(line: 390, column: 15, scope: !742)
!838 = !DILocation(line: 389, column: 12, scope: !742)
!839 = !DILocation(line: 423, column: 18, scope: !742)
!840 = !DILocation(line: 390, column: 12, scope: !742)
!841 = !DILocation(line: 424, column: 5, scope: !775)
!842 = !DILocation(line: 390, column: 70, scope: !742)
!843 = !DILocation(line: 390, column: 40, scope: !742)
!844 = !DILocation(line: 425, column: 22, scope: !742)
!845 = !DILocation(line: 390, column: 44, scope: !742)
!846 = !DILocation(line: 428, column: 14, scope: !742)
!847 = !DILocation(line: 429, column: 5, scope: !777)
!848 = !DILocation(line: 388, column: 22, scope: !742)
!849 = !DILocation(line: 390, column: 9, scope: !742)
!850 = !DILocation(line: 432, column: 20, scope: !851)
!851 = distinct !DILexicalBlock(scope: !852, file: !1, line: 432, column: 5)
!852 = distinct !DILexicalBlock(scope: !742, file: !1, line: 432, column: 5)
!853 = !DILocation(line: 432, column: 5, scope: !852)
!854 = !DILocation(line: 435, column: 13, scope: !855)
!855 = distinct !DILexicalBlock(scope: !851, file: !1, line: 433, column: 5)
!856 = !DILocation(line: 436, column: 13, scope: !855)
!857 = !DILocation(line: 388, column: 11, scope: !742)
!858 = !DILocation(line: 437, column: 9, scope: !859)
!859 = distinct !DILexicalBlock(scope: !855, file: !1, line: 437, column: 9)
!860 = !DILocation(line: 439, column: 9, scope: !855)
!861 = !DILocation(line: 439, column: 16, scope: !855)
!862 = !DILocation(line: 441, column: 9, scope: !863)
!863 = distinct !DILexicalBlock(scope: !855, file: !1, line: 441, column: 9)
!864 = !DILocation(line: 389, column: 23, scope: !742)
!865 = !DILocation(line: 444, column: 15, scope: !866)
!866 = distinct !DILexicalBlock(scope: !855, file: !1, line: 444, column: 13)
!867 = !DILocation(line: 444, column: 13, scope: !855)
!868 = !DILocation(line: 450, column: 18, scope: !869)
!869 = distinct !DILexicalBlock(scope: !855, file: !1, line: 450, column: 13)
!870 = !DILocation(line: 450, column: 13, scope: !855)
!871 = !DILocation(line: 432, column: 29, scope: !851)
!872 = distinct !{!872, !853, !873}
!873 = !DILocation(line: 455, column: 5, scope: !852)
!874 = !DILocation(line: 458, column: 5, scope: !875)
!875 = distinct !DILexicalBlock(scope: !742, file: !1, line: 458, column: 5)
!876 = !DILocation(line: 459, column: 14, scope: !877)
!877 = distinct !DILexicalBlock(scope: !742, file: !1, line: 459, column: 9)
!878 = !DILocation(line: 459, column: 9, scope: !742)
!879 = !DILocation(line: 466, column: 24, scope: !880)
!880 = distinct !DILexicalBlock(scope: !742, file: !1, line: 466, column: 9)
!881 = !DILocation(line: 466, column: 9, scope: !742)
!882 = !DILocation(line: 468, column: 25, scope: !883)
!883 = distinct !DILexicalBlock(scope: !884, file: !1, line: 468, column: 13)
!884 = distinct !DILexicalBlock(scope: !880, file: !1, line: 467, column: 5)
!885 = !DILocation(line: 468, column: 18, scope: !883)
!886 = !DILocation(line: 468, column: 13, scope: !884)
!887 = !DILocation(line: 474, column: 20, scope: !888)
!888 = distinct !DILexicalBlock(scope: !880, file: !1, line: 474, column: 14)
!889 = !DILocation(line: 474, column: 14, scope: !880)
!890 = !DILocation(line: 477, column: 9, scope: !891)
!891 = distinct !DILexicalBlock(scope: !892, file: !1, line: 477, column: 9)
!892 = distinct !DILexicalBlock(scope: !888, file: !1, line: 475, column: 5)
!893 = !DILocation(line: 478, column: 25, scope: !894)
!894 = distinct !DILexicalBlock(scope: !892, file: !1, line: 478, column: 13)
!895 = !DILocation(line: 478, column: 18, scope: !894)
!896 = !DILocation(line: 478, column: 13, scope: !892)
!897 = !DILocation(line: 486, column: 17, scope: !898)
!898 = distinct !DILexicalBlock(scope: !742, file: !1, line: 486, column: 9)
!899 = !DILocation(line: 486, column: 9, scope: !742)
!900 = !DILocation(line: 488, column: 18, scope: !901)
!901 = distinct !DILexicalBlock(scope: !898, file: !1, line: 487, column: 5)
!902 = !DILocation(line: 489, column: 18, scope: !901)
!903 = !DILocation(line: 491, column: 22, scope: !901)
!904 = !DILocation(line: 492, column: 24, scope: !901)
!905 = !DILocation(line: 492, column: 22, scope: !901)
!906 = !DILocation(line: 493, column: 5, scope: !901)
!907 = !DILocation(line: 0, scope: !908)
!908 = distinct !DILexicalBlock(scope: !898, file: !1, line: 495, column: 5)
!909 = !DILocation(line: 499, column: 5, scope: !910)
!910 = distinct !DILexicalBlock(scope: !742, file: !1, line: 499, column: 5)
!911 = !DILocation(line: 501, column: 15, scope: !742)
!912 = !DILocation(line: 502, column: 14, scope: !742)
!913 = !DILocation(line: 503, column: 18, scope: !742)
!914 = !DILocation(line: 506, column: 9, scope: !915)
!915 = distinct !DILexicalBlock(scope: !742, file: !1, line: 506, column: 9)
!916 = !DILocation(line: 506, column: 25, scope: !915)
!917 = !DILocation(line: 506, column: 36, scope: !915)
!918 = !DILocation(line: 506, column: 28, scope: !915)
!919 = !DILocation(line: 506, column: 9, scope: !742)
!920 = !DILocation(line: 513, column: 22, scope: !921)
!921 = distinct !DILexicalBlock(scope: !922, file: !1, line: 513, column: 5)
!922 = distinct !DILexicalBlock(scope: !742, file: !1, line: 513, column: 5)
!923 = !DILocation(line: 513, column: 20, scope: !921)
!924 = !DILocation(line: 513, column: 5, scope: !922)
!925 = !DILocation(line: 516, column: 9, scope: !926)
!926 = distinct !DILexicalBlock(scope: !927, file: !1, line: 516, column: 9)
!927 = distinct !DILexicalBlock(scope: !921, file: !1, line: 514, column: 5)
!928 = !DILocation(line: 513, column: 34, scope: !921)
!929 = distinct !{!929, !924, !930}
!930 = !DILocation(line: 517, column: 5, scope: !922)
!931 = !DILocation(line: 520, column: 1, scope: !742)
!932 = distinct !DISubprogram(name: "prune", scope: !1, file: !1, line: 528, type: !933, isLocal: true, isDefinition: true, scopeLine: 548, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !935)
!933 = !DISubroutineType(types: !934)
!934 = !{null, !6, !6, !7, !7, !21, !6, !6, !6, !6}
!935 = !{!936, !937, !938, !939, !940, !941, !942, !943, !944, !945, !946, !947, !948, !949, !950, !951, !952, !953, !954, !955, !956, !957, !958, !960}
!936 = !DILocalVariable(name: "Lpend", arg: 1, scope: !932, file: !1, line: 531, type: !6)
!937 = !DILocalVariable(name: "Pinv", arg: 2, scope: !932, file: !1, line: 534, type: !6)
!938 = !DILocalVariable(name: "k", arg: 3, scope: !932, file: !1, line: 536, type: !7)
!939 = !DILocalVariable(name: "pivrow", arg: 4, scope: !932, file: !1, line: 537, type: !7)
!940 = !DILocalVariable(name: "LU", arg: 5, scope: !932, file: !1, line: 540, type: !21)
!941 = !DILocalVariable(name: "Uip", arg: 6, scope: !932, file: !1, line: 543, type: !6)
!942 = !DILocalVariable(name: "Lip", arg: 7, scope: !932, file: !1, line: 544, type: !6)
!943 = !DILocalVariable(name: "Ulen", arg: 8, scope: !932, file: !1, line: 545, type: !6)
!944 = !DILocalVariable(name: "Llen", arg: 9, scope: !932, file: !1, line: 546, type: !6)
!945 = !DILocalVariable(name: "x", scope: !932, file: !1, line: 549, type: !5)
!946 = !DILocalVariable(name: "Lx", scope: !932, file: !1, line: 550, type: !8)
!947 = !DILocalVariable(name: "Ux", scope: !932, file: !1, line: 550, type: !8)
!948 = !DILocalVariable(name: "Li", scope: !932, file: !1, line: 551, type: !6)
!949 = !DILocalVariable(name: "Ui", scope: !932, file: !1, line: 551, type: !6)
!950 = !DILocalVariable(name: "p", scope: !932, file: !1, line: 552, type: !7)
!951 = !DILocalVariable(name: "i", scope: !932, file: !1, line: 552, type: !7)
!952 = !DILocalVariable(name: "j", scope: !932, file: !1, line: 552, type: !7)
!953 = !DILocalVariable(name: "p2", scope: !932, file: !1, line: 552, type: !7)
!954 = !DILocalVariable(name: "phead", scope: !932, file: !1, line: 552, type: !7)
!955 = !DILocalVariable(name: "ptail", scope: !932, file: !1, line: 552, type: !7)
!956 = !DILocalVariable(name: "llen", scope: !932, file: !1, line: 552, type: !7)
!957 = !DILocalVariable(name: "ulen", scope: !932, file: !1, line: 552, type: !7)
!958 = !DILocalVariable(name: "xp", scope: !959, file: !1, line: 556, type: !21)
!959 = distinct !DILexicalBlock(scope: !932, file: !1, line: 556, column: 5)
!960 = !DILocalVariable(name: "xp", scope: !961, file: !1, line: 566, type: !21)
!961 = distinct !DILexicalBlock(scope: !962, file: !1, line: 566, column: 13)
!962 = distinct !DILexicalBlock(scope: !963, file: !1, line: 564, column: 9)
!963 = distinct !DILexicalBlock(scope: !964, file: !1, line: 563, column: 13)
!964 = distinct !DILexicalBlock(scope: !965, file: !1, line: 558, column: 5)
!965 = distinct !DILexicalBlock(scope: !966, file: !1, line: 557, column: 5)
!966 = distinct !DILexicalBlock(scope: !932, file: !1, line: 557, column: 5)
!967 = !DILocation(line: 531, column: 9, scope: !932)
!968 = !DILocation(line: 534, column: 9, scope: !932)
!969 = !DILocation(line: 536, column: 9, scope: !932)
!970 = !DILocation(line: 537, column: 9, scope: !932)
!971 = !DILocation(line: 540, column: 11, scope: !932)
!972 = !DILocation(line: 543, column: 9, scope: !932)
!973 = !DILocation(line: 544, column: 9, scope: !932)
!974 = !DILocation(line: 545, column: 9, scope: !932)
!975 = !DILocation(line: 546, column: 9, scope: !932)
!976 = !DILocation(line: 556, column: 5, scope: !959)
!977 = !DILocation(line: 552, column: 42, scope: !932)
!978 = !DILocation(line: 551, column: 15, scope: !932)
!979 = !DILocation(line: 552, column: 9, scope: !932)
!980 = !DILocation(line: 557, column: 20, scope: !965)
!981 = !DILocation(line: 557, column: 5, scope: !966)
!982 = !DILocation(line: 559, column: 13, scope: !964)
!983 = !DILocation(line: 552, column: 15, scope: !932)
!984 = !DILocation(line: 561, column: 9, scope: !985)
!985 = distinct !DILexicalBlock(scope: !986, file: !1, line: 561, column: 9)
!986 = distinct !DILexicalBlock(scope: !964, file: !1, line: 561, column: 9)
!987 = !DILocation(line: 561, column: 9, scope: !986)
!988 = !DILocation(line: 561, column: 9, scope: !989)
!989 = distinct !DILexicalBlock(scope: !985, file: !1, line: 561, column: 9)
!990 = !DILocation(line: 563, column: 13, scope: !963)
!991 = !DILocation(line: 563, column: 23, scope: !963)
!992 = !DILocation(line: 563, column: 13, scope: !964)
!993 = !DILocation(line: 566, column: 13, scope: !961)
!994 = !DILocation(line: 552, column: 36, scope: !932)
!995 = !DILocation(line: 551, column: 10, scope: !932)
!996 = !DILocation(line: 550, column: 12, scope: !932)
!997 = !DILocation(line: 552, column: 18, scope: !932)
!998 = !DILocation(line: 567, column: 30, scope: !999)
!999 = distinct !DILexicalBlock(scope: !1000, file: !1, line: 567, column: 13)
!1000 = distinct !DILexicalBlock(scope: !962, file: !1, line: 567, column: 13)
!1001 = !DILocation(line: 567, column: 13, scope: !1000)
!1002 = distinct !{!1002, !1001, !1003}
!1003 = !DILocation(line: 630, column: 13, scope: !1000)
!1004 = !DILocation(line: 569, column: 31, scope: !1005)
!1005 = distinct !DILexicalBlock(scope: !1006, file: !1, line: 569, column: 21)
!1006 = distinct !DILexicalBlock(scope: !999, file: !1, line: 568, column: 13)
!1007 = !DILocation(line: 569, column: 28, scope: !1005)
!1008 = !DILocation(line: 567, column: 41, scope: !999)
!1009 = !DILocation(line: 569, column: 21, scope: !1006)
!1010 = !DILocation(line: 552, column: 22, scope: !932)
!1011 = !DILocation(line: 552, column: 29, scope: !932)
!1012 = !DILocation(line: 588, column: 34, scope: !1013)
!1013 = distinct !DILexicalBlock(scope: !1005, file: !1, line: 570, column: 17)
!1014 = !DILocation(line: 588, column: 21, scope: !1013)
!1015 = !DILocation(line: 590, column: 29, scope: !1016)
!1016 = distinct !DILexicalBlock(scope: !1013, file: !1, line: 589, column: 21)
!1017 = !DILocation(line: 552, column: 12, scope: !932)
!1018 = !DILocation(line: 591, column: 29, scope: !1019)
!1019 = distinct !DILexicalBlock(scope: !1016, file: !1, line: 591, column: 29)
!1020 = !DILocation(line: 591, column: 38, scope: !1019)
!1021 = !DILocation(line: 591, column: 29, scope: !1016)
!1022 = !DILocation(line: 594, column: 34, scope: !1023)
!1023 = distinct !DILexicalBlock(scope: !1019, file: !1, line: 592, column: 25)
!1024 = !DILocation(line: 595, column: 25, scope: !1023)
!1025 = !DILocation(line: 599, column: 34, scope: !1026)
!1026 = distinct !DILexicalBlock(scope: !1019, file: !1, line: 597, column: 25)
!1027 = !DILocation(line: 600, column: 42, scope: !1026)
!1028 = !DILocation(line: 600, column: 40, scope: !1026)
!1029 = !DILocation(line: 601, column: 40, scope: !1026)
!1030 = !DILocation(line: 602, column: 33, scope: !1026)
!1031 = !DILocation(line: 549, column: 11, scope: !932)
!1032 = !DILocation(line: 603, column: 42, scope: !1026)
!1033 = !DILocation(line: 603, column: 40, scope: !1026)
!1034 = !DILocation(line: 604, column: 40, scope: !1026)
!1035 = !DILocation(line: 0, scope: !1026)
!1036 = !DILocation(line: 0, scope: !1013)
!1037 = distinct !{!1037, !1014, !1038}
!1038 = !DILocation(line: 606, column: 21, scope: !1013)
!1039 = !DILocation(line: 614, column: 31, scope: !1013)
!1040 = !DILocation(line: 628, column: 21, scope: !1013)
!1041 = !DILocation(line: 557, column: 30, scope: !965)
!1042 = distinct !{!1042, !981, !1043}
!1043 = !DILocation(line: 632, column: 5, scope: !966)
!1044 = !DILocation(line: 633, column: 1, scope: !932)
!1045 = distinct !DISubprogram(name: "dfs", scope: !1, file: !1, line: 18, type: !1046, isLocal: true, isDefinition: true, scopeLine: 42, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !1048)
!1046 = !DISubroutineType(types: !1047)
!1047 = !{!7, !7, !7, !6, !6, !6, !6, !6, !6, !7, !21, !6, !6, !6}
!1048 = !{!1049, !1050, !1051, !1052, !1053, !1054, !1055, !1056, !1057, !1058, !1059, !1060, !1061, !1062, !1063, !1064, !1065, !1066, !1067}
!1049 = !DILocalVariable(name: "j", arg: 1, scope: !1045, file: !1, line: 21, type: !7)
!1050 = !DILocalVariable(name: "k", arg: 2, scope: !1045, file: !1, line: 22, type: !7)
!1051 = !DILocalVariable(name: "Pinv", arg: 3, scope: !1045, file: !1, line: 23, type: !6)
!1052 = !DILocalVariable(name: "Llen", arg: 4, scope: !1045, file: !1, line: 25, type: !6)
!1053 = !DILocalVariable(name: "Lip", arg: 5, scope: !1045, file: !1, line: 26, type: !6)
!1054 = !DILocalVariable(name: "Stack", arg: 6, scope: !1045, file: !1, line: 29, type: !6)
!1055 = !DILocalVariable(name: "Flag", arg: 7, scope: !1045, file: !1, line: 32, type: !6)
!1056 = !DILocalVariable(name: "Lpend", arg: 8, scope: !1045, file: !1, line: 33, type: !6)
!1057 = !DILocalVariable(name: "top", arg: 9, scope: !1045, file: !1, line: 34, type: !7)
!1058 = !DILocalVariable(name: "LU", arg: 10, scope: !1045, file: !1, line: 35, type: !21)
!1059 = !DILocalVariable(name: "Lik", arg: 11, scope: !1045, file: !1, line: 36, type: !6)
!1060 = !DILocalVariable(name: "plength", arg: 12, scope: !1045, file: !1, line: 37, type: !6)
!1061 = !DILocalVariable(name: "Ap_pos", arg: 13, scope: !1045, file: !1, line: 40, type: !6)
!1062 = !DILocalVariable(name: "i", scope: !1045, file: !1, line: 43, type: !7)
!1063 = !DILocalVariable(name: "pos", scope: !1045, file: !1, line: 43, type: !7)
!1064 = !DILocalVariable(name: "jnew", scope: !1045, file: !1, line: 43, type: !7)
!1065 = !DILocalVariable(name: "head", scope: !1045, file: !1, line: 43, type: !7)
!1066 = !DILocalVariable(name: "l_length", scope: !1045, file: !1, line: 43, type: !7)
!1067 = !DILocalVariable(name: "Li", scope: !1045, file: !1, line: 44, type: !6)
!1068 = !DILocation(line: 21, column: 9, scope: !1045)
!1069 = !DILocation(line: 22, column: 9, scope: !1045)
!1070 = !DILocation(line: 23, column: 9, scope: !1045)
!1071 = !DILocation(line: 25, column: 9, scope: !1045)
!1072 = !DILocation(line: 26, column: 9, scope: !1045)
!1073 = !DILocation(line: 29, column: 9, scope: !1045)
!1074 = !DILocation(line: 32, column: 9, scope: !1045)
!1075 = !DILocation(line: 33, column: 9, scope: !1045)
!1076 = !DILocation(line: 34, column: 9, scope: !1045)
!1077 = !DILocation(line: 35, column: 10, scope: !1045)
!1078 = !DILocation(line: 36, column: 10, scope: !1045)
!1079 = !DILocation(line: 37, column: 10, scope: !1045)
!1080 = !DILocation(line: 40, column: 9, scope: !1045)
!1081 = !DILocation(line: 46, column: 16, scope: !1045)
!1082 = !DILocation(line: 43, column: 29, scope: !1045)
!1083 = !DILocation(line: 43, column: 23, scope: !1045)
!1084 = !DILocation(line: 49, column: 15, scope: !1045)
!1085 = !DILocation(line: 52, column: 5, scope: !1045)
!1086 = !DILocation(line: 54, column: 9, scope: !1087)
!1087 = distinct !DILexicalBlock(scope: !1045, file: !1, line: 53, column: 5)
!1088 = !DILocation(line: 55, column: 13, scope: !1087)
!1089 = !DILocation(line: 56, column: 16, scope: !1087)
!1090 = !DILocation(line: 43, column: 17, scope: !1045)
!1091 = !DILocation(line: 59, column: 13, scope: !1092)
!1092 = distinct !DILexicalBlock(scope: !1087, file: !1, line: 59, column: 13)
!1093 = !DILocation(line: 59, column: 22, scope: !1092)
!1094 = !DILocation(line: 59, column: 13, scope: !1087)
!1095 = !DILocation(line: 62, column: 22, scope: !1096)
!1096 = distinct !DILexicalBlock(scope: !1092, file: !1, line: 60, column: 9)
!1097 = !DILocation(line: 63, column: 13, scope: !1098)
!1098 = distinct !DILexicalBlock(scope: !1099, file: !1, line: 63, column: 13)
!1099 = distinct !DILexicalBlock(scope: !1096, file: !1, line: 63, column: 13)
!1100 = !DILocation(line: 63, column: 13, scope: !1099)
!1101 = !DILocation(line: 63, column: 13, scope: !1102)
!1102 = distinct !DILexicalBlock(scope: !1098, file: !1, line: 63, column: 13)
!1103 = !DILocation(line: 66, column: 18, scope: !1096)
!1104 = !DILocation(line: 66, column: 31, scope: !1096)
!1105 = !DILocation(line: 66, column: 17, scope: !1096)
!1106 = !DILocation(line: 66, column: 44, scope: !1096)
!1107 = !DILocation(line: 65, column: 13, scope: !1096)
!1108 = !DILocation(line: 65, column: 27, scope: !1096)
!1109 = !DILocation(line: 67, column: 9, scope: !1096)
!1110 = !DILocation(line: 71, column: 28, scope: !1087)
!1111 = !DILocation(line: 71, column: 26, scope: !1087)
!1112 = !DILocation(line: 71, column: 14, scope: !1087)
!1113 = !DILocation(line: 44, column: 10, scope: !1045)
!1114 = !DILocation(line: 72, column: 22, scope: !1115)
!1115 = distinct !DILexicalBlock(scope: !1087, file: !1, line: 72, column: 9)
!1116 = !DILocation(line: 72, column: 20, scope: !1115)
!1117 = !DILocation(line: 43, column: 12, scope: !1045)
!1118 = !DILocation(line: 72, column: 42, scope: !1119)
!1119 = distinct !DILexicalBlock(scope: !1115, file: !1, line: 72, column: 9)
!1120 = !DILocation(line: 72, column: 9, scope: !1115)
!1121 = !DILocation(line: 74, column: 17, scope: !1122)
!1122 = distinct !DILexicalBlock(scope: !1119, file: !1, line: 73, column: 9)
!1123 = !DILocation(line: 43, column: 9, scope: !1045)
!1124 = !DILocation(line: 75, column: 17, scope: !1125)
!1125 = distinct !DILexicalBlock(scope: !1122, file: !1, line: 75, column: 17)
!1126 = !DILocation(line: 75, column: 26, scope: !1125)
!1127 = !DILocation(line: 75, column: 17, scope: !1122)
!1128 = !DILocation(line: 78, column: 21, scope: !1129)
!1129 = distinct !DILexicalBlock(scope: !1130, file: !1, line: 78, column: 21)
!1130 = distinct !DILexicalBlock(scope: !1125, file: !1, line: 76, column: 13)
!1131 = !DILocation(line: 78, column: 30, scope: !1129)
!1132 = !DILocation(line: 78, column: 21, scope: !1130)
!1133 = !DILocation(line: 83, column: 35, scope: !1134)
!1134 = distinct !DILexicalBlock(scope: !1129, file: !1, line: 79, column: 17)
!1135 = !DILocation(line: 87, column: 28, scope: !1134)
!1136 = !DILocation(line: 87, column: 21, scope: !1134)
!1137 = !DILocation(line: 87, column: 36, scope: !1134)
!1138 = !DILocation(line: 102, column: 13, scope: !1087)
!1139 = !DILocation(line: 95, column: 30, scope: !1140)
!1140 = distinct !DILexicalBlock(scope: !1129, file: !1, line: 91, column: 17)
!1141 = !DILocation(line: 96, column: 21, scope: !1140)
!1142 = !DILocation(line: 96, column: 36, scope: !1140)
!1143 = !DILocation(line: 97, column: 29, scope: !1140)
!1144 = !DILocation(line: 99, column: 13, scope: !1130)
!1145 = !DILocation(line: 0, scope: !1045)
!1146 = !DILocation(line: 72, column: 49, scope: !1119)
!1147 = distinct !{!1147, !1120, !1148}
!1148 = !DILocation(line: 100, column: 9, scope: !1115)
!1149 = !DILocation(line: 102, column: 17, scope: !1150)
!1150 = distinct !DILexicalBlock(scope: !1087, file: !1, line: 102, column: 13)
!1151 = !DILocation(line: 106, column: 17, scope: !1152)
!1152 = distinct !DILexicalBlock(scope: !1150, file: !1, line: 103, column: 9)
!1153 = !DILocation(line: 107, column: 19, scope: !1152)
!1154 = !DILocation(line: 107, column: 13, scope: !1152)
!1155 = !DILocation(line: 107, column: 26, scope: !1152)
!1156 = !DILocation(line: 108, column: 13, scope: !1157)
!1157 = distinct !DILexicalBlock(scope: !1158, file: !1, line: 108, column: 13)
!1158 = distinct !DILexicalBlock(scope: !1152, file: !1, line: 108, column: 13)
!1159 = !DILocation(line: 108, column: 13, scope: !1158)
!1160 = !DILocation(line: 108, column: 13, scope: !1161)
!1161 = distinct !DILexicalBlock(scope: !1157, file: !1, line: 108, column: 13)
!1162 = !DILocation(line: 0, scope: !1134)
!1163 = !DILocation(line: 52, column: 17, scope: !1045)
!1164 = distinct !{!1164, !1085, !1165}
!1165 = !DILocation(line: 110, column: 5, scope: !1045)
!1166 = !DILocation(line: 112, column: 5, scope: !1045)
!1167 = !DILocation(line: 113, column: 14, scope: !1045)
!1168 = !DILocation(line: 114, column: 5, scope: !1045)
