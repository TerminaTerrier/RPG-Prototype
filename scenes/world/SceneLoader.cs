using Godot;
using System;

public partial class SceneLoader : Node
{
	[Export]
	SceneData _sceneData;

    public override void _Ready()
    {
        Node TestArea1 = _sceneData.TestArea1.Instantiate();
		AddChild(TestArea1);
    }
}
