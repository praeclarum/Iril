; ModuleID = 'amd_aat.c'
source_filename = "amd_aat.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

@.memset_pattern = private unnamed_addr constant [2 x double] [double -1.000000e+00, double -1.000000e+00], align 16

; Function Attrs: norecurse nounwind ssp uwtable
define i64 @amd_aat(i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture, i32* nocapture, double*) local_unnamed_addr #0 {
  %7 = bitcast i32* %3 to i8*
  %8 = icmp ne double* %5, null
  br i1 %8, label %9, label %11

; <label>:9:                                      ; preds = %6
  %10 = bitcast double* %5 to i8*
  call void @memset_pattern16(i8* %10, i8* bitcast ([2 x double]* @.memset_pattern to i8*), i64 160) #3
  store double 0.000000e+00, double* %5, align 8, !tbaa !3
  br label %11

; <label>:11:                                     ; preds = %9, %6
  %12 = icmp sgt i32 %0, 0
  br i1 %12, label %17, label %13

; <label>:13:                                     ; preds = %11
  %14 = sext i32 %0 to i64
  %15 = getelementptr inbounds i32, i32* %1, i64 %14
  %16 = load i32, i32* %15, align 4, !tbaa !7
  br label %134

; <label>:17:                                     ; preds = %11
  %18 = zext i32 %0 to i64
  %19 = shl nuw nsw i64 %18, 2
  call void @llvm.memset.p0i8.i64(i8* %7, i8 0, i64 %19, i32 4, i1 false)
  %20 = sext i32 %0 to i64
  %21 = getelementptr inbounds i32, i32* %1, i64 %20
  %22 = load i32, i32* %21, align 4, !tbaa !7
  %23 = zext i32 %0 to i64
  br label %24

; <label>:24:                                     ; preds = %98, %17
  %25 = phi i64 [ 0, %17 ], [ %30, %98 ]
  %26 = phi i32 [ 0, %17 ], [ %99, %98 ]
  %27 = phi i32 [ 0, %17 ], [ %100, %98 ]
  %28 = getelementptr inbounds i32, i32* %1, i64 %25
  %29 = load i32, i32* %28, align 4, !tbaa !7
  %30 = add nuw nsw i64 %25, 1
  %31 = getelementptr inbounds i32, i32* %1, i64 %30
  %32 = load i32, i32* %31, align 4, !tbaa !7
  %33 = icmp slt i32 %29, %32
  br i1 %33, label %34, label %98

; <label>:34:                                     ; preds = %24
  %35 = getelementptr inbounds i32, i32* %3, i64 %25
  %36 = sext i32 %29 to i64
  %37 = sext i32 %32 to i64
  br label %38

; <label>:38:                                     ; preds = %34, %92
  %39 = phi i64 [ %36, %34 ], [ %51, %92 ]
  %40 = phi i32 [ %26, %34 ], [ %94, %92 ]
  %41 = getelementptr inbounds i32, i32* %2, i64 %39
  %42 = load i32, i32* %41, align 4, !tbaa !7
  %43 = sext i32 %42 to i64
  %44 = icmp sgt i64 %25, %43
  br i1 %44, label %45, label %62

; <label>:45:                                     ; preds = %38
  %46 = getelementptr inbounds i32, i32* %3, i64 %43
  %47 = load i32, i32* %46, align 4, !tbaa !7
  %48 = add nsw i32 %47, 1
  store i32 %48, i32* %46, align 4, !tbaa !7
  %49 = load i32, i32* %35, align 4, !tbaa !7
  %50 = add nsw i32 %49, 1
  store i32 %50, i32* %35, align 4, !tbaa !7
  %51 = add nsw i64 %39, 1
  %52 = add nsw i32 %42, 1
  %53 = sext i32 %52 to i64
  %54 = getelementptr inbounds i32, i32* %1, i64 %53
  %55 = load i32, i32* %54, align 4, !tbaa !7
  %56 = getelementptr inbounds i32, i32* %4, i64 %43
  %57 = load i32, i32* %56, align 4, !tbaa !7
  %58 = icmp slt i32 %57, %55
  br i1 %58, label %59, label %92

