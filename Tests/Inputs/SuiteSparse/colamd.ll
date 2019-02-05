; ModuleID = 'colamd.c'
source_filename = "colamd.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.SuiteSparse_config_struct = type { i8* (i64)*, i8* (i64, i64)*, i8* (i8*, i64)*, void (i8*)*, i32 (i8*, ...)*, double (double, double)*, i32 (double, double, double, double, double*, double*)* }
%struct.Colamd_Col_struct = type { i32, i32, %union.anon.1, %union.anon.2, %union.anon.3, %union.anon.4 }
%union.anon.1 = type { i32 }
%union.anon.2 = type { i32 }
%union.anon.3 = type { i32 }
%union.anon.4 = type { i32 }
%struct.Colamd_Row_struct = type { i32, i32, %union.anon, %union.anon.0 }
%union.anon = type { i32 }
%union.anon.0 = type { i32 }

@.str = private unnamed_addr constant [7 x i8] c"colamd\00", align 1
@.str.1 = private unnamed_addr constant [7 x i8] c"symamd\00", align 1
@SuiteSparse_config = external local_unnamed_addr global %struct.SuiteSparse_config_struct, align 8
@.str.2 = private unnamed_addr constant [24 x i8] c"\0A%s version %d.%d, %s: \00", align 1
@.str.3 = private unnamed_addr constant [12 x i8] c"May 4, 2016\00", align 1
@.str.4 = private unnamed_addr constant [26 x i8] c"No statistics available.\0A\00", align 1
@.str.5 = private unnamed_addr constant [6 x i8] c"OK.  \00", align 1
@.str.6 = private unnamed_addr constant [9 x i8] c"ERROR.  \00", align 1
@.str.7 = private unnamed_addr constant [47 x i8] c"Matrix has unsorted or duplicate row indices.\0A\00", align 1
@.str.8 = private unnamed_addr constant [57 x i8] c"%s: number of duplicate or out-of-order row indices: %d\0A\00", align 1
@.str.9 = private unnamed_addr constant [57 x i8] c"%s: last seen duplicate or out-of-order row index:   %d\0A\00", align 1
@.str.10 = private unnamed_addr constant [56 x i8] c"%s: last seen in column:                             %d\00", align 1
@.str.11 = private unnamed_addr constant [2 x i8] c"\0A\00", align 1
@.str.12 = private unnamed_addr constant [57 x i8] c"%s: number of dense or empty rows ignored:           %d\0A\00", align 1
@.str.13 = private unnamed_addr constant [57 x i8] c"%s: number of dense or empty columns ignored:        %d\0A\00", align 1
@.str.14 = private unnamed_addr constant [57 x i8] c"%s: number of garbage collections performed:         %d\0A\00", align 1
@.str.15 = private unnamed_addr constant [46 x i8] c"Array A (row indices of matrix) not present.\0A\00", align 1
@.str.16 = private unnamed_addr constant [51 x i8] c"Array p (column pointers for matrix) not present.\0A\00", align 1
@.str.17 = private unnamed_addr constant [30 x i8] c"Invalid number of rows (%d).\0A\00", align 1
@.str.18 = private unnamed_addr constant [33 x i8] c"Invalid number of columns (%d).\0A\00", align 1
@.str.19 = private unnamed_addr constant [41 x i8] c"Invalid number of nonzero entries (%d).\0A\00", align 1
@.str.20 = private unnamed_addr constant [51 x i8] c"Invalid column pointer, p [0] = %d, must be zero.\0A\00", align 1
@.str.21 = private unnamed_addr constant [20 x i8] c"Array A too small.\0A\00", align 1
@.str.22 = private unnamed_addr constant [52 x i8] c"        Need Alen >= %d, but given only Alen = %d.\0A\00", align 1
@.str.23 = private unnamed_addr constant [58 x i8] c"Column %d has a negative number of nonzero entries (%d).\0A\00", align 1
@.str.24 = private unnamed_addr constant [59 x i8] c"Row index (row %d) out of bounds (%d to %d) in column %d.\0A\00", align 1
@.str.25 = private unnamed_addr constant [16 x i8] c"Out of memory.\0A\00", align 1

; Function Attrs: nounwind readnone ssp uwtable
define i64 @colamd_recommended(i32, i32, i32) local_unnamed_addr #0 {
  %4 = or i32 %1, %0
  %5 = or i32 %4, %2
  %6 = icmp slt i32 %5, 0
  br i1 %6, label %51, label %280

; <label>:7:                                      ; preds = %280
  %8 = add nsw i64 %285, 1
  %9 = icmp ugt i64 %285, 1
  %10 = select i1 %9, i64 %285, i64 1
  %11 = icmp uge i64 %8, %10
  %12 = select i1 %11, i64 %8, i64 0
  br i1 %11, label %142, label %45

; <label>:13:                                     ; preds = %272
  %14 = add nsw i64 %279, 1
  %15 = icmp ugt i64 %279, 1
  %16 = select i1 %15, i64 %279, i64 1
  %17 = icmp uge i64 %14, %16
  %18 = select i1 %17, i64 %14, i64 0
  br i1 %17, label %53, label %45

; <label>:19:                                     ; preds = %135
  %20 = add nsw i64 %278, %284
  %21 = icmp ugt i64 %284, %278
  %22 = select i1 %21, i64 %284, i64 %278
  %23 = icmp uge i64 %20, %22
  %24 = select i1 %23, i64 %20, i64 0
  br i1 %23, label %25, label %45

; <label>:25:                                     ; preds = %19
  %26 = add i64 %141, %24
  %27 = icmp ugt i64 %24, %141
  %28 = select i1 %27, i64 %24, i64 %141
  %29 = icmp uge i64 %26, %28
  %30 = select i1 %29, i64 %26, i64 0
  br i1 %29, label %31, label %45

; <label>:31:                                     ; preds = %25
  %32 = add i64 %30, %285
  %33 = icmp ugt i64 %30, %285
  %34 = select i1 %33, i64 %30, i64 %285
  %35 = icmp uge i64 %32, %34
  %36 = select i1 %35, i64 %32, i64 0
  %37 = sdiv i32 %0, 5
  %38 = sext i32 %37 to i64
  br i1 %35, label %39, label %45

; <label>:39:                                     ; preds = %31
  %40 = add i64 %36, %38
  %41 = icmp ugt i64 %36, %38
  %42 = select i1 %41, i64 %36, i64 %38
  %43 = icmp uge i64 %40, %42
  %44 = select i1 %43, i64 %40, i64 0
  br label %45

; <label>:45:                                     ; preds = %135, %129, %123, %117, %111, %105, %99, %93, %87, %81, %75, %69, %63, %57, %53, %13, %272, %266, %260, %254, %248, %242, %236, %230, %224, %218, %212, %206, %200, %194, %188, %182, %176, %170, %164, %158, %152, %146, %142, %7, %280, %31, %25, %19, %39
  %46 = phi i1 [ %43, %39 ], [ false, %31 ], [ false, %25 ], [ false, %19 ], [ false, %135 ], [ false, %129 ], [ false, %123 ], [ false, %117 ], [ false, %111 ], [ false, %105 ], [ false, %99 ], [ false, %93 ], [ false, %87 ], [ false, %81 ], [ false, %75 ], [ false, %69 ], [ false, %63 ], [ false, %57 ], [ false, %53 ], [ false, %272 ], [ false, %13 ], [ false, %266 ], [ false, %260 ], [ false, %254 ], [ false, %248 ], [ false, %242 ], [ false, %236 ], [ false, %230 ], [ false, %224 ], [ false, %218 ], [ false, %212 ], [ false, %206 ], [ false, %200 ], [ false, %194 ], [ false, %188 ], [ false, %182 ], [ false, %176 ], [ false, %170 ], [ false, %164 ], [ false, %158 ], [ false, %152 ], [ false, %146 ], [ false, %142 ], [ false, %280 ], [ false, %7 ]
  %47 = phi i64 [ %44, %39 ], [ 0, %31 ], [ 0, %25 ], [ 0, %19 ], [ 0, %135 ], [ 0, %129 ], [ 0, %123 ], [ 0, %117 ], [ 0, %111 ], [ 0, %105 ], [ 0, %99 ], [ 0, %93 ], [ 0, %87 ], [ 0, %81 ], [ 0, %75 ], [ 0, %69 ], [ 0, %63 ], [ 0, %57 ], [ 0, %53 ], [ 0, %272 ], [ 0, %13 ], [ 0, %266 ], [ 0, %260 ], [ 0, %254 ], [ 0, %248 ], [ 0, %242 ], [ 0, %236 ], [ 0, %230 ], [ 0, %224 ], [ 0, %218 ], [ 0, %212 ], [ 0, %206 ], [ 0, %200 ], [ 0, %194 ], [ 0, %188 ], [ 0, %182 ], [ 0, %176 ], [ 0, %170 ], [ 0, %164 ], [ 0, %158 ], [ 0, %152 ], [ 0, %146 ], [ 0, %142 ], [ 0, %280 ], [ 0, %7 ]
  %48 = icmp ult i64 %47, 2147483647
  %49 = and i1 %46, %48
  %50 = select i1 %49, i64 %47, i64 0
  br label %51

; <label>:51:                                     ; preds = %3, %45
  %52 = phi i64 [ %50, %45 ], [ 0, %3 ]
  ret i64 %52

; <label>:53:                                     ; preds = %13
  %54 = shl nsw i64 %18, 1
  %55 = icmp uge i64 %54, %18
  %56 = select i1 %55, i64 %54, i64 0
  br i1 %55, label %57, label %45

; <label>:57:                                     ; preds = %53
  %58 = add nsw i64 %56, %18
  %59 = icmp ugt i64 %56, %18
  %60 = select i1 %59, i64 %56, i64 %18
  %61 = icmp uge i64 %58, %60
  %62 = select i1 %61, i64 %58, i64 0
  br i1 %61, label %63, label %45

; <label>:63:                                     ; preds = %57
  %64 = add i64 %62, %18
  %65 = icmp ugt i64 %62, %18
  %66 = select i1 %65, i64 %62, i64 %18
  %67 = icmp uge i64 %64, %66
  %68 = select i1 %67, i64 %64, i64 0
  br i1 %67, label %69, label %45

; <label>:69:                                     ; preds = %63
  %70 = add i64 %68, %18
  %71 = icmp ugt i64 %68, %18
  %72 = select i1 %71, i64 %68, i64 %18
  %73 = icmp uge i64 %70, %72
  %74 = select i1 %73, i64 %70, i64 0
  br i1 %73, label %75, label %45

; <label>:75:                                     ; preds = %69
  %76 = add i64 %74, %18
  %77 = icmp ugt i64 %74, %18
  %78 = select i1 %77, i64 %74, i64 %18
  %79 = icmp uge i64 %76, %78
  %80 = select i1 %79, i64 %76, i64 0
  br i1 %79, label %81, label %45

; <label>:81:                                     ; preds = %75
  %82 = add i64 %80, %18
  %83 = icmp ugt i64 %80, %18
  %84 = select i1 %83, i64 %80, i64 %18
  %85 = icmp uge i64 %82, %84
  %86 = select i1 %85, i64 %82, i64 0
  br i1 %85, label %87, label %45

; <label>:87:                                     ; preds = %81
  %88 = add i64 %86, %18
  %89 = icmp ugt i64 %86, %18
  %90 = select i1 %89, i64 %86, i64 %18
  %91 = icmp uge i64 %88, %90
  %92 = select i1 %91, i64 %88, i64 0
  br i1 %91, label %93, label %45

; <label>:93:                                     ; preds = %87
  %94 = add i64 %92, %18
  %95 = icmp ugt i64 %92, %18
  %96 = select i1 %95, i64 %92, i64 %18
  %97 = icmp uge i64 %94, %96
  %98 = select i1 %97, i64 %94, i64 0
  br i1 %97, label %99, label %45

; <label>:99:                                     ; preds = %93
  %100 = add i64 %98, %18
  %101 = icmp ugt i64 %98, %18
  %102 = select i1 %101, i64 %98, i64 %18
  %103 = icmp uge i64 %100, %102
  %104 = select i1 %103, i64 %100, i64 0
  br i1 %103, label %105, label %45

; <label>:105:                                    ; preds = %99
  %106 = add i64 %104, %18
  %107 = icmp ugt i64 %104, %18
  %108 = select i1 %107, i64 %104, i64 %18
  %109 = icmp uge i64 %106, %108
  %110 = select i1 %109, i64 %106, i64 0
  br i1 %109, label %111, label %45

; <label>:111:                                    ; preds = %105
  %112 = add i64 %110, %18
  %113 = icmp ugt i64 %110, %18
  %114 = select i1 %113, i64 %110, i64 %18
  %115 = icmp uge i64 %112, %114
  %116 = select i1 %115, i64 %112, i64 0
  br i1 %115, label %117, label %45

; <label>:117:                                    ; preds = %111
  %118 = add i64 %116, %18
  %119 = icmp ugt i64 %116, %18
  %120 = select i1 %119, i64 %116, i64 %18
  %121 = icmp uge i64 %118, %120
  %122 = select i1 %121, i64 %118, i64 0
  br i1 %121, label %123, label %45

; <label>:123:                                    ; preds = %117
  %124 = add i64 %122, %18
  %125 = icmp ugt i64 %122, %18
  %126 = select i1 %125, i64 %122, i64 %18
  %127 = icmp uge i64 %124, %126
  %128 = select i1 %127, i64 %124, i64 0
  br i1 %127, label %129, label %45

; <label>:129:                                    ; preds = %123
  %130 = add i64 %128, %18
  %131 = icmp ugt i64 %128, %18
  %132 = select i1 %131, i64 %128, i64 %18
  %133 = icmp uge i64 %130, %132
  %134 = select i1 %133, i64 %130, i64 0
  br i1 %133, label %135, label %45

; <label>:135:                                    ; preds = %129
  %136 = add i64 %134, %18
  %137 = icmp ugt i64 %134, %18
  %138 = select i1 %137, i64 %134, i64 %18
  %139 = icmp uge i64 %136, %138
  %140 = lshr i64 %136, 2
  %141 = select i1 %139, i64 %140, i64 0
  br i1 %139, label %19, label %45

; <label>:142:                                    ; preds = %7
  %143 = shl nsw i64 %12, 1
  %144 = icmp uge i64 %143, %12
  %145 = select i1 %144, i64 %143, i64 0
  br i1 %144, label %146, label %45

; <label>:146:                                    ; preds = %142
  %147 = add nsw i64 %145, %12
  %148 = icmp ugt i64 %145, %12
  %149 = select i1 %148, i64 %145, i64 %12
  %150 = icmp uge i64 %147, %149
  %151 = select i1 %150, i64 %147, i64 0
  br i1 %150, label %152, label %45

; <label>:152:                                    ; preds = %146
  %153 = add i64 %151, %12
  %154 = icmp ugt i64 %151, %12
  %155 = select i1 %154, i64 %151, i64 %12
  %156 = icmp uge i64 %153, %155
  %157 = select i1 %156, i64 %153, i64 0
  br i1 %156, label %158, label %45

; <label>:158:                                    ; preds = %152
  %159 = add i64 %157, %12
  %160 = icmp ugt i64 %157, %12
  %161 = select i1 %160, i64 %157, i64 %12
  %162 = icmp uge i64 %159, %161
  %163 = select i1 %162, i64 %159, i64 0
  br i1 %162, label %164, label %45

; <label>:164:                                    ; preds = %158
  %165 = add i64 %163, %12
  %166 = icmp ugt i64 %163, %12
  %167 = select i1 %166, i64 %163, i64 %12
  %168 = icmp uge i64 %165, %167
  %169 = select i1 %168, i64 %165, i64 0
  br i1 %168, label %170, label %45

; <label>:170:                                    ; preds = %164
  %171 = add i64 %169, %12
  %172 = icmp ugt i64 %169, %12
  %173 = select i1 %172, i64 %169, i64 %12
  %174 = icmp uge i64 %171, %173
  %175 = select i1 %174, i64 %171, i64 0
  br i1 %174, label %176, label %45

; <label>:176:                                    ; preds = %170
  %177 = add i64 %175, %12
  %178 = icmp ugt i64 %175, %12
  %179 = select i1 %178, i64 %175, i64 %12
  %180 = icmp uge i64 %177, %179
  %181 = select i1 %180, i64 %177, i64 0
  br i1 %180, label %182, label %45

; <label>:182:                                    ; preds = %176
  %183 = add i64 %181, %12
  %184 = icmp ugt i64 %181, %12
  %185 = select i1 %184, i64 %181, i64 %12
  %186 = icmp uge i64 %183, %185
  %187 = select i1 %186, i64 %183, i64 0
  br i1 %186, label %188, label %45

; <label>:188:                                    ; preds = %182
  %189 = add i64 %187, %12
  %190 = icmp ugt i64 %187, %12
  %191 = select i1 %190, i64 %187, i64 %12
  %192 = icmp uge i64 %189, %191
  %193 = select i1 %192, i64 %189, i64 0
  br i1 %192, label %194, label %45

; <label>:194:                                    ; preds = %188
  %195 = add i64 %193, %12
  %196 = icmp ugt i64 %193, %12
  %197 = select i1 %196, i64 %193, i64 %12
  %198 = icmp uge i64 %195, %197
  %199 = select i1 %198, i64 %195, i64 0
  br i1 %198, label %200, label %45

; <label>:200:                                    ; preds = %194
  %201 = add i64 %199, %12
  %202 = icmp ugt i64 %199, %12
  %203 = select i1 %202, i64 %199, i64 %12
  %204 = icmp uge i64 %201, %203
  %205 = select i1 %204, i64 %201, i64 0
  br i1 %204, label %206, label %45

; <label>:206:                                    ; preds = %200
  %207 = add i64 %205, %12
  %208 = icmp ugt i64 %205, %12
  %209 = select i1 %208, i64 %205, i64 %12
  %210 = icmp uge i64 %207, %209
  %211 = select i1 %210, i64 %207, i64 0
  br i1 %210, label %212, label %45

; <label>:212:                                    ; preds = %206
  %213 = add i64 %211, %12
  %214 = icmp ugt i64 %211, %12
  %215 = select i1 %214, i64 %211, i64 %12
  %216 = icmp uge i64 %213, %215
  %217 = select i1 %216, i64 %213, i64 0
  br i1 %216, label %218, label %45

; <label>:218:                                    ; preds = %212
  %219 = add i64 %217, %12
  %220 = icmp ugt i64 %217, %12
  %221 = select i1 %220, i64 %217, i64 %12
  %222 = icmp uge i64 %219, %221
  %223 = select i1 %222, i64 %219, i64 0
  br i1 %222, label %224, label %45

; <label>:224:                                    ; preds = %218
  %225 = add i64 %223, %12
  %226 = icmp ugt i64 %223, %12
  %227 = select i1 %226, i64 %223, i64 %12
  %228 = icmp uge i64 %225, %227
  %229 = select i1 %228, i64 %225, i64 0
  br i1 %228, label %230, label %45

; <label>:230:                                    ; preds = %224
  %231 = add i64 %229, %12
  %232 = icmp ugt i64 %229, %12
  %233 = select i1 %232, i64 %229, i64 %12
  %234 = icmp uge i64 %231, %233
  %235 = select i1 %234, i64 %231, i64 0
  br i1 %234, label %236, label %45

; <label>:236:                                    ; preds = %230
  %237 = add i64 %235, %12
  %238 = icmp ugt i64 %235, %12
  %239 = select i1 %238, i64 %235, i64 %12
  %240 = icmp uge i64 %237, %239
  %241 = select i1 %240, i64 %237, i64 0
  br i1 %240, label %242, label %45

; <label>:242:                                    ; preds = %236
  %243 = add i64 %241, %12
  %244 = icmp ugt i64 %241, %12
  %245 = select i1 %244, i64 %241, i64 %12
  %246 = icmp uge i64 %243, %245
  %247 = select i1 %246, i64 %243, i64 0
  br i1 %246, label %248, label %45

; <label>:248:                                    ; preds = %242
  %249 = add i64 %247, %12
  %250 = icmp ugt i64 %247, %12
  %251 = select i1 %250, i64 %247, i64 %12
  %252 = icmp uge i64 %249, %251
  %253 = select i1 %252, i64 %249, i64 0
  br i1 %252, label %254, label %45

; <label>:254:                                    ; preds = %248
  %255 = add i64 %253, %12
  %256 = icmp ugt i64 %253, %12
  %257 = select i1 %256, i64 %253, i64 %12
  %258 = icmp uge i64 %255, %257
  %259 = select i1 %258, i64 %255, i64 0
  br i1 %258, label %260, label %45

; <label>:260:                                    ; preds = %254
  %261 = add i64 %259, %12
  %262 = icmp ugt i64 %259, %12
  %263 = select i1 %262, i64 %259, i64 %12
  %264 = icmp uge i64 %261, %263
  %265 = select i1 %264, i64 %261, i64 0
  br i1 %264, label %266, label %45

; <label>:266:                                    ; preds = %260
  %267 = add i64 %265, %12
  %268 = icmp ugt i64 %265, %12
  %269 = select i1 %268, i64 %265, i64 %12
  %270 = icmp uge i64 %267, %269
  %271 = select i1 %270, i64 %267, i64 0
  br i1 %270, label %272, label %45

; <label>:272:                                    ; preds = %266
  %273 = add i64 %271, %12
  %274 = icmp ugt i64 %271, %12
  %275 = select i1 %274, i64 %271, i64 %12
  %276 = icmp uge i64 %273, %275
  %277 = lshr i64 %273, 2
  %278 = select i1 %276, i64 %277, i64 0
  %279 = sext i32 %1 to i64
  br i1 %276, label %13, label %45

; <label>:280:                                    ; preds = %3
  %281 = sext i32 %0 to i64
  %282 = shl nsw i64 %281, 1
  %283 = icmp uge i64 %282, %281
  %284 = select i1 %283, i64 %282, i64 0
  %285 = sext i32 %2 to i64
  br i1 %283, label %7, label %45
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: norecurse nounwind ssp uwtable
define void @colamd_set_defaults(double*) local_unnamed_addr #2 {
  %2 = icmp eq double* %0, null
  br i1 %2, label %8, label %3

; <label>:3:                                      ; preds = %1
  %4 = getelementptr inbounds double, double* %0, i64 3
  %5 = bitcast double* %4 to i8*
  call void @llvm.memset.p0i8.i64(i8* %5, i8 0, i64 136, i32 8, i1 false)
  %6 = bitcast double* %0 to <2 x double>*
  store <2 x double> <double 1.000000e+01, double 1.000000e+01>, <2 x double>* %6, align 8, !tbaa !3
  %7 = getelementptr inbounds double, double* %0, i64 2
  store double 1.000000e+00, double* %7, align 8, !tbaa !3
  br label %8

; <label>:8:                                      ; preds = %1, %3
  ret void
}

; Function Attrs: nounwind ssp uwtable
define i32 @symamd(i32, i32* readonly, i32* readonly, i32*, double* readonly, i32*, i8* (i64, i64)* nocapture, void (i8*)* nocapture) local_unnamed_addr #3 {
  %9 = bitcast i32* %3 to i8*
  %10 = alloca [20 x double], align 16
  %11 = bitcast [20 x double]* %10 to i8*
  %12 = alloca [20 x double], align 16
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %11) #5
  %13 = bitcast [20 x double]* %12 to i8*
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %13) #5
  %14 = icmp eq i32* %5, null
  br i1 %14, label %434, label %15

; <label>:15:                                     ; preds = %8
  %16 = getelementptr inbounds i32, i32* %5, i64 3
  %17 = getelementptr inbounds i32, i32* %5, i64 4
  %18 = bitcast i32* %5 to i8*
  call void @llvm.memset.p0i8.i64(i8* %18, i8 0, i64 80, i32 4, i1 false)
  store i32 -1, i32* %17, align 4, !tbaa !7
  %19 = getelementptr inbounds i32, i32* %5, i64 5
  store i32 -1, i32* %19, align 4, !tbaa !7
  %20 = icmp eq i32* %1, null
  br i1 %20, label %21, label %22

; <label>:21:                                     ; preds = %15
  store i32 -1, i32* %16, align 4, !tbaa !7
  br label %434

; <label>:22:                                     ; preds = %15
  %23 = icmp eq i32* %2, null
  br i1 %23, label %24, label %25

; <label>:24:                                     ; preds = %22
  store i32 -2, i32* %16, align 4, !tbaa !7
  br label %434

; <label>:25:                                     ; preds = %22
  %26 = icmp slt i32 %0, 0
  br i1 %26, label %27, label %28

; <label>:27:                                     ; preds = %25
  store i32 -4, i32* %16, align 4, !tbaa !7
  store i32 %0, i32* %17, align 4, !tbaa !7
  br label %434

; <label>:28:                                     ; preds = %25
  %29 = sext i32 %0 to i64
  %30 = getelementptr inbounds i32, i32* %2, i64 %29
  %31 = load i32, i32* %30, align 4, !tbaa !7
  %32 = icmp slt i32 %31, 0
  br i1 %32, label %33, label %34

; <label>:33:                                     ; preds = %28
  store i32 -5, i32* %16, align 4, !tbaa !7
  store i32 %31, i32* %17, align 4, !tbaa !7
  br label %434

; <label>:34:                                     ; preds = %28
  %35 = load i32, i32* %2, align 4, !tbaa !7
  %36 = icmp eq i32 %35, 0
  br i1 %36, label %39, label %37

; <label>:37:                                     ; preds = %34
  store i32 -6, i32* %16, align 4, !tbaa !7
  %38 = load i32, i32* %2, align 4, !tbaa !7
  store i32 %38, i32* %17, align 4, !tbaa !7
  br label %434

; <label>:39:                                     ; preds = %34
  %40 = icmp eq double* %4, null
  br i1 %40, label %41, label %47

