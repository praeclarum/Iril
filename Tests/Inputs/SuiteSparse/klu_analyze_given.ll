; ModuleID = 'klu_analyze_given.c'
source_filename = "klu_analyze_given.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_alloc_symbolic(i32, i32* readonly, i32* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %5 = alloca %struct.klu_symbolic*, align 8
  %6 = bitcast %struct.klu_symbolic** %5 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %6) #3
  %7 = icmp eq %struct.klu_common_struct* %3, null
  br i1 %7, label %107, label %8

; <label>:8:                                      ; preds = %4
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 11
  store i32 0, i32* %9, align 4, !tbaa !3
  %10 = icmp slt i32 %0, 1
  %11 = icmp eq i32* %1, null
  %12 = or i1 %10, %11
  %13 = icmp eq i32* %2, null
  %14 = or i1 %12, %13
  br i1 %14, label %15, label %16

; <label>:15:                                     ; preds = %8
  store i32 -3, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:16:                                     ; preds = %8
  %17 = sext i32 %0 to i64
  %18 = getelementptr inbounds i32, i32* %1, i64 %17
  %19 = load i32, i32* %18, align 4, !tbaa !11
  %20 = load i32, i32* %1, align 4, !tbaa !11
  %21 = icmp ne i32 %20, 0
  %22 = icmp slt i32 %19, 0
  %23 = or i1 %22, %21
  br i1 %23, label %25, label %24

; <label>:24:                                     ; preds = %16
  br label %28

; <label>:25:                                     ; preds = %16
  store i32 -3, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:26:                                     ; preds = %28
  %27 = icmp slt i64 %31, %17
  br i1 %27, label %28, label %36

; <label>:28:                                     ; preds = %24, %26
  %29 = phi i32 [ %33, %26 ], [ 0, %24 ]
  %30 = phi i64 [ %31, %26 ], [ 0, %24 ]
  %31 = add nuw nsw i64 %30, 1
  %32 = getelementptr inbounds i32, i32* %1, i64 %31
  %33 = load i32, i32* %32, align 4, !tbaa !11
  %34 = icmp sgt i32 %29, %33
  br i1 %34, label %35, label %26

; <label>:35:                                     ; preds = %28
  store i32 -3, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:36:                                     ; preds = %26
  %37 = tail call i8* @klu_malloc(i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %38 = bitcast i8* %37 to i32*
  %39 = load i32, i32* %9, align 4, !tbaa !3
  %40 = icmp slt i32 %39, 0
  br i1 %40, label %41, label %42

; <label>:41:                                     ; preds = %36
  store i32 -2, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:42:                                     ; preds = %36
  %43 = zext i32 %0 to i64
  %44 = shl nuw nsw i64 %43, 2
  call void @llvm.memset.p0i8.i64(i8* %37, i8 -1, i64 %44, i32 4, i1 false)
  br label %45

; <label>:45:                                     ; preds = %42, %75
  %46 = phi i64 [ 0, %42 ], [ %47, %75 ]
  %47 = add nuw nsw i64 %46, 1
  %48 = getelementptr inbounds i32, i32* %1, i64 %47
  %49 = load i32, i32* %48, align 4, !tbaa !11
  %50 = getelementptr inbounds i32, i32* %1, i64 %46
  %51 = load i32, i32* %50, align 4, !tbaa !11
  %52 = icmp slt i32 %51, %49
  br i1 %52, label %53, label %75

; <label>:53:                                     ; preds = %45
  %54 = sext i32 %51 to i64
  %55 = sext i32 %49 to i64
  %56 = trunc i64 %46 to i32
  br label %57

; <label>:57:                                     ; preds = %53, %72
  %58 = phi i64 [ %54, %53 ], [ %73, %72 ]
  %59 = getelementptr inbounds i32, i32* %2, i64 %58
  %60 = load i32, i32* %59, align 4, !tbaa !11
  %61 = icmp sgt i32 %60, -1
  %62 = icmp slt i32 %60, %0
  %63 = and i1 %61, %62
  br i1 %63, label %64, label %70

; <label>:64:                                     ; preds = %57
  %65 = sext i32 %60 to i64
  %66 = getelementptr inbounds i32, i32* %38, i64 %65
  %67 = load i32, i32* %66, align 4, !tbaa !11
  %68 = zext i32 %67 to i64
  %69 = icmp eq i64 %46, %68
  br i1 %69, label %70, label %72

; <label>:70:                                     ; preds = %57, %64
  %71 = tail call i8* @klu_free(i8* %37, i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #3
  store i32 -3, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:72:                                     ; preds = %64
  store i32 %56, i32* %66, align 4, !tbaa !11
  %73 = add nsw i64 %58, 1
  %74 = icmp slt i64 %73, %55
  br i1 %74, label %57, label %75

; <label>:75:                                     ; preds = %72, %45
  %76 = icmp slt i64 %47, %17
  br i1 %76, label %45, label %77

; <label>:77:                                     ; preds = %75
  %78 = tail call i8* @klu_malloc(i64 1, i64 96, %struct.klu_common_struct* nonnull %3) #3
  %79 = bitcast %struct.klu_symbolic** %5 to i8**
  store i8* %78, i8** %79, align 8, !tbaa !12
  %80 = load i32, i32* %9, align 4, !tbaa !3
  %81 = icmp slt i32 %80, 0
  br i1 %81, label %82, label %84

; <label>:82:                                     ; preds = %77
  %83 = tail call i8* @klu_free(i8* %37, i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #3
  store i32 -2, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:84:                                     ; preds = %77
  %85 = bitcast i8* %78 to %struct.klu_symbolic*
  %86 = tail call i8* @klu_malloc(i64 %17, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %87 = add nsw i32 %0, 1
  %88 = sext i32 %87 to i64
  %89 = tail call i8* @klu_malloc(i64 %88, i64 4, %struct.klu_common_struct* nonnull %3) #3
  %90 = tail call i8* @klu_malloc(i64 %17, i64 8, %struct.klu_common_struct* nonnull %3) #3
  %91 = getelementptr inbounds i8, i8* %78, i64 40
  %92 = bitcast i8* %91 to i32*
  store i32 %0, i32* %92, align 8, !tbaa !13
  %93 = getelementptr inbounds i8, i8* %78, i64 44
  %94 = bitcast i8* %93 to i32*
  store i32 %19, i32* %94, align 4, !tbaa !15
  %95 = getelementptr inbounds i8, i8* %78, i64 48
  %96 = bitcast i8* %95 to i8**
  store i8* %37, i8** %96, align 8, !tbaa !16
  %97 = getelementptr inbounds i8, i8* %78, i64 56
  %98 = bitcast i8* %97 to i8**
  store i8* %86, i8** %98, align 8, !tbaa !17
  %99 = getelementptr inbounds i8, i8* %78, i64 64
  %100 = bitcast i8* %99 to i8**
  store i8* %89, i8** %100, align 8, !tbaa !18
  %101 = getelementptr inbounds i8, i8* %78, i64 32
  %102 = bitcast i8* %101 to i8**
  store i8* %90, i8** %102, align 8, !tbaa !19
  %103 = load i32, i32* %9, align 4, !tbaa !3
  %104 = icmp slt i32 %103, 0
  br i1 %104, label %105, label %107

; <label>:105:                                    ; preds = %84
  %106 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %5, %struct.klu_common_struct* nonnull %3) #3
  store i32 -2, i32* %9, align 4, !tbaa !3
  br label %107

; <label>:107:                                    ; preds = %84, %4, %105, %82, %70, %41, %35, %25, %15
  %108 = phi %struct.klu_symbolic* [ null, %15 ], [ null, %25 ], [ null, %35 ], [ null, %41 ], [ null, %70 ], [ null, %82 ], [ null, %105 ], [ null, %4 ], [ %85, %84 ]
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %6) #3
  ret %struct.klu_symbolic* %108
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define %struct.klu_symbolic* @klu_analyze_given(i32, i32*, i32*, i32* readonly, i32* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %7 = alloca %struct.klu_symbolic*, align 8
  %8 = bitcast %struct.klu_symbolic** %7 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %8) #3
  %9 = tail call %struct.klu_symbolic* @klu_alloc_symbolic(i32 %0, i32* %1, i32* %2, %struct.klu_common_struct* %5)
  store %struct.klu_symbolic* %9, %struct.klu_symbolic** %7, align 8, !tbaa !12
  %10 = icmp eq %struct.klu_symbolic* %9, null
  br i1 %10, label %955, label %11

; <label>:11:                                     ; preds = %6
  %12 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 7
  %13 = load i32*, i32** %12, align 8, !tbaa !16
  %14 = bitcast i32* %13 to i8*
  %15 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 8
  %16 = load i32*, i32** %15, align 8, !tbaa !17
  %17 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 9
  %18 = load i32*, i32** %17, align 8, !tbaa !18
  %19 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 4
  %20 = load double*, double** %19, align 8, !tbaa !19
  %21 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 6
  %22 = load i32, i32* %21, align 4, !tbaa !15
  %23 = icmp eq i32* %4, null
  %24 = icmp sgt i32 %0, 0
  br i1 %23, label %25, label %100

; <label>:25:                                     ; preds = %11
  br i1 %24, label %26, label %229

; <label>:26:                                     ; preds = %25
  %27 = zext i32 %0 to i64
  %28 = icmp ult i32 %0, 8
  br i1 %28, label %92, label %29

; <label>:29:                                     ; preds = %26
  %30 = and i64 %27, 4294967288
  %31 = add nsw i64 %30, -8
  %32 = lshr exact i64 %31, 3
  %33 = add nuw nsw i64 %32, 1
  %34 = and i64 %33, 3
  %35 = icmp ult i64 %31, 24
  br i1 %35, label %72, label %36

; <label>:36:                                     ; preds = %29
  %37 = sub nsw i64 %33, %34
  br label %38

; <label>:38:                                     ; preds = %38, %36
  %39 = phi i64 [ 0, %36 ], [ %68, %38 ]
  %40 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %36 ], [ %69, %38 ]
  %41 = phi i64 [ %37, %36 ], [ %70, %38 ]
  %42 = getelementptr inbounds i32, i32* %16, i64 %39
  %43 = add <4 x i32> %40, <i32 4, i32 4, i32 4, i32 4>
  %44 = bitcast i32* %42 to <4 x i32>*
  store <4 x i32> %40, <4 x i32>* %44, align 4, !tbaa !11
  %45 = getelementptr i32, i32* %42, i64 4
  %46 = bitcast i32* %45 to <4 x i32>*
  store <4 x i32> %43, <4 x i32>* %46, align 4, !tbaa !11
  %47 = or i64 %39, 8
  %48 = add <4 x i32> %40, <i32 8, i32 8, i32 8, i32 8>
  %49 = getelementptr inbounds i32, i32* %16, i64 %47
  %50 = add <4 x i32> %40, <i32 12, i32 12, i32 12, i32 12>
  %51 = bitcast i32* %49 to <4 x i32>*
  store <4 x i32> %48, <4 x i32>* %51, align 4, !tbaa !11
  %52 = getelementptr i32, i32* %49, i64 4
  %53 = bitcast i32* %52 to <4 x i32>*
  store <4 x i32> %50, <4 x i32>* %53, align 4, !tbaa !11
  %54 = or i64 %39, 16
  %55 = add <4 x i32> %40, <i32 16, i32 16, i32 16, i32 16>
  %56 = getelementptr inbounds i32, i32* %16, i64 %54
  %57 = add <4 x i32> %40, <i32 20, i32 20, i32 20, i32 20>
  %58 = bitcast i32* %56 to <4 x i32>*
  store <4 x i32> %55, <4 x i32>* %58, align 4, !tbaa !11
  %59 = getelementptr i32, i32* %56, i64 4
  %60 = bitcast i32* %59 to <4 x i32>*
  store <4 x i32> %57, <4 x i32>* %60, align 4, !tbaa !11
  %61 = or i64 %39, 24
  %62 = add <4 x i32> %40, <i32 24, i32 24, i32 24, i32 24>
  %63 = getelementptr inbounds i32, i32* %16, i64 %61
  %64 = add <4 x i32> %40, <i32 28, i32 28, i32 28, i32 28>
  %65 = bitcast i32* %63 to <4 x i32>*
  store <4 x i32> %62, <4 x i32>* %65, align 4, !tbaa !11
  %66 = getelementptr i32, i32* %63, i64 4
  %67 = bitcast i32* %66 to <4 x i32>*
  store <4 x i32> %64, <4 x i32>* %67, align 4, !tbaa !11
  %68 = add i64 %39, 32
  %69 = add <4 x i32> %40, <i32 32, i32 32, i32 32, i32 32>
  %70 = add i64 %41, -4
  %71 = icmp eq i64 %70, 0
  br i1 %71, label %72, label %38, !llvm.loop !20

; <label>:72:                                     ; preds = %38, %29
  %73 = phi i64 [ 0, %29 ], [ %68, %38 ]
  %74 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %29 ], [ %69, %38 ]
  %75 = icmp eq i64 %34, 0
  br i1 %75, label %90, label %76

