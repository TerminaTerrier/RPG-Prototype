using Godot;
using System;

public partial class BattleGUI : Control
{
	[Export]
	private ActionMenu _actionMenu;
	EventBus eventBus;
	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.TurnEnded += UnloadActionMenu;
		eventBus.SPDepleted += (parentEntityName) => DisableActionMenuButtons();
		eventBus.SPReplenished += EnableActionMenuButtons;
	}
    
	public void SetActionMenuText(Moveset moveset)
	{
		var i = 0;
        GD.Print(_actionMenu.ActionMenuButtons[i].Text);
		foreach(var move in moveset.moveset)
		{
			_actionMenu.ActionMenuButtons[i].Text = moveset.moveset[i].MoveText;
			i++;
		}
	}

	public void UnloadActionMenu()
	{
		RemoveChild(_actionMenu);
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
