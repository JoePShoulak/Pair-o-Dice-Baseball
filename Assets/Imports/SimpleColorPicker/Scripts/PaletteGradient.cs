using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Imports.SimpleColorPicker.Scripts
{
    public class PaletteGradient : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public Canvas canvas;
        private ColorJoystick _colorJoystick;

        private void Start()
        {
            _colorJoystick = GetComponentInChildren<ColorJoystick>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var cam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? Camera.main : null;
            var rect = GetComponent<RectTransform>();
            var clickOutsideRect =
                !RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, cam, out _);

            if (clickOutsideRect) return;

            _colorJoystick.OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _colorJoystick.OnDrag(eventData);
        }
    }
}