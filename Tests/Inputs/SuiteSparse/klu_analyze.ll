; ModuleID = 'klu_analyze.c'
source_filename = "klu_analyze.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_analyze(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !11 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !74, metadata !DIExpression()), !dbg !78
  call void @llvm.dbg.value(metadata i32* %1, metadata !75, metadata !DIExpression()), !dbg !79
  call void @llvm.dbg.value(metadata i32* %2, metadata !76, metadata !DIExpression()), !dbg !80
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !77, metadata !DIExpression()), !dbg !81
  %5 = icmp eq %struct.klu_common_struct* %3, null, !dbg !82
  br i1 %5, label %16, label %6, !dbg !84

; <label>:6:                                      ; preds = %4
  %7 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11, !dbg !85
  store i32 0, i32* %7, align 4, !dbg !86, !tbaa !87
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 13, !dbg !95
  store i32 -1, i32* %8, align 4, !dbg !96, !tbaa !97
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 6, !dbg !98
  %10 = load i32, i32* %9, align 4, !dbg !98, !tbaa !100
  %11 = icmp eq i32 %10, 2, !dbg !101
  br i1 %11, label %12, label %14, !dbg !102

; <label>:12:                                     ; preds = %6
  %13 = tail call %struct.klu_symbolic* @klu_analyze_given(i32 %0, i32* %1, i32* %2, i32* null, i32* null, %struct.klu_common_struct* nonnull %3) #4, !dbg !103
  br label %16, !dbg !105

; <label>:14:                                     ; preds = %6
  %15 = tail call fastcc %struct.klu_symbolic* @order_and_analyze(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* nonnull %3), !dbg !106
  br label %16, !dbg !108

; <label>:16:                                     ; preds = %4, %14, %12
  %17 = phi %struct.klu_symbolic* [ %13, %12 ], [ %15, %14 ], [ null, %4 ], !dbg !109
  ret %struct.klu_symbolic* %17, !dbg !110
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.declare(metadata, metadata, metadata) #1

declare %struct.klu_symbolic* @klu_analyze_given(i32, i32*, i32*, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define internal fastcc %struct.klu_symbolic* @order_and_analyze(i32, i32*, i32*, %struct.klu_common_struct*) unnamed_addr #0 !dbg !111 {
  %5 = alloca double, align 8
  %6 = alloca %struct.klu_symbolic*, align 8
  call void @llvm.dbg.value(metadata i32 %0, metadata !113, metadata !DIExpression()), !dbg !141
  call void @llvm.dbg.value(metadata i32* %1, metadata !114, metadata !DIExpression()), !dbg !142
  call void @llvm.dbg.value(metadata i32* %2, metadata !115, metadata !DIExpression()), !dbg !143
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !116, metadata !DIExpression()), !dbg !144
  %7 = bitcast double* %5 to i8*, !dbg !145
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %7) #4, !dbg !145
  %8 = bitcast %struct.klu_symbolic** %6 to i8*, !dbg !146
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %8) #4, !dbg !146
  %9 = tail call %struct.klu_symbolic* @klu_alloc_symbolic(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* %3) #4, !dbg !147
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %9, metadata !118, metadata !DIExpression()), !dbg !148
  store %struct.klu_symbolic* %9, %struct.klu_symbolic** %6, align 8, !dbg !149, !tbaa !150
  %10 = icmp eq %struct.klu_symbolic* %9, null, !dbg !151
  br i1 %10, label %170, label %11, !dbg !153

; <label>:11:                                     ; preds = %4
  %12 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 7, !dbg !154
  %13 = load i32*, i32** %12, align 8, !dbg !154, !tbaa !155
  call void @llvm.dbg.value(metadata i32* %13, metadata !126, metadata !DIExpression()), !dbg !157
  %14 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 8, !dbg !158
  %15 = load i32*, i32** %14, align 8, !dbg !158, !tbaa !159
  call void @llvm.dbg.value(metadata i32* %15, metadata !127, metadata !DIExpression()), !dbg !160
  %16 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 9, !dbg !161
  %17 = load i32*, i32** %16, align 8, !dbg !161, !tbaa !162
  call void @llvm.dbg.value(metadata i32* %17, metadata !128, metadata !DIExpression()), !dbg !163
  %18 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 4, !dbg !164
  %19 = load double*, double** %18, align 8, !dbg !164, !tbaa !165
  call void @llvm.dbg.value(metadata double* %19, metadata !119, metadata !DIExpression()), !dbg !166
  %20 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 6, !dbg !167
  %21 = load i32, i32* %20, align 4, !dbg !167, !tbaa !168
  call void @llvm.dbg.value(metadata i32 %21, metadata !130, metadata !DIExpression()), !dbg !169
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 6, !dbg !170
  %23 = load i32, i32* %22, align 4, !dbg !170, !tbaa !100
  call void @llvm.dbg.value(metadata i32 %23, metadata !137, metadata !DIExpression()), !dbg !171
  switch i32 %23, label %33 [
    i32 1, label %24
    i32 0, label %31
    i32 3, label %27
  ], !dbg !172

; <label>:24:                                     ; preds = %11
  %25 = tail call i64 @colamd_recommended(i32 %21, i32 %0, i32 %0) #4, !dbg !173
  %26 = trunc i64 %25 to i32, !dbg !173
  call void @llvm.dbg.value(metadata i32 %26, metadata !139, metadata !DIExpression()), !dbg !176
  br label %36, !dbg !177

; <label>:27:                                     ; preds = %11
  %28 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 8, !dbg !178
  %29 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %28, align 8, !dbg !178, !tbaa !180
  %30 = icmp eq i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)* %29, null, !dbg !181
  br i1 %30, label %33, label %31, !dbg !182

; <label>:31:                                     ; preds = %11, %27
  %32 = add nsw i32 %21, 1, !dbg !183
  call void @llvm.dbg.value(metadata i32 %32, metadata !139, metadata !DIExpression()), !dbg !176
  br label %36

; <label>:33:                                     ; preds = %11, %27
  %34 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11, !dbg !185
  store i32 -3, i32* %34, align 4, !dbg !187, !tbaa !87
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %6, metadata !118, metadata !DIExpression()), !dbg !148
  %35 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %6, %struct.klu_common_struct* nonnull %3) #4, !dbg !188
  br label %170, !dbg !189

; <label>:36:                                     ; preds = %31, %24
  %37 = phi i32 [ %26, %24 ], [ %32, %31 ], !dbg !190
  call void @llvm.dbg.value(metadata i32 %37, metadata !139, metadata !DIExpression()), !dbg !176
  %38 = sext i32 %0 to i64, !dbg !191
  %39 = tail call i8* @klu_malloc(i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !192
  %40 = bitcast i8* %39 to i32*, !dbg !192
  call void @llvm.dbg.value(metadata i32* %40, metadata !125, metadata !DIExpression()), !dbg !193
  %41 = tail call i8* @klu_malloc(i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !194
  %42 = bitcast i8* %41 to i32*, !dbg !194
  call void @llvm.dbg.value(metadata i32* %42, metadata !120, metadata !DIExpression()), !dbg !195
  %43 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11, !dbg !196
  %44 = load i32, i32* %43, align 4, !dbg !196, !tbaa !87
  %45 = icmp slt i32 %44, 0, !dbg !198
  br i1 %45, label %46, label %50, !dbg !199

; <label>:46:                                     ; preds = %36
  %47 = tail call i8* @klu_free(i8* %39, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !200
  %48 = tail call i8* @klu_free(i8* %41, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !202
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %6, metadata !118, metadata !DIExpression()), !dbg !148
  %49 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %6, %struct.klu_common_struct* nonnull %3) #4, !dbg !203
  br label %170, !dbg !204

; <label>:50:                                     ; preds = %36
  %51 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 5, !dbg !205
  %52 = load i32, i32* %51, align 8, !dbg !205, !tbaa !206
  call void @llvm.dbg.value(metadata i32 %52, metadata !136, metadata !DIExpression()), !dbg !207
  %53 = icmp ne i32 %52, 0, !dbg !208
  %54 = zext i1 %53 to i32, !dbg !208
  call void @llvm.dbg.value(metadata i32 %54, metadata !136, metadata !DIExpression()), !dbg !207
  %55 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !209, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %55, metadata !118, metadata !DIExpression()), !dbg !148
  %56 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %55, i64 0, i32 13, !dbg !210
  store i32 %23, i32* %56, align 4, !dbg !211, !tbaa !212
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %55, metadata !118, metadata !DIExpression()), !dbg !148
  %57 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %55, i64 0, i32 14, !dbg !213
  store i32 %54, i32* %57, align 8, !dbg !214, !tbaa !215
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %55, metadata !118, metadata !DIExpression()), !dbg !148
  %58 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %55, i64 0, i32 15, !dbg !216
  store i32 -1, i32* %58, align 4, !dbg !217, !tbaa !218
  %59 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 21, !dbg !219
  store double 0.000000e+00, double* %59, align 8, !dbg !220, !tbaa !221
  br i1 %53, label %60, label %118, !dbg !222

; <label>:60:                                     ; preds = %50
  %61 = mul nsw i32 %0, 5, !dbg !223
  %62 = sext i32 %61 to i64, !dbg !226
  %63 = tail call i8* @klu_malloc(i64 %62, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !227
  %64 = load i32, i32* %43, align 4, !dbg !228, !tbaa !87
  %65 = icmp slt i32 %64, 0, !dbg !230
  br i1 %65, label %66, label %70, !dbg !231

; <label>:66:                                     ; preds = %60
  %67 = tail call i8* @klu_free(i8* %39, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !232
  %68 = tail call i8* @klu_free(i8* %41, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !234
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %6, metadata !118, metadata !DIExpression()), !dbg !148
  %69 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %6, %struct.klu_common_struct* nonnull %3) #4, !dbg !235
  br label %170, !dbg !236

; <label>:70:                                     ; preds = %60
  %71 = bitcast i8* %63 to i32*, !dbg !227
  call void @llvm.dbg.value(metadata i32* %71, metadata !140, metadata !DIExpression()), !dbg !237
  %72 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 4, !dbg !238
  %73 = load double, double* %72, align 8, !dbg !238, !tbaa !239
  %74 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !240, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %74, metadata !118, metadata !DIExpression()), !dbg !148
  %75 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %74, i64 0, i32 15, !dbg !241
  call void @llvm.dbg.value(metadata double* %5, metadata !117, metadata !DIExpression()), !dbg !242
  %76 = call i32 @btf_order(i32 %0, i32* %1, i32* %2, double %73, double* nonnull %5, i32* %40, i32* %42, i32* %17, i32* nonnull %75, i32* %71) #4, !dbg !243
  call void @llvm.dbg.value(metadata i32 %76, metadata !129, metadata !DIExpression()), !dbg !244
  %77 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !245, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %77, metadata !118, metadata !DIExpression()), !dbg !148
  %78 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %77, i64 0, i32 15, !dbg !246
  %79 = load i32, i32* %78, align 4, !dbg !246, !tbaa !218
  %80 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 13, !dbg !247
  store i32 %79, i32* %80, align 4, !dbg !248, !tbaa !97
  %81 = load double, double* %5, align 8, !dbg !249, !tbaa !250
  call void @llvm.dbg.value(metadata double %81, metadata !117, metadata !DIExpression()), !dbg !242
  %82 = load double, double* %59, align 8, !dbg !251, !tbaa !221
  %83 = fadd double %81, %82, !dbg !251
  store double %83, double* %59, align 8, !dbg !251, !tbaa !221
  %84 = call i8* @klu_free(i8* %63, i64 %62, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !252
  %85 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !253, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %85, metadata !118, metadata !DIExpression()), !dbg !148
  %86 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %85, i64 0, i32 15, !dbg !255
  %87 = load i32, i32* %86, align 4, !dbg !255, !tbaa !218
  %88 = icmp slt i32 %87, %0, !dbg !256
  %89 = icmp sgt i32 %0, 0, !dbg !257
  %90 = and i1 %88, %89, !dbg !261
  call void @llvm.dbg.value(metadata i32 0, metadata !138, metadata !DIExpression()), !dbg !262
  br i1 %90, label %91, label %102, !dbg !261

; <label>:91:                                     ; preds = %70
  %92 = zext i32 %0 to i64
  br label %93, !dbg !263