; <label>:76:                                     ; preds = %72
  br label %77

; <label>:77:                                     ; preds = %77, %76
  %78 = phi i64 [ %73, %76 ], [ %86, %77 ]
  %79 = phi <4 x i32> [ %74, %76 ], [ %87, %77 ]
  %80 = phi i64 [ %34, %76 ], [ %88, %77 ]
  %81 = getelementptr inbounds i32, i32* %16, i64 %78
  %82 = add <4 x i32> %79, <i32 4, i32 4, i32 4, i32 4>
  %83 = bitcast i32* %81 to <4 x i32>*
  store <4 x i32> %79, <4 x i32>* %83, align 4, !tbaa !11
  %84 = getelementptr i32, i32* %81, i64 4
  %85 = bitcast i32* %84 to <4 x i32>*
  store <4 x i32> %82, <4 x i32>* %85, align 4, !tbaa !11
  %86 = add i64 %78, 8
  %87 = add <4 x i32> %79, <i32 8, i32 8, i32 8, i32 8>
  %88 = add i64 %80, -1
  %89 = icmp eq i64 %88, 0
  br i1 %89, label %90, label %77, !llvm.loop !22

; <label>:90:                                     ; preds = %77, %72
  %91 = icmp eq i64 %30, %27
  br i1 %91, label %229, label %92

; <label>:92:                                     ; preds = %90, %26
  %93 = phi i64 [ 0, %26 ], [ %30, %90 ]
  br label %94

; <label>:94:                                     ; preds = %92, %94
  %95 = phi i64 [ %98, %94 ], [ %93, %92 ]
  %96 = getelementptr inbounds i32, i32* %16, i64 %95
  %97 = trunc i64 %95 to i32
  store i32 %97, i32* %96, align 4, !tbaa !11
  %98 = add nuw nsw i64 %95, 1
  %99 = icmp eq i64 %98, %27
  br i1 %99, label %229, label %94, !llvm.loop !24

; <label>:100:                                    ; preds = %11
  br i1 %24, label %101, label %229

; <label>:101:                                    ; preds = %100
  %102 = zext i32 %0 to i64
  %103 = icmp ult i32 %0, 8
  br i1 %103, label %190, label %104

; <label>:104:                                    ; preds = %101
  %105 = getelementptr i32, i32* %16, i64 %102
  %106 = getelementptr i32, i32* %4, i64 %102
  %107 = icmp ult i32* %16, %106
  %108 = icmp ugt i32* %105, %4
  %109 = and i1 %107, %108
  br i1 %109, label %190, label %110

; <label>:110:                                    ; preds = %104
  %111 = and i64 %102, 4294967288
  %112 = add nsw i64 %111, -8
  %113 = lshr exact i64 %112, 3
  %114 = add nuw nsw i64 %113, 1
  %115 = and i64 %114, 3
  %116 = icmp ult i64 %112, 24
  br i1 %116, label %168, label %117

; <label>:117:                                    ; preds = %110
  %118 = sub nsw i64 %114, %115
  br label %119

; <label>:119:                                    ; preds = %119, %117
  %120 = phi i64 [ 0, %117 ], [ %165, %119 ]
  %121 = phi i64 [ %118, %117 ], [ %166, %119 ]
  %122 = getelementptr inbounds i32, i32* %4, i64 %120
  %123 = bitcast i32* %122 to <4 x i32>*
  %124 = load <4 x i32>, <4 x i32>* %123, align 4, !tbaa !11, !alias.scope !26
  %125 = getelementptr i32, i32* %122, i64 4
  %126 = bitcast i32* %125 to <4 x i32>*
  %127 = load <4 x i32>, <4 x i32>* %126, align 4, !tbaa !11, !alias.scope !26
  %128 = getelementptr inbounds i32, i32* %16, i64 %120
  %129 = bitcast i32* %128 to <4 x i32>*
  store <4 x i32> %124, <4 x i32>* %129, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %130 = getelementptr i32, i32* %128, i64 4
  %131 = bitcast i32* %130 to <4 x i32>*
  store <4 x i32> %127, <4 x i32>* %131, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %132 = or i64 %120, 8
  %133 = getelementptr inbounds i32, i32* %4, i64 %132
  %134 = bitcast i32* %133 to <4 x i32>*
  %135 = load <4 x i32>, <4 x i32>* %134, align 4, !tbaa !11, !alias.scope !26
  %136 = getelementptr i32, i32* %133, i64 4
  %137 = bitcast i32* %136 to <4 x i32>*
  %138 = load <4 x i32>, <4 x i32>* %137, align 4, !tbaa !11, !alias.scope !26
  %139 = getelementptr inbounds i32, i32* %16, i64 %132
  %140 = bitcast i32* %139 to <4 x i32>*
  store <4 x i32> %135, <4 x i32>* %140, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %141 = getelementptr i32, i32* %139, i64 4
  %142 = bitcast i32* %141 to <4 x i32>*
  store <4 x i32> %138, <4 x i32>* %142, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %143 = or i64 %120, 16
  %144 = getelementptr inbounds i32, i32* %4, i64 %143
  %145 = bitcast i32* %144 to <4 x i32>*
  %146 = load <4 x i32>, <4 x i32>* %145, align 4, !tbaa !11, !alias.scope !26
  %147 = getelementptr i32, i32* %144, i64 4
  %148 = bitcast i32* %147 to <4 x i32>*
  %149 = load <4 x i32>, <4 x i32>* %148, align 4, !tbaa !11, !alias.scope !26
  %150 = getelementptr inbounds i32, i32* %16, i64 %143
  %151 = bitcast i32* %150 to <4 x i32>*
  store <4 x i32> %146, <4 x i32>* %151, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %152 = getelementptr i32, i32* %150, i64 4
  %153 = bitcast i32* %152 to <4 x i32>*
  store <4 x i32> %149, <4 x i32>* %153, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %154 = or i64 %120, 24
  %155 = getelementptr inbounds i32, i32* %4, i64 %154
  %156 = bitcast i32* %155 to <4 x i32>*
  %157 = load <4 x i32>, <4 x i32>* %156, align 4, !tbaa !11, !alias.scope !26
  %158 = getelementptr i32, i32* %155, i64 4
  %159 = bitcast i32* %158 to <4 x i32>*
  %160 = load <4 x i32>, <4 x i32>* %159, align 4, !tbaa !11, !alias.scope !26
  %161 = getelementptr inbounds i32, i32* %16, i64 %154
  %162 = bitcast i32* %161 to <4 x i32>*
  store <4 x i32> %157, <4 x i32>* %162, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %163 = getelementptr i32, i32* %161, i64 4
  %164 = bitcast i32* %163 to <4 x i32>*
  store <4 x i32> %160, <4 x i32>* %164, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %165 = add i64 %120, 32
  %166 = add i64 %121, -4
  %167 = icmp eq i64 %166, 0
  br i1 %167, label %168, label %119, !llvm.loop !31

; <label>:168:                                    ; preds = %119, %110
  %169 = phi i64 [ 0, %110 ], [ %165, %119 ]
  %170 = icmp eq i64 %115, 0
  br i1 %170, label %188, label %171

