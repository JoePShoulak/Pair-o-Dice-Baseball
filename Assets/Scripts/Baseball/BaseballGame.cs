using System;
using System.Collections.Generic;
using UnityEngine;

namespace FibDev.Baseball
{
    public class BaseballGame : MonoBehaviour
    {
        private enum Team
        {
            Home,
            Visiting
        }

        private enum BaseLocation
        {
            first,
            second,
            third,
            home
        }

        [Serializable]
        private struct Base
        {
            public BaseLocation location;
            public bool runnerOn;

            public Base(BaseLocation _location)
            {
                location = _location;
                runnerOn = false;
            }
        }

        // Serialized for debugging
        [SerializeField] private int inning;
        [SerializeField] private Team battingTeam;
        [SerializeField] private int outs;

        [SerializeField] private List<Base> bases = new()
        {
            new Base(BaseLocation.first), new Base(BaseLocation.second),
            new Base(BaseLocation.third), new Base(BaseLocation.home)
        };

        private void Start()
        {
            ResetState();
        }

        public void ResetState() // for debug
        {
            battingTeam = Team.Visiting;
            inning = 1;
            outs = 0;
        }

        private void AdvanceInning()
        {
            outs = 0;

            if (battingTeam == Team.Visiting)
            {
                battingTeam = Team.Home;
                return;
            }

            battingTeam = Team.Visiting;
            inning++;
        }

        private BaseballEvent.Event RandomEvent()
        {
            var options = new List<BaseballEvent.Event> { BaseballEvent.Single, BaseballEvent.Strikeout };

            return options[new System.Random().Next(0, options.Count)];
        }

        public void NextEvent()
        {
            Debug.Log(bEvent.name);
            // LogEvent(bEvent);
            // LogState();
            
            var bEvent = RandomEvent();

            foreach (var bAction in bEvent.actions)
            {
                HandleAction(bAction);
            }
            
            if (bEvent.type == BaseballEvent.EventType.Out)
            {
                outs++;
                if (outs >= 3) AdvanceInning();
            }
        }

        private void HandleAction(BaseballAction.Action bAction)
        {
            switch (bA)
            {
                
            }
        }

        private void LogEvent(BaseballEvent.Event _event)
        {
            foreach (var bAction in _event.actions)
            {
                Debug.Log(bAction.description);
            }
        }

        private void LogState()
        {
            Debug.Log($"Batting Team: {battingTeam}");
            Debug.Log($"Inning: {inning}");
            Debug.Log($"Outs: {outs}");
        }
    }
}