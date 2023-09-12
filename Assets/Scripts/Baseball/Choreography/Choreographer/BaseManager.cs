using System.Collections.Generic;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class BaseManager : MonoBehaviour
    {
        private Player.Player runnerOnFirst;
        private Player.Player runnerOnSecond;
        private Player.Player runnerOnThird;

        private Transform baseHome;
        private Transform baseFirst;
        private Transform baseSecond;
        private Transform baseThird;

        private PositionManager _positionManager;

        private void Start()
        {
            _positionManager = PositionManager.Instance;
            var fieldPositionsDictionary = _positionManager.field.positions;

            baseHome = fieldPositionsDictionary[Position.Catcher];
            baseFirst = fieldPositionsDictionary[Position.Baseman1st];
            baseSecond = fieldPositionsDictionary[Position.Baseman2nd];
            baseThird = fieldPositionsDictionary[Position.Baseman3rd];
        }

        [CanBeNull]
        private Transform ClosestBaseToPlayer(Player.Player player)
        {
            var bases = new List<Transform>() { baseFirst, baseSecond, baseThird, baseHome };

            Transform closestBase = null;
            var closestDistance = float.MaxValue;
            
            foreach (var baseLocation in bases)
            {
                var distance = Vector3.Distance(baseLocation.position, player.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBase = baseLocation;
                }
            }

            return closestBase;
        } 

        private Transform NextBase(Transform baseLoc)
        {
            if (baseLoc == baseHome) return baseFirst;
            if (baseLoc == baseFirst) return baseSecond;
            if (baseLoc == baseSecond) return baseThird;
            
            return baseHome;
        }

        private void SetBase(Transform baseLoc, Player.Player player)
        {
            if (baseLoc == baseFirst) runnerOnFirst = player;
            if (baseLoc == baseSecond) runnerOnSecond = player;
            if (baseLoc == baseThird) runnerOnThird = player;
        }

        private void SendForward(Player.Player player, int basesToAdvance = 1)
        {
            if (player == null) return; 
            
            var currentBase = ClosestBaseToPlayer(player);
            var nextBase = currentBase;
            
            for (var i = 0; i < basesToAdvance; i++)
            {
                nextBase = NextBase(nextBase);
                if (nextBase == baseHome) break;
            }
            
            player.SetIdlePosition(nextBase);
            SetBase(currentBase, null);
            SetBase(nextBase, player);
        }

        public void Advance(Player.Player batter, int times = 1)
        {
            SendForward(runnerOnThird, times);
            SendForward(runnerOnSecond, times);
            SendForward(runnerOnThird, times);
            SendForward(batter, times);
            
            Debug.Log($"Advancing batter and runners {times} times");
        }

        public void AdvanceIfForced(Player.Player batter)
        {
            if (runnerOnFirst != null && runnerOnSecond != null) SendForward(runnerOnThird);
            if (runnerOnFirst != null) SendForward(runnerOnSecond);
            SendForward(runnerOnFirst);
            SendForward(batter);
            
            Debug.Log("Advancing if forced");
        }

        public void Out(Player.Player batter, TeamType team)
        {
            var dugout = team == TeamType.Home ? _positionManager.homeDugout : _positionManager.visitorDugout;

            var batterPosition = batter.playerStats.position;
            var dugoutDestination = dugout.positions[batterPosition];

            batter.SetIdlePosition(dugoutDestination);
        }

        public void CallNewBatter(Player.Player batter)
        {
            batter.SetIdlePosition(_positionManager.field.positions[Position.Batter]);
        }
    }
}