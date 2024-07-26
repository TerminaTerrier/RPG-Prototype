using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export]
	public string parentEntityName;
	public int MaxHealth { get; private set; }
	public int CurrentHealth {get; private set;}
	EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
    }
	
    public void CalculateHealth(int healthUpdate)
	{
		if(CurrentHealth <= MaxHealth)
		{
			GD.Print(CurrentHealth);
			CurrentHealth = CurrentHealth + healthUpdate;

			if(CurrentHealth > MaxHealth)
			{
				CurrentHealth = MaxHealth;
			}

			if(CurrentHealth <= 0)
		    {
			    GD.Print("death");
			    CurrentHealth = 0;
		    }

			if(parentEntityName == "Player")
			{
			    eventBus.EmitSignal(EventBus.SignalName.PlayerHealthUpdated, CurrentHealth);
			}
			else if(parentEntityName == "Enemy")
			{
				eventBus.EmitSignal(EventBus.SignalName.EnemyHealthUpdated, CurrentHealth);
			}
		}

		
		
		if(CurrentHealth <= MaxHealth && CurrentHealth > 0)
		{
			
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
