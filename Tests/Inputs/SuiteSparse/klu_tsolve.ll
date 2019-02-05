; ModuleID = 'klu_tsolve.c'
source_filename = "klu_tsolve.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_tsolve(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %7 = icmp eq %struct.klu_common_struct* %5, null
  br i1 %7, label %1005, label %8

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
  br label %1005

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
  br i1 %58, label %59, label %1005

; <label>:59:                                     ; preds = %22
  %60 = icmp sgt i32 %25, 0
  %61 = icmp sgt i32 %14, 0
  %62 = icmp eq double* %54, null
  %63 = shl i32 %2, 1
  %64 = mul nsw i32 %2, 3
  %65 = shl nsw i32 %2, 2
  %66 = sext i32 %65 to i64
  %67 = zext i32 %25 to i64
  %68 = zext i32 %14 to i64
  %69 = zext i32 %14 to i64
  %70 = zext i32 %14 to i64
  %71 = zext i32 %14 to i64
  %72 = zext i32 %14 to i64
  %73 = zext i32 %14 to i64
  %74 = zext i32 %14 to i64
  %75 = add nsw i64 %69, -1
  %76 = and i64 %69, 3
  %77 = icmp ult i64 %75, 3
  %78 = and i64 %69, 1
  %79 = icmp eq i64 %75, 0
  %80 = sub nsw i64 %69, %76
  %81 = icmp eq i64 %76, 0
  %82 = sub nsw i64 %69, %78
  %83 = icmp eq i64 %78, 0
  %84 = and i64 %69, 3
  %85 = icmp ult i64 %75, 3
  %86 = and i64 %69, 1
  %87 = icmp eq i64 %75, 0
  %88 = and i64 %69, 1
  %89 = icmp eq i64 %75, 0
  %90 = sub nsw i64 %69, %84
  %91 = icmp eq i64 %84, 0
  %92 = sub nsw i64 %69, %86
  %93 = icmp eq i64 %86, 0
  %94 = sub nsw i64 %69, %88
  %95 = icmp eq i64 %88, 0
  br label %96

; <label>:96:                                     ; preds = %59, %1001
  %97 = phi i32 [ 0, %59 ], [ %1003, %1001 ]
  %98 = phi double* [ %4, %59 ], [ %1002, %1001 ]
  %99 = sub nsw i32 %3, %97
  %100 = icmp slt i32 %99, 4
  %101 = select i1 %100, i32 %99, i32 4
  switch i32 %101, label %306 [
    i32 1, label %102
    i32 2, label %146
    i32 3, label %190
    i32 4, label %225
  ]

; <label>:102:                                    ; preds = %96
  br i1 %61, label %103, label %306

; <label>:103:                                    ; preds = %102
  br i1 %77, label %269, label %104

; <label>:104:                                    ; preds = %103
  br label %105

; <label>:105:                                    ; preds = %105, %104
  %106 = phi i64 [ 0, %104 ], [ %143, %105 ]
  %107 = phi i64 [ %80, %104 ], [ %144, %105 ]
  %108 = getelementptr inbounds i32, i32* %27, i64 %106
  %109 = load i32, i32* %108, align 4, !tbaa !29
  %110 = sext i32 %109 to i64
  %111 = getelementptr inbounds double, double* %98, i64 %110
  %112 = bitcast double* %111 to i64*
  %113 = load i64, i64* %112, align 8, !tbaa !30
  %114 = getelementptr inbounds double, double* %57, i64 %106
  %115 = bitcast double* %114 to i64*
  store i64 %113, i64* %115, align 8, !tbaa !30
  %116 = or i64 %106, 1
  %117 = getelementptr inbounds i32, i32* %27, i64 %116
  %118 = load i32, i32* %117, align 4, !tbaa !29
  %119 = sext i32 %118 to i64
  %120 = getelementptr inbounds double, double* %98, i64 %119
  %121 = bitcast double* %120 to i64*
  %122 = load i64, i64* %121, align 8, !tbaa !30
  %123 = getelementptr inbounds double, double* %57, i64 %116
  %124 = bitcast double* %123 to i64*
  store i64 %122, i64* %124, align 8, !tbaa !30
  %125 = or i64 %106, 2
  %126 = getelementptr inbounds i32, i32* %27, i64 %125
  %127 = load i32, i32* %126, align 4, !tbaa !29
  %128 = sext i32 %127 to i64
  %129 = getelementptr inbounds double, double* %98, i64 %128
  %130 = bitcast double* %129 to i64*
  %131 = load i64, i64* %130, align 8, !tbaa !30
  %132 = getelementptr inbounds double, double* %57, i64 %125
  %133 = bitcast double* %132 to i64*
  store i64 %131, i64* %133, align 8, !tbaa !30
  %134 = or i64 %106, 3
  %135 = getelementptr inbounds i32, i32* %27, i64 %134
  %136 = load i32, i32* %135, align 4, !tbaa !29
  %137 = sext i32 %136 to i64
  %138 = getelementptr inbounds double, double* %98, i64 %137
  %139 = bitcast double* %138 to i64*
  %140 = load i64, i64* %139, align 8, !tbaa !30
  %141 = getelementptr inbounds double, double* %57, i64 %134
  %142 = bitcast double* %141 to i64*
  store i64 %140, i64* %142, align 8, !tbaa !30
  %143 = add nuw nsw i64 %106, 4
  %144 = add i64 %107, -4
  %145 = icmp eq i64 %144, 0
  br i1 %145, label %269, label %105

; <label>:146:                                    ; preds = %96
  br i1 %61, label %147, label %306

; <label>:147:                                    ; preds = %146
  br i1 %79, label %286, label %148

; <label>:148:                                    ; preds = %147
  br label %149

; <label>:149:                                    ; preds = %149, %148
  %150 = phi i64 [ 0, %148 ], [ %187, %149 ]
  %151 = phi i64 [ %82, %148 ], [ %188, %149 ]
  %152 = getelementptr inbounds i32, i32* %27, i64 %150
  %153 = load i32, i32* %152, align 4, !tbaa !29
  %154 = sext i32 %153 to i64
  %155 = getelementptr inbounds double, double* %98, i64 %154
  %156 = bitcast double* %155 to i64*
  %157 = load i64, i64* %156, align 8, !tbaa !30
  %158 = shl nuw nsw i64 %150, 1
  %159 = getelementptr inbounds double, double* %57, i64 %158
  %160 = bitcast double* %159 to i64*
  store i64 %157, i64* %160, align 8, !tbaa !30
  %161 = add nsw i32 %153, %2
  %162 = sext i32 %161 to i64
  %163 = getelementptr inbounds double, double* %98, i64 %162
  %164 = bitcast double* %163 to i64*
  %165 = load i64, i64* %164, align 8, !tbaa !30
  %166 = or i64 %158, 1
  %167 = getelementptr inbounds double, double* %57, i64 %166
  %168 = bitcast double* %167 to i64*
  store i64 %165, i64* %168, align 8, !tbaa !30
  %169 = or i64 %150, 1
  %170 = getelementptr inbounds i32, i32* %27, i64 %169
  %171 = load i32, i32* %170, align 4, !tbaa !29
  %172 = sext i32 %171 to i64
  %173 = getelementptr inbounds double, double* %98, i64 %172
  %174 = bitcast double* %173 to i64*
  %175 = load i64, i64* %174, align 8, !tbaa !30
  %176 = shl nuw nsw i64 %169, 1
  %177 = getelementptr inbounds double, double* %57, i64 %176
  %178 = bitcast double* %177 to i64*
  store i64 %175, i64* %178, align 8, !tbaa !30
  %179 = add nsw i32 %171, %2
  %180 = sext i32 %179 to i64
  %181 = getelementptr inbounds double, double* %98, i64 %180
  %182 = bitcast double* %181 to i64*
  %183 = load i64, i64* %182, align 8, !tbaa !30
  %184 = or i64 %176, 1
  %185 = getelementptr inbounds double, double* %57, i64 %184
  %186 = bitcast double* %185 to i64*
  store i64 %183, i64* %186, align 8, !tbaa !30
  %187 = add nuw nsw i64 %150, 2
  %188 = add i64 %151, -2
  %189 = icmp eq i64 %188, 0
  br i1 %189, label %286, label %149

