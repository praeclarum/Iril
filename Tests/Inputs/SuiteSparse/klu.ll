; ModuleID = 'klu.c'
source_filename = "klu.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i64 @klu_kernel_factor(i32, i32*, i32*, double*, i32*, double, double** nocapture, double*, i32*, i32*, i32*, i32*, i32*, i32*, i32*, double*, i32*, i32, i32*, double*, i32*, i32*, double*, %struct.klu_common_struct*) local_unnamed_addr #0 {
  %25 = alloca double*, align 8
  %26 = bitcast double** %25 to i8*
  call void @llvm.lifetime.start.p0i8(i64 8, i8* nonnull %26) #5
  %27 = icmp sgt i32 %0, 1
  %28 = select i1 %27, i32 %0, i32 1
  %29 = fcmp ugt double %5, 0.000000e+00
  br i1 %29, label %30, label %32

; <label>:30:                                     ; preds = %24
  %31 = sitofp i32 %28 to double
  br label %48

; <label>:32:                                     ; preds = %24
  %33 = add nsw i32 %28, %17
  %34 = sext i32 %33 to i64
  %35 = getelementptr inbounds i32, i32* %1, i64 %34
  %36 = load i32, i32* %35, align 4, !tbaa !3
  %37 = sext i32 %17 to i64
  %38 = getelementptr inbounds i32, i32* %1, i64 %37
  %39 = load i32, i32* %38, align 4, !tbaa !3
  %40 = sub nsw i32 %36, %39
  %41 = fsub double -0.000000e+00, %5
  %42 = fcmp ogt double %41, 1.000000e+00
  %43 = select i1 %42, double %41, double 1.000000e+00
  %44 = sitofp i32 %40 to double
  %45 = fmul double %43, %44
  %46 = sitofp i32 %28 to double
  %47 = fadd double %45, %46
  br label %48

; <label>:48:                                     ; preds = %30, %32
  %49 = phi double [ %31, %30 ], [ %46, %32 ]
  %50 = phi double [ %5, %30 ], [ %47, %32 ]
  %51 = fptosi double %50 to i32
  %52 = add nuw nsw i32 %28, 1
  %53 = icmp sgt i32 %52, %51
  %54 = select i1 %53, i32 %52, i32 %51
  %55 = fmul double %49, %49
  %56 = fadd double %55, %49
  %57 = fmul double %56, 5.000000e-01
  %58 = fcmp olt double %57, 0x41DFFFFFFFC00000
  %59 = select i1 %58, double %57, double 0x41DFFFFFFFC00000
  %60 = sitofp i32 %54 to double
  %61 = fcmp olt double %59, %60
  %62 = select i1 %61, double %59, double %60
  %63 = fptosi double %62 to i32
  store double* null, double** %6, align 8, !tbaa !7
  %64 = zext i32 %28 to i64
  %65 = getelementptr inbounds i32, i32* %16, i64 %64
  %66 = getelementptr inbounds i32, i32* %65, i64 %64
  %67 = getelementptr inbounds i32, i32* %66, i64 %64
  %68 = getelementptr inbounds i32, i32* %67, i64 %64
  %69 = sitofp i32 %63 to double
  %70 = insertelement <2 x double> undef, double %69, i32 0
  %71 = shufflevector <2 x double> %70, <2 x double> undef, <2 x i32> zeroinitializer
  %72 = fmul <2 x double> %71, <double 4.000000e+00, double 8.000000e+00>
  %73 = fmul <2 x double> %72, <double 1.250000e-01, double 1.250000e-01>
  %74 = call <2 x double> @llvm.ceil.v2f64(<2 x double> %73)
  %75 = extractelement <2 x double> %74, i32 0
  %76 = extractelement <2 x double> %74, i32 1
  %77 = fadd double %75, %76
  %78 = fadd double %75, %77
  %79 = fadd double %76, %78
  %80 = fptoui double %79 to i64
  %81 = fmul double %79, 0x3FF0000002AF31DC
  %82 = fcmp ole double %81, 0x41DFFFFFFFC00000
  %83 = fcmp ord double %79, 0.000000e+00
  %84 = and i1 %83, %82
  br i1 %84, label %86, label %85

; <label>:85:                                     ; preds = %48
  store double* null, double** %25, align 8, !tbaa !7
  br label %90

; <label>:86:                                     ; preds = %48
  %87 = tail call i8* @klu_malloc(i64 %80, i64 8, %struct.klu_common_struct* %23) #5
  %88 = bitcast double** %25 to i8**
  store i8* %87, i8** %88, align 8, !tbaa !7
  %89 = icmp eq i8* %87, null
  br i1 %89, label %90, label %92

; <label>:90:                                     ; preds = %85, %86
  %91 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %23, i64 0, i32 11
  store i32 -2, i32* %91, align 4, !tbaa !9
  br label %108

; <label>:92:                                     ; preds = %86
  %93 = call i64 @klu_kernel(i32 %28, i32* %1, i32* %2, double* %3, i32* %4, i64 %80, i32* %16, i32* %12, double** nonnull %25, double* %7, i32* %8, i32* %9, i32* %10, i32* %11, i32* %13, i32* %14, double* %15, i32* %65, i32* %66, i32* %68, i32* %67, i32 %17, i32* %18, double* %19, i32* %20, i32* %21, double* %22, %struct.klu_common_struct* %23) #5
  %94 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %23, i64 0, i32 11
  %95 = load i32, i32* %94, align 4, !tbaa !9
  %96 = icmp slt i32 %95, 0
  br i1 %96, label %100, label %97

; <label>:97:                                     ; preds = %92
  %98 = bitcast double** %25 to i64*
  %99 = load i64, i64* %98, align 8, !tbaa !7
  br label %104

; <label>:100:                                    ; preds = %92
  %101 = load i8*, i8** %88, align 8, !tbaa !7
  %102 = call i8* @klu_free(i8* %101, i64 %93, i64 8, %struct.klu_common_struct* nonnull %23) #5
  store i8* %102, i8** %88, align 8, !tbaa !7
  %103 = ptrtoint i8* %102 to i64
  br label %104

; <label>:104:                                    ; preds = %97, %100
  %105 = phi i64 [ %103, %100 ], [ %99, %97 ]
  %106 = phi i64 [ 0, %100 ], [ %93, %97 ]
  %107 = bitcast double** %6 to i64*
  store i64 %105, i64* %107, align 8, !tbaa !7
  br label %108

; <label>:108:                                    ; preds = %104, %90
  %109 = phi i64 [ 0, %90 ], [ %106, %104 ]
  call void @llvm.lifetime.end.p0i8(i64 8, i8* nonnull %26) #5
  ret i64 %109
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

declare i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

declare i64 @klu_kernel(i32, i32*, i32*, double*, i32*, i64, i32*, i32*, double**, double*, i32*, i32*, i32*, i32*, i32*, i32*, double*, i32*, i32*, i32*, i32*, i32, i32*, double*, i32*, i32*, double*, %struct.klu_common_struct*) local_unnamed_addr #2

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #2

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: norecurse nounwind ssp uwtable
define void @klu_lsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #3 {
  switch i32 %4, label %294 [
    i32 1, label %7
    i32 2, label %75
    i32 3, label %160
    i32 4, label %223
  ]

; <label>:7:                                      ; preds = %6
  %8 = icmp sgt i32 %0, 0
  br i1 %8, label %9, label %294

; <label>:9:                                      ; preds = %7
  %10 = zext i32 %0 to i64
  br label %11

; <label>:11:                                     ; preds = %72, %9
  %12 = phi i64 [ 0, %9 ], [ %73, %72 ]
  %13 = getelementptr inbounds double, double* %5, i64 %12
  %14 = load double, double* %13, align 8, !tbaa !13
  %15 = getelementptr inbounds i32, i32* %1, i64 %12
  %16 = load i32, i32* %15, align 4, !tbaa !3
  %17 = sext i32 %16 to i64
  %18 = getelementptr inbounds double, double* %3, i64 %17
  %19 = getelementptr inbounds i32, i32* %2, i64 %12
  %20 = load i32, i32* %19, align 4, !tbaa !3
  %21 = bitcast double* %18 to i32*
  %22 = sext i32 %20 to i64
  %23 = shl nsw i64 %22, 2
  %24 = add nsw i64 %23, 7
  %25 = lshr i64 %24, 3
  %26 = getelementptr inbounds double, double* %18, i64 %25
  %27 = icmp sgt i32 %20, 0
  br i1 %27, label %28, label %72

; <label>:28:                                     ; preds = %11
  %29 = zext i32 %20 to i64
  %30 = and i64 %29, 1
  %31 = icmp eq i32 %20, 1
  br i1 %31, label %59, label %32

; <label>:32:                                     ; preds = %28
  %33 = sub nsw i64 %29, %30
  br label %34

; <label>:34:                                     ; preds = %34, %32
  %35 = phi i64 [ 0, %32 ], [ %56, %34 ]
  %36 = phi i64 [ %33, %32 ], [ %57, %34 ]
  %37 = getelementptr inbounds double, double* %26, i64 %35
  %38 = load double, double* %37, align 8, !tbaa !13
  %39 = fmul double %14, %38
  %40 = getelementptr inbounds i32, i32* %21, i64 %35
  %41 = load i32, i32* %40, align 4, !tbaa !3
  %42 = sext i32 %41 to i64
  %43 = getelementptr inbounds double, double* %5, i64 %42
  %44 = load double, double* %43, align 8, !tbaa !13
  %45 = fsub double %44, %39
  store double %45, double* %43, align 8, !tbaa !13
  %46 = or i64 %35, 1
  %47 = getelementptr inbounds double, double* %26, i64 %46
  %48 = load double, double* %47, align 8, !tbaa !13
  %49 = fmul double %14, %48
  %50 = getelementptr inbounds i32, i32* %21, i64 %46
  %51 = load i32, i32* %50, align 4, !tbaa !3
  %52 = sext i32 %51 to i64
  %53 = getelementptr inbounds double, double* %5, i64 %52
  %54 = load double, double* %53, align 8, !tbaa !13
  %55 = fsub double %54, %49
  store double %55, double* %53, align 8, !tbaa !13
  %56 = add nuw nsw i64 %35, 2
  %57 = add i64 %36, -2
  %58 = icmp eq i64 %57, 0
  br i1 %58, label %59, label %34

; <label>:59:                                     ; preds = %34, %28
  %60 = phi i64 [ 0, %28 ], [ %56, %34 ]
  %61 = icmp eq i64 %30, 0
  br i1 %61, label %72, label %62

; <label>:62:                                     ; preds = %59
  %63 = getelementptr inbounds double, double* %26, i64 %60
  %64 = load double, double* %63, align 8, !tbaa !13
  %65 = fmul double %14, %64
  %66 = getelementptr inbounds i32, i32* %21, i64 %60
  %67 = load i32, i32* %66, align 4, !tbaa !3
  %68 = sext i32 %67 to i64
  %69 = getelementptr inbounds double, double* %5, i64 %68
  %70 = load double, double* %69, align 8, !tbaa !13
  %71 = fsub double %70, %65
  store double %71, double* %69, align 8, !tbaa !13
  br label %72

; <label>:72:                                     ; preds = %62, %59, %11
  %73 = add nuw nsw i64 %12, 1
  %74 = icmp eq i64 %73, %10
  br i1 %74, label %294, label %11

; <label>:75:                                     ; preds = %6
  %76 = icmp sgt i32 %0, 0
  br i1 %76, label %77, label %294

; <label>:77:                                     ; preds = %75
  %78 = zext i32 %0 to i64
  br label %79

