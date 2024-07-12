using Godot;
using System;
using System.Collections.Generic;
public partial class ActionClient : Node
{
	[Export]
	public ActionContext ActionContext;
	[Export]
	public TargetRetriever targetRetriever;
	EventBus eventBus;
	Stats _actorStats;
	public InstanceStats InstanceStats {get; private set;}
	public Dictionary<string, Node2D> possibleTargets;
	public Moveset Moveset { get; private set; }

	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ActionSelected += DetermineAction;
	}

	public void SetStats(Stats actorStats)
	{
	    _actorStats = actorStats;
	}

	public void SetMoveset(Moveset moveset)
	{
        Moveset = moveset;
	}
    
	public void DetermineAction(int moveNum)
	{
		var moveData = Moveset.moveset[moveNum];
		var target = targetRetriever.GetTarget(moveData.target, possibleTargets);
		var i = 0;

		foreach(var moveType in moveData.moveTypes)
		{
		    switch(moveData.moveTypes[i])
		    {
			    case Move.MoveType.SingleAttack:
			    {
                    ActionContext.SetAction(new AttackAction(moveData, target, _actorStats));
				    ActionContext.EnactAction();  
				    break;
			    }
				case Move.MoveType.MultiAttack:
				{
					ActionContext.SetAction(new AttackAction(moveData, target, _actorStats));
				    ActionContext.EnactAction();  
					break;
				}
				case Move.MoveType.Buff:
				{
					break;
				}
		    }

			i++;
		}
	}
	
}
