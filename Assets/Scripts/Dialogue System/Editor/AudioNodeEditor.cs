using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(AudioNode))]
    public class AudioNodeEditor : BaseNodeEditor {
        public override void OnBodyGUI() {
            AudioNode node = target as AudioNode;
            GenerateFields(node, serializedObject);
        }
    }
}



