using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D, IControllable, ITransition
{
	[Export]
	public Stats playerStats;
	[Export]
	CharacterController characterController;
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	InspectArea inspectArea;
	[Export]
	HealthComponent healthComponent;
	[Export]
	SpecialPointComponent spComponent;
	public IState CurrentState {get; private set;}
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	IState StandingState;
	IState WalkingState;
	IState InspectingState;
	EventBus eventBus;

    public override void _EnterTree()
    {
		var dir = DirAccess.Open("user://");
		
		if(dir.FileExists("user://BattlePlayerInstanceData.tres"))
		{ 
			GD.Print("Instance Data found. Setting values.");
			GD.Print(ResourceLoader.Load<InstanceStats>("user://BattlePlayerInstanceData.tres") is null);
            var instanceData = ResourceLoader.Load<InstanceStats>("user://BattlePlayerInstanceData.tres");

		    if(instanceData.Health == 0 | instanceData.SP == 0)
		    {
			    spComponent.SetSP(spComponent.MaxSP);
			    healthComponent.SetHealth(healthComponent.MaxHealth);
		    }
		    else
		    {
			    healthComponent.SetHealth(instanceData.Health);
			    spComponent.SetSP(instanceData.SP);	
		    }

			GD.Print("INSTANCE DATA HEALTH: " + instanceData.Health);
		    GD.Print("INSTANCE DATA SP: " + instanceData.SP);
		}

		GD.Print("HEALTH ON ENTERING TREE: " + healthComponent.CurrentHealth);

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
		spComponent.SetMaxSP(playerStats.maxSP);
		
		var dir = DirAccess.Open("user://");
		
		if(!dir.FileExists("user://BattlePlayerInstanceData.tres"))
		{
			GD.Print("No InstanceData. Setting default values");
		    healthComponent.SetHealth(playerStats.maxHP);
		    spComponent.SetSP(playerStats.maxSP);
		}

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
    
	public void SaveInstanceData()
	{
       InstanceStats newInstanceData = new InstanceStats(healthComponent.CurrentHealth, spComponent.CurrentSP);

	   ResourceSaver.Save(newInstanceData, "user://PlayerInstanceData.tres");
	   GD.Print("Player health is: " + newInstanceData.Health);
	}
}
