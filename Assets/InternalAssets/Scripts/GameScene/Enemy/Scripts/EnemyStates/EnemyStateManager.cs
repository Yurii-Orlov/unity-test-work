using System.Collections.Generic;
using Zenject;

namespace TestWork.Game.Enemies.EnemyStates
{

	public interface IEnemyState
	{
		void EnterState();
		void ExitState();
		void Update();
		void FixedUpdate();
	}

	public enum EnemyStates
	{
		Idle,
		Follow,
		None
	}

	public class EnemyStateManager : ITickable, IFixedTickable
	{

		private IEnemyState _currentStateHandler;
		private EnemyStates _currentState = EnemyStates.None;

		private List<IEnemyState> _states;

		[Inject]
		public void Construct(EnemyStateIdle idle,
		                      EnemyStateFollow follow)
		{
			_states = new List<IEnemyState> {idle, follow};
		}

		public EnemyStates CurrentState => _currentState;

		public void ChangeState(EnemyStates state)
		{
			if (_currentState == state)
			{
				return;
			}

			_currentState = state;

			if (_currentStateHandler != null)
			{
				_currentStateHandler.ExitState();
				_currentStateHandler = null;
			}

			_currentStateHandler = _states[(int) state];
			_currentStateHandler.EnterState();
		}

		public void Tick()
		{
			_currentStateHandler.Update();
		}

		public void FixedTick()
		{
			_currentStateHandler.FixedUpdate();
		}

	}

}