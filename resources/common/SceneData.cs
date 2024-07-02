using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SceneData : Resource
{
    [Export]
    public PackedScene Player { get; set; }
    [Export]
    public PackedScene TestArea1 { get; set; }
    [Export]
    public PackedScene TextBox { get; set; }
}
