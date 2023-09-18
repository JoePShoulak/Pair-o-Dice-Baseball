using System.Collections.Generic;
using System.Linq;
using static FibDev.Baseball.Builders;

namespace FibDev.Baseball.Plays
{
    public class Play
    {
        public string name = "";
        public readonly List<Operation> actions = new();

        public static readonly Dictionary<PlayEnum, Play> plays = new()
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
            },
            {
                PlayEnum.HitByPitch, A.Play.Named("Hit By Pitch")
                    .BatterGetsHit()
                    .Add(Operation.BasemenAdvanceIfForced)
            },
            {
                PlayEnum.Error, A.Play.Named("Error")
                    .BatterHitBall(false)
                    .Add(Operation.BasemenAdvanceIfForced)
                    .Add(Operation.Error)
            },
            {
                PlayEnum.StrikeOut, A.Play.Named("Strike Out")
                    .BatterMissesBall()
            },
            {
                PlayEnum.FlyOut, A.Play.Named("Fly Out")
                    .BatterHitBall(false)
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.PopOut, A.Play.Named("Pop Out")
                    .BatterHitBall(false)
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.LineOut, A.Play.Named("Line Out")
                    .BatterHitBall(false)
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.FoulOut, A.Play.Named("Foul Out")
                    .BatterHitBall(false)
                    .Add(Operation.FielderCatchesBall)
            },
            {
                PlayEnum.GroundOut, A.Play.Named("Ground Out")
                    .BatterHitBall(false)
                    .Add(Operation.OutAtFirst)
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