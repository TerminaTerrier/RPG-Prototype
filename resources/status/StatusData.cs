using Godot;
using System;

[GlobalClass]
public partial class StatusData : Resource
{
    [Export]
	public int turnLength;
	[Export]
	public int power;
    [Export]
    public float chance;
	[Export]
    public StatusFlag statusFlag;
    [Export]
    public Godot.Collections.Array<StatsToModify> statsToModify;
    [Export]
    public Godot.Collections.Dictionary<string, float> statModifiers;
	public enum StatusFlag
    {
        None,
        Modifier,
        Rooted,
        Magnetized,
        Sinking
    }
    public enum StatsToModify
    {
        Unaffected,
        Attack,
        Defense,
        Speed,
        Luck
    }
}
