using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IStatus
{
	public Queue<string> StatusNames { get; set;}
	public int TurnLength { get;  set;}
	void Effect();
}
