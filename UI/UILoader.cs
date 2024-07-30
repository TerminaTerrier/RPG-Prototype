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
        
        AddUIElement(_sceneData.StartScreen, "StartScreen");
        LoadUIElement("StartScreen");

        AddUIElement(_sceneData.BattleGUI, "BattleGUI");
        AddUIElement(_sceneData.DialoguePlayer, "DialoguePlayer"); 
        AddUIElement(_sceneData.BattleHUD, "BattleHUD"); 

        eventBus.GameStarted += () => 
        {
            RemoveChild((Node)elements["StartScreen"]);
            LoadUIElement("DialoguePlayer");
        };

		eventBus.StartBattle += (Stats enemyStats, Stats playerStats) => 
        { 
            LoadUIElement("BattleHUD"); 
            var battleHUD = (BattleHUD)elements["BattleHUD"];
            battleHUD.SetMaxValues(enemyStats, playerStats);
        };

        eventBus.PlayerTurnStarted += (Moveset moveset) => 
        {
            LoadUIElement("BattleGUI"); 
            var battleGUI = (BattleGUI)elements["BattleGUI"];
            battleGUI.SetActionMenuText(moveset);
        };

        eventBus.PlayerTurnEnded += () =>
        {
            var battleGUI = (BattleGUI)elements["BattleGUI"];
            RemoveChild(battleGUI);
        };
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
