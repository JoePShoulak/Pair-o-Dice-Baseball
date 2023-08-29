using System;
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
            cam.LerpTo(cam.dice, 2f);
            gameObject.SetActive(false);
        }
        
        public void Scores()
        {
            cam.LerpTo(cam.notebook, 2f);
            gameObject.SetActive(false);
        }
    }
}
