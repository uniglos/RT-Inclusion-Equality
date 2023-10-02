using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class DialogueNode : BaseNode {
        [Input()] public int entry;
        
        public string character;
        public string speech;

        [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

        public override void Run() {
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.DisplayButtons(this);
        }
    }
}