; <label>:79:                                     ; preds = %157, %77
  %80 = phi i64 [ 0, %77 ], [ %158, %157 ]
  %81 = shl nuw nsw i64 %80, 1
  %82 = getelementptr inbounds double, double* %5, i64 %81
  %83 = bitcast double* %82 to <2 x double>*
  %84 = load <2 x double>, <2 x double>* %83, align 8, !tbaa !13
  %85 = getelementptr inbounds i32, i32* %1, i64 %80
  %86 = load i32, i32* %85, align 4, !tbaa !3
  %87 = sext i32 %86 to i64
  %88 = getelementptr inbounds double, double* %3, i64 %87
  %89 = getelementptr inbounds i32, i32* %2, i64 %80
  %90 = load i32, i32* %89, align 4, !tbaa !3
  %91 = bitcast double* %88 to i32*
  %92 = sext i32 %90 to i64
  %93 = shl nsw i64 %92, 2
  %94 = add nsw i64 %93, 7
  %95 = lshr i64 %94, 3
  %96 = getelementptr inbounds double, double* %88, i64 %95
  %97 = icmp sgt i32 %90, 0
  br i1 %97, label %98, label %157

; <label>:98:                                     ; preds = %79
  %99 = zext i32 %90 to i64
  %100 = and i64 %99, 1
  %101 = icmp eq i32 %90, 1
  br i1 %101, label %139, label %102

; <label>:102:                                    ; preds = %98
  %103 = sub nsw i64 %99, %100
  br label %104

; <label>:104:                                    ; preds = %104, %102
  %105 = phi i64 [ 0, %102 ], [ %136, %104 ]
  %106 = phi i64 [ %103, %102 ], [ %137, %104 ]
  %107 = getelementptr inbounds i32, i32* %91, i64 %105
  %108 = load i32, i32* %107, align 4, !tbaa !3
  %109 = getelementptr inbounds double, double* %96, i64 %105
  %110 = load double, double* %109, align 8, !tbaa !13
  %111 = shl nsw i32 %108, 1
  %112 = sext i32 %111 to i64
  %113 = getelementptr inbounds double, double* %5, i64 %112
  %114 = insertelement <2 x double> undef, double %110, i32 0
  %115 = shufflevector <2 x double> %114, <2 x double> undef, <2 x i32> zeroinitializer
  %116 = fmul <2 x double> %84, %115
  %117 = bitcast double* %113 to <2 x double>*
  %118 = load <2 x double>, <2 x double>* %117, align 8, !tbaa !13
  %119 = fsub <2 x double> %118, %116
  %120 = bitcast double* %113 to <2 x double>*
  store <2 x double> %119, <2 x double>* %120, align 8, !tbaa !13
  %121 = or i64 %105, 1
  %122 = getelementptr inbounds i32, i32* %91, i64 %121
  %123 = load i32, i32* %122, align 4, !tbaa !3
  %124 = getelementptr inbounds double, double* %96, i64 %121
  %125 = load double, double* %124, align 8, !tbaa !13
  %126 = shl nsw i32 %123, 1
  %127 = sext i32 %126 to i64
  %128 = getelementptr inbounds double, double* %5, i64 %127
  %129 = insertelement <2 x double> undef, double %125, i32 0
  %130 = shufflevector <2 x double> %129, <2 x double> undef, <2 x i32> zeroinitializer
  %131 = fmul <2 x double> %84, %130
  %132 = bitcast double* %128 to <2 x double>*
  %133 = load <2 x double>, <2 x double>* %132, align 8, !tbaa !13
  %134 = fsub <2 x double> %133, %131
  %135 = bitcast double* %128 to <2 x double>*
  store <2 x double> %134, <2 x double>* %135, align 8, !tbaa !13
  %136 = add nuw nsw i64 %105, 2
  %137 = add i64 %106, -2
  %138 = icmp eq i64 %137, 0
  br i1 %138, label %139, label %104

; <label>:139:                                    ; preds = %104, %98
  %140 = phi i64 [ 0, %98 ], [ %136, %104 ]
  %141 = icmp eq i64 %100, 0
  br i1 %141, label %157, label %142

; <label>:142:                                    ; preds = %139
  %143 = getelementptr inbounds i32, i32* %91, i64 %140
  %144 = load i32, i32* %143, align 4, !tbaa !3
  %145 = getelementptr inbounds double, double* %96, i64 %140
  %146 = load double, double* %145, align 8, !tbaa !13
  %147 = shl nsw i32 %144, 1
  %148 = sext i32 %147 to i64
  %149 = getelementptr inbounds double, double* %5, i64 %148
  %150 = insertelement <2 x double> undef, double %146, i32 0
  %151 = shufflevector <2 x double> %150, <2 x double> undef, <2 x i32> zeroinitializer
  %152 = fmul <2 x double> %84, %151
  %153 = bitcast double* %149 to <2 x double>*
  %154 = load <2 x double>, <2 x double>* %153, align 8, !tbaa !13
  %155 = fsub <2 x double> %154, %152
  %156 = bitcast double* %149 to <2 x double>*
  store <2 x double> %155, <2 x double>* %156, align 8, !tbaa !13
  br label %157

; <label>:157:                                    ; preds = %142, %139, %79
  %158 = add nuw nsw i64 %80, 1
  %159 = icmp eq i64 %158, %78
  br i1 %159, label %294, label %79

; <label>:160:                                    ; preds = %6
  %161 = icmp sgt i32 %0, 0
  br i1 %161, label %162, label %294

; <label>:162:                                    ; preds = %160
  %163 = zext i32 %0 to i64
  br label %164

; <label>:164:                                    ; preds = %220, %162
  %165 = phi i64 [ 0, %162 ], [ %221, %220 ]
  %166 = trunc i64 %165 to i32
  %167 = mul nsw i32 %166, 3
  %168 = zext i32 %167 to i64
  %169 = getelementptr inbounds double, double* %5, i64 %168
  %170 = load double, double* %169, align 8, !tbaa !13
  %171 = add nuw nsw i32 %167, 1
  %172 = zext i32 %171 to i64
  %173 = getelementptr inbounds double, double* %5, i64 %172
  %174 = load double, double* %173, align 8, !tbaa !13
  %175 = add nuw nsw i32 %167, 2
  %176 = zext i32 %175 to i64
  %177 = getelementptr inbounds double, double* %5, i64 %176
  %178 = load double, double* %177, align 8, !tbaa !13
  %179 = getelementptr inbounds i32, i32* %1, i64 %165
  %180 = load i32, i32* %179, align 4, !tbaa !3
  %181 = sext i32 %180 to i64
  %182 = getelementptr inbounds double, double* %3, i64 %181
  %183 = getelementptr inbounds i32, i32* %2, i64 %165
  %184 = load i32, i32* %183, align 4, !tbaa !3
  %185 = bitcast double* %182 to i32*
  %186 = sext i32 %184 to i64
  %187 = shl nsw i64 %186, 2
  %188 = add nsw i64 %187, 7
  %189 = lshr i64 %188, 3
  %190 = getelementptr inbounds double, double* %182, i64 %189
  %191 = icmp sgt i32 %184, 0
  br i1 %191, label %192, label %220

; <label>:192:                                    ; preds = %164
  %193 = zext i32 %184 to i64
  br label %194

; <label>:194:                                    ; preds = %194, %192
  %195 = phi i64 [ 0, %192 ], [ %218, %194 ]
  %196 = getelementptr inbounds i32, i32* %185, i64 %195
  %197 = load i32, i32* %196, align 4, !tbaa !3
  %198 = getelementptr inbounds double, double* %190, i64 %195
  %199 = load double, double* %198, align 8, !tbaa !13
  %200 = fmul double %170, %199
  %201 = mul nsw i32 %197, 3
  %202 = sext i32 %201 to i64
  %203 = getelementptr inbounds double, double* %5, i64 %202
  %204 = load double, double* %203, align 8, !tbaa !13
  %205 = fsub double %204, %200
  store double %205, double* %203, align 8, !tbaa !13
  %206 = fmul double %174, %199
  %207 = add nsw i32 %201, 1
  %208 = sext i32 %207 to i64
  %209 = getelementptr inbounds double, double* %5, i64 %208
  %210 = load double, double* %209, align 8, !tbaa !13
  %211 = fsub double %210, %206
  store double %211, double* %209, align 8, !tbaa !13
  %212 = fmul double %178, %199
  %213 = add nsw i32 %201, 2
  %214 = sext i32 %213 to i64
  %215 = getelementptr inbounds double, double* %5, i64 %214
  %216 = load double, double* %215, align 8, !tbaa !13
  %217 = fsub double %216, %212
  store double %217, double* %215, align 8, !tbaa !13
  %218 = add nuw nsw i64 %195, 1
  %219 = icmp eq i64 %218, %193
  br i1 %219, label %220, label %194

; <label>:220:                                    ; preds = %194, %164
  %221 = add nuw nsw i64 %165, 1
  %222 = icmp eq i64 %221, %163
  br i1 %222, label %294, label %164

; <label>:223:                                    ; preds = %6
  %224 = icmp sgt i32 %0, 0
  br i1 %224, label %225, label %294

; <label>:225:                                    ; preds = %223
  %226 = zext i32 %0 to i64
  br label %227

; <label>:227:                                    ; preds = %291, %225
  %228 = phi i64 [ 0, %225 ], [ %292, %291 ]
  %229 = trunc i64 %228 to i32
  %230 = shl nsw i32 %229, 2
  %231 = getelementptr inbounds i32, i32* %1, i64 %228
  %232 = load i32, i32* %231, align 4, !tbaa !3
  %233 = sext i32 %232 to i64
  %234 = getelementptr inbounds double, double* %3, i64 %233
  %235 = getelementptr inbounds i32, i32* %2, i64 %228
  %236 = load i32, i32* %235, align 4, !tbaa !3
  %237 = bitcast double* %234 to i32*
  %238 = sext i32 %236 to i64
  %239 = shl nsw i64 %238, 2
  %240 = add nsw i64 %239, 7
  %241 = lshr i64 %240, 3
  %242 = getelementptr inbounds double, double* %234, i64 %241
  %243 = icmp sgt i32 %236, 0
  br i1 %243, label %244, label %291

; <label>:244:                                    ; preds = %227
  %245 = or i32 %230, 3
  %246 = zext i32 %245 to i64
  %247 = getelementptr inbounds double, double* %5, i64 %246
  %248 = load double, double* %247, align 8, !tbaa !13
  %249 = or i32 %230, 2
  %250 = zext i32 %249 to i64
  %251 = getelementptr inbounds double, double* %5, i64 %250
  %252 = load double, double* %251, align 8, !tbaa !13
  %253 = or i32 %230, 1
  %254 = zext i32 %253 to i64
  %255 = getelementptr inbounds double, double* %5, i64 %254
  %256 = load double, double* %255, align 8, !tbaa !13
  %257 = zext i32 %230 to i64
  %258 = getelementptr inbounds double, double* %5, i64 %257
  %259 = load double, double* %258, align 8, !tbaa !13
  %260 = zext i32 %236 to i64
  %261 = insertelement <2 x double> undef, double %259, i32 0
  %262 = insertelement <2 x double> %261, double %256, i32 1
  %263 = insertelement <2 x double> undef, double %252, i32 0
  %264 = insertelement <2 x double> %263, double %248, i32 1
  br label %265

