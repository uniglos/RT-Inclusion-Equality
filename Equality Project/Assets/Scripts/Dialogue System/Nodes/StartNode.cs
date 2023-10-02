using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class StartNode : BaseNode {

        [Output()] public int exit;

        public override IEnumerator Run() {
            yield return null;
            NextNode("exit");
        }
    }
}


