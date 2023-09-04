using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Decorator : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Renderer hatRenderer;
        [SerializeField] private Renderer billRenderer;
        
        private static readonly int Primary = Shader.PropertyToID("_Primary");
        private static readonly int Secondary = Shader.PropertyToID("_Secondary");

        public void SetJerseyNumber(string jerseyNumber)
        {
            numberText.text = jerseyNumber;
        }
        
        public void SetName(string playerName)
        {
            nameText.text = playerName;
        }
        
        public void SetColor(Color pPrimary, Color pSecondary)
        {
            var material = gameObject.GetComponent<MeshRenderer>().material;
            material.SetColor(Primary, pPrimary);
            material.SetColor(Secondary, pSecondary);
            hatRenderer.material.color = pPrimary;
            billRenderer.material.color = pSecondary;
        }
    }
}
