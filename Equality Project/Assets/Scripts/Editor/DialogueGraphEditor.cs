using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using XnodeDialogue;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(DialogueGraph))]
public class DialogueGraphEditor : NodeGraphEditor
{
    int selected = 0;
    string[] options = new string[]
    {
                "Option1", "Option2", "Option3",
    };

    public override string GetNodeMenuName(Type type) {
        if (typeof(DialogueNode).IsAssignableFrom(type)) {
            return base.GetNodeMenuName(type);
        } else if (typeof(StartNode).IsAssignableFrom(type)) {
            return base.GetNodeMenuName(type);
        } else return null;
    }

    public override void OnGUI() {
        using (new EditorGUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(250))) {
            if (GUILayout.Button("Press Me!")) {
                Debug.Log("Pressed!");
            };

            selected = EditorGUILayout.Popup("Label", selected, options);
        }
    }
}
