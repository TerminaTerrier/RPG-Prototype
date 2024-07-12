using Godot;
using System;

[GlobalClass]
public partial class Move : Resource
{
    [Export]
    public string MoveText;
    [Export]
    public Godot.Collections.Array<MoveType> moveTypes;
    [Export]
    public Target target;
    [Export]
    public int BaseStrength;
    [Export]
    public int HitNumber;
    [Export]
    public float CritChance;
    [Export]
    public int SPCost;
    [Export]
    public StatusFlag statusFlag;
    public enum Target
    {
        Self,
        Enemy
    }
    public enum MoveType
    {
        SingleAttack,
        MultiAttack,
        Debuff,
        Buff,
        Heal
    }
    public enum StatusFlag
    {
        None,
        Rooted,
        Magnetized,
        Sinking
    }
}
