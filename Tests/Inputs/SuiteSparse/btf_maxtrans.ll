; ModuleID = 'btf_maxtrans.c'
source_filename = "btf_maxtrans.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @btf_maxtrans(i32, i32, i32* nocapture readonly, i32* nocapture readonly, double, double* nocapture, i32* nocapture, i32* nocapture) local_unnamed_addr #0 {
  %9 = bitcast i32* %6 to i8*
  %10 = sext i32 %1 to i64
  %11 = getelementptr inbounds i32, i32* %7, i64 %10
  %12 = getelementptr inbounds i32, i32* %11, i64 %10
  %13 = getelementptr inbounds i32, i32* %12, i64 %10
  %14 = getelementptr inbounds i32, i32* %13, i64 %10
  %15 = icmp sgt i32 %1, 0
  br i1 %15, label %16, label %143

; <label>:16:                                     ; preds = %8
  %17 = zext i32 %1 to i64
  %18 = icmp ult i32 %1, 8
  br i1 %18, label %99, label %19

; <label>:19:                                     ; preds = %16
  %20 = getelementptr i32, i32* %7, i64 %17
  %21 = add nsw i64 %10, %17
  %22 = getelementptr i32, i32* %7, i64 %21
  %23 = getelementptr i32, i32* %2, i64 %17
  %24 = icmp ugt i32* %22, %7
  %25 = icmp ult i32* %11, %20
  %26 = and i1 %24, %25
  %27 = icmp ugt i32* %23, %7
  %28 = icmp ugt i32* %20, %2
  %29 = and i1 %27, %28
  %30 = or i1 %26, %29
  %31 = icmp ult i32* %11, %23
  %32 = icmp ugt i32* %22, %2
  %33 = and i1 %31, %32
  %34 = or i1 %30, %33
  br i1 %34, label %99, label %35

; <label>:35:                                     ; preds = %19
  %36 = and i64 %17, 4294967288
  %37 = add nsw i64 %36, -8
  %38 = lshr exact i64 %37, 3
  %39 = add nuw nsw i64 %38, 1
  %40 = and i64 %39, 1
  %41 = icmp eq i64 %37, 0
  br i1 %41, label %79, label %42

; <label>:42:                                     ; preds = %35
  %43 = sub nsw i64 %39, %40
  br label %44

; <label>:44:                                     ; preds = %44, %42
  %45 = phi i64 [ 0, %42 ], [ %76, %44 ]
  %46 = phi i64 [ %43, %42 ], [ %77, %44 ]
  %47 = getelementptr inbounds i32, i32* %2, i64 %45
  %48 = bitcast i32* %47 to <4 x i32>*
  %49 = load <4 x i32>, <4 x i32>* %48, align 4, !tbaa !3, !alias.scope !7
  %50 = getelementptr i32, i32* %47, i64 4
  %51 = bitcast i32* %50 to <4 x i32>*
  %52 = load <4 x i32>, <4 x i32>* %51, align 4, !tbaa !3, !alias.scope !7
  %53 = getelementptr inbounds i32, i32* %7, i64 %45
  %54 = bitcast i32* %53 to <4 x i32>*
  store <4 x i32> %49, <4 x i32>* %54, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %55 = getelementptr i32, i32* %53, i64 4
  %56 = bitcast i32* %55 to <4 x i32>*
  store <4 x i32> %52, <4 x i32>* %56, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %57 = getelementptr inbounds i32, i32* %11, i64 %45
  %58 = bitcast i32* %57 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %58, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  %59 = getelementptr i32, i32* %57, i64 4
  %60 = bitcast i32* %59 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %60, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  %61 = or i64 %45, 8
  %62 = getelementptr inbounds i32, i32* %2, i64 %61
  %63 = bitcast i32* %62 to <4 x i32>*
  %64 = load <4 x i32>, <4 x i32>* %63, align 4, !tbaa !3, !alias.scope !7
  %65 = getelementptr i32, i32* %62, i64 4
  %66 = bitcast i32* %65 to <4 x i32>*
  %67 = load <4 x i32>, <4 x i32>* %66, align 4, !tbaa !3, !alias.scope !7
  %68 = getelementptr inbounds i32, i32* %7, i64 %61
  %69 = bitcast i32* %68 to <4 x i32>*
  store <4 x i32> %64, <4 x i32>* %69, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %70 = getelementptr i32, i32* %68, i64 4
  %71 = bitcast i32* %70 to <4 x i32>*
  store <4 x i32> %67, <4 x i32>* %71, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %72 = getelementptr inbounds i32, i32* %11, i64 %61
  %73 = bitcast i32* %72 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %73, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  %74 = getelementptr i32, i32* %72, i64 4
  %75 = bitcast i32* %74 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %75, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  %76 = add i64 %45, 16
  %77 = add i64 %46, -2
  %78 = icmp eq i64 %77, 0
  br i1 %78, label %79, label %44, !llvm.loop !15

