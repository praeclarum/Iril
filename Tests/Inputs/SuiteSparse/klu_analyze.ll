; ModuleID = 'klu_analyze.c'
source_filename = "klu_analyze.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_analyze(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !11 {
  %5 = alloca [20 x double], align 16
  call void @llvm.dbg.declare(metadata [20 x double]* %5, metadata !78, metadata !DIExpression()), !dbg !126
  %6 = alloca [20 x i32], align 16
  call void @llvm.dbg.declare(metadata [20 x i32]* %6, metadata !119, metadata !DIExpression()), !dbg !163
  %7 = alloca double, align 8
  %8 = alloca %struct.klu_symbolic*, align 8
  call void @llvm.dbg.value(metadata i32 %0, metadata !74, metadata !DIExpression()), !dbg !164
  call void @llvm.dbg.value(metadata i32* %1, metadata !75, metadata !DIExpression()), !dbg !165
  call void @llvm.dbg.value(metadata i32* %2, metadata !76, metadata !DIExpression()), !dbg !166
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !77, metadata !DIExpression()), !dbg !167
  %9 = icmp eq %struct.klu_common_struct* %3, null, !dbg !168
  br i1 %9, label %976, label %10, !dbg !170

; <label>:10:                                     ; preds = %4
  %11 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11, !dbg !171
  store i32 0, i32* %11, align 4, !dbg !172, !tbaa !173
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 13, !dbg !181
  store i32 -1, i32* %12, align 4, !dbg !182, !tbaa !183
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 6, !dbg !184
  %14 = load i32, i32* %13, align 4, !dbg !184, !tbaa !185
  %15 = icmp eq i32 %14, 2, !dbg !186
  br i1 %15, label %16, label %18, !dbg !187

; <label>:16:                                     ; preds = %10
  %17 = tail call %struct.klu_symbolic* @klu_analyze_given(i32 %0, i32* %1, i32* %2, i32* null, i32* null, %struct.klu_common_struct* nonnull %3) #4, !dbg !188
  br label %976, !dbg !190

; <label>:18:                                     ; preds = %10
  call void @llvm.dbg.value(metadata i32 %0, metadata !132, metadata !DIExpression()) #4, !dbg !191
  call void @llvm.dbg.value(metadata i32* %1, metadata !133, metadata !DIExpression()) #4, !dbg !192
  call void @llvm.dbg.value(metadata i32* %2, metadata !134, metadata !DIExpression()) #4, !dbg !193
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !135, metadata !DIExpression()) #4, !dbg !194
  %19 = bitcast double* %7 to i8*, !dbg !195
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %19) #4, !dbg !195
  %20 = bitcast %struct.klu_symbolic** %8 to i8*, !dbg !196
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %20) #4, !dbg !196
  %21 = tail call %struct.klu_symbolic* @klu_alloc_symbolic(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* nonnull %3) #4, !dbg !197
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %21, metadata !137, metadata !DIExpression()) #4, !dbg !198
  store %struct.klu_symbolic* %21, %struct.klu_symbolic** %8, align 8, !dbg !199, !tbaa !200
  %22 = icmp eq %struct.klu_symbolic* %21, null, !dbg !201
  br i1 %22, label %974, label %23, !dbg !203

; <label>:23:                                     ; preds = %18
  %24 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 7, !dbg !204
  %25 = load i32*, i32** %24, align 8, !dbg !204, !tbaa !205
  call void @llvm.dbg.value(metadata i32* %25, metadata !145, metadata !DIExpression()) #4, !dbg !207
  %26 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 8, !dbg !208
  %27 = load i32*, i32** %26, align 8, !dbg !208, !tbaa !209
  call void @llvm.dbg.value(metadata i32* %27, metadata !146, metadata !DIExpression()) #4, !dbg !210
  %28 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 9, !dbg !211
  %29 = load i32*, i32** %28, align 8, !dbg !211, !tbaa !212
  call void @llvm.dbg.value(metadata i32* %29, metadata !147, metadata !DIExpression()) #4, !dbg !213
  %30 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 4, !dbg !214
  %31 = load double*, double** %30, align 8, !dbg !214, !tbaa !215
  call void @llvm.dbg.value(metadata double* %31, metadata !138, metadata !DIExpression()) #4, !dbg !216
  %32 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 6, !dbg !217
  %33 = load i32, i32* %32, align 4, !dbg !217, !tbaa !218
  call void @llvm.dbg.value(metadata i32 %33, metadata !149, metadata !DIExpression()) #4, !dbg !219
  %34 = load i32, i32* %13, align 4, !dbg !220, !tbaa !185
  call void @llvm.dbg.value(metadata i32 %34, metadata !156, metadata !DIExpression()) #4, !dbg !221
  switch i32 %34, label %44 [
    i32 1, label %35
    i32 0, label %42
    i32 3, label %38
  ], !dbg !222

; <label>:35:                                     ; preds = %23
  %36 = tail call i64 @colamd_recommended(i32 %33, i32 %0, i32 %0) #4, !dbg !223
  %37 = trunc i64 %36 to i32, !dbg !223
  call void @llvm.dbg.value(metadata i32 %37, metadata !158, metadata !DIExpression()) #4, !dbg !226
  br label %46, !dbg !227

; <label>:38:                                     ; preds = %23
  %39 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 8, !dbg !228
  %40 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %39, align 8, !dbg !228, !tbaa !230
  %41 = icmp eq i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)* %40, null, !dbg !231
  br i1 %41, label %44, label %42, !dbg !232

; <label>:42:                                     ; preds = %38, %23
  %43 = add nsw i32 %33, 1, !dbg !233
  call void @llvm.dbg.value(metadata i32 %43, metadata !158, metadata !DIExpression()) #4, !dbg !226
  br label %46

; <label>:44:                                     ; preds = %38, %23
  store i32 -3, i32* %11, align 4, !dbg !235, !tbaa !173
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %8, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %45 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #4, !dbg !237
  br label %974, !dbg !238

; <label>:46:                                     ; preds = %42, %35
  %47 = phi i32 [ %37, %35 ], [ %43, %42 ], !dbg !239
  call void @llvm.dbg.value(metadata i32 %47, metadata !158, metadata !DIExpression()) #4, !dbg !226
  %48 = sext i32 %0 to i64, !dbg !240
  %49 = tail call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !241
  %50 = bitcast i8* %49 to i32*, !dbg !241
  call void @llvm.dbg.value(metadata i32* %50, metadata !144, metadata !DIExpression()) #4, !dbg !242
  %51 = tail call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !243
  %52 = bitcast i8* %51 to i32*, !dbg !243
  call void @llvm.dbg.value(metadata i32* %52, metadata !139, metadata !DIExpression()) #4, !dbg !244
  %53 = load i32, i32* %11, align 4, !dbg !245, !tbaa !173
  %54 = icmp slt i32 %53, 0, !dbg !247
  br i1 %54, label %55, label %59, !dbg !248

; <label>:55:                                     ; preds = %46
  %56 = tail call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !249
  %57 = tail call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !251
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %8, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %58 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #4, !dbg !252
  br label %974, !dbg !253

; <label>:59:                                     ; preds = %46
  %60 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 5, !dbg !254
  %61 = load i32, i32* %60, align 8, !dbg !254, !tbaa !255
  call void @llvm.dbg.value(metadata i32 %61, metadata !155, metadata !DIExpression()) #4, !dbg !256
  %62 = icmp ne i32 %61, 0, !dbg !257
  %63 = zext i1 %62 to i32, !dbg !257
  call void @llvm.dbg.value(metadata i32 %63, metadata !155, metadata !DIExpression()) #4, !dbg !256
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %21, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %64 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 13, !dbg !258
  store i32 %34, i32* %64, align 4, !dbg !259, !tbaa !260
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %21, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %65 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 14, !dbg !261
  store i32 %63, i32* %65, align 8, !dbg !262, !tbaa !263
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %21, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %66 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 15, !dbg !264
  store i32 -1, i32* %66, align 4, !dbg !265, !tbaa !266
  %67 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 21, !dbg !267
  store double 0.000000e+00, double* %67, align 8, !dbg !268, !tbaa !269
  br i1 %62, label %68, label %285, !dbg !270

; <label>:68:                                     ; preds = %59
  %69 = mul nsw i32 %0, 5, !dbg !271
  %70 = sext i32 %69 to i64, !dbg !274
  %71 = tail call i8* @klu_malloc(i64 %70, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !275
  %72 = load i32, i32* %11, align 4, !dbg !276, !tbaa !173
  %73 = icmp slt i32 %72, 0, !dbg !278
  br i1 %73, label %74, label %78, !dbg !279

; <label>:74:                                     ; preds = %68
  %75 = tail call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !280
  %76 = tail call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !282
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %8, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %77 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #4, !dbg !283
  br label %974, !dbg !284

; <label>:78:                                     ; preds = %68
  %79 = bitcast i8* %71 to i32*, !dbg !275
  call void @llvm.dbg.value(metadata i32* %79, metadata !159, metadata !DIExpression()) #4, !dbg !285
  %80 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 4, !dbg !286
  %81 = load double, double* %80, align 8, !dbg !286, !tbaa !287
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %21, metadata !137, metadata !DIExpression()) #4, !dbg !198
  call void @llvm.dbg.value(metadata double* %7, metadata !136, metadata !DIExpression()) #4, !dbg !288
  %82 = call i32 @btf_order(i32 %0, i32* %1, i32* %2, double %81, double* nonnull %7, i32* %50, i32* %52, i32* %29, i32* nonnull %66, i32* %79) #4, !dbg !289
  call void @llvm.dbg.value(metadata i32 %82, metadata !148, metadata !DIExpression()) #4, !dbg !290
  %83 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !dbg !291, !tbaa !200
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %83, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %84 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %83, i64 0, i32 15, !dbg !292
  %85 = load i32, i32* %84, align 4, !dbg !292, !tbaa !266
  store i32 %85, i32* %12, align 4, !dbg !293, !tbaa !183
  %86 = load double, double* %7, align 8, !dbg !294, !tbaa !295
  call void @llvm.dbg.value(metadata double %86, metadata !136, metadata !DIExpression()) #4, !dbg !288
  %87 = load double, double* %67, align 8, !dbg !296, !tbaa !269
  %88 = fadd double %86, %87, !dbg !296
  store double %88, double* %67, align 8, !dbg !296, !tbaa !269
  %89 = call i8* @klu_free(i8* %71, i64 %70, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !297
  %90 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !dbg !298, !tbaa !200
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %90, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %91 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %90, i64 0, i32 15, !dbg !300
  %92 = load i32, i32* %91, align 4, !dbg !300, !tbaa !266
  %93 = icmp slt i32 %92, %0, !dbg !301
  %94 = icmp sgt i32 %0, 0, !dbg !302
  %95 = and i1 %94, %93, !dbg !306
  call void @llvm.dbg.value(metadata i32 0, metadata !157, metadata !DIExpression()) #4, !dbg !307
  br i1 %95, label %96, label %174, !dbg !306

; <label>:96:                                     ; preds = %78
  %97 = zext i32 %0 to i64
  %98 = icmp ult i32 %0, 8, !dbg !308
  br i1 %98, label %163, label %99, !dbg !308

; <label>:99:                                     ; preds = %96
  %100 = and i64 %97, 4294967288, !dbg !308
  %101 = add nsw i64 %100, -8, !dbg !308
  %102 = lshr exact i64 %101, 3, !dbg !308
  %103 = add nuw nsw i64 %102, 1, !dbg !308
  %104 = and i64 %103, 1, !dbg !308
  %105 = icmp eq i64 %101, 0, !dbg !308
  br i1 %105, label %143, label %106, !dbg !308

; <label>:106:                                    ; preds = %99
  %107 = sub nsw i64 %103, %104, !dbg !308
  br label %108, !dbg !308

; <label>:108:                                    ; preds = %108, %106
  %109 = phi i64 [ 0, %106 ], [ %140, %108 ], !dbg !309
  %110 = phi i64 [ %107, %106 ], [ %141, %108 ]
  %111 = getelementptr inbounds i32, i32* %52, i64 %109, !dbg !310
  %112 = bitcast i32* %111 to <4 x i32>*, !dbg !310
  %113 = load <4 x i32>, <4 x i32>* %112, align 4, !dbg !310, !tbaa !312
  %114 = getelementptr i32, i32* %111, i64 4, !dbg !310
  %115 = bitcast i32* %114 to <4 x i32>*, !dbg !310
  %116 = load <4 x i32>, <4 x i32>* %115, align 4, !dbg !310, !tbaa !312
  %117 = icmp slt <4 x i32> %113, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %118 = icmp slt <4 x i32> %116, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %119 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %113, !dbg !310
  %120 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %116, !dbg !310
  %121 = select <4 x i1> %117, <4 x i32> %119, <4 x i32> %113, !dbg !310
  %122 = select <4 x i1> %118, <4 x i32> %120, <4 x i32> %116, !dbg !310
  %123 = bitcast i32* %111 to <4 x i32>*, !dbg !313
  store <4 x i32> %121, <4 x i32>* %123, align 4, !dbg !313, !tbaa !312
  %124 = bitcast i32* %114 to <4 x i32>*, !dbg !313
  store <4 x i32> %122, <4 x i32>* %124, align 4, !dbg !313, !tbaa !312
  %125 = or i64 %109, 8, !dbg !309
  %126 = getelementptr inbounds i32, i32* %52, i64 %125, !dbg !310
  %127 = bitcast i32* %126 to <4 x i32>*, !dbg !310
  %128 = load <4 x i32>, <4 x i32>* %127, align 4, !dbg !310, !tbaa !312
  %129 = getelementptr i32, i32* %126, i64 4, !dbg !310
  %130 = bitcast i32* %129 to <4 x i32>*, !dbg !310
  %131 = load <4 x i32>, <4 x i32>* %130, align 4, !dbg !310, !tbaa !312
  %132 = icmp slt <4 x i32> %128, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %133 = icmp slt <4 x i32> %131, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %134 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %128, !dbg !310
  %135 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %131, !dbg !310
  %136 = select <4 x i1> %132, <4 x i32> %134, <4 x i32> %128, !dbg !310
  %137 = select <4 x i1> %133, <4 x i32> %135, <4 x i32> %131, !dbg !310
  %138 = bitcast i32* %126 to <4 x i32>*, !dbg !313
  store <4 x i32> %136, <4 x i32>* %138, align 4, !dbg !313, !tbaa !312
  %139 = bitcast i32* %129 to <4 x i32>*, !dbg !313
  store <4 x i32> %137, <4 x i32>* %139, align 4, !dbg !313, !tbaa !312
  %140 = add i64 %109, 16, !dbg !309
  %141 = add i64 %110, -2, !dbg !309
  %142 = icmp eq i64 %141, 0, !dbg !309
  br i1 %142, label %143, label %108, !dbg !309, !llvm.loop !314

; <label>:143:                                    ; preds = %108, %99
  %144 = phi i64 [ 0, %99 ], [ %140, %108 ]
  %145 = icmp eq i64 %104, 0, !dbg !309
  br i1 %145, label %161, label %146, !dbg !309

; <label>:146:                                    ; preds = %143
  %147 = getelementptr inbounds i32, i32* %52, i64 %144, !dbg !310
  %148 = bitcast i32* %147 to <4 x i32>*, !dbg !310
  %149 = load <4 x i32>, <4 x i32>* %148, align 4, !dbg !310, !tbaa !312
  %150 = getelementptr i32, i32* %147, i64 4, !dbg !310
  %151 = bitcast i32* %150 to <4 x i32>*, !dbg !310
  %152 = load <4 x i32>, <4 x i32>* %151, align 4, !dbg !310, !tbaa !312
  %153 = icmp slt <4 x i32> %149, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %154 = icmp slt <4 x i32> %152, <i32 -1, i32 -1, i32 -1, i32 -1>, !dbg !310
  %155 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %149, !dbg !310
  %156 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %152, !dbg !310
  %157 = select <4 x i1> %153, <4 x i32> %155, <4 x i32> %149, !dbg !310
  %158 = select <4 x i1> %154, <4 x i32> %156, <4 x i32> %152, !dbg !310
  %159 = bitcast i32* %147 to <4 x i32>*, !dbg !313
  store <4 x i32> %157, <4 x i32>* %159, align 4, !dbg !313, !tbaa !312
  %160 = bitcast i32* %150 to <4 x i32>*, !dbg !313
  store <4 x i32> %158, <4 x i32>* %160, align 4, !dbg !313, !tbaa !312
  br label %161

; <label>:161:                                    ; preds = %143, %146
  %162 = icmp eq i64 %100, %97
  br i1 %162, label %174, label %163, !dbg !308

; <label>:163:                                    ; preds = %161, %96
  %164 = phi i64 [ 0, %96 ], [ %100, %161 ]
  br label %165, !dbg !310

; <label>:165:                                    ; preds = %163, %165
  %166 = phi i64 [ %172, %165 ], [ %164, %163 ]
  call void @llvm.dbg.value(metadata i64 %166, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %167 = getelementptr inbounds i32, i32* %52, i64 %166, !dbg !310
  %168 = load i32, i32* %167, align 4, !dbg !310, !tbaa !312
  %169 = icmp slt i32 %168, -1, !dbg !310
  %170 = sub i32 -2, %168, !dbg !310
  %171 = select i1 %169, i32 %170, i32 %168, !dbg !310
  store i32 %171, i32* %167, align 4, !dbg !313, !tbaa !312
  %172 = add nuw nsw i64 %166, 1, !dbg !309
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  %173 = icmp eq i64 %172, %97, !dbg !302
  br i1 %173, label %174, label %165, !dbg !308, !llvm.loop !318

; <label>:174:                                    ; preds = %165, %161, %78
  call void @llvm.dbg.value(metadata i32 1, metadata !151, metadata !DIExpression()) #4, !dbg !320
  call void @llvm.dbg.value(metadata i32 0, metadata !150, metadata !DIExpression()) #4, !dbg !321
  %175 = icmp sgt i32 %82, 0, !dbg !322
  br i1 %175, label %176, label %390, !dbg !325

; <label>:176:                                    ; preds = %174
  %177 = load i32, i32* %29, align 4, !dbg !326, !tbaa !312
  %178 = zext i32 %82 to i64
  %179 = icmp ult i32 %82, 8, !dbg !325
  br i1 %179, label %270, label %180, !dbg !325

; <label>:180:                                    ; preds = %176
  %181 = and i64 %178, 4294967288, !dbg !325
  %182 = insertelement <4 x i32> undef, i32 %177, i32 3, !dbg !325
  %183 = add nsw i64 %181, -8, !dbg !325
  %184 = lshr exact i64 %183, 3, !dbg !325
  %185 = add nuw nsw i64 %184, 1, !dbg !325
  %186 = and i64 %185, 1, !dbg !325
  %187 = icmp eq i64 %183, 0, !dbg !325
  br i1 %187, label %231, label %188, !dbg !325

; <label>:188:                                    ; preds = %180
  %189 = sub nsw i64 %185, %186, !dbg !325
  br label %190, !dbg !325

; <label>:190:                                    ; preds = %190, %188
  %191 = phi i64 [ 0, %188 ], [ %226, %190 ], !dbg !328
  %192 = phi <4 x i32> [ %182, %188 ], [ %217, %190 ]
  %193 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %188 ], [ %224, %190 ]
  %194 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %188 ], [ %225, %190 ]
  %195 = phi i64 [ %189, %188 ], [ %227, %190 ]
  %196 = or i64 %191, 1, !dbg !328
  %197 = getelementptr inbounds i32, i32* %29, i64 %196, !dbg !329
  %198 = bitcast i32* %197 to <4 x i32>*, !dbg !329
  %199 = load <4 x i32>, <4 x i32>* %198, align 4, !dbg !329, !tbaa !312
  %200 = getelementptr i32, i32* %197, i64 4, !dbg !329
  %201 = bitcast i32* %200 to <4 x i32>*, !dbg !329
  %202 = load <4 x i32>, <4 x i32>* %201, align 4, !dbg !329, !tbaa !312
  %203 = shufflevector <4 x i32> %192, <4 x i32> %199, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %204 = shufflevector <4 x i32> %199, <4 x i32> %202, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %205 = sub nsw <4 x i32> %199, %203, !dbg !330
  %206 = sub nsw <4 x i32> %202, %204, !dbg !330
  %207 = icmp sgt <4 x i32> %193, %205, !dbg !331
  %208 = icmp sgt <4 x i32> %194, %206, !dbg !331
  %209 = select <4 x i1> %207, <4 x i32> %193, <4 x i32> %205, !dbg !331
  %210 = select <4 x i1> %208, <4 x i32> %194, <4 x i32> %206, !dbg !331
  %211 = or i64 %191, 9, !dbg !328
  %212 = getelementptr inbounds i32, i32* %29, i64 %211, !dbg !329
  %213 = bitcast i32* %212 to <4 x i32>*, !dbg !329
  %214 = load <4 x i32>, <4 x i32>* %213, align 4, !dbg !329, !tbaa !312
  %215 = getelementptr i32, i32* %212, i64 4, !dbg !329
  %216 = bitcast i32* %215 to <4 x i32>*, !dbg !329
  %217 = load <4 x i32>, <4 x i32>* %216, align 4, !dbg !329, !tbaa !312
  %218 = shufflevector <4 x i32> %202, <4 x i32> %214, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %219 = shufflevector <4 x i32> %214, <4 x i32> %217, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %220 = sub nsw <4 x i32> %214, %218, !dbg !330
  %221 = sub nsw <4 x i32> %217, %219, !dbg !330
  %222 = icmp sgt <4 x i32> %209, %220, !dbg !331
  %223 = icmp sgt <4 x i32> %210, %221, !dbg !331
  %224 = select <4 x i1> %222, <4 x i32> %209, <4 x i32> %220, !dbg !331
  %225 = select <4 x i1> %223, <4 x i32> %210, <4 x i32> %221, !dbg !331
  %226 = add i64 %191, 16, !dbg !328
  %227 = add i64 %195, -2, !dbg !328
  %228 = icmp eq i64 %227, 0, !dbg !328
  br i1 %228, label %229, label %190, !dbg !328, !llvm.loop !332

