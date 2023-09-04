using System.Collections;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Ball
{
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private Transform origin;
        [SerializeField] private Transform ballDestination;
        [SerializeField] private Transform strikeDestination;
        [SerializeField] private Transform batterDestination;
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
            StartCoroutine(LerpBall(ballDestination, 2f, 2f, 45f));
        }
        
        public void PitchStrike()
        {
            StartCoroutine(LerpBall(strikeDestination, 2f, 2f, 45f));
        }
        
        public void HitPlayer()
        {
            StartCoroutine(LerpBall(batterDestination, 2f, 2f, 45f));
        }

        public void Reset()
        {
            transform.position = origin.position;
            transform.rotation = origin.rotation;
        }

        private static float MapAngle(float pAngle) => pAngle * Mathf.PI / 180f + Mathf.PI / 2;

        private IEnumerator LerpBall(Transform pDestination, float pHeight, float pPitchSpeed = 1f, float pAngle = 0f)
        {
            var t = 0f;
            transform.position = origin.position;
            transform.LookAt(pDestination.position);

            while (t <= 1)
            {
                t += pollingRate * pPitchSpeed;

                transform.position = Vector3.Lerp(origin.position, pDestination.position, t);
                var direction = Mathf.Cos(MapAngle(pAngle)) * Vector3.right + Mathf.Sin(MapAngle(pAngle)) * Vector3.up;
                transform.position += pathCurve.Evaluate(t) * pHeight * direction;

                yield return new WaitForSeconds(pollingRate);
            }
        }
    }
}