using FibDev.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace FibDev.UI
{
    public class BackOverlay : MonoBehaviour
    {
        private CameraMovement _cam; // Cached
        [SerializeField] private Animator notebookAnimator;
        [SerializeField] private Transform notebookTop;
        
        private void Start()
        {
            _cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void Back()
        {
            _cam.MoveTo(_cam.start, 0.5f, () => OverlayManager.Instance.mainMenu.SetActive(true));
            gameObject.SetActive(false);
            
            Debug.Log(notebookTop.rotation.z);

            if (notebookTop.rotation.z != 0)
            {
                notebookAnimator.Play("Close", -1, 0f);
            }
        }
    }
}
