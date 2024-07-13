using Godot;
using System;

public partial class TurnManager : Node
{   
	[Export]
	TargetRetriever targetRetriever;
    EventBus eventBus;
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
        
	}

	public void ManageTurn(BattlePlayer battlePlayer, BattleEnemy battleEnemy, Stats playerStats, Stats enemyStats)
	{
	    if(BattleManager.battleStatus == BattleManager.BattleStatus.Active)
		{
            if(playerStats.speed > enemyStats.speed)
		    {
                if(tookLastTurn != TookLastTurn.Player)
				{
                    battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Player;
					currentTurn = CurrentTurn.Player;
				}
		    }
		    else if(enemyStats.speed > playerStats.speed)
		    {
               if(tookLastTurn != TookLastTurn.Enemy)
				{
                    battlePlayer.StartTurn();
					tookLastTurn = TookLastTurn.Enemy;
					currentTurn = CurrentTurn.Enemy;
				}
		    }
		}
		else
		{
			
		}
	}

    

	
}
