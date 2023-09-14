using FibDev.Baseball.Choreography.Choreographer;
using FibDev.Core;
using FibDev.UI.Score_Overlay;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI
{
    public class GameOverlayUI : MonoBehaviour
    {
        private CameraMovement _cam; // Cached
        private bool _atScoreboard;
        [SerializeField] private DieCollection _dieCollection;
        public Button camButton;
        public Button rollButton;
        public Toggle autoRun;
        [SerializeField] private Choreographer choreographer;
        [SerializeField] private GameObject _diceFeed;
        public ScoreOverlay ScoreOverlay;
        
        private void Start()
        {
            _cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
            ScoreOverlay = GetComponentInChildren<ScoreOverlay>();
        }

        public void Reset()
        {
            _atScoreboard = false;
            rollButton.interactable = false;
            autoRun.interactable = false;
            _diceFeed.SetActive(true);
            ScoreOverlay.gameObject.SetActive(true);
        }

        public void ToggleCamera()
        {
            _cam.LerpTo(_atScoreboard ? _cam.stadium : _cam.scoreboard);
            _atScoreboard = !_atScoreboard;
            _diceFeed.SetActive(!_atScoreboard);
            ScoreOverlay.gameObject.SetActive(!_atScoreboard);
        }

        public void Roll()
        {
            _dieCollection.Roll();
            rollButton.interactable = false;
            camButton.interactable = false;
        }
        
        public void Exit()
        {
            _cam.LerpTo(_cam.start, 0.5f, () => OverlayManager.Instance.mainMenu.SetActive(true));
            Reset();
            choreographer.gameEnded = true;
            choreographer.TearDownGame();
            gameObject.SetActive(false);
        }
    }
}
