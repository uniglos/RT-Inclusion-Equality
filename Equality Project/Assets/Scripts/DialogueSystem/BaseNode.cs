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
    }
}

