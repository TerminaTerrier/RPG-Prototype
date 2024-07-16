using Godot;
using System;

public interface IStatus
{
	public int TurnLength { get;  set;}
	void Effect(Node target);
}
