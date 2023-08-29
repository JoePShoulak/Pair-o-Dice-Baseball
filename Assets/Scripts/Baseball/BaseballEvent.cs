using System.Collections.Generic;

namespace FibDev.Baseball
{
    public static class BaseballEvent
    {
        public enum EventType
        {
            Hit,
            Out
        };

        public struct Event
        {
            public string name;
            public EventType type;
            public List<BaseballAction.Action> actions;

            public Event(string _name, EventType _type, List<BaseballAction.Action> _actions)
            {
                name = _name;
                type = _type;
                actions = _actions;
            }
        }

        public static Event Single =
            new("Single", EventType.Hit, new List<BaseballAction.Action>
            {
                BaseballAction.PitcherThrowStrike,
                BaseballAction.BatterHitBall,

                BaseballAction.BatterRunsFirst,
                BaseballAction.Baseman1stRunsSecond,
                BaseballAction.Baseman2ndRunsThird,
                BaseballAction.Baseman3rdRunsHome,

                BaseballAction.Cleanup,
            });
        
        public static Event Strikeout =
            new("Strikeout", EventType.Out, new List<BaseballAction.Action>
            {
                BaseballAction.PitcherThrowStrike,
                BaseballAction.BatterMissBall,

                BaseballAction.Cleanup,
            });
    }
}