using FibDev.Baseball.Choreography.Choreographer;
using FibDev.Core;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
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
            rollButton.interactable = false;
            camButton.interactable = false;
        }
        
        public void Exit()
        {
            _cam.LerpTo(_cam.start, 2f, () => OverlayManager.Instance.mainMenu.SetActive(true));
            rollButton.interactable = false;
            choreographer.TearDownGame();
            gameObject.SetActive(false);
        }
    }
}
