using UnityEditor;
using UnityEngine;

namespace FibDev
{
    public class GetSceneViewerPosition
    {
        [MenuItem("FibDev/Get Scene View Position and Direction")]
        private static void GetSceneViewPositionAndDirection()
        {
            if (SceneView.lastActiveSceneView == null)
            {
                Debug.LogWarning("No active Scene View found.");
                return;
            }

            var transform = SceneView.lastActiveSceneView.camera.transform;
            var position = transform.position;

            var direction = transform.forward;

            Debug.Log("Scene View Position: " + position);
            Debug.Log("Scene View Direction: " + direction);
        }
    }
}