; ModuleID = 'SuiteSparse_config.c'
source_filename = "SuiteSparse_config.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }

@SuiteSparse_config = local_unnamed_addr global %struct.SuiteSparse_config_struct { i8* (i64)* @malloc, i8* (i64, i64)* @calloc, i8* (i8*, i64)* @realloc, void (i8*)* @free, i32 (i8*, ...)* @printf, double (double, double)* @SuiteSparse_hypot, i32 (double, double, double, double, double*, double*)* @SuiteSparse_divcomplex }, align 8, !dbg !0

; Function Attrs: nounwind allocsize(0)
declare noalias i8* @malloc(i64) #0

; Function Attrs: nounwind allocsize(0,1)
declare noalias i8* @calloc(i64, i64) #1

; Function Attrs: nounwind allocsize(1)
declare noalias i8* @realloc(i8* nocapture, i64) #2

; Function Attrs: nounwind
declare void @free(i8* nocapture) #3

; Function Attrs: nounwind
declare i32 @printf(i8* nocapture readonly, ...) #3

; Function Attrs: nounwind readnone ssp uwtable
define double @SuiteSparse_hypot(double, double) #4 !dbg !55 {
  call void @llvm.dbg.value(metadata double %0, metadata !57, metadata !DIExpression()), !dbg !61
  call void @llvm.dbg.value(metadata double %1, metadata !58, metadata !DIExpression()), !dbg !62
  %3 = tail call double @llvm.fabs.f64(double %0), !dbg !63
  call void @llvm.dbg.value(metadata double %3, metadata !57, metadata !DIExpression()), !dbg !61
  %4 = tail call double @llvm.fabs.f64(double %1), !dbg !64
  call void @llvm.dbg.value(metadata double %4, metadata !58, metadata !DIExpression()), !dbg !62
  %5 = fcmp ult double %3, %4, !dbg !65
  %6 = fadd double %3, %4, !dbg !67
  br i1 %5, label %15, label %7, !dbg !70

; <label>:7:                                      ; preds = %2
  %8 = fcmp oeq double %6, %3, !dbg !71
  br i1 %8, label %23, label %9, !dbg !72

; <label>:9:                                      ; preds = %7
  %10 = fdiv double %4, %3, !dbg !73
  call void @llvm.dbg.value(metadata double %10, metadata !60, metadata !DIExpression()), !dbg !75
  %11 = fmul double %10, %10, !dbg !76
  %12 = fadd double %11, 1.000000e+00, !dbg !77
  %13 = tail call double @llvm.sqrt.f64(double %12), !dbg !78
  %14 = fmul double %3, %13, !dbg !79
  call void @llvm.dbg.value(metadata double %14, metadata !59, metadata !DIExpression()), !dbg !80
  br label %23

; <label>:15:                                     ; preds = %2
  %16 = fcmp oeq double %6, %4, !dbg !81
  br i1 %16, label %23, label %17, !dbg !84

; <label>:17:                                     ; preds = %15
  %18 = fdiv double %3, %4, !dbg !85
  call void @llvm.dbg.value(metadata double %18, metadata !60, metadata !DIExpression()), !dbg !75
  %19 = fmul double %18, %18, !dbg !87
  %20 = fadd double %19, 1.000000e+00, !dbg !88
  %21 = tail call double @llvm.sqrt.f64(double %20), !dbg !89
  %22 = fmul double %4, %21, !dbg !90
  call void @llvm.dbg.value(metadata double %22, metadata !59, metadata !DIExpression()), !dbg !80
  br label %23

; <label>:23:                                     ; preds = %15, %7, %17, %9
  %24 = phi double [ %14, %9 ], [ %22, %17 ], [ %3, %7 ], [ %4, %15 ], !dbg !91
  call void @llvm.dbg.value(metadata double %24, metadata !59, metadata !DIExpression()), !dbg !80
  ret double %24, !dbg !92
}

; Function Attrs: nounwind ssp uwtable
define i32 @SuiteSparse_divcomplex(double, double, double, double, double* nocapture, double* nocapture) #5 !dbg !93 {
  call void @llvm.dbg.value(metadata double %0, metadata !95, metadata !DIExpression()), !dbg !105
  call void @llvm.dbg.value(metadata double %1, metadata !96, metadata !DIExpression()), !dbg !106
  call void @llvm.dbg.value(metadata double %2, metadata !97, metadata !DIExpression()), !dbg !107
  call void @llvm.dbg.value(metadata double %3, metadata !98, metadata !DIExpression()), !dbg !108
  call void @llvm.dbg.value(metadata double* %4, metadata !99, metadata !DIExpression()), !dbg !109
  call void @llvm.dbg.value(metadata double* %5, metadata !100, metadata !DIExpression()), !dbg !110
  %7 = tail call double @llvm.fabs.f64(double %2), !dbg !111
  %8 = tail call double @llvm.fabs.f64(double %3), !dbg !113
  %9 = fcmp ult double %7, %8, !dbg !114
  br i1 %9, label %18, label %10, !dbg !115

; <label>:10:                                     ; preds = %6
  %11 = fdiv double %3, %2, !dbg !116
  call void @llvm.dbg.value(metadata double %11, metadata !103, metadata !DIExpression()), !dbg !118
  %12 = fmul double %11, %3, !dbg !119
  %13 = fadd double %12, %2, !dbg !120
  call void @llvm.dbg.value(metadata double %13, metadata !104, metadata !DIExpression()), !dbg !121
  %14 = fmul double %11, %1, !dbg !122
  %15 = fadd double %14, %0, !dbg !123
  %16 = fdiv double %15, %13, !dbg !124
  call void @llvm.dbg.value(metadata double %16, metadata !101, metadata !DIExpression()), !dbg !125
  %17 = fmul double %11, %0, !dbg !126
  br label %26, !dbg !127

; <label>:18:                                     ; preds = %6
  %19 = fdiv double %2, %3, !dbg !128
  call void @llvm.dbg.value(metadata double %19, metadata !103, metadata !DIExpression()), !dbg !118
  %20 = fmul double %19, %2, !dbg !130
  %21 = fadd double %20, %3, !dbg !131
  call void @llvm.dbg.value(metadata double %21, metadata !104, metadata !DIExpression()), !dbg !121
  %22 = fmul double %19, %0, !dbg !132
  %23 = fadd double %22, %1, !dbg !133
  %24 = fdiv double %23, %21, !dbg !134
  call void @llvm.dbg.value(metadata double %24, metadata !101, metadata !DIExpression()), !dbg !125
  %25 = fmul double %19, %1, !dbg !135
  call void @llvm.dbg.value(metadata double %32, metadata !102, metadata !DIExpression()), !dbg !136
  br label %26