; <label>:190:                                    ; preds = %96
  br i1 %61, label %191, label %306

; <label>:191:                                    ; preds = %190
  br label %192

; <label>:192:                                    ; preds = %191, %192
  %193 = phi i64 [ %223, %192 ], [ 0, %191 ]
  %194 = getelementptr inbounds i32, i32* %27, i64 %193
  %195 = load i32, i32* %194, align 4, !tbaa !29
  %196 = sext i32 %195 to i64
  %197 = getelementptr inbounds double, double* %98, i64 %196
  %198 = bitcast double* %197 to i64*
  %199 = load i64, i64* %198, align 8, !tbaa !30
  %200 = trunc i64 %193 to i32
  %201 = mul nsw i32 %200, 3
  %202 = zext i32 %201 to i64
  %203 = getelementptr inbounds double, double* %57, i64 %202
  %204 = bitcast double* %203 to i64*
  store i64 %199, i64* %204, align 8, !tbaa !30
  %205 = add nsw i32 %195, %2
  %206 = sext i32 %205 to i64
  %207 = getelementptr inbounds double, double* %98, i64 %206
  %208 = bitcast double* %207 to i64*
  %209 = load i64, i64* %208, align 8, !tbaa !30
  %210 = add nuw nsw i32 %201, 1
  %211 = zext i32 %210 to i64
  %212 = getelementptr inbounds double, double* %57, i64 %211
  %213 = bitcast double* %212 to i64*
  store i64 %209, i64* %213, align 8, !tbaa !30
  %214 = add nsw i32 %195, %63
  %215 = sext i32 %214 to i64
  %216 = getelementptr inbounds double, double* %98, i64 %215
  %217 = bitcast double* %216 to i64*
  %218 = load i64, i64* %217, align 8, !tbaa !30
  %219 = add nuw nsw i32 %201, 2
  %220 = zext i32 %219 to i64
  %221 = getelementptr inbounds double, double* %57, i64 %220
  %222 = bitcast double* %221 to i64*
  store i64 %218, i64* %222, align 8, !tbaa !30
  %223 = add nuw nsw i64 %193, 1
  %224 = icmp eq i64 %223, %68
  br i1 %224, label %306, label %192

; <label>:225:                                    ; preds = %96
  br i1 %61, label %226, label %306

; <label>:226:                                    ; preds = %225
  br label %227

; <label>:227:                                    ; preds = %226, %227
  %228 = phi i64 [ %267, %227 ], [ 0, %226 ]
  %229 = getelementptr inbounds i32, i32* %27, i64 %228
  %230 = load i32, i32* %229, align 4, !tbaa !29
  %231 = sext i32 %230 to i64
  %232 = getelementptr inbounds double, double* %98, i64 %231
  %233 = bitcast double* %232 to i64*
  %234 = load i64, i64* %233, align 8, !tbaa !30
  %235 = trunc i64 %228 to i32
  %236 = shl nsw i32 %235, 2
  %237 = zext i32 %236 to i64
  %238 = getelementptr inbounds double, double* %57, i64 %237
  %239 = bitcast double* %238 to i64*
  store i64 %234, i64* %239, align 8, !tbaa !30
  %240 = add nsw i32 %230, %2
  %241 = sext i32 %240 to i64
  %242 = getelementptr inbounds double, double* %98, i64 %241
  %243 = bitcast double* %242 to i64*
  %244 = load i64, i64* %243, align 8, !tbaa !30
  %245 = or i32 %236, 1
  %246 = zext i32 %245 to i64
  %247 = getelementptr inbounds double, double* %57, i64 %246
  %248 = bitcast double* %247 to i64*
  store i64 %244, i64* %248, align 8, !tbaa !30
  %249 = add nsw i32 %230, %63
  %250 = sext i32 %249 to i64
  %251 = getelementptr inbounds double, double* %98, i64 %250
  %252 = bitcast double* %251 to i64*
  %253 = load i64, i64* %252, align 8, !tbaa !30
  %254 = or i32 %236, 2
  %255 = zext i32 %254 to i64
  %256 = getelementptr inbounds double, double* %57, i64 %255
  %257 = bitcast double* %256 to i64*
  store i64 %253, i64* %257, align 8, !tbaa !30
  %258 = add nsw i32 %230, %64
  %259 = sext i32 %258 to i64
  %260 = getelementptr inbounds double, double* %98, i64 %259
  %261 = bitcast double* %260 to i64*
  %262 = load i64, i64* %261, align 8, !tbaa !30
  %263 = or i32 %236, 3
  %264 = zext i32 %263 to i64
  %265 = getelementptr inbounds double, double* %57, i64 %264
  %266 = bitcast double* %265 to i64*
  store i64 %262, i64* %266, align 8, !tbaa !30
  %267 = add nuw nsw i64 %228, 1
  %268 = icmp eq i64 %267, %69
  br i1 %268, label %306, label %227

; <label>:269:                                    ; preds = %105, %103
  %270 = phi i64 [ 0, %103 ], [ %143, %105 ]
  br i1 %81, label %306, label %271

; <label>:271:                                    ; preds = %269
  br label %272

; <label>:272:                                    ; preds = %272, %271
  %273 = phi i64 [ %283, %272 ], [ %270, %271 ]
  %274 = phi i64 [ %284, %272 ], [ %76, %271 ]
  %275 = getelementptr inbounds i32, i32* %27, i64 %273
  %276 = load i32, i32* %275, align 4, !tbaa !29
  %277 = sext i32 %276 to i64
  %278 = getelementptr inbounds double, double* %98, i64 %277
  %279 = bitcast double* %278 to i64*
  %280 = load i64, i64* %279, align 8, !tbaa !30
  %281 = getelementptr inbounds double, double* %57, i64 %273
  %282 = bitcast double* %281 to i64*
  store i64 %280, i64* %282, align 8, !tbaa !30
  %283 = add nuw nsw i64 %273, 1
  %284 = add i64 %274, -1
  %285 = icmp eq i64 %284, 0
  br i1 %285, label %306, label %272, !llvm.loop !31

; <label>:286:                                    ; preds = %149, %147
  %287 = phi i64 [ 0, %147 ], [ %187, %149 ]
  br i1 %83, label %306, label %288

; <label>:288:                                    ; preds = %286
  %289 = getelementptr inbounds i32, i32* %27, i64 %287
  %290 = load i32, i32* %289, align 4, !tbaa !29
  %291 = sext i32 %290 to i64
  %292 = getelementptr inbounds double, double* %98, i64 %291
  %293 = bitcast double* %292 to i64*
  %294 = load i64, i64* %293, align 8, !tbaa !30
  %295 = shl nuw nsw i64 %287, 1
  %296 = getelementptr inbounds double, double* %57, i64 %295
  %297 = bitcast double* %296 to i64*
  store i64 %294, i64* %297, align 8, !tbaa !30
  %298 = add nsw i32 %290, %2
  %299 = sext i32 %298 to i64
  %300 = getelementptr inbounds double, double* %98, i64 %299
  %301 = bitcast double* %300 to i64*
  %302 = load i64, i64* %301, align 8, !tbaa !30
  %303 = or i64 %295, 1
  %304 = getelementptr inbounds double, double* %57, i64 %303
  %305 = bitcast double* %304 to i64*
  store i64 %302, i64* %305, align 8, !tbaa !30
  br label %306

