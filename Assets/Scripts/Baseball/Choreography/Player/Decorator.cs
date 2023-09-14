using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Player
{
    public class Decorator : MonoBehaviour
    {
        [SerializeField] private Transform textTransform;
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Renderer headRenderer;
        [SerializeField] private Renderer hatRenderer;
        [SerializeField] private Renderer billRenderer;

        private Transform _cam;
        
        private static readonly int Primary = Shader.PropertyToID("_Primary");
        private static readonly int Secondary = Shader.PropertyToID("_Secondary");

        private void Start()
        {
            _cam = GameObject.FindWithTag("MainCamera").transform;
        }

        public void SetJerseyNumber(string jerseyNumber)
        {
            numberText.text = jerseyNumber;
        }
        
        public void SetName(string playerName)
        {
            nameText.text = playerName;
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

        private void Update()
        {
            textTransform.LookAt(_cam.position);
            textTransform.Rotate(Vector3.up, 180f);
        }
    }
}
