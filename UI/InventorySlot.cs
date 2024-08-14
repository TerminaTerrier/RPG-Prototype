using Godot;
using System;

public partial class InventorySlot : Panel
{
	[Export]
	public TextureButton InventoryItemIcon;
	public bool IsSlotFilled {get; private set;}
	EventBus eventBus;
	
	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.PlayerDeath += ResetInventorySlot;
	}

	public void CreateButton(string itemID)
	{
		GD.Print("creating button...");

        if(itemID == "Health")
		{
			InventoryItemIcon.Disabled = false;

			var healthSprite = GD.Load<Texture2D>("res://assets/items/hpitem.png");
			var healthSpriteFocus = GD.Load<Texture2D>("res://assets/items/hpitem_focus.png");
			InventoryItemIcon.TextureNormal = healthSprite;
			InventoryItemIcon.TextureFocused = healthSpriteFocus;
			InventoryItemIcon.SizeFlagsHorizontal = SizeFlags.Fill;
		    InventoryItemIcon.SizeFlagsVertical = SizeFlags.Fill;
		}
		else if(itemID == "SpecialPoint")
		{
			InventoryItemIcon.Disabled = false;

			var spSprite = GD.Load<Texture2D>("res://assets/items/spitem.png");
			var spSpriteFocus = GD.Load<Texture2D>("res://assets/items/spitem_focus.png");
			InventoryItemIcon.TextureNormal = spSprite;
			InventoryItemIcon.TextureFocused = spSpriteFocus;
		    InventoryItemIcon.SizeFlagsHorizontal = SizeFlags.Fill;
		    InventoryItemIcon.SizeFlagsVertical = SizeFlags.Fill;
		}
	    
	    IsSlotFilled = true;
	}

	public void OnButtonPressed()
	{
        
        eventBus.EmitSignal(EventBus.SignalName.ItemSelected, GetIndex());
		eventBus.EmitSignal(EventBus.SignalName.PlayerTurnEnded);
		eventBus.EmitSignal(EventBus.SignalName.TurnEnded);
		InventoryItemIcon.TextureNormal = null;
		InventoryItemIcon.TextureFocused = null;
		IsSlotFilled = false;
	}

	public void ResetInventorySlot()
	{
        InventoryItemIcon.TextureNormal = null;
		InventoryItemIcon.TextureFocused = null;
		IsSlotFilled = false;
	}
}
