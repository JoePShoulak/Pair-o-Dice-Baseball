using UnityEngine;
using System.Linq;

using DG.Tweening;

using FibDev.Baseball;
using FibDev.Baseball.Choreography.Positions;

namespace FibDev.Core
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Target Transforms")] public Transform start;
        public Transform stadium;
        public Transform stadiumWaypoint;
        public Transform scoreboard;
        public Transform notebook;

        private const float DEFAULT_DURATION = 0.5f;

        private static Transform Pitcher => PositionManager.Instance.field.positions[Position.Pitcher];

        private void Start()
        {
            transform.position = start.position;
            transform.rotation = start.rotation;
        }

        public void MoveTo(Transform _target, float duration = DEFAULT_DURATION, TweenCallback cb = null)
        {
            Rotate(_target, duration);
            transform.DOMove(_target.position, duration).SetEase(Ease.InOutQuad).OnComplete(cb);
        }

        private void Rotate(Transform _target, float duration)
        {
            var desiredRotation = Quaternion.LookRotation(_target.forward, _target.up);
            transform.DORotate(desiredRotation.eulerAngles, duration).SetEase(Ease.InOutQuad);
        }

        public void MoveAroundStadium(Transform pTarget, float duration = DEFAULT_DURATION, TweenCallback cb = null)
        {
            var waypoints = new[] { stadiumWaypoint, pTarget }.Select(pTransform => pTransform.position).ToArray();

            if (pTarget == stadium) transform.DODynamicLookAt(Pitcher.position, duration);
            else Rotate(start, duration);

            transform.DOPath(waypoints, duration, PathType.CatmullRom).SetEase(Ease.InOutQuad).OnComplete(cb);
        }
    }
}