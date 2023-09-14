using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI.Score_Overlay
{
    public class ScoreBox : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _runs;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _runs = GetComponentInChildren<TMP_Text>();
        }

        public void SetColor(Color pColor)
        {
            Debug.Log(_image);
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
