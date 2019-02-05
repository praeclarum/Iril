; ModuleID = 'klu_diagnostics.c'
source_filename = "klu_diagnostics.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_rgrowth(i32* readonly, i32* readonly, double* readonly, %struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %7 = icmp eq %struct.klu_common_struct* %5, null
  br i1 %7, label %338, label %8

; <label>:8:                                      ; preds = %6
  %9 = icmp eq %struct.klu_symbolic* %3, null
  %10 = icmp eq i32* %0, null
  %11 = or i1 %10, %9
  %12 = icmp eq i32* %1, null
  %13 = or i1 %12, %11
  %14 = icmp eq double* %2, null
  %15 = or i1 %14, %13
  br i1 %15, label %16, label %18

; <label>:16:                                     ; preds = %8
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 -3, i32* %17, align 4, !tbaa !3
  br label %338

; <label>:18:                                     ; preds = %8
  %19 = icmp eq %struct.klu_numeric* %4, null
  br i1 %19, label %20, label %23

; <label>:20:                                     ; preds = %18
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 20
  store double 0.000000e+00, double* %21, align 8, !tbaa !11
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 1, i32* %22, align 4, !tbaa !3
  br label %338

; <label>:23:                                     ; preds = %18
  %24 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  store i32 0, i32* %24, align 4, !tbaa !3
  %25 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 7
  %26 = load i32*, i32** %25, align 8, !tbaa !12
  %27 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 15
  %28 = load double*, double** %27, align 8, !tbaa !14
  %29 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 8
  %30 = load i32*, i32** %29, align 8, !tbaa !15
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 20
  store double 1.000000e+00, double* %31, align 8, !tbaa !11
  %32 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 11
  %33 = load i32, i32* %32, align 4, !tbaa !17
  %34 = icmp sgt i32 %33, 0
  br i1 %34, label %35, label %338

; <label>:35:                                     ; preds = %23
  %36 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 9
  %37 = load i32*, i32** %36, align 8, !tbaa !18
  %38 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 12
  %39 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 9
  %40 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 11
  %41 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %4, i64 0, i32 14
  %42 = bitcast i8** %41 to double**
  %43 = icmp eq double* %28, null
  %44 = sext i32 %33 to i64
  %45 = load i32, i32* %37, align 4, !tbaa !19
  br label %46

; <label>:46:                                     ; preds = %35, %335
  %47 = phi double [ 1.000000e+00, %35 ], [ %336, %335 ]
  %48 = phi i32 [ %45, %35 ], [ %52, %335 ]
  %49 = phi i64 [ 0, %35 ], [ %50, %335 ]
  %50 = add nuw nsw i64 %49, 1
  %51 = getelementptr inbounds i32, i32* %37, i64 %50
  %52 = load i32, i32* %51, align 4, !tbaa !19
  %53 = sub nsw i32 %52, %48
  %54 = icmp eq i32 %53, 1
  br i1 %54, label %335, label %55

; <label>:55:                                     ; preds = %46
  %56 = load i8**, i8*** %38, align 8, !tbaa !20
  %57 = getelementptr inbounds i8*, i8** %56, i64 %49
  %58 = bitcast i8** %57 to double**
  %59 = load double*, double** %58, align 8, !tbaa !21
  %60 = load i32*, i32** %39, align 8, !tbaa !22
  %61 = sext i32 %48 to i64
  %62 = getelementptr inbounds i32, i32* %60, i64 %61
  %63 = load i32*, i32** %40, align 8, !tbaa !23
  %64 = getelementptr inbounds i32, i32* %63, i64 %61
  %65 = load double*, double** %42, align 8, !tbaa !24
  %66 = getelementptr inbounds double, double* %65, i64 %61
  %67 = icmp sgt i32 %53, 0
  br i1 %67, label %68, label %331

; <label>:68:                                     ; preds = %55
  %69 = zext i32 %53 to i64
  br i1 %43, label %71, label %70

; <label>:70:                                     ; preds = %68
  br label %216

; <label>:71:                                     ; preds = %68
  br label %72

; <label>:72:                                     ; preds = %71, %127
  %73 = phi i64 [ %129, %127 ], [ 0, %71 ]
  %74 = phi double [ %128, %127 ], [ 1.000000e+00, %71 ]
  %75 = add nsw i64 %73, %61
  %76 = getelementptr inbounds i32, i32* %30, i64 %75
  %77 = load i32, i32* %76, align 4, !tbaa !19
  %78 = add nsw i32 %77, 1
  %79 = sext i32 %78 to i64
  %80 = getelementptr inbounds i32, i32* %0, i64 %79
  %81 = load i32, i32* %80, align 4, !tbaa !19
  %82 = sext i32 %77 to i64
  %83 = getelementptr inbounds i32, i32* %0, i64 %82
  %84 = load i32, i32* %83, align 4, !tbaa !19
  %85 = icmp slt i32 %84, %81
  br i1 %85, label %179, label %86

; <label>:86:                                     ; preds = %204, %348, %72
  %87 = phi double [ 0.000000e+00, %72 ], [ %205, %204 ], [ %349, %348 ]
  %88 = getelementptr inbounds i32, i32* %62, i64 %73
  %89 = load i32, i32* %88, align 4, !tbaa !19
  %90 = sext i32 %89 to i64
  %91 = getelementptr inbounds double, double* %59, i64 %90
  %92 = getelementptr inbounds i32, i32* %64, i64 %73
  %93 = load i32, i32* %92, align 4, !tbaa !19
  %94 = sext i32 %93 to i64
  %95 = shl nsw i64 %94, 2
  %96 = add nsw i64 %95, 7
  %97 = lshr i64 %96, 3
  %98 = getelementptr inbounds double, double* %91, i64 %97
  %99 = icmp sgt i32 %93, 0
  br i1 %99, label %210, label %113

; <label>:100:                                    ; preds = %131, %210
  %101 = phi double [ undef, %210 ], [ %149, %131 ]
  %102 = phi i64 [ 0, %210 ], [ %150, %131 ]
  %103 = phi double [ 0.000000e+00, %210 ], [ %149, %131 ]
  %104 = icmp eq i64 %212, 0
  br i1 %104, label %113, label %105

; <label>:105:                                    ; preds = %100
  %106 = getelementptr inbounds double, double* %98, i64 %102
  %107 = load double, double* %106, align 8, !tbaa !25
  %108 = fcmp olt double %107, 0.000000e+00
  %109 = fsub double -0.000000e+00, %107
  %110 = select i1 %108, double %109, double %107
  %111 = fcmp ogt double %110, %103
  %112 = select i1 %111, double %110, double %103
  br label %113

; <label>:113:                                    ; preds = %105, %100, %86
  %114 = phi double [ 0.000000e+00, %86 ], [ %101, %100 ], [ %112, %105 ]
  %115 = getelementptr inbounds double, double* %66, i64 %73
  %116 = load double, double* %115, align 8, !tbaa !25
  %117 = fcmp olt double %116, 0.000000e+00
  %118 = fsub double -0.000000e+00, %116
  %119 = select i1 %117, double %118, double %116
  %120 = fcmp ogt double %119, %114
  %121 = select i1 %120, double %119, double %114
  %122 = fcmp oeq double %121, 0.000000e+00
  br i1 %122, label %127, label %123

; <label>:123:                                    ; preds = %113
  %124 = fdiv double %87, %121
  %125 = fcmp olt double %124, %74
  br i1 %125, label %126, label %127

; <label>:126:                                    ; preds = %123
  br label %127

; <label>:127:                                    ; preds = %126, %123, %113
  %128 = phi double [ %74, %113 ], [ %124, %126 ], [ %74, %123 ]
  %129 = add nuw nsw i64 %73, 1
  %130 = icmp eq i64 %129, %69
  br i1 %130, label %331, label %72

; <label>:131:                                    ; preds = %131, %214
  %132 = phi i64 [ 0, %214 ], [ %150, %131 ]
  %133 = phi double [ 0.000000e+00, %214 ], [ %149, %131 ]
  %134 = phi i64 [ %215, %214 ], [ %151, %131 ]
  %135 = getelementptr inbounds double, double* %98, i64 %132
  %136 = load double, double* %135, align 8, !tbaa !25
  %137 = fcmp olt double %136, 0.000000e+00
  %138 = fsub double -0.000000e+00, %136
  %139 = select i1 %137, double %138, double %136
  %140 = fcmp ogt double %139, %133
  %141 = select i1 %140, double %139, double %133
  %142 = or i64 %132, 1
  %143 = getelementptr inbounds double, double* %98, i64 %142
  %144 = load double, double* %143, align 8, !tbaa !25
  %145 = fcmp olt double %144, 0.000000e+00
  %146 = fsub double -0.000000e+00, %144
  %147 = select i1 %145, double %146, double %144
  %148 = fcmp ogt double %147, %141
  %149 = select i1 %148, double %147, double %141
  %150 = add nuw nsw i64 %132, 2
  %151 = add i64 %134, -2
  %152 = icmp eq i64 %151, 0
  br i1 %152, label %100, label %131

; <label>:153:                                    ; preds = %348, %209
  %154 = phi i64 [ %206, %209 ], [ %350, %348 ]
  %155 = phi double [ %207, %209 ], [ %349, %348 ]
  %156 = getelementptr inbounds i32, i32* %1, i64 %154
  %157 = load i32, i32* %156, align 4, !tbaa !19
  %158 = sext i32 %157 to i64
  %159 = getelementptr inbounds i32, i32* %26, i64 %158
  %160 = load i32, i32* %159, align 4, !tbaa !19
  %161 = icmp slt i32 %160, %48
  br i1 %161, label %170, label %162

; <label>:162:                                    ; preds = %153
  %163 = getelementptr inbounds double, double* %2, i64 %154
  %164 = load double, double* %163, align 8, !tbaa !25
  %165 = fcmp olt double %164, 0.000000e+00
  %166 = fsub double -0.000000e+00, %164
  %167 = select i1 %165, double %166, double %164
  %168 = fcmp ogt double %167, %155
  br i1 %168, label %169, label %170

; <label>:169:                                    ; preds = %162
  br label %170

; <label>:170:                                    ; preds = %169, %162, %153
  %171 = phi double [ %155, %153 ], [ %167, %169 ], [ %155, %162 ]
  %172 = add nsw i64 %154, 1
  %173 = getelementptr inbounds i32, i32* %1, i64 %172
  %174 = load i32, i32* %173, align 4, !tbaa !19
  %175 = sext i32 %174 to i64
  %176 = getelementptr inbounds i32, i32* %26, i64 %175
  %177 = load i32, i32* %176, align 4, !tbaa !19
  %178 = icmp slt i32 %177, %48
  br i1 %178, label %348, label %340

