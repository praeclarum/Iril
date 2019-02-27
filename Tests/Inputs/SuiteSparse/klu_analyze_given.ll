; ModuleID = 'klu_analyze_given.c'
source_filename = "klu_analyze_given.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_alloc_symbolic(i32, i32* readonly, i32* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !12 {
  %5 = alloca %struct.klu_symbolic*, align 8
  call void @llvm.dbg.value(metadata i32 %0, metadata !74, metadata !DIExpression()), !dbg !88
  call void @llvm.dbg.value(metadata i32* %1, metadata !75, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32* %2, metadata !76, metadata !DIExpression()), !dbg !90
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !77, metadata !DIExpression()), !dbg !91
  %6 = bitcast %struct.klu_symbolic** %5 to i8*, !dbg !92
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %6) #4, !dbg !92
  %7 = icmp eq %struct.klu_common_struct* %3, null, !dbg !93
  br i1 %7, label %110, label %8, !dbg !95

; <label>:8:                                      ; preds = %4
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11, !dbg !96
  store i32 0, i32* %9, align 4, !dbg !97, !tbaa !98
  %10 = icmp slt i32 %0, 1, !dbg !106
  %11 = icmp eq i32* %1, null, !dbg !108
  %12 = or i1 %10, %11, !dbg !109
  %13 = icmp eq i32* %2, null, !dbg !110
  %14 = or i1 %12, %13, !dbg !109
  br i1 %14, label %15, label %16, !dbg !109

; <label>:15:                                     ; preds = %8
  store i32 -3, i32* %9, align 4, !dbg !111, !tbaa !98
  br label %110, !dbg !113

; <label>:16:                                     ; preds = %8
  %17 = sext i32 %0 to i64, !dbg !114
  %18 = getelementptr inbounds i32, i32* %1, i64 %17, !dbg !114
  %19 = load i32, i32* %18, align 4, !dbg !114, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %19, metadata !83, metadata !DIExpression()), !dbg !116
  %20 = load i32, i32* %1, align 4, !dbg !117, !tbaa !115
  %21 = icmp ne i32 %20, 0, !dbg !119
  %22 = icmp slt i32 %19, 0, !dbg !120
  %23 = or i1 %22, %21, !dbg !121
  br i1 %23, label %24, label %25, !dbg !121

; <label>:24:                                     ; preds = %16
  store i32 -3, i32* %9, align 4, !dbg !122, !tbaa !98
  br label %110, !dbg !124

; <label>:25:                                     ; preds = %16
  call void @llvm.dbg.value(metadata i32 0, metadata !85, metadata !DIExpression()), !dbg !125
  %26 = sext i32 %0 to i64, !dbg !126
  br label %29, !dbg !126

; <label>:27:                                     ; preds = %29
  call void @llvm.dbg.value(metadata i32 undef, metadata !85, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !125
  %28 = icmp slt i64 %33, %26, !dbg !128
  br i1 %28, label %29, label %38, !dbg !126, !llvm.loop !130

; <label>:29:                                     ; preds = %25, %27
  %30 = phi i64 [ 0, %25 ], [ %33, %27 ]
  call void @llvm.dbg.value(metadata i64 %30, metadata !85, metadata !DIExpression()), !dbg !125
  %31 = getelementptr inbounds i32, i32* %1, i64 %30, !dbg !132
  %32 = load i32, i32* %31, align 4, !dbg !132, !tbaa !115
  %33 = add nuw nsw i64 %30, 1, !dbg !135
  %34 = getelementptr inbounds i32, i32* %1, i64 %33, !dbg !136
  %35 = load i32, i32* %34, align 4, !dbg !136, !tbaa !115
  %36 = icmp sgt i32 %32, %35, !dbg !137
  call void @llvm.dbg.value(metadata i32 undef, metadata !85, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !125
  br i1 %36, label %37, label %27, !dbg !138

; <label>:37:                                     ; preds = %29
  store i32 -3, i32* %9, align 4, !dbg !139, !tbaa !98
  br label %110, !dbg !141

; <label>:38:                                     ; preds = %27
  %39 = tail call i8* @klu_malloc(i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !142
  %40 = bitcast i8* %39 to i32*, !dbg !142
  call void @llvm.dbg.value(metadata i32* %40, metadata !79, metadata !DIExpression()), !dbg !143
  %41 = load i32, i32* %9, align 4, !dbg !144, !tbaa !98
  %42 = icmp slt i32 %41, 0, !dbg !146
  br i1 %42, label %43, label %44, !dbg !147

; <label>:43:                                     ; preds = %38
  store i32 -2, i32* %9, align 4, !dbg !148, !tbaa !98
  br label %110, !dbg !150

; <label>:44:                                     ; preds = %38
  call void @llvm.dbg.value(metadata i32 0, metadata !84, metadata !DIExpression()), !dbg !151
  %45 = icmp sgt i32 %0, 0, !dbg !152
  br i1 %45, label %46, label %82, !dbg !155

; <label>:46:                                     ; preds = %44
  %47 = zext i32 %0 to i64, !dbg !155
  %48 = shl nuw nsw i64 %47, 2, !dbg !155
  call void @llvm.memset.p0i8.i64(i8* %39, i8 -1, i64 %48, i32 4, i1 false), !dbg !156
  call void @llvm.dbg.value(metadata i32 0, metadata !85, metadata !DIExpression()), !dbg !125
  %49 = sext i32 %0 to i64, !dbg !158
  br label %50, !dbg !158

; <label>:50:                                     ; preds = %46, %80
  %51 = phi i64 [ 0, %46 ], [ %52, %80 ]
  call void @llvm.dbg.value(metadata i64 %51, metadata !85, metadata !DIExpression()), !dbg !125
  %52 = add nuw nsw i64 %51, 1, !dbg !160
  %53 = getelementptr inbounds i32, i32* %1, i64 %52, !dbg !163
  %54 = load i32, i32* %53, align 4, !dbg !163, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %54, metadata !87, metadata !DIExpression()), !dbg !164
  %55 = getelementptr inbounds i32, i32* %1, i64 %51, !dbg !165
  %56 = load i32, i32* %55, align 4, !dbg !165, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %56, metadata !86, metadata !DIExpression()), !dbg !167
  %57 = icmp slt i32 %56, %54, !dbg !168
  br i1 %57, label %58, label %80, !dbg !170

; <label>:58:                                     ; preds = %50
  %59 = sext i32 %56 to i64, !dbg !170
  %60 = sext i32 %54 to i64, !dbg !170
  %61 = trunc i64 %51 to i32
  br label %62, !dbg !170

; <label>:62:                                     ; preds = %58, %77
  %63 = phi i64 [ %59, %58 ], [ %78, %77 ]
  call void @llvm.dbg.value(metadata i64 %63, metadata !86, metadata !DIExpression()), !dbg !167
  %64 = getelementptr inbounds i32, i32* %2, i64 %63, !dbg !171
  %65 = load i32, i32* %64, align 4, !dbg !171, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %65, metadata !84, metadata !DIExpression()), !dbg !151
  %66 = icmp sgt i32 %65, -1, !dbg !173
  %67 = icmp slt i32 %65, %0, !dbg !175
  %68 = and i1 %66, %67, !dbg !176
  br i1 %68, label %69, label %75, !dbg !176

; <label>:69:                                     ; preds = %62
  %70 = sext i32 %65 to i64, !dbg !177
  %71 = getelementptr inbounds i32, i32* %40, i64 %70, !dbg !177
  %72 = load i32, i32* %71, align 4, !dbg !177, !tbaa !115
  %73 = zext i32 %72 to i64, !dbg !178
  %74 = icmp eq i64 %51, %73, !dbg !178
  br i1 %74, label %75, label %77, !dbg !179

