using System.Collections.Generic;
using System.Linq;
using static FibDev.Baseball.Builders;

namespace FibDev.Baseball.Plays
{
    public class Play
    {
        public string name = "";
        public readonly List<Operation> actions = new();

        public static readonly Dictionary<PlayEnum, Play> plays = new Dictionary<PlayEnum, Play>()
        {
            {
                PlayEnum.Single, A.Play.Named("Single")
                    .BatterHitBall()
                    .BasemenAdvance()
            },
            {
                PlayEnum.Double, A.Play.Named("Double")
                    .BatterHitBall()
                    .BasemenAdvance(2)
            },
            {
                PlayEnum.Triple, A.Play.Named("Triple")
                    .BatterHitBall()
                    .BasemenAdvance(3)
            },
            {
                PlayEnum.HomeRun, A.Play.Named("Home Run")
                    .BatterHitBall()
                    .BasemenAdvance(4)
            },
            {
                PlayEnum.Walk, A.Play.Named("Walk")
                    .BatterTakesBall()
                    .Add(Operation.BasemenAdvanceIfForced)
                    .Add(Operation.BatterRunsFirst)
            },
            {
                PlayEnum.HitByPitch, A.Play.Named("Hit By Pitch")
                    .BatterGetsHit()
                    .Add(Operation.BasemenAdvanceIfForced)
                    .Add(Operation.BatterRunsFirst)
            },
            {
                PlayEnum.Error, A.Play.Named("Error")
                    .BatterHitBall(false)
                    .Add(Operation.FielderBobblesBall)
                    .BasemenAdvance()
            },
            {
                PlayEnum.StrikeOut, A.Play.Named("Strike Out")
                    .BatterMissesBall()
            },
            {
                PlayEnum.FlyOut, A.Play.Named("Fly Out")
                    .BatterHitBall()
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.FlyOutPlus, A.Play.Named("Fly Out Plus")
                    .BatterHitBall()
                    .Add(Operation.FielderCatchesBall)
                    .Add(Operation.Baseman3rdRunsHome)
            },
            {
                PlayEnum.PopOut, A.Play.Named("Pop Out")
                    .BatterHitBall()
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.LineOut, A.Play.Named("Line Out")
                    .BatterHitBall()
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.FoulOut, A.Play.Named("Foul Out")
                    .BatterHitBall()
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.GroundOut, A.Play.Named("Ground Out")
                    .BatterHitBall()
                    .Add(Operation.FielderCollectsBall)
                    .BasemenAdvance(1, false)
            },
            {
                PlayEnum.GroundOut2, A.Play.Named("Ground Out (2)")
                    .BatterHitBall()
                    .Add(Operation.FielderCollectsBall)
                    .Add(Operation.OutAtSecond)
                    .BasemenAdvance(1, false)
            }
        };

        public static Play Random()
        {
            var options = plays.Values.ToList();
            return options[new System.Random().Next(0, options.Count)];
        }
    }
}