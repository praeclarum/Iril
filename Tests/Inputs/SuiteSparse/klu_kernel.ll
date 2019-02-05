; ModuleID = 'klu_kernel.c'
source_filename = "klu_kernel.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i64 @klu_kernel(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32* nocapture readonly, i64, i32* nocapture, i32* nocapture, double** nocapture, double* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, double* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32, i32* nocapture readonly, double* nocapture readonly, i32* nocapture, i32* nocapture, double* nocapture, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %29 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 7
  %30 = load i32, i32* %29, align 8, !tbaa !3
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 0
  %32 = load double, double* %31, align 8, !tbaa !11
  %33 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 1
  %34 = load double, double* %33, align 8, !tbaa !12
  store i32 0, i32* %14, align 4, !tbaa !13
  store i32 0, i32* %15, align 4, !tbaa !13
  %35 = load double*, double** %8, align 8, !tbaa !14
  %36 = bitcast double* %35 to i8*
  %37 = icmp sgt i32 %0, 0
  br i1 %37, label %38, label %251

; <label>:38:                                     ; preds = %28
  %39 = zext i32 %0 to i64
  %40 = icmp ult i32 %0, 4
  br i1 %40, label %105, label %41

; <label>:41:                                     ; preds = %38
  %42 = getelementptr i32, i32* %18, i64 %39
  %43 = getelementptr i32, i32* %20, i64 %39
  %44 = icmp ugt i32* %43, %18
  %45 = icmp ugt i32* %42, %20
  %46 = and i1 %44, %45
  br i1 %46, label %105, label %47

; <label>:47:                                     ; preds = %41
  %48 = and i64 %39, 4294967292
  %49 = add nsw i64 %48, -4
  %50 = lshr exact i64 %49, 2
  %51 = add nuw nsw i64 %50, 1
  %52 = and i64 %51, 1
  %53 = icmp eq i64 %49, 0
  br i1 %53, label %87, label %54

; <label>:54:                                     ; preds = %47
  %55 = sub nsw i64 %51, %52
  br label %56

; <label>:56:                                     ; preds = %56, %54
  %57 = phi i64 [ 0, %54 ], [ %84, %56 ]
  %58 = phi i64 [ %55, %54 ], [ %85, %56 ]
  %59 = getelementptr inbounds double, double* %16, i64 %57
  %60 = bitcast double* %59 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %60, align 8, !tbaa !15, !alias.scope !16
  %61 = getelementptr double, double* %59, i64 2
  %62 = bitcast double* %61 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %62, align 8, !tbaa !15, !alias.scope !16
  %63 = getelementptr inbounds i32, i32* %18, i64 %57
  %64 = bitcast i32* %63 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %64, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %65 = getelementptr i32, i32* %63, i64 2
  %66 = bitcast i32* %65 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %66, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %67 = getelementptr inbounds i32, i32* %20, i64 %57
  %68 = bitcast i32* %67 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %68, align 4, !tbaa !13, !alias.scope !21
  %69 = getelementptr i32, i32* %67, i64 2
  %70 = bitcast i32* %69 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %70, align 4, !tbaa !13, !alias.scope !21
  %71 = or i64 %57, 4
  %72 = getelementptr inbounds double, double* %16, i64 %71
  %73 = bitcast double* %72 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %73, align 8, !tbaa !15, !alias.scope !16
  %74 = getelementptr double, double* %72, i64 2
  %75 = bitcast double* %74 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %75, align 8, !tbaa !15, !alias.scope !16
  %76 = getelementptr inbounds i32, i32* %18, i64 %71
  %77 = bitcast i32* %76 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %77, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %78 = getelementptr i32, i32* %76, i64 2
  %79 = bitcast i32* %78 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %79, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %80 = getelementptr inbounds i32, i32* %20, i64 %71
  %81 = bitcast i32* %80 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %81, align 4, !tbaa !13, !alias.scope !21
  %82 = getelementptr i32, i32* %80, i64 2
  %83 = bitcast i32* %82 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %83, align 4, !tbaa !13, !alias.scope !21
  %84 = add i64 %57, 8
  %85 = add i64 %58, -2
  %86 = icmp eq i64 %85, 0
  br i1 %86, label %87, label %56, !llvm.loop !23

; <label>:87:                                     ; preds = %56, %47
  %88 = phi i64 [ 0, %47 ], [ %84, %56 ]
  %89 = icmp eq i64 %52, 0
  br i1 %89, label %103, label %90

; <label>:90:                                     ; preds = %87
  %91 = getelementptr inbounds double, double* %16, i64 %88
  %92 = bitcast double* %91 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %92, align 8, !tbaa !15, !alias.scope !16
  %93 = getelementptr double, double* %91, i64 2
  %94 = bitcast double* %93 to <2 x double>*
  store <2 x double> zeroinitializer, <2 x double>* %94, align 8, !tbaa !15, !alias.scope !16
  %95 = getelementptr inbounds i32, i32* %18, i64 %88
  %96 = bitcast i32* %95 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %96, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %97 = getelementptr i32, i32* %95, i64 2
  %98 = bitcast i32* %97 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %98, align 4, !tbaa !13, !alias.scope !19, !noalias !21
  %99 = getelementptr inbounds i32, i32* %20, i64 %88
  %100 = bitcast i32* %99 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %100, align 4, !tbaa !13, !alias.scope !21
  %101 = getelementptr i32, i32* %99, i64 2
  %102 = bitcast i32* %101 to <2 x i32>*
  store <2 x i32> <i32 -1, i32 -1>, <2 x i32>* %102, align 4, !tbaa !13, !alias.scope !21
  br label %103

; <label>:103:                                    ; preds = %87, %90
  %104 = icmp eq i64 %48, %39
  br i1 %104, label %130, label %105

; <label>:105:                                    ; preds = %103, %41, %38
  %106 = phi i64 [ 0, %41 ], [ 0, %38 ], [ %48, %103 ]
  %107 = add nsw i64 %39, -1
  %108 = and i64 %39, 1
  %109 = icmp eq i64 %108, 0
  br i1 %109, label %115, label %110

; <label>:110:                                    ; preds = %105
  %111 = getelementptr inbounds double, double* %16, i64 %106
  store double 0.000000e+00, double* %111, align 8, !tbaa !15
  %112 = getelementptr inbounds i32, i32* %18, i64 %106
  store i32 -1, i32* %112, align 4, !tbaa !13
  %113 = getelementptr inbounds i32, i32* %20, i64 %106
  store i32 -1, i32* %113, align 4, !tbaa !13
  %114 = or i64 %106, 1
  br label %115

; <label>:115:                                    ; preds = %105, %110
  %116 = phi i64 [ %106, %105 ], [ %114, %110 ]
  %117 = icmp eq i64 %107, %106
  br i1 %117, label %130, label %118

; <label>:118:                                    ; preds = %115
  br label %119

; <label>:119:                                    ; preds = %119, %118
  %120 = phi i64 [ %116, %118 ], [ %128, %119 ]
  %121 = getelementptr inbounds double, double* %16, i64 %120
  store double 0.000000e+00, double* %121, align 8, !tbaa !15
  %122 = getelementptr inbounds i32, i32* %18, i64 %120
  store i32 -1, i32* %122, align 4, !tbaa !13
  %123 = getelementptr inbounds i32, i32* %20, i64 %120
  store i32 -1, i32* %123, align 4, !tbaa !13
  %124 = add nuw nsw i64 %120, 1
  %125 = getelementptr inbounds double, double* %16, i64 %124
  store double 0.000000e+00, double* %125, align 8, !tbaa !15
  %126 = getelementptr inbounds i32, i32* %18, i64 %124
  store i32 -1, i32* %126, align 4, !tbaa !13
  %127 = getelementptr inbounds i32, i32* %20, i64 %124
  store i32 -1, i32* %127, align 4, !tbaa !13
  %128 = add nsw i64 %120, 2
  %129 = icmp eq i64 %128, %39
  br i1 %129, label %130, label %119, !llvm.loop !25

; <label>:130:                                    ; preds = %115, %119, %103
  br i1 %37, label %131, label %251

; <label>:131:                                    ; preds = %130
  %132 = zext i32 %0 to i64
  %133 = icmp ult i32 %0, 8
  br i1 %133, label %202, label %134

; <label>:134:                                    ; preds = %131
  %135 = getelementptr i32, i32* %7, i64 %39
  %136 = getelementptr i32, i32* %6, i64 %39
  %137 = icmp ugt i32* %136, %7
  %138 = icmp ugt i32* %135, %6
  %139 = and i1 %137, %138
  br i1 %139, label %202, label %140

; <label>:140:                                    ; preds = %134
  %141 = and i64 %39, 4294967288
  %142 = add nsw i64 %141, -8
  %143 = lshr exact i64 %142, 3
  %144 = add nuw nsw i64 %143, 1
  %145 = and i64 %144, 1
  %146 = icmp eq i64 %142, 0
  br i1 %146, label %183, label %147

; <label>:147:                                    ; preds = %140
  %148 = sub nsw i64 %144, %145
  br label %149

