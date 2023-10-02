using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class DialogueQuestionNode : BaseNode {
        [Input()] public int entry;
        
        public string character;
        public string speech;

        [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

        public override IEnumerator Run() {
            DialogueUIManager.Instance.DisplayText(this);
            DialogueUIManager.Instance.DisplayButtons(this);
            yield return null;
        }
    }
}