using System;
using System.Collections.Generic;

public class Label : ILabel{
	public readonly string mName;
	public readonly uint offset;

	public string Name{
		get{ return mName; }
	}

	public uint Offset{
		get{ return offset; }
	}

	public Label(string labelName, uint address){
		mName = labelName;
		offset = address;
	}
}
