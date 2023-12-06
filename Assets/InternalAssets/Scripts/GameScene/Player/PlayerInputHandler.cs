using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{
	public class PlayerInputHandler : ITickable
	{

		private readonly PlayerInputState _playerInputState;
		private PlayerInputActions.PlayerActions _playerInputActions;
		private readonly bool _isEnabled;

		public PlayerInputHandler(PlayerInputState playerInputState)
		{
			_playerInputState = playerInputState;

			if (!_isEnabled)
			{
				var inputActions = new PlayerInputActions();
				_playerInputActions = inputActions.Player;
				_playerInputActions.Enable();

				_isEnabled = true;
			}
		}

		public void Tick()
		{
			_playerInputState.CurrentMoveVector = _playerInputActions.Move.ReadValue<Vector2>();
		}

	}
}


