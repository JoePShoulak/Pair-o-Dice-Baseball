using System;
using System.Collections.Generic;
using InnerDriveStudios.DiceCreator;
using UnityEngine;

namespace FibDev
{
    [RequireComponent(typeof(DieCollection))]
    public class ResultBroadcaster : MonoBehaviour
    {
        private readonly List<int> _result = new();

        public event Action<BaseballActions.Action> OnDieResult;

        private void Start()
        {
            var dieCollection = GetComponent<DieCollection>();

            dieCollection.OnChildRollBegin += DieCollectionOnOnChildRollBegin;
            dieCollection.OnChildRollEnd += DieCollectionOnOnChildRollEnd;
        }

        private void DieCollectionOnOnChildRollBegin(DieCollection collection, ARollable rollable)
        {
            _result.Clear();
        }

        private void DieCollectionOnOnChildRollEnd(DieCollection collection, ARollable rollable)
        {
            _result.Add(rollable.GetRollResult().Value());

            if (_result.Count == collection.Count) Broadcast();
        }

        private void Broadcast()
        {
            Debug.Log($"{_result[0]}{_result[1]}");
            var action = BaseballActions.GenerateActionFromD100(_result[0] + 10 * _result[1]);
            OnDieResult?.Invoke(action);
            _result.Clear();
        }
    }
}