using System;
using Imports.InnerDriveStudios.DiceCreator.Scripts.Die;
using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using UnityEngine;

namespace FibDev.Dice
{
    [RequireComponent(typeof(DieCollection))]
    public class RollProcessor : MonoBehaviour
    {
        private DieCollection _dice;

        public static event Action<int> OnRollProcessed;

        private void Start()
        {
            _dice = GetComponent<DieCollection>();
            _dice.OnRollEnd += ProcessResult;
        }

        private static int GetDieRoll(DieCollection dice, int index)
        {
            return dice.Get(index).GetRollResult().Value();
        }

        private static int GetD100Roll(DieCollection dice)
        {
            return GetDieRoll(dice, 0) + GetDieRoll(dice, 1) * 10;
        }

        private void ProcessResult(ARollable _)
        {
            var result = GetD100Roll(_dice);
            Debug.Log(result);
            OnRollProcessed?.Invoke(result);
        }
    }
}