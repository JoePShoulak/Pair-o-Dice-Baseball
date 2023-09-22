using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FibDev.Audio;
using UnityEngine;
using UnityEngine.UI;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using FibDev.Core;
using FibDev.Core.ScoreBook;
using FibDev.UI;
using FibDev.UI.Score_Overlay;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class Choreographer : MonoBehaviour
    {
        [SerializeField] private TeamPositions homeDugout;
        [SerializeField] private TeamPositions visitorDugout;
        [SerializeField] private BallMover ball;

        private Dictionary<TeamType, Team> _teams;

        private Dictionary<Position, Player.Player> _homeTeam = new();
        private Dictionary<Position, Player.Player> _visitorTeam = new();
        [SerializeField] private ScoreBook scorebook;


        private int _homeBatterIndex;
        private int _visitorBatterIndex;

        private TeamType _teamOnField = TeamType.Home;

        private GameObject _homePitcher;
        private GameObject _visitorPitcher;
        private BaseManager _baseManager;

        public Movement movement = new();

        private PlayerCreator _playerCreator;

        private bool _mustChangeSides;
        private bool _mustEndGame;

        public bool gameEnded;
        private Engine.Engine engine;

        private static ScoreOverlay ScoreOverlay =>
            OverlayManager.Instance.gameOverlay.GetComponent<GameOverlayUI>().ScoreOverlay;

        private void Start()
        {
            _playerCreator = GetComponent<PlayerCreator>();
            _baseManager = GetComponent<BaseManager>();

            engine = GetComponent<Engine.Engine>();
            ScoreOverlay.Reset();
            engine.OnInningAdvance += () =>
            {
                _mustChangeSides = true;
                ScoreOverlay.AdvanceInning();
            };
            engine.OnGameEnd += () => _mustEndGame = true;
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

        private TeamType TeamAtBat => _teamOnField == TeamType.Home ? TeamType.Visiting : TeamType.Home;

        private Player.Player ActiveBatter()
        {
            List<Player.Player> players;
            int index;

            if (TeamAtBat == TeamType.Home)
            {
                players = _homeTeam.Values.ToList();
                index = _homeBatterIndex;
            }
            else
            {
                players = _visitorTeam.Values.ToList();
                index = _visitorBatterIndex;
            }

            return players.First(player => player.playerStats.battingIndex == index);
        }

        public void SetupGame(Dictionary<TeamType, Team> pTeams)
        {
            gameEnded = false;

            movement.StartMovement();
            _teams = pTeams;

            _homeTeam = _playerCreator.CreateTeam(_teams[TeamType.Home], homeDugout, visitorDugout);
            _visitorTeam = _playerCreator.CreateTeam(_teams[TeamType.Visiting], homeDugout, visitorDugout);

            TakeFieldPositions(_homeTeam);
            _baseManager.CallNewBatter(ActiveBatter());
        }

        private void EndGame()
        {
            gameEnded = true;

            var cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
            
            TearDownGame();
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().rollButton.interactable = false;
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().autoRun.interactable = false;
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().ToggleCamera();

            var awayName = _teams[TeamType.Visiting].name;
            var homeName = _teams[TeamType.Home].name;
            var visitorTotalRuns = engine.record.visitorTotal.runs;
            var homeTotalRuns = engine.record.homeTotal.runs;
            var record = ScoreBook.ComposeRecord(awayName, homeName, visitorTotalRuns, homeTotalRuns);
            
            scorebook.AddRecord(record);
        }

        public void Begin()
        {
            // movement.OnMovementEnd += () => 
            movement.StartMovement();
        }

        public void TearDownGame()
        {
            OverlayManager.Instance.GetComponentInChildren<GameOverlayUI>().autoRun.isOn = false;

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
            _baseManager.CallNewBatter(ActiveBatter());
        }

        private static Button CamButton => OverlayManager.Instance.gameOverlay.GetComponent<GameOverlayUI>().camButton;

        public void InitiateMovement(Action callback)
        {
            if (gameEnded) return;

            ball.animator.enabled = true;
            ball.animator.Play("Ball", -1, 0f);

            StartCoroutine(ExecutePlay(callback));
        }

        private void HandleBases()
        {
            switch (movement.runnerMovement)
            {
                case RunnerMovement.Single:
                    _baseManager.Advance(ActiveBatter());
                    break;
                case RunnerMovement.Double:
                    _baseManager.Advance(ActiveBatter(), 2);
                    break;
                case RunnerMovement.Triple:
                    _baseManager.Advance(ActiveBatter(), 3);
                    break;
                case RunnerMovement.HomeRun:
                    _baseManager.Advance(ActiveBatter(), 4);
                    break;
                case RunnerMovement.Force:
                    _baseManager.AdvanceIfForced(ActiveBatter());
                    break;
                case RunnerMovement.Stay:
                    break;
                case RunnerMovement.OutAdvance:
                    _baseManager.Advance(ActiveBatter(), 1, false);
                    BaseManager.Out(ActiveBatter());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator ExecutePlay(Action callback)
        {
            StartCoroutine(PlayBallSound());
            var animationTime = ball.animator.GetCurrentAnimatorStateInfo(0).length;
            const float offset = 1f;
            yield return new WaitForSeconds((animationTime - offset)*Time.timeScale);
            CamButton.interactable = true;


            movement.StartMovement();
            ScoreOverlay.SetScores(engine.record.visitorTotal.runs, engine.record.homeTotal.runs);
            ScoreOverlay.SetBases(engine.bases);
            ScoreOverlay.SetOuts(engine.outs);

            HandleBases();

            if (movement.runnerMovement == RunnerMovement.Stay)
            {
                BaseManager.Out(ActiveBatter());
            }

            if (_teamOnField == TeamType.Home)
            {
                _visitorBatterIndex = (_visitorBatterIndex + 1) % _visitorTeam.Count;
            }
            else
            {
                _homeBatterIndex = (_homeBatterIndex + 1) % _homeTeam.Count;
            }

            _baseManager.CallNewBatter(ActiveBatter());

            if (_mustEndGame)
            {
                _mustEndGame = false;
                EndGame();
            }

            if (_mustChangeSides)
            {
                _mustChangeSides = false;
                SwitchSides();
            }

            callback?.Invoke();
        }

        private IEnumerator PlayBallSound()
        {
            var runnerMovement = movement.runnerMovement;
            const float preDelay = 3.85f;
            const float delay = 0.5f;

            yield return new WaitForSeconds(preDelay);
            
            if (runnerMovement == RunnerMovement.Stay)
            {
                AudioManager.Instance.Play("Baseball Out");
                yield return new WaitForSeconds(delay);
                AudioManager.Instance.Play("Crowd Negative");
            }
            else
            {
                AudioManager.Instance.Play("Baseball Hit");
                yield return new WaitForSeconds(delay);
                AudioManager.Instance.Play("Crowd Positive");
            }
        }

        private static void TakeFieldPositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var playerObj in pDict.Values)
            {
                playerObj.GoToFieldPosition();
            }
        }

        private static void TakeDugoutPositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var playerObj in pDict.Values)
            {
                playerObj.GoToDugout();
            }
        }
    }
}