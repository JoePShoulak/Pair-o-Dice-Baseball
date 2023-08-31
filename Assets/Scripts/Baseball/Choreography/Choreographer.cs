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
            var Pitcher = CreatePlayer(pTeam, "Pitcher");
            var Catcher = CreatePlayer(pTeam, "Catcher");
            var Shortstop = CreatePlayer(pTeam, "Shortstop");
            
            var Baseman1st = CreatePlayer(pTeam, "Baseman1st");
            var Baseman2nd = CreatePlayer(pTeam, "Baseman2nd");
            var Baseman3rd = CreatePlayer(pTeam, "Baseman3rd");
            
            var FielderLeft = CreatePlayer(pTeam, "FielderLeft");
            var FielderCenter = CreatePlayer(pTeam, "FielderCenter");
            var FielderRight = CreatePlayer(pTeam, "FielderRight");
        }

        private GameObject CreatePlayer(Team pTeam, string pPosition)
        {
            if (!Enum.TryParse(pPosition, out Position pos)) throw new Exception("Couldn't parse position enum");
            
            var dugout = pTeam.type == TeamType.Home ? homeDugout : visitorDugout;
            var destination = (Transform)GetMonoProp(dugout, pPosition);
            
            var playerStats = pTeam.Get(pos);
            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity);
            player.GetComponent<Player>().SetStats(playerStats);
            
            return player;
        }

        private static object GetMonoProp(MonoBehaviour obj, string propName)
        {
            return obj.GetType().GetField(propName)?.GetValue(obj);
        }
    }
}