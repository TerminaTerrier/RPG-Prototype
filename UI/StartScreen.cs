using Godot;
using System;
using System.Linq;
public partial class StartScreen : Control
{
	[Export]
	HBoxContainer typeSelect;
	public Button[] typeSelectButtons { get; set; }
	EventBus eventBus;
	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		typeSelectButtons = typeSelect.GetChildren().Cast<Button>().ToArray();
		typeSelectButtons[0].GrabFocus();
	}

	public void OnEarthButtonPressed()
	{
       eventBus.EmitSignal(EventBus.SignalName.TypeSelected, "Earth");
	}

	public void OnWoodButtonPressed()
	{
       eventBus.EmitSignal(EventBus.SignalName.TypeSelected, "Wood");
	}

	public void OnMetalButtonPressed()
	{
        eventBus.EmitSignal(EventBus.SignalName.TypeSelected, "Metal");
	}

	
}
