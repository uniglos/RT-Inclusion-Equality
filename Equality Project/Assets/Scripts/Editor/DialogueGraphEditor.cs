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
   public override string GetNodeMenuName(Type type) {
        if (type.BaseType != typeof(BaseNode)) {
            return null;
        }

        return base.GetNodeMenuName(type);
    }

    public override void OnGUI() {
        using (new EditorGUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(250))) {
            if (GUILayout.Button("Press Me!")) {
                Debug.Log("Pressed!");
            };
        }
    }
}
