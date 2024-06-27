using Godot;
using System;

[GlobalClass]
public partial class SceneData : Resource
{
    [Export]
    PackedScene player { get; set; }
    
}
