; ModuleID = 'amd_info.c'
source_filename = "amd_info.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }

@SuiteSparse_config = external local_unnamed_addr global %struct.SuiteSparse_config_struct, align 8
@.str = private unnamed_addr constant [37 x i8] c"\0AAMD version %d.%d.%d, %s, results:\0A\00", align 1
@.str.1 = private unnamed_addr constant [12 x i8] c"May 4, 2016\00", align 1
@.str.2 = private unnamed_addr constant [13 x i8] c"    status: \00", align 1
@.str.3 = private unnamed_addr constant [4 x i8] c"OK\0A\00", align 1
@.str.4 = private unnamed_addr constant [15 x i8] c"out of memory\0A\00", align 1
@.str.5 = private unnamed_addr constant [16 x i8] c"invalid matrix\0A\00", align 1
@.str.6 = private unnamed_addr constant [17 x i8] c"OK, but jumbled\0A\00", align 1
@.str.7 = private unnamed_addr constant [9 x i8] c"unknown\0A\00", align 1
@.str.8 = private unnamed_addr constant [63 x i8] c"    n, dimension of A:                                  %.20g\0A\00", align 1
@.str.9 = private unnamed_addr constant [63 x i8] c"    nz, number of nonzeros in A:                        %.20g\0A\00", align 1
@.str.10 = private unnamed_addr constant [62 x i8] c"    symmetry of A:                                      %.4f\0A\00", align 1
@.str.11 = private unnamed_addr constant [63 x i8] c"    number of nonzeros on diagonal:                     %.20g\0A\00", align 1
@.str.12 = private unnamed_addr constant [63 x i8] c"    nonzeros in pattern of A+A' (excl. diagonal):       %.20g\0A\00", align 1
@.str.13 = private unnamed_addr constant [63 x i8] c"    # dense rows/columns of A+A':                       %.20g\0A\00", align 1
@.str.14 = private unnamed_addr constant [63 x i8] c"    memory used, in bytes:                              %.20g\0A\00", align 1
@.str.15 = private unnamed_addr constant [63 x i8] c"    # of memory compactions:                            %.20g\0A\00", align 1
@.str.16 = private unnamed_addr constant [233 x i8] c"\0A    The following approximate statistics are for a subsequent\0A    factorization of A(P,P) + A(P,P)'.  They are slight upper\0A    bounds if there are no dense rows/columns in A+A', and become\0A    looser if dense rows/columns exist.\0A\0A\00", align 1
@.str.17 = private unnamed_addr constant [63 x i8] c"    nonzeros in L (excluding diagonal):                 %.20g\0A\00", align 1
@.str.18 = private unnamed_addr constant [63 x i8] c"    nonzeros in L (including diagonal):                 %.20g\0A\00", align 1
@.str.19 = private unnamed_addr constant [63 x i8] c"    # divide operations for LDL' or LU:                 %.20g\0A\00", align 1
@.str.20 = private unnamed_addr constant [63 x i8] c"    # multiply-subtract operations for LDL':            %.20g\0A\00", align 1
@.str.21 = private unnamed_addr constant [63 x i8] c"    # multiply-subtract operations for LU:              %.20g\0A\00", align 1
@.str.22 = private unnamed_addr constant [63 x i8] c"    max nz. in any column of L (incl. diagonal):        %.20g\0A\00", align 1
@.str.23 = private unnamed_addr constant [313 x i8] c"\0A    chol flop count for real A, sqrt counted as 1 flop: %.20g\0A    LDL' flop count for real A:                         %.20g\0A    LDL' flop count for complex A:                      %.20g\0A    LU flop count for real A (with no pivoting):        %.20g\0A    LU flop count for complex A (with no pivoting):     %.20g\0A\0A\00", align 1

; Function Attrs: nounwind ssp uwtable
define void @amd_info(double* readonly) local_unnamed_addr #0 {
  %2 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %3 = icmp eq i32 (i8*, ...)* %2, null
  br i1 %3, label %6, label %4

; <label>:4:                                      ; preds = %1
  %5 = tail call i32 (i8*, ...) %2(i8* getelementptr inbounds ([37 x i8], [37 x i8]* @.str, i64 0, i64 0), i32 2, i32 4, i32 6, i8* getelementptr inbounds ([12 x i8], [12 x i8]* @.str.1, i64 0, i64 0)) #1
  br label %6

; <label>:6:                                      ; preds = %1, %4
  %7 = icmp eq double* %0, null
  br i1 %7, label %212, label %8

