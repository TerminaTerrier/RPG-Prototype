using Godot;
using System;

public interface IMovable
{
	void Move(Vector2 direction);
	void Still(Vector2 direction);
}