; <label>:265:                                    ; preds = %265, %244
  %266 = phi i64 [ 0, %244 ], [ %289, %265 ]
  %267 = getelementptr inbounds i32, i32* %237, i64 %266
  %268 = load i32, i32* %267, align 4, !tbaa !3
  %269 = getelementptr inbounds double, double* %242, i64 %266
  %270 = load double, double* %269, align 8, !tbaa !13
  %271 = shl nsw i32 %268, 2
  %272 = sext i32 %271 to i64
  %273 = getelementptr inbounds double, double* %5, i64 %272
  %274 = insertelement <2 x double> undef, double %270, i32 0
  %275 = shufflevector <2 x double> %274, <2 x double> undef, <2 x i32> zeroinitializer
  %276 = fmul <2 x double> %262, %275
  %277 = bitcast double* %273 to <2 x double>*
  %278 = load <2 x double>, <2 x double>* %277, align 8, !tbaa !13
  %279 = fsub <2 x double> %278, %276
  %280 = bitcast double* %273 to <2 x double>*
  store <2 x double> %279, <2 x double>* %280, align 8, !tbaa !13
  %281 = or i32 %271, 2
  %282 = sext i32 %281 to i64
  %283 = getelementptr inbounds double, double* %5, i64 %282
  %284 = fmul <2 x double> %264, %275
  %285 = bitcast double* %283 to <2 x double>*
  %286 = load <2 x double>, <2 x double>* %285, align 8, !tbaa !13
  %287 = fsub <2 x double> %286, %284
  %288 = bitcast double* %283 to <2 x double>*
  store <2 x double> %287, <2 x double>* %288, align 8, !tbaa !13
  %289 = add nuw nsw i64 %266, 1
  %290 = icmp eq i64 %289, %260
  br i1 %290, label %291, label %265

; <label>:291:                                    ; preds = %265, %227
  %292 = add nuw nsw i64 %228, 1
  %293 = icmp eq i64 %292, %226
  br i1 %293, label %294, label %227

; <label>:294:                                    ; preds = %291, %220, %157, %72, %223, %160, %75, %7, %6
  ret void
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @klu_usolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #3 {
  switch i32 %5, label %327 [
    i32 1, label %8
    i32 2, label %79
    i32 3, label %179
    i32 4, label %245
  ]

; <label>:8:                                      ; preds = %7
  %9 = icmp sgt i32 %0, 0
  br i1 %9, label %10, label %327

; <label>:10:                                     ; preds = %8
  %11 = sext i32 %0 to i64
  br label %12

; <label>:12:                                     ; preds = %10, %77
  %13 = phi i64 [ %11, %10 ], [ %14, %77 ]
  %14 = add nsw i64 %13, -1
  %15 = getelementptr inbounds i32, i32* %1, i64 %14
  %16 = load i32, i32* %15, align 4, !tbaa !3
  %17 = sext i32 %16 to i64
  %18 = getelementptr inbounds double, double* %3, i64 %17
  %19 = getelementptr inbounds i32, i32* %2, i64 %14
  %20 = load i32, i32* %19, align 4, !tbaa !3
  %21 = bitcast double* %18 to i32*
  %22 = sext i32 %20 to i64
  %23 = shl nsw i64 %22, 2
  %24 = add nsw i64 %23, 7
  %25 = lshr i64 %24, 3
  %26 = getelementptr inbounds double, double* %18, i64 %25
  %27 = getelementptr inbounds double, double* %6, i64 %14
  %28 = load double, double* %27, align 8, !tbaa !13
  %29 = getelementptr inbounds double, double* %4, i64 %14
  %30 = load double, double* %29, align 8, !tbaa !13
  %31 = fdiv double %28, %30
  store double %31, double* %27, align 8, !tbaa !13
  %32 = icmp sgt i32 %20, 0
  br i1 %32, label %33, label %77

; <label>:33:                                     ; preds = %12
  %34 = zext i32 %20 to i64
  %35 = and i64 %34, 1
  %36 = icmp eq i32 %20, 1
  br i1 %36, label %64, label %37

; <label>:37:                                     ; preds = %33
  %38 = sub nsw i64 %34, %35
  br label %39

; <label>:39:                                     ; preds = %39, %37
  %40 = phi i64 [ 0, %37 ], [ %61, %39 ]
  %41 = phi i64 [ %38, %37 ], [ %62, %39 ]
  %42 = getelementptr inbounds double, double* %26, i64 %40
  %43 = load double, double* %42, align 8, !tbaa !13
  %44 = fmul double %31, %43
  %45 = getelementptr inbounds i32, i32* %21, i64 %40
  %46 = load i32, i32* %45, align 4, !tbaa !3
  %47 = sext i32 %46 to i64
  %48 = getelementptr inbounds double, double* %6, i64 %47
  %49 = load double, double* %48, align 8, !tbaa !13
  %50 = fsub double %49, %44
  store double %50, double* %48, align 8, !tbaa !13
  %51 = or i64 %40, 1
  %52 = getelementptr inbounds double, double* %26, i64 %51
  %53 = load double, double* %52, align 8, !tbaa !13
  %54 = fmul double %31, %53
  %55 = getelementptr inbounds i32, i32* %21, i64 %51
  %56 = load i32, i32* %55, align 4, !tbaa !3
  %57 = sext i32 %56 to i64
  %58 = getelementptr inbounds double, double* %6, i64 %57
  %59 = load double, double* %58, align 8, !tbaa !13
  %60 = fsub double %59, %54
  store double %60, double* %58, align 8, !tbaa !13
  %61 = add nuw nsw i64 %40, 2
  %62 = add i64 %41, -2
  %63 = icmp eq i64 %62, 0
  br i1 %63, label %64, label %39

; <label>:64:                                     ; preds = %39, %33
  %65 = phi i64 [ 0, %33 ], [ %61, %39 ]
  %66 = icmp eq i64 %35, 0
  br i1 %66, label %77, label %67

; <label>:67:                                     ; preds = %64
  %68 = getelementptr inbounds double, double* %26, i64 %65
  %69 = load double, double* %68, align 8, !tbaa !13
  %70 = fmul double %31, %69
  %71 = getelementptr inbounds i32, i32* %21, i64 %65
  %72 = load i32, i32* %71, align 4, !tbaa !3
  %73 = sext i32 %72 to i64
  %74 = getelementptr inbounds double, double* %6, i64 %73
  %75 = load double, double* %74, align 8, !tbaa !13
  %76 = fsub double %75, %70
  store double %76, double* %74, align 8, !tbaa !13
  br label %77

; <label>:77:                                     ; preds = %67, %64, %12
  %78 = icmp sgt i64 %13, 1
  br i1 %78, label %12, label %327

; <label>:79:                                     ; preds = %7
  %80 = icmp sgt i32 %0, 0
  br i1 %80, label %81, label %327

; <label>:81:                                     ; preds = %79
  %82 = sext i32 %0 to i64
  br label %83

; <label>:83:                                     ; preds = %81, %177
  %84 = phi i64 [ %82, %81 ], [ %86, %177 ]
  %85 = phi i32 [ %0, %81 ], [ %87, %177 ]
  %86 = add nsw i64 %84, -1
  %87 = add nsw i32 %85, -1
  %88 = getelementptr inbounds i32, i32* %1, i64 %86
  %89 = load i32, i32* %88, align 4, !tbaa !3
  %90 = sext i32 %89 to i64
  %91 = getelementptr inbounds double, double* %3, i64 %90
  %92 = getelementptr inbounds i32, i32* %2, i64 %86
  %93 = load i32, i32* %92, align 4, !tbaa !3
  %94 = bitcast double* %91 to i32*
  %95 = sext i32 %93 to i64
  %96 = shl nsw i64 %95, 2
  %97 = add nsw i64 %96, 7
  %98 = lshr i64 %97, 3
  %99 = getelementptr inbounds double, double* %91, i64 %98
  %100 = getelementptr inbounds double, double* %4, i64 %86
  %101 = load double, double* %100, align 8, !tbaa !13
  %102 = shl nsw i32 %87, 1
  %103 = sext i32 %102 to i64
  %104 = getelementptr inbounds double, double* %6, i64 %103
  %105 = load double, double* %104, align 8, !tbaa !13
  %106 = or i32 %102, 1
  %107 = sext i32 %106 to i64
  %108 = getelementptr inbounds double, double* %6, i64 %107
  %109 = load double, double* %108, align 8, !tbaa !13
  %110 = insertelement <2 x double> undef, double %105, i32 0
  %111 = insertelement <2 x double> %110, double %109, i32 1
  %112 = insertelement <2 x double> undef, double %101, i32 0
  %113 = shufflevector <2 x double> %112, <2 x double> undef, <2 x i32> zeroinitializer
  %114 = fdiv <2 x double> %111, %113
  %115 = extractelement <2 x double> %114, i32 0
  store double %115, double* %104, align 8, !tbaa !13
  %116 = extractelement <2 x double> %114, i32 1
  store double %116, double* %108, align 8, !tbaa !13
  %117 = icmp sgt i32 %93, 0
  br i1 %117, label %118, label %177

; <label>:118:                                    ; preds = %83
  %119 = zext i32 %93 to i64
  %120 = and i64 %119, 1
  %121 = icmp eq i32 %93, 1
  br i1 %121, label %159, label %122

; <label>:122:                                    ; preds = %118
  %123 = sub nsw i64 %119, %120
  br label %124

; <label>:124:                                    ; preds = %124, %122
  %125 = phi i64 [ 0, %122 ], [ %156, %124 ]
  %126 = phi i64 [ %123, %122 ], [ %157, %124 ]
  %127 = getelementptr inbounds i32, i32* %94, i64 %125
  %128 = load i32, i32* %127, align 4, !tbaa !3
  %129 = getelementptr inbounds double, double* %99, i64 %125
  %130 = load double, double* %129, align 8, !tbaa !13
  %131 = shl nsw i32 %128, 1
  %132 = sext i32 %131 to i64
  %133 = getelementptr inbounds double, double* %6, i64 %132
  %134 = insertelement <2 x double> undef, double %130, i32 0
  %135 = shufflevector <2 x double> %134, <2 x double> undef, <2 x i32> zeroinitializer
  %136 = fmul <2 x double> %114, %135
  %137 = bitcast double* %133 to <2 x double>*
  %138 = load <2 x double>, <2 x double>* %137, align 8, !tbaa !13
  %139 = fsub <2 x double> %138, %136
  %140 = bitcast double* %133 to <2 x double>*
  store <2 x double> %139, <2 x double>* %140, align 8, !tbaa !13
  %141 = or i64 %125, 1
  %142 = getelementptr inbounds i32, i32* %94, i64 %141
  %143 = load i32, i32* %142, align 4, !tbaa !3
  %144 = getelementptr inbounds double, double* %99, i64 %141
  %145 = load double, double* %144, align 8, !tbaa !13
  %146 = shl nsw i32 %143, 1
  %147 = sext i32 %146 to i64
  %148 = getelementptr inbounds double, double* %6, i64 %147
  %149 = insertelement <2 x double> undef, double %145, i32 0
  %150 = shufflevector <2 x double> %149, <2 x double> undef, <2 x i32> zeroinitializer
  %151 = fmul <2 x double> %114, %150
  %152 = bitcast double* %148 to <2 x double>*
  %153 = load <2 x double>, <2 x double>* %152, align 8, !tbaa !13
  %154 = fsub <2 x double> %153, %151
  %155 = bitcast double* %148 to <2 x double>*
  store <2 x double> %154, <2 x double>* %155, align 8, !tbaa !13
  %156 = add nuw nsw i64 %125, 2
  %157 = add i64 %126, -2
  %158 = icmp eq i64 %157, 0
  br i1 %158, label %159, label %124

; <label>:159:                                    ; preds = %124, %118
  %160 = phi i64 [ 0, %118 ], [ %156, %124 ]
  %161 = icmp eq i64 %120, 0
  br i1 %161, label %177, label %162

