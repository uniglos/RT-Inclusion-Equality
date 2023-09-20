using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : BaseNodeEditor {

    private int _speechFieldHeight = 50;
    private bool _isExpanded = false;

    public override void OnBodyGUI() {
        base.OnBodyGUI();

        DialogueNode node = (DialogueNode)target;

        serializedObject.Update();

        SerializedProperty characterName = serializedObject.FindProperty("characterName");
        SerializedProperty speech = serializedObject.FindProperty("speech");

        EditorGUILayout.PropertyField(characterName, new GUIContent("Character Name"));

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(speech, new GUIContent("Speech"), GUILayout.Height(_speechFieldHeight));

        //If the user has clicked on the property field
        if(GUIUtility.hotControl != 0 && !_isExpanded) {
            _speechFieldHeight = 200;
            _isExpanded = true;  
        }

        //Get the key event and check if the user has pressed enter
        Event e = Event.current;

        if (e.keyCode == KeyCode.Return) {
            //Check if the speech box is expanded if true reset vars
            if (GUIUtility.hotControl == 0 && _isExpanded) {
                _speechFieldHeight = 50;
                _isExpanded = false;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}


public class NodeDialogueWindow : EditorWindow {

    public static void Open(SerializedProperty property) {

    }

}