using UnityEngine;

namespace FibDev.Core.Lighting
{
    public class EmissivesController : MonoBehaviour
    {
        private Color IntenseColor(Color color, float intensity)
        {
            var iR = color.r * intensity;
            var iG = color.g * intensity;
            var iB = color.b * intensity;

            return new Color(iR, iG, iB);
        }

        public void SetIntensity(float intensity)
        {
            var material = GetComponentInChildren<Renderer>().sharedMaterial;
            material.color = IntenseColor(Color.white, intensity);
        }
    }
}
