using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		static public uint mExit(List<string> args)
		{
				if(args.Count > 1) throw new IndexOutOfRangeException();
				return 0xff & ((args.Count > 0) ? Convert.ToUInt32(args[0]) : 0);
		}

		static public uint mSwap(List<string> args)
		{
				if(args.Count > 0) throw new ArgumentException();
				return 1 << 24;
		}

		static public uint mInpt(List<string> args)
		{
				if(args.Count > 0) throw new ArgumentException();
				return 2 << 24;
		}

		static public uint mNop(List<string> args)
		{
				if(args.Count > 0) throw new ArgumentException();
				return 3 << 24;
		}
}
