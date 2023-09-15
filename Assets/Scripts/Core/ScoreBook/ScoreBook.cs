using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FibDev.Core.ScoreBook
{
    public class ScoreBook : MonoBehaviour
    {
        private List<TMP_Text> _recordTMPs;
        
        private void Start()
        {
            _recordTMPs = GetComponentsInChildren<TMP_Text>().ToList();
            LoadAllRecords();
        }

        public static string ComposeRecord(string awayName, string homeName, int awayScore, int homeScore)
        {
            var date = DateTime.Now;
            var month = date.Month;
            var day = date.Day;
            
            return $"{awayName} @ {homeName}: {awayScore}-{homeScore} ({month}/{day})";
        }

        public void AddRecord(string recordText)
        {
            var bottomRecord = GetComponentsInChildren<Transform>().Last();
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
            return GetComponentsInChildren<TMP_Text>().Select(tmp => tmp.text).ToArray();
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
                GetComponentsInChildren<TMP_Text>()[i].text = records[i];
            }
        }
    }
}