using UnityEngine;

namespace Dialogue {
    public class GraphRunner : MonoBehaviour {

        [SerializeField] public DialogueGraph graph;

        public static GraphRunner Current { get; private set; }

        private void Start() {
            if(graph != null) {
                Current = this;
                //Finds the first node in the graph
                foreach (BaseNode node in graph.nodes) {
                    if (node is StartNode) {
                        graph.CurrentNode = node;
                        //This it the starting a coroutine for a the start node
                        StartCoroutine(node.Run());
                    }
                }
            } else {
                Debug.LogError("The Graph is null in the inspector: Please Assign it.");
                return;
            }
        }

        /// <summary>
        /// Runs the next node in the graph
        /// </summary>
        public void Run(BaseNode next) {
            graph.CurrentNode = next;
            StartCoroutine(graph.CurrentNode.Run());

            //Debug.Log("Next Node: " + next.name);
        }
    }
}


