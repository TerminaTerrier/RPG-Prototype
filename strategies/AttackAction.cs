using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class AttackAction : Node, IAction
{
    public Move Move { get; set; }
    public IDamageable Target {get; set;}
    public Stats ActorStats { get; set;}
    int power;
    int attack;
    
	public AttackAction(Move move, IDamageable target, Stats actorStats)
	{
		Move = move;
        Target = target;
        ActorStats = actorStats;
	}
	
    public void Enact()
    {
        for(int i = 0; i < Move.HitNumber;)
        {
            var damage = CalculateDamageGiven();
            Target.TakeDamage(damage);
            i++;
        }    
    }
    
    private int CalculateDamageGiven()
    {
        power = Move.BaseStrength;
        attack = ActorStats.attack;
        var damage = attack * power;

        var rng = new RandomNumberGenerator();
        var critDeterminer = rng.RandfRange(0,1) * 100;

        if(critDeterminer < Move.CritChance)
        {
            damage = damage * 2;
        }

        return damage;
    }
   
}
