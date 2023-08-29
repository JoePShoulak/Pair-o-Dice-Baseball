using UnityEngine;

namespace FibDev
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private CameraMovement cam;
        [SerializeField] private GameObject MainMenuUI;
        [SerializeField] private GameObject teamSelectUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cam.LerpTo(cam.start, 2f);
                MainMenuUI.SetActive(true);
                teamSelectUI.SetActive(false);
            }
        }
    }
}