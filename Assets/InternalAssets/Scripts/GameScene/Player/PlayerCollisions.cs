using TestWork.Game.Enemies;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Player
{

	public class PlayerCollisions : MonoBehaviour
	{

		private PlayerFacade _playerFacade;
		
		[Inject]
		private void Construct(PlayerFacade playerFacade)
		{
			_playerFacade = playerFacade;
		}
		
		private void OnTriggerEnter(Collider other)
		{
			var enemyFacade = other.transform.GetComponent<EnemyFacade>();

			if (enemyFacade != null)
			{
				_playerFacade.TakeDamage(enemyFacade.GetEnemyDamageValue());
			}
		}

	}

}