using OrCor.Manager;
using OrCor.UI;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace OrCor.States
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
            _sceneLoaderManager.OnSceneFinishedLoading += MenuState_OnSceneFinishedLoading;
        }

        public override void Start()
        {
            _sceneLoaderManager.ChangeScene(SceneNamesConstants.SCENE_MENU_NAME, LoadSceneMode.Additive);
        }

        public override void Tick()
        {
        }

        public override void Dispose()
        {
            _sceneLoaderManager.UnloadScene(SceneNamesConstants.SCENE_MENU_NAME);
            _sceneLoaderManager.OnSceneFinishedLoading -= MenuState_OnSceneFinishedLoading;
            _disposable.Dispose();
        }

        private void MenuState_OnSceneFinishedLoading()
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