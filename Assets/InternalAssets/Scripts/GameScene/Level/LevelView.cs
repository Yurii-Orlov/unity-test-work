using UnityEngine;

namespace TestWork.Game.Level
{

	public class LevelView : MonoBehaviour
	{

		[SerializeField] private MeshRenderer _levelFloorMeshRend;

		public MeshRenderer LevelFloorMeshRend => _levelFloorMeshRend;

	}

}

