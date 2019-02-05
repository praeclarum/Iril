; ModuleID = 'btf_strongcomp.c'
source_filename = "btf_strongcomp.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: nounwind ssp uwtable
define i32 @btf_strongcomp(i32, i32* nocapture readonly, i32* nocapture readonly, i32*, i32* nocapture, i32* nocapture, i32* nocapture) local_unnamed_addr #0 {
  %8 = bitcast i32* %5 to i8*
  %9 = sext i32 %0 to i64
  %10 = getelementptr inbounds i32, i32* %6, i64 %9
  %11 = getelementptr inbounds i32, i32* %10, i64 %9
  %12 = getelementptr inbounds i32, i32* %11, i64 %9
  %13 = icmp sgt i32 %0, 0
  br i1 %13, label %14, label %360

; <label>:14:                                     ; preds = %7
  %15 = zext i32 %0 to i64
  %16 = icmp ult i32 %0, 8
  br i1 %16, label %91, label %17

; <label>:17:                                     ; preds = %14
  %18 = add nsw i64 %9, %15
  %19 = getelementptr i32, i32* %6, i64 %18
  %20 = getelementptr i32, i32* %4, i64 %15
  %21 = getelementptr i32, i32* %6, i64 %15
  %22 = icmp ult i32* %10, %20
  %23 = icmp ugt i32* %19, %4
  %24 = and i1 %22, %23
  %25 = icmp ult i32* %10, %21
  %26 = icmp ugt i32* %19, %6
  %27 = and i1 %25, %26
  %28 = or i1 %24, %27
  %29 = icmp ugt i32* %21, %4
  %30 = icmp ugt i32* %20, %6
  %31 = and i1 %29, %30
  %32 = or i1 %28, %31
  br i1 %32, label %91, label %33

; <label>:33:                                     ; preds = %17
  %34 = and i64 %15, 4294967288
  %35 = add nsw i64 %34, -8
  %36 = lshr exact i64 %35, 3
  %37 = add nuw nsw i64 %36, 1
  %38 = and i64 %37, 1
  %39 = icmp eq i64 %35, 0
  br i1 %39, label %73, label %40

; <label>:40:                                     ; preds = %33
  %41 = sub nsw i64 %37, %38
  br label %42

; <label>:42:                                     ; preds = %42, %40
  %43 = phi i64 [ 0, %40 ], [ %70, %42 ]
  %44 = phi i64 [ %41, %40 ], [ %71, %42 ]
  %45 = getelementptr inbounds i32, i32* %10, i64 %43
  %46 = bitcast i32* %45 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %46, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %47 = getelementptr i32, i32* %45, i64 4
  %48 = bitcast i32* %47 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %48, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %49 = getelementptr inbounds i32, i32* %4, i64 %43
  %50 = bitcast i32* %49 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %50, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %51 = getelementptr i32, i32* %49, i64 4
  %52 = bitcast i32* %51 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %52, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %53 = getelementptr inbounds i32, i32* %6, i64 %43
  %54 = bitcast i32* %53 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %54, align 4, !tbaa !3, !alias.scope !14
  %55 = getelementptr i32, i32* %53, i64 4
  %56 = bitcast i32* %55 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %56, align 4, !tbaa !3, !alias.scope !14
  %57 = or i64 %43, 8
  %58 = getelementptr inbounds i32, i32* %10, i64 %57
  %59 = bitcast i32* %58 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %59, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %60 = getelementptr i32, i32* %58, i64 4
  %61 = bitcast i32* %60 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %61, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %62 = getelementptr inbounds i32, i32* %4, i64 %57
  %63 = bitcast i32* %62 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %63, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %64 = getelementptr i32, i32* %62, i64 4
  %65 = bitcast i32* %64 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %65, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %66 = getelementptr inbounds i32, i32* %6, i64 %57
  %67 = bitcast i32* %66 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %67, align 4, !tbaa !3, !alias.scope !14
  %68 = getelementptr i32, i32* %66, i64 4
  %69 = bitcast i32* %68 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %69, align 4, !tbaa !3, !alias.scope !14
  %70 = add i64 %43, 16
  %71 = add i64 %44, -2
  %72 = icmp eq i64 %71, 0
  br i1 %72, label %73, label %42, !llvm.loop !15

; <label>:73:                                     ; preds = %42, %33
  %74 = phi i64 [ 0, %33 ], [ %70, %42 ]
  %75 = icmp eq i64 %38, 0
  br i1 %75, label %89, label %76

; <label>:76:                                     ; preds = %73
  %77 = getelementptr inbounds i32, i32* %10, i64 %74
  %78 = bitcast i32* %77 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %78, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %79 = getelementptr i32, i32* %77, i64 4
  %80 = bitcast i32* %79 to <4 x i32>*
  store <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, <4 x i32>* %80, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %81 = getelementptr inbounds i32, i32* %4, i64 %74
  %82 = bitcast i32* %81 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %82, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %83 = getelementptr i32, i32* %81, i64 4
  %84 = bitcast i32* %83 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %84, align 4, !tbaa !3, !alias.scope !13, !noalias !14
  %85 = getelementptr inbounds i32, i32* %6, i64 %74
  %86 = bitcast i32* %85 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %86, align 4, !tbaa !3, !alias.scope !14
  %87 = getelementptr i32, i32* %85, i64 4
  %88 = bitcast i32* %87 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %88, align 4, !tbaa !3, !alias.scope !14
  br label %89

; <label>:89:                                     ; preds = %73, %76
  %90 = icmp eq i64 %34, %15
  br i1 %90, label %116, label %91

; <label>:91:                                     ; preds = %89, %17, %14
  %92 = phi i64 [ 0, %17 ], [ 0, %14 ], [ %34, %89 ]
  %93 = add nsw i64 %15, -1
  %94 = and i64 %15, 1
  %95 = icmp eq i64 %94, 0
  br i1 %95, label %101, label %96

; <label>:96:                                     ; preds = %91
  %97 = getelementptr inbounds i32, i32* %10, i64 %92
  store i32 -2, i32* %97, align 4, !tbaa !3
  %98 = getelementptr inbounds i32, i32* %4, i64 %92
  store i32 -1, i32* %98, align 4, !tbaa !3
  %99 = getelementptr inbounds i32, i32* %6, i64 %92
  store i32 -1, i32* %99, align 4, !tbaa !3
  %100 = or i64 %92, 1
  br label %101

; <label>:101:                                    ; preds = %91, %96
  %102 = phi i64 [ %92, %91 ], [ %100, %96 ]
  %103 = icmp eq i64 %93, %92
  br i1 %103, label %116, label %104

; <label>:104:                                    ; preds = %101
  br label %105

; <label>:105:                                    ; preds = %105, %104
  %106 = phi i64 [ %102, %104 ], [ %114, %105 ]
  %107 = getelementptr inbounds i32, i32* %10, i64 %106
  store i32 -2, i32* %107, align 4, !tbaa !3
  %108 = getelementptr inbounds i32, i32* %4, i64 %106
  store i32 -1, i32* %108, align 4, !tbaa !3
  %109 = getelementptr inbounds i32, i32* %6, i64 %106
  store i32 -1, i32* %109, align 4, !tbaa !3
  %110 = add nuw nsw i64 %106, 1
  %111 = getelementptr inbounds i32, i32* %10, i64 %110
  store i32 -2, i32* %111, align 4, !tbaa !3
  %112 = getelementptr inbounds i32, i32* %4, i64 %110
  store i32 -1, i32* %112, align 4, !tbaa !3
  %113 = getelementptr inbounds i32, i32* %6, i64 %110
  store i32 -1, i32* %113, align 4, !tbaa !3
  %114 = add nsw i64 %106, 2
  %115 = icmp eq i64 %114, %15
  br i1 %115, label %116, label %105, !llvm.loop !17

