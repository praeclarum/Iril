; ModuleID = 'klu_factor.c'
source_filename = "klu_factor.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define %struct.klu_numeric* @klu_factor(i32*, i32*, double*, %struct.klu_symbolic* readonly, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %6 = alloca i32, align 4
  %7 = alloca i32, align 4
  %8 = alloca i32, align 4
  %9 = alloca %struct.klu_numeric*, align 8
  %10 = bitcast i32* %8 to i8*
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %10) #3
  store i32 1, i32* %8, align 4, !tbaa !3
  %11 = bitcast %struct.klu_numeric** %9 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %11) #3
  %12 = icmp eq %struct.klu_common_struct* %4, null
  br i1 %12, label %879, label %13

; <label>:13:                                     ; preds = %5
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11
  store i32 0, i32* %14, align 4, !tbaa !7
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 14
  store i32 -1, i32* %15, align 8, !tbaa !12
  %16 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 15
  store i32 -1, i32* %16, align 4, !tbaa !13
  %17 = icmp eq %struct.klu_symbolic* %3, null
  br i1 %17, label %18, label %19

; <label>:18:                                     ; preds = %13
  store i32 -3, i32* %14, align 4, !tbaa !7
  br label %879

; <label>:19:                                     ; preds = %13
  %20 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 5
  %21 = load i32, i32* %20, align 8, !tbaa !14
  %22 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 10
  %23 = load i32, i32* %22, align 8, !tbaa !16
  %24 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 11
  %25 = load i32, i32* %24, align 4, !tbaa !17
  %26 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 12
  %27 = load i32, i32* %26, align 8, !tbaa !18
  %28 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 2
  %29 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 3
  %30 = load double, double* %29, align 8, !tbaa !19
  %31 = fcmp olt double %30, 1.000000e+00
  %32 = select i1 %31, double 1.000000e+00, double %30
  store double %32, double* %29, align 8, !tbaa !19
  %33 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 0
  %34 = load double, double* %33, align 8, !tbaa !20
  %35 = fcmp olt double %34, 1.000000e+00
  %36 = select i1 %35, double %34, double 1.000000e+00
  %37 = fcmp olt double %36, 0.000000e+00
  %38 = select i1 %37, double 0.000000e+00, double %36
  store double %38, double* %33, align 8, !tbaa !20
  %39 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 1
  %40 = bitcast double* %39 to <2 x double>*
  %41 = load <2 x double>, <2 x double>* %40, align 8, !tbaa !21
  %42 = fcmp olt <2 x double> %41, <double 1.000000e+00, double 1.000000e+00>
  %43 = select <2 x i1> %42, <2 x double> <double 1.000000e+00, double 1.000000e+00>, <2 x double> %41
  %44 = bitcast double* %39 to <2 x double>*
  store <2 x double> %43, <2 x double>* %44, align 8, !tbaa !21
  %45 = sext i32 %21 to i64
  %46 = sext i32 %23 to i64
  %47 = add nsw i64 %46, 1
  %48 = tail call i8* @klu_malloc(i64 1, i64 168, %struct.klu_common_struct* nonnull %4) #3
  %49 = bitcast %struct.klu_numeric** %9 to i8**
  store i8* %48, i8** %49, align 8, !tbaa !22
  %50 = load i32, i32* %14, align 4, !tbaa !7
  %51 = icmp slt i32 %50, 0
  br i1 %51, label %52, label %53

; <label>:52:                                     ; preds = %19
  store i32 -2, i32* %14, align 4, !tbaa !7
  br label %879

