using System;
using OrCor.Pool;
using OrCor.UI;
using UnityEngine;
using Zenject;

namespace OrCor.SceneBinding
{
    public class GameSceneBindings : MonoInstaller
    {
        [SerializeField] private GameObject _enemyPrefab;

        public override void InstallBindings()
        {
            InitServices();
            InitUiViews();
            InitPool();
        }

        private void InitServices()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<SpawnerController>().AsSingle();
            Container.BindFactory<PlayerController, PlayerController.Factory>();
        }

        private void InitUiViews()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIElement)).To<GamePage>().AsSingle();
        }

        private void InitPool()
        {
            Container.BindFactory<EnemyFacade, EnemyFacade.Factory>()
                     .FromMonoPoolableMemoryPool(b => b
                                                      .WithInitialSize(2)
                                                      .FromSubContainerResolve()
                                                      .ByNewPrefabMethod(_enemyPrefab, InstallEnemy)
                                                      .UnderTransformGroup("Enemies"));
        }

        private static void InstallEnemy(DiContainer subContainer)
        {
            subContainer.Bind<EnemyFacade>().FromNewComponentOnRoot().AsSingle();
            subContainer.Bind<PoolableManager>().AsSingle();
        }
    }
}