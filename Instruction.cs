using System;
using System.Collections.Generic;

/*
	 Written by: Kirolos Shahat
	 Instruction Class which implements the IInstruction interface
	 used to convert an instruction into the byte code

		Methods:
				Constructor(string[])
				string indexer[int]
		Properties:
				int Count
				uint Bytes
		Variables:
				readonly string mName
				readonly uint Address
 */

public class Instruction : IInstruction
{
		public readonly string mName;
		public readonly uint Address;
		private List<string> mArgs = new List<string>();

		//return the mArgs value at index i
		public string this[int i]
		{
				get{ return mArgs[i]; }
		}

		//return the count of the mArgs array
		public int Count
		{
				get { return mArgs.Count; }
		}

		//static dictionary of functions to call based off of a label
		private static Dictionary<string, Func< Instruction, uint> > FUNCS =
				new Dictionary<string, Func<Instruction, uint> >() 
				{
						{"exit", (inst) => { return (uint)0xff & ((inst.Count > 0) ? (uint)Convert.ToInt32(inst[0]) : 0);}}, 
						{"swap", (inst) => { return (uint)1 << 24; }}, 
						{"inpt", (inst) => { return (uint)2 << 24; }},
						{"nop", (inst) => { return (uint)3 << 24; }}, 
						{"pop", (inst) => { return (uint)1 << 28; }}, 
						{"add", (inst) => { return (uint)2 << 28; }},
						{"sub", (inst) => { return (uint)0x21 << 24; }}, 
						{"mul", (inst) => { return (uint)0x22 << 24; }}, 
						{"div", (inst) => { return (uint)0x23 << 24; }},
						{"rem", (inst) => { return (uint)0x24 << 24; }}, 
						{"and", (inst) => { return (uint)0x25 << 24; }}, 
						{"or", (inst) => { return (uint)0x26 << 24; }},
						{"xor", (inst) => { return (uint)0x27 << 24; }}, 
						{"neg", (inst) => { return (uint) 3 << 28; }}, 
						{"not", (inst) => { return (uint) 0x31 << 24; }},
						{"goto", (inst) => 
								{ 
										int relOffset = Convert.ToInt32(inst[0]) - (int) inst.Address;
										return (~((uint) 8 << 28)) & ((7 << 28) | (uint) relOffset); 
								}},   
						{"dup", (inst) => 
								{ 
										uint relOffset = (inst.Count == 0) ? 0 : Convert.ToUInt32(inst[0]);
										return  ((uint) 0xc << 28) | (0xfffffff & (relOffset << 2));
								}},
						{"print", (inst) => { return (uint) 13 << 28; }}, 
						{"dump", (inst) => { return (uint) 0xe << 28; }}, 
						{"push", (inst) => 
								{
										uint valToPush = (inst.Count == 0 ) ? 0 : (uint) Convert.ToInt32(inst[0]);
										return ((uint) 0xf << 28) | valToPush;
								}}
				};

		public Instruction(uint _addr, string[] _args)
		{
				//first argument is the name
				mName = _args[0];
				Address = _addr;
				//the rest, if they exist, are the arguments
				for(var i = 1; i < _args.Length; i++)
						mArgs.Add(_args[i]);
		}
		public uint Bytes
		{
				get
				{ 
						//calls the if function;
						if(mName.Substring(0, 2) == "if")
								return if_block();

						//otherwise if the key is not in the dictionary throw an error
						if(! FUNCS.ContainsKey(mName))
								throw new KeyNotFoundException();

						//otherwise call the dictionary method and return what it returns
						return FUNCS[mName](this); 
				}
		}

		//the if instruction
		private uint if_block()
		{
				uint cond = 0;
				uint opcode = 8;
				int relOffset = Convert.ToInt32(mArgs[0]) - ((int) Address);
				//get the proper opcode and condition
				switch(mName)
				{
						case "ifeq":
								break;
						case "ifne":
								cond = 1;
								break;
						case "iflt":
								cond = 2;
								break;
						case "ifgt":
								cond = 3;
								break;
						case "ifle":
								cond = 4;
								break;
						case "ifge":
								cond = 5;
								break;
						case "ifez":
								opcode++;
								break;
						case "ifnz":
								opcode++;
								cond = 1;
								break;
						case "ifmi":
								opcode++;
								cond = 2;
								break;
						case "ifpl":
								opcode++;
								cond = 3;
								break;
						default:
								break;
				}
				//return the byte
				return (opcode << 28) | (cond << 24) | ((uint)0xffffff & (uint) relOffset) ;
		}
}