; <label>:306:                                    ; preds = %227, %192, %288, %286, %269, %272, %225, %190, %146, %102, %96
  br i1 %60, label %307, label %655

; <label>:307:                                    ; preds = %306
  br label %308

; <label>:308:                                    ; preds = %307, %653
  %309 = phi i64 [ %312, %653 ], [ 0, %307 ]
  %310 = getelementptr inbounds i32, i32* %29, i64 %309
  %311 = load i32, i32* %310, align 4, !tbaa !29
  %312 = add nuw nsw i64 %309, 1
  %313 = getelementptr inbounds i32, i32* %29, i64 %312
  %314 = load i32, i32* %313, align 4, !tbaa !29
  %315 = sub nsw i32 %314, %311
  %316 = icmp eq i64 %309, 0
  br i1 %316, label %588, label %317

; <label>:317:                                    ; preds = %308
  switch i32 %101, label %588 [
    i32 1, label %318
    i32 2, label %383
    i32 3, label %468
    i32 4, label %527
  ]

; <label>:318:                                    ; preds = %317
  %319 = icmp sgt i32 %314, %311
  br i1 %319, label %320, label %588

; <label>:320:                                    ; preds = %318
  %321 = sext i32 %311 to i64
  %322 = getelementptr inbounds i32, i32* %33, i64 %321
  %323 = load i32, i32* %322, align 4, !tbaa !29
  %324 = sext i32 %314 to i64
  br label %325

; <label>:325:                                    ; preds = %381, %320
  %326 = phi i32 [ %323, %320 ], [ %330, %381 ]
  %327 = phi i64 [ %321, %320 ], [ %328, %381 ]
  %328 = add nsw i64 %327, 1
  %329 = getelementptr inbounds i32, i32* %33, i64 %328
  %330 = load i32, i32* %329, align 4, !tbaa !29
  %331 = icmp slt i32 %326, %330
  br i1 %331, label %332, label %381

; <label>:332:                                    ; preds = %325
  %333 = getelementptr inbounds double, double* %57, i64 %327
  %334 = sext i32 %326 to i64
  %335 = load double, double* %333, align 8, !tbaa !30
  %336 = sext i32 %330 to i64
  %337 = sub nsw i64 %336, %334
  %338 = add nsw i64 %336, -1
  %339 = and i64 %337, 1
  %340 = icmp eq i64 %339, 0
  br i1 %340, label %352, label %341

; <label>:341:                                    ; preds = %332
  %342 = getelementptr inbounds double, double* %38, i64 %334
  %343 = load double, double* %342, align 8, !tbaa !30
  %344 = getelementptr inbounds i32, i32* %35, i64 %334
  %345 = load i32, i32* %344, align 4, !tbaa !29
  %346 = sext i32 %345 to i64
  %347 = getelementptr inbounds double, double* %57, i64 %346
  %348 = load double, double* %347, align 8, !tbaa !30
  %349 = fmul double %343, %348
  %350 = fsub double %335, %349
  store double %350, double* %333, align 8, !tbaa !30
  %351 = add nsw i64 %334, 1
  br label %352

; <label>:352:                                    ; preds = %341, %332
  %353 = phi double [ %350, %341 ], [ %335, %332 ]
  %354 = phi i64 [ %351, %341 ], [ %334, %332 ]
  %355 = icmp eq i64 %338, %334
  br i1 %355, label %381, label %356

; <label>:356:                                    ; preds = %352
  br label %357

; <label>:357:                                    ; preds = %357, %356
  %358 = phi double [ %353, %356 ], [ %378, %357 ]
  %359 = phi i64 [ %354, %356 ], [ %379, %357 ]
  %360 = getelementptr inbounds double, double* %38, i64 %359
  %361 = load double, double* %360, align 8, !tbaa !30
  %362 = getelementptr inbounds i32, i32* %35, i64 %359
  %363 = load i32, i32* %362, align 4, !tbaa !29
  %364 = sext i32 %363 to i64
  %365 = getelementptr inbounds double, double* %57, i64 %364
  %366 = load double, double* %365, align 8, !tbaa !30
  %367 = fmul double %361, %366
  %368 = fsub double %358, %367
  store double %368, double* %333, align 8, !tbaa !30
  %369 = add nsw i64 %359, 1
  %370 = getelementptr inbounds double, double* %38, i64 %369
  %371 = load double, double* %370, align 8, !tbaa !30
  %372 = getelementptr inbounds i32, i32* %35, i64 %369
  %373 = load i32, i32* %372, align 4, !tbaa !29
  %374 = sext i32 %373 to i64
  %375 = getelementptr inbounds double, double* %57, i64 %374
  %376 = load double, double* %375, align 8, !tbaa !30
  %377 = fmul double %371, %376
  %378 = fsub double %368, %377
  store double %378, double* %333, align 8, !tbaa !30
  %379 = add nsw i64 %359, 2
  %380 = icmp eq i64 %379, %336
  br i1 %380, label %381, label %357

; <label>:381:                                    ; preds = %352, %357, %325
  %382 = icmp eq i64 %328, %324
  br i1 %382, label %588, label %325

; <label>:383:                                    ; preds = %317
  %384 = icmp sgt i32 %314, %311
  br i1 %384, label %385, label %588

; <label>:385:                                    ; preds = %383
  %386 = sext i32 %311 to i64
  %387 = getelementptr inbounds i32, i32* %33, i64 %386
  %388 = load i32, i32* %387, align 4, !tbaa !29
  %389 = sext i32 %314 to i64
  br label %390

; <label>:390:                                    ; preds = %464, %385
  %391 = phi i32 [ %388, %385 ], [ %397, %464 ]
  %392 = phi i64 [ %386, %385 ], [ %394, %464 ]
  %393 = phi i32 [ %311, %385 ], [ %395, %464 ]
  %394 = add nsw i64 %392, 1
  %395 = add nsw i32 %393, 1
  %396 = getelementptr inbounds i32, i32* %33, i64 %394
  %397 = load i32, i32* %396, align 4, !tbaa !29
  %398 = shl nsw i32 %393, 1
  %399 = sext i32 %398 to i64
  %400 = getelementptr inbounds double, double* %57, i64 %399
  %401 = bitcast double* %400 to <2 x double>*
  %402 = load <2 x double>, <2 x double>* %401, align 8, !tbaa !30
  %403 = icmp slt i32 %391, %397
  br i1 %403, label %404, label %464

; <label>:404:                                    ; preds = %390
  %405 = sext i32 %391 to i64
  %406 = sext i32 %397 to i64
  %407 = sub nsw i64 %406, %405
  %408 = add nsw i64 %406, -1
  %409 = and i64 %407, 1
  %410 = icmp eq i64 %409, 0
  br i1 %410, label %426, label %411

; <label>:411:                                    ; preds = %404
  %412 = getelementptr inbounds i32, i32* %35, i64 %405
  %413 = load i32, i32* %412, align 4, !tbaa !29
  %414 = getelementptr inbounds double, double* %38, i64 %405
  %415 = load double, double* %414, align 8, !tbaa !30
  %416 = shl nsw i32 %413, 1
  %417 = sext i32 %416 to i64
  %418 = getelementptr inbounds double, double* %57, i64 %417
  %419 = bitcast double* %418 to <2 x double>*
  %420 = load <2 x double>, <2 x double>* %419, align 8, !tbaa !30
  %421 = insertelement <2 x double> undef, double %415, i32 0
  %422 = shufflevector <2 x double> %421, <2 x double> undef, <2 x i32> zeroinitializer
  %423 = fmul <2 x double> %422, %420
  %424 = fsub <2 x double> %402, %423
  %425 = add nsw i64 %405, 1
  br label %426

