using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using Dialogue;
using UnityEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(DialogueBaseNode))]
    public class DialogueBaseNodeEditor : BaseNodeEditor {
        CharacterNames characterNames;

        private int _speechFieldHeight = 50;

        private SerializedProperty _speech;

        /// <summary>
        /// Draw more UI inside the OnBodyGUI Method
        /// </summary>
        protected virtual void GUIBody() { }

        public override void OnCreate() {
			characterNames = Resources.Load("ScriptableObjects/CharacterNames") as CharacterNames;

			_speech = serializedObject.FindProperty("speech");
        }

        public override void OnBodyGUI() {
            DialogueBaseNode node = (DialogueBaseNode)target;
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));

            SerializedProperty nameIndexProperty = serializedObject.FindProperty("characterNameIndex");

            nameIndexProperty.intValue = EditorGUILayout.Popup(nameIndexProperty.intValue, characterNames.list);
            EditorGUILayout.PropertyField(_speech, new GUIContent("Speech"), GUILayout.Height(_speechFieldHeight));

            GUIBody();
        }
    }
}


