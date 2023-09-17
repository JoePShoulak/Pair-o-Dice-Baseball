using UnityEngine;
using UnityEngine.Serialization;

namespace FibDev.UI
{
    public class OverlayManager : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject teamSelect;
        public GameObject gameOverlay;
        public GameObject backOverlay;

        public static OverlayManager Instance;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;
        }

        private void Start()
        {
            mainMenu.SetActive(true);
            teamSelect.SetActive(false);
            gameOverlay.SetActive(false);
            backOverlay.SetActive(false);
        }
    }
}