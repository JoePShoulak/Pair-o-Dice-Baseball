using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerStats playerStats;
        private Motion _motion;

        public bool IsIdle()
        {
            return _motion.IsIdle;
        }

        private void Awake()
        {
            _motion = GetComponent<Motion>();
        }

        public void SetStats(PlayerStats pPlayerStats)
        {
            playerStats = pPlayerStats;

            var decorator = GetComponent<Decorator>();
            decorator.SetColor(playerStats.primaryColor, playerStats.secondaryColor, playerStats.skinColor);
            decorator.SetJerseyNumber(playerStats.number);
            decorator.SetName(playerStats.playerName);
        }

        public void GoTo(Transform pTransform)
        {
            _motion.SetDestination(pTransform.position);
        }
        
        public void GoToIdle()
        {
            _motion.SetDestination(_motion.IdlePosition);
        }

        public void SetIdlePosition(Transform pTransform)
        {
            _motion.IdlePosition = pTransform.position;
        }
    }
}