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
  %13 = add i64 %1, %0, !dbg !37
  %14 = select i1 %11, i64 %13, i64 -1, !dbg !38
  ret i64 %14, !dbg !38
}

; Function Attrs: nounwind ssp uwtable
define i64 @klu_mult_size_t(i64, i64, i32* nocapture) local_unnamed_addr #0 !dbg !39 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !41, metadata !DIExpression()), !dbg !46
  call void @llvm.dbg.value(metadata i64 %1, metadata !42, metadata !DIExpression()), !dbg !47
  call void @llvm.dbg.value(metadata i32* %2, metadata !43, metadata !DIExpression()), !dbg !48
  call void @llvm.dbg.value(metadata i64 0, metadata !45, metadata !DIExpression()), !dbg !49
  call void @llvm.dbg.value(metadata i64 0, metadata !44, metadata !DIExpression()), !dbg !50
  %4 = icmp eq i64 %1, 0, !dbg !51
  br i1 %4, label %12, label %5, !dbg !54

; <label>:5:                                      ; preds = %3
  br label %6, !dbg !55

; <label>:6:                                      ; preds = %5, %6
  %7 = phi i64 [ %9, %6 ], [ 0, %5 ]
  %8 = phi i64 [ %10, %6 ], [ 0, %5 ]
  call void @llvm.dbg.value(metadata i64 %7, metadata !45, metadata !DIExpression()), !dbg !49
  call void @llvm.dbg.value(metadata i64 %8, metadata !44, metadata !DIExpression()), !dbg !50
  %9 = tail call i64 @klu_add_size_t(i64 %7, i64 %0, i32* %2), !dbg !55
  %10 = add nuw i64 %8, 1, !dbg !57
  call void @llvm.dbg.value(metadata i64 %9, metadata !45, metadata !DIExpression()), !dbg !49
  call void @llvm.dbg.value(metadata i64 %10, metadata !44, metadata !DIExpression()), !dbg !50
  %11 = icmp eq i64 %10, %1, !dbg !51
  br i1 %11, label %12, label %6, !dbg !54, !llvm.loop !58

; <label>:12:                                     ; preds = %6, %3
  %13 = phi i64 [ 0, %3 ], [ %9, %6 ]
  call void @llvm.dbg.value(metadata i64 %13, metadata !45, metadata !DIExpression()), !dbg !49
  %14 = load i32, i32* %2, align 4, !dbg !60, !tbaa !26
  %15 = icmp eq i32 %14, 0, !dbg !61
  %16 = select i1 %15, i64 -1, i64 %13, !dbg !61
  ret i64 %16, !dbg !62
}

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.start.p0i8(i64, i8* nocapture) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.lifetime.end.p0i8(i64, i8* nocapture) #1

