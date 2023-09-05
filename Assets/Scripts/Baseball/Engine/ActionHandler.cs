using System;
using FibDev.Baseball.Choreography.Ball;
using FibDev.Baseball.Choreography.Play;
using FibDev.Baseball.Plays;
using FibDev.Baseball.Records;
using FibDev.UI;
using UnityEngine;

namespace FibDev.Baseball.Engine
{
    public class ActionHandler : MonoBehaviour
    {
        private Engine _engine;
        private bool readyForRoll;

        private void Start()
        {
            _engine = GetComponent<Engine>();
            _engine.ResetState();
            
            TeamSelectUI.OnTeamsSelected += _engine.StartGame;
            Dice.RollProcessor.OnRollProcessed += HandleRoll;
            Movement.OnMovementEnd += _ =>
            {
                readyForRoll = true;
                Debug.Log("Ready for roll");
            };
        }
        
        private void HandleRoll(int pObj)
        {
            if (!readyForRoll) return;

            readyForRoll = false;
            _engine.NextPlay(Play.Random());
        }

        public void HandleAction(Operation bAction)
        {
            switch (bAction)
            {
                // TODO: Fix all these public properties and no functions
                case Operation.Baseman3rdRunsHome:
                    if (!_engine.Bases.third.runnerOn) break;
                    _engine.Bases.third.runnerOn = false;
                    _engine.record.Add(_engine.inning, _engine.teamAtBat, StatType.Run);
                    break;
                case Operation.Baseman2ndRunsThird:
                    if (!_engine.Bases.second.runnerOn) break;
                    _engine.Bases.second.runnerOn = false;
                    _engine.Bases.third.runnerOn = true;
                    break;
                case Operation.Baseman1stRunsSecond:
                    if (!_engine.Bases.first.runnerOn) break;
                    _engine.Bases.first.runnerOn = false;
                    _engine.Bases.second.runnerOn = true;
                    break;
                case Operation.BatterRunsFirst:
                    _engine.Bases.first.runnerOn = true;
                    break;
                // TODO: Need to implement all of this
                case Operation.BatterHitBall:
                    break;
                case Operation.BatterMissBall:
                    _engine.AddOut();
                    break;
                case Operation.PitcherThrowStrike:
                    _engine.choreographer.movement.pitchType = PitchType.Strike;
                    break;
                case Operation.PitcherThrowsBall:
                    _engine.choreographer.movement.pitchType = PitchType.Ball;
                    break;
                case Operation.PitcherHitsPlayer:
                    _engine.choreographer.movement.pitchType = PitchType.HitByPitch;
                    break;
                case Operation.BasemenAdvanceIfForced:
                    break;
                case Operation.FielderCatchesBall:
                    _engine.AddOut();
                    break;
                case Operation.FielderCollectsBall:
                    break;
                case Operation.FielderBobblesBall:
                    _engine.record.Add(_engine.inning, _engine.teamAtBat, StatType.Error);
                    break;
                case Operation.OutAtSecond:
                    _engine.Bases.second.runnerOn = false;
                    _engine.AddOut();
                    break;
                case Operation.RecordHit:
                    _engine.record.Add(_engine.inning, _engine.teamAtBat, StatType.Hit);
                    break;
                case Operation.Cleanup:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bAction), bAction, null);
            }
        }

    }
}
