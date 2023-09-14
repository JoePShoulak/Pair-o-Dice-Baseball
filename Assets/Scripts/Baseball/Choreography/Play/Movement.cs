using System;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Play
{
    public enum RunnerMovement
    {
        Single,
        Double,
        Triple,
        HomeRun,
        Force,
        Stay,
        OutAdvance
    }

    public class Movement
    {
        public RunnerMovement runnerMovement = RunnerMovement.Stay;

        public bool inProgress;

        // ReSharper disable once EventNeverSubscribedTo.Global
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