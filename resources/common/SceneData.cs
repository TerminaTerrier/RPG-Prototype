using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SceneData : Resource
{
    [Export]
    public PackedScene Player { get; set; }
    [Export]
    public PackedScene PlayerCamera { get; set; }
    [Export]
    public PackedScene BattlePlayer { get; set; }
    [Export]
    public PackedScene Enemy { get; set; }
    [Export]
    public PackedScene BattleEnemy{ get; set; }
    [Export]
    public PackedScene TestArea1 { get; set; }
    [Export]
    public PackedScene TextBox { get; set; }
    [Export]
    public PackedScene BattleGUI{ get; set; }
    [Export]
    public PackedScene BattleHUD { get; set; }
    [Export]
    public PackedScene StartScreen { get; set; } 
    [Export]
    public PackedScene DialoguePlayer{ get; set; }
    [Export]
    public PackedScene Inventory{get; set; }
    [Export]
    public PackedScene WorldHUD { get; set; }

}
