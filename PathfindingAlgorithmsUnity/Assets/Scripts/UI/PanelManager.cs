using System.Collections.Generic;

namespace UI
{
	public class PanelManager
	{
		private List<BasePanel> _panels;

		public void InitPanel(BasePanel panel)
		{
			if(_panels == null)
				_panels = new List<BasePanel>();
			
			_panels.Add(panel);
		}

		public void ShowPanel(PanelType type)
		{
			foreach (BasePanel panel in _panels)
			{
				if(panel.type == type) 
					panel.ShowPanel();
				else
					panel.HidePanel();
			}
		}

	}
}