; <label>:426:                                    ; preds = %411, %404
  %427 = phi <2 x double> [ %424, %411 ], [ undef, %404 ]
  %428 = phi i64 [ %425, %411 ], [ %405, %404 ]
  %429 = phi <2 x double> [ %424, %411 ], [ %402, %404 ]
  %430 = icmp eq i64 %408, %405
  br i1 %430, label %464, label %431

; <label>:431:                                    ; preds = %426
  br label %432

; <label>:432:                                    ; preds = %432, %431
  %433 = phi i64 [ %428, %431 ], [ %462, %432 ]
  %434 = phi <2 x double> [ %429, %431 ], [ %461, %432 ]
  %435 = getelementptr inbounds i32, i32* %35, i64 %433
  %436 = load i32, i32* %435, align 4, !tbaa !29
  %437 = getelementptr inbounds double, double* %38, i64 %433
  %438 = load double, double* %437, align 8, !tbaa !30
  %439 = shl nsw i32 %436, 1
  %440 = sext i32 %439 to i64
  %441 = getelementptr inbounds double, double* %57, i64 %440
  %442 = bitcast double* %441 to <2 x double>*
  %443 = load <2 x double>, <2 x double>* %442, align 8, !tbaa !30
  %444 = insertelement <2 x double> undef, double %438, i32 0
  %445 = shufflevector <2 x double> %444, <2 x double> undef, <2 x i32> zeroinitializer
  %446 = fmul <2 x double> %445, %443
  %447 = fsub <2 x double> %434, %446
  %448 = add nsw i64 %433, 1
  %449 = getelementptr inbounds i32, i32* %35, i64 %448
  %450 = load i32, i32* %449, align 4, !tbaa !29
  %451 = getelementptr inbounds double, double* %38, i64 %448
  %452 = load double, double* %451, align 8, !tbaa !30
  %453 = shl nsw i32 %450, 1
  %454 = sext i32 %453 to i64
  %455 = getelementptr inbounds double, double* %57, i64 %454
  %456 = bitcast double* %455 to <2 x double>*
  %457 = load <2 x double>, <2 x double>* %456, align 8, !tbaa !30
  %458 = insertelement <2 x double> undef, double %452, i32 0
  %459 = shufflevector <2 x double> %458, <2 x double> undef, <2 x i32> zeroinitializer
  %460 = fmul <2 x double> %459, %457
  %461 = fsub <2 x double> %447, %460
  %462 = add nsw i64 %433, 2
  %463 = icmp eq i64 %462, %406
  br i1 %463, label %464, label %432

; <label>:464:                                    ; preds = %426, %432, %390
  %465 = phi <2 x double> [ %402, %390 ], [ %427, %426 ], [ %461, %432 ]
  %466 = bitcast double* %400 to <2 x double>*
  store <2 x double> %465, <2 x double>* %466, align 8, !tbaa !30
  %467 = icmp eq i64 %394, %389
  br i1 %467, label %588, label %390

; <label>:468:                                    ; preds = %317
  %469 = icmp sgt i32 %314, %311
  br i1 %469, label %470, label %588

; <label>:470:                                    ; preds = %468
  %471 = sext i32 %311 to i64
  %472 = getelementptr inbounds i32, i32* %33, i64 %471
  %473 = load i32, i32* %472, align 4, !tbaa !29
  %474 = sext i32 %314 to i64
  br label %475

; <label>:475:                                    ; preds = %522, %470
  %476 = phi i32 [ %473, %470 ], [ %480, %522 ]
  %477 = phi i64 [ %471, %470 ], [ %478, %522 ]
  %478 = add nsw i64 %477, 1
  %479 = getelementptr inbounds i32, i32* %33, i64 %478
  %480 = load i32, i32* %479, align 4, !tbaa !29
  %481 = mul nsw i64 %477, 3
  %482 = getelementptr inbounds double, double* %57, i64 %481
  %483 = bitcast double* %482 to <2 x double>*
  %484 = load <2 x double>, <2 x double>* %483, align 8, !tbaa !30
  %485 = add nsw i64 %481, 2
  %486 = getelementptr inbounds double, double* %57, i64 %485
  %487 = load double, double* %486, align 8, !tbaa !30
  %488 = icmp slt i32 %476, %480
  br i1 %488, label %489, label %522

; <label>:489:                                    ; preds = %475
  %490 = sext i32 %476 to i64
  %491 = sext i32 %480 to i64
  br label %492

; <label>:492:                                    ; preds = %492, %489
  %493 = phi i64 [ %490, %489 ], [ %520, %492 ]
  %494 = phi double [ %487, %489 ], [ %519, %492 ]
  %495 = phi <2 x double> [ %484, %489 ], [ %513, %492 ]
  %496 = getelementptr inbounds i32, i32* %35, i64 %493
  %497 = load i32, i32* %496, align 4, !tbaa !29
  %498 = getelementptr inbounds double, double* %38, i64 %493
  %499 = load double, double* %498, align 8, !tbaa !30
  %500 = mul nsw i32 %497, 3
  %501 = sext i32 %500 to i64
  %502 = getelementptr inbounds double, double* %57, i64 %501
  %503 = load double, double* %502, align 8, !tbaa !30
  %504 = add nsw i32 %500, 1
  %505 = sext i32 %504 to i64
  %506 = getelementptr inbounds double, double* %57, i64 %505
  %507 = load double, double* %506, align 8, !tbaa !30
  %508 = insertelement <2 x double> undef, double %499, i32 0
  %509 = shufflevector <2 x double> %508, <2 x double> undef, <2 x i32> zeroinitializer
  %510 = insertelement <2 x double> undef, double %503, i32 0
  %511 = insertelement <2 x double> %510, double %507, i32 1
  %512 = fmul <2 x double> %509, %511
  %513 = fsub <2 x double> %495, %512
  %514 = add nsw i32 %500, 2
  %515 = sext i32 %514 to i64
  %516 = getelementptr inbounds double, double* %57, i64 %515
  %517 = load double, double* %516, align 8, !tbaa !30
  %518 = fmul double %499, %517
  %519 = fsub double %494, %518
  %520 = add nsw i64 %493, 1
  %521 = icmp eq i64 %520, %491
  br i1 %521, label %522, label %492

; <label>:522:                                    ; preds = %492, %475
  %523 = phi double [ %487, %475 ], [ %519, %492 ]
  %524 = phi <2 x double> [ %484, %475 ], [ %513, %492 ]
  %525 = bitcast double* %482 to <2 x double>*
  store <2 x double> %524, <2 x double>* %525, align 8, !tbaa !30
  store double %523, double* %486, align 8, !tbaa !30
  %526 = icmp eq i64 %478, %474
  br i1 %526, label %588, label %475

; <label>:527:                                    ; preds = %317
  %528 = icmp sgt i32 %314, %311
  br i1 %528, label %529, label %588

; <label>:529:                                    ; preds = %527
  %530 = sext i32 %311 to i64
  %531 = getelementptr inbounds i32, i32* %33, i64 %530
  %532 = load i32, i32* %531, align 4, !tbaa !29
  %533 = sext i32 %314 to i64
  br label %534

; <label>:534:                                    ; preds = %582, %529
  %535 = phi i32 [ %532, %529 ], [ %541, %582 ]
  %536 = phi i64 [ %530, %529 ], [ %538, %582 ]
  %537 = phi i32 [ %311, %529 ], [ %539, %582 ]
  %538 = add nsw i64 %536, 1
  %539 = add nsw i32 %537, 1
  %540 = getelementptr inbounds i32, i32* %33, i64 %538
  %541 = load i32, i32* %540, align 4, !tbaa !29
  %542 = shl nsw i32 %537, 2
  %543 = sext i32 %542 to i64
  %544 = getelementptr inbounds double, double* %57, i64 %543
  %545 = bitcast double* %544 to <2 x double>*
  %546 = load <2 x double>, <2 x double>* %545, align 8, !tbaa !30
  %547 = or i32 %542, 2
  %548 = sext i32 %547 to i64
  %549 = getelementptr inbounds double, double* %57, i64 %548
  %550 = bitcast double* %549 to <2 x double>*
  %551 = load <2 x double>, <2 x double>* %550, align 8, !tbaa !30
  %552 = icmp slt i32 %535, %541
  br i1 %552, label %553, label %582

