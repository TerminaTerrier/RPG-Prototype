using Godot;
using System;

public partial class CharacterController : Node2D
{
    IControllable controlObject;
    public IState ControlObjectState {get;set;}

    public override void _Ready()
    {
        controlObject = GetParent<IControllable>();
    }
    public override void _Input(InputEvent @event)
	{
		
	}
    public override void _PhysicsProcess(double delta)
    {
        if(Input.IsActionPressed("MoveLeft"))
        {
            controlObject.InputHandler(new MoveCommand((IMovable)ControlObjectState, Vector2.Left));
        }
        
        if(Input.IsActionPressed("MoveRight"))
        {
            controlObject.InputHandler(new MoveCommand((IMovable)ControlObjectState, Vector2.Right));
        }

        if(Input.IsActionPressed("MoveUp"))
        {
            controlObject.InputHandler(new MoveCommand((IMovable)ControlObjectState, Vector2.Up));
        }

        if(Input.IsActionPressed("MoveDown"))
        {
            controlObject.InputHandler(new MoveCommand((IMovable)ControlObjectState, Vector2.Down));
        }
        
        if(Input.IsActionJustReleased("MoveLeft") || Input.IsActionJustReleased("MoveRight") || Input.IsActionJustReleased("MoveUp") || Input.IsActionJustReleased("MoveDown"))
        {
            controlObject.InputHandler(new StillCommand((IMovable)ControlObjectState, Vector2.Zero));
        }
    }
}
