using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class EnemyAI : Node
{
	Random random = new Random();
	public int PickMove(Moveset moveset, int currentHealth)
	{
        List<int> moveScores = PrioritizeMove(moveset, currentHealth);
	    List<bool> validMoves = new List<bool>();
		int randomIndex;
        GD.Print("Picking Move");
		foreach(int moveScore in moveScores)
		{
			if(moveScore >= 3)
			{
				validMoves.Add(true);
			}
			else
			{
				validMoves.Add(false);
			}
		}
        
		var validMoveCount = validMoves.Count;
        
		do
		{
           randomIndex = random.Next(validMoveCount);
		}
		while(validMoves[randomIndex] == false);
		
		return randomIndex;
	}

	private List<int> PrioritizeMove(Moveset moveset, int currentHealth)
	{
	    int i = 0;
		List<int> moveScores = new List<int>();
        GD.Print("Prioritizing Move...");
        foreach(Move move in moveset.moveset)
		{
			int priorityScore = 0;

            switch(move.moveTypes[i])
			{
				case Move.MoveType.Attack:
				{
                    priorityScore = priorityScore + 2;

					if(move.SPCost < 5)
					{
						priorityScore = priorityScore + 2;
					}

					if(move.BaseStrength > 1)
					{
						priorityScore = priorityScore + 1;
					}

					if(move.CritChance > 50)
					{
						priorityScore = priorityScore + 1;
					}
					break;
				}
				case Move.MoveType.StatusEffect:
				{
                    priorityScore = priorityScore + 1;
                    
					if(move.SPCost < 5)
					{
						priorityScore = priorityScore + 1;
					}

					if(move.status.turnLength > 1)
					{
						priorityScore = priorityScore + 1;
					}

					if(move.status.chance > 50)
					{
						priorityScore = priorityScore + 1;
					}

					if(currentHealth < 10)
					{
						priorityScore = priorityScore + 3;
					}
					break;
				}


			}
            
			
			moveScores.Add(priorityScore);
			
		}

		return moveScores;
	}
}
