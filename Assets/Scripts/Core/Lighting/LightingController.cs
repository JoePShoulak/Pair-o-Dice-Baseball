using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FibDev.Core.Lighting
{
    public class LightingController : MonoBehaviour
    {
        [SerializeField] private EmissivesController emissives;

        [SerializeField] private Light mainLight;
        [SerializeField] private Light posterLight;
        [SerializeField] private Renderer poster;

        [SerializeField] private List<Texture2D> dayPosters;
        [SerializeField] private List<Texture2D> nightPosters;

        private static bool Daytime => DateTime.Now.Hour > 7 && DateTime.Now.Hour < 19;
        private const float nightRatio = 1 / 6f;

        private void Start()
        {
            if (Daytime) SetDayTime();
            else SetNightTime();
        }

        private void SetDayTime()
        {
            poster.material.mainTexture = RandomFrom(dayPosters);
            posterLight.intensity = 1f;

            mainLight.intensity = 1.36f;
            emissives.SetIntensity(1f);
        }

        private void SetNightTime()
        {
            poster.material.mainTexture = RandomFrom(nightPosters);
            posterLight.intensity = 1f * nightRatio;

            mainLight.intensity = 1.36f * nightRatio;
            emissives.SetIntensity(5f);
        }

        private static T RandomFrom<T>(IReadOnlyList<T> listOfItems)
        {
            if (listOfItems.Count == 0) throw new Exception("Empty List");

            var randomIndex = Random.Range(0, listOfItems.Count);

            return listOfItems[randomIndex];
        }
    }
}