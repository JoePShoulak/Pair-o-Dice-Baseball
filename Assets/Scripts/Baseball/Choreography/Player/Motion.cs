using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using FibDev.Audio;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FibDev.Baseball.Choreography.Player
{
    public class Motion : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private AudioSource audioSource;

        public Vector3 IdlePosition { get; set; }
        [SerializeField] private float idleDetectionRadius = 1f;
        public bool isIdle = true;
        private bool lookedAtTarget;

        private List<Transform> _destinationQueue = new();

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            audioSource = gameObject.AddComponent<AudioSource>();
            var footPitch = Random.Range(0.7f, 1.3f);
            AudioManager.SetSourceValues(audioSource, AudioManager.Instance.FindSound("Footstep"));
            audioSource.pitch = footPitch;
        }

        public void SetQueue(List<Transform> pDestinationQueue)
        {
            _destinationQueue = pDestinationQueue;
            IdlePosition = pDestinationQueue.Last().position;
            BeginMoving();
        }

        public void SetDestination(Vector3 pPosition)
        {
            if (_agent == null) return;
            if (!_agent.isOnNavMesh) return;
            BeginMoving();
            lookedAtTarget = false;
            _agent.SetDestination(pPosition);
        }

        private void BeginMoving()
        {
            isIdle = false;
            audioSource.time = Random.Range(0f, audioSource.clip.length);
            audioSource.Play();
        }

        private void StopMoving()
        {
            isIdle = true;
            audioSource.Stop();
        }

        private void RotateBodyToTarget(Transform target)
        {
            var direction = target.position - transform.position;
            var angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.DORotate(new Vector3(0, angleY, 0), 0.25f);
        }

        private bool IsAtHome()
        {
            var fieldPositions = PositionManager.Instance.field.positions;
            return Vector3.Distance(transform.position, fieldPositions[Position.Catcher].position) < 3f;
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

            if (isIdle && !lookedAtTarget)
            {
                var fieldPositions = PositionManager.Instance.field.positions;
                var target = fieldPositions[IsAtHome() ? Position.Pitcher : Position.Catcher];

                RotateBodyToTarget(target);
                lookedAtTarget = true;
            }

            var isAtIdlePosition = Vector3.Distance(transform.position, IdlePosition) < idleDetectionRadius;
            if (isAtIdlePosition)
            {
                StopMoving();
            }
        }
    }
}