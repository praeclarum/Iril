; ModuleID = 'amd_preprocess.c'
source_filename = "amd_preprocess.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: norecurse nounwind ssp uwtable
define void @amd_preprocess(i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture) local_unnamed_addr #0 {
  %8 = icmp sgt i32 %0, 0
  br i1 %8, label %9, label %183

; <label>:9:                                      ; preds = %7
  %10 = zext i32 %0 to i64
  %11 = icmp ult i32 %0, 8
  br i1 %11, label %88, label %12

; <label>:12:                                     ; preds = %9
  %13 = getelementptr i32, i32* %5, i64 %10
  %14 = getelementptr i32, i32* %6, i64 %10
  %15 = icmp ugt i32* %14, %5
  %16 = icmp ugt i32* %13, %6
  %17 = and i1 %15, %16
  br i1 %17, label %88, label %18

; <label>:18:                                     ; preds = %12
  %19 = and i64 %10, 4294967288
  %20 = add nsw i64 %19, -8
  %21 = lshr exact i64 %20, 3
  %22 = add nuw nsw i64 %21, 1
  %23 = and i64 %22, 3
  %24 = icmp ult i64 %20, 24
  br i1 %24, label %68, label %25

; <label>:25:                                     ; preds = %18
  %26 = sub nsw i64 %22, %23
  br label %27

; <label>:27:                                     ; preds = %27, %25
  %28 = phi i64 [ 0, %25 ], [ %65, %27 ]
  %29 = phi i64 [ %26, %25 ], [ %66, %27 ]
  %30 = getelementptr inbounds i32, i32* %5, i64 %28
  %31 = bitcast i32* %30 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %31, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %32 = getelementptr i32, i32* %30, i64 4
  %33 = bitcast i32* %32 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %33, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %34 = getelementptr inbounds i32, i32* %6, i64 %28
  %35 = bitcast i32* %34 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %35, align 4, !tbaa !3, !alias.scope !10
  %36 = getelementptr i32, i32* %34, i64 4
  %37 = bitcast i32* %36 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %37, align 4, !tbaa !3, !alias.scope !10
  %38 = or i64 %28, 8
  %39 = getelementptr inbounds i32, i32* %5, i64 %38
  %40 = bitcast i32* %39 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %40, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %41 = getelementptr i32, i32* %39, i64 4
  %42 = bitcast i32* %41 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %42, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %43 = getelementptr inbounds i32, i32* %6, i64 %38
  %44 = bitcast i32* %43 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %44, align 4, !tbaa !3, !alias.scope !10
  %45 = getelementptr i32, i32* %43, i64 4
  %46 = bitcast i32* %45 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %46, align 4, !tbaa !3, !alias.scope !10
  %47 = or i64 %28, 16
  %48 = getelementptr inbounds i32, i32* %5, i64 %47
  %49 = bitcast i32* %48 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %49, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %50 = getelementptr i32, i32* %48, i64 4
  %51 = bitcast i32* %50 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %51, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %52 = getelementptr inbounds i32, i32* %6, i64 %47
  %53 = bitcast i32* %52 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %53, align 4, !tbaa !3, !alias.scope !10
  %54 = getelementptr i32, i32* %52, i64 4
  %55 = bitcast i32* %54 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %55, align 4, !tbaa !3, !alias.scope !10
  %56 = or i64 %28, 24
  %57 = getelementptr inbounds i32, i32* %5, i64 %56
  %58 = bitcast i32* %57 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %58, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %59 = getelementptr i32, i32* %57, i64 4
  %60 = bitcast i32* %59 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %60, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %61 = getelementptr inbounds i32, i32* %6, i64 %56
  %62 = bitcast i32* %61 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %62, align 4, !tbaa !3, !alias.scope !10
  %63 = getelementptr i32, i32* %61, i64 4
  %64 = bitcast i32* %63 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %64, align 4, !tbaa !3, !alias.scope !10
  %65 = add i64 %28, 32
  %66 = add i64 %29, -4
  %67 = icmp eq i64 %66, 0
  br i1 %67, label %68, label %27, !llvm.loop !12

