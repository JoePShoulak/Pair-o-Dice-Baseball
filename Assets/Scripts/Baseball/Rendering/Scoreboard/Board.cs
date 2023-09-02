using UnityEngine;

using FibDev.Baseball.Records;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private ScoreboardRow visitorScoreboardRow;
        [SerializeField] private ScoreboardRow homeScoreboardRow;

        public void SetNames(string homeName, string visitorName)
        {
            homeScoreboardRow.SetName(homeName);
            visitorScoreboardRow.SetName(visitorName);
        }

        public void Display(Record record)
        {
            for (var i = 0; i < record.innings.Count; i++)
            {
                visitorScoreboardRow.SetInning(i, record.innings[i].visitorInningStats.runs);
                homeScoreboardRow.SetInning(i, record.innings[i].homeInningStats.runs);
            }

            visitorScoreboardRow.SetTotal(record.visitorTotal);
            homeScoreboardRow.SetTotal(record.homeTotal);
        }

        public void Reset()
        {
            visitorScoreboardRow.Reset();
            homeScoreboardRow.Reset();
        }
    }
}