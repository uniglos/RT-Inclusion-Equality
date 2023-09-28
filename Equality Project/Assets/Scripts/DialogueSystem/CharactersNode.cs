using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XnodeDialogue;

public class CharactersNode : BaseNode {

    [Input] public int entry;
    [Output] public int exit;

    public Texture2D imageL;
    public Texture2D imageM;
    public Texture2D imageR;

    // Use this for initialization
    protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

    public override BaseNode NextNode() {
        NodePort port = GetOutputPort("exit");
        //Gets the next node based on the port connection to the next node
        BaseNode nextNode = (port.Connection.node as BaseNode).DetectNodeType(port); ;
        return nextNode;
    }
}