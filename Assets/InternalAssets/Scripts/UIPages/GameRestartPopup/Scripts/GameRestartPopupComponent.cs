using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace TestWork.UI.LoadingPopup
{

    public class GameRestartPopupComponent : MonoBehaviour
    {
        public PopupInfo Info => _info;

        [SerializeField] private PopupInfo _info;

        [System.Serializable]
        public struct PopupInfo
        {

            public Button restartButton;
            public TMP_Text infoText;

        }
    }
}