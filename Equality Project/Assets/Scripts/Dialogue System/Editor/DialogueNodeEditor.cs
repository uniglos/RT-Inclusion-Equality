using XNodeEditor;
using Dialogue;

namespace DialogueEditor {
	[CustomNodeEditor(typeof(DialogueNode))]
	public class DialogueNodeEditor : DialogueBaseNodeEditor {

        protected override void GUIBody() {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));
        }
    }
}


