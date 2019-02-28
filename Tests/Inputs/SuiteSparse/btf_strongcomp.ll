; ModuleID = 'btf_strongcomp.c'
source_filename = "btf_strongcomp.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

; Function Attrs: nounwind ssp uwtable
define i32 @btf_strongcomp(i32, i32* nocapture readonly, i32* nocapture readonly, i32*, i32* nocapture, i32* nocapture, i32* nocapture) local_unnamed_addr #0 !dbg !12 {
  %8 = bitcast i32* %5 to i8*
  %9 = alloca i32, align 4
  %10 = alloca i32, align 4
  call void @llvm.dbg.value(metadata i32 %0, metadata !16, metadata !DIExpression()), !dbg !34
  call void @llvm.dbg.value(metadata i32* %1, metadata !17, metadata !DIExpression()), !dbg !35
  call void @llvm.dbg.value(metadata i32* %2, metadata !18, metadata !DIExpression()), !dbg !36
  call void @llvm.dbg.value(metadata i32* %3, metadata !19, metadata !DIExpression()), !dbg !37
  call void @llvm.dbg.value(metadata i32* %4, metadata !20, metadata !DIExpression()), !dbg !38
  call void @llvm.dbg.value(metadata i32* %5, metadata !21, metadata !DIExpression()), !dbg !39
  call void @llvm.dbg.value(metadata i32* %6, metadata !22, metadata !DIExpression()), !dbg !40
  %11 = bitcast i32* %9 to i8*, !dbg !41
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %11) #3, !dbg !41
  %12 = bitcast i32* %10 to i8*, !dbg !41
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %12) #3, !dbg !41
  call void @llvm.dbg.value(metadata i32* %6, metadata !30, metadata !DIExpression()), !dbg !42
  %13 = sext i32 %0 to i64, !dbg !43
  %14 = getelementptr inbounds i32, i32* %6, i64 %13, !dbg !43
  call void @llvm.dbg.value(metadata i32* %14, metadata !22, metadata !DIExpression()), !dbg !40
  call void @llvm.dbg.value(metadata i32* %14, metadata !28, metadata !DIExpression()), !dbg !44
  %15 = getelementptr inbounds i32, i32* %14, i64 %13, !dbg !45
  call void @llvm.dbg.value(metadata i32* %15, metadata !22, metadata !DIExpression()), !dbg !40
  call void @llvm.dbg.value(metadata i32* %4, metadata !31, metadata !DIExpression()), !dbg !46
  call void @llvm.dbg.value(metadata i32* %5, metadata !29, metadata !DIExpression()), !dbg !47
  call void @llvm.dbg.value(metadata i32* %15, metadata !32, metadata !DIExpression()), !dbg !48
  %16 = getelementptr inbounds i32, i32* %15, i64 %13, !dbg !49
  call void @llvm.dbg.value(metadata i32* %16, metadata !22, metadata !DIExpression()), !dbg !40
  call void @llvm.dbg.value(metadata i32* %16, metadata !33, metadata !DIExpression()), !dbg !50
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  %17 = icmp sgt i32 %0, 0, !dbg !52
  br i1 %17, label %19, label %18, !dbg !55

; <label>:18:                                     ; preds = %7
  call void @llvm.dbg.value(metadata i32 0, metadata !26, metadata !DIExpression()), !dbg !56
  store i32 0, i32* %9, align 4, !dbg !57, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !27, metadata !DIExpression()), !dbg !62
  store i32 0, i32* %10, align 4, !dbg !63, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  br label %42, !dbg !64

; <label>:19:                                     ; preds = %7
  %20 = zext i32 %0 to i64
  br label %21, !dbg !55

; <label>:21:                                     ; preds = %21, %19
  %22 = phi i64 [ 0, %19 ], [ %26, %21 ]
  call void @llvm.dbg.value(metadata i64 %22, metadata !23, metadata !DIExpression()), !dbg !51
  %23 = getelementptr inbounds i32, i32* %14, i64 %22, !dbg !66
  store i32 -2, i32* %23, align 4, !dbg !68, !tbaa !58
  %24 = getelementptr inbounds i32, i32* %4, i64 %22, !dbg !69
  store i32 -1, i32* %24, align 4, !dbg !70, !tbaa !58
  %25 = getelementptr inbounds i32, i32* %6, i64 %22, !dbg !71
  store i32 -1, i32* %25, align 4, !dbg !72, !tbaa !58
  %26 = add nuw nsw i64 %22, 1, !dbg !73
  call void @llvm.dbg.value(metadata i32 undef, metadata !23, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !51
  %27 = icmp eq i64 %26, %20, !dbg !52
  br i1 %27, label %28, label %21, !dbg !55, !llvm.loop !74

; <label>:28:                                     ; preds = %21
  call void @llvm.dbg.value(metadata i32 0, metadata !26, metadata !DIExpression()), !dbg !56
  store i32 0, i32* %9, align 4, !dbg !57, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !27, metadata !DIExpression()), !dbg !62
  store i32 0, i32* %10, align 4, !dbg !63, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  %29 = icmp sgt i32 %0, 0, !dbg !76
  br i1 %29, label %30, label %42, !dbg !64

; <label>:30:                                     ; preds = %28
  %31 = zext i32 %0 to i64
  br label %32, !dbg !64

; <label>:32:                                     ; preds = %39, %30
  %33 = phi i64 [ 0, %30 ], [ %40, %39 ]
  call void @llvm.dbg.value(metadata i64 %33, metadata !23, metadata !DIExpression()), !dbg !51
  %34 = getelementptr inbounds i32, i32* %14, i64 %33, !dbg !78
  %35 = load i32, i32* %34, align 4, !dbg !78, !tbaa !58
  %36 = icmp eq i32 %35, -2, !dbg !81
  br i1 %36, label %37, label %39, !dbg !82

; <label>:37:                                     ; preds = %32
  call void @llvm.dbg.value(metadata i32* %9, metadata !26, metadata !DIExpression()), !dbg !56
  call void @llvm.dbg.value(metadata i32* %10, metadata !27, metadata !DIExpression()), !dbg !62
  %38 = trunc i64 %33 to i32, !dbg !83
  call fastcc void @dfs(i32 %38, i32* %1, i32* %2, i32* %3, i32* nonnull %6, i32* nonnull %14, i32* %4, i32* nonnull %10, i32* nonnull %9, i32* %5, i32* nonnull %15, i32* nonnull %16), !dbg !83
  br label %39, !dbg !85