; Function Attrs: nounwind ssp uwtable
define i8* @klu_malloc(i64, i64, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !63 {
  call void @llvm.dbg.value(metadata i64 %0, metadata !101, metadata !DIExpression()), !dbg !105
  call void @llvm.dbg.value(metadata i64 %1, metadata !102, metadata !DIExpression()), !dbg !106
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %2, metadata !103, metadata !DIExpression()), !dbg !107
  %4 = icmp eq %struct.klu_common_struct* %2, null, !dbg !108
  br i1 %4, label %29, label %5, !dbg !110

; <label>:5:                                      ; preds = %3
  %6 = icmp eq i64 %1, 0, !dbg !111
  br i1 %6, label %7, label %9, !dbg !113

; <label>:7:                                      ; preds = %5
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !114
  store i32 -3, i32* %8, align 4, !dbg !116, !tbaa !117
  call void @llvm.dbg.value(metadata i8* null, metadata !104, metadata !DIExpression()), !dbg !122
  br label %29, !dbg !123

; <label>:9:                                      ; preds = %5
  %10 = icmp ugt i64 %0, 2147483646, !dbg !124
  br i1 %10, label %11, label %13, !dbg !126

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !127
  store i32 -4, i32* %12, align 4, !dbg !129, !tbaa !117
  call void @llvm.dbg.value(metadata i8* null, metadata !104, metadata !DIExpression()), !dbg !122
  br label %29, !dbg !130

; <label>:13:                                     ; preds = %9
  %14 = tail call i8* @SuiteSparse_malloc(i64 %0, i64 %1) #4, !dbg !131
  call void @llvm.dbg.value(metadata i8* %14, metadata !104, metadata !DIExpression()), !dbg !122
  %15 = icmp eq i8* %14, null, !dbg !133
  br i1 %15, label %16, label %18, !dbg !135

; <label>:16:                                     ; preds = %13
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 11, !dbg !136
  store i32 -2, i32* %17, align 4, !dbg !138, !tbaa !117
  br label %29, !dbg !139

; <label>:18:                                     ; preds = %13
  %19 = icmp eq i64 %0, 0, !dbg !140
  %20 = select i1 %19, i64 1, i64 %0, !dbg !140
  %21 = mul i64 %20, %1, !dbg !142
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 22, !dbg !143
  %23 = load i64, i64* %22, align 8, !dbg !144, !tbaa !145
  %24 = add i64 %23, %21, !dbg !144
  store i64 %24, i64* %22, align 8, !dbg !144, !tbaa !145
  %25 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %2, i64 0, i32 23, !dbg !146
  %26 = load i64, i64* %25, align 8, !dbg !146, !tbaa !147
  %27 = icmp ugt i64 %26, %24, !dbg !146
  %28 = select i1 %27, i64 %26, i64 %24, !dbg !146
  store i64 %28, i64* %25, align 8, !dbg !148, !tbaa !147
  br label %29

; <label>:29:                                     ; preds = %3, %7, %16, %18, %11
  %30 = phi i8* [ null, %7 ], [ null, %11 ], [ null, %16 ], [ %14, %18 ], [ null, %3 ], !dbg !149
  call void @llvm.dbg.value(metadata i8* %30, metadata !104, metadata !DIExpression()), !dbg !122
  ret i8* %30, !dbg !150
}

declare i8* @SuiteSparse_malloc(i64, i64) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define noalias i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !151 {
  call void @llvm.dbg.value(metadata i8* %0, metadata !155, metadata !DIExpression()), !dbg !159
  call void @llvm.dbg.value(metadata i64 %1, metadata !156, metadata !DIExpression()), !dbg !160
  call void @llvm.dbg.value(metadata i64 %2, metadata !157, metadata !DIExpression()), !dbg !161
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %3, metadata !158, metadata !DIExpression()), !dbg !162
  %5 = icmp ne i8* %0, null, !dbg !163
  %6 = icmp ne %struct.klu_common_struct* %3, null, !dbg !165
  %7 = and i1 %5, %6, !dbg !166
  br i1 %7, label %8, label %16, !dbg !166

; <label>:8:                                      ; preds = %4
  %9 = tail call i8* @SuiteSparse_free(i8* nonnull %0) #4, !dbg !167
  %10 = icmp eq i64 %1, 0, !dbg !169
  %11 = select i1 %10, i64 1, i64 %1, !dbg !169
  %12 = mul i64 %11, %2, !dbg !170
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %3, i64 0, i32 22, !dbg !171
  %14 = load i64, i64* %13, align 8, !dbg !172, !tbaa !145
  %15 = sub i64 %14, %12, !dbg !172
  store i64 %15, i64* %13, align 8, !dbg !172, !tbaa !145
  br label %16, !dbg !173

; <label>:16:                                     ; preds = %8, %4
  ret i8* null, !dbg !174
}

declare i8* @SuiteSparse_free(i8*) local_unnamed_addr #2

