




all: Repil/IR/Parser.cs


Repil/IR/Parser.cs: Repil/IR/IR.jay Makefile Lib/skeleton.cs.template
	./Lib/jay -c -v Repil/IR/IR.jay < Lib/skeleton.cs.template > $@


