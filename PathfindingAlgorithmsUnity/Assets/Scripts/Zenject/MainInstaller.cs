using Gameplay;
using UI;

namespace Zenject
{
	public class MainInstaller : MonoInstaller<MainInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<PanelManager>().AsSingle().NonLazy();
			Container.Bind<MapManager>().AsSingle();
			Container.Bind<ObstacleRandomizer>().AsTransient();
		}
	}
}