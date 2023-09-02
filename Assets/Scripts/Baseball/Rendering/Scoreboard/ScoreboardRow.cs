using TMPro;
using UnityEngine;

using FibDev.Baseball.Records;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class ScoreboardRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text teamName;

        [SerializeField] private TMP_Text[] innings;
        
        [SerializeField] private TMP_Text runs;
        [SerializeField] private TMP_Text hits;
        [SerializeField] private TMP_Text errors;

        public void SetTotal(InningStats total)
        {
            runs.text = total.runs.ToString();
            hits.text = total.hits.ToString();
            errors.text = total.errors.ToString();
        }

        public void SetInning(int pInning, int pRuns)
        {
            innings[pInning].text = pRuns.ToString();
        }

        public void SetName(string pName)
        {
            teamName.text = pName;
        }

        public void Reset()
        {
            foreach (var inning in innings)
            {
                inning.text = "";
            }

            teamName.text = "";
            
            SetTotal(new InningStats());
        }
    }
}
