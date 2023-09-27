using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueGraphPanel : EditorWindow {

	CharacterNames characterNames;

	SerializedProperty characterNamesProperty;

	SerializedObject serializedNames;

	[MenuItem("Window/Dialogue Graph Panel")]
	public static void ShowWindow() {
		GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
	}

	private void OnEnable() {

		characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/DialogueSystem/ScriptableObjects/CharacterNames.asset");

		if (characterNames != null) {
			serializedNames = new UnityEditor.SerializedObject(characterNames);

			characterNamesProperty = serializedNames.FindProperty("characterNames");
		}
	}

	private void OnGUI() {
		//selected = EditorGUILayout.Popup("Label", selected, options);

		if (characterNames != null) {
			EditorGUILayout.PropertyField(characterNamesProperty);

			serializedNames.ApplyModifiedProperties();

		} else {
		}
	}

}