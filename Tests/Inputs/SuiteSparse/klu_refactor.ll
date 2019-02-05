; ModuleID = 'klu_refactor.c'
source_filename = "klu_refactor.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_refactor(i32*, i32*, double*, %struct.klu_symbolic* nocapture readonly, %struct.klu_numeric*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %7 = icmp eq %struct.klu_common_struct* %5, null
  br i1 %7, label %874, label %8

; <label>:8:                                      ; preds = %6
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 0, i32* %9, align 4, !tbaa !3
  %10 = icmp eq %struct.klu_numeric* %4, null
  br i1 %10, label %11, label %12

; <label>:11:                                     ; preds = %8
  store i32 -3, i32* %9, align 4, !tbaa !3
  br label %874

; <label>:12:                                     ; preds = %8
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 14
  store i32 -1, i32* %13, align 8, !tbaa !11
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 15
  store i32 -1, i32* %14, align 4, !tbaa !12
  %15 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 5
  %16 = load i32, i32* %15, align 8, !tbaa !13
  %17 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 8
  %18 = load i32*, i32** %17, align 8, !tbaa !15
  %19 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 9
  %20 = load i32*, i32** %19, align 8, !tbaa !16
  %21 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 11
  %22 = load i32, i32* %21, align 4, !tbaa !17
  %23 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 12
  %24 = load i32, i32* %23, align 8, !tbaa !18
  %25 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 6
  %26 = load i32*, i32** %25, align 8, !tbaa !19
  %27 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 22
  %28 = bitcast i8** %27 to double**
  %29 = load double*, double** %28, align 8, !tbaa !21
  %30 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 12
  %31 = bitcast i8*** %30 to double***
  %32 = load double**, double*** %31, align 8, !tbaa !22
  %33 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 7
  %34 = load i32, i32* %33, align 8, !tbaa !23
  %35 = icmp sgt i32 %34, 0
  %36 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 15
  %37 = load double*, double** %36, align 8, !tbaa !24
  br i1 %35, label %38, label %48

; <label>:38:                                     ; preds = %12
  %39 = icmp eq double* %37, null
  br i1 %39, label %40, label %54

; <label>:40:                                     ; preds = %38
  %41 = sext i32 %16 to i64
  %42 = tail call i8* @klu_malloc(i64 %41, i64 8, %struct.klu_common_struct* nonnull %5) #3
  %43 = bitcast double** %36 to i8**
  store i8* %42, i8** %43, align 8, !tbaa !24
  %44 = load i32, i32* %9, align 4, !tbaa !3
  %45 = icmp slt i32 %44, 0
  %46 = bitcast i8* %42 to double*
  br i1 %45, label %47, label %54

; <label>:47:                                     ; preds = %40
  store i32 -2, i32* %9, align 4, !tbaa !3
  br label %874

; <label>:48:                                     ; preds = %12
  %49 = bitcast double* %37 to i8*
  %50 = sext i32 %16 to i64
  %51 = tail call i8* @klu_free(i8* %49, i64 %50, i64 8, %struct.klu_common_struct* nonnull %5) #3
  %52 = bitcast double** %36 to i8**
  store i8* %51, i8** %52, align 8, !tbaa !24
  %53 = bitcast i8* %51 to double*
  br label %54

; <label>:54:                                     ; preds = %38, %40, %48
  %55 = phi double* [ %37, %38 ], [ %46, %40 ], [ %53, %48 ]
  %56 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 7
  %57 = load i32*, i32** %56, align 8, !tbaa !25
  %58 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 18
  %59 = bitcast i8** %58 to double**
  %60 = load double*, double** %59, align 8, !tbaa !26
  %61 = bitcast double* %60 to i8*
  %62 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 12
  store i32 0, i32* %62, align 8, !tbaa !27
  %63 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 14
  %64 = bitcast i8** %63 to double**
  %65 = load double*, double** %64, align 8, !tbaa !28
  %66 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 10
  %67 = load i32, i32* %66, align 8, !tbaa !29
  %68 = icmp sgt i32 %34, -1
  br i1 %68, label %69, label %72

; <label>:69:                                     ; preds = %54
  %70 = tail call i32 @klu_scale(i32 %34, i32 %16, i32* %0, i32* %1, double* %2, double* %55, i32* null, %struct.klu_common_struct* nonnull %5) #3
  %71 = icmp eq i32 %70, 0
  br i1 %71, label %874, label %72

; <label>:72:                                     ; preds = %69, %54
  %73 = icmp sgt i32 %24, 0
  br i1 %73, label %74, label %77

; <label>:74:                                     ; preds = %72
  %75 = zext i32 %24 to i64
  %76 = shl nuw nsw i64 %75, 3
  call void @llvm.memset.p0i8.i64(i8* %61, i8 0, i64 %76, i32 8, i1 false)
  br label %77

; <label>:77:                                     ; preds = %74, %72
  %78 = icmp slt i32 %34, 1
  %79 = icmp sgt i32 %22, 0
  br i1 %78, label %80, label %361

; <label>:80:                                     ; preds = %77
  br i1 %79, label %81, label %641

; <label>:81:                                     ; preds = %80
  %82 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 8
  %83 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 10
  %84 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 9
  %85 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 11
  %86 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 10
  %87 = sext i32 %22 to i64
  br label %88

; <label>:88:                                     ; preds = %81, %358
  %89 = phi i64 [ 0, %81 ], [ %93, %358 ]
  %90 = phi i32 [ 0, %81 ], [ %359, %358 ]
  %91 = getelementptr inbounds i32, i32* %20, i64 %89
  %92 = load i32, i32* %91, align 4, !tbaa !30
  %93 = add nuw nsw i64 %89, 1
  %94 = getelementptr inbounds i32, i32* %20, i64 %93
  %95 = load i32, i32* %94, align 4, !tbaa !30
  %96 = sub nsw i32 %95, %92
  %97 = icmp eq i32 %96, 1
  br i1 %97, label %98, label %144

; <label>:98:                                     ; preds = %88
  %99 = sext i32 %92 to i64
  %100 = getelementptr inbounds i32, i32* %18, i64 %99
  %101 = load i32, i32* %100, align 4, !tbaa !30
  %102 = add nsw i32 %101, 1
  %103 = sext i32 %102 to i64
  %104 = getelementptr inbounds i32, i32* %0, i64 %103
  %105 = load i32, i32* %104, align 4, !tbaa !30
  %106 = sext i32 %101 to i64
  %107 = getelementptr inbounds i32, i32* %0, i64 %106
  %108 = load i32, i32* %107, align 4, !tbaa !30
  %109 = icmp slt i32 %108, %105
  br i1 %109, label %110, label %140

; <label>:110:                                    ; preds = %98
  %111 = sext i32 %108 to i64
  %112 = sext i32 %105 to i64
  br label %113

; <label>:113:                                    ; preds = %135, %110
  %114 = phi i64 [ %111, %110 ], [ %138, %135 ]
  %115 = phi double [ 0.000000e+00, %110 ], [ %137, %135 ]
  %116 = phi i32 [ %90, %110 ], [ %136, %135 ]
  %117 = getelementptr inbounds i32, i32* %1, i64 %114
  %118 = load i32, i32* %117, align 4, !tbaa !30
  %119 = sext i32 %118 to i64
  %120 = getelementptr inbounds i32, i32* %57, i64 %119
  %121 = load i32, i32* %120, align 4, !tbaa !30
  %122 = icmp slt i32 %121, %92
  %123 = icmp slt i32 %116, %67
  %124 = and i1 %123, %122
  %125 = getelementptr inbounds double, double* %2, i64 %114
  br i1 %124, label %126, label %133