; <label>:553:                                    ; preds = %534
  %554 = sext i32 %535 to i64
  %555 = sext i32 %541 to i64
  br label %556

; <label>:556:                                    ; preds = %556, %553
  %557 = phi i64 [ %554, %553 ], [ %580, %556 ]
  %558 = phi <2 x double> [ %546, %553 ], [ %572, %556 ]
  %559 = phi <2 x double> [ %551, %553 ], [ %579, %556 ]
  %560 = getelementptr inbounds i32, i32* %35, i64 %557
  %561 = load i32, i32* %560, align 4, !tbaa !29
  %562 = getelementptr inbounds double, double* %38, i64 %557
  %563 = load double, double* %562, align 8, !tbaa !30
  %564 = shl nsw i32 %561, 2
  %565 = sext i32 %564 to i64
  %566 = getelementptr inbounds double, double* %57, i64 %565
  %567 = bitcast double* %566 to <2 x double>*
  %568 = load <2 x double>, <2 x double>* %567, align 8, !tbaa !30
  %569 = insertelement <2 x double> undef, double %563, i32 0
  %570 = shufflevector <2 x double> %569, <2 x double> undef, <2 x i32> zeroinitializer
  %571 = fmul <2 x double> %570, %568
  %572 = fsub <2 x double> %558, %571
  %573 = or i32 %564, 2
  %574 = sext i32 %573 to i64
  %575 = getelementptr inbounds double, double* %57, i64 %574
  %576 = bitcast double* %575 to <2 x double>*
  %577 = load <2 x double>, <2 x double>* %576, align 8, !tbaa !30
  %578 = fmul <2 x double> %570, %577
  %579 = fsub <2 x double> %559, %578
  %580 = add nsw i64 %557, 1
  %581 = icmp eq i64 %580, %555
  br i1 %581, label %582, label %556

; <label>:582:                                    ; preds = %556, %534
  %583 = phi <2 x double> [ %546, %534 ], [ %572, %556 ]
  %584 = phi <2 x double> [ %551, %534 ], [ %579, %556 ]
  %585 = bitcast double* %544 to <2 x double>*
  store <2 x double> %583, <2 x double>* %585, align 8, !tbaa !30
  %586 = bitcast double* %549 to <2 x double>*
  store <2 x double> %584, <2 x double>* %586, align 8, !tbaa !30
  %587 = icmp eq i64 %538, %533
  br i1 %587, label %588, label %534

; <label>:588:                                    ; preds = %582, %522, %464, %381, %527, %468, %383, %318, %308, %317
  %589 = icmp eq i32 %315, 1
  %590 = sext i32 %311 to i64
  br i1 %589, label %591, label %641

; <label>:591:                                    ; preds = %588
  %592 = getelementptr inbounds double, double* %52, i64 %590
  %593 = load double, double* %592, align 8, !tbaa !30
  switch i32 %101, label %653 [
    i32 1, label %594
    i32 2, label %598
    i32 3, label %608
    i32 4, label %624
  ]

; <label>:594:                                    ; preds = %591
  %595 = getelementptr inbounds double, double* %57, i64 %590
  %596 = load double, double* %595, align 8, !tbaa !30
  %597 = fdiv double %596, %593
  store double %597, double* %595, align 8, !tbaa !30
  br label %653

; <label>:598:                                    ; preds = %591
  %599 = shl nsw i32 %311, 1
  %600 = sext i32 %599 to i64
  %601 = getelementptr inbounds double, double* %57, i64 %600
  %602 = bitcast double* %601 to <2 x double>*
  %603 = load <2 x double>, <2 x double>* %602, align 8, !tbaa !30
  %604 = insertelement <2 x double> undef, double %593, i32 0
  %605 = shufflevector <2 x double> %604, <2 x double> undef, <2 x i32> zeroinitializer
  %606 = fdiv <2 x double> %603, %605
  %607 = bitcast double* %601 to <2 x double>*
  store <2 x double> %606, <2 x double>* %607, align 8, !tbaa !30
  br label %653

; <label>:608:                                    ; preds = %591
  %609 = mul nsw i32 %311, 3
  %610 = sext i32 %609 to i64
  %611 = getelementptr inbounds double, double* %57, i64 %610
  %612 = load double, double* %611, align 8, !tbaa !30
  %613 = fdiv double %612, %593
  store double %613, double* %611, align 8, !tbaa !30
  %614 = add nsw i32 %609, 1
  %615 = sext i32 %614 to i64
  %616 = getelementptr inbounds double, double* %57, i64 %615
  %617 = load double, double* %616, align 8, !tbaa !30
  %618 = fdiv double %617, %593
  store double %618, double* %616, align 8, !tbaa !30
  %619 = add nsw i32 %609, 2
  %620 = sext i32 %619 to i64
  %621 = getelementptr inbounds double, double* %57, i64 %620
  %622 = load double, double* %621, align 8, !tbaa !30
  %623 = fdiv double %622, %593
  store double %623, double* %621, align 8, !tbaa !30
  br label %653

; <label>:624:                                    ; preds = %591
  %625 = shl nsw i32 %311, 2
  %626 = sext i32 %625 to i64
  %627 = getelementptr inbounds double, double* %57, i64 %626
  %628 = bitcast double* %627 to <2 x double>*
  %629 = load <2 x double>, <2 x double>* %628, align 8, !tbaa !30
  %630 = insertelement <2 x double> undef, double %593, i32 0
  %631 = shufflevector <2 x double> %630, <2 x double> undef, <2 x i32> zeroinitializer
  %632 = fdiv <2 x double> %629, %631
  %633 = bitcast double* %627 to <2 x double>*
  store <2 x double> %632, <2 x double>* %633, align 8, !tbaa !30
  %634 = or i32 %625, 2
  %635 = sext i32 %634 to i64
  %636 = getelementptr inbounds double, double* %57, i64 %635
  %637 = bitcast double* %636 to <2 x double>*
  %638 = load <2 x double>, <2 x double>* %637, align 8, !tbaa !30
  %639 = fdiv <2 x double> %638, %631
  %640 = bitcast double* %636 to <2 x double>*
  store <2 x double> %639, <2 x double>* %640, align 8, !tbaa !30
  br label %653

; <label>:641:                                    ; preds = %588
  %642 = getelementptr inbounds i32, i32* %44, i64 %590
  %643 = getelementptr inbounds i32, i32* %46, i64 %590
  %644 = getelementptr inbounds double*, double** %49, i64 %309
  %645 = load double*, double** %644, align 8, !tbaa !33
  %646 = getelementptr inbounds double, double* %52, i64 %590
  %647 = mul nsw i32 %311, %101
  %648 = sext i32 %647 to i64
  %649 = getelementptr inbounds double, double* %57, i64 %648
  tail call void @klu_utsolve(i32 %315, i32* %642, i32* %643, double* %645, double* %646, i32 %101, double* %649) #2
  %650 = getelementptr inbounds i32, i32* %40, i64 %590
  %651 = getelementptr inbounds i32, i32* %42, i64 %590
  %652 = load double*, double** %644, align 8, !tbaa !33
  tail call void @klu_ltsolve(i32 %315, i32* %650, i32* %651, double* %652, i32 %101, double* %649) #2
  br label %653

; <label>:653:                                    ; preds = %641, %591, %624, %608, %598, %594
  %654 = icmp eq i64 %312, %67
  br i1 %654, label %655, label %308

