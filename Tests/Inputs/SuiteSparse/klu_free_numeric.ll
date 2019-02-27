; ModuleID = 'klu_free_numeric.c'
source_filename = "klu_free_numeric.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_numeric = type { i32, i32, i32, i32, i32, i32, i32*, i32*, i32*, i32*, i32*, i32*, i8**, i64*, i8*, double*, i64, i8*, i8*, i32*, i32*, i32*, i8*, i32 }
%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_free_numeric(%struct.klu_numeric**, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !15 {
  call void @llvm.dbg.value(metadata %struct.klu_numeric** %0, metadata !89, metadata !DIExpression()), !dbg !98
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %1, metadata !90, metadata !DIExpression()), !dbg !99
  %3 = icmp eq %struct.klu_common_struct* %1, null, !dbg !100
  br i1 %3, label %102, label %4, !dbg !102

; <label>:4:                                      ; preds = %2
  %5 = icmp eq %struct.klu_numeric** %0, null, !dbg !103
  br i1 %5, label %102, label %6, !dbg !105

; <label>:6:                                      ; preds = %4
  %7 = load %struct.klu_numeric*, %struct.klu_numeric** %0, align 8, !dbg !106, !tbaa !107
  %8 = icmp eq %struct.klu_numeric* %7, null, !dbg !111
  br i1 %8, label %102, label %9, !dbg !112

; <label>:9:                                      ; preds = %6
  call void @llvm.dbg.value(metadata %struct.klu_numeric* %7, metadata !91, metadata !DIExpression()), !dbg !113
  %10 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 0, !dbg !114
  %11 = load i32, i32* %10, align 8, !dbg !114, !tbaa !115
  call void @llvm.dbg.value(metadata i32 %11, metadata !95, metadata !DIExpression()), !dbg !119
  %12 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 23, !dbg !120
  %13 = load i32, i32* %12, align 8, !dbg !120, !tbaa !121
  call void @llvm.dbg.value(metadata i32 %13, metadata !96, metadata !DIExpression()), !dbg !122
  %14 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 1, !dbg !123
  %15 = load i32, i32* %14, align 4, !dbg !123, !tbaa !124
  call void @llvm.dbg.value(metadata i32 %15, metadata !97, metadata !DIExpression()), !dbg !125
  %16 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 13, !dbg !126
  %17 = load i64*, i64** %16, align 8, !dbg !126, !tbaa !127
  call void @llvm.dbg.value(metadata i64* %17, metadata !93, metadata !DIExpression()), !dbg !128
  %18 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 12, !dbg !129
  %19 = bitcast i8*** %18 to double***, !dbg !129
  %20 = load double**, double*** %19, align 8, !dbg !129, !tbaa !130
  call void @llvm.dbg.value(metadata double** %20, metadata !92, metadata !DIExpression()), !dbg !131
  %21 = icmp ne double** %20, null, !dbg !132
  %22 = icmp sgt i32 %15, 0, !dbg !134
  %23 = and i1 %21, %22, !dbg !138
  call void @llvm.dbg.value(metadata i32 0, metadata !94, metadata !DIExpression()), !dbg !139
  br i1 %23, label %24, label %40, !dbg !138

; <label>:24:                                     ; preds = %9
  %25 = icmp eq i64* %17, null
  %26 = zext i32 %15 to i64
  br label %27, !dbg !140

; <label>:27:                                     ; preds = %35, %24
  %28 = phi i64 [ 0, %24 ], [ %38, %35 ]
  call void @llvm.dbg.value(metadata i64 %28, metadata !94, metadata !DIExpression()), !dbg !139
  %29 = getelementptr inbounds double*, double** %20, i64 %28, !dbg !141
  %30 = bitcast double** %29 to i8**, !dbg !141
  %31 = load i8*, i8** %30, align 8, !dbg !141, !tbaa !107
  br i1 %25, label %35, label %32, !dbg !143

; <label>:32:                                     ; preds = %27
  %33 = getelementptr inbounds i64, i64* %17, i64 %28, !dbg !144
  %34 = load i64, i64* %33, align 8, !dbg !144, !tbaa !145
  br label %35, !dbg !143

; <label>:35:                                     ; preds = %27, %32
  %36 = phi i64 [ %34, %32 ], [ 0, %27 ], !dbg !143
  %37 = tail call i8* @klu_free(i8* %31, i64 %36, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !146
  %38 = add nuw nsw i64 %28, 1, !dbg !147
  call void @llvm.dbg.value(metadata i32 undef, metadata !94, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !139
  %39 = icmp eq i64 %38, %26, !dbg !134
  br i1 %39, label %40, label %27, !dbg !140, !llvm.loop !148

; <label>:40:                                     ; preds = %35, %9
  %41 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 6, !dbg !150
  %42 = bitcast i32** %41 to i8**, !dbg !150
  %43 = load i8*, i8** %42, align 8, !dbg !150, !tbaa !151
  %44 = sext i32 %11 to i64, !dbg !152
  %45 = tail call i8* @klu_free(i8* %43, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !153
  %46 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 20, !dbg !154
  %47 = bitcast i32** %46 to i8**, !dbg !154
  %48 = load i8*, i8** %47, align 8, !dbg !154, !tbaa !155
  %49 = add nsw i32 %11, 1, !dbg !156
  %50 = sext i32 %49 to i64, !dbg !157
  %51 = tail call i8* @klu_free(i8* %48, i64 %50, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !158
  %52 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 21, !dbg !159
  %53 = bitcast i32** %52 to i8**, !dbg !159
  %54 = load i8*, i8** %53, align 8, !dbg !159, !tbaa !160
  %55 = add nsw i32 %13, 1, !dbg !161
  %56 = sext i32 %55 to i64, !dbg !162
  %57 = tail call i8* @klu_free(i8* %54, i64 %56, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !163
  %58 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 22, !dbg !164
  %59 = load i8*, i8** %58, align 8, !dbg !164, !tbaa !165
  %60 = tail call i8* @klu_free(i8* %59, i64 %56, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !166
  %61 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 8, !dbg !167
  %62 = bitcast i32** %61 to i8**, !dbg !167
  %63 = load i8*, i8** %62, align 8, !dbg !167, !tbaa !168
  %64 = tail call i8* @klu_free(i8* %63, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !169
  %65 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 10, !dbg !170
  %66 = bitcast i32** %65 to i8**, !dbg !170
  %67 = load i8*, i8** %66, align 8, !dbg !170, !tbaa !171
  %68 = tail call i8* @klu_free(i8* %67, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !172
  %69 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 9, !dbg !173
  %70 = bitcast i32** %69 to i8**, !dbg !173
  %71 = load i8*, i8** %70, align 8, !dbg !173, !tbaa !174
  %72 = tail call i8* @klu_free(i8* %71, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !175
  %73 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 11, !dbg !176
  %74 = bitcast i32** %73 to i8**, !dbg !176
  %75 = load i8*, i8** %74, align 8, !dbg !176, !tbaa !177
  %76 = tail call i8* @klu_free(i8* %75, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !178
  %77 = bitcast i64** %16 to i8**, !dbg !179
  %78 = load i8*, i8** %77, align 8, !dbg !179, !tbaa !127
  %79 = sext i32 %15 to i64, !dbg !180
  %80 = tail call i8* @klu_free(i8* %78, i64 %79, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !181
  %81 = bitcast i8*** %18 to i8**, !dbg !182
  %82 = load i8*, i8** %81, align 8, !dbg !182, !tbaa !130
  %83 = tail call i8* @klu_free(i8* %82, i64 %79, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !183
  %84 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 14, !dbg !184
  %85 = load i8*, i8** %84, align 8, !dbg !184, !tbaa !185
  %86 = tail call i8* @klu_free(i8* %85, i64 %44, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !186
  %87 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 15, !dbg !187
  %88 = bitcast double** %87 to i8**, !dbg !187
  %89 = load i8*, i8** %88, align 8, !dbg !187, !tbaa !188
  %90 = tail call i8* @klu_free(i8* %89, i64 %44, i64 8, %struct.klu_common_struct* nonnull %1) #3, !dbg !189
  %91 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 7, !dbg !190
  %92 = bitcast i32** %91 to i8**, !dbg !190
  %93 = load i8*, i8** %92, align 8, !dbg !190, !tbaa !191
  %94 = tail call i8* @klu_free(i8* %93, i64 %44, i64 4, %struct.klu_common_struct* nonnull %1) #3, !dbg !192
  %95 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 17, !dbg !193
  %96 = load i8*, i8** %95, align 8, !dbg !193, !tbaa !194
  %97 = getelementptr inbounds %struct.klu_numeric, %struct.klu_numeric* %7, i64 0, i32 16, !dbg !195
  %98 = load i64, i64* %97, align 8, !dbg !195, !tbaa !196
  %99 = tail call i8* @klu_free(i8* %96, i64 %98, i64 1, %struct.klu_common_struct* nonnull %1) #3, !dbg !197
  %100 = bitcast %struct.klu_numeric* %7 to i8*, !dbg !198
  %101 = tail call i8* @klu_free(i8* %100, i64 1, i64 168, %struct.klu_common_struct* nonnull %1) #3, !dbg !199
  store %struct.klu_numeric* null, %struct.klu_numeric** %0, align 8, !dbg !200, !tbaa !107
  br label %102, !dbg !201

; <label>:102:                                    ; preds = %4, %6, %2, %40
  %103 = phi i32 [ 1, %40 ], [ 0, %2 ], [ 1, %6 ], [ 1, %4 ], !dbg !202
  ret i32 %103, !dbg !203
}

declare i8* @klu_free(i8*, i64, i64, %struct.klu_common_struct*) local_unnamed_addr #1

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #2 = { nounwind readnone speculatable }
attributes #3 = { nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!10, !11, !12, !13}
!llvm.ident = !{!14}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_free_numeric.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !7, size: 64)
!7 = !DIDerivedType(tag: DW_TAG_typedef, name: "Unit", file: !8, line: 253, baseType: !9)
!8 = !DIFile(filename: "./klu_version.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!9 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!10 = !{i32 2, !"Dwarf Version", i32 4}
!11 = !{i32 2, !"Debug Info Version", i32 3}
!12 = !{i32 1, !"wchar_size", i32 4}
!13 = !{i32 7, !"PIC Level", i32 2}
!14 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!15 = distinct !DISubprogram(name: "klu_free_numeric", scope: !1, file: !1, line: 9, type: !16, isLocal: false, isDefinition: true, scopeLine: 14, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !88)
!16 = !DISubroutineType(types: !17)
!17 = !{!18, !19, !56}
!18 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!19 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !20, size: 64)
!20 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !21, size: 64)
!21 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_numeric", file: !22, line: 107, baseType: !23)
!22 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!23 = distinct !DICompositeType(tag: DW_TAG_structure_type, file: !22, line: 69, size: 1344, elements: !24)
!24 = !{!25, !26, !27, !28, !29, !30, !31, !33, !34, !35, !36, !37, !38, !40, !45, !46, !48, !49, !50, !51, !52, !53, !54, !55}
!25 = !DIDerivedType(tag: DW_TAG_member, name: "n", scope: !23, file: !22, line: 74, baseType: !18, size: 32)
!26 = !DIDerivedType(tag: DW_TAG_member, name: "nblocks", scope: !23, file: !22, line: 75, baseType: !18, size: 32, offset: 32)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "lnz", scope: !23, file: !22, line: 76, baseType: !18, size: 32, offset: 64)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "unz", scope: !23, file: !22, line: 77, baseType: !18, size: 32, offset: 96)
!29 = !DIDerivedType(tag: DW_TAG_member, name: "max_lnz_block", scope: !23, file: !22, line: 78, baseType: !18, size: 32, offset: 128)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "max_unz_block", scope: !23, file: !22, line: 79, baseType: !18, size: 32, offset: 160)
!31 = !DIDerivedType(tag: DW_TAG_member, name: "Pnum", scope: !23, file: !22, line: 80, baseType: !32, size: 64, offset: 192)
!32 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !18, size: 64)
!33 = !DIDerivedType(tag: DW_TAG_member, name: "Pinv", scope: !23, file: !22, line: 81, baseType: !32, size: 64, offset: 256)
!34 = !DIDerivedType(tag: DW_TAG_member, name: "Lip", scope: !23, file: !22, line: 84, baseType: !32, size: 64, offset: 320)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "Uip", scope: !23, file: !22, line: 85, baseType: !32, size: 64, offset: 384)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "Llen", scope: !23, file: !22, line: 86, baseType: !32, size: 64, offset: 448)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "Ulen", scope: !23, file: !22, line: 87, baseType: !32, size: 64, offset: 512)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "LUbx", scope: !23, file: !22, line: 88, baseType: !39, size: 64, offset: 576)
!39 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !4, size: 64)
!40 = !DIDerivedType(tag: DW_TAG_member, name: "LUsize", scope: !23, file: !22, line: 89, baseType: !41, size: 64, offset: 640)
!41 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !42, size: 64)
!42 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !43, line: 62, baseType: !44)
!43 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!44 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "Udiag", scope: !23, file: !22, line: 90, baseType: !4, size: 64, offset: 704)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "Rs", scope: !23, file: !22, line: 93, baseType: !47, size: 64, offset: 768)
!47 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !9, size: 64)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "worksize", scope: !23, file: !22, line: 96, baseType: !42, size: 64, offset: 832)
!49 = !DIDerivedType(tag: DW_TAG_member, name: "Work", scope: !23, file: !22, line: 97, baseType: !4, size: 64, offset: 896)
!50 = !DIDerivedType(tag: DW_TAG_member, name: "Xwork", scope: !23, file: !22, line: 98, baseType: !4, size: 64, offset: 960)
!51 = !DIDerivedType(tag: DW_TAG_member, name: "Iwork", scope: !23, file: !22, line: 99, baseType: !32, size: 64, offset: 1024)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "Offp", scope: !23, file: !22, line: 102, baseType: !32, size: 64, offset: 1088)
!53 = !DIDerivedType(tag: DW_TAG_member, name: "Offi", scope: !23, file: !22, line: 103, baseType: !32, size: 64, offset: 1152)
!54 = !DIDerivedType(tag: DW_TAG_member, name: "Offx", scope: !23, file: !22, line: 104, baseType: !4, size: 64, offset: 1216)
!55 = !DIDerivedType(tag: DW_TAG_member, name: "nzoff", scope: !23, file: !22, line: 105, baseType: !18, size: 32, offset: 1280)
!56 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !57, size: 64)
!57 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !22, line: 207, baseType: !58)
!58 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !22, line: 137, size: 1280, elements: !59)
!59 = !{!60, !61, !62, !63, !64, !65, !66, !67, !68, !73, !74, !75, !76, !77, !78, !79, !80, !81, !82, !83, !84, !85, !86, !87}
!60 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !58, file: !22, line: 144, baseType: !9, size: 64)
!61 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !58, file: !22, line: 145, baseType: !9, size: 64, offset: 64)
!62 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !58, file: !22, line: 146, baseType: !9, size: 64, offset: 128)
!63 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !58, file: !22, line: 147, baseType: !9, size: 64, offset: 192)
!64 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !58, file: !22, line: 148, baseType: !9, size: 64, offset: 256)
!65 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !58, file: !22, line: 150, baseType: !18, size: 32, offset: 320)
!66 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !58, file: !22, line: 151, baseType: !18, size: 32, offset: 352)
!67 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !58, file: !22, line: 153, baseType: !18, size: 32, offset: 384)
!68 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !58, file: !22, line: 157, baseType: !69, size: 64, offset: 448)
!69 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !70, size: 64)
!70 = !DISubroutineType(types: !71)
!71 = !{!18, !18, !32, !32, !32, !72}
!72 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !58, size: 64)
!73 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !58, file: !22, line: 162, baseType: !4, size: 64, offset: 512)
!74 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !58, file: !22, line: 164, baseType: !18, size: 32, offset: 576)
!75 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !58, file: !22, line: 177, baseType: !18, size: 32, offset: 608)
!76 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !58, file: !22, line: 178, baseType: !18, size: 32, offset: 640)
!77 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !58, file: !22, line: 180, baseType: !18, size: 32, offset: 672)
!78 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !58, file: !22, line: 185, baseType: !18, size: 32, offset: 704)
!79 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !58, file: !22, line: 191, baseType: !18, size: 32, offset: 736)
!80 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !58, file: !22, line: 196, baseType: !18, size: 32, offset: 768)
!81 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !58, file: !22, line: 198, baseType: !9, size: 64, offset: 832)
!82 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !58, file: !22, line: 199, baseType: !9, size: 64, offset: 896)
!83 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !58, file: !22, line: 200, baseType: !9, size: 64, offset: 960)
!84 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !58, file: !22, line: 201, baseType: !9, size: 64, offset: 1024)
!85 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !58, file: !22, line: 202, baseType: !9, size: 64, offset: 1088)
!86 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !58, file: !22, line: 204, baseType: !42, size: 64, offset: 1152)
!87 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !58, file: !22, line: 205, baseType: !42, size: 64, offset: 1216)
!88 = !{!89, !90, !91, !92, !93, !94, !95, !96, !97}
!89 = !DILocalVariable(name: "NumericHandle", arg: 1, scope: !15, file: !1, line: 11, type: !19)
!90 = !DILocalVariable(name: "Common", arg: 2, scope: !15, file: !1, line: 12, type: !56)
!91 = !DILocalVariable(name: "Numeric", scope: !15, file: !1, line: 15, type: !20)
!92 = !DILocalVariable(name: "LUbx", scope: !15, file: !1, line: 16, type: !5)
!93 = !DILocalVariable(name: "LUsize", scope: !15, file: !1, line: 17, type: !41)
!94 = !DILocalVariable(name: "block", scope: !15, file: !1, line: 18, type: !18)
!95 = !DILocalVariable(name: "n", scope: !15, file: !1, line: 18, type: !18)
!96 = !DILocalVariable(name: "nzoff", scope: !15, file: !1, line: 18, type: !18)
!97 = !DILocalVariable(name: "nblocks", scope: !15, file: !1, line: 18, type: !18)
!98 = !DILocation(line: 11, column: 19, scope: !15)
!99 = !DILocation(line: 12, column: 18, scope: !15)
!100 = !DILocation(line: 20, column: 16, scope: !101)
!101 = distinct !DILexicalBlock(scope: !15, file: !1, line: 20, column: 9)
!102 = !DILocation(line: 20, column: 9, scope: !15)
!103 = !DILocation(line: 24, column: 23, scope: !104)
!104 = distinct !DILexicalBlock(scope: !15, file: !1, line: 24, column: 9)
!105 = !DILocation(line: 24, column: 31, scope: !104)
!106 = !DILocation(line: 24, column: 34, scope: !104)
!107 = !{!108, !108, i64 0}
!108 = !{!"any pointer", !109, i64 0}
!109 = !{!"omnipotent char", !110, i64 0}
!110 = !{!"Simple C/C++ TBAA"}
!111 = !DILocation(line: 24, column: 49, scope: !104)
!112 = !DILocation(line: 24, column: 9, scope: !15)
!113 = !DILocation(line: 15, column: 18, scope: !15)
!114 = !DILocation(line: 31, column: 18, scope: !15)
!115 = !{!116, !117, i64 0}
!116 = !{!"", !117, i64 0, !117, i64 4, !117, i64 8, !117, i64 12, !117, i64 16, !117, i64 20, !108, i64 24, !108, i64 32, !108, i64 40, !108, i64 48, !108, i64 56, !108, i64 64, !108, i64 72, !108, i64 80, !108, i64 88, !108, i64 96, !118, i64 104, !108, i64 112, !108, i64 120, !108, i64 128, !108, i64 136, !108, i64 144, !108, i64 152, !117, i64 160}
!117 = !{!"int", !109, i64 0}
!118 = !{!"long", !109, i64 0}
!119 = !DILocation(line: 18, column: 16, scope: !15)
!120 = !DILocation(line: 32, column: 22, scope: !15)
!121 = !{!116, !117, i64 160}
!122 = !DILocation(line: 18, column: 19, scope: !15)
!123 = !DILocation(line: 33, column: 24, scope: !15)
!124 = !{!116, !117, i64 4}
!125 = !DILocation(line: 18, column: 26, scope: !15)
!126 = !DILocation(line: 34, column: 23, scope: !15)
!127 = !{!116, !108, i64 80}
!128 = !DILocation(line: 17, column: 13, scope: !15)
!129 = !DILocation(line: 36, column: 31, scope: !15)
!130 = !{!116, !108, i64 72}
!131 = !DILocation(line: 16, column: 12, scope: !15)
!132 = !DILocation(line: 37, column: 14, scope: !133)
!133 = distinct !DILexicalBlock(scope: !15, file: !1, line: 37, column: 9)
!134 = !DILocation(line: 39, column: 32, scope: !135)
!135 = distinct !DILexicalBlock(scope: !136, file: !1, line: 39, column: 9)
!136 = distinct !DILexicalBlock(scope: !137, file: !1, line: 39, column: 9)
!137 = distinct !DILexicalBlock(scope: !133, file: !1, line: 38, column: 5)
!138 = !DILocation(line: 37, column: 9, scope: !15)
!139 = !DILocation(line: 18, column: 9, scope: !15)
!140 = !DILocation(line: 39, column: 9, scope: !136)
!141 = !DILocation(line: 41, column: 23, scope: !142)
!142 = distinct !DILexicalBlock(scope: !135, file: !1, line: 40, column: 9)
!143 = !DILocation(line: 41, column: 37, scope: !142)
!144 = !DILocation(line: 41, column: 46, scope: !142)
!145 = !{!118, !118, i64 0}
!146 = !DILocation(line: 41, column: 13, scope: !142)
!147 = !DILocation(line: 39, column: 49, scope: !135)
!148 = distinct !{!148, !140, !149}
!149 = !DILocation(line: 43, column: 9, scope: !136)
!150 = !DILocation(line: 46, column: 24, scope: !15)
!151 = !{!116, !108, i64 24}
!152 = !DILocation(line: 46, column: 30, scope: !15)
!153 = !DILocation(line: 46, column: 5, scope: !15)
!154 = !DILocation(line: 47, column: 24, scope: !15)
!155 = !{!116, !108, i64 136}
!156 = !DILocation(line: 47, column: 31, scope: !15)
!157 = !DILocation(line: 47, column: 30, scope: !15)
!158 = !DILocation(line: 47, column: 5, scope: !15)
!159 = !DILocation(line: 48, column: 24, scope: !15)
!160 = !{!116, !108, i64 144}
!161 = !DILocation(line: 48, column: 35, scope: !15)
!162 = !DILocation(line: 48, column: 30, scope: !15)
!163 = !DILocation(line: 48, column: 5, scope: !15)
!164 = !DILocation(line: 49, column: 24, scope: !15)
!165 = !{!116, !108, i64 152}
!166 = !DILocation(line: 49, column: 5, scope: !15)
!167 = !DILocation(line: 51, column: 24, scope: !15)
!168 = !{!116, !108, i64 40}
!169 = !DILocation(line: 51, column: 5, scope: !15)
!170 = !DILocation(line: 52, column: 24, scope: !15)
!171 = !{!116, !108, i64 56}
!172 = !DILocation(line: 52, column: 5, scope: !15)
!173 = !DILocation(line: 53, column: 24, scope: !15)
!174 = !{!116, !108, i64 48}
!175 = !DILocation(line: 53, column: 5, scope: !15)
!176 = !DILocation(line: 54, column: 24, scope: !15)
!177 = !{!116, !108, i64 64}
!178 = !DILocation(line: 54, column: 5, scope: !15)
!179 = !DILocation(line: 56, column: 24, scope: !15)
!180 = !DILocation(line: 56, column: 32, scope: !15)
!181 = !DILocation(line: 56, column: 5, scope: !15)
!182 = !DILocation(line: 58, column: 24, scope: !15)
!183 = !DILocation(line: 58, column: 5, scope: !15)
!184 = !DILocation(line: 60, column: 24, scope: !15)
!185 = !{!116, !108, i64 88}
!186 = !DILocation(line: 60, column: 5, scope: !15)
!187 = !DILocation(line: 62, column: 24, scope: !15)
!188 = !{!116, !108, i64 96}
!189 = !DILocation(line: 62, column: 5, scope: !15)
!190 = !DILocation(line: 63, column: 24, scope: !15)
!191 = !{!116, !108, i64 32}
!192 = !DILocation(line: 63, column: 5, scope: !15)
!193 = !DILocation(line: 65, column: 24, scope: !15)
!194 = !{!116, !108, i64 112}
!195 = !DILocation(line: 65, column: 39, scope: !15)
!196 = !{!116, !118, i64 104}
!197 = !DILocation(line: 65, column: 5, scope: !15)
!198 = !DILocation(line: 67, column: 15, scope: !15)
!199 = !DILocation(line: 67, column: 5, scope: !15)
!200 = !DILocation(line: 69, column: 20, scope: !15)
!201 = !DILocation(line: 70, column: 5, scope: !15)
!202 = !DILocation(line: 0, scope: !15)
!203 = !DILocation(line: 71, column: 1, scope: !15)
