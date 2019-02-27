; ModuleID = 'klu.c'
source_filename = "klu.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i64 @klu_kernel_factor(i32, i32*, i32*, double*, i32*, double, double** nocapture, double*, i32*, i32*, i32*, i32*, i32*, i32*, i32*, double*, i32*, i32, i32*, double*, i32*, i32*, double*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !20 {
  %25 = alloca double*, align 8
  call void @llvm.dbg.value(metadata i32 %0, metadata !58, metadata !DIExpression()), !dbg !96
  call void @llvm.dbg.value(metadata i32* %1, metadata !59, metadata !DIExpression()), !dbg !97
  call void @llvm.dbg.value(metadata i32* %2, metadata !60, metadata !DIExpression()), !dbg !98
  call void @llvm.dbg.value(metadata double* %3, metadata !61, metadata !DIExpression()), !dbg !99
  call void @llvm.dbg.value(metadata i32* %4, metadata !62, metadata !DIExpression()), !dbg !100
  call void @llvm.dbg.value(metadata double %5, metadata !63, metadata !DIExpression()), !dbg !101
  call void @llvm.dbg.value(metadata double** %6, metadata !64, metadata !DIExpression()), !dbg !102
  call void @llvm.dbg.value(metadata double* %7, metadata !65, metadata !DIExpression()), !dbg !103
  call void @llvm.dbg.value(metadata i32* %8, metadata !66, metadata !DIExpression()), !dbg !104
  call void @llvm.dbg.value(metadata i32* %9, metadata !67, metadata !DIExpression()), !dbg !105
  call void @llvm.dbg.value(metadata i32* %10, metadata !68, metadata !DIExpression()), !dbg !106
  call void @llvm.dbg.value(metadata i32* %11, metadata !69, metadata !DIExpression()), !dbg !107
  call void @llvm.dbg.value(metadata i32* %12, metadata !70, metadata !DIExpression()), !dbg !108
  call void @llvm.dbg.value(metadata i32* %13, metadata !71, metadata !DIExpression()), !dbg !109
  call void @llvm.dbg.value(metadata i32* %14, metadata !72, metadata !DIExpression()), !dbg !110
  call void @llvm.dbg.value(metadata double* %15, metadata !73, metadata !DIExpression()), !dbg !111
  call void @llvm.dbg.value(metadata i32* %16, metadata !74, metadata !DIExpression()), !dbg !112
  call void @llvm.dbg.value(metadata i32 %17, metadata !75, metadata !DIExpression()), !dbg !113
  call void @llvm.dbg.value(metadata i32* %18, metadata !76, metadata !DIExpression()), !dbg !114
  call void @llvm.dbg.value(metadata double* %19, metadata !77, metadata !DIExpression()), !dbg !115
  call void @llvm.dbg.value(metadata i32* %20, metadata !78, metadata !DIExpression()), !dbg !116
  call void @llvm.dbg.value(metadata i32* %21, metadata !79, metadata !DIExpression()), !dbg !117
  call void @llvm.dbg.value(metadata double* %22, metadata !80, metadata !DIExpression()), !dbg !118
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %23, metadata !81, metadata !DIExpression()), !dbg !119
  %26 = bitcast double** %25 to i8*, !dbg !120
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %26) #4, !dbg !120
  %27 = icmp sgt i32 %0, 1, !dbg !121
  %28 = select i1 %27, i32 %0, i32 1, !dbg !121
  call void @llvm.dbg.value(metadata i32 %28, metadata !58, metadata !DIExpression()), !dbg !96
  %29 = fcmp ugt double %5, 0.000000e+00, !dbg !122
  br i1 %29, label %46, label %30, !dbg !124

; <label>:30:                                     ; preds = %24
  %31 = add nsw i32 %28, %17, !dbg !125
  %32 = sext i32 %31 to i64, !dbg !126
  %33 = getelementptr inbounds i32, i32* %1, i64 %32, !dbg !126
  %34 = load i32, i32* %33, align 4, !dbg !126, !tbaa !127
  %35 = sext i32 %17 to i64, !dbg !131
  %36 = getelementptr inbounds i32, i32* %1, i64 %35, !dbg !131
  %37 = load i32, i32* %36, align 4, !dbg !131, !tbaa !127
  %38 = sub nsw i32 %34, %37, !dbg !132
  call void @llvm.dbg.value(metadata i32 %38, metadata !93, metadata !DIExpression()), !dbg !133
  %39 = fsub double -0.000000e+00, %5, !dbg !134
  call void @llvm.dbg.value(metadata double %39, metadata !63, metadata !DIExpression()), !dbg !101
  %40 = fcmp ogt double %39, 1.000000e+00, !dbg !136
  %41 = select i1 %40, double %39, double 1.000000e+00, !dbg !136
  call void @llvm.dbg.value(metadata double %41, metadata !63, metadata !DIExpression()), !dbg !101
  %42 = sitofp i32 %38 to double, !dbg !137
  %43 = fmul double %41, %42, !dbg !138
  %44 = sitofp i32 %28 to double, !dbg !139
  %45 = fadd double %43, %44, !dbg !140
  br label %46, !dbg !141

; <label>:46:                                     ; preds = %24, %30
  %47 = phi double [ %45, %30 ], [ %5, %24 ]
  %48 = fptosi double %47 to i32, !dbg !142
  call void @llvm.dbg.value(metadata i32 %48, metadata !91, metadata !DIExpression()), !dbg !144
  call void @llvm.dbg.value(metadata i32 %48, metadata !92, metadata !DIExpression()), !dbg !145
  %49 = add nuw nsw i32 %28, 1, !dbg !146
  %50 = icmp sgt i32 %49, %48, !dbg !146
  %51 = select i1 %50, i32 %49, i32 %48, !dbg !146
  call void @llvm.dbg.value(metadata i32 %51, metadata !91, metadata !DIExpression()), !dbg !144
  call void @llvm.dbg.value(metadata i32 %51, metadata !92, metadata !DIExpression()), !dbg !145
  %52 = sitofp i32 %28 to double, !dbg !147
  %53 = fmul double %52, %52, !dbg !148
  %54 = fadd double %53, %52, !dbg !149
  %55 = fmul double %54, 5.000000e-01, !dbg !150
  call void @llvm.dbg.value(metadata double %55, metadata !82, metadata !DIExpression()), !dbg !151
  %56 = fcmp olt double %55, 0x41DFFFFFFFC00000, !dbg !152
  %57 = select i1 %56, double %55, double 0x41DFFFFFFFC00000, !dbg !152
  call void @llvm.dbg.value(metadata double %57, metadata !82, metadata !DIExpression()), !dbg !151
  %58 = sitofp i32 %51 to double, !dbg !153
  %59 = fcmp olt double %57, %58, !dbg !153
  %60 = select i1 %59, double %57, double %58, !dbg !153
  %61 = fptosi double %60 to i32, !dbg !153
  call void @llvm.dbg.value(metadata i32 %61, metadata !91, metadata !DIExpression()), !dbg !144
  call void @llvm.dbg.value(metadata i32 %61, metadata !92, metadata !DIExpression()), !dbg !145
  store double* null, double** %6, align 8, !dbg !154, !tbaa !155
  call void @llvm.dbg.value(metadata i32* %16, metadata !90, metadata !DIExpression()), !dbg !157
  call void @llvm.dbg.value(metadata i32* %16, metadata !85, metadata !DIExpression()), !dbg !158
  %62 = zext i32 %28 to i64, !dbg !159
  %63 = getelementptr inbounds i32, i32* %16, i64 %62, !dbg !159
  call void @llvm.dbg.value(metadata i32* %63, metadata !90, metadata !DIExpression()), !dbg !157
  call void @llvm.dbg.value(metadata i32* %63, metadata !87, metadata !DIExpression()), !dbg !160
  %64 = getelementptr inbounds i32, i32* %63, i64 %62, !dbg !161
  call void @llvm.dbg.value(metadata i32* %64, metadata !90, metadata !DIExpression()), !dbg !157
  call void @llvm.dbg.value(metadata i32* %64, metadata !88, metadata !DIExpression()), !dbg !162
  %65 = getelementptr inbounds i32, i32* %64, i64 %62, !dbg !163
  call void @llvm.dbg.value(metadata i32* %65, metadata !90, metadata !DIExpression()), !dbg !157
  call void @llvm.dbg.value(metadata i32* %65, metadata !86, metadata !DIExpression()), !dbg !164
  %66 = getelementptr inbounds i32, i32* %65, i64 %62, !dbg !165
  call void @llvm.dbg.value(metadata i32* %66, metadata !90, metadata !DIExpression()), !dbg !157
  call void @llvm.dbg.value(metadata i32* %66, metadata !89, metadata !DIExpression()), !dbg !166
  %67 = sitofp i32 %61 to double, !dbg !167
  %68 = fmul double %67, 4.000000e+00, !dbg !167
  %69 = fmul double %68, 1.250000e-01, !dbg !167
  %70 = tail call double @llvm.ceil.f64(double %69), !dbg !167
  %71 = fmul double %67, 8.000000e+00, !dbg !168
  %72 = fmul double %71, 1.250000e-01, !dbg !168
  %73 = tail call double @llvm.ceil.f64(double %72), !dbg !168
  %74 = fadd double %70, %73, !dbg !169
  %75 = fadd double %70, %74, !dbg !170
  %76 = fadd double %73, %75, !dbg !171
  call void @llvm.dbg.value(metadata double %76, metadata !83, metadata !DIExpression()), !dbg !172
  %77 = fptoui double %76 to i64, !dbg !173
  call void @llvm.dbg.value(metadata i64 %77, metadata !95, metadata !DIExpression()), !dbg !174
  %78 = fmul double %76, 0x3FF0000002AF31DC, !dbg !175
  %79 = fcmp ole double %78, 0x41DFFFFFFFC00000, !dbg !175
  %80 = fcmp ord double %76, 0.000000e+00, !dbg !175
  %81 = and i1 %80, %79, !dbg !175
  br i1 %81, label %83, label %82, !dbg !176

; <label>:82:                                     ; preds = %46
  call void @llvm.dbg.value(metadata i8* %84, metadata !84, metadata !DIExpression()), !dbg !177
  store double* null, double** %25, align 8, !dbg !178, !tbaa !155
  br label %87, !dbg !179

; <label>:83:                                     ; preds = %46
  %84 = tail call i8* @klu_malloc(i64 %77, i64 8, %struct.klu_common_struct* %23) #4, !dbg !180
  call void @llvm.dbg.value(metadata i8* %84, metadata !84, metadata !DIExpression()), !dbg !177
  %85 = bitcast double** %25 to i8**, !dbg !178
  store i8* %84, i8** %85, align 8, !dbg !178, !tbaa !155
  %86 = icmp eq i8* %84, null, !dbg !181
  br i1 %86, label %87, label %89, !dbg !179

; <label>:87:                                     ; preds = %82, %83
  %88 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %23, i64 0, i32 11, !dbg !183
  store i32 -2, i32* %88, align 4, !dbg !185, !tbaa !186
  call void @llvm.dbg.value(metadata i64 0, metadata !95, metadata !DIExpression()), !dbg !174
  br label %103, !dbg !190

; <label>:89:                                     ; preds = %83
  call void @llvm.dbg.value(metadata double** %25, metadata !84, metadata !DIExpression()), !dbg !177
  %90 = call i64 @klu_kernel(i32 %28, i32* %1, i32* %2, double* %3, i32* %4, i64 %77, i32* %16, i32* %12, double** nonnull %25, double* %7, i32* %8, i32* %9, i32* %10, i32* %11, i32* %13, i32* %14, double* %15, i32* %63, i32* %64, i32* %66, i32* %65, i32 %17, i32* %18, double* %19, i32* %20, i32* %21, double* %22, %struct.klu_common_struct* %23) #4, !dbg !191
  call void @llvm.dbg.value(metadata i64 %90, metadata !95, metadata !DIExpression()), !dbg !174
  %91 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %23, i64 0, i32 11, !dbg !192
  %92 = load i32, i32* %91, align 4, !dbg !192, !tbaa !186
  %93 = icmp slt i32 %92, 0, !dbg !194
  br i1 %93, label %94, label %98, !dbg !195

; <label>:94:                                     ; preds = %89
  %95 = bitcast double** %25 to i8**, !dbg !196
  %96 = load i8*, i8** %95, align 8, !dbg !196, !tbaa !155
  call void @llvm.dbg.value(metadata double** %25, metadata !84, metadata !DIExpression(DW_OP_deref)), !dbg !177
  %97 = call i8* @klu_free(i8* %96, i64 %90, i64 8, %struct.klu_common_struct* nonnull %23) #4, !dbg !198
  call void @llvm.dbg.value(metadata i8* %97, metadata !84, metadata !DIExpression()), !dbg !177
  store i8* %97, i8** %95, align 8, !dbg !199, !tbaa !155
  call void @llvm.dbg.value(metadata i64 0, metadata !95, metadata !DIExpression()), !dbg !174
  br label %98, !dbg !200

; <label>:98:                                     ; preds = %94, %89
  %99 = phi i64 [ 0, %94 ], [ %90, %89 ], !dbg !201
  call void @llvm.dbg.value(metadata i64 %99, metadata !95, metadata !DIExpression()), !dbg !174
  %100 = bitcast double** %25 to i64*, !dbg !202
  %101 = load i64, i64* %100, align 8, !dbg !202, !tbaa !155
  call void @llvm.dbg.value(metadata double** %25, metadata !84, metadata !DIExpression(DW_OP_deref)), !dbg !177
  %102 = bitcast double** %6 to i64*, !dbg !203
  store i64 %101, i64* %102, align 8, !dbg !203, !tbaa !155
  br label %103, !dbg !204

; <label>:103:                                    ; preds = %98, %87
  %104 = phi i64 [ 0, %87 ], [ %99, %98 ], !dbg !201
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %26) #4, !dbg !205
  ret i64 %104, !dbg !205
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind readnone speculatable
declare double @llvm.ceil.f64(double) #2

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #3

declare i64 @klu_kernel(i32, i32*, i32*, double*, i32*, i64, i32*, i32*, double**, double*, i32*, i32*, i32*, i32*, i32*, i32*, double*, i32*, i32*, i32*, i32*, i32, i32*, double*, i32*, i32*, double*, %struct.klu_common_struct*) local_unnamed_addr #3

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #3

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define void @klu_lsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #0 !dbg !206 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !210, metadata !DIExpression()), !dbg !248
  call void @llvm.dbg.value(metadata i32* %1, metadata !211, metadata !DIExpression()), !dbg !249
  call void @llvm.dbg.value(metadata i32* %2, metadata !212, metadata !DIExpression()), !dbg !250
  call void @llvm.dbg.value(metadata double* %3, metadata !213, metadata !DIExpression()), !dbg !251
  call void @llvm.dbg.value(metadata i32 %4, metadata !214, metadata !DIExpression()), !dbg !252
  call void @llvm.dbg.value(metadata double* %5, metadata !215, metadata !DIExpression()), !dbg !253
  switch i32 %4, label %232 [
    i32 1, label %7
    i32 2, label %46
    i32 3, label %96
    i32 4, label %159
  ], !dbg !254

; <label>:7:                                      ; preds = %6
  call void @llvm.dbg.value(metadata i32 0, metadata !223, metadata !DIExpression()), !dbg !255
  %8 = icmp sgt i32 %0, 0, !dbg !256
  br i1 %8, label %9, label %232, !dbg !257

; <label>:9:                                      ; preds = %7
  %10 = zext i32 %0 to i64
  br label %11, !dbg !257

; <label>:11:                                     ; preds = %43, %9
  %12 = phi i64 [ 0, %9 ], [ %44, %43 ]
  call void @llvm.dbg.value(metadata i64 %12, metadata !223, metadata !DIExpression()), !dbg !255
  %13 = getelementptr inbounds double, double* %5, i64 %12, !dbg !258
  %14 = load double, double* %13, align 8, !dbg !258, !tbaa !259
  call void @llvm.dbg.value(metadata double %14, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !260
  %15 = getelementptr inbounds i32, i32* %1, i64 %12, !dbg !261
  %16 = load i32, i32* %15, align 4, !dbg !261, !tbaa !127
  %17 = sext i32 %16 to i64, !dbg !261
  %18 = getelementptr inbounds double, double* %3, i64 %17, !dbg !261
  call void @llvm.dbg.value(metadata double* %18, metadata !227, metadata !DIExpression()), !dbg !261
  %19 = getelementptr inbounds i32, i32* %2, i64 %12, !dbg !261
  %20 = load i32, i32* %19, align 4, !dbg !261, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %20, metadata !225, metadata !DIExpression()), !dbg !262
  %21 = bitcast double* %18 to i32*, !dbg !261
  call void @llvm.dbg.value(metadata i32* %21, metadata !221, metadata !DIExpression()), !dbg !263
  %22 = sext i32 %20 to i64, !dbg !261
  %23 = shl nsw i64 %22, 2, !dbg !261
  %24 = add nsw i64 %23, 7, !dbg !261
  %25 = lshr i64 %24, 3, !dbg !261
  %26 = getelementptr inbounds double, double* %18, i64 %25, !dbg !261
  call void @llvm.dbg.value(metadata double* %26, metadata !222, metadata !DIExpression()), !dbg !264
  call void @llvm.dbg.value(metadata i32 0, metadata !224, metadata !DIExpression()), !dbg !265
  %27 = icmp sgt i32 %20, 0, !dbg !266
  br i1 %27, label %28, label %43, !dbg !269

; <label>:28:                                     ; preds = %11
  %29 = zext i32 %20 to i64
  br label %30, !dbg !269

