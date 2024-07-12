using Godot;
using System;
using System.Collections.Generic;
public partial class TargetRetriever : Node
{
	public IDamageable GetTarget(Move.Target target, Dictionary<string, IDamageable> possibleTargets)
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
