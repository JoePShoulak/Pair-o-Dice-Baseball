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

        public Dictionary<Position, Stats> players; // TODO: Make this a dict


        public void Log()
        {
            
            Debug.Log($"Name: {city} {name}");
            Debug.Log($"  P. Color: {primary}");
            Debug.Log($"  S. Color: {secondary}");
            Debug.Log($"  Type: {type}");

            Debug.Log("\n  Players:");
            foreach (var player in players.Values) player.Log();
        }
    }
}