; <label>:116:                                    ; preds = %101, %105, %89
  br i1 %13, label %117, label %360

; <label>:117:                                    ; preds = %116
  %118 = icmp eq i32* %3, null
  %119 = zext i32 %0 to i64
  br label %120

; <label>:120:                                    ; preds = %355, %117
  %121 = phi i64 [ 0, %117 ], [ %358, %355 ]
  %122 = phi i32 [ 0, %117 ], [ %357, %355 ]
  %123 = phi i32 [ 0, %117 ], [ %356, %355 ]
  %124 = getelementptr inbounds i32, i32* %10, i64 %121
  %125 = load i32, i32* %124, align 4, !tbaa !3
  %126 = icmp eq i32 %125, -2
  br i1 %126, label %127, label %355

; <label>:127:                                    ; preds = %120
  %128 = trunc i64 %121 to i32
  store i32 %128, i32* %11, align 4, !tbaa !3
  br i1 %118, label %130, label %129

; <label>:129:                                    ; preds = %127
  br label %240

; <label>:130:                                    ; preds = %127
  br label %131

; <label>:131:                                    ; preds = %130, %229
  %132 = phi i32 [ %232, %229 ], [ %128, %130 ]
  %133 = phi i32 [ %163, %229 ], [ %123, %130 ]
  %134 = phi i32 [ %227, %229 ], [ %122, %130 ]
  %135 = phi i32 [ %226, %229 ], [ 0, %130 ]
  %136 = phi i32 [ %225, %229 ], [ 0, %130 ]
  %137 = sext i32 %136 to i64
  %138 = add nsw i32 %132, 1
  %139 = sext i32 %138 to i64
  %140 = getelementptr inbounds i32, i32* %1, i64 %139
  %141 = load i32, i32* %140, align 4, !tbaa !3
  %142 = sext i32 %132 to i64
  %143 = getelementptr inbounds i32, i32* %10, i64 %142
  %144 = load i32, i32* %143, align 4, !tbaa !3
  %145 = icmp eq i32 %144, -2
  br i1 %145, label %149, label %146

; <label>:146:                                    ; preds = %131
  %147 = getelementptr inbounds i32, i32* %12, i64 %137
  %148 = load i32, i32* %147, align 4, !tbaa !3
  br label %159

; <label>:149:                                    ; preds = %131
  %150 = add nsw i32 %135, 1
  %151 = sext i32 %150 to i64
  %152 = getelementptr inbounds i32, i32* %5, i64 %151
  store i32 %132, i32* %152, align 4, !tbaa !3
  %153 = add nsw i32 %133, 1
  %154 = getelementptr inbounds i32, i32* %6, i64 %142
  store i32 %153, i32* %154, align 4, !tbaa !3
  %155 = getelementptr inbounds i32, i32* %4, i64 %142
  store i32 %153, i32* %155, align 4, !tbaa !3
  store i32 -1, i32* %143, align 4, !tbaa !3
  %156 = getelementptr inbounds i32, i32* %1, i64 %142
  %157 = load i32, i32* %156, align 4, !tbaa !3
  %158 = getelementptr inbounds i32, i32* %12, i64 %137
  store i32 %157, i32* %158, align 4, !tbaa !3
  br label %159

; <label>:159:                                    ; preds = %149, %146
  %160 = phi i32* [ %147, %146 ], [ %158, %149 ]
  %161 = phi i32 [ %148, %146 ], [ %157, %149 ]
  %162 = phi i32 [ %135, %146 ], [ %150, %149 ]
  %163 = phi i32 [ %133, %146 ], [ %153, %149 ]
  %164 = icmp slt i32 %161, %141
  br i1 %164, label %236, label %186

; <label>:165:                                    ; preds = %236, %233
  %166 = phi i64 [ %238, %236 ], [ %234, %233 ]
  %167 = getelementptr inbounds i32, i32* %2, i64 %166
  %168 = load i32, i32* %167, align 4, !tbaa !3
  %169 = sext i32 %168 to i64
  %170 = getelementptr inbounds i32, i32* %10, i64 %169
  %171 = load i32, i32* %170, align 4, !tbaa !3
  switch i32 %171, label %233 [
    i32 -2, label %178
    i32 -1, label %172
  ]

; <label>:172:                                    ; preds = %165
  %173 = load i32, i32* %237, align 4, !tbaa !3
  %174 = getelementptr inbounds i32, i32* %6, i64 %169
  %175 = load i32, i32* %174, align 4, !tbaa !3
  %176 = icmp slt i32 %173, %175
  %177 = select i1 %176, i32 %173, i32 %175
  store i32 %177, i32* %237, align 4, !tbaa !3
  br label %233

; <label>:178:                                    ; preds = %165
  %179 = trunc i64 %166 to i32
  %180 = add nsw i32 %179, 1
  store i32 %180, i32* %160, align 4, !tbaa !3
  %181 = add nsw i32 %136, 1
  %182 = sext i32 %181 to i64
  %183 = getelementptr inbounds i32, i32* %11, i64 %182
  store i32 %168, i32* %183, align 4, !tbaa !3
  br label %186

; <label>:184:                                    ; preds = %233
  %185 = trunc i64 %234 to i32
  br label %186

; <label>:186:                                    ; preds = %184, %178, %159
  %187 = phi i32 [ %179, %178 ], [ %161, %159 ], [ %185, %184 ]
  %188 = phi i32 [ %181, %178 ], [ %136, %159 ], [ %136, %184 ]
  %189 = icmp eq i32 %187, %141
  br i1 %189, label %190, label %224

; <label>:190:                                    ; preds = %186
  %191 = add nsw i32 %188, -1
  %192 = getelementptr inbounds i32, i32* %4, i64 %142
  %193 = load i32, i32* %192, align 4, !tbaa !3
  %194 = getelementptr inbounds i32, i32* %6, i64 %142
  %195 = load i32, i32* %194, align 4, !tbaa !3
  %196 = icmp eq i32 %193, %195
  br i1 %196, label %197, label %210

; <label>:197:                                    ; preds = %190
  %198 = sext i32 %162 to i64
  br label %199

; <label>:199:                                    ; preds = %199, %197
  %200 = phi i64 [ %201, %199 ], [ %198, %197 ]
  %201 = add i64 %200, -1
  %202 = getelementptr inbounds i32, i32* %5, i64 %200
  %203 = load i32, i32* %202, align 4, !tbaa !3
  %204 = sext i32 %203 to i64
  %205 = getelementptr inbounds i32, i32* %10, i64 %204
  store i32 %134, i32* %205, align 4, !tbaa !3
  %206 = icmp eq i32 %203, %132
  br i1 %206, label %207, label %199

; <label>:207:                                    ; preds = %199
  %208 = trunc i64 %201 to i32
  %209 = add nsw i32 %134, 1
  br label %210

; <label>:210:                                    ; preds = %207, %190
  %211 = phi i32 [ %208, %207 ], [ %162, %190 ]
  %212 = phi i32 [ %209, %207 ], [ %134, %190 ]
  %213 = icmp sgt i32 %188, 0
  br i1 %213, label %214, label %224

; <label>:214:                                    ; preds = %210
  %215 = sext i32 %191 to i64
  %216 = getelementptr inbounds i32, i32* %11, i64 %215
  %217 = load i32, i32* %216, align 4, !tbaa !3
  %218 = sext i32 %217 to i64
  %219 = getelementptr inbounds i32, i32* %4, i64 %218
  %220 = load i32, i32* %219, align 4, !tbaa !3
  %221 = load i32, i32* %192, align 4, !tbaa !3
  %222 = icmp slt i32 %220, %221
  %223 = select i1 %222, i32 %220, i32 %221
  store i32 %223, i32* %219, align 4, !tbaa !3
  br label %224

