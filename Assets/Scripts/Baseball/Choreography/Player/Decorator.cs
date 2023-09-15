using FibDev.Baseball.Player;
using FibDev.Core;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Decorator : MonoBehaviour
    {
        [SerializeField] private Renderer headRenderer;
        [SerializeField] private Renderer hatRenderer;
        [SerializeField] private Renderer billRenderer;
        [SerializeField] private Renderer jerseyRenderer;
        [SerializeField] private RenderTextureMaker renderTextureMaker;

        private static readonly int Primary = Shader.PropertyToID("_Primary");
        private static readonly int Secondary = Shader.PropertyToID("_Secondary");
        private static readonly int Message = Shader.PropertyToID("_Message");

        [SerializeField] private float scaleMin = 0.9f;

        public void SetJerseyInfo(string jerseyNumber, string jerseyName)
        {
            var renderTexture = renderTextureMaker.GenerateRenderTexture(jerseyNumber, jerseyName);

            jerseyRenderer.material.SetTexture(Message, renderTexture);
        }

        private float GetHeightScale(HeightType height)
        {
            if (height == HeightType.Medium) return 1f;

            return height == HeightType.Short ? scaleMin : 1 / scaleMin;
        }

        private float GetWeightScale(WeightType weight)
        {
            if (weight == WeightType.Medium) return 1f;

            return weight == WeightType.Thin ? scaleMin : 1 / scaleMin;
        }

        public void SetSize(WeightType weight, HeightType height)
        {
            var heightScale = GetHeightScale(height);
            var weightScale = GetWeightScale(weight);

            transform.localScale = new Vector3(weightScale, heightScale, weightScale);
            jerseyRenderer.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void SetColor(Color pPrimary, Color pSecondary, Color pSkin)
        {
            var material = gameObject.GetComponent<MeshRenderer>().material;
            
            material.SetColor(Primary, pPrimary);
            material.SetColor(Secondary, pSecondary);
            
            hatRenderer.material.color = pPrimary;
            billRenderer.material.color = pSecondary;
            headRenderer.material.color = pSkin;
        }
    }
}