using System;
using TestWork.Game.Enemies.EnemyStates;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyFacade : MonoBehaviour, IPoolable<float, IMemoryPool>, IDisposable
	{

		private EnemyStateManager _enemyStateManager;
		private EnemyModel _enemyModel;
		private EnemyView _view;
		private EnemyRegistry _registry;
		private IMemoryPool _pool;

		[Inject]
		public void Construct(EnemyView view,
		                      EnemyRegistry registry,
		                      EnemyModel enemyModel,
		                      EnemyStateManager enemyStateManager)
		{
			_view = view;
			_registry = registry;
			_enemyModel = enemyModel;
			_enemyStateManager = enemyStateManager;
		}

		public void SetSpawnPosition(Vector3 position)
		{
			_view.SetNavmeshAgentWrapPosition(position);
		}

		public void Dispose()
		{
			_pool.Despawn(this);
		}

		public void OnDespawned()
		{
			_registry.RemoveEnemy(this);
			_pool = null;
		}

		public void OnSpawned(float speed, IMemoryPool pool)
		{
			_pool = pool;
			_enemyModel.InitSpeed(speed);
			_enemyStateManager.ChangeState(EnemyStates.EnemyStates.Follow);
			_registry.AddEnemy(this);
		}

		public class Factory : PlaceholderFactory<float, EnemyFacade>
		{

		}

	}

}