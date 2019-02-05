; ModuleID = 'klu_solve.c'
source_filename = "klu_solve.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_solve(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %7 = icmp eq %struct.klu_common_struct* %5, null
  br i1 %7, label %987, label %8

; <label>:8:                                      ; preds = %6
  %9 = icmp eq %struct.klu_numeric* %1, null
  %10 = icmp eq %struct.klu_symbolic* %0, null
  %11 = or i1 %10, %9
  br i1 %11, label %20, label %12

; <label>:12:                                     ; preds = %8
  %13 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 5
  %14 = load i32, i32* %13, align 8, !tbaa !3
  %15 = icmp sgt i32 %14, %2
  %16 = icmp slt i32 %3, 0
  %17 = or i1 %16, %15
  %18 = icmp eq double* %4, null
  %19 = or i1 %18, %17
  br i1 %19, label %20, label %22

; <label>:20:                                     ; preds = %12, %8
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 -3, i32* %21, align 4, !tbaa !10
  br label %987

; <label>:22:                                     ; preds = %12
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 0, i32* %23, align 4, !tbaa !10
  %24 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 11
  %25 = load i32, i32* %24, align 4, !tbaa !13
  %26 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 8
  %27 = load i32*, i32** %26, align 8, !tbaa !14
  %28 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 9
  %29 = load i32*, i32** %28, align 8, !tbaa !15
  %30 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 6
  %31 = load i32*, i32** %30, align 8, !tbaa !16
  %32 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 20
  %33 = load i32*, i32** %32, align 8, !tbaa !18
  %34 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 21
  %35 = load i32*, i32** %34, align 8, !tbaa !19
  %36 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 22
  %37 = bitcast i8** %36 to double**
  %38 = load double*, double** %37, align 8, !tbaa !20
  %39 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 8
  %40 = load i32*, i32** %39, align 8, !tbaa !21
  %41 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 10
  %42 = load i32*, i32** %41, align 8, !tbaa !22
  %43 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 9
  %44 = load i32*, i32** %43, align 8, !tbaa !23
  %45 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 11
  %46 = load i32*, i32** %45, align 8, !tbaa !24
  %47 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 12
  %48 = bitcast i8*** %47 to double***
  %49 = load double**, double*** %48, align 8, !tbaa !25
  %50 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 14
  %51 = bitcast i8** %50 to double**
  %52 = load double*, double** %51, align 8, !tbaa !26
  %53 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 15
  %54 = load double*, double** %53, align 8, !tbaa !27
  %55 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 18
  %56 = bitcast i8** %55 to double**
  %57 = load double*, double** %56, align 8, !tbaa !28
  %58 = icmp sgt i32 %3, 0
  br i1 %58, label %59, label %987

; <label>:59:                                     ; preds = %22
  %60 = icmp eq double* %54, null
  %61 = icmp sgt i32 %25, 0
  %62 = icmp sgt i32 %14, 0
  %63 = shl i32 %2, 1
  %64 = mul nsw i32 %2, 3
  %65 = shl nsw i32 %2, 2
  %66 = sext i32 %65 to i64
  %67 = sext i32 %25 to i64
  %68 = zext i32 %14 to i64
  %69 = zext i32 %14 to i64
  %70 = zext i32 %14 to i64
  %71 = zext i32 %14 to i64
  %72 = zext i32 %14 to i64
  %73 = zext i32 %14 to i64
  %74 = zext i32 %14 to i64
  %75 = add nsw i64 %72, -1
  %76 = and i64 %72, 3
  %77 = icmp ult i64 %75, 3
  %78 = and i64 %72, 1
  %79 = icmp eq i64 %75, 0
  %80 = and i64 %72, 1
  %81 = icmp eq i64 %75, 0
  %82 = sub nsw i64 %72, %76
  %83 = icmp eq i64 %76, 0
  %84 = sub nsw i64 %72, %78
  %85 = icmp eq i64 %78, 0
  %86 = and i64 %72, 3
  %87 = icmp ult i64 %75, 3
  %88 = and i64 %72, 1
  %89 = icmp eq i64 %75, 0
  %90 = sub nsw i64 %72, %80
  %91 = icmp eq i64 %80, 0
  %92 = sub nsw i64 %72, %86
  %93 = icmp eq i64 %86, 0
  %94 = sub nsw i64 %72, %88
  %95 = icmp eq i64 %88, 0
  br label %96

; <label>:96:                                     ; preds = %59, %983
  %97 = phi i32 [ 0, %59 ], [ %985, %983 ]
  %98 = phi double* [ %4, %59 ], [ %984, %983 ]
  %99 = sub nsw i32 %3, %97
  %100 = icmp slt i32 %99, 4
  %101 = select i1 %100, i32 %99, i32 4
  br i1 %60, label %102, label %270

; <label>:102:                                    ; preds = %96
  switch i32 %101, label %447 [
    i32 1, label %103
    i32 2, label %147
    i32 3, label %191
    i32 4, label %226
  ]

; <label>:103:                                    ; preds = %102
  br i1 %62, label %104, label %447

; <label>:104:                                    ; preds = %103
  br i1 %77, label %398, label %105

; <label>:105:                                    ; preds = %104
  br label %106

; <label>:106:                                    ; preds = %106, %105
  %107 = phi i64 [ 0, %105 ], [ %144, %106 ]
  %108 = phi i64 [ %82, %105 ], [ %145, %106 ]
  %109 = getelementptr inbounds i32, i32* %31, i64 %107
  %110 = load i32, i32* %109, align 4, !tbaa !29
  %111 = sext i32 %110 to i64
  %112 = getelementptr inbounds double, double* %98, i64 %111
  %113 = bitcast double* %112 to i64*
  %114 = load i64, i64* %113, align 8, !tbaa !30
  %115 = getelementptr inbounds double, double* %57, i64 %107
  %116 = bitcast double* %115 to i64*
  store i64 %114, i64* %116, align 8, !tbaa !30
  %117 = or i64 %107, 1
  %118 = getelementptr inbounds i32, i32* %31, i64 %117
  %119 = load i32, i32* %118, align 4, !tbaa !29
  %120 = sext i32 %119 to i64
  %121 = getelementptr inbounds double, double* %98, i64 %120
  %122 = bitcast double* %121 to i64*
  %123 = load i64, i64* %122, align 8, !tbaa !30
  %124 = getelementptr inbounds double, double* %57, i64 %117
  %125 = bitcast double* %124 to i64*
  store i64 %123, i64* %125, align 8, !tbaa !30
  %126 = or i64 %107, 2
  %127 = getelementptr inbounds i32, i32* %31, i64 %126
  %128 = load i32, i32* %127, align 4, !tbaa !29
  %129 = sext i32 %128 to i64
  %130 = getelementptr inbounds double, double* %98, i64 %129
  %131 = bitcast double* %130 to i64*
  %132 = load i64, i64* %131, align 8, !tbaa !30
  %133 = getelementptr inbounds double, double* %57, i64 %126
  %134 = bitcast double* %133 to i64*
  store i64 %132, i64* %134, align 8, !tbaa !30
  %135 = or i64 %107, 3
  %136 = getelementptr inbounds i32, i32* %31, i64 %135
  %137 = load i32, i32* %136, align 4, !tbaa !29
  %138 = sext i32 %137 to i64
  %139 = getelementptr inbounds double, double* %98, i64 %138
  %140 = bitcast double* %139 to i64*
  %141 = load i64, i64* %140, align 8, !tbaa !30
  %142 = getelementptr inbounds double, double* %57, i64 %135
  %143 = bitcast double* %142 to i64*
  store i64 %141, i64* %143, align 8, !tbaa !30
  %144 = add nuw nsw i64 %107, 4
  %145 = add i64 %108, -4
  %146 = icmp eq i64 %145, 0
  br i1 %146, label %398, label %106

; <label>:147:                                    ; preds = %102
  br i1 %62, label %148, label %447

; <label>:148:                                    ; preds = %147
  br i1 %79, label %415, label %149

; <label>:149:                                    ; preds = %148
  br label %150

