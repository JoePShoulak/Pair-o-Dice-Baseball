using System;
using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class BaseManager : MonoBehaviour
    {
        [SerializeField] private Player.Player runnerOnFirst;
        [SerializeField] private Player.Player runnerOnSecond;
        [SerializeField] private Player.Player runnerOnThird;

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

        private Transform NextLocation(Transform location)
        {
            if (location == baseHome) return baseFirst;
            if (location == baseFirst) return baseSecond;
            if (location == baseSecond) return baseThird;

            return null;
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

            List<Transform> baseRoute = new();

            for (var i = 0; i < basesToAdvance; i++)
            {
                nextBase = NextLocation(nextBase);
                baseRoute.Add(nextBase);
            }

            SetBase(currentBase, null);

            var destinationList = ParseBaseRoute(baseRoute, player);
            var finalStop = destinationList.Last();
            var bases = new List<Transform> { baseFirst, baseSecond, baseThird };
            if (bases.Contains(finalStop))
            {
                SetBase(nextBase, player);
            }

            player.Motion.SetQueue(destinationList);

            // if (nextBase != null)
            // {
            //     player.SetIdlePosition(nextBase.position);
            //     SetBase(nextBase, player);
            //     Debug.Log($"sending player to {nextBase.gameObject.name}");
            // }
            // else
            // {
            //     var dugout = player.team == TeamType.Home
            //         ? _positionManager.homeDugout
            //         : _positionManager.visitorDugout;
            //
            //     var destination = dugout.positions[player.playerStats.position];
            //     player.SetIdlePosition(destination.position);
            // }
        }

        private List<Transform> ParseBaseRoute(List<Transform> pBaseRoute, Player.Player player)
        {
            var crossedHome = pBaseRoute.Any(x => x == null);
            pBaseRoute = pBaseRoute.Where(x => x != null).ToList();
            if (crossedHome)
            {
                var dugout = player.team == TeamType.Home
                    ? _positionManager.homeDugout
                    : _positionManager.visitorDugout;

                var dugoutSpot = dugout.positions[player.playerStats.position];

                pBaseRoute.Add(baseHome);
                pBaseRoute.Add(dugoutSpot);
            }

            return pBaseRoute;
        }

        public void Reset()
        {
            runnerOnFirst = null;
            runnerOnSecond = null;
            runnerOnThird = null;
        }

        public void Advance(Player.Player batter, int times = 1, bool batterMoves = true)
        {
            SendForward(runnerOnThird, times);
            SendForward(runnerOnSecond, times);
            SendForward(runnerOnFirst, times);
            if (batterMoves) SendForward(batter, times);

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

        public void Out(Player.Player batter)
        {
            var dugout = batter.team == TeamType.Home ? _positionManager.homeDugout : _positionManager.visitorDugout;

            var batterPosition = batter.playerStats.position;
            var dugoutDestination = dugout.positions[batterPosition];

            batter.SetIdlePosition(dugoutDestination.position);
        }

        public void CallNewBatter(Player.Player batter)
        {
            batter.SetIdlePosition(_positionManager.field.positions[Position.Batter].position);
        }
    }
}