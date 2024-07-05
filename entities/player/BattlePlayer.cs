using Godot;
using System;

public partial class BattlePlayer : Node2D
{
	[Export]
	public Stats playerStats;
	public InstanceStats PlayersInstanceStats {get; private set;}


    public override void _Ready()
	{
		
		GlobalPosition = new Vector2(-300, 165);
	}
    
	public void SetPlayerInstanceValues(InstanceStats instanceStats)
	{
		PlayersInstanceStats = instanceStats;
		GD.Print(PlayersInstanceStats.Health);
	}
	
	public override void _Process(double delta)
	{
	}
}
