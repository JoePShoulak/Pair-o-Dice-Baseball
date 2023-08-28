using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FibDev
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 destination;
        [SerializeField] private float speed = 0.01f;
        
        void Start()
        {
            var startPosition = transform.position;
            var lerpTime = 0f;
            while (lerpTime < 1f)
            {
                lerpTime += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, destination, lerpTime);
            }
        }
    }
}
