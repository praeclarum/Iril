; ModuleID = 'klu_analyze.c'
source_filename = "klu_analyze.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_analyze(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %5 = alloca [20 x double], align 16
  %6 = alloca [20 x i32], align 16
  %7 = alloca double, align 8
  %8 = alloca %struct.klu_symbolic*, align 8
  %9 = icmp eq %struct.klu_common_struct* %3, null
  br i1 %9, label %976, label %10

; <label>:10:                                     ; preds = %4
  %11 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11
  store i32 0, i32* %11, align 4, !tbaa !3
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 13
  store i32 -1, i32* %12, align 4, !tbaa !11
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 6
  %14 = load i32, i32* %13, align 4, !tbaa !12
  %15 = icmp eq i32 %14, 2
  br i1 %15, label %16, label %18

; <label>:16:                                     ; preds = %10
  %17 = tail call %struct.klu_symbolic* @klu_analyze_given(i32 %0, i32* %1, i32* %2, i32* null, i32* null, %struct.klu_common_struct* nonnull %3) #3
  br label %976

; <label>:18:                                     ; preds = %10
  %19 = bitcast double* %7 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %19) #3
  %20 = bitcast %struct.klu_symbolic** %8 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %20) #3
  %21 = tail call %struct.klu_symbolic* @klu_alloc_symbolic(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* nonnull %3) #3
  store %struct.klu_symbolic* %21, %struct.klu_symbolic** %8, align 8, !tbaa !13
  %22 = icmp eq %struct.klu_symbolic* %21, null
  br i1 %22, label %974, label %23

; <label>:23:                                     ; preds = %18
  %24 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 7
  %25 = load i32*, i32** %24, align 8, !tbaa !14
  %26 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 8
  %27 = load i32*, i32** %26, align 8, !tbaa !16
  %28 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 9
  %29 = load i32*, i32** %28, align 8, !tbaa !17
  %30 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 4
  %31 = load double*, double** %30, align 8, !tbaa !18
  %32 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 6
  %33 = load i32, i32* %32, align 4, !tbaa !19
  %34 = load i32, i32* %13, align 4, !tbaa !12
  switch i32 %34, label %44 [
    i32 1, label %35
    i32 0, label %42
    i32 3, label %38
  ]

; <label>:35:                                     ; preds = %23
  %36 = tail call i64 @colamd_recommended(i32 %33, i32 %0, i32 %0) #3
  %37 = trunc i64 %36 to i32
  br label %46

; <label>:38:                                     ; preds = %23
  %39 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 8
  %40 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %39, align 8, !tbaa !20
  %41 = icmp eq i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)* %40, null
  br i1 %41, label %44, label %42

; <label>:42:                                     ; preds = %38, %23
  %43 = add nsw i32 %33, 1
  br label %46

; <label>:44:                                     ; preds = %38, %23
  store i32 -3, i32* %11, align 4, !tbaa !3
  %45 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #3
  br label %974

; <label>:46:                                     ; preds = %42, %35
  %47 = phi i32 [ %37, %35 ], [ %43, %42 ]
  %48 = sext i32 %0 to i64
  %49 = tail call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %50 = bitcast i8* %49 to i32*
  %51 = tail call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %52 = bitcast i8* %51 to i32*
  %53 = load i32, i32* %11, align 4, !tbaa !3
  %54 = icmp slt i32 %53, 0
  br i1 %54, label %55, label %59

; <label>:55:                                     ; preds = %46
  %56 = tail call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %57 = tail call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %58 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #3
  br label %974

; <label>:59:                                     ; preds = %46
  %60 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 5
  %61 = load i32, i32* %60, align 8, !tbaa !21
  %62 = icmp ne i32 %61, 0
  %63 = zext i1 %62 to i32
  %64 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 13
  store i32 %34, i32* %64, align 4, !tbaa !22
  %65 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 14
  store i32 %63, i32* %65, align 8, !tbaa !23
  %66 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %21, i64 0, i32 15
  store i32 -1, i32* %66, align 4, !tbaa !24
  %67 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 21
  store double 0.000000e+00, double* %67, align 8, !tbaa !25
  br i1 %62, label %68, label %285