; <label>:26:                                     ; preds = %18, %10
  %27 = phi double [ %0, %18 ], [ %17, %10 ]
  %28 = phi double [ %25, %18 ], [ %1, %10 ]
  %29 = phi double [ %21, %18 ], [ %13, %10 ]
  %30 = phi double [ %24, %18 ], [ %16, %10 ], !dbg !137
  %31 = fsub double %28, %27, !dbg !138
  %32 = fdiv double %31, %29, !dbg !138
  call void @llvm.dbg.value(metadata double %29, metadata !104, metadata !DIExpression()), !dbg !121
  call void @llvm.dbg.value(metadata double %32, metadata !102, metadata !DIExpression()), !dbg !136
  call void @llvm.dbg.value(metadata double %30, metadata !101, metadata !DIExpression()), !dbg !125
  store double %30, double* %4, align 8, !dbg !139, !tbaa !140
  store double %32, double* %5, align 8, !dbg !144, !tbaa !140
  %33 = fcmp oeq double %29, 0.000000e+00, !dbg !145
  %34 = zext i1 %33 to i32, !dbg !145
  ret i32 %34, !dbg !146
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @SuiteSparse_start() local_unnamed_addr #6 !dbg !147 {
  store i8* (i64)* @malloc, i8* (i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 0), align 8, !dbg !150, !tbaa !151
  store i8* (i64, i64)* @calloc, i8* (i64, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 1), align 8, !dbg !154, !tbaa !155
  store i8* (i8*, i64)* @realloc, i8* (i8*, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 2), align 8, !dbg !156, !tbaa !157
  store void (i8*)* @free, void (i8*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 3), align 8, !dbg !158, !tbaa !159
  store i32 (i8*, ...)* @printf, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !dbg !160, !tbaa !161
  store double (double, double)* @SuiteSparse_hypot, double (double, double)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 5), align 8, !dbg !162, !tbaa !163
  store i32 (double, double, double, double, double*, double*)* @SuiteSparse_divcomplex, i32 (double, double, double, double, double*, double*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 6), align 8, !dbg !164, !tbaa !165
  ret void, !dbg !166
}

; Function Attrs: norecurse nounwind readnone ssp uwtable
define void @SuiteSparse_finish() local_unnamed_addr #7 !dbg !167 {
  ret void, !dbg !168
}

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #5 !dbg !169 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !171, metadata !DIExpression()), !dbg !175
  call void @llvm.dbg.value(metadata i64 %1, metadata !172, metadata !DIExpression()), !dbg !176
  %3 = icmp eq i64 %0, 0, !dbg !177
  %4 = select i1 %3, i64 1, i64 %0, !dbg !179
  call void @llvm.dbg.value(metadata i64 %4, metadata !171, metadata !DIExpression()), !dbg !175
  %5 = icmp eq i64 %1, 0, !dbg !180
  call void @llvm.dbg.value(metadata i64 1, metadata !172, metadata !DIExpression()), !dbg !176
  %6 = select i1 %5, i64 1, i64 %1, !dbg !182
  call void @llvm.dbg.value(metadata i64 %6, metadata !172, metadata !DIExpression()), !dbg !176
  %7 = mul i64 %6, %4, !dbg !183
  call void @llvm.dbg.value(metadata i64 %7, metadata !174, metadata !DIExpression()), !dbg !184
  %8 = uitofp i64 %7 to double, !dbg !185
  %9 = uitofp i64 %4 to double, !dbg !187
  %10 = uitofp i64 %6 to double, !dbg !188
  %11 = fmul double %9, %10, !dbg !189
  %12 = fcmp une double %11, %8, !dbg !190
  br i1 %12, label %16, label %13, !dbg !191

; <label>:13:                                     ; preds = %2
  %14 = load i8* (i64)*, i8* (i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 0), align 8, !dbg !192, !tbaa !151
  %15 = tail call i8* %14(i64 %7) #10, !dbg !192
  call void @llvm.dbg.value(metadata i8* %15, metadata !173, metadata !DIExpression()), !dbg !194
  br label %16

; <label>:16:                                     ; preds = %2, %13
  %17 = phi i8* [ %15, %13 ], [ null, %2 ], !dbg !195
  call void @llvm.dbg.value(metadata i8* %17, metadata !173, metadata !DIExpression()), !dbg !194
  ret i8* %17, !dbg !196
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.declare(metadata, metadata, metadata) #8

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #9

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #9

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_calloc(i64, i64) local_unnamed_addr #5 !dbg !197 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !199, metadata !DIExpression()), !dbg !203
  call void @llvm.dbg.value(metadata i64 %1, metadata !200, metadata !DIExpression()), !dbg !204
  %3 = icmp eq i64 %0, 0, !dbg !205
  %4 = select i1 %3, i64 1, i64 %0, !dbg !207
  call void @llvm.dbg.value(metadata i64 %4, metadata !199, metadata !DIExpression()), !dbg !203
  %5 = icmp eq i64 %1, 0, !dbg !208
  call void @llvm.dbg.value(metadata i64 1, metadata !200, metadata !DIExpression()), !dbg !204
  %6 = select i1 %5, i64 1, i64 %1, !dbg !210
  call void @llvm.dbg.value(metadata i64 %6, metadata !200, metadata !DIExpression()), !dbg !204
  %7 = mul i64 %6, %4, !dbg !211
  call void @llvm.dbg.value(metadata i64 %7, metadata !202, metadata !DIExpression()), !dbg !212
  %8 = uitofp i64 %7 to double, !dbg !213
  %9 = uitofp i64 %4 to double, !dbg !215
  %10 = uitofp i64 %6 to double, !dbg !216
  %11 = fmul double %9, %10, !dbg !217
  %12 = fcmp une double %11, %8, !dbg !218
  br i1 %12, label %16, label %13, !dbg !219

; <label>:13:                                     ; preds = %2
  %14 = load i8* (i64, i64)*, i8* (i64, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 1), align 8, !dbg !220, !tbaa !155
  %15 = tail call i8* %14(i64 %4, i64 %6) #10, !dbg !220
  call void @llvm.dbg.value(metadata i8* %15, metadata !201, metadata !DIExpression()), !dbg !222
  br label %16