; <label>:224:                                    ; preds = %214, %210, %186
  %225 = phi i32 [ %191, %214 ], [ %191, %210 ], [ %188, %186 ]
  %226 = phi i32 [ %211, %214 ], [ %211, %210 ], [ %162, %186 ]
  %227 = phi i32 [ %212, %214 ], [ %212, %210 ], [ %134, %186 ]
  %228 = icmp sgt i32 %225, -1
  br i1 %228, label %229, label %355

; <label>:229:                                    ; preds = %224
  %230 = sext i32 %225 to i64
  %231 = getelementptr inbounds i32, i32* %11, i64 %230
  %232 = load i32, i32* %231, align 4, !tbaa !3
  br label %131

; <label>:233:                                    ; preds = %172, %165
  %234 = add nsw i64 %166, 1
  %235 = icmp slt i64 %234, %239
  br i1 %235, label %165, label %184

; <label>:236:                                    ; preds = %159
  %237 = getelementptr inbounds i32, i32* %4, i64 %142
  %238 = sext i32 %161 to i64
  %239 = sext i32 %141 to i64
  br label %165

; <label>:240:                                    ; preds = %129, %351
  %241 = phi i32 [ %354, %351 ], [ %128, %129 ]
  %242 = phi i32 [ %278, %351 ], [ %123, %129 ]
  %243 = phi i32 [ %349, %351 ], [ %122, %129 ]
  %244 = phi i32 [ %348, %351 ], [ 0, %129 ]
  %245 = phi i32 [ %347, %351 ], [ 0, %129 ]
  %246 = sext i32 %245 to i64
  %247 = sext i32 %241 to i64
  %248 = getelementptr inbounds i32, i32* %3, i64 %247
  %249 = load i32, i32* %248, align 4, !tbaa !3
  %250 = icmp slt i32 %249, -1
  %251 = sub i32 -2, %249
  %252 = select i1 %250, i32 %251, i32 %249
  %253 = add nsw i32 %252, 1
  %254 = sext i32 %253 to i64
  %255 = getelementptr inbounds i32, i32* %1, i64 %254
  %256 = load i32, i32* %255, align 4, !tbaa !3
  %257 = getelementptr inbounds i32, i32* %10, i64 %247
  %258 = load i32, i32* %257, align 4, !tbaa !3
  %259 = icmp eq i32 %258, -2
  br i1 %259, label %263, label %260

; <label>:260:                                    ; preds = %240
  %261 = getelementptr inbounds i32, i32* %12, i64 %246
  %262 = load i32, i32* %261, align 4, !tbaa !3
  br label %274

; <label>:263:                                    ; preds = %240
  %264 = add nsw i32 %244, 1
  %265 = sext i32 %264 to i64
  %266 = getelementptr inbounds i32, i32* %5, i64 %265
  store i32 %241, i32* %266, align 4, !tbaa !3
  %267 = add nsw i32 %242, 1
  %268 = getelementptr inbounds i32, i32* %6, i64 %247
  store i32 %267, i32* %268, align 4, !tbaa !3
  %269 = getelementptr inbounds i32, i32* %4, i64 %247
  store i32 %267, i32* %269, align 4, !tbaa !3
  store i32 -1, i32* %257, align 4, !tbaa !3
  %270 = sext i32 %252 to i64
  %271 = getelementptr inbounds i32, i32* %1, i64 %270
  %272 = load i32, i32* %271, align 4, !tbaa !3
  %273 = getelementptr inbounds i32, i32* %12, i64 %246
  store i32 %272, i32* %273, align 4, !tbaa !3
  br label %274

; <label>:274:                                    ; preds = %263, %260
  %275 = phi i32* [ %261, %260 ], [ %273, %263 ]
  %276 = phi i32 [ %262, %260 ], [ %272, %263 ]
  %277 = phi i32 [ %244, %260 ], [ %264, %263 ]
  %278 = phi i32 [ %242, %260 ], [ %267, %263 ]
  %279 = icmp slt i32 %276, %256
  br i1 %279, label %280, label %308

; <label>:280:                                    ; preds = %274
  %281 = getelementptr inbounds i32, i32* %4, i64 %247
  %282 = sext i32 %276 to i64
  %283 = sext i32 %256 to i64
  br label %284

; <label>:284:                                    ; preds = %303, %280
  %285 = phi i64 [ %282, %280 ], [ %304, %303 ]
  %286 = getelementptr inbounds i32, i32* %2, i64 %285
  %287 = load i32, i32* %286, align 4, !tbaa !3
  %288 = sext i32 %287 to i64
  %289 = getelementptr inbounds i32, i32* %10, i64 %288
  %290 = load i32, i32* %289, align 4, !tbaa !3
  switch i32 %290, label %303 [
    i32 -2, label %291
    i32 -1, label %297
  ]

; <label>:291:                                    ; preds = %284
  %292 = trunc i64 %285 to i32
  %293 = add nsw i32 %292, 1
  store i32 %293, i32* %275, align 4, !tbaa !3
  %294 = add nsw i32 %245, 1
  %295 = sext i32 %294 to i64
  %296 = getelementptr inbounds i32, i32* %11, i64 %295
  store i32 %287, i32* %296, align 4, !tbaa !3
  br label %308

; <label>:297:                                    ; preds = %284
  %298 = load i32, i32* %281, align 4, !tbaa !3
  %299 = getelementptr inbounds i32, i32* %6, i64 %288
  %300 = load i32, i32* %299, align 4, !tbaa !3
  %301 = icmp slt i32 %298, %300
  %302 = select i1 %301, i32 %298, i32 %300
  store i32 %302, i32* %281, align 4, !tbaa !3
  br label %303

; <label>:303:                                    ; preds = %297, %284
  %304 = add nsw i64 %285, 1
  %305 = icmp slt i64 %304, %283
  br i1 %305, label %284, label %306

; <label>:306:                                    ; preds = %303
  %307 = trunc i64 %304 to i32
  br label %308

; <label>:308:                                    ; preds = %306, %291, %274
  %309 = phi i32 [ %292, %291 ], [ %276, %274 ], [ %307, %306 ]
  %310 = phi i32 [ %294, %291 ], [ %245, %274 ], [ %245, %306 ]
  %311 = icmp eq i32 %309, %256
  br i1 %311, label %312, label %346

; <label>:312:                                    ; preds = %308
  %313 = add nsw i32 %310, -1
  %314 = getelementptr inbounds i32, i32* %4, i64 %247
  %315 = load i32, i32* %314, align 4, !tbaa !3
  %316 = getelementptr inbounds i32, i32* %6, i64 %247
  %317 = load i32, i32* %316, align 4, !tbaa !3
  %318 = icmp eq i32 %315, %317
  br i1 %318, label %319, label %332

; <label>:319:                                    ; preds = %312
  %320 = sext i32 %277 to i64
  br label %321

; <label>:321:                                    ; preds = %321, %319
  %322 = phi i64 [ %323, %321 ], [ %320, %319 ]
  %323 = add i64 %322, -1
  %324 = getelementptr inbounds i32, i32* %5, i64 %322
  %325 = load i32, i32* %324, align 4, !tbaa !3
  %326 = sext i32 %325 to i64
  %327 = getelementptr inbounds i32, i32* %10, i64 %326
  store i32 %243, i32* %327, align 4, !tbaa !3
  %328 = icmp eq i32 %325, %241
  br i1 %328, label %329, label %321

; <label>:329:                                    ; preds = %321
  %330 = trunc i64 %323 to i32
  %331 = add nsw i32 %243, 1
  br label %332

