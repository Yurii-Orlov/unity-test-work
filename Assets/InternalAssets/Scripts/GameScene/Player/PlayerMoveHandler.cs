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
				var playerRotation = _model.Rotation;
				var playerPosition = _model.Position;
				var move = new Vector3(moveInput.x, 0f, moveInput.y);

				var needRotation = Quaternion.LookRotation(move);
				var finalRotation = Quaternion.Slerp(playerRotation, needRotation, Time.fixedDeltaTime * _settings.rotationSpeed);
				
				var finalPosition = playerPosition + finalRotation * Vector3.forward * (Time.fixedDeltaTime * _settings.moveSpeed * moveInput.magnitude);
				_model.Position = finalPosition;

				_model.Rotation = Quaternion.LookRotation(finalPosition - playerPosition);
				
				return;
			}
			
			_model.Rigidbody.isKinematic = true;
		}
		
		[Serializable]
		public class Settings
		{
			public float moveSpeed;
			public float rotationSpeed;
		}

	}

}