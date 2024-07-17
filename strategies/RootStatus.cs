using Godot;
using System;

public partial class RootStatus : Node, IStatus
{
	public StatusData Status { get; set; }
	public Stats TargetStats{ get; set; }
	public IEffectable Target { get; set; }	
	public int TurnLength { get; set; }

    public RootStatus(StatusData statusData, Stats stats, IEffectable target)
	{
		Status = statusData;
        TargetStats = stats;
		Target = target;
		TurnLength = Status.turnLength;
	}

    public void Effect()
    {
        if(Target is BattleEnemy)
		{
			var battleEnemy = (BattleEnemy)Target;
			GD.Print("Effecting...");
		}
		else if(Target is BattlePlayer)
		{
			GD.Print("Effecting...");
			var battlePlayer = (BattlePlayer)Target;
			battlePlayer.ActionLocked = true;
			battlePlayer.EndTurn();
		}
    }

	
}
