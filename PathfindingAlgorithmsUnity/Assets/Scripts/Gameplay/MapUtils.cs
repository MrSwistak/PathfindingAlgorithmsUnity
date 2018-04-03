using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Gameplay
{
	public static class MapUtils
	{
		public static Vector2 FromEdgeToVec2(Edge edge)
		{
			switch (edge)
			{
				case Edge.Up:
					return Vector2.up;
				case Edge.Down:
					return Vector2.down;
				case Edge.Left:
					return Vector2.left;
				case Edge.Right:
					return Vector2.right;
				default:
					return Vector2.zero;
			}
		}

		public static Edge FromVec2ToEdge(Vector2 vec2)
		{
			if (vec2 == Vector2.up)
				return Edge.Up;
			else if (vec2 == Vector2.down)
				return Edge.Down;
			else if (vec2 == Vector2.left)
				return Edge.Left;
			else if (vec2 == Vector2.right)
				return Edge.Right;
			else
			{
				Debug.LogError("wrong edge");
				return Edge.Down;
			}
		}
		
		public static Edge GetOppositeEdge(Edge edge)
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
		
		public static List<Vector2> GetNeighbourTiles(Vector2 position, Dictionary<Vector2, TileData> map)
		{
			var neighbours = new List<Vector2>();
			
			foreach (Edge edge in Edge.GetValues(typeof(Edge)))
			{
				var pos = MapUtils.FromEdgeToVec2(edge) + position;

				if (!map.ContainsKey(pos))
					continue;
						
				neighbours.Add(pos);
			}

			return neighbours;
		}

		public static Vector2 GetPreviusTile(Vector2 position, Dictionary<Vector2, TileData> map)
		{
			var previusTile = position + FromEdgeToVec2(GetOppositeEdge(map[position].edge));
			if (map[previusTile].edge == Edge.Zero || !map.ContainsKey(previusTile))
				return Vector2.zero;
			else
				return previusTile;
		}
	}
	
	[System.Serializable]
	public struct MapData
	{
		public string saveName;
		public int xDim;
		public int yDim;
		public int obstaclesCount;
		public int seed;
	}

	public enum TileState
	{
		Free = 0,
		Occupied = 1,
		Path = 2,
	}
	
	public enum Edge
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
		Zero = 4,
	}
}