using TMPro;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Ball
{
    public class Messager : MonoBehaviour
    {
        [SerializeField] private TMP_Text ballMessageTMPro;

        public void UpdateMessage(string message)
        {
            ballMessageTMPro.text = $"{message}!";
        }
    }
}
