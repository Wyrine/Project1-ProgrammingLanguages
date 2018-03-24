all: Assembler.exe

Assembler.exe: *.cs
		mcs -out:Assembler.exe *.cs

clean:
		rm -rf *.exe
