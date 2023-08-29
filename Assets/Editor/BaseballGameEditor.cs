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
            if (GUILayout.Button("Next Event")) script.NextEvent();
            GUILayout.EndHorizontal();
        }
    }
}
