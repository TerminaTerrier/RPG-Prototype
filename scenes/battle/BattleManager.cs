using Godot;
using System;
using System.Collections.Generic;
public partial class BattleManager : Node
{
	[Export]
	SceneData _sceneData;
	[Export]
	Camera2D _battleCamera;
	[Export]
	TurnManager turnManager;
	[Export]
	ActionClient _actionClient;
	EventBus _eventBus;
	public Stats _playerStats;
	public Stats _enemyStats;
	InstanceStats playerInstanceData;
	BattlePlayer battlePlayer;
	BattleEnemy battleEnemy;
	public Moveset PlayerMoveset {get; set;}
	public Moveset EnemyMoveset {get; set;}
	Dictionary<string, Node2D> participants = new Dictionary<string, Node2D>();
	Dictionary<string, Stats> participantsStats = new Dictionary<string, Stats>();
	public static BattleStatus battleStatus {get; private set;}
	public enum BattleStatus
	{
		Active,
		Finished
	}

	
	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
		
	}

	public void BattleStart()
	{
        battlePlayer = (BattlePlayer)_sceneData.BattlePlayer.Instantiate();
		battleEnemy = (BattleEnemy)_sceneData.BattleEnemy.Instantiate();
		battlePlayer.playerStats = _playerStats;
		battleEnemy.enemyStats = _enemyStats;
		battlePlayer.SetStats();
		battleEnemy.SetStats();
		_actionClient.SetMovesets(PlayerMoveset, EnemyMoveset);
		CallDeferred("add_child", battlePlayer);
		CallDeferred("add_child", battleEnemy);
        


		participants.Add("Player", battlePlayer);
	    participants.Add("Enemy", battleEnemy);
		participantsStats.Add("PlayerStats", _playerStats);
		participantsStats.Add("EnemyStats", _enemyStats);
		_actionClient.possibleTargets = participants;
		_actionClient.TargetStats = participantsStats;
        
		_battleCamera.SetDeferred("enabled", true);
		battleStatus = BattleStatus.Active;
		turnManager.SetBattleProperties(battlePlayer, battleEnemy, _playerStats, _enemyStats);
		turnManager.CallDeferred("ManageTurn");
	}

	public void SetInstanceValues(InstanceStats PlayerInstanceData)
	{
        playerInstanceData = PlayerInstanceData;
	}

	public void ChangeBattleStatus()
	{
		if(battleStatus == BattleStatus.Active)
		{
			battleStatus = BattleStatus.Finished;
		}
		else if(battleStatus == BattleStatus.Finished)
		{
			battleStatus = BattleStatus.Active;
		}
	}

	
	public void EndBattle()
	{
        battleStatus = BattleStatus.Finished;
		battlePlayer.QueueFree();
		battleEnemy.QueueFree();
		_battleCamera.Enabled = false;
		participants.Clear();
		participantsStats.Clear();
	}
}