; <label>:79:                                     ; preds = %44, %35
  %80 = phi i64 [ 0, %35 ], [ %76, %44 ]
  %81 = icmp eq i64 %40, 0
  br i1 %81, label %97, label %82

; <label>:82:                                     ; preds = %79
  %83 = getelementptr inbounds i32, i32* %2, i64 %80
  %84 = bitcast i32* %83 to <4 x i32>*
  %85 = load <4 x i32>, <4 x i32>* %84, align 4, !tbaa !3, !alias.scope !7
  %86 = getelementptr i32, i32* %83, i64 4
  %87 = bitcast i32* %86 to <4 x i32>*
  %88 = load <4 x i32>, <4 x i32>* %87, align 4, !tbaa !3, !alias.scope !7
  %89 = getelementptr inbounds i32, i32* %7, i64 %80
  %90 = bitcast i32* %89 to <4 x i32>*
  store <4 x i32> %85, <4 x i32>* %90, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %91 = getelementptr i32, i32* %89, i64 4
  %92 = bitcast i32* %91 to <4 x i32>*
  store <4 x i32> %88, <4 x i32>* %92, align 4, !tbaa !3, !alias.scope !10, !noalias !12
  %93 = getelementptr inbounds i32, i32* %11, i64 %80
  %94 = bitcast i32* %93 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %94, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  %95 = getelementptr i32, i32* %93, i64 4
  %96 = bitcast i32* %95 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %96, align 4, !tbaa !3, !alias.scope !14, !noalias !7
  br label %97

; <label>:97:                                     ; preds = %79, %82
  %98 = icmp eq i64 %36, %17
  br i1 %98, label %143, label %99

; <label>:99:                                     ; preds = %97, %19, %16
  %100 = phi i64 [ 0, %19 ], [ 0, %16 ], [ %36, %97 ]
  %101 = add nsw i64 %17, -1
  %102 = sub nsw i64 %101, %100
  %103 = and i64 %17, 3
  %104 = icmp eq i64 %103, 0
  br i1 %104, label %116, label %105

; <label>:105:                                    ; preds = %99
  br label %106

; <label>:106:                                    ; preds = %106, %105
  %107 = phi i64 [ %113, %106 ], [ %100, %105 ]
  %108 = phi i64 [ %114, %106 ], [ %103, %105 ]
  %109 = getelementptr inbounds i32, i32* %2, i64 %107
  %110 = load i32, i32* %109, align 4, !tbaa !3
  %111 = getelementptr inbounds i32, i32* %7, i64 %107
  store i32 %110, i32* %111, align 4, !tbaa !3
  %112 = getelementptr inbounds i32, i32* %11, i64 %107
  store i32 -1, i32* %112, align 4, !tbaa !3
  %113 = add nuw nsw i64 %107, 1
  %114 = add i64 %108, -1
  %115 = icmp eq i64 %114, 0
  br i1 %115, label %116, label %106, !llvm.loop !17

; <label>:116:                                    ; preds = %106, %99
  %117 = phi i64 [ %100, %99 ], [ %113, %106 ]
  %118 = icmp ult i64 %102, 3
  br i1 %118, label %143, label %119

; <label>:119:                                    ; preds = %116
  br label %120

