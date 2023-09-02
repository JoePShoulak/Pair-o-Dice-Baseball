using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Imports.SimpleColorPicker.Scripts
{
    public class ColorJoystick : MonoBehaviour, IDragHandler
    {
        public Canvas canvas;
        public RectTransform rectTransform;
        public ColorPicker colorPicker;

        [HideInInspector] public Image center;
        
        private void Start()
        {
            center = GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var cam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? Camera.main : null;
            var clickOutsideRect =
                !RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, cam,
                    out var position);

            if (clickOutsideRect) return;

            var rect = rectTransform.rect;
            position.x = Mathf.Max(position.x, rect.min.x);
            position.y = Mathf.Max(position.y, rect.min.y);
            position.x = Mathf.Min(position.x, rect.max.x);
            position.y = Mathf.Min(position.y, rect.max.y);

            transform.localPosition = position;

            var texture = colorPicker.texture;
            var x = position.x / rect.width * texture.width;
            var y = position.y / rect.height * texture.height;
            var color = Color.HSVToRGB(colorPicker.h.Value, x / texture.width, y / texture.height);

            color.a = colorPicker.a.Value;

            colorPicker.SetColor(color, pPicker: false);
        }
    }
}