using Godot;
using System;

public partial class BattleEnemy : Node2D, IDamageable
{
	[Export]
	public Stats enemyStats;
	[Export]
	DamageComponent damageComponent;
	[Export]
	HealthComponent healthComponent;
	
	public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
		healthComponent.SetMaxHealth(enemyStats.maxHP);
		healthComponent.SetHealth(enemyStats.maxHP);
	}

    public void TakeDamage(int damage)
    {
       var calculatedDamage = damageComponent.CalculateDamageTaken(damage, enemyStats);
	   healthComponent.CalculateHealth(-calculatedDamage);
	   GD.Print(calculatedDamage);
	   GD.Print(healthComponent.CurrentHealth);
    }
}
