using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D, IControllable, ITransition
{
	[Export]
	Resource playerStats;
	[Export]
	CharacterController characterController;
	[Export]
	VelocityComponent velocityComponent;
	public IState CurrentState {get; private set;}
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	IState StandingState;
	IState WalkingState;
	public override void _Ready()
    {
		StandingState = new StandingState(velocityComponent, this);
		WalkingState = new WalkingState(velocityComponent, this);

		states.Add("StandingState", StandingState);
		states.Add("WalkingState", WalkingState);

	    CurrentState = StandingState;
		characterController.ControlObjectState = CurrentState;
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
        CurrentState.Enter(this);
	}
	public override void _PhysicsProcess(double delta)
	{
		CurrentState.Update(this, (float)delta);
	}
}
