using System;
using System.IO;
using System.Collections.Generic;

public class Assembler{
	public Dictionary<string,uint> LabelD = new Dictionary<string,uint>();
	public List<Instruction> IL = new List<Instruction>();
	public List<string[]> StrArrayList = new List<string[]>();
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
						LabelD.Add(line,offset);
					}
		
					// IF HERE STRING CONTAINS A NEW INSTRUCTION
					else{
						// split string into string array containing instruction and its possible arguments
						string[] prgs = line.Split(new char[]{' ','\t'},StringSplitOptions.RemoveEmptyEntries);
		
						//IL.Add(new Instruction(prgs));
						StrArrayList.Add(prgs);
						offset += 4;
					}
				}
			}
		}

		//Second Pass replaces labels found in instructions
		// with the address offset from the label dictionary
		foreach(string[] StrArr in StrArrayList){
			for(var i = 0; i < StrArr.Length; i++){
				uint val = 0;
				if(LabelD.TryGetValue(StrArr[i],out val)){
					StrArr[i] = val.ToString();
				}
				else if(StrArr[i].IndexOf("0x") == 0){
					StrArr[i] = Convert.ToUInt32(StrArr[i],16).ToString();
				}
			}
			IL.Add(new Instruction(StrArr));
		}
	}

	public void WriteOutput(){
		using(BinaryWriter bw = new BinaryWriter(File.Open(filename+".out",FileMode.Create))) {
			bw.Write(0xefbeedfe);
			foreach(Instruction I in IL) {
				bw.Write(I.Bytes);
			}
		}											
	}
}
