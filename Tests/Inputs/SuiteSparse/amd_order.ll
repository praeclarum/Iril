; ModuleID = 'amd_order.c'
source_filename = "amd_order.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

@.memset_pattern = private unnamed_addr constant [2 x double] [double -1.000000e+00, double -1.000000e+00], align 16

; Function Attrs: nounwind ssp uwtable
define i32 @amd_order(i32, i32*, i32*, i32*, double*, double*) local_unnamed_addr #0 {
  %7 = icmp ne double* %5, null
  br i1 %7, label %8, label %12

; <label>:8:                                      ; preds = %6
  %9 = bitcast double* %5 to i8*
  call void @memset_pattern16(i8* %9, i8* bitcast ([2 x double]* @.memset_pattern to i8*), i64 160) #3
  %10 = sitofp i32 %0 to double
  %11 = getelementptr inbounds double, double* %5, i64 1
  store double %10, double* %11, align 8, !tbaa !3
  store double 0.000000e+00, double* %5, align 8, !tbaa !3
  br label %12

; <label>:12:                                     ; preds = %8, %6
  %13 = icmp eq i32* %2, null
  %14 = icmp eq i32* %1, null
  %15 = or i1 %14, %13
  %16 = icmp eq i32* %3, null
  %17 = or i1 %15, %16
  %18 = icmp slt i32 %0, 0
  %19 = or i1 %18, %17
  br i1 %19, label %20, label %22

; <label>:20:                                     ; preds = %12
  br i1 %7, label %21, label %128

; <label>:21:                                     ; preds = %20
  store double -2.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:22:                                     ; preds = %12
  %23 = icmp eq i32 %0, 0
  br i1 %23, label %128, label %24

; <label>:24:                                     ; preds = %22
  %25 = sext i32 %0 to i64
  %26 = getelementptr inbounds i32, i32* %1, i64 %25
  %27 = load i32, i32* %26, align 4, !tbaa !7
  br i1 %7, label %28, label %31

; <label>:28:                                     ; preds = %24
  %29 = sitofp i32 %27 to double
  %30 = getelementptr inbounds double, double* %5, i64 2
  store double %29, double* %30, align 8, !tbaa !3
  br label %31

; <label>:31:                                     ; preds = %28, %24
  %32 = icmp slt i32 %27, 0
  br i1 %32, label %33, label %35

; <label>:33:                                     ; preds = %31
  br i1 %7, label %34, label %128

; <label>:34:                                     ; preds = %33
  store double -2.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:35:                                     ; preds = %31
  %36 = sext i32 %27 to i64
  %37 = tail call i32 @amd_valid(i32 %0, i32 %0, i32* nonnull %1, i32* nonnull %2) #3
  %38 = icmp eq i32 %37, -2
  br i1 %38, label %39, label %41

; <label>:39:                                     ; preds = %35
  br i1 %7, label %40, label %128

; <label>:40:                                     ; preds = %39
  store double -2.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:41:                                     ; preds = %35
  %42 = tail call i8* @SuiteSparse_malloc(i64 %25, i64 4) #3
  %43 = bitcast i8* %42 to i32*
  %44 = tail call i8* @SuiteSparse_malloc(i64 %25, i64 4) #3
  %45 = bitcast i8* %44 to i32*
  %46 = sitofp i32 %0 to double
  %47 = fadd double %46, %46
  %48 = icmp ne i8* %42, null
  %49 = icmp ne i8* %44, null
  %50 = and i1 %48, %49
  br i1 %50, label %55, label %51

; <label>:51:                                     ; preds = %41
  %52 = tail call i8* @SuiteSparse_free(i8* %42) #3
  %53 = tail call i8* @SuiteSparse_free(i8* %44) #3
  br i1 %7, label %54, label %128

; <label>:54:                                     ; preds = %51
  store double -1.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:55:                                     ; preds = %41
  %56 = icmp eq i32 %37, 1
  br i1 %56, label %57, label %80

