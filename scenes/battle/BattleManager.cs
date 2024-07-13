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
	Stats _playerStats;
	Stats _enemyStats;
	InstanceStats playerInstanceData;
	BattlePlayer battlePlayer;
	BattleEnemy battleEnemy;
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
		AddChild(battlePlayer);
		AddChild(battleEnemy);

		_playerStats = battlePlayer.playerStats;
		_enemyStats = battleEnemy.enemyStats;
		
		battlePlayer.SetPlayerInstanceValues(playerInstanceData);
        
		//Temporary method call that will be moved into a more fully fleged class.
		battlePlayer.SetMoveset(GD.Load<Moveset>("res://resources/moves/WoodMoveset.tres"));
		_actionClient.SetMoveset(GD.Load<Moveset>("res://resources/moves/WoodMoveset.tres"));

		participants.Add("Player", battlePlayer);
	    participants.Add("Enemy", battleEnemy);
		participantsStats.Add("PlayerStats", _playerStats);
		participantsStats.Add("EnemyStats", _enemyStats);
		_actionClient.possibleTargets = participants;
		_actionClient.TargetStats = participantsStats;


        
		_battleCamera.Enabled = true;
		battleStatus = BattleStatus.Active;
		turnManager.ManageTurn(battlePlayer, battleEnemy, _playerStats, _enemyStats);
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

	
	public void BattleStop()
	{
        
	}
}
