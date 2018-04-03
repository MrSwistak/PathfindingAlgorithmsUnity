using Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public class SetUpMapPanel : BasePanel
	{
		[Inject] private MapManager _mapManager; 
		
		[SerializeField] private Slider size_x;
		[SerializeField] private Slider size_y;
		[SerializeField] private Slider obstaclesCount;
		[SerializeField] private Slider seed;
		
		[SerializeField] private Text size_xText;
		[SerializeField] private Text size_yText;
		[SerializeField] private Text obstaclesCountText;
		[SerializeField] private Text seedText;

		private MapData _data;

		public void SliderUpdate()
		{
			size_xText.text = size_x.value.ToString();
			size_yText.text = size_y.value.ToString();
			obstaclesCountText.text = obstaclesCount.value.ToString();
			seedText.text = seed.value.ToString();
		}

		public void GoToGameplay()
		{	
			_data.xDim = (int)size_x.value;
			_data.yDim = (int)size_y.value;
			_data.obstaclesCount = (int)obstaclesCount.value;
			_data.seed = (int)seed.value;
			
			_mapManager.GenerateMap(_data);
			
			_panelManager.ChangePanel(PanelType.Gameplay);
		}
	}
}