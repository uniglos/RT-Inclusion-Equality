using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using Dialogue;

namespace DialogueEditor {
    [CustomNodeGraphEditor(typeof(DialogueGraph))]
    public class DialogueGraphEditor : NodeGraphEditor {
        public override string GetNodeMenuName(Type type) {
            //if (type.BaseType != typeof(BaseNode)) {
            //	return null;
            //}

            return base.GetNodeMenuName(type);
        }

        public override void OnGUI() {
            using (new EditorGUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(50))) {

                if (GUILayout.Button("Open Panel")) {
                    EditorWindow.GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");
                }
            }
        }
    }
}


