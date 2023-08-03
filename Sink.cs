using System;
using System.Diagnostics;

public sealed class Sink 
{
	public string SystemName { private set; get; }
	public string FriendlyName { private set; get; }

	public Sink(string[] groups) 
	{
		this.SystemName = groups[0];
		this.FriendlyName = groups[1];
	}
}