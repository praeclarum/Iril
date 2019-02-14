; ModuleID = 'klu_memory.c'
source_filename = "klu_memory.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i64 @klu_add_size_t(i64, i64, i32* nocapture) local_unnamed_addr #0 !dbg !13 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !19, metadata !DIExpression()), !dbg !22
  call void @llvm.dbg.value(metadata i64 %1, metadata !20, metadata !DIExpression()), !dbg !23
  call void @llvm.dbg.value(metadata i32* %2, metadata !21, metadata !DIExpression()), !dbg !24
  %4 = load i32, i32* %2, align 4, !dbg !25, !tbaa !26
  %5 = icmp eq i32 %4, 0, !dbg !30
  br i1 %5, label %6, label %7, !dbg !31

; <label>:6:                                      ; preds = %3
  store i32 0, i32* %2, align 4, !dbg !32, !tbaa !26
  ret i64 -1, !dbg !33

; <label>:7:                                      ; preds = %3
  %8 = add i64 %1, %0, !dbg !34
  %9 = icmp ugt i64 %0, %1, !dbg !35
  %10 = select i1 %9, i64 %0, i64 %1, !dbg !35
  %11 = icmp uge i64 %8, %10, !dbg !36
  %12 = zext i1 %11 to i32, !dbg !31
  store i32 %12, i32* %2, align 4, !dbg !32, !tbaa !26
  %13 = select i1 %11, i64 %8, i64 -1, !dbg !37
  ret i64 %13, !dbg !37
}

; Function Attrs: nounwind ssp uwtable
define i64 @klu_mult_size_t(i64, i64, i32* nocapture) local_unnamed_addr #0 !dbg !38 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !40, metadata !DIExpression()), !dbg !45
  call void @llvm.dbg.value(metadata i64 %1, metadata !41, metadata !DIExpression()), !dbg !46
  call void @llvm.dbg.value(metadata i32* %2, metadata !42, metadata !DIExpression()), !dbg !47
  call void @llvm.dbg.value(metadata i64 0, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 0, metadata !43, metadata !DIExpression()), !dbg !49
  %4 = icmp eq i64 %1, 0, !dbg !50
  %5 = load i32, i32* %2, align 4, !dbg !53, !tbaa !26
  %6 = icmp eq i32 %5, 0, !dbg !53
  br i1 %4, label %47, label %7, !dbg !54

; <label>:7:                                      ; preds = %3
  %8 = and i64 %1, 1, !dbg !55
  %9 = icmp eq i64 %1, 1, !dbg !55
  br i1 %9, label %28, label %10, !dbg !55

; <label>:10:                                     ; preds = %7
  %11 = sub i64 %1, %8, !dbg !55
  br label %12, !dbg !55

; <label>:12:                                     ; preds = %59, %10
  %13 = phi i1 [ %6, %10 ], [ %62, %59 ]
  %14 = phi i64 [ 0, %10 ], [ %61, %59 ]
  %15 = phi i64 [ %11, %10 ], [ %63, %59 ]
  call void @llvm.dbg.value(metadata i64 %14, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 undef, metadata !43, metadata !DIExpression()), !dbg !49
  call void @llvm.dbg.value(metadata i64 %14, metadata !19, metadata !DIExpression()), !dbg !58
  call void @llvm.dbg.value(metadata i64 %0, metadata !20, metadata !DIExpression()), !dbg !59
  call void @llvm.dbg.value(metadata i32* %2, metadata !21, metadata !DIExpression()), !dbg !60
  br i1 %13, label %16, label %17, !dbg !55

; <label>:16:                                     ; preds = %12
  store i32 0, i32* %2, align 4, !dbg !61, !tbaa !26
  br label %24, !dbg !62

; <label>:17:                                     ; preds = %12
  %18 = add i64 %14, %0, !dbg !63
  %19 = icmp ugt i64 %14, %0, !dbg !64
  %20 = select i1 %19, i64 %14, i64 %0, !dbg !64
  %21 = icmp uge i64 %18, %20, !dbg !65
  %22 = zext i1 %21 to i32, !dbg !55
  store i32 %22, i32* %2, align 4, !dbg !61, !tbaa !26
  %23 = select i1 %21, i64 %18, i64 -1, !dbg !66
  br label %24, !dbg !66