; <label>:149:                                    ; preds = %149, %147
  %150 = phi i64 [ 0, %147 ], [ %178, %149 ]
  %151 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %147 ], [ %179, %149 ]
  %152 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %147 ], [ %180, %149 ]
  %153 = phi i64 [ %148, %147 ], [ %181, %149 ]
  %154 = getelementptr inbounds i32, i32* %7, i64 %150
  %155 = add <4 x i32> %151, <i32 4, i32 4, i32 4, i32 4>
  %156 = bitcast i32* %154 to <4 x i32>*
  store <4 x i32> %151, <4 x i32>* %156, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %157 = getelementptr i32, i32* %154, i64 4
  %158 = bitcast i32* %157 to <4 x i32>*
  store <4 x i32> %155, <4 x i32>* %158, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %159 = getelementptr inbounds i32, i32* %6, i64 %150
  %160 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %152
  %161 = sub <4 x i32> <i32 -6, i32 -6, i32 -6, i32 -6>, %152
  %162 = bitcast i32* %159 to <4 x i32>*
  store <4 x i32> %160, <4 x i32>* %162, align 4, !tbaa !13, !alias.scope !29
  %163 = getelementptr i32, i32* %159, i64 4
  %164 = bitcast i32* %163 to <4 x i32>*
  store <4 x i32> %161, <4 x i32>* %164, align 4, !tbaa !13, !alias.scope !29
  %165 = or i64 %150, 8
  %166 = add <4 x i32> %151, <i32 8, i32 8, i32 8, i32 8>
  %167 = getelementptr inbounds i32, i32* %7, i64 %165
  %168 = add <4 x i32> %151, <i32 12, i32 12, i32 12, i32 12>
  %169 = bitcast i32* %167 to <4 x i32>*
  store <4 x i32> %166, <4 x i32>* %169, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %170 = getelementptr i32, i32* %167, i64 4
  %171 = bitcast i32* %170 to <4 x i32>*
  store <4 x i32> %168, <4 x i32>* %171, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %172 = getelementptr inbounds i32, i32* %6, i64 %165
  %173 = sub <4 x i32> <i32 -10, i32 -10, i32 -10, i32 -10>, %152
  %174 = sub <4 x i32> <i32 -14, i32 -14, i32 -14, i32 -14>, %152
  %175 = bitcast i32* %172 to <4 x i32>*
  store <4 x i32> %173, <4 x i32>* %175, align 4, !tbaa !13, !alias.scope !29
  %176 = getelementptr i32, i32* %172, i64 4
  %177 = bitcast i32* %176 to <4 x i32>*
  store <4 x i32> %174, <4 x i32>* %177, align 4, !tbaa !13, !alias.scope !29
  %178 = add i64 %150, 16
  %179 = add <4 x i32> %151, <i32 16, i32 16, i32 16, i32 16>
  %180 = add <4 x i32> %152, <i32 16, i32 16, i32 16, i32 16>
  %181 = add i64 %153, -2
  %182 = icmp eq i64 %181, 0
  br i1 %182, label %183, label %149, !llvm.loop !31

; <label>:183:                                    ; preds = %149, %140
  %184 = phi i64 [ 0, %140 ], [ %178, %149 ]
  %185 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %140 ], [ %179, %149 ]
  %186 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %140 ], [ %180, %149 ]
  %187 = icmp eq i64 %145, 0
  br i1 %187, label %200, label %188

; <label>:188:                                    ; preds = %183
  %189 = getelementptr inbounds i32, i32* %7, i64 %184
  %190 = add <4 x i32> %185, <i32 4, i32 4, i32 4, i32 4>
  %191 = bitcast i32* %189 to <4 x i32>*
  store <4 x i32> %185, <4 x i32>* %191, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %192 = getelementptr i32, i32* %189, i64 4
  %193 = bitcast i32* %192 to <4 x i32>*
  store <4 x i32> %190, <4 x i32>* %193, align 4, !tbaa !13, !alias.scope !26, !noalias !29
  %194 = getelementptr inbounds i32, i32* %6, i64 %184
  %195 = sub <4 x i32> <i32 -2, i32 -2, i32 -2, i32 -2>, %186
  %196 = sub <4 x i32> <i32 -6, i32 -6, i32 -6, i32 -6>, %186
  %197 = bitcast i32* %194 to <4 x i32>*
  store <4 x i32> %195, <4 x i32>* %197, align 4, !tbaa !13, !alias.scope !29
  %198 = getelementptr i32, i32* %194, i64 4
  %199 = bitcast i32* %198 to <4 x i32>*
  store <4 x i32> %196, <4 x i32>* %199, align 4, !tbaa !13, !alias.scope !29
  br label %200

; <label>:200:                                    ; preds = %183, %188
  %201 = icmp eq i64 %141, %39
  br i1 %201, label %252, label %202

; <label>:202:                                    ; preds = %200, %134, %131
  %203 = phi i64 [ 0, %134 ], [ 0, %131 ], [ %141, %200 ]
  %204 = add nsw i64 %132, -1
  %205 = sub nsw i64 %204, %203
  %206 = and i64 %132, 3
  %207 = icmp eq i64 %206, 0
  br i1 %207, label %220, label %208

; <label>:208:                                    ; preds = %202
  br label %209

; <label>:209:                                    ; preds = %209, %208
  %210 = phi i64 [ %217, %209 ], [ %203, %208 ]
  %211 = phi i64 [ %218, %209 ], [ %206, %208 ]
  %212 = getelementptr inbounds i32, i32* %7, i64 %210
  %213 = trunc i64 %210 to i32
  store i32 %213, i32* %212, align 4, !tbaa !13
  %214 = getelementptr inbounds i32, i32* %6, i64 %210
  %215 = trunc i64 %210 to i32
  %216 = sub i32 -2, %215
  store i32 %216, i32* %214, align 4, !tbaa !13
  %217 = add nuw nsw i64 %210, 1
  %218 = add i64 %211, -1
  %219 = icmp eq i64 %218, 0
  br i1 %219, label %220, label %209, !llvm.loop !32

; <label>:220:                                    ; preds = %209, %202
  %221 = phi i64 [ %203, %202 ], [ %217, %209 ]
  %222 = icmp ult i64 %205, 3
  br i1 %222, label %252, label %223

; <label>:223:                                    ; preds = %220
  br label %224

; <label>:224:                                    ; preds = %224, %223
  %225 = phi i64 [ %221, %223 ], [ %249, %224 ]
  %226 = getelementptr inbounds i32, i32* %7, i64 %225
  %227 = trunc i64 %225 to i32
  store i32 %227, i32* %226, align 4, !tbaa !13
  %228 = getelementptr inbounds i32, i32* %6, i64 %225
  %229 = trunc i64 %225 to i32
  %230 = sub i32 -2, %229
  store i32 %230, i32* %228, align 4, !tbaa !13
  %231 = add nuw nsw i64 %225, 1
  %232 = getelementptr inbounds i32, i32* %7, i64 %231
  %233 = trunc i64 %231 to i32
  store i32 %233, i32* %232, align 4, !tbaa !13
  %234 = getelementptr inbounds i32, i32* %6, i64 %231
  %235 = trunc i64 %231 to i32
  %236 = sub i32 -2, %235
  store i32 %236, i32* %234, align 4, !tbaa !13
  %237 = add nsw i64 %225, 2
  %238 = getelementptr inbounds i32, i32* %7, i64 %237
  %239 = trunc i64 %237 to i32
  store i32 %239, i32* %238, align 4, !tbaa !13
  %240 = getelementptr inbounds i32, i32* %6, i64 %237
  %241 = trunc i64 %237 to i32
  %242 = sub i32 -2, %241
  store i32 %242, i32* %240, align 4, !tbaa !13
  %243 = add nsw i64 %225, 3
  %244 = getelementptr inbounds i32, i32* %7, i64 %243
  %245 = trunc i64 %243 to i32
  store i32 %245, i32* %244, align 4, !tbaa !13
  %246 = getelementptr inbounds i32, i32* %6, i64 %243
  %247 = trunc i64 %243 to i32
  %248 = sub i32 -2, %247
  store i32 %248, i32* %246, align 4, !tbaa !13
  %249 = add nsw i64 %225, 4
  %250 = icmp eq i64 %249, %132
  br i1 %250, label %252, label %224, !llvm.loop !34

; <label>:251:                                    ; preds = %130, %28
  store i32 0, i32* %24, align 4, !tbaa !13
  br label %1036

; <label>:252:                                    ; preds = %220, %224, %200
  store i32 0, i32* %24, align 4, !tbaa !13
  br i1 %37, label %253, label %1036

; <label>:253:                                    ; preds = %252
  %254 = sitofp i32 %0 to double
  %255 = shl nsw i32 %0, 2
  %256 = sitofp i32 %255 to double
  %257 = shl nsw i32 %0, 1
  %258 = sitofp i32 %257 to double
  %259 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 12
  %260 = bitcast double** %8 to i8**
  %261 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 11
  %262 = icmp slt i32 %30, 1
  %263 = sext i32 %0 to i64
  %264 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 10
  %265 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 14
  %266 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 15
  %267 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %27, i64 0, i32 16
  %268 = sext i32 %21 to i64
  br label %269

