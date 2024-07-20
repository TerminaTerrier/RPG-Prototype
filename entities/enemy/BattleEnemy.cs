using Godot;
using System;

public partial class BattleEnemy : Node2D, IDamageable, IEffectable, IDepletable
{
	[Export]
	public Stats enemyStats;
	[Export]
	DamageComponent damageComponent;
	[Export]
	HealthComponent healthComponent;
	[Export]
	StatusHandler statusHandler;
	[Export]
	public SpecialPointComponent spComponent {get; private set;}
	
	public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
		healthComponent.SetMaxHealth(enemyStats.maxHP);
		healthComponent.SetHealth(enemyStats.maxHP);
		spComponent.SetMaxSP(enemyStats.maxSP);
		spComponent.SetSP(10);
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
	   // GD.Print(calculatedDamage);
	   // GD.Print(healthComponent.CurrentHealth);
    }

	public void ChangeStatus(IStatus status)
	{
        statusHandler.SetStatus(status);
	}

    public void Deplete(int cost)
    {
        spComponent.CalculateSP(-cost);
    }
}