; <label>:126:                                    ; preds = %113
  %127 = bitcast double* %125 to i64*
  %128 = load i64, i64* %127, align 8, !tbaa !31
  %129 = sext i32 %116 to i64
  %130 = getelementptr inbounds double, double* %29, i64 %129
  %131 = bitcast double* %130 to i64*
  store i64 %128, i64* %131, align 8, !tbaa !31
  %132 = add nsw i32 %116, 1
  br label %135

; <label>:133:                                    ; preds = %113
  %134 = load double, double* %125, align 8, !tbaa !31
  br label %135

; <label>:135:                                    ; preds = %126, %133
  %136 = phi i32 [ %132, %126 ], [ %116, %133 ]
  %137 = phi double [ %115, %126 ], [ %134, %133 ]
  %138 = add nsw i64 %114, 1
  %139 = icmp eq i64 %138, %112
  br i1 %139, label %140, label %113

; <label>:140:                                    ; preds = %135, %98
  %141 = phi i32 [ %90, %98 ], [ %136, %135 ]
  %142 = phi double [ 0.000000e+00, %98 ], [ %137, %135 ]
  %143 = getelementptr inbounds double, double* %65, i64 %99
  store double %142, double* %143, align 8, !tbaa !31
  br label %358

; <label>:144:                                    ; preds = %88
  %145 = load i32*, i32** %82, align 8, !tbaa !32
  %146 = sext i32 %92 to i64
  %147 = getelementptr inbounds i32, i32* %145, i64 %146
  %148 = load i32*, i32** %83, align 8, !tbaa !33
  %149 = getelementptr inbounds i32, i32* %148, i64 %146
  %150 = load i32*, i32** %84, align 8, !tbaa !34
  %151 = getelementptr inbounds i32, i32* %150, i64 %146
  %152 = load i32*, i32** %85, align 8, !tbaa !35
  %153 = getelementptr inbounds i32, i32* %152, i64 %146
  %154 = getelementptr inbounds double*, double** %32, i64 %89
  %155 = load double*, double** %154, align 8, !tbaa !36
  %156 = icmp sgt i32 %96, 0
  br i1 %156, label %157, label %358

; <label>:157:                                    ; preds = %144
  %158 = sext i32 %96 to i64
  br label %159

; <label>:159:                                    ; preds = %157, %355
  %160 = phi i64 [ 0, %157 ], [ %356, %355 ]
  %161 = phi i32 [ %90, %157 ], [ %205, %355 ]
  %162 = add nsw i64 %160, %146
  %163 = getelementptr inbounds i32, i32* %18, i64 %162
  %164 = load i32, i32* %163, align 4, !tbaa !30
  %165 = add nsw i32 %164, 1
  %166 = sext i32 %165 to i64
  %167 = getelementptr inbounds i32, i32* %0, i64 %166
  %168 = load i32, i32* %167, align 4, !tbaa !30
  %169 = sext i32 %164 to i64
  %170 = getelementptr inbounds i32, i32* %0, i64 %169
  %171 = load i32, i32* %170, align 4, !tbaa !30
  %172 = icmp slt i32 %171, %168
  br i1 %172, label %173, label %204

; <label>:173:                                    ; preds = %159
  %174 = sext i32 %171 to i64
  %175 = sext i32 %168 to i64
  br label %176

; <label>:176:                                    ; preds = %200, %173
  %177 = phi i64 [ %174, %173 ], [ %202, %200 ]
  %178 = phi i32 [ %161, %173 ], [ %201, %200 ]
  %179 = getelementptr inbounds i32, i32* %1, i64 %177
  %180 = load i32, i32* %179, align 4, !tbaa !30
  %181 = sext i32 %180 to i64
  %182 = getelementptr inbounds i32, i32* %57, i64 %181
  %183 = load i32, i32* %182, align 4, !tbaa !30
  %184 = sub nsw i32 %183, %92
  %185 = icmp slt i32 %184, 0
  %186 = icmp slt i32 %178, %67
  %187 = and i1 %186, %185
  %188 = getelementptr inbounds double, double* %2, i64 %177
  %189 = bitcast double* %188 to i64*
  %190 = load i64, i64* %189, align 8, !tbaa !31
  br i1 %187, label %191, label %196

; <label>:191:                                    ; preds = %176
  %192 = sext i32 %178 to i64
  %193 = getelementptr inbounds double, double* %29, i64 %192
  %194 = bitcast double* %193 to i64*
  store i64 %190, i64* %194, align 8, !tbaa !31
  %195 = add nsw i32 %178, 1
  br label %200

; <label>:196:                                    ; preds = %176
  %197 = sext i32 %184 to i64
  %198 = getelementptr inbounds double, double* %60, i64 %197
  %199 = bitcast double* %198 to i64*
  store i64 %190, i64* %199, align 8, !tbaa !31
  br label %200

; <label>:200:                                    ; preds = %191, %196
  %201 = phi i32 [ %195, %191 ], [ %178, %196 ]
  %202 = add nsw i64 %177, 1
  %203 = icmp eq i64 %202, %175
  br i1 %203, label %204, label %176

; <label>:204:                                    ; preds = %200, %159
  %205 = phi i32 [ %161, %159 ], [ %201, %200 ]
  %206 = getelementptr inbounds i32, i32* %151, i64 %160
  %207 = load i32, i32* %206, align 4, !tbaa !30
  %208 = sext i32 %207 to i64
  %209 = getelementptr inbounds double, double* %155, i64 %208
  %210 = getelementptr inbounds i32, i32* %153, i64 %160
  %211 = load i32, i32* %210, align 4, !tbaa !30
  %212 = bitcast double* %209 to i32*
  %213 = sext i32 %211 to i64
  %214 = shl nsw i64 %213, 2
  %215 = add nsw i64 %214, 7
  %216 = lshr i64 %215, 3
  %217 = getelementptr inbounds double, double* %209, i64 %216
  %218 = icmp sgt i32 %211, 0
  br i1 %218, label %219, label %289

; <label>:219:                                    ; preds = %204
  %220 = zext i32 %211 to i64
  br label %221

; <label>:221:                                    ; preds = %286, %219
  %222 = phi i64 [ 0, %219 ], [ %287, %286 ]
  %223 = getelementptr inbounds i32, i32* %212, i64 %222
  %224 = load i32, i32* %223, align 4, !tbaa !30
  %225 = sext i32 %224 to i64
  %226 = getelementptr inbounds double, double* %60, i64 %225
  %227 = load double, double* %226, align 8, !tbaa !31
  store double 0.000000e+00, double* %226, align 8, !tbaa !31
  %228 = getelementptr inbounds double, double* %217, i64 %222
  store double %227, double* %228, align 8, !tbaa !31
  %229 = getelementptr inbounds i32, i32* %147, i64 %225
  %230 = load i32, i32* %229, align 4, !tbaa !30
  %231 = sext i32 %230 to i64
  %232 = getelementptr inbounds double, double* %155, i64 %231
  %233 = getelementptr inbounds i32, i32* %149, i64 %225
  %234 = load i32, i32* %233, align 4, !tbaa !30
  %235 = bitcast double* %232 to i32*
  %236 = sext i32 %234 to i64
  %237 = shl nsw i64 %236, 2
  %238 = add nsw i64 %237, 7
  %239 = lshr i64 %238, 3
  %240 = getelementptr inbounds double, double* %232, i64 %239
  %241 = icmp sgt i32 %234, 0
  br i1 %241, label %242, label %286

