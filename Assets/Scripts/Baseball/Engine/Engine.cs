using System.Collections.Generic;
using FibDev.Baseball.Choreography.Choreographer;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;
using FibDev.Baseball.Rendering.Scoreboard;
using FibDev.Baseball.Teams;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public class Engine : MonoBehaviour
    {
        [SerializeField] public Record record;

        // Serialized for debugging
        [SerializeField] public int inning;
        [SerializeField] public TeamType teamAtBat;
        [SerializeField] private int outs;
        [SerializeField] private Bases.Bases bases;
        public bool gameEnded;

        [SerializeField] private Board scoreboard;
        [SerializeField] public Choreographer choreographer;

        public Choreographer Choreographer => choreographer;

        public Bases.Bases Bases => bases;

        private void Start()
        {
            ResetState();
        }

        public void StartGame(Dictionary<TeamType, Team> teams)
        {
            // teams[0].Log();
            // teams[1].Log();
            scoreboard.SetNames(teams[TeamType.Home].name, teams[TeamType.Visiting].name);
            Choreographer.SetupGame(teams);
        }

        public void AddOut() => outs++;

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
            var bPlay = play ?? Play.Random();
            Debug.Log(bPlay.name);

            foreach (var bAction in bPlay.actions) ActionHandler.HandleAction(this, bAction);
            scoreboard.Display(record);
            Choreographer.RunPlay();

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

    }
}