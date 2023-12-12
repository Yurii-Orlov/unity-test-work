using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace TestWork.Modules.Input
{
    public class FloatingJoystickController : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [InputControl(layout = "Vector2")]
        [SerializeField] 
        private string _controlPath;

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _knob;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Start()
        {
            _background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.position = eventData.position;
            _background.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _background.gameObject.SetActive(false);
            _knob.anchoredPosition = Vector2.zero;

            SendValueToControl(Vector2.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var backgroundPosition = _background.position;
            var position = ClampToCircle(eventData.position, backgroundPosition, _background.sizeDelta.x * 0.5f);
            var delta = position - (Vector2)backgroundPosition;
            _knob.anchoredPosition = delta;

            SendValueToControl(delta.normalized);
        }

        private Vector2 ClampToCircle(Vector2 position, Vector2 center, float radius)
        {
            var offset = position - center;
            var magnitude = offset.magnitude;
            if (magnitude > radius)
            {
                offset = offset.normalized * radius;
                position = center + offset;
            }

            return position;
        }
    }
}


