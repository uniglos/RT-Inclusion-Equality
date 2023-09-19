using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : BaseNodeEditor {

    public override void OnBodyGUI() {
        base.OnBodyGUI();

        DialogueNode node = (DialogueNode)target;

        //EditorGUILayout.Popup(0, DialogueNode.characters);
    }
}
