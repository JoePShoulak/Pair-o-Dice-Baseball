using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Choreography
{
    public class Decorator : MonoBehaviour
    {
        [SerializeField] private TMP_Text numberText;
        
        private static readonly int Primary = Shader.PropertyToID("_Primary");
        private static readonly int Secondary = Shader.PropertyToID("_Secondary");

        public void SetJerseyNumber(int number)
        {
            numberText.text = number.ToString();
        }
        
        public void SetColor(Color pPrimary, Color pSecondary)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Primary, pPrimary);
            gameObject.GetComponent<MeshRenderer>().material.SetColor(Secondary, pSecondary);
        }
    }
}