; <label>:41:                                     ; preds = %39
  %42 = getelementptr inbounds [20 x double], [20 x double]* %12, i64 0, i64 0
  %43 = getelementptr inbounds [20 x double], [20 x double]* %12, i64 0, i64 3
  %44 = bitcast double* %43 to i8*
  call void @llvm.memset.p0i8.i64(i8* nonnull %44, i8 0, i64 136, i32 8, i1 false) #5
  %45 = bitcast [20 x double]* %12 to <2 x double>*
  store <2 x double> <double 1.000000e+01, double 1.000000e+01>, <2 x double>* %45, align 16, !tbaa !3
  %46 = getelementptr inbounds [20 x double], [20 x double]* %12, i64 0, i64 2
  store double 1.000000e+00, double* %46, align 16, !tbaa !3
  br label %47

; <label>:47:                                     ; preds = %39, %41
  %48 = phi double* [ %4, %39 ], [ %42, %41 ]
  %49 = bitcast double* %48 to i8*
  %50 = add i32 %0, 1
  %51 = sext i32 %50 to i64
  %52 = tail call i8* %6(i64 %51, i64 4) #5
  %53 = bitcast i8* %52 to i32*
  %54 = icmp eq i8* %52, null
  br i1 %54, label %55, label %56

; <label>:55:                                     ; preds = %47
  store i32 -10, i32* %16, align 4, !tbaa !7
  br label %434

; <label>:56:                                     ; preds = %47
  %57 = tail call i8* %6(i64 %51, i64 4) #5
  %58 = bitcast i8* %57 to i32*
  %59 = icmp eq i8* %57, null
  br i1 %59, label %60, label %61

; <label>:60:                                     ; preds = %56
  store i32 -10, i32* %16, align 4, !tbaa !7
  tail call void %7(i8* nonnull %52) #5
  br label %434

; <label>:61:                                     ; preds = %56
  %62 = getelementptr inbounds i32, i32* %5, i64 6
  store i32 0, i32* %62, align 4, !tbaa !7
  %63 = icmp sgt i32 %0, 0
  br i1 %63, label %65, label %64

; <label>:64:                                     ; preds = %61
  store i32 0, i32* %3, align 4, !tbaa !7
  br label %314

; <label>:65:                                     ; preds = %61
  %66 = zext i32 %0 to i64
  %67 = shl nuw nsw i64 %66, 2
  call void @llvm.memset.p0i8.i64(i8* nonnull %57, i8 -1, i64 %67, i32 4, i1 false)
  %68 = load i32, i32* %2, align 4, !tbaa !7
  br label %69

; <label>:69:                                     ; preds = %65, %125
  %70 = phi i32 [ %68, %65 ], [ %126, %125 ]
  %71 = phi i64 [ 0, %65 ], [ %72, %125 ]
  %72 = add nuw nsw i64 %71, 1
  %73 = getelementptr inbounds i32, i32* %2, i64 %72
  %74 = load i32, i32* %73, align 4, !tbaa !7
  %75 = sub nsw i32 %74, %70
  %76 = icmp slt i32 %75, 0
  br i1 %76, label %77, label %79

; <label>:77:                                     ; preds = %69
  %78 = trunc i64 %71 to i32
  store i32 -8, i32* %16, align 4, !tbaa !7
  store i32 %78, i32* %17, align 4, !tbaa !7
  store i32 %75, i32* %19, align 4, !tbaa !7
  tail call void %7(i8* nonnull %52) #5
  tail call void %7(i8* nonnull %57) #5
  br label %434

; <label>:79:                                     ; preds = %69
  %80 = icmp sgt i32 %74, %70
  br i1 %80, label %81, label %125

; <label>:81:                                     ; preds = %79
  %82 = getelementptr inbounds i32, i32* %53, i64 %71
  %83 = sext i32 %70 to i64
  %84 = trunc i64 %71 to i32
  %85 = trunc i64 %71 to i32
  br label %86

; <label>:86:                                     ; preds = %81, %120
  %87 = phi i64 [ %83, %81 ], [ %121, %120 ]
  %88 = phi i32 [ -1, %81 ], [ %90, %120 ]
  %89 = getelementptr inbounds i32, i32* %1, i64 %87
  %90 = load i32, i32* %89, align 4, !tbaa !7
  %91 = icmp sgt i32 %90, -1
  %92 = icmp slt i32 %90, %0
  %93 = and i1 %91, %92
  br i1 %93, label %96, label %94

; <label>:94:                                     ; preds = %86
  %95 = trunc i64 %71 to i32
  store i32 -9, i32* %16, align 4, !tbaa !7
  store i32 %95, i32* %17, align 4, !tbaa !7
  store i32 %90, i32* %19, align 4, !tbaa !7
  store i32 %0, i32* %62, align 4, !tbaa !7
  tail call void %7(i8* nonnull %52) #5
  tail call void %7(i8* nonnull %57) #5
  br label %434

; <label>:96:                                     ; preds = %86
  %97 = icmp sgt i32 %90, %88
  %98 = sext i32 %90 to i64
  br i1 %97, label %99, label %104

; <label>:99:                                     ; preds = %96
  %100 = getelementptr inbounds i32, i32* %58, i64 %98
  %101 = load i32, i32* %100, align 4, !tbaa !7
  %102 = zext i32 %101 to i64
  %103 = icmp eq i64 %71, %102
  br i1 %103, label %104, label %107

; <label>:104:                                    ; preds = %96, %99
  store i32 1, i32* %16, align 4, !tbaa !7
  store i32 %84, i32* %17, align 4, !tbaa !7
  store i32 %90, i32* %19, align 4, !tbaa !7
  %105 = load i32, i32* %62, align 4, !tbaa !7
  %106 = add nsw i32 %105, 1
  store i32 %106, i32* %62, align 4, !tbaa !7
  br label %107

; <label>:107:                                    ; preds = %104, %99
  %108 = icmp slt i64 %71, %98
  %109 = getelementptr inbounds i32, i32* %58, i64 %98
  br i1 %108, label %110, label %120

; <label>:110:                                    ; preds = %107
  %111 = load i32, i32* %109, align 4, !tbaa !7
  %112 = zext i32 %111 to i64
  %113 = icmp eq i64 %71, %112
  br i1 %113, label %120, label %114

; <label>:114:                                    ; preds = %110
  %115 = getelementptr inbounds i32, i32* %53, i64 %98
  %116 = load i32, i32* %115, align 4, !tbaa !7
  %117 = add nsw i32 %116, 1
  store i32 %117, i32* %115, align 4, !tbaa !7
  %118 = load i32, i32* %82, align 4, !tbaa !7
  %119 = add nsw i32 %118, 1
  store i32 %119, i32* %82, align 4, !tbaa !7
  br label %120

; <label>:120:                                    ; preds = %107, %110, %114
  store i32 %85, i32* %109, align 4, !tbaa !7
  %121 = add nsw i64 %87, 1
  %122 = load i32, i32* %73, align 4, !tbaa !7
  %123 = sext i32 %122 to i64
  %124 = icmp slt i64 %121, %123
  br i1 %124, label %86, label %125

; <label>:125:                                    ; preds = %120, %79
  %126 = phi i32 [ %74, %79 ], [ %122, %120 ]
  %127 = icmp slt i64 %72, %29
  br i1 %127, label %69, label %128

; <label>:128:                                    ; preds = %125
  store i32 0, i32* %3, align 4, !tbaa !7
  %129 = icmp slt i32 %0, 1
  br i1 %129, label %183, label %130

; <label>:130:                                    ; preds = %128
  %131 = zext i32 %50 to i64
  %132 = add nsw i64 %131, -2
  %133 = and i32 %0, 3
  %134 = zext i32 %133 to i64
  %135 = icmp ult i64 %132, 3
  br i1 %135, label %166, label %136

; <label>:136:                                    ; preds = %130
  %137 = add nsw i64 %131, -1
  %138 = sub nsw i64 %137, %134
  br label %139

; <label>:139:                                    ; preds = %139, %136
  %140 = phi i32 [ 0, %136 ], [ %161, %139 ]
  %141 = phi i64 [ 1, %136 ], [ %163, %139 ]
  %142 = phi i64 [ %138, %136 ], [ %164, %139 ]
  %143 = add nsw i64 %141, -1
  %144 = getelementptr inbounds i32, i32* %53, i64 %143
  %145 = load i32, i32* %144, align 4, !tbaa !7
  %146 = add nsw i32 %145, %140
  %147 = getelementptr inbounds i32, i32* %3, i64 %141
  store i32 %146, i32* %147, align 4, !tbaa !7
  %148 = add nuw nsw i64 %141, 1
  %149 = getelementptr inbounds i32, i32* %53, i64 %141
  %150 = load i32, i32* %149, align 4, !tbaa !7
  %151 = add nsw i32 %150, %146
  %152 = getelementptr inbounds i32, i32* %3, i64 %148
  store i32 %151, i32* %152, align 4, !tbaa !7
  %153 = add nuw nsw i64 %141, 2
  %154 = getelementptr inbounds i32, i32* %53, i64 %148
  %155 = load i32, i32* %154, align 4, !tbaa !7
  %156 = add nsw i32 %155, %151
  %157 = getelementptr inbounds i32, i32* %3, i64 %153
  store i32 %156, i32* %157, align 4, !tbaa !7
  %158 = add nuw nsw i64 %141, 3
  %159 = getelementptr inbounds i32, i32* %53, i64 %153
  %160 = load i32, i32* %159, align 4, !tbaa !7
  %161 = add nsw i32 %160, %156
  %162 = getelementptr inbounds i32, i32* %3, i64 %158
  store i32 %161, i32* %162, align 4, !tbaa !7
  %163 = add nuw nsw i64 %141, 4
  %164 = add i64 %142, -4
  %165 = icmp eq i64 %164, 0
  br i1 %165, label %166, label %139

; <label>:166:                                    ; preds = %139, %130
  %167 = phi i32 [ 0, %130 ], [ %161, %139 ]
  %168 = phi i64 [ 1, %130 ], [ %163, %139 ]
  %169 = icmp eq i32 %133, 0
  br i1 %169, label %183, label %170

; <label>:170:                                    ; preds = %166
  br label %171

; <label>:171:                                    ; preds = %171, %170
  %172 = phi i32 [ %178, %171 ], [ %167, %170 ]
  %173 = phi i64 [ %180, %171 ], [ %168, %170 ]
  %174 = phi i64 [ %181, %171 ], [ %134, %170 ]
  %175 = add nsw i64 %173, -1
  %176 = getelementptr inbounds i32, i32* %53, i64 %175
  %177 = load i32, i32* %176, align 4, !tbaa !7
  %178 = add nsw i32 %177, %172
  %179 = getelementptr inbounds i32, i32* %3, i64 %173
  store i32 %178, i32* %179, align 4, !tbaa !7
  %180 = add nuw nsw i64 %173, 1
  %181 = add i64 %174, -1
  %182 = icmp eq i64 %181, 0
  br i1 %182, label %183, label %171, !llvm.loop !9

; <label>:183:                                    ; preds = %166, %171, %128
  br i1 %63, label %184, label %314

; <label>:184:                                    ; preds = %183
  %185 = zext i32 %0 to i64
  %186 = icmp ult i32 %0, 8
  br i1 %186, label %275, label %187

; <label>:187:                                    ; preds = %184
  %188 = shl nuw nsw i64 %185, 2
  %189 = getelementptr i8, i8* %52, i64 %188
  %190 = getelementptr i32, i32* %3, i64 %185
  %191 = bitcast i32* %190 to i8*
  %192 = icmp ult i8* %52, %191
  %193 = icmp ugt i8* %189, %9
  %194 = and i1 %192, %193
  br i1 %194, label %275, label %195

; <label>:195:                                    ; preds = %187
  %196 = and i64 %185, 4294967288
  %197 = add nsw i64 %196, -8
  %198 = lshr exact i64 %197, 3
  %199 = add nuw nsw i64 %198, 1
  %200 = and i64 %199, 3
  %201 = icmp ult i64 %197, 24
  br i1 %201, label %253, label %202

; <label>:202:                                    ; preds = %195
  %203 = sub nsw i64 %199, %200
  br label %204

; <label>:204:                                    ; preds = %204, %202
  %205 = phi i64 [ 0, %202 ], [ %250, %204 ]
  %206 = phi i64 [ %203, %202 ], [ %251, %204 ]
  %207 = getelementptr inbounds i32, i32* %3, i64 %205
  %208 = bitcast i32* %207 to <4 x i32>*
  %209 = load <4 x i32>, <4 x i32>* %208, align 4, !tbaa !7, !alias.scope !11
  %210 = getelementptr i32, i32* %207, i64 4
  %211 = bitcast i32* %210 to <4 x i32>*
  %212 = load <4 x i32>, <4 x i32>* %211, align 4, !tbaa !7, !alias.scope !11
  %213 = getelementptr inbounds i32, i32* %53, i64 %205
  %214 = bitcast i32* %213 to <4 x i32>*
  store <4 x i32> %209, <4 x i32>* %214, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %215 = getelementptr i32, i32* %213, i64 4
  %216 = bitcast i32* %215 to <4 x i32>*
  store <4 x i32> %212, <4 x i32>* %216, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %217 = or i64 %205, 8
  %218 = getelementptr inbounds i32, i32* %3, i64 %217
  %219 = bitcast i32* %218 to <4 x i32>*
  %220 = load <4 x i32>, <4 x i32>* %219, align 4, !tbaa !7, !alias.scope !11
  %221 = getelementptr i32, i32* %218, i64 4
  %222 = bitcast i32* %221 to <4 x i32>*
  %223 = load <4 x i32>, <4 x i32>* %222, align 4, !tbaa !7, !alias.scope !11
  %224 = getelementptr inbounds i32, i32* %53, i64 %217
  %225 = bitcast i32* %224 to <4 x i32>*
  store <4 x i32> %220, <4 x i32>* %225, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %226 = getelementptr i32, i32* %224, i64 4
  %227 = bitcast i32* %226 to <4 x i32>*
  store <4 x i32> %223, <4 x i32>* %227, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %228 = or i64 %205, 16
  %229 = getelementptr inbounds i32, i32* %3, i64 %228
  %230 = bitcast i32* %229 to <4 x i32>*
  %231 = load <4 x i32>, <4 x i32>* %230, align 4, !tbaa !7, !alias.scope !11
  %232 = getelementptr i32, i32* %229, i64 4
  %233 = bitcast i32* %232 to <4 x i32>*
  %234 = load <4 x i32>, <4 x i32>* %233, align 4, !tbaa !7, !alias.scope !11
  %235 = getelementptr inbounds i32, i32* %53, i64 %228
  %236 = bitcast i32* %235 to <4 x i32>*
  store <4 x i32> %231, <4 x i32>* %236, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %237 = getelementptr i32, i32* %235, i64 4
  %238 = bitcast i32* %237 to <4 x i32>*
  store <4 x i32> %234, <4 x i32>* %238, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %239 = or i64 %205, 24
  %240 = getelementptr inbounds i32, i32* %3, i64 %239
  %241 = bitcast i32* %240 to <4 x i32>*
  %242 = load <4 x i32>, <4 x i32>* %241, align 4, !tbaa !7, !alias.scope !11
  %243 = getelementptr i32, i32* %240, i64 4
  %244 = bitcast i32* %243 to <4 x i32>*
  %245 = load <4 x i32>, <4 x i32>* %244, align 4, !tbaa !7, !alias.scope !11
  %246 = getelementptr inbounds i32, i32* %53, i64 %239
  %247 = bitcast i32* %246 to <4 x i32>*
  store <4 x i32> %242, <4 x i32>* %247, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %248 = getelementptr i32, i32* %246, i64 4
  %249 = bitcast i32* %248 to <4 x i32>*
  store <4 x i32> %245, <4 x i32>* %249, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %250 = add i64 %205, 32
  %251 = add i64 %206, -4
  %252 = icmp eq i64 %251, 0
  br i1 %252, label %253, label %204, !llvm.loop !16

; <label>:253:                                    ; preds = %204, %195
  %254 = phi i64 [ 0, %195 ], [ %250, %204 ]
  %255 = icmp eq i64 %200, 0
  br i1 %255, label %273, label %256

; <label>:256:                                    ; preds = %253
  br label %257

; <label>:257:                                    ; preds = %257, %256
  %258 = phi i64 [ %254, %256 ], [ %270, %257 ]
  %259 = phi i64 [ %200, %256 ], [ %271, %257 ]
  %260 = getelementptr inbounds i32, i32* %3, i64 %258
  %261 = bitcast i32* %260 to <4 x i32>*
  %262 = load <4 x i32>, <4 x i32>* %261, align 4, !tbaa !7, !alias.scope !11
  %263 = getelementptr i32, i32* %260, i64 4
  %264 = bitcast i32* %263 to <4 x i32>*
  %265 = load <4 x i32>, <4 x i32>* %264, align 4, !tbaa !7, !alias.scope !11
  %266 = getelementptr inbounds i32, i32* %53, i64 %258
  %267 = bitcast i32* %266 to <4 x i32>*
  store <4 x i32> %262, <4 x i32>* %267, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %268 = getelementptr i32, i32* %266, i64 4
  %269 = bitcast i32* %268 to <4 x i32>*
  store <4 x i32> %265, <4 x i32>* %269, align 4, !tbaa !7, !alias.scope !14, !noalias !11
  %270 = add i64 %258, 8
  %271 = add i64 %259, -1
  %272 = icmp eq i64 %271, 0
  br i1 %272, label %273, label %257, !llvm.loop !18

; <label>:273:                                    ; preds = %257, %253
  %274 = icmp eq i64 %196, %185
  br i1 %274, label %314, label %275

; <label>:275:                                    ; preds = %273, %187, %184
  %276 = phi i64 [ 0, %187 ], [ 0, %184 ], [ %196, %273 ]
  %277 = add nsw i64 %185, -1
  %278 = sub nsw i64 %277, %276
  %279 = and i64 %185, 3
  %280 = icmp eq i64 %279, 0
  br i1 %280, label %291, label %281

; <label>:281:                                    ; preds = %275
  br label %282

; <label>:282:                                    ; preds = %282, %281
  %283 = phi i64 [ %288, %282 ], [ %276, %281 ]
  %284 = phi i64 [ %289, %282 ], [ %279, %281 ]
  %285 = getelementptr inbounds i32, i32* %3, i64 %283
  %286 = load i32, i32* %285, align 4, !tbaa !7
  %287 = getelementptr inbounds i32, i32* %53, i64 %283
  store i32 %286, i32* %287, align 4, !tbaa !7
  %288 = add nuw nsw i64 %283, 1
  %289 = add i64 %284, -1
  %290 = icmp eq i64 %289, 0
  br i1 %290, label %291, label %282, !llvm.loop !19

; <label>:291:                                    ; preds = %282, %275
  %292 = phi i64 [ %276, %275 ], [ %288, %282 ]
  %293 = icmp ult i64 %278, 3
  br i1 %293, label %314, label %294

; <label>:294:                                    ; preds = %291
  br label %295

; <label>:295:                                    ; preds = %295, %294
  %296 = phi i64 [ %292, %294 ], [ %312, %295 ]
  %297 = getelementptr inbounds i32, i32* %3, i64 %296
  %298 = load i32, i32* %297, align 4, !tbaa !7
  %299 = getelementptr inbounds i32, i32* %53, i64 %296
  store i32 %298, i32* %299, align 4, !tbaa !7
  %300 = add nuw nsw i64 %296, 1
  %301 = getelementptr inbounds i32, i32* %3, i64 %300
  %302 = load i32, i32* %301, align 4, !tbaa !7
  %303 = getelementptr inbounds i32, i32* %53, i64 %300
  store i32 %302, i32* %303, align 4, !tbaa !7
  %304 = add nsw i64 %296, 2
  %305 = getelementptr inbounds i32, i32* %3, i64 %304
  %306 = load i32, i32* %305, align 4, !tbaa !7
  %307 = getelementptr inbounds i32, i32* %53, i64 %304
  store i32 %306, i32* %307, align 4, !tbaa !7
  %308 = add nsw i64 %296, 3
  %309 = getelementptr inbounds i32, i32* %3, i64 %308
  %310 = load i32, i32* %309, align 4, !tbaa !7
  %311 = getelementptr inbounds i32, i32* %53, i64 %308
  store i32 %310, i32* %311, align 4, !tbaa !7
  %312 = add nsw i64 %296, 4
  %313 = icmp eq i64 %312, %185
  br i1 %313, label %314, label %295, !llvm.loop !20

; <label>:314:                                    ; preds = %291, %295, %273, %64, %183
  %315 = getelementptr inbounds i32, i32* %3, i64 %29
  %316 = load i32, i32* %315, align 4, !tbaa !7
  %317 = sdiv i32 %316, 2
  %318 = tail call i64 @colamd_recommended(i32 %316, i32 %317, i32 %0)
  %319 = tail call i8* %6(i64 %318, i64 4) #5
  %320 = bitcast i8* %319 to i32*
  %321 = icmp eq i8* %319, null
  br i1 %321, label %322, label %323

; <label>:322:                                    ; preds = %314
  store i32 -10, i32* %16, align 4, !tbaa !7
  tail call void %7(i8* nonnull %52) #5
  tail call void %7(i8* nonnull %57) #5
  br label %434

; <label>:323:                                    ; preds = %314
  %324 = load i32, i32* %16, align 4, !tbaa !7
  %325 = icmp eq i32 %324, 0
  br i1 %325, label %326, label %371

; <label>:326:                                    ; preds = %323
  br i1 %63, label %327, label %424

; <label>:327:                                    ; preds = %326
  %328 = load i32, i32* %2, align 4, !tbaa !7
  %329 = zext i32 %0 to i64
  br label %330

; <label>:330:                                    ; preds = %367, %327
  %331 = phi i32 [ %328, %327 ], [ %368, %367 ]
  %332 = phi i64 [ 0, %327 ], [ %334, %367 ]
  %333 = phi i32 [ 0, %327 ], [ %369, %367 ]
  %334 = add nuw nsw i64 %332, 1
  %335 = getelementptr inbounds i32, i32* %2, i64 %334
  %336 = load i32, i32* %335, align 4, !tbaa !7
  %337 = icmp slt i32 %331, %336
  br i1 %337, label %338, label %367

; <label>:338:                                    ; preds = %330
  %339 = getelementptr inbounds i32, i32* %53, i64 %332
  %340 = sext i32 %331 to i64
  br label %341

; <label>:341:                                    ; preds = %338, %361
  %342 = phi i32 [ %336, %338 ], [ %362, %361 ]
  %343 = phi i64 [ %340, %338 ], [ %364, %361 ]
  %344 = phi i32 [ %333, %338 ], [ %363, %361 ]
  %345 = getelementptr inbounds i32, i32* %1, i64 %343
  %346 = load i32, i32* %345, align 4, !tbaa !7
  %347 = sext i32 %346 to i64
  %348 = icmp slt i64 %332, %347
  br i1 %348, label %349, label %361

; <label>:349:                                    ; preds = %341
  %350 = getelementptr inbounds i32, i32* %53, i64 %347
  %351 = load i32, i32* %350, align 4, !tbaa !7
  %352 = add nsw i32 %351, 1
  store i32 %352, i32* %350, align 4, !tbaa !7
  %353 = sext i32 %351 to i64
  %354 = getelementptr inbounds i32, i32* %320, i64 %353
  store i32 %344, i32* %354, align 4, !tbaa !7
  %355 = load i32, i32* %339, align 4, !tbaa !7
  %356 = add nsw i32 %355, 1
  store i32 %356, i32* %339, align 4, !tbaa !7
  %357 = sext i32 %355 to i64
  %358 = getelementptr inbounds i32, i32* %320, i64 %357
  store i32 %344, i32* %358, align 4, !tbaa !7
  %359 = add nsw i32 %344, 1
  %360 = load i32, i32* %335, align 4, !tbaa !7
  br label %361

; <label>:361:                                    ; preds = %341, %349
  %362 = phi i32 [ %360, %349 ], [ %342, %341 ]
  %363 = phi i32 [ %359, %349 ], [ %344, %341 ]
  %364 = add nsw i64 %343, 1
  %365 = sext i32 %362 to i64
  %366 = icmp slt i64 %364, %365
  br i1 %366, label %341, label %367

; <label>:367:                                    ; preds = %361, %330
  %368 = phi i32 [ %336, %330 ], [ %362, %361 ]
  %369 = phi i32 [ %333, %330 ], [ %363, %361 ]
  %370 = icmp eq i64 %334, %329
  br i1 %370, label %424, label %330

; <label>:371:                                    ; preds = %323
  br i1 %63, label %372, label %424

; <label>:372:                                    ; preds = %371
  %373 = zext i32 %0 to i64
  %374 = shl nuw nsw i64 %373, 2
  call void @llvm.memset.p0i8.i64(i8* nonnull %57, i8 -1, i64 %374, i32 4, i1 false)
  %375 = load i32, i32* %2, align 4, !tbaa !7
  %376 = zext i32 %0 to i64
  br label %377

; <label>:377:                                    ; preds = %420, %372
  %378 = phi i32 [ %375, %372 ], [ %421, %420 ]
  %379 = phi i64 [ 0, %372 ], [ %381, %420 ]
  %380 = phi i32 [ 0, %372 ], [ %422, %420 ]
  %381 = add nuw nsw i64 %379, 1
  %382 = getelementptr inbounds i32, i32* %2, i64 %381
  %383 = load i32, i32* %382, align 4, !tbaa !7
  %384 = icmp slt i32 %378, %383
  br i1 %384, label %385, label %420

; <label>:385:                                    ; preds = %377
  %386 = getelementptr inbounds i32, i32* %53, i64 %379
  %387 = sext i32 %378 to i64
  %388 = trunc i64 %379 to i32
  br label %389