; <label>:53:                                     ; preds = %19
  %54 = bitcast i8* %48 to %struct.klu_numeric*
  %55 = add nsw i64 %45, 1
  %56 = bitcast i8* %48 to i32*
  store i32 %21, i32* %56, align 8, !tbaa !23
  %57 = getelementptr inbounds i8, i8* %48, i64 4
  %58 = bitcast i8* %57 to i32*
  store i32 %25, i32* %58, align 4, !tbaa !25
  %59 = getelementptr inbounds i8, i8* %48, i64 160
  %60 = bitcast i8* %59 to i32*
  store i32 %23, i32* %60, align 8, !tbaa !26
  %61 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %62 = getelementptr inbounds i8, i8* %48, i64 24
  %63 = bitcast i8* %62 to i8**
  store i8* %61, i8** %63, align 8, !tbaa !27
  %64 = tail call i8* @klu_malloc(i64 %55, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %65 = getelementptr inbounds i8, i8* %48, i64 136
  %66 = bitcast i8* %65 to i8**
  store i8* %64, i8** %66, align 8, !tbaa !28
  %67 = tail call i8* @klu_malloc(i64 %47, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %68 = getelementptr inbounds i8, i8* %48, i64 144
  %69 = bitcast i8* %68 to i8**
  store i8* %67, i8** %69, align 8, !tbaa !29
  %70 = tail call i8* @klu_malloc(i64 %47, i64 8, %struct.klu_common_struct* nonnull %4) #3
  %71 = getelementptr inbounds i8, i8* %48, i64 152
  %72 = bitcast i8* %71 to i8**
  store i8* %70, i8** %72, align 8, !tbaa !30
  %73 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %74 = getelementptr inbounds i8, i8* %48, i64 40
  %75 = bitcast i8* %74 to i8**
  store i8* %73, i8** %75, align 8, !tbaa !31
  %76 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %77 = getelementptr inbounds i8, i8* %48, i64 48
  %78 = bitcast i8* %77 to i8**
  store i8* %76, i8** %78, align 8, !tbaa !32
  %79 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %80 = getelementptr inbounds i8, i8* %48, i64 56
  %81 = bitcast i8* %80 to i8**
  store i8* %79, i8** %81, align 8, !tbaa !33
  %82 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %83 = getelementptr inbounds i8, i8* %48, i64 64
  %84 = bitcast i8* %83 to i8**
  store i8* %82, i8** %84, align 8, !tbaa !34
  %85 = sext i32 %25 to i64
  %86 = tail call i8* @klu_malloc(i64 %85, i64 8, %struct.klu_common_struct* nonnull %4) #3
  %87 = getelementptr inbounds i8, i8* %48, i64 80
  %88 = bitcast i8* %87 to i8**
  store i8* %86, i8** %88, align 8, !tbaa !35
  %89 = tail call i8* @klu_malloc(i64 %85, i64 8, %struct.klu_common_struct* nonnull %4) #3
  %90 = getelementptr inbounds i8, i8* %48, i64 72
  %91 = bitcast i8* %90 to i8**
  store i8* %89, i8** %91, align 8, !tbaa !36
  %92 = icmp ne i8* %89, null
  %93 = icmp sgt i32 %25, 0
  %94 = and i1 %92, %93
  br i1 %94, label %95, label %152

; <label>:95:                                     ; preds = %53
  %96 = bitcast i8* %89 to i8**
  %97 = zext i32 %25 to i64
  store i8* null, i8** %96, align 8, !tbaa !22
  %98 = icmp eq i32 %25, 1
  %99 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  br i1 %98, label %152, label %100

; <label>:100:                                    ; preds = %95
  %101 = add nsw i64 %97, -2
  %102 = add i32 %25, 3
  %103 = and i32 %102, 3
  %104 = zext i32 %103 to i64
  %105 = icmp ult i64 %101, 3
  br i1 %105, label %135, label %106

; <label>:106:                                    ; preds = %100
  %107 = add nsw i64 %97, -1
  %108 = sub nsw i64 %107, %104
  br label %109

; <label>:109:                                    ; preds = %109, %106
  %110 = phi %struct.klu_numeric* [ %99, %106 ], [ %132, %109 ]
  %111 = phi i64 [ 1, %106 ], [ %131, %109 ]
  %112 = phi i64 [ %108, %106 ], [ %133, %109 ]
  %113 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %110, i64 0, i32 12
  %114 = load i8**, i8*** %113, align 8, !tbaa !36
  %115 = getelementptr inbounds i8*, i8** %114, i64 %111
  store i8* null, i8** %115, align 8, !tbaa !22
  %116 = add nuw nsw i64 %111, 1
  %117 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  %118 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %117, i64 0, i32 12
  %119 = load i8**, i8*** %118, align 8, !tbaa !36
  %120 = getelementptr inbounds i8*, i8** %119, i64 %116
  store i8* null, i8** %120, align 8, !tbaa !22
  %121 = add nuw nsw i64 %111, 2
  %122 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  %123 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %122, i64 0, i32 12
  %124 = load i8**, i8*** %123, align 8, !tbaa !36
  %125 = getelementptr inbounds i8*, i8** %124, i64 %121
  store i8* null, i8** %125, align 8, !tbaa !22
  %126 = add nuw nsw i64 %111, 3
  %127 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  %128 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %127, i64 0, i32 12
  %129 = load i8**, i8*** %128, align 8, !tbaa !36
  %130 = getelementptr inbounds i8*, i8** %129, i64 %126
  store i8* null, i8** %130, align 8, !tbaa !22
  %131 = add nuw nsw i64 %111, 4
  %132 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  %133 = add i64 %112, -4
  %134 = icmp eq i64 %133, 0
  br i1 %134, label %135, label %109

; <label>:135:                                    ; preds = %109, %100
  %136 = phi %struct.klu_numeric* [ undef, %100 ], [ %132, %109 ]
  %137 = phi %struct.klu_numeric* [ %99, %100 ], [ %132, %109 ]
  %138 = phi i64 [ 1, %100 ], [ %131, %109 ]
  %139 = icmp eq i32 %103, 0
  br i1 %139, label %152, label %140

; <label>:140:                                    ; preds = %135
  br label %141

; <label>:141:                                    ; preds = %141, %140
  %142 = phi %struct.klu_numeric* [ %149, %141 ], [ %137, %140 ]
  %143 = phi i64 [ %148, %141 ], [ %138, %140 ]
  %144 = phi i64 [ %150, %141 ], [ %104, %140 ]
  %145 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %142, i64 0, i32 12
  %146 = load i8**, i8*** %145, align 8, !tbaa !36
  %147 = getelementptr inbounds i8*, i8** %146, i64 %143
  store i8* null, i8** %147, align 8, !tbaa !22
  %148 = add nuw nsw i64 %143, 1
  %149 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  %150 = add i64 %144, -1
  %151 = icmp eq i64 %150, 0
  br i1 %151, label %152, label %141, !llvm.loop !37

; <label>:152:                                    ; preds = %135, %141, %95, %53
  %153 = phi %struct.klu_numeric* [ %54, %53 ], [ %99, %95 ], [ %136, %135 ], [ %149, %141 ]
  %154 = tail call i8* @klu_malloc(i64 %45, i64 8, %struct.klu_common_struct* nonnull %4) #3
  %155 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 14
  store i8* %154, i8** %155, align 8, !tbaa !39
  %156 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 7
  %157 = load i32, i32* %156, align 8, !tbaa !40
  %158 = icmp sgt i32 %157, 0
  br i1 %158, label %159, label %163

; <label>:159:                                    ; preds = %152
  %160 = tail call i8* @klu_malloc(i64 %45, i64 8, %struct.klu_common_struct* nonnull %4) #3
  %161 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 15
  %162 = bitcast double** %161 to i8**
  store i8* %160, i8** %162, align 8, !tbaa !41
  br label %165

; <label>:163:                                    ; preds = %152
  %164 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 15
  store double* null, double** %164, align 8, !tbaa !41
  br label %165

; <label>:165:                                    ; preds = %163, %159
  %166 = tail call i8* @klu_malloc(i64 %45, i64 4, %struct.klu_common_struct* nonnull %4) #3
  %167 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 7
  %168 = bitcast i32** %167 to i8**
  store i8* %166, i8** %168, align 8, !tbaa !42
  %169 = call i64 @klu_mult_size_t(i64 %45, i64 8, i32* nonnull %8) #3
  %170 = call i64 @klu_mult_size_t(i64 %45, i64 24, i32* nonnull %8) #3
  %171 = sext i32 %27 to i64
  %172 = call i64 @klu_mult_size_t(i64 %171, i64 24, i32* nonnull %8) #3
  %173 = icmp ugt i64 %170, %172
  %174 = select i1 %173, i64 %170, i64 %172
  %175 = call i64 @klu_add_size_t(i64 %169, i64 %174, i32* nonnull %8) #3
  %176 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 16
  store i64 %175, i64* %176, align 8, !tbaa !43
  %177 = call i8* @klu_malloc(i64 %175, i64 1, %struct.klu_common_struct* nonnull %4) #3
  %178 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 17
  store i8* %177, i8** %178, align 8, !tbaa !44
  %179 = ptrtoint i8* %177 to i64
  %180 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 18
  %181 = bitcast i8** %180 to i64*
  store i64 %179, i64* %181, align 8, !tbaa !45
  %182 = bitcast i8* %177 to double*
  %183 = getelementptr inbounds double, double* %182, i64 %45
  %184 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 19
  %185 = bitcast i32** %184 to double**
  store double* %183, double** %185, align 8, !tbaa !46
  %186 = load i32, i32* %8, align 4, !tbaa !3
  %187 = icmp eq i32 %186, 0
  %188 = bitcast double* %183 to i32*
  br i1 %187, label %192, label %189

; <label>:189:                                    ; preds = %165
  %190 = load i32, i32* %14, align 4, !tbaa !7
  %191 = icmp slt i32 %190, 0
  br i1 %191, label %192, label %195

; <label>:192:                                    ; preds = %165, %189
  %193 = select i1 %187, i32 -4, i32 -2
  store i32 %193, i32* %14, align 4, !tbaa !7
  %194 = call i32 @klu_free_numeric(%struct.klu_numeric** nonnull %9, %struct.klu_common_struct* nonnull %4) #3
  br label %879

; <label>:195:                                    ; preds = %189
  %196 = bitcast i32* %6 to i8*
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %196) #3
  %197 = bitcast i32* %7 to i8*
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %197) #3
  %198 = load i32, i32* %20, align 8, !tbaa !14
  %199 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 7
  %200 = load i32*, i32** %199, align 8, !tbaa !47
  %201 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 8
  %202 = load i32*, i32** %201, align 8, !tbaa !48
  %203 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 9
  %204 = load i32*, i32** %203, align 8, !tbaa !49
  %205 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %3, i64 0, i32 4
  %206 = load double*, double** %205, align 8, !tbaa !50
  %207 = load i32, i32* %24, align 4, !tbaa !17
  %208 = load i32, i32* %22, align 8, !tbaa !16
  %209 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 6
  %210 = load i32*, i32** %209, align 8, !tbaa !27
  %211 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 20
  %212 = load i32*, i32** %211, align 8, !tbaa !28
  %213 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 21
  %214 = load i32*, i32** %213, align 8, !tbaa !29
  %215 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 22
  %216 = bitcast i8** %215 to double**
  %217 = load double*, double** %216, align 8, !tbaa !30
  %218 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 8
  %219 = load i32*, i32** %218, align 8, !tbaa !31
  %220 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 9
  %221 = load i32*, i32** %220, align 8, !tbaa !32
  %222 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 10
  %223 = load i32*, i32** %222, align 8, !tbaa !33
  %224 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 11
  %225 = load i32*, i32** %224, align 8, !tbaa !34
  %226 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 12
  %227 = bitcast i8*** %226 to double***
  %228 = load double**, double*** %227, align 8, !tbaa !36
  %229 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 14
  %230 = bitcast i8** %229 to double**
  %231 = load double*, double** %230, align 8, !tbaa !39
  %232 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 15
  %233 = load double*, double** %232, align 8, !tbaa !41
  %234 = bitcast double* %233 to i8*
  %235 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 7
  %236 = load i32*, i32** %235, align 8, !tbaa !42
  %237 = load i32, i32* %26, align 8, !tbaa !18
  %238 = sext i32 %237 to i64
  %239 = mul nsw i64 %238, 5
  %240 = getelementptr inbounds i32, i32* %188, i64 %239
  %241 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 12
  store i32 0, i32* %241, align 8, !tbaa !51
  %242 = load i32, i32* %156, align 8, !tbaa !40
  %243 = icmp sgt i32 %198, 0
  br i1 %243, label %244, label %295

; <label>:244:                                    ; preds = %195
  %245 = zext i32 %198 to i64
  %246 = add nsw i64 %245, -1
  %247 = and i64 %245, 3
  %248 = icmp ult i64 %246, 3
  br i1 %248, label %280, label %249

