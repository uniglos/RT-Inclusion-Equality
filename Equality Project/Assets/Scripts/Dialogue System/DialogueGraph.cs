using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    [CreateAssetMenu]
    public class DialogueGraph : NodeGraph {

        public BaseNode CurrentNode { get; set; }

        public void StartGraph(BaseNode startNode) {
            CurrentNode = startNode;
        }
    }
}

