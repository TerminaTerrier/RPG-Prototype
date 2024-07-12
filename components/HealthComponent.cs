using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Signal]
	public delegate void DeathEventHandler();
	[Signal]
	public delegate void DamageEventHandler();
	public int MaxHealth { get; private set; }
	public int CurrentHealth {get; private set;}
	EventBus EventBus;
	
    public void CalculateHealth(int healthUpdate)
	{
		if(CurrentHealth <= MaxHealth)
		{
			GD.Print(CurrentHealth);
			CurrentHealth = CurrentHealth + healthUpdate;
		}

		if(CurrentHealth <= 0)
		{
			GD.Print("death");
			EmitSignal("Death");
			CurrentHealth = MaxHealth;
		}
		
		if(CurrentHealth <= MaxHealth && CurrentHealth > 0)
		{
			EmitSignal("Damage");
		}
	}

    public void SetHealth(int healthUpdate)
	{
		CurrentHealth = healthUpdate;
	}
	public void SetMaxHealth(int healthUpdate)
	{
		MaxHealth = healthUpdate;
	}
}
