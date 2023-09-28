using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XnodeDialogue;

public class StartNode : BaseNode {

    [Output] public int exit;

    public override BaseNode NextNode() {
        NodePort port = GetOutputPort("exit");
        //Gets the next node based on the port connection to the next node
        BaseNode nextNode = (port.Connection.node as BaseNode).DetectNodeType(port);
        return nextNode;
    }
}