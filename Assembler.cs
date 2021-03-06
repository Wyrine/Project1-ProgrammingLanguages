using System;
using System.IO;
using System.Collections.Generic;

public class Assembler{
	public Dictionary<string,Label> LabelD = new Dictionary<string,Label>();
	public List<Instruction> IL = new List<Instruction>();
	public Dictionary<uint,string[]> StrArrayList = new Dictionary<uint,string[]>();
	private uint offset = 0;
	private string filename;

	public Assembler(string fn){
		filename = fn;
		using(StreamReader sr = new StreamReader(fn)){
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
						
						// MAKE NEW LABEL INSTANCE //
						
						LabelD.Add(line,new Label(line,offset));
					}
		
					// IF HERE STRING CONTAINS A NEW INSTRUCTION
					else{
						// split string into string array containing instruction and its possible arguments
						string[] prgs = line.Split(new char[]{' ','\t'},StringSplitOptions.RemoveEmptyEntries);
		
						StrArrayList.Add(offset,prgs);
						offset += 4;
					}
				}
			}
		}

		// Second Pass through instructions
		foreach(KeyValuePair<uint,string[]> kvp in StrArrayList){
			for(var i = 1; i < kvp.Value.Length; i++){
				Label lval;
				
				// Replace label arguments with their offset
				if(LabelD.TryGetValue(kvp.Value[i],out lval)){
					kvp.Value[i] = lval.Offset.ToString();
				}

				// If argument is in hex replace with decimal value
				else if(kvp.Value[i].IndexOf("0x") == 0){
					kvp.Value[i] = Convert.ToUInt32(kvp.Value[i],16).ToString();
				}
			}

			// Create new instance of an instruction and put into list
			IL.Add(new Instruction(kvp.Key, kvp.Value));
		}
	}

	// Writes the "magic header" and then all of the instructions
	public void WriteOutput(){
		using(BinaryWriter bw = new BinaryWriter(File.Open(filename+".out",FileMode.Create))) {
			bw.Write(0xefbeedfe);
			foreach(Instruction I in IL) {
				bw.Write(I.Bytes);
			}
		}											
	}
}
