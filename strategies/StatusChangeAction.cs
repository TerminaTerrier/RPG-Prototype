using Godot;
using System;

public partial class StatusChangeAction : Node, IAction
{
    public Move Move { get; set;}
	public (IEffectable targetOne, IEffectable targetTwo) Targets {get; set;}
    public Stats ActorStats { get; set;}
    
    
	public StatusChangeAction(Move move, (Node2D target1, Node2D target2) targets, Stats actorStats)
	{
		Move = move;
        var t1 = (IEffectable)targets.target1;
        var t2 = (IEffectable)targets.target2;
        Targets = (t1, t2);
        ActorStats = actorStats;
	}
 
    public void Enact()
    {
       switch (Move.status.statusFlag)
       {
            case StatusData.StatusFlag.None:
            {
                break;
            } 
            case StatusData.StatusFlag.Nonspecific:
            {
                if(Move.target == Move.Target.Self)
                {
                   Targets.targetOne?.ChangeStatus(new StatModifier(Move, ActorStats, Targets.targetOne));
                }
                else if(Move.target == Move.Target.Enemy)
                {
                    Targets.targetTwo?.ChangeStatus(new StatModifier(Move, ActorStats, Targets.targetTwo));
                }
                break;
            }
            case StatusData.StatusFlag.Rooted:
            {
                if(Move.target == Move.Target.Self)
                {
                    Targets.targetOne?.ChangeStatus(new StatModifier(Move, ActorStats, Targets.targetOne));
                }
                else if(Move.target == Move.Target.Enemy)
                {
                    Targets.targetTwo?.ChangeStatus(new RootStatus(Move.status, ActorStats, Targets.targetTwo));
                }
                break;
            }
       }
    }
}
