using System;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;
using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Choreography
{
    public class Choreographer : MonoBehaviour
    {
        [SerializeField] private TeamPositions homeDugout;
        [SerializeField] private TeamPositions visitorDugout;
        [SerializeField] private FieldPositions field;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private BallMover ball;

        private Dictionary<TeamType, Team> _teams;

        private readonly Dictionary<Position, Player.Player> _homeTeam = new();
        private readonly Dictionary<Position, Player.Player> _visitorTeam = new();

        private GameObject _homePitcher;
        private GameObject _visitorPitcher;

        private bool _movementUnfolding;

        public event Action OnMovementStart;
        public event Action OnMovementEnd;

        [HideInInspector] public PitchType pitchType;

        private void Start()
        {
            OnMovementStart += () => {
                _movementUnfolding = true;
                Debug.Log("Movement started");
            };
            OnMovementEnd += () => {
                _movementUnfolding = false;
                Debug.Log("Movement Ended");
            };
        }

        private void Update()
        {
            if (!_movementUnfolding) return;
            
            if (CheckPlayHasBeenReset()) OnMovementEnd?.Invoke();
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

        private bool PlayersReset
        {
            get
            {
                return AllPlayers.All(player => player.IsIdle());
            }
        }

        public bool BatterAtPlate => false; // TODO: Implement
        public bool PitcherHasBall => false; // TODO: Implement
        public bool IdlePlayersInDugout => false; // TODO: Implement


        public void SetupGame(Dictionary<TeamType, Team> pTeams)
        {
            OnMovementStart?.Invoke();
            _teams = pTeams;

            CreateTeam(_teams[TeamType.Home]);
            CreateTeam(_teams[TeamType.Visiting]);

            TakePositions(_homeTeam);
            _visitorTeam.Values.ToList()[0].SetIdlePosition(field.positions[Position.Batter]);
            _visitorTeam.Values.ToList()[0].GoToIdle();
        }

        private bool CheckPlayHasBeenReset()
        {
            // Fielders must be back at their starting locations
            // if (PlayersReset && BatterAtPlate && PitcherHasBall && IdlePlayersInDugout) 
            // A batter must be at the plate
            // The pitcher must have the ball
            // Anyone but the batter and the 9 fielders are in the dugout
            
            return PlayersReset;
        }

        public void RunPlay()
        {
            // Data needed:
                // Pitch Type
                // Hit Type
                // Is FlyOut+?
                // Is GroundOut(2)?
            switch (pitchType)
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
            OnMovementStart?.Invoke();
            // Throw pitch
            // Batter interaction
            
            // If Strikeout
                // Batter returns to dugout
            // If Walk or HPB
                // Batter goes to first slowly
                // Basemen advance slowly, if forced
            // If Foul, Pop, Fly, Line Out
                // Base runners begin advancing
                // Someone catches ball
                // Base runners return to their bases
                // Batter returns to dugout
            // If Single, Double, or Triple
                // Batter and all runners advance respective number of bases
                // Ball hits ground in a specific area and is thrown respective base too late to get the batter out
                // Runners past home return to dugout
            // If Home Run
                // Ball goes out of the park
                // Batter and all runners round all bases, then go to the dugout
                // A ball is thrown to the pitcher from the correct dugout
            // If Ground Out
                // Runners advance
                // The ball hits the ground and is fielded to first in time to get the runner out
                // Batter returns to dugout
            // If Error
                // The is almost caught by a player, but they drop it, pick it back up,
                //     ...and throw it to first too late to get the runner out
                // Batter returns to dugout
            // If Fly Out Plus
                // Base runners begin advancing
                // Someone catches ball
                // Base runners return to their bases, except third makes it home
                // Batter returns to dugout
            // If Ground Out(2)
                // Runners advance
                // The ball hits the ground and is fielded to first in time to get the runner out
                // If there was a runner on first before the play...
                    // The first baseman throws the ball to second to get that runner out
                // Batter (and runner formerly on first) return to dugout
                
            // Cleanup
                // New batter comes up
                // Whoever has the ball throws it back to the pitcher
                // Fielders return to their positions
        }

        // TODO: Move to another class
        private void CreateTeam(Team pTeam)
        {
            foreach (Position position in Enum.GetValues(typeof(Position)))
            {
                if (position == Position.Batter) continue;
                CreatePlayer(pTeam, position);
            }
        }

        // TODO: Move to another class
        private void CreatePlayer(Team pTeam, Position pPosition)
        {
            var dugout = pTeam.type == TeamType.Home ? homeDugout : visitorDugout;
            var destination = dugout.positions[pPosition];
            var playerStats = pTeam.players[pPosition];

            var player = Instantiate(playerPrefab, destination.position, Quaternion.identity).GetComponent<Player.Player>();
            player.SetStats(playerStats);
            player.SetIdlePosition(destination);

            switch (pTeam.type)
            {
                case TeamType.Home:
                    _homeTeam.Add(pPosition, player);
                    break;
                case TeamType.Visiting:
                    _visitorTeam.Add(pPosition, player);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // TODO: Have players store their "home" position on the field in a new class to work with the nav mesh agent
        // This will allow for easier checks on if all players are in the correct position on the field
        private void TakePositions(Dictionary<Position, Player.Player> pDict)
        {
            foreach (var (pPosition, playerObj) in pDict)
            {
                playerObj.SetIdlePosition(field.positions[pPosition]);
                playerObj.GoToIdle();
            }
        }
    }
}