; <label>:242:                                    ; preds = %221
  %243 = zext i32 %234 to i64
  %244 = and i64 %243, 1
  %245 = icmp eq i32 %234, 1
  br i1 %245, label %273, label %246

; <label>:246:                                    ; preds = %242
  %247 = sub nsw i64 %243, %244
  br label %248

; <label>:248:                                    ; preds = %248, %246
  %249 = phi i64 [ 0, %246 ], [ %270, %248 ]
  %250 = phi i64 [ %247, %246 ], [ %271, %248 ]
  %251 = getelementptr inbounds double, double* %240, i64 %249
  %252 = load double, double* %251, align 8, !tbaa !31
  %253 = fmul double %227, %252
  %254 = getelementptr inbounds i32, i32* %235, i64 %249
  %255 = load i32, i32* %254, align 4, !tbaa !30
  %256 = sext i32 %255 to i64
  %257 = getelementptr inbounds double, double* %60, i64 %256
  %258 = load double, double* %257, align 8, !tbaa !31
  %259 = fsub double %258, %253
  store double %259, double* %257, align 8, !tbaa !31
  %260 = or i64 %249, 1
  %261 = getelementptr inbounds double, double* %240, i64 %260
  %262 = load double, double* %261, align 8, !tbaa !31
  %263 = fmul double %227, %262
  %264 = getelementptr inbounds i32, i32* %235, i64 %260
  %265 = load i32, i32* %264, align 4, !tbaa !30
  %266 = sext i32 %265 to i64
  %267 = getelementptr inbounds double, double* %60, i64 %266
  %268 = load double, double* %267, align 8, !tbaa !31
  %269 = fsub double %268, %263
  store double %269, double* %267, align 8, !tbaa !31
  %270 = add nuw nsw i64 %249, 2
  %271 = add i64 %250, -2
  %272 = icmp eq i64 %271, 0
  br i1 %272, label %273, label %248

; <label>:273:                                    ; preds = %248, %242
  %274 = phi i64 [ 0, %242 ], [ %270, %248 ]
  %275 = icmp eq i64 %244, 0
  br i1 %275, label %286, label %276

; <label>:276:                                    ; preds = %273
  %277 = getelementptr inbounds double, double* %240, i64 %274
  %278 = load double, double* %277, align 8, !tbaa !31
  %279 = fmul double %227, %278
  %280 = getelementptr inbounds i32, i32* %235, i64 %274
  %281 = load i32, i32* %280, align 4, !tbaa !30
  %282 = sext i32 %281 to i64
  %283 = getelementptr inbounds double, double* %60, i64 %282
  %284 = load double, double* %283, align 8, !tbaa !31
  %285 = fsub double %284, %279
  store double %285, double* %283, align 8, !tbaa !31
  br label %286

; <label>:286:                                    ; preds = %276, %273, %221
  %287 = add nuw nsw i64 %222, 1
  %288 = icmp eq i64 %287, %220
  br i1 %288, label %289, label %221

; <label>:289:                                    ; preds = %286, %204
  %290 = getelementptr inbounds double, double* %60, i64 %160
  %291 = load double, double* %290, align 8, !tbaa !31
  store double 0.000000e+00, double* %290, align 8, !tbaa !31
  %292 = fcmp oeq double %291, 0.000000e+00
  br i1 %292, label %293, label %302

; <label>:293:                                    ; preds = %289
  store i32 1, i32* %9, align 4, !tbaa !3
  %294 = load i32, i32* %13, align 8, !tbaa !11
  %295 = icmp eq i32 %294, -1
  br i1 %295, label %296, label %299

; <label>:296:                                    ; preds = %293
  %297 = trunc i64 %162 to i32
  store i32 %297, i32* %13, align 8, !tbaa !11
  %298 = load i32, i32* %163, align 4, !tbaa !30
  store i32 %298, i32* %14, align 4, !tbaa !12
  br label %299

; <label>:299:                                    ; preds = %296, %293
  %300 = load i32, i32* %86, align 8, !tbaa !37
  %301 = icmp eq i32 %300, 0
  br i1 %301, label %302, label %874

; <label>:302:                                    ; preds = %299, %289
  %303 = getelementptr inbounds double, double* %65, i64 %162
  store double %291, double* %303, align 8, !tbaa !31
  %304 = getelementptr inbounds i32, i32* %147, i64 %160
  %305 = load i32, i32* %304, align 4, !tbaa !30
  %306 = sext i32 %305 to i64
  %307 = getelementptr inbounds double, double* %155, i64 %306
  %308 = getelementptr inbounds i32, i32* %149, i64 %160
  %309 = load i32, i32* %308, align 4, !tbaa !30
  %310 = bitcast double* %307 to i32*
  %311 = sext i32 %309 to i64
  %312 = shl nsw i64 %311, 2
  %313 = add nsw i64 %312, 7
  %314 = lshr i64 %313, 3
  %315 = getelementptr inbounds double, double* %307, i64 %314
  %316 = icmp sgt i32 %309, 0
  br i1 %316, label %317, label %355

; <label>:317:                                    ; preds = %302
  %318 = zext i32 %309 to i64
  %319 = and i64 %318, 1
  %320 = icmp eq i32 %309, 1
  br i1 %320, label %344, label %321

; <label>:321:                                    ; preds = %317
  %322 = sub nsw i64 %318, %319
  br label %323

; <label>:323:                                    ; preds = %323, %321
  %324 = phi i64 [ 0, %321 ], [ %341, %323 ]
  %325 = phi i64 [ %322, %321 ], [ %342, %323 ]
  %326 = getelementptr inbounds i32, i32* %310, i64 %324
  %327 = load i32, i32* %326, align 4, !tbaa !30
  %328 = sext i32 %327 to i64
  %329 = getelementptr inbounds double, double* %60, i64 %328
  %330 = load double, double* %329, align 8, !tbaa !31
  %331 = fdiv double %330, %291
  %332 = getelementptr inbounds double, double* %315, i64 %324
  store double %331, double* %332, align 8, !tbaa !31
  store double 0.000000e+00, double* %329, align 8, !tbaa !31
  %333 = or i64 %324, 1
  %334 = getelementptr inbounds i32, i32* %310, i64 %333
  %335 = load i32, i32* %334, align 4, !tbaa !30
  %336 = sext i32 %335 to i64
  %337 = getelementptr inbounds double, double* %60, i64 %336
  %338 = load double, double* %337, align 8, !tbaa !31
  %339 = fdiv double %338, %291
  %340 = getelementptr inbounds double, double* %315, i64 %333
  store double %339, double* %340, align 8, !tbaa !31
  store double 0.000000e+00, double* %337, align 8, !tbaa !31
  %341 = add nuw nsw i64 %324, 2
  %342 = add i64 %325, -2
  %343 = icmp eq i64 %342, 0
  br i1 %343, label %344, label %323

; <label>:344:                                    ; preds = %323, %317
  %345 = phi i64 [ 0, %317 ], [ %341, %323 ]
  %346 = icmp eq i64 %319, 0
  br i1 %346, label %355, label %347

; <label>:347:                                    ; preds = %344
  %348 = getelementptr inbounds i32, i32* %310, i64 %345
  %349 = load i32, i32* %348, align 4, !tbaa !30
  %350 = sext i32 %349 to i64
  %351 = getelementptr inbounds double, double* %60, i64 %350
  %352 = load double, double* %351, align 8, !tbaa !31
  %353 = fdiv double %352, %291
  %354 = getelementptr inbounds double, double* %315, i64 %345
  store double %353, double* %354, align 8, !tbaa !31
  store double 0.000000e+00, double* %351, align 8, !tbaa !31
  br label %355

