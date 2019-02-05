; ModuleID = 'klu_scale.c'
source_filename = "klu_scale.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_scale(i32, i32, i32* readonly, i32* readonly, double* readonly, double*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %9 = bitcast i32* %6 to i8*
  %10 = bitcast double* %5 to i8*
  %11 = icmp eq %struct.klu_common_struct* %7, null
  br i1 %11, label %180, label %12

; <label>:12:                                     ; preds = %8
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %7, i64 0, i32 11
  store i32 0, i32* %13, align 4, !tbaa !3
  %14 = icmp slt i32 %0, 0
  br i1 %14, label %180, label %15

; <label>:15:                                     ; preds = %12
  %16 = icmp slt i32 %1, 1
  %17 = icmp eq i32* %2, null
  %18 = or i1 %16, %17
  %19 = icmp eq i32* %3, null
  %20 = or i1 %18, %19
  %21 = icmp eq double* %4, null
  %22 = or i1 %20, %21
  br i1 %22, label %27, label %23

; <label>:23:                                     ; preds = %15
  %24 = icmp sgt i32 %0, 0
  %25 = icmp eq double* %5, null
  %26 = and i1 %24, %25
  br i1 %26, label %27, label %28

; <label>:27:                                     ; preds = %23, %15
  store i32 -3, i32* %13, align 4, !tbaa !3
  br label %180

; <label>:28:                                     ; preds = %23
  %29 = load i32, i32* %2, align 4, !tbaa !11
  %30 = icmp eq i32 %29, 0
  br i1 %30, label %31, label %37

; <label>:31:                                     ; preds = %28
  %32 = sext i32 %1 to i64
  %33 = getelementptr inbounds i32, i32* %2, i64 %32
  %34 = load i32, i32* %33, align 4, !tbaa !11
  %35 = icmp slt i32 %34, 0
  br i1 %35, label %37, label %36

; <label>:36:                                     ; preds = %31
  br label %40

; <label>:37:                                     ; preds = %28, %31
  store i32 -3, i32* %13, align 4, !tbaa !3
  br label %180

; <label>:38:                                     ; preds = %40
  %39 = icmp slt i64 %43, %32
  br i1 %39, label %40, label %48

; <label>:40:                                     ; preds = %36, %38
  %41 = phi i32 [ %45, %38 ], [ 0, %36 ]
  %42 = phi i64 [ %43, %38 ], [ 0, %36 ]
  %43 = add nuw nsw i64 %42, 1
  %44 = getelementptr inbounds i32, i32* %2, i64 %43
  %45 = load i32, i32* %44, align 4, !tbaa !11
  %46 = icmp sgt i32 %41, %45
  br i1 %46, label %47, label %38

; <label>:47:                                     ; preds = %40
  store i32 -3, i32* %13, align 4, !tbaa !3
  br label %180

; <label>:48:                                     ; preds = %38
  br i1 %24, label %49, label %52

; <label>:49:                                     ; preds = %48
  %50 = zext i32 %1 to i64
  %51 = shl nuw nsw i64 %50, 3
  call void @llvm.memset.p0i8.i64(i8* %10, i8 0, i64 %51, i32 8, i1 false)
  br label %52

; <label>:52:                                     ; preds = %49, %48
  %53 = icmp ne i32* %6, null
  br i1 %53, label %54, label %57

; <label>:54:                                     ; preds = %52
  %55 = zext i32 %1 to i64
  %56 = shl nuw nsw i64 %55, 2
  call void @llvm.memset.p0i8.i64(i8* %9, i8 -1, i64 %56, i32 4, i1 false)
  br label %57

; <label>:57:                                     ; preds = %52, %54
  %58 = icmp eq i32 %0, 1
  %59 = icmp sgt i32 %0, 1
  br label %60

; <label>:60:                                     ; preds = %57, %151
  %61 = phi i64 [ 0, %57 ], [ %62, %151 ]
  %62 = add nuw nsw i64 %61, 1
  %63 = getelementptr inbounds i32, i32* %2, i64 %62
  %64 = load i32, i32* %63, align 4, !tbaa !11
  %65 = getelementptr inbounds i32, i32* %2, i64 %61
  %66 = load i32, i32* %65, align 4, !tbaa !11
  %67 = icmp slt i32 %66, %64
  br i1 %67, label %68, label %151

; <label>:68:                                     ; preds = %60
  %69 = sext i32 %66 to i64
  %70 = sext i32 %64 to i64
  br i1 %53, label %71, label %105

