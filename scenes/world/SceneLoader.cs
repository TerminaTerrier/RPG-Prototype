using Godot;
using System;
using System.Runtime.Serialization;

public partial class SceneLoader : Node
{
	[Export]
	SceneData _sceneData;
    Godot.Collections.Dictionary<string, GodotObject> scenes = new Godot.Collections.Dictionary<string, GodotObject>();
    EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        
        eventBus.GameStarted += () =>
        {
            AddScene(_sceneData.Player, "Player");
            SetScenePosition("Player", new Vector2(150,150));
            LoadScene("Player");
 
            AddScene(_sceneData.TestArea1, "TestArea1");
            LoadScene("TestArea1");

            AddScene(_sceneData.Enemy, "Enemy");
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
