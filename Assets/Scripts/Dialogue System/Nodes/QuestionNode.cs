using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class QuestionNode : DialogueBaseNode {
        
		[Output(dynamicPortList = true)][HideInInspector] public List<string> exits = new List<string>();

        protected override void DisplayUI() {
            base.DisplayUI();
            DialogueUIManager.Instance.DisplayButtons(this);
        }
    }
}