; <label>:30:                                     ; preds = %30, %28
  %31 = phi i64 [ 0, %28 ], [ %41, %30 ]
  call void @llvm.dbg.value(metadata i64 %31, metadata !224, metadata !DIExpression()), !dbg !265
  %32 = getelementptr inbounds double, double* %26, i64 %31, !dbg !270
  %33 = load double, double* %32, align 8, !dbg !270, !tbaa !259
  %34 = fmul double %14, %33, !dbg !270
  %35 = getelementptr inbounds i32, i32* %21, i64 %31, !dbg !270
  %36 = load i32, i32* %35, align 4, !dbg !270, !tbaa !127
  %37 = sext i32 %36 to i64, !dbg !270
  %38 = getelementptr inbounds double, double* %5, i64 %37, !dbg !270
  %39 = load double, double* %38, align 8, !dbg !270, !tbaa !259
  %40 = fsub double %39, %34, !dbg !270
  store double %40, double* %38, align 8, !dbg !270, !tbaa !259
  %41 = add nuw nsw i64 %31, 1, !dbg !273
  call void @llvm.dbg.value(metadata i32 undef, metadata !224, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !265
  %42 = icmp eq i64 %41, %29, !dbg !266
  br i1 %42, label %43, label %30, !dbg !269, !llvm.loop !274

; <label>:43:                                     ; preds = %30, %11
  %44 = add nuw nsw i64 %12, 1, !dbg !276
  call void @llvm.dbg.value(metadata i32 undef, metadata !223, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !255
  %45 = icmp eq i64 %44, %10, !dbg !256
  br i1 %45, label %232, label %11, !dbg !257, !llvm.loop !277

; <label>:46:                                     ; preds = %6
  call void @llvm.dbg.value(metadata i32 0, metadata !223, metadata !DIExpression()), !dbg !255
  %47 = icmp sgt i32 %0, 0, !dbg !279
  br i1 %47, label %48, label %232, !dbg !280

; <label>:48:                                     ; preds = %46
  %49 = zext i32 %0 to i64
  br label %50, !dbg !280

; <label>:50:                                     ; preds = %93, %48
  %51 = phi i64 [ 0, %48 ], [ %94, %93 ]
  call void @llvm.dbg.value(metadata i64 %51, metadata !223, metadata !DIExpression()), !dbg !255
  %52 = shl nuw nsw i64 %51, 1, !dbg !281
  %53 = getelementptr inbounds double, double* %5, i64 %52, !dbg !282
  %54 = load double, double* %53, align 8, !dbg !282, !tbaa !259
  call void @llvm.dbg.value(metadata double %54, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !260
  %55 = or i64 %52, 1, !dbg !283
  %56 = getelementptr inbounds double, double* %5, i64 %55, !dbg !284
  %57 = load double, double* %56, align 8, !dbg !284, !tbaa !259
  call void @llvm.dbg.value(metadata double %57, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !260
  %58 = getelementptr inbounds i32, i32* %1, i64 %51, !dbg !285
  %59 = load i32, i32* %58, align 4, !dbg !285, !tbaa !127
  %60 = sext i32 %59 to i64, !dbg !285
  %61 = getelementptr inbounds double, double* %3, i64 %60, !dbg !285
  call void @llvm.dbg.value(metadata double* %61, metadata !233, metadata !DIExpression()), !dbg !285
  %62 = getelementptr inbounds i32, i32* %2, i64 %51, !dbg !285
  %63 = load i32, i32* %62, align 4, !dbg !285, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %63, metadata !225, metadata !DIExpression()), !dbg !262
  %64 = bitcast double* %61 to i32*, !dbg !285
  call void @llvm.dbg.value(metadata i32* %64, metadata !221, metadata !DIExpression()), !dbg !263
  %65 = sext i32 %63 to i64, !dbg !285
  %66 = shl nsw i64 %65, 2, !dbg !285
  %67 = add nsw i64 %66, 7, !dbg !285
  %68 = lshr i64 %67, 3, !dbg !285
  %69 = getelementptr inbounds double, double* %61, i64 %68, !dbg !285
  call void @llvm.dbg.value(metadata double* %69, metadata !222, metadata !DIExpression()), !dbg !264
  call void @llvm.dbg.value(metadata i32 0, metadata !224, metadata !DIExpression()), !dbg !265
  %70 = icmp sgt i32 %63, 0, !dbg !286
  br i1 %70, label %71, label %93, !dbg !289

; <label>:71:                                     ; preds = %50
  %72 = zext i32 %63 to i64
  br label %73, !dbg !289

; <label>:73:                                     ; preds = %73, %71
  %74 = phi i64 [ 0, %71 ], [ %91, %73 ]
  call void @llvm.dbg.value(metadata i64 %74, metadata !224, metadata !DIExpression()), !dbg !265
  %75 = getelementptr inbounds i32, i32* %64, i64 %74, !dbg !290
  %76 = load i32, i32* %75, align 4, !dbg !290, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %76, metadata !226, metadata !DIExpression()), !dbg !292
  %77 = getelementptr inbounds double, double* %69, i64 %74, !dbg !293
  %78 = load double, double* %77, align 8, !dbg !293, !tbaa !259
  call void @llvm.dbg.value(metadata double %78, metadata !220, metadata !DIExpression()), !dbg !294
  %79 = fmul double %54, %78, !dbg !295
  %80 = shl nsw i32 %76, 1, !dbg !295
  %81 = sext i32 %80 to i64, !dbg !295
  %82 = getelementptr inbounds double, double* %5, i64 %81, !dbg !295
  %83 = load double, double* %82, align 8, !dbg !295, !tbaa !259
  %84 = fsub double %83, %79, !dbg !295
  store double %84, double* %82, align 8, !dbg !295, !tbaa !259
  %85 = fmul double %57, %78, !dbg !297
  %86 = or i32 %80, 1, !dbg !297
  %87 = sext i32 %86 to i64, !dbg !297
  %88 = getelementptr inbounds double, double* %5, i64 %87, !dbg !297
  %89 = load double, double* %88, align 8, !dbg !297, !tbaa !259
  %90 = fsub double %89, %85, !dbg !297
  store double %90, double* %88, align 8, !dbg !297, !tbaa !259
  %91 = add nuw nsw i64 %74, 1, !dbg !299
  call void @llvm.dbg.value(metadata i32 undef, metadata !224, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !265
  %92 = icmp eq i64 %91, %72, !dbg !286
  br i1 %92, label %93, label %73, !dbg !289, !llvm.loop !300

; <label>:93:                                     ; preds = %73, %50
  %94 = add nuw nsw i64 %51, 1, !dbg !302
  call void @llvm.dbg.value(metadata i32 undef, metadata !223, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !255
  %95 = icmp eq i64 %94, %49, !dbg !279
  br i1 %95, label %232, label %50, !dbg !280, !llvm.loop !303

; <label>:96:                                     ; preds = %6
  call void @llvm.dbg.value(metadata i32 0, metadata !223, metadata !DIExpression()), !dbg !255
  %97 = icmp sgt i32 %0, 0, !dbg !305
  br i1 %97, label %98, label %232, !dbg !306

; <label>:98:                                     ; preds = %96
  %99 = zext i32 %0 to i64
  br label %100, !dbg !306

; <label>:100:                                    ; preds = %156, %98
  %101 = phi i64 [ 0, %98 ], [ %157, %156 ]
  call void @llvm.dbg.value(metadata i64 %101, metadata !223, metadata !DIExpression()), !dbg !255
  %102 = trunc i64 %101 to i32, !dbg !307
  %103 = mul nsw i32 %102, 3, !dbg !307
  %104 = zext i32 %103 to i64, !dbg !308
  %105 = getelementptr inbounds double, double* %5, i64 %104, !dbg !308
  %106 = load double, double* %105, align 8, !dbg !308, !tbaa !259
  call void @llvm.dbg.value(metadata double %106, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !260
  %107 = add nuw nsw i32 %103, 1, !dbg !309
  %108 = zext i32 %107 to i64, !dbg !310
  %109 = getelementptr inbounds double, double* %5, i64 %108, !dbg !310
  %110 = load double, double* %109, align 8, !dbg !310, !tbaa !259
  call void @llvm.dbg.value(metadata double %110, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !260
  %111 = add nuw nsw i32 %103, 2, !dbg !311
  %112 = zext i32 %111 to i64, !dbg !312
  %113 = getelementptr inbounds double, double* %5, i64 %112, !dbg !312
  %114 = load double, double* %113, align 8, !dbg !312, !tbaa !259
  call void @llvm.dbg.value(metadata double %114, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !260
  %115 = getelementptr inbounds i32, i32* %1, i64 %101, !dbg !313
  %116 = load i32, i32* %115, align 4, !dbg !313, !tbaa !127
  %117 = sext i32 %116 to i64, !dbg !313
  %118 = getelementptr inbounds double, double* %3, i64 %117, !dbg !313
  call void @llvm.dbg.value(metadata double* %118, metadata !238, metadata !DIExpression()), !dbg !313
  %119 = getelementptr inbounds i32, i32* %2, i64 %101, !dbg !313
  %120 = load i32, i32* %119, align 4, !dbg !313, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %120, metadata !225, metadata !DIExpression()), !dbg !262
  %121 = bitcast double* %118 to i32*, !dbg !313
  call void @llvm.dbg.value(metadata i32* %121, metadata !221, metadata !DIExpression()), !dbg !263
  %122 = sext i32 %120 to i64, !dbg !313
  %123 = shl nsw i64 %122, 2, !dbg !313
  %124 = add nsw i64 %123, 7, !dbg !313
  %125 = lshr i64 %124, 3, !dbg !313
  %126 = getelementptr inbounds double, double* %118, i64 %125, !dbg !313
  call void @llvm.dbg.value(metadata double* %126, metadata !222, metadata !DIExpression()), !dbg !264
  call void @llvm.dbg.value(metadata i32 0, metadata !224, metadata !DIExpression()), !dbg !265
  %127 = icmp sgt i32 %120, 0, !dbg !314
  br i1 %127, label %128, label %156, !dbg !317

; <label>:128:                                    ; preds = %100
  %129 = zext i32 %120 to i64
  br label %130, !dbg !317

; <label>:130:                                    ; preds = %130, %128
  %131 = phi i64 [ 0, %128 ], [ %154, %130 ]
  call void @llvm.dbg.value(metadata i64 %131, metadata !224, metadata !DIExpression()), !dbg !265
  %132 = getelementptr inbounds i32, i32* %121, i64 %131, !dbg !318
  %133 = load i32, i32* %132, align 4, !dbg !318, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %133, metadata !226, metadata !DIExpression()), !dbg !292
  %134 = getelementptr inbounds double, double* %126, i64 %131, !dbg !320
  %135 = load double, double* %134, align 8, !dbg !320, !tbaa !259
  call void @llvm.dbg.value(metadata double %135, metadata !220, metadata !DIExpression()), !dbg !294
  %136 = fmul double %106, %135, !dbg !321
  %137 = mul nsw i32 %133, 3, !dbg !321
  %138 = sext i32 %137 to i64, !dbg !321
  %139 = getelementptr inbounds double, double* %5, i64 %138, !dbg !321
  %140 = load double, double* %139, align 8, !dbg !321, !tbaa !259
  %141 = fsub double %140, %136, !dbg !321
  store double %141, double* %139, align 8, !dbg !321, !tbaa !259
  %142 = fmul double %110, %135, !dbg !323
  %143 = add nsw i32 %137, 1, !dbg !323
  %144 = sext i32 %143 to i64, !dbg !323
  %145 = getelementptr inbounds double, double* %5, i64 %144, !dbg !323
  %146 = load double, double* %145, align 8, !dbg !323, !tbaa !259
  %147 = fsub double %146, %142, !dbg !323
  store double %147, double* %145, align 8, !dbg !323, !tbaa !259
  %148 = fmul double %114, %135, !dbg !325
  %149 = add nsw i32 %137, 2, !dbg !325
  %150 = sext i32 %149 to i64, !dbg !325
  %151 = getelementptr inbounds double, double* %5, i64 %150, !dbg !325
  %152 = load double, double* %151, align 8, !dbg !325, !tbaa !259
  %153 = fsub double %152, %148, !dbg !325
  store double %153, double* %151, align 8, !dbg !325, !tbaa !259
  %154 = add nuw nsw i64 %131, 1, !dbg !327
  call void @llvm.dbg.value(metadata i32 undef, metadata !224, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !265
  %155 = icmp eq i64 %154, %129, !dbg !314
  br i1 %155, label %156, label %130, !dbg !317, !llvm.loop !328

; <label>:156:                                    ; preds = %130, %100
  %157 = add nuw nsw i64 %101, 1, !dbg !330
  call void @llvm.dbg.value(metadata i32 undef, metadata !223, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !255
  %158 = icmp eq i64 %157, %99, !dbg !305
  br i1 %158, label %232, label %100, !dbg !306, !llvm.loop !331

; <label>:159:                                    ; preds = %6
  call void @llvm.dbg.value(metadata i32 0, metadata !223, metadata !DIExpression()), !dbg !255
  %160 = icmp sgt i32 %0, 0, !dbg !333
  br i1 %160, label %161, label %232, !dbg !334

; <label>:161:                                    ; preds = %159
  %162 = zext i32 %0 to i64
  br label %163, !dbg !334

; <label>:163:                                    ; preds = %229, %161
  %164 = phi i64 [ 0, %161 ], [ %230, %229 ]
  call void @llvm.dbg.value(metadata i64 %164, metadata !223, metadata !DIExpression()), !dbg !255
  %165 = trunc i64 %164 to i32, !dbg !335
  %166 = shl nsw i32 %165, 2, !dbg !335
  %167 = zext i32 %166 to i64, !dbg !336
  %168 = getelementptr inbounds double, double* %5, i64 %167, !dbg !336
  %169 = load double, double* %168, align 8, !dbg !336, !tbaa !259
  call void @llvm.dbg.value(metadata double %169, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !260
  %170 = or i32 %166, 1, !dbg !337
  %171 = zext i32 %170 to i64, !dbg !338
  %172 = getelementptr inbounds double, double* %5, i64 %171, !dbg !338
  %173 = load double, double* %172, align 8, !dbg !338, !tbaa !259
  call void @llvm.dbg.value(metadata double %173, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !260
  %174 = or i32 %166, 2, !dbg !339
  %175 = zext i32 %174 to i64, !dbg !340
  %176 = getelementptr inbounds double, double* %5, i64 %175, !dbg !340
  %177 = load double, double* %176, align 8, !dbg !340, !tbaa !259
  call void @llvm.dbg.value(metadata double %177, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !260
  %178 = or i32 %166, 3, !dbg !341
  %179 = zext i32 %178 to i64, !dbg !342
  %180 = getelementptr inbounds double, double* %5, i64 %179, !dbg !342
  %181 = load double, double* %180, align 8, !dbg !342, !tbaa !259
  call void @llvm.dbg.value(metadata double %181, metadata !216, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !260
  %182 = getelementptr inbounds i32, i32* %1, i64 %164, !dbg !343
  %183 = load i32, i32* %182, align 4, !dbg !343, !tbaa !127
  %184 = sext i32 %183 to i64, !dbg !343
  %185 = getelementptr inbounds double, double* %3, i64 %184, !dbg !343
  call void @llvm.dbg.value(metadata double* %185, metadata !243, metadata !DIExpression()), !dbg !343
  %186 = getelementptr inbounds i32, i32* %2, i64 %164, !dbg !343
  %187 = load i32, i32* %186, align 4, !dbg !343, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %187, metadata !225, metadata !DIExpression()), !dbg !262
  %188 = bitcast double* %185 to i32*, !dbg !343
  call void @llvm.dbg.value(metadata i32* %188, metadata !221, metadata !DIExpression()), !dbg !263
  %189 = sext i32 %187 to i64, !dbg !343
  %190 = shl nsw i64 %189, 2, !dbg !343
  %191 = add nsw i64 %190, 7, !dbg !343
  %192 = lshr i64 %191, 3, !dbg !343
  %193 = getelementptr inbounds double, double* %185, i64 %192, !dbg !343
  call void @llvm.dbg.value(metadata double* %193, metadata !222, metadata !DIExpression()), !dbg !264
  call void @llvm.dbg.value(metadata i32 0, metadata !224, metadata !DIExpression()), !dbg !265
  %194 = icmp sgt i32 %187, 0, !dbg !344
  br i1 %194, label %195, label %229, !dbg !347

; <label>:195:                                    ; preds = %163
  %196 = zext i32 %187 to i64
  br label %197, !dbg !347

; <label>:197:                                    ; preds = %197, %195
  %198 = phi i64 [ 0, %195 ], [ %227, %197 ]
  call void @llvm.dbg.value(metadata i64 %198, metadata !224, metadata !DIExpression()), !dbg !265
  %199 = getelementptr inbounds i32, i32* %188, i64 %198, !dbg !348
  %200 = load i32, i32* %199, align 4, !dbg !348, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %200, metadata !226, metadata !DIExpression()), !dbg !292
  %201 = getelementptr inbounds double, double* %193, i64 %198, !dbg !350
  %202 = load double, double* %201, align 8, !dbg !350, !tbaa !259
  call void @llvm.dbg.value(metadata double %202, metadata !220, metadata !DIExpression()), !dbg !294
  %203 = fmul double %169, %202, !dbg !351
  %204 = shl nsw i32 %200, 2, !dbg !351
  %205 = sext i32 %204 to i64, !dbg !351
  %206 = getelementptr inbounds double, double* %5, i64 %205, !dbg !351
  %207 = load double, double* %206, align 8, !dbg !351, !tbaa !259
  %208 = fsub double %207, %203, !dbg !351
  store double %208, double* %206, align 8, !dbg !351, !tbaa !259
  %209 = fmul double %173, %202, !dbg !353
  %210 = or i32 %204, 1, !dbg !353
  %211 = sext i32 %210 to i64, !dbg !353
  %212 = getelementptr inbounds double, double* %5, i64 %211, !dbg !353
  %213 = load double, double* %212, align 8, !dbg !353, !tbaa !259
  %214 = fsub double %213, %209, !dbg !353
  store double %214, double* %212, align 8, !dbg !353, !tbaa !259
  %215 = fmul double %177, %202, !dbg !355
  %216 = or i32 %204, 2, !dbg !355
  %217 = sext i32 %216 to i64, !dbg !355
  %218 = getelementptr inbounds double, double* %5, i64 %217, !dbg !355
  %219 = load double, double* %218, align 8, !dbg !355, !tbaa !259
  %220 = fsub double %219, %215, !dbg !355
  store double %220, double* %218, align 8, !dbg !355, !tbaa !259
  %221 = fmul double %181, %202, !dbg !357
  %222 = or i32 %204, 3, !dbg !357
  %223 = sext i32 %222 to i64, !dbg !357
  %224 = getelementptr inbounds double, double* %5, i64 %223, !dbg !357
  %225 = load double, double* %224, align 8, !dbg !357, !tbaa !259
  %226 = fsub double %225, %221, !dbg !357
  store double %226, double* %224, align 8, !dbg !357, !tbaa !259
  %227 = add nuw nsw i64 %198, 1, !dbg !359
  call void @llvm.dbg.value(metadata i32 undef, metadata !224, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !265
  %228 = icmp eq i64 %227, %196, !dbg !344
  br i1 %228, label %229, label %197, !dbg !347, !llvm.loop !360

; <label>:229:                                    ; preds = %197, %163
  %230 = add nuw nsw i64 %164, 1, !dbg !362
  call void @llvm.dbg.value(metadata i32 undef, metadata !223, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !255
  %231 = icmp eq i64 %230, %162, !dbg !333
  br i1 %231, label %232, label %163, !dbg !334, !llvm.loop !363

; <label>:232:                                    ; preds = %229, %156, %93, %43, %159, %96, %46, %7, %6
  ret void, !dbg !365
}

; Function Attrs: nounwind ssp uwtable
define void @klu_usolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #0 !dbg !366 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !370, metadata !DIExpression()), !dbg !407
  call void @llvm.dbg.value(metadata i32* %1, metadata !371, metadata !DIExpression()), !dbg !408
  call void @llvm.dbg.value(metadata i32* %2, metadata !372, metadata !DIExpression()), !dbg !409
  call void @llvm.dbg.value(metadata double* %3, metadata !373, metadata !DIExpression()), !dbg !410
  call void @llvm.dbg.value(metadata double* %4, metadata !374, metadata !DIExpression()), !dbg !411
  call void @llvm.dbg.value(metadata i32 %5, metadata !375, metadata !DIExpression()), !dbg !412
  call void @llvm.dbg.value(metadata double* %6, metadata !376, metadata !DIExpression()), !dbg !413
  switch i32 %5, label %252 [
    i32 1, label %8
    i32 2, label %50
    i32 3, label %108
    i32 4, label %172
  ], !dbg !414

; <label>:8:                                      ; preds = %7
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %9 = icmp sgt i32 %0, 0, !dbg !416
  br i1 %9, label %10, label %252, !dbg !417

; <label>:10:                                     ; preds = %8
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %11 = sext i32 %0 to i64, !dbg !417
  br label %12, !dbg !417

; <label>:12:                                     ; preds = %10, %48
  %13 = phi i64 [ %11, %10 ], [ %14, %48 ]
  %14 = add nsw i64 %13, -1, !dbg !418
  %15 = getelementptr inbounds i32, i32* %1, i64 %14, !dbg !419
  %16 = load i32, i32* %15, align 4, !dbg !419, !tbaa !127
  %17 = sext i32 %16 to i64, !dbg !419
  %18 = getelementptr inbounds double, double* %3, i64 %17, !dbg !419
  call void @llvm.dbg.value(metadata double* %18, metadata !386, metadata !DIExpression()), !dbg !419
  %19 = getelementptr inbounds i32, i32* %2, i64 %14, !dbg !419
  %20 = load i32, i32* %19, align 4, !dbg !419, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %20, metadata !384, metadata !DIExpression()), !dbg !420
  %21 = bitcast double* %18 to i32*, !dbg !419
  call void @llvm.dbg.value(metadata i32* %21, metadata !380, metadata !DIExpression()), !dbg !421
  %22 = sext i32 %20 to i64, !dbg !419
  %23 = shl nsw i64 %22, 2, !dbg !419
  %24 = add nsw i64 %23, 7, !dbg !419
  %25 = lshr i64 %24, 3, !dbg !419
  %26 = getelementptr inbounds double, double* %18, i64 %25, !dbg !419
  call void @llvm.dbg.value(metadata double* %26, metadata !381, metadata !DIExpression()), !dbg !422
  %27 = getelementptr inbounds double, double* %6, i64 %14, !dbg !423
  %28 = load double, double* %27, align 8, !dbg !423, !tbaa !259
  %29 = getelementptr inbounds double, double* %4, i64 %14, !dbg !423
  %30 = load double, double* %29, align 8, !dbg !423, !tbaa !259
  %31 = fdiv double %28, %30, !dbg !423
  call void @llvm.dbg.value(metadata double %31, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !425
  store double %31, double* %27, align 8, !dbg !426, !tbaa !259
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !427
  %32 = icmp sgt i32 %20, 0, !dbg !428
  br i1 %32, label %33, label %48, !dbg !431

; <label>:33:                                     ; preds = %12
  %34 = zext i32 %20 to i64
  br label %35, !dbg !431

; <label>:35:                                     ; preds = %35, %33
  %36 = phi i64 [ 0, %33 ], [ %46, %35 ]
  call void @llvm.dbg.value(metadata i64 %36, metadata !383, metadata !DIExpression()), !dbg !427
  %37 = getelementptr inbounds double, double* %26, i64 %36, !dbg !432
  %38 = load double, double* %37, align 8, !dbg !432, !tbaa !259
  %39 = fmul double %31, %38, !dbg !432
  %40 = getelementptr inbounds i32, i32* %21, i64 %36, !dbg !432
  %41 = load i32, i32* %40, align 4, !dbg !432, !tbaa !127
  %42 = sext i32 %41 to i64, !dbg !432
  %43 = getelementptr inbounds double, double* %6, i64 %42, !dbg !432
  %44 = load double, double* %43, align 8, !dbg !432, !tbaa !259
  %45 = fsub double %44, %39, !dbg !432
  store double %45, double* %43, align 8, !dbg !432, !tbaa !259
  %46 = add nuw nsw i64 %36, 1, !dbg !435
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !427
  %47 = icmp eq i64 %46, %34, !dbg !428
  br i1 %47, label %48, label %35, !dbg !431, !llvm.loop !436

; <label>:48:                                     ; preds = %35, %12
  call void @llvm.dbg.value(metadata i32 undef, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  call void @llvm.dbg.value(metadata i32 undef, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %49 = icmp sgt i64 %13, 1, !dbg !416
  br i1 %49, label %12, label %252, !dbg !417, !llvm.loop !438

; <label>:50:                                     ; preds = %7
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %51 = icmp sgt i32 %0, 0, !dbg !440
  br i1 %51, label %52, label %252, !dbg !441

; <label>:52:                                     ; preds = %50
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %53 = sext i32 %0 to i64, !dbg !441
  br label %54, !dbg !441

; <label>:54:                                     ; preds = %52, %106
  %55 = phi i64 [ %53, %52 ], [ %57, %106 ]
  %56 = phi i32 [ %0, %52 ], [ %58, %106 ]
  %57 = add nsw i64 %55, -1, !dbg !442
  %58 = add nsw i32 %56, -1, !dbg !442
  %59 = getelementptr inbounds i32, i32* %1, i64 %57, !dbg !443
  %60 = load i32, i32* %59, align 4, !dbg !443, !tbaa !127
  %61 = sext i32 %60 to i64, !dbg !443
  %62 = getelementptr inbounds double, double* %3, i64 %61, !dbg !443
  call void @llvm.dbg.value(metadata double* %62, metadata !392, metadata !DIExpression()), !dbg !443
  %63 = getelementptr inbounds i32, i32* %2, i64 %57, !dbg !443
  %64 = load i32, i32* %63, align 4, !dbg !443, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %64, metadata !384, metadata !DIExpression()), !dbg !420
  %65 = bitcast double* %62 to i32*, !dbg !443
  call void @llvm.dbg.value(metadata i32* %65, metadata !380, metadata !DIExpression()), !dbg !421
  %66 = sext i32 %64 to i64, !dbg !443
  %67 = shl nsw i64 %66, 2, !dbg !443
  %68 = add nsw i64 %67, 7, !dbg !443
  %69 = lshr i64 %68, 3, !dbg !443
  %70 = getelementptr inbounds double, double* %62, i64 %69, !dbg !443
  call void @llvm.dbg.value(metadata double* %70, metadata !381, metadata !DIExpression()), !dbg !422
  %71 = getelementptr inbounds double, double* %4, i64 %57, !dbg !444
  %72 = load double, double* %71, align 8, !dbg !444, !tbaa !259
  call void @llvm.dbg.value(metadata double %72, metadata !379, metadata !DIExpression()), !dbg !445
  %73 = shl nsw i32 %58, 1, !dbg !446
  %74 = sext i32 %73 to i64, !dbg !446
  %75 = getelementptr inbounds double, double* %6, i64 %74, !dbg !446
  %76 = load double, double* %75, align 8, !dbg !446, !tbaa !259
  %77 = fdiv double %76, %72, !dbg !446
  call void @llvm.dbg.value(metadata double %77, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !425
  %78 = or i32 %73, 1, !dbg !448
  %79 = sext i32 %78 to i64, !dbg !448
  %80 = getelementptr inbounds double, double* %6, i64 %79, !dbg !448
  %81 = load double, double* %80, align 8, !dbg !448, !tbaa !259
  %82 = fdiv double %81, %72, !dbg !448
  call void @llvm.dbg.value(metadata double %82, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !425
  store double %77, double* %75, align 8, !dbg !450, !tbaa !259
  store double %82, double* %80, align 8, !dbg !451, !tbaa !259
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !427
  %83 = icmp sgt i32 %64, 0, !dbg !452
  br i1 %83, label %84, label %106, !dbg !455

; <label>:84:                                     ; preds = %54
  %85 = zext i32 %64 to i64
  br label %86, !dbg !455

; <label>:86:                                     ; preds = %86, %84
  %87 = phi i64 [ 0, %84 ], [ %104, %86 ]
  call void @llvm.dbg.value(metadata i64 %87, metadata !383, metadata !DIExpression()), !dbg !427
  %88 = getelementptr inbounds i32, i32* %65, i64 %87, !dbg !456
  %89 = load i32, i32* %88, align 4, !dbg !456, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %89, metadata !385, metadata !DIExpression()), !dbg !458
  %90 = getelementptr inbounds double, double* %70, i64 %87, !dbg !459
  %91 = load double, double* %90, align 8, !dbg !459, !tbaa !259
  call void @llvm.dbg.value(metadata double %91, metadata !378, metadata !DIExpression()), !dbg !460
  %92 = fmul double %77, %91, !dbg !461
  %93 = shl nsw i32 %89, 1, !dbg !461
  %94 = sext i32 %93 to i64, !dbg !461
  %95 = getelementptr inbounds double, double* %6, i64 %94, !dbg !461
  %96 = load double, double* %95, align 8, !dbg !461, !tbaa !259
  %97 = fsub double %96, %92, !dbg !461
  store double %97, double* %95, align 8, !dbg !461, !tbaa !259
  %98 = fmul double %82, %91, !dbg !463
  %99 = or i32 %93, 1, !dbg !463
  %100 = sext i32 %99 to i64, !dbg !463
  %101 = getelementptr inbounds double, double* %6, i64 %100, !dbg !463
  %102 = load double, double* %101, align 8, !dbg !463, !tbaa !259
  %103 = fsub double %102, %98, !dbg !463
  store double %103, double* %101, align 8, !dbg !463, !tbaa !259
  %104 = add nuw nsw i64 %87, 1, !dbg !465
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !427
  %105 = icmp eq i64 %104, %85, !dbg !452
  br i1 %105, label %106, label %86, !dbg !455, !llvm.loop !466

; <label>:106:                                    ; preds = %86, %54
  call void @llvm.dbg.value(metadata i32 %58, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  call void @llvm.dbg.value(metadata i32 %58, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %107 = icmp sgt i64 %55, 1, !dbg !440
  br i1 %107, label %54, label %252, !dbg !441, !llvm.loop !468

; <label>:108:                                    ; preds = %7
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %109 = icmp sgt i32 %0, 0, !dbg !470
  br i1 %109, label %110, label %252, !dbg !471

; <label>:110:                                    ; preds = %108
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %111 = sext i32 %0 to i64, !dbg !471
  br label %112, !dbg !471

; <label>:112:                                    ; preds = %110, %170
  %113 = phi i64 [ %111, %110 ], [ %114, %170 ]
  %114 = add nsw i64 %113, -1, !dbg !472
  %115 = getelementptr inbounds i32, i32* %1, i64 %114, !dbg !473
  %116 = load i32, i32* %115, align 4, !dbg !473, !tbaa !127
  %117 = sext i32 %116 to i64, !dbg !473
  %118 = getelementptr inbounds double, double* %3, i64 %117, !dbg !473
  call void @llvm.dbg.value(metadata double* %118, metadata !397, metadata !DIExpression()), !dbg !473
  %119 = getelementptr inbounds i32, i32* %2, i64 %114, !dbg !473
  %120 = load i32, i32* %119, align 4, !dbg !473, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %120, metadata !384, metadata !DIExpression()), !dbg !420
  %121 = bitcast double* %118 to i32*, !dbg !473
  call void @llvm.dbg.value(metadata i32* %121, metadata !380, metadata !DIExpression()), !dbg !421
  %122 = sext i32 %120 to i64, !dbg !473
  %123 = shl nsw i64 %122, 2, !dbg !473
  %124 = add nsw i64 %123, 7, !dbg !473
  %125 = lshr i64 %124, 3, !dbg !473
  %126 = getelementptr inbounds double, double* %118, i64 %125, !dbg !473
  call void @llvm.dbg.value(metadata double* %126, metadata !381, metadata !DIExpression()), !dbg !422
  %127 = getelementptr inbounds double, double* %4, i64 %114, !dbg !474
  %128 = load double, double* %127, align 8, !dbg !474, !tbaa !259
  call void @llvm.dbg.value(metadata double %128, metadata !379, metadata !DIExpression()), !dbg !445
  %129 = mul nsw i64 %114, 3, !dbg !475
  %130 = getelementptr inbounds double, double* %6, i64 %129, !dbg !475
  %131 = load double, double* %130, align 8, !dbg !475, !tbaa !259
  %132 = fdiv double %131, %128, !dbg !475
  call void @llvm.dbg.value(metadata double %132, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !425
  %133 = add nsw i64 %129, 1, !dbg !477
  %134 = getelementptr inbounds double, double* %6, i64 %133, !dbg !477
  %135 = load double, double* %134, align 8, !dbg !477, !tbaa !259
  %136 = fdiv double %135, %128, !dbg !477
  call void @llvm.dbg.value(metadata double %136, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !425
  %137 = add nsw i64 %129, 2, !dbg !479
  %138 = getelementptr inbounds double, double* %6, i64 %137, !dbg !479
  %139 = load double, double* %138, align 8, !dbg !479, !tbaa !259
  %140 = fdiv double %139, %128, !dbg !479
  call void @llvm.dbg.value(metadata double %140, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !425
  store double %132, double* %130, align 8, !dbg !481, !tbaa !259
  store double %136, double* %134, align 8, !dbg !482, !tbaa !259
  store double %140, double* %138, align 8, !dbg !483, !tbaa !259
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !427
  %141 = icmp sgt i32 %120, 0, !dbg !484
  br i1 %141, label %142, label %170, !dbg !487

; <label>:142:                                    ; preds = %112
  %143 = zext i32 %120 to i64
  br label %144, !dbg !487

; <label>:144:                                    ; preds = %144, %142
  %145 = phi i64 [ 0, %142 ], [ %168, %144 ]
  call void @llvm.dbg.value(metadata i64 %145, metadata !383, metadata !DIExpression()), !dbg !427
  %146 = getelementptr inbounds i32, i32* %121, i64 %145, !dbg !488
  %147 = load i32, i32* %146, align 4, !dbg !488, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %147, metadata !385, metadata !DIExpression()), !dbg !458
  %148 = getelementptr inbounds double, double* %126, i64 %145, !dbg !490
  %149 = load double, double* %148, align 8, !dbg !490, !tbaa !259
  call void @llvm.dbg.value(metadata double %149, metadata !378, metadata !DIExpression()), !dbg !460
  %150 = fmul double %132, %149, !dbg !491
  %151 = mul nsw i32 %147, 3, !dbg !491
  %152 = sext i32 %151 to i64, !dbg !491
  %153 = getelementptr inbounds double, double* %6, i64 %152, !dbg !491
  %154 = load double, double* %153, align 8, !dbg !491, !tbaa !259
  %155 = fsub double %154, %150, !dbg !491
  store double %155, double* %153, align 8, !dbg !491, !tbaa !259
  %156 = fmul double %136, %149, !dbg !493
  %157 = add nsw i32 %151, 1, !dbg !493
  %158 = sext i32 %157 to i64, !dbg !493
  %159 = getelementptr inbounds double, double* %6, i64 %158, !dbg !493
  %160 = load double, double* %159, align 8, !dbg !493, !tbaa !259
  %161 = fsub double %160, %156, !dbg !493
  store double %161, double* %159, align 8, !dbg !493, !tbaa !259
  %162 = fmul double %140, %149, !dbg !495
  %163 = add nsw i32 %151, 2, !dbg !495
  %164 = sext i32 %163 to i64, !dbg !495
  %165 = getelementptr inbounds double, double* %6, i64 %164, !dbg !495
  %166 = load double, double* %165, align 8, !dbg !495, !tbaa !259
  %167 = fsub double %166, %162, !dbg !495
  store double %167, double* %165, align 8, !dbg !495, !tbaa !259
  %168 = add nuw nsw i64 %145, 1, !dbg !497
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !427
  %169 = icmp eq i64 %168, %143, !dbg !484
  br i1 %169, label %170, label %144, !dbg !487, !llvm.loop !498

; <label>:170:                                    ; preds = %144, %112
  call void @llvm.dbg.value(metadata i32 undef, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  call void @llvm.dbg.value(metadata i32 undef, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %171 = icmp sgt i64 %113, 1, !dbg !470
  br i1 %171, label %112, label %252, !dbg !471, !llvm.loop !500

; <label>:172:                                    ; preds = %7
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %173 = icmp sgt i32 %0, 0, !dbg !502
  br i1 %173, label %174, label %252, !dbg !503

; <label>:174:                                    ; preds = %172
  call void @llvm.dbg.value(metadata i32 %0, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %175 = sext i32 %0 to i64, !dbg !503
  br label %176, !dbg !503

; <label>:176:                                    ; preds = %174, %250
  %177 = phi i64 [ %175, %174 ], [ %179, %250 ]
  %178 = phi i32 [ %0, %174 ], [ %180, %250 ]
  %179 = add nsw i64 %177, -1, !dbg !504
  %180 = add nsw i32 %178, -1, !dbg !504
  %181 = getelementptr inbounds i32, i32* %1, i64 %179, !dbg !505
  %182 = load i32, i32* %181, align 4, !dbg !505, !tbaa !127
  %183 = sext i32 %182 to i64, !dbg !505
  %184 = getelementptr inbounds double, double* %3, i64 %183, !dbg !505
  call void @llvm.dbg.value(metadata double* %184, metadata !402, metadata !DIExpression()), !dbg !505
  %185 = getelementptr inbounds i32, i32* %2, i64 %179, !dbg !505
  %186 = load i32, i32* %185, align 4, !dbg !505, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %186, metadata !384, metadata !DIExpression()), !dbg !420
  %187 = bitcast double* %184 to i32*, !dbg !505
  call void @llvm.dbg.value(metadata i32* %187, metadata !380, metadata !DIExpression()), !dbg !421
  %188 = sext i32 %186 to i64, !dbg !505
  %189 = shl nsw i64 %188, 2, !dbg !505
  %190 = add nsw i64 %189, 7, !dbg !505
  %191 = lshr i64 %190, 3, !dbg !505
  %192 = getelementptr inbounds double, double* %184, i64 %191, !dbg !505
  call void @llvm.dbg.value(metadata double* %192, metadata !381, metadata !DIExpression()), !dbg !422
  %193 = getelementptr inbounds double, double* %4, i64 %179, !dbg !506
  %194 = load double, double* %193, align 8, !dbg !506, !tbaa !259
  call void @llvm.dbg.value(metadata double %194, metadata !379, metadata !DIExpression()), !dbg !445
  %195 = shl nsw i32 %180, 2, !dbg !507
  %196 = sext i32 %195 to i64, !dbg !507
  %197 = getelementptr inbounds double, double* %6, i64 %196, !dbg !507
  %198 = load double, double* %197, align 8, !dbg !507, !tbaa !259
  %199 = fdiv double %198, %194, !dbg !507
  call void @llvm.dbg.value(metadata double %199, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !425
  %200 = or i32 %195, 1, !dbg !509
  %201 = sext i32 %200 to i64, !dbg !509
  %202 = getelementptr inbounds double, double* %6, i64 %201, !dbg !509
  %203 = load double, double* %202, align 8, !dbg !509, !tbaa !259
  %204 = fdiv double %203, %194, !dbg !509
  call void @llvm.dbg.value(metadata double %204, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !425
  %205 = or i32 %195, 2, !dbg !511
  %206 = sext i32 %205 to i64, !dbg !511
  %207 = getelementptr inbounds double, double* %6, i64 %206, !dbg !511
  %208 = load double, double* %207, align 8, !dbg !511, !tbaa !259
  %209 = fdiv double %208, %194, !dbg !511
  call void @llvm.dbg.value(metadata double %209, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !425
  %210 = or i32 %195, 3, !dbg !513
  %211 = sext i32 %210 to i64, !dbg !513
  %212 = getelementptr inbounds double, double* %6, i64 %211, !dbg !513
  %213 = load double, double* %212, align 8, !dbg !513, !tbaa !259
  %214 = fdiv double %213, %194, !dbg !513
  call void @llvm.dbg.value(metadata double %214, metadata !377, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !425
  store double %199, double* %197, align 8, !dbg !515, !tbaa !259
  store double %204, double* %202, align 8, !dbg !516, !tbaa !259
  store double %209, double* %207, align 8, !dbg !517, !tbaa !259
  store double %214, double* %212, align 8, !dbg !518, !tbaa !259
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !427
  %215 = icmp sgt i32 %186, 0, !dbg !519
  br i1 %215, label %216, label %250, !dbg !522

; <label>:216:                                    ; preds = %176
  %217 = zext i32 %186 to i64
  br label %218, !dbg !522

; <label>:218:                                    ; preds = %218, %216
  %219 = phi i64 [ 0, %216 ], [ %248, %218 ]
  call void @llvm.dbg.value(metadata i64 %219, metadata !383, metadata !DIExpression()), !dbg !427
  %220 = getelementptr inbounds i32, i32* %187, i64 %219, !dbg !523
  %221 = load i32, i32* %220, align 4, !dbg !523, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %221, metadata !385, metadata !DIExpression()), !dbg !458
  %222 = getelementptr inbounds double, double* %192, i64 %219, !dbg !525
  %223 = load double, double* %222, align 8, !dbg !525, !tbaa !259
  call void @llvm.dbg.value(metadata double %223, metadata !378, metadata !DIExpression()), !dbg !460
  %224 = fmul double %199, %223, !dbg !526
  %225 = shl nsw i32 %221, 2, !dbg !526
  %226 = sext i32 %225 to i64, !dbg !526
  %227 = getelementptr inbounds double, double* %6, i64 %226, !dbg !526
  %228 = load double, double* %227, align 8, !dbg !526, !tbaa !259
  %229 = fsub double %228, %224, !dbg !526
  store double %229, double* %227, align 8, !dbg !526, !tbaa !259
  %230 = fmul double %204, %223, !dbg !528
  %231 = or i32 %225, 1, !dbg !528
  %232 = sext i32 %231 to i64, !dbg !528
  %233 = getelementptr inbounds double, double* %6, i64 %232, !dbg !528
  %234 = load double, double* %233, align 8, !dbg !528, !tbaa !259
  %235 = fsub double %234, %230, !dbg !528
  store double %235, double* %233, align 8, !dbg !528, !tbaa !259
  %236 = fmul double %209, %223, !dbg !530
  %237 = or i32 %225, 2, !dbg !530
  %238 = sext i32 %237 to i64, !dbg !530
  %239 = getelementptr inbounds double, double* %6, i64 %238, !dbg !530
  %240 = load double, double* %239, align 8, !dbg !530, !tbaa !259
  %241 = fsub double %240, %236, !dbg !530
  store double %241, double* %239, align 8, !dbg !530, !tbaa !259
  %242 = fmul double %214, %223, !dbg !532
  %243 = or i32 %225, 3, !dbg !532
  %244 = sext i32 %243 to i64, !dbg !532
  %245 = getelementptr inbounds double, double* %6, i64 %244, !dbg !532
  %246 = load double, double* %245, align 8, !dbg !532, !tbaa !259
  %247 = fsub double %246, %242, !dbg !532
  store double %247, double* %245, align 8, !dbg !532, !tbaa !259
  %248 = add nuw nsw i64 %219, 1, !dbg !534
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !427
  %249 = icmp eq i64 %248, %217, !dbg !519
  br i1 %249, label %250, label %218, !dbg !522, !llvm.loop !535

; <label>:250:                                    ; preds = %218, %176
  call void @llvm.dbg.value(metadata i32 %180, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  call void @llvm.dbg.value(metadata i32 %180, metadata !382, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !415
  %251 = icmp sgt i64 %177, 1, !dbg !502
  br i1 %251, label %176, label %252, !dbg !503, !llvm.loop !537

; <label>:252:                                    ; preds = %250, %170, %106, %48, %172, %108, %50, %8, %7
  ret void, !dbg !539
}

; Function Attrs: nounwind ssp uwtable
define void @klu_ltsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #0 !dbg !540 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !542, metadata !DIExpression()), !dbg !577
  call void @llvm.dbg.value(metadata i32* %1, metadata !543, metadata !DIExpression()), !dbg !578
  call void @llvm.dbg.value(metadata i32* %2, metadata !544, metadata !DIExpression()), !dbg !579
  call void @llvm.dbg.value(metadata double* %3, metadata !545, metadata !DIExpression()), !dbg !580
  call void @llvm.dbg.value(metadata i32 %4, metadata !546, metadata !DIExpression()), !dbg !581
  call void @llvm.dbg.value(metadata double* %5, metadata !547, metadata !DIExpression()), !dbg !582
  switch i32 %4, label %253 [
    i32 1, label %7
    i32 2, label %48
    i32 3, label %106
    i32 4, label %171
  ], !dbg !583

; <label>:7:                                      ; preds = %6
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %8 = icmp sgt i32 %0, 0, !dbg !585
  br i1 %8, label %9, label %253, !dbg !586

; <label>:9:                                      ; preds = %7
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %10 = sext i32 %0 to i64, !dbg !586
  br label %11, !dbg !586

; <label>:11:                                     ; preds = %9, %45
  %12 = phi i64 [ %10, %9 ], [ %13, %45 ]
  %13 = add nsw i64 %12, -1, !dbg !587
  %14 = getelementptr inbounds i32, i32* %1, i64 %13, !dbg !588
  %15 = load i32, i32* %14, align 4, !dbg !588, !tbaa !127
  %16 = sext i32 %15 to i64, !dbg !588
  %17 = getelementptr inbounds double, double* %3, i64 %16, !dbg !588
  call void @llvm.dbg.value(metadata double* %17, metadata !556, metadata !DIExpression()), !dbg !588
  %18 = getelementptr inbounds i32, i32* %2, i64 %13, !dbg !588
  %19 = load i32, i32* %18, align 4, !dbg !588, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %19, metadata !554, metadata !DIExpression()), !dbg !589
  %20 = bitcast double* %17 to i32*, !dbg !588
  call void @llvm.dbg.value(metadata i32* %20, metadata !550, metadata !DIExpression()), !dbg !590
  %21 = sext i32 %19 to i64, !dbg !588
  %22 = shl nsw i64 %21, 2, !dbg !588
  %23 = add nsw i64 %22, 7, !dbg !588
  %24 = lshr i64 %23, 3, !dbg !588
  %25 = getelementptr inbounds double, double* %17, i64 %24, !dbg !588
  call void @llvm.dbg.value(metadata double* %25, metadata !551, metadata !DIExpression()), !dbg !591
  %26 = getelementptr inbounds double, double* %5, i64 %13, !dbg !592
  %27 = load double, double* %26, align 8, !dbg !592, !tbaa !259
  call void @llvm.dbg.value(metadata double %27, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i32 0, metadata !553, metadata !DIExpression()), !dbg !594
  %28 = icmp sgt i32 %19, 0, !dbg !595
  br i1 %28, label %29, label %45, !dbg !598

; <label>:29:                                     ; preds = %11
  %30 = zext i32 %19 to i64
  br label %31, !dbg !598

; <label>:31:                                     ; preds = %31, %29
  %32 = phi i64 [ 0, %29 ], [ %43, %31 ]
  %33 = phi double [ %27, %29 ], [ %42, %31 ]
  call void @llvm.dbg.value(metadata double %33, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i64 %32, metadata !553, metadata !DIExpression()), !dbg !594
  %34 = getelementptr inbounds double, double* %25, i64 %32, !dbg !599
  %35 = load double, double* %34, align 8, !dbg !599, !tbaa !259
  %36 = getelementptr inbounds i32, i32* %20, i64 %32, !dbg !599
  %37 = load i32, i32* %36, align 4, !dbg !599, !tbaa !127
  %38 = sext i32 %37 to i64, !dbg !599
  %39 = getelementptr inbounds double, double* %5, i64 %38, !dbg !599
  %40 = load double, double* %39, align 8, !dbg !599, !tbaa !259
  %41 = fmul double %35, %40, !dbg !599
  %42 = fsub double %33, %41, !dbg !599
  %43 = add nuw nsw i64 %32, 1, !dbg !603
  call void @llvm.dbg.value(metadata double %42, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i32 undef, metadata !553, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !594
  %44 = icmp eq i64 %43, %30, !dbg !595
  br i1 %44, label %45, label %31, !dbg !598, !llvm.loop !604

; <label>:45:                                     ; preds = %31, %11
  %46 = phi double [ %27, %11 ], [ %42, %31 ]
  call void @llvm.dbg.value(metadata double %46, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  store double %46, double* %26, align 8, !dbg !606, !tbaa !259
  call void @llvm.dbg.value(metadata i32 undef, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  call void @llvm.dbg.value(metadata i32 undef, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %47 = icmp sgt i64 %12, 1, !dbg !585
  br i1 %47, label %11, label %253, !dbg !586, !llvm.loop !607

; <label>:48:                                     ; preds = %6
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %49 = icmp sgt i32 %0, 0, !dbg !609
  br i1 %49, label %50, label %253, !dbg !610

; <label>:50:                                     ; preds = %48
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %51 = sext i32 %0 to i64, !dbg !610
  br label %52, !dbg !610

; <label>:52:                                     ; preds = %50, %102
  %53 = phi i64 [ %51, %50 ], [ %55, %102 ]
  %54 = phi i32 [ %0, %50 ], [ %56, %102 ]
  %55 = add nsw i64 %53, -1, !dbg !611
  %56 = add nsw i32 %54, -1, !dbg !611
  %57 = shl nsw i32 %56, 1, !dbg !612
  %58 = sext i32 %57 to i64, !dbg !613
  %59 = getelementptr inbounds double, double* %5, i64 %58, !dbg !613
  %60 = load double, double* %59, align 8, !dbg !613, !tbaa !259
  call void @llvm.dbg.value(metadata double %60, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %61 = or i32 %57, 1, !dbg !614
  %62 = sext i32 %61 to i64, !dbg !615
  %63 = getelementptr inbounds double, double* %5, i64 %62, !dbg !615
  %64 = load double, double* %63, align 8, !dbg !615, !tbaa !259
  call void @llvm.dbg.value(metadata double %64, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  %65 = getelementptr inbounds i32, i32* %1, i64 %55, !dbg !616
  %66 = load i32, i32* %65, align 4, !dbg !616, !tbaa !127
  %67 = sext i32 %66 to i64, !dbg !616
  %68 = getelementptr inbounds double, double* %3, i64 %67, !dbg !616
  call void @llvm.dbg.value(metadata double* %68, metadata !562, metadata !DIExpression()), !dbg !616
  %69 = getelementptr inbounds i32, i32* %2, i64 %55, !dbg !616
  %70 = load i32, i32* %69, align 4, !dbg !616, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %70, metadata !554, metadata !DIExpression()), !dbg !589
  %71 = bitcast double* %68 to i32*, !dbg !616
  call void @llvm.dbg.value(metadata i32* %71, metadata !550, metadata !DIExpression()), !dbg !590
  %72 = sext i32 %70 to i64, !dbg !616
  %73 = shl nsw i64 %72, 2, !dbg !616
  %74 = add nsw i64 %73, 7, !dbg !616
  %75 = lshr i64 %74, 3, !dbg !616
  %76 = getelementptr inbounds double, double* %68, i64 %75, !dbg !616
  call void @llvm.dbg.value(metadata double* %76, metadata !551, metadata !DIExpression()), !dbg !591
  call void @llvm.dbg.value(metadata i32 0, metadata !553, metadata !DIExpression()), !dbg !594
  call void @llvm.dbg.value(metadata double %64, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %60, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %77 = icmp sgt i32 %70, 0, !dbg !617
  br i1 %77, label %78, label %102, !dbg !620

; <label>:78:                                     ; preds = %52
  %79 = zext i32 %70 to i64
  br label %80, !dbg !620

; <label>:80:                                     ; preds = %80, %78
  %81 = phi i64 [ 0, %78 ], [ %100, %80 ]
  %82 = phi double [ %64, %78 ], [ %99, %80 ]
  %83 = phi double [ %60, %78 ], [ %93, %80 ]
  call void @llvm.dbg.value(metadata double %82, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %83, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i64 %81, metadata !553, metadata !DIExpression()), !dbg !594
  %84 = getelementptr inbounds i32, i32* %71, i64 %81, !dbg !621
  %85 = load i32, i32* %84, align 4, !dbg !621, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %85, metadata !555, metadata !DIExpression()), !dbg !623
  %86 = getelementptr inbounds double, double* %76, i64 %81, !dbg !624
  %87 = load double, double* %86, align 8, !dbg !624, !tbaa !259
  call void @llvm.dbg.value(metadata double %87, metadata !549, metadata !DIExpression()), !dbg !626
  %88 = shl nsw i32 %85, 1, !dbg !627
  %89 = sext i32 %88 to i64, !dbg !627
  %90 = getelementptr inbounds double, double* %5, i64 %89, !dbg !627
  %91 = load double, double* %90, align 8, !dbg !627, !tbaa !259
  %92 = fmul double %87, %91, !dbg !627
  %93 = fsub double %83, %92, !dbg !627
  %94 = or i32 %88, 1, !dbg !629
  %95 = sext i32 %94 to i64, !dbg !629
  %96 = getelementptr inbounds double, double* %5, i64 %95, !dbg !629
  %97 = load double, double* %96, align 8, !dbg !629, !tbaa !259
  %98 = fmul double %87, %97, !dbg !629
  %99 = fsub double %82, %98, !dbg !629
  %100 = add nuw nsw i64 %81, 1, !dbg !631
  call void @llvm.dbg.value(metadata double %99, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %93, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i32 undef, metadata !553, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !594
  %101 = icmp eq i64 %100, %79, !dbg !617
  br i1 %101, label %102, label %80, !dbg !620, !llvm.loop !632

; <label>:102:                                    ; preds = %80, %52
  %103 = phi double [ %60, %52 ], [ %93, %80 ]
  %104 = phi double [ %64, %52 ], [ %99, %80 ]
  call void @llvm.dbg.value(metadata double %103, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %104, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  store double %103, double* %59, align 8, !dbg !634, !tbaa !259
  store double %104, double* %63, align 8, !dbg !635, !tbaa !259
  call void @llvm.dbg.value(metadata i32 %56, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  call void @llvm.dbg.value(metadata i32 %56, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %105 = icmp sgt i64 %53, 1, !dbg !609
  br i1 %105, label %52, label %253, !dbg !610, !llvm.loop !636

; <label>:106:                                    ; preds = %6
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %107 = icmp sgt i32 %0, 0, !dbg !638
  br i1 %107, label %108, label %253, !dbg !639

; <label>:108:                                    ; preds = %106
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %109 = sext i32 %0 to i64, !dbg !639
  br label %110, !dbg !639

; <label>:110:                                    ; preds = %108, %166
  %111 = phi i64 [ %109, %108 ], [ %112, %166 ]
  %112 = add nsw i64 %111, -1, !dbg !640
  %113 = mul nsw i64 %112, 3, !dbg !641
  %114 = getelementptr inbounds double, double* %5, i64 %113, !dbg !642
  %115 = load double, double* %114, align 8, !dbg !642, !tbaa !259
  call void @llvm.dbg.value(metadata double %115, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %116 = add nsw i64 %113, 1, !dbg !643
  %117 = getelementptr inbounds double, double* %5, i64 %116, !dbg !644
  %118 = load double, double* %117, align 8, !dbg !644, !tbaa !259
  call void @llvm.dbg.value(metadata double %118, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  %119 = add nsw i64 %113, 2, !dbg !645
  %120 = getelementptr inbounds double, double* %5, i64 %119, !dbg !646
  %121 = load double, double* %120, align 8, !dbg !646, !tbaa !259
  call void @llvm.dbg.value(metadata double %121, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  %122 = getelementptr inbounds i32, i32* %1, i64 %112, !dbg !647
  %123 = load i32, i32* %122, align 4, !dbg !647, !tbaa !127
  %124 = sext i32 %123 to i64, !dbg !647
  %125 = getelementptr inbounds double, double* %3, i64 %124, !dbg !647
  call void @llvm.dbg.value(metadata double* %125, metadata !567, metadata !DIExpression()), !dbg !647
  %126 = getelementptr inbounds i32, i32* %2, i64 %112, !dbg !647
  %127 = load i32, i32* %126, align 4, !dbg !647, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %127, metadata !554, metadata !DIExpression()), !dbg !589
  %128 = bitcast double* %125 to i32*, !dbg !647
  call void @llvm.dbg.value(metadata i32* %128, metadata !550, metadata !DIExpression()), !dbg !590
  %129 = sext i32 %127 to i64, !dbg !647
  %130 = shl nsw i64 %129, 2, !dbg !647
  %131 = add nsw i64 %130, 7, !dbg !647
  %132 = lshr i64 %131, 3, !dbg !647
  %133 = getelementptr inbounds double, double* %125, i64 %132, !dbg !647
  call void @llvm.dbg.value(metadata double* %133, metadata !551, metadata !DIExpression()), !dbg !591
  call void @llvm.dbg.value(metadata i32 0, metadata !553, metadata !DIExpression()), !dbg !594
  call void @llvm.dbg.value(metadata double %121, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %118, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %115, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %134 = icmp sgt i32 %127, 0, !dbg !648
  br i1 %134, label %135, label %166, !dbg !651

; <label>:135:                                    ; preds = %110
  %136 = zext i32 %127 to i64
  br label %137, !dbg !651

; <label>:137:                                    ; preds = %137, %135
  %138 = phi i64 [ 0, %135 ], [ %164, %137 ]
  %139 = phi double [ %121, %135 ], [ %163, %137 ]
  %140 = phi double [ %118, %135 ], [ %157, %137 ]
  %141 = phi double [ %115, %135 ], [ %151, %137 ]
  call void @llvm.dbg.value(metadata double %139, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %140, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %141, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i64 %138, metadata !553, metadata !DIExpression()), !dbg !594
  %142 = getelementptr inbounds i32, i32* %128, i64 %138, !dbg !652
  %143 = load i32, i32* %142, align 4, !dbg !652, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %143, metadata !555, metadata !DIExpression()), !dbg !623
  %144 = getelementptr inbounds double, double* %133, i64 %138, !dbg !654
  %145 = load double, double* %144, align 8, !dbg !654, !tbaa !259
  call void @llvm.dbg.value(metadata double %145, metadata !549, metadata !DIExpression()), !dbg !626
  %146 = mul nsw i32 %143, 3, !dbg !656
  %147 = sext i32 %146 to i64, !dbg !656
  %148 = getelementptr inbounds double, double* %5, i64 %147, !dbg !656
  %149 = load double, double* %148, align 8, !dbg !656, !tbaa !259
  %150 = fmul double %145, %149, !dbg !656
  %151 = fsub double %141, %150, !dbg !656
  %152 = add nsw i32 %146, 1, !dbg !658
  %153 = sext i32 %152 to i64, !dbg !658
  %154 = getelementptr inbounds double, double* %5, i64 %153, !dbg !658
  %155 = load double, double* %154, align 8, !dbg !658, !tbaa !259
  %156 = fmul double %145, %155, !dbg !658
  %157 = fsub double %140, %156, !dbg !658
  %158 = add nsw i32 %146, 2, !dbg !660
  %159 = sext i32 %158 to i64, !dbg !660
  %160 = getelementptr inbounds double, double* %5, i64 %159, !dbg !660
  %161 = load double, double* %160, align 8, !dbg !660, !tbaa !259
  %162 = fmul double %145, %161, !dbg !660
  %163 = fsub double %139, %162, !dbg !660
  %164 = add nuw nsw i64 %138, 1, !dbg !662
  call void @llvm.dbg.value(metadata double %163, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %157, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %151, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i32 undef, metadata !553, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !594
  %165 = icmp eq i64 %164, %136, !dbg !648
  br i1 %165, label %166, label %137, !dbg !651, !llvm.loop !663

; <label>:166:                                    ; preds = %137, %110
  %167 = phi double [ %115, %110 ], [ %151, %137 ]
  %168 = phi double [ %118, %110 ], [ %157, %137 ]
  %169 = phi double [ %121, %110 ], [ %163, %137 ]
  call void @llvm.dbg.value(metadata double %167, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %168, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %169, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  store double %167, double* %114, align 8, !dbg !665, !tbaa !259
  store double %168, double* %117, align 8, !dbg !666, !tbaa !259
  store double %169, double* %120, align 8, !dbg !667, !tbaa !259
  call void @llvm.dbg.value(metadata i32 undef, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  call void @llvm.dbg.value(metadata i32 undef, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %170 = icmp sgt i64 %111, 1, !dbg !638
  br i1 %170, label %110, label %253, !dbg !639, !llvm.loop !668

; <label>:171:                                    ; preds = %6
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %172 = icmp sgt i32 %0, 0, !dbg !670
  br i1 %172, label %173, label %253, !dbg !671

; <label>:173:                                    ; preds = %171
  call void @llvm.dbg.value(metadata i32 %0, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %174 = sext i32 %0 to i64, !dbg !671
  br label %175, !dbg !671

; <label>:175:                                    ; preds = %173, %247
  %176 = phi i64 [ %174, %173 ], [ %178, %247 ]
  %177 = phi i32 [ %0, %173 ], [ %179, %247 ]
  %178 = add nsw i64 %176, -1, !dbg !672
  %179 = add nsw i32 %177, -1, !dbg !672
  %180 = shl nsw i32 %179, 2, !dbg !673
  %181 = sext i32 %180 to i64, !dbg !674
  %182 = getelementptr inbounds double, double* %5, i64 %181, !dbg !674
  %183 = load double, double* %182, align 8, !dbg !674, !tbaa !259
  call void @llvm.dbg.value(metadata double %183, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %184 = or i32 %180, 1, !dbg !675
  %185 = sext i32 %184 to i64, !dbg !676
  %186 = getelementptr inbounds double, double* %5, i64 %185, !dbg !676
  %187 = load double, double* %186, align 8, !dbg !676, !tbaa !259
  call void @llvm.dbg.value(metadata double %187, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  %188 = or i32 %180, 2, !dbg !677
  %189 = sext i32 %188 to i64, !dbg !678
  %190 = getelementptr inbounds double, double* %5, i64 %189, !dbg !678
  %191 = load double, double* %190, align 8, !dbg !678, !tbaa !259
  call void @llvm.dbg.value(metadata double %191, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  %192 = or i32 %180, 3, !dbg !679
  %193 = sext i32 %192 to i64, !dbg !680
  %194 = getelementptr inbounds double, double* %5, i64 %193, !dbg !680
  %195 = load double, double* %194, align 8, !dbg !680, !tbaa !259
  call void @llvm.dbg.value(metadata double %195, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !593
  %196 = getelementptr inbounds i32, i32* %1, i64 %178, !dbg !681
  %197 = load i32, i32* %196, align 4, !dbg !681, !tbaa !127
  %198 = sext i32 %197 to i64, !dbg !681
  %199 = getelementptr inbounds double, double* %3, i64 %198, !dbg !681
  call void @llvm.dbg.value(metadata double* %199, metadata !572, metadata !DIExpression()), !dbg !681
  %200 = getelementptr inbounds i32, i32* %2, i64 %178, !dbg !681
  %201 = load i32, i32* %200, align 4, !dbg !681, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %201, metadata !554, metadata !DIExpression()), !dbg !589
  %202 = bitcast double* %199 to i32*, !dbg !681
  call void @llvm.dbg.value(metadata i32* %202, metadata !550, metadata !DIExpression()), !dbg !590
  %203 = sext i32 %201 to i64, !dbg !681
  %204 = shl nsw i64 %203, 2, !dbg !681
  %205 = add nsw i64 %204, 7, !dbg !681
  %206 = lshr i64 %205, 3, !dbg !681
  %207 = getelementptr inbounds double, double* %199, i64 %206, !dbg !681
  call void @llvm.dbg.value(metadata double* %207, metadata !551, metadata !DIExpression()), !dbg !591
  call void @llvm.dbg.value(metadata i32 0, metadata !553, metadata !DIExpression()), !dbg !594
  call void @llvm.dbg.value(metadata double %195, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %191, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %187, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %183, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  %208 = icmp sgt i32 %201, 0, !dbg !682
  br i1 %208, label %209, label %247, !dbg !685

; <label>:209:                                    ; preds = %175
  %210 = zext i32 %201 to i64
  br label %211, !dbg !685

; <label>:211:                                    ; preds = %211, %209
  %212 = phi i64 [ 0, %209 ], [ %245, %211 ]
  %213 = phi double [ %195, %209 ], [ %244, %211 ]
  %214 = phi double [ %191, %209 ], [ %238, %211 ]
  %215 = phi double [ %187, %209 ], [ %232, %211 ]
  %216 = phi double [ %183, %209 ], [ %226, %211 ]
  call void @llvm.dbg.value(metadata double %213, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %214, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %215, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %216, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i64 %212, metadata !553, metadata !DIExpression()), !dbg !594
  %217 = getelementptr inbounds i32, i32* %202, i64 %212, !dbg !686
  %218 = load i32, i32* %217, align 4, !dbg !686, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %218, metadata !555, metadata !DIExpression()), !dbg !623
  %219 = getelementptr inbounds double, double* %207, i64 %212, !dbg !688
  %220 = load double, double* %219, align 8, !dbg !688, !tbaa !259
  call void @llvm.dbg.value(metadata double %220, metadata !549, metadata !DIExpression()), !dbg !626
  %221 = shl nsw i32 %218, 2, !dbg !690
  %222 = sext i32 %221 to i64, !dbg !690
  %223 = getelementptr inbounds double, double* %5, i64 %222, !dbg !690
  %224 = load double, double* %223, align 8, !dbg !690, !tbaa !259
  %225 = fmul double %220, %224, !dbg !690
  %226 = fsub double %216, %225, !dbg !690
  %227 = or i32 %221, 1, !dbg !692
  %228 = sext i32 %227 to i64, !dbg !692
  %229 = getelementptr inbounds double, double* %5, i64 %228, !dbg !692
  %230 = load double, double* %229, align 8, !dbg !692, !tbaa !259
  %231 = fmul double %220, %230, !dbg !692
  %232 = fsub double %215, %231, !dbg !692
  %233 = or i32 %221, 2, !dbg !694
  %234 = sext i32 %233 to i64, !dbg !694
  %235 = getelementptr inbounds double, double* %5, i64 %234, !dbg !694
  %236 = load double, double* %235, align 8, !dbg !694, !tbaa !259
  %237 = fmul double %220, %236, !dbg !694
  %238 = fsub double %214, %237, !dbg !694
  %239 = or i32 %221, 3, !dbg !696
  %240 = sext i32 %239 to i64, !dbg !696
  %241 = getelementptr inbounds double, double* %5, i64 %240, !dbg !696
  %242 = load double, double* %241, align 8, !dbg !696, !tbaa !259
  %243 = fmul double %220, %242, !dbg !696
  %244 = fsub double %213, %243, !dbg !696
  %245 = add nuw nsw i64 %212, 1, !dbg !698
  call void @llvm.dbg.value(metadata double %244, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %238, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %232, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %226, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata i32 undef, metadata !553, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !594
  %246 = icmp eq i64 %245, %210, !dbg !682
  br i1 %246, label %247, label %211, !dbg !685, !llvm.loop !699

; <label>:247:                                    ; preds = %211, %175
  %248 = phi double [ %183, %175 ], [ %226, %211 ]
  %249 = phi double [ %187, %175 ], [ %232, %211 ]
  %250 = phi double [ %191, %175 ], [ %238, %211 ]
  %251 = phi double [ %195, %175 ], [ %244, %211 ]
  call void @llvm.dbg.value(metadata double %248, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %249, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %250, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !593
  call void @llvm.dbg.value(metadata double %251, metadata !548, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !593
  store double %248, double* %182, align 8, !dbg !701, !tbaa !259
  store double %249, double* %186, align 8, !dbg !702, !tbaa !259
  store double %250, double* %190, align 8, !dbg !703, !tbaa !259
  store double %251, double* %194, align 8, !dbg !704, !tbaa !259
  call void @llvm.dbg.value(metadata i32 %179, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  call void @llvm.dbg.value(metadata i32 %179, metadata !552, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !584
  %252 = icmp sgt i64 %176, 1, !dbg !670
  br i1 %252, label %175, label %253, !dbg !671, !llvm.loop !705

; <label>:253:                                    ; preds = %247, %166, %102, %45, %171, %106, %48, %7, %6
  ret void, !dbg !707
}

; Function Attrs: nounwind ssp uwtable
define void @klu_utsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #0 !dbg !708 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !710, metadata !DIExpression()), !dbg !747
  call void @llvm.dbg.value(metadata i32* %1, metadata !711, metadata !DIExpression()), !dbg !748
  call void @llvm.dbg.value(metadata i32* %2, metadata !712, metadata !DIExpression()), !dbg !749
  call void @llvm.dbg.value(metadata double* %3, metadata !713, metadata !DIExpression()), !dbg !750
  call void @llvm.dbg.value(metadata double* %4, metadata !714, metadata !DIExpression()), !dbg !751
  call void @llvm.dbg.value(metadata i32 %5, metadata !715, metadata !DIExpression()), !dbg !752
  call void @llvm.dbg.value(metadata double* %6, metadata !716, metadata !DIExpression()), !dbg !753
  switch i32 %5, label %271 [
    i32 1, label %8
    i32 2, label %52
    i32 3, label %110
    i32 4, label %184
  ], !dbg !754

; <label>:8:                                      ; preds = %7
  call void @llvm.dbg.value(metadata i32 0, metadata !720, metadata !DIExpression()), !dbg !755
  %9 = icmp sgt i32 %0, 0, !dbg !756
  br i1 %9, label %10, label %271, !dbg !757

; <label>:10:                                     ; preds = %8
  %11 = zext i32 %0 to i64
  br label %12, !dbg !757

; <label>:12:                                     ; preds = %45, %10
  %13 = phi i64 [ 0, %10 ], [ %50, %45 ]
  call void @llvm.dbg.value(metadata i64 %13, metadata !720, metadata !DIExpression()), !dbg !755
  %14 = getelementptr inbounds i32, i32* %1, i64 %13, !dbg !758
  %15 = load i32, i32* %14, align 4, !dbg !758, !tbaa !127
  %16 = sext i32 %15 to i64, !dbg !758
  %17 = getelementptr inbounds double, double* %3, i64 %16, !dbg !758
  call void @llvm.dbg.value(metadata double* %17, metadata !726, metadata !DIExpression()), !dbg !758
  %18 = getelementptr inbounds i32, i32* %2, i64 %13, !dbg !758
  %19 = load i32, i32* %18, align 4, !dbg !758, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %19, metadata !722, metadata !DIExpression()), !dbg !759
  %20 = bitcast double* %17 to i32*, !dbg !758
  call void @llvm.dbg.value(metadata i32* %20, metadata !724, metadata !DIExpression()), !dbg !760
  %21 = sext i32 %19 to i64, !dbg !758
  %22 = shl nsw i64 %21, 2, !dbg !758
  %23 = add nsw i64 %22, 7, !dbg !758
  %24 = lshr i64 %23, 3, !dbg !758
  %25 = getelementptr inbounds double, double* %17, i64 %24, !dbg !758
  call void @llvm.dbg.value(metadata double* %25, metadata !725, metadata !DIExpression()), !dbg !761
  %26 = getelementptr inbounds double, double* %6, i64 %13, !dbg !762
  %27 = load double, double* %26, align 8, !dbg !762, !tbaa !259
  call void @llvm.dbg.value(metadata double %27, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 0, metadata !721, metadata !DIExpression()), !dbg !764
  %28 = icmp sgt i32 %19, 0, !dbg !765
  br i1 %28, label %29, label %45, !dbg !768

; <label>:29:                                     ; preds = %12
  %30 = zext i32 %19 to i64
  br label %31, !dbg !768

; <label>:31:                                     ; preds = %31, %29
  %32 = phi i64 [ 0, %29 ], [ %43, %31 ]
  %33 = phi double [ %27, %29 ], [ %42, %31 ]
  call void @llvm.dbg.value(metadata double %33, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i64 %32, metadata !721, metadata !DIExpression()), !dbg !764
  %34 = getelementptr inbounds double, double* %25, i64 %32, !dbg !769
  %35 = load double, double* %34, align 8, !dbg !769, !tbaa !259
  %36 = getelementptr inbounds i32, i32* %20, i64 %32, !dbg !769
  %37 = load i32, i32* %36, align 4, !dbg !769, !tbaa !127
  %38 = sext i32 %37 to i64, !dbg !769
  %39 = getelementptr inbounds double, double* %6, i64 %38, !dbg !769
  %40 = load double, double* %39, align 8, !dbg !769, !tbaa !259
  %41 = fmul double %35, %40, !dbg !769
  %42 = fsub double %33, %41, !dbg !769
  %43 = add nuw nsw i64 %32, 1, !dbg !773
  call void @llvm.dbg.value(metadata double %42, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 undef, metadata !721, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !764
  %44 = icmp eq i64 %43, %30, !dbg !765
  br i1 %44, label %45, label %31, !dbg !768, !llvm.loop !774

; <label>:45:                                     ; preds = %31, %12
  %46 = phi double [ %27, %12 ], [ %42, %31 ]
  call void @llvm.dbg.value(metadata double %46, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %47 = getelementptr inbounds double, double* %4, i64 %13, !dbg !776
  %48 = load double, double* %47, align 8, !dbg !776, !tbaa !259
  call void @llvm.dbg.value(metadata double %48, metadata !719, metadata !DIExpression()), !dbg !778
  %49 = fdiv double %46, %48, !dbg !779
  store double %49, double* %26, align 8, !dbg !779, !tbaa !259
  %50 = add nuw nsw i64 %13, 1, !dbg !781
  call void @llvm.dbg.value(metadata i32 undef, metadata !720, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !755
  %51 = icmp eq i64 %50, %11, !dbg !756
  br i1 %51, label %271, label %12, !dbg !757, !llvm.loop !782

; <label>:52:                                     ; preds = %7
  call void @llvm.dbg.value(metadata i32 0, metadata !720, metadata !DIExpression()), !dbg !755
  %53 = icmp sgt i32 %0, 0, !dbg !784
  br i1 %53, label %54, label %271, !dbg !785

; <label>:54:                                     ; preds = %52
  %55 = zext i32 %0 to i64
  br label %56, !dbg !785

; <label>:56:                                     ; preds = %101, %54
  %57 = phi i64 [ 0, %54 ], [ %108, %101 ]
  call void @llvm.dbg.value(metadata i64 %57, metadata !720, metadata !DIExpression()), !dbg !755
  %58 = getelementptr inbounds i32, i32* %1, i64 %57, !dbg !786
  %59 = load i32, i32* %58, align 4, !dbg !786, !tbaa !127
  %60 = sext i32 %59 to i64, !dbg !786
  %61 = getelementptr inbounds double, double* %3, i64 %60, !dbg !786
  call void @llvm.dbg.value(metadata double* %61, metadata !732, metadata !DIExpression()), !dbg !786
  %62 = getelementptr inbounds i32, i32* %2, i64 %57, !dbg !786
  %63 = load i32, i32* %62, align 4, !dbg !786, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %63, metadata !722, metadata !DIExpression()), !dbg !759
  %64 = bitcast double* %61 to i32*, !dbg !786
  call void @llvm.dbg.value(metadata i32* %64, metadata !724, metadata !DIExpression()), !dbg !760
  %65 = sext i32 %63 to i64, !dbg !786
  %66 = shl nsw i64 %65, 2, !dbg !786
  %67 = add nsw i64 %66, 7, !dbg !786
  %68 = lshr i64 %67, 3, !dbg !786
  %69 = getelementptr inbounds double, double* %61, i64 %68, !dbg !786
  call void @llvm.dbg.value(metadata double* %69, metadata !725, metadata !DIExpression()), !dbg !761
  %70 = shl nuw nsw i64 %57, 1, !dbg !787
  %71 = getelementptr inbounds double, double* %6, i64 %70, !dbg !788
  %72 = load double, double* %71, align 8, !dbg !788, !tbaa !259
  call void @llvm.dbg.value(metadata double %72, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %73 = or i64 %70, 1, !dbg !789
  %74 = getelementptr inbounds double, double* %6, i64 %73, !dbg !790
  %75 = load double, double* %74, align 8, !dbg !790, !tbaa !259
  call void @llvm.dbg.value(metadata double %75, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 0, metadata !721, metadata !DIExpression()), !dbg !764
  call void @llvm.dbg.value(metadata double %72, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %76 = icmp sgt i32 %63, 0, !dbg !791
  br i1 %76, label %77, label %101, !dbg !794

; <label>:77:                                     ; preds = %56
  %78 = zext i32 %63 to i64
  br label %79, !dbg !794

; <label>:79:                                     ; preds = %79, %77
  %80 = phi i64 [ 0, %77 ], [ %99, %79 ]
  %81 = phi double [ %75, %77 ], [ %98, %79 ]
  %82 = phi double [ %72, %77 ], [ %92, %79 ]
  call void @llvm.dbg.value(metadata double %81, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %82, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i64 %80, metadata !721, metadata !DIExpression()), !dbg !764
  %83 = getelementptr inbounds i32, i32* %64, i64 %80, !dbg !795
  %84 = load i32, i32* %83, align 4, !dbg !795, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %84, metadata !723, metadata !DIExpression()), !dbg !797
  %85 = getelementptr inbounds double, double* %69, i64 %80, !dbg !798
  %86 = load double, double* %85, align 8, !dbg !798, !tbaa !259
  call void @llvm.dbg.value(metadata double %86, metadata !718, metadata !DIExpression()), !dbg !800
  %87 = shl nsw i32 %84, 1, !dbg !801
  %88 = sext i32 %87 to i64, !dbg !801
  %89 = getelementptr inbounds double, double* %6, i64 %88, !dbg !801
  %90 = load double, double* %89, align 8, !dbg !801, !tbaa !259
  %91 = fmul double %86, %90, !dbg !801
  %92 = fsub double %82, %91, !dbg !801
  %93 = or i32 %87, 1, !dbg !803
  %94 = sext i32 %93 to i64, !dbg !803
  %95 = getelementptr inbounds double, double* %6, i64 %94, !dbg !803
  %96 = load double, double* %95, align 8, !dbg !803, !tbaa !259
  %97 = fmul double %86, %96, !dbg !803
  %98 = fsub double %81, %97, !dbg !803
  %99 = add nuw nsw i64 %80, 1, !dbg !805
  call void @llvm.dbg.value(metadata double %98, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %92, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 undef, metadata !721, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !764
  %100 = icmp eq i64 %99, %78, !dbg !791
  br i1 %100, label %101, label %79, !dbg !794, !llvm.loop !806

; <label>:101:                                    ; preds = %79, %56
  %102 = phi double [ %72, %56 ], [ %92, %79 ]
  %103 = phi double [ %75, %56 ], [ %98, %79 ]
  call void @llvm.dbg.value(metadata double %102, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %103, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  %104 = getelementptr inbounds double, double* %4, i64 %57, !dbg !808
  %105 = load double, double* %104, align 8, !dbg !808, !tbaa !259
  call void @llvm.dbg.value(metadata double %105, metadata !719, metadata !DIExpression()), !dbg !778
  %106 = fdiv double %102, %105, !dbg !810
  store double %106, double* %71, align 8, !dbg !810, !tbaa !259
  %107 = fdiv double %103, %105, !dbg !812
  store double %107, double* %74, align 8, !dbg !812, !tbaa !259
  %108 = add nuw nsw i64 %57, 1, !dbg !814
  call void @llvm.dbg.value(metadata i32 undef, metadata !720, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !755
  %109 = icmp eq i64 %108, %55, !dbg !784
  br i1 %109, label %271, label %56, !dbg !785, !llvm.loop !815

; <label>:110:                                    ; preds = %7
  call void @llvm.dbg.value(metadata i32 0, metadata !720, metadata !DIExpression()), !dbg !755
  %111 = icmp sgt i32 %0, 0, !dbg !817
  br i1 %111, label %112, label %271, !dbg !818

; <label>:112:                                    ; preds = %110
  %113 = zext i32 %0 to i64
  br label %114, !dbg !818

; <label>:114:                                    ; preds = %173, %112
  %115 = phi i64 [ 0, %112 ], [ %182, %173 ]
  call void @llvm.dbg.value(metadata i64 %115, metadata !720, metadata !DIExpression()), !dbg !755
  %116 = getelementptr inbounds i32, i32* %1, i64 %115, !dbg !819
  %117 = load i32, i32* %116, align 4, !dbg !819, !tbaa !127
  %118 = sext i32 %117 to i64, !dbg !819
  %119 = getelementptr inbounds double, double* %3, i64 %118, !dbg !819
  call void @llvm.dbg.value(metadata double* %119, metadata !737, metadata !DIExpression()), !dbg !819
  %120 = getelementptr inbounds i32, i32* %2, i64 %115, !dbg !819
  %121 = load i32, i32* %120, align 4, !dbg !819, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %121, metadata !722, metadata !DIExpression()), !dbg !759
  %122 = bitcast double* %119 to i32*, !dbg !819
  call void @llvm.dbg.value(metadata i32* %122, metadata !724, metadata !DIExpression()), !dbg !760
  %123 = sext i32 %121 to i64, !dbg !819
  %124 = shl nsw i64 %123, 2, !dbg !819
  %125 = add nsw i64 %124, 7, !dbg !819
  %126 = lshr i64 %125, 3, !dbg !819
  %127 = getelementptr inbounds double, double* %119, i64 %126, !dbg !819
  call void @llvm.dbg.value(metadata double* %127, metadata !725, metadata !DIExpression()), !dbg !761
  %128 = trunc i64 %115 to i32, !dbg !820
  %129 = mul nsw i32 %128, 3, !dbg !820
  %130 = zext i32 %129 to i64, !dbg !821
  %131 = getelementptr inbounds double, double* %6, i64 %130, !dbg !821
  %132 = load double, double* %131, align 8, !dbg !821, !tbaa !259
  call void @llvm.dbg.value(metadata double %132, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %133 = add nuw nsw i32 %129, 1, !dbg !822
  %134 = zext i32 %133 to i64, !dbg !823
  %135 = getelementptr inbounds double, double* %6, i64 %134, !dbg !823
  %136 = load double, double* %135, align 8, !dbg !823, !tbaa !259
  call void @llvm.dbg.value(metadata double %136, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  %137 = add nuw nsw i32 %129, 2, !dbg !824
  %138 = zext i32 %137 to i64, !dbg !825
  %139 = getelementptr inbounds double, double* %6, i64 %138, !dbg !825
  %140 = load double, double* %139, align 8, !dbg !825, !tbaa !259
  call void @llvm.dbg.value(metadata double %140, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 0, metadata !721, metadata !DIExpression()), !dbg !764
  call void @llvm.dbg.value(metadata double %136, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %132, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %141 = icmp sgt i32 %121, 0, !dbg !826
  br i1 %141, label %142, label %173, !dbg !829

; <label>:142:                                    ; preds = %114
  %143 = zext i32 %121 to i64
  br label %144, !dbg !829

; <label>:144:                                    ; preds = %144, %142
  %145 = phi i64 [ 0, %142 ], [ %171, %144 ]
  %146 = phi double [ %140, %142 ], [ %170, %144 ]
  %147 = phi double [ %136, %142 ], [ %164, %144 ]
  %148 = phi double [ %132, %142 ], [ %158, %144 ]
  call void @llvm.dbg.value(metadata double %146, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %147, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %148, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i64 %145, metadata !721, metadata !DIExpression()), !dbg !764
  %149 = getelementptr inbounds i32, i32* %122, i64 %145, !dbg !830
  %150 = load i32, i32* %149, align 4, !dbg !830, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %150, metadata !723, metadata !DIExpression()), !dbg !797
  %151 = getelementptr inbounds double, double* %127, i64 %145, !dbg !832
  %152 = load double, double* %151, align 8, !dbg !832, !tbaa !259
  call void @llvm.dbg.value(metadata double %152, metadata !718, metadata !DIExpression()), !dbg !800
  %153 = mul nsw i32 %150, 3, !dbg !834
  %154 = sext i32 %153 to i64, !dbg !834
  %155 = getelementptr inbounds double, double* %6, i64 %154, !dbg !834
  %156 = load double, double* %155, align 8, !dbg !834, !tbaa !259
  %157 = fmul double %152, %156, !dbg !834
  %158 = fsub double %148, %157, !dbg !834
  %159 = add nsw i32 %153, 1, !dbg !836
  %160 = sext i32 %159 to i64, !dbg !836
  %161 = getelementptr inbounds double, double* %6, i64 %160, !dbg !836
  %162 = load double, double* %161, align 8, !dbg !836, !tbaa !259
  %163 = fmul double %152, %162, !dbg !836
  %164 = fsub double %147, %163, !dbg !836
  %165 = add nsw i32 %153, 2, !dbg !838
  %166 = sext i32 %165 to i64, !dbg !838
  %167 = getelementptr inbounds double, double* %6, i64 %166, !dbg !838
  %168 = load double, double* %167, align 8, !dbg !838, !tbaa !259
  %169 = fmul double %152, %168, !dbg !838
  %170 = fsub double %146, %169, !dbg !838
  %171 = add nuw nsw i64 %145, 1, !dbg !840
  call void @llvm.dbg.value(metadata double %170, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %164, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %158, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 undef, metadata !721, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !764
  %172 = icmp eq i64 %171, %143, !dbg !826
  br i1 %172, label %173, label %144, !dbg !829, !llvm.loop !841

; <label>:173:                                    ; preds = %144, %114
  %174 = phi double [ %132, %114 ], [ %158, %144 ]
  %175 = phi double [ %136, %114 ], [ %164, %144 ]
  %176 = phi double [ %140, %114 ], [ %170, %144 ]
  call void @llvm.dbg.value(metadata double %174, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %175, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %176, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  %177 = getelementptr inbounds double, double* %4, i64 %115, !dbg !843
  %178 = load double, double* %177, align 8, !dbg !843, !tbaa !259
  call void @llvm.dbg.value(metadata double %178, metadata !719, metadata !DIExpression()), !dbg !778
  %179 = fdiv double %174, %178, !dbg !845
  store double %179, double* %131, align 8, !dbg !845, !tbaa !259
  %180 = fdiv double %175, %178, !dbg !847
  store double %180, double* %135, align 8, !dbg !847, !tbaa !259
  %181 = fdiv double %176, %178, !dbg !849
  store double %181, double* %139, align 8, !dbg !849, !tbaa !259
  %182 = add nuw nsw i64 %115, 1, !dbg !851
  call void @llvm.dbg.value(metadata i32 undef, metadata !720, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !755
  %183 = icmp eq i64 %182, %113, !dbg !817
  br i1 %183, label %271, label %114, !dbg !818, !llvm.loop !852

; <label>:184:                                    ; preds = %7
  call void @llvm.dbg.value(metadata i32 0, metadata !720, metadata !DIExpression()), !dbg !755
  %185 = icmp sgt i32 %0, 0, !dbg !854
  br i1 %185, label %186, label %271, !dbg !855

; <label>:186:                                    ; preds = %184
  %187 = zext i32 %0 to i64
  br label %188, !dbg !855

; <label>:188:                                    ; preds = %258, %186
  %189 = phi i64 [ 0, %186 ], [ %269, %258 ]
  call void @llvm.dbg.value(metadata i64 %189, metadata !720, metadata !DIExpression()), !dbg !755
  %190 = getelementptr inbounds i32, i32* %1, i64 %189, !dbg !856
  %191 = load i32, i32* %190, align 4, !dbg !856, !tbaa !127
  %192 = sext i32 %191 to i64, !dbg !856
  %193 = getelementptr inbounds double, double* %3, i64 %192, !dbg !856
  call void @llvm.dbg.value(metadata double* %193, metadata !742, metadata !DIExpression()), !dbg !856
  %194 = getelementptr inbounds i32, i32* %2, i64 %189, !dbg !856
  %195 = load i32, i32* %194, align 4, !dbg !856, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %195, metadata !722, metadata !DIExpression()), !dbg !759
  %196 = bitcast double* %193 to i32*, !dbg !856
  call void @llvm.dbg.value(metadata i32* %196, metadata !724, metadata !DIExpression()), !dbg !760
  %197 = sext i32 %195 to i64, !dbg !856
  %198 = shl nsw i64 %197, 2, !dbg !856
  %199 = add nsw i64 %198, 7, !dbg !856
  %200 = lshr i64 %199, 3, !dbg !856
  %201 = getelementptr inbounds double, double* %193, i64 %200, !dbg !856
  call void @llvm.dbg.value(metadata double* %201, metadata !725, metadata !DIExpression()), !dbg !761
  %202 = trunc i64 %189 to i32, !dbg !857
  %203 = shl nsw i32 %202, 2, !dbg !857
  %204 = zext i32 %203 to i64, !dbg !858
  %205 = getelementptr inbounds double, double* %6, i64 %204, !dbg !858
  %206 = load double, double* %205, align 8, !dbg !858, !tbaa !259
  call void @llvm.dbg.value(metadata double %206, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %207 = or i32 %203, 1, !dbg !859
  %208 = zext i32 %207 to i64, !dbg !860
  %209 = getelementptr inbounds double, double* %6, i64 %208, !dbg !860
  %210 = load double, double* %209, align 8, !dbg !860, !tbaa !259
  call void @llvm.dbg.value(metadata double %210, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  %211 = or i32 %203, 2, !dbg !861
  %212 = zext i32 %211 to i64, !dbg !862
  %213 = getelementptr inbounds double, double* %6, i64 %212, !dbg !862
  %214 = load double, double* %213, align 8, !dbg !862, !tbaa !259
  call void @llvm.dbg.value(metadata double %214, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  %215 = or i32 %203, 3, !dbg !863
  %216 = zext i32 %215 to i64, !dbg !864
  %217 = getelementptr inbounds double, double* %6, i64 %216, !dbg !864
  %218 = load double, double* %217, align 8, !dbg !864, !tbaa !259
  call void @llvm.dbg.value(metadata double %218, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 0, metadata !721, metadata !DIExpression()), !dbg !764
  call void @llvm.dbg.value(metadata double %214, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %210, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %206, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  %219 = icmp sgt i32 %195, 0, !dbg !865
  br i1 %219, label %220, label %258, !dbg !868

; <label>:220:                                    ; preds = %188
  %221 = zext i32 %195 to i64
  br label %222, !dbg !868

; <label>:222:                                    ; preds = %222, %220
  %223 = phi i64 [ 0, %220 ], [ %256, %222 ]
  %224 = phi double [ %218, %220 ], [ %255, %222 ]
  %225 = phi double [ %214, %220 ], [ %249, %222 ]
  %226 = phi double [ %210, %220 ], [ %243, %222 ]
  %227 = phi double [ %206, %220 ], [ %237, %222 ]
  call void @llvm.dbg.value(metadata double %224, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %225, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %226, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %227, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i64 %223, metadata !721, metadata !DIExpression()), !dbg !764
  %228 = getelementptr inbounds i32, i32* %196, i64 %223, !dbg !869
  %229 = load i32, i32* %228, align 4, !dbg !869, !tbaa !127
  call void @llvm.dbg.value(metadata i32 %229, metadata !723, metadata !DIExpression()), !dbg !797
  %230 = getelementptr inbounds double, double* %201, i64 %223, !dbg !871
  %231 = load double, double* %230, align 8, !dbg !871, !tbaa !259
  call void @llvm.dbg.value(metadata double %231, metadata !718, metadata !DIExpression()), !dbg !800
  %232 = shl nsw i32 %229, 2, !dbg !873
  %233 = sext i32 %232 to i64, !dbg !873
  %234 = getelementptr inbounds double, double* %6, i64 %233, !dbg !873
  %235 = load double, double* %234, align 8, !dbg !873, !tbaa !259
  %236 = fmul double %231, %235, !dbg !873
  %237 = fsub double %227, %236, !dbg !873
  %238 = or i32 %232, 1, !dbg !875
  %239 = sext i32 %238 to i64, !dbg !875
  %240 = getelementptr inbounds double, double* %6, i64 %239, !dbg !875
  %241 = load double, double* %240, align 8, !dbg !875, !tbaa !259
  %242 = fmul double %231, %241, !dbg !875
  %243 = fsub double %226, %242, !dbg !875
  %244 = or i32 %232, 2, !dbg !877
  %245 = sext i32 %244 to i64, !dbg !877
  %246 = getelementptr inbounds double, double* %6, i64 %245, !dbg !877
  %247 = load double, double* %246, align 8, !dbg !877, !tbaa !259
  %248 = fmul double %231, %247, !dbg !877
  %249 = fsub double %225, %248, !dbg !877
  %250 = or i32 %232, 3, !dbg !879
  %251 = sext i32 %250 to i64, !dbg !879
  %252 = getelementptr inbounds double, double* %6, i64 %251, !dbg !879
  %253 = load double, double* %252, align 8, !dbg !879, !tbaa !259
  %254 = fmul double %231, %253, !dbg !879
  %255 = fsub double %224, %254, !dbg !879
  %256 = add nuw nsw i64 %223, 1, !dbg !881
  call void @llvm.dbg.value(metadata double %255, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %249, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %243, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %237, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata i32 undef, metadata !721, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !764
  %257 = icmp eq i64 %256, %221, !dbg !865
  br i1 %257, label %258, label %222, !dbg !868, !llvm.loop !882

; <label>:258:                                    ; preds = %222, %188
  %259 = phi double [ %206, %188 ], [ %237, %222 ]
  %260 = phi double [ %210, %188 ], [ %243, %222 ]
  %261 = phi double [ %214, %188 ], [ %249, %222 ]
  %262 = phi double [ %218, %188 ], [ %255, %222 ]
  call void @llvm.dbg.value(metadata double %259, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 0, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %260, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 64, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %261, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 128, 64)), !dbg !763
  call void @llvm.dbg.value(metadata double %262, metadata !717, metadata !DIExpression(DW_OP_LLVM_fragment, 192, 64)), !dbg !763
  %263 = getelementptr inbounds double, double* %4, i64 %189, !dbg !884
  %264 = load double, double* %263, align 8, !dbg !884, !tbaa !259
  call void @llvm.dbg.value(metadata double %264, metadata !719, metadata !DIExpression()), !dbg !778
  %265 = fdiv double %259, %264, !dbg !886
  store double %265, double* %205, align 8, !dbg !886, !tbaa !259
  %266 = fdiv double %260, %264, !dbg !888
  store double %266, double* %209, align 8, !dbg !888, !tbaa !259
  %267 = fdiv double %261, %264, !dbg !890
  store double %267, double* %213, align 8, !dbg !890, !tbaa !259
  %268 = fdiv double %262, %264, !dbg !892
  store double %268, double* %217, align 8, !dbg !892, !tbaa !259
  %269 = add nuw nsw i64 %189, 1, !dbg !894
  call void @llvm.dbg.value(metadata i32 undef, metadata !720, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !755
  %270 = icmp eq i64 %269, %187, !dbg !854
  br i1 %270, label %271, label %188, !dbg !855, !llvm.loop !895

; <label>:271:                                    ; preds = %258, %173, %101, %45, %184, %110, %52, %8, %7
  ret void, !dbg !897
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { nounwind readnone speculatable }
attributes #3 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!15, !16, !17, !18}
!llvm.ident = !{!19}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5, !8, !9, !11, !14}
!4 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIDerivedType(tag: DW_TAG_typedef, name: "Unit", file: !7, line: 253, baseType: !4)
!7 = !DIFile(filename: "./klu_version.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!8 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!9 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !10, size: 64)
!10 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!11 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !12, line: 62, baseType: !13)
!12 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!13 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!14 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !4, size: 64)
!15 = !{i32 2, !"Dwarf Version", i32 4}
!16 = !{i32 2, !"Debug Info Version", i32 3}
!17 = !{i32 1, !"wchar_size", i32 4}
!18 = !{i32 7, !"PIC Level", i32 2}
!19 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!20 = distinct !DISubprogram(name: "klu_kernel_factor", scope: !1, file: !1, line: 63, type: !21, isLocal: false, isDefinition: true, scopeLine: 100, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !57)
!21 = !DISubroutineType(types: !22)
!22 = !{!11, !10, !9, !9, !14, !9, !4, !23, !14, !9, !9, !9, !9, !9, !9, !9, !14, !9, !10, !9, !14, !9, !9, !14, !24}
!23 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !5, size: 64)
!24 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !25, size: 64)
!25 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !26, line: 207, baseType: !27)
!26 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!27 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !26, line: 137, size: 1280, elements: !28)
!28 = !{!29, !30, !31, !32, !33, !34, !35, !36, !37, !42, !43, !44, !45, !46, !47, !48, !49, !50, !51, !52, !53, !54, !55, !56}
!29 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !27, file: !26, line: 144, baseType: !4, size: 64)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !27, file: !26, line: 145, baseType: !4, size: 64, offset: 64)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !27, file: !26, line: 146, baseType: !4, size: 64, offset: 128)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !27, file: !26, line: 147, baseType: !4, size: 64, offset: 192)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !27, file: !26, line: 148, baseType: !4, size: 64, offset: 256)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !27, file: !26, line: 150, baseType: !10, size: 32, offset: 320)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !27, file: !26, line: 151, baseType: !10, size: 32, offset: 352)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !27, file: !26, line: 153, baseType: !10, size: 32, offset: 384)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !27, file: !26, line: 157, baseType: !38, size: 64, offset: 448)
!38 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !39, size: 64)
!39 = !DISubroutineType(types: !40)
!40 = !{!10, !10, !9, !9, !9, !41}
!41 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !27, size: 64)
!42 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !27, file: !26, line: 162, baseType: !8, size: 64, offset: 512)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !27, file: !26, line: 164, baseType: !10, size: 32, offset: 576)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !27, file: !26, line: 177, baseType: !10, size: 32, offset: 608)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !27, file: !26, line: 178, baseType: !10, size: 32, offset: 640)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !27, file: !26, line: 180, baseType: !10, size: 32, offset: 672)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !27, file: !26, line: 185, baseType: !10, size: 32, offset: 704)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !27, file: !26, line: 191, baseType: !10, size: 32, offset: 736)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !27, file: !26, line: 196, baseType: !10, size: 32, offset: 768)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !27, file: !26, line: 198, baseType: !4, size: 64, offset: 832)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !27, file: !26, line: 199, baseType: !4, size: 64, offset: 896)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !27, file: !26, line: 200, baseType: !4, size: 64, offset: 960)
!53 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !27, file: !26, line: 201, baseType: !4, size: 64, offset: 1024)
!54 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !27, file: !26, line: 202, baseType: !4, size: 64, offset: 1088)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !27, file: !26, line: 204, baseType: !11, size: 64, offset: 1152)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !27, file: !26, line: 205, baseType: !11, size: 64, offset: 1216)
!57 = !{!58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68, !69, !70, !71, !72, !73, !74, !75, !76, !77, !78, !79, !80, !81, !82, !83, !84, !85, !86, !87, !88, !89, !90, !91, !92, !93, !94, !95}
!58 = !DILocalVariable(name: "n", arg: 1, scope: !20, file: !1, line: 66, type: !10)
!59 = !DILocalVariable(name: "Ap", arg: 2, scope: !20, file: !1, line: 67, type: !9)
!60 = !DILocalVariable(name: "Ai", arg: 3, scope: !20, file: !1, line: 68, type: !9)
!61 = !DILocalVariable(name: "Ax", arg: 4, scope: !20, file: !1, line: 69, type: !14)
!62 = !DILocalVariable(name: "Q", arg: 5, scope: !20, file: !1, line: 70, type: !9)
!63 = !DILocalVariable(name: "Lsize", arg: 6, scope: !20, file: !1, line: 71, type: !4)
!64 = !DILocalVariable(name: "p_LU", arg: 7, scope: !20, file: !1, line: 74, type: !23)
!65 = !DILocalVariable(name: "Udiag", arg: 8, scope: !20, file: !1, line: 75, type: !14)
!66 = !DILocalVariable(name: "Llen", arg: 9, scope: !20, file: !1, line: 76, type: !9)
!67 = !DILocalVariable(name: "Ulen", arg: 10, scope: !20, file: !1, line: 77, type: !9)
!68 = !DILocalVariable(name: "Lip", arg: 11, scope: !20, file: !1, line: 78, type: !9)
!69 = !DILocalVariable(name: "Uip", arg: 12, scope: !20, file: !1, line: 79, type: !9)
!70 = !DILocalVariable(name: "P", arg: 13, scope: !20, file: !1, line: 80, type: !9)
!71 = !DILocalVariable(name: "lnz", arg: 14, scope: !20, file: !1, line: 81, type: !9)
!72 = !DILocalVariable(name: "unz", arg: 15, scope: !20, file: !1, line: 82, type: !9)
!73 = !DILocalVariable(name: "X", arg: 16, scope: !20, file: !1, line: 85, type: !14)
!74 = !DILocalVariable(name: "Work", arg: 17, scope: !20, file: !1, line: 86, type: !9)
!75 = !DILocalVariable(name: "k1", arg: 18, scope: !20, file: !1, line: 89, type: !10)
!76 = !DILocalVariable(name: "PSinv", arg: 19, scope: !20, file: !1, line: 90, type: !9)
!77 = !DILocalVariable(name: "Rs", arg: 20, scope: !20, file: !1, line: 91, type: !14)
!78 = !DILocalVariable(name: "Offp", arg: 21, scope: !20, file: !1, line: 94, type: !9)
!79 = !DILocalVariable(name: "Offi", arg: 22, scope: !20, file: !1, line: 95, type: !9)
!80 = !DILocalVariable(name: "Offx", arg: 23, scope: !20, file: !1, line: 96, type: !14)
!81 = !DILocalVariable(name: "Common", arg: 24, scope: !20, file: !1, line: 98, type: !24)
!82 = !DILocalVariable(name: "maxlnz", scope: !20, file: !1, line: 101, type: !4)
!83 = !DILocalVariable(name: "dunits", scope: !20, file: !1, line: 101, type: !4)
!84 = !DILocalVariable(name: "LU", scope: !20, file: !1, line: 102, type: !5)
!85 = !DILocalVariable(name: "Pinv", scope: !20, file: !1, line: 103, type: !9)
!86 = !DILocalVariable(name: "Lpend", scope: !20, file: !1, line: 103, type: !9)
!87 = !DILocalVariable(name: "Stack", scope: !20, file: !1, line: 103, type: !9)
!88 = !DILocalVariable(name: "Flag", scope: !20, file: !1, line: 103, type: !9)
!89 = !DILocalVariable(name: "Ap_pos", scope: !20, file: !1, line: 103, type: !9)
!90 = !DILocalVariable(name: "W", scope: !20, file: !1, line: 103, type: !9)
!91 = !DILocalVariable(name: "lsize", scope: !20, file: !1, line: 104, type: !10)
!92 = !DILocalVariable(name: "usize", scope: !20, file: !1, line: 104, type: !10)
!93 = !DILocalVariable(name: "anz", scope: !20, file: !1, line: 104, type: !10)
!94 = !DILocalVariable(name: "ok", scope: !20, file: !1, line: 104, type: !10)
!95 = !DILocalVariable(name: "lusize", scope: !20, file: !1, line: 105, type: !11)
!96 = !DILocation(line: 66, column: 9, scope: !20)
!97 = !DILocation(line: 67, column: 9, scope: !20)
!98 = !DILocation(line: 68, column: 9, scope: !20)
!99 = !DILocation(line: 69, column: 11, scope: !20)
!100 = !DILocation(line: 70, column: 9, scope: !20)
!101 = !DILocation(line: 71, column: 12, scope: !20)
!102 = !DILocation(line: 74, column: 12, scope: !20)
!103 = !DILocation(line: 75, column: 11, scope: !20)
!104 = !DILocation(line: 76, column: 9, scope: !20)
!105 = !DILocation(line: 77, column: 9, scope: !20)
!106 = !DILocation(line: 78, column: 9, scope: !20)
!107 = !DILocation(line: 79, column: 9, scope: !20)
!108 = !DILocation(line: 80, column: 9, scope: !20)
!109 = !DILocation(line: 81, column: 10, scope: !20)
!110 = !DILocation(line: 82, column: 10, scope: !20)
!111 = !DILocation(line: 85, column: 12, scope: !20)
!112 = !DILocation(line: 86, column: 10, scope: !20)
!113 = !DILocation(line: 89, column: 9, scope: !20)
!114 = !DILocation(line: 90, column: 9, scope: !20)
!115 = !DILocation(line: 91, column: 12, scope: !20)
!116 = !DILocation(line: 94, column: 9, scope: !20)
!117 = !DILocation(line: 95, column: 9, scope: !20)
!118 = !DILocation(line: 96, column: 11, scope: !20)
!119 = !DILocation(line: 98, column: 17, scope: !20)
!120 = !DILocation(line: 102, column: 5, scope: !20)
!121 = !DILocation(line: 112, column: 9, scope: !20)
!122 = !DILocation(line: 115, column: 15, scope: !123)
!123 = distinct !DILexicalBlock(scope: !20, file: !1, line: 115, column: 9)
!124 = !DILocation(line: 115, column: 9, scope: !20)
!125 = !DILocation(line: 113, column: 16, scope: !20)
!126 = !DILocation(line: 113, column: 11, scope: !20)
!127 = !{!128, !128, i64 0}
!128 = !{!"int", !129, i64 0}
!129 = !{!"omnipotent char", !130, i64 0}
!130 = !{!"Simple C/C++ TBAA"}
!131 = !DILocation(line: 113, column: 23, scope: !20)
!132 = !DILocation(line: 113, column: 21, scope: !20)
!133 = !DILocation(line: 104, column: 23, scope: !20)
!134 = !DILocation(line: 117, column: 17, scope: !135)
!135 = distinct !DILexicalBlock(scope: !123, file: !1, line: 116, column: 5)
!136 = !DILocation(line: 118, column: 17, scope: !135)
!137 = !DILocation(line: 119, column: 25, scope: !135)
!138 = !DILocation(line: 119, column: 23, scope: !135)
!139 = !DILocation(line: 119, column: 31, scope: !135)
!140 = !DILocation(line: 119, column: 29, scope: !135)
!141 = !DILocation(line: 120, column: 5, scope: !135)
!142 = !DILocation(line: 0, scope: !143)
!143 = distinct !DILexicalBlock(scope: !123, file: !1, line: 122, column: 5)
!144 = !DILocation(line: 104, column: 9, scope: !20)
!145 = !DILocation(line: 104, column: 16, scope: !20)
!146 = !DILocation(line: 128, column: 14, scope: !20)
!147 = !DILocation(line: 131, column: 16, scope: !20)
!148 = !DILocation(line: 131, column: 28, scope: !20)
!149 = !DILocation(line: 131, column: 43, scope: !20)
!150 = !DILocation(line: 131, column: 59, scope: !20)
!151 = !DILocation(line: 101, column: 12, scope: !20)
!152 = !DILocation(line: 132, column: 14, scope: !20)
!153 = !DILocation(line: 133, column: 14, scope: !20)
!154 = !DILocation(line: 144, column: 11, scope: !20)
!155 = !{!156, !156, i64 0}
!156 = !{!"any pointer", !129, i64 0}
!157 = !DILocation(line: 103, column: 49, scope: !20)
!158 = !DILocation(line: 103, column: 10, scope: !20)
!159 = !DILocation(line: 148, column: 31, scope: !20)
!160 = !DILocation(line: 103, column: 25, scope: !20)
!161 = !DILocation(line: 149, column: 31, scope: !20)
!162 = !DILocation(line: 103, column: 33, scope: !20)
!163 = !DILocation(line: 150, column: 31, scope: !20)
!164 = !DILocation(line: 103, column: 17, scope: !20)
!165 = !DILocation(line: 151, column: 31, scope: !20)
!166 = !DILocation(line: 103, column: 40, scope: !20)
!167 = !DILocation(line: 154, column: 14, scope: !20)
!168 = !DILocation(line: 154, column: 36, scope: !20)
!169 = !DILocation(line: 154, column: 34, scope: !20)
!170 = !DILocation(line: 154, column: 58, scope: !20)
!171 = !DILocation(line: 155, column: 34, scope: !20)
!172 = !DILocation(line: 101, column: 20, scope: !20)
!173 = !DILocation(line: 156, column: 14, scope: !20)
!174 = !DILocation(line: 105, column: 12, scope: !20)
!175 = !DILocation(line: 157, column: 11, scope: !20)
!176 = !DILocation(line: 158, column: 10, scope: !20)
!177 = !DILocation(line: 102, column: 11, scope: !20)
!178 = !DILocation(line: 158, column: 8, scope: !20)
!179 = !DILocation(line: 159, column: 9, scope: !20)
!180 = !DILocation(line: 158, column: 15, scope: !20)
!181 = !DILocation(line: 159, column: 12, scope: !182)
!182 = distinct !DILexicalBlock(scope: !20, file: !1, line: 159, column: 9)
!183 = !DILocation(line: 162, column: 17, scope: !184)
!184 = distinct !DILexicalBlock(scope: !182, file: !1, line: 160, column: 5)
!185 = !DILocation(line: 162, column: 24, scope: !184)
!186 = !{!187, !128, i64 76}
!187 = !{!"klu_common_struct", !188, i64 0, !188, i64 8, !188, i64 16, !188, i64 24, !188, i64 32, !128, i64 40, !128, i64 44, !128, i64 48, !156, i64 56, !156, i64 64, !128, i64 72, !128, i64 76, !128, i64 80, !128, i64 84, !128, i64 88, !128, i64 92, !128, i64 96, !188, i64 104, !188, i64 112, !188, i64 120, !188, i64 128, !188, i64 136, !189, i64 144, !189, i64 152}
!188 = !{!"double", !129, i64 0}
!189 = !{!"long", !129, i64 0}
!190 = !DILocation(line: 164, column: 9, scope: !184)
!191 = !DILocation(line: 172, column: 14, scope: !20)
!192 = !DILocation(line: 181, column: 17, scope: !193)
!193 = distinct !DILexicalBlock(scope: !20, file: !1, line: 181, column: 9)
!194 = !DILocation(line: 181, column: 24, scope: !193)
!195 = !DILocation(line: 181, column: 9, scope: !20)
!196 = !DILocation(line: 183, column: 24, scope: !197)
!197 = distinct !DILexicalBlock(scope: !193, file: !1, line: 182, column: 5)
!198 = !DILocation(line: 183, column: 14, scope: !197)
!199 = !DILocation(line: 183, column: 12, scope: !197)
!200 = !DILocation(line: 185, column: 5, scope: !197)
!201 = !DILocation(line: 0, scope: !20)
!202 = !DILocation(line: 186, column: 13, scope: !20)
!203 = !DILocation(line: 186, column: 11, scope: !20)
!204 = !DILocation(line: 188, column: 5, scope: !20)
!205 = !DILocation(line: 189, column: 1, scope: !20)
!206 = distinct !DISubprogram(name: "klu_lsolve", scope: !1, file: !1, line: 201, type: !207, isLocal: false, isDefinition: true, scopeLine: 212, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !209)
!207 = !DISubroutineType(types: !208)
!208 = !{null, !10, !9, !9, !5, !10, !14}
!209 = !{!210, !211, !212, !213, !214, !215, !216, !220, !221, !222, !223, !224, !225, !226, !227, !233, !238, !243}
!210 = !DILocalVariable(name: "n", arg: 1, scope: !206, file: !1, line: 204, type: !10)
!211 = !DILocalVariable(name: "Lip", arg: 2, scope: !206, file: !1, line: 205, type: !9)
!212 = !DILocalVariable(name: "Llen", arg: 3, scope: !206, file: !1, line: 206, type: !9)
!213 = !DILocalVariable(name: "LU", arg: 4, scope: !206, file: !1, line: 207, type: !5)
!214 = !DILocalVariable(name: "nrhs", arg: 5, scope: !206, file: !1, line: 208, type: !10)
!215 = !DILocalVariable(name: "X", arg: 6, scope: !206, file: !1, line: 210, type: !14)
!216 = !DILocalVariable(name: "x", scope: !206, file: !1, line: 213, type: !217)
!217 = !DICompositeType(tag: DW_TAG_array_type, baseType: !4, size: 256, elements: !218)
!218 = !{!219}
!219 = !DISubrange(count: 4)
!220 = !DILocalVariable(name: "lik", scope: !206, file: !1, line: 213, type: !4)
!221 = !DILocalVariable(name: "Li", scope: !206, file: !1, line: 214, type: !9)
!222 = !DILocalVariable(name: "Lx", scope: !206, file: !1, line: 215, type: !14)
!223 = !DILocalVariable(name: "k", scope: !206, file: !1, line: 216, type: !10)
!224 = !DILocalVariable(name: "p", scope: !206, file: !1, line: 216, type: !10)
!225 = !DILocalVariable(name: "len", scope: !206, file: !1, line: 216, type: !10)
!226 = !DILocalVariable(name: "i", scope: !206, file: !1, line: 216, type: !10)
!227 = !DILocalVariable(name: "xp", scope: !228, file: !1, line: 225, type: !5)
!228 = distinct !DILexicalBlock(scope: !229, file: !1, line: 225, column: 17)
!229 = distinct !DILexicalBlock(scope: !230, file: !1, line: 223, column: 13)
!230 = distinct !DILexicalBlock(scope: !231, file: !1, line: 222, column: 13)
!231 = distinct !DILexicalBlock(scope: !232, file: !1, line: 222, column: 13)
!232 = distinct !DILexicalBlock(scope: !206, file: !1, line: 219, column: 5)
!233 = !DILocalVariable(name: "xp", scope: !234, file: !1, line: 241, type: !5)
!234 = distinct !DILexicalBlock(scope: !235, file: !1, line: 241, column: 17)
!235 = distinct !DILexicalBlock(scope: !236, file: !1, line: 238, column: 13)
!236 = distinct !DILexicalBlock(scope: !237, file: !1, line: 237, column: 13)
!237 = distinct !DILexicalBlock(scope: !232, file: !1, line: 237, column: 13)
!238 = !DILocalVariable(name: "xp", scope: !239, file: !1, line: 259, type: !5)
!239 = distinct !DILexicalBlock(scope: !240, file: !1, line: 259, column: 17)
!240 = distinct !DILexicalBlock(scope: !241, file: !1, line: 255, column: 13)
!241 = distinct !DILexicalBlock(scope: !242, file: !1, line: 254, column: 13)
!242 = distinct !DILexicalBlock(scope: !232, file: !1, line: 254, column: 13)
!243 = !DILocalVariable(name: "xp", scope: !244, file: !1, line: 279, type: !5)
!244 = distinct !DILexicalBlock(scope: !245, file: !1, line: 279, column: 17)
!245 = distinct !DILexicalBlock(scope: !246, file: !1, line: 274, column: 13)
!246 = distinct !DILexicalBlock(scope: !247, file: !1, line: 273, column: 13)
!247 = distinct !DILexicalBlock(scope: !232, file: !1, line: 273, column: 13)
!248 = !DILocation(line: 204, column: 9, scope: !206)
!249 = !DILocation(line: 205, column: 9, scope: !206)
!250 = !DILocation(line: 206, column: 9, scope: !206)
!251 = !DILocation(line: 207, column: 10, scope: !206)
!252 = !DILocation(line: 208, column: 9, scope: !206)
!253 = !DILocation(line: 210, column: 11, scope: !206)
!254 = !DILocation(line: 218, column: 5, scope: !206)
!255 = !DILocation(line: 216, column: 9, scope: !206)
!256 = !DILocation(line: 222, column: 28, scope: !230)
!257 = !DILocation(line: 222, column: 13, scope: !231)
!258 = !DILocation(line: 224, column: 25, scope: !229)
!259 = !{!188, !188, i64 0}
!260 = !DILocation(line: 213, column: 11, scope: !206)
!261 = !DILocation(line: 225, column: 17, scope: !228)
!262 = !DILocation(line: 216, column: 15, scope: !206)
!263 = !DILocation(line: 214, column: 10, scope: !206)
!264 = !DILocation(line: 215, column: 12, scope: !206)
!265 = !DILocation(line: 216, column: 12, scope: !206)
!266 = !DILocation(line: 227, column: 32, scope: !267)
!267 = distinct !DILexicalBlock(scope: !268, file: !1, line: 227, column: 17)
!268 = distinct !DILexicalBlock(scope: !229, file: !1, line: 227, column: 17)
!269 = !DILocation(line: 227, column: 17, scope: !268)
!270 = !DILocation(line: 230, column: 21, scope: !271)
!271 = distinct !DILexicalBlock(scope: !272, file: !1, line: 230, column: 21)
!272 = distinct !DILexicalBlock(scope: !267, file: !1, line: 228, column: 17)
!273 = !DILocation(line: 227, column: 41, scope: !267)
!274 = distinct !{!274, !269, !275}
!275 = !DILocation(line: 231, column: 17, scope: !268)
!276 = !DILocation(line: 222, column: 35, scope: !230)
!277 = distinct !{!277, !257, !278}
!278 = !DILocation(line: 232, column: 13, scope: !231)
!279 = !DILocation(line: 237, column: 28, scope: !236)
!280 = !DILocation(line: 237, column: 13, scope: !237)
!281 = !DILocation(line: 239, column: 29, scope: !235)
!282 = !DILocation(line: 239, column: 25, scope: !235)
!283 = !DILocation(line: 240, column: 32, scope: !235)
!284 = !DILocation(line: 240, column: 25, scope: !235)
!285 = !DILocation(line: 241, column: 17, scope: !234)
!286 = !DILocation(line: 242, column: 32, scope: !287)
!287 = distinct !DILexicalBlock(scope: !288, file: !1, line: 242, column: 17)
!288 = distinct !DILexicalBlock(scope: !235, file: !1, line: 242, column: 17)
!289 = !DILocation(line: 242, column: 17, scope: !288)
!290 = !DILocation(line: 244, column: 25, scope: !291)
!291 = distinct !DILexicalBlock(scope: !287, file: !1, line: 243, column: 17)
!292 = !DILocation(line: 216, column: 20, scope: !206)
!293 = !DILocation(line: 245, column: 27, scope: !291)
!294 = !DILocation(line: 213, column: 18, scope: !206)
!295 = !DILocation(line: 246, column: 21, scope: !296)
!296 = distinct !DILexicalBlock(scope: !291, file: !1, line: 246, column: 21)
!297 = !DILocation(line: 247, column: 21, scope: !298)
!298 = distinct !DILexicalBlock(scope: !291, file: !1, line: 247, column: 21)
!299 = !DILocation(line: 242, column: 41, scope: !287)
!300 = distinct !{!300, !289, !301}
!301 = !DILocation(line: 248, column: 17, scope: !288)
!302 = !DILocation(line: 237, column: 35, scope: !236)
!303 = distinct !{!303, !280, !304}
!304 = !DILocation(line: 249, column: 13, scope: !237)
!305 = !DILocation(line: 254, column: 28, scope: !241)
!306 = !DILocation(line: 254, column: 13, scope: !242)
!307 = !DILocation(line: 256, column: 29, scope: !240)
!308 = !DILocation(line: 256, column: 25, scope: !240)
!309 = !DILocation(line: 257, column: 32, scope: !240)
!310 = !DILocation(line: 257, column: 25, scope: !240)
!311 = !DILocation(line: 258, column: 32, scope: !240)
!312 = !DILocation(line: 258, column: 25, scope: !240)
!313 = !DILocation(line: 259, column: 17, scope: !239)
!314 = !DILocation(line: 260, column: 32, scope: !315)
!315 = distinct !DILexicalBlock(scope: !316, file: !1, line: 260, column: 17)
!316 = distinct !DILexicalBlock(scope: !240, file: !1, line: 260, column: 17)
!317 = !DILocation(line: 260, column: 17, scope: !316)
!318 = !DILocation(line: 262, column: 25, scope: !319)
!319 = distinct !DILexicalBlock(scope: !315, file: !1, line: 261, column: 17)
!320 = !DILocation(line: 263, column: 27, scope: !319)
!321 = !DILocation(line: 264, column: 21, scope: !322)
!322 = distinct !DILexicalBlock(scope: !319, file: !1, line: 264, column: 21)
!323 = !DILocation(line: 265, column: 21, scope: !324)
!324 = distinct !DILexicalBlock(scope: !319, file: !1, line: 265, column: 21)
!325 = !DILocation(line: 266, column: 21, scope: !326)
!326 = distinct !DILexicalBlock(scope: !319, file: !1, line: 266, column: 21)
!327 = !DILocation(line: 260, column: 41, scope: !315)
!328 = distinct !{!328, !317, !329}
!329 = !DILocation(line: 267, column: 17, scope: !316)
!330 = !DILocation(line: 254, column: 35, scope: !241)
!331 = distinct !{!331, !306, !332}
!332 = !DILocation(line: 268, column: 13, scope: !242)
!333 = !DILocation(line: 273, column: 28, scope: !246)
!334 = !DILocation(line: 273, column: 13, scope: !247)
!335 = !DILocation(line: 275, column: 29, scope: !245)
!336 = !DILocation(line: 275, column: 25, scope: !245)
!337 = !DILocation(line: 276, column: 32, scope: !245)
!338 = !DILocation(line: 276, column: 25, scope: !245)
!339 = !DILocation(line: 277, column: 32, scope: !245)
!340 = !DILocation(line: 277, column: 25, scope: !245)
!341 = !DILocation(line: 278, column: 32, scope: !245)
!342 = !DILocation(line: 278, column: 25, scope: !245)
!343 = !DILocation(line: 279, column: 17, scope: !244)
!344 = !DILocation(line: 280, column: 32, scope: !345)
!345 = distinct !DILexicalBlock(scope: !346, file: !1, line: 280, column: 17)
!346 = distinct !DILexicalBlock(scope: !245, file: !1, line: 280, column: 17)
!347 = !DILocation(line: 280, column: 17, scope: !346)
!348 = !DILocation(line: 282, column: 25, scope: !349)
!349 = distinct !DILexicalBlock(scope: !345, file: !1, line: 281, column: 17)
!350 = !DILocation(line: 283, column: 27, scope: !349)
!351 = !DILocation(line: 284, column: 21, scope: !352)
!352 = distinct !DILexicalBlock(scope: !349, file: !1, line: 284, column: 21)
!353 = !DILocation(line: 285, column: 21, scope: !354)
!354 = distinct !DILexicalBlock(scope: !349, file: !1, line: 285, column: 21)
!355 = !DILocation(line: 286, column: 21, scope: !356)
!356 = distinct !DILexicalBlock(scope: !349, file: !1, line: 286, column: 21)
!357 = !DILocation(line: 287, column: 21, scope: !358)
!358 = distinct !DILexicalBlock(scope: !349, file: !1, line: 287, column: 21)
!359 = !DILocation(line: 280, column: 41, scope: !345)
!360 = distinct !{!360, !347, !361}
!361 = !DILocation(line: 288, column: 17, scope: !346)
!362 = !DILocation(line: 273, column: 35, scope: !246)
!363 = distinct !{!363, !334, !364}
!364 = !DILocation(line: 289, column: 13, scope: !247)
!365 = !DILocation(line: 293, column: 1, scope: !206)
!366 = distinct !DISubprogram(name: "klu_usolve", scope: !1, file: !1, line: 304, type: !367, isLocal: false, isDefinition: true, scopeLine: 316, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !369)
!367 = !DISubroutineType(types: !368)
!368 = !{null, !10, !9, !9, !5, !14, !10, !14}
!369 = !{!370, !371, !372, !373, !374, !375, !376, !377, !378, !379, !380, !381, !382, !383, !384, !385, !386, !392, !397, !402}
!370 = !DILocalVariable(name: "n", arg: 1, scope: !366, file: !1, line: 307, type: !10)
!371 = !DILocalVariable(name: "Uip", arg: 2, scope: !366, file: !1, line: 308, type: !9)
!372 = !DILocalVariable(name: "Ulen", arg: 3, scope: !366, file: !1, line: 309, type: !9)
!373 = !DILocalVariable(name: "LU", arg: 4, scope: !366, file: !1, line: 310, type: !5)
!374 = !DILocalVariable(name: "Udiag", arg: 5, scope: !366, file: !1, line: 311, type: !14)
!375 = !DILocalVariable(name: "nrhs", arg: 6, scope: !366, file: !1, line: 312, type: !10)
!376 = !DILocalVariable(name: "X", arg: 7, scope: !366, file: !1, line: 314, type: !14)
!377 = !DILocalVariable(name: "x", scope: !366, file: !1, line: 317, type: !217)
!378 = !DILocalVariable(name: "uik", scope: !366, file: !1, line: 317, type: !4)
!379 = !DILocalVariable(name: "ukk", scope: !366, file: !1, line: 317, type: !4)
!380 = !DILocalVariable(name: "Ui", scope: !366, file: !1, line: 318, type: !9)
!381 = !DILocalVariable(name: "Ux", scope: !366, file: !1, line: 319, type: !14)
!382 = !DILocalVariable(name: "k", scope: !366, file: !1, line: 320, type: !10)
!383 = !DILocalVariable(name: "p", scope: !366, file: !1, line: 320, type: !10)
!384 = !DILocalVariable(name: "len", scope: !366, file: !1, line: 320, type: !10)
!385 = !DILocalVariable(name: "i", scope: !366, file: !1, line: 320, type: !10)
!386 = !DILocalVariable(name: "xp", scope: !387, file: !1, line: 329, type: !5)
!387 = distinct !DILexicalBlock(scope: !388, file: !1, line: 329, column: 17)
!388 = distinct !DILexicalBlock(scope: !389, file: !1, line: 328, column: 13)
!389 = distinct !DILexicalBlock(scope: !390, file: !1, line: 327, column: 13)
!390 = distinct !DILexicalBlock(scope: !391, file: !1, line: 327, column: 13)
!391 = distinct !DILexicalBlock(scope: !366, file: !1, line: 323, column: 5)
!392 = !DILocalVariable(name: "xp", scope: !393, file: !1, line: 347, type: !5)
!393 = distinct !DILexicalBlock(scope: !394, file: !1, line: 347, column: 17)
!394 = distinct !DILexicalBlock(scope: !395, file: !1, line: 346, column: 13)
!395 = distinct !DILexicalBlock(scope: !396, file: !1, line: 345, column: 13)
!396 = distinct !DILexicalBlock(scope: !391, file: !1, line: 345, column: 13)
!397 = !DILocalVariable(name: "xp", scope: !398, file: !1, line: 373, type: !5)
!398 = distinct !DILexicalBlock(scope: !399, file: !1, line: 373, column: 17)
!399 = distinct !DILexicalBlock(scope: !400, file: !1, line: 372, column: 13)
!400 = distinct !DILexicalBlock(scope: !401, file: !1, line: 371, column: 13)
!401 = distinct !DILexicalBlock(scope: !391, file: !1, line: 371, column: 13)
!402 = !DILocalVariable(name: "xp", scope: !403, file: !1, line: 399, type: !5)
!403 = distinct !DILexicalBlock(scope: !404, file: !1, line: 399, column: 17)
!404 = distinct !DILexicalBlock(scope: !405, file: !1, line: 398, column: 13)
!405 = distinct !DILexicalBlock(scope: !406, file: !1, line: 397, column: 13)
!406 = distinct !DILexicalBlock(scope: !391, file: !1, line: 397, column: 13)
!407 = !DILocation(line: 307, column: 9, scope: !366)
!408 = !DILocation(line: 308, column: 9, scope: !366)
!409 = !DILocation(line: 309, column: 9, scope: !366)
!410 = !DILocation(line: 310, column: 10, scope: !366)
!411 = !DILocation(line: 311, column: 11, scope: !366)
!412 = !DILocation(line: 312, column: 9, scope: !366)
!413 = !DILocation(line: 314, column: 11, scope: !366)
!414 = !DILocation(line: 322, column: 5, scope: !366)
!415 = !DILocation(line: 320, column: 9, scope: !366)
!416 = !DILocation(line: 327, column: 30, scope: !389)
!417 = !DILocation(line: 327, column: 13, scope: !390)
!418 = !DILocation(line: 0, scope: !389)
!419 = !DILocation(line: 329, column: 17, scope: !387)
!420 = !DILocation(line: 320, column: 15, scope: !366)
!421 = !DILocation(line: 318, column: 10, scope: !366)
!422 = !DILocation(line: 319, column: 12, scope: !366)
!423 = !DILocation(line: 331, column: 17, scope: !424)
!424 = distinct !DILexicalBlock(scope: !388, file: !1, line: 331, column: 17)
!425 = !DILocation(line: 317, column: 11, scope: !366)
!426 = !DILocation(line: 332, column: 23, scope: !388)
!427 = !DILocation(line: 320, column: 12, scope: !366)
!428 = !DILocation(line: 333, column: 32, scope: !429)
!429 = distinct !DILexicalBlock(scope: !430, file: !1, line: 333, column: 17)
!430 = distinct !DILexicalBlock(scope: !388, file: !1, line: 333, column: 17)
!431 = !DILocation(line: 333, column: 17, scope: !430)
!432 = !DILocation(line: 336, column: 21, scope: !433)
!433 = distinct !DILexicalBlock(scope: !434, file: !1, line: 336, column: 21)
!434 = distinct !DILexicalBlock(scope: !429, file: !1, line: 334, column: 17)
!435 = !DILocation(line: 333, column: 41, scope: !429)
!436 = distinct !{!436, !431, !437}
!437 = !DILocation(line: 338, column: 17, scope: !430)
!438 = distinct !{!438, !417, !439}
!439 = !DILocation(line: 339, column: 13, scope: !390)
!440 = !DILocation(line: 345, column: 30, scope: !395)
!441 = !DILocation(line: 345, column: 13, scope: !396)
!442 = !DILocation(line: 0, scope: !395)
!443 = !DILocation(line: 347, column: 17, scope: !393)
!444 = !DILocation(line: 348, column: 23, scope: !394)
!445 = !DILocation(line: 317, column: 23, scope: !366)
!446 = !DILocation(line: 351, column: 17, scope: !447)
!447 = distinct !DILexicalBlock(scope: !394, file: !1, line: 351, column: 17)
!448 = !DILocation(line: 352, column: 17, scope: !449)
!449 = distinct !DILexicalBlock(scope: !394, file: !1, line: 352, column: 17)
!450 = !DILocation(line: 354, column: 29, scope: !394)
!451 = !DILocation(line: 355, column: 29, scope: !394)
!452 = !DILocation(line: 356, column: 32, scope: !453)
!453 = distinct !DILexicalBlock(scope: !454, file: !1, line: 356, column: 17)
!454 = distinct !DILexicalBlock(scope: !394, file: !1, line: 356, column: 17)
!455 = !DILocation(line: 356, column: 17, scope: !454)
!456 = !DILocation(line: 358, column: 25, scope: !457)
!457 = distinct !DILexicalBlock(scope: !453, file: !1, line: 357, column: 17)
!458 = !DILocation(line: 320, column: 20, scope: !366)
!459 = !DILocation(line: 359, column: 27, scope: !457)
!460 = !DILocation(line: 317, column: 18, scope: !366)
!461 = !DILocation(line: 362, column: 21, scope: !462)
!462 = distinct !DILexicalBlock(scope: !457, file: !1, line: 362, column: 21)
!463 = !DILocation(line: 363, column: 21, scope: !464)
!464 = distinct !DILexicalBlock(scope: !457, file: !1, line: 363, column: 21)
!465 = !DILocation(line: 356, column: 41, scope: !453)
!466 = distinct !{!466, !455, !467}
!467 = !DILocation(line: 364, column: 17, scope: !454)
!468 = distinct !{!468, !441, !469}
!469 = !DILocation(line: 365, column: 13, scope: !396)
!470 = !DILocation(line: 371, column: 30, scope: !400)
!471 = !DILocation(line: 371, column: 13, scope: !401)
!472 = !DILocation(line: 0, scope: !400)
!473 = !DILocation(line: 373, column: 17, scope: !398)
!474 = !DILocation(line: 374, column: 23, scope: !399)
!475 = !DILocation(line: 376, column: 17, scope: !476)
!476 = distinct !DILexicalBlock(scope: !399, file: !1, line: 376, column: 17)
!477 = !DILocation(line: 377, column: 17, scope: !478)
!478 = distinct !DILexicalBlock(scope: !399, file: !1, line: 377, column: 17)
!479 = !DILocation(line: 378, column: 17, scope: !480)
!480 = distinct !DILexicalBlock(scope: !399, file: !1, line: 378, column: 17)
!481 = !DILocation(line: 380, column: 29, scope: !399)
!482 = !DILocation(line: 381, column: 29, scope: !399)
!483 = !DILocation(line: 382, column: 29, scope: !399)
!484 = !DILocation(line: 383, column: 32, scope: !485)
!485 = distinct !DILexicalBlock(scope: !486, file: !1, line: 383, column: 17)
!486 = distinct !DILexicalBlock(scope: !399, file: !1, line: 383, column: 17)
!487 = !DILocation(line: 383, column: 17, scope: !486)
!488 = !DILocation(line: 385, column: 25, scope: !489)
!489 = distinct !DILexicalBlock(scope: !485, file: !1, line: 384, column: 17)
!490 = !DILocation(line: 386, column: 27, scope: !489)
!491 = !DILocation(line: 387, column: 21, scope: !492)
!492 = distinct !DILexicalBlock(scope: !489, file: !1, line: 387, column: 21)
!493 = !DILocation(line: 388, column: 21, scope: !494)
!494 = distinct !DILexicalBlock(scope: !489, file: !1, line: 388, column: 21)
!495 = !DILocation(line: 389, column: 21, scope: !496)
!496 = distinct !DILexicalBlock(scope: !489, file: !1, line: 389, column: 21)
!497 = !DILocation(line: 383, column: 41, scope: !485)
!498 = distinct !{!498, !487, !499}
!499 = !DILocation(line: 390, column: 17, scope: !486)
!500 = distinct !{!500, !471, !501}
!501 = !DILocation(line: 391, column: 13, scope: !401)
!502 = !DILocation(line: 397, column: 30, scope: !405)
!503 = !DILocation(line: 397, column: 13, scope: !406)
!504 = !DILocation(line: 0, scope: !405)
!505 = !DILocation(line: 399, column: 17, scope: !403)
!506 = !DILocation(line: 400, column: 23, scope: !404)
!507 = !DILocation(line: 402, column: 17, scope: !508)
!508 = distinct !DILexicalBlock(scope: !404, file: !1, line: 402, column: 17)
!509 = !DILocation(line: 403, column: 17, scope: !510)
!510 = distinct !DILexicalBlock(scope: !404, file: !1, line: 403, column: 17)
!511 = !DILocation(line: 404, column: 17, scope: !512)
!512 = distinct !DILexicalBlock(scope: !404, file: !1, line: 404, column: 17)
!513 = !DILocation(line: 405, column: 17, scope: !514)
!514 = distinct !DILexicalBlock(scope: !404, file: !1, line: 405, column: 17)
!515 = !DILocation(line: 407, column: 29, scope: !404)
!516 = !DILocation(line: 408, column: 29, scope: !404)
!517 = !DILocation(line: 409, column: 29, scope: !404)
!518 = !DILocation(line: 410, column: 29, scope: !404)
!519 = !DILocation(line: 411, column: 32, scope: !520)
!520 = distinct !DILexicalBlock(scope: !521, file: !1, line: 411, column: 17)
!521 = distinct !DILexicalBlock(scope: !404, file: !1, line: 411, column: 17)
!522 = !DILocation(line: 411, column: 17, scope: !521)
!523 = !DILocation(line: 413, column: 25, scope: !524)
!524 = distinct !DILexicalBlock(scope: !520, file: !1, line: 412, column: 17)
!525 = !DILocation(line: 414, column: 27, scope: !524)
!526 = !DILocation(line: 416, column: 21, scope: !527)
!527 = distinct !DILexicalBlock(scope: !524, file: !1, line: 416, column: 21)
!528 = !DILocation(line: 417, column: 21, scope: !529)
!529 = distinct !DILexicalBlock(scope: !524, file: !1, line: 417, column: 21)
!530 = !DILocation(line: 418, column: 21, scope: !531)
!531 = distinct !DILexicalBlock(scope: !524, file: !1, line: 418, column: 21)
!532 = !DILocation(line: 419, column: 21, scope: !533)
!533 = distinct !DILexicalBlock(scope: !524, file: !1, line: 419, column: 21)
!534 = !DILocation(line: 411, column: 41, scope: !520)
!535 = distinct !{!535, !522, !536}
!536 = !DILocation(line: 420, column: 17, scope: !521)
!537 = distinct !{!537, !503, !538}
!538 = !DILocation(line: 421, column: 13, scope: !406)
!539 = !DILocation(line: 426, column: 1, scope: !366)
!540 = distinct !DISubprogram(name: "klu_ltsolve", scope: !1, file: !1, line: 438, type: !207, isLocal: false, isDefinition: true, scopeLine: 452, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !541)
!541 = !{!542, !543, !544, !545, !546, !547, !548, !549, !550, !551, !552, !553, !554, !555, !556, !562, !567, !572}
!542 = !DILocalVariable(name: "n", arg: 1, scope: !540, file: !1, line: 441, type: !10)
!543 = !DILocalVariable(name: "Lip", arg: 2, scope: !540, file: !1, line: 442, type: !9)
!544 = !DILocalVariable(name: "Llen", arg: 3, scope: !540, file: !1, line: 443, type: !9)
!545 = !DILocalVariable(name: "LU", arg: 4, scope: !540, file: !1, line: 444, type: !5)
!546 = !DILocalVariable(name: "nrhs", arg: 5, scope: !540, file: !1, line: 445, type: !10)
!547 = !DILocalVariable(name: "X", arg: 6, scope: !540, file: !1, line: 450, type: !14)
!548 = !DILocalVariable(name: "x", scope: !540, file: !1, line: 453, type: !217)
!549 = !DILocalVariable(name: "lik", scope: !540, file: !1, line: 453, type: !4)
!550 = !DILocalVariable(name: "Li", scope: !540, file: !1, line: 454, type: !9)
!551 = !DILocalVariable(name: "Lx", scope: !540, file: !1, line: 455, type: !14)
!552 = !DILocalVariable(name: "k", scope: !540, file: !1, line: 456, type: !10)
!553 = !DILocalVariable(name: "p", scope: !540, file: !1, line: 456, type: !10)
!554 = !DILocalVariable(name: "len", scope: !540, file: !1, line: 456, type: !10)
!555 = !DILocalVariable(name: "i", scope: !540, file: !1, line: 456, type: !10)
!556 = !DILocalVariable(name: "xp", scope: !557, file: !1, line: 465, type: !5)
!557 = distinct !DILexicalBlock(scope: !558, file: !1, line: 465, column: 17)
!558 = distinct !DILexicalBlock(scope: !559, file: !1, line: 464, column: 13)
!559 = distinct !DILexicalBlock(scope: !560, file: !1, line: 463, column: 13)
!560 = distinct !DILexicalBlock(scope: !561, file: !1, line: 463, column: 13)
!561 = distinct !DILexicalBlock(scope: !540, file: !1, line: 459, column: 5)
!562 = !DILocalVariable(name: "xp", scope: !563, file: !1, line: 492, type: !5)
!563 = distinct !DILexicalBlock(scope: !564, file: !1, line: 492, column: 17)
!564 = distinct !DILexicalBlock(scope: !565, file: !1, line: 489, column: 13)
!565 = distinct !DILexicalBlock(scope: !566, file: !1, line: 488, column: 13)
!566 = distinct !DILexicalBlock(scope: !561, file: !1, line: 488, column: 13)
!567 = !DILocalVariable(name: "xp", scope: !568, file: !1, line: 521, type: !5)
!568 = distinct !DILexicalBlock(scope: !569, file: !1, line: 521, column: 17)
!569 = distinct !DILexicalBlock(scope: !570, file: !1, line: 517, column: 13)
!570 = distinct !DILexicalBlock(scope: !571, file: !1, line: 516, column: 13)
!571 = distinct !DILexicalBlock(scope: !561, file: !1, line: 516, column: 13)
!572 = !DILocalVariable(name: "xp", scope: !573, file: !1, line: 553, type: !5)
!573 = distinct !DILexicalBlock(scope: !574, file: !1, line: 553, column: 17)
!574 = distinct !DILexicalBlock(scope: !575, file: !1, line: 548, column: 13)
!575 = distinct !DILexicalBlock(scope: !576, file: !1, line: 547, column: 13)
!576 = distinct !DILexicalBlock(scope: !561, file: !1, line: 547, column: 13)
!577 = !DILocation(line: 441, column: 9, scope: !540)
!578 = !DILocation(line: 442, column: 9, scope: !540)
!579 = !DILocation(line: 443, column: 9, scope: !540)
!580 = !DILocation(line: 444, column: 10, scope: !540)
!581 = !DILocation(line: 445, column: 9, scope: !540)
!582 = !DILocation(line: 450, column: 11, scope: !540)
!583 = !DILocation(line: 458, column: 5, scope: !540)
!584 = !DILocation(line: 456, column: 9, scope: !540)
!585 = !DILocation(line: 463, column: 30, scope: !559)
!586 = !DILocation(line: 463, column: 13, scope: !560)
!587 = !DILocation(line: 0, scope: !559)
!588 = !DILocation(line: 465, column: 17, scope: !557)
!589 = !DILocation(line: 456, column: 15, scope: !540)
!590 = !DILocation(line: 454, column: 10, scope: !540)
!591 = !DILocation(line: 455, column: 12, scope: !540)
!592 = !DILocation(line: 466, column: 25, scope: !558)
!593 = !DILocation(line: 453, column: 11, scope: !540)
!594 = !DILocation(line: 456, column: 12, scope: !540)
!595 = !DILocation(line: 467, column: 32, scope: !596)
!596 = distinct !DILexicalBlock(scope: !597, file: !1, line: 467, column: 17)
!597 = distinct !DILexicalBlock(scope: !558, file: !1, line: 467, column: 17)
!598 = !DILocation(line: 467, column: 17, scope: !597)
!599 = !DILocation(line: 479, column: 25, scope: !600)
!600 = distinct !DILexicalBlock(scope: !601, file: !1, line: 479, column: 25)
!601 = distinct !DILexicalBlock(scope: !602, file: !1, line: 477, column: 21)
!602 = distinct !DILexicalBlock(scope: !596, file: !1, line: 468, column: 17)
!603 = !DILocation(line: 467, column: 41, scope: !596)
!604 = distinct !{!604, !598, !605}
!605 = !DILocation(line: 481, column: 17, scope: !597)
!606 = !DILocation(line: 482, column: 23, scope: !558)
!607 = distinct !{!607, !586, !608}
!608 = !DILocation(line: 483, column: 13, scope: !560)
!609 = !DILocation(line: 488, column: 30, scope: !565)
!610 = !DILocation(line: 488, column: 13, scope: !566)
!611 = !DILocation(line: 0, scope: !565)
!612 = !DILocation(line: 490, column: 29, scope: !564)
!613 = !DILocation(line: 490, column: 25, scope: !564)
!614 = !DILocation(line: 491, column: 32, scope: !564)
!615 = !DILocation(line: 491, column: 25, scope: !564)
!616 = !DILocation(line: 492, column: 17, scope: !563)
!617 = !DILocation(line: 493, column: 32, scope: !618)
!618 = distinct !DILexicalBlock(scope: !619, file: !1, line: 493, column: 17)
!619 = distinct !DILexicalBlock(scope: !564, file: !1, line: 493, column: 17)
!620 = !DILocation(line: 493, column: 17, scope: !619)
!621 = !DILocation(line: 495, column: 25, scope: !622)
!622 = distinct !DILexicalBlock(scope: !618, file: !1, line: 494, column: 17)
!623 = !DILocation(line: 456, column: 20, scope: !540)
!624 = !DILocation(line: 504, column: 31, scope: !625)
!625 = distinct !DILexicalBlock(scope: !622, file: !1, line: 503, column: 21)
!626 = !DILocation(line: 453, column: 18, scope: !540)
!627 = !DILocation(line: 506, column: 21, scope: !628)
!628 = distinct !DILexicalBlock(scope: !622, file: !1, line: 506, column: 21)
!629 = !DILocation(line: 507, column: 21, scope: !630)
!630 = distinct !DILexicalBlock(scope: !622, file: !1, line: 507, column: 21)
!631 = !DILocation(line: 493, column: 41, scope: !618)
!632 = distinct !{!632, !620, !633}
!633 = !DILocation(line: 508, column: 17, scope: !619)
!634 = !DILocation(line: 509, column: 29, scope: !564)
!635 = !DILocation(line: 510, column: 29, scope: !564)
!636 = distinct !{!636, !610, !637}
!637 = !DILocation(line: 511, column: 13, scope: !566)
!638 = !DILocation(line: 516, column: 30, scope: !570)
!639 = !DILocation(line: 516, column: 13, scope: !571)
!640 = !DILocation(line: 0, scope: !570)
!641 = !DILocation(line: 518, column: 29, scope: !569)
!642 = !DILocation(line: 518, column: 25, scope: !569)
!643 = !DILocation(line: 519, column: 32, scope: !569)
!644 = !DILocation(line: 519, column: 25, scope: !569)
!645 = !DILocation(line: 520, column: 32, scope: !569)
!646 = !DILocation(line: 520, column: 25, scope: !569)
!647 = !DILocation(line: 521, column: 17, scope: !568)
!648 = !DILocation(line: 522, column: 32, scope: !649)
!649 = distinct !DILexicalBlock(scope: !650, file: !1, line: 522, column: 17)
!650 = distinct !DILexicalBlock(scope: !569, file: !1, line: 522, column: 17)
!651 = !DILocation(line: 522, column: 17, scope: !650)
!652 = !DILocation(line: 524, column: 25, scope: !653)
!653 = distinct !DILexicalBlock(scope: !649, file: !1, line: 523, column: 17)
!654 = !DILocation(line: 533, column: 31, scope: !655)
!655 = distinct !DILexicalBlock(scope: !653, file: !1, line: 532, column: 21)
!656 = !DILocation(line: 535, column: 21, scope: !657)
!657 = distinct !DILexicalBlock(scope: !653, file: !1, line: 535, column: 21)
!658 = !DILocation(line: 536, column: 21, scope: !659)
!659 = distinct !DILexicalBlock(scope: !653, file: !1, line: 536, column: 21)
!660 = !DILocation(line: 537, column: 21, scope: !661)
!661 = distinct !DILexicalBlock(scope: !653, file: !1, line: 537, column: 21)
!662 = !DILocation(line: 522, column: 41, scope: !649)
!663 = distinct !{!663, !651, !664}
!664 = !DILocation(line: 538, column: 17, scope: !650)
!665 = !DILocation(line: 539, column: 29, scope: !569)
!666 = !DILocation(line: 540, column: 29, scope: !569)
!667 = !DILocation(line: 541, column: 29, scope: !569)
!668 = distinct !{!668, !639, !669}
!669 = !DILocation(line: 542, column: 13, scope: !571)
!670 = !DILocation(line: 547, column: 30, scope: !575)
!671 = !DILocation(line: 547, column: 13, scope: !576)
!672 = !DILocation(line: 0, scope: !575)
!673 = !DILocation(line: 549, column: 29, scope: !574)
!674 = !DILocation(line: 549, column: 25, scope: !574)
!675 = !DILocation(line: 550, column: 32, scope: !574)
!676 = !DILocation(line: 550, column: 25, scope: !574)
!677 = !DILocation(line: 551, column: 32, scope: !574)
!678 = !DILocation(line: 551, column: 25, scope: !574)
!679 = !DILocation(line: 552, column: 32, scope: !574)
!680 = !DILocation(line: 552, column: 25, scope: !574)
!681 = !DILocation(line: 553, column: 17, scope: !573)
!682 = !DILocation(line: 554, column: 32, scope: !683)
!683 = distinct !DILexicalBlock(scope: !684, file: !1, line: 554, column: 17)
!684 = distinct !DILexicalBlock(scope: !574, file: !1, line: 554, column: 17)
!685 = !DILocation(line: 554, column: 17, scope: !684)
!686 = !DILocation(line: 556, column: 25, scope: !687)
!687 = distinct !DILexicalBlock(scope: !683, file: !1, line: 555, column: 17)
!688 = !DILocation(line: 565, column: 31, scope: !689)
!689 = distinct !DILexicalBlock(scope: !687, file: !1, line: 564, column: 21)
!690 = !DILocation(line: 567, column: 21, scope: !691)
!691 = distinct !DILexicalBlock(scope: !687, file: !1, line: 567, column: 21)
!692 = !DILocation(line: 568, column: 21, scope: !693)
!693 = distinct !DILexicalBlock(scope: !687, file: !1, line: 568, column: 21)
!694 = !DILocation(line: 569, column: 21, scope: !695)
!695 = distinct !DILexicalBlock(scope: !687, file: !1, line: 569, column: 21)
!696 = !DILocation(line: 570, column: 21, scope: !697)
!697 = distinct !DILexicalBlock(scope: !687, file: !1, line: 570, column: 21)
!698 = !DILocation(line: 554, column: 41, scope: !683)
!699 = distinct !{!699, !685, !700}
!700 = !DILocation(line: 571, column: 17, scope: !684)
!701 = !DILocation(line: 572, column: 29, scope: !574)
!702 = !DILocation(line: 573, column: 29, scope: !574)
!703 = !DILocation(line: 574, column: 29, scope: !574)
!704 = !DILocation(line: 575, column: 29, scope: !574)
!705 = distinct !{!705, !671, !706}
!706 = !DILocation(line: 576, column: 13, scope: !576)
!707 = !DILocation(line: 579, column: 1, scope: !540)
!708 = distinct !DISubprogram(name: "klu_utsolve", scope: !1, file: !1, line: 591, type: !367, isLocal: false, isDefinition: true, scopeLine: 606, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !709)
!709 = !{!710, !711, !712, !713, !714, !715, !716, !717, !718, !719, !720, !721, !722, !723, !724, !725, !726, !732, !737, !742}
!710 = !DILocalVariable(name: "n", arg: 1, scope: !708, file: !1, line: 594, type: !10)
!711 = !DILocalVariable(name: "Uip", arg: 2, scope: !708, file: !1, line: 595, type: !9)
!712 = !DILocalVariable(name: "Ulen", arg: 3, scope: !708, file: !1, line: 596, type: !9)
!713 = !DILocalVariable(name: "LU", arg: 4, scope: !708, file: !1, line: 597, type: !5)
!714 = !DILocalVariable(name: "Udiag", arg: 5, scope: !708, file: !1, line: 598, type: !14)
!715 = !DILocalVariable(name: "nrhs", arg: 6, scope: !708, file: !1, line: 599, type: !10)
!716 = !DILocalVariable(name: "X", arg: 7, scope: !708, file: !1, line: 604, type: !14)
!717 = !DILocalVariable(name: "x", scope: !708, file: !1, line: 607, type: !217)
!718 = !DILocalVariable(name: "uik", scope: !708, file: !1, line: 607, type: !4)
!719 = !DILocalVariable(name: "ukk", scope: !708, file: !1, line: 607, type: !4)
!720 = !DILocalVariable(name: "k", scope: !708, file: !1, line: 608, type: !10)
!721 = !DILocalVariable(name: "p", scope: !708, file: !1, line: 608, type: !10)
!722 = !DILocalVariable(name: "len", scope: !708, file: !1, line: 608, type: !10)
!723 = !DILocalVariable(name: "i", scope: !708, file: !1, line: 608, type: !10)
!724 = !DILocalVariable(name: "Ui", scope: !708, file: !1, line: 609, type: !9)
!725 = !DILocalVariable(name: "Ux", scope: !708, file: !1, line: 610, type: !14)
!726 = !DILocalVariable(name: "xp", scope: !727, file: !1, line: 619, type: !5)
!727 = distinct !DILexicalBlock(scope: !728, file: !1, line: 619, column: 17)
!728 = distinct !DILexicalBlock(scope: !729, file: !1, line: 618, column: 13)
!729 = distinct !DILexicalBlock(scope: !730, file: !1, line: 617, column: 13)
!730 = distinct !DILexicalBlock(scope: !731, file: !1, line: 617, column: 13)
!731 = distinct !DILexicalBlock(scope: !708, file: !1, line: 613, column: 5)
!732 = !DILocalVariable(name: "xp", scope: !733, file: !1, line: 654, type: !5)
!733 = distinct !DILexicalBlock(scope: !734, file: !1, line: 654, column: 17)
!734 = distinct !DILexicalBlock(scope: !735, file: !1, line: 653, column: 13)
!735 = distinct !DILexicalBlock(scope: !736, file: !1, line: 652, column: 13)
!736 = distinct !DILexicalBlock(scope: !731, file: !1, line: 652, column: 13)
!737 = !DILocalVariable(name: "xp", scope: !738, file: !1, line: 692, type: !5)
!738 = distinct !DILexicalBlock(scope: !739, file: !1, line: 692, column: 17)
!739 = distinct !DILexicalBlock(scope: !740, file: !1, line: 691, column: 13)
!740 = distinct !DILexicalBlock(scope: !741, file: !1, line: 690, column: 13)
!741 = distinct !DILexicalBlock(scope: !731, file: !1, line: 690, column: 13)
!742 = !DILocalVariable(name: "xp", scope: !743, file: !1, line: 733, type: !5)
!743 = distinct !DILexicalBlock(scope: !744, file: !1, line: 733, column: 17)
!744 = distinct !DILexicalBlock(scope: !745, file: !1, line: 732, column: 13)
!745 = distinct !DILexicalBlock(scope: !746, file: !1, line: 731, column: 13)
!746 = distinct !DILexicalBlock(scope: !731, file: !1, line: 731, column: 13)
!747 = !DILocation(line: 594, column: 9, scope: !708)
!748 = !DILocation(line: 595, column: 9, scope: !708)
!749 = !DILocation(line: 596, column: 9, scope: !708)
!750 = !DILocation(line: 597, column: 10, scope: !708)
!751 = !DILocation(line: 598, column: 11, scope: !708)
!752 = !DILocation(line: 599, column: 9, scope: !708)
!753 = !DILocation(line: 604, column: 11, scope: !708)
!754 = !DILocation(line: 612, column: 5, scope: !708)
!755 = !DILocation(line: 608, column: 9, scope: !708)
!756 = !DILocation(line: 617, column: 28, scope: !729)
!757 = !DILocation(line: 617, column: 13, scope: !730)
!758 = !DILocation(line: 619, column: 17, scope: !727)
!759 = !DILocation(line: 608, column: 15, scope: !708)
!760 = !DILocation(line: 609, column: 10, scope: !708)
!761 = !DILocation(line: 610, column: 12, scope: !708)
!762 = !DILocation(line: 620, column: 25, scope: !728)
!763 = !DILocation(line: 607, column: 11, scope: !708)
!764 = !DILocation(line: 608, column: 12, scope: !708)
!765 = !DILocation(line: 621, column: 32, scope: !766)
!766 = distinct !DILexicalBlock(scope: !767, file: !1, line: 621, column: 17)
!767 = distinct !DILexicalBlock(scope: !728, file: !1, line: 621, column: 17)
!768 = !DILocation(line: 621, column: 17, scope: !767)
!769 = !DILocation(line: 633, column: 25, scope: !770)
!770 = distinct !DILexicalBlock(scope: !771, file: !1, line: 633, column: 25)
!771 = distinct !DILexicalBlock(scope: !772, file: !1, line: 631, column: 21)
!772 = distinct !DILexicalBlock(scope: !766, file: !1, line: 622, column: 17)
!773 = !DILocation(line: 621, column: 41, scope: !766)
!774 = distinct !{!774, !768, !775}
!775 = !DILocation(line: 635, column: 17, scope: !767)
!776 = !DILocation(line: 644, column: 27, scope: !777)
!777 = distinct !DILexicalBlock(scope: !728, file: !1, line: 643, column: 17)
!778 = !DILocation(line: 607, column: 23, scope: !708)
!779 = !DILocation(line: 646, column: 17, scope: !780)
!780 = distinct !DILexicalBlock(scope: !728, file: !1, line: 646, column: 17)
!781 = !DILocation(line: 617, column: 35, scope: !729)
!782 = distinct !{!782, !757, !783}
!783 = !DILocation(line: 647, column: 13, scope: !730)
!784 = !DILocation(line: 652, column: 28, scope: !735)
!785 = !DILocation(line: 652, column: 13, scope: !736)
!786 = !DILocation(line: 654, column: 17, scope: !733)
!787 = !DILocation(line: 655, column: 29, scope: !734)
!788 = !DILocation(line: 655, column: 25, scope: !734)
!789 = !DILocation(line: 656, column: 32, scope: !734)
!790 = !DILocation(line: 656, column: 25, scope: !734)
!791 = !DILocation(line: 657, column: 32, scope: !792)
!792 = distinct !DILexicalBlock(scope: !793, file: !1, line: 657, column: 17)
!793 = distinct !DILexicalBlock(scope: !734, file: !1, line: 657, column: 17)
!794 = !DILocation(line: 657, column: 17, scope: !793)
!795 = !DILocation(line: 659, column: 25, scope: !796)
!796 = distinct !DILexicalBlock(scope: !792, file: !1, line: 658, column: 17)
!797 = !DILocation(line: 608, column: 20, scope: !708)
!798 = !DILocation(line: 668, column: 31, scope: !799)
!799 = distinct !DILexicalBlock(scope: !796, file: !1, line: 667, column: 21)
!800 = !DILocation(line: 607, column: 18, scope: !708)
!801 = !DILocation(line: 670, column: 21, scope: !802)
!802 = distinct !DILexicalBlock(scope: !796, file: !1, line: 670, column: 21)
!803 = !DILocation(line: 671, column: 21, scope: !804)
!804 = distinct !DILexicalBlock(scope: !796, file: !1, line: 671, column: 21)
!805 = !DILocation(line: 657, column: 41, scope: !792)
!806 = distinct !{!806, !794, !807}
!807 = !DILocation(line: 672, column: 17, scope: !793)
!808 = !DILocation(line: 681, column: 27, scope: !809)
!809 = distinct !DILexicalBlock(scope: !734, file: !1, line: 680, column: 17)
!810 = !DILocation(line: 683, column: 17, scope: !811)
!811 = distinct !DILexicalBlock(scope: !734, file: !1, line: 683, column: 17)
!812 = !DILocation(line: 684, column: 17, scope: !813)
!813 = distinct !DILexicalBlock(scope: !734, file: !1, line: 684, column: 17)
!814 = !DILocation(line: 652, column: 35, scope: !735)
!815 = distinct !{!815, !785, !816}
!816 = !DILocation(line: 685, column: 13, scope: !736)
!817 = !DILocation(line: 690, column: 28, scope: !740)
!818 = !DILocation(line: 690, column: 13, scope: !741)
!819 = !DILocation(line: 692, column: 17, scope: !738)
!820 = !DILocation(line: 693, column: 29, scope: !739)
!821 = !DILocation(line: 693, column: 25, scope: !739)
!822 = !DILocation(line: 694, column: 32, scope: !739)
!823 = !DILocation(line: 694, column: 25, scope: !739)
!824 = !DILocation(line: 695, column: 32, scope: !739)
!825 = !DILocation(line: 695, column: 25, scope: !739)
!826 = !DILocation(line: 696, column: 32, scope: !827)
!827 = distinct !DILexicalBlock(scope: !828, file: !1, line: 696, column: 17)
!828 = distinct !DILexicalBlock(scope: !739, file: !1, line: 696, column: 17)
!829 = !DILocation(line: 696, column: 17, scope: !828)
!830 = !DILocation(line: 698, column: 25, scope: !831)
!831 = distinct !DILexicalBlock(scope: !827, file: !1, line: 697, column: 17)
!832 = !DILocation(line: 707, column: 31, scope: !833)
!833 = distinct !DILexicalBlock(scope: !831, file: !1, line: 706, column: 21)
!834 = !DILocation(line: 709, column: 21, scope: !835)
!835 = distinct !DILexicalBlock(scope: !831, file: !1, line: 709, column: 21)
!836 = !DILocation(line: 710, column: 21, scope: !837)
!837 = distinct !DILexicalBlock(scope: !831, file: !1, line: 710, column: 21)
!838 = !DILocation(line: 711, column: 21, scope: !839)
!839 = distinct !DILexicalBlock(scope: !831, file: !1, line: 711, column: 21)
!840 = !DILocation(line: 696, column: 41, scope: !827)
!841 = distinct !{!841, !829, !842}
!842 = !DILocation(line: 712, column: 17, scope: !828)
!843 = !DILocation(line: 721, column: 27, scope: !844)
!844 = distinct !DILexicalBlock(scope: !739, file: !1, line: 720, column: 17)
!845 = !DILocation(line: 723, column: 17, scope: !846)
!846 = distinct !DILexicalBlock(scope: !739, file: !1, line: 723, column: 17)
!847 = !DILocation(line: 724, column: 17, scope: !848)
!848 = distinct !DILexicalBlock(scope: !739, file: !1, line: 724, column: 17)
!849 = !DILocation(line: 725, column: 17, scope: !850)
!850 = distinct !DILexicalBlock(scope: !739, file: !1, line: 725, column: 17)
!851 = !DILocation(line: 690, column: 35, scope: !740)
!852 = distinct !{!852, !818, !853}
!853 = !DILocation(line: 726, column: 13, scope: !741)
!854 = !DILocation(line: 731, column: 28, scope: !745)
!855 = !DILocation(line: 731, column: 13, scope: !746)
!856 = !DILocation(line: 733, column: 17, scope: !743)
!857 = !DILocation(line: 734, column: 29, scope: !744)
!858 = !DILocation(line: 734, column: 25, scope: !744)
!859 = !DILocation(line: 735, column: 32, scope: !744)
!860 = !DILocation(line: 735, column: 25, scope: !744)
!861 = !DILocation(line: 736, column: 32, scope: !744)
!862 = !DILocation(line: 736, column: 25, scope: !744)
!863 = !DILocation(line: 737, column: 32, scope: !744)
!864 = !DILocation(line: 737, column: 25, scope: !744)
!865 = !DILocation(line: 738, column: 32, scope: !866)
!866 = distinct !DILexicalBlock(scope: !867, file: !1, line: 738, column: 17)
!867 = distinct !DILexicalBlock(scope: !744, file: !1, line: 738, column: 17)
!868 = !DILocation(line: 738, column: 17, scope: !867)
!869 = !DILocation(line: 740, column: 25, scope: !870)
!870 = distinct !DILexicalBlock(scope: !866, file: !1, line: 739, column: 17)
!871 = !DILocation(line: 749, column: 31, scope: !872)
!872 = distinct !DILexicalBlock(scope: !870, file: !1, line: 748, column: 21)
!873 = !DILocation(line: 751, column: 21, scope: !874)
!874 = distinct !DILexicalBlock(scope: !870, file: !1, line: 751, column: 21)
!875 = !DILocation(line: 752, column: 21, scope: !876)
!876 = distinct !DILexicalBlock(scope: !870, file: !1, line: 752, column: 21)
!877 = !DILocation(line: 753, column: 21, scope: !878)
!878 = distinct !DILexicalBlock(scope: !870, file: !1, line: 753, column: 21)
!879 = !DILocation(line: 754, column: 21, scope: !880)
!880 = distinct !DILexicalBlock(scope: !870, file: !1, line: 754, column: 21)
!881 = !DILocation(line: 738, column: 41, scope: !866)
!882 = distinct !{!882, !868, !883}
!883 = !DILocation(line: 755, column: 17, scope: !867)
!884 = !DILocation(line: 764, column: 27, scope: !885)
!885 = distinct !DILexicalBlock(scope: !744, file: !1, line: 763, column: 17)
!886 = !DILocation(line: 766, column: 17, scope: !887)
!887 = distinct !DILexicalBlock(scope: !744, file: !1, line: 766, column: 17)
!888 = !DILocation(line: 767, column: 17, scope: !889)
!889 = distinct !DILexicalBlock(scope: !744, file: !1, line: 767, column: 17)
!890 = !DILocation(line: 768, column: 17, scope: !891)
!891 = distinct !DILexicalBlock(scope: !744, file: !1, line: 768, column: 17)
!892 = !DILocation(line: 769, column: 17, scope: !893)
!893 = distinct !DILexicalBlock(scope: !744, file: !1, line: 769, column: 17)
!894 = !DILocation(line: 731, column: 35, scope: !745)
!895 = distinct !{!895, !855, !896}
!896 = !DILocation(line: 770, column: 13, scope: !746)
!897 = !DILocation(line: 773, column: 1, scope: !708)
