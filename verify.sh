#!/usr/bin/env bash

dotnet Lib/ILVerify/ILVerify.dll -r ~/.nuget/packages/netstandard.library/2.0.0/build/netstandard2.0/ref/mscorlib.dll -r ~/.nuget/packages/netstandard.library/2.0.0/build/netstandard2.0/ref/netstandard.dll "$@"