; <label>:249:                                    ; preds = %244
  %250 = sub nsw i64 %245, %247
  br label %251

; <label>:251:                                    ; preds = %251, %249
  %252 = phi i64 [ 0, %249 ], [ %277, %251 ]
  %253 = phi i64 [ %250, %249 ], [ %278, %251 ]
  %254 = getelementptr inbounds i32, i32* %200, i64 %252
  %255 = load i32, i32* %254, align 4, !tbaa !3
  %256 = sext i32 %255 to i64
  %257 = getelementptr inbounds i32, i32* %236, i64 %256
  %258 = trunc i64 %252 to i32
  store i32 %258, i32* %257, align 4, !tbaa !3
  %259 = or i64 %252, 1
  %260 = getelementptr inbounds i32, i32* %200, i64 %259
  %261 = load i32, i32* %260, align 4, !tbaa !3
  %262 = sext i32 %261 to i64
  %263 = getelementptr inbounds i32, i32* %236, i64 %262
  %264 = trunc i64 %259 to i32
  store i32 %264, i32* %263, align 4, !tbaa !3
  %265 = or i64 %252, 2
  %266 = getelementptr inbounds i32, i32* %200, i64 %265
  %267 = load i32, i32* %266, align 4, !tbaa !3
  %268 = sext i32 %267 to i64
  %269 = getelementptr inbounds i32, i32* %236, i64 %268
  %270 = trunc i64 %265 to i32
  store i32 %270, i32* %269, align 4, !tbaa !3
  %271 = or i64 %252, 3
  %272 = getelementptr inbounds i32, i32* %200, i64 %271
  %273 = load i32, i32* %272, align 4, !tbaa !3
  %274 = sext i32 %273 to i64
  %275 = getelementptr inbounds i32, i32* %236, i64 %274
  %276 = trunc i64 %271 to i32
  store i32 %276, i32* %275, align 4, !tbaa !3
  %277 = add nuw nsw i64 %252, 4
  %278 = add i64 %253, -4
  %279 = icmp eq i64 %278, 0
  br i1 %279, label %280, label %251

; <label>:280:                                    ; preds = %251, %244
  %281 = phi i64 [ 0, %244 ], [ %277, %251 ]
  %282 = icmp eq i64 %247, 0
  br i1 %282, label %295, label %283

; <label>:283:                                    ; preds = %280
  br label %284

; <label>:284:                                    ; preds = %284, %283
  %285 = phi i64 [ %281, %283 ], [ %292, %284 ]
  %286 = phi i64 [ %247, %283 ], [ %293, %284 ]
  %287 = getelementptr inbounds i32, i32* %200, i64 %285
  %288 = load i32, i32* %287, align 4, !tbaa !3
  %289 = sext i32 %288 to i64
  %290 = getelementptr inbounds i32, i32* %236, i64 %289
  %291 = trunc i64 %285 to i32
  store i32 %291, i32* %290, align 4, !tbaa !3
  %292 = add nuw nsw i64 %285, 1
  %293 = add i64 %286, -1
  %294 = icmp eq i64 %293, 0
  br i1 %294, label %295, label %284, !llvm.loop !52

; <label>:295:                                    ; preds = %280, %284, %195
  %296 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 16
  store i32 0, i32* %296, align 8, !tbaa !53
  store i32 0, i32* %212, align 4, !tbaa !3
  %297 = icmp sgt i32 %242, -1
  br i1 %297, label %298, label %302

; <label>:298:                                    ; preds = %295
  %299 = call i32 @klu_scale(i32 %242, i32 %198, i32* %0, i32* %1, double* %2, double* %233, i32* %210, %struct.klu_common_struct* nonnull %4) #3
  %300 = load i32, i32* %14, align 4, !tbaa !7
  %301 = icmp slt i32 %300, 0
  br i1 %301, label %848, label %302

; <label>:302:                                    ; preds = %298, %295
  %303 = icmp sgt i32 %207, 0
  br i1 %303, label %304, label %519

; <label>:304:                                    ; preds = %302
  %305 = icmp slt i32 %242, 1
  %306 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 13
  %307 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 10
  %308 = sext i32 %207 to i64
  br label %309

; <label>:309:                                    ; preds = %513, %304
  %310 = phi i64 [ 0, %304 ], [ %317, %513 ]
  %311 = phi i32 [ 1, %304 ], [ %517, %513 ]
  %312 = phi i32 [ 1, %304 ], [ %516, %513 ]
  %313 = phi i32 [ 0, %304 ], [ %515, %513 ]
  %314 = phi i32 [ 0, %304 ], [ %514, %513 ]
  %315 = getelementptr inbounds i32, i32* %204, i64 %310
  %316 = load i32, i32* %315, align 4, !tbaa !3
  %317 = add nuw nsw i64 %310, 1
  %318 = getelementptr inbounds i32, i32* %204, i64 %317
  %319 = load i32, i32* %318, align 4, !tbaa !3
  %320 = sub nsw i32 %319, %316
  %321 = icmp eq i32 %320, 1
  br i1 %321, label %322, label %424

; <label>:322:                                    ; preds = %309
  %323 = sext i32 %316 to i64
  %324 = getelementptr inbounds i32, i32* %212, i64 %323
  %325 = load i32, i32* %324, align 4, !tbaa !3
  %326 = getelementptr inbounds i32, i32* %202, i64 %323
  %327 = load i32, i32* %326, align 4, !tbaa !3
  %328 = add nsw i32 %327, 1
  %329 = sext i32 %328 to i64
  %330 = getelementptr inbounds i32, i32* %0, i64 %329
  %331 = load i32, i32* %330, align 4, !tbaa !3
  %332 = sext i32 %327 to i64
  %333 = getelementptr inbounds i32, i32* %0, i64 %332
  %334 = load i32, i32* %333, align 4, !tbaa !3
  %335 = icmp slt i32 %334, %331
  br i1 %305, label %336, label %367

; <label>:336:                                    ; preds = %322
  br i1 %335, label %337, label %402

; <label>:337:                                    ; preds = %336
  %338 = sext i32 %334 to i64
  %339 = sext i32 %331 to i64
  br label %340

; <label>:340:                                    ; preds = %362, %337
  %341 = phi i64 [ %338, %337 ], [ %365, %362 ]
  %342 = phi double [ 0.000000e+00, %337 ], [ %364, %362 ]
  %343 = phi i32 [ %325, %337 ], [ %363, %362 ]
  %344 = getelementptr inbounds i32, i32* %1, i64 %341
  %345 = load i32, i32* %344, align 4, !tbaa !3
  %346 = sext i32 %345 to i64
  %347 = getelementptr inbounds i32, i32* %236, i64 %346
  %348 = load i32, i32* %347, align 4, !tbaa !3
  %349 = icmp slt i32 %348, %316
  br i1 %349, label %350, label %359

; <label>:350:                                    ; preds = %340
  %351 = sext i32 %343 to i64
  %352 = getelementptr inbounds i32, i32* %214, i64 %351
  store i32 %345, i32* %352, align 4, !tbaa !3
  %353 = getelementptr inbounds double, double* %2, i64 %341
  %354 = bitcast double* %353 to i64*
  %355 = load i64, i64* %354, align 8, !tbaa !21
  %356 = getelementptr inbounds double, double* %217, i64 %351
  %357 = bitcast double* %356 to i64*
  store i64 %355, i64* %357, align 8, !tbaa !21
  %358 = add nsw i32 %343, 1
  br label %362

; <label>:359:                                    ; preds = %340
  %360 = getelementptr inbounds double, double* %2, i64 %341
  %361 = load double, double* %360, align 8, !tbaa !21
  br label %362

; <label>:362:                                    ; preds = %359, %350
  %363 = phi i32 [ %358, %350 ], [ %343, %359 ]
  %364 = phi double [ %342, %350 ], [ %361, %359 ]
  %365 = add nsw i64 %341, 1
  %366 = icmp eq i64 %365, %339
  br i1 %366, label %404, label %340

; <label>:367:                                    ; preds = %322
  br i1 %335, label %368, label %402

; <label>:368:                                    ; preds = %367
  %369 = sext i32 %334 to i64
  %370 = sext i32 %331 to i64
  br label %371

