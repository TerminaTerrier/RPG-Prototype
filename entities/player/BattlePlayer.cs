using Godot;
using System;

public partial class BattlePlayer : Node2D, IDamageable
{
	[Export]
	HealthComponent healthComponent;
	[Export]
	DamageComponent damageComponent;
	[Export]
	public Stats playerStats;
	public Moveset Moveset { get; private set; }
	public InstanceStats PlayersInstanceStats {get; private set;}
	EventBus _eventBus;
    
    public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");

		

		GlobalPosition = new Vector2(-300, 165);
	}

	public void StartTurn()
	{
        _eventBus.EmitSignal(EventBus.SignalName.PlayerTurnStarted, Moveset);
		GD.Print(healthComponent.CurrentHealth);
		healthComponent.SetMaxHealth(playerStats.maxHP);
		
	}

	public void EndTurn()
	{

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
        throw new NotImplementedException();
    }
}
