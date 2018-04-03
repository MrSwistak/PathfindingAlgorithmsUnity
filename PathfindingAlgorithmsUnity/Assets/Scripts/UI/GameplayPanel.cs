using Gameplay;
using Zenject;

namespace UI
{
	public class GameplayPanel : BasePanel
	{
		[Inject] private MapManager _mapManager;
		
		public void ClearPath()
		{
			_mapManager.ClearPath(); 
		}

		public void SaveMap()
		{
			_panelManager.ChangePanel(PanelType.SaveMap);
		}

		public override void BackToMenu()
		{
			_mapManager.ClearMap();
			base.BackToMenu();
		}
	}
}