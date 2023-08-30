using System;
using System.Collections.Generic;
using UnityEngine;
using static FibDev.Baseball.BaseballPlays;

namespace FibDev.Baseball
{
    public class BaseballGame : MonoBehaviour
    {
        private enum Team
        {
            Home,
            Visiting
        }

        // Serialized for debugging
        [SerializeField] private int inning;
        [SerializeField] private Team battingTeam;
        [SerializeField] private int outs;
        [SerializeField] private Bases bases;
        [SerializeField] private int visitingScore;
        [SerializeField] private int homeScore;
        public bool gameEnded;
        [SerializeField] private bool homeWon;
        [SerializeField] private bool visitingWon;

        public Bases GetBases()
        {
            return bases;
        }

        private void Start()
        {
            ResetState();
        }

        public void ResetState() // for debug
        {
            battingTeam = Team.Visiting;
            inning = 1;
            outs = 0;
            bases.Reset();
            homeScore = 0;
            visitingScore = 0;
            gameEnded = false;
            homeWon = false;
            visitingWon = false;
        }

        private void AdvanceInning()
        {
            outs = 0;
            bases.Reset();

            if (battingTeam == Team.Visiting)
            {
                battingTeam = Team.Home;
                return;
            }

            battingTeam = Team.Visiting;
            inning++;
        }

        private Play RandomPlay()
        {
            var options = new List<Play>
            {
                Play.Single,
                Play.Double,
                Play.Triple,
                Play.HomeRun,
                Play.Error,

                Play.StrikeOut,
                Play.FlyOut,
                Play.FlyOutPlus,
                Play.PopOut,
                Play.LineOut,
                Play.FoulOut
            };

            return options[new System.Random().Next(0, options.Count)];
        }

        public void NextPlay()
        {
            var bPlay = RandomPlay();
            Debug.Log(bPlay.name);
            // LogPlay(bPlay);

            foreach (var bAction in bPlay.actions) HandleAction(bAction);

            if (bPlay.type == PlayType.Out)
            {
                outs++;
                if (outs >= 3) AdvanceInning();
            }

            CheckForGameEnded();
        }

        private void CheckForGameEnded()
        {
            if (battingTeam == Team.Home && inning > 8 && homeScore > visitingScore)
            {
                gameEnded = true;
                homeWon = true;
            }
            else if (battingTeam == Team.Visiting && inning > 9 && visitingScore > homeScore)
            {
                gameEnded = true;
                visitingWon = true;
            }
        }

        private void HandleAction(BaseballAction bAction)
        {
            switch (bAction)
            {
                case BaseballAction.Baseman3rdRunsHome:
                    if (!bases.third.runnerOn) break;
                    bases.third.runnerOn = false;
                    ScoreRun();
                    break;
                case BaseballAction.Baseman2ndRunsThird:
                    if (!bases.second.runnerOn) break;
                    bases.second.runnerOn = false;
                    bases.third.runnerOn = true;
                    break;
                case BaseballAction.Baseman1stRunsSecond:
                    if (!bases.first.runnerOn) break;
                    bases.first.runnerOn = false;
                    bases.second.runnerOn = true;
                    break;
                case BaseballAction.BatterRunsFirst:
                    bases.first.runnerOn = true;
                    break;
                case BaseballAction.BatterHitBall:
                    break;
                case BaseballAction.BatterMissBall:
                    break;
                case BaseballAction.PitcherThrowStrike:
                    break;
                case BaseballAction.Cleanup:
                    break;
                case BaseballAction.PitcherThrowsBall:
                    break;
                case BaseballAction.PitcherHitsPlayer:
                    break;
                case BaseballAction.BasemenAdvanceIfForced:
                    break;
                case BaseballAction.FielderCatchesBall:
                    break;
                case BaseballAction.FielderCollectsBall:
                    break;
                case BaseballAction.FielderBobblesBall:
                    break;
                case BaseballAction.OutAtFirst:
                    break;
                case BaseballAction.OutAtSecond:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

        private void ScoreRun()
        {
            if (battingTeam == Team.Home) homeScore++;
            else if (battingTeam == Team.Visiting) visitingScore++;
        }

        // private void LogPlay(Play _event)
        // {
        //     foreach (var bAction in _event.actions) Debug.Log(bAction);
        // }
    }
}