; <label>:171:                                    ; preds = %168
  br label %172

; <label>:172:                                    ; preds = %172, %171
  %173 = phi i64 [ %169, %171 ], [ %185, %172 ]
  %174 = phi i64 [ %115, %171 ], [ %186, %172 ]
  %175 = getelementptr inbounds i32, i32* %4, i64 %173
  %176 = bitcast i32* %175 to <4 x i32>*
  %177 = load <4 x i32>, <4 x i32>* %176, align 4, !tbaa !11, !alias.scope !26
  %178 = getelementptr i32, i32* %175, i64 4
  %179 = bitcast i32* %178 to <4 x i32>*
  %180 = load <4 x i32>, <4 x i32>* %179, align 4, !tbaa !11, !alias.scope !26
  %181 = getelementptr inbounds i32, i32* %16, i64 %173
  %182 = bitcast i32* %181 to <4 x i32>*
  store <4 x i32> %177, <4 x i32>* %182, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %183 = getelementptr i32, i32* %181, i64 4
  %184 = bitcast i32* %183 to <4 x i32>*
  store <4 x i32> %180, <4 x i32>* %184, align 4, !tbaa !11, !alias.scope !29, !noalias !26
  %185 = add i64 %173, 8
  %186 = add i64 %174, -1
  %187 = icmp eq i64 %186, 0
  br i1 %187, label %188, label %172, !llvm.loop !32

; <label>:188:                                    ; preds = %172, %168
  %189 = icmp eq i64 %111, %102
  br i1 %189, label %229, label %190

; <label>:190:                                    ; preds = %188, %104, %101
  %191 = phi i64 [ 0, %104 ], [ 0, %101 ], [ %111, %188 ]
  %192 = add nsw i64 %102, -1
  %193 = sub nsw i64 %192, %191
  %194 = and i64 %102, 3
  %195 = icmp eq i64 %194, 0
  br i1 %195, label %206, label %196

; <label>:196:                                    ; preds = %190
  br label %197

; <label>:197:                                    ; preds = %197, %196
  %198 = phi i64 [ %203, %197 ], [ %191, %196 ]
  %199 = phi i64 [ %204, %197 ], [ %194, %196 ]
  %200 = getelementptr inbounds i32, i32* %4, i64 %198
  %201 = load i32, i32* %200, align 4, !tbaa !11
  %202 = getelementptr inbounds i32, i32* %16, i64 %198
  store i32 %201, i32* %202, align 4, !tbaa !11
  %203 = add nuw nsw i64 %198, 1
  %204 = add i64 %199, -1
  %205 = icmp eq i64 %204, 0
  br i1 %205, label %206, label %197, !llvm.loop !33

; <label>:206:                                    ; preds = %197, %190
  %207 = phi i64 [ %191, %190 ], [ %203, %197 ]
  %208 = icmp ult i64 %193, 3
  br i1 %208, label %229, label %209

; <label>:209:                                    ; preds = %206
  br label %210

; <label>:210:                                    ; preds = %210, %209
  %211 = phi i64 [ %207, %209 ], [ %227, %210 ]
  %212 = getelementptr inbounds i32, i32* %4, i64 %211
  %213 = load i32, i32* %212, align 4, !tbaa !11
  %214 = getelementptr inbounds i32, i32* %16, i64 %211
  store i32 %213, i32* %214, align 4, !tbaa !11
  %215 = add nuw nsw i64 %211, 1
  %216 = getelementptr inbounds i32, i32* %4, i64 %215
  %217 = load i32, i32* %216, align 4, !tbaa !11
  %218 = getelementptr inbounds i32, i32* %16, i64 %215
  store i32 %217, i32* %218, align 4, !tbaa !11
  %219 = add nsw i64 %211, 2
  %220 = getelementptr inbounds i32, i32* %4, i64 %219
  %221 = load i32, i32* %220, align 4, !tbaa !11
  %222 = getelementptr inbounds i32, i32* %16, i64 %219
  store i32 %221, i32* %222, align 4, !tbaa !11
  %223 = add nsw i64 %211, 3
  %224 = getelementptr inbounds i32, i32* %4, i64 %223
  %225 = load i32, i32* %224, align 4, !tbaa !11
  %226 = getelementptr inbounds i32, i32* %16, i64 %223
  store i32 %225, i32* %226, align 4, !tbaa !11
  %227 = add nsw i64 %211, 4
  %228 = icmp eq i64 %227, %102
  br i1 %228, label %229, label %210, !llvm.loop !34

; <label>:229:                                    ; preds = %206, %210, %94, %188, %90, %100, %25
  %230 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 5
  %231 = load i32, i32* %230, align 8, !tbaa !35
  %232 = icmp ne i32 %231, 0
  %233 = zext i1 %232 to i32
  %234 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 13
  store i32 2, i32* %234, align 4, !tbaa !36
  %235 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 14
  store i32 %233, i32* %235, align 8, !tbaa !37
  br i1 %232, label %236, label %742

; <label>:236:                                    ; preds = %229
  %237 = shl nsw i32 %0, 2
  %238 = sext i32 %237 to i64
  %239 = tail call i8* @klu_malloc(i64 %238, i64 4, %struct.klu_common_struct* nonnull %5) #3
  %240 = bitcast i8* %239 to i32*
  %241 = sext i32 %0 to i64
  %242 = tail call i8* @klu_malloc(i64 %241, i64 4, %struct.klu_common_struct* nonnull %5) #3
  %243 = bitcast i8* %242 to i32*
  %244 = icmp ne i32* %3, null
  br i1 %244, label %245, label %250

; <label>:245:                                    ; preds = %236
  %246 = add nsw i32 %22, 1
  %247 = sext i32 %246 to i64
  %248 = tail call i8* @klu_malloc(i64 %247, i64 4, %struct.klu_common_struct* nonnull %5) #3
  %249 = bitcast i8* %248 to i32*
  br label %250

; <label>:250:                                    ; preds = %236, %245
  %251 = phi i32* [ %249, %245 ], [ %2, %236 ]
  %252 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %5, i64 0, i32 11
  %253 = load i32, i32* %252, align 4, !tbaa !3
  %254 = icmp slt i32 %253, 0
  br i1 %254, label %255, label %263

; <label>:255:                                    ; preds = %250
  %256 = tail call i8* @klu_free(i8* %239, i64 %238, i64 4, %struct.klu_common_struct* nonnull %5) #3
  %257 = tail call i8* @klu_free(i8* %242, i64 %241, i64 4, %struct.klu_common_struct* nonnull %5) #3
  br i1 %244, label %258, label %740

; <label>:258:                                    ; preds = %255
  %259 = bitcast i32* %251 to i8*
  %260 = add nsw i32 %22, 1
  %261 = sext i32 %260 to i64
  %262 = tail call i8* @klu_free(i8* %259, i64 %261, i64 4, %struct.klu_common_struct* nonnull %5) #3
  br label %740

; <label>:263:                                    ; preds = %250
  br i1 %244, label %266, label %264

; <label>:264:                                    ; preds = %263
  %265 = tail call i32 @btf_strongcomp(i32 %0, i32* %1, i32* %251, i32* %16, i32* %13, i32* %18, i32* %240) #3
  br label %565

; <label>:266:                                    ; preds = %263
  br i1 %24, label %267, label %318

; <label>:267:                                    ; preds = %266
  %268 = zext i32 %0 to i64
  %269 = add nsw i64 %268, -1
  %270 = and i64 %268, 3
  %271 = icmp ult i64 %269, 3
  br i1 %271, label %303, label %272

; <label>:272:                                    ; preds = %267
  %273 = sub nsw i64 %268, %270
  br label %274

; <label>:274:                                    ; preds = %274, %272
  %275 = phi i64 [ 0, %272 ], [ %300, %274 ]
  %276 = phi i64 [ %273, %272 ], [ %301, %274 ]
  %277 = getelementptr inbounds i32, i32* %3, i64 %275
  %278 = load i32, i32* %277, align 4, !tbaa !11
  %279 = sext i32 %278 to i64
  %280 = getelementptr inbounds i32, i32* %243, i64 %279
  %281 = trunc i64 %275 to i32
  store i32 %281, i32* %280, align 4, !tbaa !11
  %282 = or i64 %275, 1
  %283 = getelementptr inbounds i32, i32* %3, i64 %282
  %284 = load i32, i32* %283, align 4, !tbaa !11
  %285 = sext i32 %284 to i64
  %286 = getelementptr inbounds i32, i32* %243, i64 %285
  %287 = trunc i64 %282 to i32
  store i32 %287, i32* %286, align 4, !tbaa !11
  %288 = or i64 %275, 2
  %289 = getelementptr inbounds i32, i32* %3, i64 %288
  %290 = load i32, i32* %289, align 4, !tbaa !11
  %291 = sext i32 %290 to i64
  %292 = getelementptr inbounds i32, i32* %243, i64 %291
  %293 = trunc i64 %288 to i32
  store i32 %293, i32* %292, align 4, !tbaa !11
  %294 = or i64 %275, 3
  %295 = getelementptr inbounds i32, i32* %3, i64 %294
  %296 = load i32, i32* %295, align 4, !tbaa !11
  %297 = sext i32 %296 to i64
  %298 = getelementptr inbounds i32, i32* %243, i64 %297
  %299 = trunc i64 %294 to i32
  store i32 %299, i32* %298, align 4, !tbaa !11
  %300 = add nuw nsw i64 %275, 4
  %301 = add i64 %276, -4
  %302 = icmp eq i64 %301, 0
  br i1 %302, label %303, label %274

; <label>:303:                                    ; preds = %274, %267
  %304 = phi i64 [ 0, %267 ], [ %300, %274 ]
  %305 = icmp eq i64 %270, 0
  br i1 %305, label %318, label %306

; <label>:306:                                    ; preds = %303
  br label %307

