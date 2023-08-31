using System;
using UnityEngine;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private Transform minuteHand;
        [SerializeField] private Transform hourHand;

        private void Start()
        {
            var hours = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var seconds = DateTime.Now.Second;
            
            Debug.Log(hours);
            Debug.Log(minutes);
            Debug.Log(seconds);
        }
    }
}