; <label>:179:                                    ; preds = %72
  %180 = sext i32 %84 to i64
  %181 = sext i32 %81 to i64
  %182 = sub nsw i64 %181, %180
  %183 = add nsw i64 %181, -1
  %184 = and i64 %182, 1
  %185 = icmp eq i64 %184, 0
  br i1 %185, label %204, label %186

; <label>:186:                                    ; preds = %179
  %187 = getelementptr inbounds i32, i32* %1, i64 %180
  %188 = load i32, i32* %187, align 4, !tbaa !19
  %189 = sext i32 %188 to i64
  %190 = getelementptr inbounds i32, i32* %26, i64 %189
  %191 = load i32, i32* %190, align 4, !tbaa !19
  %192 = icmp slt i32 %191, %48
  br i1 %192, label %201, label %193

; <label>:193:                                    ; preds = %186
  %194 = getelementptr inbounds double, double* %2, i64 %180
  %195 = load double, double* %194, align 8, !tbaa !25
  %196 = fcmp olt double %195, 0.000000e+00
  %197 = fsub double -0.000000e+00, %195
  %198 = select i1 %196, double %197, double %195
  %199 = fcmp ogt double %198, 0.000000e+00
  br i1 %199, label %200, label %201

; <label>:200:                                    ; preds = %193
  br label %201

; <label>:201:                                    ; preds = %200, %193, %186
  %202 = phi double [ 0.000000e+00, %186 ], [ %198, %200 ], [ 0.000000e+00, %193 ]
  %203 = add nsw i64 %180, 1
  br label %204

; <label>:204:                                    ; preds = %201, %179
  %205 = phi double [ %202, %201 ], [ undef, %179 ]
  %206 = phi i64 [ %203, %201 ], [ %180, %179 ]
  %207 = phi double [ %202, %201 ], [ 0.000000e+00, %179 ]
  %208 = icmp eq i64 %183, %180
  br i1 %208, label %86, label %209

; <label>:209:                                    ; preds = %204
  br label %153

; <label>:210:                                    ; preds = %86
  %211 = zext i32 %93 to i64
  %212 = and i64 %211, 1
  %213 = icmp eq i32 %93, 1
  br i1 %213, label %100, label %214

; <label>:214:                                    ; preds = %210
  %215 = sub nsw i64 %211, %212
  br label %131

; <label>:216:                                    ; preds = %70, %327
  %217 = phi i64 [ %329, %327 ], [ 0, %70 ]
  %218 = phi double [ %328, %327 ], [ 1.000000e+00, %70 ]
  %219 = add nsw i64 %217, %61
  %220 = getelementptr inbounds i32, i32* %30, i64 %219
  %221 = load i32, i32* %220, align 4, !tbaa !19
  %222 = add nsw i32 %221, 1
  %223 = sext i32 %222 to i64
  %224 = getelementptr inbounds i32, i32* %0, i64 %223
  %225 = load i32, i32* %224, align 4, !tbaa !19
  %226 = sext i32 %221 to i64
  %227 = getelementptr inbounds i32, i32* %0, i64 %226
  %228 = load i32, i32* %227, align 4, !tbaa !19
  %229 = icmp slt i32 %228, %225
  br i1 %229, label %230, label %258

; <label>:230:                                    ; preds = %216
  %231 = sext i32 %228 to i64
  %232 = sext i32 %225 to i64
  br label %233

; <label>:233:                                    ; preds = %254, %230
  %234 = phi i64 [ %231, %230 ], [ %256, %254 ]
  %235 = phi double [ 0.000000e+00, %230 ], [ %255, %254 ]
  %236 = getelementptr inbounds i32, i32* %1, i64 %234
  %237 = load i32, i32* %236, align 4, !tbaa !19
  %238 = sext i32 %237 to i64
  %239 = getelementptr inbounds i32, i32* %26, i64 %238
  %240 = load i32, i32* %239, align 4, !tbaa !19
  %241 = icmp slt i32 %240, %48
  br i1 %241, label %254, label %242

; <label>:242:                                    ; preds = %233
  %243 = getelementptr inbounds double, double* %2, i64 %234
  %244 = load double, double* %243, align 8, !tbaa !25
  %245 = sext i32 %240 to i64
  %246 = getelementptr inbounds double, double* %28, i64 %245
  %247 = load double, double* %246, align 8, !tbaa !25
  %248 = fdiv double %244, %247
  %249 = fcmp olt double %248, 0.000000e+00
  %250 = fsub double -0.000000e+00, %248
  %251 = select i1 %249, double %250, double %248
  %252 = fcmp ogt double %251, %235
  br i1 %252, label %253, label %254

; <label>:253:                                    ; preds = %242
  br label %254

; <label>:254:                                    ; preds = %242, %253, %233
  %255 = phi double [ %235, %233 ], [ %251, %253 ], [ %235, %242 ]
  %256 = add nsw i64 %234, 1
  %257 = icmp eq i64 %256, %232
  br i1 %257, label %258, label %233

; <label>:258:                                    ; preds = %254, %216
  %259 = phi double [ 0.000000e+00, %216 ], [ %255, %254 ]
  %260 = getelementptr inbounds i32, i32* %62, i64 %217
  %261 = load i32, i32* %260, align 4, !tbaa !19
  %262 = sext i32 %261 to i64
  %263 = getelementptr inbounds double, double* %59, i64 %262
  %264 = getelementptr inbounds i32, i32* %64, i64 %217
  %265 = load i32, i32* %264, align 4, !tbaa !19
  %266 = sext i32 %265 to i64
  %267 = shl nsw i64 %266, 2
  %268 = add nsw i64 %267, 7
  %269 = lshr i64 %268, 3
  %270 = getelementptr inbounds double, double* %263, i64 %269
  %271 = icmp sgt i32 %265, 0
  br i1 %271, label %272, label %313

; <label>:272:                                    ; preds = %258
  %273 = zext i32 %265 to i64
  %274 = and i64 %273, 1
  %275 = icmp eq i32 %265, 1
  br i1 %275, label %300, label %276

; <label>:276:                                    ; preds = %272
  %277 = sub nsw i64 %273, %274
  br label %278

; <label>:278:                                    ; preds = %278, %276
  %279 = phi i64 [ 0, %276 ], [ %297, %278 ]
  %280 = phi double [ 0.000000e+00, %276 ], [ %296, %278 ]
  %281 = phi i64 [ %277, %276 ], [ %298, %278 ]
  %282 = getelementptr inbounds double, double* %270, i64 %279
  %283 = load double, double* %282, align 8, !tbaa !25
  %284 = fcmp olt double %283, 0.000000e+00
  %285 = fsub double -0.000000e+00, %283
  %286 = select i1 %284, double %285, double %283
  %287 = fcmp ogt double %286, %280
  %288 = select i1 %287, double %286, double %280
  %289 = or i64 %279, 1
  %290 = getelementptr inbounds double, double* %270, i64 %289
  %291 = load double, double* %290, align 8, !tbaa !25
  %292 = fcmp olt double %291, 0.000000e+00
  %293 = fsub double -0.000000e+00, %291
  %294 = select i1 %292, double %293, double %291
  %295 = fcmp ogt double %294, %288
  %296 = select i1 %295, double %294, double %288
  %297 = add nuw nsw i64 %279, 2
  %298 = add i64 %281, -2
  %299 = icmp eq i64 %298, 0
  br i1 %299, label %300, label %278

; <label>:300:                                    ; preds = %278, %272
  %301 = phi double [ undef, %272 ], [ %296, %278 ]
  %302 = phi i64 [ 0, %272 ], [ %297, %278 ]
  %303 = phi double [ 0.000000e+00, %272 ], [ %296, %278 ]
  %304 = icmp eq i64 %274, 0
  br i1 %304, label %313, label %305

; <label>:305:                                    ; preds = %300
  %306 = getelementptr inbounds double, double* %270, i64 %302
  %307 = load double, double* %306, align 8, !tbaa !25
  %308 = fcmp olt double %307, 0.000000e+00
  %309 = fsub double -0.000000e+00, %307
  %310 = select i1 %308, double %309, double %307
  %311 = fcmp ogt double %310, %303
  %312 = select i1 %311, double %310, double %303
  br label %313

; <label>:313:                                    ; preds = %305, %300, %258
  %314 = phi double [ 0.000000e+00, %258 ], [ %301, %300 ], [ %312, %305 ]
  %315 = getelementptr inbounds double, double* %66, i64 %217
  %316 = load double, double* %315, align 8, !tbaa !25
  %317 = fcmp olt double %316, 0.000000e+00
  %318 = fsub double -0.000000e+00, %316
  %319 = select i1 %317, double %318, double %316
  %320 = fcmp ogt double %319, %314
  %321 = select i1 %320, double %319, double %314
  %322 = fcmp oeq double %321, 0.000000e+00
  br i1 %322, label %327, label %323

; <label>:323:                                    ; preds = %313
  %324 = fdiv double %259, %321
  %325 = fcmp olt double %324, %218
  br i1 %325, label %326, label %327

; <label>:326:                                    ; preds = %323
  br label %327

; <label>:327:                                    ; preds = %323, %326, %313
  %328 = phi double [ %218, %313 ], [ %324, %326 ], [ %218, %323 ]
  %329 = add nuw nsw i64 %217, 1
  %330 = icmp eq i64 %329, %69
  br i1 %330, label %331, label %216

; <label>:331:                                    ; preds = %327, %127, %55
  %332 = phi double [ 1.000000e+00, %55 ], [ %128, %127 ], [ %328, %327 ]
  %333 = fcmp olt double %332, %47
  br i1 %333, label %334, label %335

; <label>:334:                                    ; preds = %331
  store double %332, double* %31, align 8, !tbaa !11
  br label %335

; <label>:335:                                    ; preds = %331, %334, %46
  %336 = phi double [ %47, %331 ], [ %332, %334 ], [ %47, %46 ]
  %337 = icmp slt i64 %50, %44
  br i1 %337, label %46, label %338

; <label>:338:                                    ; preds = %335, %23, %6, %20, %16
  %339 = phi i32 [ 0, %16 ], [ 1, %20 ], [ 0, %6 ], [ 1, %23 ], [ 1, %335 ]
  ret i32 %339

; <label>:340:                                    ; preds = %170
  %341 = getelementptr inbounds double, double* %2, i64 %172
  %342 = load double, double* %341, align 8, !tbaa !25
  %343 = fcmp olt double %342, 0.000000e+00
  %344 = fsub double -0.000000e+00, %342
  %345 = select i1 %343, double %344, double %342
  %346 = fcmp ogt double %345, %171
  br i1 %346, label %347, label %348

