; ModuleID = 'amd_1.c'
source_filename = "amd_1.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: nounwind ssp uwtable
define void @amd_1(i32, i32* nocapture readonly, i32* nocapture readonly, i32*, i32*, i32*, i32, i32*, double*, double*) local_unnamed_addr #0 {
  %11 = mul i32 %0, -6
  %12 = add i32 %11, %6
  %13 = sext i32 %0 to i64
  %14 = getelementptr inbounds i32, i32* %7, i64 %13
  %15 = getelementptr inbounds i32, i32* %14, i64 %13
  %16 = getelementptr inbounds i32, i32* %15, i64 %13
  %17 = getelementptr inbounds i32, i32* %16, i64 %13
  %18 = getelementptr inbounds i32, i32* %17, i64 %13
  %19 = getelementptr inbounds i32, i32* %18, i64 %13
  %20 = icmp sgt i32 %0, 0
  br i1 %20, label %21, label %176

; <label>:21:                                     ; preds = %10
  %22 = zext i32 %0 to i64
  %23 = and i64 %22, 1
  %24 = icmp eq i32 %0, 1
  br i1 %24, label %45, label %25

; <label>:25:                                     ; preds = %21
  %26 = sub nsw i64 %22, %23
  br label %27

; <label>:27:                                     ; preds = %27, %25
  %28 = phi i64 [ 0, %25 ], [ %42, %27 ]
  %29 = phi i32 [ 0, %25 ], [ %41, %27 ]
  %30 = phi i64 [ %26, %25 ], [ %43, %27 ]
  %31 = getelementptr inbounds i32, i32* %7, i64 %28
  store i32 %29, i32* %31, align 4, !tbaa !3
  %32 = getelementptr inbounds i32, i32* %14, i64 %28
  store i32 %29, i32* %32, align 4, !tbaa !3
  %33 = getelementptr inbounds i32, i32* %5, i64 %28
  %34 = load i32, i32* %33, align 4, !tbaa !3
  %35 = add nsw i32 %34, %29
  %36 = or i64 %28, 1
  %37 = getelementptr inbounds i32, i32* %7, i64 %36
  store i32 %35, i32* %37, align 4, !tbaa !3
  %38 = getelementptr inbounds i32, i32* %14, i64 %36
  store i32 %35, i32* %38, align 4, !tbaa !3
  %39 = getelementptr inbounds i32, i32* %5, i64 %36
  %40 = load i32, i32* %39, align 4, !tbaa !3
  %41 = add nsw i32 %40, %35
  %42 = add nuw nsw i64 %28, 2
  %43 = add i64 %30, -2
  %44 = icmp eq i64 %43, 0
  br i1 %44, label %45, label %27

; <label>:45:                                     ; preds = %27, %21
  %46 = phi i32 [ undef, %21 ], [ %41, %27 ]
  %47 = phi i64 [ 0, %21 ], [ %42, %27 ]
  %48 = phi i32 [ 0, %21 ], [ %41, %27 ]
  %49 = icmp eq i64 %23, 0
  br i1 %49, label %56, label %50

; <label>:50:                                     ; preds = %45
  %51 = getelementptr inbounds i32, i32* %7, i64 %47
  store i32 %48, i32* %51, align 4, !tbaa !3
  %52 = getelementptr inbounds i32, i32* %14, i64 %47
  store i32 %48, i32* %52, align 4, !tbaa !3
  %53 = getelementptr inbounds i32, i32* %5, i64 %47
  %54 = load i32, i32* %53, align 4, !tbaa !3
  %55 = add nsw i32 %54, %48
  br label %56

; <label>:56:                                     ; preds = %45, %50
  %57 = phi i32 [ %46, %45 ], [ %55, %50 ]
  br i1 %20, label %58, label %176

; <label>:58:                                     ; preds = %56
  %59 = zext i32 %0 to i64
  br label %60

; <label>:60:                                     ; preds = %137, %58
  %61 = phi i64 [ 0, %58 ], [ %64, %137 ]
  %62 = getelementptr inbounds i32, i32* %1, i64 %61
  %63 = load i32, i32* %62, align 4, !tbaa !3
  %64 = add nuw nsw i64 %61, 1
  %65 = getelementptr inbounds i32, i32* %1, i64 %64
  %66 = load i32, i32* %65, align 4, !tbaa !3
  %67 = icmp slt i32 %63, %66
  br i1 %67, label %68, label %137

