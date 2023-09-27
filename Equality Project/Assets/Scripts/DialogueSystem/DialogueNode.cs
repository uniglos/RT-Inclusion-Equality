﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static XNode.Node;
using XnodeDialogue;
using UnityEngine.Rendering;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;

public class DialogueNode : BaseNode {

    [Input] public int entry;
    [Output(dynamicPortList = true)] public List<string> exits = new List<string>();

    public string characterName;
    public string speech;

    public DialogueNode AnswerQuestion(int index) {
        NodePort port = null;

        //TODO: Change this to an enum
        if(exits.Count == 0) {
            Debug.LogError("Exits is zero!");
        }else if(exits.Count > 0)
        {
            //Get the port according the current port with the index
            port = GetOutputPort("exits " + index);
        }

        if (port != null) {
			NodePort connection = port.GetConnection(0);
			return connection.node as DialogueNode;
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