; <label>:59:                                     ; preds = %45
  %60 = sext i32 %57 to i64
  %61 = sext i32 %55 to i64
  br label %69

; <label>:62:                                     ; preds = %38
  %63 = trunc i64 %39 to i32
  %64 = zext i32 %42 to i64
  %65 = icmp eq i64 %25, %64
  br i1 %65, label %66, label %98

; <label>:66:                                     ; preds = %62
  %67 = add nsw i32 %63, 1
  %68 = add nsw i32 %27, 1
  br label %98

; <label>:69:                                     ; preds = %59, %75
  %70 = phi i64 [ %60, %59 ], [ %81, %75 ]
  %71 = getelementptr inbounds i32, i32* %2, i64 %70
  %72 = load i32, i32* %71, align 4, !tbaa !7
  %73 = sext i32 %72 to i64
  %74 = icmp sgt i64 %25, %73
  br i1 %74, label %75, label %83

; <label>:75:                                     ; preds = %69
  %76 = getelementptr inbounds i32, i32* %3, i64 %73
  %77 = load i32, i32* %76, align 4, !tbaa !7
  %78 = add nsw i32 %77, 1
  store i32 %78, i32* %76, align 4, !tbaa !7
  %79 = load i32, i32* %46, align 4, !tbaa !7
  %80 = add nsw i32 %79, 1
  store i32 %80, i32* %46, align 4, !tbaa !7
  %81 = add nsw i64 %70, 1
  %82 = icmp slt i64 %81, %61
  br i1 %82, label %69, label %90

; <label>:83:                                     ; preds = %69
  %84 = trunc i64 %70 to i32
  %85 = zext i32 %72 to i64
  %86 = icmp eq i64 %25, %85
  br i1 %86, label %87, label %92

; <label>:87:                                     ; preds = %83
  %88 = add nsw i32 %84, 1
  %89 = add nsw i32 %40, 1
  br label %92

; <label>:90:                                     ; preds = %75
  %91 = trunc i64 %81 to i32
  br label %92

; <label>:92:                                     ; preds = %90, %45, %83, %87
  %93 = phi i32 [ %88, %87 ], [ %84, %83 ], [ %57, %45 ], [ %91, %90 ]
  %94 = phi i32 [ %89, %87 ], [ %40, %83 ], [ %40, %45 ], [ %40, %90 ]
  store i32 %93, i32* %56, align 4, !tbaa !7
  %95 = icmp slt i64 %51, %37
  br i1 %95, label %38, label %96

; <label>:96:                                     ; preds = %92
  %97 = trunc i64 %51 to i32
  br label %98

; <label>:98:                                     ; preds = %96, %24, %62, %66
  %99 = phi i32 [ %40, %66 ], [ %40, %62 ], [ %26, %24 ], [ %94, %96 ]
  %100 = phi i32 [ %68, %66 ], [ %27, %62 ], [ %27, %24 ], [ %27, %96 ]
  %101 = phi i32 [ %67, %66 ], [ %63, %62 ], [ %29, %24 ], [ %97, %96 ]
  %102 = getelementptr inbounds i32, i32* %4, i64 %25
  store i32 %101, i32* %102, align 4, !tbaa !7
  %103 = icmp eq i64 %30, %23
  br i1 %103, label %104, label %24

; <label>:104:                                    ; preds = %98
  br i1 %12, label %105, label %134

; <label>:105:                                    ; preds = %104
  %106 = zext i32 %0 to i64
  br label %107

