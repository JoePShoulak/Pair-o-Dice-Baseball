using FibDev.Baseball.Records;
using UnityEngine;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Row visitorRow;
        [SerializeField] private Row homeRow;

        public void Display(Record record)
        {
            for (var i = 0; i < record.innings.Count; i++)
            {
                visitorRow.innings[i].text = record.innings[i].visitorStats.runs.ToString();
                homeRow.innings[i].text = record.innings[i].homeStats.runs.ToString();
            }

            visitorRow.SetTotal(record.visitorTotal);
            homeRow.SetTotal(record.homeTotal);
        }

        public void Reset()
        {
            visitorRow.Reset();
            homeRow.Reset();
        }
    }
}