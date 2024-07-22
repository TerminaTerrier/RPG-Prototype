using Godot;
using System;

public partial class BattlePlayer : Node2D, IDamageable, IEffectable, IDepletable
{
	[Export]
	HealthComponent healthComponent;
	[Export]
	public SpecialPointComponent spComponent {get; private set;}
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
		spComponent.SetMaxSP(playerStats.maxSP);
		Timer timer = new();
		AddChild(timer);

		_eventBus.SPDepleted += (string parentEntityName) =>
		{
            if(parentEntityName == "Player")
			{
				GD.Print("Setting Exhaust Status");
				var ExhaustData = GD.Load<StatusData>("res://resources/status/ExhaustedStatusData.tres");
				
                ChangeStatus(new ExhaustStatus(ExhaustData.turnLength, ExhaustData, healthComponent, spComponent, timer));
			}
		};

		GlobalPosition = new Vector2(-300, 165);
	}

	public void StartTurn()
	{
        _eventBus.EmitSignal(EventBus.SignalName.PlayerTurnStarted, Moveset);
		//GD.Print(healthComponent.CurrentHealth);
        GD.Print(spComponent.CurrentSP);
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
		//GD.Print(PlayersInstanceStats.SP);
		spComponent.SetSP(PlayersInstanceStats.SP);
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

    public void Deplete(int cost)
    {
        spComponent.CalculateSP(-cost);
    }
}
