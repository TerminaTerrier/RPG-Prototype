using Godot;
using System;

public partial class SPItemWorldIcon : Node2D
{
	EventBus eventBus;

    public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
	}
    
    public void OnBodyEntered(Node2D body)
    {
        eventBus.EmitSignal(EventBus.SignalName.ItemObtained, 2);
		QueueFree();
	}
}
