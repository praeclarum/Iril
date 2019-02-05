; ModuleID = 'klu_sort.c'
source_filename = "klu_sort.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_sort(%struct.klu_symbolic* nocapture readonly, %struct.klu_numeric* nocapture readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %4 = icmp eq %struct.klu_common_struct* %2, null
  br i1 %4, label %75, label %5

; <label>:5:                                      ; preds = %3
  %6 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11
  store i32 0, i32* %6, align 4, !tbaa !3
  %7 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 9
  %8 = load i32*, i32** %7, align 8, !tbaa !11
  %9 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 11
  %10 = load i32, i32* %9, align 4, !tbaa !13
  %11 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %0, i64 0, i32 12
  %12 = load i32, i32* %11, align 8, !tbaa !14
  %13 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 8
  %14 = load i32*, i32** %13, align 8, !tbaa !15
  %15 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 10
  %16 = load i32*, i32** %15, align 8, !tbaa !17
  %17 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 9
  %18 = load i32*, i32** %17, align 8, !tbaa !18
  %19 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 11
  %20 = load i32*, i32** %19, align 8, !tbaa !19
  %21 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 12
  %22 = bitcast i8*** %21 to double***
  %23 = load double**, double*** %22, align 8, !tbaa !20
  %24 = sext i32 %12 to i64
  %25 = add nsw i64 %24, 1
  %26 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 4
  %27 = load i32, i32* %26, align 8, !tbaa !21
  %28 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %1, i64 0, i32 5
  %29 = load i32, i32* %28, align 4, !tbaa !22
  %30 = icmp sgt i32 %27, %29
  %31 = select i1 %30, i32 %27, i32 %29
  %32 = tail call i8* @klu_malloc(i64 %24, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %33 = bitcast i8* %32 to i32*
  %34 = tail call i8* @klu_malloc(i64 %25, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %35 = bitcast i8* %34 to i32*
  %36 = sext i32 %31 to i64
  %37 = tail call i8* @klu_malloc(i64 %36, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %38 = bitcast i8* %37 to i32*
  %39 = tail call i8* @klu_malloc(i64 %36, i64 8, %struct.klu_common_struct* nonnull %2) #4
  %40 = bitcast i8* %39 to double*
  %41 = load i32, i32* %6, align 4, !tbaa !3
  %42 = icmp eq i32 %41, 0
  %43 = icmp sgt i32 %10, 0
  %44 = and i1 %42, %43
  br i1 %44, label %45, label %67

; <label>:45:                                     ; preds = %5
  %46 = zext i32 %10 to i64
  br label %47

; <label>:47:                                     ; preds = %65, %45
  %48 = phi i64 [ 0, %45 ], [ %51, %65 ]
  %49 = getelementptr inbounds i32, i32* %8, i64 %48
  %50 = load i32, i32* %49, align 4, !tbaa !23
  %51 = add nuw nsw i64 %48, 1
  %52 = getelementptr inbounds i32, i32* %8, i64 %51
  %53 = load i32, i32* %52, align 4, !tbaa !23
  %54 = sub nsw i32 %53, %50
  %55 = icmp sgt i32 %54, 1
  br i1 %55, label %56, label %65

; <label>:56:                                     ; preds = %47
  %57 = sext i32 %50 to i64
  %58 = getelementptr inbounds i32, i32* %14, i64 %57
  %59 = getelementptr inbounds i32, i32* %16, i64 %57
  %60 = getelementptr inbounds double*, double** %23, i64 %48
  %61 = load double*, double** %60, align 8, !tbaa !24
  tail call fastcc void @sort(i32 %54, i32* %58, i32* %59, double* %61, i32* %35, i32* %38, double* %40, i32* %33)
  %62 = getelementptr inbounds i32, i32* %18, i64 %57
  %63 = getelementptr inbounds i32, i32* %20, i64 %57
  %64 = load double*, double** %60, align 8, !tbaa !24
  tail call fastcc void @sort(i32 %54, i32* %62, i32* %63, double* %64, i32* %35, i32* %38, double* %40, i32* %33)
  br label %65

; <label>:65:                                     ; preds = %47, %56
  %66 = icmp eq i64 %51, %46
  br i1 %66, label %67, label %47

; <label>:67:                                     ; preds = %65, %5
  %68 = tail call i8* @klu_free(i8* %32, i64 %24, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %69 = tail call i8* @klu_free(i8* %34, i64 %25, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %70 = tail call i8* @klu_free(i8* %37, i64 %36, i64 4, %struct.klu_common_struct* nonnull %2) #4
  %71 = tail call i8* @klu_free(i8* %39, i64 %36, i64 8, %struct.klu_common_struct* nonnull %2) #4
  %72 = load i32, i32* %6, align 4, !tbaa !3
  %73 = icmp eq i32 %72, 0
  %74 = zext i1 %73 to i32
  br label %75

; <label>:75:                                     ; preds = %3, %67
  %76 = phi i32 [ %74, %67 ], [ 0, %3 ]
  ret i32 %76
}

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: norecurse nounwind ssp uwtable
define internal fastcc void @sort(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture, i32* nocapture, i32* nocapture, double* nocapture, i32* nocapture) unnamed_addr #2 {
  %9 = bitcast i32* %7 to i8*
  %10 = icmp sgt i32 %0, 0
  br i1 %10, label %11, label %117

; <label>:11:                                     ; preds = %8
  %12 = zext i32 %0 to i64
  %13 = shl nuw nsw i64 %12, 2
  call void @llvm.memset.p0i8.i64(i8* %9, i8 0, i64 %13, i32 4, i1 false)
  %14 = zext i32 %0 to i64
  br label %15

; <label>:15:                                     ; preds = %81, %11
  %16 = phi i64 [ 0, %11 ], [ %82, %81 ]
  %17 = getelementptr inbounds i32, i32* %1, i64 %16
  %18 = load i32, i32* %17, align 4, !tbaa !23
  %19 = sext i32 %18 to i64
  %20 = getelementptr inbounds double, double* %3, i64 %19
  %21 = getelementptr inbounds i32, i32* %2, i64 %16
  %22 = load i32, i32* %21, align 4, !tbaa !23
  %23 = bitcast double* %20 to i32*
  %24 = icmp sgt i32 %22, 0
  br i1 %24, label %25, label %81

; <label>:25:                                     ; preds = %15
  %26 = zext i32 %22 to i64
  %27 = add nsw i64 %26, -1
  %28 = and i64 %26, 3
  %29 = icmp ult i64 %27, 3
  br i1 %29, label %65, label %30

; <label>:30:                                     ; preds = %25
  %31 = sub nsw i64 %26, %28
  br label %32

; <label>:32:                                     ; preds = %32, %30
  %33 = phi i64 [ 0, %30 ], [ %62, %32 ]
  %34 = phi i64 [ %31, %30 ], [ %63, %32 ]
  %35 = getelementptr inbounds i32, i32* %23, i64 %33
  %36 = load i32, i32* %35, align 4, !tbaa !23
  %37 = sext i32 %36 to i64
  %38 = getelementptr inbounds i32, i32* %7, i64 %37
  %39 = load i32, i32* %38, align 4, !tbaa !23
  %40 = add nsw i32 %39, 1
  store i32 %40, i32* %38, align 4, !tbaa !23
  %41 = or i64 %33, 1
  %42 = getelementptr inbounds i32, i32* %23, i64 %41
  %43 = load i32, i32* %42, align 4, !tbaa !23
  %44 = sext i32 %43 to i64
  %45 = getelementptr inbounds i32, i32* %7, i64 %44
  %46 = load i32, i32* %45, align 4, !tbaa !23
  %47 = add nsw i32 %46, 1
  store i32 %47, i32* %45, align 4, !tbaa !23
  %48 = or i64 %33, 2
  %49 = getelementptr inbounds i32, i32* %23, i64 %48
  %50 = load i32, i32* %49, align 4, !tbaa !23
  %51 = sext i32 %50 to i64
  %52 = getelementptr inbounds i32, i32* %7, i64 %51
  %53 = load i32, i32* %52, align 4, !tbaa !23
  %54 = add nsw i32 %53, 1
  store i32 %54, i32* %52, align 4, !tbaa !23
  %55 = or i64 %33, 3
  %56 = getelementptr inbounds i32, i32* %23, i64 %55
  %57 = load i32, i32* %56, align 4, !tbaa !23
  %58 = sext i32 %57 to i64
  %59 = getelementptr inbounds i32, i32* %7, i64 %58
  %60 = load i32, i32* %59, align 4, !tbaa !23
  %61 = add nsw i32 %60, 1
  store i32 %61, i32* %59, align 4, !tbaa !23
  %62 = add nuw nsw i64 %33, 4
  %63 = add i64 %34, -4
  %64 = icmp eq i64 %63, 0
  br i1 %64, label %65, label %32

; <label>:65:                                     ; preds = %32, %25
  %66 = phi i64 [ 0, %25 ], [ %62, %32 ]
  %67 = icmp eq i64 %28, 0
  br i1 %67, label %81, label %68

; <label>:68:                                     ; preds = %65
  br label %69

; <label>:69:                                     ; preds = %69, %68
  %70 = phi i64 [ %66, %68 ], [ %78, %69 ]
  %71 = phi i64 [ %28, %68 ], [ %79, %69 ]
  %72 = getelementptr inbounds i32, i32* %23, i64 %70
  %73 = load i32, i32* %72, align 4, !tbaa !23
  %74 = sext i32 %73 to i64
  %75 = getelementptr inbounds i32, i32* %7, i64 %74
  %76 = load i32, i32* %75, align 4, !tbaa !23
  %77 = add nsw i32 %76, 1
  store i32 %77, i32* %75, align 4, !tbaa !23
  %78 = add nuw nsw i64 %70, 1
  %79 = add i64 %71, -1
  %80 = icmp eq i64 %79, 0
  br i1 %80, label %81, label %69, !llvm.loop !25

; <label>:81:                                     ; preds = %65, %69, %15
  %82 = add nuw nsw i64 %16, 1
  %83 = icmp eq i64 %82, %14
  br i1 %83, label %84, label %15

; <label>:84:                                     ; preds = %81
  br i1 %10, label %85, label %117

; <label>:85:                                     ; preds = %84
  %86 = add nsw i64 %14, -1
  %87 = and i64 %14, 3
  %88 = icmp ult i64 %86, 3
  br i1 %88, label %120, label %89

; <label>:89:                                     ; preds = %85
  %90 = sub nsw i64 %14, %87
  br label %91

; <label>:91:                                     ; preds = %91, %89
  %92 = phi i64 [ 0, %89 ], [ %114, %91 ]
  %93 = phi i32 [ 0, %89 ], [ %113, %91 ]
  %94 = phi i64 [ %90, %89 ], [ %115, %91 ]
  %95 = getelementptr inbounds i32, i32* %4, i64 %92
  store i32 %93, i32* %95, align 4, !tbaa !23
  %96 = getelementptr inbounds i32, i32* %7, i64 %92
  %97 = load i32, i32* %96, align 4, !tbaa !23
  %98 = add nsw i32 %97, %93
  %99 = or i64 %92, 1
  %100 = getelementptr inbounds i32, i32* %4, i64 %99
  store i32 %98, i32* %100, align 4, !tbaa !23
  %101 = getelementptr inbounds i32, i32* %7, i64 %99
  %102 = load i32, i32* %101, align 4, !tbaa !23
  %103 = add nsw i32 %102, %98
  %104 = or i64 %92, 2
  %105 = getelementptr inbounds i32, i32* %4, i64 %104
  store i32 %103, i32* %105, align 4, !tbaa !23
  %106 = getelementptr inbounds i32, i32* %7, i64 %104
  %107 = load i32, i32* %106, align 4, !tbaa !23
  %108 = add nsw i32 %107, %103
  %109 = or i64 %92, 3
  %110 = getelementptr inbounds i32, i32* %4, i64 %109
  store i32 %108, i32* %110, align 4, !tbaa !23
  %111 = getelementptr inbounds i32, i32* %7, i64 %109
  %112 = load i32, i32* %111, align 4, !tbaa !23
  %113 = add nsw i32 %112, %108
  %114 = add nuw nsw i64 %92, 4
  %115 = add i64 %94, -4
  %116 = icmp eq i64 %115, 0
  br i1 %116, label %120, label %91

; <label>:117:                                    ; preds = %84, %8
  %118 = sext i32 %0 to i64
  %119 = getelementptr inbounds i32, i32* %4, i64 %118
  store i32 0, i32* %119, align 4, !tbaa !23
  br label %395

; <label>:120:                                    ; preds = %91, %85
  %121 = phi i32 [ undef, %85 ], [ %113, %91 ]
  %122 = phi i64 [ 0, %85 ], [ %114, %91 ]
  %123 = phi i32 [ 0, %85 ], [ %113, %91 ]
  %124 = icmp eq i64 %87, 0
  br i1 %124, label %137, label %125

; <label>:125:                                    ; preds = %120
  br label %126

; <label>:126:                                    ; preds = %126, %125
  %127 = phi i64 [ %122, %125 ], [ %134, %126 ]
  %128 = phi i32 [ %123, %125 ], [ %133, %126 ]
  %129 = phi i64 [ %87, %125 ], [ %135, %126 ]
  %130 = getelementptr inbounds i32, i32* %4, i64 %127
  store i32 %128, i32* %130, align 4, !tbaa !23
  %131 = getelementptr inbounds i32, i32* %7, i64 %127
  %132 = load i32, i32* %131, align 4, !tbaa !23
  %133 = add nsw i32 %132, %128
  %134 = add nuw nsw i64 %127, 1
  %135 = add i64 %129, -1
  %136 = icmp eq i64 %135, 0
  br i1 %136, label %137, label %126, !llvm.loop !27

; <label>:137:                                    ; preds = %126, %120
  %138 = phi i32 [ %121, %120 ], [ %133, %126 ]
  %139 = sext i32 %0 to i64
  %140 = getelementptr inbounds i32, i32* %4, i64 %139
  store i32 %138, i32* %140, align 4, !tbaa !23
  br i1 %10, label %141, label %395

; <label>:141:                                    ; preds = %137
  %142 = zext i32 %0 to i64
  %143 = icmp ult i32 %0, 8
  br i1 %143, label %230, label %144

; <label>:144:                                    ; preds = %141
  %145 = getelementptr i32, i32* %7, i64 %14
  %146 = getelementptr i32, i32* %4, i64 %14
  %147 = icmp ugt i32* %146, %7
  %148 = icmp ugt i32* %145, %4
  %149 = and i1 %147, %148
  br i1 %149, label %230, label %150

; <label>:150:                                    ; preds = %144
  %151 = and i64 %14, 4294967288
  %152 = add nsw i64 %151, -8
  %153 = lshr exact i64 %152, 3
  %154 = add nuw nsw i64 %153, 1
  %155 = and i64 %154, 3
  %156 = icmp ult i64 %152, 24
  br i1 %156, label %208, label %157

; <label>:157:                                    ; preds = %150
  %158 = sub nsw i64 %154, %155
  br label %159

; <label>:159:                                    ; preds = %159, %157
  %160 = phi i64 [ 0, %157 ], [ %205, %159 ]
  %161 = phi i64 [ %158, %157 ], [ %206, %159 ]
  %162 = getelementptr inbounds i32, i32* %4, i64 %160
  %163 = bitcast i32* %162 to <4 x i32>*
  %164 = load <4 x i32>, <4 x i32>* %163, align 4, !tbaa !23, !alias.scope !28
  %165 = getelementptr i32, i32* %162, i64 4
  %166 = bitcast i32* %165 to <4 x i32>*
  %167 = load <4 x i32>, <4 x i32>* %166, align 4, !tbaa !23, !alias.scope !28
  %168 = getelementptr inbounds i32, i32* %7, i64 %160
  %169 = bitcast i32* %168 to <4 x i32>*
  store <4 x i32> %164, <4 x i32>* %169, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %170 = getelementptr i32, i32* %168, i64 4
  %171 = bitcast i32* %170 to <4 x i32>*
  store <4 x i32> %167, <4 x i32>* %171, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %172 = or i64 %160, 8
  %173 = getelementptr inbounds i32, i32* %4, i64 %172
  %174 = bitcast i32* %173 to <4 x i32>*
  %175 = load <4 x i32>, <4 x i32>* %174, align 4, !tbaa !23, !alias.scope !28
  %176 = getelementptr i32, i32* %173, i64 4
  %177 = bitcast i32* %176 to <4 x i32>*
  %178 = load <4 x i32>, <4 x i32>* %177, align 4, !tbaa !23, !alias.scope !28
  %179 = getelementptr inbounds i32, i32* %7, i64 %172
  %180 = bitcast i32* %179 to <4 x i32>*
  store <4 x i32> %175, <4 x i32>* %180, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %181 = getelementptr i32, i32* %179, i64 4
  %182 = bitcast i32* %181 to <4 x i32>*
  store <4 x i32> %178, <4 x i32>* %182, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %183 = or i64 %160, 16
  %184 = getelementptr inbounds i32, i32* %4, i64 %183
  %185 = bitcast i32* %184 to <4 x i32>*
  %186 = load <4 x i32>, <4 x i32>* %185, align 4, !tbaa !23, !alias.scope !28
  %187 = getelementptr i32, i32* %184, i64 4
  %188 = bitcast i32* %187 to <4 x i32>*
  %189 = load <4 x i32>, <4 x i32>* %188, align 4, !tbaa !23, !alias.scope !28
  %190 = getelementptr inbounds i32, i32* %7, i64 %183
  %191 = bitcast i32* %190 to <4 x i32>*
  store <4 x i32> %186, <4 x i32>* %191, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %192 = getelementptr i32, i32* %190, i64 4
  %193 = bitcast i32* %192 to <4 x i32>*
  store <4 x i32> %189, <4 x i32>* %193, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %194 = or i64 %160, 24
  %195 = getelementptr inbounds i32, i32* %4, i64 %194
  %196 = bitcast i32* %195 to <4 x i32>*
  %197 = load <4 x i32>, <4 x i32>* %196, align 4, !tbaa !23, !alias.scope !28
  %198 = getelementptr i32, i32* %195, i64 4
  %199 = bitcast i32* %198 to <4 x i32>*
  %200 = load <4 x i32>, <4 x i32>* %199, align 4, !tbaa !23, !alias.scope !28
  %201 = getelementptr inbounds i32, i32* %7, i64 %194
  %202 = bitcast i32* %201 to <4 x i32>*
  store <4 x i32> %197, <4 x i32>* %202, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %203 = getelementptr i32, i32* %201, i64 4
  %204 = bitcast i32* %203 to <4 x i32>*
  store <4 x i32> %200, <4 x i32>* %204, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %205 = add i64 %160, 32
  %206 = add i64 %161, -4
  %207 = icmp eq i64 %206, 0
  br i1 %207, label %208, label %159, !llvm.loop !33

; <label>:208:                                    ; preds = %159, %150
  %209 = phi i64 [ 0, %150 ], [ %205, %159 ]
  %210 = icmp eq i64 %155, 0
  br i1 %210, label %228, label %211

; <label>:211:                                    ; preds = %208
  br label %212

; <label>:212:                                    ; preds = %212, %211
  %213 = phi i64 [ %209, %211 ], [ %225, %212 ]
  %214 = phi i64 [ %155, %211 ], [ %226, %212 ]
  %215 = getelementptr inbounds i32, i32* %4, i64 %213
  %216 = bitcast i32* %215 to <4 x i32>*
  %217 = load <4 x i32>, <4 x i32>* %216, align 4, !tbaa !23, !alias.scope !28
  %218 = getelementptr i32, i32* %215, i64 4
  %219 = bitcast i32* %218 to <4 x i32>*
  %220 = load <4 x i32>, <4 x i32>* %219, align 4, !tbaa !23, !alias.scope !28
  %221 = getelementptr inbounds i32, i32* %7, i64 %213
  %222 = bitcast i32* %221 to <4 x i32>*
  store <4 x i32> %217, <4 x i32>* %222, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %223 = getelementptr i32, i32* %221, i64 4
  %224 = bitcast i32* %223 to <4 x i32>*
  store <4 x i32> %220, <4 x i32>* %224, align 4, !tbaa !23, !alias.scope !31, !noalias !28
  %225 = add i64 %213, 8
  %226 = add i64 %214, -1
  %227 = icmp eq i64 %226, 0
  br i1 %227, label %228, label %212, !llvm.loop !35

; <label>:228:                                    ; preds = %212, %208
  %229 = icmp eq i64 %151, %14
  br i1 %229, label %269, label %230

; <label>:230:                                    ; preds = %228, %144, %141
  %231 = phi i64 [ 0, %144 ], [ 0, %141 ], [ %151, %228 ]
  %232 = add nsw i64 %142, -1
  %233 = sub nsw i64 %232, %231
  %234 = and i64 %142, 3
  %235 = icmp eq i64 %234, 0
  br i1 %235, label %246, label %236

; <label>:236:                                    ; preds = %230
  br label %237

; <label>:237:                                    ; preds = %237, %236
  %238 = phi i64 [ %243, %237 ], [ %231, %236 ]
  %239 = phi i64 [ %244, %237 ], [ %234, %236 ]
  %240 = getelementptr inbounds i32, i32* %4, i64 %238
  %241 = load i32, i32* %240, align 4, !tbaa !23
  %242 = getelementptr inbounds i32, i32* %7, i64 %238
  store i32 %241, i32* %242, align 4, !tbaa !23
  %243 = add nuw nsw i64 %238, 1
  %244 = add i64 %239, -1
  %245 = icmp eq i64 %244, 0
  br i1 %245, label %246, label %237, !llvm.loop !36

; <label>:246:                                    ; preds = %237, %230
  %247 = phi i64 [ %231, %230 ], [ %243, %237 ]
  %248 = icmp ult i64 %233, 3
  br i1 %248, label %269, label %249

; <label>:249:                                    ; preds = %246
  br label %250

; <label>:250:                                    ; preds = %250, %249
  %251 = phi i64 [ %247, %249 ], [ %267, %250 ]
  %252 = getelementptr inbounds i32, i32* %4, i64 %251
  %253 = load i32, i32* %252, align 4, !tbaa !23
  %254 = getelementptr inbounds i32, i32* %7, i64 %251
  store i32 %253, i32* %254, align 4, !tbaa !23
  %255 = add nuw nsw i64 %251, 1
  %256 = getelementptr inbounds i32, i32* %4, i64 %255
  %257 = load i32, i32* %256, align 4, !tbaa !23
  %258 = getelementptr inbounds i32, i32* %7, i64 %255
  store i32 %257, i32* %258, align 4, !tbaa !23
  %259 = add nsw i64 %251, 2
  %260 = getelementptr inbounds i32, i32* %4, i64 %259
  %261 = load i32, i32* %260, align 4, !tbaa !23
  %262 = getelementptr inbounds i32, i32* %7, i64 %259
  store i32 %261, i32* %262, align 4, !tbaa !23
  %263 = add nsw i64 %251, 3
  %264 = getelementptr inbounds i32, i32* %4, i64 %263
  %265 = load i32, i32* %264, align 4, !tbaa !23
  %266 = getelementptr inbounds i32, i32* %7, i64 %263
  store i32 %265, i32* %266, align 4, !tbaa !23
  %267 = add nsw i64 %251, 4
  %268 = icmp eq i64 %267, %142
  br i1 %268, label %269, label %250, !llvm.loop !37

; <label>:269:                                    ; preds = %246, %250, %228
  br i1 %10, label %270, label %395

; <label>:270:                                    ; preds = %269
  %271 = zext i32 %0 to i64
  br label %272

; <label>:272:                                    ; preds = %344, %270
  %273 = phi i64 [ 0, %270 ], [ %345, %344 ]
  %274 = getelementptr inbounds i32, i32* %1, i64 %273
  %275 = load i32, i32* %274, align 4, !tbaa !23
  %276 = sext i32 %275 to i64
  %277 = getelementptr inbounds double, double* %3, i64 %276
  %278 = getelementptr inbounds i32, i32* %2, i64 %273
  %279 = load i32, i32* %278, align 4, !tbaa !23
  %280 = bitcast double* %277 to i32*
  %281 = sext i32 %279 to i64
  %282 = shl nsw i64 %281, 2
  %283 = add nsw i64 %282, 7
  %284 = lshr i64 %283, 3
  %285 = getelementptr inbounds double, double* %277, i64 %284
  %286 = icmp sgt i32 %279, 0
  br i1 %286, label %287, label %344

; <label>:287:                                    ; preds = %272
  %288 = trunc i64 %273 to i32
  %289 = zext i32 %279 to i64
  %290 = and i64 %289, 1
  %291 = icmp eq i32 %279, 1
  br i1 %291, label %327, label %292

; <label>:292:                                    ; preds = %287
  %293 = sub nsw i64 %289, %290
  br label %294

; <label>:294:                                    ; preds = %294, %292
  %295 = phi i64 [ 0, %292 ], [ %324, %294 ]
  %296 = phi i64 [ %293, %292 ], [ %325, %294 ]
  %297 = getelementptr inbounds i32, i32* %280, i64 %295
  %298 = load i32, i32* %297, align 4, !tbaa !23
  %299 = sext i32 %298 to i64
  %300 = getelementptr inbounds i32, i32* %7, i64 %299
  %301 = load i32, i32* %300, align 4, !tbaa !23
  %302 = add nsw i32 %301, 1
  store i32 %302, i32* %300, align 4, !tbaa !23
  %303 = sext i32 %301 to i64
  %304 = getelementptr inbounds i32, i32* %5, i64 %303
  store i32 %288, i32* %304, align 4, !tbaa !23
  %305 = getelementptr inbounds double, double* %285, i64 %295
  %306 = bitcast double* %305 to i64*
  %307 = load i64, i64* %306, align 8, !tbaa !38
  %308 = getelementptr inbounds double, double* %6, i64 %303
  %309 = bitcast double* %308 to i64*
  store i64 %307, i64* %309, align 8, !tbaa !38
  %310 = or i64 %295, 1
  %311 = getelementptr inbounds i32, i32* %280, i64 %310
  %312 = load i32, i32* %311, align 4, !tbaa !23
  %313 = sext i32 %312 to i64
  %314 = getelementptr inbounds i32, i32* %7, i64 %313
  %315 = load i32, i32* %314, align 4, !tbaa !23
  %316 = add nsw i32 %315, 1
  store i32 %316, i32* %314, align 4, !tbaa !23
  %317 = sext i32 %315 to i64
  %318 = getelementptr inbounds i32, i32* %5, i64 %317
  store i32 %288, i32* %318, align 4, !tbaa !23
  %319 = getelementptr inbounds double, double* %285, i64 %310
  %320 = bitcast double* %319 to i64*
  %321 = load i64, i64* %320, align 8, !tbaa !38
  %322 = getelementptr inbounds double, double* %6, i64 %317
  %323 = bitcast double* %322 to i64*
  store i64 %321, i64* %323, align 8, !tbaa !38
  %324 = add nuw nsw i64 %295, 2
  %325 = add i64 %296, -2
  %326 = icmp eq i64 %325, 0
  br i1 %326, label %327, label %294

; <label>:327:                                    ; preds = %294, %287
  %328 = phi i64 [ 0, %287 ], [ %324, %294 ]
  %329 = icmp eq i64 %290, 0
  br i1 %329, label %344, label %330

; <label>:330:                                    ; preds = %327
  %331 = getelementptr inbounds i32, i32* %280, i64 %328
  %332 = load i32, i32* %331, align 4, !tbaa !23
  %333 = sext i32 %332 to i64
  %334 = getelementptr inbounds i32, i32* %7, i64 %333
  %335 = load i32, i32* %334, align 4, !tbaa !23
  %336 = add nsw i32 %335, 1
  store i32 %336, i32* %334, align 4, !tbaa !23
  %337 = sext i32 %335 to i64
  %338 = getelementptr inbounds i32, i32* %5, i64 %337
  store i32 %288, i32* %338, align 4, !tbaa !23
  %339 = getelementptr inbounds double, double* %285, i64 %328
  %340 = bitcast double* %339 to i64*
  %341 = load i64, i64* %340, align 8, !tbaa !38
  %342 = getelementptr inbounds double, double* %6, i64 %337
  %343 = bitcast double* %342 to i64*
  store i64 %341, i64* %343, align 8, !tbaa !38
  br label %344

; <label>:344:                                    ; preds = %330, %327, %272
  %345 = add nuw nsw i64 %273, 1
  %346 = icmp eq i64 %345, %271
  br i1 %346, label %347, label %272

; <label>:347:                                    ; preds = %344
  br i1 %10, label %348, label %395

; <label>:348:                                    ; preds = %347
  %349 = zext i32 %0 to i64
  %350 = shl nuw nsw i64 %349, 2
  call void @llvm.memset.p0i8.i64(i8* %9, i8 0, i64 %350, i32 4, i1 false)
  %351 = zext i32 %0 to i64
  br label %352

; <label>:352:                                    ; preds = %393, %348
  %353 = phi i64 [ 0, %348 ], [ %354, %393 ]
  %354 = add nuw nsw i64 %353, 1
  %355 = getelementptr inbounds i32, i32* %4, i64 %354
  %356 = load i32, i32* %355, align 4, !tbaa !23
  %357 = getelementptr inbounds i32, i32* %4, i64 %353
  %358 = load i32, i32* %357, align 4, !tbaa !23
  %359 = icmp slt i32 %358, %356
  br i1 %359, label %360, label %393

; <label>:360:                                    ; preds = %352
  %361 = sext i32 %358 to i64
  %362 = trunc i64 %353 to i32
  %363 = sext i32 %356 to i64
  br label %364

; <label>:364:                                    ; preds = %364, %360
  %365 = phi i64 [ %361, %360 ], [ %391, %364 ]
  %366 = getelementptr inbounds i32, i32* %5, i64 %365
  %367 = load i32, i32* %366, align 4, !tbaa !23
  %368 = sext i32 %367 to i64
  %369 = getelementptr inbounds i32, i32* %1, i64 %368
  %370 = load i32, i32* %369, align 4, !tbaa !23
  %371 = sext i32 %370 to i64
  %372 = getelementptr inbounds double, double* %3, i64 %371
  %373 = getelementptr inbounds i32, i32* %2, i64 %368
  %374 = load i32, i32* %373, align 4, !tbaa !23
  %375 = bitcast double* %372 to i32*
  %376 = sext i32 %374 to i64
  %377 = shl nsw i64 %376, 2
  %378 = add nsw i64 %377, 7
  %379 = lshr i64 %378, 3
  %380 = getelementptr inbounds double, double* %372, i64 %379
  %381 = getelementptr inbounds i32, i32* %7, i64 %368
  %382 = load i32, i32* %381, align 4, !tbaa !23
  %383 = add nsw i32 %382, 1
  store i32 %383, i32* %381, align 4, !tbaa !23
  %384 = sext i32 %382 to i64
  %385 = getelementptr inbounds i32, i32* %375, i64 %384
  store i32 %362, i32* %385, align 4, !tbaa !23
  %386 = getelementptr inbounds double, double* %6, i64 %365
  %387 = bitcast double* %386 to i64*
  %388 = load i64, i64* %387, align 8, !tbaa !38
  %389 = getelementptr inbounds double, double* %380, i64 %384
  %390 = bitcast double* %389 to i64*
  store i64 %388, i64* %390, align 8, !tbaa !38
  %391 = add nsw i64 %365, 1
  %392 = icmp eq i64 %391, %363
  br i1 %392, label %393, label %364

; <label>:393:                                    ; preds = %364, %352
  %394 = icmp eq i64 %354, %351
  br i1 %394, label %395, label %352

; <label>:395:                                    ; preds = %393, %137, %117, %269, %347
  ret void
}

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #3

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { argmemonly nounwind }
attributes #4 = { nounwind }

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
!11 = !{!12, !9, i64 64}
!12 = !{!"", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !9, i64 32, !8, i64 40, !8, i64 44, !9, i64 48, !9, i64 56, !9, i64 64, !8, i64 72, !8, i64 76, !8, i64 80, !8, i64 84, !8, i64 88, !8, i64 92}
!13 = !{!12, !8, i64 76}
!14 = !{!12, !8, i64 80}
!15 = !{!16, !9, i64 40}
!16 = !{!"", !8, i64 0, !8, i64 4, !8, i64 8, !8, i64 12, !8, i64 16, !8, i64 20, !9, i64 24, !9, i64 32, !9, i64 40, !9, i64 48, !9, i64 56, !9, i64 64, !9, i64 72, !9, i64 80, !9, i64 88, !9, i64 96, !10, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !9, i64 144, !9, i64 152, !8, i64 160}
!17 = !{!16, !9, i64 56}
!18 = !{!16, !9, i64 48}
!19 = !{!16, !9, i64 64}
!20 = !{!16, !9, i64 72}
!21 = !{!16, !8, i64 16}
!22 = !{!16, !8, i64 20}
!23 = !{!8, !8, i64 0}
!24 = !{!9, !9, i64 0}
!25 = distinct !{!25, !26}
!26 = !{!"llvm.loop.unroll.disable"}
!27 = distinct !{!27, !26}
!28 = !{!29}
!29 = distinct !{!29, !30}
!30 = distinct !{!30, !"LVerDomain"}
!31 = !{!32}
!32 = distinct !{!32, !30}
!33 = distinct !{!33, !34}
!34 = !{!"llvm.loop.isvectorized", i32 1}
!35 = distinct !{!35, !26}
!36 = distinct !{!36, !26}
!37 = distinct !{!37, !34}
!38 = !{!5, !5, i64 0}
