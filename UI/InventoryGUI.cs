using Godot;
using System;
using System.Runtime.ExceptionServices;

public partial class InventoryGUI : GridContainer
{
	const int inventorySize = 4;
	PackedScene healthItem = GD.Load<PackedScene>("res://UI/health_item_inventory_icon.tscn");
	PackedScene specialPointItem = GD.Load<PackedScene>("res://UI/sp_item_inventory_icon.tscn");
	int filledSlotCounter;
	EventBus eventBus;
	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ItemObtained += FillSlot;
	}

	public void FillSlot(int itemType)
	{
        if(itemType == 1 && filledSlotCounter < inventorySize)
		{
            var hpItem = (TextureButton)healthItem.Instantiate();
			hpItem.Pressed += () => EmptySlot(filledSlotCounter);
			AddChild(hpItem);
			filledSlotCounter++;
		}
		else if(itemType == 2 && filledSlotCounter < inventorySize)
		{
			var spItem = (TextureButton)specialPointItem.Instantiate();
			spItem.Pressed += () => EmptySlot(filledSlotCounter);
			AddChild(spItem);
			filledSlotCounter++;
		}	
	}

	public void EmptySlot(int slotNum)
	{
        var item = GetChild<TextureButton>(slotNum - 1);
		GD.Print("slot is null? " + item is null);
		GD.Print("emptying slot " + slotNum);
		item.QueueFree();
		eventBus.EmitSignal(EventBus.SignalName.ItemSelected, slotNum - 1);
		filledSlotCounter--;
	}
}