; <label>:347:                                    ; preds = %340
  br label %348

; <label>:348:                                    ; preds = %347, %340, %170
  %349 = phi double [ %171, %170 ], [ %345, %347 ], [ %171, %340 ]
  %350 = add nsw i64 %154, 2
  %351 = icmp eq i64 %350, %181
  br i1 %351, label %86, label %153
}

; Function Attrs: nounwind ssp uwtable
define i32 @klu_condest(i32* readonly, double* readonly, %struct.klu_symbolic*, %struct.klu_numeric*, %struct.klu_common_struct*) local_unnamed_addr #1 {
  %6 = icmp eq %struct.klu_common_struct* %4, null
  br i1 %6, label %729, label %7

; <label>:7:                                      ; preds = %5
  %8 = icmp eq %struct.klu_symbolic* %2, null
  %9 = icmp eq i32* %0, null
  %10 = or i1 %9, %8
  %11 = icmp eq double* %1, null
  %12 = or i1 %11, %10
  br i1 %12, label %13, label %15

; <label>:13:                                     ; preds = %7
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -3, i32* %14, align 4, !tbaa !3
  br label %729

; <label>:15:                                     ; preds = %7
  %16 = icmp eq %struct.klu_numeric* %3, null
  br i1 %16, label %17, label %20

; <label>:17:                                     ; preds = %15
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19
  store double 0x7FF0000000000000, double* %18, align 8, !tbaa !26
  %19 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 1, i32* %19, align 4, !tbaa !3
  br label %729

; <label>:20:                                     ; preds = %15
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 0, i32* %21, align 4, !tbaa !3
  %22 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %2, i64 0, i32 5
  %23 = load i32, i32* %22, align 8, !tbaa !27
  %24 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 14
  %25 = bitcast i8** %24 to double**
  %26 = load double*, double** %25, align 8, !tbaa !24
  %27 = icmp sgt i32 %23, 0
  br i1 %27, label %28, label %121

; <label>:28:                                     ; preds = %20
  %29 = sext i32 %23 to i64
  br label %32

; <label>:30:                                     ; preds = %32
  %31 = icmp slt i64 %40, %29
  br i1 %31, label %32, label %44

; <label>:32:                                     ; preds = %28, %30
  %33 = phi i64 [ 0, %28 ], [ %40, %30 ]
  %34 = getelementptr inbounds double, double* %26, i64 %33
  %35 = load double, double* %34, align 8, !tbaa !25
  %36 = fcmp olt double %35, 0.000000e+00
  %37 = fsub double -0.000000e+00, %35
  %38 = select i1 %36, double %37, double %35
  %39 = fcmp oeq double %38, 0.000000e+00
  %40 = add nuw nsw i64 %33, 1
  br i1 %39, label %41, label %30

; <label>:41:                                     ; preds = %32
  %42 = fdiv double 1.000000e+00, %38
  %43 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19
  store double %42, double* %43, align 8, !tbaa !26
  store i32 1, i32* %21, align 4, !tbaa !3
  br label %729

; <label>:44:                                     ; preds = %30
  br i1 %27, label %45, label %121

; <label>:45:                                     ; preds = %44
  %46 = load i32, i32* %0, align 4, !tbaa !19
  %47 = zext i32 %23 to i64
  br label %48

; <label>:48:                                     ; preds = %116, %45
  %49 = phi i32 [ %46, %45 ], [ %54, %116 ]
  %50 = phi i64 [ 0, %45 ], [ %52, %116 ]
  %51 = phi double [ 0.000000e+00, %45 ], [ %119, %116 ]
  %52 = add nuw nsw i64 %50, 1
  %53 = getelementptr inbounds i32, i32* %0, i64 %52
  %54 = load i32, i32* %53, align 4, !tbaa !19
  %55 = icmp slt i32 %49, %54
  br i1 %55, label %56, label %116

; <label>:56:                                     ; preds = %48
  %57 = sext i32 %49 to i64
  %58 = sext i32 %54 to i64
  %59 = sub nsw i64 %58, %57
  %60 = add nsw i64 %58, -1
  %61 = sub nsw i64 %60, %57
  %62 = and i64 %59, 3
  %63 = icmp eq i64 %62, 0
  br i1 %63, label %78, label %64

; <label>:64:                                     ; preds = %56
  br label %65

; <label>:65:                                     ; preds = %65, %64
  %66 = phi i64 [ %57, %64 ], [ %75, %65 ]
  %67 = phi double [ 0.000000e+00, %64 ], [ %74, %65 ]
  %68 = phi i64 [ %62, %64 ], [ %76, %65 ]
  %69 = getelementptr inbounds double, double* %1, i64 %66
  %70 = load double, double* %69, align 8, !tbaa !25
  %71 = fcmp olt double %70, 0.000000e+00
  %72 = fsub double -0.000000e+00, %70
  %73 = select i1 %71, double %72, double %70
  %74 = fadd double %67, %73
  %75 = add nsw i64 %66, 1
  %76 = add i64 %68, -1
  %77 = icmp eq i64 %76, 0
  br i1 %77, label %78, label %65, !llvm.loop !28

; <label>:78:                                     ; preds = %65, %56
  %79 = phi double [ undef, %56 ], [ %74, %65 ]
  %80 = phi i64 [ %57, %56 ], [ %75, %65 ]
  %81 = phi double [ 0.000000e+00, %56 ], [ %74, %65 ]
  %82 = icmp ult i64 %61, 3
  br i1 %82, label %116, label %83

; <label>:83:                                     ; preds = %78
  br label %84

; <label>:84:                                     ; preds = %84, %83
  %85 = phi i64 [ %80, %83 ], [ %114, %84 ]
  %86 = phi double [ %81, %83 ], [ %113, %84 ]
  %87 = getelementptr inbounds double, double* %1, i64 %85
  %88 = load double, double* %87, align 8, !tbaa !25
  %89 = fcmp olt double %88, 0.000000e+00
  %90 = fsub double -0.000000e+00, %88
  %91 = select i1 %89, double %90, double %88
  %92 = fadd double %86, %91
  %93 = add nsw i64 %85, 1
  %94 = getelementptr inbounds double, double* %1, i64 %93
  %95 = load double, double* %94, align 8, !tbaa !25
  %96 = fcmp olt double %95, 0.000000e+00
  %97 = fsub double -0.000000e+00, %95
  %98 = select i1 %96, double %97, double %95
  %99 = fadd double %92, %98
  %100 = add nsw i64 %85, 2
  %101 = getelementptr inbounds double, double* %1, i64 %100
  %102 = load double, double* %101, align 8, !tbaa !25
  %103 = fcmp olt double %102, 0.000000e+00
  %104 = fsub double -0.000000e+00, %102
  %105 = select i1 %103, double %104, double %102
  %106 = fadd double %99, %105
  %107 = add nsw i64 %85, 3
  %108 = getelementptr inbounds double, double* %1, i64 %107
  %109 = load double, double* %108, align 8, !tbaa !25
  %110 = fcmp olt double %109, 0.000000e+00
  %111 = fsub double -0.000000e+00, %109
  %112 = select i1 %110, double %111, double %109
  %113 = fadd double %106, %112
  %114 = add nsw i64 %85, 4
  %115 = icmp eq i64 %114, %58
  br i1 %115, label %116, label %84

; <label>:116:                                    ; preds = %78, %84, %48
  %117 = phi double [ 0.000000e+00, %48 ], [ %79, %78 ], [ %113, %84 ]
  %118 = fcmp ogt double %117, %51
  %119 = select i1 %118, double %117, double %51
  %120 = icmp eq i64 %52, %47
  br i1 %120, label %127, label %48

; <label>:121:                                    ; preds = %44, %20
  %122 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 18
  %123 = bitcast i8** %122 to double**
  %124 = load double*, double** %123, align 8, !tbaa !30
  %125 = sext i32 %23 to i64
  %126 = getelementptr inbounds double, double* %124, i64 %125
  br label %605

; <label>:127:                                    ; preds = %116
  %128 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %3, i64 0, i32 18
  %129 = bitcast i8** %128 to double**
  %130 = load double*, double** %129, align 8, !tbaa !30
  %131 = sext i32 %23 to i64
  %132 = getelementptr inbounds double, double* %130, i64 %131
  %133 = bitcast double* %132 to i8*
  %134 = getelementptr inbounds double, double* %132, i64 %131
  br i1 %27, label %135, label %605

; <label>:135:                                    ; preds = %127
  %136 = sitofp i32 %23 to double
  %137 = fdiv double 1.000000e+00, %136
  %138 = zext i32 %23 to i64
  %139 = icmp ult i32 %23, 4
  br i1 %139, label %223, label %140

; <label>:140:                                    ; preds = %135
  %141 = shl nsw i64 %29, 1
  %142 = add nsw i64 %141, %47
  %143 = getelementptr double, double* %130, i64 %142
  %144 = add nsw i64 %29, %47
  %145 = getelementptr double, double* %130, i64 %144
  %146 = icmp ult double* %134, %145
  %147 = icmp ult double* %132, %143
  %148 = and i1 %146, %147
  br i1 %148, label %223, label %149

; <label>:149:                                    ; preds = %140
  %150 = and i64 %47, 4294967292
  %151 = insertelement <2 x double> undef, double %137, i32 0
  %152 = shufflevector <2 x double> %151, <2 x double> undef, <2 x i32> zeroinitializer
  %153 = insertelement <2 x double> undef, double %137, i32 0
  %154 = shufflevector <2 x double> %153, <2 x double> undef, <2 x i32> zeroinitializer
  %155 = add nsw i64 %150, -4
  %156 = lshr exact i64 %155, 2
  %157 = add nuw nsw i64 %156, 1
  %158 = and i64 %157, 3
  %159 = icmp ult i64 %155, 12
  br i1 %159, label %203, label %160

; <label>:160:                                    ; preds = %149
  %161 = sub nsw i64 %157, %158
  br label %162

