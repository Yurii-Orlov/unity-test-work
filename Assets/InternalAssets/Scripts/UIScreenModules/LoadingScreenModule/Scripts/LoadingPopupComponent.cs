using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace OrCor
{

    public class LoadingPopupComponent : MonoBehaviour
    {
        public PanelInfo Info => _info;

        [SerializeField] private PanelInfo _info;

        [System.Serializable]
        public struct PanelInfo
        {
            public Slider sliderProgress;
            public TextMeshProUGUI progressText;

        }
    }
}