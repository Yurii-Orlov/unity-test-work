using System;
using OrCor.DataInstallers;
using OrCor.Pool;
using Zenject;

namespace OrCor {

    public class SpawnerController : IInitializable, IDisposable
    {
        private readonly PlayerController.Factory _playerControllerFactory;
        private readonly EnemyFacade.Factory _enemyControllerFactory;
        private readonly TestSettings _testSettings;

        public SpawnerController(PlayerController.Factory playerControllerFactory,
                                 EnemyFacade.Factory enemyControllerFactory,
                                 TestSettings testSettings)
        {
            _playerControllerFactory = playerControllerFactory;
            _enemyControllerFactory = enemyControllerFactory;
            _testSettings = testSettings;
        }

        public void Initialize()
        {
            PlayerController player = _playerControllerFactory.Create();

            for (int i = 0; i < _testSettings.EnemiesCreateCount; i++)
            {
                EnemyFacade enemy = _enemyControllerFactory.Create();
            }
        }

        public void Dispose()
        {
            
        }
    }

}