; <label>:120:                                    ; preds = %120, %119
  %121 = phi i64 [ %117, %119 ], [ %141, %120 ]
  %122 = getelementptr inbounds i32, i32* %2, i64 %121
  %123 = load i32, i32* %122, align 4, !tbaa !3
  %124 = getelementptr inbounds i32, i32* %7, i64 %121
  store i32 %123, i32* %124, align 4, !tbaa !3
  %125 = getelementptr inbounds i32, i32* %11, i64 %121
  store i32 -1, i32* %125, align 4, !tbaa !3
  %126 = add nuw nsw i64 %121, 1
  %127 = getelementptr inbounds i32, i32* %2, i64 %126
  %128 = load i32, i32* %127, align 4, !tbaa !3
  %129 = getelementptr inbounds i32, i32* %7, i64 %126
  store i32 %128, i32* %129, align 4, !tbaa !3
  %130 = getelementptr inbounds i32, i32* %11, i64 %126
  store i32 -1, i32* %130, align 4, !tbaa !3
  %131 = add nsw i64 %121, 2
  %132 = getelementptr inbounds i32, i32* %2, i64 %131
  %133 = load i32, i32* %132, align 4, !tbaa !3
  %134 = getelementptr inbounds i32, i32* %7, i64 %131
  store i32 %133, i32* %134, align 4, !tbaa !3
  %135 = getelementptr inbounds i32, i32* %11, i64 %131
  store i32 -1, i32* %135, align 4, !tbaa !3
  %136 = add nsw i64 %121, 3
  %137 = getelementptr inbounds i32, i32* %2, i64 %136
  %138 = load i32, i32* %137, align 4, !tbaa !3
  %139 = getelementptr inbounds i32, i32* %7, i64 %136
  store i32 %138, i32* %139, align 4, !tbaa !3
  %140 = getelementptr inbounds i32, i32* %11, i64 %136
  store i32 -1, i32* %140, align 4, !tbaa !3
  %141 = add nsw i64 %121, 4
  %142 = icmp eq i64 %141, %17
  br i1 %142, label %143, label %120, !llvm.loop !19

; <label>:143:                                    ; preds = %116, %120, %97, %8
  %144 = icmp sgt i32 %0, 0
  br i1 %144, label %145, label %148

; <label>:145:                                    ; preds = %143
  %146 = zext i32 %0 to i64
  %147 = shl nuw nsw i64 %146, 2
  call void @llvm.memset.p0i8.i64(i8* %9, i8 -1, i64 %147, i32 4, i1 false)
  br label %148

; <label>:148:                                    ; preds = %145, %143
  %149 = fcmp ogt double %4, 0.000000e+00
  br i1 %149, label %150, label %155

; <label>:150:                                    ; preds = %148
  %151 = getelementptr inbounds i32, i32* %2, i64 %10
  %152 = load i32, i32* %151, align 4, !tbaa !3
  %153 = sitofp i32 %152 to double
  %154 = fmul double %153, %4
  br label %155

; <label>:155:                                    ; preds = %150, %148
  %156 = phi double [ %154, %150 ], [ %4, %148 ]
  store double 0.000000e+00, double* %5, align 8, !tbaa !20
  br i1 %15, label %157, label %367

; <label>:157:                                    ; preds = %155
  %158 = fcmp ogt double %156, 0.000000e+00
  br label %159

; <label>:159:                                    ; preds = %358, %157
  %160 = phi double [ 0.000000e+00, %157 ], [ %359, %358 ]
  %161 = phi i32 [ 0, %157 ], [ %361, %358 ]
  %162 = phi i32 [ 0, %157 ], [ %360, %358 ]
  %163 = phi i32 [ 0, %157 ], [ %362, %358 ]
  store i32 %163, i32* %13, align 4, !tbaa !3
  br i1 %158, label %165, label %164

; <label>:164:                                    ; preds = %159
  br label %251

; <label>:165:                                    ; preds = %159
  br label %166

; <label>:166:                                    ; preds = %165, %241
  %167 = phi double [ %236, %241 ], [ %160, %165 ]
  %168 = phi i32 [ %244, %241 ], [ %163, %165 ]
  %169 = phi i32 [ %239, %241 ], [ 0, %165 ]
  %170 = sext i32 %169 to i64
  %171 = add nsw i32 %168, 1
  %172 = sext i32 %171 to i64
  %173 = getelementptr inbounds i32, i32* %2, i64 %172
  %174 = load i32, i32* %173, align 4, !tbaa !3
  %175 = sext i32 %168 to i64
  %176 = getelementptr inbounds i32, i32* %11, i64 %175
  %177 = load i32, i32* %176, align 4, !tbaa !3
  %178 = icmp eq i32 %177, %163
  br i1 %178, label %201, label %179

; <label>:179:                                    ; preds = %166
  store i32 %163, i32* %176, align 4, !tbaa !3
  %180 = getelementptr inbounds i32, i32* %7, i64 %175
  %181 = load i32, i32* %180, align 4, !tbaa !3
  %182 = icmp slt i32 %181, %174
  br i1 %182, label %245, label %185

; <label>:183:                                    ; preds = %189
  %184 = trunc i64 %197 to i32
  store i32 %184, i32* %180, align 4, !tbaa !3
  br i1 %196, label %288, label %185

