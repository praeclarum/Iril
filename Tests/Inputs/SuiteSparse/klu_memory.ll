; ModuleID = 'klu_memory.c'
source_filename = "klu_memory.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: norecurse nounwind ssp uwtable
define i64 @klu_add_size_t(i64, i64, i32* nocapture) local_unnamed_addr #0 {
  %4 = load i32, i32* %2, align 4, !tbaa !3
  %5 = icmp eq i32 %4, 0
  br i1 %5, label %6, label %7

; <label>:6:                                      ; preds = %3
  store i32 0, i32* %2, align 4, !tbaa !3
  ret i64 -1

; <label>:7:                                      ; preds = %3
  %8 = add i64 %1, %0
  %9 = icmp ugt i64 %0, %1
  %10 = select i1 %9, i64 %0, i64 %1
  %11 = icmp uge i64 %8, %10
  %12 = zext i1 %11 to i32
  store i32 %12, i32* %2, align 4, !tbaa !3
  %13 = select i1 %11, i64 %8, i64 -1
  ret i64 %13
}

; Function Attrs: norecurse nounwind ssp uwtable
define i64 @klu_mult_size_t(i64, i64, i32* nocapture) local_unnamed_addr #0 {
  %4 = icmp eq i64 %1, 0
  %5 = load i32, i32* %2, align 4, !tbaa !3
  %6 = icmp eq i32 %5, 0
  br i1 %4, label %47, label %7

; <label>:7:                                      ; preds = %3
  %8 = and i64 %1, 1
  %9 = icmp eq i64 %1, 1
  br i1 %9, label %28, label %10

; <label>:10:                                     ; preds = %7
  %11 = sub i64 %1, %8
  br label %12

; <label>:12:                                     ; preds = %59, %10
  %13 = phi i1 [ %6, %10 ], [ %62, %59 ]
  %14 = phi i64 [ 0, %10 ], [ %61, %59 ]
  %15 = phi i64 [ %11, %10 ], [ %63, %59 ]
  br i1 %13, label %16, label %17

; <label>:16:                                     ; preds = %12
  store i32 0, i32* %2, align 4, !tbaa !3
  br label %24

; <label>:17:                                     ; preds = %12
  %18 = add i64 %14, %0
  %19 = icmp ugt i64 %14, %0
  %20 = select i1 %19, i64 %14, i64 %0
  %21 = icmp uge i64 %18, %20
  %22 = zext i1 %21 to i32
  store i32 %22, i32* %2, align 4, !tbaa !3
  %23 = select i1 %21, i64 %18, i64 -1
  br label %24

; <label>:24:                                     ; preds = %17, %16
  %25 = phi i32 [ 0, %16 ], [ %22, %17 ]
  %26 = phi i64 [ -1, %16 ], [ %23, %17 ]
  %27 = icmp eq i32 %25, 0
  br i1 %27, label %58, label %51

; <label>:28:                                     ; preds = %59, %7
  %29 = phi i64 [ undef, %7 ], [ %61, %59 ]
  %30 = phi i1 [ undef, %7 ], [ %62, %59 ]
  %31 = phi i1 [ %6, %7 ], [ %62, %59 ]
  %32 = phi i64 [ 0, %7 ], [ %61, %59 ]
  %33 = icmp eq i64 %8, 0
  br i1 %33, label %47, label %34

; <label>:34:                                     ; preds = %28
  br i1 %31, label %42, label %35

; <label>:35:                                     ; preds = %34
  %36 = add i64 %32, %0
  %37 = icmp ugt i64 %32, %0
  %38 = select i1 %37, i64 %32, i64 %0
  %39 = icmp uge i64 %36, %38
  %40 = zext i1 %39 to i32
  store i32 %40, i32* %2, align 4, !tbaa !3
  %41 = select i1 %39, i64 %36, i64 -1
  br label %43

; <label>:42:                                     ; preds = %34
  store i32 0, i32* %2, align 4, !tbaa !3
  br label %43

; <label>:43:                                     ; preds = %35, %42
  %44 = phi i32 [ 0, %42 ], [ %40, %35 ]
  %45 = phi i64 [ -1, %42 ], [ %41, %35 ]
  %46 = icmp eq i32 %44, 0
  br label %47

