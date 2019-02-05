; ModuleID = 'amd_postorder.c'
source_filename = "amd_postorder.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: nounwind ssp uwtable
define void @amd_postorder(i32, i32* nocapture readonly, i32* nocapture readonly, i32* nocapture readonly, i32*, i32*, i32*, i32*) local_unnamed_addr #0 {
  %9 = bitcast i32* %4 to i8*
  %10 = icmp sgt i32 %0, 0
  br i1 %10, label %11, label %210

; <label>:11:                                     ; preds = %8
  %12 = zext i32 %0 to i64
  %13 = icmp ult i32 %0, 8
  br i1 %13, label %90, label %14

; <label>:14:                                     ; preds = %11
  %15 = getelementptr i32, i32* %5, i64 %12
  %16 = getelementptr i32, i32* %6, i64 %12
  %17 = icmp ugt i32* %16, %5
  %18 = icmp ugt i32* %15, %6
  %19 = and i1 %17, %18
  br i1 %19, label %90, label %20

; <label>:20:                                     ; preds = %14
  %21 = and i64 %12, 4294967288
  %22 = add nsw i64 %21, -8
  %23 = lshr exact i64 %22, 3
  %24 = add nuw nsw i64 %23, 1
  %25 = and i64 %24, 3
  %26 = icmp ult i64 %22, 24
  br i1 %26, label %70, label %27

; <label>:27:                                     ; preds = %20
  %28 = sub nsw i64 %24, %25
  br label %29

; <label>:29:                                     ; preds = %29, %27
  %30 = phi i64 [ 0, %27 ], [ %67, %29 ]
  %31 = phi i64 [ %28, %27 ], [ %68, %29 ]
  %32 = getelementptr inbounds i32, i32* %5, i64 %30
  %33 = bitcast i32* %32 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %33, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %34 = getelementptr i32, i32* %32, i64 4
  %35 = bitcast i32* %34 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %35, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %36 = getelementptr inbounds i32, i32* %6, i64 %30
  %37 = bitcast i32* %36 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %37, align 4, !tbaa !3, !alias.scope !10
  %38 = getelementptr i32, i32* %36, i64 4
  %39 = bitcast i32* %38 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %39, align 4, !tbaa !3, !alias.scope !10
  %40 = or i64 %30, 8
  %41 = getelementptr inbounds i32, i32* %5, i64 %40
  %42 = bitcast i32* %41 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %42, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %43 = getelementptr i32, i32* %41, i64 4
  %44 = bitcast i32* %43 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %44, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %45 = getelementptr inbounds i32, i32* %6, i64 %40
  %46 = bitcast i32* %45 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %46, align 4, !tbaa !3, !alias.scope !10
  %47 = getelementptr i32, i32* %45, i64 4
  %48 = bitcast i32* %47 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %48, align 4, !tbaa !3, !alias.scope !10
  %49 = or i64 %30, 16
  %50 = getelementptr inbounds i32, i32* %5, i64 %49
  %51 = bitcast i32* %50 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %51, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %52 = getelementptr i32, i32* %50, i64 4
  %53 = bitcast i32* %52 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %53, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %54 = getelementptr inbounds i32, i32* %6, i64 %49
  %55 = bitcast i32* %54 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %55, align 4, !tbaa !3, !alias.scope !10
  %56 = getelementptr i32, i32* %54, i64 4
  %57 = bitcast i32* %56 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %57, align 4, !tbaa !3, !alias.scope !10
  %58 = or i64 %30, 24
  %59 = getelementptr inbounds i32, i32* %5, i64 %58
  %60 = bitcast i32* %59 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %60, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %61 = getelementptr i32, i32* %59, i64 4
  %62 = bitcast i32* %61 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %62, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %63 = getelementptr inbounds i32, i32* %6, i64 %58
  %64 = bitcast i32* %63 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %64, align 4, !tbaa !3, !alias.scope !10
  %65 = getelementptr i32, i32* %63, i64 4
  %66 = bitcast i32* %65 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %66, align 4, !tbaa !3, !alias.scope !10
  %67 = add i64 %30, 32
  %68 = add i64 %31, -4
  %69 = icmp eq i64 %68, 0
  br i1 %69, label %70, label %29, !llvm.loop !12

; <label>:70:                                     ; preds = %29, %20
  %71 = phi i64 [ 0, %20 ], [ %67, %29 ]
  %72 = icmp eq i64 %25, 0
  br i1 %72, label %88, label %73

; <label>:73:                                     ; preds = %70
  br label %74

