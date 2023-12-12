using UnityEngine;
using Zenject;

namespace TestWork.Game
{

	public class GamePlayTimer : ITickable
	{
		private float _gameTimer;
		private bool _isTimerStarted;
		
		public float GameTimer => _gameTimer;

		public void Tick()
		{
			if (_isTimerStarted)
			{
				_gameTimer += Time.deltaTime;
			}
		}

		public void StopTimer()
		{
			_isTimerStarted = false;
		}

		public void StartTimer()
		{
			_gameTimer = 0;
			_isTimerStarted = true;
		}

	}

}