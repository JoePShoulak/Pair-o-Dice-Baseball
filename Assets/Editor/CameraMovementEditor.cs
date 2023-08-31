using FibDev.Core;
using UnityEditor;
using UnityEngine;

namespace FibDev.Editor
{
    [CustomEditor(typeof(CameraMovement))]
    public class CameraMovementEditor : UnityEditor.Editor
    {
        private bool debugFoldout;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = (CameraMovement)target;

            debugFoldout = EditorGUILayout.Foldout(debugFoldout, "Debug");

            if (debugFoldout)
            {
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();

                GUILayout.Label("Move Camera to...");

                if (GUILayout.Button("Start")) script.MoveTo(script.start);
                if (GUILayout.Button("Stadium")) script.MoveTo(script.stadium);
                if (GUILayout.Button("Notebook")) script.MoveTo(script.notebook);

                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            }
        }
    }
}