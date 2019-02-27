; ModuleID = 'klu_defaults.c'
source_filename = "klu_defaults.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_defaults(%struct.klu_common_struct*) local_unnamed_addr #0 !dbg !10 {
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %0, metadata !53, metadata !DIExpression()), !dbg !54
  %2 = icmp eq %struct.klu_common_struct* %0, null, !dbg !55
  br i1 %2, label %26, label %3, !dbg !57

; <label>:3:                                      ; preds = %1
  %4 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 0, !dbg !58
  store double 1.000000e-03, double* %4, align 8, !dbg !59, !tbaa !60
  %5 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 1, !dbg !68
  store double 1.200000e+00, double* %5, align 8, !dbg !69, !tbaa !70
  %6 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 2, !dbg !71
  store double 1.200000e+00, double* %6, align 8, !dbg !72, !tbaa !73
  %7 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 3, !dbg !74
  store double 1.000000e+01, double* %7, align 8, !dbg !75, !tbaa !76
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 5, !dbg !77
  store i32 1, i32* %8, align 8, !dbg !78, !tbaa !79
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 4, !dbg !80
  store double 0.000000e+00, double* %9, align 8, !dbg !81, !tbaa !82
  %10 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 6, !dbg !83
  store i32 0, i32* %10, align 4, !dbg !84, !tbaa !85
  %11 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 7, !dbg !86
  store i32 2, i32* %11, align 8, !dbg !87, !tbaa !88
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 10, !dbg !89
  store i32 1, i32* %12, align 8, !dbg !90, !tbaa !91
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 8, !dbg !92
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 11, !dbg !93
  store i32 0, i32* %14, align 4, !dbg !94, !tbaa !95
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 12, !dbg !96
  store i32 0, i32* %15, align 8, !dbg !97, !tbaa !98
  %16 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 13, !dbg !99
  %17 = bitcast i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %13 to i8*, !dbg !100
  call void @llvm.memset.p0i8.i64(i8* nonnull %17, i8 0, i64 16, i32 8, i1 false), !dbg !101
  store i32 -1, i32* %16, align 4, !dbg !100, !tbaa !102
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 14, !dbg !103
  store i32 -1, i32* %18, align 8, !dbg !104, !tbaa !105
  %19 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 16, !dbg !106
  store i32 -1, i32* %19, align 8, !dbg !107, !tbaa !108
  %20 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 17, !dbg !109
  store double -1.000000e+00, double* %20, align 8, !dbg !110, !tbaa !111
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 18, !dbg !112
  store double -1.000000e+00, double* %21, align 8, !dbg !113, !tbaa !114
  %22 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 19, !dbg !115
  store double -1.000000e+00, double* %22, align 8, !dbg !116, !tbaa !117
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 20, !dbg !118
  store double -1.000000e+00, double* %23, align 8, !dbg !119, !tbaa !120
  %24 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 21, !dbg !121
  %25 = bitcast double* %24 to i8*, !dbg !122
  call void @llvm.memset.p0i8.i64(i8* nonnull %25, i8 0, i64 24, i32 8, i1 false), !dbg !123
  br label %26, !dbg !122

