using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		public readonly string Name;
		private List<string> mArgs = new List<string>();

		public string this[int i]
		{
				get{ return mArgs[i]; }
		}
		public int Count
		{
				get { return mArgs.Count; }
		}

		public readonly Dictionary<string, Func< Instruction, uint> > FUNCS =
				new Dictionary<string, Func<Instruction, uint> >() 
				{
						{"exit", (inst) => { return 0xff & ((inst.Count > 0) ? Convert.ToUInt32(inst[0]) : 0);}}, 
						{"swap", (inst) => { return 1 << 24; }}, 
						{"inpt", (inst) => { return 2 << 24; }},
						{"nop", (inst) => { return 3 << 24; }}, 
						{"pop", (inst) => { return 1 << 28; }}, 
						{"add", (inst) => { return 2 << 28; }},
						{"sub", (inst) => { return 0x21 << 24; }}, 
						{"mul", (inst) => { return 0x22 << 24; }}, 
						{"div", (inst) => { return 0x23 << 24; }},
						{"rem", (inst) => { return 0x24 << 24; }}, 
						{"and", (inst) => { return 0x25 << 24; }}, 
						{"or", (inst) => { return 0x26 << 24; }},
						{"xor", (inst) => { return 0x27 << 24; }}, 
						{"neg", (inst) => { return (uint) 3 << 28; }}, 
						{"not", (inst) => { return (uint) 0x31 << 24; }},
						{"goto", (inst) => { return (~((uint) 8 << 28)) & Convert.ToUInt32(inst[0]); }},   
						{"if", if_block}, 
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
				Name = _args[0];
				for(var i = 1; i < _args.Length; i++)
						mArgs.Add(_args[i]);
		}
		public uint Bytes
		{ 
				get
				{ 
						string key = Name;
						if(Name.Substring(0, 2) == "if") key = "if";
						if(! FUNCS.ContainsKey(key)) 
								throw new KeyNotFoundException();
						return FUNCS[key](this); 
				}
		}
		public static uint if_block(Instruction inst)
		{
				string tmp = inst.Name.Substring(2, 4);
				uint cond = 0;
				uint opcode = 8;
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

				return (opcode << 28) | (cond << 24) | Convert.ToUInt32(inst[0]) ;
		}
}