; <label>:57:                                     ; preds = %55
  %58 = add nsw i32 %0, 1
  %59 = sext i32 %58 to i64
  %60 = tail call i8* @SuiteSparse_malloc(i64 %59, i64 4) #3
  %61 = bitcast i8* %60 to i32*
  %62 = tail call i8* @SuiteSparse_malloc(i64 %36, i64 4) #3
  %63 = bitcast i8* %62 to i32*
  %64 = icmp ne i8* %60, null
  %65 = icmp ne i8* %62, null
  %66 = and i1 %64, %65
  br i1 %66, label %73, label %67

; <label>:67:                                     ; preds = %57
  %68 = tail call i8* @SuiteSparse_free(i8* %60) #3
  %69 = tail call i8* @SuiteSparse_free(i8* %62) #3
  %70 = tail call i8* @SuiteSparse_free(i8* nonnull %42) #3
  %71 = tail call i8* @SuiteSparse_free(i8* nonnull %44) #3
  br i1 %7, label %72, label %128

; <label>:72:                                     ; preds = %67
  store double -1.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:73:                                     ; preds = %57
  %74 = sitofp i32 %58 to double
  %75 = fadd double %47, %74
  %76 = icmp sgt i32 %27, 1
  %77 = select i1 %76, i32 %27, i32 1
  %78 = sitofp i32 %77 to double
  %79 = fadd double %75, %78
  tail call void @amd_preprocess(i32 %0, i32* nonnull %1, i32* nonnull %2, i32* %61, i32* %63, i32* %43, i32* %45) #3
  br label %80

; <label>:80:                                     ; preds = %55, %73
  %81 = phi i8* [ %60, %73 ], [ null, %55 ]
  %82 = phi i8* [ %62, %73 ], [ null, %55 ]
  %83 = phi i32* [ %61, %73 ], [ %1, %55 ]
  %84 = phi i32* [ %63, %73 ], [ %2, %55 ]
  %85 = phi double [ %79, %73 ], [ %47, %55 ]
  %86 = tail call i64 @amd_aat(i32 %0, i32* %83, i32* %84, i32* %43, i32* nonnull %3, double* %5) #3
  %87 = udiv i64 %86, 5
  %88 = add i64 %87, %86
  %89 = icmp ult i64 %88, %86
  br i1 %89, label %104, label %90

; <label>:90:                                     ; preds = %80
  %91 = add i64 %88, %25
  %92 = icmp ugt i64 %91, %88
  br i1 %92, label %130, label %93

; <label>:93:                                     ; preds = %145, %142, %139, %136, %133, %130, %90
  %94 = phi i64 [ %91, %90 ], [ %131, %130 ], [ %134, %133 ], [ %137, %136 ], [ %140, %139 ], [ %143, %142 ], [ %146, %145 ]
  %95 = phi i1 [ %92, %90 ], [ %132, %130 ], [ %135, %133 ], [ %138, %136 ], [ %141, %139 ], [ %144, %142 ], [ %147, %145 ]
  %96 = uitofp i64 %94 to double
  %97 = fadd double %85, %96
  %98 = icmp ult i64 %94, 2147483647
  %99 = and i1 %95, %98
  br i1 %99, label %100, label %104

; <label>:100:                                    ; preds = %93
  %101 = tail call i8* @SuiteSparse_malloc(i64 %94, i64 4) #3
  %102 = bitcast i8* %101 to i32*
  %103 = icmp eq i8* %101, null
  br i1 %103, label %104, label %110

; <label>:104:                                    ; preds = %80, %93, %100
  %105 = tail call i8* @SuiteSparse_free(i8* %81) #3
  %106 = tail call i8* @SuiteSparse_free(i8* %82) #3
  %107 = tail call i8* @SuiteSparse_free(i8* %42) #3
  %108 = tail call i8* @SuiteSparse_free(i8* %44) #3
  br i1 %7, label %109, label %128

; <label>:109:                                    ; preds = %104
  store double -1.000000e+00, double* %5, align 8, !tbaa !3
  br label %128

; <label>:110:                                    ; preds = %100
  br i1 %7, label %111, label %121