; <label>:150:                                    ; preds = %150, %149
  %151 = phi i64 [ 0, %149 ], [ %188, %150 ]
  %152 = phi i64 [ %84, %149 ], [ %189, %150 ]
  %153 = getelementptr inbounds i32, i32* %31, i64 %151
  %154 = load i32, i32* %153, align 4, !tbaa !29
  %155 = sext i32 %154 to i64
  %156 = getelementptr inbounds double, double* %98, i64 %155
  %157 = bitcast double* %156 to i64*
  %158 = load i64, i64* %157, align 8, !tbaa !30
  %159 = shl nuw nsw i64 %151, 1
  %160 = getelementptr inbounds double, double* %57, i64 %159
  %161 = bitcast double* %160 to i64*
  store i64 %158, i64* %161, align 8, !tbaa !30
  %162 = add nsw i32 %154, %2
  %163 = sext i32 %162 to i64
  %164 = getelementptr inbounds double, double* %98, i64 %163
  %165 = bitcast double* %164 to i64*
  %166 = load i64, i64* %165, align 8, !tbaa !30
  %167 = or i64 %159, 1
  %168 = getelementptr inbounds double, double* %57, i64 %167
  %169 = bitcast double* %168 to i64*
  store i64 %166, i64* %169, align 8, !tbaa !30
  %170 = or i64 %151, 1
  %171 = getelementptr inbounds i32, i32* %31, i64 %170
  %172 = load i32, i32* %171, align 4, !tbaa !29
  %173 = sext i32 %172 to i64
  %174 = getelementptr inbounds double, double* %98, i64 %173
  %175 = bitcast double* %174 to i64*
  %176 = load i64, i64* %175, align 8, !tbaa !30
  %177 = shl nuw nsw i64 %170, 1
  %178 = getelementptr inbounds double, double* %57, i64 %177
  %179 = bitcast double* %178 to i64*
  store i64 %176, i64* %179, align 8, !tbaa !30
  %180 = add nsw i32 %172, %2
  %181 = sext i32 %180 to i64
  %182 = getelementptr inbounds double, double* %98, i64 %181
  %183 = bitcast double* %182 to i64*
  %184 = load i64, i64* %183, align 8, !tbaa !30
  %185 = or i64 %177, 1
  %186 = getelementptr inbounds double, double* %57, i64 %185
  %187 = bitcast double* %186 to i64*
  store i64 %184, i64* %187, align 8, !tbaa !30
  %188 = add nuw nsw i64 %151, 2
  %189 = add i64 %152, -2
  %190 = icmp eq i64 %189, 0
  br i1 %190, label %415, label %150

; <label>:191:                                    ; preds = %102
  br i1 %62, label %192, label %447

; <label>:192:                                    ; preds = %191
  br label %193

; <label>:193:                                    ; preds = %192, %193
  %194 = phi i64 [ %224, %193 ], [ 0, %192 ]
  %195 = getelementptr inbounds i32, i32* %31, i64 %194
  %196 = load i32, i32* %195, align 4, !tbaa !29
  %197 = sext i32 %196 to i64
  %198 = getelementptr inbounds double, double* %98, i64 %197
  %199 = bitcast double* %198 to i64*
  %200 = load i64, i64* %199, align 8, !tbaa !30
  %201 = trunc i64 %194 to i32
  %202 = mul nsw i32 %201, 3
  %203 = zext i32 %202 to i64
  %204 = getelementptr inbounds double, double* %57, i64 %203
  %205 = bitcast double* %204 to i64*
  store i64 %200, i64* %205, align 8, !tbaa !30
  %206 = add nsw i32 %196, %2
  %207 = sext i32 %206 to i64
  %208 = getelementptr inbounds double, double* %98, i64 %207
  %209 = bitcast double* %208 to i64*
  %210 = load i64, i64* %209, align 8, !tbaa !30
  %211 = add nuw nsw i32 %202, 1
  %212 = zext i32 %211 to i64
  %213 = getelementptr inbounds double, double* %57, i64 %212
  %214 = bitcast double* %213 to i64*
  store i64 %210, i64* %214, align 8, !tbaa !30
  %215 = add nsw i32 %196, %63
  %216 = sext i32 %215 to i64
  %217 = getelementptr inbounds double, double* %98, i64 %216
  %218 = bitcast double* %217 to i64*
  %219 = load i64, i64* %218, align 8, !tbaa !30
  %220 = add nuw nsw i32 %202, 2
  %221 = zext i32 %220 to i64
  %222 = getelementptr inbounds double, double* %57, i64 %221
  %223 = bitcast double* %222 to i64*
  store i64 %219, i64* %223, align 8, !tbaa !30
  %224 = add nuw nsw i64 %194, 1
  %225 = icmp eq i64 %224, %68
  br i1 %225, label %447, label %193

; <label>:226:                                    ; preds = %102
  br i1 %62, label %227, label %447

; <label>:227:                                    ; preds = %226
  br label %228

; <label>:228:                                    ; preds = %227, %228
  %229 = phi i64 [ %268, %228 ], [ 0, %227 ]
  %230 = getelementptr inbounds i32, i32* %31, i64 %229
  %231 = load i32, i32* %230, align 4, !tbaa !29
  %232 = sext i32 %231 to i64
  %233 = getelementptr inbounds double, double* %98, i64 %232
  %234 = bitcast double* %233 to i64*
  %235 = load i64, i64* %234, align 8, !tbaa !30
  %236 = trunc i64 %229 to i32
  %237 = shl nsw i32 %236, 2
  %238 = zext i32 %237 to i64
  %239 = getelementptr inbounds double, double* %57, i64 %238
  %240 = bitcast double* %239 to i64*
  store i64 %235, i64* %240, align 8, !tbaa !30
  %241 = add nsw i32 %231, %2
  %242 = sext i32 %241 to i64
  %243 = getelementptr inbounds double, double* %98, i64 %242
  %244 = bitcast double* %243 to i64*
  %245 = load i64, i64* %244, align 8, !tbaa !30
  %246 = or i32 %237, 1
  %247 = zext i32 %246 to i64
  %248 = getelementptr inbounds double, double* %57, i64 %247
  %249 = bitcast double* %248 to i64*
  store i64 %245, i64* %249, align 8, !tbaa !30
  %250 = add nsw i32 %231, %63
  %251 = sext i32 %250 to i64
  %252 = getelementptr inbounds double, double* %98, i64 %251
  %253 = bitcast double* %252 to i64*
  %254 = load i64, i64* %253, align 8, !tbaa !30
  %255 = or i32 %237, 2
  %256 = zext i32 %255 to i64
  %257 = getelementptr inbounds double, double* %57, i64 %256
  %258 = bitcast double* %257 to i64*
  store i64 %254, i64* %258, align 8, !tbaa !30
  %259 = add nsw i32 %231, %64
  %260 = sext i32 %259 to i64
  %261 = getelementptr inbounds double, double* %98, i64 %260
  %262 = bitcast double* %261 to i64*
  %263 = load i64, i64* %262, align 8, !tbaa !30
  %264 = or i32 %237, 3
  %265 = zext i32 %264 to i64
  %266 = getelementptr inbounds double, double* %57, i64 %265
  %267 = bitcast double* %266 to i64*
  store i64 %263, i64* %267, align 8, !tbaa !30
  %268 = add nuw nsw i64 %229, 1
  %269 = icmp eq i64 %268, %69
  br i1 %269, label %447, label %228

; <label>:270:                                    ; preds = %96
  switch i32 %101, label %447 [
    i32 1, label %271
    i32 2, label %299
    i32 3, label %322
    i32 4, label %356
  ]

; <label>:271:                                    ; preds = %270
  br i1 %62, label %272, label %447

; <label>:272:                                    ; preds = %271
  br i1 %81, label %435, label %273

; <label>:273:                                    ; preds = %272
  br label %274