; <label>:75:                                     ; preds = %62, %69
  %76 = tail call i8* @klu_free(i8* %39, i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !180
  store i32 -3, i32* %9, align 4, !dbg !182, !tbaa !98
  br label %110, !dbg !183

; <label>:77:                                     ; preds = %69
  store i32 %61, i32* %71, align 4, !dbg !184, !tbaa !115
  %78 = add nsw i64 %63, 1, !dbg !185
  call void @llvm.dbg.value(metadata i32 undef, metadata !86, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !167
  %79 = icmp slt i64 %78, %60, !dbg !168
  br i1 %79, label %62, label %80, !dbg !170, !llvm.loop !186

; <label>:80:                                     ; preds = %77, %50
  call void @llvm.dbg.value(metadata i32 undef, metadata !85, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !125
  %81 = icmp slt i64 %52, %49, !dbg !188
  br i1 %81, label %50, label %82, !dbg !158, !llvm.loop !189

; <label>:82:                                     ; preds = %80, %44
  %83 = tail call i8* @klu_malloc(i64 1, i64 96, %struct.klu_common_struct* nonnull %3) #4, !dbg !191
  call void @llvm.dbg.value(metadata i8* %83, metadata !78, metadata !DIExpression()), !dbg !192
  %84 = bitcast %struct.klu_symbolic** %5 to i8**, !dbg !193
  store i8* %83, i8** %84, align 8, !dbg !193, !tbaa !194
  %85 = load i32, i32* %9, align 4, !dbg !195, !tbaa !98
  %86 = icmp slt i32 %85, 0, !dbg !197
  br i1 %86, label %87, label %89, !dbg !198

; <label>:87:                                     ; preds = %82
  %88 = tail call i8* @klu_free(i8* %39, i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !199
  store i32 -2, i32* %9, align 4, !dbg !201, !tbaa !98
  br label %110, !dbg !202

; <label>:89:                                     ; preds = %82
  %90 = tail call i8* @klu_malloc(i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !203
  call void @llvm.dbg.value(metadata i8* %90, metadata !80, metadata !DIExpression()), !dbg !204
  %91 = add nsw i32 %0, 1, !dbg !205
  %92 = sext i32 %91 to i64, !dbg !206
  %93 = tail call i8* @klu_malloc(i64 %92, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !207
  call void @llvm.dbg.value(metadata i8* %93, metadata !81, metadata !DIExpression()), !dbg !208
  %94 = tail call i8* @klu_malloc(i64 %17, i64 8, %struct.klu_common_struct* nonnull %3) #4, !dbg !209
  call void @llvm.dbg.value(metadata i8* %94, metadata !82, metadata !DIExpression()), !dbg !210
  %95 = load %struct.klu_symbolic*, %struct.klu_symbolic** %5, align 8, !dbg !211, !tbaa !194
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %96 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 5, !dbg !212
  store i32 %0, i32* %96, align 8, !dbg !213, !tbaa !214
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %97 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 6, !dbg !216
  store i32 %19, i32* %97, align 4, !dbg !217, !tbaa !218
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %98 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 7, !dbg !219
  %99 = bitcast i32** %98 to i8**, !dbg !220
  store i8* %39, i8** %99, align 8, !dbg !220, !tbaa !221
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %100 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 8, !dbg !222
  %101 = bitcast i32** %100 to i8**, !dbg !223
  store i8* %90, i8** %101, align 8, !dbg !223, !tbaa !224
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %102 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 9, !dbg !225
  %103 = bitcast i32** %102 to i8**, !dbg !226
  store i8* %93, i8** %103, align 8, !dbg !226, !tbaa !227
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %95, metadata !78, metadata !DIExpression()), !dbg !192
  %104 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %95, i64 0, i32 4, !dbg !228
  %105 = bitcast double** %104 to i8**, !dbg !229
  store i8* %94, i8** %105, align 8, !dbg !229, !tbaa !230
  %106 = load i32, i32* %9, align 4, !dbg !231, !tbaa !98
  %107 = icmp slt i32 %106, 0, !dbg !233
  br i1 %107, label %108, label %110, !dbg !234

; <label>:108:                                    ; preds = %89
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %5, metadata !78, metadata !DIExpression()), !dbg !192
  %109 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %5, %struct.klu_common_struct* nonnull %3) #4, !dbg !235
  store i32 -2, i32* %9, align 4, !dbg !237, !tbaa !98
  br label %110, !dbg !238

; <label>:110:                                    ; preds = %89, %4, %108, %87, %75, %43, %37, %24, %15
  %111 = phi %struct.klu_symbolic* [ null, %15 ], [ null, %24 ], [ null, %37 ], [ null, %43 ], [ null, %75 ], [ null, %87 ], [ null, %108 ], [ null, %4 ], [ %95, %89 ], !dbg !239
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %6) #4, !dbg !240
  ret %struct.klu_symbolic* %111, !dbg !240
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_analyze_given(i32, i32*, i32*, i32* readonly, i32* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !241 {
  %7 = alloca %struct.klu_symbolic*, align 8
  call void @llvm.dbg.value(metadata i32 %0, metadata !245, metadata !DIExpression()), !dbg !274
  call void @llvm.dbg.value(metadata i32* %1, metadata !246, metadata !DIExpression()), !dbg !275
  call void @llvm.dbg.value(metadata i32* %2, metadata !247, metadata !DIExpression()), !dbg !276
  call void @llvm.dbg.value(metadata i32* %3, metadata !248, metadata !DIExpression()), !dbg !277
  call void @llvm.dbg.value(metadata i32* %4, metadata !249, metadata !DIExpression()), !dbg !278
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %5, metadata !250, metadata !DIExpression()), !dbg !279
  %8 = bitcast %struct.klu_symbolic** %7 to i8*, !dbg !280
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %8) #4, !dbg !280
  %9 = tail call %struct.klu_symbolic* @klu_alloc_symbolic(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* %5), !dbg !281
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %9, metadata !251, metadata !DIExpression()), !dbg !282
  store %struct.klu_symbolic* %9, %struct.klu_symbolic** %7, align 8, !dbg !283, !tbaa !194
  %10 = icmp eq %struct.klu_symbolic* %9, null, !dbg !284
  br i1 %10, label %245, label %11, !dbg !286

; <label>:11:                                     ; preds = %6
  %12 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 7, !dbg !287
  %13 = load i32*, i32** %12, align 8, !dbg !287, !tbaa !221
  call void @llvm.dbg.value(metadata i32* %13, metadata !257, metadata !DIExpression()), !dbg !288
  %14 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 8, !dbg !289
  %15 = load i32*, i32** %14, align 8, !dbg !289, !tbaa !224
  call void @llvm.dbg.value(metadata i32* %15, metadata !258, metadata !DIExpression()), !dbg !290
  %16 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 9, !dbg !291
  %17 = load i32*, i32** %16, align 8, !dbg !291, !tbaa !227
  call void @llvm.dbg.value(metadata i32* %17, metadata !259, metadata !DIExpression()), !dbg !292
  %18 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 4, !dbg !293
  %19 = load double*, double** %18, align 8, !dbg !293, !tbaa !230
  call void @llvm.dbg.value(metadata double* %19, metadata !252, metadata !DIExpression()), !dbg !294
  %20 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 6, !dbg !295
  %21 = load i32, i32* %20, align 4, !dbg !295, !tbaa !218
  call void @llvm.dbg.value(metadata i32 %21, metadata !254, metadata !DIExpression()), !dbg !296
  %22 = icmp eq i32* %4, null, !dbg !297
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %23 = icmp sgt i32 %0, 0, !dbg !300
  br i1 %22, label %24, label %33, !dbg !304

; <label>:24:                                     ; preds = %11
  br i1 %23, label %25, label %43, !dbg !305

; <label>:25:                                     ; preds = %24
  %26 = zext i32 %0 to i64
  br label %27, !dbg !305

; <label>:27:                                     ; preds = %27, %25
  %28 = phi i64 [ 0, %25 ], [ %31, %27 ]
  call void @llvm.dbg.value(metadata i64 %28, metadata !264, metadata !DIExpression()), !dbg !299
  %29 = getelementptr inbounds i32, i32* %15, i64 %28, !dbg !308
  %30 = trunc i64 %28 to i32, !dbg !311
  store i32 %30, i32* %29, align 4, !dbg !311, !tbaa !115
  %31 = add nuw nsw i64 %28, 1, !dbg !312
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %32 = icmp eq i64 %31, %26, !dbg !313
  br i1 %32, label %43, label %27, !dbg !305, !llvm.loop !314

; <label>:33:                                     ; preds = %11
  br i1 %23, label %34, label %43, !dbg !316

; <label>:34:                                     ; preds = %33
  %35 = zext i32 %0 to i64
  br label %36, !dbg !316

; <label>:36:                                     ; preds = %36, %34
  %37 = phi i64 [ 0, %34 ], [ %41, %36 ]
  call void @llvm.dbg.value(metadata i64 %37, metadata !264, metadata !DIExpression()), !dbg !299
  %38 = getelementptr inbounds i32, i32* %4, i64 %37, !dbg !317
  %39 = load i32, i32* %38, align 4, !dbg !317, !tbaa !115
  %40 = getelementptr inbounds i32, i32* %15, i64 %37, !dbg !319
  store i32 %39, i32* %40, align 4, !dbg !320, !tbaa !115
  %41 = add nuw nsw i64 %37, 1, !dbg !321
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %42 = icmp eq i64 %41, %35, !dbg !322
  br i1 %42, label %43, label %36, !dbg !316, !llvm.loop !323

; <label>:43:                                     ; preds = %36, %27, %33, %24
  %44 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 5, !dbg !325
  %45 = load i32, i32* %44, align 8, !dbg !325, !tbaa !326
  call void @llvm.dbg.value(metadata i32 %45, metadata !263, metadata !DIExpression()), !dbg !327
  %46 = icmp ne i32 %45, 0, !dbg !328
  %47 = zext i1 %46 to i32, !dbg !328
  call void @llvm.dbg.value(metadata i32 %47, metadata !263, metadata !DIExpression()), !dbg !327
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %9, metadata !251, metadata !DIExpression()), !dbg !282
  %48 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 13, !dbg !329
  store i32 2, i32* %48, align 4, !dbg !330, !tbaa !331
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %9, metadata !251, metadata !DIExpression()), !dbg !282
  %49 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 14, !dbg !332
  store i32 %47, i32* %49, align 8, !dbg !333, !tbaa !334
  br i1 %46, label %50, label %218, !dbg !335

