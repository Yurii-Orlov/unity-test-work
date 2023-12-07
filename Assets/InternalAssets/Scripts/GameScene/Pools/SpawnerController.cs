using System;
using Zenject;

namespace TestWork.Game.Pool
{

    public class SpawnerController : IInitializable, IDisposable
    {
        private readonly EnemyFacade.Factory _enemyControllerFactory;

        public SpawnerController(EnemyFacade.Factory enemyControllerFactory)
        {
            _enemyControllerFactory = enemyControllerFactory;
        }

        public void Initialize()
        {
            // for (int i = 0; i < _testSettings.EnemiesCreateCount; i++)
            // {
            //     EnemyFacade enemy = _enemyControllerFactory.Create();
            // }
        }

        public void Dispose()
        {
            
        }
    }

}