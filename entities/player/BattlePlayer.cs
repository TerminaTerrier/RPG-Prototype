using Godot;
using System;

public partial class BattlePlayer : Node2D
{
	[Export]
	public Stats playerStats;
	
	public override void _Ready()
	{
		GlobalPosition = new Vector2(-550, 6);
	}

	
	public override void _Process(double delta)
	{
	}
}