; <label>:371:                                    ; preds = %397, %368
  %372 = phi i64 [ %369, %368 ], [ %400, %397 ]
  %373 = phi double [ 0.000000e+00, %368 ], [ %399, %397 ]
  %374 = phi i32 [ %325, %368 ], [ %398, %397 ]
  %375 = getelementptr inbounds i32, i32* %1, i64 %372
  %376 = load i32, i32* %375, align 4, !tbaa !3
  %377 = sext i32 %376 to i64
  %378 = getelementptr inbounds i32, i32* %236, i64 %377
  %379 = load i32, i32* %378, align 4, !tbaa !3
  %380 = icmp slt i32 %379, %316
  br i1 %380, label %381, label %391

; <label>:381:                                    ; preds = %371
  %382 = sext i32 %374 to i64
  %383 = getelementptr inbounds i32, i32* %214, i64 %382
  store i32 %376, i32* %383, align 4, !tbaa !3
  %384 = getelementptr inbounds double, double* %2, i64 %372
  %385 = load double, double* %384, align 8, !tbaa !21
  %386 = getelementptr inbounds double, double* %233, i64 %377
  %387 = load double, double* %386, align 8, !tbaa !21
  %388 = fdiv double %385, %387
  %389 = getelementptr inbounds double, double* %217, i64 %382
  store double %388, double* %389, align 8, !tbaa !21
  %390 = add nsw i32 %374, 1
  br label %397

; <label>:391:                                    ; preds = %371
  %392 = getelementptr inbounds double, double* %2, i64 %372
  %393 = load double, double* %392, align 8, !tbaa !21
  %394 = getelementptr inbounds double, double* %233, i64 %377
  %395 = load double, double* %394, align 8, !tbaa !21
  %396 = fdiv double %393, %395
  br label %397

; <label>:397:                                    ; preds = %391, %381
  %398 = phi i32 [ %390, %381 ], [ %374, %391 ]
  %399 = phi double [ %373, %381 ], [ %396, %391 ]
  %400 = add nsw i64 %372, 1
  %401 = icmp eq i64 %400, %370
  br i1 %401, label %404, label %371

; <label>:402:                                    ; preds = %367, %336
  %403 = getelementptr inbounds double, double* %231, i64 %323
  store double 0.000000e+00, double* %403, align 8, !tbaa !21
  br label %409

; <label>:404:                                    ; preds = %397, %362
  %405 = phi i32 [ %363, %362 ], [ %398, %397 ]
  %406 = phi double [ %364, %362 ], [ %399, %397 ]
  %407 = getelementptr inbounds double, double* %231, i64 %323
  store double %406, double* %407, align 8, !tbaa !21
  %408 = fcmp oeq double %406, 0.000000e+00
  br i1 %408, label %409, label %414

; <label>:409:                                    ; preds = %404, %402
  %410 = phi i32 [ %325, %402 ], [ %405, %404 ]
  store i32 1, i32* %14, align 4, !tbaa !7
  store i32 %316, i32* %15, align 8, !tbaa !12
  store i32 %327, i32* %16, align 4, !tbaa !13
  %411 = load i32, i32* %307, align 8, !tbaa !54
  %412 = icmp eq i32 %411, 0
  br i1 %412, label %414, label %413

; <label>:413:                                    ; preds = %409
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %197) #3
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %196) #3
  br label %874

; <label>:414:                                    ; preds = %409, %404
  %415 = phi i32 [ %410, %409 ], [ %405, %404 ]
  %416 = add nsw i32 %316, 1
  %417 = sext i32 %416 to i64
  %418 = getelementptr inbounds i32, i32* %212, i64 %417
  store i32 %415, i32* %418, align 4, !tbaa !3
  %419 = getelementptr inbounds i32, i32* %200, i64 %323
  %420 = load i32, i32* %419, align 4, !tbaa !3
  %421 = getelementptr inbounds i32, i32* %210, i64 %323
  store i32 %420, i32* %421, align 4, !tbaa !3
  %422 = add nsw i32 %314, 1
  %423 = add nsw i32 %313, 1
  br label %513

; <label>:424:                                    ; preds = %309
  %425 = getelementptr inbounds double, double* %206, i64 %310
  %426 = load double, double* %425, align 8, !tbaa !21
  %427 = fcmp olt double %426, 0.000000e+00
  br i1 %427, label %428, label %431

; <label>:428:                                    ; preds = %424
  %429 = load double, double* %29, align 8, !tbaa !19
  %430 = fsub double -0.000000e+00, %429
  br label %436

; <label>:431:                                    ; preds = %424
  %432 = load double, double* %28, align 8, !tbaa !55
  %433 = fmul double %426, %432
  %434 = sitofp i32 %320 to double
  %435 = fadd double %433, %434
  br label %436

; <label>:436:                                    ; preds = %431, %428
  %437 = phi double [ %430, %428 ], [ %435, %431 ]
  %438 = getelementptr inbounds double*, double** %228, i64 %310
  %439 = sext i32 %316 to i64
  %440 = getelementptr inbounds double, double* %231, i64 %439
  %441 = getelementptr inbounds i32, i32* %223, i64 %439
  %442 = getelementptr inbounds i32, i32* %225, i64 %439
  %443 = getelementptr inbounds i32, i32* %219, i64 %439
  %444 = getelementptr inbounds i32, i32* %221, i64 %439
  %445 = call i64 @klu_kernel_factor(i32 %320, i32* %0, i32* %1, double* %2, i32* %202, double %437, double** %438, double* %440, i32* %441, i32* %442, i32* %443, i32* %444, i32* %240, i32* nonnull %6, i32* nonnull %7, double* %182, i32* %188, i32 %316, i32* %236, double* %233, i32* %212, i32* %214, double* %217, %struct.klu_common_struct* nonnull %4) #3
  %446 = load i64*, i64** %306, align 8, !tbaa !35
  %447 = getelementptr inbounds i64, i64* %446, i64 %310
  store i64 %445, i64* %447, align 8, !tbaa !56
  %448 = load i32, i32* %14, align 4, !tbaa !7
  %449 = icmp slt i32 %448, 0
  br i1 %449, label %848, label %450

; <label>:450:                                    ; preds = %436
  %451 = icmp eq i32 %448, 1
  br i1 %451, label %452, label %455

; <label>:452:                                    ; preds = %450
  %453 = load i32, i32* %307, align 8, !tbaa !54
  %454 = icmp eq i32 %453, 0
  br i1 %454, label %455, label %864

; <label>:455:                                    ; preds = %452, %450
  %456 = load i32, i32* %6, align 4, !tbaa !3
  %457 = add nsw i32 %456, %314
  %458 = load i32, i32* %7, align 4, !tbaa !3
  %459 = add nsw i32 %458, %313
  %460 = icmp sgt i32 %312, %456
  %461 = select i1 %460, i32 %312, i32 %456
  %462 = icmp sgt i32 %311, %458
  %463 = select i1 %462, i32 %311, i32 %458
  %464 = load double, double* %425, align 8, !tbaa !21
  %465 = fcmp oeq double %464, -1.000000e+00
  br i1 %465, label %466, label %470

; <label>:466:                                    ; preds = %455
  %467 = icmp sgt i32 %456, %458
  %468 = select i1 %467, i32 %456, i32 %458
  %469 = sitofp i32 %468 to double
  store double %469, double* %425, align 8, !tbaa !21
  br label %470

; <label>:470:                                    ; preds = %466, %455
  %471 = icmp sgt i32 %320, 0
  br i1 %471, label %472, label %513

; <label>:472:                                    ; preds = %470
  %473 = zext i32 %320 to i64
  %474 = and i64 %473, 1
  %475 = icmp eq i32 %320, 1
  br i1 %475, label %501, label %476

; <label>:476:                                    ; preds = %472
  %477 = sub nsw i64 %473, %474
  br label %478

