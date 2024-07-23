using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class AttackAction : Node, IAction
{
    public Move Move { get; set; }
    public (IDamageable targetOne, IDamageable targetTwo) Targets {get; set;}
    public Stats ActorStats { get; set;}
    int power;
    int attack;
    
	public AttackAction(Move move, (Node2D target1, Node2D target2) targets, Stats actorStats)
	{
		Move = move;
        
        
        var t1 = (IDamageable)targets.target1;
        var t2 = (IDamageable)targets.target2;
        Targets = (t1, t2);
        
    
        ActorStats = actorStats;
	}
	
    public void Enact()
    {
        for(int i = 0; i < Move.HitNumber;)
        {
            var damage = CalculateDamageGiven();

            Targets.targetOne?.TakeDamage(damage);   
            Targets.targetTwo?.TakeDamage(damage);
                
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
