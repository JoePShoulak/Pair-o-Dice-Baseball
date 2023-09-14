using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI.Score_Overlay
{
    public class ScoreBox : MonoBehaviour
    {
        private Image _image;
        private TMP_Text _runs;

        private void Start()
        {
            _image = GetComponent<Image>();
            _runs = GetComponentInChildren<TMP_Text>();
        }

        public void SetColor(Color pColor)
        {
            _image.color = pColor;
        }

        public void SetScore(int score)
        {
            _runs.text = score.ToString();
        }

        public void Reset()
        {
            SetColor(Color.white);
            SetScore(0);
        }
    }
}
