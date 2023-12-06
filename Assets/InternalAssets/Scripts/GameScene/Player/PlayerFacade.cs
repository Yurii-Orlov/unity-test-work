﻿using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{

	public class PlayerFacade : MonoBehaviour
	{
		private PlayerModel _model;
		
		[Inject]
		public void Construct(PlayerModel player)
		{
			_model = player;
		}

		public bool IsDead => _model.IsDead;

		public Vector3 Position => _model.Position;

		public Quaternion Rotation => _model.Rotation;

		// public void TakeDamage(Vector3 moveDirection)
		// {
		// 	_hitHandler.TakeDamage(moveDirection);
		// }
	}

}