; <label>:307:                                    ; preds = %307, %306
  %308 = phi i64 [ %304, %306 ], [ %315, %307 ]
  %309 = phi i64 [ %270, %306 ], [ %316, %307 ]
  %310 = getelementptr inbounds i32, i32* %3, i64 %308
  %311 = load i32, i32* %310, align 4, !tbaa !11
  %312 = sext i32 %311 to i64
  %313 = getelementptr inbounds i32, i32* %243, i64 %312
  %314 = trunc i64 %308 to i32
  store i32 %314, i32* %313, align 4, !tbaa !11
  %315 = add nuw nsw i64 %308, 1
  %316 = add i64 %309, -1
  %317 = icmp eq i64 %316, 0
  br i1 %317, label %318, label %307, !llvm.loop !38

; <label>:318:                                    ; preds = %303, %307, %266
  %319 = icmp sgt i32 %22, 0
  br i1 %319, label %320, label %376

; <label>:320:                                    ; preds = %318
  %321 = zext i32 %22 to i64
  %322 = add nsw i64 %321, -1
  %323 = and i64 %321, 3
  %324 = icmp ult i64 %322, 3
  br i1 %324, label %360, label %325

; <label>:325:                                    ; preds = %320
  %326 = sub nsw i64 %321, %323
  br label %327

; <label>:327:                                    ; preds = %327, %325
  %328 = phi i64 [ 0, %325 ], [ %357, %327 ]
  %329 = phi i64 [ %326, %325 ], [ %358, %327 ]
  %330 = getelementptr inbounds i32, i32* %2, i64 %328
  %331 = load i32, i32* %330, align 4, !tbaa !11
  %332 = sext i32 %331 to i64
  %333 = getelementptr inbounds i32, i32* %243, i64 %332
  %334 = load i32, i32* %333, align 4, !tbaa !11
  %335 = getelementptr inbounds i32, i32* %251, i64 %328
  store i32 %334, i32* %335, align 4, !tbaa !11
  %336 = or i64 %328, 1
  %337 = getelementptr inbounds i32, i32* %2, i64 %336
  %338 = load i32, i32* %337, align 4, !tbaa !11
  %339 = sext i32 %338 to i64
  %340 = getelementptr inbounds i32, i32* %243, i64 %339
  %341 = load i32, i32* %340, align 4, !tbaa !11
  %342 = getelementptr inbounds i32, i32* %251, i64 %336
  store i32 %341, i32* %342, align 4, !tbaa !11
  %343 = or i64 %328, 2
  %344 = getelementptr inbounds i32, i32* %2, i64 %343
  %345 = load i32, i32* %344, align 4, !tbaa !11
  %346 = sext i32 %345 to i64
  %347 = getelementptr inbounds i32, i32* %243, i64 %346
  %348 = load i32, i32* %347, align 4, !tbaa !11
  %349 = getelementptr inbounds i32, i32* %251, i64 %343
  store i32 %348, i32* %349, align 4, !tbaa !11
  %350 = or i64 %328, 3
  %351 = getelementptr inbounds i32, i32* %2, i64 %350
  %352 = load i32, i32* %351, align 4, !tbaa !11
  %353 = sext i32 %352 to i64
  %354 = getelementptr inbounds i32, i32* %243, i64 %353
  %355 = load i32, i32* %354, align 4, !tbaa !11
  %356 = getelementptr inbounds i32, i32* %251, i64 %350
  store i32 %355, i32* %356, align 4, !tbaa !11
  %357 = add nuw nsw i64 %328, 4
  %358 = add i64 %329, -4
  %359 = icmp eq i64 %358, 0
  br i1 %359, label %360, label %327

; <label>:360:                                    ; preds = %327, %320
  %361 = phi i64 [ 0, %320 ], [ %357, %327 ]
  %362 = icmp eq i64 %323, 0
  br i1 %362, label %376, label %363

; <label>:363:                                    ; preds = %360
  br label %364

; <label>:364:                                    ; preds = %364, %363
  %365 = phi i64 [ %361, %363 ], [ %373, %364 ]
  %366 = phi i64 [ %323, %363 ], [ %374, %364 ]
  %367 = getelementptr inbounds i32, i32* %2, i64 %365
  %368 = load i32, i32* %367, align 4, !tbaa !11
  %369 = sext i32 %368 to i64
  %370 = getelementptr inbounds i32, i32* %243, i64 %369
  %371 = load i32, i32* %370, align 4, !tbaa !11
  %372 = getelementptr inbounds i32, i32* %251, i64 %365
  store i32 %371, i32* %372, align 4, !tbaa !11
  %373 = add nuw nsw i64 %365, 1
  %374 = add i64 %366, -1
  %375 = icmp eq i64 %374, 0
  br i1 %375, label %376, label %364, !llvm.loop !39

; <label>:376:                                    ; preds = %360, %364, %318
  %377 = tail call i32 @btf_strongcomp(i32 %0, i32* %1, i32* %251, i32* %16, i32* %13, i32* %18, i32* %240) #3
  br i1 %24, label %378, label %618

; <label>:378:                                    ; preds = %376
  %379 = zext i32 %0 to i64
  %380 = add nsw i64 %379, -1
  %381 = and i64 %379, 3
  %382 = icmp ult i64 %380, 3
  br i1 %382, label %418, label %383

; <label>:383:                                    ; preds = %378
  %384 = sub nsw i64 %379, %381
  br label %385

; <label>:385:                                    ; preds = %385, %383
  %386 = phi i64 [ 0, %383 ], [ %415, %385 ]
  %387 = phi i64 [ %384, %383 ], [ %416, %385 ]
  %388 = getelementptr inbounds i32, i32* %13, i64 %386
  %389 = load i32, i32* %388, align 4, !tbaa !11
  %390 = sext i32 %389 to i64
  %391 = getelementptr inbounds i32, i32* %3, i64 %390
  %392 = load i32, i32* %391, align 4, !tbaa !11
  %393 = getelementptr inbounds i32, i32* %240, i64 %386
  store i32 %392, i32* %393, align 4, !tbaa !11
  %394 = or i64 %386, 1
  %395 = getelementptr inbounds i32, i32* %13, i64 %394
  %396 = load i32, i32* %395, align 4, !tbaa !11
  %397 = sext i32 %396 to i64
  %398 = getelementptr inbounds i32, i32* %3, i64 %397
  %399 = load i32, i32* %398, align 4, !tbaa !11
  %400 = getelementptr inbounds i32, i32* %240, i64 %394
  store i32 %399, i32* %400, align 4, !tbaa !11
  %401 = or i64 %386, 2
  %402 = getelementptr inbounds i32, i32* %13, i64 %401
  %403 = load i32, i32* %402, align 4, !tbaa !11
  %404 = sext i32 %403 to i64
  %405 = getelementptr inbounds i32, i32* %3, i64 %404
  %406 = load i32, i32* %405, align 4, !tbaa !11
  %407 = getelementptr inbounds i32, i32* %240, i64 %401
  store i32 %406, i32* %407, align 4, !tbaa !11
  %408 = or i64 %386, 3
  %409 = getelementptr inbounds i32, i32* %13, i64 %408
  %410 = load i32, i32* %409, align 4, !tbaa !11
  %411 = sext i32 %410 to i64
  %412 = getelementptr inbounds i32, i32* %3, i64 %411
  %413 = load i32, i32* %412, align 4, !tbaa !11
  %414 = getelementptr inbounds i32, i32* %240, i64 %408
  store i32 %413, i32* %414, align 4, !tbaa !11
  %415 = add nuw nsw i64 %386, 4
  %416 = add i64 %387, -4
  %417 = icmp eq i64 %416, 0
  br i1 %417, label %418, label %385

; <label>:418:                                    ; preds = %385, %378
  %419 = phi i64 [ 0, %378 ], [ %415, %385 ]
  %420 = icmp eq i64 %381, 0
  br i1 %420, label %434, label %421

; <label>:421:                                    ; preds = %418
  br label %422

; <label>:422:                                    ; preds = %422, %421
  %423 = phi i64 [ %419, %421 ], [ %431, %422 ]
  %424 = phi i64 [ %381, %421 ], [ %432, %422 ]
  %425 = getelementptr inbounds i32, i32* %13, i64 %423
  %426 = load i32, i32* %425, align 4, !tbaa !11
  %427 = sext i32 %426 to i64
  %428 = getelementptr inbounds i32, i32* %3, i64 %427
  %429 = load i32, i32* %428, align 4, !tbaa !11
  %430 = getelementptr inbounds i32, i32* %240, i64 %423
  store i32 %429, i32* %430, align 4, !tbaa !11
  %431 = add nuw nsw i64 %423, 1
  %432 = add i64 %424, -1
  %433 = icmp eq i64 %432, 0
  br i1 %433, label %434, label %422, !llvm.loop !40

; <label>:434:                                    ; preds = %422, %418
  br i1 %24, label %435, label %618

; <label>:435:                                    ; preds = %434
  %436 = zext i32 %0 to i64
  %437 = icmp ult i32 %0, 8
  br i1 %437, label %526, label %438

; <label>:438:                                    ; preds = %435
  %439 = getelementptr i32, i32* %13, i64 %379
  %440 = bitcast i32* %439 to i8*
  %441 = shl nuw nsw i64 %379, 2
  %442 = getelementptr i8, i8* %239, i64 %441
  %443 = icmp ugt i8* %442, %14
  %444 = icmp ult i8* %239, %440
  %445 = and i1 %443, %444
  br i1 %445, label %526, label %446

; <label>:446:                                    ; preds = %438
  %447 = and i64 %379, 4294967288
  %448 = add nsw i64 %447, -8
  %449 = lshr exact i64 %448, 3
  %450 = add nuw nsw i64 %449, 1
  %451 = and i64 %450, 3
  %452 = icmp ult i64 %448, 24
  br i1 %452, label %504, label %453

; <label>:453:                                    ; preds = %446
  %454 = sub nsw i64 %450, %451
  br label %455

