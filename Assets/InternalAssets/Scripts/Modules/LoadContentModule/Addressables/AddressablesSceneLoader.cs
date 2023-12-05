using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TestWork.Modules.LoadContent.Addressable
{
    public class SceneLoadData
    {
        public AsyncOperation Operation;
        public Scene Scene;
    }

    public static class AddressablesSceneLoader
    {
        public static AsyncOperationHandle<SceneLoadData> LoadSceneAsync(object key, 
                                                                         LoadSceneParameters parameters, 
                                                                         bool allowSceneActivation = true, 
                                                                         int priority = 0)
        {
            var location = GetLocation(key);
            var dependencies = Addressables.DownloadDependenciesAsync(location.Dependencies);

            var operation = new SceneLoadOperation(Addressables.ResourceManager);
            operation.Init(location, parameters, allowSceneActivation, priority, dependencies);

            return Addressables.ResourceManager.StartOperation(operation, dependencies);
        }

        public static AsyncOperationHandle<SceneLoadData> LoadScene(object key, 
                                                                    LoadSceneParameters parameters, 
                                                                    bool allowSceneActivation = true, 
                                                                    Action onComplete = null, 
                                                                    int priority = 0)
        {
            var location = GetLocation(key);
            var dependencies = Addressables.DownloadDependenciesAsync(location.Dependencies);

            var operation = new SceneLoadOperation(Addressables.ResourceManager);
            operation.Init(location, parameters, allowSceneActivation, priority, dependencies);

            var asyncOperation = Addressables.ResourceManager.StartOperation(operation, dependencies);
            asyncOperation.Completed +=  (res) =>
            {
                onComplete?.Invoke();
            };
            
            return asyncOperation;
        }

        public static AsyncOperationHandle UnloadSceneAsync(AsyncOperationHandle<SceneLoadData> sceneLoadHandle)
        {
            var unloadOp = new UnloadSceneOperation();
            unloadOp.Init(sceneLoadHandle);

            return Addressables.ResourceManager.StartOperation(unloadOp, sceneLoadHandle);
        }

        private static IResourceLocation GetLocation(object key)
        {
            foreach (var locator in Addressables.ResourceLocators)
            {
                var success = locator.Locate(key, typeof(SceneInstance), out var locations);

                if (success) return locations[0];
            }

            return null;
        }
    }
}