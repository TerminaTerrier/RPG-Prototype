using Godot;
using System;
using System.ComponentModel;

public partial class StatusHandler : Node
{
	public IStatus Status { get; set; }
	int turnLength;
	EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
    }

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
		else
		{
			GD.Print("STATUS IS NULL");
		}
	}
	public void RunStatus()
	{
        if(turnLength > 0)
		{
			GD.Print("Running Status...");
			Status.Effect();
			LogStatus();
			turnLength--;
		}
		else
		{
			Status = null;
			turnLength = 0;
		}
	}

	private void LogStatus()
	{
		GD.Print("STATUS NAME IS: " + Status.StatusNames.Peek());
		string effectedEntity = (TurnManager.currentTurn == TurnManager.CurrentTurn.Player) ? "Enemy" : "Player";

		
		for(int i = Status.StatusNames.Count; i > 0; i--)
		{
            eventBus.EmitSignal(EventBus.SignalName.BattleUpdate, effectedEntity + " is " + Status.StatusNames.Dequeue() + "ed!");
		}

		Status.StatusNames.Clear();
		
	}
}
