using System;
using Object = UnityEngine.Object;

namespace OrCor
{
    public interface IContentLoader
    {
        void Init();
        void GetObjectByNameAsync<T>(string name, Action<Object> onComplete) where T : Object;
        void GetObjectByName<T>(string name, Action<Object> onComplete) where T : Object;
    }

}