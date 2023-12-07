using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyView : MonoBehaviour
	{
		[SerializeField] private MeshRenderer _renderer = null;
		
		[Inject]
		public EnemyFacade Facade
		{
			get; set;
		}
		
		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = value;
		}
	}

}