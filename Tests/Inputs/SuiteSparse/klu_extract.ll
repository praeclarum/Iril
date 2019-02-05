; ModuleID = 'klu_extract.c'
source_filename = "klu_extract.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

@.memset_pattern = private unnamed_addr constant [2 x double] [double 1.000000e+00, double 1.000000e+00], align 16

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_extract(%struct.klu_numeric* readonly, %struct.klu_symbolic* readonly, i32*, i32*, double*, i32*, i32*, double*, i32*, i32*, double*, i32*, i32*, double*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %17 = bitcast double* %13 to i8*
  %18 = icmp eq %struct.klu_common_struct* %15, null
  br i1 %18, label %1565, label %19

; <label>:19:                                     ; preds = %16
  %20 = icmp eq %struct.klu_symbolic* %1, null
  %21 = icmp eq %struct.klu_numeric* %0, null
  %22 = or i1 %21, %20
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %15, i64 0, i32 11
  br i1 %22, label %24, label %25

; <label>:24:                                     ; preds = %19
  store i32 -3, i32* %23, align 4, !tbaa !3
  br label %1565

; <label>:25:                                     ; preds = %19
  store i32 0, i32* %23, align 4, !tbaa !3
  %26 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 5
  %27 = load i32, i32* %26, align 8, !tbaa !11
  %28 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 11
  %29 = load i32, i32* %28, align 4, !tbaa !13
  %30 = icmp eq double* %13, null
  br i1 %30, label %204, label %31

; <label>:31:                                     ; preds = %25
  %32 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 15
  %33 = load double*, double** %32, align 8, !tbaa !14
  %34 = icmp eq double* %33, null
  %35 = icmp sgt i32 %27, 0
  br i1 %34, label %200, label %36

; <label>:36:                                     ; preds = %31
  br i1 %35, label %37, label %204

; <label>:37:                                     ; preds = %36
  %38 = zext i32 %27 to i64
  %39 = icmp ult i32 %27, 4
  br i1 %39, label %126, label %40

; <label>:40:                                     ; preds = %37
  %41 = getelementptr double, double* %13, i64 %38
  %42 = getelementptr double, double* %33, i64 %38
  %43 = icmp ugt double* %42, %13
  %44 = icmp ult double* %33, %41
  %45 = and i1 %43, %44
  br i1 %45, label %126, label %46

; <label>:46:                                     ; preds = %40
  %47 = and i64 %38, 4294967292
  %48 = add nsw i64 %47, -4
  %49 = lshr exact i64 %48, 2
  %50 = add nuw nsw i64 %49, 1
  %51 = and i64 %50, 3
  %52 = icmp ult i64 %48, 12
  br i1 %52, label %104, label %53

; <label>:53:                                     ; preds = %46
  %54 = sub nsw i64 %50, %51
  br label %55

; <label>:55:                                     ; preds = %55, %53
  %56 = phi i64 [ 0, %53 ], [ %101, %55 ]
  %57 = phi i64 [ %54, %53 ], [ %102, %55 ]
  %58 = getelementptr inbounds double, double* %33, i64 %56
  %59 = bitcast double* %58 to <2 x i64>*
  %60 = load <2 x i64>, <2 x i64>* %59, align 8, !tbaa !16, !alias.scope !17
  %61 = getelementptr double, double* %58, i64 2
  %62 = bitcast double* %61 to <2 x i64>*
  %63 = load <2 x i64>, <2 x i64>* %62, align 8, !tbaa !16, !alias.scope !17
  %64 = getelementptr inbounds double, double* %13, i64 %56
  %65 = bitcast double* %64 to <2 x i64>*
  store <2 x i64> %60, <2 x i64>* %65, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %66 = getelementptr double, double* %64, i64 2
  %67 = bitcast double* %66 to <2 x i64>*
  store <2 x i64> %63, <2 x i64>* %67, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %68 = or i64 %56, 4
  %69 = getelementptr inbounds double, double* %33, i64 %68
  %70 = bitcast double* %69 to <2 x i64>*
  %71 = load <2 x i64>, <2 x i64>* %70, align 8, !tbaa !16, !alias.scope !17
  %72 = getelementptr double, double* %69, i64 2
  %73 = bitcast double* %72 to <2 x i64>*
  %74 = load <2 x i64>, <2 x i64>* %73, align 8, !tbaa !16, !alias.scope !17
  %75 = getelementptr inbounds double, double* %13, i64 %68
  %76 = bitcast double* %75 to <2 x i64>*
  store <2 x i64> %71, <2 x i64>* %76, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %77 = getelementptr double, double* %75, i64 2
  %78 = bitcast double* %77 to <2 x i64>*
  store <2 x i64> %74, <2 x i64>* %78, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %79 = or i64 %56, 8
  %80 = getelementptr inbounds double, double* %33, i64 %79
  %81 = bitcast double* %80 to <2 x i64>*
  %82 = load <2 x i64>, <2 x i64>* %81, align 8, !tbaa !16, !alias.scope !17
  %83 = getelementptr double, double* %80, i64 2
  %84 = bitcast double* %83 to <2 x i64>*
  %85 = load <2 x i64>, <2 x i64>* %84, align 8, !tbaa !16, !alias.scope !17
  %86 = getelementptr inbounds double, double* %13, i64 %79
  %87 = bitcast double* %86 to <2 x i64>*
  store <2 x i64> %82, <2 x i64>* %87, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %88 = getelementptr double, double* %86, i64 2
  %89 = bitcast double* %88 to <2 x i64>*
  store <2 x i64> %85, <2 x i64>* %89, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %90 = or i64 %56, 12
  %91 = getelementptr inbounds double, double* %33, i64 %90
  %92 = bitcast double* %91 to <2 x i64>*
  %93 = load <2 x i64>, <2 x i64>* %92, align 8, !tbaa !16, !alias.scope !17
  %94 = getelementptr double, double* %91, i64 2
  %95 = bitcast double* %94 to <2 x i64>*
  %96 = load <2 x i64>, <2 x i64>* %95, align 8, !tbaa !16, !alias.scope !17
  %97 = getelementptr inbounds double, double* %13, i64 %90
  %98 = bitcast double* %97 to <2 x i64>*
  store <2 x i64> %93, <2 x i64>* %98, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %99 = getelementptr double, double* %97, i64 2
  %100 = bitcast double* %99 to <2 x i64>*
  store <2 x i64> %96, <2 x i64>* %100, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %101 = add i64 %56, 16
  %102 = add i64 %57, -4
  %103 = icmp eq i64 %102, 0
  br i1 %103, label %104, label %55, !llvm.loop !22

; <label>:104:                                    ; preds = %55, %46
  %105 = phi i64 [ 0, %46 ], [ %101, %55 ]
  %106 = icmp eq i64 %51, 0
  br i1 %106, label %124, label %107

; <label>:107:                                    ; preds = %104
  br label %108

; <label>:108:                                    ; preds = %108, %107
  %109 = phi i64 [ %105, %107 ], [ %121, %108 ]
  %110 = phi i64 [ %51, %107 ], [ %122, %108 ]
  %111 = getelementptr inbounds double, double* %33, i64 %109
  %112 = bitcast double* %111 to <2 x i64>*
  %113 = load <2 x i64>, <2 x i64>* %112, align 8, !tbaa !16, !alias.scope !17
  %114 = getelementptr double, double* %111, i64 2
  %115 = bitcast double* %114 to <2 x i64>*
  %116 = load <2 x i64>, <2 x i64>* %115, align 8, !tbaa !16, !alias.scope !17
  %117 = getelementptr inbounds double, double* %13, i64 %109
  %118 = bitcast double* %117 to <2 x i64>*
  store <2 x i64> %113, <2 x i64>* %118, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %119 = getelementptr double, double* %117, i64 2
  %120 = bitcast double* %119 to <2 x i64>*
  store <2 x i64> %116, <2 x i64>* %120, align 8, !tbaa !16, !alias.scope !20, !noalias !17
  %121 = add i64 %109, 4
  %122 = add i64 %110, -1
  %123 = icmp eq i64 %122, 0
  br i1 %123, label %124, label %108, !llvm.loop !24

; <label>:124:                                    ; preds = %108, %104
  %125 = icmp eq i64 %47, %38
  br i1 %125, label %204, label %126

; <label>:126:                                    ; preds = %124, %40, %37
  %127 = phi i64 [ 0, %40 ], [ 0, %37 ], [ %47, %124 ]
  %128 = sub nsw i64 %38, %127
  %129 = add nsw i64 %38, -1
  %130 = sub nsw i64 %129, %127
  %131 = and i64 %128, 7
  %132 = icmp eq i64 %131, 0
  br i1 %132, label %145, label %133

; <label>:133:                                    ; preds = %126
  br label %134

; <label>:134:                                    ; preds = %134, %133
  %135 = phi i64 [ %142, %134 ], [ %127, %133 ]
  %136 = phi i64 [ %143, %134 ], [ %131, %133 ]
  %137 = getelementptr inbounds double, double* %33, i64 %135
  %138 = bitcast double* %137 to i64*
  %139 = load i64, i64* %138, align 8, !tbaa !16
  %140 = getelementptr inbounds double, double* %13, i64 %135
  %141 = bitcast double* %140 to i64*
  store i64 %139, i64* %141, align 8, !tbaa !16
  %142 = add nuw nsw i64 %135, 1
  %143 = add i64 %136, -1
  %144 = icmp eq i64 %143, 0
  br i1 %144, label %145, label %134, !llvm.loop !26

; <label>:145:                                    ; preds = %134, %126
  %146 = phi i64 [ %127, %126 ], [ %142, %134 ]
  %147 = icmp ult i64 %130, 7
  br i1 %147, label %204, label %148

; <label>:148:                                    ; preds = %145
  br label %149

; <label>:149:                                    ; preds = %149, %148
  %150 = phi i64 [ %146, %148 ], [ %198, %149 ]
  %151 = getelementptr inbounds double, double* %33, i64 %150
  %152 = bitcast double* %151 to i64*
  %153 = load i64, i64* %152, align 8, !tbaa !16
  %154 = getelementptr inbounds double, double* %13, i64 %150
  %155 = bitcast double* %154 to i64*
  store i64 %153, i64* %155, align 8, !tbaa !16
  %156 = add nuw nsw i64 %150, 1
  %157 = getelementptr inbounds double, double* %33, i64 %156
  %158 = bitcast double* %157 to i64*
  %159 = load i64, i64* %158, align 8, !tbaa !16
  %160 = getelementptr inbounds double, double* %13, i64 %156
  %161 = bitcast double* %160 to i64*
  store i64 %159, i64* %161, align 8, !tbaa !16
  %162 = add nsw i64 %150, 2
  %163 = getelementptr inbounds double, double* %33, i64 %162
  %164 = bitcast double* %163 to i64*
  %165 = load i64, i64* %164, align 8, !tbaa !16
  %166 = getelementptr inbounds double, double* %13, i64 %162
  %167 = bitcast double* %166 to i64*
  store i64 %165, i64* %167, align 8, !tbaa !16
  %168 = add nsw i64 %150, 3
  %169 = getelementptr inbounds double, double* %33, i64 %168
  %170 = bitcast double* %169 to i64*
  %171 = load i64, i64* %170, align 8, !tbaa !16
  %172 = getelementptr inbounds double, double* %13, i64 %168
  %173 = bitcast double* %172 to i64*
  store i64 %171, i64* %173, align 8, !tbaa !16
  %174 = add nsw i64 %150, 4
  %175 = getelementptr inbounds double, double* %33, i64 %174
  %176 = bitcast double* %175 to i64*
  %177 = load i64, i64* %176, align 8, !tbaa !16
  %178 = getelementptr inbounds double, double* %13, i64 %174
  %179 = bitcast double* %178 to i64*
  store i64 %177, i64* %179, align 8, !tbaa !16
  %180 = add nsw i64 %150, 5
  %181 = getelementptr inbounds double, double* %33, i64 %180
  %182 = bitcast double* %181 to i64*
  %183 = load i64, i64* %182, align 8, !tbaa !16
  %184 = getelementptr inbounds double, double* %13, i64 %180
  %185 = bitcast double* %184 to i64*
  store i64 %183, i64* %185, align 8, !tbaa !16
  %186 = add nsw i64 %150, 6
  %187 = getelementptr inbounds double, double* %33, i64 %186
  %188 = bitcast double* %187 to i64*
  %189 = load i64, i64* %188, align 8, !tbaa !16
  %190 = getelementptr inbounds double, double* %13, i64 %186
  %191 = bitcast double* %190 to i64*
  store i64 %189, i64* %191, align 8, !tbaa !16
  %192 = add nsw i64 %150, 7
  %193 = getelementptr inbounds double, double* %33, i64 %192
  %194 = bitcast double* %193 to i64*
  %195 = load i64, i64* %194, align 8, !tbaa !16
  %196 = getelementptr inbounds double, double* %13, i64 %192
  %197 = bitcast double* %196 to i64*
  store i64 %195, i64* %197, align 8, !tbaa !16
  %198 = add nsw i64 %150, 8
  %199 = icmp eq i64 %198, %38
  br i1 %199, label %204, label %149, !llvm.loop !27

; <label>:200:                                    ; preds = %31
  br i1 %35, label %201, label %204

; <label>:201:                                    ; preds = %200
  %202 = zext i32 %27 to i64
  %203 = shl nuw nsw i64 %202, 3
  call void @memset_pattern16(i8* %17, i8* bitcast ([2 x double]* @.memset_pattern to i8*), i64 %203) #2
  br label %204

; <label>:204:                                    ; preds = %145, %149, %124, %201, %36, %200, %25
  %205 = icmp eq i32* %14, null
  %206 = icmp slt i32 %29, 0
  %207 = or i1 %205, %206
  br i1 %207, label %339, label %208

; <label>:208:                                    ; preds = %204
  %209 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 9
  %210 = load i32*, i32** %209, align 8, !tbaa !28
  %211 = add i32 %29, 1
  %212 = zext i32 %211 to i64
  %213 = icmp ult i32 %211, 8
  br i1 %213, label %300, label %214

; <label>:214:                                    ; preds = %208
  %215 = getelementptr i32, i32* %14, i64 %212
  %216 = getelementptr i32, i32* %210, i64 %212
  %217 = icmp ugt i32* %216, %14
  %218 = icmp ult i32* %210, %215
  %219 = and i1 %217, %218
  br i1 %219, label %300, label %220

; <label>:220:                                    ; preds = %214
  %221 = and i64 %212, 4294967288
  %222 = add nsw i64 %221, -8
  %223 = lshr exact i64 %222, 3
  %224 = add nuw nsw i64 %223, 1
  %225 = and i64 %224, 3
  %226 = icmp ult i64 %222, 24
  br i1 %226, label %278, label %227

; <label>:227:                                    ; preds = %220
  %228 = sub nsw i64 %224, %225
  br label %229

; <label>:229:                                    ; preds = %229, %227
  %230 = phi i64 [ 0, %227 ], [ %275, %229 ]
  %231 = phi i64 [ %228, %227 ], [ %276, %229 ]
  %232 = getelementptr inbounds i32, i32* %210, i64 %230
  %233 = bitcast i32* %232 to <4 x i32>*
  %234 = load <4 x i32>, <4 x i32>* %233, align 4, !tbaa !29, !alias.scope !30
  %235 = getelementptr i32, i32* %232, i64 4
  %236 = bitcast i32* %235 to <4 x i32>*
  %237 = load <4 x i32>, <4 x i32>* %236, align 4, !tbaa !29, !alias.scope !30
  %238 = getelementptr inbounds i32, i32* %14, i64 %230
  %239 = bitcast i32* %238 to <4 x i32>*
  store <4 x i32> %234, <4 x i32>* %239, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %240 = getelementptr i32, i32* %238, i64 4
  %241 = bitcast i32* %240 to <4 x i32>*
  store <4 x i32> %237, <4 x i32>* %241, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %242 = or i64 %230, 8
  %243 = getelementptr inbounds i32, i32* %210, i64 %242
  %244 = bitcast i32* %243 to <4 x i32>*
  %245 = load <4 x i32>, <4 x i32>* %244, align 4, !tbaa !29, !alias.scope !30
  %246 = getelementptr i32, i32* %243, i64 4
  %247 = bitcast i32* %246 to <4 x i32>*
  %248 = load <4 x i32>, <4 x i32>* %247, align 4, !tbaa !29, !alias.scope !30
  %249 = getelementptr inbounds i32, i32* %14, i64 %242
  %250 = bitcast i32* %249 to <4 x i32>*
  store <4 x i32> %245, <4 x i32>* %250, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %251 = getelementptr i32, i32* %249, i64 4
  %252 = bitcast i32* %251 to <4 x i32>*
  store <4 x i32> %248, <4 x i32>* %252, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %253 = or i64 %230, 16
  %254 = getelementptr inbounds i32, i32* %210, i64 %253
  %255 = bitcast i32* %254 to <4 x i32>*
  %256 = load <4 x i32>, <4 x i32>* %255, align 4, !tbaa !29, !alias.scope !30
  %257 = getelementptr i32, i32* %254, i64 4
  %258 = bitcast i32* %257 to <4 x i32>*
  %259 = load <4 x i32>, <4 x i32>* %258, align 4, !tbaa !29, !alias.scope !30
  %260 = getelementptr inbounds i32, i32* %14, i64 %253
  %261 = bitcast i32* %260 to <4 x i32>*
  store <4 x i32> %256, <4 x i32>* %261, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %262 = getelementptr i32, i32* %260, i64 4
  %263 = bitcast i32* %262 to <4 x i32>*
  store <4 x i32> %259, <4 x i32>* %263, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %264 = or i64 %230, 24
  %265 = getelementptr inbounds i32, i32* %210, i64 %264
  %266 = bitcast i32* %265 to <4 x i32>*
  %267 = load <4 x i32>, <4 x i32>* %266, align 4, !tbaa !29, !alias.scope !30
  %268 = getelementptr i32, i32* %265, i64 4
  %269 = bitcast i32* %268 to <4 x i32>*
  %270 = load <4 x i32>, <4 x i32>* %269, align 4, !tbaa !29, !alias.scope !30
  %271 = getelementptr inbounds i32, i32* %14, i64 %264
  %272 = bitcast i32* %271 to <4 x i32>*
  store <4 x i32> %267, <4 x i32>* %272, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %273 = getelementptr i32, i32* %271, i64 4
  %274 = bitcast i32* %273 to <4 x i32>*
  store <4 x i32> %270, <4 x i32>* %274, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %275 = add i64 %230, 32
  %276 = add i64 %231, -4
  %277 = icmp eq i64 %276, 0
  br i1 %277, label %278, label %229, !llvm.loop !35

; <label>:278:                                    ; preds = %229, %220
  %279 = phi i64 [ 0, %220 ], [ %275, %229 ]
  %280 = icmp eq i64 %225, 0
  br i1 %280, label %298, label %281

; <label>:281:                                    ; preds = %278
  br label %282

; <label>:282:                                    ; preds = %282, %281
  %283 = phi i64 [ %279, %281 ], [ %295, %282 ]
  %284 = phi i64 [ %225, %281 ], [ %296, %282 ]
  %285 = getelementptr inbounds i32, i32* %210, i64 %283
  %286 = bitcast i32* %285 to <4 x i32>*
  %287 = load <4 x i32>, <4 x i32>* %286, align 4, !tbaa !29, !alias.scope !30
  %288 = getelementptr i32, i32* %285, i64 4
  %289 = bitcast i32* %288 to <4 x i32>*
  %290 = load <4 x i32>, <4 x i32>* %289, align 4, !tbaa !29, !alias.scope !30
  %291 = getelementptr inbounds i32, i32* %14, i64 %283
  %292 = bitcast i32* %291 to <4 x i32>*
  store <4 x i32> %287, <4 x i32>* %292, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %293 = getelementptr i32, i32* %291, i64 4
  %294 = bitcast i32* %293 to <4 x i32>*
  store <4 x i32> %290, <4 x i32>* %294, align 4, !tbaa !29, !alias.scope !33, !noalias !30
  %295 = add i64 %283, 8
  %296 = add i64 %284, -1
  %297 = icmp eq i64 %296, 0
  br i1 %297, label %298, label %282, !llvm.loop !36

; <label>:298:                                    ; preds = %282, %278
  %299 = icmp eq i64 %221, %212
  br i1 %299, label %339, label %300

; <label>:300:                                    ; preds = %298, %214, %208
  %301 = phi i64 [ 0, %214 ], [ 0, %208 ], [ %221, %298 ]
  %302 = add nsw i64 %212, -1
  %303 = sub nsw i64 %302, %301
  %304 = and i64 %212, 3
  %305 = icmp eq i64 %304, 0
  br i1 %305, label %316, label %306

; <label>:306:                                    ; preds = %300
  br label %307

; <label>:307:                                    ; preds = %307, %306
  %308 = phi i64 [ %313, %307 ], [ %301, %306 ]
  %309 = phi i64 [ %314, %307 ], [ %304, %306 ]
  %310 = getelementptr inbounds i32, i32* %210, i64 %308
  %311 = load i32, i32* %310, align 4, !tbaa !29
  %312 = getelementptr inbounds i32, i32* %14, i64 %308
  store i32 %311, i32* %312, align 4, !tbaa !29
  %313 = add nuw nsw i64 %308, 1
  %314 = add i64 %309, -1
  %315 = icmp eq i64 %314, 0
  br i1 %315, label %316, label %307, !llvm.loop !37

; <label>:316:                                    ; preds = %307, %300
  %317 = phi i64 [ %301, %300 ], [ %313, %307 ]
  %318 = icmp ult i64 %303, 3
  br i1 %318, label %339, label %319

; <label>:319:                                    ; preds = %316
  br label %320

; <label>:320:                                    ; preds = %320, %319
  %321 = phi i64 [ %317, %319 ], [ %337, %320 ]
  %322 = getelementptr inbounds i32, i32* %210, i64 %321
  %323 = load i32, i32* %322, align 4, !tbaa !29
  %324 = getelementptr inbounds i32, i32* %14, i64 %321
  store i32 %323, i32* %324, align 4, !tbaa !29
  %325 = add nuw nsw i64 %321, 1
  %326 = getelementptr inbounds i32, i32* %210, i64 %325
  %327 = load i32, i32* %326, align 4, !tbaa !29
  %328 = getelementptr inbounds i32, i32* %14, i64 %325
  store i32 %327, i32* %328, align 4, !tbaa !29
  %329 = add nsw i64 %321, 2
  %330 = getelementptr inbounds i32, i32* %210, i64 %329
  %331 = load i32, i32* %330, align 4, !tbaa !29
  %332 = getelementptr inbounds i32, i32* %14, i64 %329
  store i32 %331, i32* %332, align 4, !tbaa !29
  %333 = add nsw i64 %321, 3
  %334 = getelementptr inbounds i32, i32* %210, i64 %333
  %335 = load i32, i32* %334, align 4, !tbaa !29
  %336 = getelementptr inbounds i32, i32* %14, i64 %333
  store i32 %335, i32* %336, align 4, !tbaa !29
  %337 = add nsw i64 %321, 4
  %338 = icmp eq i64 %337, %212
  br i1 %338, label %339, label %320, !llvm.loop !38

; <label>:339:                                    ; preds = %316, %320, %298, %204
  %340 = icmp ne i32* %11, null
  %341 = icmp sgt i32 %27, 0
  %342 = and i1 %340, %341
  br i1 %342, label %343, label %473

; <label>:343:                                    ; preds = %339
  %344 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 6
  %345 = load i32*, i32** %344, align 8, !tbaa !39
  %346 = zext i32 %27 to i64
  %347 = icmp ult i32 %27, 8
  br i1 %347, label %434, label %348

; <label>:348:                                    ; preds = %343
  %349 = getelementptr i32, i32* %11, i64 %346
  %350 = getelementptr i32, i32* %345, i64 %346
  %351 = icmp ugt i32* %350, %11
  %352 = icmp ult i32* %345, %349
  %353 = and i1 %351, %352
  br i1 %353, label %434, label %354

; <label>:354:                                    ; preds = %348
  %355 = and i64 %346, 4294967288
  %356 = add nsw i64 %355, -8
  %357 = lshr exact i64 %356, 3
  %358 = add nuw nsw i64 %357, 1
  %359 = and i64 %358, 3
  %360 = icmp ult i64 %356, 24
  br i1 %360, label %412, label %361

; <label>:361:                                    ; preds = %354
  %362 = sub nsw i64 %358, %359
  br label %363

; <label>:363:                                    ; preds = %363, %361
  %364 = phi i64 [ 0, %361 ], [ %409, %363 ]
  %365 = phi i64 [ %362, %361 ], [ %410, %363 ]
  %366 = getelementptr inbounds i32, i32* %345, i64 %364
  %367 = bitcast i32* %366 to <4 x i32>*
  %368 = load <4 x i32>, <4 x i32>* %367, align 4, !tbaa !29, !alias.scope !40
  %369 = getelementptr i32, i32* %366, i64 4
  %370 = bitcast i32* %369 to <4 x i32>*
  %371 = load <4 x i32>, <4 x i32>* %370, align 4, !tbaa !29, !alias.scope !40
  %372 = getelementptr inbounds i32, i32* %11, i64 %364
  %373 = bitcast i32* %372 to <4 x i32>*
  store <4 x i32> %368, <4 x i32>* %373, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %374 = getelementptr i32, i32* %372, i64 4
  %375 = bitcast i32* %374 to <4 x i32>*
  store <4 x i32> %371, <4 x i32>* %375, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %376 = or i64 %364, 8
  %377 = getelementptr inbounds i32, i32* %345, i64 %376
  %378 = bitcast i32* %377 to <4 x i32>*
  %379 = load <4 x i32>, <4 x i32>* %378, align 4, !tbaa !29, !alias.scope !40
  %380 = getelementptr i32, i32* %377, i64 4
  %381 = bitcast i32* %380 to <4 x i32>*
  %382 = load <4 x i32>, <4 x i32>* %381, align 4, !tbaa !29, !alias.scope !40
  %383 = getelementptr inbounds i32, i32* %11, i64 %376
  %384 = bitcast i32* %383 to <4 x i32>*
  store <4 x i32> %379, <4 x i32>* %384, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %385 = getelementptr i32, i32* %383, i64 4
  %386 = bitcast i32* %385 to <4 x i32>*
  store <4 x i32> %382, <4 x i32>* %386, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %387 = or i64 %364, 16
  %388 = getelementptr inbounds i32, i32* %345, i64 %387
  %389 = bitcast i32* %388 to <4 x i32>*
  %390 = load <4 x i32>, <4 x i32>* %389, align 4, !tbaa !29, !alias.scope !40
  %391 = getelementptr i32, i32* %388, i64 4
  %392 = bitcast i32* %391 to <4 x i32>*
  %393 = load <4 x i32>, <4 x i32>* %392, align 4, !tbaa !29, !alias.scope !40
  %394 = getelementptr inbounds i32, i32* %11, i64 %387
  %395 = bitcast i32* %394 to <4 x i32>*
  store <4 x i32> %390, <4 x i32>* %395, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %396 = getelementptr i32, i32* %394, i64 4
  %397 = bitcast i32* %396 to <4 x i32>*
  store <4 x i32> %393, <4 x i32>* %397, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %398 = or i64 %364, 24
  %399 = getelementptr inbounds i32, i32* %345, i64 %398
  %400 = bitcast i32* %399 to <4 x i32>*
  %401 = load <4 x i32>, <4 x i32>* %400, align 4, !tbaa !29, !alias.scope !40
  %402 = getelementptr i32, i32* %399, i64 4
  %403 = bitcast i32* %402 to <4 x i32>*
  %404 = load <4 x i32>, <4 x i32>* %403, align 4, !tbaa !29, !alias.scope !40
  %405 = getelementptr inbounds i32, i32* %11, i64 %398
  %406 = bitcast i32* %405 to <4 x i32>*
  store <4 x i32> %401, <4 x i32>* %406, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %407 = getelementptr i32, i32* %405, i64 4
  %408 = bitcast i32* %407 to <4 x i32>*
  store <4 x i32> %404, <4 x i32>* %408, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %409 = add i64 %364, 32
  %410 = add i64 %365, -4
  %411 = icmp eq i64 %410, 0
  br i1 %411, label %412, label %363, !llvm.loop !45

; <label>:412:                                    ; preds = %363, %354
  %413 = phi i64 [ 0, %354 ], [ %409, %363 ]
  %414 = icmp eq i64 %359, 0
  br i1 %414, label %432, label %415

; <label>:415:                                    ; preds = %412
  br label %416

; <label>:416:                                    ; preds = %416, %415
  %417 = phi i64 [ %413, %415 ], [ %429, %416 ]
  %418 = phi i64 [ %359, %415 ], [ %430, %416 ]
  %419 = getelementptr inbounds i32, i32* %345, i64 %417
  %420 = bitcast i32* %419 to <4 x i32>*
  %421 = load <4 x i32>, <4 x i32>* %420, align 4, !tbaa !29, !alias.scope !40
  %422 = getelementptr i32, i32* %419, i64 4
  %423 = bitcast i32* %422 to <4 x i32>*
  %424 = load <4 x i32>, <4 x i32>* %423, align 4, !tbaa !29, !alias.scope !40
  %425 = getelementptr inbounds i32, i32* %11, i64 %417
  %426 = bitcast i32* %425 to <4 x i32>*
  store <4 x i32> %421, <4 x i32>* %426, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %427 = getelementptr i32, i32* %425, i64 4
  %428 = bitcast i32* %427 to <4 x i32>*
  store <4 x i32> %424, <4 x i32>* %428, align 4, !tbaa !29, !alias.scope !43, !noalias !40
  %429 = add i64 %417, 8
  %430 = add i64 %418, -1
  %431 = icmp eq i64 %430, 0
  br i1 %431, label %432, label %416, !llvm.loop !46

; <label>:432:                                    ; preds = %416, %412
  %433 = icmp eq i64 %355, %346
  br i1 %433, label %473, label %434

; <label>:434:                                    ; preds = %432, %348, %343
  %435 = phi i64 [ 0, %348 ], [ 0, %343 ], [ %355, %432 ]
  %436 = add nsw i64 %346, -1
  %437 = sub nsw i64 %436, %435
  %438 = and i64 %346, 3
  %439 = icmp eq i64 %438, 0
  br i1 %439, label %450, label %440

; <label>:440:                                    ; preds = %434
  br label %441

; <label>:441:                                    ; preds = %441, %440
  %442 = phi i64 [ %447, %441 ], [ %435, %440 ]
  %443 = phi i64 [ %448, %441 ], [ %438, %440 ]
  %444 = getelementptr inbounds i32, i32* %345, i64 %442
  %445 = load i32, i32* %444, align 4, !tbaa !29
  %446 = getelementptr inbounds i32, i32* %11, i64 %442
  store i32 %445, i32* %446, align 4, !tbaa !29
  %447 = add nuw nsw i64 %442, 1
  %448 = add i64 %443, -1
  %449 = icmp eq i64 %448, 0
  br i1 %449, label %450, label %441, !llvm.loop !47

; <label>:450:                                    ; preds = %441, %434
  %451 = phi i64 [ %435, %434 ], [ %447, %441 ]
  %452 = icmp ult i64 %437, 3
  br i1 %452, label %473, label %453

; <label>:453:                                    ; preds = %450
  br label %454

; <label>:454:                                    ; preds = %454, %453
  %455 = phi i64 [ %451, %453 ], [ %471, %454 ]
  %456 = getelementptr inbounds i32, i32* %345, i64 %455
  %457 = load i32, i32* %456, align 4, !tbaa !29
  %458 = getelementptr inbounds i32, i32* %11, i64 %455
  store i32 %457, i32* %458, align 4, !tbaa !29
  %459 = add nuw nsw i64 %455, 1
  %460 = getelementptr inbounds i32, i32* %345, i64 %459
  %461 = load i32, i32* %460, align 4, !tbaa !29
  %462 = getelementptr inbounds i32, i32* %11, i64 %459
  store i32 %461, i32* %462, align 4, !tbaa !29
  %463 = add nsw i64 %455, 2
  %464 = getelementptr inbounds i32, i32* %345, i64 %463
  %465 = load i32, i32* %464, align 4, !tbaa !29
  %466 = getelementptr inbounds i32, i32* %11, i64 %463
  store i32 %465, i32* %466, align 4, !tbaa !29
  %467 = add nsw i64 %455, 3
  %468 = getelementptr inbounds i32, i32* %345, i64 %467
  %469 = load i32, i32* %468, align 4, !tbaa !29
  %470 = getelementptr inbounds i32, i32* %11, i64 %467
  store i32 %469, i32* %470, align 4, !tbaa !29
  %471 = add nsw i64 %455, 4
  %472 = icmp eq i64 %471, %346
  br i1 %472, label %473, label %454, !llvm.loop !48

; <label>:473:                                    ; preds = %450, %454, %432, %339
  %474 = icmp ne i32* %12, null
  %475 = and i1 %474, %341
  br i1 %475, label %476, label %606

; <label>:476:                                    ; preds = %473
  %477 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 8
  %478 = load i32*, i32** %477, align 8, !tbaa !49
  %479 = zext i32 %27 to i64
  %480 = icmp ult i32 %27, 8
  br i1 %480, label %567, label %481

; <label>:481:                                    ; preds = %476
  %482 = getelementptr i32, i32* %12, i64 %479
  %483 = getelementptr i32, i32* %478, i64 %479
  %484 = icmp ugt i32* %483, %12
  %485 = icmp ult i32* %478, %482
  %486 = and i1 %484, %485
  br i1 %486, label %567, label %487

; <label>:487:                                    ; preds = %481
  %488 = and i64 %479, 4294967288
  %489 = add nsw i64 %488, -8
  %490 = lshr exact i64 %489, 3
  %491 = add nuw nsw i64 %490, 1
  %492 = and i64 %491, 3
  %493 = icmp ult i64 %489, 24
  br i1 %493, label %545, label %494

; <label>:494:                                    ; preds = %487
  %495 = sub nsw i64 %491, %492
  br label %496

; <label>:496:                                    ; preds = %496, %494
  %497 = phi i64 [ 0, %494 ], [ %542, %496 ]
  %498 = phi i64 [ %495, %494 ], [ %543, %496 ]
  %499 = getelementptr inbounds i32, i32* %478, i64 %497
  %500 = bitcast i32* %499 to <4 x i32>*
  %501 = load <4 x i32>, <4 x i32>* %500, align 4, !tbaa !29, !alias.scope !50
  %502 = getelementptr i32, i32* %499, i64 4
  %503 = bitcast i32* %502 to <4 x i32>*
  %504 = load <4 x i32>, <4 x i32>* %503, align 4, !tbaa !29, !alias.scope !50
  %505 = getelementptr inbounds i32, i32* %12, i64 %497
  %506 = bitcast i32* %505 to <4 x i32>*
  store <4 x i32> %501, <4 x i32>* %506, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %507 = getelementptr i32, i32* %505, i64 4
  %508 = bitcast i32* %507 to <4 x i32>*
  store <4 x i32> %504, <4 x i32>* %508, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %509 = or i64 %497, 8
  %510 = getelementptr inbounds i32, i32* %478, i64 %509
  %511 = bitcast i32* %510 to <4 x i32>*
  %512 = load <4 x i32>, <4 x i32>* %511, align 4, !tbaa !29, !alias.scope !50
  %513 = getelementptr i32, i32* %510, i64 4
  %514 = bitcast i32* %513 to <4 x i32>*
  %515 = load <4 x i32>, <4 x i32>* %514, align 4, !tbaa !29, !alias.scope !50
  %516 = getelementptr inbounds i32, i32* %12, i64 %509
  %517 = bitcast i32* %516 to <4 x i32>*
  store <4 x i32> %512, <4 x i32>* %517, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %518 = getelementptr i32, i32* %516, i64 4
  %519 = bitcast i32* %518 to <4 x i32>*
  store <4 x i32> %515, <4 x i32>* %519, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %520 = or i64 %497, 16
  %521 = getelementptr inbounds i32, i32* %478, i64 %520
  %522 = bitcast i32* %521 to <4 x i32>*
  %523 = load <4 x i32>, <4 x i32>* %522, align 4, !tbaa !29, !alias.scope !50
  %524 = getelementptr i32, i32* %521, i64 4
  %525 = bitcast i32* %524 to <4 x i32>*
  %526 = load <4 x i32>, <4 x i32>* %525, align 4, !tbaa !29, !alias.scope !50
  %527 = getelementptr inbounds i32, i32* %12, i64 %520
  %528 = bitcast i32* %527 to <4 x i32>*
  store <4 x i32> %523, <4 x i32>* %528, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %529 = getelementptr i32, i32* %527, i64 4
  %530 = bitcast i32* %529 to <4 x i32>*
  store <4 x i32> %526, <4 x i32>* %530, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %531 = or i64 %497, 24
  %532 = getelementptr inbounds i32, i32* %478, i64 %531
  %533 = bitcast i32* %532 to <4 x i32>*
  %534 = load <4 x i32>, <4 x i32>* %533, align 4, !tbaa !29, !alias.scope !50
  %535 = getelementptr i32, i32* %532, i64 4
  %536 = bitcast i32* %535 to <4 x i32>*
  %537 = load <4 x i32>, <4 x i32>* %536, align 4, !tbaa !29, !alias.scope !50
  %538 = getelementptr inbounds i32, i32* %12, i64 %531
  %539 = bitcast i32* %538 to <4 x i32>*
  store <4 x i32> %534, <4 x i32>* %539, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %540 = getelementptr i32, i32* %538, i64 4
  %541 = bitcast i32* %540 to <4 x i32>*
  store <4 x i32> %537, <4 x i32>* %541, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %542 = add i64 %497, 32
  %543 = add i64 %498, -4
  %544 = icmp eq i64 %543, 0
  br i1 %544, label %545, label %496, !llvm.loop !55

; <label>:545:                                    ; preds = %496, %487
  %546 = phi i64 [ 0, %487 ], [ %542, %496 ]
  %547 = icmp eq i64 %492, 0
  br i1 %547, label %565, label %548

; <label>:548:                                    ; preds = %545
  br label %549

; <label>:549:                                    ; preds = %549, %548
  %550 = phi i64 [ %546, %548 ], [ %562, %549 ]
  %551 = phi i64 [ %492, %548 ], [ %563, %549 ]
  %552 = getelementptr inbounds i32, i32* %478, i64 %550
  %553 = bitcast i32* %552 to <4 x i32>*
  %554 = load <4 x i32>, <4 x i32>* %553, align 4, !tbaa !29, !alias.scope !50
  %555 = getelementptr i32, i32* %552, i64 4
  %556 = bitcast i32* %555 to <4 x i32>*
  %557 = load <4 x i32>, <4 x i32>* %556, align 4, !tbaa !29, !alias.scope !50
  %558 = getelementptr inbounds i32, i32* %12, i64 %550
  %559 = bitcast i32* %558 to <4 x i32>*
  store <4 x i32> %554, <4 x i32>* %559, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %560 = getelementptr i32, i32* %558, i64 4
  %561 = bitcast i32* %560 to <4 x i32>*
  store <4 x i32> %557, <4 x i32>* %561, align 4, !tbaa !29, !alias.scope !53, !noalias !50
  %562 = add i64 %550, 8
  %563 = add i64 %551, -1
  %564 = icmp eq i64 %563, 0
  br i1 %564, label %565, label %549, !llvm.loop !56

; <label>:565:                                    ; preds = %549, %545
  %566 = icmp eq i64 %488, %479
  br i1 %566, label %606, label %567

; <label>:567:                                    ; preds = %565, %481, %476
  %568 = phi i64 [ 0, %481 ], [ 0, %476 ], [ %488, %565 ]
  %569 = add nsw i64 %479, -1
  %570 = sub nsw i64 %569, %568
  %571 = and i64 %479, 3
  %572 = icmp eq i64 %571, 0
  br i1 %572, label %583, label %573

; <label>:573:                                    ; preds = %567
  br label %574

; <label>:574:                                    ; preds = %574, %573
  %575 = phi i64 [ %580, %574 ], [ %568, %573 ]
  %576 = phi i64 [ %581, %574 ], [ %571, %573 ]
  %577 = getelementptr inbounds i32, i32* %478, i64 %575
  %578 = load i32, i32* %577, align 4, !tbaa !29
  %579 = getelementptr inbounds i32, i32* %12, i64 %575
  store i32 %578, i32* %579, align 4, !tbaa !29
  %580 = add nuw nsw i64 %575, 1
  %581 = add i64 %576, -1
  %582 = icmp eq i64 %581, 0
  br i1 %582, label %583, label %574, !llvm.loop !57

; <label>:583:                                    ; preds = %574, %567
  %584 = phi i64 [ %568, %567 ], [ %580, %574 ]
  %585 = icmp ult i64 %570, 3
  br i1 %585, label %606, label %586

; <label>:586:                                    ; preds = %583
  br label %587

; <label>:587:                                    ; preds = %587, %586
  %588 = phi i64 [ %584, %586 ], [ %604, %587 ]
  %589 = getelementptr inbounds i32, i32* %478, i64 %588
  %590 = load i32, i32* %589, align 4, !tbaa !29
  %591 = getelementptr inbounds i32, i32* %12, i64 %588
  store i32 %590, i32* %591, align 4, !tbaa !29
  %592 = add nuw nsw i64 %588, 1
  %593 = getelementptr inbounds i32, i32* %478, i64 %592
  %594 = load i32, i32* %593, align 4, !tbaa !29
  %595 = getelementptr inbounds i32, i32* %12, i64 %592
  store i32 %594, i32* %595, align 4, !tbaa !29
  %596 = add nsw i64 %588, 2
  %597 = getelementptr inbounds i32, i32* %478, i64 %596
  %598 = load i32, i32* %597, align 4, !tbaa !29
  %599 = getelementptr inbounds i32, i32* %12, i64 %596
  store i32 %598, i32* %599, align 4, !tbaa !29
  %600 = add nsw i64 %588, 3
  %601 = getelementptr inbounds i32, i32* %478, i64 %600
  %602 = load i32, i32* %601, align 4, !tbaa !29
  %603 = getelementptr inbounds i32, i32* %12, i64 %600
  store i32 %602, i32* %603, align 4, !tbaa !29
  %604 = add nsw i64 %588, 4
  %605 = icmp eq i64 %604, %479
  br i1 %605, label %606, label %587, !llvm.loop !58

; <label>:606:                                    ; preds = %583, %587, %565, %473
  %607 = icmp ne i32* %2, null
  %608 = icmp ne i32* %3, null
  %609 = and i1 %607, %608
  %610 = icmp ne double* %4, null
  %611 = and i1 %609, %610
  br i1 %611, label %612, label %860

; <label>:612:                                    ; preds = %606
  %613 = icmp sgt i32 %29, 0
  br i1 %613, label %614, label %856

; <label>:614:                                    ; preds = %612
  %615 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 9
  %616 = load i32*, i32** %615, align 8, !tbaa !28
  %617 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 12
  %618 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 8
  %619 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 10
  %620 = zext i32 %29 to i64
  br label %621

; <label>:621:                                    ; preds = %853, %614
  %622 = phi i64 [ 0, %614 ], [ %626, %853 ]
  %623 = phi i32 [ 0, %614 ], [ %854, %853 ]
  %624 = getelementptr inbounds i32, i32* %616, i64 %622
  %625 = load i32, i32* %624, align 4, !tbaa !29
  %626 = add nuw nsw i64 %622, 1
  %627 = getelementptr inbounds i32, i32* %616, i64 %626
  %628 = load i32, i32* %627, align 4, !tbaa !29
  %629 = sub nsw i32 %628, %625
  %630 = icmp eq i32 %629, 1
  br i1 %630, label %631, label %638

; <label>:631:                                    ; preds = %621
  %632 = sext i32 %625 to i64
  %633 = getelementptr inbounds i32, i32* %2, i64 %632
  store i32 %623, i32* %633, align 4, !tbaa !29
  %634 = sext i32 %623 to i64
  %635 = getelementptr inbounds i32, i32* %3, i64 %634
  store i32 %625, i32* %635, align 4, !tbaa !29
  %636 = getelementptr inbounds double, double* %4, i64 %634
  store double 1.000000e+00, double* %636, align 8, !tbaa !16
  %637 = add nsw i32 %623, 1
  br label %853

; <label>:638:                                    ; preds = %621
  %639 = load i8**, i8*** %617, align 8, !tbaa !59
  %640 = getelementptr inbounds i8*, i8** %639, i64 %622
  %641 = bitcast i8** %640 to double**
  %642 = load double*, double** %641, align 8, !tbaa !60
  %643 = load i32*, i32** %618, align 8, !tbaa !61
  %644 = sext i32 %625 to i64
  %645 = getelementptr inbounds i32, i32* %643, i64 %644
  %646 = load i32*, i32** %619, align 8, !tbaa !62
  %647 = getelementptr inbounds i32, i32* %646, i64 %644
  %648 = icmp sgt i32 %629, 0
  br i1 %648, label %649, label %853

; <label>:649:                                    ; preds = %638
  %650 = zext i32 %629 to i64
  %651 = insertelement <2 x i32> undef, i32 %625, i32 0
  %652 = shufflevector <2 x i32> %651, <2 x i32> undef, <2 x i32> zeroinitializer
  %653 = insertelement <2 x i32> undef, i32 %625, i32 0
  %654 = shufflevector <2 x i32> %653, <2 x i32> undef, <2 x i32> zeroinitializer
  br label %655

; <label>:655:                                    ; preds = %849, %649
  %656 = phi i64 [ 0, %649 ], [ %851, %849 ]
  %657 = phi i32 [ %623, %649 ], [ %850, %849 ]
  %658 = add nsw i64 %656, %644
  %659 = getelementptr inbounds i32, i32* %2, i64 %658
  store i32 %657, i32* %659, align 4, !tbaa !29
  %660 = sext i32 %657 to i64
  %661 = getelementptr inbounds i32, i32* %3, i64 %660
  %662 = trunc i64 %658 to i32
  store i32 %662, i32* %661, align 4, !tbaa !29
  %663 = getelementptr inbounds double, double* %4, i64 %660
  store double 1.000000e+00, double* %663, align 8, !tbaa !16
  %664 = getelementptr inbounds i32, i32* %645, i64 %656
  %665 = load i32, i32* %664, align 4, !tbaa !29
  %666 = sext i32 %665 to i64
  %667 = getelementptr inbounds double, double* %642, i64 %666
  %668 = getelementptr inbounds i32, i32* %647, i64 %656
  %669 = load i32, i32* %668, align 4, !tbaa !29
  %670 = bitcast double* %667 to i32*
  %671 = sext i32 %669 to i64
  %672 = shl nsw i64 %671, 2
  %673 = add nsw i64 %672, 7
  %674 = lshr i64 %673, 3
  %675 = getelementptr inbounds double, double* %667, i64 %674
  %676 = add nsw i32 %657, 1
  %677 = icmp sgt i32 %669, 0
  br i1 %677, label %678, label %849

; <label>:678:                                    ; preds = %655
  %679 = sext i32 %676 to i64
  %680 = zext i32 %669 to i64
  %681 = zext i32 %669 to i64
  %682 = icmp ult i32 %669, 4
  br i1 %682, label %798, label %683

; <label>:683:                                    ; preds = %678
  %684 = getelementptr i32, i32* %3, i64 %679
  %685 = bitcast i32* %684 to i8*
  %686 = zext i32 %669 to i64
  %687 = add nsw i64 %679, %686
  %688 = getelementptr i32, i32* %3, i64 %687
  %689 = getelementptr double, double* %642, i64 %666
  %690 = bitcast double* %689 to i8*
  %691 = shl nuw nsw i64 %686, 2
  %692 = getelementptr i8, i8* %690, i64 %691
  %693 = getelementptr double, double* %4, i64 %679
  %694 = getelementptr double, double* %4, i64 %687
  %695 = add nsw i64 %674, %666
  %696 = add nsw i64 %695, %686
  %697 = getelementptr double, double* %642, i64 %696
  %698 = icmp ugt i8* %692, %685
  %699 = bitcast i32* %688 to double*
  %700 = icmp ult double* %667, %699
  %701 = and i1 %698, %700
  %702 = icmp ult double* %693, %697
  %703 = icmp ult double* %675, %694
  %704 = and i1 %702, %703
  %705 = or i1 %701, %704
  br i1 %705, label %798, label %706

; <label>:706:                                    ; preds = %683
  %707 = and i64 %681, 4294967292
  %708 = add nsw i64 %707, %679
  %709 = add nsw i64 %707, -4
  %710 = lshr exact i64 %709, 2
  %711 = add nuw nsw i64 %710, 1
  %712 = and i64 %711, 1
  %713 = icmp eq i64 %709, 0
  br i1 %713, label %769, label %714

; <label>:714:                                    ; preds = %706
  %715 = sub nsw i64 %711, %712
  br label %716

; <label>:716:                                    ; preds = %716, %714
  %717 = phi i64 [ 0, %714 ], [ %766, %716 ]
  %718 = phi i64 [ %715, %714 ], [ %767, %716 ]
  %719 = add i64 %717, %679
  %720 = getelementptr inbounds i32, i32* %670, i64 %717
  %721 = bitcast i32* %720 to <2 x i32>*
  %722 = load <2 x i32>, <2 x i32>* %721, align 4, !tbaa !29, !alias.scope !63
  %723 = getelementptr i32, i32* %720, i64 2
  %724 = bitcast i32* %723 to <2 x i32>*
  %725 = load <2 x i32>, <2 x i32>* %724, align 4, !tbaa !29, !alias.scope !63
  %726 = add nsw <2 x i32> %722, %652
  %727 = add nsw <2 x i32> %725, %654
  %728 = getelementptr inbounds i32, i32* %3, i64 %719
  %729 = bitcast i32* %728 to <2 x i32>*
  store <2 x i32> %726, <2 x i32>* %729, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %730 = getelementptr i32, i32* %728, i64 2
  %731 = bitcast i32* %730 to <2 x i32>*
  store <2 x i32> %727, <2 x i32>* %731, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %732 = getelementptr inbounds double, double* %675, i64 %717
  %733 = bitcast double* %732 to <2 x i64>*
  %734 = load <2 x i64>, <2 x i64>* %733, align 8, !tbaa !16, !alias.scope !68
  %735 = getelementptr double, double* %732, i64 2
  %736 = bitcast double* %735 to <2 x i64>*
  %737 = load <2 x i64>, <2 x i64>* %736, align 8, !tbaa !16, !alias.scope !68
  %738 = getelementptr inbounds double, double* %4, i64 %719
  %739 = bitcast double* %738 to <2 x i64>*
  store <2 x i64> %734, <2 x i64>* %739, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  %740 = getelementptr double, double* %738, i64 2
  %741 = bitcast double* %740 to <2 x i64>*
  store <2 x i64> %737, <2 x i64>* %741, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  %742 = or i64 %717, 4
  %743 = add i64 %742, %679
  %744 = getelementptr inbounds i32, i32* %670, i64 %742
  %745 = bitcast i32* %744 to <2 x i32>*
  %746 = load <2 x i32>, <2 x i32>* %745, align 4, !tbaa !29, !alias.scope !63
  %747 = getelementptr i32, i32* %744, i64 2
  %748 = bitcast i32* %747 to <2 x i32>*
  %749 = load <2 x i32>, <2 x i32>* %748, align 4, !tbaa !29, !alias.scope !63
  %750 = add nsw <2 x i32> %746, %652
  %751 = add nsw <2 x i32> %749, %654
  %752 = getelementptr inbounds i32, i32* %3, i64 %743
  %753 = bitcast i32* %752 to <2 x i32>*
  store <2 x i32> %750, <2 x i32>* %753, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %754 = getelementptr i32, i32* %752, i64 2
  %755 = bitcast i32* %754 to <2 x i32>*
  store <2 x i32> %751, <2 x i32>* %755, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %756 = getelementptr inbounds double, double* %675, i64 %742
  %757 = bitcast double* %756 to <2 x i64>*
  %758 = load <2 x i64>, <2 x i64>* %757, align 8, !tbaa !16, !alias.scope !68
  %759 = getelementptr double, double* %756, i64 2
  %760 = bitcast double* %759 to <2 x i64>*
  %761 = load <2 x i64>, <2 x i64>* %760, align 8, !tbaa !16, !alias.scope !68
  %762 = getelementptr inbounds double, double* %4, i64 %743
  %763 = bitcast double* %762 to <2 x i64>*
  store <2 x i64> %758, <2 x i64>* %763, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  %764 = getelementptr double, double* %762, i64 2
  %765 = bitcast double* %764 to <2 x i64>*
  store <2 x i64> %761, <2 x i64>* %765, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  %766 = add i64 %717, 8
  %767 = add i64 %718, -2
  %768 = icmp eq i64 %767, 0
  br i1 %768, label %769, label %716, !llvm.loop !72

; <label>:769:                                    ; preds = %716, %706
  %770 = phi i64 [ 0, %706 ], [ %766, %716 ]
  %771 = icmp eq i64 %712, 0
  br i1 %771, label %796, label %772

; <label>:772:                                    ; preds = %769
  %773 = add i64 %770, %679
  %774 = getelementptr inbounds i32, i32* %670, i64 %770
  %775 = bitcast i32* %774 to <2 x i32>*
  %776 = load <2 x i32>, <2 x i32>* %775, align 4, !tbaa !29, !alias.scope !63
  %777 = getelementptr i32, i32* %774, i64 2
  %778 = bitcast i32* %777 to <2 x i32>*
  %779 = load <2 x i32>, <2 x i32>* %778, align 4, !tbaa !29, !alias.scope !63
  %780 = add nsw <2 x i32> %776, %652
  %781 = add nsw <2 x i32> %779, %654
  %782 = getelementptr inbounds i32, i32* %3, i64 %773
  %783 = bitcast i32* %782 to <2 x i32>*
  store <2 x i32> %780, <2 x i32>* %783, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %784 = getelementptr i32, i32* %782, i64 2
  %785 = bitcast i32* %784 to <2 x i32>*
  store <2 x i32> %781, <2 x i32>* %785, align 4, !tbaa !29, !alias.scope !66, !noalias !63
  %786 = getelementptr inbounds double, double* %675, i64 %770
  %787 = bitcast double* %786 to <2 x i64>*
  %788 = load <2 x i64>, <2 x i64>* %787, align 8, !tbaa !16, !alias.scope !68
  %789 = getelementptr double, double* %786, i64 2
  %790 = bitcast double* %789 to <2 x i64>*
  %791 = load <2 x i64>, <2 x i64>* %790, align 8, !tbaa !16, !alias.scope !68
  %792 = getelementptr inbounds double, double* %4, i64 %773
  %793 = bitcast double* %792 to <2 x i64>*
  store <2 x i64> %788, <2 x i64>* %793, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  %794 = getelementptr double, double* %792, i64 2
  %795 = bitcast double* %794 to <2 x i64>*
  store <2 x i64> %791, <2 x i64>* %795, align 8, !tbaa !16, !alias.scope !70, !noalias !68
  br label %796

; <label>:796:                                    ; preds = %769, %772
  %797 = icmp eq i64 %707, %681
  br i1 %797, label %847, label %798

; <label>:798:                                    ; preds = %796, %683, %678
  %799 = phi i64 [ 0, %683 ], [ 0, %678 ], [ %707, %796 ]
  %800 = phi i64 [ %679, %683 ], [ %679, %678 ], [ %708, %796 ]
  %801 = add nsw i64 %680, -1
  %802 = and i64 %680, 1
  %803 = icmp eq i64 %802, 0
  br i1 %803, label %816, label %804

; <label>:804:                                    ; preds = %798
  %805 = getelementptr inbounds i32, i32* %670, i64 %799
  %806 = load i32, i32* %805, align 4, !tbaa !29
  %807 = add nsw i32 %806, %625
  %808 = getelementptr inbounds i32, i32* %3, i64 %800
  store i32 %807, i32* %808, align 4, !tbaa !29
  %809 = getelementptr inbounds double, double* %675, i64 %799
  %810 = bitcast double* %809 to i64*
  %811 = load i64, i64* %810, align 8, !tbaa !16
  %812 = getelementptr inbounds double, double* %4, i64 %800
  %813 = bitcast double* %812 to i64*
  store i64 %811, i64* %813, align 8, !tbaa !16
  %814 = or i64 %799, 1
  %815 = add nsw i64 %800, 1
  br label %816

; <label>:816:                                    ; preds = %804, %798
  %817 = phi i64 [ %814, %804 ], [ %799, %798 ]
  %818 = phi i64 [ %815, %804 ], [ %800, %798 ]
  %819 = icmp eq i64 %801, %799
  br i1 %819, label %847, label %820

; <label>:820:                                    ; preds = %816
  br label %821

; <label>:821:                                    ; preds = %821, %820
  %822 = phi i64 [ %817, %820 ], [ %844, %821 ]
  %823 = phi i64 [ %818, %820 ], [ %845, %821 ]
  %824 = getelementptr inbounds i32, i32* %670, i64 %822
  %825 = load i32, i32* %824, align 4, !tbaa !29
  %826 = add nsw i32 %825, %625
  %827 = getelementptr inbounds i32, i32* %3, i64 %823
  store i32 %826, i32* %827, align 4, !tbaa !29
  %828 = getelementptr inbounds double, double* %675, i64 %822
  %829 = bitcast double* %828 to i64*
  %830 = load i64, i64* %829, align 8, !tbaa !16
  %831 = getelementptr inbounds double, double* %4, i64 %823
  %832 = bitcast double* %831 to i64*
  store i64 %830, i64* %832, align 8, !tbaa !16
  %833 = add nuw nsw i64 %822, 1
  %834 = add nsw i64 %823, 1
  %835 = getelementptr inbounds i32, i32* %670, i64 %833
  %836 = load i32, i32* %835, align 4, !tbaa !29
  %837 = add nsw i32 %836, %625
  %838 = getelementptr inbounds i32, i32* %3, i64 %834
  store i32 %837, i32* %838, align 4, !tbaa !29
  %839 = getelementptr inbounds double, double* %675, i64 %833
  %840 = bitcast double* %839 to i64*
  %841 = load i64, i64* %840, align 8, !tbaa !16
  %842 = getelementptr inbounds double, double* %4, i64 %834
  %843 = bitcast double* %842 to i64*
  store i64 %841, i64* %843, align 8, !tbaa !16
  %844 = add nsw i64 %822, 2
  %845 = add nsw i64 %823, 2
  %846 = icmp eq i64 %844, %680
  br i1 %846, label %847, label %821, !llvm.loop !73

; <label>:847:                                    ; preds = %816, %821, %796
  %848 = add i32 %676, %669
  br label %849

; <label>:849:                                    ; preds = %847, %655
  %850 = phi i32 [ %676, %655 ], [ %848, %847 ]
  %851 = add nuw nsw i64 %656, 1
  %852 = icmp eq i64 %851, %650
  br i1 %852, label %853, label %655

; <label>:853:                                    ; preds = %849, %638, %631
  %854 = phi i32 [ %637, %631 ], [ %623, %638 ], [ %850, %849 ]
  %855 = icmp eq i64 %626, %620
  br i1 %855, label %856, label %621

; <label>:856:                                    ; preds = %853, %612
  %857 = phi i32 [ 0, %612 ], [ %854, %853 ]
  %858 = sext i32 %27 to i64
  %859 = getelementptr inbounds i32, i32* %2, i64 %858
  store i32 %857, i32* %859, align 4, !tbaa !29
  br label %860

; <label>:860:                                    ; preds = %856, %606
  %861 = icmp ne i32* %5, null
  %862 = icmp ne i32* %6, null
  %863 = and i1 %861, %862
  %864 = icmp ne double* %7, null
  %865 = and i1 %863, %864
  br i1 %865, label %866, label %1124

; <label>:866:                                    ; preds = %860
  %867 = icmp sgt i32 %29, 0
  br i1 %867, label %868, label %1120

; <label>:868:                                    ; preds = %866
  %869 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %1, i64 0, i32 9
  %870 = load i32*, i32** %869, align 8, !tbaa !28
  %871 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 14
  %872 = bitcast i8** %871 to double**
  %873 = load double*, double** %872, align 8, !tbaa !74
  %874 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 12
  %875 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 9
  %876 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 11
  %877 = zext i32 %29 to i64
  br label %878

; <label>:878:                                    ; preds = %1117, %868
  %879 = phi i64 [ 0, %868 ], [ %883, %1117 ]
  %880 = phi i32 [ 0, %868 ], [ %1118, %1117 ]
  %881 = getelementptr inbounds i32, i32* %870, i64 %879
  %882 = load i32, i32* %881, align 4, !tbaa !29
  %883 = add nuw nsw i64 %879, 1
  %884 = getelementptr inbounds i32, i32* %870, i64 %883
  %885 = load i32, i32* %884, align 4, !tbaa !29
  %886 = sub nsw i32 %885, %882
  %887 = sext i32 %882 to i64
  %888 = getelementptr inbounds double, double* %873, i64 %887
  %889 = icmp eq i32 %886, 1
  br i1 %889, label %890, label %899

; <label>:890:                                    ; preds = %878
  %891 = getelementptr inbounds i32, i32* %5, i64 %887
  store i32 %880, i32* %891, align 4, !tbaa !29
  %892 = sext i32 %880 to i64
  %893 = getelementptr inbounds i32, i32* %6, i64 %892
  store i32 %882, i32* %893, align 4, !tbaa !29
  %894 = bitcast double* %888 to i64*
  %895 = load i64, i64* %894, align 8, !tbaa !16
  %896 = getelementptr inbounds double, double* %7, i64 %892
  %897 = bitcast double* %896 to i64*
  store i64 %895, i64* %897, align 8, !tbaa !16
  %898 = add nsw i32 %880, 1
  br label %1117

; <label>:899:                                    ; preds = %878
  %900 = load i8**, i8*** %874, align 8, !tbaa !59
  %901 = getelementptr inbounds i8*, i8** %900, i64 %879
  %902 = bitcast i8** %901 to double**
  %903 = load double*, double** %902, align 8, !tbaa !60
  %904 = load i32*, i32** %875, align 8, !tbaa !75
  %905 = getelementptr inbounds i32, i32* %904, i64 %887
  %906 = load i32*, i32** %876, align 8, !tbaa !76
  %907 = getelementptr inbounds i32, i32* %906, i64 %887
  %908 = icmp sgt i32 %886, 0
  br i1 %908, label %909, label %1117

; <label>:909:                                    ; preds = %899
  %910 = zext i32 %886 to i64
  %911 = insertelement <2 x i32> undef, i32 %882, i32 0
  %912 = shufflevector <2 x i32> %911, <2 x i32> undef, <2 x i32> zeroinitializer
  %913 = insertelement <2 x i32> undef, i32 %882, i32 0
  %914 = shufflevector <2 x i32> %913, <2 x i32> undef, <2 x i32> zeroinitializer
  br label %915

; <label>:915:                                    ; preds = %1104, %909
  %916 = phi i64 [ 0, %909 ], [ %1115, %1104 ]
  %917 = phi i32 [ %880, %909 ], [ %1114, %1104 ]
  %918 = add nsw i64 %916, %887
  %919 = getelementptr inbounds i32, i32* %5, i64 %918
  store i32 %917, i32* %919, align 4, !tbaa !29
  %920 = getelementptr inbounds i32, i32* %905, i64 %916
  %921 = load i32, i32* %920, align 4, !tbaa !29
  %922 = sext i32 %921 to i64
  %923 = getelementptr inbounds double, double* %903, i64 %922
  %924 = getelementptr inbounds i32, i32* %907, i64 %916
  %925 = load i32, i32* %924, align 4, !tbaa !29
  %926 = bitcast double* %923 to i32*
  %927 = sext i32 %925 to i64
  %928 = shl nsw i64 %927, 2
  %929 = add nsw i64 %928, 7
  %930 = lshr i64 %929, 3
  %931 = getelementptr inbounds double, double* %923, i64 %930
  %932 = icmp sgt i32 %925, 0
  br i1 %932, label %933, label %1104

; <label>:933:                                    ; preds = %915
  %934 = sext i32 %917 to i64
  %935 = zext i32 %925 to i64
  %936 = zext i32 %925 to i64
  %937 = icmp ult i32 %925, 4
  br i1 %937, label %1053, label %938

; <label>:938:                                    ; preds = %933
  %939 = getelementptr i32, i32* %6, i64 %934
  %940 = bitcast i32* %939 to i8*
  %941 = zext i32 %925 to i64
  %942 = add nsw i64 %934, %941
  %943 = getelementptr i32, i32* %6, i64 %942
  %944 = getelementptr double, double* %903, i64 %922
  %945 = bitcast double* %944 to i8*
  %946 = shl nuw nsw i64 %941, 2
  %947 = getelementptr i8, i8* %945, i64 %946
  %948 = getelementptr double, double* %7, i64 %934
  %949 = getelementptr double, double* %7, i64 %942
  %950 = add nsw i64 %930, %922
  %951 = add nsw i64 %950, %941
  %952 = getelementptr double, double* %903, i64 %951
  %953 = icmp ugt i8* %947, %940
  %954 = bitcast i32* %943 to double*
  %955 = icmp ult double* %923, %954
  %956 = and i1 %953, %955
  %957 = icmp ult double* %948, %952
  %958 = icmp ult double* %931, %949
  %959 = and i1 %957, %958
  %960 = or i1 %956, %959
  br i1 %960, label %1053, label %961

; <label>:961:                                    ; preds = %938
  %962 = and i64 %936, 4294967292
  %963 = add nsw i64 %962, %934
  %964 = add nsw i64 %962, -4
  %965 = lshr exact i64 %964, 2
  %966 = add nuw nsw i64 %965, 1
  %967 = and i64 %966, 1
  %968 = icmp eq i64 %964, 0
  br i1 %968, label %1024, label %969

; <label>:969:                                    ; preds = %961
  %970 = sub nsw i64 %966, %967
  br label %971

; <label>:971:                                    ; preds = %971, %969
  %972 = phi i64 [ 0, %969 ], [ %1021, %971 ]
  %973 = phi i64 [ %970, %969 ], [ %1022, %971 ]
  %974 = add i64 %972, %934
  %975 = getelementptr inbounds i32, i32* %926, i64 %972
  %976 = bitcast i32* %975 to <2 x i32>*
  %977 = load <2 x i32>, <2 x i32>* %976, align 4, !tbaa !29, !alias.scope !77
  %978 = getelementptr i32, i32* %975, i64 2
  %979 = bitcast i32* %978 to <2 x i32>*
  %980 = load <2 x i32>, <2 x i32>* %979, align 4, !tbaa !29, !alias.scope !77
  %981 = add nsw <2 x i32> %977, %912
  %982 = add nsw <2 x i32> %980, %914
  %983 = getelementptr inbounds i32, i32* %6, i64 %974
  %984 = bitcast i32* %983 to <2 x i32>*
  store <2 x i32> %981, <2 x i32>* %984, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %985 = getelementptr i32, i32* %983, i64 2
  %986 = bitcast i32* %985 to <2 x i32>*
  store <2 x i32> %982, <2 x i32>* %986, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %987 = getelementptr inbounds double, double* %931, i64 %972
  %988 = bitcast double* %987 to <2 x i64>*
  %989 = load <2 x i64>, <2 x i64>* %988, align 8, !tbaa !16, !alias.scope !82
  %990 = getelementptr double, double* %987, i64 2
  %991 = bitcast double* %990 to <2 x i64>*
  %992 = load <2 x i64>, <2 x i64>* %991, align 8, !tbaa !16, !alias.scope !82
  %993 = getelementptr inbounds double, double* %7, i64 %974
  %994 = bitcast double* %993 to <2 x i64>*
  store <2 x i64> %989, <2 x i64>* %994, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  %995 = getelementptr double, double* %993, i64 2
  %996 = bitcast double* %995 to <2 x i64>*
  store <2 x i64> %992, <2 x i64>* %996, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  %997 = or i64 %972, 4
  %998 = add i64 %997, %934
  %999 = getelementptr inbounds i32, i32* %926, i64 %997
  %1000 = bitcast i32* %999 to <2 x i32>*
  %1001 = load <2 x i32>, <2 x i32>* %1000, align 4, !tbaa !29, !alias.scope !77
  %1002 = getelementptr i32, i32* %999, i64 2
  %1003 = bitcast i32* %1002 to <2 x i32>*
  %1004 = load <2 x i32>, <2 x i32>* %1003, align 4, !tbaa !29, !alias.scope !77
  %1005 = add nsw <2 x i32> %1001, %912
  %1006 = add nsw <2 x i32> %1004, %914
  %1007 = getelementptr inbounds i32, i32* %6, i64 %998
  %1008 = bitcast i32* %1007 to <2 x i32>*
  store <2 x i32> %1005, <2 x i32>* %1008, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %1009 = getelementptr i32, i32* %1007, i64 2
  %1010 = bitcast i32* %1009 to <2 x i32>*
  store <2 x i32> %1006, <2 x i32>* %1010, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %1011 = getelementptr inbounds double, double* %931, i64 %997
  %1012 = bitcast double* %1011 to <2 x i64>*
  %1013 = load <2 x i64>, <2 x i64>* %1012, align 8, !tbaa !16, !alias.scope !82
  %1014 = getelementptr double, double* %1011, i64 2
  %1015 = bitcast double* %1014 to <2 x i64>*
  %1016 = load <2 x i64>, <2 x i64>* %1015, align 8, !tbaa !16, !alias.scope !82
  %1017 = getelementptr inbounds double, double* %7, i64 %998
  %1018 = bitcast double* %1017 to <2 x i64>*
  store <2 x i64> %1013, <2 x i64>* %1018, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  %1019 = getelementptr double, double* %1017, i64 2
  %1020 = bitcast double* %1019 to <2 x i64>*
  store <2 x i64> %1016, <2 x i64>* %1020, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  %1021 = add i64 %972, 8
  %1022 = add i64 %973, -2
  %1023 = icmp eq i64 %1022, 0
  br i1 %1023, label %1024, label %971, !llvm.loop !86

; <label>:1024:                                   ; preds = %971, %961
  %1025 = phi i64 [ 0, %961 ], [ %1021, %971 ]
  %1026 = icmp eq i64 %967, 0
  br i1 %1026, label %1051, label %1027

; <label>:1027:                                   ; preds = %1024
  %1028 = add i64 %1025, %934
  %1029 = getelementptr inbounds i32, i32* %926, i64 %1025
  %1030 = bitcast i32* %1029 to <2 x i32>*
  %1031 = load <2 x i32>, <2 x i32>* %1030, align 4, !tbaa !29, !alias.scope !77
  %1032 = getelementptr i32, i32* %1029, i64 2
  %1033 = bitcast i32* %1032 to <2 x i32>*
  %1034 = load <2 x i32>, <2 x i32>* %1033, align 4, !tbaa !29, !alias.scope !77
  %1035 = add nsw <2 x i32> %1031, %912
  %1036 = add nsw <2 x i32> %1034, %914
  %1037 = getelementptr inbounds i32, i32* %6, i64 %1028
  %1038 = bitcast i32* %1037 to <2 x i32>*
  store <2 x i32> %1035, <2 x i32>* %1038, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %1039 = getelementptr i32, i32* %1037, i64 2
  %1040 = bitcast i32* %1039 to <2 x i32>*
  store <2 x i32> %1036, <2 x i32>* %1040, align 4, !tbaa !29, !alias.scope !80, !noalias !77
  %1041 = getelementptr inbounds double, double* %931, i64 %1025
  %1042 = bitcast double* %1041 to <2 x i64>*
  %1043 = load <2 x i64>, <2 x i64>* %1042, align 8, !tbaa !16, !alias.scope !82
  %1044 = getelementptr double, double* %1041, i64 2
  %1045 = bitcast double* %1044 to <2 x i64>*
  %1046 = load <2 x i64>, <2 x i64>* %1045, align 8, !tbaa !16, !alias.scope !82
  %1047 = getelementptr inbounds double, double* %7, i64 %1028
  %1048 = bitcast double* %1047 to <2 x i64>*
  store <2 x i64> %1043, <2 x i64>* %1048, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  %1049 = getelementptr double, double* %1047, i64 2
  %1050 = bitcast double* %1049 to <2 x i64>*
  store <2 x i64> %1046, <2 x i64>* %1050, align 8, !tbaa !16, !alias.scope !84, !noalias !82
  br label %1051

; <label>:1051:                                   ; preds = %1024, %1027
  %1052 = icmp eq i64 %962, %936
  br i1 %1052, label %1102, label %1053

; <label>:1053:                                   ; preds = %1051, %938, %933
  %1054 = phi i64 [ %934, %938 ], [ %934, %933 ], [ %963, %1051 ]
  %1055 = phi i64 [ 0, %938 ], [ 0, %933 ], [ %962, %1051 ]
  %1056 = add nsw i64 %935, -1
  %1057 = and i64 %935, 1
  %1058 = icmp eq i64 %1057, 0
  br i1 %1058, label %1071, label %1059

; <label>:1059:                                   ; preds = %1053
  %1060 = getelementptr inbounds i32, i32* %926, i64 %1055
  %1061 = load i32, i32* %1060, align 4, !tbaa !29
  %1062 = add nsw i32 %1061, %882
  %1063 = getelementptr inbounds i32, i32* %6, i64 %1054
  store i32 %1062, i32* %1063, align 4, !tbaa !29
  %1064 = getelementptr inbounds double, double* %931, i64 %1055
  %1065 = bitcast double* %1064 to i64*
  %1066 = load i64, i64* %1065, align 8, !tbaa !16
  %1067 = getelementptr inbounds double, double* %7, i64 %1054
  %1068 = bitcast double* %1067 to i64*
  store i64 %1066, i64* %1068, align 8, !tbaa !16
  %1069 = add nsw i64 %1054, 1
  %1070 = or i64 %1055, 1
  br label %1071

; <label>:1071:                                   ; preds = %1059, %1053
  %1072 = phi i64 [ %1069, %1059 ], [ %1054, %1053 ]
  %1073 = phi i64 [ %1070, %1059 ], [ %1055, %1053 ]
  %1074 = icmp eq i64 %1056, %1055
  br i1 %1074, label %1102, label %1075

; <label>:1075:                                   ; preds = %1071
  br label %1076

; <label>:1076:                                   ; preds = %1076, %1075
  %1077 = phi i64 [ %1072, %1075 ], [ %1099, %1076 ]
  %1078 = phi i64 [ %1073, %1075 ], [ %1100, %1076 ]
  %1079 = getelementptr inbounds i32, i32* %926, i64 %1078
  %1080 = load i32, i32* %1079, align 4, !tbaa !29
  %1081 = add nsw i32 %1080, %882
  %1082 = getelementptr inbounds i32, i32* %6, i64 %1077
  store i32 %1081, i32* %1082, align 4, !tbaa !29
  %1083 = getelementptr inbounds double, double* %931, i64 %1078
  %1084 = bitcast double* %1083 to i64*
  %1085 = load i64, i64* %1084, align 8, !tbaa !16
  %1086 = getelementptr inbounds double, double* %7, i64 %1077
  %1087 = bitcast double* %1086 to i64*
  store i64 %1085, i64* %1087, align 8, !tbaa !16
  %1088 = add nsw i64 %1077, 1
  %1089 = add nuw nsw i64 %1078, 1
  %1090 = getelementptr inbounds i32, i32* %926, i64 %1089
  %1091 = load i32, i32* %1090, align 4, !tbaa !29
  %1092 = add nsw i32 %1091, %882
  %1093 = getelementptr inbounds i32, i32* %6, i64 %1088
  store i32 %1092, i32* %1093, align 4, !tbaa !29
  %1094 = getelementptr inbounds double, double* %931, i64 %1089
  %1095 = bitcast double* %1094 to i64*
  %1096 = load i64, i64* %1095, align 8, !tbaa !16
  %1097 = getelementptr inbounds double, double* %7, i64 %1088
  %1098 = bitcast double* %1097 to i64*
  store i64 %1096, i64* %1098, align 8, !tbaa !16
  %1099 = add nsw i64 %1077, 2
  %1100 = add nsw i64 %1078, 2
  %1101 = icmp eq i64 %1100, %935
  br i1 %1101, label %1102, label %1076, !llvm.loop !87

; <label>:1102:                                   ; preds = %1071, %1076, %1051
  %1103 = add i32 %917, %925
  br label %1104

; <label>:1104:                                   ; preds = %1102, %915
  %1105 = phi i32 [ %917, %915 ], [ %1103, %1102 ]
  %1106 = sext i32 %1105 to i64
  %1107 = getelementptr inbounds i32, i32* %6, i64 %1106
  %1108 = trunc i64 %918 to i32
  store i32 %1108, i32* %1107, align 4, !tbaa !29
  %1109 = getelementptr inbounds double, double* %888, i64 %916
  %1110 = bitcast double* %1109 to i64*
  %1111 = load i64, i64* %1110, align 8, !tbaa !16
  %1112 = getelementptr inbounds double, double* %7, i64 %1106
  %1113 = bitcast double* %1112 to i64*
  store i64 %1111, i64* %1113, align 8, !tbaa !16
  %1114 = add nsw i32 %1105, 1
  %1115 = add nuw nsw i64 %916, 1
  %1116 = icmp eq i64 %1115, %910
  br i1 %1116, label %1117, label %915

; <label>:1117:                                   ; preds = %1104, %899, %890
  %1118 = phi i32 [ %898, %890 ], [ %880, %899 ], [ %1114, %1104 ]
  %1119 = icmp eq i64 %883, %877
  br i1 %1119, label %1120, label %878

; <label>:1120:                                   ; preds = %1117, %866
  %1121 = phi i32 [ 0, %866 ], [ %1118, %1117 ]
  %1122 = sext i32 %27 to i64
  %1123 = getelementptr inbounds i32, i32* %5, i64 %1122
  store i32 %1121, i32* %1123, align 4, !tbaa !29
  br label %1124

; <label>:1124:                                   ; preds = %1120, %860
  %1125 = icmp ne i32* %8, null
  %1126 = icmp ne i32* %9, null
  %1127 = and i1 %1125, %1126
  %1128 = icmp ne double* %10, null
  %1129 = and i1 %1127, %1128
  br i1 %1129, label %1130, label %1565

; <label>:1130:                                   ; preds = %1124
  %1131 = icmp slt i32 %27, 0
  br i1 %1131, label %1263, label %1132

; <label>:1132:                                   ; preds = %1130
  %1133 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 20
  %1134 = load i32*, i32** %1133, align 8, !tbaa !88
  %1135 = add i32 %27, 1
  %1136 = zext i32 %1135 to i64
  %1137 = icmp ult i32 %1135, 8
  br i1 %1137, label %1224, label %1138

; <label>:1138:                                   ; preds = %1132
  %1139 = getelementptr i32, i32* %8, i64 %1136
  %1140 = getelementptr i32, i32* %1134, i64 %1136
  %1141 = icmp ugt i32* %1140, %8
  %1142 = icmp ult i32* %1134, %1139
  %1143 = and i1 %1141, %1142
  br i1 %1143, label %1224, label %1144

; <label>:1144:                                   ; preds = %1138
  %1145 = and i64 %1136, 4294967288
  %1146 = add nsw i64 %1145, -8
  %1147 = lshr exact i64 %1146, 3
  %1148 = add nuw nsw i64 %1147, 1
  %1149 = and i64 %1148, 3
  %1150 = icmp ult i64 %1146, 24
  br i1 %1150, label %1202, label %1151

; <label>:1151:                                   ; preds = %1144
  %1152 = sub nsw i64 %1148, %1149
  br label %1153

; <label>:1153:                                   ; preds = %1153, %1151
  %1154 = phi i64 [ 0, %1151 ], [ %1199, %1153 ]
  %1155 = phi i64 [ %1152, %1151 ], [ %1200, %1153 ]
  %1156 = getelementptr inbounds i32, i32* %1134, i64 %1154
  %1157 = bitcast i32* %1156 to <4 x i32>*
  %1158 = load <4 x i32>, <4 x i32>* %1157, align 4, !tbaa !29, !alias.scope !89
  %1159 = getelementptr i32, i32* %1156, i64 4
  %1160 = bitcast i32* %1159 to <4 x i32>*
  %1161 = load <4 x i32>, <4 x i32>* %1160, align 4, !tbaa !29, !alias.scope !89
  %1162 = getelementptr inbounds i32, i32* %8, i64 %1154
  %1163 = bitcast i32* %1162 to <4 x i32>*
  store <4 x i32> %1158, <4 x i32>* %1163, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1164 = getelementptr i32, i32* %1162, i64 4
  %1165 = bitcast i32* %1164 to <4 x i32>*
  store <4 x i32> %1161, <4 x i32>* %1165, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1166 = or i64 %1154, 8
  %1167 = getelementptr inbounds i32, i32* %1134, i64 %1166
  %1168 = bitcast i32* %1167 to <4 x i32>*
  %1169 = load <4 x i32>, <4 x i32>* %1168, align 4, !tbaa !29, !alias.scope !89
  %1170 = getelementptr i32, i32* %1167, i64 4
  %1171 = bitcast i32* %1170 to <4 x i32>*
  %1172 = load <4 x i32>, <4 x i32>* %1171, align 4, !tbaa !29, !alias.scope !89
  %1173 = getelementptr inbounds i32, i32* %8, i64 %1166
  %1174 = bitcast i32* %1173 to <4 x i32>*
  store <4 x i32> %1169, <4 x i32>* %1174, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1175 = getelementptr i32, i32* %1173, i64 4
  %1176 = bitcast i32* %1175 to <4 x i32>*
  store <4 x i32> %1172, <4 x i32>* %1176, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1177 = or i64 %1154, 16
  %1178 = getelementptr inbounds i32, i32* %1134, i64 %1177
  %1179 = bitcast i32* %1178 to <4 x i32>*
  %1180 = load <4 x i32>, <4 x i32>* %1179, align 4, !tbaa !29, !alias.scope !89
  %1181 = getelementptr i32, i32* %1178, i64 4
  %1182 = bitcast i32* %1181 to <4 x i32>*
  %1183 = load <4 x i32>, <4 x i32>* %1182, align 4, !tbaa !29, !alias.scope !89
  %1184 = getelementptr inbounds i32, i32* %8, i64 %1177
  %1185 = bitcast i32* %1184 to <4 x i32>*
  store <4 x i32> %1180, <4 x i32>* %1185, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1186 = getelementptr i32, i32* %1184, i64 4
  %1187 = bitcast i32* %1186 to <4 x i32>*
  store <4 x i32> %1183, <4 x i32>* %1187, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1188 = or i64 %1154, 24
  %1189 = getelementptr inbounds i32, i32* %1134, i64 %1188
  %1190 = bitcast i32* %1189 to <4 x i32>*
  %1191 = load <4 x i32>, <4 x i32>* %1190, align 4, !tbaa !29, !alias.scope !89
  %1192 = getelementptr i32, i32* %1189, i64 4
  %1193 = bitcast i32* %1192 to <4 x i32>*
  %1194 = load <4 x i32>, <4 x i32>* %1193, align 4, !tbaa !29, !alias.scope !89
  %1195 = getelementptr inbounds i32, i32* %8, i64 %1188
  %1196 = bitcast i32* %1195 to <4 x i32>*
  store <4 x i32> %1191, <4 x i32>* %1196, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1197 = getelementptr i32, i32* %1195, i64 4
  %1198 = bitcast i32* %1197 to <4 x i32>*
  store <4 x i32> %1194, <4 x i32>* %1198, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1199 = add i64 %1154, 32
  %1200 = add i64 %1155, -4
  %1201 = icmp eq i64 %1200, 0
  br i1 %1201, label %1202, label %1153, !llvm.loop !94

; <label>:1202:                                   ; preds = %1153, %1144
  %1203 = phi i64 [ 0, %1144 ], [ %1199, %1153 ]
  %1204 = icmp eq i64 %1149, 0
  br i1 %1204, label %1222, label %1205

; <label>:1205:                                   ; preds = %1202
  br label %1206

; <label>:1206:                                   ; preds = %1206, %1205
  %1207 = phi i64 [ %1203, %1205 ], [ %1219, %1206 ]
  %1208 = phi i64 [ %1149, %1205 ], [ %1220, %1206 ]
  %1209 = getelementptr inbounds i32, i32* %1134, i64 %1207
  %1210 = bitcast i32* %1209 to <4 x i32>*
  %1211 = load <4 x i32>, <4 x i32>* %1210, align 4, !tbaa !29, !alias.scope !89
  %1212 = getelementptr i32, i32* %1209, i64 4
  %1213 = bitcast i32* %1212 to <4 x i32>*
  %1214 = load <4 x i32>, <4 x i32>* %1213, align 4, !tbaa !29, !alias.scope !89
  %1215 = getelementptr inbounds i32, i32* %8, i64 %1207
  %1216 = bitcast i32* %1215 to <4 x i32>*
  store <4 x i32> %1211, <4 x i32>* %1216, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1217 = getelementptr i32, i32* %1215, i64 4
  %1218 = bitcast i32* %1217 to <4 x i32>*
  store <4 x i32> %1214, <4 x i32>* %1218, align 4, !tbaa !29, !alias.scope !92, !noalias !89
  %1219 = add i64 %1207, 8
  %1220 = add i64 %1208, -1
  %1221 = icmp eq i64 %1220, 0
  br i1 %1221, label %1222, label %1206, !llvm.loop !95

; <label>:1222:                                   ; preds = %1206, %1202
  %1223 = icmp eq i64 %1145, %1136
  br i1 %1223, label %1263, label %1224

; <label>:1224:                                   ; preds = %1222, %1138, %1132
  %1225 = phi i64 [ 0, %1138 ], [ 0, %1132 ], [ %1145, %1222 ]
  %1226 = add nsw i64 %1136, -1
  %1227 = sub nsw i64 %1226, %1225
  %1228 = and i64 %1136, 3
  %1229 = icmp eq i64 %1228, 0
  br i1 %1229, label %1240, label %1230

; <label>:1230:                                   ; preds = %1224
  br label %1231

; <label>:1231:                                   ; preds = %1231, %1230
  %1232 = phi i64 [ %1237, %1231 ], [ %1225, %1230 ]
  %1233 = phi i64 [ %1238, %1231 ], [ %1228, %1230 ]
  %1234 = getelementptr inbounds i32, i32* %1134, i64 %1232
  %1235 = load i32, i32* %1234, align 4, !tbaa !29
  %1236 = getelementptr inbounds i32, i32* %8, i64 %1232
  store i32 %1235, i32* %1236, align 4, !tbaa !29
  %1237 = add nuw nsw i64 %1232, 1
  %1238 = add i64 %1233, -1
  %1239 = icmp eq i64 %1238, 0
  br i1 %1239, label %1240, label %1231, !llvm.loop !96

; <label>:1240:                                   ; preds = %1231, %1224
  %1241 = phi i64 [ %1225, %1224 ], [ %1237, %1231 ]
  %1242 = icmp ult i64 %1227, 3
  br i1 %1242, label %1263, label %1243

; <label>:1243:                                   ; preds = %1240
  br label %1244

; <label>:1244:                                   ; preds = %1244, %1243
  %1245 = phi i64 [ %1241, %1243 ], [ %1261, %1244 ]
  %1246 = getelementptr inbounds i32, i32* %1134, i64 %1245
  %1247 = load i32, i32* %1246, align 4, !tbaa !29
  %1248 = getelementptr inbounds i32, i32* %8, i64 %1245
  store i32 %1247, i32* %1248, align 4, !tbaa !29
  %1249 = add nuw nsw i64 %1245, 1
  %1250 = getelementptr inbounds i32, i32* %1134, i64 %1249
  %1251 = load i32, i32* %1250, align 4, !tbaa !29
  %1252 = getelementptr inbounds i32, i32* %8, i64 %1249
  store i32 %1251, i32* %1252, align 4, !tbaa !29
  %1253 = add nsw i64 %1245, 2
  %1254 = getelementptr inbounds i32, i32* %1134, i64 %1253
  %1255 = load i32, i32* %1254, align 4, !tbaa !29
  %1256 = getelementptr inbounds i32, i32* %8, i64 %1253
  store i32 %1255, i32* %1256, align 4, !tbaa !29
  %1257 = add nsw i64 %1245, 3
  %1258 = getelementptr inbounds i32, i32* %1134, i64 %1257
  %1259 = load i32, i32* %1258, align 4, !tbaa !29
  %1260 = getelementptr inbounds i32, i32* %8, i64 %1257
  store i32 %1259, i32* %1260, align 4, !tbaa !29
  %1261 = add nsw i64 %1245, 4
  %1262 = icmp eq i64 %1261, %1136
  br i1 %1262, label %1263, label %1244, !llvm.loop !97

; <label>:1263:                                   ; preds = %1240, %1244, %1222, %1130
  %1264 = sext i32 %27 to i64
  %1265 = getelementptr inbounds i32, i32* %8, i64 %1264
  %1266 = load i32, i32* %1265, align 4, !tbaa !29
  %1267 = icmp sgt i32 %1266, 0
  br i1 %1267, label %1268, label %1565

; <label>:1268:                                   ; preds = %1263
  %1269 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 21
  %1270 = load i32*, i32** %1269, align 8, !tbaa !98
  %1271 = zext i32 %1266 to i64
  %1272 = icmp ult i32 %1266, 8
  br i1 %1272, label %1359, label %1273

; <label>:1273:                                   ; preds = %1268
  %1274 = getelementptr i32, i32* %9, i64 %1271
  %1275 = getelementptr i32, i32* %1270, i64 %1271
  %1276 = icmp ugt i32* %1275, %9
  %1277 = icmp ult i32* %1270, %1274
  %1278 = and i1 %1276, %1277
  br i1 %1278, label %1359, label %1279

; <label>:1279:                                   ; preds = %1273
  %1280 = and i64 %1271, 4294967288
  %1281 = add nsw i64 %1280, -8
  %1282 = lshr exact i64 %1281, 3
  %1283 = add nuw nsw i64 %1282, 1
  %1284 = and i64 %1283, 3
  %1285 = icmp ult i64 %1281, 24
  br i1 %1285, label %1337, label %1286

; <label>:1286:                                   ; preds = %1279
  %1287 = sub nsw i64 %1283, %1284
  br label %1288

; <label>:1288:                                   ; preds = %1288, %1286
  %1289 = phi i64 [ 0, %1286 ], [ %1334, %1288 ]
  %1290 = phi i64 [ %1287, %1286 ], [ %1335, %1288 ]
  %1291 = getelementptr inbounds i32, i32* %1270, i64 %1289
  %1292 = bitcast i32* %1291 to <4 x i32>*
  %1293 = load <4 x i32>, <4 x i32>* %1292, align 4, !tbaa !29, !alias.scope !99
  %1294 = getelementptr i32, i32* %1291, i64 4
  %1295 = bitcast i32* %1294 to <4 x i32>*
  %1296 = load <4 x i32>, <4 x i32>* %1295, align 4, !tbaa !29, !alias.scope !99
  %1297 = getelementptr inbounds i32, i32* %9, i64 %1289
  %1298 = bitcast i32* %1297 to <4 x i32>*
  store <4 x i32> %1293, <4 x i32>* %1298, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1299 = getelementptr i32, i32* %1297, i64 4
  %1300 = bitcast i32* %1299 to <4 x i32>*
  store <4 x i32> %1296, <4 x i32>* %1300, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1301 = or i64 %1289, 8
  %1302 = getelementptr inbounds i32, i32* %1270, i64 %1301
  %1303 = bitcast i32* %1302 to <4 x i32>*
  %1304 = load <4 x i32>, <4 x i32>* %1303, align 4, !tbaa !29, !alias.scope !99
  %1305 = getelementptr i32, i32* %1302, i64 4
  %1306 = bitcast i32* %1305 to <4 x i32>*
  %1307 = load <4 x i32>, <4 x i32>* %1306, align 4, !tbaa !29, !alias.scope !99
  %1308 = getelementptr inbounds i32, i32* %9, i64 %1301
  %1309 = bitcast i32* %1308 to <4 x i32>*
  store <4 x i32> %1304, <4 x i32>* %1309, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1310 = getelementptr i32, i32* %1308, i64 4
  %1311 = bitcast i32* %1310 to <4 x i32>*
  store <4 x i32> %1307, <4 x i32>* %1311, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1312 = or i64 %1289, 16
  %1313 = getelementptr inbounds i32, i32* %1270, i64 %1312
  %1314 = bitcast i32* %1313 to <4 x i32>*
  %1315 = load <4 x i32>, <4 x i32>* %1314, align 4, !tbaa !29, !alias.scope !99
  %1316 = getelementptr i32, i32* %1313, i64 4
  %1317 = bitcast i32* %1316 to <4 x i32>*
  %1318 = load <4 x i32>, <4 x i32>* %1317, align 4, !tbaa !29, !alias.scope !99
  %1319 = getelementptr inbounds i32, i32* %9, i64 %1312
  %1320 = bitcast i32* %1319 to <4 x i32>*
  store <4 x i32> %1315, <4 x i32>* %1320, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1321 = getelementptr i32, i32* %1319, i64 4
  %1322 = bitcast i32* %1321 to <4 x i32>*
  store <4 x i32> %1318, <4 x i32>* %1322, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1323 = or i64 %1289, 24
  %1324 = getelementptr inbounds i32, i32* %1270, i64 %1323
  %1325 = bitcast i32* %1324 to <4 x i32>*
  %1326 = load <4 x i32>, <4 x i32>* %1325, align 4, !tbaa !29, !alias.scope !99
  %1327 = getelementptr i32, i32* %1324, i64 4
  %1328 = bitcast i32* %1327 to <4 x i32>*
  %1329 = load <4 x i32>, <4 x i32>* %1328, align 4, !tbaa !29, !alias.scope !99
  %1330 = getelementptr inbounds i32, i32* %9, i64 %1323
  %1331 = bitcast i32* %1330 to <4 x i32>*
  store <4 x i32> %1326, <4 x i32>* %1331, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1332 = getelementptr i32, i32* %1330, i64 4
  %1333 = bitcast i32* %1332 to <4 x i32>*
  store <4 x i32> %1329, <4 x i32>* %1333, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1334 = add i64 %1289, 32
  %1335 = add i64 %1290, -4
  %1336 = icmp eq i64 %1335, 0
  br i1 %1336, label %1337, label %1288, !llvm.loop !104

; <label>:1337:                                   ; preds = %1288, %1279
  %1338 = phi i64 [ 0, %1279 ], [ %1334, %1288 ]
  %1339 = icmp eq i64 %1284, 0
  br i1 %1339, label %1357, label %1340

; <label>:1340:                                   ; preds = %1337
  br label %1341

; <label>:1341:                                   ; preds = %1341, %1340
  %1342 = phi i64 [ %1338, %1340 ], [ %1354, %1341 ]
  %1343 = phi i64 [ %1284, %1340 ], [ %1355, %1341 ]
  %1344 = getelementptr inbounds i32, i32* %1270, i64 %1342
  %1345 = bitcast i32* %1344 to <4 x i32>*
  %1346 = load <4 x i32>, <4 x i32>* %1345, align 4, !tbaa !29, !alias.scope !99
  %1347 = getelementptr i32, i32* %1344, i64 4
  %1348 = bitcast i32* %1347 to <4 x i32>*
  %1349 = load <4 x i32>, <4 x i32>* %1348, align 4, !tbaa !29, !alias.scope !99
  %1350 = getelementptr inbounds i32, i32* %9, i64 %1342
  %1351 = bitcast i32* %1350 to <4 x i32>*
  store <4 x i32> %1346, <4 x i32>* %1351, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1352 = getelementptr i32, i32* %1350, i64 4
  %1353 = bitcast i32* %1352 to <4 x i32>*
  store <4 x i32> %1349, <4 x i32>* %1353, align 4, !tbaa !29, !alias.scope !102, !noalias !99
  %1354 = add i64 %1342, 8
  %1355 = add i64 %1343, -1
  %1356 = icmp eq i64 %1355, 0
  br i1 %1356, label %1357, label %1341, !llvm.loop !105

; <label>:1357:                                   ; preds = %1341, %1337
  %1358 = icmp eq i64 %1280, %1271
  br i1 %1358, label %1398, label %1359

; <label>:1359:                                   ; preds = %1357, %1273, %1268
  %1360 = phi i64 [ 0, %1273 ], [ 0, %1268 ], [ %1280, %1357 ]
  %1361 = add nsw i64 %1271, -1
  %1362 = sub nsw i64 %1361, %1360
  %1363 = and i64 %1271, 3
  %1364 = icmp eq i64 %1363, 0
  br i1 %1364, label %1375, label %1365

; <label>:1365:                                   ; preds = %1359
  br label %1366

; <label>:1366:                                   ; preds = %1366, %1365
  %1367 = phi i64 [ %1372, %1366 ], [ %1360, %1365 ]
  %1368 = phi i64 [ %1373, %1366 ], [ %1363, %1365 ]
  %1369 = getelementptr inbounds i32, i32* %1270, i64 %1367
  %1370 = load i32, i32* %1369, align 4, !tbaa !29
  %1371 = getelementptr inbounds i32, i32* %9, i64 %1367
  store i32 %1370, i32* %1371, align 4, !tbaa !29
  %1372 = add nuw nsw i64 %1367, 1
  %1373 = add i64 %1368, -1
  %1374 = icmp eq i64 %1373, 0
  br i1 %1374, label %1375, label %1366, !llvm.loop !106

; <label>:1375:                                   ; preds = %1366, %1359
  %1376 = phi i64 [ %1360, %1359 ], [ %1372, %1366 ]
  %1377 = icmp ult i64 %1362, 3
  br i1 %1377, label %1398, label %1378

; <label>:1378:                                   ; preds = %1375
  br label %1379

; <label>:1379:                                   ; preds = %1379, %1378
  %1380 = phi i64 [ %1376, %1378 ], [ %1396, %1379 ]
  %1381 = getelementptr inbounds i32, i32* %1270, i64 %1380
  %1382 = load i32, i32* %1381, align 4, !tbaa !29
  %1383 = getelementptr inbounds i32, i32* %9, i64 %1380
  store i32 %1382, i32* %1383, align 4, !tbaa !29
  %1384 = add nuw nsw i64 %1380, 1
  %1385 = getelementptr inbounds i32, i32* %1270, i64 %1384
  %1386 = load i32, i32* %1385, align 4, !tbaa !29
  %1387 = getelementptr inbounds i32, i32* %9, i64 %1384
  store i32 %1386, i32* %1387, align 4, !tbaa !29
  %1388 = add nsw i64 %1380, 2
  %1389 = getelementptr inbounds i32, i32* %1270, i64 %1388
  %1390 = load i32, i32* %1389, align 4, !tbaa !29
  %1391 = getelementptr inbounds i32, i32* %9, i64 %1388
  store i32 %1390, i32* %1391, align 4, !tbaa !29
  %1392 = add nsw i64 %1380, 3
  %1393 = getelementptr inbounds i32, i32* %1270, i64 %1392
  %1394 = load i32, i32* %1393, align 4, !tbaa !29
  %1395 = getelementptr inbounds i32, i32* %9, i64 %1392
  store i32 %1394, i32* %1395, align 4, !tbaa !29
  %1396 = add nsw i64 %1380, 4
  %1397 = icmp eq i64 %1396, %1271
  br i1 %1397, label %1398, label %1379, !llvm.loop !107

; <label>:1398:                                   ; preds = %1375, %1379, %1357
  br i1 %1267, label %1399, label %1565

; <label>:1399:                                   ; preds = %1398
  %1400 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %0, i64 0, i32 22
  %1401 = bitcast i8** %1400 to double**
  %1402 = load double*, double** %1401, align 8, !tbaa !108
  %1403 = zext i32 %1266 to i64
  %1404 = icmp ult i32 %1266, 4
  br i1 %1404, label %1491, label %1405

; <label>:1405:                                   ; preds = %1399
  %1406 = getelementptr double, double* %10, i64 %1271
  %1407 = getelementptr double, double* %1402, i64 %1271
  %1408 = icmp ugt double* %1407, %10
  %1409 = icmp ult double* %1402, %1406
  %1410 = and i1 %1408, %1409
  br i1 %1410, label %1491, label %1411

; <label>:1411:                                   ; preds = %1405
  %1412 = and i64 %1271, 4294967292
  %1413 = add nsw i64 %1412, -4
  %1414 = lshr exact i64 %1413, 2
  %1415 = add nuw nsw i64 %1414, 1
  %1416 = and i64 %1415, 3
  %1417 = icmp ult i64 %1413, 12
  br i1 %1417, label %1469, label %1418

; <label>:1418:                                   ; preds = %1411
  %1419 = sub nsw i64 %1415, %1416
  br label %1420

; <label>:1420:                                   ; preds = %1420, %1418
  %1421 = phi i64 [ 0, %1418 ], [ %1466, %1420 ]
  %1422 = phi i64 [ %1419, %1418 ], [ %1467, %1420 ]
  %1423 = getelementptr inbounds double, double* %1402, i64 %1421
  %1424 = bitcast double* %1423 to <2 x i64>*
  %1425 = load <2 x i64>, <2 x i64>* %1424, align 8, !tbaa !16, !alias.scope !109
  %1426 = getelementptr double, double* %1423, i64 2
  %1427 = bitcast double* %1426 to <2 x i64>*
  %1428 = load <2 x i64>, <2 x i64>* %1427, align 8, !tbaa !16, !alias.scope !109
  %1429 = getelementptr inbounds double, double* %10, i64 %1421
  %1430 = bitcast double* %1429 to <2 x i64>*
  store <2 x i64> %1425, <2 x i64>* %1430, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1431 = getelementptr double, double* %1429, i64 2
  %1432 = bitcast double* %1431 to <2 x i64>*
  store <2 x i64> %1428, <2 x i64>* %1432, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1433 = or i64 %1421, 4
  %1434 = getelementptr inbounds double, double* %1402, i64 %1433
  %1435 = bitcast double* %1434 to <2 x i64>*
  %1436 = load <2 x i64>, <2 x i64>* %1435, align 8, !tbaa !16, !alias.scope !109
  %1437 = getelementptr double, double* %1434, i64 2
  %1438 = bitcast double* %1437 to <2 x i64>*
  %1439 = load <2 x i64>, <2 x i64>* %1438, align 8, !tbaa !16, !alias.scope !109
  %1440 = getelementptr inbounds double, double* %10, i64 %1433
  %1441 = bitcast double* %1440 to <2 x i64>*
  store <2 x i64> %1436, <2 x i64>* %1441, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1442 = getelementptr double, double* %1440, i64 2
  %1443 = bitcast double* %1442 to <2 x i64>*
  store <2 x i64> %1439, <2 x i64>* %1443, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1444 = or i64 %1421, 8
  %1445 = getelementptr inbounds double, double* %1402, i64 %1444
  %1446 = bitcast double* %1445 to <2 x i64>*
  %1447 = load <2 x i64>, <2 x i64>* %1446, align 8, !tbaa !16, !alias.scope !109
  %1448 = getelementptr double, double* %1445, i64 2
  %1449 = bitcast double* %1448 to <2 x i64>*
  %1450 = load <2 x i64>, <2 x i64>* %1449, align 8, !tbaa !16, !alias.scope !109
  %1451 = getelementptr inbounds double, double* %10, i64 %1444
  %1452 = bitcast double* %1451 to <2 x i64>*
  store <2 x i64> %1447, <2 x i64>* %1452, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1453 = getelementptr double, double* %1451, i64 2
  %1454 = bitcast double* %1453 to <2 x i64>*
  store <2 x i64> %1450, <2 x i64>* %1454, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1455 = or i64 %1421, 12
  %1456 = getelementptr inbounds double, double* %1402, i64 %1455
  %1457 = bitcast double* %1456 to <2 x i64>*
  %1458 = load <2 x i64>, <2 x i64>* %1457, align 8, !tbaa !16, !alias.scope !109
  %1459 = getelementptr double, double* %1456, i64 2
  %1460 = bitcast double* %1459 to <2 x i64>*
  %1461 = load <2 x i64>, <2 x i64>* %1460, align 8, !tbaa !16, !alias.scope !109
  %1462 = getelementptr inbounds double, double* %10, i64 %1455
  %1463 = bitcast double* %1462 to <2 x i64>*
  store <2 x i64> %1458, <2 x i64>* %1463, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1464 = getelementptr double, double* %1462, i64 2
  %1465 = bitcast double* %1464 to <2 x i64>*
  store <2 x i64> %1461, <2 x i64>* %1465, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1466 = add i64 %1421, 16
  %1467 = add i64 %1422, -4
  %1468 = icmp eq i64 %1467, 0
  br i1 %1468, label %1469, label %1420, !llvm.loop !114

; <label>:1469:                                   ; preds = %1420, %1411
  %1470 = phi i64 [ 0, %1411 ], [ %1466, %1420 ]
  %1471 = icmp eq i64 %1416, 0
  br i1 %1471, label %1489, label %1472

; <label>:1472:                                   ; preds = %1469
  br label %1473

; <label>:1473:                                   ; preds = %1473, %1472
  %1474 = phi i64 [ %1470, %1472 ], [ %1486, %1473 ]
  %1475 = phi i64 [ %1416, %1472 ], [ %1487, %1473 ]
  %1476 = getelementptr inbounds double, double* %1402, i64 %1474
  %1477 = bitcast double* %1476 to <2 x i64>*
  %1478 = load <2 x i64>, <2 x i64>* %1477, align 8, !tbaa !16, !alias.scope !109
  %1479 = getelementptr double, double* %1476, i64 2
  %1480 = bitcast double* %1479 to <2 x i64>*
  %1481 = load <2 x i64>, <2 x i64>* %1480, align 8, !tbaa !16, !alias.scope !109
  %1482 = getelementptr inbounds double, double* %10, i64 %1474
  %1483 = bitcast double* %1482 to <2 x i64>*
  store <2 x i64> %1478, <2 x i64>* %1483, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1484 = getelementptr double, double* %1482, i64 2
  %1485 = bitcast double* %1484 to <2 x i64>*
  store <2 x i64> %1481, <2 x i64>* %1485, align 8, !tbaa !16, !alias.scope !112, !noalias !109
  %1486 = add i64 %1474, 4
  %1487 = add i64 %1475, -1
  %1488 = icmp eq i64 %1487, 0
  br i1 %1488, label %1489, label %1473, !llvm.loop !115

; <label>:1489:                                   ; preds = %1473, %1469
  %1490 = icmp eq i64 %1412, %1271
  br i1 %1490, label %1565, label %1491

; <label>:1491:                                   ; preds = %1489, %1405, %1399
  %1492 = phi i64 [ 0, %1405 ], [ 0, %1399 ], [ %1412, %1489 ]
  %1493 = sub nsw i64 %1403, %1492
  %1494 = add nsw i64 %1403, -1
  %1495 = sub nsw i64 %1494, %1492
  %1496 = and i64 %1493, 7
  %1497 = icmp eq i64 %1496, 0
  br i1 %1497, label %1510, label %1498

; <label>:1498:                                   ; preds = %1491
  br label %1499

; <label>:1499:                                   ; preds = %1499, %1498
  %1500 = phi i64 [ %1507, %1499 ], [ %1492, %1498 ]
  %1501 = phi i64 [ %1508, %1499 ], [ %1496, %1498 ]
  %1502 = getelementptr inbounds double, double* %1402, i64 %1500
  %1503 = bitcast double* %1502 to i64*
  %1504 = load i64, i64* %1503, align 8, !tbaa !16
  %1505 = getelementptr inbounds double, double* %10, i64 %1500
  %1506 = bitcast double* %1505 to i64*
  store i64 %1504, i64* %1506, align 8, !tbaa !16
  %1507 = add nuw nsw i64 %1500, 1
  %1508 = add i64 %1501, -1
  %1509 = icmp eq i64 %1508, 0
  br i1 %1509, label %1510, label %1499, !llvm.loop !116

; <label>:1510:                                   ; preds = %1499, %1491
  %1511 = phi i64 [ %1492, %1491 ], [ %1507, %1499 ]
  %1512 = icmp ult i64 %1495, 7
  br i1 %1512, label %1565, label %1513

; <label>:1513:                                   ; preds = %1510
  br label %1514

; <label>:1514:                                   ; preds = %1514, %1513
  %1515 = phi i64 [ %1511, %1513 ], [ %1563, %1514 ]
  %1516 = getelementptr inbounds double, double* %1402, i64 %1515
  %1517 = bitcast double* %1516 to i64*
  %1518 = load i64, i64* %1517, align 8, !tbaa !16
  %1519 = getelementptr inbounds double, double* %10, i64 %1515
  %1520 = bitcast double* %1519 to i64*
  store i64 %1518, i64* %1520, align 8, !tbaa !16
  %1521 = add nuw nsw i64 %1515, 1
  %1522 = getelementptr inbounds double, double* %1402, i64 %1521
  %1523 = bitcast double* %1522 to i64*
  %1524 = load i64, i64* %1523, align 8, !tbaa !16
  %1525 = getelementptr inbounds double, double* %10, i64 %1521
  %1526 = bitcast double* %1525 to i64*
  store i64 %1524, i64* %1526, align 8, !tbaa !16
  %1527 = add nsw i64 %1515, 2
  %1528 = getelementptr inbounds double, double* %1402, i64 %1527
  %1529 = bitcast double* %1528 to i64*
  %1530 = load i64, i64* %1529, align 8, !tbaa !16
  %1531 = getelementptr inbounds double, double* %10, i64 %1527
  %1532 = bitcast double* %1531 to i64*
  store i64 %1530, i64* %1532, align 8, !tbaa !16
  %1533 = add nsw i64 %1515, 3
  %1534 = getelementptr inbounds double, double* %1402, i64 %1533
  %1535 = bitcast double* %1534 to i64*
  %1536 = load i64, i64* %1535, align 8, !tbaa !16
  %1537 = getelementptr inbounds double, double* %10, i64 %1533
  %1538 = bitcast double* %1537 to i64*
  store i64 %1536, i64* %1538, align 8, !tbaa !16
  %1539 = add nsw i64 %1515, 4
  %1540 = getelementptr inbounds double, double* %1402, i64 %1539
  %1541 = bitcast double* %1540 to i64*
  %1542 = load i64, i64* %1541, align 8, !tbaa !16
  %1543 = getelementptr inbounds double, double* %10, i64 %1539
  %1544 = bitcast double* %1543 to i64*
  store i64 %1542, i64* %1544, align 8, !tbaa !16
  %1545 = add nsw i64 %1515, 5
  %1546 = getelementptr inbounds double, double* %1402, i64 %1545
  %1547 = bitcast double* %1546 to i64*
  %1548 = load i64, i64* %1547, align 8, !tbaa !16
  %1549 = getelementptr inbounds double, double* %10, i64 %1545
  %1550 = bitcast double* %1549 to i64*
  store i64 %1548, i64* %1550, align 8, !tbaa !16
  %1551 = add nsw i64 %1515, 6
  %1552 = getelementptr inbounds double, double* %1402, i64 %1551
  %1553 = bitcast double* %1552 to i64*
  %1554 = load i64, i64* %1553, align 8, !tbaa !16
  %1555 = getelementptr inbounds double, double* %10, i64 %1551
  %1556 = bitcast double* %1555 to i64*
  store i64 %1554, i64* %1556, align 8, !tbaa !16
  %1557 = add nsw i64 %1515, 7
  %1558 = getelementptr inbounds double, double* %1402, i64 %1557
  %1559 = bitcast double* %1558 to i64*
  %1560 = load i64, i64* %1559, align 8, !tbaa !16
  %1561 = getelementptr inbounds double, double* %10, i64 %1557
  %1562 = bitcast double* %1561 to i64*
  store i64 %1560, i64* %1562, align 8, !tbaa !16
  %1563 = add nsw i64 %1515, 8
  %1564 = icmp eq i64 %1563, %1403
  br i1 %1564, label %1565, label %1514, !llvm.loop !117

; <label>:1565:                                   ; preds = %1510, %1514, %1489, %1263, %1398, %1124, %16, %24
  %1566 = phi i32 [ 0, %24 ], [ 0, %16 ], [ 1, %1124 ], [ 1, %1398 ], [ 1, %1263 ], [ 1, %1489 ], [ 1, %1514 ], [ 1, %1510 ]
  ret i32 %1566
}

