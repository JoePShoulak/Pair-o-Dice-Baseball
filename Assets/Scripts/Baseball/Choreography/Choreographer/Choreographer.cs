using System;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using UnityEngine;
using Motion = FibDev.Baseball.Choreography.Player.Motion;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class Choreographer : MonoBehaviour
    {
        [SerializeField] private TeamPositions homeDugout;
        [SerializeField] private TeamPositions visitorDugout;
        [SerializeField] private FieldPositions field;
        [SerializeField] private BallMover ball;

        private Dictionary<TeamType, Team> _teams;

        private Dictionary<Position, Player.Player> _homeTeam = new();
        private Dictionary<Position, Player.Player> _visitorTeam = new();

        private int _homeBatterIndex = 0;
        private int _visitorBatterIndex = 0;

        private TeamType _teamOnField = TeamType.Home;

        private GameObject _homePitcher;
        private GameObject _visitorPitcher;

        public Movement movement = new();

        private PlayerCreator _playerCreator;

        private void Start()
        {
            _playerCreator = GetComponent<PlayerCreator>();
        }

        private void Update()
        {
            if (!movement.inProgress) return;
            
            if (PlayHasBeenReset) movement.EndMovement();
        }

        private IEnumerable<Player.Player> AllPlayers
        {
            get
            {
                var allPlayers = new List<Player.Player>();

                allPlayers.AddRange(_homeTeam.Values.ToList());
                allPlayers.AddRange(_visitorTeam.Values.ToList());

                return allPlayers;
            }
        }

        private bool PlayersReset => AllPlayers.All(player => player.IsIdle());

        private Player.Player ActivePitcher =>
            (_teamOnField == TeamType.Visiting ? _visitorTeam : _homeTeam)[Position.Pitcher];

        private bool PitcherHasBall => ActivePitcher.GetComponent<Motion>().HasBall;

        private bool PlayHasBeenReset => PlayersReset && PitcherHasBall;

        public void SetupGame(Dictionary<TeamType, Team> pTeams)
        {
            movement.StartMovement();
            _teams = pTeams;

            _homeTeam = _playerCreator.CreateTeam(_teams[TeamType.Home], homeDugout, visitorDugout);
            _visitorTeam = _playerCreator.CreateTeam(_teams[TeamType.Visiting], homeDugout, visitorDugout);

            TakeFieldPositions(_homeTeam);
            var batter = _visitorTeam.Values.ToList()[_visitorBatterIndex];
            batter.SetIdlePosition(field.positions[Position.Batter]);
            batter.GoToIdle();
        }

        public void Begin()
        {
            movement.StartMovement();
        }

        public void RunPlay()
        {
            switch (movement.pitchType)
            {
                case PitchType.Strike:
                    ball.PitchStrike();
                    break;
                case PitchType.Ball:
                    ball.PitchBall();
                    break;
                case PitchType.HitByPitch:
                    ball.HitPlayer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // Do more
        }

        private void TakeFieldPositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var (pPosition, playerObj) in pDict)
            {
                playerObj.SetIdlePosition(field.positions[pPosition]);
                playerObj.GoToIdle();
            }
        }
    }
}