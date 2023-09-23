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
        [Range(-2f, 2f)] public float pitch = 1f;
        public AudioMixerGroup audioMixerGroup;
        public bool loop;
        public float offset;
    }
}