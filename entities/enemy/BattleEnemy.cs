using Godot;
using System;

public partial class BattleEnemy : Node2D
{
	[Export]
	public Stats enemyStats;
	
	public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
	}

	public override void _Process(double delta)
	{
	}
}
