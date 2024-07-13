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
		var target = targetRetriever.GetTarget(moveData.target, possibleTargets);
		var targetStats = targetRetriever.GetTargetStats(moveData.target, TargetStats);
		var i = 0;

		foreach(var moveType in moveData.moveTypes)
		{
		    switch(moveData.moveTypes[i])
		    {
			    case Move.MoveType.SingleAttack:
			    {
                    ActionContext.SetAction(new AttackAction(moveData, target, targetStats));
				    ActionContext.EnactAction();  
				    break;
			    }
				case Move.MoveType.MultiAttack:
				{
					ActionContext.SetAction(new AttackAction(moveData, target, targetStats));
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
