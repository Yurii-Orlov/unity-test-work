using System.Collections.Generic;
using ModestTree;
using Zenject;
using Zenject.SpaceFighter;

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

	public class EnemyStateManager : ITickable, IFixedTickable, IInitializable
	{

		private IEnemyState _currentStateHandler;
		private EnemyStates _currentState = EnemyStates.None;
		private EnemyView _view;

		private List<IEnemyState> _states;

		[Inject]
		public void Construct(EnemyView view,
		                      EnemyStateIdle idle,
		                      EnemyStateFollow follow)
		{
			_view = view;
			_states = new List<IEnemyState> {idle, follow};
		}

		public EnemyStates CurrentState
		{
			get { return _currentState; }
		}

		public void Initialize()
		{
			Assert.IsEqual(_currentState, EnemyStates.None);
			Assert.IsNull(_currentStateHandler);

			ChangeState(EnemyStates.Follow);
		}

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