using System;
using System.Collections;
using System.Collections.Generic;
using TestWork.Modules.LoadContent.Addressable;
using TestWork.UI.LoadingPopup;
using UniRx;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestWork.Managers
{
    public class SceneLoaderManager : IInitializable
    {
        public event Action<float> SceneLoading;
        public event Action SceneFinishedLoading;

        private readonly UIManager _uIManager;
        private readonly List<string> _loadedBuildInScenes;
        private readonly Dictionary<string, AsyncOperationHandle<SceneLoadData>> _loadedAddressablesScenes;
        private readonly CompositeDisposable _disposables;

        public SceneLoaderManager(UIManager uIManager)
        {
            _uIManager = uIManager;
            _loadedBuildInScenes = new List<string>();
            _loadedAddressablesScenes = new Dictionary<string, AsyncOperationHandle<SceneLoadData>>();
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
        }

        public void ChangeScene(string sceneName, LoadSceneMode loadSceneMode, bool isLoadAsync = true)
        {
            ClearPreviousSceneLoading();
            
            var isSceneInBuildSettings = Application.CanStreamedLevelBeLoaded(sceneName);
            if (isSceneInBuildSettings)
            {
                LoadBuildInScene(sceneName, loadSceneMode, isLoadAsync);
            }
            else
            {
                LoadAddressablesScene(sceneName, loadSceneMode, isLoadAsync);
            }
        }

        private void LoadBuildInScene(string sceneName, LoadSceneMode loadSceneMode, bool isLoadAsync)
        {
            var loadSceneParameters = new LoadSceneParameters(loadSceneMode);
            
            if (isLoadAsync)
            {
                var loadOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
                AsyncSceneLoad(loadOperation);
            }
            else
            {
                SceneManager.LoadScene(sceneName, loadSceneParameters);
                SceneLoadedDelay();
            }
            
            if (!_loadedBuildInScenes.Contains(sceneName)) _loadedBuildInScenes.Add(sceneName);
        }

        private void LoadAddressablesScene(string sceneName, LoadSceneMode loadSceneMode, bool isLoadAsync)
        {
            var loadSceneParameters = new LoadSceneParameters(loadSceneMode);

            AsyncOperationHandle<SceneLoadData> asyncOperation;
            
            if (isLoadAsync)
            {
                asyncOperation = AddressablesSceneLoader.LoadSceneAsync(sceneName, loadSceneParameters, false);
                AsyncSceneLoad(asyncOperation);
            }
            else
            {

                asyncOperation = AddressablesSceneLoader.LoadScene(sceneName, loadSceneParameters, false, SceneLoadedDelay);

                asyncOperation.Completed += (res) =>
                {
                    asyncOperation.Result.Operation.allowSceneActivation = true;
                };
            }
            
            if (!_loadedAddressablesScenes.ContainsKey(sceneName) && asyncOperation.IsValid())
            {
                _loadedAddressablesScenes.Add(sceneName, asyncOperation);
            }
        }

        private void AsyncSceneLoad(AsyncOperation asyncOperation)
        {
            SceneLoadingStarted();

            Observable.FromCoroutine(() => SceneLoadingAnimation(asyncOperation)).Subscribe().AddTo(_disposables);

            asyncOperation.AsObservable().ObserveOnMainThread().DoOnCompleted(() =>
            {
                asyncOperation.allowSceneActivation = true;
                SceneLoadedDelay();
                        
            }).Subscribe().AddTo(_disposables);
        }
        
        private void AsyncSceneLoad(AsyncOperationHandle<SceneLoadData> asyncOperation)
        {
            SceneLoadingStarted();
            
            Observable.FromCoroutine(() => SceneLoadingAnimation(asyncOperation)).Subscribe().AddTo(_disposables);

            asyncOperation.ToObservable().DoOnCompleted(() =>
            {
                asyncOperation.Result.Operation.allowSceneActivation = true;
                SceneLoadedDelay();
                    
            }).Subscribe().AddTo(_disposables);
        }

        private IEnumerator SceneLoadingAnimation(AsyncOperationHandle<SceneLoadData> asyncOperation)
        {
            while (asyncOperation.PercentComplete <= 0.9f)
            {
                SceneLoading?.Invoke(asyncOperation.PercentComplete);
                yield return null;
            }
            
            SceneLoading?.Invoke(asyncOperation.PercentComplete);
        }
        
        private IEnumerator SceneLoadingAnimation(AsyncOperation asyncOperation)
        {
            while (asyncOperation.progress <= 0.9f)
            {
                SceneLoading?.Invoke(asyncOperation.progress);
                yield return null;
            }
            
            SceneLoading?.Invoke(asyncOperation.progress);
        }

        private void SceneLoadingStarted()
        {
            _uIManager.DrawPopup<LoadingPopup>(setMainPriority: true);
        }
        
        private void SceneLoadedDelay()
        {
            Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(p =>
            {
                SceneFinishedLoading?.Invoke();
            }).AddTo(_disposables);
        }

        private void ClearPreviousSceneLoading()
        {
            _disposables.Clear();
        }
        
        public void UnloadScene(string sceneName)
        {
            if (_loadedBuildInScenes.Contains(sceneName))
            {
                SceneManager.UnloadSceneAsync(sceneName).completed += (res) =>
                {
                    _loadedBuildInScenes.Remove(sceneName);
                };
            }

            if (_loadedAddressablesScenes.TryGetValue(sceneName, out var sceneLoadData))
            {
                AddressablesSceneLoader.UnloadSceneAsync(sceneLoadData).Completed += (res) =>
                {
                    _loadedAddressablesScenes.Remove(sceneName);
                };
            }
        }
    }
}