; <label>:269:                                    ; preds = %253, %996
  %270 = phi i64 [ 0, %253 ], [ %1005, %996 ]
  %271 = phi i64 [ %5, %253 ], [ %325, %996 ]
  %272 = phi double* [ %35, %253 ], [ %324, %996 ]
  %273 = phi i8* [ %36, %253 ], [ %323, %996 ]
  %274 = phi i32 [ 0, %253 ], [ %904, %996 ]
  %275 = phi i32 [ 0, %253 ], [ %807, %996 ]
  %276 = phi i32 [ 0, %253 ], [ %808, %996 ]
  %277 = phi i64 [ 0, %253 ], [ %809, %996 ]
  %278 = trunc i64 %270 to i32
  %279 = sitofp i32 %278 to double
  %280 = fsub double %254, %279
  %281 = insertelement <2 x double> undef, double %279, i32 0
  %282 = insertelement <2 x double> %281, double %280, i32 1
  %283 = fmul <2 x double> %282, <double 4.000000e+00, double 4.000000e+00>
  %284 = fmul <2 x double> %283, <double 1.250000e-01, double 1.250000e-01>
  %285 = call <2 x double> @llvm.ceil.v2f64(<2 x double> %284)
  %286 = extractelement <2 x double> %285, i32 0
  %287 = extractelement <2 x double> %285, i32 1
  %288 = fadd double %286, %287
  %289 = fmul double %280, 8.000000e+00
  %290 = fmul double %289, 1.250000e-01
  %291 = tail call double @llvm.ceil.f64(double %290)
  %292 = fadd double %291, %288
  %293 = fmul double %279, 8.000000e+00
  %294 = fmul double %293, 1.250000e-01
  %295 = tail call double @llvm.ceil.f64(double %294)
  %296 = fadd double %295, %292
  %297 = sitofp i32 %274 to double
  %298 = fadd double %296, %297
  %299 = uitofp i64 %271 to double
  %300 = fcmp ogt double %298, %299
  br i1 %300, label %301, label %322

; <label>:301:                                    ; preds = %269
  %302 = fmul double %34, %299
  %303 = fadd double %302, %256
  %304 = fadd double %303, 1.000000e+00
  %305 = fmul double %304, 0x3FF0000002AF31DC
  %306 = fcmp ugt double %305, 0x41DFFFFFFFC00000
  %307 = fcmp uno double %304, 0.000000e+00
  %308 = or i1 %307, %306
  br i1 %308, label %309, label %310

; <label>:309:                                    ; preds = %301
  store i32 -4, i32* %261, align 4, !tbaa !35
  br label %1042

; <label>:310:                                    ; preds = %301
  %311 = fadd double %302, %258
  %312 = fadd double %311, 1.000000e+00
  %313 = fptoui double %312 to i64
  %314 = bitcast double* %272 to i8*
  %315 = tail call i8* @klu_realloc(i64 %313, i64 %271, i64 8, i8* %314, %struct.klu_common_struct* %27) #3
  %316 = load i32, i32* %259, align 8, !tbaa !36
  %317 = add nsw i32 %316, 1
  store i32 %317, i32* %259, align 8, !tbaa !36
  store i8* %315, i8** %260, align 8, !tbaa !14
  %318 = load i32, i32* %261, align 4, !tbaa !35
  %319 = icmp eq i32 %318, -2
  br i1 %319, label %1042, label %320

; <label>:320:                                    ; preds = %310
  %321 = bitcast i8* %315 to double*
  br label %322

; <label>:322:                                    ; preds = %320, %269
  %323 = phi i8* [ %315, %320 ], [ %273, %269 ]
  %324 = phi double* [ %321, %320 ], [ %272, %269 ]
  %325 = phi i64 [ %313, %320 ], [ %271, %269 ]
  %326 = getelementptr inbounds i32, i32* %12, i64 %270
  store i32 %274, i32* %326, align 4, !tbaa !13
  %327 = sext i32 %274 to i64
  %328 = getelementptr inbounds double, double* %324, i64 %327
  %329 = bitcast double* %328 to i32*
  %330 = add nsw i64 %270, %268
  %331 = getelementptr inbounds i32, i32* %4, i64 %330
  %332 = load i32, i32* %331, align 4, !tbaa !13
  %333 = add nsw i32 %332, 1
  %334 = sext i32 %333 to i64
  %335 = getelementptr inbounds i32, i32* %1, i64 %334
  %336 = load i32, i32* %335, align 4, !tbaa !13
  %337 = sext i32 %332 to i64
  %338 = getelementptr inbounds i32, i32* %1, i64 %337
  %339 = load i32, i32* %338, align 4, !tbaa !13
  %340 = icmp slt i32 %339, %336
  br i1 %340, label %341, label %468

; <label>:341:                                    ; preds = %322
  %342 = sext i32 %339 to i64
  %343 = sext i32 %336 to i64
  br label %344

; <label>:344:                                    ; preds = %463, %341
  %345 = phi i64 [ %342, %341 ], [ %466, %463 ]
  %346 = phi i32 [ %0, %341 ], [ %465, %463 ]
  %347 = phi i32 [ 0, %341 ], [ %464, %463 ]
  %348 = getelementptr inbounds i32, i32* %2, i64 %345
  %349 = load i32, i32* %348, align 4, !tbaa !13
  %350 = sext i32 %349 to i64
  %351 = getelementptr inbounds i32, i32* %22, i64 %350
  %352 = load i32, i32* %351, align 4, !tbaa !13
  %353 = sub nsw i32 %352, %21
  %354 = icmp slt i32 %353, 0
  br i1 %354, label %463, label %355

; <label>:355:                                    ; preds = %344
  %356 = sext i32 %353 to i64
  %357 = getelementptr inbounds i32, i32* %18, i64 %356
  %358 = load i32, i32* %357, align 4, !tbaa !13
  %359 = zext i32 %358 to i64
  %360 = icmp eq i64 %270, %359
  br i1 %360, label %463, label %361

; <label>:361:                                    ; preds = %355
  %362 = getelementptr inbounds i32, i32* %6, i64 %356
  %363 = load i32, i32* %362, align 4, !tbaa !13
  %364 = icmp sgt i32 %363, -1
  br i1 %364, label %365, label %459

; <label>:365:                                    ; preds = %361
  store i32 %353, i32* %17, align 4, !tbaa !13
  br label %366

; <label>:366:                                    ; preds = %455, %365
  %367 = phi i32 [ %353, %365 ], [ %458, %455 ]
  %368 = phi i32 [ %347, %365 ], [ %451, %455 ]
  %369 = phi i32 [ 0, %365 ], [ %453, %455 ]
  %370 = phi i32 [ %346, %365 ], [ %452, %455 ]
  %371 = sext i32 %369 to i64
  %372 = sext i32 %367 to i64
  %373 = getelementptr inbounds i32, i32* %6, i64 %372
  %374 = load i32, i32* %373, align 4, !tbaa !13
  %375 = getelementptr inbounds i32, i32* %18, i64 %372
  %376 = load i32, i32* %375, align 4, !tbaa !13
  %377 = zext i32 %376 to i64
  %378 = icmp eq i64 %270, %377
  br i1 %378, label %379, label %383

; <label>:379:                                    ; preds = %366
  %380 = getelementptr inbounds i32, i32* %19, i64 %371
  %381 = load i32, i32* %380, align 4, !tbaa !13
  %382 = sext i32 %374 to i64
  br label %394

; <label>:383:                                    ; preds = %366
  store i32 %278, i32* %375, align 4, !tbaa !13
  %384 = sext i32 %374 to i64
  %385 = getelementptr inbounds i32, i32* %20, i64 %384
  %386 = load i32, i32* %385, align 4, !tbaa !13
  %387 = icmp eq i32 %386, -1
  br i1 %387, label %388, label %391

; <label>:388:                                    ; preds = %383
  %389 = getelementptr inbounds i32, i32* %10, i64 %384
  %390 = load i32, i32* %389, align 4, !tbaa !13
  br label %391

; <label>:391:                                    ; preds = %388, %383
  %392 = phi i32 [ %390, %388 ], [ %386, %383 ]
  %393 = getelementptr inbounds i32, i32* %19, i64 %371
  store i32 %392, i32* %393, align 4, !tbaa !13
  br label %394

; <label>:394:                                    ; preds = %391, %379
  %395 = phi i32* [ %380, %379 ], [ %393, %391 ]
  %396 = phi i64 [ %382, %379 ], [ %384, %391 ]
  %397 = phi i32 [ %381, %379 ], [ %392, %391 ]
  %398 = getelementptr inbounds i32, i32* %12, i64 %396
  %399 = load i32, i32* %398, align 4, !tbaa !13
  %400 = sext i32 %399 to i64
  %401 = getelementptr inbounds double, double* %324, i64 %400
  %402 = bitcast double* %401 to i32*
  %403 = add nsw i32 %397, -1
  store i32 %403, i32* %395, align 4, !tbaa !13
  %404 = icmp sgt i32 %397, 0
  br i1 %404, label %405, label %435

; <label>:405:                                    ; preds = %394
  %406 = sext i32 %397 to i64
  %407 = add nsw i64 %406, -1
  br label %408

