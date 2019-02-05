; ModuleID = 'klu_free_numeric.c'
source_filename = "klu_free_numeric.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_free_numeric(%struct.klu_numeric**, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %3 = icmp eq %struct.klu_common_struct* %1, null
  br i1 %3, label %109, label %4

; <label>:4:                                      ; preds = %2
  %5 = icmp eq %struct.klu_numeric** %0, null
  br i1 %5, label %109, label %6

; <label>:6:                                      ; preds = %4
  %7 = load %struct.klu_numeric*, %struct.klu_numeric** %0, align 8, !tbaa !3
  %8 = icmp eq %struct.klu_numeric* %7, null
  br i1 %8, label %109, label %9

; <label>:9:                                      ; preds = %6
  %10 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 0
  %11 = load i32, i32* %10, align 8, !tbaa !7
  %12 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 23
  %13 = load i32, i32* %12, align 8, !tbaa !11
  %14 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 1
  %15 = load i32, i32* %14, align 4, !tbaa !12
  %16 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 13
  %17 = load i64*, i64** %16, align 8, !tbaa !13
  %18 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 12
  %19 = bitcast i8*** %18 to double***
  %20 = load double**, double*** %19, align 8, !tbaa !14
  %21 = icmp ne double** %20, null
  %22 = icmp sgt i32 %15, 0
  %23 = and i1 %21, %22
  br i1 %23, label %24, label %47

; <label>:24:                                     ; preds = %9
  %25 = icmp eq i64* %17, null
  %26 = zext i32 %15 to i64
  br i1 %25, label %28, label %27

; <label>:27:                                     ; preds = %24
  br label %37

; <label>:28:                                     ; preds = %24
  br label %29

