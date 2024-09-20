using Godot;
using System;
using System.IO;

public partial class Main : Node2D
{
	
	[Export]
	SceneLoader sceneLoader;
	[Export]
	BattleManager battleManager;
	[Export]
	UILoader uiLoader;
	EventBus eventBus;
	Stats woodStatsBase;
	Stats metalStatsBase;
	Stats earthStatsBase;

    public override void _EnterTree()
    {
        //Input.MouseMode = Input.MouseModeEnum.Captured;
    }
    public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
        
		woodStatsBase = (Stats)TypeLoader.LoadStats("Wood");
		earthStatsBase = (Stats)TypeLoader.LoadStats("Earth");
		metalStatsBase = (Stats)TypeLoader.LoadStats("Metal");
        
		eventBus.TypeSelected += (string type) =>
		{
            battleManager.PlayerMoveset = TypeLoader.LoadMoveset(type);
			battleManager.EnemyMoveset = TypeLoader.LoadOpposingMoveset(type);

			sceneLoader.playerStats = TypeLoader.LoadStats(type);
			sceneLoader.enemyStats =  TypeLoader.LoadOpposingStats(type);
			battleManager._enemyStats = (Stats)TypeLoader.LoadOpposingStats(type);
			battleManager._playerStats = (Stats)TypeLoader.LoadStats(type);
			uiLoader.playerStats = (Stats)TypeLoader.LoadStats(type);
			uiLoader.enemyStats = (Stats)TypeLoader.LoadOpposingStats(type);
            
			GD.Print("Is moveset null?");
			GD.Print(TypeLoader.LoadMoveset(type) is null);

			GD.Print("Are stats null?");
			GD.Print(TypeLoader.LoadStats(type) is null);

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
			dir.Remove("user://PlayerInstanceData.tres");
			ResourceSaver.Save(woodStatsBase, "res://resources/stats/WoodStats.tres");
			ResourceSaver.Save(earthStatsBase, "res://resources/stats/EarthStats.tres");
			ResourceSaver.Save(metalStatsBase, "res://resources/stats/MetalStats.tres");
            GetTree().Quit();
		}
	}

    public void InitializeBattle()
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
