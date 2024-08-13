using Godot;
using System;

public partial class SPItem : Item
{
	[Export]
    public Item itemData;
    EventBus eventBus;
    
	public override void Use(EventBus eventBus)
    {
        eventBus.EmitSignal(EventBus.SignalName.SPItemUsed, 3);
    }
}
