using Dialogue;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    public class DialogueGraphPanel : EditorWindow {

        private CharacterNames characterNames;

        private SerializedProperty characterNamesProperty;

        private SerializedObject serializedNames;

        private bool showColourSettings = true, showTextSettings = true, showNodeSettings = false;

        public static void ShowWindow() {
            if (!EditorWindow.HasOpenInstances<DialogueGraphPanel>()) {
                GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
            } else {
                NodeEditorWindow.current.ShowNotification(new GUIContent("The Dialogue Graph Panel is already open within this graph window"), 1.0f);
            }
        }

        private void OnEnable() {

			characterNames = Resources.Load("ScriptableObjects/CharacterNames") as CharacterNames;

			if (characterNames != null) {
                serializedNames = new UnityEditor.SerializedObject(characterNames);

                characterNamesProperty = serializedNames.FindProperty("list");
            } else {
                //TODO: Create Asset here
            }
        }

        private void OnGUI() {

            if (NodeEditorWindow.hasClosed) { Close(); return; }

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

                DisplayHeader(dialogueNode.name + " Node");

                EditorGUI.indentLevel++;

                GUIStyle style = EditorStyles.textField;
                style.wordWrap = true;

                showColourSettings = EditorGUILayout.Foldout(showColourSettings, "Customisation Settings");
                EditorGUI.indentLevel++;
                {
                    if (showColourSettings) {

                        EditorGUILayout.PropertyField(serializedObject.FindProperty("nameColour"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("textColour"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("fingerColour"));
                        dialogueNode.FontSize = EditorGUILayout.FloatField("Font Size", dialogueNode.FontSize);
                        dialogueNode.textSpeed = EditorGUILayout.Slider(dialogueNode.textSpeed, 1.5f, 10.0f);
                    }
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(15);

                showTextSettings = EditorGUILayout.Foldout(showTextSettings, "Text Settings");

                EditorGUI.indentLevel++;
                {
                    if (showTextSettings) {
                        dialogueNode.speech = EditorGUILayout.TextArea(dialogueNode.speech, style, GUILayout.Height(150));
                    }
                }
                EditorGUI.indentLevel--;

                serializedObject.ApplyModifiedProperties();
            } else if (Selection.activeObject is QuestionNode questionNode) {
                SerializedObject serializedObject = new SerializedObject(questionNode);

                DisplayHeader(questionNode.name + " Node");

                EditorGUI.indentLevel++;

                GUIStyle style = EditorStyles.textField;
                style.wordWrap = true;

                showColourSettings = EditorGUILayout.Foldout(showColourSettings, "Customisation Settings");

                EditorGUI.indentLevel++;
                {
                    if (showColourSettings) {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("nameColour"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("textColour"));
                        questionNode.FontSize = EditorGUILayout.FloatField("Font Size", questionNode.FontSize);
                        questionNode.textSpeed = EditorGUILayout.Slider(questionNode.textSpeed, 1.5f, 10.0f);
                    }
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space(15);

                showTextSettings = EditorGUILayout.Foldout(showTextSettings, "Text Settings");

                EditorGUI.indentLevel++;
                {
                    if (showTextSettings) {
                        questionNode.speech = EditorGUILayout.TextArea(questionNode.speech, style, GUILayout.Height(150));

                        EditorGUILayout.Space(5);
                        EditorGUILayout.LabelField("Node Exits");
                        EditorGUILayout.Space(5);

                        for (int i = 0; i < questionNode.exits.Count; i++) {
                            questionNode.exits[i] = EditorGUILayout.TextArea(questionNode.exits[i], GUILayout.Height(50));
                        }

                    }
                }
                EditorGUI.indentLevel--;

                serializedObject.ApplyModifiedProperties();
            }

            if (Selection.activeObject is BaseNode && Selection.activeObject.GetType() != typeof(StartNode)) {

                BaseNode node = Selection.activeObject as BaseNode;

                DisplayHeader("Node Customisation");

                showNodeSettings = EditorGUILayout.Foldout(showNodeSettings, "Node Customisation Settings");

                EditorGUI.indentLevel++;
                {
                    if (showNodeSettings) {
                        node.NodeColour = EditorGUILayout.ColorField("Node Colour", node.NodeColour);
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        public void DisplayHeader(string title) {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;

            EditorGUILayout.LabelField(title, style);
        }
    }
}