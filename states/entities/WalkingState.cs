using Godot;
using System;

public partial class WalkingState : Node, IState, IMovable 
{
	public ITransition FSM { get; set; }
	public VelocityComponent VelocityComponent{ get; set; }

    public WalkingState(VelocityComponent velocityComponent, ITransition fSM)
    {
        VelocityComponent = velocityComponent;
        FSM = fSM;
    }
    public void Enter(Variant StateController)
    {
       
    }

    public void Exit(Variant StateController)
    {
       VelocityComponent.SetVelocity(Vector2.Zero);
    }
    
	public void Move(Vector2 direction)
	{
		VelocityComponent.SetVelocity(direction);
	}

    public void Still(Vector2 direction)
    {
        VelocityComponent.SetVelocity(direction);
    }

    public void Update(Variant StateController, float delta)
    {
        VelocityComponent.Move((CharacterBody2D)StateController);
    }
}
