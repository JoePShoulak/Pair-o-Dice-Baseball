using System;
using System.Collections.Generic;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Choreographer;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;
using FibDev.Baseball.Rendering.Scoreboard;
using FibDev.Baseball.Teams;
using FibDev.UI;
using FibDev.UI.Score_Overlay;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public class Engine : MonoBehaviour
    { 
        public Record record;
        public int inning;
        public TeamType teamAtBat;
        public bool gameEnded;

        public int outs;
        public Bases.Bases bases = new ();

        [SerializeField] private Board scoreboard;
        [HideInInspector] public Choreographer choreographer;
        [SerializeField] private Messager ballMessager;

        public Bases.Bases Bases => bases;
        private bool HomeAtBat => teamAtBat == TeamType.Home;
        private bool VisitorsAtBat => teamAtBat == TeamType.Visiting;

        private static ScoreOverlay ScoreOverlay =>
            OverlayManager.Instance.gameOverlay.GetComponent<GameOverlayUI>().ScoreOverlay;

        public event Action OnInningAdvance; 
        public event Action OnGameEnd; 

        private void Start()
        {
            ResetState();
            choreographer = GetComponent<Choreographer>();
        }

        public void StartGame(Dictionary<TeamType, Team> teams)
        {
            scoreboard.Reset();
            scoreboard.SetNames(teams[TeamType.Home].name, teams[TeamType.Visiting].name);
            choreographer.SetupGame(teams);
            
            ScoreOverlay.SetColors(teams[TeamType.Visiting].primary, teams[TeamType.Home].primary);
            ScoreOverlay.SetScores(0, 0);
        }

        public void AddOut() => outs++;

        public void ResetState()
        {
            if (bases == null) return;
            
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
            OnInningAdvance?.Invoke();
            outs = 0;
            bases.Reset();
            ScoreOverlay.SetBases(bases);
            ScoreOverlay.SetOuts(outs);

            if (VisitorsAtBat)
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
            ballMessager.UpdateMessage(bPlay.name);

            foreach (var operation in bPlay.actions)
            {
                OperationHandler.HandleOperation(this, operation);
            }
            

            
            choreographer.InitiateMovement(() => scoreboard.Display(record));

            if (outs >= 3 && !gameEnded) AdvanceInning();

            if (CheckForGameEnded()) EndGame();
        }

        private bool CheckForGameEnded()
        {
            if (record.LeadingTeam == null) return false;

            var homeWon = inning > 8 && HomeAtBat && record.HomeWinning;
            var visitorsWon = inning > 9 && VisitorsAtBat && record.VisitorsWinning;

            return homeWon || visitorsWon;
        }

        private void EndGame()
        {
            ScoreOverlay.Reset();
            gameEnded = true;
            OnGameEnd?.Invoke();
            Debug.Log($"{teamAtBat} Won!");
        }
    }
}