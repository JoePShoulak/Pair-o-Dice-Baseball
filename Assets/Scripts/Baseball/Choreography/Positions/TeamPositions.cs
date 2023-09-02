using System.Collections.Generic;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Positions
{
    public class TeamPositions : MonoBehaviour
    {
        [SerializeField] private Transform Catcher;
        [SerializeField] private Transform Pitcher;
        [SerializeField] private Transform Shortstop;

        [SerializeField] private Transform Baseman1st;
        [SerializeField] private Transform Baseman2nd;
        [SerializeField] private Transform Baseman3rd;

        [SerializeField] private Transform FielderLeft;
        [SerializeField] private Transform FielderCenter;
        [SerializeField] private Transform FielderRight;

        public Dictionary<Position, Transform> positions;

        private void Start()
        {
            positions = new Dictionary<Position, Transform>
            {
                { Position.Catcher, Catcher },
                { Position.Pitcher, Pitcher },
                { Position.Shortstop, Shortstop },
                { Position.Baseman1st, Baseman1st },
                { Position.Baseman2nd, Baseman2nd },
                { Position.Baseman3rd, Baseman3rd },
                { Position.FielderLeft, FielderLeft },
                { Position.FielderCenter, FielderCenter },
                { Position.FielderRight, FielderRight }
            };
        }
    }
}