; <label>:355:                                    ; preds = %347, %344, %302
  %356 = add nuw nsw i64 %160, 1
  %357 = icmp slt i64 %356, %158
  br i1 %357, label %159, label %358

; <label>:358:                                    ; preds = %355, %144, %140
  %359 = phi i32 [ %141, %140 ], [ %90, %144 ], [ %205, %355 ]
  %360 = icmp slt i64 %93, %87
  br i1 %360, label %88, label %641

; <label>:361:                                    ; preds = %77
  br i1 %79, label %362, label %641

; <label>:362:                                    ; preds = %361
  %363 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 8
  %364 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 10
  %365 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 9
  %366 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 11
  %367 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 10
  %368 = sext i32 %22 to i64
  br label %369

; <label>:369:                                    ; preds = %362, %638
  %370 = phi i64 [ 0, %362 ], [ %374, %638 ]
  %371 = phi i32 [ 0, %362 ], [ %639, %638 ]
  %372 = getelementptr inbounds i32, i32* %20, i64 %370
  %373 = load i32, i32* %372, align 4, !tbaa !30
  %374 = add nuw nsw i64 %370, 1
  %375 = getelementptr inbounds i32, i32* %20, i64 %374
  %376 = load i32, i32* %375, align 4, !tbaa !30
  %377 = sub nsw i32 %376, %373
  %378 = icmp eq i32 %377, 1
  br i1 %378, label %379, label %424

; <label>:379:                                    ; preds = %369
  %380 = sext i32 %373 to i64
  %381 = getelementptr inbounds i32, i32* %18, i64 %380
  %382 = load i32, i32* %381, align 4, !tbaa !30
  %383 = add nsw i32 %382, 1
  %384 = sext i32 %383 to i64
  %385 = getelementptr inbounds i32, i32* %0, i64 %384
  %386 = load i32, i32* %385, align 4, !tbaa !30
  %387 = sext i32 %382 to i64
  %388 = getelementptr inbounds i32, i32* %0, i64 %387
  %389 = load i32, i32* %388, align 4, !tbaa !30
  %390 = icmp slt i32 %389, %386
  br i1 %390, label %391, label %420

; <label>:391:                                    ; preds = %379
  %392 = sext i32 %389 to i64
  %393 = sext i32 %386 to i64
  br label %394

; <label>:394:                                    ; preds = %415, %391
  %395 = phi i64 [ %392, %391 ], [ %418, %415 ]
  %396 = phi double [ 0.000000e+00, %391 ], [ %417, %415 ]
  %397 = phi i32 [ %371, %391 ], [ %416, %415 ]
  %398 = getelementptr inbounds i32, i32* %1, i64 %395
  %399 = load i32, i32* %398, align 4, !tbaa !30
  %400 = sext i32 %399 to i64
  %401 = getelementptr inbounds i32, i32* %57, i64 %400
  %402 = load i32, i32* %401, align 4, !tbaa !30
  %403 = icmp slt i32 %402, %373
  %404 = icmp slt i32 %397, %67
  %405 = and i1 %404, %403
  %406 = getelementptr inbounds double, double* %2, i64 %395
  %407 = load double, double* %406, align 8, !tbaa !31
  %408 = getelementptr inbounds double, double* %55, i64 %400
  %409 = load double, double* %408, align 8, !tbaa !31
  %410 = fdiv double %407, %409
  br i1 %405, label %411, label %415

; <label>:411:                                    ; preds = %394
  %412 = sext i32 %397 to i64
  %413 = getelementptr inbounds double, double* %29, i64 %412
  store double %410, double* %413, align 8, !tbaa !31
  %414 = add nsw i32 %397, 1
  br label %415

; <label>:415:                                    ; preds = %394, %411
  %416 = phi i32 [ %414, %411 ], [ %397, %394 ]
  %417 = phi double [ %396, %411 ], [ %410, %394 ]
  %418 = add nsw i64 %395, 1
  %419 = icmp eq i64 %418, %393
  br i1 %419, label %420, label %394

; <label>:420:                                    ; preds = %415, %379
  %421 = phi i32 [ %371, %379 ], [ %416, %415 ]
  %422 = phi double [ 0.000000e+00, %379 ], [ %417, %415 ]
  %423 = getelementptr inbounds double, double* %65, i64 %380
  store double %422, double* %423, align 8, !tbaa !31
  br label %638

; <label>:424:                                    ; preds = %369
  %425 = load i32*, i32** %363, align 8, !tbaa !32
  %426 = sext i32 %373 to i64
  %427 = getelementptr inbounds i32, i32* %425, i64 %426
  %428 = load i32*, i32** %364, align 8, !tbaa !33
  %429 = getelementptr inbounds i32, i32* %428, i64 %426
  %430 = load i32*, i32** %365, align 8, !tbaa !34
  %431 = getelementptr inbounds i32, i32* %430, i64 %426
  %432 = load i32*, i32** %366, align 8, !tbaa !35
  %433 = getelementptr inbounds i32, i32* %432, i64 %426
  %434 = getelementptr inbounds double*, double** %32, i64 %370
  %435 = load double*, double** %434, align 8, !tbaa !36
  %436 = icmp sgt i32 %377, 0
  br i1 %436, label %437, label %638

; <label>:437:                                    ; preds = %424
  %438 = sext i32 %377 to i64
  br label %439

; <label>:439:                                    ; preds = %437, %635
  %440 = phi i64 [ 0, %437 ], [ %636, %635 ]
  %441 = phi i32 [ %371, %437 ], [ %485, %635 ]
  %442 = add nsw i64 %440, %426
  %443 = getelementptr inbounds i32, i32* %18, i64 %442
  %444 = load i32, i32* %443, align 4, !tbaa !30
  %445 = add nsw i32 %444, 1
  %446 = sext i32 %445 to i64
  %447 = getelementptr inbounds i32, i32* %0, i64 %446
  %448 = load i32, i32* %447, align 4, !tbaa !30
  %449 = sext i32 %444 to i64
  %450 = getelementptr inbounds i32, i32* %0, i64 %449
  %451 = load i32, i32* %450, align 4, !tbaa !30
  %452 = icmp slt i32 %451, %448
  br i1 %452, label %453, label %484

; <label>:453:                                    ; preds = %439
  %454 = sext i32 %451 to i64
  %455 = sext i32 %448 to i64
  br label %456

; <label>:456:                                    ; preds = %480, %453
  %457 = phi i64 [ %454, %453 ], [ %482, %480 ]
  %458 = phi i32 [ %441, %453 ], [ %481, %480 ]
  %459 = getelementptr inbounds i32, i32* %1, i64 %457
  %460 = load i32, i32* %459, align 4, !tbaa !30
  %461 = sext i32 %460 to i64
  %462 = getelementptr inbounds i32, i32* %57, i64 %461
  %463 = load i32, i32* %462, align 4, !tbaa !30
  %464 = sub nsw i32 %463, %373
  %465 = icmp slt i32 %464, 0
  %466 = icmp slt i32 %458, %67
  %467 = and i1 %466, %465
  %468 = getelementptr inbounds double, double* %2, i64 %457
  %469 = load double, double* %468, align 8, !tbaa !31
  %470 = getelementptr inbounds double, double* %55, i64 %461
  %471 = load double, double* %470, align 8, !tbaa !31
  %472 = fdiv double %469, %471
  br i1 %467, label %473, label %477

