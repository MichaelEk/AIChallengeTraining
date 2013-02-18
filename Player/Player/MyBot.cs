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
				int min = int.MaxValue;
				Location minLoc = ant;
				
				foreach (var point in state.FoodTiles)
				{
					int ras = state.GetDistance(point, ant);
					if (min > ras)
					{
						min = ras;
						minLoc = point;
					}
				}
				int x1 = ant.Col;
				int y1 = ant.Row;
				int x2 = minLoc.Col;
				int y2 = minLoc.Row;
				int dx = Math.Abs(x1-x2);
				int dy = Math.Abs(y1-y2);
				var direction = Direction.East;
				if (dx > 0)
				{
					if (dx < state.Width - dx)
						direction = GetDirectionX(x2 - x1);
					else
						direction = GetDirectionX(x1 - x2);
				}
				else if(dy > 0)
				{
					if (dy < state.Height - dy)
						direction = GetDirectionY(y2 - y1);
					else
						direction = GetDirectionY(y1 - y2);
				}

				Location newLoc = state.GetDestination(ant, direction);
				if (state.GetIsPassable(newLoc) && !usedMoves.Contains(newLoc))
				{
					usedMoves.Add(newLoc);
					IssueOrder(ant, direction);
				}
                /*for (int i = 0; i < 4; i++ )
                {
                    var direction = _directions[
						_random.Next(4)];

                    Location newLoc = state.GetDestination(ant, direction);

                    if (state.GetIsPassable(newLoc) && !usedMoves.Contains(newLoc))
                    {
                        usedMoves.Add(newLoc);
                        IssueOrder(ant, direction);
                        break;
                    }
                    
                }*/
				
				if (state.TimeRemaining < 10) break;
			}
			
		}

		private Direction GetDirectionX(int dx)
		{
			if (dx > 0)
				return Direction.East;
			else
				return Direction.West;
		}

		private Direction GetDirectionY(int dy)
		{
			if (dy > 0)
				return Direction.South;
			else
				return Direction.North;
		}
		
		public static void Main (string[] args) {
			new Ants().PlayGame(new MyBot());
		}

	}
	
}