; <label>:68:                                     ; preds = %60
  %69 = getelementptr inbounds i32, i32* %14, i64 %61
  %70 = sext i32 %63 to i64
  %71 = sext i32 %66 to i64
  %72 = trunc i64 %61 to i32
  br label %73

; <label>:73:                                     ; preds = %68, %132
  %74 = phi i64 [ %70, %68 ], [ %89, %132 ]
  %75 = getelementptr inbounds i32, i32* %2, i64 %74
  %76 = load i32, i32* %75, align 4, !tbaa !3
  %77 = sext i32 %76 to i64
  %78 = icmp sgt i64 %61, %77
  br i1 %78, label %79, label %100

; <label>:79:                                     ; preds = %73
  %80 = getelementptr inbounds i32, i32* %14, i64 %77
  %81 = load i32, i32* %80, align 4, !tbaa !3
  %82 = add nsw i32 %81, 1
  store i32 %82, i32* %80, align 4, !tbaa !3
  %83 = sext i32 %81 to i64
  %84 = getelementptr inbounds i32, i32* %19, i64 %83
  store i32 %72, i32* %84, align 4, !tbaa !3
  %85 = load i32, i32* %69, align 4, !tbaa !3
  %86 = add nsw i32 %85, 1
  store i32 %86, i32* %69, align 4, !tbaa !3
  %87 = sext i32 %85 to i64
  %88 = getelementptr inbounds i32, i32* %19, i64 %87
  store i32 %76, i32* %88, align 4, !tbaa !3
  %89 = add nsw i64 %74, 1
  %90 = add nsw i32 %76, 1
  %91 = sext i32 %90 to i64
  %92 = getelementptr inbounds i32, i32* %1, i64 %91
  %93 = load i32, i32* %92, align 4, !tbaa !3
  %94 = getelementptr inbounds i32, i32* %18, i64 %77
  %95 = load i32, i32* %94, align 4, !tbaa !3
  %96 = icmp slt i32 %95, %93
  br i1 %96, label %97, label %132

; <label>:97:                                     ; preds = %79
  %98 = sext i32 %95 to i64
  %99 = sext i32 %93 to i64
  br label %106

; <label>:100:                                    ; preds = %73
  %101 = trunc i64 %74 to i32
  %102 = zext i32 %76 to i64
  %103 = icmp eq i64 %61, %102
  %104 = zext i1 %103 to i32
  %105 = add nsw i32 %101, %104
  br label %137

; <label>:106:                                    ; preds = %97, %112
  %107 = phi i64 [ %98, %97 ], [ %122, %112 ]
  %108 = getelementptr inbounds i32, i32* %2, i64 %107
  %109 = load i32, i32* %108, align 4, !tbaa !3
  %110 = sext i32 %109 to i64
  %111 = icmp sgt i64 %61, %110
  br i1 %111, label %112, label %124

; <label>:112:                                    ; preds = %106
  %113 = getelementptr inbounds i32, i32* %14, i64 %110
  %114 = load i32, i32* %113, align 4, !tbaa !3
  %115 = add nsw i32 %114, 1
  store i32 %115, i32* %113, align 4, !tbaa !3
  %116 = sext i32 %114 to i64
  %117 = getelementptr inbounds i32, i32* %19, i64 %116
  store i32 %76, i32* %117, align 4, !tbaa !3
  %118 = load i32, i32* %80, align 4, !tbaa !3
  %119 = add nsw i32 %118, 1
  store i32 %119, i32* %80, align 4, !tbaa !3
  %120 = sext i32 %118 to i64
  %121 = getelementptr inbounds i32, i32* %19, i64 %120
  store i32 %109, i32* %121, align 4, !tbaa !3
  %122 = add nsw i64 %107, 1
  %123 = icmp slt i64 %122, %99
  br i1 %123, label %106, label %130

