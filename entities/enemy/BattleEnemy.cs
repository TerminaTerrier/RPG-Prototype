using Godot;
using System;

public partial class BattleEnemy : Node2D
{
	[Export]
	public Stats enemyStats;
	
	public override void _Ready()
	{
        GlobalPosition = new Vector2(0, -325);
	}

	public override void _Process(double delta)
	{
	}
}
