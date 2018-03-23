using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		static public uint mExit(List<string> mArgs)
		{
				if(mArgs.Count > 1) throw new IndexOutOfRangeException();
				return 0xff & ((mArgs.Count > 0) ? Convert.ToUInt32(mArgs[0]) : 0);
		}

		static public uint mSwap(List<string> mArgs)
		{
				return 1 << 24;
		}

		static public uint mInpt(List<string> mArgs)
		{
				return 2 << 24;
		}

		static public uint mNop(List<string> mArgs)
		{
				return 3 << 24;
		}
}