; <label>:274:                                    ; preds = %274, %273
  %275 = phi i64 [ 0, %273 ], [ %296, %274 ]
  %276 = phi i64 [ %90, %273 ], [ %297, %274 ]
  %277 = getelementptr inbounds i32, i32* %31, i64 %275
  %278 = load i32, i32* %277, align 4, !tbaa !29
  %279 = sext i32 %278 to i64
  %280 = getelementptr inbounds double, double* %98, i64 %279
  %281 = load double, double* %280, align 8, !tbaa !30
  %282 = getelementptr inbounds double, double* %54, i64 %275
  %283 = load double, double* %282, align 8, !tbaa !30
  %284 = fdiv double %281, %283
  %285 = getelementptr inbounds double, double* %57, i64 %275
  store double %284, double* %285, align 8, !tbaa !30
  %286 = or i64 %275, 1
  %287 = getelementptr inbounds i32, i32* %31, i64 %286
  %288 = load i32, i32* %287, align 4, !tbaa !29
  %289 = sext i32 %288 to i64
  %290 = getelementptr inbounds double, double* %98, i64 %289
  %291 = load double, double* %290, align 8, !tbaa !30
  %292 = getelementptr inbounds double, double* %54, i64 %286
  %293 = load double, double* %292, align 8, !tbaa !30
  %294 = fdiv double %291, %293
  %295 = getelementptr inbounds double, double* %57, i64 %286
  store double %294, double* %295, align 8, !tbaa !30
  %296 = add nuw nsw i64 %275, 2
  %297 = add i64 %276, -2
  %298 = icmp eq i64 %297, 0
  br i1 %298, label %435, label %274

; <label>:299:                                    ; preds = %270
  br i1 %62, label %300, label %447

; <label>:300:                                    ; preds = %299
  br label %301

; <label>:301:                                    ; preds = %300, %301
  %302 = phi i64 [ %320, %301 ], [ 0, %300 ]
  %303 = getelementptr inbounds i32, i32* %31, i64 %302
  %304 = load i32, i32* %303, align 4, !tbaa !29
  %305 = getelementptr inbounds double, double* %54, i64 %302
  %306 = load double, double* %305, align 8, !tbaa !30
  %307 = sext i32 %304 to i64
  %308 = getelementptr inbounds double, double* %98, i64 %307
  %309 = load double, double* %308, align 8, !tbaa !30
  %310 = fdiv double %309, %306
  %311 = shl nuw nsw i64 %302, 1
  %312 = getelementptr inbounds double, double* %57, i64 %311
  store double %310, double* %312, align 8, !tbaa !30
  %313 = add nsw i32 %304, %2
  %314 = sext i32 %313 to i64
  %315 = getelementptr inbounds double, double* %98, i64 %314
  %316 = load double, double* %315, align 8, !tbaa !30
  %317 = fdiv double %316, %306
  %318 = or i64 %311, 1
  %319 = getelementptr inbounds double, double* %57, i64 %318
  store double %317, double* %319, align 8, !tbaa !30
  %320 = add nuw nsw i64 %302, 1
  %321 = icmp eq i64 %320, %70
  br i1 %321, label %447, label %301

; <label>:322:                                    ; preds = %270
  br i1 %62, label %323, label %447

; <label>:323:                                    ; preds = %322
  br label %324

; <label>:324:                                    ; preds = %323, %324
  %325 = phi i64 [ %354, %324 ], [ 0, %323 ]
  %326 = getelementptr inbounds i32, i32* %31, i64 %325
  %327 = load i32, i32* %326, align 4, !tbaa !29
  %328 = getelementptr inbounds double, double* %54, i64 %325
  %329 = load double, double* %328, align 8, !tbaa !30
  %330 = sext i32 %327 to i64
  %331 = getelementptr inbounds double, double* %98, i64 %330
  %332 = load double, double* %331, align 8, !tbaa !30
  %333 = fdiv double %332, %329
  %334 = trunc i64 %325 to i32
  %335 = mul nsw i32 %334, 3
  %336 = zext i32 %335 to i64
  %337 = getelementptr inbounds double, double* %57, i64 %336
  store double %333, double* %337, align 8, !tbaa !30
  %338 = add nsw i32 %327, %2
  %339 = sext i32 %338 to i64
  %340 = getelementptr inbounds double, double* %98, i64 %339
  %341 = load double, double* %340, align 8, !tbaa !30
  %342 = fdiv double %341, %329
  %343 = add nuw nsw i32 %335, 1
  %344 = zext i32 %343 to i64
  %345 = getelementptr inbounds double, double* %57, i64 %344
  store double %342, double* %345, align 8, !tbaa !30
  %346 = add nsw i32 %327, %63
  %347 = sext i32 %346 to i64
  %348 = getelementptr inbounds double, double* %98, i64 %347
  %349 = load double, double* %348, align 8, !tbaa !30
  %350 = fdiv double %349, %329
  %351 = add nuw nsw i32 %335, 2
  %352 = zext i32 %351 to i64
  %353 = getelementptr inbounds double, double* %57, i64 %352
  store double %350, double* %353, align 8, !tbaa !30
  %354 = add nuw nsw i64 %325, 1
  %355 = icmp eq i64 %354, %71
  br i1 %355, label %447, label %324

; <label>:356:                                    ; preds = %270
  br i1 %62, label %357, label %447

; <label>:357:                                    ; preds = %356
  br label %358

; <label>:358:                                    ; preds = %357, %358
  %359 = phi i64 [ %396, %358 ], [ 0, %357 ]
  %360 = getelementptr inbounds i32, i32* %31, i64 %359
  %361 = load i32, i32* %360, align 4, !tbaa !29
  %362 = getelementptr inbounds double, double* %54, i64 %359
  %363 = load double, double* %362, align 8, !tbaa !30
  %364 = sext i32 %361 to i64
  %365 = getelementptr inbounds double, double* %98, i64 %364
  %366 = load double, double* %365, align 8, !tbaa !30
  %367 = fdiv double %366, %363
  %368 = trunc i64 %359 to i32
  %369 = shl nsw i32 %368, 2
  %370 = zext i32 %369 to i64
  %371 = getelementptr inbounds double, double* %57, i64 %370
  store double %367, double* %371, align 8, !tbaa !30
  %372 = add nsw i32 %361, %2
  %373 = sext i32 %372 to i64
  %374 = getelementptr inbounds double, double* %98, i64 %373
  %375 = load double, double* %374, align 8, !tbaa !30
  %376 = fdiv double %375, %363
  %377 = or i32 %369, 1
  %378 = zext i32 %377 to i64
  %379 = getelementptr inbounds double, double* %57, i64 %378
  store double %376, double* %379, align 8, !tbaa !30
  %380 = add nsw i32 %361, %63
  %381 = sext i32 %380 to i64
  %382 = getelementptr inbounds double, double* %98, i64 %381
  %383 = load double, double* %382, align 8, !tbaa !30
  %384 = fdiv double %383, %363
  %385 = or i32 %369, 2
  %386 = zext i32 %385 to i64
  %387 = getelementptr inbounds double, double* %57, i64 %386
  store double %384, double* %387, align 8, !tbaa !30
  %388 = add nsw i32 %361, %64
  %389 = sext i32 %388 to i64
  %390 = getelementptr inbounds double, double* %98, i64 %389
  %391 = load double, double* %390, align 8, !tbaa !30
  %392 = fdiv double %391, %363
  %393 = or i32 %369, 3
  %394 = zext i32 %393 to i64
  %395 = getelementptr inbounds double, double* %57, i64 %394
  store double %392, double* %395, align 8, !tbaa !30
  %396 = add nuw nsw i64 %359, 1
  %397 = icmp eq i64 %396, %72
  br i1 %397, label %447, label %358

; <label>:398:                                    ; preds = %106, %104
  %399 = phi i64 [ 0, %104 ], [ %144, %106 ]
  br i1 %83, label %447, label %400

; <label>:400:                                    ; preds = %398
  br label %401

; <label>:401:                                    ; preds = %401, %400
  %402 = phi i64 [ %412, %401 ], [ %399, %400 ]
  %403 = phi i64 [ %413, %401 ], [ %76, %400 ]
  %404 = getelementptr inbounds i32, i32* %31, i64 %402
  %405 = load i32, i32* %404, align 4, !tbaa !29
  %406 = sext i32 %405 to i64
  %407 = getelementptr inbounds double, double* %98, i64 %406
  %408 = bitcast double* %407 to i64*
  %409 = load i64, i64* %408, align 8, !tbaa !30
  %410 = getelementptr inbounds double, double* %57, i64 %402
  %411 = bitcast double* %410 to i64*
  store i64 %409, i64* %411, align 8, !tbaa !30
  %412 = add nuw nsw i64 %402, 1
  %413 = add i64 %403, -1
  %414 = icmp eq i64 %413, 0
  br i1 %414, label %447, label %401, !llvm.loop !31

