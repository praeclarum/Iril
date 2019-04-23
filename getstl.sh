set -e
set -x

if [ ! -f ${TMPDIR}stl.zip ]; then
    wget -O ${TMPDIR}stl.zip https://github.com/llvm/llvm-project/archive/release/8.x.zip
fi

unzip -n ${TMPDIR}stl.zip  -x /clang-tools-extra/* clang compiler-rt debuginfo-tests libclc libcxxabi libunwind polly llvm -d ${TMPDIR}stlwork

pushd ${TMPDIR}stlwork/llvm-project-release-8.x/libcxx/src

clang -g -O1 -S -emit-llvm -std=c++17 -frtti -fpic -D_LIBCPP_BUILDING_LIBRARY *.cpp

zip libcxx.zip *.ll

popd

mkdir -p ./Iril/Lib
cp ${TMPDIR}stlwork/llvm-project-release-8.x/libcxx/src/libcxx.zip ./Iril/Lib/
