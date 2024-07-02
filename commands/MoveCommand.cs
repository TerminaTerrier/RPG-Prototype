using Godot;
using System;

public partial class MoveCommand : Node, ICommand
{
	IState _movableObject;
	Vector2 _direction;
	public MoveCommand(IState movableObject, Vector2 direction)
	{
		_movableObject = movableObject;
		_direction = direction;
		//GD.Print("New Command")
;	}
	//use interfaces to determine what gets executed
	public void Execute()
	{
		if(_movableObject is IMovable)
		{
            IMovable movableObject = (IMovable)_movableObject;
		    movableObject.Move(_direction);
		}
		//GD.Print("Command Execute");
	}


}
