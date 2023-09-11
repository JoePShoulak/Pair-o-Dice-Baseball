using FibDev.Core;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using UnityEngine;

namespace FibDev.UI
{
    public class GameOverlayUI : MonoBehaviour
    {
        private CameraMovement _cam; // Cached
        private bool _atScoreboard;
        [SerializeField] private DieCollection _dieCollection;
        
        private void Start()
        {
            _cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        }

        public void ToggleCamera()
        {
            _cam.LerpTo(_atScoreboard ? _cam.stadium : _cam.scoreboard, 2f);
            _atScoreboard = !_atScoreboard;
        }

        public void Roll()
        {
            _dieCollection.Roll();
        }
    }
}