; <label>:389:                                    ; preds = %385, %414
  %390 = phi i32 [ %383, %385 ], [ %415, %414 ]
  %391 = phi i64 [ %387, %385 ], [ %417, %414 ]
  %392 = phi i32 [ %380, %385 ], [ %416, %414 ]
  %393 = getelementptr inbounds i32, i32* %1, i64 %391
  %394 = load i32, i32* %393, align 4, !tbaa !7
  %395 = sext i32 %394 to i64
  %396 = icmp slt i64 %379, %395
  br i1 %396, label %397, label %414

; <label>:397:                                    ; preds = %389
  %398 = getelementptr inbounds i32, i32* %58, i64 %395
  %399 = load i32, i32* %398, align 4, !tbaa !7
  %400 = zext i32 %399 to i64
  %401 = icmp eq i64 %379, %400
  br i1 %401, label %414, label %402

; <label>:402:                                    ; preds = %397
  %403 = getelementptr inbounds i32, i32* %53, i64 %395
  %404 = load i32, i32* %403, align 4, !tbaa !7
  %405 = add nsw i32 %404, 1
  store i32 %405, i32* %403, align 4, !tbaa !7
  %406 = sext i32 %404 to i64
  %407 = getelementptr inbounds i32, i32* %320, i64 %406
  store i32 %392, i32* %407, align 4, !tbaa !7
  %408 = load i32, i32* %386, align 4, !tbaa !7
  %409 = add nsw i32 %408, 1
  store i32 %409, i32* %386, align 4, !tbaa !7
  %410 = sext i32 %408 to i64
  %411 = getelementptr inbounds i32, i32* %320, i64 %410
  store i32 %392, i32* %411, align 4, !tbaa !7
  %412 = add nsw i32 %392, 1
  store i32 %388, i32* %398, align 4, !tbaa !7
  %413 = load i32, i32* %382, align 4, !tbaa !7
  br label %414

; <label>:414:                                    ; preds = %397, %389, %402
  %415 = phi i32 [ %413, %402 ], [ %390, %397 ], [ %390, %389 ]
  %416 = phi i32 [ %412, %402 ], [ %392, %397 ], [ %392, %389 ]
  %417 = add nsw i64 %391, 1
  %418 = sext i32 %415 to i64
  %419 = icmp slt i64 %417, %418
  br i1 %419, label %389, label %420

; <label>:420:                                    ; preds = %414, %377
  %421 = phi i32 [ %383, %377 ], [ %415, %414 ]
  %422 = phi i32 [ %380, %377 ], [ %416, %414 ]
  %423 = icmp eq i64 %381, %376
  br i1 %423, label %424, label %377

; <label>:424:                                    ; preds = %420, %367, %371, %326
  tail call void %7(i8* nonnull %52) #5
  tail call void %7(i8* nonnull %57) #5
  call void @llvm.memcpy.p0i8.p0i8.i64(i8* nonnull %11, i8* %49, i64 160, i32 8, i1 false)
  %425 = getelementptr inbounds [20 x double], [20 x double]* %10, i64 0, i64 0
  store double -1.000000e+00, double* %425, align 16, !tbaa !3
  %426 = bitcast double* %48 to i64*
  %427 = load i64, i64* %426, align 8, !tbaa !3
  %428 = getelementptr inbounds [20 x double], [20 x double]* %10, i64 0, i64 1
  %429 = bitcast double* %428 to i64*
  store i64 %427, i64* %429, align 8, !tbaa !3
  %430 = trunc i64 %318 to i32
  %431 = call i32 @colamd(i32 %317, i32 %0, i32 %430, i32* %320, i32* %3, double* nonnull %425, i32* nonnull %5)
  %432 = getelementptr inbounds i32, i32* %5, i64 1
  %433 = load i32, i32* %432, align 4, !tbaa !7
  store i32 %433, i32* %5, align 4, !tbaa !7
  call void %7(i8* nonnull %319) #5
  br label %434

; <label>:434:                                    ; preds = %8, %424, %322, %94, %77, %60, %55, %37, %33, %27, %24, %21
  %435 = phi i32 [ 0, %27 ], [ 0, %33 ], [ 0, %37 ], [ 0, %77 ], [ 0, %94 ], [ 1, %424 ], [ 0, %322 ], [ 0, %60 ], [ 0, %55 ], [ 0, %24 ], [ 0, %21 ], [ 0, %8 ]
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %13) #5
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %11) #5
  ret i32 %435
}

; Function Attrs: nounwind ssp uwtable
define i32 @colamd(i32, i32, i32, i32*, i32*, double* readonly, i32*) local_unnamed_addr #3 {
  %8 = ptrtoint i32* %3 to i64
  %9 = alloca [20 x double], align 16
  %10 = bitcast [20 x double]* %9 to i8*
  call void @llvm.lifetime.start.p0i8(i64 160, i8* nonnull %10) #5
  %11 = icmp eq i32* %6, null
  br i1 %11, label %1699, label %12

; <label>:12:                                     ; preds = %7
  %13 = getelementptr inbounds i32, i32* %6, i64 3
  %14 = getelementptr inbounds i32, i32* %6, i64 4
  %15 = bitcast i32* %6 to i8*
  call void @llvm.memset.p0i8.i64(i8* %15, i8 0, i64 80, i32 4, i1 false)
  store i32 -1, i32* %14, align 4, !tbaa !7
  %16 = getelementptr inbounds i32, i32* %6, i64 5
  store i32 -1, i32* %16, align 4, !tbaa !7
  %17 = icmp eq i32* %3, null
  br i1 %17, label %18, label %19

; <label>:18:                                     ; preds = %12
  store i32 -1, i32* %13, align 4, !tbaa !7
  br label %1699

; <label>:19:                                     ; preds = %12
  %20 = icmp eq i32* %4, null
  br i1 %20, label %21, label %22

; <label>:21:                                     ; preds = %19
  store i32 -2, i32* %13, align 4, !tbaa !7
  br label %1699

; <label>:22:                                     ; preds = %19
  %23 = icmp slt i32 %0, 0
  br i1 %23, label %24, label %25

; <label>:24:                                     ; preds = %22
  store i32 -3, i32* %13, align 4, !tbaa !7
  store i32 %0, i32* %14, align 4, !tbaa !7
  br label %1699

; <label>:25:                                     ; preds = %22
  %26 = icmp slt i32 %1, 0
  br i1 %26, label %27, label %28

; <label>:27:                                     ; preds = %25
  store i32 -4, i32* %13, align 4, !tbaa !7
  store i32 %1, i32* %14, align 4, !tbaa !7
  br label %1699

; <label>:28:                                     ; preds = %25
  %29 = sext i32 %1 to i64
  %30 = getelementptr inbounds i32, i32* %4, i64 %29
  %31 = load i32, i32* %30, align 4, !tbaa !7
  %32 = icmp slt i32 %31, 0
  br i1 %32, label %33, label %34

; <label>:33:                                     ; preds = %28
  store i32 -5, i32* %13, align 4, !tbaa !7
  store i32 %31, i32* %14, align 4, !tbaa !7
  br label %1699

; <label>:34:                                     ; preds = %28
  %35 = load i32, i32* %4, align 4, !tbaa !7
  %36 = icmp eq i32 %35, 0
  br i1 %36, label %39, label %37

; <label>:37:                                     ; preds = %34
  store i32 -6, i32* %13, align 4, !tbaa !7
  %38 = load i32, i32* %4, align 4, !tbaa !7
  store i32 %38, i32* %14, align 4, !tbaa !7
  br label %1699

; <label>:39:                                     ; preds = %34
  %40 = icmp eq double* %5, null
  br i1 %40, label %41, label %47

; <label>:41:                                     ; preds = %39
  %42 = getelementptr inbounds [20 x double], [20 x double]* %9, i64 0, i64 0
  %43 = getelementptr inbounds [20 x double], [20 x double]* %9, i64 0, i64 3
  %44 = bitcast double* %43 to i8*
  call void @llvm.memset.p0i8.i64(i8* nonnull %44, i8 0, i64 136, i32 8, i1 false) #5
  %45 = bitcast [20 x double]* %9 to <2 x double>*
  store <2 x double> <double 1.000000e+01, double 1.000000e+01>, <2 x double>* %45, align 16, !tbaa !3
  %46 = getelementptr inbounds [20 x double], [20 x double]* %9, i64 0, i64 2
  store double 1.000000e+00, double* %46, align 16, !tbaa !3
  br label %47

; <label>:47:                                     ; preds = %39, %41
  %48 = phi double* [ %5, %39 ], [ %42, %41 ]
  %49 = getelementptr inbounds double, double* %48, i64 2
  %50 = load double, double* %49, align 8, !tbaa !3
  %51 = add nsw i64 %29, 1
  %52 = icmp ugt i64 %29, 1
  %53 = select i1 %52, i64 %29, i64 1
  %54 = icmp uge i64 %51, %53
  %55 = select i1 %54, i64 %51, i64 0
  br i1 %54, label %1794, label %85

; <label>:56:                                     ; preds = %1924
  %57 = add nsw i64 %1932, 1
  %58 = icmp ugt i64 %1932, 1
  %59 = select i1 %58, i64 %1932, i64 1
  %60 = icmp uge i64 %57, %59
  %61 = select i1 %60, i64 %57, i64 0
  br i1 %60, label %1706, label %85

; <label>:62:                                     ; preds = %1701
  %63 = add nsw i64 %1704, %29
  %64 = icmp ugt i64 %1704, %29
  %65 = select i1 %64, i64 %1704, i64 %29
  %66 = icmp uge i64 %63, %65
  %67 = select i1 %66, i64 %63, i64 0
  br i1 %66, label %68, label %85

; <label>:68:                                     ; preds = %62
  %69 = add nsw i64 %67, %1931
  %70 = icmp ugt i64 %67, %1931
  %71 = select i1 %70, i64 %67, i64 %1931
  %72 = icmp uge i64 %69, %71
  %73 = select i1 %72, i64 %69, i64 0
  br i1 %72, label %74, label %85

; <label>:74:                                     ; preds = %68
  %75 = add i64 %73, %1705
  %76 = icmp ugt i64 %73, %1705
  %77 = select i1 %76, i64 %73, i64 %1705
  %78 = icmp uge i64 %75, %77
  %79 = select i1 %78, i64 %75, i64 0
  br i1 %78, label %80, label %85

; <label>:80:                                     ; preds = %74
  %81 = sext i32 %2 to i64
  %82 = icmp ugt i64 %79, %81
  %83 = icmp ugt i64 %79, 2147483647
  %84 = or i1 %82, %83
  br i1 %84, label %85, label %88

; <label>:85:                                     ; preds = %1788, %1701, %1782, %1776, %1770, %1764, %1758, %1752, %1746, %1740, %1734, %1728, %1722, %1716, %1710, %1706, %56, %1924, %1918, %1912, %1906, %1900, %1894, %1888, %1882, %1876, %1870, %1864, %1858, %1852, %1846, %1840, %1834, %1828, %1822, %1816, %1810, %1804, %1798, %1794, %47, %74, %68, %62, %80
  %86 = phi i64 [ 0, %74 ], [ %75, %80 ], [ 0, %68 ], [ 0, %62 ], [ 0, %1701 ], [ 0, %1788 ], [ 0, %1782 ], [ 0, %1776 ], [ 0, %1770 ], [ 0, %1764 ], [ 0, %1758 ], [ 0, %1752 ], [ 0, %1746 ], [ 0, %1740 ], [ 0, %1734 ], [ 0, %1728 ], [ 0, %1722 ], [ 0, %1716 ], [ 0, %1710 ], [ 0, %1706 ], [ 0, %1924 ], [ 0, %56 ], [ 0, %1918 ], [ 0, %1912 ], [ 0, %1906 ], [ 0, %1900 ], [ 0, %1894 ], [ 0, %1888 ], [ 0, %1882 ], [ 0, %1876 ], [ 0, %1870 ], [ 0, %1864 ], [ 0, %1858 ], [ 0, %1852 ], [ 0, %1846 ], [ 0, %1840 ], [ 0, %1834 ], [ 0, %1828 ], [ 0, %1822 ], [ 0, %1816 ], [ 0, %1810 ], [ 0, %1804 ], [ 0, %1798 ], [ 0, %1794 ], [ 0, %47 ]
  store i32 -7, i32* %13, align 4, !tbaa !7
  %87 = trunc i64 %86 to i32
  store i32 %87, i32* %14, align 4, !tbaa !7
  store i32 %2, i32* %16, align 4, !tbaa !7
  br label %1699

; <label>:88:                                     ; preds = %80
  %89 = add nuw nsw i64 %1705, %1931
  %90 = trunc i64 %89 to i32
  %91 = sub i32 %2, %90
  %92 = sext i32 %91 to i64
  %93 = getelementptr inbounds i32, i32* %3, i64 %92
  %94 = bitcast i32* %93 to %struct.Colamd_Col_struct*
  %95 = add nsw i64 %1931, %92
  %96 = getelementptr inbounds i32, i32* %3, i64 %95
  %97 = bitcast i32* %96 to %struct.Colamd_Row_struct*
  %98 = icmp sgt i32 %1, 0
  br i1 %98, label %99, label %130

; <label>:99:                                     ; preds = %88
  store i32 0, i32* %93, align 4, !tbaa !21
  %100 = getelementptr inbounds i32, i32* %4, i64 1
  %101 = load i32, i32* %100, align 4, !tbaa !7
  %102 = load i32, i32* %4, align 4, !tbaa !7
  %103 = sub nsw i32 %101, %102
  %104 = getelementptr inbounds i32, i32* %93, i64 1
  store i32 %103, i32* %104, align 4, !tbaa !23
  %105 = icmp slt i32 %103, 0
  br i1 %105, label %107, label %106

; <label>:106:                                    ; preds = %99
  br label %112

; <label>:107:                                    ; preds = %119, %99
  %108 = phi i64 [ 0, %99 ], [ %114, %119 ]
  %109 = phi i32* [ %104, %99 ], [ %128, %119 ]
  %110 = trunc i64 %108 to i32
  store i32 -8, i32* %13, align 4, !tbaa !7
  store i32 %110, i32* %14, align 4, !tbaa !7
  %111 = load i32, i32* %109, align 4, !tbaa !23
  store i32 %111, i32* %16, align 4, !tbaa !7
  br label %1699

; <label>:112:                                    ; preds = %106, %119
  %113 = phi i32* [ %124, %119 ], [ %100, %106 ]
  %114 = phi i64 [ %123, %119 ], [ 1, %106 ]
  %115 = phi i64 [ %114, %119 ], [ 0, %106 ]
  %116 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %115, i32 2, i32 0
  %117 = bitcast i32* %116 to <4 x i32>*
  store <4 x i32> <i32 1, i32 0, i32 -1, i32 -1>, <4 x i32>* %117, align 4, !tbaa !24
  %118 = icmp slt i64 %114, %29
  br i1 %118, label %119, label %130

; <label>:119:                                    ; preds = %112
  %120 = load i32, i32* %113, align 4, !tbaa !7
  %121 = getelementptr inbounds i32, i32* %4, i64 %114
  %122 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %114, i32 0
  store i32 %120, i32* %122, align 4, !tbaa !21
  %123 = add nuw nsw i64 %114, 1
  %124 = getelementptr inbounds i32, i32* %4, i64 %123
  %125 = load i32, i32* %124, align 4, !tbaa !7
  %126 = load i32, i32* %121, align 4, !tbaa !7
  %127 = sub nsw i32 %125, %126
  %128 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %114, i32 1
  store i32 %127, i32* %128, align 4, !tbaa !23
  %129 = icmp slt i32 %127, 0
  br i1 %129, label %107, label %112

; <label>:130:                                    ; preds = %112, %88
  %131 = getelementptr inbounds i32, i32* %6, i64 6
  store i32 0, i32* %131, align 4, !tbaa !7
  %132 = icmp sgt i32 %0, 0
  br i1 %132, label %133, label %156

; <label>:133:                                    ; preds = %130
  %134 = zext i32 %0 to i64
  %135 = and i64 %134, 1
  %136 = icmp eq i32 %0, 1
  br i1 %136, label %150, label %137

; <label>:137:                                    ; preds = %133
  %138 = sub nsw i64 %134, %135
  br label %139

; <label>:139:                                    ; preds = %139, %137
  %140 = phi i64 [ 0, %137 ], [ %147, %139 ]
  %141 = phi i64 [ %138, %137 ], [ %148, %139 ]
  %142 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %140, i32 1
  store i32 0, i32* %142, align 4, !tbaa !25
  %143 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %140, i32 3, i32 0
  store i32 -1, i32* %143, align 4, !tbaa !24
  %144 = or i64 %140, 1
  %145 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %144, i32 1
  store i32 0, i32* %145, align 4, !tbaa !25
  %146 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %144, i32 3, i32 0
  store i32 -1, i32* %146, align 4, !tbaa !24
  %147 = add nuw nsw i64 %140, 2
  %148 = add i64 %141, -2
  %149 = icmp eq i64 %148, 0
  br i1 %149, label %150, label %139

; <label>:150:                                    ; preds = %139, %133
  %151 = phi i64 [ 0, %133 ], [ %147, %139 ]
  %152 = icmp eq i64 %135, 0
  br i1 %152, label %156, label %153

; <label>:153:                                    ; preds = %150
  %154 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %151, i32 1
  store i32 0, i32* %154, align 4, !tbaa !25
  %155 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %151, i32 3, i32 0
  store i32 -1, i32* %155, align 4, !tbaa !24
  br label %156

; <label>:156:                                    ; preds = %153, %150, %130
  br i1 %98, label %157, label %208

; <label>:157:                                    ; preds = %156
  br label %158

; <label>:158:                                    ; preds = %157, %206
  %159 = phi i64 [ %162, %206 ], [ 0, %157 ]
  %160 = getelementptr inbounds i32, i32* %4, i64 %159
  %161 = load i32, i32* %160, align 4, !tbaa !7
  %162 = add nuw nsw i64 %159, 1
  %163 = getelementptr inbounds i32, i32* %4, i64 %162
  %164 = load i32, i32* %163, align 4, !tbaa !7
  %165 = sext i32 %164 to i64
  %166 = getelementptr inbounds i32, i32* %3, i64 %165
  %167 = icmp slt i32 %161, %164
  br i1 %167, label %168, label %206

; <label>:168:                                    ; preds = %158
  %169 = sext i32 %161 to i64
  %170 = getelementptr inbounds i32, i32* %3, i64 %169
  %171 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %159, i32 1
  %172 = trunc i64 %159 to i32
  br label %173

; <label>:173:                                    ; preds = %196, %168
  %174 = phi i32 [ -1, %168 ], [ %177, %196 ]
  %175 = phi i32* [ %170, %168 ], [ %176, %196 ]
  %176 = getelementptr inbounds i32, i32* %175, i64 1
  %177 = load i32, i32* %175, align 4, !tbaa !7
  %178 = icmp sgt i32 %177, -1
  %179 = icmp slt i32 %177, %0
  %180 = and i1 %178, %179
  br i1 %180, label %183, label %181

; <label>:181:                                    ; preds = %173
  %182 = trunc i64 %159 to i32
  store i32 -9, i32* %13, align 4, !tbaa !7
  store i32 %182, i32* %14, align 4, !tbaa !7
  store i32 %177, i32* %16, align 4, !tbaa !7
  store i32 %0, i32* %131, align 4, !tbaa !7
  br label %1699

; <label>:183:                                    ; preds = %173
  %184 = icmp sgt i32 %177, %174
  %185 = sext i32 %177 to i64
  %186 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %185, i32 3, i32 0
  br i1 %184, label %187, label %191

; <label>:187:                                    ; preds = %183
  %188 = load i32, i32* %186, align 4, !tbaa !24
  %189 = zext i32 %188 to i64
  %190 = icmp eq i64 %159, %189
  br i1 %190, label %191, label %196

; <label>:191:                                    ; preds = %187, %183
  store i32 1, i32* %13, align 4, !tbaa !7
  store i32 %172, i32* %14, align 4, !tbaa !7
  store i32 %177, i32* %16, align 4, !tbaa !7
  %192 = load i32, i32* %131, align 4, !tbaa !7
  %193 = add nsw i32 %192, 1
  store i32 %193, i32* %131, align 4, !tbaa !7
  %194 = load i32, i32* %186, align 4, !tbaa !24
  %195 = zext i32 %194 to i64
  br label %196

; <label>:196:                                    ; preds = %191, %187
  %197 = phi i64 [ %195, %191 ], [ %189, %187 ]
  %198 = icmp eq i64 %197, %159
  %199 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %185, i32 1
  %200 = select i1 %198, i32* %171, i32* %199
  %201 = select i1 %198, i32 -1, i32 1
  %202 = select i1 %198, i32* %171, i32* %199
  %203 = load i32, i32* %200, align 4, !tbaa !7
  %204 = add nsw i32 %203, %201
  store i32 %204, i32* %202, align 4, !tbaa !7
  store i32 %172, i32* %186, align 4, !tbaa !24
  %205 = icmp ult i32* %176, %166
  br i1 %205, label %173, label %206

; <label>:206:                                    ; preds = %196, %158
  %207 = icmp slt i64 %162, %29
  br i1 %207, label %158, label %208

; <label>:208:                                    ; preds = %206, %156
  %209 = load i32, i32* %30, align 4, !tbaa !7
  store i32 %209, i32* %96, align 4, !tbaa !27
  %210 = getelementptr inbounds i32, i32* %96, i64 2
  store i32 %209, i32* %210, align 4, !tbaa !24
  %211 = getelementptr inbounds i32, i32* %96, i64 3
  store i32 -1, i32* %211, align 4, !tbaa !24
  %212 = icmp sgt i32 %0, 1
  br i1 %212, label %213, label %227

; <label>:213:                                    ; preds = %208
  %214 = zext i32 %0 to i64
  br label %215

; <label>:215:                                    ; preds = %215, %213
  %216 = phi i32 [ %209, %213 ], [ %221, %215 ]
  %217 = phi i64 [ 1, %213 ], [ %225, %215 ]
  %218 = add nsw i64 %217, -1
  %219 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %218, i32 1
  %220 = load i32, i32* %219, align 4, !tbaa !25
  %221 = add nsw i32 %220, %216
  %222 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %217, i32 0
  store i32 %221, i32* %222, align 4, !tbaa !27
  %223 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %217, i32 2, i32 0
  store i32 %221, i32* %223, align 4, !tbaa !24
  %224 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %217, i32 3, i32 0
  store i32 -1, i32* %224, align 4, !tbaa !24
  %225 = add nuw nsw i64 %217, 1
  %226 = icmp eq i64 %225, %214
  br i1 %226, label %227, label %215

; <label>:227:                                    ; preds = %215, %208
  %228 = load i32, i32* %13, align 4, !tbaa !7
  %229 = icmp eq i32 %228, 1
  br i1 %229, label %230, label %266

; <label>:230:                                    ; preds = %227
  br i1 %98, label %231, label %329

; <label>:231:                                    ; preds = %230
  %232 = zext i32 %1 to i64
  br label %233

; <label>:233:                                    ; preds = %264, %231
  %234 = phi i64 [ 0, %231 ], [ %237, %264 ]
  %235 = getelementptr inbounds i32, i32* %4, i64 %234
  %236 = load i32, i32* %235, align 4, !tbaa !7
  %237 = add nuw nsw i64 %234, 1
  %238 = getelementptr inbounds i32, i32* %4, i64 %237
  %239 = load i32, i32* %238, align 4, !tbaa !7
  %240 = sext i32 %239 to i64
  %241 = getelementptr inbounds i32, i32* %3, i64 %240
  %242 = icmp slt i32 %236, %239
  br i1 %242, label %243, label %264

; <label>:243:                                    ; preds = %233
  %244 = sext i32 %236 to i64
  %245 = getelementptr inbounds i32, i32* %3, i64 %244
  %246 = trunc i64 %234 to i32
  br label %247

; <label>:247:                                    ; preds = %262, %243
  %248 = phi i32* [ %245, %243 ], [ %249, %262 ]
  %249 = getelementptr inbounds i32, i32* %248, i64 1
  %250 = load i32, i32* %248, align 4, !tbaa !7
  %251 = sext i32 %250 to i64
  %252 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %251, i32 3, i32 0
  %253 = load i32, i32* %252, align 4, !tbaa !24
  %254 = zext i32 %253 to i64
  %255 = icmp eq i64 %234, %254
  br i1 %255, label %262, label %256

; <label>:256:                                    ; preds = %247
  %257 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %251, i32 2, i32 0
  %258 = load i32, i32* %257, align 4, !tbaa !24
  %259 = add nsw i32 %258, 1
  store i32 %259, i32* %257, align 4, !tbaa !24
  %260 = sext i32 %258 to i64
  %261 = getelementptr inbounds i32, i32* %3, i64 %260
  store i32 %246, i32* %261, align 4, !tbaa !7
  store i32 %246, i32* %252, align 4, !tbaa !24
  br label %262

; <label>:262:                                    ; preds = %256, %247
  %263 = icmp ult i32* %249, %241
  br i1 %263, label %247, label %264

; <label>:264:                                    ; preds = %262, %233
  %265 = icmp eq i64 %237, %232
  br i1 %265, label %329, label %233

; <label>:266:                                    ; preds = %227
  br i1 %98, label %267, label %329

; <label>:267:                                    ; preds = %266
  %268 = zext i32 %1 to i64
  %269 = getelementptr i32, i32* %3, i64 1
  %270 = xor i64 %8, -1
  br label %271

; <label>:271:                                    ; preds = %327, %267
  %272 = phi i64 [ 0, %267 ], [ %275, %327 ]
  %273 = getelementptr inbounds i32, i32* %4, i64 %272
  %274 = load i32, i32* %273, align 4, !tbaa !7
  %275 = add nuw nsw i64 %272, 1
  %276 = getelementptr inbounds i32, i32* %4, i64 %275
  %277 = load i32, i32* %276, align 4, !tbaa !7
  %278 = sext i32 %277 to i64
  %279 = getelementptr inbounds i32, i32* %3, i64 %278
  %280 = icmp slt i32 %274, %277
  br i1 %280, label %281, label %327

