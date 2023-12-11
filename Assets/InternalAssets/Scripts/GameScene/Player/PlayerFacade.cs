using System;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{

	public class PlayerFacade : MonoBehaviour
	{
		public event Action<float, bool> OnTakeDamage;
		
		private PlayerModel _model;
		
		[Inject]
		public void Construct(PlayerModel player)
		{
			_model = player;
			_model.OnTakeDamage += OnTakeDamage;
		}

		public bool IsDead => _model.IsDead;

		public Vector3 Position => _model.Position;

		public Quaternion Rotation => _model.Rotation;

		public void TakeDamage(float damage)
		{
			_model.TakeDamage(damage);
		}

		private void OnDestroy()
		{
			if (_model != null)
			{
				_model.OnTakeDamage -= OnTakeDamage;
			}
		}

	}

}