; <label>:162:                                    ; preds = %159
  %163 = getelementptr inbounds i32, i32* %94, i64 %160
  %164 = load i32, i32* %163, align 4, !tbaa !3
  %165 = getelementptr inbounds double, double* %99, i64 %160
  %166 = load double, double* %165, align 8, !tbaa !13
  %167 = shl nsw i32 %164, 1
  %168 = sext i32 %167 to i64
  %169 = getelementptr inbounds double, double* %6, i64 %168
  %170 = insertelement <2 x double> undef, double %166, i32 0
  %171 = shufflevector <2 x double> %170, <2 x double> undef, <2 x i32> zeroinitializer
  %172 = fmul <2 x double> %114, %171
  %173 = bitcast double* %169 to <2 x double>*
  %174 = load <2 x double>, <2 x double>* %173, align 8, !tbaa !13
  %175 = fsub <2 x double> %174, %172
  %176 = bitcast double* %169 to <2 x double>*
  store <2 x double> %175, <2 x double>* %176, align 8, !tbaa !13
  br label %177

; <label>:177:                                    ; preds = %162, %159, %83
  %178 = icmp sgt i64 %84, 1
  br i1 %178, label %83, label %327

; <label>:179:                                    ; preds = %7
  %180 = icmp sgt i32 %0, 0
  br i1 %180, label %181, label %327

; <label>:181:                                    ; preds = %179
  %182 = sext i32 %0 to i64
  br label %183

; <label>:183:                                    ; preds = %181, %243
  %184 = phi i64 [ %182, %181 ], [ %185, %243 ]
  %185 = add nsw i64 %184, -1
  %186 = getelementptr inbounds i32, i32* %1, i64 %185
  %187 = load i32, i32* %186, align 4, !tbaa !3
  %188 = sext i32 %187 to i64
  %189 = getelementptr inbounds double, double* %3, i64 %188
  %190 = getelementptr inbounds i32, i32* %2, i64 %185
  %191 = load i32, i32* %190, align 4, !tbaa !3
  %192 = bitcast double* %189 to i32*
  %193 = sext i32 %191 to i64
  %194 = shl nsw i64 %193, 2
  %195 = add nsw i64 %194, 7
  %196 = lshr i64 %195, 3
  %197 = getelementptr inbounds double, double* %189, i64 %196
  %198 = getelementptr inbounds double, double* %4, i64 %185
  %199 = load double, double* %198, align 8, !tbaa !13
  %200 = mul nsw i64 %185, 3
  %201 = getelementptr inbounds double, double* %6, i64 %200
  %202 = bitcast double* %201 to <2 x double>*
  %203 = load <2 x double>, <2 x double>* %202, align 8, !tbaa !13
  %204 = insertelement <2 x double> undef, double %199, i32 0
  %205 = shufflevector <2 x double> %204, <2 x double> undef, <2 x i32> zeroinitializer
  %206 = fdiv <2 x double> %203, %205
  %207 = add nsw i64 %200, 2
  %208 = getelementptr inbounds double, double* %6, i64 %207
  %209 = load double, double* %208, align 8, !tbaa !13
  %210 = fdiv double %209, %199
  %211 = bitcast double* %201 to <2 x double>*
  store <2 x double> %206, <2 x double>* %211, align 8, !tbaa !13
  store double %210, double* %208, align 8, !tbaa !13
  %212 = icmp sgt i32 %191, 0
  br i1 %212, label %213, label %243

; <label>:213:                                    ; preds = %183
  %214 = zext i32 %191 to i64
  %215 = extractelement <2 x double> %206, i32 0
  %216 = extractelement <2 x double> %206, i32 1
  br label %217

; <label>:217:                                    ; preds = %217, %213
  %218 = phi i64 [ 0, %213 ], [ %241, %217 ]
  %219 = getelementptr inbounds i32, i32* %192, i64 %218
  %220 = load i32, i32* %219, align 4, !tbaa !3
  %221 = getelementptr inbounds double, double* %197, i64 %218
  %222 = load double, double* %221, align 8, !tbaa !13
  %223 = fmul double %215, %222
  %224 = mul nsw i32 %220, 3
  %225 = sext i32 %224 to i64
  %226 = getelementptr inbounds double, double* %6, i64 %225
  %227 = load double, double* %226, align 8, !tbaa !13
  %228 = fsub double %227, %223
  store double %228, double* %226, align 8, !tbaa !13
  %229 = fmul double %216, %222
  %230 = add nsw i32 %224, 1
  %231 = sext i32 %230 to i64
  %232 = getelementptr inbounds double, double* %6, i64 %231
  %233 = load double, double* %232, align 8, !tbaa !13
  %234 = fsub double %233, %229
  store double %234, double* %232, align 8, !tbaa !13
  %235 = fmul double %210, %222
  %236 = add nsw i32 %224, 2
  %237 = sext i32 %236 to i64
  %238 = getelementptr inbounds double, double* %6, i64 %237
  %239 = load double, double* %238, align 8, !tbaa !13
  %240 = fsub double %239, %235
  store double %240, double* %238, align 8, !tbaa !13
  %241 = add nuw nsw i64 %218, 1
  %242 = icmp eq i64 %241, %214
  br i1 %242, label %243, label %217

; <label>:243:                                    ; preds = %217, %183
  %244 = icmp sgt i64 %184, 1
  br i1 %244, label %183, label %327

; <label>:245:                                    ; preds = %7
  %246 = icmp sgt i32 %0, 0
  br i1 %246, label %247, label %327

; <label>:247:                                    ; preds = %245
  %248 = sext i32 %0 to i64
  br label %249

; <label>:249:                                    ; preds = %247, %325
  %250 = phi i64 [ %248, %247 ], [ %252, %325 ]
  %251 = phi i32 [ %0, %247 ], [ %253, %325 ]
  %252 = add nsw i64 %250, -1
  %253 = add nsw i32 %251, -1
  %254 = getelementptr inbounds i32, i32* %1, i64 %252
  %255 = load i32, i32* %254, align 4, !tbaa !3
  %256 = sext i32 %255 to i64
  %257 = getelementptr inbounds double, double* %3, i64 %256
  %258 = getelementptr inbounds i32, i32* %2, i64 %252
  %259 = load i32, i32* %258, align 4, !tbaa !3
  %260 = bitcast double* %257 to i32*
  %261 = sext i32 %259 to i64
  %262 = shl nsw i64 %261, 2
  %263 = add nsw i64 %262, 7
  %264 = lshr i64 %263, 3
  %265 = getelementptr inbounds double, double* %257, i64 %264
  %266 = getelementptr inbounds double, double* %4, i64 %252
  %267 = load double, double* %266, align 8, !tbaa !13
  %268 = shl nsw i32 %253, 2
  %269 = sext i32 %268 to i64
  %270 = getelementptr inbounds double, double* %6, i64 %269
  %271 = load double, double* %270, align 8, !tbaa !13
  %272 = or i32 %268, 1
  %273 = sext i32 %272 to i64
  %274 = getelementptr inbounds double, double* %6, i64 %273
  %275 = load double, double* %274, align 8, !tbaa !13
  %276 = insertelement <2 x double> undef, double %271, i32 0
  %277 = insertelement <2 x double> %276, double %275, i32 1
  %278 = insertelement <2 x double> undef, double %267, i32 0
  %279 = shufflevector <2 x double> %278, <2 x double> undef, <2 x i32> zeroinitializer
  %280 = fdiv <2 x double> %277, %279
  %281 = or i32 %268, 2
  %282 = sext i32 %281 to i64
  %283 = getelementptr inbounds double, double* %6, i64 %282
  %284 = load double, double* %283, align 8, !tbaa !13
  %285 = or i32 %268, 3
  %286 = sext i32 %285 to i64
  %287 = getelementptr inbounds double, double* %6, i64 %286
  %288 = load double, double* %287, align 8, !tbaa !13
  %289 = insertelement <2 x double> undef, double %284, i32 0
  %290 = insertelement <2 x double> %289, double %288, i32 1
  %291 = fdiv <2 x double> %290, %279
  %292 = extractelement <2 x double> %280, i32 0
  store double %292, double* %270, align 8, !tbaa !13
  %293 = extractelement <2 x double> %280, i32 1
  store double %293, double* %274, align 8, !tbaa !13
  %294 = extractelement <2 x double> %291, i32 0
  store double %294, double* %283, align 8, !tbaa !13
  %295 = extractelement <2 x double> %291, i32 1
  store double %295, double* %287, align 8, !tbaa !13
  %296 = icmp sgt i32 %259, 0
  br i1 %296, label %297, label %325

; <label>:297:                                    ; preds = %249
  %298 = zext i32 %259 to i64
  br label %299

; <label>:299:                                    ; preds = %299, %297
  %300 = phi i64 [ 0, %297 ], [ %323, %299 ]
  %301 = getelementptr inbounds i32, i32* %260, i64 %300
  %302 = load i32, i32* %301, align 4, !tbaa !3
  %303 = getelementptr inbounds double, double* %265, i64 %300
  %304 = load double, double* %303, align 8, !tbaa !13
  %305 = shl nsw i32 %302, 2
  %306 = sext i32 %305 to i64
  %307 = getelementptr inbounds double, double* %6, i64 %306
  %308 = insertelement <2 x double> undef, double %304, i32 0
  %309 = shufflevector <2 x double> %308, <2 x double> undef, <2 x i32> zeroinitializer
  %310 = fmul <2 x double> %280, %309
  %311 = bitcast double* %307 to <2 x double>*
  %312 = load <2 x double>, <2 x double>* %311, align 8, !tbaa !13
  %313 = fsub <2 x double> %312, %310
  %314 = bitcast double* %307 to <2 x double>*
  store <2 x double> %313, <2 x double>* %314, align 8, !tbaa !13
  %315 = or i32 %305, 2
  %316 = sext i32 %315 to i64
  %317 = getelementptr inbounds double, double* %6, i64 %316
  %318 = fmul <2 x double> %291, %309
  %319 = bitcast double* %317 to <2 x double>*
  %320 = load <2 x double>, <2 x double>* %319, align 8, !tbaa !13
  %321 = fsub <2 x double> %320, %318
  %322 = bitcast double* %317 to <2 x double>*
  store <2 x double> %321, <2 x double>* %322, align 8, !tbaa !13
  %323 = add nuw nsw i64 %300, 1
  %324 = icmp eq i64 %323, %298
  br i1 %324, label %325, label %299

; <label>:325:                                    ; preds = %299, %249
  %326 = icmp sgt i64 %250, 1
  br i1 %326, label %249, label %327

; <label>:327:                                    ; preds = %325, %243, %177, %77, %245, %179, %79, %8, %7
  ret void
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @klu_ltsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #3 {
  switch i32 %4, label %375 [
    i32 1, label %7
    i32 2, label %107
    i32 3, label %203
    i32 4, label %267
  ]

; <label>:7:                                      ; preds = %6
  %8 = icmp sgt i32 %0, 0
  br i1 %8, label %9, label %375

; <label>:9:                                      ; preds = %7
  %10 = sext i32 %0 to i64
  br label %11

; <label>:11:                                     ; preds = %9, %104
  %12 = phi i64 [ %10, %9 ], [ %13, %104 ]
  %13 = add nsw i64 %12, -1
  %14 = getelementptr inbounds i32, i32* %1, i64 %13
  %15 = load i32, i32* %14, align 4, !tbaa !3
  %16 = sext i32 %15 to i64
  %17 = getelementptr inbounds double, double* %3, i64 %16
  %18 = getelementptr inbounds i32, i32* %2, i64 %13
  %19 = load i32, i32* %18, align 4, !tbaa !3
  %20 = bitcast double* %17 to i32*
  %21 = sext i32 %19 to i64
  %22 = shl nsw i64 %21, 2
  %23 = add nsw i64 %22, 7
  %24 = lshr i64 %23, 3
  %25 = getelementptr inbounds double, double* %17, i64 %24
  %26 = getelementptr inbounds double, double* %5, i64 %13
  %27 = load double, double* %26, align 8, !tbaa !13
  %28 = icmp sgt i32 %19, 0
  br i1 %28, label %29, label %104