; <label>:185:                                    ; preds = %183, %179
  %186 = getelementptr inbounds i32, i32* %2, i64 %175
  %187 = load i32, i32* %186, align 4, !tbaa !3
  %188 = getelementptr inbounds i32, i32* %14, i64 %170
  store i32 %187, i32* %188, align 4, !tbaa !3
  br label %201

; <label>:189:                                    ; preds = %245, %189
  %190 = phi i64 [ %246, %245 ], [ %197, %189 ]
  %191 = getelementptr inbounds i32, i32* %3, i64 %190
  %192 = load i32, i32* %191, align 4, !tbaa !3
  %193 = sext i32 %192 to i64
  %194 = getelementptr inbounds i32, i32* %6, i64 %193
  %195 = load i32, i32* %194, align 4, !tbaa !3
  %196 = icmp eq i32 %195, -1
  %197 = add nsw i64 %190, 1
  %198 = icmp slt i64 %197, %247
  %199 = xor i1 %196, true
  %200 = and i1 %198, %199
  br i1 %200, label %189, label %183

; <label>:201:                                    ; preds = %185, %166
  %202 = fcmp ogt double %167, %156
  br i1 %202, label %358, label %203

; <label>:203:                                    ; preds = %201
  %204 = getelementptr inbounds i32, i32* %14, i64 %170
  %205 = load i32, i32* %204, align 4, !tbaa !3
  %206 = icmp slt i32 %205, %174
  br i1 %206, label %248, label %230

; <label>:207:                                    ; preds = %209
  %208 = icmp slt i64 %220, %250
  br i1 %208, label %209, label %228

; <label>:209:                                    ; preds = %248, %207
  %210 = phi i64 [ %249, %248 ], [ %220, %207 ]
  %211 = getelementptr inbounds i32, i32* %3, i64 %210
  %212 = load i32, i32* %211, align 4, !tbaa !3
  %213 = sext i32 %212 to i64
  %214 = getelementptr inbounds i32, i32* %6, i64 %213
  %215 = load i32, i32* %214, align 4, !tbaa !3
  %216 = sext i32 %215 to i64
  %217 = getelementptr inbounds i32, i32* %11, i64 %216
  %218 = load i32, i32* %217, align 4, !tbaa !3
  %219 = icmp eq i32 %218, %163
  %220 = add nsw i64 %210, 1
  br i1 %219, label %207, label %221

; <label>:221:                                    ; preds = %209
  %222 = trunc i64 %210 to i32
  %223 = trunc i64 %220 to i32
  store i32 %223, i32* %204, align 4, !tbaa !3
  %224 = getelementptr inbounds i32, i32* %12, i64 %170
  store i32 %212, i32* %224, align 4, !tbaa !3
  %225 = add nsw i32 %169, 1
  %226 = sext i32 %225 to i64
  %227 = getelementptr inbounds i32, i32* %13, i64 %226
  store i32 %215, i32* %227, align 4, !tbaa !3
  br label %230

; <label>:228:                                    ; preds = %207
  %229 = trunc i64 %220 to i32
  br label %230

; <label>:230:                                    ; preds = %228, %221, %203
  %231 = phi i32 [ %222, %221 ], [ %205, %203 ], [ %229, %228 ]
  %232 = phi i32 [ %225, %221 ], [ %169, %203 ], [ %169, %228 ]
  %233 = sub i32 1, %205
  %234 = add i32 %233, %231
  %235 = sitofp i32 %234 to double
  %236 = fadd double %167, %235
  store double %236, double* %5, align 8, !tbaa !20
  %237 = icmp eq i32 %231, %174
  %238 = sext i1 %237 to i32
  %239 = add nsw i32 %232, %238
  %240 = icmp sgt i32 %239, -1
  br i1 %240, label %241, label %358

; <label>:241:                                    ; preds = %230
  %242 = sext i32 %239 to i64
  %243 = getelementptr inbounds i32, i32* %13, i64 %242
  %244 = load i32, i32* %243, align 4, !tbaa !3
  br label %166

; <label>:245:                                    ; preds = %179
  %246 = sext i32 %181 to i64
  %247 = sext i32 %174 to i64
  br label %189

; <label>:248:                                    ; preds = %203
  %249 = sext i32 %205 to i64
  %250 = sext i32 %174 to i64
  br label %209

