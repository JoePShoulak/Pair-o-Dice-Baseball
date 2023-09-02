using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Player
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public Vector3 IdlePosition { get; set; }
        [SerializeField] private float idleDetectionRadius = 1f;
        public bool IsIdle { get; private set; } = true;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Vector3 pPosition)
        {
            IsIdle = false;
            _agent.SetDestination(pPosition);
        }

        private void Update()
        {
            if (IsIdle) return;

            var isAtIdlePosition = Vector3.Distance(transform.position, IdlePosition) < idleDetectionRadius;
            if (isAtIdlePosition)
            {
                IsIdle = true;
            }
        }
    }
}