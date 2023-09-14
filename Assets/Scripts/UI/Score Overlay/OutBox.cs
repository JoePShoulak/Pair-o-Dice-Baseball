using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI.Score_Overlay
{
    public class OutBox : MonoBehaviour
    {
        [SerializeField] private Image firstOut;
        [SerializeField] private Image secondOut;

        public void SetOuts(int numberOfOuts)
        {
            switch (numberOfOuts)
            {
                case 0:
                    firstOut.color = Color.white;
                    secondOut.color = Color.white;
                    break;
                case 1:
                    firstOut.color = Color.red;
                    secondOut.color = Color.white;
                    break;
                case 3:
                    firstOut.color = Color.red;
                    secondOut.color = Color.red;
                    break;
            }
        }
    }
}