; <label>:68:                                     ; preds = %27, %18
  %69 = phi i64 [ 0, %18 ], [ %65, %27 ]
  %70 = icmp eq i64 %23, 0
  br i1 %70, label %86, label %71

; <label>:71:                                     ; preds = %68
  br label %72

; <label>:72:                                     ; preds = %72, %71
  %73 = phi i64 [ %69, %71 ], [ %83, %72 ]
  %74 = phi i64 [ %23, %71 ], [ %84, %72 ]
  %75 = getelementptr inbounds i32, i32* %5, i64 %73
  %76 = bitcast i32* %75 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %76, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %77 = getelementptr i32, i32* %75, i64 4
  %78 = bitcast i32* %77 to <4 x i32>*
  store <4 x i32> zeroinitializer, <4 x i32>* %78, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %79 = getelementptr inbounds i32, i32* %6, i64 %73
  %80 = bitcast i32* %79 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %80, align 4, !tbaa !3, !alias.scope !10
  %81 = getelementptr i32, i32* %79, i64 4
  %82 = bitcast i32* %81 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %82, align 4, !tbaa !3, !alias.scope !10
  %83 = add i64 %73, 8
  %84 = add i64 %74, -1
  %85 = icmp eq i64 %84, 0
  br i1 %85, label %86, label %72, !llvm.loop !14

; <label>:86:                                     ; preds = %72, %68
  %87 = icmp eq i64 %19, %10
  br i1 %87, label %122, label %88

; <label>:88:                                     ; preds = %86, %12, %9
  %89 = phi i64 [ 0, %12 ], [ 0, %9 ], [ %19, %86 ]
  %90 = add nsw i64 %10, -1
  %91 = sub nsw i64 %90, %89
  %92 = and i64 %10, 3
  %93 = icmp eq i64 %92, 0
  br i1 %93, label %103, label %94

; <label>:94:                                     ; preds = %88
  br label %95

; <label>:95:                                     ; preds = %95, %94
  %96 = phi i64 [ %100, %95 ], [ %89, %94 ]
  %97 = phi i64 [ %101, %95 ], [ %92, %94 ]
  %98 = getelementptr inbounds i32, i32* %5, i64 %96
  store i32 0, i32* %98, align 4, !tbaa !3
  %99 = getelementptr inbounds i32, i32* %6, i64 %96
  store i32 -1, i32* %99, align 4, !tbaa !3
  %100 = add nuw nsw i64 %96, 1
  %101 = add i64 %97, -1
  %102 = icmp eq i64 %101, 0
  br i1 %102, label %103, label %95, !llvm.loop !16

; <label>:103:                                    ; preds = %95, %88
  %104 = phi i64 [ %89, %88 ], [ %100, %95 ]
  %105 = icmp ult i64 %91, 3
  br i1 %105, label %122, label %106

; <label>:106:                                    ; preds = %103
  br label %107

; <label>:107:                                    ; preds = %107, %106
  %108 = phi i64 [ %104, %106 ], [ %120, %107 ]
  %109 = getelementptr inbounds i32, i32* %5, i64 %108
  store i32 0, i32* %109, align 4, !tbaa !3
  %110 = getelementptr inbounds i32, i32* %6, i64 %108
  store i32 -1, i32* %110, align 4, !tbaa !3
  %111 = add nuw nsw i64 %108, 1
  %112 = getelementptr inbounds i32, i32* %5, i64 %111
  store i32 0, i32* %112, align 4, !tbaa !3
  %113 = getelementptr inbounds i32, i32* %6, i64 %111
  store i32 -1, i32* %113, align 4, !tbaa !3
  %114 = add nsw i64 %108, 2
  %115 = getelementptr inbounds i32, i32* %5, i64 %114
  store i32 0, i32* %115, align 4, !tbaa !3
  %116 = getelementptr inbounds i32, i32* %6, i64 %114
  store i32 -1, i32* %116, align 4, !tbaa !3
  %117 = add nsw i64 %108, 3
  %118 = getelementptr inbounds i32, i32* %5, i64 %117
  store i32 0, i32* %118, align 4, !tbaa !3
  %119 = getelementptr inbounds i32, i32* %6, i64 %117
  store i32 -1, i32* %119, align 4, !tbaa !3
  %120 = add nsw i64 %108, 4
  %121 = icmp eq i64 %120, %10
  br i1 %121, label %122, label %107, !llvm.loop !17

