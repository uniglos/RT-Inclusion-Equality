using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor {
    [CustomNodeEditor(typeof(SoundEffectNode))]
    public class SoundEffectEditor : BaseNodeEditor {
        public override void OnBodyGUI() {
            SoundEffectNode node = target as SoundEffectNode;
            GenerateFields(node, serializedObject);
        }
    }
}



