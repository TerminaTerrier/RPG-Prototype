using Godot;
using System;
using System.Linq;

public partial class InventoryManager : Node
{
	[Export]
	public Inventory PlayerInventory;
    int filledInventorySlotCounter;
	EventBus eventBus;

    public override void _EnterTree()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.ItemObtained += AddInventoryItems;
		eventBus.ItemSelected += UseItem;
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
		    PlayerInventory.inventory = new Item[4];
		}
    }

    public void AddInventoryItems(int itemType)
	{
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
			}
			GD.Print("Filled Slots: " + filledInventorySlotCounter);
		}

		if(itemType == 1)
		{   
            PlayerInventory.inventory[filledInventorySlotCounter] = new HealthItem();
			GD.Print(PlayerInventory.inventory[filledInventorySlotCounter]);
			
		}
		else if(itemType == 2)
		{
		    PlayerInventory.inventory[filledInventorySlotCounter] = new SPItem();
			GD.Print(PlayerInventory.inventory[filledInventorySlotCounter]);
		}

		ResourceSaver.Save(PlayerInventory, "user://PlayerInventory.tres");
	}
    
	public void UseItem(int slotNum)
	{
		GD.Print(slotNum);
		GD.Print(PlayerInventory.inventory[slotNum]);
       //PlayerInventory.inventory[slotNum].Use();
	   //RemoveInventoryItem(slotNum);
	}

	public void RemoveInventoryItem(int slotNum)
	{
        PlayerInventory.inventory[slotNum] = null;
	}

    public override void _ExitTree()
    {
		eventBus.ItemObtained -= AddInventoryItems;
		eventBus.ItemSelected -= UseItem;
    }
}
