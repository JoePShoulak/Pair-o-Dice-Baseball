using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Decorator : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private TMP_Text nameText;
        
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
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Primary, pPrimary);
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Secondary, pSecondary);
        }
    }
}
