using Godot;
using System;
using System.ComponentModel;

public partial class StatusHandler : Node
{
	public IStatus Status { get; set; }
	int turnLength;

    public void SetStatus(IStatus status)
	{
        Status = status;
		turnLength = Status.TurnLength; 
	}

    public void CheckStatus()
	{
		if(Status != null)
		{
			GD.Print("Checking status...");
			RunStatus();
		}
	}
	public void RunStatus()
	{
        if(turnLength > 0)
		{
			GD.Print("Running Status...");
			Status.Effect();
			turnLength--;
		}
	}
}