; <label>:29:                                     ; preds = %11
  %30 = zext i32 %19 to i64
  %31 = add nsw i64 %30, -1
  %32 = and i64 %30, 3
  %33 = icmp ult i64 %31, 3
  br i1 %33, label %82, label %34

; <label>:34:                                     ; preds = %29
  %35 = sub nsw i64 %30, %32
  br label %36

; <label>:36:                                     ; preds = %36, %34
  %37 = phi i64 [ 0, %34 ], [ %79, %36 ]
  %38 = phi double [ %27, %34 ], [ %78, %36 ]
  %39 = phi i64 [ %35, %34 ], [ %80, %36 ]
  %40 = getelementptr inbounds double, double* %25, i64 %37
  %41 = load double, double* %40, align 8, !tbaa !13
  %42 = getelementptr inbounds i32, i32* %20, i64 %37
  %43 = load i32, i32* %42, align 4, !tbaa !3
  %44 = sext i32 %43 to i64
  %45 = getelementptr inbounds double, double* %5, i64 %44
  %46 = load double, double* %45, align 8, !tbaa !13
  %47 = fmul double %41, %46
  %48 = fsub double %38, %47
  %49 = or i64 %37, 1
  %50 = getelementptr inbounds double, double* %25, i64 %49
  %51 = load double, double* %50, align 8, !tbaa !13
  %52 = getelementptr inbounds i32, i32* %20, i64 %49
  %53 = load i32, i32* %52, align 4, !tbaa !3
  %54 = sext i32 %53 to i64
  %55 = getelementptr inbounds double, double* %5, i64 %54
  %56 = load double, double* %55, align 8, !tbaa !13
  %57 = fmul double %51, %56
  %58 = fsub double %48, %57
  %59 = or i64 %37, 2
  %60 = getelementptr inbounds double, double* %25, i64 %59
  %61 = load double, double* %60, align 8, !tbaa !13
  %62 = getelementptr inbounds i32, i32* %20, i64 %59
  %63 = load i32, i32* %62, align 4, !tbaa !3
  %64 = sext i32 %63 to i64
  %65 = getelementptr inbounds double, double* %5, i64 %64
  %66 = load double, double* %65, align 8, !tbaa !13
  %67 = fmul double %61, %66
  %68 = fsub double %58, %67
  %69 = or i64 %37, 3
  %70 = getelementptr inbounds double, double* %25, i64 %69
  %71 = load double, double* %70, align 8, !tbaa !13
  %72 = getelementptr inbounds i32, i32* %20, i64 %69
  %73 = load i32, i32* %72, align 4, !tbaa !3
  %74 = sext i32 %73 to i64
  %75 = getelementptr inbounds double, double* %5, i64 %74
  %76 = load double, double* %75, align 8, !tbaa !13
  %77 = fmul double %71, %76
  %78 = fsub double %68, %77
  %79 = add nuw nsw i64 %37, 4
  %80 = add i64 %39, -4
  %81 = icmp eq i64 %80, 0
  br i1 %81, label %82, label %36

; <label>:82:                                     ; preds = %36, %29
  %83 = phi double [ undef, %29 ], [ %78, %36 ]
  %84 = phi i64 [ 0, %29 ], [ %79, %36 ]
  %85 = phi double [ %27, %29 ], [ %78, %36 ]
  %86 = icmp eq i64 %32, 0
  br i1 %86, label %104, label %87

; <label>:87:                                     ; preds = %82
  br label %88

; <label>:88:                                     ; preds = %88, %87
  %89 = phi i64 [ %84, %87 ], [ %101, %88 ]
  %90 = phi double [ %85, %87 ], [ %100, %88 ]
  %91 = phi i64 [ %32, %87 ], [ %102, %88 ]
  %92 = getelementptr inbounds double, double* %25, i64 %89
  %93 = load double, double* %92, align 8, !tbaa !13
  %94 = getelementptr inbounds i32, i32* %20, i64 %89
  %95 = load i32, i32* %94, align 4, !tbaa !3
  %96 = sext i32 %95 to i64
  %97 = getelementptr inbounds double, double* %5, i64 %96
  %98 = load double, double* %97, align 8, !tbaa !13
  %99 = fmul double %93, %98
  %100 = fsub double %90, %99
  %101 = add nuw nsw i64 %89, 1
  %102 = add i64 %91, -1
  %103 = icmp eq i64 %102, 0
  br i1 %103, label %104, label %88, !llvm.loop !14

; <label>:104:                                    ; preds = %82, %88, %11
  %105 = phi double [ %27, %11 ], [ %83, %82 ], [ %100, %88 ]
  store double %105, double* %26, align 8, !tbaa !13
  %106 = icmp sgt i64 %12, 1
  br i1 %106, label %11, label %375

; <label>:107:                                    ; preds = %6
  %108 = icmp sgt i32 %0, 0
  br i1 %108, label %109, label %375

; <label>:109:                                    ; preds = %107
  %110 = sext i32 %0 to i64
  br label %111

; <label>:111:                                    ; preds = %109, %198
  %112 = phi i64 [ %110, %109 ], [ %114, %198 ]
  %113 = phi i32 [ %0, %109 ], [ %115, %198 ]
  %114 = add nsw i64 %112, -1
  %115 = add nsw i32 %113, -1
  %116 = shl nsw i32 %115, 1
  %117 = sext i32 %116 to i64
  %118 = getelementptr inbounds double, double* %5, i64 %117
  %119 = load double, double* %118, align 8, !tbaa !13
  %120 = or i32 %116, 1
  %121 = sext i32 %120 to i64
  %122 = getelementptr inbounds double, double* %5, i64 %121
  %123 = load double, double* %122, align 8, !tbaa !13
  %124 = getelementptr inbounds i32, i32* %1, i64 %114
  %125 = load i32, i32* %124, align 4, !tbaa !3
  %126 = sext i32 %125 to i64
  %127 = getelementptr inbounds double, double* %3, i64 %126
  %128 = getelementptr inbounds i32, i32* %2, i64 %114
  %129 = load i32, i32* %128, align 4, !tbaa !3
  %130 = bitcast double* %127 to i32*
  %131 = sext i32 %129 to i64
  %132 = shl nsw i64 %131, 2
  %133 = add nsw i64 %132, 7
  %134 = lshr i64 %133, 3
  %135 = getelementptr inbounds double, double* %127, i64 %134
  %136 = icmp sgt i32 %129, 0
  %137 = insertelement <2 x double> undef, double %119, i32 0
  %138 = insertelement <2 x double> %137, double %123, i32 1
  br i1 %136, label %139, label %198

; <label>:139:                                    ; preds = %111
  %140 = zext i32 %129 to i64
  %141 = and i64 %140, 1
  %142 = icmp eq i32 %129, 1
  br i1 %142, label %179, label %143

; <label>:143:                                    ; preds = %139
  %144 = sub nsw i64 %140, %141
  br label %145

; <label>:145:                                    ; preds = %145, %143
  %146 = phi i64 [ 0, %143 ], [ %176, %145 ]
  %147 = phi <2 x double> [ %138, %143 ], [ %175, %145 ]
  %148 = phi i64 [ %144, %143 ], [ %177, %145 ]
  %149 = getelementptr inbounds i32, i32* %130, i64 %146
  %150 = load i32, i32* %149, align 4, !tbaa !3
  %151 = getelementptr inbounds double, double* %135, i64 %146
  %152 = load double, double* %151, align 8, !tbaa !13
  %153 = shl nsw i32 %150, 1
  %154 = sext i32 %153 to i64
  %155 = getelementptr inbounds double, double* %5, i64 %154
  %156 = bitcast double* %155 to <2 x double>*
  %157 = load <2 x double>, <2 x double>* %156, align 8, !tbaa !13
  %158 = insertelement <2 x double> undef, double %152, i32 0
  %159 = shufflevector <2 x double> %158, <2 x double> undef, <2 x i32> zeroinitializer
  %160 = fmul <2 x double> %159, %157
  %161 = fsub <2 x double> %147, %160
  %162 = or i64 %146, 1
  %163 = getelementptr inbounds i32, i32* %130, i64 %162
  %164 = load i32, i32* %163, align 4, !tbaa !3
  %165 = getelementptr inbounds double, double* %135, i64 %162
  %166 = load double, double* %165, align 8, !tbaa !13
  %167 = shl nsw i32 %164, 1
  %168 = sext i32 %167 to i64
  %169 = getelementptr inbounds double, double* %5, i64 %168
  %170 = bitcast double* %169 to <2 x double>*
  %171 = load <2 x double>, <2 x double>* %170, align 8, !tbaa !13
  %172 = insertelement <2 x double> undef, double %166, i32 0
  %173 = shufflevector <2 x double> %172, <2 x double> undef, <2 x i32> zeroinitializer
  %174 = fmul <2 x double> %173, %171
  %175 = fsub <2 x double> %161, %174
  %176 = add nuw nsw i64 %146, 2
  %177 = add i64 %148, -2
  %178 = icmp eq i64 %177, 0
  br i1 %178, label %179, label %145

; <label>:179:                                    ; preds = %145, %139
  %180 = phi <2 x double> [ undef, %139 ], [ %175, %145 ]
  %181 = phi i64 [ 0, %139 ], [ %176, %145 ]
  %182 = phi <2 x double> [ %138, %139 ], [ %175, %145 ]
  %183 = icmp eq i64 %141, 0
  br i1 %183, label %198, label %184

; <label>:184:                                    ; preds = %179
  %185 = getelementptr inbounds double, double* %135, i64 %181
  %186 = load double, double* %185, align 8, !tbaa !13
  %187 = insertelement <2 x double> undef, double %186, i32 0
  %188 = shufflevector <2 x double> %187, <2 x double> undef, <2 x i32> zeroinitializer
  %189 = getelementptr inbounds i32, i32* %130, i64 %181
  %190 = load i32, i32* %189, align 4, !tbaa !3
  %191 = shl nsw i32 %190, 1
  %192 = sext i32 %191 to i64
  %193 = getelementptr inbounds double, double* %5, i64 %192
  %194 = bitcast double* %193 to <2 x double>*
  %195 = load <2 x double>, <2 x double>* %194, align 8, !tbaa !13
  %196 = fmul <2 x double> %188, %195
  %197 = fsub <2 x double> %182, %196
  br label %198

; <label>:198:                                    ; preds = %184, %179, %111
  %199 = phi <2 x double> [ %138, %111 ], [ %180, %179 ], [ %197, %184 ]
  %200 = extractelement <2 x double> %199, i32 0
  store double %200, double* %118, align 8, !tbaa !13
  %201 = extractelement <2 x double> %199, i32 1
  store double %201, double* %122, align 8, !tbaa !13
  %202 = icmp sgt i64 %112, 1
  br i1 %202, label %111, label %375

; <label>:203:                                    ; preds = %6
  %204 = icmp sgt i32 %0, 0
  br i1 %204, label %205, label %375

; <label>:205:                                    ; preds = %203
  %206 = sext i32 %0 to i64
  br label %207

; <label>:207:                                    ; preds = %205, %262
  %208 = phi i64 [ %206, %205 ], [ %209, %262 ]
  %209 = add nsw i64 %208, -1
  %210 = mul nsw i64 %209, 3
  %211 = getelementptr inbounds double, double* %5, i64 %210
  %212 = bitcast double* %211 to <2 x double>*
  %213 = load <2 x double>, <2 x double>* %212, align 8, !tbaa !13
  %214 = add nsw i64 %210, 2
  %215 = getelementptr inbounds double, double* %5, i64 %214
  %216 = load double, double* %215, align 8, !tbaa !13
  %217 = getelementptr inbounds i32, i32* %1, i64 %209
  %218 = load i32, i32* %217, align 4, !tbaa !3
  %219 = sext i32 %218 to i64
  %220 = getelementptr inbounds double, double* %3, i64 %219
  %221 = getelementptr inbounds i32, i32* %2, i64 %209
  %222 = load i32, i32* %221, align 4, !tbaa !3
  %223 = bitcast double* %220 to i32*
  %224 = sext i32 %222 to i64
  %225 = shl nsw i64 %224, 2
  %226 = add nsw i64 %225, 7
  %227 = lshr i64 %226, 3
  %228 = getelementptr inbounds double, double* %220, i64 %227
  %229 = icmp sgt i32 %222, 0
  br i1 %229, label %230, label %262

