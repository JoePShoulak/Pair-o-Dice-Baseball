using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FibDev.Core
{
    public class ScoreBook : MonoBehaviour
    {
        private void Start()
        {
            LoadAllRecords();
        }

        public void AddRecord(string awayName, string homeName, int awayScore, int homeScore)
        {
            var record = $"{awayName} @ {homeName}: {awayScore}-{homeScore}";
            var bottomRecord = GetComponentsInChildren<Transform>().Last();
            
            bottomRecord.SetAsFirstSibling();
            bottomRecord.GetComponent<TMP_Text>().text = record;
            
            SaveAllRecords();
        }

        private void SaveAllRecords()
        {
            throw new NotImplementedException();
        }

        private void LoadAllRecords()
        {
            throw new NotImplementedException();
        }
    }
}