; <label>:107:                                    ; preds = %132, %105
  %108 = phi i64 [ 0, %105 ], [ %111, %132 ]
  %109 = getelementptr inbounds i32, i32* %4, i64 %108
  %110 = load i32, i32* %109, align 4, !tbaa !7
  %111 = add nuw nsw i64 %108, 1
  %112 = getelementptr inbounds i32, i32* %1, i64 %111
  %113 = load i32, i32* %112, align 4, !tbaa !7
  %114 = icmp slt i32 %110, %113
  br i1 %114, label %115, label %132

; <label>:115:                                    ; preds = %107
  %116 = getelementptr inbounds i32, i32* %3, i64 %108
  %117 = sext i32 %110 to i64
  br label %118

; <label>:118:                                    ; preds = %115, %118
  %119 = phi i64 [ %117, %115 ], [ %128, %118 ]
  %120 = getelementptr inbounds i32, i32* %2, i64 %119
  %121 = load i32, i32* %120, align 4, !tbaa !7
  %122 = sext i32 %121 to i64
  %123 = getelementptr inbounds i32, i32* %3, i64 %122
  %124 = load i32, i32* %123, align 4, !tbaa !7
  %125 = add nsw i32 %124, 1
  store i32 %125, i32* %123, align 4, !tbaa !7
  %126 = load i32, i32* %116, align 4, !tbaa !7
  %127 = add nsw i32 %126, 1
  store i32 %127, i32* %116, align 4, !tbaa !7
  %128 = add nsw i64 %119, 1
  %129 = load i32, i32* %112, align 4, !tbaa !7
  %130 = sext i32 %129 to i64
  %131 = icmp slt i64 %128, %130
  br i1 %131, label %118, label %132

; <label>:132:                                    ; preds = %118, %107
  %133 = icmp eq i64 %111, %106
  br i1 %133, label %134, label %107

; <label>:134:                                    ; preds = %132, %13, %104
  %135 = phi i32 [ 0, %13 ], [ %99, %104 ], [ %99, %132 ]
  %136 = phi i32 [ 0, %13 ], [ %100, %104 ], [ %100, %132 ]
  %137 = phi i32 [ %16, %13 ], [ %22, %104 ], [ %22, %132 ]
  %138 = icmp eq i32 %137, %136
  br i1 %138, label %145, label %139

; <label>:139:                                    ; preds = %134
  %140 = sitofp i32 %135 to double
  %141 = fmul double %140, 2.000000e+00
  %142 = sub nsw i32 %137, %136
  %143 = sitofp i32 %142 to double
  %144 = fdiv double %141, %143
  br label %145

; <label>:145:                                    ; preds = %134, %139
  %146 = phi double [ %144, %139 ], [ 1.000000e+00, %134 ]
  br i1 %12, label %147, label %256

; <label>:147:                                    ; preds = %145
  %148 = zext i32 %0 to i64
  %149 = icmp ult i32 %0, 4
  br i1 %149, label %244, label %150

; <label>:150:                                    ; preds = %147
  %151 = and i64 %148, 4294967292
  %152 = add nsw i64 %151, -4
  %153 = lshr exact i64 %152, 2
  %154 = add nuw nsw i64 %153, 1
  %155 = and i64 %154, 3
  %156 = icmp ult i64 %152, 12
  br i1 %156, label %210, label %157

; <label>:157:                                    ; preds = %150
  %158 = sub nsw i64 %154, %155
  br label %159

