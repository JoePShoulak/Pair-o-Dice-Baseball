using TMPro;
using UnityEngine;
using Assets.SimpleColorPicker.Scripts;
using FibDev.Baseball;
using FibDev.Baseball.Player;
using FibDev.Baseball.Teams;

namespace FibDev.UI
{
    public class TeamMaker : MonoBehaviour
    {
        [SerializeField] private TMP_InputField cityField;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private ColorPicker primaryPicker;
        [SerializeField] private ColorPicker secondaryPicker;
        [SerializeField] private TeamType _type;

        public Team GetData()
        {
            // TODO: This is dumb
            var selectedPlayers = new[]
            {
                new Stats { position = Position.Pitcher},
                new Stats { position = Position.Catcher},
                new Stats { position = Position.Shortstop},

                new Stats { position = Position.Baseman1st},
                new Stats { position = Position.Baseman2nd},
                new Stats { position = Position.Baseman3rd},

                new Stats { position = Position.FielderLeft},
                new Stats { position = Position.FielderCenter},
                new Stats { position = Position.FielderRight},
            };

            var team = new Team
            {
                city = cityField.text,
                name = nameField.text,
                primary = primaryPicker.Color,
                secondary = secondaryPicker.Color,
                type = _type,

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