; <label>:16:                                     ; preds = %2, %13
  %17 = phi i8* [ %15, %13 ], [ null, %2 ], !dbg !223
  call void @llvm.dbg.value(metadata i8* %17, metadata !201, metadata !DIExpression()), !dbg !222
  ret i8* %17, !dbg !224
}

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_realloc(i64, i64, i64, i8*, i32* nocapture) local_unnamed_addr #5 !dbg !225 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !230, metadata !DIExpression()), !dbg !241
  call void @llvm.dbg.value(metadata i64 %1, metadata !231, metadata !DIExpression()), !dbg !242
  call void @llvm.dbg.value(metadata i64 %2, metadata !232, metadata !DIExpression()), !dbg !243
  call void @llvm.dbg.value(metadata i8* %3, metadata !233, metadata !DIExpression()), !dbg !244
  call void @llvm.dbg.value(metadata i32* %4, metadata !234, metadata !DIExpression()), !dbg !245
  %6 = icmp eq i64 %1, 0, !dbg !246
  %7 = select i1 %6, i64 1, i64 %1, !dbg !248
  call void @llvm.dbg.value(metadata i64 %7, metadata !231, metadata !DIExpression()), !dbg !242
  %8 = icmp eq i64 %0, 0, !dbg !249
  call void @llvm.dbg.value(metadata i64 1, metadata !230, metadata !DIExpression()), !dbg !241
  %9 = select i1 %8, i64 1, i64 %0, !dbg !251
  call void @llvm.dbg.value(metadata i64 %9, metadata !230, metadata !DIExpression()), !dbg !241
  %10 = icmp eq i64 %2, 0, !dbg !252
  %11 = select i1 %10, i64 1, i64 %2, !dbg !254
  call void @llvm.dbg.value(metadata i64 %11, metadata !232, metadata !DIExpression()), !dbg !243
  %12 = mul i64 %11, %9, !dbg !255
  call void @llvm.dbg.value(metadata i64 %12, metadata !235, metadata !DIExpression()), !dbg !256
  %13 = uitofp i64 %12 to double, !dbg !257
  %14 = uitofp i64 %9 to double, !dbg !258
  %15 = uitofp i64 %11 to double, !dbg !259
  %16 = fmul double %14, %15, !dbg !260
  %17 = fcmp une double %16, %13, !dbg !261
  br i1 %17, label %32, label %18, !dbg !262

; <label>:18:                                     ; preds = %5
  %19 = icmp eq i8* %3, null, !dbg !263
  br i1 %19, label %20, label %23, !dbg !264

; <label>:20:                                     ; preds = %18
  %21 = tail call i8* @SuiteSparse_malloc(i64 %9, i64 %11), !dbg !265
  call void @llvm.dbg.value(metadata i8* %21, metadata !233, metadata !DIExpression()), !dbg !244
  %22 = icmp ne i8* %21, null, !dbg !267
  br label %32, !dbg !268

; <label>:23:                                     ; preds = %18
  %24 = icmp eq i64 %7, %9, !dbg !269
  br i1 %24, label %32, label %25, !dbg !270

; <label>:25:                                     ; preds = %23
  %26 = load i8* (i8*, i64)*, i8* (i8*, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 2), align 8, !dbg !271, !tbaa !157
  %27 = tail call i8* %26(i8* nonnull %3, i64 %12) #10, !dbg !271
  call void @llvm.dbg.value(metadata i8* %27, metadata !236, metadata !DIExpression()), !dbg !272
  %28 = icmp eq i8* %27, null, !dbg !273
  %29 = icmp ult i64 %9, %7, !dbg !275
  %30 = select i1 %28, i1 %29, i1 true, !dbg !278
  %31 = select i1 %28, i8* %3, i8* %27, !dbg !278
  br label %32, !dbg !278

; <label>:32:                                     ; preds = %25, %23, %5, %20
  %33 = phi i1 [ %22, %20 ], [ false, %5 ], [ true, %23 ], [ %30, %25 ]
  %34 = phi i8* [ %21, %20 ], [ %3, %5 ], [ %3, %23 ], [ %31, %25 ]
  %35 = zext i1 %33 to i32
  store i32 %35, i32* %4, align 4, !dbg !279, !tbaa !281
  call void @llvm.dbg.value(metadata i8* %34, metadata !233, metadata !DIExpression()), !dbg !244
  ret i8* %34, !dbg !283
}

; Function Attrs: nounwind ssp uwtable
define noalias i8* @SuiteSparse_free(i8*) local_unnamed_addr #5 !dbg !284 {
  call void @llvm.dbg.value(metadata i8* %0, metadata !288, metadata !DIExpression()), !dbg !289
  %2 = icmp eq i8* %0, null, !dbg !290
  br i1 %2, label %5, label %3, !dbg !292

; <label>:3:                                      ; preds = %1
  %4 = load void (i8*)*, void (i8*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 3), align 8, !dbg !293, !tbaa !159
  tail call void %4(i8* nonnull %0) #10, !dbg !293
  br label %5, !dbg !295

; <label>:5:                                      ; preds = %1, %3
  ret i8* null, !dbg !296
}

; Function Attrs: nounwind ssp uwtable
define void @SuiteSparse_tic(double* nocapture) local_unnamed_addr #5 !dbg !297 {
  call void @llvm.dbg.value(metadata double* %0, metadata !301, metadata !DIExpression()), !dbg !302
  %2 = bitcast double* %0 to i8*, !dbg !303
  call void @llvm.memset.p0i8.i64(i8* %2, i8 0, i64 16, i32 8, i1 false), !dbg !304
  ret void, !dbg !303
}