; <label>:229:                                    ; preds = %190
  %230 = or i64 %226, 1, !dbg !328
  br label %231, !dbg !328

; <label>:231:                                    ; preds = %229, %180
  %232 = phi <4 x i32> [ undef, %180 ], [ %217, %229 ]
  %233 = phi <4 x i32> [ undef, %180 ], [ %224, %229 ]
  %234 = phi <4 x i32> [ undef, %180 ], [ %225, %229 ]
  %235 = phi i64 [ 1, %180 ], [ %230, %229 ]
  %236 = phi <4 x i32> [ %182, %180 ], [ %217, %229 ]
  %237 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %180 ], [ %224, %229 ]
  %238 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %180 ], [ %225, %229 ]
  %239 = icmp eq i64 %186, 0, !dbg !328
  br i1 %239, label %255, label %240, !dbg !328

; <label>:240:                                    ; preds = %231
  %241 = getelementptr inbounds i32, i32* %29, i64 %235, !dbg !329
  %242 = bitcast i32* %241 to <4 x i32>*, !dbg !329
  %243 = load <4 x i32>, <4 x i32>* %242, align 4, !dbg !329, !tbaa !312
  %244 = getelementptr i32, i32* %241, i64 4, !dbg !329
  %245 = bitcast i32* %244 to <4 x i32>*, !dbg !329
  %246 = load <4 x i32>, <4 x i32>* %245, align 4, !dbg !329, !tbaa !312
  %247 = shufflevector <4 x i32> %236, <4 x i32> %243, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %248 = shufflevector <4 x i32> %243, <4 x i32> %246, <4 x i32> <i32 3, i32 4, i32 5, i32 6>, !dbg !330
  %249 = sub nsw <4 x i32> %243, %247, !dbg !330
  %250 = sub nsw <4 x i32> %246, %248, !dbg !330
  %251 = icmp sgt <4 x i32> %238, %250, !dbg !331
  %252 = select <4 x i1> %251, <4 x i32> %238, <4 x i32> %250, !dbg !331
  %253 = icmp sgt <4 x i32> %237, %249, !dbg !331
  %254 = select <4 x i1> %253, <4 x i32> %237, <4 x i32> %249, !dbg !331
  br label %255, !dbg !331

; <label>:255:                                    ; preds = %231, %240
  %256 = phi <4 x i32> [ %232, %231 ], [ %246, %240 ]
  %257 = phi <4 x i32> [ %233, %231 ], [ %254, %240 ]
  %258 = phi <4 x i32> [ %234, %231 ], [ %252, %240 ]
  %259 = icmp sgt <4 x i32> %257, %258, !dbg !331
  %260 = select <4 x i1> %259, <4 x i32> %257, <4 x i32> %258, !dbg !331
  %261 = shufflevector <4 x i32> %260, <4 x i32> undef, <4 x i32> <i32 2, i32 3, i32 undef, i32 undef>, !dbg !331
  %262 = icmp sgt <4 x i32> %260, %261, !dbg !331
  %263 = select <4 x i1> %262, <4 x i32> %260, <4 x i32> %261, !dbg !331
  %264 = shufflevector <4 x i32> %263, <4 x i32> undef, <4 x i32> <i32 1, i32 undef, i32 undef, i32 undef>, !dbg !331
  %265 = icmp sgt <4 x i32> %263, %264, !dbg !331
  %266 = select <4 x i1> %265, <4 x i32> %263, <4 x i32> %264, !dbg !331
  %267 = extractelement <4 x i32> %266, i32 0, !dbg !331
  %268 = icmp eq i64 %181, %178
  %269 = extractelement <4 x i32> %256, i32 3, !dbg !325
  br i1 %268, label %390, label %270, !dbg !325

; <label>:270:                                    ; preds = %255, %176
  %271 = phi i32 [ %177, %176 ], [ %269, %255 ]
  %272 = phi i64 [ 0, %176 ], [ %181, %255 ]
  %273 = phi i32 [ 1, %176 ], [ %267, %255 ]
  br label %274, !dbg !328

; <label>:274:                                    ; preds = %270, %274
  %275 = phi i32 [ %280, %274 ], [ %271, %270 ], !dbg !326
  %276 = phi i64 [ %278, %274 ], [ %272, %270 ]
  %277 = phi i32 [ %283, %274 ], [ %273, %270 ]
  call void @llvm.dbg.value(metadata i64 %276, metadata !150, metadata !DIExpression()) #4, !dbg !321
  call void @llvm.dbg.value(metadata i32 %277, metadata !151, metadata !DIExpression()) #4, !dbg !320
  call void @llvm.dbg.value(metadata i32 %275, metadata !152, metadata !DIExpression()) #4, !dbg !335
  %278 = add nuw nsw i64 %276, 1, !dbg !328
  %279 = getelementptr inbounds i32, i32* %29, i64 %278, !dbg !329
  %280 = load i32, i32* %279, align 4, !dbg !329, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %280, metadata !153, metadata !DIExpression()) #4, !dbg !336
  %281 = sub nsw i32 %280, %275, !dbg !330
  call void @llvm.dbg.value(metadata i32 %281, metadata !154, metadata !DIExpression()) #4, !dbg !337
  %282 = icmp sgt i32 %277, %281, !dbg !331
  %283 = select i1 %282, i32 %277, i32 %281, !dbg !331
  call void @llvm.dbg.value(metadata i32 undef, metadata !150, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !321
  call void @llvm.dbg.value(metadata i32 %283, metadata !151, metadata !DIExpression()) #4, !dbg !320
  %284 = icmp eq i64 %278, %178, !dbg !322
  br i1 %284, label %390, label %274, !dbg !325, !llvm.loop !338

; <label>:285:                                    ; preds = %59
  call void @llvm.dbg.value(metadata i32 1, metadata !148, metadata !DIExpression()) #4, !dbg !290
  call void @llvm.dbg.value(metadata i32 %0, metadata !151, metadata !DIExpression()) #4, !dbg !320
  store i32 0, i32* %29, align 4, !dbg !339, !tbaa !312
  %286 = getelementptr inbounds i32, i32* %29, i64 1, !dbg !341
  store i32 %0, i32* %286, align 4, !dbg !342, !tbaa !312
  call void @llvm.dbg.value(metadata i32 0, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %287 = icmp sgt i32 %0, 0, !dbg !343
  br i1 %287, label %288, label %390, !dbg !346

; <label>:288:                                    ; preds = %285
  %289 = zext i32 %0 to i64
  %290 = icmp ult i32 %0, 8, !dbg !346
  br i1 %290, label %351, label %291, !dbg !346

; <label>:291:                                    ; preds = %288
  %292 = shl nuw nsw i64 %289, 2, !dbg !346
  %293 = getelementptr i8, i8* %49, i64 %292, !dbg !346
  %294 = getelementptr i8, i8* %51, i64 %292, !dbg !346
  %295 = icmp ult i8* %49, %294, !dbg !346
  %296 = icmp ult i8* %51, %293, !dbg !346
  %297 = and i1 %295, %296, !dbg !346
  br i1 %297, label %351, label %298, !dbg !346

; <label>:298:                                    ; preds = %291
  %299 = and i64 %289, 4294967288, !dbg !346
  %300 = add nsw i64 %299, -8, !dbg !346
  %301 = lshr exact i64 %300, 3, !dbg !346
  %302 = add nuw nsw i64 %301, 1, !dbg !346
  %303 = and i64 %302, 1, !dbg !346
  %304 = icmp eq i64 %300, 0, !dbg !346
  br i1 %304, label %335, label %305, !dbg !346

; <label>:305:                                    ; preds = %298
  %306 = sub nsw i64 %302, %303, !dbg !346
  br label %307, !dbg !346

; <label>:307:                                    ; preds = %307, %305
  %308 = phi i64 [ 0, %305 ], [ %331, %307 ], !dbg !347
  %309 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %305 ], [ %332, %307 ]
  %310 = phi i64 [ %306, %305 ], [ %333, %307 ]
  %311 = getelementptr inbounds i32, i32* %50, i64 %308, !dbg !348
  %312 = add <4 x i32> %309, <i32 4, i32 4, i32 4, i32 4>, !dbg !347
  %313 = bitcast i32* %311 to <4 x i32>*, !dbg !350
  store <4 x i32> %309, <4 x i32>* %313, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %314 = getelementptr i32, i32* %311, i64 4, !dbg !350
  %315 = bitcast i32* %314 to <4 x i32>*, !dbg !350
  store <4 x i32> %312, <4 x i32>* %315, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %316 = getelementptr inbounds i32, i32* %52, i64 %308, !dbg !356
  %317 = bitcast i32* %316 to <4 x i32>*, !dbg !357
  store <4 x i32> %309, <4 x i32>* %317, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  %318 = getelementptr i32, i32* %316, i64 4, !dbg !357
  %319 = bitcast i32* %318 to <4 x i32>*, !dbg !357
  store <4 x i32> %312, <4 x i32>* %319, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  %320 = or i64 %308, 8, !dbg !347
  %321 = add <4 x i32> %309, <i32 8, i32 8, i32 8, i32 8>, !dbg !347
  %322 = getelementptr inbounds i32, i32* %50, i64 %320, !dbg !348
  %323 = add <4 x i32> %309, <i32 12, i32 12, i32 12, i32 12>, !dbg !347
  %324 = bitcast i32* %322 to <4 x i32>*, !dbg !350
  store <4 x i32> %321, <4 x i32>* %324, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %325 = getelementptr i32, i32* %322, i64 4, !dbg !350
  %326 = bitcast i32* %325 to <4 x i32>*, !dbg !350
  store <4 x i32> %323, <4 x i32>* %326, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %327 = getelementptr inbounds i32, i32* %52, i64 %320, !dbg !356
  %328 = bitcast i32* %327 to <4 x i32>*, !dbg !357
  store <4 x i32> %321, <4 x i32>* %328, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  %329 = getelementptr i32, i32* %327, i64 4, !dbg !357
  %330 = bitcast i32* %329 to <4 x i32>*, !dbg !357
  store <4 x i32> %323, <4 x i32>* %330, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  %331 = add i64 %308, 16, !dbg !347
  %332 = add <4 x i32> %309, <i32 16, i32 16, i32 16, i32 16>, !dbg !347
  %333 = add i64 %310, -2, !dbg !347
  %334 = icmp eq i64 %333, 0, !dbg !347
  br i1 %334, label %335, label %307, !dbg !347, !llvm.loop !358

; <label>:335:                                    ; preds = %307, %298
  %336 = phi i64 [ 0, %298 ], [ %331, %307 ]
  %337 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %298 ], [ %332, %307 ]
  %338 = icmp eq i64 %303, 0, !dbg !347
  br i1 %338, label %349, label %339, !dbg !347

; <label>:339:                                    ; preds = %335
  %340 = getelementptr inbounds i32, i32* %50, i64 %336, !dbg !348
  %341 = add <4 x i32> %337, <i32 4, i32 4, i32 4, i32 4>, !dbg !347
  %342 = bitcast i32* %340 to <4 x i32>*, !dbg !350
  store <4 x i32> %337, <4 x i32>* %342, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %343 = getelementptr i32, i32* %340, i64 4, !dbg !350
  %344 = bitcast i32* %343 to <4 x i32>*, !dbg !350
  store <4 x i32> %341, <4 x i32>* %344, align 4, !dbg !350, !tbaa !312, !alias.scope !351, !noalias !354
  %345 = getelementptr inbounds i32, i32* %52, i64 %336, !dbg !356
  %346 = bitcast i32* %345 to <4 x i32>*, !dbg !357
  store <4 x i32> %337, <4 x i32>* %346, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  %347 = getelementptr i32, i32* %345, i64 4, !dbg !357
  %348 = bitcast i32* %347 to <4 x i32>*, !dbg !357
  store <4 x i32> %341, <4 x i32>* %348, align 4, !dbg !357, !tbaa !312, !alias.scope !354
  br label %349

; <label>:349:                                    ; preds = %335, %339
  %350 = icmp eq i64 %299, %289
  br i1 %350, label %390, label %351, !dbg !346

; <label>:351:                                    ; preds = %349, %291, %288
  %352 = phi i64 [ 0, %291 ], [ 0, %288 ], [ %299, %349 ]
  %353 = add nsw i64 %289, -1, !dbg !348
  %354 = sub nsw i64 %353, %352, !dbg !348
  %355 = and i64 %289, 3, !dbg !348
  %356 = icmp eq i64 %355, 0, !dbg !348
  br i1 %356, label %367, label %357, !dbg !348

; <label>:357:                                    ; preds = %351
  br label %358, !dbg !348

