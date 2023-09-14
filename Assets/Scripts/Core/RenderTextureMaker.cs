using TMPro;
using UnityEngine;

namespace FibDev.Core
{
    public class RenderTextureMaker : MonoBehaviour
    {
        private RenderTexture _renderTexture;
        private Camera _camera;

        [SerializeField] private TMP_Text jerseyName;
        [SerializeField] private TMP_Text jerseyNumber;

        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();

            _renderTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            _camera.targetTexture = _renderTexture;
        }

        public RenderTexture GenerateRenderTexture(string pNumber, string pName)
        {
            jerseyName.text = pName;
            jerseyNumber.text = pNumber;

            return _renderTexture;
        }

    }
}
