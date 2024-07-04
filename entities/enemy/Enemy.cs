using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public Stats enemyStats;
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	InspectArea inspectArea;
	public IState CurrentState {get; private set;}
	private Dictionary<string, IState> states = new Dictionary<string, IState>();
	EventBus eventBus;

	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
	}

	public override void _Process(double delta)
	{
         
	}

	public void OnBodyEntered(Node2D body)
	{
        if(body.Name == "Player")
		{
			eventBus.EmitSignal(EventBus.SignalName.StartBattle);
		}
	}
}
