using System.Collections.Generic;

using static FibDev.Baseball.Builders;

namespace FibDev.Baseball
{
    public class Play
    {
        public string name = "";
        public readonly List<Operation> actions = new();

        /* == HITS == */
        private static readonly Play Single = A.Play.Named("Single")
            .BatterHitBall()
            .BasemenAdvance();

        private static readonly Play Double = A.Play.Named("Double")
            .BatterHitBall()
            .BasemenAdvance(2);

        private static readonly Play Triple = A.Play.Named("Triple")
            .BatterHitBall()
            .BasemenAdvance(3);

        private static readonly Play HomeRun = A.Play.Named("Home Run")
            .BatterHitBall()
            .BasemenAdvance(4);

        private static readonly Play Walk = A.Play.Named("Walk")
            .BatterTakesBall()
            .Add(Operation.BasemenAdvanceIfForced)
            .Add(Operation.BatterRunsFirst);

        private static readonly Play HitByPitch = A.Play.Named("Hit By Pitch")
            .BatterGetsHit()
            .Add(Operation.BasemenAdvanceIfForced)
            .Add(Operation.BatterRunsFirst);

        private static readonly Play Error = A.Play.Named("Error")
            .BatterHitBall(false)
            .Add(Operation.FielderBobblesBall)
            .BasemenAdvance();

        /* == OUTS == */
        private static readonly Play StrikeOut = A.Play.Named("Strike Out")
            .BatterMissesBall();

        private static readonly Play FlyOut = A.Play.Named("Fly Out")
            .BatterHitBall()
            .Add(Operation.FielderCatchesBall);

        private static readonly Play FlyOutPlus = A.Play.Named("Fly Out Plus")
            .BatterHitBall()
            .Add(Operation.FielderCatchesBall)
            .Add(Operation.Baseman3rdRunsHome);

        private static readonly Play PopOut = A.Play.Named("Pop Out")
            .BatterHitBall()
            .Add(Operation.FielderCatchesBall);

        private static readonly Play LineOut = A.Play.Named("Line Out")
            .BatterHitBall()
            .Add(Operation.FielderCatchesBall);

        private static readonly Play FoulOut = A.Play.Named("Foul Out")
            .BatterHitBall()
            .Add(Operation.FielderCatchesBall);

        private static readonly Play GroundOut = A.Play.Named("Ground Out")
            .BatterHitBall()
            .Add(Operation.FielderCollectsBall)
            .BasemenAdvance(1, false);

        private static readonly Play GroundOut2 = A.Play.Named("Ground Out")
            .BatterHitBall()
            .Add(Operation.FielderCollectsBall)
            .Add(Operation.OutAtSecond)
            .BasemenAdvance(1, false);

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