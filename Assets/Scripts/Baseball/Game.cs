using System;
using FibDev.Baseball.Records;
using UnityEngine;

namespace FibDev.Baseball
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Record record;

        // Serialized for debugging
        [SerializeField] private int inning;
        [SerializeField] private TeamType teamAtBat;
        [SerializeField] private int outs;
        [SerializeField] private Bases bases;
        public bool gameEnded;

        private TeamType FieldingTeam => teamAtBat == TeamType.Home ? TeamType.Visiting : TeamType.Home;

        private void Start()
        {
            ResetState();
        }

        public void ResetState() // for debug
        {
            bases.Reset();
            teamAtBat = TeamType.Visiting;

            record = new Record();

            inning = 1;
            outs = 0;

            gameEnded = false;
        }

        private void AdvanceInning()
        {
            outs = 0;
            bases.Reset();

            if (teamAtBat == TeamType.Visiting)
            {
                teamAtBat = TeamType.Home;
                return;
            }

            teamAtBat = TeamType.Visiting;
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
            var homeAtBatAndWinning = teamAtBat == TeamType.Home && record.LeadingTeam == TeamType.Home;
            var visitorsAtBatAndWinning = teamAtBat == TeamType.Visiting && record.LeadingTeam == TeamType.Visiting;

            if (inning > 8 && homeAtBatAndWinning) EndGame();
            else if (inning > 9 && visitorsAtBatAndWinning) EndGame();
        }

        private void EndGame()
        {
            gameEnded = true;
            Debug.Log($"{teamAtBat} Won!");
        }

        private void HandleAction(Operation bAction)
        {
            switch (bAction)
            {
                case Operation.Baseman3rdRunsHome:
                    if (!bases.third.runnerOn) break;
                    bases.third.runnerOn = false;
                    record.Add(inning, teamAtBat, RecordType.Run);
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
                    record.Add(inning, teamAtBat, RecordType.Error);
                    break;
                case Operation.OutAtSecond:
                    bases.second.runnerOn = false;
                    outs++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

        // private void LogPlay(Play _event)
        // {
        //     foreach (var bAction in _event.actions) Debug.Log(bAction);
        // }
    }
}