; <label>:332:                                    ; preds = %329, %312
  %333 = phi i32 [ %330, %329 ], [ %277, %312 ]
  %334 = phi i32 [ %331, %329 ], [ %243, %312 ]
  %335 = icmp sgt i32 %310, 0
  br i1 %335, label %336, label %346

; <label>:336:                                    ; preds = %332
  %337 = sext i32 %313 to i64
  %338 = getelementptr inbounds i32, i32* %11, i64 %337
  %339 = load i32, i32* %338, align 4, !tbaa !3
  %340 = sext i32 %339 to i64
  %341 = getelementptr inbounds i32, i32* %4, i64 %340
  %342 = load i32, i32* %341, align 4, !tbaa !3
  %343 = load i32, i32* %314, align 4, !tbaa !3
  %344 = icmp slt i32 %342, %343
  %345 = select i1 %344, i32 %342, i32 %343
  store i32 %345, i32* %341, align 4, !tbaa !3
  br label %346

; <label>:346:                                    ; preds = %336, %332, %308
  %347 = phi i32 [ %313, %336 ], [ %313, %332 ], [ %310, %308 ]
  %348 = phi i32 [ %333, %336 ], [ %333, %332 ], [ %277, %308 ]
  %349 = phi i32 [ %334, %336 ], [ %334, %332 ], [ %243, %308 ]
  %350 = icmp sgt i32 %347, -1
  br i1 %350, label %351, label %355

; <label>:351:                                    ; preds = %346
  %352 = sext i32 %347 to i64
  %353 = getelementptr inbounds i32, i32* %11, i64 %352
  %354 = load i32, i32* %353, align 4, !tbaa !3
  br label %240

; <label>:355:                                    ; preds = %346, %224, %120
  %356 = phi i32 [ %123, %120 ], [ %163, %224 ], [ %278, %346 ]
  %357 = phi i32 [ %122, %120 ], [ %227, %224 ], [ %349, %346 ]
  %358 = add nuw nsw i64 %121, 1
  %359 = icmp eq i64 %358, %119
  br i1 %359, label %361, label %120

; <label>:360:                                    ; preds = %116, %7
  store i32 0, i32* %6, align 4, !tbaa !3
  store i32 %0, i32* %5, align 4, !tbaa !3
  br label %656

; <label>:361:                                    ; preds = %355
  %362 = icmp sgt i32 %357, 0
  br i1 %362, label %363, label %366

; <label>:363:                                    ; preds = %361
  %364 = zext i32 %357 to i64
  %365 = shl nuw nsw i64 %364, 2
  call void @llvm.memset.p0i8.i64(i8* %8, i8 0, i64 %365, i32 4, i1 false)
  br label %366

; <label>:366:                                    ; preds = %363, %361
  %367 = phi i1 [ true, %363 ], [ false, %361 ]
  br i1 %13, label %368, label %424

; <label>:368:                                    ; preds = %366
  %369 = zext i32 %0 to i64
  %370 = add nsw i64 %369, -1
  %371 = and i64 %369, 3
  %372 = icmp ult i64 %370, 3
  br i1 %372, label %408, label %373

; <label>:373:                                    ; preds = %368
  %374 = sub nsw i64 %369, %371
  br label %375

; <label>:375:                                    ; preds = %375, %373
  %376 = phi i64 [ 0, %373 ], [ %405, %375 ]
  %377 = phi i64 [ %374, %373 ], [ %406, %375 ]
  %378 = getelementptr inbounds i32, i32* %10, i64 %376
  %379 = load i32, i32* %378, align 4, !tbaa !3
  %380 = sext i32 %379 to i64
  %381 = getelementptr inbounds i32, i32* %5, i64 %380
  %382 = load i32, i32* %381, align 4, !tbaa !3
  %383 = add nsw i32 %382, 1
  store i32 %383, i32* %381, align 4, !tbaa !3
  %384 = or i64 %376, 1
  %385 = getelementptr inbounds i32, i32* %10, i64 %384
  %386 = load i32, i32* %385, align 4, !tbaa !3
  %387 = sext i32 %386 to i64
  %388 = getelementptr inbounds i32, i32* %5, i64 %387
  %389 = load i32, i32* %388, align 4, !tbaa !3
  %390 = add nsw i32 %389, 1
  store i32 %390, i32* %388, align 4, !tbaa !3
  %391 = or i64 %376, 2
  %392 = getelementptr inbounds i32, i32* %10, i64 %391
  %393 = load i32, i32* %392, align 4, !tbaa !3
  %394 = sext i32 %393 to i64
  %395 = getelementptr inbounds i32, i32* %5, i64 %394
  %396 = load i32, i32* %395, align 4, !tbaa !3
  %397 = add nsw i32 %396, 1
  store i32 %397, i32* %395, align 4, !tbaa !3
  %398 = or i64 %376, 3
  %399 = getelementptr inbounds i32, i32* %10, i64 %398
  %400 = load i32, i32* %399, align 4, !tbaa !3
  %401 = sext i32 %400 to i64
  %402 = getelementptr inbounds i32, i32* %5, i64 %401
  %403 = load i32, i32* %402, align 4, !tbaa !3
  %404 = add nsw i32 %403, 1
  store i32 %404, i32* %402, align 4, !tbaa !3
  %405 = add nuw nsw i64 %376, 4
  %406 = add i64 %377, -4
  %407 = icmp eq i64 %406, 0
  br i1 %407, label %408, label %375

; <label>:408:                                    ; preds = %375, %368
  %409 = phi i64 [ 0, %368 ], [ %405, %375 ]
  %410 = icmp eq i64 %371, 0
  br i1 %410, label %424, label %411

; <label>:411:                                    ; preds = %408
  br label %412

; <label>:412:                                    ; preds = %412, %411
  %413 = phi i64 [ %409, %411 ], [ %421, %412 ]
  %414 = phi i64 [ %371, %411 ], [ %422, %412 ]
  %415 = getelementptr inbounds i32, i32* %10, i64 %413
  %416 = load i32, i32* %415, align 4, !tbaa !3
  %417 = sext i32 %416 to i64
  %418 = getelementptr inbounds i32, i32* %5, i64 %417
  %419 = load i32, i32* %418, align 4, !tbaa !3
  %420 = add nsw i32 %419, 1
  store i32 %420, i32* %418, align 4, !tbaa !3
  %421 = add nuw nsw i64 %413, 1
  %422 = add i64 %414, -1
  %423 = icmp eq i64 %422, 0
  br i1 %423, label %424, label %412, !llvm.loop !18

; <label>:424:                                    ; preds = %408, %412, %366
  store i32 0, i32* %6, align 4, !tbaa !3
  %425 = icmp sgt i32 %357, 1
  br i1 %425, label %426, label %480

; <label>:426:                                    ; preds = %424
  %427 = zext i32 %357 to i64
  %428 = add nsw i64 %427, -2
  %429 = add i32 %357, 3
  %430 = and i32 %429, 3
  %431 = zext i32 %430 to i64
  %432 = icmp ult i64 %428, 3
  br i1 %432, label %463, label %433

; <label>:433:                                    ; preds = %426
  %434 = add nsw i64 %427, -1
  %435 = sub nsw i64 %434, %431
  br label %436

