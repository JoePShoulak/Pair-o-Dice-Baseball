using System;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;
using FibDev.Baseball.Teams;
using Motion = FibDev.Baseball.Choreography.Player.Motion;

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

        private int _homeBatterIndex = 0;
        private int _visitorBatterIndex = 0;

        private TeamType _teamOnField = TeamType.Home;

        private GameObject _homePitcher;
        private GameObject _visitorPitcher;

        public Movement movement = new();

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

        private bool PlayersReset
        {
            get
            {
                return AllPlayers.All(player => player.IsIdle());
            }
        }

        private bool PitcherHasBall
        {
            get
            {
                var pitcher = (_teamOnField == TeamType.Visiting ? _visitorTeam : _homeTeam)[Position.Pitcher];
                return pitcher.GetComponent<Motion>().HasBall;
            }
        }

        private bool PlayHasBeenReset => PlayersReset && PitcherHasBall;

        public void SetupGame(Dictionary<TeamType, Team> pTeams)
        {
            movement.StartMovement();
            _teams = pTeams;

            CreateTeam(_teams[TeamType.Home]);
            CreateTeam(_teams[TeamType.Visiting]);

            TakePositions(_homeTeam);
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
            // Data needed:
                // Pitch Type
                // Hit Type
                // Is FlyOut+?
                // Is GroundOut(2)?
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