using Dialogue;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(StartNode))]
    public class StartNodeEditor : BaseNodeEditor {
        public override void OnBodyGUI() {
            StartNode node = target as StartNode;
            GenerateFields(node, serializedObject);
        }
    }
}