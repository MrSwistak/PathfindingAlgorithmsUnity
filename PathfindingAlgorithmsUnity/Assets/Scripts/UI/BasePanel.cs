using UnityEngine;
using Zenject;

namespace UI
{
	public class BasePanel : MonoBehaviour
	{
		public PanelType type;
		[HideInInspector] internal GameObject panel;

		[Inject] internal PanelManager _panelManager;

		private void Start()
		{
			_panelManager.InitPanel(this);
			panel = this.gameObject.transform.GetChild(0).gameObject;
		}

		public virtual void ShowPanel()
		{
			panel.gameObject.SetActive(true);
		}

		public virtual void HidePanel()
		{
			panel.gameObject.SetActive(false);
		}

		public virtual void BackToMenu()
		{
			_panelManager.ChangePanel(PanelType.Main);
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