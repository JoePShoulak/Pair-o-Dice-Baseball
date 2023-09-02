using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Imports.SimpleColorPicker.Scripts;
using FibDev.Baseball.Teams;

namespace FibDev.UI
{
    public class TeamMaker : MonoBehaviour
    {
        [SerializeField] private TMP_InputField cityField;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private Button primaryButton;
        [SerializeField] private Button secondaryButton;
        [SerializeField] private ColorPicker popupPicker;
        [SerializeField] private TeamType type;

        [SerializeField] private List<PlayerMaker> playerMakers;

        private void Start()
        {
            primaryButton.onClick.AddListener(() =>
            {
                popupPicker.gameObject.SetActive(true);
                popupPicker.OnColorSelected += SetPrimaryColor;
            });

            secondaryButton.onClick.AddListener(() =>
            {
                popupPicker.gameObject.SetActive(true);
                popupPicker.OnColorSelected += SetSecondaryColor;
            });
        }

        // TODO: Give this color-picking buttons their own class to handle this
        // Also, while I'm here, make a simpler color picker. It doesn't need HSV or A
        private void SetPrimaryColor(Color pColor)
        {
            primaryButton.image.color = pColor;
            popupPicker.OnColorSelected -= SetPrimaryColor;
        }

        private void SetSecondaryColor(Color pColor)
        {
            secondaryButton.image.color = pColor;
            popupPicker.OnColorSelected -= SetSecondaryColor;
        }

        public Team GetData()
        {
            var selectedPlayers = playerMakers
                .Select(maker => maker.CreatePlayer())
                .ToDictionary(player => player.position);

            var team = new Team
            {
                city = cityField.text,
                name = nameField.text,
                primary = primaryButton.image.color,
                secondary = secondaryButton.image.color,
                type = type,

                players = selectedPlayers
            };

            foreach (var player in team.players.Values)
            {
                player.primaryColor = team.primary;
                player.secondaryColor = team.secondary;
            }

            return team;
        }
    }
}