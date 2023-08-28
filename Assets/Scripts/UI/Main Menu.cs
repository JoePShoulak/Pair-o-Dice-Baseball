using UnityEngine;

namespace FibDev.UI
{
    public class MainMenu : MonoBehaviour
    {
        private CameraMovement cam; // Cached
        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void Play()
        {
            cam.LerpTo(cam.stadium, 2f);
            gameObject.SetActive(false);
        }
    }
}
