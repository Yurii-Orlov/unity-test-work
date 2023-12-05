using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace TestWork.Modules.LoadContent.Addressable
{
    public sealed class SceneLoadOperation : AsyncOperationBase<SceneLoadData>, IUpdateReceiver
    {
        private bool _allowSceneActivation;
        private AsyncOperationHandle _dependencies;
        private LoadSceneParameters _loadParameters;
        private IResourceLocation _location;
        private int _priority;
        private readonly ResourceManager _resourceManager;
        private SceneLoadData _scene;

        public SceneLoadOperation(ResourceManager manager)
        {
            _resourceManager = manager;
        }

        protected override string DebugName => $"Scene({(_location == null ? "Invalid" : _resourceManager.TransformInternalId(_location))})";

        void IUpdateReceiver.Update(float unscaledDeltaTime)
        {
            if (_scene.Operation.isDone ||
                !_allowSceneActivation && Mathf.Approximately(_scene.Operation.progress, 0.9f))
            {
                Complete(_scene, true, null);
            }
        }

        public void Init(IResourceLocation location, 
                         LoadSceneParameters parameters,
                         bool allowSceneActivation, 
                         int priority,
                         AsyncOperationHandle dependencies)
        {
            _dependencies = dependencies;
            _location = location;
            _loadParameters = parameters;
            _allowSceneActivation = allowSceneActivation;
            _priority = priority;
        }

        protected override void Destroy()
        {
            if (_dependencies.IsValid())
            {
                Addressables.Release(_dependencies);
            }

            base.Destroy();
        }

        public override void GetDependencies(List<AsyncOperationHandle> deps)
        {
            if (_dependencies.IsValid()) deps.Add(_dependencies);
        }

        protected override void Execute()
        {
            var loadingFromBundle = false;

            if (_dependencies.IsValid() && _dependencies.Result is IList dependenciesResult)
            {
                foreach (var entry in dependenciesResult)
                {
                    if (entry is IAssetBundleResource abResource && abResource.GetAssetBundle() != null)
                    {
                        loadingFromBundle = true;
                    }
                }
            }

            _scene = InternalLoadScene(_location, loadingFromBundle, _allowSceneActivation, _priority);
            ((IUpdateReceiver) this).Update(0.0f);
        }

        private SceneLoadData InternalLoadScene(IResourceLocation location, 
                                                bool loadingFromBundle, 
                                                bool allowSceneActivation, 
                                                int priority)
        {
            var internalId = _resourceManager.TransformInternalId(location);
            var asyncOperation = InternalLoad(internalId, loadingFromBundle, _loadParameters);
            asyncOperation.allowSceneActivation = allowSceneActivation;
            asyncOperation.priority = priority;

            return new SceneLoadData {Operation = asyncOperation, Scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1)};
        }

        private AsyncOperation InternalLoad(string path, bool loadingFromBundle, LoadSceneParameters parameters)
        {

#if !UNITY_EDITOR
            return SceneManager.LoadSceneAsync(path, parameters);
#else
            if (loadingFromBundle)
            {
                return SceneManager.LoadSceneAsync(path, parameters);
            }

            if (!path.ToLower().StartsWith("assets/") && !path.ToLower().StartsWith("packages/"))
                path = "Assets/" + path;
            
            if (path.LastIndexOf(".unity", StringComparison.Ordinal) == -1)
                path += ".unity";

            return EditorSceneManager.LoadSceneAsyncInPlayMode(path, parameters);
#endif
        }
    }
}