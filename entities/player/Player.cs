using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D, IControllable, ITransition
{
	[Export]
	Stats playerStats;
	[Export]
	CharacterController characterController;
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	InspectArea inspectArea;
	public IState CurrentState {get; private set;}
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	IState StandingState;
	IState WalkingState;
	IState InspectingState;

	public override void _Ready()
    {
		StandingState = new StandingState(this, velocityComponent);
		WalkingState = new WalkingState(this, velocityComponent);
		InspectingState = new InspectingState(this, inspectArea);

		states.Add("StandingState", StandingState);
		states.Add("WalkingState", WalkingState);
		states.Add("InspectingState", InspectingState);

	    CurrentState = StandingState;
		characterController.ControlObjectState = CurrentState;
		inspectArea._inspectableObject = (IInspectable)CurrentState;
        CurrentState.Enter(this);
    }

    public void InputHandler(ICommand command)
	{
		command.Execute();
	}
	
	public void TransitionState(string key)
	{
		if (!states.ContainsKey(key) || CurrentState == states[key])
		{
			return;
		}

        if(CurrentState != null)
        {
            CurrentState.Exit(this);
        }
        
        CurrentState = states[key];
		characterController.ControlObjectState = CurrentState;

		if(CurrentState is IInspectable)
		{
		    inspectArea._inspectableObject = (IInspectable)CurrentState;
		}

        CurrentState.Enter(this);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		CurrentState.Update(this, (float)delta);
	}
}
