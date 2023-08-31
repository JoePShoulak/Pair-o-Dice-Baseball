using System;
using System.Collections.Generic;
using UnityEngine;
using FibDev.Baseball.Choreography.References;
using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Choreography
{
    public class Choreographer : MonoBehaviour
    {
        [SerializeField] private TeamPositions homeDugout;
        [SerializeField] private TeamPositions visitorDugout;
        [SerializeField] private FieldPositions field;
        [SerializeField] private GameObject playerPrefab;

        private List<Team> _teams;
        private Team _homeTeam;
        private Team _visitorTeam;

        private Dictionary<Position, GameObject> _homeTeamGOs = new();
        private Dictionary<Position, GameObject> _visitorTeamGOs = new();

        private GameObject homePitcher;
        private GameObject visitorPitcher;

        private Team GetTeam(TeamType type)
        {
            return _teams[0].type == type ? _teams[0] : _teams[1];
        }

        public void SetupGame(List<Team> teams)
        {
            _teams = teams;

            _homeTeam = GetTeam(TeamType.Home);
            _visitorTeam = GetTeam(TeamType.Visiting);

            CreateTeam(_homeTeam);
            CreateTeam(_visitorTeam);
        }

        private void CreateTeam(Team pTeam)
        {
            foreach (Position position in Enum.GetValues(typeof(Position)))
            {
                CreatePlayer(pTeam, position);
            }
        }

        private void CreatePlayer(Team pTeam, Position pPosition)
        {
            var dugout = pTeam.type == TeamType.Home ? homeDugout : visitorDugout;
            var destination = (Transform)GetMonoProp(dugout, pPosition.ToString());

            var playerStats = pTeam.Get(pPosition);
            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity);
            player.GetComponent<Player>().SetStats(playerStats);

            var teamDict = pTeam.type == TeamType.Home ? _homeTeamGOs : _visitorTeamGOs;
            teamDict.Add(pPosition, player);
        }

        private static object GetMonoProp(MonoBehaviour obj, string propName)
        {
            return obj.GetType().GetField(propName)?.GetValue(obj);
        }
    }
}