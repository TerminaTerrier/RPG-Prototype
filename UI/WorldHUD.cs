using Godot;
using System;

public partial class WorldHUD : Control
{
	[Export]
	public VitalElementManager PlayerVitalElement;
    EventBus _eventBus;

	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.PlayerDeath += CheckInstanceData;
		_eventBus.EnemyDeath += CheckInstanceData;
	}

	public void SetMaxValues(Stats playerStats)
	{
		GD.Print("PLAYER MAX HP IS: " + playerStats.maxHP);
		PlayerVitalElement.OnHealthUpdate(playerStats.maxHP);
		PlayerVitalElement.OnSPUpdate(playerStats.maxSP);
		PlayerVitalElement.SetProgressBarMax(playerStats.maxHP, playerStats.maxSP);
	}

	private void CheckInstanceData()
	{
		var dir = DirAccess.Open("user://");
		
		if(dir.FileExists("user://BattlePlayerInstanceData.tres"))
		{
            var instanceData = ResourceLoader.Load<InstanceStats>("user://BattlePlayerInstanceData.tres");
			GD.Print("WORLD HUD HEALTH IS " + instanceData.Health);
		    PlayerVitalElement.OnHealthUpdate(instanceData.Health);
		    PlayerVitalElement.OnSPUpdate(instanceData.SP);
		}
	}
}