; <label>:281:                                    ; preds = %271
  %282 = sext i32 %274 to i64
  %283 = getelementptr inbounds i32, i32* %3, i64 %282
  %284 = trunc i64 %272 to i32
  %285 = getelementptr i32, i32* %269, i64 %282
  %286 = icmp ugt i32* %285, %279
  %287 = select i1 %286, i32* %285, i32* %279
  %288 = bitcast i32* %287 to i8*
  %289 = getelementptr i8, i8* %288, i64 %270
  %290 = mul nsw i64 %282, -4
  %291 = getelementptr i8, i8* %289, i64 %290
  %292 = ptrtoint i8* %291 to i64
  %293 = and i64 %292, 4
  %294 = icmp eq i64 %293, 0
  br i1 %294, label %295, label %304

; <label>:295:                                    ; preds = %281
  %296 = getelementptr inbounds i32, i32* %283, i64 1
  %297 = load i32, i32* %283, align 4, !tbaa !7
  %298 = sext i32 %297 to i64
  %299 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %298, i32 2, i32 0
  %300 = load i32, i32* %299, align 4, !tbaa !24
  %301 = add nsw i32 %300, 1
  store i32 %301, i32* %299, align 4, !tbaa !24
  %302 = sext i32 %300 to i64
  %303 = getelementptr inbounds i32, i32* %3, i64 %302
  store i32 %284, i32* %303, align 4, !tbaa !7
  br label %304

; <label>:304:                                    ; preds = %295, %281
  %305 = phi i32* [ %296, %295 ], [ %283, %281 ]
  %306 = icmp ult i8* %291, inttoptr (i64 4 to i8*)
  br i1 %306, label %327, label %307

; <label>:307:                                    ; preds = %304
  br label %308

; <label>:308:                                    ; preds = %308, %307
  %309 = phi i32* [ %305, %307 ], [ %318, %308 ]
  %310 = getelementptr inbounds i32, i32* %309, i64 1
  %311 = load i32, i32* %309, align 4, !tbaa !7
  %312 = sext i32 %311 to i64
  %313 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %312, i32 2, i32 0
  %314 = load i32, i32* %313, align 4, !tbaa !24
  %315 = add nsw i32 %314, 1
  store i32 %315, i32* %313, align 4, !tbaa !24
  %316 = sext i32 %314 to i64
  %317 = getelementptr inbounds i32, i32* %3, i64 %316
  store i32 %284, i32* %317, align 4, !tbaa !7
  %318 = getelementptr inbounds i32, i32* %309, i64 2
  %319 = load i32, i32* %310, align 4, !tbaa !7
  %320 = sext i32 %319 to i64
  %321 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %320, i32 2, i32 0
  %322 = load i32, i32* %321, align 4, !tbaa !24
  %323 = add nsw i32 %322, 1
  store i32 %323, i32* %321, align 4, !tbaa !24
  %324 = sext i32 %322 to i64
  %325 = getelementptr inbounds i32, i32* %3, i64 %324
  store i32 %284, i32* %325, align 4, !tbaa !7
  %326 = icmp ult i32* %318, %279
  br i1 %326, label %308, label %327

; <label>:327:                                    ; preds = %304, %308, %271
  %328 = icmp eq i64 %275, %268
  br i1 %328, label %329, label %271

; <label>:329:                                    ; preds = %327, %264, %266, %230
  br i1 %132, label %330, label %359

; <label>:330:                                    ; preds = %329
  %331 = zext i32 %0 to i64
  %332 = and i64 %331, 1
  %333 = icmp eq i32 %0, 1
  br i1 %333, label %351, label %334

; <label>:334:                                    ; preds = %330
  %335 = sub nsw i64 %331, %332
  br label %336

; <label>:336:                                    ; preds = %336, %334
  %337 = phi i64 [ 0, %334 ], [ %348, %336 ]
  %338 = phi i64 [ %335, %334 ], [ %349, %336 ]
  %339 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %337, i32 3, i32 0
  store i32 0, i32* %339, align 4, !tbaa !24
  %340 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %337, i32 1
  %341 = load i32, i32* %340, align 4, !tbaa !25
  %342 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %337, i32 2, i32 0
  store i32 %341, i32* %342, align 4, !tbaa !24
  %343 = or i64 %337, 1
  %344 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %343, i32 3, i32 0
  store i32 0, i32* %344, align 4, !tbaa !24
  %345 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %343, i32 1
  %346 = load i32, i32* %345, align 4, !tbaa !25
  %347 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %343, i32 2, i32 0
  store i32 %346, i32* %347, align 4, !tbaa !24
  %348 = add nuw nsw i64 %337, 2
  %349 = add i64 %338, -2
  %350 = icmp eq i64 %349, 0
  br i1 %350, label %351, label %336

; <label>:351:                                    ; preds = %336, %330
  %352 = phi i64 [ 0, %330 ], [ %348, %336 ]
  %353 = icmp eq i64 %332, 0
  br i1 %353, label %359, label %354

; <label>:354:                                    ; preds = %351
  %355 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %352, i32 3, i32 0
  store i32 0, i32* %355, align 4, !tbaa !24
  %356 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %352, i32 1
  %357 = load i32, i32* %356, align 4, !tbaa !25
  %358 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %352, i32 2, i32 0
  store i32 %357, i32* %358, align 4, !tbaa !24
  br label %359

; <label>:359:                                    ; preds = %354, %351, %329
  %360 = load i32, i32* %13, align 4, !tbaa !7
  %361 = icmp eq i32 %360, 1
  br i1 %361, label %362, label %470

; <label>:362:                                    ; preds = %359
  store i32 0, i32* %93, align 4, !tbaa !21
  store i32 0, i32* %4, align 4, !tbaa !7
  %363 = icmp sgt i32 %1, 1
  br i1 %363, label %364, label %407

; <label>:364:                                    ; preds = %362
  %365 = and i32 %1, 1
  %366 = xor i32 %365, 1
  %367 = icmp eq i32 %1, 2
  br i1 %367, label %395, label %368

; <label>:368:                                    ; preds = %364
  %369 = zext i32 %366 to i64
  %370 = zext i32 %1 to i64
  %371 = add nsw i64 %370, -1
  %372 = sub nsw i64 %371, %369
  br label %373

; <label>:373:                                    ; preds = %373, %368
  %374 = phi i64 [ 1, %368 ], [ %392, %373 ]
  %375 = phi i64 [ %372, %368 ], [ %393, %373 ]
  %376 = add nsw i64 %374, -1
  %377 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %376, i32 0
  %378 = load i32, i32* %377, align 4, !tbaa !21
  %379 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %376, i32 1
  %380 = load i32, i32* %379, align 4, !tbaa !23
  %381 = add nsw i32 %380, %378
  %382 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %374, i32 0
  store i32 %381, i32* %382, align 4, !tbaa !21
  %383 = getelementptr inbounds i32, i32* %4, i64 %374
  store i32 %381, i32* %383, align 4, !tbaa !7
  %384 = add nuw nsw i64 %374, 1
  %385 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %374, i32 0
  %386 = load i32, i32* %385, align 4, !tbaa !21
  %387 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %374, i32 1
  %388 = load i32, i32* %387, align 4, !tbaa !23
  %389 = add nsw i32 %388, %386
  %390 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %384, i32 0
  store i32 %389, i32* %390, align 4, !tbaa !21
  %391 = getelementptr inbounds i32, i32* %4, i64 %384
  store i32 %389, i32* %391, align 4, !tbaa !7
  %392 = add nuw nsw i64 %374, 2
  %393 = add i64 %375, -2
  %394 = icmp eq i64 %393, 0
  br i1 %394, label %395, label %373

; <label>:395:                                    ; preds = %373, %364
  %396 = phi i64 [ 1, %364 ], [ %392, %373 ]
  %397 = icmp eq i32 %366, 0
  br i1 %397, label %407, label %398

; <label>:398:                                    ; preds = %395
  %399 = add nsw i64 %396, -1
  %400 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %399, i32 0
  %401 = load i32, i32* %400, align 4, !tbaa !21
  %402 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %399, i32 1
  %403 = load i32, i32* %402, align 4, !tbaa !23
  %404 = add nsw i32 %403, %401
  %405 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %396, i32 0
  store i32 %404, i32* %405, align 4, !tbaa !21
  %406 = getelementptr inbounds i32, i32* %4, i64 %396
  store i32 %404, i32* %406, align 4, !tbaa !7
  br label %407

; <label>:407:                                    ; preds = %398, %395, %362
  br i1 %132, label %408, label %470

; <label>:408:                                    ; preds = %407
  %409 = zext i32 %0 to i64
  %410 = getelementptr i32, i32* %3, i64 1
  %411 = xor i64 %8, -1
  br label %412

; <label>:412:                                    ; preds = %467, %408
  %413 = phi i64 [ 0, %408 ], [ %468, %467 ]
  %414 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %413, i32 0
  %415 = load i32, i32* %414, align 4, !tbaa !27
  %416 = sext i32 %415 to i64
  %417 = getelementptr inbounds i32, i32* %3, i64 %416
  %418 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %413, i32 1
  %419 = load i32, i32* %418, align 4, !tbaa !25
  %420 = sext i32 %419 to i64
  %421 = getelementptr inbounds i32, i32* %417, i64 %420
  %422 = icmp sgt i32 %419, 0
  br i1 %422, label %423, label %467

; <label>:423:                                    ; preds = %412
  %424 = trunc i64 %413 to i32
  %425 = getelementptr i32, i32* %410, i64 %416
  %426 = icmp ugt i32* %421, %425
  %427 = select i1 %426, i32* %421, i32* %425
  %428 = bitcast i32* %427 to i8*
  %429 = getelementptr i8, i8* %428, i64 %411
  %430 = mul nsw i64 %416, -4
  %431 = getelementptr i8, i8* %429, i64 %430
  %432 = ptrtoint i8* %431 to i64
  %433 = and i64 %432, 4
  %434 = icmp eq i64 %433, 0
  br i1 %434, label %435, label %444

; <label>:435:                                    ; preds = %423
  %436 = getelementptr inbounds i32, i32* %417, i64 1
  %437 = load i32, i32* %417, align 4, !tbaa !7
  %438 = sext i32 %437 to i64
  %439 = getelementptr inbounds i32, i32* %4, i64 %438
  %440 = load i32, i32* %439, align 4, !tbaa !7
  %441 = add nsw i32 %440, 1
  store i32 %441, i32* %439, align 4, !tbaa !7
  %442 = sext i32 %440 to i64
  %443 = getelementptr inbounds i32, i32* %3, i64 %442
  store i32 %424, i32* %443, align 4, !tbaa !7
  br label %444

; <label>:444:                                    ; preds = %435, %423
  %445 = phi i32* [ %436, %435 ], [ %417, %423 ]
  %446 = icmp ult i8* %431, inttoptr (i64 4 to i8*)
  br i1 %446, label %467, label %447

; <label>:447:                                    ; preds = %444
  br label %448

; <label>:448:                                    ; preds = %448, %447
  %449 = phi i32* [ %445, %447 ], [ %458, %448 ]
  %450 = getelementptr inbounds i32, i32* %449, i64 1
  %451 = load i32, i32* %449, align 4, !tbaa !7
  %452 = sext i32 %451 to i64
  %453 = getelementptr inbounds i32, i32* %4, i64 %452
  %454 = load i32, i32* %453, align 4, !tbaa !7
  %455 = add nsw i32 %454, 1
  store i32 %455, i32* %453, align 4, !tbaa !7
  %456 = sext i32 %454 to i64
  %457 = getelementptr inbounds i32, i32* %3, i64 %456
  store i32 %424, i32* %457, align 4, !tbaa !7
  %458 = getelementptr inbounds i32, i32* %449, i64 2
  %459 = load i32, i32* %450, align 4, !tbaa !7
  %460 = sext i32 %459 to i64
  %461 = getelementptr inbounds i32, i32* %4, i64 %460
  %462 = load i32, i32* %461, align 4, !tbaa !7
  %463 = add nsw i32 %462, 1
  store i32 %463, i32* %461, align 4, !tbaa !7
  %464 = sext i32 %462 to i64
  %465 = getelementptr inbounds i32, i32* %3, i64 %464
  store i32 %424, i32* %465, align 4, !tbaa !7
  %466 = icmp ult i32* %458, %421
  br i1 %466, label %448, label %467

; <label>:467:                                    ; preds = %444, %448, %412
  %468 = add nuw nsw i64 %413, 1
  %469 = icmp eq i64 %468, %409
  br i1 %469, label %470, label %412

; <label>:470:                                    ; preds = %467, %359, %407
  %471 = bitcast i32* %4 to i8*
  %472 = load double, double* %48, align 8, !tbaa !3
  %473 = fcmp olt double %472, 0.000000e+00
  br i1 %473, label %474, label %476

; <label>:474:                                    ; preds = %470
  %475 = add nsw i32 %1, -1
  br label %483

; <label>:476:                                    ; preds = %470
  %477 = sitofp i32 %1 to double
  %478 = tail call double @llvm.sqrt.f64(double %477) #5
  %479 = fmul double %478, %472
  %480 = fcmp ole double %479, 1.600000e+01
  %481 = select i1 %480, double 1.600000e+01, double %479
  %482 = fptosi double %481 to i32
  br label %483

; <label>:483:                                    ; preds = %476, %474
  %484 = phi i32 [ %475, %474 ], [ %482, %476 ]
  %485 = getelementptr inbounds double, double* %48, i64 1
  %486 = load double, double* %485, align 8, !tbaa !3
  %487 = fcmp olt double %486, 0.000000e+00
  br i1 %487, label %488, label %490

; <label>:488:                                    ; preds = %483
  %489 = add nsw i32 %0, -1
  br label %499

; <label>:490:                                    ; preds = %483
  %491 = icmp slt i32 %0, %1
  %492 = select i1 %491, i32 %0, i32 %1
  %493 = sitofp i32 %492 to double
  %494 = tail call double @llvm.sqrt.f64(double %493) #5
  %495 = fmul double %494, %486
  %496 = fcmp ole double %495, 1.600000e+01
  %497 = select i1 %496, double 1.600000e+01, double %495
  %498 = fptosi double %497 to i32
  br label %499

; <label>:499:                                    ; preds = %490, %488
  %500 = phi i32 [ %489, %488 ], [ %498, %490 ]
  br i1 %98, label %501, label %598

; <label>:501:                                    ; preds = %499
  br label %502

; <label>:502:                                    ; preds = %501, %513
  %503 = phi i64 [ %505, %513 ], [ %29, %501 ]
  %504 = phi i32 [ %514, %513 ], [ %1, %501 ]
  %505 = add nsw i64 %503, -1
  %506 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %505, i32 1
  %507 = load i32, i32* %506, align 4, !tbaa !23
  %508 = icmp eq i32 %507, 0
  br i1 %508, label %509, label %513

; <label>:509:                                    ; preds = %502
  %510 = add nsw i32 %504, -1
  %511 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %505, i32 3, i32 0
  store i32 %510, i32* %511, align 4, !tbaa !24
  %512 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %505, i32 0
  store i32 -1, i32* %512, align 4, !tbaa !21
  br label %513

; <label>:513:                                    ; preds = %509, %502
  %514 = phi i32 [ %510, %509 ], [ %504, %502 ]
  %515 = icmp sgt i64 %503, 1
  br i1 %515, label %502, label %516

; <label>:516:                                    ; preds = %513
  %517 = getelementptr i32, i32* %3, i64 1
  %518 = xor i64 %8, -1
  br label %519

; <label>:519:                                    ; preds = %516, %595
  %520 = phi i64 [ %522, %595 ], [ %29, %516 ]
  %521 = phi i32 [ %596, %595 ], [ %514, %516 ]
  %522 = add nsw i64 %520, -1
  %523 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %522, i32 0
  %524 = load i32, i32* %523, align 4, !tbaa !21
  %525 = icmp slt i32 %524, 0
  br i1 %525, label %595, label %526

; <label>:526:                                    ; preds = %519
  %527 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %522, i32 1
  %528 = load i32, i32* %527, align 4, !tbaa !23
  %529 = icmp sgt i32 %528, %500
  br i1 %529, label %530, label %595

; <label>:530:                                    ; preds = %526
  %531 = add nsw i32 %521, -1
  %532 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %522, i32 3, i32 0
  store i32 %531, i32* %532, align 4, !tbaa !24
  %533 = sext i32 %524 to i64
  %534 = getelementptr inbounds i32, i32* %3, i64 %533
  %535 = sext i32 %528 to i64
  %536 = getelementptr inbounds i32, i32* %534, i64 %535
  %537 = icmp sgt i32 %528, 0
  br i1 %537, label %538, label %594

; <label>:538:                                    ; preds = %530
  %539 = getelementptr i32, i32* %517, i64 %533
  %540 = icmp ugt i32* %536, %539
  %541 = select i1 %540, i32* %536, i32* %539
  %542 = bitcast i32* %541 to i8*
  %543 = getelementptr i8, i8* %542, i64 %518
  %544 = mul nsw i64 %533, -4
  %545 = getelementptr i8, i8* %543, i64 %544
  %546 = ptrtoint i8* %545 to i64
  %547 = lshr i64 %546, 2
  %548 = add nuw nsw i64 %547, 1
  %549 = and i64 %548, 3
  %550 = icmp eq i64 %549, 0
  br i1 %550, label %563, label %551

; <label>:551:                                    ; preds = %538
  br label %552

; <label>:552:                                    ; preds = %552, %551
  %553 = phi i32* [ %555, %552 ], [ %534, %551 ]
  %554 = phi i64 [ %561, %552 ], [ %549, %551 ]
  %555 = getelementptr inbounds i32, i32* %553, i64 1
  %556 = load i32, i32* %553, align 4, !tbaa !7
  %557 = sext i32 %556 to i64
  %558 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %557, i32 2, i32 0
  %559 = load i32, i32* %558, align 4, !tbaa !24
  %560 = add nsw i32 %559, -1
  store i32 %560, i32* %558, align 4, !tbaa !24
  %561 = add i64 %554, -1
  %562 = icmp eq i64 %561, 0
  br i1 %562, label %563, label %552, !llvm.loop !28

; <label>:563:                                    ; preds = %552, %538
  %564 = phi i32* [ %534, %538 ], [ %555, %552 ]
  %565 = icmp ult i8* %545, inttoptr (i64 12 to i8*)
  br i1 %565, label %594, label %566

; <label>:566:                                    ; preds = %563
  br label %567

; <label>:567:                                    ; preds = %567, %566
  %568 = phi i32* [ %564, %566 ], [ %587, %567 ]
  %569 = getelementptr inbounds i32, i32* %568, i64 1
  %570 = load i32, i32* %568, align 4, !tbaa !7
  %571 = sext i32 %570 to i64
  %572 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %571, i32 2, i32 0
  %573 = load i32, i32* %572, align 4, !tbaa !24
  %574 = add nsw i32 %573, -1
  store i32 %574, i32* %572, align 4, !tbaa !24
  %575 = getelementptr inbounds i32, i32* %568, i64 2
  %576 = load i32, i32* %569, align 4, !tbaa !7
  %577 = sext i32 %576 to i64
  %578 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %577, i32 2, i32 0
  %579 = load i32, i32* %578, align 4, !tbaa !24
  %580 = add nsw i32 %579, -1
  store i32 %580, i32* %578, align 4, !tbaa !24
  %581 = getelementptr inbounds i32, i32* %568, i64 3
  %582 = load i32, i32* %575, align 4, !tbaa !7
  %583 = sext i32 %582 to i64
  %584 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %583, i32 2, i32 0
  %585 = load i32, i32* %584, align 4, !tbaa !24
  %586 = add nsw i32 %585, -1
  store i32 %586, i32* %584, align 4, !tbaa !24
  %587 = getelementptr inbounds i32, i32* %568, i64 4
  %588 = load i32, i32* %581, align 4, !tbaa !7
  %589 = sext i32 %588 to i64
  %590 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %589, i32 2, i32 0
  %591 = load i32, i32* %590, align 4, !tbaa !24
  %592 = add nsw i32 %591, -1
  store i32 %592, i32* %590, align 4, !tbaa !24
  %593 = icmp ult i32* %587, %536
  br i1 %593, label %567, label %594

; <label>:594:                                    ; preds = %563, %567, %530
  store i32 -1, i32* %523, align 4, !tbaa !21
  br label %595

; <label>:595:                                    ; preds = %594, %526, %519
  %596 = phi i32 [ %521, %519 ], [ %531, %594 ], [ %521, %526 ]
  %597 = icmp sgt i64 %520, 1
  br i1 %597, label %519, label %598

; <label>:598:                                    ; preds = %595, %499
  %599 = phi i32 [ %1, %499 ], [ %596, %595 ]
  br i1 %132, label %600, label %622

; <label>:600:                                    ; preds = %598
  %601 = zext i32 %0 to i64
  br label %602

; <label>:602:                                    ; preds = %617, %600
  %603 = phi i64 [ 0, %600 ], [ %620, %617 ]
  %604 = phi i32 [ 0, %600 ], [ %619, %617 ]
  %605 = phi i32 [ %0, %600 ], [ %618, %617 ]
  %606 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %603, i32 2, i32 0
  %607 = load i32, i32* %606, align 4, !tbaa !24
  %608 = icmp sgt i32 %607, %484
  %609 = icmp eq i32 %607, 0
  %610 = or i1 %608, %609
  br i1 %610, label %611, label %614

; <label>:611:                                    ; preds = %602
  %612 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %603, i32 3, i32 0
  store i32 -1, i32* %612, align 4, !tbaa !24
  %613 = add nsw i32 %605, -1
  br label %617

; <label>:614:                                    ; preds = %602
  %615 = icmp sgt i32 %604, %607
  %616 = select i1 %615, i32 %604, i32 %607
  br label %617

; <label>:617:                                    ; preds = %614, %611
  %618 = phi i32 [ %613, %611 ], [ %605, %614 ]
  %619 = phi i32 [ %604, %611 ], [ %616, %614 ]
  %620 = add nuw nsw i64 %603, 1
  %621 = icmp eq i64 %620, %601
  br i1 %621, label %622, label %602

; <label>:622:                                    ; preds = %617, %598
  %623 = phi i32 [ %0, %598 ], [ %618, %617 ]
  %624 = phi i32 [ 0, %598 ], [ %619, %617 ]
  br i1 %98, label %625, label %626

; <label>:625:                                    ; preds = %622
  br label %630

; <label>:626:                                    ; preds = %622
  %627 = zext i32 %1 to i64
  %628 = shl nuw nsw i64 %627, 2
  %629 = add nuw nsw i64 %628, 4
  tail call void @llvm.memset.p0i8.i64(i8* %471, i8 -1, i64 %629, i32 4, i1 false) #5
  br label %719

; <label>:630:                                    ; preds = %625, %687
  %631 = phi i64 [ %633, %687 ], [ %29, %625 ]
  %632 = phi i32 [ %688, %687 ], [ %599, %625 ]
  %633 = add nsw i64 %631, -1
  %634 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %633, i32 0
  %635 = load i32, i32* %634, align 4, !tbaa !21
  %636 = icmp slt i32 %635, 0
  br i1 %636, label %687, label %637

; <label>:637:                                    ; preds = %630
  %638 = sext i32 %635 to i64
  %639 = getelementptr inbounds i32, i32* %3, i64 %638
  %640 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %633, i32 1
  %641 = load i32, i32* %640, align 4, !tbaa !23
  %642 = sext i32 %641 to i64
  %643 = getelementptr inbounds i32, i32* %639, i64 %642
  %644 = icmp sgt i32 %641, 0
  br i1 %644, label %645, label %670

; <label>:645:                                    ; preds = %637
  br label %646

; <label>:646:                                    ; preds = %645, %660
  %647 = phi i32 [ %668, %660 ], [ 0, %645 ]
  %648 = phi i32* [ %662, %660 ], [ %639, %645 ]
  %649 = phi i32* [ %654, %660 ], [ %639, %645 ]
  br label %652

; <label>:650:                                    ; preds = %652
  %651 = icmp ult i32* %654, %643
  br i1 %651, label %652, label %670

; <label>:652:                                    ; preds = %650, %646
  %653 = phi i32* [ %649, %646 ], [ %654, %650 ]
  %654 = getelementptr inbounds i32, i32* %653, i64 1
  %655 = load i32, i32* %653, align 4, !tbaa !7
  %656 = sext i32 %655 to i64
  %657 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %656, i32 3, i32 0
  %658 = load i32, i32* %657, align 4, !tbaa !24
  %659 = icmp slt i32 %658, 0
  br i1 %659, label %650, label %660

; <label>:660:                                    ; preds = %652
  %661 = sext i32 %655 to i64
  %662 = getelementptr inbounds i32, i32* %648, i64 1
  store i32 %655, i32* %648, align 4, !tbaa !7
  %663 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %661, i32 2, i32 0
  %664 = load i32, i32* %663, align 4, !tbaa !24
  %665 = add i32 %647, -1
  %666 = add i32 %665, %664
  %667 = icmp slt i32 %666, %1
  %668 = select i1 %667, i32 %666, i32 %1
  %669 = icmp ult i32* %654, %643
  br i1 %669, label %646, label %670