; <label>:408:                                    ; preds = %429, %405
  %409 = phi i64 [ %407, %405 ], [ %431, %429 ]
  %410 = phi i32 [ %368, %405 ], [ %430, %429 ]
  %411 = getelementptr inbounds i32, i32* %402, i64 %409
  %412 = load i32, i32* %411, align 4, !tbaa !13
  %413 = sext i32 %412 to i64
  %414 = getelementptr inbounds i32, i32* %18, i64 %413
  %415 = load i32, i32* %414, align 4, !tbaa !13
  %416 = zext i32 %415 to i64
  %417 = icmp eq i64 %270, %416
  br i1 %417, label %429, label %418

; <label>:418:                                    ; preds = %408
  %419 = getelementptr inbounds i32, i32* %6, i64 %413
  %420 = load i32, i32* %419, align 4, !tbaa !13
  %421 = icmp sgt i32 %420, -1
  br i1 %421, label %422, label %425

; <label>:422:                                    ; preds = %418
  %423 = trunc i64 %409 to i32
  store i32 %423, i32* %395, align 4, !tbaa !13
  %424 = add nsw i32 %369, 1
  br label %442

; <label>:425:                                    ; preds = %418
  store i32 %278, i32* %414, align 4, !tbaa !13
  %426 = sext i32 %410 to i64
  %427 = getelementptr inbounds i32, i32* %329, i64 %426
  store i32 %412, i32* %427, align 4, !tbaa !13
  %428 = add nsw i32 %410, 1
  br label %429

; <label>:429:                                    ; preds = %425, %408
  %430 = phi i32 [ %428, %425 ], [ %410, %408 ]
  %431 = add i64 %409, -1
  %432 = icmp sgt i64 %409, 0
  br i1 %432, label %408, label %433

; <label>:433:                                    ; preds = %429
  %434 = trunc i64 %431 to i32
  br label %435

; <label>:435:                                    ; preds = %433, %394
  %436 = phi i32 [ %403, %394 ], [ %434, %433 ]
  %437 = phi i32 [ %368, %394 ], [ %430, %433 ]
  %438 = icmp eq i32 %436, -1
  br i1 %438, label %439, label %450

; <label>:439:                                    ; preds = %435
  %440 = add nsw i32 %369, -1
  %441 = add nsw i32 %370, -1
  br label %442

; <label>:442:                                    ; preds = %422, %439
  %443 = phi i32 [ %441, %439 ], [ %424, %422 ]
  %444 = phi i32 [ %367, %439 ], [ %412, %422 ]
  %445 = phi i32 [ %437, %439 ], [ %410, %422 ]
  %446 = phi i32 [ %441, %439 ], [ %370, %422 ]
  %447 = phi i32 [ %440, %439 ], [ %424, %422 ]
  %448 = sext i32 %443 to i64
  %449 = getelementptr inbounds i32, i32* %17, i64 %448
  store i32 %444, i32* %449, align 4, !tbaa !13
  br label %450

; <label>:450:                                    ; preds = %442, %435
  %451 = phi i32 [ %437, %435 ], [ %445, %442 ]
  %452 = phi i32 [ %370, %435 ], [ %446, %442 ]
  %453 = phi i32 [ %369, %435 ], [ %447, %442 ]
  %454 = icmp sgt i32 %453, -1
  br i1 %454, label %455, label %463

; <label>:455:                                    ; preds = %450
  %456 = sext i32 %453 to i64
  %457 = getelementptr inbounds i32, i32* %17, i64 %456
  %458 = load i32, i32* %457, align 4, !tbaa !13
  br label %366

; <label>:459:                                    ; preds = %361
  store i32 %278, i32* %357, align 4, !tbaa !13
  %460 = sext i32 %347 to i64
  %461 = getelementptr inbounds i32, i32* %329, i64 %460
  store i32 %353, i32* %461, align 4, !tbaa !13
  %462 = add nsw i32 %347, 1
  br label %463

; <label>:463:                                    ; preds = %450, %459, %355, %344
  %464 = phi i32 [ %347, %344 ], [ %347, %355 ], [ %462, %459 ], [ %451, %450 ]
  %465 = phi i32 [ %346, %344 ], [ %346, %355 ], [ %346, %459 ], [ %452, %450 ]
  %466 = add nsw i64 %345, 1
  %467 = icmp eq i64 %466, %343
  br i1 %467, label %468, label %344

; <label>:468:                                    ; preds = %463, %322
  %469 = phi i32 [ 0, %322 ], [ %464, %463 ]
  %470 = phi i32 [ %0, %322 ], [ %465, %463 ]
  %471 = getelementptr inbounds i32, i32* %10, i64 %270
  store i32 %469, i32* %471, align 4, !tbaa !13
  %472 = getelementptr inbounds i32, i32* %24, i64 %330
  %473 = load i32, i32* %472, align 4, !tbaa !13
  %474 = load i32, i32* %331, align 4, !tbaa !13
  %475 = add nsw i32 %474, 1
  %476 = sext i32 %475 to i64
  %477 = getelementptr inbounds i32, i32* %1, i64 %476
  %478 = load i32, i32* %477, align 4, !tbaa !13
  %479 = sext i32 %474 to i64
  %480 = getelementptr inbounds i32, i32* %1, i64 %479
  %481 = load i32, i32* %480, align 4, !tbaa !13
  %482 = icmp slt i32 %481, %478
  br i1 %262, label %483, label %514

; <label>:483:                                    ; preds = %468
  br i1 %482, label %484, label %545

; <label>:484:                                    ; preds = %483
  %485 = sext i32 %481 to i64
  %486 = sext i32 %478 to i64
  br label %487

; <label>:487:                                    ; preds = %510, %484
  %488 = phi i64 [ %485, %484 ], [ %512, %510 ]
  %489 = phi i32 [ %473, %484 ], [ %511, %510 ]
  %490 = getelementptr inbounds i32, i32* %2, i64 %488
  %491 = load i32, i32* %490, align 4, !tbaa !13
  %492 = sext i32 %491 to i64
  %493 = getelementptr inbounds i32, i32* %22, i64 %492
  %494 = load i32, i32* %493, align 4, !tbaa !13
  %495 = sub nsw i32 %494, %21
  %496 = getelementptr inbounds double, double* %3, i64 %488
  %497 = bitcast double* %496 to i64*
  %498 = load i64, i64* %497, align 8, !tbaa !15
  %499 = icmp slt i32 %495, 0
  br i1 %499, label %500, label %506

; <label>:500:                                    ; preds = %487
  %501 = sext i32 %489 to i64
  %502 = getelementptr inbounds i32, i32* %25, i64 %501
  store i32 %491, i32* %502, align 4, !tbaa !13
  %503 = getelementptr inbounds double, double* %26, i64 %501
  %504 = bitcast double* %503 to i64*
  store i64 %498, i64* %504, align 8, !tbaa !15
  %505 = add nsw i32 %489, 1
  br label %510

; <label>:506:                                    ; preds = %487
  %507 = sext i32 %495 to i64
  %508 = getelementptr inbounds double, double* %16, i64 %507
  %509 = bitcast double* %508 to i64*
  store i64 %498, i64* %509, align 8, !tbaa !15
  br label %510

; <label>:510:                                    ; preds = %506, %500
  %511 = phi i32 [ %505, %500 ], [ %489, %506 ]
  %512 = add nsw i64 %488, 1
  %513 = icmp eq i64 %512, %486
  br i1 %513, label %545, label %487

; <label>:514:                                    ; preds = %468
  br i1 %482, label %515, label %545

; <label>:515:                                    ; preds = %514
  %516 = sext i32 %481 to i64
  %517 = sext i32 %478 to i64
  br label %518

; <label>:518:                                    ; preds = %541, %515
  %519 = phi i64 [ %516, %515 ], [ %543, %541 ]
  %520 = phi i32 [ %473, %515 ], [ %542, %541 ]
  %521 = getelementptr inbounds i32, i32* %2, i64 %519
  %522 = load i32, i32* %521, align 4, !tbaa !13
  %523 = sext i32 %522 to i64
  %524 = getelementptr inbounds i32, i32* %22, i64 %523
  %525 = load i32, i32* %524, align 4, !tbaa !13
  %526 = sub nsw i32 %525, %21
  %527 = getelementptr inbounds double, double* %3, i64 %519
  %528 = load double, double* %527, align 8, !tbaa !15
  %529 = getelementptr inbounds double, double* %23, i64 %523
  %530 = load double, double* %529, align 8, !tbaa !15
  %531 = fdiv double %528, %530
  %532 = icmp slt i32 %526, 0
  br i1 %532, label %533, label %538

; <label>:533:                                    ; preds = %518
  %534 = sext i32 %520 to i64
  %535 = getelementptr inbounds i32, i32* %25, i64 %534
  store i32 %522, i32* %535, align 4, !tbaa !13
  %536 = getelementptr inbounds double, double* %26, i64 %534
  store double %531, double* %536, align 8, !tbaa !15
  %537 = add nsw i32 %520, 1
  br label %541

; <label>:538:                                    ; preds = %518
  %539 = sext i32 %526 to i64
  %540 = getelementptr inbounds double, double* %16, i64 %539
  store double %531, double* %540, align 8, !tbaa !15
  br label %541

