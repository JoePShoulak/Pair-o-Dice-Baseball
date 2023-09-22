using System;
using System.Linq;
using FibDev.Audio;
using FibDev.Core.Lighting;
using FibDev.Core.ScoreBook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private LightingController lighting;

        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider ambientSlider;
        [SerializeField] private Slider soundFXSlider;
        
        [SerializeField] private TMP_Dropdown dayDropdown;

        private void Start()
        {
            var optionsList = dayDropdown.options.Select(option => option.text).ToList();
            var dayMode = DataManager.GetDayMode();
            
            dayDropdown.value = optionsList.IndexOf(dayMode);

            masterSlider.value = DataManager.GetVolume("Master");
            musicSlider.value = DataManager.GetVolume("Music");
            ambientSlider.value = DataManager.GetVolume("Ambient");
            soundFXSlider.value = DataManager.GetVolume("SoundFX");
        }

        public void SetMasterVolume(Slider slider)
        {
            SetVolume("Master", slider);
        }
        
        public void SetMusicVolume(Slider slider)
        {
            SetVolume("Music", slider);
        }
        
        public void SetAmbientVolume(Slider slider)
        {
            SetVolume("Ambient", slider);
        }
        
        public void SetSoundFXVolume(Slider slider)
        {
            SetVolume("SoundFX", slider);
        }

        private static void SetVolume(string group, Slider slider)
        {
            var volume = slider.value;
            DataManager.SetVolume(group, volume);
            AudioManager.Instance.SetVolume(group, volume);
        }

        public void SetDayMode(TMP_Dropdown dropdown)
        {
            var dayMode = dropdown.options[dropdown.value].text;
            DataManager.SetDayMode(dayMode);
            lighting.UpdateLightMode();
        }

        public void ClearScores()
        {
            DataManager.DeleteAllScores();
            Debug.Log("Scores deleted");
        }

        public void Save()
        {
            gameObject.SetActive(false);
            OverlayManager.Instance.mainMenu.SetActive(true);
        }
    }
}
