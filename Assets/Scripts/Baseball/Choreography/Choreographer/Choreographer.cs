using System;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using UnityEngine;
using UnityEngine.AI;

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
        private BaseManager _baseManager;

        public Movement movement = new();

        private PlayerCreator _playerCreator;

        private void Start()
        {
            _playerCreator = GetComponent<PlayerCreator>();
            _baseManager = GetComponent<BaseManager>();
        }

        private void Cleanup()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            if (!movement.inProgress) return;

            if (PlayersReset) movement.EndMovement();
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

        private TeamType TeamAtBat => _teamOnField == TeamType.Home ? TeamType.Visiting : TeamType.Home;

        private Player.Player ActiveBatter => TeamAtBat == TeamType.Home
            ? _homeTeam.Values.ToList()[_homeBatterIndex]
            : _visitorTeam.Values.ToList()[_visitorBatterIndex];

        public void SetupGame(Dictionary<TeamType, Team> pTeams)
        {
            movement.StartMovement();
            _teams = pTeams;

            _homeTeam = _playerCreator.CreateTeam(_teams[TeamType.Home], homeDugout, visitorDugout);
            _visitorTeam = _playerCreator.CreateTeam(_teams[TeamType.Visiting], homeDugout, visitorDugout);

            TakeFieldPositions(_homeTeam);
            _baseManager.CallNewBatter(ActiveBatter);
        }

        public void Begin()
        {
            // movement.OnMovementEnd += () => 
            movement.StartMovement();
        }

        public void RunPlay(Action callback)
        {
            ball.animator.enabled = true;
            ball.animator.Play("Ball");

            switch (movement.runnerMovement)
            {
                case RunnerMovement.Single:
                    _baseManager.Advance(ActiveBatter);
                    break;
                case RunnerMovement.Double:
                    _baseManager.Advance(ActiveBatter, 2);
                    break;
                case RunnerMovement.Triple:
                    _baseManager.Advance(ActiveBatter, 3);
                    break;
                case RunnerMovement.HomeRun:
                    _baseManager.Advance(ActiveBatter, 4);
                    break;
                case RunnerMovement.Force:
                    _baseManager.AdvanceIfForced(ActiveBatter);
                    break;
                case RunnerMovement.Stay:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log($"Movement: {movement.runnerMovement}");
            if (movement.runnerMovement == RunnerMovement.Stay)
            {
                _baseManager.Out(ActiveBatter, TeamAtBat);

                movement.EndMovement();
            }

            if (_teamOnField == TeamType.Home)
            {
                _visitorBatterIndex++;
            }
            else
            {
                _homeBatterIndex++;
            }

            _baseManager.CallNewBatter(ActiveBatter);

            callback?.Invoke();
        }

        private void TakeFieldPositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var (pPosition, playerObj) in pDict)
            {
                playerObj.SetIdlePosition(field.positions[pPosition]);
            }
        }
    }
}