; <label>:68:                                     ; preds = %59
  %69 = mul nsw i32 %0, 5
  %70 = sext i32 %69 to i64
  %71 = tail call i8* @klu_malloc(i64 %70, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %72 = load i32, i32* %11, align 4, !tbaa !3
  %73 = icmp slt i32 %72, 0
  br i1 %73, label %74, label %78

; <label>:74:                                     ; preds = %68
  %75 = tail call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %76 = tail call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %77 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #3
  br label %974

; <label>:78:                                     ; preds = %68
  %79 = bitcast i8* %71 to i32*
  %80 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 4
  %81 = load double, double* %80, align 8, !tbaa !26
  %82 = call i32 @btf_order(i32 %0, i32* %1, i32* %2, double %81, double* nonnull %7, i32* %50, i32* %52, i32* %29, i32* nonnull %66, i32* %79) #3
  %83 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !tbaa !13
  %84 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %83, i64 0, i32 15
  %85 = load i32, i32* %84, align 4, !tbaa !24
  store i32 %85, i32* %12, align 4, !tbaa !11
  %86 = load double, double* %7, align 8, !tbaa !27
  %87 = load double, double* %67, align 8, !tbaa !25
  %88 = fadd double %86, %87
  store double %88, double* %67, align 8, !tbaa !25
  %89 = call i8* @klu_free(i8* %71, i64 %70, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %90 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !tbaa !13
  %91 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %90, i64 0, i32 15
  %92 = load i32, i32* %91, align 4, !tbaa !24
  %93 = icmp slt i32 %92, %0
  %94 = icmp sgt i32 %0, 0
  %95 = and i1 %94, %93
  br i1 %95, label %96, label %174

; <label>:96:                                     ; preds = %78
  %97 = zext i32 %0 to i64
  %98 = icmp ult i32 %0, 8
  br i1 %98, label %163, label %99

; <label>:99:                                     ; preds = %96
  %100 = and i64 %97, 4294967288
  %101 = add nsw i64 %100, -8
  %102 = lshr exact i64 %101, 3
  %103 = add nuw nsw i64 %102, 1
  %104 = and i64 %103, 1
  %105 = icmp eq i64 %101, 0
  br i1 %105, label %143, label %106

; <label>:106:                                    ; preds = %99
  %107 = sub nsw i64 %103, %104
  br label %108

; <label>:108:                                    ; preds = %108, %106
  %109 = phi i64 [ 0, %106 ], [ %140, %108 ]
  %110 = phi i64 [ %107, %106 ], [ %141, %108 ]
  %111 = getelementptr inbounds i32, i32* %52, i64 %109
  %112 = bitcast i32* %111 to <4 x i32>*
  %113 = load <4 x i32>, <4 x i32>* %112, align 4, !tbaa !28
  %114 = getelementptr i32, i32* %111, i64 4
  %115 = bitcast i32* %114 to <4 x i32>*
  %116 = load <4 x i32>, <4 x i32>* %115, align 4, !tbaa !28
  %117 = icmp slt <4 x i32> %113, <i32 -1, i32 -1, i32 -1, i32 -1>
  %118 = icmp slt <4 x i32> %116, <i32 -1, i32 -1, i32 -1, i32 -1>
  %119 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %113
  %120 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %116
  %121 = select <4 x i1> %117, <4 x i32> %119, <4 x i32> %113
  %122 = select <4 x i1> %118, <4 x i32> %120, <4 x i32> %116
  %123 = bitcast i32* %111 to <4 x i32>*
  store <4 x i32> %121, <4 x i32>* %123, align 4, !tbaa !28
  %124 = bitcast i32* %114 to <4 x i32>*
  store <4 x i32> %122, <4 x i32>* %124, align 4, !tbaa !28
  %125 = or i64 %109, 8
  %126 = getelementptr inbounds i32, i32* %52, i64 %125
  %127 = bitcast i32* %126 to <4 x i32>*
  %128 = load <4 x i32>, <4 x i32>* %127, align 4, !tbaa !28
  %129 = getelementptr i32, i32* %126, i64 4
  %130 = bitcast i32* %129 to <4 x i32>*
  %131 = load <4 x i32>, <4 x i32>* %130, align 4, !tbaa !28
  %132 = icmp slt <4 x i32> %128, <i32 -1, i32 -1, i32 -1, i32 -1>
  %133 = icmp slt <4 x i32> %131, <i32 -1, i32 -1, i32 -1, i32 -1>
  %134 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %128
  %135 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %131
  %136 = select <4 x i1> %132, <4 x i32> %134, <4 x i32> %128
  %137 = select <4 x i1> %133, <4 x i32> %135, <4 x i32> %131
  %138 = bitcast i32* %126 to <4 x i32>*
  store <4 x i32> %136, <4 x i32>* %138, align 4, !tbaa !28
  %139 = bitcast i32* %129 to <4 x i32>*
  store <4 x i32> %137, <4 x i32>* %139, align 4, !tbaa !28
  %140 = add i64 %109, 16
  %141 = add i64 %110, -2
  %142 = icmp eq i64 %141, 0
  br i1 %142, label %143, label %108, !llvm.loop !29

; <label>:143:                                    ; preds = %108, %99
  %144 = phi i64 [ 0, %99 ], [ %140, %108 ]
  %145 = icmp eq i64 %104, 0
  br i1 %145, label %161, label %146

; <label>:146:                                    ; preds = %143
  %147 = getelementptr inbounds i32, i32* %52, i64 %144
  %148 = bitcast i32* %147 to <4 x i32>*
  %149 = load <4 x i32>, <4 x i32>* %148, align 4, !tbaa !28
  %150 = getelementptr i32, i32* %147, i64 4
  %151 = bitcast i32* %150 to <4 x i32>*
  %152 = load <4 x i32>, <4 x i32>* %151, align 4, !tbaa !28
  %153 = icmp slt <4 x i32> %149, <i32 -1, i32 -1, i32 -1, i32 -1>
  %154 = icmp slt <4 x i32> %152, <i32 -1, i32 -1, i32 -1, i32 -1>
  %155 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %149
  %156 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %152
  %157 = select <4 x i1> %153, <4 x i32> %155, <4 x i32> %149
  %158 = select <4 x i1> %154, <4 x i32> %156, <4 x i32> %152
  %159 = bitcast i32* %147 to <4 x i32>*
  store <4 x i32> %157, <4 x i32>* %159, align 4, !tbaa !28
  %160 = bitcast i32* %150 to <4 x i32>*
  store <4 x i32> %158, <4 x i32>* %160, align 4, !tbaa !28
  br label %161

; <label>:161:                                    ; preds = %143, %146
  %162 = icmp eq i64 %100, %97
  br i1 %162, label %174, label %163

; <label>:163:                                    ; preds = %161, %96
  %164 = phi i64 [ 0, %96 ], [ %100, %161 ]
  br label %165

; <label>:165:                                    ; preds = %163, %165
  %166 = phi i64 [ %172, %165 ], [ %164, %163 ]
  %167 = getelementptr inbounds i32, i32* %52, i64 %166
  %168 = load i32, i32* %167, align 4, !tbaa !28
  %169 = icmp slt i32 %168, -1
  %170 = sub i32 -2, %168
  %171 = select i1 %169, i32 %170, i32 %168
  store i32 %171, i32* %167, align 4, !tbaa !28
  %172 = add nuw nsw i64 %166, 1
  %173 = icmp eq i64 %172, %97
  br i1 %173, label %174, label %165, !llvm.loop !31

; <label>:174:                                    ; preds = %165, %161, %78
  %175 = icmp sgt i32 %82, 0
  br i1 %175, label %176, label %390

; <label>:176:                                    ; preds = %174
  %177 = load i32, i32* %29, align 4, !tbaa !28
  %178 = zext i32 %82 to i64
  %179 = icmp ult i32 %82, 8
  br i1 %179, label %270, label %180

; <label>:180:                                    ; preds = %176
  %181 = and i64 %178, 4294967288
  %182 = insertelement <4 x i32> undef, i32 %177, i32 3
  %183 = add nsw i64 %181, -8
  %184 = lshr exact i64 %183, 3
  %185 = add nuw nsw i64 %184, 1
  %186 = and i64 %185, 1
  %187 = icmp eq i64 %183, 0
  br i1 %187, label %231, label %188

; <label>:188:                                    ; preds = %180
  %189 = sub nsw i64 %185, %186
  br label %190

; <label>:190:                                    ; preds = %190, %188
  %191 = phi i64 [ 0, %188 ], [ %226, %190 ]
  %192 = phi <4 x i32> [ %182, %188 ], [ %217, %190 ]
  %193 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %188 ], [ %224, %190 ]
  %194 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %188 ], [ %225, %190 ]
  %195 = phi i64 [ %189, %188 ], [ %227, %190 ]
  %196 = or i64 %191, 1
  %197 = getelementptr inbounds i32, i32* %29, i64 %196
  %198 = bitcast i32* %197 to <4 x i32>*
  %199 = load <4 x i32>, <4 x i32>* %198, align 4, !tbaa !28
  %200 = getelementptr i32, i32* %197, i64 4
  %201 = bitcast i32* %200 to <4 x i32>*
  %202 = load <4 x i32>, <4 x i32>* %201, align 4, !tbaa !28
  %203 = shufflevector <4 x i32> %192, <4 x i32> %199, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %204 = shufflevector <4 x i32> %199, <4 x i32> %202, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %205 = sub nsw <4 x i32> %199, %203
  %206 = sub nsw <4 x i32> %202, %204
  %207 = icmp sgt <4 x i32> %193, %205
  %208 = icmp sgt <4 x i32> %194, %206
  %209 = select <4 x i1> %207, <4 x i32> %193, <4 x i32> %205
  %210 = select <4 x i1> %208, <4 x i32> %194, <4 x i32> %206
  %211 = or i64 %191, 9
  %212 = getelementptr inbounds i32, i32* %29, i64 %211
  %213 = bitcast i32* %212 to <4 x i32>*
  %214 = load <4 x i32>, <4 x i32>* %213, align 4, !tbaa !28
  %215 = getelementptr i32, i32* %212, i64 4
  %216 = bitcast i32* %215 to <4 x i32>*
  %217 = load <4 x i32>, <4 x i32>* %216, align 4, !tbaa !28
  %218 = shufflevector <4 x i32> %202, <4 x i32> %214, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %219 = shufflevector <4 x i32> %214, <4 x i32> %217, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %220 = sub nsw <4 x i32> %214, %218
  %221 = sub nsw <4 x i32> %217, %219
  %222 = icmp sgt <4 x i32> %209, %220
  %223 = icmp sgt <4 x i32> %210, %221
  %224 = select <4 x i1> %222, <4 x i32> %209, <4 x i32> %220
  %225 = select <4 x i1> %223, <4 x i32> %210, <4 x i32> %221
  %226 = add i64 %191, 16
  %227 = add i64 %195, -2
  %228 = icmp eq i64 %227, 0
  br i1 %228, label %229, label %190, !llvm.loop !33

; <label>:229:                                    ; preds = %190
  %230 = or i64 %226, 1
  br label %231

; <label>:231:                                    ; preds = %229, %180
  %232 = phi <4 x i32> [ undef, %180 ], [ %217, %229 ]
  %233 = phi <4 x i32> [ undef, %180 ], [ %224, %229 ]
  %234 = phi <4 x i32> [ undef, %180 ], [ %225, %229 ]
  %235 = phi i64 [ 1, %180 ], [ %230, %229 ]
  %236 = phi <4 x i32> [ %182, %180 ], [ %217, %229 ]
  %237 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %180 ], [ %224, %229 ]
  %238 = phi <4 x i32> [ <i32 1, i32 1, i32 1, i32 1>, %180 ], [ %225, %229 ]
  %239 = icmp eq i64 %186, 0
  br i1 %239, label %255, label %240

; <label>:240:                                    ; preds = %231
  %241 = getelementptr inbounds i32, i32* %29, i64 %235
  %242 = bitcast i32* %241 to <4 x i32>*
  %243 = load <4 x i32>, <4 x i32>* %242, align 4, !tbaa !28
  %244 = getelementptr i32, i32* %241, i64 4
  %245 = bitcast i32* %244 to <4 x i32>*
  %246 = load <4 x i32>, <4 x i32>* %245, align 4, !tbaa !28
  %247 = shufflevector <4 x i32> %236, <4 x i32> %243, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %248 = shufflevector <4 x i32> %243, <4 x i32> %246, <4 x i32> <i32 3, i32 4, i32 5, i32 6>
  %249 = sub nsw <4 x i32> %243, %247
  %250 = sub nsw <4 x i32> %246, %248
  %251 = icmp sgt <4 x i32> %238, %250
  %252 = select <4 x i1> %251, <4 x i32> %238, <4 x i32> %250
  %253 = icmp sgt <4 x i32> %237, %249
  %254 = select <4 x i1> %253, <4 x i32> %237, <4 x i32> %249
  br label %255

; <label>:255:                                    ; preds = %231, %240
  %256 = phi <4 x i32> [ %232, %231 ], [ %246, %240 ]
  %257 = phi <4 x i32> [ %233, %231 ], [ %254, %240 ]
  %258 = phi <4 x i32> [ %234, %231 ], [ %252, %240 ]
  %259 = icmp sgt <4 x i32> %257, %258
  %260 = select <4 x i1> %259, <4 x i32> %257, <4 x i32> %258
  %261 = shufflevector <4 x i32> %260, <4 x i32> undef, <4 x i32> <i32 2, i32 3, i32 undef, i32 undef>
  %262 = icmp sgt <4 x i32> %260, %261
  %263 = select <4 x i1> %262, <4 x i32> %260, <4 x i32> %261
  %264 = shufflevector <4 x i32> %263, <4 x i32> undef, <4 x i32> <i32 1, i32 undef, i32 undef, i32 undef>
  %265 = icmp sgt <4 x i32> %263, %264
  %266 = select <4 x i1> %265, <4 x i32> %263, <4 x i32> %264
  %267 = extractelement <4 x i32> %266, i32 0
  %268 = icmp eq i64 %181, %178
  %269 = extractelement <4 x i32> %256, i32 3
  br i1 %268, label %390, label %270