; <label>:541:                                    ; preds = %538, %533
  %542 = phi i32 [ %537, %533 ], [ %520, %538 ]
  %543 = add nsw i64 %519, 1
  %544 = icmp eq i64 %543, %517
  br i1 %544, label %545, label %518

; <label>:545:                                    ; preds = %541, %510, %483, %514
  %546 = phi i32 [ %473, %483 ], [ %473, %514 ], [ %511, %510 ], [ %542, %541 ]
  %547 = add nsw i64 %330, 1
  %548 = getelementptr inbounds i32, i32* %24, i64 %547
  store i32 %546, i32* %548, align 4, !tbaa !13
  %549 = icmp slt i32 %470, %0
  br i1 %549, label %550, label %622

; <label>:550:                                    ; preds = %545
  %551 = sext i32 %470 to i64
  br label %552

; <label>:552:                                    ; preds = %619, %550
  %553 = phi i64 [ %551, %550 ], [ %620, %619 ]
  %554 = getelementptr inbounds i32, i32* %17, i64 %553
  %555 = load i32, i32* %554, align 4, !tbaa !13
  %556 = sext i32 %555 to i64
  %557 = getelementptr inbounds i32, i32* %6, i64 %556
  %558 = load i32, i32* %557, align 4, !tbaa !13
  %559 = getelementptr inbounds double, double* %16, i64 %556
  %560 = load double, double* %559, align 8, !tbaa !15
  %561 = sext i32 %558 to i64
  %562 = getelementptr inbounds i32, i32* %12, i64 %561
  %563 = load i32, i32* %562, align 4, !tbaa !13
  %564 = sext i32 %563 to i64
  %565 = getelementptr inbounds double, double* %324, i64 %564
  %566 = getelementptr inbounds i32, i32* %10, i64 %561
  %567 = load i32, i32* %566, align 4, !tbaa !13
  %568 = bitcast double* %565 to i32*
  %569 = sext i32 %567 to i64
  %570 = shl nsw i64 %569, 2
  %571 = add nsw i64 %570, 7
  %572 = lshr i64 %571, 3
  %573 = getelementptr inbounds double, double* %565, i64 %572
  %574 = icmp sgt i32 %567, 0
  br i1 %574, label %575, label %619

; <label>:575:                                    ; preds = %552
  %576 = zext i32 %567 to i64
  %577 = and i64 %576, 1
  %578 = icmp eq i32 %567, 1
  br i1 %578, label %606, label %579

; <label>:579:                                    ; preds = %575
  %580 = sub nsw i64 %576, %577
  br label %581

; <label>:581:                                    ; preds = %581, %579
  %582 = phi i64 [ 0, %579 ], [ %603, %581 ]
  %583 = phi i64 [ %580, %579 ], [ %604, %581 ]
  %584 = getelementptr inbounds double, double* %573, i64 %582
  %585 = load double, double* %584, align 8, !tbaa !15
  %586 = fmul double %560, %585
  %587 = getelementptr inbounds i32, i32* %568, i64 %582
  %588 = load i32, i32* %587, align 4, !tbaa !13
  %589 = sext i32 %588 to i64
  %590 = getelementptr inbounds double, double* %16, i64 %589
  %591 = load double, double* %590, align 8, !tbaa !15
  %592 = fsub double %591, %586
  store double %592, double* %590, align 8, !tbaa !15
  %593 = or i64 %582, 1
  %594 = getelementptr inbounds double, double* %573, i64 %593
  %595 = load double, double* %594, align 8, !tbaa !15
  %596 = fmul double %560, %595
  %597 = getelementptr inbounds i32, i32* %568, i64 %593
  %598 = load i32, i32* %597, align 4, !tbaa !13
  %599 = sext i32 %598 to i64
  %600 = getelementptr inbounds double, double* %16, i64 %599
  %601 = load double, double* %600, align 8, !tbaa !15
  %602 = fsub double %601, %596
  store double %602, double* %600, align 8, !tbaa !15
  %603 = add nuw nsw i64 %582, 2
  %604 = add i64 %583, -2
  %605 = icmp eq i64 %604, 0
  br i1 %605, label %606, label %581

; <label>:606:                                    ; preds = %581, %575
  %607 = phi i64 [ 0, %575 ], [ %603, %581 ]
  %608 = icmp eq i64 %577, 0
  br i1 %608, label %619, label %609

; <label>:609:                                    ; preds = %606
  %610 = getelementptr inbounds double, double* %573, i64 %607
  %611 = load double, double* %610, align 8, !tbaa !15
  %612 = fmul double %560, %611
  %613 = getelementptr inbounds i32, i32* %568, i64 %607
  %614 = load i32, i32* %613, align 4, !tbaa !13
  %615 = sext i32 %614 to i64
  %616 = getelementptr inbounds double, double* %16, i64 %615
  %617 = load double, double* %616, align 8, !tbaa !15
  %618 = fsub double %617, %612
  store double %618, double* %616, align 8, !tbaa !15
  br label %619

; <label>:619:                                    ; preds = %609, %606, %552
  %620 = add nsw i64 %553, 1
  %621 = icmp eq i64 %620, %263
  br i1 %621, label %622, label %552

; <label>:622:                                    ; preds = %619, %545
  %623 = getelementptr inbounds i32, i32* %7, i64 %270
  %624 = load i32, i32* %623, align 4, !tbaa !13
  %625 = load i32, i32* %471, align 4, !tbaa !13
  %626 = icmp eq i32 %625, 0
  br i1 %626, label %627, label %646

; <label>:627:                                    ; preds = %622
  %628 = load i32, i32* %264, align 8, !tbaa !37
  %629 = icmp eq i32 %628, 0
  br i1 %629, label %630, label %791

; <label>:630:                                    ; preds = %627
  %631 = icmp slt i32 %275, %0
  br i1 %631, label %632, label %791

; <label>:632:                                    ; preds = %630
  %633 = sext i32 %275 to i64
  br label %634

; <label>:634:                                    ; preds = %640, %632
  %635 = phi i64 [ %633, %632 ], [ %641, %640 ]
  %636 = phi i32 [ %275, %632 ], [ %642, %640 ]
  %637 = getelementptr inbounds i32, i32* %6, i64 %635
  %638 = load i32, i32* %637, align 4, !tbaa !13
  %639 = icmp slt i32 %638, 0
  br i1 %639, label %644, label %640

; <label>:640:                                    ; preds = %634
  %641 = add nsw i64 %635, 1
  %642 = add nsw i32 %636, 1
  %643 = icmp slt i64 %641, %263
  br i1 %643, label %634, label %791

; <label>:644:                                    ; preds = %634
  %645 = trunc i64 %635 to i32
  br label %791

; <label>:646:                                    ; preds = %622
  %647 = add nsw i32 %625, -1
  %648 = load i32, i32* %326, align 4, !tbaa !13
  %649 = sext i32 %648 to i64
  %650 = getelementptr inbounds double, double* %324, i64 %649
  %651 = bitcast double* %650 to i32*
  %652 = sext i32 %647 to i64
  %653 = getelementptr inbounds i32, i32* %651, i64 %652
  %654 = load i32, i32* %653, align 4, !tbaa !13
  store i32 %647, i32* %471, align 4, !tbaa !13
  %655 = load i32, i32* %326, align 4, !tbaa !13
  %656 = sext i32 %655 to i64
  %657 = getelementptr inbounds double, double* %324, i64 %656
  %658 = bitcast double* %657 to i32*
  %659 = shl nsw i64 %652, 2
  %660 = add nsw i64 %659, 7
  %661 = lshr i64 %660, 3
  %662 = getelementptr inbounds double, double* %657, i64 %661
  %663 = icmp sgt i32 %625, 1
  br i1 %663, label %664, label %688

; <label>:664:                                    ; preds = %646
  %665 = zext i32 %647 to i64
  br label %666

; <label>:666:                                    ; preds = %666, %664
  %667 = phi i64 [ 0, %664 ], [ %686, %666 ]
  %668 = phi i32 [ -1, %664 ], [ %682, %666 ]
  %669 = phi i32 [ -1, %664 ], [ %685, %666 ]
  %670 = phi double [ -1.000000e+00, %664 ], [ %684, %666 ]
  %671 = getelementptr inbounds i32, i32* %658, i64 %667
  %672 = load i32, i32* %671, align 4, !tbaa !13
  %673 = sext i32 %672 to i64
  %674 = getelementptr inbounds double, double* %16, i64 %673
  %675 = load double, double* %674, align 8, !tbaa !15
  store double 0.000000e+00, double* %674, align 8, !tbaa !15
  %676 = getelementptr inbounds double, double* %662, i64 %667
  store double %675, double* %676, align 8, !tbaa !15
  %677 = fcmp olt double %675, 0.000000e+00
  %678 = fsub double -0.000000e+00, %675
  %679 = select i1 %677, double %678, double %675
  %680 = icmp eq i32 %672, %624
  %681 = trunc i64 %667 to i32
  %682 = select i1 %680, i32 %681, i32 %668
  %683 = fcmp ogt double %679, %670
  %684 = select i1 %683, double %679, double %670
  %685 = select i1 %683, i32 %681, i32 %669
  %686 = add nuw nsw i64 %667, 1
  %687 = icmp eq i64 %686, %665
  br i1 %687, label %688, label %666

