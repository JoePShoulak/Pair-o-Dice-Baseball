using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FibDev.Core
{
    public class Lighting : MonoBehaviour
    {
        [SerializeField] private Light mainLight;
        [SerializeField] private Light posterLight;
        [SerializeField] private Renderer poster;
        
        [SerializeField] private List<Texture2D> dayPosters;
        [SerializeField] private List<Texture2D> nightPosters;
        
        private static bool Daytime => DateTime.Now.Hour > 7 && DateTime.Now.Hour < 19;
        
        private void Start()
        {
            if (Daytime) SetDayTime();
            else SetNightTime();
        }

        private void SetDayTime()
        {
            mainLight.intensity = 1f;
            posterLight.intensity = 0.4f;
            poster.material.mainTexture = RandomFrom(dayPosters);
        }

        private void SetNightTime()
        {
            const float nightRatio = 1 / 6f;
            
            mainLight.intensity = 1 * nightRatio;
            posterLight.intensity = 0.4f * nightRatio;
            
            poster.material.mainTexture = RandomFrom(nightPosters);
        }

        private static T RandomFrom<T>(IReadOnlyList<T> listOfItems)
        {
            if (listOfItems.Count == 0) throw new Exception("Empty List");

            var randomIndex = Random.Range(0, listOfItems.Count);

            return listOfItems[randomIndex];
        }
    }
}
