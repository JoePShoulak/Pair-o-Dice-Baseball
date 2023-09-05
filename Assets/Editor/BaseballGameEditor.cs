using UnityEditor;
using UnityEngine;
using FibDev.Baseball;
using FibDev.Baseball.Engine;
using FibDev.Baseball.Plays;

namespace FibDev.Editor
{
    [CustomEditor(typeof(Engine))]
    public class EngineEditor : UnityEditor.Editor
    {
        private bool showPlaysSection = true;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = (Engine)target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset State")) script.ResetState();
            EditorGUI.BeginDisabledGroup(script.gameEnded);
            if (GUILayout.Button("Random Play")) script.NextPlay(Play.Random());
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Play Until Finished"))
            {
                while (!script.gameEnded) script.NextPlay();
            }
            
            showPlaysSection = EditorGUILayout.Foldout(showPlaysSection, "Plays");
            if (showPlaysSection)
            {
                EditorGUI.indentLevel++;
                foreach (var play in Play.plays.Values)
                {
                    if (GUILayout.Button(play.name)) script.NextPlay(play);
                }
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndDisabledGroup();

            // if (GUILayout.Button("Debug")) Debug.Log(script.GetBases().first.runnerOn);
        }
    }
}