; <label>:29:                                     ; preds = %28, %29
  %30 = phi i64 [ %35, %29 ], [ 0, %28 ]
  %31 = getelementptr inbounds double*, double** %20, i64 %30
  %32 = bitcast double** %31 to i8**
  %33 = load i8*, i8** %32, align 8, !tbaa !3
  %34 = tail call i8* @klu_free(i8* %33, i64 0, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %35 = add nuw nsw i64 %30, 1
  %36 = icmp eq i64 %35, %26
  br i1 %36, label %47, label %29

; <label>:37:                                     ; preds = %27, %37
  %38 = phi i64 [ %45, %37 ], [ 0, %27 ]
  %39 = getelementptr inbounds double*, double** %20, i64 %38
  %40 = bitcast double** %39 to i8**
  %41 = load i8*, i8** %40, align 8, !tbaa !3
  %42 = getelementptr inbounds i64, i64* %17, i64 %38
  %43 = load i64, i64* %42, align 8, !tbaa !15
  %44 = tail call i8* @klu_free(i8* %41, i64 %43, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %45 = add nuw nsw i64 %38, 1
  %46 = icmp eq i64 %45, %26
  br i1 %46, label %47, label %37

; <label>:47:                                     ; preds = %37, %29, %9
  %48 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 6
  %49 = bitcast i32** %48 to i8**
  %50 = load i8*, i8** %49, align 8, !tbaa !16
  %51 = sext i32 %11 to i64
  %52 = tail call i8* @klu_free(i8* %50, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %53 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 20
  %54 = bitcast i32** %53 to i8**
  %55 = load i8*, i8** %54, align 8, !tbaa !17
  %56 = add nsw i32 %11, 1
  %57 = sext i32 %56 to i64
  %58 = tail call i8* @klu_free(i8* %55, i64 %57, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %59 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 21
  %60 = bitcast i32** %59 to i8**
  %61 = load i8*, i8** %60, align 8, !tbaa !18
  %62 = add nsw i32 %13, 1
  %63 = sext i32 %62 to i64
  %64 = tail call i8* @klu_free(i8* %61, i64 %63, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %65 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 22
  %66 = load i8*, i8** %65, align 8, !tbaa !19
  %67 = tail call i8* @klu_free(i8* %66, i64 %63, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %68 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 8
  %69 = bitcast i32** %68 to i8**
  %70 = load i8*, i8** %69, align 8, !tbaa !20
  %71 = tail call i8* @klu_free(i8* %70, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %72 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 10
  %73 = bitcast i32** %72 to i8**
  %74 = load i8*, i8** %73, align 8, !tbaa !21
  %75 = tail call i8* @klu_free(i8* %74, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %76 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 9
  %77 = bitcast i32** %76 to i8**
  %78 = load i8*, i8** %77, align 8, !tbaa !22
  %79 = tail call i8* @klu_free(i8* %78, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %80 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 11
  %81 = bitcast i32** %80 to i8**
  %82 = load i8*, i8** %81, align 8, !tbaa !23
  %83 = tail call i8* @klu_free(i8* %82, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %84 = bitcast i64** %16 to i8**
  %85 = load i8*, i8** %84, align 8, !tbaa !13
  %86 = sext i32 %15 to i64
  %87 = tail call i8* @klu_free(i8* %85, i64 %86, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %88 = bitcast i8*** %18 to i8**
  %89 = load i8*, i8** %88, align 8, !tbaa !14
  %90 = tail call i8* @klu_free(i8* %89, i64 %86, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %91 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 14
  %92 = load i8*, i8** %91, align 8, !tbaa !24
  %93 = tail call i8* @klu_free(i8* %92, i64 %51, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %94 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 15
  %95 = bitcast double** %94 to i8**
  %96 = load i8*, i8** %95, align 8, !tbaa !25
  %97 = tail call i8* @klu_free(i8* %96, i64 %51, i64 8, %struct.klu_common_struct* nonnull %1) #2
  %98 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 7
  %99 = bitcast i32** %98 to i8**
  %100 = load i8*, i8** %99, align 8, !tbaa !26
  %101 = tail call i8* @klu_free(i8* %100, i64 %51, i64 4, %struct.klu_common_struct* nonnull %1) #2
  %102 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 17
  %103 = load i8*, i8** %102, align 8, !tbaa !27
  %104 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 16
  %105 = load i64, i64* %104, align 8, !tbaa !28
  %106 = tail call i8* @klu_free(i8* %103, i64 %105, i64 1, %struct.klu_common_struct* nonnull %1) #2
  %107 = bitcast %struct.klu_numeric* %7 to i8*
  %108 = tail call i8* @klu_free(i8* %107, i64 1, i64 168, %struct.klu_common_struct* nonnull %1) #2
  store %struct.klu_numeric* null, %struct.klu_numeric** %0, align 8, !tbaa !3
  br label %109

; <label>:109:                                    ; preds = %4, %6, %2, %47
  %110 = phi i32 [ 1, %47 ], [ 0, %2 ], [ 1, %6 ], [ 1, %4 ]
  ret i32 %110
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
!7 = !{!8, !9, i64 0}
!8 = !{!"", !9, i64 0, !9, i64 4, !9, i64 8, !9, i64 12, !9, i64 16, !9, i64 20, !4, i64 24, !4, i64 32, !4, i64 40, !4, i64 48, !4, i64 56, !4, i64 64, !4, i64 72, !4, i64 80, !4, i64 88, !4, i64 96, !10, i64 104, !4, i64 112, !4, i64 120, !4, i64 128, !4, i64 136, !4, i64 144, !4, i64 152, !9, i64 160}
!9 = !{!"int", !5, i64 0}
!10 = !{!"long", !5, i64 0}
!11 = !{!8, !9, i64 160}
!12 = !{!8, !9, i64 4}
!13 = !{!8, !4, i64 80}
!14 = !{!8, !4, i64 72}
!15 = !{!10, !10, i64 0}
!16 = !{!8, !4, i64 24}
!17 = !{!8, !4, i64 136}
!18 = !{!8, !4, i64 144}
!19 = !{!8, !4, i64 152}
!20 = !{!8, !4, i64 40}
!21 = !{!8, !4, i64 56}
!22 = !{!8, !4, i64 48}
!23 = !{!8, !4, i64 64}
!24 = !{!8, !4, i64 88}
!25 = !{!8, !4, i64 96}
!26 = !{!8, !4, i64 32}
!27 = !{!8, !4, i64 112}
!28 = !{!8, !10, i64 104}