; <label>:655:                                    ; preds = %653, %306
  br i1 %62, label %656, label %824

; <label>:656:                                    ; preds = %655
  switch i32 %101, label %1001 [
    i32 1, label %657
    i32 2, label %701
    i32 3, label %745
    i32 4, label %780
  ]

; <label>:657:                                    ; preds = %656
  br i1 %61, label %658, label %1001

; <label>:658:                                    ; preds = %657
  br i1 %85, label %952, label %659

; <label>:659:                                    ; preds = %658
  br label %660

; <label>:660:                                    ; preds = %660, %659
  %661 = phi i64 [ 0, %659 ], [ %698, %660 ]
  %662 = phi i64 [ %90, %659 ], [ %699, %660 ]
  %663 = getelementptr inbounds double, double* %57, i64 %661
  %664 = bitcast double* %663 to i64*
  %665 = load i64, i64* %664, align 8, !tbaa !30
  %666 = getelementptr inbounds i32, i32* %31, i64 %661
  %667 = load i32, i32* %666, align 4, !tbaa !29
  %668 = sext i32 %667 to i64
  %669 = getelementptr inbounds double, double* %98, i64 %668
  %670 = bitcast double* %669 to i64*
  store i64 %665, i64* %670, align 8, !tbaa !30
  %671 = or i64 %661, 1
  %672 = getelementptr inbounds double, double* %57, i64 %671
  %673 = bitcast double* %672 to i64*
  %674 = load i64, i64* %673, align 8, !tbaa !30
  %675 = getelementptr inbounds i32, i32* %31, i64 %671
  %676 = load i32, i32* %675, align 4, !tbaa !29
  %677 = sext i32 %676 to i64
  %678 = getelementptr inbounds double, double* %98, i64 %677
  %679 = bitcast double* %678 to i64*
  store i64 %674, i64* %679, align 8, !tbaa !30
  %680 = or i64 %661, 2
  %681 = getelementptr inbounds double, double* %57, i64 %680
  %682 = bitcast double* %681 to i64*
  %683 = load i64, i64* %682, align 8, !tbaa !30
  %684 = getelementptr inbounds i32, i32* %31, i64 %680
  %685 = load i32, i32* %684, align 4, !tbaa !29
  %686 = sext i32 %685 to i64
  %687 = getelementptr inbounds double, double* %98, i64 %686
  %688 = bitcast double* %687 to i64*
  store i64 %683, i64* %688, align 8, !tbaa !30
  %689 = or i64 %661, 3
  %690 = getelementptr inbounds double, double* %57, i64 %689
  %691 = bitcast double* %690 to i64*
  %692 = load i64, i64* %691, align 8, !tbaa !30
  %693 = getelementptr inbounds i32, i32* %31, i64 %689
  %694 = load i32, i32* %693, align 4, !tbaa !29
  %695 = sext i32 %694 to i64
  %696 = getelementptr inbounds double, double* %98, i64 %695
  %697 = bitcast double* %696 to i64*
  store i64 %692, i64* %697, align 8, !tbaa !30
  %698 = add nuw nsw i64 %661, 4
  %699 = add i64 %662, -4
  %700 = icmp eq i64 %699, 0
  br i1 %700, label %952, label %660

; <label>:701:                                    ; preds = %656
  br i1 %61, label %702, label %1001

; <label>:702:                                    ; preds = %701
  br i1 %87, label %969, label %703

; <label>:703:                                    ; preds = %702
  br label %704

; <label>:704:                                    ; preds = %704, %703
  %705 = phi i64 [ 0, %703 ], [ %742, %704 ]
  %706 = phi i64 [ %92, %703 ], [ %743, %704 ]
  %707 = getelementptr inbounds i32, i32* %31, i64 %705
  %708 = load i32, i32* %707, align 4, !tbaa !29
  %709 = shl nuw nsw i64 %705, 1
  %710 = getelementptr inbounds double, double* %57, i64 %709
  %711 = bitcast double* %710 to i64*
  %712 = load i64, i64* %711, align 8, !tbaa !30
  %713 = sext i32 %708 to i64
  %714 = getelementptr inbounds double, double* %98, i64 %713
  %715 = bitcast double* %714 to i64*
  store i64 %712, i64* %715, align 8, !tbaa !30
  %716 = or i64 %709, 1
  %717 = getelementptr inbounds double, double* %57, i64 %716
  %718 = bitcast double* %717 to i64*
  %719 = load i64, i64* %718, align 8, !tbaa !30
  %720 = add nsw i32 %708, %2
  %721 = sext i32 %720 to i64
  %722 = getelementptr inbounds double, double* %98, i64 %721
  %723 = bitcast double* %722 to i64*
  store i64 %719, i64* %723, align 8, !tbaa !30
  %724 = or i64 %705, 1
  %725 = getelementptr inbounds i32, i32* %31, i64 %724
  %726 = load i32, i32* %725, align 4, !tbaa !29
  %727 = shl nuw nsw i64 %724, 1
  %728 = getelementptr inbounds double, double* %57, i64 %727
  %729 = bitcast double* %728 to i64*
  %730 = load i64, i64* %729, align 8, !tbaa !30
  %731 = sext i32 %726 to i64
  %732 = getelementptr inbounds double, double* %98, i64 %731
  %733 = bitcast double* %732 to i64*
  store i64 %730, i64* %733, align 8, !tbaa !30
  %734 = or i64 %727, 1
  %735 = getelementptr inbounds double, double* %57, i64 %734
  %736 = bitcast double* %735 to i64*
  %737 = load i64, i64* %736, align 8, !tbaa !30
  %738 = add nsw i32 %726, %2
  %739 = sext i32 %738 to i64
  %740 = getelementptr inbounds double, double* %98, i64 %739
  %741 = bitcast double* %740 to i64*
  store i64 %737, i64* %741, align 8, !tbaa !30
  %742 = add nuw nsw i64 %705, 2
  %743 = add i64 %706, -2
  %744 = icmp eq i64 %743, 0
  br i1 %744, label %969, label %704

; <label>:745:                                    ; preds = %656
  br i1 %61, label %746, label %1001

; <label>:746:                                    ; preds = %745
  br label %747

; <label>:747:                                    ; preds = %746, %747
  %748 = phi i64 [ %778, %747 ], [ 0, %746 ]
  %749 = getelementptr inbounds i32, i32* %31, i64 %748
  %750 = load i32, i32* %749, align 4, !tbaa !29
  %751 = trunc i64 %748 to i32
  %752 = mul nsw i32 %751, 3
  %753 = zext i32 %752 to i64
  %754 = getelementptr inbounds double, double* %57, i64 %753
  %755 = bitcast double* %754 to i64*
  %756 = load i64, i64* %755, align 8, !tbaa !30
  %757 = sext i32 %750 to i64
  %758 = getelementptr inbounds double, double* %98, i64 %757
  %759 = bitcast double* %758 to i64*
  store i64 %756, i64* %759, align 8, !tbaa !30
  %760 = add nuw nsw i32 %752, 1
  %761 = zext i32 %760 to i64
  %762 = getelementptr inbounds double, double* %57, i64 %761
  %763 = bitcast double* %762 to i64*
  %764 = load i64, i64* %763, align 8, !tbaa !30
  %765 = add nsw i32 %750, %2
  %766 = sext i32 %765 to i64
  %767 = getelementptr inbounds double, double* %98, i64 %766
  %768 = bitcast double* %767 to i64*
  store i64 %764, i64* %768, align 8, !tbaa !30
  %769 = add nuw nsw i32 %752, 2
  %770 = zext i32 %769 to i64
  %771 = getelementptr inbounds double, double* %57, i64 %770
  %772 = bitcast double* %771 to i64*
  %773 = load i64, i64* %772, align 8, !tbaa !30
  %774 = add nsw i32 %750, %63
  %775 = sext i32 %774 to i64
  %776 = getelementptr inbounds double, double* %98, i64 %775
  %777 = bitcast double* %776 to i64*
  store i64 %773, i64* %777, align 8, !tbaa !30
  %778 = add nuw nsw i64 %748, 1
  %779 = icmp eq i64 %778, %70
  br i1 %779, label %1001, label %747

