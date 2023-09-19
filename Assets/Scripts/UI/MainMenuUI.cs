using UnityEngine;
using FibDev.Core;
using TMPro;

namespace FibDev.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private CameraMovement cam; // Cached
        [SerializeField] private Animator notebookAnimator;
        [SerializeField] private GameObject quitButton;

        private void Start()
        {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
#if UNITY_STANDALONE_WIN
            quitButton.SetActive(true);
#endif
        }

        public void Play()
        {
            gameObject.SetActive(false);
            cam.MoveAroundStadium(cam.stadium, 2f, () => OverlayManager.Instance.teamSelect.SetActive(true));
        }

        public void Scores()
        {
            gameObject.SetActive(false);
            cam.MoveTo(cam.notebook, 0.5f, () => OverlayManager.Instance.backOverlay.SetActive(true));
            notebookAnimator.Play("Open", -1, 0f);
        }

        public void Credits()
        {
            gameObject.SetActive(false);
            cam.MoveTo(cam.whiteboard, 0.5f, () => OverlayManager.Instance.backOverlay.SetActive(true));
        }

        public void Settings()
        {
            gameObject.SetActive(false);
            OverlayManager.Instance.settings.SetActive(true);
        }

        public void Quit()
        {
            Debug.Log("Quit called");
            Application.Quit();
            quitButton.GetComponent<TMP_Text>().text = "quitting";
        }
    }
}