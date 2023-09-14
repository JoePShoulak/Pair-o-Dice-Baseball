using FibDev.Baseball.Bases;
using UnityEngine;
using UnityEngine.UI;

namespace FibDev.UI.Score_Overlay
{
    public class BaseBox : MonoBehaviour
    {
        [SerializeField] private Image firstBase;
        [SerializeField] private Image secondBase;
        [SerializeField] private Image thirdBase;

        public void Reset()
        {
            firstBase.color = Color.white;
            secondBase.color = Color.white;
            thirdBase.color = Color.white;
        }

        public void SetBases(Bases bases)
        {
            SetBase(firstBase, bases.first.runnerOn);
            SetBase(secondBase, bases.second.runnerOn);
            SetBase(thirdBase, bases.third.runnerOn);
        }

        private static void SetBase(Graphic pBase, bool runnerOn)
        {
            pBase.color = runnerOn ? Color.yellow : Color.white;
        }
    }
}
