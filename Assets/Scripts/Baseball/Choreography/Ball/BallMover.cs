using System.Collections;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Ball
{
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private FieldPositions field;
        [SerializeField] private Transform origin;
        [SerializeField] private Transform ballDestination;
        [SerializeField] private Transform strikeDestination;
        [SerializeField] private float pollingRate;
        [SerializeField] private Choreographer choreographer;
        private Vector3 _destination;
        
        private const float tanAngle = 2f;
        
        public Transform Transform => transform;

        private readonly AnimationCurve pathCurve = new()
        {
            keys = new[]
            {
                QuickKeyframe(0f, 0f, tanAngle),
                QuickKeyframe(0.5f, 1f),
                QuickKeyframe(1f, 0f, -tanAngle)
            }
        };

        private static Keyframe QuickKeyframe(float pTime, float pValue, float pTanAngle = 0f)
        {
            var keyframe = new Keyframe(pTime, pValue)
            {
                inTangent = pTanAngle,
                outTangent = pTanAngle
            };

            return keyframe;
        }
        
        // public Vector3 Destination => _destination;

        public void PitchBall()
        {
            StartCoroutine(LerpBall(origin.position, ballDestination.position, 2f, 3f, 45f));
        }

        public void PitchStrike()
        {
            StartCoroutine(LerpBall(origin.position, strikeDestination.position, 2f, 3f, 45f));
        }

        public void HitPlayer()
        {
            StartCoroutine(LerpBall(origin.position, field.positions[Position.Batter].position, 2f, 3f, 45f));
        }

        public void LineOut()
        {
            var destination = field.RandomFrom(
                Position.Baseman1st,
                Position.Shortstop,
                Position.Baseman3rd,
                Position.Pitcher).position;
            StartCoroutine(LerpBall(field.positions[Position.Batter].position, destination, 1f, 3f));
        }

        public void HitPopOut()
        {
            var destination = field.RandomFrom(
                Position.Baseman1st,
                Position.Baseman2nd,
                Position.Baseman3rd,
                Position.Shortstop,
                Position.Pitcher).position;
            StartCoroutine(LerpBall(field.positions[Position.Batter].position, destination, 10f, 2f));
        }

        public void FlyOut()
        {
            var destination = field.RandomFrom(
                Position.FielderLeft,
                Position.FielderCenter,
                Position.FielderRight).position;
            StartCoroutine(LerpBall(field.positions[Position.Batter].position, destination, 20f, 2f));
        }

        public void ThrowToPitcher()
        {
            StartCoroutine(LerpBall(transform.position, origin.position, 3f, 2f));
        }

        public void Reset()
        {
            transform.position = origin.position;
            transform.rotation = origin.rotation;
        }

        private static float MapAngle(float pAngle) => pAngle * Mathf.PI / 180f + Mathf.PI / 2;

        private IEnumerator LerpBall(Vector3 pOrigin, Vector3 pDestination, float pHeight, float pballSpeed = 1f,
            float pAngle = 0f)
        {
            _destination = pDestination;
            var trail = GetComponentInChildren<TrailRenderer>();

            trail.enabled = false;

            transform.position = pOrigin;
            transform.LookAt(pDestination);

            trail.enabled = true;
            var t = 0f;
            while (t <= 1)
            {
                t += pollingRate * pballSpeed;

                transform.position = Vector3.Lerp(pOrigin, pDestination, t);
                var direction = Mathf.Cos(MapAngle(pAngle)) * Vector3.right + Mathf.Sin(MapAngle(pAngle)) * Vector3.up;
                transform.position += pathCurve.Evaluate(t) * pHeight * direction;

                yield return new WaitForSeconds(pollingRate);
            }
            
            choreographer.StartMovement();
        }
    }
}