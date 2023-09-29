using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class DialogueNode : BaseNode {
        [Input()] public int entry;
        [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

        public string character;
        public string speech;

        public override void Run() {
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.DisplayButtons(this);
            NextNode("exit");
        }
    }
}