; <label>:24:                                     ; preds = %17, %16
  %25 = phi i32 [ 0, %16 ], [ %22, %17 ], !dbg !53
  %26 = phi i64 [ -1, %16 ], [ %23, %17 ]
  call void @llvm.dbg.value(metadata i64 %26, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 undef, metadata !43, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !49
  %27 = icmp eq i32 %25, 0, !dbg !53
  call void @llvm.dbg.value(metadata i64 %26, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 undef, metadata !43, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !49
  call void @llvm.dbg.value(metadata i64 %26, metadata !19, metadata !DIExpression()), !dbg !58
  call void @llvm.dbg.value(metadata i64 %0, metadata !20, metadata !DIExpression()), !dbg !59
  call void @llvm.dbg.value(metadata i32* %2, metadata !21, metadata !DIExpression()), !dbg !60
  br i1 %27, label %58, label %51, !dbg !55

; <label>:28:                                     ; preds = %59, %7
  %29 = phi i64 [ undef, %7 ], [ %61, %59 ]
  %30 = phi i1 [ undef, %7 ], [ %62, %59 ]
  %31 = phi i1 [ %6, %7 ], [ %62, %59 ]
  %32 = phi i64 [ 0, %7 ], [ %61, %59 ]
  %33 = icmp eq i64 %8, 0, !dbg !55
  br i1 %33, label %47, label %34, !dbg !55

; <label>:34:                                     ; preds = %28
  call void @llvm.dbg.value(metadata i64 %32, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 %32, metadata !19, metadata !DIExpression()), !dbg !58
  call void @llvm.dbg.value(metadata i64 %0, metadata !20, metadata !DIExpression()), !dbg !59
  call void @llvm.dbg.value(metadata i32* %2, metadata !21, metadata !DIExpression()), !dbg !60
  br i1 %31, label %42, label %35, !dbg !55

; <label>:35:                                     ; preds = %34
  %36 = add i64 %32, %0, !dbg !63
  %37 = icmp ugt i64 %32, %0, !dbg !64
  %38 = select i1 %37, i64 %32, i64 %0, !dbg !64
  %39 = icmp uge i64 %36, %38, !dbg !65
  %40 = zext i1 %39 to i32, !dbg !55
  store i32 %40, i32* %2, align 4, !dbg !61, !tbaa !26
  %41 = select i1 %39, i64 %36, i64 -1, !dbg !66
  br label %43, !dbg !66

; <label>:42:                                     ; preds = %34
  store i32 0, i32* %2, align 4, !dbg !61, !tbaa !26
  br label %43, !dbg !62

; <label>:43:                                     ; preds = %35, %42
  %44 = phi i32 [ 0, %42 ], [ %40, %35 ], !dbg !53
  %45 = phi i64 [ -1, %42 ], [ %41, %35 ]
  call void @llvm.dbg.value(metadata i64 %45, metadata !44, metadata !DIExpression()), !dbg !48
  %46 = icmp eq i32 %44, 0, !dbg !53
  br label %47, !dbg !67

; <label>:47:                                     ; preds = %43, %28, %3
  %48 = phi i64 [ 0, %3 ], [ %29, %28 ], [ %45, %43 ]
  %49 = phi i1 [ %6, %3 ], [ %30, %28 ], [ %46, %43 ]
  call void @llvm.dbg.value(metadata i64 %48, metadata !44, metadata !DIExpression()), !dbg !48
  %50 = select i1 %49, i64 -1, i64 %48, !dbg !67
  ret i64 %50, !dbg !68

; <label>:51:                                     ; preds = %24
  %52 = add i64 %26, %0, !dbg !63
  %53 = icmp ugt i64 %26, %0, !dbg !64
  %54 = select i1 %53, i64 %26, i64 %0, !dbg !64
  %55 = icmp uge i64 %52, %54, !dbg !65
  %56 = zext i1 %55 to i32, !dbg !55
  store i32 %56, i32* %2, align 4, !dbg !61, !tbaa !26
  %57 = select i1 %55, i64 %52, i64 -1, !dbg !66
  br label %59, !dbg !66

; <label>:58:                                     ; preds = %24
  store i32 0, i32* %2, align 4, !dbg !61, !tbaa !26
  br label %59, !dbg !62

; <label>:59:                                     ; preds = %58, %51
  %60 = phi i32 [ 0, %58 ], [ %56, %51 ], !dbg !53
  %61 = phi i64 [ -1, %58 ], [ %57, %51 ]
  call void @llvm.dbg.value(metadata i64 %61, metadata !44, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 undef, metadata !43, metadata !DIExpression(DW_OP_plus_uconst, 2, DW_OP_stack_value)), !dbg !49
  %62 = icmp eq i32 %60, 0, !dbg !53
  %63 = add i64 %15, -2, !dbg !54
  %64 = icmp eq i64 %63, 0, !dbg !54
  br i1 %64, label %28, label %12, !dbg !54, !llvm.loop !69
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !71 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !109, metadata !DIExpression()), !dbg !113
  call void @llvm.dbg.value(metadata i64 %1, metadata !110, metadata !DIExpression()), !dbg !114
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %2, metadata !111, metadata !DIExpression()), !dbg !115
  %4 = icmp eq %struct.klu_common_struct* %2, null, !dbg !116
  br i1 %4, label %29, label %5, !dbg !118

; <label>:5:                                      ; preds = %3
  %6 = icmp eq i64 %1, 0, !dbg !119
  br i1 %6, label %7, label %9, !dbg !121

; <label>:7:                                      ; preds = %5
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !122
  store i32 -3, i32* %8, align 4, !dbg !124, !tbaa !125
  call void @llvm.dbg.value(metadata i8* null, metadata !112, metadata !DIExpression()), !dbg !130
  br label %29, !dbg !131

; <label>:9:                                      ; preds = %5
  %10 = icmp ugt i64 %0, 2147483646, !dbg !132
  br i1 %10, label %11, label %13, !dbg !134

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !135
  store i32 -4, i32* %12, align 4, !dbg !137, !tbaa !125
  call void @llvm.dbg.value(metadata i8* null, metadata !112, metadata !DIExpression()), !dbg !130
  br label %29, !dbg !138

