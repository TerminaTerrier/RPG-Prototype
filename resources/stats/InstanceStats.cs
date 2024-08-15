using Godot;
using System;


[GlobalClass]
public partial class InstanceStats : Resource
{
    [Export]
    public int Health { get; set; }
    [Export]
    public int SP { get; set; }

    public InstanceStats() : this(0, 0) {}

    public InstanceStats(int health, int sp)
    {
        Health = health;
        SP = sp;
    }
}