; <label>:358:                                    ; preds = %358, %357
  %359 = phi i64 [ %364, %358 ], [ %352, %357 ]
  %360 = phi i64 [ %365, %358 ], [ %355, %357 ]
  call void @llvm.dbg.value(metadata i64 %359, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %361 = getelementptr inbounds i32, i32* %50, i64 %359, !dbg !348
  %362 = trunc i64 %359 to i32, !dbg !350
  store i32 %362, i32* %361, align 4, !dbg !350, !tbaa !312
  %363 = getelementptr inbounds i32, i32* %52, i64 %359, !dbg !356
  store i32 %362, i32* %363, align 4, !dbg !357, !tbaa !312
  %364 = add nuw nsw i64 %359, 1, !dbg !347
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  %365 = add i64 %360, -1, !dbg !346
  %366 = icmp eq i64 %365, 0, !dbg !346
  br i1 %366, label %367, label %358, !dbg !346, !llvm.loop !361

; <label>:367:                                    ; preds = %358, %351
  %368 = phi i64 [ %352, %351 ], [ %364, %358 ]
  %369 = icmp ult i64 %354, 3, !dbg !348
  br i1 %369, label %390, label %370, !dbg !348

; <label>:370:                                    ; preds = %367
  br label %371, !dbg !348

; <label>:371:                                    ; preds = %371, %370
  %372 = phi i64 [ %368, %370 ], [ %388, %371 ]
  call void @llvm.dbg.value(metadata i64 %372, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %373 = getelementptr inbounds i32, i32* %50, i64 %372, !dbg !348
  %374 = trunc i64 %372 to i32, !dbg !350
  store i32 %374, i32* %373, align 4, !dbg !350, !tbaa !312
  %375 = getelementptr inbounds i32, i32* %52, i64 %372, !dbg !356
  store i32 %374, i32* %375, align 4, !dbg !357, !tbaa !312
  %376 = add nuw nsw i64 %372, 1, !dbg !347
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  call void @llvm.dbg.value(metadata i64 %376, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %377 = getelementptr inbounds i32, i32* %50, i64 %376, !dbg !348
  %378 = trunc i64 %376 to i32, !dbg !350
  store i32 %378, i32* %377, align 4, !dbg !350, !tbaa !312
  %379 = getelementptr inbounds i32, i32* %52, i64 %376, !dbg !356
  store i32 %378, i32* %379, align 4, !dbg !357, !tbaa !312
  %380 = add nsw i64 %372, 2, !dbg !347
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  call void @llvm.dbg.value(metadata i64 %380, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %381 = getelementptr inbounds i32, i32* %50, i64 %380, !dbg !348
  %382 = trunc i64 %380 to i32, !dbg !350
  store i32 %382, i32* %381, align 4, !dbg !350, !tbaa !312
  %383 = getelementptr inbounds i32, i32* %52, i64 %380, !dbg !356
  store i32 %382, i32* %383, align 4, !dbg !357, !tbaa !312
  %384 = add nsw i64 %372, 3, !dbg !347
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  call void @llvm.dbg.value(metadata i64 %384, metadata !157, metadata !DIExpression()) #4, !dbg !307
  %385 = getelementptr inbounds i32, i32* %50, i64 %384, !dbg !348
  %386 = trunc i64 %384 to i32, !dbg !350
  store i32 %386, i32* %385, align 4, !dbg !350, !tbaa !312
  %387 = getelementptr inbounds i32, i32* %52, i64 %384, !dbg !356
  store i32 %386, i32* %387, align 4, !dbg !357, !tbaa !312
  %388 = add nsw i64 %372, 4, !dbg !347
  call void @llvm.dbg.value(metadata i32 undef, metadata !157, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !307
  %389 = icmp eq i64 %388, %289, !dbg !343
  br i1 %389, label %390, label %371, !dbg !346, !llvm.loop !363

; <label>:390:                                    ; preds = %367, %371, %274, %349, %255, %285, %174
  %391 = phi %struct.klu_symbolic* [ %90, %174 ], [ %21, %285 ], [ %90, %255 ], [ %21, %349 ], [ %90, %274 ], [ %21, %371 ], [ %21, %367 ], !dbg !364
  %392 = phi i32 [ 1, %174 ], [ %0, %285 ], [ %267, %255 ], [ %0, %349 ], [ %283, %274 ], [ %0, %371 ], [ %0, %367 ], !dbg !365
  %393 = phi i32 [ %82, %174 ], [ 1, %285 ], [ %82, %255 ], [ 1, %349 ], [ %82, %274 ], [ 1, %371 ], [ 1, %367 ], !dbg !365
  call void @llvm.dbg.value(metadata i32 %393, metadata !148, metadata !DIExpression()) #4, !dbg !290
  call void @llvm.dbg.value(metadata i32 %392, metadata !151, metadata !DIExpression()) #4, !dbg !320
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %391, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %394 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %391, i64 0, i32 11, !dbg !366
  store i32 %393, i32* %394, align 4, !dbg !367, !tbaa !368
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %391, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %395 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %391, i64 0, i32 12, !dbg !369
  store i32 %392, i32* %395, align 8, !dbg !370, !tbaa !371
  %396 = sext i32 %392 to i64, !dbg !372
  %397 = call i8* @klu_malloc(i64 %396, i64 4, %struct.klu_common_struct* %3) #4, !dbg !373
  %398 = add nsw i32 %392, 1, !dbg !374
  %399 = sext i32 %398 to i64, !dbg !375
  %400 = call i8* @klu_malloc(i64 %399, i64 4, %struct.klu_common_struct* %3) #4, !dbg !376
  %401 = add nsw i32 %33, 1, !dbg !377
  %402 = icmp sgt i32 %47, %401, !dbg !377
  %403 = select i1 %402, i32 %47, i32 %401, !dbg !377
  %404 = sext i32 %403 to i64, !dbg !377
  %405 = call i8* @klu_malloc(i64 %404, i64 4, %struct.klu_common_struct* %3) #4, !dbg !378
  %406 = call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* %3) #4, !dbg !379
  %407 = load i32, i32* %11, align 4, !dbg !380, !tbaa !173
  %408 = icmp eq i32 %407, 0, !dbg !381
  br i1 %408, label %409, label %961, !dbg !382

; <label>:409:                                    ; preds = %390
  %410 = bitcast i8* %406 to i32*, !dbg !379
  call void @llvm.dbg.value(metadata i32* %410, metadata !142, metadata !DIExpression()) #4, !dbg !383
  %411 = bitcast i8* %405 to i32*, !dbg !378
  call void @llvm.dbg.value(metadata i32* %411, metadata !141, metadata !DIExpression()) #4, !dbg !384
  %412 = bitcast i8* %400 to i32*, !dbg !376
  call void @llvm.dbg.value(metadata i32* %412, metadata !140, metadata !DIExpression()) #4, !dbg !385
  %413 = bitcast i8* %397 to i32*, !dbg !373
  call void @llvm.dbg.value(metadata i32* %413, metadata !143, metadata !DIExpression()) #4, !dbg !386
  %414 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !dbg !387, !tbaa !200
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %414, metadata !137, metadata !DIExpression()) #4, !dbg !198
  call void @llvm.dbg.value(metadata i32 %0, metadata !83, metadata !DIExpression()) #4, !dbg !388
  call void @llvm.dbg.value(metadata i32* %1, metadata !84, metadata !DIExpression()) #4, !dbg !389
  call void @llvm.dbg.value(metadata i32* %2, metadata !85, metadata !DIExpression()) #4, !dbg !390
  call void @llvm.dbg.value(metadata i32 %393, metadata !86, metadata !DIExpression()) #4, !dbg !391
  call void @llvm.dbg.value(metadata i32* %50, metadata !87, metadata !DIExpression()) #4, !dbg !392
  call void @llvm.dbg.value(metadata i32* %52, metadata !88, metadata !DIExpression()) #4, !dbg !393
  call void @llvm.dbg.value(metadata i32* %29, metadata !89, metadata !DIExpression()) #4, !dbg !394
  call void @llvm.dbg.value(metadata i32 %34, metadata !90, metadata !DIExpression()) #4, !dbg !395
  call void @llvm.dbg.value(metadata i32* %25, metadata !91, metadata !DIExpression()) #4, !dbg !396
  call void @llvm.dbg.value(metadata i32* %27, metadata !92, metadata !DIExpression()) #4, !dbg !397
  call void @llvm.dbg.value(metadata double* %31, metadata !93, metadata !DIExpression()) #4, !dbg !398
  call void @llvm.dbg.value(metadata i32* %413, metadata !94, metadata !DIExpression()) #4, !dbg !399
  call void @llvm.dbg.value(metadata i32* %412, metadata !95, metadata !DIExpression()) #4, !dbg !400
  call void @llvm.dbg.value(metadata i32* %411, metadata !96, metadata !DIExpression()) #4, !dbg !401
  call void @llvm.dbg.value(metadata i32 %47, metadata !97, metadata !DIExpression()) #4, !dbg !402
  call void @llvm.dbg.value(metadata i32* %410, metadata !98, metadata !DIExpression()) #4, !dbg !403
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %414, metadata !99, metadata !DIExpression()) #4, !dbg !404
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !100, metadata !DIExpression()) #4, !dbg !405
  %415 = bitcast [20 x double]* %5 to i8*, !dbg !406
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %415) #4, !dbg !406
  %416 = bitcast [20 x i32]* %6 to i8*, !dbg !407
  call void @llvm.lifetime.start.p0i8(i64 80, i8* nonnull %416) #4, !dbg !407
  call void @llvm.dbg.value(metadata i32 -3, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 0, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %417 = icmp sgt i32 %0, 0, !dbg !410
  br i1 %417, label %418, label %469, !dbg !413

; <label>:418:                                    ; preds = %409
  %419 = zext i32 %0 to i64
  %420 = add nsw i64 %419, -1, !dbg !413
  %421 = and i64 %419, 3, !dbg !413
  %422 = icmp ult i64 %420, 3, !dbg !413
  br i1 %422, label %454, label %423, !dbg !413

; <label>:423:                                    ; preds = %418
  %424 = sub nsw i64 %419, %421, !dbg !413
  br label %425, !dbg !413

; <label>:425:                                    ; preds = %425, %423
  %426 = phi i64 [ 0, %423 ], [ %451, %425 ]
  %427 = phi i64 [ %424, %423 ], [ %452, %425 ]
  call void @llvm.dbg.value(metadata i64 %426, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %428 = getelementptr inbounds i32, i32* %50, i64 %426, !dbg !414
  %429 = load i32, i32* %428, align 4, !dbg !414, !tbaa !312
  %430 = sext i32 %429 to i64, !dbg !416
  %431 = getelementptr inbounds i32, i32* %410, i64 %430, !dbg !416
  %432 = trunc i64 %426 to i32, !dbg !417
  store i32 %432, i32* %431, align 4, !dbg !417, !tbaa !312
  %433 = or i64 %426, 1, !dbg !418
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %433, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %434 = getelementptr inbounds i32, i32* %50, i64 %433, !dbg !414
  %435 = load i32, i32* %434, align 4, !dbg !414, !tbaa !312
  %436 = sext i32 %435 to i64, !dbg !416
  %437 = getelementptr inbounds i32, i32* %410, i64 %436, !dbg !416
  %438 = trunc i64 %433 to i32, !dbg !417
  store i32 %438, i32* %437, align 4, !dbg !417, !tbaa !312
  %439 = or i64 %426, 2, !dbg !418
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %439, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %440 = getelementptr inbounds i32, i32* %50, i64 %439, !dbg !414
  %441 = load i32, i32* %440, align 4, !dbg !414, !tbaa !312
  %442 = sext i32 %441 to i64, !dbg !416
  %443 = getelementptr inbounds i32, i32* %410, i64 %442, !dbg !416
  %444 = trunc i64 %439 to i32, !dbg !417
  store i32 %444, i32* %443, align 4, !dbg !417, !tbaa !312
  %445 = or i64 %426, 3, !dbg !418
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %445, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %446 = getelementptr inbounds i32, i32* %50, i64 %445, !dbg !414
  %447 = load i32, i32* %446, align 4, !dbg !414, !tbaa !312
  %448 = sext i32 %447 to i64, !dbg !416
  %449 = getelementptr inbounds i32, i32* %410, i64 %448, !dbg !416
  %450 = trunc i64 %445 to i32, !dbg !417
  store i32 %450, i32* %449, align 4, !dbg !417, !tbaa !312
  %451 = add nuw nsw i64 %426, 4, !dbg !418
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %452 = add i64 %427, -4, !dbg !413
  %453 = icmp eq i64 %452, 0, !dbg !413
  br i1 %453, label %454, label %425, !dbg !413, !llvm.loop !419

; <label>:454:                                    ; preds = %425, %418
  %455 = phi i64 [ 0, %418 ], [ %451, %425 ]
  %456 = icmp eq i64 %421, 0, !dbg !413
  br i1 %456, label %469, label %457, !dbg !413

; <label>:457:                                    ; preds = %454
  br label %458, !dbg !413

; <label>:458:                                    ; preds = %458, %457
  %459 = phi i64 [ %455, %457 ], [ %466, %458 ]
  %460 = phi i64 [ %421, %457 ], [ %467, %458 ]
  call void @llvm.dbg.value(metadata i64 %459, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %461 = getelementptr inbounds i32, i32* %50, i64 %459, !dbg !414
  %462 = load i32, i32* %461, align 4, !dbg !414, !tbaa !312
  %463 = sext i32 %462 to i64, !dbg !416
  %464 = getelementptr inbounds i32, i32* %410, i64 %463, !dbg !416
  %465 = trunc i64 %459 to i32, !dbg !417
  store i32 %465, i32* %464, align 4, !dbg !417, !tbaa !312
  %466 = add nuw nsw i64 %459, 1, !dbg !418
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %467 = add i64 %460, -1, !dbg !413
  %468 = icmp eq i64 %467, 0, !dbg !413
  br i1 %468, label %469, label %458, !dbg !413, !llvm.loop !422

; <label>:469:                                    ; preds = %454, %458, %409
  call void @llvm.dbg.value(metadata i32 0, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !101, metadata !DIExpression()) #4, !dbg !424
  call void @llvm.dbg.value(metadata i32 0, metadata !117, metadata !DIExpression()) #4, !dbg !425
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !103, metadata !DIExpression()) #4, !dbg !426
  %470 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 0, !dbg !427
  store double -1.000000e+00, double* %470, align 8, !dbg !428, !tbaa !429
  call void @llvm.dbg.value(metadata i32 0, metadata !109, metadata !DIExpression()) #4, !dbg !430
  call void @llvm.dbg.value(metadata i32 -3, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 0, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 0, metadata !117, metadata !DIExpression()) #4, !dbg !425
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !101, metadata !DIExpression()) #4, !dbg !424
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !103, metadata !DIExpression()) #4, !dbg !426
  %471 = icmp sgt i32 %393, 0, !dbg !431
  br i1 %471, label %472, label %951, !dbg !434

; <label>:472:                                    ; preds = %469
  %473 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 8
  %474 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 0
  %475 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 23
  %476 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 22
  %477 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 7
  %478 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 9
  %479 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 12
  %480 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 10
  %481 = getelementptr inbounds [20 x i32], [20 x i32]* %6, i64 0, i64 0
  %482 = getelementptr inbounds [20 x double], [20 x double]* %5, i64 0, i64 3
  %483 = bitcast double* %482 to i64*
  %484 = bitcast %struct.klu_symbolic* %414 to i64*
  %485 = sext i32 %393 to i64, !dbg !434
  br label %486, !dbg !434

; <label>:486:                                    ; preds = %949, %472
  %487 = phi i64 [ 0, %472 ], [ %495, %949 ]
  %488 = phi i32 [ -3, %472 ], [ %852, %949 ]
  %489 = phi i32 [ 0, %472 ], [ %587, %949 ]
  %490 = phi i32 [ 0, %472 ], [ %591, %949 ]
  %491 = phi double [ 0.000000e+00, %472 ], [ %859, %949 ]
  %492 = phi double [ 0.000000e+00, %472 ], [ %864, %949 ]
  call void @llvm.dbg.value(metadata i32 %488, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 %489, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 %490, metadata !117, metadata !DIExpression()) #4, !dbg !425
  call void @llvm.dbg.value(metadata double %491, metadata !101, metadata !DIExpression()) #4, !dbg !424
  call void @llvm.dbg.value(metadata double %492, metadata !103, metadata !DIExpression()) #4, !dbg !426
  call void @llvm.dbg.value(metadata i64 %487, metadata !109, metadata !DIExpression()) #4, !dbg !430
  %493 = getelementptr inbounds i32, i32* %29, i64 %487, !dbg !435
  %494 = load i32, i32* %493, align 4, !dbg !435, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %494, metadata !105, metadata !DIExpression()) #4, !dbg !437
  %495 = add nuw nsw i64 %487, 1, !dbg !438
  %496 = getelementptr inbounds i32, i32* %29, i64 %495, !dbg !439
  %497 = load i32, i32* %496, align 4, !dbg !439, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %497, metadata !106, metadata !DIExpression()) #4, !dbg !440
  %498 = sub nsw i32 %497, %494, !dbg !441
  call void @llvm.dbg.value(metadata i32 %498, metadata !107, metadata !DIExpression()) #4, !dbg !442
  %499 = getelementptr inbounds double, double* %31, i64 %487, !dbg !443
  store double -1.000000e+00, double* %499, align 8, !dbg !444, !tbaa !295
  call void @llvm.dbg.value(metadata i32 0, metadata !114, metadata !DIExpression()) #4, !dbg !445
  call void @llvm.dbg.value(metadata i32 %494, metadata !108, metadata !DIExpression()) #4, !dbg !409
  call void @llvm.dbg.value(metadata i32 %489, metadata !118, metadata !DIExpression()) #4, !dbg !423
  %500 = icmp sgt i32 %497, %494, !dbg !446
  br i1 %500, label %501, label %585, !dbg !449

; <label>:501:                                    ; preds = %486
  %502 = sext i32 %494 to i64, !dbg !449
  %503 = sext i32 %497 to i64
  br label %504, !dbg !449

; <label>:504:                                    ; preds = %580, %501
  %505 = phi i64 [ %502, %501 ], [ %583, %580 ]
  %506 = phi i32 [ %489, %501 ], [ %582, %580 ]
  %507 = phi i32 [ 0, %501 ], [ %581, %580 ]
  call void @llvm.dbg.value(metadata i32 %506, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 %507, metadata !114, metadata !DIExpression()) #4, !dbg !445
  call void @llvm.dbg.value(metadata i64 %505, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %508 = sub nsw i64 %505, %502, !dbg !450
  %509 = getelementptr inbounds i32, i32* %412, i64 %508, !dbg !452
  store i32 %507, i32* %509, align 4, !dbg !453, !tbaa !312
  %510 = getelementptr inbounds i32, i32* %52, i64 %505, !dbg !454
  %511 = load i32, i32* %510, align 4, !dbg !454, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %511, metadata !110, metadata !DIExpression()) #4, !dbg !455
  %512 = add nsw i32 %511, 1, !dbg !456
  %513 = sext i32 %512 to i64, !dbg !457
  %514 = getelementptr inbounds i32, i32* %1, i64 %513, !dbg !457
  %515 = load i32, i32* %514, align 4, !dbg !457, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %515, metadata !111, metadata !DIExpression()) #4, !dbg !458
  %516 = sext i32 %511 to i64, !dbg !459
  %517 = getelementptr inbounds i32, i32* %1, i64 %516, !dbg !459
  %518 = load i32, i32* %517, align 4, !dbg !459, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %518, metadata !115, metadata !DIExpression()) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %506, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 %507, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %519 = icmp slt i32 %518, %515, !dbg !462
  br i1 %519, label %520, label %580, !dbg !464

; <label>:520:                                    ; preds = %504
  %521 = sext i32 %518 to i64, !dbg !464
  %522 = sext i32 %515 to i64
  %523 = sub nsw i64 %522, %521, !dbg !464
  %524 = add nsw i64 %522, -1, !dbg !464
  %525 = and i64 %523, 1, !dbg !464
  %526 = icmp eq i64 %525, 0, !dbg !464
  br i1 %526, label %545, label %527, !dbg !464

; <label>:527:                                    ; preds = %520
  call void @llvm.dbg.value(metadata i32 %506, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i64 %521, metadata !115, metadata !DIExpression()) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %507, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %528 = getelementptr inbounds i32, i32* %2, i64 %521, !dbg !465
  %529 = load i32, i32* %528, align 4, !dbg !465, !tbaa !312
  %530 = sext i32 %529 to i64, !dbg !467
  %531 = getelementptr inbounds i32, i32* %410, i64 %530, !dbg !467
  %532 = load i32, i32* %531, align 4, !dbg !467, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %532, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %533 = icmp slt i32 %532, %494, !dbg !469
  br i1 %533, label %539, label %534, !dbg !471