; <label>:39:                                     ; preds = %32, %37
  %40 = add nuw nsw i64 %33, 1, !dbg !86
  call void @llvm.dbg.value(metadata i32 undef, metadata !23, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !51
  %41 = icmp eq i64 %40, %31, !dbg !76
  br i1 %41, label %43, label %32, !dbg !64, !llvm.loop !87

; <label>:42:                                     ; preds = %18, %28
  call void @llvm.dbg.value(metadata i32 0, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 %44, metadata !27, metadata !DIExpression()), !dbg !62
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  store i32 0, i32* %6, align 4, !dbg !90, !tbaa !58
  call void @llvm.dbg.value(metadata i32 1, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  call void @llvm.dbg.value(metadata i32 0, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  store i32 %0, i32* %5, align 4, !dbg !91, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  br label %112, !dbg !92

; <label>:43:                                     ; preds = %39
  %44 = load i32, i32* %10, align 4, !dbg !94, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 %44, metadata !27, metadata !DIExpression()), !dbg !62
  %45 = icmp sgt i32 %44, 0, !dbg !97
  br i1 %45, label %46, label %52, !dbg !98

; <label>:46:                                     ; preds = %43
  %47 = load i32, i32* %10, align 4, !tbaa !58
  %48 = icmp sgt i32 %47, 1, !dbg !98
  %49 = select i1 %48, i32 %47, i32 1, !dbg !98
  %50 = zext i32 %49 to i64, !dbg !98
  %51 = shl nuw nsw i64 %50, 2, !dbg !98
  call void @llvm.memset.p0i8.i64(i8* %8, i8 0, i64 %51, i32 4, i1 false), !dbg !99
  br label %52, !dbg !101

; <label>:52:                                     ; preds = %46, %43
  %53 = phi i32 [ %44, %43 ], [ %47, %46 ]
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  %54 = icmp sgt i32 %0, 0, !dbg !101
  br i1 %54, label %55, label %67, !dbg !104

; <label>:55:                                     ; preds = %52
  %56 = zext i32 %0 to i64
  br label %57, !dbg !104

; <label>:57:                                     ; preds = %57, %55
  %58 = phi i64 [ 0, %55 ], [ %65, %57 ]
  call void @llvm.dbg.value(metadata i64 %58, metadata !23, metadata !DIExpression()), !dbg !51
  %59 = getelementptr inbounds i32, i32* %14, i64 %58, !dbg !105
  %60 = load i32, i32* %59, align 4, !dbg !105, !tbaa !58
  %61 = sext i32 %60 to i64, !dbg !107
  %62 = getelementptr inbounds i32, i32* %5, i64 %61, !dbg !107
  %63 = load i32, i32* %62, align 4, !dbg !108, !tbaa !58
  %64 = add nsw i32 %63, 1, !dbg !108
  store i32 %64, i32* %62, align 4, !dbg !108, !tbaa !58
  %65 = add nuw nsw i64 %58, 1, !dbg !109
  call void @llvm.dbg.value(metadata i32 undef, metadata !23, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !51
  %66 = icmp eq i64 %65, %56, !dbg !101
  br i1 %66, label %67, label %57, !dbg !104, !llvm.loop !110

; <label>:67:                                     ; preds = %57, %52
  store i32 0, i32* %6, align 4, !dbg !90, !tbaa !58
  call void @llvm.dbg.value(metadata i32 1, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  %68 = icmp sgt i32 %53, 1, !dbg !112
  br i1 %68, label %69, label %82, !dbg !115

; <label>:69:                                     ; preds = %67
  %70 = zext i32 %53 to i64
  %71 = load i32, i32* %6, align 4
  br label %72, !dbg !115

; <label>:72:                                     ; preds = %72, %69
  %73 = phi i32 [ %71, %69 ], [ %78, %72 ]
  %74 = phi i64 [ 1, %69 ], [ %80, %72 ]
  call void @llvm.dbg.value(metadata i64 %74, metadata !25, metadata !DIExpression()), !dbg !89
  %75 = add nsw i64 %74, -1, !dbg !116
  %76 = getelementptr inbounds i32, i32* %5, i64 %75, !dbg !118
  %77 = load i32, i32* %76, align 4, !dbg !118, !tbaa !58
  %78 = add nsw i32 %77, %73, !dbg !119
  %79 = getelementptr inbounds i32, i32* %6, i64 %74, !dbg !120
  store i32 %78, i32* %79, align 4, !dbg !121, !tbaa !58
  %80 = add nuw nsw i64 %74, 1, !dbg !122
  call void @llvm.dbg.value(metadata i32 undef, metadata !25, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  %81 = icmp eq i64 %80, %70, !dbg !112
  br i1 %81, label %82, label %72, !dbg !115, !llvm.loop !123

; <label>:82:                                     ; preds = %72, %67
  call void @llvm.dbg.value(metadata i32 0, metadata !25, metadata !DIExpression()), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  %83 = icmp sgt i32 %53, 0, !dbg !125
  br i1 %83, label %84, label %93, !dbg !128

; <label>:84:                                     ; preds = %82
  %85 = zext i32 %53 to i64
  br label %86, !dbg !128

; <label>:86:                                     ; preds = %86, %84
  %87 = phi i64 [ 0, %84 ], [ %91, %86 ]
  call void @llvm.dbg.value(metadata i64 %87, metadata !25, metadata !DIExpression()), !dbg !89
  %88 = getelementptr inbounds i32, i32* %6, i64 %87, !dbg !129
  %89 = load i32, i32* %88, align 4, !dbg !129, !tbaa !58
  %90 = getelementptr inbounds i32, i32* %5, i64 %87, !dbg !131
  store i32 %89, i32* %90, align 4, !dbg !132, !tbaa !58
  %91 = add nuw nsw i64 %87, 1, !dbg !133
  call void @llvm.dbg.value(metadata i32 undef, metadata !25, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !89
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  %92 = icmp eq i64 %91, %85, !dbg !125
  br i1 %92, label %93, label %86, !dbg !128, !llvm.loop !134

; <label>:93:                                     ; preds = %86, %82
  %94 = sext i32 %53 to i64, !dbg !136
  %95 = getelementptr inbounds i32, i32* %5, i64 %94, !dbg !136
  store i32 %0, i32* %95, align 4, !dbg !91, !tbaa !58
  call void @llvm.dbg.value(metadata i32 0, metadata !23, metadata !DIExpression()), !dbg !51
  %96 = icmp sgt i32 %0, 0, !dbg !137
  br i1 %96, label %97, label %112, !dbg !92

; <label>:97:                                     ; preds = %93
  %98 = zext i32 %0 to i64
  br label %99, !dbg !92

; <label>:99:                                     ; preds = %99, %97
  %100 = phi i64 [ 0, %97 ], [ %110, %99 ]
  call void @llvm.dbg.value(metadata i64 %100, metadata !23, metadata !DIExpression()), !dbg !51
  %101 = getelementptr inbounds i32, i32* %14, i64 %100, !dbg !139
  %102 = load i32, i32* %101, align 4, !dbg !139, !tbaa !58
  %103 = sext i32 %102 to i64, !dbg !141
  %104 = getelementptr inbounds i32, i32* %6, i64 %103, !dbg !141
  %105 = load i32, i32* %104, align 4, !dbg !142, !tbaa !58
  %106 = add nsw i32 %105, 1, !dbg !142
  store i32 %106, i32* %104, align 4, !dbg !142, !tbaa !58
  %107 = sext i32 %105 to i64, !dbg !143
  %108 = getelementptr inbounds i32, i32* %4, i64 %107, !dbg !143
  %109 = trunc i64 %100 to i32, !dbg !144
  store i32 %109, i32* %108, align 4, !dbg !144, !tbaa !58
  %110 = add nuw nsw i64 %100, 1, !dbg !145
  call void @llvm.dbg.value(metadata i32 undef, metadata !23, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !51
  %111 = icmp eq i64 %110, %98, !dbg !137
  br i1 %111, label %112, label %99, !dbg !92, !llvm.loop !146

; <label>:112:                                    ; preds = %99, %42, %93
  %113 = phi i32 [ 0, %42 ], [ %53, %93 ], [ %53, %99 ]
  %114 = icmp ne i32* %3, null, !dbg !148
  %115 = icmp sgt i32 %0, 0, !dbg !150
  %116 = and i1 %114, %115, !dbg !154
  call void @llvm.dbg.value(metadata i32 0, metadata !24, metadata !DIExpression()), !dbg !155
  br i1 %116, label %117, label %140, !dbg !154

; <label>:117:                                    ; preds = %112
  %118 = zext i32 %0 to i64
  br label %119, !dbg !156

; <label>:119:                                    ; preds = %119, %117
  %120 = phi i64 [ 0, %117 ], [ %127, %119 ]
  call void @llvm.dbg.value(metadata i64 %120, metadata !24, metadata !DIExpression()), !dbg !155
  %121 = getelementptr inbounds i32, i32* %4, i64 %120, !dbg !157
  %122 = load i32, i32* %121, align 4, !dbg !157, !tbaa !58
  %123 = sext i32 %122 to i64, !dbg !159
  %124 = getelementptr inbounds i32, i32* %3, i64 %123, !dbg !159
  %125 = load i32, i32* %124, align 4, !dbg !159, !tbaa !58
  %126 = getelementptr inbounds i32, i32* %6, i64 %120, !dbg !160
  store i32 %125, i32* %126, align 4, !dbg !161, !tbaa !58
  %127 = add nuw nsw i64 %120, 1, !dbg !162
  call void @llvm.dbg.value(metadata i32 undef, metadata !24, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !155
  %128 = icmp eq i64 %127, %118, !dbg !150
  br i1 %128, label %129, label %119, !dbg !156, !llvm.loop !163

; <label>:129:                                    ; preds = %119
  call void @llvm.dbg.value(metadata i32 0, metadata !24, metadata !DIExpression()), !dbg !155
  %130 = icmp sgt i32 %0, 0, !dbg !165
  br i1 %130, label %131, label %140, !dbg !168

; <label>:131:                                    ; preds = %129
  %132 = zext i32 %0 to i64
  br label %133, !dbg !168

; <label>:133:                                    ; preds = %133, %131
  %134 = phi i64 [ 0, %131 ], [ %138, %133 ]
  call void @llvm.dbg.value(metadata i64 %134, metadata !24, metadata !DIExpression()), !dbg !155
  %135 = getelementptr inbounds i32, i32* %6, i64 %134, !dbg !169
  %136 = load i32, i32* %135, align 4, !dbg !169, !tbaa !58
  %137 = getelementptr inbounds i32, i32* %3, i64 %134, !dbg !171
  store i32 %136, i32* %137, align 4, !dbg !172, !tbaa !58
  %138 = add nuw nsw i64 %134, 1, !dbg !173
  call void @llvm.dbg.value(metadata i32 undef, metadata !24, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !155
  %139 = icmp eq i64 %138, %132, !dbg !165
  br i1 %139, label %140, label %133, !dbg !168, !llvm.loop !174

; <label>:140:                                    ; preds = %133, %112, %129
  call void @llvm.dbg.value(metadata i32 undef, metadata !27, metadata !DIExpression()), !dbg !62
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %12) #3, !dbg !176
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %11) #3, !dbg !176
  ret i32 %113, !dbg !177
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define internal fastcc void @dfs(i32, i32* nocapture readonly, i32* nocapture readonly, i32* readonly, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture, i32* nocapture) unnamed_addr #0 !dbg !178 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !182, metadata !DIExpression()), !dbg !203
  call void @llvm.dbg.value(metadata i32* %1, metadata !183, metadata !DIExpression()), !dbg !204
  call void @llvm.dbg.value(metadata i32* %2, metadata !184, metadata !DIExpression()), !dbg !205
  call void @llvm.dbg.value(metadata i32* %3, metadata !185, metadata !DIExpression()), !dbg !206
  call void @llvm.dbg.value(metadata i32* %4, metadata !186, metadata !DIExpression()), !dbg !207
  call void @llvm.dbg.value(metadata i32* %5, metadata !187, metadata !DIExpression()), !dbg !208
  call void @llvm.dbg.value(metadata i32* %6, metadata !188, metadata !DIExpression()), !dbg !209
  call void @llvm.dbg.value(metadata i32* %7, metadata !189, metadata !DIExpression()), !dbg !210
  call void @llvm.dbg.value(metadata i32* %8, metadata !190, metadata !DIExpression()), !dbg !211
  call void @llvm.dbg.value(metadata i32* %9, metadata !191, metadata !DIExpression()), !dbg !212
  call void @llvm.dbg.value(metadata i32* %10, metadata !192, metadata !DIExpression()), !dbg !213
  call void @llvm.dbg.value(metadata i32* %11, metadata !193, metadata !DIExpression()), !dbg !214
  %13 = load i32, i32* %7, align 4, !dbg !215, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %13, metadata !201, metadata !DIExpression()), !dbg !216
  %14 = load i32, i32* %8, align 4, !dbg !217, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %14, metadata !202, metadata !DIExpression()), !dbg !218
  call void @llvm.dbg.value(metadata i32 0, metadata !194, metadata !DIExpression()), !dbg !219
  call void @llvm.dbg.value(metadata i32 0, metadata !195, metadata !DIExpression()), !dbg !220
  store i32 %0, i32* %10, align 4, !dbg !221, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %14, metadata !202, metadata !DIExpression()), !dbg !218
  call void @llvm.dbg.value(metadata i32 %13, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 0, metadata !194, metadata !DIExpression()), !dbg !219
  call void @llvm.dbg.value(metadata i32 0, metadata !195, metadata !DIExpression()), !dbg !220
  %15 = icmp eq i32* %3, null
  br label %16, !dbg !222

; <label>:16:                                     ; preds = %12, %124
  %17 = phi i32 [ %14, %12 ], [ %54, %124 ]
  %18 = phi i32 [ %13, %12 ], [ %127, %124 ]
  %19 = phi i32 [ 0, %12 ], [ %126, %124 ]
  %20 = phi i32 [ 0, %12 ], [ %125, %124 ]
  call void @llvm.dbg.value(metadata i32 %17, metadata !202, metadata !DIExpression()), !dbg !218
  call void @llvm.dbg.value(metadata i32 %18, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %19, metadata !194, metadata !DIExpression()), !dbg !219
  call void @llvm.dbg.value(metadata i32 %20, metadata !195, metadata !DIExpression()), !dbg !220
  %21 = sext i32 %20 to i64, !dbg !223
  %22 = getelementptr inbounds i32, i32* %10, i64 %21, !dbg !223
  %23 = load i32, i32* %22, align 4, !dbg !223, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %23, metadata !182, metadata !DIExpression()), !dbg !203
  br i1 %15, label %31, label %24, !dbg !225

; <label>:24:                                     ; preds = %16
  %25 = sext i32 %23 to i64, !dbg !226
  %26 = getelementptr inbounds i32, i32* %3, i64 %25, !dbg !226
  %27 = load i32, i32* %26, align 4, !dbg !226, !tbaa !58
  %28 = icmp slt i32 %27, -1, !dbg !226
  %29 = sub i32 -2, %27, !dbg !226
  %30 = select i1 %28, i32 %29, i32 %27, !dbg !226
  br label %31, !dbg !226

; <label>:31:                                     ; preds = %16, %24
  %32 = phi i32 [ %30, %24 ], [ %23, %16 ], !dbg !225
  call void @llvm.dbg.value(metadata i32 %32, metadata !199, metadata !DIExpression()), !dbg !227
  %33 = add nsw i32 %32, 1, !dbg !228
  %34 = sext i32 %33 to i64, !dbg !229
  %35 = getelementptr inbounds i32, i32* %1, i64 %34, !dbg !229
  %36 = load i32, i32* %35, align 4, !dbg !229, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %36, metadata !198, metadata !DIExpression()), !dbg !230
  %37 = sext i32 %23 to i64, !dbg !231
  %38 = getelementptr inbounds i32, i32* %5, i64 %37, !dbg !231
  %39 = load i32, i32* %38, align 4, !dbg !231, !tbaa !58
  %40 = icmp eq i32 %39, -2, !dbg !233
  br i1 %40, label %41, label %52, !dbg !234

; <label>:41:                                     ; preds = %31
  %42 = add nsw i32 %19, 1, !dbg !235
  call void @llvm.dbg.value(metadata i32 %42, metadata !194, metadata !DIExpression()), !dbg !219
  %43 = sext i32 %42 to i64, !dbg !237
  %44 = getelementptr inbounds i32, i32* %9, i64 %43, !dbg !237
  store i32 %23, i32* %44, align 4, !dbg !238, !tbaa !58
  %45 = add nsw i32 %17, 1, !dbg !239
  call void @llvm.dbg.value(metadata i32 %45, metadata !202, metadata !DIExpression()), !dbg !218
  %46 = getelementptr inbounds i32, i32* %4, i64 %37, !dbg !240
  store i32 %45, i32* %46, align 4, !dbg !241, !tbaa !58
  %47 = getelementptr inbounds i32, i32* %6, i64 %37, !dbg !242
  store i32 %45, i32* %47, align 4, !dbg !243, !tbaa !58
  store i32 -1, i32* %38, align 4, !dbg !244, !tbaa !58
  %48 = sext i32 %32 to i64, !dbg !245
  %49 = getelementptr inbounds i32, i32* %1, i64 %48, !dbg !245
  %50 = load i32, i32* %49, align 4, !dbg !245, !tbaa !58
  %51 = getelementptr inbounds i32, i32* %11, i64 %21, !dbg !246
  store i32 %50, i32* %51, align 4, !dbg !247, !tbaa !58
  br label %52, !dbg !248

; <label>:52:                                     ; preds = %41, %31
  %53 = phi i32 [ %42, %41 ], [ %19, %31 ], !dbg !249
  %54 = phi i32 [ %45, %41 ], [ %17, %31 ], !dbg !249
  call void @llvm.dbg.value(metadata i32 %53, metadata !194, metadata !DIExpression()), !dbg !219
  %55 = getelementptr inbounds i32, i32* %11, i64 %21, !dbg !250
  %56 = load i32, i32* %55, align 4, !dbg !250, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %56, metadata !200, metadata !DIExpression()), !dbg !252
  %57 = icmp slt i32 %56, %36, !dbg !253
  br i1 %57, label %58, label %86, !dbg !255

; <label>:58:                                     ; preds = %52
  %59 = getelementptr inbounds i32, i32* %6, i64 %37
  %60 = sext i32 %56 to i64, !dbg !255
  %61 = sext i32 %36 to i64, !dbg !255
  br label %62, !dbg !255

; <label>:62:                                     ; preds = %58, %81
  %63 = phi i64 [ %60, %58 ], [ %82, %81 ]
  call void @llvm.dbg.value(metadata i64 %63, metadata !200, metadata !DIExpression()), !dbg !252
  %64 = getelementptr inbounds i32, i32* %2, i64 %63, !dbg !256
  %65 = load i32, i32* %64, align 4, !dbg !256, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %65, metadata !196, metadata !DIExpression()), !dbg !258
  %66 = sext i32 %65 to i64, !dbg !259
  %67 = getelementptr inbounds i32, i32* %5, i64 %66, !dbg !259
  %68 = load i32, i32* %67, align 4, !dbg !259, !tbaa !58
  switch i32 %68, label %81 [
    i32 -2, label %69
    i32 -1, label %75
  ], !dbg !261

