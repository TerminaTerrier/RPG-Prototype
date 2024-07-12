using Godot;
using System;

public interface IAction 
{
	public Move Move {get; set;}
	void Enact();
}
