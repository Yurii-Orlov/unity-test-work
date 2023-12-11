using System;
using TestWork.GameStates;
using TestWork.GameStates.States;
using TestWork.Helpers;
using TestWork.Managers;
using TestWork.Modules.LoadContent;
using TestWork.UI.Interfaces;
using TestWork.UI.LoadingPopup;
using Zenject;

namespace TestWork.Bindings.SceneBinding
{
    public class MainProjectBinding : MonoInstaller
    {
        public override void InstallBindings()
        {
            InitServices();
            InitUiViews();
            InstallGameStateManager();
            InstallGameObjects();
        }

        private void InitServices()
        {
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ContentLoadController>().AsSingle();
        }

        private void InitUiViews()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IUIPopup)).To<LoadingPopup>().AsSingle();
        }

        private void InstallGameStateManager()
        {
            Container.Bind<GameStateFactory>().AsSingle();

            Container.BindFactory<GameState, GameState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<MenuState, MenuState.Factory>().WhenInjectedInto<GameStateFactory>();
            Container.BindFactory<GameRestartState, GameRestartState.Factory>().WhenInjectedInto<GameStateFactory>();
        }

        private void InstallGameObjects()
        {
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}