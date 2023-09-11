using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Player;
using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Player
{
    public class Motion : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public Vector3 IdlePosition { get; set; }
        [SerializeField] private float idleDetectionRadius = 1f;
        [SerializeField] private Transform head;
        public bool IsIdle { get; private set; } = true;
        private BallMover ball;

        public bool HasBall
        {
            get
            {
                const float ballDetectionRadius = 3f;
                return Vector3.Distance(transform.position, ball.Transform.position) <= ballDetectionRadius;
            }
        }

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

        private void RotateBodyToTarget(Transform target)
        {
            var direction = target.position - transform.position;
            var angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

        private void RotateHeadToBall()
        {
            var direction = ball.transform.position - head.position;
            var angleX = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
            var angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            head.rotation = Quaternion.Euler(-angleX, angleY, 0);
        }


        private void Update()
        {
            if (IsIdle)
            {
                var position = GetComponent<Player>().playerStats.position;
                var fieldPositions = PositionManager.Instance.field.positions;
                var target =  fieldPositions[position == Position.Pitcher ? Position.Batter : Position.Pitcher];
                
                RotateBodyToTarget(target);
                // RotateHeadToBall(); TODO: Fix this
            }

            var isAtIdlePosition = Vector3.Distance(transform.position, IdlePosition) < idleDetectionRadius;
            if (isAtIdlePosition)
            {
                IsIdle = true;
            }
        }
    }
}