; <label>:270:                                    ; preds = %255, %176
  %271 = phi i32 [ %177, %176 ], [ %269, %255 ]
  %272 = phi i64 [ 0, %176 ], [ %181, %255 ]
  %273 = phi i32 [ 1, %176 ], [ %267, %255 ]
  br label %274

; <label>:274:                                    ; preds = %270, %274
  %275 = phi i32 [ %280, %274 ], [ %271, %270 ]
  %276 = phi i64 [ %278, %274 ], [ %272, %270 ]
  %277 = phi i32 [ %283, %274 ], [ %273, %270 ]
  %278 = add nuw nsw i64 %276, 1
  %279 = getelementptr inbounds i32, i32* %29, i64 %278
  %280 = load i32, i32* %279, align 4, !tbaa !28
  %281 = sub nsw i32 %280, %275
  %282 = icmp sgt i32 %277, %281
  %283 = select i1 %282, i32 %277, i32 %281
  %284 = icmp eq i64 %278, %178
  br i1 %284, label %390, label %274, !llvm.loop !34

; <label>:285:                                    ; preds = %59
  store i32 0, i32* %29, align 4, !tbaa !28
  %286 = getelementptr inbounds i32, i32* %29, i64 1
  store i32 %0, i32* %286, align 4, !tbaa !28
  %287 = icmp sgt i32 %0, 0
  br i1 %287, label %288, label %390

; <label>:288:                                    ; preds = %285
  %289 = zext i32 %0 to i64
  %290 = icmp ult i32 %0, 8
  br i1 %290, label %351, label %291

; <label>:291:                                    ; preds = %288
  %292 = shl nuw nsw i64 %289, 2
  %293 = getelementptr i8, i8* %49, i64 %292
  %294 = getelementptr i8, i8* %51, i64 %292
  %295 = icmp ult i8* %49, %294
  %296 = icmp ult i8* %51, %293
  %297 = and i1 %295, %296
  br i1 %297, label %351, label %298

; <label>:298:                                    ; preds = %291
  %299 = and i64 %289, 4294967288
  %300 = add nsw i64 %299, -8
  %301 = lshr exact i64 %300, 3
  %302 = add nuw nsw i64 %301, 1
  %303 = and i64 %302, 1
  %304 = icmp eq i64 %300, 0
  br i1 %304, label %335, label %305

; <label>:305:                                    ; preds = %298
  %306 = sub nsw i64 %302, %303
  br label %307

; <label>:307:                                    ; preds = %307, %305
  %308 = phi i64 [ 0, %305 ], [ %331, %307 ]
  %309 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %305 ], [ %332, %307 ]
  %310 = phi i64 [ %306, %305 ], [ %333, %307 ]
  %311 = getelementptr inbounds i32, i32* %50, i64 %308
  %312 = add <4 x i32> %309, <i32 4, i32 4, i32 4, i32 4>
  %313 = bitcast i32* %311 to <4 x i32>*
  store <4 x i32> %309, <4 x i32>* %313, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %314 = getelementptr i32, i32* %311, i64 4
  %315 = bitcast i32* %314 to <4 x i32>*
  store <4 x i32> %312, <4 x i32>* %315, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %316 = getelementptr inbounds i32, i32* %52, i64 %308
  %317 = bitcast i32* %316 to <4 x i32>*
  store <4 x i32> %309, <4 x i32>* %317, align 4, !tbaa !28, !alias.scope !38
  %318 = getelementptr i32, i32* %316, i64 4
  %319 = bitcast i32* %318 to <4 x i32>*
  store <4 x i32> %312, <4 x i32>* %319, align 4, !tbaa !28, !alias.scope !38
  %320 = or i64 %308, 8
  %321 = add <4 x i32> %309, <i32 8, i32 8, i32 8, i32 8>
  %322 = getelementptr inbounds i32, i32* %50, i64 %320
  %323 = add <4 x i32> %309, <i32 12, i32 12, i32 12, i32 12>
  %324 = bitcast i32* %322 to <4 x i32>*
  store <4 x i32> %321, <4 x i32>* %324, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %325 = getelementptr i32, i32* %322, i64 4
  %326 = bitcast i32* %325 to <4 x i32>*
  store <4 x i32> %323, <4 x i32>* %326, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %327 = getelementptr inbounds i32, i32* %52, i64 %320
  %328 = bitcast i32* %327 to <4 x i32>*
  store <4 x i32> %321, <4 x i32>* %328, align 4, !tbaa !28, !alias.scope !38
  %329 = getelementptr i32, i32* %327, i64 4
  %330 = bitcast i32* %329 to <4 x i32>*
  store <4 x i32> %323, <4 x i32>* %330, align 4, !tbaa !28, !alias.scope !38
  %331 = add i64 %308, 16
  %332 = add <4 x i32> %309, <i32 16, i32 16, i32 16, i32 16>
  %333 = add i64 %310, -2
  %334 = icmp eq i64 %333, 0
  br i1 %334, label %335, label %307, !llvm.loop !40

; <label>:335:                                    ; preds = %307, %298
  %336 = phi i64 [ 0, %298 ], [ %331, %307 ]
  %337 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %298 ], [ %332, %307 ]
  %338 = icmp eq i64 %303, 0
  br i1 %338, label %349, label %339

; <label>:339:                                    ; preds = %335
  %340 = getelementptr inbounds i32, i32* %50, i64 %336
  %341 = add <4 x i32> %337, <i32 4, i32 4, i32 4, i32 4>
  %342 = bitcast i32* %340 to <4 x i32>*
  store <4 x i32> %337, <4 x i32>* %342, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %343 = getelementptr i32, i32* %340, i64 4
  %344 = bitcast i32* %343 to <4 x i32>*
  store <4 x i32> %341, <4 x i32>* %344, align 4, !tbaa !28, !alias.scope !35, !noalias !38
  %345 = getelementptr inbounds i32, i32* %52, i64 %336
  %346 = bitcast i32* %345 to <4 x i32>*
  store <4 x i32> %337, <4 x i32>* %346, align 4, !tbaa !28, !alias.scope !38
  %347 = getelementptr i32, i32* %345, i64 4
  %348 = bitcast i32* %347 to <4 x i32>*
  store <4 x i32> %341, <4 x i32>* %348, align 4, !tbaa !28, !alias.scope !38
  br label %349

; <label>:349:                                    ; preds = %335, %339
  %350 = icmp eq i64 %299, %289
  br i1 %350, label %390, label %351

; <label>:351:                                    ; preds = %349, %291, %288
  %352 = phi i64 [ 0, %291 ], [ 0, %288 ], [ %299, %349 ]
  %353 = add nsw i64 %289, -1
  %354 = sub nsw i64 %353, %352
  %355 = and i64 %289, 3
  %356 = icmp eq i64 %355, 0
  br i1 %356, label %367, label %357

; <label>:357:                                    ; preds = %351
  br label %358

; <label>:358:                                    ; preds = %358, %357
  %359 = phi i64 [ %364, %358 ], [ %352, %357 ]
  %360 = phi i64 [ %365, %358 ], [ %355, %357 ]
  %361 = getelementptr inbounds i32, i32* %50, i64 %359
  %362 = trunc i64 %359 to i32
  store i32 %362, i32* %361, align 4, !tbaa !28
  %363 = getelementptr inbounds i32, i32* %52, i64 %359
  store i32 %362, i32* %363, align 4, !tbaa !28
  %364 = add nuw nsw i64 %359, 1
  %365 = add i64 %360, -1
  %366 = icmp eq i64 %365, 0
  br i1 %366, label %367, label %358, !llvm.loop !41

; <label>:367:                                    ; preds = %358, %351
  %368 = phi i64 [ %352, %351 ], [ %364, %358 ]
  %369 = icmp ult i64 %354, 3
  br i1 %369, label %390, label %370

; <label>:370:                                    ; preds = %367
  br label %371

; <label>:371:                                    ; preds = %371, %370
  %372 = phi i64 [ %368, %370 ], [ %388, %371 ]
  %373 = getelementptr inbounds i32, i32* %50, i64 %372
  %374 = trunc i64 %372 to i32
  store i32 %374, i32* %373, align 4, !tbaa !28
  %375 = getelementptr inbounds i32, i32* %52, i64 %372
  store i32 %374, i32* %375, align 4, !tbaa !28
  %376 = add nuw nsw i64 %372, 1
  %377 = getelementptr inbounds i32, i32* %50, i64 %376
  %378 = trunc i64 %376 to i32
  store i32 %378, i32* %377, align 4, !tbaa !28
  %379 = getelementptr inbounds i32, i32* %52, i64 %376
  store i32 %378, i32* %379, align 4, !tbaa !28
  %380 = add nsw i64 %372, 2
  %381 = getelementptr inbounds i32, i32* %50, i64 %380
  %382 = trunc i64 %380 to i32
  store i32 %382, i32* %381, align 4, !tbaa !28
  %383 = getelementptr inbounds i32, i32* %52, i64 %380
  store i32 %382, i32* %383, align 4, !tbaa !28
  %384 = add nsw i64 %372, 3
  %385 = getelementptr inbounds i32, i32* %50, i64 %384
  %386 = trunc i64 %384 to i32
  store i32 %386, i32* %385, align 4, !tbaa !28
  %387 = getelementptr inbounds i32, i32* %52, i64 %384
  store i32 %386, i32* %387, align 4, !tbaa !28
  %388 = add nsw i64 %372, 4
  %389 = icmp eq i64 %388, %289
  br i1 %389, label %390, label %371, !llvm.loop !43