; <label>:473:                                    ; preds = %456
  %474 = sext i32 %458 to i64
  %475 = getelementptr inbounds double, double* %29, i64 %474
  store double %472, double* %475, align 8, !tbaa !31
  %476 = add nsw i32 %458, 1
  br label %480

; <label>:477:                                    ; preds = %456
  %478 = sext i32 %464 to i64
  %479 = getelementptr inbounds double, double* %60, i64 %478
  store double %472, double* %479, align 8, !tbaa !31
  br label %480

; <label>:480:                                    ; preds = %473, %477
  %481 = phi i32 [ %476, %473 ], [ %458, %477 ]
  %482 = add nsw i64 %457, 1
  %483 = icmp eq i64 %482, %455
  br i1 %483, label %484, label %456

; <label>:484:                                    ; preds = %480, %439
  %485 = phi i32 [ %441, %439 ], [ %481, %480 ]
  %486 = getelementptr inbounds i32, i32* %431, i64 %440
  %487 = load i32, i32* %486, align 4, !tbaa !30
  %488 = sext i32 %487 to i64
  %489 = getelementptr inbounds double, double* %435, i64 %488
  %490 = getelementptr inbounds i32, i32* %433, i64 %440
  %491 = load i32, i32* %490, align 4, !tbaa !30
  %492 = bitcast double* %489 to i32*
  %493 = sext i32 %491 to i64
  %494 = shl nsw i64 %493, 2
  %495 = add nsw i64 %494, 7
  %496 = lshr i64 %495, 3
  %497 = getelementptr inbounds double, double* %489, i64 %496
  %498 = icmp sgt i32 %491, 0
  br i1 %498, label %499, label %569

; <label>:499:                                    ; preds = %484
  %500 = zext i32 %491 to i64
  br label %501

; <label>:501:                                    ; preds = %566, %499
  %502 = phi i64 [ 0, %499 ], [ %567, %566 ]
  %503 = getelementptr inbounds i32, i32* %492, i64 %502
  %504 = load i32, i32* %503, align 4, !tbaa !30
  %505 = sext i32 %504 to i64
  %506 = getelementptr inbounds double, double* %60, i64 %505
  %507 = load double, double* %506, align 8, !tbaa !31
  store double 0.000000e+00, double* %506, align 8, !tbaa !31
  %508 = getelementptr inbounds double, double* %497, i64 %502
  store double %507, double* %508, align 8, !tbaa !31
  %509 = getelementptr inbounds i32, i32* %427, i64 %505
  %510 = load i32, i32* %509, align 4, !tbaa !30
  %511 = sext i32 %510 to i64
  %512 = getelementptr inbounds double, double* %435, i64 %511
  %513 = getelementptr inbounds i32, i32* %429, i64 %505
  %514 = load i32, i32* %513, align 4, !tbaa !30
  %515 = bitcast double* %512 to i32*
  %516 = sext i32 %514 to i64
  %517 = shl nsw i64 %516, 2
  %518 = add nsw i64 %517, 7
  %519 = lshr i64 %518, 3
  %520 = getelementptr inbounds double, double* %512, i64 %519
  %521 = icmp sgt i32 %514, 0
  br i1 %521, label %522, label %566

; <label>:522:                                    ; preds = %501
  %523 = zext i32 %514 to i64
  %524 = and i64 %523, 1
  %525 = icmp eq i32 %514, 1
  br i1 %525, label %553, label %526

; <label>:526:                                    ; preds = %522
  %527 = sub nsw i64 %523, %524
  br label %528

; <label>:528:                                    ; preds = %528, %526
  %529 = phi i64 [ 0, %526 ], [ %550, %528 ]
  %530 = phi i64 [ %527, %526 ], [ %551, %528 ]
  %531 = getelementptr inbounds double, double* %520, i64 %529
  %532 = load double, double* %531, align 8, !tbaa !31
  %533 = fmul double %507, %532
  %534 = getelementptr inbounds i32, i32* %515, i64 %529
  %535 = load i32, i32* %534, align 4, !tbaa !30
  %536 = sext i32 %535 to i64
  %537 = getelementptr inbounds double, double* %60, i64 %536
  %538 = load double, double* %537, align 8, !tbaa !31
  %539 = fsub double %538, %533
  store double %539, double* %537, align 8, !tbaa !31
  %540 = or i64 %529, 1
  %541 = getelementptr inbounds double, double* %520, i64 %540
  %542 = load double, double* %541, align 8, !tbaa !31
  %543 = fmul double %507, %542
  %544 = getelementptr inbounds i32, i32* %515, i64 %540
  %545 = load i32, i32* %544, align 4, !tbaa !30
  %546 = sext i32 %545 to i64
  %547 = getelementptr inbounds double, double* %60, i64 %546
  %548 = load double, double* %547, align 8, !tbaa !31
  %549 = fsub double %548, %543
  store double %549, double* %547, align 8, !tbaa !31
  %550 = add nuw nsw i64 %529, 2
  %551 = add i64 %530, -2
  %552 = icmp eq i64 %551, 0
  br i1 %552, label %553, label %528

; <label>:553:                                    ; preds = %528, %522
  %554 = phi i64 [ 0, %522 ], [ %550, %528 ]
  %555 = icmp eq i64 %524, 0
  br i1 %555, label %566, label %556

; <label>:556:                                    ; preds = %553
  %557 = getelementptr inbounds double, double* %520, i64 %554
  %558 = load double, double* %557, align 8, !tbaa !31
  %559 = fmul double %507, %558
  %560 = getelementptr inbounds i32, i32* %515, i64 %554
  %561 = load i32, i32* %560, align 4, !tbaa !30
  %562 = sext i32 %561 to i64
  %563 = getelementptr inbounds double, double* %60, i64 %562
  %564 = load double, double* %563, align 8, !tbaa !31
  %565 = fsub double %564, %559
  store double %565, double* %563, align 8, !tbaa !31
  br label %566

; <label>:566:                                    ; preds = %556, %553, %501
  %567 = add nuw nsw i64 %502, 1
  %568 = icmp eq i64 %567, %500
  br i1 %568, label %569, label %501

; <label>:569:                                    ; preds = %566, %484
  %570 = getelementptr inbounds double, double* %60, i64 %440
  %571 = load double, double* %570, align 8, !tbaa !31
  store double 0.000000e+00, double* %570, align 8, !tbaa !31
  %572 = fcmp oeq double %571, 0.000000e+00
  br i1 %572, label %573, label %582

; <label>:573:                                    ; preds = %569
  store i32 1, i32* %9, align 4, !tbaa !3
  %574 = load i32, i32* %13, align 8, !tbaa !11
  %575 = icmp eq i32 %574, -1
  br i1 %575, label %576, label %579

; <label>:576:                                    ; preds = %573
  %577 = trunc i64 %442 to i32
  store i32 %577, i32* %13, align 8, !tbaa !11
  %578 = load i32, i32* %443, align 4, !tbaa !30
  store i32 %578, i32* %14, align 4, !tbaa !12
  br label %579

; <label>:579:                                    ; preds = %576, %573
  %580 = load i32, i32* %367, align 8, !tbaa !37
  %581 = icmp eq i32 %580, 0
  br i1 %581, label %582, label %874