; <label>:69:                                     ; preds = %62
  call void @llvm.dbg.value(metadata i64 %63, metadata !200, metadata !DIExpression()), !dbg !252
  call void @llvm.dbg.value(metadata i64 %63, metadata !200, metadata !DIExpression()), !dbg !252
  call void @llvm.dbg.value(metadata i64 %63, metadata !200, metadata !DIExpression()), !dbg !252
  %70 = trunc i64 %63 to i32, !dbg !252
  call void @llvm.dbg.value(metadata i32 %70, metadata !200, metadata !DIExpression()), !dbg !252
  call void @llvm.dbg.value(metadata i64 %63, metadata !200, metadata !DIExpression()), !dbg !252
  %71 = add nsw i32 %70, 1, !dbg !262
  store i32 %71, i32* %55, align 4, !dbg !264, !tbaa !58
  %72 = add nsw i32 %20, 1, !dbg !265
  call void @llvm.dbg.value(metadata i32 %72, metadata !195, metadata !DIExpression()), !dbg !220
  %73 = sext i32 %72 to i64, !dbg !266
  %74 = getelementptr inbounds i32, i32* %10, i64 %73, !dbg !266
  store i32 %65, i32* %74, align 4, !dbg !267, !tbaa !58
  br label %86, !dbg !268

