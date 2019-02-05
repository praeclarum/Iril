; ModuleID = 'SuiteSparse_config.c'
source_filename = "SuiteSparse_config.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }

@SuiteSparse_config = local_unnamed_addr global %struct.SuiteSparse_config_struct { i8* (i64)* @malloc, i8* (i64, i64)* @calloc, i8* (i8*, i64)* @realloc, void (i8*)* @free, i32 (i8*, ...)* @printf, double (double, double)* @SuiteSparse_hypot, i32 (double, double, double, double, double*, double*)* @SuiteSparse_divcomplex }, align 8

; Function Attrs: nounwind allocsize(0)
declare noalias i8* @malloc(i64) #0

; Function Attrs: nounwind allocsize(0,1)
declare noalias i8* @calloc(i64, i64) #1

; Function Attrs: nounwind allocsize(1)
declare noalias i8* @realloc(i8* nocapture, i64) #2

; Function Attrs: nounwind
declare void @free(i8* nocapture) #3

; Function Attrs: nounwind
declare i32 @printf(i8* nocapture readonly, ...) #3

; Function Attrs: nounwind readnone ssp uwtable
define double @SuiteSparse_hypot(double, double) #4 {
  %3 = tail call double @llvm.fabs.f64(double %0)
  %4 = tail call double @llvm.fabs.f64(double %1)
  %5 = fcmp ult double %3, %4
  %6 = fadd double %3, %4
  br i1 %5, label %15, label %7

; <label>:7:                                      ; preds = %2
  %8 = fcmp oeq double %6, %3
  br i1 %8, label %23, label %9

; <label>:9:                                      ; preds = %7
  %10 = fdiv double %4, %3
  %11 = fmul double %10, %10
  %12 = fadd double %11, 1.000000e+00
  %13 = tail call double @llvm.sqrt.f64(double %12)
  %14 = fmul double %3, %13
  br label %23

; <label>:15:                                     ; preds = %2
  %16 = fcmp oeq double %6, %4
  br i1 %16, label %23, label %17

; <label>:17:                                     ; preds = %15
  %18 = fdiv double %3, %4
  %19 = fmul double %18, %18
  %20 = fadd double %19, 1.000000e+00
  %21 = tail call double @llvm.sqrt.f64(double %20)
  %22 = fmul double %4, %21
  br label %23

; <label>:23:                                     ; preds = %15, %7, %17, %9
  %24 = phi double [ %14, %9 ], [ %22, %17 ], [ %3, %7 ], [ %4, %15 ]
  ret double %24
}

; Function Attrs: nounwind ssp uwtable
define i32 @SuiteSparse_divcomplex(double, double, double, double, double* nocapture, double* nocapture) #5 {
  %7 = tail call double @llvm.fabs.f64(double %2)
  %8 = tail call double @llvm.fabs.f64(double %3)
  %9 = fcmp ult double %7, %8
  br i1 %9, label %18, label %10

; <label>:10:                                     ; preds = %6
  %11 = fdiv double %3, %2
  %12 = fmul double %11, %3
  %13 = fadd double %12, %2
  %14 = fmul double %11, %1
  %15 = fadd double %14, %0
  %16 = fdiv double %15, %13
  %17 = fmul double %11, %0
  br label %26

; <label>:18:                                     ; preds = %6
  %19 = fdiv double %2, %3
  %20 = fmul double %19, %2
  %21 = fadd double %20, %3
  %22 = fmul double %19, %0
  %23 = fadd double %22, %1
  %24 = fdiv double %23, %21
  %25 = fmul double %19, %1
  br label %26

