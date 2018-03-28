using UnityEngine;
using Zenject;

namespace UI
{
	public class BasePanel : MonoBehaviour
	{
		public PanelType type;

		[Inject] private PanelManager _panelManager;

		private void Awake()
		{
			_panelManager.InitPanel(this);	
		}

		public virtual void ShowPanel()
		{
			this.gameObject.SetActive(true);
		}

		public virtual void HidePanel()
		{
			this.gameObject.SetActive(false);
		}
	}

	public enum PanelType
	{
		Main = 0,
		SetUpMap = 1,
		LoadMap = 2,
		Gameplay = 3,
		SaveMap = 4,
	}
}