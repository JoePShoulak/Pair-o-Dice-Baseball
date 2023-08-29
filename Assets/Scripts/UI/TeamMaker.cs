using TMPro;
using UnityEngine;

using Assets.SimpleColorPicker.Scripts;

using FibDev.Baseball;

namespace FibDev.UI
{
    public class TeamMaker : MonoBehaviour
    {
        [SerializeField] private TMP_InputField cityField;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private ColorPicker primaryPicker;
        [SerializeField] private ColorPicker secondaryPicker;

        public TeamCreationData GetData()
        {
            return new TeamCreationData
            {
                city = cityField.text,
                name = nameField.text,
                primary = primaryPicker.Color,
                secondary = secondaryPicker.Color
            };
        }
    }
}
