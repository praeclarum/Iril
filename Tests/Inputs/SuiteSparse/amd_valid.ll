; ModuleID = 'amd_valid.c'
source_filename = "amd_valid.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: norecurse nounwind readonly ssp uwtable
define i32 @amd_valid(i32, i32, i32* readonly, i32* readonly) local_unnamed_addr #0 {
  %5 = or i32 %1, %0
  %6 = icmp slt i32 %5, 0
  %7 = icmp eq i32* %2, null
  %8 = or i1 %6, %7
  %9 = icmp eq i32* %3, null
  %10 = or i1 %8, %9
  br i1 %10, label %52, label %11

; <label>:11:                                     ; preds = %4
  %12 = sext i32 %1 to i64
  %13 = getelementptr inbounds i32, i32* %2, i64 %12
  %14 = load i32, i32* %13, align 4, !tbaa !3
  %15 = load i32, i32* %2, align 4, !tbaa !3
  %16 = icmp ne i32 %15, 0
  %17 = icmp slt i32 %14, 0
  %18 = or i1 %17, %16
  br i1 %18, label %52, label %19

; <label>:19:                                     ; preds = %11
  %20 = icmp sgt i32 %1, 0
  br i1 %20, label %21, label %52

; <label>:21:                                     ; preds = %19
  br label %22

; <label>:22:                                     ; preds = %21, %49
  %23 = phi i32 [ %28, %49 ], [ 0, %21 ]
  %24 = phi i64 [ %26, %49 ], [ 0, %21 ]
  %25 = phi i32 [ %50, %49 ], [ 0, %21 ]
  %26 = add nuw nsw i64 %24, 1
  %27 = getelementptr inbounds i32, i32* %2, i64 %26
  %28 = load i32, i32* %27, align 4, !tbaa !3
  %29 = icmp sgt i32 %23, %28
  br i1 %29, label %52, label %30

; <label>:30:                                     ; preds = %22
  %31 = icmp slt i32 %23, %28
  br i1 %31, label %32, label %49

; <label>:32:                                     ; preds = %30
  %33 = sext i32 %23 to i64
  %34 = sext i32 %28 to i64
  br label %35

; <label>:35:                                     ; preds = %32, %44
  %36 = phi i64 [ %33, %32 ], [ %47, %44 ]
  %37 = phi i32 [ %25, %32 ], [ %46, %44 ]
  %38 = phi i32 [ -1, %32 ], [ %40, %44 ]
  %39 = getelementptr inbounds i32, i32* %3, i64 %36
  %40 = load i32, i32* %39, align 4, !tbaa !3
  %41 = icmp sgt i32 %40, -1
  %42 = icmp slt i32 %40, %0
  %43 = and i1 %41, %42
  br i1 %43, label %44, label %52

; <label>:44:                                     ; preds = %35
  %45 = icmp sgt i32 %40, %38
  %46 = select i1 %45, i32 %37, i32 1
  %47 = add nsw i64 %36, 1
  %48 = icmp slt i64 %47, %34
  br i1 %48, label %35, label %49

; <label>:49:                                     ; preds = %44, %30
  %50 = phi i32 [ %25, %30 ], [ %46, %44 ]
  %51 = icmp slt i64 %26, %12
  br i1 %51, label %22, label %52

; <label>:52:                                     ; preds = %22, %49, %35, %19, %11, %4
  %53 = phi i32 [ -2, %4 ], [ -2, %11 ], [ 0, %19 ], [ -2, %35 ], [ -2, %22 ], [ %50, %49 ]
  ret i32 %53
}

attributes #0 = { norecurse nounwind readonly ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
