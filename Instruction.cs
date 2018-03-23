using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		private string mName;
		private List<string> mArgs = new List<string>();
		/*
		private readonly Dictionary<string, Action> mFuncs =
				new Dictionary<string, Action>() 
		{
				{"exit", mExit}, {"swap", mSwap}, {"inpt", mInpt},
				{"nop", mNop}, {"pop", mPop}, {"add", mAdd},
				{"sub", mSub}, {"mul", mMul}, {"div", mDiv},
				{"rem", mRem}, {"and", mAnd}, {"or", mOr},
				{"xor", mXor}, {"neg", mNeg}, {"not", mNot},
				{"goto", mGoto}, {"if", mIf}, {"dup", mDup},
				{"print", mPrint}, {"dump", mDump}, {"push", mPush}
		};
		*/
		public Instruction(string[] _args)
		{
				mName = args[0];
				for(var i = 1; i < tmp.Length; i++)
						mArgs.Add(args[i]);
		}
		public uint Bytes
		{ 
				get
				{
						uint rv = 0;
						return rv;
				}
		}
}
