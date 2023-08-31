using FibDev.Baseball.Records;
using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Row : MonoBehaviour
    {
        public TMP_Text name;

        public TMP_Text[] innings;
        
        public TMP_Text runs;
        public TMP_Text hits;
        public TMP_Text errors;

        public void SetTotal(Stats total)
        {
            runs.text = total.runs.ToString();
            hits.text = total.hits.ToString();
            errors.text = total.errors.ToString();
        }

        public void Reset()
        {
            foreach (var inning in innings)
            {
                inning.text = "";
            }
            
            SetTotal(new Stats());
        }
    }
}
