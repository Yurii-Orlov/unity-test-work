using System;
using TestWork.GameStates;
using TestWork.Managers;
using TestWork.Modules.LoadContent;
using TestWork.ProjectSettings;
using TestWork.UI.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TestWork.UI.LoadingPopup
{
    public class GameRestartPopup : IInitializable, IDisposable, IUIPopup
    {
        private const string PREFAB_NAME = "GameRestartPopup";

        private readonly GameStateManager _gameStateManager;
        private readonly UIManager _uiManager;
        private GameRestartPopupComponent _pageComponent;

        public GameObject SelfPage { get; private set; }

        public GameRestartPopup(ContentLoadController contentLoadController,
                                UIManager uiManager,
                                GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _uiManager = uiManager;
            _uiManager.AddPopup(this);

            contentLoadController.GetObjectByName<GameObject>(PREFAB_NAME, (obj) =>
            {
                SelfPage = Object.Instantiate(obj as GameObject, _uiManager.CanvasParent.transform, false);
                _pageComponent = SelfPage.GetComponent<GameRestartPopupComponent>();
                _pageComponent.Info.restartButton.onClick.AddListener(RestartGameClick);

                Hide();
            });
        }

        private void RestartGameClick()
        {
            _gameStateManager.ChangeState(Enumerators.GameStateTypes.GAMEPLAY_START);
        }

        public void Dispose()
        {
            if (_pageComponent != null)
            {
                _pageComponent.Info.restartButton.onClick.RemoveListener(RestartGameClick);
            }

            _uiManager.RemovePopup(this);
            Object.Destroy(SelfPage);
        }

        public void Initialize()
        {
        }

        public void SetMainPriority()
        {
            SelfPage.transform.SetAsLastSibling();
        }

        public void Show()
        {
            SelfPage.SetActive(true);
        }

        public void Show(object data)
        {
            if (data is GameRestartPopupData popupData)
            {
                _pageComponent.Info.infoText.text = $"Player was alive: {popupData.gameLiveTimer}";
            }
            
            Show();
        }

        public void Hide()
        {
            if (SelfPage != null)
            {
                SelfPage.SetActive(false);
            }
        }
        
        public class GameRestartPopupData
        {
            public float gameLiveTimer;
        }
    }
}