; <label>:71:                                     ; preds = %68
  %72 = trunc i64 %61 to i32
  br label %73

; <label>:73:                                     ; preds = %102, %71
  %74 = phi i64 [ %103, %102 ], [ %69, %71 ]
  %75 = getelementptr inbounds i32, i32* %3, i64 %74
  %76 = load i32, i32* %75, align 4, !tbaa !11
  %77 = icmp sgt i32 %76, -1
  %78 = icmp slt i32 %76, %1
  %79 = and i1 %77, %78
  br i1 %79, label %80, label %134

; <label>:80:                                     ; preds = %73
  %81 = sext i32 %76 to i64
  %82 = getelementptr inbounds i32, i32* %6, i64 %81
  %83 = load i32, i32* %82, align 4, !tbaa !11
  %84 = zext i32 %83 to i64
  %85 = icmp eq i64 %61, %84
  br i1 %85, label %135, label %86

; <label>:86:                                     ; preds = %80
  store i32 %72, i32* %82, align 4, !tbaa !11
  %87 = getelementptr inbounds double, double* %4, i64 %74
  %88 = load double, double* %87, align 8, !tbaa !12
  %89 = fcmp olt double %88, 0.000000e+00
  %90 = fsub double -0.000000e+00, %88
  %91 = select i1 %89, double %90, double %88
  br i1 %58, label %98, label %92

; <label>:92:                                     ; preds = %86
  br i1 %59, label %93, label %102

; <label>:93:                                     ; preds = %92
  %94 = getelementptr inbounds double, double* %5, i64 %81
  %95 = load double, double* %94, align 8, !tbaa !12
  %96 = fcmp ogt double %95, %91
  %97 = select i1 %96, double %95, double %91
  store double %97, double* %94, align 8, !tbaa !12
  br label %102

; <label>:98:                                     ; preds = %86
  %99 = getelementptr inbounds double, double* %5, i64 %81
  %100 = load double, double* %99, align 8, !tbaa !12
  %101 = fadd double %91, %100
  store double %101, double* %99, align 8, !tbaa !12
  br label %102

; <label>:102:                                    ; preds = %98, %93, %92
  %103 = add nsw i64 %74, 1
  %104 = icmp slt i64 %103, %70
  br i1 %104, label %73, label %151

; <label>:105:                                    ; preds = %68
  br i1 %58, label %107, label %106

; <label>:106:                                    ; preds = %105
  br label %127

; <label>:107:                                    ; preds = %105
  br label %108

; <label>:108:                                    ; preds = %107, %115
  %109 = phi i64 [ %125, %115 ], [ %69, %107 ]
  %110 = getelementptr inbounds i32, i32* %3, i64 %109
  %111 = load i32, i32* %110, align 4, !tbaa !11
  %112 = icmp sgt i32 %111, -1
  %113 = icmp slt i32 %111, %1
  %114 = and i1 %112, %113
  br i1 %114, label %115, label %134

; <label>:115:                                    ; preds = %108
  %116 = getelementptr inbounds double, double* %4, i64 %109
  %117 = load double, double* %116, align 8, !tbaa !12
  %118 = fcmp olt double %117, 0.000000e+00
  %119 = fsub double -0.000000e+00, %117
  %120 = select i1 %118, double %119, double %117
  %121 = sext i32 %111 to i64
  %122 = getelementptr inbounds double, double* %5, i64 %121
  %123 = load double, double* %122, align 8, !tbaa !12
  %124 = fadd double %120, %123
  store double %124, double* %122, align 8, !tbaa !12
  %125 = add nsw i64 %109, 1
  %126 = icmp slt i64 %125, %70
  br i1 %126, label %108, label %151

; <label>:127:                                    ; preds = %106, %148
  %128 = phi i64 [ %149, %148 ], [ %69, %106 ]
  %129 = getelementptr inbounds i32, i32* %3, i64 %128
  %130 = load i32, i32* %129, align 4, !tbaa !11
  %131 = icmp sgt i32 %130, -1
  %132 = icmp slt i32 %130, %1
  %133 = and i1 %131, %132
  br i1 %133, label %136, label %134

; <label>:134:                                    ; preds = %127, %108, %73
  store i32 -3, i32* %13, align 4, !tbaa !3
  br label %180

; <label>:135:                                    ; preds = %80
  store i32 -3, i32* %13, align 4, !tbaa !3
  br label %180

