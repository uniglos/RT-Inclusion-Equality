using System;
using UnityEditor;
using XNodeEditor;
using Dialogue;

namespace DialogueEditor {
	[CustomNodeGraphEditor(typeof(DialogueGraph))]
	public class DialogueGraphEditor : NodeGraphEditor {

		DialogueGraphPanel dialogueGraphPanel;

		public override string GetNodeMenuName(Type type) {
			if (type.BaseType != typeof(BaseNode)) {
				return null;
			}

			return base.GetNodeMenuName(type);
		}

		public override void OnGUI() {
			if (!dialogueGraphPanel)
				dialogueGraphPanel = EditorWindow.GetWindow<DialogueGraphPanel>("Dialogue Graph Panel");

			dialogueGraphPanel.Repaint();
			
		}

	}
}


