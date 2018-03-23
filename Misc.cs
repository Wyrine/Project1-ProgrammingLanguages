using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		public uint mExit()
		{
				if(mArgs.Count > 1) throw new IndexOutOfRangeException();
				return 0xff & ((mArgs.Count > 0) ? Convert.ToUInt32(mArgs[0]) : 0);
		}

		public uint mSwap()
		{
				return 1 << 24;
		}

		public uint mInpt()
		{
				return 2 << 24;
		}

		public uint mNop()
		{
				return 3 << 24;
		}
}