; <label>:688:                                    ; preds = %666, %646
  %689 = phi double [ -1.000000e+00, %646 ], [ %684, %666 ]
  %690 = phi i32 [ -1, %646 ], [ %685, %666 ]
  %691 = phi i32 [ -1, %646 ], [ %682, %666 ]
  %692 = sext i32 %654 to i64
  %693 = getelementptr inbounds double, double* %16, i64 %692
  %694 = load double, double* %693, align 8, !tbaa !15
  %695 = fcmp olt double %694, 0.000000e+00
  %696 = fsub double -0.000000e+00, %694
  %697 = select i1 %695, double %696, double %694
  %698 = fcmp ogt double %697, %689
  %699 = select i1 %698, double %697, double %689
  %700 = select i1 %698, i32 -1, i32 %690
  %701 = icmp eq i32 %654, %624
  br i1 %701, label %702, label %705

; <label>:702:                                    ; preds = %688
  %703 = fmul double %32, %699
  %704 = fcmp ult double %697, %703
  br i1 %704, label %716, label %728

; <label>:705:                                    ; preds = %688
  %706 = icmp eq i32 %691, -1
  br i1 %706, label %716, label %707

; <label>:707:                                    ; preds = %705
  %708 = sext i32 %691 to i64
  %709 = getelementptr inbounds double, double* %662, i64 %708
  %710 = load double, double* %709, align 8, !tbaa !15
  %711 = fcmp olt double %710, 0.000000e+00
  %712 = fsub double -0.000000e+00, %710
  %713 = select i1 %711, double %712, double %710
  %714 = fmul double %32, %699
  %715 = fcmp ult double %713, %714
  br i1 %715, label %716, label %722

; <label>:716:                                    ; preds = %707, %705, %702
  %717 = icmp eq i32 %700, -1
  br i1 %717, label %728, label %718

; <label>:718:                                    ; preds = %716
  %719 = sext i32 %700 to i64
  %720 = getelementptr inbounds double, double* %662, i64 %719
  %721 = load double, double* %720, align 8, !tbaa !15
  br label %722

; <label>:722:                                    ; preds = %718, %707
  %723 = phi double* [ %720, %718 ], [ %709, %707 ]
  %724 = phi i64 [ %719, %718 ], [ %708, %707 ]
  %725 = phi double [ %721, %718 ], [ %710, %707 ]
  %726 = getelementptr inbounds i32, i32* %658, i64 %724
  %727 = load i32, i32* %726, align 4, !tbaa !13
  store i32 %654, i32* %726, align 4, !tbaa !13
  store double %694, double* %723, align 8, !tbaa !15
  br label %728

; <label>:728:                                    ; preds = %722, %716, %702
  %729 = phi i32 [ %727, %722 ], [ %654, %716 ], [ %624, %702 ]
  %730 = phi double [ %725, %722 ], [ %694, %716 ], [ %694, %702 ]
  store double 0.000000e+00, double* %693, align 8, !tbaa !15
  %731 = bitcast double %730 to i64
  %732 = fcmp oeq double %730, 0.000000e+00
  br i1 %732, label %733, label %736

; <label>:733:                                    ; preds = %728
  %734 = load i32, i32* %264, align 8, !tbaa !37
  %735 = icmp eq i32 %734, 0
  br i1 %735, label %736, label %791

; <label>:736:                                    ; preds = %733, %728
  %737 = load i32, i32* %471, align 4, !tbaa !13
  %738 = icmp sgt i32 %737, 0
  br i1 %738, label %739, label %805

; <label>:739:                                    ; preds = %736
  %740 = sext i32 %737 to i64
  %741 = sext i32 %737 to i64
  %742 = icmp eq i32 %737, 1
  br i1 %742, label %782, label %743

; <label>:743:                                    ; preds = %739
  %744 = and i64 %741, -2
  %745 = insertelement <2 x double> undef, double %730, i32 0
  %746 = shufflevector <2 x double> %745, <2 x double> undef, <2 x i32> zeroinitializer
  %747 = add nsw i64 %744, -2
  %748 = lshr exact i64 %747, 1
  %749 = add nuw i64 %748, 1
  %750 = and i64 %749, 1
  %751 = icmp eq i64 %747, 0
  br i1 %751, label %771, label %752

; <label>:752:                                    ; preds = %743
  %753 = sub i64 %749, %750
  br label %754

; <label>:754:                                    ; preds = %754, %752
  %755 = phi i64 [ 0, %752 ], [ %768, %754 ]
  %756 = phi i64 [ %753, %752 ], [ %769, %754 ]
  %757 = getelementptr inbounds double, double* %662, i64 %755
  %758 = bitcast double* %757 to <2 x double>*
  %759 = load <2 x double>, <2 x double>* %758, align 8, !tbaa !15
  %760 = fdiv <2 x double> %759, %746
  %761 = bitcast double* %757 to <2 x double>*
  store <2 x double> %760, <2 x double>* %761, align 8, !tbaa !15
  %762 = or i64 %755, 2
  %763 = getelementptr inbounds double, double* %662, i64 %762
  %764 = bitcast double* %763 to <2 x double>*
  %765 = load <2 x double>, <2 x double>* %764, align 8, !tbaa !15
  %766 = fdiv <2 x double> %765, %746
  %767 = bitcast double* %763 to <2 x double>*
  store <2 x double> %766, <2 x double>* %767, align 8, !tbaa !15
  %768 = add i64 %755, 4
  %769 = add i64 %756, -2
  %770 = icmp eq i64 %769, 0
  br i1 %770, label %771, label %754, !llvm.loop !38

; <label>:771:                                    ; preds = %754, %743
  %772 = phi i64 [ 0, %743 ], [ %768, %754 ]
  %773 = icmp eq i64 %750, 0
  br i1 %773, label %780, label %774

; <label>:774:                                    ; preds = %771
  %775 = getelementptr inbounds double, double* %662, i64 %772
  %776 = bitcast double* %775 to <2 x double>*
  %777 = load <2 x double>, <2 x double>* %776, align 8, !tbaa !15
  %778 = fdiv <2 x double> %777, %746
  %779 = bitcast double* %775 to <2 x double>*
  store <2 x double> %778, <2 x double>* %779, align 8, !tbaa !15
  br label %780

; <label>:780:                                    ; preds = %771, %774
  %781 = icmp eq i64 %744, %741
  br i1 %781, label %805, label %782

; <label>:782:                                    ; preds = %780, %739
  %783 = phi i64 [ 0, %739 ], [ %744, %780 ]
  br label %784

; <label>:784:                                    ; preds = %782, %784
  %785 = phi i64 [ %789, %784 ], [ %783, %782 ]
  %786 = getelementptr inbounds double, double* %662, i64 %785
  %787 = load double, double* %786, align 8, !tbaa !15
  %788 = fdiv double %787, %730
  store double %788, double* %786, align 8, !tbaa !15
  %789 = add nuw nsw i64 %785, 1
  %790 = icmp eq i64 %789, %740
  br i1 %790, label %805, label %784, !llvm.loop !39

; <label>:791:                                    ; preds = %640, %627, %733, %644, %630
  %792 = phi i32 [ 0, %630 ], [ 0, %644 ], [ %734, %733 ], [ %628, %627 ], [ 0, %640 ]
  %793 = phi i64 [ 0, %630 ], [ 0, %644 ], [ %731, %733 ], [ %277, %627 ], [ 0, %640 ]
  %794 = phi i32 [ -1, %630 ], [ %645, %644 ], [ %729, %733 ], [ %276, %627 ], [ -1, %640 ]
  %795 = phi i32 [ %275, %630 ], [ %645, %644 ], [ %275, %733 ], [ %275, %627 ], [ %642, %640 ]
  store i32 1, i32* %261, align 4, !tbaa !35
  %796 = load i32, i32* %265, align 8, !tbaa !41
  %797 = icmp eq i32 %796, -1
  br i1 %797, label %798, label %801

; <label>:798:                                    ; preds = %791
  %799 = trunc i64 %330 to i32
  store i32 %799, i32* %265, align 8, !tbaa !41
  %800 = load i32, i32* %331, align 4, !tbaa !13
  store i32 %800, i32* %266, align 4, !tbaa !42
  br label %801

; <label>:801:                                    ; preds = %798, %791
  %802 = icmp eq i32 %792, 0
  br i1 %802, label %803, label %1042

; <label>:803:                                    ; preds = %801
  %804 = load i32, i32* %471, align 4, !tbaa !13
  br label %805