; <label>:50:                                     ; preds = %43
  %51 = shl nsw i32 %0, 2, !dbg !336
  %52 = sext i32 %51 to i64, !dbg !337
  %53 = tail call i8* @klu_malloc(i64 %52, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !338
  %54 = bitcast i8* %53 to i32*, !dbg !338
  call void @llvm.dbg.value(metadata i32* %54, metadata !268, metadata !DIExpression()), !dbg !339
  %55 = sext i32 %0 to i64, !dbg !340
  %56 = tail call i8* @klu_malloc(i64 %55, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !341
  %57 = bitcast i8* %56 to i32*, !dbg !341
  call void @llvm.dbg.value(metadata i32* %57, metadata !265, metadata !DIExpression()), !dbg !342
  %58 = icmp ne i32* %3, null, !dbg !343
  br i1 %58, label %59, label %64, !dbg !345

; <label>:59:                                     ; preds = %50
  %60 = add nsw i32 %21, 1, !dbg !346
  %61 = sext i32 %60 to i64, !dbg !348
  %62 = tail call i8* @klu_malloc(i64 %61, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !349
  %63 = bitcast i8* %62 to i32*, !dbg !349
  call void @llvm.dbg.value(metadata i32* %63, metadata !269, metadata !DIExpression()), !dbg !350
  br label %64, !dbg !351

; <label>:64:                                     ; preds = %50, %59
  %65 = phi i32* [ %63, %59 ], [ %2, %50 ], !dbg !352
  call void @llvm.dbg.value(metadata i32* %65, metadata !269, metadata !DIExpression()), !dbg !350
  %66 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11, !dbg !354
  %67 = load i32, i32* %66, align 4, !dbg !354, !tbaa !98
  %68 = icmp slt i32 %67, 0, !dbg !356
  br i1 %68, label %69, label %77, !dbg !357

; <label>:69:                                     ; preds = %64
  %70 = tail call i8* @klu_free(i8* %53, i64 %52, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !358
  %71 = tail call i8* @klu_free(i8* %56, i64 %55, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !360
  br i1 %58, label %72, label %216, !dbg !361

; <label>:72:                                     ; preds = %69
  %73 = bitcast i32* %65 to i8*, !dbg !362
  %74 = add nsw i32 %21, 1, !dbg !365
  %75 = sext i32 %74 to i64, !dbg !366
  %76 = tail call i8* @klu_free(i8* %73, i64 %75, i64 4, %struct.klu_common_struct* nonnull %5) #4, !dbg !367
  br label %216, !dbg !368

; <label>:77:                                     ; preds = %64
  br i1 %58, label %80, label %78, !dbg !369

; <label>:78:                                     ; preds = %77
  %79 = tail call i32 @btf_strongcomp(i32 %0, i32* %1, i32* %65, i32* %15, i32* %13, i32* %17, i32* %54) #4, !dbg !370
  call void @llvm.dbg.value(metadata i32 %108, metadata !253, metadata !DIExpression()), !dbg !371
  br label %134, !dbg !372

; <label>:80:                                     ; preds = %77
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %81 = icmp sgt i32 %0, 0, !dbg !373
  br i1 %81, label %82, label %93, !dbg !378

; <label>:82:                                     ; preds = %80
  %83 = zext i32 %0 to i64
  br label %84, !dbg !378

; <label>:84:                                     ; preds = %84, %82
  %85 = phi i64 [ 0, %82 ], [ %91, %84 ]
  call void @llvm.dbg.value(metadata i64 %85, metadata !264, metadata !DIExpression()), !dbg !299
  %86 = getelementptr inbounds i32, i32* %3, i64 %85, !dbg !379
  %87 = load i32, i32* %86, align 4, !dbg !379, !tbaa !115
  %88 = sext i32 %87 to i64, !dbg !381
  %89 = getelementptr inbounds i32, i32* %57, i64 %88, !dbg !381
  %90 = trunc i64 %85 to i32, !dbg !382
  store i32 %90, i32* %89, align 4, !dbg !382, !tbaa !115
  %91 = add nuw nsw i64 %85, 1, !dbg !383
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %92 = icmp eq i64 %91, %83, !dbg !373
  br i1 %92, label %93, label %84, !dbg !378, !llvm.loop !384

; <label>:93:                                     ; preds = %84, %80
  call void @llvm.dbg.value(metadata i32 0, metadata !261, metadata !DIExpression()), !dbg !386
  %94 = icmp sgt i32 %21, 0, !dbg !387
  br i1 %94, label %95, label %107, !dbg !390

; <label>:95:                                     ; preds = %93
  %96 = zext i32 %21 to i64
  br label %97, !dbg !390

; <label>:97:                                     ; preds = %97, %95
  %98 = phi i64 [ 0, %95 ], [ %105, %97 ]
  call void @llvm.dbg.value(metadata i64 %98, metadata !261, metadata !DIExpression()), !dbg !386
  %99 = getelementptr inbounds i32, i32* %2, i64 %98, !dbg !391
  %100 = load i32, i32* %99, align 4, !dbg !391, !tbaa !115
  %101 = sext i32 %100 to i64, !dbg !393
  %102 = getelementptr inbounds i32, i32* %57, i64 %101, !dbg !393
  %103 = load i32, i32* %102, align 4, !dbg !393, !tbaa !115
  %104 = getelementptr inbounds i32, i32* %65, i64 %98, !dbg !394
  store i32 %103, i32* %104, align 4, !dbg !395, !tbaa !115
  %105 = add nuw nsw i64 %98, 1, !dbg !396
  call void @llvm.dbg.value(metadata i32 undef, metadata !261, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !386
  %106 = icmp eq i64 %105, %96, !dbg !387
  br i1 %106, label %107, label %97, !dbg !390, !llvm.loop !397

; <label>:107:                                    ; preds = %97, %93
  %108 = tail call i32 @btf_strongcomp(i32 %0, i32* %1, i32* %65, i32* %15, i32* %13, i32* %17, i32* %54) #4, !dbg !370
  call void @llvm.dbg.value(metadata i32 %108, metadata !253, metadata !DIExpression()), !dbg !371
  br i1 %58, label %109, label %134, !dbg !372

; <label>:109:                                    ; preds = %107
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %110 = icmp sgt i32 %0, 0, !dbg !399
  br i1 %110, label %111, label %148, !dbg !404

; <label>:111:                                    ; preds = %109
  %112 = zext i32 %0 to i64
  br label %113, !dbg !404

; <label>:113:                                    ; preds = %113, %111
  %114 = phi i64 [ 0, %111 ], [ %121, %113 ]
  call void @llvm.dbg.value(metadata i64 %114, metadata !264, metadata !DIExpression()), !dbg !299
  %115 = getelementptr inbounds i32, i32* %13, i64 %114, !dbg !405
  %116 = load i32, i32* %115, align 4, !dbg !405, !tbaa !115
  %117 = sext i32 %116 to i64, !dbg !407
  %118 = getelementptr inbounds i32, i32* %3, i64 %117, !dbg !407
  %119 = load i32, i32* %118, align 4, !dbg !407, !tbaa !115
  %120 = getelementptr inbounds i32, i32* %54, i64 %114, !dbg !408
  store i32 %119, i32* %120, align 4, !dbg !409, !tbaa !115
  %121 = add nuw nsw i64 %114, 1, !dbg !410
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %122 = icmp eq i64 %121, %112, !dbg !399
  br i1 %122, label %123, label %113, !dbg !404, !llvm.loop !411

; <label>:123:                                    ; preds = %113
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %124 = icmp sgt i32 %0, 0, !dbg !413
  br i1 %124, label %125, label %148, !dbg !416

; <label>:125:                                    ; preds = %123
  %126 = zext i32 %0 to i64
  br label %127, !dbg !416

; <label>:127:                                    ; preds = %127, %125
  %128 = phi i64 [ 0, %125 ], [ %132, %127 ]
  call void @llvm.dbg.value(metadata i64 %128, metadata !264, metadata !DIExpression()), !dbg !299
  %129 = getelementptr inbounds i32, i32* %54, i64 %128, !dbg !417
  %130 = load i32, i32* %129, align 4, !dbg !417, !tbaa !115
  %131 = getelementptr inbounds i32, i32* %13, i64 %128, !dbg !419
  store i32 %130, i32* %131, align 4, !dbg !420, !tbaa !115
  %132 = add nuw nsw i64 %128, 1, !dbg !421
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %133 = icmp eq i64 %132, %126, !dbg !413
  br i1 %133, label %134, label %127, !dbg !416, !llvm.loop !422

; <label>:134:                                    ; preds = %127, %78, %107
  %135 = phi i32 [ %79, %78 ], [ %108, %107 ], [ %108, %127 ]
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %136 = icmp sgt i32 %0, 0, !dbg !424
  br i1 %136, label %137, label %148, !dbg !427

; <label>:137:                                    ; preds = %134
  %138 = zext i32 %0 to i64
  br label %139, !dbg !427

; <label>:139:                                    ; preds = %139, %137
  %140 = phi i64 [ 0, %137 ], [ %146, %139 ]
  call void @llvm.dbg.value(metadata i64 %140, metadata !264, metadata !DIExpression()), !dbg !299
  %141 = getelementptr inbounds i32, i32* %13, i64 %140, !dbg !428
  %142 = load i32, i32* %141, align 4, !dbg !428, !tbaa !115
  %143 = sext i32 %142 to i64, !dbg !430
  %144 = getelementptr inbounds i32, i32* %57, i64 %143, !dbg !430
  %145 = trunc i64 %140 to i32, !dbg !431
  store i32 %145, i32* %144, align 4, !dbg !431, !tbaa !115
  %146 = add nuw nsw i64 %140, 1, !dbg !432
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %147 = icmp eq i64 %146, %138, !dbg !424
  br i1 %147, label %148, label %139, !dbg !427, !llvm.loop !433

; <label>:148:                                    ; preds = %139, %109, %123, %134
  %149 = phi i32 [ %135, %134 ], [ %108, %123 ], [ %108, %109 ], [ %135, %139 ]
  call void @llvm.dbg.value(metadata i32 0, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i32 1, metadata !256, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 0, metadata !255, metadata !DIExpression()), !dbg !437
  %150 = icmp sgt i32 %149, 0, !dbg !438
  br i1 %150, label %151, label %206, !dbg !441

; <label>:151:                                    ; preds = %148
  %152 = zext i32 %149 to i64
  br label %153, !dbg !441

; <label>:153:                                    ; preds = %202, %151
  %154 = phi i64 [ 0, %151 ], [ %159, %202 ]
  %155 = phi i32 [ 1, %151 ], [ %164, %202 ]
  %156 = phi i32 [ 0, %151 ], [ %203, %202 ]
  call void @llvm.dbg.value(metadata i64 %154, metadata !255, metadata !DIExpression()), !dbg !437
  call void @llvm.dbg.value(metadata i32 %155, metadata !256, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 %156, metadata !260, metadata !DIExpression()), !dbg !435
  %157 = getelementptr inbounds i32, i32* %17, i64 %154, !dbg !442
  %158 = load i32, i32* %157, align 4, !dbg !442, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %158, metadata !270, metadata !DIExpression()), !dbg !444
  %159 = add nuw nsw i64 %154, 1, !dbg !445
  %160 = getelementptr inbounds i32, i32* %17, i64 %159, !dbg !446
  %161 = load i32, i32* %160, align 4, !dbg !446, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %161, metadata !271, metadata !DIExpression()), !dbg !447
  %162 = sub nsw i32 %161, %158, !dbg !448
  call void @llvm.dbg.value(metadata i32 %162, metadata !272, metadata !DIExpression()), !dbg !449
  %163 = icmp sgt i32 %155, %162, !dbg !450
  %164 = select i1 %163, i32 %155, i32 %162, !dbg !450
  call void @llvm.dbg.value(metadata i32 %158, metadata !264, metadata !DIExpression()), !dbg !299
  call void @llvm.dbg.value(metadata i32 %156, metadata !260, metadata !DIExpression()), !dbg !435
  %165 = icmp sgt i32 %161, %158, !dbg !451
  br i1 %165, label %166, label %202, !dbg !454

; <label>:166:                                    ; preds = %153
  %167 = sext i32 %158 to i64, !dbg !454
  %168 = sext i32 %161 to i64
  br label %169, !dbg !454

; <label>:169:                                    ; preds = %198, %166
  %170 = phi i64 [ %167, %166 ], [ %200, %198 ]
  %171 = phi i32 [ %156, %166 ], [ %199, %198 ]
  call void @llvm.dbg.value(metadata i32 %171, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i64 %170, metadata !264, metadata !DIExpression()), !dbg !299
  %172 = getelementptr inbounds i32, i32* %15, i64 %170, !dbg !455
  %173 = load i32, i32* %172, align 4, !dbg !455, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %173, metadata !273, metadata !DIExpression()), !dbg !457
  %174 = add nsw i32 %173, 1, !dbg !458
  %175 = sext i32 %174 to i64, !dbg !459
  %176 = getelementptr inbounds i32, i32* %1, i64 %175, !dbg !459
  %177 = load i32, i32* %176, align 4, !dbg !459, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %177, metadata !262, metadata !DIExpression()), !dbg !460
  %178 = sext i32 %173 to i64, !dbg !461
  %179 = getelementptr inbounds i32, i32* %1, i64 %178, !dbg !461
  %180 = load i32, i32* %179, align 4, !dbg !461, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %180, metadata !261, metadata !DIExpression()), !dbg !386
  call void @llvm.dbg.value(metadata i32 %171, metadata !260, metadata !DIExpression()), !dbg !435
  %181 = icmp slt i32 %180, %177, !dbg !463
  br i1 %181, label %182, label %198, !dbg !465

; <label>:182:                                    ; preds = %169
  %183 = sext i32 %180 to i64, !dbg !465
  %184 = sext i32 %177 to i64
  br label %185, !dbg !465

; <label>:185:                                    ; preds = %185, %182
  %186 = phi i64 [ %183, %182 ], [ %196, %185 ]
  %187 = phi i32 [ %171, %182 ], [ %195, %185 ]
  call void @llvm.dbg.value(metadata i32 %187, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i64 %186, metadata !261, metadata !DIExpression()), !dbg !386
  %188 = getelementptr inbounds i32, i32* %2, i64 %186, !dbg !466
  %189 = load i32, i32* %188, align 4, !dbg !466, !tbaa !115
  %190 = sext i32 %189 to i64, !dbg !469
  %191 = getelementptr inbounds i32, i32* %57, i64 %190, !dbg !469
  %192 = load i32, i32* %191, align 4, !dbg !469, !tbaa !115
  %193 = icmp slt i32 %192, %158, !dbg !470
  %194 = zext i1 %193 to i32, !dbg !471
  %195 = add nsw i32 %187, %194, !dbg !471
  %196 = add nsw i64 %186, 1, !dbg !472
  call void @llvm.dbg.value(metadata i32 %195, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i32 undef, metadata !261, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !386
  %197 = icmp eq i64 %196, %184, !dbg !463
  br i1 %197, label %198, label %185, !dbg !465, !llvm.loop !473

; <label>:198:                                    ; preds = %185, %169
  %199 = phi i32 [ %171, %169 ], [ %195, %185 ]
  %200 = add nsw i64 %170, 1, !dbg !475
  call void @llvm.dbg.value(metadata i32 %199, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %201 = icmp eq i64 %200, %168, !dbg !451
  br i1 %201, label %202, label %169, !dbg !454, !llvm.loop !476

; <label>:202:                                    ; preds = %198, %153
  %203 = phi i32 [ %156, %153 ], [ %199, %198 ]
  %204 = getelementptr inbounds double, double* %19, i64 %154, !dbg !478
  store double -1.000000e+00, double* %204, align 8, !dbg !479, !tbaa !480
  call void @llvm.dbg.value(metadata i32 undef, metadata !255, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !437
  call void @llvm.dbg.value(metadata i32 %164, metadata !256, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 %203, metadata !260, metadata !DIExpression()), !dbg !435
  %205 = icmp eq i64 %159, %152, !dbg !438
  br i1 %205, label %206, label %153, !dbg !441, !llvm.loop !481

; <label>:206:                                    ; preds = %202, %148
  %207 = phi i32 [ 0, %148 ], [ %203, %202 ]
  %208 = phi i32 [ 1, %148 ], [ %164, %202 ]
  call void @llvm.dbg.value(metadata i32 %207, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i32 %208, metadata !256, metadata !DIExpression()), !dbg !436
  %209 = tail call i8* @klu_free(i8* %53, i64 %52, i64 4, %struct.klu_common_struct* %5) #4, !dbg !483
  %210 = tail call i8* @klu_free(i8* %56, i64 %55, i64 4, %struct.klu_common_struct* %5) #4, !dbg !484
  br i1 %58, label %211, label %235, !dbg !485

; <label>:211:                                    ; preds = %206
  %212 = bitcast i32* %65 to i8*, !dbg !486
  %213 = add nsw i32 %21, 1, !dbg !489
  %214 = sext i32 %213 to i64, !dbg !490
  %215 = tail call i8* @klu_free(i8* %212, i64 %214, i64 4, %struct.klu_common_struct* %5) #4, !dbg !491
  br label %235, !dbg !492

; <label>:216:                                    ; preds = %69, %72
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %7, metadata !251, metadata !DIExpression()), !dbg !282
  %217 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %7, %struct.klu_common_struct* nonnull %5) #4, !dbg !493
  store i32 -2, i32* %66, align 4, !dbg !494, !tbaa !98
  call void @llvm.dbg.value(metadata i32 undef, metadata !253, metadata !DIExpression()), !dbg !371
  call void @llvm.dbg.value(metadata i32 undef, metadata !256, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 undef, metadata !260, metadata !DIExpression()), !dbg !435
  br label %245

; <label>:218:                                    ; preds = %43
  call void @llvm.dbg.value(metadata i32 0, metadata !260, metadata !DIExpression()), !dbg !435
  call void @llvm.dbg.value(metadata i32 1, metadata !253, metadata !DIExpression()), !dbg !371
  call void @llvm.dbg.value(metadata i32 %0, metadata !256, metadata !DIExpression()), !dbg !436
  store i32 0, i32* %17, align 4, !dbg !495, !tbaa !115
  %219 = getelementptr inbounds i32, i32* %17, i64 1, !dbg !497
  store i32 %0, i32* %219, align 4, !dbg !498, !tbaa !115
  store double -1.000000e+00, double* %19, align 8, !dbg !499, !tbaa !480
  call void @llvm.dbg.value(metadata i32 0, metadata !264, metadata !DIExpression()), !dbg !299
  %220 = icmp sgt i32 %0, 0, !dbg !500
  br i1 %220, label %221, label %235, !dbg !503

; <label>:221:                                    ; preds = %218
  %222 = icmp eq i32* %3, null
  %223 = zext i32 %0 to i64
  br label %224, !dbg !503

; <label>:224:                                    ; preds = %230, %221
  %225 = phi i64 [ 0, %221 ], [ %233, %230 ]
  call void @llvm.dbg.value(metadata i64 %225, metadata !264, metadata !DIExpression()), !dbg !299
  %226 = trunc i64 %225 to i32, !dbg !504
  br i1 %222, label %230, label %227, !dbg !504

; <label>:227:                                    ; preds = %224
  %228 = getelementptr inbounds i32, i32* %3, i64 %225, !dbg !506
  %229 = load i32, i32* %228, align 4, !dbg !506, !tbaa !115
  br label %230, !dbg !504

; <label>:230:                                    ; preds = %224, %227
  %231 = phi i32 [ %229, %227 ], [ %226, %224 ], !dbg !504
  %232 = getelementptr inbounds i32, i32* %13, i64 %225, !dbg !507
  store i32 %231, i32* %232, align 4, !dbg !508, !tbaa !115
  %233 = add nuw nsw i64 %225, 1, !dbg !509
  call void @llvm.dbg.value(metadata i32 undef, metadata !264, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !299
  %234 = icmp eq i64 %233, %223, !dbg !500
  br i1 %234, label %235, label %224, !dbg !503, !llvm.loop !510

; <label>:235:                                    ; preds = %230, %218, %206, %211
  %236 = phi i32 [ %207, %211 ], [ %207, %206 ], [ 0, %218 ], [ 0, %230 ]
  %237 = phi i32 [ %208, %211 ], [ %208, %206 ], [ %0, %218 ], [ %0, %230 ]
  %238 = phi i32 [ %149, %211 ], [ %149, %206 ], [ 1, %218 ], [ 1, %230 ]
  call void @llvm.dbg.value(metadata i32 %238, metadata !253, metadata !DIExpression()), !dbg !371
  call void @llvm.dbg.value(metadata i32 %237, metadata !256, metadata !DIExpression()), !dbg !436
  call void @llvm.dbg.value(metadata i32 %236, metadata !260, metadata !DIExpression()), !dbg !435
  %239 = load %struct.klu_symbolic*, %struct.klu_symbolic** %7, align 8, !dbg !512, !tbaa !194
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  %240 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %239, i64 0, i32 11, !dbg !513
  store i32 %238, i32* %240, align 4, !dbg !514, !tbaa !515
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  %241 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %239, i64 0, i32 12, !dbg !516
  store i32 %237, i32* %241, align 8, !dbg !517, !tbaa !518
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  %242 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %239, i64 0, i32 2, !dbg !519
  store double -1.000000e+00, double* %242, align 8, !dbg !520, !tbaa !521
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  %243 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %239, i64 0, i32 3, !dbg !522
  store double -1.000000e+00, double* %243, align 8, !dbg !523, !tbaa !524
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  %244 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %239, i64 0, i32 10, !dbg !525
  store i32 %236, i32* %244, align 8, !dbg !526, !tbaa !527
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %239, metadata !251, metadata !DIExpression()), !dbg !282
  br label %245, !dbg !528

; <label>:245:                                    ; preds = %216, %6, %235
  %246 = phi %struct.klu_symbolic* [ %239, %235 ], [ null, %216 ], [ null, %6 ], !dbg !529
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %8) #4, !dbg !530
  ret %struct.klu_symbolic* %246, !dbg !530
}

