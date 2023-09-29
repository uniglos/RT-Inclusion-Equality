using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class BaseNode : Node {
        protected override void Init() {
            base.Init();
        }

        public virtual void Run() {
            Debug.Log("Running Node!");
            NextNode("exit");
        }

        public void NextNode(string exit) {
            BaseNode node = null;

            foreach (NodePort port in this.Ports) {
                if(port.fieldName == exit) {
                    //Node we have found
                    node = port.Connection.node as BaseNode;
                    break;
                }
            }

            if (node != null) {
                DialogueGraph graph = this.graph as DialogueGraph;
                graph.CurrentNode = node;
                graph.Run();
            }
        }
    }
}


