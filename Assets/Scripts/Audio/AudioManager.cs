using System;
using System.Linq;
using FibDev.Core.ScoreBook;
using UnityEngine;
using UnityEngine.Audio;

namespace FibDev.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixer mixer;
        public Sound[] sounds;

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;

            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.name = sound.clip.name;

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
            }
        }

        private void Start()
        {
            SetVolume("Music", DataManager.GetMusicVolume());
            SetVolume("Ambient", DataManager.GetAmbientVolume());
            SetVolume("SoundFX", DataManager.GetSoundFXVolume());
        }

        public void SetVolume(string group, float value)
        {
            mixer.SetFloat($"{group}Volume", value);
        }

        public void Play(string pName)
        {
            var sound = sounds.First(iSound => iSound.name == pName);

            Debug.Log($"Playing {sound.name}");
            sound.source.Play();
        }
    }
}