; <label>:436:                                    ; preds = %436, %433
  %437 = phi i32 [ 0, %433 ], [ %458, %436 ]
  %438 = phi i64 [ 1, %433 ], [ %460, %436 ]
  %439 = phi i64 [ %435, %433 ], [ %461, %436 ]
  %440 = add nsw i64 %438, -1
  %441 = getelementptr inbounds i32, i32* %5, i64 %440
  %442 = load i32, i32* %441, align 4, !tbaa !3
  %443 = add nsw i32 %442, %437
  %444 = getelementptr inbounds i32, i32* %6, i64 %438
  store i32 %443, i32* %444, align 4, !tbaa !3
  %445 = add nuw nsw i64 %438, 1
  %446 = getelementptr inbounds i32, i32* %5, i64 %438
  %447 = load i32, i32* %446, align 4, !tbaa !3
  %448 = add nsw i32 %447, %443
  %449 = getelementptr inbounds i32, i32* %6, i64 %445
  store i32 %448, i32* %449, align 4, !tbaa !3
  %450 = add nuw nsw i64 %438, 2
  %451 = getelementptr inbounds i32, i32* %5, i64 %445
  %452 = load i32, i32* %451, align 4, !tbaa !3
  %453 = add nsw i32 %452, %448
  %454 = getelementptr inbounds i32, i32* %6, i64 %450
  store i32 %453, i32* %454, align 4, !tbaa !3
  %455 = add nuw nsw i64 %438, 3
  %456 = getelementptr inbounds i32, i32* %5, i64 %450
  %457 = load i32, i32* %456, align 4, !tbaa !3
  %458 = add nsw i32 %457, %453
  %459 = getelementptr inbounds i32, i32* %6, i64 %455
  store i32 %458, i32* %459, align 4, !tbaa !3
  %460 = add nuw nsw i64 %438, 4
  %461 = add i64 %439, -4
  %462 = icmp eq i64 %461, 0
  br i1 %462, label %463, label %436

; <label>:463:                                    ; preds = %436, %426
  %464 = phi i32 [ 0, %426 ], [ %458, %436 ]
  %465 = phi i64 [ 1, %426 ], [ %460, %436 ]
  %466 = icmp eq i32 %430, 0
  br i1 %466, label %480, label %467

; <label>:467:                                    ; preds = %463
  br label %468

; <label>:468:                                    ; preds = %468, %467
  %469 = phi i32 [ %464, %467 ], [ %475, %468 ]
  %470 = phi i64 [ %465, %467 ], [ %477, %468 ]
  %471 = phi i64 [ %431, %467 ], [ %478, %468 ]
  %472 = add nsw i64 %470, -1
  %473 = getelementptr inbounds i32, i32* %5, i64 %472
  %474 = load i32, i32* %473, align 4, !tbaa !3
  %475 = add nsw i32 %474, %469
  %476 = getelementptr inbounds i32, i32* %6, i64 %470
  store i32 %475, i32* %476, align 4, !tbaa !3
  %477 = add nuw nsw i64 %470, 1
  %478 = add i64 %471, -1
  %479 = icmp eq i64 %478, 0
  br i1 %479, label %480, label %468, !llvm.loop !20

; <label>:480:                                    ; preds = %463, %468, %424
  br i1 %367, label %481, label %609

; <label>:481:                                    ; preds = %480
  %482 = zext i32 %357 to i64
  %483 = icmp ult i32 %357, 8
  br i1 %483, label %570, label %484

; <label>:484:                                    ; preds = %481
  %485 = getelementptr i32, i32* %5, i64 %482
  %486 = getelementptr i32, i32* %6, i64 %482
  %487 = icmp ugt i32* %486, %5
  %488 = icmp ugt i32* %485, %6
  %489 = and i1 %487, %488
  br i1 %489, label %570, label %490

; <label>:490:                                    ; preds = %484
  %491 = and i64 %482, 4294967288
  %492 = add nsw i64 %491, -8
  %493 = lshr exact i64 %492, 3
  %494 = add nuw nsw i64 %493, 1
  %495 = and i64 %494, 3
  %496 = icmp ult i64 %492, 24
  br i1 %496, label %548, label %497

; <label>:497:                                    ; preds = %490
  %498 = sub nsw i64 %494, %495
  br label %499

; <label>:499:                                    ; preds = %499, %497
  %500 = phi i64 [ 0, %497 ], [ %545, %499 ]
  %501 = phi i64 [ %498, %497 ], [ %546, %499 ]
  %502 = getelementptr inbounds i32, i32* %6, i64 %500
  %503 = bitcast i32* %502 to <4 x i32>*
  %504 = load <4 x i32>, <4 x i32>* %503, align 4, !tbaa !3, !alias.scope !21
  %505 = getelementptr i32, i32* %502, i64 4
  %506 = bitcast i32* %505 to <4 x i32>*
  %507 = load <4 x i32>, <4 x i32>* %506, align 4, !tbaa !3, !alias.scope !21
  %508 = getelementptr inbounds i32, i32* %5, i64 %500
  %509 = bitcast i32* %508 to <4 x i32>*
  store <4 x i32> %504, <4 x i32>* %509, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %510 = getelementptr i32, i32* %508, i64 4
  %511 = bitcast i32* %510 to <4 x i32>*
  store <4 x i32> %507, <4 x i32>* %511, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %512 = or i64 %500, 8
  %513 = getelementptr inbounds i32, i32* %6, i64 %512
  %514 = bitcast i32* %513 to <4 x i32>*
  %515 = load <4 x i32>, <4 x i32>* %514, align 4, !tbaa !3, !alias.scope !21
  %516 = getelementptr i32, i32* %513, i64 4
  %517 = bitcast i32* %516 to <4 x i32>*
  %518 = load <4 x i32>, <4 x i32>* %517, align 4, !tbaa !3, !alias.scope !21
  %519 = getelementptr inbounds i32, i32* %5, i64 %512
  %520 = bitcast i32* %519 to <4 x i32>*
  store <4 x i32> %515, <4 x i32>* %520, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %521 = getelementptr i32, i32* %519, i64 4
  %522 = bitcast i32* %521 to <4 x i32>*
  store <4 x i32> %518, <4 x i32>* %522, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %523 = or i64 %500, 16
  %524 = getelementptr inbounds i32, i32* %6, i64 %523
  %525 = bitcast i32* %524 to <4 x i32>*
  %526 = load <4 x i32>, <4 x i32>* %525, align 4, !tbaa !3, !alias.scope !21
  %527 = getelementptr i32, i32* %524, i64 4
  %528 = bitcast i32* %527 to <4 x i32>*
  %529 = load <4 x i32>, <4 x i32>* %528, align 4, !tbaa !3, !alias.scope !21
  %530 = getelementptr inbounds i32, i32* %5, i64 %523
  %531 = bitcast i32* %530 to <4 x i32>*
  store <4 x i32> %526, <4 x i32>* %531, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %532 = getelementptr i32, i32* %530, i64 4
  %533 = bitcast i32* %532 to <4 x i32>*
  store <4 x i32> %529, <4 x i32>* %533, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %534 = or i64 %500, 24
  %535 = getelementptr inbounds i32, i32* %6, i64 %534
  %536 = bitcast i32* %535 to <4 x i32>*
  %537 = load <4 x i32>, <4 x i32>* %536, align 4, !tbaa !3, !alias.scope !21
  %538 = getelementptr i32, i32* %535, i64 4
  %539 = bitcast i32* %538 to <4 x i32>*
  %540 = load <4 x i32>, <4 x i32>* %539, align 4, !tbaa !3, !alias.scope !21
  %541 = getelementptr inbounds i32, i32* %5, i64 %534
  %542 = bitcast i32* %541 to <4 x i32>*
  store <4 x i32> %537, <4 x i32>* %542, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %543 = getelementptr i32, i32* %541, i64 4
  %544 = bitcast i32* %543 to <4 x i32>*
  store <4 x i32> %540, <4 x i32>* %544, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %545 = add i64 %500, 32
  %546 = add i64 %501, -4
  %547 = icmp eq i64 %546, 0
  br i1 %547, label %548, label %499, !llvm.loop !26

