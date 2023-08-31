namespace FibDev.Baseball
{
    public class PlayBuilder
    {
        private readonly Play play = new();

        /* == GENERAL SETUP == */
        public PlayBuilder Named(string _name)
        {
            play.name = _name;

            return this;
        }

        public PlayBuilder Add(Operation pBOperation)
        {
            play.actions.Add(pBOperation);

            return this;
        }

        /* == BATTER == */
        public PlayBuilder BatterHitBall()
        {
            play.actions.Add(Operation.PitcherThrowStrike);
            play.actions.Add(Operation.BatterHitBall);

            return this;
        }
        
        public PlayBuilder BatterTakesBall()
        {
            play.actions.Add(Operation.PitcherThrowsBall);

            return this;
        }   
        
        public PlayBuilder BatterGetsHit()
        {
            play.actions.Add(Operation.PitcherHitsPlayer);

            return this;
        }

        public PlayBuilder BatterMissesBall()
        {
            play.actions.Add(Operation.PitcherThrowStrike);
            play.actions.Add(Operation.BatterMissBall);

            return this;
        }

        /* == BASEMEN == */
        public PlayBuilder BasemenAdvance(int times = 1, bool hit = true)
        {
            for (var i = 0; i < times; i++)
            {
                play.actions.Add(Operation.Baseman3rdRunsHome);
                play.actions.Add(Operation.Baseman2ndRunsThird);
                play.actions.Add(Operation.Baseman1stRunsSecond);
                if (i==0 && hit) play.actions.Add(Operation.BatterRunsFirst);
            }

            return this;
        }
        
        /* == BUILD == */
        private Play Build()
        {
            play.actions.Add(Operation.Cleanup);

            return play;
        }

        public static implicit operator Play(PlayBuilder builder)
        {
            return builder.Build();
        }
    }
}