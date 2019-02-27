; ModuleID = 'klu_diagnostics.c'
source_filename = "klu_diagnostics.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_rgrowth(i32* readonly, i32* readonly, double* readonly, %struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !18 {
  call void @llvm.dbg.value(metadata i32* %0, metadata !108, metadata !DIExpression()), !dbg !148
  call void @llvm.dbg.value(metadata i32* %1, metadata !109, metadata !DIExpression()), !dbg !149
  call void @llvm.dbg.value(metadata double* %2, metadata !110, metadata !DIExpression()), !dbg !150
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %3, metadata !111, metadata !DIExpression()), !dbg !151
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %4, metadata !112, metadata !DIExpression()), !dbg !152
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %5, metadata !113, metadata !DIExpression()), !dbg !153
  %7 = icmp eq %struct.klu_common_struct* %5, null, !dbg !154
  br i1 %7, label %169, label %8, !dbg !156

; <label>:8:                                      ; preds = %6
  %9 = icmp eq %struct.klu_symbolic* %3, null, !dbg !157
  %10 = icmp eq i32* %0, null, !dbg !159
  %11 = or i1 %10, %9, !dbg !160
  %12 = icmp eq i32* %1, null, !dbg !161
  %13 = or i1 %12, %11, !dbg !160
  %14 = icmp eq double* %2, null, !dbg !162
  %15 = or i1 %14, %13, !dbg !160
  br i1 %15, label %16, label %18, !dbg !160

; <label>:16:                                     ; preds = %8
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11, !dbg !163
  store i32 -3, i32* %17, align 4, !dbg !165, !tbaa !166
  br label %169, !dbg !174

; <label>:18:                                     ; preds = %8
  %19 = icmp eq %struct.klu_numeric* %4, null, !dbg !175
  br i1 %19, label %20, label %23, !dbg !177

; <label>:20:                                     ; preds = %18
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 20, !dbg !178
  store double 0.000000e+00, double* %21, align 8, !dbg !180, !tbaa !181
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11, !dbg !182
  store i32 1, i32* %22, align 4, !dbg !183, !tbaa !166
  br label %169, !dbg !184

; <label>:23:                                     ; preds = %18
  %24 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11, !dbg !185
  store i32 0, i32* %24, align 4, !dbg !186, !tbaa !166
  call void @llvm.dbg.value(metadata double* %2, metadata !125, metadata !DIExpression()), !dbg !187
  %25 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 7, !dbg !188
  %26 = load i32*, i32** %25, align 8, !dbg !188, !tbaa !189
  call void @llvm.dbg.value(metadata i32* %26, metadata !123, metadata !DIExpression()), !dbg !191
  %27 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 15, !dbg !192
  %28 = load double*, double** %27, align 8, !dbg !192, !tbaa !193
  call void @llvm.dbg.value(metadata double* %28, metadata !128, metadata !DIExpression()), !dbg !194
  %29 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 8, !dbg !195
  %30 = load i32*, i32** %29, align 8, !dbg !195, !tbaa !196
  call void @llvm.dbg.value(metadata i32* %30, metadata !119, metadata !DIExpression()), !dbg !198
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 20, !dbg !199
  store double 1.000000e+00, double* %31, align 8, !dbg !200, !tbaa !181
  call void @llvm.dbg.value(metadata i32 0, metadata !129, metadata !DIExpression()), !dbg !201
  %32 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 11, !dbg !202
  %33 = load i32, i32* %32, align 4, !dbg !202, !tbaa !203
  %34 = icmp sgt i32 %33, 0, !dbg !204
  br i1 %34, label %35, label %169, !dbg !205

; <label>:35:                                     ; preds = %23
  %36 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 9
  %37 = load i32*, i32** %36, align 8, !tbaa !206
  %38 = load i32, i32* %32, align 4, !tbaa !203
  %39 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 12
  %40 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 9
  %41 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 11
  %42 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 14
  %43 = bitcast i8** %42 to double**
  %44 = icmp eq double* %28, null
  %45 = sext i32 %38 to i64, !dbg !205
  br label %46, !dbg !205

; <label>:46:                                     ; preds = %35, %167
  %47 = phi i64 [ 0, %35 ], [ %50, %167 ]
  call void @llvm.dbg.value(metadata i64 %47, metadata !129, metadata !DIExpression()), !dbg !201
  %48 = getelementptr inbounds i32, i32* %37, i64 %47, !dbg !207
  %49 = load i32, i32* %48, align 4, !dbg !207, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %49, metadata !132, metadata !DIExpression()), !dbg !209
  %50 = add nuw nsw i64 %47, 1, !dbg !210
  %51 = getelementptr inbounds i32, i32* %37, i64 %50, !dbg !211
  %52 = load i32, i32* %51, align 4, !dbg !211, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %52, metadata !133, metadata !DIExpression()), !dbg !212
  %53 = sub nsw i32 %52, %49, !dbg !213
  call void @llvm.dbg.value(metadata i32 %53, metadata !134, metadata !DIExpression()), !dbg !214
  %54 = icmp eq i32 %53, 1, !dbg !215
  br i1 %54, label %167, label %55, !dbg !217

; <label>:55:                                     ; preds = %46
  %56 = load i8**, i8*** %39, align 8, !dbg !218, !tbaa !219
  %57 = getelementptr inbounds i8*, i8** %56, i64 %47, !dbg !220
  %58 = bitcast i8** %57 to double**, !dbg !220
  %59 = load double*, double** %58, align 8, !dbg !220, !tbaa !221
  call void @llvm.dbg.value(metadata double* %59, metadata !124, metadata !DIExpression()), !dbg !222
  %60 = load i32*, i32** %40, align 8, !dbg !223, !tbaa !224
  %61 = sext i32 %49 to i64, !dbg !225
  %62 = getelementptr inbounds i32, i32* %60, i64 %61, !dbg !225
  call void @llvm.dbg.value(metadata i32* %62, metadata !121, metadata !DIExpression()), !dbg !226
  %63 = load i32*, i32** %41, align 8, !dbg !227, !tbaa !228
  %64 = getelementptr inbounds i32, i32* %63, i64 %61, !dbg !229
  call void @llvm.dbg.value(metadata i32* %64, metadata !122, metadata !DIExpression()), !dbg !230
  %65 = load double*, double** %43, align 8, !dbg !231, !tbaa !232
  %66 = getelementptr inbounds double, double* %65, i64 %61, !dbg !233
  call void @llvm.dbg.value(metadata double* %66, metadata !127, metadata !DIExpression()), !dbg !234
  call void @llvm.dbg.value(metadata double 1.000000e+00, metadata !117, metadata !DIExpression()), !dbg !235
  call void @llvm.dbg.value(metadata i32 0, metadata !135, metadata !DIExpression()), !dbg !236
  %67 = icmp sgt i32 %53, 0, !dbg !237
  br i1 %67, label %68, label %162, !dbg !238

; <label>:68:                                     ; preds = %55
  %69 = sext i32 %49 to i64, !dbg !238
  %70 = zext i32 %53 to i64
  br label %71, !dbg !238

; <label>:71:                                     ; preds = %158, %68
  %72 = phi i64 [ 0, %68 ], [ %160, %158 ]
  %73 = phi double [ 1.000000e+00, %68 ], [ %159, %158 ]
  call void @llvm.dbg.value(metadata i64 %72, metadata !135, metadata !DIExpression()), !dbg !236
  call void @llvm.dbg.value(metadata double %73, metadata !117, metadata !DIExpression()), !dbg !235
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !115, metadata !DIExpression()), !dbg !239
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !116, metadata !DIExpression()), !dbg !240
  %74 = add nsw i64 %72, %69, !dbg !241
  %75 = getelementptr inbounds i32, i32* %30, i64 %74, !dbg !242
  %76 = load i32, i32* %75, align 4, !dbg !242, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %76, metadata !136, metadata !DIExpression()), !dbg !243
  %77 = add nsw i32 %76, 1, !dbg !244
  %78 = sext i32 %77 to i64, !dbg !245
  %79 = getelementptr inbounds i32, i32* %0, i64 %78, !dbg !245
  %80 = load i32, i32* %79, align 4, !dbg !245, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %80, metadata !138, metadata !DIExpression()), !dbg !246
  %81 = sext i32 %76 to i64, !dbg !247
  %82 = getelementptr inbounds i32, i32* %0, i64 %81, !dbg !247
  %83 = load i32, i32* %82, align 4, !dbg !247, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %83, metadata !137, metadata !DIExpression()), !dbg !249
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !115, metadata !DIExpression()), !dbg !239
  %84 = icmp slt i32 %83, %80, !dbg !250
  br i1 %84, label %85, label %116, !dbg !252

; <label>:85:                                     ; preds = %71
  %86 = sext i32 %83 to i64, !dbg !252
  %87 = sext i32 %80 to i64
  br label %88, !dbg !252

; <label>:88:                                     ; preds = %112, %85
  %89 = phi i64 [ %86, %85 ], [ %114, %112 ]
  %90 = phi double [ 0.000000e+00, %85 ], [ %113, %112 ]
  call void @llvm.dbg.value(metadata double %90, metadata !115, metadata !DIExpression()), !dbg !239
  call void @llvm.dbg.value(metadata i64 %89, metadata !137, metadata !DIExpression()), !dbg !249
  %91 = getelementptr inbounds i32, i32* %1, i64 %89, !dbg !253
  %92 = load i32, i32* %91, align 4, !dbg !253, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %92, metadata !131, metadata !DIExpression()), !dbg !255
  %93 = sext i32 %92 to i64, !dbg !256
  %94 = getelementptr inbounds i32, i32* %26, i64 %93, !dbg !256
  %95 = load i32, i32* %94, align 4, !dbg !256, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %95, metadata !130, metadata !DIExpression()), !dbg !257
  %96 = icmp slt i32 %95, %49, !dbg !258
  br i1 %96, label %112, label %97, !dbg !260

; <label>:97:                                     ; preds = %88
  %98 = getelementptr inbounds double, double* %2, i64 %89, !dbg !261
  %99 = load double, double* %98, align 8, !dbg !261, !tbaa !264
  br i1 %44, label %105, label %100, !dbg !265

; <label>:100:                                    ; preds = %97
  %101 = sext i32 %95 to i64, !dbg !266
  %102 = getelementptr inbounds double, double* %28, i64 %101, !dbg !266
  %103 = load double, double* %102, align 8, !dbg !266, !tbaa !264
  %104 = fdiv double %99, %103, !dbg !266
  call void @llvm.dbg.value(metadata double %104, metadata !118, metadata !DIExpression()), !dbg !269
  br label %105, !dbg !270

; <label>:105:                                    ; preds = %97, %100
  %106 = phi double [ %104, %100 ], [ %99, %97 ], !dbg !261
  call void @llvm.dbg.value(metadata double %106, metadata !118, metadata !DIExpression()), !dbg !269
  %107 = fcmp olt double %106, 0.000000e+00, !dbg !271
  %108 = fsub double -0.000000e+00, %106, !dbg !271
  %109 = select i1 %107, double %108, double %106, !dbg !271
  call void @llvm.dbg.value(metadata double %109, metadata !114, metadata !DIExpression()), !dbg !273
  %110 = fcmp ogt double %109, %90, !dbg !274
  br i1 %110, label %111, label %112, !dbg !276

; <label>:111:                                    ; preds = %105
  call void @llvm.dbg.value(metadata double %109, metadata !115, metadata !DIExpression()), !dbg !239
  br label %112, !dbg !277

; <label>:112:                                    ; preds = %105, %111, %88
  %113 = phi double [ %90, %88 ], [ %109, %111 ], [ %90, %105 ], !dbg !279
  %114 = add nsw i64 %89, 1, !dbg !280
  call void @llvm.dbg.value(metadata double %113, metadata !115, metadata !DIExpression()), !dbg !239
  call void @llvm.dbg.value(metadata i32 undef, metadata !137, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !249
  %115 = icmp eq i64 %114, %87, !dbg !250
  br i1 %115, label %116, label %88, !dbg !252, !llvm.loop !281

; <label>:116:                                    ; preds = %112, %71
  %117 = phi double [ 0.000000e+00, %71 ], [ %113, %112 ]
  call void @llvm.dbg.value(metadata double %117, metadata !115, metadata !DIExpression()), !dbg !239
  %118 = getelementptr inbounds i32, i32* %62, i64 %72, !dbg !283
  %119 = load i32, i32* %118, align 4, !dbg !283, !tbaa !208
  %120 = sext i32 %119 to i64, !dbg !283
  %121 = getelementptr inbounds double, double* %59, i64 %120, !dbg !283
  call void @llvm.dbg.value(metadata double* %121, metadata !140, metadata !DIExpression()), !dbg !283
  %122 = getelementptr inbounds i32, i32* %64, i64 %72, !dbg !283
  %123 = load i32, i32* %122, align 4, !dbg !283, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %123, metadata !139, metadata !DIExpression()), !dbg !284
  call void @llvm.dbg.value(metadata double* %121, metadata !120, metadata !DIExpression()), !dbg !285
  %124 = sext i32 %123 to i64, !dbg !283
  %125 = shl nsw i64 %124, 2, !dbg !283
  %126 = add nsw i64 %125, 7, !dbg !283
  %127 = lshr i64 %126, 3, !dbg !283
  %128 = getelementptr inbounds double, double* %121, i64 %127, !dbg !283
  call void @llvm.dbg.value(metadata double* %128, metadata !126, metadata !DIExpression()), !dbg !286
  call void @llvm.dbg.value(metadata i32 0, metadata !137, metadata !DIExpression()), !dbg !249
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !116, metadata !DIExpression()), !dbg !240
  %129 = icmp sgt i32 %123, 0, !dbg !287
  br i1 %129, label %130, label %144, !dbg !290

; <label>:130:                                    ; preds = %116
  %131 = zext i32 %123 to i64
  br label %132, !dbg !290

