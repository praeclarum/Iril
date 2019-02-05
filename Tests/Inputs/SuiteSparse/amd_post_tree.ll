; ModuleID = 'amd_post_tree.c'
source_filename = "amd_post_tree.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @amd_post_tree(i32, i32, i32* nocapture, i32* nocapture readonly, i32* nocapture, i32* nocapture) local_unnamed_addr #0 {
  store i32 %0, i32* %5, align 4, !tbaa !3
  br label %7

; <label>:7:                                      ; preds = %44, %6
  %8 = phi i32 [ %0, %6 ], [ %47, %44 ]
  %9 = phi i32 [ %1, %6 ], [ %42, %44 ]
  %10 = phi i32 [ 0, %6 ], [ %41, %44 ]
  %11 = sext i32 %8 to i64
  %12 = getelementptr inbounds i32, i32* %2, i64 %11
  %13 = load i32, i32* %12, align 4, !tbaa !3
  %14 = icmp eq i32 %13, -1
  br i1 %14, label %36, label %15

; <label>:15:                                     ; preds = %7
  br label %16

; <label>:16:                                     ; preds = %15, %16
  %17 = phi i32 [ %19, %16 ], [ %10, %15 ]
  %18 = phi i32 [ %22, %16 ], [ %13, %15 ]
  %19 = add nsw i32 %17, 1
  %20 = sext i32 %18 to i64
  %21 = getelementptr inbounds i32, i32* %3, i64 %20
  %22 = load i32, i32* %21, align 4, !tbaa !3
  %23 = icmp eq i32 %22, -1
  br i1 %23, label %24, label %16

; <label>:24:                                     ; preds = %16
  %25 = sext i32 %19 to i64
  br label %26

; <label>:26:                                     ; preds = %24, %26
  %27 = phi i64 [ %25, %24 ], [ %29, %26 ]
  %28 = phi i32 [ %13, %24 ], [ %33, %26 ]
  %29 = add i64 %27, -1
  %30 = getelementptr inbounds i32, i32* %5, i64 %27
  store i32 %28, i32* %30, align 4, !tbaa !3
  %31 = sext i32 %28 to i64
  %32 = getelementptr inbounds i32, i32* %3, i64 %31
  %33 = load i32, i32* %32, align 4, !tbaa !3
  %34 = icmp eq i32 %33, -1
  br i1 %34, label %35, label %26

; <label>:35:                                     ; preds = %26
  store i32 -1, i32* %12, align 4, !tbaa !3
  br label %40

; <label>:36:                                     ; preds = %7
  %37 = add nsw i32 %10, -1
  %38 = add nsw i32 %9, 1
  %39 = getelementptr inbounds i32, i32* %4, i64 %11
  store i32 %9, i32* %39, align 4, !tbaa !3
  br label %40

; <label>:40:                                     ; preds = %36, %35
  %41 = phi i32 [ %19, %35 ], [ %37, %36 ]
  %42 = phi i32 [ %9, %35 ], [ %38, %36 ]
  %43 = icmp sgt i32 %41, -1
  br i1 %43, label %44, label %48

; <label>:44:                                     ; preds = %40
  %45 = sext i32 %41 to i64
  %46 = getelementptr inbounds i32, i32* %5, i64 %45
  %47 = load i32, i32* %46, align 4, !tbaa !3
  br label %7

; <label>:48:                                     ; preds = %40
  ret i32 %42
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
