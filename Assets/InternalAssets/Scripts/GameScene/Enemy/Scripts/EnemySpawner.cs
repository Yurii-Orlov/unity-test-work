using System;
using TestWork.Game.Level;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TestWork.Game.Enemies
{

    public class EnemySpawner : ITickable
    {
        private readonly EnemyFacade.Factory _enemyFactory;
        private readonly Settings _settings;
        private readonly LevelView _levelView;
        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;

        public EnemySpawner(Settings settings,
                            EnemyFacade.Factory enemyFactory,
                            LevelView levelView)
        {
            _enemyFactory = enemyFactory;
            _levelView = levelView;
            _settings = settings;


            _desiredNumEnemies = settings.maxNumEnemies;
        }

        public void Tick()
        {
            if (_enemyCount < (int)_desiredNumEnemies && Time.realtimeSinceStartup - _lastSpawnTime > _settings.delayBetweenSpawns)
            {
                SpawnEnemy();
                _enemyCount++;
            }
        }

        private void SpawnEnemy()
        {
            var speed = Random.Range(_settings.enemySpeedMin, _settings.enemySpeedMax);

            var enemyFacade = _enemyFactory.Create(speed, _settings.enemyDamage);
            enemyFacade.SetSpawnPosition(GetRandomStartPosition());
            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        private Vector3 GetRandomStartPosition()
        {
            var meshRenderer = _levelView.LevelFloorMeshRend;
            
            if (meshRenderer != null)
            {
                var bounds = meshRenderer.bounds;

                var x = Random.Range(bounds.min.x, bounds.max.x);
                var z = Random.Range(bounds.min.z, bounds.max.z);
                
                return new Vector3(x, 0.6f, z);
            }

            return Vector3.zero;
        }

        [Serializable]
        public class Settings
        {
            public float enemyDamage;
            public float enemySpeedMin;
            public float enemySpeedMax;
            public float maxNumEnemies;
            public float delayBetweenSpawns = 2f;
        }
    }

}