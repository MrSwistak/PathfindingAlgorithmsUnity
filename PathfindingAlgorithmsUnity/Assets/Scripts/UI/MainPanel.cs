using Zenject;

namespace UI
{
	public class MainPanel : BasePanel
	{
		public void GoToSetupPanel()
		{
			_panelManager.ChangePanel(PanelType.SetUpMap);
		}

		public void GoToLoadPanel()
		{
			_panelManager.ChangePanel(PanelType.LoadMap);
		}
		
	}
}