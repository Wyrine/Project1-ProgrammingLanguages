using System;
using System.IO;

class Assembler{
	static int Main(string[] args){
		

		if(args.Length == 0){
			System.Console.WriteLine($"Usage: {System.AppDomain.CurrentDomain.FriendlyName} <filename>"); 
			return 1;
		}

		using(StreamReader sr = new StreamReader(args[0])){
			while(sr.Peek() >= 0){
				var line = sr.ReadLine();
				string[] delimiters = { "//", "#" };
				if(!String.IsNullOrWhiteSpace(line)){
					line = line.Trim().ToLower();
					if(line.IndexOf("//") >= 0 || line.IndexOf("#") >= 0){
						line = line.Split(delimiters,2,StringSplitOptions.RemoveEmptyEntries)[0];
					}
					if(line.IndexOf(":") > 0){
						line = line.Split(':')[0];
						//Console.WriteLine(line);
					}
					else{
						//Console.WriteLine(line);
					}
				}
			}
		}
		
		return 0;
	}
}
