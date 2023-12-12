using System;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{

	public class PlayerMoveHandler : IFixedTickable
	{
		private readonly PlayerModel _model;
		private readonly PlayerInputState _inputState;
		private readonly Settings _settings;

		public PlayerMoveHandler(PlayerModel model, PlayerInputState inputState, Settings settings)
		{
			_model = model;
			_inputState = inputState;
			_settings = settings;
		}

		public void FixedTick()
		{
			if (_model.IsDead)
			{
				return;
			}

			var moveInput = _inputState.CurrentMoveVector;
			var isMoving = moveInput.magnitude > 0.1f;

			if (isMoving)
			{
				_model.Rigidbody.isKinematic = false;

				_model.Velocity = new Vector3(moveInput.x * _settings.moveSpeed * Time.fixedDeltaTime, 
				                              _model.Velocity.y, 
				                              moveInput.y * _settings.moveSpeed * Time.fixedDeltaTime);

				
				_model.Rotation = Quaternion.LookRotation(_model.Velocity);

				return;
			}
			
			_model.Rigidbody.isKinematic = true;
		}
		
		[Serializable]
		public class Settings
		{
			public float moveSpeed;
		}

	}

}