; <label>:122:                                    ; preds = %103, %107, %86
  br i1 %8, label %123, label %183

; <label>:123:                                    ; preds = %122
  %124 = zext i32 %0 to i64
  br label %125

; <label>:125:                                    ; preds = %181, %123
  %126 = phi i64 [ 0, %123 ], [ %127, %181 ]
  %127 = add nuw nsw i64 %126, 1
  %128 = getelementptr inbounds i32, i32* %1, i64 %127
  %129 = load i32, i32* %128, align 4, !tbaa !3
  %130 = getelementptr inbounds i32, i32* %1, i64 %126
  %131 = load i32, i32* %130, align 4, !tbaa !3
  %132 = icmp slt i32 %131, %129
  br i1 %132, label %133, label %181

; <label>:133:                                    ; preds = %125
  %134 = sext i32 %131 to i64
  %135 = sext i32 %129 to i64
  %136 = trunc i64 %126 to i32
  %137 = sub nsw i64 %135, %134
  %138 = add nsw i64 %135, -1
  %139 = and i64 %137, 1
  %140 = icmp eq i64 %139, 0
  br i1 %140, label %155, label %141

; <label>:141:                                    ; preds = %133
  %142 = getelementptr inbounds i32, i32* %2, i64 %134
  %143 = load i32, i32* %142, align 4, !tbaa !3
  %144 = sext i32 %143 to i64
  %145 = getelementptr inbounds i32, i32* %6, i64 %144
  %146 = load i32, i32* %145, align 4, !tbaa !3
  %147 = zext i32 %146 to i64
  %148 = icmp eq i64 %126, %147
  br i1 %148, label %153, label %149

; <label>:149:                                    ; preds = %141
  %150 = getelementptr inbounds i32, i32* %5, i64 %144
  %151 = load i32, i32* %150, align 4, !tbaa !3
  %152 = add nsw i32 %151, 1
  store i32 %152, i32* %150, align 4, !tbaa !3
  store i32 %136, i32* %145, align 4, !tbaa !3
  br label %153

; <label>:153:                                    ; preds = %149, %141
  %154 = add nsw i64 %134, 1
  br label %155

; <label>:155:                                    ; preds = %153, %133
  %156 = phi i64 [ %154, %153 ], [ %134, %133 ]
  %157 = icmp eq i64 %138, %134
  br i1 %157, label %181, label %158

; <label>:158:                                    ; preds = %155
  br label %159

; <label>:159:                                    ; preds = %370, %158
  %160 = phi i64 [ %156, %158 ], [ %371, %370 ]
  %161 = getelementptr inbounds i32, i32* %2, i64 %160
  %162 = load i32, i32* %161, align 4, !tbaa !3
  %163 = sext i32 %162 to i64
  %164 = getelementptr inbounds i32, i32* %6, i64 %163
  %165 = load i32, i32* %164, align 4, !tbaa !3
  %166 = zext i32 %165 to i64
  %167 = icmp eq i64 %126, %166
  br i1 %167, label %172, label %168

; <label>:168:                                    ; preds = %159
  %169 = getelementptr inbounds i32, i32* %5, i64 %163
  %170 = load i32, i32* %169, align 4, !tbaa !3
  %171 = add nsw i32 %170, 1
  store i32 %171, i32* %169, align 4, !tbaa !3
  store i32 %136, i32* %164, align 4, !tbaa !3
  br label %172

