using System;
using System.Collections.Generic;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class PlayerCreator : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        public Dictionary<Position, Player.Player> CreateTeam(Team pTeam, TeamPositions homeDugout,
            TeamPositions visitorDugout)
        {
            var teamDict = new Dictionary<Position, Player.Player>();

            foreach (Position position in Enum.GetValues(typeof(Position)))
            {
                if (position == Position.Batter) continue;
                CreatePlayer(teamDict, pTeam, position, homeDugout, visitorDugout);
            }

            return teamDict;
        }

        private void CreatePlayer(IDictionary<Position, Player.Player> teamDict, Team pTeam, Position pPosition,
            TeamPositions homeDugout, TeamPositions visitorDugout)
        {
            var dugout = pTeam.type == TeamType.Home ? homeDugout : visitorDugout;
            var destination = dugout.positions[pPosition];
            var playerStats = pTeam.players[pPosition];

            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity)
                .GetComponent<Player.Player>();
            player.SetStats(playerStats);
            player.SetIdlePosition(destination);

            teamDict.Add(pPosition, player);
        }
    }
}

