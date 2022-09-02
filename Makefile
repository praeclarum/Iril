




all: Iril/IR/Parser.cs

Iril/IR/Parser.cs: Iril/IR/IR.jay Makefile Lib/skeleton.cs.template
	./Lib/jay -c -v Iril/IR/IR.jay < Lib/skeleton.cs.template > $@

clean:
	rm -rf Iril/bin Iril/obj Cli/bin Cli/obj

restore:
	dotnet restore