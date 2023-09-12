using FibDev.Baseball.Choreography.Positions;
using FibDev.Baseball.Teams;
using UnityEngine;
using UnityEngine.AI;

namespace FibDev.Baseball.Choreography.Choreographer
{
    public class BaseManager : MonoBehaviour
    {
        private NavMeshAgent runnerOnFirst;
        private NavMeshAgent runnerOnSecond;
        private NavMeshAgent runnerOnThird;

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

        public void Advance(Player.Player batter, int times = 1)
        {
            batter.SetIdlePosition(baseFirst);
            runnerOnFirst = batter.Agent;
            
            Debug.Log($"Advancing batter and runners {times} times");
        }

        public void AdvanceIfForced(Player.Player batter)
        {
            batter.SetIdlePosition(baseFirst);
            runnerOnFirst = batter.Agent;
            
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