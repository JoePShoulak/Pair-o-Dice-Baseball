using System.Collections.Generic;
using FibDev.Baseball.Player;
using FibDev.Baseball.Teams;
using Imports.SimpleColorPicker.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            List<Stats> selectedPlayers = new();
            foreach (var maker in playerMakers)
            {
                selectedPlayers.Add(maker.CreatePlayer());
            }

            var team = new Team
            {
                city = cityField.text,
                name = nameField.text,
                primary = primaryButton.image.color,
                secondary = secondaryButton.image.color,
                type = type,

                players = selectedPlayers
            };

            foreach (var player in team.players)
            {
                player.primaryColor = team.primary;
                player.secondaryColor = team.secondary;
            }

            return team;
        }
    }
}