; <label>:415:                                    ; preds = %150, %148
  %416 = phi i64 [ 0, %148 ], [ %188, %150 ]
  br i1 %85, label %447, label %417

; <label>:417:                                    ; preds = %415
  %418 = getelementptr inbounds i32, i32* %31, i64 %416
  %419 = load i32, i32* %418, align 4, !tbaa !29
  %420 = sext i32 %419 to i64
  %421 = getelementptr inbounds double, double* %98, i64 %420
  %422 = bitcast double* %421 to i64*
  %423 = load i64, i64* %422, align 8, !tbaa !30
  %424 = shl nuw nsw i64 %416, 1
  %425 = getelementptr inbounds double, double* %57, i64 %424
  %426 = bitcast double* %425 to i64*
  store i64 %423, i64* %426, align 8, !tbaa !30
  %427 = add nsw i32 %419, %2
  %428 = sext i32 %427 to i64
  %429 = getelementptr inbounds double, double* %98, i64 %428
  %430 = bitcast double* %429 to i64*
  %431 = load i64, i64* %430, align 8, !tbaa !30
  %432 = or i64 %424, 1
  %433 = getelementptr inbounds double, double* %57, i64 %432
  %434 = bitcast double* %433 to i64*
  store i64 %431, i64* %434, align 8, !tbaa !30
  br label %447

; <label>:435:                                    ; preds = %274, %272
  %436 = phi i64 [ 0, %272 ], [ %296, %274 ]
  br i1 %91, label %447, label %437

; <label>:437:                                    ; preds = %435
  %438 = getelementptr inbounds i32, i32* %31, i64 %436
  %439 = load i32, i32* %438, align 4, !tbaa !29
  %440 = sext i32 %439 to i64
  %441 = getelementptr inbounds double, double* %98, i64 %440
  %442 = load double, double* %441, align 8, !tbaa !30
  %443 = getelementptr inbounds double, double* %54, i64 %436
  %444 = load double, double* %443, align 8, !tbaa !30
  %445 = fdiv double %442, %444
  %446 = getelementptr inbounds double, double* %57, i64 %436
  store double %445, double* %446, align 8, !tbaa !30
  br label %447

; <label>:447:                                    ; preds = %358, %324, %301, %437, %435, %228, %193, %417, %415, %398, %401, %356, %322, %299, %271, %226, %191, %147, %103, %270, %102
  br i1 %61, label %448, label %778

; <label>:448:                                    ; preds = %447
  br label %449

; <label>:449:                                    ; preds = %448, %777
  %450 = phi i64 [ %451, %777 ], [ %67, %448 ]
  %451 = add nsw i64 %450, -1
  %452 = getelementptr inbounds i32, i32* %29, i64 %451
  %453 = load i32, i32* %452, align 4, !tbaa !29
  %454 = getelementptr inbounds i32, i32* %29, i64 %450
  %455 = load i32, i32* %454, align 4, !tbaa !29
  %456 = sub nsw i32 %455, %453
  %457 = icmp eq i32 %456, 1
  %458 = sext i32 %453 to i64
  br i1 %457, label %459, label %509

; <label>:459:                                    ; preds = %449
  %460 = getelementptr inbounds double, double* %52, i64 %458
  %461 = load double, double* %460, align 8, !tbaa !30
  switch i32 %101, label %521 [
    i32 1, label %462
    i32 2, label %466
    i32 3, label %476
    i32 4, label %492
  ]

; <label>:462:                                    ; preds = %459
  %463 = getelementptr inbounds double, double* %57, i64 %458
  %464 = load double, double* %463, align 8, !tbaa !30
  %465 = fdiv double %464, %461
  store double %465, double* %463, align 8, !tbaa !30
  br label %521

; <label>:466:                                    ; preds = %459
  %467 = shl nsw i32 %453, 1
  %468 = sext i32 %467 to i64
  %469 = getelementptr inbounds double, double* %57, i64 %468
  %470 = bitcast double* %469 to <2 x double>*
  %471 = load <2 x double>, <2 x double>* %470, align 8, !tbaa !30
  %472 = insertelement <2 x double> undef, double %461, i32 0
  %473 = shufflevector <2 x double> %472, <2 x double> undef, <2 x i32> zeroinitializer
  %474 = fdiv <2 x double> %471, %473
  %475 = bitcast double* %469 to <2 x double>*
  store <2 x double> %474, <2 x double>* %475, align 8, !tbaa !30
  br label %521

; <label>:476:                                    ; preds = %459
  %477 = mul nsw i32 %453, 3
  %478 = sext i32 %477 to i64
  %479 = getelementptr inbounds double, double* %57, i64 %478
  %480 = load double, double* %479, align 8, !tbaa !30
  %481 = fdiv double %480, %461
  store double %481, double* %479, align 8, !tbaa !30
  %482 = add nsw i32 %477, 1
  %483 = sext i32 %482 to i64
  %484 = getelementptr inbounds double, double* %57, i64 %483
  %485 = load double, double* %484, align 8, !tbaa !30
  %486 = fdiv double %485, %461
  store double %486, double* %484, align 8, !tbaa !30
  %487 = add nsw i32 %477, 2
  %488 = sext i32 %487 to i64
  %489 = getelementptr inbounds double, double* %57, i64 %488
  %490 = load double, double* %489, align 8, !tbaa !30
  %491 = fdiv double %490, %461
  store double %491, double* %489, align 8, !tbaa !30
  br label %521

; <label>:492:                                    ; preds = %459
  %493 = shl nsw i32 %453, 2
  %494 = sext i32 %493 to i64
  %495 = getelementptr inbounds double, double* %57, i64 %494
  %496 = bitcast double* %495 to <2 x double>*
  %497 = load <2 x double>, <2 x double>* %496, align 8, !tbaa !30
  %498 = insertelement <2 x double> undef, double %461, i32 0
  %499 = shufflevector <2 x double> %498, <2 x double> undef, <2 x i32> zeroinitializer
  %500 = fdiv <2 x double> %497, %499
  %501 = bitcast double* %495 to <2 x double>*
  store <2 x double> %500, <2 x double>* %501, align 8, !tbaa !30
  %502 = or i32 %493, 2
  %503 = sext i32 %502 to i64
  %504 = getelementptr inbounds double, double* %57, i64 %503
  %505 = bitcast double* %504 to <2 x double>*
  %506 = load <2 x double>, <2 x double>* %505, align 8, !tbaa !30
  %507 = fdiv <2 x double> %506, %499
  %508 = bitcast double* %504 to <2 x double>*
  store <2 x double> %507, <2 x double>* %508, align 8, !tbaa !30
  br label %521

; <label>:509:                                    ; preds = %449
  %510 = getelementptr inbounds i32, i32* %40, i64 %458
  %511 = getelementptr inbounds i32, i32* %42, i64 %458
  %512 = getelementptr inbounds double*, double** %49, i64 %451
  %513 = load double*, double** %512, align 8, !tbaa !33
  %514 = mul nsw i32 %453, %101
  %515 = sext i32 %514 to i64
  %516 = getelementptr inbounds double, double* %57, i64 %515
  tail call void @klu_lsolve(i32 %456, i32* %510, i32* %511, double* %513, i32 %101, double* %516) #2
  %517 = getelementptr inbounds i32, i32* %44, i64 %458
  %518 = getelementptr inbounds i32, i32* %46, i64 %458
  %519 = load double*, double** %512, align 8, !tbaa !33
  %520 = getelementptr inbounds double, double* %52, i64 %458
  tail call void @klu_usolve(i32 %456, i32* %517, i32* %518, double* %519, double* %520, i32 %101, double* %516) #2
  br label %521

; <label>:521:                                    ; preds = %462, %466, %476, %492, %459, %509
  %522 = icmp sgt i64 %450, 1
  br i1 %522, label %523, label %778

