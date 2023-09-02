using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FibDev.Baseball.Player
{
    [Serializable]
    public class Stats
    {
        public string playerName = "Shoulak";
        public int number = 27;
        public HeightType height = HeightType.Medium;
        public WeightType weight = WeightType.Thin;
        public bool lefty = false;

        public Color primaryColor;
        public Color secondaryColor;
        public Color skinColor;
        public Position position;
    }
}