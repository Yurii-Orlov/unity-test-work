using UnityEngine;
using Zenject;

namespace TestWork.Game.Effects
{

	public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
	{
		[SerializeField] private float _lifeTime;
		[SerializeField] private ParticleSystem _particleSystem;

		private float _startTime;
		private IMemoryPool _pool;

		public void Update()
		{
			if (Time.realtimeSinceStartup - _startTime > _lifeTime)
			{
				_pool.Despawn(this);
			}
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned(IMemoryPool pool)
		{
			_particleSystem.Clear();
			_particleSystem.Play();

			_startTime = Time.realtimeSinceStartup;
			_pool = pool;
		}

		public class Factory : PlaceholderFactory<Explosion>
		{
		}
	}

}