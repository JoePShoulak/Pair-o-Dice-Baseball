using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FibDev.Baseball.Player
{
    [Serializable]
    public class Stats
    {
        public string playerName;
        public string number;
        public HeightType height;
        public WeightType weight;
        public bool lefty;

        public Color primaryColor;
        public Color secondaryColor;
        public Color skinColor;
        public Position position;
    }
}