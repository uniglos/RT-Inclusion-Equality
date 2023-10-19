using System.Collections;
using UnityEngine;
using XNode;

namespace Dialogue {
    public class BaseNode : Node {

        /// <summary>
        /// Called when the node is intialized when in play mode.
        /// </summary>
        protected virtual void Create() { }

        /// <summary>
        /// Runs this node as a coroutine.
        /// </summary>
        public virtual IEnumerator Run() {
            Debug.Log("Running Node!");
            yield return null;
            NextNode("exit");
        }

        /// <summary>
        /// Will call the NextNode based on the port connection
        /// </summary>
        /// <param name="exit">The name of the [Output] attribute name as a string</param>
        public void NextNode(string exit) {
            BaseNode node = null;

            foreach (NodePort port in this.Ports) {
                if(port.fieldName == exit) {
                    //Node we have found
                    if(port.Connection != null) {
                        //Gets the next node based on the port connection
                        node = port.Connection.node as BaseNode;
                        break;
                    } else {
                        Debug.LogError("Cannot find next avaible port: Please make sure that all ports are connected.");
                    }

                }
            }

            if (node != null) {
                //Have moved the graph.CurrentNode = node into this method to clear some reduant casting
                GraphRunner.Current.Run(node);
            }else if(node is null) {
                Debug.LogError("Cannot find next avaible node: Please make sure that all ports are connected.");
            }
        }

        // --- Xnode overrides

        [HideInInspector] public Color NodeColour = new Color32(90, 97, 105, 255);

        protected override void Init()
        {
            Create();
        }

        /// <summary>
        /// have put this here to remove the annoying warnings.
        /// </summary>
        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}


