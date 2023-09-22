using FibDev.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FibDev.UI
{
    public class ClickableSlider : Slider
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            AudioManager.Instance.Play("Click");
        }
    }
}
