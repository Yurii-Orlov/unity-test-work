using UnityEngine.UI;
using UnityEngine;

namespace TestWork.UI.MenuPage
{
    public class MenuPageComponent : MonoBehaviour
    {
        public PageReferences PageUiRefs => _pageUiRefs;

        [SerializeField] private PageReferences _pageUiRefs;

        [System.Serializable]
        public struct PageReferences
        {
            public Button playBtn;
        }
    }
}