; <label>:478:                                    ; preds = %478, %476
  %479 = phi i64 [ 0, %476 ], [ %498, %478 ]
  %480 = phi i64 [ %477, %476 ], [ %499, %478 ]
  %481 = getelementptr inbounds i32, i32* %240, i64 %479
  %482 = load i32, i32* %481, align 4, !tbaa !3
  %483 = add nsw i32 %482, %316
  %484 = sext i32 %483 to i64
  %485 = getelementptr inbounds i32, i32* %200, i64 %484
  %486 = load i32, i32* %485, align 4, !tbaa !3
  %487 = add nsw i64 %479, %439
  %488 = getelementptr inbounds i32, i32* %210, i64 %487
  store i32 %486, i32* %488, align 4, !tbaa !3
  %489 = or i64 %479, 1
  %490 = getelementptr inbounds i32, i32* %240, i64 %489
  %491 = load i32, i32* %490, align 4, !tbaa !3
  %492 = add nsw i32 %491, %316
  %493 = sext i32 %492 to i64
  %494 = getelementptr inbounds i32, i32* %200, i64 %493
  %495 = load i32, i32* %494, align 4, !tbaa !3
  %496 = add nsw i64 %489, %439
  %497 = getelementptr inbounds i32, i32* %210, i64 %496
  store i32 %495, i32* %497, align 4, !tbaa !3
  %498 = add nuw nsw i64 %479, 2
  %499 = add i64 %480, -2
  %500 = icmp eq i64 %499, 0
  br i1 %500, label %501, label %478

; <label>:501:                                    ; preds = %478, %472
  %502 = phi i64 [ 0, %472 ], [ %498, %478 ]
  %503 = icmp eq i64 %474, 0
  br i1 %503, label %513, label %504

; <label>:504:                                    ; preds = %501
  %505 = getelementptr inbounds i32, i32* %240, i64 %502
  %506 = load i32, i32* %505, align 4, !tbaa !3
  %507 = add nsw i32 %506, %316
  %508 = sext i32 %507 to i64
  %509 = getelementptr inbounds i32, i32* %200, i64 %508
  %510 = load i32, i32* %509, align 4, !tbaa !3
  %511 = add nsw i64 %502, %439
  %512 = getelementptr inbounds i32, i32* %210, i64 %511
  store i32 %510, i32* %512, align 4, !tbaa !3
  br label %513

; <label>:513:                                    ; preds = %504, %501, %470, %414
  %514 = phi i32 [ %422, %414 ], [ %457, %470 ], [ %457, %501 ], [ %457, %504 ]
  %515 = phi i32 [ %423, %414 ], [ %459, %470 ], [ %459, %501 ], [ %459, %504 ]
  %516 = phi i32 [ %312, %414 ], [ %461, %470 ], [ %461, %501 ], [ %461, %504 ]
  %517 = phi i32 [ %311, %414 ], [ %463, %470 ], [ %463, %501 ], [ %463, %504 ]
  %518 = icmp slt i64 %317, %308
  br i1 %518, label %309, label %519

; <label>:519:                                    ; preds = %513, %302
  %520 = phi i32 [ 0, %302 ], [ %514, %513 ]
  %521 = phi i32 [ 0, %302 ], [ %515, %513 ]
  %522 = phi i32 [ 1, %302 ], [ %516, %513 ]
  %523 = phi i32 [ 1, %302 ], [ %517, %513 ]
  %524 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 2
  store i32 %520, i32* %524, align 8, !tbaa !57
  %525 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 3
  store i32 %521, i32* %525, align 4, !tbaa !58
  %526 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 4
  store i32 %522, i32* %526, align 8, !tbaa !59
  %527 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %153, i64 0, i32 5
  store i32 %523, i32* %527, align 4, !tbaa !60
  br i1 %243, label %528, label %810

; <label>:528:                                    ; preds = %519
  %529 = zext i32 %198 to i64
  %530 = add nsw i64 %529, -1
  %531 = and i64 %529, 3
  %532 = icmp ult i64 %530, 3
  br i1 %532, label %564, label %533

; <label>:533:                                    ; preds = %528
  %534 = sub nsw i64 %529, %531
  br label %535

; <label>:535:                                    ; preds = %535, %533
  %536 = phi i64 [ 0, %533 ], [ %561, %535 ]
  %537 = phi i64 [ %534, %533 ], [ %562, %535 ]
  %538 = getelementptr inbounds i32, i32* %210, i64 %536
  %539 = load i32, i32* %538, align 4, !tbaa !3
  %540 = sext i32 %539 to i64
  %541 = getelementptr inbounds i32, i32* %236, i64 %540
  %542 = trunc i64 %536 to i32
  store i32 %542, i32* %541, align 4, !tbaa !3
  %543 = or i64 %536, 1
  %544 = getelementptr inbounds i32, i32* %210, i64 %543
  %545 = load i32, i32* %544, align 4, !tbaa !3
  %546 = sext i32 %545 to i64
  %547 = getelementptr inbounds i32, i32* %236, i64 %546
  %548 = trunc i64 %543 to i32
  store i32 %548, i32* %547, align 4, !tbaa !3
  %549 = or i64 %536, 2
  %550 = getelementptr inbounds i32, i32* %210, i64 %549
  %551 = load i32, i32* %550, align 4, !tbaa !3
  %552 = sext i32 %551 to i64
  %553 = getelementptr inbounds i32, i32* %236, i64 %552
  %554 = trunc i64 %549 to i32
  store i32 %554, i32* %553, align 4, !tbaa !3
  %555 = or i64 %536, 3
  %556 = getelementptr inbounds i32, i32* %210, i64 %555
  %557 = load i32, i32* %556, align 4, !tbaa !3
  %558 = sext i32 %557 to i64
  %559 = getelementptr inbounds i32, i32* %236, i64 %558
  %560 = trunc i64 %555 to i32
  store i32 %560, i32* %559, align 4, !tbaa !3
  %561 = add nuw nsw i64 %536, 4
  %562 = add i64 %537, -4
  %563 = icmp eq i64 %562, 0
  br i1 %563, label %564, label %535

; <label>:564:                                    ; preds = %535, %528
  %565 = phi i64 [ 0, %528 ], [ %561, %535 ]
  %566 = icmp eq i64 %531, 0
  br i1 %566, label %579, label %567

; <label>:567:                                    ; preds = %564
  br label %568

; <label>:568:                                    ; preds = %568, %567
  %569 = phi i64 [ %565, %567 ], [ %576, %568 ]
  %570 = phi i64 [ %531, %567 ], [ %577, %568 ]
  %571 = getelementptr inbounds i32, i32* %210, i64 %569
  %572 = load i32, i32* %571, align 4, !tbaa !3
  %573 = sext i32 %572 to i64
  %574 = getelementptr inbounds i32, i32* %236, i64 %573
  %575 = trunc i64 %569 to i32
  store i32 %575, i32* %574, align 4, !tbaa !3
  %576 = add nuw nsw i64 %569, 1
  %577 = add i64 %570, -1
  %578 = icmp eq i64 %577, 0
  br i1 %578, label %579, label %568, !llvm.loop !61

; <label>:579:                                    ; preds = %568, %564
  %580 = icmp slt i32 %242, 1
  br i1 %580, label %810, label %581

; <label>:581:                                    ; preds = %579
  %582 = add nsw i64 %529, -1
  %583 = and i64 %529, 3
  %584 = icmp ult i64 %582, 3
  br i1 %584, label %628, label %585

; <label>:585:                                    ; preds = %581
  %586 = sub nsw i64 %529, %583
  br label %587