; <label>:670:                                    ; preds = %660, %650, %637
  %671 = phi i32* [ %639, %637 ], [ %648, %650 ], [ %662, %660 ]
  %672 = phi i32 [ 0, %637 ], [ %647, %650 ], [ %668, %660 ]
  %673 = load i32, i32* %634, align 4, !tbaa !21
  %674 = sext i32 %673 to i64
  %675 = getelementptr inbounds i32, i32* %3, i64 %674
  %676 = ptrtoint i32* %671 to i64
  %677 = ptrtoint i32* %675 to i64
  %678 = sub i64 %676, %677
  %679 = lshr exact i64 %678, 2
  %680 = trunc i64 %679 to i32
  %681 = icmp eq i32 %680, 0
  br i1 %681, label %682, label %685

; <label>:682:                                    ; preds = %670
  %683 = add nsw i32 %632, -1
  %684 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %633, i32 3, i32 0
  store i32 %683, i32* %684, align 4, !tbaa !24
  store i32 -1, i32* %634, align 4, !tbaa !21
  br label %687

; <label>:685:                                    ; preds = %670
  store i32 %680, i32* %640, align 4, !tbaa !23
  %686 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %633, i32 3, i32 0
  store i32 %672, i32* %686, align 4, !tbaa !24
  br label %687

; <label>:687:                                    ; preds = %685, %682, %630
  %688 = phi i32 [ %632, %630 ], [ %683, %682 ], [ %632, %685 ]
  %689 = icmp sgt i64 %631, 1
  br i1 %689, label %630, label %690

; <label>:690:                                    ; preds = %687
  %691 = zext i32 %1 to i64
  %692 = shl nuw nsw i64 %691, 2
  %693 = add nuw nsw i64 %692, 4
  tail call void @llvm.memset.p0i8.i64(i8* %471, i8 -1, i64 %693, i32 4, i1 false) #5
  br label %694

; <label>:694:                                    ; preds = %717, %690
  %695 = phi i64 [ %29, %690 ], [ %696, %717 ]
  %696 = add nsw i64 %695, -1
  %697 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %696, i32 0
  %698 = load i32, i32* %697, align 4, !tbaa !21
  %699 = icmp sgt i32 %698, -1
  br i1 %699, label %700, label %717

; <label>:700:                                    ; preds = %694
  %701 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %696, i32 3, i32 0
  %702 = load i32, i32* %701, align 4, !tbaa !24
  %703 = sext i32 %702 to i64
  %704 = getelementptr inbounds i32, i32* %4, i64 %703
  %705 = load i32, i32* %704, align 4, !tbaa !7
  %706 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %696, i32 4, i32 0
  store i32 -1, i32* %706, align 4, !tbaa !24
  %707 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %696, i32 5, i32 0
  store i32 %705, i32* %707, align 4, !tbaa !24
  %708 = icmp eq i32 %705, -1
  br i1 %708, label %709, label %711

; <label>:709:                                    ; preds = %700
  %710 = trunc i64 %696 to i32
  br label %715

; <label>:711:                                    ; preds = %700
  %712 = sext i32 %705 to i64
  %713 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %712, i32 4, i32 0
  %714 = trunc i64 %696 to i32
  store i32 %714, i32* %713, align 4, !tbaa !24
  br label %715

; <label>:715:                                    ; preds = %711, %709
  %716 = phi i32 [ %710, %709 ], [ %714, %711 ]
  store i32 %716, i32* %704, align 4, !tbaa !7
  br label %717

; <label>:717:                                    ; preds = %715, %694
  %718 = icmp sgt i64 %695, 1
  br i1 %718, label %694, label %719

; <label>:719:                                    ; preds = %717, %626
  %720 = phi i32 [ %599, %626 ], [ %688, %717 ]
  %721 = shl nsw i32 %31, 1
  %722 = sub nsw i32 2147483647, %1
  br i1 %132, label %723, label %749

; <label>:723:                                    ; preds = %719
  %724 = zext i32 %0 to i64
  %725 = and i64 %724, 1
  %726 = icmp eq i32 %0, 1
  br i1 %726, label %741, label %727

; <label>:727:                                    ; preds = %723
  %728 = sub nsw i64 %724, %725
  br label %729

; <label>:729:                                    ; preds = %1956, %727
  %730 = phi i64 [ 0, %727 ], [ %1957, %1956 ]
  %731 = phi i64 [ %728, %727 ], [ %1958, %1956 ]
  %732 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %730, i32 3, i32 0
  %733 = load i32, i32* %732, align 4, !tbaa !24
  %734 = icmp sgt i32 %733, -1
  br i1 %734, label %735, label %736

; <label>:735:                                    ; preds = %729
  store i32 0, i32* %732, align 4, !tbaa !24
  br label %736

; <label>:736:                                    ; preds = %735, %729
  %737 = or i64 %730, 1
  %738 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %737, i32 3, i32 0
  %739 = load i32, i32* %738, align 4, !tbaa !24
  %740 = icmp sgt i32 %739, -1
  br i1 %740, label %1955, label %1956

; <label>:741:                                    ; preds = %1956, %723
  %742 = phi i64 [ 0, %723 ], [ %1957, %1956 ]
  %743 = icmp eq i64 %725, 0
  br i1 %743, label %749, label %744

; <label>:744:                                    ; preds = %741
  %745 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %742, i32 3, i32 0
  %746 = load i32, i32* %745, align 4, !tbaa !24
  %747 = icmp sgt i32 %746, -1
  br i1 %747, label %748, label %749

; <label>:748:                                    ; preds = %744
  store i32 0, i32* %745, align 4, !tbaa !24
  br label %749

; <label>:749:                                    ; preds = %741, %744, %748, %719
  %750 = icmp sgt i32 %720, 0
  br i1 %750, label %751, label %1600

; <label>:751:                                    ; preds = %749
  %752 = zext i32 %1 to i64
  %753 = zext i32 %0 to i64
  %754 = add nsw i32 %1, 1
  %755 = xor i1 %132, true
  %756 = add nsw i64 %753, -1
  %757 = getelementptr i32, i32* %3, i64 1
  %758 = xor i64 %8, -1
  %759 = and i64 %753, 1
  %760 = icmp eq i64 %756, 0
  %761 = sub nsw i64 %753, %759
  %762 = icmp eq i64 %759, 0
  %763 = and i64 %753, 1
  %764 = icmp eq i64 %756, 0
  %765 = sub nsw i64 %753, %763
  %766 = icmp eq i64 %763, 0
  br label %767

; <label>:767:                                    ; preds = %1598, %751
  %768 = phi i32 [ 0, %751 ], [ %1018, %1598 ]
  %769 = phi i32 [ %624, %751 ], [ %1139, %1598 ]
  %770 = phi i32 [ %721, %751 ], [ %1140, %1598 ]
  %771 = phi i32 [ 0, %751 ], [ %1500, %1598 ]
  %772 = phi i32 [ 0, %751 ], [ %1585, %1598 ]
  %773 = phi i32 [ 1, %751 ], [ %1534, %1598 ]
  %774 = sext i32 %772 to i64
  br label %775

; <label>:775:                                    ; preds = %775, %767
  %776 = phi i64 [ %782, %775 ], [ %774, %767 ]
  %777 = getelementptr inbounds i32, i32* %4, i64 %776
  %778 = load i32, i32* %777, align 4, !tbaa !7
  %779 = icmp eq i32 %778, -1
  %780 = icmp slt i64 %776, %29
  %781 = and i1 %780, %779
  %782 = add nsw i64 %776, 1
  br i1 %781, label %775, label %783

; <label>:783:                                    ; preds = %775
  %784 = getelementptr inbounds i32, i32* %4, i64 %776
  %785 = trunc i64 %776 to i32
  %786 = sext i32 %778 to i64
  %787 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %786, i32 5, i32 0
  %788 = load i32, i32* %787, align 4, !tbaa !24
  store i32 %788, i32* %784, align 4, !tbaa !7
  %789 = icmp eq i32 %788, -1
  br i1 %789, label %793, label %790

; <label>:790:                                    ; preds = %783
  %791 = sext i32 %788 to i64
  %792 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %791, i32 4, i32 0
  store i32 -1, i32* %792, align 4, !tbaa !24
  br label %793

; <label>:793:                                    ; preds = %790, %783
  %794 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %786, i32 3, i32 0
  %795 = load i32, i32* %794, align 4, !tbaa !24
  store i32 %771, i32* %794, align 4, !tbaa !24
  %796 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %786, i32 2, i32 0
  %797 = load i32, i32* %796, align 4, !tbaa !24
  %798 = add nsw i32 %797, %771
  %799 = sub nsw i32 %1, %798
  %800 = icmp slt i32 %795, %799
  %801 = select i1 %800, i32 %795, i32 %799
  %802 = add nsw i32 %801, %770
  %803 = icmp slt i32 %802, %91
  br i1 %803, label %1015, label %804

; <label>:804:                                    ; preds = %793
  %805 = sext i32 %770 to i64
  %806 = getelementptr inbounds i32, i32* %3, i64 %805
  br i1 %98, label %807, label %880

; <label>:807:                                    ; preds = %804
  br label %808

; <label>:808:                                    ; preds = %807, %876
  %809 = phi i64 [ %878, %876 ], [ 0, %807 ]
  %810 = phi i32* [ %877, %876 ], [ %3, %807 ]
  %811 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %809, i32 0
  %812 = load i32, i32* %811, align 4, !tbaa !21
  %813 = icmp sgt i32 %812, -1
  br i1 %813, label %814, label %876

; <label>:814:                                    ; preds = %808
  %815 = ptrtoint i32* %810 to i64
  %816 = sub i64 %815, %8
  %817 = lshr exact i64 %816, 2
  %818 = trunc i64 %817 to i32
  store i32 %818, i32* %811, align 4, !tbaa !21
  %819 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %809, i32 1
  %820 = load i32, i32* %819, align 4, !tbaa !23
  %821 = icmp sgt i32 %820, 0
  br i1 %821, label %822, label %866

; <label>:822:                                    ; preds = %814
  %823 = sext i32 %812 to i64
  %824 = getelementptr inbounds i32, i32* %3, i64 %823
  %825 = and i32 %820, 1
  %826 = icmp eq i32 %820, 1
  br i1 %826, label %849, label %827

; <label>:827:                                    ; preds = %822
  %828 = sub i32 %820, %825
  br label %829

; <label>:829:                                    ; preds = %1935, %827
  %830 = phi i32* [ %810, %827 ], [ %1936, %1935 ]
  %831 = phi i32* [ %824, %827 ], [ %843, %1935 ]
  %832 = phi i32 [ %828, %827 ], [ %1937, %1935 ]
  %833 = getelementptr inbounds i32, i32* %831, i64 1
  %834 = load i32, i32* %831, align 4, !tbaa !7
  %835 = sext i32 %834 to i64
  %836 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %835, i32 3, i32 0
  %837 = load i32, i32* %836, align 4, !tbaa !24
  %838 = icmp sgt i32 %837, -1
  br i1 %838, label %839, label %841

; <label>:839:                                    ; preds = %829
  %840 = getelementptr inbounds i32, i32* %830, i64 1
  store i32 %834, i32* %830, align 4, !tbaa !7
  br label %841

; <label>:841:                                    ; preds = %839, %829
  %842 = phi i32* [ %840, %839 ], [ %830, %829 ]
  %843 = getelementptr inbounds i32, i32* %831, i64 2
  %844 = load i32, i32* %833, align 4, !tbaa !7
  %845 = sext i32 %844 to i64
  %846 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %845, i32 3, i32 0
  %847 = load i32, i32* %846, align 4, !tbaa !24
  %848 = icmp sgt i32 %847, -1
  br i1 %848, label %1933, label %1935

; <label>:849:                                    ; preds = %1935, %822
  %850 = phi i32* [ undef, %822 ], [ %1936, %1935 ]
  %851 = phi i32* [ %810, %822 ], [ %1936, %1935 ]
  %852 = phi i32* [ %824, %822 ], [ %843, %1935 ]
  %853 = icmp eq i32 %825, 0
  br i1 %853, label %862, label %854

; <label>:854:                                    ; preds = %849
  %855 = load i32, i32* %852, align 4, !tbaa !7
  %856 = sext i32 %855 to i64
  %857 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %856, i32 3, i32 0
  %858 = load i32, i32* %857, align 4, !tbaa !24
  %859 = icmp sgt i32 %858, -1
  br i1 %859, label %860, label %862

; <label>:860:                                    ; preds = %854
  %861 = getelementptr inbounds i32, i32* %851, i64 1
  store i32 %855, i32* %851, align 4, !tbaa !7
  br label %862

; <label>:862:                                    ; preds = %860, %854, %849
  %863 = phi i32* [ %850, %849 ], [ %861, %860 ], [ %851, %854 ]
  %864 = load i32, i32* %811, align 4, !tbaa !21
  %865 = ptrtoint i32* %863 to i64
  br label %866

; <label>:866:                                    ; preds = %862, %814
  %867 = phi i64 [ %865, %862 ], [ %815, %814 ]
  %868 = phi i32 [ %864, %862 ], [ %818, %814 ]
  %869 = phi i32* [ %863, %862 ], [ %810, %814 ]
  %870 = sext i32 %868 to i64
  %871 = getelementptr inbounds i32, i32* %3, i64 %870
  %872 = ptrtoint i32* %871 to i64
  %873 = sub i64 %867, %872
  %874 = lshr exact i64 %873, 2
  %875 = trunc i64 %874 to i32
  store i32 %875, i32* %819, align 4, !tbaa !23
  br label %876

; <label>:876:                                    ; preds = %866, %808
  %877 = phi i32* [ %869, %866 ], [ %810, %808 ]
  %878 = add nuw nsw i64 %809, 1
  %879 = icmp eq i64 %878, %752
  br i1 %879, label %880, label %808

; <label>:880:                                    ; preds = %876, %804
  %881 = phi i32* [ %3, %804 ], [ %877, %876 ]
  br i1 %132, label %882, label %905

; <label>:882:                                    ; preds = %880
  br label %883

; <label>:883:                                    ; preds = %882, %901
  %884 = phi i64 [ %902, %901 ], [ 0, %882 ]
  %885 = phi i32 [ %903, %901 ], [ 0, %882 ]
  %886 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %884, i32 3, i32 0
  %887 = load i32, i32* %886, align 4, !tbaa !24
  %888 = icmp slt i32 %887, 0
  br i1 %888, label %893, label %889

; <label>:889:                                    ; preds = %883
  %890 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %884, i32 1
  %891 = load i32, i32* %890, align 4, !tbaa !25
  %892 = icmp eq i32 %891, 0
  br i1 %892, label %893, label %894

; <label>:893:                                    ; preds = %889, %883
  store i32 -1, i32* %886, align 4, !tbaa !24
  br label %901

; <label>:894:                                    ; preds = %889
  %895 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %884, i32 0
  %896 = load i32, i32* %895, align 4, !tbaa !27
  %897 = sext i32 %896 to i64
  %898 = getelementptr inbounds i32, i32* %3, i64 %897
  %899 = load i32, i32* %898, align 4, !tbaa !7
  store i32 %899, i32* %886, align 4, !tbaa !24
  %900 = xor i32 %885, -1
  store i32 %900, i32* %898, align 4, !tbaa !7
  br label %901

; <label>:901:                                    ; preds = %894, %893
  %902 = add nuw nsw i64 %884, 1
  %903 = add nuw nsw i32 %885, 1
  %904 = icmp eq i64 %902, %753
  br i1 %904, label %905, label %883

; <label>:905:                                    ; preds = %901, %880
  %906 = icmp ult i32* %881, %806
  br i1 %906, label %907, label %987

; <label>:907:                                    ; preds = %905
  br label %908

; <label>:908:                                    ; preds = %907, %983
  %909 = phi i32* [ %985, %983 ], [ %881, %907 ]
  %910 = phi i32* [ %984, %983 ], [ %881, %907 ]
  %911 = getelementptr i32, i32* %910, i64 1
  %912 = load i32, i32* %910, align 4, !tbaa !7
  %913 = icmp slt i32 %912, 0
  br i1 %913, label %914, label %983

; <label>:914:                                    ; preds = %908
  %915 = xor i32 %912, -1
  %916 = sext i32 %915 to i64
  %917 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %916, i32 3, i32 0
  %918 = load i32, i32* %917, align 4, !tbaa !24
  store i32 %918, i32* %910, align 4, !tbaa !7
  %919 = ptrtoint i32* %909 to i64
  %920 = sub i64 %919, %8
  %921 = lshr exact i64 %920, 2
  %922 = trunc i64 %921 to i32
  %923 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %916, i32 0
  store i32 %922, i32* %923, align 4, !tbaa !27
  %924 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %916, i32 1
  %925 = load i32, i32* %924, align 4, !tbaa !25
  %926 = icmp sgt i32 %925, 0
  br i1 %926, label %927, label %972

; <label>:927:                                    ; preds = %914
  %928 = and i32 %925, 1
  %929 = icmp eq i32 %925, 1
  br i1 %929, label %952, label %930

; <label>:930:                                    ; preds = %927
  %931 = sub i32 %925, %928
  br label %932

; <label>:932:                                    ; preds = %1941, %930
  %933 = phi i32* [ %909, %930 ], [ %1942, %1941 ]
  %934 = phi i32* [ %910, %930 ], [ %946, %1941 ]
  %935 = phi i32 [ %931, %930 ], [ %1943, %1941 ]
  %936 = getelementptr inbounds i32, i32* %934, i64 1
  %937 = load i32, i32* %934, align 4, !tbaa !7
  %938 = sext i32 %937 to i64
  %939 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %938, i32 0
  %940 = load i32, i32* %939, align 4, !tbaa !21
  %941 = icmp sgt i32 %940, -1
  br i1 %941, label %942, label %944

; <label>:942:                                    ; preds = %932
  %943 = getelementptr inbounds i32, i32* %933, i64 1
  store i32 %937, i32* %933, align 4, !tbaa !7
  br label %944

; <label>:944:                                    ; preds = %942, %932
  %945 = phi i32* [ %943, %942 ], [ %933, %932 ]
  %946 = getelementptr inbounds i32, i32* %934, i64 2
  %947 = load i32, i32* %936, align 4, !tbaa !7
  %948 = sext i32 %947 to i64
  %949 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %948, i32 0
  %950 = load i32, i32* %949, align 4, !tbaa !21
  %951 = icmp sgt i32 %950, -1
  br i1 %951, label %1939, label %1941

; <label>:952:                                    ; preds = %1941, %927
  %953 = phi i32* [ undef, %927 ], [ %1942, %1941 ]
  %954 = phi i32* [ %909, %927 ], [ %1942, %1941 ]
  %955 = phi i32* [ %910, %927 ], [ %946, %1941 ]
  %956 = icmp eq i32 %928, 0
  br i1 %956, label %965, label %957

; <label>:957:                                    ; preds = %952
  %958 = load i32, i32* %955, align 4, !tbaa !7
  %959 = sext i32 %958 to i64
  %960 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %959, i32 0
  %961 = load i32, i32* %960, align 4, !tbaa !21
  %962 = icmp sgt i32 %961, -1
  br i1 %962, label %963, label %965

; <label>:963:                                    ; preds = %957
  %964 = getelementptr inbounds i32, i32* %954, i64 1
  store i32 %958, i32* %954, align 4, !tbaa !7
  br label %965

; <label>:965:                                    ; preds = %963, %957, %952
  %966 = phi i32* [ %953, %952 ], [ %964, %963 ], [ %954, %957 ]
  %967 = add i32 %925, -1
  %968 = zext i32 %967 to i64
  %969 = getelementptr i32, i32* %911, i64 %968
  %970 = load i32, i32* %923, align 4, !tbaa !27
  %971 = ptrtoint i32* %966 to i64
  br label %972

; <label>:972:                                    ; preds = %965, %914
  %973 = phi i64 [ %971, %965 ], [ %919, %914 ]
  %974 = phi i32 [ %970, %965 ], [ %922, %914 ]
  %975 = phi i32* [ %969, %965 ], [ %910, %914 ]
  %976 = phi i32* [ %966, %965 ], [ %909, %914 ]
  %977 = sext i32 %974 to i64
  %978 = getelementptr inbounds i32, i32* %3, i64 %977
  %979 = ptrtoint i32* %978 to i64
  %980 = sub i64 %973, %979
  %981 = lshr exact i64 %980, 2
  %982 = trunc i64 %981 to i32
  store i32 %982, i32* %924, align 4, !tbaa !25
  br label %983

; <label>:983:                                    ; preds = %972, %908
  %984 = phi i32* [ %975, %972 ], [ %911, %908 ]
  %985 = phi i32* [ %976, %972 ], [ %909, %908 ]
  %986 = icmp ult i32* %984, %806
  br i1 %986, label %908, label %987

; <label>:987:                                    ; preds = %983, %905
  %988 = phi i32* [ %881, %905 ], [ %985, %983 ]
  %989 = ptrtoint i32* %988 to i64
  %990 = sub i64 %989, %8
  %991 = lshr exact i64 %990, 2
  %992 = trunc i64 %991 to i32
  %993 = add nsw i32 %768, 1
  br i1 %132, label %994, label %1015

; <label>:994:                                    ; preds = %987
  br i1 %764, label %1008, label %995

; <label>:995:                                    ; preds = %994
  br label %996

; <label>:996:                                    ; preds = %1946, %995
  %997 = phi i64 [ 0, %995 ], [ %1947, %1946 ]
  %998 = phi i64 [ %765, %995 ], [ %1948, %1946 ]
  %999 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %997, i32 3, i32 0
  %1000 = load i32, i32* %999, align 4, !tbaa !24
  %1001 = icmp sgt i32 %1000, -1
  br i1 %1001, label %1002, label %1003

; <label>:1002:                                   ; preds = %996
  store i32 0, i32* %999, align 4, !tbaa !24
  br label %1003

; <label>:1003:                                   ; preds = %1002, %996
  %1004 = or i64 %997, 1
  %1005 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1004, i32 3, i32 0
  %1006 = load i32, i32* %1005, align 4, !tbaa !24
  %1007 = icmp sgt i32 %1006, -1
  br i1 %1007, label %1945, label %1946

; <label>:1008:                                   ; preds = %1946, %994
  %1009 = phi i64 [ 0, %994 ], [ %1947, %1946 ]
  br i1 %766, label %1015, label %1010

; <label>:1010:                                   ; preds = %1008
  %1011 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1009, i32 3, i32 0
  %1012 = load i32, i32* %1011, align 4, !tbaa !24
  %1013 = icmp sgt i32 %1012, -1
  br i1 %1013, label %1014, label %1015

; <label>:1014:                                   ; preds = %1010
  store i32 0, i32* %1011, align 4, !tbaa !24
  br label %1015

; <label>:1015:                                   ; preds = %1008, %1010, %1014, %987, %793
  %1016 = phi i32 [ %773, %793 ], [ 1, %987 ], [ 1, %1014 ], [ 1, %1010 ], [ 1, %1008 ]
  %1017 = phi i32 [ %770, %793 ], [ %992, %987 ], [ %992, %1014 ], [ %992, %1010 ], [ %992, %1008 ]
  %1018 = phi i32 [ %768, %793 ], [ %993, %987 ], [ %993, %1014 ], [ %993, %1010 ], [ %993, %1008 ]
  %1019 = sub nsw i32 0, %797
  store i32 %1019, i32* %796, align 4, !tbaa !24
  %1020 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %786, i32 0
  %1021 = load i32, i32* %1020, align 4, !tbaa !21
  %1022 = sext i32 %1021 to i64
  %1023 = getelementptr inbounds i32, i32* %3, i64 %1022
  %1024 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %786, i32 1
  %1025 = load i32, i32* %1024, align 4, !tbaa !23
  %1026 = sext i32 %1025 to i64
  %1027 = getelementptr inbounds i32, i32* %1023, i64 %1026
  %1028 = icmp sgt i32 %1025, 0
  br i1 %1028, label %1029, label %1030

; <label>:1029:                                   ; preds = %1015
  br label %1033

; <label>:1030:                                   ; preds = %1015
  store i32 %797, i32* %796, align 4, !tbaa !24
  %1031 = icmp sgt i32 %769, 0
  %1032 = select i1 %1031, i32 %769, i32 0
  br label %1138

; <label>:1033:                                   ; preds = %1029, %1078
  %1034 = phi i32 [ %1080, %1078 ], [ %1017, %1029 ]
  %1035 = phi i32* [ %1037, %1078 ], [ %1023, %1029 ]
  %1036 = phi i32 [ %1079, %1078 ], [ 0, %1029 ]
  %1037 = getelementptr inbounds i32, i32* %1035, i64 1
  %1038 = load i32, i32* %1035, align 4, !tbaa !7
  %1039 = sext i32 %1038 to i64
  %1040 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1039, i32 3, i32 0
  %1041 = load i32, i32* %1040, align 4, !tbaa !24
  %1042 = icmp sgt i32 %1041, -1
  br i1 %1042, label %1043, label %1078

; <label>:1043:                                   ; preds = %1033
  %1044 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1039, i32 0
  %1045 = load i32, i32* %1044, align 4, !tbaa !27
  %1046 = sext i32 %1045 to i64
  %1047 = getelementptr inbounds i32, i32* %3, i64 %1046
  %1048 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1039, i32 1
  %1049 = load i32, i32* %1048, align 4, !tbaa !25
  %1050 = sext i32 %1049 to i64
  %1051 = getelementptr inbounds i32, i32* %1047, i64 %1050
  %1052 = icmp sgt i32 %1049, 0
  br i1 %1052, label %1053, label %1078

; <label>:1053:                                   ; preds = %1043
  br label %1054

; <label>:1054:                                   ; preds = %1053, %1074
  %1055 = phi i32 [ %1076, %1074 ], [ %1034, %1053 ]
  %1056 = phi i32* [ %1058, %1074 ], [ %1047, %1053 ]
  %1057 = phi i32 [ %1075, %1074 ], [ %1036, %1053 ]
  %1058 = getelementptr inbounds i32, i32* %1056, i64 1
  %1059 = load i32, i32* %1056, align 4, !tbaa !7
  %1060 = sext i32 %1059 to i64
  %1061 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1060, i32 2, i32 0
  %1062 = load i32, i32* %1061, align 4, !tbaa !24
  %1063 = icmp sgt i32 %1062, 0
  br i1 %1063, label %1064, label %1074

