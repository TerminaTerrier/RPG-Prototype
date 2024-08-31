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
        GD.Print(t1.damageAffinity);
    
        ActorStats = actorStats;
       
	}
	
    public void Enact()
    {
        
        for(int i = 0; i < Move.HitNumber;)
        {
            var damage = CalculateDamageGiven();
            
            if(Move.MoveText != "Sap")
            {
                Targets.targetOne?.TakeDamage(damage);   
                Targets.targetTwo?.TakeDamage(damage);
            }
            else
            {
                Targets.targetTwo?.TakeDamage(damage);
                Targets.targetOne?.TakeDamage(-damage);
            }
            
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
        
        if(critDeterminer <= Move?.CritChance * ActorStats.luck)
        {
            damage = damage * 2;
        }

        switch(Move.moveAffinity)
        {
            case Move.MoveAffinity.Earth:
            {
                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Metal)
                {
                    damage = damage * 2;
                    
                }

                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Wood)
                {
                    damage = damage/2;
                }

                break;
            }
            case Move.MoveAffinity.Wood:
            {
                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Earth)
                {
                    damage = damage * 2;
                    
                }

                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Metal)
                {
                    damage = damage/2;
                }

                break;
            }
            case Move.MoveAffinity.Metal:
            {
                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Wood)
                {
                    damage = damage * 2;
                }

                if(Targets.targetOne.damageAffinity == IDamageable.DamageAffinity.Earth)
                {
                    damage = damage/2;
                } 

                break;
            }
        }
        GD.Print("damage: " + damage);
        return damage;
    }
   
}
