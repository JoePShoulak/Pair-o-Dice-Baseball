using UnityEngine;

namespace FibDev.UI
{
    public class MainMenu : MonoBehaviour
    {
        private CameraMovement cam; // Cached
        [SerializeField] private GameObject teamSelectUI;

        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void Play()
        {
            cam.LerpTo(cam.stadium, 2f);
            gameObject.SetActive(false);
            teamSelectUI.SetActive(true);
        }

        public void Scores()
        {
            cam.LerpTo(cam.notebook, 2f);
            gameObject.SetActive(false);
        }

        public void Dice()
        {
            cam.LerpTo(cam.dice, 2f);
            gameObject.SetActive(false);
        }
    }
}