; <label>:390:                                    ; preds = %367, %371, %274, %349, %255, %285, %174
  %391 = phi %struct.klu_symbolic* [ %90, %174 ], [ %21, %285 ], [ %90, %255 ], [ %21, %349 ], [ %90, %274 ], [ %21, %371 ], [ %21, %367 ]
  %392 = phi i32 [ 1, %174 ], [ %0, %285 ], [ %267, %255 ], [ %0, %349 ], [ %283, %274 ], [ %0, %371 ], [ %0, %367 ]
  %393 = phi i32 [ %82, %174 ], [ 1, %285 ], [ %82, %255 ], [ 1, %349 ], [ %82, %274 ], [ 1, %371 ], [ 1, %367 ]
  %394 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %391, i64 0, i32 11
  store i32 %393, i32* %394, align 4, !tbaa !44
  %395 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %391, i64 0, i32 12
  store i32 %392, i32* %395, align 8, !tbaa !45
  %396 = sext i32 %392 to i64
  %397 = call i8* @klu_malloc(i64 %396, i64 4, %struct.klu_common_struct* %3) #3
  %398 = add nsw i32 %392, 1
  %399 = sext i32 %398 to i64
  %400 = call i8* @klu_malloc(i64 %399, i64 4, %struct.klu_common_struct* %3) #3
  %401 = add nsw i32 %33, 1
  %402 = icmp sgt i32 %47, %401
  %403 = select i1 %402, i32 %47, i32 %401
  %404 = sext i32 %403 to i64
  %405 = call i8* @klu_malloc(i64 %404, i64 4, %struct.klu_common_struct* %3) #3
  %406 = call i8* @klu_malloc(i64 %48, i64 4, %struct.klu_common_struct* %3) #3
  %407 = load i32, i32* %11, align 4, !tbaa !3
  %408 = icmp eq i32 %407, 0
  br i1 %408, label %409, label %961

; <label>:409:                                    ; preds = %390
  %410 = bitcast i8* %406 to i32*
  %411 = bitcast i8* %405 to i32*
  %412 = bitcast i8* %400 to i32*
  %413 = bitcast i8* %397 to i32*
  %414 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !tbaa !13
  %415 = bitcast [20 x double]* %5 to i8*
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %415) #3
  %416 = bitcast [20 x i32]* %6 to i8*
  call void @llvm.lifetime.start.p0i8(i64 80, i8* nonnull %416) #3
  %417 = icmp sgt i32 %0, 0
  br i1 %417, label %418, label %469

; <label>:418:                                    ; preds = %409
  %419 = zext i32 %0 to i64
  %420 = add nsw i64 %419, -1
  %421 = and i64 %419, 3
  %422 = icmp ult i64 %420, 3
  br i1 %422, label %454, label %423

; <label>:423:                                    ; preds = %418
  %424 = sub nsw i64 %419, %421
  br label %425

; <label>:425:                                    ; preds = %425, %423
  %426 = phi i64 [ 0, %423 ], [ %451, %425 ]
  %427 = phi i64 [ %424, %423 ], [ %452, %425 ]
  %428 = getelementptr inbounds i32, i32* %50, i64 %426
  %429 = load i32, i32* %428, align 4, !tbaa !28
  %430 = sext i32 %429 to i64
  %431 = getelementptr inbounds i32, i32* %410, i64 %430
  %432 = trunc i64 %426 to i32
  store i32 %432, i32* %431, align 4, !tbaa !28
  %433 = or i64 %426, 1
  %434 = getelementptr inbounds i32, i32* %50, i64 %433
  %435 = load i32, i32* %434, align 4, !tbaa !28
  %436 = sext i32 %435 to i64
  %437 = getelementptr inbounds i32, i32* %410, i64 %436
  %438 = trunc i64 %433 to i32
  store i32 %438, i32* %437, align 4, !tbaa !28
  %439 = or i64 %426, 2
  %440 = getelementptr inbounds i32, i32* %50, i64 %439
  %441 = load i32, i32* %440, align 4, !tbaa !28
  %442 = sext i32 %441 to i64
  %443 = getelementptr inbounds i32, i32* %410, i64 %442
  %444 = trunc i64 %439 to i32
  store i32 %444, i32* %443, align 4, !tbaa !28
  %445 = or i64 %426, 3
  %446 = getelementptr inbounds i32, i32* %50, i64 %445
  %447 = load i32, i32* %446, align 4, !tbaa !28
  %448 = sext i32 %447 to i64
  %449 = getelementptr inbounds i32, i32* %410, i64 %448
  %450 = trunc i64 %445 to i32
  store i32 %450, i32* %449, align 4, !tbaa !28
  %451 = add nuw nsw i64 %426, 4
  %452 = add i64 %427, -4
  %453 = icmp eq i64 %452, 0
  br i1 %453, label %454, label %425

; <label>:454:                                    ; preds = %425, %418
  %455 = phi i64 [ 0, %418 ], [ %451, %425 ]
  %456 = icmp eq i64 %421, 0
  br i1 %456, label %469, label %457

; <label>:457:                                    ; preds = %454
  br label %458

; <label>:458:                                    ; preds = %458, %457
  %459 = phi i64 [ %455, %457 ], [ %466, %458 ]
  %460 = phi i64 [ %421, %457 ], [ %467, %458 ]
  %461 = getelementptr inbounds i32, i32* %50, i64 %459
  %462 = load i32, i32* %461, align 4, !tbaa !28
  %463 = sext i32 %462 to i64
  %464 = getelementptr inbounds i32, i32* %410, i64 %463
  %465 = trunc i64 %459 to i32
  store i32 %465, i32* %464, align 4, !tbaa !28
  %466 = add nuw nsw i64 %459, 1
  %467 = add i64 %460, -1
  %468 = icmp eq i64 %467, 0
  br i1 %468, label %469, label %458, !llvm.loop !46

; <label>:469:                                    ; preds = %454, %458, %409
  %470 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 0
  store double -1.000000e+00, double* %470, align 8, !tbaa !47
  %471 = icmp sgt i32 %393, 0
  br i1 %471, label %472, label %951

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
  %485 = sext i32 %393 to i64
  br label %486

; <label>:486:                                    ; preds = %949, %472
  %487 = phi i64 [ 0, %472 ], [ %495, %949 ]
  %488 = phi i32 [ -3, %472 ], [ %852, %949 ]
  %489 = phi i32 [ 0, %472 ], [ %587, %949 ]
  %490 = phi i32 [ 0, %472 ], [ %591, %949 ]
  %491 = phi double [ 0.000000e+00, %472 ], [ %859, %949 ]
  %492 = phi double [ 0.000000e+00, %472 ], [ %864, %949 ]
  %493 = getelementptr inbounds i32, i32* %29, i64 %487
  %494 = load i32, i32* %493, align 4, !tbaa !28
  %495 = add nuw nsw i64 %487, 1
  %496 = getelementptr inbounds i32, i32* %29, i64 %495
  %497 = load i32, i32* %496, align 4, !tbaa !28
  %498 = sub nsw i32 %497, %494
  %499 = getelementptr inbounds double, double* %31, i64 %487
  store double -1.000000e+00, double* %499, align 8, !tbaa !27
  %500 = icmp sgt i32 %497, %494
  br i1 %500, label %501, label %585

; <label>:501:                                    ; preds = %486
  %502 = sext i32 %494 to i64
  %503 = sext i32 %497 to i64
  br label %504

; <label>:504:                                    ; preds = %580, %501
  %505 = phi i64 [ %502, %501 ], [ %583, %580 ]
  %506 = phi i32 [ %489, %501 ], [ %582, %580 ]
  %507 = phi i32 [ 0, %501 ], [ %581, %580 ]
  %508 = sub nsw i64 %505, %502
  %509 = getelementptr inbounds i32, i32* %412, i64 %508
  store i32 %507, i32* %509, align 4, !tbaa !28
  %510 = getelementptr inbounds i32, i32* %52, i64 %505
  %511 = load i32, i32* %510, align 4, !tbaa !28
  %512 = add nsw i32 %511, 1
  %513 = sext i32 %512 to i64
  %514 = getelementptr inbounds i32, i32* %1, i64 %513
  %515 = load i32, i32* %514, align 4, !tbaa !28
  %516 = sext i32 %511 to i64
  %517 = getelementptr inbounds i32, i32* %1, i64 %516
  %518 = load i32, i32* %517, align 4, !tbaa !28
  %519 = icmp slt i32 %518, %515
  br i1 %519, label %520, label %580

; <label>:520:                                    ; preds = %504
  %521 = sext i32 %518 to i64
  %522 = sext i32 %515 to i64
  %523 = sub nsw i64 %522, %521
  %524 = add nsw i64 %522, -1
  %525 = and i64 %523, 1
  %526 = icmp eq i64 %525, 0
  br i1 %526, label %545, label %527

