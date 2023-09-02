using System;
using System.Collections.Generic;
using FibDev.Baseball.Choreography;
using UnityEngine;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;
using FibDev.Baseball.Rendering.Scoreboard;
using FibDev.Baseball.Teams;
using FibDev.UI;

namespace FibDev.Baseball
{
    public class Engine : MonoBehaviour
    {
        [SerializeField] private Record record;

        // Serialized for debugging
        [SerializeField] private int inning;
        [SerializeField] private TeamType teamAtBat;
        [SerializeField] private int outs;
        [SerializeField] private Bases.Bases bases;
        public bool gameEnded;
        private bool readyForRoll;

        [SerializeField] private Board scoreboard;
        [SerializeField] private Choreographer choreographer;

        private void Start()
        {
            ResetState();

            TeamSelectUI.OnTeamsSelected += StartGame;
            Dice.RollProcessor.OnRollProcessed += HandleRoll;
            choreographer.OnMovementEnd += () =>
            {
                readyForRoll = true;
                Debug.Log("Ready for roll");
            };
        }

        private void HandleRoll(int pObj)
        {
            // if (!readyForRoll) return;

            NextPlay(Play.Random());
        }

        private void StartGame(List<Team> teams)
        {
            // teams[0].Log();
            // teams[1].Log();
            choreographer.SetupGame(teams);
        }

        public void ResetState() // for debug
        {
            bases.Reset();
            teamAtBat = TeamType.Visiting;

            record = new Record();

            inning = 1;
            outs = 0;

            gameEnded = false;

            scoreboard.Reset();
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

        public void NextPlay(Play play = null)
        {
            readyForRoll = false;
            var bPlay = play ?? Play.Random();
            Debug.Log(bPlay.name);

            foreach (var bAction in bPlay.actions) HandleAction(bAction);
            scoreboard.Display(record);

            if (outs >= 3 && !gameEnded) AdvanceInning();

            if (CheckForGameEnded()) EndGame();
        }

        private bool CheckForGameEnded()
        {
            if (record.LeadingTeam == null) return false;
            
            var homeAtBatAndWinning = teamAtBat == TeamType.Home && record.LeadingTeam == TeamType.Home;
            var visitorsAtBatAndWinning = teamAtBat == TeamType.Visiting && record.LeadingTeam == TeamType.Visiting;

            return (inning > 8 && homeAtBatAndWinning) || (inning > 9 && visitorsAtBatAndWinning);
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
                    record.Add(inning, teamAtBat, StatType.Run);
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
                    record.Add(inning, teamAtBat, StatType.Error);
                    break;
                case Operation.OutAtSecond:
                    bases.second.runnerOn = false;
                    outs++;
                    break;
                case Operation.RecordHit:
                    record.Add(inning, teamAtBat, StatType.Hit);
                    break;
                case Operation.Cleanup:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }
    }
}