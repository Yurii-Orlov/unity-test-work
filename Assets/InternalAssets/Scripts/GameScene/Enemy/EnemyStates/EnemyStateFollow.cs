using TestWork.Game.Player;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies.EnemyStates
{

	public class EnemyStateFollow : IEnemyState
	{

		private EnemyView _view;
		private EnemyModel _enemyModel;
		private PlayerFacade _playerFacade;

		[Inject]
		public void Construct(EnemyView view, EnemyModel enemyModel, PlayerFacade playerFacade)
		{
			_view = view;
			_enemyModel = enemyModel;
			_playerFacade = playerFacade;
		}

		public void EnterState()
		{
			Debug.Log("EnemyStateFollow EnterState() =>");
			Debug.Log($"isAsctive => activeSelf = {_view.gameObject.activeSelf} :: activeInHierarchy = {_view.gameObject.activeInHierarchy}");
			_view.ActivateNavMeshAgent();
			_view.SetNavMeshAgentSpeed(_enemyModel.CurrentSpeed);
		}

		public void ExitState()
		{
			_view.StopNavMeshAgent();
		}

		public void Update()
		{
			_view.SetNavmeshDestinationPosition(_playerFacade.Position);
		}

		public void FixedUpdate()
		{
			
		}

	}

}