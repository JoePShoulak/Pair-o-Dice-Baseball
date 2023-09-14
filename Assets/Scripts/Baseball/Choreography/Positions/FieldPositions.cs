using UnityEngine;
using Random = System.Random;

namespace FibDev.Baseball.Choreography.Positions
{
    public class FieldPositions : TeamPositions
    {
        [SerializeField] private Transform batter;

        private void Start()
        {
            positions.Add(Position.Batter, batter);
        }

        public Transform RandomFrom(params Position[] pPositions)
        {
            var randomPositionIndex = new Random().Next(0, pPositions.Length);
            return positions[pPositions[randomPositionIndex]];
        }
    }
}