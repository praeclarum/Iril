




all: Repil/IR/Parser.cs


Repil/IR/Parser.cs: Repil/IR/IR.jay Makefile
	./Lib/jay -c -v Repil/IR/IR.jay < ./Lib/skeleton.cs > $@


