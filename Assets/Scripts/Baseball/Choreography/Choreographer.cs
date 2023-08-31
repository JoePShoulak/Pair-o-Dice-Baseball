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

        private List<Team> teams;
        private Team homeTeam;
        private Team visitorTeam;

        private Dictionary<Position, GameObject> _homeTeamGOs = new();
        private Dictionary<Position, GameObject> _visitorTeamGOs = new();

        private GameObject homePitcher;
        private GameObject visitorPitcher;

        private Team GetTeam(TeamType pType)
        {
            return teams[0].type == pType ? teams[0] : teams[1];
        }

        public void SetupGame(List<Team> pTeams)
        {
            teams = pTeams;

            homeTeam = GetTeam(TeamType.Home);
            visitorTeam = GetTeam(TeamType.Visiting);

            CreateTeam(homeTeam);
            CreateTeam(visitorTeam);
            
            TakeField(_homeTeamGOs);
            _visitorTeamGOs[Position.Catcher].GetComponent<NavMeshAgent>().SetDestination(field.Batter.position);
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
            var destination = dugout.GetTransform(pPosition);
            var playerStats = pTeam.Get(pPosition);

            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity);
            player.GetComponent<Player>().SetStats(playerStats);

            if (pTeam.type == TeamType.Home) _homeTeamGOs.Add(pPosition, player);
            if (pTeam.type == TeamType.Visiting) _visitorTeamGOs.Add(pPosition, player);
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