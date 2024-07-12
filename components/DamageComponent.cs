using Godot;
using System;

public partial class DamageComponent : Node
{
	public int CalculateDamageTaken(int damage, Stats stats)
	{
       return damage - stats.defense;
	}
}
