using System;
using TestWork.Managers;
using TestWork.Modules.LoadContent;
using TestWork.UI.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TestWork.UI.LoadingPopup
{
    public class LoadingPopup : IInitializable, IDisposable, IUIPopup
    {

        private const string PREFAB_NAME = "LoadingPopup";

        private readonly UIManager _uiManager;
        private readonly SceneLoaderManager _sceneLoaderManager;
        private LoadingPopupComponent _pageComponent;

        public GameObject SelfPage { get; private set; }

        public LoadingPopup(ContentLoadController contentLoadController,
                            UIManager uiManager,
                            SceneLoaderManager sceneLoaderManager)
        {
            _uiManager = uiManager;
            _uiManager.AddPopup(this);
            _sceneLoaderManager = sceneLoaderManager;
            
            contentLoadController.GetObjectByName<GameObject>(PREFAB_NAME, (obj) =>
            {
                SelfPage = Object.Instantiate(obj as GameObject, _uiManager.CanvasParent.transform, false);
                _pageComponent = SelfPage.GetComponent<LoadingPopupComponent>();

                Hide();
            });
        }

        public void Dispose()
        {
            _uiManager.RemovePopup(this);
            Object.Destroy(SelfPage);
        }

        public void Initialize()
        {
            _sceneLoaderManager.SceneLoading += OnSceneLoadingProgress;
            _sceneLoaderManager.SceneFinishedLoading += OnSceneFinishedLoading;
        }

        public void SetMainPriority()
        {
            SelfPage.transform.SetAsLastSibling();
        }

        public void Show()
        {
            _pageComponent.Info.sliderProgress.value = 0;
            _pageComponent.Info.progressText.text = $"{0} %";
            SelfPage.SetActive(true);
        }

        public void Show(object data)
        {
            Show();
        }

        public void Hide()
        {
            if (SelfPage != null)
            {
                SelfPage.SetActive(false);
            }
        }

        private void OnSceneFinishedLoading()
        {
            Hide();
        }

        private void OnSceneLoadingProgress(float progress)
        {
            _pageComponent.Info.sliderProgress.value = progress;
            var progressText = (progress * 100f).ToString("F1");
            _pageComponent.Info.progressText.text = $"{progressText} %";
        }
    }
}