using Godot;
using System;

public partial class HealthItemWorldIcon : Node2D
{
    EventBus eventBus;

    public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
	}
    
    public void OnBodyEntered(Node2D body)
    {
        eventBus.EmitSignal(EventBus.SignalName.ItemObtained, 1);
		QueueFree();
	}
}
