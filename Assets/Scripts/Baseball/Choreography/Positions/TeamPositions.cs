using UnityEngine;

namespace FibDev.Baseball.Choreography.Positions
{
    public class TeamPositions : MonoBehaviour
    {
        public Transform Catcher;
        public Transform Pitcher;
        public Transform Shortstop;

        public Transform Baseman1st;
        public Transform Baseman2nd;
        public Transform Baseman3rd;

        public Transform FielderLeft;
        public Transform FielderCenter;
        public Transform FielderRight;

        public Transform GetTransform(Position pPosition)
        {
            return (Transform)GetType().GetField(pPosition.ToString())?.GetValue(this);
        }
    }
}