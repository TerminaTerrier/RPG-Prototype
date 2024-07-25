using Godot;
using System;

public partial class BattleHUD : Control
{
	[Export]
	public VitalElementManager PlayerVitalElement;
	[Export]
	public VitalElementManager EnemyVitalElement;
    EventBus _eventBus;

	public override void _Ready()
	{
        _eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.PlayerHealthUpdated += OnPlayerHealthUpdate;
		_eventBus.EnemyHealthUpdated += OnEnemyHealthUpdate;
		_eventBus.PlayerSPUpdated += OnPlayerSPUpdate;
		_eventBus.EnemySPUpdated += OnEnemySPUpdate;
	}
    
	public void OnPlayerHealthUpdate(int health)
	{
		PlayerVitalElement.OnHealthUpdate(health);
	}

	public void OnEnemyHealthUpdate(int health)
	{
		EnemyVitalElement.OnHealthUpdate(health);
	}

	public void OnPlayerSPUpdate(int sp)
	{
        PlayerVitalElement.OnSPUpdate(sp);
	}

	public void OnEnemySPUpdate(int sp)
	{
		EnemyVitalElement.OnSPUpdate(sp);
	}
	
}
