using System.Collections.Generic;

namespace FibDev.Baseball
{
    public static class BaseballPlays
    {
        public enum Action
        {
            BatterHitBall,
            BatterMissBall,
            BatterRunsFirst,
            PitcherThrowStrike,
            Baseman1stRunsSecond,
            Baseman2ndRunsThird,
            Baseman3rdRunsHome,

            Cleanup
        }

        public enum PlayType
        {
            Hit,
            Out,
            Null
        };

        public class Play
        {
            public string name;
            public PlayType type;
            public readonly List<Action> actions;

            public Play(string _name, PlayType _type, List<Action> _actions)
            {
                name = _name;
                type = _type;
                actions = _actions;
            }

            public Play()
            {
                name = "";
                type = PlayType.Null;
                actions = new List<Action>();
            }

            public static readonly Play Single = new PlayBuilder()
                .A(PlayType.Hit)
                .Named("Single")
                .BatterHitBall()
                .BasemenAdvance()
                .Build();

            public static readonly Play Strikeout = new PlayBuilder()
                .An(PlayType.Out)
                .Named("Strikeout")
                .BatterMissesBall()
                .Build();
        }
    }
}