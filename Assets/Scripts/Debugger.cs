using UnityEngine;
using FibDev.Core;
using FibDev.UI;
using UnityEngine.AI;

namespace FibDev
{
    public class Debugger : MonoBehaviour
    {
        [SerializeField] private CameraMovement cam;
        [SerializeField] NavMeshAgent agent;
        private OverlayManager _overlay; // Cached

        private void Start()
        {
            _overlay = OverlayManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cam.LerpTo(cam.start, 2f);
                _overlay.mainMenu.SetActive(true);
                _overlay.teamSelect.SetActive(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("moving agent");
                agent.SetDestination(agent.transform.position + new Vector3(100, 0, 0));
            }
        }
    }
}