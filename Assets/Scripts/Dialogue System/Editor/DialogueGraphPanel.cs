using Dialogue;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    public class DialogueGraphPanel : EditorWindow {

        public static DialogueGraphPanel Current { get; private set; }

        private bool scanning = false;

        private CharacterNames characterNames;

        private SerializedProperty characterNamesProperty;

        private SerializedObject serializedNames;

        private bool showColourSettings = true, showTextSettings = true, showNodeSettings = false;

        public static DialogueGraphPanel ShowWindow() {
            if (!EditorWindow.HasOpenInstances<DialogueGraphPanel>()) {
                Current = GetWindow<DialogueGraphPanel>("Node Inspector");
                return Current;
            } else {
                NodeEditorWindow.current.ShowNotification(new GUIContent("The Node Inspector is already open within this graph window"), 1.0f);
            }

            return Current;
        }

        private void OnEnable() {

            characterNames = Resources.Load("ScriptableObjects/CharacterNames") as CharacterNames;

            if (characterNames != null) {
                serializedNames = new SerializedObject(characterNames);

                characterNamesProperty = serializedNames.FindProperty("list");
            } else {
                //TODO: Create Asset here
            }
        }

        private void OnGUI() {

            if (NodeEditorWindow.hasClosed) { Close(); return; }

            using (new EditorGUI.DisabledGroupScope(scanning)) {
                if (GUILayout.Button("Scan for keywords")) {
                    scanning = true;
                    try {
                        foreach (var n in NodeEditorWindow.current.graph.nodes) {
                            if (n.GetType().BaseType == typeof(DialogueBaseNode)) {
                                DialogueBaseNode node = (DialogueBaseNode)n;
                                node.CheckText();
                            }
                            scanning = false;
                        }
                    } catch (Exception) {
                        scanning = false;
                        throw;
                    }
                }

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
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("fontAsset"));
                            dialogueNode.FontSize = EditorGUILayout.FloatField("Font Size", dialogueNode.FontSize);
                            EditorGUILayout.LabelField("Text speed");
                            dialogueNode.textSpeed = EditorGUILayout.Slider(dialogueNode.textSpeed, 5.0f, 10.0f);
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
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("fontAsset"));
                            questionNode.FontSize = EditorGUILayout.FloatField("Font Size", questionNode.FontSize);
                            EditorGUILayout.LabelField("Text speed");
                            questionNode.textSpeed = EditorGUILayout.Slider(questionNode.textSpeed, 5.0f, 10.0f);
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

                if (Selection.activeObject is SoundEffectNode audioNode) {
                    SerializedObject serializedObject = new SerializedObject(audioNode);
                    EditorGUILayout.LabelField("Volume");
                    audioNode.volume = EditorGUILayout.Slider(audioNode.volume, 0.0f, 1.0f);
                }

                if (Selection.activeObject is BGMNode BGMaudioNode)
                {
                    SerializedObject serializedObject = new SerializedObject(BGMaudioNode);
                    EditorGUILayout.LabelField("Volume");
                    BGMaudioNode.volume = EditorGUILayout.Slider(BGMaudioNode.volume, 0.0f, 1.0f);
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
        }

        private static void DisplayHeader(string title) {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;

            EditorGUILayout.LabelField(title, style);
        }
    }
}