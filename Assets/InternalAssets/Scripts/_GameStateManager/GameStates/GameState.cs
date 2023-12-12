using System.Threading.Tasks;
using TestWork.Managers;
using TestWork.ProjectSettings;
using TestWork.UI.GamePage;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestWork.GameStates.States
{
    public class GameState : GameStateEntity
    {
        private readonly GameStateManager _gameStateManager;
        private readonly SceneLoaderManager _sceneLoaderManager;
        private readonly UIManager _uIManager;
        private readonly CompositeDisposable _disposable;

        public GameState(GameStateManager gameStateManager,
                         SceneLoaderManager sceneLoaderManager,
                         UIManager uIManager)
        {
            _gameStateManager = gameStateManager;
            _sceneLoaderManager = sceneLoaderManager;
            _uIManager = uIManager;
            _disposable = new CompositeDisposable();
        }

        public override async void Start()
        {
            await _sceneLoaderManager.UnloadScene(SceneNames.GAME);
            _sceneLoaderManager.ChangeScene(SceneNames.GAME, LoadSceneMode.Additive);
            Debug.Log("Gameplay state started");
        }

        public override void Initialize()
        {
            _sceneLoaderManager.SceneFinishedLoading += SceneFinishLoading;
        }

        public override void Tick()
        {
        }

        public override Task Dispose()
        {
            _sceneLoaderManager.SceneFinishedLoading -= SceneFinishLoading;
            _disposable.Dispose();
            
            return Task.CompletedTask;
        }

        private void SceneFinishLoading()
        {
            _uIManager.HideAllPopups();
            
            var gamePage = _uIManager.GetPage<GamePage>();

            if (gamePage != null)
            {
                gamePage.IsInited.ObserveEveryValueChanged(x => x.Value).Subscribe(
                    isInited =>
                    {
                        if (isInited)
                        {
                            _uIManager.SetPage<GamePage>(true);
                        }
                    }).AddTo(_disposable);
            }
        }

        public class Factory : PlaceholderFactory<GameState>
        {
        }
    }
}