; <label>:1064:                                   ; preds = %1054
  %1065 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1060, i32 0
  %1066 = load i32, i32* %1065, align 4, !tbaa !21
  %1067 = icmp sgt i32 %1066, -1
  br i1 %1067, label %1068, label %1074

; <label>:1068:                                   ; preds = %1064
  %1069 = sub nsw i32 0, %1062
  store i32 %1069, i32* %1061, align 4, !tbaa !24
  %1070 = add nsw i32 %1055, 1
  %1071 = sext i32 %1055 to i64
  %1072 = getelementptr inbounds i32, i32* %3, i64 %1071
  store i32 %1059, i32* %1072, align 4, !tbaa !7
  %1073 = add nsw i32 %1062, %1057
  br label %1074

; <label>:1074:                                   ; preds = %1068, %1064, %1054
  %1075 = phi i32 [ %1073, %1068 ], [ %1057, %1064 ], [ %1057, %1054 ]
  %1076 = phi i32 [ %1070, %1068 ], [ %1055, %1064 ], [ %1055, %1054 ]
  %1077 = icmp ult i32* %1058, %1051
  br i1 %1077, label %1054, label %1078

; <label>:1078:                                   ; preds = %1074, %1043, %1033
  %1079 = phi i32 [ %1036, %1033 ], [ %1036, %1043 ], [ %1075, %1074 ]
  %1080 = phi i32 [ %1034, %1033 ], [ %1034, %1043 ], [ %1076, %1074 ]
  %1081 = icmp ult i32* %1037, %1027
  br i1 %1081, label %1033, label %1082

; <label>:1082:                                   ; preds = %1078
  %1083 = load i32, i32* %1020, align 4, !tbaa !21
  %1084 = load i32, i32* %1024, align 4, !tbaa !23
  %1085 = sext i32 %1083 to i64
  %1086 = getelementptr inbounds i32, i32* %3, i64 %1085
  %1087 = sext i32 %1084 to i64
  %1088 = getelementptr inbounds i32, i32* %1086, i64 %1087
  store i32 %797, i32* %796, align 4, !tbaa !24
  %1089 = icmp sgt i32 %769, %1079
  %1090 = select i1 %1089, i32 %769, i32 %1079
  %1091 = icmp sgt i32 %1084, 0
  br i1 %1091, label %1092, label %1138

; <label>:1092:                                   ; preds = %1082
  %1093 = getelementptr i32, i32* %757, i64 %1085
  %1094 = icmp ugt i32* %1088, %1093
  %1095 = select i1 %1094, i32* %1088, i32* %1093
  %1096 = bitcast i32* %1095 to i8*
  %1097 = getelementptr i8, i8* %1096, i64 %758
  %1098 = mul nsw i64 %1085, -4
  %1099 = getelementptr i8, i8* %1097, i64 %1098
  %1100 = ptrtoint i8* %1099 to i64
  %1101 = lshr i64 %1100, 2
  %1102 = add nuw nsw i64 %1101, 1
  %1103 = and i64 %1102, 3
  %1104 = icmp eq i64 %1103, 0
  br i1 %1104, label %1115, label %1105

; <label>:1105:                                   ; preds = %1092
  br label %1106

; <label>:1106:                                   ; preds = %1106, %1105
  %1107 = phi i32* [ %1109, %1106 ], [ %1086, %1105 ]
  %1108 = phi i64 [ %1113, %1106 ], [ %1103, %1105 ]
  %1109 = getelementptr inbounds i32, i32* %1107, i64 1
  %1110 = load i32, i32* %1107, align 4, !tbaa !7
  %1111 = sext i32 %1110 to i64
  %1112 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1111, i32 3, i32 0
  store i32 -1, i32* %1112, align 4, !tbaa !24
  %1113 = add i64 %1108, -1
  %1114 = icmp eq i64 %1113, 0
  br i1 %1114, label %1115, label %1106, !llvm.loop !29

; <label>:1115:                                   ; preds = %1106, %1092
  %1116 = phi i32* [ %1086, %1092 ], [ %1109, %1106 ]
  %1117 = icmp ult i8* %1099, inttoptr (i64 12 to i8*)
  br i1 %1117, label %1138, label %1118

; <label>:1118:                                   ; preds = %1115
  br label %1119

; <label>:1119:                                   ; preds = %1119, %1118
  %1120 = phi i32* [ %1116, %1118 ], [ %1133, %1119 ]
  %1121 = getelementptr inbounds i32, i32* %1120, i64 1
  %1122 = load i32, i32* %1120, align 4, !tbaa !7
  %1123 = sext i32 %1122 to i64
  %1124 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1123, i32 3, i32 0
  store i32 -1, i32* %1124, align 4, !tbaa !24
  %1125 = getelementptr inbounds i32, i32* %1120, i64 2
  %1126 = load i32, i32* %1121, align 4, !tbaa !7
  %1127 = sext i32 %1126 to i64
  %1128 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1127, i32 3, i32 0
  store i32 -1, i32* %1128, align 4, !tbaa !24
  %1129 = getelementptr inbounds i32, i32* %1120, i64 3
  %1130 = load i32, i32* %1125, align 4, !tbaa !7
  %1131 = sext i32 %1130 to i64
  %1132 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1131, i32 3, i32 0
  store i32 -1, i32* %1132, align 4, !tbaa !24
  %1133 = getelementptr inbounds i32, i32* %1120, i64 4
  %1134 = load i32, i32* %1129, align 4, !tbaa !7
  %1135 = sext i32 %1134 to i64
  %1136 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1135, i32 3, i32 0
  store i32 -1, i32* %1136, align 4, !tbaa !24
  %1137 = icmp ult i32* %1133, %1088
  br i1 %1137, label %1119, label %1138

; <label>:1138:                                   ; preds = %1115, %1119, %1082, %1030
  %1139 = phi i32 [ %1032, %1030 ], [ %1090, %1082 ], [ %1090, %1119 ], [ %1090, %1115 ]
  %1140 = phi i32 [ %1017, %1030 ], [ %1080, %1082 ], [ %1080, %1119 ], [ %1080, %1115 ]
  %1141 = phi i32 [ 0, %1030 ], [ %1079, %1082 ], [ %1079, %1119 ], [ %1079, %1115 ]
  %1142 = sub nsw i32 %1140, %1017
  %1143 = icmp sgt i32 %1142, 0
  br i1 %1143, label %1149, label %1144

; <label>:1144:                                   ; preds = %1138
  %1145 = sext i32 %1017 to i64
  %1146 = getelementptr inbounds i32, i32* %3, i64 %1145
  %1147 = sext i32 %1142 to i64
  %1148 = getelementptr inbounds i32, i32* %1146, i64 %1147
  br label %1499

; <label>:1149:                                   ; preds = %1138
  %1150 = load i32, i32* %1020, align 4, !tbaa !21
  %1151 = sext i32 %1150 to i64
  %1152 = getelementptr inbounds i32, i32* %3, i64 %1151
  %1153 = load i32, i32* %1152, align 4, !tbaa !7
  %1154 = sext i32 %1017 to i64
  %1155 = getelementptr inbounds i32, i32* %3, i64 %1154
  %1156 = sext i32 %1142 to i64
  %1157 = getelementptr inbounds i32, i32* %1155, i64 %1156
  br i1 %1929, label %1159, label %1158

; <label>:1158:                                   ; preds = %1149
  br label %1160

; <label>:1159:                                   ; preds = %1149
  br label %1222

; <label>:1160:                                   ; preds = %1158, %1198
  %1161 = phi i32* [ %1162, %1198 ], [ %1155, %1158 ]
  %1162 = getelementptr inbounds i32, i32* %1161, i64 1
  %1163 = load i32, i32* %1161, align 4, !tbaa !7
  %1164 = sext i32 %1163 to i64
  %1165 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 2, i32 0
  %1166 = load i32, i32* %1165, align 4, !tbaa !24
  %1167 = sub nsw i32 0, %1166
  store i32 %1167, i32* %1165, align 4, !tbaa !24
  %1168 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 4, i32 0
  %1169 = load i32, i32* %1168, align 4, !tbaa !24
  %1170 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 5, i32 0
  %1171 = load i32, i32* %1170, align 4, !tbaa !24
  %1172 = icmp eq i32 %1169, -1
  br i1 %1172, label %1176, label %1173

; <label>:1173:                                   ; preds = %1160
  %1174 = sext i32 %1169 to i64
  %1175 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1174, i32 5, i32 0
  br label %1181

; <label>:1176:                                   ; preds = %1160
  %1177 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 3, i32 0
  %1178 = load i32, i32* %1177, align 4, !tbaa !24
  %1179 = sext i32 %1178 to i64
  %1180 = getelementptr inbounds i32, i32* %4, i64 %1179
  br label %1181

; <label>:1181:                                   ; preds = %1176, %1173
  %1182 = phi i32* [ %1180, %1176 ], [ %1175, %1173 ]
  store i32 %1171, i32* %1182, align 4, !tbaa !24
  %1183 = icmp eq i32 %1171, -1
  br i1 %1183, label %1187, label %1184

; <label>:1184:                                   ; preds = %1181
  %1185 = sext i32 %1171 to i64
  %1186 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1185, i32 4, i32 0
  store i32 %1169, i32* %1186, align 4, !tbaa !24
  br label %1187

; <label>:1187:                                   ; preds = %1184, %1181
  %1188 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 0
  %1189 = load i32, i32* %1188, align 4, !tbaa !21
  %1190 = sext i32 %1189 to i64
  %1191 = getelementptr inbounds i32, i32* %3, i64 %1190
  %1192 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1164, i32 1
  %1193 = load i32, i32* %1192, align 4, !tbaa !23
  %1194 = sext i32 %1193 to i64
  %1195 = getelementptr inbounds i32, i32* %1191, i64 %1194
  %1196 = icmp sgt i32 %1193, 0
  br i1 %1196, label %1197, label %1198

; <label>:1197:                                   ; preds = %1187
  br label %1200

; <label>:1198:                                   ; preds = %1220, %1187
  %1199 = icmp ult i32* %1162, %1157
  br i1 %1199, label %1160, label %1282

; <label>:1200:                                   ; preds = %1197, %1220
  %1201 = phi i32* [ %1202, %1220 ], [ %1191, %1197 ]
  %1202 = getelementptr inbounds i32, i32* %1201, i64 1
  %1203 = load i32, i32* %1201, align 4, !tbaa !7
  %1204 = sext i32 %1203 to i64
  %1205 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1204, i32 3, i32 0
  %1206 = load i32, i32* %1205, align 4, !tbaa !24
  %1207 = icmp slt i32 %1206, 0
  br i1 %1207, label %1220, label %1208

; <label>:1208:                                   ; preds = %1200
  %1209 = sub nsw i32 %1206, %1016
  %1210 = icmp slt i32 %1209, 0
  br i1 %1210, label %1211, label %1214

; <label>:1211:                                   ; preds = %1208
  %1212 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1204, i32 2, i32 0
  %1213 = load i32, i32* %1212, align 4, !tbaa !24
  br label %1214

; <label>:1214:                                   ; preds = %1211, %1208
  %1215 = phi i32 [ %1213, %1211 ], [ %1209, %1208 ]
  %1216 = add nsw i32 %1215, %1166
  %1217 = icmp eq i32 %1216, 0
  %1218 = add nsw i32 %1216, %1016
  %1219 = select i1 %1217, i32 -1, i32 %1218
  store i32 %1219, i32* %1205, align 4, !tbaa !24
  br label %1220

; <label>:1220:                                   ; preds = %1214, %1200
  %1221 = icmp ult i32* %1202, %1195
  br i1 %1221, label %1200, label %1198

; <label>:1222:                                   ; preds = %1159, %1280
  %1223 = phi i32* [ %1224, %1280 ], [ %1155, %1159 ]
  %1224 = getelementptr inbounds i32, i32* %1223, i64 1
  %1225 = load i32, i32* %1223, align 4, !tbaa !7
  %1226 = sext i32 %1225 to i64
  %1227 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 2, i32 0
  %1228 = load i32, i32* %1227, align 4, !tbaa !24
  %1229 = sub nsw i32 0, %1228
  store i32 %1229, i32* %1227, align 4, !tbaa !24
  %1230 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 4, i32 0
  %1231 = load i32, i32* %1230, align 4, !tbaa !24
  %1232 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 5, i32 0
  %1233 = load i32, i32* %1232, align 4, !tbaa !24
  %1234 = icmp eq i32 %1231, -1
  br i1 %1234, label %1235, label %1240

; <label>:1235:                                   ; preds = %1222
  %1236 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 3, i32 0
  %1237 = load i32, i32* %1236, align 4, !tbaa !24
  %1238 = sext i32 %1237 to i64
  %1239 = getelementptr inbounds i32, i32* %4, i64 %1238
  br label %1243

; <label>:1240:                                   ; preds = %1222
  %1241 = sext i32 %1231 to i64
  %1242 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1241, i32 5, i32 0
  br label %1243

; <label>:1243:                                   ; preds = %1240, %1235
  %1244 = phi i32* [ %1242, %1240 ], [ %1239, %1235 ]
  store i32 %1233, i32* %1244, align 4, !tbaa !24
  %1245 = icmp eq i32 %1233, -1
  br i1 %1245, label %1249, label %1246

; <label>:1246:                                   ; preds = %1243
  %1247 = sext i32 %1233 to i64
  %1248 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1247, i32 4, i32 0
  store i32 %1231, i32* %1248, align 4, !tbaa !24
  br label %1249

; <label>:1249:                                   ; preds = %1246, %1243
  %1250 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 0
  %1251 = load i32, i32* %1250, align 4, !tbaa !21
  %1252 = sext i32 %1251 to i64
  %1253 = getelementptr inbounds i32, i32* %3, i64 %1252
  %1254 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1226, i32 1
  %1255 = load i32, i32* %1254, align 4, !tbaa !23
  %1256 = sext i32 %1255 to i64
  %1257 = getelementptr inbounds i32, i32* %1253, i64 %1256
  %1258 = icmp sgt i32 %1255, 0
  br i1 %1258, label %1259, label %1280

; <label>:1259:                                   ; preds = %1249
  %1260 = add i32 %1228, %1016
  br label %1261

; <label>:1261:                                   ; preds = %1269, %1259
  %1262 = phi i32* [ %1253, %1259 ], [ %1263, %1269 ]
  %1263 = getelementptr inbounds i32, i32* %1262, i64 1
  %1264 = load i32, i32* %1262, align 4, !tbaa !7
  %1265 = sext i32 %1264 to i64
  %1266 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1265, i32 3, i32 0
  %1267 = load i32, i32* %1266, align 4, !tbaa !24
  %1268 = icmp slt i32 %1267, 0
  br i1 %1268, label %1269, label %1271

; <label>:1269:                                   ; preds = %1277, %1261
  %1270 = icmp ult i32* %1263, %1257
  br i1 %1270, label %1261, label %1280

; <label>:1271:                                   ; preds = %1261
  %1272 = sub nsw i32 %1267, %1016
  %1273 = icmp slt i32 %1272, 0
  br i1 %1273, label %1274, label %1277

; <label>:1274:                                   ; preds = %1271
  %1275 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1265, i32 2, i32 0
  %1276 = load i32, i32* %1275, align 4, !tbaa !24
  br label %1277

; <label>:1277:                                   ; preds = %1274, %1271
  %1278 = phi i32 [ %1276, %1274 ], [ %1272, %1271 ]
  %1279 = add i32 %1260, %1278
  store i32 %1279, i32* %1266, align 4, !tbaa !24
  br label %1269

; <label>:1280:                                   ; preds = %1269, %1249
  %1281 = icmp ult i32* %1224, %1157
  br i1 %1281, label %1222, label %1282

; <label>:1282:                                   ; preds = %1198, %1280
  br label %1283

; <label>:1283:                                   ; preds = %1282, %1360
  %1284 = phi i32 [ %1362, %1360 ], [ %798, %1282 ]
  %1285 = phi i32* [ %1287, %1360 ], [ %1155, %1282 ]
  %1286 = phi i32 [ %1361, %1360 ], [ %1141, %1282 ]
  %1287 = getelementptr inbounds i32, i32* %1285, i64 1
  %1288 = load i32, i32* %1285, align 4, !tbaa !7
  %1289 = sext i32 %1288 to i64
  %1290 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 0
  %1291 = load i32, i32* %1290, align 4, !tbaa !21
  %1292 = sext i32 %1291 to i64
  %1293 = getelementptr inbounds i32, i32* %3, i64 %1292
  %1294 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 1
  %1295 = load i32, i32* %1294, align 4, !tbaa !23
  %1296 = sext i32 %1295 to i64
  %1297 = getelementptr inbounds i32, i32* %1293, i64 %1296
  %1298 = icmp sgt i32 %1295, 0
  br i1 %1298, label %1299, label %1323

; <label>:1299:                                   ; preds = %1283
  br label %1300

; <label>:1300:                                   ; preds = %1299, %1315
  %1301 = phi i32* [ %1309, %1315 ], [ %1293, %1299 ]
  %1302 = phi i32 [ %1317, %1315 ], [ 0, %1299 ]
  %1303 = phi i32 [ %1321, %1315 ], [ 0, %1299 ]
  %1304 = phi i32* [ %1316, %1315 ], [ %1293, %1299 ]
  br label %1307

; <label>:1305:                                   ; preds = %1307
  %1306 = icmp ult i32* %1309, %1297
  br i1 %1306, label %1307, label %1323

; <label>:1307:                                   ; preds = %1305, %1300
  %1308 = phi i32* [ %1301, %1300 ], [ %1309, %1305 ]
  %1309 = getelementptr inbounds i32, i32* %1308, i64 1
  %1310 = load i32, i32* %1308, align 4, !tbaa !7
  %1311 = sext i32 %1310 to i64
  %1312 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1311, i32 3, i32 0
  %1313 = load i32, i32* %1312, align 4, !tbaa !24
  %1314 = icmp slt i32 %1313, 0
  br i1 %1314, label %1305, label %1315

; <label>:1315:                                   ; preds = %1307
  %1316 = getelementptr inbounds i32, i32* %1304, i64 1
  store i32 %1310, i32* %1304, align 4, !tbaa !7
  %1317 = add i32 %1310, %1302
  %1318 = sub nsw i32 %1313, %1016
  %1319 = add nsw i32 %1318, %1303
  %1320 = icmp slt i32 %1319, %1
  %1321 = select i1 %1320, i32 %1319, i32 %1
  %1322 = icmp ult i32* %1309, %1297
  br i1 %1322, label %1300, label %1323

; <label>:1323:                                   ; preds = %1315, %1305, %1283
  %1324 = phi i32* [ %1293, %1283 ], [ %1304, %1305 ], [ %1316, %1315 ]
  %1325 = phi i32 [ 0, %1283 ], [ %1303, %1305 ], [ %1321, %1315 ]
  %1326 = phi i32 [ 0, %1283 ], [ %1302, %1305 ], [ %1317, %1315 ]
  %1327 = load i32, i32* %1290, align 4, !tbaa !21
  %1328 = sext i32 %1327 to i64
  %1329 = getelementptr inbounds i32, i32* %3, i64 %1328
  %1330 = ptrtoint i32* %1324 to i64
  %1331 = ptrtoint i32* %1329 to i64
  %1332 = sub i64 %1330, %1331
  %1333 = lshr exact i64 %1332, 2
  %1334 = trunc i64 %1333 to i32
  store i32 %1334, i32* %1294, align 4, !tbaa !23
  %1335 = icmp eq i32 %1334, 0
  br i1 %1335, label %1336, label %1342

; <label>:1336:                                   ; preds = %1323
  store i32 -1, i32* %1290, align 4, !tbaa !21
  %1337 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 2, i32 0
  %1338 = load i32, i32* %1337, align 4, !tbaa !24
  %1339 = sub nsw i32 %1286, %1338
  %1340 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 3, i32 0
  store i32 %1284, i32* %1340, align 4, !tbaa !24
  %1341 = add nsw i32 %1338, %1284
  br label %1360

; <label>:1342:                                   ; preds = %1323
  %1343 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 3, i32 0
  store i32 %1325, i32* %1343, align 4, !tbaa !24
  %1344 = urem i32 %1326, %754
  %1345 = zext i32 %1344 to i64
  %1346 = getelementptr inbounds i32, i32* %4, i64 %1345
  %1347 = load i32, i32* %1346, align 4, !tbaa !7
  %1348 = icmp sgt i32 %1347, -1
  br i1 %1348, label %1349, label %1353

; <label>:1349:                                   ; preds = %1342
  %1350 = sext i32 %1347 to i64
  %1351 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1350, i32 4, i32 0
  %1352 = load i32, i32* %1351, align 4, !tbaa !24
  store i32 %1288, i32* %1351, align 4, !tbaa !24
  br label %1356

; <label>:1353:                                   ; preds = %1342
  %1354 = sub i32 -2, %1347
  %1355 = sub i32 -2, %1288
  store i32 %1355, i32* %1346, align 4, !tbaa !7
  br label %1356

; <label>:1356:                                   ; preds = %1353, %1349
  %1357 = phi i32 [ %1352, %1349 ], [ %1354, %1353 ]
  %1358 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 5, i32 0
  store i32 %1357, i32* %1358, align 4, !tbaa !24
  %1359 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1289, i32 4, i32 0
  store i32 %1344, i32* %1359, align 4, !tbaa !24
  br label %1360

; <label>:1360:                                   ; preds = %1356, %1336
  %1361 = phi i32 [ %1339, %1336 ], [ %1286, %1356 ]
  %1362 = phi i32 [ %1341, %1336 ], [ %1284, %1356 ]
  %1363 = icmp ult i32* %1287, %1157
  br i1 %1363, label %1283, label %1364

; <label>:1364:                                   ; preds = %1360
  br label %1365

; <label>:1365:                                   ; preds = %1364, %1373
  %1366 = phi i32* [ %1367, %1373 ], [ %1155, %1364 ]
  %1367 = getelementptr inbounds i32, i32* %1366, i64 1
  %1368 = load i32, i32* %1366, align 4, !tbaa !7
  %1369 = sext i32 %1368 to i64
  %1370 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1369, i32 0
  %1371 = load i32, i32* %1370, align 4, !tbaa !21
  %1372 = icmp slt i32 %1371, 0
  br i1 %1372, label %1373, label %1375

; <label>:1373:                                   ; preds = %1495, %1365
  %1374 = icmp ult i32* %1367, %1157
  br i1 %1374, label %1365, label %1499

; <label>:1375:                                   ; preds = %1365
  %1376 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1369, i32 4, i32 0
  %1377 = load i32, i32* %1376, align 4, !tbaa !24
  %1378 = sext i32 %1377 to i64
  %1379 = getelementptr inbounds i32, i32* %4, i64 %1378
  %1380 = load i32, i32* %1379, align 4, !tbaa !7
  %1381 = icmp sgt i32 %1380, -1
  br i1 %1381, label %1382, label %1386

; <label>:1382:                                   ; preds = %1375
  %1383 = sext i32 %1380 to i64
  %1384 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1383, i32 4, i32 0
  %1385 = load i32, i32* %1384, align 4, !tbaa !24
  br label %1388

; <label>:1386:                                   ; preds = %1375
  %1387 = sub i32 -2, %1380
  br label %1388

; <label>:1388:                                   ; preds = %1386, %1382
  %1389 = phi i32 [ %1385, %1382 ], [ %1387, %1386 ]
  %1390 = icmp eq i32 %1389, -1
  br i1 %1390, label %1495, label %1391

; <label>:1391:                                   ; preds = %1388
  br label %1392

; <label>:1392:                                   ; preds = %1391, %1492
  %1393 = phi i32 [ %1493, %1492 ], [ %1389, %1391 ]
  %1394 = sext i32 %1393 to i64
  %1395 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1394, i32 1
  %1396 = load i32, i32* %1395, align 4, !tbaa !23
  %1397 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1394, i32 5, i32 0
  %1398 = load i32, i32* %1397, align 4, !tbaa !24
  %1399 = icmp eq i32 %1398, -1
  br i1 %1399, label %1495, label %1400

; <label>:1400:                                   ; preds = %1392
  %1401 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1394, i32 3, i32 0
  %1402 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1394, i32 0
  %1403 = icmp sgt i32 %1396, 0
  %1404 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1394, i32 2, i32 0
  br i1 %1403, label %1405, label %1455

; <label>:1405:                                   ; preds = %1400
  br label %1406

; <label>:1406:                                   ; preds = %1405, %1445
  %1407 = phi i32 [ %1448, %1445 ], [ %1398, %1405 ]
  %1408 = phi i32 [ %1446, %1445 ], [ %1393, %1405 ]
  %1409 = sext i32 %1407 to i64
  %1410 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 1
  %1411 = load i32, i32* %1410, align 4, !tbaa !23
  %1412 = icmp eq i32 %1411, %1396
  br i1 %1412, label %1413, label %1445

; <label>:1413:                                   ; preds = %1406
  %1414 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 3, i32 0
  %1415 = load i32, i32* %1414, align 4, !tbaa !24
  %1416 = load i32, i32* %1401, align 4, !tbaa !24
  %1417 = icmp eq i32 %1415, %1416
  br i1 %1417, label %1418, label %1445

; <label>:1418:                                   ; preds = %1413
  %1419 = load i32, i32* %1402, align 4, !tbaa !21
  %1420 = sext i32 %1419 to i64
  %1421 = getelementptr inbounds i32, i32* %3, i64 %1420
  %1422 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 0
  %1423 = load i32, i32* %1422, align 4, !tbaa !21
  %1424 = sext i32 %1423 to i64
  %1425 = getelementptr inbounds i32, i32* %3, i64 %1424
  br label %1426

