using System;
using System.Linq;
using System.Collections.Generic;

namespace Ants 
{

	class MyBot : Bot 
    {
        Random _random = new Random();
	    private Direction[] _directions = Ants.Aim.Keys.ToArray();

		public override void DoTurn (IGameState state)
		{
		    var usedMoves = new HashSet<Location>();

			foreach (Ant ant in state.MyAnts) 
            {
                for (int i = 0; i < 4; i++ )
                {
                    var direction = _directions[_random.Next(4)];

                    Location newLoc = state.GetDestination(ant, direction);

                    if (state.GetIsPassable(newLoc) && !usedMoves.Contains(newLoc))
                    {
                        usedMoves.Add(newLoc);
                        IssueOrder(ant, direction);
                        break;
                    }
                }
				
				if (state.TimeRemaining < 10) break;
			}
			
		}
		
		
		public static void Main (string[] args) {
			new Ants().PlayGame(new MyBot());
		}

	}
	
}