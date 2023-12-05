using System;
using TestWork.GameStates;
using TestWork.Managers;
using TestWork.Modules.LoadContent;
using TestWork.ProjectSettings;
using TestWork.UI.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TestWork.UI.GamePage
{

    public class GamePage : IInitializable, IDisposable, IUIElement
    {
        private const string PREFAB_NAME = "GamePage";
        private readonly ContentLoadController _contentLoadController;
        private readonly GameStateManager _gameStateManager;
        private readonly UIManager _uiManager;

        private GamePageComponent PageComponent { get; set; }
        public GameObject SelfPage { get; set; }
        public BoolReactiveProperty IsInited { get; }

        public GamePage(
            UIManager uiManager,
            ContentLoadController contentLoadController,
            GameStateManager gameStateManager)
        {
            _uiManager = uiManager;
            _contentLoadController = contentLoadController;
            _gameStateManager = gameStateManager;

            _uiManager.AddPage(this);
            IsInited = new BoolReactiveProperty(false);
        }

        public void Dispose()
        {
            _uiManager.RemovePage(this);
            Object.Destroy(SelfPage);
        }

        public void Initialize()
        {
            _contentLoadController.GetObjectByNameAsync<GameObject>(
                PREFAB_NAME, obj =>
                {
                    SelfPage = Object.Instantiate(obj as GameObject, _uiManager.CanvasParent.transform, false);

                    if (SelfPage == null)
                    {
                        Debug.LogError($"Content loader doesnt have {PREFAB_NAME}");
                        return;
                    }

                    PageComponent = SelfPage.GetComponent<GamePageComponent>();
                    
                    PageComponent.PageUiRefs.menuBtn.onClick.AddListener(MenuBtnClick);
                    
                    Hide();

                    IsInited.Value = true;
                });
        }
        
        public void Hide()
        {
            if (SelfPage != null)
            {
                SelfPage.SetActive(false);
            }
        }

        public void Show()
        {
            SelfPage.SetActive(true);
        }

        private void MenuBtnClick()
        {
            _gameStateManager.ChangeState(Enumerators.GameStateTypes.MENU);
        }
    }
}