using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography
{
    public class Player : MonoBehaviour
    {
        private Stats _stats;

        public void SetStats(Stats stats)
        {
            _stats = stats;

            SetColor(stats.primaryColor, stats.secondaryColor);
        }

        public void SetColor(Color primary, Color secondary)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Primary", primary);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Secondary", secondary);
        }

        void Start()
        {
        }


        void Update()
        {
        }
    }
}