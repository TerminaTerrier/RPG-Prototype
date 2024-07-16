using Godot;
using System;

public partial class BattlePlayer : Node2D, IDamageable, IEffectable
{
	[Export]
	HealthComponent healthComponent;
	[Export]
	DamageComponent damageComponent;
	[Export]
	StatusHandler statusHandler;
	[Export]
	public Stats playerStats;
	public Moveset Moveset { get; private set; }
	public InstanceStats PlayersInstanceStats {get; private set;}
	public bool ActionLocked {get; set;}
	EventBus _eventBus;
    
    public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");

		healthComponent.SetMaxHealth(playerStats.maxHP);

		GlobalPosition = new Vector2(-300, 165);
	}

	public void StartTurn()
	{
        _eventBus.EmitSignal(EventBus.SignalName.PlayerTurnStarted, Moveset);
		GD.Print(healthComponent.CurrentHealth);

		statusHandler.CheckStatus();	

		if(ActionLocked)
		{
			EndTurn();
		}	
	}

	public void EndTurn()
	{
		ActionLocked = false;
        _eventBus.EmitSignal(EventBus.SignalName.TurnEnded);
	}
    
	public void SetPlayerInstanceValues(InstanceStats instanceStats)
	{
		PlayersInstanceStats = instanceStats;
		healthComponent.SetHealth(PlayersInstanceStats.Health);
		//GD.Print(PlayersInstanceStats.Health);
	}

	public void SetMoveset(Moveset moveset)
	{
		Moveset = moveset;
	}
	

    public void TakeDamage(int damage)
    {
        var calculatedDamage = damageComponent.CalculateDamageTaken(damage, playerStats);
	    healthComponent.CalculateHealth(-calculatedDamage);
    }

    public void ChangeStatus(IStatus status)
    {
		GD.Print("Changing Status...");
        statusHandler.SetStatus(status);
		statusHandler.CheckStatus();
    }
}
