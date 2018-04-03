using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
	public class MapManager
	{
		public Dictionary<Vector2, TileData> map;
		private MapController _controller;

		public MapData data;
		public Vector2 startPosition = Vector2.one * -1;
		public Vector2 endPosition = Vector2.one * -1;

		public void SetMapController(MapController controller)
		{
			this._controller = controller;
		}

		public void GenerateMap(MapData data)
		{
			if (data.xDim < 10 || data.yDim < 10)
			{
				Debug.LogError("wrong map size");
				return;
			}

			this.data = data;
			
			map = _controller.InstantiateMapTiles(this.data);
			UnityEngine.Random.InitState(this.data.seed);
			
			GenerateObstacles();
			GenerateStartAndEnd();
			_controller.InitializeMap();
		}

		private void GenerateObstacles()
		{	
			SetRandomTiles(TileState.Occupied, data.obstaclesCount);
		}

		private void GenerateStartAndEnd()
		{
			SetRandomTiles(TileState.Path, 2);
			map[startPosition].edge = Edge.Zero;
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
				else if (state == TileState.Path && endPosition == Vector2.one * -1)
					endPosition = tile.mapPosition;
					
				settedTiles++;
			}
		}

		private Vector2 RandomTileOnMap()
		{
			Vector2 position;
			var xPos = UnityEngine.Random.Range(0f, (float)data.xDim - 1);
			var yPos = UnityEngine.Random.Range(0f, (float) data.yDim - 1);

			var roundedXPos = (int) Math.Round(xPos, 0);
			var roundedYPos = (int) Math.Round(yPos, 0);
			
			return new Vector2(roundedXPos, roundedYPos);
		}

		private void RandomizeObstacle(TileData tile)
		{
			tile.state = TileState.Occupied;
			tile.edge = Edge.Zero;
			//todo: get random obstacle
		}

		public void GeneratePathway()
		{
			var currentTile = endPosition;

			while (currentTile != Vector2.zero)
			{
				map[currentTile].state = TileState.Path;
				var nextTile = MapUtils.GetPreviusTile(currentTile, map);
				currentTile = nextTile;
			}
			InitializeMap();
		}

		public void ClearPath()
		{
			foreach (KeyValuePair<Vector2, TileData> tile in map)
			{
				tile.Value.edge = Edge.Zero;

				if (tile.Value.state == TileState.Occupied)
					continue;

				if (tile.Key == startPosition)
					continue;

				if (tile.Key == endPosition)
					continue;
				
				tile.Value.state = TileState.Free;
			}
			InitializeMap();
		}

		public void ClearMap()
		{
			map.Clear();
			data.saveName = string.Empty;
			data.obstaclesCount = 0;
			data.seed = 0;
			data.xDim = 0;
			data.yDim = 0;
			startPosition = Vector2.one * -1;
			endPosition = Vector2.one * -1;
			_controller.DestroyMap();
			InitializeMap();
		}
		
		public void InitializeMap()
		{
			_controller.InitializeMap();
		}
	}
}