; <label>:172:                                    ; preds = %159, %168
  %173 = add nsw i64 %160, 1
  %174 = getelementptr inbounds i32, i32* %2, i64 %173
  %175 = load i32, i32* %174, align 4, !tbaa !3
  %176 = sext i32 %175 to i64
  %177 = getelementptr inbounds i32, i32* %6, i64 %176
  %178 = load i32, i32* %177, align 4, !tbaa !3
  %179 = zext i32 %178 to i64
  %180 = icmp eq i64 %126, %179
  br i1 %180, label %370, label %366

; <label>:181:                                    ; preds = %155, %370, %125
  %182 = icmp eq i64 %127, %124
  br i1 %182, label %184, label %125

; <label>:183:                                    ; preds = %122, %7
  store i32 0, i32* %3, align 4, !tbaa !3
  br label %365

; <label>:184:                                    ; preds = %181
  store i32 0, i32* %3, align 4, !tbaa !3
  br i1 %8, label %185, label %365

; <label>:185:                                    ; preds = %184
  %186 = add nsw i64 %10, -1
  %187 = and i64 %10, 3
  %188 = icmp ult i64 %186, 3
  br i1 %188, label %217, label %189

; <label>:189:                                    ; preds = %185
  %190 = sub nsw i64 %10, %187
  br label %191

; <label>:191:                                    ; preds = %191, %189
  %192 = phi i32 [ 0, %189 ], [ %212, %191 ]
  %193 = phi i64 [ 0, %189 ], [ %213, %191 ]
  %194 = phi i64 [ %190, %189 ], [ %215, %191 ]
  %195 = getelementptr inbounds i32, i32* %5, i64 %193
  %196 = load i32, i32* %195, align 4, !tbaa !3
  %197 = add nsw i32 %196, %192
  %198 = or i64 %193, 1
  %199 = getelementptr inbounds i32, i32* %3, i64 %198
  store i32 %197, i32* %199, align 4, !tbaa !3
  %200 = getelementptr inbounds i32, i32* %5, i64 %198
  %201 = load i32, i32* %200, align 4, !tbaa !3
  %202 = add nsw i32 %201, %197
  %203 = or i64 %193, 2
  %204 = getelementptr inbounds i32, i32* %3, i64 %203
  store i32 %202, i32* %204, align 4, !tbaa !3
  %205 = getelementptr inbounds i32, i32* %5, i64 %203
  %206 = load i32, i32* %205, align 4, !tbaa !3
  %207 = add nsw i32 %206, %202
  %208 = or i64 %193, 3
  %209 = getelementptr inbounds i32, i32* %3, i64 %208
  store i32 %207, i32* %209, align 4, !tbaa !3
  %210 = getelementptr inbounds i32, i32* %5, i64 %208
  %211 = load i32, i32* %210, align 4, !tbaa !3
  %212 = add nsw i32 %211, %207
  %213 = add nuw nsw i64 %193, 4
  %214 = getelementptr inbounds i32, i32* %3, i64 %213
  store i32 %212, i32* %214, align 4, !tbaa !3
  %215 = add i64 %194, -4
  %216 = icmp eq i64 %215, 0
  br i1 %216, label %217, label %191

; <label>:217:                                    ; preds = %191, %185
  %218 = phi i32 [ 0, %185 ], [ %212, %191 ]
  %219 = phi i64 [ 0, %185 ], [ %213, %191 ]
  %220 = icmp eq i64 %187, 0
  br i1 %220, label %233, label %221

; <label>:221:                                    ; preds = %217
  br label %222