; <label>:230:                                    ; preds = %207
  %231 = zext i32 %222 to i64
  br label %232

; <label>:232:                                    ; preds = %232, %230
  %233 = phi i64 [ 0, %230 ], [ %260, %232 ]
  %234 = phi double [ %216, %230 ], [ %259, %232 ]
  %235 = phi <2 x double> [ %213, %230 ], [ %253, %232 ]
  %236 = getelementptr inbounds i32, i32* %223, i64 %233
  %237 = load i32, i32* %236, align 4, !tbaa !3
  %238 = getelementptr inbounds double, double* %228, i64 %233
  %239 = load double, double* %238, align 8, !tbaa !13
  %240 = mul nsw i32 %237, 3
  %241 = sext i32 %240 to i64
  %242 = getelementptr inbounds double, double* %5, i64 %241
  %243 = load double, double* %242, align 8, !tbaa !13
  %244 = add nsw i32 %240, 1
  %245 = sext i32 %244 to i64
  %246 = getelementptr inbounds double, double* %5, i64 %245
  %247 = load double, double* %246, align 8, !tbaa !13
  %248 = insertelement <2 x double> undef, double %239, i32 0
  %249 = shufflevector <2 x double> %248, <2 x double> undef, <2 x i32> zeroinitializer
  %250 = insertelement <2 x double> undef, double %243, i32 0
  %251 = insertelement <2 x double> %250, double %247, i32 1
  %252 = fmul <2 x double> %249, %251
  %253 = fsub <2 x double> %235, %252
  %254 = add nsw i32 %240, 2
  %255 = sext i32 %254 to i64
  %256 = getelementptr inbounds double, double* %5, i64 %255
  %257 = load double, double* %256, align 8, !tbaa !13
  %258 = fmul double %239, %257
  %259 = fsub double %234, %258
  %260 = add nuw nsw i64 %233, 1
  %261 = icmp eq i64 %260, %231
  br i1 %261, label %262, label %232

; <label>:262:                                    ; preds = %232, %207
  %263 = phi double [ %216, %207 ], [ %259, %232 ]
  %264 = phi <2 x double> [ %213, %207 ], [ %253, %232 ]
  %265 = bitcast double* %211 to <2 x double>*
  store <2 x double> %264, <2 x double>* %265, align 8, !tbaa !13
  store double %263, double* %215, align 8, !tbaa !13
  %266 = icmp sgt i64 %208, 1
  br i1 %266, label %207, label %375

; <label>:267:                                    ; preds = %6
  %268 = icmp sgt i32 %0, 0
  br i1 %268, label %269, label %375

; <label>:269:                                    ; preds = %267
  %270 = sext i32 %0 to i64
  br label %271

; <label>:271:                                    ; preds = %269, %368
  %272 = phi i64 [ %270, %269 ], [ %274, %368 ]
  %273 = phi i32 [ %0, %269 ], [ %275, %368 ]
  %274 = add nsw i64 %272, -1
  %275 = add nsw i32 %273, -1
  %276 = shl nsw i32 %275, 2
  %277 = sext i32 %276 to i64
  %278 = getelementptr inbounds double, double* %5, i64 %277
  %279 = load double, double* %278, align 8, !tbaa !13
  %280 = or i32 %276, 1
  %281 = sext i32 %280 to i64
  %282 = getelementptr inbounds double, double* %5, i64 %281
  %283 = load double, double* %282, align 8, !tbaa !13
  %284 = or i32 %276, 2
  %285 = sext i32 %284 to i64
  %286 = getelementptr inbounds double, double* %5, i64 %285
  %287 = load double, double* %286, align 8, !tbaa !13
  %288 = or i32 %276, 3
  %289 = sext i32 %288 to i64
  %290 = getelementptr inbounds double, double* %5, i64 %289
  %291 = load double, double* %290, align 8, !tbaa !13
  %292 = getelementptr inbounds i32, i32* %1, i64 %274
  %293 = load i32, i32* %292, align 4, !tbaa !3
  %294 = sext i32 %293 to i64
  %295 = getelementptr inbounds double, double* %3, i64 %294
  %296 = getelementptr inbounds i32, i32* %2, i64 %274
  %297 = load i32, i32* %296, align 4, !tbaa !3
  %298 = bitcast double* %295 to i32*
  %299 = sext i32 %297 to i64
  %300 = shl nsw i64 %299, 2
  %301 = add nsw i64 %300, 7
  %302 = lshr i64 %301, 3
  %303 = getelementptr inbounds double, double* %295, i64 %302
  %304 = icmp sgt i32 %297, 0
  %305 = insertelement <4 x double> undef, double %279, i32 0
  %306 = insertelement <4 x double> %305, double %283, i32 1
  %307 = insertelement <4 x double> %306, double %287, i32 2
  %308 = insertelement <4 x double> %307, double %291, i32 3
  br i1 %304, label %309, label %368

; <label>:309:                                    ; preds = %271
  %310 = zext i32 %297 to i64
  %311 = and i64 %310, 1
  %312 = icmp eq i32 %297, 1
  br i1 %312, label %349, label %313

; <label>:313:                                    ; preds = %309
  %314 = sub nsw i64 %310, %311
  br label %315

; <label>:315:                                    ; preds = %315, %313
  %316 = phi i64 [ 0, %313 ], [ %346, %315 ]
  %317 = phi <4 x double> [ %308, %313 ], [ %345, %315 ]
  %318 = phi i64 [ %314, %313 ], [ %347, %315 ]
  %319 = getelementptr inbounds i32, i32* %298, i64 %316
  %320 = load i32, i32* %319, align 4, !tbaa !3
  %321 = getelementptr inbounds double, double* %303, i64 %316
  %322 = load double, double* %321, align 8, !tbaa !13
  %323 = shl nsw i32 %320, 2
  %324 = sext i32 %323 to i64
  %325 = getelementptr inbounds double, double* %5, i64 %324
  %326 = bitcast double* %325 to <4 x double>*
  %327 = load <4 x double>, <4 x double>* %326, align 8, !tbaa !13
  %328 = insertelement <4 x double> undef, double %322, i32 0
  %329 = shufflevector <4 x double> %328, <4 x double> undef, <4 x i32> zeroinitializer
  %330 = fmul <4 x double> %329, %327
  %331 = fsub <4 x double> %317, %330
  %332 = or i64 %316, 1
  %333 = getelementptr inbounds i32, i32* %298, i64 %332
  %334 = load i32, i32* %333, align 4, !tbaa !3
  %335 = getelementptr inbounds double, double* %303, i64 %332
  %336 = load double, double* %335, align 8, !tbaa !13
  %337 = shl nsw i32 %334, 2
  %338 = sext i32 %337 to i64
  %339 = getelementptr inbounds double, double* %5, i64 %338
  %340 = bitcast double* %339 to <4 x double>*
  %341 = load <4 x double>, <4 x double>* %340, align 8, !tbaa !13
  %342 = insertelement <4 x double> undef, double %336, i32 0
  %343 = shufflevector <4 x double> %342, <4 x double> undef, <4 x i32> zeroinitializer
  %344 = fmul <4 x double> %343, %341
  %345 = fsub <4 x double> %331, %344
  %346 = add nuw nsw i64 %316, 2
  %347 = add i64 %318, -2
  %348 = icmp eq i64 %347, 0
  br i1 %348, label %349, label %315

; <label>:349:                                    ; preds = %315, %309
  %350 = phi <4 x double> [ undef, %309 ], [ %345, %315 ]
  %351 = phi i64 [ 0, %309 ], [ %346, %315 ]
  %352 = phi <4 x double> [ %308, %309 ], [ %345, %315 ]
  %353 = icmp eq i64 %311, 0
  br i1 %353, label %368, label %354

; <label>:354:                                    ; preds = %349
  %355 = getelementptr inbounds double, double* %303, i64 %351
  %356 = load double, double* %355, align 8, !tbaa !13
  %357 = insertelement <4 x double> undef, double %356, i32 0
  %358 = shufflevector <4 x double> %357, <4 x double> undef, <4 x i32> zeroinitializer
  %359 = getelementptr inbounds i32, i32* %298, i64 %351
  %360 = load i32, i32* %359, align 4, !tbaa !3
  %361 = shl nsw i32 %360, 2
  %362 = sext i32 %361 to i64
  %363 = getelementptr inbounds double, double* %5, i64 %362
  %364 = bitcast double* %363 to <4 x double>*
  %365 = load <4 x double>, <4 x double>* %364, align 8, !tbaa !13
  %366 = fmul <4 x double> %358, %365
  %367 = fsub <4 x double> %352, %366
  br label %368

; <label>:368:                                    ; preds = %354, %349, %271
  %369 = phi <4 x double> [ %308, %271 ], [ %350, %349 ], [ %367, %354 ]
  %370 = extractelement <4 x double> %369, i32 0
  store double %370, double* %278, align 8, !tbaa !13
  %371 = extractelement <4 x double> %369, i32 1
  store double %371, double* %282, align 8, !tbaa !13
  %372 = extractelement <4 x double> %369, i32 2
  store double %372, double* %286, align 8, !tbaa !13
  %373 = extractelement <4 x double> %369, i32 3
  store double %373, double* %290, align 8, !tbaa !13
  %374 = icmp sgt i64 %272, 1
  br i1 %374, label %271, label %375

; <label>:375:                                    ; preds = %368, %262, %198, %104, %267, %203, %107, %7, %6
  ret void
}

; Function Attrs: norecurse nounwind ssp uwtable
define void @klu_utsolve(i32, i32* nocapture readonly, i32* nocapture readonly, double* nocapture readonly, double* nocapture readonly, i32, double* nocapture) local_unnamed_addr #3 {
  switch i32 %5, label %390 [
    i32 1, label %8
    i32 2, label %111
    i32 3, label %203
    i32 4, label %277
  ]

; <label>:8:                                      ; preds = %7
  %9 = icmp sgt i32 %0, 0
  br i1 %9, label %10, label %390

; <label>:10:                                     ; preds = %8
  %11 = zext i32 %0 to i64
  br label %12

; <label>:12:                                     ; preds = %104, %10
  %13 = phi i64 [ 0, %10 ], [ %109, %104 ]
  %14 = getelementptr inbounds i32, i32* %1, i64 %13
  %15 = load i32, i32* %14, align 4, !tbaa !3
  %16 = sext i32 %15 to i64
  %17 = getelementptr inbounds double, double* %3, i64 %16
  %18 = getelementptr inbounds i32, i32* %2, i64 %13
  %19 = load i32, i32* %18, align 4, !tbaa !3
  %20 = bitcast double* %17 to i32*
  %21 = sext i32 %19 to i64
  %22 = shl nsw i64 %21, 2
  %23 = add nsw i64 %22, 7
  %24 = lshr i64 %23, 3
  %25 = getelementptr inbounds double, double* %17, i64 %24
  %26 = getelementptr inbounds double, double* %6, i64 %13
  %27 = load double, double* %26, align 8, !tbaa !13
  %28 = icmp sgt i32 %19, 0
  br i1 %28, label %29, label %104

