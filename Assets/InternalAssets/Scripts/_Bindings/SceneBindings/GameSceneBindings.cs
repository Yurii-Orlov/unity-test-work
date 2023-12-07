using System;
using TestWork.Game.Pool;
using TestWork.Managers;
using TestWork.UI.GamePage;
using TestWork.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace TestWork.Bindings.SceneBinding
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