using Godot;
using System;

public partial class SPItem : Item
{
	[Export]
    public Item itemData;
    EventBus eventBus;
    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
    }

	public override void Use()
    {
        eventBus.EmitSignal(EventBus.SignalName.SPItemUsed, 3);
    }
}