; <label>:93:                                     ; preds = %93, %91
  %94 = phi i64 [ 0, %91 ], [ %100, %93 ]
  call void @llvm.dbg.value(metadata i64 %94, metadata !138, metadata !DIExpression()), !dbg !262
  %95 = getelementptr inbounds i32, i32* %42, i64 %94, !dbg !264
  %96 = load i32, i32* %95, align 4, !dbg !264, !tbaa !266
  %97 = icmp slt i32 %96, -1, !dbg !264
  %98 = sub i32 -2, %96, !dbg !264
  %99 = select i1 %97, i32 %98, i32 %96, !dbg !264
  store i32 %99, i32* %95, align 4, !dbg !267, !tbaa !266
  %100 = add nuw nsw i64 %94, 1, !dbg !268
  call void @llvm.dbg.value(metadata i32 undef, metadata !138, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !262
  %101 = icmp eq i64 %100, %92, !dbg !257
  br i1 %101, label %102, label %93, !dbg !263, !llvm.loop !269

; <label>:102:                                    ; preds = %93, %70
  call void @llvm.dbg.value(metadata i32 1, metadata !132, metadata !DIExpression()), !dbg !271
  call void @llvm.dbg.value(metadata i32 0, metadata !131, metadata !DIExpression()), !dbg !272
  %103 = icmp sgt i32 %76, 0, !dbg !273
  br i1 %103, label %104, label %131, !dbg !276

; <label>:104:                                    ; preds = %102
  %105 = zext i32 %76 to i64
  br label %106, !dbg !276

; <label>:106:                                    ; preds = %106, %104
  %107 = phi i64 [ 0, %104 ], [ %111, %106 ]
  %108 = phi i32 [ 1, %104 ], [ %116, %106 ]
  call void @llvm.dbg.value(metadata i64 %107, metadata !131, metadata !DIExpression()), !dbg !272
  call void @llvm.dbg.value(metadata i32 %108, metadata !132, metadata !DIExpression()), !dbg !271
  %109 = getelementptr inbounds i32, i32* %17, i64 %107, !dbg !277
  %110 = load i32, i32* %109, align 4, !dbg !277, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %110, metadata !133, metadata !DIExpression()), !dbg !279
  %111 = add nuw nsw i64 %107, 1, !dbg !280
  %112 = getelementptr inbounds i32, i32* %17, i64 %111, !dbg !281
  %113 = load i32, i32* %112, align 4, !dbg !281, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %113, metadata !134, metadata !DIExpression()), !dbg !282
  %114 = sub nsw i32 %113, %110, !dbg !283
  call void @llvm.dbg.value(metadata i32 %114, metadata !135, metadata !DIExpression()), !dbg !284
  %115 = icmp sgt i32 %108, %114, !dbg !285
  %116 = select i1 %115, i32 %108, i32 %114, !dbg !285
  call void @llvm.dbg.value(metadata i32 undef, metadata !131, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !272
  call void @llvm.dbg.value(metadata i32 %116, metadata !132, metadata !DIExpression()), !dbg !271
  %117 = icmp eq i64 %111, %105, !dbg !273
  br i1 %117, label %131, label %106, !dbg !276, !llvm.loop !286

; <label>:118:                                    ; preds = %50
  call void @llvm.dbg.value(metadata i32 1, metadata !129, metadata !DIExpression()), !dbg !244
  call void @llvm.dbg.value(metadata i32 %0, metadata !132, metadata !DIExpression()), !dbg !271
  store i32 0, i32* %17, align 4, !dbg !288, !tbaa !266
  %119 = getelementptr inbounds i32, i32* %17, i64 1, !dbg !290
  store i32 %0, i32* %119, align 4, !dbg !291, !tbaa !266
  call void @llvm.dbg.value(metadata i32 0, metadata !138, metadata !DIExpression()), !dbg !262
  %120 = icmp sgt i32 %0, 0, !dbg !292
  br i1 %120, label %121, label %131, !dbg !295

; <label>:121:                                    ; preds = %118
  %122 = zext i32 %0 to i64
  br label %123, !dbg !295

; <label>:123:                                    ; preds = %123, %121
  %124 = phi i64 [ 0, %121 ], [ %129, %123 ]
  call void @llvm.dbg.value(metadata i64 %124, metadata !138, metadata !DIExpression()), !dbg !262
  %125 = getelementptr inbounds i32, i32* %40, i64 %124, !dbg !296
  %126 = trunc i64 %124 to i32, !dbg !298
  store i32 %126, i32* %125, align 4, !dbg !298, !tbaa !266
  %127 = getelementptr inbounds i32, i32* %42, i64 %124, !dbg !299
  %128 = trunc i64 %124 to i32, !dbg !300
  store i32 %128, i32* %127, align 4, !dbg !300, !tbaa !266
  %129 = add nuw nsw i64 %124, 1, !dbg !301
  call void @llvm.dbg.value(metadata i32 undef, metadata !138, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !262
  %130 = icmp eq i64 %129, %122, !dbg !292
  br i1 %130, label %131, label %123, !dbg !295, !llvm.loop !302

; <label>:131:                                    ; preds = %123, %106, %118, %102
  %132 = phi i32 [ 1, %102 ], [ %0, %118 ], [ %116, %106 ], [ %0, %123 ], !dbg !304
  %133 = phi i32 [ %76, %102 ], [ 1, %118 ], [ %76, %106 ], [ 1, %123 ], !dbg !304
  call void @llvm.dbg.value(metadata i32 %133, metadata !129, metadata !DIExpression()), !dbg !244
  call void @llvm.dbg.value(metadata i32 %132, metadata !132, metadata !DIExpression()), !dbg !271
  %134 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !305, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %134, metadata !118, metadata !DIExpression()), !dbg !148
  %135 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %134, i64 0, i32 11, !dbg !306
  store i32 %133, i32* %135, align 4, !dbg !307, !tbaa !308
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %134, metadata !118, metadata !DIExpression()), !dbg !148
  %136 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %134, i64 0, i32 12, !dbg !309
  store i32 %132, i32* %136, align 8, !dbg !310, !tbaa !311
  %137 = sext i32 %132 to i64, !dbg !312
  %138 = call i8* @klu_malloc(i64 %137, i64 4, %struct.klu_common_struct* %3) #4, !dbg !313
  %139 = add nsw i32 %132, 1, !dbg !314
  %140 = sext i32 %139 to i64, !dbg !315
  %141 = call i8* @klu_malloc(i64 %140, i64 4, %struct.klu_common_struct* %3) #4, !dbg !316
  %142 = add nsw i32 %21, 1, !dbg !317
  %143 = icmp sgt i32 %37, %142, !dbg !317
  %144 = select i1 %143, i32 %37, i32 %142, !dbg !317
  %145 = sext i32 %144 to i64, !dbg !317
  %146 = call i8* @klu_malloc(i64 %145, i64 4, %struct.klu_common_struct* %3) #4, !dbg !318
  %147 = call i8* @klu_malloc(i64 %38, i64 4, %struct.klu_common_struct* %3) #4, !dbg !319
  %148 = load i32, i32* %43, align 4, !dbg !320, !tbaa !87
  %149 = icmp eq i32 %148, 0, !dbg !322
  br i1 %149, label %150, label %157, !dbg !323

; <label>:150:                                    ; preds = %131
  %151 = bitcast i8* %147 to i32*, !dbg !319
  call void @llvm.dbg.value(metadata i32* %151, metadata !123, metadata !DIExpression()), !dbg !324
  %152 = bitcast i8* %146 to i32*, !dbg !318
  call void @llvm.dbg.value(metadata i32* %152, metadata !122, metadata !DIExpression()), !dbg !325
  %153 = bitcast i8* %141 to i32*, !dbg !316
  call void @llvm.dbg.value(metadata i32* %153, metadata !121, metadata !DIExpression()), !dbg !326
  %154 = bitcast i8* %138 to i32*, !dbg !313
  call void @llvm.dbg.value(metadata i32* %154, metadata !124, metadata !DIExpression()), !dbg !327
  %155 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !328, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %155, metadata !118, metadata !DIExpression()), !dbg !148
  %156 = call fastcc i32 @analyze_worker(i32 %0, i32* %1, i32* %2, i32 %133, i32* %40, i32* %42, i32* %17, i32 %23, i32* %13, i32* %15, double* %19, i32* %154, i32* %153, i32* %152, i32 %37, i32* %151, %struct.klu_symbolic* %155, %struct.klu_common_struct* nonnull %3), !dbg !330
  store i32 %156, i32* %43, align 4, !dbg !331, !tbaa !87
  br label %157, !dbg !332

; <label>:157:                                    ; preds = %150, %131
  %158 = call i8* @klu_free(i8* %138, i64 %137, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !333
  %159 = call i8* @klu_free(i8* %141, i64 %140, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !334
  %160 = call i8* @klu_free(i8* %146, i64 %145, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !335
  %161 = call i8* @klu_free(i8* %147, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !336
  %162 = call i8* @klu_free(i8* %39, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !337
  %163 = call i8* @klu_free(i8* %41, i64 %38, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !338
  %164 = load i32, i32* %43, align 4, !dbg !339, !tbaa !87
  %165 = icmp slt i32 %164, 0, !dbg !341
  br i1 %165, label %166, label %168, !dbg !342

; <label>:166:                                    ; preds = %157
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %6, metadata !118, metadata !DIExpression()), !dbg !148
  %167 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %6, %struct.klu_common_struct* nonnull %3) #4, !dbg !343
  br label %168, !dbg !345

; <label>:168:                                    ; preds = %166, %157
  %169 = load %struct.klu_symbolic*, %struct.klu_symbolic** %6, align 8, !dbg !346, !tbaa !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %169, metadata !118, metadata !DIExpression()), !dbg !148
  br label %170, !dbg !347

; <label>:170:                                    ; preds = %4, %168, %66, %46, %33
  %171 = phi %struct.klu_symbolic* [ null, %46 ], [ null, %66 ], [ %169, %168 ], [ null, %33 ], [ null, %4 ], !dbg !348
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %8) #4, !dbg !349
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %7) #4, !dbg !349
  ret %struct.klu_symbolic* %171, !dbg !349
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #3

declare %struct.klu_symbolic* @klu_alloc_symbolic(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #2

declare i64 @colamd_recommended(i32, i32, i32) local_unnamed_addr #2

declare i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i32 @btf_order(i32, i32*, i32*, double, double*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define internal fastcc i32 @analyze_worker(i32, i32* nocapture readonly, i32* nocapture readonly, i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32, i32* nocapture, i32* nocapture, double* nocapture, i32*, i32*, i32*, i32, i32* nocapture, %struct.klu_symbolic* nocapture, %struct.klu_common_struct*) unnamed_addr #0 !dbg !350 {
  %19 = alloca [20 x double], align 16
  %20 = alloca [20 x i32], align 16
  call void @llvm.dbg.value(metadata i32 %0, metadata !354, metadata !DIExpression()), !dbg !398
  call void @llvm.dbg.value(metadata i32* %1, metadata !355, metadata !DIExpression()), !dbg !399
  call void @llvm.dbg.value(metadata i32* %2, metadata !356, metadata !DIExpression()), !dbg !400
  call void @llvm.dbg.value(metadata i32 %3, metadata !357, metadata !DIExpression()), !dbg !401
  call void @llvm.dbg.value(metadata i32* %4, metadata !358, metadata !DIExpression()), !dbg !402
  call void @llvm.dbg.value(metadata i32* %5, metadata !359, metadata !DIExpression()), !dbg !403
  call void @llvm.dbg.value(metadata i32* %6, metadata !360, metadata !DIExpression()), !dbg !404
  call void @llvm.dbg.value(metadata i32 %7, metadata !361, metadata !DIExpression()), !dbg !405
  call void @llvm.dbg.value(metadata i32* %8, metadata !362, metadata !DIExpression()), !dbg !406
  call void @llvm.dbg.value(metadata i32* %9, metadata !363, metadata !DIExpression()), !dbg !407
  call void @llvm.dbg.value(metadata double* %10, metadata !364, metadata !DIExpression()), !dbg !408
  call void @llvm.dbg.value(metadata i32* %11, metadata !365, metadata !DIExpression()), !dbg !409
  call void @llvm.dbg.value(metadata i32* %12, metadata !366, metadata !DIExpression()), !dbg !410
  call void @llvm.dbg.value(metadata i32* %13, metadata !367, metadata !DIExpression()), !dbg !411
  call void @llvm.dbg.value(metadata i32 %14, metadata !368, metadata !DIExpression()), !dbg !412
  call void @llvm.dbg.value(metadata i32* %15, metadata !369, metadata !DIExpression()), !dbg !413
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %16, metadata !370, metadata !DIExpression()), !dbg !414
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %17, metadata !371, metadata !DIExpression()), !dbg !415
  %21 = bitcast [20 x double]* %19 to i8*, !dbg !416
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %21) #4, !dbg !416
  call void @llvm.dbg.declare(metadata [20 x double]* %19, metadata !372, metadata !DIExpression()), !dbg !417
  %22 = bitcast [20 x i32]* %20 to i8*, !dbg !418
  call void @llvm.lifetime.start.p0i8(i64 80, i8* nonnull %22) #4, !dbg !418
  call void @llvm.dbg.declare(metadata [20 x i32]* %20, metadata !394, metadata !DIExpression()), !dbg !419
  call void @llvm.dbg.value(metadata i32 -3, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !421
  %23 = icmp sgt i32 %0, 0, !dbg !422
  br i1 %23, label %24, label %35, !dbg !425

; <label>:24:                                     ; preds = %18
  %25 = zext i32 %0 to i64
  br label %26, !dbg !425

; <label>:26:                                     ; preds = %26, %24
  %27 = phi i64 [ 0, %24 ], [ %33, %26 ]
  call void @llvm.dbg.value(metadata i64 %27, metadata !383, metadata !DIExpression()), !dbg !421
  %28 = getelementptr inbounds i32, i32* %4, i64 %27, !dbg !426
  %29 = load i32, i32* %28, align 4, !dbg !426, !tbaa !266
  %30 = sext i32 %29 to i64, !dbg !428
  %31 = getelementptr inbounds i32, i32* %15, i64 %30, !dbg !428
  %32 = trunc i64 %27 to i32, !dbg !429
  store i32 %32, i32* %31, align 4, !dbg !429, !tbaa !266
  %33 = add nuw nsw i64 %27, 1, !dbg !430
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %34 = icmp eq i64 %33, %25, !dbg !422
  br i1 %34, label %35, label %26, !dbg !425, !llvm.loop !431

; <label>:35:                                     ; preds = %26, %18
  call void @llvm.dbg.value(metadata i32 0, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !376, metadata !DIExpression()), !dbg !434
  call void @llvm.dbg.value(metadata i32 0, metadata !392, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !378, metadata !DIExpression()), !dbg !436
  %36 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %16, i64 0, i32 0, !dbg !437
  store double -1.000000e+00, double* %36, align 8, !dbg !438, !tbaa !439
  call void @llvm.dbg.value(metadata i32 0, metadata !384, metadata !DIExpression()), !dbg !440
  call void @llvm.dbg.value(metadata i32 -3, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 0, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 0, metadata !392, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !376, metadata !DIExpression()), !dbg !434
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !378, metadata !DIExpression()), !dbg !436
  %37 = icmp sgt i32 %3, 0, !dbg !441
  br i1 %37, label %38, label %246, !dbg !444

