using System;
using FibDev.Baseball;
using FibDev.Baseball.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

        private static string GetDropdownText(TMP_Dropdown pDropdown)
        {
            return pDropdown.options[pDropdown.value].text;
        }

        public Stats CreatePlayer()
        {
            var weightParsed = Enum.Parse<WeightType>(GetDropdownText(weightDropdown));
            var heightParsed = Enum.Parse<HeightType>(GetDropdownText(heightDropdown));
            
            var stats = new Stats
            {
                playerName = nameField.text,
                number = numberField.text == "" ? 0 : int.Parse(numberField.text),
                lefty = leftyToggle.isOn,
                weight = weightParsed,
                height = heightParsed,
                skinColor = skinToneImage.color,
                position = playerPosition
            };

            return stats;
        }
    }
}