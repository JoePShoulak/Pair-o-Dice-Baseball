using System;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;

namespace FibDev.Baseball.Engine
{
    public static class ActionHandler
    {
        public static void HandleAction(Engine engine, Operation bAction)
        {
            switch (bAction)
            {
                // TODO: Fix all these public properties and no functions
                case Operation.Baseman3rdRunsHome:
                    if (!engine.Bases.third.runnerOn) break;
                    engine.Bases.third.runnerOn = false;
                    engine.record.Add(engine.inning, engine.teamAtBat, StatType.Run);
                    break;
                case Operation.Baseman2ndRunsThird:
                    if (!engine.Bases.second.runnerOn) break;
                    engine.Bases.second.runnerOn = false;
                    engine.Bases.third.runnerOn = true;
                    break;
                case Operation.Baseman1stRunsSecond:
                    if (!engine.Bases.first.runnerOn) break;
                    engine.Bases.first.runnerOn = false;
                    engine.Bases.second.runnerOn = true;
                    break;
                case Operation.BatterRunsFirst:
                    engine.Bases.first.runnerOn = true;
                    break;
                // TODO: Need to implement all of this
                case Operation.BatterHitBall:
                    break;
                case Operation.BatterMissBall:
                    engine.AddOut();
                    break;
                case Operation.PitcherThrowStrike:
                    engine.choreographer.movement.pitchType = PitchType.Strike;
                    break;
                case Operation.PitcherThrowsBall:
                    engine.choreographer.movement.pitchType = PitchType.Ball;
                    break;
                case Operation.PitcherHitsPlayer:
                    engine.choreographer.movement.pitchType = PitchType.HitByPitch;
                    break;
                case Operation.BasemenAdvanceIfForced:
                    break;
                case Operation.FielderCatchesBall:
                    engine.AddOut();
                    break;
                case Operation.FielderCollectsBall:
                    break;
                case Operation.FielderBobblesBall:
                    engine.record.Add(engine.inning, engine.teamAtBat, StatType.Error);
                    break;
                case Operation.OutAtSecond:
                    engine.Bases.second.runnerOn = false;
                    engine.AddOut();
                    break;
                case Operation.RecordHit:
                    engine.record.Add(engine.inning, engine.teamAtBat, StatType.Hit);
                    break;
                case Operation.Cleanup:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

    }
}