; <label>:29:                                     ; preds = %12
  %30 = zext i32 %19 to i64
  %31 = add nsw i64 %30, -1
  %32 = and i64 %30, 3
  %33 = icmp ult i64 %31, 3
  br i1 %33, label %82, label %34

; <label>:34:                                     ; preds = %29
  %35 = sub nsw i64 %30, %32
  br label %36

; <label>:36:                                     ; preds = %36, %34
  %37 = phi i64 [ 0, %34 ], [ %79, %36 ]
  %38 = phi double [ %27, %34 ], [ %78, %36 ]
  %39 = phi i64 [ %35, %34 ], [ %80, %36 ]
  %40 = getelementptr inbounds double, double* %25, i64 %37
  %41 = load double, double* %40, align 8, !tbaa !13
  %42 = getelementptr inbounds i32, i32* %20, i64 %37
  %43 = load i32, i32* %42, align 4, !tbaa !3
  %44 = sext i32 %43 to i64
  %45 = getelementptr inbounds double, double* %6, i64 %44
  %46 = load double, double* %45, align 8, !tbaa !13
  %47 = fmul double %41, %46
  %48 = fsub double %38, %47
  %49 = or i64 %37, 1
  %50 = getelementptr inbounds double, double* %25, i64 %49
  %51 = load double, double* %50, align 8, !tbaa !13
  %52 = getelementptr inbounds i32, i32* %20, i64 %49
  %53 = load i32, i32* %52, align 4, !tbaa !3
  %54 = sext i32 %53 to i64
  %55 = getelementptr inbounds double, double* %6, i64 %54
  %56 = load double, double* %55, align 8, !tbaa !13
  %57 = fmul double %51, %56
  %58 = fsub double %48, %57
  %59 = or i64 %37, 2
  %60 = getelementptr inbounds double, double* %25, i64 %59
  %61 = load double, double* %60, align 8, !tbaa !13
  %62 = getelementptr inbounds i32, i32* %20, i64 %59
  %63 = load i32, i32* %62, align 4, !tbaa !3
  %64 = sext i32 %63 to i64
  %65 = getelementptr inbounds double, double* %6, i64 %64
  %66 = load double, double* %65, align 8, !tbaa !13
  %67 = fmul double %61, %66
  %68 = fsub double %58, %67
  %69 = or i64 %37, 3
  %70 = getelementptr inbounds double, double* %25, i64 %69
  %71 = load double, double* %70, align 8, !tbaa !13
  %72 = getelementptr inbounds i32, i32* %20, i64 %69
  %73 = load i32, i32* %72, align 4, !tbaa !3
  %74 = sext i32 %73 to i64
  %75 = getelementptr inbounds double, double* %6, i64 %74
  %76 = load double, double* %75, align 8, !tbaa !13
  %77 = fmul double %71, %76
  %78 = fsub double %68, %77
  %79 = add nuw nsw i64 %37, 4
  %80 = add i64 %39, -4
  %81 = icmp eq i64 %80, 0
  br i1 %81, label %82, label %36

; <label>:82:                                     ; preds = %36, %29
  %83 = phi double [ undef, %29 ], [ %78, %36 ]
  %84 = phi i64 [ 0, %29 ], [ %79, %36 ]
  %85 = phi double [ %27, %29 ], [ %78, %36 ]
  %86 = icmp eq i64 %32, 0
  br i1 %86, label %104, label %87

; <label>:87:                                     ; preds = %82
  br label %88

; <label>:88:                                     ; preds = %88, %87
  %89 = phi i64 [ %84, %87 ], [ %101, %88 ]
  %90 = phi double [ %85, %87 ], [ %100, %88 ]
  %91 = phi i64 [ %32, %87 ], [ %102, %88 ]
  %92 = getelementptr inbounds double, double* %25, i64 %89
  %93 = load double, double* %92, align 8, !tbaa !13
  %94 = getelementptr inbounds i32, i32* %20, i64 %89
  %95 = load i32, i32* %94, align 4, !tbaa !3
  %96 = sext i32 %95 to i64
  %97 = getelementptr inbounds double, double* %6, i64 %96
  %98 = load double, double* %97, align 8, !tbaa !13
  %99 = fmul double %93, %98
  %100 = fsub double %90, %99
  %101 = add nuw nsw i64 %89, 1
  %102 = add i64 %91, -1
  %103 = icmp eq i64 %102, 0
  br i1 %103, label %104, label %88, !llvm.loop !16

; <label>:104:                                    ; preds = %82, %88, %12
  %105 = phi double [ %27, %12 ], [ %83, %82 ], [ %100, %88 ]
  %106 = getelementptr inbounds double, double* %4, i64 %13
  %107 = load double, double* %106, align 8, !tbaa !13
  %108 = fdiv double %105, %107
  store double %108, double* %26, align 8, !tbaa !13
  %109 = add nuw nsw i64 %13, 1
  %110 = icmp eq i64 %109, %11
  br i1 %110, label %390, label %12

; <label>:111:                                    ; preds = %7
  %112 = icmp sgt i32 %0, 0
  br i1 %112, label %113, label %390

; <label>:113:                                    ; preds = %111
  %114 = zext i32 %0 to i64
  br label %115

; <label>:115:                                    ; preds = %193, %113
  %116 = phi i64 [ 0, %113 ], [ %201, %193 ]
  %117 = getelementptr inbounds i32, i32* %1, i64 %116
  %118 = load i32, i32* %117, align 4, !tbaa !3
  %119 = sext i32 %118 to i64
  %120 = getelementptr inbounds double, double* %3, i64 %119
  %121 = getelementptr inbounds i32, i32* %2, i64 %116
  %122 = load i32, i32* %121, align 4, !tbaa !3
  %123 = bitcast double* %120 to i32*
  %124 = sext i32 %122 to i64
  %125 = shl nsw i64 %124, 2
  %126 = add nsw i64 %125, 7
  %127 = lshr i64 %126, 3
  %128 = getelementptr inbounds double, double* %120, i64 %127
  %129 = shl nuw nsw i64 %116, 1
  %130 = getelementptr inbounds double, double* %6, i64 %129
  %131 = bitcast double* %130 to <2 x double>*
  %132 = load <2 x double>, <2 x double>* %131, align 8, !tbaa !13
  %133 = icmp sgt i32 %122, 0
  br i1 %133, label %134, label %193

; <label>:134:                                    ; preds = %115
  %135 = zext i32 %122 to i64
  %136 = and i64 %135, 1
  %137 = icmp eq i32 %122, 1
  br i1 %137, label %174, label %138

; <label>:138:                                    ; preds = %134
  %139 = sub nsw i64 %135, %136
  br label %140

; <label>:140:                                    ; preds = %140, %138
  %141 = phi i64 [ 0, %138 ], [ %171, %140 ]
  %142 = phi <2 x double> [ %132, %138 ], [ %170, %140 ]
  %143 = phi i64 [ %139, %138 ], [ %172, %140 ]
  %144 = getelementptr inbounds i32, i32* %123, i64 %141
  %145 = load i32, i32* %144, align 4, !tbaa !3
  %146 = getelementptr inbounds double, double* %128, i64 %141
  %147 = load double, double* %146, align 8, !tbaa !13
  %148 = shl nsw i32 %145, 1
  %149 = sext i32 %148 to i64
  %150 = getelementptr inbounds double, double* %6, i64 %149
  %151 = bitcast double* %150 to <2 x double>*
  %152 = load <2 x double>, <2 x double>* %151, align 8, !tbaa !13
  %153 = insertelement <2 x double> undef, double %147, i32 0
  %154 = shufflevector <2 x double> %153, <2 x double> undef, <2 x i32> zeroinitializer
  %155 = fmul <2 x double> %154, %152
  %156 = fsub <2 x double> %142, %155
  %157 = or i64 %141, 1
  %158 = getelementptr inbounds i32, i32* %123, i64 %157
  %159 = load i32, i32* %158, align 4, !tbaa !3
  %160 = getelementptr inbounds double, double* %128, i64 %157
  %161 = load double, double* %160, align 8, !tbaa !13
  %162 = shl nsw i32 %159, 1
  %163 = sext i32 %162 to i64
  %164 = getelementptr inbounds double, double* %6, i64 %163
  %165 = bitcast double* %164 to <2 x double>*
  %166 = load <2 x double>, <2 x double>* %165, align 8, !tbaa !13
  %167 = insertelement <2 x double> undef, double %161, i32 0
  %168 = shufflevector <2 x double> %167, <2 x double> undef, <2 x i32> zeroinitializer
  %169 = fmul <2 x double> %168, %166
  %170 = fsub <2 x double> %156, %169
  %171 = add nuw nsw i64 %141, 2
  %172 = add i64 %143, -2
  %173 = icmp eq i64 %172, 0
  br i1 %173, label %174, label %140

; <label>:174:                                    ; preds = %140, %134
  %175 = phi <2 x double> [ undef, %134 ], [ %170, %140 ]
  %176 = phi i64 [ 0, %134 ], [ %171, %140 ]
  %177 = phi <2 x double> [ %132, %134 ], [ %170, %140 ]
  %178 = icmp eq i64 %136, 0
  br i1 %178, label %193, label %179

; <label>:179:                                    ; preds = %174
  %180 = getelementptr inbounds double, double* %128, i64 %176
  %181 = load double, double* %180, align 8, !tbaa !13
  %182 = insertelement <2 x double> undef, double %181, i32 0
  %183 = shufflevector <2 x double> %182, <2 x double> undef, <2 x i32> zeroinitializer
  %184 = getelementptr inbounds i32, i32* %123, i64 %176
  %185 = load i32, i32* %184, align 4, !tbaa !3
  %186 = shl nsw i32 %185, 1
  %187 = sext i32 %186 to i64
  %188 = getelementptr inbounds double, double* %6, i64 %187
  %189 = bitcast double* %188 to <2 x double>*
  %190 = load <2 x double>, <2 x double>* %189, align 8, !tbaa !13
  %191 = fmul <2 x double> %183, %190
  %192 = fsub <2 x double> %177, %191
  br label %193

; <label>:193:                                    ; preds = %179, %174, %115
  %194 = phi <2 x double> [ %132, %115 ], [ %175, %174 ], [ %192, %179 ]
  %195 = getelementptr inbounds double, double* %4, i64 %116
  %196 = load double, double* %195, align 8, !tbaa !13
  %197 = insertelement <2 x double> undef, double %196, i32 0
  %198 = shufflevector <2 x double> %197, <2 x double> undef, <2 x i32> zeroinitializer
  %199 = fdiv <2 x double> %194, %198
  %200 = bitcast double* %130 to <2 x double>*
  store <2 x double> %199, <2 x double>* %200, align 8, !tbaa !13
  %201 = add nuw nsw i64 %116, 1
  %202 = icmp eq i64 %201, %114
  br i1 %202, label %390, label %115

; <label>:203:                                    ; preds = %7
  %204 = icmp sgt i32 %0, 0
  br i1 %204, label %205, label %390

; <label>:205:                                    ; preds = %203
  %206 = zext i32 %0 to i64
  br label %207

