using System.Collections.Generic;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Player
{
    public class Motion : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public Vector3 IdlePosition { get; set; }
        [SerializeField] private float idleDetectionRadius = 1f;
        public bool isIdle = true;

        private List<Transform> _destinationQueue = new();
        

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SetQueue(List<Transform> pDestinationQueue)
        {
            _destinationQueue = pDestinationQueue;
            isIdle = false;
        }

        public void SetDestination(Vector3 pPosition)
        {
            if (_agent == null) return;
            if (!_agent.isOnNavMesh) return;
            isIdle = false;
            _agent.SetDestination(pPosition);
        }

        private void RotateBodyToTarget(Transform target)
        {
            var direction = target.position - transform.position;
            var angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

        private void Update()
        {
            if (_destinationQueue.Count > 0 && _agent.remainingDistance < 1)
            {
                if (_destinationQueue.Count == 1)
                {
                    IdlePosition = _destinationQueue[0].position;
                }
                SetDestination(_destinationQueue[0].position);
                _destinationQueue.RemoveAt(0);
            }
            
            if (isIdle)
            {
                var position = GetComponent<Player>().playerStats.position;
                var fieldPositions = PositionManager.Instance.field.positions;
                var target = fieldPositions[position == Position.Pitcher ? Position.Batter : Position.Pitcher];

                RotateBodyToTarget(target);
            }

            var isAtIdlePosition = Vector3.Distance(transform.position, IdlePosition) < idleDetectionRadius;
            if (isAtIdlePosition)
            {
                isIdle = true;
            }
        }
    }
}