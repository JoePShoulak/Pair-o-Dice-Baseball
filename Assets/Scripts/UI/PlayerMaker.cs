using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FibDev.Baseball;
using FibDev.Baseball.Player;

namespace FibDev.UI
{
    public class PlayerMaker : MonoBehaviour
    {
        [SerializeField] private Position playerPosition;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_InputField numberField;

        [SerializeField] private Toggle leftyToggle;

        [SerializeField] private TMP_Dropdown weightDropdown;
        [SerializeField] private TMP_Dropdown heightDropdown;

        [SerializeField] private Slider skinToneSlider;
        [SerializeField] private Image skinToneImage;

        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        
        // TODO: Make this ia separate class
        private void Start()
        {
            var white = new Color(1f, 0.855f, 0.725f);
            var black = new Color(0.243f, 0.168f, 0.137f);
            skinToneImage.color = white;
            skinToneSlider.value = 0f;
            
            skinToneSlider.onValueChanged.AddListener(value =>
            {
                skinToneImage.color = Color.Lerp(white, black, value);
            });
        }

        private static T GetDropdownValueAsEnum<T>(TMP_Dropdown pDropdown) where T : struct
        {
            return Enum.Parse<T>(pDropdown.options[pDropdown.value].text);
        }

        private static string GetInputFieldValueOrPlaceholder(TMP_InputField pInputField)
        {
            var fieldText = pInputField.text;
            return fieldText.Length > 0 ? fieldText : pInputField.placeholder.GetComponent<TMP_Text>().text;
        }

        private static string PadJerseyNumber(string num)
        {
            return num.Length >= 2 ? num : $"0{num}";
        }

        public PlayerStats CreatePlayer()
        {

            var stats = new PlayerStats
            {
                playerName = GetInputFieldValueOrPlaceholder(nameField),
                number = PadJerseyNumber(GetInputFieldValueOrPlaceholder(numberField)),
                lefty = leftyToggle.isOn,
                weight = GetDropdownValueAsEnum<WeightType>(weightDropdown),
                height = GetDropdownValueAsEnum<HeightType>(heightDropdown),
                skinColor = skinToneImage.color,
                position = playerPosition
            };

            return stats;
        }
    }
}