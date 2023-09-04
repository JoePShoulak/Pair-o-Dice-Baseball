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


        private const float tanAngle = 2f;

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
            StartCoroutine(LerpBall(field.positions[Position.Batter].position,
                field.positions[Position.Baseman3rd].position, 1f, 3f));
        }

        public void HitPopOut()
        {
            StartCoroutine(LerpBall(field.positions[Position.Batter].position,
                field.positions[Position.Baseman2nd].position, 10f, 2f));
        }

        public void FlyOut()
        {
            StartCoroutine(LerpBall(field.positions[Position.Batter].position,
                field.positions[Position.FielderCenter].position, 20f, 2f));
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
        }
    }
}