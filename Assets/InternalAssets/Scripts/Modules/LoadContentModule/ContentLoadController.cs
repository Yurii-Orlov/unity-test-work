using System;
using UniRx.Async;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace OrCor
{
    public enum ContentLoaderType
    {
        Addressables,
        AssetBundles
    }
    
    
    public class ContentLoadController : IInitializable
    {
        private IContentLoader _contentLoader;
        private readonly ContentLoaderType _currentContentLoaderType;

        public ContentLoadController()
        {
            _currentContentLoaderType = ContentLoaderType.Addressables;
            InitContentLoader();
            _contentLoader.Init();
        }

        public void Initialize()
        {
        }

        private void InitContentLoader()
        {
            switch (_currentContentLoaderType)
            {
                case ContentLoaderType.Addressables:
                    _contentLoader = new AddressablesLoader();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void GetObjectByNameAsync<T>(string objName, Action<Object> onComplete) where T : Object
        {
            _contentLoader.GetObjectByNameAsync<T>(objName, onComplete);
        }
        
        public void GetObjectByName<T>(string objName, Action<Object> onComplete) where T : Object
        {
            _contentLoader.GetObjectByName<T>(objName, onComplete);
        }
        
        public T GetObjectFromResources<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async UniTask<Object> GetObjectFromResourcesAsync(string path)
        {
            var resource = await Resources.LoadAsync<Object>(path);

            return resource;
        }
    }
}