; <label>:38:                                     ; preds = %35
  %39 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %17, i64 0, i32 8
  %40 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 0
  %41 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %17, i64 0, i32 23
  %42 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %17, i64 0, i32 22
  %43 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 7
  %44 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 9
  %45 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 12
  %46 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 10
  %47 = getelementptr inbounds [20 x i32], [20 x i32]* %20, i64 0, i64 0
  %48 = getelementptr inbounds [20 x double], [20 x double]* %19, i64 0, i64 3
  %49 = bitcast double* %48 to i64*
  %50 = bitcast %struct.klu_symbolic* %16 to i64*
  %51 = sext i32 %3 to i64, !dbg !444
  br label %52, !dbg !444

; <label>:52:                                     ; preds = %38, %244
  %53 = phi i64 [ 0, %38 ], [ %61, %244 ]
  %54 = phi i32 [ -3, %38 ], [ %198, %244 ]
  %55 = phi i32 [ 0, %38 ], [ %119, %244 ]
  %56 = phi i32 [ 0, %38 ], [ %123, %244 ]
  %57 = phi double [ 0.000000e+00, %38 ], [ %205, %244 ]
  %58 = phi double [ 0.000000e+00, %38 ], [ %210, %244 ]
  call void @llvm.dbg.value(metadata i32 %54, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 %55, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 %56, metadata !392, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata double %57, metadata !376, metadata !DIExpression()), !dbg !434
  call void @llvm.dbg.value(metadata double %58, metadata !378, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i64 %53, metadata !384, metadata !DIExpression()), !dbg !440
  %59 = getelementptr inbounds i32, i32* %6, i64 %53, !dbg !445
  %60 = load i32, i32* %59, align 4, !dbg !445, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %60, metadata !380, metadata !DIExpression()), !dbg !447
  %61 = add nuw nsw i64 %53, 1, !dbg !448
  %62 = getelementptr inbounds i32, i32* %6, i64 %61, !dbg !449
  %63 = load i32, i32* %62, align 4, !dbg !449, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %63, metadata !381, metadata !DIExpression()), !dbg !450
  %64 = sub nsw i32 %63, %60, !dbg !451
  call void @llvm.dbg.value(metadata i32 %64, metadata !382, metadata !DIExpression()), !dbg !452
  %65 = getelementptr inbounds double, double* %10, i64 %53, !dbg !453
  store double -1.000000e+00, double* %65, align 8, !dbg !454, !tbaa !250
  call void @llvm.dbg.value(metadata i32 0, metadata !389, metadata !DIExpression()), !dbg !455
  call void @llvm.dbg.value(metadata i32 %60, metadata !383, metadata !DIExpression()), !dbg !421
  call void @llvm.dbg.value(metadata i32 %55, metadata !393, metadata !DIExpression()), !dbg !433
  %66 = icmp sgt i32 %63, %60, !dbg !456
  br i1 %66, label %67, label %117, !dbg !459

; <label>:67:                                     ; preds = %52
  %68 = sext i32 %60 to i64, !dbg !459
  %69 = sext i32 %60 to i64, !dbg !459
  %70 = sext i32 %63 to i64
  br label %71, !dbg !459

; <label>:71:                                     ; preds = %112, %67
  %72 = phi i64 [ %68, %67 ], [ %115, %112 ]
  %73 = phi i32 [ %55, %67 ], [ %114, %112 ]
  %74 = phi i32 [ 0, %67 ], [ %113, %112 ]
  call void @llvm.dbg.value(metadata i32 %73, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 %74, metadata !389, metadata !DIExpression()), !dbg !455
  call void @llvm.dbg.value(metadata i64 %72, metadata !383, metadata !DIExpression()), !dbg !421
  %75 = sub nsw i64 %72, %69, !dbg !460
  %76 = getelementptr inbounds i32, i32* %12, i64 %75, !dbg !462
  store i32 %74, i32* %76, align 4, !dbg !463, !tbaa !266
  %77 = getelementptr inbounds i32, i32* %5, i64 %72, !dbg !464
  %78 = load i32, i32* %77, align 4, !dbg !464, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %78, metadata !385, metadata !DIExpression()), !dbg !465
  %79 = add nsw i32 %78, 1, !dbg !466
  %80 = sext i32 %79 to i64, !dbg !467
  %81 = getelementptr inbounds i32, i32* %1, i64 %80, !dbg !467
  %82 = load i32, i32* %81, align 4, !dbg !467, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %82, metadata !386, metadata !DIExpression()), !dbg !468
  %83 = sext i32 %78 to i64, !dbg !469
  %84 = getelementptr inbounds i32, i32* %1, i64 %83, !dbg !469
  %85 = load i32, i32* %84, align 4, !dbg !469, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %85, metadata !390, metadata !DIExpression()), !dbg !471
  call void @llvm.dbg.value(metadata i32 %73, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 %74, metadata !389, metadata !DIExpression()), !dbg !455
  %86 = icmp slt i32 %85, %82, !dbg !472
  br i1 %86, label %87, label %112, !dbg !474

; <label>:87:                                     ; preds = %71
  %88 = sext i32 %85 to i64, !dbg !474
  %89 = sext i32 %82 to i64
  br label %90, !dbg !474

; <label>:90:                                     ; preds = %107, %87
  %91 = phi i64 [ %88, %87 ], [ %110, %107 ]
  %92 = phi i32 [ %73, %87 ], [ %109, %107 ]
  %93 = phi i32 [ %74, %87 ], [ %108, %107 ]
  call void @llvm.dbg.value(metadata i32 %92, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i64 %91, metadata !390, metadata !DIExpression()), !dbg !471
  call void @llvm.dbg.value(metadata i32 %93, metadata !389, metadata !DIExpression()), !dbg !455
  %94 = getelementptr inbounds i32, i32* %2, i64 %91, !dbg !475
  %95 = load i32, i32* %94, align 4, !dbg !475, !tbaa !266
  %96 = sext i32 %95 to i64, !dbg !477
  %97 = getelementptr inbounds i32, i32* %15, i64 %96, !dbg !477
  %98 = load i32, i32* %97, align 4, !dbg !477, !tbaa !266
  call void @llvm.dbg.value(metadata i32 %98, metadata !391, metadata !DIExpression()), !dbg !478
  %99 = icmp slt i32 %98, %60, !dbg !479
  br i1 %99, label %100, label %102, !dbg !481

; <label>:100:                                    ; preds = %90
  %101 = add nsw i32 %92, 1, !dbg !482
  call void @llvm.dbg.value(metadata i32 %101, metadata !393, metadata !DIExpression()), !dbg !433
  br label %107, !dbg !484

; <label>:102:                                    ; preds = %90
  %103 = sub nsw i32 %98, %60, !dbg !485
  call void @llvm.dbg.value(metadata i32 %103, metadata !391, metadata !DIExpression()), !dbg !478
  %104 = add nsw i32 %93, 1, !dbg !487
  call void @llvm.dbg.value(metadata i32 %104, metadata !389, metadata !DIExpression()), !dbg !455
  %105 = sext i32 %93 to i64, !dbg !488
  %106 = getelementptr inbounds i32, i32* %13, i64 %105, !dbg !488
  store i32 %103, i32* %106, align 4, !dbg !489, !tbaa !266
  br label %107

