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
            var options = new List<Play> { Play.Single, Play.Strikeout };

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
        }

        private void HandleAction(BaseballPlays.Action bAction)
        {
            switch (bAction)
            {
                case BaseballPlays.Action.Baseman3rdRunsHome:
                    if (!bases.third.runnerOn) break;
                    bases.third.runnerOn = false;
                    ScoreRun();
                    break;
                case BaseballPlays.Action.Baseman2ndRunsThird:
                    if (!bases.second.runnerOn) break;
                    bases.second.runnerOn = false;
                    bases.third.runnerOn = true;
                    break;
                case BaseballPlays.Action.Baseman1stRunsSecond:
                    if (!bases.first.runnerOn) break;
                    bases.first.runnerOn = false;
                    bases.second.runnerOn = true;
                    break;
                case BaseballPlays.Action.BatterRunsFirst:
                    bases.first.runnerOn = true;
                    break;
                case BaseballPlays.Action.BatterHitBall:
                    break;
                case BaseballPlays.Action.BatterMissBall:
                    break;
                case BaseballPlays.Action.PitcherThrowStrike:
                    break;
                case BaseballPlays.Action.Cleanup:
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