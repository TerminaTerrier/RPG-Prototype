using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

public partial class BattleGUI : Control
{
	[Export]
	private ActionMenu _actionMenu;
	[Export]
	public GridContainer inventory;
	EventBus eventBus;
	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.PlayerTurnEnded += CloseInventory;
		eventBus.SPDepleted += (parentEntityName) => DisableActionMenuButtons();
		eventBus.SPReplenished += EnableActionMenuButtons;
		GD.Print("hello world");
        
	}
    
	public void SetActionMenuText(Moveset moveset)
	{
		var i = 0;
		var isNull = _actionMenu.ActionMenuButtons is null;
        GD.Print("Is ActionMenuButtons null? " + isNull);
		foreach(var move in moveset.moveset)
		{
			_actionMenu.ActionMenuButtons[i].Text = moveset.moveset[i].MoveText;
			i++;
		}
	}

	public void SkipTurn()
	{
		if(TurnManager.currentTurn == TurnManager.CurrentTurn.Player)
		{
            eventBus.EmitSignal(EventBus.SignalName.PlayerTurnEnded);
		}

		eventBus.EmitSignal(EventBus.SignalName.TurnSkipped);
	}

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("Exit"))
		{
			CloseInventory();
		}
    }

    public void OpenInventory()
	{
		//var inventoryScene = GD.Load<PackedScene>("res://UI/inventory.tscn");
		//inventory = inventoryScene.Instantiate();
		//inventory.SetDeferred("visible", true);
		inventory.Visible = true;
		inventory.GetChild<Panel>(0).GetChild<CenterContainer>(0).GetChild<TextureButton>(0).GrabFocus();
		DisableActionMenuButtons();
		GetChild<Button>(2).Disabled = true;
		GetChild<Button>(3).Disabled = true;
		//inventory.FocusMode = Control.FocusMode.All;
		//inventory.GrabFocus();
	}

	public void CloseInventory()
	{
		EnableActionMenuButtons();
	    GetChild<Button>(2).Disabled = false;
		GetChild<Button>(3).Disabled = false;
        inventory.Visible = false;
	}
    
	public void UnloadGUI()
	{
		CallDeferred("queue_free");
	}
    
	public void DisableActionMenuButtons()
	{
        foreach(var button in _actionMenu.ActionMenuButtons)
		{
			button.Disabled = true;
		}
	}

	public void EnableActionMenuButtons()
	{
        foreach(var button in _actionMenu.ActionMenuButtons)
		{
			button.Disabled = false;
		}
	}
}
