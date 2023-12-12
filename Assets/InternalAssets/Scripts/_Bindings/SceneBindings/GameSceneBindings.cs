using System;
using TestWork.Game.Effects;
using TestWork.Game.Enemies;
using TestWork.Managers;
using TestWork.UI.GamePage;
using TestWork.UI.Interfaces;
using TestWork.UI.LoadingPopup;
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
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIPopup)).To<GameRestartPopup>().AsSingle();
        }

        private void InitPool()
        {
            Container.BindFactory<float, float, EnemyFacade, EnemyFacade.Factory>()
                     .FromPoolableMemoryPool<float, float, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                         .WithInitialSize(8)
                         .FromSubContainerResolve()
                         .ByNewPrefabInstaller<EnemyInstaller>(_settings.enemyFacadePrefab)
                         .UnderTransformGroup("Enemies"));
            
            Container.BindFactory<Explosion, Explosion.Factory>()
                     .FromPoolableMemoryPool<Explosion, ExplosionPool>(poolBinder => poolBinder
                                                                           .WithInitialSize(2)
                                                                           .FromComponentInNewPrefab(_settings.explosionPrefab)
                                                                           .UnderTransformGroup("Explosions"));
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject enemyFacadePrefab;
            public GameObject explosionPrefab;
        }

        private class EnemyFacadePool : MonoPoolableMemoryPool<float, float, IMemoryPool, EnemyFacade>
        {
        }
        
        private class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, Explosion>
        {
        }
    }
}