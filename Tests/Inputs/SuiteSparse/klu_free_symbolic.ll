; ModuleID = 'klu_free_symbolic.c'
source_filename = "klu_free_symbolic.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_symbolic = type { double, double, double, double, double*, i32, i32, i32*, i32*, i32*, i32, i32, i32, i32, i32, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_free_symbolic(%struct.klu_symbolic**, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !10 {
  call void @llvm.dbg.value(metadata %struct.klu_symbolic** %0, metadata !75, metadata !DIExpression()), !dbg !79
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %1, metadata !76, metadata !DIExpression()), !dbg !80
  %3 = icmp eq %struct.klu_common_struct* %1, null, !dbg !81
  br i1 %3, label %33, label %4, !dbg !83

; <label>:4:                                      ; preds = %2
  %5 = icmp eq %struct.klu_symbolic** %0, null, !dbg !84
  br i1 %5, label %33, label %6, !dbg !86

; <label>:6:                                      ; preds = %4
  %7 = load %struct.klu_symbolic*, %struct.klu_symbolic** %0, align 8, !dbg !87, !tbaa !88
  %8 = icmp eq %struct.klu_symbolic* %7, null, !dbg !92
  br i1 %8, label %33, label %9, !dbg !93

; <label>:9:                                      ; preds = %6
  call void @llvm.dbg.value(metadata %struct.klu_symbolic* %7, metadata !77, metadata !DIExpression()), !dbg !94
  %10 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 5, !dbg !95
  %11 = load i32, i32* %10, align 8, !dbg !95, !tbaa !96
  call void @llvm.dbg.value(metadata i32 %11, metadata !78, metadata !DIExpression()), !dbg !100
  %12 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 7, !dbg !101
  %13 = bitcast i32** %12 to i8**, !dbg !101
  %14 = load i8*, i8** %13, align 8, !dbg !101, !tbaa !102
  %15 = sext i32 %11 to i64, !dbg !103
  %16 = tail call i8* @klu_free(i8* %14, i64 %15, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !104
  %17 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 8, !dbg !105
  %18 = bitcast i32** %17 to i8**, !dbg !105
  %19 = load i8*, i8** %18, align 8, !dbg !105, !tbaa !106
  %20 = tail call i8* @klu_free(i8* %19, i64 %15, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !107
  %21 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 9, !dbg !108
  %22 = bitcast i32** %21 to i8**, !dbg !108
  %23 = load i8*, i8** %22, align 8, !dbg !108, !tbaa !109
  %24 = add nsw i32 %11, 1, !dbg !110
  %25 = sext i32 %24 to i64, !dbg !111
  %26 = tail call i8* @klu_free(i8* %23, i64 %25, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !112
  %27 = getelementptr inbounds %struct.klu_symbolic, %struct.klu_symbolic* %7, i64 0, i32 4, !dbg !113
  %28 = bitcast double** %27 to i8**, !dbg !113
  %29 = load i8*, i8** %28, align 8, !dbg !113, !tbaa !114
  %30 = tail call i8* @klu_free(i8* %29, i64 %15, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !115
  %31 = bitcast %struct.klu_symbolic* %7 to i8*, !dbg !116
  %32 = tail call i8* @klu_free(i8* %31, i64 1, i64 96, %struct.klu_common_struct* nonnull %1) #3, !dbg !117
  store %struct.klu_symbolic* null, %struct.klu_symbolic** %0, align 8, !dbg !118, !tbaa !88
  br label %33, !dbg !119

; <label>:33:                                     ; preds = %4, %6, %2, %9
  %34 = phi i32 [ 1, %9 ], [ 0, %2 ], [ 1, %6 ], [ 1, %4 ], !dbg !120
  ret i32 %34, !dbg !121
}

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind readnone speculatable }
attributes #3 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!5, !6, !7, !8}
!llvm.ident = !{!9}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_free_symbolic.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !{i32 2, !"Dwarf Version", i32 4}
!6 = !{i32 2, !"Debug Info Version", i32 3}
!7 = !{i32 1, !"wchar_size", i32 4}
!8 = !{i32 7, !"PIC Level", i32 2}
!9 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!10 = distinct !DISubprogram(name: "klu_free_symbolic", scope: !1, file: !1, line: 9, type: !11, isLocal: false, isDefinition: true, scopeLine: 14, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !74)
!11 = !DISubroutineType(types: !12)
!12 = !{!13, !14, !39}
!13 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!14 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !15, size: 64)
!15 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !16, size: 64)
!16 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_symbolic", file: !17, line: 54, baseType: !18)
!17 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!18 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !17, line: 23, size: 768, elements: !19)
!19 = !{!20, !22, !23, !24, !25, !27, !28, !29, !31, !32, !33, !34, !35, !36, !37, !38}
!20 = !DIDerivedType(tag: DW_TAG_member, name: "symmetry", scope: !18, file: !17, line: 31, baseType: !21, size: 64)
!21 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!22 = !DIDerivedType(tag: DW_TAG_member, name: "est_flops", scope: !18, file: !17, line: 32, baseType: !21, size: 64, offset: 64)
!23 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !18, file: !17, line: 33, baseType: !21, size: 64, offset: 128)
!24 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !18, file: !17, line: 33, baseType: !21, size: 64, offset: 192)
!25 = !DIDerivedType(tag: DW_TAG_member, name: "Lnz", scope: !18, file: !17, line: 34, baseType: !26, size: 64, offset: 256)
!26 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !21, size: 64)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !18, file: !17, line: 38, baseType: !13, size: 32, offset: 320)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "nz", scope: !18, file: !17, line: 39, baseType: !13, size: 32, offset: 352)
!29 = !DIDerivedType(tag: DW_TAG_member, name: "P", scope: !18, file: !17, line: 40, baseType: !30, size: 64, offset: 384)
!30 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !13, size: 64)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "Q", scope: !18, file: !17, line: 41, baseType: !30, size: 64, offset: 448)
!32 = !DIDerivedType(tag: DW_TAG_member, name: "R", scope: !18, file: !17, line: 42, baseType: !30, size: 64, offset: 512)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !18, file: !17, line: 43, baseType: !13, size: 32, offset: 576)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !18, file: !17, line: 44, baseType: !13, size: 32, offset: 608)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "maxblock", scope: !18, file: !17, line: 45, baseType: !13, size: 32, offset: 640)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !18, file: !17, line: 46, baseType: !13, size: 32, offset: 672)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "do_btf", scope: !18, file: !17, line: 47, baseType: !13, size: 32, offset: 704)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !18, file: !17, line: 50, baseType: !13, size: 32, offset: 736)
!39 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !40, size: 64)
!40 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !17, line: 207, baseType: !41)
!41 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !17, line: 137, size: 1280, elements: !42)
!42 = !{!43, !44, !45, !46, !47, !48, !49, !50, !51, !56, !57, !58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68, !69, !73}
!43 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !41, file: !17, line: 144, baseType: !21, size: 64)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !41, file: !17, line: 145, baseType: !21, size: 64, offset: 64)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !41, file: !17, line: 146, baseType: !21, size: 64, offset: 128)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !41, file: !17, line: 147, baseType: !21, size: 64, offset: 192)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !41, file: !17, line: 148, baseType: !21, size: 64, offset: 256)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !41, file: !17, line: 150, baseType: !13, size: 32, offset: 320)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !41, file: !17, line: 151, baseType: !13, size: 32, offset: 352)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !41, file: !17, line: 153, baseType: !13, size: 32, offset: 384)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !41, file: !17, line: 157, baseType: !52, size: 64, offset: 448)
!52 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !53, size: 64)
!53 = !DISubroutineType(types: !54)
!54 = !{!13, !13, !30, !30, !30, !55}
!55 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !41, size: 64)
!56 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !41, file: !17, line: 162, baseType: !4, size: 64, offset: 512)
!57 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !41, file: !17, line: 164, baseType: !13, size: 32, offset: 576)
!58 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !41, file: !17, line: 177, baseType: !13, size: 32, offset: 608)
!59 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !41, file: !17, line: 178, baseType: !13, size: 32, offset: 640)
!60 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !41, file: !17, line: 180, baseType: !13, size: 32, offset: 672)
!61 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !41, file: !17, line: 185, baseType: !13, size: 32, offset: 704)
!62 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !41, file: !17, line: 191, baseType: !13, size: 32, offset: 736)
!63 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !41, file: !17, line: 196, baseType: !13, size: 32, offset: 768)
!64 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !41, file: !17, line: 198, baseType: !21, size: 64, offset: 832)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !41, file: !17, line: 199, baseType: !21, size: 64, offset: 896)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !41, file: !17, line: 200, baseType: !21, size: 64, offset: 960)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !41, file: !17, line: 201, baseType: !21, size: 64, offset: 1024)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !41, file: !17, line: 202, baseType: !21, size: 64, offset: 1088)
!69 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !41, file: !17, line: 204, baseType: !70, size: 64, offset: 1152)
!70 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !71, line: 62, baseType: !72)
!71 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!72 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!73 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !41, file: !17, line: 205, baseType: !70, size: 64, offset: 1216)
!74 = !{!75, !76, !77, !78}
!75 = !DILocalVariable(name: "SymbolicHandle", arg: 1, scope: !10, file: !1, line: 11, type: !14)
!76 = !DILocalVariable(name: "Common", arg: 2, scope: !10, file: !1, line: 12, type: !39)
!77 = !DILocalVariable(name: "Symbolic", scope: !10, file: !1, line: 15, type: !15)
!78 = !DILocalVariable(name: "n", scope: !10, file: !1, line: 16, type: !13)
!79 = !DILocation(line: 11, column: 20, scope: !10)
!80 = !DILocation(line: 12, column: 19, scope: !10)
!81 = !DILocation(line: 17, column: 16, scope: !82)
!82 = distinct !DILexicalBlock(scope: !10, file: !1, line: 17, column: 9)
!83 = !DILocation(line: 17, column: 9, scope: !10)
!84 = !DILocation(line: 21, column: 24, scope: !85)
!85 = distinct !DILexicalBlock(scope: !10, file: !1, line: 21, column: 9)
!86 = !DILocation(line: 21, column: 32, scope: !85)
!87 = !DILocation(line: 21, column: 35, scope: !85)
!88 = !{!89, !89, i64 0}
!89 = !{!"any pointer", !90, i64 0}
!90 = !{!"omnipotent char", !91, i64 0}
!91 = !{!"Simple C/C++ TBAA"}
!92 = !DILocation(line: 21, column: 51, scope: !85)
!93 = !DILocation(line: 21, column: 9, scope: !10)
!94 = !DILocation(line: 15, column: 19, scope: !10)
!95 = !DILocation(line: 26, column: 19, scope: !10)
!96 = !{!97, !99, i64 40}
!97 = !{!"", !98, i64 0, !98, i64 8, !98, i64 16, !98, i64 24, !89, i64 32, !99, i64 40, !99, i64 44, !89, i64 48, !89, i64 56, !89, i64 64, !99, i64 72, !99, i64 76, !99, i64 80, !99, i64 84, !99, i64 88, !99, i64 92}
!98 = !{!"double", !90, i64 0}
!99 = !{!"int", !90, i64 0}
!100 = !DILocation(line: 16, column: 9, scope: !10)
!101 = !DILocation(line: 27, column: 25, scope: !10)
!102 = !{!97, !89, i64 48}
!103 = !DILocation(line: 27, column: 28, scope: !10)
!104 = !DILocation(line: 27, column: 5, scope: !10)
!105 = !DILocation(line: 28, column: 25, scope: !10)
!106 = !{!97, !89, i64 56}
!107 = !DILocation(line: 28, column: 5, scope: !10)
!108 = !DILocation(line: 29, column: 25, scope: !10)
!109 = !{!97, !89, i64 64}
!110 = !DILocation(line: 29, column: 29, scope: !10)
!111 = !DILocation(line: 29, column: 28, scope: !10)
!112 = !DILocation(line: 29, column: 5, scope: !10)
!113 = !DILocation(line: 30, column: 25, scope: !10)
!114 = !{!97, !89, i64 32}
!115 = !DILocation(line: 30, column: 5, scope: !10)
!116 = !DILocation(line: 31, column: 15, scope: !10)
!117 = !DILocation(line: 31, column: 5, scope: !10)
!118 = !DILocation(line: 32, column: 21, scope: !10)
!119 = !DILocation(line: 33, column: 5, scope: !10)
!120 = !DILocation(line: 0, scope: !10)
!121 = !DILocation(line: 34, column: 1, scope: !10)