; <label>:805:                                    ; preds = %784, %780, %803, %736
  %806 = phi i32 [ %804, %803 ], [ %737, %736 ], [ %737, %780 ], [ %737, %784 ]
  %807 = phi i32 [ %795, %803 ], [ %275, %736 ], [ %275, %780 ], [ %275, %784 ]
  %808 = phi i32 [ %794, %803 ], [ %729, %736 ], [ %729, %780 ], [ %729, %784 ]
  %809 = phi i64 [ %793, %803 ], [ %731, %736 ], [ %731, %780 ], [ %731, %784 ]
  %810 = load i32, i32* %326, align 4, !tbaa !13
  %811 = sext i32 %806 to i64
  %812 = shl nsw i64 %811, 2
  %813 = add nsw i64 %812, 7
  %814 = lshr i64 %813, 3
  %815 = trunc i64 %814 to i32
  %816 = add i32 %806, %810
  %817 = add i32 %816, %815
  %818 = getelementptr inbounds i32, i32* %13, i64 %270
  store i32 %817, i32* %818, align 4, !tbaa !13
  %819 = load i32, i32* %471, align 4, !tbaa !13
  %820 = sext i32 %819 to i64
  %821 = shl nsw i64 %820, 2
  %822 = add nsw i64 %821, 7
  %823 = lshr i64 %822, 3
  %824 = trunc i64 %823 to i32
  %825 = sub i32 %0, %470
  %826 = getelementptr inbounds i32, i32* %11, i64 %270
  store i32 %825, i32* %826, align 4, !tbaa !13
  %827 = load i32, i32* %818, align 4, !tbaa !13
  %828 = sext i32 %827 to i64
  %829 = getelementptr inbounds double, double* %324, i64 %828
  %830 = bitcast double* %829 to i32*
  %831 = sext i32 %825 to i64
  %832 = shl nsw i64 %831, 2
  %833 = add nsw i64 %832, 7
  %834 = lshr i64 %833, 3
  %835 = getelementptr inbounds double, double* %829, i64 %834
  br i1 %549, label %836, label %897

; <label>:836:                                    ; preds = %805
  %837 = sext i32 %470 to i64
  %838 = zext i32 %825 to i64
  %839 = and i64 %838, 1
  %840 = icmp eq i32 %825, 1
  br i1 %840, label %875, label %841

; <label>:841:                                    ; preds = %836
  %842 = sub nsw i64 %838, %839
  br label %843

; <label>:843:                                    ; preds = %843, %841
  %844 = phi i64 [ 0, %841 ], [ %872, %843 ]
  %845 = phi i64 [ %837, %841 ], [ %871, %843 ]
  %846 = phi i64 [ %842, %841 ], [ %873, %843 ]
  %847 = getelementptr inbounds i32, i32* %17, i64 %845
  %848 = load i32, i32* %847, align 4, !tbaa !13
  %849 = sext i32 %848 to i64
  %850 = getelementptr inbounds i32, i32* %6, i64 %849
  %851 = load i32, i32* %850, align 4, !tbaa !13
  %852 = getelementptr inbounds i32, i32* %830, i64 %844
  store i32 %851, i32* %852, align 4, !tbaa !13
  %853 = getelementptr inbounds double, double* %16, i64 %849
  %854 = bitcast double* %853 to i64*
  %855 = load i64, i64* %854, align 8, !tbaa !15
  %856 = getelementptr inbounds double, double* %835, i64 %844
  %857 = bitcast double* %856 to i64*
  store i64 %855, i64* %857, align 8, !tbaa !15
  store double 0.000000e+00, double* %853, align 8, !tbaa !15
  %858 = add nsw i64 %845, 1
  %859 = or i64 %844, 1
  %860 = getelementptr inbounds i32, i32* %17, i64 %858
  %861 = load i32, i32* %860, align 4, !tbaa !13
  %862 = sext i32 %861 to i64
  %863 = getelementptr inbounds i32, i32* %6, i64 %862
  %864 = load i32, i32* %863, align 4, !tbaa !13
  %865 = getelementptr inbounds i32, i32* %830, i64 %859
  store i32 %864, i32* %865, align 4, !tbaa !13
  %866 = getelementptr inbounds double, double* %16, i64 %862
  %867 = bitcast double* %866 to i64*
  %868 = load i64, i64* %867, align 8, !tbaa !15
  %869 = getelementptr inbounds double, double* %835, i64 %859
  %870 = bitcast double* %869 to i64*
  store i64 %868, i64* %870, align 8, !tbaa !15
  store double 0.000000e+00, double* %866, align 8, !tbaa !15
  %871 = add nsw i64 %845, 2
  %872 = add nuw nsw i64 %844, 2
  %873 = add i64 %846, -2
  %874 = icmp eq i64 %873, 0
  br i1 %874, label %875, label %843

; <label>:875:                                    ; preds = %843, %836
  %876 = phi i64 [ 0, %836 ], [ %872, %843 ]
  %877 = phi i64 [ %837, %836 ], [ %871, %843 ]
  %878 = icmp eq i64 %839, 0
  br i1 %878, label %891, label %879

; <label>:879:                                    ; preds = %875
  %880 = getelementptr inbounds i32, i32* %17, i64 %877
  %881 = load i32, i32* %880, align 4, !tbaa !13
  %882 = sext i32 %881 to i64
  %883 = getelementptr inbounds i32, i32* %6, i64 %882
  %884 = load i32, i32* %883, align 4, !tbaa !13
  %885 = getelementptr inbounds i32, i32* %830, i64 %876
  store i32 %884, i32* %885, align 4, !tbaa !13
  %886 = getelementptr inbounds double, double* %16, i64 %882
  %887 = bitcast double* %886 to i64*
  %888 = load i64, i64* %887, align 8, !tbaa !15
  %889 = getelementptr inbounds double, double* %835, i64 %876
  %890 = bitcast double* %889 to i64*
  store i64 %888, i64* %890, align 8, !tbaa !15
  store double 0.000000e+00, double* %886, align 8, !tbaa !15
  br label %891

; <label>:891:                                    ; preds = %875, %879
  %892 = load i32, i32* %826, align 4, !tbaa !13
  %893 = sext i32 %892 to i64
  %894 = shl nsw i64 %893, 2
  %895 = add nsw i64 %894, 7
  %896 = lshr i64 %895, 3
  br label %897

; <label>:897:                                    ; preds = %891, %805
  %898 = phi i64 [ %896, %891 ], [ %834, %805 ]
  %899 = phi i32 [ %892, %891 ], [ %825, %805 ]
  %900 = trunc i64 %898 to i32
  %901 = add i32 %819, %274
  %902 = add i32 %901, %824
  %903 = add i32 %902, %899
  %904 = add i32 %903, %900
  %905 = getelementptr inbounds double, double* %9, i64 %270
  %906 = bitcast double* %905 to i64*
  store i64 %809, i64* %906, align 8, !tbaa !15
  %907 = icmp eq i32 %808, %624
  br i1 %907, label %922, label %908

; <label>:908:                                    ; preds = %897
  %909 = load i32, i32* %267, align 8, !tbaa !43
  %910 = add nsw i32 %909, 1
  store i32 %910, i32* %267, align 8, !tbaa !43
  %911 = sext i32 %624 to i64
  %912 = getelementptr inbounds i32, i32* %6, i64 %911
  %913 = load i32, i32* %912, align 4, !tbaa !13
  %914 = icmp slt i32 %913, 0
  br i1 %914, label %915, label %922

; <label>:915:                                    ; preds = %908
  %916 = sext i32 %808 to i64
  %917 = getelementptr inbounds i32, i32* %6, i64 %916
  %918 = load i32, i32* %917, align 4, !tbaa !13
  %919 = sub i32 -2, %918
  %920 = sext i32 %919 to i64
  %921 = getelementptr inbounds i32, i32* %7, i64 %920
  store i32 %624, i32* %921, align 4, !tbaa !13
  store i32 %918, i32* %912, align 4, !tbaa !13
  br label %922

; <label>:922:                                    ; preds = %897, %908, %915
  store i32 %808, i32* %623, align 4, !tbaa !13
  %923 = sext i32 %808 to i64
  %924 = getelementptr inbounds i32, i32* %6, i64 %923
  store i32 %278, i32* %924, align 4, !tbaa !13
  %925 = load i32, i32* %818, align 4, !tbaa !13
  %926 = sext i32 %925 to i64
  %927 = getelementptr inbounds double, double* %324, i64 %926
  %928 = load i32, i32* %826, align 4, !tbaa !13
  %929 = bitcast double* %927 to i32*
  %930 = icmp sgt i32 %928, 0
  br i1 %930, label %931, label %996

; <label>:931:                                    ; preds = %922
  %932 = zext i32 %928 to i64
  br label %933

; <label>:933:                                    ; preds = %993, %931
  %934 = phi i64 [ 0, %931 ], [ %994, %993 ]
  %935 = getelementptr inbounds i32, i32* %929, i64 %934
  %936 = load i32, i32* %935, align 4, !tbaa !13
  %937 = sext i32 %936 to i64
  %938 = getelementptr inbounds i32, i32* %20, i64 %937
  %939 = load i32, i32* %938, align 4, !tbaa !13
  %940 = icmp eq i32 %939, -1
  br i1 %940, label %941, label %993

; <label>:941:                                    ; preds = %933
  %942 = getelementptr inbounds i32, i32* %12, i64 %937
  %943 = load i32, i32* %942, align 4, !tbaa !13
  %944 = sext i32 %943 to i64
  %945 = getelementptr inbounds double, double* %324, i64 %944
  %946 = getelementptr inbounds i32, i32* %10, i64 %937
  %947 = load i32, i32* %946, align 4, !tbaa !13
  %948 = bitcast double* %945 to i32*
  %949 = sext i32 %947 to i64
  %950 = shl nsw i64 %949, 2
  %951 = add nsw i64 %950, 7
  %952 = lshr i64 %951, 3
  %953 = getelementptr inbounds double, double* %945, i64 %952
  %954 = icmp sgt i32 %947, 0
  br i1 %954, label %955, label %993