; <label>:587:                                    ; preds = %587, %585
  %588 = phi i64 [ 0, %585 ], [ %625, %587 ]
  %589 = phi i64 [ %586, %585 ], [ %626, %587 ]
  %590 = getelementptr inbounds i32, i32* %210, i64 %588
  %591 = load i32, i32* %590, align 4, !tbaa !3
  %592 = sext i32 %591 to i64
  %593 = getelementptr inbounds double, double* %233, i64 %592
  %594 = bitcast double* %593 to i64*
  %595 = load i64, i64* %594, align 8, !tbaa !21
  %596 = getelementptr inbounds double, double* %182, i64 %588
  %597 = bitcast double* %596 to i64*
  store i64 %595, i64* %597, align 8, !tbaa !21
  %598 = or i64 %588, 1
  %599 = getelementptr inbounds i32, i32* %210, i64 %598
  %600 = load i32, i32* %599, align 4, !tbaa !3
  %601 = sext i32 %600 to i64
  %602 = getelementptr inbounds double, double* %233, i64 %601
  %603 = bitcast double* %602 to i64*
  %604 = load i64, i64* %603, align 8, !tbaa !21
  %605 = getelementptr inbounds double, double* %182, i64 %598
  %606 = bitcast double* %605 to i64*
  store i64 %604, i64* %606, align 8, !tbaa !21
  %607 = or i64 %588, 2
  %608 = getelementptr inbounds i32, i32* %210, i64 %607
  %609 = load i32, i32* %608, align 4, !tbaa !3
  %610 = sext i32 %609 to i64
  %611 = getelementptr inbounds double, double* %233, i64 %610
  %612 = bitcast double* %611 to i64*
  %613 = load i64, i64* %612, align 8, !tbaa !21
  %614 = getelementptr inbounds double, double* %182, i64 %607
  %615 = bitcast double* %614 to i64*
  store i64 %613, i64* %615, align 8, !tbaa !21
  %616 = or i64 %588, 3
  %617 = getelementptr inbounds i32, i32* %210, i64 %616
  %618 = load i32, i32* %617, align 4, !tbaa !3
  %619 = sext i32 %618 to i64
  %620 = getelementptr inbounds double, double* %233, i64 %619
  %621 = bitcast double* %620 to i64*
  %622 = load i64, i64* %621, align 8, !tbaa !21
  %623 = getelementptr inbounds double, double* %182, i64 %616
  %624 = bitcast double* %623 to i64*
  store i64 %622, i64* %624, align 8, !tbaa !21
  %625 = add nuw nsw i64 %588, 4
  %626 = add i64 %589, -4
  %627 = icmp eq i64 %626, 0
  br i1 %627, label %628, label %587

; <label>:628:                                    ; preds = %587, %581
  %629 = phi i64 [ 0, %581 ], [ %625, %587 ]
  %630 = icmp eq i64 %583, 0
  br i1 %630, label %646, label %631

; <label>:631:                                    ; preds = %628
  br label %632

; <label>:632:                                    ; preds = %632, %631
  %633 = phi i64 [ %643, %632 ], [ %629, %631 ]
  %634 = phi i64 [ %644, %632 ], [ %583, %631 ]
  %635 = getelementptr inbounds i32, i32* %210, i64 %633
  %636 = load i32, i32* %635, align 4, !tbaa !3
  %637 = sext i32 %636 to i64
  %638 = getelementptr inbounds double, double* %233, i64 %637
  %639 = bitcast double* %638 to i64*
  %640 = load i64, i64* %639, align 8, !tbaa !21
  %641 = getelementptr inbounds double, double* %182, i64 %633
  %642 = bitcast double* %641 to i64*
  store i64 %640, i64* %642, align 8, !tbaa !21
  %643 = add nuw nsw i64 %633, 1
  %644 = add i64 %634, -1
  %645 = icmp eq i64 %644, 0
  br i1 %645, label %646, label %632, !llvm.loop !62

; <label>:646:                                    ; preds = %632, %628
  %647 = icmp ult i32 %198, 4
  br i1 %647, label %736, label %648

; <label>:648:                                    ; preds = %646
  %649 = getelementptr double, double* %233, i64 %529
  %650 = bitcast double* %649 to i8*
  %651 = shl nuw nsw i64 %529, 3
  %652 = getelementptr i8, i8* %177, i64 %651
  %653 = icmp ugt i8* %652, %234
  %654 = icmp ult i8* %177, %650
  %655 = and i1 %653, %654
  br i1 %655, label %736, label %656

; <label>:656:                                    ; preds = %648
  %657 = and i64 %529, 4294967292
  %658 = add nsw i64 %657, -4
  %659 = lshr exact i64 %658, 2
  %660 = add nuw nsw i64 %659, 1
  %661 = and i64 %660, 3
  %662 = icmp ult i64 %658, 12
  br i1 %662, label %714, label %663

; <label>:663:                                    ; preds = %656
  %664 = sub nsw i64 %660, %661
  br label %665

; <label>:665:                                    ; preds = %665, %663
  %666 = phi i64 [ 0, %663 ], [ %711, %665 ]
  %667 = phi i64 [ %664, %663 ], [ %712, %665 ]
  %668 = getelementptr inbounds double, double* %182, i64 %666
  %669 = bitcast double* %668 to <2 x i64>*
  %670 = load <2 x i64>, <2 x i64>* %669, align 8, !tbaa !21, !alias.scope !63
  %671 = getelementptr double, double* %668, i64 2
  %672 = bitcast double* %671 to <2 x i64>*
  %673 = load <2 x i64>, <2 x i64>* %672, align 8, !tbaa !21, !alias.scope !63
  %674 = getelementptr inbounds double, double* %233, i64 %666
  %675 = bitcast double* %674 to <2 x i64>*
  store <2 x i64> %670, <2 x i64>* %675, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %676 = getelementptr double, double* %674, i64 2
  %677 = bitcast double* %676 to <2 x i64>*
  store <2 x i64> %673, <2 x i64>* %677, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %678 = or i64 %666, 4
  %679 = getelementptr inbounds double, double* %182, i64 %678
  %680 = bitcast double* %679 to <2 x i64>*
  %681 = load <2 x i64>, <2 x i64>* %680, align 8, !tbaa !21, !alias.scope !63
  %682 = getelementptr double, double* %679, i64 2
  %683 = bitcast double* %682 to <2 x i64>*
  %684 = load <2 x i64>, <2 x i64>* %683, align 8, !tbaa !21, !alias.scope !63
  %685 = getelementptr inbounds double, double* %233, i64 %678
  %686 = bitcast double* %685 to <2 x i64>*
  store <2 x i64> %681, <2 x i64>* %686, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %687 = getelementptr double, double* %685, i64 2
  %688 = bitcast double* %687 to <2 x i64>*
  store <2 x i64> %684, <2 x i64>* %688, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %689 = or i64 %666, 8
  %690 = getelementptr inbounds double, double* %182, i64 %689
  %691 = bitcast double* %690 to <2 x i64>*
  %692 = load <2 x i64>, <2 x i64>* %691, align 8, !tbaa !21, !alias.scope !63
  %693 = getelementptr double, double* %690, i64 2
  %694 = bitcast double* %693 to <2 x i64>*
  %695 = load <2 x i64>, <2 x i64>* %694, align 8, !tbaa !21, !alias.scope !63
  %696 = getelementptr inbounds double, double* %233, i64 %689
  %697 = bitcast double* %696 to <2 x i64>*
  store <2 x i64> %692, <2 x i64>* %697, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %698 = getelementptr double, double* %696, i64 2
  %699 = bitcast double* %698 to <2 x i64>*
  store <2 x i64> %695, <2 x i64>* %699, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %700 = or i64 %666, 12
  %701 = getelementptr inbounds double, double* %182, i64 %700
  %702 = bitcast double* %701 to <2 x i64>*
  %703 = load <2 x i64>, <2 x i64>* %702, align 8, !tbaa !21, !alias.scope !63
  %704 = getelementptr double, double* %701, i64 2
  %705 = bitcast double* %704 to <2 x i64>*
  %706 = load <2 x i64>, <2 x i64>* %705, align 8, !tbaa !21, !alias.scope !63
  %707 = getelementptr inbounds double, double* %233, i64 %700
  %708 = bitcast double* %707 to <2 x i64>*
  store <2 x i64> %703, <2 x i64>* %708, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %709 = getelementptr double, double* %707, i64 2
  %710 = bitcast double* %709 to <2 x i64>*
  store <2 x i64> %706, <2 x i64>* %710, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %711 = add i64 %666, 16
  %712 = add i64 %667, -4
  %713 = icmp eq i64 %712, 0
  br i1 %713, label %714, label %665, !llvm.loop !68

; <label>:714:                                    ; preds = %665, %656
  %715 = phi i64 [ 0, %656 ], [ %711, %665 ]
  %716 = icmp eq i64 %661, 0
  br i1 %716, label %734, label %717

; <label>:717:                                    ; preds = %714
  br label %718

