using System.Threading.Tasks;
using TestWork.Managers;
using TestWork.ProjectSettings;
using TestWork.UI.MenuPage;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestWork.GameStates.States
{
    public class MenuState : GameStateEntity
    {
        private readonly GameStateManager _gameStateManager;
        private readonly SceneLoaderManager _sceneLoaderManager;
        private readonly UIManager _uIManager;
        private readonly CompositeDisposable _disposable;

        public MenuState(GameStateManager gameStateManager,
                         SceneLoaderManager sceneLoaderManager,
                         UIManager uIManager)
        {
            _gameStateManager = gameStateManager;
            _sceneLoaderManager = sceneLoaderManager;
            _uIManager = uIManager;
            _disposable = new CompositeDisposable();
        }

        public override void Initialize()
        {
            _sceneLoaderManager.SceneFinishedLoading += OnSceneFinishLoading;
        }

        public override async void Start()
        {
            await _sceneLoaderManager.UnloadScene(SceneNames.GAME);
            _sceneLoaderManager.ChangeScene(SceneNames.MENU, LoadSceneMode.Additive);
            Debug.Log("Menu state started");
        }

        public override async Task Dispose()
        {
            await _sceneLoaderManager.UnloadScene(SceneNames.MENU);
            _sceneLoaderManager.SceneFinishedLoading -= OnSceneFinishLoading;
            _disposable.Dispose();
        }

        private void OnSceneFinishLoading()
        {
            _uIManager.HideAllPopups();
            
            var menuPage = _uIManager.GetPage<MenuPage>();

            if (menuPage != null)
            {
                menuPage.IsInited.ObserveEveryValueChanged(x => x.Value).Subscribe(
                    isInited =>
                    {
                        if (isInited)
                        {
                            _uIManager.SetPage<MenuPage>(true);
                        }
                    }).AddTo(_disposable);
            }
        }

        public class Factory : PlaceholderFactory<MenuState>
        {
        }
    }
}