; <label>:47:                                     ; preds = %43, %28, %3
  %48 = phi i64 [ 0, %3 ], [ %29, %28 ], [ %45, %43 ]
  %49 = phi i1 [ %6, %3 ], [ %30, %28 ], [ %46, %43 ]
  %50 = select i1 %49, i64 -1, i64 %48
  ret i64 %50

; <label>:51:                                     ; preds = %24
  %52 = add i64 %26, %0
  %53 = icmp ugt i64 %26, %0
  %54 = select i1 %53, i64 %26, i64 %0
  %55 = icmp uge i64 %52, %54
  %56 = zext i1 %55 to i32
  store i32 %56, i32* %2, align 4, !tbaa !3
  %57 = select i1 %55, i64 %52, i64 -1
  br label %59

; <label>:58:                                     ; preds = %24
  store i32 0, i32* %2, align 4, !tbaa !3
  br label %59

; <label>:59:                                     ; preds = %58, %51
  %60 = phi i32 [ 0, %58 ], [ %56, %51 ]
  %61 = phi i64 [ -1, %58 ], [ %57, %51 ]
  %62 = icmp eq i32 %60, 0
  %63 = add i64 %15, -2
  %64 = icmp eq i64 %63, 0
  br i1 %64, label %28, label %12
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2 {
  %4 = icmp eq %struct.klu_common_struct* %2, null
  br i1 %4, label %29, label %5

; <label>:5:                                      ; preds = %3
  %6 = icmp eq i64 %1, 0
  br i1 %6, label %7, label %9

; <label>:7:                                      ; preds = %5
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 -3, i32* %8, align 4, !tbaa !7
  br label %29

; <label>:9:                                      ; preds = %5
  %10 = icmp ugt i64 %0, 2147483646
  br i1 %10, label %11, label %13

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 -4, i32* %12, align 4, !tbaa !7
  br label %29

; <label>:13:                                     ; preds = %9
  %14 = tail call i8* @SuiteSparse_malloc(i64 %0, i64 %1) #4
  %15 = icmp eq i8* %14, null
  br i1 %15, label %16, label %18

; <label>:16:                                     ; preds = %13
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 -2, i32* %17, align 4, !tbaa !7
  br label %29

; <label>:18:                                     ; preds = %13
  %19 = icmp eq i64 %0, 0
  %20 = select i1 %19, i64 1, i64 %0
  %21 = mul i64 %20, %1
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 22
  %23 = load i64, i64* %22, align 8, !tbaa !12
  %24 = add i64 %23, %21
  store i64 %24, i64* %22, align 8, !tbaa !12
  %25 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 23
  %26 = load i64, i64* %25, align 8, !tbaa !13
  %27 = icmp ugt i64 %26, %24
  %28 = select i1 %27, i64 %26, i64 %24
  store i64 %28, i64* %25, align 8, !tbaa !13
  br label %29

; <label>:29:                                     ; preds = %3, %7, %16, %18, %11
  %30 = phi i8* [ null, %7 ], [ null, %11 ], [ null, %16 ], [ %14, %18 ], [ null, %3 ]
  ret i8* %30
}

declare i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #3

; Function Attrs: nounwind ssp uwtable
define noalias i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2 {
  %5 = icmp ne i8* %0, null
  %6 = icmp ne %struct.klu_common_struct* %3, null
  %7 = and i1 %5, %6
  br i1 %7, label %8, label %16

; <label>:8:                                      ; preds = %4
  %9 = tail call i8* @SuiteSparse_free(i8* nonnull %0) #4
  %10 = icmp eq i64 %1, 0
  %11 = select i1 %10, i64 1, i64 %1
  %12 = mul i64 %11, %2
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 22
  %14 = load i64, i64* %13, align 8, !tbaa !12
  %15 = sub i64 %14, %12
  store i64 %15, i64* %13, align 8, !tbaa !12
  br label %16

; <label>:16:                                     ; preds = %8, %4
  ret i8* null
}

declare i8* @SuiteSparse_free(i8*) local_unnamed_addr #3