; <label>:162:                                    ; preds = %162, %160
  %163 = phi i64 [ 0, %160 ], [ %200, %162 ]
  %164 = phi i64 [ %161, %160 ], [ %201, %162 ]
  %165 = getelementptr inbounds double, double* %134, i64 %163
  %166 = bitcast double* %165 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %166, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %167 = getelementptr double, double* %165, i64 2
  %168 = bitcast double* %167 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %168, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %169 = getelementptr inbounds double, double* %132, i64 %163
  %170 = bitcast double* %169 to <2 x double>*
  store <2 x double> %152, <2 x double>* %170, align 8, !tbaa !25, !alias.scope !34
  %171 = getelementptr double, double* %169, i64 2
  %172 = bitcast double* %171 to <2 x double>*
  store <2 x double> %154, <2 x double>* %172, align 8, !tbaa !25, !alias.scope !34
  %173 = or i64 %163, 4
  %174 = getelementptr inbounds double, double* %134, i64 %173
  %175 = bitcast double* %174 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %175, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %176 = getelementptr double, double* %174, i64 2
  %177 = bitcast double* %176 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %177, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %178 = getelementptr inbounds double, double* %132, i64 %173
  %179 = bitcast double* %178 to <2 x double>*
  store <2 x double> %152, <2 x double>* %179, align 8, !tbaa !25, !alias.scope !34
  %180 = getelementptr double, double* %178, i64 2
  %181 = bitcast double* %180 to <2 x double>*
  store <2 x double> %154, <2 x double>* %181, align 8, !tbaa !25, !alias.scope !34
  %182 = or i64 %163, 8
  %183 = getelementptr inbounds double, double* %134, i64 %182
  %184 = bitcast double* %183 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %184, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %185 = getelementptr double, double* %183, i64 2
  %186 = bitcast double* %185 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %186, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %187 = getelementptr inbounds double, double* %132, i64 %182
  %188 = bitcast double* %187 to <2 x double>*
  store <2 x double> %152, <2 x double>* %188, align 8, !tbaa !25, !alias.scope !34
  %189 = getelementptr double, double* %187, i64 2
  %190 = bitcast double* %189 to <2 x double>*
  store <2 x double> %154, <2 x double>* %190, align 8, !tbaa !25, !alias.scope !34
  %191 = or i64 %163, 12
  %192 = getelementptr inbounds double, double* %134, i64 %191
  %193 = bitcast double* %192 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %193, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %194 = getelementptr double, double* %192, i64 2
  %195 = bitcast double* %194 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %195, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %196 = getelementptr inbounds double, double* %132, i64 %191
  %197 = bitcast double* %196 to <2 x double>*
  store <2 x double> %152, <2 x double>* %197, align 8, !tbaa !25, !alias.scope !34
  %198 = getelementptr double, double* %196, i64 2
  %199 = bitcast double* %198 to <2 x double>*
  store <2 x double> %154, <2 x double>* %199, align 8, !tbaa !25, !alias.scope !34
  %200 = add i64 %163, 16
  %201 = add i64 %164, -4
  %202 = icmp eq i64 %201, 0
  br i1 %202, label %203, label %162, !llvm.loop !36

; <label>:203:                                    ; preds = %162, %149
  %204 = phi i64 [ 0, %149 ], [ %200, %162 ]
  %205 = icmp eq i64 %158, 0
  br i1 %205, label %221, label %206

; <label>:206:                                    ; preds = %203
  br label %207

; <label>:207:                                    ; preds = %207, %206
  %208 = phi i64 [ %204, %206 ], [ %218, %207 ]
  %209 = phi i64 [ %158, %206 ], [ %219, %207 ]
  %210 = getelementptr inbounds double, double* %134, i64 %208
  %211 = bitcast double* %210 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %211, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %212 = getelementptr double, double* %210, i64 2
  %213 = bitcast double* %212 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %213, align 8, !tbaa !25, !alias.scope !31, !noalias !34
  %214 = getelementptr inbounds double, double* %132, i64 %208
  %215 = bitcast double* %214 to <2 x double>*
  store <2 x double> %152, <2 x double>* %215, align 8, !tbaa !25, !alias.scope !34
  %216 = getelementptr double, double* %214, i64 2
  %217 = bitcast double* %216 to <2 x double>*
  store <2 x double> %154, <2 x double>* %217, align 8, !tbaa !25, !alias.scope !34
  %218 = add i64 %208, 4
  %219 = add i64 %209, -1
  %220 = icmp eq i64 %219, 0
  br i1 %220, label %221, label %207, !llvm.loop !38

; <label>:221:                                    ; preds = %207, %203
  %222 = icmp eq i64 %150, %47
  br i1 %222, label %257, label %223

; <label>:223:                                    ; preds = %221, %140, %135
  %224 = phi i64 [ 0, %140 ], [ 0, %135 ], [ %150, %221 ]
  %225 = add nsw i64 %47, -1
  %226 = sub nsw i64 %225, %224
  %227 = and i64 %47, 3
  %228 = icmp eq i64 %227, 0
  br i1 %228, label %238, label %229

; <label>:229:                                    ; preds = %223
  br label %230

; <label>:230:                                    ; preds = %230, %229
  %231 = phi i64 [ %235, %230 ], [ %224, %229 ]
  %232 = phi i64 [ %236, %230 ], [ %227, %229 ]
  %233 = getelementptr inbounds double, double* %134, i64 %231
  store double 0.000000e+00, double* %233, align 8, !tbaa !25
  %234 = getelementptr inbounds double, double* %132, i64 %231
  store double %137, double* %234, align 8, !tbaa !25
  %235 = add nuw nsw i64 %231, 1
  %236 = add i64 %232, -1
  %237 = icmp eq i64 %236, 0
  br i1 %237, label %238, label %230, !llvm.loop !39

; <label>:238:                                    ; preds = %230, %223
  %239 = phi i64 [ %224, %223 ], [ %235, %230 ]
  %240 = icmp ult i64 %226, 3
  br i1 %240, label %257, label %241

; <label>:241:                                    ; preds = %238
  br label %242

; <label>:242:                                    ; preds = %242, %241
  %243 = phi i64 [ %239, %241 ], [ %255, %242 ]
  %244 = getelementptr inbounds double, double* %134, i64 %243
  store double 0.000000e+00, double* %244, align 8, !tbaa !25
  %245 = getelementptr inbounds double, double* %132, i64 %243
  store double %137, double* %245, align 8, !tbaa !25
  %246 = add nuw nsw i64 %243, 1
  %247 = getelementptr inbounds double, double* %134, i64 %246
  store double 0.000000e+00, double* %247, align 8, !tbaa !25
  %248 = getelementptr inbounds double, double* %132, i64 %246
  store double %137, double* %248, align 8, !tbaa !25
  %249 = add nsw i64 %243, 2
  %250 = getelementptr inbounds double, double* %134, i64 %249
  store double 0.000000e+00, double* %250, align 8, !tbaa !25
  %251 = getelementptr inbounds double, double* %132, i64 %249
  store double %137, double* %251, align 8, !tbaa !25
  %252 = add nsw i64 %243, 3
  %253 = getelementptr inbounds double, double* %134, i64 %252
  store double 0.000000e+00, double* %253, align 8, !tbaa !25
  %254 = getelementptr inbounds double, double* %132, i64 %252
  store double %137, double* %254, align 8, !tbaa !25
  %255 = add nsw i64 %243, 4
  %256 = icmp eq i64 %255, %138
  br i1 %256, label %257, label %242, !llvm.loop !40

; <label>:257:                                    ; preds = %238, %242, %221
  br i1 %27, label %258, label %605

; <label>:258:                                    ; preds = %257
  %259 = zext i32 %23 to i64
  %260 = shl nuw nsw i64 %259, 3
  %261 = add nsw i64 %29, %47
  %262 = getelementptr double, double* %130, i64 %261
  %263 = shl nsw i64 %29, 1
  %264 = add nsw i64 %263, %47
  %265 = getelementptr double, double* %130, i64 %264
  %266 = add nsw i64 %259, -1
  %267 = and i64 %259, 4294967292
  %268 = add nsw i64 %267, -4
  %269 = lshr exact i64 %268, 2
  %270 = add nuw nsw i64 %269, 1
  %271 = add nsw i64 %259, -1
  %272 = and i64 %259, 3
  %273 = icmp ult i64 %266, 3
  %274 = sub nsw i64 %259, %272
  %275 = icmp eq i64 %272, 0
  %276 = and i64 %259, 1
  %277 = icmp eq i64 %266, 0
  %278 = sub nsw i64 %259, %276
  %279 = icmp eq i64 %276, 0
  %280 = icmp ult i32 %23, 4
  %281 = and i64 %259, 1
  %282 = icmp eq i64 %266, 0
  %283 = icmp ult double* %132, %265
  %284 = icmp ult double* %134, %262
  %285 = and i1 %283, %284
  %286 = sub nsw i64 %259, %281
  %287 = icmp eq i64 %281, 0
  %288 = and i64 %47, 4294967292
  %289 = and i64 %270, 3
  %290 = icmp ult i64 %268, 12
  %291 = sub nsw i64 %270, %289
  %292 = icmp eq i64 %289, 0
  %293 = icmp eq i64 %288, %47
  br label %294

; <label>:294:                                    ; preds = %334, %258
  %295 = phi i32 [ 0, %258 ], [ %335, %334 ]
  %296 = phi i32 [ 0, %258 ], [ %338, %334 ]
  %297 = phi double [ 0.000000e+00, %258 ], [ %499, %334 ]
  %298 = icmp ne i32 %296, 0
  br i1 %298, label %501, label %504

; <label>:299:                                    ; preds = %732, %498
  %300 = phi i32 [ undef, %498 ], [ %733, %732 ]
  %301 = phi i64 [ 0, %498 ], [ %734, %732 ]
  %302 = phi i32 [ 1, %498 ], [ %733, %732 ]
  br i1 %279, label %314, label %303

; <label>:303:                                    ; preds = %299
  %304 = getelementptr inbounds double, double* %132, i64 %301
  %305 = load double, double* %304, align 8, !tbaa !25
  %306 = fcmp oge double %305, 0.000000e+00
  %307 = select i1 %306, double 1.000000e+00, double -1.000000e+00
  %308 = getelementptr inbounds double, double* %134, i64 %301
  %309 = load double, double* %308, align 8, !tbaa !25
  %310 = fptosi double %309 to i32
  %311 = sitofp i32 %310 to double
  %312 = fcmp une double %307, %311
  br i1 %312, label %313, label %314

; <label>:313:                                    ; preds = %303
  store double %307, double* %308, align 8, !tbaa !25
  br label %314

; <label>:314:                                    ; preds = %313, %303, %299
  %315 = phi i32 [ %300, %299 ], [ 0, %313 ], [ %302, %303 ]
  br i1 %298, label %316, label %507

; <label>:316:                                    ; preds = %314
  %317 = fcmp ole double %499, %297
  %318 = icmp ne i32 %315, 0
  %319 = or i1 %317, %318
  br i1 %319, label %611, label %507

