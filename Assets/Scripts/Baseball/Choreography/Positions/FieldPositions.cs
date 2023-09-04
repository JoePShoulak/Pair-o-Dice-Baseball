using System;
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

        public GameObject ClosestPlayerTo(Vector3 pPosition)
        {
            Transform closestTransform = null;
            var minDistance = float.MaxValue;
            
            foreach (var playerTransform in positions.Values)
            {
                var distance = Vector3.Distance(playerTransform.position, pPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTransform = playerTransform;
                }
            }
            
            return closestTransform!.gameObject;
        }
    }
}