; <label>:75:                                     ; preds = %62
  %76 = load i32, i32* %59, align 4, !dbg !269, !tbaa !58
  %77 = getelementptr inbounds i32, i32* %4, i64 %66, !dbg !269
  %78 = load i32, i32* %77, align 4, !dbg !269, !tbaa !58
  %79 = icmp slt i32 %76, %78, !dbg !269
  %80 = select i1 %79, i32 %76, i32 %78, !dbg !269
  store i32 %80, i32* %59, align 4, !dbg !272, !tbaa !58
  br label %81, !dbg !273

; <label>:81:                                     ; preds = %62, %75
  %82 = add nsw i64 %63, 1, !dbg !274
  call void @llvm.dbg.value(metadata i32 undef, metadata !200, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !252
  %83 = icmp slt i64 %82, %61, !dbg !253
  br i1 %83, label %62, label %84, !dbg !255, !llvm.loop !275

; <label>:84:                                     ; preds = %81
  %85 = trunc i64 %82 to i32, !dbg !277
  br label %86, !dbg !277

; <label>:86:                                     ; preds = %84, %52, %69
  %87 = phi i32 [ %70, %69 ], [ %56, %52 ], [ %85, %84 ]
  %88 = phi i32 [ %72, %69 ], [ %20, %52 ], [ %20, %84 ], !dbg !249
  call void @llvm.dbg.value(metadata i32 %88, metadata !195, metadata !DIExpression()), !dbg !220
  %89 = icmp eq i32 %87, %36, !dbg !277
  br i1 %89, label %90, label %124, !dbg !279

; <label>:90:                                     ; preds = %86
  %91 = add nsw i32 %88, -1, !dbg !280
  call void @llvm.dbg.value(metadata i32 %91, metadata !195, metadata !DIExpression()), !dbg !220
  %92 = getelementptr inbounds i32, i32* %6, i64 %37, !dbg !282
  %93 = load i32, i32* %92, align 4, !dbg !282, !tbaa !58
  %94 = getelementptr inbounds i32, i32* %4, i64 %37, !dbg !284
  %95 = load i32, i32* %94, align 4, !dbg !284, !tbaa !58
  %96 = icmp eq i32 %93, %95, !dbg !285
  br i1 %96, label %97, label %110, !dbg !286

; <label>:97:                                     ; preds = %90
  %98 = sext i32 %53 to i64, !dbg !287
  br label %99, !dbg !287

; <label>:99:                                     ; preds = %99, %97
  %100 = phi i64 [ %101, %99 ], [ %98, %97 ], !dbg !289
  call void @llvm.dbg.value(metadata i64 %100, metadata !194, metadata !DIExpression()), !dbg !219
  %101 = add i64 %100, -1, !dbg !291
  call void @llvm.dbg.value(metadata i32 undef, metadata !194, metadata !DIExpression(DW_OP_constu, 1, DW_OP_minus, DW_OP_stack_value)), !dbg !219
  %102 = getelementptr inbounds i32, i32* %9, i64 %100, !dbg !292
  %103 = load i32, i32* %102, align 4, !dbg !292, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %103, metadata !196, metadata !DIExpression()), !dbg !258
  %104 = sext i32 %103 to i64, !dbg !293
  %105 = getelementptr inbounds i32, i32* %5, i64 %104, !dbg !293
  store i32 %18, i32* %105, align 4, !dbg !294, !tbaa !58
  %106 = icmp eq i32 %103, %23, !dbg !295
  br i1 %106, label %107, label %99, !dbg !297, !llvm.loop !298