; <label>:780:                                    ; preds = %656
  br i1 %61, label %781, label %1001

; <label>:781:                                    ; preds = %780
  br label %782

; <label>:782:                                    ; preds = %781, %782
  %783 = phi i64 [ %822, %782 ], [ 0, %781 ]
  %784 = getelementptr inbounds i32, i32* %31, i64 %783
  %785 = load i32, i32* %784, align 4, !tbaa !29
  %786 = trunc i64 %783 to i32
  %787 = shl nsw i32 %786, 2
  %788 = zext i32 %787 to i64
  %789 = getelementptr inbounds double, double* %57, i64 %788
  %790 = bitcast double* %789 to i64*
  %791 = load i64, i64* %790, align 8, !tbaa !30
  %792 = sext i32 %785 to i64
  %793 = getelementptr inbounds double, double* %98, i64 %792
  %794 = bitcast double* %793 to i64*
  store i64 %791, i64* %794, align 8, !tbaa !30
  %795 = or i32 %787, 1
  %796 = zext i32 %795 to i64
  %797 = getelementptr inbounds double, double* %57, i64 %796
  %798 = bitcast double* %797 to i64*
  %799 = load i64, i64* %798, align 8, !tbaa !30
  %800 = add nsw i32 %785, %2
  %801 = sext i32 %800 to i64
  %802 = getelementptr inbounds double, double* %98, i64 %801
  %803 = bitcast double* %802 to i64*
  store i64 %799, i64* %803, align 8, !tbaa !30
  %804 = or i32 %787, 2
  %805 = zext i32 %804 to i64
  %806 = getelementptr inbounds double, double* %57, i64 %805
  %807 = bitcast double* %806 to i64*
  %808 = load i64, i64* %807, align 8, !tbaa !30
  %809 = add nsw i32 %785, %63
  %810 = sext i32 %809 to i64
  %811 = getelementptr inbounds double, double* %98, i64 %810
  %812 = bitcast double* %811 to i64*
  store i64 %808, i64* %812, align 8, !tbaa !30
  %813 = or i32 %787, 3
  %814 = zext i32 %813 to i64
  %815 = getelementptr inbounds double, double* %57, i64 %814
  %816 = bitcast double* %815 to i64*
  %817 = load i64, i64* %816, align 8, !tbaa !30
  %818 = add nsw i32 %785, %64
  %819 = sext i32 %818 to i64
  %820 = getelementptr inbounds double, double* %98, i64 %819
  %821 = bitcast double* %820 to i64*
  store i64 %817, i64* %821, align 8, !tbaa !30
  %822 = add nuw nsw i64 %783, 1
  %823 = icmp eq i64 %822, %71
  br i1 %823, label %1001, label %782

; <label>:824:                                    ; preds = %655
  switch i32 %101, label %1001 [
    i32 1, label %825
    i32 2, label %853
    i32 3, label %876
    i32 4, label %910
  ]

; <label>:825:                                    ; preds = %824
  br i1 %61, label %826, label %1001

; <label>:826:                                    ; preds = %825
  br i1 %89, label %989, label %827

; <label>:827:                                    ; preds = %826
  br label %828

; <label>:828:                                    ; preds = %828, %827
  %829 = phi i64 [ 0, %827 ], [ %850, %828 ]
  %830 = phi i64 [ %94, %827 ], [ %851, %828 ]
  %831 = getelementptr inbounds double, double* %57, i64 %829
  %832 = load double, double* %831, align 8, !tbaa !30
  %833 = getelementptr inbounds double, double* %54, i64 %829
  %834 = load double, double* %833, align 8, !tbaa !30
  %835 = fdiv double %832, %834
  %836 = getelementptr inbounds i32, i32* %31, i64 %829
  %837 = load i32, i32* %836, align 4, !tbaa !29
  %838 = sext i32 %837 to i64
  %839 = getelementptr inbounds double, double* %98, i64 %838
  store double %835, double* %839, align 8, !tbaa !30
  %840 = or i64 %829, 1
  %841 = getelementptr inbounds double, double* %57, i64 %840
  %842 = load double, double* %841, align 8, !tbaa !30
  %843 = getelementptr inbounds double, double* %54, i64 %840
  %844 = load double, double* %843, align 8, !tbaa !30
  %845 = fdiv double %842, %844
  %846 = getelementptr inbounds i32, i32* %31, i64 %840
  %847 = load i32, i32* %846, align 4, !tbaa !29
  %848 = sext i32 %847 to i64
  %849 = getelementptr inbounds double, double* %98, i64 %848
  store double %845, double* %849, align 8, !tbaa !30
  %850 = add nuw nsw i64 %829, 2
  %851 = add i64 %830, -2
  %852 = icmp eq i64 %851, 0
  br i1 %852, label %989, label %828

; <label>:853:                                    ; preds = %824
  br i1 %61, label %854, label %1001

; <label>:854:                                    ; preds = %853
  br label %855

; <label>:855:                                    ; preds = %854, %855
  %856 = phi i64 [ %874, %855 ], [ 0, %854 ]
  %857 = getelementptr inbounds i32, i32* %31, i64 %856
  %858 = load i32, i32* %857, align 4, !tbaa !29
  %859 = getelementptr inbounds double, double* %54, i64 %856
  %860 = load double, double* %859, align 8, !tbaa !30
  %861 = shl nuw nsw i64 %856, 1
  %862 = getelementptr inbounds double, double* %57, i64 %861
  %863 = load double, double* %862, align 8, !tbaa !30
  %864 = fdiv double %863, %860
  %865 = sext i32 %858 to i64
  %866 = getelementptr inbounds double, double* %98, i64 %865
  store double %864, double* %866, align 8, !tbaa !30
  %867 = or i64 %861, 1
  %868 = getelementptr inbounds double, double* %57, i64 %867
  %869 = load double, double* %868, align 8, !tbaa !30
  %870 = fdiv double %869, %860
  %871 = add nsw i32 %858, %2
  %872 = sext i32 %871 to i64
  %873 = getelementptr inbounds double, double* %98, i64 %872
  store double %870, double* %873, align 8, !tbaa !30
  %874 = add nuw nsw i64 %856, 1
  %875 = icmp eq i64 %874, %72
  br i1 %875, label %1001, label %855

; <label>:876:                                    ; preds = %824
  br i1 %61, label %877, label %1001

; <label>:877:                                    ; preds = %876
  br label %878

