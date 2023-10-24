using System;
using UnityEditor;
using XNodeEditor;
using Dialogue;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using Dialopgue.Extensions;

namespace DialogueEditor {
	[CustomNodeGraphEditor(typeof(DialogueGraph))]
	public class DialogueGraphEditor : NodeGraphEditor {

		private DialogueGraphPanel dialogueGraphPanel;

        public override void OnOpen() {
            dialogueGraphPanel = EditorWindow.GetWindow<DialogueGraphPanel>("Node Inspector");

            dialogueGraphPanel.position = new Rect(new Rect(NodeEditorWindow.current.position.x - 455, NodeEditorWindow.current.position.y, 350, NodeEditorWindow.current.position.size.y));

            dialogueGraphPanel.minSize = new Vector2(450, NodeEditorWindow.current.position.size.y);
            dialogueGraphPanel.maxSize = new Vector2(450, NodeEditorWindow.current.position.size.y);
        }

        public override string GetNodeMenuName(Type type) {
            if (type.BaseType != typeof(BaseNode)) {
                if (type.BaseType == typeof(DialogueBaseNode)) {
                    return type.Name.InsertSpace(4);
                }
                return string.Empty;
            }

            if (type == typeof(DialogueBaseNode)) return string.Empty;
            return type.Name.InsertSpace(4);
		}

        public override void AddMenuItems(GenericMenu menu) {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Show Node Inspector"), false, () => DialogueGraphPanel.ShowWindow());
        }

        public override void OnGUI() {
            dialogueGraphPanel.Repaint();
        }
    }
}