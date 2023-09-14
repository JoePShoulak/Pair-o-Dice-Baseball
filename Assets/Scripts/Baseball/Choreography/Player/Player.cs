using System.Collections.Generic;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Player;
using FibDev.Baseball.Teams;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerStats playerStats;
        private Motion _motion;
        public TeamType team;

        public Motion Motion => GetComponent<Motion>();

        private PositionManager _positions;

        private TeamPositions Dugout => team == TeamType.Home
            ? PositionManager.Instance.homeDugout
            : PositionManager.Instance.visitorDugout;

        public Transform DugoutPosition => Dugout.positions[playerStats.position];

        public bool IsIdle()
        {
            return _motion.isIdle;
        }

        private void Awake()
        {
            _motion = GetComponent<Motion>();
        }

        public void GoToDugout()
        {
            SetIdlePosition(DugoutPosition.position);
        }

        public void GoToFieldPosition()
        {
            var offset = Vector3.zero;
            var offsetList = new List<Position> { Position.Baseman1st, Position.Baseman2nd, Position.Baseman3rd };

            if (playerStats.position == Position.Catcher)
            {
                offset = new Vector3(0, 0, -2.5f);
            }
            else if (offsetList.Contains(playerStats.position))
            {
                offset = new Vector3(0, 0, 5);
            }

            var fieldPositions = PositionManager.Instance.field.positions;
            var playerFieldLocation = fieldPositions[playerStats.position].position;

            SetIdlePosition(playerFieldLocation + offset);
        }

        public void SetStats(PlayerStats pPlayerStats)
        {
            playerStats = pPlayerStats;

            var decorator = GetComponent<Decorator>();
            decorator.SetColor(playerStats.primaryColor, playerStats.secondaryColor, playerStats.skinColor);
            decorator.SetJerseyInfo(playerStats.number, playerStats.playerName);
            decorator.SetSize(playerStats.weight, playerStats.height);
        }

        private void GoToIdle()
        {
            _motion.SetDestination(_motion.IdlePosition);
        }

        public void SetIdlePosition(Vector3 pPosition, bool autoGo = true)
        {
            _motion.IdlePosition = pPosition;
            if (autoGo) GoToIdle();
        }
    }
}