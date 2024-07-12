using Godot;
using System;

public partial class StatusChangeAction : Node, IAction
{
    public Move Move { get; set;}
	public IEffectable Target {get; set;}
    public Stats ActorStats { get; set;}
    
	public StatusChangeAction(Move move, Node2D target, Stats actorStats)
	{
		Move = move;
        Target = (IEffectable)target;
        ActorStats = actorStats;
	}
 
    public void Enact()
    {
       
    }
}
