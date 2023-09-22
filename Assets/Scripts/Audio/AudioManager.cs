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

                SetSourceValues(sound.source, sound);
            }
        }

        public static void SetSourceValues(AudioSource source, Sound sound)
        {
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
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

        public Sound FindSound(string pName)
        {
            return sounds.First(iSound => iSound.clip.name == pName);
        }

        public void Play(string pName)
        {
            var sound = FindSound(pName);

            Debug.Log($"Playing {sound.name}");
            sound.source.Play();
        }
    }
}