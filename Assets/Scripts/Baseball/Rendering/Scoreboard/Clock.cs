using System;
using UnityEngine;

namespace FibDev.Baseball.Rendering.Scoreboard
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private Transform hourHand;
        [SerializeField] private Transform minuteHand;
        [SerializeField] private Transform secondHand;

        private void Update()
        {
            var millis = DateTime.Now.Millisecond;
            var seconds = DateTime.Now.Second + millis / 1000f;
            var minutes = DateTime.Now.Minute + seconds / 60f;
            var hours = DateTime.Now.Hour + minutes / 60f;
            
            hourHand.eulerAngles = new Vector3(0, 0, -hours * 30);
            minuteHand.eulerAngles = new Vector3(0, 0, -minutes * 6);
            secondHand.eulerAngles = new Vector3(0, 0, -seconds * 6);
        }
    }
}
