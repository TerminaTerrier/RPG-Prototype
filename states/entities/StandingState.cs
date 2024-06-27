using Godot;
using System;

public partial class StandingState : Node, IState, IMovable
{
	public ITransition FSM { get; set; }
	public VelocityComponent VelocityComponent{ get; set; }

	public StandingState(VelocityComponent velocityComponent, ITransition fSM)
	{
		VelocityComponent = velocityComponent;
		FSM = fSM;
	}
	
	public void Enter(Variant controller)
	{
		
	}

	public void Exit(Variant controller)
	{
		
	}

	public void Move(Vector2 direction)
	{
		FSM.TransitionState("WalkingState");
	}

	public void Still(Vector2 direction)
    {

    }

	public void Update(Variant controller, float delta)
	{
		
	}
}