; <label>:26:                                     ; preds = %18, %10
  %27 = phi double [ %0, %18 ], [ %17, %10 ]
  %28 = phi double [ %25, %18 ], [ %1, %10 ]
  %29 = phi double [ %21, %18 ], [ %13, %10 ]
  %30 = phi double [ %24, %18 ], [ %16, %10 ]
  %31 = fsub double %28, %27
  %32 = fdiv double %31, %29
  store double %30, double* %4, align 8, !tbaa !3
  store double %32, double* %5, align 8, !tbaa !3
  %33 = fcmp oeq double %29, 0.000000e+00
  %34 = zext i1 %33 to i32
  ret i32 %34
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @SuiteSparse_start() local_unnamed_addr #6 {
  store i8* (i64)* @malloc, i8* (i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 0), align 8, !tbaa !7
  store i8* (i64, i64)* @calloc, i8* (i64, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 1), align 8, !tbaa !10
  store i8* (i8*, i64)* @realloc, i8* (i8*, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 2), align 8, !tbaa !11
  store void (i8*)* @free, void (i8*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 3), align 8, !tbaa !12
  store i32 (i8*, ...)* @printf, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !13
  store double (double, double)* @SuiteSparse_hypot, double (double, double)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 5), align 8, !tbaa !14
  store i32 (double, double, double, double, double*, double*)* @SuiteSparse_divcomplex, i32 (double, double, double, double, double*, double*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 6), align 8, !tbaa !15
  ret void
}

; Function Attrs: norecurse nounwind readnone ssp uwtable
define void @SuiteSparse_finish() local_unnamed_addr #7 {
  ret void
}

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #5 {
  %3 = icmp eq i64 %0, 0
  %4 = select i1 %3, i64 1, i64 %0
  %5 = icmp eq i64 %1, 0
  %6 = select i1 %5, i64 1, i64 %1
  %7 = mul i64 %6, %4
  %8 = uitofp i64 %7 to double
  %9 = uitofp i64 %4 to double
  %10 = uitofp i64 %6 to double
  %11 = fmul double %9, %10
  %12 = fcmp une double %11, %8
  br i1 %12, label %16, label %13

; <label>:13:                                     ; preds = %2
  %14 = load i8* (i64)*, i8* (i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 0), align 8, !tbaa !7
  %15 = tail call i8* %14(i64 %7) #11
  br label %16

; <label>:16:                                     ; preds = %2, %13
  %17 = phi i8* [ %15, %13 ], [ null, %2 ]
  ret i8* %17
}

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_calloc(i64, i64) local_unnamed_addr #5 {
  %3 = icmp eq i64 %0, 0
  %4 = select i1 %3, i64 1, i64 %0
  %5 = icmp eq i64 %1, 0
  %6 = select i1 %5, i64 1, i64 %1
  %7 = mul i64 %6, %4
  %8 = uitofp i64 %7 to double
  %9 = uitofp i64 %4 to double
  %10 = uitofp i64 %6 to double
  %11 = fmul double %9, %10
  %12 = fcmp une double %11, %8
  br i1 %12, label %16, label %13

; <label>:13:                                     ; preds = %2
  %14 = load i8* (i64, i64)*, i8* (i64, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 1), align 8, !tbaa !10
  %15 = tail call i8* %14(i64 %4, i64 %6) #11
  br label %16

; <label>:16:                                     ; preds = %2, %13
  %17 = phi i8* [ %15, %13 ], [ null, %2 ]
  ret i8* %17
}

; Function Attrs: nounwind ssp uwtable
define i8* @SuiteSparse_realloc(i64, i64, i64, i8*, i32* nocapture) local_unnamed_addr #5 {
  %6 = icmp eq i64 %1, 0
  %7 = select i1 %6, i64 1, i64 %1
  %8 = icmp eq i64 %0, 0
  %9 = select i1 %8, i64 1, i64 %0
  %10 = icmp eq i64 %2, 0
  %11 = select i1 %10, i64 1, i64 %2
  %12 = mul i64 %11, %9
  %13 = uitofp i64 %12 to double
  %14 = uitofp i64 %9 to double
  %15 = uitofp i64 %11 to double
  %16 = fmul double %14, %15
  %17 = fcmp une double %16, %13
  br i1 %17, label %33, label %18

; <label>:18:                                     ; preds = %5
  %19 = icmp eq i8* %3, null
  br i1 %19, label %20, label %24

; <label>:20:                                     ; preds = %18
  %21 = load i8* (i64)*, i8* (i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 0), align 8, !tbaa !7
  %22 = tail call i8* %21(i64 %12) #11
  %23 = icmp ne i8* %22, null
  br label %33

; <label>:24:                                     ; preds = %18
  %25 = icmp eq i64 %7, %9
  br i1 %25, label %33, label %26

