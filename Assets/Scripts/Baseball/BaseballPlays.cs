using System.Collections.Generic;

namespace FibDev.Baseball
{
    public static class BaseballPlays
    {
        public enum PlayType
        {
            Hit,
            Out,
            Null
        };

        public class Play
        {
            public string name = "";
            public PlayType type = PlayType.Null;
            public readonly List<BaseballAction> actions = new();

            /* == HITS == */
            public static readonly Play Single = new PlayBuilder()
                .A(PlayType.Hit).Named("Single")
                .BatterHitBall()
                .BasemenAdvance()
                .Build();

            public static readonly Play Double = new PlayBuilder()
                .A(PlayType.Hit).Named("Double")
                .BatterHitBall()
                .BasemenAdvance(2)
                .Build();

            public static readonly Play Triple = new PlayBuilder()
                .A(PlayType.Hit).Named("Triple")
                .BatterHitBall()
                .BasemenAdvance(3)
                .Build();

            public static readonly Play HomeRun = new PlayBuilder()
                .A(PlayType.Hit).Named("Home Run")
                .BasemenAdvance(4)
                .Build();

            public static readonly Play Walk = new PlayBuilder()
                .A(PlayType.Hit).Named("Walk")
                .BatterTakesBall()
                .Add(BaseballAction.BasemenAdvanceIfForced)
                .Add(BaseballAction.BatterRunsFirst)
                .Build();

            public static readonly Play HitByPitch = new PlayBuilder()
                .A(PlayType.Hit).Named("Hit By Pitch")
                .BatterGetsHit()
                .Add(BaseballAction.BasemenAdvanceIfForced)
                .Add(BaseballAction.BatterRunsFirst)
                .Build();

            public static readonly Play Error = new PlayBuilder()
                .A(PlayType.Hit).Named("Error")
                .BatterHitBall()
                .Add(BaseballAction.FielderBobblesBall)
                .BasemenAdvance()
                .Build();

            /* == OUTS == */
            public static readonly Play StrikeOut = new PlayBuilder()
                .An(PlayType.Out).Named("Strike Out")
                .BatterMissesBall()
                .Build();

            public static readonly Play FlyOut = new PlayBuilder()
                .An(PlayType.Out).Named("Fly Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCatchesBall)
                .Build();

            public static readonly Play FlyOutPlus = new PlayBuilder()
                .An(PlayType.Out).Named("Fly Out Plus")
                .BatterHitBall()
                .Add(BaseballAction.FielderCatchesBall)
                .Add(BaseballAction.Baseman3rdRunsHome)
                .Build();

            public static readonly Play PopOut = new PlayBuilder()
                .An(PlayType.Out).Named("Pop Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCatchesBall)
                .Build();

            public static readonly Play LineOut = new PlayBuilder()
                .An(PlayType.Out).Named("Line Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCatchesBall)
                .Build();

            public static readonly Play FoulOut = new PlayBuilder()
                .An(PlayType.Out).Named("Foul Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCatchesBall)
                .Build();

            public static readonly Play GroundOut = new PlayBuilder()
                .An(PlayType.Out).Named("Ground Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCollectsBall)
                .Add(BaseballAction.OutAtFirst)
                .BasemenAdvance(1, false)
                .Build();

            public static readonly Play GroundOut2 = new PlayBuilder()
                .An(PlayType.Out).Named("Ground Out")
                .BatterHitBall()
                .Add(BaseballAction.FielderCollectsBall)
                .Add(BaseballAction.OutAtFirst)
                .Add(BaseballAction.OutAtSecond)
                .BasemenAdvance(1, false)
                .Build();

            public static Play Random()
            {
                var options = new List<Play>
                {
                    Single,
                    Double,
                    Triple,
                    HomeRun,
                    Error,
                    Walk,
                    HitByPitch,

                    StrikeOut,
                    FlyOut,
                    FlyOutPlus,
                    PopOut,
                    LineOut,
                    FoulOut,
                    GroundOut,
                    GroundOut2
                };

                return options[new System.Random().Next(0, options.Count)];
            }
        }
    }
}