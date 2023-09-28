using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace XnodeDialogue {
    public class BaseNode : Node {

        protected bool ShouldOveride { get; set; }

        /// <summary>
        /// A way to get the next nodes
        /// </summary>
        /// <returns>The next node as a BaseNode</returns>
        public virtual BaseNode NextNode() {
            if (!ShouldOveride) {
                return null;
            }

            DialogueGraphLogger.Log("Node Error: NextNode function should be overriden!", DialogueGraphLogger.ELogError.Warning);
            return null;
        }

        public BaseNode DetectNodeType(NodePort port) {

            if (port.Connection.node is StartNode) {
                return port.Connection.node as StartNode;
            }

            if (port.Connection.node is DialogueNode) {
                DialogueUIManager.Instance.Draw(port.Connection.node as DialogueNode);
                return port.Connection.node as DialogueNode;
            }

            if (port.Connection.node is CharactersNode characterNode) {
                DialogueUIManager.Instance.Draw(characterNode);
                characterNode.NextNode();
                return port.Connection.node as CharactersNode;
            }

            if (port.Connection.node is BackgroundNode backgroundNode) {
                DialogueUIManager.Instance.Draw(port.Connection.node as BackgroundNode);
                backgroundNode.NextNode();
                return port.Connection.node as BackgroundNode;
            }

            //End Dialogue

            if (port.Connection.node == null) {
                Debug.Log("Ended Dialogue");
                return null;
            }

            return null;
        }
    }
}