; <label>:878:                                    ; preds = %877, %878
  %879 = phi i64 [ %908, %878 ], [ 0, %877 ]
  %880 = getelementptr inbounds i32, i32* %31, i64 %879
  %881 = load i32, i32* %880, align 4, !tbaa !29
  %882 = getelementptr inbounds double, double* %54, i64 %879
  %883 = load double, double* %882, align 8, !tbaa !30
  %884 = trunc i64 %879 to i32
  %885 = mul nsw i32 %884, 3
  %886 = zext i32 %885 to i64
  %887 = getelementptr inbounds double, double* %57, i64 %886
  %888 = load double, double* %887, align 8, !tbaa !30
  %889 = fdiv double %888, %883
  %890 = sext i32 %881 to i64
  %891 = getelementptr inbounds double, double* %98, i64 %890
  store double %889, double* %891, align 8, !tbaa !30
  %892 = add nuw nsw i32 %885, 1
  %893 = zext i32 %892 to i64
  %894 = getelementptr inbounds double, double* %57, i64 %893
  %895 = load double, double* %894, align 8, !tbaa !30
  %896 = fdiv double %895, %883
  %897 = add nsw i32 %881, %2
  %898 = sext i32 %897 to i64
  %899 = getelementptr inbounds double, double* %98, i64 %898
  store double %896, double* %899, align 8, !tbaa !30
  %900 = add nuw nsw i32 %885, 2
  %901 = zext i32 %900 to i64
  %902 = getelementptr inbounds double, double* %57, i64 %901
  %903 = load double, double* %902, align 8, !tbaa !30
  %904 = fdiv double %903, %883
  %905 = add nsw i32 %881, %63
  %906 = sext i32 %905 to i64
  %907 = getelementptr inbounds double, double* %98, i64 %906
  store double %904, double* %907, align 8, !tbaa !30
  %908 = add nuw nsw i64 %879, 1
  %909 = icmp eq i64 %908, %73
  br i1 %909, label %1001, label %878

; <label>:910:                                    ; preds = %824
  br i1 %61, label %911, label %1001

; <label>:911:                                    ; preds = %910
  br label %912

; <label>:912:                                    ; preds = %911, %912
  %913 = phi i64 [ %950, %912 ], [ 0, %911 ]
  %914 = getelementptr inbounds i32, i32* %31, i64 %913
  %915 = load i32, i32* %914, align 4, !tbaa !29
  %916 = getelementptr inbounds double, double* %54, i64 %913
  %917 = load double, double* %916, align 8, !tbaa !30
  %918 = trunc i64 %913 to i32
  %919 = shl nsw i32 %918, 2
  %920 = zext i32 %919 to i64
  %921 = getelementptr inbounds double, double* %57, i64 %920
  %922 = load double, double* %921, align 8, !tbaa !30
  %923 = fdiv double %922, %917
  %924 = sext i32 %915 to i64
  %925 = getelementptr inbounds double, double* %98, i64 %924
  store double %923, double* %925, align 8, !tbaa !30
  %926 = or i32 %919, 1
  %927 = zext i32 %926 to i64
  %928 = getelementptr inbounds double, double* %57, i64 %927
  %929 = load double, double* %928, align 8, !tbaa !30
  %930 = fdiv double %929, %917
  %931 = add nsw i32 %915, %2
  %932 = sext i32 %931 to i64
  %933 = getelementptr inbounds double, double* %98, i64 %932
  store double %930, double* %933, align 8, !tbaa !30
  %934 = or i32 %919, 2
  %935 = zext i32 %934 to i64
  %936 = getelementptr inbounds double, double* %57, i64 %935
  %937 = load double, double* %936, align 8, !tbaa !30
  %938 = fdiv double %937, %917
  %939 = add nsw i32 %915, %63
  %940 = sext i32 %939 to i64
  %941 = getelementptr inbounds double, double* %98, i64 %940
  store double %938, double* %941, align 8, !tbaa !30
  %942 = or i32 %919, 3
  %943 = zext i32 %942 to i64
  %944 = getelementptr inbounds double, double* %57, i64 %943
  %945 = load double, double* %944, align 8, !tbaa !30
  %946 = fdiv double %945, %917
  %947 = add nsw i32 %915, %64
  %948 = sext i32 %947 to i64
  %949 = getelementptr inbounds double, double* %98, i64 %948
  store double %946, double* %949, align 8, !tbaa !30
  %950 = add nuw nsw i64 %913, 1
  %951 = icmp eq i64 %950, %74
  br i1 %951, label %1001, label %912

; <label>:952:                                    ; preds = %660, %658
  %953 = phi i64 [ 0, %658 ], [ %698, %660 ]
  br i1 %91, label %1001, label %954

; <label>:954:                                    ; preds = %952
  br label %955

; <label>:955:                                    ; preds = %955, %954
  %956 = phi i64 [ %966, %955 ], [ %953, %954 ]
  %957 = phi i64 [ %967, %955 ], [ %84, %954 ]
  %958 = getelementptr inbounds double, double* %57, i64 %956
  %959 = bitcast double* %958 to i64*
  %960 = load i64, i64* %959, align 8, !tbaa !30
  %961 = getelementptr inbounds i32, i32* %31, i64 %956
  %962 = load i32, i32* %961, align 4, !tbaa !29
  %963 = sext i32 %962 to i64
  %964 = getelementptr inbounds double, double* %98, i64 %963
  %965 = bitcast double* %964 to i64*
  store i64 %960, i64* %965, align 8, !tbaa !30
  %966 = add nuw nsw i64 %956, 1
  %967 = add i64 %957, -1
  %968 = icmp eq i64 %967, 0
  br i1 %968, label %1001, label %955, !llvm.loop !34

; <label>:969:                                    ; preds = %704, %702
  %970 = phi i64 [ 0, %702 ], [ %742, %704 ]
  br i1 %93, label %1001, label %971

; <label>:971:                                    ; preds = %969
  %972 = getelementptr inbounds i32, i32* %31, i64 %970
  %973 = load i32, i32* %972, align 4, !tbaa !29
  %974 = shl nuw nsw i64 %970, 1
  %975 = getelementptr inbounds double, double* %57, i64 %974
  %976 = bitcast double* %975 to i64*
  %977 = load i64, i64* %976, align 8, !tbaa !30
  %978 = sext i32 %973 to i64
  %979 = getelementptr inbounds double, double* %98, i64 %978
  %980 = bitcast double* %979 to i64*
  store i64 %977, i64* %980, align 8, !tbaa !30
  %981 = or i64 %974, 1
  %982 = getelementptr inbounds double, double* %57, i64 %981
  %983 = bitcast double* %982 to i64*
  %984 = load i64, i64* %983, align 8, !tbaa !30
  %985 = add nsw i32 %973, %2
  %986 = sext i32 %985 to i64
  %987 = getelementptr inbounds double, double* %98, i64 %986
  %988 = bitcast double* %987 to i64*
  store i64 %984, i64* %988, align 8, !tbaa !30
  br label %1001

; <label>:989:                                    ; preds = %828, %826
  %990 = phi i64 [ 0, %826 ], [ %850, %828 ]
  br i1 %95, label %1001, label %991

; <label>:991:                                    ; preds = %989
  %992 = getelementptr inbounds double, double* %57, i64 %990
  %993 = load double, double* %992, align 8, !tbaa !30
  %994 = getelementptr inbounds double, double* %54, i64 %990
  %995 = load double, double* %994, align 8, !tbaa !30
  %996 = fdiv double %993, %995
  %997 = getelementptr inbounds i32, i32* %31, i64 %990
  %998 = load i32, i32* %997, align 4, !tbaa !29
  %999 = sext i32 %998 to i64
  %1000 = getelementptr inbounds double, double* %98, i64 %999
  store double %996, double* %1000, align 8, !tbaa !30
  br label %1001

; <label>:1001:                                   ; preds = %912, %878, %855, %991, %989, %782, %747, %971, %969, %952, %955, %910, %876, %853, %825, %780, %745, %701, %657, %824, %656
  %1002 = getelementptr inbounds double, double* %98, i64 %66
  %1003 = add nuw nsw i32 %97, 4
  %1004 = icmp slt i32 %1003, %3
  br i1 %1004, label %96, label %1005

; <label>:1005:                                   ; preds = %1001, %22, %6, %20
  %1006 = phi i32 [ 0, %20 ], [ 0, %6 ], [ 1, %22 ], [ 1, %1001 ]
  ret i32 %1006
}

declare void @klu_utsolve(i32, i32*, i32*, double*, double*, i32, double*) local_unnamed_addr #1

declare void @klu_ltsolve(i32, i32*, i32*, double*, i32, double*) local_unnamed_addr #1

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
