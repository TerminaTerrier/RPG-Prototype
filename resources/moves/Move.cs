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
    public MoveAffinity moveAffinity;
    [Export]
    public int BaseStrength;
    [Export]
    public int HitNumber;
    [Export]
    public float CritChance;
    [Export]
    public int SPCost;
    [Export]
    public StatusData status;
    [Flags]
    public enum Target
    {
        None = 0,
        Self = 1,
        Enemy = 2,
        SelfAndEnemy = Self | Enemy
    }
    public enum MoveType
    {
        Attack,
        StatusEffect
    }
    public enum MoveAffinity
    {
        None,
        Earth,
        Wood,
        Metal
    }
  
}
