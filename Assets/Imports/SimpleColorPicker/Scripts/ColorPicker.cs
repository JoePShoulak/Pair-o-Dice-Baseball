using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Imports.SimpleColorPicker.Scripts
{
	public class ColorPicker : MonoBehaviour
	{
		public Color color;
		public ColorMode colorMode;
		
		public Image gradient;
		public ColorSlider a;
		public InputField hex;
		public Image[] compareLook; // [0] is old color, [1] is new color.
		public Image transparencyLook;
		public Text mode;
		public GameObject rgbSliders;
		public GameObject hsvSliders;
		public bool locked;

		[HideInInspector] public Texture2D texture;
		[HideInInspector] public ColorSlider r;
		[HideInInspector] public ColorSlider g;
		[HideInInspector] public ColorSlider b;
		[HideInInspector] public ColorSlider h;
		[HideInInspector] public ColorSlider s;
		[HideInInspector] public ColorSlider v;

		private RectTransform _rectTransform;
		private ColorJoystick _colorJoystick;
		private Slider _hue;
		public event Action<Color> OnColorSelected;

		public void Start()
		{
			_rectTransform = gradient.GetComponent<RectTransform>();
			_colorJoystick = gradient.GetComponentInChildren<ColorJoystick>();
			_hue = gradient.GetComponentInChildren<Slider>();

			AssignSlidersFromChildren();

			texture = new Texture2D(128, 128) { filterMode = FilterMode.Point };
			gradient.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
			SetColor(color);
			compareLook[0].color = color;
		}

		private void AssignSlidersFromChildren()
		{
			var children = new List<ColorSlider>(rgbSliders.GetComponentsInChildren<ColorSlider>());
			children.AddRange(hsvSliders.GetComponentsInChildren<ColorSlider>());
			foreach (var child in children)
			{
				if (child.name == "R") r = child;
				else if (child.name == "G") g = child;
				else if (child.name == "B") b = child;
				else if (child.name == "H") h = child;
				else if (child.name == "S") s = child;
				else if (child.name == "V") v = child;
			}
		}

		public void Select()
		{
			compareLook[0].color = color;
			Debug.LogFormat("Color selected: {0}", color);
			OnColorSelected?.Invoke(color);
			Close();
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}

		public void SetColor(Color pColor, bool pPicker = true, bool pSliders = true, bool pHex = true, bool pHue = true)
		{
			Color.RGBToHSV(pColor, out var oH, out var oS, out var oV);
			SetColor(oS > 0 ? oH : h.Value, oS, oV, pColor.a, pPicker, pSliders, pHex, pHue);
		}

		private void SetColor(float pH, float pS, float pV, float pA, bool pPicker = true, bool pSliders = true, bool pHex = true, bool pHue = true)
		{
			var newColor = Color.HSVToRGB(pH, pS, pV);

			newColor.a = pA;

			color = transparencyLook.color = compareLook[1].color = newColor;
			_colorJoystick.center.color = new Color(color.r, color.g, color.b);
			locked = true;

			if (pSliders || colorMode == ColorMode.Hsv)
			{
				r.Set(color.r);
				g.Set(color.g);
				b.Set(color.b);
			}

			if (pSliders || colorMode == ColorMode.Rgb)
			{
				h.Set(pH);
				s.Set(pS);
				v.Set(pV);
			}

			a.Set(color.a);

			if (pHue) _hue.value = pH;
			if (pHex) hex.text = ColorUtility.ToHtmlStringRGBA(color);
			if (pPicker) _colorJoystick.transform.localPosition = new Vector2(pS * texture.width / texture.width * _rectTransform.rect.width, pV * texture.height / texture.height * _rectTransform.rect.height);

			locked = false;
			UpdateGradient();
		}

		public void OnHueChanged(float value)
		{
			if (locked) return;

			Color.RGBToHSV(color, out var oH, out var oS, out var oV);

			oH = value;
			SetColor(oH, oS, oV, a.Value, pHue: false);
		}

		public void OnSliderChanged()
		{
			if (locked) return;

			if (colorMode == ColorMode.Rgb)
			{
				SetColor(new Color(r.Value, g.Value, b.Value, a.Value), pSliders: false);
			}
			else
			{
				SetColor(h.Value, s.Value, v.Value, a.Value, pSliders: false);
			}
		}

		public void OnHexValueChanged(string value)
		{
			if (locked) return;

			value = Regex.Replace(value.ToUpper(), "[^0-9A-F]", "");

			hex.text = value;

			if (ColorUtility.TryParseHtmlString("#" + value, out var newColor))
			{
				SetColor(newColor, pHex: false);
			}
		}

		public void SwitchMode()
		{
			colorMode = colorMode == ColorMode.Rgb ? ColorMode.Hsv : ColorMode.Rgb;
			SetMode(colorMode);
		}

		private void SetMode(ColorMode pMode)
		{
			rgbSliders.SetActive(pMode == ColorMode.Rgb);
			hsvSliders.SetActive(pMode == ColorMode.Hsv);
			mode.text = pMode == ColorMode.Rgb ? "HSV" : "RGB";
		}

		private void UpdateGradient()
		{
			var pixels = new List<Color>();

			for (var y = 0; y < texture.height; y++)
			{
				for (var x = 0; x < texture.width; x++)
				{
					var texX = (float) x / texture.width;
					var texY = (float) y / texture.height;
					pixels.Add(Color.HSVToRGB(_hue.value, texX, texY));
				}
			}

			texture.SetPixels(pixels.ToArray());
			texture.Apply();
		}
	}
}