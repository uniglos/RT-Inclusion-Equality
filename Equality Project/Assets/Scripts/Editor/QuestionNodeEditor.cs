using Dialogue;
using XNode;
using XNodeEditor;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(QuestionNode))]
    public class QuestionNodeEditor : DialogueBaseNodeEditor {

        protected override void GUIBody() {
            //Draw Port list
            NodeEditorGUILayout.DynamicPortList(
                "exits",
                typeof(QuestionNode),
                serializedObject,
                NodePort.IO.Output,
                Node.ConnectionType.Override,
                Node.TypeConstraint.Strict);
        }
    }
}