; <label>:222:                                    ; preds = %222, %221
  %223 = phi i32 [ %218, %221 ], [ %228, %222 ]
  %224 = phi i64 [ %219, %221 ], [ %229, %222 ]
  %225 = phi i64 [ %187, %221 ], [ %231, %222 ]
  %226 = getelementptr inbounds i32, i32* %5, i64 %224
  %227 = load i32, i32* %226, align 4, !tbaa !3
  %228 = add nsw i32 %227, %223
  %229 = add nuw nsw i64 %224, 1
  %230 = getelementptr inbounds i32, i32* %3, i64 %229
  store i32 %228, i32* %230, align 4, !tbaa !3
  %231 = add i64 %225, -1
  %232 = icmp eq i64 %231, 0
  br i1 %232, label %233, label %222, !llvm.loop !18

; <label>:233:                                    ; preds = %222, %217
  br i1 %8, label %234, label %365

; <label>:234:                                    ; preds = %233
  %235 = zext i32 %0 to i64
  store i32 0, i32* %5, align 4, !tbaa !3
  store i32 -1, i32* %6, align 4, !tbaa !3
  %236 = icmp eq i32 %0, 1
  br i1 %236, label %330, label %237

; <label>:237:                                    ; preds = %234
  %238 = add nsw i64 %10, -1
  %239 = icmp ult i64 %238, 8
  br i1 %239, label %285, label %240

; <label>:240:                                    ; preds = %237
  %241 = getelementptr i32, i32* %5, i64 1
  %242 = getelementptr i32, i32* %5, i64 %10
  %243 = getelementptr i32, i32* %6, i64 1
  %244 = getelementptr i32, i32* %6, i64 %10
  %245 = getelementptr i32, i32* %3, i64 1
  %246 = getelementptr i32, i32* %3, i64 %10
  %247 = icmp ult i32* %241, %244
  %248 = icmp ult i32* %243, %242
  %249 = and i1 %247, %248
  %250 = icmp ult i32* %241, %246
  %251 = icmp ult i32* %245, %242
  %252 = and i1 %250, %251
  %253 = or i1 %249, %252
  %254 = icmp ult i32* %243, %246
  %255 = icmp ult i32* %245, %244
  %256 = and i1 %254, %255
  %257 = or i1 %253, %256
  br i1 %257, label %285, label %258

; <label>:258:                                    ; preds = %240
  %259 = add i32 %0, 7
  %260 = and i32 %259, 7
  %261 = zext i32 %260 to i64
  %262 = sub nsw i64 %238, %261
  %263 = add nsw i64 %262, 1
  br label %264

; <label>:264:                                    ; preds = %264, %258
  %265 = phi i64 [ 0, %258 ], [ %281, %264 ]
  %266 = or i64 %265, 1
  %267 = getelementptr inbounds i32, i32* %3, i64 %266
  %268 = bitcast i32* %267 to <4 x i32>*
  %269 = load <4 x i32>, <4 x i32>* %268, align 4, !tbaa !3, !alias.scope !19
  %270 = getelementptr i32, i32* %267, i64 4
  %271 = bitcast i32* %270 to <4 x i32>*
  %272 = load <4 x i32>, <4 x i32>* %271, align 4, !tbaa !3, !alias.scope !19
  %273 = getelementptr inbounds i32, i32* %5, i64 %266
  %274 = bitcast i32* %273 to <4 x i32>*
  store <4 x i32> %269, <4 x i32>* %274, align 4, !tbaa !3, !alias.scope !22, !noalias !24
  %275 = getelementptr i32, i32* %273, i64 4
  %276 = bitcast i32* %275 to <4 x i32>*
  store <4 x i32> %272, <4 x i32>* %276, align 4, !tbaa !3, !alias.scope !22, !noalias !24
  %277 = getelementptr inbounds i32, i32* %6, i64 %266
  %278 = bitcast i32* %277 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %278, align 4, !tbaa !3, !alias.scope !26, !noalias !19
  %279 = getelementptr i32, i32* %277, i64 4
  %280 = bitcast i32* %279 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %280, align 4, !tbaa !3, !alias.scope !26, !noalias !19
  %281 = add i64 %265, 8
  %282 = icmp eq i64 %281, %262
  br i1 %282, label %283, label %264, !llvm.loop !27