; <label>:107:                                    ; preds = %100, %102
  %108 = phi i32 [ %93, %100 ], [ %104, %102 ], !dbg !490
  %109 = phi i32 [ %101, %100 ], [ %92, %102 ], !dbg !491
  %110 = add nsw i64 %91, 1, !dbg !492
  call void @llvm.dbg.value(metadata i32 %109, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 undef, metadata !390, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !471
  call void @llvm.dbg.value(metadata i32 %108, metadata !389, metadata !DIExpression()), !dbg !455
  %111 = icmp eq i64 %110, %89, !dbg !472
  br i1 %111, label %112, label %90, !dbg !474, !llvm.loop !493

; <label>:112:                                    ; preds = %107, %71
  %113 = phi i32 [ %74, %71 ], [ %108, %107 ]
  %114 = phi i32 [ %73, %71 ], [ %109, %107 ]
  %115 = add nsw i64 %72, 1, !dbg !495
  call void @llvm.dbg.value(metadata i32 %114, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 %113, metadata !389, metadata !DIExpression()), !dbg !455
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %116 = icmp eq i64 %115, %70, !dbg !456
  br i1 %116, label %117, label %71, !dbg !459, !llvm.loop !496

; <label>:117:                                    ; preds = %112, %52
  %118 = phi i32 [ 0, %52 ], [ %113, %112 ]
  %119 = phi i32 [ %55, %52 ], [ %114, %112 ]
  call void @llvm.dbg.value(metadata i32 %118, metadata !389, metadata !DIExpression()), !dbg !455
  %120 = sext i32 %64 to i64, !dbg !498
  %121 = getelementptr inbounds i32, i32* %12, i64 %120, !dbg !498
  store i32 %118, i32* %121, align 4, !dbg !499, !tbaa !266
  %122 = icmp sgt i32 %56, %118, !dbg !500
  %123 = select i1 %122, i32 %56, i32 %118, !dbg !500
  %124 = icmp slt i32 %64, 4, !dbg !501
  br i1 %124, label %125, label %149, !dbg !503

; <label>:125:                                    ; preds = %117
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !421
  %126 = icmp sgt i32 %64, 0, !dbg !504
  br i1 %126, label %127, label %135, !dbg !508

; <label>:127:                                    ; preds = %125
  %128 = zext i32 %64 to i64
  br label %129, !dbg !508

; <label>:129:                                    ; preds = %129, %127
  %130 = phi i64 [ 0, %127 ], [ %133, %129 ]
  call void @llvm.dbg.value(metadata i64 %130, metadata !383, metadata !DIExpression()), !dbg !421
  %131 = getelementptr inbounds i32, i32* %11, i64 %130, !dbg !509
  %132 = trunc i64 %130 to i32, !dbg !511
  store i32 %132, i32* %131, align 4, !dbg !511, !tbaa !266
  %133 = add nuw nsw i64 %130, 1, !dbg !512
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %134 = icmp eq i64 %133, %128, !dbg !504
  br i1 %134, label %135, label %129, !dbg !508, !llvm.loop !513

; <label>:135:                                    ; preds = %129, %125
  %136 = add nsw i32 %64, 1, !dbg !515
  %137 = mul nsw i32 %136, %64, !dbg !516
  %138 = sdiv i32 %137, 2, !dbg !517
  %139 = sitofp i32 %138 to double, !dbg !518
  call void @llvm.dbg.value(metadata double %139, metadata !377, metadata !DIExpression()), !dbg !519
  %140 = add nsw i32 %64, -1, !dbg !520
  %141 = mul nsw i32 %140, %64, !dbg !521
  %142 = sdiv i32 %141, 2, !dbg !522
  %143 = shl i32 %64, 1, !dbg !523
  %144 = add nsw i32 %143, -1, !dbg !524
  %145 = mul nsw i32 %141, %144, !dbg !525
  %146 = sdiv i32 %145, 6, !dbg !526
  %147 = add nsw i32 %146, %142, !dbg !527
  %148 = sitofp i32 %147 to double, !dbg !528
  call void @llvm.dbg.value(metadata double %148, metadata !379, metadata !DIExpression()), !dbg !529
  call void @llvm.dbg.value(metadata i32 1, metadata !396, metadata !DIExpression()), !dbg !530
  call void @llvm.dbg.value(metadata i32 %195, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 %194, metadata !396, metadata !DIExpression()), !dbg !530
  call void @llvm.dbg.value(metadata double %193, metadata !377, metadata !DIExpression()), !dbg !519
  call void @llvm.dbg.value(metadata double %192, metadata !379, metadata !DIExpression()), !dbg !529
  br label %197, !dbg !531

; <label>:149:                                    ; preds = %117
  switch i32 %7, label %185 [
    i32 0, label %150
    i32 1, label %175
  ], !dbg !532

; <label>:150:                                    ; preds = %149
  %151 = call i32 @amd_order(i32 %64, i32* nonnull %12, i32* %13, i32* %11, double* null, double* nonnull %40) #4, !dbg !533
  call void @llvm.dbg.value(metadata i32 %151, metadata !388, metadata !DIExpression()), !dbg !536
  %152 = lshr i32 %151, 31, !dbg !537
  %153 = xor i32 %152, 1, !dbg !537
  call void @llvm.dbg.value(metadata i32 %153, metadata !396, metadata !DIExpression()), !dbg !530
  %154 = icmp eq i32 %151, -1, !dbg !538
  %155 = select i1 %154, i32 -2, i32 %54, !dbg !540
  call void @llvm.dbg.value(metadata i32 %155, metadata !397, metadata !DIExpression()), !dbg !420
  %156 = load i64, i64* %41, align 8, !dbg !541, !tbaa !542
  %157 = uitofp i64 %156 to double, !dbg !541
  %158 = load i64, i64* %42, align 8, !dbg !541, !tbaa !543
  %159 = uitofp i64 %158 to double, !dbg !541
  %160 = load double, double* %43, align 8, !dbg !541, !tbaa !250
  %161 = fadd double %160, %159, !dbg !541
  %162 = fcmp olt double %161, %157, !dbg !541
  %163 = select i1 %162, double %157, double %161, !dbg !541
  %164 = fptoui double %163 to i64, !dbg !541
  store i64 %164, i64* %41, align 8, !dbg !544, !tbaa !542
  %165 = load double, double* %44, align 8, !dbg !545, !tbaa !250
  %166 = fptosi double %165 to i32, !dbg !546
  %167 = add nsw i32 %64, %166, !dbg !547
  %168 = sitofp i32 %167 to double, !dbg !546
  call void @llvm.dbg.value(metadata double %168, metadata !377, metadata !DIExpression()), !dbg !519
  %169 = load double, double* %45, align 16, !dbg !548, !tbaa !250
  %170 = fmul double %169, 2.000000e+00, !dbg !549
  %171 = load double, double* %46, align 16, !dbg !550, !tbaa !250
  %172 = fadd double %170, %171, !dbg !551
  call void @llvm.dbg.value(metadata double %172, metadata !379, metadata !DIExpression()), !dbg !529
  br i1 %122, label %191, label %173, !dbg !552

; <label>:173:                                    ; preds = %150
  %174 = load i64, i64* %49, align 8, !dbg !553, !tbaa !250
  store i64 %174, i64* %50, align 8, !dbg !556, !tbaa !439
  br label %191, !dbg !557

; <label>:175:                                    ; preds = %149
  %176 = call i32 @colamd(i32 %64, i32 %64, i32 %14, i32* %13, i32* nonnull %12, double* null, i32* nonnull %47) #4, !dbg !558
  call void @llvm.dbg.value(metadata i32 %176, metadata !396, metadata !DIExpression()), !dbg !530
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !377, metadata !DIExpression()), !dbg !519
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !379, metadata !DIExpression()), !dbg !529
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !421
  %177 = zext i32 %64 to i64
  br label %178, !dbg !561

; <label>:178:                                    ; preds = %178, %175
  %179 = phi i64 [ 0, %175 ], [ %183, %178 ]
  call void @llvm.dbg.value(metadata i64 %179, metadata !383, metadata !DIExpression()), !dbg !421
  %180 = getelementptr inbounds i32, i32* %12, i64 %179, !dbg !563
  %181 = load i32, i32* %180, align 4, !dbg !563, !tbaa !266
  %182 = getelementptr inbounds i32, i32* %11, i64 %179, !dbg !566
  store i32 %181, i32* %182, align 4, !dbg !567, !tbaa !266
  %183 = add nuw nsw i64 %179, 1, !dbg !568
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %184 = icmp eq i64 %183, %177, !dbg !569
  br i1 %184, label %191, label %178, !dbg !561, !llvm.loop !570

; <label>:185:                                    ; preds = %149
  %186 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %39, align 8, !dbg !572, !tbaa !180
  %187 = call i32 %186(i32 %64, i32* nonnull %12, i32* %13, i32* %11, %struct.klu_common_struct* %17) #4, !dbg !572
  %188 = sitofp i32 %187 to double, !dbg !572
  call void @llvm.dbg.value(metadata double %188, metadata !377, metadata !DIExpression()), !dbg !519
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !379, metadata !DIExpression()), !dbg !529
  %189 = icmp ne i32 %187, 0, !dbg !574
  %190 = zext i1 %189 to i32, !dbg !574
  call void @llvm.dbg.value(metadata i32 %190, metadata !396, metadata !DIExpression()), !dbg !530
  br label %191

; <label>:191:                                    ; preds = %178, %150, %173, %185
  %192 = phi double [ %172, %173 ], [ %172, %150 ], [ -1.000000e+00, %185 ], [ -1.000000e+00, %178 ], !dbg !575
  %193 = phi double [ %168, %173 ], [ %168, %150 ], [ %188, %185 ], [ -1.000000e+00, %178 ], !dbg !575
  %194 = phi i32 [ %153, %173 ], [ %153, %150 ], [ %190, %185 ], [ %176, %178 ], !dbg !575
  %195 = phi i32 [ %155, %173 ], [ %155, %150 ], [ %54, %185 ], [ %54, %178 ], !dbg !491
  call void @llvm.dbg.value(metadata i32 %195, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 %194, metadata !396, metadata !DIExpression()), !dbg !530
  call void @llvm.dbg.value(metadata double %193, metadata !377, metadata !DIExpression()), !dbg !519
  call void @llvm.dbg.value(metadata double %192, metadata !379, metadata !DIExpression()), !dbg !529
  %196 = icmp eq i32 %194, 0, !dbg !576
  br i1 %196, label %254, label %197, !dbg !531

; <label>:197:                                    ; preds = %135, %191
  %198 = phi i32 [ %54, %135 ], [ %195, %191 ]
  %199 = phi double [ %139, %135 ], [ %193, %191 ]
  %200 = phi double [ %148, %135 ], [ %192, %191 ]
  store double %199, double* %65, align 8, !dbg !578, !tbaa !250
  %201 = fcmp oeq double %57, -1.000000e+00, !dbg !579
  %202 = fcmp oeq double %199, -1.000000e+00, !dbg !580
  %203 = or i1 %201, %202, !dbg !581
  %204 = fadd double %57, %199, !dbg !582
  %205 = select i1 %203, double -1.000000e+00, double %204, !dbg !581
  %206 = fcmp oeq double %58, -1.000000e+00, !dbg !583
  %207 = fcmp oeq double %200, -1.000000e+00, !dbg !584
  %208 = or i1 %206, %207, !dbg !585
  %209 = fadd double %58, %200, !dbg !586
  %210 = select i1 %208, double -1.000000e+00, double %209, !dbg !585
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !421
  %211 = icmp sgt i32 %64, 0, !dbg !587
  br i1 %211, label %212, label %244, !dbg !590

; <label>:212:                                    ; preds = %197
  %213 = sext i32 %60 to i64, !dbg !590
  %214 = zext i32 %64 to i64
  br label %215, !dbg !590

; <label>:215:                                    ; preds = %215, %212
  %216 = phi i64 [ 0, %212 ], [ %225, %215 ]
  call void @llvm.dbg.value(metadata i64 %216, metadata !383, metadata !DIExpression()), !dbg !421
  %217 = getelementptr inbounds i32, i32* %11, i64 %216, !dbg !591
  %218 = load i32, i32* %217, align 4, !dbg !591, !tbaa !266
  %219 = add nsw i32 %218, %60, !dbg !593
  %220 = sext i32 %219 to i64, !dbg !594
  %221 = getelementptr inbounds i32, i32* %5, i64 %220, !dbg !594
  %222 = load i32, i32* %221, align 4, !dbg !594, !tbaa !266
  %223 = add nsw i64 %216, %213, !dbg !595
  %224 = getelementptr inbounds i32, i32* %9, i64 %223, !dbg !596
  store i32 %222, i32* %224, align 4, !dbg !597, !tbaa !266
  %225 = add nuw nsw i64 %216, 1, !dbg !598
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %226 = icmp eq i64 %225, %214, !dbg !587
  br i1 %226, label %227, label %215, !dbg !590, !llvm.loop !599

; <label>:227:                                    ; preds = %215
  call void @llvm.dbg.value(metadata i32 0, metadata !383, metadata !DIExpression()), !dbg !421
  %228 = icmp sgt i32 %64, 0, !dbg !601
  br i1 %228, label %229, label %244, !dbg !604

; <label>:229:                                    ; preds = %227
  %230 = sext i32 %60 to i64, !dbg !604
  %231 = zext i32 %64 to i64
  br label %232, !dbg !604

