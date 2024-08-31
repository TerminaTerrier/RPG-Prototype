using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class BattleLog : ScrollContainer
{
	[Export]
	VBoxContainer vBoxContainer;
	Queue<RichTextLabel> labelQueue = new Queue<RichTextLabel>();
	EventBus eventBus;
	public override void _Ready()
	{
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.BattleUpdate += AddLabel;
		AddLabel("Battle Started!");
	}
 
    public void AddLabel(string text)
	{
		GD.Print("Adding Label...");
        var newLabel = new RichTextLabel
        {
            BbcodeEnabled = true,
		    FitContent = true 	
        };
        vBoxContainer.AddChild(newLabel);
		newLabel.Text = text;
		labelQueue.Enqueue(newLabel);

        RemoveLabel();
	} 

	private void RemoveLabel()
	{
		if(labelQueue.Count > 5)
		{
		    var oldLabel = labelQueue.Dequeue();
		    oldLabel.QueueFree();
		}
	}
	
	
}
