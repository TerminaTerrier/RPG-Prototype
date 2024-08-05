using Godot;
using System;

public abstract partial class Item : Node, IConsumable
{
    public abstract void Use();
}