; <label>:8:                                      ; preds = %6
  %9 = getelementptr inbounds double, double* %0, i64 1
  %10 = load double, double* %9, align 8, !tbaa !8
  %11 = getelementptr inbounds double, double* %0, i64 10
  %12 = bitcast double* %11 to <2 x double>*
  %13 = load <2 x double>, <2 x double>* %12, align 8, !tbaa !8
  %14 = getelementptr inbounds double, double* %0, i64 12
  %15 = load double, double* %14, align 8, !tbaa !8
  %16 = getelementptr inbounds double, double* %0, i64 9
  %17 = load double, double* %16, align 8, !tbaa !8
  %18 = fcmp oge double %10, 0.000000e+00
  %19 = fcmp oge double %17, 0.000000e+00
  %20 = and i1 %18, %19
  %21 = fadd double %10, %17
  %22 = select i1 %20, double %21, double -1.000000e+00
  %23 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %24 = icmp eq i32 (i8*, ...)* %23, null
  br i1 %24, label %27, label %25

; <label>:25:                                     ; preds = %8
  %26 = tail call i32 (i8*, ...) %23(i8* getelementptr inbounds ([13 x i8], [13 x i8]* @.str.2, i64 0, i64 0)) #1
  br label %27

; <label>:27:                                     ; preds = %8, %25
  %28 = load double, double* %0, align 8, !tbaa !8
  %29 = fcmp oeq double %28, 0.000000e+00
  br i1 %29, label %30, label %35

; <label>:30:                                     ; preds = %27
  %31 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %32 = icmp eq i32 (i8*, ...)* %31, null
  br i1 %32, label %59, label %33

; <label>:33:                                     ; preds = %30
  %34 = tail call i32 (i8*, ...) %31(i8* getelementptr inbounds ([4 x i8], [4 x i8]* @.str.3, i64 0, i64 0)) #1
  br label %59

; <label>:35:                                     ; preds = %27
  %36 = fcmp oeq double %28, -1.000000e+00
  br i1 %36, label %37, label %42

; <label>:37:                                     ; preds = %35
  %38 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %39 = icmp eq i32 (i8*, ...)* %38, null
  br i1 %39, label %59, label %40

; <label>:40:                                     ; preds = %37
  %41 = tail call i32 (i8*, ...) %38(i8* getelementptr inbounds ([15 x i8], [15 x i8]* @.str.4, i64 0, i64 0)) #1
  br label %59

; <label>:42:                                     ; preds = %35
  %43 = fcmp oeq double %28, -2.000000e+00
  br i1 %43, label %44, label %49

; <label>:44:                                     ; preds = %42
  %45 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %46 = icmp eq i32 (i8*, ...)* %45, null
  br i1 %46, label %59, label %47

; <label>:47:                                     ; preds = %44
  %48 = tail call i32 (i8*, ...) %45(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @.str.5, i64 0, i64 0)) #1
  br label %59

; <label>:49:                                     ; preds = %42
  %50 = fcmp oeq double %28, 1.000000e+00
  %51 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  %52 = icmp ne i32 (i8*, ...)* %51, null
  br i1 %50, label %53, label %56

; <label>:53:                                     ; preds = %49
  br i1 %52, label %54, label %59

; <label>:54:                                     ; preds = %53
  %55 = tail call i32 (i8*, ...) %51(i8* getelementptr inbounds ([17 x i8], [17 x i8]* @.str.6, i64 0, i64 0)) #1
  br label %59

; <label>:56:                                     ; preds = %49
  br i1 %52, label %57, label %59

; <label>:57:                                     ; preds = %56
  %58 = tail call i32 (i8*, ...) %51(i8* getelementptr inbounds ([9 x i8], [9 x i8]* @.str.7, i64 0, i64 0)) #1
  br label %59

; <label>:59:                                     ; preds = %30, %37, %44, %40, %54, %53, %57, %56, %47, %33
  %60 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  %61 = icmp ne i32 (i8*, ...)* %60, null
  %62 = and i1 %18, %61
  br i1 %62, label %63, label %66

; <label>:63:                                     ; preds = %59
  %64 = tail call i32 (i8*, ...) %60(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.8, i64 0, i64 0), double %10) #1
  %65 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %66

; <label>:66:                                     ; preds = %63, %59
  %67 = phi i32 (i8*, ...)* [ %65, %63 ], [ %60, %59 ]
  %68 = getelementptr inbounds double, double* %0, i64 2
  %69 = load double, double* %68, align 8, !tbaa !8
  %70 = fcmp oge double %69, 0.000000e+00
  %71 = icmp ne i32 (i8*, ...)* %67, null
  %72 = and i1 %70, %71
  br i1 %72, label %73, label %76

; <label>:73:                                     ; preds = %66
  %74 = tail call i32 (i8*, ...) %67(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.9, i64 0, i64 0), double %69) #1
  %75 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %76