; <label>:582:                                    ; preds = %579, %569
  %583 = getelementptr inbounds double, double* %65, i64 %442
  store double %571, double* %583, align 8, !tbaa !31
  %584 = getelementptr inbounds i32, i32* %427, i64 %440
  %585 = load i32, i32* %584, align 4, !tbaa !30
  %586 = sext i32 %585 to i64
  %587 = getelementptr inbounds double, double* %435, i64 %586
  %588 = getelementptr inbounds i32, i32* %429, i64 %440
  %589 = load i32, i32* %588, align 4, !tbaa !30
  %590 = bitcast double* %587 to i32*
  %591 = sext i32 %589 to i64
  %592 = shl nsw i64 %591, 2
  %593 = add nsw i64 %592, 7
  %594 = lshr i64 %593, 3
  %595 = getelementptr inbounds double, double* %587, i64 %594
  %596 = icmp sgt i32 %589, 0
  br i1 %596, label %597, label %635

; <label>:597:                                    ; preds = %582
  %598 = zext i32 %589 to i64
  %599 = and i64 %598, 1
  %600 = icmp eq i32 %589, 1
  br i1 %600, label %624, label %601

; <label>:601:                                    ; preds = %597
  %602 = sub nsw i64 %598, %599
  br label %603

; <label>:603:                                    ; preds = %603, %601
  %604 = phi i64 [ 0, %601 ], [ %621, %603 ]
  %605 = phi i64 [ %602, %601 ], [ %622, %603 ]
  %606 = getelementptr inbounds i32, i32* %590, i64 %604
  %607 = load i32, i32* %606, align 4, !tbaa !30
  %608 = sext i32 %607 to i64
  %609 = getelementptr inbounds double, double* %60, i64 %608
  %610 = load double, double* %609, align 8, !tbaa !31
  %611 = fdiv double %610, %571
  %612 = getelementptr inbounds double, double* %595, i64 %604
  store double %611, double* %612, align 8, !tbaa !31
  store double 0.000000e+00, double* %609, align 8, !tbaa !31
  %613 = or i64 %604, 1
  %614 = getelementptr inbounds i32, i32* %590, i64 %613
  %615 = load i32, i32* %614, align 4, !tbaa !30
  %616 = sext i32 %615 to i64
  %617 = getelementptr inbounds double, double* %60, i64 %616
  %618 = load double, double* %617, align 8, !tbaa !31
  %619 = fdiv double %618, %571
  %620 = getelementptr inbounds double, double* %595, i64 %613
  store double %619, double* %620, align 8, !tbaa !31
  store double 0.000000e+00, double* %617, align 8, !tbaa !31
  %621 = add nuw nsw i64 %604, 2
  %622 = add i64 %605, -2
  %623 = icmp eq i64 %622, 0
  br i1 %623, label %624, label %603

; <label>:624:                                    ; preds = %603, %597
  %625 = phi i64 [ 0, %597 ], [ %621, %603 ]
  %626 = icmp eq i64 %599, 0
  br i1 %626, label %635, label %627

; <label>:627:                                    ; preds = %624
  %628 = getelementptr inbounds i32, i32* %590, i64 %625
  %629 = load i32, i32* %628, align 4, !tbaa !30
  %630 = sext i32 %629 to i64
  %631 = getelementptr inbounds double, double* %60, i64 %630
  %632 = load double, double* %631, align 8, !tbaa !31
  %633 = fdiv double %632, %571
  %634 = getelementptr inbounds double, double* %595, i64 %625
  store double %633, double* %634, align 8, !tbaa !31
  store double 0.000000e+00, double* %631, align 8, !tbaa !31
  br label %635

; <label>:635:                                    ; preds = %627, %624, %582
  %636 = add nuw nsw i64 %440, 1
  %637 = icmp slt i64 %636, %438
  br i1 %637, label %439, label %638

; <label>:638:                                    ; preds = %635, %424, %420
  %639 = phi i32 [ %421, %420 ], [ %371, %424 ], [ %485, %635 ]
  %640 = icmp slt i64 %374, %368
  br i1 %640, label %369, label %641

; <label>:641:                                    ; preds = %638, %358, %361, %80
  br i1 %35, label %642, label %874

; <label>:642:                                    ; preds = %641
  %643 = icmp sgt i32 %16, 0
  br i1 %643, label %644, label %874

; <label>:644:                                    ; preds = %642
  %645 = zext i32 %16 to i64
  %646 = add nsw i64 %645, -1
  %647 = and i64 %645, 3
  %648 = icmp ult i64 %646, 3
  br i1 %648, label %692, label %649

; <label>:649:                                    ; preds = %644
  %650 = sub nsw i64 %645, %647
  br label %651

; <label>:651:                                    ; preds = %651, %649
  %652 = phi i64 [ 0, %649 ], [ %689, %651 ]
  %653 = phi i64 [ %650, %649 ], [ %690, %651 ]
  %654 = getelementptr inbounds i32, i32* %26, i64 %652
  %655 = load i32, i32* %654, align 4, !tbaa !30
  %656 = sext i32 %655 to i64
  %657 = getelementptr inbounds double, double* %55, i64 %656
  %658 = bitcast double* %657 to i64*
  %659 = load i64, i64* %658, align 8, !tbaa !31
  %660 = getelementptr inbounds double, double* %60, i64 %652
  %661 = bitcast double* %660 to i64*
  store i64 %659, i64* %661, align 8, !tbaa !31
  %662 = or i64 %652, 1
  %663 = getelementptr inbounds i32, i32* %26, i64 %662
  %664 = load i32, i32* %663, align 4, !tbaa !30
  %665 = sext i32 %664 to i64
  %666 = getelementptr inbounds double, double* %55, i64 %665
  %667 = bitcast double* %666 to i64*
  %668 = load i64, i64* %667, align 8, !tbaa !31
  %669 = getelementptr inbounds double, double* %60, i64 %662
  %670 = bitcast double* %669 to i64*
  store i64 %668, i64* %670, align 8, !tbaa !31
  %671 = or i64 %652, 2
  %672 = getelementptr inbounds i32, i32* %26, i64 %671
  %673 = load i32, i32* %672, align 4, !tbaa !30
  %674 = sext i32 %673 to i64
  %675 = getelementptr inbounds double, double* %55, i64 %674
  %676 = bitcast double* %675 to i64*
  %677 = load i64, i64* %676, align 8, !tbaa !31
  %678 = getelementptr inbounds double, double* %60, i64 %671
  %679 = bitcast double* %678 to i64*
  store i64 %677, i64* %679, align 8, !tbaa !31
  %680 = or i64 %652, 3
  %681 = getelementptr inbounds i32, i32* %26, i64 %680
  %682 = load i32, i32* %681, align 4, !tbaa !30
  %683 = sext i32 %682 to i64
  %684 = getelementptr inbounds double, double* %55, i64 %683
  %685 = bitcast double* %684 to i64*
  %686 = load i64, i64* %685, align 8, !tbaa !31
  %687 = getelementptr inbounds double, double* %60, i64 %680
  %688 = bitcast double* %687 to i64*
  store i64 %686, i64* %688, align 8, !tbaa !31
  %689 = add nuw nsw i64 %652, 4
  %690 = add i64 %653, -4
  %691 = icmp eq i64 %690, 0
  br i1 %691, label %692, label %651

; <label>:692:                                    ; preds = %651, %644
  %693 = phi i64 [ 0, %644 ], [ %689, %651 ]
  %694 = icmp eq i64 %647, 0
  br i1 %694, label %710, label %695

; <label>:695:                                    ; preds = %692
  br label %696

