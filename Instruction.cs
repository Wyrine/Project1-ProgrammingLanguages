using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		private string mName;
		private List<string> mArgs = new List<string>();

		private readonly Dictionary<string, Func< List<string>, uint> > FUNCS =
				new Dictionary<string, Func< List<string>, uint> >() 
				{
						{"exit", (TEMP) => { return 0xff & ((TEMP.Count > 0) ? Convert.ToUInt32(TEMP[0]) : 0);}}, 
						{"swap", (TEMP) => { return 1 << 24; }}, 
						{"inpt", (TEMP) => { return 2 << 24; }},
						{"nop", (TEMP) => { return 3 << 24; }}, 
						{"pop", (TEMP) => { return 1 << 28; }}, 
						{"add", (TEMP) => { return 2 << 28; }},
						{"sub", (TEMP) => { return 0x21 << 24; }}, 
						{"mul", (TEMP) => { return 0x22 << 24; }}, 
						{"div", (TEMP) => { return 0x23 << 24; }},
						{"rem", (TEMP) => { return 0x24 << 24; }}, 
						{"and", (TEMP) => { return 0x25 << 24; }}, 
						{"or", (TEMP) => { return 0x26 << 24; }},
						{"xor", (TEMP) => { return 0x27 << 24; }}, 
						{"neg", (TEMP) => { return (uint) 3 << 28; }}, 
						{"not", (TEMP) => { return (uint) 0x31 << 24; }},
						{"goto", (TEMP) => { return (~((uint) 8 << 28)) & Convert.ToUInt32(TEMP[0]); }},   
						//{"if", mIf}, 
						{"dup", (TEMP) => 
								{ 
										uint relOffset = (TEMP.Count == 0) ? 0 : Convert.ToUInt32(TEMP[0]);
										return ((uint) 9 << 28) | (relOffset << 2);
								}},
						{"print", (TEMP) => { return (uint) 13 << 28; }}, 
						{"dump", (TEMP) => { return (uint) 0xe << 28; }}, 
						{"push", (TEMP) => 
								{
										uint valToPush = (TEMP.Count == 0 ) ? 0 : Convert.ToUInt32(TEMP[0]);
										return ((uint) 0xf << 29) | valToPush;
								}}
				};

		public Instruction(string[] _args)
		{
				mName = _args[0];
				for(var i = 1; i < _args.Length; i++)
						mArgs.Add(_args[i]);
		}
		public uint Bytes
		{ 
				get
				{ 
						if(! FUNCS.ContainsKey(mName)) 
								throw new KeyNotFoundException();
						return FUNCS[mName](mArgs); 
				}
		}
}
