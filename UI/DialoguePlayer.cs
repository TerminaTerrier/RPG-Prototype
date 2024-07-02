using Godot;
using System;
using System.Linq;

public partial class DialoguePlayer : Control
{
	[Export]
	SceneData sceneData;
	EventBus eventBus;
	TextBox textBox;
	bool _isTextBoxLoaded;
	int textPageCount = 0;

	public override void _Ready()
	{
		eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.LoadText += LoadText;
		eventBus.UnloadText += UnloadText;
	}

	private void LoadText(string[] text)
	{
		var length = text.Length;
		
        
		if(_isTextBoxLoaded == false)
		{
            textBox = (TextBox)sceneData.TextBox.Instantiate();
		    AddChild(textBox);
			_isTextBoxLoaded = true;
		}
		
        if(textPageCount < length)
		{
            textBox.label.Text = text[textPageCount];
			textPageCount++;
		}
	}

	private void UnloadText()
	{
		textPageCount = 0;
		textBox.label.Text = null;
        textBox.QueueFree();
		_isTextBoxLoaded = false;
	}

}