; <label>:74:                                     ; preds = %74, %73
  %75 = phi i64 [ %71, %73 ], [ %85, %74 ]
  %76 = phi i64 [ %25, %73 ], [ %86, %74 ]
  %77 = getelementptr inbounds i32, i32* %5, i64 %75
  %78 = bitcast i32* %77 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %78, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %79 = getelementptr i32, i32* %77, i64 4
  %80 = bitcast i32* %79 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %80, align 4, !tbaa !3, !alias.scope !7, !noalias !10
  %81 = getelementptr inbounds i32, i32* %6, i64 %75
  %82 = bitcast i32* %81 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %82, align 4, !tbaa !3, !alias.scope !10
  %83 = getelementptr i32, i32* %81, i64 4
  %84 = bitcast i32* %83 to <4 x i32>*
  store <4 x i32> <i32 -1, i32 -1, i32 -1, i32 -1>, <4 x i32>* %84, align 4, !tbaa !3, !alias.scope !10
  %85 = add i64 %75, 8
  %86 = add i64 %76, -1
  %87 = icmp eq i64 %86, 0
  br i1 %87, label %88, label %74, !llvm.loop !14

; <label>:88:                                     ; preds = %74, %70
  %89 = icmp eq i64 %21, %12
  br i1 %89, label %124, label %90

; <label>:90:                                     ; preds = %88, %14, %11
  %91 = phi i64 [ 0, %14 ], [ 0, %11 ], [ %21, %88 ]
  %92 = add nsw i64 %12, -1
  %93 = sub nsw i64 %92, %91
  %94 = and i64 %12, 3
  %95 = icmp eq i64 %94, 0
  br i1 %95, label %105, label %96

; <label>:96:                                     ; preds = %90
  br label %97

; <label>:97:                                     ; preds = %97, %96
  %98 = phi i64 [ %102, %97 ], [ %91, %96 ]
  %99 = phi i64 [ %103, %97 ], [ %94, %96 ]
  %100 = getelementptr inbounds i32, i32* %5, i64 %98
  store i32 -1, i32* %100, align 4, !tbaa !3
  %101 = getelementptr inbounds i32, i32* %6, i64 %98
  store i32 -1, i32* %101, align 4, !tbaa !3
  %102 = add nuw nsw i64 %98, 1
  %103 = add i64 %99, -1
  %104 = icmp eq i64 %103, 0
  br i1 %104, label %105, label %97, !llvm.loop !16

; <label>:105:                                    ; preds = %97, %90
  %106 = phi i64 [ %91, %90 ], [ %102, %97 ]
  %107 = icmp ult i64 %93, 3
  br i1 %107, label %124, label %108

; <label>:108:                                    ; preds = %105
  br label %109

; <label>:109:                                    ; preds = %109, %108
  %110 = phi i64 [ %106, %108 ], [ %122, %109 ]
  %111 = getelementptr inbounds i32, i32* %5, i64 %110
  store i32 -1, i32* %111, align 4, !tbaa !3
  %112 = getelementptr inbounds i32, i32* %6, i64 %110
  store i32 -1, i32* %112, align 4, !tbaa !3
  %113 = add nuw nsw i64 %110, 1
  %114 = getelementptr inbounds i32, i32* %5, i64 %113
  store i32 -1, i32* %114, align 4, !tbaa !3
  %115 = getelementptr inbounds i32, i32* %6, i64 %113
  store i32 -1, i32* %115, align 4, !tbaa !3
  %116 = add nsw i64 %110, 2
  %117 = getelementptr inbounds i32, i32* %5, i64 %116
  store i32 -1, i32* %117, align 4, !tbaa !3
  %118 = getelementptr inbounds i32, i32* %6, i64 %116
  store i32 -1, i32* %118, align 4, !tbaa !3
  %119 = add nsw i64 %110, 3
  %120 = getelementptr inbounds i32, i32* %5, i64 %119
  store i32 -1, i32* %120, align 4, !tbaa !3
  %121 = getelementptr inbounds i32, i32* %6, i64 %119
  store i32 -1, i32* %121, align 4, !tbaa !3
  %122 = add nsw i64 %110, 4
  %123 = icmp eq i64 %122, %12
  br i1 %123, label %124, label %109, !llvm.loop !17

; <label>:124:                                    ; preds = %105, %109, %88
  br i1 %10, label %125, label %210

; <label>:125:                                    ; preds = %124
  %126 = sext i32 %0 to i64
  br label %127

; <label>:127:                                    ; preds = %125, %143
  %128 = phi i64 [ %126, %125 ], [ %129, %143 ]
  %129 = add nsw i64 %128, -1
  %130 = getelementptr inbounds i32, i32* %2, i64 %129
  %131 = load i32, i32* %130, align 4, !tbaa !3
  %132 = icmp sgt i32 %131, 0
  br i1 %132, label %133, label %143

; <label>:133:                                    ; preds = %127
  %134 = getelementptr inbounds i32, i32* %1, i64 %129
  %135 = load i32, i32* %134, align 4, !tbaa !3
  %136 = icmp eq i32 %135, -1
  br i1 %136, label %143, label %137

; <label>:137:                                    ; preds = %133
  %138 = sext i32 %135 to i64
  %139 = getelementptr inbounds i32, i32* %5, i64 %138
  %140 = load i32, i32* %139, align 4, !tbaa !3
  %141 = getelementptr inbounds i32, i32* %6, i64 %129
  store i32 %140, i32* %141, align 4, !tbaa !3
  %142 = trunc i64 %129 to i32
  store i32 %142, i32* %139, align 4, !tbaa !3
  br label %143

