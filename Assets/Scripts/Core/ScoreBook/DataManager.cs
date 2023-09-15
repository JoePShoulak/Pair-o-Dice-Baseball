using System;
using System.Collections.Generic;
using UnityEngine;

namespace FibDev.Core.ScoreBook
{
    public static class DataManager
    {
        
        private static string GetRecordName(int i)
        {
            return $"record{i}";
        }
        
        public static void SaveData(ScoreData data)
        {
            var recordCount = data.records.Length;
            
            for (var i = 0; i < recordCount; i++)
            {
                var record= data.records[i];
                PlayerPrefs.SetString(GetRecordName(i), record);
            }
            
            PlayerPrefs.SetInt("recordCount", data.records.Length);
        }

        public static ScoreData LoadData()
        {
            var recordCount = PlayerPrefs.GetInt("recordCount", 0);
            if (recordCount == 0) return new ScoreData();
            
            List<string> rawRecords = new();
            
            for (var i = 0; i < recordCount; i++)
            {
                var record = PlayerPrefs.GetString(GetRecordName(i));
                rawRecords.Add(record);
            }

            return new ScoreData(rawRecords.ToArray());
        }
    }
}
