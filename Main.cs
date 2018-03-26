using System;
using System.IO;
using System.Collections.Generic; 

class MyMain{
	static int Main(string[] args){
		Assembler ASM;

		// Check that filename was given on command line
		if(args.Length == 0){
			System.Console.WriteLine($"Usage: {System.AppDomain.CurrentDomain.FriendlyName} <filename>"); 
			return 1;
		}
		else{
			ASM = new Assembler(args[0]);
		}

		ASM.WriteOutput();
		return 0;
	}
}
