using Dialogue;
using UnityEditor;
using UnityEngine;

namespace DialogueEditor {
    public class DialogueGraphPanel : EditorWindow {

        private CharacterNames characterNames;

        private SerializedProperty characterNamesProperty;

        private SerializedObject serializedNames;

        public static void ShowWindow() {
            GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
        }

        private void OnEnable() {

            characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/Dialogue System/ScriptableObjects/CharacterNames.asset");

            if (characterNames != null) {
                serializedNames = new UnityEditor.SerializedObject(characterNames);

                characterNamesProperty = serializedNames.FindProperty("list");
            } else {
                //TODO: Create Asset here
            }
        }

        private void OnGUI() {
			if (characterNames != null) {
				EditorGUILayout.PropertyField(characterNamesProperty);

				serializedNames.ApplyModifiedProperties();

			} else {
				GUIStyle style = GUI.skin.textArea;
				style.wordWrap = true;

				EditorGUILayout.LabelField("Character Names asset has not been found: Please create one", style);
			}

			EditorGUILayout.Space();

			//TODO: use the new dialogue base node here

			if (Selection.activeObject is DialogueNode dialogueNode) {
				SerializedObject serializedObject = new SerializedObject(dialogueNode);

				EditorGUILayout.LabelField(dialogueNode.name + " Node");
				EditorGUILayout.PropertyField(serializedObject.FindProperty("nameColour"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("textColour"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("fingerColour"));

				serializedObject.ApplyModifiedProperties();
			} else if (Selection.activeObject is QuestionNode questionNode) {
				SerializedObject serializedObject = new SerializedObject(questionNode);

				EditorGUILayout.LabelField(questionNode.name);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("nameColour"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("textColour"));

				serializedObject.ApplyModifiedProperties();
			}
		}

    }
}

