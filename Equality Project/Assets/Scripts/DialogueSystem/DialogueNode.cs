using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XnodeDialogue;
using UnityEngine.Rendering;
using System.Linq;


public class DialogueNode : BaseNode {

    [Input] public int entry;
    [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

    public string characterName;
    public string speech;

    public BaseNode AnswerQuestion(int index) {
        NodePort port = null; 

        //TODO: Change this to an enum
        if(exits.Count == 0) {
            return null;
        }else if(exits.Count > 0)
        {
            //Get the port according the current port with the index
            port = GetOutputPort("exits " + index);
        }

        if (port != null) {
            //loop through all the connection and get the node connecting them and return that node
            NodePort connection = port.GetConnection(0);

            Debug.Log("Next Node is: " + connection.Connection.node.name);

            return (connection.Connection.node as BaseNode).DetectNodeType(connection);
        }

        return null;
    }

    public override object GetValue(NodePort port) {
        return null;
    }

    public override BaseNode NextNode() {
        return null;
    }
}