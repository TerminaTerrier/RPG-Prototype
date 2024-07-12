using Godot;
using System;

[GlobalClass]
public partial class Moveset : Resource
{
    [Export]
    public Godot.Collections.Array<Move> moveset = new Godot.Collections.Array<Move>();
}