; Function Attrs: nounwind ssp uwtable
define double @SuiteSparse_toc(double* nocapture readonly) local_unnamed_addr #5 !dbg !305 {
  %2 = alloca [2 x double], align 16
  call void @llvm.dbg.value(metadata double* %0, metadata !309, metadata !DIExpression()), !dbg !314
  %3 = bitcast [2 x double]* %2 to i8*, !dbg !315
  call void @llvm.lifetime.start.p0i8(i64 16, i8* nonnull %3) #10, !dbg !315
  call void @llvm.dbg.declare(metadata [2 x double]* %2, metadata !310, metadata !DIExpression()), !dbg !316
  %4 = getelementptr inbounds [2 x double], [2 x double]* %2, i64 0, i64 0, !dbg !317
  call void @SuiteSparse_tic(double* nonnull %4), !dbg !318
  %5 = load double, double* %4, align 16, !dbg !319, !tbaa !140
  %6 = load double, double* %0, align 8, !dbg !320, !tbaa !140
  %7 = fsub double %5, %6, !dbg !321
  %8 = getelementptr inbounds [2 x double], [2 x double]* %2, i64 0, i64 1, !dbg !322
  %9 = load double, double* %8, align 8, !dbg !322, !tbaa !140
  %10 = getelementptr inbounds double, double* %0, i64 1, !dbg !323
  %11 = load double, double* %10, align 8, !dbg !323, !tbaa !140
  %12 = fsub double %9, %11, !dbg !324
  %13 = fmul double %12, 1.000000e-09, !dbg !325
  %14 = fadd double %7, %13, !dbg !326
  call void @llvm.lifetime.end.p0i8(i64 16, i8* nonnull %3) #10, !dbg !327
  ret double %14, !dbg !328
}

; Function Attrs: nounwind ssp uwtable
define double @SuiteSparse_time() local_unnamed_addr #5 !dbg !329 {
  %1 = alloca [2 x double], align 16
  %2 = bitcast [2 x double]* %1 to i8*, !dbg !334
  call void @llvm.lifetime.start.p0i8(i64 16, i8* nonnull %2) #10, !dbg !334
  call void @llvm.dbg.declare(metadata [2 x double]* %1, metadata !333, metadata !DIExpression()), !dbg !335
  %3 = getelementptr inbounds [2 x double], [2 x double]* %1, i64 0, i64 0, !dbg !336
  call void @SuiteSparse_tic(double* nonnull %3), !dbg !337
  %4 = load double, double* %3, align 16, !dbg !338, !tbaa !140
  %5 = getelementptr inbounds [2 x double], [2 x double]* %1, i64 0, i64 1, !dbg !339
  %6 = load double, double* %5, align 8, !dbg !339, !tbaa !140
  %7 = fmul double %6, 1.000000e-09, !dbg !340
  %8 = fadd double %4, %7, !dbg !341
  call void @llvm.lifetime.end.p0i8(i64 16, i8* nonnull %2) #10, !dbg !342
  ret double %8, !dbg !343
}

; Function Attrs: nounwind ssp uwtable
define i32 @SuiteSparse_version(i32*) local_unnamed_addr #5 !dbg !344 {
  call void @llvm.dbg.value(metadata i32* %0, metadata !348, metadata !DIExpression()), !dbg !349
  %2 = icmp eq i32* %0, null, !dbg !350
  br i1 %2, label %6, label %3, !dbg !352

; <label>:3:                                      ; preds = %1
  store i32 5, i32* %0, align 4, !dbg !353, !tbaa !281
  %4 = getelementptr inbounds i32, i32* %0, i64 1, !dbg !355
  store i32 4, i32* %4, align 4, !dbg !356, !tbaa !281
  %5 = getelementptr inbounds i32, i32* %0, i64 2, !dbg !357
  store i32 0, i32* %5, align 4, !dbg !358, !tbaa !281
  br label %6, !dbg !359

; <label>:6:                                      ; preds = %1, %3
  ret i32 5004, !dbg !360
}

; Function Attrs: nounwind readnone speculatable
declare double @llvm.fabs.f64(double) #8

; Function Attrs: nounwind readnone speculatable
declare double @llvm.sqrt.f64(double) #8

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #8

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #9

attributes #0 = { nounwind allocsize(0) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind allocsize(0,1) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind allocsize(1) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind readnone ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #5 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #6 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #7 = { norecurse nounwind readnone ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #8 = { nounwind readnone speculatable }
attributes #9 = { argmemonly nounwind }
attributes #10 = { nounwind }

!llvm.dbg.cu = !{!2}
!llvm.module.flags = !{!50, !51, !52, !53}
!llvm.ident = !{!54}

