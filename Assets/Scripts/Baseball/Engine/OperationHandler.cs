using System;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public static class OperationHandler
    {
        public static void HandleOperation(Engine engine, Operation bAction)
        {
            var movement = engine.choreographer.movement.runnerMovement;
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
                case Operation.BatterHitBall:
                    break;
                case Operation.BatterMissBall:
                    engine.AddOut();
                    break;
                case Operation.PitcherThrowStrike:
                    break;
                case Operation.PitcherThrowsBall:
                    break;
                case Operation.PitcherHitsPlayer:
                    break;
                case Operation.BasemenAdvanceIfForced:
                    engine.choreographer.movement.runnerMovement = RunnerMovement.Force;
                    break;
                case Operation.FielderCatchesBall:
                    engine.AddOut();
                    break;
                case Operation.FielderCollectsBall:
                    break;
                case Operation.Error:
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
                case Operation.BasemanAdvance:
                    Debug.Log("Baseman Advance called");
                    switch (movement)
                    {
                        case RunnerMovement.Stay:
                            movement = RunnerMovement.Single;
                            Debug.Log("Stay became a single");
                            break;
                        case RunnerMovement.Single:
                            movement = RunnerMovement.Double;
                            Debug.Log("Single became a double");
                            break;
                        case RunnerMovement.Double:
                            movement = RunnerMovement.Triple;
                            Debug.Log("Double became a triple");
                            break;
                        case RunnerMovement.Triple:
                            movement = RunnerMovement.HomeRun;
                            Debug.Log("Triple became a home run");
                            break;
                        case RunnerMovement.HomeRun:
                            break;
                        case RunnerMovement.Force:
                            // TODO: Outs are advancing if forced; figure out why
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    engine.choreographer.movement.runnerMovement = movement;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

    }
}