; <label>:283:                                    ; preds = %264
  %284 = icmp eq i32 %260, 0
  br i1 %284, label %330, label %285

; <label>:285:                                    ; preds = %283, %240, %237
  %286 = phi i64 [ 1, %240 ], [ 1, %237 ], [ %263, %283 ]
  %287 = sub nsw i64 %235, %286
  %288 = add nsw i64 %235, -1
  %289 = sub nsw i64 %288, %286
  %290 = and i64 %287, 3
  %291 = icmp eq i64 %290, 0
  br i1 %291, label %303, label %292

; <label>:292:                                    ; preds = %285
  br label %293

; <label>:293:                                    ; preds = %293, %292
  %294 = phi i64 [ %300, %293 ], [ %286, %292 ]
  %295 = phi i64 [ %301, %293 ], [ %290, %292 ]
  %296 = getelementptr inbounds i32, i32* %3, i64 %294
  %297 = load i32, i32* %296, align 4, !tbaa !3
  %298 = getelementptr inbounds i32, i32* %5, i64 %294
  store i32 %297, i32* %298, align 4, !tbaa !3
  %299 = getelementptr inbounds i32, i32* %6, i64 %294
  store i32 -1, i32* %299, align 4, !tbaa !3
  %300 = add nuw nsw i64 %294, 1
  %301 = add i64 %295, -1
  %302 = icmp eq i64 %301, 0
  br i1 %302, label %303, label %293, !llvm.loop !28

; <label>:303:                                    ; preds = %293, %285
  %304 = phi i64 [ %286, %285 ], [ %300, %293 ]
  %305 = icmp ult i64 %289, 3
  br i1 %305, label %330, label %306

; <label>:306:                                    ; preds = %303
  br label %307

; <label>:307:                                    ; preds = %307, %306
  %308 = phi i64 [ %304, %306 ], [ %328, %307 ]
  %309 = getelementptr inbounds i32, i32* %3, i64 %308
  %310 = load i32, i32* %309, align 4, !tbaa !3
  %311 = getelementptr inbounds i32, i32* %5, i64 %308
  store i32 %310, i32* %311, align 4, !tbaa !3
  %312 = getelementptr inbounds i32, i32* %6, i64 %308
  store i32 -1, i32* %312, align 4, !tbaa !3
  %313 = add nuw nsw i64 %308, 1
  %314 = getelementptr inbounds i32, i32* %3, i64 %313
  %315 = load i32, i32* %314, align 4, !tbaa !3
  %316 = getelementptr inbounds i32, i32* %5, i64 %313
  store i32 %315, i32* %316, align 4, !tbaa !3
  %317 = getelementptr inbounds i32, i32* %6, i64 %313
  store i32 -1, i32* %317, align 4, !tbaa !3
  %318 = add nsw i64 %308, 2
  %319 = getelementptr inbounds i32, i32* %3, i64 %318
  %320 = load i32, i32* %319, align 4, !tbaa !3
  %321 = getelementptr inbounds i32, i32* %5, i64 %318
  store i32 %320, i32* %321, align 4, !tbaa !3
  %322 = getelementptr inbounds i32, i32* %6, i64 %318
  store i32 -1, i32* %322, align 4, !tbaa !3
  %323 = add nsw i64 %308, 3
  %324 = getelementptr inbounds i32, i32* %3, i64 %323
  %325 = load i32, i32* %324, align 4, !tbaa !3
  %326 = getelementptr inbounds i32, i32* %5, i64 %323
  store i32 %325, i32* %326, align 4, !tbaa !3
  %327 = getelementptr inbounds i32, i32* %6, i64 %323
  store i32 -1, i32* %327, align 4, !tbaa !3
  %328 = add nsw i64 %308, 4
  %329 = icmp eq i64 %328, %235
  br i1 %329, label %330, label %307, !llvm.loop !29

