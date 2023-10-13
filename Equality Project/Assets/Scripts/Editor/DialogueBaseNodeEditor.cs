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
        private bool _isExpanded = false;

        private List<PropertyActionChanged<DialogueBaseNode>> _propertyActions;

        private SerializedProperty _speech;

        DialogueGraphPanel dialogueGraphPanel;

        /// <summary>
        /// Draw more UI inside the OnBodyGUI Method
        /// </summary>
        protected virtual void GUIBody() { }

        public override void OnCreate() {
            characterNames = AssetDatabase.LoadAssetAtPath<CharacterNames>("Assets/Scripts/Dialogue System/ScriptableObjects/CharacterNames.asset");

            _speech = serializedObject.FindProperty("speech");

            _propertyActions = new List<PropertyActionChanged<DialogueBaseNode>> {
                new PropertyActionChanged<DialogueBaseNode>(_speech, ExpandSpeechProperty),
            };

            dialogueGraphPanel = EditorWindow.GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
        }

        public override void OnBodyGUI() {
            DialogueBaseNode node = (DialogueBaseNode)target;
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));

            SerializedProperty nameIndexProperty = serializedObject.FindProperty("characterNameIndex");

            nameIndexProperty.intValue = EditorGUILayout.Popup(nameIndexProperty.intValue, characterNames.list);
            EditorGUILayout.PropertyField(_speech, new GUIContent("Speech"), GUILayout.Height(_speechFieldHeight));

            GUIBody();

            //Change the size of the object based on an event system.
            _propertyActions.ForEach(action => {
                serializedObject.ApplyModifiedProperties();

                ExpandSpeechProperty(node);
            });
        }

        private void ExpandSpeechProperty(DialogueBaseNode node) {
            //Expanding the Speech Property
            {
                //Check if the current node is selected. If is not the same node then we don't update the text
                //However this does mean that the user has to select the node
                if (node != Selection.activeObject as DialogueBaseNode) {
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