; <label>:455:                                    ; preds = %455, %453
  %456 = phi i64 [ 0, %453 ], [ %501, %455 ]
  %457 = phi i64 [ %454, %453 ], [ %502, %455 ]
  %458 = getelementptr inbounds i32, i32* %240, i64 %456
  %459 = bitcast i32* %458 to <4 x i32>*
  %460 = load <4 x i32>, <4 x i32>* %459, align 4, !tbaa !11, !alias.scope !41
  %461 = getelementptr i32, i32* %458, i64 4
  %462 = bitcast i32* %461 to <4 x i32>*
  %463 = load <4 x i32>, <4 x i32>* %462, align 4, !tbaa !11, !alias.scope !41
  %464 = getelementptr inbounds i32, i32* %13, i64 %456
  %465 = bitcast i32* %464 to <4 x i32>*
  store <4 x i32> %460, <4 x i32>* %465, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %466 = getelementptr i32, i32* %464, i64 4
  %467 = bitcast i32* %466 to <4 x i32>*
  store <4 x i32> %463, <4 x i32>* %467, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %468 = or i64 %456, 8
  %469 = getelementptr inbounds i32, i32* %240, i64 %468
  %470 = bitcast i32* %469 to <4 x i32>*
  %471 = load <4 x i32>, <4 x i32>* %470, align 4, !tbaa !11, !alias.scope !41
  %472 = getelementptr i32, i32* %469, i64 4
  %473 = bitcast i32* %472 to <4 x i32>*
  %474 = load <4 x i32>, <4 x i32>* %473, align 4, !tbaa !11, !alias.scope !41
  %475 = getelementptr inbounds i32, i32* %13, i64 %468
  %476 = bitcast i32* %475 to <4 x i32>*
  store <4 x i32> %471, <4 x i32>* %476, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %477 = getelementptr i32, i32* %475, i64 4
  %478 = bitcast i32* %477 to <4 x i32>*
  store <4 x i32> %474, <4 x i32>* %478, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %479 = or i64 %456, 16
  %480 = getelementptr inbounds i32, i32* %240, i64 %479
  %481 = bitcast i32* %480 to <4 x i32>*
  %482 = load <4 x i32>, <4 x i32>* %481, align 4, !tbaa !11, !alias.scope !41
  %483 = getelementptr i32, i32* %480, i64 4
  %484 = bitcast i32* %483 to <4 x i32>*
  %485 = load <4 x i32>, <4 x i32>* %484, align 4, !tbaa !11, !alias.scope !41
  %486 = getelementptr inbounds i32, i32* %13, i64 %479
  %487 = bitcast i32* %486 to <4 x i32>*
  store <4 x i32> %482, <4 x i32>* %487, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %488 = getelementptr i32, i32* %486, i64 4
  %489 = bitcast i32* %488 to <4 x i32>*
  store <4 x i32> %485, <4 x i32>* %489, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %490 = or i64 %456, 24
  %491 = getelementptr inbounds i32, i32* %240, i64 %490
  %492 = bitcast i32* %491 to <4 x i32>*
  %493 = load <4 x i32>, <4 x i32>* %492, align 4, !tbaa !11, !alias.scope !41
  %494 = getelementptr i32, i32* %491, i64 4
  %495 = bitcast i32* %494 to <4 x i32>*
  %496 = load <4 x i32>, <4 x i32>* %495, align 4, !tbaa !11, !alias.scope !41
  %497 = getelementptr inbounds i32, i32* %13, i64 %490
  %498 = bitcast i32* %497 to <4 x i32>*
  store <4 x i32> %493, <4 x i32>* %498, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %499 = getelementptr i32, i32* %497, i64 4
  %500 = bitcast i32* %499 to <4 x i32>*
  store <4 x i32> %496, <4 x i32>* %500, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %501 = add i64 %456, 32
  %502 = add i64 %457, -4
  %503 = icmp eq i64 %502, 0
  br i1 %503, label %504, label %455, !llvm.loop !46

; <label>:504:                                    ; preds = %455, %446
  %505 = phi i64 [ 0, %446 ], [ %501, %455 ]
  %506 = icmp eq i64 %451, 0
  br i1 %506, label %524, label %507

; <label>:507:                                    ; preds = %504
  br label %508

; <label>:508:                                    ; preds = %508, %507
  %509 = phi i64 [ %505, %507 ], [ %521, %508 ]
  %510 = phi i64 [ %451, %507 ], [ %522, %508 ]
  %511 = getelementptr inbounds i32, i32* %240, i64 %509
  %512 = bitcast i32* %511 to <4 x i32>*
  %513 = load <4 x i32>, <4 x i32>* %512, align 4, !tbaa !11, !alias.scope !41
  %514 = getelementptr i32, i32* %511, i64 4
  %515 = bitcast i32* %514 to <4 x i32>*
  %516 = load <4 x i32>, <4 x i32>* %515, align 4, !tbaa !11, !alias.scope !41
  %517 = getelementptr inbounds i32, i32* %13, i64 %509
  %518 = bitcast i32* %517 to <4 x i32>*
  store <4 x i32> %513, <4 x i32>* %518, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %519 = getelementptr i32, i32* %517, i64 4
  %520 = bitcast i32* %519 to <4 x i32>*
  store <4 x i32> %516, <4 x i32>* %520, align 4, !tbaa !11, !alias.scope !44, !noalias !41
  %521 = add i64 %509, 8
  %522 = add i64 %510, -1
  %523 = icmp eq i64 %522, 0
  br i1 %523, label %524, label %508, !llvm.loop !47

; <label>:524:                                    ; preds = %508, %504
  %525 = icmp eq i64 %447, %379
  br i1 %525, label %565, label %526

; <label>:526:                                    ; preds = %524, %438, %435
  %527 = phi i64 [ 0, %438 ], [ 0, %435 ], [ %447, %524 ]
  %528 = add nsw i64 %436, -1
  %529 = sub nsw i64 %528, %527
  %530 = and i64 %436, 3
  %531 = icmp eq i64 %530, 0
  br i1 %531, label %542, label %532

; <label>:532:                                    ; preds = %526
  br label %533

; <label>:533:                                    ; preds = %533, %532
  %534 = phi i64 [ %539, %533 ], [ %527, %532 ]
  %535 = phi i64 [ %540, %533 ], [ %530, %532 ]
  %536 = getelementptr inbounds i32, i32* %240, i64 %534
  %537 = load i32, i32* %536, align 4, !tbaa !11
  %538 = getelementptr inbounds i32, i32* %13, i64 %534
  store i32 %537, i32* %538, align 4, !tbaa !11
  %539 = add nuw nsw i64 %534, 1
  %540 = add i64 %535, -1
  %541 = icmp eq i64 %540, 0
  br i1 %541, label %542, label %533, !llvm.loop !48

; <label>:542:                                    ; preds = %533, %526
  %543 = phi i64 [ %527, %526 ], [ %539, %533 ]
  %544 = icmp ult i64 %529, 3
  br i1 %544, label %565, label %545

; <label>:545:                                    ; preds = %542
  br label %546

; <label>:546:                                    ; preds = %546, %545
  %547 = phi i64 [ %543, %545 ], [ %563, %546 ]
  %548 = getelementptr inbounds i32, i32* %240, i64 %547
  %549 = load i32, i32* %548, align 4, !tbaa !11
  %550 = getelementptr inbounds i32, i32* %13, i64 %547
  store i32 %549, i32* %550, align 4, !tbaa !11
  %551 = add nuw nsw i64 %547, 1
  %552 = getelementptr inbounds i32, i32* %240, i64 %551
  %553 = load i32, i32* %552, align 4, !tbaa !11
  %554 = getelementptr inbounds i32, i32* %13, i64 %551
  store i32 %553, i32* %554, align 4, !tbaa !11
  %555 = add nsw i64 %547, 2
  %556 = getelementptr inbounds i32, i32* %240, i64 %555
  %557 = load i32, i32* %556, align 4, !tbaa !11
  %558 = getelementptr inbounds i32, i32* %13, i64 %555
  store i32 %557, i32* %558, align 4, !tbaa !11
  %559 = add nsw i64 %547, 3
  %560 = getelementptr inbounds i32, i32* %240, i64 %559
  %561 = load i32, i32* %560, align 4, !tbaa !11
  %562 = getelementptr inbounds i32, i32* %13, i64 %559
  store i32 %561, i32* %562, align 4, !tbaa !11
  %563 = add nsw i64 %547, 4
  %564 = icmp eq i64 %563, %436
  br i1 %564, label %565, label %546, !llvm.loop !49

; <label>:565:                                    ; preds = %542, %546, %524, %264
  %566 = phi i32 [ %265, %264 ], [ %377, %524 ], [ %377, %546 ], [ %377, %542 ]
  br i1 %24, label %567, label %618

; <label>:567:                                    ; preds = %565
  %568 = zext i32 %0 to i64
  %569 = add nsw i64 %568, -1
  %570 = and i64 %568, 3
  %571 = icmp ult i64 %569, 3
  br i1 %571, label %603, label %572

; <label>:572:                                    ; preds = %567
  %573 = sub nsw i64 %568, %570
  br label %574

; <label>:574:                                    ; preds = %574, %572
  %575 = phi i64 [ 0, %572 ], [ %600, %574 ]
  %576 = phi i64 [ %573, %572 ], [ %601, %574 ]
  %577 = getelementptr inbounds i32, i32* %13, i64 %575
  %578 = load i32, i32* %577, align 4, !tbaa !11
  %579 = sext i32 %578 to i64
  %580 = getelementptr inbounds i32, i32* %243, i64 %579
  %581 = trunc i64 %575 to i32
  store i32 %581, i32* %580, align 4, !tbaa !11
  %582 = or i64 %575, 1
  %583 = getelementptr inbounds i32, i32* %13, i64 %582
  %584 = load i32, i32* %583, align 4, !tbaa !11
  %585 = sext i32 %584 to i64
  %586 = getelementptr inbounds i32, i32* %243, i64 %585
  %587 = trunc i64 %582 to i32
  store i32 %587, i32* %586, align 4, !tbaa !11
  %588 = or i64 %575, 2
  %589 = getelementptr inbounds i32, i32* %13, i64 %588
  %590 = load i32, i32* %589, align 4, !tbaa !11
  %591 = sext i32 %590 to i64
  %592 = getelementptr inbounds i32, i32* %243, i64 %591
  %593 = trunc i64 %588 to i32
  store i32 %593, i32* %592, align 4, !tbaa !11
  %594 = or i64 %575, 3
  %595 = getelementptr inbounds i32, i32* %13, i64 %594
  %596 = load i32, i32* %595, align 4, !tbaa !11
  %597 = sext i32 %596 to i64
  %598 = getelementptr inbounds i32, i32* %243, i64 %597
  %599 = trunc i64 %594 to i32
  store i32 %599, i32* %598, align 4, !tbaa !11
  %600 = add nuw nsw i64 %575, 4
  %601 = add i64 %576, -4
  %602 = icmp eq i64 %601, 0
  br i1 %602, label %603, label %574

