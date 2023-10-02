using Dialogue;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(DialogueQuestionNode))]
    public class DialogueQuestionNodeEditor : BaseNodeEditor {

        private int _speechFieldHeight = 50;
        private bool _isExpanded = false;

        private List<PropertyActionChanged<DialogueQuestionNode>> _propertyActions;

        private SerializedProperty _speech;

        public override void OnCreate() {
            _speech = serializedObject.FindProperty("speech");

            this._propertyActions = new List<PropertyActionChanged<DialogueQuestionNode>>
    {
            new PropertyActionChanged<DialogueQuestionNode>(_speech, ExpandSpeechProperty),
        };
        }

        public override void OnBodyGUI() {
            DialogueQuestionNode node = (DialogueQuestionNode)target;

            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));

            SerializedProperty characterName = serializedObject.FindProperty("character");

            EditorGUILayout.PropertyField(characterName, new GUIContent("Character Name"));
            EditorGUILayout.PropertyField(_speech, new GUIContent("Speech"), GUILayout.Height(_speechFieldHeight));

            //Draw Port list
            NodeEditorGUILayout.DynamicPortList(
                "exits",
                typeof(DialogueNode), //TODO: Change this to Luca's struct 
                serializedObject,
                NodePort.IO.Output,
                Node.ConnectionType.Override,
                Node.TypeConstraint.Strict);

            //Change the size of the object based on an event system.
            this._propertyActions.ForEach(action => {
                serializedObject.ApplyModifiedProperties();

                ExpandSpeechProperty(node);
            });
        }

        private void OnCreateReorderableList(ReorderableList list) {
            // Override drawHeaderCallback to display node's name instead
            DialogueNode node = (DialogueNode)target;

            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Answers");
            };
        }

        private void ExpandSpeechProperty(DialogueQuestionNode node) {
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