; <label>:251:                                    ; preds = %164, %342
  %252 = phi double [ %337, %342 ], [ %160, %164 ]
  %253 = phi i32 [ %345, %342 ], [ %163, %164 ]
  %254 = phi i32 [ %340, %342 ], [ 0, %164 ]
  %255 = sext i32 %254 to i64
  %256 = add nsw i32 %253, 1
  %257 = sext i32 %256 to i64
  %258 = getelementptr inbounds i32, i32* %2, i64 %257
  %259 = load i32, i32* %258, align 4, !tbaa !3
  %260 = sext i32 %253 to i64
  %261 = getelementptr inbounds i32, i32* %11, i64 %260
  %262 = load i32, i32* %261, align 4, !tbaa !3
  %263 = icmp eq i32 %262, %163
  br i1 %263, label %264, label %267

; <label>:264:                                    ; preds = %251
  %265 = getelementptr inbounds i32, i32* %14, i64 %255
  %266 = load i32, i32* %265, align 4, !tbaa !3
  br label %301

; <label>:267:                                    ; preds = %251
  store i32 %163, i32* %261, align 4, !tbaa !3
  %268 = getelementptr inbounds i32, i32* %7, i64 %260
  %269 = load i32, i32* %268, align 4, !tbaa !3
  %270 = icmp slt i32 %269, %259
  br i1 %270, label %271, label %297

; <label>:271:                                    ; preds = %267
  %272 = sext i32 %269 to i64
  %273 = sext i32 %259 to i64
  br label %274

; <label>:274:                                    ; preds = %274, %271
  %275 = phi i64 [ %272, %271 ], [ %282, %274 ]
  %276 = getelementptr inbounds i32, i32* %3, i64 %275
  %277 = load i32, i32* %276, align 4, !tbaa !3
  %278 = sext i32 %277 to i64
  %279 = getelementptr inbounds i32, i32* %6, i64 %278
  %280 = load i32, i32* %279, align 4, !tbaa !3
  %281 = icmp eq i32 %280, -1
  %282 = add nsw i64 %275, 1
  %283 = icmp slt i64 %282, %273
  %284 = xor i1 %281, true
  %285 = and i1 %283, %284
  br i1 %285, label %274, label %286

; <label>:286:                                    ; preds = %274
  %287 = trunc i64 %282 to i32
  store i32 %287, i32* %268, align 4, !tbaa !3
  br i1 %281, label %288, label %297

; <label>:288:                                    ; preds = %286, %183
  %289 = phi double [ %167, %183 ], [ %252, %286 ]
  %290 = phi i32 [ %169, %183 ], [ %254, %286 ]
  %291 = phi i64 [ %170, %183 ], [ %255, %286 ]
  %292 = phi i32 [ %192, %183 ], [ %277, %286 ]
  %293 = getelementptr inbounds i32, i32* %12, i64 %291
  store i32 %292, i32* %293, align 4, !tbaa !3
  %294 = icmp sgt i32 %290, -1
  br i1 %294, label %295, label %356

; <label>:295:                                    ; preds = %288
  %296 = sext i32 %290 to i64
  br label %346

; <label>:297:                                    ; preds = %286, %267
  %298 = getelementptr inbounds i32, i32* %2, i64 %260
  %299 = load i32, i32* %298, align 4, !tbaa !3
  %300 = getelementptr inbounds i32, i32* %14, i64 %255
  store i32 %299, i32* %300, align 4, !tbaa !3
  br label %301

; <label>:301:                                    ; preds = %297, %264
  %302 = phi i32* [ %265, %264 ], [ %300, %297 ]
  %303 = phi i32 [ %266, %264 ], [ %299, %297 ]
  %304 = icmp slt i32 %303, %259
  br i1 %304, label %305, label %331

; <label>:305:                                    ; preds = %301
  %306 = sext i32 %303 to i64
  %307 = sext i32 %259 to i64
  br label %310

; <label>:308:                                    ; preds = %310
  %309 = icmp slt i64 %321, %307
  br i1 %309, label %310, label %329

; <label>:310:                                    ; preds = %308, %305
  %311 = phi i64 [ %306, %305 ], [ %321, %308 ]
  %312 = getelementptr inbounds i32, i32* %3, i64 %311
  %313 = load i32, i32* %312, align 4, !tbaa !3
  %314 = sext i32 %313 to i64
  %315 = getelementptr inbounds i32, i32* %6, i64 %314
  %316 = load i32, i32* %315, align 4, !tbaa !3
  %317 = sext i32 %316 to i64
  %318 = getelementptr inbounds i32, i32* %11, i64 %317
  %319 = load i32, i32* %318, align 4, !tbaa !3
  %320 = icmp eq i32 %319, %163
  %321 = add nsw i64 %311, 1
  br i1 %320, label %308, label %322

