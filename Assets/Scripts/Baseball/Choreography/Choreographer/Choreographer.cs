using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using FibDev.Core;
using FibDev.UI;
using UnityEngine;
using UnityEngine.UI;

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

        private bool _mustChangeSides;

        private void Start()
        {
            _playerCreator = GetComponent<PlayerCreator>();
            _baseManager = GetComponent<BaseManager>();

            var engine = GetComponent<Engine.Engine>();
            engine.OnInningAdvance += () => _mustChangeSides = true;
            engine.OnGameEnd += EndGame;
        }

        private void Update()
        {
            if (!movement.inProgress) return;

            if (PlayersReset)
            {
                movement.EndMovement();
                movement = new Movement();
            }
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

        private void EndGame()
        {
            var cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
            cam.LerpTo(cam.scoreboard, 2f);
            TearDownGame();
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().rollButton.interactable = false;
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().autoRun.interactable = false;
        }

        public void Begin()
        {
            // movement.OnMovementEnd += () => 
            movement.StartMovement();
        }

        public void TearDownGame()
        {
            foreach (var player in AllPlayers)
            {
                if (player == null) return;
                Destroy(player.gameObject);
            }
        }

        private void SwitchSides()
        {
            _teamOnField = _teamOnField == TeamType.Home ? TeamType.Visiting : TeamType.Home;
            
            TakeFieldPositions(_teamOnField == TeamType.Home ? _homeTeam : _visitorTeam);
            TakeDugoutPositions(_teamOnField == TeamType.Visiting ? _homeTeam : _visitorTeam);
            
            _baseManager.Reset();
            _baseManager.CallNewBatter(ActiveBatter);
        }

        private Button CamButton => OverlayManager.Instance.gameOverlay.GetComponent<GameOverlayUI>().camButton;

        public void InitiateMovement(Action callback)
        {
            ball.animator.enabled = true;
            ball.animator.Play("Ball", -1, 0f);

            StartCoroutine(ExecutePlay(callback));
        }

        private IEnumerator ExecutePlay(Action callback)
        {
            var animationTime = ball.animator.GetCurrentAnimatorStateInfo(0).length;
            const float offset = 1f;
            yield return new WaitForSeconds(animationTime - offset);
            CamButton.interactable = true;
            
            movement.StartMovement();
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
                case RunnerMovement.OutAdvance:
                    _baseManager.Advance(ActiveBatter, 1, false);
                    _baseManager.Out(ActiveBatter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log($"Movement: {movement.runnerMovement}");
            if (movement.runnerMovement == RunnerMovement.Stay)
            {
                _baseManager.Out(ActiveBatter);
            }

            if (_teamOnField == TeamType.Home)
            {
                _visitorBatterIndex = (_visitorBatterIndex + 1) % _visitorTeam.Count;
            }
            else
            {
                _homeBatterIndex = (_homeBatterIndex + 1) % _homeTeam.Count;
            }

            _baseManager.CallNewBatter(ActiveBatter);
            
            if (_mustChangeSides) SwitchSides();

            callback?.Invoke();
        }

        private void TakeFieldPositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var (pPosition, playerObj) in pDict)
            {
                var offsetList = new List<Position> { Position.Baseman1st, Position.Baseman2nd, Position.Baseman3rd };
                var offset = offsetList.Contains(pPosition) ? new Vector3(0, 0, 5) : Vector3.zero;

                playerObj.SetIdlePosition(field.positions[pPosition].position + offset);
            }
        }  
        
        private void TakeDugoutPositions(Dictionary<Position, Player.Player> pDict)
        {
            var dugout = pDict[Position.Pitcher].team == TeamType.Home ? homeDugout : visitorDugout;
            
            foreach (var (pPosition, playerObj) in pDict)
            {
                playerObj.SetIdlePosition(dugout.positions[pPosition].position);
            }
        }
    }
}