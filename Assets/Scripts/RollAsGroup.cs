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
            dieCollection.OnChildRollBegin += (collection, _) => collection.Roll();
        }
    }
}