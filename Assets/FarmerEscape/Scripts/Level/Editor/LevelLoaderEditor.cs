using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Level.Editor
{
    [CustomEditor(typeof(LevelLoader))]
    public class LevelLoaderEditor : UnityEditor.Editor
    {
        static int index = 1;
        private LevelLoader _levelLoader;

        private void OnEnable()
        {
            _levelLoader = (LevelLoader) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            index = EditorGUILayout.IntField("Level Index", index);
            if (GUILayout.Button("Load Level"))
            {
                _levelLoader.LoadLevel(index);
            }
        }
    }
}