; <label>:523:                                    ; preds = %521
  switch i32 %101, label %777 [
    i32 1, label %524
    i32 2, label %586
    i32 3, label %668
    i32 4, label %721
  ]

; <label>:524:                                    ; preds = %523
  %525 = icmp sgt i32 %455, %453
  br i1 %525, label %526, label %777

; <label>:526:                                    ; preds = %524
  %527 = getelementptr inbounds i32, i32* %33, i64 %458
  %528 = load i32, i32* %527, align 4, !tbaa !29
  %529 = sext i32 %455 to i64
  br label %530

; <label>:530:                                    ; preds = %584, %526
  %531 = phi i32 [ %528, %526 ], [ %535, %584 ]
  %532 = phi i64 [ %458, %526 ], [ %533, %584 ]
  %533 = add nsw i64 %532, 1
  %534 = getelementptr inbounds i32, i32* %33, i64 %533
  %535 = load i32, i32* %534, align 4, !tbaa !29
  %536 = getelementptr inbounds double, double* %57, i64 %532
  %537 = load double, double* %536, align 8, !tbaa !30
  %538 = icmp slt i32 %531, %535
  br i1 %538, label %539, label %584

; <label>:539:                                    ; preds = %530
  %540 = sext i32 %531 to i64
  %541 = sext i32 %535 to i64
  %542 = sub nsw i64 %541, %540
  %543 = add nsw i64 %541, -1
  %544 = and i64 %542, 1
  %545 = icmp eq i64 %544, 0
  br i1 %545, label %557, label %546

; <label>:546:                                    ; preds = %539
  %547 = getelementptr inbounds double, double* %38, i64 %540
  %548 = load double, double* %547, align 8, !tbaa !30
  %549 = fmul double %537, %548
  %550 = getelementptr inbounds i32, i32* %35, i64 %540
  %551 = load i32, i32* %550, align 4, !tbaa !29
  %552 = sext i32 %551 to i64
  %553 = getelementptr inbounds double, double* %57, i64 %552
  %554 = load double, double* %553, align 8, !tbaa !30
  %555 = fsub double %554, %549
  store double %555, double* %553, align 8, !tbaa !30
  %556 = add nsw i64 %540, 1
  br label %557

; <label>:557:                                    ; preds = %546, %539
  %558 = phi i64 [ %556, %546 ], [ %540, %539 ]
  %559 = icmp eq i64 %543, %540
  br i1 %559, label %584, label %560

; <label>:560:                                    ; preds = %557
  br label %561

; <label>:561:                                    ; preds = %561, %560
  %562 = phi i64 [ %558, %560 ], [ %582, %561 ]
  %563 = getelementptr inbounds double, double* %38, i64 %562
  %564 = load double, double* %563, align 8, !tbaa !30
  %565 = fmul double %537, %564
  %566 = getelementptr inbounds i32, i32* %35, i64 %562
  %567 = load i32, i32* %566, align 4, !tbaa !29
  %568 = sext i32 %567 to i64
  %569 = getelementptr inbounds double, double* %57, i64 %568
  %570 = load double, double* %569, align 8, !tbaa !30
  %571 = fsub double %570, %565
  store double %571, double* %569, align 8, !tbaa !30
  %572 = add nsw i64 %562, 1
  %573 = getelementptr inbounds double, double* %38, i64 %572
  %574 = load double, double* %573, align 8, !tbaa !30
  %575 = fmul double %537, %574
  %576 = getelementptr inbounds i32, i32* %35, i64 %572
  %577 = load i32, i32* %576, align 4, !tbaa !29
  %578 = sext i32 %577 to i64
  %579 = getelementptr inbounds double, double* %57, i64 %578
  %580 = load double, double* %579, align 8, !tbaa !30
  %581 = fsub double %580, %575
  store double %581, double* %579, align 8, !tbaa !30
  %582 = add nsw i64 %562, 2
  %583 = icmp eq i64 %582, %541
  br i1 %583, label %584, label %561

; <label>:584:                                    ; preds = %557, %561, %530
  %585 = icmp eq i64 %533, %529
  br i1 %585, label %777, label %530

; <label>:586:                                    ; preds = %523
  %587 = icmp sgt i32 %455, %453
  br i1 %587, label %588, label %777

; <label>:588:                                    ; preds = %586
  %589 = getelementptr inbounds i32, i32* %33, i64 %458
  %590 = load i32, i32* %589, align 4, !tbaa !29
  %591 = sext i32 %455 to i64
  br label %592

; <label>:592:                                    ; preds = %666, %588
  %593 = phi i32 [ %590, %588 ], [ %599, %666 ]
  %594 = phi i64 [ %458, %588 ], [ %596, %666 ]
  %595 = phi i32 [ %453, %588 ], [ %597, %666 ]
  %596 = add nsw i64 %594, 1
  %597 = add nsw i32 %595, 1
  %598 = getelementptr inbounds i32, i32* %33, i64 %596
  %599 = load i32, i32* %598, align 4, !tbaa !29
  %600 = shl nsw i32 %595, 1
  %601 = sext i32 %600 to i64
  %602 = getelementptr inbounds double, double* %57, i64 %601
  %603 = bitcast double* %602 to <2 x double>*
  %604 = load <2 x double>, <2 x double>* %603, align 8, !tbaa !30
  %605 = icmp slt i32 %593, %599
  br i1 %605, label %606, label %666

; <label>:606:                                    ; preds = %592
  %607 = sext i32 %593 to i64
  %608 = sext i32 %599 to i64
  %609 = sub nsw i64 %608, %607
  %610 = add nsw i64 %608, -1
  %611 = and i64 %609, 1
  %612 = icmp eq i64 %611, 0
  br i1 %612, label %629, label %613

; <label>:613:                                    ; preds = %606
  %614 = getelementptr inbounds i32, i32* %35, i64 %607
  %615 = load i32, i32* %614, align 4, !tbaa !29
  %616 = getelementptr inbounds double, double* %38, i64 %607
  %617 = load double, double* %616, align 8, !tbaa !30
  %618 = shl nsw i32 %615, 1
  %619 = sext i32 %618 to i64
  %620 = getelementptr inbounds double, double* %57, i64 %619
  %621 = insertelement <2 x double> undef, double %617, i32 0
  %622 = shufflevector <2 x double> %621, <2 x double> undef, <2 x i32> zeroinitializer
  %623 = fmul <2 x double> %604, %622
  %624 = bitcast double* %620 to <2 x double>*
  %625 = load <2 x double>, <2 x double>* %624, align 8, !tbaa !30
  %626 = fsub <2 x double> %625, %623
  %627 = bitcast double* %620 to <2 x double>*
  store <2 x double> %626, <2 x double>* %627, align 8, !tbaa !30
  %628 = add nsw i64 %607, 1
  br label %629

; <label>:629:                                    ; preds = %613, %606
  %630 = phi i64 [ %628, %613 ], [ %607, %606 ]
  %631 = icmp eq i64 %610, %607
  br i1 %631, label %666, label %632

; <label>:632:                                    ; preds = %629
  br label %633

; <label>:633:                                    ; preds = %633, %632
  %634 = phi i64 [ %630, %632 ], [ %664, %633 ]
  %635 = getelementptr inbounds i32, i32* %35, i64 %634
  %636 = load i32, i32* %635, align 4, !tbaa !29
  %637 = getelementptr inbounds double, double* %38, i64 %634
  %638 = load double, double* %637, align 8, !tbaa !30
  %639 = shl nsw i32 %636, 1
  %640 = sext i32 %639 to i64
  %641 = getelementptr inbounds double, double* %57, i64 %640
  %642 = insertelement <2 x double> undef, double %638, i32 0
  %643 = shufflevector <2 x double> %642, <2 x double> undef, <2 x i32> zeroinitializer
  %644 = fmul <2 x double> %604, %643
  %645 = bitcast double* %641 to <2 x double>*
  %646 = load <2 x double>, <2 x double>* %645, align 8, !tbaa !30
  %647 = fsub <2 x double> %646, %644
  %648 = bitcast double* %641 to <2 x double>*
  store <2 x double> %647, <2 x double>* %648, align 8, !tbaa !30
  %649 = add nsw i64 %634, 1
  %650 = getelementptr inbounds i32, i32* %35, i64 %649
  %651 = load i32, i32* %650, align 4, !tbaa !29
  %652 = getelementptr inbounds double, double* %38, i64 %649
  %653 = load double, double* %652, align 8, !tbaa !30
  %654 = shl nsw i32 %651, 1
  %655 = sext i32 %654 to i64
  %656 = getelementptr inbounds double, double* %57, i64 %655
  %657 = insertelement <2 x double> undef, double %653, i32 0
  %658 = shufflevector <2 x double> %657, <2 x double> undef, <2 x i32> zeroinitializer
  %659 = fmul <2 x double> %604, %658
  %660 = bitcast double* %656 to <2 x double>*
  %661 = load <2 x double>, <2 x double>* %660, align 8, !tbaa !30
  %662 = fsub <2 x double> %661, %659
  %663 = bitcast double* %656 to <2 x double>*
  store <2 x double> %662, <2 x double>* %663, align 8, !tbaa !30
  %664 = add nsw i64 %634, 2
  %665 = icmp eq i64 %664, %608
  br i1 %665, label %666, label %633

