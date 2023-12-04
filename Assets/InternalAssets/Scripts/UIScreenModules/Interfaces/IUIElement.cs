using UniRx;
using UnityEngine;

namespace OrCor
{
    public interface IUIElement
    {
        GameObject SelfPage { get; set; }
        BoolReactiveProperty IsInited { get; }

        void Show();
        void Hide();
        void Dispose();
    }
}