!0 = !DIGlobalVariableExpression(var: !1, expr: !DIExpression())
!1 = distinct !DIGlobalVariable(name: "SuiteSparse_config", scope: !2, file: !3, line: 52, type: !9, isLocal: false, isDefinition: true)
!2 = distinct !DICompileUnit(language: DW_LANG_C99, file: !3, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !4, retainedTypes: !5, globals: !8)
!3 = !DIFile(filename: "SuiteSparse_config.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!4 = !{}
!5 = !{!6, !7}
!6 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!7 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!8 = !{!0}
!9 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "SuiteSparse_config_struct", file: !10, line: 85, size: 448, elements: !11)
!10 = !DIFile(filename: "./SuiteSparse_config.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!11 = !{!12, !21, !25, !29, !33, !41, !45}
!12 = !DIDerivedType(tag: DW_TAG_member, name: "malloc_func", scope: !9, file: !10, line: 87, baseType: !13, size: 64)
!13 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !14, size: 64)
!14 = !DISubroutineType(types: !15)
!15 = !{!7, !16}
!16 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !17, line: 31, baseType: !18)
!17 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX10.14.sdk/usr/include/sys/_types/_size_t.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!18 = !DIDerivedType(tag: DW_TAG_typedef, name: "__darwin_size_t", file: !19, line: 92, baseType: !20)
!19 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Platforms/MacOSX.platform/Developer/SDKs/MacOSX10.14.sdk/usr/include/i386/_types.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!20 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!21 = !DIDerivedType(tag: DW_TAG_member, name: "calloc_func", scope: !9, file: !10, line: 88, baseType: !22, size: 64, offset: 64)
!22 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !23, size: 64)
!23 = !DISubroutineType(types: !24)
!24 = !{!7, !16, !16}
!25 = !DIDerivedType(tag: DW_TAG_member, name: "realloc_func", scope: !9, file: !10, line: 89, baseType: !26, size: 64, offset: 128)
!26 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !27, size: 64)
!27 = !DISubroutineType(types: !28)
!28 = !{!7, !7, !16}
!29 = !DIDerivedType(tag: DW_TAG_member, name: "free_func", scope: !9, file: !10, line: 90, baseType: !30, size: 64, offset: 192)
!30 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !31, size: 64)
!31 = !DISubroutineType(types: !32)
!32 = !{null, !7}
!33 = !DIDerivedType(tag: DW_TAG_member, name: "printf_func", scope: !9, file: !10, line: 91, baseType: !34, size: 64, offset: 256)
!34 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !35, size: 64)
!35 = !DISubroutineType(types: !36)
!36 = !{!37, !38, null}
!37 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!38 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !39, size: 64)
!39 = !DIDerivedType(tag: DW_TAG_const_type, baseType: !40)
!40 = !DIBasicType(name: "char", size: 8, encoding: DW_ATE_signed_char)
!41 = !DIDerivedType(tag: DW_TAG_member, name: "hypot_func", scope: !9, file: !10, line: 92, baseType: !42, size: 64, offset: 320)
!42 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !43, size: 64)
!43 = !DISubroutineType(types: !44)
!44 = !{!6, !6, !6}
!45 = !DIDerivedType(tag: DW_TAG_member, name: "divcomplex_func", scope: !9, file: !10, line: 93, baseType: !46, size: 64, offset: 384)
!46 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !47, size: 64)
!47 = !DISubroutineType(types: !48)
!48 = !{!37, !6, !6, !6, !6, !49, !49}
!49 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!50 = !{i32 2, !"Dwarf Version", i32 4}
!51 = !{i32 2, !"Debug Info Version", i32 3}
!52 = !{i32 1, !"wchar_size", i32 4}
!53 = !{i32 7, !"PIC Level", i32 2}
!54 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!55 = distinct !DISubprogram(name: "SuiteSparse_hypot", scope: !3, file: !3, line: 456, type: !43, isLocal: false, isDefinition: true, scopeLine: 457, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !56)
!56 = !{!57, !58, !59, !60}
!57 = !DILocalVariable(name: "x", arg: 1, scope: !55, file: !3, line: 456, type: !6)
!58 = !DILocalVariable(name: "y", arg: 2, scope: !55, file: !3, line: 456, type: !6)
!59 = !DILocalVariable(name: "s", scope: !55, file: !3, line: 458, type: !6)
!60 = !DILocalVariable(name: "r", scope: !55, file: !3, line: 458, type: !6)
!61 = !DILocation(line: 456, column: 34, scope: !55)
!62 = !DILocation(line: 456, column: 44, scope: !55)
!63 = !DILocation(line: 459, column: 9, scope: !55)
!64 = !DILocation(line: 460, column: 9, scope: !55)
!65 = !DILocation(line: 461, column: 11, scope: !66)
!66 = distinct !DILexicalBlock(scope: !55, file: !3, line: 461, column: 9)
!67 = !DILocation(line: 0, scope: !68)
!68 = distinct !DILexicalBlock(scope: !69, file: !3, line: 463, column: 13)
!69 = distinct !DILexicalBlock(scope: !66, file: !3, line: 462, column: 5)
!70 = !DILocation(line: 461, column: 9, scope: !55)
!71 = !DILocation(line: 463, column: 19, scope: !68)
!72 = !DILocation(line: 463, column: 13, scope: !69)
!73 = !DILocation(line: 469, column: 19, scope: !74)
!74 = distinct !DILexicalBlock(scope: !68, file: !3, line: 468, column: 9)
!75 = !DILocation(line: 458, column: 15, scope: !55)
!76 = !DILocation(line: 470, column: 34, scope: !74)
!77 = !DILocation(line: 470, column: 31, scope: !74)
!78 = !DILocation(line: 470, column: 21, scope: !74)
!79 = !DILocation(line: 470, column: 19, scope: !74)
!80 = !DILocation(line: 458, column: 12, scope: !55)
!81 = !DILocation(line: 475, column: 19, scope: !82)
!82 = distinct !DILexicalBlock(scope: !83, file: !3, line: 475, column: 13)
!83 = distinct !DILexicalBlock(scope: !66, file: !3, line: 474, column: 5)
!84 = !DILocation(line: 475, column: 13, scope: !83)
!85 = !DILocation(line: 481, column: 19, scope: !86)
!86 = distinct !DILexicalBlock(scope: !82, file: !3, line: 480, column: 9)
!87 = !DILocation(line: 482, column: 34, scope: !86)
!88 = !DILocation(line: 482, column: 31, scope: !86)
!89 = !DILocation(line: 482, column: 21, scope: !86)
!90 = !DILocation(line: 482, column: 19, scope: !86)
!91 = !DILocation(line: 0, scope: !86)
!92 = !DILocation(line: 485, column: 5, scope: !55)
!93 = distinct !DISubprogram(name: "SuiteSparse_divcomplex", scope: !3, file: !3, line: 506, type: !47, isLocal: false, isDefinition: true, scopeLine: 512, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !94)
!94 = !{!95, !96, !97, !98, !99, !100, !101, !102, !103, !104}
!95 = !DILocalVariable(name: "ar", arg: 1, scope: !93, file: !3, line: 508, type: !6)
!96 = !DILocalVariable(name: "ai", arg: 2, scope: !93, file: !3, line: 508, type: !6)
!97 = !DILocalVariable(name: "br", arg: 3, scope: !93, file: !3, line: 509, type: !6)
!98 = !DILocalVariable(name: "bi", arg: 4, scope: !93, file: !3, line: 509, type: !6)
!99 = !DILocalVariable(name: "cr", arg: 5, scope: !93, file: !3, line: 510, type: !49)
!100 = !DILocalVariable(name: "ci", arg: 6, scope: !93, file: !3, line: 510, type: !49)
!101 = !DILocalVariable(name: "tr", scope: !93, file: !3, line: 513, type: !6)
!102 = !DILocalVariable(name: "ti", scope: !93, file: !3, line: 513, type: !6)
!103 = !DILocalVariable(name: "r", scope: !93, file: !3, line: 513, type: !6)
!104 = !DILocalVariable(name: "den", scope: !93, file: !3, line: 513, type: !6)
!105 = !DILocation(line: 508, column: 12, scope: !93)
!106 = !DILocation(line: 508, column: 23, scope: !93)
!107 = !DILocation(line: 509, column: 12, scope: !93)
!108 = !DILocation(line: 509, column: 23, scope: !93)
!109 = !DILocation(line: 510, column: 13, scope: !93)
!110 = !DILocation(line: 510, column: 25, scope: !93)
!111 = !DILocation(line: 514, column: 9, scope: !112)
!112 = distinct !DILexicalBlock(scope: !93, file: !3, line: 514, column: 9)
!113 = !DILocation(line: 514, column: 22, scope: !112)
!114 = !DILocation(line: 514, column: 19, scope: !112)
!115 = !DILocation(line: 514, column: 9, scope: !93)
!116 = !DILocation(line: 516, column: 16, scope: !117)
!117 = distinct !DILexicalBlock(scope: !112, file: !3, line: 515, column: 5)
!118 = !DILocation(line: 513, column: 20, scope: !93)
!119 = !DILocation(line: 517, column: 22, scope: !117)
!120 = !DILocation(line: 517, column: 18, scope: !117)
!121 = !DILocation(line: 513, column: 23, scope: !93)
!122 = !DILocation(line: 518, column: 23, scope: !117)
!123 = !DILocation(line: 518, column: 18, scope: !117)
!124 = !DILocation(line: 518, column: 28, scope: !117)
!125 = !DILocation(line: 513, column: 12, scope: !93)
!126 = !DILocation(line: 519, column: 23, scope: !117)
!127 = !DILocation(line: 520, column: 5, scope: !117)
!128 = !DILocation(line: 523, column: 16, scope: !129)
!129 = distinct !DILexicalBlock(scope: !112, file: !3, line: 522, column: 5)
!130 = !DILocation(line: 524, column: 17, scope: !129)
!131 = !DILocation(line: 524, column: 22, scope: !129)
!132 = !DILocation(line: 525, column: 18, scope: !129)
!133 = !DILocation(line: 525, column: 22, scope: !129)
!134 = !DILocation(line: 525, column: 28, scope: !129)
!135 = !DILocation(line: 526, column: 18, scope: !129)
!136 = !DILocation(line: 513, column: 16, scope: !93)
!137 = !DILocation(line: 0, scope: !129)
!138 = !DILocation(line: 0, scope: !117)
!139 = !DILocation(line: 528, column: 9, scope: !93)
!140 = !{!141, !141, i64 0}
!141 = !{!"double", !142, i64 0}
!142 = !{!"omnipotent char", !143, i64 0}
!143 = !{!"Simple C/C++ TBAA"}
!144 = !DILocation(line: 529, column: 9, scope: !93)
!145 = !DILocation(line: 530, column: 17, scope: !93)
!146 = !DILocation(line: 530, column: 5, scope: !93)
!147 = distinct !DISubprogram(name: "SuiteSparse_start", scope: !3, file: !3, line: 104, type: !148, isLocal: false, isDefinition: true, scopeLine: 105, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !4)
!148 = !DISubroutineType(types: !149)
!149 = !{null}
!150 = !DILocation(line: 117, column: 45, scope: !147)
!151 = !{!152, !153, i64 0}
!152 = !{!"SuiteSparse_config_struct", !153, i64 0, !153, i64 8, !153, i64 16, !153, i64 24, !153, i64 32, !153, i64 40, !153, i64 48}
!153 = !{!"any pointer", !142, i64 0}
!154 = !DILocation(line: 118, column: 45, scope: !147)
!155 = !{!152, !153, i64 8}
!156 = !DILocation(line: 119, column: 45, scope: !147)
!157 = !{!152, !153, i64 16}
!158 = !DILocation(line: 120, column: 45, scope: !147)
!159 = !{!152, !153, i64 24}
!160 = !DILocation(line: 138, column: 44, scope: !147)
!161 = !{!152, !153, i64 32}
!162 = !DILocation(line: 146, column: 35, scope: !147)
!163 = !{!152, !153, i64 40}
!164 = !DILocation(line: 147, column: 40, scope: !147)
!165 = !{!152, !153, i64 48}
!166 = !DILocation(line: 148, column: 1, scope: !147)
!167 = distinct !DISubprogram(name: "SuiteSparse_finish", scope: !3, file: !3, line: 164, type: !148, isLocal: false, isDefinition: true, scopeLine: 165, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !4)
!168 = !DILocation(line: 167, column: 1, scope: !167)
!169 = distinct !DISubprogram(name: "SuiteSparse_malloc", scope: !3, file: !3, line: 173, type: !23, isLocal: false, isDefinition: true, scopeLine: 178, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !170)
!170 = !{!171, !172, !173, !174}
!171 = !DILocalVariable(name: "nitems", arg: 1, scope: !169, file: !3, line: 175, type: !16)
!172 = !DILocalVariable(name: "size_of_item", arg: 2, scope: !169, file: !3, line: 176, type: !16)
!173 = !DILocalVariable(name: "p", scope: !169, file: !3, line: 179, type: !7)
!174 = !DILocalVariable(name: "size", scope: !169, file: !3, line: 180, type: !16)
!175 = !DILocation(line: 175, column: 12, scope: !169)
!176 = !DILocation(line: 176, column: 12, scope: !169)
!177 = !DILocation(line: 181, column: 16, scope: !178)
!178 = distinct !DILexicalBlock(scope: !169, file: !3, line: 181, column: 9)
!179 = !DILocation(line: 181, column: 9, scope: !169)
!180 = !DILocation(line: 182, column: 22, scope: !181)
!181 = distinct !DILexicalBlock(scope: !169, file: !3, line: 182, column: 9)
!182 = !DILocation(line: 182, column: 9, scope: !169)
!183 = !DILocation(line: 183, column: 19, scope: !169)
!184 = !DILocation(line: 180, column: 12, scope: !169)
!185 = !DILocation(line: 185, column: 9, scope: !186)
!186 = distinct !DILexicalBlock(scope: !169, file: !3, line: 185, column: 9)
!187 = !DILocation(line: 185, column: 18, scope: !186)
!188 = !DILocation(line: 185, column: 37, scope: !186)
!189 = !DILocation(line: 185, column: 35, scope: !186)
!190 = !DILocation(line: 185, column: 14, scope: !186)
!191 = !DILocation(line: 185, column: 9, scope: !169)
!192 = !DILocation(line: 192, column: 22, scope: !193)
!193 = distinct !DILexicalBlock(scope: !186, file: !3, line: 191, column: 5)
!194 = !DILocation(line: 179, column: 11, scope: !169)
!195 = !DILocation(line: 0, scope: !193)
!196 = !DILocation(line: 194, column: 5, scope: !169)
!197 = distinct !DISubprogram(name: "SuiteSparse_calloc", scope: !3, file: !3, line: 202, type: !23, isLocal: false, isDefinition: true, scopeLine: 207, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !198)
!198 = !{!199, !200, !201, !202}
!199 = !DILocalVariable(name: "nitems", arg: 1, scope: !197, file: !3, line: 204, type: !16)
!200 = !DILocalVariable(name: "size_of_item", arg: 2, scope: !197, file: !3, line: 205, type: !16)
!201 = !DILocalVariable(name: "p", scope: !197, file: !3, line: 208, type: !7)
!202 = !DILocalVariable(name: "size", scope: !197, file: !3, line: 209, type: !16)
!203 = !DILocation(line: 204, column: 12, scope: !197)
!204 = !DILocation(line: 205, column: 12, scope: !197)
!205 = !DILocation(line: 210, column: 16, scope: !206)
!206 = distinct !DILexicalBlock(scope: !197, file: !3, line: 210, column: 9)
!207 = !DILocation(line: 210, column: 9, scope: !197)
!208 = !DILocation(line: 211, column: 22, scope: !209)
!209 = distinct !DILexicalBlock(scope: !197, file: !3, line: 211, column: 9)
!210 = !DILocation(line: 211, column: 9, scope: !197)
!211 = !DILocation(line: 212, column: 19, scope: !197)
!212 = !DILocation(line: 209, column: 12, scope: !197)
!213 = !DILocation(line: 214, column: 9, scope: !214)
!214 = distinct !DILexicalBlock(scope: !197, file: !3, line: 214, column: 9)
!215 = !DILocation(line: 214, column: 18, scope: !214)
!216 = !DILocation(line: 214, column: 37, scope: !214)
!217 = !DILocation(line: 214, column: 35, scope: !214)
!218 = !DILocation(line: 214, column: 14, scope: !214)
!219 = !DILocation(line: 214, column: 9, scope: !197)
!220 = !DILocation(line: 221, column: 22, scope: !221)
!221 = distinct !DILexicalBlock(scope: !214, file: !3, line: 220, column: 5)
!222 = !DILocation(line: 208, column: 11, scope: !197)
!223 = !DILocation(line: 0, scope: !221)
!224 = !DILocation(line: 223, column: 5, scope: !197)
!225 = distinct !DISubprogram(name: "SuiteSparse_realloc", scope: !3, file: !3, line: 238, type: !226, isLocal: false, isDefinition: true, scopeLine: 247, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !229)
!226 = !DISubroutineType(types: !227)
!227 = !{!7, !16, !16, !16, !7, !228}
!228 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !37, size: 64)
!229 = !{!230, !231, !232, !233, !234, !235, !236}
!230 = !DILocalVariable(name: "nitems_new", arg: 1, scope: !225, file: !3, line: 241, type: !16)
!231 = !DILocalVariable(name: "nitems_old", arg: 2, scope: !225, file: !3, line: 242, type: !16)
!232 = !DILocalVariable(name: "size_of_item", arg: 3, scope: !225, file: !3, line: 243, type: !16)
!233 = !DILocalVariable(name: "p", arg: 4, scope: !225, file: !3, line: 244, type: !7)
!234 = !DILocalVariable(name: "ok", arg: 5, scope: !225, file: !3, line: 245, type: !228)
!235 = !DILocalVariable(name: "size", scope: !225, file: !3, line: 248, type: !16)
!236 = !DILocalVariable(name: "pnew", scope: !237, file: !3, line: 273, type: !7)
!237 = distinct !DILexicalBlock(scope: !238, file: !3, line: 271, column: 5)
!238 = distinct !DILexicalBlock(scope: !239, file: !3, line: 265, column: 14)
!239 = distinct !DILexicalBlock(scope: !240, file: !3, line: 259, column: 14)
!240 = distinct !DILexicalBlock(scope: !225, file: !3, line: 254, column: 9)
!241 = !DILocation(line: 241, column: 12, scope: !225)
!242 = !DILocation(line: 242, column: 12, scope: !225)
!243 = !DILocation(line: 243, column: 12, scope: !225)
!244 = !DILocation(line: 244, column: 11, scope: !225)
!245 = !DILocation(line: 245, column: 10, scope: !225)
!246 = !DILocation(line: 249, column: 20, scope: !247)
!247 = distinct !DILexicalBlock(scope: !225, file: !3, line: 249, column: 9)
!248 = !DILocation(line: 249, column: 9, scope: !225)
!249 = !DILocation(line: 250, column: 20, scope: !250)
!250 = distinct !DILexicalBlock(scope: !225, file: !3, line: 250, column: 9)
!251 = !DILocation(line: 250, column: 9, scope: !225)
!252 = !DILocation(line: 251, column: 22, scope: !253)
!253 = distinct !DILexicalBlock(scope: !225, file: !3, line: 251, column: 9)
!254 = !DILocation(line: 251, column: 9, scope: !225)
!255 = !DILocation(line: 252, column: 23, scope: !225)
!256 = !DILocation(line: 248, column: 12, scope: !225)
!257 = !DILocation(line: 254, column: 9, scope: !240)
!258 = !DILocation(line: 254, column: 18, scope: !240)
!259 = !DILocation(line: 254, column: 41, scope: !240)
!260 = !DILocation(line: 254, column: 39, scope: !240)
!261 = !DILocation(line: 254, column: 14, scope: !240)
!262 = !DILocation(line: 254, column: 9, scope: !225)
!263 = !DILocation(line: 259, column: 16, scope: !239)
!264 = !DILocation(line: 259, column: 14, scope: !240)
!265 = !DILocation(line: 262, column: 13, scope: !266)
!266 = distinct !DILexicalBlock(scope: !239, file: !3, line: 260, column: 5)
!267 = !DILocation(line: 263, column: 20, scope: !266)
!268 = !DILocation(line: 264, column: 5, scope: !266)
!269 = !DILocation(line: 265, column: 25, scope: !238)
!270 = !DILocation(line: 265, column: 14, scope: !239)
!271 = !DILocation(line: 274, column: 25, scope: !237)
!272 = !DILocation(line: 273, column: 15, scope: !237)
!273 = !DILocation(line: 275, column: 18, scope: !274)
!274 = distinct !DILexicalBlock(scope: !237, file: !3, line: 275, column: 13)
!275 = !DILocation(line: 277, column: 28, scope: !276)
!276 = distinct !DILexicalBlock(scope: !277, file: !3, line: 277, column: 17)
!277 = distinct !DILexicalBlock(scope: !274, file: !3, line: 276, column: 9)
!278 = !DILocation(line: 275, column: 13, scope: !237)
!279 = !DILocation(line: 0, scope: !280)
!280 = distinct !DILexicalBlock(scope: !240, file: !3, line: 255, column: 5)
!281 = !{!282, !282, i64 0}
!282 = !{!"int", !142, i64 0}
!283 = !DILocation(line: 296, column: 5, scope: !225)
!284 = distinct !DISubprogram(name: "SuiteSparse_free", scope: !3, file: !3, line: 303, type: !285, isLocal: false, isDefinition: true, scopeLine: 307, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !287)
!285 = !DISubroutineType(types: !286)
!286 = !{!7, !7}
!287 = !{!288}
!288 = !DILocalVariable(name: "p", arg: 1, scope: !284, file: !3, line: 305, type: !7)
!289 = !DILocation(line: 305, column: 11, scope: !284)
!290 = !DILocation(line: 308, column: 9, scope: !291)
!291 = distinct !DILexicalBlock(scope: !284, file: !3, line: 308, column: 9)
!292 = !DILocation(line: 308, column: 9, scope: !284)
!293 = !DILocation(line: 310, column: 9, scope: !294)
!294 = distinct !DILexicalBlock(scope: !291, file: !3, line: 309, column: 5)
!295 = !DILocation(line: 311, column: 5, scope: !294)
!296 = !DILocation(line: 312, column: 5, scope: !284)
!297 = distinct !DISubprogram(name: "SuiteSparse_tic", scope: !3, file: !3, line: 365, type: !298, isLocal: false, isDefinition: true, scopeLine: 369, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !300)
!298 = !DISubroutineType(types: !299)
!299 = !{null, !49}
!300 = !{!301}
!301 = !DILocalVariable(name: "tic", arg: 1, scope: !297, file: !3, line: 367, type: !49)
!302 = !DILocation(line: 367, column: 12, scope: !297)
!303 = !DILocation(line: 373, column: 1, scope: !297)
!304 = !DILocation(line: 372, column: 13, scope: !297)
!305 = distinct !DISubprogram(name: "SuiteSparse_toc", scope: !3, file: !3, line: 389, type: !306, isLocal: false, isDefinition: true, scopeLine: 393, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !308)
!306 = !DISubroutineType(types: !307)
!307 = !{!6, !49}
!308 = !{!309, !310}
!309 = !DILocalVariable(name: "tic", arg: 1, scope: !305, file: !3, line: 391, type: !49)
!310 = !DILocalVariable(name: "toc", scope: !305, file: !3, line: 394, type: !311)
!311 = !DICompositeType(tag: DW_TAG_array_type, baseType: !6, size: 128, elements: !312)
!312 = !{!313}
!313 = !DISubrange(count: 2)
!314 = !DILocation(line: 391, column: 12, scope: !305)
!315 = !DILocation(line: 394, column: 5, scope: !305)
!316 = !DILocation(line: 394, column: 12, scope: !305)
!317 = !DILocation(line: 395, column: 22, scope: !305)
!318 = !DILocation(line: 395, column: 5, scope: !305)
!319 = !DILocation(line: 396, column: 14, scope: !305)
!320 = !DILocation(line: 396, column: 24, scope: !305)
!321 = !DILocation(line: 396, column: 22, scope: !305)
!322 = !DILocation(line: 396, column: 43, scope: !305)
!323 = !DILocation(line: 396, column: 53, scope: !305)
!324 = !DILocation(line: 396, column: 51, scope: !305)
!325 = !DILocation(line: 396, column: 40, scope: !305)
!326 = !DILocation(line: 396, column: 33, scope: !305)
!327 = !DILocation(line: 397, column: 1, scope: !305)
!328 = !DILocation(line: 396, column: 5, scope: !305)
!329 = distinct !DISubprogram(name: "SuiteSparse_time", scope: !3, file: !3, line: 406, type: !330, isLocal: false, isDefinition: true, scopeLine: 410, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !332)
!330 = !DISubroutineType(types: !331)
!331 = !{!6}
!332 = !{!333}
!333 = !DILocalVariable(name: "toc", scope: !329, file: !3, line: 411, type: !311)
!334 = !DILocation(line: 411, column: 5, scope: !329)
!335 = !DILocation(line: 411, column: 12, scope: !329)
!336 = !DILocation(line: 412, column: 22, scope: !329)
!337 = !DILocation(line: 412, column: 5, scope: !329)
!338 = !DILocation(line: 413, column: 13, scope: !329)
!339 = !DILocation(line: 413, column: 30, scope: !329)
!340 = !DILocation(line: 413, column: 28, scope: !329)
!341 = !DILocation(line: 413, column: 21, scope: !329)
!342 = !DILocation(line: 414, column: 1, scope: !329)
!343 = !DILocation(line: 413, column: 5, scope: !329)
!344 = distinct !DISubprogram(name: "SuiteSparse_version", scope: !3, file: !3, line: 421, type: !345, isLocal: false, isDefinition: true, scopeLine: 425, flags: DIFlagPrototyped, isOptimized: true, unit: !2, variables: !347)
!345 = !DISubroutineType(types: !346)
!346 = !{!37, !228}
!347 = !{!348}
!348 = !DILocalVariable(name: "version", arg: 1, scope: !344, file: !3, line: 423, type: !228)
!349 = !DILocation(line: 423, column: 9, scope: !344)
!350 = !DILocation(line: 426, column: 17, scope: !351)
!351 = distinct !DILexicalBlock(scope: !344, file: !3, line: 426, column: 9)
!352 = !DILocation(line: 426, column: 9, scope: !344)
!353 = !DILocation(line: 428, column: 21, scope: !354)
!354 = distinct !DILexicalBlock(scope: !351, file: !3, line: 427, column: 5)
!355 = !DILocation(line: 429, column: 9, scope: !354)
!356 = !DILocation(line: 429, column: 21, scope: !354)
!357 = !DILocation(line: 430, column: 9, scope: !354)
!358 = !DILocation(line: 430, column: 21, scope: !354)
!359 = !DILocation(line: 431, column: 5, scope: !354)
!360 = !DILocation(line: 432, column: 5, scope: !344)