; <label>:534:                                    ; preds = %527
  %535 = sub nsw i32 %532, %494, !dbg !472
  call void @llvm.dbg.value(metadata i32 %535, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %536 = add nsw i32 %507, 1, !dbg !474
  call void @llvm.dbg.value(metadata i32 %536, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %537 = sext i32 %507 to i64, !dbg !475
  %538 = getelementptr inbounds i32, i32* %411, i64 %537, !dbg !475
  store i32 %535, i32* %538, align 4, !dbg !476, !tbaa !312
  br label %541

; <label>:539:                                    ; preds = %527
  %540 = add nsw i32 %506, 1, !dbg !477
  call void @llvm.dbg.value(metadata i32 %540, metadata !118, metadata !DIExpression()) #4, !dbg !423
  br label %541, !dbg !479

; <label>:541:                                    ; preds = %539, %534
  %542 = phi i32 [ %507, %539 ], [ %536, %534 ], !dbg !480
  %543 = phi i32 [ %540, %539 ], [ %506, %534 ], !dbg !481
  %544 = add nsw i64 %521, 1, !dbg !482
  call void @llvm.dbg.value(metadata i32 %543, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 undef, metadata !115, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %542, metadata !114, metadata !DIExpression()) #4, !dbg !445
  br label %545, !dbg !464

; <label>:545:                                    ; preds = %541, %520
  %546 = phi i32 [ %542, %541 ], [ undef, %520 ]
  %547 = phi i32 [ %543, %541 ], [ undef, %520 ]
  %548 = phi i64 [ %544, %541 ], [ %521, %520 ]
  %549 = phi i32 [ %543, %541 ], [ %506, %520 ]
  %550 = phi i32 [ %542, %541 ], [ %507, %520 ]
  %551 = icmp eq i64 %524, %521, !dbg !464
  br i1 %551, label %580, label %552, !dbg !464

; <label>:552:                                    ; preds = %545
  br label %553, !dbg !464

; <label>:553:                                    ; preds = %985, %552
  %554 = phi i64 [ %548, %552 ], [ %988, %985 ]
  %555 = phi i32 [ %549, %552 ], [ %987, %985 ]
  %556 = phi i32 [ %550, %552 ], [ %986, %985 ]
  call void @llvm.dbg.value(metadata i32 %555, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i64 %554, metadata !115, metadata !DIExpression()) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %556, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %557 = getelementptr inbounds i32, i32* %2, i64 %554, !dbg !465
  %558 = load i32, i32* %557, align 4, !dbg !465, !tbaa !312
  %559 = sext i32 %558 to i64, !dbg !467
  %560 = getelementptr inbounds i32, i32* %410, i64 %559, !dbg !467
  %561 = load i32, i32* %560, align 4, !dbg !467, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %561, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %562 = icmp slt i32 %561, %494, !dbg !469
  br i1 %562, label %563, label %565, !dbg !471

; <label>:563:                                    ; preds = %553
  %564 = add nsw i32 %555, 1, !dbg !477
  call void @llvm.dbg.value(metadata i32 %564, metadata !118, metadata !DIExpression()) #4, !dbg !423
  br label %570, !dbg !479

; <label>:565:                                    ; preds = %553
  %566 = sub nsw i32 %561, %494, !dbg !472
  call void @llvm.dbg.value(metadata i32 %566, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %567 = add nsw i32 %556, 1, !dbg !474
  call void @llvm.dbg.value(metadata i32 %567, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %568 = sext i32 %556 to i64, !dbg !475
  %569 = getelementptr inbounds i32, i32* %411, i64 %568, !dbg !475
  store i32 %566, i32* %569, align 4, !dbg !476, !tbaa !312
  br label %570

; <label>:570:                                    ; preds = %565, %563
  %571 = phi i32 [ %556, %563 ], [ %567, %565 ], !dbg !480
  %572 = phi i32 [ %564, %563 ], [ %555, %565 ], !dbg !481
  %573 = add nsw i64 %554, 1, !dbg !482
  call void @llvm.dbg.value(metadata i32 %572, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 undef, metadata !115, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %571, metadata !114, metadata !DIExpression()) #4, !dbg !445
  call void @llvm.dbg.value(metadata i32 %572, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i64 %573, metadata !115, metadata !DIExpression()) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %571, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %574 = getelementptr inbounds i32, i32* %2, i64 %573, !dbg !465
  %575 = load i32, i32* %574, align 4, !dbg !465, !tbaa !312
  %576 = sext i32 %575 to i64, !dbg !467
  %577 = getelementptr inbounds i32, i32* %410, i64 %576, !dbg !467
  %578 = load i32, i32* %577, align 4, !dbg !467, !tbaa !312
  call void @llvm.dbg.value(metadata i32 %578, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %579 = icmp slt i32 %578, %494, !dbg !469
  br i1 %579, label %983, label %978, !dbg !471

; <label>:580:                                    ; preds = %545, %985, %504
  %581 = phi i32 [ %507, %504 ], [ %546, %545 ], [ %986, %985 ]
  %582 = phi i32 [ %506, %504 ], [ %547, %545 ], [ %987, %985 ]
  %583 = add nsw i64 %505, 1, !dbg !483
  call void @llvm.dbg.value(metadata i32 %582, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 %581, metadata !114, metadata !DIExpression()) #4, !dbg !445
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %584 = icmp eq i64 %583, %503, !dbg !446
  br i1 %584, label %585, label %504, !dbg !449, !llvm.loop !484

; <label>:585:                                    ; preds = %580, %486
  %586 = phi i32 [ 0, %486 ], [ %581, %580 ]
  %587 = phi i32 [ %489, %486 ], [ %582, %580 ]
  call void @llvm.dbg.value(metadata i32 %586, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %588 = sext i32 %498 to i64, !dbg !487
  %589 = getelementptr inbounds i32, i32* %412, i64 %588, !dbg !487
  store i32 %586, i32* %589, align 4, !dbg !488, !tbaa !312
  %590 = icmp sgt i32 %490, %586, !dbg !489
  %591 = select i1 %590, i32 %490, i32 %586, !dbg !489
  %592 = icmp slt i32 %498, 4, !dbg !490
  br i1 %592, label %593, label %683, !dbg !492

; <label>:593:                                    ; preds = %585
  call void @llvm.dbg.value(metadata i32 0, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %594 = icmp sgt i32 %498, 0, !dbg !493
  br i1 %594, label %595, label %669, !dbg !497

; <label>:595:                                    ; preds = %593
  %596 = zext i32 %498 to i64
  %597 = icmp ult i32 %498, 8, !dbg !497
  br i1 %597, label %661, label %598, !dbg !497

; <label>:598:                                    ; preds = %595
  %599 = and i64 %596, 4294967288, !dbg !497
  %600 = add nsw i64 %599, -8, !dbg !497
  %601 = lshr exact i64 %600, 3, !dbg !497
  %602 = add nuw nsw i64 %601, 1, !dbg !497
  %603 = and i64 %602, 3, !dbg !497
  %604 = icmp ult i64 %600, 24, !dbg !497
  br i1 %604, label %641, label %605, !dbg !497

; <label>:605:                                    ; preds = %598
  %606 = sub nsw i64 %602, %603, !dbg !497
  br label %607, !dbg !497

; <label>:607:                                    ; preds = %607, %605
  %608 = phi i64 [ 0, %605 ], [ %637, %607 ], !dbg !498
  %609 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %605 ], [ %638, %607 ]
  %610 = phi i64 [ %606, %605 ], [ %639, %607 ]
  %611 = getelementptr inbounds i32, i32* %413, i64 %608, !dbg !499
  %612 = add <4 x i32> %609, <i32 4, i32 4, i32 4, i32 4>, !dbg !498
  %613 = bitcast i32* %611 to <4 x i32>*, !dbg !501
  store <4 x i32> %609, <4 x i32>* %613, align 4, !dbg !501, !tbaa !312
  %614 = getelementptr i32, i32* %611, i64 4, !dbg !501
  %615 = bitcast i32* %614 to <4 x i32>*, !dbg !501
  store <4 x i32> %612, <4 x i32>* %615, align 4, !dbg !501, !tbaa !312
  %616 = or i64 %608, 8, !dbg !498
  %617 = add <4 x i32> %609, <i32 8, i32 8, i32 8, i32 8>, !dbg !498
  %618 = getelementptr inbounds i32, i32* %413, i64 %616, !dbg !499
  %619 = add <4 x i32> %609, <i32 12, i32 12, i32 12, i32 12>, !dbg !498
  %620 = bitcast i32* %618 to <4 x i32>*, !dbg !501
  store <4 x i32> %617, <4 x i32>* %620, align 4, !dbg !501, !tbaa !312
  %621 = getelementptr i32, i32* %618, i64 4, !dbg !501
  %622 = bitcast i32* %621 to <4 x i32>*, !dbg !501
  store <4 x i32> %619, <4 x i32>* %622, align 4, !dbg !501, !tbaa !312
  %623 = or i64 %608, 16, !dbg !498
  %624 = add <4 x i32> %609, <i32 16, i32 16, i32 16, i32 16>, !dbg !498
  %625 = getelementptr inbounds i32, i32* %413, i64 %623, !dbg !499
  %626 = add <4 x i32> %609, <i32 20, i32 20, i32 20, i32 20>, !dbg !498
  %627 = bitcast i32* %625 to <4 x i32>*, !dbg !501
  store <4 x i32> %624, <4 x i32>* %627, align 4, !dbg !501, !tbaa !312
  %628 = getelementptr i32, i32* %625, i64 4, !dbg !501
  %629 = bitcast i32* %628 to <4 x i32>*, !dbg !501
  store <4 x i32> %626, <4 x i32>* %629, align 4, !dbg !501, !tbaa !312
  %630 = or i64 %608, 24, !dbg !498
  %631 = add <4 x i32> %609, <i32 24, i32 24, i32 24, i32 24>, !dbg !498
  %632 = getelementptr inbounds i32, i32* %413, i64 %630, !dbg !499
  %633 = add <4 x i32> %609, <i32 28, i32 28, i32 28, i32 28>, !dbg !498
  %634 = bitcast i32* %632 to <4 x i32>*, !dbg !501
  store <4 x i32> %631, <4 x i32>* %634, align 4, !dbg !501, !tbaa !312
  %635 = getelementptr i32, i32* %632, i64 4, !dbg !501
  %636 = bitcast i32* %635 to <4 x i32>*, !dbg !501
  store <4 x i32> %633, <4 x i32>* %636, align 4, !dbg !501, !tbaa !312
  %637 = add i64 %608, 32, !dbg !498
  %638 = add <4 x i32> %609, <i32 32, i32 32, i32 32, i32 32>, !dbg !498
  %639 = add i64 %610, -4, !dbg !498
  %640 = icmp eq i64 %639, 0, !dbg !498
  br i1 %640, label %641, label %607, !dbg !498, !llvm.loop !502

; <label>:641:                                    ; preds = %607, %598
  %642 = phi i64 [ 0, %598 ], [ %637, %607 ]
  %643 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %598 ], [ %638, %607 ]
  %644 = icmp eq i64 %603, 0, !dbg !498
  br i1 %644, label %659, label %645, !dbg !498

; <label>:645:                                    ; preds = %641
  br label %646, !dbg !498

; <label>:646:                                    ; preds = %646, %645
  %647 = phi i64 [ %642, %645 ], [ %655, %646 ], !dbg !498
  %648 = phi <4 x i32> [ %643, %645 ], [ %656, %646 ]
  %649 = phi i64 [ %603, %645 ], [ %657, %646 ]
  %650 = getelementptr inbounds i32, i32* %413, i64 %647, !dbg !499
  %651 = add <4 x i32> %648, <i32 4, i32 4, i32 4, i32 4>, !dbg !498
  %652 = bitcast i32* %650 to <4 x i32>*, !dbg !501
  store <4 x i32> %648, <4 x i32>* %652, align 4, !dbg !501, !tbaa !312
  %653 = getelementptr i32, i32* %650, i64 4, !dbg !501
  %654 = bitcast i32* %653 to <4 x i32>*, !dbg !501
  store <4 x i32> %651, <4 x i32>* %654, align 4, !dbg !501, !tbaa !312
  %655 = add i64 %647, 8, !dbg !498
  %656 = add <4 x i32> %648, <i32 8, i32 8, i32 8, i32 8>, !dbg !498
  %657 = add i64 %649, -1, !dbg !498
  %658 = icmp eq i64 %657, 0, !dbg !498
  br i1 %658, label %659, label %646, !dbg !498, !llvm.loop !505

; <label>:659:                                    ; preds = %646, %641
  %660 = icmp eq i64 %599, %596
  br i1 %660, label %669, label %661, !dbg !497

; <label>:661:                                    ; preds = %659, %595
  %662 = phi i64 [ 0, %595 ], [ %599, %659 ]
  br label %663, !dbg !499

; <label>:663:                                    ; preds = %661, %663
  %664 = phi i64 [ %667, %663 ], [ %662, %661 ]
  call void @llvm.dbg.value(metadata i64 %664, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %665 = getelementptr inbounds i32, i32* %413, i64 %664, !dbg !499
  %666 = trunc i64 %664 to i32, !dbg !501
  store i32 %666, i32* %665, align 4, !dbg !501, !tbaa !312
  %667 = add nuw nsw i64 %664, 1, !dbg !498
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %668 = icmp eq i64 %667, %596, !dbg !493
  br i1 %668, label %669, label %663, !dbg !497, !llvm.loop !506

; <label>:669:                                    ; preds = %663, %659, %593
  %670 = add nsw i32 %498, 1, !dbg !507
  %671 = mul nsw i32 %670, %498, !dbg !508
  %672 = sdiv i32 %671, 2, !dbg !509
  %673 = sitofp i32 %672 to double, !dbg !510
  call void @llvm.dbg.value(metadata double %673, metadata !102, metadata !DIExpression()) #4, !dbg !511
  %674 = add nsw i32 %498, -1, !dbg !512
  %675 = mul nsw i32 %674, %498, !dbg !513
  %676 = sdiv i32 %675, 2, !dbg !514
  %677 = shl i32 %498, 1, !dbg !515
  %678 = add nsw i32 %677, -1, !dbg !516
  %679 = mul nsw i32 %675, %678, !dbg !517
  %680 = sdiv i32 %679, 6, !dbg !518
  %681 = add nsw i32 %680, %676, !dbg !519
  %682 = sitofp i32 %681 to double, !dbg !520
  call void @llvm.dbg.value(metadata double %682, metadata !104, metadata !DIExpression()) #4, !dbg !521
  call void @llvm.dbg.value(metadata i32 1, metadata !123, metadata !DIExpression()) #4, !dbg !522
  call void @llvm.dbg.value(metadata i32 %849, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 %848, metadata !123, metadata !DIExpression()) #4, !dbg !522
  call void @llvm.dbg.value(metadata double %847, metadata !102, metadata !DIExpression()) #4, !dbg !511
  call void @llvm.dbg.value(metadata double %846, metadata !104, metadata !DIExpression()) #4, !dbg !521
  br label %851, !dbg !523

; <label>:683:                                    ; preds = %585
  switch i32 %34, label %839 [
    i32 0, label %684
    i32 1, label %709
  ], !dbg !524

; <label>:684:                                    ; preds = %683
  %685 = call i32 @amd_order(i32 %498, i32* nonnull %412, i32* %411, i32* %413, double* null, double* nonnull %474) #4, !dbg !525
  call void @llvm.dbg.value(metadata i32 %685, metadata !113, metadata !DIExpression()) #4, !dbg !528
  %686 = lshr i32 %685, 31, !dbg !529
  %687 = xor i32 %686, 1, !dbg !529
  call void @llvm.dbg.value(metadata i32 %687, metadata !123, metadata !DIExpression()) #4, !dbg !522
  %688 = icmp eq i32 %685, -1, !dbg !530
  %689 = select i1 %688, i32 -2, i32 %488, !dbg !532
  call void @llvm.dbg.value(metadata i32 %689, metadata !124, metadata !DIExpression()) #4, !dbg !408
  %690 = load i64, i64* %475, align 8, !dbg !533, !tbaa !534
  %691 = uitofp i64 %690 to double, !dbg !533
  %692 = load i64, i64* %476, align 8, !dbg !533, !tbaa !535
  %693 = uitofp i64 %692 to double, !dbg !533
  %694 = load double, double* %477, align 8, !dbg !533, !tbaa !295
  %695 = fadd double %694, %693, !dbg !533
  %696 = fcmp olt double %695, %691, !dbg !533
  %697 = select i1 %696, double %691, double %695, !dbg !533
  %698 = fptoui double %697 to i64, !dbg !533
  store i64 %698, i64* %475, align 8, !dbg !536, !tbaa !534
  %699 = load double, double* %478, align 8, !dbg !537, !tbaa !295
  %700 = fptosi double %699 to i32, !dbg !538
  %701 = add nsw i32 %498, %700, !dbg !539
  %702 = sitofp i32 %701 to double, !dbg !538
  call void @llvm.dbg.value(metadata double %702, metadata !102, metadata !DIExpression()) #4, !dbg !511
  %703 = load double, double* %479, align 16, !dbg !540, !tbaa !295
  %704 = fmul double %703, 2.000000e+00, !dbg !541
  %705 = load double, double* %480, align 16, !dbg !542, !tbaa !295
  %706 = fadd double %704, %705, !dbg !543
  call void @llvm.dbg.value(metadata double %706, metadata !104, metadata !DIExpression()) #4, !dbg !521
  br i1 %590, label %845, label %707, !dbg !544

; <label>:707:                                    ; preds = %684
  %708 = load i64, i64* %483, align 8, !dbg !545, !tbaa !295
  store i64 %708, i64* %484, align 8, !dbg !548, !tbaa !429
  br label %845, !dbg !549

; <label>:709:                                    ; preds = %683
  %710 = call i32 @colamd(i32 %498, i32 %498, i32 %47, i32* %411, i32* nonnull %412, double* null, i32* nonnull %481) #4, !dbg !550
  call void @llvm.dbg.value(metadata i32 %710, metadata !123, metadata !DIExpression()) #4, !dbg !522
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !102, metadata !DIExpression()) #4, !dbg !511
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !104, metadata !DIExpression()) #4, !dbg !521
  call void @llvm.dbg.value(metadata i32 0, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %711 = zext i32 %498 to i64
  %712 = icmp ult i32 %498, 8, !dbg !553
  br i1 %712, label %800, label %713, !dbg !553

; <label>:713:                                    ; preds = %709
  %714 = shl nuw nsw i64 %711, 2, !dbg !553
  %715 = getelementptr i8, i8* %397, i64 %714, !dbg !553
  %716 = getelementptr i8, i8* %400, i64 %714, !dbg !553
  %717 = icmp ult i8* %397, %716, !dbg !553
  %718 = icmp ult i8* %400, %715, !dbg !553
  %719 = and i1 %717, %718, !dbg !553
  br i1 %719, label %800, label %720, !dbg !553

; <label>:720:                                    ; preds = %713
  %721 = and i64 %711, 4294967288, !dbg !553
  %722 = add nsw i64 %721, -8, !dbg !553
  %723 = lshr exact i64 %722, 3, !dbg !553
  %724 = add nuw nsw i64 %723, 1, !dbg !553
  %725 = and i64 %724, 3, !dbg !553
  %726 = icmp ult i64 %722, 24, !dbg !553
  br i1 %726, label %778, label %727, !dbg !553

; <label>:727:                                    ; preds = %720
  %728 = sub nsw i64 %724, %725, !dbg !553
  br label %729, !dbg !553

; <label>:729:                                    ; preds = %729, %727
  %730 = phi i64 [ 0, %727 ], [ %775, %729 ], !dbg !555
  %731 = phi i64 [ %728, %727 ], [ %776, %729 ]
  %732 = getelementptr inbounds i32, i32* %412, i64 %730, !dbg !557
  %733 = bitcast i32* %732 to <4 x i32>*, !dbg !557
  %734 = load <4 x i32>, <4 x i32>* %733, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %735 = getelementptr i32, i32* %732, i64 4, !dbg !557
  %736 = bitcast i32* %735 to <4 x i32>*, !dbg !557
  %737 = load <4 x i32>, <4 x i32>* %736, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %738 = getelementptr inbounds i32, i32* %413, i64 %730, !dbg !562
  %739 = bitcast i32* %738 to <4 x i32>*, !dbg !563
  store <4 x i32> %734, <4 x i32>* %739, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %740 = getelementptr i32, i32* %738, i64 4, !dbg !563
  %741 = bitcast i32* %740 to <4 x i32>*, !dbg !563
  store <4 x i32> %737, <4 x i32>* %741, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %742 = or i64 %730, 8, !dbg !555
  %743 = getelementptr inbounds i32, i32* %412, i64 %742, !dbg !557
  %744 = bitcast i32* %743 to <4 x i32>*, !dbg !557
  %745 = load <4 x i32>, <4 x i32>* %744, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %746 = getelementptr i32, i32* %743, i64 4, !dbg !557
  %747 = bitcast i32* %746 to <4 x i32>*, !dbg !557
  %748 = load <4 x i32>, <4 x i32>* %747, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %749 = getelementptr inbounds i32, i32* %413, i64 %742, !dbg !562
  %750 = bitcast i32* %749 to <4 x i32>*, !dbg !563
  store <4 x i32> %745, <4 x i32>* %750, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %751 = getelementptr i32, i32* %749, i64 4, !dbg !563
  %752 = bitcast i32* %751 to <4 x i32>*, !dbg !563
  store <4 x i32> %748, <4 x i32>* %752, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %753 = or i64 %730, 16, !dbg !555
  %754 = getelementptr inbounds i32, i32* %412, i64 %753, !dbg !557
  %755 = bitcast i32* %754 to <4 x i32>*, !dbg !557
  %756 = load <4 x i32>, <4 x i32>* %755, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %757 = getelementptr i32, i32* %754, i64 4, !dbg !557
  %758 = bitcast i32* %757 to <4 x i32>*, !dbg !557
  %759 = load <4 x i32>, <4 x i32>* %758, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %760 = getelementptr inbounds i32, i32* %413, i64 %753, !dbg !562
  %761 = bitcast i32* %760 to <4 x i32>*, !dbg !563
  store <4 x i32> %756, <4 x i32>* %761, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %762 = getelementptr i32, i32* %760, i64 4, !dbg !563
  %763 = bitcast i32* %762 to <4 x i32>*, !dbg !563
  store <4 x i32> %759, <4 x i32>* %763, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %764 = or i64 %730, 24, !dbg !555
  %765 = getelementptr inbounds i32, i32* %412, i64 %764, !dbg !557
  %766 = bitcast i32* %765 to <4 x i32>*, !dbg !557
  %767 = load <4 x i32>, <4 x i32>* %766, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %768 = getelementptr i32, i32* %765, i64 4, !dbg !557
  %769 = bitcast i32* %768 to <4 x i32>*, !dbg !557
  %770 = load <4 x i32>, <4 x i32>* %769, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %771 = getelementptr inbounds i32, i32* %413, i64 %764, !dbg !562
  %772 = bitcast i32* %771 to <4 x i32>*, !dbg !563
  store <4 x i32> %767, <4 x i32>* %772, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %773 = getelementptr i32, i32* %771, i64 4, !dbg !563
  %774 = bitcast i32* %773 to <4 x i32>*, !dbg !563
  store <4 x i32> %770, <4 x i32>* %774, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %775 = add i64 %730, 32, !dbg !555
  %776 = add i64 %731, -4, !dbg !555
  %777 = icmp eq i64 %776, 0, !dbg !555
  br i1 %777, label %778, label %729, !dbg !555, !llvm.loop !566

; <label>:778:                                    ; preds = %729, %720
  %779 = phi i64 [ 0, %720 ], [ %775, %729 ]
  %780 = icmp eq i64 %725, 0, !dbg !555
  br i1 %780, label %798, label %781, !dbg !555

; <label>:781:                                    ; preds = %778
  br label %782, !dbg !555

; <label>:782:                                    ; preds = %782, %781
  %783 = phi i64 [ %779, %781 ], [ %795, %782 ], !dbg !555
  %784 = phi i64 [ %725, %781 ], [ %796, %782 ]
  %785 = getelementptr inbounds i32, i32* %412, i64 %783, !dbg !557
  %786 = bitcast i32* %785 to <4 x i32>*, !dbg !557
  %787 = load <4 x i32>, <4 x i32>* %786, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %788 = getelementptr i32, i32* %785, i64 4, !dbg !557
  %789 = bitcast i32* %788 to <4 x i32>*, !dbg !557
  %790 = load <4 x i32>, <4 x i32>* %789, align 4, !dbg !557, !tbaa !312, !alias.scope !559
  %791 = getelementptr inbounds i32, i32* %413, i64 %783, !dbg !562
  %792 = bitcast i32* %791 to <4 x i32>*, !dbg !563
  store <4 x i32> %787, <4 x i32>* %792, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %793 = getelementptr i32, i32* %791, i64 4, !dbg !563
  %794 = bitcast i32* %793 to <4 x i32>*, !dbg !563
  store <4 x i32> %790, <4 x i32>* %794, align 4, !dbg !563, !tbaa !312, !alias.scope !564, !noalias !559
  %795 = add i64 %783, 8, !dbg !555
  %796 = add i64 %784, -1, !dbg !555
  %797 = icmp eq i64 %796, 0, !dbg !555
  br i1 %797, label %798, label %782, !dbg !555, !llvm.loop !569

; <label>:798:                                    ; preds = %782, %778
  %799 = icmp eq i64 %721, %711
  br i1 %799, label %845, label %800, !dbg !553

; <label>:800:                                    ; preds = %798, %713, %709
  %801 = phi i64 [ 0, %713 ], [ 0, %709 ], [ %721, %798 ]
  %802 = add nsw i64 %711, -1, !dbg !557
  %803 = sub nsw i64 %802, %801, !dbg !557
  %804 = and i64 %711, 3, !dbg !557
  %805 = icmp eq i64 %804, 0, !dbg !557
  br i1 %805, label %816, label %806, !dbg !557

; <label>:806:                                    ; preds = %800
  br label %807, !dbg !557

; <label>:807:                                    ; preds = %807, %806
  %808 = phi i64 [ %813, %807 ], [ %801, %806 ]
  %809 = phi i64 [ %814, %807 ], [ %804, %806 ]
  call void @llvm.dbg.value(metadata i64 %808, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %810 = getelementptr inbounds i32, i32* %412, i64 %808, !dbg !557
  %811 = load i32, i32* %810, align 4, !dbg !557, !tbaa !312
  %812 = getelementptr inbounds i32, i32* %413, i64 %808, !dbg !562
  store i32 %811, i32* %812, align 4, !dbg !563, !tbaa !312
  %813 = add nuw nsw i64 %808, 1, !dbg !555
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %814 = add i64 %809, -1, !dbg !553
  %815 = icmp eq i64 %814, 0, !dbg !553
  br i1 %815, label %816, label %807, !dbg !553, !llvm.loop !570

; <label>:816:                                    ; preds = %807, %800
  %817 = phi i64 [ %801, %800 ], [ %813, %807 ]
  %818 = icmp ult i64 %803, 3, !dbg !557
  br i1 %818, label %845, label %819, !dbg !557

; <label>:819:                                    ; preds = %816
  br label %820, !dbg !557

; <label>:820:                                    ; preds = %820, %819
  %821 = phi i64 [ %817, %819 ], [ %837, %820 ]
  call void @llvm.dbg.value(metadata i64 %821, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %822 = getelementptr inbounds i32, i32* %412, i64 %821, !dbg !557
  %823 = load i32, i32* %822, align 4, !dbg !557, !tbaa !312
  %824 = getelementptr inbounds i32, i32* %413, i64 %821, !dbg !562
  store i32 %823, i32* %824, align 4, !dbg !563, !tbaa !312
  %825 = add nuw nsw i64 %821, 1, !dbg !555
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %825, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %826 = getelementptr inbounds i32, i32* %412, i64 %825, !dbg !557
  %827 = load i32, i32* %826, align 4, !dbg !557, !tbaa !312
  %828 = getelementptr inbounds i32, i32* %413, i64 %825, !dbg !562
  store i32 %827, i32* %828, align 4, !dbg !563, !tbaa !312
  %829 = add nsw i64 %821, 2, !dbg !555
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %829, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %830 = getelementptr inbounds i32, i32* %412, i64 %829, !dbg !557
  %831 = load i32, i32* %830, align 4, !dbg !557, !tbaa !312
  %832 = getelementptr inbounds i32, i32* %413, i64 %829, !dbg !562
  store i32 %831, i32* %832, align 4, !dbg !563, !tbaa !312
  %833 = add nsw i64 %821, 3, !dbg !555
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %833, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %834 = getelementptr inbounds i32, i32* %412, i64 %833, !dbg !557
  %835 = load i32, i32* %834, align 4, !dbg !557, !tbaa !312
  %836 = getelementptr inbounds i32, i32* %413, i64 %833, !dbg !562
  store i32 %835, i32* %836, align 4, !dbg !563, !tbaa !312
  %837 = add nsw i64 %821, 4, !dbg !555
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %838 = icmp eq i64 %837, %711, !dbg !571
  br i1 %838, label %845, label %820, !dbg !553, !llvm.loop !572

; <label>:839:                                    ; preds = %683
  %840 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %473, align 8, !dbg !573, !tbaa !230
  %841 = call i32 %840(i32 %498, i32* nonnull %412, i32* %411, i32* %413, %struct.klu_common_struct* %3) #4, !dbg !573
  %842 = sitofp i32 %841 to double, !dbg !573
  call void @llvm.dbg.value(metadata double %842, metadata !102, metadata !DIExpression()) #4, !dbg !511
  call void @llvm.dbg.value(metadata double -1.000000e+00, metadata !104, metadata !DIExpression()) #4, !dbg !521
  %843 = icmp ne i32 %841, 0, !dbg !575
  %844 = zext i1 %843 to i32, !dbg !575
  call void @llvm.dbg.value(metadata i32 %844, metadata !123, metadata !DIExpression()) #4, !dbg !522
  br label %845

; <label>:845:                                    ; preds = %816, %820, %798, %839, %707, %684
  %846 = phi double [ %706, %707 ], [ %706, %684 ], [ -1.000000e+00, %839 ], [ -1.000000e+00, %798 ], [ -1.000000e+00, %820 ], [ -1.000000e+00, %816 ], !dbg !576
  %847 = phi double [ %702, %707 ], [ %702, %684 ], [ %842, %839 ], [ -1.000000e+00, %798 ], [ -1.000000e+00, %820 ], [ -1.000000e+00, %816 ], !dbg !576
  %848 = phi i32 [ %687, %707 ], [ %687, %684 ], [ %844, %839 ], [ %710, %798 ], [ %710, %820 ], [ %710, %816 ], !dbg !576
  %849 = phi i32 [ %689, %707 ], [ %689, %684 ], [ %488, %839 ], [ %488, %798 ], [ %488, %820 ], [ %488, %816 ], !dbg !481
  call void @llvm.dbg.value(metadata i32 %849, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 %848, metadata !123, metadata !DIExpression()) #4, !dbg !522
  call void @llvm.dbg.value(metadata double %847, metadata !102, metadata !DIExpression()) #4, !dbg !511
  call void @llvm.dbg.value(metadata double %846, metadata !104, metadata !DIExpression()) #4, !dbg !521
  %850 = icmp eq i32 %848, 0, !dbg !577
  br i1 %850, label %959, label %851, !dbg !523

; <label>:851:                                    ; preds = %845, %669
  %852 = phi i32 [ %488, %669 ], [ %849, %845 ]
  %853 = phi double [ %673, %669 ], [ %847, %845 ]
  %854 = phi double [ %682, %669 ], [ %846, %845 ]
  store double %853, double* %499, align 8, !dbg !579, !tbaa !295
  %855 = fcmp oeq double %491, -1.000000e+00, !dbg !580
  %856 = fcmp oeq double %853, -1.000000e+00, !dbg !581
  %857 = or i1 %855, %856, !dbg !582
  %858 = fadd double %491, %853, !dbg !583
  %859 = select i1 %857, double -1.000000e+00, double %858, !dbg !582
  %860 = fcmp oeq double %492, -1.000000e+00, !dbg !584
  %861 = fcmp oeq double %854, -1.000000e+00, !dbg !585
  %862 = or i1 %860, %861, !dbg !586
  %863 = fadd double %492, %854, !dbg !587
  %864 = select i1 %862, double -1.000000e+00, double %863, !dbg !586
  call void @llvm.dbg.value(metadata i32 0, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %865 = icmp sgt i32 %498, 0, !dbg !588
  br i1 %865, label %866, label %949, !dbg !591

; <label>:866:                                    ; preds = %851
  %867 = sext i32 %494 to i64, !dbg !591
  %868 = zext i32 %498 to i64
  %869 = add nsw i64 %868, -1, !dbg !591
  %870 = and i64 %868, 1, !dbg !591
  %871 = icmp eq i64 %869, 0, !dbg !591
  br i1 %871, label %897, label %872, !dbg !591

; <label>:872:                                    ; preds = %866
  %873 = sub nsw i64 %868, %870, !dbg !591
  br label %874, !dbg !591

; <label>:874:                                    ; preds = %874, %872
  %875 = phi i64 [ 0, %872 ], [ %894, %874 ]
  %876 = phi i64 [ %873, %872 ], [ %895, %874 ]
  call void @llvm.dbg.value(metadata i64 %875, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %877 = getelementptr inbounds i32, i32* %413, i64 %875, !dbg !592
  %878 = load i32, i32* %877, align 4, !dbg !592, !tbaa !312
  %879 = add nsw i32 %878, %494, !dbg !594
  %880 = sext i32 %879 to i64, !dbg !595
  %881 = getelementptr inbounds i32, i32* %52, i64 %880, !dbg !595
  %882 = load i32, i32* %881, align 4, !dbg !595, !tbaa !312
  %883 = add nsw i64 %875, %867, !dbg !596
  %884 = getelementptr inbounds i32, i32* %27, i64 %883, !dbg !597
  store i32 %882, i32* %884, align 4, !dbg !598, !tbaa !312
  %885 = or i64 %875, 1, !dbg !599
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %885, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %886 = getelementptr inbounds i32, i32* %413, i64 %885, !dbg !592
  %887 = load i32, i32* %886, align 4, !dbg !592, !tbaa !312
  %888 = add nsw i32 %887, %494, !dbg !594
  %889 = sext i32 %888 to i64, !dbg !595
  %890 = getelementptr inbounds i32, i32* %52, i64 %889, !dbg !595
  %891 = load i32, i32* %890, align 4, !dbg !595, !tbaa !312
  %892 = add nsw i64 %885, %867, !dbg !596
  %893 = getelementptr inbounds i32, i32* %27, i64 %892, !dbg !597
  store i32 %891, i32* %893, align 4, !dbg !598, !tbaa !312
  %894 = add nuw nsw i64 %875, 2, !dbg !599
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %895 = add i64 %876, -2, !dbg !591
  %896 = icmp eq i64 %895, 0, !dbg !591
  br i1 %896, label %897, label %874, !dbg !591, !llvm.loop !600

; <label>:897:                                    ; preds = %874, %866
  %898 = phi i64 [ 0, %866 ], [ %894, %874 ]
  %899 = icmp eq i64 %870, 0, !dbg !591
  br i1 %899, label %909, label %900, !dbg !591

; <label>:900:                                    ; preds = %897
  call void @llvm.dbg.value(metadata i64 %898, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %901 = getelementptr inbounds i32, i32* %413, i64 %898, !dbg !592
  %902 = load i32, i32* %901, align 4, !dbg !592, !tbaa !312
  %903 = add nsw i32 %902, %494, !dbg !594
  %904 = sext i32 %903 to i64, !dbg !595
  %905 = getelementptr inbounds i32, i32* %52, i64 %904, !dbg !595
  %906 = load i32, i32* %905, align 4, !dbg !595, !tbaa !312
  %907 = add nsw i64 %898, %867, !dbg !596
  %908 = getelementptr inbounds i32, i32* %27, i64 %907, !dbg !597
  store i32 %906, i32* %908, align 4, !dbg !598, !tbaa !312
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  br label %909, !dbg !603

; <label>:909:                                    ; preds = %897, %900
  %910 = and i64 %868, 1, !dbg !603
  %911 = icmp eq i64 %869, 0, !dbg !603
  br i1 %911, label %937, label %912, !dbg !603

; <label>:912:                                    ; preds = %909
  %913 = sub nsw i64 %868, %910, !dbg !603
  br label %914, !dbg !603

; <label>:914:                                    ; preds = %914, %912
  %915 = phi i64 [ 0, %912 ], [ %934, %914 ]
  %916 = phi i64 [ %913, %912 ], [ %935, %914 ]
  call void @llvm.dbg.value(metadata i64 %915, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %917 = getelementptr inbounds i32, i32* %413, i64 %915, !dbg !603
  %918 = load i32, i32* %917, align 4, !dbg !603, !tbaa !312
  %919 = add nsw i32 %918, %494, !dbg !607
  %920 = sext i32 %919 to i64, !dbg !608
  %921 = getelementptr inbounds i32, i32* %50, i64 %920, !dbg !608
  %922 = load i32, i32* %921, align 4, !dbg !608, !tbaa !312
  %923 = add nsw i64 %915, %867, !dbg !609
  %924 = getelementptr inbounds i32, i32* %25, i64 %923, !dbg !610
  store i32 %922, i32* %924, align 4, !dbg !611, !tbaa !312
  %925 = or i64 %915, 1, !dbg !612
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  call void @llvm.dbg.value(metadata i64 %925, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %926 = getelementptr inbounds i32, i32* %413, i64 %925, !dbg !603
  %927 = load i32, i32* %926, align 4, !dbg !603, !tbaa !312
  %928 = add nsw i32 %927, %494, !dbg !607
  %929 = sext i32 %928 to i64, !dbg !608
  %930 = getelementptr inbounds i32, i32* %50, i64 %929, !dbg !608
  %931 = load i32, i32* %930, align 4, !dbg !608, !tbaa !312
  %932 = add nsw i64 %925, %867, !dbg !609
  %933 = getelementptr inbounds i32, i32* %25, i64 %932, !dbg !610
  store i32 %931, i32* %933, align 4, !dbg !611, !tbaa !312
  %934 = add nuw nsw i64 %915, 2, !dbg !612
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  %935 = add i64 %916, -2, !dbg !613
  %936 = icmp eq i64 %935, 0, !dbg !613
  br i1 %936, label %937, label %914, !dbg !613, !llvm.loop !614

; <label>:937:                                    ; preds = %914, %909
  %938 = phi i64 [ 0, %909 ], [ %934, %914 ]
  %939 = icmp eq i64 %910, 0, !dbg !613
  br i1 %939, label %949, label %940, !dbg !613

; <label>:940:                                    ; preds = %937
  call void @llvm.dbg.value(metadata i64 %938, metadata !108, metadata !DIExpression()) #4, !dbg !409
  %941 = getelementptr inbounds i32, i32* %413, i64 %938, !dbg !603
  %942 = load i32, i32* %941, align 4, !dbg !603, !tbaa !312
  %943 = add nsw i32 %942, %494, !dbg !607
  %944 = sext i32 %943 to i64, !dbg !608
  %945 = getelementptr inbounds i32, i32* %50, i64 %944, !dbg !608
  %946 = load i32, i32* %945, align 4, !dbg !608, !tbaa !312
  %947 = add nsw i64 %938, %867, !dbg !609
  %948 = getelementptr inbounds i32, i32* %25, i64 %947, !dbg !610
  store i32 %946, i32* %948, align 4, !dbg !611, !tbaa !312
  call void @llvm.dbg.value(metadata i32 undef, metadata !108, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !409
  br label %949, !dbg !431

; <label>:949:                                    ; preds = %940, %937, %851
  call void @llvm.dbg.value(metadata i32 %852, metadata !124, metadata !DIExpression()) #4, !dbg !408
  call void @llvm.dbg.value(metadata i32 %587, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 %591, metadata !117, metadata !DIExpression()) #4, !dbg !425
  call void @llvm.dbg.value(metadata double %859, metadata !101, metadata !DIExpression()) #4, !dbg !424
  call void @llvm.dbg.value(metadata double %864, metadata !103, metadata !DIExpression()) #4, !dbg !426
  call void @llvm.dbg.value(metadata i32 undef, metadata !109, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !430
  %950 = icmp slt i64 %495, %485, !dbg !431
  br i1 %950, label %486, label %951, !dbg !434, !llvm.loop !617

; <label>:951:                                    ; preds = %949, %469
  %952 = phi double [ 0.000000e+00, %469 ], [ %864, %949 ]
  %953 = phi double [ 0.000000e+00, %469 ], [ %859, %949 ]
  %954 = phi i32 [ 0, %469 ], [ %587, %949 ]
  call void @llvm.dbg.value(metadata double %952, metadata !103, metadata !DIExpression()) #4, !dbg !426
  call void @llvm.dbg.value(metadata double %953, metadata !101, metadata !DIExpression()) #4, !dbg !424
  call void @llvm.dbg.value(metadata i32 %954, metadata !118, metadata !DIExpression()) #4, !dbg !423
  %955 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 2, !dbg !620
  store double %953, double* %955, align 8, !dbg !621, !tbaa !622
  %956 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 3, !dbg !623
  store double %953, double* %956, align 8, !dbg !624, !tbaa !625
  %957 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 10, !dbg !626
  store i32 %954, i32* %957, align 8, !dbg !627, !tbaa !628
  %958 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 1, !dbg !629
  store double %952, double* %958, align 8, !dbg !630, !tbaa !631
  br label %959, !dbg !632

; <label>:959:                                    ; preds = %845, %951
  %960 = phi i32 [ 0, %951 ], [ %849, %845 ], !dbg !481
  call void @llvm.lifetime.end.p0i8(i64 80, i8* nonnull %416) #4, !dbg !633
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %415) #4, !dbg !633
  store i32 %960, i32* %11, align 4, !dbg !634, !tbaa !173
  br label %961, !dbg !635

; <label>:961:                                    ; preds = %959, %390
  %962 = call i8* @klu_free(i8* %397, i64 %396, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !636
  %963 = call i8* @klu_free(i8* %400, i64 %399, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !637
  %964 = call i8* @klu_free(i8* %405, i64 %404, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !638
  %965 = call i8* @klu_free(i8* %406, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !639
  %966 = call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !640
  %967 = call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #4, !dbg !641
  %968 = load i32, i32* %11, align 4, !dbg !642, !tbaa !173
  %969 = icmp slt i32 %968, 0, !dbg !644
  br i1 %969, label %970, label %972, !dbg !645

; <label>:970:                                    ; preds = %961
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %8, metadata !137, metadata !DIExpression()) #4, !dbg !198
  %971 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #4, !dbg !646
  br label %972, !dbg !648

; <label>:972:                                    ; preds = %970, %961
  %973 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !dbg !649, !tbaa !200
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %973, metadata !137, metadata !DIExpression()) #4, !dbg !198
  br label %974, !dbg !650

; <label>:974:                                    ; preds = %18, %44, %55, %74, %972
  %975 = phi %struct.klu_symbolic* [ null, %55 ], [ null, %74 ], [ %973, %972 ], [ null, %44 ], [ null, %18 ], !dbg !651
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %20) #4, !dbg !652
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %19) #4, !dbg !652
  br label %976, !dbg !653

; <label>:976:                                    ; preds = %4, %974, %16
  %977 = phi %struct.klu_symbolic* [ %17, %16 ], [ %975, %974 ], [ null, %4 ], !dbg !654
  ret %struct.klu_symbolic* %977, !dbg !655

; <label>:978:                                    ; preds = %570
  %979 = sub nsw i32 %578, %494, !dbg !472
  call void @llvm.dbg.value(metadata i32 %979, metadata !116, metadata !DIExpression()) #4, !dbg !468
  %980 = add nsw i32 %571, 1, !dbg !474
  call void @llvm.dbg.value(metadata i32 %980, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %981 = sext i32 %571 to i64, !dbg !475
  %982 = getelementptr inbounds i32, i32* %411, i64 %981, !dbg !475
  store i32 %979, i32* %982, align 4, !dbg !476, !tbaa !312
  br label %985

; <label>:983:                                    ; preds = %570
  %984 = add nsw i32 %572, 1, !dbg !477
  call void @llvm.dbg.value(metadata i32 %984, metadata !118, metadata !DIExpression()) #4, !dbg !423
  br label %985, !dbg !479

; <label>:985:                                    ; preds = %983, %978
  %986 = phi i32 [ %571, %983 ], [ %980, %978 ], !dbg !480
  %987 = phi i32 [ %984, %983 ], [ %572, %978 ], !dbg !481
  %988 = add nsw i64 %554, 2, !dbg !482
  call void @llvm.dbg.value(metadata i32 %987, metadata !118, metadata !DIExpression()) #4, !dbg !423
  call void @llvm.dbg.value(metadata i32 undef, metadata !115, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)) #4, !dbg !461
  call void @llvm.dbg.value(metadata i32 %986, metadata !114, metadata !DIExpression()) #4, !dbg !445
  %989 = icmp eq i64 %988, %522, !dbg !462
  br i1 %989, label %580, label %553, !dbg !464, !llvm.loop !656
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.declare(metadata, metadata, metadata) #1

declare %struct.klu_symbolic* @klu_analyze_given(i32, i32*, i32*, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #3

declare %struct.klu_symbolic* @klu_alloc_symbolic(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #2

declare i64 @colamd_recommended(i32, i32, i32) local_unnamed_addr #2

declare i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i32 @btf_order(i32, i32*, i32*, double, double*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #2

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
!78 = !DILocalVariable(name: "amd_Info", scope: !79, file: !1, line: 44, type: !125)
!79 = distinct !DISubprogram(name: "analyze_worker", scope: !1, file: !1, line: 15, type: !80, isLocal: true, isDefinition: true, scopeLine: 43, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !82)
!80 = !DISubroutineType(types: !81)
!81 = !{!5, !5, !29, !29, !5, !29, !29, !29, !5, !29, !29, !25, !29, !29, !29, !5, !29, !14, !38}
!82 = !{!83, !84, !85, !86, !87, !88, !89, !90, !91, !92, !93, !94, !95, !96, !97, !98, !99, !100, !78, !101, !102, !103, !104, !105, !106, !107, !108, !109, !110, !111, !112, !113, !114, !115, !116, !117, !118, !119, !123, !124}
!83 = !DILocalVariable(name: "n", arg: 1, scope: !79, file: !1, line: 18, type: !5)
!84 = !DILocalVariable(name: "Ap", arg: 2, scope: !79, file: !1, line: 19, type: !29)
!85 = !DILocalVariable(name: "Ai", arg: 3, scope: !79, file: !1, line: 20, type: !29)
!86 = !DILocalVariable(name: "nblocks", arg: 4, scope: !79, file: !1, line: 21, type: !5)
!87 = !DILocalVariable(name: "Pbtf", arg: 5, scope: !79, file: !1, line: 22, type: !29)
!88 = !DILocalVariable(name: "Qbtf", arg: 6, scope: !79, file: !1, line: 23, type: !29)
!89 = !DILocalVariable(name: "R", arg: 7, scope: !79, file: !1, line: 24, type: !29)
!90 = !DILocalVariable(name: "ordering", arg: 8, scope: !79, file: !1, line: 25, type: !5)
!91 = !DILocalVariable(name: "P", arg: 9, scope: !79, file: !1, line: 28, type: !29)
!92 = !DILocalVariable(name: "Q", arg: 10, scope: !79, file: !1, line: 29, type: !29)
!93 = !DILocalVariable(name: "Lnz", arg: 11, scope: !79, file: !1, line: 30, type: !25)
!94 = !DILocalVariable(name: "Pblk", arg: 12, scope: !79, file: !1, line: 33, type: !29)
!95 = !DILocalVariable(name: "Cp", arg: 13, scope: !79, file: !1, line: 34, type: !29)
!96 = !DILocalVariable(name: "Ci", arg: 14, scope: !79, file: !1, line: 35, type: !29)
!97 = !DILocalVariable(name: "Cilen", arg: 15, scope: !79, file: !1, line: 36, type: !5)
!98 = !DILocalVariable(name: "Pinv", arg: 16, scope: !79, file: !1, line: 37, type: !29)
!99 = !DILocalVariable(name: "Symbolic", arg: 17, scope: !79, file: !1, line: 40, type: !14)
!100 = !DILocalVariable(name: "Common", arg: 18, scope: !79, file: !1, line: 41, type: !38)
!101 = !DILocalVariable(name: "lnz", scope: !79, file: !1, line: 44, type: !20)
!102 = !DILocalVariable(name: "lnz1", scope: !79, file: !1, line: 44, type: !20)
!103 = !DILocalVariable(name: "flops", scope: !79, file: !1, line: 44, type: !20)
!104 = !DILocalVariable(name: "flops1", scope: !79, file: !1, line: 44, type: !20)
!105 = !DILocalVariable(name: "k1", scope: !79, file: !1, line: 45, type: !5)
!106 = !DILocalVariable(name: "k2", scope: !79, file: !1, line: 45, type: !5)
!107 = !DILocalVariable(name: "nk", scope: !79, file: !1, line: 45, type: !5)
!108 = !DILocalVariable(name: "k", scope: !79, file: !1, line: 45, type: !5)
!109 = !DILocalVariable(name: "block", scope: !79, file: !1, line: 45, type: !5)
!110 = !DILocalVariable(name: "oldcol", scope: !79, file: !1, line: 45, type: !5)
!111 = !DILocalVariable(name: "pend", scope: !79, file: !1, line: 45, type: !5)
!112 = !DILocalVariable(name: "newcol", scope: !79, file: !1, line: 45, type: !5)
!113 = !DILocalVariable(name: "result", scope: !79, file: !1, line: 45, type: !5)
!114 = !DILocalVariable(name: "pc", scope: !79, file: !1, line: 45, type: !5)
!115 = !DILocalVariable(name: "p", scope: !79, file: !1, line: 45, type: !5)
!116 = !DILocalVariable(name: "newrow", scope: !79, file: !1, line: 45, type: !5)
!117 = !DILocalVariable(name: "maxnz", scope: !79, file: !1, line: 46, type: !5)
!118 = !DILocalVariable(name: "nzoff", scope: !79, file: !1, line: 46, type: !5)
!119 = !DILocalVariable(name: "cstats", scope: !79, file: !1, line: 46, type: !120)
!120 = !DICompositeType(tag: DW_TAG_array_type, baseType: !5, size: 640, elements: !121)
!121 = !{!122}
!122 = !DISubrange(count: 20)
!123 = !DILocalVariable(name: "ok", scope: !79, file: !1, line: 46, type: !5)
!124 = !DILocalVariable(name: "err", scope: !79, file: !1, line: 46, type: !5)
!125 = !DICompositeType(tag: DW_TAG_array_type, baseType: !20, size: 1280, elements: !121)
!126 = !DILocation(line: 44, column: 12, scope: !79, inlinedAt: !127)
!127 = distinct !DILocation(line: 413, column: 26, scope: !128, inlinedAt: !160)
!128 = distinct !DILexicalBlock(scope: !129, file: !1, line: 411, column: 5)
!129 = distinct !DILexicalBlock(scope: !130, file: !1, line: 410, column: 9)
!130 = distinct !DISubprogram(name: "order_and_analyze", scope: !1, file: !1, line: 257, type: !12, isLocal: true, isDefinition: true, scopeLine: 267, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !131)
!131 = !{!132, !133, !134, !135, !136, !137, !138, !139, !140, !141, !142, !143, !144, !145, !146, !147, !148, !149, !150, !151, !152, !153, !154, !155, !156, !157, !158, !159}
!132 = !DILocalVariable(name: "n", arg: 1, scope: !130, file: !1, line: 261, type: !5)
!133 = !DILocalVariable(name: "Ap", arg: 2, scope: !130, file: !1, line: 262, type: !29)
!134 = !DILocalVariable(name: "Ai", arg: 3, scope: !130, file: !1, line: 263, type: !29)
!135 = !DILocalVariable(name: "Common", arg: 4, scope: !130, file: !1, line: 265, type: !38)
!136 = !DILocalVariable(name: "work", scope: !130, file: !1, line: 268, type: !20)
!137 = !DILocalVariable(name: "Symbolic", scope: !130, file: !1, line: 269, type: !14)
!138 = !DILocalVariable(name: "Lnz", scope: !130, file: !1, line: 270, type: !25)
!139 = !DILocalVariable(name: "Qbtf", scope: !130, file: !1, line: 271, type: !29)
!140 = !DILocalVariable(name: "Cp", scope: !130, file: !1, line: 271, type: !29)
!141 = !DILocalVariable(name: "Ci", scope: !130, file: !1, line: 271, type: !29)
!142 = !DILocalVariable(name: "Pinv", scope: !130, file: !1, line: 271, type: !29)
!143 = !DILocalVariable(name: "Pblk", scope: !130, file: !1, line: 271, type: !29)
!144 = !DILocalVariable(name: "Pbtf", scope: !130, file: !1, line: 271, type: !29)
!145 = !DILocalVariable(name: "P", scope: !130, file: !1, line: 271, type: !29)
!146 = !DILocalVariable(name: "Q", scope: !130, file: !1, line: 271, type: !29)
!147 = !DILocalVariable(name: "R", scope: !130, file: !1, line: 271, type: !29)
!148 = !DILocalVariable(name: "nblocks", scope: !130, file: !1, line: 272, type: !5)
!149 = !DILocalVariable(name: "nz", scope: !130, file: !1, line: 272, type: !5)
!150 = !DILocalVariable(name: "block", scope: !130, file: !1, line: 272, type: !5)
!151 = !DILocalVariable(name: "maxblock", scope: !130, file: !1, line: 272, type: !5)
!152 = !DILocalVariable(name: "k1", scope: !130, file: !1, line: 272, type: !5)
!153 = !DILocalVariable(name: "k2", scope: !130, file: !1, line: 272, type: !5)
!154 = !DILocalVariable(name: "nk", scope: !130, file: !1, line: 272, type: !5)
!155 = !DILocalVariable(name: "do_btf", scope: !130, file: !1, line: 272, type: !5)
!156 = !DILocalVariable(name: "ordering", scope: !130, file: !1, line: 272, type: !5)
!157 = !DILocalVariable(name: "k", scope: !130, file: !1, line: 272, type: !5)
!158 = !DILocalVariable(name: "Cilen", scope: !130, file: !1, line: 272, type: !5)
!159 = !DILocalVariable(name: "Work", scope: !130, file: !1, line: 273, type: !29)
!160 = distinct !DILocation(line: 480, column: 17, scope: !161)
!161 = distinct !DILexicalBlock(scope: !162, file: !1, line: 478, column: 5)
!162 = distinct !DILexicalBlock(scope: !11, file: !1, line: 472, column: 9)
!163 = !DILocation(line: 46, column: 23, scope: !79, inlinedAt: !127)
!164 = !DILocation(line: 449, column: 9, scope: !11)
!165 = !DILocation(line: 450, column: 9, scope: !11)
!166 = !DILocation(line: 451, column: 9, scope: !11)
!167 = !DILocation(line: 453, column: 17, scope: !11)
!168 = !DILocation(line: 461, column: 16, scope: !169)
!169 = distinct !DILexicalBlock(scope: !11, file: !1, line: 461, column: 9)
!170 = !DILocation(line: 461, column: 9, scope: !11)
!171 = !DILocation(line: 465, column: 13, scope: !11)
!172 = !DILocation(line: 465, column: 20, scope: !11)
!173 = !{!174, !178, i64 76}
!174 = !{!"klu_common_struct", !175, i64 0, !175, i64 8, !175, i64 16, !175, i64 24, !175, i64 32, !178, i64 40, !178, i64 44, !178, i64 48, !179, i64 56, !179, i64 64, !178, i64 72, !178, i64 76, !178, i64 80, !178, i64 84, !178, i64 88, !178, i64 92, !178, i64 96, !175, i64 104, !175, i64 112, !175, i64 120, !175, i64 128, !175, i64 136, !180, i64 144, !180, i64 152}
!175 = !{!"double", !176, i64 0}
!176 = !{!"omnipotent char", !177, i64 0}
!177 = !{!"Simple C/C++ TBAA"}
!178 = !{!"int", !176, i64 0}
!179 = !{!"any pointer", !176, i64 0}
!180 = !{!"long", !176, i64 0}
!181 = !DILocation(line: 466, column: 13, scope: !11)
!182 = !DILocation(line: 466, column: 29, scope: !11)
!183 = !{!174, !178, i64 84}
!184 = !DILocation(line: 472, column: 17, scope: !162)
!185 = !{!174, !178, i64 44}
!186 = !DILocation(line: 472, column: 26, scope: !162)
!187 = !DILocation(line: 472, column: 9, scope: !11)
!188 = !DILocation(line: 475, column: 17, scope: !189)
!189 = distinct !DILexicalBlock(scope: !162, file: !1, line: 473, column: 5)
!190 = !DILocation(line: 475, column: 9, scope: !189)
!191 = !DILocation(line: 261, column: 9, scope: !130, inlinedAt: !160)
!192 = !DILocation(line: 262, column: 9, scope: !130, inlinedAt: !160)
!193 = !DILocation(line: 263, column: 9, scope: !130, inlinedAt: !160)
!194 = !DILocation(line: 265, column: 17, scope: !130, inlinedAt: !160)
!195 = !DILocation(line: 268, column: 5, scope: !130, inlinedAt: !160)
!196 = !DILocation(line: 269, column: 5, scope: !130, inlinedAt: !160)
!197 = !DILocation(line: 279, column: 16, scope: !130, inlinedAt: !160)
!198 = !DILocation(line: 269, column: 19, scope: !130, inlinedAt: !160)
!199 = !DILocation(line: 279, column: 14, scope: !130, inlinedAt: !160)
!200 = !{!179, !179, i64 0}
!201 = !DILocation(line: 280, column: 18, scope: !202, inlinedAt: !160)
!202 = distinct !DILexicalBlock(scope: !130, file: !1, line: 280, column: 9)
!203 = !DILocation(line: 280, column: 9, scope: !130, inlinedAt: !160)
!204 = !DILocation(line: 284, column: 19, scope: !130, inlinedAt: !160)
!205 = !{!206, !179, i64 48}
!206 = !{!"", !175, i64 0, !175, i64 8, !175, i64 16, !175, i64 24, !179, i64 32, !178, i64 40, !178, i64 44, !179, i64 48, !179, i64 56, !179, i64 64, !178, i64 72, !178, i64 76, !178, i64 80, !178, i64 84, !178, i64 88, !178, i64 92}
!207 = !DILocation(line: 271, column: 48, scope: !130, inlinedAt: !160)
!208 = !DILocation(line: 285, column: 19, scope: !130, inlinedAt: !160)
!209 = !{!206, !179, i64 56}
!210 = !DILocation(line: 271, column: 52, scope: !130, inlinedAt: !160)
!211 = !DILocation(line: 286, column: 19, scope: !130, inlinedAt: !160)
!212 = !{!206, !179, i64 64}
!213 = !DILocation(line: 271, column: 56, scope: !130, inlinedAt: !160)
!214 = !DILocation(line: 287, column: 21, scope: !130, inlinedAt: !160)
!215 = !{!206, !179, i64 32}
!216 = !DILocation(line: 270, column: 13, scope: !130, inlinedAt: !160)
!217 = !DILocation(line: 288, column: 20, scope: !130, inlinedAt: !160)
!218 = !{!206, !178, i64 44}
!219 = !DILocation(line: 272, column: 18, scope: !130, inlinedAt: !160)
!220 = !DILocation(line: 290, column: 24, scope: !130, inlinedAt: !160)
!221 = !DILocation(line: 272, column: 59, scope: !130, inlinedAt: !160)
!222 = !DILocation(line: 291, column: 9, scope: !130, inlinedAt: !160)
!223 = !DILocation(line: 294, column: 17, scope: !224, inlinedAt: !160)
!224 = distinct !DILexicalBlock(scope: !225, file: !1, line: 292, column: 5)
!225 = distinct !DILexicalBlock(scope: !130, file: !1, line: 291, column: 9)
!226 = !DILocation(line: 272, column: 72, scope: !130, inlinedAt: !160)
!227 = !DILocation(line: 295, column: 5, scope: !224, inlinedAt: !160)
!228 = !DILocation(line: 296, column: 57, scope: !229, inlinedAt: !160)
!229 = distinct !DILexicalBlock(scope: !225, file: !1, line: 296, column: 14)
!230 = !{!174, !179, i64 56}
!231 = !DILocation(line: 296, column: 68, scope: !229, inlinedAt: !160)
!232 = !DILocation(line: 296, column: 14, scope: !225, inlinedAt: !160)
!233 = !DILocation(line: 299, column: 19, scope: !234, inlinedAt: !160)
!234 = distinct !DILexicalBlock(scope: !229, file: !1, line: 297, column: 5)
!235 = !DILocation(line: 304, column: 24, scope: !236, inlinedAt: !160)
!236 = distinct !DILexicalBlock(scope: !229, file: !1, line: 302, column: 5)
!237 = !DILocation(line: 305, column: 9, scope: !236, inlinedAt: !160)
!238 = !DILocation(line: 306, column: 9, scope: !236, inlinedAt: !160)
!239 = !DILocation(line: 0, scope: !234, inlinedAt: !160)
!240 = !DILocation(line: 313, column: 24, scope: !130, inlinedAt: !160)
!241 = !DILocation(line: 313, column: 12, scope: !130, inlinedAt: !160)
!242 = !DILocation(line: 271, column: 41, scope: !130, inlinedAt: !160)
!243 = !DILocation(line: 314, column: 12, scope: !130, inlinedAt: !160)
!244 = !DILocation(line: 271, column: 10, scope: !130, inlinedAt: !160)
!245 = !DILocation(line: 315, column: 17, scope: !246, inlinedAt: !160)
!246 = distinct !DILexicalBlock(scope: !130, file: !1, line: 315, column: 9)
!247 = !DILocation(line: 315, column: 24, scope: !246, inlinedAt: !160)
!248 = !DILocation(line: 315, column: 9, scope: !130, inlinedAt: !160)
!249 = !DILocation(line: 317, column: 9, scope: !250, inlinedAt: !160)
!250 = distinct !DILexicalBlock(scope: !246, file: !1, line: 316, column: 5)
!251 = !DILocation(line: 318, column: 9, scope: !250, inlinedAt: !160)
!252 = !DILocation(line: 319, column: 9, scope: !250, inlinedAt: !160)
!253 = !DILocation(line: 320, column: 9, scope: !250, inlinedAt: !160)
!254 = !DILocation(line: 327, column: 22, scope: !130, inlinedAt: !160)
!255 = !{!174, !178, i64 40}
!256 = !DILocation(line: 272, column: 51, scope: !130, inlinedAt: !160)
!257 = !DILocation(line: 328, column: 14, scope: !130, inlinedAt: !160)
!258 = !DILocation(line: 329, column: 15, scope: !130, inlinedAt: !160)
!259 = !DILocation(line: 329, column: 24, scope: !130, inlinedAt: !160)
!260 = !{!206, !178, i64 84}
!261 = !DILocation(line: 330, column: 15, scope: !130, inlinedAt: !160)
!262 = !DILocation(line: 330, column: 22, scope: !130, inlinedAt: !160)
!263 = !{!206, !178, i64 88}
!264 = !DILocation(line: 331, column: 15, scope: !130, inlinedAt: !160)
!265 = !DILocation(line: 331, column: 31, scope: !130, inlinedAt: !160)
!266 = !{!206, !178, i64 92}
!267 = !DILocation(line: 337, column: 13, scope: !130, inlinedAt: !160)
!268 = !DILocation(line: 337, column: 18, scope: !130, inlinedAt: !160)
!269 = !{!174, !175, i64 136}
!270 = !DILocation(line: 339, column: 9, scope: !130, inlinedAt: !160)
!271 = !DILocation(line: 341, column: 29, scope: !272, inlinedAt: !160)
!272 = distinct !DILexicalBlock(scope: !273, file: !1, line: 340, column: 5)
!273 = distinct !DILexicalBlock(scope: !130, file: !1, line: 339, column: 9)
!274 = !DILocation(line: 341, column: 28, scope: !272, inlinedAt: !160)
!275 = !DILocation(line: 341, column: 16, scope: !272, inlinedAt: !160)
!276 = !DILocation(line: 342, column: 21, scope: !277, inlinedAt: !160)
!277 = distinct !DILexicalBlock(scope: !272, file: !1, line: 342, column: 13)
!278 = !DILocation(line: 342, column: 28, scope: !277, inlinedAt: !160)
!279 = !DILocation(line: 342, column: 13, scope: !272, inlinedAt: !160)
!280 = !DILocation(line: 345, column: 13, scope: !281, inlinedAt: !160)
!281 = distinct !DILexicalBlock(scope: !277, file: !1, line: 343, column: 9)
!282 = !DILocation(line: 346, column: 13, scope: !281, inlinedAt: !160)
!283 = !DILocation(line: 347, column: 13, scope: !281, inlinedAt: !160)
!284 = !DILocation(line: 348, column: 13, scope: !281, inlinedAt: !160)
!285 = !DILocation(line: 273, column: 10, scope: !130, inlinedAt: !160)
!286 = !DILocation(line: 351, column: 49, scope: !272, inlinedAt: !160)
!287 = !{!174, !175, i64 32}
!288 = !DILocation(line: 268, column: 12, scope: !130, inlinedAt: !160)
!289 = !DILocation(line: 351, column: 19, scope: !272, inlinedAt: !160)
!290 = !DILocation(line: 272, column: 9, scope: !130, inlinedAt: !160)
!291 = !DILocation(line: 353, column: 35, scope: !272, inlinedAt: !160)
!292 = !DILocation(line: 353, column: 45, scope: !272, inlinedAt: !160)
!293 = !DILocation(line: 353, column: 33, scope: !272, inlinedAt: !160)
!294 = !DILocation(line: 354, column: 25, scope: !272, inlinedAt: !160)
!295 = !{!175, !175, i64 0}
!296 = !DILocation(line: 354, column: 22, scope: !272, inlinedAt: !160)
!297 = !DILocation(line: 356, column: 9, scope: !272, inlinedAt: !160)
!298 = !DILocation(line: 359, column: 13, scope: !299, inlinedAt: !160)
!299 = distinct !DILexicalBlock(scope: !272, file: !1, line: 359, column: 13)
!300 = !DILocation(line: 359, column: 23, scope: !299, inlinedAt: !160)
!301 = !DILocation(line: 359, column: 39, scope: !299, inlinedAt: !160)
!302 = !DILocation(line: 361, column: 28, scope: !303, inlinedAt: !160)
!303 = distinct !DILexicalBlock(scope: !304, file: !1, line: 361, column: 13)
!304 = distinct !DILexicalBlock(scope: !305, file: !1, line: 361, column: 13)
!305 = distinct !DILexicalBlock(scope: !299, file: !1, line: 360, column: 9)
!306 = !DILocation(line: 359, column: 13, scope: !272, inlinedAt: !160)
!307 = !DILocation(line: 272, column: 69, scope: !130, inlinedAt: !160)
!308 = !DILocation(line: 361, column: 13, scope: !304, inlinedAt: !160)
!309 = !DILocation(line: 361, column: 35, scope: !303, inlinedAt: !160)
!310 = !DILocation(line: 363, column: 28, scope: !311, inlinedAt: !160)
!311 = distinct !DILexicalBlock(scope: !303, file: !1, line: 362, column: 13)
!312 = !{!178, !178, i64 0}
!313 = !DILocation(line: 363, column: 26, scope: !311, inlinedAt: !160)
!314 = distinct !{!314, !315, !316, !317}
!315 = !DILocation(line: 361, column: 13, scope: !304)
!316 = !DILocation(line: 364, column: 13, scope: !304)
!317 = !{!"llvm.loop.isvectorized", i32 1}
!318 = distinct !{!318, !315, !316, !319, !317}
!319 = !{!"llvm.loop.unroll.runtime.disable"}
!320 = !DILocation(line: 272, column: 29, scope: !130, inlinedAt: !160)
!321 = !DILocation(line: 272, column: 22, scope: !130, inlinedAt: !160)
!322 = !DILocation(line: 369, column: 32, scope: !323, inlinedAt: !160)
!323 = distinct !DILexicalBlock(scope: !324, file: !1, line: 369, column: 9)
!324 = distinct !DILexicalBlock(scope: !272, file: !1, line: 369, column: 9)
!325 = !DILocation(line: 369, column: 9, scope: !324, inlinedAt: !160)
!326 = !DILocation(line: 371, column: 18, scope: !327, inlinedAt: !160)
!327 = distinct !DILexicalBlock(scope: !323, file: !1, line: 370, column: 9)
!328 = !DILocation(line: 372, column: 26, scope: !327, inlinedAt: !160)
!329 = !DILocation(line: 372, column: 18, scope: !327, inlinedAt: !160)
!330 = !DILocation(line: 373, column: 21, scope: !327, inlinedAt: !160)
!331 = !DILocation(line: 375, column: 24, scope: !327, inlinedAt: !160)
!332 = distinct !{!332, !333, !334, !317}
!333 = !DILocation(line: 369, column: 9, scope: !324)
!334 = !DILocation(line: 376, column: 9, scope: !324)
!335 = !DILocation(line: 272, column: 39, scope: !130, inlinedAt: !160)
!336 = !DILocation(line: 272, column: 43, scope: !130, inlinedAt: !160)
!337 = !DILocation(line: 272, column: 47, scope: !130, inlinedAt: !160)
!338 = distinct !{!338, !333, !334, !319, !317}
!339 = !DILocation(line: 383, column: 15, scope: !340, inlinedAt: !160)
!340 = distinct !DILexicalBlock(scope: !273, file: !1, line: 379, column: 5)
!341 = !DILocation(line: 384, column: 9, scope: !340, inlinedAt: !160)
!342 = !DILocation(line: 384, column: 15, scope: !340, inlinedAt: !160)
!343 = !DILocation(line: 385, column: 24, scope: !344, inlinedAt: !160)
!344 = distinct !DILexicalBlock(scope: !345, file: !1, line: 385, column: 9)
!345 = distinct !DILexicalBlock(scope: !340, file: !1, line: 385, column: 9)
!346 = !DILocation(line: 385, column: 9, scope: !345, inlinedAt: !160)
!347 = !DILocation(line: 385, column: 31, scope: !344, inlinedAt: !160)
!348 = !DILocation(line: 387, column: 13, scope: !349, inlinedAt: !160)
!349 = distinct !DILexicalBlock(scope: !344, file: !1, line: 386, column: 9)
!350 = !DILocation(line: 387, column: 22, scope: !349, inlinedAt: !160)
!351 = !{!352}
!352 = distinct !{!352, !353}
!353 = distinct !{!353, !"LVerDomain"}
!354 = !{!355}
!355 = distinct !{!355, !353}
!356 = !DILocation(line: 388, column: 13, scope: !349, inlinedAt: !160)
!357 = !DILocation(line: 388, column: 22, scope: !349, inlinedAt: !160)
!358 = distinct !{!358, !359, !360, !317}
!359 = !DILocation(line: 385, column: 9, scope: !345)
!360 = !DILocation(line: 389, column: 9, scope: !345)
!361 = distinct !{!361, !362}
!362 = !{!"llvm.loop.unroll.disable"}
!363 = distinct !{!363, !359, !360, !317}
!364 = !DILocation(line: 392, column: 5, scope: !130, inlinedAt: !160)
!365 = !DILocation(line: 0, scope: !340, inlinedAt: !160)
!366 = !DILocation(line: 392, column: 15, scope: !130, inlinedAt: !160)
!367 = !DILocation(line: 392, column: 23, scope: !130, inlinedAt: !160)
!368 = !{!206, !178, i64 76}
!369 = !DILocation(line: 395, column: 15, scope: !130, inlinedAt: !160)
!370 = !DILocation(line: 395, column: 24, scope: !130, inlinedAt: !160)
!371 = !{!206, !178, i64 80}
!372 = !DILocation(line: 401, column: 24, scope: !130, inlinedAt: !160)
!373 = !DILocation(line: 401, column: 12, scope: !130, inlinedAt: !160)
!374 = !DILocation(line: 402, column: 33, scope: !130, inlinedAt: !160)
!375 = !DILocation(line: 402, column: 24, scope: !130, inlinedAt: !160)
!376 = !DILocation(line: 402, column: 12, scope: !130, inlinedAt: !160)
!377 = !DILocation(line: 403, column: 24, scope: !130, inlinedAt: !160)
!378 = !DILocation(line: 403, column: 12, scope: !130, inlinedAt: !160)
!379 = !DILocation(line: 404, column: 12, scope: !130, inlinedAt: !160)
!380 = !DILocation(line: 410, column: 17, scope: !129, inlinedAt: !160)
!381 = !DILocation(line: 410, column: 24, scope: !129, inlinedAt: !160)
!382 = !DILocation(line: 410, column: 9, scope: !130, inlinedAt: !160)
!383 = !DILocation(line: 271, column: 27, scope: !130, inlinedAt: !160)
!384 = !DILocation(line: 271, column: 22, scope: !130, inlinedAt: !160)
!385 = !DILocation(line: 271, column: 17, scope: !130, inlinedAt: !160)
!386 = !DILocation(line: 271, column: 34, scope: !130, inlinedAt: !160)
!387 = !DILocation(line: 414, column: 61, scope: !128, inlinedAt: !160)
!388 = !DILocation(line: 18, column: 9, scope: !79, inlinedAt: !127)
!389 = !DILocation(line: 19, column: 9, scope: !79, inlinedAt: !127)
!390 = !DILocation(line: 20, column: 9, scope: !79, inlinedAt: !127)
!391 = !DILocation(line: 21, column: 9, scope: !79, inlinedAt: !127)
!392 = !DILocation(line: 22, column: 9, scope: !79, inlinedAt: !127)
!393 = !DILocation(line: 23, column: 9, scope: !79, inlinedAt: !127)
!394 = !DILocation(line: 24, column: 9, scope: !79, inlinedAt: !127)
!395 = !DILocation(line: 25, column: 9, scope: !79, inlinedAt: !127)
!396 = !DILocation(line: 28, column: 9, scope: !79, inlinedAt: !127)
!397 = !DILocation(line: 29, column: 9, scope: !79, inlinedAt: !127)
!398 = !DILocation(line: 30, column: 12, scope: !79, inlinedAt: !127)
!399 = !DILocation(line: 33, column: 9, scope: !79, inlinedAt: !127)
!400 = !DILocation(line: 34, column: 9, scope: !79, inlinedAt: !127)
!401 = !DILocation(line: 35, column: 9, scope: !79, inlinedAt: !127)
!402 = !DILocation(line: 36, column: 9, scope: !79, inlinedAt: !127)
!403 = !DILocation(line: 37, column: 9, scope: !79, inlinedAt: !127)
!404 = !DILocation(line: 40, column: 19, scope: !79, inlinedAt: !127)
!405 = !DILocation(line: 41, column: 17, scope: !79, inlinedAt: !127)
!406 = !DILocation(line: 44, column: 5, scope: !79, inlinedAt: !127)
!407 = !DILocation(line: 45, column: 5, scope: !79, inlinedAt: !127)
!408 = !DILocation(line: 46, column: 50, scope: !79, inlinedAt: !127)
!409 = !DILocation(line: 45, column: 21, scope: !79, inlinedAt: !127)
!410 = !DILocation(line: 61, column: 20, scope: !411, inlinedAt: !127)
!411 = distinct !DILexicalBlock(scope: !412, file: !1, line: 61, column: 5)
!412 = distinct !DILexicalBlock(scope: !79, file: !1, line: 61, column: 5)
!413 = !DILocation(line: 61, column: 5, scope: !412, inlinedAt: !127)
!414 = !DILocation(line: 64, column: 15, scope: !415, inlinedAt: !127)
!415 = distinct !DILexicalBlock(scope: !411, file: !1, line: 62, column: 5)
!416 = !DILocation(line: 64, column: 9, scope: !415, inlinedAt: !127)
!417 = !DILocation(line: 64, column: 25, scope: !415, inlinedAt: !127)
!418 = !DILocation(line: 61, column: 27, scope: !411, inlinedAt: !127)
!419 = distinct !{!419, !420, !421}
!420 = !DILocation(line: 61, column: 5, scope: !412)
!421 = !DILocation(line: 65, column: 5, scope: !412)
!422 = distinct !{!422, !362}
!423 = !DILocation(line: 46, column: 16, scope: !79, inlinedAt: !127)
!424 = !DILocation(line: 44, column: 33, scope: !79, inlinedAt: !127)
!425 = !DILocation(line: 46, column: 9, scope: !79, inlinedAt: !127)
!426 = !DILocation(line: 44, column: 44, scope: !79, inlinedAt: !127)
!427 = !DILocation(line: 73, column: 15, scope: !79, inlinedAt: !127)
!428 = !DILocation(line: 73, column: 24, scope: !79, inlinedAt: !127)
!429 = !{!206, !175, i64 0}
!430 = !DILocation(line: 45, column: 24, scope: !79, inlinedAt: !127)
!431 = !DILocation(line: 79, column: 28, scope: !432, inlinedAt: !127)
!432 = distinct !DILexicalBlock(scope: !433, file: !1, line: 79, column: 5)
!433 = distinct !DILexicalBlock(scope: !79, file: !1, line: 79, column: 5)
!434 = !DILocation(line: 79, column: 5, scope: !433, inlinedAt: !127)
!435 = !DILocation(line: 86, column: 14, scope: !436, inlinedAt: !127)
!436 = distinct !DILexicalBlock(scope: !432, file: !1, line: 80, column: 5)
!437 = !DILocation(line: 45, column: 9, scope: !79, inlinedAt: !127)
!438 = !DILocation(line: 87, column: 22, scope: !436, inlinedAt: !127)
!439 = !DILocation(line: 87, column: 14, scope: !436, inlinedAt: !127)
!440 = !DILocation(line: 45, column: 13, scope: !79, inlinedAt: !127)
!441 = !DILocation(line: 88, column: 17, scope: !436, inlinedAt: !127)
!442 = !DILocation(line: 45, column: 17, scope: !79, inlinedAt: !127)
!443 = !DILocation(line: 95, column: 9, scope: !436, inlinedAt: !127)
!444 = !DILocation(line: 95, column: 21, scope: !436, inlinedAt: !127)
!445 = !DILocation(line: 45, column: 61, scope: !79, inlinedAt: !127)
!446 = !DILocation(line: 97, column: 25, scope: !447, inlinedAt: !127)
!447 = distinct !DILexicalBlock(scope: !448, file: !1, line: 97, column: 9)
!448 = distinct !DILexicalBlock(scope: !436, file: !1, line: 97, column: 9)
!449 = !DILocation(line: 97, column: 9, scope: !448, inlinedAt: !127)
!450 = !DILocation(line: 99, column: 23, scope: !451, inlinedAt: !127)
!451 = distinct !DILexicalBlock(scope: !447, file: !1, line: 98, column: 9)
!452 = !DILocation(line: 100, column: 13, scope: !451, inlinedAt: !127)
!453 = !DILocation(line: 100, column: 25, scope: !451, inlinedAt: !127)
!454 = !DILocation(line: 101, column: 22, scope: !451, inlinedAt: !127)
!455 = !DILocation(line: 45, column: 31, scope: !79, inlinedAt: !127)
!456 = !DILocation(line: 102, column: 30, scope: !451, inlinedAt: !127)
!457 = !DILocation(line: 102, column: 20, scope: !451, inlinedAt: !127)
!458 = !DILocation(line: 45, column: 39, scope: !79, inlinedAt: !127)
!459 = !DILocation(line: 103, column: 22, scope: !460, inlinedAt: !127)
!460 = distinct !DILexicalBlock(scope: !451, file: !1, line: 103, column: 13)
!461 = !DILocation(line: 45, column: 65, scope: !79, inlinedAt: !127)
!462 = !DILocation(line: 103, column: 38, scope: !463, inlinedAt: !127)
!463 = distinct !DILexicalBlock(scope: !460, file: !1, line: 103, column: 13)
!464 = !DILocation(line: 103, column: 13, scope: !460, inlinedAt: !127)
!465 = !DILocation(line: 105, column: 32, scope: !466, inlinedAt: !127)
!466 = distinct !DILexicalBlock(scope: !463, file: !1, line: 104, column: 13)
!467 = !DILocation(line: 105, column: 26, scope: !466, inlinedAt: !127)
!468 = !DILocation(line: 45, column: 68, scope: !79, inlinedAt: !127)
!469 = !DILocation(line: 106, column: 28, scope: !470, inlinedAt: !127)
!470 = distinct !DILexicalBlock(scope: !466, file: !1, line: 106, column: 21)
!471 = !DILocation(line: 106, column: 21, scope: !466, inlinedAt: !127)
!472 = !DILocation(line: 114, column: 28, scope: !473, inlinedAt: !127)
!473 = distinct !DILexicalBlock(scope: !470, file: !1, line: 111, column: 17)
!474 = !DILocation(line: 115, column: 27, scope: !473, inlinedAt: !127)
!475 = !DILocation(line: 115, column: 21, scope: !473, inlinedAt: !127)
!476 = !DILocation(line: 115, column: 31, scope: !473, inlinedAt: !127)
!477 = !DILocation(line: 108, column: 26, scope: !478, inlinedAt: !127)
!478 = distinct !DILexicalBlock(scope: !470, file: !1, line: 107, column: 17)
!479 = !DILocation(line: 109, column: 17, scope: !478, inlinedAt: !127)
!480 = !DILocation(line: 0, scope: !473, inlinedAt: !127)
!481 = !DILocation(line: 0, scope: !79, inlinedAt: !127)
!482 = !DILocation(line: 103, column: 48, scope: !463, inlinedAt: !127)
!483 = !DILocation(line: 97, column: 33, scope: !447, inlinedAt: !127)
!484 = distinct !{!484, !485, !486}
!485 = !DILocation(line: 97, column: 9, scope: !448)
!486 = !DILocation(line: 118, column: 9, scope: !448)
!487 = !DILocation(line: 119, column: 9, scope: !436, inlinedAt: !127)
!488 = !DILocation(line: 119, column: 17, scope: !436, inlinedAt: !127)
!489 = !DILocation(line: 120, column: 17, scope: !436, inlinedAt: !127)
!490 = !DILocation(line: 127, column: 16, scope: !491, inlinedAt: !127)
!491 = distinct !DILexicalBlock(scope: !436, file: !1, line: 127, column: 13)
!492 = !DILocation(line: 127, column: 13, scope: !436, inlinedAt: !127)
!493 = !DILocation(line: 134, column: 28, scope: !494, inlinedAt: !127)
!494 = distinct !DILexicalBlock(scope: !495, file: !1, line: 134, column: 13)
!495 = distinct !DILexicalBlock(scope: !496, file: !1, line: 134, column: 13)
!496 = distinct !DILexicalBlock(scope: !491, file: !1, line: 128, column: 9)
!497 = !DILocation(line: 134, column: 13, scope: !495, inlinedAt: !127)
!498 = !DILocation(line: 134, column: 36, scope: !494, inlinedAt: !127)
!499 = !DILocation(line: 136, column: 17, scope: !500, inlinedAt: !127)
!500 = distinct !DILexicalBlock(scope: !494, file: !1, line: 135, column: 13)
!501 = !DILocation(line: 136, column: 26, scope: !500, inlinedAt: !127)
!502 = distinct !{!502, !503, !504, !317}
!503 = !DILocation(line: 134, column: 13, scope: !495)
!504 = !DILocation(line: 137, column: 13, scope: !495)
!505 = distinct !{!505, !362}
!506 = distinct !{!506, !503, !504, !319, !317}
!507 = !DILocation(line: 138, column: 29, scope: !496, inlinedAt: !127)
!508 = !DILocation(line: 138, column: 23, scope: !496, inlinedAt: !127)
!509 = !DILocation(line: 138, column: 34, scope: !496, inlinedAt: !127)
!510 = !DILocation(line: 138, column: 20, scope: !496, inlinedAt: !127)
!511 = !DILocation(line: 44, column: 38, scope: !79, inlinedAt: !127)
!512 = !DILocation(line: 139, column: 31, scope: !496, inlinedAt: !127)
!513 = !DILocation(line: 139, column: 25, scope: !496, inlinedAt: !127)
!514 = !DILocation(line: 139, column: 36, scope: !496, inlinedAt: !127)
!515 = !DILocation(line: 139, column: 54, scope: !496, inlinedAt: !127)
!516 = !DILocation(line: 139, column: 57, scope: !496, inlinedAt: !127)
!517 = !DILocation(line: 139, column: 51, scope: !496, inlinedAt: !127)
!518 = !DILocation(line: 139, column: 61, scope: !496, inlinedAt: !127)
!519 = !DILocation(line: 139, column: 40, scope: !496, inlinedAt: !127)
!520 = !DILocation(line: 139, column: 22, scope: !496, inlinedAt: !127)
!521 = !DILocation(line: 44, column: 51, scope: !79, inlinedAt: !127)
!522 = !DILocation(line: 46, column: 46, scope: !79, inlinedAt: !127)
!523 = !DILocation(line: 205, column: 13, scope: !436, inlinedAt: !127)
!524 = !DILocation(line: 143, column: 18, scope: !491, inlinedAt: !127)
!525 = !DILocation(line: 150, column: 22, scope: !526, inlinedAt: !127)
!526 = distinct !DILexicalBlock(scope: !527, file: !1, line: 144, column: 9)
!527 = distinct !DILexicalBlock(scope: !491, file: !1, line: 143, column: 18)
!528 = !DILocation(line: 45, column: 53, scope: !79, inlinedAt: !127)
!529 = !DILocation(line: 151, column: 26, scope: !526, inlinedAt: !127)
!530 = !DILocation(line: 152, column: 24, scope: !531, inlinedAt: !127)
!531 = distinct !DILexicalBlock(scope: !526, file: !1, line: 152, column: 17)
!532 = !DILocation(line: 152, column: 17, scope: !526, inlinedAt: !127)
!533 = !DILocation(line: 158, column: 31, scope: !526, inlinedAt: !127)
!534 = !{!174, !180, i64 152}
!535 = !{!174, !180, i64 144}
!536 = !DILocation(line: 158, column: 29, scope: !526, inlinedAt: !127)
!537 = !DILocation(line: 162, column: 27, scope: !526, inlinedAt: !127)
!538 = !DILocation(line: 162, column: 20, scope: !526, inlinedAt: !127)
!539 = !DILocation(line: 162, column: 47, scope: !526, inlinedAt: !127)
!540 = !DILocation(line: 163, column: 26, scope: !526, inlinedAt: !127)
!541 = !DILocation(line: 163, column: 24, scope: !526, inlinedAt: !127)
!542 = !DILocation(line: 163, column: 56, scope: !526, inlinedAt: !127)
!543 = !DILocation(line: 163, column: 54, scope: !526, inlinedAt: !127)
!544 = !DILocation(line: 164, column: 17, scope: !526, inlinedAt: !127)
!545 = !DILocation(line: 167, column: 38, scope: !546, inlinedAt: !127)
!546 = distinct !DILexicalBlock(scope: !547, file: !1, line: 165, column: 13)
!547 = distinct !DILexicalBlock(scope: !526, file: !1, line: 164, column: 17)
!548 = !DILocation(line: 167, column: 36, scope: !546, inlinedAt: !127)
!549 = !DILocation(line: 168, column: 13, scope: !546, inlinedAt: !127)
!550 = !DILocation(line: 182, column: 18, scope: !551, inlinedAt: !127)
!551 = distinct !DILexicalBlock(scope: !552, file: !1, line: 172, column: 9)
!552 = distinct !DILexicalBlock(scope: !527, file: !1, line: 171, column: 18)
!553 = !DILocation(line: 187, column: 13, scope: !554, inlinedAt: !127)
!554 = distinct !DILexicalBlock(scope: !551, file: !1, line: 187, column: 13)
!555 = !DILocation(line: 187, column: 36, scope: !556, inlinedAt: !127)
!556 = distinct !DILexicalBlock(scope: !554, file: !1, line: 187, column: 13)
!557 = !DILocation(line: 189, column: 28, scope: !558, inlinedAt: !127)
!558 = distinct !DILexicalBlock(scope: !556, file: !1, line: 188, column: 13)
!559 = !{!560}
!560 = distinct !{!560, !561}
!561 = distinct !{!561, !"LVerDomain"}
!562 = !DILocation(line: 189, column: 17, scope: !558, inlinedAt: !127)
!563 = !DILocation(line: 189, column: 26, scope: !558, inlinedAt: !127)
!564 = !{!565}
!565 = distinct !{!565, !561}
!566 = distinct !{!566, !567, !568, !317}
!567 = !DILocation(line: 187, column: 13, scope: !554)
!568 = !DILocation(line: 190, column: 13, scope: !554)
!569 = distinct !{!569, !362}
!570 = distinct !{!570, !362}
!571 = !DILocation(line: 187, column: 28, scope: !556, inlinedAt: !127)
!572 = distinct !{!572, !567, !568, !317}
!573 = !DILocation(line: 200, column: 20, scope: !574, inlinedAt: !127)
!574 = distinct !DILexicalBlock(scope: !552, file: !1, line: 194, column: 9)
!575 = !DILocation(line: 202, column: 24, scope: !574, inlinedAt: !127)
!576 = !DILocation(line: 0, scope: !574, inlinedAt: !127)
!577 = !DILocation(line: 205, column: 14, scope: !578, inlinedAt: !127)
!578 = distinct !DILexicalBlock(scope: !436, file: !1, line: 205, column: 13)
!579 = !DILocation(line: 214, column: 21, scope: !436, inlinedAt: !127)
!580 = !DILocation(line: 215, column: 20, scope: !436, inlinedAt: !127)
!581 = !DILocation(line: 215, column: 37, scope: !436, inlinedAt: !127)
!582 = !DILocation(line: 215, column: 29, scope: !436, inlinedAt: !127)
!583 = !DILocation(line: 215, column: 62, scope: !436, inlinedAt: !127)
!584 = !DILocation(line: 216, column: 24, scope: !436, inlinedAt: !127)
!585 = !DILocation(line: 216, column: 43, scope: !436, inlinedAt: !127)
!586 = !DILocation(line: 216, column: 33, scope: !436, inlinedAt: !127)
!587 = !DILocation(line: 216, column: 70, scope: !436, inlinedAt: !127)
!588 = !DILocation(line: 223, column: 24, scope: !589, inlinedAt: !127)
!589 = distinct !DILexicalBlock(scope: !590, file: !1, line: 223, column: 9)
!590 = distinct !DILexicalBlock(scope: !436, file: !1, line: 223, column: 9)
!591 = !DILocation(line: 223, column: 9, scope: !590, inlinedAt: !127)
!592 = !DILocation(line: 227, column: 32, scope: !593, inlinedAt: !127)
!593 = distinct !DILexicalBlock(scope: !589, file: !1, line: 224, column: 9)
!594 = !DILocation(line: 227, column: 41, scope: !593, inlinedAt: !127)
!595 = !DILocation(line: 227, column: 26, scope: !593, inlinedAt: !127)
!596 = !DILocation(line: 227, column: 18, scope: !593, inlinedAt: !127)
!597 = !DILocation(line: 227, column: 13, scope: !593, inlinedAt: !127)
!598 = !DILocation(line: 227, column: 24, scope: !593, inlinedAt: !127)
!599 = !DILocation(line: 223, column: 32, scope: !589, inlinedAt: !127)
!600 = distinct !{!600, !601, !602}
!601 = !DILocation(line: 223, column: 9, scope: !590)
!602 = !DILocation(line: 228, column: 9, scope: !590)
!603 = !DILocation(line: 233, column: 32, scope: !604, inlinedAt: !127)
!604 = distinct !DILexicalBlock(scope: !605, file: !1, line: 230, column: 9)
!605 = distinct !DILexicalBlock(scope: !606, file: !1, line: 229, column: 9)
!606 = distinct !DILexicalBlock(scope: !436, file: !1, line: 229, column: 9)
!607 = !DILocation(line: 233, column: 41, scope: !604, inlinedAt: !127)
!608 = !DILocation(line: 233, column: 26, scope: !604, inlinedAt: !127)
!609 = !DILocation(line: 233, column: 18, scope: !604, inlinedAt: !127)
!610 = !DILocation(line: 233, column: 13, scope: !604, inlinedAt: !127)
!611 = !DILocation(line: 233, column: 24, scope: !604, inlinedAt: !127)
!612 = !DILocation(line: 229, column: 32, scope: !605, inlinedAt: !127)
!613 = !DILocation(line: 229, column: 9, scope: !606, inlinedAt: !127)
!614 = distinct !{!614, !615, !616}
!615 = !DILocation(line: 229, column: 9, scope: !606)
!616 = !DILocation(line: 234, column: 9, scope: !606)
!617 = distinct !{!617, !618, !619}
!618 = !DILocation(line: 79, column: 5, scope: !433)
!619 = !DILocation(line: 235, column: 5, scope: !433)
!620 = !DILocation(line: 241, column: 15, scope: !79, inlinedAt: !127)
!621 = !DILocation(line: 241, column: 19, scope: !79, inlinedAt: !127)
!622 = !{!206, !175, i64 16}
!623 = !DILocation(line: 242, column: 15, scope: !79, inlinedAt: !127)
!624 = !DILocation(line: 242, column: 19, scope: !79, inlinedAt: !127)
!625 = !{!206, !175, i64 24}
!626 = !DILocation(line: 243, column: 15, scope: !79, inlinedAt: !127)
!627 = !DILocation(line: 243, column: 21, scope: !79, inlinedAt: !127)
!628 = !{!206, !178, i64 72}
!629 = !DILocation(line: 244, column: 15, scope: !79, inlinedAt: !127)
!630 = !DILocation(line: 244, column: 25, scope: !79, inlinedAt: !127)
!631 = !{!206, !175, i64 8}
!632 = !DILocation(line: 245, column: 5, scope: !79, inlinedAt: !127)
!633 = !DILocation(line: 246, column: 1, scope: !79, inlinedAt: !127)
!634 = !DILocation(line: 413, column: 24, scope: !128, inlinedAt: !160)
!635 = !DILocation(line: 416, column: 5, scope: !128, inlinedAt: !160)
!636 = !DILocation(line: 422, column: 5, scope: !130, inlinedAt: !160)
!637 = !DILocation(line: 423, column: 5, scope: !130, inlinedAt: !160)
!638 = !DILocation(line: 424, column: 5, scope: !130, inlinedAt: !160)
!639 = !DILocation(line: 425, column: 5, scope: !130, inlinedAt: !160)
!640 = !DILocation(line: 426, column: 5, scope: !130, inlinedAt: !160)
!641 = !DILocation(line: 427, column: 5, scope: !130, inlinedAt: !160)
!642 = !DILocation(line: 433, column: 17, scope: !643, inlinedAt: !160)
!643 = distinct !DILexicalBlock(scope: !130, file: !1, line: 433, column: 9)
!644 = !DILocation(line: 433, column: 24, scope: !643, inlinedAt: !160)
!645 = !DILocation(line: 433, column: 9, scope: !130, inlinedAt: !160)
!646 = !DILocation(line: 435, column: 9, scope: !647, inlinedAt: !160)
!647 = distinct !DILexicalBlock(scope: !643, file: !1, line: 434, column: 5)
!648 = !DILocation(line: 436, column: 5, scope: !647, inlinedAt: !160)
!649 = !DILocation(line: 437, column: 13, scope: !130, inlinedAt: !160)
!650 = !DILocation(line: 437, column: 5, scope: !130, inlinedAt: !160)
!651 = !DILocation(line: 0, scope: !236, inlinedAt: !160)
!652 = !DILocation(line: 438, column: 1, scope: !130, inlinedAt: !160)
!653 = !DILocation(line: 480, column: 9, scope: !161)
!654 = !DILocation(line: 0, scope: !161)
!655 = !DILocation(line: 482, column: 1, scope: !11)
!656 = distinct !{!656, !657, !658}
!657 = !DILocation(line: 103, column: 13, scope: !460)
!658 = !DILocation(line: 117, column: 13, scope: !460)