; <label>:527:                                    ; preds = %520
  %528 = getelementptr inbounds i32, i32* %2, i64 %521
  %529 = load i32, i32* %528, align 4, !tbaa !28
  %530 = sext i32 %529 to i64
  %531 = getelementptr inbounds i32, i32* %410, i64 %530
  %532 = load i32, i32* %531, align 4, !tbaa !28
  %533 = icmp slt i32 %532, %494
  br i1 %533, label %539, label %534

; <label>:534:                                    ; preds = %527
  %535 = sub nsw i32 %532, %494
  %536 = add nsw i32 %507, 1
  %537 = sext i32 %507 to i64
  %538 = getelementptr inbounds i32, i32* %411, i64 %537
  store i32 %535, i32* %538, align 4, !tbaa !28
  br label %541

; <label>:539:                                    ; preds = %527
  %540 = add nsw i32 %506, 1
  br label %541

; <label>:541:                                    ; preds = %539, %534
  %542 = phi i32 [ %507, %539 ], [ %536, %534 ]
  %543 = phi i32 [ %540, %539 ], [ %506, %534 ]
  %544 = add nsw i64 %521, 1
  br label %545

; <label>:545:                                    ; preds = %541, %520
  %546 = phi i32 [ %542, %541 ], [ undef, %520 ]
  %547 = phi i32 [ %543, %541 ], [ undef, %520 ]
  %548 = phi i64 [ %544, %541 ], [ %521, %520 ]
  %549 = phi i32 [ %543, %541 ], [ %506, %520 ]
  %550 = phi i32 [ %542, %541 ], [ %507, %520 ]
  %551 = icmp eq i64 %524, %521
  br i1 %551, label %580, label %552

; <label>:552:                                    ; preds = %545
  br label %553

; <label>:553:                                    ; preds = %985, %552
  %554 = phi i64 [ %548, %552 ], [ %988, %985 ]
  %555 = phi i32 [ %549, %552 ], [ %987, %985 ]
  %556 = phi i32 [ %550, %552 ], [ %986, %985 ]
  %557 = getelementptr inbounds i32, i32* %2, i64 %554
  %558 = load i32, i32* %557, align 4, !tbaa !28
  %559 = sext i32 %558 to i64
  %560 = getelementptr inbounds i32, i32* %410, i64 %559
  %561 = load i32, i32* %560, align 4, !tbaa !28
  %562 = icmp slt i32 %561, %494
  br i1 %562, label %563, label %565

; <label>:563:                                    ; preds = %553
  %564 = add nsw i32 %555, 1
  br label %570

; <label>:565:                                    ; preds = %553
  %566 = sub nsw i32 %561, %494
  %567 = add nsw i32 %556, 1
  %568 = sext i32 %556 to i64
  %569 = getelementptr inbounds i32, i32* %411, i64 %568
  store i32 %566, i32* %569, align 4, !tbaa !28
  br label %570

; <label>:570:                                    ; preds = %565, %563
  %571 = phi i32 [ %556, %563 ], [ %567, %565 ]
  %572 = phi i32 [ %564, %563 ], [ %555, %565 ]
  %573 = add nsw i64 %554, 1
  %574 = getelementptr inbounds i32, i32* %2, i64 %573
  %575 = load i32, i32* %574, align 4, !tbaa !28
  %576 = sext i32 %575 to i64
  %577 = getelementptr inbounds i32, i32* %410, i64 %576
  %578 = load i32, i32* %577, align 4, !tbaa !28
  %579 = icmp slt i32 %578, %494
  br i1 %579, label %983, label %978

; <label>:580:                                    ; preds = %545, %985, %504
  %581 = phi i32 [ %507, %504 ], [ %546, %545 ], [ %986, %985 ]
  %582 = phi i32 [ %506, %504 ], [ %547, %545 ], [ %987, %985 ]
  %583 = add nsw i64 %505, 1
  %584 = icmp eq i64 %583, %503
  br i1 %584, label %585, label %504

; <label>:585:                                    ; preds = %580, %486
  %586 = phi i32 [ 0, %486 ], [ %581, %580 ]
  %587 = phi i32 [ %489, %486 ], [ %582, %580 ]
  %588 = sext i32 %498 to i64
  %589 = getelementptr inbounds i32, i32* %412, i64 %588
  store i32 %586, i32* %589, align 4, !tbaa !28
  %590 = icmp sgt i32 %490, %586
  %591 = select i1 %590, i32 %490, i32 %586
  %592 = icmp slt i32 %498, 4
  br i1 %592, label %593, label %683

; <label>:593:                                    ; preds = %585
  %594 = icmp sgt i32 %498, 0
  br i1 %594, label %595, label %669

; <label>:595:                                    ; preds = %593
  %596 = zext i32 %498 to i64
  %597 = icmp ult i32 %498, 8
  br i1 %597, label %661, label %598

; <label>:598:                                    ; preds = %595
  %599 = and i64 %596, 4294967288
  %600 = add nsw i64 %599, -8
  %601 = lshr exact i64 %600, 3
  %602 = add nuw nsw i64 %601, 1
  %603 = and i64 %602, 3
  %604 = icmp ult i64 %600, 24
  br i1 %604, label %641, label %605

; <label>:605:                                    ; preds = %598
  %606 = sub nsw i64 %602, %603
  br label %607

; <label>:607:                                    ; preds = %607, %605
  %608 = phi i64 [ 0, %605 ], [ %637, %607 ]
  %609 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %605 ], [ %638, %607 ]
  %610 = phi i64 [ %606, %605 ], [ %639, %607 ]
  %611 = getelementptr inbounds i32, i32* %413, i64 %608
  %612 = add <4 x i32> %609, <i32 4, i32 4, i32 4, i32 4>
  %613 = bitcast i32* %611 to <4 x i32>*
  store <4 x i32> %609, <4 x i32>* %613, align 4, !tbaa !28
  %614 = getelementptr i32, i32* %611, i64 4
  %615 = bitcast i32* %614 to <4 x i32>*
  store <4 x i32> %612, <4 x i32>* %615, align 4, !tbaa !28
  %616 = or i64 %608, 8
  %617 = add <4 x i32> %609, <i32 8, i32 8, i32 8, i32 8>
  %618 = getelementptr inbounds i32, i32* %413, i64 %616
  %619 = add <4 x i32> %609, <i32 12, i32 12, i32 12, i32 12>
  %620 = bitcast i32* %618 to <4 x i32>*
  store <4 x i32> %617, <4 x i32>* %620, align 4, !tbaa !28
  %621 = getelementptr i32, i32* %618, i64 4
  %622 = bitcast i32* %621 to <4 x i32>*
  store <4 x i32> %619, <4 x i32>* %622, align 4, !tbaa !28
  %623 = or i64 %608, 16
  %624 = add <4 x i32> %609, <i32 16, i32 16, i32 16, i32 16>
  %625 = getelementptr inbounds i32, i32* %413, i64 %623
  %626 = add <4 x i32> %609, <i32 20, i32 20, i32 20, i32 20>
  %627 = bitcast i32* %625 to <4 x i32>*
  store <4 x i32> %624, <4 x i32>* %627, align 4, !tbaa !28
  %628 = getelementptr i32, i32* %625, i64 4
  %629 = bitcast i32* %628 to <4 x i32>*
  store <4 x i32> %626, <4 x i32>* %629, align 4, !tbaa !28
  %630 = or i64 %608, 24
  %631 = add <4 x i32> %609, <i32 24, i32 24, i32 24, i32 24>
  %632 = getelementptr inbounds i32, i32* %413, i64 %630
  %633 = add <4 x i32> %609, <i32 28, i32 28, i32 28, i32 28>
  %634 = bitcast i32* %632 to <4 x i32>*
  store <4 x i32> %631, <4 x i32>* %634, align 4, !tbaa !28
  %635 = getelementptr i32, i32* %632, i64 4
  %636 = bitcast i32* %635 to <4 x i32>*
  store <4 x i32> %633, <4 x i32>* %636, align 4, !tbaa !28
  %637 = add i64 %608, 32
  %638 = add <4 x i32> %609, <i32 32, i32 32, i32 32, i32 32>
  %639 = add i64 %610, -4
  %640 = icmp eq i64 %639, 0
  br i1 %640, label %641, label %607, !llvm.loop !48

; <label>:641:                                    ; preds = %607, %598
  %642 = phi i64 [ 0, %598 ], [ %637, %607 ]
  %643 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %598 ], [ %638, %607 ]
  %644 = icmp eq i64 %603, 0
  br i1 %644, label %659, label %645

; <label>:645:                                    ; preds = %641
  br label %646