; <label>:1426:                                   ; preds = %1450, %1418
  %1427 = phi i32 [ 0, %1418 ], [ %1453, %1450 ]
  %1428 = phi i32* [ %1421, %1418 ], [ %1452, %1450 ]
  %1429 = phi i32* [ %1425, %1418 ], [ %1451, %1450 ]
  %1430 = load i32, i32* %1428, align 4, !tbaa !7
  %1431 = load i32, i32* %1429, align 4, !tbaa !7
  %1432 = icmp eq i32 %1430, %1431
  br i1 %1432, label %1450, label %1433

; <label>:1433:                                   ; preds = %1450, %1426
  %1434 = phi i32 [ %1427, %1426 ], [ %1453, %1450 ]
  %1435 = icmp eq i32 %1434, %1396
  br i1 %1435, label %1436, label %1445

; <label>:1436:                                   ; preds = %1433
  %1437 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 2, i32 0
  %1438 = load i32, i32* %1437, align 4, !tbaa !24
  %1439 = load i32, i32* %1404, align 4, !tbaa !24
  %1440 = add nsw i32 %1439, %1438
  store i32 %1440, i32* %1404, align 4, !tbaa !24
  store i32 %1393, i32* %1437, align 4, !tbaa !24
  store i32 -2, i32* %1422, align 4, !tbaa !21
  store i32 -1, i32* %1414, align 4, !tbaa !24
  %1441 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 5, i32 0
  %1442 = load i32, i32* %1441, align 4, !tbaa !24
  %1443 = sext i32 %1408 to i64
  %1444 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1443, i32 5, i32 0
  store i32 %1442, i32* %1444, align 4, !tbaa !24
  br label %1445

; <label>:1445:                                   ; preds = %1436, %1433, %1413, %1406
  %1446 = phi i32 [ %1408, %1436 ], [ %1407, %1413 ], [ %1407, %1406 ], [ %1407, %1433 ]
  %1447 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1409, i32 5, i32 0
  %1448 = load i32, i32* %1447, align 4, !tbaa !24
  %1449 = icmp eq i32 %1448, -1
  br i1 %1449, label %1492, label %1406

; <label>:1450:                                   ; preds = %1426
  %1451 = getelementptr inbounds i32, i32* %1429, i64 1
  %1452 = getelementptr inbounds i32, i32* %1428, i64 1
  %1453 = add nuw nsw i32 %1427, 1
  %1454 = icmp slt i32 %1453, %1396
  br i1 %1454, label %1426, label %1433

; <label>:1455:                                   ; preds = %1400
  %1456 = icmp eq i32 %1396, 0
  br i1 %1456, label %1458, label %1457

; <label>:1457:                                   ; preds = %1455
  br label %1459

; <label>:1458:                                   ; preds = %1455
  br label %1465

; <label>:1459:                                   ; preds = %1457, %1459
  %1460 = phi i32 [ %1463, %1459 ], [ %1398, %1457 ]
  %1461 = sext i32 %1460 to i64
  %1462 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1461, i32 5, i32 0
  %1463 = load i32, i32* %1462, align 4, !tbaa !24
  %1464 = icmp eq i32 %1463, -1
  br i1 %1464, label %1492, label %1459

; <label>:1465:                                   ; preds = %1458, %1487
  %1466 = phi i32 [ %1490, %1487 ], [ %1398, %1458 ]
  %1467 = phi i32 [ %1488, %1487 ], [ %1393, %1458 ]
  %1468 = sext i32 %1466 to i64
  %1469 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 1
  %1470 = load i32, i32* %1469, align 4, !tbaa !23
  %1471 = icmp eq i32 %1470, 0
  br i1 %1471, label %1472, label %1487

; <label>:1472:                                   ; preds = %1465
  %1473 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 3, i32 0
  %1474 = load i32, i32* %1473, align 4, !tbaa !24
  %1475 = load i32, i32* %1401, align 4, !tbaa !24
  %1476 = icmp eq i32 %1474, %1475
  br i1 %1476, label %1477, label %1487

; <label>:1477:                                   ; preds = %1472
  %1478 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 0
  %1479 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 2, i32 0
  %1480 = load i32, i32* %1479, align 4, !tbaa !24
  %1481 = load i32, i32* %1404, align 4, !tbaa !24
  %1482 = add nsw i32 %1481, %1480
  store i32 %1482, i32* %1404, align 4, !tbaa !24
  store i32 %1393, i32* %1479, align 4, !tbaa !24
  store i32 -2, i32* %1478, align 4, !tbaa !21
  store i32 -1, i32* %1473, align 4, !tbaa !24
  %1483 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 5, i32 0
  %1484 = load i32, i32* %1483, align 4, !tbaa !24
  %1485 = sext i32 %1467 to i64
  %1486 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1485, i32 5, i32 0
  store i32 %1484, i32* %1486, align 4, !tbaa !24
  br label %1487

; <label>:1487:                                   ; preds = %1477, %1472, %1465
  %1488 = phi i32 [ %1467, %1477 ], [ %1466, %1472 ], [ %1466, %1465 ]
  %1489 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1468, i32 5, i32 0
  %1490 = load i32, i32* %1489, align 4, !tbaa !24
  %1491 = icmp eq i32 %1490, -1
  br i1 %1491, label %1492, label %1465

; <label>:1492:                                   ; preds = %1459, %1487, %1445
  %1493 = load i32, i32* %1397, align 4, !tbaa !24
  %1494 = icmp eq i32 %1493, -1
  br i1 %1494, label %1495, label %1392

; <label>:1495:                                   ; preds = %1492, %1392, %1388
  %1496 = sext i32 %1380 to i64
  %1497 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1496, i32 4, i32 0
  %1498 = select i1 %1381, i32* %1497, i32* %1379
  store i32 -1, i32* %1498, align 4, !tbaa !24
  br label %1373

; <label>:1499:                                   ; preds = %1373, %1144
  %1500 = phi i32 [ %798, %1144 ], [ %1362, %1373 ]
  %1501 = phi i32 [ %1141, %1144 ], [ %1361, %1373 ]
  %1502 = phi i32* [ %1148, %1144 ], [ %1157, %1373 ]
  %1503 = phi i32* [ %1146, %1144 ], [ %1155, %1373 ]
  %1504 = phi i32 [ -1, %1144 ], [ %1153, %1373 ]
  store i32 -1, i32* %1020, align 4, !tbaa !21
  %1505 = add nsw i32 %1139, %1016
  %1506 = add nsw i32 %1505, 1
  %1507 = icmp sgt i32 %1505, -1
  %1508 = icmp slt i32 %1506, %722
  %1509 = and i1 %1507, %1508
  %1510 = or i1 %1509, %755
  %1511 = select i1 %1509, i32 %1506, i32 1
  br i1 %1510, label %1533, label %1512

; <label>:1512:                                   ; preds = %1499
  br i1 %760, label %1526, label %1513

; <label>:1513:                                   ; preds = %1512
  br label %1514

; <label>:1514:                                   ; preds = %1951, %1513
  %1515 = phi i64 [ 0, %1513 ], [ %1952, %1951 ]
  %1516 = phi i64 [ %761, %1513 ], [ %1953, %1951 ]
  %1517 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1515, i32 3, i32 0
  %1518 = load i32, i32* %1517, align 4, !tbaa !24
  %1519 = icmp sgt i32 %1518, -1
  br i1 %1519, label %1520, label %1521

; <label>:1520:                                   ; preds = %1514
  store i32 0, i32* %1517, align 4, !tbaa !24
  br label %1521

; <label>:1521:                                   ; preds = %1520, %1514
  %1522 = or i64 %1515, 1
  %1523 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1522, i32 3, i32 0
  %1524 = load i32, i32* %1523, align 4, !tbaa !24
  %1525 = icmp sgt i32 %1524, -1
  br i1 %1525, label %1950, label %1951

; <label>:1526:                                   ; preds = %1951, %1512
  %1527 = phi i64 [ 0, %1512 ], [ %1952, %1951 ]
  br i1 %762, label %1533, label %1528

; <label>:1528:                                   ; preds = %1526
  %1529 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1527, i32 3, i32 0
  %1530 = load i32, i32* %1529, align 4, !tbaa !24
  %1531 = icmp sgt i32 %1530, -1
  br i1 %1531, label %1532, label %1533

; <label>:1532:                                   ; preds = %1528
  store i32 0, i32* %1529, align 4, !tbaa !24
  br label %1533

; <label>:1533:                                   ; preds = %1526, %1528, %1532, %1499
  %1534 = phi i32 [ %1511, %1499 ], [ 1, %1532 ], [ 1, %1528 ], [ 1, %1526 ]
  br i1 %1143, label %1535, label %1583

; <label>:1535:                                   ; preds = %1533
  %1536 = sub nsw i32 %1, %1500
  br label %1537

; <label>:1537:                                   ; preds = %1579, %1535
  %1538 = phi i32 [ %785, %1535 ], [ %1581, %1579 ]
  %1539 = phi i32* [ %1503, %1535 ], [ %1545, %1579 ]
  %1540 = phi i32* [ %1503, %1535 ], [ %1553, %1579 ]
  br label %1543

; <label>:1541:                                   ; preds = %1543
  %1542 = icmp ult i32* %1545, %1502
  br i1 %1542, label %1543, label %1583

; <label>:1543:                                   ; preds = %1541, %1537
  %1544 = phi i32* [ %1539, %1537 ], [ %1545, %1541 ]
  %1545 = getelementptr inbounds i32, i32* %1544, i64 1
  %1546 = load i32, i32* %1544, align 4, !tbaa !7
  %1547 = sext i32 %1546 to i64
  %1548 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1547, i32 0
  %1549 = load i32, i32* %1548, align 4, !tbaa !21
  %1550 = icmp slt i32 %1549, 0
  br i1 %1550, label %1541, label %1551

; <label>:1551:                                   ; preds = %1543
  %1552 = sext i32 %1546 to i64
  %1553 = getelementptr inbounds i32, i32* %1540, i64 1
  store i32 %1546, i32* %1540, align 4, !tbaa !7
  %1554 = load i32, i32* %1548, align 4, !tbaa !21
  %1555 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1552, i32 1
  %1556 = load i32, i32* %1555, align 4, !tbaa !23
  %1557 = add nsw i32 %1556, 1
  store i32 %1557, i32* %1555, align 4, !tbaa !23
  %1558 = add nsw i32 %1556, %1554
  %1559 = sext i32 %1558 to i64
  %1560 = getelementptr inbounds i32, i32* %3, i64 %1559
  store i32 %1504, i32* %1560, align 4, !tbaa !7
  %1561 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1552, i32 3, i32 0
  %1562 = load i32, i32* %1561, align 4, !tbaa !24
  %1563 = add nsw i32 %1562, %1501
  %1564 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1552, i32 2, i32 0
  %1565 = load i32, i32* %1564, align 4, !tbaa !24
  %1566 = sub i32 %1536, %1565
  %1567 = sub i32 %1563, %1565
  %1568 = icmp slt i32 %1567, %1566
  %1569 = select i1 %1568, i32 %1567, i32 %1566
  store i32 %1569, i32* %1561, align 4, !tbaa !24
  %1570 = sext i32 %1569 to i64
  %1571 = getelementptr inbounds i32, i32* %4, i64 %1570
  %1572 = load i32, i32* %1571, align 4, !tbaa !7
  %1573 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1552, i32 5, i32 0
  store i32 %1572, i32* %1573, align 4, !tbaa !24
  %1574 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1552, i32 4, i32 0
  store i32 -1, i32* %1574, align 4, !tbaa !24
  %1575 = icmp eq i32 %1572, -1
  br i1 %1575, label %1579, label %1576

; <label>:1576:                                   ; preds = %1551
  %1577 = sext i32 %1572 to i64
  %1578 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1577, i32 4, i32 0
  store i32 %1546, i32* %1578, align 4, !tbaa !24
  br label %1579

; <label>:1579:                                   ; preds = %1576, %1551
  store i32 %1546, i32* %1571, align 4, !tbaa !7
  %1580 = icmp slt i32 %1538, %1569
  %1581 = select i1 %1580, i32 %1538, i32 %1569
  %1582 = icmp ult i32* %1545, %1502
  br i1 %1582, label %1537, label %1583

; <label>:1583:                                   ; preds = %1579, %1541, %1533
  %1584 = phi i32* [ %1503, %1533 ], [ %1540, %1541 ], [ %1553, %1579 ]
  %1585 = phi i32 [ %785, %1533 ], [ %1538, %1541 ], [ %1581, %1579 ]
  %1586 = icmp sgt i32 %1501, 0
  br i1 %1586, label %1587, label %1598

; <label>:1587:                                   ; preds = %1583
  %1588 = sext i32 %1504 to i64
  %1589 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1588, i32 0
  store i32 %1017, i32* %1589, align 4, !tbaa !27
  %1590 = ptrtoint i32* %1584 to i64
  %1591 = ptrtoint i32* %1503 to i64
  %1592 = sub i64 %1590, %1591
  %1593 = lshr exact i64 %1592, 2
  %1594 = trunc i64 %1593 to i32
  %1595 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1588, i32 1
  store i32 %1594, i32* %1595, align 4, !tbaa !25
  %1596 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1588, i32 2, i32 0
  store i32 %1501, i32* %1596, align 4, !tbaa !24
  %1597 = getelementptr inbounds %struct.Colamd_Row_struct, %struct.Colamd_Row_struct* %97, i64 %1588, i32 3, i32 0
  store i32 0, i32* %1597, align 4, !tbaa !24
  br label %1598

; <label>:1598:                                   ; preds = %1587, %1583
  %1599 = icmp slt i32 %1500, %720
  br i1 %1599, label %767, label %1600

; <label>:1600:                                   ; preds = %1598, %749
  %1601 = phi i32 [ 0, %749 ], [ %1018, %1598 ]
  br i1 %98, label %1602, label %1694

; <label>:1602:                                   ; preds = %1600
  %1603 = zext i32 %1 to i64
  br label %1604

; <label>:1604:                                   ; preds = %1641, %1602
  %1605 = phi i64 [ 0, %1602 ], [ %1642, %1641 ]
  %1606 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1605, i32 0
  %1607 = load i32, i32* %1606, align 4, !tbaa !21
  %1608 = icmp eq i32 %1607, -1
  br i1 %1608, label %1641, label %1609

; <label>:1609:                                   ; preds = %1604
  %1610 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1605, i32 3, i32 0
  %1611 = load i32, i32* %1610, align 4, !tbaa !24
  %1612 = icmp eq i32 %1611, -1
  br i1 %1612, label %1613, label %1641

; <label>:1613:                                   ; preds = %1609
  %1614 = trunc i64 %1605 to i32
  br label %1615

; <label>:1615:                                   ; preds = %1615, %1613
  %1616 = phi i32 [ %1614, %1613 ], [ %1619, %1615 ]
  %1617 = sext i32 %1616 to i64
  %1618 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1617, i32 2, i32 0
  %1619 = load i32, i32* %1618, align 4, !tbaa !24
  %1620 = sext i32 %1619 to i64
  %1621 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1620, i32 0
  %1622 = load i32, i32* %1621, align 4, !tbaa !21
  %1623 = icmp eq i32 %1622, -1
  br i1 %1623, label %1624, label %1615

; <label>:1624:                                   ; preds = %1615
  %1625 = sext i32 %1619 to i64
  %1626 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1625, i32 3, i32 0
  %1627 = load i32, i32* %1626, align 4, !tbaa !24
  %1628 = add nsw i32 %1627, 1
  store i32 %1627, i32* %1610, align 4, !tbaa !24
  %1629 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1605, i32 2, i32 0
  store i32 %1619, i32* %1629, align 4, !tbaa !24
  %1630 = load i32, i32* %1626, align 4, !tbaa !24
  %1631 = icmp eq i32 %1630, -1
  br i1 %1631, label %1632, label %1639

; <label>:1632:                                   ; preds = %1624
  br label %1633

; <label>:1633:                                   ; preds = %1632, %1633
  %1634 = phi i32 [ 0, %1633 ], [ %1628, %1632 ]
  %1635 = icmp eq i32 %1634, -1
  br i1 %1635, label %1633, label %1636, !llvm.loop !30

; <label>:1636:                                   ; preds = %1633
  %1637 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1625, i32 2, i32 0
  store i32 %1634, i32* %1626, align 4, !tbaa !24
  store i32 %1619, i32* %1637, align 4, !tbaa !24
  %1638 = add nsw i32 %1634, 1
  br label %1639

; <label>:1639:                                   ; preds = %1636, %1624
  %1640 = phi i32 [ %1628, %1624 ], [ %1638, %1636 ]
  store i32 %1640, i32* %1626, align 4, !tbaa !24
  br label %1641

; <label>:1641:                                   ; preds = %1639, %1609, %1604
  %1642 = add nuw nsw i64 %1605, 1
  %1643 = icmp eq i64 %1642, %1603
  br i1 %1643, label %1644, label %1604

; <label>:1644:                                   ; preds = %1641
  %1645 = add nsw i64 %1603, -1
  %1646 = and i64 %1603, 3
  %1647 = icmp ult i64 %1645, 3
  br i1 %1647, label %1679, label %1648

; <label>:1648:                                   ; preds = %1644
  %1649 = sub nsw i64 %1603, %1646
  br label %1650

; <label>:1650:                                   ; preds = %1650, %1648
  %1651 = phi i64 [ 0, %1648 ], [ %1676, %1650 ]
  %1652 = phi i64 [ %1649, %1648 ], [ %1677, %1650 ]
  %1653 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1651, i32 3, i32 0
  %1654 = load i32, i32* %1653, align 4, !tbaa !24
  %1655 = sext i32 %1654 to i64
  %1656 = getelementptr inbounds i32, i32* %4, i64 %1655
  %1657 = trunc i64 %1651 to i32
  store i32 %1657, i32* %1656, align 4, !tbaa !7
  %1658 = or i64 %1651, 1
  %1659 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1658, i32 3, i32 0
  %1660 = load i32, i32* %1659, align 4, !tbaa !24
  %1661 = sext i32 %1660 to i64
  %1662 = getelementptr inbounds i32, i32* %4, i64 %1661
  %1663 = trunc i64 %1658 to i32
  store i32 %1663, i32* %1662, align 4, !tbaa !7
  %1664 = or i64 %1651, 2
  %1665 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1664, i32 3, i32 0
  %1666 = load i32, i32* %1665, align 4, !tbaa !24
  %1667 = sext i32 %1666 to i64
  %1668 = getelementptr inbounds i32, i32* %4, i64 %1667
  %1669 = trunc i64 %1664 to i32
  store i32 %1669, i32* %1668, align 4, !tbaa !7
  %1670 = or i64 %1651, 3
  %1671 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1670, i32 3, i32 0
  %1672 = load i32, i32* %1671, align 4, !tbaa !24
  %1673 = sext i32 %1672 to i64
  %1674 = getelementptr inbounds i32, i32* %4, i64 %1673
  %1675 = trunc i64 %1670 to i32
  store i32 %1675, i32* %1674, align 4, !tbaa !7
  %1676 = add nuw nsw i64 %1651, 4
  %1677 = add i64 %1652, -4
  %1678 = icmp eq i64 %1677, 0
  br i1 %1678, label %1679, label %1650

; <label>:1679:                                   ; preds = %1650, %1644
  %1680 = phi i64 [ 0, %1644 ], [ %1676, %1650 ]
  %1681 = icmp eq i64 %1646, 0
  br i1 %1681, label %1694, label %1682

; <label>:1682:                                   ; preds = %1679
  br label %1683

; <label>:1683:                                   ; preds = %1683, %1682
  %1684 = phi i64 [ %1691, %1683 ], [ %1680, %1682 ]
  %1685 = phi i64 [ %1692, %1683 ], [ %1646, %1682 ]
  %1686 = getelementptr inbounds %struct.Colamd_Col_struct, %struct.Colamd_Col_struct* %94, i64 %1684, i32 3, i32 0
  %1687 = load i32, i32* %1686, align 4, !tbaa !24
  %1688 = sext i32 %1687 to i64
  %1689 = getelementptr inbounds i32, i32* %4, i64 %1688
  %1690 = trunc i64 %1684 to i32
  store i32 %1690, i32* %1689, align 4, !tbaa !7
  %1691 = add nuw nsw i64 %1684, 1
  %1692 = add i64 %1685, -1
  %1693 = icmp eq i64 %1692, 0
  br i1 %1693, label %1694, label %1683, !llvm.loop !31

; <label>:1694:                                   ; preds = %1679, %1683, %1600
  %1695 = sub nsw i32 %0, %623
  store i32 %1695, i32* %6, align 4, !tbaa !7
  %1696 = sub nsw i32 %1, %720
  %1697 = getelementptr inbounds i32, i32* %6, i64 1
  store i32 %1696, i32* %1697, align 4, !tbaa !7
  %1698 = getelementptr inbounds i32, i32* %6, i64 2
  store i32 %1601, i32* %1698, align 4, !tbaa !7
  br label %1699

; <label>:1699:                                   ; preds = %181, %107, %7, %1694, %85, %37, %33, %27, %24, %21, %18
  %1700 = phi i32 [ 0, %24 ], [ 0, %27 ], [ 0, %33 ], [ 0, %37 ], [ 0, %85 ], [ 1, %1694 ], [ 0, %21 ], [ 0, %18 ], [ 0, %7 ], [ 0, %107 ], [ 0, %181 ]
  call void @llvm.lifetime.end.p0i8(i64 160, i8* nonnull %10) #5
  ret i32 %1700

; <label>:1701:                                   ; preds = %1788
  %1702 = shl nsw i64 %1793, 1
  %1703 = icmp uge i64 %1702, %1793
  %1704 = select i1 %1703, i64 %1702, i64 0
  %1705 = lshr i64 %1789, 2
  br i1 %1703, label %62, label %85

; <label>:1706:                                   ; preds = %56
  %1707 = shl nsw i64 %61, 1
  %1708 = icmp uge i64 %1707, %61
  %1709 = select i1 %1708, i64 %1707, i64 0
  br i1 %1708, label %1710, label %85

; <label>:1710:                                   ; preds = %1706
  %1711 = add nsw i64 %1709, %61
  %1712 = icmp ugt i64 %1709, %61
  %1713 = select i1 %1712, i64 %1709, i64 %61
  %1714 = icmp uge i64 %1711, %1713
  %1715 = select i1 %1714, i64 %1711, i64 0
  br i1 %1714, label %1716, label %85

; <label>:1716:                                   ; preds = %1710
  %1717 = add i64 %1715, %61
  %1718 = icmp ugt i64 %1715, %61
  %1719 = select i1 %1718, i64 %1715, i64 %61
  %1720 = icmp uge i64 %1717, %1719
  %1721 = select i1 %1720, i64 %1717, i64 0
  br i1 %1720, label %1722, label %85

; <label>:1722:                                   ; preds = %1716
  %1723 = add i64 %1721, %61
  %1724 = icmp ugt i64 %1721, %61
  %1725 = select i1 %1724, i64 %1721, i64 %61
  %1726 = icmp uge i64 %1723, %1725
  %1727 = select i1 %1726, i64 %1723, i64 0
  br i1 %1726, label %1728, label %85

; <label>:1728:                                   ; preds = %1722
  %1729 = add i64 %1727, %61
  %1730 = icmp ugt i64 %1727, %61
  %1731 = select i1 %1730, i64 %1727, i64 %61
  %1732 = icmp uge i64 %1729, %1731
  %1733 = select i1 %1732, i64 %1729, i64 0
  br i1 %1732, label %1734, label %85

; <label>:1734:                                   ; preds = %1728
  %1735 = add i64 %1733, %61
  %1736 = icmp ugt i64 %1733, %61
  %1737 = select i1 %1736, i64 %1733, i64 %61
  %1738 = icmp uge i64 %1735, %1737
  %1739 = select i1 %1738, i64 %1735, i64 0
  br i1 %1738, label %1740, label %85

; <label>:1740:                                   ; preds = %1734
  %1741 = add i64 %1739, %61
  %1742 = icmp ugt i64 %1739, %61
  %1743 = select i1 %1742, i64 %1739, i64 %61
  %1744 = icmp uge i64 %1741, %1743
  %1745 = select i1 %1744, i64 %1741, i64 0
  br i1 %1744, label %1746, label %85

; <label>:1746:                                   ; preds = %1740
  %1747 = add i64 %1745, %61
  %1748 = icmp ugt i64 %1745, %61
  %1749 = select i1 %1748, i64 %1745, i64 %61
  %1750 = icmp uge i64 %1747, %1749
  %1751 = select i1 %1750, i64 %1747, i64 0
  br i1 %1750, label %1752, label %85

; <label>:1752:                                   ; preds = %1746
  %1753 = add i64 %1751, %61
  %1754 = icmp ugt i64 %1751, %61
  %1755 = select i1 %1754, i64 %1751, i64 %61
  %1756 = icmp uge i64 %1753, %1755
  %1757 = select i1 %1756, i64 %1753, i64 0
  br i1 %1756, label %1758, label %85

; <label>:1758:                                   ; preds = %1752
  %1759 = add i64 %1757, %61
  %1760 = icmp ugt i64 %1757, %61
  %1761 = select i1 %1760, i64 %1757, i64 %61
  %1762 = icmp uge i64 %1759, %1761
  %1763 = select i1 %1762, i64 %1759, i64 0
  br i1 %1762, label %1764, label %85

; <label>:1764:                                   ; preds = %1758
  %1765 = add i64 %1763, %61
  %1766 = icmp ugt i64 %1763, %61
  %1767 = select i1 %1766, i64 %1763, i64 %61
  %1768 = icmp uge i64 %1765, %1767
  %1769 = select i1 %1768, i64 %1765, i64 0
  br i1 %1768, label %1770, label %85

