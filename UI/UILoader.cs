using Godot;
using System;

public partial class UILoader : CanvasLayer
{
	[Export]
	SceneData _sceneData;
	 Godot.Collections.Dictionary<string, GodotObject> elements = new Godot.Collections.Dictionary<string, GodotObject>();
	EventBus eventBus;
    public Stats playerStats;
    public Stats enemyStats;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        
        AddUIElement(_sceneData.StartScreen, "StartScreen");
        LoadUIElement("StartScreen");

        AddUIElement(_sceneData.BattleGUI, "BattleGUI");
        AddUIElement(_sceneData.DialoguePlayer, "DialoguePlayer"); 
        AddUIElement(_sceneData.BattleHUD, "BattleHUD"); 
        AddUIElement(_sceneData.Inventory, "Inventory");
        AddUIElement(_sceneData.WorldHUD, "WorldHUD");
        AddUIElement(_sceneData.BattleLog, "BattleLog");

        eventBus.GameStarted += () => 
        {
            RemoveChild((Node)elements["StartScreen"]);
            LoadUIElement("DialoguePlayer");
            LoadUIElement("Inventory");
            LoadUIElement("WorldHUD");
            var worldHUD = (WorldHUD)elements["WorldHUD"];     
            worldHUD.SetMaxValues(playerStats);       

        };

		eventBus.StartBattle += () => 
        { 
            RemoveChild((Node)elements["WorldHUD"]);
            LoadUIElement("BattleHUD"); 
            LoadUIElement("BattleLog");
            var battleHUD = (BattleHUD)elements["BattleHUD"];
            battleHUD.SetMaxValues(enemyStats, playerStats);
        };

        eventBus.PlayerTurnStarted += LoadBattleGUI;

        eventBus.PlayerTurnEnded += () =>
        {
            var battleGUI = (BattleGUI)elements["BattleGUI"];
            battleGUI.CloseInventory();
            RemoveChild(battleGUI);
        };

        eventBus.EnemyDeath += () => 
        {
            RemoveChild((Node)elements["BattleGUI"]);
            RemoveChild((Node)elements["BattleHUD"]);
            RemoveChild((Node)elements["BattleLog"]);
            LoadUIElement("WorldHUD");
  
        
        };

		eventBus.PlayerDeath += () =>
        {
            //RemoveChild((Node)elements["BattleGUI"]);
            RemoveChild((Node)elements["BattleHUD"]);
            RemoveChild((Node)elements["BattleLog"]);
            LoadUIElement("WorldHUD");

            
        };
    }

    public void LoadBattleGUI(Moveset moveset)
    {
        LoadUIElement("BattleGUI"); 
        
        bool isMovesetNull = moveset is null;
        GD.Print("Is moveset null while loading Battle GUI?");
        GD.Print(isMovesetNull);

        var battleGUI = (BattleGUI)elements["BattleGUI"];
        battleGUI.CallDeferred("SetActionMenuText", moveset);
        battleGUI.inventory = (GridContainer)elements["Inventory"];
    }

	public void AddUIElement(PackedScene scene, string key)
    {
        var newUIElement = scene.Instantiate();
        elements.Add(key, newUIElement);  
    }

	public void LoadUIElement(string key)
    {
        CallDeferred("add_child", (Node)elements[key]);
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
