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

        public static void SetMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public static void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public static void SetAmbientVolume(float volume)
        {
            PlayerPrefs.SetFloat("AmbientVolume", volume);
        }

        public static void SetSoundFXVolume(float volume)
        {
            PlayerPrefs.SetFloat("SoundFXVolume", volume);
        }

        public static void SetDayMode(string mode)
        {
            PlayerPrefs.SetString("DayMode", mode);
        }
        
        public static float GetMasterVolume()
        {
            return PlayerPrefs.GetFloat("MasterVolume", 0f);
        }
        
        public static float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat("MusicVolume", 0f);
        }

        public static float GetAmbientVolume()
        {
            return PlayerPrefs.GetFloat("AmbientVolume", 0f);
        }

        public static float GetSoundFXVolume()
        {
            return PlayerPrefs.GetFloat("SoundFXVolume", 0f);
        }

        public static string GetDayMode()
        {
            return PlayerPrefs.GetString("DayMode", "Auto");
        }
    }
}