declare i32 @btf_strongcomp(i32, i32*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #2

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #3

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind readnone speculatable }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!7, !8, !9, !10}
!llvm.ident = !{!11}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_analyze_given.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!7 = !{i32 2, !"Dwarf Version", i32 4}
!8 = !{i32 2, !"Debug Info Version", i32 3}
!9 = !{i32 1, !"wchar_size", i32 4}
!10 = !{i32 7, !"PIC Level", i32 2}
!11 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!12 = distinct !DISubprogram(name: "klu_alloc_symbolic", scope: !1, file: !1, line: 17, type: !13, isLocal: false, isDefinition: true, scopeLine: 24, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !73)
!13 = !DISubroutineType(types: !14)
!14 = !{!15, !6, !5, !5, !38}
!15 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !16, size: 64)
!16 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_symbolic", file: !17, line: 54, baseType: !18)
!17 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!18 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !17, line: 23, size: 768, elements: !19)
!19 = !{!20, !22, !23, !24, !25, !27, !28, !29, !30, !31, !32, !33, !34, !35, !36, !37}
!20 = !DIDerivedType(tag: DW_TAG_member, name: "symmetry", scope: !18, file: !17, line: 31, baseType: !21, size: 64)
!21 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!22 = !DIDerivedType(tag: DW_TAG_member, name: "est_flops", scope: !18, file: !17, line: 32, baseType: !21, size: 64, offset: 64)
!23 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !18, file: !17, line: 33, baseType: !21, size: 64, offset: 128)
!24 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !18, file: !17, line: 33, baseType: !21, size: 64, offset: 192)
!25 = !DIDerivedType(tag: DW_TAG_member, name: "Lnz", scope: !18, file: !17, line: 34, baseType: !26, size: 64, offset: 256)
!26 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !21, size: 64)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !18, file: !17, line: 38, baseType: !6, size: 32, offset: 320)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "nz", scope: !18, file: !17, line: 39, baseType: !6, size: 32, offset: 352)
!29 = !DIDerivedType(tag: DW_TAG_member, name: "P", scope: !18, file: !17, line: 40, baseType: !5, size: 64, offset: 384)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "Q", scope: !18, file: !17, line: 41, baseType: !5, size: 64, offset: 448)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "R", scope: !18, file: !17, line: 42, baseType: !5, size: 64, offset: 512)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !18, file: !17, line: 43, baseType: !6, size: 32, offset: 576)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !18, file: !17, line: 44, baseType: !6, size: 32, offset: 608)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "maxblock", scope: !18, file: !17, line: 45, baseType: !6, size: 32, offset: 640)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !18, file: !17, line: 46, baseType: !6, size: 32, offset: 672)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "do_btf", scope: !18, file: !17, line: 47, baseType: !6, size: 32, offset: 704)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !18, file: !17, line: 50, baseType: !6, size: 32, offset: 736)
!38 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !39, size: 64)
!39 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !17, line: 207, baseType: !40)
!40 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !17, line: 137, size: 1280, elements: !41)
!41 = !{!42, !43, !44, !45, !46, !47, !48, !49, !50, !55, !56, !57, !58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68, !72}
!42 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !40, file: !17, line: 144, baseType: !21, size: 64)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !40, file: !17, line: 145, baseType: !21, size: 64, offset: 64)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !40, file: !17, line: 146, baseType: !21, size: 64, offset: 128)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !40, file: !17, line: 147, baseType: !21, size: 64, offset: 192)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !40, file: !17, line: 148, baseType: !21, size: 64, offset: 256)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !40, file: !17, line: 150, baseType: !6, size: 32, offset: 320)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !40, file: !17, line: 151, baseType: !6, size: 32, offset: 352)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !40, file: !17, line: 153, baseType: !6, size: 32, offset: 384)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !40, file: !17, line: 157, baseType: !51, size: 64, offset: 448)
!51 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !52, size: 64)
!52 = !DISubroutineType(types: !53)
!53 = !{!6, !6, !5, !5, !5, !54}
!54 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !40, size: 64)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !40, file: !17, line: 162, baseType: !4, size: 64, offset: 512)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !40, file: !17, line: 164, baseType: !6, size: 32, offset: 576)
!57 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !40, file: !17, line: 177, baseType: !6, size: 32, offset: 608)
!58 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !40, file: !17, line: 178, baseType: !6, size: 32, offset: 640)
!59 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !40, file: !17, line: 180, baseType: !6, size: 32, offset: 672)
!60 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !40, file: !17, line: 185, baseType: !6, size: 32, offset: 704)
!61 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !40, file: !17, line: 191, baseType: !6, size: 32, offset: 736)
!62 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !40, file: !17, line: 196, baseType: !6, size: 32, offset: 768)
!63 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !40, file: !17, line: 198, baseType: !21, size: 64, offset: 832)
!64 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !40, file: !17, line: 199, baseType: !21, size: 64, offset: 896)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !40, file: !17, line: 200, baseType: !21, size: 64, offset: 960)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !40, file: !17, line: 201, baseType: !21, size: 64, offset: 1024)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !40, file: !17, line: 202, baseType: !21, size: 64, offset: 1088)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !40, file: !17, line: 204, baseType: !69, size: 64, offset: 1152)
!69 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !70, line: 62, baseType: !71)
!70 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!71 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!72 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !40, file: !17, line: 205, baseType: !69, size: 64, offset: 1216)
!73 = !{!74, !75, !76, !77, !78, !79, !80, !81, !82, !83, !84, !85, !86, !87}
!74 = !DILocalVariable(name: "n", arg: 1, scope: !12, file: !1, line: 19, type: !6)
!75 = !DILocalVariable(name: "Ap", arg: 2, scope: !12, file: !1, line: 20, type: !5)
!76 = !DILocalVariable(name: "Ai", arg: 3, scope: !12, file: !1, line: 21, type: !5)
!77 = !DILocalVariable(name: "Common", arg: 4, scope: !12, file: !1, line: 22, type: !38)
!78 = !DILocalVariable(name: "Symbolic", scope: !12, file: !1, line: 25, type: !15)
!79 = !DILocalVariable(name: "P", scope: !12, file: !1, line: 26, type: !5)
!80 = !DILocalVariable(name: "Q", scope: !12, file: !1, line: 26, type: !5)
!81 = !DILocalVariable(name: "R", scope: !12, file: !1, line: 26, type: !5)
!82 = !DILocalVariable(name: "Lnz", scope: !12, file: !1, line: 27, type: !26)
!83 = !DILocalVariable(name: "nz", scope: !12, file: !1, line: 28, type: !6)
!84 = !DILocalVariable(name: "i", scope: !12, file: !1, line: 28, type: !6)
!85 = !DILocalVariable(name: "j", scope: !12, file: !1, line: 28, type: !6)
!86 = !DILocalVariable(name: "p", scope: !12, file: !1, line: 28, type: !6)
!87 = !DILocalVariable(name: "pend", scope: !12, file: !1, line: 28, type: !6)
!88 = !DILocation(line: 19, column: 9, scope: !12)
!89 = !DILocation(line: 20, column: 10, scope: !12)
!90 = !DILocation(line: 21, column: 10, scope: !12)
!91 = !DILocation(line: 22, column: 17, scope: !12)
!92 = !DILocation(line: 25, column: 5, scope: !12)
!93 = !DILocation(line: 30, column: 16, scope: !94)
!94 = distinct !DILexicalBlock(scope: !12, file: !1, line: 30, column: 9)
!95 = !DILocation(line: 30, column: 9, scope: !12)
!96 = !DILocation(line: 34, column: 13, scope: !12)
!97 = !DILocation(line: 34, column: 20, scope: !12)
!98 = !{!99, !103, i64 76}
!99 = !{!"klu_common_struct", !100, i64 0, !100, i64 8, !100, i64 16, !100, i64 24, !100, i64 32, !103, i64 40, !103, i64 44, !103, i64 48, !104, i64 56, !104, i64 64, !103, i64 72, !103, i64 76, !103, i64 80, !103, i64 84, !103, i64 88, !103, i64 92, !103, i64 96, !100, i64 104, !100, i64 112, !100, i64 120, !100, i64 128, !100, i64 136, !105, i64 144, !105, i64 152}
!100 = !{!"double", !101, i64 0}
!101 = !{!"omnipotent char", !102, i64 0}
!102 = !{!"Simple C/C++ TBAA"}
!103 = !{!"int", !101, i64 0}
!104 = !{!"any pointer", !101, i64 0}
!105 = !{!"long", !101, i64 0}
!106 = !DILocation(line: 42, column: 11, scope: !107)
!107 = distinct !DILexicalBlock(scope: !12, file: !1, line: 42, column: 9)
!108 = !DILocation(line: 42, column: 22, scope: !107)
!109 = !DILocation(line: 42, column: 16, scope: !107)
!110 = !DILocation(line: 42, column: 36, scope: !107)
!111 = !DILocation(line: 45, column: 24, scope: !112)
!112 = distinct !DILexicalBlock(scope: !107, file: !1, line: 43, column: 5)
!113 = !DILocation(line: 46, column: 9, scope: !112)
!114 = !DILocation(line: 49, column: 10, scope: !12)
!115 = !{!103, !103, i64 0}
!116 = !DILocation(line: 28, column: 9, scope: !12)
!117 = !DILocation(line: 50, column: 9, scope: !118)
!118 = distinct !DILexicalBlock(scope: !12, file: !1, line: 50, column: 9)
!119 = !DILocation(line: 50, column: 16, scope: !118)
!120 = !DILocation(line: 50, column: 27, scope: !118)
!121 = !DILocation(line: 50, column: 21, scope: !118)
!122 = !DILocation(line: 53, column: 24, scope: !123)
!123 = distinct !DILexicalBlock(scope: !118, file: !1, line: 51, column: 5)
!124 = !DILocation(line: 54, column: 9, scope: !123)
!125 = !DILocation(line: 28, column: 16, scope: !12)
!126 = !DILocation(line: 57, column: 5, scope: !127)
!127 = distinct !DILexicalBlock(scope: !12, file: !1, line: 57, column: 5)
!128 = !DILocation(line: 57, column: 20, scope: !129)
!129 = distinct !DILexicalBlock(scope: !127, file: !1, line: 57, column: 5)
!130 = distinct !{!130, !126, !131}
!131 = !DILocation(line: 65, column: 5, scope: !127)
!132 = !DILocation(line: 59, column: 13, scope: !133)
!133 = distinct !DILexicalBlock(scope: !134, file: !1, line: 59, column: 13)
!134 = distinct !DILexicalBlock(scope: !129, file: !1, line: 58, column: 5)
!135 = !DILocation(line: 59, column: 27, scope: !133)
!136 = !DILocation(line: 59, column: 22, scope: !133)
!137 = !DILocation(line: 59, column: 20, scope: !133)
!138 = !DILocation(line: 59, column: 13, scope: !134)
!139 = !DILocation(line: 62, column: 28, scope: !140)
!140 = distinct !DILexicalBlock(scope: !133, file: !1, line: 60, column: 9)
!141 = !DILocation(line: 63, column: 13, scope: !140)
!142 = !DILocation(line: 66, column: 9, scope: !12)
!143 = !DILocation(line: 26, column: 10, scope: !12)
!144 = !DILocation(line: 67, column: 17, scope: !145)
!145 = distinct !DILexicalBlock(scope: !12, file: !1, line: 67, column: 9)
!146 = !DILocation(line: 67, column: 24, scope: !145)
!147 = !DILocation(line: 67, column: 9, scope: !12)
!148 = !DILocation(line: 70, column: 24, scope: !149)
!149 = distinct !DILexicalBlock(scope: !145, file: !1, line: 68, column: 5)
!150 = !DILocation(line: 71, column: 9, scope: !149)
!151 = !DILocation(line: 28, column: 13, scope: !12)
!152 = !DILocation(line: 73, column: 20, scope: !153)
!153 = distinct !DILexicalBlock(scope: !154, file: !1, line: 73, column: 5)
!154 = distinct !DILexicalBlock(scope: !12, file: !1, line: 73, column: 5)
!155 = !DILocation(line: 73, column: 5, scope: !154)
!156 = !DILocation(line: 75, column: 15, scope: !157)
!157 = distinct !DILexicalBlock(scope: !153, file: !1, line: 74, column: 5)
!158 = !DILocation(line: 77, column: 5, scope: !159)
!159 = distinct !DILexicalBlock(scope: !12, file: !1, line: 77, column: 5)
!160 = !DILocation(line: 79, column: 21, scope: !161)
!161 = distinct !DILexicalBlock(scope: !162, file: !1, line: 78, column: 5)
!162 = distinct !DILexicalBlock(scope: !159, file: !1, line: 77, column: 5)
!163 = !DILocation(line: 79, column: 16, scope: !161)
!164 = !DILocation(line: 28, column: 22, scope: !12)
!165 = !DILocation(line: 80, column: 18, scope: !166)
!166 = distinct !DILexicalBlock(scope: !161, file: !1, line: 80, column: 9)
!167 = !DILocation(line: 28, column: 19, scope: !12)
!168 = !DILocation(line: 80, column: 29, scope: !169)
!169 = distinct !DILexicalBlock(scope: !166, file: !1, line: 80, column: 9)
!170 = !DILocation(line: 80, column: 9, scope: !166)
!171 = !DILocation(line: 82, column: 17, scope: !172)
!172 = distinct !DILexicalBlock(scope: !169, file: !1, line: 81, column: 9)
!173 = !DILocation(line: 83, column: 19, scope: !174)
!174 = distinct !DILexicalBlock(scope: !172, file: !1, line: 83, column: 17)
!175 = !DILocation(line: 83, column: 28, scope: !174)
!176 = !DILocation(line: 83, column: 23, scope: !174)
!177 = !DILocation(line: 83, column: 36, scope: !174)
!178 = !DILocation(line: 83, column: 42, scope: !174)
!179 = !DILocation(line: 83, column: 17, scope: !172)
!180 = !DILocation(line: 86, column: 17, scope: !181)
!181 = distinct !DILexicalBlock(scope: !174, file: !1, line: 84, column: 13)
!182 = !DILocation(line: 87, column: 32, scope: !181)
!183 = !DILocation(line: 88, column: 17, scope: !181)
!184 = !DILocation(line: 91, column: 19, scope: !172)
!185 = !DILocation(line: 80, column: 39, scope: !169)
!186 = distinct !{!186, !170, !187}
!187 = !DILocation(line: 92, column: 9, scope: !166)
!188 = !DILocation(line: 77, column: 20, scope: !162)
!189 = distinct !{!189, !158, !190}
!190 = !DILocation(line: 93, column: 5, scope: !159)
!191 = !DILocation(line: 99, column: 16, scope: !12)
!192 = !DILocation(line: 25, column: 19, scope: !12)
!193 = !DILocation(line: 99, column: 14, scope: !12)
!194 = !{!104, !104, i64 0}
!195 = !DILocation(line: 100, column: 17, scope: !196)
!196 = distinct !DILexicalBlock(scope: !12, file: !1, line: 100, column: 9)
!197 = !DILocation(line: 100, column: 24, scope: !196)
!198 = !DILocation(line: 100, column: 9, scope: !12)
!199 = !DILocation(line: 103, column: 9, scope: !200)
!200 = distinct !DILexicalBlock(scope: !196, file: !1, line: 101, column: 5)
!201 = !DILocation(line: 104, column: 24, scope: !200)
!202 = !DILocation(line: 105, column: 9, scope: !200)
!203 = !DILocation(line: 108, column: 9, scope: !12)
!204 = !DILocation(line: 26, column: 14, scope: !12)
!205 = !DILocation(line: 109, column: 22, scope: !12)
!206 = !DILocation(line: 109, column: 21, scope: !12)
!207 = !DILocation(line: 109, column: 9, scope: !12)
!208 = !DILocation(line: 26, column: 18, scope: !12)
!209 = !DILocation(line: 110, column: 11, scope: !12)
!210 = !DILocation(line: 27, column: 13, scope: !12)
!211 = !DILocation(line: 112, column: 5, scope: !12)
!212 = !DILocation(line: 112, column: 15, scope: !12)
!213 = !DILocation(line: 112, column: 17, scope: !12)
!214 = !{!215, !103, i64 40}
!215 = !{!"", !100, i64 0, !100, i64 8, !100, i64 16, !100, i64 24, !104, i64 32, !103, i64 40, !103, i64 44, !104, i64 48, !104, i64 56, !104, i64 64, !103, i64 72, !103, i64 76, !103, i64 80, !103, i64 84, !103, i64 88, !103, i64 92}
!216 = !DILocation(line: 113, column: 15, scope: !12)
!217 = !DILocation(line: 113, column: 18, scope: !12)
!218 = !{!215, !103, i64 44}
!219 = !DILocation(line: 114, column: 15, scope: !12)
!220 = !DILocation(line: 114, column: 17, scope: !12)
!221 = !{!215, !104, i64 48}
!222 = !DILocation(line: 115, column: 15, scope: !12)
!223 = !DILocation(line: 115, column: 17, scope: !12)
!224 = !{!215, !104, i64 56}
!225 = !DILocation(line: 116, column: 15, scope: !12)
!226 = !DILocation(line: 116, column: 17, scope: !12)
!227 = !{!215, !104, i64 64}
!228 = !DILocation(line: 117, column: 15, scope: !12)
!229 = !DILocation(line: 117, column: 19, scope: !12)
!230 = !{!215, !104, i64 32}
!231 = !DILocation(line: 119, column: 17, scope: !232)
!232 = distinct !DILexicalBlock(scope: !12, file: !1, line: 119, column: 9)
!233 = !DILocation(line: 119, column: 24, scope: !232)
!234 = !DILocation(line: 119, column: 9, scope: !12)
!235 = !DILocation(line: 122, column: 9, scope: !236)
!236 = distinct !DILexicalBlock(scope: !232, file: !1, line: 120, column: 5)
!237 = !DILocation(line: 123, column: 24, scope: !236)
!238 = !DILocation(line: 124, column: 9, scope: !236)
!239 = !DILocation(line: 0, scope: !12)
!240 = !DILocation(line: 128, column: 1, scope: !12)
!241 = distinct !DISubprogram(name: "klu_analyze_given", scope: !1, file: !1, line: 135, type: !242, isLocal: false, isDefinition: true, scopeLine: 147, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !244)
!242 = !DISubroutineType(types: !243)
!243 = !{!15, !6, !5, !5, !5, !5, !38}
!244 = !{!245, !246, !247, !248, !249, !250, !251, !252, !253, !254, !255, !256, !257, !258, !259, !260, !261, !262, !263, !264, !265, !268, !269, !270, !271, !272, !273}
!245 = !DILocalVariable(name: "n", arg: 1, scope: !241, file: !1, line: 139, type: !6)
!246 = !DILocalVariable(name: "Ap", arg: 2, scope: !241, file: !1, line: 140, type: !5)
!247 = !DILocalVariable(name: "Ai", arg: 3, scope: !241, file: !1, line: 141, type: !5)
!248 = !DILocalVariable(name: "Puser", arg: 4, scope: !241, file: !1, line: 142, type: !5)
!249 = !DILocalVariable(name: "Quser", arg: 5, scope: !241, file: !1, line: 143, type: !5)
!250 = !DILocalVariable(name: "Common", arg: 6, scope: !241, file: !1, line: 145, type: !38)
!251 = !DILocalVariable(name: "Symbolic", scope: !241, file: !1, line: 148, type: !15)
!252 = !DILocalVariable(name: "Lnz", scope: !241, file: !1, line: 149, type: !26)
!253 = !DILocalVariable(name: "nblocks", scope: !241, file: !1, line: 150, type: !6)
!254 = !DILocalVariable(name: "nz", scope: !241, file: !1, line: 150, type: !6)
!255 = !DILocalVariable(name: "block", scope: !241, file: !1, line: 150, type: !6)
!256 = !DILocalVariable(name: "maxblock", scope: !241, file: !1, line: 150, type: !6)
!257 = !DILocalVariable(name: "P", scope: !241, file: !1, line: 150, type: !5)
!258 = !DILocalVariable(name: "Q", scope: !241, file: !1, line: 150, type: !5)
!259 = !DILocalVariable(name: "R", scope: !241, file: !1, line: 150, type: !5)
!260 = !DILocalVariable(name: "nzoff", scope: !241, file: !1, line: 150, type: !6)
!261 = !DILocalVariable(name: "p", scope: !241, file: !1, line: 150, type: !6)
!262 = !DILocalVariable(name: "pend", scope: !241, file: !1, line: 150, type: !6)
!263 = !DILocalVariable(name: "do_btf", scope: !241, file: !1, line: 150, type: !6)
!264 = !DILocalVariable(name: "k", scope: !241, file: !1, line: 150, type: !6)
!265 = !DILocalVariable(name: "Pinv", scope: !266, file: !1, line: 206, type: !5)
!266 = distinct !DILexicalBlock(scope: !267, file: !1, line: 200, column: 5)
!267 = distinct !DILexicalBlock(scope: !241, file: !1, line: 199, column: 9)
!268 = !DILocalVariable(name: "Work", scope: !266, file: !1, line: 206, type: !5)
!269 = !DILocalVariable(name: "Bi", scope: !266, file: !1, line: 206, type: !5)
!270 = !DILocalVariable(name: "k1", scope: !266, file: !1, line: 206, type: !6)
!271 = !DILocalVariable(name: "k2", scope: !266, file: !1, line: 206, type: !6)
!272 = !DILocalVariable(name: "nk", scope: !266, file: !1, line: 206, type: !6)
!273 = !DILocalVariable(name: "oldcol", scope: !266, file: !1, line: 206, type: !6)
!274 = !DILocation(line: 139, column: 9, scope: !241)
!275 = !DILocation(line: 140, column: 9, scope: !241)
!276 = !DILocation(line: 141, column: 9, scope: !241)
!277 = !DILocation(line: 142, column: 9, scope: !241)
!278 = !DILocation(line: 143, column: 9, scope: !241)
!279 = !DILocation(line: 145, column: 17, scope: !241)
!280 = !DILocation(line: 148, column: 5, scope: !241)
!281 = !DILocation(line: 156, column: 16, scope: !241)
!282 = !DILocation(line: 148, column: 19, scope: !241)
!283 = !DILocation(line: 156, column: 14, scope: !241)
!284 = !DILocation(line: 157, column: 18, scope: !285)
!285 = distinct !DILexicalBlock(scope: !241, file: !1, line: 157, column: 9)
!286 = !DILocation(line: 157, column: 9, scope: !241)
!287 = !DILocation(line: 161, column: 19, scope: !241)
!288 = !DILocation(line: 150, column: 40, scope: !241)
!289 = !DILocation(line: 162, column: 19, scope: !241)
!290 = !DILocation(line: 150, column: 44, scope: !241)
!291 = !DILocation(line: 163, column: 19, scope: !241)
!292 = !DILocation(line: 150, column: 48, scope: !241)
!293 = !DILocation(line: 164, column: 21, scope: !241)
!294 = !DILocation(line: 149, column: 13, scope: !241)
!295 = !DILocation(line: 165, column: 20, scope: !241)
!296 = !DILocation(line: 150, column: 18, scope: !241)
!297 = !DILocation(line: 171, column: 15, scope: !298)
!298 = distinct !DILexicalBlock(scope: !241, file: !1, line: 171, column: 9)
!299 = !DILocation(line: 150, column: 75, scope: !241)
!300 = !DILocation(line: 0, scope: !301)
!301 = distinct !DILexicalBlock(scope: !302, file: !1, line: 180, column: 9)
!302 = distinct !DILexicalBlock(scope: !303, file: !1, line: 180, column: 9)
!303 = distinct !DILexicalBlock(scope: !298, file: !1, line: 179, column: 5)
!304 = !DILocation(line: 171, column: 9, scope: !241)
!305 = !DILocation(line: 173, column: 9, scope: !306)
!306 = distinct !DILexicalBlock(scope: !307, file: !1, line: 173, column: 9)
!307 = distinct !DILexicalBlock(scope: !298, file: !1, line: 172, column: 5)
!308 = !DILocation(line: 175, column: 13, scope: !309)
!309 = distinct !DILexicalBlock(scope: !310, file: !1, line: 174, column: 9)
!310 = distinct !DILexicalBlock(scope: !306, file: !1, line: 173, column: 9)
!311 = !DILocation(line: 175, column: 19, scope: !309)
!312 = !DILocation(line: 173, column: 31, scope: !310)
!313 = !DILocation(line: 173, column: 24, scope: !310)
!314 = distinct !{!314, !305, !315}
!315 = !DILocation(line: 176, column: 9, scope: !306)
!316 = !DILocation(line: 180, column: 9, scope: !302)
!317 = !DILocation(line: 182, column: 21, scope: !318)
!318 = distinct !DILexicalBlock(scope: !301, file: !1, line: 181, column: 9)
!319 = !DILocation(line: 182, column: 13, scope: !318)
!320 = !DILocation(line: 182, column: 19, scope: !318)
!321 = !DILocation(line: 180, column: 31, scope: !301)
!322 = !DILocation(line: 180, column: 24, scope: !301)
!323 = distinct !{!323, !316, !324}
!324 = !DILocation(line: 183, column: 9, scope: !302)
!325 = !DILocation(line: 190, column: 22, scope: !241)
!326 = !{!99, !103, i64 40}
!327 = !DILocation(line: 150, column: 67, scope: !241)
!328 = !DILocation(line: 191, column: 14, scope: !241)
!329 = !DILocation(line: 192, column: 15, scope: !241)
!330 = !DILocation(line: 192, column: 24, scope: !241)
!331 = !{!215, !103, i64 84}
!332 = !DILocation(line: 193, column: 15, scope: !241)
!333 = !DILocation(line: 193, column: 22, scope: !241)
!334 = !{!215, !103, i64 88}
!335 = !DILocation(line: 199, column: 9, scope: !241)
!336 = !DILocation(line: 208, column: 29, scope: !266)
!337 = !DILocation(line: 208, column: 28, scope: !266)
!338 = !DILocation(line: 208, column: 16, scope: !266)
!339 = !DILocation(line: 206, column: 21, scope: !266)
!340 = !DILocation(line: 209, column: 28, scope: !266)
!341 = !DILocation(line: 209, column: 16, scope: !266)
!342 = !DILocation(line: 206, column: 14, scope: !266)
!343 = !DILocation(line: 210, column: 19, scope: !344)
!344 = distinct !DILexicalBlock(scope: !266, file: !1, line: 210, column: 13)
!345 = !DILocation(line: 210, column: 13, scope: !266)
!346 = !DILocation(line: 212, column: 32, scope: !347)
!347 = distinct !DILexicalBlock(scope: !344, file: !1, line: 211, column: 9)
!348 = !DILocation(line: 212, column: 30, scope: !347)
!349 = !DILocation(line: 212, column: 18, scope: !347)
!350 = !DILocation(line: 206, column: 28, scope: !266)
!351 = !DILocation(line: 213, column: 9, scope: !347)
!352 = !DILocation(line: 0, scope: !353)
!353 = distinct !DILexicalBlock(scope: !344, file: !1, line: 215, column: 9)
!354 = !DILocation(line: 219, column: 21, scope: !355)
!355 = distinct !DILexicalBlock(scope: !266, file: !1, line: 219, column: 13)
!356 = !DILocation(line: 219, column: 28, scope: !355)
!357 = !DILocation(line: 219, column: 13, scope: !266)
!358 = !DILocation(line: 222, column: 13, scope: !359)
!359 = distinct !DILexicalBlock(scope: !355, file: !1, line: 220, column: 9)
!360 = !DILocation(line: 223, column: 13, scope: !359)
!361 = !DILocation(line: 224, column: 17, scope: !359)
!362 = !DILocation(line: 226, column: 27, scope: !363)
!363 = distinct !DILexicalBlock(scope: !364, file: !1, line: 225, column: 13)
!364 = distinct !DILexicalBlock(scope: !359, file: !1, line: 224, column: 17)
!365 = !DILocation(line: 226, column: 33, scope: !363)
!366 = !DILocation(line: 226, column: 31, scope: !363)
!367 = !DILocation(line: 226, column: 17, scope: !363)
!368 = !DILocation(line: 227, column: 13, scope: !363)
!369 = !DILocation(line: 237, column: 13, scope: !266)
!370 = !DILocation(line: 254, column: 19, scope: !266)
!371 = !DILocation(line: 150, column: 9, scope: !241)
!372 = !DILocation(line: 260, column: 13, scope: !266)
!373 = !DILocation(line: 239, column: 28, scope: !374)
!374 = distinct !DILexicalBlock(scope: !375, file: !1, line: 239, column: 13)
!375 = distinct !DILexicalBlock(scope: !376, file: !1, line: 239, column: 13)
!376 = distinct !DILexicalBlock(scope: !377, file: !1, line: 238, column: 9)
!377 = distinct !DILexicalBlock(scope: !266, file: !1, line: 237, column: 13)
!378 = !DILocation(line: 239, column: 13, scope: !375)
!379 = !DILocation(line: 241, column: 23, scope: !380)
!380 = distinct !DILexicalBlock(scope: !374, file: !1, line: 240, column: 13)
!381 = !DILocation(line: 241, column: 17, scope: !380)
!382 = !DILocation(line: 241, column: 34, scope: !380)
!383 = !DILocation(line: 239, column: 35, scope: !374)
!384 = distinct !{!384, !378, !385}
!385 = !DILocation(line: 242, column: 13, scope: !375)
!386 = !DILocation(line: 150, column: 58, scope: !241)
!387 = !DILocation(line: 243, column: 28, scope: !388)
!388 = distinct !DILexicalBlock(scope: !389, file: !1, line: 243, column: 13)
!389 = distinct !DILexicalBlock(scope: !376, file: !1, line: 243, column: 13)
!390 = !DILocation(line: 243, column: 13, scope: !389)
!391 = !DILocation(line: 245, column: 32, scope: !392)
!392 = distinct !DILexicalBlock(scope: !388, file: !1, line: 244, column: 13)
!393 = !DILocation(line: 245, column: 26, scope: !392)
!394 = !DILocation(line: 245, column: 17, scope: !392)
!395 = !DILocation(line: 245, column: 24, scope: !392)
!396 = !DILocation(line: 243, column: 36, scope: !388)
!397 = distinct !{!397, !390, !398}
!398 = !DILocation(line: 246, column: 13, scope: !389)
!399 = !DILocation(line: 262, column: 28, scope: !400)
!400 = distinct !DILexicalBlock(scope: !401, file: !1, line: 262, column: 13)
!401 = distinct !DILexicalBlock(scope: !402, file: !1, line: 262, column: 13)
!402 = distinct !DILexicalBlock(scope: !403, file: !1, line: 261, column: 9)
!403 = distinct !DILexicalBlock(scope: !266, file: !1, line: 260, column: 13)
!404 = !DILocation(line: 262, column: 13, scope: !401)
!405 = !DILocation(line: 264, column: 35, scope: !406)
!406 = distinct !DILexicalBlock(scope: !400, file: !1, line: 263, column: 13)
!407 = !DILocation(line: 264, column: 28, scope: !406)
!408 = !DILocation(line: 264, column: 17, scope: !406)
!409 = !DILocation(line: 264, column: 26, scope: !406)
!410 = !DILocation(line: 262, column: 35, scope: !400)
!411 = distinct !{!411, !404, !412}
!412 = !DILocation(line: 265, column: 13, scope: !401)
!413 = !DILocation(line: 266, column: 28, scope: !414)
!414 = distinct !DILexicalBlock(scope: !415, file: !1, line: 266, column: 13)
!415 = distinct !DILexicalBlock(scope: !402, file: !1, line: 266, column: 13)
!416 = !DILocation(line: 266, column: 13, scope: !415)
!417 = !DILocation(line: 268, column: 25, scope: !418)
!418 = distinct !DILexicalBlock(scope: !414, file: !1, line: 267, column: 13)
!419 = !DILocation(line: 268, column: 17, scope: !418)
!420 = !DILocation(line: 268, column: 23, scope: !418)
!421 = !DILocation(line: 266, column: 35, scope: !414)
!422 = distinct !{!422, !416, !423}
!423 = !DILocation(line: 269, column: 13, scope: !415)
!424 = !DILocation(line: 276, column: 24, scope: !425)
!425 = distinct !DILexicalBlock(scope: !426, file: !1, line: 276, column: 9)
!426 = distinct !DILexicalBlock(scope: !266, file: !1, line: 276, column: 9)
!427 = !DILocation(line: 276, column: 9, scope: !426)
!428 = !DILocation(line: 278, column: 19, scope: !429)
!429 = distinct !DILexicalBlock(scope: !425, file: !1, line: 277, column: 9)
!430 = !DILocation(line: 278, column: 13, scope: !429)
!431 = !DILocation(line: 278, column: 26, scope: !429)
!432 = !DILocation(line: 276, column: 31, scope: !425)
!433 = distinct !{!433, !427, !434}
!434 = !DILocation(line: 279, column: 9, scope: !426)
!435 = !DILocation(line: 150, column: 51, scope: !241)
!436 = !DILocation(line: 150, column: 29, scope: !241)
!437 = !DILocation(line: 150, column: 22, scope: !241)
!438 = !DILocation(line: 288, column: 32, scope: !439)
!439 = distinct !DILexicalBlock(scope: !440, file: !1, line: 288, column: 9)
!440 = distinct !DILexicalBlock(scope: !266, file: !1, line: 288, column: 9)
!441 = !DILocation(line: 288, column: 9, scope: !440)
!442 = !DILocation(line: 295, column: 18, scope: !443)
!443 = distinct !DILexicalBlock(scope: !439, file: !1, line: 289, column: 9)
!444 = !DILocation(line: 206, column: 32, scope: !266)
!445 = !DILocation(line: 296, column: 26, scope: !443)
!446 = !DILocation(line: 296, column: 18, scope: !443)
!447 = !DILocation(line: 206, column: 36, scope: !266)
!448 = !DILocation(line: 297, column: 21, scope: !443)
!449 = !DILocation(line: 206, column: 40, scope: !266)
!450 = !DILocation(line: 299, column: 24, scope: !443)
!451 = !DILocation(line: 305, column: 29, scope: !452)
!452 = distinct !DILexicalBlock(scope: !453, file: !1, line: 305, column: 13)
!453 = distinct !DILexicalBlock(scope: !443, file: !1, line: 305, column: 13)
!454 = !DILocation(line: 305, column: 13, scope: !453)
!455 = !DILocation(line: 307, column: 26, scope: !456)
!456 = distinct !DILexicalBlock(scope: !452, file: !1, line: 306, column: 13)
!457 = !DILocation(line: 206, column: 44, scope: !266)
!458 = !DILocation(line: 308, column: 34, scope: !456)
!459 = !DILocation(line: 308, column: 24, scope: !456)
!460 = !DILocation(line: 150, column: 61, scope: !241)
!461 = !DILocation(line: 309, column: 26, scope: !462)
!462 = distinct !DILexicalBlock(scope: !456, file: !1, line: 309, column: 17)
!463 = !DILocation(line: 309, column: 42, scope: !464)
!464 = distinct !DILexicalBlock(scope: !462, file: !1, line: 309, column: 17)
!465 = !DILocation(line: 309, column: 17, scope: !462)
!466 = !DILocation(line: 311, column: 31, scope: !467)
!467 = distinct !DILexicalBlock(scope: !468, file: !1, line: 311, column: 25)
!468 = distinct !DILexicalBlock(scope: !464, file: !1, line: 310, column: 17)
!469 = !DILocation(line: 311, column: 25, scope: !467)
!470 = !DILocation(line: 311, column: 39, scope: !467)
!471 = !DILocation(line: 311, column: 25, scope: !468)
!472 = !DILocation(line: 309, column: 52, scope: !464)
!473 = distinct !{!473, !465, !474}
!474 = !DILocation(line: 315, column: 17, scope: !462)
!475 = !DILocation(line: 305, column: 37, scope: !452)
!476 = distinct !{!476, !454, !477}
!477 = !DILocation(line: 316, column: 13, scope: !453)
!478 = !DILocation(line: 319, column: 13, scope: !443)
!479 = !DILocation(line: 319, column: 25, scope: !443)
!480 = !{!100, !100, i64 0}
!481 = distinct !{!481, !441, !482}
!482 = !DILocation(line: 320, column: 9, scope: !440)
!483 = !DILocation(line: 326, column: 9, scope: !266)
!484 = !DILocation(line: 327, column: 9, scope: !266)
!485 = !DILocation(line: 328, column: 13, scope: !266)
!486 = !DILocation(line: 330, column: 23, scope: !487)
!487 = distinct !DILexicalBlock(scope: !488, file: !1, line: 329, column: 9)
!488 = distinct !DILexicalBlock(scope: !266, file: !1, line: 328, column: 13)
!489 = !DILocation(line: 330, column: 29, scope: !487)
!490 = !DILocation(line: 330, column: 27, scope: !487)
!491 = !DILocation(line: 330, column: 13, scope: !487)
!492 = !DILocation(line: 331, column: 9, scope: !487)
!493 = !DILocation(line: 228, column: 13, scope: !359)
!494 = !DILocation(line: 229, column: 28, scope: !359)
!495 = !DILocation(line: 344, column: 15, scope: !496)
!496 = distinct !DILexicalBlock(scope: !267, file: !1, line: 335, column: 5)
!497 = !DILocation(line: 345, column: 9, scope: !496)
!498 = !DILocation(line: 345, column: 15, scope: !496)
!499 = !DILocation(line: 346, column: 17, scope: !496)
!500 = !DILocation(line: 352, column: 24, scope: !501)
!501 = distinct !DILexicalBlock(scope: !502, file: !1, line: 352, column: 9)
!502 = distinct !DILexicalBlock(scope: !496, file: !1, line: 352, column: 9)
!503 = !DILocation(line: 352, column: 9, scope: !502)
!504 = !DILocation(line: 354, column: 21, scope: !505)
!505 = distinct !DILexicalBlock(scope: !501, file: !1, line: 353, column: 9)
!506 = !DILocation(line: 354, column: 43, scope: !505)
!507 = !DILocation(line: 354, column: 13, scope: !505)
!508 = !DILocation(line: 354, column: 19, scope: !505)
!509 = !DILocation(line: 352, column: 31, scope: !501)
!510 = distinct !{!510, !503, !511}
!511 = !DILocation(line: 355, column: 9, scope: !502)
!512 = !DILocation(line: 362, column: 5, scope: !241)
!513 = !DILocation(line: 362, column: 15, scope: !241)
!514 = !DILocation(line: 362, column: 23, scope: !241)
!515 = !{!215, !103, i64 76}
!516 = !DILocation(line: 363, column: 15, scope: !241)
!517 = !DILocation(line: 363, column: 24, scope: !241)
!518 = !{!215, !103, i64 80}
!519 = !DILocation(line: 364, column: 15, scope: !241)
!520 = !DILocation(line: 364, column: 19, scope: !241)
!521 = !{!215, !100, i64 16}
!522 = !DILocation(line: 365, column: 15, scope: !241)
!523 = !DILocation(line: 365, column: 19, scope: !241)
!524 = !{!215, !100, i64 24}
!525 = !DILocation(line: 366, column: 15, scope: !241)
!526 = !DILocation(line: 366, column: 21, scope: !241)
!527 = !{!215, !103, i64 72}
!528 = !DILocation(line: 368, column: 5, scope: !241)
!529 = !DILocation(line: 0, scope: !359)
!530 = !DILocation(line: 369, column: 1, scope: !241)
