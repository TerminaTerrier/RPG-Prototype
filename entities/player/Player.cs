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
	[Export]
	HealthComponent healthComponent;
	public IState CurrentState {get; private set;}
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	IState StandingState;
	IState WalkingState;
	IState InspectingState;
	EventBus eventBus;

    public override void _EnterTree()
    {
		inspectArea.Monitorable = true;
    }
    public override void _Ready()
    {
		
        eventBus = GetNode<EventBus>("/root/EventBus");
		

		StandingState = new StandingState(this, velocityComponent);
		WalkingState = new WalkingState(this, velocityComponent);
		InspectingState = new InspectingState(this, inspectArea);

		states.Add("StandingState", StandingState);
		states.Add("WalkingState", WalkingState);
		states.Add("InspectingState", InspectingState);
        
		healthComponent.SetMaxHealth(playerStats.maxHP);
		healthComponent.SetHealth(playerStats.maxHP);

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
		//GD.Print(CurrentState is null);
		CurrentState.Update(this, (float)delta);
	}
    
	public InstanceStats GetInstanceData()
	{
	   int sp = 1;
       var newInstanceData = new InstanceStats(healthComponent.CurrentHealth, sp);

	   return newInstanceData;
	}
}
