using Godot;
using System;

public partial class StandingState : Node, IState, IMovable, IInspectable
{
	public ITransition FSM { get; set; }
	public VelocityComponent VelocityComponent{ get; set; }
	

	public StandingState(ITransition fSM, VelocityComponent velocityComponent)
	{
		FSM = fSM;
		VelocityComponent = velocityComponent;
		
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

	public void Inspect()
    {
        FSM.TransitionState("InspectingState");
    }

	public void OutputInspectionResult()
	{
		
	}

	public void Update(Variant controller, float delta)
	{
		
	}

    public bool GetInspectionStatus()
    {
       return false;
    }

    public void EndInspection()
    {
       
    }
}
