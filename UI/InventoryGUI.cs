using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ExceptionServices;

public partial class InventoryGUI : GridContainer
{
	const int inventorySize = 4;
	Godot.Collections.Array<Node> inventorySlots;
	int filledSlotCounter;
	EventBus eventBus;
	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ItemObtained += FillSlot;

		inventorySlots = GetChildren();
	}

	public void FillSlot(string itemID)
	{
        var slot = SelectSlot();

		GD.Print("filling slot...");

        if(itemID == "Health" && filledSlotCounter < inventorySize)
		{
            slot.CreateButton(itemID);
		}
		else if(itemID == "SpecialPoint" && filledSlotCounter < inventorySize)
		{
			slot.CreateButton(itemID);
		}	
	}

    public InventorySlot SelectSlot()
	{
        foreach(InventorySlot slot in inventorySlots)
		{
			if(slot.IsSlotFilled == false)
			{
				return slot;
			}
		}

		return null;
	}
}