; <label>:232:                                    ; preds = %232, %229
  %233 = phi i64 [ 0, %229 ], [ %242, %232 ]
  call void @llvm.dbg.value(metadata i64 %233, metadata !383, metadata !DIExpression()), !dbg !421
  %234 = getelementptr inbounds i32, i32* %11, i64 %233, !dbg !605
  %235 = load i32, i32* %234, align 4, !dbg !605, !tbaa !266
  %236 = add nsw i32 %235, %60, !dbg !607
  %237 = sext i32 %236 to i64, !dbg !608
  %238 = getelementptr inbounds i32, i32* %4, i64 %237, !dbg !608
  %239 = load i32, i32* %238, align 4, !dbg !608, !tbaa !266
  %240 = add nsw i64 %233, %230, !dbg !609
  %241 = getelementptr inbounds i32, i32* %8, i64 %240, !dbg !610
  store i32 %239, i32* %241, align 4, !dbg !611, !tbaa !266
  %242 = add nuw nsw i64 %233, 1, !dbg !612
  call void @llvm.dbg.value(metadata i32 undef, metadata !383, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !421
  %243 = icmp eq i64 %242, %231, !dbg !601
  br i1 %243, label %244, label %232, !dbg !604, !llvm.loop !613

; <label>:244:                                    ; preds = %232, %197, %227
  call void @llvm.dbg.value(metadata i32 %198, metadata !397, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 %119, metadata !393, metadata !DIExpression()), !dbg !433
  call void @llvm.dbg.value(metadata i32 %123, metadata !392, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata double %205, metadata !376, metadata !DIExpression()), !dbg !434
  call void @llvm.dbg.value(metadata double %210, metadata !378, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 undef, metadata !384, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !440
  %245 = icmp slt i64 %61, %51, !dbg !441
  br i1 %245, label %52, label %246, !dbg !444, !llvm.loop !615

; <label>:246:                                    ; preds = %244, %35
  %247 = phi double [ 0.000000e+00, %35 ], [ %210, %244 ]
  %248 = phi double [ 0.000000e+00, %35 ], [ %205, %244 ]
  %249 = phi i32 [ 0, %35 ], [ %119, %244 ]
  call void @llvm.dbg.value(metadata double %247, metadata !378, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata double %248, metadata !376, metadata !DIExpression()), !dbg !434
  call void @llvm.dbg.value(metadata i32 %249, metadata !393, metadata !DIExpression()), !dbg !433
  %250 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %16, i64 0, i32 2, !dbg !617
  store double %248, double* %250, align 8, !dbg !618, !tbaa !619
  %251 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %16, i64 0, i32 3, !dbg !620
  store double %248, double* %251, align 8, !dbg !621, !tbaa !622
  %252 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %16, i64 0, i32 10, !dbg !623
  store i32 %249, i32* %252, align 8, !dbg !624, !tbaa !625
  %253 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %16, i64 0, i32 1, !dbg !626
  store double %247, double* %253, align 8, !dbg !627, !tbaa !628
  br label %254, !dbg !629

; <label>:254:                                    ; preds = %191, %246
  %255 = phi i32 [ 0, %246 ], [ %195, %191 ], !dbg !491
  call void @llvm.lifetime.end.p0i8(i64 80, i8* nonnull %22) #4, !dbg !630
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %21) #4, !dbg !630
  ret i32 %255, !dbg !630
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #3

declare i32 @amd_order(i32, i32*, i32*, i32*, double*, double*) local_unnamed_addr #2

declare i32 @colamd(i32, i32, i32, i32*, i32*, double*, i32*) local_unnamed_addr #2

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind readnone speculatable }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { argmemonly nounwind }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!6, !7, !8, !9}
!llvm.ident = !{!10}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_analyze.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!6 = !{i32 2, !"Dwarf Version", i32 4}
!7 = !{i32 2, !"Debug Info Version", i32 3}
!8 = !{i32 1, !"wchar_size", i32 4}
!9 = !{i32 7, !"PIC Level", i32 2}
!10 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!11 = distinct !DISubprogram(name: "klu_analyze", scope: !1, file: !1, line: 445, type: !12, isLocal: false, isDefinition: true, scopeLine: 455, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !73)
!12 = !DISubroutineType(types: !13)
!13 = !{!14, !5, !29, !29, !38}
!14 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !15, size: 64)
!15 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_symbolic", file: !16, line: 54, baseType: !17)
!16 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!17 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !16, line: 23, size: 768, elements: !18)
!18 = !{!19, !21, !22, !23, !24, !26, !27, !28, !30, !31, !32, !33, !34, !35, !36, !37}
!19 = !DIDerivedType(tag: DW_TAG_member, name: "symmetry", scope: !17, file: !16, line: 31, baseType: !20, size: 64)
!20 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!21 = !DIDerivedType(tag: DW_TAG_member, name: "est_flops", scope: !17, file: !16, line: 32, baseType: !20, size: 64, offset: 64)
!22 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !17, file: !16, line: 33, baseType: !20, size: 64, offset: 128)
!23 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !17, file: !16, line: 33, baseType: !20, size: 64, offset: 192)
!24 = !DIDerivedType(tag: DW_TAG_member, name: "Lnz", scope: !17, file: !16, line: 34, baseType: !25, size: 64, offset: 256)
!25 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !20, size: 64)
!26 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !17, file: !16, line: 38, baseType: !5, size: 32, offset: 320)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "nz", scope: !17, file: !16, line: 39, baseType: !5, size: 32, offset: 352)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "P", scope: !17, file: !16, line: 40, baseType: !29, size: 64, offset: 384)
!29 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !5, size: 64)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "Q", scope: !17, file: !16, line: 41, baseType: !29, size: 64, offset: 448)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "R", scope: !17, file: !16, line: 42, baseType: !29, size: 64, offset: 512)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !17, file: !16, line: 43, baseType: !5, size: 32, offset: 576)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !17, file: !16, line: 44, baseType: !5, size: 32, offset: 608)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "maxblock", scope: !17, file: !16, line: 45, baseType: !5, size: 32, offset: 640)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !17, file: !16, line: 46, baseType: !5, size: 32, offset: 672)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "do_btf", scope: !17, file: !16, line: 47, baseType: !5, size: 32, offset: 704)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !17, file: !16, line: 50, baseType: !5, size: 32, offset: 736)
!38 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !39, size: 64)
!39 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !16, line: 207, baseType: !40)
!40 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !16, line: 137, size: 1280, elements: !41)
!41 = !{!42, !43, !44, !45, !46, !47, !48, !49, !50, !55, !56, !57, !58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68, !72}
!42 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !40, file: !16, line: 144, baseType: !20, size: 64)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !40, file: !16, line: 145, baseType: !20, size: 64, offset: 64)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !40, file: !16, line: 146, baseType: !20, size: 64, offset: 128)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !40, file: !16, line: 147, baseType: !20, size: 64, offset: 192)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !40, file: !16, line: 148, baseType: !20, size: 64, offset: 256)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !40, file: !16, line: 150, baseType: !5, size: 32, offset: 320)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !40, file: !16, line: 151, baseType: !5, size: 32, offset: 352)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !40, file: !16, line: 153, baseType: !5, size: 32, offset: 384)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !40, file: !16, line: 157, baseType: !51, size: 64, offset: 448)
!51 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !52, size: 64)
!52 = !DISubroutineType(types: !53)
!53 = !{!5, !5, !29, !29, !29, !54}
!54 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !40, size: 64)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !40, file: !16, line: 162, baseType: !4, size: 64, offset: 512)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !40, file: !16, line: 164, baseType: !5, size: 32, offset: 576)
!57 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !40, file: !16, line: 177, baseType: !5, size: 32, offset: 608)
!58 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !40, file: !16, line: 178, baseType: !5, size: 32, offset: 640)
!59 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !40, file: !16, line: 180, baseType: !5, size: 32, offset: 672)
!60 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !40, file: !16, line: 185, baseType: !5, size: 32, offset: 704)
!61 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !40, file: !16, line: 191, baseType: !5, size: 32, offset: 736)
!62 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !40, file: !16, line: 196, baseType: !5, size: 32, offset: 768)
!63 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !40, file: !16, line: 198, baseType: !20, size: 64, offset: 832)
!64 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !40, file: !16, line: 199, baseType: !20, size: 64, offset: 896)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !40, file: !16, line: 200, baseType: !20, size: 64, offset: 960)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !40, file: !16, line: 201, baseType: !20, size: 64, offset: 1024)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !40, file: !16, line: 202, baseType: !20, size: 64, offset: 1088)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !40, file: !16, line: 204, baseType: !69, size: 64, offset: 1152)
!69 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !70, line: 62, baseType: !71)
!70 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!71 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!72 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !40, file: !16, line: 205, baseType: !69, size: 64, offset: 1216)
!73 = !{!74, !75, !76, !77}
!74 = !DILocalVariable(name: "n", arg: 1, scope: !11, file: !1, line: 449, type: !5)
!75 = !DILocalVariable(name: "Ap", arg: 2, scope: !11, file: !1, line: 450, type: !29)
!76 = !DILocalVariable(name: "Ai", arg: 3, scope: !11, file: !1, line: 451, type: !29)
!77 = !DILocalVariable(name: "Common", arg: 4, scope: !11, file: !1, line: 453, type: !38)
!78 = !DILocation(line: 449, column: 9, scope: !11)
!79 = !DILocation(line: 450, column: 9, scope: !11)
!80 = !DILocation(line: 451, column: 9, scope: !11)
!81 = !DILocation(line: 453, column: 17, scope: !11)
!82 = !DILocation(line: 461, column: 16, scope: !83)
!83 = distinct !DILexicalBlock(scope: !11, file: !1, line: 461, column: 9)
!84 = !DILocation(line: 461, column: 9, scope: !11)
!85 = !DILocation(line: 465, column: 13, scope: !11)
!86 = !DILocation(line: 465, column: 20, scope: !11)
!87 = !{!88, !92, i64 76}
!88 = !{!"klu_common_struct", !89, i64 0, !89, i64 8, !89, i64 16, !89, i64 24, !89, i64 32, !92, i64 40, !92, i64 44, !92, i64 48, !93, i64 56, !93, i64 64, !92, i64 72, !92, i64 76, !92, i64 80, !92, i64 84, !92, i64 88, !92, i64 92, !92, i64 96, !89, i64 104, !89, i64 112, !89, i64 120, !89, i64 128, !89, i64 136, !94, i64 144, !94, i64 152}
!89 = !{!"double", !90, i64 0}
!90 = !{!"omnipotent char", !91, i64 0}
!91 = !{!"Simple C/C++ TBAA"}
!92 = !{!"int", !90, i64 0}
!93 = !{!"any pointer", !90, i64 0}
!94 = !{!"long", !90, i64 0}
!95 = !DILocation(line: 466, column: 13, scope: !11)
!96 = !DILocation(line: 466, column: 29, scope: !11)
!97 = !{!88, !92, i64 84}
!98 = !DILocation(line: 472, column: 17, scope: !99)
!99 = distinct !DILexicalBlock(scope: !11, file: !1, line: 472, column: 9)
!100 = !{!88, !92, i64 44}
!101 = !DILocation(line: 472, column: 26, scope: !99)
!102 = !DILocation(line: 472, column: 9, scope: !11)
!103 = !DILocation(line: 475, column: 17, scope: !104)
!104 = distinct !DILexicalBlock(scope: !99, file: !1, line: 473, column: 5)
!105 = !DILocation(line: 475, column: 9, scope: !104)
!106 = !DILocation(line: 480, column: 17, scope: !107)
!107 = distinct !DILexicalBlock(scope: !99, file: !1, line: 478, column: 5)
!108 = !DILocation(line: 480, column: 9, scope: !107)
!109 = !DILocation(line: 0, scope: !107)
!110 = !DILocation(line: 482, column: 1, scope: !11)
!111 = distinct !DISubprogram(name: "order_and_analyze", scope: !1, file: !1, line: 257, type: !12, isLocal: true, isDefinition: true, scopeLine: 267, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !112)
!112 = !{!113, !114, !115, !116, !117, !118, !119, !120, !121, !122, !123, !124, !125, !126, !127, !128, !129, !130, !131, !132, !133, !134, !135, !136, !137, !138, !139, !140}
!113 = !DILocalVariable(name: "n", arg: 1, scope: !111, file: !1, line: 261, type: !5)
!114 = !DILocalVariable(name: "Ap", arg: 2, scope: !111, file: !1, line: 262, type: !29)
!115 = !DILocalVariable(name: "Ai", arg: 3, scope: !111, file: !1, line: 263, type: !29)
!116 = !DILocalVariable(name: "Common", arg: 4, scope: !111, file: !1, line: 265, type: !38)
!117 = !DILocalVariable(name: "work", scope: !111, file: !1, line: 268, type: !20)
!118 = !DILocalVariable(name: "Symbolic", scope: !111, file: !1, line: 269, type: !14)
!119 = !DILocalVariable(name: "Lnz", scope: !111, file: !1, line: 270, type: !25)
!120 = !DILocalVariable(name: "Qbtf", scope: !111, file: !1, line: 271, type: !29)
!121 = !DILocalVariable(name: "Cp", scope: !111, file: !1, line: 271, type: !29)
!122 = !DILocalVariable(name: "Ci", scope: !111, file: !1, line: 271, type: !29)
!123 = !DILocalVariable(name: "Pinv", scope: !111, file: !1, line: 271, type: !29)
!124 = !DILocalVariable(name: "Pblk", scope: !111, file: !1, line: 271, type: !29)
!125 = !DILocalVariable(name: "Pbtf", scope: !111, file: !1, line: 271, type: !29)
!126 = !DILocalVariable(name: "P", scope: !111, file: !1, line: 271, type: !29)
!127 = !DILocalVariable(name: "Q", scope: !111, file: !1, line: 271, type: !29)
!128 = !DILocalVariable(name: "R", scope: !111, file: !1, line: 271, type: !29)
!129 = !DILocalVariable(name: "nblocks", scope: !111, file: !1, line: 272, type: !5)
!130 = !DILocalVariable(name: "nz", scope: !111, file: !1, line: 272, type: !5)
!131 = !DILocalVariable(name: "block", scope: !111, file: !1, line: 272, type: !5)
!132 = !DILocalVariable(name: "maxblock", scope: !111, file: !1, line: 272, type: !5)
!133 = !DILocalVariable(name: "k1", scope: !111, file: !1, line: 272, type: !5)
!134 = !DILocalVariable(name: "k2", scope: !111, file: !1, line: 272, type: !5)
!135 = !DILocalVariable(name: "nk", scope: !111, file: !1, line: 272, type: !5)
!136 = !DILocalVariable(name: "do_btf", scope: !111, file: !1, line: 272, type: !5)
!137 = !DILocalVariable(name: "ordering", scope: !111, file: !1, line: 272, type: !5)
!138 = !DILocalVariable(name: "k", scope: !111, file: !1, line: 272, type: !5)
!139 = !DILocalVariable(name: "Cilen", scope: !111, file: !1, line: 272, type: !5)
!140 = !DILocalVariable(name: "Work", scope: !111, file: !1, line: 273, type: !29)
!141 = !DILocation(line: 261, column: 9, scope: !111)
!142 = !DILocation(line: 262, column: 9, scope: !111)
!143 = !DILocation(line: 263, column: 9, scope: !111)
!144 = !DILocation(line: 265, column: 17, scope: !111)
!145 = !DILocation(line: 268, column: 5, scope: !111)
!146 = !DILocation(line: 269, column: 5, scope: !111)
!147 = !DILocation(line: 279, column: 16, scope: !111)
!148 = !DILocation(line: 269, column: 19, scope: !111)
!149 = !DILocation(line: 279, column: 14, scope: !111)
!150 = !{!93, !93, i64 0}
!151 = !DILocation(line: 280, column: 18, scope: !152)
!152 = distinct !DILexicalBlock(scope: !111, file: !1, line: 280, column: 9)
!153 = !DILocation(line: 280, column: 9, scope: !111)
!154 = !DILocation(line: 284, column: 19, scope: !111)
!155 = !{!156, !93, i64 48}
!156 = !{!"", !89, i64 0, !89, i64 8, !89, i64 16, !89, i64 24, !93, i64 32, !92, i64 40, !92, i64 44, !93, i64 48, !93, i64 56, !93, i64 64, !92, i64 72, !92, i64 76, !92, i64 80, !92, i64 84, !92, i64 88, !92, i64 92}
!157 = !DILocation(line: 271, column: 48, scope: !111)
!158 = !DILocation(line: 285, column: 19, scope: !111)
!159 = !{!156, !93, i64 56}
!160 = !DILocation(line: 271, column: 52, scope: !111)
!161 = !DILocation(line: 286, column: 19, scope: !111)
!162 = !{!156, !93, i64 64}
!163 = !DILocation(line: 271, column: 56, scope: !111)
!164 = !DILocation(line: 287, column: 21, scope: !111)
!165 = !{!156, !93, i64 32}
!166 = !DILocation(line: 270, column: 13, scope: !111)
!167 = !DILocation(line: 288, column: 20, scope: !111)
!168 = !{!156, !92, i64 44}
!169 = !DILocation(line: 272, column: 18, scope: !111)
!170 = !DILocation(line: 290, column: 24, scope: !111)
!171 = !DILocation(line: 272, column: 59, scope: !111)
!172 = !DILocation(line: 291, column: 9, scope: !111)
!173 = !DILocation(line: 294, column: 17, scope: !174)
!174 = distinct !DILexicalBlock(scope: !175, file: !1, line: 292, column: 5)
!175 = distinct !DILexicalBlock(scope: !111, file: !1, line: 291, column: 9)
!176 = !DILocation(line: 272, column: 72, scope: !111)
!177 = !DILocation(line: 295, column: 5, scope: !174)
!178 = !DILocation(line: 296, column: 57, scope: !179)
!179 = distinct !DILexicalBlock(scope: !175, file: !1, line: 296, column: 14)
!180 = !{!88, !93, i64 56}
!181 = !DILocation(line: 296, column: 68, scope: !179)
!182 = !DILocation(line: 296, column: 14, scope: !175)
!183 = !DILocation(line: 299, column: 19, scope: !184)
!184 = distinct !DILexicalBlock(scope: !179, file: !1, line: 297, column: 5)
!185 = !DILocation(line: 304, column: 17, scope: !186)
!186 = distinct !DILexicalBlock(scope: !179, file: !1, line: 302, column: 5)
!187 = !DILocation(line: 304, column: 24, scope: !186)
!188 = !DILocation(line: 305, column: 9, scope: !186)
!189 = !DILocation(line: 306, column: 9, scope: !186)
!190 = !DILocation(line: 0, scope: !184)
!191 = !DILocation(line: 313, column: 24, scope: !111)
!192 = !DILocation(line: 313, column: 12, scope: !111)
!193 = !DILocation(line: 271, column: 41, scope: !111)
!194 = !DILocation(line: 314, column: 12, scope: !111)
!195 = !DILocation(line: 271, column: 10, scope: !111)
!196 = !DILocation(line: 315, column: 17, scope: !197)
!197 = distinct !DILexicalBlock(scope: !111, file: !1, line: 315, column: 9)
!198 = !DILocation(line: 315, column: 24, scope: !197)
!199 = !DILocation(line: 315, column: 9, scope: !111)
!200 = !DILocation(line: 317, column: 9, scope: !201)
!201 = distinct !DILexicalBlock(scope: !197, file: !1, line: 316, column: 5)
!202 = !DILocation(line: 318, column: 9, scope: !201)
!203 = !DILocation(line: 319, column: 9, scope: !201)
!204 = !DILocation(line: 320, column: 9, scope: !201)
!205 = !DILocation(line: 327, column: 22, scope: !111)
!206 = !{!88, !92, i64 40}
!207 = !DILocation(line: 272, column: 51, scope: !111)
!208 = !DILocation(line: 328, column: 14, scope: !111)
!209 = !DILocation(line: 329, column: 5, scope: !111)
!210 = !DILocation(line: 329, column: 15, scope: !111)
!211 = !DILocation(line: 329, column: 24, scope: !111)
!212 = !{!156, !92, i64 84}
!213 = !DILocation(line: 330, column: 15, scope: !111)
!214 = !DILocation(line: 330, column: 22, scope: !111)
!215 = !{!156, !92, i64 88}
!216 = !DILocation(line: 331, column: 15, scope: !111)
!217 = !DILocation(line: 331, column: 31, scope: !111)
!218 = !{!156, !92, i64 92}
!219 = !DILocation(line: 337, column: 13, scope: !111)
!220 = !DILocation(line: 337, column: 18, scope: !111)
!221 = !{!88, !89, i64 136}
!222 = !DILocation(line: 339, column: 9, scope: !111)
!223 = !DILocation(line: 341, column: 29, scope: !224)
!224 = distinct !DILexicalBlock(scope: !225, file: !1, line: 340, column: 5)
!225 = distinct !DILexicalBlock(scope: !111, file: !1, line: 339, column: 9)
!226 = !DILocation(line: 341, column: 28, scope: !224)
!227 = !DILocation(line: 341, column: 16, scope: !224)
!228 = !DILocation(line: 342, column: 21, scope: !229)
!229 = distinct !DILexicalBlock(scope: !224, file: !1, line: 342, column: 13)
!230 = !DILocation(line: 342, column: 28, scope: !229)
!231 = !DILocation(line: 342, column: 13, scope: !224)
!232 = !DILocation(line: 345, column: 13, scope: !233)
!233 = distinct !DILexicalBlock(scope: !229, file: !1, line: 343, column: 9)
!234 = !DILocation(line: 346, column: 13, scope: !233)
!235 = !DILocation(line: 347, column: 13, scope: !233)
!236 = !DILocation(line: 348, column: 13, scope: !233)
!237 = !DILocation(line: 273, column: 10, scope: !111)
!238 = !DILocation(line: 351, column: 49, scope: !224)
!239 = !{!88, !89, i64 32}
!240 = !DILocation(line: 352, column: 19, scope: !224)
!241 = !DILocation(line: 352, column: 29, scope: !224)
!242 = !DILocation(line: 268, column: 12, scope: !111)
!243 = !DILocation(line: 351, column: 19, scope: !224)
!244 = !DILocation(line: 272, column: 9, scope: !111)
!245 = !DILocation(line: 353, column: 35, scope: !224)
!246 = !DILocation(line: 353, column: 45, scope: !224)
!247 = !DILocation(line: 353, column: 17, scope: !224)
!248 = !DILocation(line: 353, column: 33, scope: !224)
!249 = !DILocation(line: 354, column: 25, scope: !224)
!250 = !{!89, !89, i64 0}
!251 = !DILocation(line: 354, column: 22, scope: !224)
!252 = !DILocation(line: 356, column: 9, scope: !224)
!253 = !DILocation(line: 359, column: 13, scope: !254)
!254 = distinct !DILexicalBlock(scope: !224, file: !1, line: 359, column: 13)
!255 = !DILocation(line: 359, column: 23, scope: !254)
!256 = !DILocation(line: 359, column: 39, scope: !254)
!257 = !DILocation(line: 361, column: 28, scope: !258)
!258 = distinct !DILexicalBlock(scope: !259, file: !1, line: 361, column: 13)
!259 = distinct !DILexicalBlock(scope: !260, file: !1, line: 361, column: 13)
!260 = distinct !DILexicalBlock(scope: !254, file: !1, line: 360, column: 9)
!261 = !DILocation(line: 359, column: 13, scope: !224)
!262 = !DILocation(line: 272, column: 69, scope: !111)
!263 = !DILocation(line: 361, column: 13, scope: !259)
!264 = !DILocation(line: 363, column: 28, scope: !265)
!265 = distinct !DILexicalBlock(scope: !258, file: !1, line: 362, column: 13)
!266 = !{!92, !92, i64 0}
!267 = !DILocation(line: 363, column: 26, scope: !265)
!268 = !DILocation(line: 361, column: 35, scope: !258)
!269 = distinct !{!269, !263, !270}
!270 = !DILocation(line: 364, column: 13, scope: !259)
!271 = !DILocation(line: 272, column: 29, scope: !111)
!272 = !DILocation(line: 272, column: 22, scope: !111)
!273 = !DILocation(line: 369, column: 32, scope: !274)
!274 = distinct !DILexicalBlock(scope: !275, file: !1, line: 369, column: 9)
!275 = distinct !DILexicalBlock(scope: !224, file: !1, line: 369, column: 9)
!276 = !DILocation(line: 369, column: 9, scope: !275)
!277 = !DILocation(line: 371, column: 18, scope: !278)
!278 = distinct !DILexicalBlock(scope: !274, file: !1, line: 370, column: 9)
!279 = !DILocation(line: 272, column: 39, scope: !111)
!280 = !DILocation(line: 372, column: 26, scope: !278)
!281 = !DILocation(line: 372, column: 18, scope: !278)
!282 = !DILocation(line: 272, column: 43, scope: !111)
!283 = !DILocation(line: 373, column: 21, scope: !278)
!284 = !DILocation(line: 272, column: 47, scope: !111)
!285 = !DILocation(line: 375, column: 24, scope: !278)
!286 = distinct !{!286, !276, !287}
!287 = !DILocation(line: 376, column: 9, scope: !275)
!288 = !DILocation(line: 383, column: 15, scope: !289)
!289 = distinct !DILexicalBlock(scope: !225, file: !1, line: 379, column: 5)
!290 = !DILocation(line: 384, column: 9, scope: !289)
!291 = !DILocation(line: 384, column: 15, scope: !289)
!292 = !DILocation(line: 385, column: 24, scope: !293)
!293 = distinct !DILexicalBlock(scope: !294, file: !1, line: 385, column: 9)
!294 = distinct !DILexicalBlock(scope: !289, file: !1, line: 385, column: 9)
!295 = !DILocation(line: 385, column: 9, scope: !294)
!296 = !DILocation(line: 387, column: 13, scope: !297)
!297 = distinct !DILexicalBlock(scope: !293, file: !1, line: 386, column: 9)
!298 = !DILocation(line: 387, column: 22, scope: !297)
!299 = !DILocation(line: 388, column: 13, scope: !297)
!300 = !DILocation(line: 388, column: 22, scope: !297)
!301 = !DILocation(line: 385, column: 31, scope: !293)
!302 = distinct !{!302, !295, !303}
!303 = !DILocation(line: 389, column: 9, scope: !294)
!304 = !DILocation(line: 0, scope: !289)
!305 = !DILocation(line: 392, column: 5, scope: !111)
!306 = !DILocation(line: 392, column: 15, scope: !111)
!307 = !DILocation(line: 392, column: 23, scope: !111)
!308 = !{!156, !92, i64 76}
!309 = !DILocation(line: 395, column: 15, scope: !111)
!310 = !DILocation(line: 395, column: 24, scope: !111)
!311 = !{!156, !92, i64 80}
!312 = !DILocation(line: 401, column: 24, scope: !111)
!313 = !DILocation(line: 401, column: 12, scope: !111)
!314 = !DILocation(line: 402, column: 33, scope: !111)
!315 = !DILocation(line: 402, column: 24, scope: !111)
!316 = !DILocation(line: 402, column: 12, scope: !111)
!317 = !DILocation(line: 403, column: 24, scope: !111)
!318 = !DILocation(line: 403, column: 12, scope: !111)
!319 = !DILocation(line: 404, column: 12, scope: !111)
!320 = !DILocation(line: 410, column: 17, scope: !321)
!321 = distinct !DILexicalBlock(scope: !111, file: !1, line: 410, column: 9)
!322 = !DILocation(line: 410, column: 24, scope: !321)
!323 = !DILocation(line: 410, column: 9, scope: !111)
!324 = !DILocation(line: 271, column: 27, scope: !111)
!325 = !DILocation(line: 271, column: 22, scope: !111)
!326 = !DILocation(line: 271, column: 17, scope: !111)
!327 = !DILocation(line: 271, column: 34, scope: !111)
!328 = !DILocation(line: 414, column: 61, scope: !329)
!329 = distinct !DILexicalBlock(scope: !321, file: !1, line: 411, column: 5)
!330 = !DILocation(line: 413, column: 26, scope: !329)
!331 = !DILocation(line: 413, column: 24, scope: !329)
!332 = !DILocation(line: 416, column: 5, scope: !329)
!333 = !DILocation(line: 422, column: 5, scope: !111)
!334 = !DILocation(line: 423, column: 5, scope: !111)
!335 = !DILocation(line: 424, column: 5, scope: !111)
!336 = !DILocation(line: 425, column: 5, scope: !111)
!337 = !DILocation(line: 426, column: 5, scope: !111)
!338 = !DILocation(line: 427, column: 5, scope: !111)
!339 = !DILocation(line: 433, column: 17, scope: !340)
!340 = distinct !DILexicalBlock(scope: !111, file: !1, line: 433, column: 9)
!341 = !DILocation(line: 433, column: 24, scope: !340)
!342 = !DILocation(line: 433, column: 9, scope: !111)
!343 = !DILocation(line: 435, column: 9, scope: !344)
!344 = distinct !DILexicalBlock(scope: !340, file: !1, line: 434, column: 5)
!345 = !DILocation(line: 436, column: 5, scope: !344)
!346 = !DILocation(line: 437, column: 13, scope: !111)
!347 = !DILocation(line: 437, column: 5, scope: !111)
!348 = !DILocation(line: 0, scope: !186)
!349 = !DILocation(line: 438, column: 1, scope: !111)
!350 = distinct !DISubprogram(name: "analyze_worker", scope: !1, file: !1, line: 15, type: !351, isLocal: true, isDefinition: true, scopeLine: 43, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !353)
!351 = !DISubroutineType(types: !352)
!352 = !{!5, !5, !29, !29, !5, !29, !29, !29, !5, !29, !29, !25, !29, !29, !29, !5, !29, !14, !38}
!353 = !{!354, !355, !356, !357, !358, !359, !360, !361, !362, !363, !364, !365, !366, !367, !368, !369, !370, !371, !372, !376, !377, !378, !379, !380, !381, !382, !383, !384, !385, !386, !387, !388, !389, !390, !391, !392, !393, !394, !396, !397}
!354 = !DILocalVariable(name: "n", arg: 1, scope: !350, file: !1, line: 18, type: !5)
!355 = !DILocalVariable(name: "Ap", arg: 2, scope: !350, file: !1, line: 19, type: !29)
!356 = !DILocalVariable(name: "Ai", arg: 3, scope: !350, file: !1, line: 20, type: !29)
!357 = !DILocalVariable(name: "nblocks", arg: 4, scope: !350, file: !1, line: 21, type: !5)
!358 = !DILocalVariable(name: "Pbtf", arg: 5, scope: !350, file: !1, line: 22, type: !29)
!359 = !DILocalVariable(name: "Qbtf", arg: 6, scope: !350, file: !1, line: 23, type: !29)
!360 = !DILocalVariable(name: "R", arg: 7, scope: !350, file: !1, line: 24, type: !29)
!361 = !DILocalVariable(name: "ordering", arg: 8, scope: !350, file: !1, line: 25, type: !5)
!362 = !DILocalVariable(name: "P", arg: 9, scope: !350, file: !1, line: 28, type: !29)
!363 = !DILocalVariable(name: "Q", arg: 10, scope: !350, file: !1, line: 29, type: !29)
!364 = !DILocalVariable(name: "Lnz", arg: 11, scope: !350, file: !1, line: 30, type: !25)
!365 = !DILocalVariable(name: "Pblk", arg: 12, scope: !350, file: !1, line: 33, type: !29)
!366 = !DILocalVariable(name: "Cp", arg: 13, scope: !350, file: !1, line: 34, type: !29)
!367 = !DILocalVariable(name: "Ci", arg: 14, scope: !350, file: !1, line: 35, type: !29)
!368 = !DILocalVariable(name: "Cilen", arg: 15, scope: !350, file: !1, line: 36, type: !5)
!369 = !DILocalVariable(name: "Pinv", arg: 16, scope: !350, file: !1, line: 37, type: !29)
!370 = !DILocalVariable(name: "Symbolic", arg: 17, scope: !350, file: !1, line: 40, type: !14)
!371 = !DILocalVariable(name: "Common", arg: 18, scope: !350, file: !1, line: 41, type: !38)
!372 = !DILocalVariable(name: "amd_Info", scope: !350, file: !1, line: 44, type: !373)
!373 = !DICompositeType(tag: DW_TAG_array_type, baseType: !20, size: 1280, elements: !374)
!374 = !{!375}
!375 = !DISubrange(count: 20)
!376 = !DILocalVariable(name: "lnz", scope: !350, file: !1, line: 44, type: !20)
!377 = !DILocalVariable(name: "lnz1", scope: !350, file: !1, line: 44, type: !20)
!378 = !DILocalVariable(name: "flops", scope: !350, file: !1, line: 44, type: !20)
!379 = !DILocalVariable(name: "flops1", scope: !350, file: !1, line: 44, type: !20)
!380 = !DILocalVariable(name: "k1", scope: !350, file: !1, line: 45, type: !5)
!381 = !DILocalVariable(name: "k2", scope: !350, file: !1, line: 45, type: !5)
!382 = !DILocalVariable(name: "nk", scope: !350, file: !1, line: 45, type: !5)
!383 = !DILocalVariable(name: "k", scope: !350, file: !1, line: 45, type: !5)
!384 = !DILocalVariable(name: "block", scope: !350, file: !1, line: 45, type: !5)
!385 = !DILocalVariable(name: "oldcol", scope: !350, file: !1, line: 45, type: !5)
!386 = !DILocalVariable(name: "pend", scope: !350, file: !1, line: 45, type: !5)
!387 = !DILocalVariable(name: "newcol", scope: !350, file: !1, line: 45, type: !5)
!388 = !DILocalVariable(name: "result", scope: !350, file: !1, line: 45, type: !5)
!389 = !DILocalVariable(name: "pc", scope: !350, file: !1, line: 45, type: !5)
!390 = !DILocalVariable(name: "p", scope: !350, file: !1, line: 45, type: !5)
!391 = !DILocalVariable(name: "newrow", scope: !350, file: !1, line: 45, type: !5)
!392 = !DILocalVariable(name: "maxnz", scope: !350, file: !1, line: 46, type: !5)
!393 = !DILocalVariable(name: "nzoff", scope: !350, file: !1, line: 46, type: !5)
!394 = !DILocalVariable(name: "cstats", scope: !350, file: !1, line: 46, type: !395)
!395 = !DICompositeType(tag: DW_TAG_array_type, baseType: !5, size: 640, elements: !374)
!396 = !DILocalVariable(name: "ok", scope: !350, file: !1, line: 46, type: !5)
!397 = !DILocalVariable(name: "err", scope: !350, file: !1, line: 46, type: !5)
!398 = !DILocation(line: 18, column: 9, scope: !350)
!399 = !DILocation(line: 19, column: 9, scope: !350)
!400 = !DILocation(line: 20, column: 9, scope: !350)
!401 = !DILocation(line: 21, column: 9, scope: !350)
!402 = !DILocation(line: 22, column: 9, scope: !350)
!403 = !DILocation(line: 23, column: 9, scope: !350)
!404 = !DILocation(line: 24, column: 9, scope: !350)
!405 = !DILocation(line: 25, column: 9, scope: !350)
!406 = !DILocation(line: 28, column: 9, scope: !350)
!407 = !DILocation(line: 29, column: 9, scope: !350)
!408 = !DILocation(line: 30, column: 12, scope: !350)
!409 = !DILocation(line: 33, column: 9, scope: !350)
!410 = !DILocation(line: 34, column: 9, scope: !350)
!411 = !DILocation(line: 35, column: 9, scope: !350)
!412 = !DILocation(line: 36, column: 9, scope: !350)
!413 = !DILocation(line: 37, column: 9, scope: !350)
!414 = !DILocation(line: 40, column: 19, scope: !350)
!415 = !DILocation(line: 41, column: 17, scope: !350)
!416 = !DILocation(line: 44, column: 5, scope: !350)
!417 = !DILocation(line: 44, column: 12, scope: !350)
!418 = !DILocation(line: 45, column: 5, scope: !350)
!419 = !DILocation(line: 46, column: 23, scope: !350)
!420 = !DILocation(line: 46, column: 50, scope: !350)
!421 = !DILocation(line: 45, column: 21, scope: !350)
!422 = !DILocation(line: 61, column: 20, scope: !423)
!423 = distinct !DILexicalBlock(scope: !424, file: !1, line: 61, column: 5)
!424 = distinct !DILexicalBlock(scope: !350, file: !1, line: 61, column: 5)
!425 = !DILocation(line: 61, column: 5, scope: !424)
!426 = !DILocation(line: 64, column: 15, scope: !427)
!427 = distinct !DILexicalBlock(scope: !423, file: !1, line: 62, column: 5)
!428 = !DILocation(line: 64, column: 9, scope: !427)
!429 = !DILocation(line: 64, column: 25, scope: !427)
!430 = !DILocation(line: 61, column: 27, scope: !423)
!431 = distinct !{!431, !425, !432}
!432 = !DILocation(line: 65, column: 5, scope: !424)
!433 = !DILocation(line: 46, column: 16, scope: !350)
!434 = !DILocation(line: 44, column: 33, scope: !350)
!435 = !DILocation(line: 46, column: 9, scope: !350)
!436 = !DILocation(line: 44, column: 44, scope: !350)
!437 = !DILocation(line: 73, column: 15, scope: !350)
!438 = !DILocation(line: 73, column: 24, scope: !350)
!439 = !{!156, !89, i64 0}
!440 = !DILocation(line: 45, column: 24, scope: !350)
!441 = !DILocation(line: 79, column: 28, scope: !442)
!442 = distinct !DILexicalBlock(scope: !443, file: !1, line: 79, column: 5)
!443 = distinct !DILexicalBlock(scope: !350, file: !1, line: 79, column: 5)
!444 = !DILocation(line: 79, column: 5, scope: !443)
!445 = !DILocation(line: 86, column: 14, scope: !446)
!446 = distinct !DILexicalBlock(scope: !442, file: !1, line: 80, column: 5)
!447 = !DILocation(line: 45, column: 9, scope: !350)
!448 = !DILocation(line: 87, column: 22, scope: !446)
!449 = !DILocation(line: 87, column: 14, scope: !446)
!450 = !DILocation(line: 45, column: 13, scope: !350)
!451 = !DILocation(line: 88, column: 17, scope: !446)
!452 = !DILocation(line: 45, column: 17, scope: !350)
!453 = !DILocation(line: 95, column: 9, scope: !446)
!454 = !DILocation(line: 95, column: 21, scope: !446)
!455 = !DILocation(line: 45, column: 61, scope: !350)
!456 = !DILocation(line: 97, column: 25, scope: !457)
!457 = distinct !DILexicalBlock(scope: !458, file: !1, line: 97, column: 9)
!458 = distinct !DILexicalBlock(scope: !446, file: !1, line: 97, column: 9)
!459 = !DILocation(line: 97, column: 9, scope: !458)
!460 = !DILocation(line: 99, column: 23, scope: !461)
!461 = distinct !DILexicalBlock(scope: !457, file: !1, line: 98, column: 9)
!462 = !DILocation(line: 100, column: 13, scope: !461)
!463 = !DILocation(line: 100, column: 25, scope: !461)
!464 = !DILocation(line: 101, column: 22, scope: !461)
!465 = !DILocation(line: 45, column: 31, scope: !350)
!466 = !DILocation(line: 102, column: 30, scope: !461)
!467 = !DILocation(line: 102, column: 20, scope: !461)
!468 = !DILocation(line: 45, column: 39, scope: !350)
!469 = !DILocation(line: 103, column: 22, scope: !470)
!470 = distinct !DILexicalBlock(scope: !461, file: !1, line: 103, column: 13)
!471 = !DILocation(line: 45, column: 65, scope: !350)
!472 = !DILocation(line: 103, column: 38, scope: !473)
!473 = distinct !DILexicalBlock(scope: !470, file: !1, line: 103, column: 13)
!474 = !DILocation(line: 103, column: 13, scope: !470)
!475 = !DILocation(line: 105, column: 32, scope: !476)
!476 = distinct !DILexicalBlock(scope: !473, file: !1, line: 104, column: 13)
!477 = !DILocation(line: 105, column: 26, scope: !476)
!478 = !DILocation(line: 45, column: 68, scope: !350)
!479 = !DILocation(line: 106, column: 28, scope: !480)
!480 = distinct !DILexicalBlock(scope: !476, file: !1, line: 106, column: 21)
!481 = !DILocation(line: 106, column: 21, scope: !476)
!482 = !DILocation(line: 108, column: 26, scope: !483)
!483 = distinct !DILexicalBlock(scope: !480, file: !1, line: 107, column: 17)
!484 = !DILocation(line: 109, column: 17, scope: !483)
!485 = !DILocation(line: 114, column: 28, scope: !486)
!486 = distinct !DILexicalBlock(scope: !480, file: !1, line: 111, column: 17)
!487 = !DILocation(line: 115, column: 27, scope: !486)
!488 = !DILocation(line: 115, column: 21, scope: !486)
!489 = !DILocation(line: 115, column: 31, scope: !486)
!490 = !DILocation(line: 0, scope: !486)
!491 = !DILocation(line: 0, scope: !350)
!492 = !DILocation(line: 103, column: 48, scope: !473)
!493 = distinct !{!493, !474, !494}
!494 = !DILocation(line: 117, column: 13, scope: !470)
!495 = !DILocation(line: 97, column: 33, scope: !457)
!496 = distinct !{!496, !459, !497}
!497 = !DILocation(line: 118, column: 9, scope: !458)
!498 = !DILocation(line: 119, column: 9, scope: !446)
!499 = !DILocation(line: 119, column: 17, scope: !446)
!500 = !DILocation(line: 120, column: 17, scope: !446)
!501 = !DILocation(line: 127, column: 16, scope: !502)
!502 = distinct !DILexicalBlock(scope: !446, file: !1, line: 127, column: 13)
!503 = !DILocation(line: 127, column: 13, scope: !446)
!504 = !DILocation(line: 134, column: 28, scope: !505)
!505 = distinct !DILexicalBlock(scope: !506, file: !1, line: 134, column: 13)
!506 = distinct !DILexicalBlock(scope: !507, file: !1, line: 134, column: 13)
!507 = distinct !DILexicalBlock(scope: !502, file: !1, line: 128, column: 9)
!508 = !DILocation(line: 134, column: 13, scope: !506)
!509 = !DILocation(line: 136, column: 17, scope: !510)
!510 = distinct !DILexicalBlock(scope: !505, file: !1, line: 135, column: 13)
!511 = !DILocation(line: 136, column: 26, scope: !510)
!512 = !DILocation(line: 134, column: 36, scope: !505)
!513 = distinct !{!513, !508, !514}
!514 = !DILocation(line: 137, column: 13, scope: !506)
!515 = !DILocation(line: 138, column: 29, scope: !507)
!516 = !DILocation(line: 138, column: 23, scope: !507)
!517 = !DILocation(line: 138, column: 34, scope: !507)
!518 = !DILocation(line: 138, column: 20, scope: !507)
!519 = !DILocation(line: 44, column: 38, scope: !350)
!520 = !DILocation(line: 139, column: 31, scope: !507)
!521 = !DILocation(line: 139, column: 25, scope: !507)
!522 = !DILocation(line: 139, column: 36, scope: !507)
!523 = !DILocation(line: 139, column: 54, scope: !507)
!524 = !DILocation(line: 139, column: 57, scope: !507)
!525 = !DILocation(line: 139, column: 51, scope: !507)
!526 = !DILocation(line: 139, column: 61, scope: !507)
!527 = !DILocation(line: 139, column: 40, scope: !507)
!528 = !DILocation(line: 139, column: 22, scope: !507)
!529 = !DILocation(line: 44, column: 51, scope: !350)
!530 = !DILocation(line: 46, column: 46, scope: !350)
!531 = !DILocation(line: 205, column: 13, scope: !446)
!532 = !DILocation(line: 143, column: 18, scope: !502)
!533 = !DILocation(line: 150, column: 22, scope: !534)
!534 = distinct !DILexicalBlock(scope: !535, file: !1, line: 144, column: 9)
!535 = distinct !DILexicalBlock(scope: !502, file: !1, line: 143, column: 18)
!536 = !DILocation(line: 45, column: 53, scope: !350)
!537 = !DILocation(line: 151, column: 26, scope: !534)
!538 = !DILocation(line: 152, column: 24, scope: !539)
!539 = distinct !DILexicalBlock(scope: !534, file: !1, line: 152, column: 17)
!540 = !DILocation(line: 152, column: 17, scope: !534)
!541 = !DILocation(line: 158, column: 31, scope: !534)
!542 = !{!88, !94, i64 152}
!543 = !{!88, !94, i64 144}
!544 = !DILocation(line: 158, column: 29, scope: !534)
!545 = !DILocation(line: 162, column: 27, scope: !534)
!546 = !DILocation(line: 162, column: 20, scope: !534)
!547 = !DILocation(line: 162, column: 47, scope: !534)
!548 = !DILocation(line: 163, column: 26, scope: !534)
!549 = !DILocation(line: 163, column: 24, scope: !534)
!550 = !DILocation(line: 163, column: 56, scope: !534)
!551 = !DILocation(line: 163, column: 54, scope: !534)
!552 = !DILocation(line: 164, column: 17, scope: !534)
!553 = !DILocation(line: 167, column: 38, scope: !554)
!554 = distinct !DILexicalBlock(scope: !555, file: !1, line: 165, column: 13)
!555 = distinct !DILexicalBlock(scope: !534, file: !1, line: 164, column: 17)
!556 = !DILocation(line: 167, column: 36, scope: !554)
!557 = !DILocation(line: 168, column: 13, scope: !554)
!558 = !DILocation(line: 182, column: 18, scope: !559)
!559 = distinct !DILexicalBlock(scope: !560, file: !1, line: 172, column: 9)
!560 = distinct !DILexicalBlock(scope: !535, file: !1, line: 171, column: 18)
!561 = !DILocation(line: 187, column: 13, scope: !562)
!562 = distinct !DILexicalBlock(scope: !559, file: !1, line: 187, column: 13)
!563 = !DILocation(line: 189, column: 28, scope: !564)
!564 = distinct !DILexicalBlock(scope: !565, file: !1, line: 188, column: 13)
!565 = distinct !DILexicalBlock(scope: !562, file: !1, line: 187, column: 13)
!566 = !DILocation(line: 189, column: 17, scope: !564)
!567 = !DILocation(line: 189, column: 26, scope: !564)
!568 = !DILocation(line: 187, column: 36, scope: !565)
!569 = !DILocation(line: 187, column: 28, scope: !565)
!570 = distinct !{!570, !561, !571}
!571 = !DILocation(line: 190, column: 13, scope: !562)
!572 = !DILocation(line: 200, column: 20, scope: !573)
!573 = distinct !DILexicalBlock(scope: !560, file: !1, line: 194, column: 9)
!574 = !DILocation(line: 202, column: 24, scope: !573)
!575 = !DILocation(line: 0, scope: !573)
!576 = !DILocation(line: 205, column: 14, scope: !577)
!577 = distinct !DILexicalBlock(scope: !446, file: !1, line: 205, column: 13)
!578 = !DILocation(line: 214, column: 21, scope: !446)
!579 = !DILocation(line: 215, column: 20, scope: !446)
!580 = !DILocation(line: 215, column: 37, scope: !446)
!581 = !DILocation(line: 215, column: 29, scope: !446)
!582 = !DILocation(line: 215, column: 62, scope: !446)
!583 = !DILocation(line: 216, column: 24, scope: !446)
!584 = !DILocation(line: 216, column: 43, scope: !446)
!585 = !DILocation(line: 216, column: 33, scope: !446)
!586 = !DILocation(line: 216, column: 70, scope: !446)
!587 = !DILocation(line: 223, column: 24, scope: !588)
!588 = distinct !DILexicalBlock(scope: !589, file: !1, line: 223, column: 9)
!589 = distinct !DILexicalBlock(scope: !446, file: !1, line: 223, column: 9)
!590 = !DILocation(line: 223, column: 9, scope: !589)
!591 = !DILocation(line: 227, column: 32, scope: !592)
!592 = distinct !DILexicalBlock(scope: !588, file: !1, line: 224, column: 9)
!593 = !DILocation(line: 227, column: 41, scope: !592)
!594 = !DILocation(line: 227, column: 26, scope: !592)
!595 = !DILocation(line: 227, column: 18, scope: !592)
!596 = !DILocation(line: 227, column: 13, scope: !592)
!597 = !DILocation(line: 227, column: 24, scope: !592)
!598 = !DILocation(line: 223, column: 32, scope: !588)
!599 = distinct !{!599, !590, !600}
!600 = !DILocation(line: 228, column: 9, scope: !589)
!601 = !DILocation(line: 229, column: 24, scope: !602)
!602 = distinct !DILexicalBlock(scope: !603, file: !1, line: 229, column: 9)
!603 = distinct !DILexicalBlock(scope: !446, file: !1, line: 229, column: 9)
!604 = !DILocation(line: 229, column: 9, scope: !603)
!605 = !DILocation(line: 233, column: 32, scope: !606)
!606 = distinct !DILexicalBlock(scope: !602, file: !1, line: 230, column: 9)
!607 = !DILocation(line: 233, column: 41, scope: !606)
!608 = !DILocation(line: 233, column: 26, scope: !606)
!609 = !DILocation(line: 233, column: 18, scope: !606)
!610 = !DILocation(line: 233, column: 13, scope: !606)
!611 = !DILocation(line: 233, column: 24, scope: !606)
!612 = !DILocation(line: 229, column: 32, scope: !602)
!613 = distinct !{!613, !604, !614}
!614 = !DILocation(line: 234, column: 9, scope: !603)
!615 = distinct !{!615, !444, !616}
!616 = !DILocation(line: 235, column: 5, scope: !443)
!617 = !DILocation(line: 241, column: 15, scope: !350)
!618 = !DILocation(line: 241, column: 19, scope: !350)
!619 = !{!156, !89, i64 16}
!620 = !DILocation(line: 242, column: 15, scope: !350)
!621 = !DILocation(line: 242, column: 19, scope: !350)
!622 = !{!156, !89, i64 24}
!623 = !DILocation(line: 243, column: 15, scope: !350)
!624 = !DILocation(line: 243, column: 21, scope: !350)
!625 = !{!156, !92, i64 72}
!626 = !DILocation(line: 244, column: 15, scope: !350)
!627 = !DILocation(line: 244, column: 25, scope: !350)
!628 = !{!156, !89, i64 8}
!629 = !DILocation(line: 245, column: 5, scope: !350)
!630 = !DILocation(line: 246, column: 1, scope: !350)