; <label>:13:                                     ; preds = %9
  %14 = tail call i8* @SuiteSparse_malloc(i64 %0, i64 %1) #4, !dbg !139
  call void @llvm.dbg.value(metadata i8* %14, metadata !112, metadata !DIExpression()), !dbg !130
  %15 = icmp eq i8* %14, null, !dbg !141
  br i1 %15, label %16, label %18, !dbg !143

; <label>:16:                                     ; preds = %13
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !144
  store i32 -2, i32* %17, align 4, !dbg !146, !tbaa !125
  br label %29, !dbg !147

; <label>:18:                                     ; preds = %13
  %19 = icmp eq i64 %0, 0, !dbg !148
  %20 = select i1 %19, i64 1, i64 %0, !dbg !148
  %21 = mul i64 %20, %1, !dbg !150
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 22, !dbg !151
  %23 = load i64, i64* %22, align 8, !dbg !152, !tbaa !153
  %24 = add i64 %23, %21, !dbg !152
  store i64 %24, i64* %22, align 8, !dbg !152, !tbaa !153
  %25 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 23, !dbg !154
  %26 = load i64, i64* %25, align 8, !dbg !154, !tbaa !155
  %27 = icmp ugt i64 %26, %24, !dbg !154
  %28 = select i1 %27, i64 %26, i64 %24, !dbg !154
  store i64 %28, i64* %25, align 8, !dbg !156, !tbaa !155
  br label %29

; <label>:29:                                     ; preds = %3, %7, %16, %18, %11
  %30 = phi i8* [ null, %7 ], [ null, %11 ], [ null, %16 ], [ %14, %18 ], [ null, %3 ], !dbg !157
  call void @llvm.dbg.value(metadata i8* %30, metadata !112, metadata !DIExpression()), !dbg !130
  ret i8* %30, !dbg !158
}

declare i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define noalias i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !159 {
  call void @llvm.dbg.value(metadata i8* %0, metadata !163, metadata !DIExpression()), !dbg !167
  call void @llvm.dbg.value(metadata i64 %1, metadata !164, metadata !DIExpression()), !dbg !168
  call void @llvm.dbg.value(metadata i64 %2, metadata !165, metadata !DIExpression()), !dbg !169
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !166, metadata !DIExpression()), !dbg !170
  %5 = icmp ne i8* %0, null, !dbg !171
  %6 = icmp ne %struct.klu_common_struct* %3, null, !dbg !173
  %7 = and i1 %5, %6, !dbg !174
  br i1 %7, label %8, label %16, !dbg !174

; <label>:8:                                      ; preds = %4
  %9 = tail call i8* @SuiteSparse_free(i8* nonnull %0) #4, !dbg !175
  %10 = icmp eq i64 %1, 0, !dbg !177
  %11 = select i1 %10, i64 1, i64 %1, !dbg !177
  %12 = mul i64 %11, %2, !dbg !178
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 22, !dbg !179
  %14 = load i64, i64* %13, align 8, !dbg !180, !tbaa !153
  %15 = sub i64 %14, %12, !dbg !180
  store i64 %15, i64* %13, align 8, !dbg !180, !tbaa !153
  br label %16, !dbg !181

; <label>:16:                                     ; preds = %8, %4
  ret i8* null, !dbg !182
}

declare i8* @SuiteSparse_free(i8*) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define i8* @klu_realloc(i64, i64, i64, i8*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !183 {
  %6 = alloca i32, align 4
  call void @llvm.dbg.value(metadata i64 %0, metadata !187, metadata !DIExpression()), !dbg !194
  call void @llvm.dbg.value(metadata i64 %1, metadata !188, metadata !DIExpression()), !dbg !195
  call void @llvm.dbg.value(metadata i64 %2, metadata !189, metadata !DIExpression()), !dbg !196
  call void @llvm.dbg.value(metadata i8* %3, metadata !190, metadata !DIExpression()), !dbg !197
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %4, metadata !191, metadata !DIExpression()), !dbg !198
  %7 = bitcast i32* %6 to i8*, !dbg !199
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %7) #4, !dbg !199
  call void @llvm.dbg.value(metadata i32 1, metadata !193, metadata !DIExpression()), !dbg !200
  store i32 1, i32* %6, align 4, !dbg !200, !tbaa !26
  %8 = icmp eq %struct.klu_common_struct* %4, null, !dbg !201
  br i1 %8, label %54, label %9, !dbg !203

; <label>:9:                                      ; preds = %5
  %10 = icmp eq i64 %2, 0, !dbg !204
  br i1 %10, label %11, label %13, !dbg !206

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !207
  store i32 -3, i32* %12, align 4, !dbg !209, !tbaa !125
  call void @llvm.dbg.value(metadata i8* null, metadata !190, metadata !DIExpression()), !dbg !197
  br label %54, !dbg !210

; <label>:13:                                     ; preds = %9
  %14 = icmp eq i8* %3, null, !dbg !211
  %15 = icmp ugt i64 %0, 2147483646, !dbg !213
  br i1 %14, label %16, label %35, !dbg !215

