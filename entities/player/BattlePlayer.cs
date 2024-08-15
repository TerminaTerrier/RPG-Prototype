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
    public IDamageable.DamageAffinity damageAffinity { get; set; }

    EventBus _eventBus;
    
    public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");

		GD.Print("Battle Player ready? " + IsInsideTree());
        
		PlayersInstanceStats = ResourceLoader.Load<InstanceStats>("user://PlayerInstanceData.tres");

		if(PlayersInstanceStats.Health == 0 | PlayersInstanceStats.SP == 0)
		{
			spComponent.SetSP(spComponent.MaxSP);
			healthComponent.SetHealth(healthComponent.MaxHealth);
		}
		else
		{
			spComponent.SetSP(PlayersInstanceStats.SP);
			healthComponent.SetHealth(PlayersInstanceStats.Health);
		}
		
		GD.Print("Battle player health is: " + PlayersInstanceStats.Health);

		
		
		Timer timer = new();
		CallDeferred("add_child", timer);

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
		GD.Print("Is Moveset null? " + Moveset);
		GD.Print(_eventBus is null);
        _eventBus.EmitSignal(EventBus.SignalName.PlayerTurnStarted, Moveset);
		//GD.Print(healthComponent.CurrentHealth);
		GD.Print("Current turn check: " + TurnManager.currentTurn);
        GD.Print("Current SP check: " + spComponent.CurrentSP);
		statusHandler.CheckStatus();	
		

		
	}

	public void SetStats()
	{
		healthComponent.SetMaxHealth(playerStats.maxHP);
		spComponent.SetMaxSP(playerStats.maxSP);

		switch(playerStats.statClassName)
		{
			case "Earth":
			{
				damageAffinity = IDamageable.DamageAffinity.Earth;
				Moveset = ResourceLoader.Load<Moveset>("res://resources/moves/earth/EarthMoveset.tres");
				break;
			}
			case "Wood":
			{
				damageAffinity = IDamageable.DamageAffinity.Wood;
				Moveset = ResourceLoader.Load<Moveset>("res://resources/moves/wood/WoodMoveset.tres");
				break;
			}
			case "Metal":
			{
				damageAffinity = IDamageable.DamageAffinity.Metal;
				Moveset = ResourceLoader.Load<Moveset>("res://resources/moves/metal/MetalMoveset.tres");
				break;
			}
		}
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

    public void SaveInstanceData()
    {
	   InstanceStats newInstanceData = new InstanceStats(healthComponent.CurrentHealth, spComponent.CurrentSP);
	   //GD.Print(ResourceSaver.Save(newInstanceData, "user://InstanceData.tres"));
	   ResourceSaver.Save(newInstanceData, "user://BattlePlayerInstanceData.tres");
	   GD.Print("Battle Player health is: " + newInstanceData.Health);
	}
}
