using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public class LoadButton : MonoBehaviour, IPointerClickHandler
	{
		[Inject] private MapManager _mapManager;
		[Inject] private PanelManager _panelManager;

		[SerializeField] private Text _buttonText;

		private MapData _mapData;
		
		public void LoadButtonInit(MapData mapData)
		{
			this._mapData = mapData;
			_buttonText.text = _mapData.saveName;
		}
	
		public void OnPointerClick(PointerEventData eventData)
		{
			_mapManager.GenerateMap(_mapData);
			_panelManager.ChangePanel(PanelType.Gameplay);
		}
	}

}