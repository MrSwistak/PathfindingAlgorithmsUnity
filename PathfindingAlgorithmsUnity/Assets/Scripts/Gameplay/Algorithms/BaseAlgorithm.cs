using Zenject;
using UnityEngine;

namespace Gameplay
{
	public abstract class BaseAlgorithm : MonoBehaviour {

		[Inject] internal MapManager mapManager;

		public abstract void CalculatePath();

	}

}