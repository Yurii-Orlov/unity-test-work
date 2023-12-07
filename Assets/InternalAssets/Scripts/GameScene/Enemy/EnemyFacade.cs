using System;
using TestWork.Game.Enemies.EnemyStates;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyFacade : MonoBehaviour, IPoolable<float, float, IMemoryPool>, IDisposable
	{

		EnemyView _view;
		EnemyRegistry _registry;
		IMemoryPool _pool;
		EnemyStateManager _stateManager;

		[Inject]
		public void Construct(EnemyView view,
		                      EnemyRegistry registry,
		                      EnemyStateManager stateManager)
		{
			_view = view;
			_registry = registry;
			_stateManager = stateManager;
		}

		public Vector3 Position
		{
			get { return _view.Position; }
			set { _view.Position = value; }
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

		public void OnSpawned(float accuracy, float speed, IMemoryPool pool)
		{
			_pool = pool;

			_registry.AddEnemy(this);
		}

		public class Factory : PlaceholderFactory<float, float, EnemyFacade>
		{

		}

	}

}