; <label>:548:                                    ; preds = %499, %490
  %549 = phi i64 [ 0, %490 ], [ %545, %499 ]
  %550 = icmp eq i64 %495, 0
  br i1 %550, label %568, label %551

; <label>:551:                                    ; preds = %548
  br label %552

; <label>:552:                                    ; preds = %552, %551
  %553 = phi i64 [ %549, %551 ], [ %565, %552 ]
  %554 = phi i64 [ %495, %551 ], [ %566, %552 ]
  %555 = getelementptr inbounds i32, i32* %6, i64 %553
  %556 = bitcast i32* %555 to <4 x i32>*
  %557 = load <4 x i32>, <4 x i32>* %556, align 4, !tbaa !3, !alias.scope !21
  %558 = getelementptr i32, i32* %555, i64 4
  %559 = bitcast i32* %558 to <4 x i32>*
  %560 = load <4 x i32>, <4 x i32>* %559, align 4, !tbaa !3, !alias.scope !21
  %561 = getelementptr inbounds i32, i32* %5, i64 %553
  %562 = bitcast i32* %561 to <4 x i32>*
  store <4 x i32> %557, <4 x i32>* %562, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %563 = getelementptr i32, i32* %561, i64 4
  %564 = bitcast i32* %563 to <4 x i32>*
  store <4 x i32> %560, <4 x i32>* %564, align 4, !tbaa !3, !alias.scope !24, !noalias !21
  %565 = add i64 %553, 8
  %566 = add i64 %554, -1
  %567 = icmp eq i64 %566, 0
  br i1 %567, label %568, label %552, !llvm.loop !27

; <label>:568:                                    ; preds = %552, %548
  %569 = icmp eq i64 %491, %482
  br i1 %569, label %609, label %570

; <label>:570:                                    ; preds = %568, %484, %481
  %571 = phi i64 [ 0, %484 ], [ 0, %481 ], [ %491, %568 ]
  %572 = add nsw i64 %482, -1
  %573 = sub nsw i64 %572, %571
  %574 = and i64 %482, 3
  %575 = icmp eq i64 %574, 0
  br i1 %575, label %586, label %576

; <label>:576:                                    ; preds = %570
  br label %577

; <label>:577:                                    ; preds = %577, %576
  %578 = phi i64 [ %583, %577 ], [ %571, %576 ]
  %579 = phi i64 [ %584, %577 ], [ %574, %576 ]
  %580 = getelementptr inbounds i32, i32* %6, i64 %578
  %581 = load i32, i32* %580, align 4, !tbaa !3
  %582 = getelementptr inbounds i32, i32* %5, i64 %578
  store i32 %581, i32* %582, align 4, !tbaa !3
  %583 = add nuw nsw i64 %578, 1
  %584 = add i64 %579, -1
  %585 = icmp eq i64 %584, 0
  br i1 %585, label %586, label %577, !llvm.loop !28

; <label>:586:                                    ; preds = %577, %570
  %587 = phi i64 [ %571, %570 ], [ %583, %577 ]
  %588 = icmp ult i64 %573, 3
  br i1 %588, label %609, label %589

; <label>:589:                                    ; preds = %586
  br label %590

; <label>:590:                                    ; preds = %590, %589
  %591 = phi i64 [ %587, %589 ], [ %607, %590 ]
  %592 = getelementptr inbounds i32, i32* %6, i64 %591
  %593 = load i32, i32* %592, align 4, !tbaa !3
  %594 = getelementptr inbounds i32, i32* %5, i64 %591
  store i32 %593, i32* %594, align 4, !tbaa !3
  %595 = add nuw nsw i64 %591, 1
  %596 = getelementptr inbounds i32, i32* %6, i64 %595
  %597 = load i32, i32* %596, align 4, !tbaa !3
  %598 = getelementptr inbounds i32, i32* %5, i64 %595
  store i32 %597, i32* %598, align 4, !tbaa !3
  %599 = add nsw i64 %591, 2
  %600 = getelementptr inbounds i32, i32* %6, i64 %599
  %601 = load i32, i32* %600, align 4, !tbaa !3
  %602 = getelementptr inbounds i32, i32* %5, i64 %599
  store i32 %601, i32* %602, align 4, !tbaa !3
  %603 = add nsw i64 %591, 3
  %604 = getelementptr inbounds i32, i32* %6, i64 %603
  %605 = load i32, i32* %604, align 4, !tbaa !3
  %606 = getelementptr inbounds i32, i32* %5, i64 %603
  store i32 %605, i32* %606, align 4, !tbaa !3
  %607 = add nsw i64 %591, 4
  %608 = icmp eq i64 %607, %482
  br i1 %608, label %609, label %590, !llvm.loop !29

; <label>:609:                                    ; preds = %586, %590, %568, %480
  %610 = sext i32 %357 to i64
  %611 = getelementptr inbounds i32, i32* %5, i64 %610
  store i32 %0, i32* %611, align 4, !tbaa !3
  br i1 %13, label %612, label %656

; <label>:612:                                    ; preds = %609
  %613 = zext i32 %0 to i64
  %614 = and i64 %613, 1
  %615 = icmp eq i32 %0, 1
  br i1 %615, label %643, label %616

; <label>:616:                                    ; preds = %612
  %617 = sub nsw i64 %613, %614
  br label %618

; <label>:618:                                    ; preds = %618, %616
  %619 = phi i64 [ 0, %616 ], [ %640, %618 ]
  %620 = phi i64 [ %617, %616 ], [ %641, %618 ]
  %621 = getelementptr inbounds i32, i32* %10, i64 %619
  %622 = load i32, i32* %621, align 4, !tbaa !3
  %623 = sext i32 %622 to i64
  %624 = getelementptr inbounds i32, i32* %6, i64 %623
  %625 = load i32, i32* %624, align 4, !tbaa !3
  %626 = add nsw i32 %625, 1
  store i32 %626, i32* %624, align 4, !tbaa !3
  %627 = sext i32 %625 to i64
  %628 = getelementptr inbounds i32, i32* %4, i64 %627
  %629 = trunc i64 %619 to i32
  store i32 %629, i32* %628, align 4, !tbaa !3
  %630 = or i64 %619, 1
  %631 = getelementptr inbounds i32, i32* %10, i64 %630
  %632 = load i32, i32* %631, align 4, !tbaa !3
  %633 = sext i32 %632 to i64
  %634 = getelementptr inbounds i32, i32* %6, i64 %633
  %635 = load i32, i32* %634, align 4, !tbaa !3
  %636 = add nsw i32 %635, 1
  store i32 %636, i32* %634, align 4, !tbaa !3
  %637 = sext i32 %635 to i64
  %638 = getelementptr inbounds i32, i32* %4, i64 %637
  %639 = trunc i64 %630 to i32
  store i32 %639, i32* %638, align 4, !tbaa !3
  %640 = add nuw nsw i64 %619, 2
  %641 = add i64 %620, -2
  %642 = icmp eq i64 %641, 0
  br i1 %642, label %643, label %618

; <label>:643:                                    ; preds = %618, %612
  %644 = phi i64 [ 0, %612 ], [ %640, %618 ]
  %645 = icmp eq i64 %614, 0
  br i1 %645, label %656, label %646

; <label>:646:                                    ; preds = %643
  %647 = getelementptr inbounds i32, i32* %10, i64 %644
  %648 = load i32, i32* %647, align 4, !tbaa !3
  %649 = sext i32 %648 to i64
  %650 = getelementptr inbounds i32, i32* %6, i64 %649
  %651 = load i32, i32* %650, align 4, !tbaa !3
  %652 = add nsw i32 %651, 1
  store i32 %652, i32* %650, align 4, !tbaa !3
  %653 = sext i32 %651 to i64
  %654 = getelementptr inbounds i32, i32* %4, i64 %653
  %655 = trunc i64 %644 to i32
  store i32 %655, i32* %654, align 4, !tbaa !3
  br label %656

