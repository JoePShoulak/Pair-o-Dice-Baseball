using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;

        public void SetStats(PlayerStats pPlayerStats)
        {
            playerStats = pPlayerStats;

            var decorator = GetComponent<Decorator>();
            decorator.SetColor(playerStats.primaryColor, playerStats.secondaryColor);
            decorator.SetJerseyNumber(playerStats.number);
            decorator.SetName(playerStats.playerName);
        }
    }
}