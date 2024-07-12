using Godot;
using System;

public partial class Main : Node2D
{
	
	[Export]
	SceneLoader sceneLoader;
	[Export]
	BattleManager battleManager;
	EventBus eventBus;
	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.StartBattle += InitializeBattle;
	}

    public void InitializeBattle()
	{
		var player = sceneLoader.GetNode<Player>("Player");
		
		battleManager.SetInstanceValues(player.GetInstanceData());
		battleManager.BattleStart();
		sceneLoader.UnloadAllScenes();
		
		
	}

	
}
