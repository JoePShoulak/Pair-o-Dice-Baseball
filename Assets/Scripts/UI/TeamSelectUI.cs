using System;
using System.Collections.Generic;
using UnityEngine;
using FibDev.Baseball.Teams;

namespace FibDev.UI
{
    public class TeamSelectUI : MonoBehaviour
    {
        [SerializeField] private TeamMaker homeTeamMaker;
        [SerializeField] private TeamMaker visitingTeamMaker;

        public static event Action<Dictionary<TeamType, Team>> OnTeamsSelected;

        public void SelectTeams()
        {
            var homeData = homeTeamMaker.GetData();
            var visitingData = visitingTeamMaker.GetData();

            // Debug.Log("Home Team: ");
            // LogTeamData(homeData);
            // Debug.Log("Visiting Team: ");
            // LogTeamData(visitingData);

            var selection = new Dictionary<TeamType, Team> { { TeamType.Home, homeData }, {TeamType.Visiting,  visitingData } };
            OnTeamsSelected?.Invoke(selection);

            gameObject.SetActive(false);
            OverlayManager.Instance.gameOverlay.SetActive(true);
        }
    }
}