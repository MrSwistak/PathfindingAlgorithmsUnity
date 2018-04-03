using System;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

namespace Gameplay
{
	public class AStarAlgorithm: BaseAlgorithm
	{
		private Dictionary<Vector2, Vector2> cameFrom;
		private Dictionary<Vector2, double> costSoFar;	
		
		public override void CalculatePath()
		{
			cameFrom = new Dictionary<Vector2, Vector2>();
			costSoFar = new Dictionary<Vector2, double>();
			
			var startPos = mapManager.startPosition;
			var endPos = mapManager.endPosition;
			
			var first = new SimplePriorityQueue<Vector2, double>();
			first.Enqueue(startPos , 0);
			
			
			cameFrom[startPos] = startPos;
			costSoFar[startPos] = 0;

			while (first.Count > 0)
			{
				var current = first.Dequeue();
				
				if (current.Equals(endPos))
				{
					break;
				}
				
				foreach (var next in MapUtils.GetNeighbourTiles(current, mapManager.map))
				{
					double newCost = costSoFar[current] + 1; //changeable later - currently costs always 1
					
					if (mapManager.map[next].state == TileState.Occupied) //if occupied by obstacle
						continue;
					
					if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
					{
						var edge = MapUtils.FromVec2ToEdge(current - next);
						mapManager.map[next].edge = MapUtils.GetOppositeEdge(edge);
						
						costSoFar[next] = newCost;
						double priority = newCost + Heuristic(next, endPos);
						first.Enqueue(next, priority);
						cameFrom[next] = current;
					}
				}
			}
			mapManager.GeneratePathway();
		}

		private double Heuristic(Vector2 a, Vector2 b)
		{
			return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
		}
	}
}