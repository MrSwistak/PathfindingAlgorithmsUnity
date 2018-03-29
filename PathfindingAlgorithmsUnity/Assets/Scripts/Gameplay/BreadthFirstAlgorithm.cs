using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class BreadthFirstAlgorithm : MonoBehaviour
	{
		[Inject] private MapManager _mapManager;
		
		private Queue<Vector2> _first;
		private HashSet<Vector2> _visited;

		public void CalculatePath()
		{
			_first = new Queue<Vector2>();
			
			_first.Enqueue(_mapManager.startPosition);

			_visited = new HashSet<Vector2>();
			
			_visited.Add(_mapManager.startPosition);

			while (_first.Count > 0)
			{
				var current = _first.Dequeue();

				foreach (var next in GetNeighbours(current))
				{
					_first.Enqueue(next);
					_visited.Add(next);
				}
			}
			_mapManager.InitializeMap();
		}
		
		public List<Vector2> GetNeighbours(Vector2 position)
		{
			var neighbours = new List<Vector2>();
			
			foreach (Edge edge in Edge.GetValues(typeof(Edge)))
			{
				var pos = _mapManager.ToVector2(edge) + position;

				if (!_mapManager.map.ContainsKey(pos))
					continue;
				
				if(_mapManager.map[pos].state == TileState.Occupied)
					continue;

				if (_visited.Contains(pos))
					continue;
				
				
				neighbours.Add(pos);
				_mapManager.map[pos].edge = GetOppositeEdge(edge);
			}

			return neighbours;
		}

		public Edge GetOppositeEdge(Edge edge)
		{
			switch (edge)
			{
				case Edge.Up:
					return Edge.Down;
				case Edge.Down:
					return Edge.Up;
				case Edge.Left:
					return Edge.Right;
				case Edge.Right:
					return Edge.Left;
				default:
					return Edge.Down;
			}
		}
	}
}