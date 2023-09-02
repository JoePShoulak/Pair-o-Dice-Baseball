using FibDev.Baseball.Player;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;
        private Movement _movement;

        public bool IsIdle()
        {
            return _movement.IsIdle;
        }

        private void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        public void SetStats(PlayerStats pPlayerStats)
        {
            playerStats = pPlayerStats;

            var decorator = GetComponent<Decorator>();
            decorator.SetColor(playerStats.primaryColor, playerStats.secondaryColor);
            decorator.SetJerseyNumber(playerStats.number);
            decorator.SetName(playerStats.playerName);
        }

        public void GoTo(Transform pTransform)
        {
            _movement.SetDestination(pTransform.position);
        }
        
        public void GoToIdle()
        {
            _movement.SetDestination(_movement.IdlePosition);
        }

        public void SetIdlePosition(Transform pTransform)
        {
            _movement.IdlePosition = pTransform.position;
        }
    }
}