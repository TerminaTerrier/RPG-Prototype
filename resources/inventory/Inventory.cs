using Godot;
using System;

[GlobalClass]
public partial class Inventory : Resource
{
    [Export]
    public Item[] inventory;
}
