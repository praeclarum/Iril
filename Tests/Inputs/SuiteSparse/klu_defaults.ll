; ModuleID = 'klu_defaults.c'
source_filename = "klu_defaults.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_defaults(%struct.klu_common_struct*) local_unnamed_addr #0 !dbg !10 {
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %0, metadata !53, metadata !DIExpression()), !dbg !54
  %2 = icmp eq %struct.klu_common_struct* %0, null, !dbg !55
  br i1 %2, label %25, label %3, !dbg !57

; <label>:3:                                      ; preds = %1
  %4 = bitcast %struct.klu_common_struct* %0 to <2 x double>*, !dbg !58
  store <2 x double> <double 1.000000e-03, double 1.200000e+00>, <2 x double>* %4, align 8, !dbg !58, !tbaa !59
  %5 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 2, !dbg !63
  %6 = bitcast double* %5 to <2 x double>*, !dbg !64
  store <2 x double> <double 1.200000e+00, double 1.000000e+01>, <2 x double>* %6, align 8, !dbg !64, !tbaa !59
  %7 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 5, !dbg !65
  store i32 1, i32* %7, align 8, !dbg !66, !tbaa !67
  %8 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 4, !dbg !72
  store double 0.000000e+00, double* %8, align 8, !dbg !73, !tbaa !74
  %9 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 6, !dbg !75
  store i32 0, i32* %9, align 4, !dbg !76, !tbaa !77
  %10 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 7, !dbg !78
  store i32 2, i32* %10, align 8, !dbg !79, !tbaa !80
  %11 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 10, !dbg !81
  store i32 1, i32* %11, align 8, !dbg !82, !tbaa !83
  %12 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 8, !dbg !84
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 11, !dbg !85
  store i32 0, i32* %13, align 4, !dbg !86, !tbaa !87
  %14 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 12, !dbg !88
  store i32 0, i32* %14, align 8, !dbg !89, !tbaa !90
  %15 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 13, !dbg !91
  %16 = bitcast i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)** %12 to i8*, !dbg !92
  call void @llvm.memset.p0i8.i64(i8* nonnull %16, i8 0, i64 16, i32 8, i1 false), !dbg !93
  store i32 -1, i32* %15, align 4, !dbg !92, !tbaa !94
  %17 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 14, !dbg !95
  store i32 -1, i32* %17, align 8, !dbg !96, !tbaa !97
  %18 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 16, !dbg !98
  store i32 -1, i32* %18, align 8, !dbg !99, !tbaa !100
  %19 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 17, !dbg !101
  %20 = bitcast double* %19 to <2 x double>*, !dbg !102
  store <2 x double> <double -1.000000e+00, double -1.000000e+00>, <2 x double>* %20, align 8, !dbg !102, !tbaa !59
  %21 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 19, !dbg !103
  %22 = bitcast double* %21 to <2 x double>*, !dbg !104
  store <2 x double> <double -1.000000e+00, double -1.000000e+00>, <2 x double>* %22, align 8, !dbg !104, !tbaa !59
  %23 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %0, i64 0, i32 21, !dbg !105
  %24 = bitcast double* %23 to i8*, !dbg !106
  call void @llvm.memset.p0i8.i64(i8* nonnull %24, i8 0, i64 24, i32 8, i1 false), !dbg !107
  br label %25, !dbg !106

; <label>:25:                                     ; preds = %1, %3
  %26 = phi i32 [ 1, %3 ], [ 0, %1 ], !dbg !108
  ret i32 %26, !dbg !109
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
!58 = !DILocation(line: 20, column: 17, scope: !10)
!59 = !{!60, !60, i64 0}
!60 = !{!"double", !61, i64 0}
!61 = !{!"omnipotent char", !62, i64 0}
!62 = !{!"Simple C/C++ TBAA"}
!63 = !DILocation(line: 22, column: 13, scope: !10)
!64 = !DILocation(line: 22, column: 25, scope: !10)
!65 = !DILocation(line: 24, column: 13, scope: !10)
!66 = !DILocation(line: 24, column: 17, scope: !10)
!67 = !{!68, !69, i64 40}
!68 = !{!"klu_common_struct", !60, i64 0, !60, i64 8, !60, i64 16, !60, i64 24, !60, i64 32, !69, i64 40, !69, i64 44, !69, i64 48, !70, i64 56, !70, i64 64, !69, i64 72, !69, i64 76, !69, i64 80, !69, i64 84, !69, i64 88, !69, i64 92, !69, i64 96, !60, i64 104, !60, i64 112, !60, i64 120, !60, i64 128, !60, i64 136, !71, i64 144, !71, i64 152}
!69 = !{!"int", !61, i64 0}
!70 = !{!"any pointer", !61, i64 0}
!71 = !{!"long", !61, i64 0}
!72 = !DILocation(line: 25, column: 13, scope: !10)
!73 = !DILocation(line: 25, column: 21, scope: !10)
!74 = !{!68, !60, i64 32}
!75 = !DILocation(line: 26, column: 13, scope: !10)
!76 = !DILocation(line: 26, column: 22, scope: !10)
!77 = !{!68, !69, i64 44}
!78 = !DILocation(line: 28, column: 13, scope: !10)
!79 = !DILocation(line: 28, column: 19, scope: !10)
!80 = !{!68, !69, i64 48}
!81 = !DILocation(line: 32, column: 13, scope: !10)
!82 = !DILocation(line: 32, column: 30, scope: !10)
!83 = !{!68, !69, i64 72}
!84 = !DILocation(line: 35, column: 13, scope: !10)
!85 = !DILocation(line: 39, column: 13, scope: !10)
!86 = !DILocation(line: 39, column: 20, scope: !10)
!87 = !{!68, !69, i64 76}
!88 = !DILocation(line: 40, column: 13, scope: !10)
!89 = !DILocation(line: 40, column: 22, scope: !10)
!90 = !{!68, !69, i64 80}
!91 = !DILocation(line: 41, column: 13, scope: !10)
!92 = !DILocation(line: 41, column: 29, scope: !10)
!93 = !DILocation(line: 36, column: 23, scope: !10)
!94 = !{!68, !69, i64 84}
!95 = !DILocation(line: 42, column: 13, scope: !10)
!96 = !DILocation(line: 42, column: 28, scope: !10)
!97 = !{!68, !69, i64 88}
!98 = !DILocation(line: 43, column: 13, scope: !10)
!99 = !DILocation(line: 43, column: 22, scope: !10)
!100 = !{!68, !69, i64 96}
!101 = !DILocation(line: 44, column: 13, scope: !10)
!102 = !DILocation(line: 44, column: 19, scope: !10)
!103 = !DILocation(line: 46, column: 13, scope: !10)
!104 = !DILocation(line: 46, column: 21, scope: !10)
!105 = !DILocation(line: 48, column: 13, scope: !10)
!106 = !DILocation(line: 53, column: 5, scope: !10)
!107 = !DILocation(line: 50, column: 22, scope: !10)
!108 = !DILocation(line: 0, scope: !10)
!109 = !DILocation(line: 54, column: 1, scope: !10)
