using FibDev.Baseball.Choreography.Choreographer;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Plays;
using FibDev.UI;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public class EventHandler : MonoBehaviour
    {
        private Engine _engine;
        private bool readyForRoll;
        [SerializeField] private DieCollection _dieCollection;
        private Choreographer _choreographer;
        private GameOverlayUI gameOverlay;

        private void Start()
        {
            _engine = GetComponent<Engine>();
            _choreographer = GetComponent<Choreographer>();
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
            if (!readyForRoll) return;
            
            if (gameOverlay.autoRun.isOn && !_dieCollection.isRolling)
            {
                gameOverlay.Roll();
            }
        }

        private void HandleRoll(int pObj)
        {
            if (!readyForRoll) return;
            if (_choreographer.gameEnded) return;

            readyForRoll = false;

            _engine.NextPlay(Play.Random());
        }
    }
}