; Function Attrs: argmemonly
declare void @memset_pattern16(i8* nocapture, i8* nocapture readonly, i64) local_unnamed_addr #1

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly }
attributes #2 = { nounwind }

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
!11 = !{!12, !8, i64 40}
!12 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!13 = !{!12, !8, i64 76}
!14 = !{!15, !9, i64 96}
!15 = !{!"", !8, i64 0, !8, i64 4, !8, i64 8, !8, i64 12, !8, i64 16, !8, i64 20, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48, !9, i64 56, !9, i64 64, !9, i64 72, !9, i64 80, !9, i64 88, !9, i64 96, !10, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !9, i64 144, !9, i64 152, !8, i64 160}
!16 = !{!5, !5, i64 0}
!17 = !{!18}
!18 = distinct !{!18, !19}
!19 = distinct !{!19, !"LVerDomain"}
!20 = !{!21}
!21 = distinct !{!21, !19}
!22 = distinct !{!22, !23}
!23 = !{!"llvm.loop.isvectorized", i32 1}
!24 = distinct !{!24, !25}
!25 = !{!"llvm.loop.unroll.disable"}
!26 = distinct !{!26, !25}
!27 = distinct !{!27, !23}
!28 = !{!12, !9, i64 64}
!29 = !{!8, !8, i64 0}
!30 = !{!31}
!31 = distinct !{!31, !32}
!32 = distinct !{!32, !"LVerDomain"}
!33 = !{!34}
!34 = distinct !{!34, !32}
!35 = distinct !{!35, !23}
!36 = distinct !{!36, !25}
!37 = distinct !{!37, !25}
!38 = distinct !{!38, !23}
!39 = !{!15, !9, i64 24}
!40 = !{!41}
!41 = distinct !{!41, !42}
!42 = distinct !{!42, !"LVerDomain"}
!43 = !{!44}
!44 = distinct !{!44, !42}
!45 = distinct !{!45, !23}
!46 = distinct !{!46, !25}
!47 = distinct !{!47, !25}
!48 = distinct !{!48, !23}
!49 = !{!12, !9, i64 56}
!50 = !{!51}
!51 = distinct !{!51, !52}
!52 = distinct !{!52, !"LVerDomain"}
!53 = !{!54}
!54 = distinct !{!54, !52}
!55 = distinct !{!55, !23}
!56 = distinct !{!56, !25}
!57 = distinct !{!57, !25}
!58 = distinct !{!58, !23}
!59 = !{!15, !9, i64 72}
!60 = !{!9, !9, i64 0}
!61 = !{!15, !9, i64 40}
!62 = !{!15, !9, i64 56}
!63 = !{!64}
!64 = distinct !{!64, !65}
!65 = distinct !{!65, !"LVerDomain"}
!66 = !{!67}
!67 = distinct !{!67, !65}
!68 = !{!69}
!69 = distinct !{!69, !65}
!70 = !{!71}
!71 = distinct !{!71, !65}
!72 = distinct !{!72, !23}
!73 = distinct !{!73, !23}
!74 = !{!15, !9, i64 88}
!75 = !{!15, !9, i64 48}
!76 = !{!15, !9, i64 64}
!77 = !{!78}
!78 = distinct !{!78, !79}
!79 = distinct !{!79, !"LVerDomain"}
!80 = !{!81}
!81 = distinct !{!81, !79}
!82 = !{!83}
!83 = distinct !{!83, !79}
!84 = !{!85}
!85 = distinct !{!85, !79}
!86 = distinct !{!86, !23}
!87 = distinct !{!87, !23}
!88 = !{!15, !9, i64 136}
!89 = !{!90}
!90 = distinct !{!90, !91}
!91 = distinct !{!91, !"LVerDomain"}
!92 = !{!93}
!93 = distinct !{!93, !91}
!94 = distinct !{!94, !23}
!95 = distinct !{!95, !25}
!96 = distinct !{!96, !25}
!97 = distinct !{!97, !23}
!98 = !{!15, !9, i64 144}
!99 = !{!100}
!100 = distinct !{!100, !101}
!101 = distinct !{!101, !"LVerDomain"}
!102 = !{!103}
!103 = distinct !{!103, !101}
!104 = distinct !{!104, !23}
!105 = distinct !{!105, !25}
!106 = distinct !{!106, !25}
!107 = distinct !{!107, !23}
!108 = !{!15, !9, i64 152}
!109 = !{!110}
!110 = distinct !{!110, !111}
!111 = distinct !{!111, !"LVerDomain"}
!112 = !{!113}
!113 = distinct !{!113, !111}
!114 = distinct !{!114, !23}
!115 = distinct !{!115, !25}
!116 = distinct !{!116, !25}
!117 = distinct !{!117, !23}
