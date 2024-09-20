using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Stats : Resource
{
    [Export]
    public string statClassName; 
    [Export]
    public int attack;
    [Export]
    public int defense;
    [Export]
    public int speed;
    [Export]
    public int luck;
    [Export]
    public int maxHP;
    [Export]
    public int maxSP;
}
