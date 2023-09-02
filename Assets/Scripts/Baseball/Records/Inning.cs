using System;

using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class Inning
    {
        public InningStats homeInningStats = new();
        public InningStats visitorInningStats = new();
        
        public void Add(TeamType team, StatType type, int quantity = 1)
        {
            (team == TeamType.Home ? homeInningStats : visitorInningStats).Add(type, quantity);
        }
    }
}
