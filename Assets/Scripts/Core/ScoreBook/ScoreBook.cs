using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FibDev.Core.ScoreBook
{
    public class ScoreBook : MonoBehaviour
    {
        private IEnumerable<Transform> Records => GetComponentsInChildren<Transform>().ToList();
        private List<TMP_Text> _recordTMPs;
        
        private void Start()
        {
            LoadAllRecords();
            _recordTMPs = GetComponentsInChildren<TMP_Text>().ToList();
        }

        public static string ComposeRecord(string awayName, string homeName, int awayScore, int homeScore)
        {
            return $"{awayName} @ {homeName}: {awayScore}-{homeScore}";
        }

        public void AddRecord(string recordText)
        {
            var bottomRecord = Records.Last();
            bottomRecord.SetAsFirstSibling();
            bottomRecord.GetComponent<TMP_Text>().text = recordText;

            SaveAllRecords();
        }

        public void Clear()
        {
            foreach (var tmp in _recordTMPs)
            {
                tmp.text = "";
            }
        }

        private string[] GetRecords()
        {
            return _recordTMPs.Select(tmp => tmp.text).ToArray();
        }

        public void SaveAllRecords()
        {
            var dataToSave = new ScoreData(GetRecords());
            
            DataManager.SaveData(dataToSave);
        }

        public void LoadAllRecords()
        {
            var records = DataManager.LoadData().records;
            if (records == null) return;
            
            for (var i = 0; i < _recordTMPs.Count; i++)
            {
                _recordTMPs[i].text = records[i];
            }
        }
    }
}