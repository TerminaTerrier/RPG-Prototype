using Godot;
using System;
using System.Collections.Generic;

public partial class StatModifier : Node, IStatus
{
	public Stats TargetStats{ get; set; }
	public IEffectable Target { get; set; }	
	public Move MoveData  {get; set; }
    public int TurnLength { get; set; }
    public Queue<string> StatusNames { get; set; } = new Queue<string>();

    public StatModifier(Move moveData, Stats stats, IEffectable target)
	{
        MoveData = moveData;
		TargetStats = stats;
		Target = target;
		TurnLength = MoveData.status.turnLength;
	}
   
    public void Effect()
    {
		var i = 0;

        foreach(var stat in MoveData.status.statsToModify)
		{
				var statToModify = MoveData.status.statsToModify[i];
                GD.Print("SELECTING STAT MODIFIER");
				switch(statToModify)
				{
					case StatusData.StatsToModify.Attack:
					{
                        TargetStats.attack = Mathf.RoundToInt(TargetStats.attack * MoveData.status.statModifiers["Attack"]);
						string newStatusName = (MoveData.status.statModifiers["Attack"] >= 1) ? "Strength" : "Weaken";
						StatusNames.Enqueue(newStatusName);
						break;
					}
					case StatusData.StatsToModify.Defense:
					{
                        TargetStats.defense = Mathf.RoundToInt(TargetStats.defense * MoveData.status.statModifiers["Defense"]);
						string newStatusName = (MoveData.status.statModifiers["Defense"] >= 1) ? "Fortifi" : "Embrittl";
						StatusNames.Enqueue(newStatusName);
						GD.Print("Defense up! " + TargetStats.defense);
						break;
					}
					case StatusData.StatsToModify.Speed:
					{
						TargetStats.speed = Mathf.RoundToInt(TargetStats.speed * MoveData.status.statModifiers["Speed"]);
						string newStatusName = (MoveData.status.statModifiers["Speed"] >= 1) ? "Quicken" : "Slow";
						StatusNames.Enqueue(newStatusName);
						break;
					}
					case StatusData.StatsToModify.Luck:
					{
						TargetStats.luck = Mathf.RoundToInt(TargetStats.luck * MoveData.status.statModifiers["Luck"]);
						string newStatusName = (MoveData.status.statModifiers["Lucky"] >= 1) ? "Charm" : "Curse";
						StatusNames.Enqueue(newStatusName);
						break;
					}
				}

				i++;
		}
		
		switch(TargetStats.statClassName)
		{
			case "Wood":
			    ResourceSaver.Save(TargetStats, "res://resources/stats/WoodStats.tres");
			break;

			case "Earth":
			    ResourceSaver.Save(TargetStats, "res://resources/stats/EarthStats.tres");
			break;

			case "Metal":
			    ResourceSaver.Save(TargetStats, "res://resources/stats/MetalStats.tres");
			break;
		}
    }
}
