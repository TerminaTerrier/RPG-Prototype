using Godot;
using System;

public partial class RootStatus : Node, IStatus
{
	public StatusData Status { get; set; }
	public Stats TargetStats{ get; set; }
	public int TurnLength { get; set; }

    public RootStatus(StatusData statusData, Stats stats)
	{
		Status = statusData;
        TargetStats = stats;
		TurnLength = Status.turnLength;
	}

    public void Effect(Node target)
    {
        if(target is BattleEnemy)
		{
			var battleEnemy = (BattleEnemy)target;
			GD.Print("Effecting...");
		}
		else if(target is BattlePlayer)
		{
			GD.Print("Effecting...");
			var battlePlayer = (BattlePlayer)target;
			battlePlayer.ActionLocked = true;
			battlePlayer.EndTurn();
		}
    }

	
}
