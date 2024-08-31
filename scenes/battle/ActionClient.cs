using Godot;
using System;
using System.Collections.Generic;
public partial class ActionClient : Node
{
	[Export]
	public ActionContext ActionContext;
	[Export]
	TargetRetriever targetRetriever;
	EventBus eventBus;
	public InstanceStats InstanceStats {get; private set;}
	public Dictionary<string, Node2D> possibleTargets;
	public Dictionary<string, Stats> TargetStats;
	public Moveset PlayerMoveset { get; private set; }
	public Moveset EnemyMoveset { get; private set; }

	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ActionSelected += DetermineAction;
	}

	public void SetMovesets(Moveset moveset1, Moveset moveset2)
	{
		PlayerMoveset = moveset1;
		EnemyMoveset = moveset2;
	}
    
	public void DetermineAction(int moveNum)
	{
		Move _moveData = null;

		GD.Print("Current turn at time of determing action is " + TurnManager.currentTurn);

		if(TurnManager.currentTurn == TurnManager.CurrentTurn.Player)
		{
		    _moveData = PlayerMoveset.moveset[moveNum];
			eventBus.EmitSignal(EventBus.SignalName.BattleUpdate, "Player uses " + _moveData.MoveText + "!");
		}
		else if(TurnManager.currentTurn == TurnManager.CurrentTurn.Enemy)
		{
            _moveData = EnemyMoveset.moveset[moveNum];
			eventBus.EmitSignal(EventBus.SignalName.BattleUpdate, "Enemy uses " + _moveData.MoveText + "!");
		}

		(Node2D target1, Node2D target2)targets = targetRetriever.GetTarget(_moveData.target, possibleTargets);
		var targetStats = targetRetriever.GetTargetStats(_moveData.target, TargetStats);
		var actorStats = targetRetriever.GetActorStats(_moveData.target, TargetStats);
		var actor = targetRetriever.GetActor(possibleTargets);
		var actorSP = targetRetriever.GetActorSP(possibleTargets);
		GD.Print("Actor SP is " + actorSP);
		var i = 0;

		GD.Print("Move selected: " + _moveData.MoveText);
        
		if(actorSP >= _moveData.SPCost)
		{
            actor.Deplete(_moveData.SPCost);
			  
		    foreach(var moveType in _moveData.moveTypes)
		    {
		        switch(_moveData.moveTypes[i])
		        {
			        case Move.MoveType.Attack:
			        {
                        ActionContext.SetAction(new AttackAction(_moveData, targets, actorStats));
				        ActionContext.EnactAction();  
				        break;
			        }
				    case Move.MoveType.StatusEffect:
				    {
					    ActionContext.SetAction(new StatusChangeAction(_moveData, targets, targetStats));
					    ActionContext.EnactAction();
					    break;
				    }
		        }

		        i++;
		    }
		}
		else
		{
			GD.Print("Not enough SP!");
		}
        
		if(TurnManager.currentTurn == TurnManager.CurrentTurn.Player)
		{
		    eventBus.EmitSignal(EventBus.SignalName.PlayerTurnEnded);
		    eventBus.EmitSignal(EventBus.SignalName.TurnEnded);
		}
		else if(TurnManager.currentTurn == TurnManager.CurrentTurn.Enemy)
		{
			eventBus.EmitSignal(EventBus.SignalName.TurnEnded);
		}
	}
	
}
