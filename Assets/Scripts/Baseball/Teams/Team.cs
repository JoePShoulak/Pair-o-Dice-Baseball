using System.Collections.Generic;
using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Teams
{
    public struct Team
    {
        public string city;
        public string name;
        public Color primary;
        public Color secondary;

        public TeamType type;

        public Dictionary<Position, PlayerStats> players;


    }
}