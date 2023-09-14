using UnityEngine;
using Random = System.Random;

namespace FibDev.Baseball.Choreography.Positions
{
    public class FieldPositions : TeamPositions
    {
        [SerializeField] private Transform batterR;
        [SerializeField] private Transform batterL;

        private void Start()
        {
            positions.Add(Position.BatterR, batterR);
            positions.Add(Position.BatterL, batterL);
        }

        public Transform RandomFrom(params Position[] pPositions)
        {
            var randomPositionIndex = new Random().Next(0, pPositions.Length);
            return positions[pPositions[randomPositionIndex]];
        }
    }
}