; <label>:207:                                    ; preds = %266, %205
  %208 = phi i64 [ 0, %205 ], [ %275, %266 ]
  %209 = getelementptr inbounds i32, i32* %1, i64 %208
  %210 = load i32, i32* %209, align 4, !tbaa !3
  %211 = sext i32 %210 to i64
  %212 = getelementptr inbounds double, double* %3, i64 %211
  %213 = getelementptr inbounds i32, i32* %2, i64 %208
  %214 = load i32, i32* %213, align 4, !tbaa !3
  %215 = bitcast double* %212 to i32*
  %216 = sext i32 %214 to i64
  %217 = shl nsw i64 %216, 2
  %218 = add nsw i64 %217, 7
  %219 = lshr i64 %218, 3
  %220 = getelementptr inbounds double, double* %212, i64 %219
  %221 = trunc i64 %208 to i32
  %222 = mul nsw i32 %221, 3
  %223 = zext i32 %222 to i64
  %224 = getelementptr inbounds double, double* %6, i64 %223
  %225 = load double, double* %224, align 8, !tbaa !13
  %226 = add nuw nsw i32 %222, 1
  %227 = zext i32 %226 to i64
  %228 = getelementptr inbounds double, double* %6, i64 %227
  %229 = load double, double* %228, align 8, !tbaa !13
  %230 = add nuw nsw i32 %222, 2
  %231 = zext i32 %230 to i64
  %232 = getelementptr inbounds double, double* %6, i64 %231
  %233 = load double, double* %232, align 8, !tbaa !13
  %234 = icmp sgt i32 %214, 0
  br i1 %234, label %235, label %266

; <label>:235:                                    ; preds = %207
  %236 = zext i32 %214 to i64
  br label %237

; <label>:237:                                    ; preds = %237, %235
  %238 = phi i64 [ 0, %235 ], [ %264, %237 ]
  %239 = phi double [ %233, %235 ], [ %263, %237 ]
  %240 = phi double [ %229, %235 ], [ %257, %237 ]
  %241 = phi double [ %225, %235 ], [ %251, %237 ]
  %242 = getelementptr inbounds i32, i32* %215, i64 %238
  %243 = load i32, i32* %242, align 4, !tbaa !3
  %244 = getelementptr inbounds double, double* %220, i64 %238
  %245 = load double, double* %244, align 8, !tbaa !13
  %246 = mul nsw i32 %243, 3
  %247 = sext i32 %246 to i64
  %248 = getelementptr inbounds double, double* %6, i64 %247
  %249 = load double, double* %248, align 8, !tbaa !13
  %250 = fmul double %245, %249
  %251 = fsub double %241, %250
  %252 = add nsw i32 %246, 1
  %253 = sext i32 %252 to i64
  %254 = getelementptr inbounds double, double* %6, i64 %253
  %255 = load double, double* %254, align 8, !tbaa !13
  %256 = fmul double %245, %255
  %257 = fsub double %240, %256
  %258 = add nsw i32 %246, 2
  %259 = sext i32 %258 to i64
  %260 = getelementptr inbounds double, double* %6, i64 %259
  %261 = load double, double* %260, align 8, !tbaa !13
  %262 = fmul double %245, %261
  %263 = fsub double %239, %262
  %264 = add nuw nsw i64 %238, 1
  %265 = icmp eq i64 %264, %236
  br i1 %265, label %266, label %237

; <label>:266:                                    ; preds = %237, %207
  %267 = phi double [ %225, %207 ], [ %251, %237 ]
  %268 = phi double [ %229, %207 ], [ %257, %237 ]
  %269 = phi double [ %233, %207 ], [ %263, %237 ]
  %270 = getelementptr inbounds double, double* %4, i64 %208
  %271 = load double, double* %270, align 8, !tbaa !13
  %272 = fdiv double %267, %271
  store double %272, double* %224, align 8, !tbaa !13
  %273 = fdiv double %268, %271
  store double %273, double* %228, align 8, !tbaa !13
  %274 = fdiv double %269, %271
  store double %274, double* %232, align 8, !tbaa !13
  %275 = add nuw nsw i64 %208, 1
  %276 = icmp eq i64 %275, %206
  br i1 %276, label %390, label %207

; <label>:277:                                    ; preds = %7
  %278 = icmp sgt i32 %0, 0
  br i1 %278, label %279, label %390

; <label>:279:                                    ; preds = %277
  %280 = zext i32 %0 to i64
  br label %281

; <label>:281:                                    ; preds = %376, %279
  %282 = phi i64 [ 0, %279 ], [ %388, %376 ]
  %283 = getelementptr inbounds i32, i32* %1, i64 %282
  %284 = load i32, i32* %283, align 4, !tbaa !3
  %285 = sext i32 %284 to i64
  %286 = getelementptr inbounds double, double* %3, i64 %285
  %287 = getelementptr inbounds i32, i32* %2, i64 %282
  %288 = load i32, i32* %287, align 4, !tbaa !3
  %289 = bitcast double* %286 to i32*
  %290 = sext i32 %288 to i64
  %291 = shl nsw i64 %290, 2
  %292 = add nsw i64 %291, 7
  %293 = lshr i64 %292, 3
  %294 = getelementptr inbounds double, double* %286, i64 %293
  %295 = trunc i64 %282 to i32
  %296 = shl nsw i32 %295, 2
  %297 = zext i32 %296 to i64
  %298 = getelementptr inbounds double, double* %6, i64 %297
  %299 = load double, double* %298, align 8, !tbaa !13
  %300 = or i32 %296, 1
  %301 = zext i32 %300 to i64
  %302 = getelementptr inbounds double, double* %6, i64 %301
  %303 = load double, double* %302, align 8, !tbaa !13
  %304 = or i32 %296, 2
  %305 = zext i32 %304 to i64
  %306 = getelementptr inbounds double, double* %6, i64 %305
  %307 = load double, double* %306, align 8, !tbaa !13
  %308 = or i32 %296, 3
  %309 = zext i32 %308 to i64
  %310 = getelementptr inbounds double, double* %6, i64 %309
  %311 = load double, double* %310, align 8, !tbaa !13
  %312 = icmp sgt i32 %288, 0
  %313 = insertelement <4 x double> undef, double %299, i32 0
  %314 = insertelement <4 x double> %313, double %303, i32 1
  %315 = insertelement <4 x double> %314, double %307, i32 2
  %316 = insertelement <4 x double> %315, double %311, i32 3
  br i1 %312, label %317, label %376

; <label>:317:                                    ; preds = %281
  %318 = zext i32 %288 to i64
  %319 = and i64 %318, 1
  %320 = icmp eq i32 %288, 1
  br i1 %320, label %357, label %321

; <label>:321:                                    ; preds = %317
  %322 = sub nsw i64 %318, %319
  br label %323

; <label>:323:                                    ; preds = %323, %321
  %324 = phi i64 [ 0, %321 ], [ %354, %323 ]
  %325 = phi <4 x double> [ %316, %321 ], [ %353, %323 ]
  %326 = phi i64 [ %322, %321 ], [ %355, %323 ]
  %327 = getelementptr inbounds i32, i32* %289, i64 %324
  %328 = load i32, i32* %327, align 4, !tbaa !3
  %329 = getelementptr inbounds double, double* %294, i64 %324
  %330 = load double, double* %329, align 8, !tbaa !13
  %331 = shl nsw i32 %328, 2
  %332 = sext i32 %331 to i64
  %333 = getelementptr inbounds double, double* %6, i64 %332
  %334 = bitcast double* %333 to <4 x double>*
  %335 = load <4 x double>, <4 x double>* %334, align 8, !tbaa !13
  %336 = insertelement <4 x double> undef, double %330, i32 0
  %337 = shufflevector <4 x double> %336, <4 x double> undef, <4 x i32> zeroinitializer
  %338 = fmul <4 x double> %337, %335
  %339 = fsub <4 x double> %325, %338
  %340 = or i64 %324, 1
  %341 = getelementptr inbounds i32, i32* %289, i64 %340
  %342 = load i32, i32* %341, align 4, !tbaa !3
  %343 = getelementptr inbounds double, double* %294, i64 %340
  %344 = load double, double* %343, align 8, !tbaa !13
  %345 = shl nsw i32 %342, 2
  %346 = sext i32 %345 to i64
  %347 = getelementptr inbounds double, double* %6, i64 %346
  %348 = bitcast double* %347 to <4 x double>*
  %349 = load <4 x double>, <4 x double>* %348, align 8, !tbaa !13
  %350 = insertelement <4 x double> undef, double %344, i32 0
  %351 = shufflevector <4 x double> %350, <4 x double> undef, <4 x i32> zeroinitializer
  %352 = fmul <4 x double> %351, %349
  %353 = fsub <4 x double> %339, %352
  %354 = add nuw nsw i64 %324, 2
  %355 = add i64 %326, -2
  %356 = icmp eq i64 %355, 0
  br i1 %356, label %357, label %323

; <label>:357:                                    ; preds = %323, %317
  %358 = phi <4 x double> [ undef, %317 ], [ %353, %323 ]
  %359 = phi i64 [ 0, %317 ], [ %354, %323 ]
  %360 = phi <4 x double> [ %316, %317 ], [ %353, %323 ]
  %361 = icmp eq i64 %319, 0
  br i1 %361, label %376, label %362

; <label>:362:                                    ; preds = %357
  %363 = getelementptr inbounds double, double* %294, i64 %359
  %364 = load double, double* %363, align 8, !tbaa !13
  %365 = insertelement <4 x double> undef, double %364, i32 0
  %366 = shufflevector <4 x double> %365, <4 x double> undef, <4 x i32> zeroinitializer
  %367 = getelementptr inbounds i32, i32* %289, i64 %359
  %368 = load i32, i32* %367, align 4, !tbaa !3
  %369 = shl nsw i32 %368, 2
  %370 = sext i32 %369 to i64
  %371 = getelementptr inbounds double, double* %6, i64 %370
  %372 = bitcast double* %371 to <4 x double>*
  %373 = load <4 x double>, <4 x double>* %372, align 8, !tbaa !13
  %374 = fmul <4 x double> %366, %373
  %375 = fsub <4 x double> %360, %374
  br label %376

; <label>:376:                                    ; preds = %362, %357, %281
  %377 = phi <4 x double> [ %316, %281 ], [ %358, %357 ], [ %375, %362 ]
  %378 = getelementptr inbounds double, double* %4, i64 %282
  %379 = load double, double* %378, align 8, !tbaa !13
  %380 = extractelement <4 x double> %377, i32 0
  %381 = fdiv double %380, %379
  store double %381, double* %298, align 8, !tbaa !13
  %382 = extractelement <4 x double> %377, i32 1
  %383 = fdiv double %382, %379
  store double %383, double* %302, align 8, !tbaa !13
  %384 = extractelement <4 x double> %377, i32 2
  %385 = fdiv double %384, %379
  store double %385, double* %306, align 8, !tbaa !13
  %386 = extractelement <4 x double> %377, i32 3
  %387 = fdiv double %386, %379
  store double %387, double* %310, align 8, !tbaa !13
  %388 = add nuw nsw i64 %282, 1
  %389 = icmp eq i64 %388, %280
  br i1 %389, label %390, label %281

; <label>:390:                                    ; preds = %376, %266, %193, %104, %277, %203, %111, %8, %7
  ret void
}

; Function Attrs: nounwind readnone speculatable
declare <2 x double> @llvm.ceil.v2f64(<2 x double>) #4

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind readnone speculatable }
attributes #5 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !4, i64 0}
!4 = !{!"int", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C/C++ TBAA"}
!7 = !{!8, !8, i64 0}
!8 = !{!"any pointer", !5, i64 0}
!9 = !{!10, !4, i64 76}
!10 = !{!"klu_common_struct", !11, i64 0, !11, i64 8, !11, i64 16, !11, i64 24, !11, i64 32, !4, i64 40, !4, i64 44, !4, i64 48, !8, i64 56, !8, i64 64, !4, i64 72, !4, i64 76, !4, i64 80, !4, i64 84, !4, i64 88, !4, i64 92, !4, i64 96, !11, i64 104, !11, i64 112, !11, i64 120, !11, i64 128, !11, i64 136, !12, i64 144, !12, i64 152}
!11 = !{!"double", !5, i64 0}
!12 = !{!"long", !5, i64 0}
!13 = !{!11, !11, i64 0}
!14 = distinct !{!14, !15}
!15 = !{!"llvm.loop.unroll.disable"}
!16 = distinct !{!16, !15}
