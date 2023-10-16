using Codice.CM.Client.Differences;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    public class DialogueGraphPanel : EditorWindow {

        private CharacterNames characterNames;

        private SerializedProperty characterNamesProperty;

        private SerializedObject serializedNames;

        private bool showColourSettings = true, showTextSettings = true;

        public static void ShowWindow() {
            //GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
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

                EditorGUILayout.LabelField(dialogueNode.name + " Node");

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

                EditorGUILayout.LabelField(questionNode.name + " Node");

                GUIStyle style = EditorStyles.textField;
                style.wordWrap = true;

                showColourSettings = EditorGUILayout.Foldout(showColourSettings, "Customisation Settings");

                EditorGUI.indentLevel++;
                {
                    if (showColourSettings) {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("nameColour"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("textColour"));
                        questionNode.FontSize = EditorGUILayout.FloatField("Font Size", questionNode.FontSize);
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
                        EditorGUILayout.LabelField("Dialogue Node Exits");
                        EditorGUILayout.Space(5);

                        for (int i = 0; i < questionNode.exits.Count; i++) {
                            questionNode.exits[i] = EditorGUILayout.TextArea(questionNode.exits[i], GUILayout.Height(50));
                        }

                    }
                }
                EditorGUI.indentLevel--;

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