; <label>:646:                                    ; preds = %646, %645
  %647 = phi i64 [ %642, %645 ], [ %655, %646 ]
  %648 = phi <4 x i32> [ %643, %645 ], [ %656, %646 ]
  %649 = phi i64 [ %603, %645 ], [ %657, %646 ]
  %650 = getelementptr inbounds i32, i32* %413, i64 %647
  %651 = add <4 x i32> %648, <i32 4, i32 4, i32 4, i32 4>
  %652 = bitcast i32* %650 to <4 x i32>*
  store <4 x i32> %648, <4 x i32>* %652, align 4, !tbaa !28
  %653 = getelementptr i32, i32* %650, i64 4
  %654 = bitcast i32* %653 to <4 x i32>*
  store <4 x i32> %651, <4 x i32>* %654, align 4, !tbaa !28
  %655 = add i64 %647, 8
  %656 = add <4 x i32> %648, <i32 8, i32 8, i32 8, i32 8>
  %657 = add i64 %649, -1
  %658 = icmp eq i64 %657, 0
  br i1 %658, label %659, label %646, !llvm.loop !49

; <label>:659:                                    ; preds = %646, %641
  %660 = icmp eq i64 %599, %596
  br i1 %660, label %669, label %661

; <label>:661:                                    ; preds = %659, %595
  %662 = phi i64 [ 0, %595 ], [ %599, %659 ]
  br label %663

; <label>:663:                                    ; preds = %661, %663
  %664 = phi i64 [ %667, %663 ], [ %662, %661 ]
  %665 = getelementptr inbounds i32, i32* %413, i64 %664
  %666 = trunc i64 %664 to i32
  store i32 %666, i32* %665, align 4, !tbaa !28
  %667 = add nuw nsw i64 %664, 1
  %668 = icmp eq i64 %667, %596
  br i1 %668, label %669, label %663, !llvm.loop !50

; <label>:669:                                    ; preds = %663, %659, %593
  %670 = add nsw i32 %498, 1
  %671 = mul nsw i32 %670, %498
  %672 = sdiv i32 %671, 2
  %673 = sitofp i32 %672 to double
  %674 = add nsw i32 %498, -1
  %675 = mul nsw i32 %674, %498
  %676 = sdiv i32 %675, 2
  %677 = shl i32 %498, 1
  %678 = add nsw i32 %677, -1
  %679 = mul nsw i32 %675, %678
  %680 = sdiv i32 %679, 6
  %681 = add nsw i32 %680, %676
  %682 = sitofp i32 %681 to double
  br label %851

; <label>:683:                                    ; preds = %585
  switch i32 %34, label %839 [
    i32 0, label %684
    i32 1, label %709
  ]

; <label>:684:                                    ; preds = %683
  %685 = call i32 @amd_order(i32 %498, i32* nonnull %412, i32* %411, i32* %413, double* null, double* nonnull %474) #3
  %686 = lshr i32 %685, 31
  %687 = xor i32 %686, 1
  %688 = icmp eq i32 %685, -1
  %689 = select i1 %688, i32 -2, i32 %488
  %690 = load i64, i64* %475, align 8, !tbaa !51
  %691 = uitofp i64 %690 to double
  %692 = load i64, i64* %476, align 8, !tbaa !52
  %693 = uitofp i64 %692 to double
  %694 = load double, double* %477, align 8, !tbaa !27
  %695 = fadd double %694, %693
  %696 = fcmp olt double %695, %691
  %697 = select i1 %696, double %691, double %695
  %698 = fptoui double %697 to i64
  store i64 %698, i64* %475, align 8, !tbaa !51
  %699 = load double, double* %478, align 8, !tbaa !27
  %700 = fptosi double %699 to i32
  %701 = add nsw i32 %498, %700
  %702 = sitofp i32 %701 to double
  %703 = load double, double* %479, align 16, !tbaa !27
  %704 = fmul double %703, 2.000000e+00
  %705 = load double, double* %480, align 16, !tbaa !27
  %706 = fadd double %704, %705
  br i1 %590, label %845, label %707

; <label>:707:                                    ; preds = %684
  %708 = load i64, i64* %483, align 8, !tbaa !27
  store i64 %708, i64* %484, align 8, !tbaa !47
  br label %845

; <label>:709:                                    ; preds = %683
  %710 = call i32 @colamd(i32 %498, i32 %498, i32 %47, i32* %411, i32* nonnull %412, double* null, i32* nonnull %481) #3
  %711 = zext i32 %498 to i64
  %712 = icmp ult i32 %498, 8
  br i1 %712, label %800, label %713

; <label>:713:                                    ; preds = %709
  %714 = shl nuw nsw i64 %711, 2
  %715 = getelementptr i8, i8* %397, i64 %714
  %716 = getelementptr i8, i8* %400, i64 %714
  %717 = icmp ult i8* %397, %716
  %718 = icmp ult i8* %400, %715
  %719 = and i1 %717, %718
  br i1 %719, label %800, label %720

; <label>:720:                                    ; preds = %713
  %721 = and i64 %711, 4294967288
  %722 = add nsw i64 %721, -8
  %723 = lshr exact i64 %722, 3
  %724 = add nuw nsw i64 %723, 1
  %725 = and i64 %724, 3
  %726 = icmp ult i64 %722, 24
  br i1 %726, label %778, label %727

; <label>:727:                                    ; preds = %720
  %728 = sub nsw i64 %724, %725
  br label %729

; <label>:729:                                    ; preds = %729, %727
  %730 = phi i64 [ 0, %727 ], [ %775, %729 ]
  %731 = phi i64 [ %728, %727 ], [ %776, %729 ]
  %732 = getelementptr inbounds i32, i32* %412, i64 %730
  %733 = bitcast i32* %732 to <4 x i32>*
  %734 = load <4 x i32>, <4 x i32>* %733, align 4, !tbaa !28, !alias.scope !53
  %735 = getelementptr i32, i32* %732, i64 4
  %736 = bitcast i32* %735 to <4 x i32>*
  %737 = load <4 x i32>, <4 x i32>* %736, align 4, !tbaa !28, !alias.scope !53
  %738 = getelementptr inbounds i32, i32* %413, i64 %730
  %739 = bitcast i32* %738 to <4 x i32>*
  store <4 x i32> %734, <4 x i32>* %739, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %740 = getelementptr i32, i32* %738, i64 4
  %741 = bitcast i32* %740 to <4 x i32>*
  store <4 x i32> %737, <4 x i32>* %741, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %742 = or i64 %730, 8
  %743 = getelementptr inbounds i32, i32* %412, i64 %742
  %744 = bitcast i32* %743 to <4 x i32>*
  %745 = load <4 x i32>, <4 x i32>* %744, align 4, !tbaa !28, !alias.scope !53
  %746 = getelementptr i32, i32* %743, i64 4
  %747 = bitcast i32* %746 to <4 x i32>*
  %748 = load <4 x i32>, <4 x i32>* %747, align 4, !tbaa !28, !alias.scope !53
  %749 = getelementptr inbounds i32, i32* %413, i64 %742
  %750 = bitcast i32* %749 to <4 x i32>*
  store <4 x i32> %745, <4 x i32>* %750, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %751 = getelementptr i32, i32* %749, i64 4
  %752 = bitcast i32* %751 to <4 x i32>*
  store <4 x i32> %748, <4 x i32>* %752, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %753 = or i64 %730, 16
  %754 = getelementptr inbounds i32, i32* %412, i64 %753
  %755 = bitcast i32* %754 to <4 x i32>*
  %756 = load <4 x i32>, <4 x i32>* %755, align 4, !tbaa !28, !alias.scope !53
  %757 = getelementptr i32, i32* %754, i64 4
  %758 = bitcast i32* %757 to <4 x i32>*
  %759 = load <4 x i32>, <4 x i32>* %758, align 4, !tbaa !28, !alias.scope !53
  %760 = getelementptr inbounds i32, i32* %413, i64 %753
  %761 = bitcast i32* %760 to <4 x i32>*
  store <4 x i32> %756, <4 x i32>* %761, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %762 = getelementptr i32, i32* %760, i64 4
  %763 = bitcast i32* %762 to <4 x i32>*
  store <4 x i32> %759, <4 x i32>* %763, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %764 = or i64 %730, 24
  %765 = getelementptr inbounds i32, i32* %412, i64 %764
  %766 = bitcast i32* %765 to <4 x i32>*
  %767 = load <4 x i32>, <4 x i32>* %766, align 4, !tbaa !28, !alias.scope !53
  %768 = getelementptr i32, i32* %765, i64 4
  %769 = bitcast i32* %768 to <4 x i32>*
  %770 = load <4 x i32>, <4 x i32>* %769, align 4, !tbaa !28, !alias.scope !53
  %771 = getelementptr inbounds i32, i32* %413, i64 %764
  %772 = bitcast i32* %771 to <4 x i32>*
  store <4 x i32> %767, <4 x i32>* %772, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %773 = getelementptr i32, i32* %771, i64 4
  %774 = bitcast i32* %773 to <4 x i32>*
  store <4 x i32> %770, <4 x i32>* %774, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %775 = add i64 %730, 32
  %776 = add i64 %731, -4
  %777 = icmp eq i64 %776, 0
  br i1 %777, label %778, label %729, !llvm.loop !58

; <label>:778:                                    ; preds = %729, %720
  %779 = phi i64 [ 0, %720 ], [ %775, %729 ]
  %780 = icmp eq i64 %725, 0
  br i1 %780, label %798, label %781

; <label>:781:                                    ; preds = %778
  br label %782