; <label>:159:                                    ; preds = %159, %157
  %160 = phi i64 [ 0, %157 ], [ %207, %159 ]
  %161 = phi <2 x i64> [ zeroinitializer, %157 ], [ %205, %159 ]
  %162 = phi <2 x i64> [ zeroinitializer, %157 ], [ %206, %159 ]
  %163 = phi i64 [ %158, %157 ], [ %208, %159 ]
  %164 = getelementptr inbounds i32, i32* %3, i64 %160
  %165 = bitcast i32* %164 to <2 x i32>*
  %166 = load <2 x i32>, <2 x i32>* %165, align 4, !tbaa !7
  %167 = getelementptr i32, i32* %164, i64 2
  %168 = bitcast i32* %167 to <2 x i32>*
  %169 = load <2 x i32>, <2 x i32>* %168, align 4, !tbaa !7
  %170 = sext <2 x i32> %166 to <2 x i64>
  %171 = sext <2 x i32> %169 to <2 x i64>
  %172 = add <2 x i64> %161, %170
  %173 = add <2 x i64> %162, %171
  %174 = or i64 %160, 4
  %175 = getelementptr inbounds i32, i32* %3, i64 %174
  %176 = bitcast i32* %175 to <2 x i32>*
  %177 = load <2 x i32>, <2 x i32>* %176, align 4, !tbaa !7
  %178 = getelementptr i32, i32* %175, i64 2
  %179 = bitcast i32* %178 to <2 x i32>*
  %180 = load <2 x i32>, <2 x i32>* %179, align 4, !tbaa !7
  %181 = sext <2 x i32> %177 to <2 x i64>
  %182 = sext <2 x i32> %180 to <2 x i64>
  %183 = add <2 x i64> %172, %181
  %184 = add <2 x i64> %173, %182
  %185 = or i64 %160, 8
  %186 = getelementptr inbounds i32, i32* %3, i64 %185
  %187 = bitcast i32* %186 to <2 x i32>*
  %188 = load <2 x i32>, <2 x i32>* %187, align 4, !tbaa !7
  %189 = getelementptr i32, i32* %186, i64 2
  %190 = bitcast i32* %189 to <2 x i32>*
  %191 = load <2 x i32>, <2 x i32>* %190, align 4, !tbaa !7
  %192 = sext <2 x i32> %188 to <2 x i64>
  %193 = sext <2 x i32> %191 to <2 x i64>
  %194 = add <2 x i64> %183, %192
  %195 = add <2 x i64> %184, %193
  %196 = or i64 %160, 12
  %197 = getelementptr inbounds i32, i32* %3, i64 %196
  %198 = bitcast i32* %197 to <2 x i32>*
  %199 = load <2 x i32>, <2 x i32>* %198, align 4, !tbaa !7
  %200 = getelementptr i32, i32* %197, i64 2
  %201 = bitcast i32* %200 to <2 x i32>*
  %202 = load <2 x i32>, <2 x i32>* %201, align 4, !tbaa !7
  %203 = sext <2 x i32> %199 to <2 x i64>
  %204 = sext <2 x i32> %202 to <2 x i64>
  %205 = add <2 x i64> %194, %203
  %206 = add <2 x i64> %195, %204
  %207 = add i64 %160, 16
  %208 = add i64 %163, -4
  %209 = icmp eq i64 %208, 0
  br i1 %209, label %210, label %159, !llvm.loop !9

; <label>:210:                                    ; preds = %159, %150
  %211 = phi <2 x i64> [ undef, %150 ], [ %205, %159 ]
  %212 = phi <2 x i64> [ undef, %150 ], [ %206, %159 ]
  %213 = phi i64 [ 0, %150 ], [ %207, %159 ]
  %214 = phi <2 x i64> [ zeroinitializer, %150 ], [ %205, %159 ]
  %215 = phi <2 x i64> [ zeroinitializer, %150 ], [ %206, %159 ]
  %216 = icmp eq i64 %155, 0
  br i1 %216, label %236, label %217

; <label>:217:                                    ; preds = %210
  br label %218