; <label>:132:                                    ; preds = %132, %130
  %133 = phi i64 [ 0, %130 ], [ %142, %132 ]
  %134 = phi double [ 0.000000e+00, %130 ], [ %141, %132 ]
  call void @llvm.dbg.value(metadata i64 %133, metadata !137, metadata !DIExpression()), !dbg !249
  call void @llvm.dbg.value(metadata double %134, metadata !116, metadata !DIExpression()), !dbg !240
  %135 = getelementptr inbounds double, double* %128, i64 %133, !dbg !291
  %136 = load double, double* %135, align 8, !dbg !291, !tbaa !264
  %137 = fcmp olt double %136, 0.000000e+00, !dbg !291
  %138 = fsub double -0.000000e+00, %136, !dbg !291
  %139 = select i1 %137, double %138, double %136, !dbg !291
  call void @llvm.dbg.value(metadata double %139, metadata !114, metadata !DIExpression()), !dbg !273
  %140 = fcmp ogt double %139, %134, !dbg !294
  call void @llvm.dbg.value(metadata double %139, metadata !116, metadata !DIExpression()), !dbg !240
  %141 = select i1 %140, double %139, double %134, !dbg !296
  %142 = add nuw nsw i64 %133, 1, !dbg !297
  call void @llvm.dbg.value(metadata i32 undef, metadata !137, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !249
  call void @llvm.dbg.value(metadata double %141, metadata !116, metadata !DIExpression()), !dbg !240
  %143 = icmp eq i64 %142, %131, !dbg !287
  br i1 %143, label %144, label %132, !dbg !290, !llvm.loop !298

; <label>:144:                                    ; preds = %132, %116
  %145 = phi double [ 0.000000e+00, %116 ], [ %141, %132 ]
  call void @llvm.dbg.value(metadata double %145, metadata !116, metadata !DIExpression()), !dbg !240
  %146 = getelementptr inbounds double, double* %66, i64 %72, !dbg !300
  %147 = load double, double* %146, align 8, !dbg !300, !tbaa !264
  %148 = fcmp olt double %147, 0.000000e+00, !dbg !300
  %149 = fsub double -0.000000e+00, %147, !dbg !300
  %150 = select i1 %148, double %149, double %147, !dbg !300
  call void @llvm.dbg.value(metadata double %150, metadata !114, metadata !DIExpression()), !dbg !273
  %151 = fcmp ogt double %150, %145, !dbg !302
  call void @llvm.dbg.value(metadata double %150, metadata !116, metadata !DIExpression()), !dbg !240
  %152 = select i1 %151, double %150, double %145, !dbg !304
  call void @llvm.dbg.value(metadata double %152, metadata !116, metadata !DIExpression()), !dbg !240
  %153 = fcmp oeq double %152, 0.000000e+00, !dbg !305
  br i1 %153, label %158, label %154, !dbg !307

; <label>:154:                                    ; preds = %144
  %155 = fdiv double %117, %152, !dbg !308
  call void @llvm.dbg.value(metadata double %155, metadata !114, metadata !DIExpression()), !dbg !273
  %156 = fcmp olt double %155, %73, !dbg !309
  br i1 %156, label %157, label %158, !dbg !311

; <label>:157:                                    ; preds = %154
  call void @llvm.dbg.value(metadata double %155, metadata !117, metadata !DIExpression()), !dbg !235
  br label %158, !dbg !312

; <label>:158:                                    ; preds = %154, %157, %144
  %159 = phi double [ %73, %144 ], [ %155, %157 ], [ %73, %154 ], !dbg !314
  %160 = add nuw nsw i64 %72, 1, !dbg !315
  call void @llvm.dbg.value(metadata i32 undef, metadata !135, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !236
  call void @llvm.dbg.value(metadata double %159, metadata !117, metadata !DIExpression()), !dbg !235
  %161 = icmp eq i64 %160, %70, !dbg !237
  br i1 %161, label %162, label %71, !dbg !238, !llvm.loop !316

; <label>:162:                                    ; preds = %158, %55
  %163 = phi double [ 1.000000e+00, %55 ], [ %159, %158 ]
  call void @llvm.dbg.value(metadata double %163, metadata !117, metadata !DIExpression()), !dbg !235
  %164 = load double, double* %31, align 8, !dbg !318, !tbaa !181
  %165 = fcmp olt double %163, %164, !dbg !320
  br i1 %165, label %166, label %167, !dbg !321

; <label>:166:                                    ; preds = %162
  store double %163, double* %31, align 8, !dbg !322, !tbaa !181
  br label %167, !dbg !324

; <label>:167:                                    ; preds = %162, %166, %46
  call void @llvm.dbg.value(metadata i32 undef, metadata !129, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !201
  %168 = icmp slt i64 %50, %45, !dbg !204
  br i1 %168, label %46, label %169, !dbg !205, !llvm.loop !325

; <label>:169:                                    ; preds = %167, %23, %6, %20, %16
  %170 = phi i32 [ 0, %16 ], [ 1, %20 ], [ 0, %6 ], [ 1, %23 ], [ 1, %167 ], !dbg !327
  ret i32 %170, !dbg !328
}

; Function Attrs: nounwind ssp uwtable
define i32 @klu_condest(i32* readonly, double* readonly, %struct.klu_symbolic*, %struct.klu_numeric*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !329 {
  call void @llvm.dbg.value(metadata i32* %0, metadata !333, metadata !DIExpression()), !dbg !364
  call void @llvm.dbg.value(metadata double* %1, metadata !334, metadata !DIExpression()), !dbg !365
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %2, metadata !335, metadata !DIExpression()), !dbg !366
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %3, metadata !336, metadata !DIExpression()), !dbg !367
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %4, metadata !337, metadata !DIExpression()), !dbg !368
  %6 = icmp eq %struct.klu_common_struct* %4, null, !dbg !369
  br i1 %6, label %254, label %7, !dbg !371

; <label>:7:                                      ; preds = %5
  %8 = icmp eq %struct.klu_symbolic* %2, null, !dbg !372
  %9 = icmp eq i32* %0, null, !dbg !374
  %10 = or i1 %9, %8, !dbg !375
  %11 = icmp eq double* %1, null, !dbg !376
  %12 = or i1 %11, %10, !dbg !375
  br i1 %12, label %13, label %15, !dbg !375

; <label>:13:                                     ; preds = %7
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !377
  store i32 -3, i32* %14, align 4, !dbg !379, !tbaa !166
  br label %254, !dbg !380

; <label>:15:                                     ; preds = %7
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !345, metadata !DIExpression()), !dbg !381
  %16 = icmp eq %struct.klu_numeric* %3, null, !dbg !382
  br i1 %16, label %17, label %20, !dbg !384

; <label>:17:                                     ; preds = %15
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19, !dbg !385
  store double 0x7FF0000000000000, double* %18, align 8, !dbg !387, !tbaa !388
  %19 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !389
  store i32 1, i32* %19, align 4, !dbg !390, !tbaa !166
  br label %254, !dbg !391

; <label>:20:                                     ; preds = %15
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !392
  store i32 0, i32* %21, align 4, !dbg !393, !tbaa !166
  %22 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %2, i64 0, i32 5, !dbg !394
  %23 = load i32, i32* %22, align 8, !dbg !394, !tbaa !395
  call void @llvm.dbg.value(metadata i32 %23, metadata !355, metadata !DIExpression()), !dbg !396
  %24 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 14, !dbg !397
  %25 = bitcast i8** %24 to double**, !dbg !397
  %26 = load double*, double** %25, align 8, !dbg !397, !tbaa !232
  call void @llvm.dbg.value(metadata double* %26, metadata !346, metadata !DIExpression()), !dbg !398
  call void @llvm.dbg.value(metadata i32 0, metadata !350, metadata !DIExpression()), !dbg !399
  %27 = icmp sgt i32 %23, 0, !dbg !400
  br i1 %27, label %28, label %76, !dbg !403

; <label>:28:                                     ; preds = %20
  %29 = sext i32 %23 to i64, !dbg !403
  br label %32, !dbg !403

; <label>:30:                                     ; preds = %32
  call void @llvm.dbg.value(metadata i32 undef, metadata !350, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !399
  %31 = icmp slt i64 %40, %29, !dbg !400
  br i1 %31, label %32, label %44, !dbg !403, !llvm.loop !404

; <label>:32:                                     ; preds = %28, %30
  %33 = phi i64 [ 0, %28 ], [ %40, %30 ]
  call void @llvm.dbg.value(metadata i64 %33, metadata !350, metadata !DIExpression()), !dbg !399
  %34 = getelementptr inbounds double, double* %26, i64 %33, !dbg !406
  %35 = load double, double* %34, align 8, !dbg !406, !tbaa !264
  %36 = fcmp olt double %35, 0.000000e+00, !dbg !406
  %37 = fsub double -0.000000e+00, %35, !dbg !406
  %38 = select i1 %36, double %37, double %35, !dbg !406
  call void @llvm.dbg.value(metadata double %38, metadata !345, metadata !DIExpression()), !dbg !381
  %39 = fcmp oeq double %38, 0.000000e+00, !dbg !409
  %40 = add nuw nsw i64 %33, 1, !dbg !411
  call void @llvm.dbg.value(metadata i32 undef, metadata !350, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !399
  br i1 %39, label %41, label %30, !dbg !412

; <label>:41:                                     ; preds = %32
  %42 = fdiv double 1.000000e+00, %38, !dbg !413
  %43 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19, !dbg !415
  store double %42, double* %43, align 8, !dbg !416, !tbaa !388
  store i32 1, i32* %21, align 4, !dbg !417, !tbaa !166
  br label %254, !dbg !418

; <label>:44:                                     ; preds = %30
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !341, metadata !DIExpression()), !dbg !419
  call void @llvm.dbg.value(metadata double* %1, metadata !347, metadata !DIExpression()), !dbg !420
  call void @llvm.dbg.value(metadata i32 0, metadata !350, metadata !DIExpression()), !dbg !399
  %45 = icmp sgt i32 %23, 0, !dbg !421
  br i1 %45, label %46, label %76, !dbg !424

; <label>:46:                                     ; preds = %44
  %47 = zext i32 %23 to i64
  br label %48, !dbg !424

; <label>:48:                                     ; preds = %71, %46
  %49 = phi i64 [ 0, %46 ], [ %51, %71 ]
  %50 = phi double [ 0.000000e+00, %46 ], [ %74, %71 ]
  call void @llvm.dbg.value(metadata i64 %49, metadata !350, metadata !DIExpression()), !dbg !399
  call void @llvm.dbg.value(metadata double %50, metadata !341, metadata !DIExpression()), !dbg !419
  %51 = add nuw nsw i64 %49, 1, !dbg !425
  %52 = getelementptr inbounds i32, i32* %0, i64 %51, !dbg !427
  %53 = load i32, i32* %52, align 4, !dbg !427, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %53, metadata !354, metadata !DIExpression()), !dbg !428
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !340, metadata !DIExpression()), !dbg !429
  %54 = getelementptr inbounds i32, i32* %0, i64 %49, !dbg !430
  %55 = load i32, i32* %54, align 4, !dbg !430, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %55, metadata !351, metadata !DIExpression()), !dbg !432
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !340, metadata !DIExpression()), !dbg !429
  %56 = icmp slt i32 %55, %53, !dbg !433
  br i1 %56, label %57, label %71, !dbg !435

; <label>:57:                                     ; preds = %48
  %58 = sext i32 %55 to i64, !dbg !435
  %59 = sext i32 %53 to i64
  br label %60, !dbg !435

; <label>:60:                                     ; preds = %60, %57
  %61 = phi i64 [ %58, %57 ], [ %69, %60 ]
  %62 = phi double [ 0.000000e+00, %57 ], [ %68, %60 ]
  call void @llvm.dbg.value(metadata double %62, metadata !340, metadata !DIExpression()), !dbg !429
  call void @llvm.dbg.value(metadata i64 %61, metadata !351, metadata !DIExpression()), !dbg !432
  %63 = getelementptr inbounds double, double* %1, i64 %61, !dbg !436
  %64 = load double, double* %63, align 8, !dbg !436, !tbaa !264
  %65 = fcmp olt double %64, 0.000000e+00, !dbg !436
  %66 = fsub double -0.000000e+00, %64, !dbg !436
  %67 = select i1 %65, double %66, double %64, !dbg !436
  call void @llvm.dbg.value(metadata double %67, metadata !345, metadata !DIExpression()), !dbg !381
  %68 = fadd double %62, %67, !dbg !439
  %69 = add nsw i64 %61, 1, !dbg !440
  call void @llvm.dbg.value(metadata double %68, metadata !340, metadata !DIExpression()), !dbg !429
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  %70 = icmp eq i64 %69, %59, !dbg !433
  br i1 %70, label %71, label %60, !dbg !435, !llvm.loop !441

; <label>:71:                                     ; preds = %60, %48
  %72 = phi double [ 0.000000e+00, %48 ], [ %68, %60 ]
  call void @llvm.dbg.value(metadata double %72, metadata !340, metadata !DIExpression()), !dbg !429
  %73 = fcmp ogt double %72, %50, !dbg !443
  call void @llvm.dbg.value(metadata double undef, metadata !341, metadata !DIExpression()), !dbg !419
  %74 = select i1 %73, double %72, double %50, !dbg !445
  call void @llvm.dbg.value(metadata i32 undef, metadata !350, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !399
  call void @llvm.dbg.value(metadata double %74, metadata !341, metadata !DIExpression()), !dbg !419
  %75 = icmp eq i64 %51, %47, !dbg !421
  br i1 %75, label %84, label %48, !dbg !424, !llvm.loop !446

; <label>:76:                                     ; preds = %44, %20
  call void @llvm.dbg.value(metadata double %74, metadata !341, metadata !DIExpression()), !dbg !419
  %77 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 18, !dbg !448
  %78 = bitcast i8** %77 to double**, !dbg !448
  %79 = load double*, double** %78, align 8, !dbg !448, !tbaa !449
  call void @llvm.dbg.value(metadata double* %87, metadata !348, metadata !DIExpression()), !dbg !450
  %80 = sext i32 %23 to i64, !dbg !451
  %81 = getelementptr inbounds double, double* %79, i64 %80, !dbg !451
  %82 = bitcast double* %81 to i8*
  call void @llvm.dbg.value(metadata double* %89, metadata !348, metadata !DIExpression()), !dbg !450
  %83 = getelementptr inbounds double, double* %81, i64 %80, !dbg !452
  call void @llvm.dbg.value(metadata double* %91, metadata !349, metadata !DIExpression()), !dbg !453
  call void @llvm.dbg.value(metadata i32 0, metadata !350, metadata !DIExpression()), !dbg !399
  br label %103, !dbg !454

; <label>:84:                                     ; preds = %71
  call void @llvm.dbg.value(metadata double %74, metadata !341, metadata !DIExpression()), !dbg !419
  %85 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 18, !dbg !448
  %86 = bitcast i8** %85 to double**, !dbg !448
  %87 = load double*, double** %86, align 8, !dbg !448, !tbaa !449
  call void @llvm.dbg.value(metadata double* %87, metadata !348, metadata !DIExpression()), !dbg !450
  %88 = sext i32 %23 to i64, !dbg !451
  %89 = getelementptr inbounds double, double* %87, i64 %88, !dbg !451
  %90 = bitcast double* %89 to i8*
  call void @llvm.dbg.value(metadata double* %89, metadata !348, metadata !DIExpression()), !dbg !450
  %91 = getelementptr inbounds double, double* %89, i64 %88, !dbg !452
  call void @llvm.dbg.value(metadata double* %91, metadata !349, metadata !DIExpression()), !dbg !453
  call void @llvm.dbg.value(metadata i32 0, metadata !350, metadata !DIExpression()), !dbg !399
  %92 = icmp sgt i32 %23, 0, !dbg !456
  br i1 %92, label %93, label %103, !dbg !454

; <label>:93:                                     ; preds = %84
  %94 = sitofp i32 %23 to double
  %95 = fdiv double 1.000000e+00, %94
  %96 = zext i32 %23 to i64
  br label %97, !dbg !454

; <label>:97:                                     ; preds = %97, %93
  %98 = phi i64 [ 0, %93 ], [ %101, %97 ]
  call void @llvm.dbg.value(metadata i64 %98, metadata !350, metadata !DIExpression()), !dbg !399
  %99 = getelementptr inbounds double, double* %91, i64 %98, !dbg !458
  store double 0.000000e+00, double* %99, align 8, !dbg !458, !tbaa !264
  %100 = getelementptr inbounds double, double* %89, i64 %98, !dbg !461
  store double %95, double* %100, align 8, !dbg !463, !tbaa !264
  %101 = add nuw nsw i64 %98, 1, !dbg !464
  call void @llvm.dbg.value(metadata i32 undef, metadata !350, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !399
  %102 = icmp eq i64 %101, %96, !dbg !456
  br i1 %102, label %103, label %97, !dbg !454, !llvm.loop !465

; <label>:103:                                    ; preds = %97, %76, %84
  %104 = phi double* [ %83, %76 ], [ %91, %84 ], [ %91, %97 ]
  %105 = phi i8* [ %82, %76 ], [ %90, %84 ], [ %90, %97 ]
  %106 = phi double* [ %81, %76 ], [ %89, %84 ], [ %89, %97 ]
  %107 = phi double [ 0.000000e+00, %76 ], [ %74, %84 ], [ %74, %97 ]
  call void @llvm.dbg.value(metadata i32 0, metadata !352, metadata !DIExpression()), !dbg !467
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata i32 0, metadata !350, metadata !DIExpression()), !dbg !399
  %108 = icmp sgt i32 %23, 0
  %109 = icmp sgt i32 %23, 0
  %110 = icmp sgt i32 %23, 0
  %111 = icmp sgt i32 %23, 0
  %112 = icmp sgt i32 %23, 0
  %113 = zext i32 %23 to i64, !dbg !469
  %114 = shl nuw nsw i64 %113, 3, !dbg !469
  %115 = zext i32 %23 to i64
  %116 = zext i32 %23 to i64
  %117 = zext i32 %23 to i64
  %118 = zext i32 %23 to i64
  br label %119, !dbg !469

; <label>:119:                                    ; preds = %198, %103
  %120 = phi i32 [ 0, %103 ], [ %199, %198 ]
  %121 = phi i32 [ 0, %103 ], [ %202, %198 ]
  %122 = phi double [ 0.000000e+00, %103 ], [ %144, %198 ]
  call void @llvm.dbg.value(metadata i32 %120, metadata !352, metadata !DIExpression()), !dbg !467
  call void @llvm.dbg.value(metadata i32 %121, metadata !350, metadata !DIExpression()), !dbg !399
  call void @llvm.dbg.value(metadata double %122, metadata !342, metadata !DIExpression()), !dbg !468
  %123 = icmp ne i32 %121, 0, !dbg !470
  br i1 %123, label %124, label %129, !dbg !472

; <label>:124:                                    ; preds = %119
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br i1 %108, label %125, label %126, !dbg !473

; <label>:125:                                    ; preds = %124
  call void @llvm.memset.p0i8.i64(i8* %105, i8 0, i64 %114, i32 8, i1 false), !dbg !476
  br label %126, !dbg !480

; <label>:126:                                    ; preds = %125, %124
  %127 = sext i32 %120 to i64, !dbg !480
  %128 = getelementptr inbounds double, double* %106, i64 %127, !dbg !480
  store double 1.000000e+00, double* %128, align 8, !dbg !481, !tbaa !264
  br label %129, !dbg !482

; <label>:129:                                    ; preds = %126, %119
  %130 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %106, %struct.klu_common_struct* nonnull %4) #4, !dbg !483
  call void @llvm.dbg.value(metadata double %122, metadata !343, metadata !DIExpression()), !dbg !484
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br i1 %109, label %131, label %143, !dbg !485