; <label>:666:                                    ; preds = %629, %633, %592
  %667 = icmp eq i64 %596, %591
  br i1 %667, label %777, label %592

; <label>:668:                                    ; preds = %523
  %669 = icmp sgt i32 %455, %453
  br i1 %669, label %670, label %777

; <label>:670:                                    ; preds = %668
  %671 = getelementptr inbounds i32, i32* %33, i64 %458
  %672 = load i32, i32* %671, align 4, !tbaa !29
  %673 = sext i32 %455 to i64
  br label %674

; <label>:674:                                    ; preds = %719, %670
  %675 = phi i32 [ %672, %670 ], [ %679, %719 ]
  %676 = phi i64 [ %458, %670 ], [ %677, %719 ]
  %677 = add nsw i64 %676, 1
  %678 = getelementptr inbounds i32, i32* %33, i64 %677
  %679 = load i32, i32* %678, align 4, !tbaa !29
  %680 = mul nsw i64 %676, 3
  %681 = getelementptr inbounds double, double* %57, i64 %680
  %682 = load double, double* %681, align 8, !tbaa !30
  %683 = add nsw i64 %680, 1
  %684 = getelementptr inbounds double, double* %57, i64 %683
  %685 = load double, double* %684, align 8, !tbaa !30
  %686 = add nsw i64 %680, 2
  %687 = getelementptr inbounds double, double* %57, i64 %686
  %688 = load double, double* %687, align 8, !tbaa !30
  %689 = icmp slt i32 %675, %679
  br i1 %689, label %690, label %719

; <label>:690:                                    ; preds = %674
  %691 = sext i32 %675 to i64
  %692 = sext i32 %679 to i64
  br label %693

; <label>:693:                                    ; preds = %693, %690
  %694 = phi i64 [ %691, %690 ], [ %717, %693 ]
  %695 = getelementptr inbounds i32, i32* %35, i64 %694
  %696 = load i32, i32* %695, align 4, !tbaa !29
  %697 = getelementptr inbounds double, double* %38, i64 %694
  %698 = load double, double* %697, align 8, !tbaa !30
  %699 = fmul double %682, %698
  %700 = mul nsw i32 %696, 3
  %701 = sext i32 %700 to i64
  %702 = getelementptr inbounds double, double* %57, i64 %701
  %703 = load double, double* %702, align 8, !tbaa !30
  %704 = fsub double %703, %699
  store double %704, double* %702, align 8, !tbaa !30
  %705 = fmul double %685, %698
  %706 = add nsw i32 %700, 1
  %707 = sext i32 %706 to i64
  %708 = getelementptr inbounds double, double* %57, i64 %707
  %709 = load double, double* %708, align 8, !tbaa !30
  %710 = fsub double %709, %705
  store double %710, double* %708, align 8, !tbaa !30
  %711 = fmul double %688, %698
  %712 = add nsw i32 %700, 2
  %713 = sext i32 %712 to i64
  %714 = getelementptr inbounds double, double* %57, i64 %713
  %715 = load double, double* %714, align 8, !tbaa !30
  %716 = fsub double %715, %711
  store double %716, double* %714, align 8, !tbaa !30
  %717 = add nsw i64 %694, 1
  %718 = icmp eq i64 %717, %692
  br i1 %718, label %719, label %693

; <label>:719:                                    ; preds = %693, %674
  %720 = icmp eq i64 %677, %673
  br i1 %720, label %777, label %674

; <label>:721:                                    ; preds = %523
  %722 = icmp sgt i32 %455, %453
  br i1 %722, label %723, label %777

; <label>:723:                                    ; preds = %721
  %724 = getelementptr inbounds i32, i32* %33, i64 %458
  %725 = load i32, i32* %724, align 4, !tbaa !29
  %726 = sext i32 %455 to i64
  br label %727

; <label>:727:                                    ; preds = %775, %723
  %728 = phi i32 [ %725, %723 ], [ %734, %775 ]
  %729 = phi i64 [ %458, %723 ], [ %731, %775 ]
  %730 = phi i32 [ %453, %723 ], [ %732, %775 ]
  %731 = add nsw i64 %729, 1
  %732 = add nsw i32 %730, 1
  %733 = getelementptr inbounds i32, i32* %33, i64 %731
  %734 = load i32, i32* %733, align 4, !tbaa !29
  %735 = shl nsw i32 %730, 2
  %736 = sext i32 %735 to i64
  %737 = getelementptr inbounds double, double* %57, i64 %736
  %738 = bitcast double* %737 to <2 x double>*
  %739 = load <2 x double>, <2 x double>* %738, align 8, !tbaa !30
  %740 = or i32 %735, 2
  %741 = sext i32 %740 to i64
  %742 = getelementptr inbounds double, double* %57, i64 %741
  %743 = bitcast double* %742 to <2 x double>*
  %744 = load <2 x double>, <2 x double>* %743, align 8, !tbaa !30
  %745 = icmp slt i32 %728, %734
  br i1 %745, label %746, label %775

; <label>:746:                                    ; preds = %727
  %747 = sext i32 %728 to i64
  %748 = sext i32 %734 to i64
  br label %749

; <label>:749:                                    ; preds = %749, %746
  %750 = phi i64 [ %747, %746 ], [ %773, %749 ]
  %751 = getelementptr inbounds i32, i32* %35, i64 %750
  %752 = load i32, i32* %751, align 4, !tbaa !29
  %753 = getelementptr inbounds double, double* %38, i64 %750
  %754 = load double, double* %753, align 8, !tbaa !30
  %755 = shl nsw i32 %752, 2
  %756 = sext i32 %755 to i64
  %757 = getelementptr inbounds double, double* %57, i64 %756
  %758 = insertelement <2 x double> undef, double %754, i32 0
  %759 = shufflevector <2 x double> %758, <2 x double> undef, <2 x i32> zeroinitializer
  %760 = fmul <2 x double> %739, %759
  %761 = bitcast double* %757 to <2 x double>*
  %762 = load <2 x double>, <2 x double>* %761, align 8, !tbaa !30
  %763 = fsub <2 x double> %762, %760
  %764 = bitcast double* %757 to <2 x double>*
  store <2 x double> %763, <2 x double>* %764, align 8, !tbaa !30
  %765 = or i32 %755, 2
  %766 = sext i32 %765 to i64
  %767 = getelementptr inbounds double, double* %57, i64 %766
  %768 = fmul <2 x double> %744, %759
  %769 = bitcast double* %767 to <2 x double>*
  %770 = load <2 x double>, <2 x double>* %769, align 8, !tbaa !30
  %771 = fsub <2 x double> %770, %768
  %772 = bitcast double* %767 to <2 x double>*
  store <2 x double> %771, <2 x double>* %772, align 8, !tbaa !30
  %773 = add nsw i64 %750, 1
  %774 = icmp eq i64 %773, %748
  br i1 %774, label %775, label %749

; <label>:775:                                    ; preds = %749, %727
  %776 = icmp eq i64 %731, %726
  br i1 %776, label %777, label %727

; <label>:777:                                    ; preds = %775, %719, %666, %584, %721, %668, %586, %524, %523
  br i1 %522, label %449, label %778

