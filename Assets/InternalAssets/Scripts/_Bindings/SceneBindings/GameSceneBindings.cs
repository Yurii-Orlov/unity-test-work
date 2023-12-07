using System;
using TestWork.Bindings.ScriptableInstallers;
using TestWork.Game.Enemies;
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
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            InitServices();
            InitUiViews();
            InitPool();
        }

        private void InitServices()
        {
            Container.Bind<EnemyRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(IDisposable)).To<SpawnerController>().AsSingle();
        }

        private void InitUiViews()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIElement)).To<GamePage>().AsSingle();
        }

        private void InitPool()
        {
            Container.BindFactory<float, float, EnemyFacade, EnemyFacade.Factory>()
                     .FromPoolableMemoryPool<float, float, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
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

        class EnemyFacadePool : MonoPoolableMemoryPool<float, float, IMemoryPool, EnemyFacade>
        {
        }
    }
}