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
	public Moveset Moveset { get; private set; }

	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ActionSelected += DetermineAction;
	}

	public void SetMoveset(Moveset moveset)
	{
        Moveset = moveset;
	}
    
	public void DetermineAction(int moveNum)
	{
		var moveData = Moveset.moveset[moveNum];
		(Node2D target1, Node2D target2)targets = targetRetriever.GetTarget(moveData.target, possibleTargets);
		var targetStats = targetRetriever.GetTargetStats(moveData.target, TargetStats);
		var actor = targetRetriever.GetActor(possibleTargets);
		var actorSP = targetRetriever.GetActorSP(possibleTargets);
		GD.Print("Actor SP is " + actorSP);
		var i = 0;

		
        
		if(actorSP >= moveData.SPCost)
		{
            actor.Deplete(moveData.SPCost);
			  
		    foreach(var moveType in moveData.moveTypes)
		    {
		        switch(moveData.moveTypes[i])
		        {
			        case Move.MoveType.SingleAttack:
			        {
                        ActionContext.SetAction(new AttackAction(moveData, targets, targetStats));
				        ActionContext.EnactAction();  
				        break;
			        }
				    case Move.MoveType.MultiAttack:
				    {
					    ActionContext.SetAction(new AttackAction(moveData, targets, targetStats));
				        ActionContext.EnactAction();  
					    break;
				    }
				    case Move.MoveType.StatusEffect:
				    {
					    ActionContext.SetAction(new StatusChangeAction(moveData, targets, targetStats));
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
	}
	
}
