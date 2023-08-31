using System;
using UnityEngine;

namespace FibDev.Baseball
{
    public class Game : MonoBehaviour
    {

        // Serialized for debugging
        [SerializeField] private int inning;
        [SerializeField] private TeamType battingTeamType;
        [SerializeField] private int outs;
        [SerializeField] private Bases bases;
        [SerializeField] private int visitingScore;
        [SerializeField] private int homeScore;
        public bool gameEnded;

        private void Start()
        {
            ResetState();
        }

        public void ResetState() // for debug
        {
            bases.Reset();
            battingTeamType = TeamType.Visiting;
            
            inning = 1;
            outs = 0;
            homeScore = 0;
            visitingScore = 0;
            
            gameEnded = false;
        }

        private void AdvanceInning()
        {
            outs = 0;
            bases.Reset();

            if (battingTeamType == TeamType.Visiting)
            {
                battingTeamType = TeamType.Home;
                return;
            }

            battingTeamType = TeamType.Visiting;
            inning++;
        }

        public void NextPlay()
        {
            var bPlay = Play.Random();
            Debug.Log(bPlay.name);
            // LogPlay(bPlay);

            foreach (var bAction in bPlay.actions) HandleAction(bAction);

            CheckForGameEnded();
            if (outs >= 3 && !gameEnded) AdvanceInning();
        }

        private void CheckForGameEnded()
        {
            if (battingTeamType == TeamType.Home && inning > 8 && homeScore > visitingScore)
            {
                gameEnded = true;
                Debug.Log("Home Won!");
            }
            else if (battingTeamType == TeamType.Visiting && inning > 9 && visitingScore > homeScore)
            {
                gameEnded = true;
                Debug.Log("Visiting Won!");
            }
        }

        private void HandleAction(Operation bAction)
        {
            switch (bAction)
            {
                case Operation.Baseman3rdRunsHome:
                    if (!bases.third.runnerOn) break;
                    bases.third.runnerOn = false;
                    ScoreRun();
                    break;
                case Operation.Baseman2ndRunsThird:
                    if (!bases.second.runnerOn) break;
                    bases.second.runnerOn = false;
                    bases.third.runnerOn = true;
                    break;
                case Operation.Baseman1stRunsSecond:
                    if (!bases.first.runnerOn) break;
                    bases.first.runnerOn = false;
                    bases.second.runnerOn = true;
                    break;
                case Operation.BatterRunsFirst:
                    bases.first.runnerOn = true;
                    break;
                // TODO: Need to implement all of this
                case Operation.BatterHitBall:
                    break;
                case Operation.BatterMissBall:
                    outs++;
                    break;
                case Operation.PitcherThrowStrike:
                    break;
                case Operation.Cleanup:
                    break;
                case Operation.PitcherThrowsBall:
                    break;
                case Operation.PitcherHitsPlayer:
                    break;
                case Operation.BasemenAdvanceIfForced:
                    break;
                case Operation.FielderCatchesBall:
                    outs++;
                    break;
                case Operation.FielderCollectsBall:
                    break;
                case Operation.FielderBobblesBall:
                    break;
                case Operation.OutAtSecond:
                    bases.second.runnerOn = false;
                    outs++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

        private void ScoreRun()
        {
            if (battingTeamType == TeamType.Home) homeScore++;
            else if (battingTeamType == TeamType.Visiting) visitingScore++;
        }

        // private void LogPlay(Play _event)
        // {
        //     foreach (var bAction in _event.actions) Debug.Log(bAction);
        // }
    }
}