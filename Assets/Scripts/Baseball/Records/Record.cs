using System;
using System.Collections.Generic;

using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class Record
    {
        public List<Inning> innings = new();

        public Stats homeTotal = new();
        public Stats visitorTotal = new();

        public TeamType LeadingTeam
        {
            get
            {
                if (homeTotal.runs == visitorTotal.runs) return TeamType.Null;

                return homeTotal.runs > visitorTotal.runs
                    ? TeamType.Home
                    : TeamType.Visiting;
            }
        }

        public void Add(int inning, TeamType team, StatType type, int quantity = 1)
        {
            while (innings.Count < inning) innings.Add(new Inning());
            innings[inning - 1].Add(team, type, quantity);

            (team == TeamType.Home ? homeTotal : visitorTotal).Add(type, quantity);
        }
    }
}