; <label>:131:                                    ; preds = %129
  br label %132, !dbg !487

; <label>:132:                                    ; preds = %131, %132
  %133 = phi i64 [ %141, %132 ], [ 0, %131 ]
  %134 = phi double [ %140, %132 ], [ 0.000000e+00, %131 ]
  call void @llvm.dbg.value(metadata i64 %133, metadata !351, metadata !DIExpression()), !dbg !432
  call void @llvm.dbg.value(metadata double %134, metadata !342, metadata !DIExpression()), !dbg !468
  %135 = getelementptr inbounds double, double* %106, i64 %133, !dbg !487
  %136 = load double, double* %135, align 8, !dbg !487, !tbaa !264
  %137 = fcmp olt double %136, 0.000000e+00, !dbg !487
  %138 = fsub double -0.000000e+00, %136, !dbg !487
  %139 = select i1 %137, double %138, double %136, !dbg !487
  call void @llvm.dbg.value(metadata double %139, metadata !345, metadata !DIExpression()), !dbg !381
  %140 = fadd double %134, %139, !dbg !491
  %141 = add nuw nsw i64 %133, 1, !dbg !492
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  call void @llvm.dbg.value(metadata double %140, metadata !342, metadata !DIExpression()), !dbg !468
  %142 = icmp eq i64 %141, %115, !dbg !493
  br i1 %142, label %143, label %132, !dbg !485, !llvm.loop !494

; <label>:143:                                    ; preds = %132, %129
  %144 = phi double [ 0.000000e+00, %129 ], [ %140, %132 ]
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata i32 1, metadata !356, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br i1 %110, label %145, label %163, !dbg !497

; <label>:145:                                    ; preds = %143
  br label %146, !dbg !498

; <label>:146:                                    ; preds = %145, %159
  %147 = phi i64 [ %161, %159 ], [ 0, %145 ]
  %148 = phi i32 [ %160, %159 ], [ 1, %145 ]
  call void @llvm.dbg.value(metadata i32 %148, metadata !356, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i64 %147, metadata !351, metadata !DIExpression()), !dbg !432
  %149 = getelementptr inbounds double, double* %106, i64 %147, !dbg !498
  %150 = load double, double* %149, align 8, !dbg !498, !tbaa !264
  %151 = fcmp oge double %150, 0.000000e+00, !dbg !499
  %152 = select i1 %151, double 1.000000e+00, double -1.000000e+00, !dbg !500
  call void @llvm.dbg.value(metadata double %152, metadata !357, metadata !DIExpression()), !dbg !501
  %153 = getelementptr inbounds double, double* %104, i64 %147, !dbg !502
  %154 = load double, double* %153, align 8, !dbg !502, !tbaa !264
  %155 = fptosi double %154 to i32, !dbg !504
  %156 = sitofp i32 %155 to double, !dbg !504
  %157 = fcmp une double %152, %156, !dbg !505
  br i1 %157, label %158, label %159, !dbg !506

; <label>:158:                                    ; preds = %146
  store double %152, double* %153, align 8, !dbg !507, !tbaa !264
  call void @llvm.dbg.value(metadata i32 0, metadata !356, metadata !DIExpression()), !dbg !496
  br label %159, !dbg !509

; <label>:159:                                    ; preds = %158, %146
  %160 = phi i32 [ 0, %158 ], [ %148, %146 ], !dbg !510
  %161 = add nuw nsw i64 %147, 1, !dbg !511
  call void @llvm.dbg.value(metadata i32 %160, metadata !356, metadata !DIExpression()), !dbg !496
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  %162 = icmp eq i64 %161, %116, !dbg !512
  br i1 %162, label %163, label %146, !dbg !497, !llvm.loop !513

; <label>:163:                                    ; preds = %159, %143
  %164 = phi i32 [ 1, %143 ], [ %160, %159 ]
  call void @llvm.dbg.value(metadata i32 %164, metadata !356, metadata !DIExpression()), !dbg !496
  br i1 %123, label %165, label %169, !dbg !515

; <label>:165:                                    ; preds = %163
  %166 = fcmp ole double %144, %122, !dbg !517
  %167 = icmp ne i32 %164, 0, !dbg !518
  %168 = or i1 %166, %167, !dbg !519
  br i1 %168, label %206, label %169, !dbg !519

; <label>:169:                                    ; preds = %165, %163
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br i1 %111, label %170, label %180, !dbg !520

; <label>:170:                                    ; preds = %169
  br label %171, !dbg !522

; <label>:171:                                    ; preds = %170, %171
  %172 = phi i64 [ %178, %171 ], [ 0, %170 ]
  call void @llvm.dbg.value(metadata i64 %172, metadata !351, metadata !DIExpression()), !dbg !432
  %173 = getelementptr inbounds double, double* %104, i64 %172, !dbg !522
  %174 = bitcast double* %173 to i64*, !dbg !522
  %175 = load i64, i64* %174, align 8, !dbg !522, !tbaa !264
  %176 = getelementptr inbounds double, double* %106, i64 %172, !dbg !525
  %177 = bitcast double* %176 to i64*, !dbg !526
  store i64 %175, i64* %177, align 8, !dbg !526, !tbaa !264
  %178 = add nuw nsw i64 %172, 1, !dbg !527
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  %179 = icmp eq i64 %178, %117, !dbg !528
  br i1 %179, label %180, label %171, !dbg !520, !llvm.loop !529

; <label>:180:                                    ; preds = %171, %169
  %181 = tail call i32 @klu_tsolve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %106, %struct.klu_common_struct* nonnull %4) #4, !dbg !531
  call void @llvm.dbg.value(metadata i32 0, metadata !353, metadata !DIExpression()), !dbg !532
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !339, metadata !DIExpression()), !dbg !533
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br i1 %112, label %182, label %198, !dbg !534

; <label>:182:                                    ; preds = %180
  br label %183, !dbg !536

; <label>:183:                                    ; preds = %182, %183
  %184 = phi i64 [ %196, %183 ], [ 0, %182 ]
  %185 = phi double [ %195, %183 ], [ 0.000000e+00, %182 ]
  %186 = phi i32 [ %194, %183 ], [ 0, %182 ]
  call void @llvm.dbg.value(metadata double %185, metadata !339, metadata !DIExpression()), !dbg !533
  call void @llvm.dbg.value(metadata i32 %186, metadata !353, metadata !DIExpression()), !dbg !532
  call void @llvm.dbg.value(metadata i64 %184, metadata !351, metadata !DIExpression()), !dbg !432
  %187 = getelementptr inbounds double, double* %106, i64 %184, !dbg !536
  %188 = load double, double* %187, align 8, !dbg !536, !tbaa !264
  %189 = fcmp olt double %188, 0.000000e+00, !dbg !536
  %190 = fsub double -0.000000e+00, %188, !dbg !536
  %191 = select i1 %189, double %190, double %188, !dbg !536
  call void @llvm.dbg.value(metadata double %191, metadata !338, metadata !DIExpression()), !dbg !540
  %192 = fcmp ogt double %191, %185, !dbg !541
  call void @llvm.dbg.value(metadata double %191, metadata !339, metadata !DIExpression()), !dbg !533
  call void @llvm.dbg.value(metadata i64 %184, metadata !353, metadata !DIExpression()), !dbg !532
  %193 = trunc i64 %184 to i32, !dbg !543
  %194 = select i1 %192, i32 %193, i32 %186, !dbg !543
  %195 = select i1 %192, double %191, double %185, !dbg !543
  %196 = add nuw nsw i64 %184, 1, !dbg !544
  call void @llvm.dbg.value(metadata double %195, metadata !339, metadata !DIExpression()), !dbg !533
  call void @llvm.dbg.value(metadata i32 %194, metadata !353, metadata !DIExpression()), !dbg !532
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  %197 = icmp eq i64 %196, %118, !dbg !545
  br i1 %197, label %198, label %183, !dbg !534, !llvm.loop !546

; <label>:198:                                    ; preds = %183, %180
  %199 = phi i32 [ 0, %180 ], [ %194, %183 ]
  call void @llvm.dbg.value(metadata i32 %199, metadata !353, metadata !DIExpression()), !dbg !532
  %200 = icmp eq i32 %199, %120, !dbg !548
  %201 = and i1 %123, %200, !dbg !550
  call void @llvm.dbg.value(metadata i32 undef, metadata !352, metadata !DIExpression()), !dbg !467
  %202 = add nuw nsw i32 %121, 1, !dbg !551
  call void @llvm.dbg.value(metadata i32 %202, metadata !350, metadata !DIExpression()), !dbg !399
  %203 = xor i1 %201, true, !dbg !550
  %204 = icmp ult i32 %202, 5, !dbg !552
  %205 = and i1 %204, %203, !dbg !550
  call void @llvm.dbg.value(metadata i32 %199, metadata !352, metadata !DIExpression()), !dbg !467
  call void @llvm.dbg.value(metadata i32 %202, metadata !350, metadata !DIExpression()), !dbg !399
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  br i1 %205, label %119, label %206, !dbg !550, !llvm.loop !553

; <label>:206:                                    ; preds = %198, %165
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata double %144, metadata !342, metadata !DIExpression()), !dbg !468
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  %207 = icmp sgt i32 %23, 0, !dbg !555
  br i1 %207, label %210, label %208, !dbg !558

; <label>:208:                                    ; preds = %206
  %209 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %106, %struct.klu_common_struct* nonnull %4) #4, !dbg !559
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !344, metadata !DIExpression()), !dbg !560
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  br label %245, !dbg !561

; <label>:210:                                    ; preds = %206
  %211 = add nsw i32 %23, -1
  %212 = sitofp i32 %211 to double
  %213 = zext i32 %23 to i64
  br label %214, !dbg !558

; <label>:214:                                    ; preds = %214, %210
  %215 = phi i64 [ 0, %210 ], [ %225, %214 ]
  call void @llvm.dbg.value(metadata i64 %215, metadata !351, metadata !DIExpression()), !dbg !432
  %216 = getelementptr inbounds double, double* %106, i64 %215, !dbg !563
  %217 = and i64 %215, 1, !dbg !566
  %218 = icmp eq i64 %217, 0, !dbg !566
  %219 = trunc i64 %215 to i32, !dbg !568
  %220 = sitofp i32 %219 to double, !dbg !568
  %221 = fdiv double %220, %212, !dbg !568
  %222 = fsub double -1.000000e+00, %221, !dbg !570
  %223 = fadd double %221, 1.000000e+00, !dbg !571
  %224 = select i1 %218, double %222, double %223, !dbg !573
  store double %224, double* %216, align 8, !dbg !574, !tbaa !264
  %225 = add nuw nsw i64 %215, 1, !dbg !575
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  %226 = icmp eq i64 %225, %213, !dbg !555
  br i1 %226, label %227, label %214, !dbg !558, !llvm.loop !576

; <label>:227:                                    ; preds = %214
  %228 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* nonnull %106, %struct.klu_common_struct* nonnull %4) #4, !dbg !559
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !344, metadata !DIExpression()), !dbg !560
  call void @llvm.dbg.value(metadata i32 0, metadata !351, metadata !DIExpression()), !dbg !432
  %229 = icmp sgt i32 %23, 0, !dbg !578
  br i1 %229, label %230, label %245, !dbg !561

; <label>:230:                                    ; preds = %227
  %231 = zext i32 %23 to i64
  br label %232, !dbg !561

; <label>:232:                                    ; preds = %232, %230
  %233 = phi i64 [ 0, %230 ], [ %241, %232 ]
  %234 = phi double [ 0.000000e+00, %230 ], [ %240, %232 ]
  call void @llvm.dbg.value(metadata i64 %233, metadata !351, metadata !DIExpression()), !dbg !432
  call void @llvm.dbg.value(metadata double %234, metadata !344, metadata !DIExpression()), !dbg !560
  %235 = getelementptr inbounds double, double* %106, i64 %233, !dbg !580
  %236 = load double, double* %235, align 8, !dbg !580, !tbaa !264
  %237 = fcmp olt double %236, 0.000000e+00, !dbg !580
  %238 = fsub double -0.000000e+00, %236, !dbg !580
  %239 = select i1 %237, double %238, double %236, !dbg !580
  call void @llvm.dbg.value(metadata double %239, metadata !345, metadata !DIExpression()), !dbg !381
  %240 = fadd double %234, %239, !dbg !583
  %241 = add nuw nsw i64 %233, 1, !dbg !584
  call void @llvm.dbg.value(metadata i32 undef, metadata !351, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !432
  call void @llvm.dbg.value(metadata double %240, metadata !344, metadata !DIExpression()), !dbg !560
  %242 = icmp eq i64 %241, %231, !dbg !578
  br i1 %242, label %243, label %232, !dbg !561, !llvm.loop !585

; <label>:243:                                    ; preds = %232
  %244 = fmul double %240, 2.000000e+00, !dbg !587
  br label %245, !dbg !587

; <label>:245:                                    ; preds = %208, %243, %227
  %246 = phi double [ 0.000000e+00, %227 ], [ %244, %243 ], [ 0.000000e+00, %208 ]
  %247 = mul nsw i32 %23, 3, !dbg !588
  %248 = sitofp i32 %247 to double, !dbg !589
  %249 = fdiv double %246, %248, !dbg !590
  call void @llvm.dbg.value(metadata double %249, metadata !344, metadata !DIExpression()), !dbg !560
  %250 = fcmp ogt double %249, %144, !dbg !591
  %251 = select i1 %250, double %249, double %144, !dbg !591
  call void @llvm.dbg.value(metadata double %251, metadata !342, metadata !DIExpression()), !dbg !468
  %252 = fmul double %107, %251, !dbg !592
  %253 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19, !dbg !593
  store double %252, double* %253, align 8, !dbg !594, !tbaa !388
  br label %254, !dbg !595

; <label>:254:                                    ; preds = %5, %245, %41, %17, %13
  %255 = phi i32 [ 0, %13 ], [ 1, %17 ], [ 1, %41 ], [ 1, %245 ], [ 0, %5 ], !dbg !596
  ret i32 %255, !dbg !597
}