; <label>:782:                                    ; preds = %782, %781
  %783 = phi i64 [ %779, %781 ], [ %795, %782 ]
  %784 = phi i64 [ %725, %781 ], [ %796, %782 ]
  %785 = getelementptr inbounds i32, i32* %412, i64 %783
  %786 = bitcast i32* %785 to <4 x i32>*
  %787 = load <4 x i32>, <4 x i32>* %786, align 4, !tbaa !28, !alias.scope !53
  %788 = getelementptr i32, i32* %785, i64 4
  %789 = bitcast i32* %788 to <4 x i32>*
  %790 = load <4 x i32>, <4 x i32>* %789, align 4, !tbaa !28, !alias.scope !53
  %791 = getelementptr inbounds i32, i32* %413, i64 %783
  %792 = bitcast i32* %791 to <4 x i32>*
  store <4 x i32> %787, <4 x i32>* %792, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %793 = getelementptr i32, i32* %791, i64 4
  %794 = bitcast i32* %793 to <4 x i32>*
  store <4 x i32> %790, <4 x i32>* %794, align 4, !tbaa !28, !alias.scope !56, !noalias !53
  %795 = add i64 %783, 8
  %796 = add i64 %784, -1
  %797 = icmp eq i64 %796, 0
  br i1 %797, label %798, label %782, !llvm.loop !59

; <label>:798:                                    ; preds = %782, %778
  %799 = icmp eq i64 %721, %711
  br i1 %799, label %845, label %800

; <label>:800:                                    ; preds = %798, %713, %709
  %801 = phi i64 [ 0, %713 ], [ 0, %709 ], [ %721, %798 ]
  %802 = add nsw i64 %711, -1
  %803 = sub nsw i64 %802, %801
  %804 = and i64 %711, 3
  %805 = icmp eq i64 %804, 0
  br i1 %805, label %816, label %806

; <label>:806:                                    ; preds = %800
  br label %807

; <label>:807:                                    ; preds = %807, %806
  %808 = phi i64 [ %813, %807 ], [ %801, %806 ]
  %809 = phi i64 [ %814, %807 ], [ %804, %806 ]
  %810 = getelementptr inbounds i32, i32* %412, i64 %808
  %811 = load i32, i32* %810, align 4, !tbaa !28
  %812 = getelementptr inbounds i32, i32* %413, i64 %808
  store i32 %811, i32* %812, align 4, !tbaa !28
  %813 = add nuw nsw i64 %808, 1
  %814 = add i64 %809, -1
  %815 = icmp eq i64 %814, 0
  br i1 %815, label %816, label %807, !llvm.loop !60

; <label>:816:                                    ; preds = %807, %800
  %817 = phi i64 [ %801, %800 ], [ %813, %807 ]
  %818 = icmp ult i64 %803, 3
  br i1 %818, label %845, label %819

; <label>:819:                                    ; preds = %816
  br label %820

; <label>:820:                                    ; preds = %820, %819
  %821 = phi i64 [ %817, %819 ], [ %837, %820 ]
  %822 = getelementptr inbounds i32, i32* %412, i64 %821
  %823 = load i32, i32* %822, align 4, !tbaa !28
  %824 = getelementptr inbounds i32, i32* %413, i64 %821
  store i32 %823, i32* %824, align 4, !tbaa !28
  %825 = add nuw nsw i64 %821, 1
  %826 = getelementptr inbounds i32, i32* %412, i64 %825
  %827 = load i32, i32* %826, align 4, !tbaa !28
  %828 = getelementptr inbounds i32, i32* %413, i64 %825
  store i32 %827, i32* %828, align 4, !tbaa !28
  %829 = add nsw i64 %821, 2
  %830 = getelementptr inbounds i32, i32* %412, i64 %829
  %831 = load i32, i32* %830, align 4, !tbaa !28
  %832 = getelementptr inbounds i32, i32* %413, i64 %829
  store i32 %831, i32* %832, align 4, !tbaa !28
  %833 = add nsw i64 %821, 3
  %834 = getelementptr inbounds i32, i32* %412, i64 %833
  %835 = load i32, i32* %834, align 4, !tbaa !28
  %836 = getelementptr inbounds i32, i32* %413, i64 %833
  store i32 %835, i32* %836, align 4, !tbaa !28
  %837 = add nsw i64 %821, 4
  %838 = icmp eq i64 %837, %711
  br i1 %838, label %845, label %820, !llvm.loop !61

; <label>:839:                                    ; preds = %683
  %840 = load i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %473, align 8, !tbaa !20
  %841 = call i32 %840(i32 %498, i32* nonnull %412, i32* %411, i32* %413, %struct.klu_common_struct* %3) #3
  %842 = sitofp i32 %841 to double
  %843 = icmp ne i32 %841, 0
  %844 = zext i1 %843 to i32
  br label %845

; <label>:845:                                    ; preds = %816, %820, %798, %839, %707, %684
  %846 = phi double [ %706, %707 ], [ %706, %684 ], [ -1.000000e+00, %839 ], [ -1.000000e+00, %798 ], [ -1.000000e+00, %820 ], [ -1.000000e+00, %816 ]
  %847 = phi double [ %702, %707 ], [ %702, %684 ], [ %842, %839 ], [ -1.000000e+00, %798 ], [ -1.000000e+00, %820 ], [ -1.000000e+00, %816 ]
  %848 = phi i32 [ %687, %707 ], [ %687, %684 ], [ %844, %839 ], [ %710, %798 ], [ %710, %820 ], [ %710, %816 ]
  %849 = phi i32 [ %689, %707 ], [ %689, %684 ], [ %488, %839 ], [ %488, %798 ], [ %488, %820 ], [ %488, %816 ]
  %850 = icmp eq i32 %848, 0
  br i1 %850, label %959, label %851

; <label>:851:                                    ; preds = %845, %669
  %852 = phi i32 [ %488, %669 ], [ %849, %845 ]
  %853 = phi double [ %673, %669 ], [ %847, %845 ]
  %854 = phi double [ %682, %669 ], [ %846, %845 ]
  store double %853, double* %499, align 8, !tbaa !27
  %855 = fcmp oeq double %491, -1.000000e+00
  %856 = fcmp oeq double %853, -1.000000e+00
  %857 = or i1 %855, %856
  %858 = fadd double %491, %853
  %859 = select i1 %857, double -1.000000e+00, double %858
  %860 = fcmp oeq double %492, -1.000000e+00
  %861 = fcmp oeq double %854, -1.000000e+00
  %862 = or i1 %860, %861
  %863 = fadd double %492, %854
  %864 = select i1 %862, double -1.000000e+00, double %863
  %865 = icmp sgt i32 %498, 0
  br i1 %865, label %866, label %949

; <label>:866:                                    ; preds = %851
  %867 = sext i32 %494 to i64
  %868 = zext i32 %498 to i64
  %869 = add nsw i64 %868, -1
  %870 = and i64 %868, 1
  %871 = icmp eq i64 %869, 0
  br i1 %871, label %897, label %872

; <label>:872:                                    ; preds = %866
  %873 = sub nsw i64 %868, %870
  br label %874

; <label>:874:                                    ; preds = %874, %872
  %875 = phi i64 [ 0, %872 ], [ %894, %874 ]
  %876 = phi i64 [ %873, %872 ], [ %895, %874 ]
  %877 = getelementptr inbounds i32, i32* %413, i64 %875
  %878 = load i32, i32* %877, align 4, !tbaa !28
  %879 = add nsw i32 %878, %494
  %880 = sext i32 %879 to i64
  %881 = getelementptr inbounds i32, i32* %52, i64 %880
  %882 = load i32, i32* %881, align 4, !tbaa !28
  %883 = add nsw i64 %875, %867
  %884 = getelementptr inbounds i32, i32* %27, i64 %883
  store i32 %882, i32* %884, align 4, !tbaa !28
  %885 = or i64 %875, 1
  %886 = getelementptr inbounds i32, i32* %413, i64 %885
  %887 = load i32, i32* %886, align 4, !tbaa !28
  %888 = add nsw i32 %887, %494
  %889 = sext i32 %888 to i64
  %890 = getelementptr inbounds i32, i32* %52, i64 %889
  %891 = load i32, i32* %890, align 4, !tbaa !28
  %892 = add nsw i64 %885, %867
  %893 = getelementptr inbounds i32, i32* %27, i64 %892
  store i32 %891, i32* %893, align 4, !tbaa !28
  %894 = add nuw nsw i64 %875, 2
  %895 = add i64 %876, -2
  %896 = icmp eq i64 %895, 0
  br i1 %896, label %897, label %874

; <label>:897:                                    ; preds = %874, %866
  %898 = phi i64 [ 0, %866 ], [ %894, %874 ]
  %899 = icmp eq i64 %870, 0
  br i1 %899, label %909, label %900

; <label>:900:                                    ; preds = %897
  %901 = getelementptr inbounds i32, i32* %413, i64 %898
  %902 = load i32, i32* %901, align 4, !tbaa !28
  %903 = add nsw i32 %902, %494
  %904 = sext i32 %903 to i64
  %905 = getelementptr inbounds i32, i32* %52, i64 %904
  %906 = load i32, i32* %905, align 4, !tbaa !28
  %907 = add nsw i64 %898, %867
  %908 = getelementptr inbounds i32, i32* %27, i64 %907
  store i32 %906, i32* %908, align 4, !tbaa !28
  br label %909