; <label>:603:                                    ; preds = %574, %567
  %604 = phi i64 [ 0, %567 ], [ %600, %574 ]
  %605 = icmp eq i64 %570, 0
  br i1 %605, label %618, label %606

; <label>:606:                                    ; preds = %603
  br label %607

; <label>:607:                                    ; preds = %607, %606
  %608 = phi i64 [ %604, %606 ], [ %615, %607 ]
  %609 = phi i64 [ %570, %606 ], [ %616, %607 ]
  %610 = getelementptr inbounds i32, i32* %13, i64 %608
  %611 = load i32, i32* %610, align 4, !tbaa !11
  %612 = sext i32 %611 to i64
  %613 = getelementptr inbounds i32, i32* %243, i64 %612
  %614 = trunc i64 %608 to i32
  store i32 %614, i32* %613, align 4, !tbaa !11
  %615 = add nuw nsw i64 %608, 1
  %616 = add i64 %609, -1
  %617 = icmp eq i64 %616, 0
  br i1 %617, label %618, label %607, !llvm.loop !50

; <label>:618:                                    ; preds = %603, %607, %376, %434, %565
  %619 = phi i32 [ %566, %565 ], [ %377, %434 ], [ %377, %376 ], [ %566, %607 ], [ %566, %603 ]
  %620 = icmp sgt i32 %619, 0
  br i1 %620, label %621, label %730

; <label>:621:                                    ; preds = %618
  %622 = load i32, i32* %18, align 4, !tbaa !11
  %623 = zext i32 %619 to i64
  br label %624

; <label>:624:                                    ; preds = %726, %621
  %625 = phi i32 [ %622, %621 ], [ %631, %726 ]
  %626 = phi i64 [ 0, %621 ], [ %629, %726 ]
  %627 = phi i32 [ 1, %621 ], [ %634, %726 ]
  %628 = phi i32 [ 0, %621 ], [ %727, %726 ]
  %629 = add nuw nsw i64 %626, 1
  %630 = getelementptr inbounds i32, i32* %18, i64 %629
  %631 = load i32, i32* %630, align 4, !tbaa !11
  %632 = sub nsw i32 %631, %625
  %633 = icmp sgt i32 %627, %632
  %634 = select i1 %633, i32 %627, i32 %632
  %635 = icmp sgt i32 %631, %625
  br i1 %635, label %636, label %726

; <label>:636:                                    ; preds = %624
  %637 = sext i32 %625 to i64
  %638 = sext i32 %631 to i64
  br label %639

; <label>:639:                                    ; preds = %722, %636
  %640 = phi i64 [ %637, %636 ], [ %724, %722 ]
  %641 = phi i32 [ %628, %636 ], [ %723, %722 ]
  %642 = getelementptr inbounds i32, i32* %16, i64 %640
  %643 = load i32, i32* %642, align 4, !tbaa !11
  %644 = add nsw i32 %643, 1
  %645 = sext i32 %644 to i64
  %646 = getelementptr inbounds i32, i32* %1, i64 %645
  %647 = load i32, i32* %646, align 4, !tbaa !11
  %648 = sext i32 %643 to i64
  %649 = getelementptr inbounds i32, i32* %1, i64 %648
  %650 = load i32, i32* %649, align 4, !tbaa !11
  %651 = icmp slt i32 %650, %647
  br i1 %651, label %652, label %722

; <label>:652:                                    ; preds = %639
  %653 = sext i32 %650 to i64
  %654 = sext i32 %647 to i64
  %655 = sub nsw i64 %654, %653
  %656 = add nsw i64 %654, -1
  %657 = sub nsw i64 %656, %653
  %658 = and i64 %655, 3
  %659 = icmp eq i64 %658, 0
  br i1 %659, label %676, label %660

; <label>:660:                                    ; preds = %652
  br label %661

; <label>:661:                                    ; preds = %661, %660
  %662 = phi i64 [ %653, %660 ], [ %673, %661 ]
  %663 = phi i32 [ %641, %660 ], [ %672, %661 ]
  %664 = phi i64 [ %658, %660 ], [ %674, %661 ]
  %665 = getelementptr inbounds i32, i32* %2, i64 %662
  %666 = load i32, i32* %665, align 4, !tbaa !11
  %667 = sext i32 %666 to i64
  %668 = getelementptr inbounds i32, i32* %243, i64 %667
  %669 = load i32, i32* %668, align 4, !tbaa !11
  %670 = icmp slt i32 %669, %625
  %671 = zext i1 %670 to i32
  %672 = add nsw i32 %663, %671
  %673 = add nsw i64 %662, 1
  %674 = add i64 %664, -1
  %675 = icmp eq i64 %674, 0
  br i1 %675, label %676, label %661, !llvm.loop !51

; <label>:676:                                    ; preds = %661, %652
  %677 = phi i32 [ undef, %652 ], [ %672, %661 ]
  %678 = phi i64 [ %653, %652 ], [ %673, %661 ]
  %679 = phi i32 [ %641, %652 ], [ %672, %661 ]
  %680 = icmp ult i64 %657, 3
  br i1 %680, label %722, label %681

; <label>:681:                                    ; preds = %676
  br label %682

; <label>:682:                                    ; preds = %682, %681
  %683 = phi i64 [ %678, %681 ], [ %720, %682 ]
  %684 = phi i32 [ %679, %681 ], [ %719, %682 ]
  %685 = getelementptr inbounds i32, i32* %2, i64 %683
  %686 = load i32, i32* %685, align 4, !tbaa !11
  %687 = sext i32 %686 to i64
  %688 = getelementptr inbounds i32, i32* %243, i64 %687
  %689 = load i32, i32* %688, align 4, !tbaa !11
  %690 = icmp slt i32 %689, %625
  %691 = zext i1 %690 to i32
  %692 = add nsw i32 %684, %691
  %693 = add nsw i64 %683, 1
  %694 = getelementptr inbounds i32, i32* %2, i64 %693
  %695 = load i32, i32* %694, align 4, !tbaa !11
  %696 = sext i32 %695 to i64
  %697 = getelementptr inbounds i32, i32* %243, i64 %696
  %698 = load i32, i32* %697, align 4, !tbaa !11
  %699 = icmp slt i32 %698, %625
  %700 = zext i1 %699 to i32
  %701 = add nsw i32 %692, %700
  %702 = add nsw i64 %683, 2
  %703 = getelementptr inbounds i32, i32* %2, i64 %702
  %704 = load i32, i32* %703, align 4, !tbaa !11
  %705 = sext i32 %704 to i64
  %706 = getelementptr inbounds i32, i32* %243, i64 %705
  %707 = load i32, i32* %706, align 4, !tbaa !11
  %708 = icmp slt i32 %707, %625
  %709 = zext i1 %708 to i32
  %710 = add nsw i32 %701, %709
  %711 = add nsw i64 %683, 3
  %712 = getelementptr inbounds i32, i32* %2, i64 %711
  %713 = load i32, i32* %712, align 4, !tbaa !11
  %714 = sext i32 %713 to i64
  %715 = getelementptr inbounds i32, i32* %243, i64 %714
  %716 = load i32, i32* %715, align 4, !tbaa !11
  %717 = icmp slt i32 %716, %625
  %718 = zext i1 %717 to i32
  %719 = add nsw i32 %710, %718
  %720 = add nsw i64 %683, 4
  %721 = icmp eq i64 %720, %654
  br i1 %721, label %722, label %682

; <label>:722:                                    ; preds = %676, %682, %639
  %723 = phi i32 [ %641, %639 ], [ %677, %676 ], [ %719, %682 ]
  %724 = add nsw i64 %640, 1
  %725 = icmp eq i64 %724, %638
  br i1 %725, label %726, label %639

; <label>:726:                                    ; preds = %722, %624
  %727 = phi i32 [ %628, %624 ], [ %723, %722 ]
  %728 = getelementptr inbounds double, double* %20, i64 %626
  store double -1.000000e+00, double* %728, align 8, !tbaa !52
  %729 = icmp eq i64 %629, %623
  br i1 %729, label %730, label %624

; <label>:730:                                    ; preds = %726, %618
  %731 = phi i32 [ 0, %618 ], [ %727, %726 ]
  %732 = phi i32 [ 1, %618 ], [ %634, %726 ]
  %733 = tail call i8* @klu_free(i8* %239, i64 %238, i64 4, %struct.klu_common_struct* %5) #3
  %734 = tail call i8* @klu_free(i8* %242, i64 %241, i64 4, %struct.klu_common_struct* %5) #3
  br i1 %244, label %735, label %946

; <label>:735:                                    ; preds = %730
  %736 = bitcast i32* %251 to i8*
  %737 = add nsw i32 %22, 1
  %738 = sext i32 %737 to i64
  %739 = tail call i8* @klu_free(i8* %736, i64 %738, i64 4, %struct.klu_common_struct* %5) #3
  br label %946

; <label>:740:                                    ; preds = %255, %258
  %741 = call i32 @klu_free_symbolic(%struct.klu_symbolic** nonnull %7, %struct.klu_common_struct* nonnull %5) #3
  store i32 -2, i32* %252, align 4, !tbaa !3
  br label %955

