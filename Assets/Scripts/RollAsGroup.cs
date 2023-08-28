using InnerDriveStudios.DiceCreator;
using UnityEngine;

namespace FibDev
{
    [RequireComponent(typeof(DieCollection))]
    public class RollAsGroup : MonoBehaviour
    {
        private void Start()
        {
            var dieCollection = GetComponent<DieCollection>();

            dieCollection.OnChildRollBegin += DieCollectionOnOnChildRollBegin;
        }

        private void RollAllDiceInCollection(DieCollection collection)
        {
            foreach (var rollable in collection.GetComponentsInChildren<ARollable>())
            {
                if (!rollable.isRolling) rollable.Roll();
            }
        }

        private void DieCollectionOnOnChildRollBegin(DieCollection collection, ARollable rollable)
        {
            RollAllDiceInCollection(collection);
        }
    }
}