; <label>:909:                                    ; preds = %897, %900
  %910 = and i64 %868, 1
  %911 = icmp eq i64 %869, 0
  br i1 %911, label %937, label %912

; <label>:912:                                    ; preds = %909
  %913 = sub nsw i64 %868, %910
  br label %914

; <label>:914:                                    ; preds = %914, %912
  %915 = phi i64 [ 0, %912 ], [ %934, %914 ]
  %916 = phi i64 [ %913, %912 ], [ %935, %914 ]
  %917 = getelementptr inbounds i32, i32* %413, i64 %915
  %918 = load i32, i32* %917, align 4, !tbaa !28
  %919 = add nsw i32 %918, %494
  %920 = sext i32 %919 to i64
  %921 = getelementptr inbounds i32, i32* %50, i64 %920
  %922 = load i32, i32* %921, align 4, !tbaa !28
  %923 = add nsw i64 %915, %867
  %924 = getelementptr inbounds i32, i32* %25, i64 %923
  store i32 %922, i32* %924, align 4, !tbaa !28
  %925 = or i64 %915, 1
  %926 = getelementptr inbounds i32, i32* %413, i64 %925
  %927 = load i32, i32* %926, align 4, !tbaa !28
  %928 = add nsw i32 %927, %494
  %929 = sext i32 %928 to i64
  %930 = getelementptr inbounds i32, i32* %50, i64 %929
  %931 = load i32, i32* %930, align 4, !tbaa !28
  %932 = add nsw i64 %925, %867
  %933 = getelementptr inbounds i32, i32* %25, i64 %932
  store i32 %931, i32* %933, align 4, !tbaa !28
  %934 = add nuw nsw i64 %915, 2
  %935 = add i64 %916, -2
  %936 = icmp eq i64 %935, 0
  br i1 %936, label %937, label %914

; <label>:937:                                    ; preds = %914, %909
  %938 = phi i64 [ 0, %909 ], [ %934, %914 ]
  %939 = icmp eq i64 %910, 0
  br i1 %939, label %949, label %940

; <label>:940:                                    ; preds = %937
  %941 = getelementptr inbounds i32, i32* %413, i64 %938
  %942 = load i32, i32* %941, align 4, !tbaa !28
  %943 = add nsw i32 %942, %494
  %944 = sext i32 %943 to i64
  %945 = getelementptr inbounds i32, i32* %50, i64 %944
  %946 = load i32, i32* %945, align 4, !tbaa !28
  %947 = add nsw i64 %938, %867
  %948 = getelementptr inbounds i32, i32* %25, i64 %947
  store i32 %946, i32* %948, align 4, !tbaa !28
  br label %949

; <label>:949:                                    ; preds = %940, %937, %851
  %950 = icmp slt i64 %495, %485
  br i1 %950, label %486, label %951

; <label>:951:                                    ; preds = %949, %469
  %952 = phi double [ 0.000000e+00, %469 ], [ %864, %949 ]
  %953 = phi double [ 0.000000e+00, %469 ], [ %859, %949 ]
  %954 = phi i32 [ 0, %469 ], [ %587, %949 ]
  %955 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 2
  store double %953, double* %955, align 8, !tbaa !62
  %956 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 3
  store double %953, double* %956, align 8, !tbaa !63
  %957 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 10
  store i32 %954, i32* %957, align 8, !tbaa !64
  %958 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %414, i64 0, i32 1
  store double %952, double* %958, align 8, !tbaa !65
  br label %959

; <label>:959:                                    ; preds = %845, %951
  %960 = phi i32 [ 0, %951 ], [ %849, %845 ]
  call void @llvm.lifetime.end.p0i8(i64 80, i8* nonnull %416) #3
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %415) #3
  store i32 %960, i32* %11, align 4, !tbaa !3
  br label %961

; <label>:961:                                    ; preds = %959, %390
  %962 = call i8* @klu_free(i8* %397, i64 %396, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %963 = call i8* @klu_free(i8* %400, i64 %399, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %964 = call i8* @klu_free(i8* %405, i64 %404, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %965 = call i8* @klu_free(i8* %406, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %966 = call i8* @klu_free(i8* %49, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %967 = call i8* @klu_free(i8* %51, i64 %48, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %968 = load i32, i32* %11, align 4, !tbaa !3
  %969 = icmp slt i32 %968, 0
  br i1 %969, label %970, label %972

; <label>:970:                                    ; preds = %961
  %971 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %8, %struct.klu_common_struct* nonnull %3) #3
  br label %972

; <label>:972:                                    ; preds = %970, %961
  %973 = load %struct.klu_symbolic*, %struct.klu_symbolic** %8, align 8, !tbaa !13
  br label %974

; <label>:974:                                    ; preds = %18, %44, %55, %74, %972
  %975 = phi %struct.klu_symbolic* [ null, %55 ], [ null, %74 ], [ %973, %972 ], [ null, %44 ], [ null, %18 ]
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %20) #3
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %19) #3
  br label %976

; <label>:976:                                    ; preds = %4, %974, %16
  %977 = phi %struct.klu_symbolic* [ %17, %16 ], [ %975, %974 ], [ null, %4 ]
  ret %struct.klu_symbolic* %977

; <label>:978:                                    ; preds = %570
  %979 = sub nsw i32 %578, %494
  %980 = add nsw i32 %571, 1
  %981 = sext i32 %571 to i64
  %982 = getelementptr inbounds i32, i32* %411, i64 %981
  store i32 %979, i32* %982, align 4, !tbaa !28
  br label %985

; <label>:983:                                    ; preds = %570
  %984 = add nsw i32 %572, 1
  br label %985

; <label>:985:                                    ; preds = %983, %978
  %986 = phi i32 [ %571, %983 ], [ %980, %978 ]
  %987 = phi i32 [ %984, %983 ], [ %572, %978 ]
  %988 = add nsw i64 %554, 2
  %989 = icmp eq i64 %988, %522
  br i1 %989, label %580, label %553
}

declare %struct.klu_symbolic* @klu_analyze_given(i32, i32*, i32*, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #2

declare %struct.klu_symbolic* @klu_alloc_symbolic(i32, i32*, i32*, %struct.klu_common_struct*) local_unnamed_addr #1

declare i64 @colamd_recommended(i32, i32, i32) local_unnamed_addr #1

declare i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #1

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

declare i32 @btf_order(i32, i32*, i32*, double, double*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #2

declare i32 @amd_order(i32, i32*, i32*, i32*, double*, double*) local_unnamed_addr #1

declare i32 @colamd(i32, i32, i32, i32*, i32*, double*, i32*) local_unnamed_addr #1

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
!11 = !{!4, !8, i64 84}
!12 = !{!4, !8, i64 44}
!13 = !{!9, !9, i64 0}
!14 = !{!15, !9, i64 48}
!15 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!16 = !{!15, !9, i64 56}
!17 = !{!15, !9, i64 64}
!18 = !{!15, !9, i64 32}
!19 = !{!15, !8, i64 44}
!20 = !{!4, !9, i64 56}
!21 = !{!4, !8, i64 40}
!22 = !{!15, !8, i64 84}
!23 = !{!15, !8, i64 88}
!24 = !{!15, !8, i64 92}
!25 = !{!4, !5, i64 136}
!26 = !{!4, !5, i64 32}
!27 = !{!5, !5, i64 0}
!28 = !{!8, !8, i64 0}
!29 = distinct !{!29, !30}
!30 = !{!"llvm.loop.isvectorized", i32 1}
!31 = distinct !{!31, !32, !30}
!32 = !{!"llvm.loop.unroll.runtime.disable"}
!33 = distinct !{!33, !30}
!34 = distinct !{!34, !32, !30}
!35 = !{!36}
!36 = distinct !{!36, !37}
!37 = distinct !{!37, !"LVerDomain"}
!38 = !{!39}
!39 = distinct !{!39, !37}
!40 = distinct !{!40, !30}
!41 = distinct !{!41, !42}
!42 = !{!"llvm.loop.unroll.disable"}
!43 = distinct !{!43, !30}
!44 = !{!15, !8, i64 76}
!45 = !{!15, !8, i64 80}
!46 = distinct !{!46, !42}
!47 = !{!15, !5, i64 0}
!48 = distinct !{!48, !30}
!49 = distinct !{!49, !42}
!50 = distinct !{!50, !32, !30}
!51 = !{!4, !10, i64 152}
!52 = !{!4, !10, i64 144}
!53 = !{!54}
!54 = distinct !{!54, !55}
!55 = distinct !{!55, !"LVerDomain"}
!56 = !{!57}
!57 = distinct !{!57, !55}
!58 = distinct !{!58, !30}
!59 = distinct !{!59, !42}
!60 = distinct !{!60, !42}
!61 = distinct !{!61, !30}
!62 = !{!15, !5, i64 16}
!63 = !{!15, !5, i64 24}
!64 = !{!15, !8, i64 72}
!65 = !{!15, !5, i64 8}
