; ModuleID = 'klu_scale.c'
source_filename = "klu_scale.c"
target datalayout = "e-m:o-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-apple-macosx10.14.0"

%struct.klu_common_struct = type { double, double, double, double, double, i32, i32, i32, i32 (i32, i32*, i32*, i32*, %struct.klu_common_struct*)*, i8*, i32, i32, i32, i32, i32, i32, i32, double, double, double, double, double, i64, i64 }

; Function Attrs: nounwind ssp uwtable
define i32 @klu_scale(i32, i32, i32* readonly, i32* readonly, double* readonly, double*, i32*, %struct.klu_common_struct*) local_unnamed_addr #0 !dbg !14 {
  call void @llvm.dbg.value(metadata i32 %0, metadata !54, metadata !DIExpression()), !dbg !69
  call void @llvm.dbg.value(metadata i32 %1, metadata !55, metadata !DIExpression()), !dbg !70
  call void @llvm.dbg.value(metadata i32* %2, metadata !56, metadata !DIExpression()), !dbg !71
  call void @llvm.dbg.value(metadata i32* %3, metadata !57, metadata !DIExpression()), !dbg !72
  call void @llvm.dbg.value(metadata double* %4, metadata !58, metadata !DIExpression()), !dbg !73
  call void @llvm.dbg.value(metadata double* %5, metadata !59, metadata !DIExpression()), !dbg !74
  call void @llvm.dbg.value(metadata i32* %6, metadata !60, metadata !DIExpression()), !dbg !75
  call void @llvm.dbg.value(metadata %struct.klu_common_struct* %7, metadata !61, metadata !DIExpression()), !dbg !76
  %9 = bitcast i32* %6 to i8*
  %10 = bitcast double* %5 to i8*
  %11 = icmp eq %struct.klu_common_struct* %7, null, !dbg !77
  br i1 %11, label %135, label %12, !dbg !79

; <label>:12:                                     ; preds = %8
  %13 = getelementptr inbounds %struct.klu_common_struct, %struct.klu_common_struct* %7, i64 0, i32 11, !dbg !80
  store i32 0, i32* %13, align 4, !dbg !81, !tbaa !82
  %14 = icmp slt i32 %0, 0, !dbg !90
  br i1 %14, label %135, label %15, !dbg !92

; <label>:15:                                     ; preds = %12
  call void @llvm.dbg.value(metadata double* %4, metadata !63, metadata !DIExpression()), !dbg !93
  %16 = icmp slt i32 %1, 1, !dbg !94
  %17 = icmp eq i32* %2, null, !dbg !96
  %18 = or i1 %16, %17, !dbg !97
  %19 = icmp eq i32* %3, null, !dbg !98
  %20 = or i1 %18, %19, !dbg !97
  %21 = icmp eq double* %4, null, !dbg !99
  %22 = or i1 %20, %21, !dbg !97
  br i1 %22, label %27, label %23, !dbg !97

; <label>:23:                                     ; preds = %15
  %24 = icmp sgt i32 %0, 0, !dbg !100
  %25 = icmp eq double* %5, null, !dbg !101
  %26 = and i1 %24, %25, !dbg !102
  br i1 %26, label %27, label %28, !dbg !102

; <label>:27:                                     ; preds = %23, %15
  store i32 -3, i32* %13, align 4, !dbg !103, !tbaa !82
  br label %135, !dbg !105

; <label>:28:                                     ; preds = %23
  %29 = load i32, i32* %2, align 4, !dbg !106, !tbaa !108
  %30 = icmp eq i32 %29, 0, !dbg !109
  br i1 %30, label %31, label %36, !dbg !110

; <label>:31:                                     ; preds = %28
  %32 = sext i32 %1 to i64, !dbg !111
  %33 = getelementptr inbounds i32, i32* %2, i64 %32, !dbg !111
  %34 = load i32, i32* %33, align 4, !dbg !111, !tbaa !108
  %35 = icmp slt i32 %34, 0, !dbg !112
  br i1 %35, label %36, label %37, !dbg !113

; <label>:36:                                     ; preds = %28, %31
  store i32 -3, i32* %13, align 4, !dbg !114, !tbaa !82
  br label %135, !dbg !116

; <label>:37:                                     ; preds = %31
  call void @llvm.dbg.value(metadata i32 0, metadata !65, metadata !DIExpression()), !dbg !117
  %38 = sext i32 %1 to i64, !dbg !118
  br label %41, !dbg !118

