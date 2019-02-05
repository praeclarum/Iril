; ModuleID = 'amd_control.c'
source_filename = "amd_control.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }

@SuiteSparse_config = external local_unnamed_addr global %struct.SuiteSparse_config_struct, align 8
@.str = private unnamed_addr constant [92 x i8] c"\0AAMD version %d.%d.%d, %s: approximate minimum degree ordering\0A    dense row parameter: %g\0A\00", align 1
@.str.1 = private unnamed_addr constant [12 x i8] c"May 4, 2016\00", align 1
@.str.2 = private unnamed_addr constant [30 x i8] c"    no rows treated as dense\0A\00", align 1
@.str.3 = private unnamed_addr constant [125 x i8] c"    (rows with more than max (%g * sqrt (n), 16) entries are\0A    considered \22dense\22, and placed last in output permutation)\0A\00", align 1
@.str.4 = private unnamed_addr constant [33 x i8] c"    aggressive absorption:  yes\0A\00", align 1
@.str.5 = private unnamed_addr constant [32 x i8] c"    aggressive absorption:  no\0A\00", align 1
@.str.6 = private unnamed_addr constant [30 x i8] c"    size of AMD integer: %d\0A\0A\00", align 1

; Function Attrs: nounwind ssp uwtable
define void @amd_control(double* readonly) local_unnamed_addr #0 {
  %2 = icmp eq double* %0, null
  br i1 %2, label %9, label %3

; <label>:3:                                      ; preds = %1
  %4 = load double, double* %0, align 8, !tbaa !3
  %5 = getelementptr inbounds double, double* %0, i64 1
  %6 = load double, double* %5, align 8, !tbaa !3
  %7 = fcmp une double %6, 0.000000e+00
  %8 = zext i1 %7 to i32
  br label %9

; <label>:9:                                      ; preds = %1, %3
  %10 = phi double [ %4, %3 ], [ 1.000000e+01, %1 ]
  %11 = phi i32 [ %8, %3 ], [ 1, %1 ]
  %12 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !7
  %13 = icmp eq i32 (i8*, ...)* %12, null
  br i1 %13, label %17, label %14

; <label>:14:                                     ; preds = %9
  %15 = tail call i32 (i8*, ...) %12(i8* getelementptr inbounds ([92 x i8], [92 x i8]* @.str, i64 0, i64 0), i32 2, i32 4, i32 6, i8* getelementptr inbounds ([12 x i8], [12 x i8]* @.str.1, i64 0, i64 0), double %10) #1
  %16 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !7
  br label %17

; <label>:17:                                     ; preds = %9, %14
  %18 = phi i32 (i8*, ...)* [ null, %9 ], [ %16, %14 ]
  %19 = fcmp olt double %10, 0.000000e+00
  %20 = icmp ne i32 (i8*, ...)* %18, null
  br i1 %19, label %21, label %24

; <label>:21:                                     ; preds = %17
  br i1 %20, label %22, label %27

; <label>:22:                                     ; preds = %21
  %23 = tail call i32 (i8*, ...) %18(i8* getelementptr inbounds ([30 x i8], [30 x i8]* @.str.2, i64 0, i64 0)) #1
  br label %27

; <label>:24:                                     ; preds = %17
  br i1 %20, label %25, label %27

; <label>:25:                                     ; preds = %24
  %26 = tail call i32 (i8*, ...) %18(i8* getelementptr inbounds ([125 x i8], [125 x i8]* @.str.3, i64 0, i64 0), double %10) #1
  br label %27

; <label>:27:                                     ; preds = %24, %25, %21, %22
  %28 = icmp eq i32 %11, 0
  %29 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !7
  %30 = icmp ne i32 (i8*, ...)* %29, null
  br i1 %28, label %34, label %31

; <label>:31:                                     ; preds = %27
  br i1 %30, label %32, label %42

; <label>:32:                                     ; preds = %31
  %33 = tail call i32 (i8*, ...) %29(i8* getelementptr inbounds ([33 x i8], [33 x i8]* @.str.4, i64 0, i64 0)) #1
  br label %37

; <label>:34:                                     ; preds = %27
  br i1 %30, label %35, label %42

; <label>:35:                                     ; preds = %34
  %36 = tail call i32 (i8*, ...) %29(i8* getelementptr inbounds ([32 x i8], [32 x i8]* @.str.5, i64 0, i64 0)) #1
  br label %37

; <label>:37:                                     ; preds = %35, %32
  %38 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !7
  %39 = icmp eq i32 (i8*, ...)* %38, null
  br i1 %39, label %42, label %40

; <label>:40:                                     ; preds = %37
  %41 = tail call i32 (i8*, ...) %38(i8* getelementptr inbounds ([30 x i8], [30 x i8]* @.str.6, i64 0, i64 0), i64 4) #1
  br label %42

; <label>:42:                                     ; preds = %31, %34, %37, %40
  ret void
}

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"double", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !9, i64 32}
!8 = !{!"SuiteSparse_config_struct", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48}
!9 = !{!"any pointer", !5, i64 0}
