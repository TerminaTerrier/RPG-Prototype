using Godot;
using System;
using System.Collections.Generic;
public partial class TargetRetriever : Node
{
	public Node2D GetTarget(Move.Target target, Dictionary<string, Node2D> possibleTargets)
	{
	   if(target == Move.Target.Self)
	   {
           return possibleTargets["Actor"];
	   }
	   else if(target == Move.Target.Enemy)
	   {
		   return possibleTargets["Opponent"];
	   }

	   return null;
	}
}
