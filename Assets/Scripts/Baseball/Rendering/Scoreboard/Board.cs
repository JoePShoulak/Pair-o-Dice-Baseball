using UnityEngine;

using TMPro;
using Random = UnityEngine.Random;

using FibDev.Baseball.Records;
namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private ScoreboardRow visitorScoreboardRow;
        [SerializeField] private ScoreboardRow homeScoreboardRow;
        [SerializeField] private TMP_Text stadiumName;
        [SerializeField] private TMP_Text attendance;

        public void SetNames(string homeName, string visitorName)
        {
            homeScoreboardRow.SetName(homeName);
            visitorScoreboardRow.SetName(visitorName);
        }

        public void SetStadiumName(string pName)
        {
            stadiumName.text = $"{pName} Stadium";
        }

        public void SetAttendance()
        {
            var randomNum = Random.Range(10000, 30000).ToString("n0");
            attendance.text = "Tonight's Attendance: " + randomNum;
        }

        public void Display(Record record, bool bottomOfInning)
        {
            for (var i = 0; i < record.innings.Count; i++)
            {
                visitorScoreboardRow.SetInning(i, record.innings[i].visitorInningStats.runs);
                if (bottomOfInning) homeScoreboardRow.SetInning(i, record.innings[i].homeInningStats.runs);
            }

            visitorScoreboardRow.SetTotal(record.visitorTotal);
            homeScoreboardRow.SetTotal(record.homeTotal);
        }

        public void Reset()
        {
            visitorScoreboardRow.ResetRow(true);
            homeScoreboardRow.ResetRow();
        }
    }
}