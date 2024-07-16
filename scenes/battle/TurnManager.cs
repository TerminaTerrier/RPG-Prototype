using Godot;
using System;

public partial class TurnManager : Node
{   
	[Export]
	TargetRetriever targetRetriever;
	BattlePlayer _battlePlayer;
	BattleEnemy _battleEnemy;
	Stats _playerStats;
	Stats _enemyStats;
    EventBus _eventBus;
	TookLastTurn tookLastTurn;
	public static CurrentTurn currentTurn {get; private set; }
	enum TookLastTurn
	{
       None,
	   Player,
	   Enemy
	}
	public enum CurrentTurn
	{
		Undefined,
		Player,
		Enemy
	}

	public override void _Ready()
	{
        _eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.TurnEnded += ManageTurn;
	}

	public void SetBattleProperties(BattlePlayer battlePlayer, BattleEnemy battleEnemy, Stats playerStats, Stats enemyStats)
	{
        _battlePlayer = battlePlayer;
		_battleEnemy = battleEnemy;
		_playerStats = playerStats;
		_enemyStats = enemyStats;
	}

	public void ManageTurn()
	{
	    if(BattleManager.battleStatus == BattleManager.BattleStatus.Active)
		{
            if(_playerStats.speed > _enemyStats.speed)
		    {
                if(tookLastTurn != TookLastTurn.Player)
				{
                    _battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Player;
					currentTurn = CurrentTurn.Player;
				}
				else if(tookLastTurn != TookLastTurn.Enemy)
				{
					_battleEnemy.StartTurn();
					tookLastTurn = TookLastTurn.Enemy;
					currentTurn = CurrentTurn.Enemy;
				}
		    }
		    else if(_enemyStats.speed > _playerStats.speed)
		    {
               if(tookLastTurn != TookLastTurn.Enemy)
				{
                    _battleEnemy.StartTurn();
					tookLastTurn = TookLastTurn.Enemy;
					currentTurn = CurrentTurn.Enemy;
				}
				else if(tookLastTurn != TookLastTurn.Player)
				{
					_battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Player;
					currentTurn = CurrentTurn.Player;
				}
		    }
		}
		else
		{
			
		}
	}

    

	
}
