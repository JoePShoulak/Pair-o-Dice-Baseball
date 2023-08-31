using UnityEngine;

using FibDev.Core;

namespace FibDev.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private CameraMovement cam; // Cached

        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void Play()
        {
            gameObject.SetActive(false);
            cam.LerpTo(cam.stadium, 2f, () => OverlayManager.Instance.teamSelect.SetActive(true));
        }

        public void Scores()
        {
            gameObject.SetActive(false);
            cam.LerpTo(cam.notebook, 2f);
        }
    }
}