; <label>:76:                                     ; preds = %73, %66
  %77 = phi i32 (i8*, ...)* [ %75, %73 ], [ %67, %66 ]
  %78 = getelementptr inbounds double, double* %0, i64 3
  %79 = load double, double* %78, align 8, !tbaa !8
  %80 = fcmp oge double %79, 0.000000e+00
  %81 = icmp ne i32 (i8*, ...)* %77, null
  %82 = and i1 %80, %81
  br i1 %82, label %83, label %86

; <label>:83:                                     ; preds = %76
  %84 = tail call i32 (i8*, ...) %77(i8* getelementptr inbounds ([62 x i8], [62 x i8]* @.str.10, i64 0, i64 0), double %79) #1
  %85 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %86

; <label>:86:                                     ; preds = %83, %76
  %87 = phi i32 (i8*, ...)* [ %85, %83 ], [ %77, %76 ]
  %88 = getelementptr inbounds double, double* %0, i64 4
  %89 = load double, double* %88, align 8, !tbaa !8
  %90 = fcmp oge double %89, 0.000000e+00
  %91 = icmp ne i32 (i8*, ...)* %87, null
  %92 = and i1 %90, %91
  br i1 %92, label %93, label %96

; <label>:93:                                     ; preds = %86
  %94 = tail call i32 (i8*, ...) %87(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.11, i64 0, i64 0), double %89) #1
  %95 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %96

; <label>:96:                                     ; preds = %93, %86
  %97 = phi i32 (i8*, ...)* [ %95, %93 ], [ %87, %86 ]
  %98 = getelementptr inbounds double, double* %0, i64 5
  %99 = load double, double* %98, align 8, !tbaa !8
  %100 = fcmp oge double %99, 0.000000e+00
  %101 = icmp ne i32 (i8*, ...)* %97, null
  %102 = and i1 %100, %101
  br i1 %102, label %103, label %106

; <label>:103:                                    ; preds = %96
  %104 = tail call i32 (i8*, ...) %97(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.12, i64 0, i64 0), double %99) #1
  %105 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %106

; <label>:106:                                    ; preds = %103, %96
  %107 = phi i32 (i8*, ...)* [ %105, %103 ], [ %97, %96 ]
  %108 = getelementptr inbounds double, double* %0, i64 6
  %109 = load double, double* %108, align 8, !tbaa !8
  %110 = fcmp oge double %109, 0.000000e+00
  %111 = icmp ne i32 (i8*, ...)* %107, null
  %112 = and i1 %110, %111
  br i1 %112, label %113, label %116

; <label>:113:                                    ; preds = %106
  %114 = tail call i32 (i8*, ...) %107(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.13, i64 0, i64 0), double %109) #1
  %115 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %116

; <label>:116:                                    ; preds = %113, %106
  %117 = phi i32 (i8*, ...)* [ %115, %113 ], [ %107, %106 ]
  %118 = getelementptr inbounds double, double* %0, i64 7
  %119 = load double, double* %118, align 8, !tbaa !8
  %120 = fcmp oge double %119, 0.000000e+00
  %121 = icmp ne i32 (i8*, ...)* %117, null
  %122 = and i1 %120, %121
  br i1 %122, label %123, label %126

; <label>:123:                                    ; preds = %116
  %124 = tail call i32 (i8*, ...) %117(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.14, i64 0, i64 0), double %119) #1
  %125 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %126

; <label>:126:                                    ; preds = %123, %116
  %127 = phi i32 (i8*, ...)* [ %125, %123 ], [ %117, %116 ]
  %128 = getelementptr inbounds double, double* %0, i64 8
  %129 = load double, double* %128, align 8, !tbaa !8
  %130 = fcmp oge double %129, 0.000000e+00
  %131 = icmp ne i32 (i8*, ...)* %127, null
  %132 = and i1 %130, %131
  br i1 %132, label %133, label %136

; <label>:133:                                    ; preds = %126
  %134 = tail call i32 (i8*, ...) %127(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.15, i64 0, i64 0), double %129) #1
  %135 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !3
  br label %136

; <label>:136:                                    ; preds = %133, %126
  %137 = phi i32 (i8*, ...)* [ %135, %133 ], [ %127, %126 ]
  %138 = icmp eq i32 (i8*, ...)* %137, null
  br i1 %138, label %212, label %139

; <label>:139:                                    ; preds = %136
  %140 = tail call i32 (i8*, ...) %137(i8* getelementptr inbounds ([233 x i8], [233 x i8]* @.str.16, i64 0, i64 0)) #1
  %141 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  %142 = icmp ne i32 (i8*, ...)* %141, null
  %143 = and i1 %19, %142
  br i1 %143, label %144, label %147

; <label>:144:                                    ; preds = %139
  %145 = tail call i32 (i8*, ...) %141(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.17, i64 0, i64 0), double %17) #1
  %146 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %147

