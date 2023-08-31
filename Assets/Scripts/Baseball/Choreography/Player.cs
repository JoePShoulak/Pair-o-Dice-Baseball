using System;
using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography
{
    public class Player : MonoBehaviour
    {
        private Stats stats;
        private static readonly int Primary = Shader.PropertyToID("_Primary");
        private static readonly int Secondary = Shader.PropertyToID("_Secondary");

        public void SetStats(Stats pStats)
        {
            stats = pStats;

            SetColor(stats.primaryColor, stats.secondaryColor);
        }

        private void SetColor(Color pPrimary, Color pSecondary)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Primary, pPrimary);
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Secondary, pSecondary);
        }

        private void OnMouseDown()
        {
            Debug.Log("Clicked");
        }
    }
}