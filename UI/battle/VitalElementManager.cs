using Godot;
using System;

public partial class VitalElementManager : Control
{
	[Export]
	ProgressBar healthBar;
	[Export]
	CenterContainer hpCounter;
	[Export]
	ProgressBar spBar;
	[Export]
	CenterContainer spCounter;
	EventBus eventBus;

	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
	}
	
	public void SetProgressBarMax(int hpMax, int spMax)
	{
        healthBar.MaxValue = hpMax;
		spBar.MaxValue = spMax;
	}

	public void OnHealthUpdate(int healthUpdate)
	{
		GD.Print("Calling OnHealthUpdate...");
        
        healthBar.Value = healthUpdate;
		var label = hpCounter.GetChild<Label>(0);
		label.Text = healthUpdate.ToString();
	}

	public void OnSPUpdate(int spUpdate)	
	{
        spBar.Value = spUpdate;
		var label = spCounter.GetChild<Label>(0);
		label.Text = spUpdate.ToString();
	}
}