; <label>:322:                                    ; preds = %310
  %323 = trunc i64 %311 to i32
  %324 = trunc i64 %321 to i32
  store i32 %324, i32* %302, align 4, !tbaa !3
  %325 = getelementptr inbounds i32, i32* %12, i64 %255
  store i32 %313, i32* %325, align 4, !tbaa !3
  %326 = add nsw i32 %254, 1
  %327 = sext i32 %326 to i64
  %328 = getelementptr inbounds i32, i32* %13, i64 %327
  store i32 %316, i32* %328, align 4, !tbaa !3
  br label %331

; <label>:329:                                    ; preds = %308
  %330 = trunc i64 %321 to i32
  br label %331

; <label>:331:                                    ; preds = %329, %322, %301
  %332 = phi i32 [ %323, %322 ], [ %303, %301 ], [ %330, %329 ]
  %333 = phi i32 [ %326, %322 ], [ %254, %301 ], [ %254, %329 ]
  %334 = sub i32 1, %303
  %335 = add i32 %334, %332
  %336 = sitofp i32 %335 to double
  %337 = fadd double %252, %336
  store double %337, double* %5, align 8, !tbaa !20
  %338 = icmp eq i32 %332, %259
  %339 = sext i1 %338 to i32
  %340 = add nsw i32 %333, %339
  %341 = icmp sgt i32 %340, -1
  br i1 %341, label %342, label %358

; <label>:342:                                    ; preds = %331
  %343 = sext i32 %340 to i64
  %344 = getelementptr inbounds i32, i32* %13, i64 %343
  %345 = load i32, i32* %344, align 4, !tbaa !3
  br label %251

; <label>:346:                                    ; preds = %346, %295
  %347 = phi i64 [ %296, %295 ], [ %354, %346 ]
  %348 = getelementptr inbounds i32, i32* %13, i64 %347
  %349 = load i32, i32* %348, align 4, !tbaa !3
  %350 = getelementptr inbounds i32, i32* %12, i64 %347
  %351 = load i32, i32* %350, align 4, !tbaa !3
  %352 = sext i32 %351 to i64
  %353 = getelementptr inbounds i32, i32* %6, i64 %352
  store i32 %349, i32* %353, align 4, !tbaa !3
  %354 = add nsw i64 %347, -1
  %355 = icmp sgt i64 %347, 0
  br i1 %355, label %346, label %356

; <label>:356:                                    ; preds = %346, %288
  %357 = add nsw i32 %162, 1
  br label %358

; <label>:358:                                    ; preds = %331, %201, %230, %356
  %359 = phi double [ %289, %356 ], [ %167, %201 ], [ %236, %230 ], [ %337, %331 ]
  %360 = phi i32 [ %357, %356 ], [ %162, %230 ], [ %162, %201 ], [ %162, %331 ]
  %361 = phi i32 [ %161, %356 ], [ 1, %201 ], [ %161, %230 ], [ %161, %331 ]
  %362 = add nuw nsw i32 %163, 1
  %363 = icmp eq i32 %362, %1
  br i1 %363, label %364, label %159

; <label>:364:                                    ; preds = %358
  %365 = icmp eq i32 %361, 0
  br i1 %365, label %367, label %366

; <label>:366:                                    ; preds = %364
  store double -1.000000e+00, double* %5, align 8, !tbaa !20
  br label %367

; <label>:367:                                    ; preds = %155, %364, %366
  %368 = phi i32 [ %360, %364 ], [ %360, %366 ], [ 0, %155 ]
  ret i32 %368
}

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8}
!8 = distinct !{!8, !9}
!9 = distinct !{!9, !"LVerDomain"}
!10 = !{!11}
!11 = distinct !{!11, !9}
!12 = !{!13, !8}
!13 = distinct !{!13, !9}
!14 = !{!13}
!15 = distinct !{!15, !16}
!16 = !{!"llvm.loop.isvectorized", i32 1}
!17 = distinct !{!17, !18}
!18 = !{!"llvm.loop.unroll.disable"}
!19 = distinct !{!19, !16}
!20 = !{!21, !21, i64 0}
!21 = !{!"double", !5, i64 0}
