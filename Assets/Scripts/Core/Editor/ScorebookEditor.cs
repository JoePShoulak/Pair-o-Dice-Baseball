using FibDev.Dice;
using UnityEditor;
using UnityEngine;

namespace FibDev.Core.Editor
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
                _script.AddRecord("Joe", "Jim", 1, 2);
            }    if (GUILayout.Button("Add Dummy Record B"))
            {
                _script.AddRecord("Jim", "Joe", 2, 1);
            }
        }

    }
}