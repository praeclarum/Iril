; ModuleID = 'klu_sort.c'
source_filename = "klu_sort.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_sort(%struct.klu_symbolic* nocapture readonly, %struct.klu_numeric* nocapture readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !21 {
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %0, metadata !108, metadata !DIExpression()), !dbg !129
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %1, metadata !109, metadata !DIExpression()), !dbg !130
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %2, metadata !110, metadata !DIExpression()), !dbg !131
  %4 = icmp eq %struct.klu_common_struct* %2, null, !dbg !132
  br i1 %4, label %75, label %5, !dbg !134

; <label>:5:                                      ; preds = %3
  %6 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !135
  store i32 0, i32* %6, align 4, !dbg !136, !tbaa !137
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %0, metadata !121, metadata !DIExpression(DW_OP_plus_uconst, 40, DW_OP_deref, DW_OP_stack_value)), !dbg !145
  %7 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 9, !dbg !146
  %8 = load i32*, i32** %7, align 8, !dbg !146, !tbaa !147
  call void @llvm.dbg.value(metadata i32* %8, metadata !111, metadata !DIExpression()), !dbg !149
  %9 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 11, !dbg !150
  %10 = load i32, i32* %9, align 4, !dbg !150, !tbaa !151
  call void @llvm.dbg.value(metadata i32 %10, metadata !125, metadata !DIExpression()), !dbg !152
  %11 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 12, !dbg !153
  %12 = load i32, i32* %11, align 8, !dbg !153, !tbaa !154
  call void @llvm.dbg.value(metadata i32 %12, metadata !126, metadata !DIExpression()), !dbg !155
  %13 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 8, !dbg !156
  %14 = load i32*, i32** %13, align 8, !dbg !156, !tbaa !157
  call void @llvm.dbg.value(metadata i32* %14, metadata !115, metadata !DIExpression()), !dbg !159
  %15 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 10, !dbg !160
  %16 = load i32*, i32** %15, align 8, !dbg !160, !tbaa !161
  call void @llvm.dbg.value(metadata i32* %16, metadata !117, metadata !DIExpression()), !dbg !162
  %17 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 9, !dbg !163
  %18 = load i32*, i32** %17, align 8, !dbg !163, !tbaa !164
  call void @llvm.dbg.value(metadata i32* %18, metadata !116, metadata !DIExpression()), !dbg !165
  %19 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 11, !dbg !166
  %20 = load i32*, i32** %19, align 8, !dbg !166, !tbaa !167
  call void @llvm.dbg.value(metadata i32* %20, metadata !118, metadata !DIExpression()), !dbg !168
  %21 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 12, !dbg !169
  %22 = bitcast i8*** %21 to double***, !dbg !169
  %23 = load double**, double*** %22, align 8, !dbg !169, !tbaa !170
  call void @llvm.dbg.value(metadata double** %23, metadata !120, metadata !DIExpression()), !dbg !171
  %24 = sext i32 %12 to i64, !dbg !172
  %25 = add nsw i64 %24, 1, !dbg !173
  call void @llvm.dbg.value(metadata i64 %25, metadata !128, metadata !DIExpression()), !dbg !174
  %26 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 4, !dbg !175
  %27 = load i32, i32* %26, align 8, !dbg !175, !tbaa !176
  %28 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 5, !dbg !175
  %29 = load i32, i32* %28, align 4, !dbg !175, !tbaa !177
  %30 = icmp sgt i32 %27, %29, !dbg !175
  %31 = select i1 %30, i32 %27, i32 %29, !dbg !175
  call void @llvm.dbg.value(metadata i32 %31, metadata !123, metadata !DIExpression()), !dbg !178
  %32 = tail call i8* @klu_malloc(i64 %24, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !179
  %33 = bitcast i8* %32 to i32*, !dbg !179
  call void @llvm.dbg.value(metadata i32* %33, metadata !112, metadata !DIExpression()), !dbg !180
  %34 = tail call i8* @klu_malloc(i64 %25, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !181
  %35 = bitcast i8* %34 to i32*, !dbg !181
  call void @llvm.dbg.value(metadata i32* %35, metadata !113, metadata !DIExpression()), !dbg !182
  %36 = sext i32 %31 to i64, !dbg !183
  %37 = tail call i8* @klu_malloc(i64 %36, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !184
  %38 = bitcast i8* %37 to i32*, !dbg !184
  call void @llvm.dbg.value(metadata i32* %38, metadata !114, metadata !DIExpression()), !dbg !185
  %39 = tail call i8* @klu_malloc(i64 %36, i64 8, %struct.klu_common_struct* nonnull %2) #4, !dbg !186
  %40 = bitcast i8* %39 to double*, !dbg !186
  call void @llvm.dbg.value(metadata double* %40, metadata !119, metadata !DIExpression()), !dbg !187
  %41 = load i32, i32* %6, align 4, !dbg !188, !tbaa !137
  %42 = icmp eq i32 %41, 0, !dbg !190
  %43 = icmp sgt i32 %10, 0, !dbg !191
  %44 = and i1 %42, %43, !dbg !195
  call void @llvm.dbg.value(metadata i32 0, metadata !124, metadata !DIExpression()), !dbg !196
  br i1 %44, label %45, label %67, !dbg !195

; <label>:45:                                     ; preds = %5
  %46 = zext i32 %10 to i64
  br label %47, !dbg !197

; <label>:47:                                     ; preds = %65, %45
  %48 = phi i64 [ 0, %45 ], [ %51, %65 ]
  call void @llvm.dbg.value(metadata i64 %48, metadata !124, metadata !DIExpression()), !dbg !196
  %49 = getelementptr inbounds i32, i32* %8, i64 %48, !dbg !198
  %50 = load i32, i32* %49, align 4, !dbg !198, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %50, metadata !127, metadata !DIExpression()), !dbg !201
  %51 = add nuw nsw i64 %48, 1, !dbg !202
  %52 = getelementptr inbounds i32, i32* %8, i64 %51, !dbg !203
  %53 = load i32, i32* %52, align 4, !dbg !203, !tbaa !200
  %54 = sub nsw i32 %53, %50, !dbg !204
  call void @llvm.dbg.value(metadata i32 %54, metadata !122, metadata !DIExpression()), !dbg !205
  %55 = icmp sgt i32 %54, 1, !dbg !206
  br i1 %55, label %56, label %65, !dbg !208

; <label>:56:                                     ; preds = %47
  %57 = sext i32 %50 to i64, !dbg !209
  %58 = getelementptr inbounds i32, i32* %14, i64 %57, !dbg !209
  %59 = getelementptr inbounds i32, i32* %16, i64 %57, !dbg !211
  %60 = getelementptr inbounds double*, double** %23, i64 %48, !dbg !212
  %61 = load double*, double** %60, align 8, !dbg !212, !tbaa !213
  tail call fastcc void @sort(i32 %54, i32* %58, i32* %59, double* %61, i32* %35, i32* %38, double* %40, i32* %33), !dbg !214
  %62 = getelementptr inbounds i32, i32* %18, i64 %57, !dbg !215
  %63 = getelementptr inbounds i32, i32* %20, i64 %57, !dbg !216
  %64 = load double*, double** %60, align 8, !dbg !217, !tbaa !213
  tail call fastcc void @sort(i32 %54, i32* %62, i32* %63, double* %64, i32* %35, i32* %38, double* %40, i32* %33), !dbg !218
  br label %65, !dbg !219

; <label>:65:                                     ; preds = %47, %56
  call void @llvm.dbg.value(metadata i32 undef, metadata !124, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !196
  %66 = icmp eq i64 %51, %46, !dbg !191
  br i1 %66, label %67, label %47, !dbg !197, !llvm.loop !220

; <label>:67:                                     ; preds = %65, %5
  %68 = tail call i8* @klu_free(i8* %32, i64 %24, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !222
  %69 = tail call i8* @klu_free(i8* %34, i64 %25, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !223
  %70 = tail call i8* @klu_free(i8* %37, i64 %36, i64 4, %struct.klu_common_struct* nonnull %2) #4, !dbg !224
  %71 = tail call i8* @klu_free(i8* %39, i64 %36, i64 8, %struct.klu_common_struct* nonnull %2) #4, !dbg !225
  %72 = load i32, i32* %6, align 4, !dbg !226, !tbaa !137
  %73 = icmp eq i32 %72, 0, !dbg !227
  %74 = zext i1 %73 to i32, !dbg !227
  br label %75, !dbg !228

; <label>:75:                                     ; preds = %3, %67
  %76 = phi i32 [ %74, %67 ], [ 0, %3 ], !dbg !229
  ret i32 %76, !dbg !230
}

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @sort(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture, i32* nocapture, i32* nocapture, double* nocapture, i32* nocapture) unnamed_addr #0 !dbg !231 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !235, metadata !DIExpression()), !dbg !271
  call void @llvm.dbg.value(metadata i32* %1, metadata !236, metadata !DIExpression()), !dbg !272
  call void @llvm.dbg.value(metadata i32* %2, metadata !237, metadata !DIExpression()), !dbg !273
  call void @llvm.dbg.value(metadata double* %3, metadata !238, metadata !DIExpression()), !dbg !274
  call void @llvm.dbg.value(metadata i32* %4, metadata !239, metadata !DIExpression()), !dbg !275
  call void @llvm.dbg.value(metadata i32* %5, metadata !240, metadata !DIExpression()), !dbg !276
  call void @llvm.dbg.value(metadata double* %6, metadata !241, metadata !DIExpression()), !dbg !277
  call void @llvm.dbg.value(metadata i32* %7, metadata !242, metadata !DIExpression()), !dbg !278
  call void @llvm.dbg.value(metadata i32 0, metadata !246, metadata !DIExpression()), !dbg !279
  %9 = bitcast i32* %7 to i8*
  %10 = icmp sgt i32 %0, 0, !dbg !280
  br i1 %10, label %11, label %53, !dbg !283

; <label>:11:                                     ; preds = %8
  %12 = zext i32 %0 to i64, !dbg !283
  %13 = shl nuw nsw i64 %12, 2, !dbg !283
  call void @llvm.memset.p0i8.i64(i8* %9, i8 0, i64 %13, i32 4, i1 false), !dbg !284
  call void @llvm.dbg.value(metadata i32 0, metadata !247, metadata !DIExpression()), !dbg !286
  %14 = zext i32 %0 to i64
  br label %15, !dbg !287

; <label>:15:                                     ; preds = %37, %11
  %16 = phi i64 [ 0, %11 ], [ %38, %37 ]
  call void @llvm.dbg.value(metadata i64 %16, metadata !247, metadata !DIExpression()), !dbg !286
  %17 = getelementptr inbounds i32, i32* %1, i64 %16, !dbg !288
  %18 = load i32, i32* %17, align 4, !dbg !288, !tbaa !200
  %19 = sext i32 %18 to i64, !dbg !288
  %20 = getelementptr inbounds double, double* %3, i64 %19, !dbg !288
  call void @llvm.dbg.value(metadata double* %20, metadata !253, metadata !DIExpression()), !dbg !288
  %21 = getelementptr inbounds i32, i32* %2, i64 %16, !dbg !288
  %22 = load i32, i32* %21, align 4, !dbg !288, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %22, metadata !248, metadata !DIExpression()), !dbg !289
  %23 = bitcast double* %20 to i32*, !dbg !288
  call void @llvm.dbg.value(metadata i32* %23, metadata !243, metadata !DIExpression()), !dbg !290
  call void @llvm.dbg.value(metadata i32 0, metadata !245, metadata !DIExpression()), !dbg !291
  %24 = icmp sgt i32 %22, 0, !dbg !292
  br i1 %24, label %25, label %37, !dbg !295

; <label>:25:                                     ; preds = %15
  %26 = zext i32 %22 to i64
  br label %27, !dbg !295

; <label>:27:                                     ; preds = %27, %25
  %28 = phi i64 [ 0, %25 ], [ %35, %27 ]
  call void @llvm.dbg.value(metadata i64 %28, metadata !245, metadata !DIExpression()), !dbg !291
  %29 = getelementptr inbounds i32, i32* %23, i64 %28, !dbg !296
  %30 = load i32, i32* %29, align 4, !dbg !296, !tbaa !200
  %31 = sext i32 %30 to i64, !dbg !298
  %32 = getelementptr inbounds i32, i32* %7, i64 %31, !dbg !298
  %33 = load i32, i32* %32, align 4, !dbg !299, !tbaa !200
  %34 = add nsw i32 %33, 1, !dbg !299
  store i32 %34, i32* %32, align 4, !dbg !299, !tbaa !200
  %35 = add nuw nsw i64 %28, 1, !dbg !300
  call void @llvm.dbg.value(metadata i32 undef, metadata !245, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !291
  %36 = icmp eq i64 %35, %26, !dbg !292
  br i1 %36, label %37, label %27, !dbg !295, !llvm.loop !301

; <label>:37:                                     ; preds = %27, %15
  %38 = add nuw nsw i64 %16, 1, !dbg !303
  call void @llvm.dbg.value(metadata i32 undef, metadata !247, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !286
  %39 = icmp eq i64 %38, %14, !dbg !304
  br i1 %39, label %40, label %15, !dbg !287, !llvm.loop !305

; <label>:40:                                     ; preds = %37
  call void @llvm.dbg.value(metadata i32 0, metadata !249, metadata !DIExpression()), !dbg !307
  call void @llvm.dbg.value(metadata i32 0, metadata !246, metadata !DIExpression()), !dbg !279
  %41 = icmp sgt i32 %0, 0, !dbg !308
  br i1 %41, label %42, label %53, !dbg !311

; <label>:42:                                     ; preds = %40
  %43 = zext i32 %0 to i64
  br label %44, !dbg !311

; <label>:44:                                     ; preds = %44, %42
  %45 = phi i64 [ 0, %42 ], [ %51, %44 ]
  %46 = phi i32 [ 0, %42 ], [ %50, %44 ]
  call void @llvm.dbg.value(metadata i64 %45, metadata !246, metadata !DIExpression()), !dbg !279
  call void @llvm.dbg.value(metadata i32 %46, metadata !249, metadata !DIExpression()), !dbg !307
  %47 = getelementptr inbounds i32, i32* %4, i64 %45, !dbg !312
  store i32 %46, i32* %47, align 4, !dbg !314, !tbaa !200
  %48 = getelementptr inbounds i32, i32* %7, i64 %45, !dbg !315
  %49 = load i32, i32* %48, align 4, !dbg !315, !tbaa !200
  %50 = add nsw i32 %49, %46, !dbg !316
  %51 = add nuw nsw i64 %45, 1, !dbg !317
  call void @llvm.dbg.value(metadata i32 undef, metadata !246, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !279
  call void @llvm.dbg.value(metadata i32 %50, metadata !249, metadata !DIExpression()), !dbg !307
  %52 = icmp eq i64 %51, %43, !dbg !308
  br i1 %52, label %56, label %44, !dbg !311, !llvm.loop !318

; <label>:53:                                     ; preds = %40, %8
  call void @llvm.dbg.value(metadata i32 %50, metadata !249, metadata !DIExpression()), !dbg !307
  %54 = sext i32 %0 to i64, !dbg !320
  %55 = getelementptr inbounds i32, i32* %4, i64 %54, !dbg !320
  store i32 0, i32* %55, align 4, !dbg !321, !tbaa !200
  call void @llvm.dbg.value(metadata i32 0, metadata !246, metadata !DIExpression()), !dbg !279
  br label %160, !dbg !322

; <label>:56:                                     ; preds = %44
  call void @llvm.dbg.value(metadata i32 %50, metadata !249, metadata !DIExpression()), !dbg !307
  %57 = sext i32 %0 to i64, !dbg !320
  %58 = getelementptr inbounds i32, i32* %4, i64 %57, !dbg !320
  store i32 %50, i32* %58, align 4, !dbg !321, !tbaa !200
  call void @llvm.dbg.value(metadata i32 0, metadata !246, metadata !DIExpression()), !dbg !279
  %59 = icmp sgt i32 %0, 0, !dbg !324
  br i1 %59, label %60, label %160, !dbg !322

; <label>:60:                                     ; preds = %56
  %61 = zext i32 %0 to i64
  br label %62, !dbg !322

; <label>:62:                                     ; preds = %62, %60
  %63 = phi i64 [ 0, %60 ], [ %67, %62 ]
  call void @llvm.dbg.value(metadata i64 %63, metadata !246, metadata !DIExpression()), !dbg !279
  %64 = getelementptr inbounds i32, i32* %4, i64 %63, !dbg !326
  %65 = load i32, i32* %64, align 4, !dbg !326, !tbaa !200
  %66 = getelementptr inbounds i32, i32* %7, i64 %63, !dbg !328
  store i32 %65, i32* %66, align 4, !dbg !329, !tbaa !200
  %67 = add nuw nsw i64 %63, 1, !dbg !330
  call void @llvm.dbg.value(metadata i32 undef, metadata !246, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !279
  %68 = icmp eq i64 %67, %61, !dbg !324
  br i1 %68, label %69, label %62, !dbg !322, !llvm.loop !331

; <label>:69:                                     ; preds = %62
  call void @llvm.dbg.value(metadata i32 0, metadata !247, metadata !DIExpression()), !dbg !286
  %70 = icmp sgt i32 %0, 0, !dbg !333
  br i1 %70, label %71, label %160, !dbg !334

; <label>:71:                                     ; preds = %69
  %72 = zext i32 %0 to i64
  br label %73, !dbg !334

; <label>:73:                                     ; preds = %108, %71
  %74 = phi i64 [ 0, %71 ], [ %109, %108 ]
  call void @llvm.dbg.value(metadata i64 %74, metadata !247, metadata !DIExpression()), !dbg !286
  %75 = getelementptr inbounds i32, i32* %1, i64 %74, !dbg !335
  %76 = load i32, i32* %75, align 4, !dbg !335, !tbaa !200
  %77 = sext i32 %76 to i64, !dbg !335
  %78 = getelementptr inbounds double, double* %3, i64 %77, !dbg !335
  call void @llvm.dbg.value(metadata double* %78, metadata !258, metadata !DIExpression()), !dbg !335
  %79 = getelementptr inbounds i32, i32* %2, i64 %74, !dbg !335
  %80 = load i32, i32* %79, align 4, !dbg !335, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %80, metadata !248, metadata !DIExpression()), !dbg !289
  %81 = bitcast double* %78 to i32*, !dbg !335
  call void @llvm.dbg.value(metadata i32* %81, metadata !243, metadata !DIExpression()), !dbg !290
  %82 = sext i32 %80 to i64, !dbg !335
  %83 = shl nsw i64 %82, 2, !dbg !335
  %84 = add nsw i64 %83, 7, !dbg !335
  %85 = lshr i64 %84, 3, !dbg !335
  %86 = getelementptr inbounds double, double* %78, i64 %85, !dbg !335
  call void @llvm.dbg.value(metadata double* %86, metadata !244, metadata !DIExpression()), !dbg !336
  call void @llvm.dbg.value(metadata i32 0, metadata !245, metadata !DIExpression()), !dbg !291
  %87 = icmp sgt i32 %80, 0, !dbg !337
  br i1 %87, label %88, label %108, !dbg !340

; <label>:88:                                     ; preds = %73
  %89 = trunc i64 %74 to i32
  %90 = zext i32 %80 to i64
  br label %91, !dbg !340

; <label>:91:                                     ; preds = %91, %88
  %92 = phi i64 [ 0, %88 ], [ %106, %91 ]
  call void @llvm.dbg.value(metadata i64 %92, metadata !245, metadata !DIExpression()), !dbg !291
  %93 = getelementptr inbounds i32, i32* %81, i64 %92, !dbg !341
  %94 = load i32, i32* %93, align 4, !dbg !341, !tbaa !200
  %95 = sext i32 %94 to i64, !dbg !343
  %96 = getelementptr inbounds i32, i32* %7, i64 %95, !dbg !343
  %97 = load i32, i32* %96, align 4, !dbg !344, !tbaa !200
  %98 = add nsw i32 %97, 1, !dbg !344
  store i32 %98, i32* %96, align 4, !dbg !344, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %97, metadata !250, metadata !DIExpression()), !dbg !345
  %99 = sext i32 %97 to i64, !dbg !346
  %100 = getelementptr inbounds i32, i32* %5, i64 %99, !dbg !346
  store i32 %89, i32* %100, align 4, !dbg !347, !tbaa !200
  %101 = getelementptr inbounds double, double* %86, i64 %92, !dbg !348
  %102 = bitcast double* %101 to i64*, !dbg !348
  %103 = load i64, i64* %102, align 8, !dbg !348, !tbaa !349
  %104 = getelementptr inbounds double, double* %6, i64 %99, !dbg !350
  %105 = bitcast double* %104 to i64*, !dbg !351
  store i64 %103, i64* %105, align 8, !dbg !351, !tbaa !349
  %106 = add nuw nsw i64 %92, 1, !dbg !352
  call void @llvm.dbg.value(metadata i32 undef, metadata !245, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !291
  %107 = icmp eq i64 %106, %90, !dbg !337
  br i1 %107, label %108, label %91, !dbg !340, !llvm.loop !353

; <label>:108:                                    ; preds = %91, %73
  %109 = add nuw nsw i64 %74, 1, !dbg !355
  call void @llvm.dbg.value(metadata i32 undef, metadata !247, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !286
  %110 = icmp eq i64 %109, %72, !dbg !333
  br i1 %110, label %111, label %73, !dbg !334, !llvm.loop !356

; <label>:111:                                    ; preds = %108
  call void @llvm.dbg.value(metadata i32 0, metadata !247, metadata !DIExpression()), !dbg !286
  %112 = icmp sgt i32 %0, 0, !dbg !358
  br i1 %112, label %113, label %160, !dbg !361

; <label>:113:                                    ; preds = %111
  %114 = zext i32 %0 to i64, !dbg !361
  %115 = shl nuw nsw i64 %114, 2, !dbg !361
  call void @llvm.memset.p0i8.i64(i8* %9, i8 0, i64 %115, i32 4, i1 false), !dbg !362
  call void @llvm.dbg.value(metadata i32 0, metadata !246, metadata !DIExpression()), !dbg !279
  %116 = zext i32 %0 to i64
  br label %117, !dbg !364

; <label>:117:                                    ; preds = %158, %113
  %118 = phi i64 [ 0, %113 ], [ %119, %158 ]
  call void @llvm.dbg.value(metadata i64 %118, metadata !246, metadata !DIExpression()), !dbg !279
  %119 = add nuw nsw i64 %118, 1, !dbg !365
  %120 = getelementptr inbounds i32, i32* %4, i64 %119, !dbg !366
  %121 = load i32, i32* %120, align 4, !dbg !366, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %121, metadata !252, metadata !DIExpression()), !dbg !367
  %122 = getelementptr inbounds i32, i32* %4, i64 %118, !dbg !368
  %123 = load i32, i32* %122, align 4, !dbg !368, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %123, metadata !245, metadata !DIExpression()), !dbg !291
  %124 = icmp slt i32 %123, %121, !dbg !369
  br i1 %124, label %125, label %158, !dbg !370

; <label>:125:                                    ; preds = %117
  %126 = sext i32 %123 to i64, !dbg !370
  %127 = trunc i64 %118 to i32
  %128 = sext i32 %121 to i64
  br label %129, !dbg !370

; <label>:129:                                    ; preds = %129, %125
  %130 = phi i64 [ %126, %125 ], [ %156, %129 ]
  call void @llvm.dbg.value(metadata i64 %130, metadata !245, metadata !DIExpression()), !dbg !291
  %131 = getelementptr inbounds i32, i32* %5, i64 %130, !dbg !371
  %132 = load i32, i32* %131, align 4, !dbg !371, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %132, metadata !247, metadata !DIExpression()), !dbg !286
  %133 = sext i32 %132 to i64, !dbg !372
  %134 = getelementptr inbounds i32, i32* %1, i64 %133, !dbg !372
  %135 = load i32, i32* %134, align 4, !dbg !372, !tbaa !200
  %136 = sext i32 %135 to i64, !dbg !372
  %137 = getelementptr inbounds double, double* %3, i64 %136, !dbg !372
  call void @llvm.dbg.value(metadata double* %137, metadata !263, metadata !DIExpression()), !dbg !372
  %138 = getelementptr inbounds i32, i32* %2, i64 %133, !dbg !372
  %139 = load i32, i32* %138, align 4, !dbg !372, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %139, metadata !248, metadata !DIExpression()), !dbg !289
  %140 = bitcast double* %137 to i32*, !dbg !372
  call void @llvm.dbg.value(metadata i32* %140, metadata !243, metadata !DIExpression()), !dbg !290
  %141 = sext i32 %139 to i64, !dbg !372
  %142 = shl nsw i64 %141, 2, !dbg !372
  %143 = add nsw i64 %142, 7, !dbg !372
  %144 = lshr i64 %143, 3, !dbg !372
  %145 = getelementptr inbounds double, double* %137, i64 %144, !dbg !372
  call void @llvm.dbg.value(metadata double* %145, metadata !244, metadata !DIExpression()), !dbg !336
  %146 = getelementptr inbounds i32, i32* %7, i64 %133, !dbg !373
  %147 = load i32, i32* %146, align 4, !dbg !374, !tbaa !200
  %148 = add nsw i32 %147, 1, !dbg !374
  store i32 %148, i32* %146, align 4, !dbg !374, !tbaa !200
  call void @llvm.dbg.value(metadata i32 %147, metadata !251, metadata !DIExpression()), !dbg !375
  %149 = sext i32 %147 to i64, !dbg !376
  %150 = getelementptr inbounds i32, i32* %140, i64 %149, !dbg !376
  store i32 %127, i32* %150, align 4, !dbg !377, !tbaa !200
  %151 = getelementptr inbounds double, double* %6, i64 %130, !dbg !378
  %152 = bitcast double* %151 to i64*, !dbg !378
  %153 = load i64, i64* %152, align 8, !dbg !378, !tbaa !349
  %154 = getelementptr inbounds double, double* %145, i64 %149, !dbg !379
  %155 = bitcast double* %154 to i64*, !dbg !380
  store i64 %153, i64* %155, align 8, !dbg !380, !tbaa !349
  %156 = add nsw i64 %130, 1, !dbg !381
  call void @llvm.dbg.value(metadata i32 undef, metadata !245, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !291
  %157 = icmp eq i64 %156, %128, !dbg !369
  br i1 %157, label %158, label %129, !dbg !370, !llvm.loop !382

; <label>:158:                                    ; preds = %129, %117
  call void @llvm.dbg.value(metadata i32 undef, metadata !246, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !279
  %159 = icmp eq i64 %119, %116, !dbg !384
  br i1 %159, label %160, label %117, !dbg !364, !llvm.loop !385

; <label>:160:                                    ; preds = %158, %56, %53, %69, %111
  ret void, !dbg !387
}

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #2

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #3

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind readnone speculatable }
attributes #3 = { argmemonly nounwind }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!16, !17, !18, !19}
!llvm.ident = !{!20}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_sort.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5, !10, !13, !15}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !7, size: 64)
!7 = !DIDerivedType(tag: DW_TAG_typedef, name: "Unit", file: !8, line: 253, baseType: !9)
!8 = !DIFile(filename: "./klu_version.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!9 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!10 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !11, line: 62, baseType: !12)
!11 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!12 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!13 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !14, size: 64)
!14 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!15 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !9, size: 64)
!16 = !{i32 2, !"Dwarf Version", i32 4}
!17 = !{i32 2, !"Debug Info Version", i32 3}
!18 = !{i32 1, !"wchar_size", i32 4}
!19 = !{i32 7, !"PIC Level", i32 2}
!20 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!21 = distinct !DISubprogram(name: "klu_sort", scope: !1, file: !1, line: 91, type: !22, isLocal: false, isDefinition: true, scopeLine: 97, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !107)
!22 = !DISubroutineType(types: !23)
!23 = !{!14, !24, !45, !75}
!24 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !25, size: 64)
!25 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_symbolic", file: !26, line: 54, baseType: !27)
!26 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!27 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !26, line: 23, size: 768, elements: !28)
!28 = !{!29, !30, !31, !32, !33, !34, !35, !36, !37, !38, !39, !40, !41, !42, !43, !44}
!29 = !DIDerivedType(tag: DW_TAG_member, name: "symmetry", scope: !27, file: !26, line: 31, baseType: !9, size: 64)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "est_flops", scope: !27, file: !26, line: 32, baseType: !9, size: 64, offset: 64)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !27, file: !26, line: 33, baseType: !9, size: 64, offset: 128)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !27, file: !26, line: 33, baseType: !9, size: 64, offset: 192)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "Lnz", scope: !27, file: !26, line: 34, baseType: !15, size: 64, offset: 256)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !27, file: !26, line: 38, baseType: !14, size: 32, offset: 320)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "nz", scope: !27, file: !26, line: 39, baseType: !14, size: 32, offset: 352)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "P", scope: !27, file: !26, line: 40, baseType: !13, size: 64, offset: 384)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "Q", scope: !27, file: !26, line: 41, baseType: !13, size: 64, offset: 448)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "R", scope: !27, file: !26, line: 42, baseType: !13, size: 64, offset: 512)
!39 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !27, file: !26, line: 43, baseType: !14, size: 32, offset: 576)
!40 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !27, file: !26, line: 44, baseType: !14, size: 32, offset: 608)
!41 = !DIDerivedType(tag: DW_TAG_member, name: "maxblock", scope: !27, file: !26, line: 45, baseType: !14, size: 32, offset: 640)
!42 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !27, file: !26, line: 46, baseType: !14, size: 32, offset: 672)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "do_btf", scope: !27, file: !26, line: 47, baseType: !14, size: 32, offset: 704)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !27, file: !26, line: 50, baseType: !14, size: 32, offset: 736)
!45 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !46, size: 64)
!46 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_numeric", file: !26, line: 107, baseType: !47)
!47 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !26, line: 69, size: 1344, elements: !48)
!48 = !{!49, !50, !51, !52, !53, !54, !55, !56, !57, !58, !59, !60, !61, !63, !65, !66, !67, !68, !69, !70, !71, !72, !73, !74}
!49 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !47, file: !26, line: 74, baseType: !14, size: 32)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !47, file: !26, line: 75, baseType: !14, size: 32, offset: 32)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !47, file: !26, line: 76, baseType: !14, size: 32, offset: 64)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !47, file: !26, line: 77, baseType: !14, size: 32, offset: 96)
!53 = !DIDerivedType(tag: DW_TAG_member, name: "max_lnz_block", scope: !47, file: !26, line: 78, baseType: !14, size: 32, offset: 128)
!54 = !DIDerivedType(tag: DW_TAG_member, name: "max_unz_block", scope: !47, file: !26, line: 79, baseType: !14, size: 32, offset: 160)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "Pnum", scope: !47, file: !26, line: 80, baseType: !13, size: 64, offset: 192)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "Pinv", scope: !47, file: !26, line: 81, baseType: !13, size: 64, offset: 256)
!57 = !DIDerivedType(tag: DW_TAG_member, name: "Lip", scope: !47, file: !26, line: 84, baseType: !13, size: 64, offset: 320)
!58 = !DIDerivedType(tag: DW_TAG_member, name: "Uip", scope: !47, file: !26, line: 85, baseType: !13, size: 64, offset: 384)
!59 = !DIDerivedType(tag: DW_TAG_member, name: "Llen", scope: !47, file: !26, line: 86, baseType: !13, size: 64, offset: 448)
!60 = !DIDerivedType(tag: DW_TAG_member, name: "Ulen", scope: !47, file: !26, line: 87, baseType: !13, size: 64, offset: 512)
!61 = !DIDerivedType(tag: DW_TAG_member, name: "LUbx", scope: !47, file: !26, line: 88, baseType: !62, size: 64, offset: 576)
!62 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !4, size: 64)
!63 = !DIDerivedType(tag: DW_TAG_member, name: "LUsize", scope: !47, file: !26, line: 89, baseType: !64, size: 64, offset: 640)
!64 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !10, size: 64)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "Udiag", scope: !47, file: !26, line: 90, baseType: !4, size: 64, offset: 704)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "Rs", scope: !47, file: !26, line: 93, baseType: !15, size: 64, offset: 768)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "worksize", scope: !47, file: !26, line: 96, baseType: !10, size: 64, offset: 832)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "Work", scope: !47, file: !26, line: 97, baseType: !4, size: 64, offset: 896)
!69 = !DIDerivedType(tag: DW_TAG_member, name: "Xwork", scope: !47, file: !26, line: 98, baseType: !4, size: 64, offset: 960)
!70 = !DIDerivedType(tag: DW_TAG_member, name: "Iwork", scope: !47, file: !26, line: 99, baseType: !13, size: 64, offset: 1024)
!71 = !DIDerivedType(tag: DW_TAG_member, name: "Offp", scope: !47, file: !26, line: 102, baseType: !13, size: 64, offset: 1088)
!72 = !DIDerivedType(tag: DW_TAG_member, name: "Offi", scope: !47, file: !26, line: 103, baseType: !13, size: 64, offset: 1152)
!73 = !DIDerivedType(tag: DW_TAG_member, name: "Offx", scope: !47, file: !26, line: 104, baseType: !4, size: 64, offset: 1216)
!74 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !47, file: !26, line: 105, baseType: !14, size: 32, offset: 1280)
!75 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !76, size: 64)
!76 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !26, line: 207, baseType: !77)
!77 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !26, line: 137, size: 1280, elements: !78)
!78 = !{!79, !80, !81, !82, !83, !84, !85, !86, !87, !92, !93, !94, !95, !96, !97, !98, !99, !100, !101, !102, !103, !104, !105, !106}
!79 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !77, file: !26, line: 144, baseType: !9, size: 64)
!80 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !77, file: !26, line: 145, baseType: !9, size: 64, offset: 64)
!81 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !77, file: !26, line: 146, baseType: !9, size: 64, offset: 128)
!82 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !77, file: !26, line: 147, baseType: !9, size: 64, offset: 192)
!83 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !77, file: !26, line: 148, baseType: !9, size: 64, offset: 256)
!84 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !77, file: !26, line: 150, baseType: !14, size: 32, offset: 320)
!85 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !77, file: !26, line: 151, baseType: !14, size: 32, offset: 352)
!86 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !77, file: !26, line: 153, baseType: !14, size: 32, offset: 384)
!87 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !77, file: !26, line: 157, baseType: !88, size: 64, offset: 448)
!88 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !89, size: 64)
!89 = !DISubroutineType(types: !90)
!90 = !{!14, !14, !13, !13, !13, !91}
!91 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !77, size: 64)
!92 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !77, file: !26, line: 162, baseType: !4, size: 64, offset: 512)
!93 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !77, file: !26, line: 164, baseType: !14, size: 32, offset: 576)
!94 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !77, file: !26, line: 177, baseType: !14, size: 32, offset: 608)
!95 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !77, file: !26, line: 178, baseType: !14, size: 32, offset: 640)
!96 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !77, file: !26, line: 180, baseType: !14, size: 32, offset: 672)
!97 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !77, file: !26, line: 185, baseType: !14, size: 32, offset: 704)
!98 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !77, file: !26, line: 191, baseType: !14, size: 32, offset: 736)
!99 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !77, file: !26, line: 196, baseType: !14, size: 32, offset: 768)
!100 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !77, file: !26, line: 198, baseType: !9, size: 64, offset: 832)
!101 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !77, file: !26, line: 199, baseType: !9, size: 64, offset: 896)
!102 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !77, file: !26, line: 200, baseType: !9, size: 64, offset: 960)
!103 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !77, file: !26, line: 201, baseType: !9, size: 64, offset: 1024)
!104 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !77, file: !26, line: 202, baseType: !9, size: 64, offset: 1088)
!105 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !77, file: !26, line: 204, baseType: !10, size: 64, offset: 1152)
!106 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !77, file: !26, line: 205, baseType: !10, size: 64, offset: 1216)
!107 = !{!108, !109, !110, !111, !112, !113, !114, !115, !116, !117, !118, !119, !120, !121, !122, !123, !124, !125, !126, !127, !128}
!108 = !DILocalVariable(name: "Symbolic", arg: 1, scope: !21, file: !1, line: 93, type: !24)
!109 = !DILocalVariable(name: "Numeric", arg: 2, scope: !21, file: !1, line: 94, type: !45)
!110 = !DILocalVariable(name: "Common", arg: 3, scope: !21, file: !1, line: 95, type: !75)
!111 = !DILocalVariable(name: "R", scope: !21, file: !1, line: 98, type: !13)
!112 = !DILocalVariable(name: "W", scope: !21, file: !1, line: 98, type: !13)
!113 = !DILocalVariable(name: "Tp", scope: !21, file: !1, line: 98, type: !13)
!114 = !DILocalVariable(name: "Ti", scope: !21, file: !1, line: 98, type: !13)
!115 = !DILocalVariable(name: "Lip", scope: !21, file: !1, line: 98, type: !13)
!116 = !DILocalVariable(name: "Uip", scope: !21, file: !1, line: 98, type: !13)
!117 = !DILocalVariable(name: "Llen", scope: !21, file: !1, line: 98, type: !13)
!118 = !DILocalVariable(name: "Ulen", scope: !21, file: !1, line: 98, type: !13)
!119 = !DILocalVariable(name: "Tx", scope: !21, file: !1, line: 99, type: !15)
!120 = !DILocalVariable(name: "LUbx", scope: !21, file: !1, line: 100, type: !5)
!121 = !DILocalVariable(name: "n", scope: !21, file: !1, line: 101, type: !14)
!122 = !DILocalVariable(name: "nk", scope: !21, file: !1, line: 101, type: !14)
!123 = !DILocalVariable(name: "nz", scope: !21, file: !1, line: 101, type: !14)
!124 = !DILocalVariable(name: "block", scope: !21, file: !1, line: 101, type: !14)
!125 = !DILocalVariable(name: "nblocks", scope: !21, file: !1, line: 101, type: !14)
!126 = !DILocalVariable(name: "maxblock", scope: !21, file: !1, line: 101, type: !14)
!127 = !DILocalVariable(name: "k1", scope: !21, file: !1, line: 101, type: !14)
!128 = !DILocalVariable(name: "m1", scope: !21, file: !1, line: 102, type: !10)
!129 = !DILocation(line: 93, column: 19, scope: !21)
!130 = !DILocation(line: 94, column: 18, scope: !21)
!131 = !DILocation(line: 95, column: 17, scope: !21)
!132 = !DILocation(line: 104, column: 16, scope: !133)
!133 = distinct !DILexicalBlock(scope: !21, file: !1, line: 104, column: 9)
!134 = !DILocation(line: 104, column: 9, scope: !21)
!135 = !DILocation(line: 108, column: 13, scope: !21)
!136 = !DILocation(line: 108, column: 20, scope: !21)
!137 = !{!138, !142, i64 76}
!138 = !{!"klu_common_struct", !139, i64 0, !139, i64 8, !139, i64 16, !139, i64 24, !139, i64 32, !142, i64 40, !142, i64 44, !142, i64 48, !143, i64 56, !143, i64 64, !142, i64 72, !142, i64 76, !142, i64 80, !142, i64 84, !142, i64 88, !142, i64 92, !142, i64 96, !139, i64 104, !139, i64 112, !139, i64 120, !139, i64 128, !139, i64 136, !144, i64 144, !144, i64 152}
!139 = !{!"double", !140, i64 0}
!140 = !{!"omnipotent char", !141, i64 0}
!141 = !{!"Simple C/C++ TBAA"}
!142 = !{!"int", !140, i64 0}
!143 = !{!"any pointer", !140, i64 0}
!144 = !{!"long", !140, i64 0}
!145 = !DILocation(line: 101, column: 9, scope: !21)
!146 = !DILocation(line: 111, column: 19, scope: !21)
!147 = !{!148, !143, i64 64}
!148 = !{!"", !139, i64 0, !139, i64 8, !139, i64 16, !139, i64 24, !143, i64 32, !142, i64 40, !142, i64 44, !143, i64 48, !143, i64 56, !143, i64 64, !142, i64 72, !142, i64 76, !142, i64 80, !142, i64 84, !142, i64 88, !142, i64 92}
!149 = !DILocation(line: 98, column: 10, scope: !21)
!150 = !DILocation(line: 112, column: 25, scope: !21)
!151 = !{!148, !142, i64 76}
!152 = !DILocation(line: 101, column: 27, scope: !21)
!153 = !DILocation(line: 113, column: 26, scope: !21)
!154 = !{!148, !142, i64 80}
!155 = !DILocation(line: 101, column: 36, scope: !21)
!156 = !DILocation(line: 115, column: 21, scope: !21)
!157 = !{!158, !143, i64 40}
!158 = !{!"", !142, i64 0, !142, i64 4, !142, i64 8, !142, i64 12, !142, i64 16, !142, i64 20, !143, i64 24, !143, i64 32, !143, i64 40, !143, i64 48, !143, i64 56, !143, i64 64, !143, i64 72, !143, i64 80, !143, i64 88, !143, i64 96, !144, i64 104, !143, i64 112, !143, i64 120, !143, i64 128, !143, i64 136, !143, i64 144, !143, i64 152, !142, i64 160}
!159 = !DILocation(line: 98, column: 28, scope: !21)
!160 = !DILocation(line: 116, column: 21, scope: !21)
!161 = !{!158, !143, i64 56}
!162 = !DILocation(line: 98, column: 40, scope: !21)
!163 = !DILocation(line: 117, column: 21, scope: !21)
!164 = !{!158, !143, i64 48}
!165 = !DILocation(line: 98, column: 34, scope: !21)
!166 = !DILocation(line: 118, column: 21, scope: !21)
!167 = !{!158, !143, i64 64}
!168 = !DILocation(line: 98, column: 47, scope: !21)
!169 = !DILocation(line: 119, column: 31, scope: !21)
!170 = !{!158, !143, i64 72}
!171 = !DILocation(line: 100, column: 12, scope: !21)
!172 = !DILocation(line: 121, column: 11, scope: !21)
!173 = !DILocation(line: 121, column: 30, scope: !21)
!174 = !DILocation(line: 102, column: 12, scope: !21)
!175 = !DILocation(line: 124, column: 10, scope: !21)
!176 = !{!158, !142, i64 16}
!177 = !{!158, !142, i64 20}
!178 = !DILocation(line: 101, column: 16, scope: !21)
!179 = !DILocation(line: 125, column: 10, scope: !21)
!180 = !DILocation(line: 98, column: 14, scope: !21)
!181 = !DILocation(line: 126, column: 10, scope: !21)
!182 = !DILocation(line: 98, column: 18, scope: !21)
!183 = !DILocation(line: 127, column: 22, scope: !21)
!184 = !DILocation(line: 127, column: 10, scope: !21)
!185 = !DILocation(line: 98, column: 23, scope: !21)
!186 = !DILocation(line: 128, column: 10, scope: !21)
!187 = !DILocation(line: 99, column: 12, scope: !21)
!188 = !DILocation(line: 132, column: 17, scope: !189)
!189 = distinct !DILexicalBlock(scope: !21, file: !1, line: 132, column: 9)
!190 = !DILocation(line: 132, column: 24, scope: !189)
!191 = !DILocation(line: 135, column: 32, scope: !192)
!192 = distinct !DILexicalBlock(scope: !193, file: !1, line: 135, column: 9)
!193 = distinct !DILexicalBlock(scope: !194, file: !1, line: 135, column: 9)
!194 = distinct !DILexicalBlock(scope: !189, file: !1, line: 133, column: 5)
!195 = !DILocation(line: 132, column: 9, scope: !21)
!196 = !DILocation(line: 101, column: 20, scope: !21)
!197 = !DILocation(line: 135, column: 9, scope: !193)
!198 = !DILocation(line: 137, column: 18, scope: !199)
!199 = distinct !DILexicalBlock(scope: !192, file: !1, line: 136, column: 9)
!200 = !{!142, !142, i64 0}
!201 = !DILocation(line: 101, column: 46, scope: !21)
!202 = !DILocation(line: 138, column: 26, scope: !199)
!203 = !DILocation(line: 138, column: 18, scope: !199)
!204 = !DILocation(line: 138, column: 30, scope: !199)
!205 = !DILocation(line: 101, column: 12, scope: !21)
!206 = !DILocation(line: 139, column: 20, scope: !207)
!207 = distinct !DILexicalBlock(scope: !199, file: !1, line: 139, column: 17)
!208 = !DILocation(line: 139, column: 17, scope: !199)
!209 = !DILocation(line: 142, column: 31, scope: !210)
!210 = distinct !DILexicalBlock(scope: !207, file: !1, line: 140, column: 13)
!211 = !DILocation(line: 142, column: 42, scope: !210)
!212 = !DILocation(line: 142, column: 48, scope: !210)
!213 = !{!143, !143, i64 0}
!214 = !DILocation(line: 142, column: 17, scope: !210)
!215 = !DILocation(line: 143, column: 31, scope: !210)
!216 = !DILocation(line: 143, column: 42, scope: !210)
!217 = !DILocation(line: 143, column: 48, scope: !210)
!218 = !DILocation(line: 143, column: 17, scope: !210)
!219 = !DILocation(line: 144, column: 13, scope: !210)
!220 = distinct !{!220, !197, !221}
!221 = !DILocation(line: 145, column: 9, scope: !193)
!222 = !DILocation(line: 151, column: 5, scope: !21)
!223 = !DILocation(line: 152, column: 5, scope: !21)
!224 = !DILocation(line: 153, column: 5, scope: !21)
!225 = !DILocation(line: 154, column: 5, scope: !21)
!226 = !DILocation(line: 155, column: 21, scope: !21)
!227 = !DILocation(line: 155, column: 28, scope: !21)
!228 = !DILocation(line: 155, column: 5, scope: !21)
!229 = !DILocation(line: 0, scope: !21)
!230 = !DILocation(line: 156, column: 1, scope: !21)
!231 = distinct !DISubprogram(name: "sort", scope: !1, file: !1, line: 17, type: !232, isLocal: true, isDefinition: true, scopeLine: 19, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !234)
!232 = !DISubroutineType(types: !233)
!233 = !{null, !14, !13, !13, !6, !13, !13, !15, !13}
!234 = !{!235, !236, !237, !238, !239, !240, !241, !242, !243, !244, !245, !246, !247, !248, !249, !250, !251, !252, !253, !258, !263}
!235 = !DILocalVariable(name: "n", arg: 1, scope: !231, file: !1, line: 17, type: !14)
!236 = !DILocalVariable(name: "Xip", arg: 2, scope: !231, file: !1, line: 17, type: !13)
!237 = !DILocalVariable(name: "Xlen", arg: 3, scope: !231, file: !1, line: 17, type: !13)
!238 = !DILocalVariable(name: "LU", arg: 4, scope: !231, file: !1, line: 17, type: !6)
!239 = !DILocalVariable(name: "Tp", arg: 5, scope: !231, file: !1, line: 17, type: !13)
!240 = !DILocalVariable(name: "Tj", arg: 6, scope: !231, file: !1, line: 17, type: !13)
!241 = !DILocalVariable(name: "Tx", arg: 7, scope: !231, file: !1, line: 18, type: !15)
!242 = !DILocalVariable(name: "W", arg: 8, scope: !231, file: !1, line: 18, type: !13)
!243 = !DILocalVariable(name: "Xi", scope: !231, file: !1, line: 20, type: !13)
!244 = !DILocalVariable(name: "Xx", scope: !231, file: !1, line: 21, type: !15)
!245 = !DILocalVariable(name: "p", scope: !231, file: !1, line: 22, type: !14)
!246 = !DILocalVariable(name: "i", scope: !231, file: !1, line: 22, type: !14)
!247 = !DILocalVariable(name: "j", scope: !231, file: !1, line: 22, type: !14)
!248 = !DILocalVariable(name: "len", scope: !231, file: !1, line: 22, type: !14)
!249 = !DILocalVariable(name: "nz", scope: !231, file: !1, line: 22, type: !14)
!250 = !DILocalVariable(name: "tp", scope: !231, file: !1, line: 22, type: !14)
!251 = !DILocalVariable(name: "xlen", scope: !231, file: !1, line: 22, type: !14)
!252 = !DILocalVariable(name: "pend", scope: !231, file: !1, line: 22, type: !14)
!253 = !DILocalVariable(name: "xp", scope: !254, file: !1, line: 33, type: !6)
!254 = distinct !DILexicalBlock(scope: !255, file: !1, line: 33, column: 9)
!255 = distinct !DILexicalBlock(scope: !256, file: !1, line: 32, column: 5)
!256 = distinct !DILexicalBlock(scope: !257, file: !1, line: 31, column: 5)
!257 = distinct !DILexicalBlock(scope: !231, file: !1, line: 31, column: 5)
!258 = !DILocalVariable(name: "xp", scope: !259, file: !1, line: 56, type: !6)
!259 = distinct !DILexicalBlock(scope: !260, file: !1, line: 56, column: 9)
!260 = distinct !DILexicalBlock(scope: !261, file: !1, line: 55, column: 5)
!261 = distinct !DILexicalBlock(scope: !262, file: !1, line: 54, column: 5)
!262 = distinct !DILexicalBlock(scope: !231, file: !1, line: 54, column: 5)
!263 = !DILocalVariable(name: "xp", scope: !264, file: !1, line: 76, type: !6)
!264 = distinct !DILexicalBlock(scope: !265, file: !1, line: 76, column: 13)
!265 = distinct !DILexicalBlock(scope: !266, file: !1, line: 74, column: 9)
!266 = distinct !DILexicalBlock(scope: !267, file: !1, line: 73, column: 9)
!267 = distinct !DILexicalBlock(scope: !268, file: !1, line: 73, column: 9)
!268 = distinct !DILexicalBlock(scope: !269, file: !1, line: 71, column: 5)
!269 = distinct !DILexicalBlock(scope: !270, file: !1, line: 70, column: 5)
!270 = distinct !DILexicalBlock(scope: !231, file: !1, line: 70, column: 5)
!271 = !DILocation(line: 17, column: 23, scope: !231)
!272 = !DILocation(line: 17, column: 31, scope: !231)
!273 = !DILocation(line: 17, column: 41, scope: !231)
!274 = !DILocation(line: 17, column: 53, scope: !231)
!275 = !DILocation(line: 17, column: 62, scope: !231)
!276 = !DILocation(line: 17, column: 71, scope: !231)
!277 = !DILocation(line: 18, column: 12, scope: !231)
!278 = !DILocation(line: 18, column: 21, scope: !231)
!279 = !DILocation(line: 22, column: 12, scope: !231)
!280 = !DILocation(line: 27, column: 20, scope: !281)
!281 = distinct !DILexicalBlock(scope: !282, file: !1, line: 27, column: 5)
!282 = distinct !DILexicalBlock(scope: !231, file: !1, line: 27, column: 5)
!283 = !DILocation(line: 27, column: 5, scope: !282)
!284 = !DILocation(line: 29, column: 15, scope: !285)
!285 = distinct !DILexicalBlock(scope: !281, file: !1, line: 28, column: 5)
!286 = !DILocation(line: 22, column: 15, scope: !231)
!287 = !DILocation(line: 31, column: 5, scope: !257)
!288 = !DILocation(line: 33, column: 9, scope: !254)
!289 = !DILocation(line: 22, column: 18, scope: !231)
!290 = !DILocation(line: 20, column: 10, scope: !231)
!291 = !DILocation(line: 22, column: 9, scope: !231)
!292 = !DILocation(line: 34, column: 24, scope: !293)
!293 = distinct !DILexicalBlock(scope: !294, file: !1, line: 34, column: 9)
!294 = distinct !DILexicalBlock(scope: !255, file: !1, line: 34, column: 9)
!295 = !DILocation(line: 34, column: 9, scope: !294)
!296 = !DILocation(line: 36, column: 16, scope: !297)
!297 = distinct !DILexicalBlock(scope: !293, file: !1, line: 35, column: 9)
!298 = !DILocation(line: 36, column: 13, scope: !297)
!299 = !DILocation(line: 36, column: 23, scope: !297)
!300 = !DILocation(line: 34, column: 33, scope: !293)
!301 = distinct !{!301, !295, !302}
!302 = !DILocation(line: 37, column: 9, scope: !294)
!303 = !DILocation(line: 31, column: 27, scope: !256)
!304 = !DILocation(line: 31, column: 20, scope: !256)
!305 = distinct !{!305, !287, !306}
!306 = !DILocation(line: 38, column: 5, scope: !257)
!307 = !DILocation(line: 22, column: 23, scope: !231)
!308 = !DILocation(line: 42, column: 20, scope: !309)
!309 = distinct !DILexicalBlock(scope: !310, file: !1, line: 42, column: 5)
!310 = distinct !DILexicalBlock(scope: !231, file: !1, line: 42, column: 5)
!311 = !DILocation(line: 42, column: 5, scope: !310)
!312 = !DILocation(line: 44, column: 9, scope: !313)
!313 = distinct !DILexicalBlock(scope: !309, file: !1, line: 43, column: 5)
!314 = !DILocation(line: 44, column: 16, scope: !313)
!315 = !DILocation(line: 45, column: 15, scope: !313)
!316 = !DILocation(line: 45, column: 12, scope: !313)
!317 = !DILocation(line: 42, column: 27, scope: !309)
!318 = distinct !{!318, !311, !319}
!319 = !DILocation(line: 46, column: 5, scope: !310)
!320 = !DILocation(line: 47, column: 5, scope: !231)
!321 = !DILocation(line: 47, column: 12, scope: !231)
!322 = !DILocation(line: 48, column: 5, scope: !323)
!323 = distinct !DILexicalBlock(scope: !231, file: !1, line: 48, column: 5)
!324 = !DILocation(line: 48, column: 20, scope: !325)
!325 = distinct !DILexicalBlock(scope: !323, file: !1, line: 48, column: 5)
!326 = !DILocation(line: 50, column: 17, scope: !327)
!327 = distinct !DILexicalBlock(scope: !325, file: !1, line: 49, column: 5)
!328 = !DILocation(line: 50, column: 9, scope: !327)
!329 = !DILocation(line: 50, column: 15, scope: !327)
!330 = !DILocation(line: 48, column: 27, scope: !325)
!331 = distinct !{!331, !322, !332}
!332 = !DILocation(line: 51, column: 5, scope: !323)
!333 = !DILocation(line: 54, column: 20, scope: !261)
!334 = !DILocation(line: 54, column: 5, scope: !262)
!335 = !DILocation(line: 56, column: 9, scope: !259)
!336 = !DILocation(line: 21, column: 12, scope: !231)
!337 = !DILocation(line: 57, column: 24, scope: !338)
!338 = distinct !DILexicalBlock(scope: !339, file: !1, line: 57, column: 9)
!339 = distinct !DILexicalBlock(scope: !260, file: !1, line: 57, column: 9)
!340 = !DILocation(line: 57, column: 9, scope: !339)
!341 = !DILocation(line: 59, column: 21, scope: !342)
!342 = distinct !DILexicalBlock(scope: !338, file: !1, line: 58, column: 9)
!343 = !DILocation(line: 59, column: 18, scope: !342)
!344 = !DILocation(line: 59, column: 28, scope: !342)
!345 = !DILocation(line: 22, column: 27, scope: !231)
!346 = !DILocation(line: 60, column: 13, scope: !342)
!347 = !DILocation(line: 60, column: 21, scope: !342)
!348 = !DILocation(line: 61, column: 23, scope: !342)
!349 = !{!139, !139, i64 0}
!350 = !DILocation(line: 61, column: 13, scope: !342)
!351 = !DILocation(line: 61, column: 21, scope: !342)
!352 = !DILocation(line: 57, column: 33, scope: !338)
!353 = distinct !{!353, !340, !354}
!354 = !DILocation(line: 62, column: 9, scope: !339)
!355 = !DILocation(line: 54, column: 27, scope: !261)
!356 = distinct !{!356, !334, !357}
!357 = !DILocation(line: 63, column: 5, scope: !262)
!358 = !DILocation(line: 66, column: 20, scope: !359)
!359 = distinct !DILexicalBlock(scope: !360, file: !1, line: 66, column: 5)
!360 = distinct !DILexicalBlock(scope: !231, file: !1, line: 66, column: 5)
!361 = !DILocation(line: 66, column: 5, scope: !360)
!362 = !DILocation(line: 68, column: 15, scope: !363)
!363 = distinct !DILexicalBlock(scope: !359, file: !1, line: 67, column: 5)
!364 = !DILocation(line: 70, column: 5, scope: !270)
!365 = !DILocation(line: 72, column: 21, scope: !268)
!366 = !DILocation(line: 72, column: 16, scope: !268)
!367 = !DILocation(line: 22, column: 37, scope: !231)
!368 = !DILocation(line: 73, column: 18, scope: !267)
!369 = !DILocation(line: 73, column: 29, scope: !266)
!370 = !DILocation(line: 73, column: 9, scope: !267)
!371 = !DILocation(line: 75, column: 17, scope: !265)
!372 = !DILocation(line: 76, column: 13, scope: !264)
!373 = !DILocation(line: 77, column: 20, scope: !265)
!374 = !DILocation(line: 77, column: 25, scope: !265)
!375 = !DILocation(line: 22, column: 31, scope: !231)
!376 = !DILocation(line: 78, column: 13, scope: !265)
!377 = !DILocation(line: 78, column: 23, scope: !265)
!378 = !DILocation(line: 79, column: 25, scope: !265)
!379 = !DILocation(line: 79, column: 13, scope: !265)
!380 = !DILocation(line: 79, column: 23, scope: !265)
!381 = !DILocation(line: 73, column: 39, scope: !266)
!382 = distinct !{!382, !370, !383}
!383 = !DILocation(line: 80, column: 9, scope: !267)
!384 = !DILocation(line: 70, column: 20, scope: !269)
!385 = distinct !{!385, !364, !386}
!386 = !DILocation(line: 81, column: 5, scope: !270)
!387 = !DILocation(line: 84, column: 1, scope: !231)
