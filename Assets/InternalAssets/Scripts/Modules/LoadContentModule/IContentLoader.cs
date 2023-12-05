using System;
using Object = UnityEngine.Object;

namespace TestWork.Modules.LoadContent
{
    public interface IContentLoader
    {
        void Init();
        void GetObjectByNameAsync<T>(string name, Action<Object> onComplete) where T : Object;
        void GetObjectByName<T>(string name, Action<Object> onComplete) where T : Object;
    }

}