using System;
using UnityEngine;
using Zenject;

namespace TestWork.Game.Enemies
{

	public class EnemyFacade : MonoBehaviour, IPoolable<float, IMemoryPool>, IDisposable
	{

		private EnemyModel _enemyModel;
		private EnemyView _view;
		private EnemyRegistry _registry;
		private IMemoryPool _pool;

		[Inject]
		public void Construct(EnemyView view,
		                      EnemyRegistry registry,
		                      EnemyModel enemyModel)
		{
			_view = view;
			_registry = registry;
			_enemyModel = enemyModel;
		}

		public Vector3 Position
		{
			get => _view.Position;
			set => _view.Position = value;
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
			_registry.AddEnemy(this);
		}

		public class Factory : PlaceholderFactory<float, EnemyFacade>
		{

		}

	}

}