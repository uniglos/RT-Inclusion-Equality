using System;
using UnityEditor;
using XNodeEditor;
using Dialogue;
using UnityEngine;

namespace DialogueEditor {
	[CustomNodeGraphEditor(typeof(DialogueGraph))]
	public class DialogueGraphEditor : NodeGraphEditor {

		private DialogueGraphPanel dialogueGraphPanel;

        public override void OnOpen() {
            dialogueGraphPanel = EditorWindow.GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");

            dialogueGraphPanel.position = new Rect(new Rect(NodeEditorWindow.current.position.x - 455, NodeEditorWindow.current.position.y, 350, NodeEditorWindow.current.position.size.y));

            dialogueGraphPanel.minSize = new Vector2(450, NodeEditorWindow.current.position.size.y);
            dialogueGraphPanel.maxSize = new Vector2(450, NodeEditorWindow.current.position.size.y);
        }

        public override string GetNodeMenuName(Type type) {
			if (type.BaseType != typeof(BaseNode)) {
				return null;
			}

			return base.GetNodeMenuName(type);
		}

		public override void OnGUI() {
            dialogueGraphPanel.Repaint();
        }
    }
}