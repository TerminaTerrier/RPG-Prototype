using Godot;
using System;

public partial class HealthItem : Item
{
    [Export]
    public ItemData itemData;
    EventBus eventBus;

    public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
	}
    
    public override void Use()
    {
        eventBus.EmitSignal(EventBus.SignalName.HealthItemUsed, 3);
    }

    
}
