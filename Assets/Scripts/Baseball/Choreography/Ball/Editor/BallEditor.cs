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

            if (GUILayout.Button("Pitch Ball"))
            {
                ballMover.PitchBall();
            }
            
            if (GUILayout.Button("Pitch Strike"))
            {
                ballMover.PitchStrike();
            }
            
            if (GUILayout.Button("Hit Player"))
            {
                ballMover.HitPlayer();
            }
            
            if (GUILayout.Button("Hit Line Out"))
            {
                ballMover.LineOut();
            }
            if (GUILayout.Button("Hit Pop Out"))
            {
                ballMover.HitPopOut();
            }
            
            if (GUILayout.Button("Hit Fly Out"))
            {
                ballMover.FlyOut();
            }
            
            if (GUILayout.Button("Throw to Pitcher"))
            {
                ballMover.ThrowToPitcher();
            }
                
            if (GUILayout.Button("Reset"))
            {
                ballMover.Reset();
            }
        }
    }
}