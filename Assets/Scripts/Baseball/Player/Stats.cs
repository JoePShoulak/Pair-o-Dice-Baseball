using UnityEngine;

namespace FibDev.Baseball.Player
{
    public class Stats
    {
        public string name = "Shoulak";
        public int number = 27;
        public HeightType height = HeightType.Medium;
        public WeightType weight = WeightType.Thin;
        public bool lefty = false;
        public float skinTone = 0.9f;

        public Color primaryColor;
        public Color secondaryColor;
        public Position position;
    }
}