; <label>:778:                                    ; preds = %521, %777, %447
  switch i32 %101, label %983 [
    i32 1, label %779
    i32 2, label %823
    i32 3, label %867
    i32 4, label %902
  ]

; <label>:779:                                    ; preds = %778
  br i1 %62, label %780, label %983

; <label>:780:                                    ; preds = %779
  br i1 %87, label %946, label %781

; <label>:781:                                    ; preds = %780
  br label %782

; <label>:782:                                    ; preds = %782, %781
  %783 = phi i64 [ 0, %781 ], [ %820, %782 ]
  %784 = phi i64 [ %92, %781 ], [ %821, %782 ]
  %785 = getelementptr inbounds double, double* %57, i64 %783
  %786 = bitcast double* %785 to i64*
  %787 = load i64, i64* %786, align 8, !tbaa !30
  %788 = getelementptr inbounds i32, i32* %27, i64 %783
  %789 = load i32, i32* %788, align 4, !tbaa !29
  %790 = sext i32 %789 to i64
  %791 = getelementptr inbounds double, double* %98, i64 %790
  %792 = bitcast double* %791 to i64*
  store i64 %787, i64* %792, align 8, !tbaa !30
  %793 = or i64 %783, 1
  %794 = getelementptr inbounds double, double* %57, i64 %793
  %795 = bitcast double* %794 to i64*
  %796 = load i64, i64* %795, align 8, !tbaa !30
  %797 = getelementptr inbounds i32, i32* %27, i64 %793
  %798 = load i32, i32* %797, align 4, !tbaa !29
  %799 = sext i32 %798 to i64
  %800 = getelementptr inbounds double, double* %98, i64 %799
  %801 = bitcast double* %800 to i64*
  store i64 %796, i64* %801, align 8, !tbaa !30
  %802 = or i64 %783, 2
  %803 = getelementptr inbounds double, double* %57, i64 %802
  %804 = bitcast double* %803 to i64*
  %805 = load i64, i64* %804, align 8, !tbaa !30
  %806 = getelementptr inbounds i32, i32* %27, i64 %802
  %807 = load i32, i32* %806, align 4, !tbaa !29
  %808 = sext i32 %807 to i64
  %809 = getelementptr inbounds double, double* %98, i64 %808
  %810 = bitcast double* %809 to i64*
  store i64 %805, i64* %810, align 8, !tbaa !30
  %811 = or i64 %783, 3
  %812 = getelementptr inbounds double, double* %57, i64 %811
  %813 = bitcast double* %812 to i64*
  %814 = load i64, i64* %813, align 8, !tbaa !30
  %815 = getelementptr inbounds i32, i32* %27, i64 %811
  %816 = load i32, i32* %815, align 4, !tbaa !29
  %817 = sext i32 %816 to i64
  %818 = getelementptr inbounds double, double* %98, i64 %817
  %819 = bitcast double* %818 to i64*
  store i64 %814, i64* %819, align 8, !tbaa !30
  %820 = add nuw nsw i64 %783, 4
  %821 = add i64 %784, -4
  %822 = icmp eq i64 %821, 0
  br i1 %822, label %946, label %782

; <label>:823:                                    ; preds = %778
  br i1 %62, label %824, label %983

; <label>:824:                                    ; preds = %823
  br i1 %89, label %963, label %825

; <label>:825:                                    ; preds = %824
  br label %826

; <label>:826:                                    ; preds = %826, %825
  %827 = phi i64 [ 0, %825 ], [ %864, %826 ]
  %828 = phi i64 [ %94, %825 ], [ %865, %826 ]
  %829 = getelementptr inbounds i32, i32* %27, i64 %827
  %830 = load i32, i32* %829, align 4, !tbaa !29
  %831 = shl nuw nsw i64 %827, 1
  %832 = getelementptr inbounds double, double* %57, i64 %831
  %833 = bitcast double* %832 to i64*
  %834 = load i64, i64* %833, align 8, !tbaa !30
  %835 = sext i32 %830 to i64
  %836 = getelementptr inbounds double, double* %98, i64 %835
  %837 = bitcast double* %836 to i64*
  store i64 %834, i64* %837, align 8, !tbaa !30
  %838 = or i64 %831, 1
  %839 = getelementptr inbounds double, double* %57, i64 %838
  %840 = bitcast double* %839 to i64*
  %841 = load i64, i64* %840, align 8, !tbaa !30
  %842 = add nsw i32 %830, %2
  %843 = sext i32 %842 to i64
  %844 = getelementptr inbounds double, double* %98, i64 %843
  %845 = bitcast double* %844 to i64*
  store i64 %841, i64* %845, align 8, !tbaa !30
  %846 = or i64 %827, 1
  %847 = getelementptr inbounds i32, i32* %27, i64 %846
  %848 = load i32, i32* %847, align 4, !tbaa !29
  %849 = shl nuw nsw i64 %846, 1
  %850 = getelementptr inbounds double, double* %57, i64 %849
  %851 = bitcast double* %850 to i64*
  %852 = load i64, i64* %851, align 8, !tbaa !30
  %853 = sext i32 %848 to i64
  %854 = getelementptr inbounds double, double* %98, i64 %853
  %855 = bitcast double* %854 to i64*
  store i64 %852, i64* %855, align 8, !tbaa !30
  %856 = or i64 %849, 1
  %857 = getelementptr inbounds double, double* %57, i64 %856
  %858 = bitcast double* %857 to i64*
  %859 = load i64, i64* %858, align 8, !tbaa !30
  %860 = add nsw i32 %848, %2
  %861 = sext i32 %860 to i64
  %862 = getelementptr inbounds double, double* %98, i64 %861
  %863 = bitcast double* %862 to i64*
  store i64 %859, i64* %863, align 8, !tbaa !30
  %864 = add nuw nsw i64 %827, 2
  %865 = add i64 %828, -2
  %866 = icmp eq i64 %865, 0
  br i1 %866, label %963, label %826

; <label>:867:                                    ; preds = %778
  br i1 %62, label %868, label %983

; <label>:868:                                    ; preds = %867
  br label %869

; <label>:869:                                    ; preds = %868, %869
  %870 = phi i64 [ %900, %869 ], [ 0, %868 ]
  %871 = getelementptr inbounds i32, i32* %27, i64 %870
  %872 = load i32, i32* %871, align 4, !tbaa !29
  %873 = trunc i64 %870 to i32
  %874 = mul nsw i32 %873, 3
  %875 = zext i32 %874 to i64
  %876 = getelementptr inbounds double, double* %57, i64 %875
  %877 = bitcast double* %876 to i64*
  %878 = load i64, i64* %877, align 8, !tbaa !30
  %879 = sext i32 %872 to i64
  %880 = getelementptr inbounds double, double* %98, i64 %879
  %881 = bitcast double* %880 to i64*
  store i64 %878, i64* %881, align 8, !tbaa !30
  %882 = add nuw nsw i32 %874, 1
  %883 = zext i32 %882 to i64
  %884 = getelementptr inbounds double, double* %57, i64 %883
  %885 = bitcast double* %884 to i64*
  %886 = load i64, i64* %885, align 8, !tbaa !30
  %887 = add nsw i32 %872, %2
  %888 = sext i32 %887 to i64
  %889 = getelementptr inbounds double, double* %98, i64 %888
  %890 = bitcast double* %889 to i64*
  store i64 %886, i64* %890, align 8, !tbaa !30
  %891 = add nuw nsw i32 %874, 2
  %892 = zext i32 %891 to i64
  %893 = getelementptr inbounds double, double* %57, i64 %892
  %894 = bitcast double* %893 to i64*
  %895 = load i64, i64* %894, align 8, !tbaa !30
  %896 = add nsw i32 %872, %63
  %897 = sext i32 %896 to i64
  %898 = getelementptr inbounds double, double* %98, i64 %897
  %899 = bitcast double* %898 to i64*
  store i64 %895, i64* %899, align 8, !tbaa !30
  %900 = add nuw nsw i64 %870, 1
  %901 = icmp eq i64 %900, %73
  br i1 %901, label %983, label %869

; <label>:902:                                    ; preds = %778
  br i1 %62, label %903, label %983

