using Codice.Client.BaseCommands;
using DialogueEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Dialogue {
	[CustomNodeEditor(typeof(DialogueNode))]
	public class DialogueNodeEditor : BaseNodeEditor {

		CharacterNames characterNames;

		private int _speechFieldHeight = 50;
		private bool _isExpanded = false;

		private List<PropertyActionChanged<DialogueNode>> _propertyActions;

		private SerializedProperty _speech;

		DialogueGraphPanel dialogueGraphPanel;

		public override void OnCreate() {
			characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/Dialogue System/Scriptable Objects/CharacterNames.asset");

			_speech = serializedObject.FindProperty("speech");

			_propertyActions = new List<PropertyActionChanged<DialogueNode>> {
				new PropertyActionChanged<DialogueNode>(_speech, ExpandSpeechProperty),
			};

			dialogueGraphPanel = EditorWindow.GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
		}

		public override void OnBodyGUI() {
			DialogueNode node = (DialogueNode)target;
			serializedObject.Update();

			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));

			SerializedProperty nameIndexProperty = serializedObject.FindProperty("characterNameIndex");

			nameIndexProperty.intValue = EditorGUILayout.Popup(nameIndexProperty.intValue, characterNames.list);
			EditorGUILayout.PropertyField(_speech, new GUIContent("Speech"), GUILayout.Height(_speechFieldHeight));

			NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));

			//Change the size of the object based on an event system.
			_propertyActions.ForEach(action => {
				serializedObject.ApplyModifiedProperties();

				ExpandSpeechProperty(node);
			});
		}

		private void ExpandSpeechProperty(DialogueNode node) {
			//Expanding the Speech Property
			{
				//Check if the current node is selected. If is not the same node then we don't update the text
				//However this does mean that the user has to select the node
				if (node != Selection.activeObject as DialogueNode) {
					_speechFieldHeight = 50;
					_isExpanded = false;
					return;
				}

				if (node != null) {
					//If the user has clicked on the property field
					if (EditorGUIUtility.hotControl != 0 && !_isExpanded) {

						if (Selection.objects.Length > 1) {
							NodeEditorWindow.current.ShowNotification(new GUIContent("Graph Editor: Multiple Node editing is not supported"), 0.75f);
							return;
						}

						_speechFieldHeight = 200;
						_isExpanded = true;
					}

					//Get the key event and check if the user has pressed enter
					Event e = Event.current;

					if (e.keyCode == KeyCode.Return) {
						//Check if the speech box is expanded if true reset vars
						if (EditorGUIUtility.hotControl == 0 && _isExpanded) {
							_speechFieldHeight = 50;
							_isExpanded = false;
						}
					}
				} else {
					_speechFieldHeight = 50;
					_isExpanded = false;
				}
			}
		}
	}
}


