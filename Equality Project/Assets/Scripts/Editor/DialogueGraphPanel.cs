using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
	public class DialogueGraphPanel : EditorWindow {

		CharacterNames characterNames;

		SerializedProperty characterNamesProperty;

		SerializedObject serializedNames;

		[MenuItem("Window/Dialogue Graph Panel")]
		public static void ShowWindow() {
			GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
		}

		private void OnEnable() {

			characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/Dialogue System/Scriptable Objects/CharacterNames.asset");

			if (characterNames != null) {
				serializedNames = new SerializedObject(characterNames);

				characterNamesProperty = serializedNames.FindProperty("list");
			} else {
				//TODO: Create Asset here
			}
		}

		private void OnGUI() {
			//selected = EditorGUILayout.Popup("Label", selected, options);

			if (characterNames != null) {
				EditorGUILayout.PropertyField(characterNamesProperty);

				serializedNames.ApplyModifiedProperties();

			} else {
				GUIStyle style = GUI.skin.textArea;
				style.wordWrap = true;

				EditorGUILayout.LabelField("Character Names asset has not been found: Please create one", style);
			}
		}

	}
}

