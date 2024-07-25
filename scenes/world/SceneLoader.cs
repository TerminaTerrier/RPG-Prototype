using Godot;
using System;
using System.Runtime.Serialization;

public partial class SceneLoader : Node
{
	[Export]
	SceneData _sceneData;
    Godot.Collections.Dictionary<string, GodotObject> scenes = new Godot.Collections.Dictionary<string, GodotObject>();
    public Resource playerStats {get; set;}
    public Resource enemyStats {get; set;}
    EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        
        eventBus.GameStarted += () =>
        {
            AddScene(_sceneData.Player, "Player");
            var player = (Player)scenes["Player"];
            player.playerStats = (Stats)playerStats;
            SetScenePosition("Player", new Vector2(150,150));
            LoadScene("Player");
 
            AddScene(_sceneData.TestArea1, "TestArea1");
            LoadScene("TestArea1");

            AddScene(_sceneData.Enemy, "Enemy");
            var enemy = (Enemy)scenes["Enemy"];
            enemy.enemyStats = (Stats)enemyStats;
            LoadScene("Enemy");
        };
    }

    public void AddScene(PackedScene scene, string key)
    {
       var newScene = scene.Instantiate();

       scenes.Add(key, newScene);  
    }

    public void LoadScene(string key)
    {
       AddChild((Node)scenes[key]);
    }

    public void SetScenePosition(string key, Vector2 position)
    {
        var scene = (Node2D)scenes[key];
        scene.Position = position;
    }

    public void LoadSubScene(Node child, string key)
    {
        child.AddChild((Node)scenes[key]);
    }

    public void UnloadScene(string key)
    {
       // CallDeferred("remove_child", (Node)scenes[key]);
    }

    public void UnloadAllScenes()
    {
        var childrenArray = GetChildren();

        foreach (var child in childrenArray)
        {
            CallDeferred("remove_child", child);
        }
    }
}
