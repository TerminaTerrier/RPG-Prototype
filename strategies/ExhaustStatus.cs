using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class ExhaustStatus : Node, IStatus
{
    public int TurnLength { get; set; }
	public StatusData StatusData { get; set; }
	public HealthComponent HealthComponent { get; set; }
    public SpecialPointComponent SpecialPointComponent { get; set; }
	public Timer Timer { get; set; }
    public Queue<string> StatusNames { get; set; } = new Queue<string>();

    public ExhaustStatus(int turnLength, StatusData statusData, HealthComponent healthComponent, SpecialPointComponent specialPointComponent, Timer timer)
	{
        TurnLength = turnLength;
		StatusData = statusData;
		HealthComponent = healthComponent;
		SpecialPointComponent = specialPointComponent;
		Timer = timer;

		StatusNames.Enqueue("Exhaust");
	}
    public void Effect()
    {
		Timer.Start(1f);

		Timer.Timeout += () =>
        {
            if(SpecialPointComponent.CurrentSP < SpecialPointComponent.MaxSP)
		    {
			    GD.Print("Recovering...");
                SpecialPointComponent.CalculateSP(1);
			    HealthComponent.CalculateHealth(-1);

				Timer.Start(1f);
		    }
			else
			{
				Timer.Stop();
			}
		};
    }

    
	

}
