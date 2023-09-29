using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class GraphRunner : MonoBehaviour {

        [SerializeField] public DialogueGraph graph;

        private void Start() {
            graph.StartGraph(graph.nodes.Find(n => n is StartNode) as BaseNode);
        }
    }
}


