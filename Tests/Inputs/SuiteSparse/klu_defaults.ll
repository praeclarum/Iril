; ModuleID = 'klu_defaults.c'
source_filename = "klu_defaults.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: norecurse nounwind ssp uwtable
define i32 @klu_defaults(%struct.klu_common_struct*) local_unnamed_addr #0 {
  %2 = icmp eq %struct.klu_common_struct* %0, null
  br i1 %2, label %25, label %3

; <label>:3:                                      ; preds = %1
  %4 = bitcast %struct.klu_common_struct* %0 to <2 x double>*
  store <2 x double> <double 1.000000e-03, double 1.200000e+00>, <2 x double>* %4, align 8, !tbaa !3
  %5 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 2
  %6 = bitcast double* %5 to <2 x double>*
  store <2 x double> <double 1.200000e+00, double 1.000000e+01>, <2 x double>* %6, align 8, !tbaa !3
  %7 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 5
  store i32 1, i32* %7, align 8, !tbaa !7
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 4
  store double 0.000000e+00, double* %8, align 8, !tbaa !12
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 6
  store i32 0, i32* %9, align 4, !tbaa !13
  %10 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 7
  store i32 2, i32* %10, align 8, !tbaa !14
  %11 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 10
  store i32 1, i32* %11, align 8, !tbaa !15
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 8
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 11
  store i32 0, i32* %13, align 4, !tbaa !16
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 12
  store i32 0, i32* %14, align 8, !tbaa !17
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 13
  %16 = bitcast i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %12 to i8*
  call void @llvm.memset.p0i8.i64(i8* nonnull %16, i8 0, i64 16, i32 8, i1 false)
  store i32 -1, i32* %15, align 4, !tbaa !18
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 14
  store i32 -1, i32* %17, align 8, !tbaa !19
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 16
  store i32 -1, i32* %18, align 8, !tbaa !20
  %19 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 17
  %20 = bitcast double* %19 to <2 x double>*
  store <2 x double> <double -1.000000e+00, double -1.000000e+00>, <2 x double>* %20, align 8, !tbaa !3
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 19
  %22 = bitcast double* %21 to <2 x double>*
  store <2 x double> <double -1.000000e+00, double -1.000000e+00>, <2 x double>* %22, align 8, !tbaa !3
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 21
  %24 = bitcast double* %23 to i8*
  call void @llvm.memset.p0i8.i64(i8* nonnull %24, i8 0, i64 24, i32 8, i1 false)
  br label %25

; <label>:25:                                     ; preds = %1, %3
  %26 = phi i32 [ 1, %3 ], [ 0, %1 ]
  ret i32 %26
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
!3 = !{!4, !4, i64 0}
!4 = !{!"double", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !9, i64 40}
!8 = !{!"klu_common_struct", !4, i64 0, !4, i64 8, !4, i64 16, !4, i64 24, !4, i64 32, !9, i64 40, !9, i64 44, !9, i64 48, !10, i64 56, !10, i64 64, !9, i64 72, !9, i64 76, !9, i64 80, !9, i64 84, !9, i64 88, !9, i64 92, !9, i64 96, !4, i64 104, !4, i64 112, !4, i64 120, !4, i64 128, !4, i64 136, !11, i64 144, !11, i64 152}
!9 = !{!"int", !5, i64 0}
!10 = !{!"any pointer", !5, i64 0}
!11 = !{!"long", !5, i64 0}
!12 = !{!8, !4, i64 32}
!13 = !{!8, !9, i64 44}
!14 = !{!8, !9, i64 48}
!15 = !{!8, !9, i64 72}
!16 = !{!8, !9, i64 76}
!17 = !{!8, !9, i64 80}
!18 = !{!8, !9, i64 84}
!19 = !{!8, !9, i64 88}
!20 = !{!8, !9, i64 96}
