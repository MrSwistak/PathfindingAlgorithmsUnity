using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class BreadthFirstAlgorithm : BaseAlgorithm
	{		
		private Queue<Vector2> _first;
		private HashSet<Vector2> _visited;

		public override void CalculatePath()
		{
			var map = mapManager.map;
			
			_first = new Queue<Vector2>();
			
			_first.Enqueue(mapManager.startPosition);

			_visited = new HashSet<Vector2>();
			
			_visited.Add(mapManager.startPosition);

			while (_first.Count > 0)
			{
				var current = _first.Dequeue();

				foreach (var next in MapUtils.GetNeighbourTiles(current, mapManager.map))
				{
					if (map[next].state == TileState.Occupied) //if occupied by obstacle
						continue;

					if (_visited.Contains(next)) //if already checked
						continue;

					var edge = MapUtils.FromVec2ToEdge(current - next);
					map[next].edge = MapUtils.GetOppositeEdge(edge);
					
					_first.Enqueue(next);
					_visited.Add(next);
				}
			}
			mapManager.GeneratePathway();
		}
	}
}