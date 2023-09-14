using System.Collections.Generic;
using System.Linq;
using FibDev.Baseball.Choreography.Positions;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class BaseManager : MonoBehaviour
    {
        private PositionManager _positionManager;

        private Player.Player runnerOnFirst;
        private Player.Player runnerOnSecond;
        private Player.Player runnerOnThird;

        private Transform baseHome;
        private Transform baseFirst;
        private Transform baseSecond;
        private Transform baseThird;

        private List<Transform> _bases;

        private void Start()
        {
            _positionManager = PositionManager.Instance;
            var fieldPositionsDictionary = _positionManager.field.positions;

            baseHome = fieldPositionsDictionary[Position.Catcher];
            baseFirst = fieldPositionsDictionary[Position.Baseman1st];
            baseSecond = fieldPositionsDictionary[Position.Baseman2nd];
            baseThird = fieldPositionsDictionary[Position.Baseman3rd];

            _bases = new List<Transform> { baseHome, baseFirst, baseSecond, baseThird };
        }

        private Transform ClosestBaseToPlayer(Component player)
        {
            Transform closestBase = null;
            var closestDistance = float.MaxValue;

            foreach (var baseLocation in _bases)
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

        private Transform NextLocation(Object location)
        {
            if (location == baseHome) return baseFirst;
            if (location == baseFirst) return baseSecond;
            return location == baseSecond ? baseThird : null;
        }

        private void SetBase(Object baseLoc, Player.Player player)
        {
            if (baseLoc == baseFirst) runnerOnFirst = player;
            if (baseLoc == baseSecond) runnerOnSecond = player;
            if (baseLoc == baseThird) runnerOnThird = player;
        }

        private List<Transform> GenerateBasePath(Player.Player player, Transform currentBase, int basesToAdvance)
        {
            var nextBase = currentBase;

            List<Transform> baseRoute = new();

            for (var i = 0; i < basesToAdvance; i++)
            {
                nextBase = NextLocation(nextBase);
                baseRoute.Add(nextBase);
            }

            return ParseBaseRoute(baseRoute, player);
        }

        private void SendForward(Player.Player player, int basesToAdvance = 1)
        {
            if (player == null) return;

            var currentBase = ClosestBaseToPlayer(player);
            SetBase(currentBase, null);

            var destinationList = GenerateBasePath(player, currentBase, basesToAdvance);
            player.Motion.SetQueue(destinationList);

            var finalStop = destinationList.Last();
            if (_bases.Contains(finalStop))
            {
                SetBase(finalStop, player);
            }
        }

        private List<Transform> ParseBaseRoute(List<Transform> pBaseRoute, Player.Player player)
        {
            var crossedHome = pBaseRoute.Any(x => x == null);
            pBaseRoute = pBaseRoute.Where(x => x != null).ToList();
            if (crossedHome)
            {
                pBaseRoute.Add(baseHome);
                pBaseRoute.Add(player.DugoutPosition);
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

        public static void Out(Player.Player batter)
        {
            batter.GoToDugout();
        }

        public void CallNewBatter(Player.Player batter)
        {
            batter.SetIdlePosition(_positionManager.field.positions[Position.Batter].position);
        }
    }
}