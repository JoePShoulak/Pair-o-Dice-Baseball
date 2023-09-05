using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Plays;
using FibDev.UI;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public class EventHandler : MonoBehaviour
    {
        private Engine _engine;
        private bool readyForRoll;

        private void Start()
        {
            _engine = GetComponent<Engine>();
            _engine.ResetState();

            TeamSelectUI.OnTeamsSelected += _engine.StartGame;
            Dice.RollProcessor.OnRollProcessed += HandleRoll;
            Movement.OnMovementEnd += _ =>
            {
                readyForRoll = true;
                Debug.Log("Ready for roll");
            };
        }

        private void HandleRoll(int pObj)
        {
            if (!readyForRoll) return;

            readyForRoll = false;
            _engine.NextPlay(Play.Random());
        }
    }
}