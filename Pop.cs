using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		public static uint mPop(List<string> args)
		{
				if(args.Count > 0) throw new ArgumentException();
				return 1 << 28;
		}

}
