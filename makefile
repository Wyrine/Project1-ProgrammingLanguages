all: Assembly.exe

Assembly.exe: *.cs
		mcs -out:Assembly.exe *.cs

clean:
		rm -rf *.exe
