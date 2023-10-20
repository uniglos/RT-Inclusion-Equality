using System.Collections;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class StartNode : BaseNode {

        [Output()][HideInInspector] public int exit;

        public override IEnumerator Run() {
            yield return null;
            NextNode("exit");
        }
    }
}