; <label>:320:                                    ; preds = %342, %602
  %321 = phi i32 [ undef, %602 ], [ %364, %342 ]
  %322 = phi i64 [ 0, %602 ], [ %366, %342 ]
  %323 = phi double [ 0.000000e+00, %602 ], [ %365, %342 ]
  %324 = phi i32 [ 0, %602 ], [ %364, %342 ]
  br i1 %287, label %334, label %325

; <label>:325:                                    ; preds = %320
  %326 = getelementptr inbounds double, double* %132, i64 %322
  %327 = load double, double* %326, align 8, !tbaa !25
  %328 = fcmp olt double %327, 0.000000e+00
  %329 = fsub double -0.000000e+00, %327
  %330 = select i1 %328, double %329, double %327
  %331 = fcmp ogt double %330, %323
  %332 = trunc i64 %322 to i32
  %333 = select i1 %331, i32 %332, i32 %324
  br label %334

; <label>:334:                                    ; preds = %320, %325
  %335 = phi i32 [ %321, %320 ], [ %333, %325 ]
  %336 = icmp eq i32 %335, %295
  %337 = and i1 %298, %336
  %338 = add nuw nsw i32 %296, 1
  %339 = xor i1 %337, true
  %340 = icmp ult i32 %338, 5
  %341 = and i1 %340, %339
  br i1 %341, label %294, label %611

; <label>:342:                                    ; preds = %342, %604
  %343 = phi i64 [ 0, %604 ], [ %366, %342 ]
  %344 = phi double [ 0.000000e+00, %604 ], [ %365, %342 ]
  %345 = phi i32 [ 0, %604 ], [ %364, %342 ]
  %346 = phi i64 [ %286, %604 ], [ %367, %342 ]
  %347 = getelementptr inbounds double, double* %132, i64 %343
  %348 = load double, double* %347, align 8, !tbaa !25
  %349 = fcmp olt double %348, 0.000000e+00
  %350 = fsub double -0.000000e+00, %348
  %351 = select i1 %349, double %350, double %348
  %352 = fcmp ogt double %351, %344
  %353 = trunc i64 %343 to i32
  %354 = select i1 %352, i32 %353, i32 %345
  %355 = select i1 %352, double %351, double %344
  %356 = or i64 %343, 1
  %357 = getelementptr inbounds double, double* %132, i64 %356
  %358 = load double, double* %357, align 8, !tbaa !25
  %359 = fcmp olt double %358, 0.000000e+00
  %360 = fsub double -0.000000e+00, %358
  %361 = select i1 %359, double %360, double %358
  %362 = fcmp ogt double %361, %355
  %363 = trunc i64 %356 to i32
  %364 = select i1 %362, i32 %363, i32 %354
  %365 = select i1 %362, double %361, double %355
  %366 = add nuw nsw i64 %343, 2
  %367 = add i64 %346, -2
  %368 = icmp eq i64 %367, 0
  br i1 %368, label %320, label %342

; <label>:369:                                    ; preds = %369, %530
  %370 = phi i64 [ %528, %530 ], [ %418, %369 ]
  %371 = getelementptr inbounds double, double* %134, i64 %370
  %372 = bitcast double* %371 to i64*
  %373 = load i64, i64* %372, align 8, !tbaa !25
  %374 = getelementptr inbounds double, double* %132, i64 %370
  %375 = bitcast double* %374 to i64*
  store i64 %373, i64* %375, align 8, !tbaa !25
  %376 = add nuw nsw i64 %370, 1
  %377 = getelementptr inbounds double, double* %134, i64 %376
  %378 = bitcast double* %377 to i64*
  %379 = load i64, i64* %378, align 8, !tbaa !25
  %380 = getelementptr inbounds double, double* %132, i64 %376
  %381 = bitcast double* %380 to i64*
  store i64 %379, i64* %381, align 8, !tbaa !25
  %382 = add nsw i64 %370, 2
  %383 = getelementptr inbounds double, double* %134, i64 %382
  %384 = bitcast double* %383 to i64*
  %385 = load i64, i64* %384, align 8, !tbaa !25
  %386 = getelementptr inbounds double, double* %132, i64 %382
  %387 = bitcast double* %386 to i64*
  store i64 %385, i64* %387, align 8, !tbaa !25
  %388 = add nsw i64 %370, 3
  %389 = getelementptr inbounds double, double* %134, i64 %388
  %390 = bitcast double* %389 to i64*
  %391 = load i64, i64* %390, align 8, !tbaa !25
  %392 = getelementptr inbounds double, double* %132, i64 %388
  %393 = bitcast double* %392 to i64*
  store i64 %391, i64* %393, align 8, !tbaa !25
  %394 = add nsw i64 %370, 4
  %395 = getelementptr inbounds double, double* %134, i64 %394
  %396 = bitcast double* %395 to i64*
  %397 = load i64, i64* %396, align 8, !tbaa !25
  %398 = getelementptr inbounds double, double* %132, i64 %394
  %399 = bitcast double* %398 to i64*
  store i64 %397, i64* %399, align 8, !tbaa !25
  %400 = add nsw i64 %370, 5
  %401 = getelementptr inbounds double, double* %134, i64 %400
  %402 = bitcast double* %401 to i64*
  %403 = load i64, i64* %402, align 8, !tbaa !25
  %404 = getelementptr inbounds double, double* %132, i64 %400
  %405 = bitcast double* %404 to i64*
  store i64 %403, i64* %405, align 8, !tbaa !25
  %406 = add nsw i64 %370, 6
  %407 = getelementptr inbounds double, double* %134, i64 %406
  %408 = bitcast double* %407 to i64*
  %409 = load i64, i64* %408, align 8, !tbaa !25
  %410 = getelementptr inbounds double, double* %132, i64 %406
  %411 = bitcast double* %410 to i64*
  store i64 %409, i64* %411, align 8, !tbaa !25
  %412 = add nsw i64 %370, 7
  %413 = getelementptr inbounds double, double* %134, i64 %412
  %414 = bitcast double* %413 to i64*
  %415 = load i64, i64* %414, align 8, !tbaa !25
  %416 = getelementptr inbounds double, double* %132, i64 %412
  %417 = bitcast double* %416 to i64*
  store i64 %415, i64* %417, align 8, !tbaa !25
  %418 = add nsw i64 %370, 8
  %419 = icmp eq i64 %418, %259
  br i1 %419, label %602, label %369, !llvm.loop !41

; <label>:420:                                    ; preds = %732, %500
  %421 = phi i64 [ 0, %500 ], [ %734, %732 ]
  %422 = phi i32 [ 1, %500 ], [ %733, %732 ]
  %423 = phi i64 [ %278, %500 ], [ %735, %732 ]
  %424 = getelementptr inbounds double, double* %132, i64 %421
  %425 = load double, double* %424, align 8, !tbaa !25
  %426 = fcmp oge double %425, 0.000000e+00
  %427 = select i1 %426, double 1.000000e+00, double -1.000000e+00
  %428 = getelementptr inbounds double, double* %134, i64 %421
  %429 = load double, double* %428, align 8, !tbaa !25
  %430 = fptosi double %429 to i32
  %431 = sitofp i32 %430 to double
  %432 = fcmp une double %427, %431
  br i1 %432, label %433, label %434

; <label>:433:                                    ; preds = %420
  store double %427, double* %428, align 8, !tbaa !25
  br label %434

; <label>:434:                                    ; preds = %433, %420
  %435 = phi i32 [ 0, %433 ], [ %422, %420 ]
  %436 = or i64 %421, 1
  %437 = getelementptr inbounds double, double* %132, i64 %436
  %438 = load double, double* %437, align 8, !tbaa !25
  %439 = fcmp oge double %438, 0.000000e+00
  %440 = select i1 %439, double 1.000000e+00, double -1.000000e+00
  %441 = getelementptr inbounds double, double* %134, i64 %436
  %442 = load double, double* %441, align 8, !tbaa !25
  %443 = fptosi double %442 to i32
  %444 = sitofp i32 %443 to double
  %445 = fcmp une double %440, %444
  br i1 %445, label %731, label %732

; <label>:446:                                    ; preds = %446, %506
  %447 = phi i64 [ 0, %506 ], [ %477, %446 ]
  %448 = phi double [ 0.000000e+00, %506 ], [ %476, %446 ]
  %449 = phi i64 [ %274, %506 ], [ %478, %446 ]
  %450 = getelementptr inbounds double, double* %132, i64 %447
  %451 = load double, double* %450, align 8, !tbaa !25
  %452 = fcmp olt double %451, 0.000000e+00
  %453 = fsub double -0.000000e+00, %451
  %454 = select i1 %452, double %453, double %451
  %455 = fadd double %448, %454
  %456 = or i64 %447, 1
  %457 = getelementptr inbounds double, double* %132, i64 %456
  %458 = load double, double* %457, align 8, !tbaa !25
  %459 = fcmp olt double %458, 0.000000e+00
  %460 = fsub double -0.000000e+00, %458
  %461 = select i1 %459, double %460, double %458
  %462 = fadd double %455, %461
  %463 = or i64 %447, 2
  %464 = getelementptr inbounds double, double* %132, i64 %463
  %465 = load double, double* %464, align 8, !tbaa !25
  %466 = fcmp olt double %465, 0.000000e+00
  %467 = fsub double -0.000000e+00, %465
  %468 = select i1 %466, double %467, double %465
  %469 = fadd double %462, %468
  %470 = or i64 %447, 3
  %471 = getelementptr inbounds double, double* %132, i64 %470
  %472 = load double, double* %471, align 8, !tbaa !25
  %473 = fcmp olt double %472, 0.000000e+00
  %474 = fsub double -0.000000e+00, %472
  %475 = select i1 %473, double %474, double %472
  %476 = fadd double %469, %475
  %477 = add nuw nsw i64 %447, 4
  %478 = add i64 %449, -4
  %479 = icmp eq i64 %478, 0
  br i1 %479, label %480, label %446

; <label>:480:                                    ; preds = %446, %504
  %481 = phi double [ undef, %504 ], [ %476, %446 ]
  %482 = phi i64 [ 0, %504 ], [ %477, %446 ]
  %483 = phi double [ 0.000000e+00, %504 ], [ %476, %446 ]
  br i1 %275, label %498, label %484

; <label>:484:                                    ; preds = %480
  br label %485

; <label>:485:                                    ; preds = %485, %484
  %486 = phi i64 [ %482, %484 ], [ %495, %485 ]
  %487 = phi double [ %483, %484 ], [ %494, %485 ]
  %488 = phi i64 [ %272, %484 ], [ %496, %485 ]
  %489 = getelementptr inbounds double, double* %132, i64 %486
  %490 = load double, double* %489, align 8, !tbaa !25
  %491 = fcmp olt double %490, 0.000000e+00
  %492 = fsub double -0.000000e+00, %490
  %493 = select i1 %491, double %492, double %490
  %494 = fadd double %487, %493
  %495 = add nuw nsw i64 %486, 1
  %496 = add i64 %488, -1
  %497 = icmp eq i64 %496, 0
  br i1 %497, label %498, label %485, !llvm.loop !42