; <label>:696:                                    ; preds = %696, %695
  %697 = phi i64 [ %693, %695 ], [ %707, %696 ]
  %698 = phi i64 [ %647, %695 ], [ %708, %696 ]
  %699 = getelementptr inbounds i32, i32* %26, i64 %697
  %700 = load i32, i32* %699, align 4, !tbaa !30
  %701 = sext i32 %700 to i64
  %702 = getelementptr inbounds double, double* %55, i64 %701
  %703 = bitcast double* %702 to i64*
  %704 = load i64, i64* %703, align 8, !tbaa !31
  %705 = getelementptr inbounds double, double* %60, i64 %697
  %706 = bitcast double* %705 to i64*
  store i64 %704, i64* %706, align 8, !tbaa !31
  %707 = add nuw nsw i64 %697, 1
  %708 = add i64 %698, -1
  %709 = icmp eq i64 %708, 0
  br i1 %709, label %710, label %696, !llvm.loop !38

; <label>:710:                                    ; preds = %696, %692
  br i1 %643, label %711, label %874

; <label>:711:                                    ; preds = %710
  %712 = zext i32 %16 to i64
  %713 = icmp ult i32 %16, 4
  br i1 %713, label %800, label %714

; <label>:714:                                    ; preds = %711
  %715 = getelementptr double, double* %55, i64 %645
  %716 = getelementptr double, double* %60, i64 %645
  %717 = icmp ult double* %55, %716
  %718 = icmp ult double* %60, %715
  %719 = and i1 %717, %718
  br i1 %719, label %800, label %720

; <label>:720:                                    ; preds = %714
  %721 = and i64 %645, 4294967292
  %722 = add nsw i64 %721, -4
  %723 = lshr exact i64 %722, 2
  %724 = add nuw nsw i64 %723, 1
  %725 = and i64 %724, 3
  %726 = icmp ult i64 %722, 12
  br i1 %726, label %778, label %727

; <label>:727:                                    ; preds = %720
  %728 = sub nsw i64 %724, %725
  br label %729

; <label>:729:                                    ; preds = %729, %727
  %730 = phi i64 [ 0, %727 ], [ %775, %729 ]
  %731 = phi i64 [ %728, %727 ], [ %776, %729 ]
  %732 = getelementptr inbounds double, double* %60, i64 %730
  %733 = bitcast double* %732 to <2 x i64>*
  %734 = load <2 x i64>, <2 x i64>* %733, align 8, !tbaa !31, !alias.scope !40
  %735 = getelementptr double, double* %732, i64 2
  %736 = bitcast double* %735 to <2 x i64>*
  %737 = load <2 x i64>, <2 x i64>* %736, align 8, !tbaa !31, !alias.scope !40
  %738 = getelementptr inbounds double, double* %55, i64 %730
  %739 = bitcast double* %738 to <2 x i64>*
  store <2 x i64> %734, <2 x i64>* %739, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %740 = getelementptr double, double* %738, i64 2
  %741 = bitcast double* %740 to <2 x i64>*
  store <2 x i64> %737, <2 x i64>* %741, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %742 = or i64 %730, 4
  %743 = getelementptr inbounds double, double* %60, i64 %742
  %744 = bitcast double* %743 to <2 x i64>*
  %745 = load <2 x i64>, <2 x i64>* %744, align 8, !tbaa !31, !alias.scope !40
  %746 = getelementptr double, double* %743, i64 2
  %747 = bitcast double* %746 to <2 x i64>*
  %748 = load <2 x i64>, <2 x i64>* %747, align 8, !tbaa !31, !alias.scope !40
  %749 = getelementptr inbounds double, double* %55, i64 %742
  %750 = bitcast double* %749 to <2 x i64>*
  store <2 x i64> %745, <2 x i64>* %750, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %751 = getelementptr double, double* %749, i64 2
  %752 = bitcast double* %751 to <2 x i64>*
  store <2 x i64> %748, <2 x i64>* %752, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %753 = or i64 %730, 8
  %754 = getelementptr inbounds double, double* %60, i64 %753
  %755 = bitcast double* %754 to <2 x i64>*
  %756 = load <2 x i64>, <2 x i64>* %755, align 8, !tbaa !31, !alias.scope !40
  %757 = getelementptr double, double* %754, i64 2
  %758 = bitcast double* %757 to <2 x i64>*
  %759 = load <2 x i64>, <2 x i64>* %758, align 8, !tbaa !31, !alias.scope !40
  %760 = getelementptr inbounds double, double* %55, i64 %753
  %761 = bitcast double* %760 to <2 x i64>*
  store <2 x i64> %756, <2 x i64>* %761, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %762 = getelementptr double, double* %760, i64 2
  %763 = bitcast double* %762 to <2 x i64>*
  store <2 x i64> %759, <2 x i64>* %763, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %764 = or i64 %730, 12
  %765 = getelementptr inbounds double, double* %60, i64 %764
  %766 = bitcast double* %765 to <2 x i64>*
  %767 = load <2 x i64>, <2 x i64>* %766, align 8, !tbaa !31, !alias.scope !40
  %768 = getelementptr double, double* %765, i64 2
  %769 = bitcast double* %768 to <2 x i64>*
  %770 = load <2 x i64>, <2 x i64>* %769, align 8, !tbaa !31, !alias.scope !40
  %771 = getelementptr inbounds double, double* %55, i64 %764
  %772 = bitcast double* %771 to <2 x i64>*
  store <2 x i64> %767, <2 x i64>* %772, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %773 = getelementptr double, double* %771, i64 2
  %774 = bitcast double* %773 to <2 x i64>*
  store <2 x i64> %770, <2 x i64>* %774, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %775 = add i64 %730, 16
  %776 = add i64 %731, -4
  %777 = icmp eq i64 %776, 0
  br i1 %777, label %778, label %729, !llvm.loop !45

; <label>:778:                                    ; preds = %729, %720
  %779 = phi i64 [ 0, %720 ], [ %775, %729 ]
  %780 = icmp eq i64 %725, 0
  br i1 %780, label %798, label %781

; <label>:781:                                    ; preds = %778
  br label %782

; <label>:782:                                    ; preds = %782, %781
  %783 = phi i64 [ %779, %781 ], [ %795, %782 ]
  %784 = phi i64 [ %725, %781 ], [ %796, %782 ]
  %785 = getelementptr inbounds double, double* %60, i64 %783
  %786 = bitcast double* %785 to <2 x i64>*
  %787 = load <2 x i64>, <2 x i64>* %786, align 8, !tbaa !31, !alias.scope !40
  %788 = getelementptr double, double* %785, i64 2
  %789 = bitcast double* %788 to <2 x i64>*
  %790 = load <2 x i64>, <2 x i64>* %789, align 8, !tbaa !31, !alias.scope !40
  %791 = getelementptr inbounds double, double* %55, i64 %783
  %792 = bitcast double* %791 to <2 x i64>*
  store <2 x i64> %787, <2 x i64>* %792, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %793 = getelementptr double, double* %791, i64 2
  %794 = bitcast double* %793 to <2 x i64>*
  store <2 x i64> %790, <2 x i64>* %794, align 8, !tbaa !31, !alias.scope !43, !noalias !40
  %795 = add i64 %783, 4
  %796 = add i64 %784, -1
  %797 = icmp eq i64 %796, 0
  br i1 %797, label %798, label %782, !llvm.loop !47

; <label>:798:                                    ; preds = %782, %778
  %799 = icmp eq i64 %721, %645
  br i1 %799, label %874, label %800

; <label>:800:                                    ; preds = %798, %714, %711
  %801 = phi i64 [ 0, %714 ], [ 0, %711 ], [ %721, %798 ]
  %802 = sub nsw i64 %712, %801
  %803 = add nsw i64 %712, -1
  %804 = sub nsw i64 %803, %801
  %805 = and i64 %802, 7
  %806 = icmp eq i64 %805, 0
  br i1 %806, label %819, label %807

; <label>:807:                                    ; preds = %800
  br label %808

