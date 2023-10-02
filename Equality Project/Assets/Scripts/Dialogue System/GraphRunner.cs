using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class GraphRunner : MonoBehaviour {

        [SerializeField] public DialogueGraph graph;

        public static GraphRunner Current { get; private set; }

        private void Start() {
            Current = this;
            //Finds the first node in the graph
            foreach(BaseNode node in graph.nodes) {
                if(node is StartNode) {
                    graph.StartGraph(node);
                    StartCoroutine(node.Run());
                }
            }
        }

        public void Run() {
            StartCoroutine(graph.CurrentNode.Run());
        }
    }
}


