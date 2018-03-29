using Zenject;

namespace UI
{
	public class MainPanel : BasePanel
	{
		public void GoToSetupPanel()
		{
			_panelManager.ChangePanel(PanelType.SetUpMap);
		}
		
	}
}