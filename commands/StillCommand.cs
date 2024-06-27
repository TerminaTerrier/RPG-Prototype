using Godot;
using System;

public partial class StillCommand : Node, ICommand
{
	IMovable _movableObject;
	Vector2 _direction;

	public StillCommand(IMovable movableObject, Vector2 direction)
	{
		_movableObject = movableObject;
		_direction = direction;
	}

	public void Execute()
	{
		_movableObject.Still(_direction);
	}
}