; <label>:718:                                    ; preds = %718, %717
  %719 = phi i64 [ %715, %717 ], [ %731, %718 ]
  %720 = phi i64 [ %661, %717 ], [ %732, %718 ]
  %721 = getelementptr inbounds double, double* %182, i64 %719
  %722 = bitcast double* %721 to <2 x i64>*
  %723 = load <2 x i64>, <2 x i64>* %722, align 8, !tbaa !21, !alias.scope !63
  %724 = getelementptr double, double* %721, i64 2
  %725 = bitcast double* %724 to <2 x i64>*
  %726 = load <2 x i64>, <2 x i64>* %725, align 8, !tbaa !21, !alias.scope !63
  %727 = getelementptr inbounds double, double* %233, i64 %719
  %728 = bitcast double* %727 to <2 x i64>*
  store <2 x i64> %723, <2 x i64>* %728, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %729 = getelementptr double, double* %727, i64 2
  %730 = bitcast double* %729 to <2 x i64>*
  store <2 x i64> %726, <2 x i64>* %730, align 8, !tbaa !21, !alias.scope !66, !noalias !63
  %731 = add i64 %719, 4
  %732 = add i64 %720, -1
  %733 = icmp eq i64 %732, 0
  br i1 %733, label %734, label %718, !llvm.loop !70

; <label>:734:                                    ; preds = %718, %714
  %735 = icmp eq i64 %657, %529
  br i1 %735, label %810, label %736

; <label>:736:                                    ; preds = %734, %648, %646
  %737 = phi i64 [ 0, %648 ], [ 0, %646 ], [ %657, %734 ]
  %738 = sub nsw i64 %529, %737
  %739 = add nsw i64 %529, -1
  %740 = sub nsw i64 %739, %737
  %741 = and i64 %738, 7
  %742 = icmp eq i64 %741, 0
  br i1 %742, label %755, label %743

; <label>:743:                                    ; preds = %736
  br label %744

; <label>:744:                                    ; preds = %744, %743
  %745 = phi i64 [ %752, %744 ], [ %737, %743 ]
  %746 = phi i64 [ %753, %744 ], [ %741, %743 ]
  %747 = getelementptr inbounds double, double* %182, i64 %745
  %748 = bitcast double* %747 to i64*
  %749 = load i64, i64* %748, align 8, !tbaa !21
  %750 = getelementptr inbounds double, double* %233, i64 %745
  %751 = bitcast double* %750 to i64*
  store i64 %749, i64* %751, align 8, !tbaa !21
  %752 = add nuw nsw i64 %745, 1
  %753 = add i64 %746, -1
  %754 = icmp eq i64 %753, 0
  br i1 %754, label %755, label %744, !llvm.loop !71

; <label>:755:                                    ; preds = %744, %736
  %756 = phi i64 [ %737, %736 ], [ %752, %744 ]
  %757 = icmp ult i64 %740, 7
  br i1 %757, label %810, label %758

; <label>:758:                                    ; preds = %755
  br label %759

; <label>:759:                                    ; preds = %759, %758
  %760 = phi i64 [ %756, %758 ], [ %808, %759 ]
  %761 = getelementptr inbounds double, double* %182, i64 %760
  %762 = bitcast double* %761 to i64*
  %763 = load i64, i64* %762, align 8, !tbaa !21
  %764 = getelementptr inbounds double, double* %233, i64 %760
  %765 = bitcast double* %764 to i64*
  store i64 %763, i64* %765, align 8, !tbaa !21
  %766 = add nuw nsw i64 %760, 1
  %767 = getelementptr inbounds double, double* %182, i64 %766
  %768 = bitcast double* %767 to i64*
  %769 = load i64, i64* %768, align 8, !tbaa !21
  %770 = getelementptr inbounds double, double* %233, i64 %766
  %771 = bitcast double* %770 to i64*
  store i64 %769, i64* %771, align 8, !tbaa !21
  %772 = add nsw i64 %760, 2
  %773 = getelementptr inbounds double, double* %182, i64 %772
  %774 = bitcast double* %773 to i64*
  %775 = load i64, i64* %774, align 8, !tbaa !21
  %776 = getelementptr inbounds double, double* %233, i64 %772
  %777 = bitcast double* %776 to i64*
  store i64 %775, i64* %777, align 8, !tbaa !21
  %778 = add nsw i64 %760, 3
  %779 = getelementptr inbounds double, double* %182, i64 %778
  %780 = bitcast double* %779 to i64*
  %781 = load i64, i64* %780, align 8, !tbaa !21
  %782 = getelementptr inbounds double, double* %233, i64 %778
  %783 = bitcast double* %782 to i64*
  store i64 %781, i64* %783, align 8, !tbaa !21
  %784 = add nsw i64 %760, 4
  %785 = getelementptr inbounds double, double* %182, i64 %784
  %786 = bitcast double* %785 to i64*
  %787 = load i64, i64* %786, align 8, !tbaa !21
  %788 = getelementptr inbounds double, double* %233, i64 %784
  %789 = bitcast double* %788 to i64*
  store i64 %787, i64* %789, align 8, !tbaa !21
  %790 = add nsw i64 %760, 5
  %791 = getelementptr inbounds double, double* %182, i64 %790
  %792 = bitcast double* %791 to i64*
  %793 = load i64, i64* %792, align 8, !tbaa !21
  %794 = getelementptr inbounds double, double* %233, i64 %790
  %795 = bitcast double* %794 to i64*
  store i64 %793, i64* %795, align 8, !tbaa !21
  %796 = add nsw i64 %760, 6
  %797 = getelementptr inbounds double, double* %182, i64 %796
  %798 = bitcast double* %797 to i64*
  %799 = load i64, i64* %798, align 8, !tbaa !21
  %800 = getelementptr inbounds double, double* %233, i64 %796
  %801 = bitcast double* %800 to i64*
  store i64 %799, i64* %801, align 8, !tbaa !21
  %802 = add nsw i64 %760, 7
  %803 = getelementptr inbounds double, double* %182, i64 %802
  %804 = bitcast double* %803 to i64*
  %805 = load i64, i64* %804, align 8, !tbaa !21
  %806 = getelementptr inbounds double, double* %233, i64 %802
  %807 = bitcast double* %806 to i64*
  store i64 %805, i64* %807, align 8, !tbaa !21
  %808 = add nsw i64 %760, 8
  %809 = icmp eq i64 %808, %529
  br i1 %809, label %810, label %759, !llvm.loop !72

; <label>:810:                                    ; preds = %755, %759, %734, %519, %579
  %811 = icmp sgt i32 %208, 0
  br i1 %811, label %812, label %864

; <label>:812:                                    ; preds = %810
  %813 = zext i32 %208 to i64
  %814 = add nsw i64 %813, -1
  %815 = and i64 %813, 3
  %816 = icmp ult i64 %814, 3
  br i1 %816, label %849, label %817

; <label>:817:                                    ; preds = %812
  %818 = sub nsw i64 %813, %815
  br label %819

; <label>:819:                                    ; preds = %819, %817
  %820 = phi i64 [ 0, %817 ], [ %845, %819 ]
  %821 = phi i64 [ %818, %817 ], [ %846, %819 ]
  %822 = getelementptr inbounds i32, i32* %214, i64 %820
  %823 = load i32, i32* %822, align 4, !tbaa !3
  %824 = sext i32 %823 to i64
  %825 = getelementptr inbounds i32, i32* %236, i64 %824
  %826 = load i32, i32* %825, align 4, !tbaa !3
  store i32 %826, i32* %822, align 4, !tbaa !3
  %827 = or i64 %820, 1
  %828 = getelementptr inbounds i32, i32* %214, i64 %827
  %829 = load i32, i32* %828, align 4, !tbaa !3
  %830 = sext i32 %829 to i64
  %831 = getelementptr inbounds i32, i32* %236, i64 %830
  %832 = load i32, i32* %831, align 4, !tbaa !3
  store i32 %832, i32* %828, align 4, !tbaa !3
  %833 = or i64 %820, 2
  %834 = getelementptr inbounds i32, i32* %214, i64 %833
  %835 = load i32, i32* %834, align 4, !tbaa !3
  %836 = sext i32 %835 to i64
  %837 = getelementptr inbounds i32, i32* %236, i64 %836
  %838 = load i32, i32* %837, align 4, !tbaa !3
  store i32 %838, i32* %834, align 4, !tbaa !3
  %839 = or i64 %820, 3
  %840 = getelementptr inbounds i32, i32* %214, i64 %839
  %841 = load i32, i32* %840, align 4, !tbaa !3
  %842 = sext i32 %841 to i64
  %843 = getelementptr inbounds i32, i32* %236, i64 %842
  %844 = load i32, i32* %843, align 4, !tbaa !3
  store i32 %844, i32* %840, align 4, !tbaa !3
  %845 = add nuw nsw i64 %820, 4
  %846 = add i64 %821, -4
  %847 = icmp eq i64 %846, 0
  br i1 %847, label %849, label %819