; Function Attrs: nounwind ssp uwtable
define i8* @klu_realloc(i64, i64, i64, i8*, %struct.klu_common_struct*) local_unnamed_addr #2 {
  %6 = alloca i32, align 4
  %7 = bitcast i32* %6 to i8*
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %7) #4
  store i32 1, i32* %6, align 4, !tbaa !3
  %8 = icmp eq %struct.klu_common_struct* %4, null
  br i1 %8, label %54, label %9

; <label>:9:                                      ; preds = %5
  %10 = icmp eq i64 %2, 0
  br i1 %10, label %11, label %13

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -3, i32* %12, align 4, !tbaa !7
  br label %54

; <label>:13:                                     ; preds = %9
  %14 = icmp eq i8* %3, null
  %15 = icmp ugt i64 %0, 2147483646
  br i1 %14, label %16, label %35

; <label>:16:                                     ; preds = %13
  br i1 %15, label %17, label %19

; <label>:17:                                     ; preds = %16
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -4, i32* %18, align 4, !tbaa !7
  br label %54

; <label>:19:                                     ; preds = %16
  %20 = tail call i8* @SuiteSparse_malloc(i64 %0, i64 %2) #4
  %21 = icmp eq i8* %20, null
  br i1 %21, label %22, label %24

; <label>:22:                                     ; preds = %19
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -2, i32* %23, align 4, !tbaa !7
  br label %54

; <label>:24:                                     ; preds = %19
  %25 = icmp eq i64 %0, 0
  %26 = select i1 %25, i64 1, i64 %0
  %27 = mul i64 %26, %2
  %28 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 22
  %29 = load i64, i64* %28, align 8, !tbaa !12
  %30 = add i64 %29, %27
  store i64 %30, i64* %28, align 8, !tbaa !12
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 23
  %32 = load i64, i64* %31, align 8, !tbaa !13
  %33 = icmp ugt i64 %32, %30
  %34 = select i1 %33, i64 %32, i64 %30
  store i64 %34, i64* %31, align 8, !tbaa !13
  br label %54

; <label>:35:                                     ; preds = %13
  br i1 %15, label %36, label %38

; <label>:36:                                     ; preds = %35
  %37 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -4, i32* %37, align 4, !tbaa !7
  br label %54

; <label>:38:                                     ; preds = %35
  %39 = call i8* @SuiteSparse_realloc(i64 %0, i64 %1, i64 %2, i8* nonnull %3, i32* nonnull %6) #4
  %40 = load i32, i32* %6, align 4, !tbaa !3
  %41 = icmp eq i32 %40, 0
  br i1 %41, label %52, label %42

; <label>:42:                                     ; preds = %38
  %43 = sub i64 %0, %1
  %44 = mul i64 %43, %2
  %45 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 22
  %46 = load i64, i64* %45, align 8, !tbaa !12
  %47 = add i64 %46, %44
  store i64 %47, i64* %45, align 8, !tbaa !12
  %48 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 23
  %49 = load i64, i64* %48, align 8, !tbaa !13
  %50 = icmp ugt i64 %49, %47
  %51 = select i1 %50, i64 %49, i64 %47
  store i64 %51, i64* %48, align 8, !tbaa !13
  br label %54

; <label>:52:                                     ; preds = %38
  %53 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 -2, i32* %53, align 4, !tbaa !7
  br label %54

; <label>:54:                                     ; preds = %24, %22, %17, %5, %11, %36, %52, %42
  %55 = phi i8* [ null, %11 ], [ %3, %36 ], [ %39, %42 ], [ %3, %52 ], [ null, %5 ], [ null, %17 ], [ null, %22 ], [ %20, %24 ]
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %7) #4
  ret i8* %55
}

declare i8* @SuiteSparse_realloc(i64, i64, i64, i8*, i32*) local_unnamed_addr #3

attributes #0 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !4, i64 76}
!8 = !{!"klu_common_struct", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !9, i64 32, !4, i64 40, !4, i64 44, !4, i64 48, !10, i64 56, !10, i64 64, !4, i64 72, !4, i64 76, !4, i64 80, !4, i64 84, !4, i64 88, !4, i64 92, !4, i64 96, !9, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !11, i64 144, !11, i64 152}
!9 = !{!"double", !5, i64 0}
!10 = !{!"any pointer", !5, i64 0}
!11 = !{!"long", !5, i64 0}
!12 = !{!8, !11, i64 144}
!13 = !{!8, !11, i64 152}