; <label>:903:                                    ; preds = %902
  br label %904

; <label>:904:                                    ; preds = %903, %904
  %905 = phi i64 [ %944, %904 ], [ 0, %903 ]
  %906 = getelementptr inbounds i32, i32* %27, i64 %905
  %907 = load i32, i32* %906, align 4, !tbaa !29
  %908 = trunc i64 %905 to i32
  %909 = shl nsw i32 %908, 2
  %910 = zext i32 %909 to i64
  %911 = getelementptr inbounds double, double* %57, i64 %910
  %912 = bitcast double* %911 to i64*
  %913 = load i64, i64* %912, align 8, !tbaa !30
  %914 = sext i32 %907 to i64
  %915 = getelementptr inbounds double, double* %98, i64 %914
  %916 = bitcast double* %915 to i64*
  store i64 %913, i64* %916, align 8, !tbaa !30
  %917 = or i32 %909, 1
  %918 = zext i32 %917 to i64
  %919 = getelementptr inbounds double, double* %57, i64 %918
  %920 = bitcast double* %919 to i64*
  %921 = load i64, i64* %920, align 8, !tbaa !30
  %922 = add nsw i32 %907, %2
  %923 = sext i32 %922 to i64
  %924 = getelementptr inbounds double, double* %98, i64 %923
  %925 = bitcast double* %924 to i64*
  store i64 %921, i64* %925, align 8, !tbaa !30
  %926 = or i32 %909, 2
  %927 = zext i32 %926 to i64
  %928 = getelementptr inbounds double, double* %57, i64 %927
  %929 = bitcast double* %928 to i64*
  %930 = load i64, i64* %929, align 8, !tbaa !30
  %931 = add nsw i32 %907, %63
  %932 = sext i32 %931 to i64
  %933 = getelementptr inbounds double, double* %98, i64 %932
  %934 = bitcast double* %933 to i64*
  store i64 %930, i64* %934, align 8, !tbaa !30
  %935 = or i32 %909, 3
  %936 = zext i32 %935 to i64
  %937 = getelementptr inbounds double, double* %57, i64 %936
  %938 = bitcast double* %937 to i64*
  %939 = load i64, i64* %938, align 8, !tbaa !30
  %940 = add nsw i32 %907, %64
  %941 = sext i32 %940 to i64
  %942 = getelementptr inbounds double, double* %98, i64 %941
  %943 = bitcast double* %942 to i64*
  store i64 %939, i64* %943, align 8, !tbaa !30
  %944 = add nuw nsw i64 %905, 1
  %945 = icmp eq i64 %944, %74
  br i1 %945, label %983, label %904

; <label>:946:                                    ; preds = %782, %780
  %947 = phi i64 [ 0, %780 ], [ %820, %782 ]
  br i1 %93, label %983, label %948

; <label>:948:                                    ; preds = %946
  br label %949

; <label>:949:                                    ; preds = %949, %948
  %950 = phi i64 [ %960, %949 ], [ %947, %948 ]
  %951 = phi i64 [ %961, %949 ], [ %86, %948 ]
  %952 = getelementptr inbounds double, double* %57, i64 %950
  %953 = bitcast double* %952 to i64*
  %954 = load i64, i64* %953, align 8, !tbaa !30
  %955 = getelementptr inbounds i32, i32* %27, i64 %950
  %956 = load i32, i32* %955, align 4, !tbaa !29
  %957 = sext i32 %956 to i64
  %958 = getelementptr inbounds double, double* %98, i64 %957
  %959 = bitcast double* %958 to i64*
  store i64 %954, i64* %959, align 8, !tbaa !30
  %960 = add nuw nsw i64 %950, 1
  %961 = add i64 %951, -1
  %962 = icmp eq i64 %961, 0
  br i1 %962, label %983, label %949, !llvm.loop !34

; <label>:963:                                    ; preds = %826, %824
  %964 = phi i64 [ 0, %824 ], [ %864, %826 ]
  br i1 %95, label %983, label %965

; <label>:965:                                    ; preds = %963
  %966 = getelementptr inbounds i32, i32* %27, i64 %964
  %967 = load i32, i32* %966, align 4, !tbaa !29
  %968 = shl nuw nsw i64 %964, 1
  %969 = getelementptr inbounds double, double* %57, i64 %968
  %970 = bitcast double* %969 to i64*
  %971 = load i64, i64* %970, align 8, !tbaa !30
  %972 = sext i32 %967 to i64
  %973 = getelementptr inbounds double, double* %98, i64 %972
  %974 = bitcast double* %973 to i64*
  store i64 %971, i64* %974, align 8, !tbaa !30
  %975 = or i64 %968, 1
  %976 = getelementptr inbounds double, double* %57, i64 %975
  %977 = bitcast double* %976 to i64*
  %978 = load i64, i64* %977, align 8, !tbaa !30
  %979 = add nsw i32 %967, %2
  %980 = sext i32 %979 to i64
  %981 = getelementptr inbounds double, double* %98, i64 %980
  %982 = bitcast double* %981 to i64*
  store i64 %978, i64* %982, align 8, !tbaa !30
  br label %983

; <label>:983:                                    ; preds = %904, %869, %965, %963, %946, %949, %902, %867, %823, %779, %778
  %984 = getelementptr inbounds double, double* %98, i64 %66
  %985 = add nuw nsw i32 %97, 4
  %986 = icmp slt i32 %985, %3
  br i1 %986, label %96, label %987

; <label>:987:                                    ; preds = %983, %22, %6, %20
  %988 = phi i32 [ 0, %20 ], [ 0, %6 ], [ 1, %22 ], [ 1, %983 ]
  ret i32 %988
}

declare void @klu_lsolve(i32, i32*, i32*, double*, i32, double*) local_unnamed_addr #1

declare void @klu_usolve(i32, i32*, i32*, double*, double*, i32, double*) local_unnamed_addr #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !9, i64 40}
!4 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !8, i64 32, !9, i64 40, !9, i64 44, !8, i64 48, !8, i64 56, !8, i64 64, !9, i64 72, !9, i64 76, !9, i64 80, !9, i64 84, !9, i64 88, !9, i64 92}
!5 = !{!"double", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!"any pointer", !6, i64 0}
!9 = !{!"int", !6, i64 0}
!10 = !{!11, !9, i64 76}
!11 = !{!"klu_common_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !9, i64 40, !9, i64 44, !9, i64 48, !8, i64 56, !8, i64 64, !9, i64 72, !9, i64 76, !9, i64 80, !9, i64 84, !9, i64 88, !9, i64 92, !9, i64 96, !5, i64 104, !5, i64 112, !5, i64 120, !5, i64 128, !5, i64 136, !12, i64 144, !12, i64 152}
!12 = !{!"long", !6, i64 0}
!13 = !{!4, !9, i64 76}
!14 = !{!4, !8, i64 56}
!15 = !{!4, !8, i64 64}
!16 = !{!17, !8, i64 24}
!17 = !{!"", !9, i64 0, !9, i64 4, !9, i64 8, !9, i64 12, !9, i64 16, !9, i64 20, !8, i64 24, !8, i64 32, !8, i64 40, !8, i64 48, !8, i64 56, !8, i64 64, !8, i64 72, !8, i64 80, !8, i64 88, !8, i64 96, !12, i64 104, !8, i64 112, !8, i64 120, !8, i64 128, !8, i64 136, !8, i64 144, !8, i64 152, !9, i64 160}
!18 = !{!17, !8, i64 136}
!19 = !{!17, !8, i64 144}
!20 = !{!17, !8, i64 152}
!21 = !{!17, !8, i64 40}
!22 = !{!17, !8, i64 56}
!23 = !{!17, !8, i64 48}
!24 = !{!17, !8, i64 64}
!25 = !{!17, !8, i64 72}
!26 = !{!17, !8, i64 88}
!27 = !{!17, !8, i64 96}
!28 = !{!17, !8, i64 120}
!29 = !{!9, !9, i64 0}
!30 = !{!5, !5, i64 0}
!31 = distinct !{!31, !32}
!32 = !{!"llvm.loop.unroll.disable"}
!33 = !{!8, !8, i64 0}
!34 = distinct !{!34, !32}
