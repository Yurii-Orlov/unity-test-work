using TestWork.Game.Player;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies.EnemyStates
{

	public class EnemyStateFollow : IEnemyState
	{

		private const float CHANGE_STATE_DISTANCE = 1f;
		
		private EnemyView _view;
		private EnemyModel _enemyModel;
		private PlayerFacade _playerFacade;
		private EnemyStateManager _enemyStateManager;

		[Inject]
		public void Construct(EnemyView view, EnemyModel enemyModel, PlayerFacade playerFacade, EnemyStateManager enemyStateManager)
		{
			_view = view;
			_enemyModel = enemyModel;
			_playerFacade = playerFacade;
			_enemyStateManager = enemyStateManager;
		}

		public void EnterState()
		{
			_view.ActivateNavMeshAgent();
			_view.SetNavMeshAgentSpeed(_enemyModel.CurrentSpeed);
		}

		public void ExitState()
		{
			_view.StopNavMeshAgent();
		}

		public void Update()
		{
			if (_playerFacade.IsDead && _enemyStateManager.CurrentState != EnemyStates.Idle)
			{
				_view.StopNavMeshAgent();
				_enemyStateManager.ChangeState(EnemyStates.Idle);
				return;
			}
			
			if (Vector3.Distance(_view.Position, _playerFacade.Position) < CHANGE_STATE_DISTANCE)
			{
				_enemyStateManager.ChangeState(EnemyStates.Idle);
			}
			_view.SetNavmeshDestinationPosition(_playerFacade.Position);
		}

		public void FixedUpdate()
		{
			
		}

	}

}