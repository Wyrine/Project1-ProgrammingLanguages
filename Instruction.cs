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
*/


public class Instruction : IInstruction
{
		public readonly string mName;
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
						{"exit", (inst) => { return (uint)0xff & ((inst.Count > 0) ? Convert.ToUInt32(inst[0]) : 0);}}, 
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
						{"goto", (inst) => { return (~((uint) 8 << 28)) & Convert.ToUInt32(inst[0]); }},   
						{"dup", (inst) => 
								{ 
										uint relOffset = (inst.Count == 0) ? 0 : Convert.ToUInt32(inst[0]);
										return ((uint) 9 << 28) | (relOffset << 2);
								}},
						{"print", (inst) => { return (uint) 13 << 28; }}, 
						{"dump", (inst) => { return (uint) 0xe << 28; }}, 
						{"push", (inst) => 
								{
										uint valToPush = (inst.Count == 0 ) ? 0 : Convert.ToUInt32(inst[0]);
										return ((uint) 0xf << 29) | valToPush;
								}}
				};

		public Instruction(string[] _args)
		{
				//first argument is the name
				mName = _args[0];
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

						//if the key is not in the dictionary throw an error
						if(! FUNCS.ContainsKey(mName)) 
								throw new KeyNotFoundException();

						//otherwise call the dictionary method and return what it returns
						return FUNCS[mName](this); 
				}
		}

		//the if instruction
		private uint if_block()
		{
				//get the condition
				string tmp = mName.Substring(2, 4);
				uint cond = 0;
				uint opcode = 8;
				//get the proper opcode and condition
				switch(tmp)
				{
						case "eq":
								break;
						case "ne":
								cond = 1;
								break;
						case "lt":
								cond = 2;
								break;
						case "gt":
								cond = 3;
								break;
						case "le":
								cond = 4;
								break;
						case "ge":
								cond = 5;
								break;
						case "ez":
								opcode++;
								break;
						case "nz":
								opcode++;
								cond = 1;
								break;
						case "mi":
								opcode++;
								cond = 2;
								break;
						case "pl":
								opcode++;
								cond = 3;
								break;
						default:
								break;
				}
				//return the byte
				return (opcode << 28) | (cond << 24) | Convert.ToUInt32(mArgs[0]) ;
		}
}