; <label>:107:                                    ; preds = %99
  %108 = trunc i64 %101 to i32, !dbg !300
  %109 = add nsw i32 %18, 1, !dbg !300
  call void @llvm.dbg.value(metadata i32 %109, metadata !201, metadata !DIExpression()), !dbg !216
  br label %110, !dbg !301

; <label>:110:                                    ; preds = %107, %90
  %111 = phi i32 [ %108, %107 ], [ %53, %90 ], !dbg !302
  %112 = phi i32 [ %109, %107 ], [ %18, %90 ], !dbg !249
  call void @llvm.dbg.value(metadata i32 %112, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %111, metadata !194, metadata !DIExpression()), !dbg !219
  %113 = icmp sgt i32 %88, 0, !dbg !303
  br i1 %113, label %114, label %124, !dbg !305

; <label>:114:                                    ; preds = %110
  %115 = sext i32 %91 to i64, !dbg !306
  %116 = getelementptr inbounds i32, i32* %10, i64 %115, !dbg !306
  %117 = load i32, i32* %116, align 4, !dbg !306, !tbaa !58
  call void @llvm.dbg.value(metadata i32 %117, metadata !197, metadata !DIExpression()), !dbg !308
  %118 = sext i32 %117 to i64, !dbg !309
  %119 = getelementptr inbounds i32, i32* %6, i64 %118, !dbg !309
  %120 = load i32, i32* %119, align 4, !dbg !309, !tbaa !58
  %121 = load i32, i32* %92, align 4, !dbg !309, !tbaa !58
  %122 = icmp slt i32 %120, %121, !dbg !309
  %123 = select i1 %122, i32 %120, i32 %121, !dbg !309
  store i32 %123, i32* %119, align 4, !dbg !310, !tbaa !58
  br label %124, !dbg !311

; <label>:124:                                    ; preds = %110, %114, %86
  %125 = phi i32 [ %91, %114 ], [ %91, %110 ], [ %88, %86 ], !dbg !312
  %126 = phi i32 [ %111, %114 ], [ %111, %110 ], [ %53, %86 ], !dbg !302
  %127 = phi i32 [ %112, %114 ], [ %112, %110 ], [ %18, %86 ], !dbg !249
  call void @llvm.dbg.value(metadata i32 %54, metadata !202, metadata !DIExpression()), !dbg !218
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %126, metadata !194, metadata !DIExpression()), !dbg !219
  call void @llvm.dbg.value(metadata i32 %125, metadata !195, metadata !DIExpression()), !dbg !220
  %128 = icmp sgt i32 %125, -1, !dbg !313
  br i1 %128, label %16, label %129, !dbg !222, !llvm.loop !314

