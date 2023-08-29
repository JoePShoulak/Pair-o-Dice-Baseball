using UnityEngine;

using InnerDriveStudios.DiceCreator;

namespace FibDev.Dice
{
    [RequireComponent(typeof(DieCollection))]
    public class RollProcessor : MonoBehaviour
    {
        private DieCollection _dieCollection;

        private void Start()
        {
            _dieCollection = GetComponent<DieCollection>();

            _dieCollection.OnRollEnd += ProcessResult;
        }

        private int GetDieRoll(DieCollection dieCollection, int index)
        {
            return dieCollection.Get(index).GetRollResult().Value();
        }

        private int GetD100Roll(DieCollection dieCollection)
        {
            return GetDieRoll(dieCollection, 0) + GetDieRoll(dieCollection, 1) * 10;
        }

        private void ProcessResult(ARollable pObj)
        {
            var result = GetD100Roll(_dieCollection);
            Debug.Log(result);
        }
    }
}