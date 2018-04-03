using Gameplay;
using SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public class SaveMapPanel : BasePanel
	{
		[SerializeField] private SaveContainer _container;
		[SerializeField] private InputField _saveName;
		[Inject] private MapManager _mapManager;

		public void SaveData()
		{
			var data = _mapManager.data;
			data.saveName = _saveName.text;
			
			_container.saves.Add(data);
			_mapManager.ClearMap();
			_panelManager.ChangePanel(PanelType.Main);
		}

	}
}