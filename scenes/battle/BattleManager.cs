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
	ActionClient _actionClient;
	EventBus _eventBus;
	Stats _playerStats;
	Stats _enemyStats;
	InstanceStats playerInstanceData;
	BattlePlayer battlePlayer;
	BattleEnemy battleEnemy;
	Dictionary<string, IDamageable> participants = new Dictionary<string,IDamageable>();
	BattleStatus battleStatus;
	TookLastTurn tookLastTurn;
    
	enum BattleStatus
	{
		Active,
		Finished
	}

	enum TookLastTurn
	{
       None,
	   Player,
	   Enemy
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

		
        
		_battleCamera.Enabled = true;
		battleStatus = BattleStatus.Active;
		SetTargets();
		ManageTurn();
	}

	public void SetInstanceValues(InstanceStats PlayerInstanceData)
	{
        playerInstanceData = PlayerInstanceData;
	}
    
	//Method for setting available targets and passing them down to ActionClient from which TargetRetriever will select a target. Likely violates SOLID but I'm not sure where else to put it.
	public void SetTargets()
	{
	   if(tookLastTurn == TookLastTurn.Player | tookLastTurn == TookLastTurn.None)
	   {
		    participants.Clear();
            participants.Add("Actor", (IDamageable)battlePlayer);
	        participants.Add("Opponent", (IDamageable)battleEnemy);
			_actionClient.SetStats(_playerStats);
	   }
	   else if(tookLastTurn == TookLastTurn.Enemy)
	   {
		    participants.Clear();
            participants.Add("Actor", (IDamageable)battleEnemy);
	        participants.Add("Opponent", (IDamageable)battlePlayer);
			_actionClient.SetStats(_enemyStats);
	   }

	   _actionClient.possibleTargets = participants;
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

	public void ManageTurn()
	{
	    if(battleStatus == BattleStatus.Active)
		{
            if(_playerStats.speed > _enemyStats.speed)
		    {
                if(tookLastTurn != TookLastTurn.Player)
				{
                    battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Player;
				}
		    }
		    else if(_enemyStats.speed > _playerStats.speed)
		    {
               if(tookLastTurn != TookLastTurn.Enemy)
				{
                    battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Enemy;
				}
		    }
		}
		else
		{
			BattleStop();
		}
	}

	public void BattleStop()
	{
        tookLastTurn = TookLastTurn.None;
	}
}
