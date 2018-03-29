using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
	public class TileData : MonoBehaviour
	{
		public Vector2 mapPosition;
		public TileState state;

		public Edge edge;
		public GameObject arrow;

		[SerializeField] private Material _freeTileColor;
		[SerializeField] private Material _ocupiedTileColor;
		[SerializeField] private Material _pathTileColor;

		private MeshRenderer _meshRenderer;

		private void Awake()
		{
			_meshRenderer = gameObject.GetComponent<MeshRenderer>();
		}

		public void InitializeTile()
		{
			switch (state)
			{
					case TileState.Free:
						_meshRenderer.material = _freeTileColor;
						break;
					case TileState.Occupied:
						_meshRenderer.material = _ocupiedTileColor;
						break;
					case TileState.Path:
						_meshRenderer.material = _pathTileColor;
						break;
			}

			switch (edge)
			{
				case Edge.Up:
					arrow.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
					break;
				case Edge.Down:
					arrow.transform.rotation = Quaternion.Euler(Vector3.zero);
					break;
				case Edge.Left:
					arrow.transform.rotation = Quaternion.Euler(Vector3.forward * 270);
					break;
				case Edge.Right:
					arrow.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
					break;
					
			}
		}
	}

}