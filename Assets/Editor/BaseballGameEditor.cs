using FibDev.Baseball;
using UnityEditor;
using UnityEngine;

namespace FibDev.Editor
{
    [CustomEditor(typeof(BaseballGame))]
    public class BaseballGameEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = (BaseballGame)target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset State")) script.ResetState();
            if (GUILayout.Button("Next Play")) script.NextPlay();
            // if (GUILayout.Button("Debug")) Debug.Log(script.GetBases().first.runnerOn);
            GUILayout.EndHorizontal();
        }
    }
}
