using System;
using System.Collections.Generic;

public class Label : ILabel{
	public readonly string mName;
	public readonly uint offset;

	// Name property returns Label name
	public string Name{
		get{ return mName; }
	}

	// Offset property returns address to the
	// instruction the label refers to
	public uint Offset{
		get{ return offset; }
	}

	// Construction for Label instance to
	// set name and offset
	public Label(string labelName, uint address){
		mName = labelName;
		offset = address;
	}
}
