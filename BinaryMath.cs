using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		public static uint mAdd(List<string> args)
		{
				return 2 << 28;
		}

		public static uint mSub(List<string> args)
		{
				return 0x21 << 24;
		}

		public static uint mMul(List<string> args)
		{
				return 0x22 << 24;
		}

		public static uint mDiv(List<string> args)
		{
				return 0x23 << 24;
		}

		public static uint mRem(List<string> args)
		{
				return 0x24 << 24;
		}

		public static uint mAnd(List<string> args)
		{
				return 0x25 << 24;
		}

		public static uint mOr(List<string> args)
		{
				return 0x26 << 24;
		}

		public static uint mXor(List<string> args)
		{
				return 0x27 << 24;
		}
}
