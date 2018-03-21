using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		private string mName;
		private List<string> mArgs;
		public Instruction(string _instr)
		{
				var tmp = _instr.Split(' ');
				mName = tmp[0].ToLower();
				for( var i = 1; i < tmp.Length; i++ )
						mArgs.Add(tmp[i]);
		}
		public uint Bytes
		{ 
				get
				{
						uint rv = 0;
						switch(mName)
						{
								case "exit":
										break;
								case "swap":
										break;
								case "inpt":
										break;
								case "nop":
										break;
								case "pop":
										break;
								case "add":
										break;
								case "sub":
										break;
								case "mul":
										break;
								case "div":
										break;
								case "rem":
										break;
								case "and":
										break;
								case "or":
										break;
								case "xor":
										break;
								case "neg":
										break;
								case "not":
										break;
								case "goto":
										break;
								case "ifeq":
										break;
								case "ifne":
										break;
								case "iflt":
										break;
								case "ifgt":
										break;
								case "ifle":
										break;
								case "ifge":
										break;
								case "ifez":
										break;
								case "ifnz":
										break;
								case "ifmi":
										break;
								case "ifpl":
										break;
								case "dup":
										break;
								case "print":
										break;
								case "dump":
										break;
								case "push":
										break;
						}
						return rv;
				}
		}
}
