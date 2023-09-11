using System;
using System.Collections.Generic;

using FibDev.Baseball.Teams;

namespace FibDev.Baseball.Records
{
    [Serializable]
    public class Record
    {
        public List<Inning> innings = new();

        public InningStats homeTotal = new();
        public InningStats visitorTotal = new();

        public TeamType? LeadingTeam
        {
            get
            {
                if (homeTotal.runs == visitorTotal.runs) return null;

                return homeTotal.runs > visitorTotal.runs
                    ? TeamType.Home
                    : TeamType.Visiting;
            }
        }

        public bool HomeWinning => LeadingTeam == TeamType.Home;
        public bool VisitorsWinning => LeadingTeam == TeamType.Visiting;

        public void Add(int inning, TeamType team, StatType type, int quantity = 1)
        {
            while (innings.Count < inning) innings.Add(new Inning());
            innings[inning - 1].Add(team, type, quantity);

            (team == TeamType.Home ? homeTotal : visitorTotal).Add(type, quantity);
        }
    }
}