; ModuleID = 'btf_order.c'
source_filename = "btf_order.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: nounwind ssp uwtable
define i32 @btf_order(i32, i32*, i32*, double, double*, i32*, i32*, i32*, i32* nocapture, i32*) local_unnamed_addr #0 {
  %11 = tail call i32 @btf_maxtrans(i32 %0, i32 %0, i32* %1, i32* %2, double %3, double* %4, i32* %6, i32* %9) #3
  store i32 %11, i32* %8, align 4, !tbaa !3
  %12 = icmp slt i32 %11, %0
  br i1 %12, label %13, label %113

; <label>:13:                                     ; preds = %10
  %14 = sext i32 %0 to i64
  %15 = getelementptr inbounds i32, i32* %9, i64 %14
  %16 = icmp sgt i32 %0, 0
  br i1 %16, label %17, label %113

; <label>:17:                                     ; preds = %13
  %18 = bitcast i32* %15 to i8*
  %19 = zext i32 %0 to i64
  %20 = shl nuw nsw i64 %19, 2
  call void @llvm.memset.p0i8.i64(i8* %18, i8 0, i64 %20, i32 4, i1 false)
  %21 = zext i32 %0 to i64
  %22 = and i64 %21, 1
  %23 = icmp eq i32 %0, 1
  br i1 %23, label %40, label %24

; <label>:24:                                     ; preds = %17
  %25 = sub nsw i64 %21, %22
  br label %26

; <label>:26:                                     ; preds = %129, %24
  %27 = phi i64 [ 0, %24 ], [ %130, %129 ]
  %28 = phi i64 [ %25, %24 ], [ %131, %129 ]
  %29 = getelementptr inbounds i32, i32* %6, i64 %27
  %30 = load i32, i32* %29, align 4, !tbaa !3
  %31 = icmp eq i32 %30, -1
  br i1 %31, label %35, label %32

; <label>:32:                                     ; preds = %26
  %33 = sext i32 %30 to i64
  %34 = getelementptr inbounds i32, i32* %15, i64 %33
  store i32 1, i32* %34, align 4, !tbaa !3
  br label %35

; <label>:35:                                     ; preds = %26, %32
  %36 = or i64 %27, 1
  %37 = getelementptr inbounds i32, i32* %6, i64 %36
  %38 = load i32, i32* %37, align 4, !tbaa !3
  %39 = icmp eq i32 %38, -1
  br i1 %39, label %129, label %126

; <label>:40:                                     ; preds = %129, %17
  %41 = phi i64 [ 0, %17 ], [ %130, %129 ]
  %42 = icmp eq i64 %22, 0
  br i1 %42, label %50, label %43

; <label>:43:                                     ; preds = %40
  %44 = getelementptr inbounds i32, i32* %6, i64 %41
  %45 = load i32, i32* %44, align 4, !tbaa !3
  %46 = icmp eq i32 %45, -1
  br i1 %46, label %50, label %47

; <label>:47:                                     ; preds = %43
  %48 = sext i32 %45 to i64
  %49 = getelementptr inbounds i32, i32* %15, i64 %48
  store i32 1, i32* %49, align 4, !tbaa !3
  br label %50

; <label>:50:                                     ; preds = %47, %43, %40
  br i1 %16, label %51, label %113

; <label>:51:                                     ; preds = %50
  br label %52

; <label>:52:                                     ; preds = %51, %64
  %53 = phi i64 [ %55, %64 ], [ %14, %51 ]
  %54 = phi i32 [ %65, %64 ], [ 0, %51 ]
  %55 = add nsw i64 %53, -1
  %56 = getelementptr inbounds i32, i32* %15, i64 %55
  %57 = load i32, i32* %56, align 4, !tbaa !3
  %58 = icmp eq i32 %57, 0
  br i1 %58, label %59, label %64

; <label>:59:                                     ; preds = %52
  %60 = add nsw i32 %54, 1
  %61 = sext i32 %54 to i64
  %62 = getelementptr inbounds i32, i32* %9, i64 %61
  %63 = trunc i64 %55 to i32
  store i32 %63, i32* %62, align 4, !tbaa !3
  br label %64

; <label>:64:                                     ; preds = %52, %59
  %65 = phi i32 [ %54, %52 ], [ %60, %59 ]
  %66 = icmp sgt i64 %53, 1
  br i1 %66, label %52, label %67

; <label>:67:                                     ; preds = %64
  br i1 %16, label %68, label %113

