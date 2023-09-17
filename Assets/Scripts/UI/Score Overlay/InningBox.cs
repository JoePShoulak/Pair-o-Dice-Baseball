using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI.Score_Overlay
{
    public class InningBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text inningText;
        [SerializeField] private Image arrow;

        private int inning = 1;
        private bool bottom;

        private void Start()
        {
            inningText = GetComponentInChildren<TMP_Text>();
        }

        public void Advance()
        {
            if (bottom) inning++;
            bottom = !bottom;
            
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            inningText.text = inning.ToString();
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, bottom ? 180f : 0f));
        }

        public void Reset()
        {
            inning = 1;
            bottom = false;
            
            UpdateGraphics();
        }
    }
}
