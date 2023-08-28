using UnityEngine;

namespace FibDev
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform start;
        public Transform stadium;
        [SerializeField] private float lerpDuration = 2.0f;

        private Vector3 lerpStartPos;
        private Quaternion lerpStartRot;

        private float lerpStartTime;
        private Transform target;

        private void Update()
        {
            if (target == null) return;

            var lerpTime = (Time.time - lerpStartTime) / lerpDuration;

            var lerpedPosition = Vector3.Lerp(lerpStartPos, target.position, lerpTime);
            var lerpedRotation = Quaternion.Slerp(lerpStartRot, target.rotation, lerpTime);
            transform.position = lerpedPosition;
            transform.rotation = lerpedRotation;

            if (lerpTime > 1.0f) target = null;
        }

        public void LerpTo(Transform _target, float duration)
        {
            lerpStartPos = transform.position;
            lerpStartRot = transform.rotation;

            target = _target;
            lerpStartTime = Time.time;
            lerpDuration = duration;
        }
    }
}