; <label>:39:                                     ; preds = %41
  call void @llvm.dbg.value(metadata i32 undef, metadata !65, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !117
  %40 = icmp slt i64 %45, %38, !dbg !120
  br i1 %40, label %41, label %50, !dbg !118, !llvm.loop !122

; <label>:41:                                     ; preds = %37, %39
  %42 = phi i64 [ 0, %37 ], [ %45, %39 ]
  call void @llvm.dbg.value(metadata i64 %42, metadata !65, metadata !DIExpression()), !dbg !117
  %43 = getelementptr inbounds i32, i32* %2, i64 %42, !dbg !124
  %44 = load i32, i32* %43, align 4, !dbg !124, !tbaa !108
  %45 = add nuw nsw i64 %42, 1, !dbg !127
  %46 = getelementptr inbounds i32, i32* %2, i64 %45, !dbg !128
  %47 = load i32, i32* %46, align 4, !dbg !128, !tbaa !108
  %48 = icmp sgt i32 %44, %47, !dbg !129
  call void @llvm.dbg.value(metadata i32 undef, metadata !65, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !117
  br i1 %48, label %49, label %39, !dbg !130

; <label>:49:                                     ; preds = %41
  store i32 -3, i32* %13, align 4, !dbg !131, !tbaa !82
  br label %135, !dbg !133

; <label>:50:                                     ; preds = %39
  %51 = icmp sgt i32 %1, 0, !dbg !134
  %52 = and i1 %24, %51, !dbg !139
  call void @llvm.dbg.value(metadata i32 0, metadata !64, metadata !DIExpression()), !dbg !140
  br i1 %52, label %53, label %56, !dbg !139

; <label>:53:                                     ; preds = %50
  %54 = zext i32 %1 to i64, !dbg !141
  %55 = shl nuw nsw i64 %54, 3, !dbg !141
  call void @llvm.memset.p0i8.i64(i8* %10, i8 0, i64 %55, i32 8, i1 false), !dbg !142
  br label %56, !dbg !144

; <label>:56:                                     ; preds = %53, %50
  %57 = icmp ne i32* %6, null, !dbg !144
  %58 = icmp sgt i32 %1, 0, !dbg !145
  %59 = and i1 %57, %58, !dbg !150
  call void @llvm.dbg.value(metadata i32 0, metadata !64, metadata !DIExpression()), !dbg !140
  br i1 %59, label %60, label %63, !dbg !150

; <label>:60:                                     ; preds = %56
  %61 = zext i32 %1 to i64, !dbg !151
  %62 = shl nuw nsw i64 %61, 2, !dbg !151
  call void @llvm.memset.p0i8.i64(i8* %9, i8 -1, i64 %62, i32 4, i1 false), !dbg !152
  call void @llvm.dbg.value(metadata i32 0, metadata !65, metadata !DIExpression()), !dbg !117
  br label %65, !dbg !154

; <label>:63:                                     ; preds = %56
  call void @llvm.dbg.value(metadata i32 0, metadata !65, metadata !DIExpression()), !dbg !117
  %64 = icmp sgt i32 %1, 0, !dbg !156
  br i1 %64, label %65, label %135, !dbg !154

; <label>:65:                                     ; preds = %60, %63
  %66 = icmp eq i32 %0, 1
  %67 = icmp sgt i32 %0, 1
  %68 = sext i32 %1 to i64, !dbg !154
  br label %69, !dbg !154

; <label>:69:                                     ; preds = %65, %119
  %70 = phi i64 [ 0, %65 ], [ %71, %119 ]
  call void @llvm.dbg.value(metadata i64 %70, metadata !65, metadata !DIExpression()), !dbg !117
  %71 = add nuw nsw i64 %70, 1, !dbg !158
  %72 = getelementptr inbounds i32, i32* %2, i64 %71, !dbg !160
  %73 = load i32, i32* %72, align 4, !dbg !160, !tbaa !108
  call void @llvm.dbg.value(metadata i32 %73, metadata !67, metadata !DIExpression()), !dbg !161
  %74 = getelementptr inbounds i32, i32* %2, i64 %70, !dbg !162
  %75 = load i32, i32* %74, align 4, !dbg !162, !tbaa !108
  call void @llvm.dbg.value(metadata i32 %75, metadata !66, metadata !DIExpression()), !dbg !164
  %76 = icmp slt i32 %75, %73, !dbg !165
  br i1 %76, label %77, label %119, !dbg !167

; <label>:77:                                     ; preds = %69
  %78 = sext i32 %75 to i64, !dbg !167
  %79 = sext i32 %73 to i64, !dbg !167
  %80 = trunc i64 %70 to i32
  br label %81, !dbg !167

; <label>:81:                                     ; preds = %77, %116
  %82 = phi i64 [ %78, %77 ], [ %117, %116 ]
  call void @llvm.dbg.value(metadata i64 %82, metadata !66, metadata !DIExpression()), !dbg !164
  %83 = getelementptr inbounds i32, i32* %3, i64 %82, !dbg !168
  %84 = load i32, i32* %83, align 4, !dbg !168, !tbaa !108
  call void @llvm.dbg.value(metadata i32 %84, metadata !64, metadata !DIExpression()), !dbg !140
  %85 = icmp sgt i32 %84, -1, !dbg !170
  %86 = icmp slt i32 %84, %1, !dbg !172
  %87 = and i1 %85, %86, !dbg !173
  br i1 %87, label %89, label %88, !dbg !173

; <label>:88:                                     ; preds = %81
  store i32 -3, i32* %13, align 4, !dbg !174, !tbaa !82
  br label %135, !dbg !176

; <label>:89:                                     ; preds = %81
  br i1 %57, label %90, label %98, !dbg !177

; <label>:90:                                     ; preds = %89
  %91 = sext i32 %84 to i64, !dbg !178
  %92 = getelementptr inbounds i32, i32* %6, i64 %91, !dbg !178
  %93 = load i32, i32* %92, align 4, !dbg !178, !tbaa !108
  %94 = zext i32 %93 to i64, !dbg !182
  %95 = icmp eq i64 %70, %94, !dbg !182
  br i1 %95, label %96, label %97, !dbg !183

; <label>:96:                                     ; preds = %90
  store i32 -3, i32* %13, align 4, !dbg !184, !tbaa !82
  br label %135, !dbg !186

; <label>:97:                                     ; preds = %90
  store i32 %80, i32* %92, align 4, !dbg !187, !tbaa !108
  br label %98, !dbg !188

; <label>:98:                                     ; preds = %97, %89
  %99 = getelementptr inbounds double, double* %4, i64 %82, !dbg !189
  %100 = load double, double* %99, align 8, !dbg !189, !tbaa !191
  %101 = fcmp olt double %100, 0.000000e+00, !dbg !189
  %102 = fsub double -0.000000e+00, %100, !dbg !189
  %103 = select i1 %101, double %102, double %100, !dbg !189
  call void @llvm.dbg.value(metadata double %103, metadata !62, metadata !DIExpression()), !dbg !192
  br i1 %66, label %104, label %109, !dbg !193

; <label>:104:                                    ; preds = %98
  %105 = sext i32 %84 to i64, !dbg !194
  %106 = getelementptr inbounds double, double* %5, i64 %105, !dbg !194
  %107 = load double, double* %106, align 8, !dbg !197, !tbaa !191
  %108 = fadd double %103, %107, !dbg !197
  store double %108, double* %106, align 8, !dbg !197, !tbaa !191
  br label %116, !dbg !198

; <label>:109:                                    ; preds = %98
  br i1 %67, label %110, label %116, !dbg !199

; <label>:110:                                    ; preds = %109
  %111 = sext i32 %84 to i64, !dbg !200
  %112 = getelementptr inbounds double, double* %5, i64 %111, !dbg !200
  %113 = load double, double* %112, align 8, !dbg !200, !tbaa !191
  %114 = fcmp ogt double %113, %103, !dbg !200
  %115 = select i1 %114, double %113, double %103, !dbg !200
  store double %115, double* %112, align 8, !dbg !203, !tbaa !191
  br label %116, !dbg !204

; <label>:116:                                    ; preds = %104, %110, %109
  %117 = add nsw i64 %82, 1, !dbg !205
  call void @llvm.dbg.value(metadata i32 undef, metadata !66, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !164
  %118 = icmp slt i64 %117, %79, !dbg !165
  br i1 %118, label %81, label %119, !dbg !167, !llvm.loop !206

; <label>:119:                                    ; preds = %116, %69
  call void @llvm.dbg.value(metadata i32 undef, metadata !65, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !117
  %120 = icmp slt i64 %71, %68, !dbg !156
  br i1 %120, label %69, label %121, !dbg !154, !llvm.loop !208

; <label>:121:                                    ; preds = %119
  %122 = icmp sgt i32 %1, 0, !dbg !210
  %123 = and i1 %24, %122, !dbg !215
  call void @llvm.dbg.value(metadata i32 0, metadata !64, metadata !DIExpression()), !dbg !140
  br i1 %123, label %124, label %135, !dbg !215

; <label>:124:                                    ; preds = %121
  %125 = zext i32 %1 to i64
  br label %126, !dbg !216

; <label>:126:                                    ; preds = %132, %124
  %127 = phi i64 [ 0, %124 ], [ %133, %132 ]
  call void @llvm.dbg.value(metadata i64 %127, metadata !64, metadata !DIExpression()), !dbg !140
  %128 = getelementptr inbounds double, double* %5, i64 %127, !dbg !217
  %129 = load double, double* %128, align 8, !dbg !217, !tbaa !191
  %130 = fcmp oeq double %129, 0.000000e+00, !dbg !220
  br i1 %130, label %131, label %132, !dbg !221

; <label>:131:                                    ; preds = %126
  store double 1.000000e+00, double* %128, align 8, !dbg !222, !tbaa !191
  br label %132, !dbg !224

; <label>:132:                                    ; preds = %126, %131
  %133 = add nuw nsw i64 %127, 1, !dbg !225
  call void @llvm.dbg.value(metadata i32 undef, metadata !64, metadata !DIExpression(DW_OP_plus_uconst, 1, DW_OP_stack_value)), !dbg !140
  %134 = icmp eq i64 %133, %125, !dbg !210
  br i1 %134, label %135, label %126, !dbg !216, !llvm.loop !226

; <label>:135:                                    ; preds = %132, %63, %121, %12, %8, %96, %88, %49, %36, %27
  %136 = phi i32 [ 0, %27 ], [ 0, %36 ], [ 0, %49 ], [ 0, %88 ], [ 0, %96 ], [ 0, %8 ], [ 1, %12 ], [ 1, %121 ], [ 1, %63 ], [ 1, %132 ], !dbg !228
  ret i32 %136, !dbg !229
}

; Function Attrs: nounwind readnone speculatable
declare void @llvm.dbg.value(metadata, metadata, metadata) #1

; Function Attrs: argmemonly nounwind
declare void @llvm.memset.p0i8.i64(i8* nocapture writeonly, i8, i64, i32, i1) #2

attributes #0 = { nounwind ssp uwtable "correctly-rounded-divide-sqrt-fp-math"="false" "disable-tail-calls"="false" "less-precise-fpmad"="false" "no-frame-pointer-elim"="true" "no-frame-pointer-elim-non-leaf" "no-infs-fp-math"="false" "no-jump-tables"="false" "no-nans-fp-math"="false" "no-signed-zeros-fp-math"="false" "no-trapping-math"="false" "stack-protector-buffer-size"="8" "target-cpu"="penryn" "target-features"="+cx16,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87" "unsafe-fp-math"="false" "use-soft-float"="false" }
attributes #1 = { nounwind readnone speculatable }
attributes #2 = { argmemonly nounwind }

!llvm.dbg.cu = !{!0}
!llvm.module.flags = !{!9, !10, !11, !12}
!llvm.ident = !{!13}

!0 = distinct !DICompileUnit(language: DW_LANG_C99, file: !1, producer: "Apple LLVM version 10.0.0 (clang-1000.11.45.5)", isOptimized: true, runtimeVersion: 0, emissionKind: FullDebug, enums: !2, retainedTypes: !3)
!1 = !DIFile(filename: "klu_scale.c", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!2 = !{}
!3 = !{!4, !5, !7}
!4 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: null, size: 64)
!5 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !6, size: 64)
!6 = !DIBasicType(name: "double", size: 64, encoding: DW_ATE_float)
!7 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !8, size: 64)
!8 = !DIBasicType(name: "int", size: 32, encoding: DW_ATE_signed)
!9 = !{i32 2, !"Dwarf Version", i32 4}
!10 = !{i32 2, !"Debug Info Version", i32 3}
!11 = !{i32 1, !"wchar_size", i32 4}
!12 = !{i32 7, !"PIC Level", i32 2}
!13 = !{!"Apple LLVM version 10.0.0 (clang-1000.11.45.5)"}
!14 = distinct !DISubprogram(name: "klu_scale", scope: !1, file: !1, line: 19, type: !15, isLocal: false, isDefinition: true, scopeLine: 34, flags: DIFlagPrototyped, isOptimized: true, unit: !0, variables: !53)
!15 = !DISubroutineType(types: !16)
!16 = !{!8, !8, !8, !7, !7, !5, !5, !7, !17}
!17 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !18, size: 64)
!18 = !DIDerivedType(tag: DW_TAG_typedef, name: "klu_common", file: !19, line: 207, baseType: !20)
!19 = !DIFile(filename: "./klu.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!20 = distinct !DICompositeType(tag: DW_TAG_structure_type, name: "klu_common_struct", file: !19, line: 137, size: 1280, elements: !21)
!21 = !{!22, !23, !24, !25, !26, !27, !28, !29, !30, !35, !36, !37, !38, !39, !40, !41, !42, !43, !44, !45, !46, !47, !48, !52}
!22 = !DIDerivedType(tag: DW_TAG_member, name: "tol", scope: !20, file: !19, line: 144, baseType: !6, size: 64)
!23 = !DIDerivedType(tag: DW_TAG_member, name: "memgrow", scope: !20, file: !19, line: 145, baseType: !6, size: 64, offset: 64)
!24 = !DIDerivedType(tag: DW_TAG_member, name: "initmem_amd", scope: !20, file: !19, line: 146, baseType: !6, size: 64, offset: 128)
!25 = !DIDerivedType(tag: DW_TAG_member, name: "initmem", scope: !20, file: !19, line: 147, baseType: !6, size: 64, offset: 192)
!26 = !DIDerivedType(tag: DW_TAG_member, name: "maxwork", scope: !20, file: !19, line: 148, baseType: !6, size: 64, offset: 256)
!27 = !DIDerivedType(tag: DW_TAG_member, name: "btf", scope: !20, file: !19, line: 150, baseType: !8, size: 32, offset: 320)
!28 = !DIDerivedType(tag: DW_TAG_member, name: "ordering", scope: !20, file: !19, line: 151, baseType: !8, size: 32, offset: 352)
!29 = !DIDerivedType(tag: DW_TAG_member, name: "scale", scope: !20, file: !19, line: 153, baseType: !8, size: 32, offset: 384)
!30 = !DIDerivedType(tag: DW_TAG_member, name: "user_order", scope: !20, file: !19, line: 157, baseType: !31, size: 64, offset: 448)
!31 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !32, size: 64)
!32 = !DISubroutineType(types: !33)
!33 = !{!8, !8, !7, !7, !7, !34}
!34 = !DIDerivedType(tag: DW_TAG_pointer_type, baseType: !20, size: 64)
!35 = !DIDerivedType(tag: DW_TAG_member, name: "user_data", scope: !20, file: !19, line: 162, baseType: !4, size: 64, offset: 512)
!36 = !DIDerivedType(tag: DW_TAG_member, name: "halt_if_singular", scope: !20, file: !19, line: 164, baseType: !8, size: 32, offset: 576)
!37 = !DIDerivedType(tag: DW_TAG_member, name: "status", scope: !20, file: !19, line: 177, baseType: !8, size: 32, offset: 608)
!38 = !DIDerivedType(tag: DW_TAG_member, name: "nrealloc", scope: !20, file: !19, line: 178, baseType: !8, size: 32, offset: 640)
!39 = !DIDerivedType(tag: DW_TAG_member, name: "structural_rank", scope: !20, file: !19, line: 180, baseType: !8, size: 32, offset: 672)
!40 = !DIDerivedType(tag: DW_TAG_member, name: "numerical_rank", scope: !20, file: !19, line: 185, baseType: !8, size: 32, offset: 704)
!41 = !DIDerivedType(tag: DW_TAG_member, name: "singular_col", scope: !20, file: !19, line: 191, baseType: !8, size: 32, offset: 736)
!42 = !DIDerivedType(tag: DW_TAG_member, name: "noffdiag", scope: !20, file: !19, line: 196, baseType: !8, size: 32, offset: 768)
!43 = !DIDerivedType(tag: DW_TAG_member, name: "flops", scope: !20, file: !19, line: 198, baseType: !6, size: 64, offset: 832)
!44 = !DIDerivedType(tag: DW_TAG_member, name: "rcond", scope: !20, file: !19, line: 199, baseType: !6, size: 64, offset: 896)
!45 = !DIDerivedType(tag: DW_TAG_member, name: "condest", scope: !20, file: !19, line: 200, baseType: !6, size: 64, offset: 960)
!46 = !DIDerivedType(tag: DW_TAG_member, name: "rgrowth", scope: !20, file: !19, line: 201, baseType: !6, size: 64, offset: 1024)
!47 = !DIDerivedType(tag: DW_TAG_member, name: "work", scope: !20, file: !19, line: 202, baseType: !6, size: 64, offset: 1088)
!48 = !DIDerivedType(tag: DW_TAG_member, name: "memusage", scope: !20, file: !19, line: 204, baseType: !49, size: 64, offset: 1152)
!49 = !DIDerivedType(tag: DW_TAG_typedef, name: "size_t", file: !50, line: 62, baseType: !51)
!50 = !DIFile(filename: "/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/lib/clang/10.0.0/include/stddef.h", directory: "/Users/fak/Dropbox/Projects/SparsePerf/SparseSuite/SparseSuite")
!51 = !DIBasicType(name: "long unsigned int", size: 64, encoding: DW_ATE_unsigned)
!52 = !DIDerivedType(tag: DW_TAG_member, name: "mempeak", scope: !20, file: !19, line: 205, baseType: !49, size: 64, offset: 1216)
!53 = !{!54, !55, !56, !57, !58, !59, !60, !61, !62, !63, !64, !65, !66, !67, !68}
!54 = !DILocalVariable(name: "scale", arg: 1, scope: !14, file: !1, line: 22, type: !8)
!55 = !DILocalVariable(name: "n", arg: 2, scope: !14, file: !1, line: 23, type: !8)
!56 = !DILocalVariable(name: "Ap", arg: 3, scope: !14, file: !1, line: 24, type: !7)
!57 = !DILocalVariable(name: "Ai", arg: 4, scope: !14, file: !1, line: 25, type: !7)
!58 = !DILocalVariable(name: "Ax", arg: 5, scope: !14, file: !1, line: 26, type: !5)
!59 = !DILocalVariable(name: "Rs", arg: 6, scope: !14, file: !1, line: 28, type: !5)
!60 = !DILocalVariable(name: "W", arg: 7, scope: !14, file: !1, line: 30, type: !7)
!61 = !DILocalVariable(name: "Common", arg: 8, scope: !14, file: !1, line: 32, type: !17)
!62 = !DILocalVariable(name: "a", scope: !14, file: !1, line: 35, type: !6)
!63 = !DILocalVariable(name: "Az", scope: !14, file: !1, line: 36, type: !5)
!64 = !DILocalVariable(name: "row", scope: !14, file: !1, line: 37, type: !8)
!65 = !DILocalVariable(name: "col", scope: !14, file: !1, line: 37, type: !8)
!66 = !DILocalVariable(name: "p", scope: !14, file: !1, line: 37, type: !8)
!67 = !DILocalVariable(name: "pend", scope: !14, file: !1, line: 37, type: !8)
!68 = !DILocalVariable(name: "check_duplicates", scope: !14, file: !1, line: 37, type: !8)
!69 = !DILocation(line: 22, column: 9, scope: !14)
!70 = !DILocation(line: 23, column: 9, scope: !14)
!71 = !DILocation(line: 24, column: 9, scope: !14)
!72 = !DILocation(line: 25, column: 9, scope: !14)
!73 = !DILocation(line: 26, column: 12, scope: !14)
!74 = !DILocation(line: 28, column: 12, scope: !14)
!75 = !DILocation(line: 30, column: 9, scope: !14)
!76 = !DILocation(line: 32, column: 17, scope: !14)
!77 = !DILocation(line: 43, column: 16, scope: !78)
!78 = distinct !DILexicalBlock(scope: !14, file: !1, line: 43, column: 9)
!79 = !DILocation(line: 43, column: 9, scope: !14)
!80 = !DILocation(line: 47, column: 13, scope: !14)
!81 = !DILocation(line: 47, column: 20, scope: !14)
!82 = !{!83, !87, i64 76}
!83 = !{!"klu_common_struct", !84, i64 0, !84, i64 8, !84, i64 16, !84, i64 24, !84, i64 32, !87, i64 40, !87, i64 44, !87, i64 48, !88, i64 56, !88, i64 64, !87, i64 72, !87, i64 76, !87, i64 80, !87, i64 84, !87, i64 88, !87, i64 92, !87, i64 96, !84, i64 104, !84, i64 112, !84, i64 120, !84, i64 128, !84, i64 136, !89, i64 144, !89, i64 152}
!84 = !{!"double", !85, i64 0}
!85 = !{!"omnipotent char", !86, i64 0}
!86 = !{!"Simple C/C++ TBAA"}
!87 = !{!"int", !85, i64 0}
!88 = !{!"any pointer", !85, i64 0}
!89 = !{!"long", !85, i64 0}
!90 = !DILocation(line: 49, column: 15, scope: !91)
!91 = distinct !DILexicalBlock(scope: !14, file: !1, line: 49, column: 9)
!92 = !DILocation(line: 49, column: 9, scope: !14)
!93 = !DILocation(line: 36, column: 12, scope: !14)
!94 = !DILocation(line: 58, column: 11, scope: !95)
!95 = distinct !DILexicalBlock(scope: !14, file: !1, line: 58, column: 9)
!96 = !DILocation(line: 58, column: 22, scope: !95)
!97 = !DILocation(line: 58, column: 16, scope: !95)
!98 = !DILocation(line: 58, column: 36, scope: !95)
!99 = !DILocation(line: 58, column: 50, scope: !95)
!100 = !DILocation(line: 59, column: 16, scope: !95)
!101 = !DILocation(line: 59, column: 26, scope: !95)
!102 = !DILocation(line: 59, column: 20, scope: !95)
!103 = !DILocation(line: 62, column: 24, scope: !104)
!104 = distinct !DILexicalBlock(scope: !95, file: !1, line: 60, column: 5)
!105 = !DILocation(line: 63, column: 9, scope: !104)
!106 = !DILocation(line: 65, column: 9, scope: !107)
!107 = distinct !DILexicalBlock(scope: !14, file: !1, line: 65, column: 9)
!108 = !{!87, !87, i64 0}
!109 = !DILocation(line: 65, column: 16, scope: !107)
!110 = !DILocation(line: 65, column: 21, scope: !107)
!111 = !DILocation(line: 65, column: 24, scope: !107)
!112 = !DILocation(line: 65, column: 31, scope: !107)
!113 = !DILocation(line: 65, column: 9, scope: !14)
!114 = !DILocation(line: 68, column: 24, scope: !115)
!115 = distinct !DILexicalBlock(scope: !107, file: !1, line: 66, column: 5)
!116 = !DILocation(line: 69, column: 9, scope: !115)
!117 = !DILocation(line: 37, column: 14, scope: !14)
!118 = !DILocation(line: 71, column: 5, scope: !119)
!119 = distinct !DILexicalBlock(scope: !14, file: !1, line: 71, column: 5)
!120 = !DILocation(line: 71, column: 24, scope: !121)
!121 = distinct !DILexicalBlock(scope: !119, file: !1, line: 71, column: 5)
!122 = distinct !{!122, !118, !123}
!123 = !DILocation(line: 79, column: 5, scope: !119)
!124 = !DILocation(line: 73, column: 13, scope: !125)
!125 = distinct !DILexicalBlock(scope: !126, file: !1, line: 73, column: 13)
!126 = distinct !DILexicalBlock(scope: !121, file: !1, line: 72, column: 5)
!127 = !DILocation(line: 73, column: 31, scope: !125)
!128 = !DILocation(line: 73, column: 24, scope: !125)
!129 = !DILocation(line: 73, column: 22, scope: !125)
!130 = !DILocation(line: 73, column: 13, scope: !126)
!131 = !DILocation(line: 76, column: 28, scope: !132)
!132 = distinct !DILexicalBlock(scope: !125, file: !1, line: 74, column: 9)
!133 = !DILocation(line: 77, column: 13, scope: !132)
!134 = !DILocation(line: 88, column: 28, scope: !135)
!135 = distinct !DILexicalBlock(scope: !136, file: !1, line: 88, column: 9)
!136 = distinct !DILexicalBlock(scope: !137, file: !1, line: 88, column: 9)
!137 = distinct !DILexicalBlock(scope: !138, file: !1, line: 86, column: 5)
!138 = distinct !DILexicalBlock(scope: !14, file: !1, line: 85, column: 9)
!139 = !DILocation(line: 85, column: 9, scope: !14)
!140 = !DILocation(line: 37, column: 9, scope: !14)
!141 = !DILocation(line: 88, column: 9, scope: !136)
!142 = !DILocation(line: 90, column: 22, scope: !143)
!143 = distinct !DILexicalBlock(scope: !135, file: !1, line: 89, column: 9)
!144 = !DILocation(line: 95, column: 27, scope: !14)
!145 = !DILocation(line: 98, column: 28, scope: !146)
!146 = distinct !DILexicalBlock(scope: !147, file: !1, line: 98, column: 9)
!147 = distinct !DILexicalBlock(scope: !148, file: !1, line: 98, column: 9)
!148 = distinct !DILexicalBlock(scope: !149, file: !1, line: 97, column: 5)
!149 = distinct !DILexicalBlock(scope: !14, file: !1, line: 96, column: 9)
!150 = !DILocation(line: 96, column: 9, scope: !14)
!151 = !DILocation(line: 98, column: 9, scope: !147)
!152 = !DILocation(line: 100, column: 21, scope: !153)
!153 = distinct !DILexicalBlock(scope: !146, file: !1, line: 99, column: 9)
!154 = !DILocation(line: 104, column: 5, scope: !155)
!155 = distinct !DILexicalBlock(scope: !14, file: !1, line: 104, column: 5)
!156 = !DILocation(line: 104, column: 24, scope: !157)
!157 = distinct !DILexicalBlock(scope: !155, file: !1, line: 104, column: 5)
!158 = !DILocation(line: 106, column: 23, scope: !159)
!159 = distinct !DILexicalBlock(scope: !157, file: !1, line: 105, column: 5)
!160 = !DILocation(line: 106, column: 16, scope: !159)
!161 = !DILocation(line: 37, column: 22, scope: !14)
!162 = !DILocation(line: 107, column: 18, scope: !163)
!163 = distinct !DILexicalBlock(scope: !159, file: !1, line: 107, column: 9)
!164 = !DILocation(line: 37, column: 19, scope: !14)
!165 = !DILocation(line: 107, column: 31, scope: !166)
!166 = distinct !DILexicalBlock(scope: !163, file: !1, line: 107, column: 9)
!167 = !DILocation(line: 107, column: 9, scope: !163)
!168 = !DILocation(line: 109, column: 19, scope: !169)
!169 = distinct !DILexicalBlock(scope: !166, file: !1, line: 108, column: 9)
!170 = !DILocation(line: 110, column: 21, scope: !171)
!171 = distinct !DILexicalBlock(scope: !169, file: !1, line: 110, column: 17)
!172 = !DILocation(line: 110, column: 32, scope: !171)
!173 = !DILocation(line: 110, column: 25, scope: !171)
!174 = !DILocation(line: 113, column: 32, scope: !175)
!175 = distinct !DILexicalBlock(scope: !171, file: !1, line: 111, column: 13)
!176 = !DILocation(line: 114, column: 17, scope: !175)
!177 = !DILocation(line: 116, column: 17, scope: !169)
!178 = !DILocation(line: 118, column: 21, scope: !179)
!179 = distinct !DILexicalBlock(scope: !180, file: !1, line: 118, column: 21)
!180 = distinct !DILexicalBlock(scope: !181, file: !1, line: 117, column: 13)
!181 = distinct !DILexicalBlock(scope: !169, file: !1, line: 116, column: 17)
!182 = !DILocation(line: 118, column: 29, scope: !179)
!183 = !DILocation(line: 118, column: 21, scope: !180)
!184 = !DILocation(line: 121, column: 36, scope: !185)
!185 = distinct !DILexicalBlock(scope: !179, file: !1, line: 119, column: 17)
!186 = !DILocation(line: 122, column: 21, scope: !185)
!187 = !DILocation(line: 125, column: 25, scope: !180)
!188 = !DILocation(line: 126, column: 13, scope: !180)
!189 = !DILocation(line: 128, column: 13, scope: !190)
!190 = distinct !DILexicalBlock(scope: !169, file: !1, line: 128, column: 13)
!191 = !{!84, !84, i64 0}
!192 = !DILocation(line: 35, column: 12, scope: !14)
!193 = !DILocation(line: 129, column: 17, scope: !169)
!194 = !DILocation(line: 132, column: 17, scope: !195)
!195 = distinct !DILexicalBlock(scope: !196, file: !1, line: 130, column: 13)
!196 = distinct !DILexicalBlock(scope: !169, file: !1, line: 129, column: 17)
!197 = !DILocation(line: 132, column: 26, scope: !195)
!198 = !DILocation(line: 133, column: 13, scope: !195)
!199 = !DILocation(line: 134, column: 22, scope: !196)
!200 = !DILocation(line: 137, column: 28, scope: !201)
!201 = distinct !DILexicalBlock(scope: !202, file: !1, line: 135, column: 13)
!202 = distinct !DILexicalBlock(scope: !196, file: !1, line: 134, column: 22)
!203 = !DILocation(line: 137, column: 26, scope: !201)
!204 = !DILocation(line: 138, column: 13, scope: !201)
!205 = !DILocation(line: 107, column: 41, scope: !166)
!206 = distinct !{!206, !167, !207}
!207 = !DILocation(line: 139, column: 9, scope: !163)
!208 = distinct !{!208, !154, !209}
!209 = !DILocation(line: 140, column: 5, scope: !155)
!210 = !DILocation(line: 145, column: 28, scope: !211)
!211 = distinct !DILexicalBlock(scope: !212, file: !1, line: 145, column: 9)
!212 = distinct !DILexicalBlock(scope: !213, file: !1, line: 145, column: 9)
!213 = distinct !DILexicalBlock(scope: !214, file: !1, line: 143, column: 5)
!214 = distinct !DILexicalBlock(scope: !14, file: !1, line: 142, column: 9)
!215 = !DILocation(line: 142, column: 9, scope: !14)
!216 = !DILocation(line: 145, column: 9, scope: !212)
!217 = !DILocation(line: 150, column: 17, scope: !218)
!218 = distinct !DILexicalBlock(scope: !219, file: !1, line: 150, column: 17)
!219 = distinct !DILexicalBlock(scope: !211, file: !1, line: 146, column: 9)
!220 = !DILocation(line: 150, column: 26, scope: !218)
!221 = !DILocation(line: 150, column: 17, scope: !219)
!222 = !DILocation(line: 153, column: 26, scope: !223)
!223 = distinct !DILexicalBlock(scope: !218, file: !1, line: 151, column: 13)
!224 = !DILocation(line: 154, column: 13, scope: !223)
!225 = !DILocation(line: 145, column: 37, scope: !211)
!226 = distinct !{!226, !216, !227}
!227 = !DILocation(line: 155, column: 9, scope: !212)
!228 = !DILocation(line: 0, scope: !14)
!229 = !DILocation(line: 159, column: 1, scope: !14)
