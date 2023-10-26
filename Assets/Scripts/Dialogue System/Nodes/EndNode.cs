using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class EndNode : BaseNode {
        [Input] public int entry;

        public override IEnumerator Run() {

            DialogueUIManager.Instance.EndDialogue();
            yield return null;

            GraphRunner.Current.graph = null;
        }
    }
}


