using UnityEngine;

namespace FibDev.Baseball.Choreography.Ball
{
    public class BallMover : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.enabled = false;
        }
    }
}