; <label>:16:                                     ; preds = %13
  call void @llvm.dbg.value(metadata i64 %0, metadata !109, metadata !DIExpression()) #4, !dbg !216
  call void @llvm.dbg.value(metadata i64 %2, metadata !110, metadata !DIExpression()) #4, !dbg !219
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %4, metadata !111, metadata !DIExpression()) #4, !dbg !220
  br i1 %15, label %17, label %19, !dbg !221

; <label>:17:                                     ; preds = %16
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !222
  store i32 -4, i32* %18, align 4, !dbg !223, !tbaa !125
  call void @llvm.dbg.value(metadata i8* null, metadata !112, metadata !DIExpression()) #4, !dbg !224
  br label %54, !dbg !225

; <label>:19:                                     ; preds = %16
  %20 = tail call i8* @SuiteSparse_malloc(i64 %0, i64 %2) #4, !dbg !226
  call void @llvm.dbg.value(metadata i8* %20, metadata !112, metadata !DIExpression()) #4, !dbg !224
  %21 = icmp eq i8* %20, null, !dbg !227
  br i1 %21, label %22, label %24, !dbg !228

; <label>:22:                                     ; preds = %19
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !229
  store i32 -2, i32* %23, align 4, !dbg !230, !tbaa !125
  br label %54, !dbg !231

; <label>:24:                                     ; preds = %19
  %25 = icmp eq i64 %0, 0, !dbg !232
  %26 = select i1 %25, i64 1, i64 %0, !dbg !232
  %27 = mul i64 %26, %2, !dbg !233
  %28 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 22, !dbg !234
  %29 = load i64, i64* %28, align 8, !dbg !235, !tbaa !153
  %30 = add i64 %29, %27, !dbg !235
  store i64 %30, i64* %28, align 8, !dbg !235, !tbaa !153
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 23, !dbg !236
  %32 = load i64, i64* %31, align 8, !dbg !236, !tbaa !155
  %33 = icmp ugt i64 %32, %30, !dbg !236
  %34 = select i1 %33, i64 %32, i64 %30, !dbg !236
  store i64 %34, i64* %31, align 8, !dbg !237, !tbaa !155
  br label %54

; <label>:35:                                     ; preds = %13
  br i1 %15, label %36, label %38, !dbg !238

; <label>:36:                                     ; preds = %35
  %37 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !239
  store i32 -4, i32* %37, align 4, !dbg !241, !tbaa !125
  br label %54, !dbg !242

; <label>:38:                                     ; preds = %35
  call void @llvm.dbg.value(metadata i32* %6, metadata !193, metadata !DIExpression()), !dbg !200
  %39 = call i8* @SuiteSparse_realloc(i64 %0, i64 %1, i64 %2, i8* nonnull %3, i32* nonnull %6) #4, !dbg !243
  call void @llvm.dbg.value(metadata i8* %39, metadata !192, metadata !DIExpression()), !dbg !245
  %40 = load i32, i32* %6, align 4, !dbg !246, !tbaa !26
  call void @llvm.dbg.value(metadata i32 %40, metadata !193, metadata !DIExpression()), !dbg !200
  %41 = icmp eq i32 %40, 0, !dbg !246
  br i1 %41, label %52, label %42, !dbg !248

; <label>:42:                                     ; preds = %38
  %43 = sub i64 %0, %1, !dbg !249
  %44 = mul i64 %43, %2, !dbg !251
  %45 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 22, !dbg !252
  %46 = load i64, i64* %45, align 8, !dbg !253, !tbaa !153
  %47 = add i64 %46, %44, !dbg !253
  store i64 %47, i64* %45, align 8, !dbg !253, !tbaa !153
  %48 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 23, !dbg !254
  %49 = load i64, i64* %48, align 8, !dbg !254, !tbaa !155
  %50 = icmp ugt i64 %49, %47, !dbg !254
  %51 = select i1 %50, i64 %49, i64 %47, !dbg !254
  store i64 %51, i64* %48, align 8, !dbg !255, !tbaa !155
  call void @llvm.dbg.value(metadata i8* %39, metadata !190, metadata !DIExpression()), !dbg !197
  br label %54, !dbg !256

; <label>:52:                                     ; preds = %38
  %53 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !257
  store i32 -2, i32* %53, align 4, !dbg !259, !tbaa !125
  br label %54

; <label>:54:                                     ; preds = %24, %22, %17, %5, %11, %36, %52, %42
  %55 = phi i8* [ null, %11 ], [ %3, %36 ], [ %39, %42 ], [ %3, %52 ], [ null, %5 ], [ null, %17 ], [ null, %22 ], [ %20, %24 ]
  call void @llvm.dbg.value(metadata i8* %55, metadata !190, metadata !DIExpression()), !dbg !197
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %7) #4, !dbg !260
  ret i8* %55, !dbg !261
}