; <label>:1770:                                   ; preds = %1764
  %1771 = add i64 %1769, %61
  %1772 = icmp ugt i64 %1769, %61
  %1773 = select i1 %1772, i64 %1769, i64 %61
  %1774 = icmp uge i64 %1771, %1773
  %1775 = select i1 %1774, i64 %1771, i64 0
  br i1 %1774, label %1776, label %85

; <label>:1776:                                   ; preds = %1770
  %1777 = add i64 %1775, %61
  %1778 = icmp ugt i64 %1775, %61
  %1779 = select i1 %1778, i64 %1775, i64 %61
  %1780 = icmp uge i64 %1777, %1779
  %1781 = select i1 %1780, i64 %1777, i64 0
  br i1 %1780, label %1782, label %85

; <label>:1782:                                   ; preds = %1776
  %1783 = add i64 %1781, %61
  %1784 = icmp ugt i64 %1781, %61
  %1785 = select i1 %1784, i64 %1781, i64 %61
  %1786 = icmp uge i64 %1783, %1785
  %1787 = select i1 %1786, i64 %1783, i64 0
  br i1 %1786, label %1788, label %85

; <label>:1788:                                   ; preds = %1782
  %1789 = add i64 %1787, %61
  %1790 = icmp ugt i64 %1787, %61
  %1791 = select i1 %1790, i64 %1787, i64 %61
  %1792 = icmp ult i64 %1789, %1791
  %1793 = sext i32 %31 to i64
  br i1 %1792, label %85, label %1701

; <label>:1794:                                   ; preds = %47
  %1795 = shl nsw i64 %55, 1
  %1796 = icmp uge i64 %1795, %55
  %1797 = select i1 %1796, i64 %1795, i64 0
  br i1 %1796, label %1798, label %85

; <label>:1798:                                   ; preds = %1794
  %1799 = add nsw i64 %1797, %55
  %1800 = icmp ugt i64 %1797, %55
  %1801 = select i1 %1800, i64 %1797, i64 %55
  %1802 = icmp uge i64 %1799, %1801
  %1803 = select i1 %1802, i64 %1799, i64 0
  br i1 %1802, label %1804, label %85

; <label>:1804:                                   ; preds = %1798
  %1805 = add i64 %1803, %55
  %1806 = icmp ugt i64 %1803, %55
  %1807 = select i1 %1806, i64 %1803, i64 %55
  %1808 = icmp uge i64 %1805, %1807
  %1809 = select i1 %1808, i64 %1805, i64 0
  br i1 %1808, label %1810, label %85

; <label>:1810:                                   ; preds = %1804
  %1811 = add i64 %1809, %55
  %1812 = icmp ugt i64 %1809, %55
  %1813 = select i1 %1812, i64 %1809, i64 %55
  %1814 = icmp uge i64 %1811, %1813
  %1815 = select i1 %1814, i64 %1811, i64 0
  br i1 %1814, label %1816, label %85

; <label>:1816:                                   ; preds = %1810
  %1817 = add i64 %1815, %55
  %1818 = icmp ugt i64 %1815, %55
  %1819 = select i1 %1818, i64 %1815, i64 %55
  %1820 = icmp uge i64 %1817, %1819
  %1821 = select i1 %1820, i64 %1817, i64 0
  br i1 %1820, label %1822, label %85

; <label>:1822:                                   ; preds = %1816
  %1823 = add i64 %1821, %55
  %1824 = icmp ugt i64 %1821, %55
  %1825 = select i1 %1824, i64 %1821, i64 %55
  %1826 = icmp uge i64 %1823, %1825
  %1827 = select i1 %1826, i64 %1823, i64 0
  br i1 %1826, label %1828, label %85

; <label>:1828:                                   ; preds = %1822
  %1829 = add i64 %1827, %55
  %1830 = icmp ugt i64 %1827, %55
  %1831 = select i1 %1830, i64 %1827, i64 %55
  %1832 = icmp uge i64 %1829, %1831
  %1833 = select i1 %1832, i64 %1829, i64 0
  br i1 %1832, label %1834, label %85

; <label>:1834:                                   ; preds = %1828
  %1835 = add i64 %1833, %55
  %1836 = icmp ugt i64 %1833, %55
  %1837 = select i1 %1836, i64 %1833, i64 %55
  %1838 = icmp uge i64 %1835, %1837
  %1839 = select i1 %1838, i64 %1835, i64 0
  br i1 %1838, label %1840, label %85

; <label>:1840:                                   ; preds = %1834
  %1841 = add i64 %1839, %55
  %1842 = icmp ugt i64 %1839, %55
  %1843 = select i1 %1842, i64 %1839, i64 %55
  %1844 = icmp uge i64 %1841, %1843
  %1845 = select i1 %1844, i64 %1841, i64 0
  br i1 %1844, label %1846, label %85

; <label>:1846:                                   ; preds = %1840
  %1847 = add i64 %1845, %55
  %1848 = icmp ugt i64 %1845, %55
  %1849 = select i1 %1848, i64 %1845, i64 %55
  %1850 = icmp uge i64 %1847, %1849
  %1851 = select i1 %1850, i64 %1847, i64 0
  br i1 %1850, label %1852, label %85

; <label>:1852:                                   ; preds = %1846
  %1853 = add i64 %1851, %55
  %1854 = icmp ugt i64 %1851, %55
  %1855 = select i1 %1854, i64 %1851, i64 %55
  %1856 = icmp uge i64 %1853, %1855
  %1857 = select i1 %1856, i64 %1853, i64 0
  br i1 %1856, label %1858, label %85

; <label>:1858:                                   ; preds = %1852
  %1859 = add i64 %1857, %55
  %1860 = icmp ugt i64 %1857, %55
  %1861 = select i1 %1860, i64 %1857, i64 %55
  %1862 = icmp uge i64 %1859, %1861
  %1863 = select i1 %1862, i64 %1859, i64 0
  br i1 %1862, label %1864, label %85

; <label>:1864:                                   ; preds = %1858
  %1865 = add i64 %1863, %55
  %1866 = icmp ugt i64 %1863, %55
  %1867 = select i1 %1866, i64 %1863, i64 %55
  %1868 = icmp uge i64 %1865, %1867
  %1869 = select i1 %1868, i64 %1865, i64 0
  br i1 %1868, label %1870, label %85

; <label>:1870:                                   ; preds = %1864
  %1871 = add i64 %1869, %55
  %1872 = icmp ugt i64 %1869, %55
  %1873 = select i1 %1872, i64 %1869, i64 %55
  %1874 = icmp uge i64 %1871, %1873
  %1875 = select i1 %1874, i64 %1871, i64 0
  br i1 %1874, label %1876, label %85

; <label>:1876:                                   ; preds = %1870
  %1877 = add i64 %1875, %55
  %1878 = icmp ugt i64 %1875, %55
  %1879 = select i1 %1878, i64 %1875, i64 %55
  %1880 = icmp uge i64 %1877, %1879
  %1881 = select i1 %1880, i64 %1877, i64 0
  br i1 %1880, label %1882, label %85

; <label>:1882:                                   ; preds = %1876
  %1883 = add i64 %1881, %55
  %1884 = icmp ugt i64 %1881, %55
  %1885 = select i1 %1884, i64 %1881, i64 %55
  %1886 = icmp uge i64 %1883, %1885
  %1887 = select i1 %1886, i64 %1883, i64 0
  br i1 %1886, label %1888, label %85

; <label>:1888:                                   ; preds = %1882
  %1889 = add i64 %1887, %55
  %1890 = icmp ugt i64 %1887, %55
  %1891 = select i1 %1890, i64 %1887, i64 %55
  %1892 = icmp uge i64 %1889, %1891
  %1893 = select i1 %1892, i64 %1889, i64 0
  br i1 %1892, label %1894, label %85

; <label>:1894:                                   ; preds = %1888
  %1895 = add i64 %1893, %55
  %1896 = icmp ugt i64 %1893, %55
  %1897 = select i1 %1896, i64 %1893, i64 %55
  %1898 = icmp uge i64 %1895, %1897
  %1899 = select i1 %1898, i64 %1895, i64 0
  br i1 %1898, label %1900, label %85

; <label>:1900:                                   ; preds = %1894
  %1901 = add i64 %1899, %55
  %1902 = icmp ugt i64 %1899, %55
  %1903 = select i1 %1902, i64 %1899, i64 %55
  %1904 = icmp uge i64 %1901, %1903
  %1905 = select i1 %1904, i64 %1901, i64 0
  br i1 %1904, label %1906, label %85

; <label>:1906:                                   ; preds = %1900
  %1907 = add i64 %1905, %55
  %1908 = icmp ugt i64 %1905, %55
  %1909 = select i1 %1908, i64 %1905, i64 %55
  %1910 = icmp uge i64 %1907, %1909
  %1911 = select i1 %1910, i64 %1907, i64 0
  br i1 %1910, label %1912, label %85

; <label>:1912:                                   ; preds = %1906
  %1913 = add i64 %1911, %55
  %1914 = icmp ugt i64 %1911, %55
  %1915 = select i1 %1914, i64 %1911, i64 %55
  %1916 = icmp uge i64 %1913, %1915
  %1917 = select i1 %1916, i64 %1913, i64 0
  br i1 %1916, label %1918, label %85

; <label>:1918:                                   ; preds = %1912
  %1919 = add i64 %1917, %55
  %1920 = icmp ugt i64 %1917, %55
  %1921 = select i1 %1920, i64 %1917, i64 %55
  %1922 = icmp uge i64 %1919, %1921
  %1923 = select i1 %1922, i64 %1919, i64 0
  br i1 %1922, label %1924, label %85

; <label>:1924:                                   ; preds = %1918
  %1925 = add i64 %1923, %55
  %1926 = icmp ugt i64 %1923, %55
  %1927 = select i1 %1926, i64 %1923, i64 %55
  %1928 = icmp uge i64 %1925, %1927
  %1929 = fcmp oeq double %50, 0.000000e+00
  %1930 = lshr i64 %1925, 2
  %1931 = select i1 %1928, i64 %1930, i64 0
  %1932 = sext i32 %0 to i64
  br i1 %1928, label %56, label %85

; <label>:1933:                                   ; preds = %841
  %1934 = getelementptr inbounds i32, i32* %842, i64 1
  store i32 %844, i32* %842, align 4, !tbaa !7
  br label %1935

; <label>:1935:                                   ; preds = %1933, %841
  %1936 = phi i32* [ %1934, %1933 ], [ %842, %841 ]
  %1937 = add i32 %832, -2
  %1938 = icmp eq i32 %1937, 0
  br i1 %1938, label %849, label %829

; <label>:1939:                                   ; preds = %944
  %1940 = getelementptr inbounds i32, i32* %945, i64 1
  store i32 %947, i32* %945, align 4, !tbaa !7
  br label %1941

; <label>:1941:                                   ; preds = %1939, %944
  %1942 = phi i32* [ %1940, %1939 ], [ %945, %944 ]
  %1943 = add i32 %935, -2
  %1944 = icmp eq i32 %1943, 0
  br i1 %1944, label %952, label %932

; <label>:1945:                                   ; preds = %1003
  store i32 0, i32* %1005, align 4, !tbaa !24
  br label %1946

; <label>:1946:                                   ; preds = %1945, %1003
  %1947 = add nuw nsw i64 %997, 2
  %1948 = add i64 %998, -2
  %1949 = icmp eq i64 %1948, 0
  br i1 %1949, label %1008, label %996

; <label>:1950:                                   ; preds = %1521
  store i32 0, i32* %1523, align 4, !tbaa !24
  br label %1951

; <label>:1951:                                   ; preds = %1950, %1521
  %1952 = add nuw nsw i64 %1515, 2
  %1953 = add i64 %1516, -2
  %1954 = icmp eq i64 %1953, 0
  br i1 %1954, label %1526, label %1514

; <label>:1955:                                   ; preds = %736
  store i32 0, i32* %738, align 4, !tbaa !24
  br label %1956

; <label>:1956:                                   ; preds = %1955, %736
  %1957 = add nuw nsw i64 %730, 2
  %1958 = add i64 %731, -2
  %1959 = icmp eq i64 %1958, 0
  br i1 %1959, label %741, label %729
}

; Function Attrs: nounwind ssp uwtable
define void @colamd_report(i32* readonly) local_unnamed_addr #3 {
  tail call fastcc void @print_report(i8* getelementptr inbounds ([7 x i8], [7 x i8]* @.str, i64 0, i64 0), i32* %0)
  ret void
}

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @print_report(i8*, i32* readonly) unnamed_addr #3 {
  %3 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %4 = icmp eq i32 (i8*, ...)* %3, null
  br i1 %4, label %7, label %5

; <label>:5:                                      ; preds = %2
  %6 = tail call i32 (i8*, ...) %3(i8* getelementptr inbounds ([24 x i8], [24 x i8]* @.str.2, i64 0, i64 0), i8* %0, i32 2, i32 9, i8* getelementptr inbounds ([12 x i8], [12 x i8]* @.str.3, i64 0, i64 0)) #5
  br label %7

; <label>:7:                                      ; preds = %2, %5
  %8 = icmp eq i32* %1, null
  br i1 %8, label %9, label %14

; <label>:9:                                      ; preds = %7
  %10 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %11 = icmp eq i32 (i8*, ...)* %10, null
  br i1 %11, label %130, label %12

; <label>:12:                                     ; preds = %9
  %13 = tail call i32 (i8*, ...) %10(i8* getelementptr inbounds ([26 x i8], [26 x i8]* @.str.4, i64 0, i64 0)) #5
  br label %130

; <label>:14:                                     ; preds = %7
  %15 = getelementptr inbounds i32, i32* %1, i64 4
  %16 = load i32, i32* %15, align 4, !tbaa !7
  %17 = getelementptr inbounds i32, i32* %1, i64 5
  %18 = load i32, i32* %17, align 4, !tbaa !7
  %19 = getelementptr inbounds i32, i32* %1, i64 6
  %20 = load i32, i32* %19, align 4, !tbaa !7
  %21 = getelementptr inbounds i32, i32* %1, i64 3
  %22 = load i32, i32* %21, align 4, !tbaa !7
  %23 = icmp sgt i32 %22, -1
  %24 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %25 = icmp ne i32 (i8*, ...)* %24, null
  br i1 %23, label %26, label %29

; <label>:26:                                     ; preds = %14
  br i1 %25, label %27, label %34

; <label>:27:                                     ; preds = %26
  %28 = tail call i32 (i8*, ...) %24(i8* getelementptr inbounds ([6 x i8], [6 x i8]* @.str.5, i64 0, i64 0)) #5
  br label %32

; <label>:29:                                     ; preds = %14
  br i1 %25, label %30, label %34

; <label>:30:                                     ; preds = %29
  %31 = tail call i32 (i8*, ...) %24(i8* getelementptr inbounds ([9 x i8], [9 x i8]* @.str.6, i64 0, i64 0)) #5
  br label %32

; <label>:32:                                     ; preds = %27, %30
  %33 = load i32, i32* %21, align 4, !tbaa !7
  br label %34

; <label>:34:                                     ; preds = %32, %29, %26
  %35 = phi i32 [ %33, %32 ], [ %22, %29 ], [ %22, %26 ]
  switch i32 %35, label %130 [
    i32 1, label %36
    i32 0, label %53
    i32 -1, label %75
    i32 -2, label %80
    i32 -3, label %85
    i32 -4, label %90
    i32 -5, label %95
    i32 -6, label %100
    i32 -7, label %105
    i32 -8, label %114
    i32 -9, label %119
    i32 -10, label %125
  ]

; <label>:36:                                     ; preds = %34
  %37 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %38 = icmp eq i32 (i8*, ...)* %37, null
  br i1 %38, label %130, label %39

; <label>:39:                                     ; preds = %36
  %40 = tail call i32 (i8*, ...) %37(i8* getelementptr inbounds ([47 x i8], [47 x i8]* @.str.7, i64 0, i64 0)) #5
  %41 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %42 = icmp eq i32 (i8*, ...)* %41, null
  br i1 %42, label %130, label %43

; <label>:43:                                     ; preds = %39
  %44 = tail call i32 (i8*, ...) %41(i8* getelementptr inbounds ([57 x i8], [57 x i8]* @.str.8, i64 0, i64 0), i8* %0, i32 %20) #5
  %45 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %46 = icmp eq i32 (i8*, ...)* %45, null
  br i1 %46, label %130, label %47

; <label>:47:                                     ; preds = %43
  %48 = tail call i32 (i8*, ...) %45(i8* getelementptr inbounds ([57 x i8], [57 x i8]* @.str.9, i64 0, i64 0), i8* %0, i32 %18) #5
  %49 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %50 = icmp eq i32 (i8*, ...)* %49, null
  br i1 %50, label %130, label %51

; <label>:51:                                     ; preds = %47
  %52 = tail call i32 (i8*, ...) %49(i8* getelementptr inbounds ([56 x i8], [56 x i8]* @.str.10, i64 0, i64 0), i8* %0, i32 %16) #5
  br label %53

; <label>:53:                                     ; preds = %51, %34
  %54 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %55 = icmp eq i32 (i8*, ...)* %54, null
  br i1 %55, label %130, label %56

; <label>:56:                                     ; preds = %53
  %57 = tail call i32 (i8*, ...) %54(i8* getelementptr inbounds ([2 x i8], [2 x i8]* @.str.11, i64 0, i64 0)) #5
  %58 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %59 = icmp eq i32 (i8*, ...)* %58, null
  br i1 %59, label %130, label %60

; <label>:60:                                     ; preds = %56
  %61 = load i32, i32* %1, align 4, !tbaa !7
  %62 = tail call i32 (i8*, ...) %58(i8* getelementptr inbounds ([57 x i8], [57 x i8]* @.str.12, i64 0, i64 0), i8* %0, i32 %61) #5
  %63 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %64 = icmp eq i32 (i8*, ...)* %63, null
  br i1 %64, label %130, label %65

; <label>:65:                                     ; preds = %60
  %66 = getelementptr inbounds i32, i32* %1, i64 1
  %67 = load i32, i32* %66, align 4, !tbaa !7
  %68 = tail call i32 (i8*, ...) %63(i8* getelementptr inbounds ([57 x i8], [57 x i8]* @.str.13, i64 0, i64 0), i8* %0, i32 %67) #5
  %69 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %70 = icmp eq i32 (i8*, ...)* %69, null
  br i1 %70, label %130, label %71

; <label>:71:                                     ; preds = %65
  %72 = getelementptr inbounds i32, i32* %1, i64 2
  %73 = load i32, i32* %72, align 4, !tbaa !7
  %74 = tail call i32 (i8*, ...) %69(i8* getelementptr inbounds ([57 x i8], [57 x i8]* @.str.14, i64 0, i64 0), i8* %0, i32 %73) #5
  br label %130

; <label>:75:                                     ; preds = %34
  %76 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %77 = icmp eq i32 (i8*, ...)* %76, null
  br i1 %77, label %130, label %78

; <label>:78:                                     ; preds = %75
  %79 = tail call i32 (i8*, ...) %76(i8* getelementptr inbounds ([46 x i8], [46 x i8]* @.str.15, i64 0, i64 0)) #5
  br label %130

; <label>:80:                                     ; preds = %34
  %81 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %82 = icmp eq i32 (i8*, ...)* %81, null
  br i1 %82, label %130, label %83

; <label>:83:                                     ; preds = %80
  %84 = tail call i32 (i8*, ...) %81(i8* getelementptr inbounds ([51 x i8], [51 x i8]* @.str.16, i64 0, i64 0)) #5
  br label %130

; <label>:85:                                     ; preds = %34
  %86 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %87 = icmp eq i32 (i8*, ...)* %86, null
  br i1 %87, label %130, label %88

; <label>:88:                                     ; preds = %85
  %89 = tail call i32 (i8*, ...) %86(i8* getelementptr inbounds ([30 x i8], [30 x i8]* @.str.17, i64 0, i64 0), i32 %16) #5
  br label %130

; <label>:90:                                     ; preds = %34
  %91 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %92 = icmp eq i32 (i8*, ...)* %91, null
  br i1 %92, label %130, label %93

; <label>:93:                                     ; preds = %90
  %94 = tail call i32 (i8*, ...) %91(i8* getelementptr inbounds ([33 x i8], [33 x i8]* @.str.18, i64 0, i64 0), i32 %16) #5
  br label %130

; <label>:95:                                     ; preds = %34
  %96 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %97 = icmp eq i32 (i8*, ...)* %96, null
  br i1 %97, label %130, label %98

; <label>:98:                                     ; preds = %95
  %99 = tail call i32 (i8*, ...) %96(i8* getelementptr inbounds ([41 x i8], [41 x i8]* @.str.19, i64 0, i64 0), i32 %16) #5
  br label %130

; <label>:100:                                    ; preds = %34
  %101 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %102 = icmp eq i32 (i8*, ...)* %101, null
  br i1 %102, label %130, label %103

; <label>:103:                                    ; preds = %100
  %104 = tail call i32 (i8*, ...) %101(i8* getelementptr inbounds ([51 x i8], [51 x i8]* @.str.20, i64 0, i64 0), i32 %16) #5
  br label %130

; <label>:105:                                    ; preds = %34
  %106 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %107 = icmp eq i32 (i8*, ...)* %106, null
  br i1 %107, label %130, label %108

; <label>:108:                                    ; preds = %105
  %109 = tail call i32 (i8*, ...) %106(i8* getelementptr inbounds ([20 x i8], [20 x i8]* @.str.21, i64 0, i64 0)) #5
  %110 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %111 = icmp eq i32 (i8*, ...)* %110, null
  br i1 %111, label %130, label %112

; <label>:112:                                    ; preds = %108
  %113 = tail call i32 (i8*, ...) %110(i8* getelementptr inbounds ([52 x i8], [52 x i8]* @.str.22, i64 0, i64 0), i32 %16, i32 %18) #5
  br label %130

; <label>:114:                                    ; preds = %34
  %115 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %116 = icmp eq i32 (i8*, ...)* %115, null
  br i1 %116, label %130, label %117

; <label>:117:                                    ; preds = %114
  %118 = tail call i32 (i8*, ...) %115(i8* getelementptr inbounds ([58 x i8], [58 x i8]* @.str.23, i64 0, i64 0), i32 %16, i32 %18) #5
  br label %130

; <label>:119:                                    ; preds = %34
  %120 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %121 = icmp eq i32 (i8*, ...)* %120, null
  br i1 %121, label %130, label %122

; <label>:122:                                    ; preds = %119
  %123 = add nsw i32 %20, -1
  %124 = tail call i32 (i8*, ...) %120(i8* getelementptr inbounds ([59 x i8], [59 x i8]* @.str.24, i64 0, i64 0), i32 %18, i32 0, i32 %123, i32 %16) #5
  br label %130

; <label>:125:                                    ; preds = %34
  %126 = load i32 (i8*, ...)*, i32 (i8*, ...)** getelementptr inbounds (%struct.SuiteSparse_config_struct, %struct.SuiteSparse_config_struct* @SuiteSparse_config, i64 0, i32 4), align 8, !tbaa !32
  %127 = icmp eq i32 (i8*, ...)* %126, null
  br i1 %127, label %130, label %128

; <label>:128:                                    ; preds = %125
  %129 = tail call i32 (i8*, ...) %126(i8* getelementptr inbounds ([16 x i8], [16 x i8]* @.str.25, i64 0, i64 0)) #5
  br label %130

; <label>:130:                                    ; preds = %43, %47, %36, %39, %56, %53, %105, %60, %34, %71, %78, %83, %88, %93, %98, %103, %112, %117, %122, %128, %125, %119, %114, %108, %100, %95, %90, %85, %80, %75, %65, %12, %9
  ret void
}

; Function Attrs: nounwind ssp uwtable
define void @symamd_report(i32* readonly) local_unnamed_addr #3 {
  tail call fastcc void @print_report(i8* getelementptr inbounds ([7 x i8], [7 x i8]* @.str.1, i64 0, i64 0), i32* %0)
  ret void
}

; Function Attrs: nounwind readnone speculatable
declare double @llvm.sqrt.f64(double) #4

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memcpy.p0i8.p0i8.i64(i8* nocapture writeonly, i8* nocapture readonly, i64, i32, i1) #1

attributes #0 = { nounwind readnone ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { norecurse nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #4 = { nounwind readnone speculatable }
attributes #5 = { nounwind }

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
!9 = distinct !{!9, !10}
!10 = !{!"llvm.loop.unroll.disable"}
!11 = !{!12}
!12 = distinct !{!12, !13}
!13 = distinct !{!13, !"LVerDomain"}
!14 = !{!15}
!15 = distinct !{!15, !13}
!16 = distinct !{!16, !17}
!17 = !{!"llvm.loop.isvectorized", i32 1}
!18 = distinct !{!18, !10}
!19 = distinct !{!19, !10}
!20 = distinct !{!20, !17}
!21 = !{!22, !8, i64 0}
!22 = !{!"Colamd_Col_struct", !8, i64 0, !8, i64 4, !5, i64 8, !5, i64 12, !5, i64 16, !5, i64 20}
!23 = !{!22, !8, i64 4}
!24 = !{!5, !5, i64 0}
!25 = !{!26, !8, i64 4}
!26 = !{!"Colamd_Row_struct", !8, i64 0, !8, i64 4, !5, i64 8, !5, i64 12}
!27 = !{!26, !8, i64 0}
!28 = distinct !{!28, !10}
!29 = distinct !{!29, !10}
!30 = distinct !{!30, !10}
!31 = distinct !{!31, !10}
!32 = !{!33, !34, i64 32}
!33 = !{!"SuiteSparse_config_struct", !34, i64 0, !34, i64 8, !34, i64 16, !34, i64 24, !34, i64 32, !34, i64 40, !34, i64 48}
!34 = !{!"any pointer", !5, i64 0}
