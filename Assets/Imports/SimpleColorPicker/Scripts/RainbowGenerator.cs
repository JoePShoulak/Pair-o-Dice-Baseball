using UnityEngine;
using UnityEngine.UI;

namespace Imports.SimpleColorPicker.Scripts
{
    public class RainbowGenerator : MonoBehaviour
    {
        public void Start()
        {
            var texture = new Texture2D(1, 128);

            for (var i = 0; i < texture.height; i++)
            {
                var color = Color.HSVToRGB((float)i / (texture.height - 1), 1f, 1f);
                texture.SetPixel(0, i, color);
            }

            texture.Apply();
            var size = new Rect(0f, 0f, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            GetComponent<Image>().sprite = Sprite.Create(texture, size, pivot, 100f);
        }
    }
}