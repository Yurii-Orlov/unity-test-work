using UnityEngine;

namespace TestWork.UI.Interfaces
{
    public interface IUIPopup
    {
        GameObject SelfPage { get; }

        void Show();
        void Show(object data);
        void Hide();
        void Dispose();
        void SetMainPriority();
    }
}