; <label>:330:                                    ; preds = %303, %307, %283, %234
  br i1 %8, label %331, label %365

; <label>:331:                                    ; preds = %330
  %332 = zext i32 %0 to i64
  br label %333

; <label>:333:                                    ; preds = %363, %331
  %334 = phi i64 [ 0, %331 ], [ %335, %363 ]
  %335 = add nuw nsw i64 %334, 1
  %336 = getelementptr inbounds i32, i32* %1, i64 %335
  %337 = load i32, i32* %336, align 4, !tbaa !3
  %338 = getelementptr inbounds i32, i32* %1, i64 %334
  %339 = load i32, i32* %338, align 4, !tbaa !3
  %340 = icmp slt i32 %339, %337
  br i1 %340, label %341, label %363

; <label>:341:                                    ; preds = %333
  %342 = sext i32 %339 to i64
  %343 = sext i32 %337 to i64
  %344 = trunc i64 %334 to i32
  br label %345

; <label>:345:                                    ; preds = %360, %341
  %346 = phi i64 [ %342, %341 ], [ %361, %360 ]
  %347 = getelementptr inbounds i32, i32* %2, i64 %346
  %348 = load i32, i32* %347, align 4, !tbaa !3
  %349 = sext i32 %348 to i64
  %350 = getelementptr inbounds i32, i32* %6, i64 %349
  %351 = load i32, i32* %350, align 4, !tbaa !3
  %352 = zext i32 %351 to i64
  %353 = icmp eq i64 %334, %352
  br i1 %353, label %360, label %354

; <label>:354:                                    ; preds = %345
  %355 = getelementptr inbounds i32, i32* %5, i64 %349
  %356 = load i32, i32* %355, align 4, !tbaa !3
  %357 = add nsw i32 %356, 1
  store i32 %357, i32* %355, align 4, !tbaa !3
  %358 = sext i32 %356 to i64
  %359 = getelementptr inbounds i32, i32* %4, i64 %358
  store i32 %344, i32* %359, align 4, !tbaa !3
  store i32 %344, i32* %350, align 4, !tbaa !3
  br label %360

; <label>:360:                                    ; preds = %345, %354
  %361 = add nsw i64 %346, 1
  %362 = icmp eq i64 %361, %343
  br i1 %362, label %363, label %345

; <label>:363:                                    ; preds = %360, %333
  %364 = icmp eq i64 %335, %332
  br i1 %364, label %365, label %333

; <label>:365:                                    ; preds = %363, %184, %183, %233, %330
  ret void

; <label>:366:                                    ; preds = %172
  %367 = getelementptr inbounds i32, i32* %5, i64 %176
  %368 = load i32, i32* %367, align 4, !tbaa !3
  %369 = add nsw i32 %368, 1
  store i32 %369, i32* %367, align 4, !tbaa !3
  store i32 %136, i32* %177, align 4, !tbaa !3
  br label %370

; <label>:370:                                    ; preds = %366, %172
  %371 = add nsw i64 %160, 2
  %372 = icmp eq i64 %371, %135
  br i1 %372, label %181, label %159
}

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }

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
!12 = distinct !{!12, !13}
!13 = !{!"llvm.loop.isvectorized", i32 1}
!14 = distinct !{!14, !15}
!15 = !{!"llvm.loop.unroll.disable"}
!16 = distinct !{!16, !15}
!17 = distinct !{!17, !13}
!18 = distinct !{!18, !15}
!19 = !{!20}
!20 = distinct !{!20, !21}
!21 = distinct !{!21, !"LVerDomain"}
!22 = !{!23}
!23 = distinct !{!23, !21}
!24 = !{!25, !20}
!25 = distinct !{!25, !21}
!26 = !{!25}
!27 = distinct !{!27, !13}
!28 = distinct !{!28, !15}
!29 = distinct !{!29, !13}