; <label>:656:                                    ; preds = %646, %643, %360, %609
  %657 = phi i32 [ 0, %360 ], [ %357, %609 ], [ %357, %643 ], [ %357, %646 ]
  %658 = icmp eq i32* %3, null
  %659 = xor i1 %13, true
  %660 = or i1 %658, %659
  br i1 %660, label %846, label %661

; <label>:661:                                    ; preds = %656
  %662 = zext i32 %0 to i64
  %663 = add nsw i64 %662, -1
  %664 = and i64 %662, 3
  %665 = icmp ult i64 %663, 3
  br i1 %665, label %701, label %666

; <label>:666:                                    ; preds = %661
  %667 = sub nsw i64 %662, %664
  br label %668

; <label>:668:                                    ; preds = %668, %666
  %669 = phi i64 [ 0, %666 ], [ %698, %668 ]
  %670 = phi i64 [ %667, %666 ], [ %699, %668 ]
  %671 = getelementptr inbounds i32, i32* %4, i64 %669
  %672 = load i32, i32* %671, align 4, !tbaa !3
  %673 = sext i32 %672 to i64
  %674 = getelementptr inbounds i32, i32* %3, i64 %673
  %675 = load i32, i32* %674, align 4, !tbaa !3
  %676 = getelementptr inbounds i32, i32* %6, i64 %669
  store i32 %675, i32* %676, align 4, !tbaa !3
  %677 = or i64 %669, 1
  %678 = getelementptr inbounds i32, i32* %4, i64 %677
  %679 = load i32, i32* %678, align 4, !tbaa !3
  %680 = sext i32 %679 to i64
  %681 = getelementptr inbounds i32, i32* %3, i64 %680
  %682 = load i32, i32* %681, align 4, !tbaa !3
  %683 = getelementptr inbounds i32, i32* %6, i64 %677
  store i32 %682, i32* %683, align 4, !tbaa !3
  %684 = or i64 %669, 2
  %685 = getelementptr inbounds i32, i32* %4, i64 %684
  %686 = load i32, i32* %685, align 4, !tbaa !3
  %687 = sext i32 %686 to i64
  %688 = getelementptr inbounds i32, i32* %3, i64 %687
  %689 = load i32, i32* %688, align 4, !tbaa !3
  %690 = getelementptr inbounds i32, i32* %6, i64 %684
  store i32 %689, i32* %690, align 4, !tbaa !3
  %691 = or i64 %669, 3
  %692 = getelementptr inbounds i32, i32* %4, i64 %691
  %693 = load i32, i32* %692, align 4, !tbaa !3
  %694 = sext i32 %693 to i64
  %695 = getelementptr inbounds i32, i32* %3, i64 %694
  %696 = load i32, i32* %695, align 4, !tbaa !3
  %697 = getelementptr inbounds i32, i32* %6, i64 %691
  store i32 %696, i32* %697, align 4, !tbaa !3
  %698 = add nuw nsw i64 %669, 4
  %699 = add i64 %670, -4
  %700 = icmp eq i64 %699, 0
  br i1 %700, label %701, label %668

; <label>:701:                                    ; preds = %668, %661
  %702 = phi i64 [ 0, %661 ], [ %698, %668 ]
  %703 = icmp eq i64 %664, 0
  br i1 %703, label %717, label %704

; <label>:704:                                    ; preds = %701
  br label %705

; <label>:705:                                    ; preds = %705, %704
  %706 = phi i64 [ %702, %704 ], [ %714, %705 ]
  %707 = phi i64 [ %664, %704 ], [ %715, %705 ]
  %708 = getelementptr inbounds i32, i32* %4, i64 %706
  %709 = load i32, i32* %708, align 4, !tbaa !3
  %710 = sext i32 %709 to i64
  %711 = getelementptr inbounds i32, i32* %3, i64 %710
  %712 = load i32, i32* %711, align 4, !tbaa !3
  %713 = getelementptr inbounds i32, i32* %6, i64 %706
  store i32 %712, i32* %713, align 4, !tbaa !3
  %714 = add nuw nsw i64 %706, 1
  %715 = add i64 %707, -1
  %716 = icmp eq i64 %715, 0
  br i1 %716, label %717, label %705, !llvm.loop !30

; <label>:717:                                    ; preds = %705, %701
  br i1 %13, label %718, label %846

; <label>:718:                                    ; preds = %717
  %719 = zext i32 %0 to i64
  %720 = icmp ult i32 %0, 8
  br i1 %720, label %807, label %721

; <label>:721:                                    ; preds = %718
  %722 = getelementptr i32, i32* %3, i64 %662
  %723 = getelementptr i32, i32* %6, i64 %662
  %724 = icmp ugt i32* %723, %3
  %725 = icmp ugt i32* %722, %6
  %726 = and i1 %724, %725
  br i1 %726, label %807, label %727

; <label>:727:                                    ; preds = %721
  %728 = and i64 %662, 4294967288
  %729 = add nsw i64 %728, -8
  %730 = lshr exact i64 %729, 3
  %731 = add nuw nsw i64 %730, 1
  %732 = and i64 %731, 3
  %733 = icmp ult i64 %729, 24
  br i1 %733, label %785, label %734

; <label>:734:                                    ; preds = %727
  %735 = sub nsw i64 %731, %732
  br label %736

; <label>:736:                                    ; preds = %736, %734
  %737 = phi i64 [ 0, %734 ], [ %782, %736 ]
  %738 = phi i64 [ %735, %734 ], [ %783, %736 ]
  %739 = getelementptr inbounds i32, i32* %6, i64 %737
  %740 = bitcast i32* %739 to <4 x i32>*
  %741 = load <4 x i32>, <4 x i32>* %740, align 4, !tbaa !3, !alias.scope !31
  %742 = getelementptr i32, i32* %739, i64 4
  %743 = bitcast i32* %742 to <4 x i32>*
  %744 = load <4 x i32>, <4 x i32>* %743, align 4, !tbaa !3, !alias.scope !31
  %745 = getelementptr inbounds i32, i32* %3, i64 %737
  %746 = bitcast i32* %745 to <4 x i32>*
  store <4 x i32> %741, <4 x i32>* %746, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %747 = getelementptr i32, i32* %745, i64 4
  %748 = bitcast i32* %747 to <4 x i32>*
  store <4 x i32> %744, <4 x i32>* %748, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %749 = or i64 %737, 8
  %750 = getelementptr inbounds i32, i32* %6, i64 %749
  %751 = bitcast i32* %750 to <4 x i32>*
  %752 = load <4 x i32>, <4 x i32>* %751, align 4, !tbaa !3, !alias.scope !31
  %753 = getelementptr i32, i32* %750, i64 4
  %754 = bitcast i32* %753 to <4 x i32>*
  %755 = load <4 x i32>, <4 x i32>* %754, align 4, !tbaa !3, !alias.scope !31
  %756 = getelementptr inbounds i32, i32* %3, i64 %749
  %757 = bitcast i32* %756 to <4 x i32>*
  store <4 x i32> %752, <4 x i32>* %757, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %758 = getelementptr i32, i32* %756, i64 4
  %759 = bitcast i32* %758 to <4 x i32>*
  store <4 x i32> %755, <4 x i32>* %759, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %760 = or i64 %737, 16
  %761 = getelementptr inbounds i32, i32* %6, i64 %760
  %762 = bitcast i32* %761 to <4 x i32>*
  %763 = load <4 x i32>, <4 x i32>* %762, align 4, !tbaa !3, !alias.scope !31
  %764 = getelementptr i32, i32* %761, i64 4
  %765 = bitcast i32* %764 to <4 x i32>*
  %766 = load <4 x i32>, <4 x i32>* %765, align 4, !tbaa !3, !alias.scope !31
  %767 = getelementptr inbounds i32, i32* %3, i64 %760
  %768 = bitcast i32* %767 to <4 x i32>*
  store <4 x i32> %763, <4 x i32>* %768, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %769 = getelementptr i32, i32* %767, i64 4
  %770 = bitcast i32* %769 to <4 x i32>*
  store <4 x i32> %766, <4 x i32>* %770, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %771 = or i64 %737, 24
  %772 = getelementptr inbounds i32, i32* %6, i64 %771
  %773 = bitcast i32* %772 to <4 x i32>*
  %774 = load <4 x i32>, <4 x i32>* %773, align 4, !tbaa !3, !alias.scope !31
  %775 = getelementptr i32, i32* %772, i64 4
  %776 = bitcast i32* %775 to <4 x i32>*
  %777 = load <4 x i32>, <4 x i32>* %776, align 4, !tbaa !3, !alias.scope !31
  %778 = getelementptr inbounds i32, i32* %3, i64 %771
  %779 = bitcast i32* %778 to <4 x i32>*
  store <4 x i32> %774, <4 x i32>* %779, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %780 = getelementptr i32, i32* %778, i64 4
  %781 = bitcast i32* %780 to <4 x i32>*
  store <4 x i32> %777, <4 x i32>* %781, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %782 = add i64 %737, 32
  %783 = add i64 %738, -4
  %784 = icmp eq i64 %783, 0
  br i1 %784, label %785, label %736, !llvm.loop !36

