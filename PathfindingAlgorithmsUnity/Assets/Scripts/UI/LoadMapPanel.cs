using Gameplay;
using SaveLoad;
using UnityEngine;

namespace UI
{
	public class LoadMapPanel : BasePanel
	{
		[SerializeField] private SaveContainer _saveContainer;
		[SerializeField] private Transform _buttonsParent;
		[SerializeField] private GameObject _loadButtonPrefab;
		
		public override void ShowPanel()
		{
			foreach (MapData data in _saveContainer.saves)
			{
				var obj = Instantiate(_loadButtonPrefab, _buttonsParent);
				var buttonData = obj.GetComponent<LoadButton>();
				buttonData.LoadButtonInit(data);
			}
			
			base.ShowPanel();
		}

		public override void HidePanel()
		{
			foreach (Transform child in _buttonsParent.transform)
			{
				Destroy(child.gameObject);	
			}
			
			base.HidePanel();
		}
	}
}