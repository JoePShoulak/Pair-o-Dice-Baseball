using UnityEditor;
using UnityEngine;

using FibDev.Baseball;

namespace FibDev.Editor
{
    [CustomEditor(typeof(Engine))]
    public class BaseballGameEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = (Engine)target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset State")) script.ResetState();
            EditorGUI.BeginDisabledGroup(script.gameEnded);
            if (GUILayout.Button("Next Play")) script.NextPlay();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Play Entire Game"))
            {
                script.ResetState();
                while (!script.gameEnded) script.NextPlay();
            }

            EditorGUI.EndDisabledGroup();

            // if (GUILayout.Button("Debug")) Debug.Log(script.GetBases().first.runnerOn);
        }
    }
}