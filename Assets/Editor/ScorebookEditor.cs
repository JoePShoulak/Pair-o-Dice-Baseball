using FibDev.Core.ScoreBook;
using UnityEditor;
using UnityEngine;

namespace FibDev.Editor
{
    [CustomEditor(typeof(ScoreBook))]
    public class ScorebookEditor : UnityEditor.Editor
    {
        private ScoreBook _script;

        private void OnEnable()
        {
            _script = (ScoreBook)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Add Dummy Record A"))
            {
                var recordText = ScoreBook.ComposeRecord("Joe", "Jim", 1, 2);
                _script.AddRecord(recordText);
            }

            if (GUILayout.Button("Add Dummy Record B"))
            {
                var recordText = ScoreBook.ComposeRecord("Jake", "Sarah", 3, 4);
                _script.AddRecord(recordText);
            }

            if (GUILayout.Button("Clear"))
            {
                _script.Clear();
            }

            if (GUILayout.Button("Save"))
            {
                _script.SaveAllRecords();
            }

            if (GUILayout.Button("Load"))
            {
                _script.LoadAllRecords();
            }  
            
            if (GUILayout.Button("Delete"))
            {
                DataManager.DeleteAllScores();
            }
        }
    }
}