using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
	public class ObstacleRandomizer
	{	
		List<ObstacleType> _obstacles = new List<ObstacleType>();
		private Dictionary<Vector2, TileData> _map;
		private TileData _tile;
		
		public void GenerateObstacle(TileData tile, Dictionary<Vector2, TileData> map)
		{
			this._tile = tile;
			this._map = map;
			_obstacles.Clear();
			
			_obstacles.Add(ObstacleType.zero);
			
			CheckFreeSpacesAroundDesiredPosition();
			var obstacle = RandomizeObstacle();
			SetObstacle(obstacle);
		}

		private void CheckFreeSpacesAroundDesiredPosition()
		{
			var directNeighbourhood = new List<Edge>();

			foreach (Edge edge in Edge.GetValues(typeof(Edge)))
			{
				var pos = MapUtils.FromEdgeToVec2(edge) + _tile.mapPosition;

				if (!_map.ContainsKey(pos))
					continue;

				if (_map[pos].state != TileState.Free)
					continue;

				directNeighbourhood.Add(edge);
			}

			CheckDiagonalPositions(directNeighbourhood, Edge.Down);
			CheckDiagonalPositions(directNeighbourhood, Edge.Up);

			if (directNeighbourhood.Contains(Edge.Left))
				_obstacles.Add(ObstacleType.left);

			if (directNeighbourhood.Contains(Edge.Right))
				_obstacles.Add(ObstacleType.right);
		}

		private void CheckDiagonalPositions(List<Edge> directNeighbourhood, Edge edge)
		{
			if(edge == Edge.Right || edge == Edge.Left)
				throw new ArgumentException();

			var pos = _tile.mapPosition;
			var edgePos = MapUtils.FromEdgeToVec2(edge);
			var edgeLeftPos = MapUtils.FromEdgeToVec2(Edge.Left);
			var edgeRightPos = MapUtils.FromEdgeToVec2(Edge.Right);
			
			if (directNeighbourhood.Contains(edge))
			{
				if(edge == Edge.Down)
					_obstacles.Add(ObstacleType.down);
				else
					_obstacles.Add(ObstacleType.up);
				
				if (directNeighbourhood.Contains(Edge.Left))
				{
					if (_map[pos + edgePos + edgeLeftPos].state == TileState.Free)
					{
						if(edge == Edge.Down)
							_obstacles.Add(ObstacleType.left_down);
						else
							_obstacles.Add(ObstacleType.left_up);
					}
				}

				if(directNeighbourhood.Contains(Edge.Right))
				{
					if (_map[pos + edgePos + edgeRightPos].state == TileState.Free)
					{
						if(edge == Edge.Down)
							_obstacles.Add(ObstacleType.right_down);
						else
							_obstacles.Add(ObstacleType.right_up);
					}
				}
			}
		}

		private ObstacleType RandomizeObstacle()
		{
			var randomValue = UnityEngine.Random.Range(0f, _obstacles.Count);
			for(int i = 0; i < _obstacles.Count; i++)
			{
				if (randomValue < 1)
					return _obstacles[i];
				else
					randomValue -= 1f;
			}

			return ObstacleType.zero;
		}

		private void SetObstacle(ObstacleType type)
		{
			var tiles = new List<Vector2>();

			var mainTilePos = _tile.mapPosition;
			
			tiles.Add(_tile.mapPosition);
			
			var pos = _tile.mapPosition;
			var edgeUpPos = MapUtils.FromEdgeToVec2(Edge.Up);
			var edgeDownPos = MapUtils.FromEdgeToVec2(Edge.Down);
			var edgeLeftPos = MapUtils.FromEdgeToVec2(Edge.Left);
			var edgeRightPos = MapUtils.FromEdgeToVec2(Edge.Right);

			switch (type)
			{
				case ObstacleType.down:
					tiles.Add(mainTilePos + edgeDownPos);
					break;
				
				case ObstacleType.up:
					tiles.Add(mainTilePos + edgeUpPos);
					break;
				
				case ObstacleType.left:
					tiles.Add(mainTilePos + edgeLeftPos);
					break;
				
				case ObstacleType.right:
					tiles.Add(mainTilePos + edgeRightPos);
					break;
				
				case ObstacleType.left_down:
					tiles.Add(mainTilePos + edgeDownPos);
					tiles.Add(mainTilePos + edgeLeftPos);
					tiles.Add(mainTilePos + edgeDownPos + edgeLeftPos);
					break;
				
				case ObstacleType.left_up:
					tiles.Add(mainTilePos + edgeUpPos);
					tiles.Add(mainTilePos + edgeLeftPos);
					tiles.Add(mainTilePos + edgeUpPos + edgeLeftPos);
					break;
				
				case ObstacleType.right_down:
					tiles.Add(mainTilePos + edgeDownPos);
					tiles.Add(mainTilePos + edgeRightPos);
					tiles.Add(mainTilePos + edgeDownPos + edgeRightPos);
					break;
				
				case ObstacleType.right_up:
					tiles.Add(mainTilePos + edgeUpPos);
					tiles.Add(mainTilePos + edgeRightPos);
					tiles.Add(mainTilePos + edgeUpPos + edgeRightPos);
					break;
					
			}

			foreach (Vector2 tile in tiles)
			{
				_map[tile].state = TileState.Occupied;
				_map[tile].edge = Edge.Zero;
			}
		}
	}

	public enum ObstacleType
	{
		left = 0,
		right = 1,
		up = 2,
		down = 3,
		left_up = 4,
		left_down = 5,
		right_up = 6,
		right_down = 7,
		zero = 8,
	}
}