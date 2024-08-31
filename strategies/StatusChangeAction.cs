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
        GD.Print("ENACTING STATUS CHANGE");
       switch (Move.status.statusFlag)
       {
            case StatusData.StatusFlag.None:
            {
                break;
            } 
            case StatusData.StatusFlag.Modifier:
            {
                
                if(Targets.targetOne is not null)
                {
                    GD.Print("Applying Modifier to Target One");
                    Targets.targetOne.ChangeStatus(new StatModifier(Move, ActorStats, Targets.targetOne));
                }
                else
                {
                    GD.Print("Applying Modifier to Target Two");
                    Targets.targetTwo.ChangeStatus(new StatModifier(Move, ActorStats, Targets.targetTwo));
                }
    
                break;
            }
            case StatusData.StatusFlag.Rooted:
            {
                GD.Print("Apply Rooted...");

                if(Targets.targetOne is not null)
                {
                    Targets.targetOne.ChangeStatus(new RootStatus(Move.status, ActorStats, Targets.targetOne));
                }
                else
                {
                    Targets.targetTwo.ChangeStatus(new RootStatus(Move.status, ActorStats, Targets.targetTwo));
                }

                break;
            }
       }
    }
}
