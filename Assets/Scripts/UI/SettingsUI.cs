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
        
        public void SetMusicVolume(Slider slider)
        {
            var musicVolume = slider.value;
            DataManager.SetMusicVolume(musicVolume);
        }
        
        public void SetAmbientVolume(Slider slider)
        {
            var ambientVolume = slider.value;
            DataManager.SetAmbientVolume(ambientVolume);
        }
        
        public void SetSoundFXVolume(Slider slider)
        {
            var soundFXVolume = slider.value;
            DataManager.SetSoundFXVolume(soundFXVolume);
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
