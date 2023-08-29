using System;
using System.Collections.Generic;
using UnityEngine;

using FibDev.Baseball;

namespace FibDev.UI
{
    public class TeamSelectUI : MonoBehaviour
    {
        [SerializeField] private TeamMaker homeTeam;
        [SerializeField] private TeamMaker visitingTeam;

        public event Action<List<TeamCreationData>> OnTeamsSelected;
        
        public void SelectTeams()
        {
            var homeData = homeTeam.GetData();
            var visitingData = visitingTeam.GetData();
            
            Debug.Log("Home Team: ");
            LogTeamData(homeData);
            Debug.Log("Visiting Team: ");
            LogTeamData(visitingData);

            var selection = new List<TeamCreationData>() { homeData, visitingData };
            OnTeamsSelected?.Invoke(selection);

            gameObject.SetActive(false);
        }

        private void LogTeamData(TeamCreationData data)
        {
            Debug.Log($"  Name: {data.city} {data.name}");
            Debug.Log($"  P. Color: {data.primary}");
            Debug.Log($"  S. Color: {data.secondary}");
        }
    }
}