; <label>:742:                                    ; preds = %229
  store i32 0, i32* %18, align 4, !tbaa !11
  %743 = getelementptr inbounds i32, i32* %18, i64 1
  store i32 %0, i32* %743, align 4, !tbaa !11
  store double -1.000000e+00, double* %20, align 8, !tbaa !52
  br i1 %24, label %744, label %946

; <label>:744:                                    ; preds = %742
  %745 = icmp eq i32* %3, null
  %746 = zext i32 %0 to i64
  %747 = icmp ult i32 %0, 8
  br i1 %745, label %748, label %820

; <label>:748:                                    ; preds = %744
  br i1 %747, label %812, label %749

; <label>:749:                                    ; preds = %748
  %750 = and i64 %746, 4294967288
  %751 = add nsw i64 %750, -8
  %752 = lshr exact i64 %751, 3
  %753 = add nuw nsw i64 %752, 1
  %754 = and i64 %753, 3
  %755 = icmp ult i64 %751, 24
  br i1 %755, label %792, label %756

; <label>:756:                                    ; preds = %749
  %757 = sub nsw i64 %753, %754
  br label %758

; <label>:758:                                    ; preds = %758, %756
  %759 = phi i64 [ 0, %756 ], [ %788, %758 ]
  %760 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %756 ], [ %789, %758 ]
  %761 = phi i64 [ %757, %756 ], [ %790, %758 ]
  %762 = getelementptr inbounds i32, i32* %13, i64 %759
  %763 = add <4 x i32> %760, <i32 4, i32 4, i32 4, i32 4>
  %764 = bitcast i32* %762 to <4 x i32>*
  store <4 x i32> %760, <4 x i32>* %764, align 4, !tbaa !11
  %765 = getelementptr i32, i32* %762, i64 4
  %766 = bitcast i32* %765 to <4 x i32>*
  store <4 x i32> %763, <4 x i32>* %766, align 4, !tbaa !11
  %767 = or i64 %759, 8
  %768 = add <4 x i32> %760, <i32 8, i32 8, i32 8, i32 8>
  %769 = getelementptr inbounds i32, i32* %13, i64 %767
  %770 = add <4 x i32> %760, <i32 12, i32 12, i32 12, i32 12>
  %771 = bitcast i32* %769 to <4 x i32>*
  store <4 x i32> %768, <4 x i32>* %771, align 4, !tbaa !11
  %772 = getelementptr i32, i32* %769, i64 4
  %773 = bitcast i32* %772 to <4 x i32>*
  store <4 x i32> %770, <4 x i32>* %773, align 4, !tbaa !11
  %774 = or i64 %759, 16
  %775 = add <4 x i32> %760, <i32 16, i32 16, i32 16, i32 16>
  %776 = getelementptr inbounds i32, i32* %13, i64 %774
  %777 = add <4 x i32> %760, <i32 20, i32 20, i32 20, i32 20>
  %778 = bitcast i32* %776 to <4 x i32>*
  store <4 x i32> %775, <4 x i32>* %778, align 4, !tbaa !11
  %779 = getelementptr i32, i32* %776, i64 4
  %780 = bitcast i32* %779 to <4 x i32>*
  store <4 x i32> %777, <4 x i32>* %780, align 4, !tbaa !11
  %781 = or i64 %759, 24
  %782 = add <4 x i32> %760, <i32 24, i32 24, i32 24, i32 24>
  %783 = getelementptr inbounds i32, i32* %13, i64 %781
  %784 = add <4 x i32> %760, <i32 28, i32 28, i32 28, i32 28>
  %785 = bitcast i32* %783 to <4 x i32>*
  store <4 x i32> %782, <4 x i32>* %785, align 4, !tbaa !11
  %786 = getelementptr i32, i32* %783, i64 4
  %787 = bitcast i32* %786 to <4 x i32>*
  store <4 x i32> %784, <4 x i32>* %787, align 4, !tbaa !11
  %788 = add i64 %759, 32
  %789 = add <4 x i32> %760, <i32 32, i32 32, i32 32, i32 32>
  %790 = add i64 %761, -4
  %791 = icmp eq i64 %790, 0
  br i1 %791, label %792, label %758, !llvm.loop !53

; <label>:792:                                    ; preds = %758, %749
  %793 = phi i64 [ 0, %749 ], [ %788, %758 ]
  %794 = phi <4 x i32> [ <i32 0, i32 1, i32 2, i32 3>, %749 ], [ %789, %758 ]
  %795 = icmp eq i64 %754, 0
  br i1 %795, label %810, label %796

; <label>:796:                                    ; preds = %792
  br label %797

; <label>:797:                                    ; preds = %797, %796
  %798 = phi i64 [ %793, %796 ], [ %806, %797 ]
  %799 = phi <4 x i32> [ %794, %796 ], [ %807, %797 ]
  %800 = phi i64 [ %754, %796 ], [ %808, %797 ]
  %801 = getelementptr inbounds i32, i32* %13, i64 %798
  %802 = add <4 x i32> %799, <i32 4, i32 4, i32 4, i32 4>
  %803 = bitcast i32* %801 to <4 x i32>*
  store <4 x i32> %799, <4 x i32>* %803, align 4, !tbaa !11
  %804 = getelementptr i32, i32* %801, i64 4
  %805 = bitcast i32* %804 to <4 x i32>*
  store <4 x i32> %802, <4 x i32>* %805, align 4, !tbaa !11
  %806 = add i64 %798, 8
  %807 = add <4 x i32> %799, <i32 8, i32 8, i32 8, i32 8>
  %808 = add i64 %800, -1
  %809 = icmp eq i64 %808, 0
  br i1 %809, label %810, label %797, !llvm.loop !54

; <label>:810:                                    ; preds = %797, %792
  %811 = icmp eq i64 %750, %746
  br i1 %811, label %946, label %812

; <label>:812:                                    ; preds = %810, %748
  %813 = phi i64 [ 0, %748 ], [ %750, %810 ]
  br label %814

; <label>:814:                                    ; preds = %812, %814
  %815 = phi i64 [ %818, %814 ], [ %813, %812 ]
  %816 = getelementptr inbounds i32, i32* %13, i64 %815
  %817 = trunc i64 %815 to i32
  store i32 %817, i32* %816, align 4, !tbaa !11
  %818 = add nuw nsw i64 %815, 1
  %819 = icmp eq i64 %818, %746
  br i1 %819, label %946, label %814, !llvm.loop !55

; <label>:820:                                    ; preds = %744
  br i1 %747, label %907, label %821

; <label>:821:                                    ; preds = %820
  %822 = getelementptr i32, i32* %13, i64 %746
  %823 = getelementptr i32, i32* %3, i64 %746
  %824 = icmp ult i32* %13, %823
  %825 = icmp ugt i32* %822, %3
  %826 = and i1 %824, %825
  br i1 %826, label %907, label %827

; <label>:827:                                    ; preds = %821
  %828 = and i64 %746, 4294967288
  %829 = add nsw i64 %828, -8
  %830 = lshr exact i64 %829, 3
  %831 = add nuw nsw i64 %830, 1
  %832 = and i64 %831, 3
  %833 = icmp ult i64 %829, 24
  br i1 %833, label %885, label %834

; <label>:834:                                    ; preds = %827
  %835 = sub nsw i64 %831, %832
  br label %836

; <label>:836:                                    ; preds = %836, %834
  %837 = phi i64 [ 0, %834 ], [ %882, %836 ]
  %838 = phi i64 [ %835, %834 ], [ %883, %836 ]
  %839 = getelementptr inbounds i32, i32* %3, i64 %837
  %840 = bitcast i32* %839 to <4 x i32>*
  %841 = load <4 x i32>, <4 x i32>* %840, align 4, !tbaa !11, !alias.scope !56
  %842 = getelementptr i32, i32* %839, i64 4
  %843 = bitcast i32* %842 to <4 x i32>*
  %844 = load <4 x i32>, <4 x i32>* %843, align 4, !tbaa !11, !alias.scope !56
  %845 = getelementptr inbounds i32, i32* %13, i64 %837
  %846 = bitcast i32* %845 to <4 x i32>*
  store <4 x i32> %841, <4 x i32>* %846, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %847 = getelementptr i32, i32* %845, i64 4
  %848 = bitcast i32* %847 to <4 x i32>*
  store <4 x i32> %844, <4 x i32>* %848, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %849 = or i64 %837, 8
  %850 = getelementptr inbounds i32, i32* %3, i64 %849
  %851 = bitcast i32* %850 to <4 x i32>*
  %852 = load <4 x i32>, <4 x i32>* %851, align 4, !tbaa !11, !alias.scope !56
  %853 = getelementptr i32, i32* %850, i64 4
  %854 = bitcast i32* %853 to <4 x i32>*
  %855 = load <4 x i32>, <4 x i32>* %854, align 4, !tbaa !11, !alias.scope !56
  %856 = getelementptr inbounds i32, i32* %13, i64 %849
  %857 = bitcast i32* %856 to <4 x i32>*
  store <4 x i32> %852, <4 x i32>* %857, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %858 = getelementptr i32, i32* %856, i64 4
  %859 = bitcast i32* %858 to <4 x i32>*
  store <4 x i32> %855, <4 x i32>* %859, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %860 = or i64 %837, 16
  %861 = getelementptr inbounds i32, i32* %3, i64 %860
  %862 = bitcast i32* %861 to <4 x i32>*
  %863 = load <4 x i32>, <4 x i32>* %862, align 4, !tbaa !11, !alias.scope !56
  %864 = getelementptr i32, i32* %861, i64 4
  %865 = bitcast i32* %864 to <4 x i32>*
  %866 = load <4 x i32>, <4 x i32>* %865, align 4, !tbaa !11, !alias.scope !56
  %867 = getelementptr inbounds i32, i32* %13, i64 %860
  %868 = bitcast i32* %867 to <4 x i32>*
  store <4 x i32> %863, <4 x i32>* %868, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %869 = getelementptr i32, i32* %867, i64 4
  %870 = bitcast i32* %869 to <4 x i32>*
  store <4 x i32> %866, <4 x i32>* %870, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %871 = or i64 %837, 24
  %872 = getelementptr inbounds i32, i32* %3, i64 %871
  %873 = bitcast i32* %872 to <4 x i32>*
  %874 = load <4 x i32>, <4 x i32>* %873, align 4, !tbaa !11, !alias.scope !56
  %875 = getelementptr i32, i32* %872, i64 4
  %876 = bitcast i32* %875 to <4 x i32>*
  %877 = load <4 x i32>, <4 x i32>* %876, align 4, !tbaa !11, !alias.scope !56
  %878 = getelementptr inbounds i32, i32* %13, i64 %871
  %879 = bitcast i32* %878 to <4 x i32>*
  store <4 x i32> %874, <4 x i32>* %879, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %880 = getelementptr i32, i32* %878, i64 4
  %881 = bitcast i32* %880 to <4 x i32>*
  store <4 x i32> %877, <4 x i32>* %881, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %882 = add i64 %837, 32
  %883 = add i64 %838, -4
  %884 = icmp eq i64 %883, 0
  br i1 %884, label %885, label %836, !llvm.loop !61

