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

        public RunnerMovement runnerMovement;
        public bool playAtFirst;

        public bool inProgress;
        public bool outHasOccurred;

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
        
        public static event Action<Movement> OnOut = movement =>
        {
            movement.outHasOccurred = true;
            Debug.Log("Out Has Occurred");
        };

        public void StartMovement()
        {
            OnMovementStart?.Invoke(this);
        }

        public void EndMovement()
        {
            OnMovementEnd?.Invoke(this);
        }

        public void CallOut()
        {
            OnOut?.Invoke(this);
        }
    }
}