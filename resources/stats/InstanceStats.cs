using Godot;
using System;


[GlobalClass]
public partial class InstanceStats : Resource
{
    [Export]
    public int Health {get;set;}
    [Export]
    public int SP {get; set;}

    public InstanceStats()
    {
         //C# doesn't define a parameterless constructor if you define one with parameters so I have to make one myself.
    }

    public InstanceStats(int health, int sp)
    {
        Health = health;
        SP = sp;
    }
}
