using System;
using TestWork.Game.Enemies;
using TestWork.Managers;
using TestWork.UI.GamePage;
using TestWork.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace TestWork.Bindings.SceneBinding
{
    public class GameSceneBindings : MonoInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            InitServices();
            InitUiViews();
            InitPool();
        }

        private void InitServices()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.Bind<EnemyRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }

        private void InitUiViews()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIElement)).To<GamePage>().AsSingle();
        }

        private void InitPool()
        {
            Container.BindFactory<float, EnemyFacade, EnemyFacade.Factory>()
                     .FromPoolableMemoryPool<float, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                         .WithInitialSize(8)
                         .FromSubContainerResolve()
                         .ByNewPrefabInstaller<EnemyInstaller>(_settings.enemyFacadePrefab)
                         .UnderTransformGroup("Enemies"));
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject enemyFacadePrefab;
        }

        class EnemyFacadePool : MonoPoolableMemoryPool<float, IMemoryPool, EnemyFacade>
        {
        }
    }
}