; <label>:147:                                    ; preds = %144, %139
  %148 = phi i32 (i8*, ...)* [ %146, %144 ], [ %141, %139 ]
  %149 = fcmp oge double %22, 0.000000e+00
  %150 = icmp ne i32 (i8*, ...)* %148, null
  %151 = and i1 %149, %150
  br i1 %151, label %152, label %155

; <label>:152:                                    ; preds = %147
  %153 = tail call i32 (i8*, ...) %148(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.18, i64 0, i64 0), double %22) #1
  %154 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %155

; <label>:155:                                    ; preds = %152, %147
  %156 = phi i32 (i8*, ...)* [ %154, %152 ], [ %148, %147 ]
  %157 = extractelement <2 x double> %13, i32 0
  %158 = fcmp oge double %157, 0.000000e+00
  %159 = icmp ne i32 (i8*, ...)* %156, null
  %160 = and i1 %158, %159
  br i1 %160, label %161, label %164

; <label>:161:                                    ; preds = %155
  %162 = tail call i32 (i8*, ...) %156(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.19, i64 0, i64 0), double %157) #1
  %163 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %164

; <label>:164:                                    ; preds = %161, %155
  %165 = phi i32 (i8*, ...)* [ %163, %161 ], [ %156, %155 ]
  %166 = extractelement <2 x double> %13, i32 1
  %167 = fcmp oge double %166, 0.000000e+00
  %168 = icmp ne i32 (i8*, ...)* %165, null
  %169 = and i1 %167, %168
  br i1 %169, label %170, label %173

; <label>:170:                                    ; preds = %164
  %171 = tail call i32 (i8*, ...) %165(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.20, i64 0, i64 0), double %166) #1
  %172 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %173

; <label>:173:                                    ; preds = %170, %164
  %174 = phi i32 (i8*, ...)* [ %172, %170 ], [ %165, %164 ]
  %175 = fcmp oge double %15, 0.000000e+00
  %176 = icmp ne i32 (i8*, ...)* %174, null
  %177 = and i1 %175, %176
  br i1 %177, label %178, label %181

; <label>:178:                                    ; preds = %173
  %179 = tail call i32 (i8*, ...) %174(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.21, i64 0, i64 0), double %15) #1
  %180 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %181

; <label>:181:                                    ; preds = %178, %173
  %182 = phi i32 (i8*, ...)* [ %180, %178 ], [ %174, %173 ]
  %183 = getelementptr inbounds double, double* %0, i64 13
  %184 = load double, double* %183, align 8, !tbaa !8
  %185 = fcmp oge double %184, 0.000000e+00
  %186 = icmp ne i32 (i8*, ...)* %182, null
  %187 = and i1 %185, %186
  br i1 %187, label %188, label %191

; <label>:188:                                    ; preds = %181
  %189 = tail call i32 (i8*, ...) %182(i8* getelementptr inbounds ([63 x i8], [63 x i8]* @.str.22, i64 0, i64 0), double %184) #1
  %190 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8
  br label %191

; <label>:191:                                    ; preds = %188, %181
  %192 = phi i32 (i8*, ...)* [ %190, %188 ], [ %182, %181 ]
  %193 = and i1 %18, %158
  %194 = and i1 %193, %167
  %195 = and i1 %194, %175
  %196 = icmp ne i32 (i8*, ...)* %192, null
  %197 = and i1 %195, %196
  br i1 %197, label %198, label %212

; <label>:198:                                    ; preds = %191
  %199 = fadd double %10, %157
  %200 = fmul double %166, 2.000000e+00
  %201 = fadd double %199, %200
  %202 = fadd double %157, %200
  %203 = fmul <2 x double> %13, <double 9.000000e+00, double 8.000000e+00>
  %204 = extractelement <2 x double> %203, i32 0
  %205 = extractelement <2 x double> %203, i32 1
  %206 = fadd double %204, %205
  %207 = fmul double %15, 2.000000e+00
  %208 = fadd double %157, %207
  %209 = fmul double %15, 8.000000e+00
  %210 = fadd double %204, %209
  %211 = tail call i32 (i8*, ...) %192(i8* getelementptr inbounds ([313 x i8], [313 x i8]* @.str.23, i64 0, i64 0), double %201, double %202, double %206, double %208, double %210) #1
  br label %212

; <label>:212:                                    ; preds = %136, %191, %198, %6
  ret void
}

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!3 = !{!4, !5, i64 32}
!4 = !{!"SuiteSparse_config_struct", !5, i64 0, !5, i64 8, !5, i64 16, !5, i64 24, !5, i64 32, !5, i64 40, !5, i64 48}
!5 = !{!"any pointer", !6, i64 0}
!6 = !{!"omnipotent char", !7, i64 0}
!7 = !{!"Simple C/C++ TBAA"}
!8 = !{!9, !9, i64 0}
!9 = !{!"double", !6, i64 0}