declare i32 @klu_solve(%struct.klu_symbolic*, %struct.klu_numeric*, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #1

declare i32 @klu_tsolve(%struct.klu_symbolic*, %struct.klu_numeric*, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: nounwind ssp uwtable
define i32 @klu_flops(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !598 {
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %0, metadata !602, metadata !DIExpression()), !dbg !620
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %1, metadata !603, metadata !DIExpression()), !dbg !621
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %2, metadata !604, metadata !DIExpression()), !dbg !622
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !605, metadata !DIExpression()), !dbg !623
  %4 = icmp eq %struct.klu_common_struct* %2, null, !dbg !624
  br i1 %4, label %86, label %5, !dbg !626

; <label>:5:                                      ; preds = %3
  %6 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 17, !dbg !627
  store double -1.000000e+00, double* %6, align 8, !dbg !628, !tbaa !629
  %7 = icmp eq %struct.klu_numeric* %1, null, !dbg !630
  %8 = icmp eq %struct.klu_symbolic* %0, null, !dbg !632
  %9 = or i1 %8, %7, !dbg !633
  %10 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !634
  br i1 %9, label %11, label %12, !dbg !633

; <label>:11:                                     ; preds = %5
  store i32 -3, i32* %10, align 4, !dbg !635, !tbaa !166
  br label %86, !dbg !637

; <label>:12:                                     ; preds = %5
  store i32 0, i32* %10, align 4, !dbg !638, !tbaa !166
  %13 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 9, !dbg !639
  %14 = load i32*, i32** %13, align 8, !dbg !639, !tbaa !206
  call void @llvm.dbg.value(metadata i32* %14, metadata !606, metadata !DIExpression()), !dbg !640
  %15 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 11, !dbg !641
  %16 = load i32, i32* %15, align 4, !dbg !641, !tbaa !203
  call void @llvm.dbg.value(metadata i32 %16, metadata !618, metadata !DIExpression()), !dbg !642
  %17 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 12, !dbg !643
  %18 = bitcast i8*** %17 to double***, !dbg !643
  %19 = load double**, double*** %18, align 8, !dbg !643, !tbaa !219
  call void @llvm.dbg.value(metadata double** %19, metadata !611, metadata !DIExpression()), !dbg !644
  call void @llvm.dbg.value(metadata i32 0, metadata !617, metadata !DIExpression()), !dbg !645
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !605, metadata !DIExpression()), !dbg !623
  %20 = icmp sgt i32 %16, 0, !dbg !646
  br i1 %20, label %21, label %84, !dbg !649

; <label>:21:                                     ; preds = %12
  %22 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 10
  %23 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 9
  %24 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 11
  %25 = zext i32 %16 to i64
  br label %26, !dbg !649

; <label>:26:                                     ; preds = %81, %21
  %27 = phi i64 [ 0, %21 ], [ %31, %81 ]
  %28 = phi double [ 0.000000e+00, %21 ], [ %82, %81 ]
  call void @llvm.dbg.value(metadata double %28, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i64 %27, metadata !617, metadata !DIExpression()), !dbg !645
  %29 = getelementptr inbounds i32, i32* %14, i64 %27, !dbg !650
  %30 = load i32, i32* %29, align 4, !dbg !650, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %30, metadata !619, metadata !DIExpression()), !dbg !652
  %31 = add nuw nsw i64 %27, 1, !dbg !653
  %32 = getelementptr inbounds i32, i32* %14, i64 %31, !dbg !654
  %33 = load i32, i32* %32, align 4, !dbg !654, !tbaa !208
  %34 = sub nsw i32 %33, %30, !dbg !655
  call void @llvm.dbg.value(metadata i32 %34, metadata !616, metadata !DIExpression()), !dbg !656
  %35 = icmp sgt i32 %34, 1, !dbg !657
  br i1 %35, label %36, label %81, !dbg !659

; <label>:36:                                     ; preds = %26
  %37 = load i32*, i32** %22, align 8, !dbg !660, !tbaa !662
  %38 = sext i32 %30 to i64, !dbg !663
  %39 = getelementptr inbounds i32, i32* %37, i64 %38, !dbg !663
  call void @llvm.dbg.value(metadata i32* %39, metadata !609, metadata !DIExpression()), !dbg !664
  %40 = load i32*, i32** %23, align 8, !dbg !665, !tbaa !224
  %41 = getelementptr inbounds i32, i32* %40, i64 %38, !dbg !666
  call void @llvm.dbg.value(metadata i32* %41, metadata !608, metadata !DIExpression()), !dbg !667
  %42 = load i32*, i32** %24, align 8, !dbg !668, !tbaa !228
  %43 = getelementptr inbounds i32, i32* %42, i64 %38, !dbg !669
  call void @llvm.dbg.value(metadata i32* %43, metadata !610, metadata !DIExpression()), !dbg !670
  %44 = getelementptr inbounds double*, double** %19, i64 %27, !dbg !671
  %45 = load double*, double** %44, align 8, !dbg !671, !tbaa !221
  call void @llvm.dbg.value(metadata double* %45, metadata !612, metadata !DIExpression()), !dbg !672
  call void @llvm.dbg.value(metadata i32 0, metadata !613, metadata !DIExpression()), !dbg !673
  call void @llvm.dbg.value(metadata double %28, metadata !605, metadata !DIExpression()), !dbg !623
  %46 = zext i32 %34 to i64
  br label %47, !dbg !674

; <label>:47:                                     ; preds = %73, %36
  %48 = phi i64 [ 0, %36 ], [ %79, %73 ]
  %49 = phi double [ %28, %36 ], [ %78, %73 ]
  call void @llvm.dbg.value(metadata double %49, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i64 %48, metadata !613, metadata !DIExpression()), !dbg !673
  %50 = getelementptr inbounds i32, i32* %41, i64 %48, !dbg !676
  %51 = load i32, i32* %50, align 4, !dbg !676, !tbaa !208
  %52 = sext i32 %51 to i64, !dbg !676
  %53 = getelementptr inbounds double, double* %45, i64 %52, !dbg !676
  %54 = bitcast double* %53 to i32*, !dbg !676
  call void @llvm.dbg.value(metadata i32* %54, metadata !607, metadata !DIExpression()), !dbg !680
  %55 = getelementptr inbounds i32, i32* %43, i64 %48, !dbg !681
  %56 = load i32, i32* %55, align 4, !dbg !681, !tbaa !208
  call void @llvm.dbg.value(metadata i32 %56, metadata !614, metadata !DIExpression()), !dbg !682
  call void @llvm.dbg.value(metadata i32 0, metadata !615, metadata !DIExpression()), !dbg !683
  call void @llvm.dbg.value(metadata double %49, metadata !605, metadata !DIExpression()), !dbg !623
  %57 = icmp sgt i32 %56, 0, !dbg !684
  br i1 %57, label %58, label %73, !dbg !687

; <label>:58:                                     ; preds = %47
  %59 = zext i32 %56 to i64
  br label %60, !dbg !687

; <label>:60:                                     ; preds = %60, %58
  %61 = phi i64 [ 0, %58 ], [ %71, %60 ]
  %62 = phi double [ %49, %58 ], [ %70, %60 ]
  call void @llvm.dbg.value(metadata double %62, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i64 %61, metadata !615, metadata !DIExpression()), !dbg !683
  %63 = getelementptr inbounds i32, i32* %54, i64 %61, !dbg !688
  %64 = load i32, i32* %63, align 4, !dbg !688, !tbaa !208
  %65 = sext i32 %64 to i64, !dbg !690
  %66 = getelementptr inbounds i32, i32* %39, i64 %65, !dbg !690
  %67 = load i32, i32* %66, align 4, !dbg !690, !tbaa !208
  %68 = shl nsw i32 %67, 1, !dbg !691
  %69 = sitofp i32 %68 to double, !dbg !692
  %70 = fadd double %62, %69, !dbg !693
  %71 = add nuw nsw i64 %61, 1, !dbg !694
  call void @llvm.dbg.value(metadata double %70, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i32 undef, metadata !615, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !683
  %72 = icmp eq i64 %71, %59, !dbg !684
  br i1 %72, label %73, label %60, !dbg !687, !llvm.loop !695

; <label>:73:                                     ; preds = %60, %47
  %74 = phi double [ %49, %47 ], [ %70, %60 ]
  call void @llvm.dbg.value(metadata double %74, metadata !605, metadata !DIExpression()), !dbg !623
  %75 = getelementptr inbounds i32, i32* %39, i64 %48, !dbg !697
  %76 = load i32, i32* %75, align 4, !dbg !697, !tbaa !208
  %77 = sitofp i32 %76 to double, !dbg !697
  %78 = fadd double %74, %77, !dbg !698
  %79 = add nuw nsw i64 %48, 1, !dbg !699
  call void @llvm.dbg.value(metadata double %78, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i32 undef, metadata !613, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !673
  %80 = icmp eq i64 %79, %46, !dbg !700
  br i1 %80, label %81, label %47, !dbg !674, !llvm.loop !701

; <label>:81:                                     ; preds = %73, %26
  %82 = phi double [ %28, %26 ], [ %78, %73 ], !dbg !623
  call void @llvm.dbg.value(metadata double %82, metadata !605, metadata !DIExpression()), !dbg !623
  call void @llvm.dbg.value(metadata i32 undef, metadata !617, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !645
  %83 = icmp eq i64 %31, %25, !dbg !646
  br i1 %83, label %84, label %26, !dbg !649, !llvm.loop !703

; <label>:84:                                     ; preds = %81, %12
  %85 = phi double [ 0.000000e+00, %12 ], [ %82, %81 ]
  call void @llvm.dbg.value(metadata double %85, metadata !605, metadata !DIExpression()), !dbg !623
  store double %85, double* %6, align 8, !dbg !705, !tbaa !629
  br label %86, !dbg !706

; <label>:86:                                     ; preds = %3, %84, %11
  %87 = phi i32 [ 0, %11 ], [ 1, %84 ], [ 0, %3 ], !dbg !634
  ret i32 %87, !dbg !707
}

; Function Attrs: nounwind ssp uwtable
define i32 @klu_rcond(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !708 {
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %0, metadata !710, metadata !DIExpression()), !dbg !719
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %1, metadata !711, metadata !DIExpression()), !dbg !720
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %2, metadata !712, metadata !DIExpression()), !dbg !721
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !714, metadata !DIExpression()), !dbg !722
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !715, metadata !DIExpression()), !dbg !723
  %4 = icmp eq %struct.klu_common_struct* %2, null, !dbg !724
  br i1 %4, label %55, label %5, !dbg !726

; <label>:5:                                      ; preds = %3
  %6 = icmp eq %struct.klu_symbolic* %0, null, !dbg !727
  br i1 %6, label %7, label %9, !dbg !729

; <label>:7:                                      ; preds = %5
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !730
  store i32 -3, i32* %8, align 4, !dbg !732, !tbaa !166
  br label %55, !dbg !733

; <label>:9:                                      ; preds = %5
  %10 = icmp eq %struct.klu_numeric* %1, null, !dbg !734
  br i1 %10, label %11, label %14, !dbg !736

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18, !dbg !737
  store double 0.000000e+00, double* %12, align 8, !dbg !739, !tbaa !740
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !741
  store i32 1, i32* %13, align 4, !dbg !742, !tbaa !166
  br label %55, !dbg !743

; <label>:14:                                     ; preds = %9
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !744
  store i32 0, i32* %15, align 4, !dbg !745, !tbaa !166
  %16 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 5, !dbg !746
  %17 = load i32, i32* %16, align 8, !dbg !746, !tbaa !395
  call void @llvm.dbg.value(metadata i32 %17, metadata !718, metadata !DIExpression()), !dbg !747
  %18 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 14, !dbg !748
  %19 = bitcast i8** %18 to double**, !dbg !748
  %20 = load double*, double** %19, align 8, !dbg !748, !tbaa !232
  call void @llvm.dbg.value(metadata double* %20, metadata !716, metadata !DIExpression()), !dbg !749
  call void @llvm.dbg.value(metadata i32 0, metadata !717, metadata !DIExpression()), !dbg !750
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !714, metadata !DIExpression()), !dbg !722
  call void @llvm.dbg.value(metadata double 0.000000e+00, metadata !715, metadata !DIExpression()), !dbg !723
  %21 = icmp sgt i32 %17, 0, !dbg !751
  br i1 %21, label %22, label %48, !dbg !754

; <label>:22:                                     ; preds = %14
  %23 = sext i32 %17 to i64, !dbg !754
  br label %24, !dbg !754

; <label>:24:                                     ; preds = %22, %43
  %25 = phi i64 [ 0, %22 ], [ %46, %43 ]
  %26 = phi double [ 0.000000e+00, %22 ], [ %45, %43 ]
  %27 = phi double [ 0.000000e+00, %22 ], [ %44, %43 ]
  call void @llvm.dbg.value(metadata i64 %25, metadata !717, metadata !DIExpression()), !dbg !750
  call void @llvm.dbg.value(metadata double %26, metadata !714, metadata !DIExpression()), !dbg !722
  call void @llvm.dbg.value(metadata double %27, metadata !715, metadata !DIExpression()), !dbg !723
  %28 = getelementptr inbounds double, double* %20, i64 %25, !dbg !755
  %29 = load double, double* %28, align 8, !dbg !755, !tbaa !264
  %30 = fcmp olt double %29, 0.000000e+00, !dbg !755
  %31 = fsub double -0.000000e+00, %29, !dbg !755
  %32 = select i1 %30, double %31, double %29, !dbg !755
  call void @llvm.dbg.value(metadata double %32, metadata !713, metadata !DIExpression()), !dbg !758
  %33 = fcmp ueq double %32, 0.000000e+00, !dbg !759
  br i1 %33, label %34, label %36, !dbg !759

; <label>:34:                                     ; preds = %24
  %35 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18, !dbg !761
  store double 0.000000e+00, double* %35, align 8, !dbg !763, !tbaa !740
  store i32 1, i32* %15, align 4, !dbg !764, !tbaa !166
  br label %55, !dbg !765

; <label>:36:                                     ; preds = %24
  %37 = icmp eq i64 %25, 0, !dbg !766
  br i1 %37, label %43, label %38, !dbg !768

; <label>:38:                                     ; preds = %36
  %39 = fcmp olt double %26, %32, !dbg !769
  %40 = select i1 %39, double %26, double %32, !dbg !769
  call void @llvm.dbg.value(metadata double %40, metadata !714, metadata !DIExpression()), !dbg !722
  %41 = fcmp ogt double %27, %32, !dbg !771
  %42 = select i1 %41, double %27, double %32, !dbg !771
  call void @llvm.dbg.value(metadata double %42, metadata !715, metadata !DIExpression()), !dbg !723
  br label %43

