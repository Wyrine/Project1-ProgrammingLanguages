using System;
using System.IO;
using System.Collections.Generic; 

class Assembler{
	static int Main(string[] args){
	List<Instruction> IL = new List<Instruction>();

		// Check that filename was given on command line
		if(args.Length == 0){
			System.Console.WriteLine($"Usage: {System.AppDomain.CurrentDomain.FriendlyName} <filename>"); 
			return 1;
		}

		using(StreamReader sr = new StreamReader(args[0])){
			while(sr.Peek() >= 0){
				var line = sr.ReadLine();
				string[] delimiters = new string[] { "//", "#" };
				
				// IF HERE SPLIT ON COMMENT CHARS AND TAKE FIRST ENTRY
				// IF COMMENTS ARE AT END OF LINE THIS WILL RETURN STRING WITHOUT COMMENTS
				// IF THE ENTIRE LINE IS COMMENTS THEN STRING WILL ONLY CONTAIN WHITESPACE
				if(line.IndexOf("//") >= 0 || line.IndexOf("#") >= 0){
					line = line.Split(delimiters,2,StringSplitOptions.None)[0];
				}
				
				// IF TRUE STRING STILL HAS NON-WHITESPACE CHARS
				if(!String.IsNullOrWhiteSpace(line)){
					// remove any leading or trailing whitespace
					line = line.Trim().ToLower();

					// IF HERE STRING CONTAINS A NEW LABEL
					if(line.IndexOf(":") > 0){
						line = line.Split(':')[0];
					}

					// IF HERE STRING CONTAINS A NEW INSTRUCTION
					else{
						// split string into string array containing instruction and its possible arguments
						string[] prgs = line.Split(new char[]{' ','\t'},StringSplitOptions.RemoveEmptyEntries);
						
						IL.Add(new Instruction(prgs));											

						//THIS BLOCK IS TO TEST ALL INSTRUCTIONS AND ARGUMENTS WERE SAVED IN ARRAY
						//foreach(var prg in prgs){
						//	Console.Write($"{prg} ");
						//}
						//Console.WriteLine();
					}
				}
			}
		}

		//THIS BLOCK TESTS THAT ALL INSTRUCTIONS WERE PUT INTO LIST
		// note: mName and mArgs have to be made public
		// note: will change list to dictionary once ready to print instructions
	//	foreach(Instruction instr in IL){
	//		Console.Write($"{instr.mName} ");
	//		foreach(string arg in instr.mArgs){
	//			Console.Write($"{arg} ");
	//		}
	//		Console.WriteLine();
	//	}

		return 0;
	}
}