; <label>:129:                                    ; preds = %124
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %127, metadata !201, metadata !DIExpression()), !dbg !216
  call void @llvm.dbg.value(metadata i32 %54, metadata !202, metadata !DIExpression()), !dbg !218
  store i32 %54, i32* %8, align 4, !dbg !316, !tbaa !58
  store i32 %127, i32* %7, align 4, !dbg !317, !tbaa !58
  ret void, !dbg !318
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #2

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #1

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { argmemonly nounwind }
attributes #2 = { nounwind readnone speculatable }
attributes #3 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!7, !8, !9, !10}
!llvm.ident = !{!11}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "btf_strongcomp.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !6}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !5, size: 64)
!5 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!6 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!7 = !{i32 2, !"Dwarf Version", i32 4}
!8 = !{i32 2, !"Debug Info Version", i32 3}
!9 = !{i32 1, !"wchar_size", i32 4}
!10 = !{i32 7, !"PIC Level", i32 2}
!11 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!12 = distinct !DISubprogram(name: "btf_strongcomp", scope: !1, file: !1, line: 323, type: !13, isLocal: false, isDefinition: true, scopeLine: 362, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !15)
!13 = !DISubroutineType(types: !14)
!14 = !{!5, !5, !4, !4, !4, !4, !4, !4}
!15 = !{!16, !17, !18, !19, !20, !21, !22, !23, !24, !25, !26, !27, !28, !29, !30, !31, !32, !33}
!16 = !DILocalVariable(name: "n", arg: 1, scope: !12, file: !1, line: 326, type: !5)
!17 = !DILocalVariable(name: "Ap", arg: 2, scope: !12, file: !1, line: 327, type: !4)
!18 = !DILocalVariable(name: "Ai", arg: 3, scope: !12, file: !1, line: 328, type: !4)
!19 = !DILocalVariable(name: "Q", arg: 4, scope: !12, file: !1, line: 331, type: !4)
!20 = !DILocalVariable(name: "P", arg: 5, scope: !12, file: !1, line: 338, type: !4)
!21 = !DILocalVariable(name: "R", arg: 6, scope: !12, file: !1, line: 340, type: !4)
!22 = !DILocalVariable(name: "Work", arg: 7, scope: !12, file: !1, line: 344, type: !4)
!23 = !DILocalVariable(name: "j", scope: !12, file: !1, line: 363, type: !5)
!24 = !DILocalVariable(name: "k", scope: !12, file: !1, line: 363, type: !5)
!25 = !DILocalVariable(name: "b", scope: !12, file: !1, line: 363, type: !5)
!26 = !DILocalVariable(name: "timestamp", scope: !12, file: !1, line: 366, type: !5)
!27 = !DILocalVariable(name: "nblocks", scope: !12, file: !1, line: 366, type: !5)
!28 = !DILocalVariable(name: "Flag", scope: !12, file: !1, line: 366, type: !4)
!29 = !DILocalVariable(name: "Cstack", scope: !12, file: !1, line: 366, type: !4)
!30 = !DILocalVariable(name: "Time", scope: !12, file: !1, line: 366, type: !4)
!31 = !DILocalVariable(name: "Low", scope: !12, file: !1, line: 366, type: !4)
!32 = !DILocalVariable(name: "Jstack", scope: !12, file: !1, line: 366, type: !4)
!33 = !DILocalVariable(name: "Pstack", scope: !12, file: !1, line: 366, type: !4)
!34 = !DILocation(line: 326, column: 9, scope: !12)
!35 = !DILocation(line: 327, column: 9, scope: !12)
!36 = !DILocation(line: 328, column: 9, scope: !12)
!37 = !DILocation(line: 331, column: 9, scope: !12)
!38 = !DILocation(line: 338, column: 9, scope: !12)
!39 = !DILocation(line: 340, column: 9, scope: !12)
!40 = !DILocation(line: 344, column: 9, scope: !12)
!41 = !DILocation(line: 366, column: 5, scope: !12)
!42 = !DILocation(line: 366, column: 46, scope: !12)
!43 = !DILocation(line: 404, column: 26, scope: !12)
!44 = !DILocation(line: 366, column: 30, scope: !12)
!45 = !DILocation(line: 405, column: 26, scope: !12)
!46 = !DILocation(line: 366, column: 53, scope: !12)
!47 = !DILocation(line: 366, column: 37, scope: !12)
!48 = !DILocation(line: 366, column: 59, scope: !12)
!49 = !DILocation(line: 411, column: 26, scope: !12)
!50 = !DILocation(line: 366, column: 68, scope: !12)
!51 = !DILocation(line: 363, column: 9, scope: !12)
!52 = !DILocation(line: 415, column: 20, scope: !53)
!53 = distinct !DILexicalBlock(scope: !54, file: !1, line: 415, column: 5)
!54 = distinct !DILexicalBlock(scope: !12, file: !1, line: 415, column: 5)
!55 = !DILocation(line: 415, column: 5, scope: !54)
!56 = !DILocation(line: 366, column: 9, scope: !12)
!57 = !DILocation(line: 429, column: 15, scope: !12)
!58 = !{!59, !59, i64 0}
!59 = !{!"int", !60, i64 0}
!60 = !{!"omnipotent char", !61, i64 0}
!61 = !{!"Simple C/C++ TBAA"}
!62 = !DILocation(line: 366, column: 20, scope: !12)
!63 = !DILocation(line: 430, column: 13, scope: !12)
!64 = !DILocation(line: 436, column: 5, scope: !65)
!65 = distinct !DILexicalBlock(scope: !12, file: !1, line: 436, column: 5)
!66 = !DILocation(line: 417, column: 9, scope: !67)
!67 = distinct !DILexicalBlock(scope: !53, file: !1, line: 416, column: 5)
!68 = !DILocation(line: 417, column: 18, scope: !67)
!69 = !DILocation(line: 418, column: 9, scope: !67)
!70 = !DILocation(line: 418, column: 17, scope: !67)
!71 = !DILocation(line: 419, column: 9, scope: !67)
!72 = !DILocation(line: 419, column: 18, scope: !67)
!73 = !DILocation(line: 415, column: 27, scope: !53)
!74 = distinct !{!74, !55, !75}
!75 = !DILocation(line: 427, column: 5, scope: !54)
!76 = !DILocation(line: 436, column: 20, scope: !77)
!77 = distinct !DILexicalBlock(scope: !65, file: !1, line: 436, column: 5)
!78 = !DILocation(line: 440, column: 13, scope: !79)
!79 = distinct !DILexicalBlock(scope: !80, file: !1, line: 440, column: 13)
!80 = distinct !DILexicalBlock(scope: !77, file: !1, line: 437, column: 5)
!81 = !DILocation(line: 440, column: 22, scope: !79)
!82 = !DILocation(line: 440, column: 13, scope: !80)
!83 = !DILocation(line: 444, column: 13, scope: !84)
!84 = distinct !DILexicalBlock(scope: !79, file: !1, line: 441, column: 9)
!85 = !DILocation(line: 452, column: 9, scope: !84)
!86 = !DILocation(line: 436, column: 27, scope: !77)
!87 = distinct !{!87, !64, !88}
!88 = !DILocation(line: 453, column: 5, scope: !65)
!89 = !DILocation(line: 363, column: 15, scope: !12)
!90 = !DILocation(line: 474, column: 14, scope: !12)
!91 = !DILocation(line: 483, column: 17, scope: !12)
!92 = !DILocation(line: 496, column: 5, scope: !93)
!93 = distinct !DILexicalBlock(scope: !12, file: !1, line: 496, column: 5)
!94 = !DILocation(line: 460, column: 22, scope: !95)
!95 = distinct !DILexicalBlock(scope: !96, file: !1, line: 460, column: 5)
!96 = distinct !DILexicalBlock(scope: !12, file: !1, line: 460, column: 5)
!97 = !DILocation(line: 460, column: 20, scope: !95)
!98 = !DILocation(line: 460, column: 5, scope: !96)
!99 = !DILocation(line: 462, column: 15, scope: !100)
!100 = distinct !DILexicalBlock(scope: !95, file: !1, line: 461, column: 5)
!101 = !DILocation(line: 464, column: 20, scope: !102)
!102 = distinct !DILexicalBlock(scope: !103, file: !1, line: 464, column: 5)
!103 = distinct !DILexicalBlock(scope: !12, file: !1, line: 464, column: 5)
!104 = !DILocation(line: 464, column: 5, scope: !103)
!105 = !DILocation(line: 470, column: 12, scope: !106)
!106 = distinct !DILexicalBlock(scope: !102, file: !1, line: 465, column: 5)
!107 = !DILocation(line: 470, column: 9, scope: !106)
!108 = !DILocation(line: 470, column: 21, scope: !106)
!109 = !DILocation(line: 464, column: 27, scope: !102)
!110 = distinct !{!110, !104, !111}
!111 = !DILocation(line: 471, column: 5, scope: !103)
!112 = !DILocation(line: 475, column: 20, scope: !113)
!113 = distinct !DILexicalBlock(scope: !114, file: !1, line: 475, column: 5)
!114 = distinct !DILexicalBlock(scope: !12, file: !1, line: 475, column: 5)
!115 = !DILocation(line: 475, column: 5, scope: !114)
!116 = !DILocation(line: 477, column: 27, scope: !117)
!117 = distinct !DILexicalBlock(scope: !113, file: !1, line: 476, column: 5)
!118 = !DILocation(line: 477, column: 33, scope: !117)
!119 = !DILocation(line: 477, column: 31, scope: !117)
!120 = !DILocation(line: 477, column: 9, scope: !117)
!121 = !DILocation(line: 477, column: 18, scope: !117)
!122 = !DILocation(line: 475, column: 33, scope: !113)
!123 = distinct !{!123, !115, !124}
!124 = !DILocation(line: 478, column: 5, scope: !114)
!125 = !DILocation(line: 479, column: 20, scope: !126)
!126 = distinct !DILexicalBlock(scope: !127, file: !1, line: 479, column: 5)
!127 = distinct !DILexicalBlock(scope: !12, file: !1, line: 479, column: 5)
!128 = !DILocation(line: 479, column: 5, scope: !127)
!129 = !DILocation(line: 481, column: 17, scope: !130)
!130 = distinct !DILexicalBlock(scope: !126, file: !1, line: 480, column: 5)
!131 = !DILocation(line: 481, column: 9, scope: !130)
!132 = !DILocation(line: 481, column: 15, scope: !130)
!133 = !DILocation(line: 479, column: 33, scope: !126)
!134 = distinct !{!134, !128, !135}
!135 = !DILocation(line: 482, column: 5, scope: !127)
!136 = !DILocation(line: 483, column: 5, scope: !12)
!137 = !DILocation(line: 496, column: 20, scope: !138)
!138 = distinct !DILexicalBlock(scope: !93, file: !1, line: 496, column: 5)
!139 = !DILocation(line: 499, column: 18, scope: !140)
!140 = distinct !DILexicalBlock(scope: !138, file: !1, line: 497, column: 5)
!141 = !DILocation(line: 499, column: 12, scope: !140)
!142 = !DILocation(line: 499, column: 27, scope: !140)
!143 = !DILocation(line: 499, column: 9, scope: !140)
!144 = !DILocation(line: 499, column: 31, scope: !140)
!145 = !DILocation(line: 496, column: 27, scope: !138)
!146 = distinct !{!146, !92, !147}
!147 = !DILocation(line: 500, column: 5, scope: !93)
!148 = !DILocation(line: 520, column: 11, scope: !149)
!149 = distinct !DILexicalBlock(scope: !12, file: !1, line: 520, column: 9)
!150 = !DILocation(line: 527, column: 24, scope: !151)
!151 = distinct !DILexicalBlock(scope: !152, file: !1, line: 527, column: 9)
!152 = distinct !DILexicalBlock(scope: !153, file: !1, line: 527, column: 9)
!153 = distinct !DILexicalBlock(scope: !149, file: !1, line: 521, column: 5)
!154 = !DILocation(line: 520, column: 9, scope: !12)
!155 = !DILocation(line: 363, column: 12, scope: !12)
!156 = !DILocation(line: 527, column: 9, scope: !152)
!157 = !DILocation(line: 529, column: 27, scope: !158)
!158 = distinct !DILexicalBlock(scope: !151, file: !1, line: 528, column: 9)
!159 = !DILocation(line: 529, column: 24, scope: !158)
!160 = !DILocation(line: 529, column: 13, scope: !158)
!161 = !DILocation(line: 529, column: 22, scope: !158)
!162 = !DILocation(line: 527, column: 31, scope: !151)
!163 = distinct !{!163, !156, !164}
!164 = !DILocation(line: 530, column: 9, scope: !152)
!165 = !DILocation(line: 531, column: 24, scope: !166)
!166 = distinct !DILexicalBlock(scope: !167, file: !1, line: 531, column: 9)
!167 = distinct !DILexicalBlock(scope: !153, file: !1, line: 531, column: 9)
!168 = !DILocation(line: 531, column: 9, scope: !167)
!169 = !DILocation(line: 533, column: 21, scope: !170)
!170 = distinct !DILexicalBlock(scope: !166, file: !1, line: 532, column: 9)
!171 = !DILocation(line: 533, column: 13, scope: !170)
!172 = !DILocation(line: 533, column: 19, scope: !170)
!173 = !DILocation(line: 531, column: 31, scope: !166)
!174 = distinct !{!174, !168, !175}
!175 = !DILocation(line: 534, column: 9, scope: !167)
!176 = !DILocation(line: 593, column: 1, scope: !12)
!177 = !DILocation(line: 592, column: 5, scope: !12)
!178 = distinct !DISubprogram(name: "dfs", scope: !1, file: !1, line: 58, type: !179, isLocal: true, isDefinition: true, scopeLine: 78, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !181)
!179 = !DISubroutineType(types: !180)
!180 = !{null, !5, !4, !4, !4, !4, !4, !4, !4, !4, !4, !4, !4}
!181 = !{!182, !183, !184, !185, !186, !187, !188, !189, !190, !191, !192, !193, !194, !195, !196, !197, !198, !199, !200, !201, !202}
!182 = !DILocalVariable(name: "j", arg: 1, scope: !178, file: !1, line: 61, type: !5)
!183 = !DILocalVariable(name: "Ap", arg: 2, scope: !178, file: !1, line: 62, type: !4)
!184 = !DILocalVariable(name: "Ai", arg: 3, scope: !178, file: !1, line: 63, type: !4)
!185 = !DILocalVariable(name: "Q", arg: 4, scope: !178, file: !1, line: 64, type: !4)
!186 = !DILocalVariable(name: "Time", arg: 5, scope: !178, file: !1, line: 67, type: !4)
!187 = !DILocalVariable(name: "Flag", arg: 6, scope: !178, file: !1, line: 68, type: !4)
!188 = !DILocalVariable(name: "Low", arg: 7, scope: !178, file: !1, line: 69, type: !4)
!189 = !DILocalVariable(name: "p_nblocks", arg: 8, scope: !178, file: !1, line: 70, type: !4)
!190 = !DILocalVariable(name: "p_timestamp", arg: 9, scope: !178, file: !1, line: 71, type: !4)
!191 = !DILocalVariable(name: "Cstack", arg: 10, scope: !178, file: !1, line: 74, type: !4)
!192 = !DILocalVariable(name: "Jstack", arg: 11, scope: !178, file: !1, line: 75, type: !4)
!193 = !DILocalVariable(name: "Pstack", arg: 12, scope: !178, file: !1, line: 76, type: !4)
!194 = !DILocalVariable(name: "chead", scope: !178, file: !1, line: 84, type: !5)
!195 = !DILocalVariable(name: "jhead", scope: !178, file: !1, line: 85, type: !5)
!196 = !DILocalVariable(name: "i", scope: !178, file: !1, line: 88, type: !5)
!197 = !DILocalVariable(name: "parent", scope: !178, file: !1, line: 89, type: !5)
!198 = !DILocalVariable(name: "pend", scope: !178, file: !1, line: 90, type: !5)
!199 = !DILocalVariable(name: "jj", scope: !178, file: !1, line: 91, type: !5)
!200 = !DILocalVariable(name: "p", scope: !178, file: !1, line: 94, type: !5)
!201 = !DILocalVariable(name: "nblocks", scope: !178, file: !1, line: 98, type: !5)
!202 = !DILocalVariable(name: "timestamp", scope: !178, file: !1, line: 99, type: !5)
!203 = !DILocation(line: 61, column: 9, scope: !178)
!204 = !DILocation(line: 62, column: 9, scope: !178)
!205 = !DILocation(line: 63, column: 9, scope: !178)
!206 = !DILocation(line: 64, column: 9, scope: !178)
!207 = !DILocation(line: 67, column: 9, scope: !178)
!208 = !DILocation(line: 68, column: 9, scope: !178)
!209 = !DILocation(line: 69, column: 9, scope: !178)
!210 = !DILocation(line: 70, column: 10, scope: !178)
!211 = !DILocation(line: 71, column: 10, scope: !178)
!212 = !DILocation(line: 74, column: 9, scope: !178)
!213 = !DILocation(line: 75, column: 9, scope: !178)
!214 = !DILocation(line: 76, column: 9, scope: !178)
!215 = !DILocation(line: 98, column: 21, scope: !178)
!216 = !DILocation(line: 98, column: 9, scope: !178)
!217 = !DILocation(line: 99, column: 21, scope: !178)
!218 = !DILocation(line: 99, column: 9, scope: !178)
!219 = !DILocation(line: 84, column: 9, scope: !178)
!220 = !DILocation(line: 85, column: 9, scope: !178)
!221 = !DILocation(line: 107, column: 16, scope: !178)
!222 = !DILocation(line: 110, column: 5, scope: !178)
!223 = !DILocation(line: 112, column: 13, scope: !224)
!224 = distinct !DILexicalBlock(scope: !178, file: !1, line: 111, column: 5)
!225 = !DILocation(line: 115, column: 14, scope: !224)
!226 = !DILocation(line: 115, column: 43, scope: !224)
!227 = !DILocation(line: 91, column: 9, scope: !178)
!228 = !DILocation(line: 116, column: 22, scope: !224)
!229 = !DILocation(line: 116, column: 16, scope: !224)
!230 = !DILocation(line: 90, column: 9, scope: !178)
!231 = !DILocation(line: 118, column: 13, scope: !232)
!232 = distinct !DILexicalBlock(scope: !224, file: !1, line: 118, column: 13)
!233 = !DILocation(line: 118, column: 22, scope: !232)
!234 = !DILocation(line: 118, column: 13, scope: !224)
!235 = !DILocation(line: 126, column: 21, scope: !236)
!236 = distinct !DILexicalBlock(scope: !232, file: !1, line: 119, column: 9)
!237 = !DILocation(line: 126, column: 13, scope: !236)
!238 = !DILocation(line: 126, column: 30, scope: !236)
!239 = !DILocation(line: 127, column: 22, scope: !236)
!240 = !DILocation(line: 128, column: 13, scope: !236)
!241 = !DILocation(line: 128, column: 22, scope: !236)
!242 = !DILocation(line: 129, column: 13, scope: !236)
!243 = !DILocation(line: 129, column: 21, scope: !236)
!244 = !DILocation(line: 130, column: 22, scope: !236)
!245 = !DILocation(line: 136, column: 30, scope: !236)
!246 = !DILocation(line: 136, column: 13, scope: !236)
!247 = !DILocation(line: 136, column: 28, scope: !236)
!248 = !DILocation(line: 137, column: 9, scope: !236)
!249 = !DILocation(line: 0, scope: !178)
!250 = !DILocation(line: 143, column: 18, scope: !251)
!251 = distinct !DILexicalBlock(scope: !224, file: !1, line: 143, column: 9)
!252 = !DILocation(line: 94, column: 9, scope: !178)
!253 = !DILocation(line: 143, column: 37, scope: !254)
!254 = distinct !DILexicalBlock(scope: !251, file: !1, line: 143, column: 9)
!255 = !DILocation(line: 143, column: 9, scope: !251)
!256 = !DILocation(line: 145, column: 17, scope: !257)
!257 = distinct !DILexicalBlock(scope: !254, file: !1, line: 144, column: 9)
!258 = !DILocation(line: 88, column: 9, scope: !178)
!259 = !DILocation(line: 146, column: 17, scope: !260)
!260 = distinct !DILexicalBlock(scope: !257, file: !1, line: 146, column: 17)
!261 = !DILocation(line: 146, column: 17, scope: !257)
!262 = !DILocation(line: 151, column: 36, scope: !263)
!263 = distinct !DILexicalBlock(scope: !260, file: !1, line: 147, column: 13)
!264 = !DILocation(line: 151, column: 32, scope: !263)
!265 = !DILocation(line: 154, column: 25, scope: !263)
!266 = !DILocation(line: 154, column: 17, scope: !263)
!267 = !DILocation(line: 154, column: 34, scope: !263)
!268 = !DILocation(line: 158, column: 17, scope: !263)
!269 = !DILocation(line: 168, column: 27, scope: !270)
!270 = distinct !DILexicalBlock(scope: !271, file: !1, line: 161, column: 13)
!271 = distinct !DILexicalBlock(scope: !260, file: !1, line: 160, column: 22)
!272 = !DILocation(line: 168, column: 25, scope: !270)
!273 = !DILocation(line: 169, column: 13, scope: !270)
!274 = !DILocation(line: 143, column: 47, scope: !254)
!275 = distinct !{!275, !255, !276}
!276 = !DILocation(line: 170, column: 9, scope: !251)
!277 = !DILocation(line: 172, column: 15, scope: !278)
!278 = distinct !DILexicalBlock(scope: !224, file: !1, line: 172, column: 13)
!279 = !DILocation(line: 172, column: 13, scope: !224)
!280 = !DILocation(line: 177, column: 18, scope: !281)
!281 = distinct !DILexicalBlock(scope: !278, file: !1, line: 173, column: 9)
!282 = !DILocation(line: 184, column: 17, scope: !283)
!283 = distinct !DILexicalBlock(scope: !281, file: !1, line: 184, column: 17)
!284 = !DILocation(line: 184, column: 28, scope: !283)
!285 = !DILocation(line: 184, column: 25, scope: !283)
!286 = !DILocation(line: 184, column: 17, scope: !281)
!287 = !DILocation(line: 187, column: 17, scope: !288)
!288 = distinct !DILexicalBlock(scope: !283, file: !1, line: 185, column: 13)
!289 = !DILocation(line: 0, scope: !290)
!290 = distinct !DILexicalBlock(scope: !288, file: !1, line: 188, column: 17)
!291 = !DILocation(line: 190, column: 38, scope: !290)
!292 = !DILocation(line: 190, column: 25, scope: !290)
!293 = !DILocation(line: 193, column: 21, scope: !290)
!294 = !DILocation(line: 193, column: 30, scope: !290)
!295 = !DILocation(line: 194, column: 27, scope: !296)
!296 = distinct !DILexicalBlock(scope: !290, file: !1, line: 194, column: 25)
!297 = !DILocation(line: 194, column: 25, scope: !290)
!298 = distinct !{!298, !287, !299}
!299 = !DILocation(line: 195, column: 17, scope: !288)
!300 = !DILocation(line: 196, column: 24, scope: !288)
!301 = !DILocation(line: 197, column: 13, scope: !288)
!302 = !DILocation(line: 0, scope: !236)
!303 = !DILocation(line: 199, column: 23, scope: !304)
!304 = distinct !DILexicalBlock(scope: !281, file: !1, line: 199, column: 17)
!305 = !DILocation(line: 199, column: 17, scope: !281)
!306 = !DILocation(line: 201, column: 26, scope: !307)
!307 = distinct !DILexicalBlock(scope: !304, file: !1, line: 200, column: 13)
!308 = !DILocation(line: 89, column: 9, scope: !178)
!309 = !DILocation(line: 202, column: 32, scope: !307)
!310 = !DILocation(line: 202, column: 30, scope: !307)
!311 = !DILocation(line: 203, column: 13, scope: !307)
!312 = !DILocation(line: 0, scope: !263)
!313 = !DILocation(line: 110, column: 18, scope: !178)
!314 = distinct !{!314, !222, !315}
!315 = !DILocation(line: 205, column: 5, scope: !178)
!316 = !DILocation(line: 211, column: 18, scope: !178)
!317 = !DILocation(line: 212, column: 18, scope: !178)
!318 = !DILocation(line: 213, column: 1, scope: !178)