; <label>:498:                                    ; preds = %485, %480
  %499 = phi double [ %481, %480 ], [ %494, %485 ]
  br i1 %277, label %299, label %500

; <label>:500:                                    ; preds = %498
  br label %420

; <label>:501:                                    ; preds = %294
  call void @llvm.memset.p0i8.i64(i8* nonnull %133, i8 0, i64 %260, i32 8, i1 false)
  %502 = sext i32 %295 to i64
  %503 = getelementptr inbounds double, double* %132, i64 %502
  store double 1.000000e+00, double* %503, align 8, !tbaa !25
  br label %504

; <label>:504:                                    ; preds = %294, %501
  %505 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* nonnull %132, %struct.klu_common_struct* nonnull %4) #4
  br i1 %273, label %480, label %506

; <label>:506:                                    ; preds = %504
  br label %446

; <label>:507:                                    ; preds = %314, %316
  %508 = or i1 %280, %285
  br i1 %508, label %509, label %531

; <label>:509:                                    ; preds = %507, %601
  %510 = phi i64 [ 0, %507 ], [ %288, %601 ]
  %511 = sub nsw i64 %259, %510
  %512 = sub nsw i64 %271, %510
  %513 = and i64 %511, 7
  %514 = icmp eq i64 %513, 0
  br i1 %514, label %527, label %515

; <label>:515:                                    ; preds = %509
  br label %516

; <label>:516:                                    ; preds = %516, %515
  %517 = phi i64 [ %524, %516 ], [ %510, %515 ]
  %518 = phi i64 [ %525, %516 ], [ %513, %515 ]
  %519 = getelementptr inbounds double, double* %134, i64 %517
  %520 = bitcast double* %519 to i64*
  %521 = load i64, i64* %520, align 8, !tbaa !25
  %522 = getelementptr inbounds double, double* %132, i64 %517
  %523 = bitcast double* %522 to i64*
  store i64 %521, i64* %523, align 8, !tbaa !25
  %524 = add nuw nsw i64 %517, 1
  %525 = add i64 %518, -1
  %526 = icmp eq i64 %525, 0
  br i1 %526, label %527, label %516, !llvm.loop !43

; <label>:527:                                    ; preds = %516, %509
  %528 = phi i64 [ %510, %509 ], [ %524, %516 ]
  %529 = icmp ult i64 %512, 7
  br i1 %529, label %602, label %530

; <label>:530:                                    ; preds = %527
  br label %369

; <label>:531:                                    ; preds = %507
  br i1 %290, label %582, label %532

; <label>:532:                                    ; preds = %531
  br label %533

; <label>:533:                                    ; preds = %533, %532
  %534 = phi i64 [ 0, %532 ], [ %579, %533 ]
  %535 = phi i64 [ %291, %532 ], [ %580, %533 ]
  %536 = getelementptr inbounds double, double* %134, i64 %534
  %537 = bitcast double* %536 to <2 x i64>*
  %538 = load <2 x i64>, <2 x i64>* %537, align 8, !tbaa !25, !alias.scope !44
  %539 = getelementptr double, double* %536, i64 2
  %540 = bitcast double* %539 to <2 x i64>*
  %541 = load <2 x i64>, <2 x i64>* %540, align 8, !tbaa !25, !alias.scope !44
  %542 = getelementptr inbounds double, double* %132, i64 %534
  %543 = bitcast double* %542 to <2 x i64>*
  store <2 x i64> %538, <2 x i64>* %543, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %544 = getelementptr double, double* %542, i64 2
  %545 = bitcast double* %544 to <2 x i64>*
  store <2 x i64> %541, <2 x i64>* %545, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %546 = or i64 %534, 4
  %547 = getelementptr inbounds double, double* %134, i64 %546
  %548 = bitcast double* %547 to <2 x i64>*
  %549 = load <2 x i64>, <2 x i64>* %548, align 8, !tbaa !25, !alias.scope !44
  %550 = getelementptr double, double* %547, i64 2
  %551 = bitcast double* %550 to <2 x i64>*
  %552 = load <2 x i64>, <2 x i64>* %551, align 8, !tbaa !25, !alias.scope !44
  %553 = getelementptr inbounds double, double* %132, i64 %546
  %554 = bitcast double* %553 to <2 x i64>*
  store <2 x i64> %549, <2 x i64>* %554, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %555 = getelementptr double, double* %553, i64 2
  %556 = bitcast double* %555 to <2 x i64>*
  store <2 x i64> %552, <2 x i64>* %556, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %557 = or i64 %534, 8
  %558 = getelementptr inbounds double, double* %134, i64 %557
  %559 = bitcast double* %558 to <2 x i64>*
  %560 = load <2 x i64>, <2 x i64>* %559, align 8, !tbaa !25, !alias.scope !44
  %561 = getelementptr double, double* %558, i64 2
  %562 = bitcast double* %561 to <2 x i64>*
  %563 = load <2 x i64>, <2 x i64>* %562, align 8, !tbaa !25, !alias.scope !44
  %564 = getelementptr inbounds double, double* %132, i64 %557
  %565 = bitcast double* %564 to <2 x i64>*
  store <2 x i64> %560, <2 x i64>* %565, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %566 = getelementptr double, double* %564, i64 2
  %567 = bitcast double* %566 to <2 x i64>*
  store <2 x i64> %563, <2 x i64>* %567, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %568 = or i64 %534, 12
  %569 = getelementptr inbounds double, double* %134, i64 %568
  %570 = bitcast double* %569 to <2 x i64>*
  %571 = load <2 x i64>, <2 x i64>* %570, align 8, !tbaa !25, !alias.scope !44
  %572 = getelementptr double, double* %569, i64 2
  %573 = bitcast double* %572 to <2 x i64>*
  %574 = load <2 x i64>, <2 x i64>* %573, align 8, !tbaa !25, !alias.scope !44
  %575 = getelementptr inbounds double, double* %132, i64 %568
  %576 = bitcast double* %575 to <2 x i64>*
  store <2 x i64> %571, <2 x i64>* %576, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %577 = getelementptr double, double* %575, i64 2
  %578 = bitcast double* %577 to <2 x i64>*
  store <2 x i64> %574, <2 x i64>* %578, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %579 = add i64 %534, 16
  %580 = add i64 %535, -4
  %581 = icmp eq i64 %580, 0
  br i1 %581, label %582, label %533, !llvm.loop !49

; <label>:582:                                    ; preds = %533, %531
  %583 = phi i64 [ 0, %531 ], [ %579, %533 ]
  br i1 %292, label %601, label %584

; <label>:584:                                    ; preds = %582
  br label %585

; <label>:585:                                    ; preds = %585, %584
  %586 = phi i64 [ %583, %584 ], [ %598, %585 ]
  %587 = phi i64 [ %289, %584 ], [ %599, %585 ]
  %588 = getelementptr inbounds double, double* %134, i64 %586
  %589 = bitcast double* %588 to <2 x i64>*
  %590 = load <2 x i64>, <2 x i64>* %589, align 8, !tbaa !25, !alias.scope !44
  %591 = getelementptr double, double* %588, i64 2
  %592 = bitcast double* %591 to <2 x i64>*
  %593 = load <2 x i64>, <2 x i64>* %592, align 8, !tbaa !25, !alias.scope !44
  %594 = getelementptr inbounds double, double* %132, i64 %586
  %595 = bitcast double* %594 to <2 x i64>*
  store <2 x i64> %590, <2 x i64>* %595, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %596 = getelementptr double, double* %594, i64 2
  %597 = bitcast double* %596 to <2 x i64>*
  store <2 x i64> %593, <2 x i64>* %597, align 8, !tbaa !25, !alias.scope !47, !noalias !44
  %598 = add i64 %586, 4
  %599 = add i64 %587, -1
  %600 = icmp eq i64 %599, 0
  br i1 %600, label %601, label %585, !llvm.loop !50

; <label>:601:                                    ; preds = %585, %582
  br i1 %293, label %602, label %509

; <label>:602:                                    ; preds = %527, %369, %601
  %603 = tail call i32 @klu_tsolve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* nonnull %132, %struct.klu_common_struct* nonnull %4) #4
  br i1 %282, label %320, label %604

; <label>:604:                                    ; preds = %602
  br label %342

; <label>:605:                                    ; preds = %127, %121, %257
  %606 = phi double [ %119, %257 ], [ %119, %127 ], [ 0.000000e+00, %121 ]
  %607 = phi double* [ %132, %257 ], [ %132, %127 ], [ %126, %121 ]
  %608 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %607, %struct.klu_common_struct* nonnull %4) #4
  %609 = tail call i32 @klu_tsolve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %607, %struct.klu_common_struct* nonnull %4) #4
  store double 1.000000e+00, double* %607, align 8, !tbaa !25
  %610 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %607, %struct.klu_common_struct* nonnull %4) #4
  br label %611

; <label>:611:                                    ; preds = %316, %334, %605
  %612 = phi double [ %606, %605 ], [ %119, %334 ], [ %119, %316 ]
  %613 = phi double* [ %607, %605 ], [ %132, %334 ], [ %132, %316 ]
  %614 = phi double [ 0.000000e+00, %605 ], [ %499, %334 ], [ %499, %316 ]
  br i1 %27, label %617, label %615

; <label>:615:                                    ; preds = %611
  %616 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* %613, %struct.klu_common_struct* nonnull %4) #4
  br label %720

; <label>:617:                                    ; preds = %611
  %618 = add nsw i32 %23, -1
  %619 = sitofp i32 %618 to double
  %620 = zext i32 %23 to i64
  %621 = and i64 %620, 1
  %622 = icmp eq i32 %23, 1
  br i1 %622, label %642, label %623

; <label>:623:                                    ; preds = %617
  %624 = sub nsw i64 %620, %621
  br label %625

; <label>:625:                                    ; preds = %625, %623
  %626 = phi i64 [ 0, %623 ], [ %639, %625 ]
  %627 = phi i64 [ %624, %623 ], [ %640, %625 ]
  %628 = getelementptr inbounds double, double* %613, i64 %626
  %629 = trunc i64 %626 to i32
  %630 = sitofp i32 %629 to double
  %631 = fdiv double %630, %619
  %632 = fsub double -1.000000e+00, %631
  store double %632, double* %628, align 8, !tbaa !25
  %633 = or i64 %626, 1
  %634 = getelementptr inbounds double, double* %613, i64 %633
  %635 = trunc i64 %633 to i32
  %636 = sitofp i32 %635 to double
  %637 = fdiv double %636, %619
  %638 = fadd double %637, 1.000000e+00
  store double %638, double* %634, align 8, !tbaa !25
  %639 = add nuw nsw i64 %626, 2
  %640 = add i64 %627, -2
  %641 = icmp eq i64 %640, 0
  br i1 %641, label %642, label %625

