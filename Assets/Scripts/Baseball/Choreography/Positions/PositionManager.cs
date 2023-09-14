using UnityEngine;

namespace FibDev.Baseball.Choreography.Positions
{
    public class PositionManager : MonoBehaviour
    {
        public static PositionManager Instance;

         public TeamPositions homeDugout;
         public TeamPositions visitorDugout;
         public FieldPositions field;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;
        }
    }
}
