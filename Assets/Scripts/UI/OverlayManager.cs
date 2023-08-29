using UnityEngine;

namespace FibDev.UI
{
    public class OverlayManager : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject teamSelect;

        public static OverlayManager Instance;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;
        }

        private void Start()
        {
            mainMenu.SetActive(true);
        }
    }
}
