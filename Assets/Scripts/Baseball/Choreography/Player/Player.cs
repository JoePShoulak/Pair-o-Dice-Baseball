using System;
using System.Collections.Generic;
using FibDev.Baseball.Player;
using FibDev.Baseball.Teams;
using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerStats playerStats;
        private Motion _motion;
        public TeamType team;

        public NavMeshAgent Agent => GetComponent<NavMeshAgent>(); 

        public bool IsIdle()
        {
            return _motion.isIdle;
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
        
        private void GoToIdle()
        {
            _motion.SetDestination(_motion.IdlePosition);
        }

        public void SetIdlePosition(Vector3 position, bool autoGo = true)
        {
            _motion.IdlePosition = position;
            if (autoGo) GoToIdle();
        }
    }
}