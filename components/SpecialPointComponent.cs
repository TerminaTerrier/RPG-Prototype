using Godot;
using System;

public partial class SpecialPointComponent : Node
{
	[Export]
	public string parentEntityName;
	public int MaxSP { get; private set; }
	public int CurrentSP {get; private set;}
	EventBus eventBus;

	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.SPItemUsed += CalculateSP;
	}

	public void CalculateSP(int spUpdate)
	{
		if(CurrentSP <= MaxSP)
		{
            CurrentSP = CurrentSP + spUpdate;
			GD.Print("SP equals " + CurrentSP);

			if(parentEntityName == "Player")
			{
			    eventBus.EmitSignal(EventBus.SignalName.PlayerSPUpdated, CurrentSP);
			}
			else if(parentEntityName == "Enemy")
			{
				eventBus.EmitSignal(EventBus.SignalName.EnemySPUpdated, CurrentSP);
			}

			if(CurrentSP <= 0)
			{
				GD.Print("SP Depleted!");
				eventBus.EmitSignal(EventBus.SignalName.SPDepleted, parentEntityName);
			}
		}

		if(CurrentSP == MaxSP)
		{
            eventBus.EmitSignal(EventBus.SignalName.SPReplenished);
		}
	}

	public void SetSP(int spUpdate)
	{
		CurrentSP = spUpdate;
	}

	public void SetMaxSP(int spUpdate)
	{
		MaxSP = spUpdate;
	}
}
