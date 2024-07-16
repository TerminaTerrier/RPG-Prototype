using Godot;
using System;

public partial class BattleEnemy : Node2D, IDamageable, IEffectable
{
	[Export]
	public Stats enemyStats;
	[Export]
	DamageComponent damageComponent;
	[Export]
	HealthComponent healthComponent;
	[Export]
	StatusHandler statusHandler;
	
	public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
		healthComponent.SetMaxHealth(enemyStats.maxHP);
		healthComponent.SetHealth(enemyStats.maxHP);
	}

    public void StartTurn()
	{

	}

	public void EndTurn()
	{

	}
	
    public void TakeDamage(int damage)
    {
        var calculatedDamage = damageComponent.CalculateDamageTaken(damage, enemyStats);
	    healthComponent.CalculateHealth(-calculatedDamage);
	    GD.Print(calculatedDamage);
	    GD.Print(healthComponent.CurrentHealth);
    }

	public void ChangeStatus(IStatus status)
	{
        statusHandler.SetStatus(status);
	}
}