; <label>:26:                                     ; preds = %24
  %27 = load i8* (i8*, i64)*, i8* (i8*, i64)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 2), align 8, !tbaa !11
  %28 = tail call i8* %27(i8* nonnull %3, i64 %12) #11
  %29 = icmp eq i8* %28, null
  %30 = icmp ult i64 %9, %7
  %31 = select i1 %29, i1 %30, i1 true
  %32 = select i1 %29, i8* %3, i8* %28
  br label %33

; <label>:33:                                     ; preds = %26, %24, %5, %20
  %34 = phi i1 [ %23, %20 ], [ false, %5 ], [ true, %24 ], [ %31, %26 ]
  %35 = phi i8* [ %22, %20 ], [ %3, %5 ], [ %3, %24 ], [ %32, %26 ]
  %36 = zext i1 %34 to i32
  store i32 %36, i32* %4, align 4, !tbaa !16
  ret i8* %35
}

; Function Attrs: nounwind ssp uwtable
define noalias i8* @SuiteSparse_free(i8*) local_unnamed_addr #5 {
  %2 = icmp eq i8* %0, null
  br i1 %2, label %5, label %3

; <label>:3:                                      ; preds = %1
  %4 = load void (i8*)*, void (i8*)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 3), align 8, !tbaa !12
  tail call void %4(i8* nonnull %0) #11
  br label %5

; <label>:5:                                      ; preds = %1, %3
  ret i8* null
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @SuiteSparse_tic(double* nocapture) local_unnamed_addr #6 {
  %2 = bitcast double* %0 to i8*
  call void @llvm.memset.p0i8.i64(i8* %2, i8 0, i64 16, i32 8, i1 false)
  ret void
}

; Function Attrs: nounwind readonly ssp uwtable
define double @SuiteSparse_toc(double* nocapture readonly) local_unnamed_addr #8 {
  %2 = bitcast double* %0 to <2 x double>*
  %3 = load <2 x double>, <2 x double>* %2, align 8, !tbaa !3
  %4 = fsub <2 x double> zeroinitializer, %3
  %5 = extractelement <2 x double> %4, i32 1
  %6 = fmul double %5, 1.000000e-09
  %7 = extractelement <2 x double> %4, i32 0
  %8 = fadd double %7, %6
  ret double %8
}

; Function Attrs: nounwind readnone ssp uwtable
define double @SuiteSparse_time() local_unnamed_addr #4 {
  ret double 0.000000e+00
}

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @SuiteSparse_version(i32*) local_unnamed_addr #6 {
  %2 = icmp eq i32* %0, null
  br i1 %2, label %6, label %3

; <label>:3:                                      ; preds = %1
  store i32 5, i32* %0, align 4, !tbaa !16
  %4 = getelementptr inbounds i32, i32* %0, i64 1
  store i32 4, i32* %4, align 4, !tbaa !16
  %5 = getelementptr inbounds i32, i32* %0, i64 2
  store i32 0, i32* %5, align 4, !tbaa !16
  br label %6

; <label>:6:                                      ; preds = %1, %3
  ret i32 5004
}

; Function Attrs: nounwind readnone speculatable
declare double @llvm.fabs.f64(double) #9

; Function Attrs: nounwind readnone speculatable
declare double @llvm.sqrt.f64(double) #9

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #10

attributes #0 = { nounwind allocsize(0) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind allocsize(0,1) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind allocsize(1) "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind readnone ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #5 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #6 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #7 = { norecurse nounwind readnone ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #8 = { nounwind readonly ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #9 = { nounwind readnone speculatable }
attributes #10 = { argmemonly nounwind }
attributes #11 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"double", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !9, i64 0}
!8 = !{!"SuiteSparse_config_struct", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48}
!9 = !{!"any pointer", !5, i64 0}
!10 = !{!8, !9, i64 8}
!11 = !{!8, !9, i64 16}
!12 = !{!8, !9, i64 24}
!13 = !{!8, !9, i64 32}
!14 = !{!8, !9, i64 40}
!15 = !{!8, !9, i64 48}
!16 = !{!17, !17, i64 0}
!17 = !{!"int", !5, i64 0}