; <label>:955:                                    ; preds = %941
  br label %958

; <label>:956:                                    ; preds = %958
  %957 = icmp slt i64 %963, %949
  br i1 %957, label %958, label %993

; <label>:958:                                    ; preds = %955, %956
  %959 = phi i64 [ %963, %956 ], [ 0, %955 ]
  %960 = getelementptr inbounds i32, i32* %948, i64 %959
  %961 = load i32, i32* %960, align 4, !tbaa !13
  %962 = icmp eq i32 %961, %808
  %963 = add nuw nsw i64 %959, 1
  br i1 %962, label %964, label %956

; <label>:964:                                    ; preds = %958
  br label %965

; <label>:965:                                    ; preds = %964, %988
  %966 = phi i32 [ %990, %988 ], [ 0, %964 ]
  %967 = phi i32 [ %989, %988 ], [ %947, %964 ]
  %968 = sext i32 %966 to i64
  %969 = getelementptr inbounds i32, i32* %948, i64 %968
  %970 = load i32, i32* %969, align 4, !tbaa !13
  %971 = sext i32 %970 to i64
  %972 = getelementptr inbounds i32, i32* %6, i64 %971
  %973 = load i32, i32* %972, align 4, !tbaa !13
  %974 = icmp sgt i32 %973, -1
  br i1 %974, label %975, label %977

; <label>:975:                                    ; preds = %965
  %976 = add nsw i32 %966, 1
  br label %988

; <label>:977:                                    ; preds = %965
  %978 = add nsw i32 %967, -1
  %979 = sext i32 %978 to i64
  %980 = getelementptr inbounds i32, i32* %948, i64 %979
  %981 = load i32, i32* %980, align 4, !tbaa !13
  store i32 %981, i32* %969, align 4, !tbaa !13
  store i32 %970, i32* %980, align 4, !tbaa !13
  %982 = getelementptr inbounds double, double* %953, i64 %968
  %983 = bitcast double* %982 to i64*
  %984 = load i64, i64* %983, align 8, !tbaa !15
  %985 = getelementptr inbounds double, double* %953, i64 %979
  %986 = bitcast double* %985 to i64*
  %987 = load i64, i64* %986, align 8, !tbaa !15
  store i64 %987, i64* %983, align 8, !tbaa !15
  store i64 %984, i64* %986, align 8, !tbaa !15
  br label %988

; <label>:988:                                    ; preds = %977, %975
  %989 = phi i32 [ %967, %975 ], [ %978, %977 ]
  %990 = phi i32 [ %976, %975 ], [ %966, %977 ]
  %991 = icmp slt i32 %990, %989
  br i1 %991, label %965, label %992

; <label>:992:                                    ; preds = %988
  store i32 %989, i32* %938, align 4, !tbaa !13
  br label %993

; <label>:993:                                    ; preds = %956, %992, %941, %933
  %994 = add nuw nsw i64 %934, 1
  %995 = icmp eq i64 %994, %932
  br i1 %995, label %996, label %933

; <label>:996:                                    ; preds = %993, %922
  %997 = load i32, i32* %471, align 4, !tbaa !13
  %998 = add nsw i32 %997, 1
  %999 = load i32, i32* %14, align 4, !tbaa !13
  %1000 = add nsw i32 %998, %999
  store i32 %1000, i32* %14, align 4, !tbaa !13
  %1001 = load i32, i32* %826, align 4, !tbaa !13
  %1002 = add nsw i32 %1001, 1
  %1003 = load i32, i32* %15, align 4, !tbaa !13
  %1004 = add nsw i32 %1002, %1003
  store i32 %1004, i32* %15, align 4, !tbaa !13
  %1005 = add nuw nsw i64 %270, 1
  %1006 = icmp slt i64 %1005, %263
  br i1 %1006, label %269, label %1007

; <label>:1007:                                   ; preds = %996
  %1008 = sext i32 %904 to i64
  br i1 %37, label %1009, label %1036

; <label>:1009:                                   ; preds = %1007
  %1010 = zext i32 %0 to i64
  br label %1011

; <label>:1011:                                   ; preds = %1033, %1009
  %1012 = phi i64 [ 0, %1009 ], [ %1034, %1033 ]
  %1013 = getelementptr inbounds i32, i32* %12, i64 %1012
  %1014 = load i32, i32* %1013, align 4, !tbaa !13
  %1015 = sext i32 %1014 to i64
  %1016 = getelementptr inbounds double, double* %324, i64 %1015
  %1017 = bitcast double* %1016 to i32*
  %1018 = getelementptr inbounds i32, i32* %10, i64 %1012
  %1019 = load i32, i32* %1018, align 4, !tbaa !13
  %1020 = icmp sgt i32 %1019, 0
  br i1 %1020, label %1021, label %1033

; <label>:1021:                                   ; preds = %1011
  br label %1022

; <label>:1022:                                   ; preds = %1021, %1022
  %1023 = phi i64 [ %1029, %1022 ], [ 0, %1021 ]
  %1024 = getelementptr inbounds i32, i32* %1017, i64 %1023
  %1025 = load i32, i32* %1024, align 4, !tbaa !13
  %1026 = sext i32 %1025 to i64
  %1027 = getelementptr inbounds i32, i32* %6, i64 %1026
  %1028 = load i32, i32* %1027, align 4, !tbaa !13
  store i32 %1028, i32* %1024, align 4, !tbaa !13
  %1029 = add nuw nsw i64 %1023, 1
  %1030 = load i32, i32* %1018, align 4, !tbaa !13
  %1031 = sext i32 %1030 to i64
  %1032 = icmp slt i64 %1029, %1031
  br i1 %1032, label %1022, label %1033

; <label>:1033:                                   ; preds = %1022, %1011
  %1034 = add nuw nsw i64 %1012, 1
  %1035 = icmp eq i64 %1034, %1010
  br i1 %1035, label %1036, label %1011

; <label>:1036:                                   ; preds = %1033, %252, %251, %1007
  %1037 = phi i64 [ %325, %1007 ], [ %5, %251 ], [ %5, %252 ], [ %325, %1033 ]
  %1038 = phi i8* [ %323, %1007 ], [ %36, %251 ], [ %36, %252 ], [ %323, %1033 ]
  %1039 = phi i64 [ %1008, %1007 ], [ 0, %251 ], [ 0, %252 ], [ %1008, %1033 ]
  %1040 = tail call i8* @klu_realloc(i64 %1039, i64 %1037, i64 8, i8* %1038, %struct.klu_common_struct* %27) #3
  %1041 = bitcast double** %8 to i8**
  store i8* %1040, i8** %1041, align 8, !tbaa !14
  br label %1042

; <label>:1042:                                   ; preds = %801, %310, %1036, %309
  %1043 = phi i64 [ %271, %309 ], [ %1039, %1036 ], [ %325, %801 ], [ %271, %310 ]
  ret i64 %1043
}

; Function Attrs: nounwind readnone speculatable
declare double @llvm.ceil.f64(double) #1

declare i8* @klu_realloc(i64, i64, i64, i8*, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: nounwind readnone speculatable
declare <2 x double> @llvm.ceil.v2f64(<2 x double>) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind readnone speculatable }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !8, i64 48}
!4 = !{!"klu_common_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !8, i64 40, !8, i64 44, !8, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92, !8, i64 96, !5, i64 104, !5, i64 112, !5, i64 120, !5, i64 128, !5, i64 136, !10, i64 144, !10, i64 152}
!5 = !{!"double", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!"int", !6, i64 0}
!9 = !{!"any pointer", !6, i64 0}
!10 = !{!"long", !6, i64 0}
!11 = !{!4, !5, i64 0}
!12 = !{!4, !5, i64 8}
!13 = !{!8, !8, i64 0}
!14 = !{!9, !9, i64 0}
!15 = !{!5, !5, i64 0}
!16 = !{!17}
!17 = distinct !{!17, !18}
!18 = distinct !{!18, !"LVerDomain"}
!19 = !{!20}
!20 = distinct !{!20, !18}
!21 = !{!22}
!22 = distinct !{!22, !18}
!23 = distinct !{!23, !24}
!24 = !{!"llvm.loop.isvectorized", i32 1}
!25 = distinct !{!25, !24}
!26 = !{!27}
!27 = distinct !{!27, !28}
!28 = distinct !{!28, !"LVerDomain"}
!29 = !{!30}
!30 = distinct !{!30, !28}
!31 = distinct !{!31, !24}
!32 = distinct !{!32, !33}
!33 = !{!"llvm.loop.unroll.disable"}
!34 = distinct !{!34, !24}
!35 = !{!4, !8, i64 76}
!36 = !{!4, !8, i64 80}
!37 = !{!4, !8, i64 72}
!38 = distinct !{!38, !24}
!39 = distinct !{!39, !40, !24}
!40 = !{!"llvm.loop.unroll.runtime.disable"}
!41 = !{!4, !8, i64 88}
!42 = !{!4, !8, i64 92}
!43 = !{!4, !8, i64 96}
