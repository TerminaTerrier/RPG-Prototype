using Godot;
using System;

public partial class SceneLoader : Node
{
	[Export]
	SceneData _sceneData;
    Godot.Collections.Dictionary<string, GodotObject> scenes = new Godot.Collections.Dictionary<string, GodotObject>();
    EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        eventBus.StartBattle += UnloadAllScenes;
        
        AddScene(_sceneData.TestArea1, "TestArea1");
        LoadScene("TestArea1");

        AddScene(_sceneData.Enemy, "Enemy");
        LoadScene("Enemy");


        //Node TestArea1 = _sceneData.TestArea1.Instantiate();
		//AddChild(TestArea1);
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

    public void UnloadScene(string key)
    {
        CallDeferred("remove_child", (Node)scenes[key]);
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