; <label>:642:                                    ; preds = %625, %617
  %643 = phi i64 [ 0, %617 ], [ %639, %625 ]
  %644 = icmp eq i64 %621, 0
  br i1 %644, label %655, label %645

; <label>:645:                                    ; preds = %642
  %646 = getelementptr inbounds double, double* %613, i64 %643
  %647 = trunc i64 %643 to i32
  %648 = and i32 %647, 1
  %649 = icmp eq i32 %648, 0
  %650 = sitofp i32 %647 to double
  %651 = fdiv double %650, %619
  %652 = fsub double -1.000000e+00, %651
  %653 = fadd double %651, 1.000000e+00
  %654 = select i1 %649, double %652, double %653
  store double %654, double* %646, align 8, !tbaa !25
  br label %655

; <label>:655:                                    ; preds = %642, %645
  %656 = tail call i32 @klu_solve(%struct.klu_symbolic* %2, %struct.klu_numeric* nonnull %3, i32 %23, i32 1, double* nonnull %613, %struct.klu_common_struct* nonnull %4) #4
  br i1 %27, label %657, label %720

; <label>:657:                                    ; preds = %655
  %658 = zext i32 %23 to i64
  %659 = add nsw i64 %658, -1
  %660 = and i64 %658, 3
  %661 = icmp ult i64 %659, 3
  br i1 %661, label %698, label %662

; <label>:662:                                    ; preds = %657
  %663 = sub nsw i64 %658, %660
  br label %664

; <label>:664:                                    ; preds = %664, %662
  %665 = phi i64 [ 0, %662 ], [ %695, %664 ]
  %666 = phi double [ 0.000000e+00, %662 ], [ %694, %664 ]
  %667 = phi i64 [ %663, %662 ], [ %696, %664 ]
  %668 = getelementptr inbounds double, double* %613, i64 %665
  %669 = load double, double* %668, align 8, !tbaa !25
  %670 = fcmp olt double %669, 0.000000e+00
  %671 = fsub double -0.000000e+00, %669
  %672 = select i1 %670, double %671, double %669
  %673 = fadd double %666, %672
  %674 = or i64 %665, 1
  %675 = getelementptr inbounds double, double* %613, i64 %674
  %676 = load double, double* %675, align 8, !tbaa !25
  %677 = fcmp olt double %676, 0.000000e+00
  %678 = fsub double -0.000000e+00, %676
  %679 = select i1 %677, double %678, double %676
  %680 = fadd double %673, %679
  %681 = or i64 %665, 2
  %682 = getelementptr inbounds double, double* %613, i64 %681
  %683 = load double, double* %682, align 8, !tbaa !25
  %684 = fcmp olt double %683, 0.000000e+00
  %685 = fsub double -0.000000e+00, %683
  %686 = select i1 %684, double %685, double %683
  %687 = fadd double %680, %686
  %688 = or i64 %665, 3
  %689 = getelementptr inbounds double, double* %613, i64 %688
  %690 = load double, double* %689, align 8, !tbaa !25
  %691 = fcmp olt double %690, 0.000000e+00
  %692 = fsub double -0.000000e+00, %690
  %693 = select i1 %691, double %692, double %690
  %694 = fadd double %687, %693
  %695 = add nuw nsw i64 %665, 4
  %696 = add i64 %667, -4
  %697 = icmp eq i64 %696, 0
  br i1 %697, label %698, label %664

; <label>:698:                                    ; preds = %664, %657
  %699 = phi double [ undef, %657 ], [ %694, %664 ]
  %700 = phi i64 [ 0, %657 ], [ %695, %664 ]
  %701 = phi double [ 0.000000e+00, %657 ], [ %694, %664 ]
  %702 = icmp eq i64 %660, 0
  br i1 %702, label %717, label %703

; <label>:703:                                    ; preds = %698
  br label %704

; <label>:704:                                    ; preds = %704, %703
  %705 = phi i64 [ %700, %703 ], [ %714, %704 ]
  %706 = phi double [ %701, %703 ], [ %713, %704 ]
  %707 = phi i64 [ %660, %703 ], [ %715, %704 ]
  %708 = getelementptr inbounds double, double* %613, i64 %705
  %709 = load double, double* %708, align 8, !tbaa !25
  %710 = fcmp olt double %709, 0.000000e+00
  %711 = fsub double -0.000000e+00, %709
  %712 = select i1 %710, double %711, double %709
  %713 = fadd double %706, %712
  %714 = add nuw nsw i64 %705, 1
  %715 = add i64 %707, -1
  %716 = icmp eq i64 %715, 0
  br i1 %716, label %717, label %704, !llvm.loop !51

; <label>:717:                                    ; preds = %704, %698
  %718 = phi double [ %699, %698 ], [ %713, %704 ]
  %719 = fmul double %718, 2.000000e+00
  br label %720

; <label>:720:                                    ; preds = %615, %717, %655
  %721 = phi double [ 0.000000e+00, %655 ], [ %719, %717 ], [ 0.000000e+00, %615 ]
  %722 = mul nsw i32 %23, 3
  %723 = sitofp i32 %722 to double
  %724 = fdiv double %721, %723
  %725 = fcmp ogt double %724, %614
  %726 = select i1 %725, double %724, double %614
  %727 = fmul double %612, %726
  %728 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 19
  store double %727, double* %728, align 8, !tbaa !26
  br label %729

; <label>:729:                                    ; preds = %5, %720, %41, %17, %13
  %730 = phi i32 [ 0, %13 ], [ 1, %17 ], [ 1, %41 ], [ 1, %720 ], [ 0, %5 ]
  ret i32 %730

; <label>:731:                                    ; preds = %434
  store double %440, double* %441, align 8, !tbaa !25
  br label %732

; <label>:732:                                    ; preds = %731, %434
  %733 = phi i32 [ 0, %731 ], [ %435, %434 ]
  %734 = add nuw nsw i64 %421, 2
  %735 = add i64 %423, -2
  %736 = icmp eq i64 %735, 0
  br i1 %736, label %299, label %420
}

