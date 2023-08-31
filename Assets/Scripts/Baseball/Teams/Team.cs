using System;
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

        public Stats[] players;

        public Stats Get(Position pPosition)
        {
            foreach (var player in players)
            {
                if (player.position == pPosition) return player;
            }

            throw new Exception("No player found");
        }
    }
}