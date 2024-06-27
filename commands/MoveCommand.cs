using Godot;
using System;

public partial class MoveCommand : Node, ICommand
{
	IMovable _movableObject;
	Vector2 _direction;
	public MoveCommand(IMovable movableObject, Vector2 direction)
	{
		_movableObject = movableObject;
		_direction = direction;
		//GD.Print("New Command")
;	}
	//use interfaces to determine what gets executed
	public void Execute()
	{
		_movableObject.Move(_direction);
		//GD.Print("Command Execute");
	}


}