; <label>:808:                                    ; preds = %808, %807
  %809 = phi i64 [ %816, %808 ], [ %801, %807 ]
  %810 = phi i64 [ %817, %808 ], [ %805, %807 ]
  %811 = getelementptr inbounds double, double* %60, i64 %809
  %812 = bitcast double* %811 to i64*
  %813 = load i64, i64* %812, align 8, !tbaa !31
  %814 = getelementptr inbounds double, double* %55, i64 %809
  %815 = bitcast double* %814 to i64*
  store i64 %813, i64* %815, align 8, !tbaa !31
  %816 = add nuw nsw i64 %809, 1
  %817 = add i64 %810, -1
  %818 = icmp eq i64 %817, 0
  br i1 %818, label %819, label %808, !llvm.loop !48

; <label>:819:                                    ; preds = %808, %800
  %820 = phi i64 [ %801, %800 ], [ %816, %808 ]
  %821 = icmp ult i64 %804, 7
  br i1 %821, label %874, label %822

; <label>:822:                                    ; preds = %819
  br label %823

; <label>:823:                                    ; preds = %823, %822
  %824 = phi i64 [ %820, %822 ], [ %872, %823 ]
  %825 = getelementptr inbounds double, double* %60, i64 %824
  %826 = bitcast double* %825 to i64*
  %827 = load i64, i64* %826, align 8, !tbaa !31
  %828 = getelementptr inbounds double, double* %55, i64 %824
  %829 = bitcast double* %828 to i64*
  store i64 %827, i64* %829, align 8, !tbaa !31
  %830 = add nuw nsw i64 %824, 1
  %831 = getelementptr inbounds double, double* %60, i64 %830
  %832 = bitcast double* %831 to i64*
  %833 = load i64, i64* %832, align 8, !tbaa !31
  %834 = getelementptr inbounds double, double* %55, i64 %830
  %835 = bitcast double* %834 to i64*
  store i64 %833, i64* %835, align 8, !tbaa !31
  %836 = add nsw i64 %824, 2
  %837 = getelementptr inbounds double, double* %60, i64 %836
  %838 = bitcast double* %837 to i64*
  %839 = load i64, i64* %838, align 8, !tbaa !31
  %840 = getelementptr inbounds double, double* %55, i64 %836
  %841 = bitcast double* %840 to i64*
  store i64 %839, i64* %841, align 8, !tbaa !31
  %842 = add nsw i64 %824, 3
  %843 = getelementptr inbounds double, double* %60, i64 %842
  %844 = bitcast double* %843 to i64*
  %845 = load i64, i64* %844, align 8, !tbaa !31
  %846 = getelementptr inbounds double, double* %55, i64 %842
  %847 = bitcast double* %846 to i64*
  store i64 %845, i64* %847, align 8, !tbaa !31
  %848 = add nsw i64 %824, 4
  %849 = getelementptr inbounds double, double* %60, i64 %848
  %850 = bitcast double* %849 to i64*
  %851 = load i64, i64* %850, align 8, !tbaa !31
  %852 = getelementptr inbounds double, double* %55, i64 %848
  %853 = bitcast double* %852 to i64*
  store i64 %851, i64* %853, align 8, !tbaa !31
  %854 = add nsw i64 %824, 5
  %855 = getelementptr inbounds double, double* %60, i64 %854
  %856 = bitcast double* %855 to i64*
  %857 = load i64, i64* %856, align 8, !tbaa !31
  %858 = getelementptr inbounds double, double* %55, i64 %854
  %859 = bitcast double* %858 to i64*
  store i64 %857, i64* %859, align 8, !tbaa !31
  %860 = add nsw i64 %824, 6
  %861 = getelementptr inbounds double, double* %60, i64 %860
  %862 = bitcast double* %861 to i64*
  %863 = load i64, i64* %862, align 8, !tbaa !31
  %864 = getelementptr inbounds double, double* %55, i64 %860
  %865 = bitcast double* %864 to i64*
  store i64 %863, i64* %865, align 8, !tbaa !31
  %866 = add nsw i64 %824, 7
  %867 = getelementptr inbounds double, double* %60, i64 %866
  %868 = bitcast double* %867 to i64*
  %869 = load i64, i64* %868, align 8, !tbaa !31
  %870 = getelementptr inbounds double, double* %55, i64 %866
  %871 = bitcast double* %870 to i64*
  store i64 %869, i64* %871, align 8, !tbaa !31
  %872 = add nsw i64 %824, 8
  %873 = icmp eq i64 %872, %712
  br i1 %873, label %874, label %823, !llvm.loop !49

; <label>:874:                                    ; preds = %579, %299, %819, %823, %798, %642, %710, %641, %69, %6, %47, %11
  %875 = phi i32 [ 0, %11 ], [ 0, %47 ], [ 0, %6 ], [ 0, %69 ], [ 1, %641 ], [ 1, %710 ], [ 1, %642 ], [ 1, %798 ], [ 1, %823 ], [ 1, %819 ], [ 0, %299 ], [ 0, %579 ]
  ret i32 %875
}

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

declare i32 @klu_scale(i32, i32, i32*, i32*, double*, double*, i32*, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { argmemonly nounwind }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !8, i64 76}
!4 = !{!"klu_common_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !8, i64 40, !8, i64 44, !8, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92, !8, i64 96, !5, i64 104, !5, i64 112, !5, i64 120, !5, i64 128, !5, i64 136, !10, i64 144, !10, i64 152}
!5 = !{!"double", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!"int", !6, i64 0}
!9 = !{!"any pointer", !6, i64 0}
!10 = !{!"long", !6, i64 0}
!11 = !{!4, !8, i64 88}
!12 = !{!4, !8, i64 92}
!13 = !{!14, !8, i64 40}
!14 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!15 = !{!14, !9, i64 56}
!16 = !{!14, !9, i64 64}
!17 = !{!14, !8, i64 76}
!18 = !{!14, !8, i64 80}
!19 = !{!20, !9, i64 24}
!20 = !{!"", !8, i64 0, !8, i64 4, !8, i64 8, !8, i64 12, !8, i64 16, !8, i64 20, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48, !9, i64 56, !9, i64 64, !9, i64 72, !9, i64 80, !9, i64 88, !9, i64 96, !10, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !9, i64 144, !9, i64 152, !8, i64 160}
!21 = !{!20, !9, i64 152}
!22 = !{!20, !9, i64 72}
!23 = !{!4, !8, i64 48}
!24 = !{!20, !9, i64 96}
!25 = !{!20, !9, i64 32}
!26 = !{!20, !9, i64 120}
!27 = !{!4, !8, i64 80}
!28 = !{!20, !9, i64 88}
!29 = !{!14, !8, i64 72}
!30 = !{!8, !8, i64 0}
!31 = !{!5, !5, i64 0}
!32 = !{!20, !9, i64 40}
!33 = !{!20, !9, i64 56}
!34 = !{!20, !9, i64 48}
!35 = !{!20, !9, i64 64}
!36 = !{!9, !9, i64 0}
!37 = !{!4, !8, i64 72}
!38 = distinct !{!38, !39}
!39 = !{!"llvm.loop.unroll.disable"}
!40 = !{!41}
!41 = distinct !{!41, !42}
!42 = distinct !{!42, !"LVerDomain"}
!43 = !{!44}
!44 = distinct !{!44, !42}
!45 = distinct !{!45, !46}
!46 = !{!"llvm.loop.isvectorized", i32 1}
!47 = distinct !{!47, !39}
!48 = distinct !{!48, !39}
!49 = distinct !{!49, !46}
