using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace OrCor
{
    public class AddressablesLoader : IContentLoader
    {

        public void Init()
        {

        }

        public async void GetObjectByNameAsync<T>(string name, System.Action<Object> onComplete) where T : Object
        {
            var assetAsync = Addressables.LoadAssetAsync<T>(name);

            await assetAsync.Task;

            if (assetAsync.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete(assetAsync.Result);
            }
        }

        public void GetObjectByName<T>(string name, System.Action<Object> onComplete) where T : Object
        {
            var assetAsync = Addressables.LoadAssetAsync<T>(name);
            assetAsync.WaitForCompletion();

            if (assetAsync.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete(assetAsync.Result);
            }
        }
    }
}