; <label>:26:                                     ; preds = %1, %3
  %27 = phi i32 [ 1, %3 ], [ 0, %1 ], !dbg !124
  ret i32 %27, !dbg !125
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind readnone speculatable }
attributes #2 = { argmemonly nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!5, !6, !7, !8}
!llvm.ident = !{!9}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_defaults.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !{i32 2, !"Dwarf Version", i32 4}
!6 = !{i32 2, !"Debug Info Version", i32 3}
!7 = !{i32 1, !"wchar_size", i32 4}
!8 = !{i32 7, !"PIC Level", i32 2}
!9 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!10 = distinct !DISubprogram(name: "klu_defaults", scope: !1, file: !1, line: 9, type: !11, isLocal: false, isDefinition: true, scopeLine: 13, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !52)
!11 = !DISubroutineType(types: !12)
!12 = !{!13, !14}
!13 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!14 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !15, size: 64)
!15 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !16, line: 207, baseType: !17)
!16 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!17 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !16, line: 137, size: 1280, elements: !18)
!18 = !{!19, !21, !22, !23, !24, !25, !26, !27, !28, !34, !35, !36, !37, !38, !39, !40, !41, !42, !43, !44, !45, !46, !47, !51}
!19 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !17, file: !16, line: 144, baseType: !20, size: 64)
!20 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!21 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !17, file: !16, line: 145, baseType: !20, size: 64, offset: 64)
!22 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !17, file: !16, line: 146, baseType: !20, size: 64, offset: 128)
!23 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !17, file: !16, line: 147, baseType: !20, size: 64, offset: 192)
!24 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !17, file: !16, line: 148, baseType: !20, size: 64, offset: 256)
!25 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !17, file: !16, line: 150, baseType: !13, size: 32, offset: 320)
!26 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !17, file: !16, line: 151, baseType: !13, size: 32, offset: 352)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !17, file: !16, line: 153, baseType: !13, size: 32, offset: 384)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !17, file: !16, line: 157, baseType: !29, size: 64, offset: 448)
!29 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !30, size: 64)
!30 = !DISubroutineType(types: !31)
!31 = !{!13, !13, !32, !32, !32, !33}
!32 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !13, size: 64)
!33 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !17, size: 64)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !17, file: !16, line: 162, baseType: !4, size: 64, offset: 512)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !17, file: !16, line: 164, baseType: !13, size: 32, offset: 576)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !17, file: !16, line: 177, baseType: !13, size: 32, offset: 608)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !17, file: !16, line: 178, baseType: !13, size: 32, offset: 640)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !17, file: !16, line: 180, baseType: !13, size: 32, offset: 672)
!39 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !17, file: !16, line: 185, baseType: !13, size: 32, offset: 704)
!40 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !17, file: !16, line: 191, baseType: !13, size: 32, offset: 736)
!41 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !17, file: !16, line: 196, baseType: !13, size: 32, offset: 768)
!42 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !17, file: !16, line: 198, baseType: !20, size: 64, offset: 832)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !17, file: !16, line: 199, baseType: !20, size: 64, offset: 896)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !17, file: !16, line: 200, baseType: !20, size: 64, offset: 960)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !17, file: !16, line: 201, baseType: !20, size: 64, offset: 1024)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !17, file: !16, line: 202, baseType: !20, size: 64, offset: 1088)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !17, file: !16, line: 204, baseType: !48, size: 64, offset: 1152)
!48 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !49, line: 62, baseType: !50)
!49 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!50 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !17, file: !16, line: 205, baseType: !48, size: 64, offset: 1216)
!52 = !{!53}
!53 = !DILocalVariable(name: "Common", arg: 1, scope: !10, file: !1, line: 11, type: !14)
!54 = !DILocation(line: 11, column: 17, scope: !10)
!55 = !DILocation(line: 14, column: 16, scope: !56)
!56 = distinct !DILexicalBlock(scope: !10, file: !1, line: 14, column: 9)
!57 = !DILocation(line: 14, column: 9, scope: !10)
!58 = !DILocation(line: 20, column: 13, scope: !10)
!59 = !DILocation(line: 20, column: 17, scope: !10)
!60 = !{!61, !62, i64 0}
!61 = !{!"klu_common_struct", !62, i64 0, !62, i64 8, !62, i64 16, !62, i64 24, !62, i64 32, !65, i64 40, !65, i64 44, !65, i64 48, !66, i64 56, !66, i64 64, !65, i64 72, !65, i64 76, !65, i64 80, !65, i64 84, !65, i64 88, !65, i64 92, !65, i64 96, !62, i64 104, !62, i64 112, !62, i64 120, !62, i64 128, !62, i64 136, !67, i64 144, !67, i64 152}
!62 = !{!"double", !63, i64 0}
!63 = !{!"omnipotent char", !64, i64 0}
!64 = !{!"Simple C/C++ TBAA"}
!65 = !{!"int", !63, i64 0}
!66 = !{!"any pointer", !63, i64 0}
!67 = !{!"long", !63, i64 0}
!68 = !DILocation(line: 21, column: 13, scope: !10)
!69 = !DILocation(line: 21, column: 21, scope: !10)
!70 = !{!61, !62, i64 8}
!71 = !DILocation(line: 22, column: 13, scope: !10)
!72 = !DILocation(line: 22, column: 25, scope: !10)
!73 = !{!61, !62, i64 16}
!74 = !DILocation(line: 23, column: 13, scope: !10)
!75 = !DILocation(line: 23, column: 21, scope: !10)
!76 = !{!61, !62, i64 24}
!77 = !DILocation(line: 24, column: 13, scope: !10)
!78 = !DILocation(line: 24, column: 17, scope: !10)
!79 = !{!61, !65, i64 40}
!80 = !DILocation(line: 25, column: 13, scope: !10)
!81 = !DILocation(line: 25, column: 21, scope: !10)
!82 = !{!61, !62, i64 32}
!83 = !DILocation(line: 26, column: 13, scope: !10)
!84 = !DILocation(line: 26, column: 22, scope: !10)
!85 = !{!61, !65, i64 44}
!86 = !DILocation(line: 28, column: 13, scope: !10)
!87 = !DILocation(line: 28, column: 19, scope: !10)
!88 = !{!61, !65, i64 48}
!89 = !DILocation(line: 32, column: 13, scope: !10)
!90 = !DILocation(line: 32, column: 30, scope: !10)
!91 = !{!61, !65, i64 72}
!92 = !DILocation(line: 35, column: 13, scope: !10)
!93 = !DILocation(line: 39, column: 13, scope: !10)
!94 = !DILocation(line: 39, column: 20, scope: !10)
!95 = !{!61, !65, i64 76}
!96 = !DILocation(line: 40, column: 13, scope: !10)
!97 = !DILocation(line: 40, column: 22, scope: !10)
!98 = !{!61, !65, i64 80}
!99 = !DILocation(line: 41, column: 13, scope: !10)
!100 = !DILocation(line: 41, column: 29, scope: !10)
!101 = !DILocation(line: 36, column: 23, scope: !10)
!102 = !{!61, !65, i64 84}
!103 = !DILocation(line: 42, column: 13, scope: !10)
!104 = !DILocation(line: 42, column: 28, scope: !10)
!105 = !{!61, !65, i64 88}
!106 = !DILocation(line: 43, column: 13, scope: !10)
!107 = !DILocation(line: 43, column: 22, scope: !10)
!108 = !{!61, !65, i64 96}
!109 = !DILocation(line: 44, column: 13, scope: !10)
!110 = !DILocation(line: 44, column: 19, scope: !10)
!111 = !{!61, !62, i64 104}
!112 = !DILocation(line: 45, column: 13, scope: !10)
!113 = !DILocation(line: 45, column: 19, scope: !10)
!114 = !{!61, !62, i64 112}
!115 = !DILocation(line: 46, column: 13, scope: !10)
!116 = !DILocation(line: 46, column: 21, scope: !10)
!117 = !{!61, !62, i64 120}
!118 = !DILocation(line: 47, column: 13, scope: !10)
!119 = !DILocation(line: 47, column: 21, scope: !10)
!120 = !{!61, !62, i64 128}
!121 = !DILocation(line: 48, column: 13, scope: !10)
!122 = !DILocation(line: 53, column: 5, scope: !10)
!123 = !DILocation(line: 50, column: 22, scope: !10)
!124 = !DILocation(line: 0, scope: !10)
!125 = !DILocation(line: 54, column: 1, scope: !10)