; <label>:68:                                     ; preds = %67
  %69 = zext i32 %0 to i64
  %70 = and i64 %69, 1
  %71 = icmp eq i32 %0, 1
  br i1 %71, label %97, label %72

; <label>:72:                                     ; preds = %68
  %73 = sub nsw i64 %69, %70
  br label %74

; <label>:74:                                     ; preds = %121, %72
  %75 = phi i64 [ 0, %72 ], [ %123, %121 ]
  %76 = phi i32 [ %65, %72 ], [ %122, %121 ]
  %77 = phi i64 [ %73, %72 ], [ %124, %121 ]
  %78 = getelementptr inbounds i32, i32* %6, i64 %75
  %79 = load i32, i32* %78, align 4, !tbaa !3
  %80 = icmp eq i32 %79, -1
  %81 = icmp sgt i32 %76, 0
  %82 = and i1 %81, %80
  br i1 %82, label %83, label %89

; <label>:83:                                     ; preds = %74
  %84 = add nsw i32 %76, -1
  %85 = sext i32 %84 to i64
  %86 = getelementptr inbounds i32, i32* %9, i64 %85
  %87 = load i32, i32* %86, align 4, !tbaa !3
  %88 = sub i32 -2, %87
  store i32 %88, i32* %78, align 4, !tbaa !3
  br label %89

; <label>:89:                                     ; preds = %74, %83
  %90 = phi i32 [ %84, %83 ], [ %76, %74 ]
  %91 = or i64 %75, 1
  %92 = getelementptr inbounds i32, i32* %6, i64 %91
  %93 = load i32, i32* %92, align 4, !tbaa !3
  %94 = icmp eq i32 %93, -1
  %95 = icmp sgt i32 %90, 0
  %96 = and i1 %95, %94
  br i1 %96, label %115, label %121

; <label>:97:                                     ; preds = %121, %68
  %98 = phi i64 [ 0, %68 ], [ %123, %121 ]
  %99 = phi i32 [ %65, %68 ], [ %122, %121 ]
  %100 = icmp eq i64 %70, 0
  br i1 %100, label %113, label %101

; <label>:101:                                    ; preds = %97
  %102 = getelementptr inbounds i32, i32* %6, i64 %98
  %103 = load i32, i32* %102, align 4, !tbaa !3
  %104 = icmp eq i32 %103, -1
  %105 = icmp sgt i32 %99, 0
  %106 = and i1 %105, %104
  br i1 %106, label %107, label %113

; <label>:107:                                    ; preds = %101
  %108 = add nsw i32 %99, -1
  %109 = sext i32 %108 to i64
  %110 = getelementptr inbounds i32, i32* %9, i64 %109
  %111 = load i32, i32* %110, align 4, !tbaa !3
  %112 = sub i32 -2, %111
  store i32 %112, i32* %102, align 4, !tbaa !3
  br label %113

; <label>:113:                                    ; preds = %97, %101, %107, %13, %50, %67, %10
  %114 = tail call i32 @btf_strongcomp(i32 %0, i32* %1, i32* %2, i32* %6, i32* %5, i32* %7, i32* %9) #3
  ret i32 %114

; <label>:115:                                    ; preds = %89
  %116 = add nsw i32 %90, -1
  %117 = sext i32 %116 to i64
  %118 = getelementptr inbounds i32, i32* %9, i64 %117
  %119 = load i32, i32* %118, align 4, !tbaa !3
  %120 = sub i32 -2, %119
  store i32 %120, i32* %92, align 4, !tbaa !3
  br label %121

; <label>:121:                                    ; preds = %115, %89
  %122 = phi i32 [ %116, %115 ], [ %90, %89 ]
  %123 = add nuw nsw i64 %75, 2
  %124 = add i64 %77, -2
  %125 = icmp eq i64 %124, 0
  br i1 %125, label %97, label %74

; <label>:126:                                    ; preds = %35
  %127 = sext i32 %38 to i64
  %128 = getelementptr inbounds i32, i32* %15, i64 %127
  store i32 1, i32* %128, align 4, !tbaa !3
  br label %129

; <label>:129:                                    ; preds = %126, %35
  %130 = add nuw nsw i64 %27, 2
  %131 = add i64 %28, -2
  %132 = icmp eq i64 %131, 0
  br i1 %132, label %40, label %26
}

declare i32 @btf_maxtrans(i32, i32, i32*, i32*, double, double*, i32*, i32*) local_unnamed_addr #1

declare i32 @btf_strongcomp(i32, i32*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { argmemonly nounwind }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
