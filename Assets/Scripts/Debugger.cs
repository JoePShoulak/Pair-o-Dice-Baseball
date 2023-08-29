using UnityEngine;

namespace FibDev
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private GameObject cam;
        [SerializeField] private GameObject MainMenuUI;

        private void Update()
        {
            // FIXME: Debug code
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var move = cam.GetComponent<CameraMovement>();
                move.LerpTo(move.start, 2f);
                MainMenuUI.SetActive(true);
            }
        }
    }
}