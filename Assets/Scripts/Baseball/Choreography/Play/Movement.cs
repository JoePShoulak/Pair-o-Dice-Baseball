using System;
using FibDev.Baseball.Choreography.Ball;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Play
{
    public enum RunnerMovement
    {
        Stay,
        Advance,
        Force
    }

    public class Movement
    {
        public PitchType pitchType;
        public HitType hitType;

        public Vector3 HitDestination;

        public bool bobble = false;
        public RunnerMovement runnerMovement;
        public bool playAtFirst;

        public bool inProgress;

        public event Action<Movement> OnMovementStart = movement =>
        {
            movement.inProgress = true;
            Debug.Log("Movement started");
        };

        public static event Action<Movement> OnMovementEnd = movement =>
        {
            movement.inProgress = false;
            Debug.Log("Movement Ended");
        };

        public void StartMovement()
        {
            OnMovementStart?.Invoke(this);
        }

        public void EndMovement()
        {
            OnMovementEnd?.Invoke(this);
        }
    }
}