; <label>:885:                                    ; preds = %836, %827
  %886 = phi i64 [ 0, %827 ], [ %882, %836 ]
  %887 = icmp eq i64 %832, 0
  br i1 %887, label %905, label %888

; <label>:888:                                    ; preds = %885
  br label %889

; <label>:889:                                    ; preds = %889, %888
  %890 = phi i64 [ %886, %888 ], [ %902, %889 ]
  %891 = phi i64 [ %832, %888 ], [ %903, %889 ]
  %892 = getelementptr inbounds i32, i32* %3, i64 %890
  %893 = bitcast i32* %892 to <4 x i32>*
  %894 = load <4 x i32>, <4 x i32>* %893, align 4, !tbaa !11, !alias.scope !56
  %895 = getelementptr i32, i32* %892, i64 4
  %896 = bitcast i32* %895 to <4 x i32>*
  %897 = load <4 x i32>, <4 x i32>* %896, align 4, !tbaa !11, !alias.scope !56
  %898 = getelementptr inbounds i32, i32* %13, i64 %890
  %899 = bitcast i32* %898 to <4 x i32>*
  store <4 x i32> %894, <4 x i32>* %899, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %900 = getelementptr i32, i32* %898, i64 4
  %901 = bitcast i32* %900 to <4 x i32>*
  store <4 x i32> %897, <4 x i32>* %901, align 4, !tbaa !11, !alias.scope !59, !noalias !56
  %902 = add i64 %890, 8
  %903 = add i64 %891, -1
  %904 = icmp eq i64 %903, 0
  br i1 %904, label %905, label %889, !llvm.loop !62

; <label>:905:                                    ; preds = %889, %885
  %906 = icmp eq i64 %828, %746
  br i1 %906, label %946, label %907

; <label>:907:                                    ; preds = %905, %821, %820
  %908 = phi i64 [ 0, %821 ], [ 0, %820 ], [ %828, %905 ]
  %909 = add nsw i64 %746, -1
  %910 = sub nsw i64 %909, %908
  %911 = and i64 %746, 3
  %912 = icmp eq i64 %911, 0
  br i1 %912, label %923, label %913

; <label>:913:                                    ; preds = %907
  br label %914

; <label>:914:                                    ; preds = %914, %913
  %915 = phi i64 [ %920, %914 ], [ %908, %913 ]
  %916 = phi i64 [ %921, %914 ], [ %911, %913 ]
  %917 = getelementptr inbounds i32, i32* %3, i64 %915
  %918 = load i32, i32* %917, align 4, !tbaa !11
  %919 = getelementptr inbounds i32, i32* %13, i64 %915
  store i32 %918, i32* %919, align 4, !tbaa !11
  %920 = add nuw nsw i64 %915, 1
  %921 = add i64 %916, -1
  %922 = icmp eq i64 %921, 0
  br i1 %922, label %923, label %914, !llvm.loop !63

; <label>:923:                                    ; preds = %914, %907
  %924 = phi i64 [ %908, %907 ], [ %920, %914 ]
  %925 = icmp ult i64 %910, 3
  br i1 %925, label %946, label %926

; <label>:926:                                    ; preds = %923
  br label %927

; <label>:927:                                    ; preds = %927, %926
  %928 = phi i64 [ %924, %926 ], [ %944, %927 ]
  %929 = getelementptr inbounds i32, i32* %3, i64 %928
  %930 = load i32, i32* %929, align 4, !tbaa !11
  %931 = getelementptr inbounds i32, i32* %13, i64 %928
  store i32 %930, i32* %931, align 4, !tbaa !11
  %932 = add nuw nsw i64 %928, 1
  %933 = getelementptr inbounds i32, i32* %3, i64 %932
  %934 = load i32, i32* %933, align 4, !tbaa !11
  %935 = getelementptr inbounds i32, i32* %13, i64 %932
  store i32 %934, i32* %935, align 4, !tbaa !11
  %936 = add nsw i64 %928, 2
  %937 = getelementptr inbounds i32, i32* %3, i64 %936
  %938 = load i32, i32* %937, align 4, !tbaa !11
  %939 = getelementptr inbounds i32, i32* %13, i64 %936
  store i32 %938, i32* %939, align 4, !tbaa !11
  %940 = add nsw i64 %928, 3
  %941 = getelementptr inbounds i32, i32* %3, i64 %940
  %942 = load i32, i32* %941, align 4, !tbaa !11
  %943 = getelementptr inbounds i32, i32* %13, i64 %940
  store i32 %942, i32* %943, align 4, !tbaa !11
  %944 = add nsw i64 %928, 4
  %945 = icmp eq i64 %944, %746
  br i1 %945, label %946, label %927, !llvm.loop !64

; <label>:946:                                    ; preds = %923, %927, %814, %905, %810, %742, %730, %735
  %947 = phi i32 [ %731, %735 ], [ %731, %730 ], [ 0, %742 ], [ 0, %810 ], [ 0, %905 ], [ 0, %814 ], [ 0, %927 ], [ 0, %923 ]
  %948 = phi i32 [ %732, %735 ], [ %732, %730 ], [ %0, %742 ], [ %0, %810 ], [ %0, %905 ], [ %0, %814 ], [ %0, %927 ], [ %0, %923 ]
  %949 = phi i32 [ %619, %735 ], [ %619, %730 ], [ 1, %742 ], [ 1, %810 ], [ 1, %905 ], [ 1, %814 ], [ 1, %927 ], [ 1, %923 ]
  %950 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 11
  store i32 %949, i32* %950, align 4, !tbaa !65
  %951 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 12
  store i32 %948, i32* %951, align 8, !tbaa !66
  %952 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 2
  %953 = bitcast double* %952 to <2 x double>*
  store <2 x double> <double -1.000000e+00, double -1.000000e+00>, <2 x double>* %953, align 8, !tbaa !52
  %954 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %9, i64 0, i32 10
  store i32 %947, i32* %954, align 8, !tbaa !67
  br label %955

; <label>:955:                                    ; preds = %740, %6, %946
  %956 = phi %struct.klu_symbolic* [ %9, %946 ], [ null, %740 ], [ null, %6 ]
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %8) #3
  ret %struct.klu_symbolic* %956
}

declare i32 @btf_strongcomp(i32, i32*, i32*, i32*, i32*, i32*, i32*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !8, i64 76}
!4 = !{!"klu_common_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !8, i64 40, !8, i64 44, !8, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92, !8, i64 96, !5, i64 104, !5, i64 112, !5, i64 120, !5, i64 128, !5, i64 136, !10, i64 144, !10, i64 152}
!5 = !{!"double", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!"int", !6, i64 0}
!9 = !{!"any pointer", !6, i64 0}
!10 = !{!"long", !6, i64 0}
!11 = !{!8, !8, i64 0}
!12 = !{!9, !9, i64 0}
!13 = !{!14, !8, i64 40}
!14 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!15 = !{!14, !8, i64 44}
!16 = !{!14, !9, i64 48}
!17 = !{!14, !9, i64 56}
!18 = !{!14, !9, i64 64}
!19 = !{!14, !9, i64 32}
!20 = distinct !{!20, !21}
!21 = !{!"llvm.loop.isvectorized", i32 1}
!22 = distinct !{!22, !23}
!23 = !{!"llvm.loop.unroll.disable"}
!24 = distinct !{!24, !25, !21}
!25 = !{!"llvm.loop.unroll.runtime.disable"}
!26 = !{!27}
!27 = distinct !{!27, !28}
!28 = distinct !{!28, !"LVerDomain"}
!29 = !{!30}
!30 = distinct !{!30, !28}
!31 = distinct !{!31, !21}
!32 = distinct !{!32, !23}
!33 = distinct !{!33, !23}
!34 = distinct !{!34, !21}
!35 = !{!4, !8, i64 40}
!36 = !{!14, !8, i64 84}
!37 = !{!14, !8, i64 88}
!38 = distinct !{!38, !23}
!39 = distinct !{!39, !23}
!40 = distinct !{!40, !23}
!41 = !{!42}
!42 = distinct !{!42, !43}
!43 = distinct !{!43, !"LVerDomain"}
!44 = !{!45}
!45 = distinct !{!45, !43}
!46 = distinct !{!46, !21}
!47 = distinct !{!47, !23}
!48 = distinct !{!48, !23}
!49 = distinct !{!49, !21}
!50 = distinct !{!50, !23}
!51 = distinct !{!51, !23}
!52 = !{!5, !5, i64 0}
!53 = distinct !{!53, !21}
!54 = distinct !{!54, !23}
!55 = distinct !{!55, !25, !21}
!56 = !{!57}
!57 = distinct !{!57, !58}
!58 = distinct !{!58, !"LVerDomain"}
!59 = !{!60}
!60 = distinct !{!60, !58}
!61 = distinct !{!61, !21}
!62 = distinct !{!62, !23}
!63 = distinct !{!63, !23}
!64 = distinct !{!64, !21}
!65 = !{!14, !8, i64 76}
!66 = !{!14, !8, i64 80}
!67 = !{!14, !8, i64 72}
