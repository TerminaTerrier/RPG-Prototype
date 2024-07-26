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
    public IDamageable.DamageAffinity damageAffinity { get; set; }

    public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
		
		
	}

	public void SetStats()
	{
		healthComponent.SetMaxHealth(enemyStats.maxHP);
		healthComponent.SetHealth(enemyStats.maxHP);
		spComponent.SetMaxSP(enemyStats.maxSP);
		spComponent.SetSP(10);

		switch(enemyStats.statClassName)
		{
			case "Earth":
			{
				damageAffinity = IDamageable.DamageAffinity.Earth;
				break;
			}
			case "Wood":
			{
				damageAffinity = IDamageable.DamageAffinity.Wood;
				break;
			}
			case "Metal":
			{
				damageAffinity = IDamageable.DamageAffinity.Metal;
				break;
			}
		}
	}

    public void StartTurn()
	{
       GD.Print("It's my turn!");
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
