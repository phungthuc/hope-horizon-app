using System.Linq;
using Game.Screens;
using UnityEditor;
using UnityEngine;

namespace Game.Core.UITemplate.Editors.Screen
{
    [CustomEditor(typeof(ActiveScreen))]
    public class ActiveScreenEditor : Editor
    {
        private SerializedProperty key;
        private SerializedProperty screenManager;
        private SerializedProperty activeOnStart;
        private SerializedProperty saveHistory;

        private int selectedKeyIndex;

        private void OnEnable()
        {
            key = serializedObject.FindProperty("key");
            screenManager = serializedObject.FindProperty("screenManager");
            activeOnStart = serializedObject.FindProperty("activeOnStart");
            saveHistory = serializedObject.FindProperty("saveHistory");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(screenManager, new GUIContent("Screen Manager"));
            EditorGUILayout.PropertyField(activeOnStart, new GUIContent("Active On Start"));
            EditorGUILayout.PropertyField(saveHistory, new GUIContent("Save To History"));
            DisplayKeyPopup();
            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayKeyPopup()
        {
            var handler = screenManager.objectReferenceValue as ScreenManager;
            if (!handler)
            {
                return;
            }
            var options = handler.ScreenConfig.ScreenKeyList.Select(key => key).ToList();
            selectedKeyIndex = options.IndexOf(key.stringValue);
            if(selectedKeyIndex == -1)
            {
                selectedKeyIndex = 0;
            }
            selectedKeyIndex = EditorGUILayout.Popup("Key", selectedKeyIndex, options.ToArray());
            key.stringValue = options[selectedKeyIndex];
        }
    }
}