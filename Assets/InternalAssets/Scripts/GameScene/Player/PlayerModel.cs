using System;
using UnityEngine;

namespace TestWork.Game.Player
{
	public class PlayerModel
	{

		public event Action<float, bool> OnTakeDamage;

		private readonly Rigidbody _rigidBody;
		private float _health = 100.0f;
		
		public PlayerModel(Rigidbody rigidBody)
		{
			_rigidBody = rigidBody;
		}

		public bool IsDead
		{
			get; set;
		}

		public float Health => _health;

		public Quaternion Rotation
		{
			get => _rigidBody.rotation;
			set => _rigidBody.rotation = value;
		}

		public Vector3 Position
		{
			get => _rigidBody.position;
			set => _rigidBody.position = value;
		}

		public Vector3 Velocity
		{
			get => _rigidBody.velocity;
			set => _rigidBody.velocity = value;
		}
		
		public Rigidbody Rigidbody => _rigidBody;

		public void TakeDamage(float healthLoss)
		{
			_health = Mathf.Max(0.0f, _health - healthLoss);

			IsDead = _health == 0;
			
			OnTakeDamage?.Invoke(_health, IsDead);
		}
	}
}
