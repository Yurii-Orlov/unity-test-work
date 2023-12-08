using Zenject;

namespace TestWork.Game.Enemies.EnemyStates
{

	public class EnemyStateIdle : IEnemyState
	{
		private EnemyView _view;
		private EnemyModel _enemyModel;

		[Inject]
		public void Construct(EnemyView view, EnemyModel enemyModel)
		{
			_view = view;
			_enemyModel = enemyModel;
		}

		public void EnterState()
		{
			_view.StopNavMeshAgent();
		}

		public void ExitState()
		{
			_view.StopNavMeshAgent();
		}

		public void Update()
		{
			
		}

		public void FixedUpdate()
		{
			
		}
		
	}

}