; <label>:848:                                    ; preds = %436, %298
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %197) #3
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %196) #3
  br label %867

; <label>:849:                                    ; preds = %819, %812
  %850 = phi i64 [ 0, %812 ], [ %845, %819 ]
  %851 = icmp eq i64 %815, 0
  br i1 %851, label %864, label %852

; <label>:852:                                    ; preds = %849
  br label %853

; <label>:853:                                    ; preds = %853, %852
  %854 = phi i64 [ %850, %852 ], [ %861, %853 ]
  %855 = phi i64 [ %815, %852 ], [ %862, %853 ]
  %856 = getelementptr inbounds i32, i32* %214, i64 %854
  %857 = load i32, i32* %856, align 4, !tbaa !3
  %858 = sext i32 %857 to i64
  %859 = getelementptr inbounds i32, i32* %236, i64 %858
  %860 = load i32, i32* %859, align 4, !tbaa !3
  store i32 %860, i32* %856, align 4, !tbaa !3
  %861 = add nuw nsw i64 %854, 1
  %862 = add i64 %855, -1
  %863 = icmp eq i64 %862, 0
  br i1 %863, label %864, label %853, !llvm.loop !73

; <label>:864:                                    ; preds = %452, %849, %853, %810
  %865 = load i32, i32* %14, align 4, !tbaa !7
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %197) #3
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %196) #3
  %866 = icmp slt i32 %865, 0
  br i1 %866, label %867, label %869

; <label>:867:                                    ; preds = %848, %864
  %868 = call i32 @klu_free_numeric(%struct.klu_numeric** nonnull %9, %struct.klu_common_struct* nonnull %4) #3
  br label %877

; <label>:869:                                    ; preds = %864
  switch i32 %865, label %877 [
    i32 1, label %870
    i32 0, label %876
  ]

; <label>:870:                                    ; preds = %869
  %871 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 10
  %872 = load i32, i32* %871, align 8, !tbaa !54
  %873 = icmp eq i32 %872, 0
  br i1 %873, label %877, label %874

; <label>:874:                                    ; preds = %413, %870
  %875 = call i32 @klu_free_numeric(%struct.klu_numeric** nonnull %9, %struct.klu_common_struct* nonnull %4) #3
  br label %877

; <label>:876:                                    ; preds = %869
  store i32 %21, i32* %15, align 8, !tbaa !12
  store i32 %21, i32* %16, align 4, !tbaa !13
  br label %877

; <label>:877:                                    ; preds = %869, %870, %874, %876, %867
  %878 = load %struct.klu_numeric*, %struct.klu_numeric** %9, align 8, !tbaa !22
  br label %879

; <label>:879:                                    ; preds = %5, %877, %192, %52, %18
  %880 = phi %struct.klu_numeric* [ null, %18 ], [ null, %52 ], [ null, %192 ], [ %878, %877 ], [ null, %5 ]
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %11) #3
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %10) #3
  ret %struct.klu_numeric* %880
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i64 @klu_mult_size_t(i64, i64, i32*) local_unnamed_addr #2

declare i64 @klu_add_size_t(i64, i64, i32*) local_unnamed_addr #2

declare i32 @klu_free_numeric(%struct.klu_numeric**, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

declare i32 @klu_scale(i32, i32, i32*, i32*, double*, double*, i32*, %struct.klu_common_struct*) local_unnamed_addr #2

declare i64 @klu_kernel_factor(i32, i32*, i32*, double*, i32*, double, double**, double*, i32*, i32*, i32*, i32*, i32*, i32*, i32*, double*, i32*, i32, i32*, double*, i32*, i32*, double*, %struct.klu_common_struct*) local_unnamed_addr #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
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
!7 = !{!8, !4, i64 76}
!8 = !{!"klu_common_struct", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !9, i64 32, !4, i64 40, !4, i64 44, !4, i64 48, !10, i64 56, !10, i64 64, !4, i64 72, !4, i64 76, !4, i64 80, !4, i64 84, !4, i64 88, !4, i64 92, !4, i64 96, !9, i64 104, !9, i64 112, !9, i64 120, !9, i64 128, !9, i64 136, !11, i64 144, !11, i64 152}
!9 = !{!"double", !5, i64 0}
!10 = !{!"any pointer", !5, i64 0}
!11 = !{!"long", !5, i64 0}
!12 = !{!8, !4, i64 88}
!13 = !{!8, !4, i64 92}
!14 = !{!15, !4, i64 40}
!15 = !{!"", !9, i64 0, !9, i64 8, !9, i64 16, !9, i64 24, !10, i64 32, !4, i64 40, !4, i64 44, !10, i64 48, !10, i64 56, !10, i64 64, !4, i64 72, !4, i64 76, !4, i64 80, !4, i64 84, !4, i64 88, !4, i64 92}
!16 = !{!15, !4, i64 72}
!17 = !{!15, !4, i64 76}
!18 = !{!15, !4, i64 80}
!19 = !{!8, !9, i64 24}
!20 = !{!8, !9, i64 0}
!21 = !{!9, !9, i64 0}
!22 = !{!10, !10, i64 0}
!23 = !{!24, !4, i64 0}
!24 = !{!"", !4, i64 0, !4, i64 4, !4, i64 8, !4, i64 12, !4, i64 16, !4, i64 20, !10, i64 24, !10, i64 32, !10, i64 40, !10, i64 48, !10, i64 56, !10, i64 64, !10, i64 72, !10, i64 80, !10, i64 88, !10, i64 96, !11, i64 104, !10, i64 112, !10, i64 120, !10, i64 128, !10, i64 136, !10, i64 144, !10, i64 152, !4, i64 160}
!25 = !{!24, !4, i64 4}
!26 = !{!24, !4, i64 160}
!27 = !{!24, !10, i64 24}
!28 = !{!24, !10, i64 136}
!29 = !{!24, !10, i64 144}
!30 = !{!24, !10, i64 152}
!31 = !{!24, !10, i64 40}
!32 = !{!24, !10, i64 48}
!33 = !{!24, !10, i64 56}
!34 = !{!24, !10, i64 64}
!35 = !{!24, !10, i64 80}
!36 = !{!24, !10, i64 72}
!37 = distinct !{!37, !38}
!38 = !{!"llvm.loop.unroll.disable"}
!39 = !{!24, !10, i64 88}
!40 = !{!8, !4, i64 48}
!41 = !{!24, !10, i64 96}
!42 = !{!24, !10, i64 32}
!43 = !{!24, !11, i64 104}
!44 = !{!24, !10, i64 112}
!45 = !{!24, !10, i64 120}
!46 = !{!24, !10, i64 128}
!47 = !{!15, !10, i64 48}
!48 = !{!15, !10, i64 56}
!49 = !{!15, !10, i64 64}
!50 = !{!15, !10, i64 32}
!51 = !{!8, !4, i64 80}
!52 = distinct !{!52, !38}
!53 = !{!8, !4, i64 96}
!54 = !{!8, !4, i64 72}
!55 = !{!8, !9, i64 16}
!56 = !{!11, !11, i64 0}
!57 = !{!24, !4, i64 8}
!58 = !{!24, !4, i64 12}
!59 = !{!24, !4, i64 16}
!60 = !{!24, !4, i64 20}
!61 = distinct !{!61, !38}
!62 = distinct !{!62, !38}
!63 = !{!64}
!64 = distinct !{!64, !65}
!65 = distinct !{!65, !"LVerDomain"}
!66 = !{!67}
!67 = distinct !{!67, !65}
!68 = distinct !{!68, !69}
!69 = !{!"llvm.loop.isvectorized", i32 1}
!70 = distinct !{!70, !38}
!71 = distinct !{!71, !38}
!72 = distinct !{!72, !69}
!73 = distinct !{!73, !38}