; <label>:785:                                    ; preds = %736, %727
  %786 = phi i64 [ 0, %727 ], [ %782, %736 ]
  %787 = icmp eq i64 %732, 0
  br i1 %787, label %805, label %788

; <label>:788:                                    ; preds = %785
  br label %789

; <label>:789:                                    ; preds = %789, %788
  %790 = phi i64 [ %786, %788 ], [ %802, %789 ]
  %791 = phi i64 [ %732, %788 ], [ %803, %789 ]
  %792 = getelementptr inbounds i32, i32* %6, i64 %790
  %793 = bitcast i32* %792 to <4 x i32>*
  %794 = load <4 x i32>, <4 x i32>* %793, align 4, !tbaa !3, !alias.scope !31
  %795 = getelementptr i32, i32* %792, i64 4
  %796 = bitcast i32* %795 to <4 x i32>*
  %797 = load <4 x i32>, <4 x i32>* %796, align 4, !tbaa !3, !alias.scope !31
  %798 = getelementptr inbounds i32, i32* %3, i64 %790
  %799 = bitcast i32* %798 to <4 x i32>*
  store <4 x i32> %794, <4 x i32>* %799, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %800 = getelementptr i32, i32* %798, i64 4
  %801 = bitcast i32* %800 to <4 x i32>*
  store <4 x i32> %797, <4 x i32>* %801, align 4, !tbaa !3, !alias.scope !34, !noalias !31
  %802 = add i64 %790, 8
  %803 = add i64 %791, -1
  %804 = icmp eq i64 %803, 0
  br i1 %804, label %805, label %789, !llvm.loop !37

; <label>:805:                                    ; preds = %789, %785
  %806 = icmp eq i64 %728, %662
  br i1 %806, label %846, label %807

; <label>:807:                                    ; preds = %805, %721, %718
  %808 = phi i64 [ 0, %721 ], [ 0, %718 ], [ %728, %805 ]
  %809 = add nsw i64 %719, -1
  %810 = sub nsw i64 %809, %808
  %811 = and i64 %719, 3
  %812 = icmp eq i64 %811, 0
  br i1 %812, label %823, label %813

; <label>:813:                                    ; preds = %807
  br label %814

; <label>:814:                                    ; preds = %814, %813
  %815 = phi i64 [ %820, %814 ], [ %808, %813 ]
  %816 = phi i64 [ %821, %814 ], [ %811, %813 ]
  %817 = getelementptr inbounds i32, i32* %6, i64 %815
  %818 = load i32, i32* %817, align 4, !tbaa !3
  %819 = getelementptr inbounds i32, i32* %3, i64 %815
  store i32 %818, i32* %819, align 4, !tbaa !3
  %820 = add nuw nsw i64 %815, 1
  %821 = add i64 %816, -1
  %822 = icmp eq i64 %821, 0
  br i1 %822, label %823, label %814, !llvm.loop !38

; <label>:823:                                    ; preds = %814, %807
  %824 = phi i64 [ %808, %807 ], [ %820, %814 ]
  %825 = icmp ult i64 %810, 3
  br i1 %825, label %846, label %826

; <label>:826:                                    ; preds = %823
  br label %827

; <label>:827:                                    ; preds = %827, %826
  %828 = phi i64 [ %824, %826 ], [ %844, %827 ]
  %829 = getelementptr inbounds i32, i32* %6, i64 %828
  %830 = load i32, i32* %829, align 4, !tbaa !3
  %831 = getelementptr inbounds i32, i32* %3, i64 %828
  store i32 %830, i32* %831, align 4, !tbaa !3
  %832 = add nuw nsw i64 %828, 1
  %833 = getelementptr inbounds i32, i32* %6, i64 %832
  %834 = load i32, i32* %833, align 4, !tbaa !3
  %835 = getelementptr inbounds i32, i32* %3, i64 %832
  store i32 %834, i32* %835, align 4, !tbaa !3
  %836 = add nsw i64 %828, 2
  %837 = getelementptr inbounds i32, i32* %6, i64 %836
  %838 = load i32, i32* %837, align 4, !tbaa !3
  %839 = getelementptr inbounds i32, i32* %3, i64 %836
  store i32 %838, i32* %839, align 4, !tbaa !3
  %840 = add nsw i64 %828, 3
  %841 = getelementptr inbounds i32, i32* %6, i64 %840
  %842 = load i32, i32* %841, align 4, !tbaa !3
  %843 = getelementptr inbounds i32, i32* %3, i64 %840
  store i32 %842, i32* %843, align 4, !tbaa !3
  %844 = add nsw i64 %828, 4
  %845 = icmp eq i64 %844, %719
  br i1 %845, label %846, label %827, !llvm.loop !39

; <label>:846:                                    ; preds = %823, %827, %805, %656, %717
  ret i32 %657
}

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
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
!10 = !{!11, !12}
!11 = distinct !{!11, !9}
!12 = distinct !{!12, !9}
!13 = !{!11}
!14 = !{!12}
!15 = distinct !{!15, !16}
!16 = !{!"llvm.loop.isvectorized", i32 1}
!17 = distinct !{!17, !16}
!18 = distinct !{!18, !19}
!19 = !{!"llvm.loop.unroll.disable"}
!20 = distinct !{!20, !19}
!21 = !{!22}
!22 = distinct !{!22, !23}
!23 = distinct !{!23, !"LVerDomain"}
!24 = !{!25}
!25 = distinct !{!25, !23}
!26 = distinct !{!26, !16}
!27 = distinct !{!27, !19}
!28 = distinct !{!28, !19}
!29 = distinct !{!29, !16}
!30 = distinct !{!30, !19}
!31 = !{!32}
!32 = distinct !{!32, !33}
!33 = distinct !{!33, !"LVerDomain"}
!34 = !{!35}
!35 = distinct !{!35, !33}
!36 = distinct !{!36, !16}
!37 = distinct !{!37, !19}
!38 = distinct !{!38, !19}
!39 = distinct !{!39, !16}
