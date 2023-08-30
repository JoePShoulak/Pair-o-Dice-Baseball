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

        public PlayBuilder Add(BaseballAction pBBaseballAction)
        {
            play.actions.Add(pBBaseballAction);

            return this;
        }

        /* == BATTER == */
        public PlayBuilder BatterHitBall()
        {
            play.actions.Add(BaseballAction.PitcherThrowStrike);
            play.actions.Add(BaseballAction.BatterHitBall);

            return this;
        }
        
        public PlayBuilder BatterTakesBall()
        {
            play.actions.Add(BaseballAction.PitcherThrowsBall);

            return this;
        }   
        
        public PlayBuilder BatterGetsHit()
        {
            play.actions.Add(BaseballAction.PitcherHitsPlayer);

            return this;
        }

        public PlayBuilder BatterMissesBall()
        {
            play.actions.Add(BaseballAction.PitcherThrowStrike);
            play.actions.Add(BaseballAction.BatterMissBall);

            return this;
        }

        /* == BASEMEN == */
        public PlayBuilder BasemenAdvance(int times = 1, bool hit = true)
        {
            for (var i = 0; i < times; i++)
            {
                play.actions.Add(BaseballAction.Baseman3rdRunsHome);
                play.actions.Add(BaseballAction.Baseman2ndRunsThird);
                play.actions.Add(BaseballAction.Baseman1stRunsSecond);
                if (i==0 && hit) play.actions.Add(BaseballAction.BatterRunsFirst);
            }

            return this;
        }

        public Play Build()
        {
            play.actions.Add(BaseballAction.Cleanup);

            return play;
        }
    }
}