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

            masterSlider.value = DataManager.GetMasterVolume();
            musicSlider.value = DataManager.GetMusicVolume();
            ambientSlider.value = DataManager.GetAmbientVolume();
            soundFXSlider.value = DataManager.GetSoundFXVolume();
        }

        public void SetMasterVolume(Slider slider)
        {
            var volume = slider.value;
            DataManager.SetMasterVolume(volume);
            AudioManager.Instance.SetVolume("Master", volume);
        }
        
        public void SetMusicVolume(Slider slider)
        {
            var volume = slider.value;
            DataManager.SetMusicVolume(volume);
            AudioManager.Instance.SetVolume("Music", volume);
        }
        
        public void SetAmbientVolume(Slider slider)
        {
            var volume = slider.value;
            DataManager.SetAmbientVolume(volume);
            AudioManager.Instance.SetVolume("Ambient", volume);
        }
        
        public void SetSoundFXVolume(Slider slider)
        {
            var volume = slider.value;
            DataManager.SetSoundFXVolume(volume);
            AudioManager.Instance.SetVolume("SoundFX", volume);
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
