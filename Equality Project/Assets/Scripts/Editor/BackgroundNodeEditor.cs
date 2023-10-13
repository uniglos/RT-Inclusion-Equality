using Dialogue;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(BackgroundNode))]
    public class BackgroundNodeEditor : BaseNodeEditor {
        public override void OnBodyGUI() {
            BackgroundNode node = target as BackgroundNode;
            GenerateFields(node, serializedObject);
        }
    }
}