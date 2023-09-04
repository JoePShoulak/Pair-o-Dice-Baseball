using UnityEditor;
using UnityEngine;

namespace FibDev.Baseball.Choreography.Ball.Editor
{
    [CustomEditor(typeof(BallMover))]
    public class YourScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var ballMover = (BallMover)target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Pitch Ball"))
            {
                ballMover.PitchBall();
            }
            if (GUILayout.Button("Pitch Strike"))
            {
                ballMover.PitchStrike();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hit Player"))
            {
                ballMover.HitPlayer();
            }
            if (GUILayout.Button("Reset"))
            {
                ballMover.Reset();
            }
            
            
            
            GUILayout.EndHorizontal();
        }
    }
}