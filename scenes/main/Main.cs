using Godot;
using System;
using System.IO;

public partial class Main : Node2D
{
	
	[Export]
	SceneLoader sceneLoader;
	[Export]
	BattleManager battleManager;
	EventBus eventBus;

    public override void _EnterTree()
    {
        //Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");

        
		eventBus.TypeSelected += (string type) =>
		{
            battleManager.PlayerMoveset = TypeLoader.LoadMoveset(type);
			battleManager.EnemyMoveset = TypeLoader.LoadOpposingMoveset(type);

			sceneLoader.playerStats = TypeLoader.LoadStats(type);
			sceneLoader.enemyStats =  TypeLoader.LoadOpposingStats(type);
			battleManager._enemyStats = (Stats)TypeLoader.LoadOpposingStats(type);
			battleManager._playerStats = (Stats)TypeLoader.LoadStats(type);
            
			GD.Print(TypeLoader.LoadMoveset(type) is null);

			eventBus.EmitSignal(EventBus.SignalName.GameStarted);
		};

		eventBus.StartBattle += InitializeBattle;
        eventBus.EnemyDeath += UnloadBattle;
		eventBus.PlayerDeath += GameOver;
	}

	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("ui_cancel"))
		{
			var dir = DirAccess.Open("user://");
			dir.Remove("user://PlayerInventory.tres");
			dir.Remove("user://BattlePlayerInstanceData.tres");
			dir.Remove("user://BattlePlayerInstanceData.tres");
            GetTree().Quit();
		}
	}

    public void InitializeBattle(Stats enemyStats, Stats playerStats)
	{
		var player = sceneLoader.GetNode<Player>("Player");
        
		player.SaveInstanceData();
		sceneLoader.UnloadAllScenes();
		battleManager.BattleStart();
	}

	public void UnloadBattle()
	{
        battleManager.EndBattle();
		sceneLoader.LoadInitialScene();
	}

	public void GameOver()
	{
		battleManager.EndBattle();
		sceneLoader.LoadInitialScene();
	}

	
}
