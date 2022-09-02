# Iril

[![Build Test and Package](https://github.com/praeclarum/Iril/actions/workflows/build.yml/badge.svg)](https://github.com/praeclarum/Iril/actions/workflows/build.yml)

Iril is an LLVM IR to IL converter. That's a fancy way of saying it can compile native code like C to be crossplatform process-independent .NET.

## Installation

```sh
dotnet tool install iril-cli -g
```

(Or `dotnet tool update iril-cli -g` if you already have it installed.)

## Use

Given some code in *HelloWorld.c*:

```c
#include <stdio.h>

int main(int argc, char *argv[])
{
    printf("Hello, world!");
}
```

You can compile that code to *HelloWorld.dll* with Iril:

```sh
iril HelloWorld.c
```

Run the code using `dotnet`:

```sh
dotnet HelloWorld.dll
```

## Links

* [LLVM IR Language Reference](https://llvm.org/docs/LangRef.html)
