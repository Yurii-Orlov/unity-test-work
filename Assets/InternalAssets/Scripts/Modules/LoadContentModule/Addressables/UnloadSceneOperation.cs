using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace OrCor
{
    public sealed class UnloadSceneOperation : AsyncOperationBase<SceneLoadData>
    {

        private SceneLoadData _instance;
        private AsyncOperationHandle<SceneLoadData> _sceneLoadHandle;

        public void Init(AsyncOperationHandle<SceneLoadData> sceneLoadHandle)
        {
            _sceneLoadHandle = sceneLoadHandle;
            _instance = _sceneLoadHandle.Result;
        }

        protected override void Execute()
        {
            if (_sceneLoadHandle.IsValid() && _instance.Scene.isLoaded)
            {
                var unloadOp = SceneManager.UnloadSceneAsync(_instance.Scene);

                if (unloadOp == null)
                {
                    UnloadSceneCompleted(null);
                }
                else
                {
                    unloadOp.completed += UnloadSceneCompleted;
                }
            }
            else
            {
                UnloadSceneCompleted(null);
            }
        }

        private void UnloadSceneCompleted(AsyncOperation obj)
        {
            if (_sceneLoadHandle.IsValid())
            {
                Addressables.Release(_sceneLoadHandle);
            }

            Complete(_instance, true, "");
        }
    }
}