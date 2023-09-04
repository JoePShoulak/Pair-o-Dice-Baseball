using System;
using FibDev.Baseball.Choreography.Ball;
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
        private BallMover ball;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            ball = GameObject.FindWithTag("Ball").GetComponent<BallMover>();
        }

        public void SetDestination(Vector3 pPosition)
        {
            IsIdle = false;
            _agent.SetDestination(pPosition);
        }

        private void Update()
        {
            if (IsIdle)
            {
                transform.LookAt(ball.Transform);
            }

            var isAtIdlePosition = Vector3.Distance(transform.position, IdlePosition) < idleDetectionRadius;
            if (isAtIdlePosition)
            {
                IsIdle = true;
            }
        }
    }
}