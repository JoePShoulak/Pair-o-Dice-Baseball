using System;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Plays;
using FibDev.UI;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.Baseball.Engine
{
    public class EventHandler : MonoBehaviour
    {
        private Engine _engine;
        private bool readyForRoll;
        [SerializeField] private DieCollection _dieCollection;
        private GameOverlayUI gameOverlay;

        private void Start()
        {
            _engine = GetComponent<Engine>();
            _engine.ResetState();
            gameOverlay = OverlayManager.Instance.gameOverlay.GetComponent<GameOverlayUI>();

            TeamSelectUI.OnTeamsSelected += _engine.StartGame;
            Dice.RollProcessor.OnRollProcessed += HandleRoll;
            Movement.OnMovementEnd += _ =>
            {
                readyForRoll = true;
                gameOverlay.rollButton.interactable = true;
                gameOverlay.autoRun.interactable = true;
                Debug.Log("Ready for roll");
            };
        }

        private void Update()
        {
            if (gameOverlay.autoRun.isOn && readyForRoll && !_dieCollection.isRolling)
            {
                gameOverlay.Roll();
            }
        }

        private void HandleRoll(int pObj)
        {
            if (!readyForRoll) return;

            readyForRoll = false;

            _engine.NextPlay(Play.Random());
        }
    }
}