; <label>:43:                                     ; preds = %36, %38
  %44 = phi double [ %42, %38 ], [ %32, %36 ], !dbg !772
  %45 = phi double [ %40, %38 ], [ %32, %36 ], !dbg !772
  %46 = add nuw nsw i64 %25, 1, !dbg !773
  call void @llvm.dbg.value(metadata i32 undef, metadata !717, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !750
  call void @llvm.dbg.value(metadata double %45, metadata !714, metadata !DIExpression()), !dbg !722
  call void @llvm.dbg.value(metadata double %44, metadata !715, metadata !DIExpression()), !dbg !723
  %47 = icmp slt i64 %46, %23, !dbg !751
  br i1 %47, label %24, label %48, !dbg !754, !llvm.loop !774

; <label>:48:                                     ; preds = %43, %14
  %49 = phi double [ 0.000000e+00, %14 ], [ %44, %43 ]
  %50 = phi double [ 0.000000e+00, %14 ], [ %45, %43 ]
  call void @llvm.dbg.value(metadata double %49, metadata !715, metadata !DIExpression()), !dbg !723
  call void @llvm.dbg.value(metadata double %50, metadata !714, metadata !DIExpression()), !dbg !722
  %51 = fdiv double %50, %49, !dbg !776
  %52 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18, !dbg !777
  store double %51, double* %52, align 8, !dbg !778, !tbaa !740
  %53 = fcmp ueq double %51, 0.000000e+00, !dbg !779
  br i1 %53, label %54, label %55, !dbg !779

; <label>:54:                                     ; preds = %48
  store double 0.000000e+00, double* %52, align 8, !dbg !781, !tbaa !740
  store i32 1, i32* %15, align 4, !dbg !783, !tbaa !166
  br label %55, !dbg !784

; <label>:55:                                     ; preds = %54, %48, %3, %34, %11, %7
  %56 = phi i32 [ 0, %7 ], [ 1, %11 ], [ 1, %34 ], [ 0, %3 ], [ 1, %48 ], [ 1, %54 ], !dbg !785
  ret i32 %56, !dbg !786
}

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
!llvm.module.flags = !{!13, !14, !15, !16}
!llvm.ident = !{!17}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_diagnostics.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5, !7, !10, !6, !11, !12}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!7 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !8, size: 64)
!8 = !DIDerivedType(tag: DW_TAG_typedef, name: "Unit", file: !9, line: 253, baseType: !6)
!9 = !DIFile(filename: "./klu_version.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!10 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !11, size: 64)
!11 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!12 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !7, size: 64)
!13 = !{i32 2, !"Dwarf Version", i32 4}
!14 = !{i32 2, !"Debug Info Version", i32 3}
!15 = !{i32 1, !"wchar_size", i32 4}
!16 = !{i32 7, !"PIC Level", i32 2}
!17 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!18 = distinct !DISubprogram(name: "klu_rgrowth", scope: !1, file: !1, line: 25, type: !19, isLocal: false, isDefinition: true, scopeLine: 34, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !107)
!19 = !DISubroutineType(types: !20)
!20 = !{!11, !10, !10, !5, !21, !42, !75}
!21 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !22, size: 64)
!22 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_symbolic", file: !23, line: 54, baseType: !24)
!23 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!24 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !23, line: 23, size: 768, elements: !25)
!25 = !{!26, !27, !28, !29, !30, !31, !32, !33, !34, !35, !36, !37, !38, !39, !40, !41}
!26 = !DIDerivedType(tag: DW_TAG_member, name: "symmetry", scope: !24, file: !23, line: 31, baseType: !6, size: 64)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "est_flops", scope: !24, file: !23, line: 32, baseType: !6, size: 64, offset: 64)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !24, file: !23, line: 33, baseType: !6, size: 64, offset: 128)
!29 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !24, file: !23, line: 33, baseType: !6, size: 64, offset: 192)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "Lnz", scope: !24, file: !23, line: 34, baseType: !5, size: 64, offset: 256)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !24, file: !23, line: 38, baseType: !11, size: 32, offset: 320)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "nz", scope: !24, file: !23, line: 39, baseType: !11, size: 32, offset: 352)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "P", scope: !24, file: !23, line: 40, baseType: !10, size: 64, offset: 384)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "Q", scope: !24, file: !23, line: 41, baseType: !10, size: 64, offset: 448)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "R", scope: !24, file: !23, line: 42, baseType: !10, size: 64, offset: 512)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !24, file: !23, line: 43, baseType: !11, size: 32, offset: 576)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !24, file: !23, line: 44, baseType: !11, size: 32, offset: 608)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "maxblock", scope: !24, file: !23, line: 45, baseType: !11, size: 32, offset: 640)
!39 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !24, file: !23, line: 46, baseType: !11, size: 32, offset: 672)
!40 = !DIDerivedType(tag: DW_TAG_member, name: "do_btf", scope: !24, file: !23, line: 47, baseType: !11, size: 32, offset: 704)
!41 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !24, file: !23, line: 50, baseType: !11, size: 32, offset: 736)
!42 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !43, size: 64)
!43 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_numeric", file: !23, line: 107, baseType: !44)
!44 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !23, line: 69, size: 1344, elements: !45)
!45 = !{!46, !47, !48, !49, !50, !51, !52, !53, !54, !55, !56, !57, !58, !60, !65, !66, !67, !68, !69, !70, !71, !72, !73, !74}
!46 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !44, file: !23, line: 74, baseType: !11, size: 32)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !44, file: !23, line: 75, baseType: !11, size: 32, offset: 32)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !44, file: !23, line: 76, baseType: !11, size: 32, offset: 64)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !44, file: !23, line: 77, baseType: !11, size: 32, offset: 96)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "max_lnz_block", scope: !44, file: !23, line: 78, baseType: !11, size: 32, offset: 128)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "max_unz_block", scope: !44, file: !23, line: 79, baseType: !11, size: 32, offset: 160)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "Pnum", scope: !44, file: !23, line: 80, baseType: !10, size: 64, offset: 192)
!53 = !DIDerivedType(tag: DW_TAG_member, name: "Pinv", scope: !44, file: !23, line: 81, baseType: !10, size: 64, offset: 256)
!54 = !DIDerivedType(tag: DW_TAG_member, name: "Lip", scope: !44, file: !23, line: 84, baseType: !10, size: 64, offset: 320)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "Uip", scope: !44, file: !23, line: 85, baseType: !10, size: 64, offset: 384)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "Llen", scope: !44, file: !23, line: 86, baseType: !10, size: 64, offset: 448)
!57 = !DIDerivedType(tag: DW_TAG_member, name: "Ulen", scope: !44, file: !23, line: 87, baseType: !10, size: 64, offset: 512)
!58 = !DIDerivedType(tag: DW_TAG_member, name: "LUbx", scope: !44, file: !23, line: 88, baseType: !59, size: 64, offset: 576)
!59 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !4, size: 64)
!60 = !DIDerivedType(tag: DW_TAG_member, name: "LUsize", scope: !44, file: !23, line: 89, baseType: !61, size: 64, offset: 640)
!61 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !62, size: 64)
!62 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !63, line: 62, baseType: !64)
!63 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!64 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "Udiag", scope: !44, file: !23, line: 90, baseType: !4, size: 64, offset: 704)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "Rs", scope: !44, file: !23, line: 93, baseType: !5, size: 64, offset: 768)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "worksize", scope: !44, file: !23, line: 96, baseType: !62, size: 64, offset: 832)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "Work", scope: !44, file: !23, line: 97, baseType: !4, size: 64, offset: 896)
!69 = !DIDerivedType(tag: DW_TAG_member, name: "Xwork", scope: !44, file: !23, line: 98, baseType: !4, size: 64, offset: 960)
!70 = !DIDerivedType(tag: DW_TAG_member, name: "Iwork", scope: !44, file: !23, line: 99, baseType: !10, size: 64, offset: 1024)
!71 = !DIDerivedType(tag: DW_TAG_member, name: "Offp", scope: !44, file: !23, line: 102, baseType: !10, size: 64, offset: 1088)
!72 = !DIDerivedType(tag: DW_TAG_member, name: "Offi", scope: !44, file: !23, line: 103, baseType: !10, size: 64, offset: 1152)
!73 = !DIDerivedType(tag: DW_TAG_member, name: "Offx", scope: !44, file: !23, line: 104, baseType: !4, size: 64, offset: 1216)
!74 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !44, file: !23, line: 105, baseType: !11, size: 32, offset: 1280)
!75 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !76, size: 64)
!76 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !23, line: 207, baseType: !77)
!77 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !23, line: 137, size: 1280, elements: !78)
!78 = !{!79, !80, !81, !82, !83, !84, !85, !86, !87, !92, !93, !94, !95, !96, !97, !98, !99, !100, !101, !102, !103, !104, !105, !106}
!79 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !77, file: !23, line: 144, baseType: !6, size: 64)
!80 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !77, file: !23, line: 145, baseType: !6, size: 64, offset: 64)
!81 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !77, file: !23, line: 146, baseType: !6, size: 64, offset: 128)
!82 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !77, file: !23, line: 147, baseType: !6, size: 64, offset: 192)
!83 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !77, file: !23, line: 148, baseType: !6, size: 64, offset: 256)
!84 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !77, file: !23, line: 150, baseType: !11, size: 32, offset: 320)
!85 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !77, file: !23, line: 151, baseType: !11, size: 32, offset: 352)
!86 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !77, file: !23, line: 153, baseType: !11, size: 32, offset: 384)
!87 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !77, file: !23, line: 157, baseType: !88, size: 64, offset: 448)
!88 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !89, size: 64)
!89 = !DISubroutineType(types: !90)
!90 = !{!11, !11, !10, !10, !10, !91}
!91 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !77, size: 64)
!92 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !77, file: !23, line: 162, baseType: !4, size: 64, offset: 512)
!93 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !77, file: !23, line: 164, baseType: !11, size: 32, offset: 576)
!94 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !77, file: !23, line: 177, baseType: !11, size: 32, offset: 608)
!95 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !77, file: !23, line: 178, baseType: !11, size: 32, offset: 640)
!96 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !77, file: !23, line: 180, baseType: !11, size: 32, offset: 672)
!97 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !77, file: !23, line: 185, baseType: !11, size: 32, offset: 704)
!98 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !77, file: !23, line: 191, baseType: !11, size: 32, offset: 736)
!99 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !77, file: !23, line: 196, baseType: !11, size: 32, offset: 768)
!100 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !77, file: !23, line: 198, baseType: !6, size: 64, offset: 832)
!101 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !77, file: !23, line: 199, baseType: !6, size: 64, offset: 896)
!102 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !77, file: !23, line: 200, baseType: !6, size: 64, offset: 960)
!103 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !77, file: !23, line: 201, baseType: !6, size: 64, offset: 1024)
!104 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !77, file: !23, line: 202, baseType: !6, size: 64, offset: 1088)
!105 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !77, file: !23, line: 204, baseType: !62, size: 64, offset: 1152)
!106 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !77, file: !23, line: 205, baseType: !62, size: 64, offset: 1216)
!107 = !{!108, !109, !110, !111, !112, !113, !114, !115, !116, !117, !118, !119, !120, !121, !122, !123, !124, !125, !126, !127, !128, !129, !130, !131, !132, !133, !134, !135, !136, !137, !138, !139, !140}
!108 = !DILocalVariable(name: "Ap", arg: 1, scope: !18, file: !1, line: 27, type: !10)
!109 = !DILocalVariable(name: "Ai", arg: 2, scope: !18, file: !1, line: 28, type: !10)
!110 = !DILocalVariable(name: "Ax", arg: 3, scope: !18, file: !1, line: 29, type: !5)
!111 = !DILocalVariable(name: "Symbolic", arg: 4, scope: !18, file: !1, line: 30, type: !21)
!112 = !DILocalVariable(name: "Numeric", arg: 5, scope: !18, file: !1, line: 31, type: !42)
!113 = !DILocalVariable(name: "Common", arg: 6, scope: !18, file: !1, line: 32, type: !75)
!114 = !DILocalVariable(name: "temp", scope: !18, file: !1, line: 35, type: !6)
!115 = !DILocalVariable(name: "max_ai", scope: !18, file: !1, line: 35, type: !6)
!116 = !DILocalVariable(name: "max_ui", scope: !18, file: !1, line: 35, type: !6)
!117 = !DILocalVariable(name: "min_block_rgrowth", scope: !18, file: !1, line: 35, type: !6)
!118 = !DILocalVariable(name: "aik", scope: !18, file: !1, line: 36, type: !6)
!119 = !DILocalVariable(name: "Q", scope: !18, file: !1, line: 37, type: !10)
!120 = !DILocalVariable(name: "Ui", scope: !18, file: !1, line: 37, type: !10)
!121 = !DILocalVariable(name: "Uip", scope: !18, file: !1, line: 37, type: !10)
!122 = !DILocalVariable(name: "Ulen", scope: !18, file: !1, line: 37, type: !10)
!123 = !DILocalVariable(name: "Pinv", scope: !18, file: !1, line: 37, type: !10)
!124 = !DILocalVariable(name: "LU", scope: !18, file: !1, line: 38, type: !7)
!125 = !DILocalVariable(name: "Aentry", scope: !18, file: !1, line: 39, type: !5)
!126 = !DILocalVariable(name: "Ux", scope: !18, file: !1, line: 39, type: !5)
!127 = !DILocalVariable(name: "Ukk", scope: !18, file: !1, line: 39, type: !5)
!128 = !DILocalVariable(name: "Rs", scope: !18, file: !1, line: 40, type: !5)
!129 = !DILocalVariable(name: "i", scope: !18, file: !1, line: 41, type: !11)
!130 = !DILocalVariable(name: "newrow", scope: !18, file: !1, line: 41, type: !11)
!131 = !DILocalVariable(name: "oldrow", scope: !18, file: !1, line: 41, type: !11)
!132 = !DILocalVariable(name: "k1", scope: !18, file: !1, line: 41, type: !11)
!133 = !DILocalVariable(name: "k2", scope: !18, file: !1, line: 41, type: !11)
!134 = !DILocalVariable(name: "nk", scope: !18, file: !1, line: 41, type: !11)
!135 = !DILocalVariable(name: "j", scope: !18, file: !1, line: 41, type: !11)
!136 = !DILocalVariable(name: "oldcol", scope: !18, file: !1, line: 41, type: !11)
!137 = !DILocalVariable(name: "k", scope: !18, file: !1, line: 41, type: !11)
!138 = !DILocalVariable(name: "pend", scope: !18, file: !1, line: 41, type: !11)
!139 = !DILocalVariable(name: "len", scope: !18, file: !1, line: 41, type: !11)
!140 = !DILocalVariable(name: "xp", scope: !141, file: !1, line: 125, type: !7)
!141 = distinct !DILexicalBlock(scope: !142, file: !1, line: 125, column: 13)
!142 = distinct !DILexicalBlock(scope: !143, file: !1, line: 92, column: 9)
!143 = distinct !DILexicalBlock(scope: !144, file: !1, line: 91, column: 9)
!144 = distinct !DILexicalBlock(scope: !145, file: !1, line: 91, column: 9)
!145 = distinct !DILexicalBlock(scope: !146, file: !1, line: 78, column: 5)
!146 = distinct !DILexicalBlock(scope: !147, file: !1, line: 77, column: 5)
!147 = distinct !DILexicalBlock(scope: !18, file: !1, line: 77, column: 5)
!148 = !DILocation(line: 27, column: 10, scope: !18)
!149 = !DILocation(line: 28, column: 10, scope: !18)
!150 = !DILocation(line: 29, column: 13, scope: !18)
!151 = !DILocation(line: 30, column: 19, scope: !18)
!152 = !DILocation(line: 31, column: 18, scope: !18)
!153 = !DILocation(line: 32, column: 17, scope: !18)
!154 = !DILocation(line: 47, column: 16, scope: !155)
!155 = distinct !DILexicalBlock(scope: !18, file: !1, line: 47, column: 9)
!156 = !DILocation(line: 47, column: 9, scope: !18)
!157 = !DILocation(line: 52, column: 18, scope: !158)
!158 = distinct !DILexicalBlock(scope: !18, file: !1, line: 52, column: 9)
!159 = !DILocation(line: 52, column: 32, scope: !158)
!160 = !DILocation(line: 52, column: 26, scope: !158)
!161 = !DILocation(line: 52, column: 46, scope: !158)
!162 = !DILocation(line: 52, column: 60, scope: !158)
!163 = !DILocation(line: 54, column: 17, scope: !164)
!164 = distinct !DILexicalBlock(scope: !158, file: !1, line: 53, column: 5)
!165 = !DILocation(line: 54, column: 24, scope: !164)
!166 = !{!167, !171, i64 76}
!167 = !{!"klu_common_struct", !168, i64 0, !168, i64 8, !168, i64 16, !168, i64 24, !168, i64 32, !171, i64 40, !171, i64 44, !171, i64 48, !172, i64 56, !172, i64 64, !171, i64 72, !171, i64 76, !171, i64 80, !171, i64 84, !171, i64 88, !171, i64 92, !171, i64 96, !168, i64 104, !168, i64 112, !168, i64 120, !168, i64 128, !168, i64 136, !173, i64 144, !173, i64 152}
!168 = !{!"double", !169, i64 0}
!169 = !{!"omnipotent char", !170, i64 0}
!170 = !{!"Simple C/C++ TBAA"}
!171 = !{!"int", !169, i64 0}
!172 = !{!"any pointer", !169, i64 0}
!173 = !{!"long", !169, i64 0}
!174 = !DILocation(line: 55, column: 9, scope: !164)
!175 = !DILocation(line: 58, column: 17, scope: !176)
!176 = distinct !DILexicalBlock(scope: !18, file: !1, line: 58, column: 9)
!177 = !DILocation(line: 58, column: 9, scope: !18)
!178 = !DILocation(line: 61, column: 17, scope: !179)
!179 = distinct !DILexicalBlock(scope: !176, file: !1, line: 59, column: 5)
!180 = !DILocation(line: 61, column: 25, scope: !179)
!181 = !{!167, !168, i64 128}
!182 = !DILocation(line: 62, column: 17, scope: !179)
!183 = !DILocation(line: 62, column: 24, scope: !179)
!184 = !DILocation(line: 63, column: 9, scope: !179)
!185 = !DILocation(line: 65, column: 13, scope: !18)
!186 = !DILocation(line: 65, column: 20, scope: !18)
!187 = !DILocation(line: 39, column: 12, scope: !18)
!188 = !DILocation(line: 72, column: 21, scope: !18)
!189 = !{!190, !172, i64 32}
!190 = !{!"", !171, i64 0, !171, i64 4, !171, i64 8, !171, i64 12, !171, i64 16, !171, i64 20, !172, i64 24, !172, i64 32, !172, i64 40, !172, i64 48, !172, i64 56, !172, i64 64, !172, i64 72, !172, i64 80, !172, i64 88, !172, i64 96, !173, i64 104, !172, i64 112, !172, i64 120, !172, i64 128, !172, i64 136, !172, i64 144, !172, i64 152, !171, i64 160}
!191 = !DILocation(line: 37, column: 32, scope: !18)
!192 = !DILocation(line: 73, column: 19, scope: !18)
!193 = !{!190, !172, i64 96}
!194 = !DILocation(line: 40, column: 13, scope: !18)
!195 = !DILocation(line: 74, column: 19, scope: !18)
!196 = !{!197, !172, i64 56}
!197 = !{!"", !168, i64 0, !168, i64 8, !168, i64 16, !168, i64 24, !172, i64 32, !171, i64 40, !171, i64 44, !172, i64 48, !172, i64 56, !172, i64 64, !171, i64 72, !171, i64 76, !171, i64 80, !171, i64 84, !171, i64 88, !171, i64 92}
!198 = !DILocation(line: 37, column: 10, scope: !18)
!199 = !DILocation(line: 75, column: 13, scope: !18)
!200 = !DILocation(line: 75, column: 21, scope: !18)
!201 = !DILocation(line: 41, column: 9, scope: !18)
!202 = !DILocation(line: 77, column: 32, scope: !146)
!203 = !{!197, !171, i64 76}
!204 = !DILocation(line: 77, column: 20, scope: !146)
!205 = !DILocation(line: 77, column: 5, scope: !147)
!206 = !{!197, !172, i64 64}
!207 = !DILocation(line: 79, column: 14, scope: !145)
!208 = !{!171, !171, i64 0}
!209 = !DILocation(line: 41, column: 28, scope: !18)
!210 = !DILocation(line: 80, column: 27, scope: !145)
!211 = !DILocation(line: 80, column: 14, scope: !145)
!212 = !DILocation(line: 41, column: 32, scope: !18)
!213 = !DILocation(line: 81, column: 17, scope: !145)
!214 = !DILocation(line: 41, column: 36, scope: !18)
!215 = !DILocation(line: 82, column: 16, scope: !216)
!216 = distinct !DILexicalBlock(scope: !145, file: !1, line: 82, column: 13)
!217 = !DILocation(line: 82, column: 13, scope: !145)
!218 = !DILocation(line: 86, column: 32, scope: !145)
!219 = !{!190, !172, i64 72}
!220 = !DILocation(line: 86, column: 23, scope: !145)
!221 = !{!172, !172, i64 0}
!222 = !DILocation(line: 38, column: 11, scope: !18)
!223 = !DILocation(line: 87, column: 24, scope: !145)
!224 = !{!190, !172, i64 48}
!225 = !DILocation(line: 87, column: 28, scope: !145)
!226 = !DILocation(line: 37, column: 19, scope: !18)
!227 = !DILocation(line: 88, column: 25, scope: !145)
!228 = !{!190, !172, i64 64}
!229 = !DILocation(line: 88, column: 30, scope: !145)
!230 = !DILocation(line: 37, column: 25, scope: !18)
!231 = !DILocation(line: 89, column: 35, scope: !145)
!232 = !{!190, !172, i64 88}
!233 = !DILocation(line: 89, column: 42, scope: !145)
!234 = !DILocation(line: 39, column: 26, scope: !18)
!235 = !DILocation(line: 35, column: 34, scope: !18)
!236 = !DILocation(line: 41, column: 40, scope: !18)
!237 = !DILocation(line: 91, column: 24, scope: !143)
!238 = !DILocation(line: 91, column: 9, scope: !144)
!239 = !DILocation(line: 35, column: 18, scope: !18)
!240 = !DILocation(line: 35, column: 26, scope: !18)
!241 = !DILocation(line: 95, column: 26, scope: !142)
!242 = !DILocation(line: 95, column: 22, scope: !142)
!243 = !DILocation(line: 41, column: 43, scope: !18)
!244 = !DILocation(line: 96, column: 31, scope: !142)
!245 = !DILocation(line: 96, column: 20, scope: !142)
!246 = !DILocation(line: 41, column: 54, scope: !18)
!247 = !DILocation(line: 97, column: 22, scope: !248)
!248 = distinct !DILexicalBlock(scope: !142, file: !1, line: 97, column: 13)
!249 = !DILocation(line: 41, column: 51, scope: !18)
!250 = !DILocation(line: 97, column: 38, scope: !251)
!251 = distinct !DILexicalBlock(scope: !248, file: !1, line: 97, column: 13)
!252 = !DILocation(line: 97, column: 13, scope: !248)
!253 = !DILocation(line: 99, column: 26, scope: !254)
!254 = distinct !DILexicalBlock(scope: !251, file: !1, line: 98, column: 13)
!255 = !DILocation(line: 41, column: 20, scope: !18)
!256 = !DILocation(line: 100, column: 26, scope: !254)
!257 = !DILocation(line: 41, column: 12, scope: !18)
!258 = !DILocation(line: 101, column: 28, scope: !259)
!259 = distinct !DILexicalBlock(scope: !254, file: !1, line: 101, column: 21)
!260 = !DILocation(line: 101, column: 21, scope: !254)
!261 = !DILocation(line: 0, scope: !262)
!262 = distinct !DILexicalBlock(scope: !263, file: !1, line: 112, column: 17)
!263 = distinct !DILexicalBlock(scope: !254, file: !1, line: 106, column: 21)
!264 = !{!168, !168, i64 0}
!265 = !DILocation(line: 106, column: 21, scope: !254)
!266 = !DILocation(line: 109, column: 21, scope: !267)
!267 = distinct !DILexicalBlock(scope: !268, file: !1, line: 109, column: 21)
!268 = distinct !DILexicalBlock(scope: !263, file: !1, line: 107, column: 17)
!269 = !DILocation(line: 36, column: 11, scope: !18)
!270 = !DILocation(line: 110, column: 17, scope: !268)
!271 = !DILocation(line: 116, column: 17, scope: !272)
!272 = distinct !DILexicalBlock(scope: !254, file: !1, line: 116, column: 17)
!273 = !DILocation(line: 35, column: 12, scope: !18)
!274 = !DILocation(line: 117, column: 26, scope: !275)
!275 = distinct !DILexicalBlock(scope: !254, file: !1, line: 117, column: 21)
!276 = !DILocation(line: 117, column: 21, scope: !254)
!277 = !DILocation(line: 120, column: 17, scope: !278)
!278 = distinct !DILexicalBlock(scope: !275, file: !1, line: 118, column: 17)
!279 = !DILocation(line: 0, scope: !142)
!280 = !DILocation(line: 97, column: 48, scope: !251)
!281 = distinct !{!281, !252, !282}
!282 = !DILocation(line: 121, column: 13, scope: !248)
!283 = !DILocation(line: 125, column: 13, scope: !141)
!284 = !DILocation(line: 41, column: 60, scope: !18)
!285 = !DILocation(line: 37, column: 14, scope: !18)
!286 = !DILocation(line: 39, column: 21, scope: !18)
!287 = !DILocation(line: 126, column: 28, scope: !288)
!288 = distinct !DILexicalBlock(scope: !289, file: !1, line: 126, column: 13)
!289 = distinct !DILexicalBlock(scope: !142, file: !1, line: 126, column: 13)
!290 = !DILocation(line: 126, column: 13, scope: !289)
!291 = !DILocation(line: 129, column: 17, scope: !292)
!292 = distinct !DILexicalBlock(scope: !293, file: !1, line: 129, column: 17)
!293 = distinct !DILexicalBlock(scope: !288, file: !1, line: 127, column: 13)
!294 = !DILocation(line: 130, column: 26, scope: !295)
!295 = distinct !DILexicalBlock(scope: !293, file: !1, line: 130, column: 21)
!296 = !DILocation(line: 130, column: 21, scope: !293)
!297 = !DILocation(line: 126, column: 37, scope: !288)
!298 = distinct !{!298, !290, !299}
!299 = !DILocation(line: 134, column: 13, scope: !289)
!300 = !DILocation(line: 136, column: 13, scope: !301)
!301 = distinct !DILexicalBlock(scope: !142, file: !1, line: 136, column: 13)
!302 = !DILocation(line: 137, column: 22, scope: !303)
!303 = distinct !DILexicalBlock(scope: !142, file: !1, line: 137, column: 17)
!304 = !DILocation(line: 137, column: 17, scope: !142)
!305 = !DILocation(line: 143, column: 17, scope: !306)
!306 = distinct !DILexicalBlock(scope: !142, file: !1, line: 143, column: 17)
!307 = !DILocation(line: 143, column: 17, scope: !142)
!308 = !DILocation(line: 147, column: 27, scope: !142)
!309 = !DILocation(line: 148, column: 22, scope: !310)
!310 = distinct !DILexicalBlock(scope: !142, file: !1, line: 148, column: 17)
!311 = !DILocation(line: 148, column: 17, scope: !142)
!312 = !DILocation(line: 151, column: 13, scope: !313)
!313 = distinct !DILexicalBlock(scope: !310, file: !1, line: 149, column: 13)
!314 = !DILocation(line: 0, scope: !145)
!315 = !DILocation(line: 91, column: 32, scope: !143)
!316 = distinct !{!316, !238, !317}
!317 = !DILocation(line: 152, column: 9, scope: !144)
!318 = !DILocation(line: 154, column: 41, scope: !319)
!319 = distinct !DILexicalBlock(scope: !145, file: !1, line: 154, column: 13)
!320 = !DILocation(line: 154, column: 31, scope: !319)
!321 = !DILocation(line: 154, column: 13, scope: !145)
!322 = !DILocation(line: 156, column: 29, scope: !323)
!323 = distinct !DILexicalBlock(scope: !319, file: !1, line: 155, column: 9)
!324 = !DILocation(line: 157, column: 9, scope: !323)
!325 = distinct !{!325, !205, !326}
!326 = !DILocation(line: 158, column: 5, scope: !147)
!327 = !DILocation(line: 0, scope: !18)
!328 = !DILocation(line: 160, column: 1, scope: !18)
!329 = distinct !DISubprogram(name: "klu_condest", scope: !1, file: !1, line: 172, type: !330, isLocal: false, isDefinition: true, scopeLine: 180, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !332)
!330 = !DISubroutineType(types: !331)
!331 = !{!11, !10, !5, !21, !42, !75}
!332 = !{!333, !334, !335, !336, !337, !338, !339, !340, !341, !342, !343, !344, !345, !346, !347, !348, !349, !350, !351, !352, !353, !354, !355, !356, !357}
!333 = !DILocalVariable(name: "Ap", arg: 1, scope: !329, file: !1, line: 174, type: !10)
!334 = !DILocalVariable(name: "Ax", arg: 2, scope: !329, file: !1, line: 175, type: !5)
!335 = !DILocalVariable(name: "Symbolic", arg: 3, scope: !329, file: !1, line: 176, type: !21)
!336 = !DILocalVariable(name: "Numeric", arg: 4, scope: !329, file: !1, line: 177, type: !42)
!337 = !DILocalVariable(name: "Common", arg: 5, scope: !329, file: !1, line: 178, type: !75)
!338 = !DILocalVariable(name: "xj", scope: !329, file: !1, line: 181, type: !6)
!339 = !DILocalVariable(name: "Xmax", scope: !329, file: !1, line: 181, type: !6)
!340 = !DILocalVariable(name: "csum", scope: !329, file: !1, line: 181, type: !6)
!341 = !DILocalVariable(name: "anorm", scope: !329, file: !1, line: 181, type: !6)
!342 = !DILocalVariable(name: "ainv_norm", scope: !329, file: !1, line: 181, type: !6)
!343 = !DILocalVariable(name: "est_old", scope: !329, file: !1, line: 181, type: !6)
!344 = !DILocalVariable(name: "est_new", scope: !329, file: !1, line: 181, type: !6)
!345 = !DILocalVariable(name: "abs_value", scope: !329, file: !1, line: 181, type: !6)
!346 = !DILocalVariable(name: "Udiag", scope: !329, file: !1, line: 182, type: !5)
!347 = !DILocalVariable(name: "Aentry", scope: !329, file: !1, line: 182, type: !5)
!348 = !DILocalVariable(name: "X", scope: !329, file: !1, line: 182, type: !5)
!349 = !DILocalVariable(name: "S", scope: !329, file: !1, line: 182, type: !5)
!350 = !DILocalVariable(name: "i", scope: !329, file: !1, line: 183, type: !11)
!351 = !DILocalVariable(name: "j", scope: !329, file: !1, line: 183, type: !11)
!352 = !DILocalVariable(name: "jmax", scope: !329, file: !1, line: 183, type: !11)
!353 = !DILocalVariable(name: "jnew", scope: !329, file: !1, line: 183, type: !11)
!354 = !DILocalVariable(name: "pend", scope: !329, file: !1, line: 183, type: !11)
!355 = !DILocalVariable(name: "n", scope: !329, file: !1, line: 183, type: !11)
!356 = !DILocalVariable(name: "unchanged", scope: !329, file: !1, line: 185, type: !11)
!357 = !DILocalVariable(name: "s", scope: !358, file: !1, line: 301, type: !6)
!358 = distinct !DILexicalBlock(scope: !359, file: !1, line: 300, column: 9)
!359 = distinct !DILexicalBlock(scope: !360, file: !1, line: 299, column: 9)
!360 = distinct !DILexicalBlock(scope: !361, file: !1, line: 299, column: 9)
!361 = distinct !DILexicalBlock(scope: !362, file: !1, line: 273, column: 5)
!362 = distinct !DILexicalBlock(scope: !363, file: !1, line: 272, column: 5)
!363 = distinct !DILexicalBlock(scope: !329, file: !1, line: 272, column: 5)
!364 = !DILocation(line: 174, column: 9, scope: !329)
!365 = !DILocation(line: 175, column: 12, scope: !329)
!366 = !DILocation(line: 176, column: 19, scope: !329)
!367 = !DILocation(line: 177, column: 18, scope: !329)
!368 = !DILocation(line: 178, column: 17, scope: !329)
!369 = !DILocation(line: 192, column: 16, scope: !370)
!370 = distinct !DILexicalBlock(scope: !329, file: !1, line: 192, column: 9)
!371 = !DILocation(line: 192, column: 9, scope: !329)
!372 = !DILocation(line: 196, column: 18, scope: !373)
!373 = distinct !DILexicalBlock(scope: !329, file: !1, line: 196, column: 9)
!374 = !DILocation(line: 196, column: 32, scope: !373)
!375 = !DILocation(line: 196, column: 26, scope: !373)
!376 = !DILocation(line: 196, column: 46, scope: !373)
!377 = !DILocation(line: 198, column: 17, scope: !378)
!378 = distinct !DILexicalBlock(scope: !373, file: !1, line: 197, column: 5)
!379 = !DILocation(line: 198, column: 24, scope: !378)
!380 = !DILocation(line: 199, column: 9, scope: !378)
!381 = !DILocation(line: 181, column: 64, scope: !329)
!382 = !DILocation(line: 202, column: 17, scope: !383)
!383 = distinct !DILexicalBlock(scope: !329, file: !1, line: 202, column: 9)
!384 = !DILocation(line: 202, column: 9, scope: !329)
!385 = !DILocation(line: 205, column: 17, scope: !386)
!386 = distinct !DILexicalBlock(scope: !383, file: !1, line: 203, column: 5)
!387 = !DILocation(line: 205, column: 25, scope: !386)
!388 = !{!167, !168, i64 120}
!389 = !DILocation(line: 206, column: 17, scope: !386)
!390 = !DILocation(line: 206, column: 24, scope: !386)
!391 = !DILocation(line: 207, column: 9, scope: !386)
!392 = !DILocation(line: 209, column: 13, scope: !329)
!393 = !DILocation(line: 209, column: 20, scope: !329)
!394 = !DILocation(line: 215, column: 19, scope: !329)
!395 = !{!197, !171, i64 40}
!396 = !DILocation(line: 183, column: 33, scope: !329)
!397 = !DILocation(line: 216, column: 22, scope: !329)
!398 = !DILocation(line: 182, column: 12, scope: !329)
!399 = !DILocation(line: 183, column: 9, scope: !329)
!400 = !DILocation(line: 222, column: 20, scope: !401)
!401 = distinct !DILexicalBlock(scope: !402, file: !1, line: 222, column: 5)
!402 = distinct !DILexicalBlock(scope: !329, file: !1, line: 222, column: 5)
!403 = !DILocation(line: 222, column: 5, scope: !402)
!404 = distinct !{!404, !403, !405}
!405 = !DILocation(line: 231, column: 5, scope: !402)
!406 = !DILocation(line: 224, column: 9, scope: !407)
!407 = distinct !DILexicalBlock(scope: !408, file: !1, line: 224, column: 9)
!408 = distinct !DILexicalBlock(scope: !401, file: !1, line: 223, column: 5)
!409 = !DILocation(line: 225, column: 13, scope: !410)
!410 = distinct !DILexicalBlock(scope: !408, file: !1, line: 225, column: 13)
!411 = !DILocation(line: 222, column: 27, scope: !401)
!412 = !DILocation(line: 225, column: 13, scope: !408)
!413 = !DILocation(line: 227, column: 33, scope: !414)
!414 = distinct !DILexicalBlock(scope: !410, file: !1, line: 226, column: 9)
!415 = !DILocation(line: 227, column: 21, scope: !414)
!416 = !DILocation(line: 227, column: 29, scope: !414)
!417 = !DILocation(line: 228, column: 28, scope: !414)
!418 = !DILocation(line: 229, column: 13, scope: !414)
!419 = !DILocation(line: 181, column: 28, scope: !329)
!420 = !DILocation(line: 182, column: 20, scope: !329)
!421 = !DILocation(line: 239, column: 20, scope: !422)
!422 = distinct !DILexicalBlock(scope: !423, file: !1, line: 239, column: 5)
!423 = distinct !DILexicalBlock(scope: !329, file: !1, line: 239, column: 5)
!424 = !DILocation(line: 239, column: 5, scope: !423)
!425 = !DILocation(line: 241, column: 22, scope: !426)
!426 = distinct !DILexicalBlock(scope: !422, file: !1, line: 240, column: 5)
!427 = !DILocation(line: 241, column: 16, scope: !426)
!428 = !DILocation(line: 183, column: 27, scope: !329)
!429 = !DILocation(line: 181, column: 22, scope: !329)
!430 = !DILocation(line: 243, column: 18, scope: !431)
!431 = distinct !DILexicalBlock(scope: !426, file: !1, line: 243, column: 9)
!432 = !DILocation(line: 183, column: 12, scope: !329)
!433 = !DILocation(line: 243, column: 29, scope: !434)
!434 = distinct !DILexicalBlock(scope: !431, file: !1, line: 243, column: 9)
!435 = !DILocation(line: 243, column: 9, scope: !431)
!436 = !DILocation(line: 245, column: 13, scope: !437)
!437 = distinct !DILexicalBlock(scope: !438, file: !1, line: 245, column: 13)
!438 = distinct !DILexicalBlock(scope: !434, file: !1, line: 244, column: 9)
!439 = !DILocation(line: 246, column: 18, scope: !438)
!440 = !DILocation(line: 243, column: 39, scope: !434)
!441 = distinct !{!441, !435, !442}
!442 = !DILocation(line: 247, column: 9, scope: !431)
!443 = !DILocation(line: 248, column: 18, scope: !444)
!444 = distinct !DILexicalBlock(scope: !426, file: !1, line: 248, column: 13)
!445 = !DILocation(line: 248, column: 13, scope: !426)
!446 = distinct !{!446, !424, !447}
!447 = !DILocation(line: 252, column: 5, scope: !423)
!448 = !DILocation(line: 259, column: 18, scope: !329)
!449 = !{!190, !172, i64 120}
!450 = !DILocation(line: 182, column: 29, scope: !329)
!451 = !DILocation(line: 260, column: 7, scope: !329)
!452 = !DILocation(line: 261, column: 11, scope: !329)
!453 = !DILocation(line: 182, column: 33, scope: !329)
!454 = !DILocation(line: 263, column: 5, scope: !455)
!455 = distinct !DILexicalBlock(scope: !329, file: !1, line: 263, column: 5)
!456 = !DILocation(line: 263, column: 20, scope: !457)
!457 = distinct !DILexicalBlock(scope: !455, file: !1, line: 263, column: 5)
!458 = !DILocation(line: 265, column: 9, scope: !459)
!459 = distinct !DILexicalBlock(scope: !460, file: !1, line: 265, column: 9)
!460 = distinct !DILexicalBlock(scope: !457, file: !1, line: 264, column: 5)
!461 = !DILocation(line: 266, column: 9, scope: !462)
!462 = distinct !DILexicalBlock(scope: !460, file: !1, line: 266, column: 9)
!463 = !DILocation(line: 267, column: 22, scope: !460)
!464 = !DILocation(line: 263, column: 27, scope: !457)
!465 = distinct !{!465, !454, !466}
!466 = !DILocation(line: 268, column: 5, scope: !455)
!467 = !DILocation(line: 183, column: 15, scope: !329)
!468 = !DILocation(line: 181, column: 35, scope: !329)
!469 = !DILocation(line: 272, column: 5, scope: !363)
!470 = !DILocation(line: 274, column: 15, scope: !471)
!471 = distinct !DILexicalBlock(scope: !361, file: !1, line: 274, column: 13)
!472 = !DILocation(line: 274, column: 13, scope: !361)
!473 = !DILocation(line: 277, column: 13, scope: !474)
!474 = distinct !DILexicalBlock(scope: !475, file: !1, line: 277, column: 13)
!475 = distinct !DILexicalBlock(scope: !471, file: !1, line: 275, column: 9)
!476 = !DILocation(line: 280, column: 17, scope: !477)
!477 = distinct !DILexicalBlock(scope: !478, file: !1, line: 280, column: 17)
!478 = distinct !DILexicalBlock(scope: !479, file: !1, line: 278, column: 13)
!479 = distinct !DILexicalBlock(scope: !474, file: !1, line: 277, column: 13)
!480 = !DILocation(line: 282, column: 13, scope: !475)
!481 = !DILocation(line: 282, column: 29, scope: !475)
!482 = !DILocation(line: 283, column: 9, scope: !475)
!483 = !DILocation(line: 285, column: 9, scope: !361)
!484 = !DILocation(line: 181, column: 46, scope: !329)
!485 = !DILocation(line: 289, column: 9, scope: !486)
!486 = distinct !DILexicalBlock(scope: !361, file: !1, line: 289, column: 9)
!487 = !DILocation(line: 292, column: 13, scope: !488)
!488 = distinct !DILexicalBlock(scope: !489, file: !1, line: 292, column: 13)
!489 = distinct !DILexicalBlock(scope: !490, file: !1, line: 290, column: 9)
!490 = distinct !DILexicalBlock(scope: !486, file: !1, line: 289, column: 9)
!491 = !DILocation(line: 293, column: 23, scope: !489)
!492 = !DILocation(line: 289, column: 31, scope: !490)
!493 = !DILocation(line: 289, column: 24, scope: !490)
!494 = distinct !{!494, !485, !495}
!495 = !DILocation(line: 294, column: 9, scope: !486)
!496 = !DILocation(line: 185, column: 9, scope: !329)
!497 = !DILocation(line: 299, column: 9, scope: !360)
!498 = !DILocation(line: 301, column: 25, scope: !358)
!499 = !DILocation(line: 301, column: 31, scope: !358)
!500 = !DILocation(line: 301, column: 24, scope: !358)
!501 = !DILocation(line: 301, column: 20, scope: !358)
!502 = !DILocation(line: 302, column: 28, scope: !503)
!503 = distinct !DILexicalBlock(scope: !358, file: !1, line: 302, column: 17)
!504 = !DILocation(line: 302, column: 22, scope: !503)
!505 = !DILocation(line: 302, column: 19, scope: !503)
!506 = !DILocation(line: 302, column: 17, scope: !358)
!507 = !DILocation(line: 304, column: 23, scope: !508)
!508 = distinct !DILexicalBlock(scope: !503, file: !1, line: 303, column: 13)
!509 = !DILocation(line: 306, column: 13, scope: !508)
!510 = !DILocation(line: 0, scope: !361)
!511 = !DILocation(line: 299, column: 31, scope: !359)
!512 = !DILocation(line: 299, column: 24, scope: !359)
!513 = distinct !{!513, !497, !514}
!514 = !DILocation(line: 307, column: 9, scope: !360)
!515 = !DILocation(line: 309, column: 19, scope: !516)
!516 = distinct !DILexicalBlock(scope: !361, file: !1, line: 309, column: 13)
!517 = !DILocation(line: 309, column: 33, scope: !516)
!518 = !DILocation(line: 309, column: 47, scope: !516)
!519 = !DILocation(line: 309, column: 44, scope: !516)
!520 = !DILocation(line: 334, column: 9, scope: !521)
!521 = distinct !DILexicalBlock(scope: !361, file: !1, line: 334, column: 9)
!522 = !DILocation(line: 336, column: 21, scope: !523)
!523 = distinct !DILexicalBlock(scope: !524, file: !1, line: 335, column: 9)
!524 = distinct !DILexicalBlock(scope: !521, file: !1, line: 334, column: 9)
!525 = !DILocation(line: 336, column: 13, scope: !523)
!526 = !DILocation(line: 336, column: 19, scope: !523)
!527 = !DILocation(line: 334, column: 31, scope: !524)
!528 = !DILocation(line: 334, column: 24, scope: !524)
!529 = distinct !{!529, !520, !530}
!530 = !DILocation(line: 337, column: 9, scope: !521)
!531 = !DILocation(line: 341, column: 9, scope: !361)
!532 = !DILocation(line: 183, column: 21, scope: !329)
!533 = !DILocation(line: 181, column: 16, scope: !329)
!534 = !DILocation(line: 350, column: 9, scope: !535)
!535 = distinct !DILexicalBlock(scope: !361, file: !1, line: 350, column: 9)
!536 = !DILocation(line: 353, column: 13, scope: !537)
!537 = distinct !DILexicalBlock(scope: !538, file: !1, line: 353, column: 13)
!538 = distinct !DILexicalBlock(scope: !539, file: !1, line: 351, column: 9)
!539 = distinct !DILexicalBlock(scope: !535, file: !1, line: 350, column: 9)
!540 = !DILocation(line: 181, column: 12, scope: !329)
!541 = !DILocation(line: 354, column: 20, scope: !542)
!542 = distinct !DILexicalBlock(scope: !538, file: !1, line: 354, column: 17)
!543 = !DILocation(line: 354, column: 17, scope: !538)
!544 = !DILocation(line: 350, column: 31, scope: !539)
!545 = !DILocation(line: 350, column: 24, scope: !539)
!546 = distinct !{!546, !534, !547}
!547 = !DILocation(line: 359, column: 9, scope: !535)
!548 = !DILocation(line: 360, column: 27, scope: !549)
!549 = distinct !DILexicalBlock(scope: !361, file: !1, line: 360, column: 13)
!550 = !DILocation(line: 360, column: 19, scope: !549)
!551 = !DILocation(line: 272, column: 27, scope: !362)
!552 = !DILocation(line: 272, column: 20, scope: !362)
!553 = distinct !{!553, !469, !554}
!554 = !DILocation(line: 367, column: 5, scope: !363)
!555 = !DILocation(line: 373, column: 20, scope: !556)
!556 = distinct !DILexicalBlock(scope: !557, file: !1, line: 373, column: 5)
!557 = distinct !DILexicalBlock(scope: !329, file: !1, line: 373, column: 5)
!558 = !DILocation(line: 373, column: 5, scope: !557)
!559 = !DILocation(line: 386, column: 5, scope: !329)
!560 = !DILocation(line: 181, column: 55, scope: !329)
!561 = !DILocation(line: 389, column: 5, scope: !562)
!562 = distinct !DILexicalBlock(scope: !329, file: !1, line: 389, column: 5)
!563 = !DILocation(line: 375, column: 9, scope: !564)
!564 = distinct !DILexicalBlock(scope: !565, file: !1, line: 375, column: 9)
!565 = distinct !DILexicalBlock(scope: !556, file: !1, line: 374, column: 5)
!566 = !DILocation(line: 376, column: 15, scope: !567)
!567 = distinct !DILexicalBlock(scope: !565, file: !1, line: 376, column: 13)
!568 = !DILocation(line: 0, scope: !569)
!569 = distinct !DILexicalBlock(scope: !567, file: !1, line: 381, column: 9)
!570 = !DILocation(line: 382, column: 31, scope: !569)
!571 = !DILocation(line: 378, column: 30, scope: !572)
!572 = distinct !DILexicalBlock(scope: !567, file: !1, line: 377, column: 9)
!573 = !DILocation(line: 376, column: 13, scope: !565)
!574 = !DILocation(line: 0, scope: !572)
!575 = !DILocation(line: 373, column: 27, scope: !556)
!576 = distinct !{!576, !558, !577}
!577 = !DILocation(line: 384, column: 5, scope: !557)
!578 = !DILocation(line: 389, column: 20, scope: !579)
!579 = distinct !DILexicalBlock(scope: !562, file: !1, line: 389, column: 5)
!580 = !DILocation(line: 392, column: 9, scope: !581)
!581 = distinct !DILexicalBlock(scope: !582, file: !1, line: 392, column: 9)
!582 = distinct !DILexicalBlock(scope: !579, file: !1, line: 390, column: 5)
!583 = !DILocation(line: 393, column: 17, scope: !582)
!584 = !DILocation(line: 389, column: 27, scope: !579)
!585 = distinct !{!585, !561, !586}
!586 = !DILocation(line: 394, column: 5, scope: !562)
!587 = !DILocation(line: 395, column: 17, scope: !329)
!588 = !DILocation(line: 395, column: 32, scope: !329)
!589 = !DILocation(line: 395, column: 29, scope: !329)
!590 = !DILocation(line: 395, column: 27, scope: !329)
!591 = !DILocation(line: 396, column: 17, scope: !329)
!592 = !DILocation(line: 402, column: 33, scope: !329)
!593 = !DILocation(line: 402, column: 13, scope: !329)
!594 = !DILocation(line: 402, column: 21, scope: !329)
!595 = !DILocation(line: 403, column: 5, scope: !329)
!596 = !DILocation(line: 0, scope: !329)
!597 = !DILocation(line: 404, column: 1, scope: !329)
!598 = distinct !DISubprogram(name: "klu_flops", scope: !1, file: !1, line: 413, type: !599, isLocal: false, isDefinition: true, scopeLine: 419, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !601)
!599 = !DISubroutineType(types: !600)
!600 = !{!11, !21, !42, !75}
!601 = !{!602, !603, !604, !605, !606, !607, !608, !609, !610, !611, !612, !613, !614, !615, !616, !617, !618, !619}
!602 = !DILocalVariable(name: "Symbolic", arg: 1, scope: !598, file: !1, line: 415, type: !21)
!603 = !DILocalVariable(name: "Numeric", arg: 2, scope: !598, file: !1, line: 416, type: !42)
!604 = !DILocalVariable(name: "Common", arg: 3, scope: !598, file: !1, line: 417, type: !75)
!605 = !DILocalVariable(name: "flops", scope: !598, file: !1, line: 420, type: !6)
!606 = !DILocalVariable(name: "R", scope: !598, file: !1, line: 421, type: !10)
!607 = !DILocalVariable(name: "Ui", scope: !598, file: !1, line: 421, type: !10)
!608 = !DILocalVariable(name: "Uip", scope: !598, file: !1, line: 421, type: !10)
!609 = !DILocalVariable(name: "Llen", scope: !598, file: !1, line: 421, type: !10)
!610 = !DILocalVariable(name: "Ulen", scope: !598, file: !1, line: 421, type: !10)
!611 = !DILocalVariable(name: "LUbx", scope: !598, file: !1, line: 422, type: !12)
!612 = !DILocalVariable(name: "LU", scope: !598, file: !1, line: 423, type: !7)
!613 = !DILocalVariable(name: "k", scope: !598, file: !1, line: 424, type: !11)
!614 = !DILocalVariable(name: "ulen", scope: !598, file: !1, line: 424, type: !11)
!615 = !DILocalVariable(name: "p", scope: !598, file: !1, line: 424, type: !11)
!616 = !DILocalVariable(name: "nk", scope: !598, file: !1, line: 424, type: !11)
!617 = !DILocalVariable(name: "block", scope: !598, file: !1, line: 424, type: !11)
!618 = !DILocalVariable(name: "nblocks", scope: !598, file: !1, line: 424, type: !11)
!619 = !DILocalVariable(name: "k1", scope: !598, file: !1, line: 424, type: !11)
!620 = !DILocation(line: 415, column: 19, scope: !598)
!621 = !DILocation(line: 416, column: 18, scope: !598)
!622 = !DILocation(line: 417, column: 17, scope: !598)
!623 = !DILocation(line: 420, column: 12, scope: !598)
!624 = !DILocation(line: 430, column: 16, scope: !625)
!625 = distinct !DILexicalBlock(scope: !598, file: !1, line: 430, column: 9)
!626 = !DILocation(line: 430, column: 9, scope: !598)
!627 = !DILocation(line: 434, column: 13, scope: !598)
!628 = !DILocation(line: 434, column: 19, scope: !598)
!629 = !{!167, !168, i64 104}
!630 = !DILocation(line: 435, column: 17, scope: !631)
!631 = distinct !DILexicalBlock(scope: !598, file: !1, line: 435, column: 9)
!632 = !DILocation(line: 435, column: 37, scope: !631)
!633 = !DILocation(line: 435, column: 25, scope: !631)
!634 = !DILocation(line: 0, scope: !598)
!635 = !DILocation(line: 437, column: 24, scope: !636)
!636 = distinct !DILexicalBlock(scope: !631, file: !1, line: 436, column: 5)
!637 = !DILocation(line: 438, column: 9, scope: !636)
!638 = !DILocation(line: 440, column: 20, scope: !598)
!639 = !DILocation(line: 446, column: 19, scope: !598)
!640 = !DILocation(line: 421, column: 10, scope: !598)
!641 = !DILocation(line: 447, column: 25, scope: !598)
!642 = !DILocation(line: 424, column: 32, scope: !598)
!643 = !DILocation(line: 453, column: 31, scope: !598)
!644 = !DILocation(line: 422, column: 12, scope: !598)
!645 = !DILocation(line: 424, column: 25, scope: !598)
!646 = !DILocation(line: 459, column: 28, scope: !647)
!647 = distinct !DILexicalBlock(scope: !648, file: !1, line: 459, column: 5)
!648 = distinct !DILexicalBlock(scope: !598, file: !1, line: 459, column: 5)
!649 = !DILocation(line: 459, column: 5, scope: !648)
!650 = !DILocation(line: 461, column: 14, scope: !651)
!651 = distinct !DILexicalBlock(scope: !647, file: !1, line: 460, column: 5)
!652 = !DILocation(line: 424, column: 41, scope: !598)
!653 = !DILocation(line: 462, column: 22, scope: !651)
!654 = !DILocation(line: 462, column: 14, scope: !651)
!655 = !DILocation(line: 462, column: 26, scope: !651)
!656 = !DILocation(line: 424, column: 21, scope: !598)
!657 = !DILocation(line: 463, column: 16, scope: !658)
!658 = distinct !DILexicalBlock(scope: !651, file: !1, line: 463, column: 13)
!659 = !DILocation(line: 463, column: 13, scope: !651)
!660 = !DILocation(line: 465, column: 29, scope: !661)
!661 = distinct !DILexicalBlock(scope: !658, file: !1, line: 464, column: 9)
!662 = !{!190, !172, i64 56}
!663 = !DILocation(line: 465, column: 34, scope: !661)
!664 = !DILocation(line: 421, column: 25, scope: !598)
!665 = !DILocation(line: 466, column: 29, scope: !661)
!666 = !DILocation(line: 466, column: 34, scope: !661)
!667 = !DILocation(line: 421, column: 19, scope: !598)
!668 = !DILocation(line: 467, column: 29, scope: !661)
!669 = !DILocation(line: 467, column: 34, scope: !661)
!670 = !DILocation(line: 421, column: 32, scope: !598)
!671 = !DILocation(line: 468, column: 18, scope: !661)
!672 = !DILocation(line: 423, column: 11, scope: !598)
!673 = !DILocation(line: 424, column: 9, scope: !598)
!674 = !DILocation(line: 469, column: 13, scope: !675)
!675 = distinct !DILexicalBlock(scope: !661, file: !1, line: 469, column: 13)
!676 = !DILocation(line: 472, column: 17, scope: !677)
!677 = distinct !DILexicalBlock(scope: !678, file: !1, line: 472, column: 17)
!678 = distinct !DILexicalBlock(scope: !679, file: !1, line: 470, column: 13)
!679 = distinct !DILexicalBlock(scope: !675, file: !1, line: 469, column: 13)
!680 = !DILocation(line: 421, column: 14, scope: !598)
!681 = !DILocation(line: 473, column: 24, scope: !678)
!682 = !DILocation(line: 424, column: 12, scope: !598)
!683 = !DILocation(line: 424, column: 18, scope: !598)
!684 = !DILocation(line: 474, column: 32, scope: !685)
!685 = distinct !DILexicalBlock(scope: !686, file: !1, line: 474, column: 17)
!686 = distinct !DILexicalBlock(scope: !678, file: !1, line: 474, column: 17)
!687 = !DILocation(line: 474, column: 17, scope: !686)
!688 = !DILocation(line: 476, column: 40, scope: !689)
!689 = distinct !DILexicalBlock(scope: !685, file: !1, line: 475, column: 17)
!690 = !DILocation(line: 476, column: 34, scope: !689)
!691 = !DILocation(line: 476, column: 32, scope: !689)
!692 = !DILocation(line: 476, column: 30, scope: !689)
!693 = !DILocation(line: 476, column: 27, scope: !689)
!694 = !DILocation(line: 474, column: 42, scope: !685)
!695 = distinct !{!695, !687, !696}
!696 = !DILocation(line: 477, column: 17, scope: !686)
!697 = !DILocation(line: 479, column: 26, scope: !678)
!698 = !DILocation(line: 479, column: 23, scope: !678)
!699 = !DILocation(line: 469, column: 36, scope: !679)
!700 = !DILocation(line: 469, column: 28, scope: !679)
!701 = distinct !{!701, !674, !702}
!702 = !DILocation(line: 480, column: 13, scope: !675)
!703 = distinct !{!703, !649, !704}
!704 = !DILocation(line: 482, column: 5, scope: !648)
!705 = !DILocation(line: 483, column: 19, scope: !598)
!706 = !DILocation(line: 484, column: 5, scope: !598)
!707 = !DILocation(line: 485, column: 1, scope: !598)
!708 = distinct !DISubprogram(name: "klu_rcond", scope: !1, file: !1, line: 497, type: !599, isLocal: false, isDefinition: true, scopeLine: 503, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !709)
!709 = !{!710, !711, !712, !713, !714, !715, !716, !717, !718}
!710 = !DILocalVariable(name: "Symbolic", arg: 1, scope: !708, file: !1, line: 499, type: !21)
!711 = !DILocalVariable(name: "Numeric", arg: 2, scope: !708, file: !1, line: 500, type: !42)
!712 = !DILocalVariable(name: "Common", arg: 3, scope: !708, file: !1, line: 501, type: !75)
!713 = !DILocalVariable(name: "ukk", scope: !708, file: !1, line: 504, type: !6)
!714 = !DILocalVariable(name: "umin", scope: !708, file: !1, line: 504, type: !6)
!715 = !DILocalVariable(name: "umax", scope: !708, file: !1, line: 504, type: !6)
!716 = !DILocalVariable(name: "Udiag", scope: !708, file: !1, line: 505, type: !5)
!717 = !DILocalVariable(name: "j", scope: !708, file: !1, line: 506, type: !11)
!718 = !DILocalVariable(name: "n", scope: !708, file: !1, line: 506, type: !11)
!719 = !DILocation(line: 499, column: 19, scope: !708)
!720 = !DILocation(line: 500, column: 18, scope: !708)
!721 = !DILocation(line: 501, column: 17, scope: !708)
!722 = !DILocation(line: 504, column: 17, scope: !708)
!723 = !DILocation(line: 504, column: 27, scope: !708)
!724 = !DILocation(line: 512, column: 16, scope: !725)
!725 = distinct !DILexicalBlock(scope: !708, file: !1, line: 512, column: 9)
!726 = !DILocation(line: 512, column: 9, scope: !708)
!727 = !DILocation(line: 516, column: 18, scope: !728)
!728 = distinct !DILexicalBlock(scope: !708, file: !1, line: 516, column: 9)
!729 = !DILocation(line: 516, column: 9, scope: !708)
!730 = !DILocation(line: 518, column: 17, scope: !731)
!731 = distinct !DILexicalBlock(scope: !728, file: !1, line: 517, column: 5)
!732 = !DILocation(line: 518, column: 24, scope: !731)
!733 = !DILocation(line: 519, column: 9, scope: !731)
!734 = !DILocation(line: 521, column: 17, scope: !735)
!735 = distinct !DILexicalBlock(scope: !708, file: !1, line: 521, column: 9)
!736 = !DILocation(line: 521, column: 9, scope: !708)
!737 = !DILocation(line: 523, column: 17, scope: !738)
!738 = distinct !DILexicalBlock(scope: !735, file: !1, line: 522, column: 5)
!739 = !DILocation(line: 523, column: 23, scope: !738)
!740 = !{!167, !168, i64 112}
!741 = !DILocation(line: 524, column: 17, scope: !738)
!742 = !DILocation(line: 524, column: 24, scope: !738)
!743 = !DILocation(line: 525, column: 9, scope: !738)
!744 = !DILocation(line: 527, column: 13, scope: !708)
!745 = !DILocation(line: 527, column: 20, scope: !708)
!746 = !DILocation(line: 533, column: 19, scope: !708)
!747 = !DILocation(line: 506, column: 12, scope: !708)
!748 = !DILocation(line: 534, column: 22, scope: !708)
!749 = !DILocation(line: 505, column: 12, scope: !708)
!750 = !DILocation(line: 506, column: 9, scope: !708)
!751 = !DILocation(line: 535, column: 20, scope: !752)
!752 = distinct !DILexicalBlock(scope: !753, file: !1, line: 535, column: 5)
!753 = distinct !DILexicalBlock(scope: !708, file: !1, line: 535, column: 5)
!754 = !DILocation(line: 535, column: 5, scope: !753)
!755 = !DILocation(line: 538, column: 9, scope: !756)
!756 = distinct !DILexicalBlock(scope: !757, file: !1, line: 538, column: 9)
!757 = distinct !DILexicalBlock(scope: !752, file: !1, line: 536, column: 5)
!758 = !DILocation(line: 504, column: 12, scope: !708)
!759 = !DILocation(line: 539, column: 33, scope: !760)
!760 = distinct !DILexicalBlock(scope: !757, file: !1, line: 539, column: 13)
!761 = !DILocation(line: 542, column: 21, scope: !762)
!762 = distinct !DILexicalBlock(scope: !760, file: !1, line: 540, column: 9)
!763 = !DILocation(line: 542, column: 27, scope: !762)
!764 = !DILocation(line: 543, column: 28, scope: !762)
!765 = !DILocation(line: 544, column: 13, scope: !762)
!766 = !DILocation(line: 546, column: 15, scope: !767)
!767 = distinct !DILexicalBlock(scope: !757, file: !1, line: 546, column: 13)
!768 = !DILocation(line: 546, column: 13, scope: !757)
!769 = !DILocation(line: 555, column: 20, scope: !770)
!770 = distinct !DILexicalBlock(scope: !767, file: !1, line: 553, column: 9)
!771 = !DILocation(line: 556, column: 20, scope: !770)
!772 = !DILocation(line: 0, scope: !770)
!773 = !DILocation(line: 535, column: 27, scope: !752)
!774 = distinct !{!774, !754, !775}
!775 = !DILocation(line: 558, column: 5, scope: !753)
!776 = !DILocation(line: 560, column: 26, scope: !708)
!777 = !DILocation(line: 560, column: 13, scope: !708)
!778 = !DILocation(line: 560, column: 19, scope: !708)
!779 = !DILocation(line: 561, column: 39, scope: !780)
!780 = distinct !DILexicalBlock(scope: !708, file: !1, line: 561, column: 9)
!781 = !DILocation(line: 564, column: 23, scope: !782)
!782 = distinct !DILexicalBlock(scope: !780, file: !1, line: 562, column: 5)
!783 = !DILocation(line: 565, column: 24, scope: !782)
!784 = !DILocation(line: 566, column: 5, scope: !782)
!785 = !DILocation(line: 0, scope: !708)
!786 = !DILocation(line: 568, column: 1, scope: !708)
