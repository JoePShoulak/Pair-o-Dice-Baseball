using FibDev.Baseball.Choreography.Ball;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Play
{
    public enum RunnerMovement
    {
        Stay, Advance, Force
    }
    
    public class Movement
    {
        public PitchType pitchType;
        public HitType hitType;

        public Vector3 HitDestination;

        public bool bobble = false;
        public RunnerMovement runnerMovement;
        public bool playAtFirst;

        // Cleanup check for inning change and game over. Otherwise...
        // Out players go to dugout, runners passed home go to dugout, ball goes to pitcher
    }
}
