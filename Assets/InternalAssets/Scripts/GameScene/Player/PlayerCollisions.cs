using TestWork.Game.Effects;
using UnityEngine;
using Zenject;
using EnemyFacade = TestWork.Game.Enemies.EnemyFacade;

namespace TestWork.Game.Player
{

	public class PlayerCollisions : MonoBehaviour
	{

		private PlayerFacade _playerFacade;
		private Explosion.Factory _explosionFactory;
		
		[Inject]
		private void Construct(PlayerFacade playerFacade, Explosion.Factory explosionFactory)
		{
			_playerFacade = playerFacade;
			_explosionFactory = explosionFactory;
		}
		
		private void OnTriggerEnter(Collider other)
		{
			var enemyFacade = other.transform.GetComponent<EnemyFacade>();

			if (enemyFacade != null)
			{
				_playerFacade.TakeDamage(enemyFacade.GetEnemyDamageValue());

				if (_playerFacade.IsDead)
				{
					var explosion = _explosionFactory.Create();
					explosion.transform.position = _playerFacade.Position;
					_playerFacade.DeactivatePlayer();
				}
			}
		}

	}

}