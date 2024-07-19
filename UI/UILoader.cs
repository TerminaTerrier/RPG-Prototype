using Godot;
using System;

public partial class UILoader : CanvasLayer
{
	[Export]
	SceneData _sceneData;
	 Godot.Collections.Dictionary<string, GodotObject> elements = new Godot.Collections.Dictionary<string, GodotObject>();
	EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.StartBattle += () => { AddUIElement(_sceneData.BattleGUI, "BattleGUI"); AddUIElement(_sceneData.BattleHUD, "BattleHUD"); LoadUIElement("BattleHUD"); LoadUIElement("BattleGUI"); };
    }

	public void AddUIElement(PackedScene scene, string key)
    {
       var newUIElement = scene.Instantiate();

       elements.Add(key, newUIElement);  
    }

	public void LoadUIElement(string key)
    {
       AddChild((Node)elements[key]);
    }

    public void UnloadAllUIElements()
    {
        var childrenArray = GetChildren();

        foreach (var child in childrenArray)
        {
            CallDeferred("remove_child", child);
        }
    }
}
