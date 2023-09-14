using FibDev.Core;
using UnityEngine;

namespace FibDev.UI
{
    public class ScoresOverlayUI : MonoBehaviour
    {
        private CameraMovement _cam; // Cached
        
        private void Start()
        {
            _cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void Back()
        {
            _cam.LerpTo(_cam.start, 0.5f, () => OverlayManager.Instance.mainMenu.SetActive(true));
            gameObject.SetActive(false);
        }
    }
}
