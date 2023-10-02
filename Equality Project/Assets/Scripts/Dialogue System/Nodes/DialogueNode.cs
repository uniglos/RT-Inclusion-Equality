using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class DialogueNode : BaseNode {

        [Input] public int entry;
        [Output] public int exit;

        public string character;
        public string speech;

        public override IEnumerator Run() {
            DialogueUIManager.Instance.DisplayText(this);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            NextNode("exit");
        }
    }
}


