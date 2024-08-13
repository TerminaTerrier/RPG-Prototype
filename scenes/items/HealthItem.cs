using Godot;
using System;

public partial class HealthItem : Item
{
    [Export]
    public ItemData itemData;
    EventBus eventBus;
 
    public override void Use(EventBus eventBus)
    {
        eventBus.EmitSignal(EventBus.SignalName.HealthItemUsed, 3);
    }

    
}
