using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{	
	public class MapController : MonoBehaviour
	{
		[Inject] private MapManager _manager;
		public GameObject _tilePrefab;

		[SerializeField] private float xPrefabSize;
		[SerializeField] private float yPrefabSize;

		private void Awake()
		{
			_manager.SetMapController(this);
		}

		public Dictionary<Vector2, TileData> InstantiateMapTiles(MapGenerateData data)
		{
			var map = new Dictionary<Vector2, TileData>();
			for (int x = 0; x < data.x_dim; x++)
			{
				for (int y = 0; y < data.y_dim; y++)
				{
					var position = new Vector2(x, y);
					var tileWorldPos = new Vector3(x * xPrefabSize , y * yPrefabSize, 0f);
						
					GameObject instantiatedTile = Instantiate(_tilePrefab, position, Quaternion.identity, this.transform);
					var tileData = instantiatedTile.GetComponent<TileData>();

					tileData.mapPosition = position;
					map.Add(position, tileData);
				}
			}
			return map;
		}

		public void InitializeMap()
		{
			foreach (var mapTile in _manager.map)
			{
				mapTile.Value.InitializeTile();
			}
		}
	}
}