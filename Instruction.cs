using System;
using System.Collections.Generic;

public partial class Instruction : IInstruction
{
		private string mName;
		private List<string> mArgs = new List<string>();
		
		private readonly Dictionary<string, Func< List<string>, uint> > FUNCS =
				new Dictionary<string, Func< List<string>, uint> >() 
		{
				{"exit", mExit}, {"swap", mSwap}, {"inpt", mInpt},
				{"nop", mNop}};/* , {"pop", mPop}, {"add", mAdd},
				{"sub", mSub}, {"mul", mMul}, {"div", mDiv},
				{"rem", mRem}, {"and", mAnd}, {"or", mOr},
				{"xor", mXor}, {"neg", mNeg}, {"not", mNot},
				{"goto", mGoto}, {"if", mIf}, {"dup", mDup},
				{"print", mPrint}, {"dump", mDump}, {"push", mPush}
		};*/
		
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
						return FUNCS[mName](mArgs);
				}
		}
}
