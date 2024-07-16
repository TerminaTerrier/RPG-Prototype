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
	public enum StatusFlag
    {
        None,
        Rooted,
        Magnetized,
        Sinking
    }
}