declare i32 @klu_solve(%struct.klu_symbolic*, %struct.klu_numeric*, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #2

declare i32 @klu_tsolve(%struct.klu_symbolic*, %struct.klu_numeric*, i32, i32, double*, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_flops(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %4 = icmp eq %struct.klu_common_struct* %2, null
  br i1 %4, label %141, label %5

; <label>:5:                                      ; preds = %3
  %6 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 17
  store double -1.000000e+00, double* %6, align 8, !tbaa !52
  %7 = icmp eq %struct.klu_numeric* %1, null
  %8 = icmp eq %struct.klu_symbolic* %0, null
  %9 = or i1 %8, %7
  %10 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  br i1 %9, label %11, label %12

; <label>:11:                                     ; preds = %5
  store i32 -3, i32* %10, align 4, !tbaa !3
  br label %141

; <label>:12:                                     ; preds = %5
  store i32 0, i32* %10, align 4, !tbaa !3
  %13 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 9
  %14 = load i32*, i32** %13, align 8, !tbaa !18
  %15 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 11
  %16 = load i32, i32* %15, align 4, !tbaa !17
  %17 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 12
  %18 = bitcast i8*** %17 to double***
  %19 = load double**, double*** %18, align 8, !tbaa !20
  %20 = icmp sgt i32 %16, 0
  br i1 %20, label %21, label %139

; <label>:21:                                     ; preds = %12
  %22 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 10
  %23 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 9
  %24 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 11
  %25 = load i32, i32* %14, align 4, !tbaa !19
  %26 = zext i32 %16 to i64
  br label %27

; <label>:27:                                     ; preds = %136, %21
  %28 = phi i32 [ %25, %21 ], [ %33, %136 ]
  %29 = phi i64 [ 0, %21 ], [ %31, %136 ]
  %30 = phi double [ 0.000000e+00, %21 ], [ %137, %136 ]
  %31 = add nuw nsw i64 %29, 1
  %32 = getelementptr inbounds i32, i32* %14, i64 %31
  %33 = load i32, i32* %32, align 4, !tbaa !19
  %34 = sub nsw i32 %33, %28
  %35 = icmp sgt i32 %34, 1
  br i1 %35, label %36, label %136

; <label>:36:                                     ; preds = %27
  %37 = load i32*, i32** %22, align 8, !tbaa !53
  %38 = sext i32 %28 to i64
  %39 = getelementptr inbounds i32, i32* %37, i64 %38
  %40 = load i32*, i32** %23, align 8, !tbaa !22
  %41 = getelementptr inbounds i32, i32* %40, i64 %38
  %42 = load i32*, i32** %24, align 8, !tbaa !23
  %43 = getelementptr inbounds i32, i32* %42, i64 %38
  %44 = getelementptr inbounds double*, double** %19, i64 %29
  %45 = load double*, double** %44, align 8, !tbaa !21
  %46 = zext i32 %34 to i64
  br label %47

; <label>:47:                                     ; preds = %128, %36
  %48 = phi i64 [ 0, %36 ], [ %134, %128 ]
  %49 = phi double [ %30, %36 ], [ %133, %128 ]
  %50 = getelementptr inbounds i32, i32* %41, i64 %48
  %51 = load i32, i32* %50, align 4, !tbaa !19
  %52 = sext i32 %51 to i64
  %53 = getelementptr inbounds double, double* %45, i64 %52
  %54 = bitcast double* %53 to i32*
  %55 = getelementptr inbounds i32, i32* %43, i64 %48
  %56 = load i32, i32* %55, align 4, !tbaa !19
  %57 = icmp sgt i32 %56, 0
  br i1 %57, label %58, label %128

; <label>:58:                                     ; preds = %47
  %59 = zext i32 %56 to i64
  %60 = add nsw i64 %59, -1
  %61 = and i64 %59, 3
  %62 = icmp ult i64 %60, 3
  br i1 %62, label %107, label %63

; <label>:63:                                     ; preds = %58
  %64 = sub nsw i64 %59, %61
  br label %65

; <label>:65:                                     ; preds = %65, %63
  %66 = phi i64 [ 0, %63 ], [ %104, %65 ]
  %67 = phi double [ %49, %63 ], [ %103, %65 ]
  %68 = phi i64 [ %64, %63 ], [ %105, %65 ]
  %69 = getelementptr inbounds i32, i32* %54, i64 %66
  %70 = load i32, i32* %69, align 4, !tbaa !19
  %71 = sext i32 %70 to i64
  %72 = getelementptr inbounds i32, i32* %39, i64 %71
  %73 = load i32, i32* %72, align 4, !tbaa !19
  %74 = shl nsw i32 %73, 1
  %75 = sitofp i32 %74 to double
  %76 = fadd double %67, %75
  %77 = or i64 %66, 1
  %78 = getelementptr inbounds i32, i32* %54, i64 %77
  %79 = load i32, i32* %78, align 4, !tbaa !19
  %80 = sext i32 %79 to i64
  %81 = getelementptr inbounds i32, i32* %39, i64 %80
  %82 = load i32, i32* %81, align 4, !tbaa !19
  %83 = shl nsw i32 %82, 1
  %84 = sitofp i32 %83 to double
  %85 = fadd double %76, %84
  %86 = or i64 %66, 2
  %87 = getelementptr inbounds i32, i32* %54, i64 %86
  %88 = load i32, i32* %87, align 4, !tbaa !19
  %89 = sext i32 %88 to i64
  %90 = getelementptr inbounds i32, i32* %39, i64 %89
  %91 = load i32, i32* %90, align 4, !tbaa !19
  %92 = shl nsw i32 %91, 1
  %93 = sitofp i32 %92 to double
  %94 = fadd double %85, %93
  %95 = or i64 %66, 3
  %96 = getelementptr inbounds i32, i32* %54, i64 %95
  %97 = load i32, i32* %96, align 4, !tbaa !19
  %98 = sext i32 %97 to i64
  %99 = getelementptr inbounds i32, i32* %39, i64 %98
  %100 = load i32, i32* %99, align 4, !tbaa !19
  %101 = shl nsw i32 %100, 1
  %102 = sitofp i32 %101 to double
  %103 = fadd double %94, %102
  %104 = add nuw nsw i64 %66, 4
  %105 = add i64 %68, -4
  %106 = icmp eq i64 %105, 0
  br i1 %106, label %107, label %65

; <label>:107:                                    ; preds = %65, %58
  %108 = phi double [ undef, %58 ], [ %103, %65 ]
  %109 = phi i64 [ 0, %58 ], [ %104, %65 ]
  %110 = phi double [ %49, %58 ], [ %103, %65 ]
  %111 = icmp eq i64 %61, 0
  br i1 %111, label %128, label %112

; <label>:112:                                    ; preds = %107
  br label %113

; <label>:113:                                    ; preds = %113, %112
  %114 = phi i64 [ %109, %112 ], [ %125, %113 ]
  %115 = phi double [ %110, %112 ], [ %124, %113 ]
  %116 = phi i64 [ %61, %112 ], [ %126, %113 ]
  %117 = getelementptr inbounds i32, i32* %54, i64 %114
  %118 = load i32, i32* %117, align 4, !tbaa !19
  %119 = sext i32 %118 to i64
  %120 = getelementptr inbounds i32, i32* %39, i64 %119
  %121 = load i32, i32* %120, align 4, !tbaa !19
  %122 = shl nsw i32 %121, 1
  %123 = sitofp i32 %122 to double
  %124 = fadd double %115, %123
  %125 = add nuw nsw i64 %114, 1
  %126 = add i64 %116, -1
  %127 = icmp eq i64 %126, 0
  br i1 %127, label %128, label %113, !llvm.loop !54

; <label>:128:                                    ; preds = %107, %113, %47
  %129 = phi double [ %49, %47 ], [ %108, %107 ], [ %124, %113 ]
  %130 = getelementptr inbounds i32, i32* %39, i64 %48
  %131 = load i32, i32* %130, align 4, !tbaa !19
  %132 = sitofp i32 %131 to double
  %133 = fadd double %129, %132
  %134 = add nuw nsw i64 %48, 1
  %135 = icmp eq i64 %134, %46
  br i1 %135, label %136, label %47

; <label>:136:                                    ; preds = %128, %27
  %137 = phi double [ %30, %27 ], [ %133, %128 ]
  %138 = icmp eq i64 %31, %26
  br i1 %138, label %139, label %27

; <label>:139:                                    ; preds = %136, %12
  %140 = phi double [ 0.000000e+00, %12 ], [ %137, %136 ]
  store double %140, double* %6, align 8, !tbaa !52
  br label %141

; <label>:141:                                    ; preds = %3, %139, %11
  %142 = phi i32 [ 0, %11 ], [ 1, %139 ], [ 0, %3 ]
  ret i32 %142
}

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_rcond(%struct.klu_symbolic* readonly, %struct.klu_numeric* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %4 = icmp eq %struct.klu_common_struct* %2, null
  br i1 %4, label %55, label %5

; <label>:5:                                      ; preds = %3
  %6 = icmp eq %struct.klu_symbolic* %0, null
  br i1 %6, label %7, label %9

; <label>:7:                                      ; preds = %5
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 -3, i32* %8, align 4, !tbaa !3
  br label %55

; <label>:9:                                      ; preds = %5
  %10 = icmp eq %struct.klu_numeric* %1, null
  br i1 %10, label %11, label %14

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18
  store double 0.000000e+00, double* %12, align 8, !tbaa !55
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 1, i32* %13, align 4, !tbaa !3
  br label %55

; <label>:14:                                     ; preds = %9
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 0, i32* %15, align 4, !tbaa !3
  %16 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 5
  %17 = load i32, i32* %16, align 8, !tbaa !27
  %18 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 14
  %19 = bitcast i8** %18 to double**
  %20 = load double*, double** %19, align 8, !tbaa !24
  %21 = icmp sgt i32 %17, 0
  br i1 %21, label %22, label %48

; <label>:22:                                     ; preds = %14
  %23 = sext i32 %17 to i64
  br label %24

; <label>:24:                                     ; preds = %22, %43
  %25 = phi i64 [ 0, %22 ], [ %46, %43 ]
  %26 = phi double [ 0.000000e+00, %22 ], [ %45, %43 ]
  %27 = phi double [ 0.000000e+00, %22 ], [ %44, %43 ]
  %28 = getelementptr inbounds double, double* %20, i64 %25
  %29 = load double, double* %28, align 8, !tbaa !25
  %30 = fcmp olt double %29, 0.000000e+00
  %31 = fsub double -0.000000e+00, %29
  %32 = select i1 %30, double %31, double %29
  %33 = fcmp ueq double %32, 0.000000e+00
  br i1 %33, label %34, label %36

; <label>:34:                                     ; preds = %24
  %35 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18
  store double 0.000000e+00, double* %35, align 8, !tbaa !55
  store i32 1, i32* %15, align 4, !tbaa !3
  br label %55

; <label>:36:                                     ; preds = %24
  %37 = icmp eq i64 %25, 0
  br i1 %37, label %43, label %38

; <label>:38:                                     ; preds = %36
  %39 = fcmp olt double %26, %32
  %40 = select i1 %39, double %26, double %32
  %41 = fcmp ogt double %27, %32
  %42 = select i1 %41, double %27, double %32
  br label %43

; <label>:43:                                     ; preds = %36, %38
  %44 = phi double [ %42, %38 ], [ %32, %36 ]
  %45 = phi double [ %40, %38 ], [ %32, %36 ]
  %46 = add nuw nsw i64 %25, 1
  %47 = icmp slt i64 %46, %23
  br i1 %47, label %24, label %48

; <label>:48:                                     ; preds = %43, %14
  %49 = phi double [ 0.000000e+00, %14 ], [ %44, %43 ]
  %50 = phi double [ 0.000000e+00, %14 ], [ %45, %43 ]
  %51 = fdiv double %50, %49
  %52 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 18
  store double %51, double* %52, align 8, !tbaa !55
  %53 = fcmp ueq double %51, 0.000000e+00
  br i1 %53, label %54, label %55

; <label>:54:                                     ; preds = %48
  store double 0.000000e+00, double* %52, align 8, !tbaa !55
  store i32 1, i32* %15, align 4, !tbaa !3
  br label %55

; <label>:55:                                     ; preds = %54, %48, %3, %34, %11, %7
  %56 = phi i32 [ 0, %7 ], [ 1, %11 ], [ 1, %34 ], [ 0, %3 ], [ 1, %48 ], [ 1, %54 ]
  ret i32 %56
}

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #3

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { argmemonly nounwind }
attributes #4 = { nounwind }

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
!11 = !{!4, !5, i64 128}
!12 = !{!13, !9, i64 32}
!13 = !{!"", !8, i64 0, !8, i64 4, !8, i64 8, !8, i64 12, !8, i64 16, !8, i64 20, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48, !9, i64 56, !9, i64 64, !9, i64 72, !9, i64 80, !9, i64 88, !9, i64 96, !10, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !9, i64 144, !9, i64 152, !8, i64 160}
!14 = !{!13, !9, i64 96}
!15 = !{!16, !9, i64 56}
!16 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!17 = !{!16, !8, i64 76}
!18 = !{!16, !9, i64 64}
!19 = !{!8, !8, i64 0}
!20 = !{!13, !9, i64 72}
!21 = !{!9, !9, i64 0}
!22 = !{!13, !9, i64 48}
!23 = !{!13, !9, i64 64}
!24 = !{!13, !9, i64 88}
!25 = !{!5, !5, i64 0}
!26 = !{!4, !5, i64 120}
!27 = !{!16, !8, i64 40}
!28 = distinct !{!28, !29}
!29 = !{!"llvm.loop.unroll.disable"}
!30 = !{!13, !9, i64 120}
!31 = !{!32}
!32 = distinct !{!32, !33}
!33 = distinct !{!33, !"LVerDomain"}
!34 = !{!35}
!35 = distinct !{!35, !33}
!36 = distinct !{!36, !37}
!37 = !{!"llvm.loop.isvectorized", i32 1}
!38 = distinct !{!38, !29}
!39 = distinct !{!39, !29}
!40 = distinct !{!40, !37}
!41 = distinct !{!41, !37}
!42 = distinct !{!42, !29}
!43 = distinct !{!43, !29}
!44 = !{!45}
!45 = distinct !{!45, !46}
!46 = distinct !{!46, !"LVerDomain"}
!47 = !{!48}
!48 = distinct !{!48, !46}
!49 = distinct !{!49, !37}
!50 = distinct !{!50, !29}
!51 = distinct !{!51, !29}
!52 = !{!4, !5, i64 104}
!53 = !{!13, !9, i64 56}
!54 = distinct !{!54, !29}
!55 = !{!4, !5, i64 112}
