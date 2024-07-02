using Godot;
using System;

public partial class StillCommand : Node, ICommand
{
	IState _movableState;
	Vector2 _direction;

	public StillCommand(IState movableState, Vector2 direction)
	{
		_movableState = movableState;
		_direction = direction;
	}

	public void Execute()
	{
		if(_movableState is IMovable)
		{
            IMovable movableObject = (IMovable)_movableState;
		    movableObject.Still(_direction);
		}
	}
}
