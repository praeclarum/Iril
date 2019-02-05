; ModuleID = 'klu_free_symbolic.c'
source_filename = "klu_free_symbolic.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %3 = icmp eq %struct.klu_common_struct* %1, null
  br i1 %3, label %33, label %4

; <label>:4:                                      ; preds = %2
  %5 = icmp eq %struct.klu_symbolic** %0, null
  br i1 %5, label %33, label %6

; <label>:6:                                      ; preds = %4
  %7 = load %struct.klu_symbolic*, %struct.klu_symbolic** %0, align 8, !tbaa !3
  %8 = icmp eq %struct.klu_symbolic* %7, null
  br i1 %8, label %33, label %9

; <label>:9:                                      ; preds = %6
  %10 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 5
  %11 = load i32, i32* %10, align 8, !tbaa !7
  %12 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 7
  %13 = bitcast i32** %12 to i8**
  %14 = load i8*, i8** %13, align 8, !tbaa !11
  %15 = sext i32 %11 to i64
  %16 = tail call i8* @klu_free(i8* %14, i64 %15, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %17 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 8
  %18 = bitcast i32** %17 to i8**
  %19 = load i8*, i8** %18, align 8, !tbaa !12
  %20 = tail call i8* @klu_free(i8* %19, i64 %15, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %21 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 9
  %22 = bitcast i32** %21 to i8**
  %23 = load i8*, i8** %22, align 8, !tbaa !13
  %24 = add nsw i32 %11, 1
  %25 = sext i32 %24 to i64
  %26 = tail call i8* @klu_free(i8* %23, i64 %25, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %27 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 4
  %28 = bitcast double** %27 to i8**
  %29 = load i8*, i8** %28, align 8, !tbaa !14
  %30 = tail call i8* @klu_free(i8* %29, i64 %15, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %31 = bitcast %struct.klu_symbolic* %7 to i8*
  %32 = tail call i8* @klu_free(i8* %31, i64 1, i64 96, %struct.klu_common_struct* nonnull %1) #2
  store %struct.klu_symbolic* null, %struct.klu_symbolic** %0, align 8, !tbaa !3
  br label %33

; <label>:33:                                     ; preds = %4, %6, %2, %9
  %34 = phi i32 [ 1, %9 ], [ 0, %2 ], [ 1, %6 ], [ 1, %4 ]
  ret i32 %34
}

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !10, i64 40}
!8 = !{!"", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !4, i64 32, !10, i64 40, !10, i64 44, !4, i64 48, !4, i64 56, !4, i64 64, !10, i64 72, !10, i64 76, !10, i64 80, !10, i64 84, !10, i64 88, !10, i64 92}
!9 = !{!"double", !5, i64 0}
!10 = !{!"int", !5, i64 0}
!11 = !{!8, !4, i64 48}
!12 = !{!8, !4, i64 56}
!13 = !{!8, !4, i64 64}
!14 = !{!8, !4, i64 32}
