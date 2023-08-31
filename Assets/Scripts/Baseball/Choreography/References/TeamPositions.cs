using UnityEngine;

namespace FibDev.Baseball.Choreography.References
{
    public class TeamPositions : MonoBehaviour
    {
        [SerializeField] private Transform Catcher;
        [SerializeField] private Transform Pitcher;
        [SerializeField] private Transform Shortstop;

        [SerializeField] private Transform Baseman1st;
        [SerializeField] private Transform Baseman2nd;
        [SerializeField] private Transform Baseman3rd;

        [SerializeField] private Transform FielderLeft;
        [SerializeField] private Transform FielderCenter;
        [SerializeField] private Transform FielderRight;

        public Transform GetTransform(Position pPosition)
        {
            return (Transform)GetType().GetField(pPosition.ToString())?.GetValue(this);
        }
    }
}