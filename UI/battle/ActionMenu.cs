using Godot;
using System;
using System.Linq;
public partial class ActionMenu : GridContainer
{   
    [Export]
	public Button[] ActionMenuButtons { get; set; }
	EventBus eventBus;

    public override void _EnterTree()
    {
        ActionMenuButtons[0]?.CallDeferred("grab_focus");
    }
    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
		//ActionMenuButtons = GetChildren().Cast<Button>().ToArray();
		GD.Print("ActionMenuButtons length is: " + ActionMenuButtons.Length);
		
    }

    public void OnButtonOnePressed()
	{
		eventBus.EmitSignal(EventBus.SignalName.ActionSelected, 0);
	}

	public void OnButtonTwoPressed()
	{
        eventBus.EmitSignal(EventBus.SignalName.ActionSelected, 1);
	}

	public void OnButtonThreePressed()
	{
        eventBus.EmitSignal(EventBus.SignalName.ActionSelected, 2);
	}

	public void OnButtonFourPressed()
	{
        eventBus.EmitSignal(EventBus.SignalName.ActionSelected, 3);
	}
}
