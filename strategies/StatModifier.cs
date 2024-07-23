using Godot;
using System;

public partial class StatModifier : Node, IStatus
{
	public Stats TargetStats{ get; set; }
	public IEffectable Target { get; set; }	
	public Move MoveData  {get; set; }
    public int TurnLength { get; set; }
    
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
			if(MoveData.status.statsToModify[i] == stat)
			{
				var statToModify = MoveData.status.statsToModify[i];

				switch(statToModify)
				{
					case StatusData.StatsToModify.Attack:
					{
                        TargetStats.attack = Mathf.RoundToInt(TargetStats.attack * MoveData.status.statModifiers["Attack"]);
						break;
					}
					case StatusData.StatsToModify.Defense:
					{
                        TargetStats.defense = Mathf.RoundToInt(TargetStats.defense * MoveData.status.statModifiers["Defense"]);
						GD.Print("Defense up! " + TargetStats.defense);
						break;
					}
					case StatusData.StatsToModify.Speed:
					{
						TargetStats.speed = Mathf.RoundToInt(TargetStats.speed * MoveData.status.statModifiers["Speed"]);
						break;
					}
					case StatusData.StatsToModify.Luck:
					{
						TargetStats.luck = Mathf.RoundToInt(TargetStats.luck * MoveData.status.statModifiers["Luck"]);
						break;
					}
				}

				i++;
			}
		
		}
		
    }
}
