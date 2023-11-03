using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(BGMNode))]
    public class BGMEditor : BaseNodeEditor {
        public override void OnBodyGUI() {
            BGMNode node = target as BGMNode;
            GenerateFields(node, serializedObject);
        }
    }
}



