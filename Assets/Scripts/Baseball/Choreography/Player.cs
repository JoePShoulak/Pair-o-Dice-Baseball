using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        public void SetStats(Stats pStats)
        {
            stats = pStats;

            var decorator = GetComponent<Decorator>();
            decorator.SetColor(stats.primaryColor, stats.secondaryColor);
            decorator.SetJerseyNumber(stats.number);
            
        }
    }
}