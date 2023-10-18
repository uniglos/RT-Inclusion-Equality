using Dialogue;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(CharactersNode))]
    public class ImagesNodeEditor : BaseNodeEditor {
        
        public override void OnBodyGUI() {
            CharactersNode node = target as CharactersNode;
            GenerateFields(node, serializedObject);
        }
}
}


