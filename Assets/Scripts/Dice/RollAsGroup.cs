using Imports.InnerDriveStudios.DiceCreator.Scripts.DieCollection;
using UnityEngine;

namespace FibDev.Dice
{
    [RequireComponent(typeof(DieCollection))]
    public class RollAsGroup : MonoBehaviour
    {
        private void Start()
        {
            var dieCollection = GetComponent<DieCollection>();
            dieCollection.OnChildRollBegin += (collection, _) => collection.Roll();
        }
    }
}