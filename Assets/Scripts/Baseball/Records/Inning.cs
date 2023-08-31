using System;
using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class Inning
    {
        public Stats homeStats = new();
        public Stats visitorStats = new();
        
        public void Add(TeamType team, StatType type, int quantity = 1)
        {
            (team == TeamType.Home ? homeStats : visitorStats).Add(type, quantity);
        }
    }
}
