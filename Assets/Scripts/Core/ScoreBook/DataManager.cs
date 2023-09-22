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
        
        public static void SaveScores(ScoreData data)
        {
            var recordCount = data.records.Length;
            
            for (var i = 0; i < recordCount; i++)
            {
                var record= data.records[i];
                PlayerPrefs.SetString(GetRecordName(i), record);
            }
            
            PlayerPrefs.SetInt("recordCount", data.records.Length);
        }

        public static ScoreData LoadScores()
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

        public static void DeleteAllScores()
        {
            var recordCount = PlayerPrefs.GetInt("recordCount", 0);
            if (recordCount == 0) return;
            
            for (var i = 0; i < recordCount; i++)
            {
                PlayerPrefs.DeleteKey(GetRecordName(i));
            }

            PlayerPrefs.SetInt("recordCount", 0);
        }

        public static void SetVolume(string group, float volume)
        {
            PlayerPrefs.SetFloat($"{group}Volume", volume);
        }

        public static float GetVolume(string group)
        {
            return PlayerPrefs.GetFloat($"{group}Volume", 0f);

        }

        public static void SetDayMode(string mode)
        {
            PlayerPrefs.SetString("DayMode", mode);
        }

        public static string GetDayMode()
        {
            return PlayerPrefs.GetString("DayMode", "Auto");
        }
    }
}
