using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
	public class MapManager
	{
		public Dictionary<Vector2, TileData> map;
		private MapController _controller;

		private MapGenerateData _data;
		public Vector2 startPosition = Vector2.one * -1;

		public void SetMapController(MapController controller)
		{
			this._controller = controller;
		}

		public void GenerateMap(MapGenerateData data)
		{
			if (data.x_dim < 10 || data.y_dim < 10)
			{
				Debug.LogError("wrong map size");
				return;
			}

			_data = data;
			
			map = _controller.InstantiateMapTiles(_data);
			UnityEngine.Random.InitState(_data.seed);
			
			GenerateObstacles();
			GenerateStartAndEnd();
			Debug.Log(startPosition.ToString());			
		}

		public void InitializeMap()
		{
			_controller.InitializeMap();
		}

		private void GenerateObstacles()
		{	
			SetRandomTiles(TileState.Occupied, _data.obstacles_count);
		}

		private void GenerateStartAndEnd()
		{
			SetRandomTiles(TileState.Path, 2);
		}

		private void SetRandomTiles(TileState state, int count)
		{
			var settedTiles = 0;
			while (settedTiles < count)
			{
				var tilePos = RandomTileOnMap();

				var tile = map[tilePos];
				
				if (tile.state != TileState.Free)
					continue;

				tile.state = state;
				
				if(state == TileState.Occupied)
					RandomizeObstacle(tile);
				else if (state == TileState.Path && startPosition == Vector2.one * -1)
					startPosition = tile.mapPosition;
					
				settedTiles++;
			}
		}

		private Vector2 RandomTileOnMap()
		{
			Vector2 position;
			var xPos = UnityEngine.Random.Range(0f, (float)_data.x_dim - 1);
			var yPos = UnityEngine.Random.Range(0f, (float) _data.y_dim - 1);

			var roundedXPos = (int) Math.Round(xPos, 0);
			var roundedYPos = (int) Math.Round(yPos, 0);
			
			return new Vector2(roundedXPos, roundedYPos);
		}

		private void RandomizeObstacle(TileData tile)
		{
			tile.state = TileState.Occupied;
			//todo: get random obstacle
		}

		public void ClearMap()
		{
			map.Clear();
			_data.obstacles_count = 0;
			_data.seed = 0;
			_data.x_dim = 0;
			_data.y_dim = 0;
			startPosition = Vector2.one * -1;
		}

		public Vector2 ToVector2(Edge edge)
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

		public Edge ToEdge(Vector2 vec2)
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
	}


	public struct MapGenerateData
	{
		public int x_dim;
		public int y_dim;
		public int obstacles_count;
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
	}
}