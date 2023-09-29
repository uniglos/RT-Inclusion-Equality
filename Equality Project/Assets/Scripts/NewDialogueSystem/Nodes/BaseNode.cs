using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
                    if(port.Connection != null) {
                        node = port.Connection.node as BaseNode;
                        break;
                    } else {
                        Debug.LogError("Cannot find next avaible port: Please make sure that all ports are connected.");
                    }

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


