using UnityEngine;

namespace FibDev.Baseball.Choreography.Positions
{
    public class FieldPositions : TeamPositions
    {
        [SerializeField] private Transform batter;

        private void Start()
        {
            positions.Add(Position.Batter, batter);
        }
    }
}