; <label>:111:                                    ; preds = %110
  %112 = fmul double %97, 4.000000e+00
  %113 = getelementptr inbounds double, double* %5, i64 7
  store double %112, double* %113, align 8, !tbaa !3
  %114 = trunc i64 %94 to i32
  tail call void @amd_1(i32 %0, i32* %83, i32* %84, i32* %3, i32* %45, i32* %43, i32 %114, i32* nonnull %102, double* %4, double* nonnull %5) #3
  %115 = tail call i8* @SuiteSparse_free(i8* %81) #3
  %116 = tail call i8* @SuiteSparse_free(i8* %82) #3
  %117 = tail call i8* @SuiteSparse_free(i8* %42) #3
  %118 = tail call i8* @SuiteSparse_free(i8* %44) #3
  %119 = tail call i8* @SuiteSparse_free(i8* nonnull %101) #3
  %120 = sitofp i32 %37 to double
  store double %120, double* %5, align 8, !tbaa !3
  br label %128

; <label>:121:                                    ; preds = %110
  %122 = trunc i64 %94 to i32
  tail call void @amd_1(i32 %0, i32* %83, i32* %84, i32* %3, i32* %45, i32* %43, i32 %122, i32* nonnull %102, double* %4, double* null) #3
  %123 = tail call i8* @SuiteSparse_free(i8* %81) #3
  %124 = tail call i8* @SuiteSparse_free(i8* %82) #3
  %125 = tail call i8* @SuiteSparse_free(i8* %42) #3
  %126 = tail call i8* @SuiteSparse_free(i8* %44) #3
  %127 = tail call i8* @SuiteSparse_free(i8* nonnull %101) #3
  br label %128

; <label>:128:                                    ; preds = %111, %121, %104, %109, %67, %72, %51, %54, %39, %40, %33, %34, %22, %20, %21
  %129 = phi i32 [ -2, %21 ], [ -2, %20 ], [ 0, %22 ], [ -2, %34 ], [ -2, %33 ], [ -2, %40 ], [ -2, %39 ], [ -1, %54 ], [ -1, %51 ], [ -1, %72 ], [ -1, %67 ], [ -1, %109 ], [ -1, %104 ], [ %37, %121 ], [ %37, %111 ]
  ret i32 %129

; <label>:130:                                    ; preds = %90
  %131 = add i64 %91, %25
  %132 = icmp ugt i64 %131, %91
  br i1 %132, label %133, label %93

; <label>:133:                                    ; preds = %130
  %134 = add i64 %131, %25
  %135 = icmp ugt i64 %134, %131
  br i1 %135, label %136, label %93

; <label>:136:                                    ; preds = %133
  %137 = add i64 %134, %25
  %138 = icmp ugt i64 %137, %134
  br i1 %138, label %139, label %93

; <label>:139:                                    ; preds = %136
  %140 = add i64 %137, %25
  %141 = icmp ugt i64 %140, %137
  br i1 %141, label %142, label %93

; <label>:142:                                    ; preds = %139
  %143 = add i64 %140, %25
  %144 = icmp ugt i64 %143, %140
  br i1 %144, label %145, label %93

; <label>:145:                                    ; preds = %142
  %146 = add i64 %143, %25
  %147 = icmp ugt i64 %146, %143
  br label %93
}

declare i32 @amd_valid(i32, i32, i32*, i32*) local_unnamed_addr #1

declare i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #1

declare i8* @SuiteSparse_free(i8*) local_unnamed_addr #1

declare void @amd_preprocess(i32, i32*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #1

declare i64 @amd_aat(i32, i32*, i32*, i32*, i32*, double*) local_unnamed_addr #1

declare void @amd_1(i32, i32*, i32*, i32*, i32*, i32*, i32, i32*, double*, double*) local_unnamed_addr #1

; Function Attrs: argmemonly
declare void @memset_pattern16(i8* nocapture, i8* nocapture readonly, i64) local_unnamed_addr #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { argmemonly }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"double", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !8, i64 0}
!8 = !{!"int", !5, i64 0}
