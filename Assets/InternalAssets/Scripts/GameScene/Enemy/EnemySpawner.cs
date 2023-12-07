using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TestWork.Game.Enemies
{

    public class EnemySpawner : ITickable
    {
        private readonly EnemyFacade.Factory _enemyFactory;
        private readonly Settings _settings;

        private float _desiredNumEnemies;
        private int _enemyCount;
        private float _lastSpawnTime;

        public EnemySpawner(Settings settings,
                            EnemyFacade.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;
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

        void SpawnEnemy()
        {
            var speed = Random.Range(_settings.enemySpeedMin, _settings.enemySpeedMax);

            var enemyFacade = _enemyFactory.Create(speed);
            enemyFacade.Position = GetRandomStartPosition();

            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        private Vector3 GetRandomStartPosition()
        {
            return Vector3.zero;
        }

        [Serializable]
        public class Settings
        {
            public float enemySpeedMin;
            public float enemySpeedMax;
            public float maxNumEnemies;
            public float delayBetweenSpawns = 2f;
        }
    }

}