using UnityEngine;
using System.Collections.Generic;
using Gameplay;

namespace SaveLoad
{
	[CreateAssetMenu(fileName = "SaveContainer", menuName = "DK/SaveContainer", order = 1)]
	public class SaveContainer : ScriptableObject
	{
		public List<MapData> saves;
	}
}