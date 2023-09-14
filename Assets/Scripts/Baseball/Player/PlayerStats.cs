using System;
using UnityEngine;

namespace FibDev.Baseball.Player
{
    [Serializable]
    public class PlayerStats
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

        public int battingIndex;

        public void Log()
        {
            Debug.Log($"  Name: {playerName}");
            Debug.Log($"    Number: {number}");
            Debug.Log($"    Height: {height}");
            Debug.Log($"    Weight: {weight}");
            Debug.Log($"    Lefty?: {lefty}");
            Debug.Log($"    Skin Tone: {skinColor}");
            Debug.Log($"    P. Color: {primaryColor}");
            Debug.Log($"    S. Color: {secondaryColor}");
            Debug.Log($"    Position: {position}");
        }
    }
}