using System;
using UnityEngine;

namespace FibDev.Audio
{
    public class CrowdAudioManager : MonoBehaviour
    {
        [SerializeField] private float homeCrowdVolume = 1f;
        [SerializeField] private float visitorCrowdVolume = 1f;

        [SerializeField] private AudioSource homeCrowdSource;
        [SerializeField] private AudioSource visitorCrowdSource;

        private void Start()
        {
            homeCrowdSource.volume = homeCrowdVolume;
            visitorCrowdSource.volume = visitorCrowdVolume;
        }

        public void PlayHomePositive()
        {
            AudioManager.SetSourceValues(homeCrowdSource, AudioManager.Instance.FindSound("Crowd Positive"));
            AudioManager.SetSourceValues(visitorCrowdSource, AudioManager.Instance.FindSound("Crowd Negative"));

            PlayCrowdCheers();
        }

        public void PlayHomeNegative()
        {
            AudioManager.SetSourceValues(homeCrowdSource, AudioManager.Instance.FindSound("Crowd Negative"));
            AudioManager.SetSourceValues(visitorCrowdSource, AudioManager.Instance.FindSound("Crowd Positive"));

            PlayCrowdCheers();
        }

        private void PlayCrowdCheers()
        {
            homeCrowdSource.Play();
            visitorCrowdSource.Play();
        }
    }
}