; Function Attrs: nounwind ssp uwtable
define i8* @klu_realloc(i64, i64, i64, i8*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !175 {
  %6 = alloca i32, align 4
  call void @llvm.dbg.value(metadata i64 %0, metadata !179, metadata !DIExpression()), !dbg !186
  call void @llvm.dbg.value(metadata i64 %1, metadata !180, metadata !DIExpression()), !dbg !187
  call void @llvm.dbg.value(metadata i64 %2, metadata !181, metadata !DIExpression()), !dbg !188
  call void @llvm.dbg.value(metadata i8* %3, metadata !182, metadata !DIExpression()), !dbg !189
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %4, metadata !183, metadata !DIExpression()), !dbg !190
  %7 = bitcast i32* %6 to i8*, !dbg !191
  call void @llvm.lifetime.start.p0i8(i64 4, i8* nonnull %7) #4, !dbg !191
  call void @llvm.dbg.value(metadata i32 1, metadata !185, metadata !DIExpression()), !dbg !192
  store i32 1, i32* %6, align 4, !dbg !192, !tbaa !26
  %8 = icmp eq %struct.klu_common_struct* %4, null, !dbg !193
  br i1 %8, label %37, label %9, !dbg !195

; <label>:9:                                      ; preds = %5
  %10 = icmp eq i64 %2, 0, !dbg !196
  br i1 %10, label %11, label %13, !dbg !198

; <label>:11:                                     ; preds = %9
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !199
  store i32 -3, i32* %12, align 4, !dbg !201, !tbaa !117
  call void @llvm.dbg.value(metadata i8* null, metadata !182, metadata !DIExpression()), !dbg !189
  br label %37, !dbg !202

; <label>:13:                                     ; preds = %9
  %14 = icmp eq i8* %3, null, !dbg !203
  br i1 %14, label %15, label %17, !dbg !205

; <label>:15:                                     ; preds = %13
  %16 = tail call i8* @klu_malloc(i64 %0, i64 %2, %struct.klu_common_struct* nonnull %4), !dbg !206
  call void @llvm.dbg.value(metadata i8* %16, metadata !182, metadata !DIExpression()), !dbg !189
  br label %37, !dbg !208

; <label>:17:                                     ; preds = %13
  %18 = icmp ugt i64 %0, 2147483646, !dbg !209
  br i1 %18, label %19, label %21, !dbg !211

; <label>:19:                                     ; preds = %17
  %20 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !212
  store i32 -4, i32* %20, align 4, !dbg !214, !tbaa !117
  br label %37, !dbg !215

; <label>:21:                                     ; preds = %17
  call void @llvm.dbg.value(metadata i32* %6, metadata !185, metadata !DIExpression()), !dbg !192
  %22 = call i8* @SuiteSparse_realloc(i64 %0, i64 %1, i64 %2, i8* nonnull %3, i32* nonnull %6) #4, !dbg !216
  call void @llvm.dbg.value(metadata i8* %22, metadata !184, metadata !DIExpression()), !dbg !218
  %23 = load i32, i32* %6, align 4, !dbg !219, !tbaa !26
  call void @llvm.dbg.value(metadata i32 %23, metadata !185, metadata !DIExpression()), !dbg !192
  %24 = icmp eq i32 %23, 0, !dbg !219
  br i1 %24, label %35, label %25, !dbg !221

; <label>:25:                                     ; preds = %21
  %26 = sub i64 %0, %1, !dbg !222
  %27 = mul i64 %26, %2, !dbg !224
  %28 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 22, !dbg !225
  %29 = load i64, i64* %28, align 8, !dbg !226, !tbaa !145
  %30 = add i64 %29, %27, !dbg !226
  store i64 %30, i64* %28, align 8, !dbg !226, !tbaa !145
  %31 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 23, !dbg !227
  %32 = load i64, i64* %31, align 8, !dbg !227, !tbaa !147
  %33 = icmp ugt i64 %32, %30, !dbg !227
  %34 = select i1 %33, i64 %32, i64 %30, !dbg !227
  store i64 %34, i64* %31, align 8, !dbg !228, !tbaa !147
  call void @llvm.dbg.value(metadata i8* %22, metadata !182, metadata !DIExpression()), !dbg !189
  br label %37, !dbg !229

; <label>:35:                                     ; preds = %21
  %36 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %4, i64 0, i32 11, !dbg !230
  store i32 -2, i32* %36, align 4, !dbg !232, !tbaa !117
  br label %37

; <label>:37:                                     ; preds = %5, %11, %19, %35, %25, %15
  %38 = phi i8* [ null, %11 ], [ %16, %15 ], [ %3, %19 ], [ %22, %25 ], [ %3, %35 ], [ null, %5 ]
  call void @llvm.dbg.value(metadata i8* %38, metadata !182, metadata !DIExpression()), !dbg !189
  call void @llvm.lifetime.end.p0i8(i64 4, i8* nonnull %7) #4, !dbg !233
  ret i8* %38, !dbg !234
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
!37 = !DILocation(line: 23, column: 24, scope: !13)
!38 = !DILocation(line: 23, column: 13, scope: !13)
!39 = distinct !DISubprogram(name: "klu_mult_size_t", scope: !1, file: !1, line: 32, type: !14, isLocal: false, isDefinition: true, scopeLine: 33, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !40)
!40 = !{!41, !42, !43, !44, !45}
!41 = !DILocalVariable(name: "a", arg: 1, scope: !39, file: !1, line: 32, type: !4)
!42 = !DILocalVariable(name: "k", arg: 2, scope: !39, file: !1, line: 32, type: !4)
!43 = !DILocalVariable(name: "ok", arg: 3, scope: !39, file: !1, line: 32, type: !16)
!44 = !DILocalVariable(name: "i", scope: !39, file: !1, line: 34, type: !4)
!45 = !DILocalVariable(name: "s", scope: !39, file: !1, line: 34, type: !4)
!46 = !DILocation(line: 32, column: 32, scope: !39)
!47 = !DILocation(line: 32, column: 42, scope: !39)
!48 = !DILocation(line: 32, column: 50, scope: !39)
!49 = !DILocation(line: 34, column: 15, scope: !39)
!50 = !DILocation(line: 34, column: 12, scope: !39)
!51 = !DILocation(line: 35, column: 20, scope: !52)
!52 = distinct !DILexicalBlock(scope: !53, file: !1, line: 35, column: 5)
!53 = distinct !DILexicalBlock(scope: !39, file: !1, line: 35, column: 5)
!54 = !DILocation(line: 35, column: 5, scope: !53)
!55 = !DILocation(line: 37, column: 13, scope: !56)
!56 = distinct !DILexicalBlock(scope: !52, file: !1, line: 36, column: 5)
!57 = !DILocation(line: 35, column: 27, scope: !52)
!58 = distinct !{!58, !54, !59}
!59 = !DILocation(line: 38, column: 5, scope: !53)
!60 = !DILocation(line: 39, column: 14, scope: !39)
!61 = !DILocation(line: 39, column: 13, scope: !39)
!62 = !DILocation(line: 39, column: 5, scope: !39)
!63 = distinct !DISubprogram(name: "klu_malloc", scope: !1, file: !1, line: 60, type: !64, isLocal: false, isDefinition: true, scopeLine: 68, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !100)
!64 = !DISubroutineType(types: !65)
!65 = !{!7, !4, !4, !66}
!66 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !67, size: 64)
!67 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !68, line: 207, baseType: !69)
!68 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!69 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !68, line: 137, size: 1280, elements: !70)
!70 = !{!71, !73, !74, !75, !76, !77, !78, !79, !80, !85, !86, !87, !88, !89, !90, !91, !92, !93, !94, !95, !96, !97, !98, !99}
!71 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !69, file: !68, line: 144, baseType: !72, size: 64)
!72 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!73 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !69, file: !68, line: 145, baseType: !72, size: 64, offset: 64)
!74 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !69, file: !68, line: 146, baseType: !72, size: 64, offset: 128)
!75 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !69, file: !68, line: 147, baseType: !72, size: 64, offset: 192)
!76 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !69, file: !68, line: 148, baseType: !72, size: 64, offset: 256)
!77 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !69, file: !68, line: 150, baseType: !17, size: 32, offset: 320)
!78 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !69, file: !68, line: 151, baseType: !17, size: 32, offset: 352)
!79 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !69, file: !68, line: 153, baseType: !17, size: 32, offset: 384)
!80 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !69, file: !68, line: 157, baseType: !81, size: 64, offset: 448)
!81 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !82, size: 64)
!82 = !DISubroutineType(types: !83)
!83 = !{!17, !17, !16, !16, !16, !84}
!84 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !69, size: 64)
!85 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !69, file: !68, line: 162, baseType: !7, size: 64, offset: 512)
!86 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !69, file: !68, line: 164, baseType: !17, size: 32, offset: 576)
!87 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !69, file: !68, line: 177, baseType: !17, size: 32, offset: 608)
!88 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !69, file: !68, line: 178, baseType: !17, size: 32, offset: 640)
!89 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !69, file: !68, line: 180, baseType: !17, size: 32, offset: 672)
!90 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !69, file: !68, line: 185, baseType: !17, size: 32, offset: 704)
!91 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !69, file: !68, line: 191, baseType: !17, size: 32, offset: 736)
!92 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !69, file: !68, line: 196, baseType: !17, size: 32, offset: 768)
!93 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !69, file: !68, line: 198, baseType: !72, size: 64, offset: 832)
!94 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !69, file: !68, line: 199, baseType: !72, size: 64, offset: 896)
!95 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !69, file: !68, line: 200, baseType: !72, size: 64, offset: 960)
!96 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !69, file: !68, line: 201, baseType: !72, size: 64, offset: 1024)
!97 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !69, file: !68, line: 202, baseType: !72, size: 64, offset: 1088)
!98 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !69, file: !68, line: 204, baseType: !4, size: 64, offset: 1152)
!99 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !69, file: !68, line: 205, baseType: !4, size: 64, offset: 1216)
!100 = !{!101, !102, !103, !104}
!101 = !DILocalVariable(name: "n", arg: 1, scope: !63, file: !1, line: 63, type: !4)
!102 = !DILocalVariable(name: "size", arg: 2, scope: !63, file: !1, line: 64, type: !4)
!103 = !DILocalVariable(name: "Common", arg: 3, scope: !63, file: !1, line: 66, type: !66)
!104 = !DILocalVariable(name: "p", scope: !63, file: !1, line: 69, type: !7)
!105 = !DILocation(line: 63, column: 12, scope: !63)
!106 = !DILocation(line: 64, column: 12, scope: !63)
!107 = !DILocation(line: 66, column: 17, scope: !63)
!108 = !DILocation(line: 71, column: 16, scope: !109)
!109 = distinct !DILexicalBlock(scope: !63, file: !1, line: 71, column: 9)
!110 = !DILocation(line: 71, column: 9, scope: !63)
!111 = !DILocation(line: 75, column: 19, scope: !112)
!112 = distinct !DILexicalBlock(scope: !109, file: !1, line: 75, column: 14)
!113 = !DILocation(line: 75, column: 14, scope: !109)
!114 = !DILocation(line: 78, column: 17, scope: !115)
!115 = distinct !DILexicalBlock(scope: !112, file: !1, line: 76, column: 5)
!116 = !DILocation(line: 78, column: 24, scope: !115)
!117 = !{!118, !27, i64 76}
!118 = !{!"klu_common_struct", !119, i64 0, !119, i64 8, !119, i64 16, !119, i64 24, !119, i64 32, !27, i64 40, !27, i64 44, !27, i64 48, !120, i64 56, !120, i64 64, !27, i64 72, !27, i64 76, !27, i64 80, !27, i64 84, !27, i64 88, !27, i64 92, !27, i64 96, !119, i64 104, !119, i64 112, !119, i64 120, !119, i64 128, !119, i64 136, !121, i64 144, !121, i64 152}
!119 = !{!"double", !28, i64 0}
!120 = !{!"any pointer", !28, i64 0}
!121 = !{!"long", !28, i64 0}
!122 = !DILocation(line: 69, column: 11, scope: !63)
!123 = !DILocation(line: 80, column: 5, scope: !115)
!124 = !DILocation(line: 81, column: 16, scope: !125)
!125 = distinct !DILexicalBlock(scope: !112, file: !1, line: 81, column: 14)
!126 = !DILocation(line: 81, column: 14, scope: !112)
!127 = !DILocation(line: 85, column: 17, scope: !128)
!128 = distinct !DILexicalBlock(scope: !125, file: !1, line: 82, column: 5)
!129 = !DILocation(line: 85, column: 24, scope: !128)
!130 = !DILocation(line: 87, column: 5, scope: !128)
!131 = !DILocation(line: 91, column: 13, scope: !132)
!132 = distinct !DILexicalBlock(scope: !125, file: !1, line: 89, column: 5)
!133 = !DILocation(line: 92, column: 15, scope: !134)
!134 = distinct !DILexicalBlock(scope: !132, file: !1, line: 92, column: 13)
!135 = !DILocation(line: 92, column: 13, scope: !132)
!136 = !DILocation(line: 95, column: 21, scope: !137)
!137 = distinct !DILexicalBlock(scope: !134, file: !1, line: 93, column: 9)
!138 = !DILocation(line: 95, column: 28, scope: !137)
!139 = !DILocation(line: 96, column: 9, scope: !137)
!140 = !DILocation(line: 99, column: 34, scope: !141)
!141 = distinct !DILexicalBlock(scope: !134, file: !1, line: 98, column: 9)
!142 = !DILocation(line: 99, column: 44, scope: !141)
!143 = !DILocation(line: 99, column: 21, scope: !141)
!144 = !DILocation(line: 99, column: 30, scope: !141)
!145 = !{!118, !121, i64 144}
!146 = !DILocation(line: 100, column: 31, scope: !141)
!147 = !{!118, !121, i64 152}
!148 = !DILocation(line: 100, column: 29, scope: !141)
!149 = !DILocation(line: 0, scope: !132)
!150 = !DILocation(line: 103, column: 5, scope: !63)
!151 = distinct !DISubprogram(name: "klu_free", scope: !1, file: !1, line: 117, type: !152, isLocal: false, isDefinition: true, scopeLine: 127, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !154)
!152 = !DISubroutineType(types: !153)
!153 = !{!7, !7, !4, !4, !66}
!154 = !{!155, !156, !157, !158}
!155 = !DILocalVariable(name: "p", arg: 1, scope: !151, file: !1, line: 120, type: !7)
!156 = !DILocalVariable(name: "n", arg: 2, scope: !151, file: !1, line: 122, type: !4)
!157 = !DILocalVariable(name: "size", arg: 3, scope: !151, file: !1, line: 123, type: !4)
!158 = !DILocalVariable(name: "Common", arg: 4, scope: !151, file: !1, line: 125, type: !66)
!159 = !DILocation(line: 120, column: 11, scope: !151)
!160 = !DILocation(line: 122, column: 12, scope: !151)
!161 = !DILocation(line: 123, column: 12, scope: !151)
!162 = !DILocation(line: 125, column: 17, scope: !151)
!163 = !DILocation(line: 128, column: 11, scope: !164)
!164 = distinct !DILexicalBlock(scope: !151, file: !1, line: 128, column: 9)
!165 = !DILocation(line: 128, column: 29, scope: !164)
!166 = !DILocation(line: 128, column: 19, scope: !164)
!167 = !DILocation(line: 132, column: 9, scope: !168)
!168 = distinct !DILexicalBlock(scope: !164, file: !1, line: 129, column: 5)
!169 = !DILocation(line: 133, column: 30, scope: !168)
!170 = !DILocation(line: 133, column: 40, scope: !168)
!171 = !DILocation(line: 133, column: 17, scope: !168)
!172 = !DILocation(line: 133, column: 26, scope: !168)
!173 = !DILocation(line: 134, column: 5, scope: !168)
!174 = !DILocation(line: 137, column: 5, scope: !151)
!175 = distinct !DISubprogram(name: "klu_realloc", scope: !1, file: !1, line: 162, type: !176, isLocal: false, isDefinition: true, scopeLine: 173, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !178)
!176 = !DISubroutineType(types: !177)
!177 = !{!7, !4, !4, !4, !7, !66}
!178 = !{!179, !180, !181, !182, !183, !184, !185}
!179 = !DILocalVariable(name: "nnew", arg: 1, scope: !175, file: !1, line: 165, type: !4)
!180 = !DILocalVariable(name: "nold", arg: 2, scope: !175, file: !1, line: 166, type: !4)
!181 = !DILocalVariable(name: "size", arg: 3, scope: !175, file: !1, line: 167, type: !4)
!182 = !DILocalVariable(name: "p", arg: 4, scope: !175, file: !1, line: 169, type: !7)
!183 = !DILocalVariable(name: "Common", arg: 5, scope: !175, file: !1, line: 171, type: !66)
!184 = !DILocalVariable(name: "pnew", scope: !175, file: !1, line: 174, type: !7)
!185 = !DILocalVariable(name: "ok", scope: !175, file: !1, line: 175, type: !17)
!186 = !DILocation(line: 165, column: 12, scope: !175)
!187 = !DILocation(line: 166, column: 12, scope: !175)
!188 = !DILocation(line: 167, column: 12, scope: !175)
!189 = !DILocation(line: 169, column: 11, scope: !175)
!190 = !DILocation(line: 171, column: 17, scope: !175)
!191 = !DILocation(line: 175, column: 5, scope: !175)
!192 = !DILocation(line: 175, column: 9, scope: !175)
!193 = !DILocation(line: 177, column: 16, scope: !194)
!194 = distinct !DILexicalBlock(scope: !175, file: !1, line: 177, column: 9)
!195 = !DILocation(line: 177, column: 9, scope: !175)
!196 = !DILocation(line: 181, column: 19, scope: !197)
!197 = distinct !DILexicalBlock(scope: !194, file: !1, line: 181, column: 14)
!198 = !DILocation(line: 181, column: 14, scope: !194)
!199 = !DILocation(line: 184, column: 17, scope: !200)
!200 = distinct !DILexicalBlock(scope: !197, file: !1, line: 182, column: 5)
!201 = !DILocation(line: 184, column: 24, scope: !200)
!202 = !DILocation(line: 186, column: 5, scope: !200)
!203 = !DILocation(line: 187, column: 16, scope: !204)
!204 = distinct !DILexicalBlock(scope: !197, file: !1, line: 187, column: 14)
!205 = !DILocation(line: 187, column: 14, scope: !197)
!206 = !DILocation(line: 190, column: 13, scope: !207)
!207 = distinct !DILexicalBlock(scope: !204, file: !1, line: 188, column: 5)
!208 = !DILocation(line: 191, column: 5, scope: !207)
!209 = !DILocation(line: 192, column: 19, scope: !210)
!210 = distinct !DILexicalBlock(scope: !204, file: !1, line: 192, column: 14)
!211 = !DILocation(line: 192, column: 14, scope: !204)
!212 = !DILocation(line: 195, column: 17, scope: !213)
!213 = distinct !DILexicalBlock(scope: !210, file: !1, line: 193, column: 5)
!214 = !DILocation(line: 195, column: 24, scope: !213)
!215 = !DILocation(line: 196, column: 5, scope: !213)
!216 = !DILocation(line: 201, column: 16, scope: !217)
!217 = distinct !DILexicalBlock(scope: !210, file: !1, line: 198, column: 5)
!218 = !DILocation(line: 174, column: 11, scope: !175)
!219 = !DILocation(line: 202, column: 13, scope: !220)
!220 = distinct !DILexicalBlock(scope: !217, file: !1, line: 202, column: 13)
!221 = !DILocation(line: 202, column: 13, scope: !217)
!222 = !DILocation(line: 205, column: 39, scope: !223)
!223 = distinct !DILexicalBlock(scope: !220, file: !1, line: 203, column: 9)
!224 = !DILocation(line: 205, column: 46, scope: !223)
!225 = !DILocation(line: 205, column: 21, scope: !223)
!226 = !DILocation(line: 205, column: 30, scope: !223)
!227 = !DILocation(line: 206, column: 31, scope: !223)
!228 = !DILocation(line: 206, column: 29, scope: !223)
!229 = !DILocation(line: 208, column: 9, scope: !223)
!230 = !DILocation(line: 212, column: 21, scope: !231)
!231 = distinct !DILexicalBlock(scope: !220, file: !1, line: 210, column: 9)
!232 = !DILocation(line: 212, column: 28, scope: !231)
!233 = !DILocation(line: 216, column: 1, scope: !175)
!234 = !DILocation(line: 215, column: 5, scope: !175)
