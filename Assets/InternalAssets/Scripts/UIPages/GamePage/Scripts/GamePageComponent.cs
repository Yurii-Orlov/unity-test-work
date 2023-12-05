using UnityEngine.UI;
using UnityEngine;

namespace TestWork.UI.GamePage
{

    public class GamePageComponent : MonoBehaviour
    {
        public PageReferences PageUiRefs => _pageUiRefs;

        [SerializeField] private PageReferences _pageUiRefs;

        [System.Serializable]
        public struct PageReferences
        {
            public Button menuBtn;
        }
    }
}