declare i8* @SuiteSparse_realloc(i64, i64, i64, i8*, i32*) local_unnamed_addr #2

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #3

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #3 = { nounwind readnone speculatable }
attributes #4 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!8, !9, !10, !11}
!llvm.ident = !{!12}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_memory.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !7}
!4 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !5, line: 62, baseType: !6)
!5 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!6 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!7 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!8 = !{i32 2, !"Dwarf Version", i32 4}
!9 = !{i32 2, !"Debug Info Version", i32 3}
!10 = !{i32 1, !"wchar_size", i32 4}
!11 = !{i32 7, !"PIC Level", i32 2}
!12 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!13 = distinct !DISubprogram(name: "klu_add_size_t", scope: !1, file: !1, line: 20, type: !14, isLocal: false, isDefinition: true, scopeLine: 21, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !18)
!14 = !DISubroutineType(types: !15)
!15 = !{!4, !4, !4, !16}
!16 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !17, size: 64)
!17 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!18 = !{!19, !20, !21}
!19 = !DILocalVariable(name: "a", arg: 1, scope: !13, file: !1, line: 20, type: !4)
!20 = !DILocalVariable(name: "b", arg: 2, scope: !13, file: !1, line: 20, type: !4)
!21 = !DILocalVariable(name: "ok", arg: 3, scope: !13, file: !1, line: 20, type: !16)
!22 = !DILocation(line: 20, column: 31, scope: !13)
!23 = !DILocation(line: 20, column: 41, scope: !13)
!24 = !DILocation(line: 20, column: 49, scope: !13)
!25 = !DILocation(line: 22, column: 14, scope: !13)
!26 = !{!27, !27, i64 0}
!27 = !{!"int", !28, i64 0}
!28 = !{!"omnipotent char", !29, i64 0}
!29 = !{!"Simple C/C++ TBAA"}
!30 = !DILocation(line: 22, column: 13, scope: !13)
!31 = !DILocation(line: 22, column: 19, scope: !13)
!32 = !DILocation(line: 22, column: 11, scope: !13)
!33 = !DILocation(line: 23, column: 5, scope: !13)
!34 = !DILocation(line: 22, column: 26, scope: !13)
!35 = !DILocation(line: 22, column: 34, scope: !13)
!36 = !DILocation(line: 22, column: 31, scope: !13)
!37 = !DILocation(line: 23, column: 13, scope: !13)
!38 = distinct !DISubprogram(name: "klu_mult_size_t", scope: !1, file: !1, line: 32, type: !14, isLocal: false, isDefinition: true, scopeLine: 33, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !39)
!39 = !{!40, !41, !42, !43, !44}
!40 = !DILocalVariable(name: "a", arg: 1, scope: !38, file: !1, line: 32, type: !4)
!41 = !DILocalVariable(name: "k", arg: 2, scope: !38, file: !1, line: 32, type: !4)
!42 = !DILocalVariable(name: "ok", arg: 3, scope: !38, file: !1, line: 32, type: !16)
!43 = !DILocalVariable(name: "i", scope: !38, file: !1, line: 34, type: !4)
!44 = !DILocalVariable(name: "s", scope: !38, file: !1, line: 34, type: !4)
!45 = !DILocation(line: 32, column: 32, scope: !38)
!46 = !DILocation(line: 32, column: 42, scope: !38)
!47 = !DILocation(line: 32, column: 50, scope: !38)
!48 = !DILocation(line: 34, column: 15, scope: !38)
!49 = !DILocation(line: 34, column: 12, scope: !38)
!50 = !DILocation(line: 35, column: 20, scope: !51)
!51 = distinct !DILexicalBlock(scope: !52, file: !1, line: 35, column: 5)
!52 = distinct !DILexicalBlock(scope: !38, file: !1, line: 35, column: 5)
!53 = !DILocation(line: 0, scope: !38)
!54 = !DILocation(line: 35, column: 5, scope: !52)
!55 = !DILocation(line: 22, column: 19, scope: !13, inlinedAt: !56)
!56 = distinct !DILocation(line: 37, column: 13, scope: !57)
!57 = distinct !DILexicalBlock(scope: !51, file: !1, line: 36, column: 5)
!58 = !DILocation(line: 20, column: 31, scope: !13, inlinedAt: !56)
!59 = !DILocation(line: 20, column: 41, scope: !13, inlinedAt: !56)
!60 = !DILocation(line: 20, column: 49, scope: !13, inlinedAt: !56)
!61 = !DILocation(line: 22, column: 11, scope: !13, inlinedAt: !56)
!62 = !DILocation(line: 23, column: 5, scope: !13, inlinedAt: !56)
!63 = !DILocation(line: 22, column: 26, scope: !13, inlinedAt: !56)
!64 = !DILocation(line: 22, column: 34, scope: !13, inlinedAt: !56)
!65 = !DILocation(line: 22, column: 31, scope: !13, inlinedAt: !56)
!66 = !DILocation(line: 23, column: 13, scope: !13, inlinedAt: !56)
!67 = !DILocation(line: 39, column: 13, scope: !38)
!68 = !DILocation(line: 39, column: 5, scope: !38)
!69 = distinct !{!69, !54, !70}
!70 = !DILocation(line: 38, column: 5, scope: !52)
!71 = distinct !DISubprogram(name: "klu_malloc", scope: !1, file: !1, line: 60, type: !72, isLocal: false, isDefinition: true, scopeLine: 68, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !108)
!72 = !DISubroutineType(types: !73)
!73 = !{!7, !4, !4, !74}
!74 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !75, size: 64)
!75 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !76, line: 207, baseType: !77)
!76 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!77 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !76, line: 137, size: 1280, elements: !78)
!78 = !{!79, !81, !82, !83, !84, !85, !86, !87, !88, !93, !94, !95, !96, !97, !98, !99, !100, !101, !102, !103, !104, !105, !106, !107}
!79 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !77, file: !76, line: 144, baseType: !80, size: 64)
!80 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!81 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !77, file: !76, line: 145, baseType: !80, size: 64, offset: 64)
!82 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !77, file: !76, line: 146, baseType: !80, size: 64, offset: 128)
!83 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !77, file: !76, line: 147, baseType: !80, size: 64, offset: 192)
!84 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !77, file: !76, line: 148, baseType: !80, size: 64, offset: 256)
!85 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !77, file: !76, line: 150, baseType: !17, size: 32, offset: 320)
!86 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !77, file: !76, line: 151, baseType: !17, size: 32, offset: 352)
!87 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !77, file: !76, line: 153, baseType: !17, size: 32, offset: 384)
!88 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !77, file: !76, line: 157, baseType: !89, size: 64, offset: 448)
!89 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !90, size: 64)
!90 = !DISubroutineType(types: !91)
!91 = !{!17, !17, !16, !16, !16, !92}
!92 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !77, size: 64)
!93 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !77, file: !76, line: 162, baseType: !7, size: 64, offset: 512)
!94 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !77, file: !76, line: 164, baseType: !17, size: 32, offset: 576)
!95 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !77, file: !76, line: 177, baseType: !17, size: 32, offset: 608)
!96 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !77, file: !76, line: 178, baseType: !17, size: 32, offset: 640)
!97 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !77, file: !76, line: 180, baseType: !17, size: 32, offset: 672)
!98 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !77, file: !76, line: 185, baseType: !17, size: 32, offset: 704)
!99 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !77, file: !76, line: 191, baseType: !17, size: 32, offset: 736)
!100 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !77, file: !76, line: 196, baseType: !17, size: 32, offset: 768)
!101 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !77, file: !76, line: 198, baseType: !80, size: 64, offset: 832)
!102 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !77, file: !76, line: 199, baseType: !80, size: 64, offset: 896)
!103 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !77, file: !76, line: 200, baseType: !80, size: 64, offset: 960)
!104 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !77, file: !76, line: 201, baseType: !80, size: 64, offset: 1024)
!105 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !77, file: !76, line: 202, baseType: !80, size: 64, offset: 1088)
!106 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !77, file: !76, line: 204, baseType: !4, size: 64, offset: 1152)
!107 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !77, file: !76, line: 205, baseType: !4, size: 64, offset: 1216)
!108 = !{!109, !110, !111, !112}
!109 = !DILocalVariable(name: "n", arg: 1, scope: !71, file: !1, line: 63, type: !4)
!110 = !DILocalVariable(name: "size", arg: 2, scope: !71, file: !1, line: 64, type: !4)
!111 = !DILocalVariable(name: "Common", arg: 3, scope: !71, file: !1, line: 66, type: !74)
!112 = !DILocalVariable(name: "p", scope: !71, file: !1, line: 69, type: !7)
!113 = !DILocation(line: 63, column: 12, scope: !71)
!114 = !DILocation(line: 64, column: 12, scope: !71)
!115 = !DILocation(line: 66, column: 17, scope: !71)
!116 = !DILocation(line: 71, column: 16, scope: !117)
!117 = distinct !DILexicalBlock(scope: !71, file: !1, line: 71, column: 9)
!118 = !DILocation(line: 71, column: 9, scope: !71)
!119 = !DILocation(line: 75, column: 19, scope: !120)
!120 = distinct !DILexicalBlock(scope: !117, file: !1, line: 75, column: 14)
!121 = !DILocation(line: 75, column: 14, scope: !117)
!122 = !DILocation(line: 78, column: 17, scope: !123)
!123 = distinct !DILexicalBlock(scope: !120, file: !1, line: 76, column: 5)
!124 = !DILocation(line: 78, column: 24, scope: !123)
!125 = !{!126, !27, i64 76}
!126 = !{!"klu_common_struct", !127, i64 0, !127, i64 8, !127, i64 16, !127, i64 24, !127, i64 32, !27, i64 40, !27, i64 44, !27, i64 48, !128, i64 56, !128, i64 64, !27, i64 72, !27, i64 76, !27, i64 80, !27, i64 84, !27, i64 88, !27, i64 92, !27, i64 96, !127, i64 104, !127, i64 112, !127, i64 120, !127, i64 128, !127, i64 136, !129, i64 144, !129, i64 152}
!127 = !{!"double", !28, i64 0}
!128 = !{!"any pointer", !28, i64 0}
!129 = !{!"long", !28, i64 0}
!130 = !DILocation(line: 69, column: 11, scope: !71)
!131 = !DILocation(line: 80, column: 5, scope: !123)
!132 = !DILocation(line: 81, column: 16, scope: !133)
!133 = distinct !DILexicalBlock(scope: !120, file: !1, line: 81, column: 14)
!134 = !DILocation(line: 81, column: 14, scope: !120)
!135 = !DILocation(line: 85, column: 17, scope: !136)
!136 = distinct !DILexicalBlock(scope: !133, file: !1, line: 82, column: 5)
!137 = !DILocation(line: 85, column: 24, scope: !136)
!138 = !DILocation(line: 87, column: 5, scope: !136)
!139 = !DILocation(line: 91, column: 13, scope: !140)
!140 = distinct !DILexicalBlock(scope: !133, file: !1, line: 89, column: 5)
!141 = !DILocation(line: 92, column: 15, scope: !142)
!142 = distinct !DILexicalBlock(scope: !140, file: !1, line: 92, column: 13)
!143 = !DILocation(line: 92, column: 13, scope: !140)
!144 = !DILocation(line: 95, column: 21, scope: !145)
!145 = distinct !DILexicalBlock(scope: !142, file: !1, line: 93, column: 9)
!146 = !DILocation(line: 95, column: 28, scope: !145)
!147 = !DILocation(line: 96, column: 9, scope: !145)
!148 = !DILocation(line: 99, column: 34, scope: !149)
!149 = distinct !DILexicalBlock(scope: !142, file: !1, line: 98, column: 9)
!150 = !DILocation(line: 99, column: 44, scope: !149)
!151 = !DILocation(line: 99, column: 21, scope: !149)
!152 = !DILocation(line: 99, column: 30, scope: !149)
!153 = !{!126, !129, i64 144}
!154 = !DILocation(line: 100, column: 31, scope: !149)
!155 = !{!126, !129, i64 152}
!156 = !DILocation(line: 100, column: 29, scope: !149)
!157 = !DILocation(line: 0, scope: !140)
!158 = !DILocation(line: 103, column: 5, scope: !71)
!159 = distinct !DISubprogram(name: "klu_free", scope: !1, file: !1, line: 117, type: !160, isLocal: false, isDefinition: true, scopeLine: 127, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !162)
!160 = !DISubroutineType(types: !161)
!161 = !{!7, !7, !4, !4, !74}
!162 = !{!163, !164, !165, !166}
!163 = !DILocalVariable(name: "p", arg: 1, scope: !159, file: !1, line: 120, type: !7)
!164 = !DILocalVariable(name: "n", arg: 2, scope: !159, file: !1, line: 122, type: !4)
!165 = !DILocalVariable(name: "size", arg: 3, scope: !159, file: !1, line: 123, type: !4)
!166 = !DILocalVariable(name: "Common", arg: 4, scope: !159, file: !1, line: 125, type: !74)
!167 = !DILocation(line: 120, column: 11, scope: !159)
!168 = !DILocation(line: 122, column: 12, scope: !159)
!169 = !DILocation(line: 123, column: 12, scope: !159)
!170 = !DILocation(line: 125, column: 17, scope: !159)
!171 = !DILocation(line: 128, column: 11, scope: !172)
!172 = distinct !DILexicalBlock(scope: !159, file: !1, line: 128, column: 9)
!173 = !DILocation(line: 128, column: 29, scope: !172)
!174 = !DILocation(line: 128, column: 19, scope: !172)
!175 = !DILocation(line: 132, column: 9, scope: !176)
!176 = distinct !DILexicalBlock(scope: !172, file: !1, line: 129, column: 5)
!177 = !DILocation(line: 133, column: 30, scope: !176)
!178 = !DILocation(line: 133, column: 40, scope: !176)
!179 = !DILocation(line: 133, column: 17, scope: !176)
!180 = !DILocation(line: 133, column: 26, scope: !176)
!181 = !DILocation(line: 134, column: 5, scope: !176)
!182 = !DILocation(line: 137, column: 5, scope: !159)
!183 = distinct !DISubprogram(name: "klu_realloc", scope: !1, file: !1, line: 162, type: !184, isLocal: false, isDefinition: true, scopeLine: 173, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !186)
!184 = !DISubroutineType(types: !185)
!185 = !{!7, !4, !4, !4, !7, !74}
!186 = !{!187, !188, !189, !190, !191, !192, !193}
!187 = !DILocalVariable(name: "nnew", arg: 1, scope: !183, file: !1, line: 165, type: !4)
!188 = !DILocalVariable(name: "nold", arg: 2, scope: !183, file: !1, line: 166, type: !4)
!189 = !DILocalVariable(name: "size", arg: 3, scope: !183, file: !1, line: 167, type: !4)
!190 = !DILocalVariable(name: "p", arg: 4, scope: !183, file: !1, line: 169, type: !7)
!191 = !DILocalVariable(name: "Common", arg: 5, scope: !183, file: !1, line: 171, type: !74)
!192 = !DILocalVariable(name: "pnew", scope: !183, file: !1, line: 174, type: !7)
!193 = !DILocalVariable(name: "ok", scope: !183, file: !1, line: 175, type: !17)
!194 = !DILocation(line: 165, column: 12, scope: !183)
!195 = !DILocation(line: 166, column: 12, scope: !183)
!196 = !DILocation(line: 167, column: 12, scope: !183)
!197 = !DILocation(line: 169, column: 11, scope: !183)
!198 = !DILocation(line: 171, column: 17, scope: !183)
!199 = !DILocation(line: 175, column: 5, scope: !183)
!200 = !DILocation(line: 175, column: 9, scope: !183)
!201 = !DILocation(line: 177, column: 16, scope: !202)
!202 = distinct !DILexicalBlock(scope: !183, file: !1, line: 177, column: 9)
!203 = !DILocation(line: 177, column: 9, scope: !183)
!204 = !DILocation(line: 181, column: 19, scope: !205)
!205 = distinct !DILexicalBlock(scope: !202, file: !1, line: 181, column: 14)
!206 = !DILocation(line: 181, column: 14, scope: !202)
!207 = !DILocation(line: 184, column: 17, scope: !208)
!208 = distinct !DILexicalBlock(scope: !205, file: !1, line: 182, column: 5)
!209 = !DILocation(line: 184, column: 24, scope: !208)
!210 = !DILocation(line: 186, column: 5, scope: !208)
!211 = !DILocation(line: 187, column: 16, scope: !212)
!212 = distinct !DILexicalBlock(scope: !205, file: !1, line: 187, column: 14)
!213 = !DILocation(line: 0, scope: !214)
!214 = distinct !DILexicalBlock(scope: !212, file: !1, line: 192, column: 14)
!215 = !DILocation(line: 187, column: 14, scope: !205)
!216 = !DILocation(line: 63, column: 12, scope: !71, inlinedAt: !217)
!217 = distinct !DILocation(line: 190, column: 13, scope: !218)
!218 = distinct !DILexicalBlock(scope: !212, file: !1, line: 188, column: 5)
!219 = !DILocation(line: 64, column: 12, scope: !71, inlinedAt: !217)
!220 = !DILocation(line: 66, column: 17, scope: !71, inlinedAt: !217)
!221 = !DILocation(line: 81, column: 14, scope: !120, inlinedAt: !217)
!222 = !DILocation(line: 85, column: 17, scope: !136, inlinedAt: !217)
!223 = !DILocation(line: 85, column: 24, scope: !136, inlinedAt: !217)
!224 = !DILocation(line: 69, column: 11, scope: !71, inlinedAt: !217)
!225 = !DILocation(line: 87, column: 5, scope: !136, inlinedAt: !217)
!226 = !DILocation(line: 91, column: 13, scope: !140, inlinedAt: !217)
!227 = !DILocation(line: 92, column: 15, scope: !142, inlinedAt: !217)
!228 = !DILocation(line: 92, column: 13, scope: !140, inlinedAt: !217)
!229 = !DILocation(line: 95, column: 21, scope: !145, inlinedAt: !217)
!230 = !DILocation(line: 95, column: 28, scope: !145, inlinedAt: !217)
!231 = !DILocation(line: 96, column: 9, scope: !145, inlinedAt: !217)
!232 = !DILocation(line: 99, column: 34, scope: !149, inlinedAt: !217)
!233 = !DILocation(line: 99, column: 44, scope: !149, inlinedAt: !217)
!234 = !DILocation(line: 99, column: 21, scope: !149, inlinedAt: !217)
!235 = !DILocation(line: 99, column: 30, scope: !149, inlinedAt: !217)
!236 = !DILocation(line: 100, column: 31, scope: !149, inlinedAt: !217)
!237 = !DILocation(line: 100, column: 29, scope: !149, inlinedAt: !217)
!238 = !DILocation(line: 192, column: 14, scope: !212)
!239 = !DILocation(line: 195, column: 17, scope: !240)
!240 = distinct !DILexicalBlock(scope: !214, file: !1, line: 193, column: 5)
!241 = !DILocation(line: 195, column: 24, scope: !240)
!242 = !DILocation(line: 196, column: 5, scope: !240)
!243 = !DILocation(line: 201, column: 16, scope: !244)
!244 = distinct !DILexicalBlock(scope: !214, file: !1, line: 198, column: 5)
!245 = !DILocation(line: 174, column: 11, scope: !183)
!246 = !DILocation(line: 202, column: 13, scope: !247)
!247 = distinct !DILexicalBlock(scope: !244, file: !1, line: 202, column: 13)
!248 = !DILocation(line: 202, column: 13, scope: !244)
!249 = !DILocation(line: 205, column: 39, scope: !250)
!250 = distinct !DILexicalBlock(scope: !247, file: !1, line: 203, column: 9)
!251 = !DILocation(line: 205, column: 46, scope: !250)
!252 = !DILocation(line: 205, column: 21, scope: !250)
!253 = !DILocation(line: 205, column: 30, scope: !250)
!254 = !DILocation(line: 206, column: 31, scope: !250)
!255 = !DILocation(line: 206, column: 29, scope: !250)
!256 = !DILocation(line: 208, column: 9, scope: !250)
!257 = !DILocation(line: 212, column: 21, scope: !258)
!258 = distinct !DILexicalBlock(scope: !247, file: !1, line: 210, column: 9)
!259 = !DILocation(line: 212, column: 28, scope: !258)
!260 = !DILocation(line: 216, column: 1, scope: !183)
!261 = !DILocation(line: 215, column: 5, scope: !183)
