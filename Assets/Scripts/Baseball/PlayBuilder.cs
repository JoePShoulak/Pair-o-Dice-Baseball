using static FibDev.Baseball.BaseballPlays;

namespace FibDev.Baseball
{
    public class PlayBuilder
    {
        private Play play;

        /* == GENERAL SETUP == */
        public PlayBuilder A(PlayType _type)
        {
            play = new Play { type = _type };

            return this;
        }

        public PlayBuilder An(PlayType _type)
        {
            return A(_type);
        }

        public PlayBuilder Named(string _name)
        {
            play.name = _name;

            return this;
        }

        /* == BATTER == */
        public PlayBuilder BatterHitBall()
        {
            play.actions.Add(Action.PitcherThrowStrike);
            play.actions.Add(Action.BatterHitBall);

            return this;
        }

        public PlayBuilder BatterMissesBall()
        {
            play.actions.Add(Action.PitcherThrowStrike);
            play.actions.Add(Action.BatterMissBall);

            return this;
        }

        /* == BASEMEN == */
        public PlayBuilder BasemenAdvance()
        {
            play.actions.Add(Action.Baseman3rdRunsHome);
            play.actions.Add(Action.Baseman2ndRunsThird);
            play.actions.Add(Action.Baseman1stRunsSecond);
            play.actions.Add(Action.BatterRunsFirst);

            return this;
        }

        public Play Build()
        {
            play.actions.Add(Action.Cleanup);

            return play;
        }
    }
}