; <label>:143:                                    ; preds = %133, %127, %137
  %144 = icmp sgt i64 %128, 1
  br i1 %144, label %127, label %145

; <label>:145:                                    ; preds = %143
  br i1 %10, label %146, label %210

; <label>:146:                                    ; preds = %145
  %147 = zext i32 %0 to i64
  br label %148

; <label>:148:                                    ; preds = %185, %146
  %149 = phi i64 [ 0, %146 ], [ %186, %185 ]
  %150 = getelementptr inbounds i32, i32* %2, i64 %149
  %151 = load i32, i32* %150, align 4, !tbaa !3
  %152 = icmp sgt i32 %151, 0
  br i1 %152, label %153, label %185

; <label>:153:                                    ; preds = %148
  %154 = getelementptr inbounds i32, i32* %5, i64 %149
  %155 = load i32, i32* %154, align 4, !tbaa !3
  %156 = icmp eq i32 %155, -1
  br i1 %156, label %185, label %157

; <label>:157:                                    ; preds = %153
  br label %158

; <label>:158:                                    ; preds = %157, %158
  %159 = phi i32 [ %170, %158 ], [ -1, %157 ]
  %160 = phi i32 [ %169, %158 ], [ -1, %157 ]
  %161 = phi i32 [ %168, %158 ], [ -1, %157 ]
  %162 = phi i32 [ %163, %158 ], [ -1, %157 ]
  %163 = phi i32 [ %172, %158 ], [ %155, %157 ]
  %164 = sext i32 %163 to i64
  %165 = getelementptr inbounds i32, i32* %3, i64 %164
  %166 = load i32, i32* %165, align 4, !tbaa !3
  %167 = icmp slt i32 %166, %161
  %168 = select i1 %167, i32 %161, i32 %166
  %169 = select i1 %167, i32 %160, i32 %162
  %170 = select i1 %167, i32 %159, i32 %163
  %171 = getelementptr inbounds i32, i32* %6, i64 %164
  %172 = load i32, i32* %171, align 4, !tbaa !3
  %173 = icmp eq i32 %172, -1
  br i1 %173, label %174, label %158

; <label>:174:                                    ; preds = %158
  %175 = sext i32 %170 to i64
  %176 = getelementptr inbounds i32, i32* %6, i64 %175
  %177 = load i32, i32* %176, align 4, !tbaa !3
  %178 = icmp eq i32 %177, -1
  br i1 %178, label %185, label %179

; <label>:179:                                    ; preds = %174
  %180 = getelementptr inbounds i32, i32* %6, i64 %164
  %181 = icmp eq i32 %169, -1
  %182 = sext i32 %169 to i64
  %183 = getelementptr inbounds i32, i32* %6, i64 %182
  %184 = select i1 %181, i32* %154, i32* %183
  store i32 %177, i32* %184, align 4, !tbaa !3
  store i32 -1, i32* %176, align 4, !tbaa !3
  store i32 %170, i32* %180, align 4, !tbaa !3
  br label %185

; <label>:185:                                    ; preds = %174, %153, %148, %179
  %186 = add nuw nsw i64 %149, 1
  %187 = icmp eq i64 %186, %147
  br i1 %187, label %188, label %148

; <label>:188:                                    ; preds = %185
  br i1 %10, label %189, label %210

; <label>:189:                                    ; preds = %188
  %190 = zext i32 %0 to i64
  %191 = shl nuw nsw i64 %190, 2
  call void @llvm.memset.p0i8.i64(i8* %9, i8 -1, i64 %191, i32 4, i1 false)
  %192 = zext i32 %0 to i64
  br label %193

; <label>:193:                                    ; preds = %206, %189
  %194 = phi i64 [ 0, %189 ], [ %208, %206 ]
  %195 = phi i32 [ 0, %189 ], [ %207, %206 ]
  %196 = getelementptr inbounds i32, i32* %1, i64 %194
  %197 = load i32, i32* %196, align 4, !tbaa !3
  %198 = icmp eq i32 %197, -1
  br i1 %198, label %199, label %206

; <label>:199:                                    ; preds = %193
  %200 = getelementptr inbounds i32, i32* %2, i64 %194
  %201 = load i32, i32* %200, align 4, !tbaa !3
  %202 = icmp sgt i32 %201, 0
  br i1 %202, label %203, label %206

; <label>:203:                                    ; preds = %199
  %204 = trunc i64 %194 to i32
  %205 = tail call i32 @amd_post_tree(i32 %204, i32 %195, i32* %5, i32* %6, i32* %4, i32* %7) #3
  br label %206

; <label>:206:                                    ; preds = %193, %199, %203
  %207 = phi i32 [ %205, %203 ], [ %195, %199 ], [ %195, %193 ]
  %208 = add nuw nsw i64 %194, 1
  %209 = icmp eq i64 %208, %192
  br i1 %209, label %210, label %193

; <label>:210:                                    ; preds = %206, %8, %124, %145, %188
  ret void
}

declare i32 @amd_post_tree(i32, i32, i32*, i32*, i32*, i32*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { argmemonly nounwind }
attributes #3 = { nounwind }

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
