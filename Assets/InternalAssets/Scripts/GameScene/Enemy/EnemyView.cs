using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyView : MonoBehaviour
	{

		[SerializeField] private NavMeshAgent _navMeshAgent;
		
		[Inject]
		public EnemyFacade Facade
		{
			get; set;
		}
		
		public Vector3 Position
		{
			get => transform.position;
			set => transform.position = new Vector3(value.x, transform.position.y, value.z);
		}

		public void StopNavMeshAgent()
		{
			_navMeshAgent.isStopped = true;
		}
		
		public void ActivateNavMeshAgent()
		{
			_navMeshAgent.isStopped = false;
		}

		public void SetNavMeshAgentSpeed(float speed)
		{
			_navMeshAgent.speed = speed;
		}

		public void SetNavmeshAgentWrapPosition(Vector3 position)
		{
			_navMeshAgent.Warp(position);
		}

		public void SetNavmeshDestinationPosition(Vector3 position)
		{
			_navMeshAgent.destination = position;
		}
	}

}