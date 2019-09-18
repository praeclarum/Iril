# Iril

[![Build Status](https://app.bitrise.io/app/215cbf7fe9a1fad5/status.svg?token=K4l3jgu8fAUWcExrQsRTtQ&branch=master)](https://app.bitrise.io/app/215cbf7fe9a1fad5)

IR to IL converter.

## Installation

```sh
dotnet tool install iril-cli -g
```

## Use

Given some code in *HelloWorld.c*:

```c
#include <stdio.h>

int main(int argc, char *argv[]) {
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
