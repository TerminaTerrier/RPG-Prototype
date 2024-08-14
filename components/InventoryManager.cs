using Godot;
using System;
using System.Linq;

public partial class InventoryManager : Node2D
{
	[Export]
	public Inventory PlayerInventory;
    int filledInventorySlotCounter;
	int emptySlotIndex;
	EventBus eventBus;

    public override void _EnterTree()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ItemSelected += UseItem;
		eventBus.PlayerDeath += ClearInventory;
    }
    public override void _Ready()
    {
		var dir = DirAccess.Open("user://");
		
        if(dir.FileExists("user://PlayerInventory.tres"))
		{ 
			GD.Print(ResourceLoader.Load<Inventory>("user://PlayerInventory.tres") is null);
            PlayerInventory = ResourceLoader.Load<Inventory>("user://PlayerInventory.tres");
			
		}
		else
		{
			GD.Print("inventory manager is ready!");
		    PlayerInventory.inventory = new Item[4];
		}
    }

	public void ClearInventory()
	{
		Array.Clear(PlayerInventory.inventory);
		ResourceSaver.Save(PlayerInventory, "user://PlayerInventory.tres");
	}

    public void AddInventoryItems(Area2D area2D)
	{
		string itemID = (string)area2D.GetMeta("ItemType");
		

		GD.Print("this is item type: " + itemID);

		foreach(var item in PlayerInventory.inventory)
		{
			GD.Print("iterating...");
		    if(item == null)
		    {  
				break;
		    }

			if(item != null)
			{
				filledInventorySlotCounter++;
				emptySlotIndex = filledInventorySlotCounter;
			}
			
		}
        
		filledInventorySlotCounter = 0;
		GD.Print("Filled Slots: " + filledInventorySlotCounter);

		if(itemID == "Health")
		{
			GD.Print("item is health!");
            PlayerInventory.inventory[emptySlotIndex] = new HealthItem();
			eventBus.EmitSignal(EventBus.SignalName.ItemObtained, "Health");
		}
		else if(itemID == "SpecialPoint")
		{
			PlayerInventory.inventory[emptySlotIndex] = new SPItem();
			eventBus.EmitSignal(EventBus.SignalName.ItemObtained, "SpecialPoint");
		}

		ResourceSaver.Save(PlayerInventory, "user://PlayerInventory.tres");
	}
    
	public void UseItem(int slotNum)
	{
		GD.Print(slotNum);
		GD.Print(PlayerInventory.inventory[slotNum]);
        PlayerInventory.inventory[slotNum].Use(eventBus);
	    RemoveInventoryItem(slotNum);
	}

	public void RemoveInventoryItem(int slotNum)
	{
        PlayerInventory.inventory[slotNum] = null;
	}

    public override void _ExitTree()
    {
		eventBus.ItemSelected -= UseItem;
    }
}
