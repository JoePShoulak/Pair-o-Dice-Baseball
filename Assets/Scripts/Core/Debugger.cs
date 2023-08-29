using UnityEngine;

using FibDev.UI;

namespace FibDev.Core
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private CameraMovement cam;
        private OverlayManager _overlay; // Cached

        private void Start()
        {
            _overlay = OverlayManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cam.LerpTo(cam.start, 2f);
                _overlay.mainMenu.SetActive(true);
                _overlay.teamSelect.SetActive(false);
            }
        }
    }
}