; <label>:218:                                    ; preds = %218, %217
  %219 = phi i64 [ %213, %217 ], [ %233, %218 ]
  %220 = phi <2 x i64> [ %214, %217 ], [ %231, %218 ]
  %221 = phi <2 x i64> [ %215, %217 ], [ %232, %218 ]
  %222 = phi i64 [ %155, %217 ], [ %234, %218 ]
  %223 = getelementptr inbounds i32, i32* %3, i64 %219
  %224 = bitcast i32* %223 to <2 x i32>*
  %225 = load <2 x i32>, <2 x i32>* %224, align 4, !tbaa !7
  %226 = getelementptr i32, i32* %223, i64 2
  %227 = bitcast i32* %226 to <2 x i32>*
  %228 = load <2 x i32>, <2 x i32>* %227, align 4, !tbaa !7
  %229 = sext <2 x i32> %225 to <2 x i64>
  %230 = sext <2 x i32> %228 to <2 x i64>
  %231 = add <2 x i64> %220, %229
  %232 = add <2 x i64> %221, %230
  %233 = add i64 %219, 4
  %234 = add i64 %222, -1
  %235 = icmp eq i64 %234, 0
  br i1 %235, label %236, label %218, !llvm.loop !11

; <label>:236:                                    ; preds = %218, %210
  %237 = phi <2 x i64> [ %211, %210 ], [ %231, %218 ]
  %238 = phi <2 x i64> [ %212, %210 ], [ %232, %218 ]
  %239 = add <2 x i64> %238, %237
  %240 = shufflevector <2 x i64> %239, <2 x i64> undef, <2 x i32> <i32 1, i32 undef>
  %241 = add <2 x i64> %239, %240
  %242 = extractelement <2 x i64> %241, i32 0
  %243 = icmp eq i64 %151, %148
  br i1 %243, label %256, label %244

; <label>:244:                                    ; preds = %236, %147
  %245 = phi i64 [ 0, %147 ], [ %151, %236 ]
  %246 = phi i64 [ 0, %147 ], [ %242, %236 ]
  br label %247

; <label>:247:                                    ; preds = %244, %247
  %248 = phi i64 [ %254, %247 ], [ %245, %244 ]
  %249 = phi i64 [ %253, %247 ], [ %246, %244 ]
  %250 = getelementptr inbounds i32, i32* %3, i64 %248
  %251 = load i32, i32* %250, align 4, !tbaa !7
  %252 = sext i32 %251 to i64
  %253 = add i64 %249, %252
  %254 = add nuw nsw i64 %248, 1
  %255 = icmp eq i64 %254, %148
  br i1 %255, label %256, label %247, !llvm.loop !13

; <label>:256:                                    ; preds = %247, %236, %145
  %257 = phi i64 [ 0, %145 ], [ %242, %236 ], [ %253, %247 ]
  br i1 %8, label %258, label %268

; <label>:258:                                    ; preds = %256
  store double 0.000000e+00, double* %5, align 8, !tbaa !3
  %259 = sitofp i32 %0 to double
  %260 = getelementptr inbounds double, double* %5, i64 1
  store double %259, double* %260, align 8, !tbaa !3
  %261 = sitofp i32 %137 to double
  %262 = getelementptr inbounds double, double* %5, i64 2
  store double %261, double* %262, align 8, !tbaa !3
  %263 = getelementptr inbounds double, double* %5, i64 3
  store double %146, double* %263, align 8, !tbaa !3
  %264 = sitofp i32 %136 to double
  %265 = getelementptr inbounds double, double* %5, i64 4
  store double %264, double* %265, align 8, !tbaa !3
  %266 = uitofp i64 %257 to double
  %267 = getelementptr inbounds double, double* %5, i64 5
  store double %266, double* %267, align 8, !tbaa !3
  br label %268

; <label>:268:                                    ; preds = %258, %256
  ret i64 %257
}

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

; Function Attrs: argmemonly
declare void @memset_pattern16(i8* nocapture, i8* nocapture readonly, i64) local_unnamed_addr #2

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { argmemonly }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"double", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !8, i64 0}
!8 = !{!"int", !5, i64 0}
!9 = distinct !{!9, !10}
!10 = !{!"llvm.loop.isvectorized", i32 1}
!11 = distinct !{!11, !12}
!12 = !{!"llvm.loop.unroll.disable"}
!13 = distinct !{!13, !14, !10}
!14 = !{!"llvm.loop.unroll.runtime.disable"}
