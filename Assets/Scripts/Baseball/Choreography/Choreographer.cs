using System;
using System.Collections.Generic;
using UnityEngine;
using FibDev.Baseball.Choreography.References;
using FibDev.Baseball.Teams;
using UnityEngine.AI;

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

        private GameObject _homePitcher;
        private GameObject _visitorPitcher;

        private Team GetTeam(TeamType pType)
        {
            return _teams[0].type == pType ? _teams[0] : _teams[1];
        }

        public void SetupGame(List<Team> pTeams)
        {
            _teams = pTeams;

            _homeTeam = GetTeam(TeamType.Home);
            _visitorTeam = GetTeam(TeamType.Visiting);

            CreateTeam(_homeTeam);
            CreateTeam(_visitorTeam);
            
            TakeField(_homeTeamGOs);
            _visitorTeamGOs[Position.Catcher].GetComponent<NavMeshAgent>().SetDestination(field.Batter.position);
        }

        private void CreateTeam(Team pTeam)
        {
            foreach (Position position in Enum.GetValues(typeof(Position)))
            {
                Debug.Log(position);
                CreatePlayer(pTeam, position);
            }
        }

        private void CreatePlayer(Team pTeam, Position pPosition)
        {
            var dugout = pTeam.type == TeamType.Home ? homeDugout : visitorDugout;
            var destination = dugout.GetTransform(pPosition);
            var playerStats = pTeam.Get(pPosition);

            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity);
            player.GetComponent<Player>().SetStats(playerStats);

            if (pTeam.type == TeamType.Home)
            {
                _homeTeamGOs.Add(pPosition, player);
            }

            if (pTeam.type == TeamType.Visiting)
            {
                _visitorTeamGOs.Add(pPosition, player);
            }
        }

        private void TakeField(Dictionary<Position, GameObject> pDict)
        {
            foreach (var (position, gObj) in pDict)
            {
                gObj.GetComponent<NavMeshAgent>().SetDestination(field.GetTransform(position).position);
            }
        }
    }
}