; <label>:124:                                    ; preds = %106
  %125 = trunc i64 %107 to i32
  %126 = zext i32 %109 to i64
  %127 = icmp eq i64 %61, %126
  %128 = zext i1 %127 to i32
  %129 = add nsw i32 %125, %128
  br label %132

; <label>:130:                                    ; preds = %112
  %131 = trunc i64 %122 to i32
  br label %132

; <label>:132:                                    ; preds = %130, %79, %124
  %133 = phi i32 [ %129, %124 ], [ %95, %79 ], [ %131, %130 ]
  store i32 %133, i32* %94, align 4, !tbaa !3
  %134 = icmp slt i64 %89, %71
  br i1 %134, label %73, label %135

; <label>:135:                                    ; preds = %132
  %136 = trunc i64 %89 to i32
  br label %137

; <label>:137:                                    ; preds = %135, %60, %100
  %138 = phi i32 [ %105, %100 ], [ %63, %60 ], [ %136, %135 ]
  %139 = getelementptr inbounds i32, i32* %18, i64 %61
  store i32 %138, i32* %139, align 4, !tbaa !3
  %140 = icmp eq i64 %64, %59
  br i1 %140, label %141, label %60

; <label>:141:                                    ; preds = %137
  br i1 %20, label %142, label %176

; <label>:142:                                    ; preds = %141
  %143 = zext i32 %0 to i64
  br label %144

; <label>:144:                                    ; preds = %174, %142
  %145 = phi i64 [ 0, %142 ], [ %148, %174 ]
  %146 = getelementptr inbounds i32, i32* %18, i64 %145
  %147 = load i32, i32* %146, align 4, !tbaa !3
  %148 = add nuw nsw i64 %145, 1
  %149 = getelementptr inbounds i32, i32* %1, i64 %148
  %150 = load i32, i32* %149, align 4, !tbaa !3
  %151 = icmp slt i32 %147, %150
  br i1 %151, label %152, label %174

; <label>:152:                                    ; preds = %144
  %153 = getelementptr inbounds i32, i32* %14, i64 %145
  %154 = sext i32 %147 to i64
  %155 = trunc i64 %145 to i32
  br label %156

; <label>:156:                                    ; preds = %152, %156
  %157 = phi i64 [ %154, %152 ], [ %170, %156 ]
  %158 = getelementptr inbounds i32, i32* %2, i64 %157
  %159 = load i32, i32* %158, align 4, !tbaa !3
  %160 = sext i32 %159 to i64
  %161 = getelementptr inbounds i32, i32* %14, i64 %160
  %162 = load i32, i32* %161, align 4, !tbaa !3
  %163 = add nsw i32 %162, 1
  store i32 %163, i32* %161, align 4, !tbaa !3
  %164 = sext i32 %162 to i64
  %165 = getelementptr inbounds i32, i32* %19, i64 %164
  store i32 %155, i32* %165, align 4, !tbaa !3
  %166 = load i32, i32* %153, align 4, !tbaa !3
  %167 = add nsw i32 %166, 1
  store i32 %167, i32* %153, align 4, !tbaa !3
  %168 = sext i32 %166 to i64
  %169 = getelementptr inbounds i32, i32* %19, i64 %168
  store i32 %159, i32* %169, align 4, !tbaa !3
  %170 = add nsw i64 %157, 1
  %171 = load i32, i32* %149, align 4, !tbaa !3
  %172 = sext i32 %171 to i64
  %173 = icmp slt i64 %170, %172
  br i1 %173, label %156, label %174

; <label>:174:                                    ; preds = %156, %144
  %175 = icmp eq i64 %148, %143
  br i1 %175, label %176, label %144

; <label>:176:                                    ; preds = %174, %10, %56, %141
  %177 = phi i32 [ %57, %141 ], [ %57, %56 ], [ 0, %10 ], [ %57, %174 ]
  tail call void @amd_2(i32 %0, i32* %7, i32* %19, i32* %5, i32 %12, i32 %177, i32* %14, i32* %4, i32* %3, i32* %15, i32* %16, i32* %17, i32* %18, double* %8, double* %9) #2
  ret void
}

declare void @amd_2(i32, i32*, i32*, i32*, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i32*, double*, double*) local_unnamed_addr #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
