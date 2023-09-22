using System;
using UnityEngine;
using UnityEngine.Audio;

namespace FibDev.Audio
{
    [Serializable]
    public class Sound
    {
        public AudioClip clip;

        [HideInInspector] public AudioSource source;
        [HideInInspector] public string name;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public AudioMixerGroup audioMixerGroup;
    }
}