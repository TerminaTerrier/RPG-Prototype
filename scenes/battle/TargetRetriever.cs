using Godot;
using System;
using System.Collections.Generic;
public partial class TargetRetriever : Node
{
	//Might consider making these methods static.
	public (Node2D target1, Node2D target2) GetTarget(Move.Target target, Dictionary<string, Node2D> possibleTargets)
	{
	    switch (TurnManager.currentTurn)
	    {
	        case TurnManager.CurrentTurn.Player:
			{
	            if(target == Move.Target.Self)
	            {
                    return (possibleTargets["Player"], null);
	            }
				
				if(target == Move.Target.Enemy)
	            {
		            return (possibleTargets["Enemy"], null);
	            }

				if(target == Move.Target.SelfAndEnemy)
				{
					Node2D target1 = possibleTargets["Player"];
					Node2D target2 = possibleTargets["Enemy"];
					return (target1, target2);
				}
				break;
	        }
			case TurnManager.CurrentTurn.Enemy:
			{
				if(target == Move.Target.Self)
				{
					return (possibleTargets["Enemy"], null);
				}

				if(target == Move.Target.Enemy)
				{
					return (possibleTargets["Player"], null);
				}

				if(target == Move.Target.SelfAndEnemy)
				{
					Node2D target1 = possibleTargets["Enemy"];
					Node2D target2 = possibleTargets["Player"];
					return (target1, target2);
				}
				break;
			}
	    }

		return (null, null);
    }

	public Stats GetTargetStats(Move.Target target, Dictionary<string, Stats> targetStats)
	{
		  switch (TurnManager.currentTurn)
	    {
	        case TurnManager.CurrentTurn.Player:
			{
	            if(target == Move.Target.Self)
	            {
                    return targetStats["PlayerStats"];
	            }
				else if(target == Move.Target.Enemy)
	            {
		            return targetStats["EnemyStats"];
	            }
				break;
	        }
			case TurnManager.CurrentTurn.Enemy:
			{
				if(target == Move.Target.Self)
				{
					return targetStats["EnemyStats"];
				}
				else if(target == Move.Target.Enemy)
				{
					return targetStats["PlayerStats"];
				}
				break;
			}
	    }
        
		return null;
	}
	
	public Stats GetActorStats(Move.Target target, Dictionary<string, Stats> targetStats)
	{
		switch (TurnManager.currentTurn)
	    {
	        case TurnManager.CurrentTurn.Player:
			{
                return targetStats["PlayerStats"];
	        }
			case TurnManager.CurrentTurn.Enemy:
			{
			return targetStats["EnemyStats"];
			}	
	    }
        
		return null;
	}

	public IDepletable GetActor(Dictionary<string, Node2D> possibleTargets)
	{
        switch(TurnManager.currentTurn)
		{
			case TurnManager.CurrentTurn.Player:
			{
				var actor = (BattlePlayer)possibleTargets["Player"];
				return actor;
			}
			case TurnManager.CurrentTurn.Enemy:
			{
				var actor = (BattleEnemy)possibleTargets["Enemy"];
				return actor;
			}
		}
		
        return null;
	}

	public int GetActorSP(Dictionary<string, Node2D> possibleTargets)
	{
		switch(TurnManager.currentTurn)
		{
			case TurnManager.CurrentTurn.Player:
			{
				var actor = (BattlePlayer)possibleTargets["Player"];
				return actor.spComponent.CurrentSP;
			}
			case TurnManager.CurrentTurn.Enemy:
			{
				var actor = (BattleEnemy)possibleTargets["Enemy"];
				return actor.spComponent.CurrentSP;
			}
		}

		return 0;
	}
}
