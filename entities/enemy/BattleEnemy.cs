using Godot;
using System;

public partial class BattleEnemy : Node2D, IDamageable, IEffectable, IDepletable
{
	[Export]
	public Stats enemyStats;
	[Export]
	EnemyAI enemyAI;
	[Export]
	DamageComponent damageComponent;
	[Export]
	HealthComponent healthComponent;
	[Export]
	StatusHandler statusHandler;
	[Export]
	public SpecialPointComponent spComponent {get; private set;}
	public Moveset enemyMoveset;
    public IDamageable.DamageAffinity damageAffinity { get; set; }
	EventBus eventBus;

    public override void _Ready()
	{
        GlobalPosition = new Vector2(300, -170);
		eventBus = GetNode<EventBus>("/root/EventBus");
		
	}

	public void SetStats()
	{
		healthComponent.SetMaxHealth(enemyStats.maxHP);
		healthComponent.SetHealth(enemyStats.maxHP);
		spComponent.SetMaxSP(enemyStats.maxSP);
		spComponent.SetSP(enemyStats.maxSP);

		switch(enemyStats.statClassName)
		{
			case "Earth":
			{
				damageAffinity = IDamageable.DamageAffinity.Earth;
				enemyMoveset = ResourceLoader.Load<Moveset>("res://resources/moves/earth/EarthMoveset.tres");
				break;
			}
			case "Wood":
			{
				damageAffinity = IDamageable.DamageAffinity.Wood;
				enemyMoveset = ResourceLoader.Load<Moveset>("res://resources/moves/wood/WoodMoveset.tres");
				break;
			}
			case "Metal":
			{
				damageAffinity = IDamageable.DamageAffinity.Metal;
				enemyMoveset = ResourceLoader.Load<Moveset>("res://resources/moves/metal/MetalMoveset.tres");
				break;
			}
		}
	}

    public void StartTurn()
	{
       GD.Print("It's my turn!");
	   var moveNum = enemyAI.PickMove(enemyMoveset, healthComponent.CurrentHealth);

	   	GD.Print("Is Enemy Moveset null? ");
		GD.Print(enemyMoveset is null);
       
	   Timer timer = new Timer();
	   AddChild(timer);

	   timer.OneShot = true;

	   timer.Start(3f);
	   
       timer.Timeout += () => eventBus.EmitSignal(EventBus.SignalName.ActionSelected, moveNum);
       //EndTurn();
	}

	public void EndTurn()
	{
        //eventBus.EmitSignal(EventBus.SignalName.TurnEnded);
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
		GD.Print("Changing enemy status");
        statusHandler.SetStatus(status);
		statusHandler.CheckStatus();
	}

    public void Deplete(int cost)
    {
        spComponent.CalculateSP(-cost);
    }
}