; <label>:136:                                    ; preds = %127
  %137 = getelementptr inbounds double, double* %4, i64 %128
  %138 = load double, double* %137, align 8, !tbaa !12
  %139 = fcmp olt double %138, 0.000000e+00
  %140 = fsub double -0.000000e+00, %138
  %141 = select i1 %139, double %140, double %138
  br i1 %59, label %142, label %148

; <label>:142:                                    ; preds = %136
  %143 = sext i32 %130 to i64
  %144 = getelementptr inbounds double, double* %5, i64 %143
  %145 = load double, double* %144, align 8, !tbaa !12
  %146 = fcmp ogt double %145, %141
  %147 = select i1 %146, double %145, double %141
  store double %147, double* %144, align 8, !tbaa !12
  br label %148

; <label>:148:                                    ; preds = %142, %136
  %149 = add nsw i64 %128, 1
  %150 = icmp slt i64 %149, %70
  br i1 %150, label %127, label %151

; <label>:151:                                    ; preds = %148, %115, %102, %60
  %152 = icmp slt i64 %62, %32
  br i1 %152, label %60, label %153

; <label>:153:                                    ; preds = %151
  br i1 %24, label %154, label %180

; <label>:154:                                    ; preds = %153
  %155 = zext i32 %1 to i64
  %156 = and i64 %155, 1
  %157 = icmp eq i32 %1, 1
  br i1 %157, label %172, label %158

; <label>:158:                                    ; preds = %154
  %159 = sub nsw i64 %155, %156
  br label %160

; <label>:160:                                    ; preds = %183, %158
  %161 = phi i64 [ 0, %158 ], [ %184, %183 ]
  %162 = phi i64 [ %159, %158 ], [ %185, %183 ]
  %163 = getelementptr inbounds double, double* %5, i64 %161
  %164 = load double, double* %163, align 8, !tbaa !12
  %165 = fcmp oeq double %164, 0.000000e+00
  br i1 %165, label %166, label %167

; <label>:166:                                    ; preds = %160
  store double 1.000000e+00, double* %163, align 8, !tbaa !12
  br label %167

; <label>:167:                                    ; preds = %160, %166
  %168 = or i64 %161, 1
  %169 = getelementptr inbounds double, double* %5, i64 %168
  %170 = load double, double* %169, align 8, !tbaa !12
  %171 = fcmp oeq double %170, 0.000000e+00
  br i1 %171, label %182, label %183

; <label>:172:                                    ; preds = %183, %154
  %173 = phi i64 [ 0, %154 ], [ %184, %183 ]
  %174 = icmp eq i64 %156, 0
  br i1 %174, label %180, label %175

; <label>:175:                                    ; preds = %172
  %176 = getelementptr inbounds double, double* %5, i64 %173
  %177 = load double, double* %176, align 8, !tbaa !12
  %178 = fcmp oeq double %177, 0.000000e+00
  br i1 %178, label %179, label %180

; <label>:179:                                    ; preds = %175
  store double 1.000000e+00, double* %176, align 8, !tbaa !12
  br label %180

; <label>:180:                                    ; preds = %172, %175, %179, %153, %12, %8, %135, %134, %47, %37, %27
  %181 = phi i32 [ 0, %27 ], [ 0, %37 ], [ 0, %47 ], [ 0, %134 ], [ 0, %135 ], [ 0, %8 ], [ 1, %12 ], [ 1, %153 ], [ 1, %179 ], [ 1, %175 ], [ 1, %172 ]
  ret i32 %181

; <label>:182:                                    ; preds = %167
  store double 1.000000e+00, double* %169, align 8, !tbaa !12
  br label %183

; <label>:183:                                    ; preds = %182, %167
  %184 = add nuw nsw i64 %161, 2
  %185 = add i64 %162, -2
  %186 = icmp eq i64 %185, 0
  br i1 %186, label %172, label %160
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
!3 = !{!4, !8, i64 76}
!4 = !{!"klu_common_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !8, i64 40, !8, i64 44, !8, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92, !8, i64 96, !5, i64 104, !5, i64 112, !5, i64 120, !5, i64 128, !5, i64 136, !10, i64 144, !10, i64 152}
!5 = !{!"double", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!"int", !6, i64 0}
!9 = !{!"any pointer", !6, i64 0}
!10 = !{!"long", !6, i64 0}
!11 = !{!8, !8, i64 0}
!12 = !{!5, !5, i64 0}
